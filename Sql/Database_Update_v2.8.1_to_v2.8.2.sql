/**** SAVINGS ****/

/*** TABLE SavingBookProducts ***/

/** ADD transfer_fees COLUMNS **/

ALTER TABLE [dbo].[SavingBookProducts]
ADD [transfer_fees_type] SMALLINT
GO

ALTER TABLE [dbo].[SavingBookProducts]
ADD [flat_transfer_fees_min] MONEY
GO

ALTER TABLE [dbo].[SavingBookProducts]
ADD [flat_transfer_fees_max] MONEY
GO

ALTER TABLE [dbo].[SavingBookProducts]
ADD [flat_transfer_fees] MONEY
GO

ALTER TABLE [dbo].[SavingBookProducts]
ADD [rate_transfer_fees_min] FLOAT
GO

ALTER TABLE [dbo].[SavingBookProducts]
ADD [rate_transfer_fees_max] FLOAT
GO

ALTER TABLE [dbo].[SavingBookProducts]
ADD [rate_transfer_fees] FLOAT
GO

UPDATE [dbo].[SavingBookProducts]
SET transfer_fees_type = 1, flat_transfer_fees = 0
GO

ALTER TABLE [dbo].[SavingBookProducts]
ALTER COLUMN [transfer_fees_type] SMALLINT NOT NULL
GO

/*** TABLE CompulsorySavingsProduct ***/

/** CREATE TABLE **/

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CompulsorySavingsProducts](
	[id] [int] NOT NULL,
	[loan_amount_min] [float] NULL,
	[loan_amount_max] [float] NULL,
	[loan_amount] [float] NULL,
 CONSTRAINT [PK_CompulsorySavingsProducts] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


ALTER TABLE [dbo].[CompulsorySavingsProducts]  WITH CHECK ADD  CONSTRAINT [FK_CompulsorySavingsProducts_SavingProducts] FOREIGN KEY([id])
REFERENCES [dbo].[SavingProducts] ([id])
GO

ALTER TABLE [dbo].[CompulsorySavingsProducts] CHECK CONSTRAINT [FK_CompulsorySavingsProducts_SavingProducts]
GO

/*** TABLE CompulsorySavingsContracts ***/

/** CREATE TABLE **/

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CompulsorySavingsContracts](
	[id] [int] NOT NULL,
	[loan_id] [int] NULL
 CONSTRAINT [PK_CompulsorySavingsContracts] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


ALTER TABLE [dbo].[CompulsorySavingsContracts]  WITH CHECK ADD  CONSTRAINT [FK_CompulsorySavingsContracts_SavingContracts] FOREIGN KEY([id])
REFERENCES [dbo].[SavingContracts] ([id])
GO

ALTER TABLE [dbo].[CompulsorySavingsContracts] CHECK CONSTRAINT [FK_CompulsorySavingsContracts_SavingContracts]
GO

ALTER TABLE [dbo].[CompulsorySavingsContracts]  WITH CHECK ADD  CONSTRAINT [FK_CompulsorySavingsContracts_Contracts] FOREIGN KEY([loan_id])
REFERENCES [dbo].[Contracts] ([id])
GO

ALTER TABLE [dbo].[CompulsorySavingsContracts] CHECK CONSTRAINT [FK_CompulsorySavingsContracts_Contracts]
GO

/*** TABLE SavingEvents ***/

/** ADD COLUMN Fees **/

ALTER TABLE [dbo].[SavingEvents]
ADD [fees] [money] NULL
GO

/** Modification on savings fees **/

DECLARE @event_id INT,
		@event_amount MONEY,
		@event_code CHAR(4),
		@parent_event_id INT
DECLARE SavingEvents_Cursor CURSOR FOR
		SELECT [id], [code], [amount]
		FROM [dbo].[SavingEvents]
		WHERE deleted = 0 AND code IN ('SVFE', 'SWFE', 'SCFE')
OPEN SavingEvents_Cursor
FETCH NEXT FROM SavingEvents_Cursor INTO @event_id, @event_code, @event_amount
WHILE @@FETCH_STATUS = 0
	BEGIN
		IF @event_code = 'SVFE'
			BEGIN
				SET @parent_event_id = (SELECT TOP(1) id FROM SavingEvents WHERE id < @event_id AND code = 'SVIE' ORDER BY id DESC)
			END
		ELSE IF @event_code = 'SWFE'
			BEGIN
				SET @parent_event_id = (SELECT TOP(1) id FROM SavingEvents WHERE id < @event_id AND code = 'SVWE' ORDER BY id DESC)
			END
		ELSE IF @event_code = 'SCFE'
			BEGIN
				SET @parent_event_id = (SELECT TOP(1) id FROM SavingEvents WHERE id > @event_id AND code IN ('SVWE', 'SDTE') ORDER BY id ASC)
			END
			
		UPDATE [dbo].[SavingEvents] SET fees = @event_amount
		WHERE id = @parent_event_id
		
		DELETE FROM [dbo].[SavingEvents]
		WHERE id = @event_id
		
		UPDATE [dbo].[ElementaryMvts] SET movement_set_id = @parent_event_id
		WHERE movement_set_id = @event_id
		
		DELETE FROM [dbo].[MovementSet]
		WHERE id = @event_id
		
		FETCH NEXT FROM SavingEvents_Cursor INTO @event_id, @event_code, @event_amount
	END;

CLOSE SavingEvents_Cursor
DEALLOCATE SavingEvents_Cursor
GO

/*** TABLE SavingBookContracts ***/

/** ADD transfer_fees COLUMNS **/

ALTER TABLE [dbo].[SavingBookContracts]
ADD flat_transfer_fees MONEY NULL
GO

ALTER TABLE [dbo].[SavingBookContracts]
ADD rate_transfer_fees FLOAT NULL
GO

UPDATE [dbo].[SavingBookContracts]
SET flat_transfer_fees = 0
GO

/*** ACCOUNTS ***/
DECLARE @saving_id INT,
		@saving_type CHAR(1)
DECLARE Saving_Cursor CURSOR FOR
SELECT sc.[id], [product_type]
FROM [dbo].[SavingContracts] sc
INNER JOIN [dbo].[SavingProducts] ON SavingProducts.id = sc.product_id
OPEN Saving_Cursor
FETCH NEXT FROM Saving_Cursor INTO @saving_id, @saving_type
WHILE @@FETCH_STATUS = 0
	BEGIN
		INSERT INTO [SavingAccountsBalance] (saving_id, account_number, balance) VALUES (@saving_id, '222', 0)
		INSERT INTO [SavingAccountsBalance] (saving_id, account_number, balance) VALUES (@saving_id, '223', 0)
		INSERT INTO [SavingAccountsBalance] (saving_id, account_number, balance) VALUES (@saving_id, '2262', 0)
		INSERT INTO [SavingAccountsBalance] (saving_id, account_number, balance) VALUES (@saving_id, '2263', 0)
		
		UPDATE [SavingAccountsBalance] SET account_number = '221' WHERE account_number = '2551' AND saving_id = @saving_id
		UPDATE [SavingAccountsBalance] SET account_number = '2261' WHERE account_number = '2000' AND saving_id = @saving_id
		
		IF @saving_type != 'B'
			BEGIN
				IF @saving_type = 'T'
					BEGIN
						UPDATE [SavingAccountsBalance] SET balance = (SELECT balance FROM [SavingAccountsBalance] WHERE account_number = '221' AND saving_id = @saving_id)
						WHERE saving_id = @saving_id AND account_number = '222'
					
						UPDATE [SavingAccountsBalance] SET balance = (SELECT balance FROM [SavingAccountsBalance] WHERE account_number = '2261' AND saving_id = @saving_id)
						WHERE saving_id = @saving_id AND account_number = '2262'
					END
				ELSE
					BEGIN
						UPDATE [SavingAccountsBalance] SET balance = (SELECT balance FROM [SavingAccountsBalance] WHERE account_number = '221' AND saving_id = @saving_id)
						WHERE saving_id = @saving_id AND account_number = '223'
					
						UPDATE [SavingAccountsBalance] SET balance = (SELECT balance FROM [SavingAccountsBalance] WHERE account_number = '2261' AND saving_id = @saving_id)
						WHERE saving_id = @saving_id AND account_number = '2263'
					END
					
				UPDATE [SavingAccountsBalance] SET balance = 0
				WHERE saving_id = @saving_id AND account_number = '221'
				
				UPDATE [SavingAccountsBalance] SET balance = 0
				WHERE saving_id = @saving_id AND account_number = '2261'
			END
			
		FETCH NEXT FROM Saving_Cursor INTO @saving_id, @saving_type
	END;
CLOSE Saving_Cursor
DEALLOCATE Saving_Cursor
GO

UPDATE [Accounts]
SET account_number = '221', local_account_number = '221'
WHERE account_number = '2551'
GO

UPDATE [Accounts]
SET account_number = '2261', local_account_number = '2261', label = 'Account payable interests on Savings Books',
type_code = 'ACCOUNT_PAYABLE_INTERESTS_ON_SAVINGS_BOOKS'
WHERE account_number = '2000'
GO

DECLARE @currency_id INT
DECLARE Currency_Cursor CURSOR FOR
SELECT [id] 
FROM [dbo].[Currencies]
OPEN Currency_Cursor
FETCH NEXT FROM Currency_Cursor INTO @currency_id
WHILE @@FETCH_STATUS = 0
   BEGIN
		INSERT INTO [Accounts]([account_number], [local_account_number], [label], [balance], [debit_plus], [type_code], [description], [type], [currency_id]) VALUES('222','222','Term Deposit',0,0,'TERM_DEPOSIT', 2, 1, @currency_id)
		INSERT INTO [Accounts]([account_number], [local_account_number], [label], [balance], [debit_plus], [type_code], [description], [type], [currency_id]) VALUES('223','223','Compulsory Savings',0,0,'COMPULSORY_SAVINGS', 2, 1, @currency_id)

		INSERT INTO [Accounts]([account_number], [local_account_number], [label], [balance], [debit_plus], [type_code], [description], [type], [currency_id]) VALUES('2262','2262','Account payable interests on Term Deposit',0,0,'ACCOUNT_PAYABLE_INTERESTS_ON_TERM_DEPOSIT',3, 1, @currency_id)
		INSERT INTO [Accounts]([account_number], [local_account_number], [label], [balance], [debit_plus], [type_code], [description], [type], [currency_id]) VALUES('2263','2263','Account payable interests on Compulsory Savings',0,0,'ACCOUNT_PAYABLE_INTERESTS_ON_COMPULSORY_SAVINGS',3, 1, @currency_id)

		UPDATE [Accounts] SET balance = ISNULL((SELECT SUM(balance) FROM SavingAccountsBalance WHERE saving_id IN (SELECT sc.id FROM SavingContracts SC INNER JOIN SavingProducts SP ON SC.product_id = SP.id WHERE SP.product_type = 'B' AND SP.currency_id = @currency_id) AND account_number = '221'), 0)
		WHERE account_number = '221' AND currency_id = @currency_id
		
		UPDATE [Accounts] SET balance = ISNULL((SELECT SUM(balance) FROM SavingAccountsBalance WHERE saving_id IN (SELECT sc.id FROM SavingContracts SC INNER JOIN SavingProducts SP ON SC.product_id = SP.id WHERE SP.product_type = 'T' AND SP.currency_id = @currency_id) AND account_number = '222'), 0)
		WHERE account_number = '222' AND currency_id = @currency_id
		
		UPDATE [Accounts] SET balance = ISNULL((SELECT SUM(balance) FROM SavingAccountsBalance WHERE saving_id IN (SELECT sc.id FROM SavingContracts SC INNER JOIN SavingProducts SP ON SC.product_id = SP.id WHERE SP.product_type = 'C' AND SP.currency_id = @currency_id) AND account_number = '223'), 0)
		WHERE account_number = '223' AND currency_id = @currency_id
		
		UPDATE [Accounts] SET balance = ISNULL((SELECT SUM(balance) FROM SavingAccountsBalance WHERE saving_id IN (SELECT sc.id FROM SavingContracts SC INNER JOIN SavingProducts SP ON SC.product_id = SP.id WHERE SP.product_type = 'B' AND SP.currency_id = @currency_id) AND account_number = '2261'), 0)
		WHERE account_number = '2261' AND currency_id = @currency_id
		
		UPDATE [Accounts] SET balance = ISNULL((SELECT SUM(balance) FROM SavingAccountsBalance WHERE saving_id IN (SELECT sc.id FROM SavingContracts SC INNER JOIN SavingProducts SP ON SC.product_id = SP.id WHERE SP.product_type = 'T' AND SP.currency_id = @currency_id) AND account_number = '2262'), 0)
		WHERE account_number = '2262' AND currency_id = @currency_id
		
		UPDATE [Accounts] SET balance = ISNULL((SELECT SUM(balance) FROM SavingAccountsBalance WHERE saving_id IN (SELECT sc.id FROM SavingContracts SC INNER JOIN SavingProducts SP ON SC.product_id = SP.id WHERE SP.product_type = 'C' AND SP.currency_id = @currency_id) AND account_number = '2263'), 0)
		WHERE account_number = '2263' AND currency_id = @currency_id
		
		UPDATE [ElementaryMvts] SET debit_account_id = (SELECT id FROM Accounts WHERE account_number = '221' AND currency_id = @currency_id)
		WHERE debit_account_id = (SELECT id FROM Accounts WHERE account_number = '221' AND currency_id = @currency_id)
		AND movement_set_id IN (SELECT id FROM SavingEvents WHERE contract_id IN (SELECT SC.id FROM SavingContracts SC INNER JOIN SavingProducts ON SC.product_id = SavingProducts.id WHERE SavingProducts.product_type = 'B' AND SavingProducts.currency_id = @currency_id))
		
		UPDATE [ElementaryMvts] SET credit_account_id = (SELECT id FROM Accounts WHERE account_number = '221' AND currency_id = @currency_id)
		WHERE credit_account_id = (SELECT id FROM Accounts WHERE account_number = '221' AND currency_id = @currency_id)
		AND movement_set_id IN (SELECT id FROM SavingEvents WHERE contract_id IN (SELECT SC.id FROM SavingContracts SC INNER JOIN SavingProducts ON SC.product_id = SavingProducts.id WHERE SavingProducts.product_type = 'B' AND SavingProducts.currency_id = @currency_id))
		
		UPDATE [ElementaryMvts] SET debit_account_id = (SELECT id FROM Accounts WHERE account_number = '222' AND currency_id = @currency_id)
		WHERE debit_account_id = (SELECT id FROM Accounts WHERE account_number = '221' AND currency_id = @currency_id)
		AND movement_set_id IN (SELECT id FROM SavingEvents WHERE contract_id IN (SELECT SC.id FROM SavingContracts SC INNER JOIN SavingProducts ON SC.product_id = SavingProducts.id WHERE SavingProducts.product_type = 'T' AND SavingProducts.currency_id = @currency_id))
		
		UPDATE [ElementaryMvts] SET credit_account_id = (SELECT id FROM Accounts WHERE account_number = '222' AND currency_id = @currency_id)
		WHERE credit_account_id = (SELECT id FROM Accounts WHERE account_number = '221' AND currency_id = @currency_id)
		AND movement_set_id IN (SELECT id FROM SavingEvents WHERE contract_id IN (SELECT SC.id FROM SavingContracts SC INNER JOIN SavingProducts ON SC.product_id = SavingProducts.id WHERE SavingProducts.product_type = 'T' AND SavingProducts.currency_id = @currency_id))
		
		UPDATE [ElementaryMvts] SET debit_account_id = (SELECT id FROM Accounts WHERE account_number = '223' AND currency_id = @currency_id)
		WHERE debit_account_id = (SELECT id FROM Accounts WHERE account_number = '221' AND currency_id = @currency_id)
		AND movement_set_id IN (SELECT id FROM SavingEvents WHERE contract_id IN (SELECT SC.id FROM SavingContracts SC INNER JOIN SavingProducts ON SC.product_id = SavingProducts.id WHERE SavingProducts.product_type = 'C' AND SavingProducts.currency_id = @currency_id))
		
		UPDATE [ElementaryMvts] SET credit_account_id = (SELECT id FROM Accounts WHERE account_number = '223' AND currency_id = @currency_id)
		WHERE credit_account_id = (SELECT id FROM Accounts WHERE account_number = '221' AND currency_id = @currency_id)
		AND movement_set_id IN (SELECT id FROM SavingEvents WHERE contract_id IN (SELECT SC.id FROM SavingContracts SC INNER JOIN SavingProducts ON SC.product_id = SavingProducts.id WHERE SavingProducts.product_type = 'C' AND SavingProducts.currency_id = @currency_id))
		
		UPDATE [ElementaryMvts] SET debit_account_id = (SELECT id FROM Accounts WHERE account_number = '2261' AND currency_id = @currency_id)
		WHERE debit_account_id = (SELECT id FROM Accounts WHERE account_number = '2261' AND currency_id = @currency_id)
		AND movement_set_id IN (SELECT id FROM SavingEvents WHERE contract_id IN (SELECT SC.id FROM SavingContracts SC INNER JOIN SavingProducts ON SC.product_id = SavingProducts.id WHERE SavingProducts.product_type = 'B' AND SavingProducts.currency_id = @currency_id))
		
		UPDATE [ElementaryMvts] SET credit_account_id = (SELECT id FROM Accounts WHERE account_number = '2261' AND currency_id = @currency_id)
		WHERE credit_account_id = (SELECT id FROM Accounts WHERE account_number = '2261' AND currency_id = @currency_id)
		AND movement_set_id IN (SELECT id FROM SavingEvents WHERE contract_id IN (SELECT SC.id FROM SavingContracts SC INNER JOIN SavingProducts ON SC.product_id = SavingProducts.id WHERE SavingProducts.product_type = 'B' AND SavingProducts.currency_id = @currency_id))
		
		UPDATE [ElementaryMvts] SET debit_account_id = (SELECT id FROM Accounts WHERE account_number = '2262' AND currency_id = @currency_id)
		WHERE debit_account_id = (SELECT id FROM Accounts WHERE account_number = '2261' AND currency_id = @currency_id)
		AND movement_set_id IN (SELECT id FROM SavingEvents WHERE contract_id IN (SELECT SC.id FROM SavingContracts SC INNER JOIN SavingProducts ON SC.product_id = SavingProducts.id WHERE SavingProducts.product_type = 'T' AND SavingProducts.currency_id = @currency_id))
		
		UPDATE [ElementaryMvts] SET credit_account_id = (SELECT id FROM Accounts WHERE account_number = '2262' AND currency_id = @currency_id)
		WHERE credit_account_id = (SELECT id FROM Accounts WHERE account_number = '2261' AND currency_id = @currency_id)
		AND movement_set_id IN (SELECT id FROM SavingEvents WHERE contract_id IN (SELECT SC.id FROM SavingContracts SC INNER JOIN SavingProducts ON SC.product_id = SavingProducts.id WHERE SavingProducts.product_type = 'T' AND SavingProducts.currency_id = @currency_id))
		
		UPDATE [ElementaryMvts] SET debit_account_id = (SELECT id FROM Accounts WHERE account_number = '2263' AND currency_id = @currency_id)
		WHERE debit_account_id = (SELECT id FROM Accounts WHERE account_number = '2261' AND currency_id = @currency_id)
		AND movement_set_id IN (SELECT id FROM SavingEvents WHERE contract_id IN (SELECT SC.id FROM SavingContracts SC INNER JOIN SavingProducts ON SC.product_id = SavingProducts.id WHERE SavingProducts.product_type = 'C' AND SavingProducts.currency_id = @currency_id))
		
		UPDATE [ElementaryMvts] SET credit_account_id = (SELECT id FROM Accounts WHERE account_number = '2263' AND currency_id = @currency_id)
		WHERE credit_account_id = (SELECT id FROM Accounts WHERE account_number = '2261' AND currency_id = @currency_id)
		AND movement_set_id IN (SELECT id FROM SavingEvents WHERE contract_id IN (SELECT SC.id FROM SavingContracts SC INNER JOIN SavingProducts ON SC.product_id = SavingProducts.id WHERE SavingProducts.product_type = 'C' AND SavingProducts.currency_id = @currency_id))
			
		FETCH NEXT FROM Currency_Cursor INTO @currency_id
   END;
CLOSE Currency_Cursor
DEALLOCATE Currency_Cursor
GO

/*** COMPULSORY SAVINGS ***/

ALTER TABLE Credit
ADD savings_is_mandatory BIT NOT NULL DEFAULT 0
GO

/**** END SAVINGS ****/

IF NOT OBJECT_ID('dbo.ReportLookUpFields') IS NULL
DROP TABLE dbo.ReportLookUpFields
GO

IF NOT OBJECT_ID('dbo.ReportParametrs') IS NULL
DROP TABLE dbo.ReportParametrs
GO

IF NOT OBJECT_ID('dbo.ReportDataObjectSource') IS NULL
DROP TABLE dbo.ReportDataObjectSource
GO

IF NOT OBJECT_ID('dbo.ReportObject') IS NULL
DROP TABLE dbo.ReportObject
GO

ALTER TABLE Packages
ADD rounding_type SMALLINT NOT NULL DEFAULT(1)
GO

UPDATE Packages
SET rounding_type = 1
GO

UPDATE [TechnicalParameters] SET [value] = 'v2.8.2'
GO

EXECUTE sp_rename N'dbo.ReschedulingOfALoanEvents.[nb_of_grace_period]', N'date_offset', N'COLUMN'
GO
