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

public partial class web_purchasing_screens_BidTenderAwardConfirmation : System.Web.UI.Page
{
    private string connstring = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        FormsAuthenticationHelper.AuthenticateUserWithReturnUrl();
        connstring = Client.ConnectionString(HttpContext.Current.Session["clientid"].ToString());
        if (Int32.Parse(Session[Constant.SESSION_USERTYPE].ToString().Trim()) != (int)Constant.USERTYPE.PURCHASING)
            Response.Redirect("../unauthorizedaccess.aspx");

        if(!IsPostBack)
        {
            if (Session[Constant.SESSION_USERID] != null)
            {
                if (Session["BidTenderNos"] != null)
                {
                    ViewState["BidTenderNos"] = Session["BidTenderNos"].ToString().Trim();
                    ViewState[Constant.SESSION_BIDREFNO] = Session[Constant.SESSION_BIDREFNO].ToString().Trim();
                    ViewState[Constant.SESSION_USERTYPE] = Session[Constant.SESSION_USERTYPE].ToString().Trim();
                    ViewState[Constant.SESSION_USERID] = Session[Constant.SESSION_USERID].ToString().Trim();

                    InitializeHiddenFields();

                    string bidTenderNos = ViewState["BidTenderNos"].ToString().Trim();
                    string bidRefNo = ViewState[Constant.SESSION_BIDREFNO].ToString().Trim();

                    lblBidRefNo.Text = ViewState[Constant.SESSION_BIDREFNO].ToString().Trim();

                    DataTable dtEndorsed = PurchasingTransaction.QuerySelectedEndorsedItems(connstring, bidRefNo, bidTenderNos);
                    DataView dvEndorsed = new DataView(dtEndorsed);

                    gvEndorsedItems.DataSource = dvEndorsed;
                    gvEndorsedItems.DataBind();
                }
            }
            
        }
    }
    protected void Submit_Click(object sender, EventArgs e)
    {
        if (ViewState["BidTenderNos"] != null)
        {
            string bidTenderNos = ViewState["BidTenderNos"].ToString().Trim();

            PurchasingTransaction tender = new PurchasingTransaction();
            tender.UpdateBidTenderStatus(connstring, bidTenderNos, Constant.BID_TENDER_STATUS_AWARD.ToString().Trim());
            Response.Redirect("bidsforeval.aspx");
        }
    }

    private void InitializeHiddenFields()
    {
        HiddenField hdnTenderStat = (HiddenField)TendersCommentBox.FindControl("hdnTenderStat");
        hdnTenderStat.Value = Constant.BID_TENDER_STATUS_AWARD.ToString().Trim();
        HiddenField hdnBidRefNo = (HiddenField)TendersCommentBox.FindControl("hdnBidRefNo");
        hdnBidRefNo.Value = ViewState["BidRefNo"].ToString().Trim();
        HiddenField hdnUserType = (HiddenField)TendersCommentBox.FindControl("hdnUserType");
        hdnUserType.Value = ViewState[Constant.SESSION_USERTYPE].ToString().Trim();
        HiddenField hdnUserID = (HiddenField)TendersCommentBox.FindControl("hdnUserID");
        hdnUserID.Value = ViewState[Constant.SESSION_USERID].ToString().Trim();
    }
}
