IF EXISTS ( SELECT  *
            FROM    sys.objects
            WHERE   object_id = OBJECT_ID(N'dbo.RunLoanAccountingClosure')
                    AND type = N'P' ) 
    DROP PROCEDURE [dbo].[RunLoanAccountingClosure]
GO

IF EXISTS ( SELECT  *
            FROM    sys.objects
            WHERE   object_id = OBJECT_ID(N'dbo.RunSavingAccountingClosure')
                    AND type = N'P' ) 
    DROP PROCEDURE [dbo].[RunSavingAccountingClosure]
GO

INSERT INTO [GeneralParameters]([key], [value]) VALUES('MODIFY_ENTRY_FEE', 0)
GO

ALTER TABLE dbo.Contracts ADD activity_id INT NULL
GO

UPDATE c
SET    c.activity_id = p.activity_id
FROM   Contracts c
INNER JOIN Projects pr ON  c.project_id = pr.id
INNER JOIN Tiers t ON  pr.tiers_id = t.id
INNER JOIN Persons p ON  p.id = t.id
GO

UPDATE c
SET    c.activity_id = cr.activity_id
FROM   Contracts c
INNER JOIN Projects pr ON  c.project_id = pr.id
INNER JOIN Tiers t ON  pr.tiers_id = t.id
INNER JOIN Corporates cr ON  cr.id = t.id
GO

UPDATE c
SET    c.activity_id = p.activity_id
FROM   Contracts c
INNER JOIN Projects pr ON  c.project_id = pr.id
INNER JOIN Tiers t ON  pr.tiers_id = t.id
INNER JOIN Groups g ON  g.id = t.id
INNER JOIN PersonGroupBelonging pgb ON  pgb.group_id = g.id AND pgb.is_leader = 1
INNER JOIN Persons p ON  pgb.person_id = p.id
GO

DECLARE @activityId INT
SELECT @activityId = MIN(id)
FROM   EconomicActivities

UPDATE Contracts
SET    activity_id = @activityId
WHERE  activity_id IS NULL
GO

ALTER TABLE dbo.Contracts ALTER COLUMN activity_id INT NOT NULL
GO

ALTER TABLE dbo.Contracts ADD CONSTRAINT
	FK_Contracts_EconomicActivities FOREIGN KEY(activity_id) REFERENCES dbo.EconomicActivities (id)
ON UPDATE NO ACTION
ON DELETE NO ACTION 

GO

UPDATE dbo.ActionItems SET method_name = 'CanUserModifyEntryFees' WHERE method_name = 'CanUserDisableEntryFees'
GO

ALTER TABLE dbo.Tellers
DROP CONSTRAINT [FK_Tellers_Users]
GO

SET IDENTITY_INSERT [MenuItems] ON
INSERT INTO [dbo].[MenuItems]([id], [component_name]) VALUES (61, 'tellerManagementToolStripMenuItem')
INSERT INTO [dbo].[MenuItems]([id], [component_name]) VALUES (74, 'tellerOperationsToolStripMenuItem')
SET IDENTITY_INSERT [MenuItems] OFF
GO

ALTER TABLE dbo.TellerEvents
ADD [description] NVARCHAR(100)
GO
            
UPDATE  [TechnicalParameters]
SET     [value] = 'v4.7.0'
WHERE   [name] = 'VERSION'
GO
