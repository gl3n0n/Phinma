USE [vms_transasia]
GO
/****** Object:  StoredProcedure [dbo].[sp_GetVendorInformation]    Script Date: 04/30/2015 15:42:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Created By: GA S. 09052006

CREATE 	PROCEDURE [dbo].[sp_GetVendorInformation]
	(@VendorId int)
AS

--declare @VendorCode varchar(12)
--declare @FirstVendorChar char(1)
--declare @PreFix varchar(12)
--declare @LastNumberCodes char(12)
--declare @LastNumberInt int
--declare @VendorId int
--set @VendorId = 4

--SELECT @PreFix  = 'H-' + UPPER(SUBSTRING(CompanyName, 1, 1)) + '999'
--FROM tblVendorInformation WHERE VendorId=@VendorId

--SELECT @LastNumberCodes = MAX(SUBSTRING(VENDORID, 4, 99))
--FROM APVEN
--WHERE VENDORID LIKE '%'+@PreFix+'%'

--SELECT @LastNumberInt = cast(dbo.UDF_ParseAlphaChars(@LastNumberCodes) as int) + 1
--SELECT @VendorCode = @PreFix + RIGHT('0000'+ CONVERT(VARCHAR,@LastNumberInt),4) 

--BEGIN
--INSERT INTO APVEN (VENDORID, VENDNAME, TEXTSTRE1, TEXTSTRE2, NAMECITY, CODESTTE, CODEPSTL, CODECTRY, TEXTPHON1, EMAIL2, WEBSITE)
--SELECT @VendorCode VENDORID, t1.CompanyName VENDNAME,
--	   CASE WHEN t1.regBldgBldg != '' THEN t1.regBldgBldg + ' ' ELSE '' END +
--	   CASE WHEN t1.regBldgUnit != '' THEN t1.regBldgUnit + ' ' ELSE '' END +
--	   CASE WHEN t1.regBldgLotNo != '' THEN t1.regBldgLotNo + ' ' ELSE '' END +
--	   CASE WHEN t1.regBldgBlock != '' THEN t1.regBldgBlock + ' ' ELSE '' END +
--	   CASE WHEN t1.regBldgPhase != '' THEN t1.regBldgPhase + ' ' ELSE '' END 
--	   TEXTSTRE1,
--	   CASE WHEN t1.regBldgHouseNo != '' THEN t1.regBldgHouseNo + ' ' ELSE '' END +
--	   CASE WHEN regBldgStreet != '' THEN t1.regBldgStreet + ' ' ELSE '' END +
--	   CASE WHEN t1.regBldgSubd != '' THEN t1.regBldgSubd + ' ' ELSE '' END +
--	   CASE WHEN t1.regBrgy != '' THEN t1.regBrgy + ' ' ELSE '' END
--	   TEXTSTRE2,
--	   t1.regCity CITY, t1.regProvince PROVINCE, t1.regPostal ZIPCODE, t1.regCountry COUNTRY,
--	   t1.conSalTelNo TEXTPHON1, t1.conSalEmail EMAIL2, '' WEBSITE
--FROM tblVendorInformation t1, tblVendor t2
--WHERE t1.VendorId = @VendorId AND t2.VendorId = t1.VendorId
--END

BEGIN
SELECT t2.VendorCode VENDORID, 
		t1.CompanyName VENDNAME,
		CASE WHEN t1.regBldgBldg != '' THEN t1.regBldgBldg + ' ' ELSE '' END +
		CASE WHEN t1.regBldgUnit != '' THEN t1.regBldgUnit + ' ' ELSE '' END +
		CASE WHEN t1.regBldgLotNo != '' THEN t1.regBldgLotNo + ' ' ELSE '' END +
		CASE WHEN t1.regBldgBlock != '' THEN t1.regBldgBlock + ' ' ELSE '' END +
		CASE WHEN t1.regBldgPhase != '' THEN t1.regBldgPhase + ' ' ELSE '' END 
		TEXTSTRE1,
		CASE WHEN t1.regBldgHouseNo != '' THEN t1.regBldgHouseNo + ' ' ELSE '' END +
		CASE WHEN regBldgStreet != '' THEN t1.regBldgStreet + ' ' ELSE '' END +
		CASE WHEN t1.regBldgSubd != '' THEN t1.regBldgSubd + ' ' ELSE '' END +
		CASE WHEN t1.regBrgy != '' THEN t1.regBrgy + ' ' ELSE '' END
		TEXTSTRE2,
		t1.regCity CITY, 
		t1.regProvince PROVINCE, 
		t1.regPostal ZIPCODE, 
		t1.regCountry COUNTRY,
		t1.conSalTelNo TEXTPHON1, 
		t1.conSalName CONTACT,
		t1.conSalTelNo CONTACTPHONE,
		t1.conSalFaxNo CTACTFAX,
		t1.conSalEmail EMAIL1, 
		t1.conSalEmail EMAIL2, 
		'' WEBSITE, 
		t2.AccGroup GROUPID
FROM tblVendorInformation t1, tblVendor t2
WHERE t1.VendorId = @VendorId AND t2.VendorId = t1.VendorId
END
GO
