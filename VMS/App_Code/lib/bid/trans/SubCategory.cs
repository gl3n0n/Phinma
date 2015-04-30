using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.OleDb;
using Ava.lib.utils;
using System.Data.SqlClient;

/// <summary>
/// Summary description for SubCategory
/// </summary>
/// 
namespace Ava.lib.bid.trans
{
    public class SubCategory
    {
        private string connstring = ConfigurationManager.ConnectionStrings["AVAConnectionString"].ConnectionString;

        public DataTable GetAllSubCategories()
        {
            SqlConnection sqlConnect = new SqlConnection(connstring);
            DataSet dsQueryResult;
            dsQueryResult = SqlHelper.ExecuteDataset(sqlConnect,CommandType.StoredProcedure, "s3p_EBid_GetAllProductSubCategory");
            sqlConnect.Close();
            return dsQueryResult.Tables[0];
        }

        public DataTable GetSubCategoryByCategoryId(string categoryId)
        {
            SqlConnection sqlConnect = new SqlConnection(connstring);
            DataSet dsQueryResult;
            sqlConnect.Open();
            SqlParameter[] sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@categoryId", SqlDbType.Int);
            sqlParams[0].Value = categoryId;
            dsQueryResult = SqlHelper.ExecuteDataset(sqlConnect, "s3p_EBid_GetProductSubCategoryByCategoryId", sqlParams);
            sqlConnect.Close();
            return dsQueryResult.Tables[0];
        }

        public string GetCategoryIdBySubCategory(string vSubCateogryId)
        {
            if (vSubCateogryId != "")
            {
                string query = "SELECT [CategoryId] FROM [rfcProductSubCategory] WHERE [SubCategoryId]=" + vSubCateogryId;
                DataSet subcategoryData = new DataSet();
                SqlParameter[] sqlparam = new SqlParameter[1];
                sqlparam[0] = new SqlParameter("@query", SqlDbType.NText);
                sqlparam[0].Value = query;
                subcategoryData = SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "[s3p_EBid_GenericQueryProcedure]", sqlparam);
                return subcategoryData.Tables[0].Rows[0]["CategoryId"].ToString().Trim();
            }
            else
                return "";
        }

        public string GetSubCategoryNameById(string subcategoryId)
        {
            if (subcategoryId != "")
            {
                SqlParameter[] sqlParams = new SqlParameter[1];
                sqlParams[0] = new SqlParameter("@subCatId", SqlDbType.Int);
                sqlParams[0].Value = Int32.Parse(subcategoryId);

                return SqlHelper.ExecuteScalar(connstring, CommandType.StoredProcedure, "s3p_EBid_GetSubCategoryNameById", sqlParams).ToString().Trim();
            }
            else
            {
                return "";
            }
        }

    }
}
