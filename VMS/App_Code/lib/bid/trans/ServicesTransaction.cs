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
/// Summary description for ServicesTransaction
/// </summary>

namespace Ava.lib.bid.trans
{

    public class ServicesTransaction
    {
        private static string connstring = System.Configuration.ConfigurationManager.ConnectionStrings["AVAConnectionString"].ConnectionString;

        public DataSet GetAllServices()
        {
           return SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "s3p_EBid_GetAllServices");
        }

        public DataTable GetAllSelectedServices(string selectedServiceIds)
        {
            SqlParameter[] sqlparams = new SqlParameter[1];
            sqlparams[0] = new SqlParameter("@servicesIds", SqlDbType.NVarChar);
            sqlparams[0].Value = selectedServiceIds;

            return SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "s3p_EBid_GetAllSelectedServices", sqlparams).Tables[0];
        }

        public DataTable GetAllUnSelectedServices(string selectedServiceIds)
        {
            DataTable dt  = new DataTable();
           
                SqlParameter[] sqlparams = new SqlParameter[1];
                sqlparams[0] = new SqlParameter("@servicesIds", SqlDbType.NVarChar);
                sqlparams[0].Value = selectedServiceIds;

                DataSet ds = SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "s3p_EBid_GetAllUnSelectedServices", sqlparams);
                if (ds.Tables.Count > 0)
                    dt = ds.Tables[0];
           
            return dt;
        }
    }
}