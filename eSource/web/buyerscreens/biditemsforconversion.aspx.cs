using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using EBid.lib.bid.data;
using EBid.lib.bid.trans;
using EBid.lib.constant;
using EBid.lib;
using EBid.ConnectionString;
using EBid.ConnectionString;

public partial class web_buyerscreens_biditemsforconversion : System.Web.UI.Page
{
    private string connstring = "";
    protected void Page_Load(object sender, EventArgs e)
    {
            connstring = Client.ConnectionString(HttpContext.Current.Session["clientid"].ToString());
        if (!(IsPostBack))        
            Session[Constant.SESSION_LASTPAGE] = Request.AppRelativeCurrentExecutionFilePath;

        PageTitle.InnerText = String.Format(Constant.TITLEFORMAT, "Converted Bid Items");
    }

    protected void gvBidsForConversion_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridViewRow gvr = ((Control)e.CommandSource).NamingContainer as GridViewRow;

        if (e.CommandName.Equals("SelectBidItem"))
        {
            Session[Constant.SESSION_BIDREFNO] = gvBidsForConversion.DataKeys[gvr.RowIndex].Values[0].ToString();
            Session[Constant.SESSION_BIDDETAILNO] = e.CommandArgument.ToString().Trim();
            Response.Redirect("biditemdetail.aspx");
        }
        else if (e.CommandName.Equals("SelectBidEvent"))
        {
            Session[Constant.SESSION_BIDREFNO] = e.CommandArgument.ToString().Trim();
            Response.Redirect("bideventdetails.aspx");
        }
        else if (e.CommandName.Equals("ConvertItem"))
        {
            Session[Constant.SESSION_AUCTIONREFNO] = null;
            Session[Constant.SESSION_BIDDETAILNO] = e.CommandArgument.ToString().Trim();
            Session[Constant.SESSION_BIDREFNO] = QueryRefNoByBidDetailNo(e.CommandArgument.ToString().Trim());
            Response.Redirect("createauction.aspx");
        }
        else if (e.CommandName.Equals("EditAuctionDraft"))
        {
             Session[Constant.SESSION_AUCTIONREFNO] = e.CommandArgument.ToString().Trim();
             Response.Redirect("draftauctiondetails.aspx");
        }
    }

    protected bool IsVisible(string status)
    {
        return !(status == "Converted");
    }

    protected bool IsDrafted(string status, string istatus)
    {
        if (istatus == "Converted")
        {
            return false;
        }
        else if ((status == "True") && (istatus == "For Conversion"))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    protected bool IsForConversion(string status, string istatus)
    {
        if (istatus == "Converted")
        {
            return false;
        }
        else if ((status == "False") && (istatus == "For Conversion"))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public string QueryRefNoByBidDetailNo(string vDetailNo)
    {
        SqlParameter[] sqlparams = new SqlParameter[1];
        sqlparams[0] = new SqlParameter("@BidDetailNo", SqlDbType.Int);
        sqlparams[0].Value = vDetailNo;
        DataSet itemData = SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "sp_GetBidItemDetails", sqlparams);

        DataTable itemDataTable = itemData.Tables[0];
        DataRow itemRow = itemDataTable.Rows[0];

        return itemRow["BidRefNo"].ToString().Trim();
    }
}
