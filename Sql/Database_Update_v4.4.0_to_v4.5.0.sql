ALTER TABLE dbo.WriteOffEvents
ADD overdue_principal  [money] NULL
GO

UPDATE  dbo.WriteOffEvents
SET     overdue_principal = 0
GO

ALTER TABLE dbo.WriteOffEvents
ALTER COLUMN overdue_principal [money] NOT NULL
GO

INSERT INTO EventAttributes (event_type, name) VALUES('WROE', 'overdue_principal')
GO

ALTER TABLE dbo.LoanAccountingMovements
ADD fiscal_year_id INT 
GO

ALTER TABLE dbo.SavingsAccountingMovements
ADD fiscal_year_id INT 
GO

ALTER TABLE dbo.ManualAccountingMovements
ADD fiscal_year_id INT 
GO

INSERT  INTO dbo.FiscalYear
        ( name ,
          open_date ,
          close_date
        )
VALUES  ( 'New Year' ,
          '2000-01-01' ,
          NULL
        )
GO

ALTER TABLE dbo.Installments
ADD start_date DATETIME NOT NULL DEFAULT(GETDATE()),
olb MONEY NOT NULL DEFAULT(0)
GO

ALTER TABLE dbo.InstallmentHistory
ADD start_date DATETIME NOT NULL DEFAULT(GETDATE()),
olb MONEY NOT NULL DEFAULT(0)
GO

-- Update dbo.Installments.start_date values
SELECT  i1.contract_id ,
        i1.number ,
        ISNULL(i2.expected_date, c.start_date) START_DATE
INTO    #1
FROM    dbo.Installments i1
        LEFT JOIN dbo.Installments i2 ON i1.contract_id = i2.contract_id
                                         AND i1.number = i2.number + 1
        LEFT JOIN dbo.Contracts c ON c.id = i1.contract_id

UPDATE  Installments
SET     Installments.start_date = ISNULL(#1.start_date, GETDATE())
FROM    #1
WHERE   Installments.contract_id = #1.contract_id
        AND Installments.number = #1.number
DROP TABLE #1
GO 

-- Update dbo.Installments.olb values        
SELECT  i1.contract_id ,
        i1.number ,
        cr.amount - ISNULL(SUM(i2.capital_repayment), 0) olb
INTO    #1
FROM    dbo.Installments i1
        LEFT JOIN dbo.Installments i2 ON i1.contract_id = i2.contract_id
                                         AND i2.number < i1.number
        LEFT JOIN dbo.Credit cr ON cr.id = i1.contract_id
GROUP BY i1.contract_id ,
        i1.number ,
        cr.amount
		
UPDATE  Installments
SET     Installments.olb = ISNULL(#1.olb, 0)
FROM    #1
WHERE   Installments.contract_id = #1.contract_id
        AND Installments.number = #1.number
        
DROP TABLE #1
GO 

-- Update dbo.InstallmentHistory.start_date values
SELECT  i1.contract_id ,
        i1.number ,
        i1.event_id,
        ISNULL(i2.expected_date, c.start_date) START_DATE
INTO    #1
FROM    dbo.InstallmentHistory i1
        LEFT JOIN dbo.InstallmentHistory i2 ON i1.contract_id = i2.contract_id AND i1.number = i2.number + 1 AND i1.event_id = i2.event_id
        LEFT JOIN dbo.Contracts c ON c.id = i1.contract_id

UPDATE  InstallmentHistory
SET     InstallmentHistory.start_date = ISNULL(#1.start_date, GETDATE())
FROM    #1
WHERE   InstallmentHistory.contract_id = #1.contract_id
        AND InstallmentHistory.number = #1.number
        AND InstallmentHistory.event_id = #1.event_id
DROP TABLE #1
GO 

-- Update dbo.InstallmentHistory.olb values        
SELECT  i1.contract_id ,
        i1.number ,
        i1.event_id,
        cr.amount - ISNULL(SUM(i2.capital_repayment), 0) olb
INTO    #1
FROM    dbo.InstallmentHistory i1
        LEFT JOIN dbo.InstallmentHistory i2 ON i1.contract_id = i2.contract_id AND i2.number < i1.number AND i1.event_id = i2.event_id
        LEFT JOIN dbo.Credit cr ON cr.id = i1.contract_id
GROUP BY i1.contract_id ,
        i1.number ,
        cr.amount,
        i1.event_id
		
UPDATE  InstallmentHistory
SET     InstallmentHistory.olb = ISNULL(#1.olb, 0)
FROM    #1
WHERE   InstallmentHistory.contract_id = #1.contract_id
        AND InstallmentHistory.number = #1.number
        AND InstallmentHistory.event_id = #1.event_id
        
DROP TABLE #1
GO 

UPDATE  [TechnicalParameters]
SET     [value] = 'v4.5.0'
WHERE   [name] = 'VERSION'
GO
