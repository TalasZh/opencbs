ALTER TABLE dbo.Packages ADD
	corporate_id int NULL,
	[fake] [bit] NOT NULL  DEFAULT ((0))
GO

ALTER TABLE dbo.Credit ADD
	corporate_id int NULL,
	[fake] [bit] NOT NULL  DEFAULT ((0))
GO

UPDATE Credit SET corporate_id = (SELECT TOP 1 id FROM Corporates)
GO

ALTER TABLE dbo.CorporateEvents ADD
	[type] int NOT NULL DEFAULT ((0))
GO

CREATE TABLE [dbo].[CorporateEventsType](
	[id] [int] NOT NULL,
	[code] [nvarchar](50) NULL,
 CONSTRAINT [PK_CorporateEventsType] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE dbo.CorporateEvents WITH NOCHECK ADD CONSTRAINT
	FK_CorporateEvents_CorporateEventsType FOREIGN KEY
	(
	[type]
	) REFERENCES dbo.CorporateEventsType
	(
	id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
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
	FK_Credit_Corporates FOREIGN KEY
	(
	corporate_id
	) REFERENCES dbo.Corporates
	(
	id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	 NOT FOR REPLICATION
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[InsertionType]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[InsertionType](
	[id] [int] NOT NULL,
	[code] [varchar](50) NULL,
 CONSTRAINT [PK_InsertionType] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[LegalForm]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[LegalForm](
	[id] [int] NOT NULL,
	[code] [nvarchar](50) NULL,
 CONSTRAINT [PK_LegalForm] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CorporateDomainOfActivity]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[CorporateDomainOfActivity](
	[id] [int] NOT NULL,
	[code] [varchar](50) NULL,
 CONSTRAINT [PK_CorporateDomainOfActivity] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO

ALTER TABLE dbo.Corporates ADD
	[sigle] [nvarchar](50) NULL,
	[small_name] [nvarchar](50) NULL,
	[volunteer_count] [int] NULL,
	[agrement_date] [datetime] NULL,
	[agrement_solidarity] [bit] NULL,
	[insertion_type_id] [int] NULL,
	[employee_count] [int] NULL,
	[siret] [nvarchar](50) NULL,
	[corporate_domain_activity_id] [int] NULL,
	[legalForm_id] [int] NULL,
	[date_create] [datetime] NULL,
	[rcs] [nvarchar](50) NULL
GO

ALTER TABLE dbo.Corporates WITH NOCHECK ADD CONSTRAINT
	FK_Corporates_CorporateDomainOfActivity FOREIGN KEY
	(
	corporate_domain_activity_id
	) REFERENCES dbo.CorporateDomainOfActivity
	(
	id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
GO

ALTER TABLE dbo.Corporates WITH NOCHECK ADD CONSTRAINT
	FK_Corporates_InsertionType FOREIGN KEY
	(
	insertion_type_id
	) REFERENCES dbo.InsertionType
	(
	id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
GO

ALTER TABLE dbo.Corporates WITH NOCHECK ADD CONSTRAINT
	FK_Corporates_LegalForm FOREIGN KEY
	(
	legalForm_id
	) REFERENCES dbo.LegalForm
	(
	id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
GO

CREATE TABLE [dbo].[Roles](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[code] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[CorporatePersonBelonging](
	[corporate_id] [int] NOT NULL,
	[person_id] [int] NOT NULL,
	[role_id] [int],
 CONSTRAINT [PK_CorporatePersonBelonging] PRIMARY KEY CLUSTERED 
(
	[corporate_id] ASC,
	[person_id] ASC
	
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[CorporatePersonBelonging]  WITH CHECK ADD  CONSTRAINT [FK_CorporatePersonBelonging_Corporates] FOREIGN KEY([corporate_id])
REFERENCES [dbo].[Corporates] ([id])
GO

ALTER TABLE [dbo].[CorporatePersonBelonging]  WITH CHECK ADD  CONSTRAINT [FK_CorporatePersonBelonging_Persons] FOREIGN KEY([person_id])
REFERENCES [dbo].[Persons] ([id])
GO

CREATE TABLE [dbo].[ProjectPurposes](
	[name] [nvarchar](200) NOT NULL
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Projects] ADD
	[begin_date] [datetime] NULL,
	[abilities] [nvarchar](50) NULL,
	[experience] [nvarchar](50) NULL,
	[market] [nvarchar](50) NULL,
	[concurrence] [nvarchar](50) NULL,
	[purpose] [nvarchar](50) NULL
GO

UPDATE [Projects] SET [begin_date] = '01/01/2008'
GO

ALTER TABLE dbo.Tiers ADD
	home_type nvarchar(50) NULL,
	e_mail nvarchar(50) NULL,
	secondary_home_type nvarchar(50) NULL,
	secondary_e_mail nvarchar(50) NULL
GO

ALTER TABLE dbo.Persons ADD
	birth_place nvarchar(50) NULL,
	nationality nvarchar(50) NULL,
	IBAN nvarchar(50) NULL,
	study_level nvarchar(50) NULL,
	SS nvarchar(50) NULL,
	CAF nvarchar(50) NULL,
	housing_situation nvarchar(50) NULL,
	bank_address nvarchar(50) NULL,
	handicapped bit NOT NULL CONSTRAINT DF_Credit_handicapped DEFAULT 0,
	professional_experience nvarchar(50) NULL,
	professional_situation nvarchar(50) NULL,
	first_contact datetime NULL,
	sponsor nvarchar(50) NULL,
	family_situation nvarchar(50) NULL
GO

CREATE TABLE [dbo].[ProfessionalSituations](
	[name] [nvarchar](50) NOT NULL
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[ProfessionalExperience](
	[name] [nvarchar](50) NOT NULL
) ON [PRIMARY]
GO

ALTER TABLE dbo.Tiers 	
	DROP CONSTRAINT CK_TiersTypeCode
GO

ALTER TABLE dbo.Tiers WITH NOCHECK ADD CONSTRAINT
	CK_TiersTypeCode CHECK (([client_type_code] = 'G' or [client_type_code] = 'I' or [client_type_code] = 'C'))
GO

ALTER TABLE dbo.Packages
 	DROP CONSTRAINT CK_Packages
GO
ALTER TABLE dbo.Packages WITH NOCHECK ADD CONSTRAINT
	CK_Packages CHECK NOT FOR REPLICATION (([client_type] = 'I' or [client_type] = 'G' or [client_type] = '-' or [client_type] = 'C'))
GO

CREATE TABLE [dbo].[HousingSituation](
	[name] [nvarchar](50) NOT NULL
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[GuaranteesPackages](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[deleted] [bit] NOT NULL,
	[name] [nvarchar](100) NULL,
	[client_type] [char](1) NULL,
	[amount] [money] NULL,
	[amount_min] [money] NULL,
	[amount_max] [money] NULL,
	[amount_limit] [money] NULL,
	[amount_limit_min] [money] NULL,
	[amount_limit_max] [money] NULL,
	[rate] [float] NULL,
	[rate_min] [float] NULL,
	[rate_max] [float] NULL,
	[rate_limit] [float] NULL,
	[rate_limit_min] [float] NULL,
	[rate_limit_max] [float] NULL,
	[guaranted_amount] [money] NULL,
	[guaranted_amount_min] [money] NULL,
	[guaranted_amount_max] [money] NULL,
	[guarantee_fees] [float] NULL,
	[guarantee_fees_min] [float] NULL,
	[guarantee_fees_max] [float] NULL,
	[fundingLine_id] [int] NOT NULL,
	[corporate_id] [int] NOT NULL,
 CONSTRAINT [PK_GaranteesPackages] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[GuaranteesPackages]  WITH CHECK ADD  CONSTRAINT [FK_GaranteesPackages_Corporates] FOREIGN KEY([corporate_id])
REFERENCES [dbo].[Corporates] ([id])
GO

ALTER TABLE [dbo].[GuaranteesPackages] CHECK CONSTRAINT [FK_GaranteesPackages_Corporates]
GO

ALTER TABLE [dbo].[GuaranteesPackages]  WITH CHECK ADD  CONSTRAINT [FK_GaranteesPackages_FundingLines] FOREIGN KEY([fundingLine_id])
REFERENCES [dbo].[FundingLines] ([id])
GO

ALTER TABLE [dbo].[GuaranteesPackages] CHECK CONSTRAINT [FK_GaranteesPackages_FundingLines]
GO

CREATE TABLE [dbo].[Guarantees](
	[id] [int] NOT NULL,
	[guarantee_package_id] [int] NOT NULL,
	[amount] [money] NULL,
	[amount_limit] [money] NULL,
	[rate] [float] NULL,
	[rate_limit] [float] NULL,
	[amount_guaranted] [money] NULL,
	[guarantee_fees] [float] NULL,
	[fundingLine_id] [int] NOT NULL,
	[corporate_id] [int] NOT NULL,
	[activated] [bit] NOT NULL CONSTRAINT [DF_Guarantees_activated]  DEFAULT ((0)),
	[loanofficer_id] [int] NOT NULL,
 CONSTRAINT [PK_Garantees] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Guarantees]  WITH CHECK ADD  CONSTRAINT [FK_Garantees_Corporates] FOREIGN KEY([corporate_id])
REFERENCES [dbo].[Corporates] ([id])
GO

ALTER TABLE [dbo].[Guarantees] CHECK CONSTRAINT [FK_Garantees_Corporates]
GO

ALTER TABLE [dbo].[Guarantees]  WITH CHECK ADD  CONSTRAINT [FK_Garantees_GaranteesPackages] FOREIGN KEY([guarantee_package_id])
REFERENCES [dbo].[GuaranteesPackages] ([id])
GO

ALTER TABLE [dbo].[Guarantees] CHECK CONSTRAINT [FK_Garantees_GaranteesPackages]
GO

ALTER TABLE [dbo].[Guarantees]  WITH CHECK ADD  CONSTRAINT [FK_Guarantees_Contracts] FOREIGN KEY([id])
REFERENCES [dbo].[Contracts] ([id])
GO

ALTER TABLE [dbo].[Guarantees] CHECK CONSTRAINT [FK_Guarantees_Contracts]
GO

ALTER TABLE [dbo].[Guarantees]  WITH CHECK ADD  CONSTRAINT [FK_Guarantees_FundingLines] FOREIGN KEY([fundingLine_id])
REFERENCES [dbo].[FundingLines] ([id])
GO

ALTER TABLE [dbo].[Guarantees] CHECK CONSTRAINT [FK_Guarantees_FundingLines]
GO

ALTER TABLE [dbo].[Guarantees]  WITH CHECK ADD  CONSTRAINT [FK_Guarantees_Users] FOREIGN KEY([loanofficer_id])
REFERENCES [dbo].[Users] ([id])
GO

ALTER TABLE [dbo].[Guarantees] CHECK CONSTRAINT [FK_Guarantees_Users]
GO

ALTER TABLE dbo.Tiers ADD
	status smallint NOT NULL CONSTRAINT DF_Tiers_status DEFAULT 1
GO

ALTER TABLE dbo.Contracts ADD
	status smallint NOT NULL CONSTRAINT DF_Contracts_status DEFAULT 1,
	credit_commitee_date datetime NULL,
	credit_commitee_comment nvarchar(200) NULL
GO


INSERT INTO [CorporateEventsType]([id], [code]) VALUES(1, 'Disbursment')
INSERT INTO [CorporateEventsType]([id], [code]) VALUES(2, 'Repay')
INSERT INTO [CorporateEventsType]([id], [code]) VALUES(3, 'Loan Corporate')
INSERT INTO [CorporateEventsType]([id], [code]) VALUES(4, 'Guarantee')
GO
UPDATE [Contracts] SET [status] = 2 WHERE 
(
SELECT disbursed FROM Credit WHERE Contracts.Id = Credit.Id
) = 0 AND closed = 0
GO

UPDATE [Contracts] SET [status] = 5 WHERE 
(
SELECT disbursed FROM Credit WHERE Contracts.Id = Credit.Id
) = 1 AND closed = 0
GO

UPDATE [Contracts] SET [status] = 6 WHERE closed = 1
GO

/************** VERSION ***********************/
UPDATE [TechnicalParameters] SET [value]='v1.2.0'
GO
