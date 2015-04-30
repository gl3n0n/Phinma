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
using System.Xml;
using EBid.lib.constant;
using System.IO;
using EBid.lib;
using EBid.ConnectionString;

public partial class web_buyer_screens_registration : System.Web.UI.Page
{
    #region "PageEvents"

    private string connstring = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        FormsAuthenticationHelper.AuthenticateUserWithReturnUrl();
        connstring = Client.ConnectionString(HttpContext.Current.Session["clientid"].ToString());
        if (Int32.Parse(Session[Constant.SESSION_USERTYPE].ToString().Trim()) != (int)Constant.USERTYPE.BUYER)
            Response.Redirect("../unauthorizedaccess.aspx");

        if (!(IsPostBack))
        {
            SupplierType sty = new SupplierType();
            ddlSupplierType.DataSource = sty.GetAllSupplierTypes(connstring);
            ddlSupplierType.DataTextField = "SupplierTypeDesc";
            ddlSupplierType.DataValueField = "SupplierTypeId";
            ddlSupplierType.DataBind();
            ListItem l2 = new ListItem();
            l2.Value = "";
            l2.Text = "---Supplier Type---";
            ddlSupplierType.Items.Insert(0, l2);

            txtOwnershipFilipino.Attributes.Add("onBlur", "AutoCompute();");
            txtOtherNationality.Attributes.Add("onBlur", "AutoCompute();");
            btnNext.Attributes.Add("onClick", "BranchValidator();");

            CategoryTransaction c = new CategoryTransaction();
            lstCategories.DataSource = c.GetAllCategories(connstring);
            lstCategories.DataTextField = "CategoryName";
            lstCategories.DataValueField = "CategoryId";
            lstCategories.DataBind();

            btnSelectCategory.Attributes.Add("onClick", "copyToCategoryList();");
            btnSelectAllCategories.Attributes.Add("onClick", "copyAllCategories();");
            btnDeSelectCategory.Attributes.Add("onClick", "removeAllCategories();");
            btnDeSelectAllCategories.Attributes.Add("onClick", "removeFromCategoryList();");
            lstCategories.Attributes.Add("onDblClick", "copyACategory();");
            lstSelectedCategories.Attributes.Add("onDblClick", "removeACategory();");

            btnSelectSubCategory.Attributes.Add("onClick","copyToSubCategoryList();");
            btnSelectAllSubCategories.Attributes.Add("onClick", "copyAllSubCategories();");
            btnDeSelectAllSubCategories.Attributes.Add("onClick", "removeAllSubCategories();");
            btnDeSelectSubCategory.Attributes.Add("onClick", "removeFromSubCategoryList();");
            lstSubCategory.Attributes.Add("onDblClick", "copyASubCategory();");
            lstSelectedSubCategories.Attributes.Add("onDblClick", "removeASubCategory();");

            gvKeyPersonnel.DataSource = CreateEmptyKeyPersonell();
            gvKeyPersonnel.DataBind();
            
            gvPresentServices.DataSource = CreateEmptyPresentServices();
            gvPresentServices.DataBind();
            
            gvMajorCustomers.DataSource = CreateEmptyMajorCustomers();
            gvMajorCustomers.DataBind();
            
            gvBanks.DataSource = CreateEmptyBanks();
            gvBanks.DataBind();
            
            gvAffiliatedCompanies.DataSource = CreateEmptyAffiliatedCompanies();
            gvAffiliatedCompanies.DataBind();
            
            gvExternalAuditors.DataSource = CreateEmptyExternalAuditor();
            gvExternalAuditors.DataBind();
            
            gvEquipment.DataSource = CreateEmptyEquipment();
            gvEquipment.DataBind();
            
            gvRelatives.DataSource = CreateEmptyRelativesTable();
            gvRelatives.DataBind();
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
            DataSet ds = su.QuerySelectedAndUnselectedCategories(connstring, category.Trim());

            if (ds.Tables.Count >0)
            {
                if (ds.Tables[0] != null)
                {
                    DataTable selectedcategories = ds.Tables[0];
                    lstSelectedCategories.DataSource = selectedcategories;
                    lstSelectedCategories.DataTextField = "CategoryName";
                    lstSelectedCategories.DataValueField = "CategoryId";
                    lstSelectedCategories.DataBind();
                }
                if (ds.Tables[1] != null)
                {
                    DataTable availcategories = ds.Tables[1];
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
            DataSet ds1 = su.QuerySelectedAndUnselectedSubCategories(connstring, hdnSubCategory.Text.Trim(), category.Trim() );
            if (ds1.Tables.Count > 0)
            {
                if (ds1.Tables[0] != null)
                {
                    DataTable selectedsubcategories = ds1.Tables[0];
                    lstSelectedSubCategories.DataSource = selectedsubcategories;
                    lstSelectedSubCategories.DataTextField = "SubCategoryName";
                    lstSelectedSubCategories.DataValueField = "SubCategoryId";
                    lstSelectedSubCategories.DataBind();
                }
                if (ds1.Tables[1] != null)
                {
                    DataTable availsubcategories = ds1.Tables[1];
                    lstSubCategory.DataSource = availsubcategories;
                    lstSubCategory.DataTextField = "SubCategoryName";
                    lstSubCategory.DataValueField = "SubCategoryId";
                    lstSubCategory.DataBind();
                }
            }
        }

    }

    protected void btnNext_Click(object sender, EventArgs e)
    {
        WriteAllData();
        Response.Redirect("registration2.aspx");
    }

    

    

    protected void lnkPresentServices_Click(object sender, EventArgs e)
    {
        hdnAddAPresentService.Value = "1";
        hdnFocusToLastPresentService.Value = "1";
        WriteAllData();
        ReadFromXML();
    }

    protected void lnkMajorCustomers_Click(object sender, EventArgs e)
    {
        hdnAddMajorCustomer.Value = "1";
        hdnFocusToLastMajorCustomer.Value = "1";
        WriteAllData();
        ReadFromXML();
    }

    protected void lnkBanks_Click(object sender, EventArgs e)
    {
        hdnAddABank.Value = "1";
        hdnFocusToLastBank.Value = "1";
        WriteAllData();
        ReadFromXML();
    }

    protected void lnkAffiliatedCompanies_Click(object sender, EventArgs e)
    {
        hdnAddACompany.Value = "1";
        hdnFocusToLastAffiliatedCompany.Value = "1";
        WriteAllData();
        ReadFromXML();
    }

    protected void lnkExternalAuditors_Click(object sender, EventArgs e)
    {
        hdnAddAnAuditor.Value = "1";
        hdnFocusToLastExternalAuditor.Value = "1";
        WriteAllData();
        ReadFromXML();
    }

    protected void lnkEquipment_Click(object sender, EventArgs e)
    {
        hdnAddAnEquipment.Value = "1";
        hdnFocusToLastEquipment.Value = "1";
        WriteAllData();
        ReadFromXML();
    }

    protected void lnkAddRelative_Click(object sender, EventArgs e)
    {
        hdnAddARelative.Value = "1";
        hdnFocusToLastRelative.Value = "1";
        WriteAllData();
        ReadFromXML();
    }
    #endregion
    #region "Code"

    #region "CreatEmptyTablesAndRows"

    private DataTable CreateEmptyKeyPersonell()
    {
        DataTable dt = new DataTable();
        DataColumn dcol = new DataColumn("Index", typeof(System.String));
        dt.Columns.Add(dcol);
        DataColumn dcol1 = new DataColumn("Name", typeof(System.String));
        dt.Columns.Add(dcol1);
        DataColumn dcol2 = new DataColumn("Position", typeof(System.String));
        dt.Columns.Add(dcol2);

        DataRow dr = dt.NewRow();
        dr["Index"] = 0;
        dr["Name"] = "";
        dr["Position"] = "";
        dt.Rows.Add(dr);

        return dt;
    }

    private DataTable CreateEmptyKeyPersonellRow(ref DataTable dt)
    {
        DataRow dr = dt.NewRow();
        dr["Index"] = dt.Rows.Count;
        dr["Name"] = "";
        dr["Position"] = "";
        dt.Rows.Add(dr);

        return dt;
    }

    private DataTable CreateEmptyPresentServices()
    {
        DataTable dt = new DataTable();
        DataColumn dcol = new DataColumn("Index", typeof(System.String));
        dt.Columns.Add(dcol);
        dcol = new DataColumn("Plan", typeof(System.String));
        dt.Columns.Add(dcol);
        dcol = new DataColumn("AcctNo", typeof(System.String));
        dt.Columns.Add(dcol);
        dcol = new DataColumn("CreditLimit", typeof(System.String));
        dt.Columns.Add(dcol);

        DataRow dr = dt.NewRow();
        dr["Index"] = "";
        dr["Plan"] = "";
        dr["AcctNo"] = "";
        dr["CreditLimit"] = "";
        dt.Rows.Add(dr);

        return dt;
    }

    private DataTable CreateEmptyPresentServicesRow(ref DataTable dt)
    {
        DataRow dr = dt.NewRow();
        dr["Index"] = dt.Rows.Count;
        dr["Plan"] = "";
        dr["AcctNo"] = "";
        dr["CreditLimit"] = "";
        dt.Rows.Add(dr);

        return dt;
    }

    private DataTable CreateEmptyMajorCustomers()
    {
        DataTable dt = new DataTable();
        DataColumn dcol = new DataColumn("Index", typeof(System.String));
        dt.Columns.Add(dcol);
        dcol = new DataColumn("Customer", typeof(System.String));
        dt.Columns.Add(dcol);
        dcol = new DataColumn("Sale", typeof(System.String));
        dt.Columns.Add(dcol);
        
        DataRow dr = dt.NewRow();
        dr["Index"] = "";
        dr["Customer"] = "";
        dr["Sale"] = "";
        dt.Rows.Add(dr);

        return dt;
    }

    private DataTable CreateEmptyMajorCustomersRow(ref DataTable dt)
    {
        DataRow dr = dt.NewRow();
        dr["Index"] = dt.Rows.Count;
        dr["Customer"] = "";
        dr["Sale"] = "";
        dt.Rows.Add(dr);

        return dt;
    }

    private DataTable CreateEmptyBanks()
    {
        DataTable dt = new DataTable();
        DataColumn dcol = new DataColumn("Index", typeof(System.String));
        dt.Columns.Add(dcol);
        dcol = new DataColumn("Bank", typeof(System.String));
        dt.Columns.Add(dcol);
        dcol = new DataColumn("CreditLine", typeof(System.String));
        dt.Columns.Add(dcol);
        
        DataRow dr = dt.NewRow();
        dr["Index"] = "";
        dr["Bank"] = "";
        dr["CreditLine"] = "";
        dt.Rows.Add(dr);

        return dt;
    }

    private DataTable CreateEmptyBanksRow(ref DataTable dt)
    {
        DataRow dr = dt.NewRow();
        dr["Index"] = dt.Rows.Count;
        dr["Bank"] = "";
        dr["CreditLine"] = "";
        dt.Rows.Add(dr);

        return dt;
    }

    private DataTable CreateEmptyAffiliatedCompanies()
    {
        DataTable dt = new DataTable();
        DataColumn dcol = new DataColumn("Index", typeof(System.String));
        dt.Columns.Add(dcol);
        dcol = new DataColumn("Company", typeof(System.String));
        dt.Columns.Add(dcol);
        dcol = new DataColumn("Business", typeof(System.String));
        dt.Columns.Add(dcol);

        DataRow dr = dt.NewRow();
        dr["Index"] = "";
        dr["Company"] = "";
        dr["Business"] = "";
        dt.Rows.Add(dr);

        return dt;
    }


    private DataTable CreateEmptyAffiliatedCompaniesRow(ref DataTable dt)
    {
        DataRow dr = dt.NewRow();
        dr["Index"] = "";
        dr["Company"] = "";
        dr["Business"] = "";
        dt.Rows.Add(dr);

        return dt;
    }
    private DataTable CreateEmptyExternalAuditor()
    {
        DataTable dt = new DataTable();
        DataColumn dcol = new DataColumn("Index", typeof(System.String));
        dt.Columns.Add(dcol);
        dcol = new DataColumn("Auditor", typeof(System.String));
        dt.Columns.Add(dcol);
        dcol = new DataColumn("Counsel", typeof(System.String));
        dt.Columns.Add(dcol);
        
        DataRow dr = dt.NewRow();
        dr["Index"] = "";
        dr["Auditor"] = "";
        dr["Counsel"] = "";
        dt.Rows.Add(dr);

        return dt;
    }

    private DataTable CreateEmptyExternalAuditorRow(ref DataTable dt)
    {
        DataRow dr = dt.NewRow();
        dr["Index"] = "";
        dr["Auditor"] = "";
        dr["Counsel"] = "";
        dt.Rows.Add(dr);

        return dt;
    }

    private DataTable CreateEmptyEquipment()
    {
        DataTable dt = new DataTable();
        DataColumn dcol = new DataColumn("Index", typeof(System.String));
        dt.Columns.Add(dcol);
        dcol = new DataColumn("Type", typeof(System.String));
        dt.Columns.Add(dcol);
        dcol = new DataColumn("Unit", typeof(System.String));
        dt.Columns.Add(dcol);
        dcol = new DataColumn("Remarks", typeof(System.String));
        dt.Columns.Add(dcol);

        DataRow dr = dt.NewRow();
        dr["Index"] = "";
        dr["Type"] = "";
        dr["Unit"] = "";
        dr["Remarks"] = "";
        dt.Rows.Add(dr);

        return dt;
    }

    private DataTable CreateEmptyEquipmentRow(ref DataTable dt)
    {
        DataRow dr = dt.NewRow();
        dr["Index"] = "";
        dr["Type"] = "";
        dr["Unit"] = "";
        dr["Remarks"] = "";
        dt.Rows.Add(dr);

        return dt;
    }

    private DataTable CreateEmptyRelativesTable()
    {
        DataTable dt = new DataTable();
        DataColumn dcol = new DataColumn("Index", typeof(System.String));
        dt.Columns.Add(dcol);
        dcol = new DataColumn("Name", typeof(System.String));
        dt.Columns.Add(dcol);
        dcol = new DataColumn("TitlePosition", typeof(System.String));
        dt.Columns.Add(dcol);
        dcol = new DataColumn("Relationship", typeof(System.String));
        dt.Columns.Add(dcol);

        DataRow dr = dt.NewRow();
        dr["Index"] = "";
        dr["Name"] = "";
        dr["TitlePosition"] = "";
        dr["Relationship"] = "";
        dt.Rows.Add(dr);

        return dt;
    }

    private DataTable CreateEmptyRelativesRow(ref DataTable dt)
    {
        DataRow dr = dt.NewRow();
        dr["Index"] = "";
        dr["Name"] = "";
        dr["TitlePosition"] = "";
        dr["Relationship"] = "";
        dt.Rows.Add(dr);

        return dt;
    }

    #endregion "CreatEmptyTablesAndRows"

    #region "CreateXMLFile"
    private void CreateNewXMLFileName()
    {
        string filename = Constant.TEMP_SUPPLIER_STORE.Trim();
        string strDirectory = Constant.TEMPDIR.Trim();
        if (!(Directory.Exists(strDirectory)))
            Directory.CreateDirectory(strDirectory);
        string ofilename = strDirectory + "\\" + filename + ".xml";
        Boolean FileExists = File.Exists(ofilename);
        int ctr = 0;
        while (FileExists)
        {
            ofilename = strDirectory + "\\" + filename + ctr.ToString().Trim() + ".xml";
            FileExists = File.Exists(ofilename);
            if (FileExists)
                ctr++;
        }
        Session["XMLFile"] = ofilename.Trim();
        hdnXMLFileName.Value = Session["XMLFile"].ToString().Trim();
    }

    private void WriteAllData()
    {
        WriteToXMLFile();
        WriteKeyPersonnelToXML();
        WritePresentServicesToXML();
        WriteMajorCustomersToXML();
        WriteBanksToXML();
        WriteAffiliatedCompaniesToXML();
        WriteExternalAuditorsToXML();
        WriteEquipmentToXML();
        WriteRelativeToXML();
    }

    private string GetTypeOfBusinessOrganization()
    {
        if (chkOrgType_Individual.Checked)
            return Constant.TYPE_INDIVIDUAL.ToString().Trim();
        if (chkOrgType_Partnership.Checked)
            return Constant.TYPE_PARTNERSHIP.ToString().Trim();
        if (chkOrgType_Corporation.Checked)
            return Constant.TYPE_CORPORATION.ToString().Trim();
        return "";
    }

    private string GetCompanyClassification()
    {
        string CompanyClass = "";
        if (chkCompanyClassification_0.Checked)
            CompanyClass += "," + Constant.COMPANYCLASS_MANUFACTURER.ToString().Trim();
        if (chkCompanyClassification_1.Checked)
            CompanyClass += "," + Constant.COMPANYCLASS_IMPORTER.ToString().Trim();
        if (chkCompanyClassification_2.Checked)
            CompanyClass += "," + Constant.COMPANYCLASS_DEALER.ToString().Trim();
        if (chkCompanyClassification_3.Checked)
            CompanyClass += "," + Constant.COMPANYCLASS_TRADER.ToString().Trim();
        if (chkCompanyClassification_4.Checked)
            CompanyClass += "," + Constant.COMPANYCLASS_NA.ToString().Trim();
        return CompanyClass;
    }
    
    private void WriteToXMLFile()
    {
        string filename = "";
        if (Session["XMLFile"] == null)
        {
            CreateNewXMLFileName();
        }
        else
        {
            if (Session["XMLFile"].ToString().Trim() == "")
            {
                CreateNewXMLFileName();
            }
            else
            {
                hdnXMLFileName.Value = Session["XMLFile"].ToString().Trim();
            }
        }

        filename = hdnXMLFileName.Value.Trim();
        XmlTextWriter tw = new XmlTextWriter(filename, null);

        tw.Formatting = Formatting.Indented;
        tw.WriteStartDocument();
        tw.WriteStartElement("SUPPLIER");
        tw.WriteStartElement("DETAILS");
        tw.WriteAttributeString("suppliertype", ddlSupplierType.SelectedItem.Value.Trim());
        tw.WriteAttributeString("category", hdnCategories.Text.Trim());
        tw.WriteAttributeString("subcategory", hdnSubCategory.Text.Trim());
        tw.WriteAttributeString("companyname", txtCompanyName.Value.Trim());
        tw.WriteAttributeString("headaddress1", txtHeadAddress1.Value.Trim());
        tw.WriteAttributeString("headaddress2", txtHeadAddress2.Value.Trim());
        tw.WriteAttributeString("headtel", txtHeadTel.Value.Trim());
        tw.WriteAttributeString("headtelext", txtHeadExt.Value.Trim());
        tw.WriteAttributeString("headfax", txtHeadFax.Value.Trim());
        tw.WriteAttributeString("branchaddress1", txtBranchAddress1.Value.Trim());
        tw.WriteAttributeString("branchaddress2", txtBranchAddress2.Value.Trim());
        tw.WriteAttributeString("branchtel", txtBranchTel.Value.Trim());
        tw.WriteAttributeString("branchtelext", txtBranchExt.Value.Trim());
        tw.WriteAttributeString("branchfax", txtBranchFax.Value.Trim());
        tw.WriteAttributeString("vatregno", txtVatRegNo.Value.Trim());
        tw.WriteAttributeString("tin", txtTIN.Value.Trim());
        tw.WriteAttributeString("pobox", txtPOBOX.Value.Trim());
        tw.WriteAttributeString("postalcode", txtPostalCode.Value.Trim());
        tw.WriteAttributeString("email", txtEmail.Value.Trim());
        tw.WriteAttributeString("standardtermsofpayment", txtStandardTermsOfPayment.Value.Trim());
        tw.WriteAttributeString("specialterms", txtSpecialTerms.Value.Trim());
        tw.WriteAttributeString("minimumordervalue", txtMinOrderValue.Value.Trim());
        tw.WriteAttributeString("salesperson", txtSalesPerson.Value.Trim());
        tw.WriteAttributeString("salespersontelephone", txtSalesPersonTel.Value.Trim());
        tw.WriteAttributeString("typeofbusinessorganization", GetTypeOfBusinessOrganization());
        tw.WriteAttributeString("ownershipfilipino", txtOwnershipFilipino.Value.Trim());
        tw.WriteAttributeString("ownershipother", txtOtherNationality.Value.Trim());
        tw.WriteAttributeString("companyclassification", GetCompanyClassification());
        tw.WriteAttributeString("solesupplierline1", txtSoleSupplier.Value.Trim());
        tw.WriteAttributeString("solesupplierline2", txtSoleSupplier2.Value.Trim());
        tw.WriteAttributeString("specialization", txtSpecialization.Value.Trim());
        tw.WriteEndElement(); //end details
        tw.WriteEndElement();//end supplier
        tw.WriteEndDocument();//end start
        tw.Flush();
        tw.Close();

    }

    private void WriteKeyPersonnelToXML()
    {
        int ctr = 0;

        hdnXMLFileName.Value = Session["XMLFile"].ToString().Trim();
        XmlDocument doc = new XmlDocument();
        doc.Load(hdnXMLFileName.Value.Trim());

        foreach (GridViewRow row in gvKeyPersonnel.Rows)
        {
            string strName = ((TextBox)row.FindControl("txtName")).Text.Trim();
            string strTitlePosition = ((TextBox)row.FindControl("txtTitlePosition")).Text.Trim();

            XmlNode node = doc.SelectSingleNode("//KEYPERSONNEL");
            if (node != null)
            {
                XmlElement newRow = doc.CreateElement("ROW");
                XmlAttribute newAttr = doc.CreateAttribute("id");
                newAttr.Value = ctr.ToString().Trim();
                newRow.Attributes.Append(newAttr);

                newAttr = doc.CreateAttribute("name");
                newAttr.Value = strName.Trim();
                newRow.Attributes.Append(newAttr);

                newAttr = doc.CreateAttribute("titleposition");
                newAttr.Value = strTitlePosition.Trim();
                newRow.Attributes.Append(newAttr);

                node.AppendChild(newRow);
            }
            else
            {
                XmlElement newElem = doc.CreateElement("KEYPERSONNEL");
                XmlElement newRow = doc.CreateElement("ROW");
                XmlAttribute newAttr = doc.CreateAttribute("id");
                newAttr.Value = ctr.ToString().Trim();
                newRow.Attributes.Append(newAttr);

                newAttr = doc.CreateAttribute("name");
                newAttr.Value = strName.Trim();
                newRow.Attributes.Append(newAttr);

                newAttr = doc.CreateAttribute("titleposition");
                newAttr.Value = strTitlePosition.Trim();
                newRow.Attributes.Append(newAttr);

                newElem.AppendChild(newRow);
                doc.DocumentElement.AppendChild(newElem);
            }
            ctr++;
        }

        doc.PreserveWhitespace = true;
        XmlTextWriter wrtr = new XmlTextWriter(hdnXMLFileName.Value.Trim(), null);
        doc.WriteTo(wrtr);
        wrtr.Close();
    }

    private void WritePresentServicesToXML()
    {
        int ctr = 0;

        hdnXMLFileName.Value = Session["XMLFile"].ToString().Trim();
        XmlDocument doc = new XmlDocument();
        doc.Load(hdnXMLFileName.Value.Trim());

        foreach (GridViewRow row in gvPresentServices.Rows)
        {
            string strTypeOfPlan = ((DropDownList)row.FindControl("ddlTypeOfPlan")).SelectedItem.Value.Trim();
            string strAcctNo = ((TextBox)row.FindControl("txtAcctNo")).Text.Trim();
            string strCreditLimit = ((TextBox)row.FindControl("txtCreditLimit")).Text.Trim();

            XmlNode node = doc.SelectSingleNode("//PRESENTSERVICES");
            if (node != null)
            {
                XmlElement newRow = doc.CreateElement("ROW");
                XmlAttribute newAttr = doc.CreateAttribute("id");
                newAttr.Value = ctr.ToString().Trim();
                newRow.Attributes.Append(newAttr);

                newAttr = doc.CreateAttribute("plan");
                newAttr.Value = strTypeOfPlan.Trim();
                newRow.Attributes.Append(newAttr);

                newAttr = doc.CreateAttribute("acctno");
                newAttr.Value = strAcctNo.Trim();
                newRow.Attributes.Append(newAttr);

                newAttr = doc.CreateAttribute("creditlimit");
                newAttr.Value = strCreditLimit.Trim();
                newRow.Attributes.Append(newAttr);

                node.AppendChild(newRow);
            }
            else
            {
                XmlElement newElem = doc.CreateElement("PRESENTSERVICES");
                XmlElement newRow = doc.CreateElement("ROW");
                XmlAttribute newAttr = doc.CreateAttribute("id");
                newAttr.Value = ctr.ToString().Trim();
                newRow.Attributes.Append(newAttr);

                newAttr = doc.CreateAttribute("plan");
                newAttr.Value = strTypeOfPlan.Trim();
                newRow.Attributes.Append(newAttr);

                newAttr = doc.CreateAttribute("acctno");
                newAttr.Value = strAcctNo.Trim();
                newRow.Attributes.Append(newAttr);

                newAttr = doc.CreateAttribute("creditlimit");
                newAttr.Value = strCreditLimit.Trim();
                newRow.Attributes.Append(newAttr);

                newElem.AppendChild(newRow);
                doc.DocumentElement.AppendChild(newElem);
            }
            ctr++;
        }

        doc.PreserveWhitespace = true;
        XmlTextWriter wrtr = new XmlTextWriter(hdnXMLFileName.Value.Trim(), null);
        doc.WriteTo(wrtr);
        wrtr.Close();
    }

    private void WriteMajorCustomersToXML()
    {
        int ctr = 0;

        hdnXMLFileName.Value = Session["XMLFile"].ToString().Trim();
        XmlDocument doc = new XmlDocument();
        doc.Load(hdnXMLFileName.Value.Trim());

        foreach (GridViewRow row in gvMajorCustomers.Rows)
        {
            string strMajorCust = ((TextBox)row.FindControl("txtMajorCust")).Text.Trim();
            string strAveMonthSa = ((TextBox)row.FindControl("txtAveMonthlySales")).Text.Trim();

            XmlNode node = doc.SelectSingleNode("//MAJORCUSTOMERS");
            if (node != null)
            {
                XmlElement newRow = doc.CreateElement("ROW");
                XmlAttribute newAttr = doc.CreateAttribute("id");
                newAttr.Value = ctr.ToString().Trim();
                newRow.Attributes.Append(newAttr);

                newAttr = doc.CreateAttribute("customer");
                newAttr.Value = strMajorCust.Trim();
                newRow.Attributes.Append(newAttr);

                newAttr = doc.CreateAttribute("sale");
                newAttr.Value = strAveMonthSa.Trim();
                newRow.Attributes.Append(newAttr);

                node.AppendChild(newRow);
            }
            else
            {
                XmlElement newElem = doc.CreateElement("MAJORCUSTOMERS");
                XmlElement newRow = doc.CreateElement("ROW");
                XmlAttribute newAttr = doc.CreateAttribute("id");
                newAttr.Value = ctr.ToString().Trim();
                newRow.Attributes.Append(newAttr);

                newAttr = doc.CreateAttribute("customer");
                newAttr.Value = strMajorCust.Trim();
                newRow.Attributes.Append(newAttr);

                newAttr = doc.CreateAttribute("sale");
                newAttr.Value = strAveMonthSa.Trim();
                newRow.Attributes.Append(newAttr);

                newElem.AppendChild(newRow);
                doc.DocumentElement.AppendChild(newElem);
            }
            ctr++;
        }

        doc.PreserveWhitespace = true;
        XmlTextWriter wrtr = new XmlTextWriter(hdnXMLFileName.Value.Trim(), null);
        doc.WriteTo(wrtr);
        wrtr.Close();
    }

    private void WriteBanksToXML()
    {
        int ctr = 0;

        hdnXMLFileName.Value = Session["XMLFile"].ToString().Trim();
        XmlDocument doc = new XmlDocument();
        doc.Load(hdnXMLFileName.Value.Trim());

        foreach (GridViewRow row in gvBanks.Rows)
        {
            string strBank = ((TextBox)row.FindControl("txtBanks")).Text.Trim();
            string strCreditLine = ((TextBox)row.FindControl("txtCreditLine")).Text.Trim();

            XmlNode node = doc.SelectSingleNode("//BANKS");
            if (node != null)
            {
                XmlElement newRow = doc.CreateElement("ROW");
                XmlAttribute newAttr = doc.CreateAttribute("id");
                newAttr.Value = ctr.ToString().Trim();
                newRow.Attributes.Append(newAttr);

                newAttr = doc.CreateAttribute("bank");
                newAttr.Value = strBank.Trim();
                newRow.Attributes.Append(newAttr);

                newAttr = doc.CreateAttribute("creditline");
                newAttr.Value = strCreditLine.Trim();
                newRow.Attributes.Append(newAttr);

                node.AppendChild(newRow);
            }
            else
            {
                XmlElement newElem = doc.CreateElement("BANKS");
                XmlElement newRow = doc.CreateElement("ROW");
                XmlAttribute newAttr = doc.CreateAttribute("id");
                newAttr.Value = ctr.ToString().Trim();
                newRow.Attributes.Append(newAttr);

                newAttr = doc.CreateAttribute("bank");
                newAttr.Value = strBank.Trim();
                newRow.Attributes.Append(newAttr);

                newAttr = doc.CreateAttribute("creditline");
                newAttr.Value = strCreditLine.Trim();
                newRow.Attributes.Append(newAttr);

                newElem.AppendChild(newRow);
                doc.DocumentElement.AppendChild(newElem);
            }
            ctr++;
        }

        doc.PreserveWhitespace = true;
        XmlTextWriter wrtr = new XmlTextWriter(hdnXMLFileName.Value.Trim(), null);
        doc.WriteTo(wrtr);
        wrtr.Close();
    }


    private void WriteAffiliatedCompaniesToXML()
    {
        int ctr = 0;

        hdnXMLFileName.Value = Session["XMLFile"].ToString().Trim();
        XmlDocument doc = new XmlDocument();
        doc.Load(hdnXMLFileName.Value.Trim());

        foreach (GridViewRow row in gvAffiliatedCompanies.Rows)
        {
            string strCompany = ((TextBox)row.FindControl("txtAffiliatedCompanies")).Text.Trim();
            string strBusiness = ((TextBox)row.FindControl("txtKindOfBusiness")).Text.Trim();

            XmlNode node = doc.SelectSingleNode("//AFFILIATEDCOMPANIES");
            if (node != null)
            {
                XmlElement newRow = doc.CreateElement("ROW");
                XmlAttribute newAttr = doc.CreateAttribute("id");
                newAttr.Value = ctr.ToString().Trim();
                newRow.Attributes.Append(newAttr);

                newAttr = doc.CreateAttribute("company");
                newAttr.Value = strCompany.Trim();
                newRow.Attributes.Append(newAttr);

                newAttr = doc.CreateAttribute("business");
                newAttr.Value = strBusiness.Trim();
                newRow.Attributes.Append(newAttr);

                node.AppendChild(newRow);
            }
            else
            {
                XmlElement newElem = doc.CreateElement("AFFILIATEDCOMPANIES");
                XmlElement newRow = doc.CreateElement("ROW");
                XmlAttribute newAttr = doc.CreateAttribute("id");
                newAttr.Value = ctr.ToString().Trim();
                newRow.Attributes.Append(newAttr);

                newAttr = doc.CreateAttribute("company");
                newAttr.Value = strCompany.Trim();
                newRow.Attributes.Append(newAttr);

                newAttr = doc.CreateAttribute("business");
                newAttr.Value = strBusiness.Trim();
                newRow.Attributes.Append(newAttr);

                newElem.AppendChild(newRow);
                doc.DocumentElement.AppendChild(newElem);
            }
            ctr++;
        }

        doc.PreserveWhitespace = true;
        XmlTextWriter wrtr = new XmlTextWriter(hdnXMLFileName.Value.Trim(), null);
        doc.WriteTo(wrtr);
        wrtr.Close();
    }

    private void WriteExternalAuditorsToXML()
    {
        int ctr = 0;

        hdnXMLFileName.Value = Session["XMLFile"].ToString().Trim();
        XmlDocument doc = new XmlDocument();
        doc.Load(hdnXMLFileName.Value.Trim());

        foreach (GridViewRow row in gvExternalAuditors.Rows)
        {
            string strAuditor = ((TextBox)row.FindControl("txtExternalAuditors")).Text.Trim();
            string strLegalCounsel = ((TextBox)row.FindControl("txtLegalCounsel")).Text.Trim();

            XmlNode node = doc.SelectSingleNode("//EXTERNALAUDITORS");
            if (node != null)
            {
                XmlElement newRow = doc.CreateElement("ROW");
                XmlAttribute newAttr = doc.CreateAttribute("id");
                newAttr.Value = ctr.ToString().Trim();
                newRow.Attributes.Append(newAttr);

                newAttr = doc.CreateAttribute("auditor");
                newAttr.Value = strAuditor.Trim();
                newRow.Attributes.Append(newAttr);

                newAttr = doc.CreateAttribute("counsel");
                newAttr.Value = strLegalCounsel.Trim();
                newRow.Attributes.Append(newAttr);

                node.AppendChild(newRow);
            }
            else
            {
                XmlElement newElem = doc.CreateElement("EXTERNALAUDITORS");
                XmlElement newRow = doc.CreateElement("ROW");
                XmlAttribute newAttr = doc.CreateAttribute("id");
                newAttr.Value = ctr.ToString().Trim();
                newRow.Attributes.Append(newAttr);

                newAttr = doc.CreateAttribute("auditor");
                newAttr.Value = strAuditor.Trim();
                newRow.Attributes.Append(newAttr);

                newAttr = doc.CreateAttribute("counsel");
                newAttr.Value = strLegalCounsel.Trim();
                newRow.Attributes.Append(newAttr);

                newElem.AppendChild(newRow);
                doc.DocumentElement.AppendChild(newElem);
            }
            ctr++;
        }

        doc.PreserveWhitespace = true;
        XmlTextWriter wrtr = new XmlTextWriter(hdnXMLFileName.Value.Trim(), null);
        doc.WriteTo(wrtr);
        wrtr.Close();
    }


    private void WriteEquipmentToXML()
    {
        int ctr = 0;

        hdnXMLFileName.Value = Session["XMLFile"].ToString().Trim();
        XmlDocument doc = new XmlDocument();
        doc.Load(hdnXMLFileName.Value.Trim());

        foreach (GridViewRow row in gvEquipment.Rows)
        {
            string strType = ((TextBox)row.FindControl("txtType")).Text.Trim();
            string strUnit = ((TextBox)row.FindControl("txtUnit")).Text.Trim();
            string strRemarks = ((TextBox)row.FindControl("txtRemarks")).Text.Trim();


            XmlNode node = doc.SelectSingleNode("//EQUIPMENT");
            if (node != null)
            {
                XmlElement newRow = doc.CreateElement("ROW");
                XmlAttribute newAttr = doc.CreateAttribute("id");
                newAttr.Value = ctr.ToString().Trim();
                newRow.Attributes.Append(newAttr);

                newAttr = doc.CreateAttribute("type");
                newAttr.Value = strType.Trim();
                newRow.Attributes.Append(newAttr);

                newAttr = doc.CreateAttribute("unit");
                newAttr.Value = strUnit.Trim();
                newRow.Attributes.Append(newAttr);

                newAttr = doc.CreateAttribute("remarks");
                newAttr.Value = strRemarks.Trim();
                newRow.Attributes.Append(newAttr);

                node.AppendChild(newRow);
            }
            else
            {
                XmlElement newElem = doc.CreateElement("EQUIPMENT");
                XmlElement newRow = doc.CreateElement("ROW");
                XmlAttribute newAttr = doc.CreateAttribute("id");
                newAttr.Value = ctr.ToString().Trim();
                newRow.Attributes.Append(newAttr);

                newAttr = doc.CreateAttribute("type");
                newAttr.Value = strType.Trim();
                newRow.Attributes.Append(newAttr);

                newAttr = doc.CreateAttribute("unit");
                newAttr.Value = strUnit.Trim();
                newRow.Attributes.Append(newAttr);

                newAttr = doc.CreateAttribute("remarks");
                newAttr.Value = strRemarks.Trim();
                newRow.Attributes.Append(newAttr);

                newElem.AppendChild(newRow);
                doc.DocumentElement.AppendChild(newElem);
            }
            ctr++;
        }

        doc.PreserveWhitespace = true;
        XmlTextWriter wrtr = new XmlTextWriter(hdnXMLFileName.Value.Trim(), null);
        doc.WriteTo(wrtr);
        wrtr.Close();
    }

    private void WriteRelativeToXML()
    {
        int ctr = 0;

        hdnXMLFileName.Value = Session["XMLFile"].ToString().Trim();
        XmlDocument doc = new XmlDocument();
        doc.Load(hdnXMLFileName.Value.Trim());

        foreach (GridViewRow row in gvRelatives.Rows)
        {
            string strName = ((TextBox)row.FindControl("txtName")).Text.Trim();
            string strTitlePosition = ((TextBox)row.FindControl("txtTitlePosition")).Text.Trim();
            string strRelationship = ((TextBox)row.FindControl("txtRelationship")).Text.Trim();

            XmlNode node = doc.SelectSingleNode("//RELATIVE");
            if (node != null)
            {
                XmlElement newRow = doc.CreateElement("ROW");
                XmlAttribute newAttr = doc.CreateAttribute("id");
                newAttr.Value = ctr.ToString().Trim();
                newRow.Attributes.Append(newAttr);

                newAttr = doc.CreateAttribute("name");
                newAttr.Value = strName.Trim();
                newRow.Attributes.Append(newAttr);

                newAttr = doc.CreateAttribute("titleposition");
                newAttr.Value = strTitlePosition.Trim();
                newRow.Attributes.Append(newAttr);

                newAttr = doc.CreateAttribute("relationship");
                newAttr.Value = strRelationship.Trim();
                newRow.Attributes.Append(newAttr);

                node.AppendChild(newRow);
            }
            else
            {
                XmlElement newElem = doc.CreateElement("RELATIVE");
                XmlElement newRow = doc.CreateElement("ROW");
                XmlAttribute newAttr = doc.CreateAttribute("id");
                newAttr.Value = ctr.ToString().Trim();
                newRow.Attributes.Append(newAttr);

                newAttr = doc.CreateAttribute("name");
                newAttr.Value = strName.Trim();
                newRow.Attributes.Append(newAttr);

                newAttr = doc.CreateAttribute("titleposition");
                newAttr.Value = strTitlePosition.Trim();
                newRow.Attributes.Append(newAttr);

                newAttr = doc.CreateAttribute("relationship");
                newAttr.Value = strRelationship.Trim();
                newRow.Attributes.Append(newAttr);

                newElem.AppendChild(newRow);
                doc.DocumentElement.AppendChild(newElem);
            }
            ctr++;
        }

        doc.PreserveWhitespace = true;
        XmlTextWriter wrtr = new XmlTextWriter(hdnXMLFileName.Value.Trim(), null);
        doc.WriteTo(wrtr);
        wrtr.Close();
    }
    #endregion
    #region "ReadFromXML"
    private void ReadFromXML()
    {
        if (Session["XMLFile"] != null)
        {
            hdnXMLFileName.Value = Session["XMLFile"].ToString().Trim();
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
                                                ddlSupplierType.SelectedIndex = ddlSupplierType.Items.IndexOf(ddlSupplierType.Items.FindByValue(tr.Value.Trim()));
                                                break;
                                            case "CATEGORY":
                                                hdnCategories.Text = tr.Value.Trim();
                                                break;
                                            case "SUBCATEGORY":
                                                hdnSubCategory.Text = tr.Value.Trim();
                                                break;
                                            case "COMPANYNAME":
                                                txtCompanyName.Value = tr.Value.Trim();
                                                break;
                                            case "HEADADDRESS1":
                                                txtHeadAddress1.Value = tr.Value.Trim();
                                                break;
                                            case "HEADADDRESS2":
                                                txtHeadAddress2.Value = tr.Value.Trim();
                                                break;
                                            case "HEADTEL":
                                                txtHeadTel.Value = tr.Value.Trim();
                                                break;
                                            case "HEADTELEXT":
                                                txtHeadExt.Value = tr.Value.Trim();
                                                break;
                                            case "HEADFAX":
                                                txtHeadFax.Value = tr.Value.Trim();
                                                break;
                                            case "BRANCHADDRESS1":
                                                txtBranchAddress1.Value = tr.Value.Trim();
                                                break;
                                            case "BRANCHADDRESS2":
                                                txtBranchAddress2.Value = tr.Value.Trim();
                                                break;
                                            case "BRANCHTEL":
                                                txtBranchTel.Value = tr.Value.Trim();
                                                break;
                                            case "BRANCHTELEXT":
                                                txtBranchExt.Value = tr.Value.Trim();
                                                break;
                                            case "BRANCHFAX":
                                                txtBranchFax.Value = tr.Value.Trim();
                                                break;
                                            case "VATREGNO":
                                                txtVatRegNo.Value = tr.Value.Trim();
                                                break;
                                            case "TIN":
                                                txtTIN.Value = tr.Value.Trim();
                                                break;
                                            case "POBOX":
                                                txtPOBOX.Value = tr.Value.Trim();
                                                break;
                                            case "POSTALCODE":
                                                txtPostalCode.Value = tr.Value.Trim();
                                                break;
                                            case "EMAIL":
                                                txtEmail.Value = tr.Value.Trim();
                                                break;
                                            case "STANDARDTERMSOFPAYMENT":
                                                txtStandardTermsOfPayment.Value = tr.Value.Trim();
                                                break;
                                            case "SPECIALTERMS":
                                                txtSpecialTerms.Value = tr.Value.Trim();
                                                break;
                                            case "MINIMUMORDERVALUE":
                                                txtMinOrderValue.Value = tr.Value.Trim();
                                                break;
                                            case "SALESPERSON":
                                                txtSalesPerson.Value = tr.Value.Trim();
                                                break;
                                            case "SALESPERSONTELEPHONE":
                                                txtSalesPersonTel.Value = tr.Value.Trim();
                                                break;
                                            case "TYPEOFBUSINESSORGANIZATION":
                                                //chkTypeOfOrganization.Items.FindByValue(tr.Value.Trim()).Selected = true;
                                                break;
                                            case "OWNERSHIP":
                                                txtOwnershipFilipino.Value = tr.Value.Trim();
                                                break;
                                            case "COMPANYCLASSIFICATION":
                                                // chkCompanyClassification.Items.FindByValue(tr.Value.Trim()).Selected = true;
                                                break;
                                            case "SOLESUPPLIERLINE1":
                                                txtSoleSupplier.Value = tr.Value.Trim();
                                                break;
                                            case "SOLESUPPLIERLINE2":
                                                txtSoleSupplier2.Value = tr.Value.Trim();
                                                break;
                                            case "SPECIALIZATION":
                                                txtSpecialization.Value = tr.Value.Trim();
                                                break;
                                        }
                                    }
                                }
                                break;
                            case "KEYPERSONNEL":
                                {

                                    DataTable dt = new DataTable();
                                    DataColumn dc = new DataColumn("Index", typeof(System.String));
                                    dt.Columns.Add(dc);
                                    dc = new DataColumn("Name", typeof(System.String));
                                    dt.Columns.Add(dc);
                                    dc = new DataColumn("Position", typeof(System.String));
                                    dt.Columns.Add(dc);
                                    int ctr = 0;
                                    while (tr.Read() && tr.NodeType != XmlNodeType.EndElement && tr.Name != "KEYPERSONNEL")
                                    {
                                        DataRow dr = dt.NewRow();
                                        dr["Index"] = 0;
                                        dr["Name"] = "";
                                        dr["Position"] = "";
                                        if (tr.Name.ToUpper().Trim() == "ROW")
                                        {
                                            while (tr.MoveToNextAttribute())
                                            {
                                                switch (tr.Name.Trim().ToUpper())
                                                {
                                                    case "ID":
                                                        dr["Index"] = ctr;
                                                        break;
                                                    case "NAME":
                                                        dr["Name"] = tr.Value.Trim();
                                                        break;
                                                    case "TITLEPOSITION":
                                                        dr["Position"] = tr.Value.Trim();
                                                        break;
                                                }
                                            }
                                            dt.Rows.Add(dr);
                                        }
                                        ctr++;
                                    }
                                    if (dt.Rows.Count == 0)
                                        dt = CreateEmptyKeyPersonell();

                                   
                                    gvKeyPersonnel.DataSource = dt;
                                    gvKeyPersonnel.DataBind();
                                }
                                break;

                            case "PRESENTSERVICES":
                                {

                                    DataTable dt = new DataTable();
                                    DataColumn dc = new DataColumn("Index", typeof(System.String));
                                    dt.Columns.Add(dc);
                                    dc = new DataColumn("Plan", typeof(System.String));
                                    dt.Columns.Add(dc);
                                    dc = new DataColumn("AcctNo", typeof(System.String));
                                    dt.Columns.Add(dc);
                                    dc = new DataColumn("CreditLimit", typeof(System.String));
                                    dt.Columns.Add(dc);
                                    int ctr = 0;
                                    while (tr.Read() && tr.NodeType != XmlNodeType.EndElement && tr.Name != "KEYPERSONNEL")
                                    {
                                        DataRow dr = dt.NewRow();
                                        dr["Index"] = 0;
                                        dr["Plan"] = "";
                                        dr["AcctNo"] = "";
                                        dr["CreditLimit"] = "";
                                        if (tr.Name.ToUpper().Trim() == "ROW")
                                        {
                                            while (tr.MoveToNextAttribute())
                                            {
                                                switch (tr.Name.Trim().ToUpper())
                                                {
                                                    case "ID":
                                                        dr["Index"] = ctr;
                                                        break;
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
                                            dt.Rows.Add(dr);
                                        }
                                        ctr++;
                                    }
                                    if (dt.Rows.Count == 0)
                                        dt = CreateEmptyPresentServices();

                                    if (hdnAddAPresentService.Value.ToString().Trim() == "1")
                                    {
                                        CreateEmptyPresentServicesRow(ref dt);
                                        hdnAddAPresentService.Value = "";
                                    }
                                    gvPresentServices.DataSource = dt;
                                    gvPresentServices.DataBind();
                                }
                                break;

                            case "MAJORCUSTOMERS":
                                {

                                    DataTable dt = new DataTable();
                                    DataColumn dc = new DataColumn("Index", typeof(System.String));
                                    dt.Columns.Add(dc);
                                    dc = new DataColumn("Customer", typeof(System.String));
                                    dt.Columns.Add(dc);
                                    dc = new DataColumn("Sale", typeof(System.String));
                                    dt.Columns.Add(dc);
                                    int ctr = 0;
                                    while (tr.Read() && tr.NodeType != XmlNodeType.EndElement && tr.Name != "KEYPERSONNEL")
                                    {
                                        DataRow dr = dt.NewRow();
                                        dr["Index"] = 0;
                                        dr["Customer"] = "";
                                        dr["Sale"] = "";
                                        if (tr.Name.ToUpper().Trim() == "ROW")
                                        {
                                            while (tr.MoveToNextAttribute())
                                            {
                                                switch (tr.Name.Trim().ToUpper())
                                                {
                                                    case "ID":
                                                        dr["Index"] = ctr;
                                                        break;
                                                    case "CUSTOMER":
                                                        dr["Customer"] = tr.Value.Trim();
                                                        break;
                                                    case "SALE":
                                                        dr["Sale"] = tr.Value.Trim();
                                                        break;
                                                }
                                            }
                                            dt.Rows.Add(dr);
                                        }
                                        ctr++;
                                    }
                                    if (dt.Rows.Count == 0)
                                        dt = CreateEmptyMajorCustomers();

                                    if (hdnAddMajorCustomer.Value.ToString().Trim() == "1")
                                    {
                                        CreateEmptyMajorCustomersRow(ref dt);
                                        hdnAddMajorCustomer.Value = "";
                                    }
                                    gvMajorCustomers.DataSource = dt;
                                    gvMajorCustomers.DataBind();
                                }
                                break;

                            case "BANKS":
                                {
                                    DataTable dt = new DataTable();
                                    DataColumn dc = new DataColumn("Index", typeof(System.String));
                                    dt.Columns.Add(dc);
                                    dc = new DataColumn("Bank", typeof(System.String));
                                    dt.Columns.Add(dc);
                                    dc = new DataColumn("CreditLine", typeof(System.String));
                                    dt.Columns.Add(dc);
                                    int ctr = 0;
                                    while (tr.Read() && tr.NodeType != XmlNodeType.EndElement && tr.Name != "KEYPERSONNEL")
                                    {
                                        DataRow dr = dt.NewRow();
                                        dr["Index"] = 0;
                                        dr["Bank"] = "";
                                        dr["CreditLine"] = "";
                                        if (tr.Name.ToUpper().Trim() == "ROW")
                                        {
                                            while (tr.MoveToNextAttribute())
                                            {
                                                switch (tr.Name.Trim().ToUpper())
                                                {
                                                    case "ID":
                                                        dr["Index"] = ctr;
                                                        break;
                                                    case "BANK":
                                                        dr["Bank"] = tr.Value.Trim();
                                                        break;
                                                    case "CREDITLINE":
                                                        dr["CreditLine"] = tr.Value.Trim();
                                                        break;
                                                }
                                            }
                                            dt.Rows.Add(dr);
                                        }
                                        ctr++;
                                    }
                                    if (dt.Rows.Count == 0)
                                        dt = CreateEmptyBanks();

                                    if (hdnAddABank.Value.ToString().Trim() == "1")
                                    {
                                        CreateEmptyBanksRow(ref dt);
                                        hdnAddABank.Value = "";
                                    }
                                    gvBanks.DataSource = dt;
                                    gvBanks.DataBind();
                                }
                                break;

                            case "AFFILIATEDCOMPANIES":
                                {

                                    DataTable dt = new DataTable();
                                    DataColumn dc = new DataColumn("Index", typeof(System.String));
                                    dt.Columns.Add(dc);
                                    dc = new DataColumn("Company", typeof(System.String));
                                    dt.Columns.Add(dc);
                                    dc = new DataColumn("Business", typeof(System.String));
                                    dt.Columns.Add(dc);
                                    int ctr = 0;
                                    while (tr.Read() && tr.NodeType != XmlNodeType.EndElement && tr.Name != "KEYPERSONNEL")
                                    {
                                        DataRow dr = dt.NewRow();
                                        dr["Index"] = 0;
                                        dr["Company"] = "";
                                        dr["Business"] = "";
                                        if (tr.Name.ToUpper().Trim() == "ROW")
                                        {
                                            while (tr.MoveToNextAttribute())
                                            {
                                                switch (tr.Name.Trim().ToUpper())
                                                {
                                                    case "ID":
                                                        dr["Index"] = ctr;
                                                        break;
                                                    case "COMPANY":
                                                        dr["Company"] = tr.Value.Trim();
                                                        break;
                                                    case "BUSINESS":
                                                        dr["Business"] = tr.Value.Trim();
                                                        break;
                                                }
                                            }
                                            dt.Rows.Add(dr);
                                        }
                                        ctr++;
                                    }
                                    if (dt.Rows.Count == 0)
                                        dt = CreateEmptyAffiliatedCompanies();

                                    if (hdnAddACompany.Value.ToString().Trim() == "1")
                                    {
                                        CreateEmptyAffiliatedCompaniesRow(ref dt);
                                        hdnAddACompany.Value = "";
                                    }
                                    gvAffiliatedCompanies.DataSource = dt;
                                    gvAffiliatedCompanies.DataBind();
                                }
                                break;

                            case "EXTERNALAUDITORS":
                                {

                                    DataTable dt = new DataTable();
                                    DataColumn dc = new DataColumn("Index", typeof(System.String));
                                    dt.Columns.Add(dc);
                                    dc = new DataColumn("Auditor", typeof(System.String));
                                    dt.Columns.Add(dc);
                                    dc = new DataColumn("Counsel", typeof(System.String));
                                    dt.Columns.Add(dc);
                                    int ctr = 0;
                                    while (tr.Read() && tr.NodeType != XmlNodeType.EndElement && tr.Name != "KEYPERSONNEL")
                                    {
                                        DataRow dr = dt.NewRow();
                                        dr["Index"] = 0;
                                        dr["Auditor"] = "";
                                        dr["Counsel"] = "";
                                        if (tr.Name.ToUpper().Trim() == "ROW")
                                        {
                                            while (tr.MoveToNextAttribute())
                                            {
                                                switch (tr.Name.Trim().ToUpper())
                                                {
                                                    case "ID":
                                                        dr["Index"] = ctr;
                                                        break;
                                                    case "AUDITOR":
                                                        dr["Auditor"] = tr.Value.Trim();
                                                        break;
                                                    case "COUNSEL":
                                                        dr["Counsel"] = tr.Value.Trim();
                                                        break;
                                                }
                                            }
                                            dt.Rows.Add(dr);
                                        }
                                        ctr++;
                                    }
                                    if (dt.Rows.Count == 0)
                                        dt = CreateEmptyExternalAuditor();

                                    if (hdnAddAnAuditor.Value.ToString().Trim() == "1")
                                    {
                                        CreateEmptyExternalAuditorRow(ref dt);
                                        hdnAddAnAuditor.Value = "";
                                    }
                                    gvExternalAuditors.DataSource = dt;
                                    gvExternalAuditors.DataBind();
                                }
                                break;

                            case "EQUIPMENT":
                                {

                                    DataTable dt = new DataTable();
                                    DataColumn dc = new DataColumn("Index", typeof(System.String));
                                    dt.Columns.Add(dc);
                                    dc = new DataColumn("Type", typeof(System.String));
                                    dt.Columns.Add(dc);
                                    dc = new DataColumn("Unit", typeof(System.String));
                                    dt.Columns.Add(dc);
                                    dc = new DataColumn("Remarks", typeof(System.String));
                                    dt.Columns.Add(dc);
                                    int ctr = 0;
                                    while (tr.Read() && tr.NodeType != XmlNodeType.EndElement && tr.Name != "KEYPERSONNEL")
                                    {
                                        DataRow dr = dt.NewRow();
                                        dr["Index"] = 0;
                                        dr["Type"] = "";
                                        dr["Unit"] = "";
                                        dr["Remarks"] = "";
                                        if (tr.Name.ToUpper().Trim() == "ROW")
                                        {
                                            while (tr.MoveToNextAttribute())
                                            {
                                                switch (tr.Name.Trim().ToUpper())
                                                {
                                                    case "ID":
                                                        dr["Index"] = ctr;
                                                        break;
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
                                            dt.Rows.Add(dr);
                                        }
                                        ctr++;
                                    }
                                    if (dt.Rows.Count == 0)
                                        dt = CreateEmptyEquipment();

                                    if (hdnAddAnEquipment.Value.ToString().Trim() == "1")
                                    {
                                        CreateEmptyEquipmentRow(ref dt);
                                        hdnAddAnEquipment.Value = "";
                                    }
                                    gvEquipment.DataSource = dt;
                                    gvEquipment.DataBind();
                                }
                                break;

                            case "RELATIVE":
                                {

                                    DataTable dt = new DataTable();
                                    DataColumn dc = new DataColumn("Index", typeof(System.String));
                                    dt.Columns.Add(dc);
                                    dc = new DataColumn("Name", typeof(System.String));
                                    dt.Columns.Add(dc);
                                    dc = new DataColumn("TitlePosition", typeof(System.String));
                                    dt.Columns.Add(dc);
                                    dc = new DataColumn("Relationship", typeof(System.String));
                                    dt.Columns.Add(dc);
                                    int ctr = 0;
                                    while (tr.Read() && tr.NodeType != XmlNodeType.EndElement && tr.Name != "KEYPERSONNEL")
                                    {
                                        DataRow dr = dt.NewRow();
                                        dr["Index"] = 0;
                                        dr["Name"] = "";
                                        dr["TitlePosition"] = "";
                                        dr["Relationship"] = "";
                                        if (tr.Name.ToUpper().Trim() == "ROW")
                                        {
                                            while (tr.MoveToNextAttribute())
                                            {
                                                switch (tr.Name.Trim().ToUpper())
                                                {
                                                    case "ID":
                                                        dr["Index"] = ctr;
                                                        break;
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
                                            dt.Rows.Add(dr);
                                        }
                                        ctr++;
                                    }
                                    if (dt.Rows.Count == 0)
                                        dt = CreateEmptyRelativesTable();

                                    if (hdnAddARelative.Value.ToString().Trim() == "1")
                                    {
                                        CreateEmptyRelativesRow(ref dt);
                                        hdnAddARelative.Value = "";
                                    }
                                    gvRelatives.DataSource = dt;
                                    gvRelatives.DataBind();
                                }
                                break;
                        }

                    }
                }
                tr.Close();
            }
        }
    }

    #endregion
    #region RemoveFromXML

    private void RemoveKeyPersonnelFromXML()
        {
        if (Session["XMLFile"] != null)
        {
            string Index = Session["KeyPersonnelIndex"].ToString().Trim();
            hdnXMLFileName.Value = Session["XMLFile"].ToString().Trim();
            XmlDocument doc = new XmlDocument();
            doc.Load( hdnXMLFileName.Value.Trim());
            XmlNode node = doc.SelectSingleNode("//KEYPERSONNEL");
            if (node != null)
            {
                if (node.ChildNodes.Count > 0)
                {
                    if (Index.Trim() != "")
                    {
                        XmlNode x = node.ChildNodes.Item(Int32.Parse(Index.Trim()));
                        node.RemoveChild(x);
                    }
                }
            }

            doc.PreserveWhitespace = true;
            XmlTextWriter wrtr = new XmlTextWriter(hdnXMLFileName.Value.Trim(), null);
            doc.WriteTo(wrtr);
            wrtr.Close();
        }
    }

    private void RemovePresentServicesFromXML()
    {
        if (Session["XMLFile"] != null)
        {
            string Index = Session["PresentServicesIndex"].ToString().Trim();
            hdnXMLFileName.Value = Session["XMLFile"].ToString().Trim();
            XmlDocument doc = new XmlDocument();
            doc.Load(hdnXMLFileName.Value.Trim());
            XmlNode node = doc.SelectSingleNode("//PRESENTSERVICES");
            if (node != null)
            {
                if (node.ChildNodes.Count > 0)
                {
                    if (Index.Trim() != "")
                    {
                        XmlNode x = node.ChildNodes.Item(Int32.Parse(Index.Trim()));
                        node.RemoveChild(x);
                    }
                }
            }

            doc.PreserveWhitespace = true;
            XmlTextWriter wrtr = new XmlTextWriter(hdnXMLFileName.Value.Trim(), null);
            doc.WriteTo(wrtr);
            wrtr.Close();
        }
    }
    private void RemoveMajorCustomer()
    {
        if (Session["XMLFile"] != null)
        {
            string Index = Session["CustomerIndex"].ToString().Trim();
            hdnXMLFileName.Value = Session["XMLFile"].ToString().Trim();
            XmlDocument doc = new XmlDocument();
            doc.Load(hdnXMLFileName.Value.Trim());
            XmlNode node = doc.SelectSingleNode("//MAJORCUSTOMERS");
            if (node != null)
            {
                if (node.ChildNodes.Count > 0)
                {
                    if (Index.Trim() != "")
                    {
                        XmlNode x = node.ChildNodes.Item(Int32.Parse(Index.Trim()));
                        node.RemoveChild(x);
                    }
                }
            }

            doc.PreserveWhitespace = true;
            XmlTextWriter wrtr = new XmlTextWriter(hdnXMLFileName.Value.Trim(), null);
            doc.WriteTo(wrtr);
            wrtr.Close();
        }
    }
    private void RemoveBank()
    {
        if (Session["XMLFile"] != null)
        {
            string Index = Session["BankIndex"].ToString().Trim();
            hdnXMLFileName.Value = Session["XMLFile"].ToString().Trim();
            XmlDocument doc = new XmlDocument();
            doc.Load(hdnXMLFileName.Value.Trim());
            XmlNode node = doc.SelectSingleNode("//BANKS");
            if (node != null)
            {
                if (node.ChildNodes.Count > 0)
                {
                    if (Index.Trim() != "")
                    {
                        XmlNode x = node.ChildNodes.Item(Int32.Parse(Index.Trim()));
                        node.RemoveChild(x);
                    }
                }
            }

            doc.PreserveWhitespace = true;
            XmlTextWriter wrtr = new XmlTextWriter(hdnXMLFileName.Value.Trim(), null);
            doc.WriteTo(wrtr);
            wrtr.Close();
        }
    }
    private void RemoveAffiliatedCompany()
    {
        if (Session["XMLFile"] != null)
        {
            string Index = Session["CompanyIndex"].ToString().Trim();
            hdnXMLFileName.Value = Session["XMLFile"].ToString().Trim();
            XmlDocument doc = new XmlDocument();
            doc.Load(hdnXMLFileName.Value.Trim());
            XmlNode node = doc.SelectSingleNode("//AFFILIATEDCOMPANIES");
            if (node != null)
            {
                if (node.ChildNodes.Count > 0)
                {
                    if (Index.Trim() != "")
                    {
                        XmlNode x = node.ChildNodes.Item(Int32.Parse(Index.Trim()));
                        node.RemoveChild(x);
                    }
                }
            }

            doc.PreserveWhitespace = true;
            XmlTextWriter wrtr = new XmlTextWriter(hdnXMLFileName.Value.Trim(), null);
            doc.WriteTo(wrtr);
            wrtr.Close();
        }
    }
    private void RemoveExternalAuditor()
    {
        if (Session["XMLFile"] != null)
        {
            string Index = Session["AuditorIndex"].ToString().Trim();
            hdnXMLFileName.Value = Session["XMLFile"].ToString().Trim();
            XmlDocument doc = new XmlDocument();
            doc.Load(hdnXMLFileName.Value.Trim());
            XmlNode node = doc.SelectSingleNode("//EXTERNALAUDITORS");
            if (node != null)
            {
                if (node.ChildNodes.Count > 0)
                {
                    if (Index.Trim() != "")
                    {
                        XmlNode x = node.ChildNodes.Item(Int32.Parse(Index.Trim()));
                        node.RemoveChild(x);
                    }
                }
            }

            doc.PreserveWhitespace = true;
            XmlTextWriter wrtr = new XmlTextWriter(hdnXMLFileName.Value.Trim(), null);
            doc.WriteTo(wrtr);
            wrtr.Close();
        }
    }
    private void RemoveEquipment()
    {
        if (Session["XMLFile"] != null)
        {
            string Index = Session["EquipmentIndex"].ToString().Trim();
            hdnXMLFileName.Value = Session["XMLFile"].ToString().Trim();
            XmlDocument doc = new XmlDocument();
            doc.Load(hdnXMLFileName.Value.Trim());
            XmlNode node = doc.SelectSingleNode("//EQUIPMENT");
            if (node != null)
            {
                if (node.ChildNodes.Count > 0)
                {
                    if (Index.Trim() != "")
                    {
                        XmlNode x = node.ChildNodes.Item(Int32.Parse(Index.Trim()));
                        node.RemoveChild(x);
                    }
                }
            }

            doc.PreserveWhitespace = true;
            XmlTextWriter wrtr = new XmlTextWriter(hdnXMLFileName.Value.Trim(), null);
            doc.WriteTo(wrtr);
            wrtr.Close();
        }
    }
    private void RemoveRelatives()
    {
        if (Session["XMLFile"] != null)
        {
            string Index = Session["RelativeIndex"].ToString().Trim();
            hdnXMLFileName.Value = Session["XMLFile"].ToString().Trim();
            XmlDocument doc = new XmlDocument();
            doc.Load(hdnXMLFileName.Value.Trim());
            XmlNode node = doc.SelectSingleNode("//RELATIVE");
            if (node != null)
            {
                if (node.ChildNodes.Count > 0)
                {
                    if (Index.Trim() != "")
                    {
                        XmlNode x = node.ChildNodes.Item(Int32.Parse(Index.Trim()));
                        node.RemoveChild(x);
                    }
                }
            }

            doc.PreserveWhitespace = true;
            XmlTextWriter wrtr = new XmlTextWriter(hdnXMLFileName.Value.Trim(), null);
            doc.WriteTo(wrtr);
            wrtr.Close();
        }
    }



    #endregion

    #endregion
    #region "GridViewCommands"

    protected void gvPresentServices_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("Remove"))
        {
            if (hdnConfirmRemovePS.Value.Trim() == "true")
            {
                hdnFocusToLastPresentService.Value = "1";
                hdnAddAPresentService.Value = "";
                WriteAllData();
                Session["PresentServicesIndex"] = e.CommandArgument.ToString().Trim();
                RemovePresentServicesFromXML();
                ReadFromXML();
            }
        }
    }

    protected void gvPresentServices_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            TypeOfPlanTransaction typ = new TypeOfPlanTransaction();
            LinkButton lnk = (LinkButton)e.Row.FindControl("lnkRemove");
            lnk.Attributes.Add("onClick", "ConfirmPresentServices();");
            HiddenField hdn = (HiddenField)e.Row.FindControl("hdnTypeOfPlan");
            DropDownList ddl = (DropDownList)e.Row.FindControl("ddlTypeOfPlan");
            ddl.DataSource = typ.QueryTypeOfPlan(connstring);
            ddl.DataValueField = "PlanID";
            ddl.DataTextField = "PlanName";
            ddl.DataBind();
            ddl.Items.Insert(0, "");
            ddl.SelectedIndex = ddl.Items.IndexOf(ddl.Items.FindByValue(hdn.Value.Trim()));
        }
    }


    protected void gvMajorCustomers_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("Remove"))
        {
            if (hdnConfirmRemoveMC.Value.Trim() == "true")
            {
                hdnFocusToLastMajorCustomer.Value = "1";
                hdnAddMajorCustomer.Value = "";
                WriteAllData();
                Session["CustomerIndex"] = e.CommandArgument.ToString().Trim();
                RemoveMajorCustomer();
                ReadFromXML();
            }
        }
    }

    protected void gvMajorCustomers_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton lnk = (LinkButton)e.Row.FindControl("lnkRemove");
            lnk.Attributes.Add("onClick", "ConfirmMajorCustomers();");
        }
    }

    protected void gvBanks_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("Remove"))
        {
            if (hdnConfirmRemoveBank.Value.Trim() == "true")
            {
                hdnFocusToLastBank.Value = "1";
                hdnAddABank.Value = "";
                WriteAllData();
                Session["BankIndex"] = e.CommandArgument.ToString().Trim();
                RemoveBank();
                ReadFromXML();
            }
        }
    }

    protected void gvBanks_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton lnk = (LinkButton)e.Row.FindControl("lnkRemove");
            lnk.Attributes.Add("onClick", "ConfirmBanks();");
        }
    }

    protected void gvAffiliatedCompanies_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("Remove"))
        {
            if (hdnConfirmRemoveCompany.Value.Trim() == "true")
            {
                hdnFocusToLastAffiliatedCompany.Value = "1";
                hdnAddACompany.Value = "";
                WriteAllData();
                Session["CompanyIndex"] = e.CommandArgument.ToString().Trim();
                RemoveAffiliatedCompany();
                ReadFromXML();
            }
        }
    }

    protected void gvAffiliatedCompanies_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton lnk = (LinkButton)e.Row.FindControl("lnkRemove");
            lnk.Attributes.Add("onClick", "ConfirmAffiliatedCompanies();");
        }
    }

    protected void gvExternalAuditors_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("Remove"))
        {
            if (hdnConfirmRemoveAuditor.Value.Trim() == "true")
            {
                hdnFocusToLastExternalAuditor.Value = "1";
                hdnAddAnAuditor.Value = "";
                WriteAllData();
                Session["AuditorIndex"] = e.CommandArgument.ToString().Trim();
                RemoveExternalAuditor();
                ReadFromXML();
            }
        }
    }

    protected void gvExternalAuditors_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton lnk = (LinkButton)e.Row.FindControl("lnkRemove");
            lnk.Attributes.Add("onClick", "ConfirmExternalAuditors();");
        }
    }

    protected void gvEquipment_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("Remove"))
        {
            if (hdnConfirmRemoveEquipment.Value.Trim() == "true")
            {
                hdnFocusToLastEquipment.Value = "1";
                hdnAddAnEquipment.Value = "";
                WriteAllData();
                Session["EquipmentIndex"] = e.CommandArgument.ToString().Trim();
                RemoveEquipment();
                ReadFromXML();
            }
        }
    }

    protected void gvEquipment_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton lnk = (LinkButton)e.Row.FindControl("lnkRemove");
            lnk.Attributes.Add("onClick", "ConfirmEquipment();");
        }
    }

    protected void gvRelatives_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("Remove"))
        {
            if (hdnConfirmRemoveRelative.Value.Trim() == "true")
            {
                hdnFocusToLastRelative.Value = "1";
                hdnAddARelative.Value = "";
                WriteAllData();
                Session["RelativeIndex"] = e.CommandArgument.ToString().Trim();
                RemoveRelatives();
                ReadFromXML();
            }
        }
    }

    protected void gvRelatives_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton lnk = (LinkButton)e.Row.FindControl("lnkRemove");
            lnk.Attributes.Add("onClick", "ConfirmRelative();");
        }
    }
    #endregion 
   
}
