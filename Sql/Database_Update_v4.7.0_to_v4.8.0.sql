ALTER TABLE PaymentMethods ADD is_active_for_loans BIT NOT NULL DEFAULT ((0))
ALTER TABLE PaymentMethods ADD is_pending_for_loans BIT NOT NULL DEFAULT ((0))
ALTER TABLE PaymentMethods ADD is_active_for_savings BIT  NOT NULL DEFAULT ((0))
ALTER TABLE PaymentMethods ADD is_pending_for_savings BIT NOT NULL DEFAULT ((0))
ALTER TABLE PaymentMethods ADD account_id INT NULL
ALTER TABLE PaymentMethods ADD is_deleted [bit] NOT NULL DEFAULT((0))

GO


UPDATE PaymentMethods SET is_active_for_loans =1 WHERE name = 'Cash'
UPDATE PaymentMethods SET is_active_for_savings =1 WHERE name = 'Cash'
UPDATE PaymentMethods SET [is_pending_for_loans] =1 WHERE name = 'Cheque'
UPDATE PaymentMethods SET [is_active_for_loans] =1 WHERE name = 'Cheque'
UPDATE PaymentMethods SET [is_active_for_savings] =1 WHERE name = 'Cheque'
UPDATE PaymentMethods SET [is_pending_for_savings] =1 WHERE name = 'Cheque'
GO

IF  EXISTS (SELECT  name FROM dbo.sysobjects WHERE name like N'DF__PaymentMe__pendi%')
BEGIN
DECLARE @constraint_name NVARCHAR(MAX)
SELECT @constraint_name = name FROM dbo.sysobjects WHERE name like N'DF__PaymentMe__pendi%'
DECLARE @delete_command NVARCHAR(MAX)
SET @delete_command = 'ALTER TABLE [dbo].[PaymentMethods] DROP CONSTRAINT'+SPACE(1)+@constraint_name
EXEC(@delete_command)
END
GO

ALTER TABLE PaymentMethods DROP COLUMN pending
GO

IF  EXISTS (SELECT  name FROM dbo.sysobjects WHERE name like N'DF__LinkBranc__accou%')
BEGIN
DECLARE @constraint_name NVARCHAR(MAX)
SELECT @constraint_name = name FROM dbo.sysobjects WHERE name like N'DF__LinkBranc__accou%'
DECLARE @delete_command NVARCHAR(MAX)
SET @delete_command = 'ALTER TABLE [dbo].[LinkBranchesPaymentMethods] DROP CONSTRAINT'+SPACE(1)+@constraint_name
EXEC(@delete_command)
END
GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE name ='FK_LinkBranchesPaymentMethods_ChartOfAccounts')
BEGIN
ALTER TABLE LinkBranchesPaymentMethods DROP CONSTRAINT FK_LinkBranchesPaymentMethods_ChartOfAccounts
END 
GO

ALTER TABLE LinkBranchesPaymentMethods DROP COLUMN [account_id] 
GO

DECLARE @menu_item_id INT
INSERT INTO [dbo].[MenuItems]([component_name]) VALUES ('mnuPaymentMethod')
SELECT @menu_item_id=SCOPE_IDENTITY()

IF EXISTS (SELECT id FROM Roles WHERE id = 1)
	INSERT INTO [dbo].[AllowedRoleMenus] ([menu_item_id],[role_id] ,[allowed]) VALUES (@menu_item_id, 1, 'true')
IF EXISTS (SELECT id FROM Roles WHERE id = 2)
	INSERT INTO [dbo].[AllowedRoleMenus] ([menu_item_id],[role_id] ,[allowed]) VALUES (@menu_item_id, 2, 'false')
IF EXISTS (SELECT id FROM Roles WHERE id = 3)
	INSERT INTO [dbo].[AllowedRoleMenus] ([menu_item_id],[role_id] ,[allowed]) VALUES (@menu_item_id, 3, 'false')
IF EXISTS (SELECT id FROM Roles WHERE id = 4)
	INSERT INTO [dbo].[AllowedRoleMenus] ([menu_item_id],[role_id] ,[allowed]) VALUES (@menu_item_id, 4, 'true')
IF EXISTS (SELECT id FROM Roles WHERE id = 5)
	INSERT INTO [dbo].[AllowedRoleMenus] ([menu_item_id],[role_id] ,[allowed]) VALUES (@menu_item_id, 5, 'false')
GO

INSERT INTO ActionItems (class_name, method_name) VALUES('LoanServices', 'ModifyInstallmentType')
GO

INSERT INTO [dbo].[ActionItems] ([class_name],[method_name]) VALUES ('PaymentMethodServices', 'ModifyPaymentMethod')
INSERT INTO [dbo].[ActionItems] ([class_name],[method_name]) VALUES ('PaymentMethodServices', 'DeletePaymentMethod')
INSERT INTO [dbo].[ActionItems] ([class_name],[method_name]) VALUES ('PaymentMethodServices', 'AddPaymentMethod')
GO

UPDATE  [TechnicalParameters]
SET     [value] = 'v4.8.0'
WHERE   [name] = 'VERSION'
GO


