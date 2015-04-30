using System;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.OleDb;
using EBid.lib;
using EBid.ConnectionString;
using EBid.lib.utils;
using EBid.lib.bid.data;
using EBid.lib.constant;

/// <summary>
/// Summary description for BACTransaction
/// </summary>
/// 
namespace EBid.lib.user.trans
{
    public class BACTransaction
    {
        public BACTransaction()
        {
           
        }

        public static string GetBACFirstName(string connstring, int bacID)
        {
            SqlParameter[] sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@userId", SqlDbType.Int);
            sqlParams[0].Value = bacID;

            return SqlHelper.ExecuteScalar(connstring, CommandType.StoredProcedure, "s3p_EBid_GetBACFirstName", sqlParams).ToString().Trim();
        }

        public static int GetForOpenBidsCount(string connstring)
        {

            SqlConnection sqlConnect = new SqlConnection(connstring);

            int count = 0;

            sqlConnect.Open();
            count = Convert.ToInt32(SqlHelper.ExecuteScalar(sqlConnect, CommandType.StoredProcedure, "s3p_EBid_GetBidsForOpenCount"));
            sqlConnect.Close();

            return count;
        }

        public static int GetCountBidEventsForOpening(string connstring)
        {

            SqlConnection sqlConnect = new SqlConnection(connstring);

            int count = 0;

            sqlConnect.Open();
            count = Convert.ToInt32(SqlHelper.ExecuteScalar(sqlConnect, CommandType.StoredProcedure, "sp_QueryCountBidsEventsForOpening"));
            sqlConnect.Close();

            return count;
        }

        public static int GetCountBidEventsOpened(string connstring)
        {

            SqlConnection sqlConnect = new SqlConnection(connstring);

            int count = 0;

            sqlConnect.Open();
            count = Convert.ToInt32(SqlHelper.ExecuteScalar(sqlConnect, CommandType.StoredProcedure, "sp_QueryCountBidsEventsOpened"));
            sqlConnect.Close();

            return count;
        }


    }
}