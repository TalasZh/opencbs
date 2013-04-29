CREATE VIEW dbo.PortfolioAndPAREvolutionView
AS
SELECT     TOP 100 PERCENT branch_code, conso_number,
                          (SELECT     SUM(interest_repayment + capital_repayment - paid_interest - paid_capital)
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
                            WHERE      (DATEDIFF(day, dbo.Conso_CreditContracts.first_non_repaid_date, dbo.Conso_Details.application_date) <= 30) AND 
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
ORDER BY conso_number DESC
GO

CREATE VIEW dbo.PortfolioAndPAREvolutionByHeadQuarterView
AS
SELECT     TOP 100 PERCENT conso_number, SUM(OLB) AS OLB, SUM(nber_of_contracts) AS nber_of_contracts, SUM(PAR1_30) AS PAR1_30, SUM(PAR31_60) 
                      AS PAR31_60, SUM(PAR61_90) AS PAR61_90, SUM(PAR91_180) AS PAR91_180, SUM(PAR181_365) AS PAR181_365, SUM(PAR365) AS PAR365
FROM         dbo.PortfolioAndPAREvolutionView
GROUP BY conso_number
ORDER BY conso_number
GO

CREATE VIEW dbo.PortfolioAndPAREvolutionByHeadQuarterLast12MonthsView
AS
SELECT     TOP 12 dbo.PortfolioAndPAREvolutionByHeadQuarterView.*
FROM         dbo.PortfolioAndPAREvolutionByHeadQuarterView
ORDER BY conso_number DESC
GO

CREATE VIEW dbo.PortfolioAndPAREvolutionByBranchLast12MonthsView
AS
SELECT     dbo.PortfolioAndPAREvolutionView.*
FROM         dbo.PortfolioAndPAREvolutionView
WHERE     (conso_number >
                          (SELECT     MAX(conso_number)
                            FROM          PortfolioAndPAREvolutionView) - 12)

GO

