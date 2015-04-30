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

public partial class web_vendorscreens_withdrawnedbiditems : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        FormsAuthenticationHelper.AuthenticateUserWithReturnUrl();
        if (Int32.Parse(Session[Constant.SESSION_USERTYPE].ToString().Trim()) != (int)Constant.USERTYPE.VENDOR)
            Response.Redirect("../unauthorizedaccess.aspx");

        PageTitle.InnerText = String.Format(Constant.TITLEFORMAT, "Withdrawn Bid Items");

        Session[Constant.SESSION_LASTPAGE] = Request.AppRelativeCurrentExecutionFilePath;
    }

    protected void gvBids_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridViewRow gvr = ((Control)e.CommandSource).NamingContainer as GridViewRow;

        switch (e.CommandName)
        {
            case "ViewBidEventDetails":
                {
                    Session[Constant.SESSION_BIDREFNO] = e.CommandArgument.ToString();
                    Response.Redirect("biddetails.aspx");
                } break;
            case "ViewBidItemDetails":
                {
                    string[] args = e.CommandArgument.ToString().Split(new char[] { '|' });

                    Session[Constant.SESSION_BIDDETAILNO] = args[1];
                    Session[Constant.SESSION_BIDREFNO] = args[0];

                    Response.Redirect("biditemdetail.aspx");
                } break;
        }
    }
}
