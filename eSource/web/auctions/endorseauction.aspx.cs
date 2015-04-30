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
using EBid.lib.bid.trans;
using EBid.lib.auction.data;
using EBid.lib.auction.trans;
using EBid.lib.constant;
using System.Data.SqlClient;
using System.Data.Sql;
using System.Text;
using EBid.lib;
using EBid.ConnectionString;

public partial class web_onlineAuction_EndorseAuction : System.Web.UI.Page
{
	private ArrayList _headerlist = new ArrayList();
	private string connstring = "";

	protected void Page_Load(object sender, EventArgs e)
	{
                connstring = Client.ConnectionString(HttpContext.Current.Session["clientid"].ToString());
		FormsAuthenticationHelper.AuthenticateUserWithReturnUrl();

        PageTitle.InnerText = String.Format(Constant.TITLEFORMAT, "Auction Endorsement");

		#region Check if online auction ref no is specified, if not, redirect back to finishedauctionevents.aspx
		if (Session[Constant.SESSION_AUCTIONREFNO] == null)
			Response.Redirect("finishedauctionevents.aspx");
		#endregion

		// check if user is buyer, if not, redirect to unauthorized access page..
		if (Session[Constant.SESSION_USERTYPE].ToString() != ((int)Constant.USERTYPE.BUYER).ToString())
			Response.Redirect("~/web/unauthorizedaccess.aspx");

		if (Session[Constant.SESSION_AUCTIONREFNO] == null)
			Response.Redirect("finishedauctionevents.aspx");


		if (Page.IsPostBack)
			gvAuctionLastBids.DataBind();

		if (Session["Message"] != null)
		{
			lblMessage.Text = Session["Message"].ToString();
			Session["Message"] = null;
		}

        Session[Constant.SESSION_COMMENT_TYPE] = "1";

	}

	public string Format(string datepart, string timepart)
	{
		return datepart + " " + Convert.ToDateTime(timepart).ToString("HH:mm tt");
	}
		
	protected void gvAuctionLastBids_RowCreated(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.Header)
		{
			if (e.Row.Cells[0] != null)
				e.Row.Cells[0].Visible = false;
			if (e.Row.Cells[1] != null)
				e.Row.Cells[1].Visible = false;
			if (e.Row.Cells[2] != null)
				e.Row.Cells[2].Visible = false;
		}



		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			if (e.Row.Cells[0] != null)
				e.Row.Cells[0].Visible = false;
			if (e.Row.Cells[1] != null)
				e.Row.Cells[1].Visible = false;
			if (e.Row.Cells[2] != null)
				e.Row.Cells[2].Visible = false;

			if (e.Row.Cells[3] != null)
				e.Row.Cells[3].Width = Unit.Pixel(180);

			for (int i = 4; i < e.Row.Cells.Count; i++)
			{				
				e.Row.Cells[i].BackColor = System.Drawing.Color.White;					
				if (i != e.Row.Cells.Count - 1)
				{
					e.Row.Cells[i].Width = Unit.Pixel(120);						
				}								
			}			
		}		
	}
		
	protected void gvAuctionLastBids_RowDataBound(object sender, GridViewRowEventArgs e)
	{		
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			e.Row.Cells[3].Text += HttpUtility.HtmlDecode("<label style='font-size: 9px; color: red;'>" + e.Row.Cells[2].Text + "</label>");

			for (int i = 4; i < e.Row.Cells.Count; i++)
			{				
				if ((e.Row.Cells[i].Text.Trim() == "0.00") || (e.Row.Cells[i].Text.Trim() == "&nbsp;"))
					e.Row.Cells[i].Text = "[No Bid For This Item]";
				else
				{
					RadioButton rbtn = new RadioButton();
					rbtn.ID = "rbtn" + e.Row.RowIndex + "" + i;
					if (e.Row.Cells[i].Text.Trim() == e.Row.Cells[1].Text.Trim())
						rbtn.Checked = true;
					else
						rbtn.Checked = false;
					rbtn.GroupName = "row" + e.Row.RowIndex;
					rbtn.Text = " " + e.Row.Cells[i].Text;
					e.Row.Cells[i].Controls.Add(rbtn);
				}				
			}
		}
	}

	protected void lnkCancel_Click(object sender, EventArgs e)
	{
		Response.Redirect("finishedauctionevents.aspx");
	}

	protected void lnkReset_Click(object sender, EventArgs e)
	{
		Response.Redirect("endorseauction.aspx");
	}

	protected void lnkEndorse_Click(object sender, EventArgs e)
	{		
		string list = GetInputs();

		if (list.Trim() != "")
		{
			if (EndorseAuctionResults(int.Parse(Session[Constant.SESSION_USERID].ToString()), list))
			{	//successful
				Session["Message"] = "Auction results were successfully endorsed.";
				Response.Redirect("finishedauctionevents.aspx");
			}
			else
			{	// failed
				Session["Message"] = "Auction results were unsuccessfully endorsed.";
				Response.Redirect("endorseauction.aspx");
			}
		}
		else
		{	// no selected radiobutton
			Session["Message"] = "Please select atleast one auction item to endorse.";
			Response.Redirect("endorseauction.aspx");
		}
		
	}

	private bool EndorseAuctionResults(int buyerid, string auctiontrailids)
	{
		SqlConnection sqlConnect = new SqlConnection(connstring);
		SqlTransaction sqlTransact = null;
		bool success = false;

		try
		{
			sqlConnect.Open();
			sqlTransact = sqlConnect.BeginTransaction();

			SqlParameter[] sqlParams = new SqlParameter[2];			
			sqlParams[0] = new SqlParameter("@BuyerId", SqlDbType.Int);
			sqlParams[1] = new SqlParameter("@AuctionTrailIds", SqlDbType.NVarChar);			
			sqlParams[0].Value = buyerid;
			sqlParams[1].Value = auctiontrailids;

			SqlHelper.ExecuteNonQuery(sqlTransact, "sp_AddAuctionEndorsement", sqlParams);

			sqlTransact.Commit();

			success = true;
		}
		catch
		{
			sqlTransact.Rollback();
			success = false;
		}
		finally
		{
			sqlConnect.Close();
		}
		return success;
	}

	private RadioButton GetRadioButton(Control parent)
	{
		RadioButton r = null;

		foreach (Control c in parent.Controls)
		{
			if (c.GetType().ToString().Equals("System.Web.UI.WebControls.RadioButton"))
			{
				r = (RadioButton)c;
			}

			if (c.Controls.Count > 0)
			{
				r = GetRadioButton(c);
			}
		}
		return r;
	}

	private string GetInputs()
	{			
		StringBuilder sb = new StringBuilder();
		
		for (int i = 0; i < gvAuctionLastBids.Rows.Count; i++)
		{
			for (int j = 2; j < gvAuctionLastBids.Rows[i].Cells.Count; j++)
			{
				RadioButton rbtn = GetRadioButton(gvAuctionLastBids.Rows[i].Cells[j]);
				if (rbtn != null)
				{
					if (rbtn.Checked) // if checked, add to list
					{						
						string item = gvAuctionLastBids.Rows[i].Cells[0].Text;
						string participantid = ((DataControlFieldCell)gvAuctionLastBids.Rows[i].Cells[j]).ContainingField.HeaderText.Split(new char[] { '(', ')' })[1].Replace("COMPANY_", "");
						string bid = rbtn.Text.Trim();

						sb.Append(GetAuctionTrailId(int.Parse(Session[Constant.SESSION_AUCTIONREFNO].ToString()), int.Parse(participantid), item, float.Parse(bid)) + "|");
					}
				}
			}
		}

		return sb.ToString().EndsWith("|") ? sb.ToString().Substring(0, sb.ToString().Length - 1) : sb.ToString();
	}

	private int GetAuctionTrailId(int auctionrefno, int participantid, string item, float bid)
	{
		SqlParameter[] sqlParams = new SqlParameter[4];
		sqlParams[0] = new SqlParameter("@AuctionRefNo", SqlDbType.Int);
		sqlParams[1] = new SqlParameter("@ParticipantId", SqlDbType.Int);
		sqlParams[2] = new SqlParameter("@Item", SqlDbType.NVarChar);
		sqlParams[3] = new SqlParameter("@Bid", SqlDbType.Money);
		sqlParams[0].Value = auctionrefno;
		sqlParams[1].Value = participantid;
		sqlParams[2].Value = item;
		sqlParams[3].Value = bid;
		
		return (int)SqlHelper.ExecuteScalar(connstring, "sp_GetAuctionTrailId", sqlParams);
	}
	protected void dsApprovedEndorsements_Selected(object sender, SqlDataSourceStatusEventArgs e)
	{
		if (e.AffectedRows == 0)
		{
			trApprove.Visible = false;
			trApprove2.Visible = false;
		}
		else
		{
			trApprove.Visible = true;
			trApprove2.Visible = true;
		}
	}
	protected void dsAuctionLatestBids_Selected(object sender, SqlDataSourceStatusEventArgs e)
	{
		if (e.AffectedRows == 0)
		{
			trDisapprove.Visible = false;
			trDisapprove2.Visible = false;
		}
		else
		{
			trDisapprove.Visible = true;
			trDisapprove2.Visible = true;
		}
	}

}