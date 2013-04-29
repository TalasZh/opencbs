/****** Object:  Table [dbo].[SavingBookProducts]    Script Date: 08/31/2009 13:19:13 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[SavingBookProducts](
	[id] [int] NOT NULL,
	[interest_base] [smallint] NOT NULL,
	[interest_frequency] [smallint] NOT NULL,
	[calcul_amount_base] [smallint] NULL,
 CONSTRAINT [PK_SavingBookProducts] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[SavingBookProducts]  WITH CHECK ADD  CONSTRAINT [FK_SavingBookProducts_SavingProducts] FOREIGN KEY([id])
REFERENCES [dbo].[SavingProducts] ([id])
GO

ALTER TABLE [dbo].[SavingBookProducts] CHECK CONSTRAINT [FK_SavingBookProducts_SavingProducts]
GO


/* Populate table */

DECLARE @product_id INT, 
		@interest_base SMALLINT, 
		@interest_frequency SMALLINT, 
		@calcul_amount_base SMALLINT
DECLARE SavingProduct_Cursor CURSOR FOR
		SELECT [id], [interest_base], [interest_frequency], [calcul_amount_base]
		FROM [dbo].[SavingProducts]
OPEN SavingProduct_Cursor
FETCH NEXT FROM SavingProduct_Cursor INTO @product_id, @interest_base, @interest_frequency, @calcul_amount_base
WHILE @@FETCH_STATUS = 0
	BEGIN
		INSERT INTO [dbo].[SavingBookProducts] ([id], [interest_base], [interest_frequency], [calcul_amount_base])
		VALUES (@product_id, @interest_base, @interest_frequency,@calcul_amount_base)
	
		FETCH NEXT FROM SavingProduct_Cursor INTO @product_id, @interest_base, @interest_frequency, @calcul_amount_base
	END;

CLOSE SavingProduct_Cursor
DEALLOCATE SavingProduct_Cursor
GO

/** Table SavingProducts **/

/* Create columns */

ALTER TABLE [dbo].[SavingProducts] 
ADD [product_type] CHAR(1) 
GO

UPDATE [dbo].[SavingProducts]
SET product_type = 'B'
GO

ALTER TABLE [dbo].[SavingProducts] 
ALTER COLUMN  [product_type] CHAR(1) NOT NULL
GO

ALTER TABLE [dbo].[SavingProducts]
ADD [code] NVARCHAR(50)
GO

UPDATE [dbo].[SavingProducts]
SET code = LEFT(name, 50)
GO

ALTER TABLE [dbo].[SavingProducts] 
ALTER COLUMN  [code] NVARCHAR(50) NOT NULL
GO

ALTER TABLE [dbo].[SavingProducts]
ADD [withdraw_fees_type] SMALLINT
GO

ALTER TABLE [dbo].[SavingProducts]
ADD [flat_withdraw_fees_min] MONEY
GO

ALTER TABLE [dbo].[SavingProducts]
ADD [flat_withdraw_fees_max] MONEY
GO

ALTER TABLE [dbo].[SavingProducts]
ADD [flat_withdraw_fees] MONEY
GO

ALTER TABLE [dbo].[SavingProducts]
ADD [rate_withdraw_fees_min] FLOAT
GO

ALTER TABLE [dbo].[SavingProducts]
ADD [rate_withdraw_fees_max] FLOAT
GO

ALTER TABLE [dbo].[SavingProducts]
ADD [rate_withdraw_fees] FLOAT
GO

UPDATE [dbo].[SavingProducts]
SET withdraw_fees_type = 1, flat_withdraw_fees = 0
GO

ALTER TABLE [dbo].[SavingProducts]
ALTER COLUMN [withdraw_fees_type] SMALLINT NOT NULL
GO


ALTER TABLE [dbo].[SavingProducts]
ADD [transfer_min] MONEY
GO

ALTER TABLE [dbo].[SavingProducts]
ADD [transfer_max] MONEY
GO

DECLARE @product_id INT, 
		@withdraw_min MONEY, 
		@withdraw_max MONEY
DECLARE SavingProduct_Cursor CURSOR FOR
		SELECT [id], [withdraw_min], [withdraw_max]
		FROM [dbo].[SavingProducts]
OPEN SavingProduct_Cursor
FETCH NEXT FROM SavingProduct_Cursor INTO @product_id, @withdraw_min, @withdraw_max
WHILE @@FETCH_STATUS = 0
	BEGIN
		UPDATE [dbo].[SavingProducts]
		SET [transfer_min] = @withdraw_min, [transfer_max] = @withdraw_max
		
	
		FETCH NEXT FROM SavingProduct_Cursor INTO @product_id, @withdraw_min, @withdraw_max
	END;

CLOSE SavingProduct_Cursor
DEALLOCATE SavingProduct_Cursor
GO


ALTER TABLE [dbo].[SavingProducts]
ALTER COLUMN [transfer_min] MONEY NOT NULL
GO

ALTER TABLE [dbo].[SavingProducts]
ALTER COLUMN [transfer_max] MONEY NOT NULL
GO


/* Drop columns */

ALTER TABLE [dbo].[SavingProducts]
DROP COLUMN interest_frequency
GO

ALTER TABLE [dbo].[SavingProducts]
DROP COLUMN interest_base
GO

ALTER TABLE [dbo].[SavingProducts]
DROP COLUMN calcul_amount_base
GO

/** Table TermDepositProducts **/

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[TermDepositProducts](
	[id] [int] NOT NULL,
	[installment_types_id] [int] NOT NULL,
	[number_period] [int] NULL,
	[number_period_min] [int] NULL,
	[number_period_max] [int] NULL,
	[interest_frequency] [smallint] NOT NULL,
 CONSTRAINT [PK_TermDepositProducts] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[TermDepositProducts]  WITH CHECK ADD  CONSTRAINT [FK_TermDepositProducts_InstallmentTypes] FOREIGN KEY([installment_types_id])
REFERENCES [dbo].[InstallmentTypes] ([id])
GO

ALTER TABLE [dbo].[TermDepositProducts] CHECK CONSTRAINT [FK_TermDepositProducts_InstallmentTypes]
GO

ALTER TABLE [dbo].[TermDepositProducts]  WITH CHECK ADD  CONSTRAINT [FK_TermDepositProducts_SavingProducts] FOREIGN KEY([id])
REFERENCES [dbo].[SavingProducts] ([id])
GO

ALTER TABLE [dbo].[TermDepositProducts] CHECK CONSTRAINT [FK_TermDepositProducts_SavingProducts]
GO

/** Table SavingContracts **/

ALTER TABLE [dbo].[SavingContracts]
ADD number_periods INT NULL
GO

ALTER TABLE [dbo].[SavingContracts]
ADD [status] [smallint] NULL
GO

UPDATE [dbo].[SavingContracts]
SET [status] = 1
GO

ALTER TABLE [dbo].[SavingContracts]
ALTER COLUMN [status] [smallint] NOT NULL
GO


ALTER TABLE [dbo].[SavingContracts]
ADD flat_withdraw_fees MONEY NULL
GO

ALTER TABLE [dbo].[SavingContracts]
ADD rate_withdraw_fees FLOAT NULL
GO

UPDATE [dbo].[SavingContracts]
SET flat_withdraw_fees = 0
GO

ALTER TABLE [dbo].[SavingContracts]
ADD closed_date DATETIME NULL
GO


/** Table SavingEvents **/

ALTER TABLE [dbo].[SavingEvents]
ADD [related_contract_code] nvarchar(50) NULL
GO

SELECT * INTO tmp_SavingEvents FROM SavingEvents

DROP TABLE SavingEvents

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SavingEvents]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[SavingEvents](
	[id] [int] NOT NULL,
	[user_id] [int] NOT NULL,
	[contract_id] [int] NOT NULL,
	[code] [char](4) NOT NULL,
	[amount] [money] NOT NULL,
	[description] [nvarchar](200) NULL,
	[deleted] [bit] NOT NULL,
	[creation_date] [datetime] NOT NULL,
	[cancelable] [bit] NOT NULL,
	[is_fired] [bit] NOT NULL,
	[related_contract_code] [nvarchar](50) NULL,
 CONSTRAINT [PK_SavingEvents] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

DECLARE @id INT, 
		@user_id INT, 
		@contract_id INT,
		@code CHAR(4),
		@amount MONEY,
		@description NVARCHAR(200),
		@deleted BIT,
		@creation_date DATETIME,
		@cancelable BIT,
		@is_fired BIT
DECLARE SavingEvents_Cursor CURSOR FOR
		SELECT [id], [user_id], [contract_id], [code], [amount], [description], [deleted], [creation_date], [cancelable], [is_fired] 
		FROM [dbo].[tmp_SavingEvents]
OPEN SavingEvents_Cursor
FETCH NEXT FROM SavingEvents_Cursor INTO @id, @user_id, @contract_id, @code, @amount, @description, @deleted, @creation_date, @cancelable, @is_fired
WHILE @@FETCH_STATUS = 0
	BEGIN
		INSERT INTO SavingEvents([id], [user_id], [contract_id], [code], [amount], [description], [deleted], [creation_date], [cancelable], [is_fired])
		VALUES (@id, @user_id, @contract_id, @code, @amount, @description, @deleted, @creation_date, @cancelable, @is_fired)
		
	
		FETCH NEXT FROM SavingEvents_Cursor INTO @id, @user_id, @contract_id, @code, @amount, @description, @deleted, @creation_date, @cancelable, @is_fired
	END;

CLOSE SavingEvents_Cursor
DEALLOCATE SavingEvents_Cursor
GO


DROP TABLE tmp_SavingEvents


/** Table GeneralParameters **/

IF (SELECT COUNT(*) FROM SavingContracts) > 1
	INSERT INTO GeneralParameters ([key],[value]) VALUES ('SAVINGS_CODE_TEMPLATE', 'BC/YY/PC-PS/CN-ID') 
ELSE
	INSERT INTO GeneralParameters ([key],[value]) VALUES ('SAVINGS_CODE_TEMPLATE', 'IC/BC/CS/ID')
GO

INSERT INTO GeneralParameters ([key], [value]) VALUES ('IMF_CODE', 'IMF')
GO

/*** End Savings ***/



/**Add new Account to Accounts**/
DECLARE @currency_id int
DECLARE currency_cursor CURSOR FOR 
SELECT id
FROM [dbo].[Currencies]
ORDER BY id

OPEN currency_cursor

FETCH NEXT FROM currency_cursor 
INTO @currency_id

WHILE @@FETCH_STATUS = 0
BEGIN    

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
     SELECT
           '42802',
           '42802',
           'RECOVERY OF CHARGED OFF ASSETS',
           0,
           0,
     	   'RECOVERY_OF_CHARGED_OFF_ASSETS',
			3,
			1,
			@currency_id
	UNION ALL
	SELECT
          '9330201',
           '9330201',
           'NON_BALANCE_COMMITTED_FUNDS',
           0,
           0,
     	   'NON_BALANCE_COMMITTED_FUNDS',
			3,
			1,
			@currency_id
	UNION ALL
	SELECT
          '9330202',
           '9330202',
           'VALIDATED_LOANS',
           0,
           1,
     	   'VALIDATED_LOANS',
			1,
			1,
			@currency_id

    FETCH NEXT FROM currency_cursor 
    INTO @currency_id
END 
CLOSE currency_cursor
DEALLOCATE currency_cursor

/*Adjust written-off loans to new logic*/
UPDATE Credit SET bad_loan = 0 WHERE written_off = 1
GO



ALTER TABLE dbo.PersonGroupBelonging DROP COLUMN loan_share_amount
GO

ALTER TABLE [dbo].[CustomizableFieldsSettings]
ADD [unique] bit NOT NULL DEFAULT (0)
GO

UPDATE [CustomizableFieldsSettings]
SET [unique] = 0
GO

ALTER TABLE dbo.InstallmentHistory DROP COLUMN event_date
GO

EXECUTE sp_rename N'dbo.InstallmentHistory.[principal]', N'capital_repayment', N'COLUMN'
GO

EXECUTE sp_rename N'dbo.InstallmentHistory.[interest]', N'interest_repayment', N'COLUMN'
GO

ALTER TABLE dbo.InstallmentHistory ADD paid_interest MONEY NOT NULL DEFAULT (0)
GO	

ALTER TABLE dbo.InstallmentHistory ADD paid_capital MONEY NOT NULL DEFAULT (0)
GO

ALTER TABLE dbo.InstallmentHistory ADD paid_fees MONEY NOT NULL DEFAULT (0)
GO

ALTER TABLE dbo.InstallmentHistory ADD fees_unpaid MONEY NOT NULL DEFAULT (0)
GO

ALTER TABLE dbo.InstallmentHistory ADD paid_date DATETIME NULL 
GO

ALTER TABLE dbo.InstallmentHistory ADD delete_date DATETIME NULL
GO

INSERT INTO [GeneralParameters]([key], [value]) VALUES('ENFORCE_ID_PATTERN', 0)
GO
INSERT INTO [GeneralParameters]([key], [value]) VALUES('ID_WILD_CHAR_CHECK', 0)
GO
INSERT INTO [GeneralParameters]([key], [value]) VALUES('ID_PATTERN', '[A-Z]{2}[0-9]{7}')
GO

INSERT INTO InstallmentHistory (contract_id, event_id, number, expected_date, capital_repayment, interest_repayment,
paid_interest, paid_capital, paid_fees, fees_unpaid, paid_date)
SELECT 
t2.contract_id, t2.event_id, t2.number, t2.expected_date,
ISNULL(t1.capital_repayment, t2.capital_repayment) AS capital_repayment,
ISNULL(t1.interest_repayment, t2.interest_repayment) AS interest_repayment,
ISNULL(t1.paid_interest, t2.paid_interest) AS paid_interest,
ISNULL(t1.paid_capital, t2.paid_capital) AS paid_capital,
ISNULL(t1.paid_fees, t2.paid_fees) AS paid_fees,
t2.fees_unpaid, 
CASE WHEN t1.number IS NULL THEN t2.paid_date ELSE t1.paid_date END AS paid_date
FROM InstallmentsHistoric AS t1
RIGHT JOIN
(
	SELECT t.event_id, i.number, t.contract_id, i.expected_date, i.capital_repayment,
	i.interest_repayment, i.paid_interest, i.paid_capital, i.paid_fees, i.fees_unpaid,
	i.paid_date
	FROM installments AS i
	LEFT JOIN 
	(
		SELECT contract_id, event_id
		FROM InstallmentsHistoric
		WHERE event_id IN (SELECT id FROM ContractEvents)
		GROUP BY contract_id, event_id
	) AS t ON t.contract_id = i.contract_id
	WHERE NOT t.event_id IS NULL
) AS t2 ON t1.event_id = t2.event_id AND t1.contract_id = t2.contract_id AND t1.number = t2.number
GO

CREATE TABLE [dbo].[Info](
	[ceo] [nvarchar](50) NULL,
	[accountant] [nvarchar](50) NULL,
	[mfi] [nvarchar](50) NULL,
	[branch] [nvarchar](50) NULL,
	[cashier] [nvarchar](50) NULL,
	[branchmanager] [nvarchar](50) NULL,
	[branchadress] [nvarchar](50) NULL
) ON [PRIMARY]
GO

DELETE FROM dbo.WriteOffEvents
WHERE id IN
(
	SELECT id FROM dbo.ContractEvents
	WHERE contract_id IN
	(
		SELECT id FROM dbo.Credit
		WHERE written_off = 0
	)
	AND event_type = 'WROE'
)
GO

DELETE FROM dbo.ContractEvents
WHERE contract_id IN
(
	SELECT id FROM dbo.Credit
	WHERE written_off = 0
)
AND event_type = 'WROE'
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[delinquent_list_temp]') AND type in (N'U'))
DROP TABLE [delinquent_list_temp]
GO

UPDATE  [TechnicalParameters] SET [value] = 'v2.8.0'
GO
