using System;
using System.Data;
using System.Data.OleDb;
using Ava.lib.utils;

namespace Ava.lib.bid.trans
{
	public class BidItemTypeTransaction
	{
        private static string connstring = System.Configuration.ConfigurationManager.ConnectionStrings["AVAConnectionString"].ConnectionString;

        public DataTable GetItemTypes()
		{
            DataTable bidDataTable = SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "[s3p_EBid_QueryAllProductItems]").Tables[0];
            return bidDataTable;
		}

        
	}
}
