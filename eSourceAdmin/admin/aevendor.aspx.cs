using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text.RegularExpressions;
using System.Text;
using EBid.lib;
using EBid.lib.constant;
using EBid.lib.utils;
using EBid.lib.bid.data;
using EBid.lib.bid.trans;
using System.Diagnostics;

public partial class admin_aevendor : System.Web.UI.Page
{
    private string connstring;
        
    protected void Page_Load(object sender, EventArgs e)
    {
        FormsAuthenticationHelper.AuthenticateUserWithReturnUrl();
        connstring = HttpContext.Current.Session["ConnectionString"].ToString();

        if (Session["AddVendorMessage"] != null)
        {
            lblMessage.Visible = true;
            lblMessage.Text = Session["AddVendorMessage"].ToString();
            Session["AddVendorMessage"] = null;
        }
        else
            lblMessage.Visible = false;

        if (!Page.IsPostBack)
        {
            #region Initialization
            ddlUserTypes.SelectedIndex = 1;
            
            if (AddOrEdit == "Update")
            {
                litHeader.Text = "Edit Vendor Information";
                if (!String.IsNullOrEmpty(Request.QueryString["vid"]))
                {                    
                    InitDeletedIDContainer();
                    InitializeEditVendor(Int32.Parse(Request.QueryString["vid"]));
                }
                ddlUserTypes.Visible = false;
            }
            else
            {
                litHeader.Text = "Add New Vendor";
                InitializeAddVendor();
                ddlUserTypes.Visible = true;
            }

            #endregion
            
            Wizard1.ActiveStepIndex = 0;                      
        }
        else
        {
            // Reload categories and subcategories
            ReloadCategoryAndSubCategories();
            // Reload brands
            ReloadBrands();
            // Reload items
            ReloadItems();
            // Reload services
            ReloadServices();
            // Reload locations
            ReloadLocations();

            if ((ViewState["PCABClass"] != null) && (Wizard1.ActiveStepIndex == 1))
            {
                ddlPCABClass.SelectedValue = ViewState["PCABClass"].ToString();
                ViewState["PCABClass"] = null;
            }
        }        
    }
    
    protected string AddOrEdit
    {
        get
        {
            string actionType = String.IsNullOrEmpty(Request.QueryString["t"]) ? "0" : Request.QueryString["t"].Trim();
            if (actionType == "1")
                return "Update";
            else
                return "Save";
        }
    }    

    #region Loading and Reloading
    private void InitializeAddVendor()
    {
        ddlPCABClass.SelectedIndex = 0;
        ddlSupplierType.SelectedIndex = 0;
        rbOrganizationType.SelectedIndex = 0;
        chkCompanyClassification_4.Checked = true;
        //tbPassword.ReadOnly = false;
        //tbUserName.ReadOnly = false;

        ViewState["dtPresentSvc"] = CreateEmptyPresentServices();
        LoadPresentSvcData();

        // Equipments
        ViewState["dtEquipments"] = CreateEmptyEquipment();
        LoadEquipmentsData();

        // Relatives
        ViewState["dtRelatives"] = CreateEmptyRelativesTable();
        LoadRelativesData();

        // References
        ViewState["MajCustomers"] = CreateEmptyMajorCustomers();
        LoadMajCustomersData();

        ViewState["Banks"] = CreateEmptyBanks();
        LoadBanksData();

        ViewState["AffCompany"] = CreateEmptyAffiliatedCompanies();
        LoadAffiliatesData();

        ViewState["ExternAudit"] = CreateEmptyExternalAuditor();
        LoadExtAuditorsData();

        // Load client scripts
        LoadScripts();
    }
	
	private bool FillVendorAdditional(int vendorId)
    {
        SqlParameter[] sqlParams = new SqlParameter[2];
        sqlParams[0] = new SqlParameter("@VendorId", SqlDbType.Int);
        sqlParams[0].Value = vendorId;

        DataTable dtVendorData = SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "s3p_EBid_QueryVendor_Addl_Info", sqlParams).Tables[0];

        if (dtVendorData.Rows.Count > 0)
        {
			tbVendorCode.Text = dtVendorData.Rows[0]["Vendor_Code"].ToString().Trim();
            tbSLASIRDate.Text = dtVendorData.Rows[0]["SLA_SIR_Date"].ToString()!="" ? Convert.ToDateTime(dtVendorData.Rows[0]["SLA_SIR_Date"].ToString().Trim()).ToShortDateString().ToString() : "";
            tbSLADateApproved.Text = dtVendorData.Rows[0]["SLA_Date_Approved"].ToString()!="" ? Convert.ToDateTime(dtVendorData.Rows[0]["SLA_Date_Approved"].ToString().Trim()).ToShortDateString().ToString():"";            
            tbSLAAccredited.Text = dtVendorData.Rows[0]["Accreditation_Duration"].ToString()!="" ? Convert.ToDateTime(dtVendorData.Rows[0]["Accreditation_Duration"].ToString().Trim()).ToShortDateString().ToString():"";
			tbAccrFrom.Text = dtVendorData.Rows[0]["Accreditation_From"].ToString()!="" ? Convert.ToDateTime(dtVendorData.Rows[0]["Accreditation_From"].ToString().Trim()).ToShortDateString().ToString():"";
			tbAccrTo.Text = dtVendorData.Rows[0]["Accreditation_To"].ToString()!="" ? Convert.ToDateTime(dtVendorData.Rows[0]["Accreditation_To"].ToString().Trim()).ToShortDateString().ToString() : "";
			tbPerfDate.Text = dtVendorData.Rows[0]["Perf_Evaluation_Date"].ToString()!="" ? Convert.ToDateTime(dtVendorData.Rows[0]["Perf_Evaluation_Date"].ToString().Trim()).ToShortDateString().ToString() : "";
			tbPerfRate.Text = dtVendorData.Rows[0]["Perf_Evaluation_Rate"] == null ? "" : dtVendorData.Rows[0]["Perf_Evaluation_Rate"].ToString().Trim();
			tbCompSIRDate.Text = dtVendorData.Rows[0]["Composite_Rating_SIR_Date"].ToString()!="" ? Convert.ToDateTime(dtVendorData.Rows[0]["Composite_Rating_SIR_Date"].ToString().Trim()).ToShortDateString().ToString() : "";
			tbCompRate.Text = dtVendorData.Rows[0]["Composite_Rating_Rate"] == null ? "" : dtVendorData.Rows[0]["Composite_Rating_Rate"].ToString().Trim();			 
			tbMaxSIRDate.Text = dtVendorData.Rows[0]["Maximum_Exposure_SIR_Date"].ToString()!="" ? Convert.ToDateTime(dtVendorData.Rows[0]["Maximum_Exposure_SIR_Date"].ToString().Trim()).ToShortDateString().ToString():"";
			tbMaxRate.Text = dtVendorData.Rows[0]["Maximum_Exposure_Amount"] == null ? "" : dtVendorData.Rows[0]["Maximum_Exposure_Amount"].ToString().Trim();
			tbEnrollmentDate.Text = dtVendorData.Rows[0]["Enrollment_Date"].ToString()!="" ? Convert.ToDateTime(dtVendorData.Rows[0]["Enrollment_Date"].ToString().Trim()).ToShortDateString().ToString():"";
			tbIRDate.Text = dtVendorData.Rows[0]["IR_Date"].ToString()!="" ? Convert.ToDateTime(dtVendorData.Rows[0]["IR_Date"].ToString().Trim()).ToShortDateString().ToString():"";
			tbIRNumber.Text = dtVendorData.Rows[0]["IR_Number"] == null ? "" : dtVendorData.Rows[0]["IR_Number"].ToString().Trim();
			tbIRDescription.Text = dtVendorData.Rows[0]["IR_Description"] == null ? "" : dtVendorData.Rows[0]["IR_Description"].ToString().Trim();
			/*
			if (dtVendorData.Rows[0]["Exempted"].ToString() == "Y")
				chkAccreditationStatus.Checked = true;
			else
				chkAccreditationStatus.Checked = false;
			*/
            return true;
        }
        return false;
    }
	
    private void InitializeEditVendor(int vendorId)
    {
        //tbPassword.ReadOnly = true;
        //tbUserName.ReadOnly = true;
        FillVendorInfo(vendorId);
        FillVendorDetails(vendorId);
        FillVendorCategories(vendorId);
        FillListBox(vendorId);
        FillTables(vendorId);
		FillVendorAdditional(vendorId);
        // Load client scripts
        LoadScripts();
    }

    private void LoadScripts()
    {
        tbTelephone.Attributes.Add("onkeypress", "return PhoneAndNoValidator(this)");
        tbBranchExtension.Attributes.Add("onkeypress", "return NumberOnlyValidator(this)");
        tbBranchFax.Attributes.Add("onkeypress", "return PhoneAndNoValidator(this)");
        tbBranchPhone.Attributes.Add("onkeypress", "return PhoneAndNoValidator(this)");
        tbExtension.Attributes.Add("onkeypress", "return NumberOnlyValidator(this)");
        tbFax.Attributes.Add("onkeypress", "return PhoneAndNoValidator(this)");
        tbMinOrderValue.Attributes.Add("onkeypress", "return(currencyFormat(this,'','.',event))");
        tbOwnershipFilipino.Attributes.Add("onkeypress", "return NumberOnlyValidator(this)");
        tbOwnershipFilipino.Attributes.Add("onblur", "ReturnAverage('tbOwnershipFilipino','tbOwnershipOther')");
        tbOwnershipOther.Attributes.Add("onkeypress", "return NumberOnlyValidator(this)");
        tbOwnershipOther.Attributes.Add("onblur", "ReturnAverage('tbOwnershipOther','tbOwnershipFilipino')");
        tbPostalCode.Attributes.Add("onkeypress", "return NumberOnlyValidator(this)");
        tbSalesPersonPhone.Attributes.Add("onkeypress", "return PhoneAndNoValidator(this)");
        tbTIN.Attributes.Add("onkeypress", "return PhoneAndNoValidator(this)");
        tbVatRegNo.Attributes.Add("onkeypress", "return PhoneAndNoValidator(this)");

        // Disallow spaces
        tbUserName.Attributes.Add("onkeypress", "return NoSpaces(event);");        
        tbEmail.Attributes.Add("onkeypress", "return NoSpaces(event);");
        
        // For categories
        btnSelectCategory.Attributes.Add("onclick", "copyToCategoryList();");
        btnSelectAllCategories.Attributes.Add("onclick", "copyAllCategories();");
        btnDeSelectCategory.Attributes.Add("onclick", "removeAllCategories();");
        btnDeSelectAllCategories.Attributes.Add("onclick", "removeFromCategoryList();");
        lstCategories.Attributes.Add("ondblclick", "copyACategory();");
        lstSelectedCategories.Attributes.Add("ondblclick", "removeACategory();");

        btnSelectSubCategory.Attributes.Add("onClick", "copyToSubCategoryList();");
        btnSelectAllSubCategories.Attributes.Add("onClick", "copyAllSubCategories();");
        btnDeSelectAllSubCategories.Attributes.Add("onClick", "removeAllSubCategories();");
        btnDeSelectSubCategory.Attributes.Add("onClick", "removeFromSubCategoryList();");
        lstSubCategory.Attributes.Add("onDblClick", "copyASubCategory();");
        lstSelectedSubCategories.Attributes.Add("onDblClick", "removeASubCategory();");
    }

    private void ReloadCategoryAndSubCategories()
    {
        string category = "";
        if (hdnCategories.Text.Trim() != "")
        {
            SupplierTransaction su = new SupplierTransaction();
            category = hdnCategories.Text.Trim();
            string[] categoryA = category.Split(Convert.ToChar(","));
            category = "";
            foreach (string cat in categoryA)
            {
                if (category == "")
                    category = "'" + cat + "'";
                else
                    category = category + ",'" + cat + "'";
            }
            DataSet ds = su.QuerySelectedAndUnselectedCategories(category.Trim());

            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0] != null)
                {
                    DataTable selectedcategories = ds.Tables[0];
                    lstCategories.DataSourceID = "";
                    lstSelectedCategories.DataSource = selectedcategories;
                    lstSelectedCategories.DataTextField = "CategoryName";
                    lstSelectedCategories.DataValueField = "CategoryId";
                    lstSelectedCategories.DataBind();
                }
                if (ds.Tables[1] != null)
                {
                    DataTable availcategories = ds.Tables[1];
                    lstCategories.DataSourceID = "";
                    lstCategories.DataSource = availcategories;
                    lstCategories.DataTextField = "CategoryName";
                    lstCategories.DataValueField = "CategoryId";
                    lstCategories.DataBind();
                }
            }
        }
        if (hdnSubCategory.Text.Trim() != "")
        {
            SupplierTransaction su = new SupplierTransaction();
            DataSet ds1 = su.QuerySelectedAndUnselectedSubCategories(hdnSubCategory.Text.Trim(), category.Trim(), connstring);
            if (ds1.Tables.Count > 0)
            {
                if (ds1.Tables[0] != null)
                {
                    DataTable selectedsubcategories = ds1.Tables[0];
                    lstSelectedSubCategories.DataSourceID = "";
                    lstSelectedSubCategories.DataSource = selectedsubcategories;
                    lstSelectedSubCategories.DataTextField = "SubCategoryName";
                    lstSelectedSubCategories.DataValueField = "SubCategoryId";
                    lstSelectedSubCategories.DataBind();
                }
                if (ds1.Tables[1] != null)
                {
                    DataTable availsubcategories = ds1.Tables[1];
                    lstSelectedSubCategories.DataSourceID = "";
                    lstSubCategory.DataSource = availsubcategories;
                    lstSubCategory.DataTextField = "SubCategoryName";
                    lstSubCategory.DataValueField = "SubCategoryId";
                    lstSubCategory.DataBind();
                }
            }
        }
    }

    private void ReloadBrands()
    {
        // transfer first all items in the right to left
        foreach (ListItem li in lbBrandsSelected.Items)
        {
            lbBrandsCarried.Items.Add(li);
        }
        lbBrandsSelected.Items.Clear();

        ArrayList toBeRemoved = new ArrayList();
        if (!String.IsNullOrEmpty(hdnBrands.Value))
        {   
            string[] hBrands = hdnBrands.Value.Split(new char[]{','}, StringSplitOptions.RemoveEmptyEntries);            

            foreach(ListItem lbc in lbBrandsCarried.Items)
            {
                for (int j = 0; j < hBrands.Length; j++)
                {
                    // transfer brands if previously selected
                    if (lbc.Value.Trim() == hBrands[j].Trim())
                    {                        
                        lbBrandsSelected.Items.Add(new ListItem(lbc.Text, lbc.Value));
                        toBeRemoved.Add(lbc);
                    }
                }
            }            
        }
        foreach (Object obj in toBeRemoved)
        {
            ListItem li = (ListItem)obj;
            lbBrandsCarried.Items.Remove(li);
        }
    }

    private void ReloadItems()
    {
        // transfer first all items in the right to left
        foreach (ListItem li in lbItemSelected.Items)
        {
            lbItemCarried.Items.Add(li);
        }
        lbItemSelected.Items.Clear();

        if (!String.IsNullOrEmpty(hdnItems.Value))
        {
            string[] hItems = hdnItems.Value.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            
            ArrayList toBeRemoved = new ArrayList();
            foreach (ListItem lbc in lbItemCarried.Items)
            {
                for (int j = 0; j < hItems.Length; j++)
                {
                    // transfer items if previously selected
                    if (lbc.Value.Trim() == hItems[j].Trim())
                    {                        
                        lbItemSelected.Items.Add(new ListItem(lbc.Text, lbc.Value));
                        toBeRemoved.Add(lbc);
                    }                    
                }
            }
            foreach (Object obj in toBeRemoved)
            {
                ListItem li = (ListItem)obj;
                lbItemCarried.Items.Remove(li);
            }
        }
    }

    private void ReloadServices()
    {
        // transfer first all items in the right to left
        foreach (ListItem li in lbSelectedService.Items)
        {
            lbServices.Items.Add(li);
        }
        lbSelectedService.Items.Clear();

        if (!String.IsNullOrEmpty(hdnServices.Value)) 
        {
            string[] hServices = hdnServices.Value.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            
            ArrayList toBeRemoved = new ArrayList();
            foreach (ListItem lbc in lbServices.Items)
            {
                for (int j = 0; j < hServices.Length; j++)
                {
                    // transfer serveices if previously selected
                    if (lbc.Value.Trim() == hServices[j].Trim())
                    {
                        lbSelectedService.Items.Add(new ListItem(lbc.Text, lbc.Value));
                        toBeRemoved.Add(lbc);
                    }
                }
            }
            foreach (Object obj in toBeRemoved)
            {
                ListItem li = (ListItem)obj;
                lbServices.Items.Remove(li);
            }
        }
    }

    private void ReloadLocations()
    {
        // transfer first all items in the right to left
        foreach (ListItem li in lbSelectedLocation.Items)
        {
            lbLocations.Items.Add(li);
        }
        lbSelectedLocation.Items.Clear();

        if (!String.IsNullOrEmpty(hdnLocations.Value))
        {
            string[] hLocations = hdnLocations.Value.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            
            ArrayList toBeRemoved = new ArrayList();
            foreach (ListItem lbc in lbLocations.Items)
            {
                for (int j = 0; j < hLocations.Length; j++)
                {
                    // transfer locations if previously selected
                    if (lbc.Value.Trim() == hLocations[j].Trim())
                    {
                        lbSelectedLocation.Items.Add(new ListItem(lbc.Text, lbc.Value));
                        toBeRemoved.Add(lbc);
                    }
                }
            }
            foreach (Object obj in toBeRemoved)
            {
                ListItem li = (ListItem)obj;
                lbLocations.Items.Remove(li);
            }
        }
    }

    // Update Methods
    private bool FillVendorInfo(int vendorId)
    {
        SqlParameter[] sqlParams = new SqlParameter[2];
        sqlParams[0] = new SqlParameter("@userID", SqlDbType.Int);
        sqlParams[1] = new SqlParameter("@userType", SqlDbType.Int);
        sqlParams[0].Value = vendorId;
        sqlParams[1].Value = 2;

        DataTable dtUserData = SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "s3p_EBid_ViewSpecificUser", sqlParams).Tables[0];

        if (dtUserData.Rows.Count > 0)
        {
            ViewState["OLDUSERNAME"] = tbUserName.Text = dtUserData.Rows[0]["UserName"].ToString().Trim();
            ViewState["VendorPwd"] = dtUserData.Rows[0]["UserPassword"].ToString().Trim();            
            ViewState["VendorEmail"]  = tbEmail.Text = dtUserData.Rows[0]["EmailAdd"].ToString().Trim();
            return true;
        }
        return false;
    }

    private void FillVendorDetails(int vendorId)
    {
        CompanyTransaction companyDetails = new CompanyTransaction();
        CompanyDetails companyInfo = companyDetails.QueryCompanyDetails(vendorId.ToString());
        ViewState["CompanyDetails"] = companyInfo;

        tbCompanyName.Text = companyInfo.CompanyName;
        tbHeadOfficeAddress1.Text = companyInfo.Address;
        tbHeadOfficeAddress2.Text = companyInfo.Address1;
        tbTelephone.Text = companyInfo.HeadTelephone;
        tbMobileNo.Text = companyInfo.HeadMobileNo;
        tbFax.Text = companyInfo.HeadFax;
        tbExtension.Text = companyInfo.HeadExtension;
        tbBranchAddress1.Text = companyInfo.Address2;
        tbBranchAddress2.Text = companyInfo.Address3;
        tbBranchPhone.Text = companyInfo.BranchTelephone;
        tbBranchFax.Text = companyInfo.BranchFax;
        tbBranchExtension.Text = companyInfo.BranchExtension;
        tbVatRegNo.Text = companyInfo.VatRegNo;
        tbTIN.Text = companyInfo.TIN;
        tbPOBox.Text = companyInfo.POBox;
        tbPostalCode.Text = companyInfo.PostalCode;
        tbEmail.Text = companyInfo.CompanyEmail;
        tbTermsofPayment.Text = companyInfo.TermsofPayment;
        tbSpecialTerms.Text = companyInfo.SpecialTerms;
        tbMinOrderValue.Text = companyInfo.MinOrderValue;
        ViewState["CompanySalesPerson"]  = tbSalesPerson.Text = companyInfo.SalesPerson;
        tbSalesPersonPhone.Text = companyInfo.SalesPersonPhone;

        if (companyInfo.OrganizationType != 0)
            rbOrganizationType.Items[companyInfo.OrganizationType - 1].Selected = true;
        else
            rbOrganizationType.SelectedIndex = 0;        

        if (companyInfo.Accredited > 0)
            ddlSupplierType.SelectedIndex = companyInfo.Accredited - 1;
        else
            ddlSupplierType.SelectedIndex = 0;

        tbOwnershipFilipino.Text = companyInfo.OwnershipFilipino;
        tbOwnershipOther.Text = companyInfo.OwnershipOther;        

        tbSoleSupplier1.Text = companyInfo.SoleSupplier;
        tbSoleSupplier2.Text = companyInfo.SoleSupplier1;
        tbSpecialization.Text = companyInfo.Specialization;

        tbKeyPersonnel.Text = companyInfo.KeyPersonnel;
        tbKpPosition.Text = companyInfo.KpPosition;

        CheckVendorClassification(vendorId);
        CheckSelectedStandard(companyInfo.ISOStandard.Trim());
        ddlPCABClass.SelectedIndex = companyInfo.PCABClass - 1;
        ViewState["PCABClass"] = companyInfo.PCABClass;
    }

    private void FillVendorCategories(int vendorId)
    {
        SqlParameter[] sqlParam = new SqlParameter[1];
        sqlParam[0] = new SqlParameter("VendorId",vendorId);
        SqlDataReader reader = SqlHelper.ExecuteReader(connstring, "s3p_EBid_GetSelectedCategoryAndSubCategoryForAVendor", sqlParam);
                
        while(reader.Read())
        {
            hdnCategories.Text = reader.IsDBNull(0) ? "" : (string)reader["CategoryIds"];
            hdnSubCategory.Text = reader.IsDBNull(1) ? "" : (string)reader["SubCategoryIds"];
        }
        
        string category = "";
        if (hdnCategories.Text.Trim() != "")
        {
            SupplierTransaction su = new SupplierTransaction();
            category = hdnCategories.Text.Trim();
            string[] categoryA = category.Split(Convert.ToChar(","));
            category = "";
            foreach (string cat in categoryA)
            {
                if (category == "")
                    category = "'" + cat + "'";
                else
                    category = category + ",'" + cat + "'";
            }
            DataSet ds = su.QuerySelectedAndUnselectedCategories(category.Trim());

            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0] != null)
                {
                    DataTable selectedcategories = ds.Tables[0];
                    lstCategories.DataSourceID = "";
                    lstSelectedCategories.DataSource = selectedcategories;
                    lstSelectedCategories.DataTextField = "CategoryName";
                    lstSelectedCategories.DataValueField = "CategoryId";
                    lstSelectedCategories.DataBind();
                }
                if (ds.Tables[1] != null)
                {
                    DataTable availcategories = ds.Tables[1];
                    lstCategories.DataSourceID = "";
                    lstCategories.DataSource = availcategories;
                    lstCategories.DataTextField = "CategoryName";
                    lstCategories.DataValueField = "CategoryId";
                    lstCategories.DataBind();
                }
            }
        }
        if (hdnSubCategory.Text.Trim() != "")
        {
            //Response.Write(hdnSubCategory.Text.Trim() + category.Trim());
            SupplierTransaction su = new SupplierTransaction();
            DataSet ds1 = su.QuerySelectedAndUnselectedSubCategories(hdnSubCategory.Text.Trim(), category.Trim(), connstring);
            if (ds1.Tables.Count > 0)
            {
                if (ds1.Tables[0] != null)
                {
                    DataTable selectedsubcategories = ds1.Tables[0];
                    lstSelectedSubCategories.DataSourceID = "";
                    lstSelectedSubCategories.DataSource = selectedsubcategories;
                    lstSelectedSubCategories.DataTextField = "SubCategoryName";
                    lstSelectedSubCategories.DataValueField = "SubCategoryId";
                    lstSelectedSubCategories.DataBind();
                }
                if (ds1.Tables[1] != null)
                {
                    DataTable availsubcategories = ds1.Tables[1];
                    lstSelectedSubCategories.DataSourceID = "";
                    lstSubCategory.DataSource = availsubcategories;
                    lstSubCategory.DataTextField = "SubCategoryName";
                    lstSubCategory.DataValueField = "SubCategoryId";
                    lstSubCategory.DataBind();
                }
            }
        }
    }

    private void FillListBox(int vendorId)
    {
        CompanyTransaction companyDetails = new CompanyTransaction();

        DataTable dtProductBrands = companyDetails.QueryVendorProdBrands(vendorId.ToString(), false);
        DataView dvProductBrands = new DataView(dtProductBrands);
        lbBrandsCarried.DataSourceID = null;
        lbBrandsCarried.DataSource = dvProductBrands;
        lbBrandsCarried.DataTextField = "BrandName";
        lbBrandsCarried.DataValueField = "BrandId";
        lbBrandsCarried.DataBind();

        dtProductBrands = companyDetails.QueryVendorProdBrands(vendorId.ToString(), true);
        dvProductBrands = new DataView(dtProductBrands);
        lbBrandsSelected.DataSourceID = null;
        lbBrandsSelected.DataSource = dvProductBrands;
        lbBrandsSelected.DataTextField = "BrandName";
        lbBrandsSelected.DataValueField = "BrandId";
        lbBrandsSelected.DataBind();

        if (dtProductBrands.Rows.Count > 0)
        {
            hdnBrands.Value = dtProductBrands.Rows[0]["BrandId"].ToString().Trim();
            for (int i = 1; i < dtProductBrands.Rows.Count; i++)
            {
                hdnBrands.Value += "," + dtProductBrands.Rows[i]["BrandId"].ToString().Trim();
            }
        }

        DataTable dtItemsCarried = companyDetails.QueryVendorItemsCarried(vendorId.ToString(), false);
        DataView dvItemsCarried = new DataView(dtItemsCarried);
        lbItemCarried.DataSourceID = null;
        lbItemCarried.DataSource = dvItemsCarried;
        lbItemCarried.DataTextField = "ItemsCarried";
        lbItemCarried.DataValueField = "ItemNo";
        lbItemCarried.DataBind();

        dtItemsCarried = companyDetails.QueryVendorItemsCarried(vendorId.ToString(), true);
        dvItemsCarried = new DataView(dtItemsCarried);
        lbItemSelected.DataSourceID = null;
        lbItemSelected.DataSource = dvItemsCarried;
        lbItemSelected.DataTextField = "ItemsCarried";
        lbItemSelected.DataValueField = "ItemNo";
        lbItemSelected.DataBind();

        if (dtItemsCarried.Rows.Count > 0)
        {
            hdnItems.Value = dtItemsCarried.Rows[0]["ItemNo"].ToString().Trim();
            for (int i = 1; i < dtItemsCarried.Rows.Count; i++)
            {
                hdnItems.Value += "," + dtItemsCarried.Rows[i]["ItemNo"].ToString().Trim();
            }
        }

        DataTable dtServices = companyDetails.QueryVendorServices(vendorId.ToString(), false);
        DataView dvServices = new DataView(dtServices);
        lbServices.DataSourceID = null;
        lbServices.DataSource = dvServices;
        lbServices.DataTextField = "ServiceName";
        lbServices.DataValueField = "ServiceId";
        lbServices.DataBind();

        dtServices = companyDetails.QueryVendorServices(vendorId.ToString(), true);
        dvServices = new DataView(dtServices);
        lbSelectedService.DataSourceID = null;
        lbSelectedService.DataSource = dvServices;
        lbSelectedService.DataTextField = "ServiceName";
        lbSelectedService.DataValueField = "ServiceId";
        lbSelectedService.DataBind();

        if (dtServices.Rows.Count > 0)
        {
            hdnServices.Value = dtServices.Rows[0]["ServiceId"].ToString().Trim();
            for (int i = 1; i < dtServices.Rows.Count; i++)
            {
                hdnServices.Value += "," + dtServices.Rows[i]["ServiceId"].ToString().Trim();
            }
        }

        DataTable dtLocations = companyDetails.QueryVendorLocations(vendorId.ToString(), false);
        DataView dvLocations = new DataView(dtLocations);
        lbLocations.DataSourceID = null;
        lbLocations.DataSource = dvLocations;
        lbLocations.DataTextField = "LocationName";
        lbLocations.DataValueField = "LocationId";
        lbLocations.DataBind();

        dtLocations = companyDetails.QueryVendorLocations(vendorId.ToString(), true);
        dvLocations = new DataView(dtLocations);
        lbSelectedLocation.DataSourceID = null;
        lbSelectedLocation.DataSource = dvLocations;
        lbSelectedLocation.DataTextField = "LocationName";
        lbSelectedLocation.DataValueField = "LocationId";
        lbSelectedLocation.DataBind();

        if (dtLocations.Rows.Count > 0)
        {
            hdnLocations.Value = dtLocations.Rows[0]["LocationId"].ToString().Trim();
            for (int i = 1; i < dtLocations.Rows.Count; i++)
            {
                hdnLocations.Value += "," + dtLocations.Rows[i]["LocationId"].ToString().Trim();
            }
        }

        //DataTable dtPCABClass = companyDetails.QueryAllPCABClass();
        //DataView dvPCABClass = new DataView(dtPCABClass);
        //ddlPCABClass.DataTextField = "PCAB CLass Name";
        //ddlPCABClass.DataValueField = "PCAB CLass Id";
        //ddlPCABClass.DataSource = dvPCABClass;
        //ddlPCABClass.DataBind();
    }

    private void FillTables(int vendorId)
    {
        CompanyTransaction companyDetails = new CompanyTransaction();

        // Present Services
        ViewState["dtPresentSvc"] = companyDetails.QueryPresentServices(vendorId.ToString());
        // ViewState["dtGlobePlans"] = SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedur, "s3p_EBid_QueryAllGlobePlans").Tables[0];
        LoadPresentSvcData();

        // Equipments
        ViewState["dtEquipments"] = companyDetails.QueryVendorEquipments(vendorId.ToString());
        LoadEquipmentsData();

        // Relatives
        ViewState["dtRelatives"] = companyDetails.QueryVendorRelatives(vendorId.ToString());
        LoadRelativesData();

        // References
        ViewState["MajCustomers"] = companyDetails.QueryVendorReferences(vendorId.ToString(), Constant.REF_MAIN_CUSTOMERS);
        LoadMajCustomersData();

        ViewState["Banks"] = companyDetails.QueryVendorReferences(vendorId.ToString(), Constant.REF_BANKS);
        LoadBanksData();

        ViewState["AffCompany"] = companyDetails.QueryVendorReferences(vendorId.ToString(), Constant.REF_AFFILIATE);
        LoadAffiliatesData();

        ViewState["ExternAudit"] = companyDetails.QueryVendorReferences(vendorId.ToString(), Constant.REF_EXTERN_AUDITOR);
        LoadExtAuditorsData();
    }

    private void CheckVendorClassification(int vendorId)
    {
        SqlParameter[] sqlparams = new SqlParameter[1];
        sqlparams[0] = new SqlParameter("@vendorId", SqlDbType.Int);
        sqlparams[0].Value = vendorId;

        DataTable dtClassifications = SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "s3p_EBid_QueryVendorClassification", sqlparams).Tables[0];

        if (dtClassifications.Rows.Count > 0)
        {
            foreach (DataRow row in dtClassifications.Rows)
            {
                switch (Int32.Parse(row["ClassificationId"].ToString().Trim()))
                {
                    case 1:
                        chkCompanyClassification_0.Checked = true;
                        break;
                    case 2:
                        chkCompanyClassification_1.Checked = true;
                        break;
                    case 3:
                        chkCompanyClassification_2.Checked = true;
                        break;
                    case 4:
                        chkCompanyClassification_3.Checked = true;
                        break;
                    case 5:
                        chkCompanyClassification_4.Checked = true;
                        chkCompanyClassification_0.Disabled = true;
                        chkCompanyClassification_1.Disabled = true;
                        chkCompanyClassification_2.Disabled = true;
                        chkCompanyClassification_3.Disabled = true;
                        break;
                }
            }
        }
        else
        {
            chkCompanyClassification_4.Checked = true;
            chkCompanyClassification_0.Disabled = true;
            chkCompanyClassification_1.Disabled = true;
            chkCompanyClassification_2.Disabled = true;
            chkCompanyClassification_3.Disabled = true;
        }
    }

    private void CheckSelectedStandard(string standard)
    {
        switch (standard)
        {
            case "01": // 9002 is checked
                cb9001.Checked = false;
                cb9002.Checked = true;
                break;
            case "10": // 9001 is checked
                cb9001.Checked = true;
                cb9002.Checked = false;
                break;
            case "11": // both are checked
                cb9001.Checked = true;
                cb9002.Checked = true;
                break;
            default:
                cb9001.Checked = false;
                cb9002.Checked = false;
                break;
        }
    }
    #endregion
    
    #region Checkings    
    
    protected void CheckUsernameAvailability(object source, ServerValidateEventArgs args)
    {
        SupplierTransaction st = new SupplierTransaction();
        if (AddOrEdit == "Save")
        {
            args.IsValid = st.CheckUser(args.Value.Trim());
        }
        else
        {
            args.IsValid = st.CheckUserExcept(args.Value.Trim(), ViewState["OLDUSERNAME"].ToString());
        }
    }

    protected void CheckMobileNo(object source, ServerValidateEventArgs args)
    {
        args.IsValid = SMSHelper.AreValidMobileNumbers(tbMobileNo.Text.ToString().Trim());
    }

    protected void CheckIfFloat(object source, ServerValidateEventArgs args)
    {
        string pattern = @"^[0-9]*.?[0-9]{0,2}$";

        args.IsValid = (PasswordChecker.IsMatch(args.Value.Trim(), pattern, RegexOptions.IgnorePatternWhitespace));
    }
    #endregion

    #region Services methods
    protected void lnkAddServices_Click(object sender, EventArgs e)
    {
        DataTable presentsvc = (DataTable)ViewState["dtPresentSvc"];
        UpdateSVCDataRow();

        if ((presentsvc.Rows[presentsvc.Rows.Count - 1]["AccountNo"].ToString().Trim().Length > 0) &&
            (presentsvc.Rows[presentsvc.Rows.Count - 1]["CreditLimit"].ToString().Trim().Length > 0))
        {
            AddEmptySvcRow(ref presentsvc);
            LoadPresentSvcData();
        }
    }

    private void LoadPresentSvcData()
    {
        DataTable presentsvc = (DataTable)ViewState["dtPresentSvc"];

        if (presentsvc.Rows.Count == 0)
        {
            AddEmptySvcRow(ref presentsvc);
        }

        UpdateIndexColumn(ref presentsvc);
        DataView dvPresentSvc = new DataView(presentsvc);

        gvPresentSvc.DataSource = dvPresentSvc;
        gvPresentSvc.DataBind();

        ViewState["dtPresentSvc"] = presentsvc;
    }

    private DataTable CreateEmptyPresentServices()
    {
        DataTable dt = new DataTable();
        DataColumn dcol = new DataColumn("PlanID", typeof(System.String));
        dt.Columns.Add(dcol);
        dcol = new DataColumn("AccountNo", typeof(System.String));
        dt.Columns.Add(dcol);
        dcol = new DataColumn("CreditLimit", typeof(System.String));
        dt.Columns.Add(dcol);

        DataRow dr = dt.NewRow();
        dr["PlanID"] = "";
        dr["AccountNo"] = "";
        dr["CreditLimit"] = "";
        dt.Rows.Add(dr);

        return dt;
    }

    private void AddEmptySvcRow(ref DataTable dt)
    {
        if (dt != null)
        {
            DataRow drEmpty = dt.NewRow();

            drEmpty["PlanID"] = System.DBNull.Value;
            drEmpty["AccountNo"] = System.DBNull.Value;
            drEmpty["CreditLimit"] = System.DBNull.Value;
            dt.Rows.Add(drEmpty);
        }
    }

    private void UpdateSVCDataRow()
    {
        if (ViewState["dtPresentSvc"] != null)
        {
            DataTable presentsvc = (DataTable)ViewState["dtPresentSvc"];

            if (gvPresentSvc.Rows.Count > 0)
            {
                HiddenField hdnPlanID = null;
                DropDownList ddList = null;
                TextBox lblAcctNo = null;
                TextBox lblCreditLine = null;

                int i = 0;
                foreach (GridViewRow row in gvPresentSvc.Rows)
                {
                    //row.BeginEdit();
                    ddList = (DropDownList)row.FindControl("ddlPlans");

                    hdnPlanID = (HiddenField)row.FindControl("hdPlanID");
                    lblAcctNo = (TextBox)row.FindControl("lblAcctNo");
                    lblCreditLine = (TextBox)row.FindControl("lblCreditLimit");

                    presentsvc.Rows[i].BeginEdit();

                    try
                    {
                        presentsvc.Rows[i]["PlanID"] = ddList.SelectedValue;
                    }
                    catch
                    {
                        presentsvc.Rows[i]["PlanID"] = "1";
                    }

                    presentsvc.Rows[i]["AccountNo"] = lblAcctNo.Text.Trim();
                    presentsvc.Rows[i]["CreditLimit"] = lblCreditLine.Text.Trim();
                    presentsvc.Rows[i].EndEdit();
                    i++;
                }
                presentsvc.AcceptChanges();
                ViewState["dtPresentSvc"] = presentsvc;
            }
        }
    }

    protected void gvPresentSvc_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int rowid = Convert.ToInt32(e.CommandArgument);
        DataTable presentsvc = (DataTable)ViewState["dtPresentSvc"];
        UpdateSVCDataRow();

        if (e.CommandName.ToString().Trim() == "Remove")
        {
            presentsvc.Rows[rowid].Delete();
            presentsvc.AcceptChanges();
            ViewState["dtPresentSvc"] = presentsvc;

            LoadPresentSvcData();
            UpdateSVCDataRow();
        }
    }

    protected void gvPresentSvc_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DropDownList ddlPlans = (DropDownList)e.Row.FindControl("ddlPlans");
            TextBox lblAcctNo = (TextBox)e.Row.FindControl("lblAcctNo");
            TextBox lblCreditLimit = (TextBox)e.Row.FindControl("lblCreditLimit");
            HiddenField hdPlanID = (HiddenField)e.Row.FindControl("hdPlanID");

            lblAcctNo.Attributes.Add("maxLength", "20");
            lblCreditLimit.Attributes.Add("maxLength", "20");

            try
            {
                ddlPlans.SelectedValue = hdPlanID.Value.Trim();
            }
            catch
            {
                ddlPlans.SelectedIndex = 0;
            }
        }
    }
    #endregion

    #region References methods
    protected void lnkReferences_Click(object sender, EventArgs e)
    {
        DataTable references = null;
        switch (ddlReference.SelectedItem.Value.Trim())
        {
            case "1":
                references = (DataTable)ViewState["MajCustomers"];
                UpdateMajCustomersDataRow();
                if (references.Rows[references.Rows.Count - 1]["CompanyName"].ToString().Trim().Length > 0)
                {
                    AddEmptyReferencesRow(ref references);
                    LoadMajCustomersData();
                }
                break;
            case "2":
                references = (DataTable)ViewState["Banks"];
                UpdateBanksDataRow();
                if (references.Rows[references.Rows.Count - 1]["CompanyName"].ToString().Trim().Length > 0)
                {
                    AddEmptyReferencesRow(ref references);
                    LoadBanksData();
                }
                break;
            case "3":
                references = (DataTable)ViewState["AffCompany"];
                UpdateAffiliatesDataRow();
                if (references.Rows[references.Rows.Count - 1]["CompanyName"].ToString().Trim().Length > 0)
                {
                    AddEmptyReferencesRow(ref references);
                    LoadAffiliatesData();
                }
                break;
            case "4":
                references = (DataTable)ViewState["ExternAudit"];
                UpdateExternAuditDataRow();
                if (references.Rows[references.Rows.Count - 1]["CompanyName"].ToString().Trim().Length > 0)
                {
                    AddEmptyReferencesRow(ref references);
                    LoadExtAuditorsData();
                }
                break;
        }
    }

    private void UpdateMajCustomersDataRow()
    {
        if (ViewState["MajCustomers"] != null)
        {
            DataTable majcustoms = (DataTable)ViewState["MajCustomers"];

            if (gvMajCustomers.Rows.Count > 0)
            {
                TextBox lblName = null;
                TextBox lblAveMonthly = null;

                int i = 0;
                foreach (GridViewRow row in gvMajCustomers.Rows)
                {
                    lblName = (TextBox)row.FindControl("lblName");
                    lblAveMonthly = (TextBox)row.FindControl("lblAveMonthly");

                    majcustoms.Rows[i].BeginEdit();
                    majcustoms.Rows[i]["CompanyName"] = lblName.Text.Trim();
                    majcustoms.Rows[i]["AveMonthlySales"] = lblAveMonthly.Text.Trim();
                    majcustoms.Rows[i].EndEdit();
                    i++;
                }

                majcustoms.AcceptChanges();
                ViewState["MajCustomers"] = majcustoms;
            }
        }
    }

    private void UpdateBanksDataRow()
    {
        if (ViewState["Banks"] != null)
        {
            DataTable banks = (DataTable)ViewState["Banks"];

            if (gvBanks.Rows.Count > 0)
            {
                TextBox lblName = null;
                TextBox lblCreditLine = null;

                int i = 0;
                foreach (GridViewRow row in gvBanks.Rows)
                {
                    lblName = (TextBox)row.FindControl("lblName");
                    lblCreditLine = (TextBox)row.FindControl("lblCreditLine");

                    banks.Rows[i].BeginEdit();

                    banks.Rows[i]["CompanyName"] = lblName.Text.Trim();
                    banks.Rows[i]["CreditLine"] = lblCreditLine.Text.Trim();
                    banks.Rows[i].EndEdit();
                    i++;
                }

                banks.AcceptChanges();
                ViewState["Banks"] = banks;
            }
        }
    }

    private void UpdateAffiliatesDataRow()
    {
        if (ViewState["AffCompany"] != null)
        {
            DataTable affiliate = (DataTable)ViewState["AffCompany"];

            if (gvAffiliate.Rows.Count > 0)
            {
                TextBox lblName = null;
                TextBox lblBusiness = null;

                int i = 0;
                foreach (GridViewRow row in gvAffiliate.Rows)
                {
                    lblName = (TextBox)row.FindControl("lblName");
                    lblBusiness = (TextBox)row.FindControl("lblBusiness");

                    affiliate.Rows[i].BeginEdit();

                    affiliate.Rows[i]["CompanyName"] = lblName.Text.Trim();
                    affiliate.Rows[i]["KindOfBusiness"] = lblBusiness.Text.Trim();
                    affiliate.Rows[i].EndEdit();
                    i++;
                }

                affiliate.AcceptChanges();
                ViewState["AffCompany"] = affiliate;
            }
        }
    }

    private void UpdateExternAuditDataRow()
    {
        if (ViewState["ExternAudit"] != null)
        {
            DataTable extaudit = (DataTable)ViewState["ExternAudit"];

            if (gvExtAuditors.Rows.Count > 0)
            {
                TextBox lblName = null;
                TextBox lblLegalCounsel = null;

                int i = 0;
                foreach (GridViewRow row in gvExtAuditors.Rows)
                {
                    lblName = (TextBox)row.FindControl("lblName");
                    lblLegalCounsel = (TextBox)row.FindControl("lblLegalCounsel");

                    extaudit.Rows[i].BeginEdit();

                    extaudit.Rows[i]["CompanyName"] = lblName.Text.Trim();
                    extaudit.Rows[i]["LegalCounsel"] = lblLegalCounsel.Text.Trim();
                    extaudit.Rows[i].EndEdit();
                    i++;
                }

                extaudit.AcceptChanges();
                ViewState["ExternAudit"] = extaudit;
            }
        }
    }

    private void LoadMajCustomersData()
    {
        DataTable majcustoms = (DataTable)ViewState["MajCustomers"];

        if (majcustoms.Rows.Count == 0)
        {
            ddlReference.SelectedItem.Value = "1";
            AddEmptyReferencesRow(ref majcustoms);
        }

        UpdateIndexColumn(ref majcustoms);
        DataView dvMajcustoms = new DataView(majcustoms);

        gvMajCustomers.DataSource = dvMajcustoms;
        gvMajCustomers.DataBind();

        ViewState["MajCustomers"] = majcustoms;
    }

    private void LoadBanksData()
    {
        DataTable banks = (DataTable)ViewState["Banks"];

        if (banks.Rows.Count == 0)
        {
            ddlReference.SelectedItem.Value = "2";
            AddEmptyReferencesRow(ref banks);
        }

        UpdateIndexColumn(ref banks);
        DataView dvBanks = new DataView(banks);

        gvBanks.DataSource = dvBanks;
        gvBanks.DataBind();

        ViewState["Banks"] = banks;
    }

    private void LoadAffiliatesData()
    {
        DataTable affiliates = (DataTable)ViewState["AffCompany"];

        if (affiliates.Rows.Count == 0)
        {
            ddlReference.SelectedItem.Value = "3";
            AddEmptyReferencesRow(ref affiliates);
        }

        UpdateIndexColumn(ref affiliates);
        DataView dvAffiliates = new DataView(affiliates);

        gvAffiliate.DataSource = dvAffiliates;
        gvAffiliate.DataBind();

        ViewState["AffCompany"] = affiliates;
    }

    private void LoadExtAuditorsData()
    {
        DataTable extauditors = (DataTable)ViewState["ExternAudit"];

        if (extauditors.Rows.Count == 0)
        {
            ddlReference.SelectedItem.Value = "4";
            AddEmptyReferencesRow(ref extauditors);
        }

        UpdateIndexColumn(ref extauditors);
        DataView dvExtAuditors = new DataView(extauditors);

        gvExtAuditors.DataSource = dvExtAuditors;
        gvExtAuditors.DataBind();

        ViewState["ExternAudit"] = extauditors;
    }

    private DataTable CreateEmptyMajorCustomers()
    {
        DataTable dt = new DataTable();
        DataColumn dcol = new DataColumn("CompanyName", typeof(System.String));
        dt.Columns.Add(dcol);
        dcol = new DataColumn("AveMonthlySales", typeof(System.String));
        dt.Columns.Add(dcol);

        DataRow dr = dt.NewRow();
        dr["CompanyName"] = "";
        dr["AveMonthlySales"] = "";
        dt.Rows.Add(dr);

        return dt;
    }

    private DataTable CreateEmptyBanks()
    {
        DataTable dt = new DataTable();
        DataColumn dcol = new DataColumn("CompanyName", typeof(System.String));
        dt.Columns.Add(dcol);
        dcol = new DataColumn("CreditLine", typeof(System.String));
        dt.Columns.Add(dcol);

        DataRow dr = dt.NewRow();
        dr["CompanyName"] = "";
        dr["CreditLine"] = "";
        dt.Rows.Add(dr);

        return dt;
    }

    private DataTable CreateEmptyAffiliatedCompanies()
    {
        DataTable dt = new DataTable();
        DataColumn dcol = new DataColumn("CompanyName", typeof(System.String));
        dt.Columns.Add(dcol);
        dcol = new DataColumn("KindOfBusiness", typeof(System.String));
        dt.Columns.Add(dcol);

        DataRow dr = dt.NewRow();
        dr["CompanyName"] = "";
        dr["KindOfBusiness"] = "";
        dt.Rows.Add(dr);

        return dt;
    }

    private DataTable CreateEmptyExternalAuditor()
    {
        DataTable dt = new DataTable();
        DataColumn dcol = new DataColumn("CompanyName", typeof(System.String));
        dt.Columns.Add(dcol);
        dcol = new DataColumn("LegalCounsel", typeof(System.String));
        dt.Columns.Add(dcol);

        DataRow dr = dt.NewRow();
        dr["CompanyName"] = "";
        dr["LegalCounsel"] = "";
        dt.Rows.Add(dr);

        return dt;
    }

    private void AddEmptyReferencesRow(ref DataTable dt)
    {
        if (dt != null)
        {
            DataRow drEmpty = dt.NewRow();

            drEmpty["CompanyName"] = System.DBNull.Value;
            switch (ddlReference.SelectedItem.Value.Trim())
            {
                case "1":
                    drEmpty["AveMonthlySales"] = System.DBNull.Value;
                    break;
                case "2":
                    drEmpty["CreditLine"] = System.DBNull.Value;
                    break;
                case "3":
                    drEmpty["KindOfBusiness"] = System.DBNull.Value;
                    break;
                case "4":
                    drEmpty["LegalCounsel"] = System.DBNull.Value;
                    break;
            }

            dt.Rows.Add(drEmpty);
        }
    }

    protected void gvMajCustomers_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int rowid = Convert.ToInt32(e.CommandArgument);
        DataTable majcustoms = (DataTable)ViewState["MajCustomers"];
        UpdateMajCustomersDataRow();

        if (e.CommandName.ToString().Trim() == "Remove")
        {
            majcustoms.Rows[rowid].Delete();
            majcustoms.AcceptChanges();
            ViewState["MajCustomers"] = majcustoms;

            LoadMajCustomersData();
            UpdateMajCustomersDataRow();
        }
    }

    protected void gvMajCustomers_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            TextBox lblName = (TextBox)e.Row.FindControl("lblName");
            TextBox lblAveMonthly = (TextBox)e.Row.FindControl("lblAveMonthly");

            lblName.Attributes.Add("maxLength", "100");
            lblAveMonthly.Attributes.Add("maxLength", "10");
        }
    }

    protected void gvBanks_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int rowid = Convert.ToInt32(e.CommandArgument);
        DataTable banks = (DataTable)ViewState["Banks"];
        UpdateBanksDataRow();

        if (e.CommandName.ToString().Trim() == "Remove")
        {
            banks.Rows[rowid].Delete();
            banks.AcceptChanges();
            ViewState["Banks"] = banks;

            LoadBanksData();
            UpdateBanksDataRow();
        }
    }

    protected void gvBanks_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            TextBox lblName = (TextBox)e.Row.FindControl("lblName");
            TextBox lblCreditLine = (TextBox)e.Row.FindControl("lblCreditLine");

            lblName.Attributes.Add("maxLength", "100");
            lblCreditLine.Attributes.Add("maxLength", "10");
        }
    }

    protected void gvAffiliate_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int rowid = Convert.ToInt32(e.CommandArgument);
        DataTable affiliates = (DataTable)ViewState["AffCompany"];
        UpdateAffiliatesDataRow();

        if (e.CommandName.ToString().Trim() == "Remove")
        {
            affiliates.Rows[rowid].Delete();
            affiliates.AcceptChanges();
            ViewState["AffCompany"] = affiliates;

            LoadAffiliatesData();
            UpdateAffiliatesDataRow();
        }
        else
        {
            Response.Write("No Action");
        }
    }

    protected void gvAffiliate_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            TextBox lblName = (TextBox)e.Row.FindControl("lblName");
            TextBox lblBusiness = (TextBox)e.Row.FindControl("lblBusiness");

            lblName.Attributes.Add("maxLength", "100");
            lblBusiness.Attributes.Add("maxLength", "100");
        }
    }

    protected void gvExtAuditors_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int rowid = Convert.ToInt32(e.CommandArgument);
        DataTable extAuditors = (DataTable)ViewState["ExternAudit"];
        UpdateExternAuditDataRow();

        if (e.CommandName.ToString().Trim() == "Remove")
        {
            extAuditors.Rows[rowid].Delete();
            extAuditors.AcceptChanges();
            ViewState["ExternAudit"] = extAuditors;

            LoadExtAuditorsData();
            UpdateExternAuditDataRow();
        }
    }

    protected void gvExtAuditors_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            TextBox lblName = (TextBox)e.Row.FindControl("lblName");
            TextBox lblLegalCounsel = (TextBox)e.Row.FindControl("lblLegalCounsel");

            lblName.Attributes.Add("maxLength", "100");
            lblLegalCounsel.Attributes.Add("maxLength", "100");
        }
    }
    #endregion

    #region Equipments methods
    protected void lnkAddEquipment_Click(object sender, EventArgs e)
    {
        DataTable equipments = (DataTable)ViewState["dtEquipments"];
        UpdateEquipmentDataRow();

        if ((equipments.Rows[equipments.Rows.Count - 1]["EquipmentType"].ToString().Trim().Length > 0) &&
            (equipments.Rows[equipments.Rows.Count - 1]["Units"].ToString().Trim().Length > 0))
        {
            AddEmptyEquipmentRow(ref equipments);
            LoadEquipmentsData();
        }

    }

    private DataTable CreateEmptyEquipment()
    {
        DataTable dt = new DataTable();
        DataColumn dcol = new DataColumn("EquipmentType", typeof(System.String));
        dt.Columns.Add(dcol);
        dcol = new DataColumn("Units", typeof(System.String));
        dt.Columns.Add(dcol);
        dcol = new DataColumn("Remarks", typeof(System.String));
        dt.Columns.Add(dcol);

        DataRow dr = dt.NewRow();
        dr["EquipmentType"] = "";
        dr["Units"] = "";
        dr["Remarks"] = "";
        dt.Rows.Add(dr);

        return dt;
    }

    private void LoadEquipmentsData()
    {
        DataTable equipments = (DataTable)ViewState["dtEquipments"];

        if (equipments.Rows.Count == 0)
        {
            AddEmptyEquipmentRow(ref equipments);
        }

        UpdateIndexColumn(ref equipments);
        DataView dvEquipments = new DataView(equipments);

        gvEquipments.DataSource = dvEquipments;
        gvEquipments.DataBind();

        ViewState["dtEquipments"] = equipments;
    }

    private void UpdateEquipmentDataRow()
    {
        if (ViewState["dtEquipments"] != null)
        {
            DataTable equipments = (DataTable)ViewState["dtEquipments"];

            if (gvEquipments.Rows.Count > 0)
            {
                TextBox lblEquipmentType = null;
                TextBox lblUnits = null;
                TextBox lblRemarks = null;

                int i = 0;
                foreach (GridViewRow row in gvEquipments.Rows)
                {
                    lblEquipmentType = (TextBox)row.FindControl("lblEqpmntType");
                    lblUnits = (TextBox)row.FindControl("lblUnits");
                    lblRemarks = (TextBox)row.FindControl("lblRemarks");

                    equipments.Rows[i].BeginEdit();
                    equipments.Rows[i]["EquipmentType"] = lblEquipmentType.Text.Trim();
                    try
                    {
                        equipments.Rows[i]["Units"] = lblUnits.Text.Trim();
                    }
                    catch
                    {
                        equipments.Rows[i]["Units"] = System.DBNull.Value;
                    }
                    equipments.Rows[i]["Remarks"] = lblRemarks.Text.Trim();
                    equipments.Rows[i].EndEdit();
                    i++;
                }

                equipments.AcceptChanges();
                ViewState["dtEquipments"] = equipments;
            }
        }
    }

    private void AddEmptyEquipmentRow(ref DataTable dt)
    {
        if (dt != null)
        {
            DataRow drEmpty = dt.NewRow();
            drEmpty["EquipmentType"] = System.DBNull.Value;
            drEmpty["Units"] = System.DBNull.Value;
            drEmpty["Remarks"] = System.DBNull.Value;
            dt.Rows.Add(drEmpty);
        }
    }

    protected void gvEquipments_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int rowid = Convert.ToInt32(e.CommandArgument);
        DataTable equipments = (DataTable)ViewState["dtEquipments"];
        UpdateEquipmentDataRow();

        if (e.CommandName.ToString().Trim() == "Remove")
        {
            equipments.Rows[rowid].Delete();
            equipments.AcceptChanges();
            ViewState["dtEquipments"] = equipments;

            LoadEquipmentsData();
            UpdateEquipmentDataRow();
        }
    }

    protected void gvEquipments_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            TextBox lblEquipmentType = (TextBox)e.Row.FindControl("lblEqpmntType");
            TextBox lblUnits = (TextBox)e.Row.FindControl("lblUnits");
            TextBox lblRemarks = (TextBox)e.Row.FindControl("lblRemarks");

            lblUnits.Attributes.Add("maxLength", "10");
            lblEquipmentType.Attributes.Add("maxLength", "50");
            lblRemarks.Attributes.Add("maxLength", "100");
        }
    }
    #endregion

    #region Relatives methods
    protected void lnkAddRelative_Click(object sender, EventArgs e)
    {
        DataTable relatives = (DataTable)ViewState["dtRelatives"];
        UpdateRelativesDataRow();

        if ((relatives.Rows[relatives.Rows.Count - 1]["VendorRelative"].ToString().Trim().Length > 0) &&
            (relatives.Rows[relatives.Rows.Count - 1]["Title"].ToString().Trim().Length > 0))
        {
            AddEmptyRelativesRow(ref relatives);
            LoadRelativesData();
        }
    }

    private DataTable CreateEmptyRelativesTable()
    {
        DataTable dt = new DataTable();
        DataColumn dcol = new DataColumn("VendorRelative", typeof(System.String));
        dt.Columns.Add(dcol);
        dcol = new DataColumn("Title", typeof(System.String));
        dt.Columns.Add(dcol);
        dcol = new DataColumn("Relationship", typeof(System.String));
        dt.Columns.Add(dcol);

        DataRow dr = dt.NewRow();
        dr["VendorRelative"] = "";
        dr["Title"] = "";
        dr["Relationship"] = "";
        dt.Rows.Add(dr);

        return dt;
    }

    private void UpdateRelativesDataRow()
    {
        if (ViewState["dtRelatives"] != null)
        {
            DataTable relatives = (DataTable)ViewState["dtRelatives"];

            if (gvRelatives.Rows.Count > 0)
            {
                TextBox lblRelative = null;
                TextBox lblTitle = null;
                TextBox lblRelationship = null;

                int i = 0;
                foreach (GridViewRow row in gvRelatives.Rows)
                {
                    //row.BeginEdit();

                    lblRelative = (TextBox)row.FindControl("lblRelative");
                    lblTitle = (TextBox)row.FindControl("lblTitle");
                    lblRelationship = (TextBox)row.FindControl("lblRelationship");

                    relatives.Rows[i].BeginEdit();

                    relatives.Rows[i]["VendorRelative"] = lblRelative.Text.Trim();
                    relatives.Rows[i]["Title"] = lblTitle.Text.Trim();
                    relatives.Rows[i]["Relationship"] = lblRelationship.Text.Trim();
                    relatives.Rows[i].EndEdit();
                    i++;
                }

                relatives.AcceptChanges();
                ViewState["dtRelatives"] = relatives;
            }
        }
    }

    private void LoadRelativesData()
    {
        DataTable relatives = (DataTable)ViewState["dtRelatives"];

        if (relatives.Rows.Count == 0)
        {
            AddEmptyRelativesRow(ref relatives);
        }

        UpdateIndexColumn(ref relatives);
        DataView dvRelatives = new DataView(relatives);

        gvRelatives.DataSource = dvRelatives;
        gvRelatives.DataBind();

        ViewState["dtRelatives"] = relatives;
    }

    private void AddEmptyRelativesRow(ref DataTable dt)
    {
        if (dt != null)
        {
            DataRow drEmpty = dt.NewRow();
            drEmpty["VendorRelative"] = System.DBNull.Value;
            drEmpty["Title"] = System.DBNull.Value;
            drEmpty["Relationship"] = System.DBNull.Value;
            dt.Rows.Add(drEmpty);
        }
    }

    protected void gvRelatives_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int rowid = Convert.ToInt32(e.CommandArgument);
        DataTable relatives = (DataTable)ViewState["dtRelatives"];
        UpdateRelativesDataRow();

        if (e.CommandName.ToString().Trim() == "Remove")
        {
            relatives.Rows[rowid].Delete();
            relatives.AcceptChanges();
            ViewState["dtRelatives"] = relatives;

            LoadRelativesData();
            UpdateRelativesDataRow();
        }
    }

    protected void gvRelatives_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            TextBox lblRelative = (TextBox)e.Row.FindControl("lblRelative");
            TextBox lblTitle = (TextBox)e.Row.FindControl("lblTitle");
            TextBox lblRelationship = (TextBox)e.Row.FindControl("lblRelationship");

            lblRelative.Attributes.Add("maxLength", "50");
            lblTitle.Attributes.Add("maxLength", "50");
            lblRelationship.Attributes.Add("maxLength", "50");
        }
    }
    #endregion

    #region Save methods
    private void InitDeletedIDContainer()
    {
        DataTable deleted = new DataTable();

        deleted.Columns.Add("ID");
        deleted.Columns.Add("Type");

        ViewState["Deleted"] = deleted;
    }

    private void UpdateAll()
    {
        UpdateSVCDataRow();
        UpdateMajCustomersDataRow();
        UpdateBanksDataRow();
        UpdateExternAuditDataRow();
        UpdateAffiliatesDataRow();
        UpdateEquipmentDataRow();
        UpdateRelativesDataRow();
        
    }

    private void SaveVendorClassification(int vendorId, SqlTransaction transact)
    {
        if (vendorId > 0)
        {
            SqlParameter[] sqlparams = new SqlParameter[2];
            sqlparams[0] = new SqlParameter("@vendorId", SqlDbType.Int);
            sqlparams[0].Value = vendorId;
            sqlparams[1] = new SqlParameter("@classId", SqlDbType.Int);

            if (chkCompanyClassification_4.Checked == false)
            {
                sqlparams[1].Value = 5;
                SqlHelper.ExecuteNonQuery(transact, "s3p_EBid_InsertVendorClassification", sqlparams);
                return;
            }

            if (chkCompanyClassification_0.Checked == true)
            {
                sqlparams[1].Value = 1;
                SqlHelper.ExecuteNonQuery(transact, "s3p_EBid_InsertVendorClassification", sqlparams);
            }

            if (chkCompanyClassification_1.Checked == true)
            {
                sqlparams[1].Value = 2;
                SqlHelper.ExecuteNonQuery(transact, "s3p_EBid_InsertVendorClassification", sqlparams);
            }

            if (chkCompanyClassification_2.Checked == true)
            {
                sqlparams[1].Value = 3;
                SqlHelper.ExecuteNonQuery(transact, "s3p_EBid_InsertVendorClassification", sqlparams);
            }

            if (chkCompanyClassification_3.Checked == true)
            {
                sqlparams[1].Value = 4;
                SqlHelper.ExecuteNonQuery(transact, "s3p_EBid_InsertVendorClassification", sqlparams);
            }
        }
    }

    private void SaveCategoriesAndSubCategories(int vendorId, SqlTransaction transact)
    {        
        SqlParameter[] sqlParams = new SqlParameter[3];
        sqlParams[0] = new SqlParameter("@vendorId", SqlDbType.Int);
        sqlParams[1] = new SqlParameter("@CategoryIds", SqlDbType.VarChar);
        sqlParams[2] = new SqlParameter("@SubCategoryIds", SqlDbType.VarChar);
        sqlParams[0].Value = vendorId;
        sqlParams[1].Value = hdnCategories.Text.Replace(",", "|");
        sqlParams[2].Value = hdnSubCategory.Text.Replace(",", "|");

        SqlHelper.ExecuteNonQuery(transact, "sp_AddVendorCategoriesAndSubCategories", sqlParams);
    }
        
    private void SavePresentServices(int vendorId, SqlTransaction transact)
    {
        if (ViewState["dtPresentSvc"] != null)
        {
            DataTable presentSvc = (DataTable)ViewState["dtPresentSvc"];            
            CompanyTransaction companyTrans = new CompanyTransaction();

            if (presentSvc.Rows.Count > 0)
            {
                foreach (DataRow row in presentSvc.Rows)
                {
                    bool isNew = true;

                    if ((row["AccountNo"].ToString().Trim().Length > 0) &&
                        (row["CreditLimit"].ToString().Trim().Length > 0))
                        companyTrans.UpdatePresentServices(vendorId.ToString().Trim(), row["PlanID"].ToString().Trim(),
                                               null,
                                               row["AccountNo"].ToString().Trim(),
                                               row["CreditLimit"].ToString().Trim(), isNew, transact);
                }
            }
        }
    }

    private void SaveMajCustomers(int vendorId, SqlTransaction transact)
    {
        if (ViewState["MajCustomers"] != null)
        {
            CompanyTransaction companyTrans = new CompanyTransaction();
            DataTable majCustomers = (DataTable)ViewState["MajCustomers"];

            if (majCustomers.Rows.Count > 0)
            {
                foreach (DataRow row in majCustomers.Rows)
                {
                    bool isNew = true;

                    if (row["CompanyName"].ToString().Trim().Length > 0)
                        companyTrans.UpdateReference1(vendorId.ToString().Trim(), null,
                                                   row["CompanyName"].ToString().Trim(),
                                                   row["AveMonthlySales"].ToString().Trim(), isNew, transact);
                }
            }
        }
    }

    private void SaveBanks(int vendorId, SqlTransaction transact)
    {
        if (ViewState["Banks"] != null)
        {
            CompanyTransaction companyTrans = new CompanyTransaction();
            DataTable banks = (DataTable)ViewState["Banks"];

            if (banks.Rows.Count > 0)
            {
                foreach (DataRow row in banks.Rows)
                {
                    bool isNew = true;

                    if (row["CompanyName"].ToString().Trim().Length > 0)
                        companyTrans.UpdateReference2(vendorId.ToString().Trim(), null,
                                                   row["CompanyName"].ToString().Trim(),
                                                   row["CreditLine"].ToString().Trim(), isNew, transact);
                }
            }
        }
    }

    private void SaveAffiliates(int vendorId, SqlTransaction transact)
    {
        if (ViewState["AffCompany"] != null)
        {
            CompanyTransaction companyTrans = new CompanyTransaction();
            DataTable affiliates = (DataTable)ViewState["AffCompany"];

            if (affiliates.Rows.Count > 0)
            {
                foreach (DataRow row in affiliates.Rows)
                {
                    bool isNew = true;

                    if (row["CompanyName"].ToString().Trim().Length > 0)
                        companyTrans.UpdateReference3(vendorId.ToString().Trim(), null,
                                                   row["CompanyName"].ToString().Trim(),
                                                   row["KindOfBusiness"].ToString().Trim(), isNew, transact);
                }
            }
        }
    }

    private void SaveExtAuditors(int vendorId, SqlTransaction transact)
    {
        if (ViewState["ExternAudit"] != null)
        {
            CompanyTransaction companyTrans = new CompanyTransaction();
            DataTable extAuditors = (DataTable)ViewState["ExternAudit"];

            if (extAuditors.Rows.Count > 0)
            {
                foreach (DataRow row in extAuditors.Rows)
                {
                    bool isNew = true;

                    if (row["CompanyName"].ToString().Trim().Length > 0)
                        companyTrans.UpdateReference4(vendorId.ToString().Trim(), null,
                                                   row["CompanyName"].ToString().Trim(),
                                                   row["LegalCounsel"].ToString().Trim(), isNew, transact);
                }
            }
        }
    }

    private void SaveEquipments(int vendorId, SqlTransaction transact)
    {
        if (ViewState["dtEquipments"] != null)
        {
            CompanyTransaction companyTrans = new CompanyTransaction();
            DataTable equipments = (DataTable)ViewState["dtEquipments"];

            if (equipments.Rows.Count > 0)
            {
                foreach (DataRow row in equipments.Rows)
                {
                    bool isNew = true;

                    if (row["EquipmentType"].ToString().Trim().Length > 0)
                        companyTrans.UpdateEquipments(vendorId.ToString().Trim(), null,
                                                   row["EquipmentType"].ToString().Trim(),
                                                   row["Units"].ToString().Trim(),
                                                   row["Remarks"].ToString().Trim(), isNew, transact);
                }
            }
        }
    }
    
    private void SaveRelatives(int vendorId, SqlTransaction transact)
    {
        if (ViewState["dtRelatives"] != null)
        {
            CompanyTransaction companyTrans = new CompanyTransaction();
            DataTable relatives = (DataTable)ViewState["dtRelatives"];

            if (relatives.Rows.Count > 0)
            {
                foreach (DataRow row in relatives.Rows)
                {
                    bool isNew = true;

                    if (row["VendorRelative"].ToString().Trim().Length > 0)
                        companyTrans.UpdateRelatives(vendorId.ToString().Trim(), null,
                                               row["VendorRelative"].ToString().Trim(),
                                               row["Title"].ToString().Trim(),
                                               row["Relationship"].ToString().Trim(), isNew, transact);
                }
            }
        }
    }

    private void SaveVendorBrands(int vendorId, SqlTransaction transact)
    {
        SqlParameter[] sqlParams = new SqlParameter[2];
        sqlParams[0] = new SqlParameter("@vendorId", SqlDbType.Int);
        sqlParams[1] = new SqlParameter("@brands", SqlDbType.NVarChar);
        sqlParams[0].Value = vendorId;
        sqlParams[1].Value = hdnBrands.Value;

        SqlHelper.ExecuteNonQuery(transact, "s3p_EBid_UpdateVendorBrands", sqlParams);
    }

    private void SaveVendorItems(int vendorId, SqlTransaction transact)
    {
        SqlParameter[] sqlParams = new SqlParameter[2];
        sqlParams[0] = new SqlParameter("@vendorId", SqlDbType.Int);
        sqlParams[1] = new SqlParameter("@items", SqlDbType.NVarChar);
        sqlParams[0].Value = vendorId;
        sqlParams[1].Value = hdnItems.Value;

        SqlHelper.ExecuteNonQuery(transact, "s3p_EBid_UpdateVendorItems", sqlParams);
    }

    private void SaveVendorServices(int vendorId, SqlTransaction transact)
    {
        SqlParameter[] sqlParams = new SqlParameter[2];
        sqlParams[0] = new SqlParameter("@vendorId", SqlDbType.Int);
        sqlParams[1] = new SqlParameter("@services", SqlDbType.NVarChar);
        sqlParams[0].Value = vendorId;
        sqlParams[1].Value = hdnServices.Value;

        SqlHelper.ExecuteNonQuery(transact, "s3p_EBid_UpdateVendorServices", sqlParams);
    }

    private void SaveVendorLocations(int vendorId, SqlTransaction transact)
    {
        SqlParameter[] sqlParams = new SqlParameter[2];
        sqlParams[0] = new SqlParameter("@vendorId", SqlDbType.Int);
        sqlParams[1] = new SqlParameter("@locations", SqlDbType.NVarChar);
        sqlParams[0].Value = vendorId;
        sqlParams[1].Value = hdnLocations.Value;

        SqlHelper.ExecuteNonQuery(transact, "s3p_EBid_UpdateVendorLocations", sqlParams);
    }
	
	private void SaveVendorOtherInfo(int vendorId, SqlTransaction transact)
	{
		SqlParameter[] sqlParams = new SqlParameter[17];
		sqlParams[0] = new SqlParameter("@VendorId", SqlDbType.Int);
		sqlParams[1] = new SqlParameter("@Vendor_Code", SqlDbType.VarChar);
		sqlParams[2] = new SqlParameter("@SLA_SIR_Date", SqlDbType.DateTime);
		sqlParams[3] = new SqlParameter("@SLA_Date_Approved", SqlDbType.DateTime);
		sqlParams[4] = new SqlParameter("@Accreditation_Duration", SqlDbType.DateTime);
		sqlParams[5] = new SqlParameter("@Accreditation_From", SqlDbType.DateTime);
		sqlParams[6] = new SqlParameter("@Accreditation_To", SqlDbType.DateTime);
		sqlParams[7] = new SqlParameter("@Perf_Evaluation_Date", SqlDbType.DateTime);
		sqlParams[8] = new SqlParameter("@Perf_Evaluation_Rate", SqlDbType.Int);
		sqlParams[9] = new SqlParameter("@Composite_Rating_SIR_Date", SqlDbType.DateTime);
		sqlParams[10] = new SqlParameter("@Composite_Rating_Rate", SqlDbType.Int);
		sqlParams[11] = new SqlParameter("@Maximum_Exposure_SIR_Date", SqlDbType.DateTime);
		sqlParams[12] = new SqlParameter("@Maximum_Exposure_Amount", SqlDbType.Float);
		sqlParams[13] = new SqlParameter("@Enrollment_Date", SqlDbType.DateTime);
		sqlParams[14] = new SqlParameter("@IR_Date", SqlDbType.DateTime);
		sqlParams[15] = new SqlParameter("@IR_Number", SqlDbType.Int);
		sqlParams[16] = new SqlParameter("@IR_Description", SqlDbType.VarChar);
		
		sqlParams[0].Value = vendorId;
		sqlParams[1].Value = tbVendorCode.Text.Trim();
		sqlParams[2].Value = (tbSLASIRDate.Text.Trim().Length > 0) ? Convert.ToDateTime(tbSLASIRDate.Text.Trim()) : Convert.ToDateTime("01-01-1900");	
		sqlParams[3].Value = (tbSLADateApproved.Text.Trim().Length > 0) ? Convert.ToDateTime(tbSLADateApproved.Text.Trim()) : Convert.ToDateTime("01-01-1900");
		sqlParams[4].Value = (tbSLAAccredited.Text.Trim().Length > 0) ? Convert.ToDateTime(tbSLAAccredited.Text.Trim()) : Convert.ToDateTime("01-01-1900");
		sqlParams[5].Value = (tbAccrFrom.Text.Trim().Length > 0) ? Convert.ToDateTime(tbAccrFrom.Text.Trim()) : Convert.ToDateTime("01-01-1900");
		sqlParams[6].Value = (tbAccrTo.Text.Trim().Length > 0) ? Convert.ToDateTime(tbAccrTo.Text.Trim()) : Convert.ToDateTime("01-01-1900");
		sqlParams[7].Value = (tbPerfDate.Text.Trim().Length > 0) ? Convert.ToDateTime(tbPerfDate.Text.Trim()) : Convert.ToDateTime("01-01-1900");
		sqlParams[8].Value = (tbPerfRate.Text.Trim().Length > 0) ? Int32.Parse(tbPerfRate.Text.Trim()) : 0;
		sqlParams[9].Value = (tbCompSIRDate.Text.Trim().Length) > 0 ? Convert.ToDateTime(tbCompSIRDate.Text.Trim()) : Convert.ToDateTime("01-01-1900");
		sqlParams[10].Value = (tbCompRate.Text.Trim().Length > 0) ? Int32.Parse(tbCompRate.Text.Trim()) : 0;
		sqlParams[11].Value = (tbMaxSIRDate.Text.Trim().Length > 0) ? Convert.ToDateTime(tbMaxSIRDate.Text.Trim()) : Convert.ToDateTime("01-01-1900");
		sqlParams[12].Value = (tbMaxRate.Text.Trim().Length > 0) ? float.Parse(tbMaxRate.Text.Trim()) : 0;
		sqlParams[13].Value = (tbEnrollmentDate.Text.Trim().Length > 0) ? Convert.ToDateTime(tbEnrollmentDate.Text.Trim()) : Convert.ToDateTime("01-01-1900");
		sqlParams[14].Value = (tbIRDate.Text.Trim().Length > 0) ? Convert.ToDateTime(tbIRDate.Text.Trim()) : Convert.ToDateTime("01-01-1900");
		sqlParams[15].Value = (tbIRNumber.Text.Trim().Length > 0) ? Int32.Parse(tbIRNumber.Text.Trim()) : 0;
		sqlParams[16].Value = tbIRDescription.Text.Trim();
		
		SqlHelper.ExecuteNonQuery(transact, "s3p_EBid_UpdateVendor_Addl_Info", sqlParams);

	}
	
    private void SaveAll(int vendorId, SqlTransaction transact)
    {
        SaveVendorClassification(vendorId, transact);
        SaveCategoriesAndSubCategories(vendorId, transact);
        SavePresentServices(vendorId, transact);
        SaveMajCustomers(vendorId, transact);
        SaveBanks(vendorId, transact);
        SaveAffiliates(vendorId, transact);
        SaveExtAuditors(vendorId, transact);
        SaveEquipments(vendorId, transact);
        SaveRelatives(vendorId, transact);
        SaveVendorBrands(vendorId, transact);
        SaveVendorItems(vendorId, transact);
        SaveVendorServices(vendorId, transact);
        SaveVendorLocations(vendorId, transact);
		//new
		SaveVendorOtherInfo(vendorId, transact);
    }

    private int SaveVendorDetails()
    {

        SqlConnection sqlConnect = new SqlConnection(connstring);
        SqlTransaction sqlTransact = null;
        int value = 0;

        try
        {
            sqlConnect.Open();
            sqlTransact = sqlConnect.BeginTransaction();

            string randomPwd = RandomPasswordGenerator.GenerateRandomPassword();

            SqlParameter[] sqlParams = new SqlParameter[38];
            sqlParams[0] = new SqlParameter("@userName", SqlDbType.VarChar);
            sqlParams[0].Value = tbUserName.Text.Trim();
            sqlParams[1] = new SqlParameter("@password", SqlDbType.VarChar);
            sqlParams[1].Value = EncryptionHelper.Encrypt(randomPwd);
            sqlParams[2] = new SqlParameter("@vendorName", SqlDbType.VarChar);
            sqlParams[2].Value = tbCompanyName.Text.Trim();
            sqlParams[3] = new SqlParameter("@vendorAddress", SqlDbType.VarChar);
            sqlParams[3].Value = (tbHeadOfficeAddress1.Text.Trim().Length > 0) ? tbHeadOfficeAddress1.Text.Trim() : null;
            sqlParams[4] = new SqlParameter("@vendorAddress1", SqlDbType.VarChar);
            sqlParams[4].Value = (tbHeadOfficeAddress2.Text.Trim().Length > 0) ? tbHeadOfficeAddress2.Text.Trim() : null;
            sqlParams[5] = new SqlParameter("@vendorPhone", SqlDbType.VarChar);
            sqlParams[5].Value = (tbTelephone.Text.Trim().Length > 0) ? tbTelephone.Text.Trim() : null;
            sqlParams[6] = new SqlParameter("@vendorMobile", SqlDbType.VarChar);
            sqlParams[6].Value = (tbMobileNo.Text.Trim().Length > 0) ? tbMobileNo.Text.Trim() : null;
            sqlParams[7] = new SqlParameter("@vendorFax", SqlDbType.VarChar);
            sqlParams[7].Value = (tbFax.Text.Trim().Length > 0) ? tbFax.Text.Trim() : null;
            sqlParams[8] = new SqlParameter("@vendorExt", SqlDbType.VarChar);
            sqlParams[8].Value = (tbExtension.Text.Trim().Length > 0) ? tbExtension.Text.Trim() : null;
            sqlParams[9] = new SqlParameter("@branchAddress", SqlDbType.VarChar);
            sqlParams[9].Value = (tbBranchAddress1.Text.Trim().Length > 0) ? tbBranchAddress1.Text.Trim() : null;
            sqlParams[10] = new SqlParameter("@branchAddress1", SqlDbType.VarChar);
            sqlParams[10].Value = (tbBranchAddress2.Text.Trim().Length > 0) ? tbBranchAddress2.Text.Trim() : null;
            sqlParams[11] = new SqlParameter("@branchPhone", SqlDbType.VarChar);
            sqlParams[11].Value = (tbBranchPhone.Text.Trim().Length > 0) ? tbBranchPhone.Text.Trim() : null;
            sqlParams[12] = new SqlParameter("@branchFax", SqlDbType.VarChar);
            sqlParams[12].Value = (tbBranchFax.Text.Trim().Length > 0) ? tbBranchFax.Text.Trim() : null;
            sqlParams[13] = new SqlParameter("@branchExt", SqlDbType.VarChar);
            sqlParams[13].Value = (tbBranchExtension.Text.Trim().Length > 0) ? tbBranchExtension.Text.Trim() : null;
            sqlParams[14] = new SqlParameter("@vatRegNo", SqlDbType.VarChar);
            sqlParams[14].Value = (tbVatRegNo.Text.Trim().Length > 0) ? tbVatRegNo.Text.Trim() : null;
            sqlParams[15] = new SqlParameter("@TIN", SqlDbType.VarChar);
            sqlParams[15].Value = (tbTIN.Text.Trim().Length > 0) ? tbTIN.Text.Trim() : null;
            sqlParams[16] = new SqlParameter("@POBox", SqlDbType.VarChar);
            sqlParams[16].Value = (tbPOBox.Text.Trim().Length > 0) ? tbPOBox.Text.Trim() : null;
            sqlParams[17] = new SqlParameter("@postalCode", SqlDbType.VarChar);
            sqlParams[17].Value = (tbPostalCode.Text.Trim().Length > 0) ? tbPostalCode.Text.Trim() : null;
            sqlParams[18] = new SqlParameter("@standardTerms", SqlDbType.VarChar);
            sqlParams[18].Value = (tbTermsofPayment.Text.Trim().Length > 0) ? tbTermsofPayment.Text.Trim() : null;
            sqlParams[19] = new SqlParameter("@specialTerms", SqlDbType.VarChar);
            sqlParams[19].Value = (tbSpecialTerms.Text.Trim().Length > 0) ? tbSpecialTerms.Text.Trim() : null;
            sqlParams[20] = new SqlParameter("@minOrderVal", SqlDbType.Float);
            sqlParams[20].Value = (tbMinOrderValue.Text.Trim().Length > 0) ? float.Parse(tbMinOrderValue.Text.Trim()) : 0;
            sqlParams[21] = new SqlParameter("@salesPerson", SqlDbType.VarChar);
            sqlParams[21].Value = (tbSalesPerson.Text.Trim().Length > 0) ? tbSalesPerson.Text.Trim() : null;
            sqlParams[22] = new SqlParameter("@salesPersonPhone", SqlDbType.VarChar);
            sqlParams[22].Value = (tbSalesPersonPhone.Text.Trim().Length > 0) ? tbSalesPersonPhone.Text.Trim() : null;
            sqlParams[23] = new SqlParameter("@emailAdd", SqlDbType.VarChar);
            sqlParams[23].Value = (tbEmail.Text.Trim().Length > 0) ? tbEmail.Text.Trim() : null;
            sqlParams[24] = new SqlParameter("@soleSupplier1", SqlDbType.VarChar);
            sqlParams[24].Value = (tbSoleSupplier1.Text.Trim().Length > 0) ? tbSoleSupplier1.Text.Trim() : null;
            sqlParams[25] = new SqlParameter("@soleSupplier2", SqlDbType.VarChar);
            sqlParams[25].Value = (tbSoleSupplier2.Text.Trim().Length > 0) ? tbSoleSupplier2.Text.Trim() : null;
            sqlParams[26] = new SqlParameter("@keyPersonnel", SqlDbType.VarChar);
            sqlParams[26].Value = (tbKeyPersonnel.Text.Trim().Length > 0) ? tbKeyPersonnel.Text.Trim() : null;
            sqlParams[27] = new SqlParameter("@kpPosition", SqlDbType.VarChar);
            sqlParams[27].Value = (tbKpPosition.Text.Trim().Length > 0) ? tbKpPosition.Text.Trim() : null;
            sqlParams[28] = new SqlParameter("@specialization", SqlDbType.VarChar);
            sqlParams[28].Value = (tbSpecialization.Text.Trim().Length > 0) ? tbSpecialization.Text.Trim() : null;
            sqlParams[29] = new SqlParameter("@category", SqlDbType.VarChar);
            sqlParams[29].Value = "0";
            sqlParams[30] = new SqlParameter("@accredited", SqlDbType.Int);
            sqlParams[30].Value = Int32.Parse(ddlSupplierType.SelectedValue);
            sqlParams[31] = new SqlParameter("@orgTypeId", SqlDbType.Int);
            sqlParams[31].Value = Int32.Parse(rbOrganizationType.SelectedValue);
            sqlParams[32] = new SqlParameter("@ownershipFil", SqlDbType.Int);
            sqlParams[32].Value = (tbOwnershipFilipino.Text.Trim().Length > 0) ? Int32.Parse(tbOwnershipFilipino.Text.Trim()) : 0;
            sqlParams[33] = new SqlParameter("@ownershipOther", SqlDbType.Int);
            sqlParams[33].Value = (tbOwnershipOther.Text.Trim().Length > 0) ? Int32.Parse(tbOwnershipOther.Text.Trim()) : 0;
            sqlParams[34] = new SqlParameter("@classification", SqlDbType.Int);
            sqlParams[34].Value = 1;
            sqlParams[35] = new SqlParameter("@isoStandard", SqlDbType.NVarChar);
            sqlParams[35].Value = SaveSelectedISO();
            sqlParams[36] = new SqlParameter("@pcabClass", SqlDbType.Int);
            sqlParams[36].Value = Int32.Parse(ddlPCABClass.SelectedValue);
            sqlParams[37] = new SqlParameter("@Clientid", SqlDbType.Int);
            sqlParams[37].Value = Int32.Parse(HttpContext.Current.Session["clientid"].ToString());

            value = Convert.ToInt32(SqlHelper.ExecuteScalar(sqlTransact, "s3p_EBid_SaveVendorDetails", sqlParams));

            sqlTransact.Commit();
        }
        catch
        {
            sqlTransact.Rollback();
            value = 0;
        }
        finally
        {
            sqlConnect.Close();
        }

        return value;
    }

    private string SaveSelectedISO()
    {
        string standard = "00";
        if ((cb9001.Checked) && (cb9002.Checked))
        {
            standard = "11";
        }
        else if ((cb9001.Checked) && (!(cb9002.Checked)))
        {
            standard = "10";
        }
        else if ((!(cb9001.Checked)) && (cb9002.Checked))
        {
            standard = "01";
        }

        return standard;
    }

    private bool SaveVendor()
    {
        SqlConnection sqlConnect = new SqlConnection(connstring);
        SqlTransaction sqlTransact = null;
        bool success = false;
        int result = 0;

        try
        {
            sqlConnect.Open();
            sqlTransact = sqlConnect.BeginTransaction();
            // get the userid/vendorid of the new vendor
            result = SaveVendorDetails();

            //Response.Write(result.ToString());
            if (result > 0)
            {
                UpdateAll();
                SaveAll(result, sqlTransact);
                sqlTransact.Commit();
                success = true;
                ViewState["sUserID"] = result;
            }
            else
            {
                success = false;
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.ToString());
            sqlTransact.Rollback(); 
            // delete completely the newly created user
            if (result > 0 && Request.QueryString["vid"]!=null)
            {
                SqlParameter[] sqlParams = new SqlParameter[3];
                sqlParams[0] = new SqlParameter("@UserID", Int32.Parse(Request.QueryString["vid"]));
                sqlParams[1] = new SqlParameter("@UserType", 2);
                sqlParams[2] = new SqlParameter("@DeleteType", 1); // delete completely
                SqlHelper.ExecuteNonQuery(connstring, "s3p_EBid_DeleteUser", sqlParams);
            }
            success = false;
        }
        finally
        {
            sqlConnect.Close();
        }
		
        return success;
    }    
    #endregion

    #region Update Methods
    private bool UpdateVendor(int vendorId)
    {
        SqlConnection sqlConnect = new SqlConnection(connstring);
        SqlTransaction sqlTransact = null;
        bool success = false;

        try
        {
            sqlConnect.Open();
            sqlTransact = sqlConnect.BeginTransaction();

            UpdateAll();

            DeleteRowsInDB();
            SaveVendorOtherInfo(vendorId, sqlTransact);
            UpdateUserData(vendorId, sqlTransact);
            UpdateVendorDetail(vendorId, sqlTransact);
            UpdateVendorClassification(vendorId, sqlTransact);            
            UpdateCategoriesAndSubCategories(vendorId, sqlTransact);
            UpdatePresentServices(vendorId, sqlTransact);
            UpdateMajCustomers(vendorId, sqlTransact);
            UpdateBanks(vendorId, sqlTransact);
            UpdateAffiliates(vendorId, sqlTransact);
            UpdateExtAuditors(vendorId, sqlTransact);
            UpdateEquipments(vendorId, sqlTransact);
            UpdateRelatives(vendorId, sqlTransact);
            UpdateVendorBrands(vendorId, sqlTransact);
            UpdateVendorItems(vendorId, sqlTransact);
            UpdateVendorServices(vendorId, sqlTransact);
            UpdateVendorLocations(vendorId, sqlTransact);
	    
            sqlTransact.Commit();
            success = true;
            ViewState["sUserID"] = vendorId;
        }
        catch
        {
            sqlTransact.Rollback();
            success = false;
        }
        finally
        {
            sqlConnect.Close();
        }
        return success;
    }

    private void DeleteRowsInDB()
    {
        DataTable deleted = (DataTable)ViewState["Deleted"];
        CompanyTransaction companyTrans = new CompanyTransaction();
        if (deleted.Rows.Count > 0)
        {
            foreach (DataRow row in deleted.Rows)
            {
                if (row["Type"].ToString().Trim().Length > 0)
                {
                    switch (Convert.ToInt32(row["Type"]))
                    {
                        case 1:
                            companyTrans.DeletePresentSvc(row["ID"].ToString().Trim());
                            break;
                        case 2:
                            companyTrans.DeleteReference(row["ID"].ToString().Trim());
                            break;
                        case 3:
                            companyTrans.DeleteEquipment(row["ID"].ToString().Trim());
                            break;
                        case 4:
                            companyTrans.DeleteRelative(row["ID"].ToString().Trim());
                            break;
                    }
                }
            }
        }
    }

    private void UpdateUserData(int vendorId, SqlTransaction transact)
    {   
        SqlParameter[] sqlParams = new SqlParameter[5];
        sqlParams[0] = new SqlParameter("@userID", SqlDbType.Int);
        sqlParams[1] = new SqlParameter("@userType", SqlDbType.Int);
        sqlParams[2] = new SqlParameter("@userName", SqlDbType.Int);
        sqlParams[3] = new SqlParameter("@password", SqlDbType.Int);
        sqlParams[4] = new SqlParameter("@email", SqlDbType.Int);
        sqlParams[0].Value = vendorId;
        sqlParams[1].Value = 2;
        sqlParams[2].Value = tbUserName.Text.Trim();
        sqlParams[3].Value = ViewState["VendorPwd"].ToString();
        sqlParams[4].Value = tbEmail.Text.Trim();

        int tempInt = Convert.ToInt32(SqlHelper.ExecuteScalar(transact, "s3p_EBid_UpdateSpecificUser", sqlParams));
    }

    private void UpdateVendorDetail(int vendorId, SqlTransaction transact)
    {
        SqlParameter[] sqlParams = new SqlParameter[35];
        sqlParams[0] = new SqlParameter("@userId", SqlDbType.Int);
        sqlParams[0].Value = vendorId;
        sqlParams[1] = new SqlParameter("@vendorName", SqlDbType.VarChar);
        sqlParams[1].Value = tbCompanyName.Text.Trim();
        sqlParams[2] = new SqlParameter("@vendorAddress", SqlDbType.VarChar);
        sqlParams[2].Value = (tbHeadOfficeAddress1.Text.Trim().Length > 0) ? tbHeadOfficeAddress1.Text.Trim() : null;
        sqlParams[3] = new SqlParameter("@vendorAddress1", SqlDbType.VarChar);
        sqlParams[3].Value = (tbHeadOfficeAddress2.Text.Trim().Length > 0) ? tbHeadOfficeAddress2.Text.Trim() : null;
        sqlParams[4] = new SqlParameter("@vendorPhone", SqlDbType.VarChar);
        sqlParams[4].Value = (tbTelephone.Text.Trim().Length > 0) ? tbTelephone.Text.Trim() : null;
        sqlParams[5] = new SqlParameter("@vendorMobile", SqlDbType.VarChar);
        sqlParams[5].Value = (tbMobileNo.Text.Trim().Length > 0) ? tbMobileNo.Text.Trim() : null;
        sqlParams[6] = new SqlParameter("@vendorFax", SqlDbType.VarChar);
        sqlParams[6].Value = (tbFax.Text.Trim().Length > 0) ? tbFax.Text.Trim() : null;
        sqlParams[7] = new SqlParameter("@vendorExt", SqlDbType.VarChar);
        sqlParams[7].Value = (tbExtension.Text.Trim().Length > 0) ? tbExtension.Text.Trim() : null;
        sqlParams[8] = new SqlParameter("@branchAddress", SqlDbType.VarChar);
        sqlParams[8].Value = (tbBranchAddress1.Text.Trim().Length > 0) ? tbBranchAddress1.Text.Trim() : null;
        sqlParams[9] = new SqlParameter("@branchAddress1", SqlDbType.VarChar);
        sqlParams[9].Value = (tbBranchAddress2.Text.Trim().Length > 0) ? tbBranchAddress2.Text.Trim() : null;
        sqlParams[10] = new SqlParameter("@branchPhone", SqlDbType.VarChar);
        sqlParams[10].Value = (tbBranchPhone.Text.Trim().Length > 0) ? tbBranchPhone.Text.Trim() : null;
        sqlParams[11] = new SqlParameter("@branchFax", SqlDbType.VarChar);
        sqlParams[11].Value = (tbBranchFax.Text.Trim().Length > 0) ? tbBranchFax.Text.Trim() : null;
        sqlParams[12] = new SqlParameter("@branchExt", SqlDbType.VarChar);
        sqlParams[12].Value = (tbBranchExtension.Text.Trim().Length > 0) ? tbBranchExtension.Text.Trim() : null;
        sqlParams[13] = new SqlParameter("@vatRegNo", SqlDbType.VarChar);
        sqlParams[13].Value = (tbVatRegNo.Text.Trim().Length > 0) ? tbVatRegNo.Text.Trim() : null;
        sqlParams[14] = new SqlParameter("@TIN", SqlDbType.VarChar);
        sqlParams[14].Value = (tbTIN.Text.Trim().Length > 0) ? tbTIN.Text.Trim() : null;
        sqlParams[15] = new SqlParameter("@POBox", SqlDbType.VarChar);
        sqlParams[15].Value = (tbPOBox.Text.Trim().Length > 0) ? tbPOBox.Text.Trim() : null;
        sqlParams[16] = new SqlParameter("@postalCode", SqlDbType.VarChar);
        sqlParams[16].Value = (tbPostalCode.Text.Trim().Length > 0) ? tbPostalCode.Text.Trim() : null;
        sqlParams[17] = new SqlParameter("@standardTerms", SqlDbType.VarChar);
        sqlParams[17].Value = (tbTermsofPayment.Text.Trim().Length > 0) ? tbTermsofPayment.Text.Trim() : null;
        sqlParams[18] = new SqlParameter("@specialTerms", SqlDbType.VarChar);
        sqlParams[18].Value = (tbSpecialTerms.Text.Trim().Length > 0) ? tbSpecialTerms.Text.Trim() : null;
        sqlParams[19] = new SqlParameter("@minOrderVal", SqlDbType.Float);
        sqlParams[19].Value = (tbMinOrderValue.Text.Trim().Length > 0) ? float.Parse(tbMinOrderValue.Text.Trim()) : 0;
        sqlParams[20] = new SqlParameter("@salesPerson", SqlDbType.VarChar);
        sqlParams[20].Value = (tbSalesPerson.Text.Trim().Length > 0) ? tbSalesPerson.Text.Trim() : null;
        sqlParams[21] = new SqlParameter("@salesPersonPhone", SqlDbType.VarChar);
        sqlParams[21].Value = (tbSalesPersonPhone.Text.Trim().Length > 0) ? tbSalesPersonPhone.Text.Trim() : null;
        sqlParams[22] = new SqlParameter("@soleSupplier1", SqlDbType.VarChar);
        sqlParams[22].Value = (tbSoleSupplier1.Text.Trim().Length > 0) ? tbSoleSupplier1.Text.Trim() : null;
        sqlParams[23] = new SqlParameter("@soleSupplier2", SqlDbType.VarChar);
        sqlParams[23].Value = (tbSoleSupplier2.Text.Trim().Length > 0) ? tbSoleSupplier2.Text.Trim() : null;
        sqlParams[24] = new SqlParameter("@keyPersonnel", SqlDbType.Int);
        sqlParams[24].Value = (tbKeyPersonnel.Text.Trim().Length > 0) ? tbKeyPersonnel.Text.Trim() : null;
        sqlParams[25] = new SqlParameter("@kpPosition", SqlDbType.VarChar);
        sqlParams[25].Value = (tbKpPosition.Text.Trim().Length > 0) ? tbKpPosition.Text.Trim() : null;
        sqlParams[26] = new SqlParameter("@specialization", SqlDbType.VarChar);
        sqlParams[26].Value = (tbSpecialization.Text.Trim().Length > 0) ? tbSpecialization.Text.Trim() : null;
        sqlParams[27] = new SqlParameter("@category", SqlDbType.VarChar);        
        sqlParams[27].Value = "0";
        sqlParams[28] = new SqlParameter("@accredited", SqlDbType.Int);
        sqlParams[28].Value = Int32.Parse(ddlSupplierType.SelectedValue);
        sqlParams[29] = new SqlParameter("@orgTypeId", SqlDbType.Int);
        sqlParams[29].Value = Int32.Parse(rbOrganizationType.SelectedValue);
        sqlParams[30] = new SqlParameter("@ownershipFil", SqlDbType.Int);
        sqlParams[30].Value = (tbOwnershipFilipino.Text.Trim().Length > 0) ? Int32.Parse(tbOwnershipFilipino.Text.Trim()) : 0;
        sqlParams[31] = new SqlParameter("@ownershipOther", SqlDbType.Int);
        sqlParams[31].Value = (tbOwnershipOther.Text.Trim().Length > 0) ? Int32.Parse(tbOwnershipOther.Text.Trim()) : 0;
        sqlParams[32] = new SqlParameter("@classification", SqlDbType.Int);        
        sqlParams[32].Value = 0;
        sqlParams[33] = new SqlParameter("@isoStandard", SqlDbType.NVarChar);
        sqlParams[33].Value = SaveSelectedISO();
        sqlParams[34] = new SqlParameter("@pcabClass", SqlDbType.Int);
        sqlParams[34].Value = Int32.Parse(ddlPCABClass.SelectedValue);

        SqlHelper.ExecuteNonQuery(transact, "s3p_EBid_UpdateVendorDetails", sqlParams);         
    }

    private void UpdateVendorClassification(int vendorId, SqlTransaction transact)
    {
        if (vendorId > 0)
        {

            SqlParameter[] sqlparams = new SqlParameter[2];
            sqlparams[0] = new SqlParameter("@vendorId", SqlDbType.Int);
            sqlparams[0].Value = vendorId;
            sqlparams[1] = new SqlParameter("@classIds", SqlDbType.NVarChar);

            if (chkCompanyClassification_4.Checked == true)
            {
                sqlparams[1].Value = "5";
            }
            else
            {
                string tempBuf = "";

                if (chkCompanyClassification_0.Checked == true)
                {
                    tempBuf = "1";
                }

                if (chkCompanyClassification_1.Checked == true)
                {
                    if (tempBuf != "")
                        tempBuf = tempBuf + ",";

                    tempBuf += "2";
                }

                if (chkCompanyClassification_2.Checked == true)
                {
                    if (tempBuf != "")
                        tempBuf = tempBuf + ",";

                    tempBuf += "3";

                }

                if (chkCompanyClassification_3.Checked == true)
                {
                    if (tempBuf != "")
                        tempBuf = tempBuf + ",";

                    tempBuf += "4";
                }

                if (tempBuf == "")
                    tempBuf = "5";

                sqlparams[1].Value = tempBuf.Trim();
            }

            SqlHelper.ExecuteNonQuery(transact, "s3p_EBid_UpdateVendorClassification", sqlparams);
        }
    }

    private void UpdateCategoriesAndSubCategories(int vendorId, SqlTransaction transact)
    {
        SqlParameter[] sqlParams = new SqlParameter[3];
        sqlParams[0] = new SqlParameter("@vendorId", SqlDbType.Int);
        sqlParams[1] = new SqlParameter("@CategoryIds", SqlDbType.VarChar);
        sqlParams[2] = new SqlParameter("@SubCategoryIds", SqlDbType.VarChar);
        sqlParams[0].Value = vendorId;
        sqlParams[1].Value = hdnCategories.Text.Replace(",", "|");
        sqlParams[2].Value = hdnSubCategory.Text.Replace(",", "|");

        SqlHelper.ExecuteNonQuery(transact, "sp_AddVendorCategoriesAndSubCategories", sqlParams);
    }

    private void UpdatePresentServices(int vendorId, SqlTransaction transact)
    {
        if (ViewState["dtPresentSvc"] != null)
        {
            DataTable presentSvc = (DataTable)ViewState["dtPresentSvc"];            
            CompanyTransaction companyTrans = new CompanyTransaction();

            if (presentSvc.Rows.Count > 0)
            {
                foreach (DataRow row in presentSvc.Rows)
                {
                    bool isNew = true;

                    if (row["PresentServiceID"].ToString().Trim().Length > 0)
                        isNew = false;

                    if ((row["AccountNo"].ToString().Trim().Length > 0) &&
                        (row["CreditLimit"].ToString().Trim().Length > 0))
                        companyTrans.UpdatePresentServices(vendorId.ToString(), row["PlanID"].ToString().Trim(),
                                                           row["PresentServiceID"].ToString().Trim(),
                                                           row["AccountNo"].ToString().Trim(),
                                                           row["CreditLimit"].ToString().Trim(), isNew, transact);
                }
            }
        }
    }

    private void UpdateMajCustomers(int vendorId, SqlTransaction transact)
    {
        if (ViewState["MajCustomers"] != null)
        {
            CompanyTransaction companyTrans = new CompanyTransaction();            
            DataTable majCustomers = (DataTable)ViewState["MajCustomers"];

            if (majCustomers.Rows.Count > 0)
            {
                foreach (DataRow row in majCustomers.Rows)
                {
                    bool isNew = true;

                    if (row["ReferencesNo"].ToString().Trim().Length > 0)
                        isNew = false;

                    if (row["CompanyName"].ToString().Trim().Length > 0)
                        companyTrans.UpdateReference1(vendorId.ToString(), row["ReferencesNo"].ToString().Trim(),
                                                           row["CompanyName"].ToString().Trim(),
                                                           row["AveMonthlySales"].ToString().Trim(), isNew, transact);
                }
            }
        }
    }

    private void UpdateBanks(int vendorId, SqlTransaction transact)
    {
        if (ViewState["Banks"] != null)
        {
            CompanyTransaction companyTrans = new CompanyTransaction();            
            DataTable banks = (DataTable)ViewState["Banks"];

            if (banks.Rows.Count > 0)
            {
                foreach (DataRow row in banks.Rows)
                {
                    bool isNew = true;

                    if (row["ReferencesNo"].ToString().Trim().Length > 0)
                        isNew = false;

                    if (row["CompanyName"].ToString().Trim().Length > 0)
                        companyTrans.UpdateReference2(vendorId.ToString(), row["ReferencesNo"].ToString().Trim(),
                                                           row["CompanyName"].ToString().Trim(),
                                                           row["CreditLine"].ToString().Trim(), isNew, transact);
                }
            }
        }
    }

    private void UpdateAffiliates(int vendorId, SqlTransaction transact)
    {
        if (ViewState["AffCompany"] != null)
        {
            CompanyTransaction companyTrans = new CompanyTransaction();            
            DataTable affiliates = (DataTable)ViewState["AffCompany"];

            if (affiliates.Rows.Count > 0)
            {
                foreach (DataRow row in affiliates.Rows)
                {
                    bool isNew = true;

                    if (row["ReferencesNo"].ToString().Trim().Length > 0)
                        isNew = false;

                    if (row["CompanyName"].ToString().Trim().Length > 0)
                        companyTrans.UpdateReference3(vendorId.ToString(), row["ReferencesNo"].ToString().Trim(),
                                                           row["CompanyName"].ToString().Trim(),
                                                           row["KindOfBusiness"].ToString().Trim(), isNew, transact);
                }
            }
        }
    }

    private void UpdateExtAuditors(int vendorId, SqlTransaction transact)
    {
        if (ViewState["ExternAudit"] != null)
        {
            CompanyTransaction companyTrans = new CompanyTransaction();            
            DataTable extAuditors = (DataTable)ViewState["ExternAudit"];

            if (extAuditors.Rows.Count > 0)
            {
                foreach (DataRow row in extAuditors.Rows)
                {
                    bool isNew = true;

                    if (row["ReferencesNo"].ToString().Trim().Length > 0)
                        isNew = false;

                    if (row["CompanyName"].ToString().Trim().Length > 0)
                        companyTrans.UpdateReference4(vendorId.ToString(), row["ReferencesNo"].ToString().Trim(),
                                                           row["CompanyName"].ToString().Trim(),
                                                           row["LegalCounsel"].ToString().Trim(), isNew, transact);
                }
            }
        }
    }

    private void UpdateEquipments(int vendorId, SqlTransaction transact)
    {
        if (ViewState["dtEquipments"] != null)
        {
            CompanyTransaction companyTrans = new CompanyTransaction();            
            DataTable equipments = (DataTable)ViewState["dtEquipments"];

            if (equipments.Rows.Count > 0)
            {
                foreach (DataRow row in equipments.Rows)
                {
                    bool isNew = true;

                    if (row["EquipmentID"].ToString().Trim().Length > 0)
                        isNew = false;

                    if (row["EquipmentType"].ToString().Trim().Length > 0)
                        companyTrans.UpdateEquipments(vendorId.ToString(), row["EquipmentID"].ToString().Trim(),
                                                           row["EquipmentType"].ToString().Trim(),
                                                           row["Units"].ToString().Trim(),
                                                           row["Remarks"].ToString().Trim(), isNew, transact);
                }
            }
        }
    }

    private void UpdateRelatives(int vendorId, SqlTransaction transact)
    {
        if (ViewState["dtRelatives"] != null)
        {
            CompanyTransaction companyTrans = new CompanyTransaction();            
            DataTable relatives = (DataTable)ViewState["dtRelatives"];

            if (relatives.Rows.Count > 0)
            {
                foreach (DataRow row in relatives.Rows)
                {
                    bool isNew = true;

                    if (row["VendorRelativeID"].ToString().Trim().Length > 0)
                        isNew = false;

                    if (row["VendorRelative"].ToString().Trim().Length > 0)
                        companyTrans.UpdateRelatives(vendorId.ToString(), row["VendorRelativeID"].ToString().Trim(),
                                                           row["VendorRelative"].ToString().Trim(),
                                                           row["Title"].ToString().Trim(),
                                                           row["Relationship"].ToString().Trim(), isNew, transact);
                }
            }
        }
    }

    private void UpdateVendorBrands(int vendorId, SqlTransaction transact)
    {
        SqlParameter[] sqlParams = new SqlParameter[2];
        sqlParams[0] = new SqlParameter("@vendorId", SqlDbType.Int);
        sqlParams[1] = new SqlParameter("@brands", SqlDbType.NVarChar);
        sqlParams[0].Value = vendorId;
        sqlParams[1].Value = hdnBrands.Value;

        SqlHelper.ExecuteNonQuery(transact, "s3p_EBid_UpdateVendorBrands", sqlParams);
    }

    private void UpdateVendorItems(int vendorId, SqlTransaction transact)
    {
        SqlParameter[] sqlParams = new SqlParameter[2];
        sqlParams[0] = new SqlParameter("@vendorId", SqlDbType.Int);
        sqlParams[1] = new SqlParameter("@items", SqlDbType.NVarChar);
        sqlParams[0].Value = vendorId;
        sqlParams[1].Value = hdnItems.Value;

        SqlHelper.ExecuteNonQuery(transact, "s3p_EBid_UpdateVendorItems", sqlParams);
    }

    private void UpdateVendorServices(int vendorId, SqlTransaction transact)
    {
        SqlParameter[] sqlParams = new SqlParameter[2];
        sqlParams[0] = new SqlParameter("@vendorId", SqlDbType.Int);
        sqlParams[1] = new SqlParameter("@services", SqlDbType.NVarChar);
        sqlParams[0].Value = vendorId;
        sqlParams[1].Value = hdnServices.Value;

        SqlHelper.ExecuteNonQuery(transact, "s3p_EBid_UpdateVendorServices", sqlParams);
    }

    private void UpdateVendorLocations(int vendorId, SqlTransaction transact)
    {
        SqlParameter[] sqlParams = new SqlParameter[2];
        sqlParams[0] = new SqlParameter("@vendorId", SqlDbType.Int);
        sqlParams[1] = new SqlParameter("@locations", SqlDbType.NVarChar);
        sqlParams[0].Value = vendorId;
        sqlParams[1].Value = hdnLocations.Value;

        SqlHelper.ExecuteNonQuery(transact, "s3p_EBid_UpdateVendorLocations", sqlParams);
    }
    #endregion

    protected void ddlUserTypes_SelectedIndexChanged(object sender, EventArgs e)
    {
        Response.Cookies.Add(new HttpCookie("selectedusertype", ddlUserTypes.SelectedIndex.ToString().Trim()));

        if (ddlUserTypes.SelectedIndex == 0)
        {
            Response.Redirect("adduser.aspx");
        }
        else if (ddlUserTypes.SelectedIndex == 1)
        {
            Response.Redirect("aevendor.aspx");
        }
        else if (ddlUserTypes.SelectedIndex == 2)
        {
            Response.Redirect("addadmin.aspx");
        }
        else if (ddlUserTypes.SelectedIndex == 3)
        {
            Response.Redirect("addadmin.aspx");
        }
        else if (ddlUserTypes.SelectedIndex == 4)
        {
            Response.Redirect("addboc.aspx");
        }
        {
            Response.Redirect("addbac.aspx");
        }
    }

    private void UpdateIndexColumn(ref DataTable dtaTable)
    {
        if (!(dtaTable.Columns.Contains("index")))
            dtaTable.Columns.Add("index");

        if (dtaTable.Rows.Count > 0)
        {
            for (int i = 0; i < dtaTable.Rows.Count; i++)
            {
                dtaTable.Rows[i].BeginEdit();
                dtaTable.Rows[i]["index"] = i.ToString().Trim();
                dtaTable.Rows[i].EndEdit();
            }
            dtaTable.AcceptChanges();
        }
    }

    protected void WizardNavigationClick(object sender, CommandEventArgs e)
    {
        //Response.Write(e.CommandName + ": " + AddOrEdit.ToString());
        switch (e.CommandName)
        {
            case "Next":
                {
                    if (IsValid)
                        Wizard1.ActiveStepIndex++;
                } break;
            case "Previous":
                {
                    Wizard1.ActiveStepIndex--;
                } break;
            case "Cancel":
                {
                    if (AddOrEdit == "Save")
                        Response.Redirect("aevendor.aspx");
                    else
                        Response.Redirect("users.aspx");
                } break;
            case "Finish":
                {
                    if (Page.IsValid)
                    {
                        if (AddOrEdit == "Save")
                        {
                            if (SaveVendor())
                            {
                                    //SendEmail(TransactionType.Add);
                                    Session["AddVendorMessage"] = "Vendor was added successfuly!";
                                    Response.Redirect("users.aspx");
                            }
                            else 
                            {
                                Session["AddVendorMessage"] = "Failed to add new vendor! Please try again.";
                                //Response.Redirect("aevendor.aspx");
                                //Response.Redirect("users.aspx");
                            }
                        }
                        else
                        {
                            if (UpdateVendor(Int32.Parse(Request.QueryString["vid"])) && Request.QueryString["vid"] != null)
                            {
                                if ((ViewState["OLDUSERNAME"].ToString().Trim() != tbUserName.Text.ToString().Trim()) ||
                                (ViewState["VendorEmail"].ToString().Trim() != tbEmail.Text.ToString().Trim()) ||
                                (ViewState["CompanySalesPerson"].ToString().Trim() != tbSalesPerson.Text.ToString().Trim()))
                                {
                                    ResetVendorPassword();
                                    SendEmail(TransactionType.Edit);
                                    Session["AddVendorMessage"] = "Vendor information was updated successfuly!";
                                    Response.Redirect("aevendor.aspx?" + Page.ClientQueryString);
                                }
                                else
                                {
                                    SendEmail(TransactionType.Edit);
                                    Session["AddVendorMessage"] = "Vendor information was updated successfuly!";
                                    Response.Redirect("aevendor.aspx?" + Page.ClientQueryString);
                                }
                            }
                            else
                            {
                                Session["AddVendorMessage"] = "Failed to update vendor information! Please try again.";
                            }
                        }
                    }
                } break;
        }        
    }

    private void ResetVendorPassword()
    {
            string randomPwd = RandomPasswordGenerator.GenerateRandomPassword();
            SqlParameter[] sqlParams = new SqlParameter[3];
            sqlParams[0] = new SqlParameter("@UserID", Int32.Parse(Request.QueryString["vid"]));
            sqlParams[1] = new SqlParameter("@Password", EncryptionHelper.Encrypt(randomPwd) );
            sqlParams[2] = new SqlParameter("@AuthenticateUser", -1);
            SqlHelper.ExecuteNonQuery(connstring, "s3p_EBid_UpdateUserPassword", sqlParams);
    }

    #region Email
    public enum TransactionType
    {
        Add = 0,
        Edit
    };

    private bool SendEmail(TransactionType trans)
    {
        bool success = false;
        string fullname = tbCompanyName.Text.Trim();
        string subject;

        if (trans == TransactionType.Add)
            subject = "Trans-Asias : User Profile Created";
        else
            subject = "Trans-Asias : User Profile Changed";

        if (MailHelper.SendEmail(MailTemplate.GetDefaultSMTPServer(),
                MailHelper.ChangeToFriendlyName(ConfigurationManager.AppSettings["AdminEmailName"], ConfigurationManager.AppSettings["AdminEmailAddress"]),
                MailHelper.ChangeToFriendlyName(fullname, tbEmail.Text.Trim()),
                subject,
                CreateBody(trans),
                MailTemplate.GetTemplateLinkedResources(this)))
            success = true;

        return success;
    }

    private string CreateBody(TransactionType trans)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("<tr>");
        sb.Append("<td valign='top'>");
        sb.Append("<p>");
        sb.Append("<br /><b>" + DateTime.Now.ToLongDateString() + "</b><br />");
        sb.Append("<b>TO&nbsp;&nbsp;:&nbsp;&nbsp;<u>" + tbCompanyName.Text.Trim() + "</u></b>");
        sb.Append("<br /><br /><br />");
        sb.Append("Good Day!");
        sb.Append("<br /><br />");
        if (trans == TransactionType.Add)
            sb.Append("Your profile has been successfully created.");
        else
            sb.Append("Your profile has been successfully changed.");
        sb.Append("<br />&nbsp;&nbsp;&nbsp;Username: " + tbUserName.Text.Trim());
        sb.Append("<br />&nbsp;&nbsp;&nbsp;Password: " + HttpUtility.HtmlEncode(EncryptionHelper.Decrypt(GetUserPassword(Int32.Parse(ViewState["sUserID"].ToString().Trim())))));
        sb.Append("<br /><br />");
        sb.Append("You can access the site using <a href='" + ConfigurationManager.AppSettings["ServerUrl"] + "' target='_blank'>'" + ConfigurationManager.AppSettings["ServerUrl"] + "' </a>");
        sb.Append("<br /><br /><br />");
        sb.Append("Sincerely Yours,");
        sb.Append("<br /><br />");
        sb.Append(ConfigurationManager.AppSettings["AdminEmailName"]);
        sb.Append("<br /><br />");
        sb.Append("</p>");
        sb.Append("</td>");
        sb.Append("</tr>");

        return MailTemplate.IntegrateBodyIntoTemplate(sb.ToString());
    }

    public string GetUserPassword(int userid)
    {
        SqlParameter[] sqlParams = new SqlParameter[1];
        sqlParams[0] = new SqlParameter("@UserID", SqlDbType.Int);
        sqlParams[0].Value = userid;

        string s = (string)SqlHelper.ExecuteScalar(connstring, "s3p_EBid_GetUserPassword", sqlParams);

        return s;
    }
    #endregion
}

