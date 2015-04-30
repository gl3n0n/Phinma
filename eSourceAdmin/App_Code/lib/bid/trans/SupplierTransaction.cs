using System;
using System.Data;
using System.Data.OleDb;
using EBid.lib.utils;
using EBid.lib.constant;
using EBid.lib.bid.data;
using System.Configuration;
using System.Data.SqlClient;
using EBid.lib;
using System.Web;

namespace EBid.lib.bid.trans
{
	public class SupplierTransaction
	{
        private static string connstring = HttpContext.Current.Session["ConnectionString"].ToString();

		public string GetSuppliersByCategoryId(string vCategoryId, string vSubcategoryid)
		{
            DataSet dsQueryResult;
            DataTable dt = new DataTable();
            string suppliers = "";
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[3];
                sqlParams[0] = new SqlParameter("@CategoryId", SqlDbType.NVarChar);
                sqlParams[0].Value = vCategoryId;
                sqlParams[1] = new SqlParameter("@SubCategoryId", SqlDbType.NVarChar);
                sqlParams[1].Value = vSubcategoryid;
                sqlParams[2] = new SqlParameter("@Accredited", SqlDbType.NVarChar);
                sqlParams[2].Value = Convert.ToInt32(Constant.SUPPLIERTYPE.Accredited).ToString().Trim();
                dsQueryResult = SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "s3p_EBid_GetVendorsForACategoryAndSubCategory", sqlParams);
                if (dsQueryResult.Tables.Count > 0)
                    if (dsQueryResult.Tables[0] != null)
                    {
                        dt = dsQueryResult.Tables[0];
                        for (int i = 0; i < dt.Rows.Count; i++)
                            suppliers = ((suppliers == "") ? dt.Rows[i]["VendorId"].ToString().Trim() : suppliers.Trim() + ", " + dt.Rows[i]["VendorId"].ToString().Trim());
                    }
                
            }
            catch(Exception e)
            {
                string m = e.Message.ToString();
            }
            return suppliers;
		}


        public DataSet GetSuppliersByCategoryId_FilteredList(string vCategoryId, string vSubcategoryid, string vSelectedVendors)
		{

            DataSet dsQueryResult = new DataSet();
            
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[4];
                sqlParams[0] = new SqlParameter("@CategoryId", SqlDbType.NVarChar);
                sqlParams[0].Value = vCategoryId;
                sqlParams[1] = new SqlParameter("@SubCategoryId", SqlDbType.NVarChar);
                sqlParams[1].Value = vSubcategoryid;
                sqlParams[2] = new SqlParameter("@Accredited", SqlDbType.NVarChar);
                sqlParams[2].Value = Convert.ToInt32(Constant.SUPPLIERTYPE.Accredited).ToString().Trim();
                sqlParams[3] = new SqlParameter("@SelectedVendors", SqlDbType.NVarChar);
                sqlParams[3].Value = vSelectedVendors;
                dsQueryResult = SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "s3p_EBid_GetSelectedVendorsForACategoryAndSubCategory", sqlParams);
                
            }
            catch (Exception e)
            {
                string m = e.Message.ToString();
            }
            return dsQueryResult;
		}

        public DataTable GetSelectedSuppliers(string vSelectedVendors, string vOTSSuppliers)
        {
            DataTable dt = new DataTable();    
            try
            {
                    DataSet SupplierData = new DataSet();
                    string query = "";
                    if ((vSelectedVendors != "") && (vOTSSuppliers != ""))
                    {
                        query = "SELECT [VendorId], [VendorName] " +
                                    "FROM [tblVendors] " +
                                    "WHERE [VendorId] IN (" + vSelectedVendors + ") " +
                                    "UNION " +
                                    "SELECT [VendorId], 'OTS - ' + [VendorName] as [VendorName] " +
                                    "FROM [tblVendors] " +
                                    "WHERE [VendorId] IN (" + vOTSSuppliers + ")";
                    }
                    else
                    {
                        if (vSelectedVendors != "")
                        {
                            query = "SELECT [VendorId], [VendorName] " +
                                    "FROM [tblVendors] " +
                                    "WHERE [VendorId] IN (" + vSelectedVendors + ")";
                        }
                        if (vOTSSuppliers != "")
                        {
                            query = "SELECT [VendorId], 'OTS - ' + [VendorName] as [VendorName] " +
                                    "FROM [tblVendors] " +
                                    "WHERE [VendorId] IN (" + vOTSSuppliers + ")";
                        }

                    }

                    if (query != "")
                    {
                        SqlParameter[] sqlparam = new SqlParameter[1];
                        sqlparam[0] = new SqlParameter("@query", SqlDbType.NText);
                        sqlparam[0].Value = query;
                        SupplierData = SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "[s3p_EBid_GenericQueryProcedure]", sqlparam);
                        if (SupplierData.Tables.Count > 0)
                            return SupplierData.Tables[0];
                    }
            }
            catch
            {
            }
            return dt;
        }

        public string GetVendorNameById(string vVendorId)
        {
            SqlParameter[] sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@vendorId", SqlDbType.Int);
            sqlParams[0].Value = Int32.Parse(vVendorId);

            return SqlHelper.ExecuteScalar(connstring, CommandType.StoredProcedure, "s3p_EBid_GetVendorNameById", sqlParams).ToString().Trim();
        }

        public DataSet GetAllVendors()
        {
            return SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "s3p_Ebid_GetAllVendors");
        }

        public DataTable SearchVendors(string category, string subcategory, string ISOStandard, string PCABClass, string Brand, string Service, string Location, string Item)
        {
            string query = "SELECT DISTINCT v.[VendorId], v.[VendorName] " +
                "FROM [tblVendors] v ";

            //append tables
            if ((category != "") || (subcategory != ""))
            {
                query += " INNER JOIN [tblVendorCategoriesAndSubcategories] vcs " +
                         " ON vcs.VendorId = v.VendorId ";
            }
            if (Brand != "")   
            {
                query += " INNER JOIN [tblVendorBrands] vb " +
                         " ON vb.VendorId = v.VendorId ";
            }
             if (Service != "")
             {
                 query += " INNER JOIN [tblVendorServices] vs " +
                         " ON vs.VendorId = v.VendorId ";
             }
            if (Location != "")
            {
                query += " INNER JOIN [tblVendorLocation] vl " +
                         " ON vl.VendorId = v.VendorId ";
            }
            if (Item != "")
            {
                query += " INNER JOIN [tblVendorProdItems] vi " +
                         " ON vi.VendorId = v.VendorId ";
            }

            if ((category != "") || (subcategory != "") || (ISOStandard != "") || (PCABClass != "")||(Brand!="")||(Service!="")||(Location!="")||(Item!=""))
            {
                if ((category != "") || (subcategory != ""))
                {
                    if (category != "")
                        query += "WHERE ((vcs.CategoryId = '" + category + "')";
                    if (subcategory != "")
                    {
                        if (category != "")
                            query += " OR ";
                        else
                            query += " WHERE (";
                        query += " (vcs.SubCategoryId= " + subcategory + ")";
                    }
                }

                if (!(ISOStandard == ""))
                {
                    if ((category != "") || (subcategory != ""))
                        query += " OR ";
                    else
                        query += " WHERE (";
                    query += " (v.ISOStandard LIKE '" + ISOStandard + "')";
                }
                if (PCABClass != "")
                {
                    if ((category != "") || (subcategory != "") || (ISOStandard != ""))
                        query += " OR ";
                    else
                        query += " WHERE (";
                    query += " (v.PCABClass = " + PCABClass + ")";
                }
                if (Brand != "")
                {
                    if ((category != "") || (subcategory != "") || (ISOStandard != "") || (PCABClass !=""))
                        query += " OR ";
                    else
                        query += " WHERE (";
                    query += " (vb.BrandId = " + Brand + ")";
                }
                if (Service!= "")
                {
                    if ((category != "") || (subcategory != "") || (ISOStandard != "") || (PCABClass != "") || (Brand != ""))
                        query += " OR ";
                    else
                        query += " WHERE (";
                    query += " (vs.ServiceID = " + Service + ")";
                }

                if (Location != "")
                {
                    if ((category != "") || (subcategory != "") || (ISOStandard != "") || (PCABClass != "") || (Brand != "")||(Service != ""))
                        query += " OR ";
                    else
                        query += " WHERE (";
                    query += " (vl.LocationID = " + Location + ")";
                }
                if (Item != "")
                {
                    if ((category != "") || (subcategory != "") || (ISOStandard != "") || (PCABClass != "") || (Brand != "") || (Service != "") || (Location != ""))
                        query += " OR ";
                    else
                        query += " WHERE (";
                    query += " (vi.ProdItemNo = " + Item + ")";
                }
                query += ") AND ";
                
            }
            else
            {
                query += " WHERE ";
            }

            query +=  " (v.[Accredited]=" + Convert.ToInt32(Constant.SUPPLIERTYPE.Accredited).ToString() + ")";

            DataSet VendorData = new DataSet();
            SqlParameter[] sqlparam = new SqlParameter[1];
            sqlparam[0] = new SqlParameter("@query", SqlDbType.NText);
            sqlparam[0].Value = query;
            VendorData = SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "[s3p_EBid_GenericQueryProcedure]", sqlparam);
            DataTable dt = new DataTable();
            
            if (VendorData.Tables.Count > 0)
                dt = VendorData.Tables[0];

            return dt;
        }


        public DataTable GetOneTimeSuppliers(string vCategoryId)
        {
            SqlParameter[] sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@supplierType", SqlDbType.Int);
            sqlParams[0].Value = (int)Constant.SUPPLIERTYPE.OneTimeSupplier;

            return SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "s3p_EBid_GetOneTimeSuppliers", sqlParams).Tables[0];
        }

        public Supplier GetVendorDetailsByVendorId(string vVendorId)
        {
            Supplier supplier = new Supplier();

            if (vVendorId != "")
            {
                SqlParameter[] sqlParams = new SqlParameter[1];
                sqlParams[0] = new SqlParameter("@vendorId", SqlDbType.Int);
                sqlParams[0].Value = Int32.Parse(vVendorId);

                DataSet VendorData = SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "s3p_EBid_GetVendorDetailsByVendorId", sqlParams);

                if (VendorData.Tables[0].Rows.Count > 0)
                {
                    supplier.VendorAddress = VendorData.Tables[0].Rows[0]["VendorAddress"].ToString().Trim();
                    supplier.VendorAddress1 = VendorData.Tables[0].Rows[0]["VendorAddress1"].ToString().Trim();
                    supplier.VendorAddress2 = VendorData.Tables[0].Rows[0]["VendorAddress2"].ToString().Trim();
                    supplier.VendorAddress3 = VendorData.Tables[0].Rows[0]["VendorAddress3"].ToString().Trim();
                    supplier.ContactPerson = VendorData.Tables[0].Rows[0]["ContactPerson"].ToString().Trim();
                    supplier.TelephoneNumber = VendorData.Tables[0].Rows[0]["TelephoneNo"].ToString().Trim();
                    supplier.VendorEmail = VendorData.Tables[0].Rows[0]["VendorEmail"].ToString().Trim();
                }
                else
                {
                    supplier.VendorAddress = "";
                    supplier.VendorAddress1 = "";
                    supplier.VendorAddress2 = "";
                    supplier.VendorAddress3 = "";
                    supplier.ContactPerson = "";
                    supplier.TelephoneNumber = "";
                    supplier.VendorEmail = "";
                }
                
            }
            else
            {
                supplier.VendorAddress = "";
                supplier.VendorAddress1 = "";
                supplier.VendorAddress2 = "";
                supplier.VendorAddress3 = "";
                supplier.ContactPerson = "";
                supplier.TelephoneNumber = "";
                supplier.VendorEmail = "";
            }
            return supplier;
        }


        public bool VendorExists(string vVendorName)
        {
            SqlParameter[] sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@vendorName", SqlDbType.NVarChar);
            sqlParams[0].Value = vVendorName;

            string strResult = SqlHelper.ExecuteScalar(connstring, CommandType.StoredProcedure, "s3p_EBid_VendorExists", sqlParams).ToString().Trim();

            if (Int32.Parse(strResult) == 0)
                return false;
            else
                return true;
        }

        public string InsertOneTimeSupplier(string vVendorId, 
                            string vVendorName,
                                string vAccredited,
                                string vVendorEmail,
                                string vVendorAddress,
                                string vVendorAddress1,
                                string vContactPerson,
                                string vTelephoneNo)
        {
            
                        SqlParameter[] sqlparam = new SqlParameter[9];
                        sqlparam[0] = new SqlParameter("@VendorId", SqlDbType.Int);
                        sqlparam[0].Value=vVendorId;
                        sqlparam[1] = new SqlParameter("@VendorName ", SqlDbType.VarChar);
                        sqlparam[1].Value=vVendorName;
                        sqlparam[2] = new SqlParameter("@Accredited ", SqlDbType.Int);
                        sqlparam[2].Value=vAccredited;
                        sqlparam[3] = new SqlParameter("@VendorEmail", SqlDbType.VarChar);
                        sqlparam[3].Value=vVendorEmail;
                        sqlparam[4] = new SqlParameter("@VendorAddress", SqlDbType.VarChar);
                        sqlparam[4].Value=vVendorAddress;
                        sqlparam[5] = new SqlParameter("@VendorAddress1", SqlDbType.VarChar);
                        sqlparam[5].Value=vVendorAddress1;
                        sqlparam[6] = new SqlParameter("@ContactPerson", SqlDbType.VarChar);
                        sqlparam[6].Value=vContactPerson;
                        sqlparam[7] = new SqlParameter("@TelephoneNo", SqlDbType.VarChar);
                        sqlparam[7].Value=vTelephoneNo;
                        sqlparam[8] = new SqlParameter("@RetVal", SqlDbType.Int);
                        sqlparam[8].Direction = ParameterDirection.Output;
                        SqlHelper.ExecuteNonQuery(connstring, CommandType.StoredProcedure, "[s3p_EBid_InsertOneTimeSupplier]", sqlparam);
                        return sqlparam[8].Value.ToString().Trim();
        }




        public int RegisterSupplier(
                string vVendorId,
                string vVendorName, 
                string vAccredited, 
                string vVendorEmail, 
                string vVendorAddress, 
                string vVendorAddress1, 
                string vVendorAddress2, 
                string vVendorAddress3,
                string vContactPerson, 
                string vSalesPersonTelNo,
                string vSyskey, 
                string vTelephoneNo, 
                string vFax, 
                string vExtension, 
                string vBranchTelephoneNo, 
                string vBranchFax, 
                string vBranchExtension, 
                string vVatRegNo, 
                string vTIN, 
                string vPOBox, 
                string vTermsofPayment, 
                string vSpecialTerms, 
                string vMinOrderValue, 
                string vPostalCode, 
                string vOwnershipFilipino, 
                string vOwnershipOther, 
                string vOrgTypeID, 
                string vSpecialization, 
                string vSoleSupplier1, 
                string vSoleSupplier2, 
                string vKeyPersonnel, 
                string vKpPosition, 
                string vISOStandard, 
                string vPCABClass)
        {
            SqlParameter[] sqlParams = new SqlParameter[34];
            sqlParams[0] = new SqlParameter("@VendorId", SqlDbType.Int);
            sqlParams[0].Value = vVendorId;
            sqlParams[1] = new SqlParameter("@VendorName", SqlDbType.VarChar);
            sqlParams[1].Value = vVendorName;
            sqlParams[2] = new SqlParameter("@Accredited", SqlDbType.Int);
            sqlParams[2].Value = vAccredited;
            sqlParams[3] = new SqlParameter("@VendorEmail", SqlDbType.VarChar);
            sqlParams[3].Value = vVendorEmail;
            sqlParams[4] = new SqlParameter("@VendorAddress", SqlDbType.VarChar);
            sqlParams[4].Value = vVendorAddress;
            sqlParams[5] = new SqlParameter("@VendorAddress1", SqlDbType.VarChar);
            sqlParams[5].Value = vVendorAddress1;
            sqlParams[6] = new SqlParameter("@VendorAddress2", SqlDbType.VarChar);
            sqlParams[6].Value = vVendorAddress2;
            sqlParams[7] = new SqlParameter("@VendorAddress3", SqlDbType.VarChar);
            sqlParams[7].Value = vVendorAddress3;
            sqlParams[8] = new SqlParameter("@ContactPerson", SqlDbType.VarChar);
            sqlParams[8].Value = vContactPerson;
            sqlParams[9] = new SqlParameter("@SalesPersonTelNo", SqlDbType.VarChar);
            sqlParams[9].Value = vSalesPersonTelNo;
            sqlParams[10] = new SqlParameter("@Syskey", SqlDbType.VarChar);
            sqlParams[10].Value = vSyskey;
            sqlParams[11] = new SqlParameter("@TelephoneNo", SqlDbType.VarChar);
            sqlParams[11].Value = vTelephoneNo;
            sqlParams[12] = new SqlParameter("@Fax", SqlDbType.VarChar);
            sqlParams[12].Value = vFax;
            sqlParams[13] = new SqlParameter("@Extension", SqlDbType.VarChar);
            sqlParams[13].Value = vExtension;
            sqlParams[14] = new SqlParameter("@BranchTelephoneNo", SqlDbType.VarChar);
            sqlParams[14].Value = vBranchTelephoneNo;
            sqlParams[15] = new SqlParameter("@BranchFax", SqlDbType.VarChar);
            sqlParams[15].Value = vBranchFax;
            sqlParams[16] = new SqlParameter("@BranchExtension", SqlDbType.VarChar);
            sqlParams[16].Value = vBranchExtension;
            sqlParams[17] = new SqlParameter("@VatRegNo", SqlDbType.VarChar);
            sqlParams[17].Value = vVatRegNo;
            sqlParams[18] = new SqlParameter("@TIN", SqlDbType.VarChar);
            sqlParams[18].Value = vTIN;
            sqlParams[19] = new SqlParameter("@POBox", SqlDbType.VarChar);
            sqlParams[19].Value = vPOBox;
            sqlParams[20] = new SqlParameter("@TermsofPayment", SqlDbType.VarChar);
            sqlParams[20].Value = vTermsofPayment;
            sqlParams[21] = new SqlParameter("@SpecialTerms", SqlDbType.VarChar);
            sqlParams[21].Value = vSpecialTerms;
            sqlParams[22] = new SqlParameter("@MinOrderValue", SqlDbType.Float);
            sqlParams[22].Value = vMinOrderValue;
            sqlParams[23] = new SqlParameter("@PostalCode", SqlDbType.VarChar);
            sqlParams[23].Value = vPostalCode;
            sqlParams[24] = new SqlParameter("@OwnershipFilipino", SqlDbType.Int);
            sqlParams[24].Value = vOwnershipFilipino;
            sqlParams[25] = new SqlParameter("@OwnershipOther", SqlDbType.Int);
            sqlParams[25].Value = vOwnershipOther;
            sqlParams[26] = new SqlParameter("@OrgTypeID", SqlDbType.Int);
            sqlParams[26].Value = vOrgTypeID;
            sqlParams[27] = new SqlParameter("@Specialization", SqlDbType.VarChar);
            sqlParams[27].Value = vSpecialization;
            sqlParams[28] = new SqlParameter("@SoleSupplier1", SqlDbType.VarChar);
            sqlParams[28].Value = vSoleSupplier1;
            sqlParams[29] = new SqlParameter("@SoleSupplier2", SqlDbType.VarChar);
            sqlParams[29].Value = vSoleSupplier2;
            sqlParams[30] = new SqlParameter("@KeyPersonnel", SqlDbType.VarChar);
            sqlParams[30].Value = vKeyPersonnel;
            sqlParams[31] = new SqlParameter("@KpPosition", SqlDbType.VarChar);
            sqlParams[31].Value = vKpPosition;
            sqlParams[32] = new SqlParameter("@ISOStandard", SqlDbType.VarChar);
            sqlParams[32].Value = vISOStandard;
            sqlParams[33] = new SqlParameter("@PCABClass", SqlDbType.Int);
            sqlParams[33].Value = vPCABClass;
            int result = Convert.ToInt32(SqlHelper.ExecuteScalar(connstring, CommandType.StoredProcedure, "[s3p_EBid_RegisterVendor]", sqlParams));
            return result;
        }

        public bool InsertVendorClassification(string vendorid, string[] vendorclassifiction)
        {
            bool completed = false;
            int lastindex = 0;
            while (completed == false)
            {
                string strSQL = CreateVendorClassification(vendorclassifiction, vendorid, ref lastindex, ref completed);
                //execute strSQL; retval should be 0; no error
                string retVal = "0";
                if (strSQL != "")
                    retVal = GenericInsert(strSQL);
                if (Int32.Parse(retVal) > 0)
                    return false;
            }
            return true;
        }

       

        public bool InsertSubCategory(string vendorid, string[] subcategories)
        {
            bool completed = false;
            int lastindex = 0;
            while (completed == false)
            {
                string strSQL = CreateSubCategories(subcategories, vendorid, ref lastindex, ref completed);
                //execute strSQL; retval should be 0; no error
                string retVal = "0";
                if (strSQL != "")
                    retVal = GenericInsert(strSQL);
                if (Int32.Parse(retVal) > 0)
                    return false;
            }
            return true;
        }

        public bool InsertCategory(string vendorid, string[] categories)
    {
            bool completed = false;
            int lastindex = 0;
            while (completed == false)
            {
                string strSQL = CreateCategories(categories, vendorid, ref lastindex, ref completed);
                //execute strSQL; retval should be 0; no error
                string retVal = "0";
                if (strSQL != "")
                    retVal = GenericInsert(strSQL);
                if (Int32.Parse(retVal) > 0)
                    return false;
            }
            return true;
        }

 


        public bool InsertPresentServices(string vendorid, DataTable dtPresentServices)
        {
            bool completed = false;
            int lastindex = 0;
            while (completed == false)
            {
                string strSQL = CreateInsertPresentServicesStmt(dtPresentServices, vendorid, ref lastindex, ref completed);
                //execute strSQL; retval should be 0; no error
                 string retVal="0";
                if (strSQL !="")
                    retVal = GenericInsert(strSQL);
                if (Int32.Parse(retVal) > 0)
                    return false;
            }
            return true;
        }

        private string CreateVendorClassification(string[] AVendorClassifiction, string vendorid, ref int lastindex, ref bool completed)
        {
            string strSQL = "";
            OtherTransaction oth = new OtherTransaction();

            for (int i = lastindex; i < AVendorClassifiction.Length; i++)
            {

                if (AVendorClassifiction[i]!= null)
                    if (AVendorClassifiction[i].ToString().Trim() != "")
                        {
                            string newStrSQL = "";
                            if (strSQL == "")
                            {
                                strSQL = "INSERT INTO [tblVendorClassification]([VendorId], [ClassificationId]) " +
                                        "VALUES(" + vendorid + ", " + AVendorClassifiction[i].ToString().Trim() + ")";
                            }
                            else
                            {
                                newStrSQL = "INSERT INTO [tblVendorClassification]([VendorId], [ClassificationId]) " +
                                            "VALUES(" + vendorid + ", " + AVendorClassifiction[i].ToString().Trim() + ")";
                                if (strSQL.Length + newStrSQL.Length < 8000)
                                    strSQL = strSQL + " " + newStrSQL;
                                else
                                {
                                    lastindex = i;
                                    completed = false;
                                    return strSQL;
                                }
                            }
                        }
            }
            completed = true;
            return strSQL;
        }

        private string GetCategoryOfASubCategory(string vSubCategoryId)
        {
            SqlParameter[] sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@SubCategoryId", SqlDbType.Int);
            sqlParams[0].Value = vSubCategoryId;
            return Convert.ToString(SqlHelper.ExecuteScalar(connstring, CommandType.StoredProcedure, "s3p_EBid_GetCategoryIdOfASubCategory", sqlParams)).Trim();
        }

        private string CreateSubCategories(string[] ASubCategories, string vendorid, ref int lastindex, ref bool completed)
        {
            string strSQL = "";
            OtherTransaction oth = new OtherTransaction();

            for (int i = lastindex; i < ASubCategories.Length; i++)
            {

                if (ASubCategories[i] != null)
                    if (ASubCategories[i].ToString().Trim() != "")
                    {
                        string category = GetCategoryOfASubCategory(ASubCategories[i].ToString().Trim());
                        string newStrSQL = "";
                        if (strSQL == "")
                        {
                            strSQL = "INSERT INTO [tblVendorCategoriesAndSubcategories]([VendorId], [CategoryId], [IncludesAllSubCategories], [SubCategoryId]) " +
                                    "VALUES(" + vendorid + ", '" + category + "'," + Constant.FALSE.ToString().Trim() + "," + ASubCategories[i].ToString().Trim() + ")";
                        }
                        else
                        {
                            newStrSQL = "INSERT INTO [tblVendorCategoriesAndSubcategories]([VendorId], [CategoryId], [IncludesAllSubCategories], [SubCategoryId]) " +
                                        "VALUES(" + vendorid + ", '" + category + "'," + Constant.FALSE.ToString().Trim() + "," + ASubCategories[i].ToString().Trim() + ")";
                            if (strSQL.Length + newStrSQL.Length < 8000)
                                strSQL = strSQL + " " + newStrSQL;
                            else
                            {
                                lastindex = i;
                                completed = false;
                                return strSQL;
                            }
                        }
                    }
            }
            completed = true;
            return strSQL;
        }

        private bool CategoryHasNotBeenInserted(string vendorId, string categoryId)
        {

            SqlParameter[] sqlParams = new SqlParameter[2];
            sqlParams[0] = new SqlParameter("@VendorId", SqlDbType.Int);
            sqlParams[0].Value = vendorId;
            sqlParams[1] = new SqlParameter("@CategoryId", SqlDbType.NVarChar);
            sqlParams[1].Value = categoryId;
            int count = Convert.ToInt32(SqlHelper.ExecuteScalar(connstring, CommandType.StoredProcedure, "s3p_EBid_CheckCategory", sqlParams));
            if (count == 0)
                return true;
            else
                return false;

        }

        private string CreateCategories(string[] ACategories, string vendorid, ref int lastindex, ref bool completed)
        {
            string strSQL = "";
            OtherTransaction oth = new OtherTransaction();

            for (int i = lastindex; i < ACategories.Length; i++)
            {
                
                    if (ACategories[i] != null)
                        if (ACategories[i].ToString().Trim() != "")
                        {
                            if (CategoryHasNotBeenInserted(vendorid, ACategories[i].ToString().Trim()))
                            {
                                string newStrSQL = "";
                                if (strSQL == "")
                                {
                                    strSQL = "INSERT INTO [tblVendorCategoriesAndSubcategories]([VendorId], [CategoryId], [IncludesAllSubCategories], [SubCategoryId]) " +
                                            "VALUES(" + vendorid + ", '" + ACategories[i].ToString().Trim() + "'," + Constant.TRUE.ToString().Trim() + ", null)";
                                }
                                else
                                {
                                    newStrSQL = "INSERT INTO [tblVendorCategoriesAndSubcategories]([VendorId], [CategoryId], [IncludesAllSubCategories], [SubCategoryId]) " +
                                                "VALUES(" + vendorid + ", '" + ACategories[i].ToString().Trim() + "'," + Constant.TRUE.ToString().Trim() + ", null)";
                                    if (strSQL.Length + newStrSQL.Length < 8000)
                                        strSQL = strSQL + " " + newStrSQL;
                                    else
                                    {
                                        lastindex = i;
                                        completed = false;
                                        return strSQL;
                                    }
                                }
                            }
                        }
                
            }
            completed = true;
            return strSQL;
        }

        private string CreateInsertPresentServicesStmt(DataTable dtPresentServices, string vendorid, ref int lastindex, ref bool completed)
        {
            string strSQL = "";
            OtherTransaction oth = new OtherTransaction();

            for (int i = lastindex; i < dtPresentServices.Rows.Count; i++)
            {

                if (dtPresentServices.Rows[i]["Plan"].ToString().Trim() != "")
                        {
                            string newStrSQL = "";
                            if (strSQL == "")
                            {
                                strSQL = "INSERT INTO [tblPresentServices]([VendorID], [PlanID], [AccountNo], [CreditLimit]) " +
                                        "VALUES(" + vendorid + ", " + dtPresentServices.Rows[i]["Plan"].ToString().Trim() + ", '" + oth.Replace(dtPresentServices.Rows[i]["AcctNo"].ToString().Trim()) + "', '" + oth.Replace(dtPresentServices.Rows[i]["CreditLimit"].ToString().Trim()) + "')";
                            }
                            else
                            {
                                newStrSQL = "INSERT INTO [tblPresentServices]([VendorID], [PlanID], [AccountNo], [CreditLimit]) " +
                                            "VALUES(" + vendorid + ", " + dtPresentServices.Rows[i]["Plan"].ToString().Trim() + ", '" + oth.Replace(dtPresentServices.Rows[i]["AcctNo"].ToString().Trim()) + "' , '" + oth.Replace(dtPresentServices.Rows[i]["CreditLimit"].ToString().Trim()) + "')";
                                if (strSQL.Length + newStrSQL.Length < 8000)
                                    strSQL = strSQL + " " + newStrSQL;
                                else
                                {
                                    lastindex = i;
                                    completed = false;
                                    return strSQL;
                                }
                            }
                        }
            }
            completed = true;
            return strSQL;
        }
        //begin---
        public bool InsertMajorCustomers(string vendorid, DataTable dtMajorCustomers)
        {
            bool completed = false;
            int lastindex = 0;
            while (completed == false)
            {
                string strSQL = CreateInsertMajorCustomerStmt(dtMajorCustomers, vendorid, ref lastindex, ref completed);
                //execute strSQL; retval should be 0; no error
                string retVal = "0";
                if (strSQL != "")
                    retVal = GenericInsert(strSQL);
                if (Int32.Parse(retVal) > 0)
                    return false;
            }
            return true;
        }

        private string CreateInsertMajorCustomerStmt(DataTable dtMajorCustomers,  string vendorid, ref int lastindex, ref bool completed)
        {
            string strSQL = "";
            OtherTransaction o = new OtherTransaction();

            for (int i = lastindex; i < dtMajorCustomers.Rows.Count ; i++)
            {
                if (dtMajorCustomers.Rows[i]["Customer"].ToString().Trim() != "")
                        {
                            string newStrSQL = "";
                            if (strSQL == "")
                            {
                                strSQL = "INSERT INTO [tblVendorReferences]([VendorID], [CompanyName], [AveMonthlySales], [ReferenceType]) " +
                                        "VALUES(" + vendorid + ", '" + o.Replace(dtMajorCustomers.Rows[i]["Customer"].ToString().Trim()) + "', '" + o.Replace(dtMajorCustomers.Rows[i]["Sale"].ToString().Trim()) + "', " + Constant.REF_MAIN_CUSTOMERS.ToString().Trim() + ")";
                            }
                            else
                            {
                                newStrSQL = "INSERT INTO [tblVendorReferences]([VendorID], [CompanyName], [AveMonthlySales], [ReferenceType]) " +
                                        "VALUES(" + vendorid + ", '" + o.Replace(dtMajorCustomers.Rows[i]["Customer"].ToString().Trim()) + "', '" + o.Replace(dtMajorCustomers.Rows[i]["Sale"].ToString().Trim()) + "', " + Constant.REF_MAIN_CUSTOMERS.ToString().Trim() + ")";
                                if (strSQL.Length + newStrSQL.Length < 8000)
                                    strSQL = strSQL + " " + newStrSQL;
                                else
                                {
                                    lastindex = i;
                                    completed = false;
                                    return strSQL;
                                }
                            }
                        }
            }
            completed = true;
            return strSQL;
        }

        public bool InsertBanks(string vendorid, DataTable dtBanks)
        {
            
            bool completed = false;
            int lastindex = 0;
            while (completed == false)
            {
                string strSQL = CreateInsertBanksStmt(dtBanks, vendorid, ref lastindex, ref completed);
                //execute strSQL; retval should be 0; no error
                string retVal = "0";
                if (strSQL !="")
                    retVal = GenericInsert(strSQL);
                if (Int32.Parse(retVal) > 0)
                    return false;
            }
            return true;
        }

        private string CreateInsertBanksStmt(DataTable dtBanks, string vendorid, ref int lastindex, ref bool completed)
        {
            string strSQL = "";
            OtherTransaction o = new OtherTransaction();

            for (int i = lastindex; i < dtBanks.Rows.Count; i++)
            {
                if (dtBanks.Rows[i]["Bank"].ToString().Trim() != null)
                {
                    string newStrSQL = "";
                    if (strSQL == "")
                    {
                        strSQL = "INSERT INTO [tblVendorReferences]([VendorID], [CompanyName], [CreditLine], [ReferenceType]) " +
                                "VALUES(" + vendorid + ", '" + o.Replace(dtBanks.Rows[i]["Bank"].ToString().Trim()) + "', '" + o.Replace(dtBanks.Rows[i]["CreditLine"].ToString().Trim()) + "', " + Constant.REF_BANKS.ToString().Trim() + ")";
                    }
                    else
                    {
                        newStrSQL = "INSERT INTO [tblVendorReferences]([VendorID], [CompanyName], [CreditLine], [ReferenceType]) " +
                                "VALUES(" + vendorid + ", '" + o.Replace(dtBanks.Rows[i]["Bank"].ToString().Trim()) + "', '" + o.Replace(dtBanks.Rows[i]["CreditLine"].ToString().Trim()) + "', " + Constant.REF_BANKS.ToString().Trim() + ")";
                        if (strSQL.Length + newStrSQL.Length < 8000)
                            strSQL = strSQL + " " + newStrSQL;
                        else
                        {
                            lastindex = i;
                            completed = false;
                            return strSQL;
                        }
                    }
                }
            }
            completed = true;
            return strSQL;
        }

        public bool InsertAffiliatedCompanies(string vendorid, DataTable dtAffiliatedCompanies)
        {
            bool completed = false;
            int lastindex = 0;
            while (completed == false)
            {
                string strSQL = CreateAffiliatedCompaniesStmt(dtAffiliatedCompanies, vendorid, ref lastindex, ref completed);
                //execute strSQL; retval should be 0; no error
                string retVal = "0";
                 if (strSQL !="")
                     retVal=GenericInsert(strSQL);
                if (Int32.Parse(retVal) > 0)
                    return false;
            }
            return true;
        }

        private string CreateAffiliatedCompaniesStmt(DataTable dtAffiliatedCompanies, string vendorid, ref int lastindex, ref bool completed)
        {
            string strSQL = "";
            OtherTransaction o = new OtherTransaction();
            for (int i = lastindex; i < dtAffiliatedCompanies.Rows.Count; i++)
            {

                if (dtAffiliatedCompanies.Rows[i]["Company"].ToString().Trim() != "")
                {
                    string newStrSQL = "";
                    if (strSQL == "")
                    {
                        strSQL = "INSERT INTO [tblVendorReferences]([VendorID], [CompanyName], [KindOfBusiness],[ReferenceType]) " +
                                "VALUES(" + vendorid + ", '" + o.Replace(dtAffiliatedCompanies.Rows[i]["Company"].ToString().Trim()) + "', '" + o.Replace(dtAffiliatedCompanies.Rows[i]["Business"].ToString().Trim()) + "', " + Constant.REF_AFFILIATE.ToString().Trim() + ")";
                    }
                    else
                    {
                        newStrSQL = "INSERT INTO [tblVendorReferences]([VendorID], [CompanyName], [KindOfBusiness], [ReferenceType]) " +
                                "VALUES(" + vendorid + ", '" + o.Replace(dtAffiliatedCompanies.Rows[i]["Company"].ToString().Trim()) + "', '" + o.Replace(dtAffiliatedCompanies.Rows[i]["Business"].ToString().Trim()) + "', " + Constant.REF_AFFILIATE.ToString().Trim() + ")";
                        if (strSQL.Length + newStrSQL.Length < 8000)
                            strSQL = strSQL + " " + newStrSQL;
                        else
                        {
                            lastindex = i;
                            completed = false;
                            return strSQL;
                        }
                    }
                }
            }
            completed = true;
            return strSQL;
        }

        public bool InsertExternalAuditors(string vendorid, DataTable dtExternalAuditors)
        {
            
            bool completed = false;
            int lastindex = 0;
            while (completed == false)
            {
                string strSQL = CreateExternalAuditorsStmt(dtExternalAuditors, vendorid, ref lastindex, ref completed);
                //execute strSQL; retval should be 0; no error
                string retVal = "0";
                if (strSQL!="")
                    retVal = GenericInsert(strSQL);
                if (Int32.Parse(retVal) > 0)
                    return false;
            }
            return true;
        }

        private string CreateExternalAuditorsStmt(DataTable dtExternalAuditors, string vendorid, ref int lastindex, ref bool completed)
        {
            string strSQL = "";
            OtherTransaction o = new OtherTransaction();

            for (int i = lastindex; i < dtExternalAuditors.Rows.Count; i++)
            {
                if (dtExternalAuditors.Rows[i]["Auditor"].ToString().Trim() != "")
                {
                    string newStrSQL = "";
                    if (strSQL == "")
                    {
                        strSQL = "INSERT INTO [tblVendorReferences]([VendorID], [CompanyName], [LegalCounsel], [ReferenceType]) " +
                                "VALUES(" + vendorid + ", '" + o.Replace(dtExternalAuditors.Rows[i]["Auditor"].ToString().Trim()) + "', '" + o.Replace(dtExternalAuditors.Rows[i]["Counsel"].ToString().Trim()) + "', " + Constant.REF_EXTERN_AUDITOR.ToString().Trim() + ")";
                    }
                    else
                    {
                        newStrSQL = "INSERT INTO [tblVendorReferences]([VendorID], [CompanyName], [LegalCounsel], [ReferenceType]) " +
                                "VALUES(" + vendorid + ", '" + o.Replace(dtExternalAuditors.Rows[i]["Auditor"].ToString().Trim()) + "', '" + o.Replace(dtExternalAuditors.Rows[i]["Counsel"].ToString().Trim()) + "', " + Constant.REF_EXTERN_AUDITOR.ToString().Trim() + ")";
                        if (strSQL.Length + newStrSQL.Length < 8000)
                            strSQL = strSQL + " " + newStrSQL;
                        else
                        {
                            lastindex = i;
                            completed = false;
                            return strSQL;
                        }
                    }
                }
            }
            completed = true;
            return strSQL;
        }

        public bool InsertEquipment(string vendorid, DataTable dtEquipment)
        {
            
            bool completed = false;
            int lastindex = 0;
            while (completed == false)
            {
                string strSQL = CreateEquipmentStmt(dtEquipment, vendorid, ref lastindex, ref completed);
                //execute strSQL; retval should be 0; no error
                string retVal = "0";
                if (strSQL != "")
                    retVal = GenericInsert(strSQL);
                if (Int32.Parse(retVal) > 0)
                    return false;
            }
            return true;
        }

        private string CreateEquipmentStmt(DataTable dtEquipment, string vendorid, ref int lastindex, ref bool completed)
        {
            string strSQL = "";
            OtherTransaction o = new OtherTransaction();
            for (int i = lastindex; i < dtEquipment.Rows.Count; i++)
            {
                if (dtEquipment.Rows[i]["Type"].ToString().Trim() != "")
                {
                    string newStrSQL = "";
                    if (strSQL == "")
                    {
                        strSQL = "INSERT INTO [tblVendorEquipments]([VendorID], [EquipmentType], [Units], [Remarks]) " +
                                "VALUES(" + vendorid + ", '" + o.Replace(dtEquipment.Rows[i]["Type"].ToString().Trim()) + "', " + dtEquipment.Rows[i]["Unit"].ToString().Trim() + ", '" + o.Replace(dtEquipment.Rows[i]["Remarks"].ToString().Trim()) + "')";
                    }
                    else
                    {
                        newStrSQL = "INSERT INTO [tblVendorEquipments]([VendorID], [EquipmentType], [Units], [Remarks]) " +
                                "VALUES(" + vendorid + ", '" + o.Replace(dtEquipment.Rows[i]["Type"].ToString().Trim()) + "', " + dtEquipment.Rows[i]["Unit"].ToString().Trim() + ", '" + o.Replace(dtEquipment.Rows[i]["Remarks"].ToString().Trim()) + "')";
                        if (strSQL.Length + newStrSQL.Length < 8000)
                            strSQL = strSQL + " " + newStrSQL;
                        else
                        {
                            lastindex = i;
                            completed = false;
                            return strSQL;
                        }
                    }
                }
            }
            completed = true;
            return strSQL;
        }

        public bool InsertRelative(string vendorid, DataTable dtRelative)
        {
            
            bool completed = false;
            int lastindex = 0;
            while (completed == false)
            {
                string strSQL = CreateRelativeStmt(dtRelative, vendorid, ref lastindex, ref completed);
                //execute strSQL; retval should be 0; no error
                string retVal = "0";
                if (strSQL != "")
                    retVal = GenericInsert(strSQL);
                if (Int32.Parse(retVal) > 0)
                    return false;
            }
            return true;
        }

        private string CreateRelativeStmt(DataTable dtRelatives, string vendorid, ref int lastindex, ref bool completed)
        {
            string strSQL = "";
            OtherTransaction o = new OtherTransaction();

            for (int i = lastindex; i < dtRelatives.Rows.Count; i++)
            {
                if (dtRelatives.Rows[i]["Name"].ToString().Trim() != "")
                {
                    string newStrSQL = "";
                    if (strSQL == "")
                    {
                        strSQL = "INSERT INTO [tblVendorRelative]([VendorID], [VendorRelative], [Title], [Relationship]) " +
                                "VALUES(" + vendorid + ", '" + o.Replace(dtRelatives.Rows[i]["Name"].ToString().Trim()) + "', '" + o.Replace(dtRelatives.Rows[i]["TitlePosition"].ToString().Trim()) + "', '" + o.Replace(dtRelatives.Rows[i]["Relationship"].ToString().Trim()) + "')";
                    }
                    else
                    {
                        newStrSQL = "INSERT INTO [tblVendorRelative]([VendorID], [VendorRelative], [Title], [Relationship]) " +
                                "VALUES(" + vendorid + ", '" + o.Replace(dtRelatives.Rows[i]["Name"].ToString().Trim()) + "', '" + o.Replace(dtRelatives.Rows[i]["TitlePosition"].ToString().Trim()) + "', '" + o.Replace(dtRelatives.Rows[i]["Relationship"].ToString().Trim()) + "')";
                        if (strSQL.Length + newStrSQL.Length < 8000)
                            strSQL = strSQL + " " + newStrSQL;
                        else
                        {
                            lastindex = i;
                            completed = false;
                            return strSQL;
                        }
                    }
                }
            }
            completed = true;
            return strSQL;
        }
        //end---
        public bool InsertBrandsMain(string vendorid, string brandids)
        {
            string[] brandidA = brandids.Split(Convert.ToChar(","));
            bool completed = false;
            int lastindex = 0;
            while (completed == false)
            {
                string strSQL = CreateInsertBrandStatement(brandidA, vendorid, ref lastindex, ref completed);
                //execute strSQL; retval should be 0; no error
                string retVal = GenericInsert(strSQL);
                if (Int32.Parse(retVal) > 0)
                    return false;
            }
            return true;
        }

        private string CreateInsertBrandStatement(string[] brandidA, string vendorid, ref int lastindex, ref bool completed)
        {
            string strSQL = "";

            for (int i = lastindex; i < brandidA.Length; i++)
            {
                if (brandidA[i] != null)
                {
                    string newStrSQL = "";
                    if (strSQL == "")
                    {
                        strSQL = "INSERT INTO [tblVendorBrands]([VendorID], [BrandId]) " +
                                    "VALUES(" + vendorid + ", " + brandidA[i].ToString().Trim() + ")";
                    }
                    else
                    {
                        newStrSQL = "INSERT INTO [tblVendorBrands]([VendorID], [BrandId]) " +
                                    "VALUES(" + vendorid + ", " + brandidA[i].ToString().Trim() + ")";
                        if (strSQL.Length + newStrSQL.Length < 8000)
                            strSQL = strSQL + " " + newStrSQL;
                        else
                        {
                            lastindex = i;
                            completed = false;
                            return strSQL;
                        }
                    }
                }
            }
            completed = true;
            return strSQL;
        }

        private string GenericInsert(string strSQL)
        {
            SqlParameter[] sqlParams = new SqlParameter[2];
            sqlParams[0] = new SqlParameter("@SQLStatement", SqlDbType.NText);
            sqlParams[0].Value = strSQL;
            sqlParams[1] = new SqlParameter("@retVal", SqlDbType.Int);
            sqlParams[1].Direction= ParameterDirection.Output;
            SqlHelper.ExecuteNonQuery(connstring, CommandType.StoredProcedure, "[s3p_EBid_ExecuteSqlStatement]", sqlParams);
            return sqlParams[1].Value.ToString().Trim();
        }

        public bool InsertItemsCarriedMain(string vendorid, string itemCarriedIds)
        {
            string[] itemCarriedIdA = itemCarriedIds.Split(Convert.ToChar(","));
            bool completed = false;
            int lastindex = 0;
            while (completed == false)
            {
                string strSQL = CreateInsertItemsCarriedStmt(itemCarriedIdA, vendorid, ref lastindex, ref completed);
                //execute strSQL; retval should be 0; no error
                string retVal = GenericInsert(strSQL);
                if (Int32.Parse(retVal) > 0)
                    return false;
            }
            return true;
        }

        private string CreateInsertItemsCarriedStmt(string[] itemCarriedIdA, string vendorid, ref int lastindex, ref bool completed)
        {
            string strSQL = "";

            for (int i = lastindex; i < itemCarriedIdA.Length; i++)
            {
                if (itemCarriedIdA[i] != null)
                {
                    string newStrSQL = "";
                    if (strSQL == "")
                    {
                        strSQL = "INSERT INTO [tblVendorProdItems]([VendorID], [ProdItemNo]) " +
                                    "VALUES(" + vendorid + ", " + itemCarriedIdA[i].ToString().Trim() + ")";
                    }
                    else
                    {
                        newStrSQL = "INSERT INTO [tblVendorProdItems]([VendorID], [ProdItemNo]) " +
                                    "VALUES(" + vendorid + ", " + itemCarriedIdA[i].ToString().Trim() + ")";
                        if (strSQL.Length + newStrSQL.Length < 8000)
                            strSQL = strSQL + " " + newStrSQL;
                        else
                        {
                            lastindex = i;
                            completed = false;
                            return strSQL;
                        }
                    }
                }
            }
            completed = true;
            return strSQL;
        }

        public bool InsertServicesOfferedMain(string vendorid, string serviceids)
        {
            string[] serviceidA = serviceids.Split(Convert.ToChar(","));
            bool completed = false;
            int lastindex = 0;
            while (completed == false)
            {
                string strSQL = CreateInsertServicesOfferedStmt(serviceidA, vendorid, ref lastindex, ref completed);
                //execute strSQL; retval should be 0; no error
                string retVal = GenericInsert(strSQL);
                if (Int32.Parse(retVal) > 0)
                    return false;
            }
            return true;
        }

        private string CreateInsertServicesOfferedStmt(string[] serviceidA, string vendorid, ref int lastindex, ref bool completed)
        {
            string strSQL = "";

            for (int i = lastindex; i < serviceidA.Length; i++)
            {
                if (serviceidA[i] != null)
                {
                    string newStrSQL = "";
                    if (strSQL == "")
                    {
                        strSQL = "INSERT INTO [tblVendorServices]([VendorID], [ServiceID]) " +
                                    "VALUES(" + vendorid + ", " + serviceidA[i].ToString().Trim() + ")";
                    }
                    else
                    {
                        newStrSQL = "INSERT INTO [tblVendorServices]([VendorID], [ServiceID]) " +
                                    "VALUES(" + vendorid + ", " + serviceidA[i].ToString().Trim() + ")";
                        if (strSQL.Length + newStrSQL.Length < 8000)
                            strSQL = strSQL + " " + newStrSQL;
                        else
                        {
                            lastindex = i;
                            completed = false;
                            return strSQL;
                        }
                    }
                }
            }
            completed = true;
            return strSQL;
        }


        public bool InsertLocationMain(string vendorid, string locationids)
        {
            string[] locationidA = locationids.Split(Convert.ToChar(","));
            bool completed = false;
            int lastindex = 0;
            while (completed == false)
            {
                string strSQL = CreateInsertLocationStmt(locationidA, vendorid, ref lastindex, ref completed);
                //execute strSQL; retval should be 0; no error
                string retVal = GenericInsert(strSQL);
                if (Int32.Parse(retVal) > 0)
                    return false;
            }
            return true;
        }

        private string CreateInsertLocationStmt(string[] locationidA, string vendorid, ref int lastindex, ref bool completed)
        {
            string strSQL = "";

            for (int i = lastindex; i < locationidA.Length; i++)
            {
                if (locationidA[i] != null)
                {
                    string newStrSQL = "";
                    if (strSQL == "")
                    {
                        strSQL = "INSERT INTO [tblVendorLocation]([VendorID], [LocationID]) " +
                                    "VALUES(" + vendorid + ", " + locationidA[i].ToString().Trim() + ")";
                    }
                    else
                    {
                        newStrSQL = "INSERT INTO [tblVendorLocation]([VendorID], [LocationID]) " +
                                    "VALUES(" + vendorid + ", " + locationidA[i].ToString().Trim() + ")";
                        if (strSQL.Length + newStrSQL.Length < 8000)
                            strSQL = strSQL + " " + newStrSQL;
                        else
                        {
                            lastindex = i;
                            completed = false;
                            return strSQL;
                        }
                    }
                }
            }
            completed = true;
            return strSQL;
        }


        public DataTable GetVendorsWithSubmittedTenders(string vBidRefNo)
        {
            SqlParameter[] sqlParams = new SqlParameter[2];
            sqlParams[0] = new SqlParameter("@bidRefNo", SqlDbType.Int);
            sqlParams[0].Value = Int32.Parse(vBidRefNo);
            sqlParams[1] = new SqlParameter("@status", SqlDbType.Int);
            sqlParams[1].Value = Constant.BID_TENDER_STATUS_SUBMITTED;

            return SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "s3p_EBid_GetVendorsWithSubmittedTenders", sqlParams).Tables[0];
        }


        public DataTable GetVendorsWithEndorsedTenders(string vBidRefNo)
        {
            SqlParameter[] sqlParams = new SqlParameter[2];
            sqlParams[0] = new SqlParameter("@bidRefNo", SqlDbType.Int);
            sqlParams[0].Value = Int32.Parse(vBidRefNo);
            sqlParams[1] = new SqlParameter("@status", SqlDbType.Int);
            sqlParams[1].Value = Constant.BID_TENDER_STATUS_ENDORSED;

            return SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "s3p_EBid_GetVendorsWithEndorsedTenders", sqlParams).Tables[0];
        }

        public void InsertVendorFileUploads(string vVendorID, string vBidRefNo, string vFileName, string vFileNamePath)
        {
            vFileName = vFileName.Replace("'", "''");
            vFileNamePath = vFileNamePath.Replace("'", "''");

            SqlParameter[] sqlParams = new SqlParameter[4];
            sqlParams[0] = new SqlParameter("@bidRefNo", SqlDbType.Int);
            sqlParams[0].Value = Int32.Parse(vBidRefNo);
            sqlParams[1] = new SqlParameter("@vendorId", SqlDbType.Int);
            sqlParams[1].Value = Int32.Parse(vVendorID);
            sqlParams[2] = new SqlParameter("@fileName", SqlDbType.NVarChar);
            sqlParams[2].Value = vFileName;
            sqlParams[3] = new SqlParameter("@fileNamePath", SqlDbType.NVarChar);
            sqlParams[3].Value = vFileNamePath;

            SqlHelper.ExecuteNonQuery(connstring, CommandType.StoredProcedure, "s3p_EBid_InsertVendorFileUploads", sqlParams);
        }

        public void UpdateVendorFileUploads(string vFileUploadID, string vFileName, string vFileNamePath)
        {
            SqlParameter[] sqlParams = new SqlParameter[3];
            sqlParams[0] = new SqlParameter("@fileUploadId", SqlDbType.Int);
            sqlParams[0].Value = Int32.Parse(vFileUploadID);
            sqlParams[1] = new SqlParameter("@fileName", SqlDbType.NVarChar);
            sqlParams[1].Value = vFileName;
            sqlParams[2] = new SqlParameter("@fileNamePath", SqlDbType.NVarChar);
            sqlParams[2].Value = vFileNamePath;

            SqlHelper.ExecuteNonQuery(connstring, CommandType.StoredProcedure, "s3p_EBid_UpdateVendorFileUploads", sqlParams);
        }

        public DataTable QueryFilesViaBidRefNoVendorID(string vVendorID, string vBidRefNo)
        {
            SqlParameter[] sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@bidRefNo", SqlDbType.Int);
            sqlParams[0].Value = Int32.Parse(vBidRefNo);
            sqlParams[0] = new SqlParameter("@vendorId", SqlDbType.Int);
            sqlParams[0].Value = Int32.Parse(vVendorID);

            return SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "s3p_EBid_QueryFilesViaBidRefNoVendorID", sqlParams).Tables[0];
        }

        public DataTable QueryBrandsByVendorId(string vVendorId)
        {
            SqlParameter[] sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@vendorId", SqlDbType.Int);
            sqlParams[0].Value = Int32.Parse(vVendorId);

            return SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "s3p_EBid_QueryBrandsByVendorId", sqlParams).Tables[0];
        }

        public DataTable QueryItemsCarriedByVendorId(string vVendorId)
        {
            SqlParameter[] sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@vendorId", SqlDbType.Int);
            sqlParams[0].Value = Int32.Parse(vVendorId);

            return SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "s3p_EBid_QueryItemsCarriedByVendorId", sqlParams).Tables[0];
        }

        public DataTable QueryServicesByVendorId(string vVendorId)
        {
            SqlParameter[] sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@vendorId", SqlDbType.Int);
            sqlParams[0].Value = Int32.Parse(vVendorId);

            return SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "s3p_EBid_QueryServicesByVendorId", sqlParams).Tables[0];
        }

        public DataTable QueryLocationsByVendorId(string vVendorId)
        {
            SqlParameter[] sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@vendorId", SqlDbType.Int);
            sqlParams[0].Value = Int32.Parse(vVendorId);

            return SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "s3p_EBid_QueryLocationsByVendorId", sqlParams).Tables[0];
        }

        public Supplier QuerySuppliers(string vVendorID)
        {
            SqlParameter[] sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@vendorId", SqlDbType.Int);
            sqlParams[0].Value = Int32.Parse(vVendorID);

            DataSet VendorData = SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "s3p_EBid_QuerySuppliers", sqlParams);
        
            DataTable dt = new DataTable();
            Supplier s = new Supplier();

            if (VendorData.Tables.Count > 0)
            {
                dt = VendorData.Tables[0];
                s.VendorName = dt.Rows[0]["VendorName"].ToString().Trim();
                s.VendorAddress = dt.Rows[0]["VendorAddress"].ToString().Trim();
                s.VendorAddress1 = dt.Rows[0]["VendorAddress1"].ToString().Trim();
                s.TelephoneNumber = dt.Rows[0]["TelephoneNo"].ToString().Trim();
                s.Fax = dt.Rows[0]["Fax"].ToString().Trim();
                s.Extension = dt.Rows[0]["Extension"].ToString().Trim();
                s.VendorAddress2 = dt.Rows[0]["VendorAddress2"].ToString().Trim();
                s.VendorAddress3 = dt.Rows[0]["VendorAddress3"].ToString().Trim();
                s.BranchTelephoneNo = dt.Rows[0]["BranchTelephoneNo"].ToString().Trim();
                s.BranchFax = dt.Rows[0]["BranchFax"].ToString().Trim();
                s.BranchExtension = dt.Rows[0]["BranchExtension"].ToString().Trim();
                s.VatRegNo = dt.Rows[0]["VatRegNo"].ToString().Trim();
                s.TIN = dt.Rows[0]["TIN"].ToString().Trim();
                s.POBOX = dt.Rows[0]["POBox"].ToString().Trim();
                s.PostalCode = dt.Rows[0]["PostalCode"].ToString().Trim();
                s.VendorEmail = dt.Rows[0]["VendorEmail"].ToString().Trim();
                s.TermsOfPayment = dt.Rows[0]["TermsofPayment"].ToString().Trim();
                s.SpecialTerms = dt.Rows[0]["SpecialTerms"].ToString().Trim();
                s.MinOrderValue = dt.Rows[0]["MinOrderValue"].ToString().Trim();
                s.ContactPerson = dt.Rows[0]["ContactPerson"].ToString().Trim();
                s.SalesPersonTelNo = dt.Rows[0]["SalesPersonTelNo"].ToString().Trim();
                s.OrgTypeId = dt.Rows[0]["OrgTypeID"].ToString().Trim();
                s.OwnershipFilipino = dt.Rows[0]["OwnershipFilipino"].ToString().Trim();
                s.OwnershipOther = dt.Rows[0]["OwnershipOther"].ToString().Trim();
                s.SoleSupplier1 = dt.Rows[0]["SoleSupplier1"].ToString().Trim();
                s.SoleSupplier2 = dt.Rows[0]["SoleSupplier2"].ToString().Trim();
                s.Specialization = dt.Rows[0]["Specialization"].ToString().Trim();
                s.Accredited = dt.Rows[0]["Accredited"].ToString().Trim();
                s.PCABClass = dt.Rows[0]["PCABClass"].ToString().Trim();
                s.ISOStandard = dt.Rows[0]["ISOStandard"].ToString().Trim();
                s.KeyPersonnel = dt.Rows[0]["KeyPersonnel"].ToString().Trim();
                s.KeyPosition = dt.Rows[0]["KpPosition"].ToString().Trim();
            }
            else
            {
                s.VendorName = "";
                s.VendorAddress = "";
                s.VendorAddress1 = "";
                s.TelephoneNumber = "";
                s.Fax = "";
                s.Extension = "";
                s.VendorAddress2 = "";
                s.VendorAddress3 = "";
                s.BranchTelephoneNo = "";
                s.BranchFax = "";
                s.BranchExtension = "";
                s.VatRegNo = "";
                s.TIN = "";
                s.POBOX = "";
                s.PostalCode = "";
                s.VendorEmail = "";
                s.TermsOfPayment = "";
                s.SpecialTerms = "";
                s.MinOrderValue = "";
                s.ContactPerson = "";
                s.SalesPersonTelNo = "";
                s.OrgTypeId = "";
                s.OwnershipFilipino = "";
                s.OwnershipOther = "";
                s.Classification = "";
                s.SoleSupplier1 = "";
                s.SoleSupplier2 = "";
                s.Specialization = "";
                s.Accredited = "";
                s.PCABClass = "";
                s.ISOStandard = "";
                s.KeyPersonnel = "";
                s.KeyPosition = "";
            }
            return s;
        }



        public DataTable QueryPresentServices(string vVendorId)
        {
            SqlConnection sqlConnect = new SqlConnection(connstring);
            DataSet dsQueryResult;
            sqlConnect.Open();
            SqlParameter[] sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@vendorId", SqlDbType.Int);
            sqlParams[0].Value = vVendorId;
            dsQueryResult = SqlHelper.ExecuteDataset(sqlConnect, "s3p_EBid_GetPresentServicesForAVendorId", sqlParams);
            sqlConnect.Close();
            return dsQueryResult.Tables[0];
        }

        public DataTable QueryMajorCustomers(string vVendorId)
        {
            SqlConnection sqlConnect = new SqlConnection(connstring);
            DataSet dsQueryResult;
            sqlConnect.Open();
            SqlParameter[] sqlParams = new SqlParameter[2];
            sqlParams[0] = new SqlParameter("@vendorId", SqlDbType.Int);
            sqlParams[0].Value = vVendorId;
            sqlParams[1] = new SqlParameter("@ReferenceType", SqlDbType.Int);
            sqlParams[1].Value = Constant.REF_MAIN_CUSTOMERS.ToString().Trim();
            dsQueryResult = SqlHelper.ExecuteDataset(sqlConnect, "s3p_EBid_QueryMajorCustomerByVendorId", sqlParams);
            sqlConnect.Close();
            return dsQueryResult.Tables[0];
        }

       

        public DataTable QueryBanks(string vVendorId)
        {
            SqlConnection sqlConnect = new SqlConnection(connstring);
            DataSet dsQueryResult;
            sqlConnect.Open();
            SqlParameter[] sqlParams = new SqlParameter[2];
            sqlParams[0] = new SqlParameter("@vendorId", SqlDbType.Int);
            sqlParams[0].Value = vVendorId;
            sqlParams[1] = new SqlParameter("@ReferenceType", SqlDbType.Int);
            sqlParams[1].Value = Constant.REF_BANKS.ToString().Trim();
            dsQueryResult = SqlHelper.ExecuteDataset(sqlConnect, "s3p_EBid_QueryBanksByVendorId", sqlParams);
            sqlConnect.Close();
            return dsQueryResult.Tables[0];
        }

        public DataTable QueryAffiliatedCompanies(string vVendorId)
        {
            SqlConnection sqlConnect = new SqlConnection(connstring);
            DataSet dsQueryResult;
            sqlConnect.Open();
            SqlParameter[] sqlParams = new SqlParameter[2];
            sqlParams[0] = new SqlParameter("@vendorId", SqlDbType.Int);
            sqlParams[0].Value = vVendorId;
            sqlParams[1] = new SqlParameter("@ReferenceType", SqlDbType.Int);
            sqlParams[1].Value = Constant.REF_AFFILIATE.ToString().Trim();
            dsQueryResult = SqlHelper.ExecuteDataset(sqlConnect, "s3p_EBid_QueryAffiliatedCompaniesByVendorId", sqlParams);
            sqlConnect.Close();
            return dsQueryResult.Tables[0];
        }

        public DataTable QueryExternalAuditors(string vVendorId)
        {
            SqlConnection sqlConnect = new SqlConnection(connstring);
            DataSet dsQueryResult;
            sqlConnect.Open();
            SqlParameter[] sqlParams = new SqlParameter[2];
            sqlParams[0] = new SqlParameter("@vendorId", SqlDbType.Int);
            sqlParams[0].Value = vVendorId;
            sqlParams[1] = new SqlParameter("@ReferenceType", SqlDbType.Int);
            sqlParams[1].Value = Constant.REF_EXTERN_AUDITOR.ToString().Trim();
            dsQueryResult = SqlHelper.ExecuteDataset(sqlConnect, "s3p_EBid_QueryExternalAuditorsByVendorId", sqlParams);
            sqlConnect.Close();
            return dsQueryResult.Tables[0];
        }
        
        public DataTable QueryEquipment(string vVendorId)
        {
            SqlConnection sqlConnect = new SqlConnection(connstring);
            DataSet dsQueryResult;
            sqlConnect.Open();
            SqlParameter[] sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@vendorId", SqlDbType.Int);
            sqlParams[0].Value = vVendorId;
            dsQueryResult = SqlHelper.ExecuteDataset(sqlConnect, "s3p_EBid_QueryEquipmentByVendorId", sqlParams);
            sqlConnect.Close();
            return dsQueryResult.Tables[0];
        }

        
        public DataTable QueryRelatives(string vVendorId)
        {
            SqlConnection sqlConnect = new SqlConnection(connstring);
            DataSet dsQueryResult;
            sqlConnect.Open();
            SqlParameter[] sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@vendorId", SqlDbType.Int);
            sqlParams[0].Value = vVendorId;
            dsQueryResult = SqlHelper.ExecuteDataset(sqlConnect, "s3p_EBid_QueryRelativesByVendorId", sqlParams);
            sqlConnect.Close();
            return dsQueryResult.Tables[0];
        }


        public DataSet QueryCategoryAndSubCategory(string vVendorId)
        {
            DataSet dsQueryResult;
            SqlParameter[] sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@vendorId", SqlDbType.Int);
            sqlParams[0].Value = vVendorId;
            dsQueryResult = SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "s3p_EBid_GetCategoryAndSubCategoryForAVendor", sqlParams);
            return dsQueryResult;
        }

        public bool CheckUser(string vUserName)
        {
            SqlConnection sqlConnect = new SqlConnection(connstring);
            sqlConnect.Open();
            SqlParameter[] sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@UserName", SqlDbType.NVarChar);
            sqlParams[0].Value = vUserName;
            int count = Convert.ToInt32(SqlHelper.ExecuteScalar(sqlConnect, CommandType.StoredProcedure, "s3p_EBid_CheckIfUserNameExists", sqlParams));
            sqlConnect.Close();
            if (count == 0)
                return true;
            else
                return false;
        }

        public bool CheckUserExcept(string vUserName, string oldUsername)
        {
            SqlConnection sqlConnect = new SqlConnection(connstring);
            sqlConnect.Open();
            SqlParameter[] sqlParams = new SqlParameter[2];
            sqlParams[0] = new SqlParameter("@UserName", SqlDbType.NVarChar);
            sqlParams[0].Value = vUserName;
            sqlParams[1] = new SqlParameter("@OldUsername", SqlDbType.NVarChar);
            sqlParams[1].Value = oldUsername;
            int count = Convert.ToInt32(SqlHelper.ExecuteScalar(sqlConnect, CommandType.StoredProcedure, "s3p_EBid_CheckIfUserNameExists", sqlParams));
            sqlConnect.Close();
            if (count == 0)
                return true;
            else
                return false;

        }

        public int InsertUser(string vUserName, string vPassword, string vUserType)
        {
            try
            {
                SqlConnection sqlConnect = new SqlConnection(connstring);
                sqlConnect.Open();
                SqlParameter[] sqlParams = new SqlParameter[3];
                sqlParams[0] = new SqlParameter("@userName", SqlDbType.NVarChar);
                sqlParams[0].Value = vUserName;
                sqlParams[1] = new SqlParameter("@password", SqlDbType.NVarChar);
                sqlParams[1].Value = vPassword;
                sqlParams[2] = new SqlParameter("@userType", SqlDbType.Int);
                sqlParams[2].Value = vUserType;
                int UserId = Convert.ToInt32(SqlHelper.ExecuteScalar(sqlConnect, CommandType.StoredProcedure, "s3p_EBid_InsertNewUser", sqlParams));
                sqlConnect.Close();
                return UserId;
            }
            catch
            {
                return -1;
            }
        }

        public int UpdateSupplier(string vVendorId, string vAccredited,  string vPCABClass, string vISOStandard)
        {
            try
            {
                SqlConnection sqlConnect = new SqlConnection(connstring);
                sqlConnect.Open();
                SqlParameter[] sqlParams = new SqlParameter[4];
                sqlParams[0] = new SqlParameter("@VendorId", SqlDbType.Int);
                sqlParams[0].Value = vVendorId;
                sqlParams[1] = new SqlParameter("@Accredited", SqlDbType.Int);
                sqlParams[1].Value = vAccredited;
                sqlParams[2] = new SqlParameter("@PCABClass", SqlDbType.Int);
                sqlParams[2].Value = vPCABClass;
                sqlParams[3] = new SqlParameter("@ISOStandard", SqlDbType.VarChar);
                sqlParams[3].Value = vISOStandard;
                int UserId = Convert.ToInt32(SqlHelper.ExecuteScalar(sqlConnect, CommandType.StoredProcedure, "s3p_EBid_UpdateVendor", sqlParams));
                sqlConnect.Close();
                return UserId;
            }
            catch
            {
                return -1;
            }
        }

        public int DeleteBSLP(string vVendorId)
        {

            try
            {
                SqlParameter[] sqlParams = new SqlParameter[1];
                sqlParams[0] = new SqlParameter("@VendorId", SqlDbType.Int);
                sqlParams[0].Value = vVendorId;
                int Result = Convert.ToInt32(SqlHelper.ExecuteScalar(connstring, CommandType.StoredProcedure, "s3p_EBid_DeleteOldVendorBSLP", sqlParams));
                return Result;
            }
            catch
            {
                return -1;
            }
        }

        public int DeleteVendorCategorySubCategory(string vVendorId)
        {

            try
            {
                SqlParameter[] sqlParams = new SqlParameter[1];
                sqlParams[0] = new SqlParameter("@VendorId", SqlDbType.Int);
                sqlParams[0].Value = vVendorId;
                int Result = Convert.ToInt32(SqlHelper.ExecuteScalar(connstring, CommandType.StoredProcedure, "s3p_EBid_DeleteVendorCategorySubCategory", sqlParams));
                return Result;
            }
            catch
            {
                return -1;
            }
        }

        public DataSet QuerySelectedAndUnselectedCategories(string categoryid)
        {
            DataSet dsQueryResult;
            SqlParameter[] sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@CategoryId", SqlDbType.NText);
            if (categoryid != "")
                sqlParams[0].Value =  categoryid;
            else
                sqlParams[0].Value = System.DBNull.Value;
            
            dsQueryResult = SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "s3p_EBid_GetSelectedAndUnselectedCategories", sqlParams);
            HttpContext.Current.Response.Write(connstring);
            return dsQueryResult;
        }

        public DataSet QuerySelectedAndUnselectedSubCategories(string subcategoryid, string categoryid, string connstring)
        {
            DataSet dsQueryResult;
            SqlParameter[] sqlParams = new SqlParameter[2];
            sqlParams[0] = new SqlParameter("@SubCategoryId", SqlDbType.NText);
            sqlParams[0].Value = subcategoryid;
            sqlParams[1] = new SqlParameter("@CategoryId", SqlDbType.NText);
            sqlParams[1].Value = categoryid;
            dsQueryResult = SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "s3p_EBid_GetSelectedAndUnselectedSubCategories", sqlParams);
            return dsQueryResult;
        }

        public DataTable GetSupplierEmail(string vBidRefNo)
        {
            SqlParameter[] sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@BidRefNo", SqlDbType.Int);
            sqlParams[0].Value = vBidRefNo;
            DataTable dt = new DataTable();

            DataSet ds = SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "s3p_EBid_GetEmailAddresses", sqlParams);
            if (ds.Tables.Count > 0)
                dt = ds.Tables[0];
            //string email = "";
            //for (int i = 0; i < dt.Rows.Count; i++ )
            //    email += (email == "") ? dt.Rows[i]["VendorEmail"].ToString().Trim() : ";" + dt.Rows[i]["VendorEmail"].ToString().Trim();
            return dt;
        }


	}
}
