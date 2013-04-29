CREATE TABLE [dbo].[Questionnaire](
	[Name] [nvarchar](100) NOT NULL,
	[Country] [nvarchar](50) NOT NULL,
	[Email] [nvarchar](100) NOT NULL,
	[NumberOfClients] [int] NULL CONSTRAINT [DF_Questionnaire_NumberOfClients]  DEFAULT ((0)),
	[GrossPortfolio] [int] NULL CONSTRAINT [DF_Questionnaire_GrossPortfolio]  DEFAULT ((0)),
	[PositionInCompony] [nvarchar](100) NULL,
	[BeContacted] [nvarchar](150) NULL,
	[FirstTime] [bit] NOT NULL,
	[DailyActivity] [nvarchar](150) NOT NULL,
	[MainPriorities] [nvarchar](4000) NULL,
	[MainAdvantages] [nvarchar](4000) NULL,
	[OtherMessages] [nvarchar](4000) NULL
) ON [PRIMARY]
GO

ALTER TABLE dbo.WriteOffEvents ADD
	past_due_days int NOT NULL CONSTRAINT DF_WriteOffEvents_past_due_days DEFAULT ((365))
GO

/*********** Saving Contracts Tables *********/

/****** Object:  Table [dbo].[SavingProducts]    Script Date: 11/17/2008 16:26:01 ******/
CREATE TABLE [dbo].[SavingProducts](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[deleted] [bit] NOT NULL DEFAULT ((0)),
	[name] [nvarchar](100) NOT NULL,
	[client_type] [char](1) NULL DEFAULT ('-'),
	[initial_amount_min] [money] NULL,
	[initial_amount_max] [money] NULL,
	[balance_min] [money] NULL,
	[balance_max] [money] NULL,
	[withdraw_min] [money] NULL,
	[withdraw_max] [money] NULL,
	[deposit_min] [money] NULL,
	[deposit_max] [money] NULL,
	[interest_rate] [float] NULL,
	[interest_rate_min] [float] NULL,
	[interest_rate_max] [float] NULL,
 CONSTRAINT [PK_SavingProduct] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [IX_SavingProduct_name] UNIQUE NONCLUSTERED 
(
	[name] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

/****** Object:  Table [dbo].[SavingContracts]    Script Date: 11/17/2008 16:29:41 ******/
CREATE TABLE [dbo].[SavingContracts](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[product_id] [int] NOT NULL,
	[user_id] [int] NOT NULL,
	[code] [nvarchar](50) NOT NULL,
	[tiers_id] [int] NOT NULL,
	[creation_date] [datetime] NOT NULL,
	[description] [nvarchar](300) NOT NULL,
	[interest_rate] [float] NOT NULL,
 CONSTRAINT [PK_SavingContracts] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[SavingContracts]  WITH CHECK ADD  CONSTRAINT [FK_SavingContracts_Tiers] FOREIGN KEY([tiers_id])
REFERENCES [dbo].[Tiers] ([id])
GO
ALTER TABLE [dbo].[SavingContracts] CHECK CONSTRAINT [FK_SavingContracts_Tiers]
GO
ALTER TABLE [dbo].[SavingContracts]  WITH CHECK ADD  CONSTRAINT [FK_SavingContracts_Users] FOREIGN KEY([user_id])
REFERENCES [dbo].[Users] ([id])
GO
ALTER TABLE [dbo].[SavingContracts] CHECK CONSTRAINT [FK_SavingContracts_Users]
GO
ALTER TABLE [dbo].[SavingContracts]  WITH CHECK ADD  CONSTRAINT [FK_Savings_SavingProducts] FOREIGN KEY([product_id])
REFERENCES [dbo].[SavingProducts] ([id])
GO
ALTER TABLE [dbo].[SavingContracts] CHECK CONSTRAINT [FK_Savings_SavingProducts]


/****** Object:  Table [dbo].[SavingEvents]    Script Date: 11/17/2008 16:25:55 ******/
CREATE TABLE [dbo].[SavingEvents](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[user_id] [int] NOT NULL,
	[contract_id] [int] NOT NULL,
	[code] [char](4) NOT NULL,
	[amount] [money] NOT NULL,
	[description] [nvarchar](200) NULL,
	[direction] [smallint] NOT NULL,
	[deleted] [bit] NOT NULL,
	[creation_date] [datetime] NOT NULL,
	[cancelable] [bit] NOT NULL,
	[is_fired] [bit] NOT NULL,
 CONSTRAINT [PK_SavingEvents] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[SavingEvents]  WITH CHECK ADD  CONSTRAINT [FK_SavingEvents_SavingContracts] FOREIGN KEY([contract_id])
REFERENCES [dbo].[SavingContracts] ([id])
GO
ALTER TABLE [dbo].[SavingEvents] CHECK CONSTRAINT [FK_SavingEvents_SavingContracts]
GO
ALTER TABLE [dbo].[SavingEvents]  WITH CHECK ADD  CONSTRAINT [FK_SavingEvents_Users] FOREIGN KEY([user_id])
REFERENCES [dbo].[Users] ([id])
GO
ALTER TABLE [dbo].[SavingEvents] CHECK CONSTRAINT [FK_SavingEvents_Users]
GO
-- Clean double entries 
SELECT * 
INTO #TempLinkCollateralCredit
FROM [LinkGuarantorCredit]
GROUP BY tiers_id, contract_id, guarantee_amount

TRUNCATE TABLE [LinkGuarantorCredit]

INSERT INTO [LinkGuarantorCredit] (
 [tiers_id],
 [contract_id],
 [guarantee_amount]
) 
SELECT * FROM [#TempLinkCollateralCredit]
GO
CREATE TABLE [dbo].[LinkCollateralCredit]
(
  [contract_id] [int] NOT NULL,
  [collateral_id] [int] NOT NULL,
  [collateral_amount] [money] NULL
)
GO
INSERT INTO [LinkCollateralCredit] 
SELECT id, [collateral_id], [collateral_amount]
FROM Credit
WHERE [collateral_id] IS NOT NULL AND [collateral_amount] IS NOT NULL
GO
ALTER TABLE Credit 
DROP COLUMN collateral_amount
GO
ALTER TABLE Credit DROP CONSTRAINT FK_Credit_Collaterals
GO
ALTER TABLE Credit 
DROP COLUMN [collateral_id]
GO

UPDATE Contracts SET credit_commitee_date = [start_date] WHERE credit_commitee_date IS NULL
GO

UPDATE Contracts SET credit_commitee_comment = 'NotSet' WHERE credit_commitee_comment IS NULL
GO

ALTER TABLE dbo.Tiers
	DROP COLUMN home_type, secondary_home_type
GO
ALTER TABLE dbo.Tiers ADD
	home_type nvarchar(50) NULL,
	secondary_hometype nvarchar(50) NULL,
	zipCode nvarchar(50) NULL,
	secondary_zipCode nvarchar(50) NULL
GO

ALTER TABLE dbo.Persons
	DROP CONSTRAINT FK_Persons_Tiers
GO

ALTER TABLE dbo.Persons
	DROP CONSTRAINT FK_Persons_DomainOfApplications
GO

ALTER TABLE dbo.Persons
	DROP CONSTRAINT DF_Credit_handicapped
GO

ALTER TABLE dbo.Persons
	DROP CONSTRAINT DF_Persons_povertylevel_childreneducation
GO

ALTER TABLE dbo.Persons
	DROP CONSTRAINT DF_Persons_povertylevel_economiceducation
GO

ALTER TABLE dbo.Persons
	DROP CONSTRAINT DF_Persons_povertylevel_socialparticipation
GO

ALTER TABLE dbo.Persons
	DROP CONSTRAINT DF_Persons_povertylevel_healthsituation
GO

CREATE TABLE dbo.Tmp_Persons
	(
	id int NOT NULL,
	first_name nvarchar(100) NOT NULL,
	sex char(1) NOT NULL,
	identification_data nvarchar(200) NOT NULL,
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
	BIC nvarchar(50) NULL,
	IBAN1 nvarchar(50) NULL,
	IBAN2 nvarchar(50) NULL,
	study_level nvarchar(50) NULL,
	SS nvarchar(50) NULL,
	CAF nvarchar(50) NULL,
	housing_situation nvarchar(50) NULL,
	bank_address nvarchar(50) NULL,
	bank_situation nvarchar(50) NULL,
	handicapped bit NOT NULL,
	professional_experience nvarchar(50) NULL,
	professional_situation nvarchar(50) NULL,
	first_contact datetime NULL,
	family_situation nvarchar(50) NULL,
	mother_name nvarchar(200) NULL,
	povertylevel_childreneducation int NOT NULL,
	povertylevel_economiceducation int NOT NULL,
	povertylevel_socialparticipation int NOT NULL,
	povertylevel_healthsituation int NOT NULL,
	unemployment_months int NULL
	)  ON [PRIMARY]
GO

ALTER TABLE dbo.Tmp_Persons ADD CONSTRAINT
	DF_Credit_handicapped DEFAULT ((0)) FOR handicapped
GO

ALTER TABLE dbo.Tmp_Persons ADD CONSTRAINT
	DF_Persons_povertylevel_childreneducation DEFAULT ((0)) FOR povertylevel_childreneducation
GO

ALTER TABLE dbo.Tmp_Persons ADD CONSTRAINT
	DF_Persons_povertylevel_economiceducation DEFAULT ((0)) FOR povertylevel_economiceducation
GO

ALTER TABLE dbo.Tmp_Persons ADD CONSTRAINT
	DF_Persons_povertylevel_socialparticipation DEFAULT ((0)) FOR povertylevel_socialparticipation
GO

ALTER TABLE dbo.Tmp_Persons ADD CONSTRAINT
	DF_Persons_povertylevel_healthsituation DEFAULT ((0)) FOR povertylevel_healthsituation
GO

IF EXISTS(SELECT * FROM dbo.Persons)
	 EXEC('INSERT INTO dbo.Tmp_Persons (id, first_name, sex, identification_data, last_name, birth_date, household_head, nb_of_dependents, nb_of_children, children_basic_education, livestock_number, livestock_type, landplot_size, home_size, home_time_living_in, capital_other_equipments, activity_id, experience, nb_of_people, monthly_income, monthly_expenditure, comments, image_path, father_name, birth_place, nationality, IBAN1, study_level, SS, CAF, housing_situation, bank_address, handicapped, professional_experience, professional_situation, first_contact, family_situation, mother_name, povertylevel_childreneducation, povertylevel_economiceducation, povertylevel_socialparticipation, povertylevel_healthsituation)
		SELECT id, first_name, sex, identification_data, last_name, birth_date, household_head, nb_of_dependents, nb_of_children, children_basic_education, livestock_number, livestock_type, landplot_size, home_size, home_time_living_in, capital_other_equipments, activity_id, experience, nb_of_people, monthly_income, monthly_expenditure, comments, image_path, father_name, birth_place, nationality, IBAN, study_level, SS, CAF, housing_situation, bank_address, handicapped, professional_experience, professional_situation, first_contact, family_situation, mother_name, povertylevel_childreneducation, povertylevel_economiceducation, povertylevel_socialparticipation, povertylevel_healthsituation FROM dbo.Persons WITH (HOLDLOCK TABLOCKX)')
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

ALTER TABLE dbo.Tiers	DROP CONSTRAINT FK_Tiers_Districts
GO

ALTER TABLE dbo.Tiers	DROP CONSTRAINT FK_Tiers_Districts1
GO

ALTER TABLE dbo.Tiers	DROP CONSTRAINT FK_Tiers_Corporates
GO

ALTER TABLE dbo.Tiers	DROP CONSTRAINT DF_Tiers_loan_cycle
GO

ALTER TABLE dbo.Tiers	DROP CONSTRAINT DF_Tiers_status
GO

CREATE TABLE dbo.Tmp_Tiers
	(
	id int NOT NULL IDENTITY (1, 1),
	client_type_code char(1) NOT NULL,
	scoring float(53) NULL,
	loan_cycle int NOT NULL,
	active bit NOT NULL,
	bad_client bit NOT NULL,
	other_org_name nvarchar(100) NULL,
	other_org_amount money NULL,
	other_org_debts money NULL,
	district_id int NOT NULL,
	city nvarchar(50) NULL,
	address nvarchar(500) NULL,
	secondary_district_id int NULL,
	secondary_city nvarchar(50) NULL,
	secondary_address nvarchar(500) NULL,
	cash_input_voucher_number int NULL,
	cash_output_voucher_number int NULL,
	creation_date datetime NULL,
	home_phone varchar(50) NULL,
	personal_phone varchar(50) NULL,
	secondary_home_phone varchar(50) NULL,
	secondary_personal_phone varchar(50) NULL,
	e_mail nvarchar(50) NULL,
	secondary_e_mail nvarchar(50) NULL,
	status smallint NOT NULL,
	other_org_comment nvarchar(500) NULL,
	sponsor1 nvarchar(50) NULL,
	sponsor1_comment nvarchar(100) NULL,
	sponsor2 nvarchar(50) NULL,
	sponsor2_comment nvarchar(100) NULL,
	follow_up_comment nvarchar(500) NULL,
	home_type nvarchar(50) NULL,
	secondary_homeType nvarchar(50) NULL,
	zipCode nvarchar(50) NULL,
	secondary_zipCode nvarchar(50) NULL
	)  ON [PRIMARY]
GO

ALTER TABLE dbo.Tmp_Tiers ADD CONSTRAINT
	DF_Tiers_loan_cycle DEFAULT ((1)) FOR loan_cycle
GO

ALTER TABLE dbo.Tmp_Tiers ADD CONSTRAINT
	DF_Tiers_status DEFAULT ((1)) FOR status
GO
SET IDENTITY_INSERT dbo.Tmp_Tiers ON
GO
IF EXISTS(SELECT * FROM dbo.Tiers)
	 EXEC('INSERT INTO dbo.Tmp_Tiers (id, client_type_code, scoring, loan_cycle, active, bad_client, other_org_name, other_org_amount, other_org_debts, district_id, city, address, secondary_district_id, secondary_city, secondary_address, cash_input_voucher_number, cash_output_voucher_number, creation_date, home_phone, personal_phone, secondary_home_phone, secondary_personal_phone, e_mail, secondary_e_mail, status, other_org_comment, sponsor1, follow_up_comment, home_type, secondary_hometype, zipCode, secondary_zipCode)
		SELECT id, client_type_code, scoring, loan_cycle, active, bad_client, other_org_name, other_org_amount, other_org_debts, district_id, city, address, secondary_district_id, secondary_city, secondary_address, cash_input_voucher_number, cash_output_voucher_number, creation_date, home_phone, personal_phone, secondary_home_phone, secondary_personal_phone, e_mail, secondary_e_mail, status, other_org_comment, sponsor, follow_up_comment, home_type, secondary_hometype, zipCode, secondary_zipCode FROM dbo.Tiers WITH (HOLDLOCK TABLOCKX)')
GO

SET IDENTITY_INSERT dbo.Tmp_Tiers OFF
GO

ALTER TABLE dbo.LinkGuarantorCredit	DROP CONSTRAINT FK_LinkGuarantorCredit_Tiers
GO

ALTER TABLE dbo.Projects	DROP CONSTRAINT FK_Projects_Tiers
GO

ALTER TABLE dbo.SavingContracts	DROP CONSTRAINT FK_SavingContracts_Tiers
GO

ALTER TABLE dbo.Persons	DROP CONSTRAINT FK_Persons_Tiers
GO
ALTER TABLE dbo.Groups	DROP CONSTRAINT FK_Groups_Tiers
GO

DROP TABLE dbo.Tiers
GO

EXECUTE sp_rename N'dbo.Tmp_Tiers', N'Tiers', 'OBJECT' 
GO

ALTER TABLE dbo.Tiers ADD CONSTRAINT
	PK_Clients PRIMARY KEY CLUSTERED 
	(
	id
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO

ALTER TABLE dbo.Tiers WITH NOCHECK ADD CONSTRAINT
	CK_TiersTypeCode CHECK (([client_type_code]='G' OR [client_type_code]='I' OR [client_type_code]='C'))
GO

ALTER TABLE dbo.Tiers WITH NOCHECK ADD CONSTRAINT
	FK_Tiers_Corporates FOREIGN KEY
	(
	id
	) REFERENCES dbo.Corporates
	(
	id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO

ALTER TABLE dbo.Tiers
	NOCHECK CONSTRAINT FK_Tiers_Corporates
GO

ALTER TABLE dbo.Tiers WITH NOCHECK ADD CONSTRAINT
	FK_Tiers_Districts FOREIGN KEY
	(
	district_id
	) REFERENCES dbo.Districts
	(
	id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO

ALTER TABLE dbo.Tiers WITH NOCHECK ADD CONSTRAINT
	FK_Tiers_Districts1 FOREIGN KEY
	(
	secondary_district_id
	) REFERENCES dbo.Districts
	(
	id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO

ALTER TABLE dbo.Groups WITH NOCHECK ADD CONSTRAINT
	FK_Groups_Tiers FOREIGN KEY
	(
	id
	) REFERENCES dbo.Tiers
	(
	id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO

ALTER TABLE dbo.Groups
	NOCHECK CONSTRAINT FK_Groups_Tiers
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

ALTER TABLE dbo.SavingContracts ADD CONSTRAINT
	FK_SavingContracts_Tiers FOREIGN KEY
	(
	tiers_id
	) REFERENCES dbo.Tiers
	(
	id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO

ALTER TABLE dbo.Projects ADD CONSTRAINT
	FK_Projects_Tiers FOREIGN KEY
	(
	tiers_id
	) REFERENCES dbo.Tiers
	(
	id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO

ALTER TABLE dbo.LinkGuarantorCredit WITH NOCHECK ADD CONSTRAINT
	FK_LinkGuarantorCredit_Tiers FOREIGN KEY
	(
	tiers_id
	) REFERENCES dbo.Tiers
	(
	id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO

ALTER TABLE dbo.Projects DROP CONSTRAINT FK_Projects_Corporates
GO

ALTER TABLE dbo.Projects DROP COLUMN corporate_id
GO

CREATE TABLE dbo.Banks
	(
	id int IDENTITY(1,1) NOT NULL,
	address nvarchar(200) NULL,
	BIC nvarchar(50) NULL,
	IBAN1 nvarchar(100) NULL,
	IBAN2 nvarchar(100) NULL
	)  ON [PRIMARY]
GO

ALTER TABLE dbo.Banks ADD CONSTRAINT
	PK_Banks PRIMARY KEY CLUSTERED 
	(
	id
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO

ALTER TABLE dbo.Persons ADD
	personalBank_id int NULL,
	businessBank_id int NULL
GO

ALTER TABLE dbo.Persons ADD CONSTRAINT
	FK_Persons_Banks FOREIGN KEY
	(
	personalBank_id
	) REFERENCES dbo.Banks
	(
	id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO

ALTER TABLE dbo.Persons ADD CONSTRAINT
	FK_Persons_Banks1 FOREIGN KEY
	(
	businessBank_id
	) REFERENCES dbo.Banks
	(
	id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO

ALTER TABLE dbo.Persons
	DROP COLUMN IBAN1, IBAN2, BIC, bank_address
GO

ALTER TABLE dbo.Projects ADD
	corporate_name nvarchar(50) NULL,
	corporate_juridicStatus nvarchar(50) NULL,
	corporate_FiscalStatus nvarchar(50) NULL,
	corporate_siret nvarchar(50) NULL,
	corporate_CA money NULL,
	corporate_nbOfJobs int NULL,
	corporate_financialPlanType nvarchar(50) NULL,
	corporateFinancialPlanAmount money NULL,
	corporate_financialPlanTotalAmount money NULL,
	address nvarchar(20) NULL,
	city nvarchar(50) NULL,
	zipCode nvarchar(50) NULL,
	district_id int NULL,
	home_phone nvarchar(50) NULL,
	personalPhone nvarchar(50) NULL,
	Email nvarchar(50) NULL,
	hometype nvarchar(50) NULL
GO

CREATE TABLE dbo.FollowUp
	(
	id int IDENTITY (1, 1) NOT NULL,
	project_id int NOT NULL,
	year int NOT NULL,
	CA money NOT NULL,
	Jobs1 int NOT NULL,
	Jobs2 int NOT NULL,
	PersonalSituation nvarchar(50) NOT NULL,
	activity nvarchar(50) NOT NULL,
	comment nvarchar(20) NOT NULL
	)  ON [PRIMARY]
GO

ALTER TABLE dbo.FollowUp ADD CONSTRAINT
	PK_FollowUp PRIMARY KEY CLUSTERED 
	(
	id
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO

ALTER TABLE dbo.FollowUp ADD CONSTRAINT
	FK_FollowUp_Projects FOREIGN KEY
	(
	project_id
	) REFERENCES dbo.Projects
	(
	id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO

ALTER TABLE dbo.Corporates ADD
	fiscal_status nvarchar(50) NULL,
	registre nvarchar(50) NULL
GO
ALTER TABLE dbo.Guarantees ADD
	banque nvarchar(50) NULL
GO
ALTER TABLE dbo.Guarantees
	DROP COLUMN rate, rate_limit
GO

ALTER TABLE dbo.Corporates
	DROP CONSTRAINT FK_Corporates_LegalForm
GO

ALTER TABLE dbo.Corporates
	DROP CONSTRAINT FK_Corporates_InsertionType
GO

ALTER TABLE dbo.Corporates ADD
	legalForm nvarchar(50) NULL,
	insertionType nvarchar(50) NULL
GO
ALTER TABLE dbo.Corporates
	DROP COLUMN insertion_type_id, legalForm_id, rcs
GO

CREATE TABLE [dbo].[SetUp_ProfessionalSituation](
	[value] [nvarchar](50) NOT NULL
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[SetUp_Sponsor2](
	[value] [nvarchar](50) NOT NULL
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[SetUp_ActivityState](
	[value] [nvarchar](50) NOT NULL
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[SetUp_ProfessionalExperience](
	[value] [nvarchar](50) NOT NULL
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[SetUp_HousingSituation](
	[value] [nvarchar](50) NOT NULL
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[SetUp_Sponsor1](
	[value] [nvarchar](50) NOT NULL
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[SetUp_BankSituation](
	[value] [nvarchar](50) NOT NULL
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[SetUp_PersonalSituation](
	[value] [nvarchar](50) NOT NULL
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[SetUp_SocialSituation](
	[value] [nvarchar](50) NOT NULL
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[SetUp_FiscalStatus](
	[value] [nvarchar](50) NOT NULL
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[SetUp_LegalStatus](
	[value] [nvarchar](50) NOT NULL
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[SetUp_InsertionTypes](
	[value] [nvarchar](50) NOT NULL
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[SetUp_SubventionTypes](
	[value] [nvarchar](50) NOT NULL
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[SetUp_DwellingPlace](
	[value] [nvarchar](50) NOT NULL
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[SetUp_FamilySituation](
	[value] [nvarchar](50) NOT NULL
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[SetUp_BusinessPlan](
	[value] [nvarchar](50) NOT NULL
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[SetUp_StudyLevel](
	[value] [nvarchar](50) NOT NULL
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[SetUp_Registre](
	[value] [nvarchar](50) NOT NULL
) ON [PRIMARY]
GO

DROP TABLE dbo.ProfessionalSituations
GO

DROP TABLE dbo.ProfessionalExperience
GO

DROP TABLE dbo.InsertionType
GO

DROP TABLE dbo.LegalForm
GO

ALTER TABLE dbo.Packages	DROP CONSTRAINT FK_Packages_Corporates
GO

ALTER TABLE dbo.Packages	DROP CONSTRAINT FK_Packages_Cycles
GO

ALTER TABLE dbo.Packages	DROP CONSTRAINT FK_Packages_InstallmentTypes
GO

ALTER TABLE dbo.Packages	DROP CONSTRAINT FK_Packages_Exotics
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'DF__Tmp_Packa__delet__6F2063EF')) 
ALTER TABLE dbo.Packages	DROP CONSTRAINT DF__Tmp_Packa__delet__6F2063EF
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'DF__Tmp_Packa__clien__70148828')) 
ALTER TABLE dbo.Packages	DROP CONSTRAINT DF__Tmp_Packa__clien__70148828
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'DF__Tmp_Packag__fake__7108AC61')) 
ALTER TABLE dbo.Packages	DROP CONSTRAINT DF__Tmp_Packag__fake__7108AC61
GO
CREATE TABLE dbo.Tmp_Packages
	(
	id int NOT NULL IDENTITY (1, 1),
	deleted bit NOT NULL,
	code nvarchar(50) NOT NULL,
	name nvarchar(100) NOT NULL,
	client_type char(1) NULL,
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
	fake bit NOT NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.Tmp_Packages ADD CONSTRAINT	DF__Tmp_Packa__delet__6F2063EF DEFAULT ((0)) FOR deleted
GO

ALTER TABLE dbo.Tmp_Packages ADD CONSTRAINT	DF_Packages_code DEFAULT N'NotSet' FOR code
GO

ALTER TABLE dbo.Tmp_Packages ADD CONSTRAINT	DF__Tmp_Packa__clien__70148828 DEFAULT ('-') FOR client_type
GO

ALTER TABLE dbo.Tmp_Packages ADD CONSTRAINT	DF__Tmp_Packag__fake__7108AC61 DEFAULT ((0)) FOR fake
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

ALTER TABLE dbo.Contracts ADD
  [credit_commitee_code][nvarchar] (100) Null
GO

UPDATE dbo.[GeneralParameters]
SET [value] = '0' WHERE [key] = 'ALLOWS_MULTIPLE_LOANS'AND [value] = 'False'
GO

UPDATE dbo.[GeneralParameters]
SET [value] = '1' WHERE [key] = 'ALLOWS_MULTIPLE_LOANS' AND [value] = 'True'
GO

INSERT INTO [GeneralParameters]([key], [value]) VALUES('SENT_QUESTIONNAIRE',0)
GO

INSERT INTO [GeneralParameters]([key], [value]) VALUES('NAME_FORMAT','L U')
GO

/* Embed Repayment Collection Sheet custom report */
DELETE FROM [ReportParametrs] WHERE [data_object_id] IN (SELECT id FROM [ReportDataObjectSource] WHERE [sub_report_name] = 'RepaymentCollectionSheet.rpt')
DELETE FROM [ReportDataObjectSource] WHERE [sub_report_name] = 'RepaymentCollectionSheet.rpt'
DELETE FROM [ReportObject] WHERE [report_name] = 'RepaymentCollectionSheet.rpt'
DECLARE @ObjectID INT
DECLARE @SourceID INT
DECLARE @ParametrID INT
INSERT INTO ReportObject (report_name) VALUES ('RepaymentCollectionSheet.rpt') 
SELECT @ObjectID = @@IDENTITY
INSERT INTO ReportDataObjectSource (report_object_id, sub_report_name, data_object_source, data_object_type) VALUES (@ObjectID, 'RepaymentCollectionSheet.rpt', 'repaymentCollection', 'P') 
SELECT @SourceID = @@IDENTITY 
INSERT INTO ReportParametrs (data_object_id, name, type) VALUES (@SourceID, '@beginDate', 'DateTimeType') 
INSERT INTO ReportParametrs (data_object_id, name, type) VALUES (@SourceID, '@endDate', 'DateTimeType') 
INSERT INTO ReportParametrs (data_object_id, name, type) VALUES (@SourceID, '@showDelinquent', 'TextType') 
GO

ALTER TABLE ElementaryMvts
ADD export_date DATETIME
GO

DECLARE @D DATETIME
SET @D = GETDATE()

UPDATE ElementaryMvts
SET export_date = @D
WHERE is_exported = 1
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'ReschedulingOfALoanEvents')) 
ALTER TABLE dbo.ReschedulingOfALoanEvents ADD
	nb_of_days int NOT NULL CONSTRAINT DF_ReschedulingOfALoanEvents_nb_of_days DEFAULT 0,
	nb_of_months int NOT NULL CONSTRAINT DF_ReschedulingOfALoanEvents_nb_of_months DEFAULT 0
GO

UPDATE [Accounts] SET [description] = 10 WHERE [description] = 4
GO

UPDATE [Accounts] SET [description] = 4  WHERE [description] = 3
GO

UPDATE [Accounts] SET [description] = 3  WHERE [description] = 10 
GO

-- Add Disbursement Cash Receipt as a custom report
DELETE FROM [ReportParametrs] WHERE [data_object_id] IN (SELECT id FROM [ReportDataObjectSource] WHERE [sub_report_name] = 'DisbursementCashReceipt.rpt')
DELETE FROM [ReportParametrs] WHERE [data_object_id] IN (SELECT id FROM [ReportDataObjectSource] WHERE [sub_report_name] = 'DisbursementCashReceipt_CustomerCopy')
DELETE FROM [ReportParametrs] WHERE [data_object_id] IN (SELECT id FROM [ReportDataObjectSource] WHERE [sub_report_name] = 'DisbursementCashReceipt_LoanOfficerCopy')
DELETE FROM [ReportParametrs] WHERE [data_object_id] IN (SELECT id FROM [ReportDataObjectSource] WHERE [sub_report_name] = 'DisbursementCashReceipt_CashierCopy')
DELETE FROM [ReportDataObjectSource] WHERE [sub_report_name] = 'DisbursementCashReceipt.rpt'
DELETE FROM [ReportDataObjectSource] WHERE [sub_report_name] = 'DisbursementCashReceipt_CustomerCopy'
DELETE FROM [ReportDataObjectSource] WHERE [sub_report_name] = 'DisbursementCashReceipt_LoanOfficerCopy'
DELETE FROM [ReportDataObjectSource] WHERE [sub_report_name] = 'DisbursementCashReceipt_CashierCopy'
DELETE FROM [ReportObject] WHERE [report_name] = 'DisbursementCashReceipt.rpt'
DECLARE @ObjectID INT
DECLARE @SourceID INT
INSERT INTO ReportObject (report_name) VALUES ('DisbursementCashReceipt.rpt') 
SELECT @ObjectID = @@IDENTITY
INSERT INTO ReportDataObjectSource (report_object_id, sub_report_name, data_object_source, data_object_type) VALUES (@ObjectID, 'DisbursementCashReceipt.rpt', 'GetDisbursementCashReceiptInfo', 'P') 
SELECT @SourceID = @@IDENTITY 
INSERT INTO ReportParametrs (data_object_id, name, type) VALUES (@SourceID, '@contract_id', 'TextType') 
INSERT INTO ReportDataObjectSource (report_object_id, sub_report_name, data_object_source, data_object_type) VALUES (@ObjectID, 'DisbursementCashReceipt_CustomerCopy', 'GetDisbursementCashReceiptMemberDetails', 'P') 
SELECT @SourceID = @@IDENTITY 
INSERT INTO ReportParametrs (data_object_id, name, type) VALUES (@SourceID, '@contract_id', 'TextType') 
INSERT INTO ReportDataObjectSource (report_object_id, sub_report_name, data_object_source, data_object_type) VALUES (@ObjectID, 'DisbursementCashReceipt_LoanOfficerCopy', 'GetDisbursementCashReceiptMemberDetails', 'P') 
SELECT @SourceID = @@IDENTITY 
INSERT INTO ReportParametrs (data_object_id, name, type) VALUES (@SourceID, '@contract_id', 'TextType') 
INSERT INTO ReportDataObjectSource (report_object_id, sub_report_name, data_object_source, data_object_type) VALUES (@ObjectID, 'DisbursementCashReceipt_CashierCopy', 'GetDisbursementCashReceiptMemberDetails', 'P') 
SELECT @SourceID = @@IDENTITY 
INSERT INTO ReportParametrs (data_object_id, name, type) VALUES (@SourceID, '@contract_id', 'TextType') 
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'DF_WriteOffEvents_provisioning_amount')) 
ALTER TABLE dbo.WriteOffEvents
	DROP CONSTRAINT DF_WriteOffEvents_provisioning_amount
GO
ALTER TABLE dbo.WriteOffEvents
	DROP COLUMN provisioning_amount
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'DF_ReschedulingOfALoanEvents_fees')) 
ALTER TABLE dbo.ReschedulingOfALoanEvents
	DROP CONSTRAINT DF_ReschedulingOfALoanEvents_fees
GO
ALTER TABLE dbo.ReschedulingOfALoanEvents
	DROP COLUMN fees
GO
-- Get rid of deprecated views and stored procedures
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[CashReceiptView]'))
DROP VIEW [CashReceiptView]
GO

IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[ClientsBySectorView]'))
DROP VIEW [ClientsBySectorView]
GO

IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[ContractFormView]'))
DROP VIEW [ContractFormView]
GO

IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[CurrentRepaymentRateView]'))
DROP VIEW [CurrentRepaymentRateView]
GO

IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[DelinquentLoansView]'))
DROP VIEW [DelinquentLoansView]
GO

IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[DisbursementAndCustomersPerLoanOfficerView]'))
DROP VIEW [DisbursementAndCustomersPerLoanOfficerView]
GO

IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[DisbursmentView]'))
DROP VIEW [DisbursmentView]
GO

IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[DormantCustomersView]'))
DROP VIEW [DormantCustomersView]
GO

IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[ExternalCurrency]'))
DROP VIEW [ExternalCurrency]
GO

IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[GLoanSizeMaturityGraceDomainDistrict_FullyRepaid]'))
DROP VIEW [GLoanSizeMaturityGraceDomainDistrict_FullyRepaid]
GO

IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[InternalCurrency]'))
DROP VIEW [InternalCurrency]
GO

IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[LoansBySectorsView]'))
DROP VIEW [LoansBySectorsView]
GO

IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[LoansBySectorView]'))
DROP VIEW [LoansBySectorView]
GO

IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[LoansBySizeView]'))
DROP VIEW [LoansBySizeView]
GO

IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[NonActiveCustomersView]'))
DROP VIEW [NonActiveCustomersView]
GO

IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[ActiveCustomersView]'))
DROP VIEW [ActiveCustomersView]
GO

IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[OLBView]'))
DROP VIEW [OLBView]
GO

IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[PARAmountView]'))
DROP VIEW [PARAmountView]
GO

IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[OutstandingPortfolioView]'))
DROP VIEW [OutstandingPortfolioView]
GO

IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[OverDueView]'))
DROP VIEW [OverDueView]
GO

IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[ReportPARView]'))
DROP VIEW [ReportPARView]
GO

IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[RevenuesView]'))
DROP VIEW [RevenuesView]
GO

IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[QualityReport_PersonFinancial]'))
DROP VIEW [QualityReport_PersonFinancial]
GO

IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[QualityReport_shares_past]'))
DROP VIEW [QualityReport_shares_past]
GO

IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[QualityReport_shares]'))
DROP VIEW [QualityReport_shares]
GO

IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[QualityReport_ActivePersonsPhysical]'))
DROP VIEW [QualityReport_ActivePersonsPhysical]
GO

IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[QualityReport_PersonsPhysicalInformation]'))
DROP VIEW [QualityReport_PersonsPhysicalInformation]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetMembersForContract]') AND type in (N'P', N'PC'))
DROP PROCEDURE [GetGuarantors]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetMembersForContract]') AND type in (N'P', N'PC'))
DROP PROCEDURE [GetInstallmentsForContract]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'GetLoanGuarantors')) 
DROP PROCEDURE [GetLoanGuarantors]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetMembersForContract]') AND type in (N'P', N'PC'))
DROP PROCEDURE [GetMembersForContract]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetMembersForGroupContract]') AND type in (N'P', N'PC'))
DROP PROCEDURE [GetMembersForGroupContract]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[LoanFullyRepaid_ByDistrict]') AND type in (N'P', N'PC'))
DROP PROCEDURE [LoanFullyRepaid_ByDistrict]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[LoanFullyRepaid_ByLoanOfficer]') AND type in (N'P', N'PC'))
DROP PROCEDURE [LoanFullyRepaid_ByLoanOfficer]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[LoanFullyRepaid_ByProduct]') AND type in (N'P', N'PC'))
DROP PROCEDURE [LoanFullyRepaid_ByProduct]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[LoansDisbursed_Amount_Nb_ByActivity]') AND type in (N'P', N'PC'))
DROP PROCEDURE [LoansDisbursed_Amount_Nb_ByActivity]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[LoansDisbursed_Amount_Nb_ByDistrict]') AND type in (N'P', N'PC'))
DROP PROCEDURE [LoansDisbursed_Amount_Nb_ByDistrict]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[LoansDisbursed_Amount_Nb_ByLoanOfficer]') AND type in (N'P', N'PC'))
DROP PROCEDURE [LoansDisbursed_Amount_Nb_ByLoanOfficer]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[LoansDisbursed_Amount_Nb_ByProduct]') AND type in (N'P', N'PC'))
DROP PROCEDURE [LoansDisbursed_Amount_Nb_ByProduct]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PAR_LoansPAR]') AND type in (N'P', N'PC'))
DROP PROCEDURE [PAR_LoansPAR]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RepaymentsSchedule_Group]') AND type in (N'P', N'PC'))
DROP PROCEDURE [RepaymentsSchedule_Group]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RepaymentsSchedule_Individual]') AND type in (N'P', N'PC'))
DROP PROCEDURE [RepaymentsSchedule_Individual]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TempCashReceipt_Proc]') AND type in (N'P', N'PC'))
DROP PROCEDURE [TempCashReceipt_Proc]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetContract]') AND type in (N'P', N'PC'))
DROP PROCEDURE [GetContract]
GO

UPDATE dbo.Contracts SET status = 2 WHERE closed = 0
GO

UPDATE [TechnicalParameters] SET [value]='v2.5.2'
GO
