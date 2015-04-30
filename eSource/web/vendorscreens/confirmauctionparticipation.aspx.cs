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
using System.Data.SqlClient;
using EBid.lib.constant;
using EBid.lib.auction.data;
using EBid.lib.auction.trans;
using EBid.lib.bid.data;
using EBid.lib.bid.trans;
using EBid.lib.user.trans;
using EBid.lib.user.data;
using EBid.lib;

public partial class web_vendorscreens_confirmauctionparticipation : System.Web.UI.Page
{
    string connstring = "";

    protected void Page_Load(object sender, EventArgs e)
    {
		FormsAuthenticationHelper.AuthenticateUserWithReturnUrl();
        connstring = HttpContext.Current.Session["ConnectionString"].ToString();
        if (Int32.Parse(Session[Constant.SESSION_USERTYPE].ToString().Trim()) != (int)Constant.USERTYPE.VENDOR)
            Response.Redirect("../unauthorizedaccess.aspx");

        PageTitle.InnerText = String.Format(Constant.TITLEFORMAT, "Confirm Auction Event Participation");

        if (Session[Constant.SESSION_AUCTIONREFNO] == null)
            Response.Redirect("auctions.aspx");

        if (!IsPostBack)
        {
            if (Session["ERRORMESSAGE"] != null)
            {
                lblError.Text = Session["ERRORMESSAGE"].ToString().Trim();
                Session["ERRORMESSAGE"] = null;
            }
        }
    }

    protected void lnkBack_Click(object sender, EventArgs e)
    {
        if (Session[Constant.SESSION_LASTPAGE] != null)
            Response.Redirect(Session[Constant.SESSION_LASTPAGE].ToString());
        else
            Response.Redirect("auctions.aspx");
    }

    protected void lnkConfirm_Command(object sender, CommandEventArgs e)
    {
        int aid = int.Parse(Session[Constant.SESSION_AUCTIONREFNO].ToString());
        int uid = int.Parse(Session[Constant.SESSION_USERID].ToString());

        if (AuctionTransaction.UpdateStatus(connstring, aid, uid, Constant.BID_INVITATION_STATUS_CONFIRM /* Confirm Invitation */, EncryptionHelper.Encrypt(txtTicket.Text.Trim())))
        {   // if successful
            if (txtComment.Text.Trim().Length > 0)
            {
                //if there is comment insert comment
                InsertParticipationComments(GetParticipantId(uid, aid), txtComment.Text.ToString().Trim());
            }
            Response.Redirect("confirmedinvitations.aspx");
        }
        else
        {
            Session["ERRORMESSAGE"] = "Invalid Ticket!";
            Response.Redirect(Request.RawUrl);
        }
    }

    protected void lnkDecline_Command(object sender, CommandEventArgs e)
    {
        if (IsValid)
        {
            int aid = int.Parse(Session[Constant.SESSION_AUCTIONREFNO].ToString());
            int uid = int.Parse(Session[Constant.SESSION_USERID].ToString());

            if (AuctionTransaction.UpdateStatus(connstring, aid, uid, Constant.BID_INVITATION_STATUS_DECLINE /* Confirm Invitation */, EncryptionHelper.Encrypt(txtTicket.Text.Trim())))
            {   // if successful
                if (txtComment.Text.Trim().Length > 0)
                {
                    InsertParticipationComments(GetParticipantId(uid, aid), txtComment.Text.ToString().Trim());
                }
                Response.Redirect("auctions.aspx");
            }
            else
            {
                Session["ERRORMESSAGE"] = "Invalid Ticket!";
                Response.Redirect(Request.RawUrl);
            }
        }
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
