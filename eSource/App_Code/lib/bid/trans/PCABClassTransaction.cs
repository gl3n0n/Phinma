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
using EBid.lib.utils;
using System.Data.OleDb;
using EBid.lib;
using EBid.ConnectionString;
using System.Data.SqlClient;


/// <summary>
/// Summary description for PCABClassTransaction
/// </summary>
namespace EBid.lib.bid.trans
{
    public class PCABClassTransaction
    {
        public DataSet GetAllPCABClass(string connstring)
        {
            return SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "s3p_EBid_GetAllPCABClass");
        }

        public string GetPCABClasName(string connstring, string PCABClassId)
        {
            if (PCABClassId != "")
            {
                SqlParameter[] sqlParams = new SqlParameter[1];
                sqlParams[0] = new SqlParameter("@PCABClassId", SqlDbType.Int);
                sqlParams[0].Value = PCABClassId;
                return Convert.ToString(SqlHelper.ExecuteScalar(connstring, CommandType.StoredProcedure, "s3p_EBid_GetPCABClassName", sqlParams));
            }
            else
                return "";
        }


    }
}