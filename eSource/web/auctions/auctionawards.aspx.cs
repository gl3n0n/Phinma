using System;
using System.Collections;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using EBid.lib.bid.trans;
using EBid.lib.auction.data;
using EBid.lib.auction.trans;
using EBid.lib.constant;
using EBid.lib;
using EBid.ConnectionString;


public partial class web_onlineAuction_AuctionAwards : System.Web.UI.Page
{
    private string connstring = "";

	protected void Page_Load(object sender, EventArgs e)
	{
        FormsAuthenticationHelper.AuthenticateUserWithReturnUrl();		
		connstring = Client.ConnectionString(HttpContext.Current.Session["clientid"].ToString());

		#region Check if online auction ref no is specified, if not, redirect back to finishedauctionevents.aspx
		if (Session[Constant.SESSION_AUCTIONREFNO] == null)
			Response.Redirect("finishedauctionevents.aspx");
		#endregion

        PageTitle.InnerText = String.Format(Constant.TITLEFORMAT, "Auction Awards");
		trHistory.Visible = !String.IsNullOrEmpty(Request.QueryString["a"]);
        web_usercontrol_commentlist_auction commentlist = (web_usercontrol_commentlist_auction)FindControl("Commentlist_auction1");

		try
		{
			if (!String.IsNullOrEmpty(Request.QueryString["b"]))
				lblHistory.Text = "Bid History : " + EncryptionHelper.Decrypt(Request.QueryString["b"]);
		}
		catch
		{
			Response.Redirect("auctionawards.aspx");
		}

        //if (Session[Constant.SESSION_USERTYPE].Equals("2"))
        //{
        //    commentlist.Visible = false;
        //}

        if (Page.IsPostBack)
        {
            gvAuctionLastBids.DataBind();
        }
            
        if (Session[Constant.SESSION_AUCTIONREFNO] != null)
            {
                if (Int32.Parse(Session[Constant.SESSION_USERTYPE].ToString().Trim()) == (int)Constant.USERTYPE.BUYER)
                {
                    //if ((GetAuctionItemForConversion(Session[Constant.SESSION_AUCTIONREFNO].ToString()).Length > 0) && (GridView1.Rows.Count == 0))
                    //{
                    //    lnkEndorse.Visible = true;
                    //}
                    if ((GetAuctionStatus(Session[Constant.SESSION_AUCTIONREFNO].ToString().Trim()) == Constant.AUCTION_STATUS_CANCELLED.ToString()) && (GridView1.Rows.Count == 0))
                    {
                        lnkEndorse.Visible = true;
                    }
                }
            }
        
	}


	public string Format(string datepart, string timepart)
	{
		return datepart + " " + Convert.ToDateTime(timepart).ToString("HH:mm tt");
	}
	
	protected void lnkOk_Click(object sender, EventArgs e)
	{
		Response.Redirect("finishedauctionevents.aspx");
	}

	protected void gvAuctionLastBids_RowCreated(object sender, GridViewRowEventArgs e)
	{

		if (e.Row.RowType == DataControlRowType.Header)
			if (e.Row.Cells[0] != null)
				e.Row.Cells[0].Visible = false;

            if (e.Row.Cells[2] != null)
                e.Row.Cells[2].Visible = false;


		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			if (e.Row.Cells[0] != null)
				e.Row.Cells[0].Visible = false;

            if (e.Row.Cells[2] != null)
                e.Row.Cells[2].Visible = false;

            if (e.Row.Cells[1] != null)
            {
                e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Left;
                e.Row.Cells[1].Width = Unit.Pixel(280);     
            }
           
			for (int i = 1; i < e.Row.Cells.Count; i++)
			{
				if (i != 1)
				{
					e.Row.Cells[i].HorizontalAlign = HorizontalAlign.Center;					
				}
			}

            if (e.Row.Cells.Count < 4)
            {
                lnkEndorse.Visible = false;
            }
		}
	}

	protected void gvAuctionLastBids_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			LinkButton lbtn = new LinkButton();
			lbtn.CommandArgument = e.Row.Cells[0].Text + ":" + e.Row.Cells[1].Text;			
			lbtn.ForeColor = System.Drawing.Color.Navy;
			lbtn.Command += new CommandEventHandler(lblAction_Command);
			lbtn.Text = e.Row.Cells[2].Text;
			lbtn.Attributes.CssStyle.Add("padding-left", "3px");
			e.Row.Cells[1].Text = "";
			e.Row.Cells[1].Controls.Add(lbtn);
		}
	}

	protected void lblAction_Command(object sender, CommandEventArgs e)
	{		
		string[] args = e.CommandArgument.ToString().Split(new char[]{':'});				
		Response.Redirect("auctionawards.aspx?a=" + args[0] + "&b=" + HttpUtility.UrlEncode(EncryptionHelper.Encrypt(args[1])));//HttpUtility.UrlEncode(EncryptionHelper.Encrypt(e.CommandArgument.ToString())));		
	}

    private string GetAuctionItemForConversion(string auctionRefNo)
    {
        SqlParameter[] sqlParams = new SqlParameter[1];
        sqlParams[0] = new SqlParameter("@AuctionRefNo", SqlDbType.Int);
        sqlParams[0].Value = auctionRefNo;

        return SqlHelper.ExecuteScalar(connstring, CommandType.StoredProcedure, "sp_GetAuctionItemForConversion", sqlParams).ToString().Trim();
    }

    private string GetAuctionStatus(string auctionRefNo)
    {
        SqlParameter[] sqlParams = new SqlParameter[1];
        sqlParams[0] = new SqlParameter("@AuctionRefNo", SqlDbType.Int);
        sqlParams[0].Value = auctionRefNo;

        return SqlHelper.ExecuteScalar(connstring, CommandType.StoredProcedure, "sp_GetAuctionStatus", sqlParams).ToString().Trim();
    }

    protected void lnkEndorse_Click(object sender, EventArgs e)
    {
        if (Session[Constant.SESSION_AUCTIONREFNO] != null)
        {
            AuctionItemTransaction.UpdateAuctionStatusNoComment(connstring, Session[Constant.SESSION_AUCTIONREFNO].ToString(), Session[Constant.SESSION_USERID].ToString(),int.Parse(Constant.AUCTION_STATUS_FINISHED.ToString()));
            Response.Redirect("endorseauction.aspx");
        }
    }
}