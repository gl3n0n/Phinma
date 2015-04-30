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
using EBid.lib;

public partial class web_purchasing_screens_Auctions : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        FormsAuthenticationHelper.AuthenticateUserWithReturnUrl();
        if (Int32.Parse(Session[Constant.SESSION_USERTYPE].ToString().Trim()) != (int)Constant.USERTYPE.PURCHASING)
            Response.Redirect("../unauthorizedaccess.aspx");

        if (!(Page.IsPostBack))
            Session[Constant.SESSION_LASTPAGE] = Request.AppRelativeCurrentExecutionFilePath;


        PageTitle.InnerText = String.Format(Constant.TITLEFORMAT, "Auctions Events For Approval");
    }

    protected void gvBids_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("auctionEvent"))
        {
           Session[Constant.SESSION_AUCTIONREFNO] = e.CommandArgument;
           Response.Redirect("auctiondetailssubmitted.aspx");
        }
    }
}
