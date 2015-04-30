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
using Ava.lib;
using Ava.lib.utils;
using System.Data.OleDb;

/// <summary>
/// Summary description for ItemsCarried
/// </summary>
/// 
namespace Ava.lib.bid.trans
{
    public class ItemsCarriedTransaction
    {
        private static string connstring = ConfigurationManager.ConnectionStrings["AVAConnectionString"].ConnectionString;

        
        public DataSet GetAllItemsCarried()
        {
            return SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "s3p_EBid_GetAllItemsCarried");
        }

        public DataTable GetAllUnSelectedItemsCarried(string vSelectedItems)
        {
            SqlParameter[] sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@selectedItems", SqlDbType.NVarChar);
            sqlParams[0].Value = vSelectedItems;

            return SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "s3p_EBid_GetAllUnSelectedItemsCarried", sqlParams).Tables[0];
        }

        public DataTable GetAllSelectedItemsCarried(string vSelectedItems)
        {
            SqlParameter[] sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@selectedItems", SqlDbType.NVarChar);
            sqlParams[0].Value = vSelectedItems;

            return SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "s3p_EBid_GetAllSelectedItemsCarried", sqlParams).Tables[0];
        }



    }
}
