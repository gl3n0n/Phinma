using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using Ava.lib;
using Ava.lib.constant;
using System.Text;
using System.Text.RegularExpressions;

public partial class vendor_08_Undertakings : System.Web.UI.Page
{
    SqlDataReader oReader;
    string connstring = ConfigurationManager.ConnectionStrings["AVAConnectionString"].ConnectionString;
    string query;
    SqlCommand cmd;
    SqlConnection conn;
    public int VendorId;
    public string queryString;

    protected void TestShowAllSessions()
    {  //test show all session
        string str = null;
        foreach (string key in HttpContext.Current.Session.Keys)
        { str += string.Format("<b>{0}</b>: {1};  ", key, HttpContext.Current.Session[key].ToString()); }
        Response.Write("<span style='font-size:12px'>" + str + "</span>");
    }

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


        //TestShowAllSessions();
        if (
            Session["UserId"] == null ||
            Session["UserId"].ToString() == "" ||
            VendorId.ToString() == ""
            )
        {
            Response.Redirect("login.aspx");
        }


        if (editable())
        {
            if (IsPostBack) SaveToDB();
            PopulateFields();
            tbl01_Lbl.Visible = false;
        }
        else
        {
            PopulateFields_Lbl();
            tbl01.Visible = false;
            createBt.Visible = createBt1.Visible = false;
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
                        }
                    }
                }
            }
        }

        return EditMode;
    }

    private string GenerateAuthenticationTicket()
    {
        string strPwdchar = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        string strPwd = "";
        Random rnd = new Random();
        for (int i = 0; i <= 10; i++)
        {
            int iRandom = rnd.Next(0, strPwdchar.Length - 1);
            strPwd += strPwdchar.Substring(iRandom, 1);
        }
        DateTime DateToday = DateTime.Now;
        string DateTodayString = String.Format("{0:yyyyMMdd}", DateToday);
        strPwd = DateTodayString + Session["VendorId"].ToString() + strPwd;
        return strPwd;
    }

    void PopulateFields()
    {
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
                        step8FullName.Value = oReader["step8FullName"].ToString()!="" ? oReader["step8FullName"].ToString() : "Full Name";
                        step8OfficialTitle.Value = oReader["step8OfficialTitle"].ToString() != "" ? oReader["step8OfficialTitle"].ToString() : "Official Title";
                        step8OfCompanyName.Value = oReader["CompanyName"].ToString() != "" ? oReader["CompanyName"].ToString() : "Company Name";
                        step8bindCompanyName.Value = oReader["CompanyName"].ToString() != "" ? oReader["CompanyName"].ToString() : "Company Name";
                    }
                }
            }
        }
    }

    void PopulateFields_Lbl()
    {
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
                        step8FullName_Lbl.Text = oReader["step8FullName"].ToString() != "" ? oReader["step8FullName"].ToString() : "Full Name";
                        step8OfficialTitle_Lbl.Text = oReader["step8OfficialTitle"].ToString() != "" ? oReader["step8OfficialTitle"].ToString() : "Official Title";
                        step8OfCompanyName_Lbl.Text = oReader["step8OfCompanyName"].ToString() != "" ? oReader["step8OfCompanyName"].ToString() : "Company Name";
                        step8bindCompanyName_Lbl.Text = oReader["step8bindCompanyName"].ToString() != "" ? oReader["step8bindCompanyName"].ToString() : "Company Name";
                    }
                }
            }
        }
    }


    void SaveToDB()
    {

        query = "UPDATE tblVendorInformation SET step8FullName = @step8FullName, step8OfficialTitle=@step8OfficialTitle, step8OfCompanyName=@step8OfCompanyName, step8bindCompanyName=@step8bindCompanyName WHERE VendorId = @VendorId";
        //query = "sp_GetVendorInformation"; //##storedProcedure
        using (conn = new SqlConnection(connstring))
        {
            using (cmd = new SqlCommand(query, conn))
            {
                //cmd.CommandType = CommandType.StoredProcedure; //##storedProcedure
                cmd.Parameters.AddWithValue("@VendorId", VendorId);
                cmd.Parameters.AddWithValue("@step8FullName", step8FullName.Value.Trim().ToString());
                cmd.Parameters.AddWithValue("@step8OfficialTitle", step8OfficialTitle.Value.Trim().ToString());
                cmd.Parameters.AddWithValue("@step8OfCompanyName", step8OfCompanyName.Value.Trim().ToString());
                cmd.Parameters.AddWithValue("@step8bindCompanyName", step8bindCompanyName.Value.Trim().ToString());
                conn.Open(); cmd.ExecuteNonQuery();
            }
        }

        query = "UPDATE tblVendor SET authenticationTicket=@authenticationTicket, Status = 1 WHERE VendorId = @VendorId";
        //query = "sp_GetVendorInformation"; //##storedProcedure
        using (conn = new SqlConnection(connstring))
        {
            using (cmd = new SqlCommand(query, conn))
            {
                //cmd.CommandType = CommandType.StoredProcedure; //##storedProcedure
                cmd.Parameters.AddWithValue("@VendorId", VendorId);
                cmd.Parameters.AddWithValue("@authenticationTicket", GenerateAuthenticationTicket());
                //conn.Open(); cmd.ExecuteNonQuery();
            }
        }


        string fromName = "", fromEmail = "", toName = "", toEmail = "", AuthenticationTicket = "", VendorName = "";
        query = "SELECT t3.CompanyName as fromName, t3.EmailAdd as fromEmail, t1.FirstName + ' ' + t1.LastName as toName, t1.EmailAdd as toEmail, t4.CompanyName, t4.AuthenticationTicket FROM tblUsers t1, tblUserTypes t2, tblUsers t3, tblVendor t4 WHERE t1.UserId = t2.UserId AND t2.UserType = 12 AND t3.UserId = @UserId AND t4.Status = 1 AND t4.VendorId = @VendorId";
        //query = "sp_GetVendorInformation"; //##storedProcedure
        using (conn = new SqlConnection(connstring))
        {
            using (cmd = new SqlCommand(query, conn))
            {
                //cmd.CommandType = CommandType.StoredProcedure; //##storedProcedure
                cmd.Parameters.AddWithValue("@VendorId", VendorId);
                cmd.Parameters.AddWithValue("@UserId", Convert.ToInt32(Session["UserId"]));
                conn.Open();
                //Process results
                oReader = cmd.ExecuteReader();
                if (oReader.HasRows)
                {
                    while (oReader.Read())
                    {
                        fromName = oReader["fromName"].ToString();
                        fromEmail = oReader["fromEmail"].ToString();
                        toName = oReader["toName"].ToString();
                        toEmail = oReader["toEmail"].ToString();
                        AuthenticationTicket = oReader["AuthenticationTicket"].ToString();
                        VendorName = oReader["CompanyName"].ToString();
                        //SendEmailNotification(fromName, fromEmail, toName, toEmail, AuthenticationTicket, VendorName);
                    }
                }
            }
        }

        //SendEmailNotificationforVendor(fromName, fromEmail, toName, toEmail, AuthenticationTicket, VendorName);
        

        string control1 = Request.Form["__EVENTTARGET"];
        if (control1 == "continueStp")
        {
            Response.Redirect("vendor_Home.aspx");
        }
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
            subject = "Trans-Asia VMS Accreditation application posted for authentication.";
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
        string sTxt = "<table border='1' style='font-size:12px'>";
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

        sb.Append("<tr><td><p>Sent: " + DateTime.Now.ToLongDateString() + "<br>From: " + cfromName + "<br> To: " + ctoName + "<br><br> Good day!<br><br> This is to inform you that application for vendor accreditation has been posted for your approval.<br></p><br>" + sTxt + "<p>Very truly yours,<br><br><br> <strong>" + cfromName + "</strong></p><p>&nbsp;</p> <span style='font-size:10px; font-style:italic;'>Please do not reply to this auto-generated  message.&nbsp;</span></td></tr>");

        return MailTemplate.IntegrateBodyIntoTemplate(sb.ToString());
    }



    //############################################################
    //############################################################
    // SEND EMAIL NOTIFICATION TO VENDOR
    private bool SendEmailNotificationforVendor(string sfromName, string sfromEmail, string stoName, string stoEmail, string sAuthenticationTicket, string sVendorName)
    {
        bool success = false;

        stoName = sfromName;
        stoEmail = sfromEmail;
        string to = sfromName + "<" + sfromEmail + ">";

        sfromEmail = System.Configuration.ConfigurationManager.AppSettings["AdminEmailAddress"];
        sfromName = System.Configuration.ConfigurationManager.AppSettings["AdminEmailName"];
        string from = sfromName + "<" + sfromEmail + ">";
        string subject = "";

        try
        {
            subject = "Trans-Asia VMS Accreditation application for approval process.";
            if (!MailHelper.SendEmail(MailTemplate.GetDefaultSMTPServer(),
                    from,
                    to,
                    subject,
                    CreateNotificationBodyforVendor(sfromName, stoName, sAuthenticationTicket, sVendorName),
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
        string sTxt = "<table border='1' style='font-size:12px'>";
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

        sb.Append("<tr><td><p>Sent: " + DateTime.Now.ToLongDateString() + "<br>From: " + cfromName + "<br> To: " + ctoName + "<br><br> Good day!<br><br> Your application for accreditation will now undergo approval processes. You will be notified once approved or rejected.<br></p><br>" + sTxt + "<p>Very truly yours,<br><br><br> <strong>e-Sourcing Procurement</strong></p><p>&nbsp;</p> <span style='font-size:10px; font-style:italic;'>Please do not reply to this auto-generated  message.&nbsp;</span></td></tr>");

        return MailTemplate.IntegrateBodyIntoTemplate(sb.ToString());
    }
}