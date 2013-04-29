INSERT INTO [dbo].[ActionItems] ([class_name],[method_name]) VALUES ('ClientServices','AddExistingMember')
INSERT INTO [dbo].[ActionItems] ([class_name],[method_name]) VALUES ('ClientServices','RemoveMember')
INSERT INTO [dbo].[ActionItems] ([class_name],[method_name]) VALUES ('LoanServices', 'PerformBackDateOperations')
INSERT INTO [dbo].[ActionItems] ([class_name],[method_name]) VALUES ('LoanServices', 'PerformFutureDateOperations')
INSERT INTO [dbo].[ActionItems] ([class_name],[method_name]) VALUES ('SavingServices', 'PerformBackDateOperations')
INSERT INTO [dbo].[ActionItems] ([class_name],[method_name]) VALUES ('SavingServices', 'PerformFutureDateOperations')
GO

ALTER TABLE [dbo].[Packages]
ADD 
insurance_min DECIMAL(18,2) NOT NULL DEFAULT ((0)),
insurance_max DECIMAL(18,2) NOT NULL DEFAULT((0))
GO

ALTER TABLE [dbo].[Credit]
ADD insurance DECIMAL(18,2) NOT NULL DEFAULT((0))
GO

INSERT INTO dbo.EventTypes (event_type, description, sort_order, accounting) VALUES ('UOTE', 'User open teller', 600, 0)
INSERT INTO dbo.EventTypes (event_type, description, sort_order, accounting) VALUES ('UCTE', 'User close teller', 610, 0)

CREATE TABLE [dbo].[CreditInsuranceEvents](
	[id] [int] NOT NULL,
	[commission] [decimal](18, 2) NOT NULL,
	[principal] [decimal](18, 2) NOT NULL
) ON [PRIMARY]

CREATE TABLE [dbo].[Rep_Par_Analysis_Data](
	[id] [int] NOT NULL,
	[branch_name] [nvarchar](50) NULL,
	[load_date] [datetime] NULL,
	[break_down] [nvarchar](150) NULL,
	[break_down_type] [varchar](20) NULL,
	[olb] [money] NULL,
	[par] [money] NULL,
	[contracts] [int] NULL,
	[clients] [int] NULL,
	[all_contracts] [int] NULL,
	[all_clients] [int] NULL,
	[par_30] [money] NULL,
	[contracts_30] [int] NULL,
	[clients_30] [int] NULL,
	[par_1_30] [money] NULL,
	[contracts_1_30] [int] NULL,
	[clients_1_30] [int] NULL,
	[par_31_60] [money] NULL,
	[contracts_31_60] [int] NULL,
	[clients_31_60] [int] NULL,
	[par_61_90] [money] NULL,
	[contracts_61_90] [int] NULL,
	[clients_61_90] [int] NULL,
	[par_91_180] [money] NULL,
	[contracts_91_180] [int] NULL,
	[clients_91_180] [int] NULL,
	[par_181_365] [money] NULL,
	[contracts_181_365] [int] NULL,
	[clients_181_365] [int] NULL,
	[par_365] [money] NULL,
	[contracts_365] [int] NULL,
	[clients_365] [int] NULL
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[Rep_Active_Loans_Data](
	[id] [int] NOT NULL,
	[branch_name] [nvarchar](50) NULL,
	[load_date] [datetime] NULL,
	[break_down] [nvarchar](150) NULL,
	[break_down_type] [nvarchar](20) NULL,
	[contracts] [int] NULL,
	[individual] [int] NULL,
	[group] [int] NULL,
	[corporate] [int] NULL,
	[clients] [int] NULL,
	[in_groups] [int] NULL,
	[projects] [int] NULL,
	[olb] [money] NULL
) ON [PRIMARY]
GO

INSERT INTO dbo.TechnicalParameters
VALUES ('last_id', 'false')
GO

DROP TABLE dbo.MenuItems
GO

CREATE TABLE [dbo].[MenuItems](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[component_name] [nvarchar](100) NOT NULL UNIQUE
) ON [PRIMARY]
GO

SET IDENTITY_INSERT [MenuItems] ON
INSERT INTO [dbo].[MenuItems]([id], [component_name]) VALUES (1, 'mnuClients')
INSERT INTO [dbo].[MenuItems]([id], [component_name]) VALUES (2, 'mnuNewClient')
INSERT INTO [dbo].[MenuItems]([id], [component_name]) VALUES (3, 'mnuNewPerson')
INSERT INTO [dbo].[MenuItems]([id], [component_name]) VALUES (4, 'mnuNewGroup')
INSERT INTO [dbo].[MenuItems]([id], [component_name]) VALUES (5, 'mnuNewVillage')
INSERT INTO [dbo].[MenuItems]([id], [component_name]) VALUES (6, 'newCorporateToolStripMenuItem')
INSERT INTO [dbo].[MenuItems]([id], [component_name]) VALUES (7, 'mnuSearchClient')
INSERT INTO [dbo].[MenuItems]([id], [component_name]) VALUES (8, 'mnuContracts')
INSERT INTO [dbo].[MenuItems]([id], [component_name]) VALUES (9, 'mnuSearchContract')
INSERT INTO [dbo].[MenuItems]([id], [component_name]) VALUES (10, 'reasignToolStripMenuItem')
INSERT INTO [dbo].[MenuItems]([id], [component_name]) VALUES (11, 'mnuAccounting')
INSERT INTO [dbo].[MenuItems]([id], [component_name]) VALUES (12, 'mnuChartOfAccounts')
INSERT INTO [dbo].[MenuItems]([id], [component_name]) VALUES (13, 'accountingRulesToolStripMenuItem')
INSERT INTO [dbo].[MenuItems]([id], [component_name]) VALUES (14, 'trialBalanceToolStripMenuItem')
INSERT INTO [dbo].[MenuItems]([id], [component_name]) VALUES (15, 'toolStripMenuItemAccountView')
INSERT INTO [dbo].[MenuItems]([id], [component_name]) VALUES (16, 'manualEntriesToolStripMenuItem')
INSERT INTO [dbo].[MenuItems]([id], [component_name]) VALUES (17, 'standardToolStripMenuItem')
INSERT INTO [dbo].[MenuItems]([id], [component_name]) VALUES (18, 'menuItemExportTransaction')
INSERT INTO [dbo].[MenuItems]([id], [component_name]) VALUES (19, 'mnuMonthlyClosure')
INSERT INTO [dbo].[MenuItems]([id], [component_name]) VALUES (20, 'creditClosureToolStripMenuItem')
INSERT INTO [dbo].[MenuItems]([id], [component_name]) VALUES (21, 'savingClosureToolStripMenuItem')
INSERT INTO [dbo].[MenuItems]([id], [component_name]) VALUES (22, 'exportImportCustomizableToolStripMenuItem')
INSERT INTO [dbo].[MenuItems]([id], [component_name]) VALUES (23, 'mnuConfiguration')
INSERT INTO [dbo].[MenuItems]([id], [component_name]) VALUES (24, 'menuItemAddUser')
INSERT INTO [dbo].[MenuItems]([id], [component_name]) VALUES (25, 'rolesToolStripMenuItem')
INSERT INTO [dbo].[MenuItems]([id], [component_name]) VALUES (26, 'tellersToolStripMenuItem')
INSERT INTO [dbo].[MenuItems]([id], [component_name]) VALUES (27, 'branchesToolStripMenuItem')
INSERT INTO [dbo].[MenuItems]([id], [component_name]) VALUES (28, 'changePasswordToolStripMenuItem')
INSERT INTO [dbo].[MenuItems]([id], [component_name]) VALUES (29, 'languagesToolStripMenuItem')
INSERT INTO [dbo].[MenuItems]([id], [component_name]) VALUES (30, 'frenchToolStripMenuItem')
INSERT INTO [dbo].[MenuItems]([id], [component_name]) VALUES (31, 'englishToolStripMenuItem')
INSERT INTO [dbo].[MenuItems]([id], [component_name]) VALUES (32, 'russianToolStripMenuItem')
INSERT INTO [dbo].[MenuItems]([id], [component_name]) VALUES (33, 'kyrgyzToolStripMenuItem')
INSERT INTO [dbo].[MenuItems]([id], [component_name]) VALUES (34, 'spanishToolStripMenuItem')
INSERT INTO [dbo].[MenuItems]([id], [component_name]) VALUES (35, 'portugueseToolStripMenuItem')
INSERT INTO [dbo].[MenuItems]([id], [component_name]) VALUES (36, 'mnuPackages')
INSERT INTO [dbo].[MenuItems]([id], [component_name]) VALUES (37, 'savingProductsToolStripMenuItem')
INSERT INTO [dbo].[MenuItems]([id], [component_name]) VALUES (38, 'menuItemCollateralProducts')
INSERT INTO [dbo].[MenuItems]([id], [component_name]) VALUES (39, 'guaranteeProductsToolStripMenuItem')
INSERT INTO [dbo].[MenuItems]([id], [component_name]) VALUES (40, 'toolStripMenuItemFundingLines')
INSERT INTO [dbo].[MenuItems]([id], [component_name]) VALUES (41, 'mnuDomainOfApplication')
INSERT INTO [dbo].[MenuItems]([id], [component_name]) VALUES (42, 'menuItemLocations')
INSERT INTO [dbo].[MenuItems]([id], [component_name]) VALUES (43, 'toolStripMenuItemInstallmentTypes')
INSERT INTO [dbo].[MenuItems]([id], [component_name]) VALUES (44, 'miContractCode')
INSERT INTO [dbo].[MenuItems]([id], [component_name]) VALUES (45, 'menuItemExchangeRate')
INSERT INTO [dbo].[MenuItems]([id], [component_name]) VALUES (46, 'currenciesToolStripMenuItem')
INSERT INTO [dbo].[MenuItems]([id], [component_name]) VALUES (47, 'menuItemApplicationDate')
INSERT INTO [dbo].[MenuItems]([id], [component_name]) VALUES (48, 'menuItemSetting')
INSERT INTO [dbo].[MenuItems]([id], [component_name]) VALUES (49, 'menuItemAdvancedSettings')
INSERT INTO [dbo].[MenuItems]([id], [component_name]) VALUES (50, 'customizableFieldsToolStripMenuItem')
INSERT INTO [dbo].[MenuItems]([id], [component_name]) VALUES (51, 'customizableExportImportToolStripMenuItem')
INSERT INTO [dbo].[MenuItems]([id], [component_name]) VALUES (52, 'createCustomizableExportFileToolStripMenuItem')
INSERT INTO [dbo].[MenuItems]([id], [component_name]) VALUES (53, 'createCustomizableImportFileToolStripMenuItem')
INSERT INTO [dbo].[MenuItems]([id], [component_name]) VALUES (54, 'openExistingCustomizableFileToolStripMenuItem')
INSERT INTO [dbo].[MenuItems]([id], [component_name]) VALUES (55, 'mView')
INSERT INTO [dbo].[MenuItems]([id], [component_name]) VALUES (56, 'miAuditTrail')
INSERT INTO [dbo].[MenuItems]([id], [component_name]) VALUES (57, 'miReports')
INSERT INTO [dbo].[MenuItems]([id], [component_name]) VALUES (58, 'mnuDatamanagement')
INSERT INTO [dbo].[MenuItems]([id], [component_name]) VALUES (59, 'menuItemDatabaseControlPanel')
INSERT INTO [dbo].[MenuItems]([id], [component_name]) VALUES (60, 'menuItemDatabaseMaintenance')
INSERT INTO [dbo].[MenuItems]([id], [component_name]) VALUES (61, 'tellerManagementToolStripMenuItem')
INSERT INTO [dbo].[MenuItems]([id], [component_name]) VALUES (62, 'closeTellerToolStripMenuItem')
INSERT INTO [dbo].[MenuItems]([id], [component_name]) VALUES (63, 'mnuWindow')
INSERT INTO [dbo].[MenuItems]([id], [component_name]) VALUES (64, 'mnuHelp')
INSERT INTO [dbo].[MenuItems]([id], [component_name]) VALUES (65, 'menuItemAboutOctopus')
INSERT INTO [dbo].[MenuItems]([id], [component_name]) VALUES (66, 'octopusForumToolStripMenuItem')
INSERT INTO [dbo].[MenuItems]([id], [component_name]) VALUES (67, 'questionnaireToolStripMenuItem')
INSERT INTO [dbo].[MenuItems]([id], [component_name]) VALUES (68, 'userGuideToolStripMenuItem')
INSERT INTO [dbo].[MenuItems]([id], [component_name]) VALUES (69, 'wIKIHelpToolStripMenuItem')
SET IDENTITY_INSERT [MenuItems] OFF
GO

TRUNCATE TABLE AllowedRoleMenus
GO

IF EXISTS(SELECT id FROM dbo.Roles WHERE id = 2)
BEGIN
  INSERT INTO [dbo].[AllowedRoleMenus] ([menu_item_id],[role_id] ,[allowed]) VALUES (2, 2, 'false')
  INSERT INTO [dbo].[AllowedRoleMenus] ([menu_item_id],[role_id] ,[allowed]) VALUES (10, 2, 'false')
  INSERT INTO [dbo].[AllowedRoleMenus] ([menu_item_id],[role_id] ,[allowed]) VALUES (15, 2, 'false')
  INSERT INTO [dbo].[AllowedRoleMenus] ([menu_item_id],[role_id] ,[allowed]) VALUES (18, 2, 'false')
  INSERT INTO [dbo].[AllowedRoleMenus] ([menu_item_id],[role_id] ,[allowed]) VALUES (19, 2, 'false')
  INSERT INTO [dbo].[AllowedRoleMenus] ([menu_item_id],[role_id] ,[allowed]) VALUES (21, 2, 'false')
  INSERT INTO [dbo].[AllowedRoleMenus] ([menu_item_id],[role_id] ,[allowed]) VALUES (24, 2, 'false')
  INSERT INTO [dbo].[AllowedRoleMenus] ([menu_item_id],[role_id] ,[allowed]) VALUES (36, 2, 'false')
  INSERT INTO [dbo].[AllowedRoleMenus] ([menu_item_id],[role_id] ,[allowed]) VALUES (37, 2, 'false')
  INSERT INTO [dbo].[AllowedRoleMenus] ([menu_item_id],[role_id] ,[allowed]) VALUES (39, 2, 'false')
  INSERT INTO [dbo].[AllowedRoleMenus] ([menu_item_id],[role_id] ,[allowed]) VALUES (41, 2, 'false')
  INSERT INTO [dbo].[AllowedRoleMenus] ([menu_item_id],[role_id] ,[allowed]) VALUES (42, 2, 'false')
  INSERT INTO [dbo].[AllowedRoleMenus] ([menu_item_id],[role_id] ,[allowed]) VALUES (43, 2, 'false')
  INSERT INTO [dbo].[AllowedRoleMenus] ([menu_item_id],[role_id] ,[allowed]) VALUES (44, 2, 'false')
  INSERT INTO [dbo].[AllowedRoleMenus] ([menu_item_id],[role_id] ,[allowed]) VALUES (45, 2, 'false')
  INSERT INTO [dbo].[AllowedRoleMenus] ([menu_item_id],[role_id] ,[allowed]) VALUES (46, 2, 'false')
  INSERT INTO [dbo].[AllowedRoleMenus] ([menu_item_id],[role_id] ,[allowed]) VALUES (47, 2, 'false')
  INSERT INTO [dbo].[AllowedRoleMenus] ([menu_item_id],[role_id] ,[allowed]) VALUES (49, 2, 'false')
  INSERT INTO [dbo].[AllowedRoleMenus] ([menu_item_id],[role_id] ,[allowed]) VALUES (50, 2, 'false')
  INSERT INTO [dbo].[AllowedRoleMenus] ([menu_item_id],[role_id] ,[allowed]) VALUES (56, 2, 'false')
END
GO

IF EXISTS(SELECT id FROM dbo.Roles WHERE id = 3)
BEGIN
  INSERT INTO [dbo].[AllowedRoleMenus] ([menu_item_id],[role_id] ,[allowed]) VALUES (2, 3, 'false')
  INSERT INTO [dbo].[AllowedRoleMenus] ([menu_item_id],[role_id] ,[allowed]) VALUES (10, 3, 'false')
  INSERT INTO [dbo].[AllowedRoleMenus] ([menu_item_id],[role_id] ,[allowed]) VALUES (15, 3, 'false')
  INSERT INTO [dbo].[AllowedRoleMenus] ([menu_item_id],[role_id] ,[allowed]) VALUES (18, 3, 'false')
  INSERT INTO [dbo].[AllowedRoleMenus] ([menu_item_id],[role_id] ,[allowed]) VALUES (19, 3, 'false')
  INSERT INTO [dbo].[AllowedRoleMenus] ([menu_item_id],[role_id] ,[allowed]) VALUES (21, 3, 'false')
  INSERT INTO [dbo].[AllowedRoleMenus] ([menu_item_id],[role_id] ,[allowed]) VALUES (24, 3, 'false')
  INSERT INTO [dbo].[AllowedRoleMenus] ([menu_item_id],[role_id] ,[allowed]) VALUES (36, 3, 'false')
  INSERT INTO [dbo].[AllowedRoleMenus] ([menu_item_id],[role_id] ,[allowed]) VALUES (37, 3, 'false')
  INSERT INTO [dbo].[AllowedRoleMenus] ([menu_item_id],[role_id] ,[allowed]) VALUES (39, 3, 'false')
  INSERT INTO [dbo].[AllowedRoleMenus] ([menu_item_id],[role_id] ,[allowed]) VALUES (41, 3, 'false')
  INSERT INTO [dbo].[AllowedRoleMenus] ([menu_item_id],[role_id] ,[allowed]) VALUES (42, 3, 'false')
  INSERT INTO [dbo].[AllowedRoleMenus] ([menu_item_id],[role_id] ,[allowed]) VALUES (43, 3, 'false')
  INSERT INTO [dbo].[AllowedRoleMenus] ([menu_item_id],[role_id] ,[allowed]) VALUES (44, 3, 'false')
  INSERT INTO [dbo].[AllowedRoleMenus] ([menu_item_id],[role_id] ,[allowed]) VALUES (45, 3, 'false')
  INSERT INTO [dbo].[AllowedRoleMenus] ([menu_item_id],[role_id] ,[allowed]) VALUES (46, 3, 'false')
  INSERT INTO [dbo].[AllowedRoleMenus] ([menu_item_id],[role_id] ,[allowed]) VALUES (47, 3, 'false')
  INSERT INTO [dbo].[AllowedRoleMenus] ([menu_item_id],[role_id] ,[allowed]) VALUES (49, 3, 'false')
  INSERT INTO [dbo].[AllowedRoleMenus] ([menu_item_id],[role_id] ,[allowed]) VALUES (50, 3, 'false')
  INSERT INTO [dbo].[AllowedRoleMenus] ([menu_item_id],[role_id] ,[allowed]) VALUES (56, 3, 'false')
  INSERT INTO [dbo].[AllowedRoleMenus] ([menu_item_id],[role_id] ,[allowed]) VALUES (64, 3, 'false')
END
GO

IF EXISTS(SELECT id FROM dbo.Roles WHERE id = 2)
BEGIN
  INSERT INTO [dbo].[AllowedRoleMenus] ([menu_item_id],[role_id] ,[allowed]) VALUES (2, 5, 'false')
  INSERT INTO [dbo].[AllowedRoleMenus] ([menu_item_id],[role_id] ,[allowed]) VALUES (10, 5, 'false')
  INSERT INTO [dbo].[AllowedRoleMenus] ([menu_item_id],[role_id] ,[allowed]) VALUES (15, 5, 'false')
  INSERT INTO [dbo].[AllowedRoleMenus] ([menu_item_id],[role_id] ,[allowed]) VALUES (18, 5, 'false')
  INSERT INTO [dbo].[AllowedRoleMenus] ([menu_item_id],[role_id] ,[allowed]) VALUES (19, 5, 'false')
  INSERT INTO [dbo].[AllowedRoleMenus] ([menu_item_id],[role_id] ,[allowed]) VALUES (21, 5, 'false')
  INSERT INTO [dbo].[AllowedRoleMenus] ([menu_item_id],[role_id] ,[allowed]) VALUES (24, 5, 'false')
  INSERT INTO [dbo].[AllowedRoleMenus] ([menu_item_id],[role_id] ,[allowed]) VALUES (36, 5, 'false')
  INSERT INTO [dbo].[AllowedRoleMenus] ([menu_item_id],[role_id] ,[allowed]) VALUES (37, 5, 'false')
  INSERT INTO [dbo].[AllowedRoleMenus] ([menu_item_id],[role_id] ,[allowed]) VALUES (39, 5, 'false')
  INSERT INTO [dbo].[AllowedRoleMenus] ([menu_item_id],[role_id] ,[allowed]) VALUES (41, 5, 'false')
  INSERT INTO [dbo].[AllowedRoleMenus] ([menu_item_id],[role_id] ,[allowed]) VALUES (42, 5, 'false')
  INSERT INTO [dbo].[AllowedRoleMenus] ([menu_item_id],[role_id] ,[allowed]) VALUES (43, 5, 'false')
  INSERT INTO [dbo].[AllowedRoleMenus] ([menu_item_id],[role_id] ,[allowed]) VALUES (44, 5, 'false')
  INSERT INTO [dbo].[AllowedRoleMenus] ([menu_item_id],[role_id] ,[allowed]) VALUES (45, 5, 'false')
  INSERT INTO [dbo].[AllowedRoleMenus] ([menu_item_id],[role_id] ,[allowed]) VALUES (46, 5, 'false')
  INSERT INTO [dbo].[AllowedRoleMenus] ([menu_item_id],[role_id] ,[allowed]) VALUES (47, 5, 'false')
  INSERT INTO [dbo].[AllowedRoleMenus] ([menu_item_id],[role_id] ,[allowed]) VALUES (49, 5, 'false')
  INSERT INTO [dbo].[AllowedRoleMenus] ([menu_item_id],[role_id] ,[allowed]) VALUES (50, 5, 'false')
  INSERT INTO [dbo].[AllowedRoleMenus] ([menu_item_id],[role_id] ,[allowed]) VALUES (56, 5, 'false')
  INSERT INTO [dbo].[AllowedRoleMenus] ([menu_item_id],[role_id] ,[allowed]) VALUES (64, 5, 'false')
END
GO

INSERT INTO dbo.EventTypes (event_type, description, sort_order, accounting) VALUES ('LCIE', 'Loan credit insurance event', 620, 1)
INSERT INTO dbo.EventTypes (event_type, description, sort_order, accounting) VALUES ('LCIP', 'Loan credit insurance premium event', 630, 1)
INSERT INTO dbo.EventTypes (event_type, description, sort_order, accounting) VALUES ('LCIW', 'Loan credit insurance Write-off event', 640, 1)
GO

INSERT INTO [dbo].[EventAttributes] (event_type, name) VALUES ('LCIE', 'commission')
INSERT INTO [dbo].[EventAttributes] (event_type, name) VALUES ('LCIE', 'principal')
INSERT INTO [dbo].[EventAttributes] (event_type, name) VALUES ('LCIP', 'commission')
INSERT INTO [dbo].[EventAttributes] (event_type, name) VALUES ('LCIP', 'principal')
INSERT INTO [dbo].[EventAttributes] (event_type, name) VALUES ('LCIW', 'commission')
INSERT INTO [dbo].[EventAttributes] (event_type, name) VALUES ('LCIW', 'principal')
GO

DELETE FROM [dbo].[GeneralParameters] WHERE [key] = 'DISABLE_FUTURE_REPAYMENTS'
GO

UPDATE [TechnicalParameters] SET [value] = 'v3.3.0' WHERE [name] = 'VERSION'
GO