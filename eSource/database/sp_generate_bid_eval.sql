USE [ebid_transasia]
GO
/****** Object:  StoredProcedure [dbo].[sp_generate_bid_eval]    Script Date: 07/25/2013 02:52:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--ALTER PROC [dbo].[sp_generate_bid_eval]
--	(@BidRefNo int)
--AS

-- exec sp_generate_bid_eval @BidRefNo=10711
declare @BidRefNo int 
set @BidRefNo=10711
DECLARE @vVendorId varchar(10);
DECLARE @vVendorName varchar(120);
DECLARE @vTable varchar(4000);
DECLARE @vColumn varchar(4000);
DECLARE @vInsert varchar(4000);
DECLARE @vItemDesc varchar(120);
DECLARE @vBidDetailNo int;
DECLARE @vQty int;
DECLARE @vAmount varchar(20);
DECLARE @vSeqNo int;

SET @vTable = 't_' + cast(@BidRefNo as varchar); 
SET @vSeqNo = 1;
BEGIN
	DECLARE vendor_c CURSOR FOR
       select distinct t.VendorId, replace(v.VendorName, '''','')
       from   tblBidTenders t, tblBidItemDetails d, tblVendors v
       where  t.BidDetailNo = d.BidDetailNo
       and    v.VendorId = t.VendorId
       and    d.BidRefNo = 10711
       --order by v.VendorName;

	DECLARE vend_c CURSOR FOR
       select distinct t.VendorId
       from   tblBidTenders t, tblBidItemDetails d, tblVendors v
       where  t.BidDetailNo = d.BidDetailNo
       and    v.VendorId = t.VendorId
       and    d.BidRefNo = 10711
       --order by v.VendorName;

	OPEN vendor_c
	FETCH NEXT FROM vendor_c INTO @vVendorId, @vVendorName
    WHILE @@FETCH_STATUS = 0
	BEGIN
       IF (@vColumn is null) 
          BEGIN
             SET @vColumn = ' t_' + cast(@vVendorId as varchar) + ' varchar(120)'; 
             SET @vInsert = '''' + @vVendorName + ''''; 
          END;
       ELSE
          BEGIN
             SET @vColumn = @vColumn + ', t_' + cast(@vVendorId as varchar) + ' varchar(120)'; 
             SET @vInsert = @vInsert + ',''' + @vVendorName + ''''; 
          END;
	   FETCH NEXT FROM vendor_c INTO @vVendorId, @vVendorName
	END
	CLOSE vendor_c
	DEALLOCATE vendor_c

    exec ('create table ' + @vTable + ' ( SeqNo int, BidDetailNo int, BidItem nvarchar(250), Qty int, ' + @vColumn + ')');
    exec ('insert into ' + @vTable + ' values ( ' + @vSeqNo + ', null, null, null, ' + @vInsert + ')');

	OPEN vend_c
	FETCH NEXT FROM vend_c INTO @vVendorId
    WHILE @@FETCH_STATUS = 0
	BEGIN
       select @vBidDetailNo=d.BidDetailNo, 
              @vItemDesc=d.DetailDesc, 
              @vQty=d.Qty, 
              @vAmount=cast(t.Amount as decimal(12,2))
       from   tblBidTenders t, tblBidItemDetails d, tblVendors v
       where  t.BidDetailNo = d.BidDetailNo
       and    v.VendorId = t.VendorId
       and    d.BidRefNo = @BidRefNo
       and    t.VendorId = @vVendorId;

       SET @vSeqNo = @vSeqNo + 1;
       SET @vInsert = 'insert into ' + @vTable + ' (SeqNo, BidDetailNo, BidItem, Qty, t_' + @vVendorId + ' ) values ( ' + cast(@vSeqNo as nvarchar)+ ', ' + cast(@vBidDetailNo as nvarchar) + ', ''' + @vItemDesc + ''', ' + cast(@vQty as nvarchar) + ',' + @vAmount + ')';
       -- INSERT  INTO sp_log VALUES ('genmatrix', @BidRefNo, @vInsert, getdate());
       EXEC (@vInsert);
	   FETCH NEXT FROM vend_c INTO @vVendorId
	END
	CLOSE vend_c
	DEALLOCATE vend_c

    EXEC ('SELECT * FROM ' + @vTable + ' ORDER BY SeqNo');
    EXEC ('DROP TABLE ' + @vTable);

END;

