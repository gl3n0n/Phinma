using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using EBid.lib.bid.data;
using EBid.lib.bid.trans;
using EBid.lib.constant;
using EBid.lib;

namespace EBid.web.vendor_screens
{
	public partial class AwardedBids : System.Web.UI.Page
	{
        protected void Page_Load(object sender, System.EventArgs e)
		{
            FormsAuthenticationHelper.AuthenticateUserWithReturnUrl();
            if (Int32.Parse(Session[Constant.SESSION_USERTYPE].ToString().Trim()) != (int)Constant.USERTYPE.VENDOR)
                Response.Redirect("../unauthorizedaccess.aspx");

            PageTitle.InnerText = String.Format(Constant.TITLEFORMAT, "Awarded Bid Items");

            Session[Constant.SESSION_LASTPAGE] = Request.AppRelativeCurrentExecutionFilePath;
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

        protected void gvBids_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            GridViewRow gvr = ((Control)e.CommandSource).NamingContainer as GridViewRow;

            switch (e.CommandName)
            {
                case "ViewBidEventDetails":
                    {
                        string[] args = e.CommandArgument.ToString().Split(new char[] { '|' });
                        Session[Constant.SESSION_BIDTENDERNO] = args[0];
                        Session["TVendorId"] = args[1];
                        Session[Constant.SESSION_BIDREFNO] = args[2];
                        Session[Constant.SESSION_BIDDETAILNO] = args[3];
                        Session["ViewOption"] = "AsBuyer";
                        //Session[Constant.SESSION_BIDREFNO] = e.CommandArgument.ToString();
                        Response.Redirect("biddetails.aspx");
                    } break;
                case "ViewBidItemDetails":
                    {
                        string[] args = e.CommandArgument.ToString().Split(new char[] { '|' });
                        Session[Constant.SESSION_BIDTENDERNO] = args[0];
                        Session["TVendorId"] = args[1];
                        Session[Constant.SESSION_BIDREFNO] = args[2];
                        Session[Constant.SESSION_BIDDETAILNO] = args[3];
                        //Session["ViewOption"] = "AsVendor";
                        //Session[Constant.SESSION_BIDTENDERNO] = gvBids.DataKeys[gvr.RowIndex].Values[0].ToString();
                        //Session[Constant.SESSION_BIDDETAILNO] = e.CommandArgument.ToString();
                        Response.Redirect("biditemdetails.aspx");
                    } break;
            }
        }
	}
}
