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
using EBid.lib.constant;
using EBid.lib;
using EBid.ConnectionString;

public partial class WEB_buyer_screens_BidItemsForRenegotiation : System.Web.UI.Page
{
    private string connstring = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        connstring = Client.ConnectionString(HttpContext.Current.Session["clientid"].ToString());
        if (!(Page.IsPostBack))
        {
            if (Session["ORDERBY"] == null)
                Session["ORDERBY"] = "DESC";
            Bind();
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("index.aspx");
    }

    private void Bind()
    {
        DataTable dt = CreateDataSource();
        if (dt != null)
        {
            DataView m_DataView = new DataView(dt);
			m_DataView.Sort = "[DateRenegotiated]" + " " + Session["ORDERBY"].ToString().Trim();
            gvBids.DataSource = m_DataView;
            gvBids.DataBind();
            lblBidCount.Text = m_DataView.Count.ToString().Trim();
        }
    }
    private DataTable CreateDataSource()
    {
        DataTable dtDrafts = BidItemTransaction.QueryRenegotiateBidItemsSent(connstring, Session[Constant.SESSION_USERID].ToString());
        return dtDrafts;
    }

    protected void gvBids_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("Select"))
			Response.Redirect("sentbiditemsforrenegotiationdetails.aspx?bidrefno=" + e.CommandArgument.ToString().Trim());
    }

    protected void gvBids_Sorting(object sender, GridViewSortEventArgs e)
    {
        Session["ORDERBY"] = ((Session["ORDERBY"].ToString().Trim() == "DESC") ? "ASC" : "DESC");
        Bind();
    }

    protected void gvBids_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        DataTable dt = CreateDataSource();
        if (dt != null)
        {
            DataView m_DataView = new DataView(dt);
			m_DataView.Sort = "[DateRenegotiated]" + " " + Session["ORDERBY"].ToString().Trim();

            gvBids.DataSource = m_DataView;
            gvBids.PageIndex = e.NewPageIndex;
            gvBids.DataBind();
        }
    }
    
}