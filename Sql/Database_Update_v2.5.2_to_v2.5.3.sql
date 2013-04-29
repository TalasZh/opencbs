ALTER TABLE dbo.ExoticInstallments
	DROP COLUMN constant_amount
GO

CREATE TABLE dbo.Tmp_Cycles
	(
	id int NOT NULL IDENTITY (1, 1),
	name nvarchar(200) NOT NULL
	)  ON [PRIMARY]
GO

SET IDENTITY_INSERT dbo.Tmp_Cycles ON
GO

IF EXISTS(SELECT * FROM dbo.Cycles)
	 EXEC('INSERT INTO dbo.Tmp_Cycles (id, name)
		SELECT id, CONVERT(nvarchar(200), name) FROM dbo.Cycles WITH (HOLDLOCK TABLOCKX)')
GO

SET IDENTITY_INSERT dbo.Tmp_Cycles OFF
GO

ALTER TABLE dbo.AmountCycles
	DROP CONSTRAINT FK_AmountCycles_Cycles
GO

ALTER TABLE dbo.Packages
	DROP CONSTRAINT FK_Packages_Cycles
GO

DROP TABLE dbo.Cycles
GO

EXECUTE sp_rename N'dbo.Tmp_Cycles', N'Cycles', 'OBJECT' 
GO

ALTER TABLE dbo.Cycles ADD CONSTRAINT
	PK_Cycles PRIMARY KEY CLUSTERED 
	(
	id
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
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

ALTER TABLE dbo.AmountCycles ADD CONSTRAINT
	FK_AmountCycles_Cycles FOREIGN KEY
	(
	cycle_id
	) REFERENCES dbo.Cycles
	(
	id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
GO

ALTER TABLE Contracts ALTER COLUMN credit_commitee_comment NVARCHAR(4000)
GO

UPDATE [TechnicalParameters] SET [value]='v2.5.3'
GO

