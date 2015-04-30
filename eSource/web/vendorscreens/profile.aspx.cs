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

public partial class web_vendorscreens_Profile : System.Web.UI.Page
{
    private string connstring = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        FormsAuthenticationHelper.AuthenticateUserWithReturnUrl();
        connstring = Client.ConnectionString(HttpContext.Current.Session["clientid"].ToString());
        if (Int32.Parse(Session[Constant.SESSION_USERTYPE].ToString().Trim()) != (int)Constant.USERTYPE.VENDOR)
            Response.Redirect("../unauthorizedaccess.aspx");

        PageTitle.InnerText = String.Format(Constant.TITLEFORMAT, "My Profile");

        if (!(Page.IsPostBack))
        {
            if (Session[Constant.SESSION_USERID] != null)
            {
                ViewState[Constant.SESSION_USERID] = Session[Constant.SESSION_USERID].ToString().Trim();
                itemDetailsV.Visible = false;

                DisplayCompanyInfo();
                DisplayPresentServices();
                DisplayReferences();
                DisplayEquipments();
                DisplayRelatives();
                DisplayOthers();
            }
        }
    }

    private void DisplayCompanyInfo()
    {
        CompanyTransaction companyDetails = new CompanyTransaction();
        CompanyDetails companyInfo = companyDetails.QueryCompanyDetails(connstring, ViewState[Constant.SESSION_USERID].ToString().Trim());

        lblCompanyName.Text = companyInfo.CompanyName;
		lblHeadOfficeAddress.Text = (String.IsNullOrEmpty(companyInfo.Address)) ? "&nbsp;" : companyInfo.Address + " " + companyInfo.Address1;
        lblTelephone.Text = companyInfo.HeadTelephone;
        lblMobileNo.Text = companyInfo.MobileNo;
        lblFax.Text = companyInfo.HeadFax;
        lblExtension.Text = companyInfo.HeadExtension;
        lblBranchAddress.Text = companyInfo.Address2 + " " + companyInfo.Address3;
        lblBranchPhone.Text = companyInfo.BranchTelephone;
        lblBranchFax.Text = companyInfo.BranchFax;
        lblBranchExtension.Text = companyInfo.BranchExtension;
        lblVatRegNo.Text = companyInfo.VatRegNo;
        lblTin.Text = companyInfo.TIN;
        lblPOBox.Text = companyInfo.POBox;
        lblPostalCode.Text = companyInfo.PostalCode;
        lblEmail.Text = companyInfo.CompanyEmail;
        lblTermsofPayment.Text = companyInfo.TermsofPayment;
        lblSpecialTerms.Text = companyInfo.SpecialTerms + " %PPD";
        lblMinOrderValue.Text = companyInfo.MinOrderValue;
        lblSalesPerson.Text = companyInfo.SalesPerson;
        lblSalesPersonPhone.Text = companyInfo.SalesPersonPhone;
        lblOrgType.Text = companyDetails.GetOrganizationType(connstring, companyInfo.OrganizationType.ToString().Trim());
        lblOwnershipFilipino.Text = companyInfo.OwnershipFilipino + "%";
        lblOwnershipOther.Text = companyInfo.OwnershipOther + "%";
       // lblClassification.Text = companyDetails.GetClassificationName(companyInfo.Classification.ToString().Trim());
        lblClassification.Text = GetVendorClassification();
        lblSoleSupplier.Text = companyInfo.SoleSupplier + ((companyInfo.SoleSupplier1.Trim().Length > 0) ? ", " + companyInfo.SoleSupplier1: "");
        lblSpecialization.Text = companyInfo.Specialization;

        lblKeyPersonnel.Text = companyInfo.KeyPersonnel;
        lblKpPosition.Text = companyInfo.KpPosition;
    }

    private string GetVendorClassification()
    {
        //string connstring = HttpContext.Current.Session["ConnectionString"].ToString();

        string tempBuf = "";

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
                        tempBuf = row["ClassificationName"].ToString().Trim();
                        break;
                    case 2:
                        if (tempBuf != "")
                            tempBuf += " / ";

                        tempBuf += row["ClassificationName"].ToString().Trim();
                        break;
                    case 3:
                        if (tempBuf != "")
                            tempBuf += " / ";

                        tempBuf += row["ClassificationName"].ToString().Trim();
                        break;
                    case 4:
                        if (tempBuf != "")
                            tempBuf += " / ";

                        tempBuf += row["ClassificationName"].ToString().Trim();
                        break;
                    case 5:
                        tempBuf = "N/A";
                        break;
                }
            }
        }
        else
        {
            tempBuf = "N/A";
        }
        
        if(tempBuf == "")
            tempBuf = "N/A";

        return tempBuf;
    }

    private void DisplayPresentServices()
    {
        CompanyTransaction companyDetails = new CompanyTransaction();
        DataTable presentsvc = companyDetails.QueryPresentServices(connstring, ViewState[Constant.SESSION_USERID].ToString().Trim());
        HtmlTableRow htmlRow = new HtmlTableRow();
        HtmlTableCell[] cell = new HtmlTableCell[3];

        if (presentsvc.Rows.Count > 0)
        {
            int i = 1;
            foreach (DataRow row in presentsvc.Rows)
            {
                htmlRow = new HtmlTableRow();
                cell[0] = new HtmlTableCell();
                cell[0].Attributes.Add("class", ((i % 2 == 0) ? "evenCells" : "value"));
                cell[0].Controls.Add(new LiteralControl(row["PlanName"].ToString()));
                htmlRow.Cells.Add(cell[0]);
                cell[1] = new HtmlTableCell();
                cell[1].Attributes.Add("class", ((i % 2 == 0) ? "evenCells" : "value"));
                cell[1].Controls.Add(new LiteralControl(row["AccountNo"].ToString()));
                htmlRow.Cells.Add(cell[1]);
                cell[2] = new HtmlTableCell();
                cell[2].Attributes.Add("class", ((i % 2 == 0) ? "evenCells" : "value"));
                cell[2].Controls.Add(new LiteralControl(row["CreditLimit"].ToString()));
                htmlRow.Cells.Add(cell[2]);
                itemDetails2.Rows.Add(htmlRow);
                i++;
            }
           
        }
    }

    private void DisplayReferences()
    {
        CompanyTransaction companyDetails = new CompanyTransaction();
        DataTable vendorRef;
        string refTypeName, refTypeExtra;

        vendorRef = companyDetails.QueryVendorReferences(connstring, ViewState[Constant.SESSION_USERID].ToString().Trim(), Constant.REF_MAIN_CUSTOMERS);
        refTypeName = companyDetails.GetReferenceTypeName(connstring, Constant.REF_MAIN_CUSTOMERS.ToString());
        refTypeExtra = companyDetails.GetReferenceTypeExtra(connstring, Constant.REF_MAIN_CUSTOMERS.ToString());
        FillReferenceTable(vendorRef, "AveMonthlySales", refTypeName, refTypeExtra);

        vendorRef = companyDetails.QueryVendorReferences(connstring, ViewState[Constant.SESSION_USERID].ToString().Trim(), Constant.REF_BANKS);
        refTypeName = companyDetails.GetReferenceTypeName(connstring, Constant.REF_BANKS.ToString());
        refTypeExtra = companyDetails.GetReferenceTypeExtra(connstring, Constant.REF_BANKS.ToString());
        FillReferenceTable(vendorRef, "CreditLine", refTypeName, refTypeExtra);

        vendorRef = companyDetails.QueryVendorReferences(connstring, ViewState[Constant.SESSION_USERID].ToString().Trim(), Constant.REF_AFFILIATE);
        refTypeName = companyDetails.GetReferenceTypeName(connstring, Constant.REF_AFFILIATE.ToString());
        refTypeExtra = companyDetails.GetReferenceTypeExtra(connstring, Constant.REF_AFFILIATE.ToString());
        FillReferenceTable(vendorRef, "KindOfBusiness", refTypeName, refTypeExtra);

        vendorRef = companyDetails.QueryVendorReferences(connstring, ViewState[Constant.SESSION_USERID].ToString().Trim(), Constant.REF_EXTERN_AUDITOR);
        refTypeName = companyDetails.GetReferenceTypeName(connstring, Constant.REF_EXTERN_AUDITOR.ToString());
        refTypeExtra = companyDetails.GetReferenceTypeExtra(connstring, Constant.REF_EXTERN_AUDITOR.ToString());
        FillReferenceTable(vendorRef, "LegalCounsel", refTypeName, refTypeExtra);
    }

    private void FillReferenceTable(DataTable dtVendorRef, string refColName, string refTypeName, string refTypeExtra)
    {
        HtmlTableRow htmlRow = new HtmlTableRow();
        HtmlTableCell[] cell = new HtmlTableCell[1];

        if (dtVendorRef.Rows.Count > 0)
        {
            htmlRow = new HtmlTableRow();
            cell[0] = new HtmlTableCell();
            cell[0].Controls.Add(new LiteralControl(refTypeName));
            htmlRow.Cells.Add(cell[0]);
            cell[0] = new HtmlTableCell();
            cell[0].Controls.Add(new LiteralControl(refTypeExtra));
            htmlRow.Cells.Add(cell[0]);
            itemDetails3.Rows.Add(htmlRow);

            int i=1;
            foreach (DataRow row in dtVendorRef.Rows)
            {
                htmlRow = new HtmlTableRow();
                cell[0] = new HtmlTableCell();
                cell[0].Attributes.Add("class", ((i%2 == 0)?"evenCells":"value"));
                cell[0].Controls.Add(new LiteralControl(row["CompanyName"].ToString()));
                htmlRow.Cells.Add(cell[0]);
                cell[0] = new HtmlTableCell();
                cell[0].Attributes.Add("class", ((i % 2 == 0) ? "evenCells" : "value"));
                cell[0].Controls.Add(new LiteralControl(row[refColName].ToString()));
                htmlRow.Cells.Add(cell[0]);
                itemDetails3.Rows.Add(htmlRow);
                i++;
            }
        }
    }

    private void DisplayEquipments()
    {
        CompanyTransaction companyDetails = new CompanyTransaction();
        DataTable equipments = companyDetails.QueryVendorEquipments(connstring, ViewState[Constant.SESSION_USERID].ToString().Trim());
        HtmlTableRow htmlRow = new HtmlTableRow();
        HtmlTableCell[] cell = new HtmlTableCell[3];

        if (equipments.Rows.Count > 0)
        {
            int i = 1;
            foreach (DataRow row in equipments.Rows)
            {
                htmlRow = new HtmlTableRow();
                cell[0] = new HtmlTableCell();
                cell[0].Attributes.Add("class", ((i % 2 == 0) ? "evenCells" : "value"));
                cell[0].Controls.Add(new LiteralControl(row["EquipmentType"].ToString()));
                htmlRow.Cells.Add(cell[0]);
                cell[1] = new HtmlTableCell();
                cell[1].Attributes.Add("class", ((i % 2 == 0) ? "evenCells" : "value"));
                cell[1].Controls.Add(new LiteralControl(row["Units"].ToString()));
                htmlRow.Cells.Add(cell[1]);
                cell[2] = new HtmlTableCell();
                cell[2].Attributes.Add("class", ((i % 2 == 0) ? "evenCells" : "value"));
                cell[2].Controls.Add(new LiteralControl(row["Remarks"].ToString()));
                htmlRow.Cells.Add(cell[2]);
                itemDetails4.Rows.Add(htmlRow);
                i++;
            }

        }
    }

    private void DisplayRelatives()
    {
        CompanyTransaction companyDetails = new CompanyTransaction();
        DataTable equipments = companyDetails.QueryVendorRelatives(connstring, ViewState[Constant.SESSION_USERID].ToString().Trim());
        HtmlTableRow htmlRow = new HtmlTableRow();
        HtmlTableCell[] cell = new HtmlTableCell[3];

        if (equipments.Rows.Count > 0)
        {
            itemDetailsV.Visible = true;
            int i = 1;
            foreach (DataRow row in equipments.Rows)
            {
                htmlRow = new HtmlTableRow();
                cell[0] = new HtmlTableCell();
                cell[0].Attributes.Add("class", ((i % 2 == 0) ? "evenCells" : "value"));
                cell[0].Controls.Add(new LiteralControl(row["VendorRelative"].ToString()));
                htmlRow.Cells.Add(cell[0]);
                cell[1] = new HtmlTableCell();
                cell[1].Attributes.Add("class", ((i % 2 == 0) ? "evenCells" : "value"));
                cell[1].Controls.Add(new LiteralControl(row["Title"].ToString()));
                htmlRow.Cells.Add(cell[1]);
                cell[2] = new HtmlTableCell();
                cell[2].Attributes.Add("class", ((i % 2 == 0) ? "evenCells" : "value"));
                cell[2].Controls.Add(new LiteralControl(row["Relationship"].ToString()));
                htmlRow.Cells.Add(cell[2]);
                itemDetailsV.Rows.Add(htmlRow);
                i++;
            }

        }
    }

    private void DisplayOthers()
    {
        //string connstring = HttpContext.Current.Session["ConnectionString"].ToString();

        SupplierTransaction st = new SupplierTransaction();
        Supplier s = st.QuerySuppliers(connstring, ViewState[Constant.SESSION_USERID].ToString().Trim());

        SqlParameter[] sqlParams = new SqlParameter[1];
        sqlParams[0] = new SqlParameter("@SupplierTypeId", SqlDbType.Int);
        sqlParams[0].Value = Int32.Parse(s.Accredited.Trim());

        lblSupplierType.Text = SqlHelper.ExecuteScalar(connstring, CommandType.StoredProcedure, "s3p_EBid_GetSupplierType", sqlParams).ToString().Trim();

        //to be updated when rfcCategory is finalized        
      //  lblCategory.Text = s.VendorCategory.Trim();
      //  lblSubCategory.Text = s.VendorSubCategory.Trim();

        switch (s.PCABClass.Trim())
        {
            case "1":
                lblPCABClass.Text = "A";
                break;
            case "2":
                lblPCABClass.Text = "AA";
                break;
            case "3":
                lblPCABClass.Text = "AAA";
                break;
            case "4":
                lblPCABClass.Text = "B";
                break;
            case "5":
                lblPCABClass.Text = "C";
                break;
            case "6":
                lblPCABClass.Text = "D";
                break;
        }
        

        switch (s.ISOStandard.Trim())
        {
            case "01":
                lblISOStandard.Text = "ISO 9002";
                break;
            case "10":
                lblISOStandard.Text = "ISO 9001";
                break;
            case "11":
                lblISOStandard.Text = "ISO 9001<br />ISO 9002";
                break;
        }

        gvBrands.DataSource = st.QueryBrandsByVendorId(connstring, ViewState[Constant.SESSION_USERID].ToString().Trim());
        gvBrands.DataBind();

        gvItems.DataSource = st.QueryItemsCarriedByVendorId(connstring, ViewState[Constant.SESSION_USERID].ToString().Trim());
        gvItems.DataBind();

        gvLocations.DataSource = st.QueryLocationsByVendorId(connstring, ViewState[Constant.SESSION_USERID].ToString().Trim());
        gvLocations.DataBind();

        gvServices.DataSource = st.QueryServicesByVendorId(connstring, ViewState[Constant.SESSION_USERID].ToString().Trim());
        gvServices.DataBind();
    }

    protected void lnkEdit_Click(object sender, EventArgs e)
    {
        if (ViewState[Constant.SESSION_USERID] != null)
        {
            Session[Constant.SESSION_USERID] = ViewState[Constant.SESSION_USERID].ToString().Trim();
            Response.Redirect("profileedit.aspx");
        }
    }
}
