ALTER TABLE dbo.ContractEvents
ADD repayment_type  NVARCHAR(20)
GO

UPDATE dbo.ContractEvents
SET repayment_type = 'StandardPayment'
GO

SELECT Contracts.id, Credit.grace_period
INTO #1
FROM dbo.Installments  
INNER JOIN dbo.Contracts ON dbo.Contracts.id = dbo.Installments.contract_id  
INNER JOIN dbo.Credit ON dbo.Contracts.id = dbo.Credit.id
WHERE dbo.Installments.capital_repayment = 0 AND dbo.Installments.interest_repayment = 0
ORDER BY contract_code

SELECT id, COUNT(id) AS number, grace_period
INTO #2
FROM #1
GROUP BY id, grace_period

DELETE FROM #1
WHERE #1.id IN (SELECT id FROM #2 WHERE grace_period = number)

UPDATE dbo.ContractEvents
SET repayment_type = 'TotalPayment'
WHERE contract_id IN (SELECT id FROM #1)

DROP TABLE #1
DROP TABLE #2
GO
SELECT 
Installments.contract_id, 
Installments.number
INTO #1
FROM dbo.Contracts
INNER JOIN dbo.Installments ON dbo.Installments.contract_id = dbo.Contracts.id
LEFT JOIN dbo.ContractEvents ON dbo.Installments.contract_id = dbo.ContractEvents.contract_id
LEFT JOIN dbo.RepaymentEvents ON dbo.ContractEvents.id = dbo.RepaymentEvents.id AND number = installment_number
WHERE paid_capital - ROUND(paid_capital, 2) > 0

UPDATE dbo.Installments
SET Installments.paid_capital = ROUND(Installments.paid_capital, 2)
FROM #1
WHERE dbo.Installments.contract_id = #1.contract_id
AND dbo.Installments.number = #1.number

DROP TABLE #1
GO
SELECT 
Installments.contract_id, 
Installments.number
INTO #1
FROM dbo.Contracts
INNER JOIN dbo.Installments ON dbo.Installments.contract_id = dbo.Contracts.id
LEFT JOIN dbo.ContractEvents ON dbo.Installments.contract_id = dbo.ContractEvents.contract_id
LEFT JOIN dbo.RepaymentEvents ON dbo.ContractEvents.id = dbo.RepaymentEvents.id AND number = installment_number
WHERE (paid_capital - capital_repayment) > 0
AND paid_capital > 0

UPDATE dbo.Installments
SET Installments.paid_capital = Installments.capital_repayment
FROM #1
WHERE dbo.Installments.contract_id = #1.contract_id
AND dbo.Installments.number = #1.number

DROP TABLE #1
GO

ALTER TABLE dbo.RepaymentEvents 
ADD commissions MONEY NOT NULL DEFAULT (0)
GO

ALTER TABLE dbo.RepaymentEvents 
ADD penalties MONEY NOT NULL DEFAULT (0)
GO

UPDATE RepaymentEvents
SET commissions = 0
GO

UPDATE RepaymentEvents
SET penalties = fees
GO

INSERT INTO [GeneralParameters]([key], [value]) VALUES('ALLOWS_MULTIPLE_GROUPS',0)
GO

INSERT INTO [GeneralParameters]([key], [value]) VALUES('MAX_NUMBER_INSTALLMENT', '100')
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[SavingBookContracts](
	[id] [int] NOT NULL,
	[flat_withdraw_fees] [MONEY] NULL,
	[rate_withdraw_fees] [float] NULL,
 CONSTRAINT [PK_SavingBookContracts] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[SavingBookContracts]  WITH CHECK ADD  CONSTRAINT [FK_SavingBookContract_SavingContracts] FOREIGN KEY([id])
REFERENCES [dbo].[SavingContracts] ([id])
GO

ALTER TABLE [dbo].[SavingBookContracts] CHECK CONSTRAINT [FK_SavingBookContract_SavingContracts]
GO

DECLARE @contract_id INT, 
		@flat_withdraw_fees MONEY,
		@rate_withdraw_fees FLOAT
DECLARE SavingContract_Cursor CURSOR FOR
		SELECT [id], [flat_withdraw_fees], [rate_withdraw_fees]
		FROM [dbo].[SavingContracts]
		WHERE product_id IN (SELECT [id] FROM SavingBookProducts)
OPEN SavingContract_Cursor
FETCH NEXT FROM SavingContract_Cursor INTO @contract_id, @flat_withdraw_fees, @rate_withdraw_fees
WHILE @@FETCH_STATUS = 0
	BEGIN
		INSERT INTO [dbo].[SavingBookContracts] ([id], [flat_withdraw_fees], [rate_withdraw_fees])
		VALUES (@contract_id, @flat_withdraw_fees, @rate_withdraw_fees)
	
		FETCH NEXT FROM SavingContract_Cursor INTO @contract_id, @flat_withdraw_fees, @rate_withdraw_fees
	END;

CLOSE SavingContract_Cursor
DEALLOCATE SavingContract_Cursor
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[SavingDepositContracts](
	[id] [int] NOT NULL,
	[number_periods] [int] NOT NULL,
	[rollover] [smallint] NOT NULL,
	[transfer_account] [nvarchar](50) NULL,
	[withdrawal_fees] [float] NOT NULL, 
	[next_maturity] [datetime] NULL,
 CONSTRAINT [PK_SavingDepositContracts] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[SavingDepositContracts]  WITH CHECK ADD  CONSTRAINT [FK_SavingDepositContract_SavingContracts] FOREIGN KEY([id])
REFERENCES [dbo].[SavingContracts] ([id])
GO

ALTER TABLE [dbo].[SavingDepositContracts] CHECK CONSTRAINT [FK_SavingDepositContract_SavingContracts]
GO

DECLARE @contract_id INT, 
		@number_periods INT
DECLARE SavingContract_Cursor CURSOR FOR
		SELECT [id], [number_periods]
		FROM [dbo].[SavingContracts]
		WHERE product_id IN (SELECT [id] FROM TermDepositProducts)
OPEN SavingContract_Cursor
FETCH NEXT FROM SavingContract_Cursor INTO @contract_id, @number_periods
WHILE @@FETCH_STATUS = 0
	BEGIN
		INSERT INTO [dbo].[SavingDepositContracts] ([id], [number_periods], [rollover], [withdrawal_fees])
		VALUES (@contract_id, @number_periods, 3, 0)
	
		FETCH NEXT FROM SavingContract_Cursor INTO @contract_id, @number_periods
	END;

CLOSE SavingContract_Cursor
DEALLOCATE SavingContract_Cursor
GO

DECLARE @contract_id INT, 
		@number_periods INT, 
		@last_maturity DATETIME,
		@nb_of_months INT,
		@nb_of_days INT,
		@n INT
DECLARE SavingContract_Cursor CURSOR FOR
		SELECT sdc.id, sdc.number_periods, it.nb_of_months, it.nb_of_days, 
			   ISNULL (tableEvents.creation_date, sc.creation_date) as last_maturity
		FROM SavingDepositContracts sdc
		INNER JOIN SavingContracts sc ON sdc.id = sc.id
		INNER JOIN TermDepositProducts tdp ON  tdp.id = sc.product_id
		INNER JOIN InstallmentTypes it ON it.id = tdp.installment_types_id
		LEFT OUTER JOIN (SELECT TOP 1 se.contract_id, se.creation_date 
						 FROM SavingEvents se 
						 WHERE se.code = 'SIPE'
						 ORDER BY se.creation_date DESC) tableEvents ON tableEvents.contract_id = sdc.id 
		WHERE sc.status != 2
OPEN SavingContract_Cursor
FETCH NEXT FROM SavingContract_Cursor INTO @contract_id, @number_periods, @nb_of_months, @nb_of_days, @last_maturity
WHILE @@FETCH_STATUS = 0
	BEGIN
		SET @n = 0
		
		WHILE @n < @number_periods
			BEGIN
				SET @last_maturity = DATEADD(month, @nb_of_months, @last_maturity)
				SET @last_maturity = DATEADD(day, @nb_of_days, @last_maturity)
				SET @n = @n + 1
			END
			
		UPDATE [dbo].[SavingDepositContracts] 
		SET next_maturity = @last_maturity
		WHERE id = @contract_id

		FETCH NEXT FROM SavingContract_Cursor INTO @contract_id, @number_periods, @nb_of_months, @nb_of_days, @last_maturity
	END
CLOSE SavingContract_Cursor
DEALLOCATE SavingContract_Cursor
GO	

ALTER TABLE [dbo].[SavingContracts]
DROP COLUMN number_periods
GO

ALTER TABLE [dbo].[SavingContracts]
DROP COLUMN flat_withdraw_fees
GO

ALTER TABLE [dbo].[SavingContracts]
DROP COLUMN rate_withdraw_fees
GO

ALTER TABLE [dbo].[SavingBookProducts]
ADD [withdraw_fees_type] SMALLINT
GO

ALTER TABLE [dbo].[SavingBookProducts]
ADD [flat_withdraw_fees_min] MONEY
GO

ALTER TABLE [dbo].[SavingBookProducts]
ADD [flat_withdraw_fees_max] MONEY
GO

ALTER TABLE [dbo].[SavingBookProducts]
ADD [flat_withdraw_fees] MONEY
GO

ALTER TABLE [dbo].[SavingBookProducts]
ADD [rate_withdraw_fees_min] FLOAT
GO

ALTER TABLE [dbo].[SavingBookProducts]
ADD [rate_withdraw_fees_max] FLOAT
GO

ALTER TABLE [dbo].[SavingBookProducts]
ADD [rate_withdraw_fees] FLOAT
GO

DECLARE @product_id INT, 
		@withdraw_fees_type SMALLINT,
		@flat_withdraw_fees_min MONEY,
		@flat_withdraw_fees_max MONEY,
		@flat_withdraw_fees MONEY,
		@rate_withdraw_fees_min FLOAT,
		@rate_withdraw_fees_max FLOAT,
		@rate_withdraw_fees FLOAT
DECLARE SavingProduct_Cursor CURSOR FOR
		SELECT [id], [withdraw_fees_type], [flat_withdraw_fees_min], [flat_withdraw_fees_max], [flat_withdraw_fees],
		[rate_withdraw_fees_min], [rate_withdraw_fees_max], [rate_withdraw_fees]
		FROM [dbo].[SavingProducts]
		WHERE product_type = 'B'
OPEN SavingProduct_Cursor
FETCH NEXT FROM SavingProduct_Cursor INTO @product_id, @withdraw_fees_type,	@flat_withdraw_fees_min, @flat_withdraw_fees_max,
		@flat_withdraw_fees, @rate_withdraw_fees_min, @rate_withdraw_fees_max, @rate_withdraw_fees
WHILE @@FETCH_STATUS = 0
	BEGIN
		UPDATE [dbo].[SavingBookProducts] 
		SET [withdraw_fees_type] = @withdraw_fees_type, [flat_withdraw_fees_min] = @flat_withdraw_fees_min, 
			[flat_withdraw_fees_max] = @flat_withdraw_fees_max, [flat_withdraw_fees] = @flat_withdraw_fees,
			[rate_withdraw_fees_min] = @rate_withdraw_fees_min, [rate_withdraw_fees_max] = @rate_withdraw_fees_max,
			[rate_withdraw_fees] = @rate_withdraw_fees
		WHERE id = @product_id
	
		FETCH NEXT FROM SavingProduct_Cursor INTO @product_id, @withdraw_fees_type,	@flat_withdraw_fees_min, @flat_withdraw_fees_max,
		@flat_withdraw_fees, @rate_withdraw_fees_min, @rate_withdraw_fees_max, @rate_withdraw_fees
	END;

CLOSE SavingProduct_Cursor
DEALLOCATE SavingProduct_Cursor
GO

ALTER TABLE [dbo].[SavingBookProducts]
ALTER COLUMN [withdraw_fees_type] SMALLINT NOT NULL
GO

ALTER TABLE [dbo].[SavingProducts]
DROP COLUMN [withdraw_fees_type]
GO

ALTER TABLE [dbo].[SavingProducts]
DROP COLUMN [flat_withdraw_fees_min]
GO

ALTER TABLE [dbo].[SavingProducts]
DROP COLUMN [flat_withdraw_fees_max]
GO

ALTER TABLE [dbo].[SavingProducts]
DROP COLUMN [flat_withdraw_fees]
GO

ALTER TABLE [dbo].[SavingProducts]
DROP COLUMN [rate_withdraw_fees_min]
GO

ALTER TABLE [dbo].[SavingProducts]
DROP COLUMN [rate_withdraw_fees_max]
GO

ALTER TABLE [dbo].[SavingProducts]
DROP COLUMN [rate_withdraw_fees]
GO

ALTER TABLE [dbo].[TermDepositProducts]
ADD [withdrawal_fees_type] SMALLINT
GO

ALTER TABLE [dbo].[TermDepositProducts]
ADD [withdrawal_fees_min] FLOAT
GO

ALTER TABLE [dbo].[TermDepositProducts]
ADD [withdrawal_fees_max] FLOAT
GO

ALTER TABLE [dbo].[TermDepositProducts]
ADD [withdrawal_fees] FLOAT
GO

UPDATE TermDepositProducts
SET withdrawal_fees_type = 0, withdrawal_fees = 0
GO

ALTER TABLE [dbo].[TermDepositProducts]
ALTER COLUMN [withdrawal_fees_type] SMALLINT NOT NULL
GO  

DROP TABLE dbo.Info      

CREATE TABLE [dbo].[Info](
	[ceo] [nvarchar](50) NULL,
	[accountant] [nvarchar](50) NULL,
	[mfi] [nvarchar](50) NULL,
	[branch] [nvarchar](50) NULL,
	[City] [nvarchar](50) NULL,
	[cashier] [nvarchar](50) NULL,
	[branchmanager] [nvarchar](50) NULL,
	[branchadress] [nvarchar](50) NULL,
	[BIK] [nvarchar](50) NULL,
	[INN] [nvarchar](50) NULL,
	[AN] [nvarchar](50) NULL,
	[BranchLicense] [nvarchar](100) NULL,
	[LA] [nvarchar](50) NULL,
	[Superviser] [nvarchar](50) NULL
) ON [PRIMARY]    
 
GO 

CREATE TABLE [dbo].[AlertSettings](
	[parameter] [nvarchar](20) NOT NULL,
	[value] [nvarchar](5) NOT NULL
) ON [PRIMARY]

INSERT INTO [dbo].[AlertSettings] ([parameter], [value]) VALUES('client_name', '1')
INSERT INTO [dbo].[AlertSettings] ([parameter], [value]) VALUES('amount', '1')
INSERT INTO [dbo].[AlertSettings] ([parameter], [value]) VALUES('effect_date', '1')
INSERT INTO [dbo].[AlertSettings] ([parameter], [value]) VALUES('phone_num', '1')
INSERT INTO [dbo].[AlertSettings] ([parameter], [value]) VALUES('address', '1')
INSERT INTO [dbo].[AlertSettings] ([parameter], [value]) VALUES('loan_officer', '1')
INSERT INTO [dbo].[AlertSettings] ([parameter], [value]) VALUES('contract_id', '0')
INSERT INTO [dbo].[AlertSettings] ([parameter], [value]) VALUES('show_only_late_loans', '1')
INSERT INTO [dbo].[AlertSettings] ([parameter], [value]) VALUES('contract_status', '1')
GO 

ALTER TABLE dbo.Contracts   
ADD align_disbursed_date DATETIME NULL   
GO  

UPDATE  [TechnicalParameters] SET [value] = 'v2.8.1'
GO
