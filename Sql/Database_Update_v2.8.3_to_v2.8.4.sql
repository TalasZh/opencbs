ALTER TABLE dbo.Currencies
ADD use_cents BIT NOT NULL DEFAULT 1
GO

ALTER TABLE dbo.Persons
ADD first_appointment DATETIME NULL
GO

ALTER TABLE dbo.Projects
ADD corporate_registre NVARCHAR(50) NULL
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SetUp_HousingLocation]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[SetUp_HousingLocation](
	[value] [nvarchar](50) NOT NULL
) ON [PRIMARY]
END
GO

IF NOT OBJECT_ID('dbo.InstallmentsHistoric') IS NULL
DROP TABLE dbo.InstallmentsHistoric
GO

DECLARE @num INT
SELECT @num = COUNT(*) FROM dbo.Currencies
IF 1 = @num
BEGIN
  UPDATE dbo.Currencies SET is_pivot = 1
END
GO

IF NOT EXISTS(SELECT * FROM sys.columns WHERE OBJECT_ID = object_id('Contracts') AND name = 'align_disbursed_date')
BEGIN
  ALTER TABLE dbo.Contracts 
  ADD align_disbursed_date DATETIME NULL
END
GO
        
UPDATE [TechnicalParameters] SET [value] = 'v2.8.4'
GO

