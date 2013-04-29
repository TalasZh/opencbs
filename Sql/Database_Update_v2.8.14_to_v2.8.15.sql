ALTER TABLE dbo.Roles ALTER COLUMN [code] NVARCHAR(256) NOT NULL;
GO
ALTER TABLE dbo.Roles ADD [description] NVARCHAR(2048) DEFAULT '';
GO

UPDATE [dbo].[Roles] SET [description]='Administrator role' WHERE [code] = 'ADMIN'
UPDATE [dbo].[Roles] SET [description]='Cashier role' WHERE [code] = 'CASHI'
UPDATE [dbo].[Roles] SET [description]='Loan officer role' WHERE [code] = 'LOF'
UPDATE [dbo].[Roles] SET [description]='SUPER role' WHERE [code] = 'SUPER'
UPDATE [dbo].[Roles] SET [description]='Visitor role' WHERE [code] = 'VISIT'
GO

SELECT *
INTO #1
FROM CollateralsLinkContracts
WHERE contract_id NOT IN (SELECT id FROM dbo.Contracts)

DELETE FROM CollateralsLinkContracts
WHERE contract_id IN (SELECT contract_id FROM #1)

DROP TABLE #1
GO

SELECT *
INTO #2
FROM CollateralPropertyValues
WHERE contract_collateral_id NOT IN (SELECT id FROM CollateralsLinkContracts)
	
DELETE FROM CollateralPropertyValues
WHERE contract_collateral_id IN (SELECT contract_collateral_id FROM #2)
GO

ALTER TABLE dbo.Users
ALTER COLUMN [role_code] nvarchar(256) NOT NULL;
GO

-- Removing Compulsory Savings
DROP TABLE dbo.CompulsorySavingsContracts
GO

DROP TABLE dbo.CompulsorySavingsProducts
GO

DECLARE @cnstr VARCHAR(100)
SET @cnstr = (SELECT name FROM sysobjects WHERE name LIKE 'DF__Credit__savings%')
EXEC('ALTER TABLE dbo.Credit DROP CONSTRAINT '+ @cnstr)
GO

ALTER TABLE dbo.Credit
	DROP COLUMN savings_is_mandatory
GO

-- Accounting changes
CREATE TABLE [dbo].[ContractEvents_Tmp](
	[id] [int] NOT NULL IDENTITY(1,1),
	[event_type] [nvarchar](4) NOT NULL,
	[contract_id] [int] NOT NULL,
	[event_date] [datetime] NOT NULL,
	[user_id] [int] NOT NULL,
	[is_deleted] [bit] NOT NULL,
	[entry_date] [datetime] NULL,
	[is_exported] [bit] NOT NULL,
 CONSTRAINT [PK_ContractEvents_Tmp] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET IDENTITY_INSERT [ContractEvents_Tmp] ON
INSERT INTO dbo.ContractEvents_Tmp
        ( id ,
          event_type ,
          contract_id ,
          event_date ,
          user_id ,
          is_deleted ,
          entry_date ,
          is_exported
        )
SELECT id ,
          event_type ,
          contract_id ,
          event_date ,
          user_id ,
          is_deleted ,
          entry_date ,
          is_exported
 FROM dbo.ContractEvents
 ORDER BY id
SET IDENTITY_INSERT [ContractEvents_Tmp] OFF
GO

ALTER TABLE dbo.PastDueLoanEvents
	DROP CONSTRAINT FK_PastDueLoanEvents_ContractEvents
GO
ALTER TABLE dbo.RepaymentEvents
	DROP CONSTRAINT FK_RepaymentEvents_ContractEvents
GO
ALTER TABLE dbo.ReschedulingOfALoanEvents
	DROP CONSTRAINT FK_ReschedulingOfALoanEvents_ContractEvents
GO
ALTER TABLE dbo.OverdueEvents
	DROP CONSTRAINT FK_OverdueEvents_ContractEvents
GO
ALTER TABLE dbo.InstallmentHistory
	DROP CONSTRAINT FK_InstallmentHistory_ContractEvents
GO
ALTER TABLE dbo.TrancheEvents
	DROP CONSTRAINT FK_TrancheEvents_ContractEvents
GO
ALTER TABLE dbo.WriteOffEvents
	DROP CONSTRAINT FK_WriteOffEvents_ContractEvents
GO
ALTER TABLE dbo.ProvisionEvents
	DROP CONSTRAINT FK_ProvisionEvents_ContractEvents
GO
ALTER TABLE dbo.LoanDisbursmentEvents
	DROP CONSTRAINT FK_CreditOrigEvents_ContractEvents
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_MovementSet_ContractEvents]') AND parent_object_id = OBJECT_ID(N'[dbo].[MovementSet]'))
ALTER TABLE [dbo].[MovementSet] DROP CONSTRAINT [FK_MovementSet_ContractEvents]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_LoanDisbursmentEvents_ContractEvents]') AND parent_object_id = OBJECT_ID(N'[dbo].[LoanDisbursmentEvents]'))
ALTER TABLE [dbo].[LoanDisbursmentEvents] DROP CONSTRAINT [FK_LoanDisbursmentEvents_ContractEvents]
GO

DROP TABLE dbo.ContractEvents
GO

EXEC sp_rename 'ContractEvents_Tmp', 'ContractEvents'
GO

ALTER TABLE [dbo].[ContractEvents]  WITH NOCHECK ADD  CONSTRAINT [FK_ContractEvents_Contracts] FOREIGN KEY([contract_id])
REFERENCES [dbo].[Contracts] ([id])
GO

ALTER TABLE [dbo].[ContractEvents] CHECK CONSTRAINT [FK_ContractEvents_Contracts]
GO

ALTER TABLE [dbo].[ContractEvents]  WITH NOCHECK ADD  CONSTRAINT [FK_ContractEvents_LoanInterestAccruingEvents] FOREIGN KEY([id])
REFERENCES [dbo].[LoanInterestAccruingEvents] ([id])
NOT FOR REPLICATION 
GO

ALTER TABLE [dbo].[ContractEvents] NOCHECK CONSTRAINT [FK_ContractEvents_LoanInterestAccruingEvents]
GO

ALTER TABLE [dbo].[ContractEvents]  WITH NOCHECK ADD  CONSTRAINT [FK_ContractEvents_Users] FOREIGN KEY([user_id])
REFERENCES [dbo].[Users] ([id])
GO

ALTER TABLE [dbo].[ContractEvents] CHECK CONSTRAINT [FK_ContractEvents_Users]
GO

ALTER TABLE [dbo].[ContractEvents] ADD  DEFAULT (getdate()) FOR [entry_date]
GO

ALTER TABLE [dbo].[ContractEvents] ADD  DEFAULT ((0)) FOR [is_exported]
GO

CREATE TABLE [dbo].[SavingEvents_Tmp](
	[id] [int] NOT NULL IDENTITY(1, 1),
	[user_id] [int] NOT NULL,
	[contract_id] [int] NOT NULL,
	[code] [char](4) NOT NULL,
	[amount] [money] NOT NULL,
	[description] [nvarchar](200) NULL,
	[deleted] [bit] NOT NULL,
	[creation_date] [datetime] NOT NULL,
	[cancelable] [bit] NOT NULL,
	[is_fired] [bit] NOT NULL,
	[related_contract_code] [nvarchar](50) NULL,
	[fees] [money] NULL,
	[is_exported] [bit] NOT NULL,
	[savings_method] [smallint] NULL,
	[pending] [bit] NOT NULL,
 CONSTRAINT [PK_SavingEvents_Tmp] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET IDENTITY_INSERT SavingEvents_Tmp ON
INSERT INTO dbo.SavingEvents_Tmp
        ( id ,
          user_id ,
          contract_id ,
          code ,
          amount ,
          description ,
          deleted ,
          creation_date ,
          cancelable ,
          is_fired ,
          related_contract_code ,
          fees ,
          is_exported ,
          savings_method ,
          pending
        )
SELECT id ,
          user_id ,
          contract_id ,
          code ,
          amount ,
          description ,
          deleted ,
          creation_date ,
          cancelable ,
          is_fired ,
          related_contract_code ,
          fees ,
          is_exported ,
          savings_method ,
          pending
 FROM dbo.SavingEvents
 ORDER BY id
SET IDENTITY_INSERT SavingEvents_Tmp OFF
GO

DROP TABLE dbo.SavingEvents
GO

EXEC sp_rename 'SavingEvents_Tmp', 'SavingEvents'
GO

ALTER TABLE [dbo].[SavingEvents]  WITH CHECK ADD  CONSTRAINT [FK_SavingEvents_SavingContracts] FOREIGN KEY([contract_id])
REFERENCES [dbo].[SavingContracts] ([id])
GO

ALTER TABLE [dbo].[SavingEvents] CHECK CONSTRAINT [FK_SavingEvents_SavingContracts]
GO

ALTER TABLE [dbo].[SavingEvents] ADD  DEFAULT ((0)) FOR [is_exported]
GO

ALTER TABLE [dbo].[SavingEvents] ADD  DEFAULT ((0)) FOR [pending]
GO

-- Tables relations
ALTER TABLE [dbo].[WriteOffEvents]  WITH NOCHECK ADD  CONSTRAINT [FK_WriteOffEvents_ContractEvents] FOREIGN KEY([id])
REFERENCES [dbo].[ContractEvents] ([id])
GO
ALTER TABLE [dbo].[WriteOffEvents] NOCHECK CONSTRAINT [FK_WriteOffEvents_ContractEvents]
GO

ALTER TABLE [dbo].[ProvisionEvents]  WITH CHECK ADD  CONSTRAINT [FK_ProvisionEvents_ContractEvents] FOREIGN KEY([id])
REFERENCES [dbo].[ContractEvents] ([id])
GO
ALTER TABLE [dbo].[ProvisionEvents] CHECK CONSTRAINT [FK_ProvisionEvents_ContractEvents]
GO

ALTER TABLE [dbo].[TrancheEvents]  WITH CHECK ADD  CONSTRAINT [FK_TrancheEvents_ContractEvents] FOREIGN KEY([id])
REFERENCES [dbo].[ContractEvents] ([id])
GO
ALTER TABLE [dbo].[TrancheEvents] CHECK CONSTRAINT [FK_TrancheEvents_ContractEvents]
GO

ALTER TABLE [dbo].[InstallmentHistory]  WITH CHECK ADD  CONSTRAINT [FK_InstallmentHistory_ContractEvents] FOREIGN KEY([event_id])
REFERENCES [dbo].[ContractEvents] ([id])
GO
ALTER TABLE [dbo].[InstallmentHistory] CHECK CONSTRAINT [FK_InstallmentHistory_ContractEvents]
GO

ALTER TABLE [dbo].[OverdueEvents]  WITH CHECK ADD  CONSTRAINT [FK_OverdueEvents_ContractEvents] FOREIGN KEY([id])
REFERENCES [dbo].[ContractEvents] ([id])
GO
ALTER TABLE [dbo].[OverdueEvents] CHECK CONSTRAINT [FK_OverdueEvents_ContractEvents]
GO

ALTER TABLE [dbo].[ReschedulingOfALoanEvents]  WITH NOCHECK ADD  CONSTRAINT [FK_ReschedulingOfALoanEvents_ContractEvents] FOREIGN KEY([id])
REFERENCES [dbo].[ContractEvents] ([id])
GO
ALTER TABLE [dbo].[ReschedulingOfALoanEvents] NOCHECK CONSTRAINT [FK_ReschedulingOfALoanEvents_ContractEvents]
GO

ALTER TABLE [dbo].[RepaymentEvents]  WITH NOCHECK ADD  CONSTRAINT [FK_RepaymentEvents_ContractEvents] FOREIGN KEY([id])
REFERENCES [dbo].[ContractEvents] ([id])
GO
ALTER TABLE [dbo].[RepaymentEvents] NOCHECK CONSTRAINT [FK_RepaymentEvents_ContractEvents]
GO

ALTER TABLE [dbo].[LoanDisbursmentEvents]  WITH NOCHECK ADD  CONSTRAINT [FK_LoanDisbursmentEvents_ContractEvents] FOREIGN KEY([id])
REFERENCES [dbo].[ContractEvents] ([id])
GO
ALTER TABLE [dbo].[LoanDisbursmentEvents] CHECK CONSTRAINT [FK_LoanDisbursmentEvents_ContractEvents]
GO
ALTER TABLE dbo.UsersSubordinates ADD CONSTRAINT
	FK_UsersSubordinates_Users FOREIGN KEY
	(
	user_id
	) REFERENCES dbo.Users
	(
	id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	 
	 ALTER TABLE dbo.VillagesPersons ADD CONSTRAINT
	FK_VillagesPersons_Villages FOREIGN KEY
	(
	village_id
	) REFERENCES dbo.Villages
	(
	id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	 
ALTER TABLE dbo.AllowedRoleActions ADD CONSTRAINT
	FK_AllowedRoleActions_Roles FOREIGN KEY
	(
	role_id
	) REFERENCES dbo.Roles
	(
	id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	 
ALTER TABLE dbo.AllowedRoleMenus ADD CONSTRAINT
	FK_AllowedRoleMenus_Roles FOREIGN KEY
	(
	role_id
	) REFERENCES dbo.Roles
	(
	id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
GO
	 
ALTER TABLE dbo.AllowedRoleActions ADD CONSTRAINT
	FK_AllowedRoleActions_ActionItems FOREIGN KEY
	(
	action_item_id
	) REFERENCES dbo.ActionItems
	(
	id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
	
ALTER TABLE dbo.StandardBookings ADD CONSTRAINT
	PK_StandardBookings PRIMARY KEY CLUSTERED 
	(
	Id
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.StandardBookings ADD CONSTRAINT
	FK_StandardBookings_ChartOfAccounts FOREIGN KEY
	(
	debit_account_id
	) REFERENCES dbo.ChartOfAccounts
	(
	id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 	
GO
ALTER TABLE dbo.StandardBookings ADD CONSTRAINT
	FK_StandardBookings_ChartOfAccounts1 FOREIGN KEY
	(
	credit_account_id
	) REFERENCES dbo.ChartOfAccounts
	(
	id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO

ALTER TABLE dbo.CollateralsLinkContracts ADD CONSTRAINT
	FK_CollateralsLinkContracts_Contracts FOREIGN KEY
	(
	contract_id
	) REFERENCES dbo.Contracts
	(
	id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO

ALTER TABLE dbo.CollateralProperties ADD CONSTRAINT
	FK_CollateralProperties_CollateralProducts FOREIGN KEY
	(
	product_id
	) REFERENCES dbo.CollateralProducts
	(
	id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.CollateralProperties ADD CONSTRAINT
	FK_CollateralProperties_CollateralPropertyTypes FOREIGN KEY
	(
	type_id
	) REFERENCES dbo.CollateralPropertyTypes
	(
	id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.CollateralPropertyCollections ADD CONSTRAINT
	FK_CollateralPropertyCollections_CollateralProperties FOREIGN KEY
	(
	property_id
	) REFERENCES dbo.CollateralProperties
	(
	id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.CollateralPropertyValues ADD CONSTRAINT
	FK_CollateralPropertyValues_CollateralProperties FOREIGN KEY
	(
	property_id
	) REFERENCES dbo.CollateralProperties
	(
	id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.CollateralPropertyValues ADD CONSTRAINT
	FK_CollateralPropertyValues_CollateralsLinkContracts FOREIGN KEY
	(
	contract_collateral_id
	) REFERENCES dbo.CollateralsLinkContracts
	(
	id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.LoanAccountingMovements ADD CONSTRAINT
	FK_LoanAccountingMovements_ChartOfAccounts FOREIGN KEY
	(
	debit_account_number_id
	) REFERENCES dbo.ChartOfAccounts
	(
	id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.LoanAccountingMovements ADD CONSTRAINT
	FK_LoanAccountingMovements_ChartOfAccounts1 FOREIGN KEY
	(
	credit_account_number_id
	) REFERENCES dbo.ChartOfAccounts
	(
	id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.SavingsAccountingMovements ADD CONSTRAINT
	FK_SavingsAccountingMovements_ChartOfAccounts FOREIGN KEY
	(
	debit_account_number_id
	) REFERENCES dbo.ChartOfAccounts
	(
	id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.SavingsAccountingMovements ADD CONSTRAINT
	FK_SavingsAccountingMovements_ChartOfAccounts1 FOREIGN KEY
	(
	credit_account_number_id
	) REFERENCES dbo.ChartOfAccounts
	(
	id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.ManualAccountingMovements ADD CONSTRAINT
	FK_ManualAccountingMovements_ChartOfAccounts FOREIGN KEY
	(
	debit_account_number_id
	) REFERENCES dbo.ChartOfAccounts
	(
	id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.ManualAccountingMovements ADD CONSTRAINT
	FK_ManualAccountingMovements_ChartOfAccounts1 FOREIGN KEY
	(
	credit_account_number_id
	) REFERENCES dbo.ChartOfAccounts
	(
	id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO

-- drop obsolet tables
DROP TABLE [Events]
GO

DROP TABLE StatisticalProvisoningEvents
GO

DROP TABLE dbo.ElementaryMvts
GO

DROP TABLE dbo.MovementSet
GO

DROP TABLE SavingAccountsBalance
GO

DROP TABLE dbo.Accounts
GO

INSERT INTO [dbo].[ActionItems] ([class_name],[method_name]) VALUES ('LoanServices','WriteOff')
GO

DECLARE @id INT
SELECT @id = id
FROM dbo.ActionItems
WHERE method_name = 'WriteOff'

INSERT INTO [dbo].[AllowedRoleActions] ([action_item_id],[role_id] ,[allowed])
VALUES (@id, 1, 'false')
INSERT INTO [dbo].[AllowedRoleActions] ([action_item_id],[role_id] ,[allowed])
VALUES (@id, 2, 'false')
INSERT INTO [dbo].[AllowedRoleActions] ([action_item_id],[role_id] ,[allowed])
VALUES (@id, 3, 'false')
INSERT INTO [dbo].[AllowedRoleActions] ([action_item_id],[role_id] ,[allowed])
VALUES (@id, 5, 'false')
GO

DELETE FROM dbo.EventAttributes
WHERE event_type = 'RRLE'
GO

INSERT INTO EventAttributes (event_type, name) VALUES('RRLE', 'principal')
INSERT INTO EventAttributes (event_type, name) VALUES('RRLE', 'penalties')
INSERT INTO EventAttributes (event_type, name) VALUES('RRLE', 'commissions')
INSERT INTO EventAttributes (event_type, name) VALUES('RRLE', 'interests')
GO

INSERT INTO EventAttributes (event_type, name) VALUES('ROWE', 'principal')
INSERT INTO EventAttributes (event_type, name) VALUES('ROWE', 'penalties')
INSERT INTO EventAttributes (event_type, name) VALUES('ROWE', 'commissions')
INSERT INTO EventAttributes (event_type, name) VALUES('ROWE', 'interests')
GO

CREATE TABLE dbo.Branches (
	id INT IDENTITY(1,1) NOT NULL
	, name NVARCHAR(100)
	, CONSTRAINT PK_Branches PRIMARY KEY CLUSTERED 
	(
		id ASC
	) WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

INSERT INTO dbo.Branches (name)
VALUES ('Default')
GO

ALTER TABLE dbo.Tiers
ADD branch_id INT NOT NULL DEFAULT(1)
GO

ALTER TABLE dbo.Tiers  WITH CHECK ADD  CONSTRAINT FK_Tiers_Branches FOREIGN KEY(branch_id)
REFERENCES dbo.Branches (id)
GO

ALTER TABLE dbo.Roles
ADD role_of_loan BIT DEFAULT(0),
  role_of_saving BIT DEFAULT(0)
GO

UPDATE dbo.Roles
SET role_of_loan = 1,
  role_of_saving = 1
GO

ALTER TABLE dbo.Contracts
ADD branch_id INT NOT NULL DEFAULT(1)
GO

ALTER TABLE dbo.Contracts WITH CHECK ADD  CONSTRAINT FK_Contracts_Branches FOREIGN KEY(branch_id)
REFERENCES dbo.Branches (id)
GO

CREATE TABLE [dbo].[EntryFees](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[id_product] [int] NOT NULL,
	[name_of_fee] [nvarchar](100) NOT NULL,
	[min] [decimal] (18, 4) NULL,
	[max] [decimal] (18, 4) NULL,
	[value] [decimal] (18, 4) NULL,
	[rate] [bit] NULL,
	[is_deleted] [bit] NOT NULL,
 CONSTRAINT [PK_EntryFees] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[EntryFees]  WITH CHECK ADD  CONSTRAINT [FK_EntryFees_Packages] FOREIGN KEY([id_product])
REFERENCES [dbo].[Packages] ([id])
GO

ALTER TABLE [dbo].[EntryFees] CHECK CONSTRAINT [FK_EntryFees_Packages]
GO

ALTER TABLE [dbo].[EntryFees] ADD  CONSTRAINT [DF_EntryFees_is_deleted]  DEFAULT ((0)) FOR [is_deleted]
GO

DECLARE EntryFeesCursor CURSOR FOR
SELECT id
, CASE [entry_fees_percentage] WHEN 1 THEN entry_fees*100 END 
, CASE [entry_fees_percentage] WHEN 1 THEN  entry_fees_min*100 END
, CASE [entry_fees_percentage] WHEN 1 THEN entry_fees_max*100 END
, [entry_fees_percentage]
FROM [dbo].[Packages]

DECLARE @id INT, @entry_fees FLOAT, @entry_fees_min FLOAT, @entry_fees_max FLOAT, @is_rate BIT
OPEN EntryFeesCursor
FETCH NEXT FROM EntryFeesCursor INTO
@id, @entry_fees, @entry_fees_min, @entry_fees_max,   @is_rate
WHILE @@FETCH_STATUS=0
BEGIN
	INSERT INTO EntryFees (id_product, name_of_fee, [min], [max], [value], [rate])
	VALUES 	(@id, 'Entry fee', @entry_fees_min, @entry_fees_max, @entry_fees, @is_rate)
	FETCH NEXT FROM EntryFeesCursor INTO
	@id, @entry_fees, @entry_fees_min, @entry_fees_max, @is_rate
END
CLOSE EntryFeesCursor
DEALLOCATE EntryFeesCursor

CREATE TABLE [dbo].[CreditEntryFees](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[credit_id] [int] NOT NULL,
	[entry_fee_id] [int] NOT NULL,
	[fee_value] [decimal](18, 4) NOT NULL,
 CONSTRAINT [PK_CreditEntryFees] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[CreditEntryFees]  WITH CHECK ADD  CONSTRAINT [FK_CreditEntryFees_Credit] FOREIGN KEY([credit_id])
REFERENCES [dbo].[Credit] ([id])
GO

ALTER TABLE [dbo].[CreditEntryFees] CHECK CONSTRAINT [FK_CreditEntryFees_Credit]
GO

ALTER TABLE [dbo].[CreditEntryFees]  WITH CHECK ADD  CONSTRAINT [FK_CreditEntryFees_EntryFees] FOREIGN KEY([entry_fee_id])
REFERENCES [dbo].[EntryFees] ([id])
GO

ALTER TABLE [dbo].[CreditEntryFees] CHECK CONSTRAINT [FK_CreditEntryFees_EntryFees]
GO

INSERT INTO [dbo].[EventTypes]([event_type],[description],[sort_order]) values('UMEE','Manual accounting entry event',440)
INSERT INTO [dbo].[EventTypes]([event_type],[description],[sort_order]) values('USBE','Manual standard booking event',450)
INSERT INTO dbo.EventTypes (event_type, description, sort_order) VALUES ('SVDC', 'Saving cheque deposit event',460)
INSERT INTO dbo.EventTypes (event_type, description, sort_order) VALUES ('SPDR', 'Saving pending deposit refused event',470)
GO

INSERT INTO EventAttributes (event_type, name) VALUES ('SVDC', 'amount')
INSERT INTO EventAttributes (event_type, name) VALUES ('SPDR', 'amount')
GO

INSERT INTO EventAttributes (event_type, name) VALUES ('SVDC', 'fees')
INSERT INTO EventAttributes (event_type, name) VALUES ('SPDR', 'fees')
GO

ALTER TABLE dbo.SavingEvents
ADD [pending_event_id] [int] NULL
GO

ALTER TABLE dbo.LoanDisbursmentEvents
DROP CONSTRAINT PK_EventTbl
GO

DECLARE EntryFeesCursor CURSOR FOR
SELECT Credit.id AS credit_id,
EntryFees.id AS entry_fee_id,
entry_fees_value = 
CASE Packages.entry_fees_percentage
	 WHEN NULL THEN Credit.entry_fees
	 WHEN 0 THEN Credit.entry_fees
	 WHEN 1 THEN Credit.entry_fees*100
 END

FROM dbo.Credit
INNER JOIN dbo.Packages ON Credit.package_id=Packages.id
INNER JOIN dbo.EntryFees ON EntryFees.id_product=Packages.id

DECLARE @credit_id INT, @entry_fees_value MONEY, @entry_fee_id INT

OPEN EntryFeesCursor
FETCH NEXT FROM EntryFeesCursor INTO 
@credit_id, @entry_fee_id,  @entry_fees_value
WHILE @@FETCH_STATUS=0
BEGIN
	INSERT INTO CreditEntryFees ([credit_id], [entry_fee_id], [fee_value])
	VALUES 	(@credit_id, @entry_fee_id,  @entry_fees_value)
	FETCH NEXT FROM EntryFeesCursor INTO
	@credit_id, @entry_fee_id,  @entry_fees_value
END
CLOSE EntryFeesCursor
DEALLOCATE EntryFeesCursor
GO

ALTER TABLE dbo.ManualAccountingMovements
ADD [user_id] INT,
    [event_id] INT
GO

UPDATE ManualAccountingMovements
SET [user_id] = (SELECT TOP 1 id FROM dbo.Users WHERE role_code = 'SUPER')
GO

ALTER TABLE dbo.ManualAccountingMovements
ALTER COLUMN [user_id] INT NOT NULL
GO

UPDATE dbo.Contracts
SET [status] = 5
WHERE id in (
	SELECT al.id
	FROM dbo.ActiveLoans(getdate()) al
	LEFT JOIN dbo.Contracts c on c.id = al.id
	WHERE c.[status] = 2
)
GO

INSERT INTO [dbo].[ActionItems]
([class_name], [method_name])
VALUES ('SavingServices', 'AllowSettingSavingsOperationsFeesManually')
GO

INSERT INTO dbo.EventTypes (event_type, description, sort_order) VALUES ('SOCE', 'Savings special operation credit', 480)
INSERT INTO dbo.EventTypes (event_type, description, sort_order) VALUES ('SODE', 'Savings special operation dredit', 490)
GO

INSERT INTO [dbo].[ActionItems] ([class_name],[method_name]) VALUES ('SavingServices','SpecialOperation')
GO

DECLARE @id INT
SELECT @id = id
FROM dbo.ActionItems
WHERE method_name = 'SpecialOperation'

INSERT INTO [dbo].[AllowedRoleActions] ([action_item_id],[role_id] ,[allowed])
VALUES (@id, 1, 'false')
INSERT INTO [dbo].[AllowedRoleActions] ([action_item_id],[role_id] ,[allowed])
VALUES (@id, 2, 'false')
INSERT INTO [dbo].[AllowedRoleActions] ([action_item_id],[role_id] ,[allowed])
VALUES (@id, 3, 'false')
INSERT INTO [dbo].[AllowedRoleActions] ([action_item_id],[role_id] ,[allowed])
VALUES (@id, 5, 'false')
GO

ALTER TABLE dbo.Projects
ALTER COLUMN code NVARCHAR(255) NOT NULL
GO

ALTER TABLE dbo.Contracts
ALTER COLUMN contract_code NVARCHAR(255) NOT NULL
GO

ALTER TABLE dbo.CollateralProperties ALTER COLUMN [name] NVARCHAR(100) NOT NULL
ALTER TABLE dbo.CollateralProperties ALTER COLUMN [desc] NVARCHAR(1000) NOT NULL
ALTER TABLE dbo.CollateralProducts ALTER COLUMN [desc] NVARCHAR(1000) NOT NULL
GO

ALTER TABLE dbo.PersonCustomizableFields ALTER COLUMN [value] NVARCHAR(1000) NULL
GO

UPDATE [dbo].[Tiers] SET active = 1
WHERE id IN (
	SELECT COALESCE(pe.id,pe2.id,corp.id) AS id
	FROM Contracts co
	INNER JOIN Projects pr ON pr.id = co.project_id
	INNER JOIN Tiers ti ON ti.id = pr.tiers_id
	LEFT JOIN Persons pe ON pe.id = ti.id
	LEFT JOIN LoanShareAmounts lsa ON lsa.group_id = ti.id AND lsa.contract_id = co.id
	LEFT JOIN Persons pe2 ON lsa.person_id = pe2.id 
	LEFT JOIN Corporates corp ON corp.id = ti.id
	WHERE co.closed = 0 AND co.status = 5 ) AND active = 0
GO 
	
UPDATE LoanDisbursmentEvents SET fees=EntryFees.fee
FROM
(
	SELECT LDE.id, CASE Currencies.use_cents WHEN 1 THEN ROUND(LDE.fees, 2)
	ELSE ROUND(LDE.fees, 0) 
	END AS fee

	FROM LoanDisbursmentEvents AS LDE
	INNER JOIN [dbo].[ContractEvents] ON LDE.id=ContractEvents.id
	INNER JOIN [dbo].Credit ON Credit.id=ContractEvents.contract_id
	INNER JOIN [dbo].Packages ON Credit.package_id=Packages.id
	INNER JOIN [dbo].Currencies ON Packages.currency_id=Currencies.id
) EntryFees
WHERE LoanDisbursmentEvents.id = EntryFees.id
GO

ALTER TABLE dbo.Questionnaire 
ADD [PersonName] NVARCHAR(200) NULL,
	[Phone] NVARCHAR(200) NULL,
	[Skype] NVARCHAR(200) NULL,
	[PurposeOfUsage] NVARCHAR(200) NULL
GO

ALTER TABLE dbo.Questionnaire
ALTER COLUMN [Name] [nvarchar](256) NULL
GO

ALTER TABLE dbo.Questionnaire
ALTER COLUMN [Country] [nvarchar](50) NULL
GO

ALTER TABLE dbo.Questionnaire
ALTER COLUMN [Email] [nvarchar](100) NULL
GO

ALTER TABLE dbo.Questionnaire
DROP COLUMN [BeContacted], [FirstTime], [DailyActivity], [MainPriorities], [MainAdvantages]
GO

DELETE FROM GeneralParameters
WHERE [key] = 'SENT_QUESTIONNAIRE'

UPDATE [TechnicalParameters] SET [value] = 'v2.8.15' WHERE [name] = 'VERSION'
GO
