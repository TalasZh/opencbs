ALTER TABLE CreditEntryFees
DROP CONSTRAINT FK_CreditEntryFees_EntryFees
GO

DECLARE @CreditStr NVARCHAR(100)
SET @CreditStr = ( SELECT   name
               FROM     sysobjects
               WHERE    name LIKE '%_Credit__fake_%'
             )
EXEC('ALTER TABLE dbo.Credit DROP CONSTRAINT '+ @CreditStr)
GO

ALTER TABLE Credit
DROP COLUMN fake
GO

DECLARE @PackageStr NVARCHAR(100)
SET @PackageStr = ( SELECT   name
               FROM     sysobjects
               WHERE    name LIKE '%_Packag__fake_%'
             )
EXEC('ALTER TABLE dbo.Packages DROP CONSTRAINT '+ @PackageStr)
GO

ALTER TABLE Packages
DROP COLUMN fake
GO

ALTER TABLE Users
ADD phone NVARCHAR(50) NULL
GO

ALTER TABLE SavingContracts
ADD nsg_id INT NULL
GO

ALTER TABLE dbo.LoanAccountingMovements
ADD closure_id INT NULL
GO

ALTER TABLE dbo.ManualAccountingMovements
ADD closure_id INT NULL
GO

ALTER TABLE dbo.SavingsAccountingMovements
ADD closure_id INT NULL
GO

CREATE TABLE [AccountingClosure](
	[user_id] [int] NOT NULL,
	[date_of_closure] [datetime] NOT NULL,
	[count_of_transactions] [int] NOT NULL,
	[id] [int] IDENTITY(1,1) NOT NULL,
) ON [PRIMARY]
GO

INSERT INTO MenuItems(component_name) VALUES(N'mnuExtensionManager')
GO

INSERT INTO AllowedRoleMenus
SELECT mi.id, r.id, 0
FROM   MenuItems mi, Roles r
WHERE  mi.component_name = 'mnuExtensionManager'
       AND r.code <> 'SUPER' AND
       NOT EXISTS (
                      SELECT 1
                      FROM   AllowedRoleMenus arm
                      WHERE  arm.menu_item_id = mi.id AND arm.role_id = r.id
       )       
GO

INSERT INTO AllowedRoleMenus
SELECT mi.id, r.id, 1
FROM   MenuItems mi, Roles r
WHERE  mi.component_name = 'mnuExtensionManager'
       AND r.code = 'SUPER' AND
       NOT EXISTS (
                      SELECT 1
                      FROM   AllowedRoleMenus arm
                      WHERE  arm.menu_item_id = mi.id AND arm.role_id = r.id
       )
GO

UPDATE  [TechnicalParameters]
SET     [value] = 'v4.0.0'
WHERE   [name] = 'VERSION'
GO