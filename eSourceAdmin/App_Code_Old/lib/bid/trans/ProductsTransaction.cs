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
using System.Data.SqlClient;
using EBid.lib;
using EBid.lib.utils;
using EBid.lib.bid.data;

/// <summary>
/// Summary description for ProductsTransaction
/// </summary>
/// 
namespace EBid.lib.bid.trans
{
    public class ProductsTransaction
    {
        private string connstring = System.Configuration.ConfigurationManager.ConnectionStrings["EBidConnectionString"].ConnectionString;
        public Product GetUOMAndDescriptionByProductId(string vProductId)
        {
            Product p = new Product();
            if (vProductId != "")
            {
                SqlParameter[] sqlParams = new SqlParameter[1];
                sqlParams[0] = new SqlParameter("@productId", SqlDbType.NVarChar);
                sqlParams[0].Value = vProductId;

                DataSet productData = SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "s3p_EBid_GetUOMAndDescriptionByProductId", sqlParams);

                if (productData.Tables[0].Rows.Count > 0)
                {
                    p.ProductDescription = productData.Tables[0].Rows[0]["ProductDescription"].ToString().Trim();
                    p.UnitOfMeasure = productData.Tables[0].Rows[0]["UnitOfMeasure"].ToString().Trim();
                    
                }
                else
                {
                    p.ProductDescription = "";
                    p.UnitOfMeasure = "";   
                }
            }
            else
            {
                p.ProductDescription = "";
                p.UnitOfMeasure = "";   
            }
            return p;
        }

        public string GetUOMBySubCategoryId(string vsubcateogryId)
        {
            SqlParameter[] sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@subCatId", SqlDbType.Int);
            sqlParams[0].Value = Int32.Parse(vsubcateogryId);

            return SqlHelper.ExecuteScalar(connstring, CommandType.StoredProcedure, "s3p_EBid_GetUOMBySubCategoryId", sqlParams).ToString().Trim();
        }

        public string GetUOMByProductId(string vProductId)
        {
            if (vProductId != "")
            {
                try
                {
                    SqlParameter[] sqlParams = new SqlParameter[1];
                    sqlParams[0] = new SqlParameter("@prodId", SqlDbType.NVarChar);
                    sqlParams[0].Value = vProductId;

                    DataSet subcategoryData = SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "s3p_EBid_GetUOMByProductId", sqlParams);

                    if (subcategoryData.Tables[0].Rows.Count > 0)
                        return subcategoryData.Tables[0].Rows[0]["UnitOfMeasure"].ToString().Trim();
                    else
                        return "No Unit Of Measure Available";
                }
                catch (Exception e)
                {
                    string strMessage = e.Message.Trim();
                    return "No Unit Of Measure Available";
                }
            }
            else
            {
                return "Select a product";
            }
        }

        public DataTable SearchProduct(string vSearch, string vBrandName, string vService, string vSubCategory, string connstring)
        {
                    string query = "SELECT Isnull(P.[SKU], '') as [SKU], " +
                                    "Isnull(P.[ItemName], '') as [ItemName], " +
                                    "Isnull(P.[ProductDescription], '') as [ProductDescription], " +
                                    "Isnull(S.[ServiceName], '') as [ServiceName], " +
                                    "Isnull(B.[BrandName], '') as [BrandName], " +
                                    "Isnull(SC.[SubCategoryName], '') as [SubCategoryName], " +
                                    "Isnull(P.[UnitOfMeasure], '') as [UnitOfMeasure] " +
                                    "FROM [tblProducts] P " +
                                    "LEFT JOIN [rfcServices] S " +
                                    "ON P.[Service] = S.[ServiceId] " +
                                    "LEFT JOIN [rfcProductBrands] B " +
                                    "ON P.[Brand] = B.[BrandId] " +
                                    "LEFT JOIN [rfcProductSubCategory] SC " +
                                    "ON P.[SubCategoryId] = SC.[SubCategoryId] ";

                    if ((vSearch != "") || (vBrandName != "") || (vService != "") || (vSubCategory != ""))
                    {
                        query = query + "WHERE ";
                        if (vSearch != "")
                        {
                            query = query + "((P.[SKU] like '%" + vSearch + "%') " +
                                                "OR (P.[ItemName] like '%" + vSearch + "%') " +
                                                "OR (P.[ProductDescription] like '%" + vSearch + "%') " +
                                                "OR (S.[ServiceName] like '%" + vSearch + "%') " +
                                                "OR (B.[BrandName] like '%" + vSearch + "%') " +
                                                "OR (SC.[SubCategoryName] like '%" + vSearch + "%') " +
                                                ")";
                        }
                        if (vService != "")
                        {
                            if (vSearch != "")
                                query = query + "OR ";
                            query = query + "(S.[ServiceId] = " + vService + ") ";
                        }
                        if (vBrandName != "") 
                        {
                            if ((vSearch != "") || (vService != ""))
                                query = query + "OR ";
                            query = query + "(B.[BrandId] =" + vBrandName + ") ";
                        }
                        //08/10/2006 there was a change in the items displayed in the subcategory field
                        //items displayed in the subcategory field are taken from services table as requested
                        if (vSubCategory != "")
                        {
                            if ((vSearch != "") || (vService != "") || (vBrandName != ""))
                                query = query + "OR ";
                            query = query + "(SC.[SubCategoryId] = " + vSubCategory + ") ";
                        }
                    
                    }
                    
                    
                    DataSet subcategoryData = new DataSet();
                    SqlParameter[] sqlparam = new SqlParameter[1];
                    sqlparam[0] = new SqlParameter("@query", SqlDbType.NText);
                    sqlparam[0].Value = query;

                    subcategoryData = SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "[s3p_EBid_GenericQueryProcedure]", sqlparam);
                    
                    if (subcategoryData.Tables.Count > 0)
                        return subcategoryData.Tables[0];
                    else
                    {
                        DataTable dt = new DataTable();
                        return dt;
                    }
        }


        public DataTable QueryProducts(string vSearchString, string vSubCategoryId, string vCategoryId, string vMode)
        {

            SqlParameter[] sqlParams = new SqlParameter[4];
            sqlParams[0] = new SqlParameter("@SearchString", SqlDbType.NVarChar);
            sqlParams[0].Value = vSearchString;
            sqlParams[1] = new SqlParameter("@SubCategoryId", SqlDbType.NVarChar);
            sqlParams[1].Value = vSubCategoryId;
            sqlParams[2] = new SqlParameter("@CategoryId", SqlDbType.NVarChar);
            sqlParams[2].Value = vCategoryId;
            sqlParams[3] = new SqlParameter("@Mode", SqlDbType.NVarChar);
            sqlParams[3].Value = vMode;
            DataSet ds = SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "[s3p_EBid_QueryProducts]", sqlParams);
            DataTable dt  = new DataTable();
            if (ds.Tables.Count > 0)
                dt = ds.Tables[0];
            
            return dt;
           
        }

        public Product QueryProductBySKU(string vSKU, string connstring)
        {

            SqlParameter[] sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@SKU", SqlDbType.NVarChar);
            sqlParams[0].Value = vSKU;
            DataSet ds = SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "[s3p_EBid_QueryProductDetailBySKU]", sqlParams);
            DataTable dt = new DataTable();
            Product p = new Product();
            if (ds.Tables.Count > 0)
            {
                dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    p.ItemName = dt.Rows[0]["ItemName"].ToString().Trim();
                    p.SKU = dt.Rows[0]["SKU"].ToString().Trim();
                    p.ProductDescription = dt.Rows[0]["ProductDescription"].ToString().Trim();
                    p.UnitOfMeasure = dt.Rows[0]["UnitOfMeasure"].ToString().Trim();
                    p.Category = dt.Rows[0]["CategoryId"].ToString().Trim();
                    p.SubCategory = dt.Rows[0]["SubCategoryId"].ToString().Trim();
                    p.ServiceType = dt.Rows[0]["Service"].ToString().Trim();
                    p.Brand = dt.Rows[0]["Brand"].ToString().Trim();

                }
                else
                {
                    p.ItemName = "";
                    p.SKU = "";
                    p.ProductDescription = "";
                    p.UnitOfMeasure = "";
                    p.Category = "";
                    p.SubCategory = "";
                    p.ServiceType = "";
                    p.Brand = "";
                }
            }
            else
            {
                p.ItemName = "";
                p.SKU = "";
                p.ProductDescription = "";
                p.UnitOfMeasure = "";
                p.Category = "";
                p.SubCategory = "";
                p.ServiceType = "";
                p.Brand = "";
            }

            return p;
        }

        public DataTable GetProducts(string vSubCategoryId, string vCategoryId)
        {
            
            string query = "SELECT IsNull(p.[SKU], '') as [SKU], IsNull(p.[ItemName], '') as [ItemName],  " +
                            "Isnull(c.categoryId, '') as [CategoryId], " +
                            "Isnull(s.SubCategoryId, '') as [SubCategoryId], " +
                            "convert(varchar(50), Isnull(c.categoryId, '')) + '|' + convert(varchar(50), Isnull(s.SubCategoryId, '')) + '|' + convert(varchar(50), Isnull(p.[SKU], '')) as [value] " +
                            "FROM [tblProducts] p " +
                            "LEFT JOIN [rfcProductSubCategory] s " +
                            "ON p.SubCategoryId = s.SubCategoryId " +
                            "LEFT JOIN [rfcProductCategory] c " +
                            "ON c.categoryId = s.CategoryId ";

            string query2 = "SELECT IsNull(p.[SKU], '') as [SKU], IsNull(p.[SKU], '') + ' - ' + IsNull(p.[ItemName], '') as [ItemName], " +
                            "Isnull(c.categoryId, '') as [CategoryId], " +
                            "Isnull(s.SubCategoryId, '') as [SubCategoryId], " +
                            "convert(varchar(50), Isnull(c.categoryId, '')) + '|' + convert(varchar(50), Isnull(s.SubCategoryId, '')) + '|' + convert(varchar(50), Isnull(p.[SKU], '')) as [value] " +
                            "FROM [tblProducts] p " +
                            "LEFT JOIN [rfcProductSubCategory] s " +
                            "ON p.SubCategoryId = s.SubCategoryId " +
                            "LEFT JOIN [rfcProductCategory] c " +
                            "ON c.categoryId = s.CategoryId ";

            if ((vSubCategoryId != "") || (vCategoryId != ""))
            {
                query = query + "WHERE ";
                query2 = query2 + "WHERE ";
                
                if (vSubCategoryId != "")
                {
                    query = query + " s.[SubCategoryId] =" + vSubCategoryId;
                    query2 = query2 + " s.[SubCategoryId] =" + vSubCategoryId;
                }

                if (vCategoryId != "")
                {
                    if ((vSubCategoryId != ""))
                    {
                        query = query + " AND ";
                        query2 = query2 + " AND ";
                    }

                    query = query + " c.categoryId =" + vCategoryId;
                    query2 = query2 + " c.categoryId =" + vCategoryId;
                }
            }

            string uQuery = query + " UNION " + query2;
            uQuery = uQuery + " ORDER BY [ItemName]";

            
            DataSet subcategoryData = new DataSet();
            SqlParameter[] sqlparam = new SqlParameter[1];
            sqlparam[0] = new SqlParameter("@query", SqlDbType.NText);
            sqlparam[0].Value = uQuery;

            subcategoryData = SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "[s3p_EBid_GenericQueryProcedure]", sqlparam);
                    
            if (subcategoryData.Tables.Count > 0)
                return subcategoryData.Tables[0];
            else
            {
                DataTable dt = new DataTable();
                return dt;
            }
        }


        public DataTable GetDescAndUOM(string vSubCategoryId, string vCategoryId)
        {
            string query = "SELECT IsNull(p.[ItemName], '') + '|' + Isnull(ProductDescription, '') as [ItemNameAndDesc], " +
                            "IsNull(p.[SKU], '') + '|' + Isnull(UnitOfMeasure, '') as [SKUAndUOM] " +
                            "FROM [tblProducts] p " +
                            "LEFT JOIN [rfcProductSubCategory] s " +
                            "ON p.SubCategoryId = s.SubCategoryId " +
                            "LEFT JOIN [rfcProductCategory] c " +
                            "ON c.categoryId = s.CategoryId ";

            if ((vSubCategoryId != "") || (vCategoryId != ""))
            {
                query = query + "WHERE ";
                
                if (vSubCategoryId != "")
                {
                    query = query + " s.[SubCategoryId] =" + vSubCategoryId;
                }

                if (vCategoryId != "")
                {
                    if ((vSubCategoryId != ""))
                    {
                        query = query + " AND ";
                    }

                    query = query + " c.categoryId =" + vCategoryId;
                }
            }

             query = query  + " ORDER BY [ItemName]";


             DataSet subcategoryData = new DataSet();
             SqlParameter[] sqlparam = new SqlParameter[1];
             sqlparam[0] = new SqlParameter("@query", SqlDbType.NText);
             sqlparam[0].Value = query;
             subcategoryData = SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "[s3p_EBid_GenericQueryProcedure]", sqlparam);

            if (subcategoryData.Tables.Count > 0)
                return subcategoryData.Tables[0];
            else
            {
                DataTable dt = new DataTable();
                return dt;
            }
        }

        public DataTable QueryProductsViaName(string queryString)
        {                                   
            SqlParameter[] sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@query", SqlDbType.NVarChar);
            sqlParams[0].Value = queryString;
            
            return SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "s3p_EBid_SearchProductsViaName", sqlParams).Tables[0];
        }

        public int InsertProduct(string vSKU, string vItemName, string vProductDescription, string vUnitOfMeasure, string vCategory, string vSubCategory, string vBrand, string vServiceType, string connstring)
        {
            SqlParameter[] sqlParams = new SqlParameter[8];
            sqlParams[0] = new SqlParameter("@SKU", SqlDbType.NVarChar);
            sqlParams[0].Value = vSKU;
            sqlParams[1] = new SqlParameter("@ItemName", SqlDbType.VarChar);
            sqlParams[1].Value = vItemName;
            sqlParams[2] = new SqlParameter("@ProductDescription", SqlDbType.VarChar);
            sqlParams[2].Value = vProductDescription;
            sqlParams[3] = new SqlParameter("@UnitOfMeasurement", SqlDbType.NVarChar);
            sqlParams[3].Value = vUnitOfMeasure;
            sqlParams[4] = new SqlParameter("@SubCategoryId", SqlDbType.NVarChar);
            sqlParams[4].Value = vSubCategory;
            sqlParams[5] = new SqlParameter("@Service", SqlDbType.NVarChar);
            sqlParams[5].Value = vServiceType;
            sqlParams[6] = new SqlParameter("@Brand", SqlDbType.NVarChar);
            sqlParams[6].Value = vBrand;
            sqlParams[7] = new SqlParameter("@CategoryId", SqlDbType.NVarChar);
            sqlParams[7].Value = vCategory;
            
            int Count = Convert.ToInt32(SqlHelper.ExecuteScalar(connstring, CommandType.StoredProcedure, "s3p_EBid_InsertProducts", sqlParams));
            return Count;
        }

        public int UpdateProduct(string vSKU, string vItemName, string vProductDescription, string vUnitOfMeasure, string vCategory, string vSubCategory, string vBrand, string vServiceType, string connstring)
        {
            SqlParameter[] sqlParams = new SqlParameter[8];
            sqlParams[0] = new SqlParameter("@SKU", SqlDbType.NVarChar);
            sqlParams[0].Value = vSKU;
            sqlParams[1] = new SqlParameter("@ItemName", SqlDbType.VarChar);
            sqlParams[1].Value = vItemName;
            sqlParams[2] = new SqlParameter("@ProductDescription", SqlDbType.VarChar);
            sqlParams[2].Value = vProductDescription;
            sqlParams[3] = new SqlParameter("@UnitOfMeasure", SqlDbType.NVarChar);
            sqlParams[3].Value = vUnitOfMeasure;
            sqlParams[4] = new SqlParameter("@SubCategoryId", SqlDbType.NVarChar);
            sqlParams[4].Value = vSubCategory;
            sqlParams[5] = new SqlParameter("@Service", SqlDbType.NVarChar);
            sqlParams[5].Value = vServiceType;
            sqlParams[6] = new SqlParameter("@Brand", SqlDbType.NVarChar);
            sqlParams[6].Value = vBrand;
            sqlParams[7] = new SqlParameter("@CategoryId", SqlDbType.NVarChar);
            sqlParams[7].Value = vCategory;

            int Result = Convert.ToInt32(SqlHelper.ExecuteScalar(connstring, CommandType.StoredProcedure, "s3p_EBid_UpdateProductDetail", sqlParams));
            return Result;
        }


        public Category InsertCategory(string vCategoryId, string vCategoryName, string vCategoryDesc)
        {
            SqlParameter[] sqlParams = new SqlParameter[8];
            sqlParams[0] = new SqlParameter("@CategoryId", SqlDbType.NVarChar);
            sqlParams[0].Value = vCategoryId;
            sqlParams[0].Direction = ParameterDirection.Input;
            sqlParams[1] = new SqlParameter("@CategoryName", SqlDbType.VarChar);
            sqlParams[1].Value = vCategoryName;
            sqlParams[1].Direction = ParameterDirection.Input;
            sqlParams[2] = new SqlParameter("@CategoryDesc", SqlDbType.VarChar);
            sqlParams[2].Value = vCategoryDesc;
            sqlParams[2].Direction = ParameterDirection.Input;
            sqlParams[3] = new SqlParameter("@IdCount", SqlDbType.Int);
            sqlParams[3].Direction = ParameterDirection.Output;
            sqlParams[4] = new SqlParameter("@NameCount", SqlDbType.Int);
            sqlParams[4].Direction = ParameterDirection.Output;


            SqlHelper.ExecuteReader(connstring, CommandType.StoredProcedure, "s3p_EBid_InsertCategory", sqlParams);
            Category cat = new Category();
            cat.IdCount = sqlParams[3].Value.ToString().Trim();
            cat.NameCount = sqlParams[4].Value.ToString().Trim();
            return cat;
        }


        public int InsertSubCategory(string vCategoryId, string vSubCategoryName, string vSubCategoryDesc)
        {
            SqlParameter[] sqlParams = new SqlParameter[8];
            sqlParams[0] = new SqlParameter("@CategoryId", SqlDbType.NVarChar);
            sqlParams[0].Value = vCategoryId;
            sqlParams[1] = new SqlParameter("@SubCategoryName", SqlDbType.VarChar);
            sqlParams[1].Value = vSubCategoryName;
            sqlParams[2] = new SqlParameter("@SubCategoryDesc", SqlDbType.VarChar);
            sqlParams[2].Value = vSubCategoryDesc;
            int Count = Convert.ToInt32(SqlHelper.ExecuteScalar(connstring, CommandType.StoredProcedure, "s3p_EBid_InsertSubCategory", sqlParams));
            return Count;
        }

        public int UpdateProductStatus(string vSKU, int vStatus, string connstring)
        {
            SqlParameter[] sqlParams = new SqlParameter[2];
            sqlParams[0] = new SqlParameter("@Sku", SqlDbType.NVarChar);
            sqlParams[0].Value = vSKU;
            sqlParams[1] = new SqlParameter("@Status", SqlDbType.Int);
            sqlParams[1].Value = vStatus;

            int Result = Convert.ToInt32(SqlHelper.ExecuteScalar(connstring, CommandType.StoredProcedure, "sp_UpdateProductStatus", sqlParams));
            return Result;
        }
    }
}