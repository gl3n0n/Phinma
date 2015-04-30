using System;
using System.Configuration;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.IO.Compression;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using EBid.lib;
using EBid.lib.constant;
using EBid.lib.report;
using System.IO;
using System.Text;
using EBid.lib.bid.trans;
using EBid.lib.bid.data;
using EBid.lib.user.trans;
using EBid.lib.user.data;
using System.Data.Common;
using EBid.lib.auction.data;
using EBid.lib;
using EBid.lib.constant;


public partial class vsfviewOnly : System.Web.UI.Page
{
    SqlDataReader oReader;
    string connstring = "";
    string query;
    SqlCommand cmd;
    SqlConnection conn;
    int PurchasingId1;


    bool IsNumber(string text)
    {
       Regex regex = new Regex(@"^[-+]?[0-9]*\.?[0-9]+$");
       return regex.IsMatch(text);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
	connstring = HttpContext.Current.Session["ConnectionString"].ToString();
        if (Session["pageDetails"] == "" || Session["pageDetails"] == null)
        {
            Session["pageDetails"] = "index.aspx";
        }
        if (Request.QueryString["VSFId"] == null || Request.QueryString["VSFId"]== "")
        {
            SaveToDB_Init();
        }
        if (IsPostBack)
        {
            SaveToDB();
        }
        PopulateFields();

        //if (Request.UrlReferrer.ToString() != HttpContext.Current.Request.Url.AbsoluteUri)
        //{
        //    //Session["pageDetails"] = Request.UrlReferrer.ToString();
        //}

        //Response.Write(Session["pageDetails"].ToString());
    }



    protected void Page_LoadComplete(object sender, EventArgs e)
    {
    }


    void PopulateFields()
    {
        //Response.Write(Request.UrlReferrer.ToString());
        query = "SELECT t1.*, t2.BuyerFirstName + ' ' + t2.BuyerMidName + ' ' + t2.BuyerLastName AS BuyerName, t3.FirstName + ' ' + t3.MiddleName + ' ' + t3.LastName AS PurchasingName FROM tblVendorShortlistingForm t1, tblBuyers t2, tblPurchasing t3 WHERE t1.VSFId=@VSFId AND t2.BuyerId = t1.BuyerId AND t3.PurchasingId = t1.PurchasingId";
        //query = "sp_GetVendorInformation"; //##storedProcedure
        using (conn = new SqlConnection(connstring))
        {
            using (cmd = new SqlCommand(query, conn))
            {
                //cmd.CommandType = CommandType.StoredProcedure; //##storedProcedure
                cmd.Parameters.AddWithValue("@VSFId", Convert.ToInt32(Request.QueryString["VSFId"]));
                conn.Open();
                //Process results
                oReader = cmd.ExecuteReader();
                if (oReader.HasRows)
                {
                    while (oReader.Read())
                    {
                        VSFDate.Text = oReader["VSFDate"].ToString();
                        ProponentName.Text = oReader["ProponentName"].ToString();
                        PRNo.Text = oReader["PRNo"].ToString();
                        GroupDept.Text = oReader["GroupDept"].ToString();
                        ProjectName.Text = oReader["ProjectName"].ToString();
                        NumPotentialVendor.Text = oReader["NumPotentialVendor"].ToString();
                        PRAmount.Text = oReader["PRAmount"].ToString();
                        NumShortlistedVendor.Text = oReader["NumShortlistedVendor"].ToString();
                        PRDescription.Text = oReader["PRDescription"].ToString();
                        lblPreparedDate.Text = oReader["PreparedDt"].ToString();
                        lblApprovedDate.Text = oReader["ApprovedDt"].ToString();
                        ApprovedMemo.Checked = oReader["ApprovedMemo"].ToString()=="1" ? true : false;
                        BoardApproval.Checked = oReader["BoardApproval"].ToString() == "1" ? true : false;
                        EndorsementMemo.Checked = oReader["EndorsementMemo"].ToString() == "1" ? true : false;
                        ExcomApproval.Checked = oReader["ExcomApproval"].ToString() == "1" ? true : false;
                        Others.Checked = oReader["Others"].ToString() == "1" ? true : false;
                        OthersDesc.Text  = oReader["OthersDesc"].ToString();
                        Recomendatation.Value = oReader["Recomendatation"].ToString();
                        lblBuyer.Text = oReader["BuyerName"].ToString();
                        lblPurchasing.Text = oReader["PurchasingName"].ToString();

                        if (oReader["Status"].ToString() == "2")
                        {
                           lblMessage.Text = "VSF is currently being clarified.";
                           btnClarify.Visible = false;
                           Comment.Visible = false;
                           Recomendatation.Disabled = true;
                        }
                        if (oReader["Status"].ToString() == "3")
                        {
                           lblMessage.Text = "";
                           btnClarify.Visible = false;;
                           Comment.Visible = false;
                           Recomendatation.Disabled = true;
                        }
                    }
                }
            }
        }
    }


    void SaveToDB()
    {

        if(Request.Form["__EVENTTARGET"] == "HistoryBack")
        {
            //Request.QueryString["VSFId"] = "";
            //Response.Redirect(Session["pageDetails"].ToString());
        }

        if(Request.Form["__EVENTTARGET"] == "Reject")
        {
            query = "UPDATE tblVendorShortlistingForm SET Status=4 WHERE VSFId=@VSFId";
            //query = "sp_GetVendorInformation"; //##storedProcedure
            using (conn = new SqlConnection(connstring))
            {
                using (cmd = new SqlCommand(query, conn))
                {
                    //cmd.CommandType = CommandType.StoredProcedure; //##storedProcedure
                    cmd.Parameters.AddWithValue("@VSFId", Convert.ToInt32(Request.QueryString["VSFId"].ToString()));
                    conn.Open(); cmd.ExecuteNonQuery();
                }
            }
            SendEmailNotification();
            Request.QueryString["VSFId"] = "";
            Response.Redirect("vsfrejected.aspx");
        }

        if(Request.Form["__EVENTTARGET"] == "Clarify")
        {
            query = "UPDATE tblVendorShortlistingForm SET Status=2, Recomendatation=@Recomendatation WHERE VSFId=@VSFId";
            //query = "sp_GetVendorInformation"; //##storedProcedure
            using (conn = new SqlConnection(connstring))
            {
                using (cmd = new SqlCommand(query, conn))
                {
                    //cmd.CommandType = CommandType.StoredProcedure; //##storedProcedure
                    cmd.Parameters.AddWithValue("@VSFId", Convert.ToInt32(Request.QueryString["VSFId"].ToString()));
                    cmd.Parameters.AddWithValue("@Recomendatation", Recomendatation.Value);
                    conn.Open(); cmd.ExecuteNonQuery();
                }
            }

            if(Request.Form["Comment"].ToString()!=""){
                query = "INSERT INTO tblVSFComments (VSFId, UserId, Name, Comment, DateCreated) VALUES (@VSFId, @UserId, @Name, @Comment, getdate())";
                //query = "sp_GetVendorInformation"; //##storedProcedure
                using (conn = new SqlConnection(connstring))
                {
                    using (cmd = new SqlCommand(query, conn))
                    {
                        //cmd.CommandType = CommandType.StoredProcedure; //##storedProcedure
                        cmd.Parameters.AddWithValue("@VSFId", Convert.ToInt32(Request.QueryString["VSFId"].ToString()));
                        cmd.Parameters.AddWithValue("@UserId", Convert.ToInt32(Session["UserId"].ToString()));
                        cmd.Parameters.AddWithValue("@Name", Session["UserFullName"].ToString());
                        cmd.Parameters.AddWithValue("@Comment", Request.Form["Comment"].ToString());
                        conn.Open(); cmd.ExecuteNonQuery();
                    }
                }
            }

            SendEmailNotification();
            //Request.QueryString["VSFId"] = "";
            Response.Redirect(Session["pageDetails"].ToString());
        }

        if(Request.Form["__EVENTTARGET"] == "Approve")
        {

            query = "UPDATE tblVendorShortlistingForm SET Status=3, Recommendation=@Recomendatation ApprovedDt=getdate() WHERE VSFId=@VSFId";
            //query = "sp_GetVendorInformation"; //##storedProcedure
            using (conn = new SqlConnection(connstring))
            {
                using (cmd = new SqlCommand(query, conn))
                {
                    //cmd.CommandType = CommandType.StoredProcedure; //##storedProcedure
                    cmd.Parameters.AddWithValue("@VSFId", Convert.ToInt32(Request.QueryString["VSFId"].ToString()));
                    cmd.Parameters.AddWithValue("@Recomendatation", Recomendatation.Value);
                    conn.Open(); cmd.ExecuteNonQuery();
                }
            }

            if (Request.Form["Comment"].ToString() != "")
            {
                query = "INSERT INTO tblVSFComments (VSFId, UserId, Name, Comment, DateCreated) VALUES (@VSFId, @UserId, @Name, @Comment, getdate())";
                //query = "sp_GetVendorInformation"; //##storedProcedure
                using (conn = new SqlConnection(connstring))
                {
                    using (cmd = new SqlCommand(query, conn))
                    {
                        //cmd.CommandType = CommandType.StoredProcedure; //##storedProcedure
                        cmd.Parameters.AddWithValue("@VSFId", Convert.ToInt32(Request.QueryString["VSFId"].ToString()));
                        cmd.Parameters.AddWithValue("@UserId", Convert.ToInt32(Session["UserId"].ToString()));
                        cmd.Parameters.AddWithValue("@Name", Session["UserFullName"].ToString());
                        cmd.Parameters.AddWithValue("@Comment", Request.Form["Comment"].ToString());
                        conn.Open(); cmd.ExecuteNonQuery();
                    }
                }
            }

            SendEmailNotification();
            //Request.QueryString["VSFId"] = "";
            Response.Redirect("vsfapproved.aspx");
        }
        
    }

    void SaveToDB_Init()
    {
        
    }


    protected void repeaterVSFDetails_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
             //System.Web.UI.HtmlControls.HtmlInputHidden oHiddenVendors = ((System.Web.UI.HtmlControls.HtmlInputHidden)e.Item.FindControl("VendorId"));
            //((System.Web.UI.WebControls.Image)e.Item.FindControl("imgVendor")).Attributes.Add("onclick", "javascript: __doPostBack('deleteVendor', '" + ((DataRowView)e.Item.DataItem)["VendorID"].ToString() + "');");
            ((CheckBox)e.Item.FindControl("Selected")).Checked = (((DataRowView)e.Item.DataItem)["Selected"].ToString()) == "1" ? true : false;
        }
    }


    //############################################################
    //############################################################
    private bool SendEmailNotification()
    {
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
                cmd.Parameters.AddWithValue("@VSFId", Convert.ToInt32(Session["VSFId"]));
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

        //Response.Write(from + "<br>");
        //Response.Write(to + "<br>");
        //Response.Write(subject + "<br>");
        //Response.Write(CreateNotificationBody() + "<br>");
        //Response.Write(MailTemplate.GetTemplateLinkedResources(this) + "<br>");
        
        try
        {
            if (Request.Form["__EVENTTARGET"] == "Approve")
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
            }
            if (Request.Form["__EVENTTARGET"] == "Clarify")
            {
                subject = "VSF for Clarification";
                if (!MailHelper.SendEmail(MailTemplate.GetDefaultSMTPServer(),
                        from,
                        to,
                        subject,
                        CreateNotificationBodyClarify(oVSFId, oProjectName, fromName, toName),
                        MailTemplate.GetTemplateLinkedResources(this)))
                {	//if sending failed					
                    LogHelper.EventLogHelper.Log("Bid > Send Notification : Sending Failed to " + from, System.Diagnostics.EventLogEntryType.Error);
                }
                else
                {	//if sending successful
                    LogHelper.EventLogHelper.Log("Bid > Send Notification : Email Sent to " + from, System.Diagnostics.EventLogEntryType.Information);

                }
            }
            if (Request.Form["__EVENTTARGET"] == "Reject")
            {
                subject = "VSF Rejected";
                if (!MailHelper.SendEmail(MailTemplate.GetDefaultSMTPServer(),
                        from,
                        to,
                        subject,
                        CreateNotificationBodyReject(oVSFId, oProjectName, fromName, toName),
                        MailTemplate.GetTemplateLinkedResources(this)))
                {	//if sending failed					
                    LogHelper.EventLogHelper.Log("Bid > Send Notification : Sending Failed to " + from, System.Diagnostics.EventLogEntryType.Error);
                }
                else
                {	//if sending successful
                    LogHelper.EventLogHelper.Log("Bid > Send Notification : Email Sent to " + from, System.Diagnostics.EventLogEntryType.Information);

                }
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


    private string CreateNotificationBodyClarify(string oVSFId, string oProjectName, string fromName, string toName)
    {
        StringBuilder sb = new StringBuilder();

        string BuyersName1 = fromName;
        string PurchasingName1 = toName;

        sb.Append("<tr><td><p><strong>VSF for Clarificiation</strong> <br> Sent: " + DateTime.Now.ToLongDateString() + "</p> <p>From: " + BuyersName1 + "<br><br> To: " + PurchasingName1 + "<br><br> Subject: " + oProjectName + "<br><br> Dear Buyer, <br><br> Re: Vendor Shortlisting Form for Clarification – <strong>" + oProjectName + "</strong><br><br> This is to clarify Vendor Shortlisting Form with the ff:<br><br> <b>VSF Reference Number:</b> " + oVSFId + "<br> <b>Project Name:</b> " + oProjectName + "<br></p>&nbsp;<p>Very truly yours,<br><br><br> <strong>e-Sourcing Procurement</strong></p><p>&nbsp;</p> <p><strong>Instructions:</strong></p> <ol> <li>Go to <a href='https://e-sourcing.Trans-Asia.com.ph/'>https://e-sourcing.Trans-Asia.com.ph</a></li> <li>Enter your Username and Password then  click Login</li> <li>Under Vendor Shortlisting Form, click View Clarifications</li> <li>Click VSF Reference No: " + oVSFId + "</li> <li>Review / Endorse / VSF for Awarding</li> <li>Click Clarify to endorse the VSF</li> </ol> Please do not reply to this auto-generated  message.&nbsp;</td></tr>");

        return MailTemplate.IntegrateBodyIntoTemplate(sb.ToString());
    }


    private string CreateNotificationBodyReject(string oVSFId, string oProjectName, string fromName, string toName)
    {
        StringBuilder sb = new StringBuilder();

        string BuyersName1 = fromName;
        string PurchasingName1 = toName;

        sb.Append("<tr><td><p><strong>VSF Rejected</strong> <br> Sent: " + DateTime.Now.ToLongDateString() + "</p> <p>From: " + BuyersName1 + "<br><br> To: " + PurchasingName1 + "<br><br> Subject: " + oProjectName + "<br><br> Dear Buyer, <br><br> Re: Vendor Shortlisting Form Rejected – <strong>" + oProjectName + "</strong><br><br> This is to inform you that Vendor Shortlisting Form has been rejected with the ff:<br><br> <b>VSF Reference Number:</b> " + oVSFId + "<br> <b>Project Name:</b> " + oProjectName + "<br></p>&nbsp;<p>Very truly yours,<br><br><br> <strong>e-Sourcing Procurement</strong></p><p>&nbsp;</p> <p><strong>Instructions:</strong></p> <ol> <li>Go to <a href='https://e-sourcing.Trans-Asia.com.ph/'>https://e-sourcing.Trans-Asia.com.ph</a></li> <li>Enter your Username and Password then  click Login</li> <li>Under Vendor Shortlisting Form, click View Rejected</li> </ol> Please do not reply to this auto-generated  message.&nbsp;</td></tr>");

        return MailTemplate.IntegrateBodyIntoTemplate(sb.ToString());
    }
}