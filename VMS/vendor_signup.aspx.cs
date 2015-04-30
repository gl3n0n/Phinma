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

public partial class vendor_signup : System.Web.UI.Page
{
    SqlDataReader oReader;
    string connstring = ConfigurationManager.ConnectionStrings["AVAConnectionString"].ConnectionString;
    string query;
    SqlCommand cmd;
    SqlConnection conn;
    bool applicantExists;
    string sCommand;
    int numRowsTbl;

    protected void TestShowAllSessions()
    {  //test show all session
        string str = null;
        foreach (string key in HttpContext.Current.Session.Keys)
        { str += string.Format("<b>{0}</b>: {1};  ", key, HttpContext.Current.Session[key].ToString()); }
        Response.Write("<span style='font-size:12px'>" + str + "</span>");
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        
        //if (Session["CaptchaImageText"].ToString()!="") { } else { Response.Redirect("vendor_signup.aspx"); }
        dsSelectedCategory.SelectCommand = "Select '0' as CategoryId, '--Selected' as Categoryname";
        repeaterSelectedCategory.DataBind();
        //TestShowAllSessions();
        if (IsPostBack)
        {
            string queries="";
	    numRowsTbl = Convert.ToInt32(Request.Form["CategoryCounter"].Trim().ToString());
            for (int i = 1; i <= numRowsTbl; i++)
            {
                //Response.Write(Request.Form["CategoryId" + i].ToString()+"<br>");
                queries = queries + "'" + Request.Form["CategoryId" + i].Trim().ToString() + "'";

                if (numRowsTbl > 0 && i < numRowsTbl)
                {
                    queries = queries + ", ";
                }
            }

            if (queries == "'0'" || queries == "")
            {
                dsSelectedCategory.SelectCommand = "Select '0' as CategoryId, '--Selected' as Categoryname";
            }
            else
            {
                dsSelectedCategory.SelectCommand = "SELECT CategoryId, CategoryName FROM rfcProductCategory WHERE CategoryId in (" + queries + ")";
            }
            repeaterSelectedCategory.DataBind();
            //Response.Write("SELECT CategoryId, CategoryName FROM rfcProductCategory WHERE CategoryId in ("+queries+")");
            
            string control1 = Request.Form["__EVENTTARGET"];
            bool isValid = true;
            errNotification.Text = "";
           // fileuploaded1.Text = "<a href='" + LOIFileName.Value + "'>" + LOIFileName.Value + "</a>";

            if (LOIFileName.Value != "")
            {
                fileuploaded1.Text = "<a href='" + LOIFileName.Value + "' target='_blank'>Attached file</a> <img src=\"images/xicon.png\" style=\"margin-left: 10px; padding-top: 5px;\" id=\"LOIFileNamex\" onclick=\"$('#ContentPlaceHolder1_LOIFileName').val('');$('#ContentPlaceHolder1_fileuploaded1').html('Attach file');$(this).hide();\">";
            }
            else
            {
                fileuploaded1.Text = "Attach file";
            }


            if (this.Session["CaptchaImageText"]==null)
            {
                Response.Redirect("vendor_signup.aspx");
            }
            if (!IsEmail(EmailAdd.Value.Trim()) || CompanyName.Value.Trim() == "" ||  !isValid || this.txtimgcode.Text.ToLower().Trim() != this.Session["CaptchaImageText"].ToString().ToLower() || (queries == "'0'" || queries == ""))
            {
                if (!IsEmail(EmailAdd.Value))
                {
                    errNotification.Text = errNotification.Text + "Invalid Email.<br>";
                }
                if (CompanyName.Value == "")
                {
                    errNotification.Text = errNotification.Text + "Invalid Company Name.<br>";
                }
                if (!isValid)
                {
                    errNotification.Text = errNotification.Text + "Invalid Captcha.<br>";
                }
                if (queries == "'0'" || queries == "")
                {
                    errNotification.Text = errNotification.Text + "Select at least (1) business category.<br>";
                }
                if (LOIFileName.Value == "")
                {
                    errNotification.Text = errNotification.Text + "Please attach the LOI file.<br>";
                }
                if (this.txtimgcode.Text != this.Session["CaptchaImageText"].ToString())
                {
                    errNotification.Text = errNotification.Text + "Invalid Captcha code.<br>";
                }
                //errNotification.Text = "Invalid form field.";
                errNotification.ForeColor = Color.Red;
            }
            else
            {
                SaveToDB();
            }
        }
    }



    protected void Page_LoadComplete(object sender, EventArgs e)
    {

    }

    void PopulateFields()
    {

    }


    // CHECK EMAIL
    public static bool IsEmail(string Email)
    {
        string strRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
        @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
        @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
        Regex re = new Regex(strRegex);
        if (re.IsMatch(Email))
            return (true);
        else
            return (false);
    }

    // VERIFY EMAIL AND COMPANY IF ALREADY APPLIED AVA 
    private bool ApplicantExists()
    {
        query = "SELECT 1 FROM tblVendorApplicants WHERE EmailAdd = @EmailAdd AND CompanyName = @CompanyName";
        //query = "storedProc_Name"; //##storedProcedure
        using (conn = new SqlConnection(connstring))
        {
            using (cmd = new SqlCommand(query, conn))
            {
                //cmd.CommandType = CommandType.StoredProcedure; //##storedProcedure
                cmd.Parameters.AddWithValue("@EmailAdd", EmailAdd.Value.ToString().Replace("'", "''"));
                cmd.Parameters.AddWithValue("@CompanyName", CompanyName.Value.ToString().Replace("'", "''"));
                conn.Open(); oReader = cmd.ExecuteReader();
                if (oReader.HasRows)
                {
                    applicantExists = true;
                }
                else
                {
                    applicantExists = false;
                }
            }
        } conn.Close();
        return applicantExists;
    }


    void SaveToDB()
    {
        if (!ApplicantExists())
        {
            query = "INSERT INTO tblVendorApplicants (EmailAdd, CompanyName, LOIFileName, DateCreated, IsAuthenticated, Status, DateStarted, FinancialStatement) VALUES (@EmailAdd, @CompanyName, @LOIFileName, @DateCreated, @IsAuthenticated, @Status, @DateStarted, @FinancialStatement)";
            using (conn = new SqlConnection(connstring))
            {
                using (cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@EmailAdd", EmailAdd.Value.Trim().Replace("'", "''"));
                    cmd.Parameters.AddWithValue("@CompanyName", CompanyName.Value.Trim().Replace("'", "''"));
                    cmd.Parameters.AddWithValue("@LOIFileName", LOIFileName.Value);
                    cmd.Parameters.AddWithValue("@DateCreated", DateTime.Now);
                    cmd.Parameters.AddWithValue("@IsAuthenticated", 0);
                    cmd.Parameters.AddWithValue("@Status", 0);
                    cmd.Parameters.AddWithValue("@DateStarted", DateStarted.Value.Trim().ToString());
                    cmd.Parameters.AddWithValue("@FinancialStatement", FinancialStatement.SelectedValue.ToString());
                    conn.Open(); cmd.ExecuteNonQuery();
                }
            } conn.Close();

            int VendorId = 0;
            query = "SELECT ID FROM tblVendorApplicants WHERE EmailAdd = @EmailAdd AND CompanyName = @CompanyName";
            using (conn = new SqlConnection(connstring))
            {
                using (cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@EmailAdd", EmailAdd.Value.ToString().Replace("'", "''"));
                    cmd.Parameters.AddWithValue("@CompanyName", CompanyName.Value.ToString().Replace("'", "''"));
                    conn.Open(); oReader = cmd.ExecuteReader();
                    if (oReader.HasRows)
                    {
                        while (oReader.Read())
                        {
                            VendorId = Convert.ToInt32(oReader["ID"].ToString());
                        }
                    }
                }
            } conn.Close();

            //CLEAR tblVendorApplicantCategory FROM USER
            sCommand = "DELETE FROM tblVendorApplicantCategory WHERE VendorApplicantId = " + VendorId.ToString();
            SqlHelper.ExecuteNonQuery(connstring, CommandType.Text, sCommand);

            numRowsTbl = Convert.ToInt32(Request.Form["CategoryCounter"].ToString());
            for (int i = 1; i <= numRowsTbl; i++)
            {
                query = "INSERT INTO tblVendorApplicantCategory (VendorApplicantId, CategoryId, DateCreated) VALUES (@VendorApplicantId, @CategoryId,  @DateCreated)";
                using (conn = new SqlConnection(connstring))
                {
                    using (cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@VendorApplicantId", VendorId);
                        cmd.Parameters.AddWithValue("@CategoryId", Request.Form["CategoryId" + i].ToString());
                        cmd.Parameters.AddWithValue("@DateCreated", DateTime.Now);
                        conn.Open(); cmd.ExecuteNonQuery();
                    }
                }
            }

            string fromName = "", fromEmail = "", toName = "", toEmail = "", oEmailAdd = "", VendorName = "";
            query = "SELECT '" + ConfigurationManager.AppSettings["AdminEmailName"].ToString() + "' as fromName, '" + ConfigurationManager.AppSettings["AdminNoReplyEmail"].ToString() + "' as fromEmail, t1.FirstName + ' ' + t1.LastName as toName, t1.EmailAdd as toEmail, t4.EmailAdd, t4.CompanyName FROM tblUsers t1, tblUserTypes t2, tblVendorApplicants t4 WHERE t1.UserId = t2.UserId AND t1.Status = 1 AND t2.UserType = 10  AND t4.CompanyName = @CompanyName AND t4.EmailAdd = @EmailAdd";
            using (conn = new SqlConnection(connstring))
            {
                using (cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@EmailAdd", EmailAdd.Value.Trim().Replace("'", "''"));
                    cmd.Parameters.AddWithValue("@CompanyName", CompanyName.Value.Trim().Replace("'", "''"));
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
                            oEmailAdd = oReader["EmailAdd"].ToString();
                            VendorName = oReader["CompanyName"].ToString();
                            SendEmailNotification(fromName, fromEmail, toName, toEmail, oEmailAdd, VendorName);
                        }
                    }
                }
            } conn.Close();
            //SendEmailNotification();

            errNotification.Text = "Application successfully posted.";
            errNotification.ForeColor = Color.Blue;
            errNotification2.Text = "Your Vendor Accrediation application is subject to prequalification by Trans-Asia Vendor Management. Once prequalify, your username and password will be sent to you via email.";
            titleRegister.InnerHtml = "";
            tblForm.Visible = false;
            createBt.Visible = false;
            cancelBtLbl.Text = "BACK";

            EmailAdd.Value = "";
            CompanyName.Value = "";
            //txtCaptcha.Text = "";
            CompanyName.Disabled = true;
            EmailAdd.Disabled = true;
        }
        else
        {
            errNotification.Text = "Company Name & Email already have existing application.";
            errNotification.ForeColor = Color.Red;
            fileuploaded1.Text = "<a href='" + LOIFileName.Value + "'>" + LOIFileName.Value + "</a>"; 
        }

    }

    protected void Submit(object sender, EventArgs e)
    {
        //bool isValid = ucCaptcha.Validate(txtCaptcha.Text.Trim());
        //if (isValid)
        //{
        //    errNotification.Text = "Valid!";
        //    errNotification.ForeColor = Color.Green;
        //}
        //else
        //{
        //    errNotification.Text = "Invalid!";
        //    errNotification.ForeColor = Color.Red;
        //}
    }

    //############################################################
    //############################################################
    private bool SendEmailNotification(string sfromName, string sfromEmail, string stoName, string stoEmail, string sVendorEmail, string sVendorName)
    {
        bool success = false;

        string fromName = sfromName;
        string fromEmail = sfromEmail;
        string toName = stoName;
        string toEmail = stoName;

        string from = sfromName + " <" + sfromEmail + ">";
        string to = stoName + " <" + stoEmail + ">";
        string subject = "";
        //string oProjectName = "";
        //string oVSFId = "";

        //Response.Write(CreateNotificationBody(fromName, toName, sVendorEmail, sVendorName);
        try
        {
            //subject = "Trans-Asia VMS Accreditation new applicant.";
            subject = "Vendor Accreditation: New Applicant - " + sVendorName + "";
                if (!MailHelper.SendEmail(MailTemplate.GetDefaultSMTPServer(),
                        from,
                        to,
                        subject,
                        CreateNotificationBody(fromName, toName, sVendorEmail, sVendorName),
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


    private string CreateNotificationBody(string cfromName, string ctoName, string cVendorEmail, string cVendorName)
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
        sb.Append("To: " + ctoName + "<br><br>");
        sb.Append("</p>");
        sb.Append("<tr><td>");
        sb.Append("<p>");
        sb.Append("Dear " + ctoName + ":<br><br>");
        sb.Append("This email is to inform you that we have received a new letter of intent for accreditation.<br><br>");
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
        //StringBuilder sb = new StringBuilder();

        //string AdminName1 = fromName;
        //string VendorName1 = toName;

        //sb.Append("<tr><td><p>Sent: " + DateTime.Now.ToLongDateString() + "<br> To: " + VendorName1 + "<br><br> Good day!<br><br> This is to inform you that your application for accreditation is being processed.<br></p>&nbsp;<p>Very truly yours,<br><br><br> <strong>e-Sourcing Procurement</strong></p><p>&nbsp;</p> <span style='font-size:10px; font-style:italic;'>Please do not reply to this auto-generated  message.&nbsp;</span></td></tr>");

        return MailTemplate.IntegrateBodyIntoTemplate(sb.ToString());
    }

}