SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[TellerEvents](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[teller_id] [int] NOT NULL,
	[event_code] [nchar](4) NOT NULL,
	[amount] [money] NOT NULL,
	[date] [datetime] NOT NULL,
	[is_exported] [bit] NOT NULL DEFAULT((0)),
 CONSTRAINT [PK_TellerEvents] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[TellerEvents]  WITH CHECK ADD  CONSTRAINT [FK_TellerEvents_Tellers] FOREIGN KEY([teller_id])
REFERENCES [dbo].[Tellers] ([id])
GO

ALTER TABLE [dbo].[TellerEvents] CHECK CONSTRAINT [FK_TellerEvents_Tellers]
GO

ALTER TABLE dbo.Tellers
ADD
	[currency_id] INT NOT NULL,
	[user_id] INT NOT NULL,
	[amount_min] MONEY,
	[amount_max] MONEY,
	[deposit_amount_min] MONEY,
	[deposit_amount_max] MONEY,
	[withdrawal_amount_min] MONEY,
	[withdrawal_amount_max] MONEY
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Tellers_ChartOfAccounts]') AND parent_object_id = OBJECT_ID(N'[dbo].[Tellers]'))
ALTER TABLE [dbo].[Tellers]  WITH CHECK ADD  CONSTRAINT [FK_Tellers_ChartOfAccounts] FOREIGN KEY([account_id])
REFERENCES [dbo].[ChartOfAccounts] ([id])
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Tellers_Users]') AND parent_object_id = OBJECT_ID(N'[dbo].[Tellers]'))
ALTER TABLE [dbo].[Tellers]  WITH CHECK ADD  CONSTRAINT [FK_Tellers_Users] FOREIGN KEY([user_id])
REFERENCES [dbo].[Users] ([id])
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Tellers_Branches]') AND parent_object_id = OBJECT_ID(N'[dbo].[Tellers]'))
ALTER TABLE [dbo].[Tellers]  WITH CHECK ADD  CONSTRAINT [FK_Tellers_Branches] FOREIGN KEY([branch_id])
REFERENCES [dbo].[Branches] ([id])
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Tellers_Currencies]') AND parent_object_id = OBJECT_ID(N'[dbo].[Tellers]'))
ALTER TABLE [dbo].[Tellers]  WITH CHECK ADD  CONSTRAINT [FK_Tellers_Currencies] FOREIGN KEY([currency_id])
REFERENCES [dbo].[Currencies] ([id])
GO

DROP TABLE dbo.UsersTellers
GO

INSERT INTO dbo.EventTypes (event_type, description, sort_order, accounting) VALUES ('CPDE', 'Close amount Positive Difference Event', 660, 1)
INSERT INTO dbo.EventTypes (event_type, description, sort_order, accounting) VALUES ('CNDE', 'Close amount Negative Difference Event', 670, 1)
INSERT INTO dbo.EventTypes (event_type, description, sort_order, accounting) VALUES ('OPDE', 'Open amount Positive Difference Event', 680, 1)
INSERT INTO dbo.EventTypes (event_type, description, sort_order, accounting) VALUES ('ONDE', 'Open amount Negative Difference Event', 690, 1)
INSERT INTO dbo.EventTypes (event_type, description, sort_order, accounting) VALUES ('ODAE', 'Open Day Amount Event', 700, 1)
INSERT INTO dbo.EventTypes (event_type, description, sort_order, accounting) VALUES ('CDAE', 'Close Day Amount Event', 710, 1)
GO

INSERT INTO [dbo].[EventAttributes] (event_type, name) VALUES ('CPDE', 'amount')
INSERT INTO [dbo].[EventAttributes] (event_type, name) VALUES ('CNDE', 'amount')
INSERT INTO [dbo].[EventAttributes] (event_type, name) VALUES ('OPDE', 'amount')
INSERT INTO [dbo].[EventAttributes] (event_type, name) VALUES ('ONDE', 'amount')
INSERT INTO [dbo].[EventAttributes] (event_type, name) VALUES ('CDAE', 'amount')
INSERT INTO [dbo].[EventAttributes] (event_type, name) VALUES ('ODAE', 'amount')
GO

ALTER TABLE [dbo].[LoanAccountingMovements]
ADD booking_type INT NOT NULL DEFAULT(1)
GO

DELETE FROM dbo.MenuItems
WHERE component_name IN ('creditClosureToolStripMenuItem', 'savingClosureToolStripMenuItem', 'mnuMonthlyClosure')
GO

UPDATE [dbo].[Tellers] SET user_id=1, currency_id = 1
GO

UPDATE  [TechnicalParameters]
SET     [value] = 'v4.6.0'
WHERE   [name] = 'VERSION'
GO
