using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using Ava.lib;
using Ava.lib.constant;
using System.Text;
using System.Text.RegularExpressions;

public partial class vmofficer_VendorDetails : System.Web.UI.Page
{
    SqlDataReader oReader;
    string connstring = ConfigurationManager.ConnectionStrings["AVAConnectionString"].ConnectionString;
    string query;
    SqlCommand cmd;
    SqlConnection conn;
    bool UserNameExists;
    //bool VendorCompanyNameExists;
    string sCommand;

    protected void TestShowAllSessions()
    {  //test show all session
        string str = null;
        foreach (string key in HttpContext.Current.Session.Keys)
        { str += string.Format("<b>{0}</b>: {1};  ", key, HttpContext.Current.Session[key].ToString()); }
        Response.Write("<span style='font-size:12px'>" + str + "</span>");
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //TestShowAllSessions();
        if (Session["UserId"] == null) Response.Redirect("login.aspx");
        if (Session["SESSION_USERTYPE"].ToString() != ((int)Constant.USERTYPE.VMOFFICER).ToString()) Response.Redirect("login.aspx");

        if (IsPostBack)
        {
            SaveToDB();
            PopulateFields();
        }
        else 
        {
            string status1 = "";
            SqlDataReader oReader;
            query = "SELECT Status FROM tblVendorApplicants WHERE ID=@ID";
            using (conn = new SqlConnection(connstring))
            {
                using (cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ID", Convert.ToInt32(Session["VendorApplicantId"].ToString()));
                    conn.Open(); oReader = cmd.ExecuteReader();
                    if (oReader.HasRows)
                    {
                        while (oReader.Read())
                        {
                            status1 = oReader["Status"].ToString();
                        }
                    }
                }
            }
            
            if (status1 == "1" || status1 == "0")
            {
                PreCreateUserAcct();
                backBt.Visible = false;
            }
            else 
            {
                PopulateFields();
                createBt.Visible = false;
                cancelBt.Visible = false; 
                rejectBt.Visible = false;
            }
        }

    }


    void PreCreateUserAcct()
    {
        SqlDataReader oReader;
        query = "SELECT * FROM tblVendorApplicants WHERE ID=@ID";
        using (conn = new SqlConnection(connstring))
        {
            using (cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@ID", Convert.ToInt32(Session["VendorApplicantId"].ToString()));
                conn.Open(); oReader = cmd.ExecuteReader();
                if (oReader.HasRows)
                {
                    while (oReader.Read())
                    {
                        DateTime newDateT = Convert.ToDateTime(oReader["DateCreated"].ToString());
                        String newDateT_to_string = newDateT.ToString("Mdyyhm");
                        String tmpUserName = oReader["CompanyName"].ToString().ToLower().Replace(" ", "");
                        if (tmpUserName.Length > 9) { tmpUserName = tmpUserName.Remove(9); }

                        UserName.Value = tmpUserName + "_" + newDateT_to_string;
                        UserPassword.Value = oReader["CompanyName"].ToString().ToLower().Replace(" ", "");
                        CompanyName.Value = oReader["CompanyName"].ToString();
                        EmailAdd.Value = oReader["EmailAdd"].ToString();
                    }
                }
            }
        }
    }


    void PopulateFields()
    {
        SqlDataReader oReader;
        query = "SELECT * FROM tblUsers WHERE (EmailAdd=EmailAdd AND CompanyName=@CompanyName) AND UserName=@UserName";
        using (conn = new SqlConnection(connstring))
        {
            using (cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@EmailAdd", Session["VendorEmail"].ToString());
                cmd.Parameters.AddWithValue("@CompanyName", Session["VendorCompany"].ToString());
                cmd.Parameters.AddWithValue("@UserName", UserName.Value.Trim());
                conn.Open(); oReader = cmd.ExecuteReader();
                if (oReader.HasRows)
                {
                    while (oReader.Read())
                    {
                        UserName.Value = oReader["UserName"].ToString();
                        UserPassword.Value = EncryptionHelper.Decrypt(oReader["UserPassword"].ToString());
                        CompanyName.Value = oReader["CompanyName"].ToString();
                        EmailAdd.Value = oReader["EmailAdd"].ToString();
                        UserName.Value = oReader["UserName"].ToString();
                        FirstName.Value = oReader["FirstName"].ToString();
                        MiddleName.Value = oReader["MiddleName"].ToString();
                        LastName.Value = oReader["LastName"].ToString();
                        UserName.Disabled = true;
                        UserPassword.Disabled = true;
                        CompanyName.Disabled = true;
                        EmailAdd.Disabled = true;
                        UserName.Disabled = true;
                        FirstName.Disabled = true;
                        MiddleName.Disabled = true;
                        LastName.Disabled = true;
                        //EndorsedBy.Disabled = true;
                        EndorsedTo.Enabled = false;


                    }
                }
            }
        }
    }

    protected void DetailsView1_OnDataBound(object sender, EventArgs e)
    {
        if (DetailsView1.Rows.Count > 0)
        {
            string DateStarted1 = DetailsView1.Rows[1].Cells[1].Text.ToString() != "" ? DetailsView1.Rows[1].Cells[1].Text.ToString() : "";
            //DetailsView1.Rows[1].Cells[1].Text = DateStarted1 != "1/1/1900 12:00:00 AM"  || DateStarted1 != "" ? string.Format("{0:MM/dd/yyyy}", Convert.ToDateTime(DateStarted1)) : "";
            Response.Write(DetailsView1.Rows[4].Cells[1].Text.ToString());
            string FinancialStatement1 = DetailsView1.Rows[2].Cells[1].Text;
            if (FinancialStatement1.ToString() == "Yes")
            {
                DetailsView1.Rows[2].Cells[1].Text = "Covers 12 months";
            }
            else
            {
                DetailsView1.Rows[2].Cells[1].Text = "Covers less than 12 months, or no FS available";
            }

            string coreBusiness1 = DetailsView1.Rows[5].Cells[1].Text + ";";
            DetailsView1.Rows[5].Cells[1].Text = "";
            string[] coreBusiness_arr = Regex.Split(coreBusiness1, ";");
            foreach (string coreBusiness in coreBusiness_arr)
            {
                if (coreBusiness != "")
                {
                    query = "IF EXISTS(select 1 from tblVendorProductsAndServices t1, tblVendor t2 where t1.VendorId = t2.VendorId and t2.Status = 6 and CategoryId = @CategoryId) BEGIN SELECT count(*) AS totalUsers, ta.CategoryId, tb.CategoryName FROM (select t1.VendorId, t1.CategoryId from tblVendorProductsAndServices t1, tblVendor t2 where t1.VendorId = t2.VendorId and t2.Status = 6 and CategoryId = @CategoryId) ta, rfcProductCategory tb WHERE tb.CategoryId=ta.CategoryId GROUP BY ta.CategoryId, tb.CategoryName END ELSE BEGIN SELECT 0 AS totalUsers, ta.CategoryId, tb.CategoryName FROM (select 0 as VendorId, @CategoryId as CategoryId from rfcProductCategory t1 where t1.CategoryId = @CategoryId) ta, rfcProductCategory tb WHERE tb.CategoryId=ta.CategoryId GROUP BY ta.CategoryId, tb.CategoryName END";
                    using (conn = new SqlConnection(connstring))
                    {
                        using (cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@CategoryId", coreBusiness.Trim());
                            conn.Open(); oReader = cmd.ExecuteReader();
                            if (oReader.HasRows)
                            {
                                while (oReader.Read())
                                {
                                    DetailsView1.Rows[5].Cells[1].Text = DetailsView1.Rows[5].Cells[1].Text + "[" + oReader["totalUsers"].ToString() + "] " + oReader["CategoryName"].ToString() + "<br>";
                                }
                            }
                            else
                            {
                                DetailsView1.Rows[5].Cells[1].Text = DetailsView1.Rows[5].Cells[1].Text + "[0] " + coreBusiness + "<br>";
                            }
                        }
                    }
                }
            }
        }
    }

    void SaveToDB() 
    {
        string control1 = Request.Form["__EVENTTARGET"];
        if (control1 == "createBt")
        {
            string connstring = ConfigurationManager.ConnectionStrings["AVAConnectionString"].ConnectionString;
            //string sCommand = "";

            query = "SELECT UserName FROM tblUsers WHERE UserName = @UserName OR (EmailAdd=@EmailAdd AND CompanyName=@CompanyName)";
            using (conn = new SqlConnection(connstring))
            {
                using (cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    cmd.Parameters.AddWithValue("@UserName", UserName.Value.Trim());
                    cmd.Parameters.AddWithValue("@EmailAdd", EmailAdd.Value.Trim());
                    cmd.Parameters.AddWithValue("@CompanyName", CompanyName.Value.Trim());
                    oReader = cmd.ExecuteReader();
                    if (oReader.HasRows)
                    {
                        UserNameExists = true;
                    }
                    else
                    {
                        UserNameExists = false;
                    }
                    conn.Close();
                }
            }

            if (UserPassword.Value == "")
            {
                UserNameExists = true;
                errNotification.Text = errNotification.Text + "Invalid Password </br>";
            }

            if (!UserNameExists)
            {
                query = "INSERT INTO tblCommentsProcurement (VendorApplicantId, UserId, Comment, DateCreated) VALUES (@VendorApplicantId, @UserId, @Comment, @DateCreated)";
                //query = "sp_GetVendorInformation"; //##storedProcedure
                using (conn = new SqlConnection(connstring))
                {
                    using (cmd = new SqlCommand(query, conn))
                    {
                        //cmd.CommandType = CommandType.StoredProcedure; //##storedProcedure
                        cmd.Parameters.AddWithValue("@VendorApplicantId", Session["VendorApplicantId"]);
                        cmd.Parameters.AddWithValue("@UserId", Convert.ToInt32(Session["UserId"]));
                        cmd.Parameters.AddWithValue("@Comment", Comment.Value.ToString());
                        cmd.Parameters.AddWithValue("@DateCreated", DateTime.Now);
                        conn.Open(); cmd.ExecuteNonQuery();
                    }
                }


                //CREATE THE VENDOR
                query = "IF NOT EXISTS (SELECT 1 FROM tblVendor WHERE CompanyName = @CompanyName) BEGIN INSERT INTO tblVendor (CompanyName, Status, approvedbyProc, approvedbyProcDate, EndorsedTo) VALUES (@CompanyName, 0, @approvedbyProc, getdate(), @EndorsedTo) END";
                using (conn = new SqlConnection(connstring))
                {
                    using (cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@CompanyName", CompanyName.Value.Trim());
                        cmd.Parameters.AddWithValue("@approvedbyProc", Convert.ToInt32(Session["UserId"]));
                        cmd.Parameters.AddWithValue("@EndorsedTo", Convert.ToInt32(EndorsedTo.SelectedValue.Trim()));
                        conn.Open(); cmd.ExecuteNonQuery(); conn.Close();
                    }
                }


                //CREATE THE VENDOR INFORMATION
                query = "IF NOT EXISTS (SELECT 1 FROM tblVendorInformation WHERE CompanyName = @CompanyName) BEGIN INSERT INTO tblVendorInformation (VendorId, VendorCode, CompanyName) SELECT VendorId, CompanyName, CompanyName FROM tblVendor WHERE CompanyName = @CompanyName END";
                using (conn = new SqlConnection(connstring))
                {
                    using (cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@CompanyName", CompanyName.Value.Trim());
                        conn.Open(); cmd.ExecuteNonQuery(); conn.Close();
                    }
                }

                //CREATE THE USER
                query = "IF NOT EXISTS (SELECT 1 FROM tblUsers WHERE UserName = @UserName OR (EmailAdd=@EmailAdd AND CompanyName=@CompanyName)) BEGIN INSERT INTO tblUsers (UserName, UserPassword, Status, FirstName, MiddleName, LastName, EmailAdd, CompanyName) VALUES (@UserName, @UserPassword, @Status, @FirstName, @MiddleName, @LastName, @EmailAdd, @CompanyName) END";
                using (conn = new SqlConnection(connstring))
                {
                    using (cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@UserName", UserName.Value.Trim());
                        cmd.Parameters.AddWithValue("@UserPassword", EncryptionHelper.Encrypt(UserPassword.Value.Trim()));
                        cmd.Parameters.AddWithValue("@Status", 1);
                        cmd.Parameters.AddWithValue("@FirstName", FirstName.Value.Trim());
                        cmd.Parameters.AddWithValue("@MiddleName", MiddleName.Value.Trim());
                        cmd.Parameters.AddWithValue("@LastName", LastName.Value.Trim());
                        cmd.Parameters.AddWithValue("@EmailAdd", EmailAdd.Value.Trim());
                        cmd.Parameters.AddWithValue("@CompanyName", CompanyName.Value.Trim());
                        conn.Open(); cmd.ExecuteNonQuery(); conn.Close();
                    }
                }

                //ASSIGN THIS USER TO THE VENDOR
                query = "INSERT INTO tblUsersForVendors (UserId, VendorId) SELECT t1.UserId, t2.VendorId FROM tblUsers t1, tblVendor t2 WHERE t1.UserName = @UserName AND t2.CompanyName=@CompanyName";
                using (conn = new SqlConnection(connstring))
                {
                    using (cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@UserName", UserName.Value.Trim());
                        cmd.Parameters.AddWithValue("@CompanyName", CompanyName.Value.Trim());
                        conn.Open(); cmd.ExecuteNonQuery(); conn.Close();
                    }
                }

                //CREATE THE USERTYPE
                sCommand = "IF NOT EXISTS (SELECT 1 FROM tblUserTypes t1, tblUsers t2 WHERE t1.UserId = t2.UserId AND t2.UserName = '" + UserName.Value.Trim() + "') BEGIN INSERT INTO tblUserTypes (UserID, UserType, DateCreated) SELECT UserId, 11, getdate() FROM tblUsers WHERE UserName = '" + UserName.Value.Trim() + "' END";
                SqlHelper.ExecuteNonQuery(connstring, CommandType.Text, sCommand);

                //CHANGE STATUS OF APPLICANTS TO ENDORSE Status 2

                query = "UPDATE tblVendorApplicants SET Status = 2, ApprovedBy=@ApprovedBy, ApprovedDt=getdate(), EndorsedTo=@EndorsedTo  WHERE ID=@ID";
                using (conn = new SqlConnection(connstring))
                {
                    using (cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@ID", Convert.ToInt32(Session["VendorApplicantId"].ToString()));
                        cmd.Parameters.AddWithValue("@ApprovedBy", Convert.ToInt32(Session["UserId"]));
                        cmd.Parameters.AddWithValue("@EndorsedTo", Convert.ToInt32(EndorsedTo.SelectedValue.Trim()));
                        conn.Open(); cmd.ExecuteNonQuery(); conn.Close();
                    }
                }

                string fromName = "", fromEmail = "", toName = "", toEmail = "", VendorEmail = "", VendorName = "", VendorUserName = "", VendorPassword = "";
                query = "SELECT t3.CompanyName as toName, t3.EmailAdd as toEmail, '" + ConfigurationManager.AppSettings["AdminEmailName"].ToString() + "' as fromName, '" + ConfigurationManager.AppSettings["AdminNoReplyEmail"].ToString() + "' as fromEmail, t3.CompanyName, t3.EmailAdd FROM tblVendorApplicants t3 WHERE t3.Status = 2 AND t3.ID=@ID";
                //query = "sp_GetVendorInformation"; //##storedProcedure
                using (conn = new SqlConnection(connstring))
                {
                    using (cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@ID", Convert.ToInt32(Session["VendorApplicantId"].ToString()));
                        conn.Open(); oReader = cmd.ExecuteReader();
                        if (oReader.HasRows)
                        {
                            while (oReader.Read())
                            {
                                fromName = oReader["fromName"].ToString();
                                fromEmail = oReader["fromEmail"].ToString();
                                toName = oReader["CompanyName"].ToString();
                                toEmail = oReader["EmailAdd"].ToString();
                                VendorEmail = oReader["EmailAdd"].ToString();
                                VendorName = oReader["CompanyName"].ToString();
                                VendorUserName = UserName.Value;
                                VendorPassword = UserPassword.Value;
                                SendEmailNotification(fromName, fromEmail, toName, toEmail, VendorEmail, VendorName, VendorUserName, VendorPassword);
                            }
                        }
                    }
                }


                UserName.Disabled = true;
                UserPassword.Disabled = true;
                CompanyName.Disabled = true;
                EmailAdd.Disabled = true;
                UserName.Disabled = true;
                FirstName.Disabled = true;
                MiddleName.Disabled = true;
                LastName.Disabled = true;
                errNotification.Text = "User approved and created";
                errNotification.ForeColor = Color.Blue;
                rejectBt.Visible = false;
                createBt.Visible = false;

            }
            else
            {
                errNotification.Text = errNotification.Text + "Username or email already exists </br>"; 
                errNotification.ForeColor = Color.Red;
            }
        }
        else if (control1 == "rejectBt")
        {

                query = "INSERT INTO tblCommentsProcurement (VendorApplicantId, UserId, Comment, DateCreated) VALUES (@VendorApplicantId, @UserId, @Comment, @DateCreated)";
                using (conn = new SqlConnection(connstring))
                {
                    using (cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@VendorApplicantId", Convert.ToInt32(Session["VendorApplicantId"]));
                        cmd.Parameters.AddWithValue("@UserId", Convert.ToInt32(Session["UserId"]));
                        cmd.Parameters.AddWithValue("@Comment", Comment.Value.ToString());
                        cmd.Parameters.AddWithValue("@DateCreated", DateTime.Now);
                        conn.Open(); cmd.ExecuteNonQuery();
                    }
                }
            //CHANGE STATUS OF APPLICANTS TO REJECT Status 3
            //sCommand = "UPDATE tblVendorApplicants SET Status = 3 WHERE EmailAdd = '" + Session["VendorEmail"].ToString() + "'";
            //SqlHelper.ExecuteNonQuery(connstring, CommandType.Text, sCommand);
                query = "UPDATE tblVendorApplicants SET Status = 3, RejectedBy=@RejectedBy, RejectedDt=getdate(), EndorsedTo=@EndorsedTo WHERE ID = @ID";
            using (conn = new SqlConnection(connstring))
            {
                using (cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ID", Convert.ToInt32(Session["VendorApplicantId"]));
                    cmd.Parameters.AddWithValue("@RejectedBy", Convert.ToInt32(Session["UserId"]));
                    cmd.Parameters.AddWithValue("@EndorsedTo", Convert.ToInt32(EndorsedTo.SelectedValue.Trim()));
                    conn.Open(); cmd.ExecuteNonQuery();
                }
            }


            string fromName = "", fromEmail = "", toName = "", toEmail = "", VendorEmail = "", VendorName = "";
            query = "SELECT t3.CompanyName as toName, t3.EmailAdd as toEmail, '" + ConfigurationManager.AppSettings["AdminEmailName"].ToString() + "' as fromName, '" + ConfigurationManager.AppSettings["AdminNoReplyEmail"].ToString() + "' as fromEmail, t3.CompanyName, t3.EmailAdd FROM tblVendorApplicants t3 WHERE t3.Status = 3 AND t3.ID = @ID";
            using (conn = new SqlConnection(connstring))
            {
                using (cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ID", Convert.ToInt32(Session["VendorApplicantId"]));
                    conn.Open(); oReader = cmd.ExecuteReader();
                    if (oReader.HasRows)
                    {
                        while (oReader.Read())
                        {
                            //fromName = oReader["fromName"].ToString();
                            //fromEmail = oReader["fromEmail"].ToString();
                            fromName = ConfigurationManager.AppSettings["AdminEmailName"].ToString();
                            fromEmail = ConfigurationManager.AppSettings["AdminNoReplyEmail"].ToString();
                            toName = oReader["CompanyName"].ToString();
                            toEmail = oReader["EmailAdd"].ToString();
                            VendorEmail = oReader["EmailAdd"].ToString();
                            VendorName = oReader["CompanyName"].ToString();
                            SendEmailNotificationReject(fromName, fromEmail, toName, toEmail, VendorEmail, VendorName);
                        }
                    }
                }
            }

            UserName.Disabled = true;
            UserPassword.Disabled = true;
            CompanyName.Disabled = true;
            EmailAdd.Disabled = true;
            UserName.Disabled = true;
            FirstName.Disabled = true;
            MiddleName.Disabled = true;
            LastName.Disabled = true;
            errNotification.Text = "Application has been rejected.";
            errNotification.ForeColor = Color.Red;
            rejectBt.Visible = false;
            createBt.Visible = false;
        }

        Comment.Visible = false;
        DetailsView1.DataBind();
        repeaterCommentsProc.DataBind();
    }




    //############################################################
    //############################################################
    // SEND EMAIL NOTIFICATION TO VENDOR
    private bool SendEmailNotification(string sfromName, string sfromEmail, string stoName, string stoEmail, string sVendorEmail, string sVendorName, string sUserName, string sUserPassword)
    {
        bool success = false;

        string from = sfromName + "<" + sfromEmail + ">";
        string to = stoName + "<" + stoEmail + ">";
        string subject = "";

        try
        {
            subject = "Trans-Asia VMS Accreditation application has been activated.";
            if (!MailHelper.SendEmail(MailTemplate.GetDefaultSMTPServer(),
                    from,
                    to,
                    subject,
                    CreateNotificationBody(sfromName, stoName, sVendorEmail, sVendorName, sUserName, sUserPassword),
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

    private string CreateNotificationBody(string cfromName, string ctoName, string cVendorEmail, string cVendorName, string cUserName, string cPassword)
    {
        StringBuilder sb = new StringBuilder();
        string sTxt = "";
        //sTxt = sTxt + "<table border='0' style='font-size:12px' cellpadding='5'>";
        //sTxt = sTxt + "<tr>";
        //sTxt = sTxt + "<td><strong>&nbsp;Company Name</strong></td>";
        //sTxt = sTxt + "<td>&nbsp;" + cVendorName + "&nbsp;</td>";
        //sTxt = sTxt + "</tr>";
        //sTxt = sTxt + "<tr>";
        //sTxt = sTxt + "<td><strong>&nbsp;Vendor Email</strong></td>";
        //sTxt = sTxt + "<td>&nbsp;" + cVendorEmail + "&nbsp;</td>";
        //sTxt = sTxt + "</tr>";
        //sTxt = sTxt + "</table>";
        sTxt = sTxt + "<table border='0' cellpadding='5' style='font-size:12px'>";
        sTxt = sTxt + "<tr>";
        sTxt = sTxt + "<td><strong>&nbsp;Login</strong></td>";
        sTxt = sTxt + "<td>&nbsp;<a href='" + System.Configuration.ConfigurationManager.AppSettings["ServerUrl"] + "' target='_blank'>" + System.Configuration.ConfigurationManager.AppSettings["ServerUrl"] + "</a>&nbsp;</td>";
        sTxt = sTxt + "</tr>";
        sTxt = sTxt + "<tr>";
        sTxt = sTxt + "<td><strong>&nbsp;User Name</strong></td>";
        sTxt = sTxt + "<td>&nbsp;" + cUserName + "&nbsp;</td>";
        sTxt = sTxt + "</tr>";
        sTxt = sTxt + "<tr>";
        sTxt = sTxt + "<td><strong>&nbsp;User Password</strong></td>";
        sTxt = sTxt + "<td>&nbsp;" + cPassword + "&nbsp;</td>";
        sTxt = sTxt + "</tr>";
        sTxt = sTxt + "</table>";


        sb.Append("<tr><td>");
            sb.Append("<p>");
            sb.Append("Sent: " + DateTime.Now.ToLongDateString() + "<br>From: " + cfromName + "<br>");
            sb.Append("To: " + ctoName + " &lt;" + cVendorEmail + "&gt;<br><br>");
            sb.Append("</p>");
        sb.Append("<tr><td>");
            sb.Append("<p>");
            sb.Append("Dear " + ctoName + ":<br><br>");
            sb.Append("This email is to inform you that we have received your letter of intent for accreditation. Your request has been approved.<br><br>");
            sb.Append("Please access the link below using your username and password to start your application for Trans-Asia accreditation. <br><br>");
            //sb.Append("<a href='http://'<br><br>");
            sb.Append(sTxt);
            sb.Append("</p>");
            sb.Append("We are happy to be doing business with you. Thank you and God bless your dealings.<br><br><br>");
            sb.Append("Sincerely,<br><br>");
            sb.Append("Trans-Asia<br><br>");
        sb.Append("</td></tr>");
        sb.Append("<tr><td>");
            sb.Append("<p>&nbsp;</p><span style='font-size:10px; font-style:italic;'>Please do not reply to this auto-generated  message.&nbsp;</span>");
        sb.Append("</td></tr>");

        return MailTemplate.IntegrateBodyIntoTemplate(sb.ToString());
    }


    //############################################################
    //############################################################
    // SEND EMAIL NOTIFICATION TO VENDOR FOR REJECT
    private bool SendEmailNotificationReject(string sfromName, string sfromEmail, string stoName, string stoEmail, string sVendorEmail, string sVendorName)
    {
        bool success = false;

        string from = sfromName + "<" + sfromEmail + ">";
        string to = stoName + "<" + stoEmail + ">";
        string subject = "";

        try
        {
            subject = "Trans-Asia VMS Accreditation application rejected.";
            if (!MailHelper.SendEmail(MailTemplate.GetDefaultSMTPServer(),
                    from,
                    to,
                    subject,
                    CreateNotificationBodyReject(sfromName, stoName, sVendorEmail, sVendorName),
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

    private string CreateNotificationBodyReject(string cfromName, string ctoName, string cVendorEmail, string cVendorName)
    {
        StringBuilder sb = new StringBuilder();
        string sTxt = "<table border='0' cellpadding='5' style='font-size:12px'>";
        sTxt = sTxt + "<tr>";
        sTxt = sTxt + "<td><strong>&nbsp;Company Name</strong></td>";
        sTxt = sTxt + "<td>&nbsp;" + cVendorName + "&nbsp;</td>";
        sTxt = sTxt + "</tr>";
        sTxt = sTxt + "<tr>";
        sTxt = sTxt + "<td><strong>&nbsp;Vendor Email</strong></td>";
        sTxt = sTxt + "<td>&nbsp;" + cVendorEmail + "&nbsp;</td>";
        sTxt = sTxt + "</tr>";
        sTxt = sTxt + "</table>";

        sb.Append("<tr><td>");
        sb.Append("<p>");
        sb.Append("Sent: " + DateTime.Now.ToLongDateString() + "<br>From: " + cfromName + "<br>");
        sb.Append("To: " + ctoName + " &lt;" + cVendorEmail + "&gt;<br><br>");
        sb.Append("</p>");
        sb.Append("<tr><td>");
        sb.Append("<p>");
        sb.Append("Dear " + ctoName + ":<br><br>");
        sb.Append("This email is to acknowledge receipt of your Letter of Intent for accreditation.<br><br>We regret to inform you that your application did not pass Trans-Asia's pre-screening conditions at this time.<br><br>Thank you.<br><br>");
        //sb.Append("This email is to inform you that we have received your letter of intent for accreditation. We regret to inform you that your application for accreditation has been denied.<br><br>Thank you.<br><br>");
        //sb.Append("Please access the link below using your username and password to start your application for Trans-Asia accreditation. <br><br>");
        //sb.Append("<a href='http://'<br><br>");
        sb.Append(sTxt);
        sb.Append("</p>");
        sb.Append("<br><br><br>");
        sb.Append("Sincerely,<br><br>");
        sb.Append("Trans-Asia<br><br>");
        sb.Append("</td></tr>");
        sb.Append("<tr><td>");
        sb.Append("<p>&nbsp;</p><span style='font-size:10px; font-style:italic;'>Please do not reply to this auto-generated  message.&nbsp;</span>");
        sb.Append("</td></tr>");
        

        return MailTemplate.IntegrateBodyIntoTemplate(sb.ToString());
    }
}