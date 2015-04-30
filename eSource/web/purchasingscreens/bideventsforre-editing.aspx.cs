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
using EBid.lib.bid.trans;
using EBid.lib.user.trans;
using EBid.lib;

public partial class web_purchasing_screens_BidEventsforRe_Editing : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        FormsAuthenticationHelper.AuthenticateUserWithReturnUrl();
        if (Int32.Parse(Session[Constant.SESSION_USERTYPE].ToString().Trim()) != (int)Constant.USERTYPE.PURCHASING)
            Response.Redirect("../unauthorizedaccess.aspx");

        if (Session[Constant.SESSION_USERID] != null)
        {
        }

        PageTitle.InnerText = String.Format(Constant.TITLEFORMAT, "Bid Events For Re-Editing");
    }

    protected void gvBids_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("BidEvent"))
        {
            Session[Constant.SESSION_BIDREFNO] = e.CommandArgument;
            Response.Redirect("biddetails.aspx");
        }
    }
}
