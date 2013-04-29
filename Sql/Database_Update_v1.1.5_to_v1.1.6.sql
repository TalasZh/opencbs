IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[TempCashReceipt]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1)
DROP TABLE [dbo].[TempCashReceipt]
GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[TempCashReceipt_Members]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1)
DROP TABLE [dbo].[TempCashReceipt_Members]
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
	[paid_amountInLetter] [nvarchar](200) NULL
) ON [PRIMARY]

CREATE TABLE [dbo].[TempCashReceipt_Members](
	[userID] [int] NULL,
	[member_name] [nvarchar](200) NULL,
	[loan_share_amount] [money] NULL,
	[identification_data] [nvarchar](100) NULL,
	[contract_code] [nvarchar](200) NULL
) ON [PRIMARY]


/************** VERSION ***********************/
UPDATE [TechnicalParameters] SET [value]='v1.1.6'
GO

