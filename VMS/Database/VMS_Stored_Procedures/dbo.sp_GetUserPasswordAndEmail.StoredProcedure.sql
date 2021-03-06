USE [vms_transasia]
GO
/****** Object:  StoredProcedure [dbo].[sp_GetUserPasswordAndEmail]    Script Date: 04/30/2015 15:42:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--CREATED BY: EDRICK TAN 08/15/2013
CREATE PROCEDURE [dbo].[sp_GetUserPasswordAndEmail]
	(@UserName nvarchar(100))

AS
--DECLARE @UserName nvarchar(100)
--SET	@UserName = 'admin' -- Vendor

DECLARE @UserType int

SELECT @UserType = UserType from tblUserTypes 
where UserId = (select UserId from tblUsers where UserName like @UserName)

-- Username found and is of type : buyer, vendor or purchasing
IF @UserType IS NOT NULL
  BEGIN
	SELECT UserPassword AS [Password], EmailAdd AS [EmailAddress] FROM tblUsers 
	WHERE UserName LIKE @UserName
	  END

-- Username not found
ELSE 
  SELECT 'NONE' AS [Password], 'NONE' AS [EmailAddress]
GO
