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
using EBid.ConnectionString;

public partial class web_purchasing_screens_AwardItem : System.Web.UI.Page
{
    private string connstring = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        FormsAuthenticationHelper.AuthenticateUserWithReturnUrl();
        connstring = Client.ConnectionString(HttpContext.Current.Session["clientid"].ToString());
        if (Int32.Parse(Session[Constant.SESSION_USERTYPE].ToString().Trim()) != (int)Constant.USERTYPE.PURCHASING)
            Response.Redirect("../unauthorizedaccess.aspx");

        if (!(Page.IsPostBack))
        {
            if (Session[Constant.SESSION_USERID] != null)
            {
                if (Session[Constant.SESSION_BIDREFNO] != null)
                {
                    ViewState[Constant.SESSION_BIDREFNO] = Session[Constant.SESSION_BIDREFNO].ToString().Trim();

                    lnkBidItem.Text = "Bid Item " + ViewState[Constant.SESSION_BIDREFNO].ToString().Trim();

                    PurchasingTransaction bid = new PurchasingTransaction();

                    DataTable dtAwardedItems = bid.QueryAwardedItemsbyBidRefNo(connstring, ViewState[Constant.SESSION_BIDREFNO].ToString().Trim());
                    DataView dvAwardedItems = new DataView(dtAwardedItems);

                    gvAwardedItems.DataSource = dvAwardedItems;
                    gvAwardedItems.DataBind();
                }
            }
            
        }
    }

    protected void lnkBidItem_Click(object sender, EventArgs e)
    {
        if (ViewState[Constant.SESSION_BIDREFNO] != null)
            Session[Constant.SESSION_BIDREFNO] = ViewState[Constant.SESSION_BIDREFNO].ToString().Trim();

        Response.Redirect("biddetailsgeneric.aspx");
    }
}
