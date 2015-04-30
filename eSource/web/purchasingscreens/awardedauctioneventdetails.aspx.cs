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
using EBid.lib.constant;
using EBid.lib.user.trans;
using EBid.lib.auction.trans;
using EBid.lib.bid.trans;
using EBid.lib;

public partial class web_purchasing_screens_AwardedAuctionEventDetails : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        FormsAuthenticationHelper.AuthenticateUserWithReturnUrl();
        if (Int32.Parse(Session[Constant.SESSION_USERTYPE].ToString().Trim()) != (int)Constant.USERTYPE.PURCHASING)
            Response.Redirect("../unauthorizedaccess.aspx");

        if (!IsPostBack)
        {
            Session.Add("PurchasingId", Int32.Parse(Session[Constant.SESSION_USERID].ToString().Trim()));
           
        }
        PageTitle.InnerText = String.Format(Constant.TITLEFORMAT, "Awarded Auctions");
    }
    protected void btnOK_Click(object sender, EventArgs e)
    {
        Response.Redirect(Session[Constant.SESSION_LASTPAGE].ToString());
    }
}
