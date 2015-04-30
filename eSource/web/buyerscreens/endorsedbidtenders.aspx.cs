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

public partial class WEB_buyer_screens_EndorsedBidTenders : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        FormsAuthenticationHelper.AuthenticateUserWithReturnUrl();
        if (Int32.Parse(Session[Constant.SESSION_USERTYPE].ToString().Trim()) != (int)Constant.USERTYPE.BUYER)
            Response.Redirect("../unauthorizedaccess.aspx");

        Session[Constant.SESSION_LASTPAGE] = Request.AppRelativeCurrentExecutionFilePath;
        PageTitle.InnerText = String.Format(Constant.TITLEFORMAT, "Endorsed Bid Tenders");
    }

    protected void gvBids_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "ViewBidEventDetails":
                {
                    string[] args = e.CommandArgument.ToString().Split(new char[] { '|' });
                    Session[Constant.SESSION_BIDTENDERNO] = args[0];
                    Session["TVendorId"] = args[1];
                    Session[Constant.SESSION_BIDREFNO] = args[2];
                    Session["ViewOption"] = "AsBuyer";
                    //Session[Constant.SESSION_BIDREFNO] = e.CommandArgument.ToString();
                    Response.Redirect("bideventdetails.aspx");
                } break;
            case "ViewBidTenderDetails":
                {
                    string[] args = e.CommandArgument.ToString().Split(new char[] { '|' });
                    Session[Constant.SESSION_BIDTENDERNO] = args[0];
                    Session["TVendorId"] = args[1];
                    Session[Constant.SESSION_BIDREFNO] = args[2];
                    Session["ViewOption"] = "AsVendor";
                    //Session[Constant.SESSION_BIDTENDERNO] = e.CommandArgument.ToString();
                    Response.Redirect("bidtenderdetails.aspx");
                } break;
        }
    }
}