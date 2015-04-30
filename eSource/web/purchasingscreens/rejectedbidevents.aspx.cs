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

public partial class web_purchasing_screens_RejectedBidEvents : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        FormsAuthenticationHelper.AuthenticateUserWithReturnUrl();
        if (Int32.Parse(Session[Constant.SESSION_USERTYPE].ToString().Trim()) != (int)Constant.USERTYPE.PURCHASING)
            Response.Redirect("../unauthorizedaccess.aspx");

        if (Session[Constant.SESSION_USERID] != null)
        {
            //if (Session["ORDERBY"] == null)
            //    Session["ORDERBY"] = "DESC";

            //DataTable dtRejectedBids = PurchasingTransaction.QueryRejectedBids(connstring, Session["ORDERBY"].ToString().Trim());

            //if (dtRejectedBids.Rows.Count > 0)
            //{
            //    DataView dvRejectedBids = new DataView(dtRejectedBids);

            //    gvRejectedBids.DataSource = dvRejectedBids;
            //    gvRejectedBids.DataBind();
            //}
            //else
            //{
            //    lblDataIsEmpty.Text = "There are no rejected bid events to display at the moment.";
            //    actions.Visible = false;
            //}
        }

        PageTitle.InnerText = String.Format(Constant.TITLEFORMAT, "Rejected Bid Events");
        
    }

    //protected void gridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
    //{
    //    DataTable dtRejectedBids = PurchasingTransaction.QueryRejectedBids(connstring, Session["ORDERBY"].ToString().Trim());
    //    DataView dvRejectedBids = new DataView(dtRejectedBids);

    //    gvRejectedBids.DataSource = dvRejectedBids;
    //    gvRejectedBids.PageIndex = e.NewPageIndex;
    //    gvRejectedBids.DataBind();
    //}

    //protected void gridView_Sorting(object sender, GridViewSortEventArgs e)
    //{
    //    if (Session["ORDERBY"] == null)
    //    {
    //        Session["ORDERBY"] = "DESC";
    //    }
    //    else
    //        if (Session["ORDERBY"].ToString().Trim() == "ASC")
    //            Session["ORDERBY"] = "DESC";
    //        else
    //            Session["ORDERBY"] = "ASC";

    //    Response.Redirect("RejectedBidEvents.aspx");
    //}

    protected void gvBids_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("bidRefNo"))
        {
            Session.Remove("ORDERBY");
            Session[Constant.SESSION_BIDREFNO] = e.CommandArgument;
            Session["BidEventType"] = "2";
            Response.Redirect("biddetails.aspx");
        }
    }
}
