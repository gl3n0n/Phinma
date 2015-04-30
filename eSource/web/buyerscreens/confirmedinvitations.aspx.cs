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
using EBid.lib.auction.trans;
using EBid.lib;
using EBid.ConnectionString;

public partial class web_buyerscreens_ConfirmedInvitations : System.Web.UI.Page
{
    private string connstring = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        FormsAuthenticationHelper.AuthenticateUserWithReturnUrl();
        connstring = Client.ConnectionString(HttpContext.Current.Session["clientid"].ToString());
        if (Int32.Parse(Session[Constant.SESSION_USERTYPE].ToString().Trim()) != (int)Constant.USERTYPE.BUYER)
            Response.Redirect("../unauthorizedaccess.aspx");

        if (Session[Constant.SESSION_AUCTIONREFNO] == null)
            Response.Redirect("confirmedauctioninvitations.aspx");

        if (!(IsPostBack))
        {
            Session[Constant.SESSION_COMMENT_TYPE] = "1";
            if (Session[Constant.SESSION_USERID] != null)
            {
                if (Session["ORDERBY"] == null)
                    Session["ORDERBY"] = "DESC";
                else
                    if (Session["ORDERBY"].ToString().Trim() == "")
                        Session["ORDERBY"] = "DESC";
                if (Session["AuctionRefNo"] != null)
                    hdnAuctionRefNo.Value = Session["AuctionRefNo"].ToString().Trim();
                Bind();
            }
            else
            {
                Response.Redirect(FormsAuthentication.LoginUrl);
            }
        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("confirmedauctioninvitations.aspx");
    }

    private void Bind()
    {
        DataTable dt = CreateDataSource();
        if (dt != null)
        {
            DataView m_DataView = new DataView(dt);
            m_DataView.Sort = "[DateReplied]" + " " + Session["ORDERBY"].ToString().Trim();
            gvConfirmedInvitations.DataSource = m_DataView;
            gvConfirmedInvitations.DataBind();
        }
    }

    private DataTable CreateDataSource()
    {
        AuctionTransaction a = new AuctionTransaction();
        DataTable dt = a.QueryVendorsThatConfirmed(connstring, hdnAuctionRefNo.Value.Trim());
        return dt;
    }

    protected void gvConfirmedInvitations_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName.Trim())
        {
            case "Select":
                Session["VendorId"] = e.CommandArgument.ToString().Trim();
                Response.Redirect("supplierdetails.aspx");
                break;
        }
    }

    protected void gvConfirmedInvitations_Sorting(object sender, GridViewSortEventArgs e)
    {
        Session["ORDERBY"] = ((Session["ORDERBY"].ToString().Trim() == "DESC") ? "ASC" : "DESC");
        Bind();
    }

    protected void gvConfirmedInvitations_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        DataTable dt = CreateDataSource();
        if (dt != null)
        {
            DataView m_DataView = new DataView(dt);
            m_DataView.Sort = "[DateReplied]" + " " + Session["ORDERBY"].ToString().Trim();

            gvConfirmedInvitations.DataSource = m_DataView;
            gvConfirmedInvitations.PageIndex = e.NewPageIndex;
            gvConfirmedInvitations.DataBind();
        }
    }
}
