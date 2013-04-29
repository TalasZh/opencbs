--Use current script to generate link between SQL Server and Octopus file Octopus.Stringifier.dll
-- instead of 0 put your database name
exec sp_dbcmptlevel '{0}', 90
GO

sp_configure 'clr_enable', 1
GO

RECONFIGURE
GO

-- instead of 0 put your database name
EXEC sp_dbcmptlevel {0}, 90;
GO

IF EXISTS (SELECT * FROM sys.objects WHERE [object_id] = OBJECT_ID(N'[dbo].[AmountToLetters]') AND [type] = 'FS')
DROP FUNCTION [dbo].[AmountToLetters]
GO

IF EXISTS (SELECT * FROM sys.objects WHERE [object_id] = OBJECT_ID(N'[dbo].[AmountToLettersPercent]') AND [type] = 'FS')
DROP FUNCTION [dbo].[AmountToLettersPercent]
GO

IF EXISTS (SELECT * FROM sys.objects WHERE [object_id] = OBJECT_ID(N'[dbo].[Stringify]') AND [type] = 'FS')
DROP FUNCTION [dbo].[Stringify]
GO

IF EXISTS (SELECT * FROM sys.assemblies WHERE [name] = N'sqludf')
DROP ASSEMBLY [sqludf]
GO

-- instead of {1} put path to the file Octopus.Stringfiler.dll
CREATE ASSEMBLY sqludf FROM '{1}'
GO

CREATE FUNCTION AmountToLetters(@number DECIMAL(18, 2), @lang NVARCHAR(5) = 'en-US') RETURNS NVARCHAR(255)
AS EXTERNAL NAME [sqludf].[Octopus.Stringifier.Udf].[AmountToLetters]
GO

CREATE FUNCTION AmountToLettersPercent(@number DECIMAL(18, 2), @lang NVARCHAR(5) = 'en-US', @ignorePercent BIT) RETURNS NVARCHAR(255)
AS EXTERNAL NAME [sqludf].[Octopus.Stringifier.Udf].[AmountToLettersPercent]
GO

CREATE FUNCTION Stringify(@format NVARCHAR(20), @amount DECIMAL(18, 2)) RETURNS NVARCHAR(255)
AS EXTERNAL NAME [sqludf].[Octopus.Stringifier.Udf].[Stringify]
GO