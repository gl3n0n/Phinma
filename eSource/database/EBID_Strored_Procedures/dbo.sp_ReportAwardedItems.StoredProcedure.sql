USE [ebid]
GO
/****** Object:  StoredProcedure [dbo].[sp_ReportAwardedItems]    Script Date: 04/30/2015 15:41:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[sp_ReportAwardedItems]
   (@ClientId int) 
AS
   DECLARE @DBName nvarchar(200)
   DECLARE @SQL nvarchar(4000)

   select  @DBName = databaseName
   from    tblClients
   where   clientid = @ClientId
  
   SET @SQL = @DBName + '.dbo.sp_ReportAwardedItems'
   insert into sp_log values ('sp_ReportAwardedItems', @SQL, getdate())
   exec (@SQL)

   -- select * from sp_log order by _date desc
GO
