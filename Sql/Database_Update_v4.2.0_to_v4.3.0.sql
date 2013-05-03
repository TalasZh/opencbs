
DECLARE @db_from NVARCHAR(MAX),--current db
@db_to  NVARCHAR(MAX)--attachments db

SELECT @db_from = DB_NAME()
SET @db_to = @db_from + '_attachments'

DECLARE @sql NVARCHAR(MAX)

IF EXISTS (SELECT NAME FROM master..sysdatabases WHERE NAME = @db_to)
BEGIN
SET @sql = 
	'IF EXISTS (SELECT * FROM ' + @db_to + '.[sys].[tables] WHERE name= ''ClientDocuments'')
	BEGIN
		USE '+ @db_to +
		' IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N''[dbo].[ClientDocuments]'') AND type in (N''U'')) 
		EXEC sp_rename  ''ClientDocuments'', ''Documents''
	END'
EXEC sp_executesql @sql
END
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ContractAccountingRules_GuaranteesPackages]') AND parent_object_id = OBJECT_ID(N'[dbo].[ContractAccountingRules]'))
ALTER TABLE [dbo].[ContractAccountingRules] DROP CONSTRAINT [FK_ContractAccountingRules_GuaranteesPackages]
GO

ALTER TABLE [ContractAccountingRules] DROP COLUMN [guarantee_product_id]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_GaranteesPackages_FundingLines]') AND parent_object_id = OBJECT_ID(N'[dbo].[GuaranteesPackages]'))
ALTER TABLE [dbo].[GuaranteesPackages] DROP CONSTRAINT [FK_GaranteesPackages_FundingLines]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_GuaranteesPackages_Currencies]') AND parent_object_id = OBJECT_ID(N'[dbo].[GuaranteesPackages]'))
ALTER TABLE [dbo].[GuaranteesPackages] DROP CONSTRAINT [FK_GuaranteesPackages_Currencies]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Guarantees]') AND type in (N'U'))
DROP TABLE [dbo].[Guarantees]
GO


IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GuaranteesPackages]') AND type in (N'U'))
DROP TABLE [dbo].[GuaranteesPackages]
GO

ALTER TABLE dbo.Questionnaire ADD is_sent BIT NOT NULL DEFAULT((0))
GO

CREATE TABLE ClientBranchHistory
(
	id INT IDENTITY(1, 1) NOT NULL,
	date_changed DATETIME,
	branch_from_id INT NOT NULL,
	branch_to_id INT NOT NULL,
	client_id INT NOT NULL,
	user_id INT NOT NULL
)
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ClientBranchHistory_Branches]') AND parent_object_id = OBJECT_ID(N'[dbo].[ClientBranchHistory]'))
ALTER TABLE [dbo].[ClientBranchHistory]  WITH CHECK ADD  CONSTRAINT [FK_ClientBranchHistory_Branches] FOREIGN KEY([branch_from_id])
REFERENCES [dbo].[Branches] ([id])
GO


IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ClientBranchHistory_Branches]') AND parent_object_id = OBJECT_ID(N'[dbo].[ClientBranchHistory]'))
ALTER TABLE [dbo].[ClientBranchHistory]  WITH CHECK ADD  CONSTRAINT [FK_ClientBranchHistory_Branches] FOREIGN KEY([branch_to_id])
REFERENCES [dbo].[Branches] ([id])
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ClientBranchHistory_Users]') AND parent_object_id = OBJECT_ID(N'[dbo].[ClientBranchHistory]'))
ALTER TABLE [dbo].[ClientBranchHistory]  WITH CHECK ADD  CONSTRAINT [FK_ClientBranchHistory_Users] FOREIGN KEY([user_id])
REFERENCES [dbo].[Users] ([id])
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ClientBranchHistory_Tiers]') AND parent_object_id = OBJECT_ID(N'[dbo].[ClientBranchHistory]'))
ALTER TABLE [dbo].[ClientBranchHistory]  WITH CHECK ADD  CONSTRAINT [FK_ClientBranchHistory_Tiers] FOREIGN KEY([client_id])
REFERENCES [dbo].[Tiers] ([id])
GO

INSERT INTO [GeneralParameters]([key], [value]) VALUES('STOP_WRITEOFF_PENALTY', 0)
GO

ALTER TABLE dbo.TraceUserLogs ALTER COLUMN event_description NVARCHAR(MAX)
GO

ALTER TABLE dbo.Corporates ALTER COLUMN name NVARCHAR(MAX) NOT NULL
GO

IF(NOT EXISTS(SELECT 1 FROM CreditScoringQuestions))
BEGIN	
	DROP TABLE CreditScoringValues
	DROP TABLE CreditScoringAnswers
	DROP TABLE CreditScoringQuestions
END
GO

UPDATE  [TechnicalParameters]
SET     [value] = 'v4.3.0'
WHERE   [name] = 'VERSION'
GO
