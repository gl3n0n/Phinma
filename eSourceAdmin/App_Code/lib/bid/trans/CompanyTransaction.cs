using System;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using EBid.lib;
using EBid.lib.utils;
using EBid.lib.bid.data;
using EBid.lib.constant;
using System.Web;

namespace EBid.lib.bid.trans
{
	/// <summary>
	/// Summary description for CompanyTransaction.
	/// </summary>
	public class CompanyTransaction
	{
        private static string connstring = HttpContext.Current.Session["ConnectionString"].ToString();
        
		public string GetCompanyName(string vCompanyId) 
		{
            SqlParameter[] sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@companyId", SqlDbType.Int);
            sqlParams[0].Value = Int32.Parse(vCompanyId);

            return SqlHelper.ExecuteScalar(connstring, CommandType.StoredProcedure, "s3p_EBid_GetCompanyName", sqlParams).ToString().Trim();
		}

        public CompanyDetails QueryCompanyDetails(string vendorID)
        {
            SqlParameter[] sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@vendorID", SqlDbType.Int);
            sqlParams[0].Value = Int32.Parse(vendorID);
           

            DataTable companyDataTable = SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "s3p_EBid_QueryCompanyProfile", sqlParams).Tables[0];
            CompanyDetails companyInfo = new CompanyDetails();

            if (companyDataTable.Rows.Count > 0)
            {
                DataRow companyDataRow = companyDataTable.Rows[0];
                companyInfo.VendorID = Int32.Parse(companyDataRow["VendorId"].ToString().Trim());
                companyInfo.CompanyName = companyDataRow["VendorName"].ToString().Trim();
                companyInfo.CompanyEmail = companyDataRow["VendorEmail"].ToString().Trim();
                companyInfo.Address = companyDataRow["VendorAddress"].ToString().Trim();
                companyInfo.Address1 = companyDataRow["VendorAddress1"].ToString().Trim();
                companyInfo.HeadTelephone = companyDataRow["Telephone"].ToString().Trim();
                companyInfo.HeadMobileNo = companyDataRow["MobileNo"].ToString().Trim();
                companyInfo.HeadFax = companyDataRow["Fax"].ToString().Trim();
                companyInfo.HeadExtension = companyDataRow["Extension"].ToString().Trim();
                companyInfo.Address2 = companyDataRow["VendorAddress2"].ToString().Trim();
                companyInfo.Address3 = companyDataRow["VendorAddress3"].ToString().Trim();
                companyInfo.BranchTelephone = companyDataRow["BranchTelephoneNo"].ToString().Trim();
                companyInfo.BranchFax = companyDataRow["BranchFax"].ToString().Trim();
                companyInfo.BranchExtension = companyDataRow["BranchExtension"].ToString().Trim();
                companyInfo.SalesPerson = companyDataRow["ContactPerson"].ToString().Trim();
                companyInfo.SalesPersonPhone = companyDataRow["SalesPersonTelNo"].ToString().Trim();
                companyInfo.VatRegNo = companyDataRow["VatRegNo"].ToString().Trim();
                companyInfo.TIN = companyDataRow["TIN"].ToString().Trim();
                companyInfo.POBox = companyDataRow["POBox"].ToString().Trim();
                companyInfo.PostalCode = companyDataRow["PostalCode"].ToString().Trim();
                companyInfo.TermsofPayment = companyDataRow["TermsofPayment"].ToString().Trim();
                companyInfo.SpecialTerms = companyDataRow["SpecialTerms"].ToString().Trim();
                companyInfo.MinOrderValue = companyDataRow["MinOrderValue"].ToString().Trim();
                companyInfo.Classification = (companyDataRow["Classification"] != DBNull.Value) ? Int32.Parse(companyDataRow["Classification"].ToString().Trim()) : 1;
                companyInfo.OwnershipFilipino = companyDataRow["OwnershipFilipino"].ToString().Trim();
                companyInfo.OwnershipOther = companyDataRow["OwnershipOther"].ToString().Trim();
                companyInfo.OrganizationType = (companyDataRow["OrgTypeID"] != DBNull.Value) ? Int32.Parse(companyDataRow["OrgTypeID"].ToString().Trim()) : 1;
                companyInfo.Accredited = (companyDataRow["Accredited"] != DBNull.Value) ? Int32.Parse(companyDataRow["Accredited"].ToString().Trim()) : 0;
                companyInfo.Category = companyDataRow["VendorCategory"].ToString().Trim();
                companyInfo.Specialization = companyDataRow["Specialization"].ToString().Trim();
                companyInfo.SoleSupplier = companyDataRow["SoleSupplier1"].ToString().Trim();
                companyInfo.SoleSupplier1 = companyDataRow["SoleSupplier2"].ToString().Trim();
                companyInfo.KeyPersonnel = companyDataRow["KeyPersonnel"].ToString().Trim();
                companyInfo.KpPosition = companyDataRow["KpPosition"].ToString().Trim();
                companyInfo.ISOStandard = companyDataRow["ISOStandard"].ToString().Trim();
                companyInfo.PCABClass = Int32.Parse(companyDataRow["PCABClass"].ToString().Trim());
                companyInfo.IsBlackListed = Int32.Parse(companyDataRow["isBlackListed"].ToString().Trim());
            }

            return companyInfo;
        }

        public void UpdateCompanyDetails(string vendorID, CompanyDetails companyInfo)
        {
            SqlConnection sqlConnect = new SqlConnection(connstring);
            SqlTransaction sqlTransact = null;

            try
            {
                sqlConnect.Open();
                sqlTransact = sqlConnect.BeginTransaction();

                SqlParameter[] sqlParams = new SqlParameter[35];
                sqlParams[0] = new SqlParameter("@userId", SqlDbType.Int);
                sqlParams[0].Value = Int32.Parse(vendorID.Trim());
                sqlParams[1] = new SqlParameter("@vendorName", SqlDbType.VarChar);
                sqlParams[1].Value = companyInfo.CompanyName;
                sqlParams[2] = new SqlParameter("@vendorAddress", SqlDbType.VarChar);
                sqlParams[2].Value = (companyInfo.Address.Trim().Length > 0) ? companyInfo.Address.Trim() : null;
                sqlParams[3] = new SqlParameter("@vendorAddress1", SqlDbType.VarChar);
                sqlParams[3].Value = (companyInfo.Address1.Trim().Length > 0) ? companyInfo.Address1.Trim() : null;
                sqlParams[4] = new SqlParameter("@vendorPhone", SqlDbType.VarChar);
                sqlParams[4].Value = (companyInfo.HeadTelephone.Trim().Length > 0) ? companyInfo.HeadTelephone.Trim() : null;
                sqlParams[5] = new SqlParameter("@vendorFax", SqlDbType.VarChar);
                sqlParams[5].Value = (companyInfo.HeadFax.Trim().Length > 0) ? companyInfo.HeadFax.Trim() : null;
                sqlParams[6] = new SqlParameter("@vendorExt", SqlDbType.VarChar);
                sqlParams[6].Value = (companyInfo.HeadExtension.Trim().Length > 0) ? companyInfo.HeadExtension.Trim() : null;
                sqlParams[7] = new SqlParameter("@branchAddress", SqlDbType.VarChar);
                sqlParams[7].Value = (companyInfo.Address2.Trim().Length > 0) ? companyInfo.Address2.Trim() : null;
                sqlParams[8] = new SqlParameter("@branchAddress1", SqlDbType.VarChar);
                sqlParams[8].Value = (companyInfo.Address3.Trim().Length > 0) ? companyInfo.Address3.Trim() : null;
                sqlParams[9] = new SqlParameter("@branchPhone", SqlDbType.VarChar);
                sqlParams[9].Value = (companyInfo.BranchTelephone.Trim().Length > 0) ? companyInfo.BranchTelephone.Trim() : null;
                sqlParams[10] = new SqlParameter("@branchFax", SqlDbType.VarChar);
                sqlParams[10].Value = (companyInfo.BranchFax.Trim().Length > 0) ? companyInfo.BranchFax.Trim() : null;
                sqlParams[11] = new SqlParameter("@branchExt", SqlDbType.VarChar);
                sqlParams[11].Value = (companyInfo.BranchExtension.Trim().Length > 0) ? companyInfo.BranchExtension.Trim() : null;
                sqlParams[12] = new SqlParameter("@vatRegNo", SqlDbType.VarChar);
                sqlParams[12].Value = (companyInfo.VatRegNo.Trim().Length > 0) ? companyInfo.VatRegNo.Trim() : null;
                sqlParams[13] = new SqlParameter("@TIN", SqlDbType.VarChar);
                sqlParams[13].Value = (companyInfo.TIN.Trim().Length > 0) ? companyInfo.TIN.Trim() : null;
                sqlParams[14] = new SqlParameter("@POBox", SqlDbType.VarChar);
                sqlParams[14].Value = (companyInfo.POBox.Trim().Length > 0) ? companyInfo.POBox.Trim() : null;
                sqlParams[15] = new SqlParameter("@postalCode", SqlDbType.VarChar);
                sqlParams[15].Value = (companyInfo.PostalCode.Trim().Length > 0) ? companyInfo.PostalCode.Trim() : null;
                sqlParams[16] = new SqlParameter("@standardTerms", SqlDbType.VarChar);
                sqlParams[16].Value = (companyInfo.TermsofPayment.Trim().Length > 0) ? companyInfo.TermsofPayment.Trim() : null;
                sqlParams[17] = new SqlParameter("@specialTerms", SqlDbType.VarChar);
                sqlParams[17].Value = (companyInfo.SpecialTerms.Trim().Length > 0) ? companyInfo.SpecialTerms.Trim() : null;
                sqlParams[18] = new SqlParameter("@minOrderVal", SqlDbType.Float);
                sqlParams[18].Value = (companyInfo.MinOrderValue.Trim().Length > 0) ? float.Parse(companyInfo.MinOrderValue.Trim()) : 0;
                sqlParams[19] = new SqlParameter("@salesPerson", SqlDbType.VarChar);
                sqlParams[19].Value = (companyInfo.SalesPerson.Trim().Length > 0) ? companyInfo.SalesPerson.Trim() : null;
                sqlParams[20] = new SqlParameter("@salesPersonPhone", SqlDbType.VarChar);
                sqlParams[20].Value = (companyInfo.SalesPersonPhone.Trim().Length > 0) ? companyInfo.SalesPersonPhone.Trim() : null;
                sqlParams[21] = new SqlParameter("@soleSupplier1", SqlDbType.VarChar);
                sqlParams[21].Value = (companyInfo.SoleSupplier.Trim().Length > 0) ? companyInfo.SoleSupplier.Trim() : null;
                sqlParams[22] = new SqlParameter("@soleSupplier2", SqlDbType.VarChar);
                sqlParams[22].Value = (companyInfo.SoleSupplier1.Trim().Length > 0) ? companyInfo.SoleSupplier1.Trim() : null;
                sqlParams[23] = new SqlParameter("@keyPersonnel", SqlDbType.Int);
                sqlParams[23].Value = (companyInfo.KeyPersonnel.Trim().Length > 0) ? companyInfo.KeyPersonnel.Trim() : null;
                sqlParams[24] = new SqlParameter("@kpPosition", SqlDbType.VarChar);
                sqlParams[24].Value = (companyInfo.KpPosition.Trim().Length > 0) ? companyInfo.KpPosition.Trim() : null;
                sqlParams[25] = new SqlParameter("@specialization", SqlDbType.VarChar);
                sqlParams[25].Value = (companyInfo.Specialization.Trim().Length > 0) ? companyInfo.Specialization.Trim() : null;
                sqlParams[26] = new SqlParameter("@category", SqlDbType.VarChar);
                sqlParams[26].Value = (companyInfo.Category.Trim().Length > 0) ? companyInfo.Category.Trim() : null;
                sqlParams[27] = new SqlParameter("@accredited", SqlDbType.Int);
                sqlParams[27].Value = System.DBNull.Value;
                sqlParams[28] = new SqlParameter("@orgTypeId", SqlDbType.Int);
                sqlParams[28].Value = companyInfo.OrganizationType;
                sqlParams[29] = new SqlParameter("@ownershipFil", SqlDbType.Int);
                sqlParams[29].Value = (companyInfo.OwnershipFilipino.Trim().Length > 0) ? Int32.Parse(companyInfo.OwnershipFilipino.Trim()) : 0;
                sqlParams[30] = new SqlParameter("@ownershipOther", SqlDbType.Int);
                sqlParams[30].Value = (companyInfo.OwnershipOther.Trim().Length > 0) ? Int32.Parse(companyInfo.OwnershipOther.Trim()) : 0;
                sqlParams[31] = new SqlParameter("@classification", SqlDbType.Int);
                sqlParams[31].Value = companyInfo.Classification;
                sqlParams[32] = new SqlParameter("@isoStandard", SqlDbType.NVarChar);
                sqlParams[32].Value = (companyInfo.ISOStandard.Trim().Length > 0) ? companyInfo.ISOStandard.Trim() : null;
                sqlParams[33] = new SqlParameter("@pcabClass", SqlDbType.Int);
                sqlParams[33].Value = companyInfo.PCABClass;
                sqlParams[34] = new SqlParameter("@vendorEmail", SqlDbType.NVarChar);
                sqlParams[34].Value = (companyInfo.CompanyEmail.Trim().Length > 0) ? companyInfo.CompanyEmail.Trim() : null;

                SqlHelper.ExecuteNonQuery(sqlTransact, "s3p_EBid_UpdateVendorDetailsFull", sqlParams);

                sqlTransact.Commit();
            }
            catch
            {
                sqlTransact.Rollback();
            }
            finally
            {
                sqlConnect.Close();
            }
        }

        public string GetClassificationName(string classification)
        {
            if (classification == "0")
                classification = "1";

            SqlParameter[] sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@classID", SqlDbType.Int);
            sqlParams[0].Value = Int32.Parse(classification);

            return SqlHelper.ExecuteScalar(connstring, CommandType.StoredProcedure, "s3p_EBid_GetClassificationName", sqlParams).ToString().Trim();
        }

        public string GetOrganizationType(string orgType)
        {
            if (orgType == "0")
                orgType = "1";

            SqlParameter[] sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@orgTypeId", SqlDbType.Int);
            sqlParams[0].Value = Int32.Parse(orgType);

            return SqlHelper.ExecuteScalar(connstring, CommandType.StoredProcedure, "s3p_EBid_GetOrganizationTypeName", sqlParams).ToString().Trim();
        }

        public DataTable QueryPresentServices(string vendorId)
        {
            SqlParameter[] sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@vendorId", SqlDbType.Int);
            sqlParams[0].Value = Int32.Parse(vendorId);

            return SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "s3p_EBid_QueryPresentServices", sqlParams).Tables[0];
        }

        public DataTable QueryAllGlobePlans()
        {
            return SqlHelper.ExecuteDataset(connstring, CommandType.Text, "s3p_EBid_QueryAllGlobePlans").Tables[0];
        }

        public DataTable QueryVendorReferences(string vendorId, int vendRefType)
        {
            SqlParameter[] sqlParams = new SqlParameter[2];
            sqlParams[0] = new SqlParameter("@vendorId", SqlDbType.Int);
            sqlParams[0].Value = Int32.Parse(vendorId);
            sqlParams[1] = new SqlParameter("@refType", SqlDbType.Int);
            sqlParams[1].Value = vendRefType;

            return SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "s3p_EBid_QueryVendorReferences", sqlParams).Tables[0];
        }

        public string GetReferenceTypeName(string refTypeID)
        {
            SqlParameter[] sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@refId", SqlDbType.Int);
            sqlParams[0].Value = Int32.Parse(refTypeID);

            return SqlHelper.ExecuteScalar(connstring, CommandType.StoredProcedure, "s3p_EBid_GetReferenceTypeName", sqlParams).ToString().Trim();
        }

        public string GetReferenceTypeExtra(string refTypeID)
        {
            SqlParameter[] sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@refId", SqlDbType.Int);
            sqlParams[0].Value = Int32.Parse(refTypeID);

            return SqlHelper.ExecuteScalar(connstring, CommandType.StoredProcedure, "s3p_EBid_GetReferenceTypeExtra", sqlParams).ToString().Trim();
        }

        public DataTable QueryVendorEquipments(string vendorId)
        {
            SqlParameter[] sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@vendorId", SqlDbType.Int);
            sqlParams[0].Value = Int32.Parse(vendorId);

            return SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "s3p_EBid_QueryVendorEquipments", sqlParams).Tables[0];
        }

        public DataTable QueryVendorRelatives(string vendorId)
        {
            SqlParameter[] sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@vendorId", SqlDbType.Int);
            sqlParams[0].Value = Int32.Parse(vendorId);

            return SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "s3p_EBid_QueryVendorRelatives", sqlParams).Tables[0];
        }

        public DataTable QueryVendorProdBrands(string vendorId, bool isSelected)
        {
            DataTable dt = null;
            SqlParameter[] sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@vendorId", SqlDbType.Int);
            sqlParams[0].Value = Int32.Parse(vendorId);

            if(isSelected)
            {
                dt = SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "s3p_EBid_QueryVendorProdBrands1", sqlParams).Tables[0];    
            }
            else
            {
                dt = SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "s3p_EBid_QueryVendorProdBrands2", sqlParams).Tables[0];    
            }

            return dt;
        }

        public DataTable QueryVendorItemsCarried(string vendorId, bool isSelected)
        {
            DataTable dt = null;
            SqlParameter[] sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@vendorId", SqlDbType.Int);
            sqlParams[0].Value = Int32.Parse(vendorId);

            if (isSelected)
            {
                dt = SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "s3p_EBid_QueryVendorItemsCarried1", sqlParams).Tables[0];
            }
            else
            {
                dt = SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "s3p_EBid_QueryVendorItemsCarried2", sqlParams).Tables[0];
            }

            return dt;
        }
        
        public DataTable QueryVendorServices(string vendorId, bool isSelected)
        {
            DataTable dt = null;
            SqlParameter[] sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@vendorId", SqlDbType.Int);
            sqlParams[0].Value = Int32.Parse(vendorId);

            if (isSelected)
            {
                dt = SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "s3p_EBid_QueryVendorServices1", sqlParams).Tables[0];
            }
            else
            {
                dt = SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "s3p_EBid_QueryVendorServices2", sqlParams).Tables[0];
            }

            return dt;
        }

        public DataTable QueryVendorLocations(string vendorId, bool isSelected)
        {
            DataTable dt = null;
            SqlParameter[] sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@vendorId", SqlDbType.Int);
            sqlParams[0].Value = Int32.Parse(vendorId);

            if (isSelected)
            {
                dt = SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "s3p_EBid_QueryVendorLocations1", sqlParams).Tables[0];
            }
            else
            {
                dt = SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "s3p_EBid_QueryVendorLocations2", sqlParams).Tables[0];
            }

            return dt;
        }

        public DataTable QueryVendorSearch(string searchstring)
        {
            SqlParameter[] sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@searchstring", SqlDbType.NVarChar);
            sqlParams[0].Value = searchstring;

            return SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "s3p_EBid_SearchForVendor", sqlParams).Tables[0];
        }

        public void UpdatePresentServices(string vendorID, string planID, string presentSvcID, 
                                          string acctNo, string creditLimit, bool isNew, SqlTransaction transact)
        {
            if (!isNew)
            {
                SqlParameter[] sqlParams = new SqlParameter[5];
                sqlParams[0] = new SqlParameter("@vendorId", SqlDbType.Int);
                sqlParams[1] = new SqlParameter("@presentSvcID", SqlDbType.Int);
                sqlParams[2] = new SqlParameter("@planID", SqlDbType.Int);
                sqlParams[3] = new SqlParameter("@acctNo", SqlDbType.NVarChar);
                sqlParams[4] = new SqlParameter("@creditLimit", SqlDbType.NVarChar);
                sqlParams[0].Value = Int32.Parse(vendorID);
                sqlParams[1].Value = Int32.Parse(presentSvcID);
                sqlParams[2].Value = Int32.Parse(planID);
                sqlParams[3].Value = acctNo;
                sqlParams[4].Value = creditLimit;

                SqlHelper.ExecuteNonQuery(transact, "s3p_EBid_UpdatetblPresentSvcs", sqlParams);
            }
            else
            {
                SqlParameter[] sqlParams = new SqlParameter[4];
                sqlParams[0] = new SqlParameter("@vendorId", SqlDbType.Int);
                sqlParams[1] = new SqlParameter("@planID", SqlDbType.Int);
                sqlParams[2] = new SqlParameter("@acctNo", SqlDbType.NVarChar);
                sqlParams[3] = new SqlParameter("@creditLimit", SqlDbType.NVarChar);
                sqlParams[0].Value = Int32.Parse(vendorID);
                sqlParams[1].Value = Int32.Parse(planID);
                sqlParams[2].Value = acctNo;
                sqlParams[3].Value = creditLimit;

                SqlHelper.ExecuteNonQuery(transact, "s3p_EBid_SaveNewPresentSvcs", sqlParams);
            }
        }

        public void UpdatePresentServices(string vendorID, string planID, string presentSvcID,
                                          string acctNo, string creditLimit, bool isNew)
        {
            if (!isNew)
            {
                SqlParameter[] sqlParams = new SqlParameter[5];
                sqlParams[0] = new SqlParameter("@vendorId", SqlDbType.Int);
                sqlParams[1] = new SqlParameter("@presentSvcID", SqlDbType.Int);
                sqlParams[2] = new SqlParameter("@planID", SqlDbType.Int);
                sqlParams[3] = new SqlParameter("@acctNo", SqlDbType.NVarChar);
                sqlParams[4] = new SqlParameter("@creditLimit", SqlDbType.NVarChar);
                sqlParams[0].Value = Int32.Parse(vendorID);
                sqlParams[1].Value = Int32.Parse(presentSvcID);
                sqlParams[2].Value = Int32.Parse(planID);
                sqlParams[3].Value = acctNo;
                sqlParams[4].Value = creditLimit;

                SqlHelper.ExecuteNonQuery(connstring, CommandType.StoredProcedure, "s3p_EBid_UpdatetblPresentSvcs", sqlParams);
            }
            else
            {
                SqlParameter[] sqlParams = new SqlParameter[4];
                sqlParams[0] = new SqlParameter("@vendorId", SqlDbType.Int);
                sqlParams[1] = new SqlParameter("@planID", SqlDbType.Int);
                sqlParams[2] = new SqlParameter("@acctNo", SqlDbType.NVarChar);
                sqlParams[3] = new SqlParameter("@creditLimit", SqlDbType.NVarChar);
                sqlParams[0].Value = Int32.Parse(vendorID);
                sqlParams[1].Value = Int32.Parse(planID);
                sqlParams[2].Value = acctNo;
                sqlParams[3].Value = creditLimit;

                SqlHelper.ExecuteNonQuery(connstring, CommandType.StoredProcedure, "s3p_EBid_SaveNewPresentSvcs", sqlParams);
            }
        }

        public void UpdateEquipments(string vendorID, string eqpmntID, string eqpmntType,
                                  string units, string remarks, bool isNew)
        {
            if (isNew)
            {
                SqlParameter[] sqlParams = new SqlParameter[4];
                sqlParams[0] = new SqlParameter("@vendorId", SqlDbType.Int);
                sqlParams[1] = new SqlParameter("@eqpmntType", SqlDbType.NVarChar);
                sqlParams[2] = new SqlParameter("@units", SqlDbType.Int);
                sqlParams[3] = new SqlParameter("@remarks", SqlDbType.NVarChar);
                sqlParams[0].Value = Int32.Parse(vendorID);
                sqlParams[1].Value = eqpmntType;
                sqlParams[2].Value = Int32.Parse(units);
                sqlParams[3].Value = remarks;

                SqlHelper.ExecuteNonQuery(connstring, CommandType.StoredProcedure, "s3p_EBid_SaveNewVendorEquipment", sqlParams);
            }
            else
            {
                SqlParameter[] sqlParams = new SqlParameter[5];
                sqlParams[0] = new SqlParameter("@vendorId", SqlDbType.Int);
                sqlParams[1] = new SqlParameter("@eqpmntID", SqlDbType.Int);
                sqlParams[2] = new SqlParameter("@eqpmntType", SqlDbType.NVarChar);
                sqlParams[3] = new SqlParameter("@units", SqlDbType.Int);
                sqlParams[4] = new SqlParameter("@remarks", SqlDbType.NVarChar);
                sqlParams[0].Value = Int32.Parse(vendorID);
                sqlParams[1].Value = Int32.Parse(eqpmntID);
                sqlParams[2].Value = eqpmntType;
                sqlParams[3].Value = Int32.Parse(units);
                sqlParams[4].Value = remarks;

                SqlHelper.ExecuteNonQuery(connstring, CommandType.StoredProcedure, "s3p_EBid_UpdateVendorEquipment", sqlParams);
            }
        }

        public void UpdateEquipments(string vendorID, string eqpmntID, string eqpmntType,
                                  string units, string remarks, bool isNew, SqlTransaction transact)
        {
            if (isNew)
            {
                SqlParameter[] sqlParams = new SqlParameter[4];
                sqlParams[0] = new SqlParameter("@vendorId", SqlDbType.Int);
                sqlParams[1] = new SqlParameter("@eqpmntType", SqlDbType.NVarChar);
                sqlParams[2] = new SqlParameter("@units", SqlDbType.Int);
                sqlParams[3] = new SqlParameter("@remarks", SqlDbType.NVarChar);
                sqlParams[0].Value = Int32.Parse(vendorID);
                sqlParams[1].Value = eqpmntType;
                sqlParams[2].Value = Int32.Parse(units);
                sqlParams[3].Value = remarks;

                SqlHelper.ExecuteNonQuery(transact, "s3p_EBid_SaveNewVendorEquipment", sqlParams);
            }
            else
            {
                SqlParameter[] sqlParams = new SqlParameter[5];
                sqlParams[0] = new SqlParameter("@vendorId", SqlDbType.Int);
                sqlParams[1] = new SqlParameter("@eqpmntID", SqlDbType.Int);
                sqlParams[2] = new SqlParameter("@eqpmntType", SqlDbType.NVarChar);
                sqlParams[3] = new SqlParameter("@units", SqlDbType.Int);
                sqlParams[4] = new SqlParameter("@remarks", SqlDbType.NVarChar);
                sqlParams[0].Value = Int32.Parse(vendorID);
                sqlParams[1].Value = Int32.Parse(eqpmntID);
                sqlParams[2].Value = eqpmntType;
                sqlParams[3].Value = Int32.Parse(units);
                sqlParams[4].Value = remarks;

                SqlHelper.ExecuteNonQuery(transact, "s3p_EBid_UpdateVendorEquipment", sqlParams);
            }
        }

        public void UpdateRelatives(string vendorID, string relativeID, string relative,
                          string title, string relation, bool isNew)
        {
            if (isNew)
            {
                SqlParameter[] sqlParams = new SqlParameter[4];
                sqlParams[0] = new SqlParameter("@vendorId", SqlDbType.Int);
                sqlParams[1] = new SqlParameter("@relative", SqlDbType.NVarChar);
                sqlParams[2] = new SqlParameter("@title", SqlDbType.NVarChar);
                sqlParams[3] = new SqlParameter("@relation", SqlDbType.NVarChar);
                sqlParams[0].Value = Int32.Parse(vendorID);
                sqlParams[1].Value = relative;
                sqlParams[2].Value = title;
                sqlParams[3].Value = relative;

                SqlHelper.ExecuteNonQuery(connstring, CommandType.StoredProcedure, "s3p_EBid_SaveNewVendorRelative", sqlParams);
            }
            else
            {
                SqlParameter[] sqlParams = new SqlParameter[5];
                sqlParams[0] = new SqlParameter("@vendorId", SqlDbType.Int);
                sqlParams[1] = new SqlParameter("@relativeID", SqlDbType.Int);
                sqlParams[2] = new SqlParameter("@relative", SqlDbType.NVarChar);
                sqlParams[3] = new SqlParameter("@title", SqlDbType.NVarChar);
                sqlParams[4] = new SqlParameter("@relation", SqlDbType.NVarChar);
                sqlParams[0].Value = Int32.Parse(vendorID);
                sqlParams[1].Value = Int32.Parse(relativeID);
                sqlParams[2].Value = relative;
                sqlParams[3].Value = title;
                sqlParams[4].Value = relative;

                SqlHelper.ExecuteNonQuery(connstring, CommandType.StoredProcedure, "s3p_EBid_UpdateVendorRelative", sqlParams);
            }
        }

        public void UpdateRelatives(string vendorID, string relativeID, string relative,
                          string title, string relation, bool isNew, SqlTransaction transact)
        {
            if (isNew)
            {
                SqlParameter[] sqlParams = new SqlParameter[4];
                sqlParams[0] = new SqlParameter("@vendorId", SqlDbType.Int);
                sqlParams[1] = new SqlParameter("@relative", SqlDbType.NVarChar);
                sqlParams[2] = new SqlParameter("@title", SqlDbType.NVarChar);
                sqlParams[3] = new SqlParameter("@relation", SqlDbType.NVarChar);
                sqlParams[0].Value = Int32.Parse(vendorID);
                sqlParams[1].Value = relative;
                sqlParams[2].Value = title;
                sqlParams[3].Value = relative;

                SqlHelper.ExecuteNonQuery(transact, "s3p_EBid_SaveNewVendorRelative", sqlParams);
            }
            else
            {
                SqlParameter[] sqlParams = new SqlParameter[5];
                sqlParams[0] = new SqlParameter("@vendorId", SqlDbType.Int);
                sqlParams[1] = new SqlParameter("@relativeID", SqlDbType.Int);
                sqlParams[2] = new SqlParameter("@relative", SqlDbType.NVarChar);
                sqlParams[3] = new SqlParameter("@title", SqlDbType.NVarChar);
                sqlParams[4] = new SqlParameter("@relation", SqlDbType.NVarChar);
                sqlParams[0].Value = Int32.Parse(vendorID);
                sqlParams[1].Value = Int32.Parse(relativeID);
                sqlParams[2].Value = relative;
                sqlParams[3].Value = title;
                sqlParams[4].Value = relative;

                SqlHelper.ExecuteNonQuery(transact, "s3p_EBid_UpdateVendorRelative", sqlParams);
            }
        }

        public void UpdateReference1(string vendorID, string referenceNo, string companyName, string avemonthly, bool isNew)
        {
            if (isNew)
            {
                SqlParameter[] sqlParams = new SqlParameter[4];
                sqlParams[0] = new SqlParameter("@vendorId", SqlDbType.Int);
                sqlParams[1] = new SqlParameter("@refType", SqlDbType.Int);
                sqlParams[2] = new SqlParameter("@companyName", SqlDbType.NVarChar);
                sqlParams[3] = new SqlParameter("@avemonthly", SqlDbType.NVarChar);
                sqlParams[0].Value = Int32.Parse(vendorID);
                sqlParams[1].Value = Constant.REF_MAIN_CUSTOMERS;
                sqlParams[2].Value = companyName;
                sqlParams[3].Value = avemonthly;

                SqlHelper.ExecuteNonQuery(connstring, CommandType.StoredProcedure, "s3p_EBid_SaveNewVendorRef1", sqlParams);
            }
            else
            {
                SqlParameter[] sqlParams = new SqlParameter[5];
                sqlParams[0] = new SqlParameter("@vendorId", SqlDbType.Int);
                sqlParams[1] = new SqlParameter("@referenceNo", SqlDbType.Int);
                sqlParams[2] = new SqlParameter("@refType", SqlDbType.Int);
                sqlParams[3] = new SqlParameter("@companyName", SqlDbType.NVarChar);
                sqlParams[4] = new SqlParameter("@avemonthly", SqlDbType.NVarChar);
                sqlParams[0].Value = Int32.Parse(vendorID);
                sqlParams[1].Value = Int32.Parse(referenceNo);
                sqlParams[2].Value = Constant.REF_MAIN_CUSTOMERS;
                sqlParams[3].Value = companyName;
                sqlParams[4].Value = avemonthly;

                SqlHelper.ExecuteNonQuery(connstring, CommandType.StoredProcedure, "s3p_EBid_UpdateVendorRef1", sqlParams);
            }
        }

        public void UpdateReference1(string vendorID, string referenceNo, string companyName, string avemonthly, bool isNew, SqlTransaction transact)
        {
            if (isNew)
            {
                SqlParameter[] sqlParams = new SqlParameter[4];
                sqlParams[0] = new SqlParameter("@vendorId", SqlDbType.Int);
                sqlParams[1] = new SqlParameter("@refType", SqlDbType.Int);
                sqlParams[2] = new SqlParameter("@companyName", SqlDbType.NVarChar);
                sqlParams[3] = new SqlParameter("@avemonthly", SqlDbType.NVarChar);
                sqlParams[0].Value = Int32.Parse(vendorID);
                sqlParams[1].Value = Constant.REF_MAIN_CUSTOMERS;
                sqlParams[2].Value = companyName;
                sqlParams[3].Value = avemonthly;

                SqlHelper.ExecuteNonQuery(transact, "s3p_EBid_SaveNewVendorRef1", sqlParams);
            }
            else
            {
                SqlParameter[] sqlParams = new SqlParameter[5];
                sqlParams[0] = new SqlParameter("@vendorId", SqlDbType.Int);
                sqlParams[1] = new SqlParameter("@referenceNo", SqlDbType.Int);
                sqlParams[2] = new SqlParameter("@refType", SqlDbType.Int);
                sqlParams[3] = new SqlParameter("@companyName", SqlDbType.NVarChar);
                sqlParams[4] = new SqlParameter("@avemonthly", SqlDbType.NVarChar);
                sqlParams[0].Value = Int32.Parse(vendorID);
                sqlParams[1].Value = Int32.Parse(referenceNo);
                sqlParams[2].Value = Constant.REF_MAIN_CUSTOMERS;
                sqlParams[3].Value = companyName;
                sqlParams[4].Value = avemonthly;

                SqlHelper.ExecuteNonQuery(transact, "s3p_EBid_UpdateVendorRef1", sqlParams);
            }
        }

        public void UpdateReference2(string vendorID, string referenceNo, string companyName, string creditline, bool isNew)
        {
            if (isNew)
            {
                SqlParameter[] sqlParams = new SqlParameter[4];
                sqlParams[0] = new SqlParameter("@vendorId", SqlDbType.Int);
                sqlParams[1] = new SqlParameter("@refType", SqlDbType.Int);
                sqlParams[2] = new SqlParameter("@companyName", SqlDbType.NVarChar);
                sqlParams[3] = new SqlParameter("@creditline", SqlDbType.NVarChar);
                sqlParams[0].Value = Int32.Parse(vendorID);
                sqlParams[1].Value = Constant.REF_BANKS;
                sqlParams[2].Value = companyName;
                sqlParams[3].Value = creditline;

                SqlHelper.ExecuteNonQuery(connstring, CommandType.StoredProcedure, "s3p_EBid_SaveNewVendorRef2", sqlParams);
            }
            else
            {
                SqlParameter[] sqlParams = new SqlParameter[5];
                sqlParams[0] = new SqlParameter("@vendorId", SqlDbType.Int);
                sqlParams[1] = new SqlParameter("@referenceNo", SqlDbType.Int);
                sqlParams[2] = new SqlParameter("@refType", SqlDbType.Int);
                sqlParams[3] = new SqlParameter("@companyName", SqlDbType.NVarChar);
                sqlParams[4] = new SqlParameter("@creditline", SqlDbType.NVarChar);
                sqlParams[0].Value = Int32.Parse(vendorID);
                sqlParams[1].Value = Int32.Parse(referenceNo);
                sqlParams[2].Value = Constant.REF_BANKS;
                sqlParams[3].Value = companyName;
                sqlParams[4].Value = creditline;

                SqlHelper.ExecuteNonQuery(connstring, CommandType.StoredProcedure, "s3p_EBid_UpdateVendorRef2", sqlParams);
            }
        }

        public void UpdateReference2(string vendorID, string referenceNo, string companyName, string creditline, bool isNew, SqlTransaction transact)
        {
            if (isNew)
            {
                SqlParameter[] sqlParams = new SqlParameter[4];
                sqlParams[0] = new SqlParameter("@vendorId", SqlDbType.Int);
                sqlParams[1] = new SqlParameter("@refType", SqlDbType.Int);
                sqlParams[2] = new SqlParameter("@companyName", SqlDbType.NVarChar);
                sqlParams[3] = new SqlParameter("@creditline", SqlDbType.NVarChar);
                sqlParams[0].Value = Int32.Parse(vendorID);
                sqlParams[1].Value = Constant.REF_BANKS;
                sqlParams[2].Value = companyName;
                sqlParams[3].Value = creditline;

                SqlHelper.ExecuteNonQuery(transact, "s3p_EBid_SaveNewVendorRef2", sqlParams);
            }
            else
            {
                SqlParameter[] sqlParams = new SqlParameter[5];
                sqlParams[0] = new SqlParameter("@vendorId", SqlDbType.Int);
                sqlParams[1] = new SqlParameter("@referenceNo", SqlDbType.Int);
                sqlParams[2] = new SqlParameter("@refType", SqlDbType.Int);
                sqlParams[3] = new SqlParameter("@companyName", SqlDbType.NVarChar);
                sqlParams[4] = new SqlParameter("@creditline", SqlDbType.NVarChar);
                sqlParams[0].Value = Int32.Parse(vendorID);
                sqlParams[1].Value = Int32.Parse(referenceNo);
                sqlParams[2].Value = Constant.REF_BANKS;
                sqlParams[3].Value = companyName;
                sqlParams[4].Value = creditline;

                SqlHelper.ExecuteNonQuery(transact, "s3p_EBid_UpdateVendorRef2", sqlParams);
            }
        }

        public void UpdateReference3(string vendorID, string referenceNo, string companyName, string kindofbus, bool isNew)
        {
            if (isNew)
            {
                SqlParameter[] sqlParams = new SqlParameter[4];
                sqlParams[0] = new SqlParameter("@vendorId", SqlDbType.Int);
                sqlParams[1] = new SqlParameter("@refType", SqlDbType.Int);
                sqlParams[2] = new SqlParameter("@companyName", SqlDbType.NVarChar);
                sqlParams[3] = new SqlParameter("@kindOfBus", SqlDbType.NVarChar);
                sqlParams[0].Value = Int32.Parse(vendorID);
                sqlParams[1].Value = Constant.REF_AFFILIATE;
                sqlParams[2].Value = companyName;
                sqlParams[3].Value = kindofbus;

                SqlHelper.ExecuteNonQuery(connstring, CommandType.StoredProcedure, "s3p_EBid_SaveNewVendorRef3", sqlParams);
            }
            else
            {
                SqlParameter[] sqlParams = new SqlParameter[5];
                sqlParams[0] = new SqlParameter("@vendorId", SqlDbType.Int);
                sqlParams[1] = new SqlParameter("@referenceNo", SqlDbType.Int);
                sqlParams[2] = new SqlParameter("@refType", SqlDbType.Int);
                sqlParams[3] = new SqlParameter("@companyName", SqlDbType.NVarChar);
                sqlParams[4] = new SqlParameter("@kindOfBus", SqlDbType.NVarChar);
                sqlParams[0].Value = Int32.Parse(vendorID);
                sqlParams[1].Value = Int32.Parse(referenceNo);
                sqlParams[2].Value = Constant.REF_AFFILIATE;
                sqlParams[3].Value = companyName;
                sqlParams[4].Value = kindofbus;

                SqlHelper.ExecuteNonQuery(connstring, CommandType.StoredProcedure, "s3p_EBid_UpdateVendorRef3", sqlParams);
            }
        }

        public void UpdateReference3(string vendorID, string referenceNo, string companyName, string kindofbus, bool isNew, SqlTransaction transact)
        {
            if (isNew)
            {
                SqlParameter[] sqlParams = new SqlParameter[4];
                sqlParams[0] = new SqlParameter("@vendorId", SqlDbType.Int);
                sqlParams[1] = new SqlParameter("@refType", SqlDbType.Int);
                sqlParams[2] = new SqlParameter("@companyName", SqlDbType.NVarChar);
                sqlParams[3] = new SqlParameter("@kindOfBus", SqlDbType.NVarChar);
                sqlParams[0].Value = Int32.Parse(vendorID);
                sqlParams[1].Value = Constant.REF_AFFILIATE;
                sqlParams[2].Value = companyName;
                sqlParams[3].Value = kindofbus;

                SqlHelper.ExecuteNonQuery(transact, "s3p_EBid_SaveNewVendorRef3", sqlParams);
            }
            else
            {
                SqlParameter[] sqlParams = new SqlParameter[5];
                sqlParams[0] = new SqlParameter("@vendorId", SqlDbType.Int);
                sqlParams[1] = new SqlParameter("@referenceNo", SqlDbType.Int);
                sqlParams[2] = new SqlParameter("@refType", SqlDbType.Int);
                sqlParams[3] = new SqlParameter("@companyName", SqlDbType.NVarChar);
                sqlParams[4] = new SqlParameter("@kindOfBus", SqlDbType.NVarChar);
                sqlParams[0].Value = Int32.Parse(vendorID);
                sqlParams[1].Value = Int32.Parse(referenceNo);
                sqlParams[2].Value = Constant.REF_AFFILIATE;
                sqlParams[3].Value = companyName;
                sqlParams[4].Value = kindofbus;

                SqlHelper.ExecuteNonQuery(transact, "s3p_EBid_UpdateVendorRef3", sqlParams);
            }
        }

        public void UpdateReference4(string vendorID, string referenceNo, string companyName, string legalcouns, bool isNew)
        {
            if (isNew)
            {
                SqlParameter[] sqlParams = new SqlParameter[4];
                sqlParams[0] = new SqlParameter("@vendorId", SqlDbType.Int);
                sqlParams[1] = new SqlParameter("@refType", SqlDbType.Int);
                sqlParams[2] = new SqlParameter("@companyName", SqlDbType.NVarChar);
                sqlParams[3] = new SqlParameter("@counsel", SqlDbType.NVarChar);
                sqlParams[0].Value = Int32.Parse(vendorID);
                sqlParams[1].Value = Constant.REF_EXTERN_AUDITOR;
                sqlParams[2].Value = companyName;
                sqlParams[3].Value = legalcouns;

                SqlHelper.ExecuteNonQuery(connstring, CommandType.StoredProcedure, "s3p_EBid_SaveNewVendorRef4", sqlParams);
            }
            else
            {
                SqlParameter[] sqlParams = new SqlParameter[5];
                sqlParams[0] = new SqlParameter("@vendorId", SqlDbType.Int);
                sqlParams[1] = new SqlParameter("@referenceNo", SqlDbType.Int);
                sqlParams[2] = new SqlParameter("@refType", SqlDbType.Int);
                sqlParams[3] = new SqlParameter("@companyName", SqlDbType.NVarChar);
                sqlParams[4] = new SqlParameter("@counsel", SqlDbType.NVarChar);
                sqlParams[0].Value = Int32.Parse(vendorID);
                sqlParams[1].Value = Int32.Parse(referenceNo);
                sqlParams[2].Value = Constant.REF_EXTERN_AUDITOR;
                sqlParams[3].Value = companyName;
                sqlParams[4].Value = legalcouns;

                SqlHelper.ExecuteNonQuery(connstring, CommandType.StoredProcedure, "s3p_EBid_UpdateVendorRef4", sqlParams);
            }
        }

        public void UpdateReference4(string vendorID, string referenceNo, string companyName, string legalcouns, bool isNew, SqlTransaction transact)
        {
            if (isNew)
            {
                SqlParameter[] sqlParams = new SqlParameter[4];
                sqlParams[0] = new SqlParameter("@vendorId", SqlDbType.Int);
                sqlParams[1] = new SqlParameter("@refType", SqlDbType.Int);
                sqlParams[2] = new SqlParameter("@companyName", SqlDbType.NVarChar);
                sqlParams[3] = new SqlParameter("@counsel", SqlDbType.NVarChar);
                sqlParams[0].Value = Int32.Parse(vendorID);
                sqlParams[1].Value = Constant.REF_EXTERN_AUDITOR;
                sqlParams[2].Value = companyName;
                sqlParams[3].Value = legalcouns;

                SqlHelper.ExecuteNonQuery(transact, "s3p_EBid_SaveNewVendorRef4", sqlParams);
            }
            else
            {
                SqlParameter[] sqlParams = new SqlParameter[5];
                sqlParams[0] = new SqlParameter("@vendorId", SqlDbType.Int);
                sqlParams[1] = new SqlParameter("@referenceNo", SqlDbType.Int);
                sqlParams[2] = new SqlParameter("@refType", SqlDbType.Int);
                sqlParams[3] = new SqlParameter("@companyName", SqlDbType.NVarChar);
                sqlParams[4] = new SqlParameter("@counsel", SqlDbType.NVarChar);
                sqlParams[0].Value = Int32.Parse(vendorID);
                sqlParams[1].Value = Int32.Parse(referenceNo);
                sqlParams[2].Value = Constant.REF_EXTERN_AUDITOR;
                sqlParams[3].Value = companyName;
                sqlParams[4].Value = legalcouns;

                SqlHelper.ExecuteNonQuery(transact, "s3p_EBid_UpdateVendorRef4", sqlParams);
            }
        }

        public void DeletePresentSvc(string presentSvcId)
        {
            SqlParameter[] sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@refId", SqlDbType.Int);
            sqlParams[0].Value = Int32.Parse(presentSvcId);

            SqlHelper.ExecuteNonQuery(connstring, CommandType.StoredProcedure, "s3p_EBid_DeletePresentSvc", sqlParams);
        }

        public void DeleteReference(string referenceNo)
        {
            SqlParameter[] sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@refId", SqlDbType.Int);
            sqlParams[0].Value = Int32.Parse(referenceNo);

            SqlHelper.ExecuteNonQuery(connstring, CommandType.StoredProcedure, "s3p_EBid_DeleteReference", sqlParams);
        }

        public void DeleteEquipment(string equipmentID)
        {
            SqlParameter[] sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@refId", SqlDbType.Int);
            sqlParams[0].Value = Int32.Parse(equipmentID);

            SqlHelper.ExecuteNonQuery(connstring, CommandType.StoredProcedure, "s3p_EBid_DeleteVendorEquipment", sqlParams);
        }

        public void DeleteRelative(string relativeId)
        {
            SqlParameter[] sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@refId", SqlDbType.Int);
            sqlParams[0].Value = Int32.Parse(relativeId);

            SqlHelper.ExecuteNonQuery(connstring, CommandType.StoredProcedure, "s3p_EBid_DeleteVendorRelative", sqlParams);
        }


        public DataTable GetCompanies()
        {
            DataSet ds = new DataSet();
            ds= SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "[s3p_EBid_GetCompanies]");
            DataTable dt = new DataTable();
            if (ds.Tables.Count > 0)
                dt = ds.Tables[0];
            return dt;
        }

        public DataTable QueryAllPCABClass()
        {
            DataSet ds = new DataSet();
            ds = SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "[s3p_EBid_GetPCABClass]");
            DataTable dt = new DataTable();
            if (ds.Tables.Count > 0)
                dt = ds.Tables[0];
            return dt;
        }
    }
}
