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
using EBid.lib.bid.data;
using EBid.lib.user.trans;
using EBid.lib.constant;
using EBid.lib;
using EBid.ConnectionString;

public partial class web_buyer_screens_ApprovedBidItems : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        FormsAuthenticationHelper.AuthenticateUserWithReturnUrl();
        if (Int32.Parse(Session[Constant.SESSION_USERTYPE].ToString().Trim()) != (int)Constant.USERTYPE.BUYER)
            Response.Redirect("../unauthorizedaccess.aspx");

        Session[Constant.SESSION_LASTPAGE] = Request.AppRelativeCurrentExecutionFilePath;
        PageTitle.InnerText = String.Format(Constant.TITLEFORMAT, "Approved Bid Events");
    }

    protected void gvBids_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("Details"))
        {
            Session["ViewOption"] = "AsBuyer";
            Session[Constant.SESSION_BIDREFNO] = e.CommandArgument.ToString().Trim();
            Response.Redirect("approvedbiddetails.aspx");
        }
    }

    protected void chkShowEventsBeforeDeadline_CheckedChanged(object sender, EventArgs e)
    {
        if (chkShowEventsBeforeDeadline.Checked)
        {
            dsAproved.FilterExpression = "IsPastDeadline = 'NO'";
        }
        else
        {
            dsAproved.FilterExpression = "IsPastDeadline IN ('YES','NO')";
        }
        dsAproved.DataBind();
    }
}
