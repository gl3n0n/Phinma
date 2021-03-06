USE [ebid]
GO
/****** Object:  StoredProcedure [dbo].[sp_GetUserPasswordAndEmail]    Script Date: 04/30/2015 15:41:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE  PROC [dbo].[sp_GetUserPasswordAndEmail]
	@UserName nvarchar(100)
AS
   DECLARE @DBName nvarchar(200)
   DECLARE @SQL nvarchar(4000)

   select  @DBName = databaseName
   from    tblClients
   where   clientid in (select clientid from tblUsers where UserName = @UserName)
  
   SET @SQL = @DBName + '.dbo.sp_GetUserPasswordAndEmail @UserName=''' + @UserName + ''''
   insert into ebid.dbo.sp_log values ('sp_GetUserPasswordAndEmail', @SQL, getdate())
   exec (@SQL)

   -- select * from sp_log
GO
