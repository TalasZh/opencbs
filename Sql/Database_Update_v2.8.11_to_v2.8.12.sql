-- DEFAULT FUNDING LINE
INSERT INTO FundingLines (name, begin_date, end_date, amount, purpose, deleted, currency_id) 
VALUES('NO_FUNDING_LINE', GETDATE(), '2020-01-01', 922337203685477, 'NO_FUNDING_LINE', 0, 1)

DECLARE @id INT

SELECT @id = id
FROM FundingLines
WHERE name = 'NO_FUNDING_LINE'

INSERT INTO FundingLineEvents (code, amount, direction, fundingline_id, deleted, creation_date, type)
VALUES('UNLIMITED', 922337203685477, 1, @id, 0, GETDATE(), 0)
GO

/* CollateralProducts */
CREATE TABLE [dbo].[CollateralProducts](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](100) NOT NULL,
	[desc] [nvarchar](100) NOT NULL,
	[deleted] [bit] NOT NULL,
CONSTRAINT [PK_CollateralProducts] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [IX_CollateralProducts_name] UNIQUE NONCLUSTERED 
(
	[name] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

/* CollateralPropertyTypes */
CREATE TABLE [dbo].[CollateralPropertyTypes](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](30) NOT NULL,
 CONSTRAINT [PK_CollateralPropertyTypes] PRIMARY KEY CLUSTERED 
(
	[id] ASC
) WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

INSERT INTO [dbo].[CollateralPropertyTypes] ([name]) VALUES ('Number')
INSERT INTO [dbo].[CollateralPropertyTypes] ([name]) VALUES ('String')
INSERT INTO [dbo].[CollateralPropertyTypes] ([name]) VALUES ('Date')
INSERT INTO [dbo].[CollateralPropertyTypes] ([name]) VALUES ('Collection')
INSERT INTO [dbo].[CollateralPropertyTypes] ([name]) VALUES ('Owner')
GO

/* CollateralProperties */
CREATE TABLE [dbo].[CollateralProperties](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[product_id] [int] NOT NULL,
	[type_id] [int] NOT NULL,
	[name] [nvarchar](30) NOT NULL,
	[desc] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_CollateralProperties] PRIMARY KEY CLUSTERED 
(
	[id] ASC
) WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

/* CollateralPropertyCollections */
CREATE TABLE [dbo].[CollateralPropertyCollections](
	[property_id] [int] NOT NULL,
	[value] [nvarchar](100) NOT NULL
)
GO

/* CollateralsLinkContracts */
CREATE TABLE [dbo].[CollateralsLinkContracts](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[contract_id] [int] NOT NULL,
 CONSTRAINT [PK_CollateralsLinkContracts] PRIMARY KEY CLUSTERED 
(
	[id] ASC
) WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

/* CollateralPropertyValues */
CREATE TABLE [dbo].[CollateralPropertyValues](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[contract_collateral_id] [int] NOT NULL,
	[property_id] [int] NOT NULL,
	[value] [nvarchar](100) NULL,
 CONSTRAINT [PK_CollateralPropertyValues] PRIMARY KEY CLUSTERED 
(
	[id] ASC
) WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

/* Migration to new collateral system */

-- CollateralProducts
SET IDENTITY_INSERT CollateralProducts ON
INSERT INTO dbo.CollateralProducts([id], [name], [desc], [deleted])
SELECT Col.id, Col.name, Col.name, Col.deleted FROM dbo.Collaterals AS Col
SET IDENTITY_INSERT CollateralProducts OFF
GO

-- CollateralProperties
INSERT INTO dbo.CollateralProperties([product_id], [type_id], [name], [desc])
SELECT ColProd.id, 1 AS [type_id], 'Amount' AS [name], 'Collateral amount' AS [desc] 
FROM dbo.CollateralProducts AS ColProd
GO

INSERT INTO dbo.CollateralProperties([product_id], [type_id], [name], [desc])
SELECT ColProd.id, 2 AS [type_id], 'Description' AS [name], 'Collateral description' AS [desc] 
FROM dbo.CollateralProducts AS ColProd
GO

-- Temp table for LinkCollateralCredit
SELECT IDENTITY(int, 1, 1) AS Id, 
LinkCollateralCredit.contract_id, 
LinkCollateralCredit.collateral_id, 
LinkCollateralCredit.collateral_amount,
LinkCollateralCredit.collateral_desc
INTO #Temp
FROM dbo.LinkCollateralCredit
GO

-- CollateralPropertyValues
DECLARE @Id INT
DECLARE @collateral_id INT
DECLARE @amount INT
DECLARE @property_id_num INT
DECLARE @property_id_str INT
DECLARE @desc NVARCHAR(100)
DECLARE @contract_collateral_id INT

WHILE (SELECT Count(*) FROM #Temp) > 0
BEGIN
    SELECT TOP 1 @Id = Id From #Temp
	
	SELECT @collateral_id = collateral_id FROM #Temp WHERE Id = @Id
	SELECT @amount = collateral_amount FROM #Temp WHERE Id = @Id
	SELECT @desc = collateral_desc FROM #Temp WHERE Id = @Id
	
	SELECT @property_id_num = id FROM CollateralProperties WHERE product_id = @collateral_id AND [type_id] = 1
	SELECT @property_id_str = id FROM CollateralProperties WHERE product_id = @collateral_id AND [type_id] = 2
	
    INSERT INTO CollateralsLinkContracts (contract_id) SELECT contract_id FROM #Temp WHERE Id = @Id
    SELECT @contract_collateral_id = @@IDENTITY
    
    INSERT INTO CollateralPropertyValues (contract_collateral_id, property_id, value) VALUES (@contract_collateral_id, @property_id_num, @amount)
	INSERT INTO CollateralPropertyValues (contract_collateral_id, property_id, value) VALUES (@contract_collateral_id, @property_id_str, @desc)
	
    DELETE #Temp WHERE Id = @Id
END
GO

DROP TABLE #Temp
GO

DROP TABLE dbo.Collaterals
GO

DROP TABLE dbo.LinkCollateralCredit
GO

/* END of Migration to new collateral system */

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ChartOfAccounts]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ChartOfAccounts](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[account_number] [nvarchar](50) NOT NULL,
	[local_account_number] [nvarchar](50) NULL,
	[label] [nvarchar](200) NOT NULL,
	[debit_plus] [bit] NOT NULL,
	[type_code] [varchar](60) NOT NULL,
	[account_category_id] [smallint] NOT NULL,
	[type] [bit] NOT NULL DEFAULT ((0)),
	[parent_account_id] [int] NULL,
 CONSTRAINT [PK_ChartOfAccounts] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [UK_ChartOfAccounts] UNIQUE NONCLUSTERED 
(
	[account_number] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AccountsCategory]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[AccountsCategory](
	[id] [smallint] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](50) NOT NULL
 CONSTRAINT [PK_AccountsCategory] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
) ON [PRIMARY]
END
GO

INSERT INTO [ChartOfAccounts]([account_number], [local_account_number], [label], [debit_plus], [type_code], [account_category_id], [type]) 
VALUES('1011','10101','Cash',1,'CASH',1, 1)
INSERT INTO [ChartOfAccounts]([account_number], [local_account_number], [label], [debit_plus], [type_code], [account_category_id], [type]) 
VALUES('2031','10913','Cash Credit',1,'CASH_CREDIT',1, 1)
INSERT INTO [ChartOfAccounts]([account_number], [local_account_number], [label], [debit_plus], [type_code], [account_category_id], [type]) 
VALUES('2032','11113','Rescheduled Loans',1,'RESCHEDULED_LOANS',1, 1)
INSERT INTO [ChartOfAccounts]([account_number], [local_account_number], [label], [debit_plus], [type_code], [account_category_id], [type]) 
VALUES('2037','11113','Accrued interests receivable',1,'ACCRUED_INTERESTS_LOANS',1, 1)
INSERT INTO [ChartOfAccounts]([account_number], [local_account_number], [label], [debit_plus], [type_code], [account_category_id], [type]) 
VALUES('2038','11113','Accrued interests on rescheduled loans',1,'ACCRUED_INTERESTS_RESCHEDULED_LOANS',1, 1)
INSERT INTO [ChartOfAccounts]([account_number], [local_account_number], [label], [debit_plus], [type_code], [account_category_id], [type]) 
VALUES('2911','10923','Bad Loans',1,'BAD_LOANS',1, 1)
INSERT INTO [ChartOfAccounts]([account_number], [local_account_number], [label], [debit_plus], [type_code], [account_category_id], [type]) 
VALUES('2921','10923','Unrecoverable Bad Loans',1,'UNRECO_BAD_LOANS',1, 1)
INSERT INTO [ChartOfAccounts]([account_number], [local_account_number], [label], [debit_plus], [type_code], [account_category_id], [type]) 
VALUES('2971','40260','Interest on Past Due Loans',1,'INTERESTS_ON_PAST_DUE_LOANS',1, 1)
INSERT INTO [ChartOfAccounts]([account_number], [local_account_number], [label], [debit_plus], [type_code], [account_category_id], [type]) 
VALUES('2972','40260','Penalties on Past Due Loans',1,'PENALTIES_ON_PAST_DUE_LOANS_ASSET',1, 1)
INSERT INTO [ChartOfAccounts]([account_number], [local_account_number], [label], [debit_plus], [type_code], [account_category_id], [type]) 
VALUES('2991','10989','Loan Loss Reserve',0,'LOAN_LOSS_RESERVE',2, 1)
INSERT INTO [ChartOfAccounts]([account_number], [local_account_number], [label], [debit_plus], [type_code], [account_category_id], [type]) 
VALUES('3882','42820','Deferred Income',0,'DEFERRED_INCOME',2, 1)
INSERT INTO [ChartOfAccounts]([account_number], [local_account_number], [label], [debit_plus], [type_code], [account_category_id], [type]) 
VALUES('6712','57101','Provision on bad loans',1,'PROVISION_ON_BAD_LOANS',3, 1)
INSERT INTO [ChartOfAccounts]([account_number], [local_account_number], [label], [debit_plus], [type_code], [account_category_id], [type]) 
VALUES('6751','57107','Loan Loss',1,'LOAN_LOSS',3, 1)
INSERT INTO [ChartOfAccounts]([account_number], [local_account_number], [label], [debit_plus], [type_code], [account_category_id], [type]) 
VALUES('7021','40260','Interests on cash credit',0,'INTERESTS_ON_CASH_CREDIT',4, 1)
INSERT INTO [ChartOfAccounts]([account_number], [local_account_number], [label], [debit_plus], [type_code], [account_category_id], [type]) 
VALUES('7022','40222','Interests on rescheduled loans',0,'INTERESTS_ON_RESCHEDULED_LOANS',4, 1)
INSERT INTO [ChartOfAccounts]([account_number], [local_account_number], [label], [debit_plus], [type_code], [account_category_id], [type]) 
VALUES('7027','70270','Penalties on past due loans',0,'PENALTIES_ON_PAST_DUE_LOANS_INCOME',4, 1)
INSERT INTO [ChartOfAccounts]([account_number], [local_account_number], [label], [debit_plus], [type_code], [account_category_id], [type]) 
VALUES('7028','40260','Interests on bad loans',0,'INTERESTS_ON_BAD_LOANS',4, 1)
INSERT INTO [ChartOfAccounts]([account_number], [local_account_number], [label], [debit_plus], [type_code], [account_category_id], [type]) 
VALUES('7029','40402','Commissions',0,'COMMISSIONS',4, 1)
INSERT INTO [ChartOfAccounts]([account_number], [local_account_number], [label], [debit_plus], [type_code], [account_category_id], [type])
VALUES('7712','42802','Provision write off',0,'PROVISION_WRITE_OFF',4, 1)
INSERT INTO [ChartOfAccounts]([account_number], [local_account_number], [label], [debit_plus], [type_code], [account_category_id], [type]) 
VALUES('5211','42802','Loan Loss Allowance on Current Loans',0,'LIABILITIES_LOAN_LOSS_CURRENT',2, 1)
INSERT INTO [ChartOfAccounts]([account_number], [local_account_number], [label], [debit_plus], [type_code], [account_category_id], [type]) 
VALUES('6731','42802','Loan Loss Allowance on Current Loans',1,'EXPENSES_LOAN_LOSS_CURRENT',3, 1)
INSERT INTO [ChartOfAccounts]([account_number], [local_account_number], [label], [debit_plus], [type_code], [account_category_id], [type]) 
VALUES('7731','42802','Resumption of Loan Loss allowance on current loans',0,'INCOME_LOAN_LOSS_CURRENT',4, 1)
INSERT INTO [ChartOfAccounts]([account_number], [local_account_number], [label], [debit_plus], [type_code], [account_category_id], [type]) 
VALUES('1322','1322','Accounts and Terms Loans',0,'ACCOUNTS_AND_TERM_LOANS', 2, 1)
INSERT INTO [ChartOfAccounts]([account_number], [local_account_number], [label], [debit_plus], [type_code], [account_category_id], [type]) 
VALUES('221','221','Savings',0,'SAVINGS', 2, 1)
INSERT INTO [ChartOfAccounts]([account_number], [local_account_number], [label], [debit_plus], [type_code], [account_category_id], [type]) 
VALUES('2261','2261','Account payable interests on Savings Books',0,'ACCOUNT_PAYABLE_INTERESTS_ON_SAVINGS_BOOKS',3, 1)
INSERT INTO [ChartOfAccounts]([account_number], [local_account_number], [label], [debit_plus], [type_code], [account_category_id], [type]) 
VALUES('60132','60132','Interests on deposit account',1,'INTERESTS_ON_DEPOSIT_ACCOUNT',3, 1)
INSERT INTO [ChartOfAccounts]([account_number], [local_account_number], [label], [debit_plus], [type_code], [account_category_id], [type]) 
VALUES('42802', '42802','Recovery of charged off assets',0,'RECOVERY_OF_CHARGED_OFF_ASSETS', 3, 1)
INSERT INTO [ChartOfAccounts]([account_number], [local_account_number], [label], [debit_plus], [type_code], [account_category_id], [type]) 
VALUES('9330201', '9330201','NON_BALANCE_COMMITTED_FUNDS', 0,'NON_BALANCE_COMMITTED_FUNDS', 3, 1)
INSERT INTO [ChartOfAccounts]([account_number], [local_account_number], [label], [debit_plus], [type_code], [account_category_id], [type]) 
VALUES('9330202', '9330202','NON_BALANCE_VALIDATED_LOANS', 1,'NON_BALANCE_VALIDATED_LOANS', 1, 1)
INSERT INTO [ChartOfAccounts]([account_number], [local_account_number], [label], [debit_plus], [type_code], [account_category_id], [type]) 
VALUES('222','222','Term Deposit',0,'TERM_DEPOSIT', 2, 1)
INSERT INTO [ChartOfAccounts]([account_number], [local_account_number], [label], [debit_plus], [type_code], [account_category_id], [type]) 
VALUES('223','223','Compulsory Savings',0,'COMPULSORY_SAVINGS', 2, 1)
INSERT INTO [ChartOfAccounts]([account_number], [local_account_number], [label], [debit_plus], [type_code], [account_category_id], [type]) 
VALUES('2262','2262','Account payable interests on Term Deposit',0,'ACCOUNT_PAYABLE_INTERESTS_ON_TERM_DEPOSIT',3, 1)
INSERT INTO [ChartOfAccounts]([account_number], [local_account_number], [label], [debit_plus], [type_code], [account_category_id], [type]) 
VALUES('2263','2263','Account payable interests on Compulsory Savings',0,'ACCOUNT_PAYABLE_INTERESTS_ON_COMPULSORY_SAVINGS',3, 1)
GO

INSERT INTO [AccountsCategory] ([name]) VALUES ('BalanceSheetAsset')
INSERT INTO [AccountsCategory] ([name]) VALUES ('BalanceSheetLiabilities')
INSERT INTO [AccountsCategory] ([name]) VALUES ('ProfitAndLossIncome')
INSERT INTO [AccountsCategory] ([name]) VALUES ('ProfitAndLossExpense')
GO

ALTER TABLE [dbo].[ChartOfAccounts]  WITH CHECK ADD  CONSTRAINT [FK_ChartOfAccounts_AccountsCategory] FOREIGN KEY([account_category_id])
REFERENCES [dbo].[AccountsCategory] ([id])
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EventTypes]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[EventTypes](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[event_type] [nvarchar](4) NOT NULL,
	[description] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_EventTypes_1] PRIMARY KEY CLUSTERED 
(
	[event_type] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EventAttributes]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[EventAttributes](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[event_type] [nvarchar](4) NOT NULL,
	[name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_EventAttributes] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO

ALTER TABLE [dbo].[EventAttributes]  WITH NOCHECK ADD  CONSTRAINT [FK_EventAttributes_EventTypes] FOREIGN KEY([event_type])
REFERENCES [dbo].[EventTypes] ([event_type])
NOT FOR REPLICATION 
GO

ALTER TABLE [dbo].[EventAttributes] CHECK CONSTRAINT [FK_EventAttributes_EventTypes]
GO

INSERT INTO EventTypes (event_type, description) VALUES('LOVE', 'Loan Validation Event')
INSERT INTO EventTypes (event_type, description) VALUES('LODE', 'Loan Disbursement Event')
INSERT INTO EventTypes (event_type, description) VALUES('RGLE', 'Repayment of Good Loan Event')
INSERT INTO EventTypes (event_type, description) VALUES('RBLE', 'Repayment of Bad Loan Event')
INSERT INTO EventTypes (event_type, description) VALUES('PDLE', 'Past Due Loan Event')
INSERT INTO EventTypes (event_type, description) VALUES('WROE', 'Write-Off Even')
INSERT INTO EventTypes (event_type, description) VALUES('ROWE', 'Repayment Over Write-Off')
INSERT INTO EventTypes (event_type, description) VALUES('ROLE', 'Reschedule Of Loan Event')
INSERT INTO EventTypes (event_type, description) VALUES('RRLE', 'Repayment for Rescheduled Loan Event')
INSERT INTO EventTypes (event_type, description) VALUES('TEET', 'Tranche event')
INSERT INTO EventTypes (event_type, description) VALUES('APR', 'Anticipated Partial Repayment')
INSERT INTO EventTypes (event_type, description) VALUES('ATR', 'Anticipated Total Repayment')
INSERT INTO EventTypes (event_type, description) VALUES('APTR', 'Anticipated Partial Total Repayment')
GO

INSERT INTO EventAttributes (event_type, name) VALUES('RRLE', 'principal')
INSERT INTO EventAttributes (event_type, name) VALUES('RRLE', 'penalties')
INSERT INTO EventAttributes (event_type, name) VALUES('RRLE', 'commissions')
INSERT INTO EventAttributes (event_type, name) VALUES('RRLE', 'interests')

INSERT INTO EventAttributes (event_type, name) VALUES('RGLE', 'principal')
INSERT INTO EventAttributes (event_type, name) VALUES('RGLE', 'penalties')
INSERT INTO EventAttributes (event_type, name) VALUES('RGLE', 'commissions')
INSERT INTO EventAttributes (event_type, name) VALUES('RGLE', 'interests')

INSERT INTO EventAttributes (event_type, name) VALUES('RBLE', 'principal')
INSERT INTO EventAttributes (event_type, name) VALUES('RBLE', 'penalties')
INSERT INTO EventAttributes (event_type, name) VALUES('RBLE', 'commissions')
INSERT INTO EventAttributes (event_type, name) VALUES('RBLE', 'interests')

INSERT INTO EventAttributes (event_type, name) VALUES('RRLE', 'principal')
INSERT INTO EventAttributes (event_type, name) VALUES('RRLE', 'penalties')
INSERT INTO EventAttributes (event_type, name) VALUES('RRLE', 'commissions')
INSERT INTO EventAttributes (event_type, name) VALUES('RRLE', 'interests')

INSERT INTO EventAttributes (event_type, name) VALUES('ATR', 'principal')
INSERT INTO EventAttributes (event_type, name) VALUES('ATR', 'penalties')
INSERT INTO EventAttributes (event_type, name) VALUES('ATR', 'commissions')
INSERT INTO EventAttributes (event_type, name) VALUES('ATR', 'interests')

INSERT INTO EventAttributes (event_type, name) VALUES('APR', 'principal')
INSERT INTO EventAttributes (event_type, name) VALUES('APR', 'penalties')
INSERT INTO EventAttributes (event_type, name) VALUES('APR', 'commissions')
INSERT INTO EventAttributes (event_type, name) VALUES('APR', 'interests')

INSERT INTO EventAttributes (event_type, name) VALUES('APTR', 'principal')
INSERT INTO EventAttributes (event_type, name) VALUES('APTR', 'penalties')
INSERT INTO EventAttributes (event_type, name) VALUES('APTR', 'commissions')
INSERT INTO EventAttributes (event_type, name) VALUES('APTR', 'interests')

INSERT INTO EventAttributes (event_type, name) VALUES('LODE', 'amount')
INSERT INTO EventAttributes (event_type, name) VALUES('LODE', 'fees')
INSERT INTO EventAttributes (event_type, name) VALUES('LODE', 'interest')
GO

DELETE FROM ContractAccountingRules
GO
	
DELETE FROM AccountingRules
GO

ALTER TABLE AccountingRules
ADD [debit_account_number] [nvarchar](50) NOT NULL,
	[credit_account_number] [nvarchar](50) NOT NULL,
	[event_type] [nvarchar](4) NOT NULL,
	[event_attribute_id] int NOT NULL
GO
	
ALTER TABLE AccountingRules
DROP COLUMN [generic_account_number],
	 [specific_account_number]
GO 

ALTER TABLE ContractEvents 
ALTER COLUMN event_type NVARCHAR(4) NOT NULL
GO

ALTER TABLE [dbo].[AccountingRules]  WITH NOCHECK ADD  CONSTRAINT [FK_AccountingRules_EventAttributes] FOREIGN KEY([event_attribute_id])
REFERENCES [dbo].[EventAttributes] ([id])
NOT FOR REPLICATION 
GO

ALTER TABLE [dbo].[AccountingRules] CHECK CONSTRAINT [FK_AccountingRules_EventAttributes]
GO

ALTER TABLE [dbo].[AccountingRules]  WITH NOCHECK ADD  CONSTRAINT [FK_AccountingRules_EventTypes] FOREIGN KEY([event_type])
REFERENCES [dbo].[EventTypes] ([event_type])
NOT FOR REPLICATION 
GO

ALTER TABLE [dbo].[AccountingRules] CHECK CONSTRAINT [FK_AccountingRules_EventTypes]
GO

CREATE TABLE [dbo].[TraceUserLogs](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[event_code] [nvarchar](10) NULL,
	[event_date] [datetime] NULL,
	[user_id] [int] NULL,
	[event_description] [nvarchar](50) NULL
) ON [PRIMARY]
GO

/* Savings Book Product */
ALTER TABLE [dbo].[SavingBookProducts]
ADD [deposit_fees] [money] NULL,
	[deposit_fees_max] [money] NULL,
	[deposit_fees_min] [money] NULL,
	[close_fees] [money] NULL,
	[close_fees_max] [money] NULL,
	[close_fees_min] [money] NULL,
	[management_fees] [money] NULL,
	[management_fees_max] [money] NULL,
	[management_fees_min] [money] NULL,
	[management_fees_freq] INT NOT NULL DEFAULT(1)
GO

UPDATE dbo.SavingBookProducts
SET management_fees_freq = (SELECT TOP 1 id FROM
	(
		SELECT id
		FROM dbo.InstallmentTypes
		WHERE name = 'Monthly'

		UNION ALL

		SELECT TOP 1 id
		FROM dbo.InstallmentTypes
	) AS freq
)
GO

/* Savings Book Contracts */
ALTER TABLE [dbo].[SavingBookContracts]
ADD [flat_deposit_fees] [money] NULL,
	[flat_close_fees] [money] NULL,
	[flat_management_fees] [money] NULL
GO

CREATE TABLE [dbo].[ConsolidatedData](
	[branch] [nvarchar](20) NOT NULL,
	[date] [datetime] NOT NULL,
	[olb] [money] NULL,
	[par] [money] NULL,
	[number_of_clients] [int] NULL,
	[number_of_contracts] [int] NULL,
	[disbursements_amount] [money] NULL,
	[disbursements_fees] [money] NULL,
	[repayments_principal] [money] NULL,
	[repayments_interest] [money] NULL,
	[repayments_commissions] [money] NULL,
	[repayments_penalties] [money] NULL,
 CONSTRAINT [IX_ConsolidatedData] UNIQUE NONCLUSTERED 
(
	[branch] ASC,
	[date] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[PersonsPhotos](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[person_id] [int] NOT NULL,
	[picture_id] [int] NOT NULL
) ON [PRIMARY]

ALTER TABLE [dbo].[Pictures]
ADD [picture_id] [int] IDENTITY(1,1) NOT NULL
GO

CREATE TABLE [dbo].[ClientTypes](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[type_name] [nvarchar](50) NOT NULL
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[PackagesClientTypes](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[client_type_id] [int] NOT NULL,
	[package_id] [int] NOT NULL
) ON [PRIMARY]
GO

INSERT INTO [dbo].[ClientTypes] ([type_name])  VALUES ('All')
INSERT INTO [dbo].[ClientTypes] ([type_name])  VALUES ('Group')
INSERT INTO [dbo].[ClientTypes] ([type_name])  VALUES ('Individual')
INSERT INTO [dbo].[ClientTypes] ([type_name])  VALUES ('Corporate')
INSERT INTO [dbo].[ClientTypes] ([type_name])  VALUES ('Village')
GO

CREATE TABLE [dbo].[SavingProductsClientTypes](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[saving_product_id] [int] NOT NULL,
	[client_type_id] [int] NOT NULL
) ON [PRIMARY]

GO

DECLARE @id INT, @client_type NCHAR(1)

DECLARE PackagesCursor CURSOR FOR
SELECT [id], [client_type]
FROM [dbo].[Packages]

OPEN PackagesCursor 
FETCH NEXT FROM PackagesCursor  INTO
@id, @client_type
WHILE @@FETCH_STATUS=0

BEGIN
	
	IF @client_type='-' 
	BEGIN
	 
				INSERT INTO [dbo].[PackagesClientTypes] 
				([package_id],[client_type_id])
				VALUES (@id, 1)
				INSERT INTO [dbo].[PackagesClientTypes] 
				([package_id],[client_type_id])
				VALUES (@id, 2)
				INSERT INTO [dbo].[PackagesClientTypes]
				([package_id],[client_type_id])
				VALUES (@id, 3)
				INSERT INTO [dbo].[PackagesClientTypes] 
				([package_id],[client_type_id])
				VALUES (@id, 4)
				INSERT INTO [dbo].[PackagesClientTypes] 
				([package_id],[client_type_id])
				VALUES (@id, 5)
	END
	
	ELSE IF @client_type='I' 
	BEGIN 
	   INSERT INTO [dbo].[PackagesClientTypes]
	  ([package_id],[client_type_id])
		VALUES (@id, 3)
	END
	
	ELSE IF @client_type='G' 
	BEGIN
		INSERT INTO [dbo].[PackagesClientTypes]
		([package_id],[client_type_id])
		VALUES (@id, 2)
	END
	
	ELSE IF @client_type='C'
	BEGIN 
		INSERT INTO [dbo].[PackagesClientTypes]
		([package_id],[client_type_id])
		VALUES (@id, 4)
	END		
	
	ELSE IF @client_type='V'
	BEGIN 
		INSERT INTO [dbo].[PackagesClientTypes]
		([package_id],[client_type_id])
		VALUES (@id, 5)
	END		
	
FETCH NEXT FROM PackagesCursor INTO
@id,  @client_type				
END		
CLOSE 	PackagesCursor
DEALLOCATE PackagesCursor	
GO

DECLARE @id INT, @client_type NCHAR(1)

DECLARE SavingsProductsCursor CURSOR FOR
SELECT [id], [client_type]
FROM [dbo].[SavingProducts]

OPEN SavingsProductsCursor 
FETCH NEXT FROM SavingsProductsCursor INTO
@id, @client_type
WHILE @@FETCH_STATUS=0
BEGIN
	
	IF @client_type='-' 
	BEGIN
	 	INSERT INTO [dbo].[SavingProductsClientTypes] 
		([saving_product_id],[client_type_id])
		VALUES (@id, 1)
		INSERT INTO [dbo].[SavingProductsClientTypes] 
		([saving_product_id],[client_type_id])
		VALUES (@id, 2)
		INSERT INTO [dbo].[SavingProductsClientTypes] 
		([saving_product_id],[client_type_id])
		VALUES (@id, 3)
		INSERT INTO [dbo].[SavingProductsClientTypes] 
		([saving_product_id],[client_type_id])
		VALUES (@id, 4)
		INSERT INTO [dbo].[SavingProductsClientTypes] 
		([saving_product_id],[client_type_id])
		VALUES (@id, 5)
	END

	ELSE IF @client_type='I' 
	BEGIN 
	   INSERT INTO [dbo].[SavingProductsClientTypes] 
	  ([saving_product_id],[client_type_id])
		VALUES (@id, 3)
	END
	
	ELSE IF @client_type='G' 
	BEGIN
		INSERT INTO [dbo].[SavingProductsClientTypes] 
		([saving_product_id],[client_type_id])
		VALUES (@id, 2)
	END
	
	ELSE IF @client_type='C'
	BEGIN 
		INSERT INTO [dbo].[SavingProductsClientTypes] 
		([saving_product_id],[client_type_id])
		VALUES (@id, 4)
	END		
	
FETCH NEXT FROM SavingsProductsCursor INTO
@id,  @client_type				
END		
CLOSE 	SavingsProductsCursor
DEALLOCATE SavingsProductsCursor	
GO
UPDATE [TechnicalParameters] SET [value] = 'v2.8.12' WHERE [name] = 'VERSION'
GO
