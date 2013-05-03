ALTER TABLE dbo.VillagesAttendance
ADD loan_id INT NOT NULL DEFAULT (0)

INSERT INTO dbo.GeneralParameters
        ( [key], value )
VALUES  ( 'MAX_GUARANTOR_AMOUNT', 2000000000)

UPDATE dbo.SavingBookContracts 
SET term_deposit_period=0 
WHERE term_deposit_period IS NULL 
GO

ALTER TABLE SavingBookContracts 
ALTER COLUMN term_deposit_period int NOT NULL
GO

ALTER TABLE SavingBookContracts 
ADD CONSTRAINT period_default 
DEFAULT 0 FOR  term_deposit_period
GO

ALTER TABLE [dbo].[SavingBookContracts]
ADD [next_maturity] DATETIME NULL 
GO

ALTER TABLE dbo.ChartOfAccounts
ADD lft INT NOT NULL DEFAULT(0)
, rgt INT NOT NULL DEFAULT(0)
GO

-- Tree holds the adjacency model 
DECLARE @Tree TABLE 
(
	child INT NOT NULL
	, parent INT
)
-- A fake root account
INSERT INTO @Tree
SELECT 0, NULL
-- Accounts
INSERT INTO @Tree
SELECT id, ISNULL(parent_account_id, 0)
FROM dbo.ChartOfAccounts

-- Stack starts empty, will hold the nested set model 
DECLARE @Stack TABLE
(
	stack_top INT NOT NULL
	, child INT NOT NULL
	, lft INT
	, rgt INT
)

DECLARE @lft_rgt INT
	, @stack_pointer INT
	, @max_lft_rgt INT
	
SET @max_lft_rgt = 2 * (SELECT COUNT(*) FROM @Tree)
INSERT INTO @Stack SELECT 1, child, 1, @max_lft_rgt 
FROM @Tree 
WHERE parent IS NULL

SET @lft_rgt = 2
SET @Stack_pointer = 1
DELETE FROM @Tree WHERE parent IS NULL

-- The Stack is now loaded and ready to use
WHILE (@lft_rgt < @max_lft_rgt) 
BEGIN 
	IF EXISTS (SELECT * FROM @Stack AS S1, @Tree AS T1 WHERE S1.child = T1.parent AND S1.stack_top = @stack_pointer) 
	BEGIN -- push when stack_top has subordinates and set lft value 
		INSERT INTO @Stack 
		SELECT (@stack_pointer + 1), MIN(T1.child), @lft_rgt, NULL 
		FROM @Stack AS S1, @Tree AS T1 WHERE S1.child = T1.parent AND S1.stack_top = @stack_pointer;
		
		-- remove this row from Tree 
		DELETE FROM @Tree WHERE child = (SELECT child FROM @Stack WHERE stack_top = @stack_pointer + 1)
		SET @stack_pointer = @stack_pointer + 1
	END -- push 
	ELSE 
	BEGIN -- pop the Stack and set rgt value 
		UPDATE @Stack SET rgt = @lft_rgt, stack_top = -stack_top 
		WHERE stack_top = @stack_pointer
		
		SET @stack_pointer = @stack_pointer - 1
	END -- pop 
	SET @lft_rgt = @lft_rgt + 1
END -- WHILE

DELETE FROM @Stack WHERE child = 0
UPDATE @Stack SET lft = lft - 1, rgt = rgt - 1

UPDATE coa
SET coa.lft = s.lft, coa.rgt = s.rgt
FROM dbo.ChartOfAccounts coa
LEFT JOIN @Stack s ON s.child = coa.id
GO

-- Dumping PersonCustomizableField and CustomizableFieldSettings tables

DECLARE @table TABLE
(
	number INT IDENTITY(1,1) NOT NULL
	, [use] BIT
	, [name] NVARCHAR(100)
	, [type] NVARCHAR(50)
	, [mandatory] BIT
	, [unique] BIT
	, old_number INT
)

INSERT INTO @table
SELECT [use], [name], [type], [mandatory], [unique], [number] FROM dbo.CustomizableFieldsSettings WHERE [use] = 1

DELETE FROM dbo.PersonCustomizableFields WHERE [key] NOT IN (SELECT number FROM dbo.CustomizableFieldsSettings WHERE [use] = 1)
UPDATE dbo.CustomizableFieldsSettings SET [use] = 0, [name] = '', [type] = '', mandatory = 0, [unique] = 0
UPDATE dbo.CustomizableFieldsSettings SET [use] = t.[use], [name] = t.name, [type] = t.type, mandatory = t.mandatory, [unique] = t.[unique] FROM @table t WHERE CustomizableFieldsSettings.number = t.number
UPDATE dbo.PersonCustomizableFields SET [key] = t.number FROM @table t WHERE PersonCustomizableFields.[key] = t.old_number
GO

-- Migrating from Customizable fields to advanced customizable fields

DECLARE @temp INT
SELECT @temp = IDENT_CURRENT('AdvancedFields')

INSERT INTO dbo.AdvancedFields
        ( entity_id ,
          type_id ,
          name ,
          [desc] ,
          is_mandatory ,
          is_unique
        )
SELECT  1 AS entity_id,
		(CASE WHEN [type] = 'String' THEN 3
		WHEN [type] = 'Boolean' THEN 1 END) AS [type_id],
		(CASE WHEN mandatory = 1 THEN name + '*'
			  ELSE [name]
		END) AS name,
		'' AS [desc],
		[mandatory] AS is_mandatory,
		[unique] AS is_unique
FROM dbo.CustomizableFieldsSettings WHERE [use] = 1

DECLARE db_cursor CURSOR FOR
SELECT pcf.person_id, pcf.[key], pcf.value FROM dbo.PersonCustomizableFields pcf
INNER JOIN dbo.CustomizableFieldsSettings cfs ON cfs.number = pcf.[key]
WHERE cfs.[use] = 1
ORDER BY person_id, [key]

DECLARE @id INT
DECLARE @key INT
DECLARE @value NVARCHAR(100)

OPEN db_cursor
FETCH NEXT FROM db_cursor
INTO @id, @key, @value

WHILE 0 = @@FETCH_STATUS
	BEGIN
		IF NOT EXISTS (SELECT * FROM dbo.AdvancedFieldsLinkEntities WHERE link_type = 'C' AND link_id = @id)
		INSERT INTO dbo.AdvancedFieldsLinkEntities
		        ( link_type, link_id )
		VALUES  ( 'C', @id )
		
		DECLARE @entity_field_id INT
		SELECT @entity_field_id = id FROM dbo.AdvancedFieldsLinkEntities WHERE link_type = 'C' AND link_id = @id
		
		DECLARE @person_id INT
		SET @person_id = @id

		INSERT INTO dbo.AdvancedFieldsValues
		        ( entity_field_id, field_id, value )
		VALUES  ( @entity_field_id, @temp + @key, @value )
		
		FETCH NEXT FROM db_cursor
		INTO @id, @key, @value
		
		WHILE (0 = @@FETCH_STATUS AND @id = @person_id)
		BEGIN

			INSERT INTO dbo.AdvancedFieldsValues
				    ( entity_field_id, field_id, value )
			VALUES  ( @entity_field_id, @temp + @key, @value )
		    
			FETCH NEXT FROM db_cursor
			INTO @id, @key, @value	
		END
	END
	
CLOSE db_cursor
DEALLOCATE db_cursor
GO

DROP TABLE dbo.CustomizableFieldsSettings
DROP TABLE dbo.PersonCustomizableFields
GO

IF EXISTS (SELECT TABLE_NAME FROM INFORMATION_SCHEMA.VIEWS
         WHERE TABLE_NAME = 'Export_LoanSizeMaturityGraceDomainDistrict')
         DROP VIEW dbo.Export_LoanSizeMaturityGraceDomainDistrict
GO

DELETE FROM dbo.GeneralParameters WHERE [key] = 'BRANCH_NAME'
DELETE FROM dbo.GeneralParameters WHERE [key] = 'BRANCH_CODE'
DELETE FROM dbo.GeneralParameters WHERE [key] = 'BRANCH_ADDRESS'
GO

DELETE FROM GeneralParameters where [key] = 'ACCOUNTING_EXPORT_MODE'
GO

INSERT INTO [dbo].[ActionItems]
           (
            [class_name]
           ,[method_name]
           )
     VALUES
           ('ClientServices',
            'SaveSolidarityGroup')
GO

DELETE FROM [dbo].[GeneralParameters]  WHERE [key]='PENDING_REPAYMENT_MODE'
GO

ALTER TABLE dbo.RepaymentEvents
DROP COLUMN voucher_number
GO

ALTER TABLE dbo.LoanDisbursmentEvents
DROP COLUMN voucher_number
GO

DECLARE @db_from NVARCHAR(MAX)
DECLARE @db_to NVARCHAR(MAX)
DECLARE @sql NVARCHAR(MAX)

SELECT @db_from = DB_NAME()
SET @db_to = @db_from + '_attachments'

IF EXISTS (SELECT * FROM sys.databases WHERE name = @db_to)
BEGIN
	SET @sql = 'DROP DATABASE ' + @db_to
	EXEC sp_executesql @sql	
END

SET @sql = 'CREATE DATABASE ' + @db_to
EXEC sp_executesql @sql

SET @sql = 'CREATE TABLE ' + @db_to + '..Pictures' +
'(
	[group] [nvarchar](50) NOT NULL,
	[id] [int] NOT NULL,
	[subid] [int] NOT NULL,
	[picture] [image] NOT NULL,
	[thumbnail] [image] NOT NULL,
	[name] [varchar](50) NOT NULL,
	[picture_id] [int] IDENTITY(1,1) NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]'
EXEC sp_executesql @sql

SET @sql = 'ALTER TABLE ' + @db_to + '..Pictures ADD  CONSTRAINT [DF_Pictures_subid]  DEFAULT ((0)) FOR [subid]'
EXEC sp_executesql @sql

SET @sql = 'SET IDENTITY_INSERT ' + @db_to + '..Pictures ON

INSERT INTO ' + @db_to + '..Pictures ([group], id, subid, picture, thumbnail, name, picture_id)
SELECT [group], id, subid, picture, thumbnail, name, picture_id
FROM ' + @db_from + '..Pictures

SET IDENTITY_INSERT ' + @db_to + '..Pictures OFF'

EXEC sp_executesql @sql
GO

DROP TABLE dbo.Pictures
GO

UPDATE  [TechnicalParameters]
SET     [value] = 'v3.7.0'
WHERE   [name] = 'VERSION'
GO