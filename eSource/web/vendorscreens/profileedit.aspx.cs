using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using EBid.lib;
using EBid.lib.constant;
using EBid.lib.bid.data;
using EBid.lib.bid.trans;
using EBid.lib.user.trans;
using EBid.lib.user.data;
using EBid.ConnectionString;

public partial class web_vendorscreens_profileEdit : System.Web.UI.Page
{
    private static string connstring = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        FormsAuthenticationHelper.AuthenticateUserWithReturnUrl();
        connstring = Client.ConnectionString(HttpContext.Current.Session["clientid"].ToString());
        if (Int32.Parse(Session[Constant.SESSION_USERTYPE].ToString().Trim()) != (int)Constant.USERTYPE.VENDOR)
            Response.Redirect("../unauthorizedaccess.aspx");

        PageTitle.InnerText = String.Format(Constant.TITLEFORMAT, "My Profile");

        if (!(Page.IsPostBack))
        {
            InitVariables();
            AddValidators();

            if (Session[Constant.SESSION_USERID] != null)
            {
                ViewState[Constant.SESSION_USERID] = Session[Constant.SESSION_USERID].ToString().Trim();

                CompanyTransaction companyDetails = new CompanyTransaction();

                // Present Services
                ViewState["dtPresentSvc"] = companyDetails.QueryPresentServices(connstring, ViewState[Constant.SESSION_USERID].ToString().Trim());
                ViewState["dtGlobePlans"] = companyDetails.QueryAllGlobePlans(connstring);
                LoadPresentSvcData();

                // Equipments
                ViewState["dtEquipments"] = companyDetails.QueryVendorEquipments(connstring, ViewState[Constant.SESSION_USERID].ToString().Trim());
                LoadEquipmentsData();

                // Relatives
                ViewState["dtRelatives"] = companyDetails.QueryVendorRelatives(connstring, ViewState[Constant.SESSION_USERID].ToString().Trim());
                LoadRelativesData();

                // References
                ViewState["MajCustomers"] = companyDetails.QueryVendorReferences(connstring, ViewState[Constant.SESSION_USERID].ToString().Trim(), Constant.REF_MAIN_CUSTOMERS);
                LoadMajCustomersData();

                ViewState["Banks"] = companyDetails.QueryVendorReferences(connstring, ViewState[Constant.SESSION_USERID].ToString().Trim(), Constant.REF_BANKS);
                LoadBanksData();

                ViewState["AffCompany"] = companyDetails.QueryVendorReferences(connstring, ViewState[Constant.SESSION_USERID].ToString().Trim(), Constant.REF_AFFILIATE);
                LoadAffiliatesData();

                ViewState["ExternAudit"] = companyDetails.QueryVendorReferences(connstring, ViewState[Constant.SESSION_USERID].ToString().Trim(), Constant.REF_EXTERN_AUDITOR);
                LoadExtAuditorsData();

                DisplayCompanyInfo();
            }            
        }
    }

    private void InitVariables()
    {
        InitDeletedIDContainer();
    }

    private void AddValidators()
    {
        /* Textboxes that accepts numeric with '-' */
        tbMobileNo.Attributes.Add("onkeypress", "return PhoneAndNoValidator(this)");
        tbTelephone.Attributes.Add("onkeypress", "return PhoneAndNoValidator(this)");
        tbTelephone.Attributes.Add("maxLength", "20");
        tbFax.Attributes.Add("onkeypress", "return PhoneAndNoValidator(this)");
        tbFax.Attributes.Add("maxLength", "20");
        tbExtension.Attributes.Add("onkeypress", "return  PhoneAndNoValidator(this)");
        tbExtension.Attributes.Add("maxLength", "10");
        tbBranchPhone.Attributes.Add("onkeypress", "return  PhoneAndNoValidator(this)");
        tbBranchPhone.Attributes.Add("maxLength", "20");
        tbBranchFax.Attributes.Add("onkeypress", "return PhoneAndNoValidator(this)");
        tbBranchFax.Attributes.Add("maxLength", "20");
        tbBranchExtension.Attributes.Add("onkeypress", "return PhoneAndNoValidator(this)");
        tbBranchExtension.Attributes.Add("maxLength", "10");
        tbVatRegNo.Attributes.Add("onkeypress", "return PhoneAndNoValidator(this)");
        tbVatRegNo.Attributes.Add("maxLength", "20");
        tbPOBox.Attributes.Add("onkeypress", "return PhoneAndNoValidator(this)");
        tbPOBox.Attributes.Add("maxLength", "10");
        tbTIN.Attributes.Add("onkeypress", "return PhoneAndNoValidator(this)");
        tbTIN.Attributes.Add("maxLength", "20");
        tbSalesPersonPhone.Attributes.Add("onkeypress", "return PhoneAndNoValidator(this)");
        tbSalesPersonPhone.Attributes.Add("maxLength", "20");

        /* Textboxes that accepts currencies only */
        tbMinOrderValue.Attributes.Add("onkeypress", "return(currencyFormat(this,',','.',event))");

        /* Textboxes that accepts numeric only */
        tbPostalCode.Attributes.Add("onkeypress", "return NumberOnlyValidator(this)");
        tbPostalCode.Attributes.Add("maxLength", "8");
        tbOwnershipFilipino.Attributes.Add("onkeypress", "return NumberOnlyValidator(this)");
        tbOwnershipFilipino.Attributes.Add("onblur", "ReturnAverage('tbOwnershipFilipino','tbOwnershipOther')");
        tbOwnershipFilipino.Attributes.Add("maxLength", "3");
        tbOwnershipOther.Attributes.Add("onkeypress", "return NumberOnlyValidator(this)");
        tbOwnershipOther.Attributes.Add("onblur", "ReturnAverage('tbOwnershipOther','tbOwnershipFilipino')");
        tbOwnershipOther.Attributes.Add("maxLength", "3");
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

    private void InitDeletedIDContainer()
    {
        DataTable deleted = new DataTable();

        deleted.Columns.Add("ID");
        deleted.Columns.Add("Type");

        ViewState["Deleted"] = deleted;
    }

    private void UpdateDeletedIDContainer(string id, string type)
    {
        DataTable deleted = (DataTable)ViewState["Deleted"];

        DataRow drNewRow = deleted.NewRow();
        drNewRow["ID"] = id;
        drNewRow["Type"] = type;
        deleted.Rows.Add(drNewRow);
        deleted.AcceptChanges();

        ViewState["Deleted"] = deleted;
    }

    private void DisplayCompanyInfo()
    {
        CompanyTransaction companyDetails = new CompanyTransaction();
        CompanyDetails companyInfo = companyDetails.QueryCompanyDetails(connstring, ViewState[Constant.SESSION_USERID].ToString().Trim());
        ViewState["CompanyDetails"] = companyInfo;

        tbCompanyName.Text = companyInfo.CompanyName;
        tbHeadOfficeAddress1.Text = companyInfo.Address;
        tbHeadOfficeAddress2.Text = companyInfo.Address1;
        tbTelephone.Text = companyInfo.HeadTelephone;
        tbMobileNo.Text = companyInfo.MobileNo;
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
        tbSalesPerson.Text = companyInfo.SalesPerson;
        tbSalesPersonPhone.Text = companyInfo.SalesPersonPhone;

        rbOrganizationType.Items[companyInfo.OrganizationType - 1].Selected = true;

        tbOwnershipFilipino.Text = companyInfo.OwnershipFilipino;
        tbOwnershipOther.Text = companyInfo.OwnershipOther;

        //  rbClassification.Items[companyInfo.Classification - 1].Selected = true;

        tbSoleSupplier1.Text = companyInfo.SoleSupplier;
        tbSoleSupplier2.Text = companyInfo.SoleSupplier1;
        tbSpecialization.Text = companyInfo.Specialization;

        tbKeyPersonnel.Text = companyInfo.KeyPersonnel;
        tbKpPosition.Text = companyInfo.KpPosition;

        CheckVendorClassification();
    }

    private void CheckVendorClassification()
    {
        //string connstring = HttpContext.Current.Session["ConnectionString"].ToString();

        SqlParameter[] sqlparams = new SqlParameter[1];
        sqlparams[0] = new SqlParameter("@vendorId", SqlDbType.Int);
        sqlparams[0].Value = Int32.Parse(ViewState[Constant.SESSION_USERID].ToString().Trim());

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

    private void UpdateSVCDataRow()
    {
        if (ViewState["dtPresentSvc"] != null)
        {
            DataTable presentsvc = (DataTable)ViewState["dtPresentSvc"];

            if (gvPresentSvc.Rows.Count > 0)
            {
                HiddenField hdnPlanID = null;
                HiddenField hdnPresentSvcID = null;
                DropDownList ddList = null;
                TextBox lblAcctNo = null;
                TextBox lblCreditLine = null;

                int i = 0;
                foreach (GridViewRow row in gvPresentSvc.Rows)
                {
                    //row.BeginEdit();
                    ddList = (DropDownList)row.FindControl("ddlPlans");

                    hdnPlanID = (HiddenField)row.FindControl("hdPlanID");
                    hdnPresentSvcID = (HiddenField)row.FindControl("hdPresentSvcID");
                    lblAcctNo = (TextBox)row.FindControl("lblAcctNo");
                    lblCreditLine = (TextBox)row.FindControl("lblCreditLimit");

                    presentsvc.Rows[i].BeginEdit();
                    if ((hdnPresentSvcID != null) && (hdnPresentSvcID.Value.Trim() != ""))
                        presentsvc.Rows[i]["PresentServiceID"] = hdnPresentSvcID.Value.Trim();

                    int index = ddList.SelectedIndex + 1;
                    try
                    {
                        presentsvc.Rows[i]["PlanID"] = index.ToString().Trim();
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

    private void UpdateMajCustomersDataRow()
    {
        if (ViewState["MajCustomers"] != null)
        {
            DataTable majcustoms = (DataTable)ViewState["MajCustomers"];

            if (gvMajCustomers.Rows.Count > 0)
            {
                HiddenField hdReferenceNo = null;
                TextBox lblName = null;
                TextBox lblAveMonthly = null;

                int i = 0;
                foreach (GridViewRow row in gvMajCustomers.Rows)
                {
                    //row.BeginEdit();
                    hdReferenceNo = (HiddenField)row.FindControl("hdReferenceNo");
                    lblName = (TextBox)row.FindControl("lblName");
                    lblAveMonthly = (TextBox)row.FindControl("lblAveMonthly");

                    majcustoms.Rows[i].BeginEdit();
                    if ((hdReferenceNo != null) && (hdReferenceNo.Value.Trim() != ""))
                        majcustoms.Rows[i]["ReferencesNo"] = hdReferenceNo.Value.Trim();

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
                HiddenField hdReferenceNo = null;
                TextBox lblName = null;
                TextBox lblCreditLine = null;

                int i = 0;
                foreach (GridViewRow row in gvBanks.Rows)
                {
                    //row.BeginEdit();
                    hdReferenceNo = (HiddenField)row.FindControl("hdReferenceNo");
                    lblName = (TextBox)row.FindControl("lblName");
                    lblCreditLine = (TextBox)row.FindControl("lblCreditLine");

                    banks.Rows[i].BeginEdit();
                    if ((hdReferenceNo != null) && (hdReferenceNo.Value.Trim() != ""))
                        banks.Rows[i]["ReferencesNo"] = hdReferenceNo.Value.Trim();

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
                HiddenField hdReferenceNo = null;
                TextBox lblName = null;
                TextBox lblBusiness = null;

                int i = 0;
                foreach (GridViewRow row in gvAffiliate.Rows)
                {
                    //row.BeginEdit();
                    hdReferenceNo = (HiddenField)row.FindControl("hdReferenceNo");
                    lblName = (TextBox)row.FindControl("lblName");
                    lblBusiness = (TextBox)row.FindControl("lblBusiness");

                    affiliate.Rows[i].BeginEdit();
                    if ((hdReferenceNo != null) && (hdReferenceNo.Value.Trim() != ""))
                        affiliate.Rows[i]["ReferencesNo"] = hdReferenceNo.Value.Trim();

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
                HiddenField hdReferenceNo = null;
                TextBox lblName = null;
                TextBox lblLegalCounsel = null;

                int i = 0;
                foreach (GridViewRow row in gvExtAuditors.Rows)
                {
                    hdReferenceNo = (HiddenField)row.FindControl("hdReferenceNo");
                    lblName = (TextBox)row.FindControl("lblName");
                    lblLegalCounsel = (TextBox)row.FindControl("lblLegalCounsel");

                    extaudit.Rows[i].BeginEdit();
                    if ((hdReferenceNo != null) && (hdReferenceNo.Value.Trim() != ""))
                        extaudit.Rows[i]["ReferencesNo"] = hdReferenceNo.Value.Trim();

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

    private void UpdateEquipmentDataRow()
    {
        if (ViewState["dtEquipments"] != null)
        {
            DataTable equipments = (DataTable)ViewState["dtEquipments"];

            if (gvEquipments.Rows.Count > 0)
            {
                HiddenField hdEquipmentID = null;
                TextBox lblEquipmentType = null;
                TextBox lblUnits = null;
                TextBox lblRemarks = null;

                int i = 0;
                foreach (GridViewRow row in gvEquipments.Rows)
                {
                    //row.BeginEdit();
                    hdEquipmentID = (HiddenField)row.FindControl("hdEquipmentID");
                    lblEquipmentType = (TextBox)row.FindControl("lblEqpmntType");
                    lblUnits = (TextBox)row.FindControl("lblUnits");
                    lblRemarks = (TextBox)row.FindControl("lblRemarks");

                    equipments.Rows[i].BeginEdit();
                    if ((hdEquipmentID != null) && (hdEquipmentID.Value.Trim() != ""))
                        equipments.Rows[i]["EquipmentID"] = hdEquipmentID.Value.Trim();

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

    private void UpdateRelativesDataRow()
    {
        if (ViewState["dtRelatives"] != null)
        {
            DataTable relatives = (DataTable)ViewState["dtRelatives"];

            if (gvRelatives.Rows.Count > 0)
            {
                HiddenField hdRelativeID = null;
                TextBox lblRelative = null;
                TextBox lblTitle = null;
                TextBox lblRelationship = null;

                int i = 0;
                foreach (GridViewRow row in gvRelatives.Rows)
                {
                    //row.BeginEdit();
                    hdRelativeID = (HiddenField)row.FindControl("hdRelativeID");
                    lblRelative = (TextBox)row.FindControl("lblRelative");
                    lblTitle = (TextBox)row.FindControl("lblTitle");
                    lblRelationship = (TextBox)row.FindControl("lblRelationship");

                    relatives.Rows[i].BeginEdit();
                    if ((hdRelativeID != null) && (hdRelativeID.Value.Trim() != ""))
                        relatives.Rows[i]["VendorRelativeID"] = hdRelativeID.Value.Trim();

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

    private void AddEmptySvcRow(ref DataTable dt)
    {
        if (dt != null)
        {
            DataRow drEmpty = dt.NewRow();

            drEmpty["PresentServiceID"] = System.DBNull.Value;
            drEmpty["PlanName"] = System.DBNull.Value;
            drEmpty["AccountNo"] = System.DBNull.Value;
            drEmpty["CreditLimit"] = System.DBNull.Value;
            dt.Rows.Add(drEmpty);
        }
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

    private void CopyNewCompanyInfo()
    {
        CompanyDetails companyInfo = (CompanyDetails)ViewState["CompanyDetails"];

        companyInfo.CompanyName = tbCompanyName.Text;
        companyInfo.Address = tbHeadOfficeAddress1.Text;
        companyInfo.Address1 = tbHeadOfficeAddress2.Text;
        companyInfo.HeadTelephone = tbTelephone.Text;
        companyInfo.MobileNo = tbMobileNo.Text;
        companyInfo.HeadFax = tbFax.Text;
        companyInfo.HeadExtension = tbExtension.Text;
        companyInfo.Address2 = tbBranchAddress1.Text;
        companyInfo.Address3 = tbBranchAddress2.Text;
        companyInfo.BranchTelephone = tbBranchPhone.Text;
        companyInfo.BranchFax = tbBranchFax.Text;
        companyInfo.BranchExtension = tbBranchExtension.Text;
        companyInfo.VatRegNo = tbVatRegNo.Text;
        companyInfo.TIN = tbTIN.Text;
        companyInfo.POBox = tbPOBox.Text;
        companyInfo.PostalCode = tbPostalCode.Text;
        companyInfo.CompanyEmail = tbEmail.Text;
        companyInfo.TermsofPayment = tbTermsofPayment.Text;
        companyInfo.SpecialTerms = tbSpecialTerms.Text;
        companyInfo.MinOrderValue = tbMinOrderValue.Text;
        companyInfo.SalesPerson = tbSalesPerson.Text;
        companyInfo.SalesPersonPhone = tbSalesPersonPhone.Text;

        companyInfo.OrganizationType = Int32.Parse(rbOrganizationType.SelectedValue);

        companyInfo.OwnershipFilipino = tbOwnershipFilipino.Text;
        companyInfo.OwnershipOther = tbOwnershipOther.Text;

        // companyInfo.Classification = Int32.Parse(rbClassification.SelectedValue);
        companyInfo.Classification = 0;
        companyInfo.SoleSupplier = tbSoleSupplier1.Text;
        companyInfo.SoleSupplier1 = tbSoleSupplier2.Text;
        companyInfo.Specialization = tbSpecialization.Text;

        companyInfo.KeyPersonnel = tbKeyPersonnel.Text;
        companyInfo.KpPosition = tbKpPosition.Text;
        ViewState["CompanyDetails"] = companyInfo;
    }

    private void CopyVendorClassification()
    {
        string tempBuf = "";

        if (chkCompanyClassification_4.Checked == true)
        {
            tempBuf = "5";
        }
        else
        {
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
        }

        Session["Classification"] = tempBuf;
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

    #region SAVE

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
                            companyTrans.DeletePresentSvc(connstring, row["ID"].ToString().Trim());
                            break;
                        case 2:
                            companyTrans.DeleteReference(connstring, row["ID"].ToString().Trim());
                            break;
                        case 3:
                            companyTrans.DeleteEquipment(connstring, row["ID"].ToString().Trim());
                            break;
                        case 4:
                            companyTrans.DeleteRelative(connstring, row["ID"].ToString().Trim());
                            break;
                    }
                }
            }
        }
    }

    private void SavePresentServices()
    {
        if (ViewState["PresentSvc"] != null)
        {
            DataTable presentSvc = (DataTable)ViewState["PresentSvc"];
            string vendorID = ViewState[Constant.SESSION_USERID].ToString().Trim();
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
                        companyTrans.UpdatePresentServices(connstring, vendorID, row["PlanID"].ToString().Trim(),
                                                           row["PresentServiceID"].ToString().Trim(),
                                                           row["AccountNo"].ToString().Trim(),
                                                           row["CreditLimit"].ToString().Trim(), isNew);
                }
            }
        }
    }

    private void SaveEquipments()
    {
        if (ViewState["Equipments"] != null)
        {
            CompanyTransaction companyTrans = new CompanyTransaction();
            string vendorID = ViewState[Constant.SESSION_USERID].ToString().Trim();
            DataTable equipments = (DataTable)ViewState["Equipments"];

            if (equipments.Rows.Count > 0)
            {
                foreach (DataRow row in equipments.Rows)
                {
                    bool isNew = true;

                    if (row["EquipmentID"].ToString().Trim().Length > 0)
                        isNew = false;

                    if (row["EquipmentType"].ToString().Trim().Length > 0)
                        companyTrans.UpdateEquipments(connstring, vendorID, row["EquipmentID"].ToString().Trim(),
                                                           row["EquipmentType"].ToString().Trim(),
                                                           row["Units"].ToString().Trim(),
                                                           row["Remarks"].ToString().Trim(), isNew);
                }
            }
        }
    }

    private void SaveRelatives()
    {
        if (ViewState["Relatives"] != null)
        {
            CompanyTransaction companyTrans = new CompanyTransaction();
            string vendorID = ViewState[Constant.SESSION_USERID].ToString().Trim();
            DataTable relatives = (DataTable)ViewState["Relatives"];

            if (relatives.Rows.Count > 0)
            {
                foreach (DataRow row in relatives.Rows)
                {
                    bool isNew = true;

                    if (row["VendorRelativeID"].ToString().Trim().Length > 0)
                        isNew = false;

                    if (row["VendorRelative"].ToString().Trim().Length > 0)
                        companyTrans.UpdateRelatives(connstring, vendorID, row["VendorRelativeID"].ToString().Trim(),
                                                           row["VendorRelative"].ToString().Trim(),
                                                           row["Title"].ToString().Trim(),
                                                           row["Relationship"].ToString().Trim(), isNew);
                }
            }
        }
    }

    private void SaveMajCustomers()
    {
        if (ViewState["MajCustomers"] != null)
        {
            CompanyTransaction companyTrans = new CompanyTransaction();
            string vendorID = ViewState[Constant.SESSION_USERID].ToString().Trim();
            DataTable majCustomers = (DataTable)ViewState["MajCustomers"];

            if (majCustomers.Rows.Count > 0)
            {
                foreach (DataRow row in majCustomers.Rows)
                {
                    bool isNew = true;

                    if (row["ReferencesNo"].ToString().Trim().Length > 0)
                        isNew = false;

                    if (row["CompanyName"].ToString().Trim().Length > 0)
                        companyTrans.UpdateReference1(connstring, vendorID, row["ReferencesNo"].ToString().Trim(),
                                                           row["CompanyName"].ToString().Trim(),
                                                           row["AveMonthlySales"].ToString().Trim(), isNew);
                }
            }
        }
    }

    private void SaveBanks()
    {
        if (ViewState["Banks"] != null)
        {
            CompanyTransaction companyTrans = new CompanyTransaction();
            string vendorID = ViewState[Constant.SESSION_USERID].ToString().Trim();
            DataTable banks = (DataTable)ViewState["Banks"];

            if (banks.Rows.Count > 0)
            {
                foreach (DataRow row in banks.Rows)
                {
                    bool isNew = true;

                    if (row["ReferencesNo"].ToString().Trim().Length > 0)
                        isNew = false;

                    if (row["CompanyName"].ToString().Trim().Length > 0)
                        companyTrans.UpdateReference2(connstring, vendorID, row["ReferencesNo"].ToString().Trim(),
                                                           row["CompanyName"].ToString().Trim(),
                                                           row["CreditLine"].ToString().Trim(), isNew);
                }
            }
        }
    }

    private void SaveAffiliates()
    {
        if (ViewState["AffCompany"] != null)
        {
            CompanyTransaction companyTrans = new CompanyTransaction();
            string vendorID = ViewState[Constant.SESSION_USERID].ToString().Trim();
            DataTable affiliates = (DataTable)ViewState["AffCompany"];

            if (affiliates.Rows.Count > 0)
            {
                foreach (DataRow row in affiliates.Rows)
                {
                    bool isNew = true;

                    if (row["ReferencesNo"].ToString().Trim().Length > 0)
                        isNew = false;

                    if (row["CompanyName"].ToString().Trim().Length > 0)
                        companyTrans.UpdateReference3(connstring, vendorID, row["ReferencesNo"].ToString().Trim(),
                                                           row["CompanyName"].ToString().Trim(),
                                                           row["KindOfBusiness"].ToString().Trim(), isNew);
                }
            }
        }
    }

    private void SaveExtAuditors()
    {
        if (ViewState["ExternAudit"] != null)
        {
            CompanyTransaction companyTrans = new CompanyTransaction();
            string vendorID = ViewState[Constant.SESSION_USERID].ToString().Trim();
            DataTable extAuditors = (DataTable)ViewState["ExternAudit"];

            if (extAuditors.Rows.Count > 0)
            {
                foreach (DataRow row in extAuditors.Rows)
                {
                    bool isNew = true;

                    if (row["ReferencesNo"].ToString().Trim().Length > 0)
                        isNew = false;

                    if (row["CompanyName"].ToString().Trim().Length > 0)
                        companyTrans.UpdateReference4(connstring, vendorID, row["ReferencesNo"].ToString().Trim(),
                                                           row["CompanyName"].ToString().Trim(),
                                                           row["LegalCounsel"].ToString().Trim(), isNew);
                }
            }
        }
    }

    private void SaveVendorClassification()
    {
        string tempBuf = null;

        if (ViewState["Classification"] != null)
            tempBuf = ViewState["Classification"].ToString().Trim();

        if (tempBuf == "")
            tempBuf = "5";

        SqlParameter[] sqlparams = new SqlParameter[2];
        sqlparams[0] = new SqlParameter("@vendorId", SqlDbType.Int);
        sqlparams[0].Value = Int32.Parse(ViewState[Constant.SESSION_USERID].ToString().Trim());
        sqlparams[1] = new SqlParameter("@classIds", SqlDbType.NVarChar);
        sqlparams[1].Value = tempBuf;

        SqlHelper.ExecuteNonQuery(connstring, CommandType.StoredProcedure, "s3p_EBid_UpdateVendorClassification", sqlparams);
    }

    #endregion

    private void ClearSessions()
    {
        Session.Remove("CompanyDetails");
        Session.Remove("PresentSvc");
        Session.Remove("Equipments");
        Session.Remove("Relatives");
        Session.Remove("MajCustomers");
        Session.Remove("Banks");
        Session.Remove("AffCompany");
        Session.Remove("ExternAudit");
        Session.Remove("Deleted");
        Session.Remove("Classification");
    }

    protected void lnkNext_Click(object sender, EventArgs e)
    {
        if (IsValid)
        {
            UpdateSVCDataRow();
            UpdateMajCustomersDataRow();
            UpdateBanksDataRow();
            UpdateAffiliatesDataRow();
            UpdateExternAuditDataRow();
            UpdateEquipmentDataRow();
            UpdateRelativesDataRow();
            CopyNewCompanyInfo();
            CopyVendorClassification();

            Session["CompanyDetails"] = ViewState["CompanyDetails"];
            Session["PresentSvc"] = (DataTable)ViewState["dtPresentSvc"];
            Session["Equipments"] = (DataTable)ViewState["dtEquipments"];
            Session["Relatives"] = (DataTable)ViewState["dtRelatives"];
            Session["MajCustomers"] = (DataTable)ViewState["MajCustomers"];
            Session["Banks"] = (DataTable)ViewState["Banks"];
            Session["AffCompany"] = (DataTable)ViewState["AffCompany"];
            Session["ExternAudit"] = (DataTable)ViewState["ExternAudit"];
            Session["Deleted"] = (DataTable)ViewState["Deleted"];

            //Save All
            if (Session["Deleted"] != null)
                ViewState["Deleted"] = (DataTable)Session["Deleted"];
            if (Session["CompanyDetails"] != null)
                ViewState["CompanyDetails"] = (CompanyDetails)Session["CompanyDetails"];
            if (Session["PresentSvc"] != null)
                ViewState["PresentSvc"] = (DataTable)Session["PresentSvc"];
            if (Session["Equipments"] != null)
                ViewState["Equipments"] = (DataTable)Session["Equipments"];
            if (Session["Relatives"] != null)
                ViewState["Relatives"] = (DataTable)Session["Relatives"];
            if (Session["MajCustomers"] != null)
                ViewState["MajCustomers"] = (DataTable)Session["MajCustomers"];
            if (Session["Banks"] != null)
                ViewState["Banks"] = (DataTable)Session["Banks"];
            if (Session["AffCompany"] != null)
                ViewState["AffCompany"] = (DataTable)Session["AffCompany"];
            if (Session["ExternAudit"] != null)
                ViewState["ExternAudit"] = (DataTable)Session["ExternAudit"];
            if (Session["Classification"] != null)
                ViewState["Classification"] = Session["Classification"].ToString().Trim();

            ClearSessions();

            CompanyDetails companyInfo = (CompanyDetails)ViewState["CompanyDetails"];
            CompanyTransaction companyTrans = new CompanyTransaction();

            companyTrans.UpdateCompanyDetails(connstring, ViewState[Constant.SESSION_USERID].ToString().Trim(), companyInfo);

            DeleteRowsInDB();
            SaveVendorClassification();
            SavePresentServices();
            SaveMajCustomers();
            SaveBanks();
            SaveAffiliates();
            SaveExtAuditors();
            SaveEquipments();
            SaveRelatives();

            Response.Redirect("profile.aspx");
        }
    }

    protected void gvPresentSvc_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int rowid = Convert.ToInt32(e.CommandArgument);
        DataTable presentsvc = (DataTable)ViewState["dtPresentSvc"];
        UpdateSVCDataRow();

        if (e.CommandName.ToString().Trim() == "Remove")
        {
            HiddenField hdnPresentSvcID = (HiddenField)gvPresentSvc.Rows[rowid].FindControl("hdPresentSvcID");
            string hdnID = hdnPresentSvcID.Value.ToString();

            UpdateDeletedIDContainer(hdnID, "1");

            presentsvc.Rows[rowid].Delete();
            presentsvc.AcceptChanges();
            ViewState["dtPresentSvc"] = presentsvc;

            LoadPresentSvcData();
            UpdateSVCDataRow();
        }
        else
        {
            Response.Write("No Action");
        }
    }

    protected void gvPresentSvc_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DropDownList ddlPlans = (DropDownList)e.Row.FindControl("ddlPlans");
            TextBox lblAcctNo = (TextBox)e.Row.FindControl("lblAcctNo");
            TextBox lblCreditLimit = (TextBox)e.Row.FindControl("lblCreditLimit");
            LinkButton lnkRemove = (LinkButton)e.Row.FindControl("lnkRemove");
            HiddenField hdPlanID = (HiddenField)e.Row.FindControl("hdPlanID");
            //  HiddenField hdPresentSvcID = (HiddenField)e.Row.FindControl("hdPresentSvcID");

            //    DataTable presentsvc = (DataTable)ViewState["dtPresentSvc"];
            DataTable dtGlobePlans = (DataTable)ViewState["dtGlobePlans"];
            DataView dvGlobePlans = new DataView(dtGlobePlans);

            ddlPlans.DataSource = dvGlobePlans;
            ddlPlans.DataTextField = "PlanName";
            ddlPlans.DataValueField = "PlanID";
            ddlPlans.DataBind();

            lblAcctNo.Attributes.Add("maxLength", "20");

            try
            {
                ddlPlans.Items[Int32.Parse(hdPlanID.Value.Trim()) - 1].Selected = true;
            }
            catch
            {
                ddlPlans.Items[0].Selected = true;
            }
        }
    }

    protected void gvMajCustomers_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int rowid = Convert.ToInt32(e.CommandArgument);
        DataTable majcustoms = (DataTable)ViewState["MajCustomers"];
        UpdateMajCustomersDataRow();

        if (e.CommandName.ToString().Trim() == "Remove")
        {
            HiddenField hdReferenceNo = (HiddenField)gvMajCustomers.Rows[rowid].FindControl("hdReferenceNo");
            string hdnID = hdReferenceNo.Value.ToString();

            UpdateDeletedIDContainer(hdnID, "2");

            majcustoms.Rows[rowid].Delete();
            majcustoms.AcceptChanges();
            ViewState["MajCustomers"] = majcustoms;

            LoadMajCustomersData();
            UpdateMajCustomersDataRow();
        }
        else
        {
            Response.Write("No Action");
        }
    }

    protected void gvMajCustomers_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            TextBox lblName = (TextBox)e.Row.FindControl("lblName");
            TextBox lblAveMonthly = (TextBox)e.Row.FindControl("lblAveMonthly");
            LinkButton lnkRemove = (LinkButton)e.Row.FindControl("lnkRemove");

            lblName.Attributes.Add("maxLength", "100");
            lblAveMonthly.Attributes.Add("maxLength", "10");

            //    if (lblName.Text.Length > 0)
            //       lnkRemove.Visible = true;
        }
    }

    protected void gvBanks_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int rowid = Convert.ToInt32(e.CommandArgument);
        DataTable banks = (DataTable)ViewState["Banks"];
        UpdateBanksDataRow();

        if (e.CommandName.ToString().Trim() == "Remove")
        {
            HiddenField hdReferenceNo = (HiddenField)gvBanks.Rows[rowid].FindControl("hdReferenceNo");
            string hdnID = hdReferenceNo.Value.ToString();

            UpdateDeletedIDContainer(hdnID, "2");

            banks.Rows[rowid].Delete();
            banks.AcceptChanges();
            ViewState["Banks"] = banks;

            LoadBanksData();
            UpdateBanksDataRow();
        }
        else
        {
            Response.Write("No Action");
        }
    }

    protected void gvBanks_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            TextBox lblName = (TextBox)e.Row.FindControl("lblName");
            TextBox lblCreditLine = (TextBox)e.Row.FindControl("lblCreditLine");
            LinkButton lnkRemove = (LinkButton)e.Row.FindControl("lnkRemove");

            lblName.Attributes.Add("maxLength", "100");
            lblCreditLine.Attributes.Add("maxLength", "10");

            //     if (lblName.Text.Length > 0)
            //         lnkRemove.Visible = true;
        }
    }

    protected void gvAffiliate_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int rowid = Convert.ToInt32(e.CommandArgument);
        DataTable affiliates = (DataTable)ViewState["AffCompany"];
        UpdateAffiliatesDataRow();

        if (e.CommandName.ToString().Trim() == "Remove")
        {
            HiddenField hdReferenceNo = (HiddenField)gvAffiliate.Rows[rowid].FindControl("hdReferenceNo");
            string hdnID = hdReferenceNo.Value.ToString();

            UpdateDeletedIDContainer(hdnID, "2");

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
            LinkButton lnkRemove = (LinkButton)e.Row.FindControl("lnkRemove");

            lblName.Attributes.Add("maxLength", "100");
            lblBusiness.Attributes.Add("maxLength", "100");

            //     if (lblName.Text.Length > 0)
            //         lnkRemove.Visible = true;
        }
    }

    protected void gvExtAuditors_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int rowid = Convert.ToInt32(e.CommandArgument);
        DataTable extAuditors = (DataTable)ViewState["ExternAudit"];
        UpdateExternAuditDataRow();

        if (e.CommandName.ToString().Trim() == "Remove")
        {
            HiddenField hdReferenceNo = (HiddenField)gvExtAuditors.Rows[rowid].FindControl("hdReferenceNo");
            string hdnID = hdReferenceNo.Value.ToString();

            UpdateDeletedIDContainer(hdnID, "2");

            extAuditors.Rows[rowid].Delete();
            extAuditors.AcceptChanges();
            ViewState["ExternAudit"] = extAuditors;

            LoadExtAuditorsData();
            UpdateExternAuditDataRow();
        }
        else
        {
            Response.Write("No Action");
        }
    }

    protected void gvExtAuditors_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            TextBox lblName = (TextBox)e.Row.FindControl("lblName");
            TextBox lblLegalCounsel = (TextBox)e.Row.FindControl("lblLegalCounsel");
            LinkButton lnkRemove = (LinkButton)e.Row.FindControl("lnkRemove");

            lblName.Attributes.Add("maxLength", "100");
            lblLegalCounsel.Attributes.Add("maxLength", "100");

            //   if (lblName.Text.Length > 0)
            //      lnkRemove.Visible = true;
        }
    }

    protected void gvEquipments_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int rowid = Convert.ToInt32(e.CommandArgument);
        DataTable equipments = (DataTable)ViewState["dtEquipments"];
        UpdateEquipmentDataRow();

        if (e.CommandName.ToString().Trim() == "Remove")
        {
            HiddenField hdEquipmentID = (HiddenField)gvEquipments.Rows[rowid].FindControl("hdEquipmentID");
            string hdnID = hdEquipmentID.Value.ToString();

            UpdateDeletedIDContainer(hdnID, "3");

            equipments.Rows[rowid].Delete();
            equipments.AcceptChanges();
            ViewState["dtEquipments"] = equipments;

            LoadEquipmentsData();
            UpdateEquipmentDataRow();
        }
        else
        {
            Response.Write("No Action");
        }
    }

    protected void gvEquipments_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            // HiddenField hdEquipmentID = (HiddenField)e.Row.FindControl("hdEquipmentID");
            TextBox lblEquipmentType = (TextBox)e.Row.FindControl("lblEqpmntType");
            TextBox lblUnits = (TextBox)e.Row.FindControl("lblUnits");
            TextBox lblRemarks = (TextBox)e.Row.FindControl("lblRemarks");
            LinkButton lnkRemove = (LinkButton)e.Row.FindControl("lnkRemove");

            // DataTable equipments = (DataTable)ViewState["dtEquipments"];

            lblUnits.Attributes.Add("maxLength", "10");
            lblEquipmentType.Attributes.Add("maxLength", "50");
            lblRemarks.Attributes.Add("maxLength", "200");

            //  if ((lblEquipmentType.Text.Length > 0) && (lblRemarks.Text.Length > 0))
            //      lnkRemove.Visible = true;
        }
    }

    protected void gvRelatives_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int rowid = Convert.ToInt32(e.CommandArgument);
        DataTable relatives = (DataTable)ViewState["dtRelatives"];
        UpdateRelativesDataRow();

        if (e.CommandName.ToString().Trim() == "Remove")
        {
            HiddenField hdRelativeID = (HiddenField)gvRelatives.Rows[rowid].FindControl("hdRelativeID");
            string hdnID = hdRelativeID.Value.ToString();

            UpdateDeletedIDContainer(hdnID, "4");
            relatives.Rows[rowid].Delete();
            relatives.AcceptChanges();
            ViewState["dtRelatives"] = relatives;

            LoadRelativesData();
            UpdateRelativesDataRow();
        }
        else
        {
            Response.Write("No Action");
        }
    }

    protected void gvRelatives_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            TextBox lblRelative = (TextBox)e.Row.FindControl("lblRelative");
            TextBox lblTitle = (TextBox)e.Row.FindControl("lblTitle");
            TextBox lblRelationship = (TextBox)e.Row.FindControl("lblRelationship");
            LinkButton lnkRemove = (LinkButton)e.Row.FindControl("lnkRemove");

            //  DataTable relatives = (DataTable)ViewState["dtRelatives"];
            lblRelative.Attributes.Add("maxLength", "50");
            lblTitle.Attributes.Add("maxLength", "50");
            lblRelationship.Attributes.Add("maxLength", "50");

            //    if ((lblRelative.Text.Length > 0) && (lblTitle.Text.Length > 0))
            //        lnkRemove.Visible = true;
        }
    }

    protected void CheckMobileNo(object source, ServerValidateEventArgs args)
    {
        args.IsValid = SMSHelper.AreValidMobileNumbers(tbMobileNo.Text.ToString().Trim());
    }
}