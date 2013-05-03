ALTER TABLE dbo.ContractEvents
ADD payment_method_id INT NULL
GO

UPDATE dbo.ContractEvents
SET ContractEvents.payment_method_id = LoanDisbursmentEvents.payment_method_id
FROM LoanDisbursmentEvents
WHERE LoanDisbursmentEvents.id = ContractEvents.id
GO

UPDATE ContractEvents
SET ContractEvents.payment_method_id = RepaymentEvents.payment_method_id
FROM RepaymentEvents
WHERE RepaymentEvents.id = ContractEvents.id
GO

ALTER TABLE LoanDisbursmentEvents
DROP COLUMN payment_method_id
GO

IF  EXISTS (SELECT  name FROM dbo.sysobjects WHERE name like N'DF__Repayment__payme%')
BEGIN
  DECLARE @constraint_name NVARCHAR(MAX)
  SELECT @constraint_name = name FROM dbo.sysobjects WHERE name like N'DF__Repayment__payme%'
  
  DECLARE @delete_command NVARCHAR(MAX)
  SET @delete_command = 'ALTER TABLE [dbo].[RepaymentEvents] DROP CONSTRAINT'+SPACE(1)+@constraint_name
  EXEC(@delete_command)
END
GO

ALTER TABLE RepaymentEvents
DROP COLUMN payment_method_id
GO

DELETE FROM [dbo].[GeneralParameters]
WHERE [key]='PENDING_SAVINGS_MODE'
GO

IF NOT EXISTS (
	SELECT *
	FROM   sys.columns
	WHERE NAME = N'is_balloon' AND OBJECT_ID = OBJECT_ID(N'Packages')
)
BEGIN
	ALTER TABLE dbo.Packages
	ADD is_balloon bit NOT NULL CONSTRAINT DF_Packages_is_balloon DEFAULT 0
END
GO

UPDATE  [TechnicalParameters]
SET     [value] = 'v4.9.0'
WHERE   [name] = 'VERSION'
GO