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
using EBid.lib.constant;
using EBid.lib.bid.data;
using EBid.lib.bid.trans;
using EBid.lib.user.trans;
using EBid.lib.user.data;
using EBid.lib.auction.data;
using EBid.lib;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Text;
using EBid.ConnectionString;


public partial class web_purchasing_screens_draftBidDetails : System.Web.UI.Page
{
    private string connstring = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (HttpContext.Current.Session["clientid"] != null)
        {
            connstring = Client.ConnectionString(HttpContext.Current.Session["clientid"].ToString());
        }
        else
        {
            Response.Redirect("../../login.aspx");
        }
        FormsAuthenticationHelper.AuthenticateUserWithReturnUrl();
        if (Int32.Parse(Session[Constant.SESSION_USERTYPE].ToString().Trim()) != (int)Constant.USERTYPE.PURCHASING)
            Response.Redirect("../unauthorizedaccess.aspx");

        if (Session[Constant.SESSION_BIDREFNO] == null)
            Response.Redirect("bids.aspx");

        if (!(Page.IsPostBack))
        {            
            Session[Constant.SESSION_COMMENT_TYPE] = "0";            
        }

        PageTitle.InnerText = String.Format(Constant.TITLEFORMAT, "Bid Event Details");
        if (CheckVSF() == "2")
        {
            btnApprove.Visible = false;
            lblMessage.Text = "VSF is currently being clarified.";
            //Response.Write(oStatus);
        }

    }

    private string CheckVSF()
    {
        connstring = Client.ConnectionString(HttpContext.Current.Session["clientid"].ToString());
        int bidrefno = int.Parse(Session[Constant.SESSION_BIDREFNO].ToString());
        string sCommand = "SELECT t2.Status FROM tblBidItems t1, tblVendorShortlistingForm t2 WHERE t1.BidRefNo=" + bidrefno + " AND t2.VSFId = t1.VSFId";
        string oStatus = "";
        //Response.Write(sCommand);
        SqlDataReader oReader = SqlHelper.ExecuteReader(connstring, CommandType.Text, sCommand);
        if (oReader.HasRows)
        {
            oReader.Read();
            oStatus = oReader["Status"].ToString();
        }
        return oStatus;
    }

    protected void btnReedit_Click(object sender, EventArgs e)
    {
        if (UpdateBidEventStatus(int.Parse(Session[Constant.SESSION_BIDREFNO].ToString()), Constant.BID_STATUS_RE_EDIT, 0,
            int.Parse(Session[Constant.SESSION_USERID].ToString()), txtComment.Text.Trim()))
        {
            Session["STATUS"] = Constant.BID_STATUS_RE_EDIT.ToString().Trim();            
            Response.Redirect("confirmation.aspx");
        }        
    }

    protected void btnReject_Click(object sender, EventArgs e)
    {
        if (UpdateBidEventStatus(int.Parse(Session[Constant.SESSION_BIDREFNO].ToString()), Constant.BID_STATUS_REJECTED, 0,
            int.Parse(Session[Constant.SESSION_USERID].ToString()), txtComment.Text.Trim()))
        {
            Session["STATUS"] = Constant.BID_STATUS_REJECTED.ToString().Trim();
            Response.Redirect("confirmation.aspx");
        } 
    }
    
    protected void btnApprove_Click(object sender, EventArgs e)
    {
        if (CheckVSF() != "2")
        {
            string VSFStatus = CheckVSF();
            if (UpdateBidEventStatus(int.Parse(Session[Constant.SESSION_BIDREFNO].ToString()), Constant.BID_STATUS_APPROVED, 0,
                int.Parse(Session[Constant.SESSION_USERID].ToString()), txtComment.Text.Trim()))
            {

                if (VSFStatus == "1")
                {
                    int bidrefno1 = int.Parse(Session[Constant.SESSION_BIDREFNO].ToString());
                    string sCommand = "SELECT VSFId FROM tblBidItems WHERE BidRefNo=" + bidrefno1;
                    string oVSFId = "";
                    SqlDataReader oReader = SqlHelper.ExecuteReader(connstring, CommandType.Text, sCommand);
                    if (oReader.HasRows)
                    {
                        oReader.Read();
                        oVSFId = oReader["VSFId"].ToString();
                        SendEmailNotification(oVSFId);
                    }

                }
                Session["STATUS"] = Constant.BID_STATUS_APPROVED.ToString().Trim();

                // by: zander => previously disabled (not sure why?)
                if (IsForBidForOpening() == 1)
                {
                    AddToBidsForOpening();
                }
                Response.Redirect("confirmation.aspx");
            }
        }
    }
    
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Session.Remove(Constant.SESSION_BIDREFNO);
        Response.Redirect("bids.aspx");
    }

    private bool UpdateBidEventStatus(int bidrefno, int status, int buyerid, int purchasingid, string comments)
    {
        SqlConnection sqlConnect = new SqlConnection(connstring);
        SqlTransaction sqlTransact = null;
        bool isSuccessful = false;

        try
        {
            sqlConnect.Open();
            sqlTransact = sqlConnect.BeginTransaction();

            SqlParameter[] sqlParams = new SqlParameter[5];
            sqlParams[0] = new SqlParameter("@BidRefNo", SqlDbType.Int);
            sqlParams[0].Value = bidrefno;
            sqlParams[1] = new SqlParameter("@Status", SqlDbType.Int);
            sqlParams[1].Value = status;
            sqlParams[2] = new SqlParameter("@BuyerId", SqlDbType.Int);
            sqlParams[2].Value = buyerid;
            sqlParams[3] = new SqlParameter("@PurchasingId", SqlDbType.Int);
            sqlParams[3].Value = purchasingid;
            sqlParams[4] = new SqlParameter("@Comment", SqlDbType.VarChar);
            sqlParams[4].Value = comments;

            SqlHelper.ExecuteNonQuery(connstring, CommandType.StoredProcedure, "sp_UpdateBidEventStatus", sqlParams);

            sqlTransact.Commit();            
            isSuccessful = true;
        }
        catch
        {
            sqlTransact.Rollback();
            isSuccessful = false;
        }
        finally
        {
            sqlConnect.Close();
        }
        return isSuccessful;
    }

    private int IsForBidForOpening()
    {
        SqlParameter[] sqlParams = new SqlParameter[1];
        sqlParams[0] = new SqlParameter("@BidRefNo", SqlDbType.Int);
        sqlParams[0].Value = int.Parse(Session[Constant.SESSION_BIDREFNO].ToString());

        return Convert.ToInt32(SqlHelper.ExecuteScalar(connstring, CommandType.StoredProcedure, "sp_IsBidEventForOpening", sqlParams).ToString().Trim());
    }

    private BidDetails GetBidItemDetails(int bidrefno)
    {
        DataTable dt = SqlHelper.ExecuteDataset(connstring, "sp_GetBidInvitationInfo", new SqlParameter[] { new SqlParameter("@BidRefNo", bidrefno) }).Tables[0];
        BidDetails item = new BidDetails();

        if (dt.Rows.Count > 0)
            item = new BidDetails(dt.Rows[0]);

        return item;
    }

    private void AddToBidsForOpening()
    {
        BidDetails details = GetBidItemDetails(int.Parse(Session[Constant.SESSION_BIDREFNO].ToString()));

        SqlConnection sqlConnect = new SqlConnection(connstring);
        SqlTransaction sqlTransact = null;
        try
        {
            sqlConnect.Open();
            sqlTransact = sqlConnect.BeginTransaction();

            SqlParameter[] sqlParams = new SqlParameter[2];
            sqlParams[0] = new SqlParameter("@BidRefNo", SqlDbType.Int);
            sqlParams[0].Value = int.Parse(Session[Constant.SESSION_BIDREFNO].ToString());
            sqlParams[1] = new SqlParameter("@Deadline", SqlDbType.DateTime);
            sqlParams[1].Value = details.SubmissionDeadline;

            SqlHelper.ExecuteNonQuery(connstring, CommandType.StoredProcedure, "sp_InsertBidItemForOpening", sqlParams);

            sqlTransact.Commit();
        }
        catch
        {
            sqlTransact.Rollback();
        }
        finally
        {
            sqlConnect.Close();
        }

    }





    //############################################################
    //############################################################
    private bool SendEmailNotification(string strVSFId)
    {
        SqlDataReader oReader;
        string connstring = HttpContext.Current.Session["ConnectionString"].ToString();
        string query;
        SqlCommand cmd;
        SqlConnection conn;
        bool success = false;

        string fromName = "";
        string fromEmail = "";
        string from = "";
        string toName = "";
        string toEmail = "";
        string to = "";
        string subject = "";
        string oProjectName = "";
        string oVSFId = "";

        query = "SELECT t1.VSFId, t1.ProjectName, t2.BuyerFirstName + ' ' + t2.BuyerMidName + ' ' + t2.BuyerLastName AS BuyerName, t2.EmailAdd as BuyerEmail, t3.FirstName + ' ' + t3.MiddleName + ' ' + t3.LastName AS PurchasingName, t3.EmailAdd as PurchasingEmail FROM tblVendorShortlistingForm t1, tblBuyers t2, tblPurchasing t3 WHERE t1.VSFId=@VSFId AND t2.BuyerId = t1.BuyerId AND t3.PurchasingId = t1.PurchasingId";
        //query = "sp_GetVendorInformation"; //##storedProcedure
        using (conn = new SqlConnection(connstring))
        {
            using (cmd = new SqlCommand(query, conn))
            {
                //cmd.CommandType = CommandType.StoredProcedure; //##storedProcedure
                cmd.Parameters.AddWithValue("@VSFId", Convert.ToInt32(strVSFId));
                conn.Open();
                //Process results
                oReader = cmd.ExecuteReader();
                if (oReader.HasRows)
                {
                    while (oReader.Read())
                    {
                        oVSFId = oReader["VSFId"].ToString();
                        oProjectName = oReader["ProjectName"].ToString();

                        fromName = oReader["PurchasingName"].ToString();
                        fromEmail = oReader["PurchasingEmail"].ToString();
                        from = '"' + oReader["PurchasingName"].ToString() + '"' + " <" + oReader["BuyerEmail"].ToString() + ">";

                        toName = oReader["BuyerName"].ToString();
                        toEmail = oReader["BuyerEmail"].ToString();
                        to = '"' + oReader["BuyerName"].ToString() + '"' + " <" + oReader["PurchasingEmail"].ToString() + ">";
                    }
                }
            }
        }

        try
        {
            subject = "VSF Approved";
            if (!MailHelper.SendEmail(MailTemplate.GetDefaultSMTPServer(),
                    from,
                    to,
                    subject,
                    CreateNotificationBody(oVSFId, oProjectName, fromName, toName),
                    MailTemplate.GetTemplateLinkedResources(this)))
            {	//if sending failed					
                LogHelper.EventLogHelper.Log("Bid > Send Notification : Sending Failed to " + from, System.Diagnostics.EventLogEntryType.Error);
            }
            else
            {	//if sending successful
                LogHelper.EventLogHelper.Log("Bid > Send Notification : Email Sent to " + from, System.Diagnostics.EventLogEntryType.Information);

            }
            success = true;
        }
        catch (Exception ex)
        {
            success = false;
            LogHelper.EventLogHelper.Log("Bid > Send Notification : " + ex.Message, System.Diagnostics.EventLogEntryType.Error);
        }
        return success;
    }

    private string CreateNotificationBody(string oVSFId, string oProjectName, string fromName, string toName)
    {
        StringBuilder sb = new StringBuilder();

        string BuyersName1 = fromName;
        string PurchasingName1 = toName;

        sb.Append("<tr><td><p><strong>VSF Approved</strong> <br> Sent: " + DateTime.Now.ToLongDateString() + "</p> <p>From: " + BuyersName1 + "<br><br> To: " + PurchasingName1 + "<br><br> Subject: " + oProjectName + "<br><br> Dear Buyer, <br><br> Re: Vendor Shortlisting Form Approved – <strong>" + oProjectName + "</strong><br><br> This is to inform you that Vendor Shortlisting Form has been approved with the ff:<br><br> <b>VSF Reference Number:</b> " + oVSFId + "<br> <b>Project Name:</b> " + oProjectName + "<br></p>&nbsp;<p>Very truly yours,<br><br><br> <strong>e-Sourcing Procurement</strong></p><p>&nbsp;</p> <p><strong>Instructions:</strong></p> <ol> <li>Go to <a href='https://e-sourcing.Trans-Asia.com.ph/'>https://e-sourcing.Trans-Asia.com.ph</a></li> <li>Enter your Username and Password then  click Login</li> <li>Under Vendor Shortlisting Form, click View Endorsed</li> <li>Click VSF Reference No: " + oVSFId + "</li> <li>Review / Endorse / VSF for Awarding</li> <li>Click Clarify if you have clarification  or click Approved to award VSF</li> </ol> Please do not reply to this auto-generated  message.&nbsp;</td></tr>");

        return MailTemplate.IntegrateBodyIntoTemplate(sb.ToString());
    }
}
