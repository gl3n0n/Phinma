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
using EBid.lib.bid.trans;
using EBid.lib.constant;
using EBid.lib;

namespace EBid.web.vendor_screens
{	
	public partial class Confirmation : System.Web.UI.Page
	{
		protected void Page_Load(object sender, System.EventArgs e)
		{
            FormsAuthenticationHelper.AuthenticateUserWithReturnUrl();
            if (Int32.Parse(Session[Constant.SESSION_USERTYPE].ToString().Trim()) != (int)Constant.USERTYPE.VENDOR)
                Response.Redirect("../unauthorizedaccess.aspx");

            PageTitle.InnerText = String.Format(Constant.TITLEFORMAT, "Confirmation");

            if (!(Page.IsPostBack))
            {
                if (Session[Constant.SESSION_USERID] != null)
                {
                    lnkBidName.NavigateUrl = "BidDetailsSubmitted.aspx";
                    lnkBidName.Text = BidItemTransaction.GetBidName(Session[Constant.SESSION_BIDREFNO].ToString().Trim());
                }            
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


        protected void btnOK_Click1(object sender, EventArgs e)
        {
            Response.Redirect("index.aspx");
        }
}
}
