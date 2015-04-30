using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using EBid.lib.bid.data;
using EBid.lib.bid.trans;
using EBid.lib.constant;
using EBid.lib;
using System.IO;
using System.Configuration;
using System.Data.SqlClient;
using EBid.ConnectionString;

namespace EBid.web.vendor_screens
{
    public partial class BidDetails2 : System.Web.UI.Page
    {
        private string connstring = "";

        protected void Page_Load(object sender, System.EventArgs e)
        {
            FormsAuthenticationHelper.AuthenticateUserWithReturnUrl();
            connstring = Client.ConnectionString(HttpContext.Current.Session["clientid"].ToString());

            if (Int32.Parse(Session[Constant.SESSION_USERTYPE].ToString().Trim()) != (int)Constant.USERTYPE.VENDOR)
                Response.Redirect("../unauthorizedaccess.aspx");

            PageTitle.InnerText = String.Format(Constant.TITLEFORMAT, "Bid Event Details");

            if (Request.QueryString["brn"] != null)
            {
                Session[Constant.SESSION_BIDREFNO] = Request.QueryString["brn"].ToString();
            }

            if (Session[Constant.SESSION_BIDREFNO] == null)
                Response.Redirect("bids.aspx");

            if (!IsPostBack)
            {
                if (Session[Constant.SESSION_LASTPAGE] != null)
                {
                  
                    if ((Session[Constant.SESSION_LASTPAGE].ToString().Trim() == "~/web/vendorscreens/finishedbidevents.aspx") ||
                        (Session[Constant.SESSION_LASTPAGE].ToString().Trim() == "~/web/vendorscreens/declinedbidevents.aspx") ||
                        (Session[Constant.SESSION_LASTPAGE].ToString().Trim() == "~/web/vendorscreens/bidsforrenegotiation.aspx")) 
                    
                    {
                        pnlComments.Visible = btnBids.Visible = btnDecline.Visible = false;
                        
                    }
                    else
                        pnlComments.Visible = btnBids.Visible = btnDecline.Visible = BidEventTransaction.IsBidEventConfirmed(connstring, int.Parse(Session[Constant.SESSION_BIDREFNO].ToString()), int.Parse(Session[Constant.SESSION_USERID].ToString())) == 0 ? true : false;
                }
                else
                    pnlComments.Visible = btnBids.Visible = btnDecline.Visible = BidEventTransaction.IsBidEventConfirmed(connstring, int.Parse(Session[Constant.SESSION_BIDREFNO].ToString()), int.Parse(Session[Constant.SESSION_USERID].ToString())) == 0 ? true : false;
            }
        }

        protected void btnBids_Click(object sender, EventArgs e)
        {
            BidEventTransaction.ConfirmBidInvitation(connstring, int.Parse(Session[Constant.SESSION_BIDREFNO].ToString()), int.Parse(Session[Constant.SESSION_USERID].ToString()));

            int bid = int.Parse(Session[Constant.SESSION_BIDREFNO].ToString());
            int uid = int.Parse(Session[Constant.SESSION_USERID].ToString());

            if (txtComment.Text.Trim().Length > 0)
            {
                //if there is comment insert comment
                InsertParticipationComments(GetVendorsInBidId(uid, bid), txtComment.Text.ToString().Trim());
            }
            Response.Redirect("submittender.aspx");
        }

        protected void btnDecline_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                BidEventTransaction.DeclineBidInvitation(connstring, int.Parse(Session[Constant.SESSION_BIDREFNO].ToString()), int.Parse(Session[Constant.SESSION_USERID].ToString()));

                int bid = int.Parse(Session[Constant.SESSION_BIDREFNO].ToString());
                int uid = int.Parse(Session[Constant.SESSION_USERID].ToString());

                if (txtComment.Text.Trim().Length > 0)
                {
                    //if there is comment insert comment
                    InsertParticipationComments(GetVendorsInBidId(uid, bid), txtComment.Text.ToString().Trim());
                }

                Response.Redirect("decline.aspx");
            }
        }

        protected void lnkBack_Click(object sender, EventArgs e)
        {
            if (Session[Constant.SESSION_LASTPAGE] != null)
                Response.Redirect(Session[Constant.SESSION_LASTPAGE].ToString());
            else
                Response.Redirect("bids.aspx");
        }

        protected void cvValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = (txtComment.Text.Trim().Length > 0);
        }

        private int GetVendorsInBidId(int vendorid, int bidrefno)
        {
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@VendorId", vendorid);
            param[1] = new SqlParameter("@BidRefNo", bidrefno);

            return (int)SqlHelper.ExecuteScalar(connstring, CommandType.StoredProcedure, "sp_GetVendorsInBidId", param);
        }

        private void InsertParticipationComments(int vendorsInBidId, string comments)
        {
            SqlParameter[] sqlParams = new SqlParameter[2];
            sqlParams[0] = new SqlParameter("@VendorsInBidId", SqlDbType.Int);
            sqlParams[0].Value = vendorsInBidId;
            sqlParams[1] = new SqlParameter("@Comments", SqlDbType.NVarChar);
            sqlParams[1].Value = comments;

            SqlHelper.ExecuteNonQuery(connstring, CommandType.StoredProcedure, "sp_InsertBidParticipantComments", sqlParams);
        }
}
}
