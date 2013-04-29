IF EXISTS(SELECT OBJECT_NAME(object_id) FROM sys.default_constraints WHERE name like '%DF__Packages__entry%')

DECLARE @table_name nvarchar(256)
DECLARE @Command NVARCHAR(max) = ''

SET @table_name = N'Packages'
SELECT @Command = @Command + 'ALTER TABLE ' + @table_name + ' drop constraint ' + d.name
FROM sys.tables t
JOIN sys.default_constraints d
ON d.parent_object_id = t.object_id
JOIN sys.columns c
ON c.object_id = t.object_id
AND c.column_id = d.parent_column_id
WHERE t.name = @table_name
AND c.name = 'entry_fees_percentage'
EXECUTE (@Command)
GO

IF EXISTS(SELECT OBJECT_NAME(object_id) FROM sys.default_constraints WHERE name like '%DF__Credit__entry_fe%')

DECLARE @table_name nvarchar(256)
DECLARE @Command NVARCHAR(max) = ''

SET @table_name = N'Credit'
SELECT @Command = @Command + 'ALTER TABLE ' + @table_name + ' drop constraint ' + d.name
FROM sys.tables t
JOIN sys.default_constraints d
ON d.parent_object_id = t.object_id
JOIN sys.columns c
ON c.object_id = t.object_id
AND c.column_id = d.parent_column_id
WHERE t.name = @table_name
AND c.name = 'entry_fees_percentage'
EXECUTE (@Command)
GO

ALTER TABLE dbo.Packages
DROP COLUMN entry_fees_percentage
GO

ALTER TABLE dbo.Credit
DROP COLUMN entry_fees_percentage
GO

ALTER TABLE dbo.Packages
DROP COLUMN entry_fees
GO
ALTER TABLE dbo.Packages
DROP COLUMN entry_fees_min
GO

ALTER TABLE dbo.Packages
DROP COLUMN entry_fees_max
GO

ALTER TABLE dbo.Credit
DROP COLUMN entry_fees
GO

ALTER TABLE ChartOfAccounts
DROP COLUMN local_account_number
GO

ALTER TABLE dbo.Installments
ALTER COLUMN [payment_method] [int]
GO

ALTER TABLE dbo.InstallmentHistory
ALTER COLUMN [payment_method] [int]
GO

ALTER TABLE dbo.ContractEvents
ADD parent_id INT NULL
GO

DECLARE @num INT
DECLARE @parent_id INT
DECLARE @id_new INT
DECLARE @i INT
DECLARE @id INT
DECLARE @event_type NVARCHAR(4)
DECLARE @contract_id INT
DECLARE @event_date DATETIME
DECLARE @user_id INT
DECLARE @is_deleted BIT
DECLARE @entry_date DATETIME
DECLARE @is_exported BIT
DECLARE @comment NVARCHAR(255)
DECLARE @teller_id INT
DECLARE @past_due_days INT
DECLARE @principal MONEY
DECLARE @interests MONEY
DECLARE @installment_number INT
DECLARE @commissions MONEY
DECLARE @penalties MONEY

-- Make sure there are no repayment events with the same id and installment number
DECLARE re_cursor CURSOR FOR
SELECT id, past_due_days, principal, interests, installment_number, commissions, penalties 
FROM
(
SELECT RepaymentEvents.id
, MIN(past_due_days) past_due_days
, SUM(principal) principal
, SUM(interests) interests
, installment_number
, SUM(commissions) commissions
, SUM(penalties) penalties
, COUNT(RepaymentEvents.id) num
FROM dbo.RepaymentEvents
INNER JOIN dbo.ContractEvents ON dbo.RepaymentEvents.id = dbo.ContractEvents.id
WHERE is_deleted = 0
GROUP BY RepaymentEvents.id, installment_number
) t
WHERE t.num > 1
GROUP BY id, past_due_days, principal, interests, installment_number, commissions, penalties

OPEN re_cursor
FETCH NEXT FROM re_cursor
INTO @id, @past_due_days, @principal, @interests, @installment_number, @commissions, @penalties

WHILE 0 = @@FETCH_STATUS 
    BEGIN
        DELETE  FROM dbo.RepaymentEvents
        WHERE   id = @id
                AND installment_number = @installment_number
    
        INSERT  INTO dbo.RepaymentEvents
                ( id ,
                  past_due_days ,
                  principal ,
                  interests ,
                  voucher_number ,
                  installment_number ,
                  commissions ,
                  penalties ,
                  payment_method_id
                )
        VALUES  ( @id ,
                  @past_due_days ,
                  @principal ,
                  @interests ,
                  NULL ,
                  @installment_number ,
                  @commissions ,
                  @penalties ,
                  0
                )
    
        FETCH NEXT FROM re_cursor
    INTO @id, @past_due_days, @principal, @interests, @installment_number, @commissions, @penalties
    END

CLOSE re_cursor
DEALLOCATE re_cursor
GO

CREATE TABLE #Events
    (
      id INT ,
      old_id INT ,
      event_type NVARCHAR(5) ,
      event_type_new NVARCHAR(5) ,
      event_date DATETIME ,
      contract_id INT ,
      number INT ,
      capital MONEY ,
      new_capital MONEY
    )


DECLARE @num INT
DECLARE @parent_id INT
DECLARE @id_new INT
DECLARE @i INT
DECLARE @id INT
DECLARE @event_type NVARCHAR(4)
DECLARE @contract_id INT
DECLARE @event_date DATETIME
DECLARE @user_id INT
DECLARE @is_deleted BIT
DECLARE @entry_date DATETIME
DECLARE @is_exported BIT
DECLARE @comment NVARCHAR(255)
DECLARE @teller_id INT
DECLARE @past_due_days INT
DECLARE @principal MONEY
DECLARE @interests MONEY
DECLARE @installment_number INT
DECLARE @commissions MONEY
DECLARE @penalties MONEY

-- Replace one-to-many with one-to-one

DECLARE ce_cursor CURSOR FOR
SELECT ce.id, t.num, ce.event_type, ce.contract_id, ce.event_date, ce.user_id, ce.is_deleted, ce.entry_date, ce.is_exported, ce.comment, ce.teller_id
FROM dbo.ContractEvents ce
INNER JOIN
(
SELECT id, COUNT(id) num
FROM dbo.RepaymentEvents
GROUP BY id
) t ON ce.id = t.id
WHERE t.num > 1
AND ce.is_deleted = 0
GROUP BY ce.id, t.num, ce.event_type, ce.contract_id, ce.event_date, ce.user_id, ce.is_deleted, ce.entry_date, ce.is_exported, ce.comment, ce.teller_id

OPEN ce_cursor

FETCH NEXT FROM ce_cursor
INTO @id, @num, @event_type, @contract_id, @event_date, @user_id, @is_deleted, @entry_date, @is_exported, @comment, @teller_id

WHILE 0 = @@FETCH_STATUS 
    BEGIN
        SET @i = 0
    
        DECLARE re_cursor CURSOR FOR
        SELECT installment_number
        FROM dbo.RepaymentEvents
        WHERE id = @id
        ORDER BY installment_number
        OPEN re_cursor
    
        FETCH NEXT FROM re_cursor INTO @installment_number    
        WHILE 0 = @@FETCH_STATUS 
            BEGIN
                IF 0 = @i 
                    BEGIN
                        SET @parent_id = @id
                        SET @id_new = @id 
                        
                        INSERT  INTO #Events
                                ( id ,
                                  old_id ,
                                  event_type ,
                                  event_date ,
                                  number ,
                                  contract_id 
                                
                                )
                                SELECT  @id_new ,
                                        @id ,
                                        @event_type ,
                                        @event_date ,
                                        @installment_number ,
                                        @contract_id
                                FROM    dbo.RepaymentEvents
                                WHERE   id = @id
                                        AND installment_number = @installment_number
                    END
                ELSE 
                    BEGIN                        
                        INSERT  INTO dbo.ContractEvents
                                ( event_type ,
                                  contract_id ,
                                  event_date ,
                                  user_id ,
                                  is_deleted ,
                                  entry_date ,
                                  is_exported ,
                                  comment ,
                                  teller_id ,
                                  parent_id
                                )
                        VALUES  ( @event_type ,
                                  @contract_id ,
                                  @event_date ,
                                  @user_id ,
                                  @is_deleted ,
                                  @entry_date ,
                                  @is_exported ,
                                  @comment ,
                                  @teller_id ,
                                  @parent_id
                                )
                                
                        SET @id_new = @@IDENTITY
            
                        INSERT  INTO #Events
                                ( id ,
                                  old_id ,
                                  event_type ,
                                  event_date ,
                                  number ,
                                  contract_id 
                                
                                )
                                SELECT  @id_new ,
                                        @id ,
                                        @event_type ,
                                        @event_date ,
                                        @installment_number ,
                                        @contract_id
                                FROM    dbo.RepaymentEvents
                                WHERE   id = @id
                                        AND installment_number = @installment_number
                                
                        UPDATE  dbo.RepaymentEvents
                        SET     id = @id_new
                        WHERE   id = @id
                                AND installment_number = @installment_number
                    END
        
                SET @i = @i + 1
                
                FETCH NEXT FROM re_cursor INTO @installment_number
            END
    
        CLOSE re_cursor
        DEALLOCATE re_cursor
    
        FETCH NEXT FROM ce_cursor
    INTO @id, @num, @event_type, @contract_id, @event_date, @user_id, @is_deleted, @entry_date, @is_exported, @comment, @teller_id
    END

CLOSE ce_cursor
DEALLOCATE ce_cursor

DELETE  FROM #Events
WHERE   event_type IN ( 'ROWO', 'ATR', 'APTR' )

UPDATE  #Events
SET     event_type_new = CASE WHEN i.expected_date < e.event_date
                              THEN CASE WHEN i.capital_repayment <> snap.capital_repayment
                                        THEN 'APR'
                                        ELSE 'RBLE'
                                   END
                              WHEN i.expected_date > e.event_date
                              THEN CASE WHEN i.capital_repayment <> snap.capital_repayment
                                        THEN 'APR'
                                        ELSE 'RGLE'
                                   END
                              WHEN i.expected_date = e.event_date
                              THEN CASE WHEN i.capital_repayment <> snap.capital_repayment
                                        THEN 'APR'
                                        ELSE 'RGLE'
                                   END
                              ELSE CASE WHEN i.capital_repayment <> snap.capital_repayment
                                        THEN 'APR'
                                        ELSE 'RGLE'
                                   END
                         END ,
        capital = i.capital_repayment ,
        new_capital = snap.capital_repayment
FROM    #Events e
        INNER JOIN dbo.Installments i ON e.number = i.number
                                         AND i.contract_id = e.contract_id
        INNER JOIN InstallmentHistory snap ON i.contract_id = snap.contract_id
                                              AND i.number = snap.number
                                              AND snap.event_id = e.old_id

UPDATE  dbo.ContractEvents
SET     event_type = event_type_new
FROM    #Events
WHERE   dbo.ContractEvents.id = #Events.id
        AND #Events.event_type_new <> #Events.event_type

-- Fix for ATR cases
UPDATE  ContractEvents
SET     ContractEvents.event_type = 'ATR'
WHERE   id IN ( SELECT  ce.id
                FROM    ContractEvents ce
                        INNER JOIN dbo.Contracts c ON ce.contract_id = c.id
                        INNER JOIN #Events ON ce.id = #Events.id
                WHERE   ContractEvents.event_type = 'APR'
                        AND closed = 1
                        AND is_deleted = 0
                        AND ce.event_date = c.close_date )
        
DROP TABLE #Events
GO

INSERT INTO [PaymentMethods] ([name])
VALUES ('Voucher')
GO

UPDATE [dbo].[Installments]SET payment_method = 1
WHERE (payment_method IS NULL AND [paid_interest]<>0 AND [paid_capital]<>0) 
	   OR (payment_method IS NULL AND [paid_fees]<>0) OR payment_method>6
GO

IF (EXISTS(SELECT * FROM GeneralParameters 
				WHERE  [key]='PENDING_REPAYMENT_MODE' 
				AND [value] like '%All%'))
UPDATE PaymentMethods SET pending=1
ELSE
BEGIN
	DECLARE PaymentMethodsNamesCURSOR CURSOR FOR
	SELECT id, name FROM PaymentMethods

	DECLARE @payment_method_name nvarchar(max), @payment_method_id INT 
	OPEN PaymentMethodsNamesCURSOR
	FETCH NEXT FROM PaymentMethodsNamesCURSOR INTO @payment_method_id, @payment_method_name
	WHILE @@FETCH_STATUS=0
	BEGIN
		IF (EXISTS(SELECT * FROM GeneralParameters 
					WHERE  [key]='PENDING_REPAYMENT_MODE' 
					AND value like '%'+@payment_method_name+'%'))
		UPDATE PaymentMethods SET pending=1 WHERE id=@payment_method_id
		FETCH NEXT FROM PaymentMethodsNamesCURSOR INTO @payment_method_id, @payment_method_name
	END
	CLOSE PaymentMethodsNamesCURSOR
	DEALLOCATE PaymentMethodsNamesCURSOR
END
GO

INSERT INTO [PaymentMethods] ([name])
VALUES ('Savings')

ALTER TABLE [dbo].[SavingEvents]
ADD loan_id [int] NULL
GO

INSERT INTO dbo.EventTypes (event_type, description, sort_order, accounting) 
VALUES ('SVLD', 'Savings loan disbursement event', 315, 1)
GO

INSERT INTO EventAttributes (event_type, name) VALUES ('SVLD', 'amount')
INSERT INTO EventAttributes (event_type, name) VALUES ('SVLD', 'fees')
GO

ALTER TABLE [dbo].LoanDisbursmentEvents
ADD payment_method_id [int] NULL
GO

INSERT INTO EventAttributes (event_type, name) VALUES ('SCIT', 'fees')
INSERT INTO EventAttributes (event_type, name) VALUES ('SDIT', 'fees')
INSERT INTO EventAttributes (event_type, name) VALUES ('SCIT', 'amount')
INSERT INTO EventAttributes (event_type, name) VALUES ('SDIT', 'amount')
GO

UPDATE [dbo].LoanDisbursmentEvents
SET payment_method_id=1
GO

ALTER TABLE dbo.Packages 
ADD use_entry_fees_cycles [BIT] NOT NULL DEFAULT((0))
GO

ALTER TABLE [dbo].[EntryFees]
ADD [cycle_id] [int] NULL
GO

CREATE TABLE dbo.Rep_OLB_and_LLP_Data
(
    id INT NOT NULL
    , branch_name NVARCHAR(50) NULL
    , load_date DATETIME NULL
    , contract_code NVARCHAR(255) NULL
    , olb MONEY NULL
    , interest MONEY NULL
    , late_days INT NULL
    , client_name NVARCHAR(255) NULL
    , loan_officer_name NVARCHAR(255) NULL
    , product_name NVARCHAR(255) NULL
    , district_name NVARCHAR(255) NULL
    , start_date DATETIME NULL
    , close_date DATETIME NULL
    , range_from INT NULL
    , range_to INT NULL
    , llp_rate INT NULL
    , llp MONEY NULL
    , rescheduled BIT NULL
)
GO

ALTER TABLE dbo.Rep_Active_Loans_Data
ADD break_down_id INT NULL
GO

ALTER TABLE dbo.Rep_Par_Analysis_Data
ADD break_down_id INT NULL
GO

CREATE TABLE dbo.Rep_Disbursements_Data
(
    id INT NOT NULL
    , branch_name NVARCHAR(50) NULL
    , load_date DATETIME NULL
    , contract_code NVARCHAR(255) NULL
    , district NVARCHAR(255) NULL
    , loan_product NVARCHAR(255) NULL
    , client_name NVARCHAR(255) NULL
    , loan_cycle INT NULL
    , loan_officer NVARCHAR(255) NULL
    , disbursement_date DATETIME NULL
    , amount MONEY NULL
    , interest MONEY NULL
    , fees MONEY NULL
)
GO

ALTER TABLE AdvancedFieldsCollections
ADD id INT IDENTITY(1,1) NOT NULL
GO

ALTER TABLE CollateralPropertyValues
ALTER COLUMN value nvarchar (1000)
GO

-- SIPE event
DECLARE @fees_event_id INT
SELECT @fees_event_id = id FROM dbo.EventAttributes 
WHERE event_type = 'SIPE' AND name = 'fees'

IF @fees_event_id IS NOT NULL
DELETE FROM dbo.EventAttributes WHERE id = @fees_event_id
GO

DECLARE @amount_event_id INT
SELECT @amount_event_id = id FROM dbo.EventAttributes
WHERE event_type = 'SIPE' AND name = 'amount'

IF @amount_event_id IS NULL
INSERT INTO dbo.EventAttributes( event_type, name )
VALUES ( 'SIPE', 'amount' )
GO

-- SIAE
DECLARE @fees_event_id INT
SELECT @fees_event_id = id FROM dbo.EventAttributes 
WHERE event_type = 'SIAE' AND name = 'fees'

IF @fees_event_id IS NOT NULL
DELETE FROM dbo.EventAttributes WHERE id = @fees_event_id
GO

DECLARE @amount_event_id INT
SELECT @amount_event_id = id FROM dbo.EventAttributes
WHERE event_type = 'SIAE' AND name = 'amount'

IF @amount_event_id IS NULL
INSERT INTO dbo.EventAttributes( event_type, name )
VALUES ( 'SIAE', 'amount' )
GO

UPDATE  dbo.Packages
SET     number_of_installments = num ,
        number_of_installments_max = NULL ,
        number_of_installments_min = NULL
FROM    ( SELECT    e.id ,
                    COUNT(ei.exotic_id) AS num
          FROM      dbo.Exotics e
                    INNER JOIN dbo.ExoticInstallments ei ON e.id = ei.exotic_id
          GROUP BY  e.id
        ) t1
WHERE   dbo.Packages.exotic_id = t1.id
GO

UPDATE  dbo.Packages
SET loan_type = 2
WHERE loan_type = 3 
  AND exotic_id IS NOT NULL
GO

DECLARE @active_loans TABLE
(
    contract_id INT NOT NULL
)
DECLARE @active_clients TABLE
(
    client_id INT NOT NULL
)

INSERT INTO @active_loans
SELECT t.contract_id
FROM
(
    SELECT contract_id, SUM(capital_repayment - paid_capital) olb
    FROM dbo.Installments
    GROUP BY contract_id
) t
LEFT JOIN
(
    SELECT c.id contract_id
    , SUM(CASE WHEN 'LODE' = ce.event_type THEN 1 ELSE 0 END) disbursements
    , SUM(CASE WHEN 'WROE' = ce.event_type THEN 1 ELSE 0 END) writeoffs
    FROM dbo.Contracts c    
    LEFT JOIN dbo.ContractEvents ce ON ce.contract_id = c.id    
    WHERE ce.is_deleted = 0
    GROUP BY c.id
) e ON e.contract_id = t.contract_id
WHERE t.olb > 0 AND e.disbursements > 0 AND 0 = e.writeoffs

INSERT INTO @active_clients
SELECT j.tiers_id
FROM dbo.Contracts c
INNER JOIN @active_loans al ON c.id = al.contract_id
INNER JOIN dbo.Projects j ON j.id = c.project_id

INSERT INTO @active_clients
SELECT person_id
FROM dbo.LoanShareAmounts
WHERE contract_id IN (SELECT contract_id FROM @active_loans)

UPDATE dbo.Tiers SET active = 1 WHERE id IN (SELECT client_id FROM @active_clients)

GO

UPDATE [TechnicalParameters] SET [value] = 'v3.5.0' WHERE [name] = 'VERSION'
GO