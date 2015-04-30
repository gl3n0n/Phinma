using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using EBid.lib.bid.trans;
using System.IO;
using System.Xml;
using EBid.lib.constant;
using EBid.lib;
using EBid.ConnectionString;

public partial class web_buyerscreens_registration2 : System.Web.UI.Page
{
    private string connstring = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        FormsAuthenticationHelper.AuthenticateUserWithReturnUrl();
        connstring = Client.ConnectionString(HttpContext.Current.Session["clientid"].ToString());
        if (Int32.Parse(Session[Constant.SESSION_USERTYPE].ToString().Trim()) != (int)Constant.USERTYPE.BUYER)
            Response.Redirect("../unauthorizedaccess.aspx");

        if (!(IsPostBack))
        {
            if (Session["XMLFile"] != null)
                hdnXMLFileName.Value = Session["XMLFile"].ToString().Trim();

            BrandsTransaction b = new BrandsTransaction();
            lstBrandsCarried.DataSource = b.GetAllBrands(connstring);
            lstBrandsCarried.DataValueField = "BrandId";
            lstBrandsCarried.DataTextField = "BrandName";
            lstBrandsCarried.DataBind();

            ItemsCarriedTransaction i = new ItemsCarriedTransaction();
            lstItemsCarried.DataSource = i.GetAllItemsCarried(connstring);
            lstItemsCarried.DataValueField = "ItemNo";
            lstItemsCarried.DataTextField = "ItemsCarried";
            lstItemsCarried.DataBind();

            ServicesTransaction s = new ServicesTransaction();
            lstServicesOffered.DataSource = s.GetAllServices(connstring);
            lstServicesOffered.DataValueField = "ServiceId";
            lstServicesOffered.DataTextField = "ServiceName";
            lstServicesOffered.DataBind();

            LocationsTransaction l = new LocationsTransaction();
            lstLocation.DataSource = l.GetAllLocations(connstring);
            lstLocation.DataValueField = "LocationId";
            lstLocation.DataTextField = "LocationName";
            lstLocation.DataBind();

            PCABClassTransaction p = new PCABClassTransaction();
            ddlPCABClass.DataSource = p.GetAllPCABClass(connstring);
            ddlPCABClass.DataValueField = "PCABClassId";
            ddlPCABClass.DataTextField = "PCABClassName";
            ddlPCABClass.DataBind();
            ListItem li = new ListItem();
            li.Value = "";
            li.Text = "-- PCAB Class --";
            ddlPCABClass.Items.Insert(0, li);

            ISOStandard iso = new ISOStandard();
            chkISOStandard_white.DataSource = iso.GetISOStandard(connstring);
            chkISOStandard_white.DataTextField = "ISOStandardName";
            chkISOStandard_white.DataValueField = "ISOStandardId";
            chkISOStandard_white.DataBind();
        }
    }

    private string GetISOStandard()
    {
        string ISOStandard = "";
        for (int i = 0; i < chkISOStandard_white.Items.Count; i++ )
        {
            if (chkISOStandard_white.Items[i].Selected)
                ISOStandard += "1";
            else
                ISOStandard += "0";
 
        }
        return ISOStandard.Trim();
    }


    private void SaveSupplierData()
    {
        string vSuppliertype = "", vCategory = "", vSubcategory = "", vCompanyname = "", vHeadaddress1 = "",
                vHeadaddress2 = "", vHeadtel = "", vHeadtelext = "", vHeadfax = "", vBranchaddress1 = "", vBranchaddress2 = "",
                vBranchtel = "", vBranchtelext = "", vBranchfax = "", vVatregno = "", vTin = "", vPobox = "", vPostalcode = "", vEmail = "",
                vStandardtermsofpayment = "", vSpecialterms = "", vMinimumordervalue = "", vSalesperson = "", vSalespersontelephone = "",
                vTypeofbusinessorganization = "", vOwnershipFilipino = "", vOwnershipOther = "", vCompanyclassification = "", vSolesupplierline1 = "", vSolesupplierline2 = "",
                vSpecialization = "", vKeyPersonnel = "", vKeyPosition = "";
         
        DataTable dtPresentServices = new DataTable();
        DataTable dtMajorCustomers = new DataTable();
        DataTable dtAffiliatedCompanies = new DataTable();
        DataTable dtBanks = new DataTable();
        DataTable dtExternalAuditors = new DataTable();
        DataTable dtRelatives = new DataTable();
        DataTable dtEquipment = new DataTable();

        
        ReadFromXMLFile(ref vSuppliertype, ref vCategory, ref vSubcategory, ref vCompanyname, ref vHeadaddress1,
                ref vHeadaddress2, ref vHeadtel, ref vHeadtelext, ref vHeadfax, ref vBranchaddress1, ref vBranchaddress2,
                ref vBranchtel, ref vBranchtelext, ref vBranchfax, ref vVatregno, ref vTin, ref vPobox, ref vPostalcode, ref vEmail,
                ref vStandardtermsofpayment, ref vSpecialterms, ref vMinimumordervalue, ref vSalesperson, ref vSalespersontelephone,
                ref vTypeofbusinessorganization, ref vOwnershipFilipino,ref vOwnershipOther, ref vCompanyclassification, ref vSolesupplierline1, ref vSolesupplierline2,
                ref vSpecialization, ref vKeyPersonnel, ref vKeyPosition, ref dtPresentServices, ref dtMajorCustomers, ref dtAffiliatedCompanies, 
                ref dtBanks, ref dtExternalAuditors, ref dtEquipment, ref dtRelatives);

        SupplierTransaction s = new SupplierTransaction();
        if (!s.VendorExists(connstring, vCompanyname.Trim()))
        {
            OtherTransaction oth = new OtherTransaction();
            string vUserName = vCompanyname.Replace(" ", "");
            if (vUserName.Length > 8)
                vUserName = vUserName.Substring(0, 8).ToUpper();
            else
                vUserName = vUserName.ToUpper();

            bool IsValidUserName = s.CheckUser(connstring, vUserName.Trim());

            while (!IsValidUserName)
            {
                int ctr = 0;
                vUserName = vUserName + ctr.ToString().Trim();
                IsValidUserName = s.CheckUser(connstring, vUserName.Trim());
            }
            if (IsValidUserName)
                Session["VendorId"] = s.InsertUser(connstring, vUserName, EncryptionHelper.Encrypt(vUserName), ((int)Constant.USERTYPE.VENDOR).ToString().Trim());
            if (Session["VendorId"].ToString().Trim() != "-1")
            {
                s.RegisterSupplier(connstring, Session["VendorId"].ToString().Trim(), vCompanyname, vSuppliertype, vEmail, vHeadaddress1, vHeadaddress2, vBranchaddress1,
                    vBranchaddress2, vSalesperson, vSalespersontelephone, "",
                    vHeadtel, vHeadfax, vHeadtelext, vBranchtel, vBranchfax, vBranchtelext, vVatregno,
                    vTin, vPobox, vStandardtermsofpayment, vSpecialterms, vMinimumordervalue, vPostalcode,
                    vOwnershipFilipino, vOwnershipOther, vTypeofbusinessorganization, vSpecialization, vSolesupplierline1,
                    vSolesupplierline2, vKeyPersonnel, vKeyPosition, GetISOStandard(),
                    ddlPCABClass.SelectedItem.Value.Trim());

                string[] VendorSubCategory = vSubcategory.Split(Convert.ToChar(","));
                s.InsertSubCategory(connstring, Session["VendorId"].ToString().Trim(), VendorSubCategory);
                string[] VendorCategory = vCategory.Split(Convert.ToChar(","));
                s.InsertCategory(connstring, Session["VendorId"].ToString().Trim(), VendorCategory);
                string[] VendorClassifiction = vCompanyclassification.Split(Convert.ToChar(","));
                s.InsertVendorClassification(connstring, Session["VendorId"].ToString().Trim(), VendorClassifiction);
                s.InsertPresentServices(connstring, Session["VendorId"].ToString().Trim(), dtPresentServices);
                s.InsertMajorCustomers(connstring, Session["VendorId"].ToString().Trim(), dtMajorCustomers);
                s.InsertBanks(connstring, Session["VendorId"].ToString().Trim(), dtBanks);
                s.InsertAffiliatedCompanies(connstring, Session["VendorId"].ToString().Trim(), dtAffiliatedCompanies);
                s.InsertExternalAuditors(Session["VendorId"].ToString().Trim(), dtExternalAuditors);

                s.InsertEquipment(connstring, Session["VendorId"].ToString().Trim(), dtEquipment);
                s.InsertRelative(connstring, Session["VendorId"].ToString().Trim(), dtRelatives);

                s.InsertBrandsMain(connstring, Session["VendorId"].ToString().Trim(), hdnBrands.Text.Trim());
                s.InsertItemsCarriedMain(connstring, Session["VendorId"].ToString().Trim(), hdnItems.Text.Trim());
                s.InsertServicesOfferedMain(connstring, Session["VendorId"].ToString().Trim(), hdnServices.Text.Trim());
                s.InsertLocationMain(connstring, Session["VendorId"].ToString().Trim(), hdnLocation.Text.Trim());
                Session["XMLFile"] = null;
                Response.Redirect("supplierdetails.aspx");
            }
            else
            {
                lblMessage.Text = "Error in inserting user.";
            }
        }
        else
        {
            lblMessage.Text = "Supplier already exists.";


        }
    }
    protected void btnRegister_Click(object sender, EventArgs e)
    {
        SaveSupplierData();
    }

    private void ReadFromXMLFile(ref string vSuppliertype, ref string vCategory, ref string vSubcategory, ref string vCompanyname, ref string vHeadaddress1,
                ref string vHeadaddress2, ref string vHeadtel, ref string vHeadtelext, ref string vHeadfax, ref string vBranchaddress1, ref string vBranchaddress2,
                ref string vBranchtel, ref string vBranchtelext, ref string vBranchfax, ref string vVatregno, ref string vTin, ref string vPobox, ref string vPostalcode, ref string vEmail,
                ref string vStandardtermsofpayment, ref string vSpecialterms, ref string vMinimumordervalue, ref string vSalesperson, ref string vSalespersontelephone,
                ref string vTypeofbusinessorganization, ref string vOwnershipFilipino, ref string vOwnershipOther, ref string vCompanyclassification, ref string vSolesupplierline1, ref string vSolesupplierline2,
                ref string vSpecialization, ref string vKeyPersonnel, ref string vKeyPosition, ref DataTable dtPresentServices, 
                ref  DataTable dtMajorCustomer, ref  DataTable dtAffiliatedCompanies, ref DataTable dtBanks, ref  DataTable dtExternalAuditors, 
                ref DataTable dtEquipment, ref DataTable dtRelatives)
    {
            string filename = hdnXMLFileName.Value.Trim();
            if ((File.Exists(filename)))
            {
                XmlTextReader tr = new XmlTextReader(filename);
                while (tr.Read())
                {
                    if (tr.NodeType == XmlNodeType.Element)
                    {
                        string nodename = tr.Name.ToUpper().Trim();
                        switch (nodename)
                        {
                            case "DETAILS":
                                {
                                    while (tr.MoveToNextAttribute()) // Read the attributes.
                                    {
                                        switch (tr.Name.ToUpper().Trim())
                                        {
                                            case "SUPPLIERTYPE":
                                                vSuppliertype = tr.Value.Trim();
                                                break;
                                            case "CATEGORY":
                                                vCategory = tr.Value.Trim();
                                                break;
                                            case "SUBCATEGORY":
                                                vSubcategory = tr.Value.Trim();
                                                break;
                                            case "COMPANYNAME":
                                                vCompanyname = tr.Value.Trim();
                                                break;
                                            case "HEADADDRESS1":
                                                vHeadaddress1 = tr.Value.Trim();
                                                break;
                                            case "HEADADDRESS2":
                                                vHeadaddress2 = tr.Value.Trim();
                                                break;
                                            case "HEADTEL":
                                                vHeadtel = tr.Value.Trim();
                                                break;
                                            case "HEADTELEXT":
                                                vHeadtelext = tr.Value.Trim();
                                                break;
                                            case "HEADFAX":
                                                vHeadfax = tr.Value.Trim();
                                                break;
                                            case "BRANCHADDRESS1":
                                                vBranchaddress1 = tr.Value.Trim();
                                                break;
                                            case "BRANCHADDRESS2":
                                                vBranchaddress2 = tr.Value.Trim();
                                                break;
                                            case "BRANCHTEL":
                                                vBranchtel = tr.Value.Trim();
                                                break;
                                            case "BRANCHTELEXT":
                                                vBranchtelext = tr.Value.Trim();
                                                break;
                                            case "BRANCHFAX":
                                                vBranchfax = tr.Value.Trim();
                                                break;
                                            case "VATREGNO":
                                                vVatregno = tr.Value.Trim();
                                                break;
                                            case "TIN":
                                                vTin = tr.Value.Trim();
                                                break;
                                            case "POBOX":
                                                vPobox = tr.Value.Trim();
                                                break;
                                            case "POSTALCODE":
                                                vPostalcode = tr.Value.Trim();
                                                break;
                                            case "EMAIL":
                                                vEmail = tr.Value.Trim();
                                                break;
                                            case "STANDARDTERMSOFPAYMENT":
                                                vStandardtermsofpayment = tr.Value.Trim();
                                                break;
                                            case "SPECIALTERMS":
                                                vSpecialterms = tr.Value.Trim();
                                                break;
                                            case "MINIMUMORDERVALUE":
                                                vMinimumordervalue = tr.Value.Trim();
                                                break;
                                            case "SALESPERSON":
                                                vSalesperson = tr.Value.Trim();
                                                break;
                                            case "SALESPERSONTELEPHONE":
                                                vSalespersontelephone = tr.Value.Trim();
                                                break;
                                            case "TYPEOFBUSINESSORGANIZATION":
                                                vTypeofbusinessorganization = tr.Value.Trim();
                                                break;
                                            case "OWNERSHIPFILIPINO":
                                                vOwnershipFilipino = tr.Value.Trim();
                                                break;
                                            case "OWNERSHIPOTHER":
                                                vOwnershipOther = tr.Value.Trim();
                                                break;
                                            case "COMPANYCLASSIFICATION":
                                                vCompanyclassification = tr.Value.Trim();
                                                break;
                                            case "SOLESUPPLIERLINE1":
                                                vSolesupplierline1 = tr.Value.Trim();
                                                break;
                                            case "SOLESUPPLIERLINE2":
                                                vSolesupplierline2 = tr.Value.Trim();
                                                break;
                                            case "SPECIALIZATION":
                                                vSpecialization = tr.Value.Trim();
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "KEYPERSONNEL":
                                {
                                    while (tr.Read() && tr.NodeType != XmlNodeType.EndElement && tr.Name != "KEYPERSONNEL")
                                    {
                                        if (tr.Name.ToUpper().Trim() == "ROW")
                                        {
                                            while (tr.MoveToNextAttribute())
                                            {
                                                switch (tr.Name.Trim().ToUpper())
                                                {
                                                    case "NAME":
                                                        if (vKeyPersonnel == "")
                                                            vKeyPersonnel = tr.Value.Trim();
                                                        else
                                                            vKeyPersonnel = vKeyPersonnel.Trim() + "|"+ tr.Value.Trim();
                                                        break;
                                                    case "TITLEPOSITION":
                                                        if (vKeyPosition== "")
                                                            vKeyPosition = tr.Value.Trim();
                                                        else
                                                            vKeyPosition = vKeyPosition.Trim() + "|" + tr.Value.Trim();
                                                        break;
                                                }
                                            }
                                        }
                                    }
                                }
                                break;

                            case "PRESENTSERVICES":
                                {
                                    DataColumn dc = new DataColumn("Index", typeof(System.String));
                                    dtPresentServices.Columns.Add(dc);
                                    dc = new DataColumn("Plan", typeof(System.String));
                                    dtPresentServices.Columns.Add(dc);
                                    dc = new DataColumn("AcctNo", typeof(System.String));
                                    dtPresentServices.Columns.Add(dc);
                                    dc = new DataColumn("CreditLimit", typeof(System.String));
                                    dtPresentServices.Columns.Add(dc);
                                    
                                    while (tr.Read() && tr.NodeType != XmlNodeType.EndElement && tr.Name != "PRESENTSERVICES")
                                    {
                                        DataRow dr = dtPresentServices.NewRow();
                                        dr["Plan"] = "";
                                        dr["AcctNo"] = "";
                                        dr["CreditLimit"] = "";

                                       if (tr.Name.ToUpper().Trim() == "ROW")
                                        {
                                            
                                            while (tr.MoveToNextAttribute())
                                            {
                                                switch (tr.Name.Trim().ToUpper())
                                                {
                                                    case "PLAN":
                                                           dr["Plan"] = tr.Value.Trim();
                                                     break;
                                                     case "ACCTNO":
                                                           dr["AcctNo"] = tr.Value.Trim();
                                                     break;
                                                     case "CREDITLIMIT":
                                                           dr["CreditLimit"] = tr.Value.Trim();
                                                     break;
                                                  }
                                                        
                                                }
                                            }
                                            dtPresentServices.Rows.Add(dr);
                                        
                                        }
                                    
                                    }
                                
                                break;

                            case "MAJORCUSTOMERS":
                                {
                                    
                                    DataColumn dc = new DataColumn("Customer", typeof(System.String));
                                    dtMajorCustomer.Columns.Add(dc);
                                    dc = new DataColumn("Sale", typeof(System.String));
                                    dtMajorCustomer.Columns.Add(dc);
                                    
                                    
                                    while (tr.Read() && tr.NodeType != XmlNodeType.EndElement && tr.Name != "KEYPERSONNEL")
                                    {
                                        DataRow dr = dtMajorCustomer.NewRow();
                                        dr["Customer"] = "";
                                        dr["Sale"] = "";
                                        if (tr.Name.ToUpper().Trim() == "ROW")
                                        {
                                            while (tr.MoveToNextAttribute())
                                            {
                                                switch (tr.Name.Trim().ToUpper())
                                                {
                                                    
                                                    case "CUSTOMER":
                                                        dr["Customer"] = tr.Value.Trim();
                                                        break;
                                                    case "SALE":
                                                        dr["Sale"] = tr.Value.Trim();
                                                        break;
                                                }
                                            }
                                            dtMajorCustomer.Rows.Add(dr);
                                        }
                                    }
                                }
                                break;

                            case "BANKS":
                                {
                                    DataColumn dc = new DataColumn("Bank", typeof(System.String));
                                    dtBanks.Columns.Add(dc);
                                    dc = new DataColumn("CreditLine", typeof(System.String));
                                    dtBanks.Columns.Add(dc);
                                   
                                    while (tr.Read() && tr.NodeType != XmlNodeType.EndElement && tr.Name != "KEYPERSONNEL")
                                    {
                                        DataRow dr = dtBanks.NewRow();
                                        dr["Bank"] = "";
                                        dr["CreditLine"] = "";
                                        if (tr.Name.ToUpper().Trim() == "ROW")
                                        {
                                            while (tr.MoveToNextAttribute())
                                            {
                                                switch (tr.Name.Trim().ToUpper())
                                                {
                                                    case "BANK":
                                                        dr["Bank"] = tr.Value.Trim();
                                                        break;
                                                    case "CREDITLINE":
                                                        dr["CreditLine"] = tr.Value.Trim();
                                                        break;
                                                }
                                            }
                                            dtBanks.Rows.Add(dr);
                                        }
                                    }
                                }
                                break;

                            case "AFFILIATEDCOMPANIES":
                                {
                                    DataColumn dc = new DataColumn("Company", typeof(System.String));
                                    dtAffiliatedCompanies.Columns.Add(dc);
                                    dc = new DataColumn("Business", typeof(System.String));
                                    dtAffiliatedCompanies.Columns.Add(dc);
                                    while (tr.Read() && tr.NodeType != XmlNodeType.EndElement && tr.Name != "KEYPERSONNEL")
                                    {
                                        DataRow dr = dtAffiliatedCompanies.NewRow();
                                        dr["Company"] = "";
                                        dr["Business"] = "";
                                        if (tr.Name.ToUpper().Trim() == "ROW")
                                        {
                                            while (tr.MoveToNextAttribute())
                                            {
                                                switch (tr.Name.Trim().ToUpper())
                                                {
                                                    case "COMPANY":
                                                        dr["Company"] = tr.Value.Trim();
                                                        break;
                                                    case "BUSINESS":
                                                        dr["Business"] = tr.Value.Trim();
                                                        break;
                                                }
                                            }
                                            dtAffiliatedCompanies.Rows.Add(dr);
                                        }
                                    }
                                }
                                break;

                            case "EXTERNALAUDITORS":
                                {
                                    DataColumn dc = new DataColumn("Auditor", typeof(System.String));
                                    dtExternalAuditors.Columns.Add(dc);
                                    dc = new DataColumn("Counsel", typeof(System.String));
                                    dtExternalAuditors.Columns.Add(dc);
                                    while (tr.Read() && tr.NodeType != XmlNodeType.EndElement && tr.Name != "KEYPERSONNEL")
                                    {
                                        DataRow dr = dtExternalAuditors.NewRow();
                                        dr["Auditor"] = "";
                                        dr["Counsel"] = "";
                                        if (tr.Name.ToUpper().Trim() == "ROW")
                                        {
                                            while (tr.MoveToNextAttribute())
                                            {
                                                switch (tr.Name.Trim().ToUpper())
                                                {
                                                    case "AUDITOR":
                                                        dr["Auditor"] = tr.Value.Trim();
                                                        break;
                                                    case "COUNSEL":
                                                        dr["Counsel"] = tr.Value.Trim();
                                                        break;
                                                }
                                            }
                                            dtExternalAuditors.Rows.Add(dr);
                                        }
                                    } 
                                }
                                break;

                            case "EQUIPMENT":
                                {
                                    DataColumn dc = new DataColumn("Type", typeof(System.String));
                                    dtEquipment.Columns.Add(dc);
                                    dc = new DataColumn("Unit", typeof(System.String));
                                    dtEquipment.Columns.Add(dc);
                                    dc = new DataColumn("Remarks", typeof(System.String));
                                    dtEquipment.Columns.Add(dc);
                                    while (tr.Read() && tr.NodeType != XmlNodeType.EndElement && tr.Name != "KEYPERSONNEL")
                                    {
                                        DataRow dr = dtEquipment.NewRow();
                                        dr["Type"] = "";
                                        dr["Unit"] = "";
                                        dr["Remarks"] = "";
                                        if (tr.Name.ToUpper().Trim() == "ROW")
                                        {
                                            while (tr.MoveToNextAttribute())
                                            {
                                                switch (tr.Name.Trim().ToUpper())
                                                {
                                                    case "TYPE":
                                                        dr["Type"] = tr.Value.Trim();
                                                        break;
                                                    case "UNIT":
                                                        dr["Unit"] = tr.Value.Trim();
                                                        break;
                                                    case "REMARKS":
                                                        dr["Remarks"] = tr.Value.Trim();
                                                        break;
                                                }
                                            }
                                            dtEquipment.Rows.Add(dr);
                                        }
                                    }
                                }
                                break;

                            case "RELATIVE":
                                {
                                    DataColumn dc = new DataColumn("Name", typeof(System.String));
                                    dtRelatives.Columns.Add(dc);
                                    dc = new DataColumn("TitlePosition", typeof(System.String));
                                    dtRelatives.Columns.Add(dc);
                                    dc = new DataColumn("Relationship", typeof(System.String));
                                    dtRelatives.Columns.Add(dc);
                                   
                                    while (tr.Read() && tr.NodeType != XmlNodeType.EndElement && tr.Name != "KEYPERSONNEL")
                                    {
                                        DataRow dr = dtRelatives.NewRow();
                                        dr["Name"] = "";
                                        dr["TitlePosition"] = "";
                                        dr["Relationship"] = "";
                                        if (tr.Name.ToUpper().Trim() == "ROW")
                                        {
                                            while (tr.MoveToNextAttribute())
                                            {
                                                switch (tr.Name.Trim().ToUpper())
                                                {
                                                    case "NAME":
                                                        dr["Name"] = tr.Value.Trim();
                                                        break;
                                                    case "TITLEPOSITION":
                                                        dr["TitlePosition"] = tr.Value.Trim();
                                                        break;
                                                    case "RELATIONSHIP":
                                                        dr["RELATIONSHIP"] = tr.Value.Trim();
                                                        break;
                                                }
                                            }
                                            dtRelatives.Rows.Add(dr);
                                        }
                                     }
                                }
                                break;
                        }
                    }
                }
                tr.Close();
            }
    }
}
