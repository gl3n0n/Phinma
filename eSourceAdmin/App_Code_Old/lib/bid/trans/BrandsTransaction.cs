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
/// Summary description for BrandsTransaction
/// </summary>
/// 
namespace EBid.lib.bid.trans
{
    public class BrandsTransaction
    {
        private  string connstring = "";
        public DataSet GetAllBrands(string connstring)
        {
            //connstring = System.Configuration.ConfigurationManager.ConnectionStrings["EBidConnectionString"].ConnectionString;
            return SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "s3p_EBid_GetAllBrands");
        }


        public DataTable GetAllSelectedBrands(string vSelectedBrandIds)
        {
            DataTable dt = new DataTable();
            if (vSelectedBrandIds != string.Empty)
            {
                string sql = "SELECT [BrandId], [BrandName] FROM [rfcProductBrands] WHERE BrandId in (" + vSelectedBrandIds.Trim() + ")";
                SqlParameter[] sqlparams = new SqlParameter[1];
                sqlparams[0] = new SqlParameter("@SQL", SqlDbType.NText);
                sqlparams[0].Value = sql;
                DataSet ds = SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "[s3p_EBid_GetAllSelectedBrands]", sqlparams);
                if (ds.Tables.Count > 0)
                    dt = ds.Tables[0];                
            }

            return dt;
        }

        public DataTable GetAllUnSelectedBrands(string vSelectedBrandIds)
        {
            //calls same sql procedure as in GetAllSelectedBrands but sql statement passed is different
            DataTable dt = new DataTable();
           
            try
            {
                string sql = "SELECT [BrandId], [BrandName] FROM [rfcProductBrands]";
                if (vSelectedBrandIds != String.Empty)
                    sql += " WHERE BrandId not in (" + vSelectedBrandIds.Trim() + ")";
                SqlParameter[] sqlparams = new SqlParameter[1];
                sqlparams[0] = new SqlParameter("@SQL", SqlDbType.NText);
                sqlparams[0].Value = sql;
                DataSet ds = SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "[s3p_EBid_GetAllSelectedBrands]", sqlparams);
                if (ds.Tables.Count > 0)
                    dt = ds.Tables[0];
            }
            catch
            {
            }
            
            return dt;
        }
    }
}
