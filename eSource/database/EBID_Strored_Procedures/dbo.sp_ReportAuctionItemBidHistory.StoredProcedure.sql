USE [ebid]
GO
/****** Object:  StoredProcedure [dbo].[sp_ReportAuctionItemBidHistory]    Script Date: 04/30/2015 15:41:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[sp_ReportAuctionItemBidHistory] 
AS
  insert into ebid.dbo.sp_log values ('sp_ReportAuctionItemBidHistory', 'no parameter.', getdate())
  exec ebid_abcompany.dbo.sp_ReportAuctionItemBidHistory
GO
