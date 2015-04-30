using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Web.UI.HtmlControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using Ava.lib;
using Ava.lib.constant;
using System.Text;
using System.Text.RegularExpressions;

public partial class vmofficer_vendorDetails_Endorsed : System.Web.UI.Page
{
    SqlDataReader oReader;
    string connstring = ConfigurationManager.ConnectionStrings["AVAConnectionString"].ConnectionString;
    string query;
    SqlCommand cmd;
    SqlConnection conn;
    //int numRowsTbl;

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
        }
        PopulateFields();
    }

    protected void Page_LoadComplete(object sender, EventArgs e)
    {
        
    }


    void PopulateFields()
    {
        query = "SELECT * FROM tblVendorApprovalbyLegal WHERE VendorId = @VendorId";
        //query = "sp_GetVendorInformation"; //##storedProcedure
        using (conn = new SqlConnection(connstring))
        {
            using (cmd = new SqlCommand(query, conn))
            {
                //cmd.CommandType = CommandType.StoredProcedure; //##storedProcedure
                cmd.Parameters.AddWithValue("@VendorId", Convert.ToInt32(Session["VendorId"]));
                conn.Open();
                //Process results
                oReader = cmd.ExecuteReader();
                if (oReader.HasRows)
                {
                    while (oReader.Read())
                    {
                        legalApproved.SelectedValue = oReader["legalApproved"].ToString();
                        legalRecollection.SelectedValue = oReader["legalRecollection"].ToString();
                        Recommendation.SelectedValue = oReader["Recommendation"].ToString();
                        FileAttachement.Value = oReader["FileAttachement"].ToString();
                        FileAttachementLbl.Text = "<a href='" + oReader["FileAttachement"].ToString() + "' target='_blank'>" + oReader["FileAttachement"].ToString() + "</a>";
                        if (oReader["legalApproved"].ToString() != "")
                        {
                            errNotification.Text = "Vendor has been endorsed.";
                            errNotification.ForeColor = Color.Blue;
                            createBt.Visible = false;
                        }
                    }
                }
            }

            query = "SELECT * FROM tblComments WHERE UserId = @UserId AND VendorId=@VendorId";
            //query = "sp_GetVendorInformation"; //##storedProcedure
            using (conn = new SqlConnection(connstring))
            {
                using (cmd = new SqlCommand(query, conn))
                {
                    //cmd.CommandType = CommandType.StoredProcedure; //##storedProcedure
                    cmd.Parameters.AddWithValue("@UserId", Convert.ToInt32(Session["UserId"]));
                    cmd.Parameters.AddWithValue("@VendorId", Convert.ToInt32(Session["VendorId"]));
                    conn.Open();
                    //Process results
                    oReader = cmd.ExecuteReader();
                    if (oReader.HasRows)
                    {
                        while (oReader.Read())
                        {
                            Comment.Value = oReader["Comment"].ToString();
                        }
                    }
                }
            }


            legalStrucOrgType.Text = "n/a";
            query = "SELECT * FROM tblVendorInformation WHERE VendorId = @VendorId";
            //query = "sp_GetVendorInformation"; //##storedProcedure
            using (conn = new SqlConnection(connstring))
            {
                using (cmd = new SqlCommand(query, conn))
                {
                    //cmd.CommandType = CommandType.StoredProcedure; //##storedProcedure
                    cmd.Parameters.AddWithValue("@VendorId", Convert.ToInt32(Session["VendorId"]));
                    conn.Open();
                    //Process results
                    oReader = cmd.ExecuteReader();
                    if (oReader.HasRows)
                    {
                        while (oReader.Read())
                        {
                            legalStrucOrgType.Text = oReader["legalStrucOrgType"].ToString() != "" ? oReader["legalStrucOrgType"].ToString() : "n/a";
                        }
                    }
                }
            }

            string NatureOfBusiness1 = "";
            query = "SELECT t2.NatureOfBusinessName FROM tblVendorNatureOfBusiness t1, rfcNatureOfBusiness t2  WHERE t1.VendorId = @VendorId AND t1.NatureOfBusinessId = t2.NatureOfBusinessId";
            //query = "sp_GetVendorInformation"; //##storedProcedure
            using (conn = new SqlConnection(connstring))
            {
                using (cmd = new SqlCommand(query, conn))
                {
                    //cmd.CommandType = CommandType.StoredProcedure; //##storedProcedure
                    cmd.Parameters.AddWithValue("@VendorId", Convert.ToInt32(Session["VendorId"]));
                    conn.Open();
                    //Process results
                    oReader = cmd.ExecuteReader();
                    if (oReader.HasRows)
                    {
                        while (oReader.Read())
                        {
                            NatureOfBusiness1 = NatureOfBusiness1 + oReader["NatureOfBusinessName"].ToString() + ", ";
                        }
                    }
                }
            }
            NatureOfBusiness.Text = NatureOfBusiness1 != "" ? NatureOfBusiness1.Substring(0, NatureOfBusiness1.Length - 2) : "n/a";


            string Category1 = "";
            query = "SELECT t2.CategoryName FROM tblVendorProductsAndServices t1, rfcProductCategory t2  WHERE t1.VendorId = @VendorId AND t1.CategoryId = t2.CategoryId GROUP BY t2.CategoryName";
            //query = "sp_GetVendorInformation"; //##storedProcedure
            using (conn = new SqlConnection(connstring))
            {
                using (cmd = new SqlCommand(query, conn))
                {
                    //cmd.CommandType = CommandType.StoredProcedure; //##storedProcedure
                    cmd.Parameters.AddWithValue("@VendorId", Convert.ToInt32(Session["VendorId"]));
                    conn.Open();
                    //Process results
                    oReader = cmd.ExecuteReader();
                    if (oReader.HasRows)
                    {
                        while (oReader.Read())
                        {
                            Category1 = Category1 + oReader["CategoryName"].ToString() + ", ";
                        }
                    }
                }
            }
            Category.Text = Category1 != "" ? Category1.Substring(0, Category1.Length - 2) : "n/a";



            query = "SELECT * FROM tblVendor WHERE VendorId = @VendorId";
            //query = "sp_GetVendorInformation"; //##storedProcedure
            using (conn = new SqlConnection(connstring))
            {
                using (cmd = new SqlCommand(query, conn))
                {
                    //cmd.CommandType = CommandType.StoredProcedure; //##storedProcedure
                    cmd.Parameters.AddWithValue("@VendorId", Convert.ToInt32(Session["VendorId"]));
                    conn.Open();
                    //Process results
                    oReader = cmd.ExecuteReader();
                    if (oReader.HasRows)
                    {
                        while (oReader.Read())
                        {
                            CompanyNameLbl.Text = oReader["CompanyName"].ToString() != "" ? "<a href='view_vendor_01_vendorInfo.aspx?VendorId=" + Session["VendorId"] + "' target='_blank'>" + oReader["CompanyName"].ToString() + "</a>" : "n/a";
                            //AuthenticationTicketLbl.Text = oReader["AuthenticationTicket"].ToString();
                        }
                    }
                }
            }

            query = "SELECT * FROM tblDnbRating WHERE VendorId = @VendorId";
            //query = "sp_GetVendorInformation"; //##storedProcedure
            using (conn = new SqlConnection(connstring))
            {
                using (cmd = new SqlCommand(query, conn))
                {
                    //cmd.CommandType = CommandType.StoredProcedure; //##storedProcedure
                    cmd.Parameters.AddWithValue("@VendorId", Convert.ToInt32(Session["VendorId"]));
                    conn.Open();
                    //Process results
                    oReader = cmd.ExecuteReader();
                    if (oReader.HasRows)
                    {
                        while (oReader.Read())
                        {
                            //.Value = oReader[""].ToString();
                            dnbDuns.Text = oReader["dnbDuns"].ToString() != "" ? oReader["dnbDuns"].ToString() : "n/a";
                            dnbRating.Text = oReader["dnbRating"].ToString() != "" ? oReader["dnbRating"].ToString() : "n/a";
                            dnbCompRating.Text = oReader["dnbCompRating"].ToString() != "" ? oReader["dnbCompRating"].ToString() : "n/a";
                            condition.Text = oReader["condition"].ToString() != "" ? oReader["condition"].ToString() : "n/a";
                            //dnbCompRating.Value = oReader["dnbCompRating"].ToString();
                            //condition.Value = oReader["condition"].ToString();

                        }
                    }
                }
            }

            query = "SELECT * FROM tblDnbFinancialReport WHERE VendorId = @VendorId";
            //query = "sp_GetVendorInformation"; //##storedProcedure
            using (conn = new SqlConnection(connstring))
            {
                using (cmd = new SqlCommand(query, conn))
                {
                    //cmd.CommandType = CommandType.StoredProcedure; //##storedProcedure
                    cmd.Parameters.AddWithValue("@VendorId", Convert.ToInt32(Session["VendorId"]));
                    conn.Open();
                    //Process results
                    oReader = cmd.ExecuteReader();
                    if (oReader.HasRows)
                    {
                        while (oReader.Read())
                        {
                            //.Value = oReader[""].ToString();
                            Year1.Text = oReader["Year1"].ToString() != "" ? oReader["Year1"].ToString() : "n/a";
                            yr1Revenue.Text = oReader["yr1Revenue"].ToString() != "" ? oReader["yr1Revenue"].ToString() : "n/a";
                            yr1NetIncome.Text = oReader["yr1NetIncome"].ToString() != "" ? oReader["yr1NetIncome"].ToString() : "n/a";
                            yr1NetEquity.Text = oReader["yr1NetEquity"].ToString() != "" ? oReader["yr1NetEquity"].ToString() : "n/a";
                            Year2.Text = oReader["Year2"].ToString() != "" ? oReader["Year2"].ToString() : "n/a";
                            yr2Revenue.Text = oReader["yr2Revenue"].ToString() != "" ? oReader["yr2Revenue"].ToString() : "n/a";
                            yr2NetIncome.Text = oReader["yr2NetIncome"].ToString() != "" ? oReader["yr2NetIncome"].ToString() : "n/a";
                            yr2NetEquity.Text = oReader["yr2NetEquity"].ToString() != "" ? oReader["yr2NetEquity"].ToString() : "n/a";
                            Year3.Text = oReader["Year3"].ToString() != "" ? oReader["Year3"].ToString() : "n/a";
                            yr3Revenue.Text = oReader["yr3Revenue"].ToString() != "" ? oReader["yr3Revenue"].ToString() : "n/a";
                            yr3NetIncome.Text = oReader["yr3NetIncome"].ToString() != "" ? oReader["yr3NetIncome"].ToString() : "n/a";
                            yr3NetEquity.Text = oReader["yr3NetEquity"].ToString() != "" ? oReader["yr3NetEquity"].ToString() : "n/a";
                            maxExpLimit.Text = oReader["maxExpLimit"].ToString() != "" ? oReader["maxExpLimit"].ToString() : "n/a";
                            creditExpLimit.Text = oReader["creditExpLimit"].ToString() != "" ? oReader["creditExpLimit"].ToString() : "n/a";

                        }
                    }
                }
            }


            query = "SELECT * FROM tblDnbLegalReport WHERE VendorId = @VendorId";
            //query = "sp_GetVendorInformation"; //##storedProcedure
            using (conn = new SqlConnection(connstring))
            {
                using (cmd = new SqlCommand(query, conn))
                {
                    //cmd.CommandType = CommandType.StoredProcedure; //##storedProcedure
                    cmd.Parameters.AddWithValue("@VendorId", Convert.ToInt32(Session["VendorId"]));
                    conn.Open();
                    //Process results
                    oReader = cmd.ExecuteReader();
                    if (oReader.HasRows)
                    {
                        while (oReader.Read())
                        {
                            //.Value = oReader[""].ToString();
                            TypeOfCase.Text = oReader["TypeOfCase"].ToString() != "" ? oReader["TypeOfCase"].ToString() : "n/a";
                            DateFiled.Text = oReader["DateFiled"].ToString() != "" ? oReader["DateFiled"].ToString() : "n/a";
                            fileuploaded_1.Text = oReader["Attachment"].ToString() != "" ? "<a href='" + oReader["Attachment"].ToString() + "' target='_blank'>" + oReader["Attachment"].ToString() + "</a>" : "<h3>n/a</h3>";
                        }
                    }
                }
            }
        }
    }


    void SaveToDB()
    {
        string connstring = ConfigurationManager.ConnectionStrings["AVAConnectionString"].ConnectionString;
        string sCommand = "";
        
        if (Request.Form["__EVENTTARGET"] == "continueStp")
        {
            if (legalApproved.SelectedValue != "")
            {
                sCommand = "DELETE FROM tblVendorApprovalbyLegal  WHERE VendorId = " + Session["VendorId"];
                SqlHelper.ExecuteNonQuery(connstring, CommandType.Text, sCommand);

                query = "INSERT INTO tblVendorApprovalbyLegal (VendorId, legalUserId, legalApproved, FileAttachement, legalRecollection, Recommendation, DateCreated) VALUES (@VendorId, @legalUserId, @legalApproved, @FileAttachement, @legalRecollection, @Recommendation, @DateCreated)";
                //query = "sp_GetVendorInformation"; //##storedProcedure
                using (conn = new SqlConnection(connstring))
                {
                    using (cmd = new SqlCommand(query, conn))
                    {
                        //cmd.CommandType = CommandType.StoredProcedure; //##storedProcedure
                        cmd.Parameters.AddWithValue("@VendorId", Convert.ToInt32(Session["VendorId"]));
                        cmd.Parameters.AddWithValue("@legalUserId", Convert.ToInt32(Session["UserId"]));
                        cmd.Parameters.AddWithValue("@legalApproved", Convert.ToInt32(legalApproved.SelectedValue.ToString()));
                        cmd.Parameters.AddWithValue("@FileAttachement", FileAttachement.Value.ToString());
                        cmd.Parameters.AddWithValue("@legalRecollection", Convert.ToInt32(legalRecollection.SelectedValue.ToString()));
                        cmd.Parameters.AddWithValue("@Recommendation", Convert.ToInt32(Recommendation.SelectedValue.ToString()));
                        cmd.Parameters.AddWithValue("@DateCreated", DateTime.Now);
                        conn.Open(); cmd.ExecuteNonQuery();
                    }
                }
                //sCommand = "INSERT INTO tblVendorApprovalbyLegal (VendorId, legalUserId, legalApproved, DateCreated) VALUES (" + Session["VendorId"] + ", " + Session["UserId"] + ", " + legalApproved.SelectedValue + ", getdate())";
                //SqlHelper.ExecuteNonQuery(connstring, CommandType.Text, sCommand);

                sCommand = "IF EXISTS (SELECT 1 FROM tblVendor WHERE VendorId = " + Session["VendorId"] + " AND (approvedbyVMTechDate IS NOT NULL OR approvedbyVMTechDate != '') AND (approvedbyVMIssueDate IS NOT NULL OR approvedbyVMIssueDate != '') AND Status = 2) BEGIN UPDATE tblVendor SET approvedbyLegal = " + Session["UserId"] + ", approvedbyLegalDate = getdate(), Status = 3 WHERE VendorId = " + Session["VendorId"] + " END ELSE BEGIN UPDATE tblVendor SET approvedbyLegal = " + Session["UserId"] + ", approvedbyLegalDate = getdate() WHERE VendorId = " + Session["VendorId"] + " END";
                SqlHelper.ExecuteNonQuery(connstring, CommandType.Text, sCommand);

                errNotification.Text = "Vendor has been successfully endorsed.";
                errNotification.ForeColor = Color.Blue;
                createBt.Visible = false;
                legalApproved.Enabled = false;
                Comment.Disabled = false;
                legalRecollection.Enabled = false;
                Recommendation.Enabled = false;
 


                // SEND EMAIL NOTIFICATION TO VM RECO
                string fromName = "", fromEmail = "", toName = "", toEmail = "", AuthenticationTicket = "", VendorName = "";
                query = "SELECT t3.FirstName + ' ' + t3.LastName as fromName, t3.EmailAdd as fromEmail, t1.FirstName + ' ' + t1.LastName as toName, t1.EmailAdd as toEmail, t4.CompanyName, t4.AuthenticationTicket FROM tblUsers t1, tblUserTypes t2, tblUsers t3, tblVendor t4 WHERE t1.UserId = t2.UserId AND t2.UserType = 16 AND t3.UserId = @UserId AND t4.Status = 3 AND t4.VendorId = @VendorId";
                //query = "sp_GetVendorInformation"; //##storedProcedure
                using (conn = new SqlConnection(connstring))
                {
                    using (cmd = new SqlCommand(query, conn))
                    {
                        //cmd.CommandType = CommandType.StoredProcedure; //##storedProcedure
                        cmd.Parameters.AddWithValue("@VendorId", Convert.ToInt32(Session["VendorId"]));
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
                                SendEmailNotification(fromName, fromEmail, toName, toEmail, AuthenticationTicket, VendorName);
                            }
                        }
                    }
                }
                // SEND EMAIL NOTIFICATION TO VM RECO ENDS
            }
            else 
            {
                errNotification.Text = "Please tick \"Will / Will Not\" to endorse this vendor.";
                errNotification.ForeColor = Color.Red;
            }
        }


        if (Comment.Value != "")
        {
            query = "INSERT INTO tblComments (VendorId, UserId, Comment, DateCreated) VALUES (@VendorId, @UserId, @Comment, @DateCreated)";
            //query = "sp_GetVendorInformation"; //##storedProcedure
            using (conn = new SqlConnection(connstring))
            {
                using (cmd = new SqlCommand(query, conn))
                {
                    //cmd.CommandType = CommandType.StoredProcedure; //##storedProcedure
                    cmd.Parameters.AddWithValue("@VendorId", Convert.ToInt32(Session["VendorId"]));
                    cmd.Parameters.AddWithValue("@UserId", Convert.ToInt32(Session["UserId"]));
                    cmd.Parameters.AddWithValue("@Comment", Comment.Value.ToString());
                    cmd.Parameters.AddWithValue("@DateCreated", DateTime.Now);
                    conn.Open(); cmd.ExecuteNonQuery();
                }
            }
        }
        Comment.Disabled = true;
    }



    //############################################################
    //############################################################
    // SEND EMAIL NOTIFICATION TO VM RECO
    private bool SendEmailNotification(string sfromName, string sfromEmail, string stoName, string stoEmail, string sAuthenticationTicket, string sVendorName)
    {
        bool success = false;

        string from = sfromName + "<" + sfromEmail + ">";
        string to = stoName + "<" + stoEmail + ">";
        string subject = "";

        try
        {
            subject = "Trans-Asia VMS Accreditation application reviewed for your approval.";
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
        //sTxt = sTxt + "<tr>";
        //sTxt = sTxt + "<td><strong>&nbsp;Authentication Ticket</strong></td>";
        //sTxt = sTxt + "<td>&nbsp;" + cAuthenticationNumber + "&nbsp;</td>";
        //sTxt = sTxt + "</tr>";
        sTxt = sTxt + "</table>";

        sb.Append("<tr><td><p>Sent: " + DateTime.Now.ToLongDateString() + "<br>From: " + cfromName + "<br> To: " + ctoName + "<br><br> Good day!<br><br> This is to inform you that application for vendor accreditation has been reviewed for your approval.<br></p><br>" + sTxt + "<p>Very truly yours,<br><br><br> <strong>" + cfromName + "</strong></p><p>&nbsp;</p> <span style='font-size:10px; font-style:italic;'>Please do not reply to this auto-generated  message.&nbsp;</span></td></tr>");

        return MailTemplate.IntegrateBodyIntoTemplate(sb.ToString());
    }

}