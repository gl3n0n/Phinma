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
using EBid.lib.auction.trans;
using EBid.lib.constant;
using EBid.lib;

public partial class web_purchasingscreens_bidinvitationdetails : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        FormsAuthenticationHelper.AuthenticateUserWithReturnUrl();
        if (Int32.Parse(Session[Constant.SESSION_USERTYPE].ToString().Trim()) != (int)Constant.USERTYPE.PURCHASING)
            Response.Redirect("../unauthorizedaccess.aspx");

        if (!IsPostBack)
        {
            if (Session["BIStatus"] == null)
                Response.Redirect("bidinvitations.aspx");

            string header = "";
            switch (Session["BIStatus"].ToString())
            {
                case "0": header = "Pending"; break;
                case "1": header = "Confirmed"; break;
                case "2": header = "Declined"; break;
            }
            header = String.Format("{0} Bid Event Invitations", header);
            lblHeader.Text = header;
            PageTitle.InnerText = String.Format(Constant.TITLEFORMAT, header);
        }
    }
    protected void lnkBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("bidinvitations.aspx");
    }
}
