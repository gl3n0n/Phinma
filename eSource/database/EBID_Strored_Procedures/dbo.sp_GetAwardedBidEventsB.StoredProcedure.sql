USE [ebid]
GO
/****** Object:  StoredProcedure [dbo].[sp_GetAwardedBidEventsB]    Script Date: 04/30/2015 15:41:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE  PROC [dbo].[sp_GetAwardedBidEventsB]
	(@UserId int, @UserType int)

AS
   insert into ebid.dbo.sp_log values ('sp_GetAwardedBidEventsB', '@UserId=' + CAST(@UserId as varchar), getdate())
   exec ebid_uat.dbo.sp_GetAwardedBidEventsB @UserId=@UserId, @UserType=@UserType
GO
