ALTER TABLE dbo.Tiers DROP CONSTRAINT CK_TiersTypeCode
GO

ALTER TABLE dbo.Tiers WITH NOCHECK
ADD CONSTRAINT CK_TiersTypeCode CHECK ( ( [client_type_code] = 'G'
                                          OR [client_type_code] = 'I'
                                          OR [client_type_code] = 'C'
                                          OR [client_type_code] = 'V'
                                        ) )
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Villages]') AND type in (N'U'))
BEGIN
CREATE TABLE dbo.Villages
    (
      id int NOT NULL,
      name nvarchar(50) NOT NULL,
      establishment_date datetime NOT NULL,
      loan_officer INT NOT NULL
    )
ON  [PRIMARY]
END
GO

ALTER TABLE dbo.Villages ADD CONSTRAINT
	PK_Villages PRIMARY KEY CLUSTERED 
	(
	id
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO


IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[VillagesPersons]') AND type in (N'U'))
BEGIN
CREATE TABLE dbo.VillagesPersons
    (
      village_id int NOT NULL,
      person_id int NOT NULL,
      joined_date datetime NOT NULL,
      left_date datetime NULL
    )
ON  [PRIMARY]
END
GO

ALTER TABLE dbo.Villages ADD CONSTRAINT
	FK_Villages_Users FOREIGN KEY
	(
	loan_officer
	) REFERENCES dbo.Users
	(
	id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
GO

ALTER TABLE dbo.VillagesPersons
ADD CONSTRAINT PK_VillagesPersons PRIMARY KEY CLUSTERED
        ( village_id, person_id )
        WITH ( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF,
               ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON ) ON [PRIMARY]
GO
	
ALTER TABLE dbo.Packages DROP CONSTRAINT CK_Packages
GO

ALTER TABLE dbo.Packages WITH NOCHECK
ADD CONSTRAINT CK_Packages CHECK NOT FOR REPLICATION ( ( [client_type] = 'I'
                                                         OR [client_type] = 'G'
                                                         OR [client_type] = '-'
                                                         OR [client_type] = 'C'
                                                         OR [client_type] = 'V'
                                                       ) )
GO

ALTER TABLE dbo.ReportObject
ADD report_type NVARCHAR(50)
GO

ALTER TABLE dbo.ReportObject
ADD [description] TEXT
GO

ALTER TABLE dbo.ReportParametrs
ADD value NVARCHAR(250)
GO
CREATE TABLE NumberDayPeriod(
period INT)
GO
INSERT INTO NumberDayPeriod (period) VALUES (15) 
GO
INSERT INTO NumberDayPeriod (period) VALUES (30) 
GO
INSERT INTO NumberDayPeriod (period) VALUES (60) 
GO
INSERT INTO NumberDayPeriod (period) VALUES (90) 
GO
----------------------------------- residualMaturity.rpt -----------------------------------------------
DECLARE @ObjectID INT
DECLARE @SourceID INT
DECLARE @ParametrID INT
INSERT INTO ReportObject (report_name, report_type) VALUES ('residualMaturity.rpt', 'Standard') 
SELECT @ObjectID = @@IDENTITY
INSERT INTO ReportDataObjectSource (report_object_id, sub_report_name, data_object_source, data_object_type) VALUES (@ObjectID, 'residualMaturity.rpt', 'residualMaturity', 'P') 
SELECT @SourceID = @@IDENTITY 
INSERT INTO ReportParametrs (data_object_id, name, type, value) VALUES (@SourceID, '@endDate', 'DateTimeType', '') 
GO
----------------------------------- Repayments.rpt ------------------------------------------------------
DECLARE @ObjectID INT
DECLARE @SourceID INT
DECLARE @ParametrID INT
INSERT INTO ReportObject (report_name, report_type) VALUES ('Repayments.rpt', 'Standard') 
SELECT @ObjectID = @@IDENTITY
INSERT INTO ReportDataObjectSource (report_object_id, sub_report_name, data_object_source, data_object_type) VALUES (@ObjectID, 'Repayments.rpt', 'Repayments_sqlQuery', 'P') 
SELECT @SourceID = @@IDENTITY 
INSERT INTO ReportParametrs (data_object_id, name, type, value) VALUES (@SourceID, '@beginDate', 'DateTimeType', '') 
INSERT INTO ReportParametrs (data_object_id, name, type, value) VALUES (@SourceID, '@endDate', 'DateTimeType', '') 
INSERT INTO ReportParametrs (data_object_id, name, type, value) VALUES (@SourceID, 'INTERNAL_CURRENCY', 'SystemConstantType', '') 
INSERT INTO ReportParametrs (data_object_id, name, type, value) VALUES (@SourceID, 'EXTERNAL_CURRENCY', 'SystemConstantType', '') 
GO
----------------------------------- QualityReport.rpt ------------------------------------------------------
DECLARE @ObjectID INT
DECLARE @SourceID INT
DECLARE @ParametrID INT
INSERT INTO ReportObject (report_name, report_type) VALUES ('QualityReport.rpt', 'Standard') 
SELECT @ObjectID = @@IDENTITY
INSERT INTO ReportDataObjectSource (report_object_id, sub_report_name, data_object_source, data_object_type) VALUES (@ObjectID, 'ContractWithoutLoanOfficer', 'QualityReport_noLoanOfficer', 'V') 
INSERT INTO ReportDataObjectSource (report_object_id, sub_report_name, data_object_source, data_object_type) VALUES (@ObjectID, 'EstablishmentDate', 'QualityReport_EstablishmentDate', 'V') 
INSERT INTO ReportDataObjectSource (report_object_id, sub_report_name, data_object_source, data_object_type) VALUES (@ObjectID, 'Eventdate', 'QualityReport_eventdate', 'V') 
INSERT INTO ReportDataObjectSource (report_object_id, sub_report_name, data_object_source, data_object_type) VALUES (@ObjectID, 'GroupMembers', 'QualityReport_GroupMembers', 'V') 
INSERT INTO ReportDataObjectSource (report_object_id, sub_report_name, data_object_source, data_object_type) VALUES (@ObjectID, 'GroupMembersOld', 'QualityReport_GroupMembersOld', 'V') 
INSERT INTO ReportDataObjectSource (report_object_id, sub_report_name, data_object_source, data_object_type) VALUES (@ObjectID, 'LoanShareAmountClosed.rpt', 'QualityReport_LoanShareAmount', 'V') 
INSERT INTO ReportDataObjectSource (report_object_id, sub_report_name, data_object_source, data_object_type) VALUES (@ObjectID, 'NullLoanShareAmount', 'QualityReport_NullLoanShareAmount', 'V') 
INSERT INTO ReportDataObjectSource (report_object_id, sub_report_name, data_object_source, data_object_type) VALUES (@ObjectID, 'OLBDiscrepancy', 'QualityReport_OLB', 'V') 
INSERT INTO ReportDataObjectSource (report_object_id, sub_report_name, data_object_source, data_object_type) VALUES (@ObjectID, 'SameContractCode', 'QualityReport_SameContractCodes', 'V') 
INSERT INTO ReportDataObjectSource (report_object_id, sub_report_name, data_object_source, data_object_type) VALUES (@ObjectID, 'WeirdContracts', 'QualityReport_WeirdContracts', 'V') 
GO
----------------------------------- PARAnalysis.rpt ------------------------------------------------------
DECLARE @ObjectID INT
DECLARE @SourceID INT
DECLARE @ParametrID INT
INSERT INTO ReportObject (report_name, report_type) VALUES ('PARAnalysis.rpt', 'Standard') 
SELECT @ObjectID = @@IDENTITY
INSERT INTO ReportDataObjectSource (report_object_id, sub_report_name, data_object_source, data_object_type) VALUES (@ObjectID, 'ByDistrict', 'LoansPAR_proc', 'P') 
SELECT @SourceID = @@IDENTITY 
INSERT INTO ReportParametrs (data_object_id, name, type, value) VALUES (@SourceID, '@endDate', 'DateTimeType', '') 
INSERT INTO ReportDataObjectSource (report_object_id, sub_report_name, data_object_source, data_object_type) VALUES (@ObjectID, 'ByLoanOfficer', 'LoansPAR_proc', 'P') 
SELECT @SourceID = @@IDENTITY 
INSERT INTO ReportParametrs (data_object_id, name, type, value) VALUES (@SourceID, '@endDate', 'DateTimeType', '') 
INSERT INTO ReportDataObjectSource (report_object_id, sub_report_name, data_object_source, data_object_type) VALUES (@ObjectID, 'ByProduct', 'LoansPAR_proc', 'P') 
SELECT @SourceID = @@IDENTITY 
INSERT INTO ReportParametrs (data_object_id, name, type, value) VALUES (@SourceID, '@endDate', 'DateTimeType', '') 
GO
----------------------------------- OLB_per_Loan.rpt ------------------------------------------------------
DECLARE @ObjectID INT
DECLARE @SourceID INT
DECLARE @ParametrID INT
INSERT INTO ReportObject (report_name, report_type) VALUES ('OLB_per_Loan.rpt', 'Standard') 
SELECT @ObjectID = @@IDENTITY
INSERT INTO ReportDataObjectSource (report_object_id, sub_report_name, data_object_source, data_object_type) VALUES (@ObjectID, 'OLB_per_Loan.rpt', 'LoanSizeMaturityGraceDomainDistrict_proc', 'P') 
SELECT @SourceID = @@IDENTITY 
INSERT INTO ReportParametrs (data_object_id, name, type, value) VALUES (@SourceID, '@endDate', 'DateTimeType', '') 
INSERT INTO ReportParametrs (data_object_id, name, type, value) VALUES (@SourceID, 'INTERNAL_CURRENCY', 'SystemConstantType', '') 
GO
----------------------------------- NBTquarterly.rpt ------------------------------------------------------
DECLARE @ObjectID INT
DECLARE @SourceID INT
DECLARE @ParametrID INT
INSERT INTO ReportObject (report_name, report_type) VALUES ('NBTquarterly.rpt', 'Standard') 
SELECT @ObjectID = @@IDENTITY
INSERT INTO ReportDataObjectSource (report_object_id, sub_report_name, data_object_source, data_object_type) VALUES (@ObjectID, 'NBTreport.rpt', 'NBT_quarter', 'P') 
SELECT @SourceID = @@IDENTITY 
INSERT INTO ReportParametrs (data_object_id, name, type, value) VALUES (@SourceID, '@beginDate', 'DateTimeType', '') 
INSERT INTO ReportParametrs (data_object_id, name, type, value) VALUES (@SourceID, '@endDate', 'DateTimeType', '') 
INSERT INTO ReportDataObjectSource (report_object_id, sub_report_name, data_object_source, data_object_type) VALUES (@ObjectID, 'activities.rpt', 'NBT_quarter_domain', 'P') 
SELECT @SourceID = @@IDENTITY 
INSERT INTO ReportParametrs (data_object_id, name, type, value) VALUES (@SourceID, '@beginDate', 'DateTimeType', '') 
INSERT INTO ReportParametrs (data_object_id, name, type, value) VALUES (@SourceID, '@endDate', 'DateTimeType', '') 
GO
----------------------------------- nbtmonthly.rpt ------------------------------------------------------
DECLARE @ObjectID INT
DECLARE @SourceID INT
DECLARE @ParametrID INT
INSERT INTO ReportObject (report_name, report_type) VALUES ('nbtmonthly.rpt', 'Standard') 
SELECT @ObjectID = @@IDENTITY
INSERT INTO ReportDataObjectSource (report_object_id, sub_report_name, data_object_source, data_object_type) VALUES (@ObjectID, 'nbtmonthly.rpt', 'NBT_monthly', 'P') 
SELECT @SourceID = @@IDENTITY 
INSERT INTO ReportParametrs (data_object_id, name, type, value) VALUES (@SourceID, '@beginDate', 'DateTimeType', '') 
INSERT INTO ReportParametrs (data_object_id, name, type, value) VALUES (@SourceID, '@endDate', 'DateTimeType', '') 
GO
----------------------------------- LoansDisbursed.rpt ------------------------------------------------------
DECLARE @ObjectID INT
DECLARE @SourceID INT
DECLARE @ParametrID INT
INSERT INTO ReportObject (report_name, report_type) VALUES ('LoansDisbursed.rpt', 'Standard') 
SELECT @ObjectID = @@IDENTITY
INSERT INTO ReportDataObjectSource (report_object_id, sub_report_name, data_object_source, data_object_type) VALUES (@ObjectID, 'ByActivity', 'loansDisbursed', 'P') 
SELECT @SourceID = @@IDENTITY 
INSERT INTO ReportParametrs (data_object_id, name, type, value) VALUES (@SourceID, '@beginDate', 'DateTimeType', '') 
INSERT INTO ReportParametrs (data_object_id, name, type, value) VALUES (@SourceID, '@endDate', 'DateTimeType', '') 
INSERT INTO ReportDataObjectSource (report_object_id, sub_report_name, data_object_source, data_object_type) VALUES (@ObjectID, 'ByDistrict', 'loansDisbursed', 'P') 
SELECT @SourceID = @@IDENTITY 
INSERT INTO ReportParametrs (data_object_id, name, type, value) VALUES (@SourceID, '@beginDate', 'DateTimeType', '') 
INSERT INTO ReportParametrs (data_object_id, name, type, value) VALUES (@SourceID, '@endDate', 'DateTimeType', '') 
INSERT INTO ReportDataObjectSource (report_object_id, sub_report_name, data_object_source, data_object_type) VALUES (@ObjectID, 'ByLoanOfficer', 'loansDisbursed', 'P') 
SELECT @SourceID = @@IDENTITY 
INSERT INTO ReportParametrs (data_object_id, name, type, value) VALUES (@SourceID, '@beginDate', 'DateTimeType', '') 
INSERT INTO ReportParametrs (data_object_id, name, type, value) VALUES (@SourceID, '@endDate', 'DateTimeType', '') 
INSERT INTO ReportDataObjectSource (report_object_id, sub_report_name, data_object_source, data_object_type) VALUES (@ObjectID, 'ByProduct', 'loansDisbursed', 'P') 
SELECT @SourceID = @@IDENTITY 
INSERT INTO ReportParametrs (data_object_id, name, type, value) VALUES (@SourceID, '@beginDate', 'DateTimeType', '') 
INSERT INTO ReportParametrs (data_object_id, name, type, value) VALUES (@SourceID, '@endDate', 'DateTimeType', '') 
GO
----------------------------------- FollowUp.rpt ------------------------------------------------------
DECLARE @ObjectID INT
DECLARE @SourceID INT
DECLARE @ParametrID INT
INSERT INTO ReportObject (report_name, report_type) VALUES ('FollowUp.rpt', 'Standard') 
SELECT @ObjectID = @@IDENTITY
INSERT INTO ReportDataObjectSource (report_object_id, sub_report_name, data_object_source, data_object_type) VALUES (@ObjectID, 'FollowUpPAR.rpt', 'LoansPAR_proc', 'P') 
SELECT @SourceID = @@IDENTITY 
INSERT INTO ReportParametrs (data_object_id, name, type, value) VALUES (@SourceID, '@endDate', 'DateTimeType', '') 
INSERT INTO ReportDataObjectSource (report_object_id, sub_report_name, data_object_source, data_object_type) VALUES (@ObjectID, 'followuprd.rpt', 'FollowUp_PrincipalAndInterest', 'P') 
SELECT @SourceID = @@IDENTITY 
INSERT INTO ReportParametrs (data_object_id, name, type, value) VALUES (@SourceID, '@beginDate', 'DateTimeType', '') 
INSERT INTO ReportParametrs (data_object_id, name, type, value) VALUES (@SourceID, '@endDate', 'DateTimeType', '') 
GO
----------------------------------- FinancialStock.rpt ------------------------------------------------------
DECLARE @ObjectID INT
DECLARE @SourceID INT
DECLARE @ParametrID INT
INSERT INTO ReportObject (report_name, report_type) VALUES ('FinancialStock.rpt', 'Standard') 
SELECT @ObjectID = @@IDENTITY
INSERT INTO ReportDataObjectSource (report_object_id, sub_report_name, data_object_source, data_object_type) VALUES (@ObjectID, 'Assets', 'FinancialStock_Assets', 'V') 
SELECT @SourceID = @@IDENTITY 
INSERT INTO ReportParametrs (data_object_id, name, type, value) VALUES (@SourceID, 'INTERNAL_CURRENCY', 'SystemConstantType', '') 
INSERT INTO ReportDataObjectSource (report_object_id, sub_report_name, data_object_source, data_object_type) VALUES (@ObjectID, 'Expenses', 'FinancialStock_Expenses', 'V') 
SELECT @SourceID = @@IDENTITY 
INSERT INTO ReportParametrs (data_object_id, name, type, value) VALUES (@SourceID, 'INTERNAL_CURRENCY', 'SystemConstantType', '') 
INSERT INTO ReportDataObjectSource (report_object_id, sub_report_name, data_object_source, data_object_type) VALUES (@ObjectID, 'Income', 'FinancialStock_Income', 'V') 
SELECT @SourceID = @@IDENTITY 
INSERT INTO ReportParametrs (data_object_id, name, type, value) VALUES (@SourceID, 'INTERNAL_CURRENCY', 'SystemConstantType', '') 
INSERT INTO ReportDataObjectSource (report_object_id, sub_report_name, data_object_source, data_object_type) VALUES (@ObjectID, 'Liabilities', 'FinancialStock_Liabilities', 'V') 
SELECT @SourceID = @@IDENTITY 
INSERT INTO ReportParametrs (data_object_id, name, type, value) VALUES (@SourceID, 'INTERNAL_CURRENCY', 'SystemConstantType', '') 
GO
----------------------------------- DropOut_months.rpt ------------------------------------------------------
DECLARE @ObjectID INT
DECLARE @SourceID INT
DECLARE @ParametrID INT
INSERT INTO ReportObject (report_name, report_type, description) VALUES ('DropOut_months.rpt', 'Standard', 'This report will display all contracts closed between @beginDate and @endDate.Renewed contracts are contracts that have been reconducted after @period days. The calculation of drop-out is as following: (1- (closed contracts /renewed contracts))*100.The same applies to loans.') 
SELECT @ObjectID = @@IDENTITY
INSERT INTO ReportDataObjectSource (report_object_id, sub_report_name, data_object_source, data_object_type) VALUES (@ObjectID, 'ByDistrict', 'DropOut_byDistrict', 'P') 
SELECT @SourceID = @@IDENTITY 
INSERT INTO ReportParametrs (data_object_id, name, type, value) VALUES (@SourceID, '@beginDate', 'DateTimeType', '') 
INSERT INTO ReportParametrs (data_object_id, name, type, value) VALUES (@SourceID, '@endDate', 'DateTimeType', '') 
INSERT INTO ReportParametrs (data_object_id, name, type, value) VALUES (@SourceID, '@period', 'LookupType', '') 
SELECT @ParametrID = @@IDENTITY
INSERT INTO ReportLookUpFields (parametr_id, field_to_send, field_to_show, data_object) VALUES (@ParametrID, 'period', 'period', 'NumberDayPeriod')
INSERT INTO ReportDataObjectSource (report_object_id, sub_report_name, data_object_source, data_object_type) VALUES (@ObjectID, 'ByLoanOfficer', 'DropOut_byLoanOfficer', 'P') 
SELECT @SourceID = @@IDENTITY 
INSERT INTO ReportParametrs (data_object_id, name, type, value) VALUES (@SourceID, '@beginDate', 'DateTimeType', '') 
INSERT INTO ReportParametrs (data_object_id, name, type, value) VALUES (@SourceID, '@endDate', 'DateTimeType', '') 
INSERT INTO ReportParametrs (data_object_id, name, type, value) VALUES (@SourceID, '@period', 'LookupType', '') 
SELECT @ParametrID = @@IDENTITY
INSERT INTO ReportLookUpFields (parametr_id, field_to_send, field_to_show, data_object) VALUES (@ParametrID, 'period', 'period', 'NumberDayPeriod')
INSERT INTO ReportDataObjectSource (report_object_id, sub_report_name, data_object_source, data_object_type) VALUES (@ObjectID, 'ByProduct', 'DropOut_byProduct', 'P') 
SELECT @SourceID = @@IDENTITY 
INSERT INTO ReportParametrs (data_object_id, name, type, value) VALUES (@SourceID, '@beginDate', 'DateTimeType', '') 
INSERT INTO ReportParametrs (data_object_id, name, type, value) VALUES (@SourceID, '@endDate', 'DateTimeType', '') 
INSERT INTO ReportParametrs (data_object_id, name, type, value) VALUES (@SourceID, '@period', 'LookupType', '') 
SELECT @ParametrID = @@IDENTITY
INSERT INTO ReportLookUpFields (parametr_id, field_to_send, field_to_show, data_object) VALUES (@ParametrID, 'period', 'period', 'NumberDayPeriod')
GO
----------------------------------- Disbursments.rpt ------------------------------------------------------
DECLARE @ObjectID INT
DECLARE @SourceID INT
DECLARE @ParametrID INT
INSERT INTO ReportObject (report_name, report_type) VALUES ('Disbursments.rpt', 'Standard') 
SELECT @ObjectID = @@IDENTITY
INSERT INTO ReportDataObjectSource (report_object_id, sub_report_name, data_object_source, data_object_type) VALUES (@ObjectID, 'Disbursments.rpt', 'Disbursments_sqlQuery', 'P') 
SELECT @SourceID = @@IDENTITY 
INSERT INTO ReportParametrs (data_object_id, name, type, value) VALUES (@SourceID, '@beginDate', 'DateTimeType', '') 
INSERT INTO ReportParametrs (data_object_id, name, type, value) VALUES (@SourceID, '@endDate', 'DateTimeType', '') 
INSERT INTO ReportParametrs (data_object_id, name, type, value) VALUES (@SourceID, 'INTERNAL_CURRENCY', 'SystemConstantType', '') 
INSERT INTO ReportParametrs (data_object_id, name, type, value) VALUES (@SourceID, 'EXTERNAL_CURRENCY', 'SystemConstantType', '') 
GO
----------------------------------- Disbursements_and_Reimbursements.rpt ------------------------------------------------------
DECLARE @ObjectID INT
DECLARE @SourceID INT
DECLARE @ParametrID INT
INSERT INTO ReportObject (report_name, report_type) VALUES ('Disbursements_and_Reimbursements.rpt', 'Standard') 
SELECT @ObjectID = @@IDENTITY
INSERT INTO ReportDataObjectSource (report_object_id, sub_report_name, data_object_source, data_object_type) VALUES (@ObjectID, 'ByDistrict', 'Disbursements_and_Reimbursements', 'P') 
SELECT @SourceID = @@IDENTITY 
INSERT INTO ReportParametrs (data_object_id, name, type, value) VALUES (@SourceID, '@beginDate', 'DateTimeType', '') 
INSERT INTO ReportParametrs (data_object_id, name, type, value) VALUES (@SourceID, '@endDate', 'DateTimeType', '') 
INSERT INTO ReportDataObjectSource (report_object_id, sub_report_name, data_object_source, data_object_type) VALUES (@ObjectID, 'ByLoanOfficer', 'Disbursements_and_Reimbursements', 'P') 
SELECT @SourceID = @@IDENTITY 
INSERT INTO ReportParametrs (data_object_id, name, type, value) VALUES (@SourceID, '@beginDate', 'DateTimeType', '') 
INSERT INTO ReportParametrs (data_object_id, name, type, value) VALUES (@SourceID, '@endDate', 'DateTimeType', '') 
INSERT INTO ReportDataObjectSource (report_object_id, sub_report_name, data_object_source, data_object_type) VALUES (@ObjectID, 'ByProduct', 'Disbursements_and_Reimbursements', 'P') 
SELECT @SourceID = @@IDENTITY 
INSERT INTO ReportParametrs (data_object_id, name, type, value) VALUES (@SourceID, '@beginDate', 'DateTimeType', '') 
INSERT INTO ReportParametrs (data_object_id, name, type, value) VALUES (@SourceID, '@endDate', 'DateTimeType', '') 
GO
----------------------------------- DelinquentLoans.rpt ------------------------------------------------------
DECLARE @ObjectID INT
DECLARE @SourceID INT
DECLARE @ParametrID INT
INSERT INTO ReportObject (report_name, report_type) VALUES ('DelinquentLoans.rpt', 'Standard') 
SELECT @ObjectID = @@IDENTITY
INSERT INTO ReportDataObjectSource (report_object_id, sub_report_name, data_object_source, data_object_type) VALUES (@ObjectID, 'DelinquentLoans.rpt', 'DelinquentLoans_sqlQuery', 'P') 
SELECT @SourceID = @@IDENTITY 
INSERT INTO ReportParametrs (data_object_id, name, type, value) VALUES (@SourceID, '@endDate', 'DateTimeType', '') 
INSERT INTO ReportParametrs (data_object_id, name, type, value) VALUES (@SourceID, 'INTERNAL_CURRENCY', 'SystemConstantType', '') 
GO
----------------------------------- ClientsAndShareOfWomen.rpt ------------------------------------------------------
DECLARE @ObjectID INT
DECLARE @SourceID INT
DECLARE @ParametrID INT
INSERT INTO ReportObject (report_name, report_type) VALUES ('ClientsAndShareOfWomen.rpt', 'Standard') 
SELECT @ObjectID = @@IDENTITY
INSERT INTO ReportDataObjectSource (report_object_id, sub_report_name, data_object_source, data_object_type) VALUES (@ObjectID, 'ByDistrict', 'ClientsAndShareOfWomen_ByDistrict', 'P') 
SELECT @SourceID = @@IDENTITY 
INSERT INTO ReportParametrs (data_object_id, name, type, value) VALUES (@SourceID, '@endDate', 'DateTimeType', '') 
INSERT INTO ReportDataObjectSource (report_object_id, sub_report_name, data_object_source, data_object_type) VALUES (@ObjectID, 'ByLoanOfficer', 'ClientsAndShareOfWomen_ByLoanOfficer', 'P') 
SELECT @SourceID = @@IDENTITY 
INSERT INTO ReportParametrs (data_object_id, name, type, value) VALUES (@SourceID, '@endDate', 'DateTimeType', '') 
INSERT INTO ReportDataObjectSource (report_object_id, sub_report_name, data_object_source, data_object_type) VALUES (@ObjectID, 'ByProduct', 'ClientsAndShareOfWomen_ByProduct', 'P') 
SELECT @SourceID = @@IDENTITY 
INSERT INTO ReportParametrs (data_object_id, name, type, value) VALUES (@SourceID, '@endDate', 'DateTimeType', '') 
GO
----------------------------------- ClientsAndShareOfWomen.rpt ------------------------------------------------------
DECLARE @ObjectID INT
DECLARE @SourceID INT
DECLARE @ParametrID INT
INSERT INTO ReportObject (report_name, report_type) VALUES ('ClientAndLoansStatistics.rpt', 'Standard') 
SELECT @ObjectID = @@IDENTITY
INSERT INTO ReportDataObjectSource (report_object_id, sub_report_name, data_object_source, data_object_type) VALUES (@ObjectID, 'LoanActivity', 'LoanSizeMaturityGraceDomainDistrict_proc', 'P') 
SELECT @SourceID = @@IDENTITY 
INSERT INTO ReportParametrs (data_object_id, name, type, value) VALUES (@SourceID, '@endDate', 'DateTimeType', '') 
INSERT INTO ReportDataObjectSource (report_object_id, sub_report_name, data_object_source, data_object_type) VALUES (@ObjectID, 'LoanDistrict', 'LoanSizeMaturityGraceDomainDistrict_proc', 'P') 
SELECT @SourceID = @@IDENTITY 
INSERT INTO ReportParametrs (data_object_id, name, type, value) VALUES (@SourceID, '@endDate', 'DateTimeType', '') 
INSERT INTO ReportDataObjectSource (report_object_id, sub_report_name, data_object_source, data_object_type) VALUES (@ObjectID, 'LoanGender', 'LoanSizeMaturityGraceDomainDistrict_proc', 'P') 
SELECT @SourceID = @@IDENTITY 
INSERT INTO ReportParametrs (data_object_id, name, type, value) VALUES (@SourceID, '@endDate', 'DateTimeType', '') 
INSERT INTO ReportDataObjectSource (report_object_id, sub_report_name, data_object_source, data_object_type) VALUES (@ObjectID, 'LoanGrace', 'LoanSizeMaturityGraceDomainDistrict_proc', 'P') 
SELECT @SourceID = @@IDENTITY 
INSERT INTO ReportParametrs (data_object_id, name, type, value) VALUES (@SourceID, '@endDate', 'DateTimeType', '') 
INSERT INTO ReportDataObjectSource (report_object_id, sub_report_name, data_object_source, data_object_type) VALUES (@ObjectID, 'LoanMaturity', 'LoanSizeMaturityGraceDomainDistrict_proc', 'P') 
SELECT @SourceID = @@IDENTITY 
INSERT INTO ReportParametrs (data_object_id, name, type, value) VALUES (@SourceID, '@endDate', 'DateTimeType', '') 
INSERT INTO ReportDataObjectSource (report_object_id, sub_report_name, data_object_source, data_object_type) VALUES (@ObjectID, 'LoanSizePerLoanScale', 'LoanSizeMaturityGraceDomainDistrict_proc', 'P') 
SELECT @SourceID = @@IDENTITY 
INSERT INTO ReportParametrs (data_object_id, name, type, value) VALUES (@SourceID, '@endDate', 'DateTimeType', '') 
INSERT INTO ReportDataObjectSource (report_object_id, sub_report_name, data_object_source, data_object_type) VALUES (@ObjectID, 'LoansSize', 'LoanSizeMaturityGraceDomainDistrict_proc', 'P') 
SELECT @SourceID = @@IDENTITY 
INSERT INTO ReportParametrs (data_object_id, name, type, value) VALUES (@SourceID, '@endDate', 'DateTimeType', '') 
GO
----------------------------------- ActiveLoans.rpt ------------------------------------------------------
DECLARE @ObjectID INT
DECLARE @SourceID INT
DECLARE @ParametrID INT
INSERT INTO ReportObject (report_name, report_type) VALUES ('ActiveLoans.rpt', 'Standard') 
SELECT @ObjectID = @@IDENTITY
INSERT INTO ReportDataObjectSource (report_object_id, sub_report_name, data_object_source, data_object_type) VALUES (@ObjectID, 'ByActivity', 'LoanSizeMaturityGraceDomainDistrict_proc', 'P') 
SELECT @SourceID = @@IDENTITY 
INSERT INTO ReportParametrs (data_object_id, name, type, value) VALUES (@SourceID, '@endDate', 'DateTimeType', '') 
INSERT INTO ReportDataObjectSource (report_object_id, sub_report_name, data_object_source, data_object_type) VALUES (@ObjectID, 'ByDistrict', 'LoanSizeMaturityGraceDomainDistrict_proc', 'P') 
SELECT @SourceID = @@IDENTITY 
INSERT INTO ReportParametrs (data_object_id, name, type, value) VALUES (@SourceID, '@endDate', 'DateTimeType', '') 
INSERT INTO ReportDataObjectSource (report_object_id, sub_report_name, data_object_source, data_object_type) VALUES (@ObjectID, 'ByLoanOfficer', 'LoanSizeMaturityGraceDomainDistrict_proc', 'P') 
SELECT @SourceID = @@IDENTITY 
INSERT INTO ReportParametrs (data_object_id, name, type, value) VALUES (@SourceID, '@endDate', 'DateTimeType', '') 
INSERT INTO ReportDataObjectSource (report_object_id, sub_report_name, data_object_source, data_object_type) VALUES (@ObjectID, 'ByProduct', 'LoanSizeMaturityGraceDomainDistrict_proc', 'P') 
SELECT @SourceID = @@IDENTITY 
INSERT INTO ReportParametrs (data_object_id, name, type, value) VALUES (@SourceID, '@endDate', 'DateTimeType', '') 
INSERT INTO ReportDataObjectSource (report_object_id, sub_report_name, data_object_source, data_object_type) VALUES (@ObjectID, 'ByRangeOfAmount', 'LoanSizeMaturityGraceDomainDistrict_proc', 'P') 
SELECT @SourceID = @@IDENTITY 
INSERT INTO ReportParametrs (data_object_id, name, type, value) VALUES (@SourceID, '@endDate', 'DateTimeType', '') 
INSERT INTO ReportDataObjectSource (report_object_id, sub_report_name, data_object_source, data_object_type) VALUES (@ObjectID, 'ByRangeOfInterestRate', 'LoanSizeMaturityGraceDomainDistrict_proc', 'P') 
SELECT @SourceID = @@IDENTITY 
INSERT INTO ReportParametrs (data_object_id, name, type, value) VALUES (@SourceID, '@endDate', 'DateTimeType', '') 
INSERT INTO ReportDataObjectSource (report_object_id, sub_report_name, data_object_source, data_object_type) VALUES (@ObjectID, 'ByRangeOfLoanScale', 'LoanSizeMaturityGraceDomainDistrict_proc', 'P') 
SELECT @SourceID = @@IDENTITY 
INSERT INTO ReportParametrs (data_object_id, name, type, value) VALUES (@SourceID, '@endDate', 'DateTimeType', '') 
GO

-- Internal repayment schedules
DECLARE @ObjectID INT
DECLARE @SourceID INT
INSERT INTO ReportObject (report_name, report_type) VALUES ('GroupRepaymentSchedule.rpt', 'Internal') 
SELECT @ObjectID = @@IDENTITY
INSERT INTO ReportDataObjectSource (report_object_id, sub_report_name, data_object_source, data_object_type) VALUES (@ObjectID, 'GroupRepaymentSchedule.rpt', 'GroupRepaymentScheduleInfo', 'P') 
SELECT @SourceID = @@IDENTITY 
INSERT INTO ReportParametrs (data_object_id, name, type, value) VALUES (@SourceID, '@contract_id', 'TextType', '0') 
INSERT INTO ReportDataObjectSource (report_object_id, sub_report_name, data_object_source, data_object_type) VALUES (@ObjectID, 'RepaymentSchedule.rpt', 'RepaymentSchedule', 'P') 
SELECT @SourceID = @@IDENTITY 
INSERT INTO ReportParametrs (data_object_id, name, type, value) VALUES (@SourceID, '@contract_id', 'TextType', '0') 

INSERT INTO ReportObject (report_name, report_type) VALUES ('IndividualRepaymentSchedule.rpt', 'Internal') 
SELECT @ObjectID = @@IDENTITY
INSERT INTO ReportDataObjectSource (report_object_id, sub_report_name, data_object_source, data_object_type) VALUES (@ObjectID, 'IndividualRepaymentSchedule.rpt', 'IndividualRepaymentScheduleInfo', 'P') 
SELECT @SourceID = @@IDENTITY 
INSERT INTO ReportParametrs (data_object_id, name, type, value) VALUES (@SourceID, '@contract_id', 'TextType', '0') 
INSERT INTO ReportDataObjectSource (report_object_id, sub_report_name, data_object_source, data_object_type) VALUES (@ObjectID, 'RepaymentSchedule.rpt', 'RepaymentSchedule', 'P') 
SELECT @SourceID = @@IDENTITY 
INSERT INTO ReportParametrs (data_object_id, name, type, value) VALUES (@SourceID, '@contract_id', 'TextType', '0') 

INSERT INTO ReportObject (report_name, report_type) VALUES ('CorporateRepaymentSchedule.rpt', 'Internal') 
SELECT @ObjectID = @@IDENTITY
INSERT INTO ReportDataObjectSource (report_object_id, sub_report_name, data_object_source, data_object_type) VALUES (@ObjectID, 'CorporateRepaymentSchedule.rpt', 'CorporateRepaymentScheduleInfo', 'P') 
SELECT @SourceID = @@IDENTITY 
INSERT INTO ReportParametrs (data_object_id, name, type, value) VALUES (@SourceID, '@contract_id', 'TextType', '0') 
INSERT INTO ReportDataObjectSource (report_object_id, sub_report_name, data_object_source, data_object_type) VALUES (@ObjectID, 'RepaymentSchedule.rpt', 'RepaymentSchedule', 'P') 
SELECT @SourceID = @@IDENTITY 
INSERT INTO ReportParametrs (data_object_id, name, type, value) VALUES (@SourceID, '@contract_id', 'TextType', '0') 
GO

UPDATE  [TechnicalParameters] SET [value] = 'v2.5.7'
GO