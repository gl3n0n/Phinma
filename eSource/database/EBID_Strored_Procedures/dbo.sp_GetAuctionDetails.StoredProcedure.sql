USE [ebid]
GO
/****** Object:  StoredProcedure [dbo].[sp_GetAuctionDetails]    Script Date: 04/30/2015 15:41:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_GetAuctionDetails]
	(@AuctionRefNo int, @ClientId int)
AS
   DECLARE @DBName nvarchar(200)
   DECLARE @SQL nvarchar(4000)

   select  @DBName = databaseName
   from    tblClients
   where   clientid = @ClientId
  
   SET @SQL = @DBName + '.dbo.sp_GetAuctionDetails @AuctionRefNo=' + cast(@AuctionRefNo as varchar)
   insert into ebid.dbo.sp_log values ('sp_GetAuctionDetails', @SQL, getdate())
   exec (@SQL)
GO
