USE [vms_transasia]
GO
/****** Object:  StoredProcedure [dbo].[sp_InsertUpdateVendorToAPVEN]    Script Date: 04/30/2015 15:42:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Created By: GA S. 09052006

CREATE 	PROCEDURE [dbo].[sp_InsertUpdateVendorToAPVEN]
	(
		@VENDORID varchar(12),
		@VENDNAME varchar(60),
		@TEXTSTRE1 varchar(60), 
		@TEXTSTRE2 varchar(60), 
		@NAMECITY varchar(30), 
		@CODESTTE varchar(30), 
		@CODEPSTL varchar(20), 
		@CODECTRY varchar(30), 
		@TEXTPHON1 varchar(30), 
		@EMAIL2 varchar(50), 
		@WEBSITE varchar(100)
	)
AS

BEGIN
	IF EXISTS(SELECT 1 FROM APVEN WHERE VENDORID = @VENDORID)
		BEGIN
			UPDATE APVEN SET VENDORID=@VENDORID, VENDNAME=@VENDNAME, TEXTSTRE1=@TEXTSTRE1, TEXTSTRE2=@TEXTSTRE2, NAMECITY=@NAMECITY, CODESTTE=@CODESTTE, CODEPSTL=@CODEPSTL, CODECTRY=@CODECTRY, TEXTPHON1=@TEXTPHON1, EMAIL2=@EMAIL2, WEBSITE=@WEBSITE WHERE VENDORID=@VENDORID
		END
	ELSE
		BEGIN
			INSERT INTO APVEN 
			(VENDORID, VENDNAME, TEXTSTRE1, TEXTSTRE2, NAMECITY, CODESTTE, CODEPSTL, CODECTRY, TEXTPHON1, EMAIL2, WEBSITE) 
			VALUES
			(@VENDORID, @VENDNAME, @TEXTSTRE1, @TEXTSTRE2, @NAMECITY, @CODESTTE, @CODEPSTL, @CODECTRY, @TEXTPHON1, @EMAIL2, @WEBSITE)		
		END
END

--BEGIN
--	UPDATE tblVendor Set VendorCode = @VendorCode 
--	WHERE VendorId = @VendorId
--END
GO
