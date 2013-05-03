ALTER TABLE [dbo].[ContractEvents]
ADD cancel_date DATETIME NULL
GO

ALTER TABLE [dbo].[SavingEvents]
ADD cancel_date DATETIME NULL
GO

ALTER TABLE [dbo].[Tellers]
ADD branch_id INT NOT NULL DEFAULT(0)
GO

UPDATE dbo.Tellers
SET branch_id = (SELECT MIN(id) FROM dbo.Branches WHERE deleted = 0)
GO

ALTER TABLE dbo.Tellers ADD CONSTRAINT
FK_Tellers_Branches FOREIGN KEY
(
	branch_id
) 
REFERENCES dbo.Branches
(
	id
)
ON UPDATE  NO ACTION 
ON DELETE  NO ACTION 
GO


UPDATE [dbo].[ContractEvents] SET [cancel_date]=[event_date] WHERE is_deleted=1
GO

UPDATE  [dbo].[SavingEvents] SET [cancel_date]= [creation_date] WHERE deleted=1
GO

DECLARE @db_from NVARCHAR(MAX)
DECLARE @db_to NVARCHAR(MAX)
DECLARE @sql NVARCHAR(MAX)
DECLARE @table_name NVARCHAR(MAX)

SELECT @db_from = DB_NAME()
SET @db_to = @db_from + '_attachments'

SET @table_name = @db_from + '_attachments..ClientDocuments'

IF EXISTS (SELECT * FROM sys.databases WHERE name = @db_to)
BEGIN
		SET @sql = '
		IF EXISTS (SELECT * FROM ' + @db_to + '.[sys].[tables] WHERE name= ''ClientDocuments'')
		SELECT @exist = COUNT(*) FROM ' + @db_to + '.INFORMATION_SCHEMA.[COLUMNS] WHERE TABLE_NAME = ''ClientDocuments'' AND COLUMN_NAME = ''object_id'''
		DECLARE @exist INT
		EXEC sp_executesql @sql, N'@exist INT OUTPUT', @exist = @exist OUTPUT
		if(@exist = 0)
		BEGIN
			SET @sql = 
			'ALTER TABLE ' + @table_name + ' ADD [object_id] INT NULL ' +
			'ALTER TABLE ' + @table_name + ' ADD [object_type] INT NULL ' 
			EXEC sp_executesql @sql
			SET @sql = 
			'UPDATE		 ' + @table_name + ' SET [object_id] = client_id, object_type = 0 ' + 
			'ALTER TABLE ' + @table_name + ' DROP COLUMN client_id ' +
			'ALTER TABLE ' + @table_name + ' ALTER COLUMN [object_id] INT NOT NULL '+
			'ALTER TABLE ' + @table_name + ' ALTER COLUMN [object_type] INT NOT NULL '
			EXEC sp_executesql @sql
		END
END

GO

CREATE TABLE LinkBranchesPaymentMethods
(
	id INT IDENTITY(1, 1) NOT NULL,
	branch_id INT NOT NULL,
	payment_method_id INT NOT NULL,
	deleted BIT NOT NULL DEFAULT ((0)),
	date DATETIME NULL DEFAULT (getdate()),
)
GO

ALTER TABLE dbo.ManualAccountingMovements
ADD branch_id INT NULL
GO

UPDATE dbo.ManualAccountingMovements 
SET branch_id = (SELECT MIN(id) FROM dbo.Branches WHERE deleted = 0)

ALTER TABLE dbo.ManualAccountingMovements
ALTER COLUMN branch_id INT NOT NULL
GO

ALTER TABLE dbo.SavingsAccountingMovements
ADD branch_id INT NULL
GO

UPDATE dbo.SavingsAccountingMovements SET branch_id = (SELECT MIN(id) FROM dbo.Branches WHERE deleted = 0)
GO

ALTER TABLE dbo.SavingsAccountingMovements
ALTER COLUMN branch_id INT NOT NULL
GO

ALTER TABLE dbo.LoanAccountingMovements
ADD branch_id INT NULL
GO

UPDATE dbo.LoanAccountingMovements SET branch_id = (SELECT MIN(id) FROM dbo.Branches WHERE deleted = 0)
GO

ALTER TABLE dbo.LoanAccountingMovements
ALTER COLUMN branch_id INT NOT NULL
GO

IF EXISTS (
       SELECT 1
       FROM   sysobjects
       WHERE  xtype = 'u'
              AND NAME = 'Reports'
   )
    DROP TABLE Reports
GO

IF(NOT EXISTS(SELECT * FROM GeneralParameters WHERE [key] = 'REPORT_TIMEOUT'))
	INSERT INTO GeneralParameters ([key],[value]) VALUES ('REPORT_TIMEOUT',	'300')
GO

UPDATE t2 SET t2.[active] = 0 FROM Tiers t2
       LEFT JOIN (
                SELECT t.id tiers_id
                FROM   Installments i
                       INNER JOIN Credit cr
                            ON  cr.id = i.contract_id
                            AND cr.disbursed = 1
                            AND cr.written_off = 0
                       INNER JOIN Contracts c
                            ON  c.id = cr.id
                       INNER JOIN Projects p
                            ON  c.project_id = p.id
                       INNER JOIN Tiers t
                            ON  p.tiers_id = t.id
                WHERE  i.capital_repayment > i.paid_capital + 0.02
                GROUP BY
                       t.id
            ) al
            ON  t2.id = al.tiers_id
WHERE t2.client_type_code = 'I' AND t2.[active] = 1 AND al.tiers_id IS NULL

GO

UPDATE  [TechnicalParameters]
SET     [value] = 'v3.9.0'
WHERE   [name] = 'VERSION'

GO