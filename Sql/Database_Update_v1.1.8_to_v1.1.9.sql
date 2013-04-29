CREATE TABLE dbo.LoanInterestAccruingEvents
	(
	id int NOT NULL,
	interest_prepayment money NOT NULL,
	accrued_interest money NOT NULL,
	rescheduled bit NOT NULL
	)  ON [PRIMARY]
GO

ALTER TABLE dbo.LoanInterestAccruingEvents ADD CONSTRAINT
	PK_LoanInterestAccruingEvents PRIMARY KEY CLUSTERED 
	(
	id
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO

ALTER TABLE dbo.ContractEvents WITH NOCHECK ADD CONSTRAINT
	FK_ContractEvents_LoanInterestAccruingEvents FOREIGN KEY
	(
	id
	) REFERENCES dbo.LoanInterestAccruingEvents
	(
	id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	 NOT FOR REPLICATION

GO

ALTER TABLE dbo.ContractEvents
	NOCHECK CONSTRAINT FK_ContractEvents_LoanInterestAccruingEvents
GO

CREATE TABLE [dbo].[Projects](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[tiers_id] [int] NOT NULL,
	[status] [smallint] NOT NULL,
	[name] [nvarchar](50) NOT NULL,
	[code] [nvarchar](50) NOT NULL,
	[aim] [nvarchar](200) NOT NULL
 CONSTRAINT [PK_Projects2] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
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
ALTER TABLE dbo.Contracts ADD
	project_id int NOT NULL CONSTRAINT DF_Contracts_project_id DEFAULT 0
GO

INSERT INTO Projects(tiers_id,[status],[name],[code],[aim])
SELECT DISTINCT beneficiary_id AS tiers_id,0 AS [status],'NotSet','NotSet','NotSet' FROM Contracts 
GO

UPDATE Contracts  SET project_id  =
(SELECT  Projects.id FROM Projects WHERE ( tiers_id = beneficiary_id ))
GO

ALTER TABLE dbo.Contracts ADD CONSTRAINT
	FK_Contracts_Projects FOREIGN KEY
	(
	project_id
	) REFERENCES dbo.Projects
	(
	id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.Contracts
	DROP CONSTRAINT FK_Contracts_Beneficiary
GO

ALTER TABLE dbo.Contracts
	DROP COLUMN beneficiary_id
GO

ALTER TABLE dbo.FundingLines ADD
	begin_date datetime NULL,
	end_date datetime NULL,
	amount decimal NULL,
	purpose nvarchar(50) NULL,
	residual_amount money NULL
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_EVENTBODYCORPORATE_BODYCORPORATE]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[EventBodyCorporates] DROP CONSTRAINT FK_EVENTBODYCORPORATE_BODYCORPORATE
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_TEMP_BODYCORPORATE_FUNDINGLINE]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[BodyCorporates] DROP CONSTRAINT FK_TEMP_BODYCORPORATE_FUNDINGLINE 
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[BodyCorporates]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[BodyCorporates]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[EventBodyCorporates]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[EventBodyCorporates]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Temp_FundingLines]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[Temp_FundingLines]
GO

CREATE TABLE dbo.Temp_FundingLines
(
	[id] [int] IDENTITY(1,1) NOT NULL,
	[code] [nvarchar](50) NOT NULL,
	[name] [nvarchar](50) NULL,
	[begin_date] [datetime] NOT NULL,
	[end_date] [datetime] NOT NULL,
	[amount] [decimal](18, 0) NOT NULL,
	[purpose] [nvarchar](50) NOT NULL,
	[residual_amount] [money] NULL,
	[deleted] [bit] NOT NULL,
)ON [PRIMARY]


GO 
CREATE TABLE dbo.BodyCorporates
	(
	[id] int IDENTITY(1,1) NOT NULL,
	[code]  nvarchar(50) NOT NULL,
	[name] nvarchar(50) NOT NULL,
	[amount] money NULL,
	[begin_date] dateTime NOT NULL,
	[end_date] dateTime NOT NULL,
	[fundingLine_id] int NOT NULL,
	[deleted] bit NOT NULL

	)  ON [PRIMARY] 
GO

CREATE TABLE dbo.EventBodyCorporates
	(
	[id] [int] IDENTITY(1,1) NOT NULL,
	[code] [nvarchar](50) NOT NULL,
	[amount] [money] NULL,
	[mouvement] [smallint] NOT NULL,
	[bodyCorporate_id] [int] NOT NULL,
	[deleted] [bit] NOT NULL,
	[begin_date] [datetime] NOT NULL,
	[end_date] [datetime] NOT NULL,

	)  ON [PRIMARY] 
GO

ALTER TABLE Temp_FundingLines ADD CONSTRAINT PK_TEMP_FUNDINGLINES_1
	PRIMARY KEY (id)
GO

ALTER TABLE EventBodyCorporates ADD CONSTRAINT PK_EVENTBODYCORPORATE
	PRIMARY KEY (id)
GO

ALTER TABLE BodyCorporates ADD CONSTRAINT PK_BODYCORPORATE
	PRIMARY KEY (id)
GO

ALTER TABLE BodyCorporates ADD CONSTRAINT FK_TEMP_BODYCORPORATE_FUNDINGLINE 
	FOREIGN KEY (fundingLine_id) REFERENCES Temp_Fundinglines (id)
GO

ALTER TABLE EventBodyCorporates ADD CONSTRAINT FK_EVENTBODYCORPORATE_BODYCORPORATE 
	FOREIGN KEY (bodyCorporate_id) REFERENCES BodyCorporates (id)
GO

/************** VERSION ***********************/
UPDATE [TechnicalParameters] SET [value]='v1.1.9'
GO
