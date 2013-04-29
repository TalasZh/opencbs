IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[TempEvents]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1)
DROP TABLE [dbo].[TempEvents]
GO

IF OBJECT_ID ('T_Events', 'TR') IS NOT NULL
   DROP TRIGGER T_Events
GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[testView]') AND OBJECTPROPERTY(id, N'IsView') = 1)
DROP VIEW [dbo].[testView]
GO

CREATE VIEW [testView]
AS
SELECT     dbo.MovementSet.transaction_date AS MS_date, dbo.MovementSet.user_id AS MS_user_id, dbo.ContractEvents.event_type AS CE_event_type, 
                      dbo.ContractEvents.contract_id AS CE_contract_id, dbo.ContractEvents.event_date AS CE_event_date, dbo.ContractEvents.user_id AS CE_user_id, 
                      dbo.ContractEvents.is_deleted AS CE_is_deleted, dbo.RepaymentEvents.past_due_days AS RE_past_due_days, dbo.RepaymentEvents.principal AS RE_principal, 
                      dbo.RepaymentEvents.interests AS RE_interests, dbo.RepaymentEvents.fees AS RE_fees, 
                      dbo.RepaymentEvents.installment_number AS RE_installment_number
FROM         dbo.ContractEvents INNER JOIN
                      dbo.RepaymentEvents ON dbo.ContractEvents.id = dbo.RepaymentEvents.id INNER JOIN
                      dbo.MovementSet ON dbo.ContractEvents.id = dbo.MovementSet.id

GO

CREATE TRIGGER T_Events ON testView INSTEAD OF INSERT
AS
BEGIN
DECLARE @ID_EVENT INT

INSERT INTO [MovementSet]
           ([transaction_date]
           ,[user_id])
SELECT MS_date
      ,MS_user_id
FROM inserted

SET @ID_EVENT = SCOPE_IDENTITY()

INSERT INTO [ContractEvents]
           ([id]
		   ,[event_type]
           ,[contract_id]
           ,[event_date]
           ,[user_id]
           ,[is_deleted])
SELECT @ID_EVENT
      ,CE_event_type
      ,CE_contract_id
      ,CE_event_date
      ,CE_user_id
      ,CE_is_deleted
FROM inserted

INSERT INTO [RepaymentEvents]
           ([id]
           ,[past_due_days]
           ,[principal]
           ,[interests]
           ,[fees]
           ,[installment_number])
SELECT @ID_EVENT
           ,RE_past_due_days
           ,RE_principal
           ,RE_interests
           ,RE_fees
           ,RE_installment_number
FROM inserted

END
GO

CREATE TABLE [dbo].[TempEvents](
	[MS_date] [datetime] NOT NULL,
	[MS_user_id] [int] NOT NULL,
	[CE_event_type] [char](4) NOT NULL,
	[CE_contract_id] [int] NOT NULL,
	[Ce_event_date] [datetime] NOT NULL,
	[CE_user_id] [int] NOT NULL,
	[CE_is_deleted] [bit] NOT NULL,
	[RE_past_due_days] [int] NOT NULL,
	[RE_principal] [money] NOT NULL,
	[RE_interests] [money] NOT NULL,
	[RE_fees] [money] NOT NULL,
	[RE_installment_number] [int] NOT NULL
) ON [PRIMARY]
GO

INSERT INTO [TempEvents]
           ([MS_date]
           ,[MS_user_id]
           ,[CE_event_type]
           ,[CE_contract_id]
           ,[Ce_event_date]
           ,[CE_user_id]
           ,[CE_is_deleted]
           ,[RE_past_due_days]
           ,[RE_principal]
		   ,[RE_interests]
           ,[RE_fees]
           ,[RE_installment_number])
     SELECT ISNULL(Installments.paid_date,Installments.expected_date) AS MS_date,1 AS MS_user_id, 'RGLE' As CE_event_type, Installments.contract_id AS CE_contract_id, 
ISNULL(Installments.paid_date,Installments.expected_date) AS CE_event_date,
1 AS CE_user_id, 0 AS CE_is_deleted,0 AS RE_past_due_days, Installments.capital_repayment AS [RE_principal],
Installments.interest_repayment AS [RE_interests], 0 AS [RE_fees],
Installments.number AS [RE_installment_number] FROM Installments WHERE (SELECT COUNT(ContractEvents.id)  FROM dbo.ContractEvents,dbo.RepaymentEvents WHERE ContractEvents.id = RepaymentEvents.id AND 
ContractEvents.contract_id = Installments.contract_id AND RepaymentEvents.installment_number = Installments.number) = 0
AND capital_repayment + interest_repayment - paid_capital - paid_interest < 0.02 AND (SELECT COUNT(ContractEvents.id)  FROM dbo.ContractEvents,dbo.RepaymentEvents WHERE ContractEvents.id = RepaymentEvents.id AND 
ContractEvents.contract_id = Installments.contract_id AND RepaymentEvents.installment_number < Installments.number) = 0 
GO

DECLARE @MS_date datetime
DECLARE @MS_user_id int
DECLARE @CE_event_type char(4)
DECLARE @CE_contract_id int
DECLARE @Ce_event_date datetime
DECLARE @CE_user_id int
DECLARE @CE_is_deleted bit
DECLARE @RE_past_due_days int
DECLARE @RE_interests money
DECLARE @RE_principal money
DECLARE @RE_fees money
DECLARE @RE_installment_number int


DECLARE CURSOR_EVENT CURSOR FOR
SELECT * FROM TempEvents WHERE TempEvents.RE_principal <> 0 OR TempEvents.RE_interests <> 0

OPEN CURSOR_EVENT;
FETCH NEXT FROM CURSOR_EVENT INTO @MS_date, @MS_user_id, @CE_event_type, @CE_contract_id, @Ce_event_date, @CE_user_id, @CE_is_deleted, @RE_past_due_days, @RE_principal, @RE_interests ,@RE_fees ,@RE_installment_number
WHILE @@FETCH_STATUS=0
BEGIN
	INSERT INTO [testView]
           ([MS_date]
           ,[MS_user_id]
           ,[CE_event_type]
           ,[CE_contract_id]
           ,[CE_event_date]
           ,[CE_user_id]
           ,[CE_is_deleted]
           ,[RE_past_due_days]
           ,[RE_principal]
           ,[RE_interests]
           ,[RE_fees]
           ,[RE_installment_number])
     SELECT @MS_date, 
			@MS_user_id, 
			@CE_event_type, 
			@CE_contract_id, 
			@Ce_event_date, 
			@CE_user_id, 
			@CE_is_deleted, 
			@RE_past_due_days,
			@RE_principal, 
			@RE_interests,
			@RE_fees,
			@RE_installment_number

	FETCH NEXT FROM CURSOR_EVENT INTO @MS_date, @MS_user_id, @CE_event_type, @CE_contract_id, @Ce_event_date, @CE_user_id, @CE_is_deleted, @RE_past_due_days,@RE_principal, @RE_interests,@RE_fees ,@RE_installment_number
END

CLOSE		CURSOR_EVENT
DEALLOCATE	CURSOR_EVENT
GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[TempEvents]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1)
DROP TABLE [dbo].[TempEvents]
GO

IF OBJECT_ID ('T_Events', 'TR') IS NOT NULL
   DROP TRIGGER T_Events
GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[testView]') AND OBJECTPROPERTY(id, N'IsView') = 1)
DROP VIEW [dbo].[testView]
GO