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

public partial class web_boc_SupplierDetails : System.Web.UI.Page
{
    private string connstring = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        FormsAuthenticationHelper.AuthenticateUserWithReturnUrl();
        connstring = Client.ConnectionString(HttpContext.Current.Session["clientid"].ToString());
        if (Int32.Parse(Session[Constant.SESSION_USERTYPE].ToString().Trim()) != (int)Constant.USERTYPE.BOC)
            Response.Redirect("../unauthorizedaccess.aspx");

        if (!(Page.IsPostBack))
        {
            if (Session[Constant.SESSION_USERID] != null)
            {
                if (Request.QueryString["VendorID"] != null)
                    Session["VendorID"] = Request.QueryString["VendorID"].ToString().Trim();

                if(Session["VendorID"] != null)
                    ViewState["VendorID"] = Session["VendorID"].ToString().Trim();

                if (ViewState["VendorID"] != null)
                {
                    itemDetails5.Visible = false;

                    DisplayCompanyInfo();
                    DisplayPresentServices();
                    DisplayReferences();
                    DisplayEquipments();
                    DisplayRelatives();
                    DisplayOthers();
                }
            }
            
        }
        PageTitle.InnerText = String.Format(Constant.TITLEFORMAT, "Supplier Details");
    }

    private void DisplayCompanyInfo()
    {
        CompanyTransaction companyDetails = new CompanyTransaction();
        CompanyDetails companyInfo = companyDetails.QueryCompanyDetails(connstring, ViewState["VendorID"].ToString().Trim());

        lblCompanyName.Text = companyInfo.CompanyName;

        if (companyInfo.IsBlackListed == 1)
        {
            lblIsBlacklisted.Visible = true;
            btBlackList.Text = "Remove Blacklist";
            hdBlacklisted.Value = "1";
        }
        else
        {
            lblIsBlacklisted.Visible = false;
            btBlackList.Text = "Blacklist Supplier";
            hdBlacklisted.Value = "0";
        }

        lblHeadOfficeAddress.Text = companyInfo.Address + " " + companyInfo.Address1;
        lblTelephone.Text = companyInfo.HeadTelephone;
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
        lblClassification.Text = companyDetails.GetClassificationName(connstring, companyInfo.Classification.ToString().Trim());
        lblSoleSupplier.Text = companyInfo.SoleSupplier + ((companyInfo.SoleSupplier1 != null) ? ", " + companyInfo.SoleSupplier1 : "");
        lblSpecialization.Text = companyInfo.Specialization;

        lblKeyPersonnel.Text = companyInfo.KeyPersonnel;
        lblKpPosition.Text = companyInfo.KpPosition;

        switch (companyInfo.ISOStandard.Trim())
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
    }

    private void DisplayPresentServices()
    {
        CompanyTransaction companyDetails = new CompanyTransaction();
        DataTable presentsvc = companyDetails.QueryPresentServices(connstring, ViewState["VendorID"].ToString().Trim());
        HtmlTableRow htmlRow = new HtmlTableRow();
        HtmlTableCell[] cell = new HtmlTableCell[3];

        if (presentsvc.Rows.Count > 0)
        {
            itemDetails2.Visible = true;

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
        else
            itemDetails2.Visible = false;
    }

    private void DisplayReferences()
    {
        CompanyTransaction companyDetails = new CompanyTransaction();
        DataTable vendorRef;
        string refTypeName, refTypeExtra;

        vendorRef = companyDetails.QueryVendorReferences(connstring, ViewState["VendorID"].ToString().Trim(), Constant.REF_MAIN_CUSTOMERS);
        refTypeName = companyDetails.GetReferenceTypeName(connstring, Constant.REF_MAIN_CUSTOMERS.ToString());
        refTypeExtra = companyDetails.GetReferenceTypeExtra(connstring, Constant.REF_MAIN_CUSTOMERS.ToString());
        FillReferenceTable(vendorRef, "AveMonthlySales", refTypeName, refTypeExtra);

        vendorRef = companyDetails.QueryVendorReferences(connstring, ViewState["VendorID"].ToString().Trim(), Constant.REF_BANKS);
        refTypeName = companyDetails.GetReferenceTypeName(connstring, Constant.REF_BANKS.ToString());
        refTypeExtra = companyDetails.GetReferenceTypeExtra(connstring, Constant.REF_BANKS.ToString());
        FillReferenceTable(vendorRef, "CreditLine", refTypeName, refTypeExtra);

        vendorRef = companyDetails.QueryVendorReferences(connstring, ViewState["VendorID"].ToString().Trim(), Constant.REF_AFFILIATE);
        refTypeName = companyDetails.GetReferenceTypeName(connstring, Constant.REF_AFFILIATE.ToString());
        refTypeExtra = companyDetails.GetReferenceTypeExtra(connstring, Constant.REF_AFFILIATE.ToString());
        FillReferenceTable(vendorRef, "KindOfBusiness", refTypeName, refTypeExtra);

        vendorRef = companyDetails.QueryVendorReferences(connstring, ViewState["VendorID"].ToString().Trim(), Constant.REF_EXTERN_AUDITOR);
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

            int i = 1;
            foreach (DataRow row in dtVendorRef.Rows)
            {
                htmlRow = new HtmlTableRow();
                cell[0] = new HtmlTableCell();
                cell[0].Attributes.Add("class", ((i % 2 == 0) ? "evenCells" : "value"));
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
        DataTable equipments = companyDetails.QueryVendorEquipments(connstring, ViewState["VendorID"].ToString().Trim());
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
        DataTable equipments = companyDetails.QueryVendorRelatives(connstring, ViewState["VendorID"].ToString().Trim());
        HtmlTableRow htmlRow = new HtmlTableRow();
        HtmlTableCell[] cell = new HtmlTableCell[3];

        if (equipments.Rows.Count > 0)
        {
            itemDetails5.Visible = true;
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
                itemDetails5.Rows.Add(htmlRow);
                i++;
            }
        }
        else
            itemDetails5.Visible = false;
    }

    private void DisplayOthers()
    {
        string connstring = HttpContext.Current.Session["ConnectionString"].ToString();

        SupplierTransaction st = new SupplierTransaction();
        Supplier s = st.QuerySuppliers(connstring, Session["VendorID"].ToString().Trim());

        SqlParameter[] sqlParams = new SqlParameter[1];
        sqlParams[0] = new SqlParameter("@SupplierTypeId", SqlDbType.Int);
        sqlParams[0].Value = Int32.Parse(s.Accredited.Trim());

        lblSupplierType.Text = SqlHelper.ExecuteScalar(connstring, CommandType.StoredProcedure, "s3p_EBid_GetSupplierType", sqlParams).ToString().Trim();

        //lblSupplierType.Text = s.Accredited.Trim();
      //  lblCategory.Text = s.VendorCategory.Trim();
      //  lblSubCategory.Text = s.VendorSubCategory.Trim();
        lblPCABClass.Text = s.PCABClass.Trim();

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

        gvBrands.DataSource = st.QueryBrandsByVendorId(connstring, ViewState["VendorID"].ToString().Trim());
        gvBrands.DataBind();

        gvItems.DataSource = st.QueryItemsCarriedByVendorId(connstring, ViewState["VendorID"].ToString().Trim());
        gvItems.DataBind();

        gvLocations.DataSource = st.QueryLocationsByVendorId(connstring, ViewState["VendorID"].ToString().Trim());
        gvLocations.DataBind();

        gvServices.DataSource = st.QueryServicesByVendorId(connstring, ViewState["VendorID"].ToString().Trim());
        gvServices.DataBind();
    }

    protected void btBlackList_Click(object sender, EventArgs e)
    {
        string connstring = HttpContext.Current.Session["ConnectionString"].ToString();

        SqlParameter[] sqlParams = new SqlParameter[2];
        sqlParams[0] = new SqlParameter("@vendorID", SqlDbType.Int);
        sqlParams[1] = new SqlParameter("@blackList", SqlDbType.Int);
        sqlParams[0].Value = Int32.Parse(ViewState["VendorID"].ToString().Trim());
        sqlParams[1].Value = Int32.Parse(hdBlacklisted.Value);

        SqlHelper.ExecuteNonQuery(connstring, CommandType.StoredProcedure, "s3p_EBid_UpdateBlackListStatus", sqlParams);

        Response.Redirect("supplierdetails.aspx");
    }
}
