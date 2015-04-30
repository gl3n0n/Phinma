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

public partial class web_onlineAuction_confirmauctionevent : System.Web.UI.Page
{
	private int _aid = 0, _uid = 0;
	private string _backUrl = "";
    private string connstring = "";

    protected void Page_Load(object sender, EventArgs e)
    {
		FormsAuthenticationHelper.AuthenticateUserWithReturnUrl();
        connstring = Client.ConnectionString(HttpContext.Current.Session["clientid"].ToString());	
        PageTitle.InnerText = String.Format(Constant.TITLEFORMAT, "Confirm Participation to Auction Event"); 
     
		if (Session["CurrentTab"] == null)		
			Session["CurrentTab"] = "2";

		switch (Session["CurrentTab"].ToString())
		{
			case "1": _backUrl = "~/web/auctions/ongoingauctionevents.aspx"; break;
			case "2": _backUrl = "~/web/auctions/upcomingauctionevents.aspx"; break;
			default: _backUrl = "~/web/auctions/upcomingauctionevents.aspx"; break;
		}		

		// check user type, if not vendor, redirect to upcoming events page
		if (Session[Constant.SESSION_USERTYPE].ToString() != ((int)Constant.USERTYPE.VENDOR).ToString())
			Response.Redirect(_backUrl);

		// get userid
		_uid = Convert.ToInt32(Session[Constant.SESSION_USERID].ToString());
				
		// check if auction ref no is provided
		if (!String.IsNullOrEmpty(Request.QueryString["aid"]))
		{
			// if yes, continue
			string q = string.Empty;
			try
			{
                string aid = HttpUtility.UrlDecode(Request.QueryString["aid"]);
				//q = EncryptionHelper.Decrypt(aid.Replace("%PLUS%", "+"));
                q = FormattingHelper.DecryptQueryString(aid);
                _aid = Convert.ToInt32(q);
			}
			catch
			{ 
				// redirect to error page
                Response.Redirect(_backUrl);
			}			
			Session[Constant.SESSION_AUCTIONREFNO] = q;
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
			// if not, redirect page
			Response.Redirect(_backUrl);
		}

       
    }

	public string Format(string date)
	{
		return FormattingHelper.FormatDateToString(Convert.ToDateTime(date));
	}
	
	protected void lnkConfirm_Command(object sender, CommandEventArgs e)
	{
		// check again if confirmation is not yet reached
		// if yes, redirect to error page
		// else, continue
        if (AuctionTransaction.UpdateStatus(connstring, _aid, _uid, Constant.BID_INVITATION_STATUS_CONFIRM /* Confirm Invitation */, EncryptionHelper.Encrypt(txtTicket.Text.Trim())))
		{
            int aid = int.Parse(Session[Constant.SESSION_AUCTIONREFNO].ToString());
            int uid = int.Parse(Session[Constant.SESSION_USERID].ToString());

            if (txtComment.Text.Trim().Length > 0)
            {
                //if there is comment insert comment
                InsertParticipationComments(GetParticipantId(uid, aid), txtComment.Text.ToString().Trim());
            }

			Session["ERRORMESSAGE"] = "";
			Session["CONFIRMATIONMESSAGE"] = "You have successfully confirmed your invitation.";
			Response.Redirect(_backUrl);
		}
		// if failed
		else
		{
			Session["CONFIRMATIONMESSAGE"] = "";
			Session["ERRORMESSAGE"] = "Invalid Ticket!";
			Response.Redirect(Request.RawUrl);
		}
	}

	protected void lnkDecline_Command(object sender, CommandEventArgs e)
	{
        if (IsValid)
        {
            // check again if confirmation is not yet reached
            // if yes, redirect to error page
            // else, continue		
            if (AuctionTransaction.UpdateStatus(connstring, _aid, _uid, Constant.BID_INVITATION_STATUS_DECLINE /* Decline Invitation */, EncryptionHelper.Encrypt(txtTicket.Text.Trim())))
            // if success
            {
                
                int aid = int.Parse(Session[Constant.SESSION_AUCTIONREFNO].ToString());
                int uid = int.Parse(Session[Constant.SESSION_USERID].ToString());

                if (txtComment.Text.Trim().Length > 0)
                {
                    //if there is comment insert comment
                    InsertParticipationComments(GetParticipantId(uid, aid), txtComment.Text.ToString().Trim());
                }

                Session["ERRORMESSAGE"] = "";
                Session["CONFIRMATIONMESSAGE"] = "You have successfully declined your invitation.";
                Response.Redirect(_backUrl);
            }
            // if failed
            else
            {
                Session["CONFIRMATIONMESSAGE"] = "";
                Session["ERRORMESSAGE"] = "Invalid Ticket!";
                Response.Redirect(Request.RawUrl);
            }
        }
	}

	private bool DeadlineReached(int auctionrefno)
	{
		return false;
	}	
	
	protected void lnkCancel_Click(object sender, EventArgs e)
	{
		Response.Redirect(_backUrl);
	}

    private void InsertParticipationComments(int participantId, string comments)
    {
        SqlParameter[] sqlParams = new SqlParameter[2];
        sqlParams[0] = new SqlParameter("@ParticipantId", SqlDbType.Int);
        sqlParams[0].Value = participantId;
        sqlParams[1] = new SqlParameter("@Comments", SqlDbType.NVarChar);
        sqlParams[1].Value = comments;

        SqlHelper.ExecuteNonQuery(connstring, CommandType.StoredProcedure, "sp_InsertParticipantComments", sqlParams);
    }

    private int GetParticipantId(int vendorid, int auctionrefno)
    {
        SqlParameter[] param = new SqlParameter[2];
        param[0] = new SqlParameter("@VendorId", vendorid);
        param[1] = new SqlParameter("@AuctionRefNo", auctionrefno);

        return (int)SqlHelper.ExecuteScalar(connstring, CommandType.StoredProcedure, "sp_GetVendorParticipantId", param);
    }

    protected void cvValidator_ServerValidate(object source, ServerValidateEventArgs args)
    {
        args.IsValid = (txtComment.Text.Trim().Length > 0);
    }
    protected void lnkTerms_Click(object sender, EventArgs e)
    {
        Response.Redirect(ConfigurationManager.AppSettings["ServerUrl"] + "rules.htm");
    }
}
