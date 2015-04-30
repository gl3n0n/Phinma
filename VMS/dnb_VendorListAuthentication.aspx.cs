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

public partial class dnb_VendorListAuthentication : System.Web.UI.Page
{
    SqlDataReader oReader;
    string connstring = ConfigurationManager.ConnectionStrings["AVAConnectionString"].ConnectionString;
    string query;
    SqlCommand cmd;
    SqlConnection conn;
    //string sCommand;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserId"] == null) Response.Redirect("login.aspx");
        if (Session["SESSION_USERTYPE"].ToString() != "12") Response.Redirect("login.aspx");
        if (IsPostBack)
        {
            string control1 = Request.Form["__EVENTTARGET"];
            //Response.Write(control1);
            if (control1 == "Clarify")
            {
                doClarify(Convert.ToInt32(Request.Form["__EVENTARGUMENT"]));
            }
        }
    }

    void doClarify(int vendorid)
    {
        query = "UPDATE tblVendor SET Status=9 WHERE VendorId = @VendorId";
        using (conn = new SqlConnection(connstring))
        {
            using (cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@VendorId", vendorid);
                conn.Open(); cmd.ExecuteNonQuery();
            }
        }
        query = "INSERT INTO tblCommentsDnbClarify (VendorId, UserId, Comment, DateCreated) VALUES (@VendorId, @UserId, @Comment, @DateCreated)";
        using (conn = new SqlConnection(connstring))
        {
            using (cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@VendorId", vendorid);
                cmd.Parameters.AddWithValue("@UserId", Convert.ToInt32(Session["UserId"]));
                cmd.Parameters.AddWithValue("@Comment", txtClarify.Value);
                cmd.Parameters.AddWithValue("@DateCreated", DateTime.Now);
                conn.Open(); cmd.ExecuteNonQuery();
            }
        }

        string fromName = "", fromEmail = "", toName = "", toEmail = "", VendorEmail = "", VendorName = "", VendorAuthTicket = "";
        query = "SELECT t3.CompanyName as toName, t1.EmailAdd as toEmail, '" + ConfigurationManager.AppSettings["AdminEmailName"].ToString() + "' as fromName, '" + ConfigurationManager.AppSettings["AdminNoReplyEmail"].ToString() + "' as fromEmail, t3.AuthenticationTicket, t3.CompanyName, t1.EmailAdd FROM tblUsers t1, tblUsersForVendors t2, tblVendor t3 WHERE t3.VendorId=@VendorId AND t2.VendorId=t3.VendorId AND t1.UserId=t2.UserId";
        //query = "sp_GetVendorInformation"; //##storedProcedure
        using (conn = new SqlConnection(connstring))
        {
            using (cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@VendorId", vendorid);
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
                        VendorAuthTicket = oReader["AuthenticationTicket"].ToString();

                        SendEmailNotification(fromName, fromEmail, toName, toEmail, VendorAuthTicket, VendorEmail, VendorName);
                    }
                }
            }
        } conn.Close();

        errNotificationAuthTicket.Text = "Clarification sent";
        errNotificationAuthTicket.ForeColor = Color.Blue;
        GridView1.DataBind();
    }


    protected void gvVendors_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("Details"))
        {
            Session["VendorId"] = e.CommandArgument.ToString().Trim();

            Session["PrevUrl"] = HttpContext.Current.Request.Url.AbsoluteUri;
            Response.Redirect("vendor_Home.aspx?VendorId=" + e.CommandArgument.ToString().Trim());
            //Response.Redirect("dnb_vendorDetails.aspx");

        }
        if (e.CommandName.Equals("Authenticate"))
        {
            query = "UPDATE tblVendor SET IsAuthenticated = 1, DateAuthenticatedByDnb=getdate() WHERE VendorId = @VendorId";
            using (conn = new SqlConnection(connstring))
            {
                using (cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@VendorId", e.CommandArgument.ToString().Trim());
                    conn.Open(); cmd.ExecuteNonQuery();
                }
            }
            errNotificationAuthTicket.Text = "Vendor successfully authenticated.";
            errNotificationAuthTicket.ForeColor = Color.Blue;
            Response.Redirect("dnb_VendorListAuthentication.aspx");

        }
    }

    protected void Gridview1_RowDataBound(Object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int Status = (int)DataBinder.Eval(e.Row.DataItem, "Status");
            int VendorId = (int)DataBinder.Eval(e.Row.DataItem, "VendorId");
            string CompanyName = (string)DataBinder.Eval(e.Row.DataItem, "CompanyName");
            LinkButton LinkBt;
            HyperLink LinkBt2;
            if (Status == 9)
            {
                LinkBt = ((LinkButton)e.Row.FindControl("lnkAuth"));
                LinkBt.Enabled = false;
                LinkBt.ForeColor = Color.Gray;

                LinkBt2 = ((HyperLink)e.Row.FindControl("lnkClarify"));
                LinkBt2.Enabled = false;
                LinkBt2.Text = "Clarified";
                LinkBt2.ForeColor = Color.Gray;
            }
            else
            {
                LinkBt = ((LinkButton)e.Row.FindControl("lnkAuth"));
                LinkBt.Attributes["onclick"] = "javascript: return confirm('Are you sure you want to Authenticate this vendor?');";
                LinkBt2 = ((HyperLink)e.Row.FindControl("lnkClarify"));
                //LinkBt2.NavigateUrl = "javascript:__doPostBack('ctl00$ContentPlaceHolder1$GridView1$ctl04$lnkClarify','')";
                LinkBt2.Attributes["onclick"] = "javascript:ShowClarify('" + VendorId.ToString() + "','" + CompanyName.Replace("'","''") + "')";
                //LinkBt.Attributes["onclick"] = "javascript: return confirm('Are you sure you want to Clarify with this vendor?');";
            }
        }
    }


    //############################################################
    //############################################################
    // SEND EMAIL NOTIFICATION TO VENDOR
    private bool SendEmailNotification(string sfromName, string sfromEmail, string stoName, string stoEmail, string sAuthenticationTicket, string sVendorEmail, string sVendorName)
    {
        bool success = false;

        string from = sfromName + "<" + sfromEmail + ">";
        string to = stoName + "<" + stoEmail + ">";
        string subject = "";

        try
        {
            subject = "Trans-Asia VMS Accreditation application for clarification.";
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
        sTxt = sTxt + "<td>&nbsp;" + Request.Form["__EVENTARGUMENT"] + "&nbsp;</td>";
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
        sb.Append("Re: Vendor information for clarification -- " + cVendorName + "<br><br>");
        sb.Append("This is to request for your clarification of the ff: <br><br>");
        //sb.Append("<a href='http://'<br><br>");
        sb.Append(sTxt);
        sb.Append("</p><br><br>");
        //sb.Append("We are happy to be doing business with you. Thank you and God bless your dealings.<br><br><br>");
        sb.Append("Very truly yours,<br><br>");
        sb.Append("Trans-Asia<br><br>");
        sb.Append("</td></tr>");
        sb.Append("<tr><td>");
        sb.Append("<p>&nbsp;</p>");
        //sb.Append("<b>Instructions:</b><br>");
        //sb.Append("&nbsp;&nbsp;1. Go to <a href='" + System.Configuration.ConfigurationManager.AppSettings["ServerUrl"] + "' target='_blank'>" + System.Configuration.ConfigurationManager.AppSettings["ServerUrl"] + "</a><br>");
        //sb.Append("&nbsp;&nbsp;2. Enter your Username and Password then click Login<br>");
        //sb.Append("&nbsp;&nbsp;3. Click Vendors for Authentication<br>");
        //sb.Append("&nbsp;&nbsp;4. Click Authenticate<br>");
        sb.Append("</td></tr>");
        sb.Append("<tr><td>");
        sb.Append("<p>&nbsp;</p><span style='font-size:10px; font-style:italic;'>Please do not reply to this auto-generated  message.&nbsp;</span>");
        sb.Append("</td></tr>");

        return MailTemplate.IntegrateBodyIntoTemplate(sb.ToString());
    }

}