USE [ebid]
GO
/****** Object:  StoredProcedure [dbo].[sp_ReportAuctionItemDetail]    Script Date: 04/30/2015 15:41:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[sp_ReportAuctionItemDetail] 
    (@ClientId int)
AS
   DECLARE @DBName nvarchar(200)
   DECLARE @SQL nvarchar(4000)

   select  @DBName = databaseName
   from    tblClients
   where   clientid = @ClientId
  
   SET @SQL = @DBName + '.dbo.sp_ReportAuctionItemDetail'
   insert into ebid.dbo.sp_log values ('sp_ReportAuctionItemDetail', @SQL, getdate())
   exec (@SQL)

   -- select * from sp_log order by _date desc
GO
