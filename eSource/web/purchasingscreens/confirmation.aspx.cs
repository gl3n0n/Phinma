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

public partial class web_purchasing_screens_bidConfirmation : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        FormsAuthenticationHelper.AuthenticateUserWithReturnUrl();
        if (Int32.Parse(Session[Constant.SESSION_USERTYPE].ToString().Trim()) != (int)Constant.USERTYPE.PURCHASING)
            Response.Redirect("../unauthorizedaccess.aspx");

        if (Session["STATUS"] == null)
            Response.Redirect("bids.aspx");

        if (!IsPostBack)
        {
            int stat = 0;
            if (!String.IsNullOrEmpty(Session["Status"].ToString()))
                stat = int.Parse(Session["STATUS"].ToString());

            switch (stat)
            {
                case 2:
                    lblTitle.Text = "Rejected Bid Event";
                    lblMessage.Text = "Bid event was successfully rejected.";
                    Session["NextUrl"] = "rejectedbidevents.aspx";
                    break;
                case 3:
                    lblTitle.Text = "Bid Event For Re-Edit";
                    lblMessage.Text = "Bid event was successfully sent back to buyer for re-editing.";
                    Session["NextUrl"] = "bideventsforre-editing.aspx";
                    break;                
                case 4:
                    lblTitle.Text = "Approved Bid Event";
                    lblMessage.Text = "Bid event was successfully approved.";
                    Session["NextUrl"] = "approvedbidevents.aspx";
                    break;
            }
        }
        PageTitle.InnerText = String.Format(Constant.TITLEFORMAT, "Confirmation");
    }

    protected void lnkOK_Click(object sender, EventArgs e)
    {
        Response.Redirect(Session["NextUrl"].ToString());
    }
}
