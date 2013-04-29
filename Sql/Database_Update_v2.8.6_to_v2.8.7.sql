ALTER TABLE [Installments]
ADD [payment_method] [smallint] NULL
GO

ALTER TABLE [InstallmentHistory]
ADD [payment_method] [smallint] NULL
GO

ALTER TABLE [Installments]
ADD [comment] [nvarchar](50) NULL
GO

ALTER TABLE [InstallmentHistory]
ADD [comment] [nvarchar](50) NULL
GO

ALTER TABLE [Installments]
ADD [pending] [bit] NOT NULL DEFAULT(0)
GO

ALTER TABLE [InstallmentHistory]
ADD [pending] [bit] NOT NULL DEFAULT(0)
GO

ALTER TABLE Roles
ADD 
	[deleted] BIT NOT NULL DEFAULT 0
GO

DELETE FROM ROLES
GO
SET IDENTITY_INSERT [Roles] ON
INSERT INTO [dbo].[Roles] ([id], [code], [deleted]) VALUES (1, 'ADMIN',0)
INSERT INTO [dbo].[Roles] ([id], [code], [deleted]) VALUES (2, 'CASHI',0)
INSERT INTO [dbo].[Roles] ([id], [code], [deleted]) VALUES (3, 'LOF',0)
INSERT INTO [dbo].[Roles] ([id], [code], [deleted])  VALUES (4, 'SUPER',0)
INSERT INTO [dbo].[Roles] ([id], [code], [deleted]) VALUES (5, 'VISIT',0)
SET IDENTITY_INSERT [Roles] OFF
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MenuItems](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[menu_name] [nvarchar](50) NOT NULL
) ON [PRIMARY]
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AllowedRoleMenus](
	[menu_item_id] [int] NOT NULL,
	[role_id] [int] NOT NULL,
	[allowed] [bit] NOT NULL,
 CONSTRAINT [PK_AllowedRoleMenus] PRIMARY KEY CLUSTERED 
(
	[menu_item_id] ASC,
	[role_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

CREATE TABLE [dbo].[UserRole](
	[role_id] [int] NOT NULL,
	[user_id] [int] NOT NULL,
	[date_role_set] [datetime] NULL
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[UserRole]  WITH CHECK ADD  CONSTRAINT [FK_UserRole_Roles] FOREIGN KEY([role_id])
REFERENCES [dbo].[Roles] ([id])
GO
ALTER TABLE [dbo].[UserRole] CHECK CONSTRAINT [FK_UserRole_Roles]
GO
ALTER TABLE [dbo].[UserRole]  WITH CHECK ADD  CONSTRAINT [FK_UserRole_Users] FOREIGN KEY([user_id])
REFERENCES [dbo].[Users] ([id])
GO
ALTER TABLE [dbo].[UserRole] CHECK CONSTRAINT [FK_UserRole_Users]
GO



INSERT INTO [dbo].[UserRole]
           ([role_id]
           ,[user_id])
SELECT Roles.id, Users.id from Users INNER JOIN Roles on Users.role_code = Roles.code
GO

CREATE TABLE [dbo].[LoanRepaymentEvents](
	[id] [int] NOT NULL,
	[past_due_days] [int] NOT NULL,
	[principal] [money] NOT NULL,
	[interests] [money] NOT NULL,
	[voucher_number] [int] NULL,
	[installment_number] [int] NOT NULL,
    [parent_id] [int] NULL,
	[commissions] [money] NOT NULL DEFAULT ((0)),
	[penalties] [money] NOT NULL DEFAULT ((0))
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[LoanRepaymentEvents]  
WITH CHECK ADD  CONSTRAINT [FK_LoanRepaymentEvents_ContractEvents] FOREIGN KEY([id])
REFERENCES [dbo].[ContractEvents] ([id])
GO

ALTER TABLE [dbo].[LoanRepaymentEvents] CHECK CONSTRAINT [FK_LoanRepaymentEvents_ContractEvents]
GO
INSERT INTO LoanRepaymentEvents
SELECT RepaymentEvents.[id],
	[past_due_days],
	[principal],
	[interests],
	[voucher_number],
	[installment_number],
    RepaymentEvents.[id],
	[commissions],
	[penalties] 
FROM RepaymentEvents
INNER JOIN dbo.ContractEvents ON dbo.RepaymentEvents.id = dbo.ContractEvents.id
GO

CREATE TABLE [dbo].[ActionItems](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[class_name] [nvarchar](50) NOT NULL,
	[method_name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_ActionItems] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]


CREATE TABLE [dbo].[AllowedRoleActions](
	[action_item_id] [int] NOT NULL,
	[role_id] [int] NOT NULL,
	[allowed] [bit] NOT NULL,
 CONSTRAINT [PK_AllowedRoleActions] PRIMARY KEY CLUSTERED 
(
	[action_item_id] ASC,
	[role_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[AllowedRoleActions]  WITH CHECK ADD  CONSTRAINT [FK_AllowedRoleActions_AllowedRoleActions] FOREIGN KEY([action_item_id], [role_id])
REFERENCES [dbo].[AllowedRoleActions] ([action_item_id], [role_id])
GO
ALTER TABLE [dbo].[AllowedRoleActions] CHECK CONSTRAINT [FK_AllowedRoleActions_AllowedRoleActions]
GO

SET IDENTITY_INSERT [MenuItems] ON
INSERT INTO [dbo].[MenuItems]([id], [menu_name]) VALUES (1, 'Clients')
INSERT INTO [dbo].[MenuItems]([id], [menu_name]) VALUES (2, 'New Client')
INSERT INTO [dbo].[MenuItems]([id], [menu_name]) VALUES (3, 'Search Client')
INSERT INTO [dbo].[MenuItems]([id], [menu_name]) VALUES (4, 'Contracts')
INSERT INTO [dbo].[MenuItems]([id], [menu_name]) VALUES (5, 'Search contract')
INSERT INTO [dbo].[MenuItems]([id], [menu_name]) VALUES (6, 'Reassign contracts')
INSERT INTO [dbo].[MenuItems]([id], [menu_name]) VALUES (7, 'Accounting')
INSERT INTO [dbo].[MenuItems]([id], [menu_name]) VALUES (8, 'Chart of Accounts')
INSERT INTO [dbo].[MenuItems]([id], [menu_name]) VALUES (9, 'Standard Bookings')
INSERT INTO [dbo].[MenuItems]([id], [menu_name]) VALUES (10, 'Account View')
INSERT INTO [dbo].[MenuItems]([id], [menu_name]) VALUES (11, 'Accounting Rules')
INSERT INTO [dbo].[MenuItems]([id], [menu_name]) VALUES (12, 'Export Transactions')
INSERT INTO [dbo].[MenuItems]([id], [menu_name]) VALUES (13, 'Closure')
INSERT INTO [dbo].[MenuItems]([id], [menu_name]) VALUES (14, 'Configuration')
INSERT INTO [dbo].[MenuItems]([id], [menu_name]) VALUES (15, 'Users')
INSERT INTO [dbo].[MenuItems]([id], [menu_name]) VALUES (16, 'Roles')
INSERT INTO [dbo].[MenuItems]([id], [menu_name]) VALUES (17, 'Language')
INSERT INTO [dbo].[MenuItems]([id], [menu_name]) VALUES (18, 'Loan Products')
INSERT INTO [dbo].[MenuItems]([id], [menu_name]) VALUES (19, 'Saving Products')
INSERT INTO [dbo].[MenuItems]([id], [menu_name]) VALUES (20, 'Guarantee Products')
INSERT INTO [dbo].[MenuItems]([id], [menu_name]) VALUES (21, 'Funding Lines')
INSERT INTO [dbo].[MenuItems]([id], [menu_name]) VALUES (22, 'Economic Activity')
INSERT INTO [dbo].[MenuItems]([id], [menu_name]) VALUES (23, 'Collaterals')
INSERT INTO [dbo].[MenuItems]([id], [menu_name]) VALUES (24, 'Provinces, Districts and Cities')
INSERT INTO [dbo].[MenuItems]([id], [menu_name]) VALUES (25, 'Standard Installment Periodicity')
INSERT INTO [dbo].[MenuItems]([id], [menu_name]) VALUES (26, 'Contract Code')
INSERT INTO [dbo].[MenuItems]([id], [menu_name]) VALUES (27, 'Exchange Rate')
INSERT INTO [dbo].[MenuItems]([id], [menu_name]) VALUES (28, 'Currencies')
INSERT INTO [dbo].[MenuItems]([id], [menu_name]) VALUES (29, 'Change application date')
INSERT INTO [dbo].[MenuItems]([id], [menu_name]) VALUES (30, 'General Settings')
INSERT INTO [dbo].[MenuItems]([id], [menu_name]) VALUES (31, 'User Settings')
INSERT INTO [dbo].[MenuItems]([id], [menu_name]) VALUES (32, 'Customizable Fields')
INSERT INTO [dbo].[MenuItems]([id], [menu_name]) VALUES (33, 'Reporting')
INSERT INTO [dbo].[MenuItems]([id], [menu_name]) VALUES (34, 'Report browser')
INSERT INTO [dbo].[MenuItems]([id], [menu_name]) VALUES (35, 'Audit Trail')
INSERT INTO [dbo].[MenuItems]([id], [menu_name]) VALUES (36, 'View Events Trail')
INSERT INTO [dbo].[MenuItems]([id], [menu_name]) VALUES (37, 'Data management')
INSERT INTO [dbo].[MenuItems]([id], [menu_name]) VALUES (38, 'Database control panel')
INSERT INTO [dbo].[MenuItems]([id], [menu_name]) VALUES (39, 'Database maintenance')
INSERT INTO [dbo].[MenuItems]([id], [menu_name]) VALUES (40, 'Export consolidation')
INSERT INTO [dbo].[MenuItems]([id], [menu_name]) VALUES (41, 'Import consolidation')
INSERT INTO [dbo].[MenuItems]([id], [menu_name]) VALUES (42, 'Window')
INSERT INTO [dbo].[MenuItems]([id], [menu_name]) VALUES (43, 'Help')
INSERT INTO [dbo].[MenuItems]([id], [menu_name]) VALUES (44, 'About Octopus')
INSERT INTO [dbo].[MenuItems]([id], [menu_name]) VALUES (45, 'Octopus Forum')
INSERT INTO [dbo].[MenuItems]([id], [menu_name]) VALUES (46, 'Questionnaire')
INSERT INTO [dbo].[MenuItems]([id], [menu_name]) VALUES (47, 'BabyLoan')
INSERT INTO [dbo].[MenuItems]([id], [menu_name]) VALUES (48, 'Synchronize')
INSERT INTO [dbo].[MenuItems]([id], [menu_name]) VALUES (49, 'MFI Information')
INSERT INTO [dbo].[MenuItems]([id], [menu_name]) VALUES (50, 'New Individual')
INSERT INTO [dbo].[MenuItems]([id], [menu_name]) VALUES (51, 'New solidary group')
INSERT INTO [dbo].[MenuItems]([id], [menu_name]) VALUES (52, 'New non solidary group')
INSERT INTO [dbo].[MenuItems]([id], [menu_name]) VALUES (53, 'New Company')
INSERT INTO [dbo].[MenuItems]([id], [menu_name]) VALUES (54, 'SOM')
INSERT INTO [dbo].[MenuItems]([id], [menu_name]) VALUES (55, 'USD')
INSERT INTO [dbo].[MenuItems]([id], [menu_name]) VALUES (56, 'Consolidated CoA')
INSERT INTO [dbo].[MenuItems]([id], [menu_name]) VALUES (57, 'Credit Closure')
INSERT INTO [dbo].[MenuItems]([id], [menu_name]) VALUES (58, 'Saving Closure')
INSERT INTO [dbo].[MenuItems]([id], [menu_name]) VALUES (59, 'French')
INSERT INTO [dbo].[MenuItems]([id], [menu_name]) VALUES (60, 'English')
INSERT INTO [dbo].[MenuItems]([id], [menu_name]) VALUES (61, 'Russian')
INSERT INTO [dbo].[MenuItems]([id], [menu_name]) VALUES (62, 'Kyrgyz')
INSERT INTO [dbo].[MenuItems]([id], [menu_name]) VALUES (63, 'Spanish')
SET IDENTITY_INSERT [MenuItems] OFF
GO

INSERT INTO [dbo].[AllowedRoleMenus] ([menu_item_id],[role_id] ,[allowed])
VALUES (2, 3, 'false')
INSERT INTO [dbo].[AllowedRoleMenus] ([menu_item_id],[role_id] ,[allowed])
VALUES (2, 2, 'false')
INSERT INTO [dbo].[AllowedRoleMenus] ([menu_item_id],[role_id] ,[allowed])
VALUES (2, 5, 'false')
INSERT INTO [dbo].[AllowedRoleMenus] ([menu_item_id],[role_id] ,[allowed])
VALUES (6, 2, 'false')
INSERT INTO [dbo].[AllowedRoleMenus] ([menu_item_id],[role_id] ,[allowed])
VALUES (6, 5, 'false')
INSERT INTO [dbo].[AllowedRoleMenus] ([menu_item_id],[role_id] ,[allowed])
VALUES (6, 3, 'false')
INSERT INTO [dbo].[AllowedRoleMenus] ([menu_item_id],[role_id] ,[allowed])
VALUES (20, 2, 'false')
INSERT INTO [dbo].[AllowedRoleMenus] ([menu_item_id],[role_id] ,[allowed])
VALUES (20, 3, 'false')
INSERT INTO [dbo].[AllowedRoleMenus] ([menu_item_id],[role_id] ,[allowed])
VALUES (20, 5, 'false')
INSERT INTO [dbo].[AllowedRoleMenus] ([menu_item_id],[role_id] ,[allowed])
VALUES (12, 2, 'false')
INSERT INTO [dbo].[AllowedRoleMenus] ([menu_item_id],[role_id] ,[allowed])
VALUES (12, 5, 'false')
INSERT INTO [dbo].[AllowedRoleMenus] ([menu_item_id],[role_id] ,[allowed])
VALUES (12, 3, 'false')
INSERT INTO [dbo].[AllowedRoleMenus] ([menu_item_id],[role_id] ,[allowed])
VALUES (13, 2, 'false')
INSERT INTO [dbo].[AllowedRoleMenus] ([menu_item_id],[role_id] ,[allowed])
VALUES (13, 5, 'false')
INSERT INTO [dbo].[AllowedRoleMenus] ([menu_item_id],[role_id] ,[allowed])
VALUES (13, 3, 'false')
INSERT INTO [dbo].[AllowedRoleMenus] ([menu_item_id],[role_id] ,[allowed])
VALUES (10, 2, 'false')
INSERT INTO [dbo].[AllowedRoleMenus] ([menu_item_id],[role_id] ,[allowed])
VALUES (10, 3, 'false')
INSERT INTO [dbo].[AllowedRoleMenus] ([menu_item_id],[role_id] ,[allowed])
VALUES (10, 5, 'false')
INSERT INTO [dbo].[AllowedRoleMenus] ([menu_item_id],[role_id] ,[allowed])
VALUES (40, 2, 'false')
INSERT INTO [dbo].[AllowedRoleMenus] ([menu_item_id],[role_id] ,[allowed])
VALUES (40, 3, 'false')
INSERT INTO [dbo].[AllowedRoleMenus] ([menu_item_id],[role_id] ,[allowed])
VALUES (40, 5, 'false')
INSERT INTO [dbo].[AllowedRoleMenus] ([menu_item_id],[role_id] ,[allowed])
VALUES (43, 3, 'false')
INSERT INTO [dbo].[AllowedRoleMenus] ([menu_item_id],[role_id] ,[allowed])
VALUES (43, 5, 'false')
INSERT INTO [dbo].[AllowedRoleMenus] ([menu_item_id],[role_id] ,[allowed])
VALUES (33, 2, 'false')
INSERT INTO [dbo].[AllowedRoleMenus] ([menu_item_id],[role_id] ,[allowed])
VALUES (33, 3, 'false')
INSERT INTO [dbo].[AllowedRoleMenus] ([menu_item_id],[role_id] ,[allowed])
VALUES (33, 5, 'false')
INSERT INTO [dbo].[AllowedRoleMenus] ([menu_item_id],[role_id] ,[allowed])
VALUES (32, 2, 'false')
INSERT INTO [dbo].[AllowedRoleMenus] ([menu_item_id],[role_id] ,[allowed])
VALUES (32, 3, 'false')
INSERT INTO [dbo].[AllowedRoleMenus] ([menu_item_id],[role_id] ,[allowed])
VALUES (32, 5, 'false')
INSERT INTO [dbo].[AllowedRoleMenus] ([menu_item_id],[role_id] ,[allowed])
VALUES (19, 2, 'false')
INSERT INTO [dbo].[AllowedRoleMenus] ([menu_item_id],[role_id] ,[allowed])
VALUES (19, 3, 'false')
INSERT INTO [dbo].[AllowedRoleMenus] ([menu_item_id],[role_id] ,[allowed])
VALUES (19, 5, 'false')
INSERT INTO [dbo].[AllowedRoleMenus] ([menu_item_id],[role_id] ,[allowed])
VALUES (15, 2, 'false')
INSERT INTO [dbo].[AllowedRoleMenus] ([menu_item_id],[role_id] ,[allowed])
VALUES (15, 3, 'false')
INSERT INTO [dbo].[AllowedRoleMenus] ([menu_item_id],[role_id] ,[allowed])
VALUES (15, 5, 'false')
INSERT INTO [dbo].[AllowedRoleMenus] ([menu_item_id],[role_id] ,[allowed])
VALUES (21, 2, 'false')
INSERT INTO [dbo].[AllowedRoleMenus] ([menu_item_id],[role_id] ,[allowed])
VALUES (21, 3, 'false')
INSERT INTO [dbo].[AllowedRoleMenus] ([menu_item_id],[role_id] ,[allowed])
VALUES (21, 5, 'false')
INSERT INTO [dbo].[AllowedRoleMenus] ([menu_item_id],[role_id] ,[allowed])
VALUES (18, 2, 'false')
INSERT INTO [dbo].[AllowedRoleMenus] ([menu_item_id],[role_id] ,[allowed])
VALUES (18, 3, 'false')
INSERT INTO [dbo].[AllowedRoleMenus] ([menu_item_id],[role_id] ,[allowed])
VALUES (18, 5, 'false')
INSERT INTO [dbo].[AllowedRoleMenus] ([menu_item_id],[role_id] ,[allowed])
VALUES (31, 2, 'false')
INSERT INTO [dbo].[AllowedRoleMenus] ([menu_item_id],[role_id] ,[allowed])
VALUES (31, 3, 'false')
INSERT INTO [dbo].[AllowedRoleMenus] ([menu_item_id],[role_id] ,[allowed])
VALUES (31, 5, 'false')
INSERT INTO [dbo].[AllowedRoleMenus] ([menu_item_id],[role_id] ,[allowed])
VALUES (22, 2, 'false')
INSERT INTO [dbo].[AllowedRoleMenus] ([menu_item_id],[role_id] ,[allowed])
VALUES (22, 3, 'false')
INSERT INTO [dbo].[AllowedRoleMenus] ([menu_item_id],[role_id] ,[allowed])
VALUES (22, 5, 'false')
INSERT INTO [dbo].[AllowedRoleMenus] ([menu_item_id],[role_id] ,[allowed])
VALUES (23, 2, 'false')
INSERT INTO [dbo].[AllowedRoleMenus] ([menu_item_id],[role_id] ,[allowed])
VALUES (23, 3, 'false')
INSERT INTO [dbo].[AllowedRoleMenus] ([menu_item_id],[role_id] ,[allowed])
VALUES (23, 5, 'false')
INSERT INTO [dbo].[AllowedRoleMenus] ([menu_item_id],[role_id] ,[allowed])
VALUES (24, 2, 'false')
INSERT INTO [dbo].[AllowedRoleMenus] ([menu_item_id],[role_id] ,[allowed])
VALUES (24, 3, 'false')
INSERT INTO [dbo].[AllowedRoleMenus] ([menu_item_id],[role_id] ,[allowed])
VALUES (24, 5, 'false')
INSERT INTO [dbo].[AllowedRoleMenus] ([menu_item_id],[role_id] ,[allowed])
VALUES (25, 2, 'false')
INSERT INTO [dbo].[AllowedRoleMenus] ([menu_item_id],[role_id] ,[allowed])
VALUES (25, 3, 'false')
INSERT INTO [dbo].[AllowedRoleMenus] ([menu_item_id],[role_id] ,[allowed])
VALUES (25, 5, 'false')
INSERT INTO [dbo].[AllowedRoleMenus] ([menu_item_id],[role_id] ,[allowed])
VALUES (26, 2, 'false')
INSERT INTO [dbo].[AllowedRoleMenus] ([menu_item_id],[role_id] ,[allowed])
VALUES (26, 3, 'false')
INSERT INTO [dbo].[AllowedRoleMenus] ([menu_item_id],[role_id] ,[allowed])
VALUES (26, 5, 'false')
INSERT INTO [dbo].[AllowedRoleMenus] ([menu_item_id],[role_id] ,[allowed])
VALUES (27, 2, 'false')
INSERT INTO [dbo].[AllowedRoleMenus] ([menu_item_id],[role_id] ,[allowed])
VALUES (27, 3, 'false')
INSERT INTO [dbo].[AllowedRoleMenus] ([menu_item_id],[role_id] ,[allowed])
VALUES (27, 5, 'false')
INSERT INTO [dbo].[AllowedRoleMenus] ([menu_item_id],[role_id] ,[allowed])
VALUES (28, 3, 'false')
INSERT INTO [dbo].[AllowedRoleMenus] ([menu_item_id],[role_id] ,[allowed])
VALUES (28, 5, 'false')
INSERT INTO [dbo].[AllowedRoleMenus] ([menu_item_id],[role_id] ,[allowed])
VALUES (28, 2, 'false')
INSERT INTO [dbo].[AllowedRoleMenus] ([menu_item_id],[role_id] ,[allowed])
VALUES (29, 3, 'false')
INSERT INTO [dbo].[AllowedRoleMenus] ([menu_item_id],[role_id] ,[allowed])
VALUES (29, 5, 'false')
INSERT INTO [dbo].[AllowedRoleMenus] ([menu_item_id],[role_id] ,[allowed])
VALUES (29, 2, 'false')
INSERT INTO [dbo].[AllowedRoleMenus] ([menu_item_id],[role_id] ,[allowed])
VALUES (35, 3, 'false')
INSERT INTO [dbo].[AllowedRoleMenus] ([menu_item_id],[role_id] ,[allowed])
VALUES (35, 5, 'false')
INSERT INTO [dbo].[AllowedRoleMenus] ([menu_item_id],[role_id] ,[allowed])
VALUES (35, 2, 'false')
INSERT INTO [dbo].[AllowedRoleMenus] ([menu_item_id],[role_id] ,[allowed])
VALUES (47, 3, 'false')
INSERT INTO [dbo].[AllowedRoleMenus] ([menu_item_id],[role_id] ,[allowed])
VALUES (47, 5, 'false')
INSERT INTO [dbo].[AllowedRoleMenus] ([menu_item_id],[role_id] ,[allowed])
VALUES (47, 2, 'false')
GO


SET IDENTITY_INSERT [ActionItems] ON
INSERT INTO [dbo].[ActionItems] ([id], [class_name],[method_name]) VALUES (1, 'LoanServices','SaveLoan')
INSERT INTO [dbo].[ActionItems] ([id], [class_name],[method_name]) VALUES (2, 'LoanServices','Disburse')
INSERT INTO [dbo].[ActionItems] ([id], [class_name],[method_name]) VALUES (3, 'LoanServices','UpdateContractStatus')
INSERT INTO [dbo].[ActionItems] ([id], [class_name],[method_name]) VALUES (4, 'LoanServices','CancelLastEvent')
INSERT INTO [dbo].[ActionItems] ([id], [class_name],[method_name]) VALUES (5, 'ClientServices','SavePerson')
INSERT INTO [dbo].[ActionItems] ([id], [class_name],[method_name]) VALUES (6, 'LoanServices','Repay')
SET IDENTITY_INSERT [ActionItems] OFF
GO

INSERT INTO [dbo].[AllowedRoleActions] ([action_item_id],[role_id] ,[allowed])
VALUES (1, 2, 'false')
INSERT INTO [dbo].[AllowedRoleActions] ([action_item_id],[role_id] ,[allowed])
VALUES (1, 5, 'false')

INSERT INTO [dbo].[AllowedRoleActions] ([action_item_id],[role_id] ,[allowed])
VALUES (2, 2, 'false')
INSERT INTO [dbo].[AllowedRoleActions] ([action_item_id],[role_id] ,[allowed])
VALUES (2, 3, 'false')
INSERT INTO [dbo].[AllowedRoleActions] ([action_item_id],[role_id] ,[allowed])
VALUES (2, 5, 'false')

INSERT INTO [dbo].[AllowedRoleActions] ([action_item_id],[role_id] ,[allowed])
VALUES (3, 2, 'false')
INSERT INTO [dbo].[AllowedRoleActions] ([action_item_id],[role_id] ,[allowed])
VALUES (3, 3, 'false')
INSERT INTO [dbo].[AllowedRoleActions] ([action_item_id],[role_id] ,[allowed])
VALUES (3, 5, 'false')

INSERT INTO [dbo].[AllowedRoleActions] ([action_item_id],[role_id] ,[allowed])
VALUES (4, 2, 'false')
GO

IF  EXISTS (SELECT * FROM sys.check_constraints WHERE object_id = OBJECT_ID(N'[dbo].[CK_Users_role]') AND parent_object_id = OBJECT_ID(N'[dbo].[Users]'))
ALTER TABLE [dbo].[Users] DROP CONSTRAINT [CK_Users_role]
GO

DELETE FROM [GeneralParameters]
WHERE [key] IN ('CASH_RECEIPT_BEFORE_CONFIRMATION', 'LOAN_OFFICER_PORTFOLIO_FILTER', 'CONSO_NUMBER',
'ALIGN_INSTALMENT_ON_REAL_DISBURSEMENT_DATE', 'CHARGE_INTEREST_WITHIN_GRACE_PERIOD',
'GROUPED_CASH_RECEIPTS',
'WEEKLY_CONSOLIDATION_DAY')
GO

ALTER TABLE [Banks]
ADD [customIBAN1] [bit] NULL
GO

ALTER TABLE [Banks]
ADD [customIBAN2] [bit] NULL
GO

UPDATE [TechnicalParameters] SET [value] = 'v2.8.7' WHERE [name] = 'VERSION'
GO
