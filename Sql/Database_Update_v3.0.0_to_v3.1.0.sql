DECLARE @ct NVARCHAR(100)
SELECT @ct = name
FROM sysobjects WHERE name LIKE 'DF__Contracts__branc%'
EXEC('ALTER TABLE dbo.Contracts DROP CONSTRAINT ' + @ct)
GO

ALTER TABLE dbo.Contracts DROP CONSTRAINT FK_Contracts_Branches
GO

ALTER TABLE dbo.Contracts DROP COLUMN branch_id
GO

ALTER TABLE dbo.Branches
ADD deleted BIT NOT NULL DEFAULT(0)
GO

DELETE FROM dbo.InstallmentHistory
WHERE event_id IN 
(SELECT id FROM ContractEvents WHERE event_type='PDLE')

DELETE FROM dbo.ContractEvents
WHERE event_type = 'PDLE'
GO

DROP TABLE dbo.PastDueLoanEvents
GO

ALTER TABLE dbo.ContractAccountingRules
ADD payment_method_id INT DEFAULT(1)
GO

ALTER TABLE dbo.RepaymentEvents
ADD payment_method_id INT DEFAULT(1)
GO

CREATE TABLE dbo.UsersBranches
(
	user_id INT NOT NULL
	, branch_id INT NOT NULL
)
GO

ALTER TABLE dbo.UsersBranches
ADD CONSTRAINT FK_UsersBranches_Users FOREIGN KEY (user_id) 
REFERENCES dbo.Users (id) 
GO

CREATE TABLE [dbo].[PaymentMethods](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](50) NULL,
	[description] [nvarchar](250) NULL,
	[pending] [bit] NULL DEFAULT(0),
 CONSTRAINT [PK_PaymentMethods] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

INSERT INTO [PaymentMethods] ([name])
VALUES ('Cash')
INSERT INTO [PaymentMethods] ([name])
VALUES ('Cheque')
INSERT INTO [PaymentMethods] ([name])
VALUES ('Withdrawal')
INSERT INTO [PaymentMethods] ([name])
VALUES ('DirectDebit')
INSERT INTO [PaymentMethods] ([name])
VALUES ('WireTransfer')
INSERT INTO [PaymentMethods] ([name])
VALUES ('DebitCard')
GO

INSERT INTO dbo.UsersBranches (user_id, branch_id)
SELECT 1, id FROM dbo.Branches
GO

ALTER TABLE dbo.OverdueEvents
ADD overdue_principal MONEY
GO

INSERT INTO EventAttributes (event_type, name) VALUES('GLLL', 'overdue_principal')
INSERT INTO EventAttributes (event_type, name) VALUES('GLBL', 'overdue_principal')
INSERT INTO EventAttributes (event_type, name) VALUES('LLGL', 'overdue_principal')
INSERT INTO EventAttributes (event_type, name) VALUES('LLBL', 'overdue_principal')
INSERT INTO EventAttributes (event_type, name) VALUES('BLGL', 'overdue_principal')
INSERT INTO EventAttributes (event_type, name) VALUES('BLLL', 'overdue_principal')
INSERT INTO EventAttributes (event_type, name) VALUES('GLRL', 'overdue_principal')
INSERT INTO EventAttributes (event_type, name) VALUES('LLRL', 'overdue_principal')
INSERT INTO EventAttributes (event_type, name) VALUES('BLRL', 'overdue_principal')
GO

DELETE FROM dbo.EventAttributes
WHERE event_type IN ('SWFE', 'PDLE', 'SVDC')

DELETE FROM EventTypes
WHERE event_type IN ('PDLE', 'SWFE', 'SVDC')
GO

DELETE FROM EventAttributes 
WHERE event_type = 'LODE'
  AND name = 'interest'
GO

DELETE FROM EventAttributes 
WHERE event_type IN ('SMFE', 'SOFE', 'SVAE', 'SVCE')
  AND name = 'amount'
GO

DELETE FROM EventAttributes 
WHERE event_type = 'SPDR'
  AND name = 'fees'
GO

ALTER TABLE EventTypes
ADD accounting BIT DEFAULT(0)
GO

CREATE TABLE [dbo].[LoanEntryFeeEvents](
	[id] [int] NOT NULL,
	[fee] [money] NOT NULL,
	[disbursement_event_id] [int] NOT NULL
) ON [PRIMARY]
GO

INSERT INTO [dbo].[EventTypes]([event_type],[description],[sort_order]) VALUES('LEE0','Entry fees 0',500)
INSERT INTO [dbo].[EventTypes]([event_type],[description],[sort_order]) VALUES('LEE1','Entry fees 1',510)
INSERT INTO [dbo].[EventTypes]([event_type],[description],[sort_order]) VALUES('LEE2','Entry fees 2',520)
INSERT INTO [dbo].[EventTypes]([event_type],[description],[sort_order]) VALUES('LEE3','Entry fees 3',530)
INSERT INTO [dbo].[EventTypes]([event_type],[description],[sort_order]) VALUES('LEE4','Entry fees 4',540)
INSERT INTO [dbo].[EventTypes]([event_type],[description],[sort_order]) VALUES('LEE5','Entry fees 5',550)
INSERT INTO [dbo].[EventTypes]([event_type],[description],[sort_order]) VALUES('LEE6','Entry fees 6',560)
INSERT INTO [dbo].[EventTypes]([event_type],[description],[sort_order]) VALUES('LEE7','Entry fees 7',570)
INSERT INTO [dbo].[EventTypes]([event_type],[description],[sort_order]) VALUES('LEE8','Entry fees 8',580)
INSERT INTO [dbo].[EventTypes]([event_type],[description],[sort_order]) VALUES('LEE9','Entry fees 9',590)
GO

INSERT INTO [dbo].[EventAttributes] ([event_type],[name]) VALUES('LEE0', 'fee')
INSERT INTO [dbo].[EventAttributes] ([event_type],[name]) VALUES('LEE1', 'fee')
INSERT INTO [dbo].[EventAttributes] ([event_type],[name]) VALUES('LEE2', 'fee')
INSERT INTO [dbo].[EventAttributes] ([event_type],[name]) VALUES('LEE3', 'fee')
INSERT INTO [dbo].[EventAttributes] ([event_type],[name]) VALUES('LEE4', 'fee')
INSERT INTO [dbo].[EventAttributes] ([event_type],[name]) VALUES('LEE5', 'fee')
INSERT INTO [dbo].[EventAttributes] ([event_type],[name]) VALUES('LEE6', 'fee')
INSERT INTO [dbo].[EventAttributes] ([event_type],[name]) VALUES('LEE7', 'fee')
INSERT INTO [dbo].[EventAttributes] ([event_type],[name]) VALUES('LEE8', 'fee')
INSERT INTO [dbo].[EventAttributes] ([event_type],[name]) VALUES('LEE9', 'fee')
GO

ALTER TABLE EntryFees
ADD fee_index [int] NOT NULL DEFAULT(-1)
GO

UPDATE dbo.EventTypes
SET accounting = 1
WHERE event_type 
  NOT IN ('CSUE', 'FLNE', 'ULIE', 'ULOE', 'UMEE', 'USBE', 'SCLE', 'SOCE', 'SODE', 'LOVE', 'SCLE')
GO

UPDATE SavingBookContracts
SET cheque_deposit_fees = 0
WHERE cheque_deposit_fees IS NULL
GO

UPDATE dbo.SavingEvents
SET code = 'SVDE',
  savings_method = 2
WHERE code = 'SVDC'
GO

DELETE FROM dbo.GeneralParameters
WHERE [key] = 'WRITE_OFF_DAYS'
GO

DECLARE @id_package INT
DECLARE PackagesIdsCursor  CURSOR FOR
SELECT id
FROM Packages
ORDER BY id

OPEN PackagesIdsCursor
FETCH NEXT FROM PackagesIdsCursor INTO @id_package
WHILE @@FETCH_STATUS=0
BEGIN
	DECLARE @entry_fee_id INT
	DECLARE @fee_index INT
	SET @fee_index=0
	
	DECLARE FeesIdsCursor CURSOR FOR
	SELECT id FROM EntryFees
	WHERE id_product=@id_package
	
	OPEN FeesIdsCursor
		FETCH NEXT FROM FeesIdsCursor INTO @entry_fee_id
	WHILE @@FETCH_STATUS=0
	BEGIN
		UPDATE [dbo].[EntryFees] SET [fee_index]=@fee_index
		WHERE id=@entry_fee_id
		SET @fee_index=@fee_index+1
		FETCH NEXT FROM FeesIdsCursor INTO @entry_fee_id	
	END
	CLOSE FeesIdsCursor
	DEALLOCATE FeesIdsCursor
	
FETCH NEXT FROM PackagesIdsCursor INTO @id_package
END
CLOSE PackagesIdsCursor
DEALLOCATE PackagesIdsCursor
GO

ALTER TABLE [dbo].[ContractEvents]
ADD disbursement_id INT
GO

INSERT INTO [dbo].[ContractEvents]
           ([event_type]
           ,[contract_id]
           ,[event_date]
           ,[user_id]
           ,[is_deleted]
           ,[entry_date]
           ,[is_exported]
           ,[disbursement_id])
           
SELECT DISTINCT 'LEE'+ STR(EntryFees.fee_index, 1, 0) AS event_code
	  ,[contract_id]
      ,[event_date]
      ,[user_id]
      ,ContractEvents.is_deleted
      ,[entry_date]
      ,[is_exported]
      ,ContractEvents.id
FROM [dbo].[ContractEvents]
INNER JOIN CreditEntryFees ON CreditEntryFees.credit_id = ContractEvents.contract_id
INNER JOIN LoanDisbursmentEvents ON LoanDisbursmentEvents.id=ContractEvents.id
INNER JOIN EntryFees ON EntryFees.id=CreditEntryFees.entry_fee_id
WHERE event_type='LODE' AND LoanDisbursmentEvents.fees<>0
GO

DECLARE @disb_id INT
DECLARE @fees MONEY
DECLARE @contract_event_id INT
DECLARE @loan_disb_id INT

DECLARE LoanDisbIdsCursor CURSOR FOR 
SELECT DISTINCT id FROM LoanDisbursmentEvents 

OPEN LoanDisbIdsCursor
FETCH NEXT FROM LoanDisbIdsCursor INTO @loan_disb_id
WHILE @@FETCH_STATUS=0
BEGIN
	DECLARE LoanEntryFeesEventsCursor CURSOR FOR
	SELECT id, [fees]
	FROM [dbo].[LoanDisbursmentEvents]
	WHERE (id=@loan_disb_id AND  [fees]<>0) OR (id=@loan_disb_id AND [fees]=0 AND [amount]=0)
	ORDER BY [fees] 	
	
	OPEN LoanEntryFeesEventsCursor
	FETCH NEXT FROM LoanEntryFeesEventsCursor INTO @disb_id, @fees
	WHILE @@FETCH_STATUS=0
	BEGIN
			DECLARE LoanEntryFeesEventsCursor2 CURSOR FOR
			SELECT DISTINCT id
			FROM ContractEvents
			WHERE event_type LIKE 'LEE%' AND disbursement_id=@disb_id
						
			OPEN LoanEntryFeesEventsCursor2
			FETCH NEXT FROM LoanEntryFeesEventsCursor2 INTO @contract_event_id
			WHILE @@FETCH_STATUS=0
			BEGIN
					INSERT INTO dbo.LoanEntryFeeEvents (id, fee, disbursement_event_id)
					VALUES (@contract_event_id, @fees, @disb_id)
					FETCH NEXT FROM LoanEntryFeesEventsCursor INTO @disb_id, @fees
					FETCH NEXT FROM LoanEntryFeesEventsCursor2 INTO @contract_event_id
			END
			CLOSE LoanEntryFeesEventsCursor2
			DEALLOCATE LoanEntryFeesEventsCursor2
	END
	CLOSE LoanEntryFeesEventsCursor
	DEALLOCATE LoanEntryFeesEventsCursor
	FETCH NEXT FROM LoanDisbIdsCursor INTO @loan_disb_id
END
CLOSE LoanDisbIdsCursor
DEALLOCATE LoanDisbIdsCursor
GO

ALTER TABLE [dbo].[ContractEvents]
DROP COLUMN disbursement_id
GO

UPDATE [LoanDisbursmentEvents]
SET fees=0
GO

DELETE FROM [dbo].[LoanDisbursmentEvents]
WHERE [amount]=0 AND [fees]=0
GO

EXECUTE sp_rename N'dbo.Statuses.Status_name', N'status_name', 'COLUMN' 
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_SavingEvents_Users]') AND parent_object_id = OBJECT_ID(N'[dbo].[SavingEvents]'))
ALTER TABLE [dbo].[SavingEvents]  WITH CHECK ADD  CONSTRAINT [FK_SavingEvents_Users] FOREIGN KEY([user_id])
REFERENCES [dbo].[Users] ([id])
GO
ALTER TABLE [dbo].[SavingEvents] CHECK CONSTRAINT [FK_SavingEvents_Users]
GO

INSERT INTO [dbo].[ActionItems] ([class_name],[method_name]) VALUES  ('LoanServices' ,'CanUserEditRepaymentSchedule')
GO

ALTER TABLE [dbo].[Contracts] 
ADD [loan_purpose] [nvarchar](4000) NULL
GO

ALTER TABLE [dbo].[Contracts] 
ADD [comments] [nvarchar](4000) NULL
GO

CREATE TABLE [dbo].[CycleObjects](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](100) NOT NULL
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[CycleParameters](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[loan_cycle] [int] NOT NULL,
	[min] [money] NOT NULL,
	[max] [money] NOT NULL,
	[cycle_object_id] [int] NOT NULL,
	[cycle_id] [int] NOT NULL
) ON [PRIMARY]
GO

INSERT INTO [dbo].[ActionItems] ([class_name], [method_name]) 
VALUES  ('SavingServices', 'FirstDeposit')
GO

INSERT INTO [dbo].[CycleObjects] ([name])  VALUES ('Loan amount')
INSERT INTO [dbo].[CycleObjects] ([name])  VALUES ('Interest rate')
INSERT INTO [dbo].[CycleObjects] ([name])  VALUES ('Number of installments')
GO

ALTER TABLE [dbo].[Credit] 
DROP COLUMN [comments_of_end]
GO

ALTER TABLE [dbo].ContractEvents 
ADD [comment] [nvarchar](4000) NULL
GO

DECLARE PackagesCursor CURSOR FOR 
SELECT	 [id]
		,[interest_rate]
		,[interest_rate_min]
		,[interest_rate_max]
		,[number_of_installments] 
		,[number_of_installments_min]
		,[number_of_installments_max]
		,[cycle_id]
FROM [dbo].[Packages]
WHERE cycle_id IS NOT NULL
  
DECLARE @package_id INT
		,@ir FLOAT
		,@ir_min FLOAT
		,@ir_max FLOAT
		,@nmb_of_inst INT
		,@nmb_of_inst_min INT
		,@nmb_of_inst_max INT
		,@cycle_id INT
		,@loan_cycle INT

OPEN PackagesCursor

FETCH NEXT FROM PackagesCursor INTO
		 @package_id
		,@ir
		,@ir_min
		,@ir_max
		,@nmb_of_inst
		,@nmb_of_inst_min
		,@nmb_of_inst_max
		,@cycle_id
		
WHILE @@FETCH_STATUS=0
BEGIN
	IF @ir IS NOT NULL INSERT INTO CycleParameters VALUES (0, @ir, @ir,  2, @cycle_id) 
	ELSE INSERT INTO CycleParameters VALUES (0, @ir_min, @ir_max,  2, @cycle_id) 
	
	IF @nmb_of_inst IS NOT NULL INSERT INTO CycleParameters VALUES (0, @nmb_of_inst, @nmb_of_inst,  3, @cycle_id) 
	ELSE INSERT INTO CycleParameters VALUES (0, @nmb_of_inst_min, @nmb_of_inst_max,  3, @cycle_id) 
	
	INSERT INTO CycleParameters
	SELECT [number]
		   ,[amount_min]
		   ,[amount_max]
		   ,1
		   ,[cycle_id] 
	FROM [dbo].[AmountCycles]
	WHERE [cycle_id]=@cycle_id
	
	FETCH NEXT FROM PackagesCursor INTO 
			 @package_id
			,@ir
			,@ir_min
			,@ir_max
			,@nmb_of_inst
			,@nmb_of_inst_min
			,@nmb_of_inst_max
			,@cycle_id		
END
CLOSE PackagesCursor
DEALLOCATE PackagesCursor

GO

ALTER TABLE dbo.Credit
ADD [amount_min] [money] NULL
GO

ALTER TABLE dbo.Credit
ADD [amount_max] [money] NULL
GO

ALTER TABLE dbo.Credit
ADD [ir_min] [float] NULL
GO

ALTER TABLE dbo.Credit
ADD [ir_max] [float] NULL
GO

ALTER TABLE dbo.Credit
ADD [nmb_of_inst_min] [int] NULL
GO

ALTER TABLE dbo.Credit
ADD [nmb_of_inst_max] [int] NULL
GO

ALTER TABLE dbo.Credit
ADD [loan_cycle] [int] NULL
GO

DECLARE CreditCursor CURSOR FOR
SELECT Credit.id, Credit.amount, Credit.interest_rate, Credit.nb_of_installment
FROM Credit
INNER JOIN Packages on Credit.package_id=Packages.id
WHERE Packages.cycle_id IS NOT NULL
DECLARE @credit_id INT, @credit_amount MONEY, @credit_ir FLOAT, @credit_installments INT

OPEN CreditCursor
FETCH NEXT FROM CreditCursor INTO @credit_id, @credit_amount, @credit_ir, @credit_installments
WHILE @@FETCH_STATUS=0
BEGIN
	UPDATE dbo.Credit SET
		[amount_min]= @credit_amount
		,[amount_max]=@credit_amount
		,[ir_min]=@credit_ir
		,[ir_max]=@credit_ir
		,[nmb_of_inst_min] = @credit_installments
		,[nmb_of_inst_max] = @credit_installments
	WHERE id = @credit_id
	FETCH NEXT FROM CreditCursor INTO @credit_id, @credit_amount, @credit_ir, @credit_installments
END
CLOSE CreditCursor
DEALLOCATE CreditCursor

GO

DECLARE EventsCursor CURSOR FOR
SELECT ContractEvents1.id 
FROM [dbo].[ContractEvents]
INNER JOIN
	(
	SELECT  MAX([ContractEvents].[id]) AS id
		  ,[contract_id]
	FROM [dbo].[ContractEvents]
	INNER JOIN Contracts ON Contracts.id=ContractEvents.contract_id
	WHERE [is_deleted]=0 AND Contracts.closed=1 AND event_type NOT LIKE 'LEE%'
	GROUP BY [contract_id], contract_code
 ) AS ContractEvents1 ON [dbo].[ContractEvents].id=ContractEvents1.id
INNER JOIN Contracts ON [ContractEvents].contract_id=Contracts.id 
WHERE event_type='APR' 
DECLARE @event_id INT

OPEN EventsCursor
FETCH NEXT FROM EventsCursor INTO @event_id
WHILE @@FETCH_STATUS=0
BEGIN	
	UPDATE ContractEvents
	SET event_type='ATR'
	WHERE id=@event_id
	FETCH NEXT FROM EventsCursor INTO @event_id
END
CLOSE EventsCursor
DEALLOCATE EventsCursor

GO

UPDATE [TechnicalParameters] SET [value] = 'v3.1.0' WHERE [name] = 'VERSION'
GO
