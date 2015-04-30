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
using EBid.lib;
using EBid.ConnectionString;
using EBid.lib.constant;
using EBid.ConnectionString;

public partial class web_buyer_screens_EndorsementSummary : System.Web.UI.Page
{
    private string connstring = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        FormsAuthenticationHelper.AuthenticateUserWithReturnUrl();
        connstring = Client.ConnectionString(HttpContext.Current.Session["clientid"].ToString());
        if (Int32.Parse(Session[Constant.SESSION_USERTYPE].ToString().Trim()) != (int)Constant.USERTYPE.BUYER)
            Response.Redirect("../unauthorizedaccess.aspx");

        if (!(IsPostBack))
        {
            string strBidTenderNos = Session["hdnBidTenderNos"].ToString().Trim();
            strBidTenderNos = strBidTenderNos.Replace(Convert.ToChar("|"), Convert.ToChar(","));

            DataTable dt = new DataTable();
            if (strBidTenderNos.ToString().Trim() != "")
            {
                BidTenderTransaction bt = new BidTenderTransaction();
                dt = bt.GetEndorsedBidTenders(connstring, strBidTenderNos);
            }

            gvBids.DataSource = dt;
            gvBids.DataBind();

            lblBidName.Text = BidItemTransaction.GetBidName(connstring, Session["BidRefNo"].ToString().Trim());
        }

    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        BidTenderTransaction bt = new BidTenderTransaction();
        string strBidTenderNos = Session["hdnBidTenderNos"].ToString().Trim();
        string[] strBidTenderNos1 = strBidTenderNos.Split(Convert.ToChar("|"));
        for (int i = 0; i < strBidTenderNos1.Length; i++)
        {
            bt.EndorseBidTender(connstring, strBidTenderNos1[i].ToString().Trim());
        }
        BidItemTransaction.UpdateBidStatusToEndorsed(connstring, Session["BidRefNo"].ToString().Trim());
        Response.Redirect("endorsementconfirmation.aspx");
    }
}
