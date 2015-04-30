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


/// <summary>
/// Summary description for Incoterm
/// </summary>
/// 
namespace EBid.lib.bid.trans
{
    public class IncotermTransaction
    {
        private static string connstring = ConfigurationManager.ConnectionStrings["EBidConnectionString"].ConnectionString;

        public IncotermTransaction()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        
        public DataTable GetIncoterm()
        {
            DataSet incoterm = SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "s3p_EBid_GetIncoterm1");
            DataTable dt = incoterm.Tables[0];
            DataRow dr = dt.NewRow();
            dr["Id"] = -1;
            dr["Incoterm"] = "";
            dt.Rows.InsertAt(dr, 0);

            return dt;
        }

        public DataSet GetIncoterm_Buyer()
        {
           return SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "s3p_EBid_GetIncoterm1");
        }

        public string GetIncotermName(string id)
        {
            SqlParameter[] sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@id", SqlDbType.Int);
            sqlParams[0].Value = Int32.Parse(id);

            return SqlHelper.ExecuteScalar(connstring, CommandType.StoredProcedure, "s3p_EBid_GetIncotermName", sqlParams).ToString().Trim();
        }
    }
}