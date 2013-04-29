UPDATE Persons SET identification_data = 'Not Set' WHERE identification_data IS NULL
GO

ALTER TABLE dbo.Persons ALTER COLUMN identification_data nvarchar(200) NOT NULL
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[FK_Corporates_CorporateDomainOfActivity]') AND parent_object_id = OBJECT_ID(N'[Corporates]'))
ALTER TABLE [Corporates] DROP CONSTRAINT [FK_Corporates_CorporateDomainOfActivity]
GO

DROP TABLE dbo.CorporateDomainOfActivity
GO

EXECUTE sp_rename N'dbo.Corporates.corporate_domain_activity_id', N'activity_id', 'COLUMN' 
GO

UPDATE Corporates SET activity_id = 1;
GO

ALTER TABLE dbo.Corporates ADD CONSTRAINT
	FK_Corporates_DomainOfApplications FOREIGN KEY
	(
	activity_id
	) REFERENCES dbo.DomainOfApplications
	(
	id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO

IF (SELECT COUNT(uid) FROM sys.sysobjects WHERE [type] = 'U' AND [name] = 'Pictures') > 0
ALTER TABLE Pictures
ALTER COLUMN [group] nvarchar(50) NOT NULL
GO

IF NOT EXISTS(SELECT [name] FROM sys.sysobjects WHERE [type] = 'D' AND [name] = 'DF_Pictures_subid')
ALTER TABLE dbo.Pictures ADD CONSTRAINT
	DF_Pictures_subid DEFAULT ((0)) FOR subid
GO

ALTER TABLE dbo.Persons ADD
	povertylevel_childreneducation int NOT NULL CONSTRAINT DF_Persons_povertylevel_childreneducation DEFAULT 0,
	povertylevel_economiceducation int NOT NULL CONSTRAINT DF_Persons_povertylevel_economiceducation DEFAULT 0,
	povertylevel_socialparticipation int NOT NULL CONSTRAINT DF_Persons_povertylevel_socialparticipation DEFAULT 0,
	povertylevel_healthsituation int NOT NULL CONSTRAINT DF_Persons_povertylevel_healthsituation DEFAULT 0
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

ALTER TABLE dbo.Users
	DROP CONSTRAINT CK_Users_role
GO
ALTER TABLE dbo.Users WITH NOCHECK ADD CONSTRAINT
	CK_Users_role CHECK (([role_code]='ADMIN' OR [role_code]='LOF' OR [role_code]='VISIT' OR [role_code]='SUPER' OR [role_code]='CASHI' OR [role_code]='BABYL'))
GO


/************** VERSION ***********************/
UPDATE [TechnicalParameters] SET [value]='v2.1.0'
GO

