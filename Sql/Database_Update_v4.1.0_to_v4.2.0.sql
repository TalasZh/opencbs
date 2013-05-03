DECLARE @Str NVARCHAR(100)
SET @Str = ( SELECT   name
               FROM     sysobjects
               WHERE    name LIKE 'DF__ContractA__payme%'
             )
EXEC('ALTER TABLE ContractAccountingRules DROP CONSTRAINT '+ @Str)
GO

ALTER TABLE dbo.ContractAccountingRules
DROP COLUMN payment_method_id
GO

INSERT INTO GeneralParameters ([key], [value]) VALUES ('INTEREST_RATE_DECIMAL_PLACES', '2')
GO

ALTER TABLE Credit 
ALTER COLUMN interest_rate NUMERIC(16, 12) NOT NULL

ALTER TABLE Credit 
ALTER COLUMN ir_min NUMERIC(16, 12) NULL

ALTER TABLE Credit 
ALTER COLUMN ir_max NUMERIC(16, 12) NULL

ALTER TABLE Packages
ALTER COLUMN interest_rate NUMERIC(16, 12) NULL

ALTER TABLE Packages
ALTER COLUMN interest_rate_min NUMERIC(16, 12) NULL

ALTER TABLE Packages
ALTER COLUMN interest_rate_max NUMERIC(16, 12) NULL
GO

ALTER TABLE dbo.MenuItems ADD [type] int NOT NULL CONSTRAINT DF_MenuItems_type DEFAULT 0
GO

DECLARE @v sql_variant 
SET @v = N'0: Normal menu items loaded for main menu
1: Extension control menus'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'MenuItems', N'COLUMN', N'type'
GO

INSERT INTO dbo.ContractEvents        
SELECT 'LOCE' AS event_type
, ce.contract_id
, ce.event_date
, ce.user_id
, 0 AS is_deleted
, ce.entry_date
, 0 AS is_exported
, ce.comment
, ce.teller_id
, NULL AS parent_id
, NULL AS cancel_date
FROM Contracts co
INNER JOIN dbo.ContractEvents ce ON ce.contract_id = co.id
WHERE co.closed = 1 AND ce.is_deleted = 0
AND ce.id IN (SELECT MAX(id) FROM dbo.ContractEvents WHERE is_deleted = 0 GROUP BY contract_id)
GO

INSERT INTO dbo.EventTypes (event_type, description, sort_order, accounting) VALUES ('LOCE', 'Loan Close Event', 650, 0)
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_LinkBranchesPaymentMethods_PaymentMethods]') AND parent_object_id = OBJECT_ID(N'[dbo].[LinkBranchesPaymentMethods]'))
ALTER TABLE [dbo].[LinkBranchesPaymentMethods]  WITH CHECK ADD  CONSTRAINT [FK_LinkBranchesPaymentMethods_PaymentMethods] FOREIGN KEY([payment_method_id])
REFERENCES [dbo].[PaymentMethods] ([id])
GO

UPDATE dbo.GeneralParameters SET value = '1' WHERE value = 'True'
GO

UPDATE dbo.GeneralParameters SET value = '0' WHERE value = 'False'
GO

UPDATE  [TechnicalParameters]
SET     [value] = 'v4.2.0'
WHERE   [name] = 'VERSION'
GO