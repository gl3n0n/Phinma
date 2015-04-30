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

public partial class web_vendor_screens_Decline : System.Web.UI.Page
{
    private string connstring = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        FormsAuthenticationHelper.AuthenticateUserWithReturnUrl();
        connstring = Client.ConnectionString(HttpContext.Current.Session["clientid"].ToString());
        if (Int32.Parse(Session[Constant.SESSION_USERTYPE].ToString().Trim()) != (int)Constant.USERTYPE.VENDOR)
            Response.Redirect("../unauthorizedaccess.aspx");

        PageTitle.InnerText = String.Format(Constant.TITLEFORMAT, "Decline Bid Event");

        if (!(IsPostBack))
        {            
            if (Session[Constant.SESSION_BIDREFNO] != null)
                ViewState[Constant.SESSION_BIDREFNO] = Session[Constant.SESSION_BIDREFNO].ToString().Trim();

            lnkBidItemName.Text = BidItemTransaction.GetBidName(connstring, ViewState["BidRefNo"].ToString().Trim());
        }
    }
    protected void btnOK_Click(object sender, EventArgs e)
    {
        Response.Redirect("declinedbidevents.aspx");
    }

    protected void lnkBidItemName_Click(object sender, EventArgs e)
    {
        Session[Constant.SESSION_BIDREFNO] = ViewState[Constant.SESSION_BIDREFNO].ToString().Trim();
        Response.Redirect("biddetails.aspx");
    }
}
