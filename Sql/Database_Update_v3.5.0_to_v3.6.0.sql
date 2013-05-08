CREATE TABLE dbo.Rep_Repayments_Data
    (
      id INT NOT NULL ,
      branch_name NVARCHAR(50) NULL ,
      load_date DATETIME NULL ,
      event_id INT NULL ,
      contract_code NVARCHAR(255) NULL ,
      client_name NVARCHAR(255) NULL ,
      district_name NVARCHAR(255) NULL ,
      loan_officer_name NVARCHAR(255) NULL ,
      loan_product_name NVARCHAR(255) NULL ,
      early_fee MONEY NULL ,
      late_fee MONEY NULL ,
      principal MONEY NULL ,
      interest MONEY NULL ,
      total MONEY NULL ,
      written_off BIT NULL
    )
GO

INSERT  INTO [GeneralParameters]
        ( [key], [value] )
VALUES  ( 'CLIENT_AGE_MIN', 18 )
INSERT  INTO [GeneralParameters]
        ( [key], [value] )
VALUES  ( 'CLIENT_AGE_MAX', 100 )
INSERT  INTO [GeneralParameters]
        ( [key], [value] )
VALUES  ( 'AUTOMATIC_ID', 0 )
INSERT  INTO [GeneralParameters]
        ( [key], [value] )
VALUES  ( 'MAX_LOANS_COVERED', 1 )
GO

DECLARE @cnstr VARCHAR(100)
SET @cnstr = ( SELECT   name
               FROM     sysobjects
               WHERE    name LIKE 'DF__Contracts__is_fo%'
             )
EXEC('ALTER TABLE dbo.Contracts DROP CONSTRAINT '+ @cnstr)
GO

ALTER TABLE Contracts
DROP COLUMN is_for_nsg
GO

ALTER TABLE Contracts
ADD nsg_id INT NULL
GO

ALTER TABLE dbo.Contracts ADD CONSTRAINT
FK_Contracts_Villages FOREIGN KEY
(
nsg_id
) REFERENCES dbo.Villages
(
id
) ON UPDATE  NO ACTION 
ON DELETE  NO ACTION 
GO

ALTER TABLE [dbo].[SavingEvents]
ADD loan_event_id INT NULL
GO

SET IDENTITY_INSERT [MenuItems] ON
INSERT  INTO [dbo].[MenuItems]
        ( [id], [component_name] )
VALUES  ( 71, 'mnuCreditScoringQuestions' )
SET IDENTITY_INSERT [MenuItems] OFF
GO

INSERT  INTO [dbo].[ActionItems]
        ( [class_name], [method_name] )
VALUES  ( 'LoanServices', 'WaiveFee' )
GO

CREATE TABLE [dbo].[CreditScoringAnswers]
    (
      [id] [int] IDENTITY(1, 1)
                 NOT NULL ,
      [name] [nvarchar](1000) NOT NULL ,
      [score] [int] NOT NULL ,
      [credit_scoring_question_id] [int] NOT NULL ,
      CONSTRAINT [PK_CreditScoringAnswers] PRIMARY KEY CLUSTERED ( [id] ASC )
        WITH ( PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF,
               IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON,
               ALLOW_PAGE_LOCKS = ON ) ON [PRIMARY]
    )
ON  [PRIMARY]
GO

CREATE TABLE [dbo].[CreditScoringQuestions]
    (
      [id] [int] IDENTITY(1, 1)
                 NOT NULL ,
      [question_name] [nvarchar](1000) NOT NULL ,
      [question_type] [int] NOT NULL ,
      CONSTRAINT [PK_CreditScoringQuestions] PRIMARY KEY CLUSTERED
        ( [id] ASC )
        WITH ( PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF,
               IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON,
               ALLOW_PAGE_LOCKS = ON ) ON [PRIMARY]
    )
ON  [PRIMARY]
GO

ALTER TABLE dbo.CreditScoringAnswers ADD CONSTRAINT
FK_CreditScoringAnswers_CreditScoringQuestions FOREIGN KEY
(
credit_scoring_question_id
) REFERENCES dbo.CreditScoringQuestions
(
id
) 
GO

CREATE TABLE [dbo].[CreditScoringValues]
    (
      [id] [int] IDENTITY(1, 1)
                 NOT NULL ,
      [question_name] [nvarchar](1000) NOT NULL ,
      [question_type] [int] NOT NULL ,
      [answer_value] [nvarchar](1000) NOT NULL ,
      [score] [int] NULL ,
      [loan_id] [int] NOT NULL ,
      [question_id] [int] NOT NULL ,
      CONSTRAINT [PK_CreditScoringValues] PRIMARY KEY CLUSTERED ( [id] ASC )
        ON [PRIMARY]
    )
ON  [PRIMARY]
GO

ALTER TABLE dbo.RepaymentEvents
ADD calculated_penalties MONEY NOT NULL DEFAULT(0)
, written_off_penalties MONEY NOT NULL DEFAULT(0)
, unpaid_penalties MONEY NOT NULL DEFAULT(0)
GO

INSERT  INTO [dbo].[EventTypes]
        ( [event_type] ,
          [description] ,
          [sort_order] ,
          [accounting]
        )
VALUES  ( 'SRLE' ,
          'Savings repayment for loan event' ,
          316 ,
          1
        )
GO
INSERT  INTO [dbo].[EventAttributes]
        ( [event_type], [name] )
VALUES  ( 'SRLE', 'amount' )
GO

DECLARE @loan_event_id INT
DECLARE @loan_id INT

DECLARE DisbursementsIntoSavingsCursor CURSOR FOR
SELECT CE.id, CE.contract_id
FROM [dbo].[ContractEvents] AS CE
INNER JOIN [dbo].[LoanDisbursmentEvents] AS LDE ON CE.id=LDE.id
WHERE payment_method_id=8 AND CE.is_deleted=0

OPEN DisbursementsIntoSavingsCursor
FETCH NEXT FROM DisbursementsIntoSavingsCursor INTO @loan_event_id, @loan_id
WHILE @@FETCH_STATUS = 0 
    BEGIN
        UPDATE  SavingEvents
        SET     loan_event_id = @loan_event_id
        WHERE   loan_id = @loan_id
        FETCH NEXT FROM DisbursementsIntoSavingsCursor INTO @loan_event_id, @loan_id
    END 
CLOSE DisbursementsIntoSavingsCursor
DEALLOCATE DisbursementsIntoSavingsCursor
GO

ALTER TABLE dbo.SavingEvents 
DROP COLUMN loan_id
GO

ALTER TABLE dbo.Installments
DROP COLUMN payment_method
GO

ALTER TABLE dbo.InstallmentHistory
DROP COLUMN payment_method
GO

SELECT re.id AS event_id
INTO #Events
FROM dbo.RepaymentEvents re
INNER JOIN dbo.ContractEvents ce ON re.id = ce.id
INNER JOIN dbo.Contracts c ON ce.contract_id = c.id
WHERE principal - ROUND(principal, 2) <> 0
 OR interests - ROUND(interests, 2) <> 0
 OR penalties - ROUND(penalties, 2) <> 0
 OR commissions - ROUND(commissions, 2) <> 0
ORDER BY entry_date, re.id

UPDATE dbo.RepaymentEvents
SET principal = ROUND(principal, 2),
  interests = ROUND(interests, 2),
  penalties = ROUND(penalties, 2),
  commissions = ROUND(commissions, 2)
FROM #Events
WHERE event_id = id
GO

ALTER TABLE dbo.SavingBookProducts
ADD use_term_deposit BIT NOT NULL DEFAULT ((0))
GO

ALTER TABLE dbo.SavingBookProducts
ADD term_deposit_period_min INT NULL
GO

ALTER TABLE dbo.SavingBookProducts
ADD term_deposit_period_max INT NULL
GO

ALTER TABLE dbo.SavingBookProducts
ADD posting_frequency INT NULL
GO

CREATE TABLE [dbo].[Rep_Rescheduled_Loans_Data]
(
	id INT NOT NULL,
	branch_name NVARCHAR(50) NULL,
	load_date DATETIME NULL,
	loan_officer NVARCHAR(255) NULL,
	client_name NVARCHAR(255) NULL,
	contract_code NVARCHAR(255) NULL,
	package_name NVARCHAR(255) NULL,
	loan_amount MONEY NULL,
	amount_rescheduled MONEY NULL,
	maturity INT NULL,
	reschedule_date DATETIME NULL,
	olb MONEY NULL
)
GO

ALTER TABLE [dbo].[SavingBookContracts]
ADD [use_term_deposit] [bit] NOT NULL DEFAULT ((0))
GO 

ALTER TABLE [dbo].[SavingBookContracts]
ADD [term_deposit_period] [int] NULL
GO

ALTER TABLE [dbo].[SavingBookContracts]
ADD [term_deposit_period_min] [int] NULL
GO

ALTER TABLE [dbo].[SavingBookContracts]
ADD [term_deposit_period_max] [int] NULL
GO

ALTER TABLE [dbo].[SavingBookContracts]
ADD [transfer_account] NVARCHAR(50) NULL
GO

ALTER TABLE [dbo].[SavingBookContracts]
ADD [rollover] [int] NULL
GO

DECLARE @lode_events TABLE (
	id INT NOT NULL
	, contract_id INT NOT NULL
	, event_date DATETIME NOT NULL
	, num INT NOT NULL
)
INSERT INTO @lode_events
SELECT id, contract_id, event_date
	, ROW_NUMBER() OVER (PARTITION BY contract_id ORDER BY id) num
FROM dbo.ContractEvents
WHERE is_deleted = 0 AND event_type = 'LODE'

DELETE FROM dbo.LoanDisbursmentEvents
WHERE id IN (
	SELECT id
	FROM @lode_events
	WHERE contract_id IN (
		SELECT contract_id
		FROM @lode_events
		WHERE num = 2
	)
	AND num = 1
)

DELETE FROM dbo.ContractEvents
WHERE id IN (
	SELECT id
	FROM @lode_events
	WHERE contract_id IN (
		SELECT contract_id
		FROM @lode_events
		WHERE num = 2
	)
	AND num = 1
)
GO

UPDATE  [TechnicalParameters]
SET     [value] = 'v3.6.0'
WHERE   [name] = 'VERSION'
GO
