USE [ebid]
GO
/****** Object:  StoredProcedure [dbo].[sp_GetBuyerApprovedBidEventsByCompany]    Script Date: 04/30/2015 15:41:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE  PROCEDURE [dbo].[sp_GetBuyerApprovedBidEventsByCompany]
  @BuyerId int, @CompanyId int, @FromDate datetime, @ToDate datetime, @ClientId int
AS 
   DECLARE @DBName nvarchar(200)
   DECLARE @SQL nvarchar(4000)

   select  @DBName = databaseName
   from    tblClients
   where   clientid = @ClientId

   SET @SQL = @DBName + '.dbo.sp_GetBuyerApprovedBidEventsByCompany @BuyerId=' + cast(@BuyerId as varchar) + ',@CompanyId=' + cast(@CompanyId as varchar) + ',@FromDate=''' + convert(nvarchar(30), @FromDate, 120) + ''',@ToDate=''' + convert(nvarchar(30), @ToDate, 120) + ''''
   insert into sp_log values ('sp_GetBuyerApprovedBidEventsByCompany', @SQL, getdate())
   exec (@SQL)

   -- select * from sp_log order by _date desc
GO
