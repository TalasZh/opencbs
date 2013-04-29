SET NOCOUNT ON

-- Backup the ElementaryMvts table data (just in case...)
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ElementaryMvtsBackUp]') AND type in (N'U'))
DROP TABLE [ElementaryMvtsBackUp]
GO
CREATE TABLE [dbo].[ElementaryMvtsBackUp](
	[number] [int] NOT NULL,
	[account_id] [int] NOT NULL,
	[amount] [money] NOT NULL,
	[movement_set_id] [int] NOT NULL,
	[direction] [char](1) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[is_exported] [bit] NOT NULL CONSTRAINT [DF_ElementaryMvtsBackUp_isExported]  DEFAULT (0),
	[voucher_number] [int] NULL
)
GO
INSERT INTO ElementaryMvtsBackUp SELECT * FROM ElementaryMvts
GO

-- ElementaryMvtsPairs table creation
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ElementaryMvtsPairs]') AND type in (N'U'))
DROP TABLE [dbo].[ElementaryMvtsPairs]
GO
CREATE TABLE [dbo].[ElementaryMvtsPairs](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[debit_account_id] [int] NOT NULL,
    [credit_account_id] [int] NOT NULL,
	[amount] [money] NOT NULL,
	[movement_set_id] [int] NOT NULL,
	[is_exported] [bit] NOT NULL CONSTRAINT [DF_ElementaryMvts_Exported]  DEFAULT (0),
	[voucher_number] [int] NULL
CONSTRAINT [PK_ElementaryMvtsPK] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

SET NOCOUNT ON

-- Local vars declarations
DECLARE @CURRENT_MOVEMENT_SET_ID INT
SET		@CURRENT_MOVEMENT_SET_ID=1

DECLARE @NUMBER_1 INT
DECLARE @ACCOUNT_ID_1 INT
DECLARE @AMOUNT_1 MONEY
DECLARE @MOVEMENT_SET_ID_1 INT
DECLARE @DIRECTION_1 CHAR(1)
DECLARE @IS_EXPORTED_1 BIT
DECLARE @VOUCHER_NUMBER_1 INT

DECLARE @NUMBER_2 INT
DECLARE @ACCOUNT_ID_2 INT
DECLARE @AMOUNT_2 MONEY
DECLARE @MOVEMENT_SET_ID_2 INT
DECLARE @DIRECTION_2 CHAR(1)
DECLARE @IS_EXPORTED_2 BIT
DECLARE @VOUCHER_NUMBER_2 INT

-- First, we get the MAX(MOVEMENT_SET_ID)
DECLARE CURSOR_MOVEMENT_SET_IDS CURSOR FOR
SELECT DISTINCT (MOVEMENT_SET_ID) FROM ELEMENTARYMVTS

OPEN CURSOR_MOVEMENT_SET_IDS;
FETCH NEXT FROM CURSOR_MOVEMENT_SET_IDS INTO @CURRENT_MOVEMENT_SET_ID

WHILE @@FETCH_STATUS=0 -- @CURRENT_MOVEMENT_SET_ID<5 -- while temporaire
BEGIN
	PRINT '@CURRENT_MOVEMENT_SET_ID' + STR (@CURRENT_MOVEMENT_SET_ID, 5)

	-- Get the Cursor of 2 elementary movements
	DECLARE CURSOR_UNITARY_MOVEMENTS CURSOR FOR
		SELECT * FROM ELEMENTARYMVTS WHERE MOVEMENT_SET_ID = @CURRENT_MOVEMENT_SET_ID ORDER BY NUMBER ASC

	OPEN CURSOR_UNITARY_MOVEMENTS;
	FETCH NEXT FROM CURSOR_UNITARY_MOVEMENTS INTO @NUMBER_1, @ACCOUNT_ID_1, @AMOUNT_1, @MOVEMENT_SET_ID_1, @DIRECTION_1, @IS_EXPORTED_1, @VOUCHER_NUMBER_1
	FETCH NEXT FROM CURSOR_UNITARY_MOVEMENTS INTO @NUMBER_2, @ACCOUNT_ID_2, @AMOUNT_2, @MOVEMENT_SET_ID_2, @DIRECTION_2, @IS_EXPORTED_2, @VOUCHER_NUMBER_2

	-- Process 2 by 2 elementary movements
	WHILE @@FETCH_STATUS=0
	BEGIN
		-- Insert the pair record
		IF @DIRECTION_1='D' -- Case where the 1st elem. mvmt is a 'Debit' and 2nd elem. mvmt='Credit
		BEGIN
			INSERT INTO ELEMENTARYMVTSPAIRS (MOVEMENT_SET_ID, DEBIT_ACCOUNT_ID, CREDIT_ACCOUNT_ID, AMOUNT, IS_EXPORTED, VOUCHER_NUMBER) VALUES (@MOVEMENT_SET_ID_1, @ACCOUNT_ID_1, @ACCOUNT_ID_2, @AMOUNT_1, @IS_EXPORTED_1, @VOUCHER_NUMBER_1)
		END
		IF @DIRECTION_1='C' -- Case where the 1st elem. mvmt is a 'Credit' ; 2nd elem. mvmt='Debit'
		BEGIN
			INSERT INTO ELEMENTARYMVTSPAIRS (MOVEMENT_SET_ID, DEBIT_ACCOUNT_ID, CREDIT_ACCOUNT_ID, AMOUNT, IS_EXPORTED, VOUCHER_NUMBER) VALUES (@MOVEMENT_SET_ID_2, @ACCOUNT_ID_2, @ACCOUNT_ID_1, @AMOUNT_1, @IS_EXPORTED_1, @VOUCHER_NUMBER_1)
		END
		PRINT 'AMOUNT :' + ' ' + STR(@AMOUNT_1, 9)
		FETCH NEXT FROM CURSOR_UNITARY_MOVEMENTS INTO @NUMBER_1, @ACCOUNT_ID_1, @AMOUNT_1, @MOVEMENT_SET_ID_1, @DIRECTION_1, @IS_EXPORTED_1, @VOUCHER_NUMBER_1
		FETCH NEXT FROM CURSOR_UNITARY_MOVEMENTS INTO @NUMBER_2, @ACCOUNT_ID_2, @AMOUNT_2, @MOVEMENT_SET_ID_2, @DIRECTION_2, @IS_EXPORTED_2, @VOUCHER_NUMBER_2
	END

	-- Close the CURSOR
	CLOSE		CURSOR_UNITARY_MOVEMENTS
	DEALLOCATE	CURSOR_UNITARY_MOVEMENTS
	FETCH NEXT FROM CURSOR_MOVEMENT_SET_IDS INTO @CURRENT_MOVEMENT_SET_ID
END
-- Close the CURSOR
CLOSE CURSOR_MOVEMENT_SET_IDS
DEALLOCATE CURSOR_MOVEMENT_SET_IDS

SELECT * FROM ELEMENTARYMVTSPAIRS ORDER BY MOVEMENT_SET_ID

SELECT COUNT(*) AS SOMME_UNITARIES FROM ELEMENTARYMVTS 
SELECT COUNT(*) AS SOMME_PAIRS FROM ELEMENTARYMVTSPAIRS

-- If ElementaryMvts' constraints exist, then we drop them
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[FK_ElementaryMvts_Accounts]') AND type in (N'F'))
ALTER TABLE [dbo].[ELEMENTARYMVTS] DROP CONSTRAINT [FK_ElementaryMvts_Accounts]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[FK_ElementaryMvts_Transactions1]') AND type in (N'F'))
ALTER TABLE [DBO].[ELEMENTARYMVTS] DROP CONSTRAINT [FK_ElementaryMvts_Transactions1]
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CK_ElementaryMvts_direction]') AND type in (N'C'))
ALTER TABLE [DBO].[ELEMENTARYMVTS] DROP CONSTRAINT [CK_ElementaryMvts_direction]
GO

-- We rename the former table and replace it by our new one 
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ElementaryMvtsOld]') AND type in (N'U'))
DROP TABLE [ElementaryMvtsOld]
GO
EXECUTE sp_rename N'dbo.ElementaryMvts', N'ElementaryMvtsOld', 'OBJECT' 
GO
EXECUTE sp_rename N'dbo.ElementaryMvtsPairs', N'ElementaryMvts', 'OBJECT' 
GO

-- We restore the table constraints
ALTER TABLE [dbo].[ElementaryMvts]  WITH NOCHECK ADD  CONSTRAINT [FK_ElementaryMvts_Credit_Accounts] FOREIGN KEY([credit_account_id])
REFERENCES [dbo].[Accounts] ([id])
GO
ALTER TABLE [dbo].[ElementaryMvts]  WITH NOCHECK ADD  CONSTRAINT [FK_ElementaryMvts_Debit_Accounts] FOREIGN KEY([debit_account_id])
REFERENCES [dbo].[Accounts] ([id])
GO
ALTER TABLE [dbo].[ElementaryMvts]  WITH CHECK ADD  CONSTRAINT [FK_ElementaryMvts_Transactions1] FOREIGN KEY([movement_set_id])
REFERENCES [dbo].[MovementSet] ([id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO




-- ##################################################################
-- Note :
-- Thinking that PK =(debit_account_id + credit_account_id + movement_set_id) is wrong
-- See record 111 in the test purpose database, SELECT * FROM ELEMENTARYMVTS WHERE movement_set_id=111
-- Either: **PK=ID**, or it's: debit_account_id + credit_account_id + movement_set_id + amount
-- ##################################################################
