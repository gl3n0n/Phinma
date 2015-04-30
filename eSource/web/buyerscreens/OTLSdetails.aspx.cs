using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using EBid.lib.bid.trans;
using EBid.lib.bid.data;
using EBid.lib.constant;
using EBid.lib;
using EBid.ConnectionString;

namespace EBid.WEB.buyer_screens
{	
	public partial class OTLSdetails : System.Web.UI.Page
	{	
    private string connstring = "";
		protected void Page_Load(object sender, System.EventArgs e)
		{
            FormsAuthenticationHelper.AuthenticateUserWithReturnUrl();
            connstring = Client.ConnectionString(HttpContext.Current.Session["clientid"].ToString());
            if (Int32.Parse(Session[Constant.SESSION_USERTYPE].ToString().Trim()) != (int)Constant.USERTYPE.BUYER)
                Response.Redirect("../unauthorizedaccess.aspx");

			if (!(Page.IsPostBack)) 
			{
                PageTitle.InnerText = String.Format(Constant.TITLEFORMAT, "OTLS Details");
                if (Request.QueryString["ots"] != null)
                    hdnOTS.Value = Request.QueryString["ots"].ToString().Trim();
                if (Request.QueryString["CategoryId"] != null)
                    hdnCategoryId.Value = Request.QueryString["CategoryId"].ToString().Trim();
                if (Request.QueryString["Suppliers"] != null)
                    hdnSuppliers.Value = Request.QueryString["Suppliers"].ToString().Trim();

                if (Session["AddedNewVendor"] != null)
                {
                    hdnOTS.Value = Session["OTS"].ToString().Trim();
                    hdnCategoryId.Value = Session["CategoryId"].ToString().Trim();
                    hdnSuppliers.Value = Session["Suppliers"].ToString().Trim();
                    Session["AddedNewVendor"] = null;
                }
                lnkOK.Attributes.Add("onclick", "Close();");
                ShowVendors();
			}

		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    

		}
		#endregion


        private void ShowVendors()
        {
            SupplierTransaction vendor = new SupplierTransaction();
            DataTable dtOneTimeSuppliers = vendor.GetOneTimeSuppliers(connstring, hdnCategoryId.Value.Trim());
            ddlVendorName.DataSource = dtOneTimeSuppliers;
            ddlVendorName.DataTextField = "VendorName";
            ddlVendorName.DataValueField = "VendorId";
            ddlVendorName.DataBind();
            ddlVendorName.Items.Insert(0, "");
        }

        protected void ddlVendorName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlVendorName.SelectedIndex > 0)
            {
                SupplierTransaction sup = new SupplierTransaction();
                Supplier supplier = new Supplier();
                supplier = sup.GetVendorDetailsByVendorId(connstring, ddlVendorName.SelectedItem.Value.Trim());
                txtAddress1.Text = ((supplier.VendorAddress.Trim() == "") ? "No Data Available" : supplier.VendorAddress.Trim());
                txtAddress2.Text = ((supplier.VendorAddress1.Trim() == "") ? "No Data Available" : supplier.VendorAddress1.Trim());
                txtContactPerson.Text = ((supplier.ContactPerson.Trim() == "") ? "No Data Available" : supplier.ContactPerson.Trim());
                txtTelephoneNumber.Text = ((supplier.TelephoneNumber.Trim() == "") ? "No Data Available" : supplier.TelephoneNumber.Trim());
                txtEmailAddress.Text = ((supplier.VendorEmail.Trim() == "") ? "No Data Available" : supplier.VendorEmail.Trim());
                lblMessage.Text = "";
                hdnVendorId.Value = ddlVendorName.SelectedItem.Value.Trim();
            }
            else
            {
                txtAddress1.Text = "";
                txtAddress2.Text = "";
                txtContactPerson.Text = "";
                txtTelephoneNumber.Text = "";
                txtEmailAddress.Text = "";
                lblMessage.Text = "";
                hdnVendorId.Value = "";
            }

        }
        protected void btnAddNewVendor_Click(object sender, EventArgs e)
        {
            Session["CategoryId"] = hdnCategoryId.Value.Trim();
            Session["OTS"] = hdnOTS.Value.Trim();
            Session["Suppliers"]=hdnSuppliers.Value.Trim();
            Response.Redirect("otlsdetails_add.aspx");
        }
        
}
}
