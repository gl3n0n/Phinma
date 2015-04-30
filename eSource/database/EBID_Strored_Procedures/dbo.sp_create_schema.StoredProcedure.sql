USE [ebid]
GO
/****** Object:  StoredProcedure [dbo].[sp_create_schema]    Script Date: 04/30/2015 15:41:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_create_schema] (@dbname varchar(30)) 
AS
  
   DECLARE @SQL nvarchar(4000)
   SET @SQL = 'RESTORE DATABASE [' + @dbname + '] FROM DISK = N''C:\ESOURCE_DB\TEMPLATE\ebid_template.bak'' WITH  FILE = 1, NOUNLOAD, REPLACE, STATS = 10'
   EXEC (@SQL)
GO
