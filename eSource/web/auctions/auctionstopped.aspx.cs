using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using EBid.lib.bid.trans;
using EBid.lib.auction.data;
using EBid.lib.auction.trans;
using EBid.lib.constant;
using EBid.lib;
using EBid.ConnectionString;

public partial class web_onlineAuction_AuctionStopped : System.Web.UI.Page
{

    private static string connstring = "";

	protected void Page_Load(object sender, EventArgs e)
	{
        connstring = Client.ConnectionString(HttpContext.Current.Session["clientid"].ToString());

		FormsAuthenticationHelper.AuthenticateUserWithReturnUrl();

        PageTitle.InnerText = String.Format(Constant.TITLEFORMAT, "Auction Stopped");

        if (!IsPostBack)
        {
            if (Request.QueryString["arn"] != null)
            {
                GetComments();
            }
        }
	}	
	
	protected void lnkOk_Click(object sender, EventArgs e)
	{
		Response.Redirect("ongoingauctionevents.aspx");
	}

    private void GetComments()
    {
        SqlParameter[] sqlParams = new SqlParameter[1];
        sqlParams[0] = new SqlParameter("@AuctionRefNo", SqlDbType.Int);
        sqlParams[0].Value = Int32.Parse(Request.QueryString["arn"].ToString());

        DataTable commentsDataTable = SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "sp_GetAuctionLatestComment", sqlParams).Tables[0];

        if (commentsDataTable.Rows.Count > 0)
            {
                DataRow commentDataRow = commentsDataTable.Rows[0];
                lblDatePosted.Text = commentDataRow["DatePosted"].ToString().Trim();
                lblAuthor.Text = commentDataRow["Author"].ToString().Trim();
                lblcomment.Text = commentDataRow["Comment"].ToString().Trim();
            }
    
    }
}