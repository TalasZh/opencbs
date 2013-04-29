IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[TempCashReceipt]') AND type in (N'U'))
DROP TABLE [TempCashReceipt]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[TempCashReceipt_Members]') AND type in (N'U'))
DROP TABLE [TempCashReceipt_Members]
GO

UPDATE Contracts
SET align_disbursed_date = creation_date
WHERE align_disbursed_date IS NULL
GO

/*** ACCOUNTING ***/

/** ACOUNTING RULES **/

CREATE TABLE [dbo].[AccountingRules](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[generic_account_number] [nvarchar](50) NOT NULL,
	[specific_account_number] [nvarchar](50) NOT NULL,
	[rule_type] [char](1) NOT NULL,
	[deleted] [bit] NOT NULL DEFAULT ((0)),
 CONSTRAINT [PK_AccountingRules] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

CREATE TABLE [dbo].[ContractAccountingRules](
	[id] [int] NOT NULL,
	[product_type] [smallint] NOT NULL,
	[loan_product_id] [int] NULL,
	[guarantee_product_id] [int] NULL,
	[savings_product_id] [int] NULL,
	[client_type] [char](1) NOT NULL,
	[activity_id] [int] NULL,
 CONSTRAINT [PK_ContractAccountingRules] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

CREATE TABLE [dbo].[FundingLineAccountingRules](
	[id] [int] NOT NULL,
	[funding_line_id] [int] NULL,
 CONSTRAINT [PK_FundingLineAccountingRules] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[ContractAccountingRules] WITH CHECK ADD CONSTRAINT [FK_ContractAccountingRules_AccountingRules] FOREIGN KEY([id])
REFERENCES [dbo].[AccountingRules] ([id])
GO

ALTER TABLE [dbo].[ContractAccountingRules]  WITH CHECK ADD  CONSTRAINT [FK_ContractAccountingRules_DomainOfApplications] FOREIGN KEY([activity_id])
REFERENCES [dbo].[DomainOfApplications] ([id])
GO

ALTER TABLE [dbo].[ContractAccountingRules] CHECK CONSTRAINT [FK_ContractAccountingRules_DomainOfApplications]
GO

ALTER TABLE [dbo].[ContractAccountingRules]  WITH CHECK ADD  CONSTRAINT [FK_ContractAccountingRules_GuaranteesPackages] FOREIGN KEY([guarantee_product_id])
REFERENCES [dbo].[GuaranteesPackages] ([id])
GO

ALTER TABLE [dbo].[ContractAccountingRules] CHECK CONSTRAINT [FK_ContractAccountingRules_GuaranteesPackages]
GO

ALTER TABLE [dbo].[ContractAccountingRules]  WITH CHECK ADD  CONSTRAINT [FK_ContractAccountingRules_Packages] FOREIGN KEY([loan_product_id])
REFERENCES [dbo].[Packages] ([id])
GO

ALTER TABLE [dbo].[ContractAccountingRules] CHECK CONSTRAINT [FK_ContractAccountingRules_Packages]
GO

ALTER TABLE [dbo].[ContractAccountingRules]  WITH CHECK ADD  CONSTRAINT [FK_ContractAccountingRules_SavingProducts] FOREIGN KEY([savings_product_id])
REFERENCES [dbo].[SavingProducts] ([id])
GO

ALTER TABLE [dbo].[ContractAccountingRules] CHECK CONSTRAINT [FK_ContractAccountingRules_SavingProducts]
GO

ALTER TABLE [dbo].[FundingLineAccountingRules] WITH CHECK ADD CONSTRAINT [FK_FundingLineAccountingRules_AccountingRules] FOREIGN KEY([id])
REFERENCES [dbo].[AccountingRules] ([id])
GO

ALTER TABLE [dbo].[FundingLineAccountingRules] CHECK CONSTRAINT [FK_FundingLineAccountingRules_AccountingRules]
GO

ALTER TABLE [dbo].[FundingLineAccountingRules] WITH CHECK ADD CONSTRAINT [FK_FundingLineAccountingRules_FundingLine] FOREIGN KEY([funding_line_id])
REFERENCES [dbo].[FundingLines] ([id])
GO

ALTER TABLE [dbo].[FundingLineAccountingRules] CHECK CONSTRAINT [FK_FundingLineAccountingRules_FundingLine]
GO

/** END ACCOUNTING RULES **/

/** OMFS-1575 Merge the 'Individual' and 'Group' accounts **/

UPDATE ContractAccountsBalance
SET balance = balance + (SELECT balance 
						 FROM ContractAccountsBalance C2 
						 WHERE ContractAccountsBalance.contract_id = C2.contract_id 
						 AND C2.account_number = '20312'),
	account_number = '2031'						 
WHERE account_number = '20311'
GO

UPDATE ContractAccountsBalance
SET balance = balance + (SELECT balance 
						 FROM ContractAccountsBalance C2 
						 WHERE ContractAccountsBalance.contract_id = C2.contract_id 
						 AND C2.account_number = '70212'),
	account_number = '7021'
WHERE account_number = '70211'
GO

UPDATE ContractAccountsBalance
SET balance = balance + (SELECT balance 
						 FROM ContractAccountsBalance C2 
						 WHERE ContractAccountsBalance.contract_id = C2.contract_id 
						 AND C2.account_number = '70272'),
	account_number = '7027'
WHERE account_number = '70271'
GO

DELETE FROM ContractAccountsBalance
WHERE account_number IN ('20312', '70212', '70272')
GO

DECLARE @currency_id INT
DECLARE currency_cursor CURSOR FOR
		SELECT id 
		FROM currencies
OPEN currency_cursor
FETCH NEXT FROM currency_cursor INTO @currency_id
WHILE @@FETCH_STATUS = 0
	BEGIN
		UPDATE ElementaryMvts
		SET debit_account_id = (SELECT id FROM Accounts WHERE account_number = '20311' AND currency_id = @currency_id)
		WHERE debit_account_id = (SELECT id FROM Accounts WHERE account_number = '20312' AND currency_id = @currency_id)
		
		UPDATE ElementaryMvts
		SET credit_account_id = (SELECT id FROM Accounts WHERE account_number = '20311' AND currency_id = @currency_id)
		WHERE credit_account_id = (SELECT id FROM Accounts WHERE account_number = '20312' AND currency_id = @currency_id)
		
		UPDATE ElementaryMvts
		SET debit_account_id = (SELECT id FROM Accounts WHERE account_number = '70211' AND currency_id = @currency_id)
		WHERE debit_account_id = (SELECT id FROM Accounts WHERE account_number = '70212' AND currency_id = @currency_id)
		
		UPDATE ElementaryMvts
		SET credit_account_id = (SELECT id FROM Accounts WHERE account_number = '70211' AND currency_id = @currency_id)
		WHERE credit_account_id = (SELECT id FROM Accounts WHERE account_number = '70212' AND currency_id = @currency_id)
		
		UPDATE ElementaryMvts
		SET debit_account_id = (SELECT id FROM Accounts WHERE account_number = '70271' AND currency_id = @currency_id)
		WHERE debit_account_id = (SELECT id FROM Accounts WHERE account_number = '70272' AND currency_id = @currency_id)
		
		UPDATE ElementaryMvts
		SET credit_account_id = (SELECT id FROM Accounts WHERE account_number = '70271' AND currency_id = @currency_id)
		WHERE credit_account_id = (SELECT id FROM Accounts WHERE account_number = '70272' AND currency_id = @currency_id)
		
		UPDATE Accounts 
		SET balance = balance + (SELECT balance 
								 FROM Accounts A2
								 WHERE currency_id = @currency_id 
								 AND A2.account_number = '20312'),
			account_number = '2031',
			label = 'Cash Credit',
			type_code = 'CASH_CREDIT'
		WHERE account_number = '20311'
		AND currency_id = @currency_id
		
		UPDATE Accounts 
		SET balance = balance + (SELECT balance 
								 FROM Accounts A2
								 WHERE currency_id = @currency_id 
								 AND A2.account_number = '70212'),
			account_number = '7021',
			label = 'Interests on cash credit',
			type_code = 'INTERESTS_ON_CASH_CREDIT'
		WHERE account_number = '70211'
		AND currency_id = @currency_id
		
		UPDATE Accounts 
		SET balance = balance + (SELECT balance 
								 FROM Accounts A2
								 WHERE currency_id = @currency_id 
								 AND A2.account_number = '70272'),
			account_number = '7027',
			label = 'Penalties on past due loans',
			type_code = 'PENALTIES_ON_PAST_DUE_LOANS_INCOME'
		WHERE account_number = '70271'		
		AND currency_id = @currency_id										 
	
		FETCH NEXT FROM currency_cursor INTO @currency_id
	END;
CLOSE currency_cursor
DEALLOCATE currency_cursor
GO

DELETE FROM Accounts
WHERE account_number IN ('20312', '70212', '70272')
GO

/** END OMFS-1575 **/

/** OMFS-1593 [Accounting] Separate 2971 'Penalties and Interest on Past Due Loans' into two accounts **/

UPDATE Accounts
SET label = 'Interest on Past Due Loans',
	type_code = 'INTERESTS_ON_PAST_DUE_LOANS'
WHERE account_number = '2971'

DECLARE @currency_id INT
DECLARE currency_cursor CURSOR FOR
		SELECT id 
		FROM currencies
OPEN currency_cursor
FETCH NEXT FROM currency_cursor INTO @currency_id
WHILE @@FETCH_STATUS = 0
	BEGIN
		INSERT INTO [Accounts]([account_number], [local_account_number], [label], [balance], [debit_plus], [type_code], [description], [type], [currency_id]) 
		VALUES('2972','40260','Penalties on Past Due Loans',0,1,'PENALTIES_ON_PAST_DUE_LOANS_ASSET',1, 1, @currency_id)
		
		FETCH NEXT FROM currency_cursor INTO @currency_id
	END;
CLOSE currency_cursor
DEALLOCATE currency_cursor
GO

DECLARE @elementary_mvt_id INT,
		@event_type CHAR(4),
		@movement_set_id INT,
		@debit_account_id INT,
		@debit_account_number VARCHAR,
		@credit_account_id INT,
		@credit_account_number VARCHAR,
		@currency_id INT,
		@amount MONEY,
		@interests MONEY,
		@penalties MONEY
DECLARE elementary_mvts_cursor CURSOR FOR
		SELECT EM.id, CE.id, CE.event_type, EM.debit_account_id, DA.account_number, DA.currency_id, EM.credit_account_id, CA.account_number, EM.amount
		FROM ElementaryMvts EM
		INNER JOIN ContractEvents CE ON CE.id = EM.movement_set_id
		INNER JOIN Accounts CA ON CA.id = EM.credit_account_id
		INNER JOIN Accounts DA ON DA.id = EM.debit_account_id
		WHERE credit_account_id IN (SELECT Accounts.id FROM Accounts WHERE Accounts.account_number = '2971')
		OR debit_account_id IN (SELECT Accounts.id FROM Accounts WHERE Accounts.account_number = '2971')
OPEN elementary_mvts_cursor
FETCH NEXT FROM elementary_mvts_cursor INTO @elementary_mvt_id, @movement_set_id, @event_type, @debit_account_id, @debit_account_number, @currency_id, @credit_account_id, @credit_account_number, @amount
WHILE @@FETCH_STATUS = 0
	BEGIN
		IF @event_type = 'PDLE'
			BEGIN
				IF @credit_account_number = '7027'
					BEGIN
						UPDATE ElementaryMvts
						SET debit_account_id = (SELECT id FROM Accounts WHERE account_number = '2972' AND currency_id = @currency_id)
						WHERE ElementaryMvts.ID = @elementary_mvt_id
					END	
			END
		ELSE
			BEGIN
				SELECT @penalties = ISNULL(RE.fees, WOE.accrued_penalties),
					   @interests = ISNULL(RE.interests, WOE.accrued_interests)
				FROM ContractEvents CE
				LEFT OUTER JOIN RepaymentEvents RE ON RE.id = CE.id
				LEFT OUTER JOIN WriteOffEvents WOE ON WOE.id = CE.id
				WHERE CE.id = @movement_set_id
				
				IF @amount = @penalties
					BEGIN
						UPDATE ElementaryMvts
						SET credit_account_id = (SELECT id FROM Accounts WHERE account_number = '2972' AND currency_id = @currency_id)
						WHERE ElementaryMvts.ID = @elementary_mvt_id
					END
			END
		
		FETCH NEXT FROM elementary_mvts_cursor INTO @elementary_mvt_id, @movement_set_id, @event_type, @debit_account_id, @debit_account_number, @currency_id, @credit_account_id, @credit_account_number, @amount
	END;
CLOSE elementary_mvts_cursor
DEALLOCATE elementary_mvts_cursor
GO

DECLARE @account_id INT,
		@sum_debit MONEY,
		@sum_credit MONEY
DECLARE account_cursor CURSOR FOR 
		SELECT id 
		FROM Accounts
		WHERE account_number IN ('2971', '2972')
OPEN account_cursor
FETCH NEXT FROM account_cursor INTO @account_id
WHILE @@FETCH_STATUS = 0
	BEGIN
		SELECT @sum_credit = SUM(amount)
		FROM ElementaryMvts
		WHERE credit_account_id = @account_id

		SELECT @sum_debit = SUM(amount)
		FROM ElementaryMvts
		WHERE debit_account_id = @account_id
		
		SET @sum_credit = ISNULL(@sum_credit, 0)
		SET @sum_debit = ISNULL(@sum_debit, 0)
		
		UPDATE Accounts
		SET balance = @sum_debit - @sum_credit
		WHERE id = @account_id
		
		FETCH NEXT FROM account_cursor INTO @account_id
	END;
CLOSE account_cursor
DEALLOCATE account_cursor
GO

UPDATE ContractAccountsBalance 
SET balance = ISNULL((SELECT SUM(amount)
                     FROM ElementaryMvts
                     INNER JOIN Accounts ON Accounts.id = ElementaryMvts.debit_account_id 
                     INNER JOIN ContractEvents ON ElementaryMvts.movement_set_id = ContractEvents.id
                     WHERE Accounts.account_number = '2971'
                       AND ContractEvents.contract_id = ContractAccountsBalance.contract_id), 0) -
     ISNULL((SELECT SUM(amount)
                     FROM ElementaryMvts
                     INNER JOIN Accounts ON Accounts.id = ElementaryMvts.credit_account_id 
                     INNER JOIN ContractEvents ON ElementaryMvts.movement_set_id = ContractEvents.id
                     WHERE Accounts.account_number = '2971'
                       AND ContractEvents.contract_id = ContractAccountsBalance.contract_id), 0)  
WHERE account_number = '2971'

UPDATE ContractAccountsBalance 
SET balance = ISNULL((SELECT SUM(amount)
                     FROM ElementaryMvts
                     INNER JOIN Accounts ON Accounts.id = ElementaryMvts.debit_account_id 
                     INNER JOIN ContractEvents ON ElementaryMvts.movement_set_id = ContractEvents.id
                     WHERE Accounts.account_number = '2972'
                       AND ContractEvents.contract_id = ContractAccountsBalance.contract_id), 0) -
     ISNULL((SELECT SUM(amount)
                     FROM ElementaryMvts
                     INNER JOIN Accounts ON Accounts.id = ElementaryMvts.credit_account_id 
                     INNER JOIN ContractEvents ON ElementaryMvts.movement_set_id = ContractEvents.id
                     WHERE Accounts.account_number = '2972'
                       AND ContractEvents.contract_id = ContractAccountsBalance.contract_id), 0)  
WHERE account_number = '2972'

/** END OMFS-1593 **/

/*** END ACCOUNTING ***/

UPDATE Credit
SET nb_of_installment = Inst.num
FROM Credit Cr
LEFT JOIN (SELECT contract_id, COUNT(number) AS num FROM Installments GROUP BY contract_id) Inst
ON Inst.contract_id = Cr.id
WHERE Inst.num <> Cr.nb_of_installment
AND id = Cr.id
GO

ALTER TABLE ContractEvents
ADD entry_date DATETIME DEFAULT(GETDATE()) NULL
GO

UPDATE ContractEvents
SET entry_date = event_date
FROM ContractEvents c
WHERE id = c.id
GO

CREATE TABLE TrancheEvents
(id int NOT NULL,
 interest_rate int NOT NULL,
 amount money NOT NULL,
 maturity int NOT NULL,
 start_date datetime NOT NULL,
 close_date datetime NOT NULL,
 closed bit NOT NULL
 CONSTRAINT [PK_TrancheEvents] PRIMARY KEY CLUSTERED
 ([id] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, 
 IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE Packages
ADD 
  anticipated_partial_repayment_penalties float NULL,
  anticipated_partial_repayment_penalties_min float NULL,
  anticipated_partial_repayment_penalties_max float NULL,
  anticipated_partial_repayment_base smallint DEFAULT(0) NOT NULL,
  anticipated_total_repayment_base smallint DEFAULT(0) NOT NULL
GO 

UPDATE Packages
SET anticipated_total_repayment_base = p.anticipated_repayment_base,
  anticipated_partial_repayment_penalties = anticipated_total_repayment_penalties,
  anticipated_partial_repayment_penalties_min = anticipated_total_repayment_penalties_min,
  anticipated_partial_repayment_penalties_max = anticipated_total_repayment_penalties_max
FROM Packages p
WHERE id = p.id
GO

ALTER TABLE Packages
DROP COLUMN anticipated_repayment_base
GO

ALTER TABLE Packages
ADD
[number_of_drawings_loc] int NULL,
[amount_under_loc] money NULL, 
[amount_under_loc_min] money NULL, 
[amount_under_loc_max] money NULL, 
[maturity_loc] int NULL, 
[maturity_loc_min] int NULL, 
[maturity_loc_max] int NULL
GO

ALTER TABLE Credit
ADD anticipated_partial_repayment_penalties float DEFAULT(0) NULL
GO

UPDATE Credit
SET anticipated_partial_repayment_penalties = anticipated_total_repayment_penalties
FROM Credit c
WHERE id = c.id

ALTER TABLE Credit
ADD
[number_of_drawings_loc] int NULL,
[amount_under_loc] money NULL, 
[amount_under_loc_min] money NULL, 
[amount_under_loc_max] money NULL, 
[maturity_loc] int NULL, 
[maturity_loc_min] int NULL, 
[maturity_loc_max] int NULL
GO

ALTER TABLE dbo.Packages
ADD [entry_fees_percentage] BIT NOT NULL DEFAULT(1)
GO

ALTER TABLE dbo.Credit
ADD [entry_fees_percentage] BIT NOT NULL DEFAULT(1)
GO

ALTER TABLE Banks
ADD [name] NVARCHAR(50) NULL
GO

INSERT INTO [GeneralParameters]([key], [value]) VALUES('CHECK_BIC_CODE', 1)
GO

TRUNCATE TABLE dbo.Questionnaire
GO

UPDATE dbo.GeneralParameters SET [value] = '0' WHERE [key] = 'SENT_QUESTIONNAIRE'
GO

ALTER TABLE dbo.Questionnaire
DROP CONSTRAINT DF_Questionnaire_GrossPortfolio
GO

ALTER TABLE dbo.Questionnaire 
DROP COLUMN GrossPortfolio
GO

ALTER TABLE dbo.Questionnaire
ADD GrossPortfolio NVARCHAR(50) NULL
GO

ALTER TABLE dbo.Questionnaire
DROP CONSTRAINT DF_Questionnaire_NumberOfClients
GO

ALTER TABLE dbo.Questionnaire
DROP COLUMN NumberOfClients
GO

ALTER TABLE dbo.Questionnaire
ADD NumberOfClients NVARCHAR(50) NULL
GO

UPDATE [TechnicalParameters] SET [value] = 'v2.8.5' WHERE [name] = 'VERSION'
GO
