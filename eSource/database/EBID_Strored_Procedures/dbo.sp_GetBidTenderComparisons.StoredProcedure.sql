USE [ebid]
GO
/****** Object:  StoredProcedure [dbo].[sp_GetBidTenderComparisons]    Script Date: 04/30/2015 15:41:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_GetBidTenderComparisons]
	@BidDetailNo int,@UseAlias int, @ClientId int

AS
   DECLARE @DBName nvarchar(200)
   DECLARE @SQL nvarchar(4000)

   select  @DBName = databaseName
   from    tblClients
   where   clientid = @ClientId
  
   SET @SQL = @DBName + '.dbo.sp_GetBidTenderComparisons @BidDetailNo=' + cast(@BidDetailNo as varchar) + ',@UseAlias=' + cast(@UseAlias as varchar)
   insert into ebid.dbo.sp_log values ('sp_GetBidTenderComparisons', @SQL, getdate())
   exec (@SQL)

   --select * from sp_log
GO
