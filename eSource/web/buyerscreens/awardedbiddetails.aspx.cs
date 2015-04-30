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

public partial class WEB_buyer_screens_AwardedBidDetails : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        FormsAuthenticationHelper.AuthenticateUserWithReturnUrl();
        if (Int32.Parse(Session[Constant.SESSION_USERTYPE].ToString().Trim()) != (int)Constant.USERTYPE.BUYER)
            Response.Redirect("../unauthorizedaccess.aspx");

        if (Session[Constant.SESSION_BIDDETAILNO] == null)
            Response.Redirect("awardedbiditems.aspx");

        PageTitle.InnerText = String.Format(Constant.TITLEFORMAT, "");

        if (!IsPostBack)
        {
            lnkExport.NavigateUrl = "javascript://";
            lnkExport.Attributes.Add("onclick", "window.open('../reports/awardeditem.aspx?bdn=' + " + Session[Constant.SESSION_BIDDETAILNO].ToString() + " , 'x', 'toolbar=no, menubar=no, width=600; height=800, top=80, left=80, resizable=no, scrollbars=no');");

            lnkComparison.NavigateUrl = "javascript://";
            lnkComparison.Attributes.Add("onclick", "window.open('../reports/bidtendercomparisons.aspx?bdn=' + " + Session[Constant.SESSION_BIDDETAILNO].ToString() + " , 'x', 'toolbar=no, menubar=no, width=800; height=600, top=80, left=80, resizable=yes, scrollbars=yes');");
        }
    }
       
    protected void lnkBack_Click(object sender, EventArgs e)
    {
        if (Session[Constant.SESSION_LASTPAGE] != null)
            Response.Redirect(Session[Constant.SESSION_LASTPAGE].ToString());
        else
            Response.Redirect("awardedbiditems.aspx");
    }
}
