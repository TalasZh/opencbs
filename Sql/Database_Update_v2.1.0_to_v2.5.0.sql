ALTER TABLE dbo.CorporateEvents	DROP CONSTRAINT FK_CorporateEvents_Corporates
GO

ALTER TABLE dbo.CorporateEvents	DROP CONSTRAINT FK_CorporateEvents_CorporateEventsType
GO

CREATE TABLE dbo.Tmp_CorporateEvents
	(
	id int NOT NULL IDENTITY (1, 1),
	code nvarchar(200) NOT NULL,
	amount money NOT NULL,
	direction smallint NOT NULL,
	corporate_id int NOT NULL,
	deleted bit NOT NULL,
	creation_date datetime NOT NULL,
	type smallint NOT NULL
	)  ON [PRIMARY]
GO

SET IDENTITY_INSERT dbo.Tmp_CorporateEvents ON
GO

IF EXISTS(SELECT * FROM dbo.CorporateEvents)
	 EXEC('INSERT INTO dbo.Tmp_CorporateEvents (id, code, amount, direction, corporate_id, deleted, creation_date, type)
		SELECT id, code, amount, mouvement, corporate_id, deleted, creation_date, CONVERT(smallint, type) FROM dbo.CorporateEvents WITH (HOLDLOCK TABLOCKX)')
GO

SET IDENTITY_INSERT dbo.Tmp_CorporateEvents OFF
GO

DROP TABLE dbo.CorporateEvents
GO

EXECUTE sp_rename N'dbo.Tmp_CorporateEvents', N'CorporateEvents', 'OBJECT' 
GO

ALTER TABLE dbo.CorporateEvents ADD CONSTRAINT
	PK_EVENTBODYCORPORATE PRIMARY KEY CLUSTERED 
	(
	id
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO

ALTER TABLE dbo.CorporateEvents WITH NOCHECK ADD CONSTRAINT
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

/************** VERSION ***********************/
UPDATE [TechnicalParameters] SET [value]='v2.5.0'
GO

