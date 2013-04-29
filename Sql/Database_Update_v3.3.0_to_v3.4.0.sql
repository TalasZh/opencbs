ALTER TABLE [dbo].[VillagesPersons]
ADD is_leader [bit] NOT NULL DEFAULT ((0)),
	currently_in [bit] NOT NULL DEFAULT ((0))
GO

UPDATE dbo.VillagesPersons
SET currently_in = 1
WHERE left_date IS NULL
GO

ALTER TABLE [dbo].[VillagesPersons] ADD [id] [int] IDENTITY(1,1) NOT NULL
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ExportAcountingTransactions]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[ExportAcountingTransactions]
GO

DELETE FROM dbo.SavingEvents
WHERE (amount IS NULL OR 0 = amount) 
    AND (fees IS NULL OR 0 = fees)
    AND code IN ('SIAE', 'SIPE', 'SVAE', 'SMFE')
GO

ALTER TABLE dbo.LoanAccountingMovements
ALTER COLUMN rule_id INT NULL
GO

ALTER TABLE dbo.SavingsAccountingMovements
ALTER COLUMN rule_id INT NULL
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[VillagesAttendance]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[VillagesAttendance](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[village_id] [int] NOT NULL,
	[person_id] [int] NOT NULL,
	[date] [datetime] NOT NULL,
	[attended] [bit] NOT NULL DEFAULT(0),
	[comment] NVARCHAR(1000) NULL,
CONSTRAINT [PK_VillagesAttendance] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_VillagesAttendance_Villages]') AND parent_object_id = OBJECT_ID(N'[dbo].[VillagesAttendance]'))
ALTER TABLE [dbo].[VillagesAttendance]  WITH CHECK ADD  CONSTRAINT [FK_VillagesAttendance_Villages] FOREIGN KEY([village_id])
REFERENCES [dbo].[Villages] ([id])
GO
ALTER TABLE [dbo].[VillagesAttendance] CHECK CONSTRAINT [FK_VillagesAttendance_Villages]
GO

INSERT INTO [GeneralParameters]([key], [value]) VALUES('GROUP_MAX_MEMBERS',10)
INSERT INTO [GeneralParameters]([key], [value]) VALUES('NSG_MIN_MEMBERS',0)
INSERT INTO [GeneralParameters]([key], [value]) VALUES('NSG_MAX_MEMBERS',10)
GO

ALTER TABLE [dbo].[Contracts]
ADD is_for_nsg [bit] NOT NULL DEFAULT ((0))	
GO

UPDATE  Contracts
SET     [status] = 5
WHERE   contracts.id IN (
        SELECT  c.id
        FROM    Contracts c
                INNER JOIN ContractEvents ce ON ce.contract_id = c.id
                INNER JOIN dbo.LoanDisbursmentEvents ld ON ce.id = ld.id
        WHERE   c.closed = 0
                AND c.[status] <> 5
                AND ce.is_deleted = 0 )
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_AccountingRules_ChartOfAccounts]') AND parent_object_id = OBJECT_ID(N'[dbo].[AccountingRules]'))
ALTER TABLE [dbo].[AccountingRules]  WITH CHECK ADD  CONSTRAINT [FK_AccountingRules_ChartOfAccounts] FOREIGN KEY([debit_account_number_id])
REFERENCES [dbo].[ChartOfAccounts] ([id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_AccountingRules_ChartOfAccounts]') AND parent_object_id = OBJECT_ID(N'[dbo].[AccountingRules]'))
ALTER TABLE [dbo].[AccountingRules] CHECK CONSTRAINT [FK_AccountingRules_ChartOfAccounts]
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_AccountingRules_ChartOfAccounts1]') AND parent_object_id = OBJECT_ID(N'[dbo].[AccountingRules]'))
ALTER TABLE [dbo].[AccountingRules]  WITH CHECK ADD  CONSTRAINT [FK_AccountingRules_ChartOfAccounts1] FOREIGN KEY([credit_account_number_id])
REFERENCES [dbo].[ChartOfAccounts] ([id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_AccountingRules_ChartOfAccounts1]') AND parent_object_id = OBJECT_ID(N'[dbo].[AccountingRules]'))
ALTER TABLE [dbo].[AccountingRules] CHECK CONSTRAINT [FK_AccountingRules_ChartOfAccounts1]
GO

-- Customizable fields BLOCK - BEGIN
SET IDENTITY_INSERT [MenuItems] ON
INSERT INTO [dbo].[MenuItems]([id], [component_name]) VALUES (70, 'advancedCustomizableFieldsToolStripMenuItem')
SET IDENTITY_INSERT [MenuItems] OFF
GO

-- AdvancedFieldsEntities
CREATE TABLE [dbo].[AdvancedFieldsEntities](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_AdvancedFieldsEntities] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

-- AdvancedFieldsTypes
CREATE TABLE [dbo].[AdvancedFieldsTypes](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_AdvancedFieldsTypes] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

-- AdvancedFields
CREATE TABLE [dbo].[AdvancedFields](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[entity_id] [int] NOT NULL,
	[type_id] [int] NOT NULL,
	[name] [nvarchar](100) NOT NULL,
	[desc] [nvarchar](1000) NOT NULL,
	[is_mandatory] [bit] NOT NULL,
	[is_unique] [bit] NOT NULL,
 CONSTRAINT [PK_AdvancedFields] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[AdvancedFields]  WITH CHECK ADD CONSTRAINT [FK_AdvancedFields_AdvancedFieldsEntities] FOREIGN KEY([entity_id])
REFERENCES [dbo].[AdvancedFieldsEntities] ([id])
GO

ALTER TABLE [dbo].[AdvancedFields] CHECK CONSTRAINT [FK_AdvancedFields_AdvancedFieldsEntities]
GO

ALTER TABLE [dbo].[AdvancedFields]  WITH CHECK ADD  CONSTRAINT [FK_AdvancedFields_AdvancedFieldsTypes] FOREIGN KEY([type_id])
REFERENCES [dbo].[AdvancedFieldsTypes] ([id])
GO

ALTER TABLE [dbo].[AdvancedFields] CHECK CONSTRAINT [FK_AdvancedFields_AdvancedFieldsTypes]
GO

-- AdvancedFieldsCollections
CREATE TABLE [dbo].[AdvancedFieldsCollections](
	[field_id] [int] NOT NULL,
	[value] [nvarchar](100) NOT NULL
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[AdvancedFieldsCollections]  WITH CHECK ADD  CONSTRAINT [FK_AdvancedFieldsCollections_AdvancedFields] FOREIGN KEY([field_id])
REFERENCES [dbo].[AdvancedFields] ([id])
GO

ALTER TABLE [dbo].[AdvancedFieldsCollections] CHECK CONSTRAINT [FK_AdvancedFieldsCollections_AdvancedFields]
GO

-- AdvancedFieldsLinkEntities
CREATE TABLE [dbo].[AdvancedFieldsLinkEntities](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[link_type] [char](1) NOT NULL DEFAULT ('-'),
	[link_id] [int] NOT NULL,
 CONSTRAINT [PK_AdvancedFieldsLinkEntities] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

-- AdvancedFieldsValues
CREATE TABLE [dbo].[AdvancedFieldsValues](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[entity_field_id] [int] NOT NULL,
	[field_id] [int] NOT NULL,
	[value] [nvarchar](100) NULL,
 CONSTRAINT [PK_AdvancedFieldsValues] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[AdvancedFieldsValues]  WITH CHECK ADD  CONSTRAINT [FK_AdvancedFieldsValues_AdvancedFields] FOREIGN KEY([field_id])
REFERENCES [dbo].[AdvancedFields] ([id])
GO

ALTER TABLE [dbo].[AdvancedFieldsValues] CHECK CONSTRAINT [FK_AdvancedFieldsValues_AdvancedFields]
GO

ALTER TABLE [dbo].[AdvancedFieldsValues]  WITH CHECK ADD  CONSTRAINT [FK_AdvancedFieldsValues_AdvancedFieldsLinkEntities] FOREIGN KEY([entity_field_id])
REFERENCES [dbo].[AdvancedFieldsLinkEntities] ([id])
GO

ALTER TABLE [dbo].[AdvancedFieldsValues] CHECK CONSTRAINT [FK_AdvancedFieldsValues_AdvancedFieldsLinkEntities]
GO

-- Insert
INSERT INTO [dbo].[AdvancedFieldsEntities] ([name]) VALUES ('Individual')
INSERT INTO [dbo].[AdvancedFieldsEntities] ([name]) VALUES ('SG')
INSERT INTO [dbo].[AdvancedFieldsEntities] ([name]) VALUES ('NSG')
INSERT INTO [dbo].[AdvancedFieldsEntities] ([name]) VALUES ('Corporate')
INSERT INTO [dbo].[AdvancedFieldsEntities] ([name]) VALUES ('Loan')
INSERT INTO [dbo].[AdvancedFieldsEntities] ([name]) VALUES ('Savings')
GO

INSERT INTO [dbo].[AdvancedFieldsTypes] ([name]) VALUES ('Boolean')
INSERT INTO [dbo].[AdvancedFieldsTypes] ([name]) VALUES ('Number')
INSERT INTO [dbo].[AdvancedFieldsTypes] ([name]) VALUES ('String')
INSERT INTO [dbo].[AdvancedFieldsTypes] ([name]) VALUES ('Date')
INSERT INTO [dbo].[AdvancedFieldsTypes] ([name]) VALUES ('Collection')
GO

INSERT INTO dbo.AdvancedFields ( entity_id, type_id, name, [desc], is_mandatory, is_unique )
VALUES  ( 5, 5, 'Type of clients', '', 0, 0 )
GO

INSERT INTO dbo.AdvancedFieldsCollections ( field_id, value ) VALUES  ( 1, 'Urban' )
INSERT INTO dbo.AdvancedFieldsCollections ( field_id, value ) VALUES  ( 1, 'Rural' )
GO

-- Migrating 'rural' field for contracts as advanced field
DECLARE AdvancedFieldsCursor CURSOR FOR
SELECT id AS [contract_id], rural AS [is_rural] FROM dbo.Contracts
DECLARE @contract_id INT, @is_rural BIT
OPEN AdvancedFieldsCursor
FETCH NEXT FROM AdvancedFieldsCursor INTO
@contract_id, @is_rural
WHILE @@FETCH_STATUS=0
BEGIN
	DECLARE @entity_field_id INT
	INSERT INTO dbo.AdvancedFieldsLinkEntities(link_type, link_id) VALUES('L', @contract_id)
	SET @entity_field_id = @@IDENTITY
	
	INSERT INTO dbo.AdvancedFieldsValues(entity_field_id, field_id, value) VALUES(@entity_field_id, 1, @is_rural)
	FETCH NEXT FROM AdvancedFieldsCursor INTO @contract_id, @is_rural
END
CLOSE AdvancedFieldsCursor
DEALLOCATE AdvancedFieldsCursor
GO

-- removing rural
ALTER TABLE dbo.Contracts
DROP COLUMN rural

-- Customizable fields BLOCK - END

-- Fill in missing activity history entries
INSERT INTO dbo.EconomicActivityLoanHistory (contract_id, person_id, group_id, economic_activity_id, deleted)
SELECT c.contract_id, c.person_id, c.group_id, p.activity_id, 0
FROM 
(
    SELECT c.id contract_id, t.id person_id, NULL group_id
    FROM dbo.Contracts c
    INNER JOIN dbo.Projects j ON j.id = c.project_id
    LEFT JOIN dbo.Tiers t ON t.id = j.tiers_id
    WHERE t.client_type_code <> 'G'

    UNION ALL
    SELECT contract_id, person_id, group_id
    FROM dbo.LoanShareAmounts
) c
LEFT JOIN dbo.EconomicActivityLoanHistory ea ON ea.contract_id = c.contract_id AND ea.person_id = c.person_id
INNER JOIN dbo.Persons p ON p.id = c.person_id
WHERE ea.economic_activity_id IS NULL AND c.contract_id > 0 AND NOT p.activity_id IS NULL
GO

UPDATE dbo.EntryFees
SET value = 0
WHERE value IS NULL
 AND [max] IS NULL
 AND [min] IS NULL
GO

INSERT INTO [dbo].[ActionItems](class_name, method_name) VALUES ('LoanServices', 'AddTranche')
GO

UPDATE [TechnicalParameters] SET [value] = 'v3.4.0' WHERE [name] = 'VERSION'
GO
