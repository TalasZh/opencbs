CREATE TABLE [dbo].[LoanShareAmounts](
	[person_id] [int] NOT NULL,
	[group_id] [int] NOT NULL,
	[contract_id] [int] NOT NULL,
	[amount] [money] NOT NULL
) ON [PRIMARY]
GO

CREATE table #tempTableForFL
(
	[amount] [money] NOT NULL,
	[fundingline_id] [int] NOT NULL
)
GO
INSERT INTO #tempTableForFL
SELECT sum(CASE WHEN direction = 1 THEN amount WHEN direction = 2 THEN amount * (-1) end )  AS amount, fundingline_id AS fundingline_id 
FROM FundingLineEvents 
where FundingLineEvents.deleted = 0 AND code NOT LIKE 'DE%' AND  code NOT LIKE 'RE%' and code NOT LIKE 'Com%'
AND creation_date<=GETDATE()
GROUP BY fundingline_id
HAVING sum(amount)>0
GO

delete from  FundingLineEvents where FundingLineEvents.deleted = 0 AND (code LIKE 'DE%' OR  code LIKE 'RE%')
GO

INSERT INTO [dbo].[FundingLineEvents]
           ([type]
           ,[direction]
           ,[fundingline_id]
           ,[deleted]
		   ,[code]
           ,[creation_date]
           ,[amount])
 
 
SELECT		2, 
			1, 
			isnull(Credit.fundingline_id,1) ,
			ContractEvents.is_deleted, 
			isnull('RE' + CAST(Credit.id AS VARCHAR) + CAST(RepaymentEvents.installment_number AS VARCHAR),'no code'), 
			ContractEvents.event_date, 
			isnull(RepaymentEvents.principal,0)

FROM RepaymentEvents RIGHT JOIN ContractEvents ON ContractEvents.id = RepaymentEvents.id 
					LEFT JOIN Credit on ContractEvents.contract_id = Credit.id
WHERE ContractEvents.event_type = 'RGLE' and RepaymentEvents.principal<>0 
GO

INSERT INTO [dbo].[FundingLineEvents]
           ([type]
           ,[direction]
           ,[fundingline_id]
           ,[deleted]
		   ,[code]
           ,[creation_date]
           ,[amount])
SELECT		5, 
			2, 
			isnull(Credit.fundingline_id,1) ,
			ContractEvents.is_deleted, 
			isnull('COMMITMENT' + '/' + Contracts.contract_code,'no code'), 
			ContractEvents.event_date, 
			isnull(Credit.amount,0)

FROM ContractEvents  LEFT JOIN RepaymentEvents ON ContractEvents.id = RepaymentEvents.id 
					LEFT JOIN Credit on ContractEvents.contract_id = Credit.id
					LEFT JOIN Contracts on ContractEvents.contract_id = Contracts.id
WHERE ContractEvents.event_type = 'LODE' AND ('COMMITMENT' + '/' + Contracts.contract_code) NOT IN (SELECT FundingLineEvents.code FROM FundingLineEvents)-- where deleted =0)
GO



INSERT INTO [dbo].[FundingLineEvents]
           ([type]
           ,[direction]
           ,[fundingline_id]
           ,[deleted]
		   ,[code]
           ,[creation_date]
           ,[amount])
SELECT		1, 
			2, 
			isnull(Credit.fundingline_id,1) ,
			ContractEvents.is_deleted, 
			isnull('DE' + CAST(Credit.id AS VARCHAR),'no code'), 
			ContractEvents.event_date, 
			isnull(Credit.amount,0)
	
FROM RepaymentEvents RIGHT JOIN ContractEvents ON ContractEvents.id = RepaymentEvents.id 
					LEFT JOIN Credit on ContractEvents.contract_id = Credit.id
					LEFT JOIN Contracts on ContractEvents.contract_id = Contracts.id

WHERE ContractEvents.event_type = 'LODE' 
GO

SELECT sum(amount) as amount, fundingline_id  as fundingline_id 
INTO #tempRepay 
from FundingLineEvents
WHERE code LIKE 'RE%' and deleted = 0
GROUP BY fundingline_id

INSERT INTO [dbo].[FundingLineEvents]
           ([type]
            ,[direction]          
            ,[deleted]
		    ,[code]
            ,[fundingline_id]
			,[creation_date]
           ,[amount])
SELECT		0, 
			1,
			0,  
			'AUTOMATIC_INITIAL_AMOUNT_CORRECTION', 
			FundingLineEvents.fundingline_id,
			MIN(creation_date),
			ISNULL(SUM(FundingLineEvents.amount),0)- ISNULL(#tempTableForFL.amount,0) - ISNULL(#tempRepay.amount, 0)
	
FROM FundingLineEvents LEFT JOIN #tempTableForFL
ON #tempTableForFL.fundingline_id = FundingLineEvents.fundingline_id
LEFT JOIN #tempRepay on FundingLineEvents.fundingline_id = #tempRepay.fundingline_id
WHERE FundingLineEvents.type = 5 and FundingLineEvents.deleted = 0
GROUP BY FundingLineEvents.fundingline_id, #tempTableForFL.amount, #tempRepay.amount
HAVING (ISNULL(SUM(FundingLineEvents.amount),0)- ISNULL(#tempTableForFL.amount,0) - ISNULL(#tempRepay.amount, 0))>0

drop table #tempTableForFL
GO
drop table #tempRepay
GO

CREATE TABLE StandardBookings(
Id INT IDENTITY(1,1),
[Name] VARCHAR(128),
debit_account_id INT NOT NULL,
credit_account_id INT NOT NULL
)
GO

ALTER TABLE Events
ADD [description] NVARCHAR(512) NULL DEFAULT ''
GO

ALTER TABLE dbo.[StandardBookings] ADD CONSTRAINT
	[inx_uniq_StandardBooking] UNIQUE NONCLUSTERED 
	(
		[Name],
	    [debit_account_id],
	    [credit_account_id]
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO

ALTER TABLE dbo.Credit
ADD interest MONEY NOT NULL DEFAULT 0
GO

ALTER TABLE dbo.LoanDisbursmentEvents
ADD interest MONEY NOT NULL DEFAULT 0
GO

ALTER TABLE dbo.ReschedulingOfALoanEvents
ADD interest MONEY NOT NULL DEFAULT 0
GO

DECLARE @event_id INT
DECLARE @contract_id INT

DECLARE event_cursor CURSOR FOR
SELECT lde.id, ce.contract_id
FROM LoanDisbursmentEvents AS lde
LEFT JOIN ContractEvents AS ce ON lde.id = ce.id

DECLARE @interest MONEY
OPEN event_cursor
FETCH NEXT FROM event_cursor INTO @event_id, @contract_id
WHILE 0 = @@FETCH_STATUS
BEGIN
	SELECT @interest = SUM(i.interest_repayment)
	FROM Installments AS i
	WHERE contract_id = @contract_id
	
	UPDATE LoanDisbursmentEvents SET interest = @interest WHERE id = @event_id
	UPDATE Credit SET interest = @interest WHERE id = @contract_id
	
	FETCH NEXT FROM event_cursor INTO @event_id, @contract_id
END
CLOSE event_cursor
DEALLOCATE event_cursor
GO

UPDATE  [TechnicalParameters] SET [value] = 'v2.5.9'
GO