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
using EBid.lib.bid.data;
using EBid.lib;
using EBid.ConnectionString;
using EBid.lib.constant;

public partial class web_buyer_screens_SupplierDetails : System.Web.UI.Page
{
    private string connstring = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        FormsAuthenticationHelper.AuthenticateUserWithReturnUrl();
        connstring = Client.ConnectionString(HttpContext.Current.Session["clientid"].ToString());
        if (Int32.Parse(Session[Constant.SESSION_USERTYPE].ToString().Trim()) != (int)Constant.USERTYPE.BUYER)
            Response.Redirect("../unauthorizedaccess.aspx");

        if (!(Page.IsPostBack))
        {
            if (Session["VendorId"]!=null)
            {
            ViewState["VendorId"] = Session["VendorId"].ToString().Trim();
            SupplierTransaction st = new SupplierTransaction();
            Supplier s = st.QuerySuppliers(connstring, ViewState["VendorId"].ToString().Trim());
            lblCompanyName.Text = s.VendorName.Trim();
            lblAddressHeadOffice.Text = s.VendorAddress.Trim() + " " + s.VendorAddress1.Trim();
            lblTelephone.Text = s.TelephoneNumber.Trim();
            lblFax.Text = s.Fax.Trim();
            lblExtension.Text = s.Extension.Trim();
            lblAddressBranch.Text = s.VendorAddress2.Trim() + " " + s.VendorAddress3.Trim();
            lblTelephone1.Text = s.BranchTelephoneNo.Trim();
            lblFax1.Text = s.BranchFax.Trim();
            lblExtension1.Text = s.BranchExtension.Trim();
            lblVatRegNo.Text = s.VatRegNo.Trim();
            lblTin.Text = s.TIN.Trim();
            lblPOBox.Text = s.POBOX.Trim();
            lblPostalCode.Text = s.PostalCode.Trim();
            lblEmail.Text = s.VendorEmail.Trim();
            lblStandardTerms.Text = s.TermsOfPayment.Trim();
            lblSpecialTerms.Text = s.SpecialTerms.Trim();
            lblMinimumOrderValue.Text = s.MinOrderValue.Trim();
            lblSalesPerson.Text = s.ContactPerson.Trim();
            lblTelephone2.Text = s.SalesPersonTelNo.Trim();
            lblTypeOfBusinessOrganization.Text = s.OrgTypeId;
            lblFilipino.Text = s.OwnershipFilipino.Trim();
            lblOtherNationality.Text = s.OwnershipOther.Trim();
            lblCompanyClassification.Text = "";
            lblSoleSupplier.Text = s.SoleSupplier1.Trim() + " " + s.SoleSupplier2.Trim();
            lblSpecialization.Text = s.Specialization.Trim();

            SupplierType sut = new SupplierType();
            lblSupplierType.Text = sut.GetSupplierType(connstring, s.Accredited.Trim());
            PCABClassTransaction pcab = new PCABClassTransaction();
                lblPCABClass.Text = pcab.GetPCABClasName(connstring, s.PCABClass.Trim());
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
             
            DataSet ds = st.QueryCategoryAndSubCategory(connstring, ViewState["VendorId"].ToString().Trim());
            string strcategory = "";
            string strsubcategory = "";
    
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0] != null)
                    {
                        DataTable dtCategory = ds.Tables[0];
                        if (dtCategory.Rows.Count > 0)
                        {
                            
                            for (int i = 0; i < dtCategory.Rows.Count; i++)
                            {
                                strcategory = (strcategory == "" ? dtCategory.Rows[i]["CategoryName"].ToString() : strcategory +  "<br />" + dtCategory.Rows[i]["CategoryName"].ToString());
                            }
                        }
                    }
                    if (ds.Tables[1] != null)
                    {
                        DataTable dtSubCategory = ds.Tables[1];
                        if (dtSubCategory.Rows.Count > 0)
                        {

                            for (int i = 0; i < dtSubCategory.Rows.Count; i++)
                            {
                                strsubcategory = (strsubcategory == "" ? dtSubCategory.Rows[i]["CompleteName"].ToString() : strsubcategory + "<br />" + dtSubCategory.Rows[i]["CompleteName"].ToString());
                            }
                        }
                    }
                }
                lblCategory.Text = strcategory;
                lblSubCategory.Text = strsubcategory;
             


            gvKeyPersonnel.DataSource = CreateKeyPersonell(s.KeyPersonnel.Trim(), s.KeyPosition.Trim());
            gvKeyPersonnel.DataBind();

            gvPresentServices.DataSource = st.QueryPresentServices(connstring, ViewState["VendorId"].ToString().Trim());
            gvPresentServices.DataBind();

            DataTable dt = st.QueryMajorCustomers(connstring, ViewState["VendorId"].ToString().Trim());
            if (dt.Rows.Count == 0)
                dt = CreateEmptyMajorCustomers();
            gvMajorCustomers.DataSource = dt;
            gvMajorCustomers.DataBind();

             dt = st.QueryBanks(connstring, ViewState["VendorId"].ToString().Trim());
                if (dt.Rows.Count==0)
                    dt = CreateEmptyBanks();
            gvBanks.DataSource = dt;
            gvBanks.DataBind();


             dt = st.QueryAffiliatedCompanies(connstring, ViewState["VendorId"].ToString().Trim());
            if (dt.Rows.Count == 0)
                dt = CreateEmptyAffiliatedCompanies();
            gvAffiliatedCompanies.DataSource = dt;
            gvAffiliatedCompanies.DataBind();

             dt = st.QueryExternalAuditors(connstring, ViewState["VendorId"].ToString().Trim());
            if (dt.Rows.Count == 0)
                dt = CreateEmptyExternalAuditor();
            gvExternalAuditors.DataSource = dt;
            gvExternalAuditors.DataBind();

             dt = st.QueryEquipment(connstring, ViewState["VendorId"].ToString().Trim());
            if (dt.Rows.Count == 0)
                dt = CreateEmptyEquipment();
            gvEquipment.DataSource = dt;
            gvEquipment.DataBind();

             dt = st.QueryRelatives(connstring, ViewState["VendorId"].ToString().Trim());
            if (dt.Rows.Count == 0)
                dt = CreateEmptyRelativesTable();
            gvRelatives.DataSource = dt;
            gvRelatives.DataBind();

            DataTable dtBrands = st.QueryBrandsByVendorId(connstring, Session["VendorId"].ToString().Trim());
            gvBrands.DataSource = dtBrands;
            gvBrands.DataBind();

            DataTable dtItems = st.QueryItemsCarriedByVendorId(connstring, Session["VendorId"].ToString().Trim());
            gvItems.DataSource = dtItems;
            gvItems.DataBind();

            DataTable dtLocations = st.QueryLocationsByVendorId(connstring, Session["VendorId"].ToString().Trim());
            gvLocations.DataSource = dtLocations; 
            gvLocations.DataBind();

            DataTable dtServices = st.QueryServicesByVendorId(connstring, Session["VendorId"].ToString().Trim());
            gvServices.DataSource = dtServices;
            gvServices.DataBind();
            }
            else
            {
                    Response.Redirect("suppliers.aspx");
            }

        }

    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        Session["VendorId"] = ViewState["VendorId"].ToString().Trim();
        Response.Redirect("supplieredit.aspx");
    }

    private DataTable CreateKeyPersonell(string strKeyPersonnel, string strKeyPosition)
    {

        string[] strKeyPersonnel1 = strKeyPersonnel.Split(Convert.ToChar("|"));
        string[] strKeyPosition1 = strKeyPosition.Split(Convert.ToChar("|"));
        DataTable dt = new DataTable();
        DataColumn dcol1 = new DataColumn("Name", typeof(System.String));
        dt.Columns.Add(dcol1);
        DataColumn dcol2 = new DataColumn("Position", typeof(System.String));
        dt.Columns.Add(dcol2);

        for (int i = 0; i < strKeyPersonnel1.Length; i++)
        {
            DataRow dr = dt.NewRow();
            dr["Name"] = ((strKeyPersonnel1[i].Trim() == "") ? "&nbsp;" : strKeyPersonnel1[i].Trim());
            dr["Position"] = ((strKeyPosition1[i].Trim() == "") ? "&nbsp;" : strKeyPosition1[i].Trim());
            dt.Rows.Add(dr);
        }

        return dt;
    }

    private DataTable CreateEmptyPresentServices()
    {
        DataTable dt = new DataTable();
        DataColumn dcol = new DataColumn("Plan", typeof(System.String));
        dt.Columns.Add(dcol);
        dcol = new DataColumn("AcctNo", typeof(System.String));
        dt.Columns.Add(dcol);
        dcol = new DataColumn("CreditLimit", typeof(System.String));
        dt.Columns.Add(dcol);

        DataRow dr = dt.NewRow();
        dr["Plan"] = "test";
        dr["AcctNo"] = "test";
        dr["CreditLimit"] = "test";
        dt.Rows.Add(dr);

        return dt;
    }



    private DataTable CreateEmptyMajorCustomers()
    {
        DataTable dt = new DataTable();
        DataColumn dcol = new DataColumn("Customer", typeof(System.String));
        dt.Columns.Add(dcol);
        dcol = new DataColumn("Sale", typeof(System.String));
        dt.Columns.Add(dcol);

        DataRow dr = dt.NewRow();
        dr["Customer"] = "&nbsp;";
        dr["Sale"] = "&nbsp;";
        dt.Rows.Add(dr);

        return dt;
    }

    private DataTable CreateEmptyBanks()
    {
        DataTable dt = new DataTable();
        DataColumn dcol = new DataColumn("Bank", typeof(System.String));
        dt.Columns.Add(dcol);
        dcol = new DataColumn("CreditLine", typeof(System.String));
        dt.Columns.Add(dcol);

        DataRow dr = dt.NewRow();
        dr["Bank"] = "&nbsp;";
        dr["CreditLine"] = "&nbsp;";
        dt.Rows.Add(dr);

        return dt;
    }

    private DataTable CreateEmptyAffiliatedCompanies()
    {
        DataTable dt = new DataTable();
        DataColumn dcol = new DataColumn("Company", typeof(System.String));
        dt.Columns.Add(dcol);
        dcol = new DataColumn("Business", typeof(System.String));
        dt.Columns.Add(dcol);

        DataRow dr = dt.NewRow();
        dr["Company"] = "&nbsp;";
        dr["Business"] = "&nbsp;";
        dt.Rows.Add(dr);

        return dt;
    }

    private DataTable CreateEmptyExternalAuditor()
    {
        DataTable dt = new DataTable();
        DataColumn dcol = new DataColumn("Auditor", typeof(System.String));
        dt.Columns.Add(dcol);
        dcol = new DataColumn("Counsel", typeof(System.String));
        dt.Columns.Add(dcol);

        DataRow dr = dt.NewRow();
        dr["Auditor"] = "&nbsp;";
        dr["Counsel"] = "&nbsp;";
        dt.Rows.Add(dr);

        return dt;
    }


    private DataTable CreateEmptyEquipment()
    {
        DataTable dt = new DataTable();
        DataColumn dcol = new DataColumn("Type", typeof(System.String));
        dt.Columns.Add(dcol);
        dcol = new DataColumn("Unit", typeof(System.String));
        dt.Columns.Add(dcol);
        dcol = new DataColumn("Remarks", typeof(System.String));
        dt.Columns.Add(dcol);

        DataRow dr = dt.NewRow();
        dr["Type"] = "&nbsp;";
        dr["Unit"] = "&nbsp;";
        dr["Remarks"] = "&nbsp;";
        dt.Rows.Add(dr);

        return dt;
    }


    private DataTable CreateEmptyRelativesTable()
    {
        DataTable dt = new DataTable();
        DataColumn dcol = new DataColumn("Name", typeof(System.String));
        dt.Columns.Add(dcol);
        dcol = new DataColumn("TitlePosition", typeof(System.String));
        dt.Columns.Add(dcol);
        dcol = new DataColumn("Relationship", typeof(System.String));
        dt.Columns.Add(dcol);

        DataRow dr = dt.NewRow();
        dr["Name"] = "&nbsp;";
        dr["TitlePosition"] = "&nbsp;";
        dr["Relationship"] = "&nbsp;";
        dt.Rows.Add(dr);

        return dt;
    }

}
