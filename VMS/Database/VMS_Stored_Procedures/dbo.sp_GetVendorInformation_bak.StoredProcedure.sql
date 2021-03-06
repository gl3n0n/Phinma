USE [vms_transasia]
GO
/****** Object:  StoredProcedure [dbo].[sp_GetVendorInformation_bak]    Script Date: 04/30/2015 15:42:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Created By: GA S. 09052006

CREATE 	PROCEDURE [dbo].[sp_GetVendorInformation_bak]
	(@VendorId int)
AS

declare @VendorCode varchar(12)
declare @FirstVendorChar char(1)
declare @PreFix varchar(12)
declare @LastNumberCodes char(12)
declare @LastNumberInt int
--declare @VendorId int
--set @VendorId = 38

SELECT @PreFix  = 'H-' + UPPER(SUBSTRING(CompanyName, 1, 1))
FROM tblVendorInformation WHERE VendorId=@VendorId

SELECT @LastNumberCodes = MAX(SUBSTRING(VENDORID, 4, 99))
FROM [SHIDAT].dbo.APVEN
WHERE VENDORID LIKE '%'+@PreFix+'%'

SELECT @LastNumberInt = cast(dbo.UDF_ParseAlphaChars(@LastNumberCodes) as int) + 1
SELECT @VendorCode = @PreFix + RIGHT('000'+ CONVERT(VARCHAR,@LastNumberInt),4) 

SELECT @VendorCode VENDORID, CompanyName VENDNAME,
	   CASE WHEN regBldgBldg != '' THEN regBldgBldg + ' ' ELSE '' END +
	   CASE WHEN regBldgUnit != '' THEN regBldgUnit + ' ' ELSE '' END +
	   CASE WHEN regBldgLotNo != '' THEN regBldgLotNo + ' ' ELSE '' END +
	   CASE WHEN regBldgBlock != '' THEN regBldgBlock + ' ' ELSE '' END +
	   CASE WHEN regBldgPhase != '' THEN regBldgPhase + ' ' ELSE '' END 
	   TEXTSTRE1,
	   CASE WHEN regBldgHouseNo != '' THEN regBldgHouseNo + ' ' ELSE '' END +
	   CASE WHEN regBldgStreet != '' THEN regBldgStreet + ' ' ELSE '' END +
	   CASE WHEN regBldgSubd != '' THEN regBldgSubd + ' ' ELSE '' END +
	   CASE WHEN regBrgy != '' THEN regBrgy + ' ' ELSE '' END
	   TEXTSTRE2,
	   regCity CITY, regProvince PROVINCE, regPostal ZIPCODE, regCountry COUNTRY,
	   conBidTelNo TEXTPHON1, conBidEmail EMAIL2, '' WEBSITE, 'SUPPLR' GROUPID




	   

FROM tblVendorInformation
WHERE VendorId = @VendorId
GO
