using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using EBid.lib.bid.trans;
using EBid.lib.auction.trans;
using EBid.lib.user.trans;
using EBid.lib.constant;
using EBid.lib;

namespace EBid.WEB.purchasing_screens
{
    public partial class announcementdetails : System.Web.UI.Page
	{
		protected void Page_Load(object sender, System.EventArgs e)
		{
            FormsAuthenticationHelper.AuthenticateUserWithReturnUrl();
            if (Int32.Parse(Session[Constant.SESSION_USERTYPE].ToString().Trim()) != (int)Constant.USERTYPE.PURCHASING)
                Response.Redirect("../unauthorizedaccess.aspx");            

            if (!(Page.IsPostBack))
            {                
            }
            PageTitle.InnerText = String.Format(Constant.TITLEFORMAT, "Announcement Details");
		}	        
    }
}
