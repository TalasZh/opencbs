UPDATE dbo.ContractEvents
SET event_type = 'ATR'
WHERE ContractEvents.id IN 
   (SELECT ce.id
    FROM ContractEvents ce
    INNER JOIN RepaymentEvents re ON ce.id = re.id
    WHERE repayment_type = 'TotalPayment'
      AND (event_type = 'RGLE' OR event_type = 'RBLE')
      AND ce.id IN (SELECT ContractEvents.id
                    FROM ContractEvents
                    INNER JOIN RepaymentEvents 
                      ON ContractEvents.id = RepaymentEvents.id 
                    INNER JOIN (SELECT 
                                  c.id as con_id, 
                                  MAX(installment_number) as installment_number
                                FROM ContractEvents ce
                                INNER JOIN RepaymentEvents re ON ce.id = re.id
                                INNER JOIN Contracts c ON ce.contract_id = c.id 
                                WHERE repayment_type = 'TotalPayment'
                                GROUP BY  c.id) AS a 
                                ON a.con_id = ContractEvents.contract_id 
                                  AND RepaymentEvents.installment_number = a.installment_number))
GO

UPDATE dbo.ContractEvents
SET event_type = 'APR'
WHERE repayment_type = 'PartialPayment'
AND event_type = 'RGLE'
GO

ALTER TABLE dbo.ContractEvents
DROP COLUMN repayment_type
GO

ALTER TABLE dbo.AccountingRules
ADD [booking_direction] [smallint] NOT NULL DEFAULT(3)
GO

ALTER TABLE dbo.RepaymentEvents
	DROP CONSTRAINT PK_RepaymentEvents
GO

DROP TABLE LoanRepaymentEvents
GO

INSERT INTO [ProvisioningRules]([id], [number_of_days_min], [number_of_days_max], [provisioning_value]) VALUES(8, -1, -1, 0.1)
GO

ALTER TABLE ReschedulingOfALoanEvents
ADD grace_period INT NOT NULL DEFAULT(0)
, charge_interest_during_shift BIT NOT NULL DEFAULT(0)
, charge_interest_during_grace_period BIT NOT NULL DEFAULT(0)
GO

UPDATE Banks 
SET customIBAN1 = 0
WHERE customIBAN1 IS NULL AND IBAN1 IS NOT NULL
GO

UPDATE Banks 
SET customIBAN2 = 0
WHERE customIBAN2 IS NULL AND IBAN2 IS NOT NULL
GO

UPDATE [TechnicalParameters] SET [value] = 'v2.8.8' WHERE [name] = 'VERSION'
GO
