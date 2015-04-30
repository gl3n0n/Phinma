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

public partial class dnb_vendorDetails : System.Web.UI.Page
{
    SqlDataReader oReader;
    string connstring = ConfigurationManager.ConnectionStrings["AVAConnectionString"].ConnectionString;
    string query;
    SqlCommand cmd;
    SqlConnection conn;
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
        //TestShowAllSessions();
        if (Session["UserId"] == null) Response.Redirect("login.aspx");
        if (Session["SESSION_USERTYPE"].ToString() != "12") Response.Redirect("login.aspx");
        if (IsPostBack)
        {
            SaveToDB();
            PopulateFields();
        }
        PopulateFields();
        repeaterdsDnbLegalReport.DataBind();
    }

    protected void Page_LoadComplete(object sender, EventArgs e)
    {
        
    }


    void PopulateFields()
    {
        query = "SELECT * FROM tblVendor WHERE VendorId = @VendorId";
        using (conn = new SqlConnection(connstring))
        {
            using (cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@VendorId", Convert.ToInt32(Session["VendorId"]));
                conn.Open(); oReader = cmd.ExecuteReader();
                if (oReader.HasRows)
                {
                    while (oReader.Read())
                    {
                        if (oReader["Status"].ToString() == "2") { Response.Redirect("dnb_vendorDetailsView.aspx"); }
                        AuthenticationTicketLbl.Text = oReader["AuthenticationTicket"].ToString();
                        dnbDuns.Value = oReader["AuthenticationTicket"].ToString();
                        dnbDuns.Disabled = true;
                        CompanyNameLbl.Text = oReader["CompanyName"].ToString() != "" ? "<a href='vendor_Home.aspx?VendorId=" + Session["VendorId"] + "' target='_blank'>" + oReader["CompanyName"].ToString() + "</a>" : "n/a";
                        VendorGroupCode.SelectedValue = oReader["AccGroup"].ToString();

                        TradeNonTrade.SelectedValue = oReader["TradeNonTrade"].ToString()!="" ? oReader["TradeNonTrade"].ToString() : "H";
                    }
                }
            }
        }

        query = "SELECT * FROM tblDnbReport WHERE VendorId = @VendorId";
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
                        if (oReader["dnbDuns"].ToString() != "")
                        {
                            dnbDuns.Value = oReader["dnbDuns"].ToString();
                        }
                        dnbScore.Value = oReader["dnbScore"].ToString();
                        dnbFinCapScore.Value = oReader["dnbFinCapScore"].ToString();
                        dnbFinCapScore_Remarks.Value = oReader["dnbFinCapScore_Remarks"].ToString();
                        dnbLegalConfScore.Value = oReader["dnbLegalConfScore"].ToString();
                        dnbLegalConfScore_Remarks.Value = oReader["dnbLegalConfScore_Remarks"].ToString();
                        dnbTechCompScore.Value = oReader["dnbTechCompScore"].ToString();
                        dnbTechCompScore_Remarks.Value = oReader["dnbTechCompScore_Remarks"].ToString();
                        dnbCurrentRevenue.Value = oReader["dnbCurrentRevenue"].ToString();
                        dnbSupplierInfoReport.Value = oReader["dnbSupplierInfoReport"].ToString();
                        dnbSupplierInfoReportLbl.Text = oReader["dnbSupplierInfoReport"].ToString() != "" ? "<a href='" + oReader["dnbSupplierInfoReport"].ToString() + "' target='_blank'>" + oReader["dnbSupplierInfoReport"].ToString() + "</a> " + "<script type='text/javascript'>$(document).ready(function(){ $('#dnbSupplierInfoReportx').show(); });</script>" : "Attach file";
                        dnbOtherDocuments.Value = oReader["dnbOtherDocuments"].ToString();
                        dnbOtherDocumentsLbl.Text = oReader["dnbOtherDocuments"].ToString() != "" ? "<a href='" + oReader["dnbOtherDocuments"].ToString() + "' target='_blank'>" + oReader["dnbOtherDocuments"].ToString() + "</a> " + "<script type='text/javascript'>$(document).ready(function(){ $('#dnbOtherDocumentsx').show(); });</script>" : "Attach file";

                        dnbScore.Value = (Convert.ToInt32(oReader["dnbFinCapScore"].ToString()) + Convert.ToInt32(oReader["dnbLegalConfScore"].ToString()) + Convert.ToInt32(oReader["dnbTechCompScore"].ToString())).ToString();

                        //MaxExposureLimit.Value = (Convert.ToInt32(oReader["dnbMaxExposureLimit"].ToString()) * .5).ToString();
                        MaxExposureLimit.Value = oReader["dnbMaxExposureLimit"].ToString();
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
                        //TypeOfCase.Value = oReader["TypeOfCase"].ToString();
                        //DateFiled.Value = oReader["DateFiled"].ToString();
                        //Attachment1.Value = oReader["Attachment"].ToString();
                        //fileuploaded_1.Text = "<a href='" + oReader["Attachment"].ToString() + "' target='_blank'>" + oReader["Attachment"].ToString() + "</a>";
                    }
                }
            }
        }
    }


    void SaveToDB()
    {
        string connstring = ConfigurationManager.ConnectionStrings["AVAConnectionString"].ConnectionString;
        string sCommand = "";

        //CLEAR tblVendorProductsAndServices FROM USER
        sCommand = "DELETE FROM tblDnbReport WHERE VendorId = " + Session["VendorId"];
        SqlHelper.ExecuteNonQuery(connstring, CommandType.Text, sCommand);
        query = "INSERT INTO tblDnbReport (VendorId, dnbUserId, dnbDuns, dnbScore, dnbFinCapScore, dnbFinCapScore_Remarks, dnbLegalConfScore, dnbLegalConfScore_Remarks, dnbTechCompScore, dnbTechCompScore_Remarks, dnbCurrentRevenue, dnbMaxExposureLimit, dnbSupplierInfoReport, dnbOtherDocuments) VALUES (@VendorId, @dnbUserId, @dnbDuns, @dnbScore, @dnbFinCapScore, @dnbFinCapScore_Remarks, @dnbLegalConfScore, @dnbLegalConfScore_Remarks, @dnbTechCompScore, @dnbTechCompScore_Remarks, @dnbCurrentRevenue, @dnbMaxExposureLimit, @dnbSupplierInfoReport, @dnbOtherDocuments)";
        //query = "sp_GetVendorInformation"; //##storedProcedure
        using (conn = new SqlConnection(connstring))
        {
            using (cmd = new SqlCommand(query, conn))
            {
                //cmd.CommandType = CommandType.StoredProcedure; //##storedProcedure
                cmd.Parameters.AddWithValue("@VendorId", Convert.ToInt32(Session["VendorId"]));
                cmd.Parameters.AddWithValue("@dnbUserId", Convert.ToInt32(Session["UserId"]));
                cmd.Parameters.AddWithValue("@dnbDuns", dnbDuns.Value.Trim().ToString());
                cmd.Parameters.AddWithValue("@dnbScore", dnbScore.Value.Trim().ToString());
                cmd.Parameters.AddWithValue("@dnbFinCapScore", dnbFinCapScore.Value.Trim().ToString() != "" ? Convert.ToInt32(dnbFinCapScore.Value.Trim().ToString().Replace(",", "")) : 0);
                cmd.Parameters.AddWithValue("@dnbFinCapScore_Remarks", dnbFinCapScore_Remarks.Value.Trim().ToString());
                cmd.Parameters.AddWithValue("@dnbLegalConfScore", dnbLegalConfScore.Value.Trim().ToString() != "" ? Convert.ToInt32(dnbLegalConfScore.Value.Trim().ToString().Replace(",", "")) : 0);
                cmd.Parameters.AddWithValue("@dnbLegalConfScore_Remarks", dnbLegalConfScore_Remarks.InnerText);
                cmd.Parameters.AddWithValue("@dnbTechCompScore", dnbTechCompScore.Value.Trim().ToString() != "" ? Convert.ToInt32(dnbTechCompScore.Value.Trim().ToString().Replace(",", "")) : 0);
                cmd.Parameters.AddWithValue("@dnbTechCompScore_Remarks", dnbTechCompScore_Remarks.Value.Trim().ToString());
                cmd.Parameters.AddWithValue("@dnbCurrentRevenue", dnbCurrentRevenue.Value.Trim().ToString() != "" ? Convert.ToDouble(dnbCurrentRevenue.Value.Trim().ToString().Replace(",", "")) : 0);
                cmd.Parameters.AddWithValue("@dnbMaxExposureLimit", MaxExposureLimit.Value.Trim().ToString() != "" ? Convert.ToDouble(MaxExposureLimit.Value.Trim().ToString().Replace(",", "")) : 0);
                cmd.Parameters.AddWithValue("@dnbSupplierInfoReport", dnbSupplierInfoReport.Value.Trim());
                cmd.Parameters.AddWithValue("@dnbOtherDocuments", dnbOtherDocuments.Value.Trim());
                conn.Open(); cmd.ExecuteNonQuery();

                errNotification.Text = "Vendor report successfully updated.";
                errNotification.ForeColor = Color.Blue;
            }
        }

        //update vendor group
        query = "UPDATE tblVendor SET AccGroup=@AccGroup, TradeNonTrade=@TradeNonTrade WHERE VendorId = @VendorId";
        //query = "sp_GetVendorInformation"; //##storedProcedure
        using (conn = new SqlConnection(connstring))
        {
            using (cmd = new SqlCommand(query, conn))
            {
                //cmd.CommandType = CommandType.StoredProcedure; //##storedProcedure
                cmd.Parameters.AddWithValue("@VendorId", Convert.ToInt32(Session["VendorId"]));
                cmd.Parameters.AddWithValue("@AccGroup", VendorGroupCode.SelectedValue.Trim());
                cmd.Parameters.AddWithValue("@TradeNonTrade", TradeNonTrade.SelectedValue.Trim());
                conn.Open(); cmd.ExecuteNonQuery();
            }
        }


        //Response.Write(Request.Form["DateFiled1"].ToString());
        //CLEAR tblDnbLegalReport FROM USER
        sCommand = "DELETE FROM tblDnbLegalReport WHERE VendorId = " + Session["VendorId"].ToString();
        SqlHelper.ExecuteNonQuery(connstring, CommandType.Text, sCommand);

        numRowsTbl = Convert.ToInt32(Request.Form["DnbLegalReportCounter"].ToString());
        for (int i = 1; i <= numRowsTbl; i++){
            query = "INSERT INTO tblDnbLegalReport (VendorId, TypeOfCase, DateFiled, Attachment, DateCreated) VALUES (@VendorId, @TypeOfCase, @DateFiled, @Attachment, @DateCreated)";
            using (conn = new SqlConnection(connstring))
            {
                using (cmd = new SqlCommand(query, conn))
                {
                    //Response.Write(Request.Form["DateFiled" + i].ToString());
                    if (Request.Form["TypeOfCase" + i].ToString()!="" && (Request.Form["DateFiled" + i].ToString() != "01/01/1900" && Request.Form["DateFiled" + i].ToString() != ""))
                    {
                        cmd.Parameters.AddWithValue("@VendorId", Convert.ToInt32(Session["VendorId"].ToString()));
                        cmd.Parameters.AddWithValue("@TypeOfCase", Request.Form["TypeOfCase" + i].ToString());
                        cmd.Parameters.AddWithValue("@DateFiled", Request.Form["DateFiled" + i].ToString());
                        cmd.Parameters.AddWithValue("@Attachment", Request.Form["Attachment" + i].ToString());
                        //cmd.Parameters.AddWithValue("@VendorId", Convert.ToInt32(Session["VendorId"].ToString()));
                        //cmd.Parameters.AddWithValue("@TypeOfCase", Request.Form["TypeOfCase1"].ToString());
                        //cmd.Parameters.AddWithValue("@DateFiled", Request.Form["DateFiled1"].ToString());
                        //cmd.Parameters.AddWithValue("@Attachment", Request.Form["Attachment1"].ToString());
                        cmd.Parameters.AddWithValue("@DateCreated", DateTime.Now);
                        conn.Open(); cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        ////CLEAR tblVendorProductsAndServices FROM USER
        //sCommand = "DELETE FROM tblDnbLegalReport WHERE VendorId = " + Session["VendorId"];
        //SqlHelper.ExecuteNonQuery(connstring, CommandType.Text, sCommand);
        //query = "INSERT INTO tblDnbLegalReport (VendorId, TypeOfCase, DateFiled, Attachment, DateCreated) VALUES (@VendorId, @TypeOfCase, @DateFiled, @Attachment, @DateCreated)";
        ////query = "sp_GetVendorInformation"; //##storedProcedure
        //using (conn = new SqlConnection(connstring))
        //{
        //    using (cmd = new SqlCommand(query, conn))
        //    {
        //        //cmd.CommandType = CommandType.StoredProcedure; //##storedProcedure
        //        //cmd.Parameters.AddWithValue("@VendorId", Convert.ToInt32(Session["VendorId"]));
        //        //cmd.Parameters.AddWithValue("@TypeOfCase", TypeOfCase.Value.Trim());
        //        //cmd.Parameters.AddWithValue("@DateFiled", DateFiled.Value.Trim());
        //        //cmd.Parameters.AddWithValue("@Attachment", Attachment1.Value.Trim());
        //        //cmd.Parameters.AddWithValue("@DateCreated", DateTime.Now);
        //        //conn.Open(); cmd.ExecuteNonQuery();

        //        errNotification.Text = "Vendor report successfully updated.";
        //        errNotification.ForeColor = Color.Blue;
        //    }
        //}


        //Response.Write(Request.Form["__EVENTTARGET"]);
        if (Request.Form["__EVENTTARGET"] == "continueStp")
        {
            sCommand = "UPDATE tblVendor SET Status = 2, approvedbyDnb = " + Session["UserId"] + ", approvedbyDnbDate = getdate() WHERE VendorId = " + Session["VendorId"];
            SqlHelper.ExecuteNonQuery(connstring, CommandType.Text, sCommand);

            string fromName = "", fromEmail = "", toName = "", toEmail = "", AuthenticationTicket = "", VendorName = "";
            query = "SELECT t3.FirstName + ' ' + t3.LastName as fromName, t3.EmailAdd as fromEmail, t1.FirstName + ' ' + t1.LastName as toName, t1.EmailAdd as toEmail, t4.CompanyName, t4.AuthenticationTicket FROM tblUsers t1, tblUserTypes t2, tblUsers t3, tblVendor t4 WHERE t1.UserId = t2.UserId AND t1.Status = 1 AND t3.Status = 1 AND t2.UserType = 10 AND t3.UserId = @UserId AND t4.Status = 2 AND t4.VendorId = @VendorId";
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
        }
    }


    //############################################################
    //############################################################
    // SEND EMAIL NOTIFICATION TO LEGAL, TECH & ISSUE
    private bool SendEmailNotification(string sfromName, string sfromEmail, string stoName, string stoEmail, string sAuthenticationTicket, string sVendorName)
    {
        bool success = false;

        string from = sfromName + "<" + sfromEmail + ">";
        string to = stoName + "<" + stoEmail + ">";
        string subject = "";

        try
        {
            //subject = "Trans-Asia VMS Accreditation for Approval -- " + sVendorName;
            subject = "Vendor Accreditation: For Approval <"+ sVendorName + ">";
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
        SqlDataReader oReader;
        string cServices = "";
        query = "SELECT t1.*, t2.CategoryName FROM tblVendorProductsAndServices t1, rfcProductCategory t2 WHERE t2.CategoryId = t1.CategoryId AND t1.VendorId = @VendorId";
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
                        cServices = cServices + "&bull; " + oReader["CategoryName"].ToString() + "<br>";
                    }
                }
            }
        }

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
        sTxt = sTxt + "<td><strong>&nbsp;Category</strong></td>";
        sTxt = sTxt + "<td>&nbsp;" + cServices + "&nbsp;</td>";
        sTxt = sTxt + "</tr>";
        sTxt = sTxt + "</table>";

        //sb.Append("<tr><td><p>Sent: " + DateTime.Now.ToLongDateString() + "<br>From: " + cfromName + "<br> To: " + ctoName + "<br><br> Good day!<br><br> This is to inform you that application for vendor accreditation has been reviewed for your approval.<br></p><br>" + sTxt + "<p>Very truly yours,<br><br><br> <strong>" + cfromName + "</strong></p><p>&nbsp;</p> <span style='font-size:10px; font-style:italic;'>Please do not reply to this auto-generated  message.&nbsp;</span></td></tr>");


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
        sb.Append("Trans-Asia Vendor Magement<br><br>");
        sb.Append("</td></tr>");
        sb.Append("<tr><td>");
        sb.Append("<p>&nbsp;</p>");
        sb.Append("<b>Instructions:</b><br>");
        sb.Append("&nbsp;&nbsp;1. Go to <a href='" + System.Configuration.ConfigurationManager.AppSettings["ServerUrl"] + "' target='_blank'>" + System.Configuration.ConfigurationManager.AppSettings["ServerUrl"] + "</a><br>");
        sb.Append("&nbsp;&nbsp;2. Enter your Username and Password then click Login<br>");
        sb.Append("&nbsp;&nbsp;3. Click Vendors for Approval<br>");
        sb.Append("&nbsp;&nbsp;4. Click View<br>");
        sb.Append("</td></tr>");
        sb.Append("<tr><td>");
        sb.Append("<p>&nbsp;</p><span style='font-size:10px; font-style:italic;'>Please do not reply to this auto-generated  message.&nbsp;</span>");
        sb.Append("</td></tr>");

        return MailTemplate.IntegrateBodyIntoTemplate(sb.ToString());
    }
}