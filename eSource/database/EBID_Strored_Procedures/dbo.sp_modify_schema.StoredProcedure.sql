USE [ebid]
GO
/****** Object:  StoredProcedure [dbo].[sp_modify_schema]    Script Date: 04/30/2015 15:41:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_modify_schema] (@dbname varchar(30), @Clientid int) 
AS
  
   DECLARE @vSQL nvarchar(4000)
   SET @vSQL = 'ALTER PROC [dbo].[s3p_EBid_InsertNewUser] '
   SET @vSQL = @vSQL + '	@UserName nvarchar(100), '
   SET @vSQL = @vSQL + '	@Password nvarchar(1000), '
   SET @vSQL = @vSQL + '	@UserType int '
   SET @vSQL = @vSQL + 'AS '
   SET @vSQL = @vSQL + 'INSERT INTO ebid.dbo.tblUsers (UserName, UserPassword, ClientID) '
   SET @vSQL = @vSQL + 'VALUES (@UserName, @Password, ' + cast(@Clientid as varchar) + ')'
   SET @vSQL = @vSQL + ' '
   SET @vSQL = @vSQL + 'INSERT INTO tblUsers (UserName, UserPassword, UserType) '
   SET @vSQL = @vSQL + 'VALUES (@UserName, @Password, @UserType) '
   SET @vSQL = @vSQL + ' '
   SET @vSQL = @vSQL + 'SELECT SCOPE_IDENTITY() '
   SET @vSQL = @vSQL + 'GO '
   EXEC (@vSQL)
GO
