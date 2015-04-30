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

public partial class web_purchasing_screens_bidConfirmation : System.Web.UI.Page
{
    private string connstring = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        FormsAuthenticationHelper.AuthenticateUserWithReturnUrl();
        connstring = Client.ConnectionString(HttpContext.Current.Session["clientid"].ToString());
        if (Int32.Parse(Session[Constant.SESSION_USERTYPE].ToString().Trim()) != (int)Constant.USERTYPE.PURCHASING)
            Response.Redirect("../unauthorizedaccess.aspx");

        if (CheckIfPosted())
            Response.Redirect("bids.aspx");

        if (!(Page.IsPostBack))
        {
            Session[Constant.SESSION_COMMENT_TYPE] = "0";

            if (Session[Constant.SESSION_USERID] != null)
            {
                if (Session[Constant.SESSION_BIDREFNO] != null)
                {
                    ViewState[Constant.SESSION_BIDREFNO] = Session[Constant.SESSION_BIDREFNO].ToString().Trim();
                    lnkBidItem.Text = "Bid Item " + ViewState[Constant.SESSION_BIDREFNO].ToString().Trim();

                    if (Session["STATUS"] != null)
                    {
                        ViewState["STATUS"] = Session["STATUS"].ToString().Trim();

                        switch (ViewState["STATUS"].ToString().Trim())
                        {
                            case "2":
                                //Rejected
                                lblTitle.Text = "Bid Event For Rejection Confirmation.";
                                lblMessage.Text = "Click 'Confirm' To Reject the following Bid Item.";
                                btnOK.Attributes.Add("OnClick", "CheckIfPosted(" + 0 + ")");
                                break;
                            case "3":
                                //Reedit
                                lblTitle.Text = "Bid Item For Editing Confirmation.";
                                lblMessage.Text = "Click 'Confirm' To tag the following Bid Item for Editing.";
                                btnOK.Attributes.Add("OnClick", "CheckIfPosted(" + 0 + ")");
                                break;
                            case "4":
                                //Approved
                                lblTitle.Text = "Bid Item For Approval Confirmation";
                                lblMessage.Text = "Click 'Confirm' To Approve the following Bid Item..";
                                btnOK.Attributes.Add("OnClick", "CheckIfPosted(" + 1 + ")");
                                //hdnIsPosted.Value = "1";
                                break;
                        }
                    }

                }
            }
            
        }
    }

    private bool CheckIfPosted()
    {
        bool isPosted = false;
        try
        {
            if ((hdnIsPosted.Value.Trim() != "") && (Int32.Parse(hdnIsPosted.Value.Trim()) == 1))
            {
                PurchasingTransaction purch = new PurchasingTransaction();
                // TODO : Fix mail
                //SMTPEmail mail = new SMTPEmail();

                string emailAdd = purch.QueryBuyerEmailAddViaBidRefNo(connstring, ViewState[Constant.SESSION_BIDREFNO].ToString().Trim(), false);

                if (ViewState["STATUS"] != null)
                {
                    BidItemTransaction.UpdateBidStatus(connstring, ViewState[Constant.SESSION_BIDREFNO].ToString().Trim(), ViewState["STATUS"].ToString().Trim());

                    switch (Int32.Parse(ViewState["STATUS"].ToString().Trim()))
                    {
                        case 2:
                            //mail.SendEmail(emailAdd, "purchasing@globetel.com.ph", "", "", "Bid Event Rejected", "Your Bid Event has been rejected please check comment for further reasons.", "");
                            break;
                        case 3:
                            //mail.SendEmail(emailAdd, "purchasing@globetel.com.ph", "", "", "Bid Event For Re-Edit", "Please Re-edit your Bid Event, please check comment for further reasons.", "");
                            break;
                        case 4:
                            //mail.SendEmail(emailAdd, "purchasing@globetel.com.ph", "", "", "Bid Event Approved", "Congratulations, your Bid Event has been approved!", "");
                            break;
                    }
                    Session.Remove("STATUS");
                    Session.Remove(Constant.SESSION_BIDREFNO);
                    isPosted = true;
                }
            }
        }
        catch
        {
            isPosted = false;
        }
        return isPosted;
    }

    protected void lnkBidItem_Click(object sender, EventArgs e)
    {
        Response.Redirect("biddetailsgeneric.aspx");
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Session.Remove("STATUS");
        Session.Remove(Constant.SESSION_BIDREFNO);
        Response.Redirect("bids.aspx");
    }

    protected void btnOK_Click(object sender, EventArgs e)
    {

    }
}
