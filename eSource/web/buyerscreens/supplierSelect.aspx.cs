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
using EBid.lib;
using EBid.ConnectionString;
using EBid.lib.constant;

namespace EBid.WEB.buyer_screens
{
	/// <summary>
	/// Summary description for supplierSelect.
	/// </summary>
	public partial class supplierSelect : System.Web.UI.Page
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
                btnClick.Attributes.Add("onClick", "ShowOTLSDetails();");
                if (Session["BidRefNo"] != null)
                    hdnBidRefNo.Value = Session["BidRefNo"].ToString().Trim();
                
                if (Session["CategoryId"] != null)
                    hdnCategoryId.Value = Session["CategoryId"].ToString().Trim();
                else
                    if (Request.QueryString["catid"] != null)
                        hdnCategoryId.Value = Request.QueryString["catid"].ToString().Trim();

                GetAllVendorsForAParticularCategory();
                
                if (Session["Suppliers"] != null)
                    SeparateSuppliers();

                if (Request.QueryString["Suppliers"] != null)
                    hdnSuppliers.Value = Request.QueryString["Suppliers"].ToString().Trim();

                if (Request.QueryString["ots"] != null)
                    hdnOTS.Value = Request.QueryString["ots"].ToString().Trim();

                DisplayAvailableAndSelectedSuppliers();
			}
		}

        private void SeparateSuppliers()
        {
            string accvendors = hdnVendorsForACategoryId.Value.Trim();
            string[] AccVendors = accvendors.Split(Convert.ToChar(","));
            string[] suppliers = Session["Suppliers"].ToString().Trim().Split(Convert.ToChar(","));

            foreach (string str in suppliers)
            {
                bool accreditedvendor = false;
                foreach (string strAccVendor in AccVendors)
                {
                    if (strAccVendor == str)
                    {
                        accreditedvendor = accreditedvendor || true;
                        break;
                    }
                }
                if (!accreditedvendor)
                    hdnOTS.Value = ((hdnOTS.Value.Trim() == "") ? str.Trim() : hdnOTS.Value.Trim() + "," + str.Trim());
                else
                    hdnSuppliers.Value = ((hdnSuppliers.Value.Trim() == "") ? str.Trim() : hdnSuppliers.Value.Trim() + "," + str.Trim());
            }
        }

        private void GetAllVendorsForAParticularCategory()
        {
            if (hdnCategoryId.Value.Trim() != "")
            {
                SupplierTransaction sup = new SupplierTransaction();
                hdnVendorsForACategoryId.Value = sup.GetSuppliersByCategoryId(connstring, hdnCategoryId.Value.Trim(), "");
            }
        }

        private void DisplayAvailableAndSelectedSuppliers()
        {
            if (hdnCategoryId.Value.Trim() != "")
            {
                CategoryTransaction cat = new CategoryTransaction();
                lblSupplierCategory.Text = cat.GetCategoryNameById(connstring, hdnCategoryId.Value.Trim());

                string allSelectedVendors = "";
                if (hdnSuppliers.Value.Trim() == "")
                    if (hdnOTS.Value.Trim() == "")
                        allSelectedVendors = "";
                    else
                        allSelectedVendors = hdnOTS.Value.Trim();
                else
                    if (hdnOTS.Value.Trim() == "")
                        allSelectedVendors = hdnSuppliers.Value.Trim();
                    else
                        allSelectedVendors = hdnSuppliers.Value.Trim() +  "," + hdnOTS.Value.Trim();

                SupplierTransaction sup = new SupplierTransaction();
                DataSet ds = sup.GetSuppliersByCategoryId_FilteredList(connstring, hdnCategoryId.Value.Trim(), "", allSelectedVendors);
                DataTable dtAvailable = new DataTable();
                DataTable dtSelected = new DataTable();
                if (ds.Tables.Count > 0)
                {
                    try
                    {
                        if (ds.Tables[0] != null)
                            dtAvailable = ds.Tables[0];
                        if (ds.Tables.Count > 1)
                            if (ds.Tables[1] != null)
                                dtSelected = ds.Tables[1];
                    }
                    catch
                    {
                    }
                }
                lstSupplierA.DataSource = dtAvailable;
                lstSupplierA.DataTextField = "VendorName";
                lstSupplierA.DataValueField = "VendorId";
                lstSupplierA.DataBind();

                lstSupplierB.DataSource = dtSelected;
                lstSupplierB.DataTextField = "VendorName";
                lstSupplierB.DataValueField = "VendorId";
                lstSupplierB.DataBind();
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



        private string JoinSuppliers()
        {
            string supplierlist = "";
            if ((hdnSuppliers.Value.Trim() != "") && (hdnOTS.Value.Trim() != ""))
                supplierlist = hdnSuppliers.Value.Trim() + "," + hdnOTS.Value.Trim();
            else
            {
                if (hdnSuppliers.Value.Trim() == "")
                    supplierlist = hdnOTS.Value.Trim();
                if (hdnOTS.Value.Trim() == "")
                    supplierlist = hdnSuppliers.Value.Trim();
            }
            return supplierlist;
        }
        protected void btnOk_Click(object sender, EventArgs e)
        {
            
            Session["BidRefNo"] = hdnBidRefNo.Value.ToString().Trim();
            Session["Suppliers"] = JoinSuppliers();
            Session["FirstLoad"] = "false";
            Response.Redirect("createnewitem.aspx");
        }








     
}
}
