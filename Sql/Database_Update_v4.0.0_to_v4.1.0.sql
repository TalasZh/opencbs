INSERT INTO [dbo].[ActionItems] ([class_name],[method_name]) VALUES ('ClientServices', 'SaveCorporate')
GO

INSERT INTO [dbo].[ActionItems] ([class_name],[method_name]) VALUES ('ClientServices', 'SaveNonSolidarityGroup')
GO

ALTER TABLE dbo.AdvancedFieldsValues ALTER COLUMN [value] NVARCHAR(300) 
GO

ALTER TABLE dbo.LinkBranchesPaymentMethods
ADD account_id INT NOT NULL DEFAULT(1)
GO

UPDATE dbo.LinkBranchesPaymentMethods
SET account_id = (SELECT MIN(id) FROM ChartOfAccounts)
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_LinkBranchesPaymentMethods_ChartOfAccounts]') AND parent_object_id = OBJECT_ID(N'[dbo].[LinkBranchesPaymentMethods]'))
ALTER TABLE [dbo].[LinkBranchesPaymentMethods]  WITH CHECK ADD  CONSTRAINT [FK_LinkBranchesPaymentMethods_ChartOfAccounts] FOREIGN KEY([account_id])
REFERENCES [dbo].[ChartOfAccounts] ([id])
GO


ALTER TABLE dbo.AccountingClosure
ADD is_deleted BIT DEFAULT(0)
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UsersTellers]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[UsersTellers](
	[user_id] [int] NOT NULL,
	[teller_id] [int] NOT NULL
) ON [PRIMARY]
END
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UsersTellers_Users]') AND parent_object_id = OBJECT_ID(N'[dbo].[UsersTellers]'))
ALTER TABLE [dbo].[UsersTellers]  WITH CHECK ADD  CONSTRAINT [FK_UsersTellers_Users] FOREIGN KEY([user_id])
REFERENCES [dbo].[Users] ([id])
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UsersTellers_Users]') AND parent_object_id = OBJECT_ID(N'[dbo].[UsersTellers]'))
ALTER TABLE [dbo].[UsersTellers] CHECK CONSTRAINT [FK_UsersTellers_Users]
GO

SET IDENTITY_INSERT [MenuItems] ON
INSERT INTO [dbo].[MenuItems]([id], [component_name]) VALUES (73, 'mnuNewClosure')
SET IDENTITY_INSERT [MenuItems] OFF
GO

INSERT INTO [dbo].[AllowedRoleMenus] ([menu_item_id],[role_id] ,[allowed]) VALUES (73, 4, 'true')
GO

INSERT INTO [dbo].[AllowedRoleMenus] ([menu_item_id],[role_id] ,[allowed]) 
SELECT 73, id, 'false'
FROM dbo.Roles
WHERE id <> 4
GO

EXECUTE sp_rename N'dbo.LoanMonitoring.contract_id', N'object_id', 'COLUMN' 
GO

ALTER TABLE dbo.LoanMonitoring ADD
	type bit NOT NULL CONSTRAINT DF_LoanMonitoring_type DEFAULT 1
GO

DECLARE @v sql_variant 
SET @v = N'0: Client 1:Loan'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'LoanMonitoring', N'COLUMN', N'type'
GO

EXECUTE sp_rename N'LoanMonitoring', N'Monitoring'
GO 

DELETE FROM [dbo].[MenuItems]
WHERE id=61 AND [component_name]= 'tellerManagementToolStripMenuItem'

UPDATE  [TechnicalParameters]
SET     [value] = 'v4.1.0'
WHERE   [name] = 'VERSION'
GO