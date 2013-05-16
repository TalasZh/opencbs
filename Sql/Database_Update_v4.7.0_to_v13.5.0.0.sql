ALTER TABLE dbo.Contracts
ADD preferred_first_installment_date DATETIME NULL
GO

UPDATE c
SET preferred_first_installment_date = i.expected_date
FROM dbo.Contracts c
INNER JOIN dbo.Installments i ON i.contract_id = c.id AND i.number = 1
GO

UPDATE  [TechnicalParameters]
SET     [value] = 'v13.5.0.0'
WHERE   [name] = 'VERSION'
GO
