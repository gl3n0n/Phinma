USE [ebid]
GO
/****** Object:  StoredProcedure [dbo].[sp_GetClientDetails]    Script Date: 04/30/2015 15:41:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Created By: GA S. 06072006

CREATE  PROCEDURE [dbo].[sp_GetClientDetails]
	(@clientid int)

AS 

-- This procedure returns the userid of the given username and password
-- otherwise, 0

SELECT	* FROM	tblClients WHERE clientid = @clientid
GO
