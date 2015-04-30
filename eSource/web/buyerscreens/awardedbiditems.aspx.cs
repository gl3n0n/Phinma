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

public partial class WEB_buyer_screens_AwardedBidItems : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        FormsAuthenticationHelper.AuthenticateUserWithReturnUrl();
        if (Int32.Parse(Session[Constant.SESSION_USERTYPE].ToString().Trim()) != (int)Constant.USERTYPE.BUYER)
            Response.Redirect("../unauthorizedaccess.aspx");

        Session[Constant.SESSION_LASTPAGE] = Request.AppRelativeCurrentExecutionFilePath;
        PageTitle.InnerText = String.Format(Constant.TITLEFORMAT, "Awarded Bid Items");
    }
    

    protected void gvBids_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridViewRow gvr = ((Control)e.CommandSource).NamingContainer as GridViewRow;

        switch (e.CommandName)
        {
            case "ViewBidEventDetails":
                {
                    string[] args = e.CommandArgument.ToString().Split(new char[] { '|' });
                    Session[Constant.SESSION_BIDTENDERNO] = args[0];
                    Session["TVendorId"] = args[1];
                    Session[Constant.SESSION_BIDREFNO] = args[2];
                    Session[Constant.SESSION_BIDDETAILNO] = args[3];
                    Session["ViewOption"] = "AsBuyer";
                    Response.Redirect("bideventdetails.aspx");
                } break;
            case "ViewBidItemDetails":
                {
                    string[] args = e.CommandArgument.ToString().Split(new char[] { '|' });
                    Session[Constant.SESSION_BIDTENDERNO] = args[0];
                    Session["TVendorId"] = args[1];
                    Session[Constant.SESSION_BIDREFNO] = args[2];
                    Session[Constant.SESSION_BIDDETAILNO] = args[3];
                    //Session["ViewOption"] = "AsVendor";
                    Response.Redirect("awardedbiddetails.aspx");
                } break;
        }
    }
    protected void chkbuyeropts_SelectedIndexChange(object sender, EventArgs e)
    {
        setFilter();
    }

    private void setFilter()
    {
        string filter = string.Empty;

        for (int ind = 0; ind < chkbuyeropts.Items.Count; ind++)
        {
            if (chkbuyeropts.Items[ind].Selected == true)
            {
                switch (chkbuyeropts.Items[ind].Value)
                {
                    case "0"://Trans-Asia
                        {
                            if (string.IsNullOrEmpty(filter))
                                filter = "0";
                            else
                                filter += ",0";
                        }
                        break;
                    case "1"://
                        {
                            if (string.IsNullOrEmpty(filter))
                                filter = "1";
                            else
                                filter += ",1";
                        }
                        break;
                    case "2"://2
                        {
                            if (string.IsNullOrEmpty(filter))
                                filter = "2";
                            else
                                filter += ",2";
                        }
                        break;
                }
            }
        }

        if (string.IsNullOrEmpty(filter))
            filter = "-1";

        dsAwardedItems.FilterExpression = String.Format("CompanyId IN ({0})", filter); ;
        gvBids.DataBind();
    }
}   
