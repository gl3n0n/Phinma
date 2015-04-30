using System;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using EBid.lib;
using EBid.ConnectionString;
using EBid.lib.utils;
using System.Data.OleDb;

/// <summary>
/// Summary description for ItemsCarried
/// </summary>
/// 
namespace EBid.lib.bid.trans
{
    public class ItemsCarriedTransaction
    {
        public DataSet GetAllItemsCarried(string connstring)
        {
            return SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "s3p_EBid_GetAllItemsCarried");
        }

        public DataTable GetAllUnSelectedItemsCarried(string connstring, string vSelectedItems)
        {
            SqlParameter[] sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@selectedItems", SqlDbType.NVarChar);
            sqlParams[0].Value = vSelectedItems;

            return SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "s3p_EBid_GetAllUnSelectedItemsCarried", sqlParams).Tables[0];
        }

        public DataTable GetAllSelectedItemsCarried(string connstring, string vSelectedItems)
        {
            SqlParameter[] sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@selectedItems", SqlDbType.NVarChar);
            sqlParams[0].Value = vSelectedItems;

            return SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "s3p_EBid_GetAllSelectedItemsCarried", sqlParams).Tables[0];
        }



    }
}
