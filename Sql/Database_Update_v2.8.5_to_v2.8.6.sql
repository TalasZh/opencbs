IF NOT EXISTS(SELECT [key] FROM dbo.GeneralParameters WHERE [key] = 'ACCOUNTING_EXPORT_MODE')
INSERT INTO [GeneralParameters]([key], [value]) VALUES('ACCOUNTING_EXPORT_MODE', 'DEFAULT')
GO

ALTER TABLE TrancheEvents
DROP COLUMN close_date
GO

ALTER TABLE TrancheEvents
DROP COLUMN closed
GO

ALTER TABLE TrancheEvents
DROP COLUMN [interest_rate]
GO

ALTER TABLE TrancheEvents
ADD [interest_rate] money DEFAULT ((0)) NOT NULL
GO

ALTER TABLE TrancheEvents
ADD [started_from_installment] int NULL
GO

ALTER TABLE TrancheEvents
ADD [applied_new_interest] bit NOT NULL
GO

ALTER TABLE Credit
DROP COLUMN amount_under_loc_min
GO

ALTER TABLE Credit
DROP COLUMN amount_under_loc_max
GO

ALTER TABLE Credit
DROP COLUMN maturity_loc_min
GO

ALTER TABLE Credit
DROP COLUMN maturity_loc_max
GO

ALTER TABLE Credit
ADD 
	[anticipated_partial_repayment_base] SMALLINT DEFAULT ((0)) NOT NULL,
	[anticipated_total_repayment_base] SMALLINT DEFAULT ((0)) NOT NULL
GO

ALTER TABLE dbo.RepaymentEvents
DROP COLUMN fees
GO

ALTER TABLE dbo.Accounts
ADD [parent_account_id] [int] NULL
GO

ALTER TABLE [dbo].[Accounts]  WITH NOCHECK ADD  CONSTRAINT [FK_Accounts_Accounts] FOREIGN KEY([parent_account_id])
REFERENCES [dbo].[Accounts] ([id])
GO
ALTER TABLE [dbo].[Accounts] CHECK CONSTRAINT [FK_Accounts_Accounts]
GO

UPDATE Credit
SET [anticipated_total_repayment_base] = Pack.[anticipated_total_repayment_base]
FROM Credit Cr 
INNER JOIN dbo.Packages pack ON Cr.package_id = Pack.id
GO

UPDATE Credit
SET [anticipated_partial_repayment_base] = Pack.anticipated_partial_repayment_base
FROM Credit Cr 
INNER JOIN dbo.Packages Pack ON Cr.package_id = Pack.id
GO

ALTER TABLE Packages
ADD [activated_loc] BIT DEFAULT ((0)) NOT NULL
GO

-- Update Contracts status
UPDATE Contracts
SET status = 5
FROM Contracts Cont
INNER JOIN Credit Cr ON Cr.id = Cont.id
WHERE Cr.disbursed = 1 AND Cont.closed = 0
GO

-- Update Tiers status to false
UPDATE Tiers
SET active = 0
FROM Tiers
INNER JOIN Projects P on Tiers.id = P.tiers_id
INNER JOIN Contracts C on C.project_id = P.id
WHERE C.status = 1 OR C.status = 2 OR C.status = 6
GO

-- Update Tiers status to true
UPDATE Tiers
SET active = 1
FROM Tiers
INNER JOIN Projects P on Tiers.id = P.tiers_id
INNER JOIN Contracts C on C.project_id = P.id
WHERE C.status = 3 OR C.status = 4 OR C.status = 5
GO

CREATE TABLE [dbo].[SetUp_SageJournal](
	[product_code] [nvarchar](50) NOT NULL,
	[journal_code] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_SetUp_SageJournal] PRIMARY KEY CLUSTERED 
(
	[product_code] ASC,
	[journal_code] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

UPDATE [TechnicalParameters] SET [value] = 'v2.8.6' WHERE [name] = 'VERSION'
GO
