IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Pictures]') AND type in (N'U'))
DROP TABLE [dbo].[Pictures]
GO

CREATE TABLE [dbo].[Pictures](
	[group] [nvarchar](50) NOT NULL,
	[id] [int] NOT NULL,
	[subid] [int] NOT NULL CONSTRAINT [DF_Pictures_subid]  DEFAULT ((0)),
	[picture] [image] NOT NULL,
	[thumbnail] [image] NOT NULL,
	[name] [varchar](50) NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

CREATE TABLE dbo.ContractAccountsBalance
	(
	contract_id int NOT NULL,
	account_number nvarchar(50) NOT NULL,
	balance money NOT NULL
	)  ON [PRIMARY]
GO

ALTER TABLE dbo.ContractAccountsBalance ADD CONSTRAINT
	PK_ContractAccountsBalance PRIMARY KEY CLUSTERED 
	(
	contract_id,
	account_number
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

ALTER TABLE dbo.ContractAccountsBalance WITH NOCHECK ADD CONSTRAINT
	FK_ContractAccountsBalance_Contracts FOREIGN KEY
	(
	contract_id
	) REFERENCES dbo.Contracts
	(
	id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
DECLARE @CONTRACT_ID int

DECLARE CURSOR_CONTRACT_ID CURSOR FOR
SELECT DISTINCT (id) FROM Contracts

OPEN CURSOR_CONTRACT_ID;
FETCH NEXT FROM CURSOR_CONTRACT_ID INTO @CONTRACT_ID

WHILE @@FETCH_STATUS=0
BEGIN
	INSERT INTO [ContractAccountsBalance]([contract_id],[account_number],[balance]) VALUES(@CONTRACT_ID,'1011',0)
	INSERT INTO [ContractAccountsBalance]([contract_id],[account_number],[balance]) VALUES(@CONTRACT_ID,'20311',0)
	INSERT INTO [ContractAccountsBalance]([contract_id],[account_number],[balance]) VALUES(@CONTRACT_ID,'20312',0)
	INSERT INTO [ContractAccountsBalance]([contract_id],[account_number],[balance]) VALUES(@CONTRACT_ID,'2032',0)
	INSERT INTO [ContractAccountsBalance]([contract_id],[account_number],[balance]) VALUES(@CONTRACT_ID,'2037',0)
	INSERT INTO [ContractAccountsBalance]([contract_id],[account_number],[balance]) VALUES(@CONTRACT_ID,'2038',0)
	INSERT INTO [ContractAccountsBalance]([contract_id],[account_number],[balance]) VALUES(@CONTRACT_ID,'2911',0)
	INSERT INTO [ContractAccountsBalance]([contract_id],[account_number],[balance]) VALUES(@CONTRACT_ID,'2921',0)
	INSERT INTO [ContractAccountsBalance]([contract_id],[account_number],[balance]) VALUES(@CONTRACT_ID,'2971',0)
	INSERT INTO [ContractAccountsBalance]([contract_id],[account_number],[balance]) VALUES(@CONTRACT_ID,'2991',0)
	INSERT INTO [ContractAccountsBalance]([contract_id],[account_number],[balance]) VALUES(@CONTRACT_ID,'3882',0)
	INSERT INTO [ContractAccountsBalance]([contract_id],[account_number],[balance]) VALUES(@CONTRACT_ID,'6712',0)
	INSERT INTO [ContractAccountsBalance]([contract_id],[account_number],[balance]) VALUES(@CONTRACT_ID,'6751',0)
	INSERT INTO [ContractAccountsBalance]([contract_id],[account_number],[balance]) VALUES(@CONTRACT_ID,'70211',0)
	INSERT INTO [ContractAccountsBalance]([contract_id],[account_number],[balance]) VALUES(@CONTRACT_ID,'70212',0)
	INSERT INTO [ContractAccountsBalance]([contract_id],[account_number],[balance]) VALUES(@CONTRACT_ID,'7022',0)
	INSERT INTO [ContractAccountsBalance]([contract_id],[account_number],[balance]) VALUES(@CONTRACT_ID,'7028',0)
	INSERT INTO [ContractAccountsBalance]([contract_id],[account_number],[balance]) VALUES(@CONTRACT_ID,'7029',0)
	INSERT INTO [ContractAccountsBalance]([contract_id],[account_number],[balance]) VALUES(@CONTRACT_ID,'7712',0)
	INSERT INTO [ContractAccountsBalance]([contract_id],[account_number],[balance]) VALUES(@CONTRACT_ID,'70271',0)
	INSERT INTO [ContractAccountsBalance]([contract_id],[account_number],[balance]) VALUES(@CONTRACT_ID,'70272',0)
	INSERT INTO [ContractAccountsBalance]([contract_id],[account_number],[balance]) VALUES(@CONTRACT_ID,'5211',0)
	INSERT INTO [ContractAccountsBalance]([contract_id],[account_number],[balance]) VALUES(@CONTRACT_ID,'6731',0)
	INSERT INTO [ContractAccountsBalance]([contract_id],[account_number],[balance]) VALUES(@CONTRACT_ID,'7731',0)
	
	FETCH NEXT FROM CURSOR_CONTRACT_ID INTO @CONTRACT_ID
END
CLOSE		CURSOR_CONTRACT_ID
DEALLOCATE	CURSOR_CONTRACT_ID
GO

ALTER TABLE dbo.PersonGroupBelonging ADD
	joined_date datetime NULL,
	left_date datetime NULL
GO

ALTER TABLE dbo.Contracts ADD
	closed bit NULL
GO

UPDATE Contracts SET [closed] = 0
GO

UPDATE Contracts SET [closed] = 1 WHERE 
(
SELECT SUM(capital_repayment - paid_capital + interest_repayment - paid_interest) 
FROM Installments WHERE Installments.contract_id = Contracts.id
) < 0.02
GO

UPDATE  [Groups]
SET     [establishment_date] = ( SELECT MIN(Contracts.[start_date])
                                 FROM   Contracts
                                 WHERE  contracts.beneficiary_id = Groups.[id]
                               )
WHERE   [establishment_date] IS NULL
GO

UPDATE [PersonGroupBelonging] SET [joined_date] = (SELECT ISNULL(establishment_date,GETDATE()) FROM Groups WHERE id = PersonGroupBelonging.group_id)
GO

UPDATE [PersonGroupBelonging] SET [left_date] = (SELECT ISNULL(establishment_date,GETDATE()) FROM Groups WHERE id = PersonGroupBelonging.group_id)
WHERE currently_in = 0
GO

ALTER TABLE dbo.PersonGroupBelonging
	DROP CONSTRAINT FK_PersonGroupCorrespondance_Groups
GO

ALTER TABLE dbo.PersonGroupBelonging
	DROP CONSTRAINT FK_PersonGroupBelonging_Persons1
GO

CREATE TABLE dbo.Tmp_PersonGroupBelonging
	(
	person_id int NOT NULL,
	group_id int NOT NULL,
	is_leader bit NOT NULL,
	currently_in bit NOT NULL,
	loan_share_amount money NOT NULL,
	joined_date datetime NOT NULL,
	left_date datetime NULL
	)  ON [PRIMARY]
GO

IF EXISTS(SELECT * FROM dbo.PersonGroupBelonging)
	 EXEC('INSERT INTO dbo.Tmp_PersonGroupBelonging (person_id, group_id, is_leader, currently_in, loan_share_amount, joined_date, left_date)
		SELECT person_id, group_id, is_leader, currently_in, loan_share_amount, joined_date, left_date FROM dbo.PersonGroupBelonging WITH (HOLDLOCK TABLOCKX)')
GO

DROP TABLE dbo.PersonGroupBelonging
GO

EXECUTE sp_rename N'dbo.Tmp_PersonGroupBelonging', N'PersonGroupBelonging', 'OBJECT' 
GO

ALTER TABLE dbo.PersonGroupBelonging ADD CONSTRAINT
	PK_PersonGroupBelonging PRIMARY KEY CLUSTERED 
	(
	person_id,
	group_id
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

ALTER TABLE dbo.PersonGroupBelonging WITH NOCHECK ADD CONSTRAINT
	FK_PersonGroupBelonging_Persons1 FOREIGN KEY
	(
	person_id
	) REFERENCES dbo.Persons
	(
	id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
GO

ALTER TABLE dbo.PersonGroupBelonging WITH NOCHECK ADD CONSTRAINT
	FK_PersonGroupCorrespondance_Groups FOREIGN KEY
	(
	group_id
	) REFERENCES dbo.Groups
	(
	id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
GO

ALTER TABLE dbo.Contracts
	DROP CONSTRAINT FK_Contracts_Beneficiary
GO

CREATE TABLE dbo.Tmp_Contracts
	(
	id int NOT NULL IDENTITY (1, 1),
	contract_code nvarchar(50) NOT NULL,
	branch_code varchar(50) NOT NULL,
	beneficiary_id int NOT NULL,
	creation_date datetime NOT NULL,
	start_date datetime NOT NULL,
	close_date datetime NOT NULL,
	rural bit NOT NULL,
	closed bit NOT NULL
	)  ON [PRIMARY]
GO

SET IDENTITY_INSERT dbo.Tmp_Contracts ON
GO

IF EXISTS(SELECT * FROM dbo.Contracts)
	 EXEC('INSERT INTO dbo.Tmp_Contracts (id, contract_code, branch_code, beneficiary_id, creation_date, start_date, close_date, rural, closed)
		SELECT id, contract_code, branch_code, beneficiary_id, creation_date, start_date, close_date, rural, closed FROM dbo.Contracts WITH (HOLDLOCK TABLOCKX)')
GO

SET IDENTITY_INSERT dbo.Tmp_Contracts OFF
GO

ALTER TABLE dbo.ContractAccountsBalance
	DROP CONSTRAINT FK_ContractAccountsBalance_Contracts
GO

ALTER TABLE dbo.ContractEvents
	DROP CONSTRAINT FK_ContractEvents_Contracts
GO

ALTER TABLE dbo.Credit
	DROP CONSTRAINT FK_Credit_Contracts
GO

ALTER TABLE dbo.LinkGuarantorCredit
	DROP CONSTRAINT FK_LinkGuarantorCredit_Contracts
GO

DROP TABLE dbo.Contracts
GO

EXECUTE sp_rename N'dbo.Tmp_Contracts', N'Contracts', 'OBJECT' 
GO

ALTER TABLE dbo.Contracts ADD CONSTRAINT
	PK_Contracts PRIMARY KEY CLUSTERED 
	(
	id
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO

ALTER TABLE dbo.Contracts WITH NOCHECK ADD CONSTRAINT
	FK_Contracts_Beneficiary FOREIGN KEY
	(
	beneficiary_id
	) REFERENCES dbo.Tiers
	(
	id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO

ALTER TABLE dbo.LinkGuarantorCredit WITH NOCHECK ADD CONSTRAINT
	FK_LinkGuarantorCredit_Contracts FOREIGN KEY
	(
	contract_id
	) REFERENCES dbo.Contracts
	(
	id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO

ALTER TABLE dbo.Credit WITH NOCHECK ADD CONSTRAINT
	FK_Credit_Contracts FOREIGN KEY
	(
	id
	) REFERENCES dbo.Contracts
	(
	id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO

ALTER TABLE dbo.ContractEvents WITH NOCHECK ADD CONSTRAINT
	FK_ContractEvents_Contracts FOREIGN KEY
	(
	contract_id
	) REFERENCES dbo.Contracts
	(
	id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO

ALTER TABLE dbo.ContractAccountsBalance WITH NOCHECK ADD CONSTRAINT
	FK_ContractAccountsBalance_Contracts FOREIGN KEY
	(
	contract_id
	) REFERENCES dbo.Contracts
	(
	id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO

ALTER TABLE TempCashReceipt ADD
	loan_amount money NULL,
	disbursment_date datetime NULL,
	maturity int NULL,
	gracePeriod int NULL
GO
ALTER TABLE Users ADD
	mail nvarchar(100) NOT NULL CONSTRAINT DF_Users_mail DEFAULT N'Not Set'
GO
ALTER TABLE dbo.ElementaryMvts ADD label smallint NOT NULL CONSTRAINT DF_ElementaryMvts_label DEFAULT 4
GO
UPDATE [ElementaryMvts] SET [label] = 3 WHERE debit_account_id = (select id from accounts where account_number = '7029')
UPDATE [ElementaryMvts] SET [label] = 3 WHERE credit_account_id = (select id from accounts where account_number = '7029')
GO
UPDATE [ElementaryMvts] SET [label] = 1 WHERE debit_account_id = (select id from accounts where account_number = '20311')
UPDATE [ElementaryMvts] SET [label] = 1 WHERE credit_account_id = (select id from accounts where account_number = '20311')
GO
UPDATE [ElementaryMvts] SET [label] = 1 WHERE debit_account_id = (select id from accounts where account_number = '20312')
UPDATE [ElementaryMvts] SET [label] = 1 WHERE credit_account_id = (select id from accounts where account_number = '20312')
GO
UPDATE [ElementaryMvts] SET [label] = 2 WHERE debit_account_id = (select id from accounts where account_number = '70211')
UPDATE [ElementaryMvts] SET [label] = 2 WHERE credit_account_id = (select id from accounts where account_number = '70211')
GO
UPDATE [ElementaryMvts] SET [label] = 2 WHERE debit_account_id = (select id from accounts where account_number = '70212')
UPDATE [ElementaryMvts] SET [label] = 2 WHERE credit_account_id = (select id from accounts where account_number = '70212')
GO
UPDATE [ElementaryMvts] SET [label] = 1 WHERE debit_account_id = (select id from accounts where account_number = '2032')
UPDATE [ElementaryMvts] SET [label] = 1 WHERE credit_account_id = (select id from accounts where account_number = '2032')
GO
UPDATE [ElementaryMvts] SET [label] = 2 WHERE debit_account_id = (select id from accounts where account_number = '7022')
UPDATE [ElementaryMvts] SET [label] = 2 WHERE credit_account_id = (select id from accounts where account_number = '7022')
GO
UPDATE [ElementaryMvts] SET [label] = 1 WHERE debit_account_id = (select id from accounts where account_number = '2911')
UPDATE [ElementaryMvts] SET [label] = 1 WHERE credit_account_id = (select id from accounts where account_number = '2911')
GO
UPDATE [ElementaryMvts] SET [label] = 1 WHERE debit_account_id = (select id from accounts where account_number = '2921')
UPDATE [ElementaryMvts] SET [label] = 1 WHERE credit_account_id = (select id from accounts where account_number = '2921')
GO


INSERT INTO [GeneralParameters]([key], [value]) VALUES('LATE_DAYS_AFTER_ACCRUAL_CEASES',NULL)
INSERT INTO [Accounts]([account_number], [local_account_number], [label], [balance], [debit_plus], [type_code], [description]) VALUES('2037','11113','Accrued interests receivable',0,1,'ACCRUED_INTERESTS_LOANS',1)
INSERT INTO [Accounts]([account_number], [local_account_number], [label], [balance], [debit_plus], [type_code], [description]) VALUES('2038','11113','Accrued interests on rescheduled loans',0,1,'ACCRUED_INTERESTS_RESCHEDULED_LOANS',1)
GO
/************** VERSION ***********************/
UPDATE [TechnicalParameters] SET [value]='v1.1.8'
GO
