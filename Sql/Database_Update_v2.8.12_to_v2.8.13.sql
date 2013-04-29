CREATE TABLE [LoanAccountingMovements](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[contract_id] [int] NOT NULL,
	[debit_account_number_id] INT NOT NULL,
	[credit_account_number_id] INT NOT NULL,
	[amount] [money] NOT NULL,
	[event_id] [int] NOT NULL,
	[transaction_date] [datetime] NOT NULL,
	[export_date] [datetime] NULL,
	[is_exported] [bit] NOT NULL,
	[currency_id] [int] NOT NULL,
	[exchange_rate] [float] NOT NULL DEFAULT(1),
	[rule_id] [int] NOT NULL DEFAULT(0)
 CONSTRAINT [PK_LoanAccountingMovements] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [SavingsAccountingMovements](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[contract_id] [int] NOT NULL,
	[debit_account_number_id] INT NOT NULL,
	[credit_account_number_id] INT NOT NULL,
	[amount] [money] NOT NULL,
	[event_id] [int] NOT NULL,
	[transaction_date] [datetime] NOT NULL,
	[export_date] [datetime] NULL,
	[is_exported] [bit] NOT NULL,
	[currency_id] [int] NOT NULL,
	[exchange_rate] [float] NOT NULL DEFAULT(1),
	[rule_id] [int] NOT NULL DEFAULT(0)
 CONSTRAINT [PK_SavingsAccountingMovements] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE AccountingRules
ADD 
    [debit_account_number_id] INT NOT NULL,
	[credit_account_number_id] INT NOT NULL,
    [order] [int] NOT NULL DEFAULT(0),
    [description] nvarchar(256) NULL
GO 

ALTER TABLE AccountingRules
DROP COLUMN [debit_account_number],
	[credit_account_number]
GO 

ALTER TABLE ContractEvents
ADD [is_exported] [bit] NOT NULL DEFAULT(0)
GO 

ALTER TABLE SavingEvents
ADD [is_exported] [bit] NOT NULL DEFAULT(0)
GO 

-- Daily installment type
IF NOT EXISTS (SELECT * FROM dbo.InstallmentTypes WHERE nb_of_days = 1 AND nb_of_months = 0)
	INSERT INTO dbo.InstallmentTypes ( name, nb_of_days, nb_of_months ) VALUES ( 'Daily', 1, 0 )
GO

-- Savings Book Products
ALTER TABLE [dbo].[SavingBookProducts]
ADD [overdraft_fees] [money] NULL,
	[overdraft_fees_max] [money] NULL,
	[overdraft_fees_min] [money] NULL,
	[agio_fees] [float] NULL,
	[agio_fees_max] [float] NULL,
	[agio_fees_min] [float] NULL,
	[agio_fees_freq] INT NOT NULL DEFAULT(1)
GO

-- Savings Book Contracts
ALTER TABLE [dbo].[SavingBookContracts]
ADD [flat_overdraft_fees] [money] NULL,
	[in_overdraft] [bit] NOT NULL DEFAULT(0),
	[rate_agio_fees] [float] NULL
GO

UPDATE dbo.SavingBookProducts
SET agio_fees_freq = (SELECT TOP 1 id FROM
	(
		SELECT id
		FROM dbo.InstallmentTypes
		WHERE name = 'Daily'

		UNION ALL

		SELECT TOP 1 id
		FROM dbo.InstallmentTypes
	) AS freq
)
GO

ALTER TABLE dbo.EventTypes
ADD 
sort_order int null
GO

INSERT INTO dbo.EventTypes (event_type, description, sort_order)
VALUES ('SVIE', 'New saving book', 140)
GO

INSERT INTO dbo.EventTypes (event_type, description, sort_order)
VALUES ('SVDE', 'Deposit', 150)
GO

INSERT INTO dbo.EventTypes (event_type, description, sort_order)
VALUES ('SVWE', 'Withdrawal', 160)
GO

INSERT INTO dbo.EventTypes (event_type, description, sort_order)
VALUES ('SDTE', 'Outgoing transfer', 170)
GO

INSERT INTO dbo.EventTypes (event_type, description, sort_order)
VALUES ('SCTE', 'Incoming transfer', 180)
GO

INSERT INTO dbo.EventTypes (event_type, description, sort_order)
VALUES ('SIAE', 'Accrued interests', 190)
GO

INSERT INTO dbo.EventTypes (event_type, description, sort_order)
VALUES ('SIPE', 'Posting of accrued interests', 200)
GO

INSERT INTO dbo.EventTypes (event_type, description, sort_order)
VALUES ('SWFE', 'Fee for a withdrawal', 210)
GO

INSERT INTO dbo.EventTypes (event_type, description, sort_order)
VALUES ('SMFE', 'Management fee for a period', 220)
GO

INSERT INTO dbo.EventTypes (event_type, description, sort_order)
VALUES ('SCLE', 'Saving closure', 230)
GO

INSERT INTO dbo.EventTypes (event_type, description, sort_order)
VALUES ('SOFE', 'Overdraft fee', 240)
GO

INSERT INTO dbo.EventTypes (event_type, description, sort_order)
VALUES ('SVAE', 'Agio', 250)
GO

INSERT INTO dbo.EventTypes (event_type, description, sort_order)
VALUES ('SVCE', 'Closing of a savings account', 260)
GO

INSERT INTO dbo.EventTypes (event_type, description, sort_order)
VALUES ('FLNE', 'Funding line event', 270)
GO

INSERT INTO dbo.EventTypes (event_type, description, sort_order)
VALUES ('ULIE', 'User login', 280)
GO

INSERT INTO dbo.EventTypes (event_type, description, sort_order)
VALUES ('ULOE', 'User logout', 290)
GO

UPDATE dbo.EventTypes SET sort_order = 10
WHERE event_type = 'LOVE'
GO

UPDATE dbo.EventTypes SET sort_order = 20
WHERE event_type = 'LODE'
GO

UPDATE dbo.EventTypes SET sort_order = 30
WHERE event_type = 'PDLE'
GO

UPDATE dbo.EventTypes SET sort_order = 40
WHERE event_type = 'RGLE'
GO

UPDATE dbo.EventTypes SET sort_order = 50
WHERE event_type = 'RBLE'
GO

UPDATE dbo.EventTypes SET sort_order = 60
WHERE event_type = 'ROWE'
GO

UPDATE dbo.EventTypes SET sort_order = 70
WHERE event_type = 'RRLE'
GO

UPDATE dbo.EventTypes SET sort_order = 80
WHERE event_type = 'ROLE'
GO

UPDATE dbo.EventTypes SET sort_order = 90
WHERE event_type = 'WROE'
GO

UPDATE dbo.EventTypes SET sort_order = 100
WHERE event_type = 'TEET'
GO

UPDATE dbo.EventTypes SET sort_order = 110
WHERE event_type = 'APR'
GO

UPDATE dbo.EventTypes SET sort_order = 120
WHERE event_type = 'ATR'
GO

UPDATE dbo.EventTypes SET sort_order = 130
WHERE event_type = 'APTR'
GO

ALTER TABLE dbo.FundingLineEvents
ADD user_id INT NULL
GO

ALTER TABLE dbo.FundingLineEvents
ADD contract_event_id INT NULL
GO

UPDATE dbo.TraceUserLogs
SET event_code = 'ULIE'
WHERE event_code = 'OULIE'
GO

UPDATE dbo.TraceUserLogs
SET event_code = 'ULOE'
WHERE event_code = 'OULOE'
GO

CREATE TABLE dbo.UsersSubordinates
(
	user_id INT NOT NULL
	, subordinate_id INT NOT NULL
)
GO

INSERT INTO dbo.EventTypes (event_type, description, sort_order) VALUES ('GLLL', 'Good Loan Late Loan', 300)
INSERT INTO dbo.EventTypes (event_type, description, sort_order) VALUES ('GLBL', 'Good Loan Bad Loan', 310)
INSERT INTO dbo.EventTypes (event_type, description, sort_order) VALUES ('LLGL', 'Late Loan Good Loan', 320)
INSERT INTO dbo.EventTypes (event_type, description, sort_order) VALUES ('LLBL', 'Late Loan Bad Loan', 330)
INSERT INTO dbo.EventTypes (event_type, description, sort_order) VALUES ('BLGL', 'Bad Loan Good Loan', 340)
INSERT INTO dbo.EventTypes (event_type, description, sort_order) VALUES ('BLLL', 'Bad Loan Late Loan', 350)
INSERT INTO dbo.EventTypes (event_type, description, sort_order) VALUES ('LLPE', 'Loan provision event', 360)
INSERT INTO dbo.EventTypes (event_type, description, sort_order) VALUES ('LIAE', 'Loan accrued interest event', 370)
GO

INSERT INTO EventAttributes (event_type, name) VALUES('GLLL', 'olb')
INSERT INTO EventAttributes (event_type, name) VALUES('GLBL', 'olb')
INSERT INTO EventAttributes (event_type, name) VALUES('LLGL', 'olb')
INSERT INTO EventAttributes (event_type, name) VALUES('LLBL', 'olb')
INSERT INTO EventAttributes (event_type, name) VALUES('BLGL', 'olb')
INSERT INTO EventAttributes (event_type, name) VALUES('BLLL', 'olb')

INSERT INTO EventAttributes (event_type, name) VALUES('ROLE', 'amount')
INSERT INTO EventAttributes (event_type, name) VALUES('ROLE', 'interest')

INSERT INTO EventAttributes (event_type, name) VALUES('WROE', 'olb')
INSERT INTO EventAttributes (event_type, name) VALUES('WROE', 'accrued_interests')
INSERT INTO EventAttributes (event_type, name) VALUES('WROE', 'accrued_penalties')
INSERT INTO EventAttributes (event_type, name) VALUES('WROE', 'past_due_days')

INSERT INTO EventAttributes (event_type, name) VALUES('LLPE', 'amount')
INSERT INTO EventAttributes (event_type, name) VALUES('TEET', 'amount')
INSERT INTO EventAttributes (event_type, name) VALUES('LIAE', 'accrued_interest')

INSERT INTO EventAttributes (event_type, name) VALUES ('SVIE', 'amount')
INSERT INTO EventAttributes (event_type, name) VALUES ('SVDE', 'amount')
INSERT INTO EventAttributes (event_type, name) VALUES ('SVWE', 'amount')
INSERT INTO EventAttributes (event_type, name) VALUES ('SDTE', 'amount')
INSERT INTO EventAttributes (event_type, name) VALUES ('SCTE', 'amount')
INSERT INTO EventAttributes (event_type, name) VALUES ('SIAE', 'amount')
INSERT INTO EventAttributes (event_type, name) VALUES ('SIPE', 'amount')
INSERT INTO EventAttributes (event_type, name) VALUES ('SWFE', 'amount')
INSERT INTO EventAttributes (event_type, name) VALUES ('SMFE', 'amount')
INSERT INTO EventAttributes (event_type, name) VALUES ('SCLE', 'amount')
INSERT INTO EventAttributes (event_type, name) VALUES ('SOFE', 'amount')
INSERT INTO EventAttributes (event_type, name) VALUES ('SVAE', 'amount')
INSERT INTO EventAttributes (event_type, name) VALUES ('SVCE', 'amount')

INSERT INTO EventAttributes (event_type, name) VALUES ('SVIE', 'fees')
INSERT INTO EventAttributes (event_type, name) VALUES ('SVDE', 'fees')
INSERT INTO EventAttributes (event_type, name) VALUES ('SVWE', 'fees')
INSERT INTO EventAttributes (event_type, name) VALUES ('SDTE', 'fees')
INSERT INTO EventAttributes (event_type, name) VALUES ('SCTE', 'fees')
INSERT INTO EventAttributes (event_type, name) VALUES ('SIAE', 'fees')
INSERT INTO EventAttributes (event_type, name) VALUES ('SIPE', 'fees')
INSERT INTO EventAttributes (event_type, name) VALUES ('SWFE', 'fees')
INSERT INTO EventAttributes (event_type, name) VALUES ('SMFE', 'fees')
INSERT INTO EventAttributes (event_type, name) VALUES ('SCLE', 'fees')
INSERT INTO EventAttributes (event_type, name) VALUES ('SOFE', 'fees')
INSERT INTO EventAttributes (event_type, name) VALUES ('SVAE', 'fees')
INSERT INTO EventAttributes (event_type, name) VALUES ('SVCE', 'fees')
GO

CREATE TABLE [dbo].[OverdueEvents](
	[id] [int] NOT NULL,
	[olb] [money] NOT NULL,
	[overdue_days] [int] NOT NULL,
 CONSTRAINT [PK_OverdueEvents] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[OverdueEvents]  WITH CHECK ADD  CONSTRAINT [FK_OverdueEvents_ContractEvents] FOREIGN KEY([id])
REFERENCES [dbo].[ContractEvents] ([id])
GO

ALTER TABLE [dbo].[OverdueEvents] CHECK CONSTRAINT [FK_OverdueEvents_ContractEvents]
GO

ALTER TABLE [dbo].[OverdueEvents] ADD  CONSTRAINT [DF_OverdueEvents_olb]  DEFAULT ((0)) FOR [olb]
GO

ALTER TABLE [dbo].[OverdueEvents] ADD  CONSTRAINT [DF_OverdueEvents_overdue_days]  DEFAULT ((0)) FOR [overdue_days]
GO

ALTER TABLE [dbo].[TrancheEvents]  WITH CHECK ADD  CONSTRAINT [FK_TrancheEvents_ContractEvents] FOREIGN KEY([id])
REFERENCES [dbo].[ContractEvents] ([id])
GO

ALTER TABLE [dbo].[TrancheEvents] CHECK CONSTRAINT [FK_TrancheEvents_ContractEvents]
GO

CREATE TABLE [dbo].[ProvisionEvents](
	[id] [int] NOT NULL,
	[amount] [money] NOT NULL,
	[rate] [float] NOT NULL,
	[overdue_days] [int] NOT NULL
 CONSTRAINT [PK_ProvisionEvents] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[ProvisionEvents]  WITH CHECK ADD  CONSTRAINT [FK_ProvisionEvents_ContractEvents] FOREIGN KEY([id])
REFERENCES [dbo].[ContractEvents] ([id])
GO

ALTER TABLE [dbo].[ProvisionEvents] CHECK CONSTRAINT [FK_ProvisionEvents_ContractEvents]
GO

ALTER TABLE [dbo].[ProvisionEvents] ADD  CONSTRAINT [DF_ProvisionEvents_amount]  DEFAULT ((0)) FOR [amount]
GO

TRUNCATE TABLE ContractAccountingRules
TRUNCATE TABLE FundingLineAccountingRules
DELETE FROM AccountingRules
DELETE FROM ChartOfAccounts
GO

SET IDENTITY_INSERT ChartOfAccounts ON
INSERT INTO ChartOfAccounts (id, account_number, local_account_number, label, debit_plus, type_code, account_category_id, type) SELECT  1,'1011','10101'  ,'Cash' ,1  ,'CASH' ,1,1
INSERT INTO ChartOfAccounts (id, account_number, local_account_number, label, debit_plus, type_code, account_category_id, type) SELECT  2,'2031','10913'  ,'Cash Credit'  ,1  ,'CASH_CREDIT'  ,1,1
INSERT INTO ChartOfAccounts (id, account_number, local_account_number, label, debit_plus, type_code, account_category_id, type) SELECT  3,'2032','11113'  ,'Rescheduled Loans',1  ,'RESCHEDULED_LOANS',1,1
INSERT INTO ChartOfAccounts (id, account_number, local_account_number, label, debit_plus, type_code, account_category_id, type) SELECT  4,'2037','11113'  ,'Accrued interests receivable' ,1  ,'ACCRUED_INTERESTS_LOANS'  ,1,1
INSERT INTO ChartOfAccounts (id, account_number, local_account_number, label, debit_plus, type_code, account_category_id, type) SELECT  5,'2038','11113'  ,'Accrued interests on rescheduled loans',1  ,'ACCRUED_INTERESTS_RESCHEDULED_LOANS'  ,1,1
INSERT INTO ChartOfAccounts (id, account_number, local_account_number, label, debit_plus, type_code, account_category_id, type) SELECT  6,'2911','10923'  ,'Bad Loans',1  ,'BAD_LOANS',1,1
INSERT INTO ChartOfAccounts (id, account_number, local_account_number, label, debit_plus, type_code, account_category_id, type) SELECT  7,'2921','10923'  ,'Unrecoverable Bad Loans'  ,1  ,'UNRECO_BAD_LOANS' ,1,1
INSERT INTO ChartOfAccounts (id, account_number, local_account_number, label, debit_plus, type_code, account_category_id, type) SELECT  8,'2971','40260'  ,'Interest on Past Due Loans',1  ,'INTERESTS_ON_PAST_DUE_LOANS'  ,1,1
INSERT INTO ChartOfAccounts (id, account_number, local_account_number, label, debit_plus, type_code, account_category_id, type) SELECT  9,'2972','40260'  ,'Penalties on Past Due Loans'  ,1  ,'PENALTIES_ON_PAST_DUE_LOANS_ASSET',1,1
INSERT INTO ChartOfAccounts (id, account_number, local_account_number, label, debit_plus, type_code, account_category_id, type) SELECT  10  ,'2991','10989'  ,'Loan Loss Reserve',0  ,'LOAN_LOSS_RESERVE',2,1
INSERT INTO ChartOfAccounts (id, account_number, local_account_number, label, debit_plus, type_code, account_category_id, type) SELECT  11  ,'3882','42820'  ,'Deferred Income'  ,0  ,'DEFERRED_INCOME'  ,2,1
INSERT INTO ChartOfAccounts (id, account_number, local_account_number, label, debit_plus, type_code, account_category_id, type) SELECT  12  ,'6712','57101'  ,'Provision on bad loans',1  ,'PROVISION_ON_BAD_LOANS',3,1
INSERT INTO ChartOfAccounts (id, account_number, local_account_number, label, debit_plus, type_code, account_category_id, type) SELECT  13  ,'6751','57107'  ,'Loan Loss',1  ,'LOAN_LOSS',3,1
INSERT INTO ChartOfAccounts (id, account_number, local_account_number, label, debit_plus, type_code, account_category_id, type) SELECT  14  ,'7021','40260'  ,'Interests on cash credit' ,0  ,'INTERESTS_ON_CASH_CREDIT' ,4,1
INSERT INTO ChartOfAccounts (id, account_number, local_account_number, label, debit_plus, type_code, account_category_id, type) SELECT  15  ,'7022','40222'  ,'Interests on rescheduled loans',0  ,'INTERESTS_ON_RESCHEDULED_LOANS',4,1
INSERT INTO ChartOfAccounts (id, account_number, local_account_number, label, debit_plus, type_code, account_category_id, type) SELECT  16  ,'7027','70270'  ,'Penalties on past due loans'  ,0  ,'PENALTIES_ON_PAST_DUE_LOANS_INCOME',4,1
INSERT INTO ChartOfAccounts (id, account_number, local_account_number, label, debit_plus, type_code, account_category_id, type) SELECT  17  ,'7028','40260'  ,'Interests on bad loans',0  ,'INTERESTS_ON_BAD_LOANS',4,1
INSERT INTO ChartOfAccounts (id, account_number, local_account_number, label, debit_plus, type_code, account_category_id, type) SELECT  18  ,'7029','40402'  ,'Commissions'  ,0  ,'COMMISSIONS'  ,4,1
INSERT INTO ChartOfAccounts (id, account_number, local_account_number, label, debit_plus, type_code, account_category_id, type) SELECT  19  ,'7712','42802'  ,'Provision write off'  ,0  ,'PROVISION_WRITE_OFF'  ,4,1
INSERT INTO ChartOfAccounts (id, account_number, local_account_number, label, debit_plus, type_code, account_category_id, type) SELECT  20  ,'5211','42802'  ,'Loan Loss Allowance on Current Loans' ,0  ,'LIABILITIES_LOAN_LOSS_CURRENT',2,1
INSERT INTO ChartOfAccounts (id, account_number, local_account_number, label, debit_plus, type_code, account_category_id, type) SELECT  21  ,'6731','42802'  ,'Loan Loss Allowance on Current Loans' ,1  ,'EXPENSES_LOAN_LOSS_CURRENT',3,1
INSERT INTO ChartOfAccounts (id, account_number, local_account_number, label, debit_plus, type_code, account_category_id, type) SELECT  22  ,'7731','42802'  ,'Resumption of Loan Loss allowance on current loans',0  ,'INCOME_LOAN_LOSS_CURRENT' ,4,1
INSERT INTO ChartOfAccounts (id, account_number, local_account_number, label, debit_plus, type_code, account_category_id, type) SELECT  23  ,'1322','1322','Accounts and Terms Loans' ,0  ,'ACCOUNTS_AND_TERM_LOANS'  ,2,1
INSERT INTO ChartOfAccounts (id, account_number, local_account_number, label, debit_plus, type_code, account_category_id, type) SELECT  24  ,'221','221','Savings'  ,0  ,'SAVINGS'  ,2,1
INSERT INTO ChartOfAccounts (id, account_number, local_account_number, label, debit_plus, type_code, account_category_id, type) SELECT  25  ,'2261','2261','Account payable interests on Savings Books',0  ,'ACCOUNT_PAYABLE_INTERESTS_ON_SAVINGS_BOOKS',3,1
INSERT INTO ChartOfAccounts (id, account_number, local_account_number, label, debit_plus, type_code, account_category_id, type) SELECT  26  ,'60132'  ,'60132'  ,'Interests on deposit account' ,1  ,'INTERESTS_ON_DEPOSIT_ACCOUNT' ,3,1
INSERT INTO ChartOfAccounts (id, account_number, local_account_number, label, debit_plus, type_code, account_category_id, type) SELECT  27  ,'42802'  ,'42802'  ,'Recovery of charged off assets',0  ,'RECOVERY_OF_CHARGED_OFF_ASSETS',3,1
INSERT INTO ChartOfAccounts (id, account_number, local_account_number, label, debit_plus, type_code, account_category_id, type) SELECT  28  ,'9330201','9330201','NON_BALANCE_COMMITTED_FUNDS'  ,0  ,'NON_BALANCE_COMMITTED_FUNDS'  ,3,1
INSERT INTO ChartOfAccounts (id, account_number, local_account_number, label, debit_plus, type_code, account_category_id, type) SELECT  29  ,'9330202','9330202','NON_BALANCE_VALIDATED_LOANS'  ,1  ,'NON_BALANCE_VALIDATED_LOANS'  ,1,1
INSERT INTO ChartOfAccounts (id, account_number, local_account_number, label, debit_plus, type_code, account_category_id, type) SELECT  30  ,'222','222','Term Deposit' ,0  ,'TERM_DEPOSIT' ,2,1
INSERT INTO ChartOfAccounts (id, account_number, local_account_number, label, debit_plus, type_code, account_category_id, type) SELECT  31  ,'223','223','Compulsory Savings',0  ,'COMPULSORY_SAVINGS',2,1
INSERT INTO ChartOfAccounts (id, account_number, local_account_number, label, debit_plus, type_code, account_category_id, type) SELECT  32  ,'2262','2262','Account payable interests on Term Deposit',0  ,'ACCOUNT_PAYABLE_INTERESTS_ON_TERM_DEPOSIT',3,1
INSERT INTO ChartOfAccounts (id, account_number, local_account_number, label, debit_plus, type_code, account_category_id, type) SELECT  33  ,'2263','2263','Account payable interests on Compulsory Savings'  ,0  ,'ACCOUNT_PAYABLE_INTERESTS_ON_COMPULSORY_SAVINGS'  ,3,1
INSERT INTO ChartOfAccounts (id, account_number, local_account_number, label, debit_plus, type_code, account_category_id, type) SELECT  34  ,'10915'  ,'10915'  ,'Cash credit foreign currency' ,1  ,'CASH CREDIT FOREIGN CURRENCY' ,1,0
INSERT INTO ChartOfAccounts (id, account_number, local_account_number, label, debit_plus, type_code, account_category_id, type) SELECT  35  ,'10105'  ,'10105'  ,'Cash foreign currency',1  ,'CASH FOREIGN CURRENCY',1,0
INSERT INTO ChartOfAccounts (id, account_number, local_account_number, label, debit_plus, type_code, account_category_id, type) SELECT  36  ,'10901'  ,'10901'  ,'Cash credit corporate',1  ,'CASH CREDIT CORPORATE',1,0
INSERT INTO ChartOfAccounts (id, account_number, local_account_number, label, debit_plus, type_code, account_category_id, type) SELECT  37  ,'10903'  ,'10903'  ,'Cash credit corporate foreign currency',1  ,'CASH CREDIT CORPORATE FOREIGN CURRENCY',1,0
INSERT INTO ChartOfAccounts (id, account_number, local_account_number, label, debit_plus, type_code, account_category_id, type) SELECT  38  ,'10301'  ,'10301'  ,'Bank account' ,1  ,'BANK ACCOUNT' ,1,0
INSERT INTO ChartOfAccounts (id, account_number, local_account_number, label, debit_plus, type_code, account_category_id, type) SELECT  39  ,'10305'  ,'10305'  ,'Bank account foreign currency',1  ,'BANK ACCOUNT FOREIGN CURRENCY',1,0
INSERT INTO ChartOfAccounts (id, account_number, local_account_number, label, debit_plus, type_code, account_category_id, type) SELECT  40  ,'40260'  ,'40260'  ,'Interest income'  ,0  ,'INTEREST INCOME'  ,3,0
INSERT INTO ChartOfAccounts (id, account_number, local_account_number, label, debit_plus, type_code, account_category_id, type) SELECT  41  ,'40256'  ,'40256'  ,'Interest income from corporates'  ,0  ,'INTEREST INCOME FROM CORPORATES'  ,3,0
INSERT INTO ChartOfAccounts (id, account_number, local_account_number, label, debit_plus, type_code, account_category_id, type) SELECT  42  ,'40294'  ,'40294'  ,'Penalties and commision income',0  ,'PENALTIES AND COMMISION INCOME',3,0
INSERT INTO ChartOfAccounts (id, account_number, local_account_number, label, debit_plus, type_code, account_category_id, type) SELECT  43  ,'10919'  ,'10919'  ,'Bad loans corporates' ,1  ,'BAD LOANS CORPORATE'  ,1,0
INSERT INTO ChartOfAccounts (id, account_number, local_account_number, label, debit_plus, type_code, account_category_id, type) SELECT  44  ,'11101'  ,'11101'  ,'Rescheduled loans corporates' ,1  ,'RESCHEDULED LOANS CORPORATES' ,1,0
SET IDENTITY_INSERT ChartOfAccounts OFF
GO

--SET IDENTITY_INSERT AccountingRules ON
--INSERT INTO AccountingRules (id, rule_type, deleted, debit_account_number_id, credit_account_number_id, event_type,  event_attribute_id, [order], [description]) SELECT  1           ,'C' ,    0       ,    2                       ,    1                        ,'LODE' ,    29                 ,    1           ,N'Выдача займа физическому лицу в национальной вылюте наличными'
--INSERT INTO AccountingRules (id, rule_type, deleted, debit_account_number_id, credit_account_number_id, event_type,  event_attribute_id, [order], [description]) SELECT  2           ,'C' ,    0       ,    34                      ,    35                       ,'LODE' ,    29                 ,    2           ,N'Выдача займа физическому лицу в иностранной валюте наличными'
--INSERT INTO AccountingRules (id, rule_type, deleted, debit_account_number_id, credit_account_number_id, event_type,  event_attribute_id, [order], [description]) SELECT  3           ,'C' ,    0       ,    36                      ,    1                        ,'LODE' ,    29                 ,    3           ,N'Выдача займа юридическому лицу в национальной валюте наличными'
--INSERT INTO AccountingRules (id, rule_type, deleted, debit_account_number_id, credit_account_number_id, event_type,  event_attribute_id, [order], [description]) SELECT  4           ,'C' ,    0       ,    37                      ,    35                       ,'LODE' ,    29                 ,    4           ,N'Выдача займа юридическому лицу в иностранной валюте наличными'
--INSERT INTO AccountingRules (id, rule_type, deleted, debit_account_number_id, credit_account_number_id, event_type,  event_attribute_id, [order], [description]) SELECT  5           ,'C' ,    0       ,    2                       ,    38                       ,'LODE' ,    29                 ,    5           ,N'Выдача займа физическому лицу в национальной валюте с расчетного счета'
--INSERT INTO AccountingRules (id, rule_type, deleted, debit_account_number_id, credit_account_number_id, event_type,  event_attribute_id, [order], [description]) SELECT  6           ,'C' ,    0       ,    34                      ,    39                       ,'LODE' ,    29                 ,    6           ,N'Выдача займа физическому лицу в иностранной валюте с расчетного счета'
--INSERT INTO AccountingRules (id, rule_type, deleted, debit_account_number_id, credit_account_number_id, event_type,  event_attribute_id, [order], [description]) SELECT  7           ,'C' ,    0       ,    36                      ,    38                       ,'LODE' ,    29                 ,    7           ,N'Выдача займа юридическому лицу в национальной валюте с расчетного счета'
--INSERT INTO AccountingRules (id, rule_type, deleted, debit_account_number_id, credit_account_number_id, event_type,  event_attribute_id, [order], [description]) SELECT  8           ,'C' ,    0       ,    37                      ,    39                       ,'LODE' ,    29                 ,    8           ,N'Выдача займа юридическому лицу в иностранной валюте с расчетного счета'
--INSERT INTO AccountingRules (id, rule_type, deleted, debit_account_number_id, credit_account_number_id, event_type,  event_attribute_id, [order], [description]) SELECT  9           ,'C' ,    0       ,    1                       ,    2                        ,'RGLE' ,    5                  ,    9           ,N'Погашение основной задолжности физического лица по нормальному кредиту в национальной валюте в кассу'
--INSERT INTO AccountingRules (id, rule_type, deleted, debit_account_number_id, credit_account_number_id, event_type,  event_attribute_id, [order], [description]) SELECT  10          ,'C' ,    0       ,    35                      ,    34                       ,'RGLE' ,    5                  ,    10          ,N'Погашение основной задолжности физического лица по нормальному кредиту в иностранной валюте в кассу'
--INSERT INTO AccountingRules (id, rule_type, deleted, debit_account_number_id, credit_account_number_id, event_type,  event_attribute_id, [order], [description]) SELECT  11          ,'C' ,    0       ,    1                       ,    36                       ,'RGLE' ,    5                  ,    11          ,N'Погашение основной задолжности юридического лица по нормальному кредиту в национальной валюте в кассу'
--INSERT INTO AccountingRules (id, rule_type, deleted, debit_account_number_id, credit_account_number_id, event_type,  event_attribute_id, [order], [description]) SELECT  12          ,'C' ,    0       ,    35                      ,    37                       ,'RGLE' ,    5                  ,    12          ,N'Погашение основной задолжности юридического лица по нормальному кредиту в иностранной валюте в кассу'
--INSERT INTO AccountingRules (id, rule_type, deleted, debit_account_number_id, credit_account_number_id, event_type,  event_attribute_id, [order], [description]) SELECT  13          ,'C' ,    0       ,    38                      ,    2                        ,'RGLE' ,    5                  ,    13          ,N'Погашение основной задолжности физического лица по нормальному кредиту в национальной валюте на расчетный счет'
--INSERT INTO AccountingRules (id, rule_type, deleted, debit_account_number_id, credit_account_number_id, event_type,  event_attribute_id, [order], [description]) SELECT  14          ,'C' ,    0       ,    39                      ,    34                       ,'RGLE' ,    5                  ,    14          ,N'Погашение основной задолжности физического лица по нормальному кредиту в иностранной валюте на расчетный счет'
--INSERT INTO AccountingRules (id, rule_type, deleted, debit_account_number_id, credit_account_number_id, event_type,  event_attribute_id, [order], [description]) SELECT  15          ,'C' ,    0       ,    38                      ,    36                       ,'RGLE' ,    5                  ,    15          ,N'Погашение основной задолжности юридического лица по нормальному кредиту в национальной валюте на расчетный счет'
--INSERT INTO AccountingRules (id, rule_type, deleted, debit_account_number_id, credit_account_number_id, event_type,  event_attribute_id, [order], [description]) SELECT  16          ,'C' ,    0       ,    39                      ,    37                       ,'RGLE' ,    5                  ,    16          ,N'Погашение основной задолжности юридического лица по нормальному кредиту в иностранной валюте на расчетный счет'
--INSERT INTO AccountingRules (id, rule_type, deleted, debit_account_number_id, credit_account_number_id, event_type,  event_attribute_id, [order], [description]) SELECT  17          ,'C' ,    0       ,    1                       ,    40                       ,'RGLE' ,    8                  ,    17          ,N'Доход по процентам от физических лиц по нормальному кредиту в национальной валюте в кассу'
--INSERT INTO AccountingRules (id, rule_type, deleted, debit_account_number_id, credit_account_number_id, event_type,  event_attribute_id, [order], [description]) SELECT  18          ,'C' ,    0       ,    1                       ,    41                       ,'RGLE' ,    8                  ,    18          ,N'Доход по процентам от юридических лиц по нормальному кредиту в национальной валюте в кассу'
--INSERT INTO AccountingRules (id, rule_type, deleted, debit_account_number_id, credit_account_number_id, event_type,  event_attribute_id, [order], [description]) SELECT  19          ,'C' ,    0       ,    38                      ,    40                       ,'RGLE' ,    8                  ,    19          ,N'Доход по процентам от физических лиц по нормальному кредиту в национальной валюте на расчетный счет'
--INSERT INTO AccountingRules (id, rule_type, deleted, debit_account_number_id, credit_account_number_id, event_type,  event_attribute_id, [order], [description]) SELECT  20          ,'C' ,    0       ,    38                      ,    41                       ,'RGLE' ,    8                  ,    20          ,N'Доход по процентам от юридических лиц по нормальному кредиту в национальной валюте в кассу'
--INSERT INTO AccountingRules (id, rule_type, deleted, debit_account_number_id, credit_account_number_id, event_type,  event_attribute_id, [order], [description]) SELECT  21          ,'C' ,    0       ,    1                       ,    6                        ,'RBLE' ,    9                  ,    21          ,N'Погашение основной задолжности физизического лица по просроченному крелиту в национальной валюте в кассу'
--INSERT INTO AccountingRules (id, rule_type, deleted, debit_account_number_id, credit_account_number_id, event_type,  event_attribute_id, [order], [description]) SELECT  22          ,'C' ,    0       ,    1                       ,    43                       ,'RBLE' ,    9                  ,    22          ,N'Погашение основной задолжности юридического лица по просроченному крелиту в национальной валюте в кассу'
--INSERT INTO AccountingRules (id, rule_type, deleted, debit_account_number_id, credit_account_number_id, event_type,  event_attribute_id, [order], [description]) SELECT  23          ,'C' ,    0       ,    38                      ,    6                        ,'RBLE' ,    9                  ,    23          ,N'Погашение основной задолжности физизического лица по просроченному крелиту в национальной валюте на расчетный счет'
--INSERT INTO AccountingRules (id, rule_type, deleted, debit_account_number_id, credit_account_number_id, event_type,  event_attribute_id, [order], [description]) SELECT  24          ,'C' ,    0       ,    38                      ,    43                       ,'RBLE' ,    9                  ,    24          ,N'Погашение основной задолжности юридического лица по просроченному крелиту в национальной валюте на расчетный счет'
--INSERT INTO AccountingRules (id, rule_type, deleted, debit_account_number_id, credit_account_number_id, event_type,  event_attribute_id, [order], [description]) SELECT  25          ,'C' ,    0       ,    1                       ,    42                       ,'RBLE' ,    10                 ,    25          ,N'Доход по пени от физизического лица по просроченному кредиту в национальной валюте в кассу'
--INSERT INTO AccountingRules (id, rule_type, deleted, debit_account_number_id, credit_account_number_id, event_type,  event_attribute_id, [order], [description]) SELECT  26          ,'C' ,    0       ,    1                       ,    42                       ,'RBLE' ,    10                 ,    26          ,N'Доход по пени от юридического лица по просроченному кредиту в национальной валюте в кассу'
--INSERT INTO AccountingRules (id, rule_type, deleted, debit_account_number_id, credit_account_number_id, event_type,  event_attribute_id, [order], [description]) SELECT  27          ,'C' ,    0       ,    38                      ,    42                       ,'RBLE' ,    10                 ,    27          ,N'Доход по пени от физизического лица по просроченному кредиту в национальной валюте на расчетный счет'
--INSERT INTO AccountingRules (id, rule_type, deleted, debit_account_number_id, credit_account_number_id, event_type,  event_attribute_id, [order], [description]) SELECT  28          ,'C' ,    0       ,    38                      ,    42                       ,'RBLE' ,    10                 ,    28          ,N'Доход по пени от юридического лица по просроченному кредиту в национальной валюте на расчетный счет'
--INSERT INTO AccountingRules (id, rule_type, deleted, debit_account_number_id, credit_account_number_id, event_type,  event_attribute_id, [order], [description]) SELECT  29          ,'C' ,    0       ,    1                       ,    3                        ,'RRLE' ,    1                  ,    29          ,N'Погашение основной задолжности фического лица по реструктурированому кредиту в национальной валюте в кассу'
--INSERT INTO AccountingRules (id, rule_type, deleted, debit_account_number_id, credit_account_number_id, event_type,  event_attribute_id, [order], [description]) SELECT  30          ,'C' ,    0       ,    1                       ,    44                       ,'RRLE' ,    1                  ,    30          ,N'Погашение по основной задолжности юридического лица в национальной валюте в кассу'
--SET IDENTITY_INSERT AccountingRules OFF
--GO

--INSERT INTO ContractAccountingRules (id, product_type, loan_product_id, guarantee_product_id, savings_product_id, client_type, activity_id) SELECT  1           ,    1            ,    NULL            ,    NULL                 ,    NULL               ,'-' ,    NULL
--INSERT INTO ContractAccountingRules (id, product_type, loan_product_id, guarantee_product_id, savings_product_id, client_type, activity_id) SELECT  2           ,    1            ,    NULL            ,    NULL                 ,    NULL               ,'-' ,    NULL
--INSERT INTO ContractAccountingRules (id, product_type, loan_product_id, guarantee_product_id, savings_product_id, client_type, activity_id) SELECT  3           ,    1            ,    NULL            ,    NULL                 ,    NULL               ,'-' ,    NULL
--INSERT INTO ContractAccountingRules (id, product_type, loan_product_id, guarantee_product_id, savings_product_id, client_type, activity_id) SELECT  4           ,    1            ,    NULL            ,    NULL                 ,    NULL               ,'-' ,    NULL
--INSERT INTO ContractAccountingRules (id, product_type, loan_product_id, guarantee_product_id, savings_product_id, client_type, activity_id) SELECT  5           ,    1            ,    NULL            ,    NULL                 ,    NULL               ,'-' ,    NULL
--INSERT INTO ContractAccountingRules (id, product_type, loan_product_id, guarantee_product_id, savings_product_id, client_type, activity_id) SELECT  6           ,    1            ,    NULL            ,    NULL                 ,    NULL               ,'-' ,    NULL
--INSERT INTO ContractAccountingRules (id, product_type, loan_product_id, guarantee_product_id, savings_product_id, client_type, activity_id) SELECT  7           ,    1            ,    NULL            ,    NULL                 ,    NULL               ,'-' ,    NULL
--INSERT INTO ContractAccountingRules (id, product_type, loan_product_id, guarantee_product_id, savings_product_id, client_type, activity_id) SELECT  8           ,    1            ,    NULL            ,    NULL                 ,    NULL               ,'-' ,    NULL
--INSERT INTO ContractAccountingRules (id, product_type, loan_product_id, guarantee_product_id, savings_product_id, client_type, activity_id) SELECT  9           ,    1            ,    NULL            ,    NULL                 ,    NULL               ,'I' ,    NULL
--INSERT INTO ContractAccountingRules (id, product_type, loan_product_id, guarantee_product_id, savings_product_id, client_type, activity_id) SELECT  10          ,    1            ,    NULL            ,    NULL                 ,    NULL               ,'-' ,    NULL
--INSERT INTO ContractAccountingRules (id, product_type, loan_product_id, guarantee_product_id, savings_product_id, client_type, activity_id) SELECT  11          ,    1            ,    NULL            ,    NULL                 ,    NULL               ,'-' ,    NULL
--INSERT INTO ContractAccountingRules (id, product_type, loan_product_id, guarantee_product_id, savings_product_id, client_type, activity_id) SELECT  12          ,    1            ,    NULL            ,    NULL                 ,    NULL               ,'-' ,    NULL
--INSERT INTO ContractAccountingRules (id, product_type, loan_product_id, guarantee_product_id, savings_product_id, client_type, activity_id) SELECT  13          ,    1            ,    NULL            ,    NULL                 ,    NULL               ,'-' ,    NULL
--INSERT INTO ContractAccountingRules (id, product_type, loan_product_id, guarantee_product_id, savings_product_id, client_type, activity_id) SELECT  14          ,    1            ,    NULL            ,    NULL                 ,    NULL               ,'-' ,    NULL
--INSERT INTO ContractAccountingRules (id, product_type, loan_product_id, guarantee_product_id, savings_product_id, client_type, activity_id) SELECT  15          ,    1            ,    NULL            ,    NULL                 ,    NULL               ,'-' ,    NULL
--INSERT INTO ContractAccountingRules (id, product_type, loan_product_id, guarantee_product_id, savings_product_id, client_type, activity_id) SELECT  16          ,    1            ,    NULL            ,    NULL                 ,    NULL               ,'-' ,    NULL
--INSERT INTO ContractAccountingRules (id, product_type, loan_product_id, guarantee_product_id, savings_product_id, client_type, activity_id) SELECT  17          ,    1            ,    NULL            ,    NULL                 ,    NULL               ,'I' ,    NULL
--INSERT INTO ContractAccountingRules (id, product_type, loan_product_id, guarantee_product_id, savings_product_id, client_type, activity_id) SELECT  18          ,    1            ,    NULL            ,    NULL                 ,    NULL               ,'C' ,    NULL
--INSERT INTO ContractAccountingRules (id, product_type, loan_product_id, guarantee_product_id, savings_product_id, client_type, activity_id) SELECT  19          ,    1            ,    NULL            ,    NULL                 ,    NULL               ,'-' ,    NULL
--INSERT INTO ContractAccountingRules (id, product_type, loan_product_id, guarantee_product_id, savings_product_id, client_type, activity_id) SELECT  20          ,    1            ,    NULL            ,    NULL                 ,    NULL               ,'-' ,    NULL
--INSERT INTO ContractAccountingRules (id, product_type, loan_product_id, guarantee_product_id, savings_product_id, client_type, activity_id) SELECT  21          ,    1            ,    NULL            ,    NULL                 ,    NULL               ,'I' ,    NULL
--INSERT INTO ContractAccountingRules (id, product_type, loan_product_id, guarantee_product_id, savings_product_id, client_type, activity_id) SELECT  22          ,    1            ,    NULL            ,    NULL                 ,    NULL               ,'-' ,    NULL
--INSERT INTO ContractAccountingRules (id, product_type, loan_product_id, guarantee_product_id, savings_product_id, client_type, activity_id) SELECT  23          ,    1            ,    NULL            ,    NULL                 ,    NULL               ,'-' ,    NULL
--INSERT INTO ContractAccountingRules (id, product_type, loan_product_id, guarantee_product_id, savings_product_id, client_type, activity_id) SELECT  24          ,    1            ,    NULL            ,    NULL                 ,    NULL               ,'-' ,    NULL
--INSERT INTO ContractAccountingRules (id, product_type, loan_product_id, guarantee_product_id, savings_product_id, client_type, activity_id) SELECT  25          ,    1            ,    NULL            ,    NULL                 ,    NULL               ,'I' ,    NULL
--INSERT INTO ContractAccountingRules (id, product_type, loan_product_id, guarantee_product_id, savings_product_id, client_type, activity_id) SELECT  26          ,    1            ,    NULL            ,    NULL                 ,    NULL               ,'-' ,    NULL
--INSERT INTO ContractAccountingRules (id, product_type, loan_product_id, guarantee_product_id, savings_product_id, client_type, activity_id) SELECT  27          ,    1            ,    NULL            ,    NULL                 ,    NULL               ,'-' ,    NULL
--INSERT INTO ContractAccountingRules (id, product_type, loan_product_id, guarantee_product_id, savings_product_id, client_type, activity_id) SELECT  28          ,    1            ,    NULL            ,    NULL                 ,    NULL               ,'-' ,    NULL
--INSERT INTO ContractAccountingRules (id, product_type, loan_product_id, guarantee_product_id, savings_product_id, client_type, activity_id) SELECT  29          ,    1            ,    NULL            ,    NULL                 ,    NULL               ,'-' ,    NULL
--INSERT INTO ContractAccountingRules (id, product_type, loan_product_id, guarantee_product_id, savings_product_id, client_type, activity_id) SELECT  30          ,    1            ,    NULL            ,    NULL                 ,    NULL               ,'-' ,    NULL
--GO

INSERT INTO dbo.ActionItems VALUES('LoanServices', 'CanUserDisableEntryFees')
GO

INSERT INTO dbo.UsersSubordinates
SELECT 1, id
FROM dbo.Users WHERE id <> 1
GO

ALTER TABLE [dbo].[SavingEvents]
ADD [savings_method] [smallint] NULL,
	[pending] [bit] NOT NULL DEFAULT ((0))
GO

ALTER TABLE [dbo].[Persons] WITH NOCHECK 
ADD CONSTRAINT [CK_Persons_Sex] CHECK (sex='M' OR sex = 'F') 
GO

IF EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('dbo.RepaymentSchedule') AND [type] = 'P')
DROP PROCEDURE dbo.RepaymentSchedule
GO

INSERT INTO [dbo].[ActionItems] VALUES('SavingServices','SaveContract')
INSERT INTO [dbo].[ActionItems] VALUES('SavingServices','CloseAndWithdraw')
INSERT INTO [dbo].[ActionItems] VALUES('SavingServices','CloseAndTransfer')
INSERT INTO [dbo].[ActionItems] VALUES('SavingServices','Transfer')
INSERT INTO [dbo].[ActionItems] VALUES('SavingServices','Withdraw')
INSERT INTO [dbo].[ActionItems] VALUES('SavingServices','Deposit')
INSERT INTO [dbo].[ActionItems] VALUES('SavingServices','CanUserMakeBalanceNegative')
GO

UPDATE dbo.SavingBookProducts
SET deposit_fees = 0
WHERE (deposit_fees IS NULL) AND (deposit_fees_min IS NULL) AND (deposit_fees_max IS NULL)

UPDATE dbo.SavingBookProducts
SET close_fees = 0
WHERE (close_fees IS NULL) AND (close_fees_min IS NULL) AND (close_fees_max IS NULL)

UPDATE dbo.SavingBookProducts
SET management_fees = 0
WHERE (management_fees IS NULL) AND (management_fees_min IS NULL) AND (management_fees_max IS NULL)

UPDATE dbo.SavingBookProducts
SET overdraft_fees = 0
WHERE (overdraft_fees IS NULL) AND (overdraft_fees_min IS NULL) AND (overdraft_fees_max IS NULL)

UPDATE dbo.SavingBookProducts
SET agio_fees = 0
WHERE (agio_fees IS NULL) AND (agio_fees_min IS NULL) AND (agio_fees_max IS NULL)

UPDATE dbo.SavingBookContracts
SET flat_deposit_fees = 0
WHERE flat_deposit_fees IS NULL

UPDATE dbo.SavingBookContracts
SET flat_close_fees = 0
WHERE flat_close_fees IS NULL

UPDATE dbo.SavingBookContracts
SET flat_management_fees = 0
WHERE flat_management_fees IS NULL

UPDATE dbo.SavingBookContracts
SET flat_overdraft_fees = 0
WHERE flat_overdraft_fees IS NULL

UPDATE dbo.SavingBookContracts
SET rate_agio_fees = 0
WHERE rate_agio_fees IS NULL

UPDATE [TechnicalParameters] SET [value] = 'v2.8.13' WHERE [name] = 'VERSION'
GO
