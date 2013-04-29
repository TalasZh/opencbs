UPDATE GeneralParameters SET stringValue = (SELECT boolValue FROM GeneralParameters WHERE upper([name]) = 'ALIGN_INSTALLMENTS_ON_REAL_DISBURSEMENT_DATE') WHERE upper([name]) = 'ALIGN_INSTALLMENTS_ON_REAL_DISBURSEMENT_DATE'
UPDATE GeneralParameters SET stringValue = (SELECT boolValue FROM GeneralParameters WHERE upper([name]) = 'CASH_RECEIPT_BEFORE_CONFIRMATION') WHERE upper([name]) = 'CASH_RECEIPT_BEFORE_CONFIRMATION'
UPDATE GeneralParameters SET stringValue = (SELECT boolValue FROM GeneralParameters WHERE upper([name]) = 'CHARGE_INTEREST_WITHIN_GRACE_PERIOD') WHERE upper([name]) = 'CHARGE_INTEREST_WITHIN_GRACE_PERIOD'
UPDATE GeneralParameters SET stringValue = (SELECT boolValue FROM GeneralParameters WHERE upper([name]) = 'CITY_IS_AN_OPEN_VALUE') WHERE upper([name]) = 'CITY_IS_AN_OPEN_VALUE'
UPDATE GeneralParameters SET stringValue = (SELECT boolValue FROM GeneralParameters WHERE upper([name]) = 'CITY_MANDATORY') WHERE upper([name]) = 'CITY_MANDATORY'
UPDATE GeneralParameters SET stringValue = (SELECT intValue FROM GeneralParameters WHERE upper([name]) = 'CONSO_NUMBER') WHERE upper([name]) = 'CONSO_NUMBER'
UPDATE GeneralParameters SET stringValue = (SELECT boolValue FROM GeneralParameters WHERE upper([name]) = 'DISABLE_FUTURE_REPAYMENTS') WHERE upper([name]) = 'DISABLE_FUTURE_REPAYMENTS'
UPDATE GeneralParameters SET stringValue = (SELECT intValue FROM GeneralParameters WHERE upper([name]) = 'GROUP_MIN_MEMBERS') WHERE upper([name]) = 'GROUP_MIN_MEMBERS'
UPDATE GeneralParameters SET stringValue = (SELECT boolValue FROM GeneralParameters WHERE upper([name]) = 'GROUPED_CASH_RECEIPTS') WHERE upper([name]) = 'GROUPED_CASH_RECEIPTS'
UPDATE GeneralParameters SET stringValue = (SELECT boolValue FROM GeneralParameters WHERE upper([name]) = 'LOAN_OFFICER_PORTFOLIO_FILTER') WHERE upper([name]) = 'LOAN_OFFICER_PORTFOLIO_FILTER'
UPDATE GeneralParameters SET stringValue = (SELECT boolValue FROM GeneralParameters WHERE upper([name]) = 'PAY_FIRST_INSTALLMENT_REAL_VALUE') WHERE upper([name]) = 'PAY_FIRST_INSTALLMENT_REAL_VALUE'
UPDATE GeneralParameters SET stringValue = (SELECT boolValue FROM GeneralParameters WHERE upper([name]) = 'USE_CENTS') WHERE upper([name]) = 'USE_CENTS'
UPDATE GeneralParameters SET stringValue = (SELECT intValue FROM GeneralParameters WHERE upper([name]) = 'week_end_day1') WHERE upper([name]) = 'week_end_day1'
UPDATE GeneralParameters SET stringValue = (SELECT intValue FROM GeneralParameters WHERE upper([name]) = 'week_end_day2') WHERE upper([name]) = 'week_end_day2'
UPDATE GeneralParameters SET stringValue = (SELECT intValue FROM GeneralParameters WHERE upper([name]) = 'WEEKLY_CONSOLIDATION_DAY') WHERE upper([name]) = 'WEEKLY_CONSOLIDATION_DAY'
GO

CREATE TABLE dbo.Tmp_GeneralParameters
	(
	[key] varchar(50) NOT NULL,
	[value] nvarchar(200) NULL
	)  ON [PRIMARY]
GO
IF EXISTS(SELECT * FROM dbo.GeneralParameters)
	 EXEC('INSERT INTO dbo.Tmp_GeneralParameters ([key], [value])
		SELECT name, stringValue FROM dbo.GeneralParameters WITH (HOLDLOCK TABLOCKX)')
GO
DROP TABLE dbo.GeneralParameters
GO
EXECUTE sp_rename N'dbo.Tmp_GeneralParameters', N'GeneralParameters', 'OBJECT' 
GO
ALTER TABLE dbo.GeneralParameters ADD CONSTRAINT
	PK_GeneralParameters PRIMARY KEY CLUSTERED 
	(
	[key]
	)  ON [PRIMARY]

GO

/************** VERSION ***********************/
UPDATE [TechnicalParameters] SET [value]='v1.1.5'
GO