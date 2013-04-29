SELECT  contracts.id AS contract_id, contracts.project_id, dbo.Contracts.contract_code
INTO #NotClosedOrValidatedContractIDs
FROM    Contracts
                INNER JOIN ContractEvents ON ContractEvents.contract_id = Contracts.id
                INNER JOIN dbo.RepaymentEvents ON dbo.ContractEvents.id = dbo.RepaymentEvents.id
                INNER JOIN dbo.Credit ON dbo.Credit.id = dbo.Contracts.id
        WHERE   (Contracts.closed = 0
                 OR Contracts.status <> 6
                )
                AND (ContractEvents.is_deleted = 0)
                AND dbo.ContractEvents.event_type = 'RGLE'
        GROUP BY contracts.id, contracts.project_id, amount, dbo.Contracts.contract_code         
HAVING ABS(SUM(dbo.RepaymentEvents.principal) - amount) < 0.05

UPDATE  contracts
SET     contracts.closed = 1,
        status = 6
FROM    contracts C
INNER JOIN #NotClosedOrValidatedContractIDs Found ON C.id = Found.contract_id 

UPDATE  tiers
SET     active = 0
FROM    tiers
INNER JOIN projects ON projects.tiers_id = tiers.id
INNER JOIN #NotClosedOrValidatedContractIDs ON #NotClosedOrValidatedContractIDs.project_id = projects.id

DROP TABLE #NotClosedOrValidatedContractIDs
GO

ALTER TABLE Packages
 ADD grace_period_of_latefees INT NOT NULL DEFAULT(0)
GO

ALTER TABLE Credit
 ADD grace_period_of_latefees INT NOT NULL DEFAULT(0)
GO

UPDATE Packages
SET grace_period_of_latefees = 0
GO

UPDATE Credit
SET grace_period_of_latefees = 0
GO
UPDATE dbo.Packages 
SET keep_expected_installment = 1
GO

IF NOT EXISTS (SELECT * FROM dbo.GeneralParameters WHERE [key] = 'OLB_BEFORE_REPAYMENT')
BEGIN
  INSERT INTO dbo.GeneralParameters ([key], value) VALUES ('OLB_BEFORE_REPAYMENT', 0)
END
GO
        
UPDATE [TechnicalParameters] SET [value] = 'v2.8.3'
GO
