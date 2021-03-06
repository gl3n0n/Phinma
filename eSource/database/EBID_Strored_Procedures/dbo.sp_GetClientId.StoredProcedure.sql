USE [ebid]
GO
/****** Object:  StoredProcedure [dbo].[sp_GetClientId]    Script Date: 04/30/2015 15:41:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Created By: GA S. 06072006

CREATE  PROCEDURE [dbo].[sp_GetClientId]
	(@Username nvarchar(100))

AS 

-- This procedure returns the userid of the given username and password
-- otherwise, 0

SELECT 	ISNULL((SELECT	clientId
			FROM	tblUsers
			WHERE	UserName = @Username), 0) AS clientId
GO
