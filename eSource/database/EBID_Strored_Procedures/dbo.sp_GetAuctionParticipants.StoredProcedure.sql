USE [ebid]
GO
/****** Object:  StoredProcedure [dbo].[sp_GetAuctionParticipants]    Script Date: 04/30/2015 15:41:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_GetAuctionParticipants]
	(@AuctionRefNo int, @ClientId int)
AS
   DECLARE @DBName nvarchar(200)
   DECLARE @SQL nvarchar(4000)

   select  @DBName = databaseName
   from    tblClients
   where   clientid = @ClientId
  
   SET @SQL = @DBName + '.dbo.sp_GetAuctionParticipants @AuctionRefNo=' + cast(@AuctionRefNo as varchar)
   insert into ebid.dbo.sp_log values ('sp_GetAuctionParticipants', @SQL, getdate())
   exec (@SQL)
GO
