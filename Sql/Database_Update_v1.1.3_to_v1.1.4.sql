/************** VERSION ***********************/
UPDATE [TechnicalParameters] SET [value]='v1.1.4'
GO

ALTER TABLE dbo.Conso_Customers ADD
	first_name nvarchar(50) NOT NULL CONSTRAINT DF_Conso_Customers_first_name DEFAULT N'None',
	last_name nvarchar(100) NOT NULL CONSTRAINT DF_Conso_Customers_last_name DEFAULT N'None',
	father_name nvarchar(100) NULL CONSTRAINT DF_Conso_Customers_father_name DEFAULT N'None'
GO

CREATE TABLE [dbo].[CustomizableFieldsSettings](
	[number] [int] NOT NULL,
	[use] [bit] NOT NULL,
	[name] [nvarchar](100) NOT NULL,
	[type] [nvarchar](50) NOT NULL,
	[mandatory] [bit] NOT NULL,
 CONSTRAINT [PK_CustomizableFields] PRIMARY KEY CLUSTERED 
(
	[number] ASC
) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[PersonCustomizableFields](
	[person_id] [int] NOT NULL,
	[key] [int] NOT NULL,
	[value] [nvarchar](100) NULL
) ON [PRIMARY]
GO

INSERT INTO [CustomizableFieldsSettings]([number],[use],[name],[type],[mandatory]) VALUES(1,0,'','',0)
INSERT INTO [CustomizableFieldsSettings]([number],[use],[name],[type],[mandatory]) VALUES(2,0,'','',0)
INSERT INTO [CustomizableFieldsSettings]([number],[use],[name],[type],[mandatory]) VALUES(3,0,'','',0)
INSERT INTO [CustomizableFieldsSettings]([number],[use],[name],[type],[mandatory]) VALUES(4,0,'','',0)
INSERT INTO [CustomizableFieldsSettings]([number],[use],[name],[type],[mandatory]) VALUES(5,0,'','',0)
INSERT INTO [CustomizableFieldsSettings]([number],[use],[name],[type],[mandatory]) VALUES(6,0,'','',0)
INSERT INTO [CustomizableFieldsSettings]([number],[use],[name],[type],[mandatory]) VALUES(7,0,'','',0)
INSERT INTO [CustomizableFieldsSettings]([number],[use],[name],[type],[mandatory]) VALUES(8,0,'','',0)
INSERT INTO [CustomizableFieldsSettings]([number],[use],[name],[type],[mandatory]) VALUES(9,0,'','',0)
INSERT INTO [CustomizableFieldsSettings]([number],[use],[name],[type],[mandatory]) VALUES(10,0,'','',0)
GO
/*Views*/
/********************/
/****** VIEWS *******/
/********************/

/****** Objet :  View [dbo].[LoansPAR]    Date de génération du script : 08/21/2007 13:34:50 ******/
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[LoansPAR]') AND OBJECTPROPERTY(id, N'IsView') = 1)
DROP VIEW [dbo].[LoansPAR]
GO
/****** Objet :  View [dbo].[ILoansPAR]    Date de génération du script : 08/21/2007 13:34:50 ******/
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[ILoansPAR]') AND OBJECTPROPERTY(id, N'IsView') = 1)
DROP VIEW [dbo].[ILoansPAR]
GO
/****** Objet :  View [dbo].[GLoansPAR]    Date de génération du script : 08/21/2007 13:34:50 ******/
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[GLoansPAR]') AND OBJECTPROPERTY(id, N'IsView') = 1)
DROP VIEW [dbo].[GLoansPAR]
GO

/****** Objet :  View [dbo].[GLoanSizeMaturityGraceDomainDistrict]    Date de génération du script : 08/21/2007 13:34:50 ******/
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[GLoanSizeMaturityGraceDomainDistrict]') AND OBJECTPROPERTY(id, N'IsView') = 1)
DROP VIEW [dbo].[GLoanSizeMaturityGraceDomainDistrict]
GO
/****** Objet :  View [dbo].[LoanSizeMaturityGraceDomainDistrict]    Date de génération du script : 08/21/2007 13:34:52 ******/
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[LoanSizeMaturityGraceDomainDistrict]') AND OBJECTPROPERTY(id, N'IsView') = 1)
DROP VIEW [dbo].[LoanSizeMaturityGraceDomainDistrict]
GO
/****** Objet :  View [dbo].[PortFolioEvolutionView]    Date de génération du script : 08/21/2007 13:34:53 ******/
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[PortFolioEvolutionView]') AND OBJECTPROPERTY(id, N'IsView') = 1)
DROP VIEW [dbo].[PortFolioEvolutionView]
GO
/****** Objet :  View [dbo].[PAREvolution]    Date de génération du script : 08/21/2007 13:34:52 ******/
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[PAREvolution]') AND OBJECTPROPERTY(id, N'IsView') = 1)
DROP VIEW [dbo].[PAREvolution]
GO
/****** Objet :  View [dbo].[Conso_ILoanSizeMaturityGraceDomainDistrict]    Date de génération du script : 08/21/2007 13:34:49 ******/
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[Conso_ILoanSizeMaturityGraceDomainDistrict]') AND OBJECTPROPERTY(id, N'IsView') = 1)
DROP VIEW [dbo].[Conso_ILoanSizeMaturityGraceDomainDistrict]
GO
/****** Objet :  View [dbo].[ReportPARView]    Date de génération du script : 08/21/2007 13:34:54 ******/
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[ReportPARView]') AND OBJECTPROPERTY(id, N'IsView') = 1)
DROP VIEW [dbo].[ReportPARView]
GO
/****** Objet :  View [dbo].[LoanSizeMaturityGraceDomainDistrict_FullyRepaid]    Date de génération du script : 08/21/2007 13:34:52 ******/
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[LoanSizeMaturityGraceDomainDistrict_FullyRepaid]') AND OBJECTPROPERTY(id, N'IsView') = 1)
DROP VIEW [dbo].[LoanSizeMaturityGraceDomainDistrict_FullyRepaid]
GO
/****** Objet :  View [dbo].[Conso_LoanSizeMaturityGraceDomainDistrict]    Date de génération du script : 08/21/2007 13:34:50 ******/
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[Conso_LoanSizeMaturityGraceDomainDistrict]') AND OBJECTPROPERTY(id, N'IsView') = 1)
DROP VIEW [dbo].[Conso_LoanSizeMaturityGraceDomainDistrict]
GO
/****** Objet :  View [dbo].[PortfolioAndPAREvolutionByBranchLast12MonthsView]    Date de génération du script : 08/21/2007 13:34:53 ******/
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[PortfolioAndPAREvolutionByBranchLast12MonthsView]') AND OBJECTPROPERTY(id, N'IsView') = 1)
DROP VIEW [dbo].[PortfolioAndPAREvolutionByBranchLast12MonthsView]
GO

/****** Objet :  View [dbo].[PortfolioAndPAREvolutionByHeadQuarterLast12MonthsView]    Date de génération du script : 08/21/2007 13:34:53 ******/
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[PortfolioAndPAREvolutionByHeadQuarterLast12MonthsView]') AND OBJECTPROPERTY(id, N'IsView') = 1)
DROP VIEW [dbo].[PortfolioAndPAREvolutionByHeadQuarterLast12MonthsView]
GO
/****** Objet :  View [dbo].[Conso_LoansPAR]    Date de génération du script : 08/21/2007 13:34:50 ******/
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[Conso_LoansPAR]') AND OBJECTPROPERTY(id, N'IsView') = 1)
DROP VIEW [dbo].[Conso_LoansPAR]
GO
/****** Objet :  View [dbo].[ILoanSizeMaturityGraceDomainDistrict]    Date de génération du script : 08/21/2007 13:34:51 ******/
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[ILoanSizeMaturityGraceDomainDistrict]') AND OBJECTPROPERTY(id, N'IsView') = 1)
DROP VIEW [dbo].[ILoanSizeMaturityGraceDomainDistrict]
GO
/****** Objet :  View [dbo].[ILoanSizeMaturityGraceDomainDistrict_FullyRepaid]    Date de génération du script : 08/21/2007 13:34:51 ******/
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[ILoanSizeMaturityGraceDomainDistrict_FullyRepaid]') AND OBJECTPROPERTY(id, N'IsView') = 1)
DROP VIEW [dbo].[ILoanSizeMaturityGraceDomainDistrict_FullyRepaid]
GO
/****** Objet :  View [dbo].[GLoanSizeMaturityGraceDomainDistrict_FullyRepaid]    Date de génération du script : 08/21/2007 13:34:50 ******/
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[GLoanSizeMaturityGraceDomainDistrict_FullyRepaid]') AND OBJECTPROPERTY(id, N'IsView') = 1)
DROP VIEW [dbo].[GLoanSizeMaturityGraceDomainDistrict_FullyRepaid]
GO
/****** Objet :  View [dbo].[Conso_GLoanSizeMaturityGraceDomainDistrict]    Date de génération du script : 08/21/2007 13:34:49 ******/
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[Conso_GLoanSizeMaturityGraceDomainDistrict]') AND OBJECTPROPERTY(id, N'IsView') = 1)
DROP VIEW [dbo].[Conso_GLoanSizeMaturityGraceDomainDistrict]
GO
/****** Objet :  View [dbo].[PortfolioAndPAREvolutionByHeadQuarterView]    Date de génération du script : 08/21/2007 13:34:53 ******/
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[PortfolioAndPAREvolutionByHeadQuarterView]') AND OBJECTPROPERTY(id, N'IsView') = 1)
DROP VIEW [dbo].[PortfolioAndPAREvolutionByHeadQuarterView]
GO
/****** Objet :  View [dbo].[Conso_GLoansPAR]    Date de génération du script : 08/21/2007 13:34:49 ******/
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[Conso_GLoansPAR]') AND OBJECTPROPERTY(id, N'IsView') = 1)
DROP VIEW [dbo].[Conso_GLoansPAR]
GO
/****** Objet :  View [dbo].[Conso_ILoansPAR]    Date de génération du script : 08/21/2007 13:34:49 ******/
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[Conso_ILoansPAR]') AND OBJECTPROPERTY(id, N'IsView') = 1)
DROP VIEW [dbo].[Conso_ILoansPAR]
GO
/****** Objet :  View [dbo].[ReportInstallmentsView]    Date de génération du script : 08/21/2007 13:34:53 ******/
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[ReportInstallmentsView]') AND OBJECTPROPERTY(id, N'IsView') = 1)
DROP VIEW [dbo].[ReportInstallmentsView]
GO
/****** Objet :  View [dbo].[PortfolioAndPAREvolutionView]    Date de génération du script : 08/21/2007 13:34:53 ******/
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[PortfolioAndPAREvolutionView]') AND OBJECTPROPERTY(id, N'IsView') = 1)
DROP VIEW [dbo].[PortfolioAndPAREvolutionView]
GO
/********************************************/
/******************  MINE  ******************/
/********************************************/

/*************************/
/*******  Common  ********/
/*************************/
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[InternalCurrency]') AND OBJECTPROPERTY(id, N'IsView') = 1)
DROP VIEW [dbo].[InternalCurrency]
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[ExternalCurrency]') AND OBJECTPROPERTY(id, N'IsView') = 1)
DROP VIEW [dbo].[ExternalCurrency]
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[OLBView]') AND OBJECTPROPERTY(id, N'IsView') = 1)
DROP VIEW [dbo].[OLBView]
GO
/*************************/
/*** DormantCustomers  ***/
/*************************/
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[DormantCustomersView]') AND OBJECTPROPERTY(id, N'IsView') = 1)
DROP VIEW [dbo].[DormantCustomersView]
GO
/*************************/
/***   PortfolioQuality **/
/*************************/
/****** Objet :  View [dbo].[OverDueView]    Date de génération du script : 08/21/2007 13:34:52 ******/
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[OverDueView]') AND OBJECTPROPERTY(id, N'IsView') = 1)
DROP VIEW [dbo].[OverDueView]
GO
/****** Objet :  View [dbo].[CurrentRepaymentRateView]    Date de génération du script : 08/21/2007 13:34:50 ******/
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[CurrentRepaymentRateView]') AND OBJECTPROPERTY(id, N'IsView') = 1)
DROP VIEW [dbo].[CurrentRepaymentRateView]
GO
/*************************/
/*****  OLB_Per_Loan *****/
/*************************/
/****** Objet :  View [dbo].[OLBPerLoanView]    Date de génération du script : 08/21/2007 13:34:52 ******/
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[OLBPerLoanView]') AND OBJECTPROPERTY(id, N'IsView') = 1)
DROP VIEW [dbo].[OLBPerLoanView]
GO
/*************************/
/**** FinancialStock  ****/
/*************************/
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[FinancialStock_Assets]') AND OBJECTPROPERTY(id, N'IsView') = 1)
DROP VIEW [dbo].[FinancialStock_Assets]
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[FinancialStock_Liabilities]') AND OBJECTPROPERTY(id, N'IsView') = 1)
DROP VIEW [dbo].[FinancialStock_Liabilities]
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[FinancialStock_Income]') AND OBJECTPROPERTY(id, N'IsView') = 1)
DROP VIEW [dbo].[FinancialStock_Income]
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[FinancialStock_Expenses]') AND OBJECTPROPERTY(id, N'IsView') = 1)
DROP VIEW [dbo].[FinancialStock_Expenses]
GO
/*************************/
/****  QualityReport  ****/
/*************************/
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[QualityReport_GroupMembers]') AND OBJECTPROPERTY(id, N'IsView') = 1)
DROP VIEW [dbo].[QualityReport_GroupMembers]
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[QualityReport_GroupMembersOld]') AND OBJECTPROPERTY(id, N'IsView') = 1)
DROP VIEW [dbo].[QualityReport_GroupMembersOld]
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[QualityReport_ActivePersonsPhysical]') AND OBJECTPROPERTY(id, N'IsView') = 1)
DROP VIEW [dbo].[QualityReport_ActivePersonsPhysical]
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[QualityReport_PersonsPhysicalInformation]') AND OBJECTPROPERTY(id, N'IsView') = 1)
DROP VIEW [dbo].[QualityReport_PersonsPhysicalInformation]
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[QualityReport_shares]') AND OBJECTPROPERTY(id, N'IsView') = 1)
DROP VIEW [dbo].[QualityReport_shares]
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[QualityReport_PersonFinancial]') AND OBJECTPROPERTY(id, N'IsView') = 1)
DROP VIEW [dbo].[QualityReport_PersonFinancial]
GO
/********************************************/
/******************  END   ******************/
/********************************************/

/****** Objet :  View [dbo].[GLoanSizeMaturityGraceDomainDistrict]    Date de génération du script : 08/21/2007 13:34:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[GLoanSizeMaturityGraceDomainDistrict]') AND OBJECTPROPERTY(id, N'IsView') = 1)
EXEC dbo.sp_executesql @statement = N'CREATE VIEW [dbo].[GLoanSizeMaturityGraceDomainDistrict]
AS
SELECT     TOP 100 PERCENT (SUM(dbo.Installments.capital_repayment) - SUM(dbo.Installments.paid_capital)) 
                      * dbo.PersonGroupBelonging.loan_share_amount / dbo.Credit.amount AS OLB, dbo.Contracts.contract_code AS Contract, dbo.Contracts.start_date, 
                      dbo.Users.first_name AS loan_officer, dbo.Packages.name AS Product, dbo.Districts.name AS District, dbo.Credit.amount, dbo.Credit.grace_period, 
                      dbo.Credit.nb_of_installment * { fn ROUND(ISNULL(NULLIF (dbo.InstallmentTypes.nb_of_days, 0) / 30, dbo.InstallmentTypes.nb_of_months), 0) 
                      } AS maturity, dbo.Persons.sex, dbo.DomainOfApplications.name AS domainName, dbo.PersonGroupBelonging.loan_share_amount, 
                      dbo.Contracts.close_date, dbo.Credit.interest_rate, dbo.Tiers.active, dbo.Tiers.loan_cycle, dbo.Persons.first_name, dbo.Persons.last_name, 
                      dbo.Credit.bad_loan
FROM         dbo.Installments INNER JOIN
                      dbo.Contracts ON dbo.Installments.contract_id = dbo.Contracts.id INNER JOIN
                      dbo.Credit ON dbo.Contracts.id = dbo.Credit.id INNER JOIN
                      dbo.InstallmentTypes ON dbo.Credit.installment_type = dbo.InstallmentTypes.id INNER JOIN
                      dbo.Users ON dbo.Credit.loanofficer_id = dbo.Users.id INNER JOIN
                      dbo.Packages ON dbo.Packages.id = dbo.Credit.package_id INNER JOIN
                      dbo.Tiers ON dbo.Tiers.id = dbo.Contracts.beneficiary_id INNER JOIN
                      dbo.Districts ON dbo.Districts.id = dbo.Tiers.district_id INNER JOIN
                      dbo.PersonGroupBelonging ON dbo.PersonGroupBelonging.group_id = dbo.Tiers.id INNER JOIN
                      dbo.Persons ON dbo.Persons.id = dbo.PersonGroupBelonging.person_id INNER JOIN
                      dbo.DomainOfApplications ON dbo.Persons.activity_id = dbo.DomainOfApplications.id
WHERE     (dbo.Credit.written_off = 0) AND (dbo.PersonGroupBelonging.currently_in = 1) AND (dbo.Credit.disbursed = 1)
GROUP BY dbo.Installments.contract_id, dbo.Contracts.contract_code, dbo.Users.first_name, dbo.Packages.name, dbo.Districts.name, dbo.Credit.amount, 
                      dbo.Credit.grace_period, dbo.Credit.nb_of_installment, dbo.Persons.sex, dbo.DomainOfApplications.name, 
                      dbo.PersonGroupBelonging.loan_share_amount, dbo.PersonGroupBelonging.person_id, dbo.InstallmentTypes.nb_of_days, 
                      dbo.InstallmentTypes.nb_of_months, dbo.Contracts.start_date, dbo.Contracts.close_date, dbo.Credit.interest_rate, dbo.Tiers.active, 
                      dbo.Tiers.loan_cycle, dbo.Persons.first_name, dbo.Persons.last_name, dbo.Credit.bad_loan
HAVING      (SUM(dbo.Installments.capital_repayment) - SUM(dbo.Installments.paid_capital) > 0.02)
ORDER BY Contract
'
GO
/****** Objet :  View [dbo].[ILoanSizeMaturityGraceDomainDistrict]    Date de génération du script : 08/21/2007 13:34:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[ILoanSizeMaturityGraceDomainDistrict]') AND OBJECTPROPERTY(id, N'IsView') = 1)
EXEC dbo.sp_executesql @statement = N'CREATE VIEW [dbo].[ILoanSizeMaturityGraceDomainDistrict]
AS
SELECT     SUM(dbo.Installments.capital_repayment) - SUM(dbo.Installments.paid_capital) AS OLB, dbo.Contracts.contract_code AS Contract, 
                      dbo.Contracts.start_date, dbo.Users.first_name AS loan_officer, dbo.Packages.name AS Product, dbo.Districts.name AS District, dbo.Credit.amount, 
                      dbo.Credit.grace_period, dbo.Credit.nb_of_installment * { fn ROUND(ISNULL(NULLIF (dbo.InstallmentTypes.nb_of_days, 0) / 30, 
                      dbo.InstallmentTypes.nb_of_months), 0) } AS maturity, dbo.Persons.sex, dbo.DomainOfApplications.name AS domainName, 
                      dbo.Credit.amount AS loan_share_amount, dbo.Contracts.close_date, dbo.Credit.interest_rate, dbo.Tiers.active, dbo.Tiers.loan_cycle, 
                      dbo.Persons.last_name, dbo.Persons.first_name, dbo.Credit.bad_loan
FROM         dbo.Installments INNER JOIN
                      dbo.Contracts ON dbo.Installments.contract_id = dbo.Contracts.id INNER JOIN
                      dbo.Credit ON dbo.Contracts.id = dbo.Credit.id INNER JOIN
                      dbo.InstallmentTypes ON dbo.Credit.installment_type = dbo.InstallmentTypes.id INNER JOIN
                      dbo.Users ON dbo.Credit.loanofficer_id = dbo.Users.id INNER JOIN
                      dbo.Packages ON dbo.Packages.id = dbo.Credit.package_id INNER JOIN
                      dbo.Tiers ON dbo.Tiers.id = dbo.Contracts.beneficiary_id INNER JOIN
                      dbo.Districts ON dbo.Districts.id = dbo.Tiers.district_id INNER JOIN
                      dbo.Persons ON dbo.Tiers.id = dbo.Persons.id INNER JOIN
                      dbo.DomainOfApplications ON dbo.Persons.activity_id = dbo.DomainOfApplications.id
WHERE     (dbo.Credit.written_off = 0) AND (dbo.Credit.disbursed = 1)
GROUP BY dbo.Installments.contract_id, dbo.Contracts.contract_code, dbo.Users.first_name, dbo.Packages.name, dbo.Districts.name, dbo.Credit.amount, 
                      dbo.Credit.grace_period, dbo.Credit.nb_of_installment, dbo.Persons.sex, dbo.DomainOfApplications.name, dbo.InstallmentTypes.nb_of_days, 
                      dbo.InstallmentTypes.nb_of_months, dbo.Contracts.start_date, dbo.Contracts.close_date, dbo.Credit.interest_rate, dbo.Tiers.active, 
                      dbo.Tiers.loan_cycle, dbo.Persons.last_name, dbo.Persons.first_name, dbo.Credit.bad_loan
HAVING      (SUM(dbo.Installments.capital_repayment) - SUM(dbo.Installments.paid_capital) > 0.02)
'
GO

/****** Objet :  View [dbo].[ILoanSizeMaturityGraceDomainDistrict_FullyRepaid]    Date de génération du script : 08/21/2007 13:34:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[ILoanSizeMaturityGraceDomainDistrict_FullyRepaid]') AND OBJECTPROPERTY(id, N'IsView') = 1)
EXEC dbo.sp_executesql @statement = N'CREATE VIEW [dbo].[ILoanSizeMaturityGraceDomainDistrict_FullyRepaid]
AS
SELECT     SUM(dbo.Installments.capital_repayment) - SUM(dbo.Installments.paid_capital) AS OLB, dbo.Contracts.contract_code AS Contract, 
                      dbo.Users.first_name AS loan_officer, dbo.Packages.name AS Product, dbo.Districts.name AS District, dbo.Credit.amount, dbo.Credit.grace_period, 
                      dbo.Credit.nb_of_installment * { fn ROUND(ISNULL(NULLIF (dbo.InstallmentTypes.nb_of_days, 0) / 30, dbo.InstallmentTypes.nb_of_months), 0) } AS maturity, 
                      dbo.Persons.sex, dbo.DomainOfApplications.name AS domainName, dbo.Credit.amount AS loan_share_amount, dbo.Contracts.start_date, dbo.Contracts.close_date, 
                      dbo.Credit.interest_rate, dbo.Tiers.active
FROM         dbo.Installments INNER JOIN
                      dbo.Contracts ON dbo.Installments.contract_id = dbo.Contracts.id INNER JOIN
                      dbo.Credit ON dbo.Contracts.id = dbo.Credit.id INNER JOIN
                      dbo.InstallmentTypes ON dbo.Credit.installment_type = dbo.InstallmentTypes.id INNER JOIN
                      dbo.Users ON dbo.Credit.loanofficer_id = dbo.Users.id INNER JOIN
                      dbo.Packages ON dbo.Packages.id = dbo.Credit.package_id INNER JOIN
                      dbo.Tiers ON dbo.Tiers.id = dbo.Contracts.beneficiary_id INNER JOIN
                      dbo.Districts ON dbo.Districts.id = dbo.Tiers.district_id INNER JOIN
                      dbo.Persons ON dbo.Tiers.id = dbo.Persons.id INNER JOIN
                      dbo.DomainOfApplications ON dbo.Persons.activity_id = dbo.DomainOfApplications.id
WHERE     (dbo.Credit.written_off = 0)
GROUP BY dbo.Installments.contract_id, dbo.Contracts.contract_code, dbo.Users.first_name, dbo.Packages.name, dbo.Districts.name, dbo.Credit.amount, 
                      dbo.Credit.grace_period, dbo.Credit.nb_of_installment, dbo.Persons.sex, dbo.DomainOfApplications.name, dbo.InstallmentTypes.nb_of_days, 
                      dbo.InstallmentTypes.nb_of_months, dbo.Contracts.start_date, dbo.Contracts.close_date, dbo.Credit.interest_rate, dbo.Tiers.active
HAVING      (SUM(dbo.Installments.capital_repayment) - SUM(dbo.Installments.paid_capital) <= 0.02)
'
GO
/****** Objet :  View [dbo].[GLoanSizeMaturityGraceDomainDistrict_FullyRepaid]    Date de génération du script : 08/21/2007 13:34:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[GLoanSizeMaturityGraceDomainDistrict_FullyRepaid]') AND OBJECTPROPERTY(id, N'IsView') = 1)
EXEC dbo.sp_executesql @statement = N'CREATE VIEW [dbo].[GLoanSizeMaturityGraceDomainDistrict_FullyRepaid]
AS
SELECT     TOP 100 PERCENT (SUM(dbo.Installments.capital_repayment) - SUM(dbo.Installments.paid_capital)) 
                      * dbo.PersonGroupBelonging.loan_share_amount / dbo.Credit.amount AS OLB, dbo.Contracts.contract_code AS Contract, dbo.Users.first_name AS loan_officer, 
                      dbo.Packages.name AS Product, dbo.Districts.name AS District, dbo.Credit.amount, dbo.Credit.grace_period, 
                      dbo.Credit.nb_of_installment * { fn ROUND(ISNULL(NULLIF (dbo.InstallmentTypes.nb_of_days, 0) / 30, dbo.InstallmentTypes.nb_of_months), 0) } AS maturity, 
                      dbo.Persons.sex, dbo.DomainOfApplications.name AS domainName, dbo.PersonGroupBelonging.loan_share_amount, dbo.Contracts.start_date, 
                      dbo.Contracts.close_date, dbo.Credit.interest_rate, dbo.Tiers.active
FROM         dbo.Installments INNER JOIN
                      dbo.Contracts ON dbo.Installments.contract_id = dbo.Contracts.id INNER JOIN
                      dbo.Credit ON dbo.Contracts.id = dbo.Credit.id INNER JOIN
                      dbo.InstallmentTypes ON dbo.Credit.installment_type = dbo.InstallmentTypes.id INNER JOIN
                      dbo.Users ON dbo.Credit.loanofficer_id = dbo.Users.id INNER JOIN
                      dbo.Packages ON dbo.Packages.id = dbo.Credit.package_id INNER JOIN
                      dbo.Tiers ON dbo.Tiers.id = dbo.Contracts.beneficiary_id INNER JOIN
                      dbo.Districts ON dbo.Districts.id = dbo.Tiers.district_id INNER JOIN
                      dbo.PersonGroupBelonging ON dbo.PersonGroupBelonging.group_id = dbo.Tiers.id INNER JOIN
                      dbo.Persons ON dbo.Persons.id = dbo.PersonGroupBelonging.person_id INNER JOIN
                      dbo.DomainOfApplications ON dbo.Persons.activity_id = dbo.DomainOfApplications.id
WHERE     (dbo.Credit.written_off = 0) AND (dbo.PersonGroupBelonging.currently_in = 1)
GROUP BY dbo.Installments.contract_id, dbo.Contracts.contract_code, dbo.Users.first_name, dbo.Packages.name, dbo.Districts.name, dbo.Credit.amount, 
                      dbo.Credit.grace_period, dbo.Credit.nb_of_installment, dbo.Persons.sex, dbo.DomainOfApplications.name, dbo.PersonGroupBelonging.loan_share_amount, 
                      dbo.PersonGroupBelonging.person_id, dbo.InstallmentTypes.nb_of_days, dbo.InstallmentTypes.nb_of_months, dbo.Contracts.start_date, dbo.Contracts.close_date, 
                      dbo.Credit.interest_rate, dbo.Tiers.active
HAVING      (SUM(dbo.Installments.capital_repayment) - SUM(dbo.Installments.paid_capital) <= 0.02)
ORDER BY Contract
'
GO
/****** Objet :  View [dbo].[Conso_GLoanSizeMaturityGraceDomainDistrict]    Date de génération du script : 08/21/2007 13:34:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[Conso_GLoanSizeMaturityGraceDomainDistrict]') AND OBJECTPROPERTY(id, N'IsView') = 1)
EXEC dbo.sp_executesql @statement = N'CREATE VIEW [dbo].[Conso_GLoanSizeMaturityGraceDomainDistrict]
AS
SELECT     TOP 100 PERCENT dbo.Conso_CreditContracts.branch_code, dbo.Conso_CreditContracts.conso_number, 
                      (dbo.Conso_CreditContracts.capital_repayment - dbo.Conso_CreditContracts.paid_capital) 
                      * dbo.Conso_Customers.loan_share_amount / dbo.Conso_CreditContracts.amount AS OLB, dbo.Conso_CreditContracts.contract_code, 
                      dbo.Conso_CreditContracts.loan_officer_name AS loan_officer, dbo.Conso_CreditContracts.package_name AS product, dbo.Conso_Customers.district_name AS district, 
                      dbo.Conso_CreditContracts.amount, 0 AS grace_period, 0 AS maturity, dbo.Conso_Customers.is_male, dbo.Conso_Customers.doa_name AS domain_name, 
                      dbo.Conso_Customers.loan_share_amount, dbo.Conso_CreditContracts.year, dbo.Conso_CreditContracts.period, dbo.Conso_Customers.first_name, 
                      dbo.Conso_Customers.last_name, dbo.Conso_Customers.father_name, dbo.Conso_Customers.active
FROM         dbo.Conso_CreditContracts INNER JOIN
                      dbo.Conso_Customers ON dbo.Conso_CreditContracts.branch_code = dbo.Conso_Customers.branch_code AND 
                      dbo.Conso_CreditContracts.conso_number = dbo.Conso_Customers.conso_number AND 
                      dbo.Conso_CreditContracts.contract_code = dbo.Conso_Customers.contract_code
WHERE     (dbo.Conso_CreditContracts.capital_repayment - dbo.Conso_CreditContracts.paid_capital > 0.02) AND (dbo.Conso_Customers.is_in_group = 1) AND 
                      (dbo.Conso_CreditContracts.written_off = 0) AND (dbo.Conso_CreditContracts.disbursed = 1)
ORDER BY dbo.Conso_Customers.branch_code, dbo.Conso_CreditContracts.year, dbo.Conso_Customers.conso_number, dbo.Conso_CreditContracts.contract_code'
GO
/****** Objet :  View [dbo].[Conso_GLoansPAR]    Date de génération du script : 08/21/2007 13:34:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[Conso_GLoansPAR]') AND OBJECTPROPERTY(id, N'IsView') = 1)
EXEC dbo.sp_executesql @statement = N'CREATE VIEW [dbo].[Conso_GLoansPAR]
AS
SELECT     dbo.Conso_CreditContracts.branch_code, dbo.Conso_CreditContracts.conso_number, dbo.Conso_CreditContracts.loan_officer_name AS loan_officer, 
                      dbo.Conso_CreditContracts.package_name AS product, dbo.Conso_CreditContracts.contract_code, dbo.Conso_Customers.district_name AS district, 
                      dbo.Conso_Customers.doa_name AS domain_name, ISNULL((CASE WHEN DATEDIFF(day, Conso_CreditContracts.first_non_repaid_date, 
                      Conso_Details.application_date) < 0 THEN 0 ELSE DATEDIFF(day, Conso_CreditContracts.first_non_repaid_date, Conso_Details.application_date) END), 0) 
                      AS days_late, ISNULL((dbo.Conso_CreditContracts.capital_repayment - dbo.Conso_CreditContracts.paid_capital) 
                      * dbo.Conso_Customers.loan_share_amount / dbo.Conso_CreditContracts.amount, 0) AS OLB, ISNULL
                          ((SELECT     capital_repayment - paid_capital AS Expr1
                              FROM         dbo.Conso_CreditContracts AS Temp
                              WHERE     (id = dbo.Conso_CreditContracts.id) AND (DATEDIFF(day, dbo.Conso_CreditContracts.first_non_repaid_date, dbo.Conso_Details.application_date) >= 1) 
                                                    AND (DATEDIFF(day, dbo.Conso_CreditContracts.first_non_repaid_date, dbo.Conso_Details.application_date) <= 30)), 0) 
                      * dbo.Conso_Customers.loan_share_amount / dbo.Conso_CreditContracts.amount AS PAR1_30, ISNULL
                          ((SELECT     capital_repayment - paid_capital AS Expr1
                              FROM         dbo.Conso_CreditContracts AS Temp
                              WHERE     (id = dbo.Conso_CreditContracts.id) AND (DATEDIFF(day, dbo.Conso_CreditContracts.first_non_repaid_date, dbo.Conso_Details.application_date) >= 31) 
                                                    AND (DATEDIFF(day, dbo.Conso_CreditContracts.first_non_repaid_date, dbo.Conso_Details.application_date) <= 60)), 0) 
                      * dbo.Conso_Customers.loan_share_amount / dbo.Conso_CreditContracts.amount AS PAR31_60, ISNULL
                          ((SELECT     capital_repayment - paid_capital AS Expr1
                              FROM         dbo.Conso_CreditContracts AS Temp
                              WHERE     (id = dbo.Conso_CreditContracts.id) AND (DATEDIFF(day, dbo.Conso_CreditContracts.first_non_repaid_date, dbo.Conso_Details.application_date) >= 61) 
                                                    AND (DATEDIFF(day, dbo.Conso_CreditContracts.first_non_repaid_date, dbo.Conso_Details.application_date) <= 90)), 0) 
                      * dbo.Conso_Customers.loan_share_amount / dbo.Conso_CreditContracts.amount AS PAR61_90, ISNULL
                          ((SELECT     capital_repayment - paid_capital AS Expr1
                              FROM         dbo.Conso_CreditContracts AS Temp
                              WHERE     (id = dbo.Conso_CreditContracts.id) AND (DATEDIFF(day, dbo.Conso_CreditContracts.first_non_repaid_date, dbo.Conso_Details.application_date) >= 91) 
                                                    AND (DATEDIFF(day, dbo.Conso_CreditContracts.first_non_repaid_date, dbo.Conso_Details.application_date) <= 180)), 0) 
                      * dbo.Conso_Customers.loan_share_amount / dbo.Conso_CreditContracts.amount AS PAR91_180, ISNULL
                          ((SELECT     capital_repayment - paid_capital AS Expr1
                              FROM         dbo.Conso_CreditContracts AS Temp
                              WHERE     (id = dbo.Conso_CreditContracts.id) AND (DATEDIFF(day, dbo.Conso_CreditContracts.first_non_repaid_date, dbo.Conso_Details.application_date) >= 181) 
                                                    AND (DATEDIFF(day, dbo.Conso_CreditContracts.first_non_repaid_date, dbo.Conso_Details.application_date) <= 365)), 0) 
                      * dbo.Conso_Customers.loan_share_amount / dbo.Conso_CreditContracts.amount AS PAR181_365, ISNULL
                          ((SELECT     capital_repayment - paid_capital AS Expr1
                              FROM         dbo.Conso_CreditContracts AS Temp
                              WHERE     (id = dbo.Conso_CreditContracts.id) AND (DATEDIFF(day, dbo.Conso_CreditContracts.first_non_repaid_date, dbo.Conso_Details.application_date) 
                                                    >= 366)), 0) * dbo.Conso_Customers.loan_share_amount / dbo.Conso_CreditContracts.amount AS PAR365
                                                    , Conso_CreditContracts.year,Conso_CreditContracts.period
FROM         dbo.Conso_CreditContracts INNER JOIN
                      dbo.Conso_Customers ON dbo.Conso_CreditContracts.branch_code = dbo.Conso_Customers.branch_code AND 
                      dbo.Conso_CreditContracts.conso_number = dbo.Conso_Customers.conso_number AND 
                      dbo.Conso_CreditContracts.contract_code = dbo.Conso_Customers.contract_code INNER JOIN
                      dbo.Conso_Details ON dbo.Conso_CreditContracts.branch_code = dbo.Conso_Details.branch_code AND 
                      dbo.Conso_CreditContracts.conso_number = dbo.Conso_Details.conso_number
WHERE     (dbo.Conso_CreditContracts.capital_repayment - dbo.Conso_CreditContracts.paid_capital > 0.02) AND (dbo.Conso_Customers.is_in_group = 1) AND 
                      (dbo.Conso_CreditContracts.written_off = 0) AND (dbo.Conso_CreditContracts.disbursed = 1)
'
GO
/****** Objet :  View [dbo].[Conso_ILoansPAR]    Date de génération du script : 08/21/2007 13:34:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[Conso_ILoansPAR]') AND OBJECTPROPERTY(id, N'IsView') = 1)
EXEC dbo.sp_executesql @statement = N'CREATE VIEW [dbo].[Conso_ILoansPAR]
AS
SELECT     dbo.Conso_CreditContracts.branch_code, dbo.Conso_CreditContracts.conso_number, dbo.Conso_CreditContracts.loan_officer_name AS loan_officer, 
                      dbo.Conso_CreditContracts.package_name AS product, dbo.Conso_CreditContracts.contract_code, dbo.Conso_Customers.district_name AS district, 
                      dbo.Conso_Customers.doa_name AS domain_name, ISNULL((CASE WHEN DATEDIFF(day, Conso_CreditContracts.first_non_repaid_date, 
                      Conso_Details.application_date) < 0 THEN 0 ELSE DATEDIFF(day, Conso_CreditContracts.first_non_repaid_date, Conso_Details.application_date) END), 0) 
                      AS days_late, ISNULL(dbo.Conso_CreditContracts.capital_repayment - dbo.Conso_CreditContracts.paid_capital, 0) AS OLB, ISNULL
                          ((SELECT     capital_repayment - paid_capital AS Expr1
                              FROM         dbo.Conso_CreditContracts AS Temp
                              WHERE     (id = dbo.Conso_CreditContracts.id) AND (DATEDIFF(day, dbo.Conso_CreditContracts.first_non_repaid_date, dbo.Conso_Details.application_date) >= 1) 
                                                    AND (DATEDIFF(day, dbo.Conso_CreditContracts.first_non_repaid_date, dbo.Conso_Details.application_date) <= 30)), 0) AS PAR1_30, ISNULL
                          ((SELECT     capital_repayment - paid_capital AS Expr1
                              FROM         dbo.Conso_CreditContracts AS Temp
                              WHERE     (id = dbo.Conso_CreditContracts.id) AND (DATEDIFF(day, dbo.Conso_CreditContracts.first_non_repaid_date, dbo.Conso_Details.application_date) >= 31) 
                                                    AND (DATEDIFF(day, dbo.Conso_CreditContracts.first_non_repaid_date, dbo.Conso_Details.application_date) <= 60)), 0) AS PAR31_60, ISNULL
                          ((SELECT     capital_repayment - paid_capital AS Expr1
                              FROM         dbo.Conso_CreditContracts AS Temp
                              WHERE     (id = dbo.Conso_CreditContracts.id) AND (DATEDIFF(day, dbo.Conso_CreditContracts.first_non_repaid_date, dbo.Conso_Details.application_date) >= 61) 
                                                    AND (DATEDIFF(day, dbo.Conso_CreditContracts.first_non_repaid_date, dbo.Conso_Details.application_date) <= 90)), 0) AS PAR61_90, ISNULL
                          ((SELECT     capital_repayment - paid_capital AS Expr1
                              FROM         dbo.Conso_CreditContracts AS Temp
                              WHERE     (id = dbo.Conso_CreditContracts.id) AND (DATEDIFF(day, dbo.Conso_CreditContracts.first_non_repaid_date, dbo.Conso_Details.application_date) >= 91) 
                                                    AND (DATEDIFF(day, dbo.Conso_CreditContracts.first_non_repaid_date, dbo.Conso_Details.application_date) <= 180)), 0) AS PAR91_180, 
                      ISNULL
                          ((SELECT     capital_repayment - paid_capital AS Expr1
                              FROM         dbo.Conso_CreditContracts AS Temp
                              WHERE     (id = dbo.Conso_CreditContracts.id) AND (DATEDIFF(day, dbo.Conso_CreditContracts.first_non_repaid_date, dbo.Conso_Details.application_date) >= 181) 
                                                    AND (DATEDIFF(day, dbo.Conso_CreditContracts.first_non_repaid_date, dbo.Conso_Details.application_date) <= 365)), 0) AS PAR181_365, 
                      ISNULL
                          ((SELECT     capital_repayment - paid_capital AS Expr1
                              FROM         dbo.Conso_CreditContracts AS Temp
                              WHERE     (id = dbo.Conso_CreditContracts.id) AND (DATEDIFF(day, dbo.Conso_CreditContracts.first_non_repaid_date, dbo.Conso_Details.application_date) 
                                                    >= 366)), 0) AS PAR365
                                                    ,Conso_CreditContracts.year,Conso_CreditContracts.period
FROM         dbo.Conso_CreditContracts INNER JOIN
                      dbo.Conso_Customers ON dbo.Conso_CreditContracts.branch_code = dbo.Conso_Customers.branch_code AND 
                      dbo.Conso_CreditContracts.conso_number = dbo.Conso_Customers.conso_number AND 
                      dbo.Conso_CreditContracts.contract_code = dbo.Conso_Customers.contract_code INNER JOIN
                      dbo.Conso_Details ON dbo.Conso_CreditContracts.branch_code = dbo.Conso_Details.branch_code AND 
                      dbo.Conso_CreditContracts.conso_number = dbo.Conso_Details.conso_number
WHERE     (dbo.Conso_CreditContracts.capital_repayment - dbo.Conso_CreditContracts.paid_capital > 0.02) AND (dbo.Conso_Customers.is_in_group = 0) AND 
                      (dbo.Conso_CreditContracts.written_off = 0) AND (dbo.Conso_CreditContracts.disbursed = 1)
'
GO
/****** Objet :  View [dbo].[Conso_ILoanSizeMaturityGraceDomainDistrict]    Date de génération du script : 08/21/2007 13:34:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[Conso_ILoanSizeMaturityGraceDomainDistrict]') AND OBJECTPROPERTY(id, N'IsView') = 1)
EXEC dbo.sp_executesql @statement = N'CREATE VIEW [dbo].[Conso_ILoanSizeMaturityGraceDomainDistrict]
AS
SELECT     TOP 100 PERCENT dbo.Conso_Customers.branch_code, dbo.Conso_Customers.conso_number, 
                      dbo.Conso_CreditContracts.capital_repayment - dbo.Conso_CreditContracts.paid_capital AS OLB, dbo.Conso_Customers.contract_code, 
                      dbo.Conso_CreditContracts.loan_officer_name AS loan_officer, dbo.Conso_CreditContracts.package_name AS product, dbo.Conso_Customers.district_name AS district, 
                      dbo.Conso_CreditContracts.amount, 0 AS grace_period, 0 AS nb_of_installments, dbo.Conso_Customers.is_male, dbo.Conso_Customers.doa_name AS domain_name, 
                      dbo.Conso_Customers.loan_share_amount, dbo.Conso_CreditContracts.year, dbo.Conso_CreditContracts.period, dbo.Conso_Customers.first_name, 
                      dbo.Conso_Customers.last_name, dbo.Conso_Customers.father_name, dbo.Conso_Customers.active
FROM         dbo.Conso_Customers INNER JOIN
                      dbo.Conso_CreditContracts ON dbo.Conso_Customers.contract_code = dbo.Conso_CreditContracts.contract_code AND 
                      dbo.Conso_Customers.branch_code = dbo.Conso_CreditContracts.branch_code AND 
                      dbo.Conso_Customers.conso_number = dbo.Conso_CreditContracts.conso_number
WHERE     (dbo.Conso_Customers.is_in_group = 0) AND (dbo.Conso_CreditContracts.written_off = 0) AND (dbo.Conso_CreditContracts.disbursed = 1) AND 
                      (dbo.Conso_CreditContracts.capital_repayment - dbo.Conso_CreditContracts.paid_capital > 0.02)
ORDER BY dbo.Conso_Customers.branch_code, dbo.Conso_CreditContracts.year, dbo.Conso_Customers.conso_number
'
GO
/****** Objet :  View [dbo].[PAREvolution]    Date de génération du script : 08/21/2007 13:34:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[PAREvolution]') AND OBJECTPROPERTY(id, N'IsView') = 1)
EXEC dbo.sp_executesql @statement = N'
CREATE VIEW [dbo].[PAREvolution]
AS
SELECT     branch_code, conso_number,
                          (SELECT     COUNT(dbo.Conso_CreditContracts.contract_code)
                            FROM          dbo.Conso_CreditContracts
                            WHERE      Conso_CreditContracts.branch_code = CreditContract.branch_code AND
                                                   Conso_CreditContracts.conso_number = CreditContract.conso_number) AS nber_of_contracts,
                          (SELECT     COUNT(dbo.Conso_CreditContracts.contract_code)
                            FROM          dbo.Conso_CreditContracts INNER JOIN
                                                   dbo.Conso_Details ON dbo.Conso_CreditContracts.branch_code = dbo.Conso_Details.branch_code AND
                                                   dbo.Conso_CreditContracts.conso_number = dbo.Conso_Details.conso_number
                            WHERE      
							(DATEDIFF(day, dbo.Conso_CreditContracts.first_non_repaid_date, dbo.Conso_Details.application_date) >= 1) AND                            
                            (DATEDIFF(day, dbo.Conso_CreditContracts.first_non_repaid_date, dbo.Conso_Details.application_date) <= 30) AND
                                                   Conso_CreditContracts.branch_code = CreditContract.branch_code AND
                                                   Conso_CreditContracts.conso_number = CreditContract.conso_number) AS PAR1_30,
                          (SELECT     COUNT(dbo.Conso_CreditContracts.contract_code)
                            FROM          dbo.Conso_CreditContracts INNER JOIN
                                                   dbo.Conso_Details ON dbo.Conso_CreditContracts.branch_code = dbo.Conso_Details.branch_code AND
                                                   dbo.Conso_CreditContracts.conso_number = dbo.Conso_Details.conso_number
                            WHERE      (DATEDIFF(day, dbo.Conso_CreditContracts.first_non_repaid_date, dbo.Conso_Details.application_date) > 30) AND (DATEDIFF(day,
                                                   dbo.Conso_CreditContracts.first_non_repaid_date, dbo.Conso_Details.application_date) <= 60) AND
                                                   Conso_CreditContracts.branch_code = CreditContract.branch_code AND
                                                   Conso_CreditContracts.conso_number = CreditContract.conso_number) AS PAR31_60,
                          (SELECT     COUNT(dbo.Conso_CreditContracts.contract_code)
                            FROM          dbo.Conso_CreditContracts INNER JOIN
                                                   dbo.Conso_Details ON dbo.Conso_CreditContracts.branch_code = dbo.Conso_Details.branch_code AND
                                                   dbo.Conso_CreditContracts.conso_number = dbo.Conso_Details.conso_number
                            WHERE      (DATEDIFF(day, dbo.Conso_CreditContracts.first_non_repaid_date, dbo.Conso_Details.application_date) > 60) AND (DATEDIFF(day,
                                                   dbo.Conso_CreditContracts.first_non_repaid_date, dbo.Conso_Details.application_date) <= 90) AND
                                                   Conso_CreditContracts.branch_code = CreditContract.branch_code AND
                                                   Conso_CreditContracts.conso_number = CreditContract.conso_number) AS PAR61_90,
                          (SELECT     COUNT(dbo.Conso_CreditContracts.contract_code)
                            FROM          dbo.Conso_CreditContracts INNER JOIN
                                                   dbo.Conso_Details ON dbo.Conso_CreditContracts.branch_code = dbo.Conso_Details.branch_code AND
                                                   dbo.Conso_CreditContracts.conso_number = dbo.Conso_Details.conso_number
                            WHERE      (DATEDIFF(day, dbo.Conso_CreditContracts.first_non_repaid_date, dbo.Conso_Details.application_date) > 90) AND (DATEDIFF(day,
                                                   dbo.Conso_CreditContracts.first_non_repaid_date, dbo.Conso_Details.application_date) <= 180) AND
                                                   Conso_CreditContracts.branch_code = CreditContract.branch_code AND
                                                   Conso_CreditContracts.conso_number = CreditContract.conso_number) AS PAR91_180,
                          (SELECT     COUNT(dbo.Conso_CreditContracts.contract_code)
                            FROM          dbo.Conso_CreditContracts INNER JOIN
                                                   dbo.Conso_Details ON dbo.Conso_CreditContracts.branch_code = dbo.Conso_Details.branch_code AND
                                                   dbo.Conso_CreditContracts.conso_number = dbo.Conso_Details.conso_number
                            WHERE      (DATEDIFF(day, dbo.Conso_CreditContracts.first_non_repaid_date, dbo.Conso_Details.application_date) > 180) AND (DATEDIFF(day,
                                                   dbo.Conso_CreditContracts.first_non_repaid_date, dbo.Conso_Details.application_date) <= 365) AND
                                                   Conso_CreditContracts.branch_code = CreditContract.branch_code AND
                                                   Conso_CreditContracts.conso_number = CreditContract.conso_number) AS PAR181_365,
                          (SELECT     COUNT(dbo.Conso_CreditContracts.contract_code)
                            FROM          dbo.Conso_CreditContracts INNER JOIN
                                                   dbo.Conso_Details ON dbo.Conso_CreditContracts.branch_code = dbo.Conso_Details.branch_code AND
                                                   dbo.Conso_CreditContracts.conso_number = dbo.Conso_Details.conso_number
                            WHERE      (DATEDIFF(day, dbo.Conso_CreditContracts.first_non_repaid_date, dbo.Conso_Details.application_date) > 365) AND
                                                   Conso_CreditContracts.branch_code = CreditContract.branch_code AND
                                                   Conso_CreditContracts.conso_number = CreditContract.conso_number) AS PAR365
FROM         dbo.Conso_CreditContracts CreditContract
GROUP BY branch_code, conso_number
'
GO
/****** Objet :  View [dbo].[PortfolioAndPAREvolutionView]    Date de génération du script : 08/21/2007 13:34:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[PortfolioAndPAREvolutionView]') AND OBJECTPROPERTY(id, N'IsView') = 1)
EXEC dbo.sp_executesql @statement = N'CREATE VIEW [dbo].[PortfolioAndPAREvolutionView]
AS
SELECT     TOP 100 PERCENT branch_code, conso_number,year,period,
                          (SELECT     SUM(capital_repayment - paid_capital)
                            FROM          dbo.Conso_CreditContracts
                            WHERE      Conso_CreditContracts.branch_code = CreditContract.branch_code AND 
                                                   Conso_CreditContracts.conso_number = CreditContract.conso_number
                            GROUP BY branch_code, conso_number) AS OLB,
                          (SELECT     COUNT(dbo.Conso_CreditContracts.contract_code)
                            FROM          dbo.Conso_CreditContracts
                            WHERE      Conso_CreditContracts.branch_code = CreditContract.branch_code AND 
                                                   Conso_CreditContracts.conso_number = CreditContract.conso_number) AS nber_of_contracts,
                          (SELECT     COUNT(dbo.Conso_CreditContracts.contract_code)
                            FROM          dbo.Conso_CreditContracts INNER JOIN
                                                   dbo.Conso_Details ON dbo.Conso_CreditContracts.branch_code = dbo.Conso_Details.branch_code AND 
                                                   dbo.Conso_CreditContracts.conso_number = dbo.Conso_Details.conso_number
                            WHERE      
							(DATEDIFF(day, dbo.Conso_CreditContracts.first_non_repaid_date, dbo.Conso_Details.application_date) >= 1) AND                            
                            (DATEDIFF(day, dbo.Conso_CreditContracts.first_non_repaid_date, dbo.Conso_Details.application_date) <= 30) AND 
                                                   Conso_CreditContracts.branch_code = CreditContract.branch_code AND 
                                                   Conso_CreditContracts.conso_number = CreditContract.conso_number) AS PAR1_30,
                          (SELECT     COUNT(dbo.Conso_CreditContracts.contract_code)
                            FROM          dbo.Conso_CreditContracts INNER JOIN
                                                   dbo.Conso_Details ON dbo.Conso_CreditContracts.branch_code = dbo.Conso_Details.branch_code AND 
                                                   dbo.Conso_CreditContracts.conso_number = dbo.Conso_Details.conso_number
                            WHERE      (DATEDIFF(day, dbo.Conso_CreditContracts.first_non_repaid_date, dbo.Conso_Details.application_date) > 30) AND (DATEDIFF(day, 
                                                   dbo.Conso_CreditContracts.first_non_repaid_date, dbo.Conso_Details.application_date) <= 60) AND 
                                                   Conso_CreditContracts.branch_code = CreditContract.branch_code AND 
                                                   Conso_CreditContracts.conso_number = CreditContract.conso_number) AS PAR31_60,
                          (SELECT     COUNT(dbo.Conso_CreditContracts.contract_code)
                            FROM          dbo.Conso_CreditContracts INNER JOIN
                                                   dbo.Conso_Details ON dbo.Conso_CreditContracts.branch_code = dbo.Conso_Details.branch_code AND 
                                                   dbo.Conso_CreditContracts.conso_number = dbo.Conso_Details.conso_number
                            WHERE      (DATEDIFF(day, dbo.Conso_CreditContracts.first_non_repaid_date, dbo.Conso_Details.application_date) > 60) AND (DATEDIFF(day, 
                                                   dbo.Conso_CreditContracts.first_non_repaid_date, dbo.Conso_Details.application_date) <= 90) AND 
                                                   Conso_CreditContracts.branch_code = CreditContract.branch_code AND 
                                                   Conso_CreditContracts.conso_number = CreditContract.conso_number) AS PAR61_90,
                          (SELECT     COUNT(dbo.Conso_CreditContracts.contract_code)
                            FROM          dbo.Conso_CreditContracts INNER JOIN
                                                   dbo.Conso_Details ON dbo.Conso_CreditContracts.branch_code = dbo.Conso_Details.branch_code AND 
                                                   dbo.Conso_CreditContracts.conso_number = dbo.Conso_Details.conso_number
                            WHERE      (DATEDIFF(day, dbo.Conso_CreditContracts.first_non_repaid_date, dbo.Conso_Details.application_date) > 90) AND (DATEDIFF(day, 
                                                   dbo.Conso_CreditContracts.first_non_repaid_date, dbo.Conso_Details.application_date) <= 180) AND 
                                                   Conso_CreditContracts.branch_code = CreditContract.branch_code AND 
                                                   Conso_CreditContracts.conso_number = CreditContract.conso_number) AS PAR91_180,
                          (SELECT     COUNT(dbo.Conso_CreditContracts.contract_code)
                            FROM          dbo.Conso_CreditContracts INNER JOIN
                                                   dbo.Conso_Details ON dbo.Conso_CreditContracts.branch_code = dbo.Conso_Details.branch_code AND 
                                                   dbo.Conso_CreditContracts.conso_number = dbo.Conso_Details.conso_number
                            WHERE      (DATEDIFF(day, dbo.Conso_CreditContracts.first_non_repaid_date, dbo.Conso_Details.application_date) > 180) AND (DATEDIFF(day, 
                                                   dbo.Conso_CreditContracts.first_non_repaid_date, dbo.Conso_Details.application_date) <= 365) AND 
                                                   Conso_CreditContracts.branch_code = CreditContract.branch_code AND 
                                                   Conso_CreditContracts.conso_number = CreditContract.conso_number) AS PAR181_365,
                          (SELECT     COUNT(dbo.Conso_CreditContracts.contract_code)
                            FROM          dbo.Conso_CreditContracts INNER JOIN
                                                   dbo.Conso_Details ON dbo.Conso_CreditContracts.branch_code = dbo.Conso_Details.branch_code AND 
                                                   dbo.Conso_CreditContracts.conso_number = dbo.Conso_Details.conso_number
                            WHERE      (DATEDIFF(day, dbo.Conso_CreditContracts.first_non_repaid_date, dbo.Conso_Details.application_date) > 365) AND 
                                                   Conso_CreditContracts.branch_code = CreditContract.branch_code AND 
                                                   Conso_CreditContracts.conso_number = CreditContract.conso_number) AS PAR365
FROM         dbo.Conso_CreditContracts CreditContract
GROUP BY branch_code, conso_number,year,period
ORDER BY year,conso_number DESC
'
GO

/****** Objet :  View [dbo].[ReportInstallmentsView]    Date de génération du script : 08/21/2007 13:34:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[ReportInstallmentsView]') AND OBJECTPROPERTY(id, N'IsView') = 1)
EXEC dbo.sp_executesql @statement = N'CREATE VIEW [dbo].[ReportInstallmentsView]
AS
SELECT     contract_id, number, expected_date, interest_repayment, capital_repayment, paid_interest, paid_capital, 
                      capital_repayment - paid_capital + interest_repayment - paid_interest AS due_amount, 
                      CASE WHEN capital_repayment - paid_capital + interest_repayment - paid_interest < 0.02 THEN 0 ELSE DATEDIFF(day, expected_date, GETDATE()) 
                      END AS day_late
FROM         dbo.Installments
'
GO
/****** Objet :  View [dbo].[PortFolioEvolutionView]    Date de génération du script : 08/21/2007 13:34:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[PortFolioEvolutionView]') AND OBJECTPROPERTY(id, N'IsView') = 1)
EXEC dbo.sp_executesql @statement = N'
CREATE VIEW [dbo].[PortFolioEvolutionView]
AS
SELECT     branch_code, conso_number, SUM(capital_repayment - paid_capital) AS OLB
FROM         dbo.Conso_CreditContracts
GROUP BY branch_code, conso_number
'
GO
/****** Objet :  View [dbo].[ReportPARView]    Date de génération du script : 08/21/2007 13:34:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[ReportPARView]') AND OBJECTPROPERTY(id, N'IsView') = 1)
EXEC dbo.sp_executesql @statement = N'CREATE VIEW [dbo].[ReportPARView]
AS
SELECT     TOP 100 PERCENT dbo.Users.id, dbo.Users.first_name, dbo.Packages.name, ISNULL(dbo.Groups.name, 
                      dbo.Persons.first_name + '' '' + dbo.Persons.last_name) AS ClientName, dbo.Contracts.contract_code, dbo.Contracts.branch_code, 
                      Credit_1.funding_line_code, ISNULL
                          ((SELECT     SUM(capital_repayment) - SUM(paid_capital) AS Expr1
                              FROM         dbo.Installments
                              WHERE     (contract_id = Credit_1.id)), 0) AS OLB,
                          (SELECT     SUM(Installments_1.capital_repayment - Installments_1.paid_capital) AS Expr1
                            FROM          dbo.Credit INNER JOIN
                                                   dbo.Installments AS Installments_1 ON dbo.Credit.id = Installments_1.contract_id
                            WHERE      (dbo.Credit.disbursed = 1) AND (dbo.Credit.written_off = 0)) AS TotalOLB, ISNULL
                          ((SELECT     SUM(capital_repayment) - SUM(paid_capital) AS Expr1
                              FROM         dbo.ReportInstallmentsView
                              WHERE     (contract_id = Credit_1.id)
                              GROUP BY contract_id
                              HAVING      (MAX(day_late) > 0) AND (MAX(day_late) <= 30)), 0) AS CreditPAR1_30, ISNULL
                          ((SELECT     SUM(capital_repayment) - SUM(paid_capital) AS Expr1
                              FROM         dbo.ReportInstallmentsView AS ReportInstallmentsView_11
                              WHERE     (contract_id = Credit_1.id)
                              GROUP BY contract_id
                              HAVING      (MAX(day_late) > 30) AND (MAX(day_late) <= 60)), 0) AS CreditPAR31_60, ISNULL
                          ((SELECT     SUM(capital_repayment) - SUM(paid_capital) AS Expr1
                              FROM         dbo.ReportInstallmentsView AS ReportInstallmentsView_10
                              WHERE     (contract_id = Credit_1.id)
                              GROUP BY contract_id
                              HAVING      (MAX(day_late) > 60) AND (MAX(day_late) <= 90)), 0) AS CreditPAR61_90, ISNULL
                          ((SELECT     SUM(capital_repayment) - SUM(paid_capital) AS Expr1
                              FROM         dbo.ReportInstallmentsView AS ReportInstallmentsView_9
                              WHERE     (contract_id = Credit_1.id)
                              GROUP BY contract_id
                              HAVING      (MAX(day_late) > 90) AND (MAX(day_late) <= 180)), 0) AS CreditPAR91_180, ISNULL
                          ((SELECT     SUM(capital_repayment) - SUM(paid_capital) AS Expr1
                              FROM         dbo.ReportInstallmentsView AS ReportInstallmentsView_8
                              WHERE     (contract_id = Credit_1.id)
                              GROUP BY contract_id
                              HAVING      (MAX(day_late) > 180) AND (MAX(day_late) <= 365)), 0) AS CreditPAR181_365, ISNULL
                          ((SELECT     SUM(capital_repayment) - SUM(paid_capital) AS Expr1
                              FROM         dbo.ReportInstallmentsView AS ReportInstallmentsView_7
                              WHERE     (contract_id = Credit_1.id)
                              GROUP BY contract_id
                              HAVING      (MAX(day_late) > 365)), 0) AS CreditPAR365, Credit_1.amount
FROM         dbo.Contracts INNER JOIN
                      dbo.Credit AS Credit_1 ON dbo.Contracts.id = Credit_1.id INNER JOIN
                      dbo.Packages ON Credit_1.package_id = dbo.Packages.id INNER JOIN
                      dbo.Tiers ON dbo.Contracts.beneficiary_id = dbo.Tiers.id INNER JOIN
                      dbo.Users ON Credit_1.loanofficer_id = dbo.Users.id LEFT OUTER JOIN
                      dbo.Persons ON dbo.Tiers.id = dbo.Persons.id LEFT OUTER JOIN
                      dbo.Groups ON dbo.Tiers.id = dbo.Groups.id
WHERE     (ISNULL
                          ((SELECT     SUM(capital_repayment) - SUM(paid_capital) AS Expr1
                              FROM         dbo.ReportInstallmentsView AS ReportInstallmentsView_6
                              WHERE     (contract_id = Credit_1.id)
                              GROUP BY contract_id
                              HAVING      (MAX(day_late) > 0) AND (MAX(day_late) <= 30)), 0) <> 0) OR
                      (ISNULL
                          ((SELECT     SUM(capital_repayment) - SUM(paid_capital) AS Expr1
                              FROM         dbo.ReportInstallmentsView AS ReportInstallmentsView_5
                              WHERE     (contract_id = Credit_1.id)
                              GROUP BY contract_id
                              HAVING      (MAX(day_late) > 30) AND (MAX(day_late) <= 60)), 0) <> 0) OR
                      (ISNULL
                          ((SELECT     SUM(capital_repayment) - SUM(paid_capital) AS Expr1
                              FROM         dbo.ReportInstallmentsView AS ReportInstallmentsView_4
                              WHERE     (contract_id = Credit_1.id)
                              GROUP BY contract_id
                              HAVING      (MAX(day_late) > 60) AND (MAX(day_late) <= 90)), 0) <> 0) OR
                      (ISNULL
                          ((SELECT     SUM(capital_repayment) - SUM(paid_capital) AS Expr1
                              FROM         dbo.ReportInstallmentsView AS ReportInstallmentsView_3
                              WHERE     (contract_id = Credit_1.id)
                              GROUP BY contract_id
                              HAVING      (MAX(day_late) > 90) AND (MAX(day_late) <= 180)), 0) <> 0) OR
                      (ISNULL
                          ((SELECT     SUM(capital_repayment) - SUM(paid_capital) AS Expr1
                              FROM         dbo.ReportInstallmentsView AS ReportInstallmentsView_2
                              WHERE     (contract_id = Credit_1.id)
                              GROUP BY contract_id
                              HAVING      (MAX(day_late) > 180) AND (MAX(day_late) <= 365)), 0) <> 0) OR
                      (ISNULL
                          ((SELECT     SUM(capital_repayment) - SUM(paid_capital) AS Expr1
                              FROM         dbo.ReportInstallmentsView AS ReportInstallmentsView_1
                              WHERE     (contract_id = Credit_1.id)
                              GROUP BY contract_id
                              HAVING      (MAX(day_late) > 365)), 0) <> 0)
ORDER BY dbo.Contracts.branch_code
'
GO
/****** Objet :  View [dbo].[LoanSizeMaturityGraceDomainDistrict]    Date de génération du script : 08/21/2007 13:34:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[LoanSizeMaturityGraceDomainDistrict]') AND OBJECTPROPERTY(id, N'IsView') = 1)
EXEC dbo.sp_executesql @statement = N'CREATE VIEW [dbo].[LoanSizeMaturityGraceDomainDistrict]
AS
SELECT     OLB, Contract, start_date, loan_officer, Product, District, amount, grace_period, maturity, sex, domainName, loan_share_amount, close_date, interest_rate, active, 
                      loan_cycle, first_name, last_name, bad_loan
FROM         dbo.GLoanSizeMaturityGraceDomainDistrict
UNION ALL
SELECT     OLB, Contract, start_date, loan_officer, Product, District, amount, grace_period, maturity, sex, domainName, loan_share_amount, close_date, interest_rate, active, 
                      loan_cycle, first_name, last_name, bad_loan
FROM         dbo.ILoanSizeMaturityGraceDomainDistrict
'
GO
/****** Objet :  View [dbo].[PortfolioAndPAREvolutionByHeadQuarterView]    Date de génération du script : 08/21/2007 13:34:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[PortfolioAndPAREvolutionByHeadQuarterView]') AND OBJECTPROPERTY(id, N'IsView') = 1)
EXEC dbo.sp_executesql @statement = N'
CREATE VIEW [dbo].[PortfolioAndPAREvolutionByHeadQuarterView]
AS
SELECT     TOP 100 PERCENT conso_number,year, period, SUM(OLB) AS OLB, SUM(nber_of_contracts) AS nber_of_contracts, SUM(PAR1_30) AS PAR1_30, SUM(PAR31_60) 
                      AS PAR31_60, SUM(PAR61_90) AS PAR61_90, SUM(PAR91_180) AS PAR91_180, SUM(PAR181_365) AS PAR181_365, SUM(PAR365) AS PAR365
FROM         dbo.PortfolioAndPAREvolutionView
GROUP BY period, year, conso_number
ORDER BY conso_number
'
GO
/****** Objet :  View [dbo].[PortfolioAndPAREvolutionByBranchLast12MonthsView]    Date de génération du script : 08/21/2007 13:34:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[PortfolioAndPAREvolutionByBranchLast12MonthsView]') AND OBJECTPROPERTY(id, N'IsView') = 1)
EXEC dbo.sp_executesql @statement = N'
CREATE VIEW [dbo].[PortfolioAndPAREvolutionByBranchLast12MonthsView]
AS
SELECT TOP 12    dbo.PortfolioAndPAREvolutionView.*
FROM         dbo.PortfolioAndPAREvolutionView
WHERE ( (conso_number > (SELECT MAX(conso_number) FROM PortfolioAndPAREvolutionView WHERE period = ''M'') - 12) AND (period = ''M'') )
 OR  ( (conso_number > (SELECT MAX(conso_number) FROM PortfolioAndPAREvolutionView WHERE period = ''W'') - 52) AND (period = ''W'') )
'
GO
/****** Objet :  View [dbo].[Conso_LoanSizeMaturityGraceDomainDistrict]    Date de génération du script : 08/21/2007 13:34:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[Conso_LoanSizeMaturityGraceDomainDistrict]') AND OBJECTPROPERTY(id, N'IsView') = 1)
EXEC dbo.sp_executesql @statement = N'CREATE VIEW [dbo].[Conso_LoanSizeMaturityGraceDomainDistrict]
AS
SELECT     branch_code, conso_number, OLB, contract_code, loan_officer, product, district, amount, grace_period, maturity, is_male, domain_name, loan_share_amount, year, 
                      period, active, first_name, last_name, father_name
FROM         dbo.Conso_GLoanSizeMaturityGraceDomainDistrict
UNION ALL
SELECT     branch_code, conso_number, OLB, contract_code, loan_officer, product, district, amount, grace_period, nb_of_installments, is_male, domain_name, 
                      loan_share_amount, year, period, active, first_name, last_name, father_name
FROM         dbo.Conso_ILoanSizeMaturityGraceDomainDistrict
'
GO
/****** Objet :  View [dbo].[LoanSizeMaturityGraceDomainDistrict_FullyRepaid]    Date de génération du script : 08/21/2007 13:34:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[LoanSizeMaturityGraceDomainDistrict_FullyRepaid]') AND OBJECTPROPERTY(id, N'IsView') = 1)
EXEC dbo.sp_executesql @statement = N'CREATE VIEW [dbo].[LoanSizeMaturityGraceDomainDistrict_FullyRepaid]
AS
SELECT     OLB, Contract, loan_officer, Product, District, amount, grace_period, maturity, sex, domainName, loan_share_amount, start_date, close_date, active
FROM         dbo.GLoanSizeMaturityGraceDomainDistrict_FullyRepaid
UNION ALL
SELECT     OLB, Contract, loan_officer, Product, District, amount, grace_period, maturity, sex, domainName, loan_share_amount, start_date, close_date, active
FROM         dbo.ILoanSizeMaturityGraceDomainDistrict_FullyRepaid
'
GO
/****** Objet :  View [dbo].[Conso_LoansPAR]    Date de génération du script : 08/21/2007 13:34:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[Conso_LoansPAR]') AND OBJECTPROPERTY(id, N'IsView') = 1)
EXEC dbo.sp_executesql @statement = N'CREATE VIEW [dbo].[Conso_LoansPAR]
AS
SELECT     branch_code, conso_number, loan_officer, product, contract_code, district, domain_name, days_late, OLB, PAR1_30, PAR31_60, PAR61_90, PAR91_180, PAR181_365,
                       PAR365,year,period
FROM         dbo.Conso_GLoansPAR
UNION ALL
SELECT     branch_code, conso_number, loan_officer, product, contract_code, district, domain_name, days_late, OLB, PAR1_30, PAR31_60, PAR61_90, PAR91_180, PAR181_365,
                       PAR365,year,period
FROM         dbo.Conso_ILoansPAR
'
GO

/****** Objet :  View [dbo].[PortfolioAndPAREvolutionByHeadQuarterLast12MonthsView]    Date de génération du script : 08/21/2007 13:34:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[PortfolioAndPAREvolutionByHeadQuarterLast12MonthsView]') AND OBJECTPROPERTY(id, N'IsView') = 1)
EXEC dbo.sp_executesql @statement = N'
CREATE VIEW [dbo].[PortfolioAndPAREvolutionByHeadQuarterLast12MonthsView]
AS
SELECT     TOP 12 dbo.PortfolioAndPAREvolutionByHeadQuarterView.*
FROM         dbo.PortfolioAndPAREvolutionByHeadQuarterView
ORDER BY conso_number DESC
'
GO



/********************************************/
/******************  MINE  ******************/
/********************************************/

/*************************/
/*******  Common  ********/
/*************************/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[InternalCurrency]') AND OBJECTPROPERTY(id, N'IsView') = 1)
EXEC dbo.sp_executesql @statement = N'CREATE VIEW InternalCurrency
AS 
select stringValue AS internal_currency from GeneralParameters where name = ''INTERNAL_CURRENCY''
'
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[ExternalCurrency]') AND OBJECTPROPERTY(id, N'IsView') = 1)
EXEC dbo.sp_executesql @statement = N'CREATE VIEW ExternalCurrency
AS 
select stringValue AS external_currency from GeneralParameters where name = ''EXTERNAL_CURRENCY''
'
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].OLBView') AND OBJECTPROPERTY(id, N'IsView') = 1)
EXEC dbo.sp_executesql @statement = N'CREATE VIEW OLBView
AS
SELECT SUM (OLB) AS Expr1000 FROM LoanSizeMaturityGraceDomainDistrict
'
GO
/*************************/
/***  FinancialStock  ****/
/*************************/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[FinancialStock_Assets]') AND OBJECTPROPERTY(id, N'IsView') = 1)
EXEC dbo.sp_executesql @statement = N'CREATE VIEW FinancialStock_Assets
AS
SELECT * FROM Accounts WHERE description = 1
'
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[FinancialStock_Liabilities]') AND OBJECTPROPERTY(id, N'IsView') = 1)
EXEC dbo.sp_executesql @statement = N'CREATE VIEW FinancialStock_Liabilities
AS
SELECT * FROM Accounts WHERE description = 2
'
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[FinancialStock_Income]') AND OBJECTPROPERTY(id, N'IsView') = 1)
EXEC dbo.sp_executesql @statement = N'CREATE VIEW FinancialStock_Income
AS
SELECT * FROM Accounts WHERE description = 3
'
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[FinancialStock_Expenses]') AND OBJECTPROPERTY(id, N'IsView') = 1)
EXEC dbo.sp_executesql @statement = N'CREATE VIEW FinancialStock_Expenses
AS
SELECT * FROM Accounts WHERE description = 4
'
GO

/*************************/
/*****  OLB_Per_Loan *****/
/*************************/
/****** Objet :  View [dbo].[OLBPerLoanView]    Date de génération du script : 08/21/2007 13:34:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[OLBPerLoanView]') AND OBJECTPROPERTY(id, N'IsView') = 1)
EXEC dbo.sp_executesql @statement = N'CREATE VIEW [dbo].[OLBPerLoanView]
AS
SELECT     SUM(dbo.Installments.capital_repayment) - SUM(dbo.Installments.paid_capital) AS OLB, dbo.Contracts.contract_code AS Contract, 
                      dbo.Users.first_name AS loan_officer, dbo.Packages.name AS Product, dbo.Districts.name AS District, ISNULL(dbo.Groups.name, 
                      dbo.Persons.first_name + '' '' + dbo.Persons.last_name) AS clientName
FROM         dbo.Installments INNER JOIN
                      dbo.Contracts ON dbo.Installments.contract_id = dbo.Contracts.id INNER JOIN
                      dbo.Credit ON dbo.Contracts.id = dbo.Credit.id INNER JOIN
                      dbo.Users ON dbo.Credit.loanofficer_id = dbo.Users.id INNER JOIN
                      dbo.Packages ON dbo.Packages.id = dbo.Credit.package_id INNER JOIN
                      dbo.Tiers ON dbo.Tiers.id = dbo.Contracts.beneficiary_id INNER JOIN
                      dbo.Districts ON dbo.Districts.id = dbo.Tiers.district_id LEFT OUTER JOIN
                      dbo.Persons ON dbo.Tiers.id = dbo.Persons.id LEFT OUTER JOIN
                      dbo.Groups ON dbo.Tiers.id = dbo.Groups.id
WHERE     (dbo.Credit.written_off = 0) AND (dbo.Credit.disbursed = 1)
GROUP BY dbo.Installments.contract_id, dbo.Contracts.contract_code, dbo.Users.first_name, dbo.Packages.name, dbo.Districts.name, dbo.Groups.name, 
                      dbo.Persons.first_name, dbo.Persons.last_name
HAVING      (SUM(dbo.Installments.capital_repayment) - SUM(dbo.Installments.paid_capital) > 0.02)
'
GO
/*************************/
/***   PortfolioQuality **/
/*************************/
/****** Objet :  View [dbo].[CurrentRepaymentRateView]    Date de génération du script : 08/21/2007 13:34:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[CurrentRepaymentRateView]') AND OBJECTPROPERTY(id, N'IsView') = 1)
EXEC dbo.sp_executesql @statement = N'CREATE VIEW [dbo].[CurrentRepaymentRateView]
AS
SELECT     id, first_name,
                          (SELECT     ISNULL(SUM(dbo.RepaymentEvents.principal), 0) AS Expr1
                            FROM          dbo.Contracts INNER JOIN
                                                   dbo.Credit ON dbo.Contracts.id = dbo.Credit.id INNER JOIN
                                                   dbo.ContractEvents ON dbo.Contracts.id = dbo.ContractEvents.contract_id INNER JOIN
                                                   dbo.RepaymentEvents ON dbo.ContractEvents.id = dbo.RepaymentEvents.id
                            WHERE      (dbo.ContractEvents.event_date >= DATEADD(mm, - 1, GETDATE())) AND (dbo.ContractEvents.event_date < GETDATE()) AND 
                                                   (dbo.ContractEvents.user_id = UsersTable.id)) AS PrincipalReceived,
                          (SELECT     SUM(dbo.Installments.capital_repayment) AS Expr1
                            FROM          dbo.Contracts AS Contracts_5 INNER JOIN
                                                   dbo.Credit AS Credit_5 ON Contracts_5.id = Credit_5.id INNER JOIN
                                                   dbo.Installments ON Credit_5.id = dbo.Installments.contract_id
                            WHERE      (Credit_5.disbursed = 1) AND (dbo.Installments.expected_date >= DATEADD(mm, - 1, GETDATE())) AND 
                                                   (dbo.Installments.expected_date < GETDATE()) AND (Credit_5.loanofficer_id = UsersTable.id)) +
                          (SELECT     SUM(Installments_3.capital_repayment - Installments_3.paid_capital) AS Expr1
                            FROM          dbo.Contracts AS Contracts_4 INNER JOIN
                                                   dbo.Credit AS Credit_4 ON Contracts_4.id = Credit_4.id INNER JOIN
                                                   dbo.Installments AS Installments_3 ON Credit_4.id = Installments_3.contract_id
                            WHERE      (Installments_3.capital_repayment - Installments_3.paid_capital > 0.02) AND (Credit_4.loanofficer_id = UsersTable.id)) 
                      AS PrincipalExpected,
                          (SELECT     ISNULL(SUM(RepaymentEvents_1.principal), 0) AS Expr1
                            FROM          dbo.Contracts AS Contracts_3 INNER JOIN
                                                   dbo.Credit AS Credit_3 ON Contracts_3.id = Credit_3.id INNER JOIN
                                                   dbo.ContractEvents AS ContractEvents_1 ON Contracts_3.id = ContractEvents_1.contract_id INNER JOIN
                                                   dbo.RepaymentEvents AS RepaymentEvents_1 ON ContractEvents_1.id = RepaymentEvents_1.id
                            WHERE      (ContractEvents_1.event_date >= DATEADD(mm, - 1, GETDATE())) AND (ContractEvents_1.event_date < GETDATE()) AND 
                                                   (ContractEvents_1.user_id = UsersTable.id)) /
                          ((SELECT     SUM(Installments_2.capital_repayment) AS Expr1
                              FROM         dbo.Contracts AS Contracts_2 INNER JOIN
                                                    dbo.Credit AS Credit_2 ON Contracts_2.id = Credit_2.id INNER JOIN
                                                    dbo.Installments AS Installments_2 ON Credit_2.id = Installments_2.contract_id
                              WHERE     (Credit_2.disbursed = 1) AND (Installments_2.expected_date >= DATEADD(mm, - 1, GETDATE())) AND 
                                                    (Installments_2.expected_date < GETDATE()) AND (Credit_2.loanofficer_id = UsersTable.id)) +
                          (SELECT     SUM(Installments_1.capital_repayment - Installments_1.paid_capital) AS Expr1
                            FROM          dbo.Contracts AS Contracts_1 INNER JOIN
                                                   dbo.Credit AS Credit_1 ON Contracts_1.id = Credit_1.id INNER JOIN
                                                   dbo.Installments AS Installments_1 ON Credit_1.id = Installments_1.contract_id
                            WHERE      (Installments_1.capital_repayment - Installments_1.paid_capital > 0.02) AND (Credit_1.loanofficer_id = UsersTable.id))) 
                      AS CurrentRepaymentRate
FROM         dbo.Users AS UsersTable
'
GO
/****** Objet :  View [dbo].[OverDueView]    Date de génération du script : 08/21/2007 13:34:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[OverDueView]') AND OBJECTPROPERTY(id, N'IsView') = 1)
EXEC dbo.sp_executesql @statement = N'CREATE VIEW [dbo].[OverDueView]
AS
SELECT     id, first_name AS UserName,
                          (SELECT     SUM(dbo.Installments.capital_repayment - dbo.Installments.paid_capital) AS Expr1
                            FROM          dbo.Users INNER JOIN
                                                   dbo.Credit ON dbo.Users.id = dbo.Credit.loanofficer_id INNER JOIN
                                                   dbo.Installments ON dbo.Credit.id = dbo.Installments.contract_id
                            WHERE      (dbo.Users.id = dbo.Users.id) AND (dbo.Installments.expected_date < GETDATE()) AND 
                                                   (dbo.Installments.capital_repayment - dbo.Installments.paid_capital > 0)) AS OverDue
FROM         dbo.Users AS Users_1
'
GO
/*************************/
/*** DormantCustomers  ***/
/*************************/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[DormantCustomersView]') AND OBJECTPROPERTY(id, N'IsView') = 1)
EXEC dbo.sp_executesql @statement = N'CREATE VIEW [DormantCustomersView]
AS
SELECT UserTable.first_name+'' ''+UserTable.last_name AS loan_officer_name,(SELECT COUNT(ISNULL(Persons_1.id, Persons_2.id)) 
FROM PersonGroupBelonging 
INNER JOIN Groups ON PersonGroupBelonging.group_id = Groups.id 
INNER JOIN Persons AS Persons_1 ON PersonGroupBelonging.person_id = Persons_1.id 
RIGHT OUTER JOIN Tiers 
INNER JOIN Contracts ON Tiers.id = Contracts.beneficiary_id 
INNER JOIN Credit ON Contracts.id = Credit.id 
INNER JOIN Users ON Credit.loanofficer_id = Users.id ON Groups.id = Tiers.id 
LEFT OUTER JOIN Persons AS Persons_2 ON Tiers.id = Persons_2.id 
WHERE (Tiers.active = 0) AND (Credit.loanofficer_id = UserTable.id)) 
AS non_active_clients FROM Users AS UserTable
'
GO

/*************************/
/****  QualityReport  ****/
/*************************/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[QualityReport_GroupMembers]') AND OBJECTPROPERTY(id, N'IsView') = 1)
EXEC dbo.sp_executesql @statement = N'CREATE VIEW QualityReport_GroupMembers
AS
SELECT     TOP 100 PERCENT dbo.Contracts.contract_code,
                          (SELECT     COUNT(person_id) AS Expr1
                            FROM          dbo.PersonGroupBelonging
                            WHERE      (group_id = dbo.Contracts.beneficiary_id)) AS pCount
FROM         dbo.Contracts INNER JOIN
                      dbo.Tiers ON dbo.Contracts.beneficiary_id = dbo.Tiers.id
WHERE     (dbo.Tiers.client_type_code = ''G'') AND
                          ((SELECT     COUNT(person_id) AS Expr1
                              FROM         dbo.PersonGroupBelonging AS PersonGroupBelonging_1
                              WHERE     (group_id = dbo.Contracts.beneficiary_id)) = 0) AND
                          ((SELECT     SUM(capital_repayment - paid_capital) AS Expr1
                              FROM         dbo.Installments
                              WHERE     (contract_id = dbo.Contracts.id)) > 0.02)
ORDER BY pCount
'
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[QualityReport_GroupMembersOld]') AND OBJECTPROPERTY(id, N'IsView') = 1)
EXEC dbo.sp_executesql @statement = N'CREATE VIEW QualityReport_GroupMembersOld
AS
SELECT     TOP 100 PERCENT dbo.Contracts.contract_code,
                          (SELECT     COUNT(person_id) AS Expr1
                            FROM          dbo.PersonGroupBelonging
                            WHERE      (group_id = dbo.Contracts.beneficiary_id)) AS pCount
FROM         dbo.Contracts INNER JOIN
                      dbo.Tiers ON dbo.Contracts.beneficiary_id = dbo.Tiers.id
WHERE     (dbo.Tiers.client_type_code = ''G'') AND
                          ((SELECT     COUNT(person_id) AS Expr1
                              FROM         dbo.PersonGroupBelonging AS PersonGroupBelonging_1
                              WHERE     (group_id = dbo.Contracts.beneficiary_id)) = 0) AND
                          ((SELECT     SUM(capital_repayment - paid_capital) AS Expr1
                              FROM         dbo.Installments
                              WHERE     (contract_id = dbo.Contracts.id)) <= 0.02)
ORDER BY pCount
'
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[QualityReport_ActivePersonsPhysical]') AND OBJECTPROPERTY(id, N'IsView') = 1)
EXEC dbo.sp_executesql @statement = N'CREATE VIEW QualityReport_ActivePersonsPhysical
AS
select [Persons].[id], [Persons].[first_name], [Persons].[sex], [Persons].[identification_data], [Persons].[last_name], 
[Persons].[birth_date], [Persons].[household_head], [Persons].[nb_of_dependents], [Persons].[nb_of_children],
[Persons].[children_basic_education], [Persons].[livestock_number], [Persons].[livestock_type], 
[Persons].[landplot_size], [Persons].[home_size], [Persons].[home_time_living_in],[Persons].[capital_other_equipments],
[Persons].[activity_id], [Persons].[experience], [Persons].[nb_of_people], [Persons].[monthly_income],
[Persons].[monthly_expenditure], [Persons].[comments], [Persons].[image_path],
[Tiers].id AS id1,
[Tiers].[client_type_code], [Tiers].[scoring], [Tiers].[loan_cycle],
[Tiers].[active], [Tiers].[bad_client],
[Tiers].[other_org_name], [Tiers].[other_org_amount], [Tiers].[other_org_debts], 
[Tiers].[district_id],[Tiers].[city], [Tiers].[address],
[Tiers].[secondary_district_id], [Tiers].[secondary_city],[Tiers].[secondary_address],
[Tiers].[cash_input_voucher_number],  [Tiers].[cash_output_voucher_number], [Tiers].[creation_date]
from dbo.Persons 
inner join dbo.Tiers on dbo.Tiers.id = dbo.Persons.id 
where isnull(birth_date,-1)=-1 or 
isnull(dbo.Persons.nb_of_dependents,-1)=-1 or 
isnull(nb_of_children,-1)=-1 or 
isnull(children_basic_education,-1) = -1 
and dbo.Tiers.active = 1
'
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[QualityReport_PersonsPhysicalInformation]') AND OBJECTPROPERTY(id, N'IsView') = 1)
EXEC dbo.sp_executesql @statement = N'CREATE VIEW [QualityReport_PersonsPhysicalInformation]
AS
select * from dbo.Persons 
where isnull(birth_date,-1)=-1 or 
isnull(dbo.Persons.nb_of_dependents,-1)=-1 or 
isnull(nb_of_children,-1)=-1 or 
isnull(children_basic_education,-1) = -1
'
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[QualityReport_shares]') AND OBJECTPROPERTY(id, N'IsView') = 1)
EXEC dbo.sp_executesql @statement = N'CREATE VIEW QualityReport_shares
AS
select Contract, sum(loan_share_amount) as total_shares, amount 
from dbo.GLoanSizeMaturityGraceDomainDistrict
group by Contract,amount having sum(loan_share_amount) <> amount
'
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[QualityReport_PersonFinancial]') AND OBJECTPROPERTY(id, N'IsView') = 1)
EXEC dbo.sp_executesql @statement = N'CREATE VIEW QualityReport_PersonFinancial
AS
select * from dbo.Persons 
where isnull(monthly_income,-1)=-1 or 
isnull(monthly_expenditure,-1)=-1
'
GO

/****** Object:  View [dbo].[GLoansPAR]    Script Date: 12/13/2007 10:38:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[GLoansPAR]') AND OBJECTPROPERTY(id, N'IsView') = 1)
EXEC dbo.sp_executesql @statement = N'CREATE VIEW  [dbo].[GLoansPAR]
AS
SELECT     TOP 100 PERCENT dbo.Users.first_name + '' '' + dbo.Users.last_name AS loan_officer_name, dbo.Packages.name AS product, dbo.Contracts.contract_code, 
                      dbo.Contracts.start_date, dbo.Districts.name AS district_name, dbo.DomainOfApplications.name AS activity_name, ISNULL
                          ((SELECT     SUM(capital_repayment) - SUM(paid_capital) AS Expr1
                              FROM         dbo.Installments
                              WHERE     (contract_id = Credit.id)), 0) * dbo.PersonGroupBelonging.loan_share_amount / Credit.amount AS OLB, ISNULL
                          ((SELECT     SUM(capital_repayment) - SUM(paid_capital) AS Expr1
                              FROM         dbo.ReportInstallmentsView
                              WHERE     (contract_id = Credit.id)
                              GROUP BY contract_id
                              HAVING      (MAX(day_late) > 0) AND (MAX(day_late) <= 30)), 0) * dbo.PersonGroupBelonging.loan_share_amount / Credit.amount AS PAR1_30, ISNULL
                          ((SELECT     SUM(capital_repayment) - SUM(paid_capital) AS Expr1
                              FROM         dbo.ReportInstallmentsView AS ReportInstallmentsView_11
                              WHERE     (contract_id = Credit.id)
                              GROUP BY contract_id
                              HAVING      (MAX(day_late) > 30) AND (MAX(day_late) <= 60)), 0) * dbo.PersonGroupBelonging.loan_share_amount / Credit.amount AS PAR31_60, ISNULL
                          ((SELECT     SUM(capital_repayment) - SUM(paid_capital) AS Expr1
                              FROM         dbo.ReportInstallmentsView AS ReportInstallmentsView_10
                              WHERE     (contract_id = Credit.id)
                              GROUP BY contract_id
                              HAVING      (MAX(day_late) > 60) AND (MAX(day_late) <= 90)), 0) * dbo.PersonGroupBelonging.loan_share_amount / Credit.amount AS PAR61_90, ISNULL
                          ((SELECT     SUM(capital_repayment) - SUM(paid_capital) AS Expr1
                              FROM         dbo.ReportInstallmentsView AS ReportInstallmentsView_9
                              WHERE     (contract_id = Credit.id)
                              GROUP BY contract_id
                              HAVING      (MAX(day_late) > 90) AND (MAX(day_late) <= 180)), 0) * dbo.PersonGroupBelonging.loan_share_amount / Credit.amount AS PAR91_180, ISNULL
                          ((SELECT     SUM(capital_repayment) - SUM(paid_capital) AS Expr1
                              FROM         dbo.ReportInstallmentsView AS ReportInstallmentsView_8
                              WHERE     (contract_id = Credit.id)
                              GROUP BY contract_id
                              HAVING      (MAX(day_late) > 180) AND (MAX(day_late) <= 365)), 0) * dbo.PersonGroupBelonging.loan_share_amount / Credit.amount AS PAR181_365, 
                      ISNULL
                          ((SELECT     SUM(capital_repayment) - SUM(paid_capital) AS Expr1
                              FROM         dbo.ReportInstallmentsView AS ReportInstallmentsView_7
                              WHERE     (contract_id = Credit.id)
                              GROUP BY contract_id
                              HAVING      (MAX(day_late) > 365)), 0) * dbo.PersonGroupBelonging.loan_share_amount / Credit.amount AS CreditPAR365,
                          (SELECT     MAX(day_late) AS Expr1
                            FROM          dbo.ReportInstallmentsView AS ReportInstallmentsView_2
                            WHERE      (contract_id = Credit.id)) AS days_late
FROM         dbo.Contracts INNER JOIN
                      dbo.Credit AS Credit ON dbo.Contracts.id = Credit.id INNER JOIN
                      dbo.Packages ON Credit.package_id = dbo.Packages.id INNER JOIN
                      dbo.Tiers ON dbo.Contracts.beneficiary_id = dbo.Tiers.id INNER JOIN
                      dbo.Users ON Credit.loanofficer_id = dbo.Users.id INNER JOIN
                      dbo.Districts ON dbo.Tiers.district_id = dbo.Districts.id INNER JOIN
                      dbo.PersonGroupBelonging ON dbo.Tiers.id = dbo.PersonGroupBelonging.group_id INNER JOIN
                      dbo.Persons ON dbo.PersonGroupBelonging.person_id = dbo.Persons.id INNER JOIN
                      dbo.DomainOfApplications ON dbo.Persons.activity_id = dbo.DomainOfApplications.id
WHERE     (Credit.disbursed = 1) AND (Credit.written_off = 0) AND
                          ((SELECT     MAX(day_late) AS Expr1
                              FROM         dbo.ReportInstallmentsView AS ReportInstallmentsView_1
                              WHERE     (contract_id = Credit.id)) > 0)
'
/****** Object:  View [dbo].[ILoansPAR]    Script Date: 12/13/2007 10:38:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[ILoansPAR]') AND OBJECTPROPERTY(id, N'IsView') = 1)
EXEC dbo.sp_executesql @statement = N'CREATE VIEW [ILoansPAR]
AS
SELECT     TOP 100 PERCENT dbo.Users.first_name + '' '' + dbo.Users.last_name AS loan_officer_name, dbo.Packages.name AS product, dbo.Contracts.contract_code, 
                      dbo.Contracts.start_date, dbo.Districts.name AS district_name, dbo.DomainOfApplications.name AS activity_name, ISNULL
                          ((SELECT     SUM(capital_repayment) - SUM(paid_capital) AS Expr1
                              FROM         dbo.Installments
                              WHERE     (contract_id = dbo.Credit.id)), 0) AS OLB, ISNULL
                          ((SELECT     SUM(capital_repayment) - SUM(paid_capital) AS Expr1
                              FROM         dbo.ReportInstallmentsView
                              WHERE     (contract_id = dbo.Credit.id)
                              GROUP BY contract_id
                              HAVING      (MAX(day_late) > 0) AND (MAX(day_late) <= 30)), 0) AS PAR1_30, ISNULL
                          ((SELECT     SUM(capital_repayment) - SUM(paid_capital) AS Expr1
                              FROM         dbo.ReportInstallmentsView AS ReportInstallmentsView_11
                              WHERE     (contract_id = dbo.Credit.id)
                              GROUP BY contract_id
                              HAVING      (MAX(day_late) > 30) AND (MAX(day_late) <= 60)), 0) AS PAR31_60, ISNULL
                          ((SELECT     SUM(capital_repayment) - SUM(paid_capital) AS Expr1
                              FROM         dbo.ReportInstallmentsView AS ReportInstallmentsView_10
                              WHERE     (contract_id = dbo.Credit.id)
                              GROUP BY contract_id
                              HAVING      (MAX(day_late) > 60) AND (MAX(day_late) <= 90)), 0) AS PAR61_90, ISNULL
                          ((SELECT     SUM(capital_repayment) - SUM(paid_capital) AS Expr1
                              FROM         dbo.ReportInstallmentsView AS ReportInstallmentsView_9
                              WHERE     (contract_id = dbo.Credit.id)
                              GROUP BY contract_id
                              HAVING      (MAX(day_late) > 90) AND (MAX(day_late) <= 180)), 0) AS PAR91_180, ISNULL
                          ((SELECT     SUM(capital_repayment) - SUM(paid_capital) AS Expr1
                              FROM         dbo.ReportInstallmentsView AS ReportInstallmentsView_8
                              WHERE     (contract_id = dbo.Credit.id)
                              GROUP BY contract_id
                              HAVING      (MAX(day_late) > 180) AND (MAX(day_late) <= 365)), 0) AS PAR181_365, ISNULL
                          ((SELECT     SUM(capital_repayment) - SUM(paid_capital) AS Expr1
                              FROM         dbo.ReportInstallmentsView AS ReportInstallmentsView_7
                              WHERE     (contract_id = dbo.Credit.id)
                              GROUP BY contract_id
                              HAVING      (MAX(day_late) > 365)), 0) AS PAR365,
                          (SELECT     MAX(day_late) AS Expr1
                            FROM          dbo.ReportInstallmentsView AS ReportInstallmentsView_2
                            WHERE      (contract_id = dbo.Credit.id)) AS days_late
FROM         dbo.Contracts INNER JOIN
                      dbo.Credit ON dbo.Contracts.id = dbo.Credit.id INNER JOIN
                      dbo.Packages ON dbo.Credit.package_id = dbo.Packages.id INNER JOIN
                      dbo.Tiers ON dbo.Contracts.beneficiary_id = dbo.Tiers.id INNER JOIN
                      dbo.Users ON dbo.Credit.loanofficer_id = dbo.Users.id INNER JOIN
                      dbo.Persons ON dbo.Tiers.id = dbo.Persons.id INNER JOIN
                      dbo.Districts ON dbo.Tiers.district_id = dbo.Districts.id INNER JOIN
                      dbo.DomainOfApplications ON dbo.Persons.activity_id = dbo.DomainOfApplications.id
WHERE     (dbo.Credit.disbursed = 1) AND (dbo.Credit.written_off = 0) AND
                          ((SELECT     MAX(day_late) AS Expr1
                              FROM         dbo.ReportInstallmentsView AS ReportInstallmentsView_1
                              WHERE     (contract_id = dbo.Credit.id)) > 0)
'

/****** Object:  View [dbo].[LoansPAR]    Script Date: 12/13/2007 10:36:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[LoansPAR]') AND OBJECTPROPERTY(id, N'IsView') = 1)
EXEC dbo.sp_executesql @statement = N'CREATE VIEW  [dbo].[LoansPAR]
AS
SELECT     loan_officer_name, product, contract_code, start_date, district_name, activity_name, OLB, PAR1_30, PAR31_60, PAR61_90, PAR91_180, PAR181_365, PAR365, 
                      days_late
FROM         dbo.ILoansPAR
UNION ALL
SELECT     loan_officer_name, product, contract_code, start_date, district_name, activity_name, OLB, PAR1_30, PAR31_60, PAR61_90, PAR91_180, PAR181_365, CreditPAR365, 
                      days_late
FROM         dbo.GLoansPAR
'
/*end*/

/*Stored procedures*/
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[LoanPortfolioAnalysis_OverView]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[LoanPortfolioAnalysis_OverView]
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[LoanPortfolioAnalysis_Repayments]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[LoanPortfolioAnalysis_Repayments]
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[LoanPortfolioAnalysis_LateLoansAndPrincipal]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[LoanPortfolioAnalysis_LateLoansAndPrincipal]
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[LoanPortfolioAnalysis_Provisioning]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[LoanPortfolioAnalysis_Provisioning]
GO
/*PARAnalysis*/
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[PARAnalysis_LoansPAR]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[PARAnalysis_LoansPAR]
GO


/*********************/
/****** DropOut ******/
/*********************/
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[DropOut_last2months_ByLoanOfficer]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[DropOut_last2months_ByLoanOfficer]
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[DropOut_last2months_ByProduct]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[DropOut_last2months_ByProduct]
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[DropOut_last2months_ByDistrict]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[DropOut_last2months_ByDistrict]
GO
/*************************/
/****** Repayments *******/
/*************************/
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[Repayments_sqlQuery]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[Repayments_sqlQuery]
GO
/*************************/
/*********  PAR  *********/
/*************************/
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[PAR_LoansPAR]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[PAR_LoansPAR]
GO
/*************************/
/****   Disbursments  ****/
/*************************/
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[Disbursments_sqlQuery]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[Disbursments_sqlQuery]
GO
/*************************/
/***   DelinquentLoans ***/
/*************************/
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[DelinquentLoans_sqlQuery]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[DelinquentLoans_sqlQuery]
GO

/*************************/
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[LoanFullyRepaid_ByLoanOfficer]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[LoanFullyRepaid_ByLoanOfficer]
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[LoanFullyRepaid_ByProduct]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[LoanFullyRepaid_ByProduct]
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[LoanFullyRepaid_ByDistrict]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[LoanFullyRepaid_ByDistrict]
GO
/****************************************/
/*** Disbursements_and_Reimbursements ***/
/****************************************/
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[Disbursements_and_Reimbursements_ByDistrict]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[Disbursements_and_Reimbursements_ByDistrict]
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[Disbursements_and_Reimbursements_ByLoanOfficer]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[Disbursements_and_Reimbursements_ByLoanOfficer]
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[Disbursements_and_Reimbursements_ByProduct]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[Disbursements_and_Reimbursements_ByProduct]
GO
/******************************/
/*** ClientsAndShareOfWomen ***/
/******************************/
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[ClientsAndShareOfWomen_ByDistrict]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[ClientsAndShareOfWomen_ByDistrict]
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[ClientsAndShareOfWomen_ByLoanOfficer]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[ClientsAndShareOfWomen_ByLoanOfficer]
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[ClientsAndShareOfWomen_ByProduct]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[ClientsAndShareOfWomen_ByProduct]
GO
/**************************************/
/*****  LoansDisbursed_Amount_Nb  *****/
/**************************************/
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[LoansDisbursed_Amount_Nb_ByDistrict]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[LoansDisbursed_Amount_Nb_ByDistrict]
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[LoansDisbursed_Amount_Nb_ByProduct]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[LoansDisbursed_Amount_Nb_ByProduct]
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[LoansDisbursed_Amount_Nb_ByActivity]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[LoansDisbursed_Amount_Nb_ByActivity]
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[LoansDisbursed_Amount_Nb_ByLoanOfficer]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[LoansDisbursed_Amount_Nb_ByLoanOfficer]
GO
/*************************/
/******  FollowUp  *******/
/*************************/
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[FollowUp_OLBAndLoansActif]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[FollowUp_OLBAndLoansActif]
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[FollowUp_OLBAndLoans]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[FollowUp_OLBAndLoans]
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[FollowUp_PortFolioAtRisk]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[FollowUp_PortFolioAtRisk]
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[FollowUp_PrincipalAndInterest]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[FollowUp_PrincipalAndInterest]
GO

/*************************/
/***  Conso_FollowUp  ****/
/*************************/
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[Conso_FollowUp_OLBAndLoansActif]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[Conso_FollowUp_OLBAndLoansActif]
GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[Conso_FollowUp_PortFolioAtRisk]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[Conso_FollowUp_PortFolioAtRisk]
GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[Conso_FollowUp_PrincipalAndInterestByProduct]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE [dbo].[Conso_FollowUp_PrincipalAndInterestByProduct]
GO

IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[FollowUp_OLBAndLoansActif]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE FollowUp_OLBAndLoansActif
@begindate datetime,
@user nvarchar(150)
AS
BEGIN
	DECLARE @sql nvarchar(1000)
	DECLARE @paramlist nvarchar(1000)

	 SELECT @sql = 'SELECT * 
					FROM LoanSizeMaturityGraceDomainDistrict 
					WHERE start_date <= @begindate AND active = 1 '

	 if @user is not null
		SELECT @sql = @sql + ' AND loan_officer  LIKE ''%'' + @user + ''%''' 

	 SELECT @paramlist = '@begindate datetime,
						  @user   nvarchar(150)'
	 EXEC dbo.sp_executesql @sql, @paramlist, @begindate, @user
END


IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[FollowUp_OLBAndLoans]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE FollowUp_OLBAndLoans
@begindate datetime,
@user nvarchar(150)
AS
BEGIN
	DECLARE @sql nvarchar(1000)
	DECLARE @paramlist nvarchar(1000)

	 SELECT @sql = 'SELECT * 
					FROM LoanSizeMaturityGraceDomainDistrict 
					WHERE start_date <= @beginDate '

	 if @user is not null
		SELECT @sql = @sql + ' AND loan_officer  LIKE ''%'' + @user + ''%''' 

	 SELECT @paramlist = '@begindate datetime,
						  @user   nvarchar(150)'
	 EXEC dbo.sp_executesql @sql, @paramlist, @begindate, @user
END


IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[FollowUp_PortFolioAtRisk]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [FollowUp_PortFolioAtRisk]
@begindate datetime,
@user nvarchar(150)
AS
BEGIN
	DECLARE @sql nvarchar(1000)
	DECLARE @paramlist nvarchar(1000)

	 SELECT @sql = 'SELECT * 
					FROM LoansPAR 
					WHERE  start_date <= @beginDate'

	 if @user is not null
		SELECT @sql = @sql + ' AND loan_officer_name  LIKE ''%'' + @user + ''%''' 

	 SELECT @paramlist = '@begindate datetime,
						  @user   nvarchar(150)'
	 EXEC dbo.sp_executesql @sql, @paramlist, @begindate, @user
END

IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[FollowUp_PrincipalAndInterest]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [FollowUp_PrincipalAndInterest]
@begindate datetime,
@endDate datetime,
@user nvarchar(150)
AS
BEGIN
	DECLARE @sql nvarchar(1000)
	DECLARE @paramlist nvarchar(1000)

	 SELECT @sql = 'SELECT Contracts.contract_code,
        Contracts.id, 
        Contracts.start_date AS startDate,
        Districts.name AS district,
        Users.first_name + '' '' +  Users.last_name AS loan_officer_name,
        packages.name AS product,

        (SELECT ISNULL(SUM(amount),0) FROM Credit,Contracts AS temp WHERE Credit.id = Contracts.id 
        AND temp.start_date >= @beginDate AND temp.start_date <= @endDate AND Credit.disbursed = 0) AS plannedDisbursment,

        (SELECT ISNULL(SUM(amount),0) FROM LoanDisbursmentEvents,ContractEvents WHERE LoanDisbursmentEvents.id = ContractEvents.id AND 
        ContractEvents.contract_id = Contracts.id AND ContractEvents.is_deleted = 0  
        AND ContractEvents.event_date >= @beginDate AND ContractEvents.event_date <= @endDate) AS currentDisbursment,

        (SELECT ISNULL(SUM(interests),0) FROM RepaymentEvents,ContractEvents WHERE RepaymentEvents.id = ContractEvents.id AND 
        ContractEvents.contract_id = Contracts.id AND ContractEvents.is_deleted = 0 
        AND ContractEvents.event_date >= @beginDate AND ContractEvents.event_date <= @endDate) AS currentInterestReimbursment,

        (SELECT ISNULL(SUM(principal),0) FROM RepaymentEvents,ContractEvents WHERE RepaymentEvents.id = ContractEvents.id AND 
        ContractEvents.contract_id = Contracts.id AND ContractEvents.is_deleted = 0 
        AND ContractEvents.event_date >= @beginDate AND ContractEvents.event_date <= @endDate) AS currentPrincipalReimbursment,

        (SELECT ISNULL(SUM(interest_repayment),0) FROM Installments WHERE Installments.contract_id = Contracts.id
        AND Installments.expected_date >= @beginDate AND Installments.expected_date <= @endDate) AS plannedInterestReimbursment,

        (SELECT ISNULL(SUM(capital_repayment),0) FROM Installments WHERE Installments.contract_id = Contracts.id
        AND Installments.expected_date >= @beginDate AND Installments.expected_date <= @endDate) AS plannedCapitalReimbursment

        FROM Contracts,Credit,Users,Packages,Tiers,Districts
        WHERE Credit.id = Contracts.id AND Users.id =  Credit.loanofficer_id AND credit.package_id = packages.id
        AND Contracts.beneficiary_id =  Tiers.id AND Districts.id =  Tiers.district_id'

	 if @user is not null
		SELECT @sql = @sql + ' AND Users.first_name  LIKE ''%'' + @user + ''%''' 

	 SELECT @paramlist = '@begindate datetime,
						  @endDate   datetime,
						  @user   nvarchar(150)'
	 EXEC dbo.sp_executesql @sql, @paramlist, @begindate, @endDate, @user
END
/**************************************/
/*****  LoansDisbursed_Amount_Nb  *****/
/**************************************/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[LoansDisbursed_Amount_Nb_ByDistrict]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [LoansDisbursed_Amount_Nb_ByDistrict]
@beginDate datetime,
@endDate datetime
AS
BEGIN
SELECT * FROM [LoanSizeMaturityGraceDomainDistrict] WHERE start_date >= @beginDate AND start_date <= @endDate
END
'
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[LoansDisbursed_Amount_Nb_ByProduct]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [LoansDisbursed_Amount_Nb_ByProduct]
@beginDate datetime,
@endDate datetime
AS
BEGIN
SELECT * FROM [LoanSizeMaturityGraceDomainDistrict] WHERE start_date >= @beginDate AND start_date <= @endDate
END
'
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[LoansDisbursed_Amount_Nb_ByActivity]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE LoansDisbursed_Amount_Nb_ByActivity
@beginDate datetime,
@endDate datetime
AS
BEGIN
SELECT * FROM [LoanSizeMaturityGraceDomainDistrict] WHERE start_date >= @beginDate AND start_date <= @endDate
END
'
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[LoansDisbursed_Amount_Nb_ByLoanOfficer]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [LoansDisbursed_Amount_Nb_ByLoanOfficer]
@beginDate datetime,
@endDate datetime
AS
BEGIN
SELECT * FROM [LoanSizeMaturityGraceDomainDistrict] WHERE start_date >= @beginDate AND start_date <= @endDate
END
'
GO
/******************************/
/*** ClientsAndShareOfWomen ***/
/******************************/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[ClientsAndShareOfWomen_ByDistrict]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE ClientsAndShareOfWomen_ByDistrict
@endDate datetime
AS
BEGIN
SELECT name ,
                            (SELECT COUNT(Contract) FROM LoanSizeMaturityGraceDomainDistrict,Contracts WHERE LoanSizeMaturityGraceDomainDistrict.Contract = Contracts.contract_code 
                            AND District = Districts.name AND Contracts.start_date <= @endDate) As NbOfClient,
                            (SELECT COUNT(Contract) FROM LoanSizeMaturityGraceDomainDistrict,Contracts WHERE LoanSizeMaturityGraceDomainDistrict.Contract = Contracts.contract_code
                            AND District = Districts.name AND sex <> ''M'' AND Contracts.start_date <= @endDate) AS NbOfWomen 
                            FROM Districts
END
'
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[ClientsAndShareOfWomen_ByLoanOfficer]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [ClientsAndShareOfWomen_ByLoanOfficer]
@endDate datetime
AS
BEGIN
SELECT first_name+'' ''+last_name AS loan_officer_name ,
                            (SELECT COUNT(Contract) FROM LoanSizeMaturityGraceDomainDistrict,Contracts WHERE LoanSizeMaturityGraceDomainDistrict.Contract = Contracts.contract_code 
                            AND loan_officer = Users.first_name AND Contracts.start_date <= @endDate) As clients_by_LO,
                            (SELECT COUNT(Contract) FROM LoanSizeMaturityGraceDomainDistrict,Contracts WHERE LoanSizeMaturityGraceDomainDistrict.Contract = Contracts.contract_code 
                            AND loan_officer = Users.first_name AND sex <> ''M'' AND Contracts.start_date <= @endDate) As women_by_LO 
                            FROM Users WHERE deleted = 0 AND role_code <> ''ADMIN''
END
'
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[ClientsAndShareOfWomen_ByProduct]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE ClientsAndShareOfWomen_ByProduct
@endDate datetime
AS
BEGIN
SELECT name ,
                            (SELECT COUNT(Contract) FROM LoanSizeMaturityGraceDomainDistrict,Contracts WHERE LoanSizeMaturityGraceDomainDistrict.Contract = Contracts.contract_code 
                            AND Product = Packages.name AND Contracts.start_date <= @endDate) As NbOfClient,
                            (SELECT COUNT(Contract) FROM LoanSizeMaturityGraceDomainDistrict,Contracts WHERE LoanSizeMaturityGraceDomainDistrict.Contract = Contracts.contract_code 
                            AND Product = Packages.name AND sex <> ''M'' AND Contracts.start_date <= @endDate) As NbOfWomen 
                            FROM Packages WHERE deleted = 0
END
'
GO

/****************************************/
/*** Disbursements_and_Reimbursements ***/
/****************************************/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[Disbursements_and_Reimbursements_ByDistrict]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE Disbursements_and_Reimbursements_ByDistrict
@beginDate datetime,
@endDate datetime
AS
BEGIN
SELECT name AS district_name,
                              (SELECT ISNULL(SUM(Credit.amount), 0) FROM Credit INNER JOIN Contracts ON dbo.Credit.id = dbo.Contracts.id 
                                INNER JOIN Packages ON dbo.Credit.package_id = dbo.Packages.id 
								INNER JOIN Tiers ON Contracts.beneficiary_id = Tiers.id
								
								WHERE      (dbo.Credit.disbursed = 1) 
                                AND (Tiers.district_id = Districts.id) AND (dbo.Contracts.start_date >= @beginDate) AND (dbo.Contracts.start_date <= @endDate)) AS disbursements_current,
                              
                                (SELECT     ISNULL(SUM(Credit.amount), 0) FROM          dbo.Credit INNER JOIN Contracts  
                                ON Credit.id = Contracts.id INNER JOIN Packages ON Credit.package_id = Packages.id INNER JOIN Tiers ON Contracts.beneficiary_id = Tiers.id
								WHERE      
                                (Tiers.district_id = Districts.id)  AND (dbo.Contracts.start_date >= @beginDate) AND (dbo.Contracts.start_date <= @endDate)) AS disbursements_planned, 
                              (SELECT     ISNULL(SUM(dbo.RepaymentEvents.principal),0) FROM         dbo.ContractEvents 
                                INNER JOIN RepaymentEvents ON dbo.ContractEvents.id = dbo.RepaymentEvents.id 
                                INNER JOIN Credit ON dbo.ContractEvents.contract_id = dbo.Credit.id 
                                INNER JOIN Contracts ON Credit.id = Contracts.id INNER JOIN Tiers ON Contracts.beneficiary_id = Tiers.id
								WHERE    ContractEvents.is_deleted = 0 AND 
                                (Tiers.district_id = Districts.id)  AND (ContractEvents.event_date >= @beginDate) AND (ContractEvents.event_date <= @endDate)) AS reimbursements_principal_current, 
                            (SELECT     ISNULL(SUM(dbo.RepaymentEvents.interests),0) FROM         dbo.ContractEvents 
                                INNER JOIN RepaymentEvents ON dbo.ContractEvents.id = dbo.RepaymentEvents.id 
                                INNER JOIN Credit ON dbo.ContractEvents.contract_id = dbo.Credit.id 
                                INNER JOIN Contracts ON Credit.id = Contracts.id INNER JOIN Tiers ON Contracts.beneficiary_id = Tiers.id
								WHERE    ContractEvents.is_deleted = 0 AND 
                                (Tiers.district_id = Districts.id)  AND (ContractEvents.event_date >= @beginDate) AND (ContractEvents.event_date <= @endDate)) AS reimbursements_interest_current,
                              (SELECT     ISNULL(SUM(dbo.Installments.capital_repayment),0) FROM Credit INNER JOIN
                                Packages ON Credit.package_id = Packages.id INNER JOIN Installments ON Credit.id = dbo.Installments.contract_id INNER JOIN Contracts ON Credit.id = Contracts.id INNER JOIN Tiers ON Contracts.beneficiary_id = Tiers.id
                                WHERE (Tiers.district_id = Districts.id) AND (dbo.Credit.disbursed = 1)  AND (Installments.expected_date >= @beginDate) AND (Installments.expected_date <= @endDate)) AS reimbursements_principal_planned,
                            (SELECT     ISNULL(SUM(dbo.Installments.interest_repayment),0) FROM Credit INNER JOIN
                                Packages ON Credit.package_id = Packages.id INNER JOIN Installments ON Credit.id = dbo.Installments.contract_id INNER JOIN Contracts ON Credit.id = Contracts.id INNER JOIN Tiers ON Contracts.beneficiary_id = Tiers.id
                                WHERE (Tiers.district_id = Districts.id) AND (dbo.Credit.disbursed = 1)  AND (Installments.expected_date >= @beginDate) AND (Installments.expected_date <= @endDate)) AS reimbursements_interest_planned
                            FROM Districts
END
'
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[Disbursements_and_Reimbursements_ByLoanOfficer]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE Disbursements_and_Reimbursements_ByLoanOfficer
@beginDate datetime,
@endDate datetime
AS
BEGIN
SELECT first_name+'' ''+last_name AS loan_officer_name,
                              (SELECT ISNULL(SUM(Credit.amount), 0) FROM Credit INNER JOIN Contracts ON dbo.Credit.id = dbo.Contracts.id 
                                INNER JOIN Packages ON dbo.Credit.package_id = dbo.Packages.id WHERE      (dbo.Credit.disbursed = 1) 
                                AND (Credit.loanofficer_id = Users.id) AND (dbo.Contracts.start_date >= @beginDate) AND (dbo.Contracts.start_date <= @endDate)) AS disbursements_current,
                              (SELECT     ISNULL(SUM(Credit.amount), 0) FROM          dbo.Credit INNER JOIN Contracts  
                                ON Credit.id = Contracts.id INNER JOIN Packages ON Credit.package_id = Packages.id WHERE      
                                (Credit.loanofficer_id = Users.id)  AND (dbo.Contracts.start_date >= @beginDate) AND (dbo.Contracts.start_date <= @endDate)) AS disbursements_planned, 
                              (SELECT     ISNULL(SUM(dbo.RepaymentEvents.principal),0) FROM         dbo.ContractEvents 
                                INNER JOIN RepaymentEvents ON dbo.ContractEvents.id = dbo.RepaymentEvents.id 
                                INNER JOIN Credit ON dbo.ContractEvents.contract_id = dbo.Credit.id 
                                INNER JOIN Contracts ON Credit.id = Contracts.id WHERE ContractEvents.is_deleted = 0 AND     
                                (Credit.loanofficer_id = Users.id)  AND (ContractEvents.event_date >= @beginDate) AND (ContractEvents.event_date <= @endDate)) AS reimbursements_principal_current, 
                            (SELECT     ISNULL(SUM(dbo.RepaymentEvents.interests),0) FROM         dbo.ContractEvents 
                                INNER JOIN RepaymentEvents ON dbo.ContractEvents.id = dbo.RepaymentEvents.id 
                                INNER JOIN Credit ON dbo.ContractEvents.contract_id = dbo.Credit.id 
                                INNER JOIN Contracts ON Credit.id = Contracts.id WHERE ContractEvents.is_deleted = 0 AND     
                                (Credit.loanofficer_id = Users.id)  AND (ContractEvents.event_date >= @beginDate) AND (ContractEvents.event_date <= @endDate)) AS reimbursements_interest_current,
                              (SELECT     ISNULL(SUM(dbo.Installments.capital_repayment),0) FROM Credit INNER JOIN
                                Packages ON Credit.package_id = Packages.id INNER JOIN Installments ON Credit.id = dbo.Installments.contract_id INNER JOIN Contracts ON Credit.id = Contracts.id 
                                WHERE (Credit.loanofficer_id = Users.id) AND (dbo.Credit.disbursed = 1)  AND (Installments.expected_date >= @beginDate) AND (Installments.expected_date <= @endDate)) AS reimbursements_principal_planned,
                            (SELECT     ISNULL(SUM(dbo.Installments.interest_repayment),0) FROM Credit INNER JOIN
                                Packages ON Credit.package_id = Packages.id INNER JOIN Installments ON Credit.id = dbo.Installments.contract_id INNER JOIN Contracts ON Credit.id = Contracts.id 
                                WHERE (Credit.loanofficer_id = Users.id) AND (dbo.Credit.disbursed = 1)  AND (Installments.expected_date >= @beginDate) AND (Installments.expected_date <= @endDate)) AS reimbursements_interest_planned
                            FROM         Users
END
'
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[Disbursements_and_Reimbursements_ByProduct]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE Disbursements_and_Reimbursements_ByProduct
@beginDate datetime,
@endDate datetime
AS
BEGIN
SELECT  PackagesTable.name AS loan_product,
                            (SELECT ISNULL(SUM(Credit.amount), 0)
                                   FROM Credit INNER JOIN Contracts ON Credit.id = Contracts.id INNER JOIN
                                   Packages ON Credit.package_id = Packages.id
                                   WHERE      (Credit.disbursed = 1) AND (Packages.id = PackagesTable.id) AND (Contracts.start_date >= @beginDate) AND 
	                               (Contracts.start_date <= @endDate)) AS disbursements_current,
                            (SELECT ISNULL(SUM(Credit.amount), 0)
                                   FROM Credit INNER JOIN Contracts ON Credit.id = Contracts.id INNER JOIN
                                   Packages ON Credit.package_id = Packages.id
                                   WHERE (Packages.id = PackagesTable.id) AND (Contracts.start_date >= @beginDate) AND 
	                               (Contracts.start_date <= @endDate)) AS disbursements_planned,
                            (SELECT ISNULL(SUM(RepaymentEvents.principal),0)
	                               FROM ContractEvents INNER JOIN RepaymentEvents ON ContractEvents.id = RepaymentEvents.id INNER JOIN
                                   Credit ON ContractEvents.contract_id = Credit.id
	                               WHERE (Credit.package_id = PackagesTable.id) AND (ContractEvents.is_deleted = 0) AND 
	                               (ContractEvents.event_date >= @beginDate) AND (ContractEvents.event_date <= @endDate)) AS reimbursements_principal_current,
                            (SELECT ISNULL(SUM(RepaymentEvents.interests),0)
	                               FROM ContractEvents INNER JOIN RepaymentEvents ON ContractEvents.id = RepaymentEvents.id INNER JOIN
                                   Credit ON ContractEvents.contract_id = Credit.id
	                               WHERE (Credit.package_id = PackagesTable.id) AND (ContractEvents.is_deleted = 0) AND 
	                               (ContractEvents.event_date >= @beginDate) AND (ContractEvents.event_date <= @endDate)) AS reimbursements_interest_current,
                            (SELECT ISNULL(SUM(Installments.capital_repayment),0)
                                   FROM Credit  INNER JOIN Packages ON Credit.package_id = Packages.id INNER JOIN
                                   Installments ON Credit.id = Installments.contract_id
                                   WHERE (Packages.id = PackagesTable.id) AND (Credit.disbursed = 1) AND 
	                               (Installments.expected_date >= @beginDate) AND (Installments.expected_date <= @endDate)) AS reimbursements_principal_planned,
                            (SELECT ISNULL(SUM(Installments.interest_repayment),0)
                                   FROM Credit  INNER JOIN Packages ON Credit.package_id = Packages.id INNER JOIN
                                   Installments ON Credit.id = Installments.contract_id
                                   WHERE (Packages.id = PackagesTable.id) AND (Credit.disbursed = 1) AND 
	                               (Installments.expected_date >= @beginDate) AND (Installments.expected_date <= @endDate)) AS reimbursements_interest_planned                    
                            FROM Packages AS PackagesTable
END
'
GO
/*************************/
/**** LoanFullyRepaid ****/
/*************************/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[LoanFullyRepaid_ByLoanOfficer]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [LoanFullyRepaid_ByLoanOfficer]
@beginDate datetime,
@endDate datetime
AS
BEGIN
SELECT first_name+'' ''+last_name AS loan_officer_name ,
                                (SELECT ISNULL(COUNT(TempTable.Contract),0) FROM (SELECT DISTINCT Contract,loan_officer FROM  LoanSizeMaturityGraceDomainDistrict_FullyRepaid) TempTable,
                                Contracts WHERE TempTable.Contract = Contracts.contract_code 
                                AND TempTable.loan_officer = Users.first_name AND Contracts.close_date > @beginDate AND Contracts.close_date <= @endDate) As nb_of_contracts
                                 FROM Users WHERE deleted = 0 AND role_code <> ''ADMIN''
END
'
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[LoanFullyRepaid_ByProduct]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE LoanFullyRepaid_ByProduct
@beginDate datetime,
@endDate datetime
AS
BEGIN
SELECT name ,
                                (SELECT ISNULL(COUNT(TempTable.Contract),0) FROM (SELECT DISTINCT Contract,Product FROM  LoanSizeMaturityGraceDomainDistrict_FullyRepaid) TempTable,
                                Contracts WHERE TempTable.Contract = Contracts.contract_code 
                                AND TempTable.Product = Packages.name AND Contracts.close_date > @beginDate AND Contracts.close_date <= @endDate) As nb_of_contracts 
                                FROM Packages WHERE deleted = 0
END
'
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[LoanFullyRepaid_ByDistrict]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE LoanFullyRepaid_ByDistrict
@beginDate datetime,
@endDate datetime
AS
BEGIN
SELECT name,
                                (SELECT ISNULL(COUNT(TempTable.Contract),0) FROM (SELECT DISTINCT Contract,District FROM  LoanSizeMaturityGraceDomainDistrict_FullyRepaid) TempTable,
                                Contracts WHERE Contracts.contract_code = TempTable.Contract 
                                AND TempTable.District = Districts.name AND Contracts.close_date > @beginDate AND Contracts.close_date <= @endDate) As nb_of_contracts 
                                FROM Districts
END
'
GO


/*************************/
/**** DelinquentLoans ****/
/*************************/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[DelinquentLoans_sqlQuery]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE DelinquentLoans_sqlQuery
@endDate datetime
AS
BEGIN
   (SELECT 	Packages.name AS package_name, 
			Contracts.contract_code, 
			Contracts.branch_code, 
			Credit.funding_line_code, 	
			ISNULL ((	SELECT SUM(capital_repayment - paid_capital) 
						FROM Installments 
						WHERE (contract_id = Credit.id)), 0) AS OLB, 
			ISNULL ((	SELECT SUM(capital_repayment - paid_capital) 
						FROM 
						(SELECT contract_id, 
								number, 
								expected_date, 
								interest_repayment, 
								capital_repayment, 
								paid_interest, 
								paid_capital, 
								capital_repayment - paid_capital + interest_repayment - paid_interest AS due_amount, 
								CASE WHEN capital_repayment - paid_capital + interest_repayment - paid_interest < 0.02 
									THEN 0 
									ELSE DATEDIFF(day, expected_date, @endDate) 
								END AS day_late 
						FROM Installments 
						WHERE expected_date < @endDate  AND (contract_id = dbo.Credit.id) ) AS ReportInstallmentsView 
						GROUP BY contract_id), 0) AS late_principal,
			ISNULL ((	SELECT SUM(paid_capital) 
						FROM (SELECT contract_id, number, expected_date, interest_repayment, 
						capital_repayment, paid_interest, paid_capital, capital_repayment - paid_capital + interest_repayment - paid_interest AS due_amount, CASE 
						WHEN capital_repayment - paid_capital + interest_repayment - paid_interest < 0.02 THEN 0 ELSE DATEDIFF(day, expected_date, @endDate) 
						END AS day_late FROM Installments) AS ReportInstallmentsView 
						WHERE (contract_id = Credit.id) GROUP BY contract_id), 0) AS paid_principal,
			(SELECT MAX(day_late) FROM (SELECT contract_id, number, expected_date, interest_repayment, capital_repayment, paid_interest, paid_capital,
				capital_repayment - paid_capital + interest_repayment - paid_interest AS due_amount, CASE WHEN capital_repayment - paid_capital + 
				interest_repayment - paid_interest < 0.02 THEN 0 ELSE DATEDIFF(day, expected_date, @endDate) END AS day_late FROM Installments) AS 
				ReportInstallmentsView WHERE (contract_id = Credit.id)) AS day_late, 
			Credit.amount,
			Contracts.start_date, 
			Contracts.close_date, 
			Users.first_name + '' '' + Users.last_name AS loan_officer_name,
			ISNULL(Groups.name, Persons.first_name + '' '' + Persons.last_name) AS customer_name,
			Districts.name AS district_name 
			
	FROM Groups RIGHT OUTER JOIN Persons RIGHT OUTER JOIN Credit INNER JOIN Contracts
			INNER JOIN Tiers ON Contracts.beneficiary_id = Tiers.id ON Credit.id = Contracts.id INNER JOIN Users ON Credit.loanofficer_id = 
			Users.id ON Persons.id = Tiers.id ON Groups.id = Tiers.id INNER JOIN Packages ON Credit.package_id = Packages.id INNER JOIN Districts 
			ON Tiers.district_id = Districts.id 
			
	WHERE Credit.written_off = 0 and (SELECT MAX(day_late) FROM (SELECT contract_id, number, expected_date, interest_repayment, capital_repayment, paid_interest, paid_capital,
				capital_repayment - paid_capital + interest_repayment - paid_interest AS due_amount, CASE WHEN capital_repayment - paid_capital + 
				interest_repayment - paid_interest < 0.02 THEN 0 ELSE DATEDIFF(day, expected_date, @endDate) END AS day_late FROM Installments) AS 
				ReportInstallmentsView WHERE (contract_id = Credit.id)) > 0)
END
'
GO
/*************************/
/****** Repayments *******/
/*************************/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[Repayments_sqlQuery]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE Repayments_sqlQuery
@beginDate datetime,
@endDate datetime
AS
BEGIN
SELECT contract_code,
             district,
             loan_officer_name,
			 client_name,product,
             SUM(internal_interest) AS internal_interest,
             SUM(internal_principal) AS internal_principal,
             SUM(internal_fees) AS internal_fees,
             (CASE WHEN SUM(Test.external_interest) < 0 THEN NULL ELSE SUM(Test.external_interest) END)  AS external_interest,
             (CASE WHEN SUM(Test.external_principal) < 0 THEN NULL ELSE SUM(Test.external_principal) END) AS external_principal,
             (CASE WHEN SUM(Test.external_fees) < 0 THEN NULL ELSE SUM(Test.external_fees) END) AS external_fees

             FROM (SELECT     dbo.Contracts.contract_code, dbo.Districts.name AS district, Packages.name AS product, dbo.Users.first_name + '' '' + dbo.Users.last_name AS loan_officer_name, dbo.ContractEvents.event_date, 
                      ISNULL(dbo.Groups.name, dbo.Persons.first_name + '' '' + dbo.Persons.last_name) AS client_name, dbo.RepaymentEvents.interests AS internal_interest, 
                      dbo.RepaymentEvents.principal AS internal_principal, dbo.RepaymentEvents.fees AS internal_fees, (CASE WHEN
                          (SELECT     stringValue
                            FROM          GeneralParameters
                            WHERE      name = ''EXTERNAL_CURRENCY'') IS NULL THEN - 1 ELSE CASE WHEN
                          (SELECT     exchange_rate
                            FROM          Exchange_rate
                            WHERE      exchange_date = ContractEvents.event_date) IS NULL THEN - 1000000 ELSE
                          (SELECT     exchange_rate
                            FROM          Exchange_rate
                            WHERE      exchange_date = ContractEvents.event_date) * RepaymentEvents.principal END END) AS external_principal, (CASE WHEN
                          (SELECT     stringValue
                            FROM          GeneralParameters
                            WHERE      name = ''EXTERNAL_CURRENCY'') IS NULL THEN - 1 ELSE CASE WHEN
                          (SELECT     exchange_rate
                            FROM          Exchange_rate
                            WHERE      exchange_date = ContractEvents.event_date) IS NULL THEN - 1000000 ELSE
                          (SELECT     exchange_rate
                            FROM          Exchange_rate
                            WHERE      exchange_date = ContractEvents.event_date) * RepaymentEvents.interests END END) AS external_interest, (CASE WHEN
                          (SELECT     stringValue
                            FROM          GeneralParameters
                            WHERE      name = ''EXTERNAL_CURRENCY'') IS NULL THEN - 1 ELSE CASE WHEN
                          (SELECT     exchange_rate
                            FROM          Exchange_rate
                            WHERE      exchange_date = ContractEvents.event_date) IS NULL THEN - 1000000 ELSE
                          (SELECT     exchange_rate
                            FROM          Exchange_rate
                            WHERE      exchange_date = ContractEvents.event_date) * RepaymentEvents.fees END END) AS external_fees
                    FROM         dbo.Districts INNER JOIN
                      dbo.Contracts INNER JOIN
                      dbo.Users INNER JOIN
                      dbo.Credit ON dbo.Users.id = dbo.Credit.loanofficer_id ON dbo.Contracts.id = dbo.Credit.id INNER JOIN
                      dbo.Tiers ON dbo.Contracts.beneficiary_id = dbo.Tiers.id ON dbo.Districts.id = dbo.Tiers.district_id INNER JOIN
                      dbo.ContractEvents ON dbo.Contracts.id = dbo.ContractEvents.contract_id INNER JOIN
                      dbo.RepaymentEvents ON dbo.ContractEvents.id = dbo.RepaymentEvents.id INNER JOIN Packages ON Credit.package_id = Packages.id LEFT OUTER JOIN
                      dbo.Persons ON dbo.Tiers.id = dbo.Persons.id LEFT OUTER JOIN
                      dbo.Groups ON dbo.Tiers.id = dbo.Groups.id
                        WHERE     (dbo.Credit.disbursed = 1) AND (dbo.ContractEvents.is_deleted = 0)
 AND ContractEvents.event_date >= @beginDate
 AND ContractEvents.event_date <= @endDate
 ) Test GROUP BY contract_code,district,loan_officer_name,client_name,product
END
'
GO

/*************************/
/*********  PAR  *********/
/*************************/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[PAR_LoansPAR]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE PAR_LoansPAR
@endDate datetime
AS
BEGIN
SELECT * FROM (SELECT     TOP 100 PERCENT Users.first_name + '' '' + Users.last_name AS loan_officer_name, Packages.name AS product, Contracts.contract_code, 
                      Districts.name AS district_name, DomainOfApplications.name AS activity_name, ISNULL
                          ((SELECT     SUM(capital_repayment) - SUM(paid_capital) AS Expr1
                              FROM         Installments
                              WHERE     (contract_id = Credit.id)), 0) AS OLB,
 ISNULL
                          ((SELECT     SUM(capital_repayment) - SUM(paid_capital) AS Expr1
                              FROM         (SELECT contract_id, capital_repayment,  paid_capital, 
                                                 CASE WHEN capital_repayment - paid_capital + interest_repayment - paid_interest < 0.02 THEN 0 ELSE DATEDIFF(day, expected_date, @endDate) 
                                                 END AS day_late
			                              FROM Installments) ReportInstallmentsView
                              WHERE     (contract_id = Credit.id)
                              GROUP BY contract_id
                              HAVING      (MAX(day_late) > 0) AND (MAX(day_late) <= 30)), 0) AS PAR1_30,
 ISNULL
                          ((SELECT     SUM(capital_repayment) - SUM(paid_capital) AS Expr1
                              FROM         (SELECT contract_id, capital_repayment,  paid_capital, 
                                                 CASE WHEN capital_repayment - paid_capital + interest_repayment - paid_interest < 0.02 THEN 0 ELSE DATEDIFF(day, expected_date, @endDate) 
                                                 END AS day_late
			                              FROM Installments) ReportInstallmentsView
                              WHERE     (contract_id = Credit.id)
                              GROUP BY contract_id
                              HAVING      (MAX(day_late) > 30) AND (MAX(day_late) <= 60)), 0) AS PAR31_60,
 ISNULL
                          ((SELECT     SUM(capital_repayment) - SUM(paid_capital) AS Expr1
                              FROM         (SELECT contract_id, capital_repayment,  paid_capital, 
                                                 CASE WHEN capital_repayment - paid_capital + interest_repayment - paid_interest < 0.02 THEN 0 ELSE DATEDIFF(day, expected_date, @endDate) 
                                                 END AS day_late
			                              FROM Installments) ReportInstallmentsView
                              WHERE     (contract_id = Credit.id)
                              GROUP BY contract_id
                              HAVING      (MAX(day_late) > 60) AND (MAX(day_late) <= 90)), 0) AS PAR61_90,
 ISNULL
                          ((SELECT     SUM(capital_repayment) - SUM(paid_capital) AS Expr1
                              FROM         (SELECT contract_id, capital_repayment,  paid_capital, 
                                                 CASE WHEN capital_repayment - paid_capital + interest_repayment - paid_interest < 0.02 THEN 0 ELSE DATEDIFF(day, expected_date, @endDate) 
                                                 END AS day_late
			                              FROM Installments) ReportInstallmentsView
                              WHERE     (contract_id = Credit.id)
                              GROUP BY contract_id
                              HAVING      (MAX(day_late) > 90) AND (MAX(day_late) <= 180)), 0) AS PAR91_180,
 ISNULL
                          ((SELECT     SUM(capital_repayment) - SUM(paid_capital) AS Expr1
                              FROM         (SELECT contract_id, capital_repayment,  paid_capital, 
                                                 CASE WHEN capital_repayment - paid_capital + interest_repayment - paid_interest < 0.02 THEN 0 ELSE DATEDIFF(day, expected_date, @endDate) 
                                                 END AS day_late
			                              FROM Installments) ReportInstallmentsView
                              WHERE     (contract_id = Credit.id)
                              GROUP BY contract_id
                              HAVING      (MAX(day_late) > 180) AND (MAX(day_late) <= 365)), 0) AS PAR181_365,
 ISNULL
                          ((SELECT     SUM(capital_repayment) - SUM(paid_capital) AS Expr1
                              FROM         (SELECT contract_id, capital_repayment,  paid_capital, 
                                                 CASE WHEN capital_repayment - paid_capital + interest_repayment - paid_interest < 0.02 THEN 0 ELSE DATEDIFF(day, expected_date, @endDate) 
                                                 END AS day_late
			                              FROM Installments) ReportInstallmentsView
                              WHERE     (contract_id = Credit.id)
                              GROUP BY contract_id
                              HAVING      (MAX(day_late) > 365)), 0) AS PAR365,
(SELECT MAX(day_late) FROM (SELECT contract_id, capital_repayment,  paid_capital, 
                                                 CASE WHEN capital_repayment - paid_capital + interest_repayment - paid_interest < 0.02 THEN 0 ELSE DATEDIFF(day, expected_date, @endDate) 
                                                 END AS day_late
			                              FROM Installments) ReportInstallmentsView WHERE ReportInstallmentsView.contract_id = Credit.id) AS days_late
FROM         Contracts INNER JOIN
                      Credit ON Contracts.id = Credit.id INNER JOIN
                      Packages ON Credit.package_id = Packages.id INNER JOIN
                      Tiers ON Contracts.beneficiary_id = Tiers.id INNER JOIN
                      Users ON Credit.loanofficer_id = Users.id INNER JOIN
                      Persons ON Tiers.id = Persons.id INNER JOIN
                      Districts ON Tiers.district_id = Districts.id INNER JOIN
                      DomainOfApplications ON Persons.activity_id = DomainOfApplications.id
WHERE     Credit.disbursed = 1 AND Credit.written_off = 0 ) Test2
UNION ALL
SELECT * FROM (SELECT     TOP 100 PERCENT Users.first_name + '' '' + Users.last_name AS loan_officer_name, Packages.name AS product, Contracts.contract_code, 
                      Districts.name AS district_name, DomainOfApplications.name AS activity_name, ISNULL
                          ((SELECT     SUM(capital_repayment) - SUM(paid_capital) AS Expr1
                              FROM         Installments
                              WHERE     (contract_id = Credit.id)), 0) * PersonGroupBelonging.loan_share_amount / Credit.amount AS OLB, ISNULL
                          ((SELECT     SUM(capital_repayment) - SUM(paid_capital) AS Expr1
                              FROM         (SELECT contract_id, capital_repayment,  paid_capital, 
                                                 CASE WHEN capital_repayment - paid_capital + interest_repayment - paid_interest < 0.02 THEN 0 ELSE DATEDIFF(day, expected_date, @endDate) 
                                                 END AS day_late
			                              FROM Installments) ReportInstallmentsView
                              WHERE     (contract_id = Credit.id)
                              GROUP BY contract_id
                              HAVING      (MAX(day_late) > 0) AND (MAX(day_late) <= 30)), 0) * PersonGroupBelonging.loan_share_amount / Credit.amount AS PAR1_30, ISNULL
                          ((SELECT     SUM(capital_repayment) - SUM(paid_capital) AS Expr1
                              FROM         (SELECT contract_id, capital_repayment,  paid_capital, 
                                                 CASE WHEN capital_repayment - paid_capital + interest_repayment - paid_interest < 0.02 THEN 0 ELSE DATEDIFF(day, expected_date, @endDate) 
                                                 END AS day_late
			                              FROM Installments) ReportInstallmentsView
                              WHERE     (contract_id = Credit.id)
                              GROUP BY contract_id
                              HAVING      (MAX(day_late) > 30) AND (MAX(day_late) <= 60)), 0) * PersonGroupBelonging.loan_share_amount / Credit.amount AS PAR31_60, ISNULL
                          ((SELECT     SUM(capital_repayment) - SUM(paid_capital) AS Expr1
                              FROM         (SELECT contract_id, capital_repayment,  paid_capital, 
                                                 CASE WHEN capital_repayment - paid_capital + interest_repayment - paid_interest < 0.02 THEN 0 ELSE DATEDIFF(day, expected_date, @endDate) 
                                                 END AS day_late
			                              FROM Installments) ReportInstallmentsView
                              WHERE     (contract_id = Credit.id)
                              GROUP BY contract_id
                              HAVING      (MAX(day_late) > 60) AND (MAX(day_late) <= 90)), 0) * PersonGroupBelonging.loan_share_amount / Credit.amount AS PAR61_90, ISNULL
                          ((SELECT     SUM(capital_repayment) - SUM(paid_capital) AS Expr1
                              FROM         (SELECT contract_id, capital_repayment,  paid_capital, 
                                                 CASE WHEN capital_repayment - paid_capital + interest_repayment - paid_interest < 0.02 THEN 0 ELSE DATEDIFF(day, expected_date, @endDate) 
                                                 END AS day_late
			                              FROM Installments) ReportInstallmentsView
                              WHERE     (contract_id = Credit.id)
                              GROUP BY contract_id
                              HAVING      (MAX(day_late) > 90) AND (MAX(day_late) <= 180)), 0) * PersonGroupBelonging.loan_share_amount / Credit.amount AS PAR91_180, 
                      ISNULL
                          ((SELECT     SUM(capital_repayment) - SUM(paid_capital) AS Expr1
                              FROM         (SELECT contract_id, capital_repayment,  paid_capital, 
                                                 CASE WHEN capital_repayment - paid_capital + interest_repayment - paid_interest < 0.02 THEN 0 ELSE DATEDIFF(day, expected_date, @endDate) 
                                                 END AS day_late
			                              FROM Installments) ReportInstallmentsView
                              WHERE     (contract_id = Credit.id)
                              GROUP BY contract_id
                              HAVING      (MAX(day_late) > 180) AND (MAX(day_late) <= 365)), 0) * PersonGroupBelonging.loan_share_amount / Credit.amount AS PAR181_365, 
                      ISNULL
                          ((SELECT     SUM(capital_repayment) - SUM(paid_capital) AS Expr1
                              FROM         (SELECT contract_id, capital_repayment,  paid_capital, 
                                                 CASE WHEN capital_repayment - paid_capital + interest_repayment - paid_interest < 0.02 THEN 0 ELSE DATEDIFF(day, expected_date, @endDate) 
                                                 END AS day_late
			                              FROM Installments) ReportInstallmentsView
                              WHERE     (contract_id = Credit.id)
                              GROUP BY contract_id
                              HAVING      (MAX(day_late) > 365)), 0) * PersonGroupBelonging.loan_share_amount / Credit.amount AS CreditPAR365,
(SELECT MAX(day_late) FROM (SELECT contract_id, capital_repayment,  paid_capital, 
                                                 CASE WHEN capital_repayment - paid_capital + interest_repayment - paid_interest < 0.02 THEN 0 ELSE DATEDIFF(day, expected_date, @endDate) 
                                                 END AS day_late
			                              FROM Installments) ReportInstallmentsView WHERE ReportInstallmentsView.contract_id = Credit.id) AS days_late
FROM         Contracts INNER JOIN
                      Credit AS Credit ON Contracts.id = Credit.id INNER JOIN
                      Packages ON Credit.package_id = Packages.id INNER JOIN
                      Tiers ON Contracts.beneficiary_id = Tiers.id INNER JOIN
                      Users ON Credit.loanofficer_id = Users.id INNER JOIN
                      Districts ON Tiers.district_id = Districts.id INNER JOIN
                      PersonGroupBelonging ON Tiers.id = PersonGroupBelonging.group_id INNER JOIN
                      Persons ON PersonGroupBelonging.person_id = Persons.id INNER JOIN
                      DomainOfApplications ON Persons.activity_id = DomainOfApplications.id
WHERE     Credit.disbursed = 1 AND Credit.written_off = 0) Test
END
'
GO
/*************************/
/****   Disbursments  ****/
/*************************/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[Disbursments_sqlQuery]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE Disbursments_sqlQuery
@beginDate datetime,
@endDate datetime
AS
BEGIN
SELECT Contracts.contract_code, Districts.name AS district, Packages.name AS product,ContractEvents.event_date AS disburseDate, Users.first_name + '' '' + Users.last_name AS loan_officer_name, ContractEvents.event_date, 
                      ISNULL(Groups.name, Persons.first_name + '' '' + Persons.last_name) AS client_name, LoanDisbursmentEvents.amount AS internal_amount, 
                      (CASE WHEN
                          (SELECT     stringValue
                            FROM          GeneralParameters
                            WHERE      name = ''EXTERNAL_CURRENCY'') IS NULL THEN NULL ELSE CASE WHEN
                          (SELECT     exchange_rate
                            FROM          Exchange_rate
                            WHERE      exchange_date = ContractEvents.event_date) IS NULL THEN NULL ELSE
                          (SELECT     exchange_rate
                            FROM          Exchange_rate
                            WHERE      exchange_date = ContractEvents.event_date) * LoanDisbursmentEvents.amount END END) AS external_amount
                            FROM         Districts INNER JOIN
                                                      Contracts INNER JOIN
                                                      Users INNER JOIN
                                                      Credit ON Users.id = Credit.loanofficer_id ON Contracts.id = Credit.id INNER JOIN
                                                      Tiers ON Contracts.beneficiary_id = Tiers.id ON Districts.id = Tiers.district_id INNER JOIN
                                                      ContractEvents ON Contracts.id = ContractEvents.contract_id INNER JOIN
                                                      LoanDisbursmentEvents ON ContractEvents.id = LoanDisbursmentEvents.id INNER JOIN
                                                        Packages ON Credit.package_id = Packages.id LEFT OUTER JOIN
                                                      Persons ON Tiers.id = Persons.id LEFT OUTER JOIN
                                                      Groups ON Tiers.id = Groups.id
                                WHERE     (Credit.disbursed = 1) AND (ContractEvents.is_deleted = 0) AND ContractEvents.event_date >= @beginDate 
                                AND ContractEvents.event_date <= @endDate
END
'
GO
/*********************/
/****** DropOut ******/
/*********************/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[DropOut_last2months_ByLoanOfficer]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [DropOut_last2months_ByLoanOfficer]
@endDate datetime
AS
BEGIN
SELECT first_name+'' ''+last_name AS loan_officer_name ,
                        (SELECT ISNULL(COUNT(TempTable.Contract),0) FROM (SELECT DISTINCT Contract,loan_officer,close_date FROM  LoanSizeMaturityGraceDomainDistrict_FullyRepaid) TempTable,
                              Contracts WHERE TempTable.Contract = Contracts.contract_code 
                              AND TempTable.loan_officer = Users.first_name AND TempTable.close_date >= DATEADD(month,-1,@endDate) 
                              AND TempTable.close_date <= @endDate) AS nb_of_contracts_last_month,
                        (SELECT ISNULL(COUNT(TempTable.Contract),0) FROM (SELECT DISTINCT Contract,loan_officer,close_date FROM  LoanSizeMaturityGraceDomainDistrict_FullyRepaid) TempTable,
                              Contracts WHERE TempTable.Contract = Contracts.contract_code 
                              AND TempTable.loan_officer = Users.first_name AND TempTable.close_date >= DATEADD(month,-2,@endDate) 
                              AND TempTable.close_date <= @endDate) AS nb_of_contracts_last_2months,
                        (SELECT ISNULL(COUNT(TempTable.Contract),0) FROM (
		                        SELECT DISTINCT Contract,loan_officer,close_date, active FROM  LoanSizeMaturityGraceDomainDistrict_FullyRepaid) 
		                        TempTable,Contracts WHERE TempTable.Contract = Contracts.contract_code AND 
		                        TempTable.loan_officer = Users.first_name AND TempTable.close_date >= DATEADD(month,-1,@endDate) and TempTable.active=1 
		                        AND TempTable.close_date <= @endDate) AS nb_of_contracts1_active,
                        (SELECT ISNULL(COUNT(TempTable.Contract),0) FROM (
		                        SELECT DISTINCT Contract,loan_officer,close_date, active FROM  LoanSizeMaturityGraceDomainDistrict_FullyRepaid) 
		                        TempTable,Contracts WHERE TempTable.Contract = Contracts.contract_code AND 
		                        TempTable.loan_officer = Users.first_name AND TempTable.close_date >= DATEADD(month,-2,@endDate) and TempTable.active=1 
		                        AND TempTable.close_date <= @endDate) AS nb_of_contracts2_active,
                        (SELECT ISNULL(COUNT(Contract),0) FROM (SELECT DISTINCT Contract,loan_officer,close_date,active FROM  LoanSizeMaturityGraceDomainDistrict_FullyRepaid) TempTable 
                              WHERE TempTable.loan_officer = Users.first_name 
                              AND TempTable.active = 1 AND TempTable.close_date >= DATEADD(month,-1,@endDate) 
                              AND TempTable.close_date <= @endDate) As nb_of_clients_always_active_last_month,
                        (SELECT ISNULL(COUNT(Contract),0) FROM (SELECT DISTINCT Contract,loan_officer,close_date,active FROM  LoanSizeMaturityGraceDomainDistrict_FullyRepaid) TempTable 
                              WHERE TempTable.loan_officer = Users.first_name 
                              AND TempTable.active = 1 AND TempTable.close_date >= DATEADD(month,-2,@endDate) 
                              AND TempTable.close_date <= @endDate) As nb_of_clients_always_active_last_2months
                        FROM Users WHERE deleted = 0 AND role_code <> ''ADMIN''
END
'
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[DropOut_last2months_ByProduct]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE DropOut_last2months_ByProduct
@endDate datetime
AS
BEGIN
SELECT name ,
                        (SELECT ISNULL(COUNT(TempTable.Contract),0) FROM (SELECT DISTINCT Contract,Product,close_date FROM  LoanSizeMaturityGraceDomainDistrict_FullyRepaid) TempTable,
                              Contracts WHERE TempTable.Contract = Contracts.contract_code 
                              AND TempTable.Product = Packages.name AND TempTable.close_date >= DATEADD(month,-1,@endDate) 
                              AND TempTable.close_date <= @endDate) AS nb_of_contracts_last_month,
                        (SELECT ISNULL(COUNT(TempTable.Contract),0) FROM (SELECT DISTINCT Contract,Product,close_date FROM  LoanSizeMaturityGraceDomainDistrict_FullyRepaid) TempTable,
                              Contracts WHERE TempTable.Contract = Contracts.contract_code 
                              AND TempTable.Product = Packages.name AND TempTable.close_date >= DATEADD(month,-2,@endDate) 
                              AND TempTable.close_date <= @endDate) AS nb_of_contracts_last_2months,
                        (SELECT ISNULL(COUNT(TempTable.Contract),0) FROM (
		                        SELECT DISTINCT Contract,Product,close_date, active FROM  LoanSizeMaturityGraceDomainDistrict_FullyRepaid) 
		                        TempTable,Contracts WHERE TempTable.Contract = Contracts.contract_code AND 
		                        TempTable.Product = Packages.name AND TempTable.close_date >= DATEADD(month,-1,@endDate) and TempTable.active=1 
		                        AND TempTable.close_date <= @endDate) AS nb_of_contracts1_active,
                        (SELECT ISNULL(COUNT(TempTable.Contract),0) FROM (
		                        SELECT DISTINCT Contract,Product,close_date, active FROM  LoanSizeMaturityGraceDomainDistrict_FullyRepaid) 
		                        TempTable,Contracts WHERE TempTable.Contract = Contracts.contract_code AND 
		                        TempTable.Product = Packages.name AND TempTable.close_date >= DATEADD(month,-2,@endDate) and TempTable.active=1 
		                        AND TempTable.close_date <= @endDate) AS nb_of_contracts2_active,
                        (SELECT ISNULL(COUNT(Contract),0) FROM (SELECT DISTINCT Contract,Product,close_date,active FROM  LoanSizeMaturityGraceDomainDistrict_FullyRepaid) TempTable 
                              WHERE TempTable.Product = Packages.name 
                              AND TempTable.active = 1 AND TempTable.close_date >= DATEADD(month,-1,@endDate) 
                              AND TempTable.close_date <= @endDate) As nb_of_clients_always_active_last_month,
                        (SELECT ISNULL(COUNT(Contract),0) FROM (SELECT DISTINCT Contract,Product,close_date,active FROM  LoanSizeMaturityGraceDomainDistrict_FullyRepaid) TempTable 
                              WHERE TempTable.Product = Packages.name 
                              AND TempTable.active = 1 AND TempTable.close_date >= DATEADD(month,-2,@endDate) 
                              AND TempTable.close_date <= @endDate) As nb_of_clients_always_active_last_2months
                        FROM Packages WHERE deleted = 0
END
'
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[DropOut_last2months_ByDistrict]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE DropOut_last2months_ByDistrict
@endDate datetime
AS
BEGIN
SELECT name,
                    (SELECT ISNULL(COUNT(TempTable.Contract),0) FROM (SELECT DISTINCT Contract,District,close_date FROM  LoanSizeMaturityGraceDomainDistrict_FullyRepaid) TempTable,
                          Contracts WHERE TempTable.Contract = Contracts.contract_code 
                          AND TempTable.District = Districts.name AND TempTable.close_date >= DATEADD(month,-1,@endDate) 
                          AND TempTable.close_date <= @endDate) AS nb_of_contracts_last_month,
                    (SELECT ISNULL(COUNT(TempTable.Contract),0) FROM (SELECT DISTINCT Contract,District,close_date FROM  LoanSizeMaturityGraceDomainDistrict_FullyRepaid) TempTable,
                          Contracts WHERE TempTable.Contract = Contracts.contract_code 
                          AND TempTable.District = Districts.name AND TempTable.close_date >= DATEADD(month,-2,@endDate) 
                          AND TempTable.close_date <= @endDate) AS nb_of_contracts_last_2months,
                    (SELECT ISNULL(COUNT(TempTable.Contract),0) FROM (
		                    SELECT DISTINCT Contract,District,close_date, active FROM  LoanSizeMaturityGraceDomainDistrict_FullyRepaid) 
		                    TempTable,Contracts WHERE TempTable.Contract = Contracts.contract_code AND 
		                    TempTable.District = Districts.name AND TempTable.close_date >= DATEADD(month,-1,@endDate) and TempTable.active=1 
		                    AND TempTable.close_date <= @endDate) AS nb_of_contracts1_active,
                    (SELECT ISNULL(COUNT(TempTable.Contract),0) FROM (
		                    SELECT DISTINCT Contract,District,close_date, active FROM  LoanSizeMaturityGraceDomainDistrict_FullyRepaid) 
		                    TempTable,Contracts WHERE TempTable.Contract = Contracts.contract_code AND 
		                    TempTable.District = Districts.name AND TempTable.close_date >= DATEADD(month,-2,@endDate) and TempTable.active=1 
		                    AND TempTable.close_date <= @endDate) AS nb_of_contracts2_active,
                    (SELECT ISNULL(COUNT(TempTable.Contract),0) FROM (SELECT DISTINCT Contract,District,close_date,active FROM  LoanSizeMaturityGraceDomainDistrict_FullyRepaid) TempTable 
                          WHERE TempTable.District = Districts.name 
                          AND TempTable.active = 1 AND TempTable.close_date >= DATEADD(month,-1,@endDate) 
                          AND TempTable.close_date <= @endDate) As nb_of_clients_always_active_last_month,
                    (SELECT ISNULL(COUNT(TempTable.Contract),0) FROM (SELECT DISTINCT Contract,District,close_date,active FROM  LoanSizeMaturityGraceDomainDistrict_FullyRepaid) TempTable 
                          WHERE TempTable.District = Districts.name 
                          AND TempTable.active = 1 AND TempTable.close_date >= DATEADD(month,-2,@endDate) 
                          AND TempTable.close_date <= @endDate) As nb_of_clients_always_active_last_2months
                    FROM Districts
END
'
GO

/*************************/
/***  Conso_FollowUp  ****/
/*************************/
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[Conso_FollowUp_OLBAndLoansActif]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Conso_FollowUp_OLBAndLoansActif]
@beginConso int,
@endConso int,
@user nvarchar(150),
@byHeadQuarter bit,
@branchCode nvarchar(150),
@period char
AS
BEGIN
	DECLARE @sql nvarchar(1000)
	DECLARE @paramlist nvarchar(1000)

if (@byHeadQuarter = 1)
		 SELECT @sql = 'SELECT ''All'' AS branch_code,[conso_number],[OLB],[contract_code],
								[loan_officer],[product],[district],
								[amount],[grace_period],[maturity],[is_male],
								[domain_name],[loan_share_amount],year,period 
						FROM [Conso_LoanSizeMaturityGraceDomainDistrict] 
						WHERE 1 = 1 '
ELSE
	/* by branchCode*/
	BEGIN
		SELECT @sql = 'SELECT * FROM Conso_LoanSizeMaturityGraceDomainDistrict WHERE 1 = 1 '
		if @branchCode is not null
			SELECT @sql = @sql + ' AND branch_code = @branchCode' 
	END

if @beginConso is not null
 SELECT @sql = @sql + ' AND conso_number >= @beginConso'
if @endConso is not null
 SELECT @sql = @sql + ' AND conso_number <= @endConso'
if @user is not null
 SELECT @sql = @sql + ' AND loan_officer  LIKE ''%'' + @user + ''%''' 

SELECT @sql = @sql + ' AND period = @period'

	SELECT @paramlist =	   '@beginConso int,
							@endConso int,
							@user nvarchar(150),
							@byHeadQuarter bit,
							@branchCode nvarchar(150),
							@period char'

	 EXEC dbo.sp_executesql @sql, @paramlist, 
							@beginConso, 
							@endConso, 
							@user,
							@byHeadQuarter,
							@branchCode,
							@period
END


IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[Conso_FollowUp_PortFolioAtRisk]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Conso_FollowUp_PortFolioAtRisk]
@beginConso int,
@endConso int,
@user nvarchar(150),
@byHeadQuarter bit,
@branchCode nvarchar(150),
@period char
AS
BEGIN
	DECLARE @sql nvarchar(1000)
	DECLARE @paramlist nvarchar(1000)

if (@byHeadQuarter = 1)
		 SELECT @sql = 'SELECT ''All'' AS branch_code,[conso_number],
								[loan_officer],[product],[contract_code],[district],
								[domain_name],[days_late],[OLB],[PAR1_30],[PAR31_60],
								[PAR61_90],[PAR91_180],[PAR181_365],[PAR365],year,[period]
						FROM Conso_LoansPAR  
						WHERE 1 = 1 '
ELSE
	/* by branchCode*/
	BEGIN
		SELECT @sql = 'SELECT * FROM Conso_LoansPAR  WHERE 1 = 1 '
		if @branchCode is not null
			SELECT @sql = @sql + ' AND branch_code = @branchCode' 
	END

if @beginConso is not null
 SELECT @sql = @sql + ' AND conso_number >= @beginConso'
if @endConso is not null
 SELECT @sql = @sql + ' AND conso_number <= @endConso'
if @user is not null
 SELECT @sql = @sql + ' AND loan_officer  LIKE ''%'' + @user + ''%''' 

SELECT @sql = @sql + ' AND period = @period'

	SELECT @paramlist =	   '@beginConso int,
							@endConso int,
							@user nvarchar(150),
							@byHeadQuarter bit,
							@branchCode nvarchar(150),
							@period char'

	 EXEC dbo.sp_executesql @sql, @paramlist, 
							@beginConso, 
							@endConso, 
							@user,
							@byHeadQuarter,
							@branchCode,
							@period
END


IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[Conso_FollowUp_PrincipalAndInterestByProduct]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Conso_FollowUp_PrincipalAndInterestByProduct]
@beginConso int,
@endConso int,
@user nvarchar(150),
@byHeadQuarter bit,
@branchCode nvarchar(150),
@period char
AS
BEGIN
	DECLARE @sql nvarchar(4000)
	DECLARE @paramlist nvarchar(4000)

if (@byHeadQuarter = 1)
		 SELECT @sql = 'SELECT ''All'' AS branch_code, conso_number, loan_officer_name, package_name,
                            year,period,
                          (SELECT ISNULL(SUM(Conso_CreditContracts.paid_capital), 0)
                                FROM Conso_CreditContracts
                                WHERE (Conso_CreditContracts.branch_code = CreditContract.branch_code) AND 
                                      (Conso_CreditContracts.conso_number = CreditContract.conso_number) 
								AND (Conso_CreditContracts.loan_officer_id = CreditContract.loan_officer_id) AND disbursed = 1 AND written_off = 0) 
                                -
                                (SELECT ISNULL(SUM(Conso_CreditContracts.paid_capital), 0)
                                FROM Conso_CreditContracts
                                WHERE (Conso_CreditContracts.branch_code = CreditContract.branch_code) AND 
                                      (Conso_CreditContracts.conso_number = CreditContract.conso_number - 1) 
                                AND (Conso_CreditContracts.loan_officer_id = CreditContract.loan_officer_id) AND disbursed = 1 AND written_off = 0) 
                      AS amount_repaid_this_conso,
                          (SELECT     ISNULL(SUM(Conso_CreditContracts.unpaid_capital), 0)
                            FROM          Conso_CreditContracts
                            WHERE      (Conso_CreditContracts.branch_code = CreditContract.branch_code) AND 
                                                   (Conso_CreditContracts.conso_number = CreditContract.conso_number) AND (Conso_CreditContracts.loan_officer_id = CreditContract.loan_officer_id) AND disbursed = 1 AND written_off = 0) -
                          (SELECT     ISNULL(SUM(Conso_CreditContracts.unpaid_capital), 0)
                            FROM          Conso_CreditContracts
                            WHERE      (Conso_CreditContracts.branch_code = CreditContract.branch_code) AND
                        (Conso_CreditContracts.loan_officer_id = CreditContract.loan_officer_id) AND 
                                                                           (Conso_CreditContracts.conso_number = CreditContract.conso_number - 1) AND disbursed = 1 AND written_off = 0) 
                                              AS amount_due_this_conso,
                                                  (SELECT     ISNULL(SUM(Conso_CreditContracts.paid_interest), 0)
                                                    FROM          Conso_CreditContracts
                                                    WHERE      (Conso_CreditContracts.branch_code = CreditContract.branch_code) AND 
                                                                           (Conso_CreditContracts.conso_number = CreditContract.conso_number) AND 
                        (Conso_CreditContracts.loan_officer_id = CreditContract.loan_officer_id) AND disbursed = 1 AND written_off = 0) -
                                                  (SELECT     ISNULL(SUM(Conso_CreditContracts.paid_interest), 0)
                                                    FROM          Conso_CreditContracts
                                                    WHERE      (Conso_CreditContracts.branch_code = CreditContract.branch_code) AND 
                                                                           (Conso_CreditContracts.conso_number = CreditContract.conso_number - 1) 
                        AND (Conso_CreditContracts.loan_officer_id = CreditContract.loan_officer_id) AND disbursed = 1 AND written_off = 0) 
                                              AS interest_repaid_this_conso,
                                                  (SELECT     ISNULL(SUM(Conso_CreditContracts.unpaid_interest), 0)
                                                    FROM          Conso_CreditContracts
                                                    WHERE      (Conso_CreditContracts.branch_code = CreditContract.branch_code) AND 
                                                                           (Conso_CreditContracts.conso_number = CreditContract.conso_number) 
                        AND (Conso_CreditContracts.loan_officer_id = CreditContract.loan_officer_id) AND disbursed = 1 AND written_off = 0) -
                                                  (SELECT     ISNULL(SUM(Conso_CreditContracts.unpaid_interest), 0)
                                                    FROM          Conso_CreditContracts
                                                    WHERE      (Conso_CreditContracts.branch_code = CreditContract.branch_code) AND 
                                                                           (Conso_CreditContracts.conso_number = CreditContract.conso_number - 1) 
                        AND (Conso_CreditContracts.loan_officer_id = CreditContract.loan_officer_id) AND disbursed = 1 AND written_off = 0) 
                        AS interest_due_this_conso
                        FROM Conso_CreditContracts CreditContract
                        WHERE (1 = 1) '
ELSE
	/* by branchCode*/
	BEGIN
		SELECT @sql = 'SELECT branch_code, conso_number, loan_officer_name, package_name,year,period,
                          (SELECT ISNULL(SUM(Conso_CreditContracts.paid_capital), 0)
                                FROM Conso_CreditContracts
                                WHERE (Conso_CreditContracts.branch_code = CreditContract.branch_code) AND 
                                      (Conso_CreditContracts.conso_number = CreditContract.conso_number) 
								AND (Conso_CreditContracts.loan_officer_id = CreditContract.loan_officer_id) AND disbursed = 1 AND written_off = 0) 
                                -
                                (SELECT ISNULL(SUM(Conso_CreditContracts.paid_capital), 0)
                                FROM Conso_CreditContracts
                                WHERE (Conso_CreditContracts.branch_code = CreditContract.branch_code) AND 
                                      (Conso_CreditContracts.conso_number = CreditContract.conso_number - 1) 
                                AND (Conso_CreditContracts.loan_officer_id = CreditContract.loan_officer_id) AND disbursed = 1 AND written_off = 0) 
                      AS amount_repaid_this_conso,
                          (SELECT     ISNULL(SUM(Conso_CreditContracts.unpaid_capital), 0)
                            FROM          Conso_CreditContracts
                            WHERE      (Conso_CreditContracts.branch_code = CreditContract.branch_code) AND 
                                                   (Conso_CreditContracts.conso_number = CreditContract.conso_number) AND (Conso_CreditContracts.loan_officer_id = CreditContract.loan_officer_id) AND disbursed = 1 AND written_off = 0) -
                          (SELECT     ISNULL(SUM(Conso_CreditContracts.unpaid_capital), 0)
                            FROM          Conso_CreditContracts
                            WHERE      (Conso_CreditContracts.branch_code = CreditContract.branch_code) AND
                        (Conso_CreditContracts.loan_officer_id = CreditContract.loan_officer_id) AND 
                                                                           (Conso_CreditContracts.conso_number = CreditContract.conso_number - 1) AND disbursed = 1 AND written_off = 0) 
                                              AS amount_due_this_conso,
                                                  (SELECT     ISNULL(SUM(Conso_CreditContracts.paid_interest), 0)
                                                    FROM          Conso_CreditContracts
                                                    WHERE      (Conso_CreditContracts.branch_code = CreditContract.branch_code) AND 
                                                                           (Conso_CreditContracts.conso_number = CreditContract.conso_number) AND 
                        (Conso_CreditContracts.loan_officer_id = CreditContract.loan_officer_id) AND disbursed = 1 AND written_off = 0) -
                                                  (SELECT     ISNULL(SUM(Conso_CreditContracts.paid_interest), 0)
                                                    FROM          Conso_CreditContracts
                                                    WHERE      (Conso_CreditContracts.branch_code = CreditContract.branch_code) AND 
                                                                           (Conso_CreditContracts.conso_number = CreditContract.conso_number - 1) 
                        AND (Conso_CreditContracts.loan_officer_id = CreditContract.loan_officer_id) AND disbursed = 1 AND written_off = 0) 
                                              AS interest_repaid_this_conso,
                                                  (SELECT     ISNULL(SUM(Conso_CreditContracts.unpaid_interest), 0)
                                                    FROM          Conso_CreditContracts
                                                    WHERE      (Conso_CreditContracts.branch_code = CreditContract.branch_code) AND 
                                                                           (Conso_CreditContracts.conso_number = CreditContract.conso_number) 
                        AND (Conso_CreditContracts.loan_officer_id = CreditContract.loan_officer_id) AND disbursed = 1 AND written_off = 0) -
                                                  (SELECT     ISNULL(SUM(Conso_CreditContracts.unpaid_interest), 0)
                                                    FROM          Conso_CreditContracts
                                                    WHERE      (Conso_CreditContracts.branch_code = CreditContract.branch_code) AND 
                                                                           (Conso_CreditContracts.conso_number = CreditContract.conso_number - 1) 
                        AND (Conso_CreditContracts.loan_officer_id = CreditContract.loan_officer_id) AND disbursed = 1 AND written_off = 0) 
                        AS interest_due_this_conso
                        FROM Conso_CreditContracts CreditContract
                        WHERE (1 = 1) '
		if @branchCode is not null
			SELECT @sql = @sql + ' AND branch_code = @branchCode' 
	END

if @beginConso is not null
 SELECT @sql = @sql + ' AND conso_number >= @beginConso'
if @endConso is not null
 SELECT @sql = @sql + ' AND conso_number <= @endConso'
if @user is not null
 SELECT @sql = @sql + ' AND loan_officer  LIKE ''%'' + @user + ''%''' 

SELECT @sql = @sql + ' AND period = @period'

SELECT @sql = @sql +  '  GROUP BY branch_code, conso_number, loan_officer_name, loan_officer_id,package_name,year,period
                        ORDER BY conso_number'

	SELECT @paramlist =	   '@beginConso int,
							@endConso int,
							@user nvarchar(150),
							@byHeadQuarter bit,
							@branchCode nvarchar(150),
							@period char'

	 EXEC dbo.sp_executesql @sql, @paramlist, 
							@beginConso, 
							@endConso, 
							@user,
							@byHeadQuarter,
							@branchCode,
							@period
END
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[LoanPortfolioAnalysis_OverView]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
						
CREATE PROCEDURE [dbo].[LoanPortfolioAnalysis_OverView]
@beginConso int,
@endConso int,
@packageName nvarchar(150),
@byHeadQuarter bit,
@branchCode nvarchar(150),
@period char
AS
BEGIN
	DECLARE @sql nvarchar(4000)
	DECLARE @paramlist nvarchar(4000)

if (@byHeadQuarter = 1)
		 SELECT @sql = 'SELECT DISTINCT ''All'' AS branch_code,conso_number,
                            ((SELECT SUM(amount) FROM Conso_CreditContracts WHERE (disbursed = 1) AND (written_off = 0) AND
                            (conso_number = CreditContract.conso_number) GROUP BY conso_number)
                             - 
                            ISNULL((SELECT SUM(amount) FROM Conso_CreditContracts WHERE (disbursed = 1) AND (written_off = 0) AND 
                            (conso_number = CreditContract.conso_number - 1) GROUP BY conso_number),0)) AS amount_disbursed_this_conso,

                            ((SELECT COUNT(id) FROM Conso_CreditContracts WHERE (disbursed = 1) AND (written_off = 0) AND
                            (conso_number = CreditContract.conso_number) GROUP BY conso_number)
                             - 
                            ISNULL((SELECT COUNT(id) FROM Conso_CreditContracts WHERE (disbursed = 1)  AND (written_off = 0) 
                            AND (conso_number = CreditContract.conso_number - 1) 
                            GROUP BY conso_number),0)) AS nb_of_loans_disbursed_this_conso,

                            ISNULL((SELECT COUNT(id) FROM Conso_CreditContracts WHERE (disbursed = 1) AND (written_off = 0) 
                            AND (interest_repayment + capital_repayment - paid_capital - paid_interest > 0.02) AND 
                            (conso_number = CreditContract.conso_number) GROUP BY conso_number),0) AS nb_of_active_loans,

                            (SELECT ISNULL(SUM(Conso_CreditContracts.capital_repayment - Conso_CreditContracts.paid_capital), 0) 
                                    FROM Conso_CreditContracts INNER JOIN Conso_Details ON Conso_CreditContracts.branch_code = Conso_Details.branch_code AND  
                                    Conso_CreditContracts.conso_number = Conso_Details.conso_number WHERE 
                                    unpaid_capital = 0 AND unpaid_interest = 0 AND 
                                    (disbursed = 1) AND (written_off = 0) AND (Conso_CreditContracts.branch_code = CreditContract.branch_code) AND 
                                    (Conso_CreditContracts.conso_number = CreditContract.conso_number)) AS OLB_without_past_due,

                            (SELECT ISNULL(SUM(Conso_CreditContracts.capital_repayment - Conso_CreditContracts.paid_capital), 0) 
                            FROM Conso_CreditContracts INNER JOIN Conso_Details ON  Conso_CreditContracts.conso_number = Conso_Details.conso_number WHERE (
                            disbursed = 1) AND (written_off = 0) AND (Conso_CreditContracts.conso_number = CreditContract.conso_number)) AS OLB
                             FROM Conso_CreditContracts AS CreditContract WHERE 1 = 1 '
ELSE
	/* by branchCode*/
	BEGIN
		SELECT @sql = 'SELECT branch_code, conso_number, 
                                ((SELECT SUM(amount) FROM Conso_CreditContracts WHERE (disbursed = 1) AND (written_off = 0)AND 
                                    (branch_code = CreditContract.branch_code) AND (conso_number = CreditContract.conso_number) GROUP BY branch_code, conso_number)
                                     -
                                    ISNULL((SELECT SUM(amount) FROM Conso_CreditContracts WHERE (disbursed = 1) AND (written_off = 0) AND 
                                    (branch_code = CreditContract.branch_code) AND (conso_number = CreditContract.conso_number - 1) 
                                    GROUP BY branch_code, conso_number),0)) AS amount_disbursed_this_conso,

                                ((SELECT COUNT(id) FROM Conso_CreditContracts WHERE   (disbursed = 1) AND (written_off = 0) AND 
	                                (branch_code = CreditContract.branch_code) AND (conso_number = CreditContract.conso_number) 
                                    GROUP BY branch_code, conso_number)
                                   -
                                   ISNULL((SELECT COUNT(id) FROM Conso_CreditContracts WHERE
                                   (disbursed = 1) AND (written_off = 0) AND   (branch_code = CreditContract.branch_code) AND (conso_number = CreditContract.conso_number - 1) 
                                   GROUP BY branch_code, conso_number),0)) AS nb_of_loans_disbursed_this_conso, 

                                ISNULL((SELECT COUNT(id) FROM Conso_CreditContracts WHERE   (disbursed = 1) 
                                    AND (written_off = 0) AND (capital_repayment - paid_capital > 1) AND 
                                    (branch_code = CreditContract.branch_code) AND (conso_number = CreditContract.conso_number) 
                                    GROUP BY branch_code, conso_number),0) AS nb_of_active_loans, 

                                (SELECT ISNULL(SUM(Conso_CreditContracts.capital_repayment - Conso_CreditContracts.paid_capital), 0) 
                                    FROM Conso_CreditContracts INNER JOIN Conso_Details ON Conso_CreditContracts.branch_code = Conso_Details.branch_code AND  
                                    Conso_CreditContracts.conso_number = Conso_Details.conso_number WHERE 
                                    unpaid_capital = 0 AND unpaid_interest = 0 AND 
                                    (disbursed = 1) AND (written_off = 0) AND (Conso_CreditContracts.branch_code = CreditContract.branch_code) AND 
                                    (Conso_CreditContracts.conso_number = CreditContract.conso_number)) AS OLB_without_past_due,

                                (SELECT ISNULL(SUM(Conso_CreditContracts.capital_repayment - Conso_CreditContracts.paid_capital), 0) 
                                    FROM Conso_CreditContracts INNER JOIN   Conso_Details ON Conso_CreditContracts.branch_code = Conso_Details.branch_code AND 
                                    Conso_CreditContracts.conso_number = Conso_Details.conso_number WHERE (Conso_CreditContracts.branch_code = CreditContract.branch_code) 
                                    AND (disbursed = 1) AND (written_off = 0) AND (Conso_CreditContracts.conso_number = CreditContract.conso_number)) AS OLB
                                                         
                                FROM Conso_CreditContracts AS CreditContract WHERE 1 = 1'
		if @branchCode is not null
			SELECT @sql = @sql + ' AND branch_code = @branchCode' 
	END

if @beginConso is not null
 SELECT @sql = @sql + ' AND conso_number >= @beginConso'
if @endConso is not null
 SELECT @sql = @sql + ' AND conso_number <= @endConso'
if @packageName is not null
 SELECT @sql = @sql + ' AND package_name = @packageName' 

SELECT @sql = @sql + ' AND period = @period'

SELECT @sql = @sql + ' GROUP BY branch_code, conso_number ORDER BY conso_number'

	SELECT @paramlist =	   '@beginConso int,
							@endConso int,
							@packageName nvarchar(150),
							@byHeadQuarter bit,
							@branchCode nvarchar(150),
							@period char'

	 EXEC dbo.sp_executesql @sql, @paramlist, 
							@beginConso, 
							@endConso, 
							@packageName,
							@byHeadQuarter,
							@branchCode,
							@period
END


IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[LoanPortfolioAnalysis_Repayments]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[LoanPortfolioAnalysis_Repayments]
@beginConso int,
@endConso int,
@packageName nvarchar(150),
@byHeadQuarter bit,
@branchCode nvarchar(150),
@period char
AS
BEGIN
	DECLARE @sql nvarchar(4000)
	DECLARE @paramlist nvarchar(4000)

if (@byHeadQuarter = 1)
		 SELECT @sql = 'SELECT DISTINCT ''All'' AS branch_code, conso_number,
                        ((SELECT ISNULL(SUM(Conso_CreditContracts.paid_capital),0) 
                        FROM Conso_CreditContracts WHERE (Conso_CreditContracts.conso_number = CreditContract.conso_number) 
                        AND disbursed = 1 AND written_off = 0)  
                        -  
                        (SELECT ISNULL(SUM(Conso_CreditContracts.paid_capital),0)  FROM  Conso_CreditContracts WHERE 
                        (Conso_CreditContracts.conso_number = CreditContract.conso_number - 1)  AND disbursed = 1 AND written_off = 0)) 
                        AS amount_repaid_this_conso,

                        ((SELECT ISNULL(SUM(Conso_CreditContracts.unpaid_capital),0) FROM Conso_CreditContracts WHERE 
                        (Conso_CreditContracts.conso_number = CreditContract.conso_number) AND disbursed = 1 AND written_off = 0)  
                        -  
                        (SELECT ISNULL(SUM(Conso_CreditContracts.unpaid_capital),0)  FROM  Conso_CreditContracts 
                        WHERE (Conso_CreditContracts.conso_number = CreditContract.conso_number - 1) AND disbursed = 1 AND written_off = 0)) 
                        AS amount_due_this_conso, 

                        ((SELECT ISNULL(SUM(Conso_CreditContracts.paid_interest),0) FROM Conso_CreditContracts WHERE 
                        (Conso_CreditContracts.conso_number = CreditContract.conso_number) AND disbursed = 1 AND written_off = 0)  
                        -  
                        (SELECT ISNULL(SUM(Conso_CreditContracts.paid_interest),0) FROM Conso_CreditContracts WHERE  
                        (Conso_CreditContracts.conso_number = CreditContract.conso_number - 1) AND disbursed = 1 AND written_off = 0)) 
                        AS interest_repaid_this_conso, 

                        ((SELECT ISNULL(SUM(Conso_CreditContracts.unpaid_interest),0) FROM Conso_CreditContracts WHERE  
                        (Conso_CreditContracts.conso_number = CreditContract.conso_number) AND disbursed = 1 AND written_off = 0)  
                        -  
                        (SELECT ISNULL(SUM(Conso_CreditContracts.unpaid_interest),0)  FROM  Conso_CreditContracts WHERE 
                        (Conso_CreditContracts.conso_number = CreditContract.conso_number - 1) AND disbursed = 1 AND written_off = 0)) 
                        AS interest_due_this_conso 

                        FROM Conso_CreditContracts AS CreditContract WHERE 1 = 1 '
ELSE
	/* by branchCode*/
	BEGIN
		SELECT @sql = 'SELECT branch_code, conso_number,
                        ((SELECT ISNULL(SUM(Conso_CreditContracts.paid_capital),0) FROM Conso_CreditContracts WHERE 
                        (Conso_CreditContracts.branch_code = CreditContract.branch_code) AND 
                        (Conso_CreditContracts.conso_number = CreditContract.conso_number) AND disbursed = 1 AND written_off = 0)  
                        -  
                        (SELECT ISNULL(SUM(Conso_CreditContracts.paid_capital),0)  FROM  Conso_CreditContracts WHERE 
                        (Conso_CreditContracts.branch_code = CreditContract.branch_code) AND 
                        (Conso_CreditContracts.conso_number = CreditContract.conso_number - 1)  AND disbursed = 1 AND written_off = 0)) 
                        AS amount_repaid_this_conso,

                        ((SELECT ISNULL(SUM(Conso_CreditContracts.unpaid_capital),0) 
                        FROM Conso_CreditContracts WHERE (Conso_CreditContracts.branch_code = CreditContract.branch_code) AND 
                        (Conso_CreditContracts.conso_number = CreditContract.conso_number) AND disbursed = 1 AND written_off = 0)  
                        -  
                        (SELECT ISNULL(SUM(Conso_CreditContracts.unpaid_capital),0)  FROM  Conso_CreditContracts WHERE 
                        (Conso_CreditContracts.branch_code = CreditContract.branch_code) AND 
                        (Conso_CreditContracts.conso_number = CreditContract.conso_number - 1) AND disbursed = 1 AND written_off = 0)) 
                        AS amount_due_this_conso, 

                        ((SELECT ISNULL(SUM(Conso_CreditContracts.paid_interest),0) FROM Conso_CreditContracts WHERE 
                        (Conso_CreditContracts.branch_code = CreditContract.branch_code) AND 
                        (Conso_CreditContracts.conso_number = CreditContract.conso_number) AND disbursed = 1 AND written_off = 0)  
                        -  
                        (SELECT ISNULL(SUM(Conso_CreditContracts.paid_interest),0) FROM Conso_CreditContracts WHERE 
                        (Conso_CreditContracts.branch_code = CreditContract.branch_code) AND 
                        (Conso_CreditContracts.conso_number = CreditContract.conso_number - 1) AND disbursed = 1 AND written_off = 0)) 
                        AS interest_repaid_this_conso, 

                        ((SELECT ISNULL(SUM(Conso_CreditContracts.unpaid_interest),0) FROM Conso_CreditContracts WHERE 
                        (Conso_CreditContracts.branch_code = CreditContract.branch_code) AND 
                        (Conso_CreditContracts.conso_number = CreditContract.conso_number) AND disbursed = 1 AND written_off = 0)  
                        -  
                        (SELECT ISNULL(SUM(Conso_CreditContracts.unpaid_interest),0)  FROM  
                        Conso_CreditContracts WHERE (Conso_CreditContracts.branch_code = CreditContract.branch_code) AND 
                        (Conso_CreditContracts.conso_number = CreditContract.conso_number - 1) AND disbursed = 1 AND written_off = 0)) 
                        AS interest_due_this_conso
                         
                        FROM Conso_CreditContracts AS CreditContract WHERE 1 = 1 '
		if @branchCode is not null
			SELECT @sql = @sql + ' AND branch_code = @branchCode' 
	END

if @beginConso is not null
 SELECT @sql = @sql + ' AND conso_number >= @beginConso'
if @endConso is not null
 SELECT @sql = @sql + ' AND conso_number <= @endConso'
if @packageName is not null
 SELECT @sql = @sql + ' AND package_name = @packageName' 

SELECT @sql = @sql + ' AND period = @period'

SELECT @sql = @sql + ' GROUP BY branch_code, conso_number ORDER BY conso_number'

	SELECT @paramlist =	   '@beginConso int,
							@endConso int,
							@packageName nvarchar(150),
							@byHeadQuarter bit,
							@branchCode nvarchar(150),
							@period char'

	 EXEC dbo.sp_executesql @sql, @paramlist, 
							@beginConso, 
							@endConso, 
							@packageName,
							@byHeadQuarter,
							@branchCode,
							@period
END


IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[LoanPortfolioAnalysis_LateLoansAndPrincipal]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[LoanPortfolioAnalysis_LateLoansAndPrincipal]
@beginConso int,
@endConso int,
@packageName nvarchar(150),
@byHeadQuarter bit,
@branchCode nvarchar(150),
@period char
AS
BEGIN
	DECLARE @sql nvarchar(4000)
	DECLARE @paramlist nvarchar(4000)

if (@byHeadQuarter = 1)
		 SELECT @sql = 'SELECT DISTINCT ''All'' AS branch_code, conso_number,
                            (SELECT SUM(capital_repayment - paid_capital) FROM Conso_CreditContracts WHERE
                            (conso_number = CreditContract.conso_number) AND (disbursed = 1) AND (written_off = 0) GROUP BY conso_number) AS OLB, 

                            (SELECT COUNT(contract_code) FROM Conso_CreditContracts WHERE  (conso_number = CreditContract.conso_number) 
                            AND (disbursed = 1) AND (written_off = 0)) AS nber_of_contracts,

                            (SELECT COUNT( Conso_CreditContracts.contract_code) FROM 
                            Conso_CreditContracts INNER JOIN Conso_Details ON Conso_CreditContracts.conso_number = Conso_Details.conso_number WHERE
                            (DATEDIFF(day, Conso_CreditContracts.first_non_repaid_date, Conso_Details.application_date) >= 1) AND 
                            (DATEDIFF(day, Conso_CreditContracts.first_non_repaid_date, Conso_Details.application_date) <= 30) AND 
                            (Conso_CreditContracts.conso_number = CreditContract.conso_number) AND (disbursed = 1) AND (written_off = 0)) AS PAR1_30_NBR,

                            (SELECT ISNULL(SUM(capital_repayment - paid_capital),0) FROM 
                            Conso_CreditContracts INNER JOIN Conso_Details ON Conso_CreditContracts.conso_number = Conso_Details.conso_number WHERE
                            (DATEDIFF(day,  Conso_CreditContracts.first_non_repaid_date, Conso_Details.application_date) >= 1) AND
                            (DATEDIFF(day, Conso_CreditContracts.first_non_repaid_date, Conso_Details.application_date) <= 30) AND
                            (Conso_CreditContracts.conso_number = CreditContract.conso_number) AND (disbursed = 1) AND (written_off = 0)) AS PAR1_30_OLB,

                            (SELECT COUNT( Conso_CreditContracts.contract_code) FROM Conso_CreditContracts 
                            INNER JOIN Conso_Details ON Conso_CreditContracts.conso_number = Conso_Details.conso_number WHERE
                            (DATEDIFF(day, Conso_CreditContracts.first_non_repaid_date,  Conso_Details.application_date) > 30) 
                            AND (DATEDIFF(day, Conso_CreditContracts.first_non_repaid_date,  Conso_Details.application_date) <= 60) 
                            AND ( Conso_CreditContracts.conso_number = CreditContract.conso_number) AND (disbursed = 1) AND (written_off = 0)) AS PAR31_60_NBR,

                            (SELECT ISNULL(SUM(capital_repayment - paid_capital),0) FROM Conso_CreditContracts INNER JOIN Conso_Details 
                            ON Conso_CreditContracts.conso_number =  Conso_Details.conso_number WHERE 
                            (DATEDIFF(day, Conso_CreditContracts.first_non_repaid_date, Conso_Details.application_date) > 30) 
                            AND (DATEDIFF(day, Conso_CreditContracts.first_non_repaid_date, Conso_Details.application_date) <= 60) 
                            AND ( Conso_CreditContracts.conso_number = CreditContract.conso_number) AND (disbursed = 1) AND (written_off = 0)) AS PAR31_60_OLB,

                            (SELECT COUNT( Conso_CreditContracts.contract_code) FROM Conso_CreditContracts INNER JOIN 
                            Conso_Details ON Conso_CreditContracts.conso_number = Conso_Details.conso_number WHERE 
                            (DATEDIFF(day, Conso_CreditContracts.first_non_repaid_date, Conso_Details.application_date) > 60) AND
                            (DATEDIFF(day, Conso_CreditContracts.first_non_repaid_date,  Conso_Details.application_date) <= 180) 
                            AND ( Conso_CreditContracts.conso_number = CreditContract.conso_number) AND (disbursed = 1) AND (written_off = 0)) AS PAR61_180_NBR,

                            (SELECT ISNULL(SUM(capital_repayment - paid_capital),0) FROM Conso_CreditContracts INNER JOIN Conso_Details ON
                             Conso_CreditContracts.conso_number =  Conso_Details.conso_number WHERE 
                            (DATEDIFF(day, Conso_CreditContracts.first_non_repaid_date, Conso_Details.application_date) > 60) AND 
                            (DATEDIFF(day, Conso_CreditContracts.first_non_repaid_date, Conso_Details.application_date) <= 180) AND 
                            ( Conso_CreditContracts.conso_number = CreditContract.conso_number) AND (disbursed = 1) AND (written_off = 0)) AS PAR61_180_OLB,
                             
                            (SELECT COUNT( Conso_CreditContracts.contract_code) FROM Conso_CreditContracts INNER JOIN 
                            Conso_Details ON Conso_CreditContracts.conso_number =  Conso_Details.conso_number WHERE 
                            (DATEDIFF(day, Conso_CreditContracts.first_non_repaid_date, Conso_Details.application_date) > 180) 
                            AND ( Conso_CreditContracts.conso_number = CreditContract.conso_number) AND (disbursed = 1) AND (written_off = 0)) AS PAR365_NBR,

                            (SELECT ISNULL(SUM(capital_repayment - paid_capital),0) FROM Conso_CreditContracts INNER JOIN 
                            Conso_Details ON Conso_CreditContracts.conso_number = Conso_Details.conso_number WHERE 
                            (DATEDIFF(day, Conso_CreditContracts.first_non_repaid_date, Conso_Details.application_date) > 180) AND 
                            ( Conso_CreditContracts.conso_number = CreditContract.conso_number) AND (disbursed = 1) AND (written_off = 0)) AS PAR365_OLB

                            FROM Conso_CreditContracts AS CreditContract WHERE 1 = 1 '
ELSE
	/* by branchCode*/
	BEGIN
		SELECT @sql = 'SELECT branch_code, conso_number,
                            (SELECT SUM(capital_repayment - paid_capital) FROM Conso_CreditContracts WHERE 
                            (branch_code = CreditContract.branch_code) AND (conso_number = CreditContract.conso_number) AND (disbursed = 1) AND (written_off = 0) 
                            GROUP BY branch_code, conso_number) AS OLB,

                            (SELECT COUNT(contract_code) FROM Conso_CreditContracts WHERE (branch_code = CreditContract.branch_code) AND 
                            (conso_number = CreditContract.conso_number)) AS nber_of_contracts, (SELECT COUNT( Conso_CreditContracts.contract_code) 
                            FROM Conso_CreditContracts INNER JOIN Conso_Details ON Conso_CreditContracts.branch_code = Conso_Details.branch_code 
                            AND Conso_CreditContracts.conso_number = Conso_Details.conso_number WHERE 
                            (DATEDIFF(day, Conso_CreditContracts.first_non_repaid_date, Conso_Details.application_date) >= 1) AND 
                            (DATEDIFF(day, Conso_CreditContracts.first_non_repaid_date, Conso_Details.application_date) <= 30) AND 
                            ( Conso_CreditContracts.branch_code = CreditContract.branch_code) AND 
                            (Conso_CreditContracts.conso_number = CreditContract.conso_number)AND (disbursed = 1) AND (written_off = 0)) AS PAR1_30_NBR, 

                            (SELECT ISNULL(SUM(capital_repayment - paid_capital),0) FROM Conso_CreditContracts INNER JOIN Conso_Details ON 
                            Conso_CreditContracts.branch_code = Conso_Details.branch_code AND Conso_CreditContracts.conso_number = Conso_Details.conso_number 
                            WHERE (DATEDIFF(day, Conso_CreditContracts.first_non_repaid_date, Conso_Details.application_date) >= 1) AND 
                            (DATEDIFF(day, Conso_CreditContracts.first_non_repaid_date, Conso_Details.application_date) <= 30) AND 
                            ( Conso_CreditContracts.branch_code = CreditContract.branch_code) AND 
                            ( Conso_CreditContracts.conso_number = CreditContract.conso_number)AND (disbursed = 1) AND (written_off = 0)) AS PAR1_30_OLB, 

                            (SELECT COUNT(Conso_CreditContracts.contract_code) FROM Conso_CreditContracts INNER JOIN 
                            Conso_Details ON Conso_CreditContracts.branch_code = Conso_Details.branch_code AND 
                            Conso_CreditContracts.conso_number = Conso_Details.conso_number WHERE 
                            (DATEDIFF(day, Conso_CreditContracts.first_non_repaid_date, Conso_Details.application_date) > 30) AND 
                            (DATEDIFF(day, Conso_CreditContracts.first_non_repaid_date, Conso_Details.application_date) <= 60) AND 
                            ( Conso_CreditContracts.branch_code = CreditContract.branch_code) AND 
                            (Conso_CreditContracts.conso_number = CreditContract.conso_number) AND (disbursed = 1) AND (written_off = 0)) AS PAR31_60_NBR, 

                            (SELECT ISNULL(SUM(capital_repayment - paid_capital),0) FROM Conso_CreditContracts INNER JOIN 
                            Conso_Details ON Conso_CreditContracts.branch_code = Conso_Details.branch_code AND 
                            Conso_CreditContracts.conso_number =  Conso_Details.conso_number WHERE 
                            (DATEDIFF(day, Conso_CreditContracts.first_non_repaid_date, Conso_Details.application_date) > 30) AND 
                            (DATEDIFF(day, Conso_CreditContracts.first_non_repaid_date, Conso_Details.application_date) <= 60) AND 
                            ( Conso_CreditContracts.branch_code = CreditContract.branch_code) AND 
                            ( Conso_CreditContracts.conso_number = CreditContract.conso_number) AND (disbursed = 1) AND (written_off = 0)) AS PAR31_60_OLB, 

                            (SELECT COUNT( Conso_CreditContracts.contract_code) FROM Conso_CreditContracts INNER JOIN Conso_Details 
                            ON Conso_CreditContracts.branch_code = Conso_Details.branch_code AND Conso_CreditContracts.conso_number = 
                            Conso_Details.conso_number WHERE (DATEDIFF(day, Conso_CreditContracts.first_non_repaid_date, Conso_Details.application_date) > 60) 
                            AND (DATEDIFF(day, Conso_CreditContracts.first_non_repaid_date, Conso_Details.application_date) <= 180) AND 
                            ( Conso_CreditContracts.branch_code = CreditContract.branch_code) AND 
                            ( Conso_CreditContracts.conso_number = CreditContract.conso_number) AND (disbursed = 1) AND (written_off = 0)) AS PAR61_180_NBR, 

                            (SELECT ISNULL(SUM(capital_repayment - paid_capital),0) FROM Conso_CreditContracts INNER JOIN 
                            Conso_Details ON Conso_CreditContracts.branch_code = Conso_Details.branch_code AND 
                            Conso_CreditContracts.conso_number = Conso_Details.conso_number WHERE 
                            (DATEDIFF(day, Conso_CreditContracts.first_non_repaid_date, Conso_Details.application_date) > 60) AND 
                            (DATEDIFF(day, Conso_CreditContracts.first_non_repaid_date, Conso_Details.application_date) <= 180) AND 
                            ( Conso_CreditContracts.branch_code = CreditContract.branch_code) AND 
                            (Conso_CreditContracts.conso_number = CreditContract.conso_number) AND (disbursed = 1) AND (written_off = 0)) AS PAR61_180_OLB, 

                            (SELECT COUNT(Conso_CreditContracts.contract_code) FROM Conso_CreditContracts INNER JOIN Conso_Details ON 
                            Conso_CreditContracts.branch_code = Conso_Details.branch_code AND Conso_CreditContracts.conso_number = Conso_Details.conso_number 
                            WHERE (DATEDIFF(day, Conso_CreditContracts.first_non_repaid_date, Conso_Details.application_date) > 180) AND 
                            ( Conso_CreditContracts.branch_code = CreditContract.branch_code) AND 
                            (Conso_CreditContracts.conso_number = CreditContract.conso_number) AND (disbursed = 1) AND (written_off = 0)) AS PAR365_NBR, 

                            (SELECT ISNULL(SUM(capital_repayment - paid_capital),0) FROM Conso_CreditContracts INNER JOIN 
                            Conso_Details ON Conso_CreditContracts.branch_code = Conso_Details.branch_code AND 
                            Conso_CreditContracts.conso_number = Conso_Details.conso_number WHERE 
                            (DATEDIFF(day, Conso_CreditContracts.first_non_repaid_date, Conso_Details.application_date) > 180) 
                            AND ( Conso_CreditContracts.branch_code = CreditContract.branch_code) AND 
                            ( Conso_CreditContracts.conso_number = CreditContract.conso_number) AND (disbursed = 1) AND (written_off = 0)) AS PAR365_OLB 

                            FROM Conso_CreditContracts AS CreditContract WHERE 1 = 1 '
		if @branchCode is not null
			SELECT @sql = @sql + ' AND branch_code = @branchCode' 
	END

if @beginConso is not null
 SELECT @sql = @sql + ' AND conso_number >= @beginConso'
if @endConso is not null
 SELECT @sql = @sql + ' AND conso_number <= @endConso'
if @packageName is not null
 SELECT @sql = @sql + ' AND package_name = @packageName' 

SELECT @sql = @sql + ' AND period = @period'

SELECT @sql = @sql + '   GROUP BY branch_code, conso_number ORDER BY conso_number'

	SELECT @paramlist =	   '@beginConso int,
							@endConso int,
							@packageName nvarchar(150),
							@byHeadQuarter bit,
							@branchCode nvarchar(150),
							@period char'

	 EXEC dbo.sp_executesql @sql, @paramlist, 
							@beginConso, 
							@endConso, 
							@packageName,
							@byHeadQuarter,
							@branchCode,
							@period
END



IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[LoanPortfolioAnalysis_Provisioning]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[LoanPortfolioAnalysis_Provisioning]
@beginConso int,
@endConso int,
@packageName nvarchar(150),
@byHeadQuarter bit,
@branchCode nvarchar(150),
@period char
AS
BEGIN
	DECLARE @sql nvarchar(4000)
	DECLARE @paramlist nvarchar(4000)

if (@byHeadQuarter = 1)
		 SELECT @sql = 'SELECT DISTINCT ''All'' AS branch_code, conso_number, 
						(	SELECT SUM(capital_repayment - paid_capital) 
							FROM Conso_CreditContracts 
							WHERE (conso_number = CreditContract.conso_number)
				 			GROUP BY conso_number) AS OLB, 
									ISNULL( (	SELECT balance 
												FROM Conso_Accounts 
												WHERE(conso_number = CreditContract.conso_number)			
												AND account_number = 7712),0) AS loan_loss_reserved, 
												ISNULL((SELECT balance FROM Conso_Accounts 
									WHERE  (conso_number = CreditContract.conso_number) 
									AND account_number = 6731),0) AS general_reserve 
						FROM Conso_CreditContracts AS CreditContract WHERE 1 = 1 '
ELSE
	/* by branchCode*/
	BEGIN
		SELECT @sql = 'SELECT branch_code, conso_number, 
						(	SELECT SUM(capital_repayment - paid_capital) 
							FROM Conso_CreditContracts 
							WHERE (branch_code = CreditContract.branch_code) 
							AND (conso_number = CreditContract.conso_number) 			
							GROUP BY branch_code, conso_number) AS OLB, 
							ISNULL(  (	SELECT balance 
										FROM Conso_Accounts 
										WHERE (branch_code = CreditContract.branch_code) 
										AND (conso_number = CreditContract.conso_number)			
										AND account_number = 7712),0) AS loan_loss_reserved, 
										ISNULL( (	SELECT balance 
													FROM Conso_Accounts 
													WHERE (branch_code = CreditContract.branch_code) 
													AND (conso_number = CreditContract.conso_number) 
													AND account_number = 6731),0) 
													AS general_reserve 
						FROM Conso_CreditContracts AS CreditContract WHERE 1 = 1 '
		if @branchCode is not null
			SELECT @sql = @sql + ' AND branch_code = @branchCode' 
	END

if @beginConso is not null
 SELECT @sql = @sql + ' AND conso_number >= @beginConso'
if @endConso is not null
 SELECT @sql = @sql + ' AND conso_number <= @endConso'
if @packageName is not null
 SELECT @sql = @sql + ' AND package_name = @packageName' 

SELECT @sql = @sql + ' AND period = @period'

SELECT @sql = @sql + ' GROUP BY branch_code, conso_number ORDER BY conso_number'

	SELECT @paramlist =	   '@beginConso int,
							@endConso int,
							@packageName nvarchar(150),
							@byHeadQuarter bit,
							@branchCode nvarchar(150),
							@period char'

	 EXEC dbo.sp_executesql @sql, @paramlist, 
							@beginConso, 
							@endConso, 
							@packageName,
							@byHeadQuarter,
							@branchCode,
							@period
END
/*end*/
GO


