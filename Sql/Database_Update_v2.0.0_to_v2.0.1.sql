DROP TABLE [TempCashReceipt]
GO

CREATE TABLE [dbo].[TempCashReceipt](
	[userID] [int] NULL,
	[beneficiary_name] [nvarchar](200) NULL,
	[beneficiary_city] [nvarchar](200) NULL,
	[beneficiary_district_name] [nvarchar](200) NULL,
	[contract_code] [nvarchar](50) NULL,
	[loan_officer_name_contract] [nvarchar](200) NULL,
	[paid_date] [datetime] NULL,
	[expected_date] [datetime] NULL,
	[cash_input_voucher_number] [int] NULL,
	[cash_output_voucher_number] [int] NULL,
	[paid_interest_in_internal_currency] [money] NULL,
	[paid_principal_in_internal_currency] [money] NULL,
	[paid_fees_in_internal_currency] [money] NULL,
	[olb_in_internal_currency] [money] NULL,
	[paid_interest_in_external_currency] [money] NULL,
	[paid_principal_in_external_currency] [money] NULL,
	[paid_fees_in_external_currency] [money] NULL,
	[olb_in_external_currency] [money] NULL,
	[paid_interestInLetter] [nvarchar](200) NULL,
	[paid_principalInLetter] [nvarchar](200) NULL,
	[paid_feesInLetter] [nvarchar](200) NULL,
	[interesLocalAccountNumber] [nvarchar](50) NULL,
	[principalLocalAccountNumber] [nvarchar](50) NULL,
	[feeslLocalAccountNumber] [nvarchar](50) NULL,
	[loan_officer_name_event] [nvarchar](200) NULL,
	[interest_repayment_in_internal_currency] [money] NULL,
	[capital_repayment_in_internal_currency] [money] NULL,
	[fees_repayment_in_internal_currency] [money] NULL,
	[installment_number] [int] NULL,
	[paid_amountInLetter] [nvarchar](200) NULL,
	[paid_date_in_letter] [nvarchar](200) NULL,
	[loan_amount] [money] NULL,
	[disbursment_date] [datetime] NULL,
	[maturity] [int] NULL,
	[gracePeriod] [int] NULL,
	[address] [nvarchar](200) NULL,
	[ZipCode] [nvarchar](200) NULL,
	[CreditComiteDate] [nvarchar](50) NULL,
	[ContractStatus] [nvarchar](50) NULL,
	[product] [nvarchar](200) NULL,
	[interest_rate] [nvarchar](50) NULL,
	[RIB] [nvarchar](200) NULL
) ON [PRIMARY]
GO

ALTER TABLE dbo.Projects DROP CONSTRAINT FK_Projects_Tiers
GO

ALTER TABLE dbo.Projects DROP CONSTRAINT FK_Projects_Corporates
GO

CREATE TABLE dbo.Tmp_Projects
	(
	id int NOT NULL IDENTITY (1, 1),
	tiers_id int NOT NULL,
	status smallint NOT NULL,
	name nvarchar(50) NOT NULL,
	code nvarchar(50) NOT NULL,
	aim nvarchar(200) NOT NULL,
	begin_date datetime NOT NULL,
	abilities nvarchar(50) NULL,
	experience nvarchar(50) NULL,
	market nvarchar(50) NULL,
	concurrence nvarchar(50) NULL,
	purpose nvarchar(50) NULL,
	corporate_id int NULL
	)  ON [PRIMARY]
GO

SET IDENTITY_INSERT dbo.Tmp_Projects ON
GO

IF EXISTS(SELECT * FROM dbo.Projects)
	 EXEC('INSERT INTO dbo.Tmp_Projects (id, tiers_id, status, name, code, aim, begin_date, abilities, experience, market, concurrence, purpose, corporate_id)
		SELECT id, tiers_id, status, name, code, aim, begin_date, abilities, experience, market, concurrence, purpose, corporate_id FROM dbo.Projects WITH (HOLDLOCK TABLOCKX)')
GO

SET IDENTITY_INSERT dbo.Tmp_Projects OFF
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].FK_Contracts_Projects') AND parent_object_id = OBJECT_ID(N'[dbo].Contracts'))
ALTER TABLE [dbo].Contracts DROP CONSTRAINT FK_Contracts_Projects
GO

DROP TABLE dbo.Projects
GO

EXECUTE sp_rename N'dbo.Tmp_Projects', N'Projects', 'OBJECT' 
GO

ALTER TABLE dbo.Projects ADD CONSTRAINT
	PK_Projects2 PRIMARY KEY CLUSTERED 
	(
	id
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

ALTER TABLE dbo.Projects ADD CONSTRAINT
	FK_Projects_Corporates FOREIGN KEY
	(
	corporate_id
	) REFERENCES dbo.Corporates
	(
	id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
GO

ALTER TABLE dbo.Projects ADD CONSTRAINT
	FK_Projects_Tiers FOREIGN KEY
	(
	tiers_id
	) REFERENCES dbo.Tiers
	(
	id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
GO

ALTER TABLE dbo.Contracts ADD CONSTRAINT
	FK_Contracts_Projects FOREIGN KEY
	(
	project_id
	) REFERENCES dbo.Projects
	(
	id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
GO

/************** VERSION ***********************/
UPDATE [TechnicalParameters] SET [value]='v2.0.1'
GO

