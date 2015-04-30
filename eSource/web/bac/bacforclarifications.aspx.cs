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

public partial class web_bac_bacforclarifications : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        FormsAuthenticationHelper.AuthenticateUserWithReturnUrl();
        if (Int32.Parse(Session[Constant.SESSION_USERTYPE].ToString().Trim()) != (int)Constant.USERTYPE.BIDAWARDINGCOMMITTEE)
            Response.Redirect("../unauthorizedaccess.aspx");

        Session[Constant.SESSION_LASTPAGE] = Request.AppRelativeCurrentExecutionFilePath;

        if (!(Page.IsPostBack))
        {
        }
        if (Session[Constant.SESSION_USERID] == null)
        {
            Session.Abandon();
            Session.Clear();
            FormsAuthentication.SignOut();

            string returnUrl = string.Empty;
            if (Request.RawUrl.Trim() != "")
                returnUrl = "?ReturnUrl=" + Request.RawUrl.Trim().Replace("~/", "");
            Response.Redirect(FormsAuthentication.LoginUrl + returnUrl);
        }


        PageTitle.InnerText = String.Format(Constant.TITLEFORMAT, "Endorsed Bid Events");
    }

    protected void gvBids_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("Details"))
        {
            Session["ViewOption"] = "AsCommittee";
            string sArg = e.CommandArgument.ToString().Trim();
            char[] mySeparator = new char[] { ';' };
            string[] Arr = sArg.Split(mySeparator);
            Session["BuyerBidForBac"] = "";
            Session["BuyerBidForBac"] = Arr[0].ToString();
            Session["BuyerBacRefNo"] = Arr[1].ToString();
            Session["BuyerBuyerId"] = Arr[2].ToString();
            Session["ClarifiedBy"] = Arr[3].ToString();
            Session["pageDetails"] = "bidawardingchecklistclarify.aspx";

            Server.Transfer("bidawardingchecklistclarify.aspx");
        }
    }
}

