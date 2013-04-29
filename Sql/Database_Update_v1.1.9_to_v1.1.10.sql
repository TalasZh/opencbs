ALTER TABLE dbo.Tiers ADD
	home_phone varchar(50) NULL,
	personal_phone varchar(50) NULL,
	secondary_home_phone varchar(50) NULL,
	secondary_personal_phone varchar(50) NULL
GO
ALTER TABLE dbo.BodyCorporates 	DROP CONSTRAINT FK_TEMP_BODYCORPORATE_FUNDINGLINE
GO
DROP TABLE dbo.Temp_FundingLines
GO
ALTER TABLE dbo.EventBodyCorporates 	DROP CONSTRAINT FK_EVENTBODYCORPORATE_BODYCORPORATE
GO
DROP TABLE dbo.BodyCorporates
GO
DROP TABLE dbo.EventBodyCorporates
GO
CREATE TABLE [dbo].[Temp_FundingLines](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](50) NOT NULL,
	[begin_date] [datetime] NOT NULL,
	[end_date] [datetime] NOT NULL,
	[amount] [decimal](18, 0) NOT NULL,
	[purpose] [nvarchar](50) NOT NULL,
	[residual_amount] [money] NOT NULL,
	[deleted] [bit] NOT NULL,
 CONSTRAINT [PK_TEMP_FUNDINGLINES_1] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE TABLE [dbo].[Corporates](
	[id] [int] NOT NULL,
	[name] [nvarchar](50) NOT NULL,
	[amount] [money] NOT NULL,
	[deleted] [bit] NOT NULL,
 CONSTRAINT [PK_BODYCORPORATE] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE TABLE [dbo].[CorporateFundingLineBelonging](
	[fundingLine_id] [int] NOT NULL,
	[corporate_id] [int] NOT NULL,
	[currently_in] [bit] NOT NULL,
 CONSTRAINT [PK_CorporateFundingLineBelonging] PRIMARY KEY CLUSTERED 
(
	[fundingLine_id] ASC,
	[corporate_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[CorporateFundingLineBelonging]  WITH NOCHECK ADD  CONSTRAINT [FK_CorporateFundingLineBelonging_Corporates] FOREIGN KEY([corporate_id])
REFERENCES [dbo].[Corporates] ([id])
NOT FOR REPLICATION 
GO
ALTER TABLE [dbo].[CorporateFundingLineBelonging] CHECK CONSTRAINT [FK_CorporateFundingLineBelonging_Corporates]
GO
ALTER TABLE [dbo].[CorporateFundingLineBelonging]  WITH NOCHECK ADD  CONSTRAINT [FK_CorporateFundingLineBelonging_Temp_FundingLines] FOREIGN KEY([fundingLine_id])
REFERENCES [dbo].[Temp_FundingLines] ([id])
NOT FOR REPLICATION 
GO
ALTER TABLE [dbo].[CorporateFundingLineBelonging] CHECK CONSTRAINT [FK_CorporateFundingLineBelonging_Temp_FundingLines]
GO
CREATE TABLE [dbo].[CorporateEvents](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[code] [nvarchar](50) NOT NULL,
	[amount] [money] NOT NULL,
	[mouvement] [smallint] NOT NULL,
	[corporate_id] [int] NOT NULL,
	[deleted] [bit] NOT NULL,
	[creation_date] [datetime] NOT NULL,
 CONSTRAINT [PK_EVENTBODYCORPORATE] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE dbo.Credit
	DROP CONSTRAINT FK_Credit_FundingLines
GO


ALTER TABLE dbo.Packages ADD
	fundingLine_id int NULL
GO

ALTER TABLE dbo.Credit ADD
	fundingLine_id int NULL
GO

INSERT INTO Temp_FundingLines ([name],[begin_date],[end_date],[amount],[purpose],[residual_amount],[deleted])  
SELECT code AS [name],'01/01/2006' AS begin_date, '01/01/2010' AS end_date, 1000 AS amount, code AS purpose,
1000 AS residual_amount, 0 AS deleted FROM FundingLines
GO
	
DECLARE @FUNDINGLINE_ID int
DECLARE @CORPORATE_ID int
DECLARE @FUNDINGLINE_CODE [nvarchar](200)
DECLARE @DISTRICT_ID int

DECLARE CURSOR_FUNDINGLINE_ID CURSOR FOR
SELECT id,[name] FROM Temp_FundingLines

OPEN CURSOR_FUNDINGLINE_ID;
FETCH NEXT FROM CURSOR_FUNDINGLINE_ID INTO @FUNDINGLINE_ID,@FUNDINGLINE_CODE

WHILE @@FETCH_STATUS=0
BEGIN
	SELECT TOP 1 @DISTRICT_ID = id FROM Districts
	INSERT INTO [Tiers]([client_type_code],[loan_cycle],[active],[bad_client],[district_id],[city]) VALUES ('I',1,1,0, @DISTRICT_ID,'NotSet')
	SET @CORPORATE_ID = SCOPE_IDENTITY()
	INSERT INTO [Corporates]([id],[name],[amount],[deleted]) VALUES(@CORPORATE_ID,@FUNDINGLINE_CODE,1000,0)
	INSERT INTO [CorporateFundingLineBelonging] (corporate_id, fundingLine_id, currently_in) VALUES (@CORPORATE_ID ,@FUNDINGLINE_ID, 1)
	INSERT INTO [CorporateEvents] ([code],[amount],[mouvement],[corporate_id],[deleted],[creation_date]) VALUES (@FUNDINGLINE_CODE,1000,1,@CORPORATE_ID,0,'01/01/2008')
	
	FETCH NEXT FROM CURSOR_FUNDINGLINE_ID INTO @FUNDINGLINE_ID,@FUNDINGLINE_CODE
END
CLOSE		CURSOR_FUNDINGLINE_ID
DEALLOCATE	CURSOR_FUNDINGLINE_ID
GO

UPDATE Credit SET fundingLine_id = (SELECT TOP 1 id FROM Temp_FundingLines WHERE Temp_FundingLines.name = Credit.funding_line_code)
GO

ALTER TABLE dbo.CorporateEvents ADD CONSTRAINT
	FK_CorporateEvents_Corporates FOREIGN KEY
	(
	corporate_id
	) REFERENCES dbo.Corporates
	(
	id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	 NOT FOR REPLICATION
GO

ALTER TABLE dbo.Credit WITH NOCHECK ADD CONSTRAINT
	FK_Credit_Temp_FundingLines FOREIGN KEY
	(
	fundingLine_id
	) REFERENCES dbo.Temp_FundingLines
	(
	id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	 NOT FOR REPLICATION

GO

ALTER TABLE dbo.Credit
	DROP COLUMN funding_line_code
GO

DROP TABLE dbo.FundingLines
GO

EXECUTE sp_rename N'dbo.Temp_FundingLines', N'FundingLines', 'OBJECT' 
GO
ALTER TABLE dbo.Credit
	DROP CONSTRAINT FK_Credit_Temp_FundingLines
GO

ALTER TABLE dbo.Credit
	DROP CONSTRAINT FK_Credit_Contracts
GO

ALTER TABLE dbo.Credit
	DROP CONSTRAINT FK_Credit_InstallmentTypes
GO

ALTER TABLE dbo.Credit
	DROP CONSTRAINT FK_Credit_Users
GO

ALTER TABLE dbo.Credit
	DROP CONSTRAINT FK_Credit_Collaterals
GO

ALTER TABLE dbo.Credit
	DROP CONSTRAINT FK_Credit_Packages
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[DF_Credit_write_off]') AND parent_object_id = OBJECT_ID(N'[dbo].[Credit]'))
ALTER TABLE [dbo].[Credit] DROP CONSTRAINT [DF_Credit_write_off]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[DF_Credit_reschedule]') AND parent_object_id = OBJECT_ID(N'[dbo].[Credit]'))
ALTER TABLE [dbo].[Credit] DROP CONSTRAINT [DF_Credit_reschedule]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[DF_Credit_bad_loan]') AND parent_object_id = OBJECT_ID(N'[dbo].[Credit]'))
ALTER TABLE [dbo].[Credit] DROP CONSTRAINT [DF_Credit_bad_loan]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[DF_Credit_non_repayment_penalties_based_on_overdue_principal]') AND parent_object_id = OBJECT_ID(N'[dbo].[Credit]'))
ALTER TABLE [dbo].[Credit] DROP CONSTRAINT [DF_Credit_non_repayment_penalties_based_on_overdue_principal]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[DF_Credit_non_repayment_penalties_based_on_initial_amount]') AND parent_object_id = OBJECT_ID(N'[dbo].[Credit]'))
ALTER TABLE [dbo].[Credit] DROP CONSTRAINT [DF_Credit_non_repayment_penalties_based_on_initial_amount]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[DF_Credit_non_repayment_penalties_based_on_olb]') AND parent_object_id = OBJECT_ID(N'[dbo].[Credit]'))
ALTER TABLE [dbo].[Credit] DROP CONSTRAINT [DF_Credit_non_repayment_penalties_based_on_olb]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[DF_Credit_non_repayment_penalties_based_on_overdue_interest]') AND parent_object_id = OBJECT_ID(N'[dbo].[Credit]'))
ALTER TABLE [dbo].[Credit] DROP CONSTRAINT [DF_Credit_non_repayment_penalties_based_on_overdue_interest]
GO

CREATE TABLE dbo.Tmp_Credit
	(
	id int NOT NULL,
	package_id int NOT NULL,
	amount money NOT NULL,
	interest_rate float(53) NOT NULL,
	installment_type int NOT NULL,
	nb_of_installment int NOT NULL,
	anticipated_total_repayment_penalties float(53) NOT NULL,
	disbursed bit NOT NULL,
	loanofficer_id int NOT NULL,
	entry_fees float(53) NOT NULL,
	grace_period int NULL,
	written_off bit NOT NULL,
	rescheduled bit NOT NULL,
	collateral_amount money NULL,
	bad_loan bit NOT NULL,
	comments_of_end nvarchar(1000) NULL,
	collateral_id int NULL,
	non_repayment_penalties_based_on_overdue_principal float(53) NOT NULL,
	non_repayment_penalties_based_on_initial_amount float(53) NOT NULL,
	non_repayment_penalties_based_on_olb float(53) NOT NULL,
	non_repayment_penalties_based_on_overdue_interest float(53) NOT NULL,
	fundingLine_id int NOT NULL
	)  ON [PRIMARY]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[DF_Credit_write_off]') AND parent_object_id = OBJECT_ID(N'[Tmp_Credit]'))
ALTER TABLE dbo.Tmp_Credit ADD CONSTRAINT DF_Credit_write_off DEFAULT ((0)) FOR written_off
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[DF_Credit_reschedule]') AND parent_object_id = OBJECT_ID(N'[Tmp_Credit]'))
ALTER TABLE dbo.Tmp_Credit ADD CONSTRAINT DF_Credit_reschedule DEFAULT ((0)) FOR rescheduled
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[DF_Credit_bad_loan]') AND parent_object_id = OBJECT_ID(N'[Tmp_Credit]'))
ALTER TABLE dbo.Tmp_Credit ADD CONSTRAINT DF_Credit_bad_loan DEFAULT ((0)) FOR bad_loan
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[DF_Credit_non_repayment_penalties_based_on_overdue_principal]') AND parent_object_id = OBJECT_ID(N'[Tmp_Credit]'))
ALTER TABLE dbo.Tmp_Credit ADD CONSTRAINT DF_Credit_non_repayment_penalties_based_on_overdue_principal DEFAULT ((0)) FOR non_repayment_penalties_based_on_overdue_principal
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[DF_Credit_non_repayment_penalties_based_on_initial_amount]') AND parent_object_id = OBJECT_ID(N'[Tmp_Credit]'))
ALTER TABLE dbo.Tmp_Credit ADD CONSTRAINT DF_Credit_non_repayment_penalties_based_on_initial_amount DEFAULT ((0)) FOR non_repayment_penalties_based_on_initial_amount
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[DF_Credit_non_repayment_penalties_based_on_olb]') AND parent_object_id = OBJECT_ID(N'[Tmp_Credit]'))
ALTER TABLE dbo.Tmp_Credit ADD CONSTRAINT DF_Credit_non_repayment_penalties_based_on_olb DEFAULT ((0)) FOR non_repayment_penalties_based_on_olb
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[DF_Credit_non_repayment_penalties_based_on_overdue_interest]') AND parent_object_id = OBJECT_ID(N'[Tmp_Credit]'))
ALTER TABLE dbo.Tmp_Credit ADD CONSTRAINT DF_Credit_non_repayment_penalties_based_on_overdue_interest DEFAULT ((0)) FOR non_repayment_penalties_based_on_overdue_interest
GO

IF EXISTS(SELECT * FROM dbo.Credit)
	 EXEC('INSERT INTO dbo.Tmp_Credit (id, package_id, amount, interest_rate, installment_type, nb_of_installment, anticipated_total_repayment_penalties, disbursed, loanofficer_id, entry_fees, grace_period, written_off, rescheduled, collateral_amount, bad_loan, comments_of_end, collateral_id, non_repayment_penalties_based_on_overdue_principal, non_repayment_penalties_based_on_initial_amount, non_repayment_penalties_based_on_olb, non_repayment_penalties_based_on_overdue_interest, fundingLine_id)
		SELECT id, package_id, amount, interest_rate, installment_type, nb_of_installment, anticipated_total_repayment_penalties, disbursed, loanofficer_id, entry_fees, grace_period, written_off, rescheduled, collateral_amount, bad_loan, comments_of_end, collateral_id, non_repayment_penalties_based_on_overdue_principal, non_repayment_penalties_based_on_initial_amount, non_repayment_penalties_based_on_olb, non_repayment_penalties_based_on_overdue_interest, fundingLine_id FROM dbo.Credit WITH (HOLDLOCK TABLOCKX)')
GO

ALTER TABLE dbo.Installments
	DROP CONSTRAINT FK_Installments_Credit
GO

DROP TABLE dbo.Credit
GO

EXECUTE sp_rename N'dbo.Tmp_Credit', N'Credit', 'OBJECT' 
GO

ALTER TABLE dbo.Credit ADD CONSTRAINT
	PK_Credit PRIMARY KEY CLUSTERED 
	(
	id
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO

CREATE NONCLUSTERED INDEX IX_Credit_packageId ON dbo.Credit
	(
	package_id
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX IX_Credit_installmentsTypeId ON dbo.Credit
	(
	installment_type
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX IX_Credit_loanOfficerId ON dbo.Credit
	(
	loanofficer_id
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX IX_Credit_fundingLine ON dbo.Credit
	(
	id
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
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

ALTER TABLE dbo.Credit WITH NOCHECK ADD CONSTRAINT
	FK_Credit_Collaterals FOREIGN KEY
	(
	collateral_id
	) REFERENCES dbo.Collaterals
	(
	id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO

ALTER TABLE dbo.Credit WITH NOCHECK ADD CONSTRAINT
	FK_Credit_Users FOREIGN KEY
	(
	loanofficer_id
	) REFERENCES dbo.Users
	(
	id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO

ALTER TABLE dbo.Credit
	NOCHECK CONSTRAINT FK_Credit_Users
GO

ALTER TABLE dbo.Credit WITH NOCHECK ADD CONSTRAINT
	FK_Credit_InstallmentTypes FOREIGN KEY
	(
	installment_type
	) REFERENCES dbo.InstallmentTypes
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

ALTER TABLE dbo.Credit WITH NOCHECK ADD CONSTRAINT
	FK_Credit_Temp_FundingLines FOREIGN KEY
	(
	fundingLine_id
	) REFERENCES dbo.FundingLines
	(
	id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	 NOT FOR REPLICATION

GO

ALTER TABLE dbo.Installments WITH NOCHECK ADD CONSTRAINT
	FK_Installments_Credit FOREIGN KEY
	(
	contract_id
	) REFERENCES dbo.Credit
	(
	id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.Installments
	NOCHECK CONSTRAINT FK_Installments_Credit
GO
/************** VERSION ***********************/
UPDATE [TechnicalParameters] SET [value]='v1.1.10'
GO
