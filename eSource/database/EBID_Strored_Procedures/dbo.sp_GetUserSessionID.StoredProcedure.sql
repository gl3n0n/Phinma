USE [ebid]
GO
/****** Object:  StoredProcedure [dbo].[sp_GetUserSessionID]    Script Date: 04/30/2015 15:41:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--created By Eric 11222007

CREATE  PROC [dbo].[sp_GetUserSessionID]
	(@UserId int)
AS


SELECT ISNULL(SessionId, '') AS SessionId

FROM tblUsers

WHERE UserId = @UserId
GO
