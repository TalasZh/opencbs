ALTER TABLE [Packages]
ADD [allow_flexible_schedule] [bit] NOT NULL DEFAULT((0))
GO

UPDATE Packages
SET [allow_flexible_schedule] = 0
GO

UPDATE Contracts
SET Contracts.status = 6
WHERE Contracts.closed = 1 AND Contracts.status <> 6
GO

UPDATE Tiers
SET Tiers.active = 0
FROM Tiers
INNER JOIN Projects ON Projects.tiers_id = Tiers.id
INNER JOIN Contracts ON Contracts.project_id = Projects.id
WHERE Contracts.closed = 1 AND Tiers.active = 1
GO

UPDATE Tiers
SET Tiers.active = 1
FROM Tiers
INNER JOIN Projects ON Projects.tiers_id = Tiers.id
INNER JOIN Contracts ON Contracts.project_id = Projects.id
WHERE Contracts.closed = 0 
AND (Contracts.status = 1 OR Contracts.status = 2 OR Contracts.status = 5)
GO

ALTER TABLE dbo.CorporatePersonBelonging
DROP COLUMN role_id
GO

ALTER TABLE dbo.CorporatePersonBelonging
ADD [position] [nvarchar](50) NULL
GO

UPDATE [TechnicalParameters] SET [value] = 'v2.8.9' WHERE [name] = 'VERSION'
GO
