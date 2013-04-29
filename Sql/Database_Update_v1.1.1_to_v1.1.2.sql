/********************/
/****** VIEWS *******/
/********************/

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
/****** Objet :  View [dbo].[OverDueView]    Date de génération du script : 08/21/2007 13:34:52 ******/
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[OverDueView]') AND OBJECTPROPERTY(id, N'IsView') = 1)
DROP VIEW [dbo].[OverDueView]
GO
/****** Objet :  View [dbo].[CurrentRepaymentRateView]    Date de génération du script : 08/21/2007 13:34:50 ******/
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[CurrentRepaymentRateView]') AND OBJECTPROPERTY(id, N'IsView') = 1)
DROP VIEW [dbo].[CurrentRepaymentRateView]
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
/****** Objet :  View [dbo].[LoansPAR]    Date de génération du script : 08/21/2007 13:34:52 ******/
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[LoansPAR]') AND OBJECTPROPERTY(id, N'IsView') = 1)
DROP VIEW [dbo].[LoansPAR]
GO
/****** Objet :  View [dbo].[PortfolioAndPAREvolutionByHeadQuarterLast12MonthsView]    Date de génération du script : 08/21/2007 13:34:53 ******/
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[PortfolioAndPAREvolutionByHeadQuarterLast12MonthsView]') AND OBJECTPROPERTY(id, N'IsView') = 1)
DROP VIEW [dbo].[PortfolioAndPAREvolutionByHeadQuarterLast12MonthsView]
GO
/****** Objet :  View [dbo].[OLBPerLoanView]    Date de génération du script : 08/21/2007 13:34:52 ******/
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[OLBPerLoanView]') AND OBJECTPROPERTY(id, N'IsView') = 1)
DROP VIEW [dbo].[OLBPerLoanView]
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
/****** Objet :  View [dbo].[ILoansPAR]    Date de génération du script : 08/21/2007 13:34:51 ******/
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[ILoansPAR]') AND OBJECTPROPERTY(id, N'IsView') = 1)
DROP VIEW [dbo].[ILoansPAR]
GO
/****** Objet :  View [dbo].[GLoansPAR]    Date de génération du script : 08/21/2007 13:34:51 ******/
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[GLoansPAR]') AND OBJECTPROPERTY(id, N'IsView') = 1)
DROP VIEW [dbo].[GLoansPAR]
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
                      dbo.Credit.nb_of_installment * { fn ROUND(ISNULL(NULLIF (dbo.InstallmentTypes.nb_of_days, 0) / 30, dbo.InstallmentTypes.nb_of_months), 0) } AS maturity, 
                      dbo.Persons.sex, dbo.DomainOfApplications.name AS domainName, dbo.PersonGroupBelonging.loan_share_amount, dbo.Contracts.start_date AS Expr1, 
                      dbo.Contracts.close_date, dbo.Credit.interest_rate, dbo.Tiers.active, dbo.Tiers.loan_cycle
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
                      dbo.Credit.grace_period, dbo.Credit.nb_of_installment, dbo.Persons.sex, dbo.DomainOfApplications.name, dbo.PersonGroupBelonging.loan_share_amount, 
                      dbo.PersonGroupBelonging.person_id, dbo.InstallmentTypes.nb_of_days, dbo.InstallmentTypes.nb_of_months, dbo.Contracts.start_date, dbo.Contracts.close_date, 
                      dbo.Credit.interest_rate, dbo.Tiers.active, dbo.Tiers.loan_cycle
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
SELECT     SUM(dbo.Installments.capital_repayment) - SUM(dbo.Installments.paid_capital) AS OLB, dbo.Contracts.contract_code AS Contract, dbo.Contracts.start_date, 
                      dbo.Users.first_name AS loan_officer, dbo.Packages.name AS Product, dbo.Districts.name AS District, dbo.Credit.amount, dbo.Credit.grace_period, 
                      dbo.Credit.nb_of_installment * { fn ROUND(ISNULL(NULLIF (dbo.InstallmentTypes.nb_of_days, 0) / 30, dbo.InstallmentTypes.nb_of_months), 0) } AS maturity, 
                      dbo.Persons.sex, dbo.DomainOfApplications.name AS domainName, dbo.Credit.amount AS loan_share_amount, dbo.Contracts.start_date AS Expr1, 
                      dbo.Contracts.close_date, dbo.Credit.interest_rate, dbo.Tiers.active, dbo.Tiers.loan_cycle
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
                      dbo.InstallmentTypes.nb_of_months, dbo.Contracts.start_date, dbo.Contracts.close_date, dbo.Credit.interest_rate, dbo.Tiers.active, dbo.Tiers.loan_cycle
HAVING      (SUM(dbo.Installments.capital_repayment) - SUM(dbo.Installments.paid_capital) > 0.02)
'
GO
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
                      dbo.Conso_Customers.loan_share_amount,dbo.Conso_CreditContracts.year, dbo.Conso_CreditContracts.period
FROM         dbo.Conso_CreditContracts INNER JOIN
                      dbo.Conso_Customers ON dbo.Conso_CreditContracts.branch_code = dbo.Conso_Customers.branch_code AND 
                      dbo.Conso_CreditContracts.conso_number = dbo.Conso_Customers.conso_number AND 
                      dbo.Conso_CreditContracts.contract_code = dbo.Conso_Customers.contract_code
WHERE     (dbo.Conso_CreditContracts.capital_repayment - dbo.Conso_CreditContracts.paid_capital > 0.02) AND (dbo.Conso_Customers.is_in_group = 1) AND 
                      (dbo.Conso_CreditContracts.written_off = 0) AND (dbo.Conso_CreditContracts.disbursed = 1)
ORDER BY dbo.Conso_Customers.branch_code,dbo.Conso_CreditContracts.year, dbo.Conso_Customers.conso_number, dbo.Conso_CreditContracts.contract_code
'
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
                      dbo.Conso_Customers.loan_share_amount, dbo.Conso_CreditContracts.year, dbo.Conso_CreditContracts.period
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
/****** Objet :  View [dbo].[ILoansPAR]    Date de génération du script : 08/21/2007 13:34:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[ILoansPAR]') AND OBJECTPROPERTY(id, N'IsView') = 1)
EXEC dbo.sp_executesql @statement = N'CREATE VIEW [dbo].[ILoansPAR]
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
GO
/****** Objet :  View [dbo].[GLoansPAR]    Date de génération du script : 08/21/2007 13:34:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[GLoansPAR]') AND OBJECTPROPERTY(id, N'IsView') = 1)
EXEC dbo.sp_executesql @statement = N'CREATE VIEW [dbo].[GLoansPAR]
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
GO
/****** Objet :  View [dbo].[LoanSizeMaturityGraceDomainDistrict]    Date de génération du script : 08/21/2007 13:34:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[LoanSizeMaturityGraceDomainDistrict]') AND OBJECTPROPERTY(id, N'IsView') = 1)
EXEC dbo.sp_executesql @statement = N'CREATE VIEW [dbo].[LoanSizeMaturityGraceDomainDistrict]
AS
SELECT     OLB, Contract, start_date, loan_officer, Product, District, amount, grace_period, maturity, sex, domainName, loan_share_amount, start_date AS Expr1, close_date, 
                      interest_rate, active, loan_cycle
FROM         dbo.GLoanSizeMaturityGraceDomainDistrict
UNION ALL
SELECT     OLB, Contract, start_date, loan_officer, Product, District, amount, grace_period, maturity, sex, domainName, loan_share_amount, start_date AS Expr1, close_date, 
                      interest_rate, active, loan_cycle
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
SELECT     branch_code, conso_number, OLB, contract_code, loan_officer, product, district, amount, grace_period, maturity, is_male, domain_name, loan_share_amount, year, period
FROM         dbo.Conso_GLoanSizeMaturityGraceDomainDistrict
UNION ALL
SELECT     branch_code, conso_number, OLB, contract_code, loan_officer, product, district, amount, grace_period, nb_of_installments, is_male, domain_name, 
                      loan_share_amount, year, period
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
/****** Objet :  View [dbo].[LoansPAR]    Date de génération du script : 08/21/2007 13:34:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[LoansPAR]') AND OBJECTPROPERTY(id, N'IsView') = 1)
EXEC dbo.sp_executesql @statement = N'CREATE VIEW [dbo].[LoansPAR]
AS
SELECT     loan_officer_name, product, contract_code, start_date, district_name, activity_name, OLB, PAR1_30, PAR31_60, PAR61_90, PAR91_180, PAR181_365, PAR365, 
                      days_late
FROM         dbo.ILoansPAR
UNION ALL
SELECT     loan_officer_name, product, contract_code, start_date, district_name, activity_name, OLB, PAR1_30, PAR31_60, PAR61_90, PAR91_180, PAR181_365, CreditPAR365, 
                      days_late
FROM         dbo.GLoansPAR
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

/************** VERSION ***********************/

UPDATE [TechnicalParameters] SET [value]='v1.1.2'
GO
