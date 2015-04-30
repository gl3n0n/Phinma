using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.OleDb;
using EBid.lib;
using EBid.lib.utils;
using EBid.lib.constant;

namespace EBid.lib.user.trans
{

    public class VendorTransaction
    {
        private static string connstring = System.Configuration.ConfigurationManager.ConnectionStrings["EBidConnectionString"].ConnectionString;

        public VendorTransaction()
        {
        }

        public DataTable GetNewBidEvents(int vendorId, int status, int isAuction)
        {
            SqlParameter[] sqlParams = new SqlParameter[3];
            sqlParams[0] = new SqlParameter("@VendorId", SqlDbType.Int);
            sqlParams[1] = new SqlParameter("@ForAuction", SqlDbType.Int);
            sqlParams[2] = new SqlParameter("@Status", SqlDbType.Int);
            sqlParams[0].Value = vendorId;
            sqlParams[1].Value = isAuction;
            sqlParams[2].Value = status;

            return SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "s3p_EBid_GetNewBidEvents", sqlParams).Tables[0];
        }
    }
}