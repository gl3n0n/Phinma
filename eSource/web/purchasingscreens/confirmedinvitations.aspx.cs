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
using EBid.lib.user.trans;
using EBid.lib.auction.trans;
using EBid.lib;

public partial class web_purchasing_screens_ConfirmedInvitations : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        FormsAuthenticationHelper.AuthenticateUserWithReturnUrl();
        if (Int32.Parse(Session[Constant.SESSION_USERTYPE].ToString().Trim()) != (int)Constant.USERTYPE.PURCHASING)
            Response.Redirect("../unauthorizedaccess.aspx");

        if (!(Page.IsPostBack))
        {
            if (Session[Constant.SESSION_USERID] != null)
            {
                if (Session["AuctionRefNo"] != null)
                {
                    //if (Session["ORDERBY"] == null)
                    //    Session["ORDERBY"] = "DESC";

                    //AuctionItemtransaction aucts = new AuctionItemtransaction();
                    //DataTable dtConfirmedAuctions = aucts.QueryConfirmedVendors(Session["ORDERBY"].ToString().Trim(), Session["AuctionRefNo"].ToString().Trim());

                    //DataView dvConfirmedAuctions = new DataView(dtConfirmedAuctions);

                    //gvConfirmedInvites.DataSource = dvConfirmedAuctions;
                    //gvConfirmedInvites.DataBind();

                }
            }
            
        }
        PageTitle.InnerText = String.Format(Constant.TITLEFORMAT, "Confirmed Invitations");
    }

    protected void gvBids_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("vendorId"))
        {
            Session["VendorID"] = e.CommandArgument;
            Response.Redirect("supplierdetails.aspx");
        }
    }
}
