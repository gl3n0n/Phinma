using System;
using System.Web;
using System.Data;
using System.Data.OleDb;
using System.Web;
using EBid.lib.utils;
using EBid.ConnectionString;

namespace EBid.lib.bid.trans
{
	public class BidItemTypeTransaction
	{
        public DataTable GetItemTypes(string connstring)
		{
            DataTable bidDataTable = SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "[s3p_EBid_QueryAllProductItems]").Tables[0];
            return bidDataTable;
		}

        
	}
}
