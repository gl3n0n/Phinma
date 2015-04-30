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
using EBid.lib.user.trans;
using EBid.lib.constant;
using EBid.lib;
using EBid.ConnectionString;

public partial class web_purchasing_screens_RenegotiationConfirmation : System.Web.UI.Page
{
    private string connstring = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        FormsAuthenticationHelper.AuthenticateUserWithReturnUrl();
        connstring = Client.ConnectionString(HttpContext.Current.Session["clientid"].ToString());
        if (Int32.Parse(Session[Constant.SESSION_USERTYPE].ToString().Trim()) != (int)Constant.USERTYPE.PURCHASING)
            Response.Redirect("../unauthorizedaccess.aspx");

        CheckIfPosted();
        if(!IsPostBack)
        {
            if (Session[Constant.SESSION_USERID] != null)
            {
                if ((Session["BidTenderNos"] != null) && (Session["BidRefNo"] != null))
                {
                    ViewState["BidTenderNos"] = Session["BidTenderNos"].ToString().Trim();
                    ViewState[Constant.SESSION_BIDREFNO] = Session[Constant.SESSION_BIDREFNO].ToString().Trim();
                    ViewState[Constant.SESSION_USERTYPE] = Session[Constant.SESSION_USERTYPE].ToString().Trim();
                    ViewState[Constant.SESSION_USERID] = Session[Constant.SESSION_USERID].ToString().Trim();

                    string bidTenderNos = ViewState["BidTenderNos"].ToString().Trim();
                    string bidRefNo = ViewState[Constant.SESSION_BIDREFNO].ToString().Trim();

                    lblBidRefNo.Text = ViewState[Constant.SESSION_BIDREFNO].ToString().Trim();

                    DataTable dtEndorsed = PurchasingTransaction.QuerySelectedEndorsedItems(connstring, bidRefNo, bidTenderNos);
                    DataView dvEndorsed = new DataView(dtEndorsed);

                    gvEndorsedItems.DataSource = dvEndorsed;
                    gvEndorsedItems.DataBind();
                }

                Submit.Attributes.Add("OnClick", "CheckIfPosted()");
            }
            
        }
    }   

    private void CheckIfPosted()
    {
        string isPosted = hdnIsPosted.Value.Trim();
        if (isPosted == "1")
        {
            if (ViewState["BidTenderNos"] != null)
            {                
                string bidTenderNos = ViewState["BidTenderNos"].ToString().Trim();

                PurchasingTransaction tender = new PurchasingTransaction();
                
                tender.UpdateBidTenderStatus(connstring, bidTenderNos, Constant.BID_TENDER_STATUS_WAIT_FOR_RE_NEGOTIATE.ToString().Trim());

                Session.Remove(Constant.SESSION_BIDREFNO);
                Session.Remove("BidTenderNos");
                hdnIsPosted.Value = "0";
                Response.Redirect("bidsforeval.aspx");
            }
        }
    }
}
