USE [ebid]
GO
/****** Object:  StoredProcedure [dbo].[sp_GetAwardedItemReportDetails]    Script Date: 04/30/2015 15:41:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[sp_GetAwardedItemReportDetails] 
   (@BidDetailNo int, @Usage int, @ClientId int)
AS
   DECLARE @DBName nvarchar(200)
   DECLARE @SQL nvarchar(4000)

   select  @DBName = databaseName
   from    tblClients
   where   clientid = @ClientId
  
   SET @SQL = @DBName + '.dbo.sp_GetAwardedItemReportDetails @BidDetailNo=' + cast(@BidDetailNo as varchar) + ',@Usage=' + cast(@Usage as varchar)
   insert into ebid.dbo.sp_log values ('sp_GetAwardedItemReportDetails', @SQL, getdate())
   exec (@SQL)
GO
