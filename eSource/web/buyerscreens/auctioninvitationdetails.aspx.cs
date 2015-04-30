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
using EBid.ConnectionString;

public partial class web_buyerscreens_auctioninvitationdetails : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        FormsAuthenticationHelper.AuthenticateUserWithReturnUrl();
        if (Int32.Parse(Session[Constant.SESSION_USERTYPE].ToString().Trim()) != (int)Constant.USERTYPE.BUYER)
            Response.Redirect("../unauthorizedaccess.aspx");

        if (!IsPostBack)
        {
            if (Session["IStatus"] == null)
                Response.Redirect("auctioninvitations.aspx");

            string header = "";
            switch (Session["IStatus"].ToString())
            {
                case "0": header = "Pending"; break;
                case "1": header = "Confirmed"; break;
                case "2": header = "Declined"; break;
            }
            header = String.Format("{0} Auction Event Invitations", header);
            lblHeader.Text = header;
            PageTitle.InnerText = String.Format(Constant.TITLEFORMAT, header);
        }
    }

    protected void lnkBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("auctioninvitations.aspx");
    }

    protected System.Drawing.Color GetColor(string comment)
    {
        if (comment.Trim().ToUpper() == "NO COMMENT")
            return System.Drawing.Color.Gray;
        else
            return System.Drawing.Color.Black;
    }
}
