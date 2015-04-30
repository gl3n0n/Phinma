using System;
using System.Data.SqlClient;
using System.Data;
using Ava.lib.utils;
using Ava.lib.user.data;
using Ava.lib;
using System.Configuration;

namespace Ava.lib.user.trans
{
    /// <summary>
    /// Summary description for Buyer.
    /// </summary>
    public class BuyerTransaction
    {
        private string connstring = ConfigurationManager.ConnectionStrings["AVAConnectionString"].ConnectionString;
        public string QueryBuyerCodeByBuyerId(string vBuyerId)
        {
            SqlParameter[] sqlparams = new SqlParameter[1];
            sqlparams[0] = new SqlParameter("@BuyerId", SqlDbType.Int);
            sqlparams[0].Value = vBuyerId;
            DataSet buyerData = SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "s3p_EBid_GetBuyerCode", sqlparams);

            DataTable buyerDataTable = buyerData.Tables[0];
            DataRow buyerRow = buyerDataTable.Rows[0];

            return buyerRow["BuyerCode"].ToString().Trim();
        }

        public string QueryBuyerEmailAddByBuyerId(string vBuyerId)
        {
            SqlParameter[] sqlparams = new SqlParameter[1];
            sqlparams[0] = new SqlParameter("@BuyerId", SqlDbType.Int);
            sqlparams[0].Value = vBuyerId;
            DataSet buyerData = SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "s3p_EBid_GetBuyerEmailAddress", sqlparams);

            DataTable buyerDataTable = buyerData.Tables[0];
            DataRow buyerRow = buyerDataTable.Rows[0];

            return buyerRow["EmailAdd"].ToString().Trim();
        }
    }
}
