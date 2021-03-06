USE [ebid]
GO
/****** Object:  StoredProcedure [dbo].[sp_Report_Vendor_Info]    Script Date: 04/30/2015 15:41:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[sp_Report_Vendor_Info]
   (@DateFrom datetime, @DateTo datetime)--, @ClientId int)
AS
   DECLARE @DBName nvarchar(200)
   DECLARE @SQL nvarchar(4000)

   select  @DBName = databaseName
   from    tblClients
   where   clientid = 1--@ClientId

   SET @SQL = @DBName + '.dbo.sp_Report_Vendor_Info @DateFrom=''' + convert(nvarchar(30), @DateFrom, 120) + ''',@DateTo=''' + convert(nvarchar(30), @DateTo, 120) + ''''
   insert into sp_log values ('sp_Report_Vendor_Info', @SQL, getdate())
   exec (@SQL)

   -- select * from sp_log order by _date desc
GO
