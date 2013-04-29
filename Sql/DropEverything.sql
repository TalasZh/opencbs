WHILE EXISTS(SELECT [name] FROM sys.tables WHERE [type] = 'U')
BEGIN
	DECLARE @table_name varchar(100)
	DECLARE table_cursor CURSOR FOR SELECT [name] FROM sys.tables WHERE [type] = 'U'
	OPEN table_cursor
	FETCH NEXT FROM table_cursor INTO @table_name
		WHILE @@FETCH_STATUS = 0
		BEGIN
			BEGIN TRY
				EXEC ('DROP TABLE [' + @table_name + ']')
				--PRINT 'Dropped Table ' + @table_name
			END TRY
		BEGIN CATCH END CATCH
	FETCH NEXT FROM table_cursor INTO @table_name
	END
	CLOSE table_cursor
	DEALLOCATE table_cursor
END

WHILE EXISTS(SELECT [name] FROM sys.sysobjects WHERE [type] = 'P' OR [type] = 'V')
BEGIN
	DECLARE @object_name varchar(100), @type_object varchar(2)
	DECLARE object_cursor CURSOR FOR SELECT [name], [type] FROM sys.sysobjects WHERE [type] = 'P' OR [type] = 'V'
	OPEN object_cursor
	FETCH NEXT FROM object_cursor INTO @object_name, @type_object
		WHILE @@FETCH_STATUS = 0
		BEGIN
			BEGIN TRY
				IF (@type_object = 'P')
				BEGIN
					EXEC ('DROP PROC [' + @object_name + ']')
					--PRINT 'Dropped Proc ' + @object_name
				END
				ELSE
				BEGIN
					IF (@type_object = 'V')
					BEGIN 
						EXEC ('DROP VIEW [' + @object_name + ']')
						--PRINT 'Dropped View ' + @object_name
					END
				END
			END TRY
		BEGIN CATCH END CATCH
	FETCH NEXT FROM object_cursor INTO @object_name, @type_object
	END
	CLOSE object_cursor
	DEALLOCATE object_cursor
END