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
using EBid.lib.bid.data;
using EBid.lib.bid.trans;
using EBid.lib.user.trans;
using EBid.lib.user.data;
using EBid.lib;

public partial class web_purchasing_screens_BidDetails : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        FormsAuthenticationHelper.AuthenticateUserWithReturnUrl();
        if (Int32.Parse(Session[Constant.SESSION_USERTYPE].ToString().Trim()) != (int)Constant.USERTYPE.PURCHASING)
            Response.Redirect("../unauthorizedaccess.aspx");

        if (Request.QueryString["brn"] != null)
        {
            Session[Constant.SESSION_BIDREFNO] = Request.QueryString["brn"].ToString();
        }

        if (Session[Constant.SESSION_BIDREFNO] != null)
        {
            hdBidRefNo.Value = Session[Constant.SESSION_BIDREFNO].ToString().Trim();
        }
        if (Session["BidEventType"] != null)
        {
            hdBidEventType.Value = Session["BidEventType"].ToString().Trim();
        }

        if (Session[Constant.SESSION_LASTPAGE] != null)
        {
            if (Session[Constant.SESSION_LASTPAGE].ToString() == "~/web/purchasingscreens/bidsforeval.aspx")
            {
                lnkViewReport.Visible = true;
            }
            else
            {
                lnkViewReport.Visible = false;
            }
        }

        PageTitle.InnerText = String.Format(Constant.TITLEFORMAT, "Bid Event Details");

    }

    protected void lnkCancel_Click(object sender, EventArgs e)
    {
        if (Session[Constant.SESSION_LASTPAGE] != null)
            Response.Redirect(Session[Constant.SESSION_LASTPAGE].ToString());
        else
            Response.Redirect("bids.aspx");
    }

    protected void lnkViewReport_Click(object sender, EventArgs e)
    {
        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "SHOWWINDOW", "<script type='text/javascript'> window.open('../reports/eventtenderscomparison.aspx?brn=" + Session[Constant.SESSION_BIDREFNO].ToString().Trim() + "','r1', 'toolbar=no, menubar=no, width=800; height=600, top=80, left=80, resizable=yes , scrollbars=yes'); </script>");
    }
}
