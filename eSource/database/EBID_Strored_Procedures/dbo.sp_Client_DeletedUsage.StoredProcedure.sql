USE [ebid]
GO
/****** Object:  StoredProcedure [dbo].[sp_Client_DeletedUsage]    Script Date: 04/30/2015 15:41:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_Client_DeletedUsage]
	(@ClientId int,
	 @TranDate date,
	 @DiskUsage float,
	 @NumUsers float,
	 @NumVendors float,
	 @NumBidEvents float,
	 @NumAuctionEvents float)

AS

IF NOT EXISTS (SELECT 1 FROM tblClientsDeletedUsage WHERE ClientId = @ClientId AND TranDate=@TranDate )
   INSERT INTO tblClientsDeletedUsage
      (ClientId,
       TranDate, 
       DiskUsage, 
       NumUsers, 
       NumVendors, 
       NumBidEvents, 
       NumAuctionEvents
      )
   VALUES	
      (@ClientId,       
       @TranDate,       
       ISNULL(@DiskUsage,0),      
       ISNULL(@NumUsers,0),       
       ISNULL(@NumVendors,0),     
       ISNULL(@NumBidEvents,0),   
       ISNULL(@NumAuctionEvents,0)
      )
ELSE
   UPDATE tblClientsDeletedUsage
   SET    DiskUsage        = DiskUsage + ISNULL(@DiskUsage,0),
          NumUsers         = NumUsers + ISNULL(@NumUsers,0),
          NumVendors       = NumVendors + ISNULL(@NumVendors,0),
          NumBidEvents     = NumBidEvents + ISNULL(@NumBidEvents,0),
          NumAuctionEvents = NumAuctionEvents + ISNULL(@NumAuctionEvents,0)
   WHERE  ClientId=@ClientId
   AND    TranDate=@TranDate
GO
