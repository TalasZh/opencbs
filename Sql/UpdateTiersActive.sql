SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO

CREATE VIEW dbo.GroupTiersActive
AS
SELECT     dbo.Tiers.id, dbo.Tiers.active
FROM         dbo.Groups INNER JOIN
                      dbo.Tiers ON dbo.Groups.id = dbo.Tiers.id INNER JOIN
                      dbo.Contracts ON dbo.Tiers.id = dbo.Contracts.beneficiary_id INNER JOIN
                      dbo.Credit ON dbo.Contracts.id = dbo.Credit.id
WHERE     ((SELECT     SUM(capital_repayment - paid_capital + interest_repayment - paid_interest)
                         FROM         Installments
                         WHERE     Installments.contract_id = Credit.id) > 0.02)

GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO

CREATE VIEW dbo.MembresTiersActive
AS
SELECT     Tiers_1.id AS id, Tiers_1.active AS active
FROM         dbo.Tiers INNER JOIN
                      dbo.Contracts ON dbo.Tiers.id = dbo.Contracts.beneficiary_id INNER JOIN
                      dbo.Credit ON dbo.Contracts.id = dbo.Credit.id INNER JOIN
                      dbo.Groups ON dbo.Tiers.id = dbo.Groups.id INNER JOIN
                      dbo.PersonGroupBelonging ON dbo.Groups.id = dbo.PersonGroupBelonging.group_id INNER JOIN
                      dbo.Tiers Tiers_1 ON dbo.PersonGroupBelonging.person_id = Tiers_1.id
WHERE     ((SELECT     SUM(capital_repayment - paid_capital + interest_repayment - paid_interest)
                         FROM         Installments
                         WHERE     Installments.contract_id = Credit.id) > 0.02)

GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO

CREATE VIEW dbo.PersonsTiersActive
AS
SELECT     dbo.Tiers.id, dbo.Tiers.active
FROM         dbo.Tiers INNER JOIN
                      dbo.Contracts ON dbo.Tiers.id = dbo.Contracts.beneficiary_id INNER JOIN
                      dbo.Credit ON dbo.Contracts.id = dbo.Credit.id INNER JOIN
                      dbo.Persons ON dbo.Tiers.id = dbo.Persons.id
WHERE     ((SELECT     SUM(capital_repayment - paid_capital + interest_repayment - paid_interest)
                         FROM         Installments
                         WHERE     Installments.contract_id = Credit.id) > 0.02)

GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


UPDATE [Tiers] SET [active] = 0
UPDATE [GroupTiersActive] SET [active] = 1
UPDATE [MembresTiersActive] SET [active] = 1
UPDATE [PersonsTiersActive] SET [active] = 1

DROP VIEW PersonsTiersActive
DROP VIEW MembresTiersActive
DROP VIEW GroupTiersActive