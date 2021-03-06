USE [vms_transasia]
GO
/****** Object:  StoredProcedure [dbo].[sp_AVA_GetVendorForSendingToSAP]    Script Date: 04/30/2015 15:42:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROC [dbo].[sp_AVA_GetVendorForSendingToSAP]

AS
SELECT 
	t1.VendorId, 
	t1.VendorCode, 
	t1.CompanyName, 
	t1.AccGroup, 
	t1.VendorAlias, 
	t2.	regBldgCode, 
	t2.regBldgRoom, 
	t2.regBldgFloor, 
	t2.regBldgHouseNo, 
	t2.regStreetName, 
	t2.regCity, 
	t2.regProvince, 
	t2.regCountry, 
	t2.regPostal, 'vat' as VAT, 
	t2.birRegTIN, 
	t2.busPermitNo, 
	t2.conLegName, 
	t1.PurchasingOrg, 
	'php' AS currency, 
	'1time' as paymentTerm, 
	t2.conLegEmail, 
	t2.conLegMobile, 
	t3.dnbMaxExposureLimit
FROM 
	tblVendor t1, tblvendorInformation t2, tblDnbReport t3 
WHERE 
	t2.VendorId = t1.VendorId and 
	t1.NotificationSent is not null and 
	t1.SendToSAP_Status = 1 and 
	t3.VendorId = t1.VendorId and
	t1.Status = 6
GO
