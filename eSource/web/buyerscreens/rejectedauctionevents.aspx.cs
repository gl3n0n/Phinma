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


public partial class web_buyer_screens_rejectedAuctionEvents : System.Web.UI.Page
{
    private string connstring = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        connstring = Client.ConnectionString(HttpContext.Current.Session["clientid"].ToString());
        FormsAuthenticationHelper.AuthenticateUserWithReturnUrl();
        if (Int32.Parse(Session[Constant.SESSION_USERTYPE].ToString().Trim()) != (int)Constant.USERTYPE.BUYER)
            Response.Redirect("../unauthorizedaccess.aspx");

        if (!(Page.IsPostBack))
        {
            Session[Constant.SESSION_LASTPAGE] = Request.AppRelativeCurrentExecutionFilePath;
            PageTitle.InnerText = String.Format(Constant.TITLEFORMAT, "Rejected Auction Events");

            hdnUserId.Value = Session[Constant.SESSION_USERID].ToString().Trim();
            if (Session["ORDERBY"] == null)
                Session["ORDERBY"] = "DESC";
            Bind();            
        }
    }


    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("index.aspx");
    }

    protected void gvAuctions_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("Select"))
        {
            Session["AuctionRefNo"] = e.CommandArgument.ToString().Trim();
            Response.Redirect("awardedauctioneventdetails.aspx");
        }
    }

    protected void gvAuctions_Sorting(object sender, GridViewSortEventArgs e)
    {
        Session["ORDERBY"] = ((Session["ORDERBY"].ToString().Trim() == "DESC") ? "ASC" : "DESC");
        Bind();
    }

    protected void gvAuctions_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        DataTable dt = CreateDataSource();
        if (dt != null)
        {
            DataView m_DataView = new DataView(dt);
            m_DataView.Sort = "[DateRejected]" + " " + Session["ORDERBY"].ToString().Trim();

            gvAuctions.DataSource = m_DataView;
            gvAuctions.PageIndex = e.NewPageIndex;
            gvAuctions.DataBind();
        }
    }

    private void Bind()
    {
        DataTable dt = CreateDataSource();
        if (dt != null)
        {
            DataView m_DataView = new DataView(dt);
            m_DataView.Sort = "[DateRejected]" + " " + Session["ORDERBY"].ToString().Trim();
            gvAuctions.DataSource = m_DataView;
            gvAuctions.DataBind();            
        }
    }

    private DataTable CreateDataSource()
    {
        AuctionTransaction au = new AuctionTransaction();
        DataTable dtAuctions = au.GetRejectedAuctions(connstring, hdnUserId.Value.Trim(), Session["ORDERBY"].ToString().Trim());
        return dtAuctions;
    }
}
