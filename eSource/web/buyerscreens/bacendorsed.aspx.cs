using System;
using System.Web.UI.WebControls;
using EBid.lib;
using EBid.ConnectionString;
using EBid.lib.constant;

public partial class web_buyer_screens_bacendorsed : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        FormsAuthenticationHelper.AuthenticateUserWithReturnUrl();
        if (Int32.Parse(Session[Constant.SESSION_USERTYPE].ToString().Trim()) != (int)Constant.USERTYPE.BUYER)
            Response.Redirect("../unauthorizedaccess.aspx");

        Session[Constant.SESSION_LASTPAGE] = Request.AppRelativeCurrentExecutionFilePath;
        PageTitle.InnerText = String.Format(Constant.TITLEFORMAT, "Endorsed Bid Events");
    }

    protected void gvBids_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("Details"))
        {
            Session["ViewOption"] = "AsBuyer";
            string sArg = e.CommandArgument.ToString().Trim();
            char[] mySeparator = new char[] { ';' };
            string[] Arr = sArg.Split(mySeparator);
            Session["BuyerBidForBac"] = "";
            Session["BuyerBidForBac"] = Arr[0].ToString();
            Session["BuyerBacRefNo"] = Arr[1].ToString();
            //Session["detailsPage"] = "bidawardingchecklistendorsed.aspx";
            Session["pageDetails"] = "bidawardingchecklistendorsed.aspx";
            Server.Transfer("bidawardingchecklistendorsed.aspx");
            
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
