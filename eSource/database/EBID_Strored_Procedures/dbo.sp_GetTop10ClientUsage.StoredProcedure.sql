USE [ebid]
GO
/****** Object:  StoredProcedure [dbo].[sp_GetTop10ClientUsage]    Script Date: 04/30/2015 15:41:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE  PROC [dbo].[sp_GetTop10ClientUsage]
AS
   SELECT top 10 
          t2.configCompanyName CompanyName, 
          t1.ClientId,
          sum(t1.DiskUsage) DiskUsage,
          sum(t1.NumUsers) NumUsers,
          sum(t1.NumVendors) NumVendors,
          sum(t1.NumBidEvents) NumBidEvents,
          sum(t1.NumAuctionEvents) NumAuctionEvents
   FROM   tblClientsUsage t1, tblClients t2
   WHERE  t1.ClientId=t2.ClientId
   GROUP  by t2.configCompanyName, t1.ClientId
   ORDER  BY 3 desc
GO
