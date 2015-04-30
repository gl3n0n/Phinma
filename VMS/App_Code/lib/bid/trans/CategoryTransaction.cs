using System;
using System.Collections;
using System.Data;
using System.Data.OleDb;
using Ava.lib;
using Ava.lib.utils;
using Ava.lib.bid.data;
using System.Configuration;
using System.Data.SqlClient;

namespace Ava.lib.bid.trans
{
	public class CategoryTransaction
	{
        private static string connstring = ConfigurationManager.ConnectionStrings["AVAConnectionString"].ConnectionString;
        public ArrayList GetCategories()
        {
            ArrayList categoryArray = new ArrayList();
            DataSet ds = SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "[s3p_EBid_GetCategories]");
            DataTable dt = new DataTable();
            if (ds.Tables.Count > 0)
                dt = ds.Tables[0];

            foreach (DataRow row in dt.Rows)
            {
                Category cat = new Category();
                cat.CategoryName = row["CategoryName"].ToString();
                cat.CategoryId = row["CategoryId"].ToString();
                cat.CategoryDesc = row["CategoryDesc"].ToString();
                categoryArray.Add(cat);
            }
            
            return categoryArray;
        }

		public Category GetCategoryByID(string id)
		{
			Category category = null;

            SqlParameter[] sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@catId", SqlDbType.VarChar);
            sqlParams[0].Value = id;

            DataSet categoryData = SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "s3p_EBid_GetCategoryDetails", sqlParams);
            
            if (categoryData.Tables[0].Rows.Count > 0)
            {

                foreach (DataTable table in categoryData.Tables)
                {
                    foreach (DataRow row in table.Rows)
                    {
                        category = new Category();
                        category.CategoryId = row["CategoryId"].ToString();
                        category.CategoryName = row["CategoryName"].ToString();
                        category.CategoryDesc = row["CategoryDesc"].ToString();
                    }
                }
            }

			return category;
		}


        public string GetCategoryNameById(string vCategoryId)
        {
            if (vCategoryId != "")
            {
                SqlParameter[] sqlParams = new SqlParameter[1];
                sqlParams[0] = new SqlParameter("@catId", SqlDbType.NVarChar);
                sqlParams[0].Value = vCategoryId;

                return SqlHelper.ExecuteScalar(connstring, CommandType.StoredProcedure, "s3p_EBid_GetCategoryNameById", sqlParams).ToString().Trim();
            }
            else
                return "";
        }

        public DataTable GetAllCategories()
        {
            SqlConnection sqlConnect = new SqlConnection(connstring);
            DataSet dsQueryResult = new DataSet();
            sqlConnect.Open();
            dsQueryResult = SqlHelper.ExecuteDataset(sqlConnect, CommandType.StoredProcedure, "s3p_EBid_GetAllProductCategory");
            sqlConnect.Close();
            return dsQueryResult.Tables[0];
        }

        public DataTable SearchCategory(string vSearchText)
        {
            string query = "SELECT Isnull(CategoryId, '') as CategoryId, Isnull(CategoryName, '') as CategoryName " +
                            "FROM  [rfcProductCategory]";
            if (vSearchText !="")
                query = query + " WHERE ((CategoryId like '%" + vSearchText + "%') or (CategoryName like '%" + vSearchText + "%') OR (CategoryDesc like '%" + vSearchText + "%'))";

            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            SqlParameter[] sqlparam = new SqlParameter[1];
            sqlparam[0] = new SqlParameter("@query", SqlDbType.NText);
            ds = SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "s3p_EBid_GetAllProductCategory2", sqlparam);
            if (ds.Tables.Count > 0)
                dt = ds.Tables[0];
            else
            {
                DataColumn dc = new DataColumn("CategoryId", typeof(System.String));
                dt.Columns.Add(dc);
                dc = new DataColumn("CategoryName", typeof(System.String));
                dt.Columns.Add(dc);

                DataRow dr = dt.NewRow();
                dr["CategoryId"] = "";
                dr["CategoryName"] = "No search results";
                dt.Rows.Add(dr);
            }

            return dt;
        }

        public DataTable GetAllSubCategories()
        {
            DataSet dsQueryResult = new DataSet();
            dsQueryResult = SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "[s3p_EBid_GetAllSubCategories]");
            return dsQueryResult.Tables[0];
        }
	}
}