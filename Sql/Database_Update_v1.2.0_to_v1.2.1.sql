ALTER TABLE dbo.Persons ADD
	mother_name nvarchar(200) NULL
GO

ALTER TABLE dbo.Persons
	DROP COLUMN [sponsor]
GO

ALTER TABLE dbo.Tiers ADD
	[other_org_comment][nvarchar](500) NULL,
	[sponsor] [nvarchar](50) NULL
GO

DELETE FROM dbo.TempCashReceipt
GO

ALTER TABLE dbo.TempCashReceipt
ALTER COLUMN [paid_date] [datetime] NULL

GO

ALTER TABLE dbo.City ADD
	[deleted][bit] NOT NULL DEFAULT (0),
	[id] [int] IDENTITY(1,1) NOT NULL
GO

ALTER TABLE dbo.Districts ADD
	[deleted][bit] NOT NULL DEFAULT (0)
GO

ALTER TABLE dbo.Provinces ADD
	[deleted][bit] NOT NULL DEFAULT (0)
GO

--Making identification_data column nullable
ALTER TABLE dbo.Persons
	DROP CONSTRAINT FK_Persons_Tiers
GO

ALTER TABLE dbo.Persons
	DROP CONSTRAINT FK_Persons_DomainOfApplications
GO

ALTER TABLE dbo.Persons
	DROP CONSTRAINT DF_Credit_handicapped
GO
CREATE TABLE dbo.Tmp_Persons
	(
	id int NOT NULL,
	first_name nvarchar(100) NOT NULL,
	sex char(1) NOT NULL,
	identification_data nvarchar(200) NULL,
	last_name nvarchar(100) NOT NULL,
	birth_date datetime NULL,
	household_head bit NOT NULL,
	nb_of_dependents int NULL,
	nb_of_children int NULL,
	children_basic_education int NULL,
	livestock_number int NULL,
	livestock_type nvarchar(100) NULL,
	landplot_size float(53) NULL,
	home_size float(53) NULL,
	home_time_living_in int NULL,
	capital_other_equipments nvarchar(500) NULL,
	activity_id int NULL,
	experience int NULL,
	nb_of_people int NULL,
	monthly_income money NULL,
	monthly_expenditure money NULL,
	comments nvarchar(500) NULL,
	image_path nvarchar(500) NULL,
	father_name nvarchar(200) NULL,
	birth_place nvarchar(50) NULL,
	nationality nvarchar(50) NULL,
	IBAN nvarchar(50) NULL,
	study_level nvarchar(50) NULL,
	SS nvarchar(50) NULL,
	CAF nvarchar(50) NULL,
	housing_situation nvarchar(50) NULL,
	bank_address nvarchar(50) NULL,
	handicapped bit NOT NULL,
	professional_experience nvarchar(50) NULL,
	professional_situation nvarchar(50) NULL,
	first_contact datetime NULL,
	family_situation nvarchar(50) NULL,
	mother_name nvarchar(200) NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.Tmp_Persons ADD CONSTRAINT
	DF_Credit_handicapped DEFAULT ((0)) FOR handicapped
GO
IF EXISTS(SELECT * FROM dbo.Persons)
	 EXEC('INSERT INTO dbo.Tmp_Persons (id, first_name, sex, identification_data, last_name, birth_date, household_head, nb_of_dependents, nb_of_children, children_basic_education, livestock_number, livestock_type, landplot_size, home_size, home_time_living_in, capital_other_equipments, activity_id, experience, nb_of_people, monthly_income, monthly_expenditure, comments, image_path, father_name, birth_place, nationality, IBAN, study_level, SS, CAF, housing_situation, bank_address, handicapped, professional_experience, professional_situation, first_contact, family_situation, mother_name)
		SELECT id, first_name, sex, identification_data, last_name, birth_date, household_head, nb_of_dependents, nb_of_children, children_basic_education, livestock_number, livestock_type, landplot_size, home_size, home_time_living_in, capital_other_equipments, activity_id, experience, nb_of_people, monthly_income, monthly_expenditure, comments, image_path, father_name, birth_place, nationality, IBAN, study_level, SS, CAF, housing_situation, bank_address, handicapped, professional_experience, professional_situation, first_contact, family_situation, mother_name FROM dbo.Persons WITH (HOLDLOCK TABLOCKX)')
GO
ALTER TABLE dbo.CorporatePersonBelonging
	DROP CONSTRAINT FK_CorporatePersonBelonging_Persons
GO
ALTER TABLE dbo.PersonGroupBelonging
	DROP CONSTRAINT FK_PersonGroupBelonging_Persons1
GO
DROP TABLE dbo.Persons
GO
EXECUTE sp_rename N'dbo.Tmp_Persons', N'Persons', 'OBJECT' 
GO
ALTER TABLE dbo.Persons ADD CONSTRAINT
	PK_Persons PRIMARY KEY CLUSTERED 
	(
	id
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.Persons ADD CONSTRAINT
	IX_Persons_personal_address_id UNIQUE NONCLUSTERED 
	(
	id
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.Persons WITH NOCHECK ADD CONSTRAINT
	FK_Persons_DomainOfApplications FOREIGN KEY
	(
	activity_id
	) REFERENCES dbo.DomainOfApplications
	(
	id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.Persons WITH NOCHECK ADD CONSTRAINT
	FK_Persons_Tiers FOREIGN KEY
	(
	id
	) REFERENCES dbo.Tiers
	(
	id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.Persons
	NOCHECK CONSTRAINT FK_Persons_Tiers
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

ALTER TABLE dbo.CorporatePersonBelonging ADD CONSTRAINT
	FK_CorporatePersonBelonging_Persons FOREIGN KEY
	(
	person_id
	) REFERENCES dbo.Persons
	(
	id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
-- making identification_data column nullable finished
ALTER TABLE dbo.City ADD CONSTRAINT
	PK_City PRIMARY KEY CLUSTERED 
	(
	id
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.GuaranteesPackages
	DROP CONSTRAINT FK_GaranteesPackages_FundingLines
GO
ALTER TABLE dbo.GuaranteesPackages
	DROP CONSTRAINT FK_GaranteesPackages_Corporates
GO
CREATE TABLE dbo.Tmp_GuaranteesPackages
	(
	id int NOT NULL IDENTITY (1, 1),
	deleted bit NOT NULL,
	name nvarchar(100) NULL,
	client_type char(1) NULL,
	amount money NULL,
	amount_min money NULL,
	amount_max money NULL,
	amount_limit money NULL,
	amount_limit_min money NULL,
	amount_limit_max money NULL,
	rate float(53) NULL,
	rate_min float(53) NULL,
	rate_max float(53) NULL,
	rate_limit float(53) NULL,
	rate_limit_min float(53) NULL,
	rate_limit_max float(53) NULL,
	guaranted_amount money NULL,
	guaranted_amount_min money NULL,
	guaranted_amount_max money NULL,
	guarantee_fees float(53) NULL,
	guarantee_fees_min float(53) NULL,
	guarantee_fees_max float(53) NULL,
	fundingLine_id int NULL,
	corporate_id int NULL
	)  ON [PRIMARY]
GO
SET IDENTITY_INSERT dbo.Tmp_GuaranteesPackages ON
GO
IF EXISTS(SELECT * FROM dbo.GuaranteesPackages)
	 EXEC('INSERT INTO dbo.Tmp_GuaranteesPackages (id, deleted, name, client_type, amount, amount_min, amount_max, amount_limit, amount_limit_min, amount_limit_max, rate, rate_min, rate_max, rate_limit, rate_limit_min, rate_limit_max, guaranted_amount, guaranted_amount_min, guaranted_amount_max, guarantee_fees, guarantee_fees_min, guarantee_fees_max, fundingLine_id, corporate_id)
		SELECT id, deleted, name, client_type, amount, amount_min, amount_max, amount_limit, amount_limit_min, amount_limit_max, rate, rate_min, rate_max, rate_limit, rate_limit_min, rate_limit_max, guaranted_amount, guaranted_amount_min, guaranted_amount_max, guarantee_fees, guarantee_fees_min, guarantee_fees_max, fundingLine_id, corporate_id FROM dbo.GuaranteesPackages WITH (HOLDLOCK TABLOCKX)')
GO
SET IDENTITY_INSERT dbo.Tmp_GuaranteesPackages OFF
GO
ALTER TABLE dbo.Guarantees
	DROP CONSTRAINT FK_Garantees_GaranteesPackages
GO
DROP TABLE dbo.GuaranteesPackages
GO
EXECUTE sp_rename N'dbo.Tmp_GuaranteesPackages', N'GuaranteesPackages', 'OBJECT' 
GO
ALTER TABLE dbo.GuaranteesPackages ADD CONSTRAINT
	PK_GaranteesPackages PRIMARY KEY CLUSTERED 
	(
	id
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.GuaranteesPackages ADD CONSTRAINT
	FK_GaranteesPackages_Corporates FOREIGN KEY
	(
	corporate_id
	) REFERENCES dbo.Corporates
	(
	id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.GuaranteesPackages ADD CONSTRAINT
	FK_GaranteesPackages_FundingLines FOREIGN KEY
	(
	fundingLine_id
	) REFERENCES dbo.FundingLines
	(
	id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
GO
ALTER TABLE dbo.Guarantees ADD CONSTRAINT
	FK_Garantees_GaranteesPackages FOREIGN KEY
	(
	guarantee_package_id
	) REFERENCES dbo.GuaranteesPackages
	(
	id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO

INSERT INTO [GeneralParameters]([key], [value]) VALUES('CALCULATION_LATE_FEES_DURING_PUBLIC_HOLIDAYS',1)
GO


--Making [keep_expected_installment] [charge_interest_within_grace_period]  column nullable
ALTER TABLE dbo.Packages
	DROP CONSTRAINT FK_Packages_Corporates


GO
ALTER TABLE dbo.Packages
	DROP CONSTRAINT FK_Packages_Cycles
GO

ALTER TABLE dbo.Packages
	DROP CONSTRAINT FK_Packages_InstallmentTypes
GO

ALTER TABLE dbo.Packages
	DROP CONSTRAINT FK_Packages_Exotics
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[DF_Packages_client_type]') AND parent_object_id = OBJECT_ID(N'[dbo].[Packages]'))
ALTER TABLE dbo.Packages DROP CONSTRAINT DF_Packages_delete
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[DF_Packages_client_type]') AND parent_object_id = OBJECT_ID(N'[dbo].[Packages]'))
ALTER TABLE dbo.Packages DROP CONSTRAINT DF_Packages_client_type
GO

CREATE TABLE dbo.Tmp_Packages
	(
	id int NOT NULL IDENTITY (1, 1),
	deleted bit NOT NULL DEFAULT ((0)),
	name nvarchar(100) NOT NULL,
	client_type char(1) NULL DEFAULT ('-'),
	amount money NULL,
	amount_min money NULL,
	amount_max money NULL,
	interest_rate float(53) NULL,
	interest_rate_min float(53) NULL,
	interest_rate_max float(53) NULL,
	installment_type int NOT NULL,
	grace_period int NULL,
	grace_period_min int NULL,
	grace_period_max int NULL,
	number_of_installments int NULL,
	number_of_installments_min int NULL,
	number_of_installments_max int NULL,
	anticipated_total_repayment_penalties float(53) NULL,
	anticipated_total_repayment_penalties_min float(53) NULL,
	anticipated_total_repayment_penalties_max float(53) NULL,
	exotic_id int NULL,
	entry_fees float(53) NULL,
	entry_fees_min float(53) NULL,
	entry_fees_max float(53) NULL,
	loan_type smallint NOT NULL,
	keep_expected_installment bit NOT NULL,
	charge_interest_within_grace_period bit NOT NULL,
	anticipated_repayment_base smallint NOT NULL,
	cycle_id int NULL,
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
	non_repayment_penalties_based_on_overdue_principal_max float(53) NULL,
	fundingLine_id int NULL,
	corporate_id int NULL,
	fake bit NOT NULL DEFAULT ((0))
	)  ON [PRIMARY]
GO
SET IDENTITY_INSERT dbo.Tmp_Packages ON
GO
IF EXISTS(SELECT * FROM dbo.Packages)
	 EXEC('INSERT INTO dbo.Tmp_Packages (id, deleted, name, client_type, amount, amount_min, amount_max, interest_rate, interest_rate_min, interest_rate_max, installment_type, grace_period, grace_period_min, grace_period_max, number_of_installments, number_of_installments_min, number_of_installments_max, anticipated_total_repayment_penalties, anticipated_total_repayment_penalties_min, anticipated_total_repayment_penalties_max, exotic_id, entry_fees, entry_fees_min, entry_fees_max, loan_type, keep_expected_installment, charge_interest_within_grace_period, anticipated_repayment_base, cycle_id, non_repayment_penalties_based_on_overdue_interest, non_repayment_penalties_based_on_initial_amount, non_repayment_penalties_based_on_olb, non_repayment_penalties_based_on_overdue_principal, non_repayment_penalties_based_on_overdue_interest_min, non_repayment_penalties_based_on_initial_amount_min, non_repayment_penalties_based_on_olb_min, non_repayment_penalties_based_on_overdue_principal_min, non_repayment_penalties_based_on_overdue_interest_max, non_repayment_penalties_based_on_initial_amount_max, non_repayment_penalties_based_on_olb_max, non_repayment_penalties_based_on_overdue_principal_max, fundingLine_id, corporate_id, fake)
		SELECT id, deleted, name, client_type, amount, amount_min, amount_max, interest_rate, interest_rate_min, interest_rate_max, installment_type, grace_period, grace_period_min, grace_period_max, number_of_installments, number_of_installments_min, number_of_installments_max, anticipated_total_repayment_penalties, anticipated_total_repayment_penalties_min, anticipated_total_repayment_penalties_max, exotic_id, entry_fees, entry_fees_min, entry_fees_max, loan_type, keep_expected_installment, charge_interest_within_grace_period, anticipated_repayment_base, cycle_id, non_repayment_penalties_based_on_overdue_interest, non_repayment_penalties_based_on_initial_amount, non_repayment_penalties_based_on_olb, non_repayment_penalties_based_on_overdue_principal, non_repayment_penalties_based_on_overdue_interest_min, non_repayment_penalties_based_on_initial_amount_min, non_repayment_penalties_based_on_olb_min, non_repayment_penalties_based_on_overdue_principal_min, non_repayment_penalties_based_on_overdue_interest_max, non_repayment_penalties_based_on_initial_amount_max, non_repayment_penalties_based_on_olb_max, non_repayment_penalties_based_on_overdue_principal_max, fundingLine_id, corporate_id, fake FROM dbo.Packages WITH (HOLDLOCK TABLOCKX)')
GO
SET IDENTITY_INSERT dbo.Tmp_Packages OFF
GO
ALTER TABLE dbo.Credit
	DROP CONSTRAINT FK_Credit_Packages
GO
DROP TABLE dbo.Packages
GO
EXECUTE sp_rename N'dbo.Tmp_Packages', N'Packages', 'OBJECT' 
GO
ALTER TABLE dbo.Packages ADD CONSTRAINT
	PK_Packages PRIMARY KEY CLUSTERED 
	(
	id
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
CREATE NONCLUSTERED INDEX IX_Packages_installmentTypeId ON dbo.Packages
	(
	installment_type
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX IX_Packages_exoticsId ON dbo.Packages
	(
	exotic_id
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE dbo.Packages ADD CONSTRAINT
	IX_Packages_name UNIQUE NONCLUSTERED 
	(
	name
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.Packages WITH NOCHECK ADD CONSTRAINT
	CK_Packages CHECK NOT FOR REPLICATION (([client_type]='I' OR [client_type]='G' OR [client_type]='-' OR [client_type]='C'))
GO
ALTER TABLE dbo.Packages WITH NOCHECK ADD CONSTRAINT
	FK_Packages_Exotics FOREIGN KEY
	(
	exotic_id
	) REFERENCES dbo.Exotics
	(
	id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.Packages
	NOCHECK CONSTRAINT FK_Packages_Exotics
GO
ALTER TABLE dbo.Packages WITH NOCHECK ADD CONSTRAINT
	FK_Packages_InstallmentTypes FOREIGN KEY
	(
	installment_type
	) REFERENCES dbo.InstallmentTypes
	(
	id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.Packages
	NOCHECK CONSTRAINT FK_Packages_InstallmentTypes
GO
ALTER TABLE dbo.Packages WITH NOCHECK ADD CONSTRAINT
	FK_Packages_Cycles FOREIGN KEY
	(
	cycle_id
	) REFERENCES dbo.Cycles
	(
	id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.Packages
	NOCHECK CONSTRAINT FK_Packages_Cycles
GO
ALTER TABLE dbo.Packages WITH NOCHECK ADD CONSTRAINT
	FK_Packages_Corporates FOREIGN KEY
	(
	corporate_id
	) REFERENCES dbo.Corporates
	(
	id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO

ALTER TABLE dbo.Credit WITH NOCHECK ADD CONSTRAINT
	FK_Credit_Packages FOREIGN KEY
	(
	package_id
	) REFERENCES dbo.Packages
	(
	id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
-- making [keep_expected_installment] [charge_interest_within_grace_period] column nullable finished


CREATE TABLE [dbo].[ContractAssignHistory](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[DateChanged] [datetime] NOT NULL CONSTRAINT [DF_ContractAssignHistory_DateChanged]  DEFAULT (getdate()),
	[loanofficerFrom_id] [int] NOT NULL,
	[loanofficerTo_id] [int] NOT NULL,
	[contract_id] [int] NOT NULL
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[ContractAssignHistory]  WITH CHECK ADD  CONSTRAINT [FK_ContractAssignHistory_Contracts] FOREIGN KEY([contract_id])
REFERENCES [dbo].[Contracts] ([id])
GO
ALTER TABLE [dbo].[ContractAssignHistory] CHECK CONSTRAINT [FK_ContractAssignHistory_Contracts]
GO
ALTER TABLE [dbo].[ContractAssignHistory]  WITH CHECK ADD  CONSTRAINT [FK_ContractAssignHistory_Users] FOREIGN KEY([loanofficerFrom_id])
REFERENCES [dbo].[Users] ([id])
GO
ALTER TABLE [dbo].[ContractAssignHistory] CHECK CONSTRAINT [FK_ContractAssignHistory_Users]
GO
ALTER TABLE [dbo].[ContractAssignHistory]  WITH CHECK ADD  CONSTRAINT [FK_ContractAssignHistory_Users1] FOREIGN KEY([loanofficerTo_id])
REFERENCES [dbo].[Users] ([id])
GO
ALTER TABLE [dbo].[ContractAssignHistory] CHECK CONSTRAINT [FK_ContractAssignHistory_Users1]
GO

INSERT INTO [CorporateEventsType]([id], [code]) VALUES(0, 'Entry')

/************** VERSION ***********************/
UPDATE [TechnicalParameters] SET [value]='v1.2.1'
GO
