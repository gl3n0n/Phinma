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
using EBid.lib.bid.trans;
using EBid.lib.constant;
using EBid.lib;
using EBid.ConnectionString;

public partial class WEB_buyer_screens_withdrawnedbiditems : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        FormsAuthenticationHelper.AuthenticateUserWithReturnUrl();
        if (Int32.Parse(Session[Constant.SESSION_USERTYPE].ToString().Trim()) != (int)Constant.USERTYPE.BUYER)
            Response.Redirect("../unauthorizedaccess.aspx");

        Session[Constant.SESSION_LASTPAGE] = Request.AppRelativeCurrentExecutionFilePath;
        PageTitle.InnerText = String.Format(Constant.TITLEFORMAT, "Withdrawned Bid Items");
    }
    

    protected void gvBids_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridViewRow gvr = ((Control)e.CommandSource).NamingContainer as GridViewRow;

        switch (e.CommandName)
        {
            case "ViewBidEventDetails":
                {
                    Session[Constant.SESSION_BIDREFNO] = e.CommandArgument.ToString();
                    Response.Redirect("bideventdetails.aspx");
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
