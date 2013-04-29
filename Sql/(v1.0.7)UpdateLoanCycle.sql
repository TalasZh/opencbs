CREATE VIEW [dbo].[tempViewForPersons]
AS
SELECT     dbo.Persons.id, dbo.Tiers.amount_cycle_id,
                          (SELECT     COUNT(id) AS Expr1
                            FROM          dbo.Contracts
                            WHERE      (beneficiary_id = 3) AND
                                                       ((SELECT     SUM(capital_repayment + interest_repayment - paid_capital - paid_interest) AS Expr1
                                                           FROM         dbo.Installments
                                                           WHERE     (contract_id = dbo.Contracts.id)) < 0.02)) + 1 AS NbrOfCloseContracts
FROM         dbo.Persons INNER JOIN
                      dbo.Tiers ON dbo.Persons.id = dbo.Tiers.id

GO

CREATE VIEW [dbo].[tempViewForGroup]
AS
SELECT     dbo.Groups.id, dbo.Tiers.amount_cycle_id,
                          (SELECT     COUNT(id) AS Expr1
                            FROM          dbo.Contracts
                            WHERE      (beneficiary_id = 3) AND
                                                       ((SELECT     SUM(capital_repayment + interest_repayment - paid_capital - paid_interest) AS Expr1
                                                           FROM         dbo.Installments
                                                           WHERE     (contract_id = dbo.Contracts.id)) < 0.02)) + 1 AS NbrOfCloseContracts
FROM         dbo.Groups INNER JOIN
                      dbo.Tiers ON dbo.Groups.id = dbo.Tiers.id

GO

UPDATE [tempViewForPersons] SET amount_cycle_id = NbrOfCloseContracts
UPDATE [tempViewForGroup] SET amount_cycle_id = NbrOfCloseContracts
GO

DROP VIEW [tempViewForPersons]
DROP VIEW [tempViewForGroup]
GO