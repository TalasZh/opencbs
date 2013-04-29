UPDATE Accounts SET [description] = '1' WHERE [description] = 'Balance Sheet / Asset'
GO
UPDATE Accounts SET [description] = '1' WHERE [description] = 'Balance Sheet / Assets'
GO
UPDATE Accounts SET [description] = '2' WHERE [description] = 'Balance Sheet / Liabilities'
GO
UPDATE Accounts SET [description] = '3' WHERE [description] = 'P&L / Expense'
GO
UPDATE Accounts SET [description] = '4' WHERE [description] = 'P&L / Income'
GO

CREATE TABLE dbo.Tmp_Accounts
	(
	id int NOT NULL IDENTITY (1, 1),
	account_number nvarchar(50) NOT NULL,
	local_account_number nvarchar(50) NULL,
	label nvarchar(200) NOT NULL,
	balance money NOT NULL,
	debit_plus bit NOT NULL,
	type_code varchar(60) NOT NULL,
	description smallint NOT NULL
	)  ON [PRIMARY]
GO
SET IDENTITY_INSERT dbo.Tmp_Accounts ON
GO
IF EXISTS(SELECT * FROM dbo.Accounts)
	 EXEC('INSERT INTO dbo.Tmp_Accounts (id, account_number, local_account_number, label, balance, debit_plus, type_code, description)
		SELECT id, account_number, local_account_number, label, balance, debit_plus, type_code, CONVERT(smallint, description) FROM dbo.Accounts WITH (HOLDLOCK TABLOCKX)')
GO
SET IDENTITY_INSERT dbo.Tmp_Accounts OFF
GO
ALTER TABLE dbo.ElementaryMvts
	DROP CONSTRAINT FK_ElementaryMvts_Credit_Accounts
GO
ALTER TABLE dbo.ElementaryMvts
	DROP CONSTRAINT FK_ElementaryMvts_Debit_Accounts
GO
DROP TABLE dbo.Accounts
GO
EXECUTE sp_rename N'dbo.Tmp_Accounts', N'Accounts', 'OBJECT' 
GO
ALTER TABLE dbo.Accounts ADD CONSTRAINT
	PK_Accounts PRIMARY KEY CLUSTERED 
	(
	id
	)  ON [PRIMARY]

GO
ALTER TABLE dbo.ElementaryMvts WITH NOCHECK ADD CONSTRAINT
	FK_ElementaryMvts_Credit_Accounts FOREIGN KEY
	(
	credit_account_id
	) REFERENCES dbo.Accounts
	(
	id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.ElementaryMvts WITH NOCHECK ADD CONSTRAINT
	FK_ElementaryMvts_Debit_Accounts FOREIGN KEY
	(
	debit_account_id
	) REFERENCES dbo.Accounts
	(
	id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO

ALTER TABLE dbo.Persons ADD
	father_name nvarchar(200) NULL
GO

/**** Package ****/
ALTER TABLE dbo.Packages ADD
	non_repayment_penalties_based_on_overdue_interest float(53) NULL,
	non_repayment_penalties_based_on_initial_amount float(53) NULL,
	non_repayment_penalties_based_on_olb float(53) NULL,
	non_repayment_penalties_based_on_overdue_principal float(53) NULL,

	non_repayment_penalties_based_on_overdue_interest_min float(53) NULL,
	non_repayment_penalties_based_on_initial_amount_min float(53) NULL,
	non_repayment_penalties_based_on_olb_min float(53) NULL,
	non_repayment_penalties_based_on_overdue_principal_min float(53) NULL,
	
	non_repayment_penalties_based_on_overdue_interest_max float(53) NULL,
	non_repayment_penalties_based_on_initial_amount_max float(53) NULL,
	non_repayment_penalties_based_on_olb_max float(53) NULL,
	non_repayment_penalties_based_on_overdue_principal_max float(53) NULL
GO

UPDATE Packages SET non_repayment_penalties_based_on_initial_amount = non_repayment_penalties
UPDATE Packages SET non_repayment_penalties_based_on_initial_amount_min = non_repayment_penalties_min
UPDATE Packages SET non_repayment_penalties_based_on_initial_amount_max = non_repayment_penalties_max

UPDATE Packages SET non_repayment_penalties_based_on_overdue_interest = non_repayment_penalties
UPDATE Packages SET non_repayment_penalties_based_on_overdue_interest_min = non_repayment_penalties_min
UPDATE Packages SET non_repayment_penalties_based_on_overdue_interest_max = non_repayment_penalties_max

UPDATE Packages SET non_repayment_penalties_based_on_overdue_principal = non_repayment_penalties
UPDATE Packages SET non_repayment_penalties_based_on_overdue_principal_min = non_repayment_penalties_min
UPDATE Packages SET non_repayment_penalties_based_on_overdue_principal_max = non_repayment_penalties_max

UPDATE Packages SET non_repayment_penalties_based_on_olb = non_repayment_penalties
UPDATE Packages SET non_repayment_penalties_based_on_olb_min = non_repayment_penalties_min
UPDATE Packages SET non_repayment_penalties_based_on_olb_max = non_repayment_penalties_max
GO

UPDATE Packages SET non_repayment_penalties_based_on_initial_amount = 0 WHERE non_repayment_penalties_base = 1 /* OLB */
UPDATE Packages SET non_repayment_penalties_based_on_initial_amount_min = NULL WHERE non_repayment_penalties_base = 1 /* OLB */
UPDATE Packages SET non_repayment_penalties_based_on_initial_amount_max = NULL WHERE non_repayment_penalties_base = 1 /* OLB */

UPDATE Packages SET non_repayment_penalties_based_on_overdue_interest = 0 WHERE non_repayment_penalties_base = 1 /* OLB */
UPDATE Packages SET non_repayment_penalties_based_on_overdue_interest_min = NULL WHERE non_repayment_penalties_base = 1 /* OLB */
UPDATE Packages SET non_repayment_penalties_based_on_overdue_interest_max = NULL WHERE non_repayment_penalties_base = 1 /* OLB */

UPDATE Packages SET non_repayment_penalties_based_on_overdue_principal = 0 WHERE non_repayment_penalties_base = 1 /* OLB */
UPDATE Packages SET non_repayment_penalties_based_on_overdue_principal_min = NULL WHERE non_repayment_penalties_base = 1 /* OLB */
UPDATE Packages SET non_repayment_penalties_based_on_overdue_principal_max = NULL WHERE non_repayment_penalties_base = 1 /* OLB */
GO

UPDATE Packages SET non_repayment_penalties_based_on_olb = 0 WHERE non_repayment_penalties_base = 4 /* InitialAmount */
UPDATE Packages SET non_repayment_penalties_based_on_olb_min = NULL WHERE non_repayment_penalties_base = 4 /* InitialAmount */
UPDATE Packages SET non_repayment_penalties_based_on_olb_max = NULL WHERE non_repayment_penalties_base = 4 /* InitialAmount */

UPDATE Packages SET non_repayment_penalties_based_on_overdue_interest = 0 WHERE non_repayment_penalties_base = 4 /* InitialAmount */
UPDATE Packages SET non_repayment_penalties_based_on_overdue_interest_min = NULL WHERE non_repayment_penalties_base = 4 /* InitialAmount */
UPDATE Packages SET non_repayment_penalties_based_on_overdue_interest_max = NULL WHERE non_repayment_penalties_base = 4 /* InitialAmount */

UPDATE Packages SET non_repayment_penalties_based_on_overdue_principal = 0 WHERE non_repayment_penalties_base = 4 /* InitialAmount */
UPDATE Packages SET non_repayment_penalties_based_on_overdue_principal_min = NULL WHERE non_repayment_penalties_base = 4 /* InitialAmount */
UPDATE Packages SET non_repayment_penalties_based_on_overdue_principal_max = NULL WHERE non_repayment_penalties_base = 4 /* InitialAmount */
GO

UPDATE Packages SET non_repayment_penalties_based_on_olb = 0 WHERE non_repayment_penalties_base = 2 /* WithOutInterest */
UPDATE Packages SET non_repayment_penalties_based_on_olb_min = NULL WHERE non_repayment_penalties_base = 2 /* WithOutInterest */
UPDATE Packages SET non_repayment_penalties_based_on_olb_max = NULL WHERE non_repayment_penalties_base = 2 /* WithOutInterest */

UPDATE Packages SET non_repayment_penalties_based_on_overdue_interest = 0 WHERE non_repayment_penalties_base = 2 /* WithOutInterest */
UPDATE Packages SET non_repayment_penalties_based_on_overdue_interest_min = NULL WHERE non_repayment_penalties_base = 2 /* WithOutInterest */
UPDATE Packages SET non_repayment_penalties_based_on_overdue_interest_max = NULL WHERE non_repayment_penalties_base = 2 /* WithOutInterest */

UPDATE Packages SET non_repayment_penalties_based_on_initial_amount = 0 WHERE non_repayment_penalties_base = 2 /* WithOutInterest */
UPDATE Packages SET non_repayment_penalties_based_on_initial_amount_min = NULL WHERE non_repayment_penalties_base = 2 /* WithOutInterest */
UPDATE Packages SET non_repayment_penalties_based_on_initial_amount_max = NULL WHERE non_repayment_penalties_base = 2 /* WithOutInterest */
GO

UPDATE Packages SET non_repayment_penalties_based_on_olb = 0 WHERE non_repayment_penalties_base = 3 /* WithInterest */
UPDATE Packages SET non_repayment_penalties_based_on_olb_min = NULL WHERE non_repayment_penalties_base = 3 /* WithInterest */
UPDATE Packages SET non_repayment_penalties_based_on_olb_max = NULL WHERE non_repayment_penalties_base = 3 /* WithInterest */

UPDATE Packages SET non_repayment_penalties_based_on_initial_amount = 0 WHERE non_repayment_penalties_base = 3 /* WithInterest */
UPDATE Packages SET non_repayment_penalties_based_on_initial_amount_min = NULL WHERE non_repayment_penalties_base = 3 /* WithInterest */
UPDATE Packages SET non_repayment_penalties_based_on_initial_amount_max = NULL WHERE non_repayment_penalties_base = 3 /* WithInterest */
GO


/**** Credit ****/
ALTER TABLE dbo.Credit ADD
	non_repayment_penalties_based_on_overdue_principal float(53) NOT NULL CONSTRAINT DF_Credit_non_repayment_penalties_based_on_overdue_principal DEFAULT 0,
	non_repayment_penalties_based_on_initial_amount float(53) NOT NULL CONSTRAINT DF_Credit_non_repayment_penalties_based_on_initial_amount DEFAULT 0,
	non_repayment_penalties_based_on_olb float(53) NOT NULL CONSTRAINT DF_Credit_non_repayment_penalties_based_on_olb DEFAULT 0,
	non_repayment_penalties_based_on_overdue_interest float(53) NOT NULL CONSTRAINT DF_Credit_non_repayment_penalties_based_on_overdue_interest DEFAULT 0
GO

DECLARE @packageId int
DECLARE CURSOR_PACKAGE_OLB CURSOR FOR
SELECT id FROM Packages WHERE non_repayment_penalties_base = 1

OPEN CURSOR_PACKAGE_OLB;
FETCH NEXT FROM CURSOR_PACKAGE_OLB
	INTO @packageId
WHILE @@FETCH_STATUS=0
BEGIN
	UPDATE Credit SET non_repayment_penalties_based_on_olb = non_repayment_penalties WHERE package_id = @packageId

	FETCH NEXT FROM CURSOR_PACKAGE_OLB
		INTO @packageId
END
CLOSE		CURSOR_PACKAGE_OLB
DEALLOCATE	CURSOR_PACKAGE_OLB
GO

DECLARE @packageId int
DECLARE CURSOR_PACKAGE_INITIAL_AMOUNT CURSOR FOR
SELECT id FROM Packages WHERE non_repayment_penalties_base = 4

OPEN CURSOR_PACKAGE_INITIAL_AMOUNT;
FETCH NEXT FROM CURSOR_PACKAGE_INITIAL_AMOUNT
	INTO @packageId
WHILE @@FETCH_STATUS=0
BEGIN
	UPDATE Credit SET non_repayment_penalties_based_on_initial_amount = non_repayment_penalties WHERE package_id = @packageId

	FETCH NEXT FROM CURSOR_PACKAGE_INITIAL_AMOUNT
		INTO @packageId
END
CLOSE		CURSOR_PACKAGE_INITIAL_AMOUNT
DEALLOCATE	CURSOR_PACKAGE_INITIAL_AMOUNT
GO

DECLARE @packageId int
DECLARE CURSOR_PACKAGE_WITH_INTEREST CURSOR FOR
SELECT id FROM Packages WHERE non_repayment_penalties_base = 3

OPEN CURSOR_PACKAGE_WITH_INTEREST;
FETCH NEXT FROM CURSOR_PACKAGE_WITH_INTEREST
	INTO @packageId
WHILE @@FETCH_STATUS=0
BEGIN
	UPDATE Credit SET non_repayment_penalties_based_on_overdue_principal = non_repayment_penalties WHERE package_id = @packageId
	UPDATE Credit SET non_repayment_penalties_based_on_overdue_interest = non_repayment_penalties WHERE package_id = @packageId

	FETCH NEXT FROM CURSOR_PACKAGE_WITH_INTEREST
		INTO @packageId
END
CLOSE		CURSOR_PACKAGE_WITH_INTEREST
DEALLOCATE	CURSOR_PACKAGE_WITH_INTEREST
GO

DECLARE @packageId int
DECLARE CURSOR_PACKAGE_WITHOUT_INTEREST CURSOR FOR
SELECT id FROM Packages WHERE non_repayment_penalties_base = 2

OPEN CURSOR_PACKAGE_WITHOUT_INTEREST;
FETCH NEXT FROM CURSOR_PACKAGE_WITHOUT_INTEREST
	INTO @packageId
WHILE @@FETCH_STATUS=0
BEGIN
	UPDATE Credit SET non_repayment_penalties_based_on_overdue_principal = non_repayment_penalties WHERE package_id = @packageId

	FETCH NEXT FROM CURSOR_PACKAGE_WITHOUT_INTEREST
		INTO @packageId
END
CLOSE		CURSOR_PACKAGE_WITHOUT_INTEREST
DEALLOCATE	CURSOR_PACKAGE_WITHOUT_INTEREST
GO


ALTER TABLE dbo.Packages
	DROP COLUMN non_repayment_penalties, non_repayment_penalties_min, non_repayment_penalties_max, non_repayment_penalties_base
GO
ALTER TABLE dbo.Credit
	DROP COLUMN non_repayment_penalties
GO
/************** VERSION ***********************/

UPDATE [TechnicalParameters] SET [value]='v1.1.3'
GO
