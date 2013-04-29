ALTER TABLE dbo.Accounts
ADD [type] BIT NOT NULL DEFAULT 0
GO

UPDATE dbo.Accounts
SET [type] = 1
GO

CREATE TABLE [dbo].[FundingLineEvents](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[code] [nvarchar](200) NOT NULL,
	[amount] [money] NOT NULL,
	[direction] [smallint] NOT NULL,
	[fundingline_id] [int] NOT NULL DEFAULT (1),
	[deleted] [bit] NOT NULL,
	[creation_date] [datetime] NOT NULL,
	[type] [smallint] NOT NULL,
 CONSTRAINT [PK_EVENTFUNDINGLINE] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[FundingLineEvents]  WITH NOCHECK ADD  CONSTRAINT [FK_FundingLineEvents_FundingLines] FOREIGN KEY([fundingline_id])
REFERENCES [dbo].[FundingLines] ([id])
NOT FOR REPLICATION 
GO

ALTER TABLE [dbo].[FundingLineEvents] CHECK CONSTRAINT [FK_FundingLineEvents_FundingLines]
GO

--Fill funding line events table
INSERT INTO FundingLineEvents
(code, amount, direction, deleted, creation_date, type, fundingline_id)
SELECT ce.code, ce.amount, ce.direction, ce.deleted, ce.creation_date, ce.[type], 
t.fundingLine_id
FROM 
dbo.CorporateEvents ce
INNER JOIN 
(
SELECT ROW_NUMBER() OVER (PARTITION BY cfb.corporate_id ORDER BY cfb.corporate_id) AS RowNo, 
cfb.fundingLine_id, cfb.corporate_id
FROM dbo.CorporateFundingLineBelonging cfb ) t ON ce.corporate_id = t.corporate_id WHERE t.RowNo=1
GO

DECLARE @fle_id int

DECLARE FLE_cursor CURSOR FOR
SELECT id
FROM [dbo].[FundingLineEvents]
WHERE fundingline_id = null
OPEN FLE_cursor 
FETCH NEXT FROM FLE_cursor INTO @fle_id
WHILE @@FETCH_STATUS = 0
BEGIN
	UPDATE [dbo].[FundingLineEvents] SET fundingline_id = 1 WHERE id = @fle_id
	FETCH NEXT FROM FLE_cursor INTO @fle_id
END
CLOSE FLE_cursor
DEALLOCATE FLE_cursor
GO

--delete corporate events table
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Garantees_Corporates]') AND parent_object_id = OBJECT_ID(N'[dbo].[Guarantees]'))
ALTER TABLE [dbo].[Guarantees] DROP CONSTRAINT [FK_Garantees_Corporates]
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_CorporateEvents_Corporates]') AND parent_object_id = OBJECT_ID(N'[dbo].[CorporateEvents]'))
ALTER TABLE [dbo].[CorporateEvents] DROP CONSTRAINT [FK_CorporateEvents_Corporates]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CorporateEvents]') AND type = (N'U'))
DROP TABLE [dbo].[CorporateEvents]
GO

--delete CorporateFundingLineBelonging
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_CorporateFundingLineBelonging_Corporates]') AND parent_object_id = OBJECT_ID(N'[dbo].[CorporateFundingLineBelonging]'))
ALTER TABLE [dbo].[CorporateFundingLineBelonging] DROP CONSTRAINT [FK_CorporateFundingLineBelonging_Corporates]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_CorporateFundingLineBelonging_Temp_FundingLines]') AND parent_object_id = OBJECT_ID(N'[dbo].[CorporateFundingLineBelonging]'))
ALTER TABLE [dbo].[CorporateFundingLineBelonging] DROP CONSTRAINT [FK_CorporateFundingLineBelonging_Temp_FundingLines]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CorporateFundingLineBelonging]') AND type in (N'U'))
DROP TABLE [dbo].[CorporateFundingLineBelonging]
GO
-----Delete amount in Corporates
ALTER TABLE Corporates
DROP COLUMN amount
GO

-----Delete corporate_id in Credit
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Credit_Corporates]') AND parent_object_id = OBJECT_ID(N'[dbo].[Credit]'))
ALTER TABLE [dbo].[Credit] DROP CONSTRAINT [FK_Credit_Corporates]
GO

ALTER TABLE Credit
DROP COLUMN corporate_id
GO
-----Delete residual_amount in FundingLines
ALTER TABLE FundingLines
DROP COLUMN residual_amount
GO
-----Delete corporate_id in Packages
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].FK_Packages_Corporates') AND parent_object_id = OBJECT_ID(N'[dbo].[Packages]'))
ALTER TABLE [dbo].[Packages] DROP CONSTRAINT FK_Packages_Corporates
GO

ALTER TABLE Packages
DROP COLUMN corporate_id
GO

-----Delete corporate_id in GuaranteePackages
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].FK_GaranteesPackages_Corporates') AND parent_object_id = OBJECT_ID(N'[dbo].[GuaranteesPackages]'))
ALTER TABLE [dbo].GuaranteesPackages DROP CONSTRAINT FK_GaranteesPackages_Corporates
GO

ALTER TABLE GuaranteesPackages
DROP COLUMN corporate_id
GO

-----Delete corporate_id in Guarantees
ALTER TABLE Guarantees
DROP COLUMN corporate_id
GO

INSERT INTO [GeneralParameters]([key], [value]) VALUES('CONTRACT_CODE_TEMPLATE','BC/YY/PC-LC/ID')
GO

INSERT INTO [GeneralParameters]([key], [value]) VALUES('USE_PROJECTS', 0)
GO


INSERT INTO Accounts (account_number, local_account_number, label, balance, debit_plus, type_code, description, type)
VALUES     (1322, 1322, 'Accounts and Terms Loans', 0, 0, 'ACCOUNTS_AND_TERM_LOANS', 2, 1)
GO

INSERT INTO [GeneralParameters]([key], [value]) VALUES('INTERESTS_ALSO_CREDITED_IN_FL', 0)
GO

UPDATE  [TechnicalParameters] SET [value] = 'v2.5.8'
GO

-----Drop old conso_ Table
IF EXISTS(SELECT name FROM sysobjects WHERE name = 'Conso_Accounts' AND xtype='U') 
DROP TABLE Conso_Accounts
GO

IF EXISTS(SELECT name FROM sysobjects WHERE name = 'Conso_CreditContracts' AND xtype='U') 
DROP TABLE Conso_CreditContracts
GO

IF EXISTS(SELECT name FROM sysobjects WHERE name = 'Conso_Customers' AND xtype='U') 
DROP TABLE Conso_Customers
GO

IF EXISTS(SELECT name FROM sysobjects WHERE name = 'Conso_Details' AND xtype='U') 
DROP TABLE Conso_Details
GO

-----Create Tconso_temp Table
IF NOT EXISTS(SELECT name FROM sysobjects WHERE name = 'Tconso_temp' AND xtype='U') 
CREATE TABLE [dbo].[Tconso_temp] (
[branch_code] [nvarchar] (50) NOT NULL,
[country] [nvarchar] (50) NOT NULL,
[contract_code] [nvarchar] (50) NOT NULL,
[client_name] [nvarchar] (100) NOT NULL,
[OLB] [MONEY] NOT NULL,
[period_number] [int] NOT NULL,
[period_type] [CHAR] (1) NOT NULL,
[date] [DATETIME] NOT NULL
)

-----Drop view or proc of old conso report
IF EXISTS(SELECT name FROM sysobjects WHERE name = 'Conso_LoanSizeMaturityGraceDomainDistrict' AND xtype='V')
DROP VIEW [dbo].[Conso_LoanSizeMaturityGraceDomainDistrict]
GO

IF EXISTS(SELECT name FROM sysobjects WHERE name = 'PortfolioAndPAREvolutionByBranchLast12MonthsView' AND xtype='V')
DROP VIEW [dbo].[PortfolioAndPAREvolutionByBranchLast12MonthsView]
GO

IF EXISTS(SELECT name FROM sysobjects WHERE name = 'PortfolioAndPAREvolutionView' AND xtype='V')
DROP VIEW [dbo].[PortfolioAndPAREvolutionView]
GO

IF EXISTS(SELECT name FROM sysobjects WHERE name = 'PortfolioAndPAREvolutionByHeadQuarterLast12MonthsView' AND xtype='V')
DROP VIEW [dbo].[PortfolioAndPAREvolutionByHeadQuarterLast12MonthsView]
GO

IF EXISTS(SELECT name FROM sysobjects WHERE name = 'Conso_LoanSizeMaturityGraceDomainDistrict' AND xtype='V')
DROP VIEW [dbo].[Conso_LoanSizeMaturityGraceDomainDistrict]
GO

IF EXISTS(SELECT name FROM sysobjects WHERE name = 'PortfolioAndPAREvolutionByHeadQuarterView' AND xtype='V')
DROP VIEW [dbo].[PortfolioAndPAREvolutionByHeadQuarterView]
GO

IF EXISTS(SELECT name FROM sysobjects WHERE name = 'Conso_FollowUp_OLBAndLoansActif' AND xtype='V')
DROP VIEW [dbo].[Conso_FollowUp_OLBAndLoansActif]
GO

IF EXISTS(SELECT name FROM sysobjects WHERE name = 'Conso_FollowUp_PrincipalAndInterestByProduct' AND xtype='V')
DROP VIEW [dbo].[Conso_FollowUp_PrincipalAndInterestByProduct]
GO

IF EXISTS(SELECT name FROM sysobjects WHERE name = 'Conso_FollowUp_PortFolioAtRisk' AND xtype='V')
DROP VIEW [dbo].[Conso_FollowUp_PortFolioAtRisk]
GO

IF EXISTS(SELECT name FROM sysobjects WHERE name = 'LoanPortfolioAnalysis_Overview' AND xtype='V')
DROP VIEW [dbo].[LoanPortfolioAnalysis_Overview]
GO

IF EXISTS(SELECT name FROM sysobjects WHERE name = 'LoanPortfolioAnalysis_Repayments' AND xtype='V')
DROP VIEW [dbo].[LoanPortfolioAnalysis_Repayments]
GO

IF EXISTS(SELECT name FROM sysobjects WHERE name = 'LoanPortfolioAnalysis_LateLoansAndPrincipal' AND xtype='V')
DROP VIEW [dbo].[LoanPortfolioAnalysis_LateLoansAndPrincipal]
GO

IF EXISTS(SELECT name FROM sysobjects WHERE name = 'LoanPortfolioAnalysis_Provisioning' AND xtype='V')
DROP VIEW [dbo].[LoanPortfolioAnalysis_Provisioning]
GO