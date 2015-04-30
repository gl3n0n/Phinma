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
using EBid.lib.auction.trans;
using EBid.lib.user.trans;

public partial class web_purchasingscreens_auctionItemsForAwarding : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        FormsAuthenticationHelper.AuthenticateUserWithReturnUrl();
        if (Int32.Parse(Session[Constant.SESSION_USERTYPE].ToString().Trim()) != (int)Constant.USERTYPE.PURCHASING)
            Response.Redirect("../unauthorizedaccess.aspx");

        if (Session[Constant.SESSION_USERID] == null)
            Session[Constant.SESSION_LASTPAGE] = Request.AppRelativeCurrentExecutionFilePath;

        PageTitle.InnerText = String.Format(Constant.TITLEFORMAT, "Endorsed Auction Items");
    }

    protected void gvAuctions_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("AuctionEvent"))
        {
            Session[Constant.SESSION_AUCTIONREFNO] = e.CommandArgument;
            Response.Redirect("auctionendorsementoptions.aspx");
        }
     
    }
}
