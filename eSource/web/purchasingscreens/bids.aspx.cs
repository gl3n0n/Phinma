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
using EBid.lib.constant;
using EBid.lib.bid.trans;
using EBid.lib.user.trans;
using EBid.lib;

namespace EBid.WEB.purchasing_screens
{
	/// <summary>
	/// Summary description for bids.
	/// </summary>
	public partial class bids : System.Web.UI.Page
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
            FormsAuthenticationHelper.AuthenticateUserWithReturnUrl();
            if (Int32.Parse(Session[Constant.SESSION_USERTYPE].ToString().Trim()) != (int)Constant.USERTYPE.PURCHASING)
                Response.Redirect("../unauthorizedaccess.aspx");

			if (!(Page.IsPostBack))
			{
                if (Session[Constant.SESSION_USERID] != null)
                {

                }
                
    		}

            PageTitle.InnerText = String.Format(Constant.TITLEFORMAT, "Bid Events For Approval");
		}

        protected void  gvBids_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("bidEvent"))
            {
                Session[Constant.SESSION_BIDREFNO] = e.CommandArgument;
                Response.Redirect("biddetailssubmitted.aspx");
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
    }
}
