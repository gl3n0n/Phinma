using System;
using System.Data;
using System.Data.OleDb;
using EBid.lib.utils;

namespace EBid.lib.bid.trans
{
	public class BidItemTypeTransaction
	{
        private static string connstring = System.Configuration.ConfigurationManager.ConnectionStrings["EBidConnectionString"].ConnectionString;

        public DataTable GetItemTypes()
		{
            DataTable bidDataTable = SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "[s3p_EBid_QueryAllProductItems]").Tables[0];
            return bidDataTable;
		}

        
	}
}
