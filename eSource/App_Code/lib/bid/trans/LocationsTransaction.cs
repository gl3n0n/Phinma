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
/// Summary description for Locations
/// </summary>
/// 
namespace EBid.lib.bid.trans
{
    public class LocationsTransaction
    {
        public DataSet GetAllLocations(string connstring)
        {
            return SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "s3p_EBid_GetAllLocations");
        }


        public DataTable GetAllSelectedLocations(string connstring, string vlocationids)
        {
            DataTable dt = new DataTable();
            if (vlocationids != String.Empty)
            {
                SqlParameter[] sqlParams = new SqlParameter[1];
                sqlParams[0] = new SqlParameter("@locationIds", SqlDbType.NVarChar);
                sqlParams[0].Value = vlocationids;

                DataSet ds = SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "s3p_EBid_GetAllSelectedLocations", sqlParams);
                if (ds.Tables.Count > 0)
                    dt = ds.Tables[0];
            }
            return dt;
        }

        public DataTable GetAllUnSelectedLocations(string connstring, string vlocationids)
        {
            DataTable dt = new DataTable();
                SqlParameter[] sqlParams = new SqlParameter[1];
                sqlParams[0] = new SqlParameter("@locationIds", SqlDbType.NVarChar);
                sqlParams[0].Value = vlocationids;

                DataSet ds = SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "s3p_EBid_GetAllUnSelectedLocations", sqlParams);
                if (ds.Tables.Count > 0)
                    dt = ds.Tables[0];
            return dt;
        }
    }
}

