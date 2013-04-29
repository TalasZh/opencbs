ALTER TABLE dbo.Installments
ADD [paid_fees] [money] NOT NULL DEFAULT(0)
GO

UPDATE dbo.Installments SET paid_fees = 0
GO

ALTER TABLE dbo.InstallmentsHistoric
ADD paid_fees MONEY DEFAULT(0)
GO

UPDATE dbo.InstallmentsHistoric SET paid_fees = 0
GO

EXECUTE sp_rename N'dbo.GeneralParameters', N'GeneralParameters2', 'OBJECT' 
GO

ALTER TABLE [dbo].[GeneralParameters2] DROP CONSTRAINT [PK_GeneralParameters]
GO

CREATE TABLE [dbo].[GeneralParameters](
	[key] [varchar](50) NOT NULL,
	[value] [nvarchar](200) NULL,
 CONSTRAINT [PK_GeneralParameters] PRIMARY KEY CLUSTERED 
(
	[key] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

INSERT INTO GeneralParameters SELECT [key],[value] FROM GeneralParameters2
GO

DROP TABLE [GeneralParameters2]
GO

CREATE TABLE [dbo].[Currencies](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](100) NOT NULL,
	[is_pivot] [bit] NOT NULL,
	[code] [nvarchar](20) NOT NULL,
 CONSTRAINT [PK_Currencies] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

/*Start of loan share amount migration script*/
DECLARE cur_contracts CURSOR
FOR SELECT c.id, cr.amount, c.start_date, c.close_date, t.id AS group_id
FROM dbo.Contracts AS c
LEFT JOIN dbo.Credit AS cr ON c.id = cr.id
LEFT JOIN dbo.Projects AS j ON c.project_id = j.id
LEFT JOIN dbo.Tiers AS t ON j.tiers_id = t.id
WHERE t.client_type_code = 'G'

DECLARE @contract_id INT
DECLARE @amount MONEY
DECLARE @start_date DATETIME
DECLARE @close_date DATETIME
DECLARE @group_id INT

OPEN cur_contracts
FETCH NEXT FROM cur_contracts INTO @contract_id, @amount, @start_date, @close_date, @group_id

WHILE @@FETCH_STATUS = 0
BEGIN
	DECLARE @sum_of_shares MONEY
	SELECT @sum_of_shares = SUM(loan_share_amount)
	FROM dbo.PersonGroupBelonging AS pgb
	WHERE pgb.group_id = @group_id AND @close_date <= ISNULL(left_date, @close_date)
	
	DECLARE @members INT
	DECLARE @share MONEY
	DECLARE @person_id INT
	DECLARE @i INT
	DECLARE @new_share MONEY
			
	SELECT @members = COUNT(person_id)
	FROM dbo.PersonGroupBelonging AS pgb
	WHERE pgb.group_id = @group_id AND @close_date <= ISNULL(left_date, @close_date)
	
	DECLARE cur_shares CURSOR
	FOR SELECT person_id, loan_share_amount
	FROM dbo.PersonGroupBelonging
	WHERE group_id = @group_id AND @close_date <= ISNULL(left_date, @close_date)
	
	SET @i = 1
	OPEN cur_shares
	FETCH NEXT FROM cur_shares INTO @person_id, @share
	WHILE 0 = @@FETCH_STATUS
	BEGIN
		IF (@sum_of_shares <> @amount)
		BEGIN
			IF (@i < @members)
			BEGIN
				SET @new_share = FLOOR(@amount / @members)
			END
			ELSE
			BEGIN
				SET @new_share = @amount - (@members - 1) * (FLOOR(@amount / @members))
			END
		END
		ELSE
		BEGIN
			SET @new_share = @share
		END
		
		INSERT INTO dbo.LoanShareAmounts (
			person_id,
			group_id,
			contract_id,
			amount
		) VALUES ( 
			@person_id,
			@group_id,
			@contract_id,
			@new_share) 
		FETCH NEXT FROM cur_shares INTO @person_id, @share
		SET @i = @i + 1
	END
	CLOSE cur_shares
	DEALLOCATE cur_shares
	
	FETCH NEXT FROM cur_contracts INTO @contract_id, @amount, @start_date, @close_date, @group_id
END
CLOSE cur_contracts
DEALLOCATE cur_contracts
GO

/*End of loan share amount script*/

/*START OF currency migration script*/

ALTER TABLE dbo.Exchange_rate
ADD [currency_id] [int] 
GO

DECLARE @mode bit
SELECT @mode = CASE WHEN [value] IS NULL OR LEN(value)<2 THEN 1 ELSE 0 END FROM dbo.GeneralParameters WHERE [key] LIKE 'external_currency'

DECLARE @IC NVARCHAR(5)
DECLARE @EC NVARCHAR(5)
SELECT @IC = value FROM dbo.GeneralParameters WHERE [key] LIKE 'internal_currency'
SELECT @EC = value FROM dbo.GeneralParameters WHERE [key] LIKE 'external_currency'

INSERT into dbo.Currencies (code, name, is_pivot)
SELECT
(SELECT [value] from dbo.GeneralParameters where [key] like 'internal_currency'),
(SELECT [value] from dbo.GeneralParameters where [key] like 'internal_currency'),
@mode

IF @mode=0 AND @IC <> @EC
INSERT into dbo.Currencies (code, name, is_pivot)
SELECT
(SELECT [value] from dbo.GeneralParameters where [key] like 'external_currency'),
(SELECT [value] from dbo.GeneralParameters where [key] like 'external_currency'),
1

if @mode = 1
UPDATE dbo.Exchange_rate SET currency_id = 
(SELECT id FROM dbo.CURRENCIES where name = (SELECT [value] FROM dbo.GeneralParameters WHERE [key] LIKE 'internal_currency'))

if @mode = 0
UPDATE dbo.Exchange_rate SET currency_id = (SELECT id FROM dbo.CURRENCIES WHERE name = 
(SELECT [value] from dbo.GeneralParameters where [key] like 'internal_currency')), exchange_rate = 1/exchange_rate
GO

ALTER TABLE dbo.Exchange_rate
ALTER COLUMN currency_id int NOT NULL
GO

ALTER TABLE dbo.FundingLines
ADD [currency_id] [int] 
GO

ALTER TABLE [dbo].[FundingLines]  WITH NOCHECK ADD  CONSTRAINT [FK_FundingLines_Currencies] FOREIGN KEY([currency_id])
REFERENCES [dbo].[Currencies] ([id])
GO

ALTER TABLE dbo.Packages
ADD [currency_id] [int] 
GO

UPDATE dbo.Packages
SET currency_id = (SELECT id FROM dbo.Currencies where name = 
(SELECT [value] from dbo.GeneralParameters where [key] like 'internal_currency'))
GO

ALTER TABLE dbo.Packages
ALTER COLUMN currency_id int NOT NULL
GO

ALTER TABLE [dbo].[Packages]  WITH NOCHECK ADD  CONSTRAINT [FK_Packages_Currencies] FOREIGN KEY([currency_id])
REFERENCES [dbo].[Currencies] ([id])
GO

ALTER TABLE dbo.GuaranteesPackages
ADD [currency_id] [int] 
GO

UPDATE dbo.GuaranteesPackages
Set currency_id = (SELECT id FROM dbo.CURRENCIES where name = 
(SELECT [value] from dbo.GeneralParameters where [key] like 'internal_currency'))
GO

ALTER TABLE dbo.GuaranteesPackages
ALTER COLUMN currency_id int NOT NULL
GO

ALTER TABLE [dbo].[GuaranteesPackages]  WITH NOCHECK ADD CONSTRAINT [FK_GuaranteesPackages_Currencies] FOREIGN KEY([currency_id])
REFERENCES [dbo].[Currencies] ([id])
GO

UPDATE dbo.FundingLines
Set currency_id = (SELECT id FROM dbo.CURRENCIES where name = 
(SELECT [value] from dbo.GeneralParameters where [key] like 'internal_currency'))
GO

ALTER TABLE dbo.FundingLines
ALTER COLUMN currency_id int NOT NULL
GO

EXECUTE sp_rename N'dbo.Exchange_rate', N'ExchangeRates', 'OBJECT' 
GO

ALTER TABLE dbo.Currencies
ADD [is_swapped] [bit] NOT NULL DEFAULT 0 
GO

ALTER TABLE dbo.SavingProducts
ADD [currency_id] [int] 
GO

UPDATE dbo.SavingProducts
Set currency_id = (SELECT id FROM dbo.CURRENCIES where name = 
(SELECT [value] from dbo.GeneralParameters where [key] like 'internal_currency'))
GO

ALTER TABLE dbo.SavingProducts
ALTER COLUMN currency_id int NOT NULL
GO

ALTER TABLE [dbo].SavingProducts  WITH NOCHECK ADD  CONSTRAINT [FK_SavingProducts_Currencies] FOREIGN KEY([currency_id])
REFERENCES [dbo].[Currencies] ([id])
GO

ALTER TABLE dbo.Accounts
ADD currency_id int
GO

UPDATE dbo.Accounts SET currency_id = (SELECT id FROM dbo.CURRENCIES where name = 
(SELECT [value] from dbo.GeneralParameters where [key] like 'internal_currency'))
GO

ALTER TABLE dbo.Accounts
ALTER COLUMN currency_id int NOT NULL
GO

ALTER TABLE [dbo].[Accounts]  WITH NOCHECK ADD  CONSTRAINT [FK_Accounts_Currencies] FOREIGN KEY([currency_id])
REFERENCES [dbo].[Currencies] ([id])
GO

CREATE TABLE [dbo].[InstallmentHistory](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[contract_id] [int] NOT NULL,
	[event_id] [int] NOT NULL,
	[event_date] [datetime] NOT NULL,
	[number] [int] NOT NULL,
	[expected_date] [datetime] NOT NULL,
	[principal] [money] NOT NULL,
	[interest] [money] NOT NULL
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[InstallmentHistory]  WITH CHECK ADD  CONSTRAINT [FK_InstallmentHistory_ContractEvents] FOREIGN KEY([event_id])
REFERENCES [dbo].[ContractEvents] ([id])
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Tconso_temp]') AND type in (N'U'))
DROP TABLE [dbo].[Tconso_temp] 
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[NumberDayPeriod]') AND type in (N'U'))
DROP TABLE [dbo].[NumberDayPeriod]
GO

/*currency migration script from July 1*/
if (SELECT COUNT(id) FROM Currencies) > 1
INSERT INTO [dbo].[Accounts]
           ([account_number]
           ,[local_account_number]
           ,[label]
           ,[balance]
           ,[debit_plus]
           ,[type_code]
           ,[description]
           ,[type]
           ,[currency_id])
     SELECT	[account_number],
					[local_account_number],
		            [label],
					0,
					[debit_plus],
					[type_code],
					[description],
					[type],
					2
			FROM [dbo].[Accounts]

/* continuation of currency migration*/
/****** [PK_Exchange_rate]    Script Date: 07/20/2009 11:28:48 ******/
IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[ExchangeRates]') AND name = N'PK_Exchange_rate')
ALTER TABLE [dbo].[ExchangeRates] DROP CONSTRAINT [PK_Exchange_rate]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[octopus_user].[Pictures]') AND type in (N'U'))
DROP TABLE [octopus_user].[Pictures]
GO

UPDATE  [TechnicalParameters] SET [value] = 'v2.6.0'
GO
