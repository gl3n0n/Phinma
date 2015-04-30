using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using EBid.lib;
using EBid.lib.constant;
using EBid.lib.auction.data;
using EBid.lib.auction.trans;
using EBid.ConnectionString;

public partial class web_purchasingscreens_auctionEndorsementOptions : System.Web.UI.Page
{
    private string connstring = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        FormsAuthenticationHelper.AuthenticateUserWithReturnUrl();
		connstring = Client.ConnectionString(HttpContext.Current.Session["clientid"].ToString());
        if (Int32.Parse(Session[Constant.SESSION_USERTYPE].ToString().Trim()) != (int)Constant.USERTYPE.PURCHASING)
            Response.Redirect("../unauthorizedaccess.aspx");

        if (!IsPostBack)
        {
            Session[Constant.SESSION_LASTPAGE] = Request.AppRelativeCurrentExecutionFilePath;

            lblAuctionRefNo.Text = Session[Constant.SESSION_AUCTIONREFNO].ToString().Trim();
            ViewState[Constant.SESSION_USERID] = Session[Constant.SESSION_USERID].ToString().Trim();
        }

        Session[Constant.SESSION_COMMENT_TYPE] = "2";

        PageTitle.InnerText = String.Format(Constant.TITLEFORMAT, "Auction Endorsement Options");

    }

    private string GetEndorsementIds()
    {
        // StringBuilder object
        StringBuilder str = new StringBuilder();

        // Select the checkboxes from the GridView control
        for (int i = 0, j = 0; i < gvAuctionItems.Rows.Count; i++)
        {
            GridViewRow row = gvAuctionItems.Rows[i];
            bool isChecked = ((CheckBox)row.FindControl("cbSelected")).Checked;

            if (isChecked)
            {
                if (j > 0)
                    str.Append(",");
                str.Append(((HiddenField)row.FindControl("hdEndorsedID")).Value);
                j++;
            }
        }

        return str.ToString().Trim();
    }

    protected void Approve_Click(object sender, EventArgs e)
    {
        string auctionIds = GetEndorsementIds();

        if (auctionIds != null)
        {
            SqlParameter[] sqlParams = new SqlParameter[3];
            sqlParams[0] = new SqlParameter("@auctionId", SqlDbType.VarChar);
            sqlParams[1] = new SqlParameter("@status", SqlDbType.Int);
            sqlParams[2] = new SqlParameter("@userId", SqlDbType.Int);
            sqlParams[0].Value = auctionIds.Trim();
            sqlParams[1].Value = 1;
            sqlParams[2].Value = Int32.Parse(ViewState[Constant.SESSION_USERID].ToString().Trim());

            SqlHelper.ExecuteNonQuery(connstring, CommandType.StoredProcedure, "s3p_EBid_UpdateAuctionItemEndorsement", sqlParams);

            Response.Redirect("auctionitemsforawarding.aspx");
        }
    }

    protected void Disapprove_Click(object sender, EventArgs e)
    {
        string auctionIds = GetEndorsementIds();

        if (auctionIds != null)
        {
            SqlParameter[] sqlParams = new SqlParameter[3];
            sqlParams[0] = new SqlParameter("@auctionId", SqlDbType.VarChar);
            sqlParams[1] = new SqlParameter("@status", SqlDbType.Int);
            sqlParams[2] = new SqlParameter("@userId", SqlDbType.Int);
            sqlParams[0].Value = auctionIds;
            sqlParams[1].Value = 2;
            sqlParams[2].Value = Int32.Parse(ViewState[Constant.SESSION_USERID].ToString().Trim());

            SqlHelper.ExecuteNonQuery(connstring, CommandType.StoredProcedure, "s3p_EBid_UpdateAuctionItemEndorsement", sqlParams);

            Response.Redirect("auctionitemsforawarding.aspx");
        }
    }

    protected void lnkBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("auctionitemsforawarding.aspx");
    }
    protected void lnkFailed_Click(object sender, EventArgs e)
    {
        AuctionItemTransaction.UpdateAuctionStatusNoComment(connstring, Session[Constant.SESSION_AUCTIONREFNO].ToString().Trim(),
            Session[Constant.SESSION_USERID].ToString(),
                Constant.AUCTION_STATUS_CANCELLED);

        Response.Redirect("~/web/auctions/finishedauctionevents.aspx");
    }
}
