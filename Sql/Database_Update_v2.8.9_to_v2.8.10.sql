UPDATE ContractEvents
SET event_type = 'LODE'
WHERE ContractEvents.id IN (SELECT ce.id
FROM ContractEvents ce 
INNER JOIN LoanDisbursmentEvents ld on ce.id = ld.id
WHERE event_type = 'ATR')
GO

UPDATE ContractEvents
SET event_type = 'WROE'
WHERE ContractEvents.id IN (SELECT ce.id
FROM ContractEvents ce 
INNER JOIN WriteOffEvents we on ce.id = we.id
WHERE event_type = 'ATR')
GO

UPDATE ContractEvents
SET event_type = 'ROLE'
WHERE ContractEvents.id IN (SELECT ce.id
FROM ContractEvents ce 
INNER JOIN ReschedulingOfALoanEvents rle on ce.id = rle.id
WHERE event_type = 'ATR')
GO

UPDATE ContractEvents
SET event_type = 'PDLE'
WHERE ContractEvents.id IN (SELECT ce.id
FROM ContractEvents ce 
INNER JOIN PastDueLoanEvents pe on ce.id = pe.id
WHERE event_type = 'ATR')
GO

INSERT INTO dbo.GeneralParameters ([key], value)
SELECT 'BAD_LOAN_DAYS', '180'
UNION ALL
SELECT 'WRITE_OFF_DAYS', '365'
GO

ALTER TABLE [LinkGuarantorCredit]
ADD [guarantee_desc] [nvarchar](100) NULL
GO

ALTER TABLE [LinkCollateralCredit]
ADD [collateral_desc] [nvarchar](100) NULL
GO

ALTER TABLE [Credit]
ADD [schedule_changed] [bit] NOT NULL DEFAULT(0) 
GO

UPDATE [Credit]
SET [schedule_changed] = 0
GO

ALTER TABLE [Packages]
ADD [use_guarantor_collateral] [bit] NOT NULL DEFAULT ((0))
GO

ALTER TABLE [Packages]
ADD [set_separate_guarantor_collateral] [bit] NOT NULL DEFAULT ((0))
GO

ALTER TABLE [Packages]
ADD [percentage_total_guarantor_collateral] [int] NOT NULL DEFAULT ((0))
GO

ALTER TABLE [Packages]
ADD [percentage_separate_guarantor] [int] NOT NULL DEFAULT ((0))
GO

ALTER TABLE [Packages]
ADD [percentage_separate_collateral] [int] NOT NULL DEFAULT ((0))
GO

ALTER TABLE [LoanShareAmounts]
ADD [event_id] [int] NULL
GO

ALTER TABLE [LoanShareAmounts]
ADD [payment_date] [datetime] NULL
GO

UPDATE Contracts
SET 
Contracts.status = 7, Contracts.closed = 0
FROM Contracts AS Cont
INNER JOIN Credit AS Cr ON Cr.id = Cont.id
WHERE Cr.written_off = 1 AND Cr.bad_loan = 0
GO

UPDATE Tiers
SET Tiers.active = 0
FROM Tiers
INNER JOIN Projects ON Projects.tiers_id = Tiers.id
INNER JOIN Contracts ON Contracts.project_id = Projects.id
WHERE Contracts.status = 7 AND Tiers.active = 1
GO

UPDATE Tiers
SET Tiers.active = 1
FROM Tiers
INNER JOIN Projects ON Projects.tiers_id = Tiers.id
INNER JOIN Contracts ON Contracts.project_id = Projects.id
WHERE Contracts.closed = 0 
AND (Contracts.status = 1 OR Contracts.status = 2 OR Contracts.status = 5)
GO

UPDATE [TechnicalParameters] SET [value] = 'v2.8.10' WHERE [name] = 'VERSION'
GO
