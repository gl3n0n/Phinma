using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using EBid.lib.utils;
using System.Data.OleDb;
using EBid.lib;
using System.Data.SqlClient;


/// <summary>
/// Summary description for SupplierType
/// </summary>
/// 
namespace EBid.lib.bid.trans
{
    public class SupplierType
    {
        private static string connstring = ConfigurationManager.ConnectionStrings["EBidConnectionString"].ConnectionString;


        public DataTable GetAllSupplierTypes()
        {
            string query = "SELECT [SupplierTypeId], [SupplierTypeDesc] " +
                    "FROM [rfcSupplierType]";
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            SqlParameter[] sqlparam = new SqlParameter[1];
            sqlparam[0] = new SqlParameter("@query", SqlDbType.NText);
            sqlparam[0].Value = query;
            ds = SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "[s3p_EBid_GenericQueryProcedure]", sqlparam);
            if (ds.Tables.Count > 0)
                dt = ds.Tables[0];
            return dt;
        }

        public string GetSupplierType(string SupplierTypeId)
        {
            if (SupplierTypeId != "")
            {
                SqlParameter[] sqlParams = new SqlParameter[1];
                sqlParams[0] = new SqlParameter("@SupplierTypeId", SqlDbType.Int);
                sqlParams[0].Value = SupplierTypeId;
                return Convert.ToString(SqlHelper.ExecuteScalar(connstring, CommandType.StoredProcedure, "s3p_EBid_GetSupplierType", sqlParams));
            }
            else
                return "";

        }
    }
}