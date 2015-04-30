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
using EBid.lib.auction.trans;
using EBid.lib.constant;
using EBid.lib;
using System.Data.SqlClient;
using EBid.ConnectionString;

public partial class web_onlineAuction_participateauctionevent : System.Web.UI.Page
{
	private int _aid = 0, _uid = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session[Constant.SESSION_USERID] == null)
            Response.Redirect("sessionexpired.aspx");

        PageTitle.InnerText = String.Format(Constant.TITLEFORMAT, "Participate Auction Event");

		// check user type, if not vendor, redirect to upcoming events page
		if (Session[Constant.SESSION_USERTYPE].ToString() != ((int)Constant.USERTYPE.VENDOR).ToString())
			Response.Redirect("~/web/auctions/upcomingauctionevents.aspx");

		// get userid
		_uid = Convert.ToInt32(Session[Constant.SESSION_USERID].ToString());
				
		// check if auction ref no is provided
		if (OnlineAuctionRefNo != null)
		{
			// if yes, continue
            		_aid = int.Parse(OnlineAuctionRefNo);
            		hdnAuctionRefNo.Value = OnlineAuctionRefNo;
            			
			// check if invitation is already still pending
			// if not, display to user status
			// else, continue
			// check if auction's confirmation deadline is not yet reached
			// if already reached, redirect to error page
			// else, continue

			// check if there was error
			if (Session["ERRORMESSAGE"] != null)
			// if yes, display it
			{
				lblError.Text = Session["ERRORMESSAGE"].ToString().Trim();
				Session["ERRORMESSAGE"] = null;
			}
		}	
		else
		{
			// if not, redirect to upcoming auctions page
			Response.Redirect("~/web/auctions/upcomingauctionevents.aspx");
		}
			
    }

	public string Format(string date)
	{
		return FormattingHelper.FormatDateToString(Convert.ToDateTime(date));
	}

	protected void lnkParticipate_Command(object sender, CommandEventArgs e)
	{
		// check again if confirmation is not yet reached
		// if yes, redirect to error page
		// else, continue
		if (UpdateStatus(_aid, _uid, 3 /* Participate Auction */, EncryptionHelper.Encrypt(txtTicket.Text.Trim())))
		{
			Session["ERRORMESSAGE"] = "";
                        
            Session.Add(String.Format("AUCTIONTICKET:{0}", _aid), Session.SessionID);

            string  url = String.Format("~/web/auctions/onlineauctionpopup.aspx?{0}", Request.QueryString);
                        
            Response.Redirect(url);
		}
		// if failed
		else
		{			
			Session["ERRORMESSAGE"] = "Invalid Ticket!";
			Response.Redirect(Request.RawUrl);
		}
	}

	private bool DeadlineReached(int auctionrefno)
	{
		return false;
	}

	public bool UpdateStatus(int auctionrefno, int vendorid, int task, string ticket)
	{
		//string connstring = HttpContext.Current.Session["ConnectionString"].ToString();
		string connstring = Client.ConnectionString(HttpContext.Current.Session["clientid"].ToString());
		SqlConnection sqlConnect = new SqlConnection(connstring);
		SqlTransaction sqlTransact = null;		
		bool success = false;

		try
		{
			sqlConnect.Open();
			sqlTransact = sqlConnect.BeginTransaction();
		
			SqlParameter[] sqlParams = new SqlParameter[4];
			sqlParams[0] = new SqlParameter("@AuctionRefNo", SqlDbType.Int);
			sqlParams[1] = new SqlParameter("@Vendorid", SqlDbType.Int);
			sqlParams[2] = new SqlParameter("@Task", SqlDbType.Int);
			sqlParams[3] = new SqlParameter("@Ticket", SqlDbType.NVarChar);
			sqlParams[0].Value = auctionrefno;
			sqlParams[1].Value = vendorid;
			sqlParams[2].Value = task;
			sqlParams[3].Value = ticket;

			int i = (int)SqlHelper.ExecuteScalar(sqlTransact, CommandType.StoredProcedure, "sp_SetVendorAuctionParticipationStatus", sqlParams);

			sqlTransact.Commit();

			if (i == 1)
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
    
    private string OnlineAuctionRefNo
    {
        get
        {            
            return FormattingHelper.DecryptQueryString(Request.QueryString["qs"].Trim());
        }
    }

    protected void lnkTerms_Click(object sender, EventArgs e)
    {
        Response.Redirect(ConfigurationManager.AppSettings["ServerUrl"] + "rules.htm");
    }
}
