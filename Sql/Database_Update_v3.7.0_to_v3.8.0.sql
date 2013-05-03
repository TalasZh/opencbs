ALTER TABLE Users ADD
	sex NCHAR(1) NOT NULL
	CONSTRAINT DF_Users_sex DEFAULT 'M'
GO

EXEC sp_rename 'DomainOfApplications', 'EconomicActivities'
GO

INSERT INTO [dbo].[AdvancedFieldsTypes] ([name]) VALUES ('Client')
GO

INSERT INTO [TechnicalParameters]([name], [value]) VALUES ('BuildNumber', '0')
GO

INSERT INTO [dbo].[InstallmentTypes] ([name], [nb_of_days], [nb_of_months])  VALUES ('Maturity',0,0)
GO

INSERT INTO [dbo].[ActionItems] ([class_name],[method_name]) 
VALUES ('LoanServices', 'ModifyDisbursementDate')
GO

INSERT INTO [dbo].[ActionItems] ([class_name],[method_name]) 
VALUES ('LoanServices', 'ModifyFirstInstalmentDate')
GO

INSERT INTO [dbo].[ActionItems] ([class_name],[method_name]) 
VALUES ('ClientServices', 'ModifyBadClient')
GO

DECLARE @action_item_id INT
INSERT INTO [dbo].[ActionItems] ([class_name],[method_name]) 
VALUES ('LoanServices', 'ModifyGuarantorsCollaterals')

SELECT @action_item_id = SCOPE_IDENTITY()

INSERT INTO [dbo].[AllowedRoleActions] ([action_item_id],[role_id] ,[allowed])
SELECT @action_item_id, r.id, 
CASE 
	WHEN r.code = 'SUPER' THEN 1 ELSE 0 
END 
FROM Roles r
WHERE not exists(SELECT * FROM AllowedRoleActions WHERE action_item_id = @action_item_id and role_id = r.id)
GO

INSERT INTO dbo.EventTypes (event_type, [description], sort_order, accounting) VALUES ('SBCS', 'Savings Block Compulsory Savings', 317, 1)
INSERT INTO dbo.EventTypes (event_type, [description], sort_order, accounting) VALUES ('SUCS', 'Savings Unblock Compulsory Savings', 318, 1)

INSERT INTO [dbo].[EventAttributes] (event_type, name) VALUES ('SBCS', 'amount')
INSERT INTO [dbo].[EventAttributes] (event_type, name) VALUES ('SUCS', 'amount')
GO

DECLARE @db_from NVARCHAR(MAX)
DECLARE @db_to NVARCHAR(MAX)
DECLARE @sql NVARCHAR(MAX)

SELECT @db_from = DB_NAME()
SET @db_to = @db_from + '_attachments'

IF EXISTS (SELECT * FROM sys.databases WHERE name = @db_to)
BEGIN
	SET @sql = '
	IF NOT EXISTS (SELECT * FROM ' + @db_to + '.[sys].[tables] WHERE name= ''Documents'')
	BEGIN
	CREATE TABLE ' + @db_to + '..ClientDocuments' +
	'(
		id INT IDENTITY(1,1) NOT NULL
		, client_id INT NOT NULL
		, name NVARCHAR(255) NOT NULL
		, [filename] NVARCHAR(255) NOT NULL
		, document IMAGE NULL
		, comment TEXT
        , date DATETIME
        , user_id INT NOT NULL
        , is_deleted bit
	) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
	END'
	EXEC sp_executesql @sql
END
GO

INSERT INTO Statuses(status_name) VALUES ('Postponed')
GO

IF NOT EXISTS (SELECT * FROM TechnicalParameters WHERE name = 'last_id')
INSERT INTO TechnicalParameters (name,value) VALUES ('last_id',0)
ELSE
UPDATE TechnicalParameters SET value = 0 FROM TechnicalParameters WHERE name = 'last_id'
GO

CREATE TABLE [dbo].[LoanMonitoring]
(
	[id] [int] IDENTITY(1,1) NOT NULL,
	[contract_id] [int] NOT NULL,
	[date] [datetime] NULL,
	[purpose] [nvarchar](255) NULL,
	[monitor] [nvarchar](255) NULL,
	[comment] [nvarchar](4000) NULL
)
GO

UPDATE GeneralParameters SET value = (
   CASE 
      WHEN value = 'True' THEN 1 ELSE 0 
   END) 
WHERE value in ('True','False')
GO

INSERT INTO SavingBookProducts
(
	id,
	interest_base,
	interest_frequency,
	calcul_amount_base,
	withdraw_fees_type,
	flat_withdraw_fees_min,
	flat_withdraw_fees_max,
	flat_withdraw_fees,
	rate_withdraw_fees_min,
	rate_withdraw_fees_max,
	rate_withdraw_fees,
	transfer_fees_type,
	flat_transfer_fees_min,
	flat_transfer_fees_max,
	flat_transfer_fees,
	rate_transfer_fees_min,
	rate_transfer_fees_max,
	rate_transfer_fees,
	deposit_fees,
	deposit_fees_max,
	deposit_fees_min,
	close_fees,
	close_fees_max,
	close_fees_min,
	management_fees,
	management_fees_max,
	management_fees_min,
	management_fees_freq,
	overdraft_fees,
	overdraft_fees_max,
	overdraft_fees_min,
	agio_fees,
	agio_fees_max,
	agio_fees_min,
	agio_fees_freq,
	cheque_deposit_min,
	cheque_deposit_max,
	cheque_deposit_fees,
	cheque_deposit_fees_min,
	cheque_deposit_fees_max,
	reopen_fees,
	reopen_fees_min,
	reopen_fees_max,
	is_ibt_fee_flat,
	ibt_fee_min,
	ibt_fee_max,
	ibt_fee,
	use_term_deposit,
	term_deposit_period_min,
	term_deposit_period_max,
	posting_frequency
)
SELECT 	[SavingProducts].[id] AS id
		,CAST('10' AS INT ) AS [interest_base]
		,CAST('10' AS INT) AS [interest_frequency]
		,CAST('0' AS INT) AS [calcul_amount_base]
		,TermDepositProducts.[withdrawal_fees_type] 
		,TermDepositProducts.[withdrawal_fees_min] AS flat_withdraw_fees_min
		,TermDepositProducts.[withdrawal_fees_max] AS flat_withdraw_fees_max
		,TermDepositProducts.[withdrawal_fees] AS flat_withdraw_fees
		,TermDepositProducts.[withdrawal_fees_min] AS rate_withdraw_fees_min
		,TermDepositProducts.[withdrawal_fees_max] AS rate_withdraw_fees_max
		,TermDepositProducts.[withdrawal_fees] AS rate_withdraw_fees
		,TermDepositProducts.[withdrawal_fees_type] AS [transfer_fees_type]
		,TermDepositProducts.[withdrawal_fees_min] AS flat_transfer_fees_min
		,TermDepositProducts.[withdrawal_fees_max] AS flat_transfer_fees_max
		,TermDepositProducts.[withdrawal_fees] AS flat_transfer_fees
		,TermDepositProducts.[withdrawal_fees_min] AS rate_transfer_fees_min
		,TermDepositProducts.[withdrawal_fees_max] AS rate_transfer_fees_max
		,TermDepositProducts.[withdrawal_fees] AS rate_transfer_fees
		,CAST('0' AS DECIMAL) AS deposit_fees
		,NULL AS deposit_fees_max
		,NULL AS deposit_fees_min
		,TermDepositProducts.[withdrawal_fees] AS close_fees
		,TermDepositProducts.[withdrawal_fees_min] AS close_fees_min
		,TermDepositProducts.[withdrawal_fees_max] AS close_fees_max
		,CAST('0' AS DECIMAL) AS [management_fees]
		,NULL AS management_fees_max
		,NULL AS management_fees_min
		,CAST ('1' AS INT) AS [management_fees_freq]
		,CAST('0' AS DECIMAL) AS[overdraft_fees]
		,NULL AS [overdraft_fees_max]
		,NULL AS [overdraft_fees_min]
		,CAST('0' AS DECIMAL) AS agio_fees
		,NULL AS agio_fees_max
		,NULL AS agio_fees_min
		,CAST('4' AS INT) AS agio_fees_freq
		,SavingProducts.balance_min AS cheque_deposit_min
		,SavingProducts.balance_max AS cheque_deposit_max
		,CAST('0' AS DECIMAL) AS cheque_deposit_fees
		,NULL AS cheque_deposit_fees_min
		,NULL AS cheque_deposit_fees_max
		,CAST('0' AS DECIMAL) AS [reopen_fees]
		,NULL AS [reopen_fees_min]
		,NULL AS [reopen_fees_max]
		,CAST('0' AS BIT ) AS [is_ibt_fee_flat]
		,NULL AS [ibt_fee_min]
		,NULL AS [ibt_fee_max]
		,CAST('0' AS DECIMAL) AS [ibt_fee]
		,CAST('1' AS BIT) AS [use_term_deposit]
		,CASE WHEN [number_period] IS NOT NULL THEN [number_period] ELSE [number_period_min] END AS [term_deposit_period_min]
		,CASE WHEN [number_period] IS NOT NULL THEN [number_period] ELSE [number_period_max] END AS [term_deposit_period_max]
		,[installment_types_id] AS [posting_frequency]
FROM  dbo.SavingProducts  
INNER JOIN [dbo].[TermDepositProducts] ON [dbo].[TermDepositProducts].id = SavingProducts.id
WHERE savingProducts.product_type = 'T'
GO

UPDATE dbo.SavingProducts SET 
							deposit_min = balance_min, 
							deposit_max = balance_max
WHERE product_type='T'
GO

UPDATE dbo.SavingProducts SET product_type = 'B'
WHERE product_type='T'
GO

INSERT INTO SavingBookContracts
(
	id,
	flat_withdraw_fees,
	rate_withdraw_fees,
	flat_transfer_fees,
	rate_transfer_fees,
	flat_deposit_fees,
	flat_close_fees,
	flat_management_fees,
	flat_overdraft_fees,
	in_overdraft,
	rate_agio_fees,
	cheque_deposit_fees,
	flat_reopen_fees,
	flat_ibt_fee,
	rate_ibt_fee,
	use_term_deposit,
	term_deposit_period,
	term_deposit_period_min,
	term_deposit_period_max,
	transfer_account,
	rollover,
	next_maturity
)

SELECT 
	Id
	, withdrawal_fees
	, CAST('0' AS DECIMAL) AS [rate_withdraw_fees]
	, CAST('0' AS DECIMAL) AS [flat_transfer_fees]
	, CAST('0' AS DECIMAL) AS [rate_transfer_fees]
	, CAST('0' AS DECIMAL) AS [flat_deposit_fees]
	, CAST('0' AS DECIMAL) AS [flat_close_fees]
	, CAST('0' AS DECIMAL) AS [flat_management_fees]
	, CAST('0' AS DECIMAL) AS [flat_overdraft_fees]
	, CAST('0' AS DECIMAL) AS [in_overdraft]
	, CAST('0' AS DECIMAL) AS [rate_agio_fees]
	, CAST('0' AS DECIMAL) AS [cheque_deposit_fees]
	, CAST('0' AS DECIMAL) AS [flat_reopen_fees]
	, CAST('0' AS DECIMAL) AS [flat_ibt_fee]
	, CAST('0' AS DECIMAL) AS [rate_ibt_fee]
	, CAST('1' AS BIT) AS [use_term_deposit]
	,[number_periods] AS [term_deposit_period]
	,[number_periods] AS  [term_deposit_period_min]
	,[number_periods] AS  [term_deposit_period_max]
	,[transfer_account] AS [transfer_account]
	,[rollover] AS [rollover]
	,[next_maturity] AS [next_maturity]
FROM dbo.SavingDepositContracts 
GO

DELETE FROM dbo.GeneralParameters WHERE [key] = 'USE_CENTS'
GO

IF  EXISTS 
	(SELECT * 
    FROM sys.objects 
    WHERE object_id = OBJECT_ID(N'dbo.TrialBalance') AND type = N'P')
DROP PROCEDURE [dbo].[TrialBalance]
GO

UPDATE  [TechnicalParameters]
SET     [value] = 'v3.8.0'
WHERE   [name] = 'VERSION'
GO