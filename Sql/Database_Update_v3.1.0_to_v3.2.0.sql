ALTER TABLE dbo.Branches
ADD [code] NVARCHAR(20) NULL
, [address] NVARCHAR(255) NULL
, [description] NVARCHAR(255) NULL
GO

-- Teller management
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Tellers]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Tellers](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](100) NOT NULL,
	[desc] [nvarchar](100) NULL,
	[account_id] [int] NOT NULL,
	[deleted] [bit] NOT NULL DEFAULT ((0)),
 CONSTRAINT [PK_Tellers] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Tellers_ChartOfAccounts]') AND parent_object_id = OBJECT_ID(N'[dbo].[Tellers]'))
ALTER TABLE [dbo].[Tellers]  WITH CHECK ADD  CONSTRAINT [FK_Tellers_ChartOfAccounts] FOREIGN KEY([account_id])
REFERENCES [dbo].[ChartOfAccounts] ([id])
GO

ALTER TABLE dbo.SavingBookProducts
ADD is_ibt_fee_flat bit not null default(0)
, ibt_fee_min money null
, ibt_fee_max money null
, ibt_fee money null
GO

ALTER TABLE dbo.SavingBookContracts
ADD flat_ibt_fee MONEY NULL
, rate_ibt_fee FLOAT NULL
GO

INSERT INTO [GeneralParameters]([key], [value]) VALUES('USE_TELLER_MANAGEMENT', 0)
GO

ALTER TABLE dbo.ContractEvents
ADD [teller_id] [int] NULL
GO

ALTER TABLE dbo.SavingEvents
ADD [teller_id] [int] NULL
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ContractEvents_Tellers]') AND parent_object_id = OBJECT_ID(N'[dbo].[ContractEvents]'))
ALTER TABLE [dbo].[ContractEvents]  WITH NOCHECK ADD  CONSTRAINT [FK_ContractEvents_Tellers] FOREIGN KEY([teller_id])
REFERENCES [dbo].[Tellers] ([id])
GO
ALTER TABLE [dbo].[ContractEvents] CHECK CONSTRAINT [FK_ContractEvents_Tellers]
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_SavingEvents_Tellers]') AND parent_object_id = OBJECT_ID(N'[dbo].[SavingEvents]'))
ALTER TABLE [dbo].[SavingEvents]  WITH CHECK ADD  CONSTRAINT [FK_SavingEvents_Tellers] FOREIGN KEY([teller_id])
REFERENCES [dbo].[Tellers] ([id])
GO
ALTER TABLE [dbo].[SavingEvents] CHECK CONSTRAINT [FK_SavingEvents_Tellers]
GO

INSERT INTO dbo.EventTypes (event_type, description, sort_order, accounting)
VALUES ('SDIT', 'Saving debit inter-branch transfer', 185, 1)
GO

INSERT INTO dbo.EventTypes (event_type, description, sort_order, accounting)
VALUES ('SCIT', 'Saving credit inter-branch transfer', 186, 1)
GO

DECLARE @LODE_id INT
DECLARE @LEE0_id INT

SELECT @lEE0_id = id
FROM dbo.EventAttributes
WHERE event_type = 'LEE0'

SELECT @LODE_id = id
FROM dbo.EventAttributes
WHERE event_type = 'LODE'
AND name = 'fees'

UPDATE AccountingRules SET event_type = 'LEE0', event_attribute_id = @LEE0_id
WHERE event_type = 'LODE'
AND event_attribute_id = @LODE_id
GO

DELETE FROM dbo.EventAttributes WHERE event_type = 'LODE' AND name = 'fees'
GO

UPDATE Contracts SET status = 3
WHERE closed = 1 AND id
  IN (SELECT contract_id
	  FROM ContractEvents
	  WHERE event_type = 'LODE'
	    AND is_deleted = 1)
  AND id NOT IN (SELECT contract_id
				 FROM ContractEvents ce
				 INNER JOIN RepaymentEvents re ON ce.id = re.id)
GO

SELECT c.id, c.contract_code
INTO #1
FROM Contracts c
INNER JOIN ContractEvents ce ON ce.contract_id = c.id
WHERE c.id NOT IN (SELECT c.id
                   FROM Contracts c
                   INNER JOIN ContractEvents ce ON ce.contract_id = c.id
                   INNER JOIN LoanDisbursmentEvents ld ON ld.id = ce.id)
  AND c.id IN (SELECT c.id
               FROM Contracts c
               INNER JOIN ContractEvents ce ON ce.contract_id = c.id
               INNER JOIN RepaymentEvents re ON re.id = ce.id)
GROUP BY c.id, c.contract_code

DECLARE db_cursor CURSOR FOR
SELECT id
FROM #1

DECLARE @temp_id int
DECLARE	@temp_amount float
DECLARE @temp_date datetime
DECLARE	@temp_interest float
DECLARE @LODE_id int
DECLARE @LEE0_id int
DECLARE @EntryFee float
DECLARE @temp_entry float
DECLARE @isRate int
OPEN db_cursor

FETCH NEXT FROM db_cursor INTO @temp_id
WHILE @@FETCH_STATUS = 0
BEGIN
	SELECT @temp_amount = amount
	FROM Credit 
	WHERE id = @temp_id

	SELECT @temp_interest = SUM(interest_repayment)
	FROM Installments
	WHERE contract_id = @temp_id
	
	SELECT @temp_date = start_date
	FROM Contracts
	WHERE id = @temp_id
	
	INSERT INTO ContractEvents (event_type, contract_id, event_date, user_id, is_deleted, is_exported)
	VALUES ('LODE', @temp_id, @temp_date, 1, 0, 0) 
	
	SELECT @LODE_id = @@IDENTITY  
	
	INSERT INTO LoanDisbursmentEvents (id, amount, interest) 
	VALUES (@LODE_id, @temp_amount, @temp_interest)
	
	SELECT @temp_entry = entry_fees
	FROM Credit 
	WHERE id = @temp_id
	
	SET @EntryFee = @temp_entry * @temp_amount
	
	IF (@temp_entry <> 0)
	BEGIN
		SELECT @isRate = e.rate
		FROM Credit c
		INNER JOIN Packages p ON p.id = c.package_id
		INNER JOIN dbo.EntryFees e ON e.id_product = p.id
		WHERE c.id = @temp_id
		
		IF (@isRate <> 1)
		BEGIN
			SET @EntryFee = @temp_entry 	
		END
		
		INSERT INTO ContractEvents (event_type, contract_id, event_date, user_id, is_deleted, is_exported)
		VALUES ('LEE0', @temp_id, @temp_date, 1, 0, 0) 
		
		SELECT @LEE0_id = @@IDENTITY  
	
		INSERT INTO LoanEntryFeeEvents (id, fee, disbursement_event_id) 
		VALUES (@LEE0_id, @EntryFee, @LODE_id)
	END
	
	FETCH NEXT FROM db_cursor INTO @temp_id
END

DEALLOCATE db_cursor
DROP TABLE #1
GO

ALTER TABLE dbo.SavingContracts
ADD [initial_amount] [money] NOT NULL DEFAULT ((0)),
	[entry_fees] [money] NOT NULL DEFAULT ((0))
GO

UPDATE dbo.SavingBookProducts
SET is_ibt_fee_flat = CASE WHEN transfer_fees_type = 1 THEN 1 ELSE 0 END
, ibt_fee_min = CASE WHEN transfer_fees_type = 1 THEN flat_transfer_fees_min ELSE 100 * rate_transfer_fees_min END
, ibt_fee_max = CASE WHEN transfer_fees_type = 1 THEN flat_transfer_fees_max ELSE 100 * rate_transfer_fees_max END
, ibt_fee = CASE WHEN transfer_fees_type = 1 THEN flat_transfer_fees ELSE 100 * rate_transfer_fees END
GO

ALTER TABLE dbo.Roles
ADD [role_of_teller] [bit] NULL DEFAULT ((0))
GO

UPDATE dbo.Roles
SET [role_of_teller] = 0
GO

INSERT INTO dbo.UsersBranches
SELECT id, 1
FROM dbo.Users
WHERE id > 1
GO

DELETE FROM dbo.ContractAccountingRules
WHERE id IN (SELECT id 
             FROM dbo.AccountingRules 
             WHERE event_type = 'ROWE')

DELETE FROM dbo.AccountingRules
WHERE event_type = 'ROWE'

DELETE FROM dbo.EventAttributes
WHERE event_type = 'ROWE'

UPDATE dbo.EventTypes
SET event_type = 'ROWO'
WHERE event_type = 'ROWE'

INSERT INTO EventAttributes (event_type, name) VALUES('ROWO', 'principal')
INSERT INTO EventAttributes (event_type, name) VALUES('ROWO', 'penalties')
INSERT INTO EventAttributes (event_type, name) VALUES('ROWO', 'commissions')
INSERT INTO EventAttributes (event_type, name) VALUES('ROWO', 'interests')
GO

UPDATE dbo.SavingBookContracts
SET flat_ibt_fee = flat_transfer_fees
, rate_ibt_fee = rate_transfer_fees
GO

INSERT INTO [dbo].[ActionItems] ([class_name], [method_name]) VALUES ('ExchangeRateServices', 'SaveRate')
GO

UPDATE dbo.Branches
SET name = (SELECT value FROM dbo.GeneralParameters WHERE [key] = 'BRANCH_NAME')
, address = (SELECT value FROM dbo.GeneralParameters WHERE [key] = 'BRANCH_ADDRESS')
, code = (SELECT value FROM dbo.GeneralParameters WHERE [key] = 'BRANCH_CODE')
WHERE id = 1
GO

UPDATE dbo.ContractAccountingRules
SET payment_method_id = 0
WHERE payment_method_id IS NULL
GO

UPDATE [TechnicalParameters] SET [value] = 'v3.2.0' WHERE [name] = 'VERSION'
GO