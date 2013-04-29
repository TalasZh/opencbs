ALTER TABLE dbo.SavingContracts
ADD savings_officer_id INT NOT NULL DEFAULT(1)
GO

ALTER TABLE dbo.ContractAccountingRules
ADD currency_id INT NULL
GO

DELETE FROM GeneralParameters WHERE [key]='ALIGN_INSTALLMENTS_ON_REAL_DISBURSEMENT_DATE'
GO 
DELETE FROM GeneralParameters WHERE [key]='CURRENCY_CODE'
GO 

ALTER TABLE dbo.SavingBookProducts
ADD	[cheque_deposit_min] [money] NULL,
	[cheque_deposit_max] [money] NULL,
	[cheque_deposit_fees] [money] NULL,
	[cheque_deposit_fees_min] [money] NULL,
	[cheque_deposit_fees_max] [money] NULL,
	[reopen_fees] [money] NULL,
	[reopen_fees_min] [money] NULL,
	[reopen_fees_max] [money] NULL
GO

ALTER TABLE [dbo].[SavingBookContracts]
ADD [cheque_deposit_fees] [money] NULL,
	[flat_reopen_fees] [money] NULL
GO

UPDATE dbo.SavingBookProducts
SET reopen_fees = 0
WHERE (reopen_fees IS NULL) AND (reopen_fees_min IS NULL) AND (reopen_fees_max IS NULL)

UPDATE dbo.SavingBookContracts
SET flat_reopen_fees = 0
WHERE flat_reopen_fees IS NULL

ALTER TABLE dbo.Villages
ADD	[meeting_day] [int] NULL
GO

ALTER TABLE dbo.Groups
ADD	[meeting_day] [int] NULL
GO

INSERT INTO dbo.EventTypes (event_type, description, sort_order) 
VALUES ('GLRL', 'Late Loan Rescheduled', 360)
GO
INSERT INTO dbo.EventTypes (event_type, description, sort_order) 
VALUES ('BLRL', 'Bad Loan Rescheduled', 370)
GO
INSERT INTO dbo.EventTypes (event_type, description, sort_order) 
VALUES ('LLRL', 'Late Loan Rescheduled', 380)
GO

INSERT INTO EventAttributes (event_type, name) 
VALUES('GLRL', 'olb')
GO
INSERT INTO EventAttributes (event_type, name) 
VALUES('LLRL', 'olb')
GO
INSERT INTO EventAttributes (event_type, name) 
VALUES('BLRL', 'olb')
GO

ALTER TABLE dbo.Packages
ADD	[use_compulsory_savings] [bit] NOT NULL DEFAULT ((0)),
	[compulsory_amount] [int] NULL,
	[compulsory_amount_min] [int] NULL,
	[compulsory_amount_max] [int] NULL
GO

DECLARE DepositCursor CURSOR FOR
SELECT id, deposit_min, deposit_max
FROM [dbo].[SavingProducts]
  
DECLARE @id INT, @deposit_min MONEY, @deposit_max MONEY
  
OPEN DepositCursor
FETCH NEXT FROM DepositCursor INTO 
@id, @deposit_min, @deposit_max
WHILE @@FETCH_STATUS=0
BEGIN
	UPDATE [dbo].[SavingBookProducts]
	SET [cheque_deposit_max]=@deposit_max,
		[cheque_deposit_min]=@deposit_min
	WHERE [id]=@id
	
	FETCH NEXT FROM DepositCursor INTO
	@id, @deposit_min, @deposit_max
END
CLOSE 	DepositCursor
DEALLOCATE DepositCursor

DECLARE DepositFeesCursor CURSOR FOR
SELECT 	id, deposit_fees_min, deposit_fees_max, deposit_fees
FROM [dbo].[SavingBookProducts]

DECLARE @deposit_fees_min MONEY, @deposit_fees_max MONEY, @deposit_fees MONEY

OPEN DepositFeesCursor
FETCH NEXT FROM DepositFeesCursor INTO
@id, @deposit_fees_min, @deposit_fees_max, @deposit_fees

WHILE @@FETCH_STATUS=0
BEGIN
UPDATE [dbo].[SavingBookProducts]
SET		[cheque_deposit_fees]=@deposit_fees
      ,[cheque_deposit_fees_min]=@deposit_fees_min
      ,[cheque_deposit_fees_max]=@deposit_fees_max
      WHERE id=@id
FETCH NEXT FROM DepositFeesCursor INTO
@id, @deposit_fees_min, @deposit_fees_max, @deposit_fees
END
CLOSE DepositFeesCursor
DEALLOCATE DepositFeesCursor
GO

INSERT INTO dbo.EventTypes (event_type, description, sort_order) 
VALUES ('CSUE', 'Client save/update event', 410)
GO

DROP TABLE dbo.ContractAccountsBalance
GO

ALTER  TABLE Persons
ADD loan_officer_id INT NULL
GO

ALTER TABLE Groups
ADD loan_officer_id INT NULL
GO

ALTER TABLE Corporates
ADD loan_officer_id INT NULL
GO

CREATE TABLE [dbo].[ManualAccountingMovements](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[debit_account_number_id] [int] NOT NULL,
	[credit_account_number_id] [int] NOT NULL,
	[amount] [money] NOT NULL,
	[transaction_date] [datetime] NOT NULL,
	[export_date] [datetime] NULL,
	[is_exported] [bit] NOT NULL,
	[currency_id] [int] NOT NULL,
	[exchange_rate] [float] NOT NULL,
	[description] [nvarchar](500) NULL,
 CONSTRAINT [PK_ManualAccountingMovements] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[ManualAccountingMovements] ADD  DEFAULT ((1)) FOR [exchange_rate]
GO

-- Migrating CS Product values to Loan Product values
UPDATE dbo.Packages
SET compulsory_amount = Sav.loan_amount, 
	compulsory_amount_min = Sav.loan_amount_min,
	compulsory_amount_max = Sav.loan_amount_max,
	use_compulsory_savings = 1
FROM dbo.Packages AS Pack
INNER JOIN
(SELECT DISTINCT
Cr.package_id AS [loan_product_id],
ComSavPr.loan_amount, ComSavPr.loan_amount_min, ComSavPr.loan_amount_max
FROM dbo.SavingContracts AS SavCont
INNER JOIN dbo.CompulsorySavingsContracts AS ComSavCont ON SavCont.id = ComSavCont.id
INNER JOIN dbo.CompulsorySavingsProducts AS ComSavPr ON SavCont.product_id = ComSavPr.id
INNER JOIN dbo.Credit AS Cr ON ComSavCont.loan_id = Cr.id
) AS Sav ON Pack.id = Sav.loan_product_id
GO

-- New table for storing Loans and SB Links
CREATE TABLE [dbo].[LoansLinkSavingsBook]
(
[id] [int] IDENTITY(1,1) NOT NULL,
[loan_id] [int] NULL UNIQUE,
[savings_id] [int] NULL,
[loan_percentage] [int] NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[LoansLinkSavingsBook] ADD CONSTRAINT [PK_LoansLinkSavingsBook] PRIMARY KEY CLUSTERED ([id]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[LoansLinkSavingsBook] ADD CONSTRAINT [FK_LoansLinkSavingsBook_Contracts] FOREIGN KEY ([loan_id]) REFERENCES [dbo].[Contracts] ([id])
GO
ALTER TABLE [dbo].[LoansLinkSavingsBook] ADD CONSTRAINT [FK_LoansLinkSavingsBook_SavingContracts] FOREIGN KEY ([savings_id]) REFERENCES [dbo].[SavingContracts] ([id])
GO

-- Migrate from older CS Contracts table
INSERT INTO LoansLinkSavingsBook 
(loan_id, savings_id, loan_percentage)
SELECT loan_id, MAX(id) AS id, 0
FROM dbo.CompulsorySavingsContracts
WHERE loan_id IS NOT NULL
GROUP BY loan_id
GO

-- Changing Savings Product type from CS to SB
INSERT INTO dbo.SavingBookProducts
        ( id ,
          interest_base ,
          interest_frequency ,
          calcul_amount_base ,
          withdraw_fees_type ,
          flat_withdraw_fees_min ,
          flat_withdraw_fees_max ,
          flat_withdraw_fees ,
          rate_withdraw_fees_min ,
          rate_withdraw_fees_max ,
          rate_withdraw_fees ,
          transfer_fees_type ,
          flat_transfer_fees_min ,
          flat_transfer_fees_max ,
          flat_transfer_fees ,
          rate_transfer_fees_min ,
          rate_transfer_fees_max ,
          rate_transfer_fees ,
          deposit_fees ,
          deposit_fees_max ,
          deposit_fees_min ,
          close_fees ,
          close_fees_max ,
          close_fees_min ,
          management_fees ,
          management_fees_max ,
          management_fees_min ,
          management_fees_freq ,
          overdraft_fees ,
          overdraft_fees_max ,
          overdraft_fees_min ,
          agio_fees ,
          agio_fees_max ,
          agio_fees_min ,
          agio_fees_freq ,
          cheque_deposit_min ,
          cheque_deposit_max ,
          cheque_deposit_fees ,
          cheque_deposit_fees_min ,
          cheque_deposit_fees_max ,
          reopen_fees ,
          reopen_fees_min ,
          reopen_fees_max
        )
SELECT CompulsorySavingsProducts.id,         
		  --0 , -- id - int
          10 , -- interest_base - smallint
          10 , -- interest_frequency - smallint
          0 , -- calcul_amount_base - smallint
          1 , -- withdraw_fees_type - smallint
          NULL , -- flat_withdraw_fees_min - money
          NULL , -- flat_withdraw_fees_max - money
          0 , -- flat_withdraw_fees - money
          NULL , -- rate_withdraw_fees_min - float
          NULL , -- rate_withdraw_fees_max - float
          NULL , -- rate_withdraw_fees - float
          1 , -- transfer_fees_type - smallint
          NULL , -- flat_transfer_fees_min - money
          NULL , -- flat_transfer_fees_max - money
          0 , -- flat_transfer_fees - money
          NULL , -- rate_transfer_fees_min - float
          NULL , -- rate_transfer_fees_max - float
          NULL , -- rate_transfer_fees - float
          0 , -- deposit_fees - money
          NULL , -- deposit_fees_max - money
          NULL , -- deposit_fees_min - money
          0 , -- close_fees - money
          NULL , -- close_fees_max - money
          NULL , -- close_fees_min - money
          0 , -- management_fees - money
          NULL , -- management_fees_max - money
          NULL , -- management_fees_min - money
          1 , -- management_fees_freq - int
          0 , -- overdraft_fees - money
          NULL , -- overdraft_fees_max - money
          NULL , -- overdraft_fees_min - money
          0.0 , -- agio_fees - float
          NULL , -- agio_fees_max - float
          NULL , -- agio_fees_min - float
          1 , -- agio_fees_freq - int
          SavingProducts.deposit_min , -- cheque_deposit_min - money
          SavingProducts.deposit_max , -- cheque_deposit_max - money
          0 , -- cheque_deposit_fees - money
          NULL , -- cheque_deposit_fees_min - money
          NULL , -- cheque_deposit_fees_max - money
          0 , -- reopen_fees - money
          NULL , -- reopen_fees_min - money
          NULL  -- reopen_fees_max - money
FROM dbo.CompulsorySavingsProducts, dbo.SavingProducts
GO

UPDATE dbo.SavingProducts
SET transfer_min = withdraw_min, transfer_max = withdraw_max
WHERE product_type = 'C'
GO

INSERT INTO dbo.SavingBookContracts
        ( id ,
          flat_withdraw_fees ,
          rate_withdraw_fees ,
          flat_transfer_fees ,
          rate_transfer_fees ,
          flat_deposit_fees ,
          flat_close_fees ,
          flat_management_fees ,
          flat_overdraft_fees ,
          in_overdraft ,
          rate_agio_fees ,
          cheque_deposit_fees ,
          flat_reopen_fees
        )
SELECT 	  SavingContracts.id , -- id - int
		  CASE WHEN flat_withdraw_fees IS NOT NULL THEN flat_withdraw_fees ELSE flat_withdraw_fees_min END, -- flat_withdraw_fees - money
          CASE WHEN rate_withdraw_fees IS NOT NULL THEN rate_withdraw_fees ELSE rate_withdraw_fees_min END, -- rate_withdraw_fees - float
          CASE WHEN flat_transfer_fees IS NOT NULL THEN flat_transfer_fees ELSE flat_transfer_fees_min END, -- flat_transfer_fees - money
          CASE WHEN rate_transfer_fees IS NOT NULL THEN rate_transfer_fees ELSE rate_transfer_fees_min END, -- rate_transfer_fees - float
          CASE WHEN deposit_fees IS NOT NULL THEN deposit_fees ELSE deposit_fees_min END, -- flat_deposit_fees - money
          CASE WHEN close_fees IS NOT NULL THEN close_fees ELSE close_fees_min END, -- flat_close_fees - money
          CASE WHEN management_fees IS NOT NULL THEN management_fees ELSE management_fees_min END, -- flat_management_fees - money
          CASE WHEN overdraft_fees IS NOT NULL THEN overdraft_fees ELSE overdraft_fees_min END, -- flat_overdraft_fees - money
          0, -- in_overdraft - bit
          CASE WHEN agio_fees IS NOT NULL THEN agio_fees ELSE agio_fees_min END, -- rate_agio_fees - float
          CASE WHEN cheque_deposit_fees IS NOT NULL THEN cheque_deposit_fees ELSE cheque_deposit_fees_min END, -- cheque_deposit_fees - money
          CASE WHEN reopen_fees IS NOT NULL THEN reopen_fees ELSE reopen_fees_min END -- flat_reopen_fees - money
FROM dbo.SavingContracts
INNER JOIN dbo.SavingBookProducts ON dbo.SavingBookProducts.id = dbo.SavingContracts.product_id
INNER JOIN dbo.SavingProducts ON dbo.SavingProducts.id = dbo.SavingBookProducts.id
WHERE SavingProducts.product_type = 'C'
GO

UPDATE dbo.SavingProducts
SET product_type = 'B'
WHERE product_type = 'C'
GO

INSERT INTO [dbo].[ActionItems]
([class_name], [method_name])
VALUES ('SavingServices', 'AllowOperationsDuringPendingDeposit')
GO

INSERT INTO dbo.EventTypes (event_type, description, sort_order) 
VALUES ('SVRE', 'Savings reopen event', 380)
GO

INSERT INTO EventAttributes (event_type, name) 
VALUES('SVRE', 'amount')
GO

INSERT INTO EventAttributes (event_type, name) 
VALUES('SVRE', 'fees')
GO

INSERT INTO dbo.EventTypes (event_type, description, sort_order) 
VALUES ('SPDE', 'Saving pending deposit event', 380)
GO

INSERT INTO EventAttributes (event_type, name) 
VALUES('SPDE', 'amount')
GO

INSERT INTO EventAttributes (event_type, name) 
VALUES('SPDE', 'fees')
GO

DELETE FROM [dbo].[MenuItems]
WHERE menu_name LIKE 'Synchronize'
GO

DELETE FROM [dbo].[MenuItems]
WHERE menu_name LIKE 'MFI Information'
GO

DELETE FROM [dbo].[MenuItems]
WHERE menu_name LIKE 'BabyLoan'
GO

UPDATE  dbo.Contracts
SET     close_date = d.event_date
FROM    ( SELECT    MAX(ce.event_date) AS event_date ,
                    c.id
          FROM      dbo.Contracts c
                    INNER JOIN dbo.ContractEvents ce ON c.id = ce.contract_id
                    INNER JOIN dbo.RepaymentEvents re ON ce.id = re.id
          WHERE     ce.event_date > c.close_date
                    AND closed = 1
          GROUP BY c.id
        ) AS d
WHERE   Contracts.id = d.id
GO

UPDATE [TechnicalParameters] SET [value] = 'v2.8.14' WHERE [name] = 'VERSION'
GO
