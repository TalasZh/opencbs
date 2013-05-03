DELETE FROM [dbo].[GeneralParameters]
WHERE [key] = 'CHECK_BIC_CODE'
GO

IF NOT EXISTS (SELECT id FROM dbo.AccountingClosure)
BEGIN
	DECLARE @user_id INT
	DECLARE @count INT
	SET @user_id = (SELECT TOP 1 id FROM Users WHERE (role_code = 'SUPER' OR role_code = 'ADMIN') AND deleted = 0)
	SET @count = (SELECT COUNT(id) FROM dbo.ManualAccountingMovements WHERE closure_id IS NULL)
	INSERT INTO dbo.AccountingClosure
			( user_id ,
			  date_of_closure ,
			  count_of_transactions ,
			  is_deleted
			)
	VALUES  ( @user_id,
			  GETDATE() ,
			  @count ,
			  0 
			)
END

DECLARE @id INT
SET @id = (SELECT TOP 1 id FROM dbo.AccountingClosure)
UPDATE dbo.ManualAccountingMovements SET closure_id = @id WHERE closure_id IS NULL
GO

CREATE TABLE [dbo].[FiscalYear](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](max) NULL,
	[open_date] [datetime] NULL,
	[close_date] [datetime] NULL,
 CONSTRAINT [PK_FiscalYear] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

UPDATE  [TechnicalParameters]
SET     [value] = 'v4.4.0'
WHERE   [name] = 'VERSION'
GO
