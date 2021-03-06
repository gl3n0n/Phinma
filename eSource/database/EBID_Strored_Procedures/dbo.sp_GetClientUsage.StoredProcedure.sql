USE [ebid]
GO
/****** Object:  StoredProcedure [dbo].[sp_GetClientUsage]    Script Date: 04/30/2015 15:41:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE  PROC [dbo].[sp_GetClientUsage]
(@ClientId int)
AS
   SELECT 
          t2.configCompanyName CompanyName, 
          t2.MaxDiskUse_Mb,
          t2.maxNumUsers,
          t2.maxNumVendors,
          t2.maxBidEvents,
          t1.ClientId,
          sum(t1.DiskUsage) DiskUsage,
          sum(t1.NumUsers) NumUsers,
          sum(t1.NumVendors) NumVendors,
          sum(t1.NumBidEvents) NumBidEvents,
          sum(t1.NumAuctionEvents) NumAuctionEvents
   FROM   tblClientsUsage t1, tblClients t2
   WHERE  t2.ClientId = @ClientId AND t1.ClientId=t2.ClientId
   GROUP  by 
   t2.configCompanyName, 
   t2.MaxDiskUse_Mb, 
          t2.maxNumUsers,
          t2.maxNumVendors,
          t2.maxBidEvents,
   t1.ClientId
   ORDER  BY 3 desc
GO
