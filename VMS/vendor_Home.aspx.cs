using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using Ava.lib;
using Ava.lib.constant;
using System.Text;
using System.Text.RegularExpressions;
using System.Drawing;

public partial class vendor_Home : System.Web.UI.Page
{
    protected void TestShowAllSessions()
    {  //test show all session
        string str = null;
        foreach (string key in HttpContext.Current.Session.Keys)
        { str += string.Format("<b>{0}</b>: {1};  ", key, HttpContext.Current.Session[key].ToString()); }
        Response.Write("<span style='font-size:12px'>" + str + "</span>");
    }

    SqlDataReader oReader;
    string connstring = ConfigurationManager.ConnectionStrings["AVAConnectionString"].ConnectionString;
    string query;
    SqlCommand cmd;
    SqlConnection conn;
    public int VendorId;
    public string queryString;

    protected void Page_Load(object sender, EventArgs e)
    {

        queryString = (Request.QueryString["VendorId"] != null && Request.QueryString["VendorId"] != "") ? "?VendorId=" + Request.QueryString["VendorId"] : "";
        if (Request.QueryString["VendorId"] != null && Request.QueryString["VendorId"] != "")
        {
            VendorId = Convert.ToInt32(Request.QueryString["VendorId"]);
        }
        else if (Session["VendorId"] != null && Session["VendorId"].ToString() != "")
        {
            VendorId = Convert.ToInt32(Session["VendorId"].ToString());
        }

        if (Session["UserId"] == null || Session["UserId"].ToString() == "" || VendorId.ToString()=="")
        {
            Response.Redirect("login.aspx");
        }

        string control1 = Request.Form["__EVENTTARGET"];

        if (editable())
        {
            if (IsPostBack)
            {
                if (control1 == "justSave")
                {
                    SaveToDB();
                }
                else if (control1 == "endorse")
                {
                    SubmitToDNB();
                    PopulateFields_Lbl(); 
                    repeaterCommentsDnbClarify.DataBind();
                }
            }
            PopulateFields(); 
            //paymentProofFile.Visible = true;
            //SaveBt1.Visible = submitToDNBbt.Visible = true;
            //tbl01_Lbl.Visible = false;
        }
        else
        {
            PopulateFields_Lbl(); 
            //paymentProofFile.Visible = false;
            //SaveBt1.Visible = submitToDNBbt.Visible = false;
            //tbl01.Visible = false;
            //createBt.Visible = createBt1.Visible = false;
        }
    }

    protected void Page_LoadComplete(object sender, EventArgs e)
    {
        
    }

    private bool editable()
    {
        bool EditMode = false;
        query = "SELECT * FROM tblVendor WHERE VendorId =  @VendorId";
        using (conn = new SqlConnection(connstring))
        {
            using (cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@VendorId", VendorId);
                conn.Open();
                oReader = cmd.ExecuteReader();
                if (oReader.HasRows)
                {
                    while (oReader.Read())
                    {
                        if (
                            (Session["SESSION_USERTYPE"].ToString() == "11" && oReader["Status"].ToString() == "0") ||
                            (Session["SESSION_USERTYPE"].ToString() == "11" && oReader["Status"].ToString() == "9")
                            )
                        {
                            EditMode = true;
                            if (oReader["Status"].ToString() == "9") { clarifyCommentDiv.Visible = true; }
                        }
                    }
                }
            }
        }

        return EditMode;
    }

    void PopulateFields() 
    {
        query = "SELECT * FROM tblVendor WHERE VendorId = @VendorId";
        //query = "sp_GetVendorInformation"; //##storedProcedure
        using (conn = new SqlConnection(connstring))
        {
            using (cmd = new SqlCommand(query, conn))
            {
                //cmd.CommandType = CommandType.StoredProcedure; //##storedProcedure
                cmd.Parameters.AddWithValue("@VendorId", VendorId);
                conn.Open();
                //Process results
                oReader = cmd.ExecuteReader();
                if (oReader.HasRows)
                {
                    while (oReader.Read())
                    {
                        AuthenticationTicketLbl.Text = oReader["AuthenticationTicket"].ToString() !="" ?   oReader["AuthenticationTicket"].ToString() : "n/a";
                        CompanyNameLbl.Text = oReader["CompanyName"].ToString()!="" ? "<h3>"+oReader["CompanyName"].ToString()+"</h3>" : "";
                        //FileName_Lbl.Text = "<div style=\"float:left; width:30px;\"><img src=\"images/attachment.png\" /></div> <a href='" + oReader["FileName"].ToString() + "' target='_blank'>Attachment</a>";
                        paymentProofLbl.Text = oReader["paymentProof"].ToString() != "" ? "<a href='" + oReader["paymentProof"].ToString() + "' target='_blank'>" + oReader["paymentProof"].ToString() + "</a>" + "</a>" + "<script type='text/javascript'>$(document).ready(function(){ $('#paymentProofx').show(); });</script>" : "Attach proof of payment";
                        paymentProof.Value = oReader["paymentProof"].ToString();

                        if (oReader["AuthenticationTicket"].ToString() != "")
                        {
                            //generateAuthBt.Visible = false;
                            authDiv.Visible = true;
                        }
                        else
                        {
                            authDiv.Visible = false;
                        }
                        if (oReader["Status"].ToString() != "0" && oReader["Status"].ToString() != "9")
                        {
                            paymentProofFile.Visible = false;
                            SaveBt1.Visible = submitToDNBbt.Visible = false;
                        }
                        if (oReader["Status"].ToString() == "9")
                        {
                            clarifyTxt.Text = "For Clarification.";
                            //clarifyTxt.ForeColor = Color.Blue;
                        }

                    }
                }
            }
        }


        query = "SELECT * FROM tblVendorInformation WHERE VendorId = @VendorId";
        //query = "sp_GetVendorInformation"; //##storedProcedure
        using (conn = new SqlConnection(connstring))
        {
            using (cmd = new SqlCommand(query, conn))
            {
                //cmd.CommandType = CommandType.StoredProcedure; //##storedProcedure
                cmd.Parameters.AddWithValue("@VendorId", VendorId);
                conn.Open();
                //Process results
                oReader = cmd.ExecuteReader();
                if (oReader.HasRows)
                {
                    while (oReader.Read())
                    {
                        CertAndWarranty_AttachedSignedLbl.Text = oReader["CertAndWarranty_AttachedSigned"].ToString() != "" ? "<a href='" + oReader["CertAndWarranty_AttachedSigned"].ToString() + "' target='_blank'>" + oReader["CertAndWarranty_AttachedSigned"].ToString() + "</a>" + "<script type='text/javascript'>$(document).ready(function(){ $('#CertAndWarranty_AttachedSignedx').show(); });</script>" : "Attach signed form";
                        CertAndWarranty_AttachedSigned.Value = oReader["CertAndWarranty_AttachedSigned"].ToString();
                    }
                }
            }
        }
    }


    void PopulateFields_Lbl()
    {
        query = "SELECT AuthenticationTicket, CompanyName, paymentProof FROM tblVendor WHERE VendorId = @VendorId";
        //query = "sp_GetVendorInformation"; //##storedProcedure
        using (conn = new SqlConnection(connstring))
        {
            using (cmd = new SqlCommand(query, conn))
            {
                //cmd.CommandType = CommandType.StoredProcedure; //##storedProcedure
                cmd.Parameters.AddWithValue("@VendorId", VendorId);
                conn.Open();
                //Process results
                oReader = cmd.ExecuteReader();
                if (oReader.HasRows)
                {
                    while (oReader.Read())
                    {
                        AuthenticationTicketLbl.Text = oReader["AuthenticationTicket"].ToString() != "" ? oReader["AuthenticationTicket"].ToString() : "n/a";
                        CompanyNameLbl.Text = oReader["CompanyName"].ToString() != "" ? "<h3>" + oReader["CompanyName"].ToString() + "</h3>" : "";
                        paymentProofLbl.Text = oReader["paymentProof"].ToString()!="" ?  "<div style=\"float:left; width:30px;\"><img src=\"images/attachment.png\" /></div> <a href='" + oReader["paymentProof"].ToString() + "' target='_blank'>" + oReader["paymentProof"].ToString() + "</a>" : "<div style=\"float:left; width:30px;\"><img src=\"images/attachment.png\" /></div> No attach file.";
                        //paymentProofLbl.Text = "<a href='" + oReader["paymentProof"].ToString() + "' target='_blank'>" + oReader["paymentProof"].ToString() + "</a>";
                        //paymentProof.Value = oReader["paymentProof"].ToString();
                        paymentProofFile.Visible = false;
                        SaveBt1.Visible = submitToDNBbt.Visible = false;
                    }
                }
            }
        }
        query = "SELECT CertAndWarranty_AttachedSigned FROM tblVendorInformation WHERE VendorId = @VendorId";
        //query = "sp_GetVendorInformation"; //##storedProcedure
        using (conn = new SqlConnection(connstring))
        {
            using (cmd = new SqlCommand(query, conn))
            {
                //cmd.CommandType = CommandType.StoredProcedure; //##storedProcedure
                cmd.Parameters.AddWithValue("@VendorId", VendorId);
                conn.Open();
                //Process results
                oReader = cmd.ExecuteReader();
                if (oReader.HasRows)
                {
                    while (oReader.Read())
                    {
                        CertAndWarranty_AttachedSignedLbl.Text = oReader["CertAndWarranty_AttachedSigned"].ToString() != "" ? "<div style=\"float:left; width:30px;\"><img src=\"images/attachment.png\" /></div> <a href='" + oReader["CertAndWarranty_AttachedSigned"].ToString() + "' target='_blank'>" + oReader["CertAndWarranty_AttachedSigned"].ToString() + "</a>" : "<div style=\"float:left; width:30px;\"><img src=\"images/attachment.png\" /></div> No attach file.";
                        CertAndWarranty_AttachedSignedFile.Visible = false;
                    }
                }
            }
        }
    }

    void SaveToDB()
    {
        //string status1 = Request.Form["status"].ToString();
        string connstring = ConfigurationManager.ConnectionStrings["AVAConnectionString"].ConnectionString;
        //string sCommand = "";

        query = "UPDATE tblVendor SET paymentProof=@paymentProof WHERE VendorId=@VendorId";
        using (conn = new SqlConnection(connstring))
        {
            using (cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@VendorId", VendorId);
                cmd.Parameters.AddWithValue("@paymentProof", paymentProof.Value);
                conn.Open(); cmd.ExecuteNonQuery();
            }
        }
        query = "UPDATE tblVendorInformation SET CertAndWarranty_AttachedSigned=@CertAndWarranty_AttachedSigned WHERE VendorId=@VendorId";
        using (conn = new SqlConnection(connstring))
        {
            using (cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@VendorId", VendorId);
                cmd.Parameters.AddWithValue("@CertAndWarranty_AttachedSigned", CertAndWarranty_AttachedSigned.Value);
                conn.Open(); cmd.ExecuteNonQuery();
            }
        }
    }


    void SubmitToDNB()
    {
        string connstring = ConfigurationManager.ConnectionStrings["AVAConnectionString"].ConnectionString;
        string sCommand = "";

        sCommand = "SELECT Status FROM tblVendor WHERE VendorId = " + VendorId.ToString();
        oReader = SqlHelper.ExecuteReader(connstring, CommandType.Text, sCommand);
        if (oReader.HasRows)
        {
            oReader.Read();
            if (oReader["Status"].ToString() == "0")
            {
                GenerateAuthCode();
                yourComment();
            }
        }
        oReader.Close();

        query = "UPDATE tblVendor SET Status = 1, paymentProof=@paymentProof, DateSubmittedToDnb=getdate() WHERE VendorId=@VendorId";
        using (conn = new SqlConnection(connstring))
        {
            using (cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@VendorId", VendorId);
                cmd.Parameters.AddWithValue("@paymentProof", paymentProof.Value);
                conn.Open(); cmd.ExecuteNonQuery();
            }
        }
        query = "UPDATE tblVendorInformation SET CertAndWarranty_AttachedSigned=@CertAndWarranty_AttachedSigned WHERE VendorId=@VendorId";
        using (conn = new SqlConnection(connstring))
        {
            using (cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@VendorId", VendorId);
                cmd.Parameters.AddWithValue("@CertAndWarranty_AttachedSigned", CertAndWarranty_AttachedSigned.Value);
                conn.Open(); cmd.ExecuteNonQuery();
            }
        }


        clarifyTxt.Text = "<span style='font-size:14px;'>Vendor Information has been submitted.</span>";
        clarifyTxt.ForeColor = Color.Blue;

        string fromName = "", fromEmail = "", toName = "", toEmail = "", AuthenticationTicket = "", VendorName = "";
        //query = "SELECT 'Trans-Asia Admin' as fromName, 'noreply@globetel.com' as fromEmail, t1.FirstName + ' ' + t1.LastName as toName, t1.EmailAdd as toEmail, t4.CompanyName, t4.AuthenticationTicket FROM tblUsers t1, tblUserTypes t2, tblUsers t3, tblVendor t4 WHERE t1.UserId = t2.UserId AND t2.UserType = 12 AND t3.UserId = @UserId AND t4.Status = 1 AND t4.VendorId = @VendorId";
        query = "SELECT '" + ConfigurationManager.AppSettings["AdminEmailName"].ToString() + "' as fromName, '" + ConfigurationManager.AppSettings["AdminNoReplyEmail"].ToString() + "' as fromEmail, t1.FirstName + ' ' + t1.LastName as toName, t1.EmailAdd as toEmail, t4.CompanyName, t4.AuthenticationTicket FROM tblUsers t1, tblUserTypes t2, tblVendor t4 WHERE t1.UserId = t2.UserId AND t1.Status = 1 AND t2.UserType = 12 AND t4.Status = 1 AND t4.VendorId = @VendorId";
        using (conn = new SqlConnection(connstring))
        {
            using (cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@VendorId", VendorId);
                cmd.Parameters.AddWithValue("@UserId", Convert.ToInt32(Session["UserId"]));
                conn.Open(); oReader = cmd.ExecuteReader();
                if (oReader.HasRows)
                {
                    while (oReader.Read())
                    {
                        //fromName = oReader["fromName"].ToString();
                        //fromEmail = oReader["fromEmail"].ToString();
                        fromName = ConfigurationManager.AppSettings["AdminEmailName"].ToString();
                        fromEmail = ConfigurationManager.AppSettings["AdminNoReplyEmail"].ToString();
                        toName = oReader["toName"].ToString();
                        toEmail = oReader["toEmail"].ToString();
                        AuthenticationTicket = oReader["AuthenticationTicket"].ToString();
                        VendorName = oReader["CompanyName"].ToString();
                        SendEmailNotification(fromName, fromEmail, toName, toEmail, AuthenticationTicket, VendorName);
                    }
                }
            }
        }

        string VendorEmail = "";
        query = "SELECT '" + ConfigurationManager.AppSettings["AdminEmailName"].ToString() + "' as fromName, '" + ConfigurationManager.AppSettings["AdminNoReplyEmail"].ToString() + "' as fromEmail, t1.FirstName + ' ' + t1.LastName as toName, t1.EmailAdd as toEmail, t4.CompanyName, t4.AuthenticationTicket, t3.EmailAdd as VendorEmail FROM tblUsers t1, tblUserTypes t2, tblUsers t3, tblVendor t4 WHERE t1.UserId = t2.UserId AND t2.UserType = 12 AND t3.UserId = @UserId AND t4.Status = 1 AND t4.VendorId = @VendorId";
        using (conn = new SqlConnection(connstring))
        {
            using (cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@VendorId", VendorId);
                cmd.Parameters.AddWithValue("@UserId", Convert.ToInt32(Session["UserId"]));
                conn.Open(); oReader = cmd.ExecuteReader();
                if (oReader.HasRows)
                {
                    while (oReader.Read())
                    {
                        //fromName = oReader["fromName"].ToString();
                        //fromEmail = oReader["fromEmail"].ToString();
                        fromName = ConfigurationManager.AppSettings["AdminEmailName"].ToString();
                        fromEmail = ConfigurationManager.AppSettings["AdminNoReplyEmail"].ToString();
                        toName = oReader["toName"].ToString();
                        toEmail = oReader["toEmail"].ToString();
                        AuthenticationTicket = oReader["AuthenticationTicket"].ToString();
                        VendorName = oReader["CompanyName"].ToString();
                        VendorEmail = oReader["VendorEmail"].ToString();
                        //SendEmailNotification(fromName, fromEmail, toName, toEmail, AuthenticationTicket, VendorName);
                    }
                }
            }
        }
        SendEmailNotificationforVendor(fromName, fromEmail, toName, toEmail, AuthenticationTicket, VendorName, VendorEmail);
    }

    void GenerateAuthCode()
    {
        string authTicketTemp = "", authTicketOnDb = "";
        do{
            authTicketTemp = GenerateAuthenticationTicket();
            query = "SELECT AuthenticationTicket FROM tblVendor WHERE AuthenticationTicket=@AuthenticationTicket";
            using (conn = new SqlConnection(connstring))
            {
                using (cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@AuthenticationTicket", authTicketTemp);
                    conn.Open(); oReader = cmd.ExecuteReader();
                    if (oReader.HasRows)
                    {
                        authTicketOnDb = oReader["AuthenticationTicket"].ToString();
                    }
                }
            }
        } while (authTicketOnDb == authTicketTemp);


        query = "UPDATE tblVendor SET authenticationTicket=@authenticationTicket WHERE VendorId = @VendorId";
        using (conn = new SqlConnection(connstring))
        {
            using (cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@VendorId", VendorId);
                cmd.Parameters.AddWithValue("@authenticationTicket", GenerateAuthenticationTicket());
                conn.Open(); cmd.ExecuteNonQuery();
            }
        }
    }


    private string GenerateAuthenticationTicket()
    {
        string strPwdchar = "0123456789";
        string strPwd = "";
        Random rnd = new Random();
        for (int i = 0; i <= 2; i++)
        {
            int iRandom = rnd.Next(0, strPwdchar.Length - 1);
            strPwd += strPwdchar.Substring(iRandom, 1);
        }

        string strPwdchar2 = "ABCDEFGHJKLMNPQRSTUVWXYZ";
        string strPwd2 = "";
        Random rnd2 = new Random();
        for (int i = 0; i <= 2; i++)
        {
            int iRandom2 = rnd.Next(0, strPwdchar2.Length - 1);
            strPwd2 += strPwdchar2.Substring(iRandom2, 1);
        }
        //DateTime DateToday = DateTime.Now;
        //string DateTodayString = String.Format("{0:yyyyMMdd}", DateToday);
        //strPwd = DateTodayString + Session["VendorId"].ToString() + strPwd;
        strPwd = strPwd2 + strPwd;
        return strPwd;
    }


    void yourComment()
    {
        //query = "INSERT INTO tblCommentsDnbClarify (VendorId, UserId, Comment, DateCreated) VALUES (@VendorId, @UserId, @Comment, @DateCreated)";
        //using (conn = new SqlConnection(connstring))
        //{
        //    using (cmd = new SqlCommand(query, conn))
        //    {
        //        cmd.Parameters.AddWithValue("@VendorId", VendorId);
        //        cmd.Parameters.AddWithValue("@UserId", Convert.ToInt32(Session["UserId"]));
        //        cmd.Parameters.AddWithValue("@Comment", Comment.Value);
        //        cmd.Parameters.AddWithValue("@DateCreated", DateTime.Now);
        //        conn.Open(); cmd.ExecuteNonQuery();
        //    }
        //}
    }


    //############################################################
    //############################################################
    // SEND EMAIL NOTIFICATION TO DNB
    private bool SendEmailNotification(string sfromName, string sfromEmail, string stoName, string stoEmail, string sAuthenticationTicket, string sVendorName)
    {
        bool success = false;

        string from = sfromName + "<" + sfromEmail + ">";
        string to = stoName + "<" + stoEmail + ">";
        string subject = "";

        try
        {
            //subject = "Trans-Asia VMS Accreditation application posted for authentication -- "+sVendorName;
            subject = "Vendor Accreditation: For Authentication - " + sVendorName + "";
            if (!MailHelper.SendEmail(MailTemplate.GetDefaultSMTPServer(),
                    from,
                    to,
                    subject,
                    CreateNotificationBody(sfromName, stoName, sAuthenticationTicket, sVendorName),
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
            //Response.Write(ex.ToString());
        }
        return success;
    }

    private string CreateNotificationBody(string cfromName, string ctoName, string cAuthenticationNumber, string cVendorName)
    {
        StringBuilder sb = new StringBuilder();
        string sTxt = "<table border='0' cellpadding='5' style='font-size:12px'>";
        sTxt = sTxt + "<tr>";
        sTxt = sTxt + "<td><strong>&nbsp;Vendor ID</strong></td>";
        sTxt = sTxt + "<td>&nbsp;" + Session["VendorId"] + "&nbsp;</td>";
        sTxt = sTxt + "</tr>";
        sTxt = sTxt + "<tr>";
        sTxt = sTxt + "<td><strong>&nbsp;Company Name</strong></td>";
        sTxt = sTxt + "<td>&nbsp;" + cVendorName + "&nbsp;</td>";
        sTxt = sTxt + "</tr>";
        sTxt = sTxt + "<tr>";
        sTxt = sTxt + "<td><strong>&nbsp;Authentication Ticket</strong></td>";
        sTxt = sTxt + "<td>&nbsp;" + cAuthenticationNumber + "&nbsp;</td>";
        sTxt = sTxt + "</tr>";
        sTxt = sTxt + "</table>";

        //sb.Append("<tr><td><p>Sent: " + DateTime.Now.ToLongDateString() + "<br>From: " + cfromName + "<br> To: " + ctoName + "<br><br> Good day!<br><br> This is to inform you that application for vendor accreditation has been posted for your approval.<br></p><br>" + sTxt + "<p>Very truly yours,<br><br><br> <strong>" + cfromName + "</strong></p><p>&nbsp;</p> <span style='font-size:10px; font-style:italic;'>Please do not reply to this auto-generated  message.&nbsp;</span></td></tr>");
        sb.Append("<tr><td>");
        sb.Append("<p>");
        sb.Append("Sent: " + DateTime.Now.ToLongDateString() + "<br>From: " + cfromName + "<br>");
        sb.Append("To: " + ctoName + "<br><br>");
        sb.Append("</p>");
        sb.Append("<tr><td>");
        sb.Append("<p>");
        sb.Append("Dear " + ctoName + ":<br><br>");
        sb.Append("Re: Request for Vendor Accreditation Approval -- " + cVendorName + "<br><br>");
        sb.Append("This is to request for your Approval of the ff: <br><br>");
        //sb.Append("<a href='http://'<br><br>");
        sb.Append(sTxt);
        sb.Append("</p><br><br>");
        //sb.Append("We are happy to be doing business with you. Thank you and God bless your dealings.<br><br><br>");
        sb.Append("Very truly yours,<br><br>");
        sb.Append("Trans-Asia<br><br>");
        sb.Append("</td></tr>");
        sb.Append("<tr><td>");
        sb.Append("<p>&nbsp;</p>");
        sb.Append("<b>Instructions:</b><br>");
        sb.Append("&nbsp;&nbsp;1. Go to <a href='" + System.Configuration.ConfigurationManager.AppSettings["ServerUrl"] + "' target='_blank'>" + System.Configuration.ConfigurationManager.AppSettings["ServerUrl"] + "</a><br>");
        sb.Append("&nbsp;&nbsp;2. Enter your Username and Password then click Login<br>");
        sb.Append("&nbsp;&nbsp;3. Click Vendors for Authentication<br>");
        sb.Append("&nbsp;&nbsp;4. Click Authenticate<br>");
        sb.Append("</td></tr>");
        sb.Append("<tr><td>");
        sb.Append("<p>&nbsp;</p><span style='font-size:10px; font-style:italic;'>Please do not reply to this auto-generated  message.&nbsp;</span>");
        sb.Append("</td></tr>");

        return MailTemplate.IntegrateBodyIntoTemplate(sb.ToString());
    }



    //############################################################
    //############################################################
    // SEND EMAIL NOTIFICATION TO VENDOR
    private bool SendEmailNotificationforVendor(string sfromName, string sfromEmail, string stoName, string stoEmail, string sAuthenticationTicket, string sVendorName, string sVendorEmail)
    {
        bool success = false;

        //stoName = sVendorName;
        //stoEmail = sVendorEmail;
        string to = sVendorName + "<" + sVendorEmail + ">";

        //sfromEmail = sfromEmail;
        //sfromName = sfromName;
        string from = sfromName + "<" + sfromEmail + ">";
        string subject = "";

        try
        {
            subject = "Trans-Asia VMS Accreditation application for approval process.";
            if (!MailHelper.SendEmail(MailTemplate.GetDefaultSMTPServer(),
                    from,
                    to,
                    subject,
                    CreateNotificationBodyforVendor(from, to, sAuthenticationTicket, sVendorName),
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
            //Response.Write(ex.ToString());
        }
        return success;
    }

    private string CreateNotificationBodyforVendor(string cfromName, string ctoName, string cAuthenticationNumber, string cVendorName)
    {
        StringBuilder sb = new StringBuilder();
        string sTxt = "<table border='0' cellpadding='5' style='font-size:12px'>";
        sTxt = sTxt + "<tr>";
        sTxt = sTxt + "<td><strong>&nbsp;Vendor ID</strong></td>";
        sTxt = sTxt + "<td>&nbsp;" + Session["VendorId"] + "&nbsp;</td>";
        sTxt = sTxt + "</tr>";
        sTxt = sTxt + "<tr>";
        sTxt = sTxt + "<td><strong>&nbsp;Company Name</strong></td>";
        sTxt = sTxt + "<td>&nbsp;" + cVendorName + "&nbsp;</td>";
        sTxt = sTxt + "</tr>";
        sTxt = sTxt + "<tr>";
        sTxt = sTxt + "<td><strong>&nbsp;Authentication Ticket</strong></td>";
        sTxt = sTxt + "<td>&nbsp;" + cAuthenticationNumber + "&nbsp;</td>";
        sTxt = sTxt + "</tr>";
        sTxt = sTxt + "</table>";

        sb.Append("<tr><td><p>Sent: " + DateTime.Now.ToLongDateString() + "<br>From: " + cfromName + "<br> To: " + cVendorName + "<br><br> Good day!<br><br> Your application for accreditation will now undergo approval processes. You will be notified once approved or rejected.<br></p><br>" + sTxt + "<p>Very truly yours,<br><br><br> <strong>Trans-Asia</strong></p><p>&nbsp;</p> <span style='font-size:10px; font-style:italic;'>Please do not reply to this auto-generated  message.&nbsp;</span></td></tr>");

        return MailTemplate.IntegrateBodyIntoTemplate(sb.ToString());
    }
}