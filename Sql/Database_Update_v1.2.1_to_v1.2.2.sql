CREATE TABLE [dbo].[MFI](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](55) NOT NULL,
	[login] [nvarchar](55) NULL,
	[password] [nvarchar](55) NULL
) ON [PRIMARY]
GO
UPDATE [GeneralParameters] SET [value] = 1 WHERE [key] = 'ALLOWS_MULTIPLE_LOANS'
GO

INSERT INTO [CorporateEventsType]([id], [code]) VALUES(5, 'Commitment')
GO

ALTER TABLE dbo.Tiers ADD follow_up_comment nvarchar(500) NULL
GO

UPDATE [Tiers] SET [creation_date] = GETDATE()
GO

UPDATE Credit SET corporate_id = (SELECT TOP 1 corporate_id FROM CorporateFundingLineBelonging WHERE fundingLine_id = Credit.fundingLine_id)
GO

ALTER TABLE dbo.Credit ADD synchronize bit NOT NULL CONSTRAINT DF_Credit_synchronize DEFAULT 0
GO

ALTER TABLE dbo.Projects ADD corporate_id int NULL
GO

ALTER TABLE dbo.Projects ADD CONSTRAINT
	FK_Projects_Corporates FOREIGN KEY
	(
	corporate_id
	) REFERENCES dbo.Corporates
	(
	id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO

/************** VERSION ***********************/
UPDATE [TechnicalParameters] SET [value]='v1.2.2'
GO

