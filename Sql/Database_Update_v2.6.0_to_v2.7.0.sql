/*** Savings ***/

/**Add savings account in Accounts**/

DECLARE @currency_id INT
DECLARE Currency_Cursor CURSOR FOR
SELECT [id] 
FROM [dbo].[Currencies]
OPEN Currency_Cursor
FETCH NEXT FROM Currency_Cursor INTO @currency_id
WHILE @@FETCH_STATUS = 0
   BEGIN
		INSERT INTO [Accounts]([account_number], [local_account_number], [label], [balance], [debit_plus], [type_code], [description], [type], [currency_id]) VALUES('2551','2551','Savings',0,0,'SAVINGS', 2, 1, @currency_id)
		INSERT INTO [Accounts]([account_number], [local_account_number], [label], [balance], [debit_plus], [type_code], [description], [type], [currency_id]) VALUES('2000','2000','Account payable interests',0,0,'ACCOUNT_PAYABLE_INTERESTS',3, 1, @currency_id)
		INSERT INTO [Accounts]([account_number], [local_account_number], [label], [balance], [debit_plus], [type_code], [description], [type], [currency_id]) VALUES('60132','60132','Interests on deposit account',0,1,'INTERESTS_ON_DEPOSIT_ACCOUNT',3, 1, @currency_id)

		FETCH NEXT FROM Currency_Cursor INTO @currency_id
   END;
CLOSE Currency_Cursor
DEALLOCATE Currency_Cursor
GO

/**Table SavingProducts**/

/*Create columns*/

ALTER TABLE [dbo].[SavingProducts] 
ADD [interest_frequency] [smallint]
GO

ALTER TABLE [dbo].[SavingProducts]
ADD [interest_base] [smallint]
GO

ALTER TABLE [dbo].[SavingProducts]
ADD [calcul_amount_base] [smallint]
GO

ALTER TABLE [dbo].[SavingProducts]
ADD [entry_fees_min] [money]
GO

ALTER TABLE [dbo].[SavingProducts]
ADD [entry_fees_max] [money]
GO

ALTER TABLE [dbo].[SavingProducts]
ADD [entry_fees] [money]
GO

/*Populate new columns*/

UPDATE [dbo].[SavingProducts]
SET interest_frequency = 40, interest_base = 30, calcul_amount_base = 1, entry_fees = 0
GO

ALTER TABLE [dbo].[SavingProducts] 
ALTER COLUMN  [interest_frequency] [smallint] NOT NULL
GO

ALTER TABLE [dbo].[SavingProducts]
ALTER COLUMN [interest_base] [smallint] NOT NULL
GO


/**Table SavingEvents**/


ALTER TABLE [dbo].[SavingEvents]
DROP COLUMN [direction]
GO

/**Table SavingContracts**/

ALTER TABLE [dbo].[SavingContracts]
DROP COLUMN [description]
GO

UPDATE [dbo].[SavingContracts]
SET interest_rate = 0
GO


/*** End Saving ***/

ALTER TABLE [dbo].[ReschedulingOfALoanEvents] 
ADD [nb_of_maturity] [int] NOT NULL DEFAULT(0)
GO

ALTER TABLE [dbo].[ReschedulingOfALoanEvents]
ADD [nb_of_grace_period] [int] NOT NULL DEFAULT(0)
GO

ALTER TABLE [dbo].[ReschedulingOfALoanEvents] DROP CONSTRAINT [DF_ReschedulingOfALoanEvents_nb_of_days]
ALTER TABLE [dbo].[ReschedulingOfALoanEvents] DROP CONSTRAINT [DF_ReschedulingOfALoanEvents_nb_of_months]
GO

ALTER TABLE [dbo].[ReschedulingOfALoanEvents]
DROP COLUMN [nb_of_days]
GO

ALTER TABLE [dbo].[ReschedulingOfALoanEvents]
DROP COLUMN [nb_of_months]
GO



/****** Object:  ForeignKey [FK_SavingAccountsBalance_Contracts]    Script Date: 07/09/2009 17:35:56 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_SavingAccountsBalance_SavingContracts]') AND parent_object_id = OBJECT_ID(N'[dbo].[SavingAccountsBalance]'))
ALTER TABLE [dbo].[SavingAccountsBalance] DROP CONSTRAINT [FK_SavingAccountsBalance_SavingContracts]
GO

/****** Object:  Table [dbo].[SavingAccountsBalance]    Script Date: 07/27/2009 15:09:55 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SavingAccountsBalance]') AND type in (N'U'))
DROP TABLE [dbo].[SavingAccountsBalance]
GO

/****** Object:  Table [dbo].[SavingAccountsBalance]    Script Date: 07/27/2009 14:48:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SavingAccountsBalance]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[SavingAccountsBalance](
	[saving_id] [int] NOT NULL,
	[account_number] [nvarchar](50) NOT NULL,
	[balance] [money] NOT NULL,
 CONSTRAINT [PK_SavingAccountsBalance] PRIMARY KEY CLUSTERED 
(
	[saving_id] ASC,
	[account_number] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO


/****** Object:  ForeignKey [FK_SavingAccountsBalance_SavingContracts]    Script Date: 07/09/2009 17:35:56 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_SavingAccountsBalance_SavingContracts]') AND parent_object_id = OBJECT_ID(N'[dbo].[SavingAccountsBalance]'))
ALTER TABLE [dbo].[SavingAccountsBalance]  WITH NOCHECK ADD  CONSTRAINT [FK_SavingAccountsBalance_SavingContracts] FOREIGN KEY([saving_id])
REFERENCES [dbo].[SavingContracts] ([id])
GO
ALTER TABLE [dbo].[SavingAccountsBalance] CHECK CONSTRAINT [FK_SavingAccountsBalance_SavingContracts]
GO

/* Fix problematic groups where establishment_date > 1st contract's start date*/
UPDATE [Groups]
SET [establishment_date] = ( SELECT MIN(Contracts.[start_date])
FROM Contracts INNER JOIN
projects ON projects.id = contracts.[project_id]
WHERE projects.[tiers_id] = Groups.[id]
)
WHERE (([establishment_date] IS NULL) or
( SELECT MIN(Contracts.[start_date])
FROM Contracts INNER JOIN
projects ON projects.id = contracts.[project_id]
WHERE projects.[tiers_id] = Groups.[id]
) < establishment_date)
GO

/* Delete references to internal and external currency*/
DELETE FROM [dbo].[GeneralParameters]
      WHERE [key] LIKE 'INTERNAL_CURRENCY' OR [key] LIKE 'EXTERNAL_CURRENCY'

/*Clean up ExchangeRates table*/
IF(SELECT count(id) FROM Currencies)<2 DELETE FROM ExchangeRates

UPDATE  [TechnicalParameters] SET [value] = 'v2.7.0'
GO

