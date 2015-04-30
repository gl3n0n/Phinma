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

public partial class WEB_buyer_screens_AwardedSuppliers : System.Web.UI.Page
{
    private string connstring = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        FormsAuthenticationHelper.AuthenticateUserWithReturnUrl();
        connstring = Client.ConnectionString(HttpContext.Current.Session["clientid"].ToString());
        if (Int32.Parse(Session[Constant.SESSION_USERTYPE].ToString().Trim()) != (int)Constant.USERTYPE.BUYER)
            Response.Redirect("../unauthorizedaccess.aspx");

        if (!(Page.IsPostBack))
        {
            Session[Constant.SESSION_COMMENT_TYPE] = "0";
            if (Session["ORDERBY"] == null)
                Session["ORDERBY"] = "DESC";
			lblBidRefNo.Text = "Bid Item #" + Session[Constant.SESSION_BIDREFNO].ToString();
            Bind();
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
		Response.Redirect("awardedbiddetails.aspx?bidrefno=" + Session[Constant.SESSION_BIDREFNO].ToString());
    }

    private void Bind()
    {
        DataTable dt = CreateDataSource();
        if (dt != null)
        {
            DataView m_DataView = new DataView(dt);
			m_DataView.Sort = "[VendorName]" + " " + Session["ORDERBY"].ToString().Trim();
            gvBids.DataSource = m_DataView;
            gvBids.DataBind();
            lblBidCount.Text = m_DataView.Count.ToString().Trim();
        }
    }
    private DataTable CreateDataSource()
    {
        DataTable dtDrafts = BidItemTransaction.QueryAwardedSuppliers(connstring, Session[Constant.SESSION_USERID].ToString(), Session[Constant.SESSION_BIDREFNO].ToString(), Session["ORDERBY"].ToString());
        return dtDrafts;
    }

    protected void gvBids_RowCommand(object sender, GridViewCommandEventArgs e)
    {       
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
			m_DataView.Sort = "[VendorName]" + " " + Session["ORDERBY"].ToString().Trim();

            gvBids.DataSource = m_DataView;
            gvBids.PageIndex = e.NewPageIndex;
            gvBids.DataBind();
        }
    }
    
}
