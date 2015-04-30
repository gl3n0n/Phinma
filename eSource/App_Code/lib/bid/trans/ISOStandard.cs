using System;
using System.Web;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.OleDb;
using EBid.lib.utils;
using System.Data.SqlClient;
using EBid.ConnectionString;

/// <summary>
/// Summary description for ISOStandard
/// </summary>
/// 
namespace EBid.lib.bid.trans
{
    public class ISOStandard
    {

        public DataTable GetISOStandard(string connstring)
        {
            SqlConnection sqlConnect = new SqlConnection(connstring);
            DataSet dsQueryResult;
            sqlConnect.Open();

            dsQueryResult = SqlHelper.ExecuteDataset(sqlConnect, CommandType.StoredProcedure, "s3p_EBid_QueryISOStandard");
            sqlConnect.Close();
            return dsQueryResult.Tables[0];
        }

        public DataTable QueryISOStandard(string connstring)
        {
            SqlConnection sqlConnect = new SqlConnection(connstring);
            DataSet dsQueryResult;
            sqlConnect.Open();
            dsQueryResult = SqlHelper.ExecuteDataset(sqlConnect, CommandType.StoredProcedure, "s3p_EBid_QueryISOStandard");
            sqlConnect.Close();
            return dsQueryResult.Tables[0];
        }
    }
}
