using System;
using System.Collections;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using EBid.lib.bid.trans;
using EBid.lib.bid.data;
using EBid.lib.constant;
using EBid.lib;
using System.IO;
using System.IO.Compression;
using System.Xml;
using System.Text;
using CalendarControl;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using EBid.ConnectionString;

public partial class web_buyerscreens_bidawardingchecklistclarify : System.Web.UI.Page
{

    protected void Page_LoadComplete(object sender, EventArgs e)
    {
        //determine and show who is clarifying
        string connstring = HttpContext.Current.Session["ConnectionString"].ToString();
        string sCommand = "SELECT FirstName+' '+LastName as ResponseClarifyTo from tblBidAwardingCommittee where BACId=" + Session["ClarifiedBy"];
        SqlDataReader oReader = SqlHelper.ExecuteReader(connstring, CommandType.Text, sCommand);
        if (oReader.HasRows)
        {
            oReader.Read();
            ResponseClarifyTo.Value = oReader["ResponseClarifyTo"].ToString();
        }
        oReader.Close();

        
        if (IsPostBack)
        {
            sCommand = "UPDATE tblBACExecutiveSummary SET Comment='" + (Request.Form["executive_summary"].Replace("\n", "<br />")).Replace("'", "''") + "' WHERE BidRefNo= " + Session["BuyerBidForBac"] + " ";
            SqlHelper.ExecuteNonQuery(connstring, CommandType.Text, sCommand);

            string control1 = Request.Form["__EVENTTARGET"];
            //Response.Write(control1);
            if (control1 == "BuyerBidForBac")
            {
                //ShowBidItemTenders();
            }
            else if (control1 == "Clarify")
            {
                doClarify();
            }
            else if (control1 == "Approve")
            {
                doApprove();
            }
            else if (control1 == "Reject")
            {
                doReject();
            }
        }

        // awarding committee
        SqlDataSource dsApprover = (SqlDataSource)bac_bidApprovingCommittee1.FindControl("dsApprover");
        dsApprover.SelectCommand = "select BACId, NAME1, ApprovingLimit, ApprovedDt from ( SELECT t1.BACId, t1.LastName + ', ' + t1.FirstName + ' ' + t1.MiddleName AS Name1, t2.ApprovedDt_1 ApprovedDt, 1 AS ApprovingLimit  FROM tblBidAwardingCommittee t1, tblBacBidItems t2   WHERE t1.BACId = t2.Approver_1 AND t2.BidRefNo = " + Session["BuyerBidForBac"] + " UNION SELECT t1.BACId, t1.LastName + ', ' + t1.FirstName + ' ' + t1.MiddleName AS Name1, t2.ApprovedDt_2 ApprovedDt, 2 AS ApprovingLimit  FROM tblBidAwardingCommittee t1, tblBacBidItems t2   WHERE t1.BACId = t2.Approver_2 AND t2.BidRefNo = " + Session["BuyerBidForBac"] + " UNION SELECT t1.BACId, t1.LastName + ', ' + t1.FirstName + ' ' + t1.MiddleName AS Name1, t2.ApprovedDt_3 ApprovedDt, 3 AS ApprovingLimit  FROM tblBidAwardingCommittee t1, tblBacBidItems t2   WHERE t1.BACId = t2.Approver_3 AND t2.BidRefNo = " + Session["BuyerBidForBac"] + " UNION SELECT t1.BACId, t1.LastName + ', ' + t1.FirstName + ' ' + t1.MiddleName AS Name1, t2.ApprovedDt_4 ApprovedDt, 4 AS ApprovingLimit  FROM tblBidAwardingCommittee t1, tblBacBidItems t2   WHERE t1.BACId = t2.Approver_4 AND t2.BidRefNo = " + Session["BuyerBidForBac"] + " UNION SELECT t1.BACId, t1.LastName + ', ' + t1.FirstName + ' ' + t1.MiddleName AS Name1, t2.ApprovedDt_5 ApprovedDt, 5 AS ApprovingLimit  FROM tblBidAwardingCommittee t1, tblBacBidItems t2   WHERE t1.BACId = t2.Approver_5 AND t2.BidRefNo = " + Session["BuyerBidForBac"] + " UNION SELECT t1.BACId, t1.LastName + ', ' + t1.FirstName + ' ' + t1.MiddleName AS Name1, t2.ApprovedDt_6 ApprovedDt, 6 AS ApprovingLimit  FROM tblBidAwardingCommittee t1, tblBacBidItems t2   WHERE t1.BACId = t2.Approver_6 AND t2.BidRefNo = " + Session["BuyerBidForBac"] + " UNION SELECT t1.BACId, t1.LastName + ', ' + t1.FirstName + ' ' + t1.MiddleName AS Name1, t2.ApprovedDt_7 ApprovedDt, 7 AS ApprovingLimit  FROM tblBidAwardingCommittee t1, tblBacBidItems t2   WHERE t1.BACId = t2.Approver_7 AND t2.BidRefNo = " + Session["BuyerBidForBac"] + " UNION SELECT t1.BACId, t1.LastName + ', ' + t1.FirstName + ' ' + t1.MiddleName AS Name1, t2.ApprovedDt_8 ApprovedDt,  8 AS ApprovingLimit  FROM tblBidAwardingCommittee t1, tblBacBidItems t2   WHERE t1.BACId = t2.Approver_8 AND t2.BidRefNo = " + Session["BuyerBidForBac"] + " UNION SELECT t1.BACId, t1.LastName + ', ' + t1.FirstName + ' ' + t1.MiddleName AS Name1, t2.ApprovedDt_9 ApprovedDt,  9 AS ApprovingLimit  FROM tblBidAwardingCommittee t1, tblBacBidItems t2   WHERE t1.BACId = t2.Approver_9 AND t2.BidRefNo = " + Session["BuyerBidForBac"] + " UNION SELECT t1.BACId, t1.LastName + ', ' + t1.FirstName + ' ' + t1.MiddleName AS Name1, t2.ApprovedDt_10 ApprovedDt, 10 AS ApprovingLimit  FROM tblBidAwardingCommittee t1, tblBacBidItems t2 WHERE t1.BACId = t2.Approver_10 AND t2.BidRefNo = " + Session["BuyerBidForBac"] + ") as table_1 order by ApprovingLimit";
        dsApprover.DataBind();
        Repeater RepeaterApprover1 = (Repeater)bac_bidApprovingCommittee1.FindControl("RepeaterApprover1");
        RepeaterApprover1.DataSourceID = null;
        RepeaterApprover1.DataBind();
        RepeaterApprover1.DataSourceID = "dsApprover";
        RepeaterApprover1.DataBind();

        sCommand = "SELECT * FROM tblBACExecutiveSummary WHERE BidRefNo=" + Session["BuyerBidForBac"] + " ";
        oReader = SqlHelper.ExecuteReader(connstring, CommandType.Text, sCommand);
        while (oReader.Read())
        {
            executive_summary.Text = oReader["Comment"].ToString().Replace("<br />", "\n");
        }
        oReader.Close();
    }

    protected void doClarify()
    {
        string connstring = HttpContext.Current.Session["ConnectionString"].ToString();
        string sCommand;
        string sLastCommented = "";

        sCommand = "SELECT ClarifyDt_0, ClarifyDt_1, ClarifyDt_2, ClarifyDt_3, ClarifyDt_4, ClarifyDt_5, ClarifyDt_6, ClarifyDt_7, ClarifyDt_8, ClarifyDt_9, ClarifyDt_10 FROM tblBacBidItems WHERE BacRefNo=" + Session["BuyerBacRefNo"];
        SqlDataReader oReader = SqlHelper.ExecuteReader(connstring, CommandType.Text, sCommand);
        if (oReader.HasRows)
        {
            oReader.Read();
            //string i = oReader["ItemDesc"].ToString();
            if (oReader["ClarifyDt_0"].ToString() != "") sLastCommented = "ApprovedDt_00";
            if (oReader["ClarifyDt_1"].ToString() != "") sLastCommented = "ApprovedDt_0";
            if (oReader["ClarifyDt_2"].ToString() != "") sLastCommented = "ApprovedDt_1";
            if (oReader["ClarifyDt_3"].ToString() != "") sLastCommented = "ApprovedDt_2";
            if (oReader["ClarifyDt_4"].ToString() != "") sLastCommented = "ApprovedDt_3";
            if (oReader["ClarifyDt_5"].ToString() != "") sLastCommented = "ApprovedDt_4";
            if (oReader["ClarifyDt_6"].ToString() != "") sLastCommented = "ApprovedDt_5";
            if (oReader["ClarifyDt_7"].ToString() != "") sLastCommented = "ApprovedDt_6";
            if (oReader["ClarifyDt_8"].ToString() != "") sLastCommented = "ApprovedDt_7";
            if (oReader["ClarifyDt_9"].ToString() != "") sLastCommented = "ApprovedDt_8";
            if (oReader["ClarifyDt_10"].ToString() != "") sLastCommented = "ApprovedDt_9";
            //sLastCommented = "ApprovedDt_9";
            //Response.Write(sLastCommented);
        }
        oReader.Close();
        //Session["sLastCommented"] = sLastCommented;

        // Clear Approved and Clarfied from tblBacBidItems table
        sCommand = "UPDATE tblBacBidItems SET ClarifyDt_0=NULL, ClarifyDt_1=NULL, ClarifyDt_2=NULL, ClarifyDt_3=NULL, ClarifyDt_4=NULL, ClarifyDt_5=NULL, ClarifyDt_6=NULL, ClarifyDt_7=NULL, ClarifyDt_8=NULL, ClarifyDt_9=NULL, ClarifyDt_10=NULL WHERE BacRefNo=" + Session["BuyerBacRefNo"];
        SqlHelper.ExecuteNonQuery(connstring, CommandType.Text, sCommand);

        //Response.Write(Request.Form["txtClarify"]);

        // Insert to tblBACComments table
        sCommand = "INSERT INTO tblBACComments (BidRefNo, UserId, Comment, DatePosted) VALUES (";
        sCommand = sCommand + Session["BuyerBidForBac"].ToString() + ", " + Session["UserId"] + ", '" + Request.Form["txtClarify"].Replace("'","''") + "', GETDATE())";
        SqlHelper.ExecuteNonQuery(connstring, CommandType.Text, sCommand);

        // update item tblBacBidItems as endorsed/response to whoever is clarifying
        sCommand = "UPDATE tblBacBidItems SET Status=1  WHERE BacRefNo=" + Session["BuyerBacRefNo"];
        SqlHelper.ExecuteNonQuery(connstring, CommandType.Text, sCommand);
        //Response.Write(Request.Form["txtClarify"].Replace("'", "''"));

        if (SendEmailNotificationApprove())
        {
            Session["Message"] = "Notification sent successfully.";
        }
        else
        {
            // failed
            Session["Message"] = "Failed to send notification. Please try again or contact adminitrator for assistance.";
        }
        Session["Message"] = "";
        Response.Redirect("bacforclarifications.aspx");

        
    }

    protected void doApprove()
    {
        string connstring = HttpContext.Current.Session["ConnectionString"].ToString();
        string sCommand;

        // add row to tblBACComments table
        sCommand = "INSERT INTO tblBACComments (BidRefNo, UserId, Comment, DatePosted) VALUES (";
        sCommand = sCommand + Session["BuyerBidForBac"].ToString() + ", " + Session["UserId"] + ", '" + Request.Form["txtClarify"].Replace("'","''") + "', GETDATE())";
        SqlHelper.ExecuteNonQuery(connstring, CommandType.Text, sCommand);

        // update tblBacBidItems table
        sCommand = "UPDATE tblBacBidItems SET Status=1, ApprovedDt_0=GETDATE(), ClarifyDt_0=NULL, ClarifyDt_1=NULL, ClarifyDt_2=NULL, ClarifyDt_3=NULL, ClarifyDt_4=NULL, ClarifyDt_5=NULL, ClarifyDt_6=NULL, ClarifyDt_7=NULL, ClarifyDt_8=NULL, ClarifyDt_9=NULL, ClarifyDt_10=NULL WHERE BacRefNo=" + Session["BuyerBacRefNo"];
        SqlHelper.ExecuteNonQuery(connstring, CommandType.Text, sCommand);
        //Response.Write(Request.Form["txtClarify"]);
        //Response.Write(Request.Form["txtApproved"]);
        Response.Redirect("bacendorsed.aspx");
    }


    protected void doReject()
    {
        string connstring = HttpContext.Current.Session["ConnectionString"].ToString();
        string sCommand;

        // add row to tblBACComments table
        sCommand = "INSERT INTO tblBACComments (BidRefNo, UserId, Comment, DatePosted) VALUES (";
        sCommand = sCommand + Session["BuyerBidForBac"].ToString() + ", " + Session["UserId"] + ", '" + Request.Form["txtClarify"].Replace("'", "''") + "', GETDATE())";
        SqlHelper.ExecuteNonQuery(connstring, CommandType.Text, sCommand);

        sCommand = "UPDATE tblBacBidItems SET Status=4 WHERE BacRefNo=" + Session["BuyerBacRefNo"];
        SqlHelper.ExecuteNonQuery(connstring, CommandType.Text, sCommand);

        if (SendEmailNotificationReject())
        {
            Session["Message"] = "Notification sent successfully.";
        }
        else
        {
            // failed
            Session["Message"] = "Failed to send notification. Please try again or contact adminitrator for assistance.";
        }
        Session["Message"] = "";
        Response.Redirect("bacrejected.aspx");
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        
    }

    private bool SendEmailNotificationApprove()
    {
        //"Fistname Lastname" <email@globetel.com.ph>
        //"Fistname Lastname" <email@globetel.com.ph>
        string sCommand;
        string connstring = HttpContext.Current.Session["ConnectionString"].ToString();
        SqlDataReader oReader;
        bool success = false;


        string fromName = "";
        string fromEmail = "";
        string from = "";
        string toName = "";
        string toEmail = "";
        string to = "";
        string subject = "BAC For Approval";

        //GET STATUS IF FOR APPROVAL OR ALREADY AWARDED
        string Status = "";
        sCommand = "SELECT Status FROM tblBacBidItems WHERE BidRefNo=" + Session["BuyerBidForBac"];
        oReader = SqlHelper.ExecuteReader(connstring, CommandType.Text, sCommand);
        if (oReader.HasRows)
        {
            oReader.Read();
            Status = oReader["Status"].ToString();
        } oReader.Close();



        //GET BUYER EMAIL AS THE SENDER
        //sCommand = "SELECT t1.BuyerFirstName + ' ' + t1.BuyerMidName + ' ' + t1.BuyerLastName AS Name1, t1.EmailAdd  FROM tblBuyers t1, tblBacBidItems t2 WHERE t1.BuyerID=t2.BuyerId AND t2.BidRefNo=" + Session["BuyerBidForBac"];

        //GET BUYER EMAIL AS THE SENDER
        sCommand = "SELECT Name1, EmailAdd FROM ( ";
        sCommand = sCommand + "SELECT t1.BuyerID UserId, t1.BuyerFirstName + ' ' + t1.BuyerMidName + ' ' + t1.BuyerLastName AS Name1, t1.EmailAdd  FROM tblBuyers t1, tblBacBidItems t2 WHERE t1.BuyerID=t2.BuyerId AND t2.BidRefNo=" + Session["BuyerBidForBac"] + " ";
        sCommand = sCommand + "union ";
        sCommand = sCommand + "SELECT t1.PurchasingID UserId, t1.FirstName + ' ' + t1.MiddleName + ' ' + t1.LastName AS Name1, t1.EmailAdd  FROM tblPurchasing t1 ";
        sCommand = sCommand + "union ";
        sCommand = sCommand + "SELECT t1.BACID UserId, t1.FirstName + ' ' + t1.MiddleName + ' ' + t1.LastName AS Name1, t1.EmailAdd  FROM tblBidAwardingCommittee t1 ) t2 ";
        sCommand = sCommand + "WHERE UserId = (select TOP 1 ToUserId from tblBacClarifications where BidRefNo = " + Session["BuyerBidForBac"] + " ORDER BY ID desc)";

        oReader = SqlHelper.ExecuteReader(connstring, CommandType.Text, sCommand);
        if (oReader.HasRows)
        {
            oReader.Read();
            fromName = oReader["Name1"].ToString();
            fromEmail = oReader["EmailAdd"].ToString();
            from = '"' + oReader["Name1"].ToString() + '"' + " <" + oReader["EmailAdd"].ToString() + ">";
        } oReader.Close();


        //GET APPROVER EMAIL AS THE RECEPIENT OR IF NULL, NOTIFY BUYER AS AWARDED BAC
        sCommand = "SELECT FirstName+ ' ' + MiddleName + ' '+LastName as Name1, EmailAdd from tblBidAwardingCommittee where BACId=" + Session["ClarifiedBy"];

        oReader = SqlHelper.ExecuteReader(connstring, CommandType.Text, sCommand);
        if (oReader.HasRows)
        {
            oReader.Read();
            toName = oReader["Name1"].ToString();
            toEmail = oReader["EmailAdd"].ToString();
            to = '"' + oReader["Name1"].ToString() + '"' + " <" + oReader["EmailAdd"].ToString() + ">";
        } oReader.Close();

        //Response.Write(from + "<br>");
        //Response.Write(to + "<br>");
        //Response.Write(subject + "<br>");
        //Response.Write(CreateNotificationBodyApprove() + "<br>");
        //Response.Write(MailTemplate.GetTemplateLinkedResources(this) + "<br>");
        try
        {
            if (!MailHelper.SendEmail(MailTemplate.GetDefaultSMTPServer(),
                    from,
                    to,
                    subject,
                    CreateNotificationBodyApprove(),
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

    private bool SendEmailNotificationReject()
    {
        //"Fistname Lastname" <email@globetel.com.ph>
        //"Fistname Lastname" <email@globetel.com.ph>
        string sCommand;
        string connstring = HttpContext.Current.Session["ConnectionString"].ToString();
        SqlDataReader oReader;
        bool success = false;


        string fromName = "";
        string fromEmail = "";
        string from = "";
        string toName = "";
        string toEmail = "";
        string to = "";
        string subject = "Rejected BAC";


        //GET PURCHASING EMAIL AS THE SENDER
        sCommand = "SELECT t1.PurchasingID, t2.FirstName + ' ' + t2.MiddleName + ' ' + t2.LastName AS Name1, CONVERT(VARCHAR(17), t3.ApprovedDt_0, 113) ApprovedDt, t2.EmailAdd  FROM tblSupervisor t1, tblPurchasing t2, tblBacBidItems t3 WHERE t1.PurchasingID=t2.PurchasingID and t1.BuyerId = t3.buyerId AND t2.PurchasingID=t3.Approver_0 and t3.BidRefNo=" + Session["BuyerBidForBac"];
        oReader = SqlHelper.ExecuteReader(connstring, CommandType.Text, sCommand);
        if (oReader.HasRows)
        {
            oReader.Read();
            fromName = oReader["Name1"].ToString();
            fromEmail = oReader["EmailAdd"].ToString();
            from = '"' + oReader["Name1"].ToString() + '"' + " <" + oReader["EmailAdd"].ToString() + ">";
        } oReader.Close();


        //GET BUYER EMAIL AS THE RECEPIENT
        sCommand = "SELECT t1.BuyerFirstName + ' ' + t1.BuyerMidName + ' ' + t1.BuyerLastName AS Name1, t1.EmailAdd  FROM tblBuyers t1, tblBacBidItems t2 WHERE t1.BuyerID=t2.BuyerId AND t2.BidRefNo=" + Session["BuyerBidForBac"];
        oReader = SqlHelper.ExecuteReader(connstring, CommandType.Text, sCommand);
        if (oReader.HasRows)
        {
            oReader.Read();
            toName = oReader["Name1"].ToString();
            toEmail = oReader["EmailAdd"].ToString();
            to = '"' + oReader["Name1"].ToString() + '"' + " <" + oReader["EmailAdd"].ToString() + ">";
        } oReader.Close();

        //Response.Write(from + "<br>");
        //Response.Write(to + "<br>");
        //Response.Write(subject + "<br>");
        //Response.Write(CreateNotificationBodyReject() + "<br>");
        //Response.Write(MailTemplate.GetTemplateLinkedResources(this) + "<br>");
        try
        {
            if (!MailHelper.SendEmail(MailTemplate.GetDefaultSMTPServer(),
                    from,
                    to,
                    subject,
                    CreateNotificationBodyApprove(),
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

    private bool SendEmailNotificationClarify()
    {
        //"Fistname Lastname" <email@globetel.com.ph>
        //"Fistname Lastname" <email@globetel.com.ph>
        string sCommand;
        string connstring = HttpContext.Current.Session["ConnectionString"].ToString();
        SqlDataReader oReader;
        bool success = false;


        string fromName = "";
        string fromEmail = "";
        string from = "";
        string toName = "";
        string toEmail = "";
        string to = "";
        string subject = "BAC For Clarification";


        //GET PURCHASING EMAIL AS THE SENDER
        sCommand = "SELECT t1.PurchasingID, t2.FirstName + ' ' + t2.MiddleName + ' ' + t2.LastName AS Name1, CONVERT(VARCHAR(17), t3.ApprovedDt_0, 113) ApprovedDt, t2.EmailAdd  FROM tblSupervisor t1, tblPurchasing t2, tblBacBidItems t3 WHERE t1.PurchasingID=t2.PurchasingID and t1.BuyerId = t3.buyerId AND t2.PurchasingID=t3.Approver_0 and t3.BidRefNo=" + Session["BuyerBidForBac"];
        oReader = SqlHelper.ExecuteReader(connstring, CommandType.Text, sCommand);
        if (oReader.HasRows)
        {
            oReader.Read();
            fromName = oReader["Name1"].ToString();
            fromEmail = oReader["EmailAdd"].ToString();
            from = '"' + oReader["Name1"].ToString() + '"' + " <" + oReader["EmailAdd"].ToString() + ">";
        } oReader.Close();


        //GET BUYER EMAIL AS THE RECEPIENT
        sCommand = "SELECT PurchasingID, Name1, EmailAdd FROM (SELECT t2.PurchasingID, t2.FirstName + ' ' + t2.MiddleName + ' ' + t2.LastName AS Name1, t2.EmailAdd  FROM tblPurchasing t2, tblBacBidItems t3 WHERE t2.PurchasingID=t3.Approver_0 and t3.BidRefNo= " + Session["BuyerBidForBac"] + " UNION SELECT t1.BuyerId PurchasingID, t1.BuyerLastName + ', ' + t1.BuyerFirstName + ' ' + t1.BuyerMidName AS Name1, t1.EmailAdd FROM tblBuyers t1, tblBacBidItems t2 WHERE t1.BuyerId = t2.BuyerId AND t2.BidRefNo = " + Session["BuyerBidForBac"] + " UNION SELECT t1.BACId PurchasingID, t1.LastName + ', ' + t1.FirstName + ' ' + t1.MiddleName AS Name1, t1.EmailAdd FROM tblBidAwardingCommittee t1 ) as a1 INNER JOIN (SELECT TOP 1 * FROM tblBACClarifications d  WHERE  d.BidRefNo = " + Session["BuyerBidForBac"] + " ORDER BY d.DatePosted desc) t2 ON t2.FrUserId = a1.PurchasingID";
        oReader = SqlHelper.ExecuteReader(connstring, CommandType.Text, sCommand);
        if (oReader.HasRows)
        {
            oReader.Read();
            toName = oReader["Name1"].ToString();
            toEmail = oReader["EmailAdd"].ToString();
            to = '"' + oReader["Name1"].ToString() + '"' + " <" + oReader["EmailAdd"].ToString() + ">";
        } oReader.Close();

        Response.Write(from + "<br>");
        Response.Write(to + "<br>");
        Response.Write(subject + "<br>");
        Response.Write(CreateNotificationBodyClarify() + "<br>");
        Response.Write(MailTemplate.GetTemplateLinkedResources(this) + "<br>");
        try
        {
            if (!MailHelper.SendEmail(MailTemplate.GetDefaultSMTPServer(),
                    from,
                    to,
                    subject,
                    CreateNotificationBodyClarify(),
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

    private string CreateNotificationBodyApprove()
    {
        StringBuilder sb = new StringBuilder();

        string sCommand;
        string BuyersName1 = "";
        string ApproverName1 = "";
        string connstring = HttpContext.Current.Session["ConnectionString"].ToString();
        SqlDataReader oReader;


        //GET BUYER EMAIL AS THE SENDER
        //sCommand = "SELECT t1.BuyerFirstName + ' ' + t1.BuyerMidName + ' ' + t1.BuyerLastName AS Name1 FROM tblBuyers t1, tblBacBidItems t2 WHERE t1.BuyerID=t2.BuyerId AND t2.BidRefNo=" + Session["BuyerBidForBac"];

        //GET BUYER EMAIL AS THE SENDER
        sCommand = "SELECT Name1, EmailAdd FROM ( ";
        sCommand = sCommand + "SELECT t1.BuyerID UserId, t1.BuyerFirstName + ' ' + t1.BuyerMidName + ' ' + t1.BuyerLastName AS Name1, t1.EmailAdd  FROM tblBuyers t1, tblBacBidItems t2 WHERE t1.BuyerID=t2.BuyerId AND t2.BidRefNo=" + Session["BuyerBidForBac"] + " ";
        sCommand = sCommand + "union ";
        sCommand = sCommand + "SELECT t1.PurchasingID UserId, t1.FirstName + ' ' + t1.MiddleName + ' ' + t1.LastName AS Name1, t1.EmailAdd  FROM tblPurchasing t1 ";
        sCommand = sCommand + "union ";
        sCommand = sCommand + "SELECT t1.BACID UserId, t1.FirstName + ' ' + t1.MiddleName + ' ' + t1.LastName AS Name1, t1.EmailAdd  FROM tblBidAwardingCommittee t1 ) t2 ";
        sCommand = sCommand + "WHERE UserId = (select TOP 1 ToUserId from tblBacClarifications where BidRefNo = " + Session["BuyerBidForBac"] + " ORDER BY ID desc)";

        oReader = SqlHelper.ExecuteReader(connstring, CommandType.Text, sCommand);
        if (oReader.HasRows)
        {
            oReader.Read();
            BuyersName1 = oReader["Name1"].ToString();
        } oReader.Close();

        sCommand = "SELECT FirstName+ ' ' + MiddleName + ' '+LastName as Name1, EmailAdd from tblBidAwardingCommittee where BACId=" + Session["ClarifiedBy"];
        oReader = SqlHelper.ExecuteReader(connstring, CommandType.Text, sCommand);
        if (oReader.HasRows)
        {
            oReader.Read();
            ApproverName1 = oReader["Name1"].ToString();
        } oReader.Close();

        // Awarded To
        string sTxt = "<table border='1' style='font-size:12px'><tr>";
        sTxt = sTxt + "<td><strong>&nbsp;Item #</strong></td>";
        sTxt = sTxt + "<td><strong>&nbsp;Item Details</strong></td>";
        sTxt = sTxt + "<td><strong>&nbsp;Vendor Name</strong></td>";
        sTxt = sTxt + "<td><strong>&nbsp;Qty</strong></td>";
        sTxt = sTxt + "<td><strong>&nbsp;Total</strong></td>";
        sTxt = sTxt + "<td><strong>&nbsp;Ranking</strong></td>";
        sTxt = sTxt + "</tr>";
        sCommand = "SELECT BidDetailNo, ItemName, VendorName, Qty, CONVERT(VARCHAR(20), CONVERT(MONEY, TotalCost), 1) TotalCost, Ranking ";
        sCommand = sCommand + "FROM tblBACEvaluationDetails WHERE BidRefNo=" + Session["BuyerBidForBac"] + " AND Chkd=1";
        oReader = SqlHelper.ExecuteReader(connstring, CommandType.Text, sCommand);
        if (oReader.HasRows)
        {
            while (oReader.Read())
            {
                sTxt = sTxt + "<tr>";
                sTxt = sTxt + "<td>";
                sTxt = sTxt + "&nbsp;" + oReader["BidDetailNo"].ToString() + "&nbsp;";
                sTxt = sTxt + "</td>";
                sTxt = sTxt + "<td>";
                sTxt = sTxt + "&nbsp;" + oReader["ItemName"].ToString() + "&nbsp;";
                sTxt = sTxt + "</td>";
                sTxt = sTxt + "<td>";
                sTxt = sTxt + "&nbsp;" + oReader["VendorName"].ToString() + "&nbsp;";
                sTxt = sTxt + "</td>";
                sTxt = sTxt + "<td>";
                sTxt = sTxt + "&nbsp;" + oReader["Qty"].ToString() + "&nbsp;";
                sTxt = sTxt + "</td>";
                sTxt = sTxt + "<td>";
                sTxt = sTxt + "&nbsp;" + oReader["TotalCost"].ToString() + "&nbsp;";
                sTxt = sTxt + "</td>";
                sTxt = sTxt + "<td>";
                sTxt = sTxt + "&nbsp;" + oReader["Ranking"].ToString() + "&nbsp;";
                sTxt = sTxt + "</td>";
                sTxt = sTxt + "</tr>";
            }
        }
        sTxt = sTxt + "<tr></table>";

        string ItemDesc = "";
        sCommand = "SELECT ItemDesc FROM tblBacBidItems WHERE BidRefNo=" + Session["BuyerBidForBac"];
        oReader = SqlHelper.ExecuteReader(connstring, CommandType.Text, sCommand);
        if (oReader.HasRows)
        {
            oReader.Read();
            ItemDesc = oReader["ItemDesc"].ToString();
        } oReader.Close();

        //sb.Append("<tr><td align='right'><h5>" + DateTime.Now.ToLongDateString() + "</h5></td></tr>");
        sb.Append("<tr><td><p><strong>BAC for Approval</strong> <br> Sent: " + DateTime.Now.ToLongDateString() + "</p> <p>From: " + BuyersName1 + "<br><br> To: " + ApproverName1 + "<br><br> Subject: " + ItemDesc + "<br><br> Dear Bid Award Approvers, <br><br> Re: Request for Bid Award Approval – <strong>" + ItemDesc + "</strong><br><br> This is to request for your Bid Award Approval of the ff:<br><br> <b>Bid Reference Number:</b> " + Session["BuyerBidForBac"] + "<br> <b>Bid Event Name:</b> " + ItemDesc + "<br> </p>  <p>Very truly yours,<br><br><br> <strong>e-Sourcing Procurement</strong></p><p>&nbsp;</p> <p><strong>Instructions:</strong></p> <ol> <li>Go to <a href='https://e-sourcing.Trans-Asia.com.ph/'>https://e-sourcing.Trans-Asia.com.ph</a></li> <li>Enter your Username and Password then  click Login</li> <li>Click Received Bid Events for Awarding</li> <li>Click Bid Events Name</li> <li>Review / Endorse / Approve Bid event  for Awarding</li> <li>Click Clarify if you have clarification  or click Approved to award Bid Events</li> </ol> Please do not reply to this auto-generated  message.&nbsp;</td></tr>");

        return MailTemplate.IntegrateBodyIntoTemplate(sb.ToString());
    }

    private string CreateNotificationBodyAwarded()
    {
        StringBuilder sb = new StringBuilder();

        string sCommand;
        string BuyersName1 = "";
        string ApproverName1 = "";
        string connstring = HttpContext.Current.Session["ConnectionString"].ToString();
        SqlDataReader oReader;

        sCommand = "SELECT t1.BuyerFirstName + ' ' + t1.BuyerMidName + ' ' + t1.BuyerLastName AS Name1 FROM tblBuyers t1, tblBacBidItems t2 WHERE t1.BuyerID=t2.BuyerId AND t2.BidRefNo=" + Session["BuyerBidForBac"];
        oReader = SqlHelper.ExecuteReader(connstring, CommandType.Text, sCommand);
        if (oReader.HasRows)
        {
            oReader.Read();
            BuyersName1 = oReader["Name1"].ToString();
        } oReader.Close();

        sCommand = "SELECT t1.BACId, t1.LastName + ', ' + t1.FirstName + ' ' + t1.MiddleName AS Name1, t1.EmailAdd  FROM tblBidAwardingCommittee t1, tblBacBidItems t2   WHERE t1.BACId = t2.Approver_1 AND t2.BidRefNo = " + Session["BuyerBidForBac"];
        oReader = SqlHelper.ExecuteReader(connstring, CommandType.Text, sCommand);
        if (oReader.HasRows)
        {
            oReader.Read();
            ApproverName1 = oReader["Name1"].ToString();
        } oReader.Close();

        // Awarded To
        string sTxt = "<table border='1' style='font-size:12px'><tr>";
        sTxt = sTxt + "<td><strong>&nbsp;Item #</strong></td>";
        sTxt = sTxt + "<td><strong>&nbsp;Item Details</strong></td>";
        sTxt = sTxt + "<td><strong>&nbsp;Vendor Name</strong></td>";
        sTxt = sTxt + "<td><strong>&nbsp;Qty</strong></td>";
        sTxt = sTxt + "<td><strong>&nbsp;Total</strong></td>";
        sTxt = sTxt + "<td><strong>&nbsp;Ranking</strong></td>";
        sTxt = sTxt + "</tr>";
        sCommand = "SELECT BidDetailNo, ItemName, VendorName, Qty, CONVERT(VARCHAR(20), CONVERT(MONEY, TotalCost), 1) TotalCost, Ranking ";
        sCommand = sCommand + "FROM tblBACEvaluationDetails WHERE BidRefNo=" + Session["BuyerBidForBac"] + " AND Chkd=1";
        oReader = SqlHelper.ExecuteReader(connstring, CommandType.Text, sCommand);
        if (oReader.HasRows)
        {
            while (oReader.Read())
            {
                sTxt = sTxt + "<tr>";
                sTxt = sTxt + "<td>";
                sTxt = sTxt + "&nbsp;" + oReader["BidDetailNo"].ToString() + "&nbsp;";
                sTxt = sTxt + "</td>";
                sTxt = sTxt + "<td>";
                sTxt = sTxt + "&nbsp;" + oReader["ItemName"].ToString() + "&nbsp;";
                sTxt = sTxt + "</td>";
                sTxt = sTxt + "<td>";
                sTxt = sTxt + "&nbsp;" + oReader["VendorName"].ToString() + "&nbsp;";
                sTxt = sTxt + "</td>";
                sTxt = sTxt + "<td>";
                sTxt = sTxt + "&nbsp;" + oReader["Qty"].ToString() + "&nbsp;";
                sTxt = sTxt + "</td>";
                sTxt = sTxt + "<td>";
                sTxt = sTxt + "&nbsp;" + oReader["TotalCost"].ToString() + "&nbsp;";
                sTxt = sTxt + "</td>";
                sTxt = sTxt + "<td>";
                sTxt = sTxt + "&nbsp;" + oReader["Ranking"].ToString() + "&nbsp;";
                sTxt = sTxt + "</td>";
                sTxt = sTxt + "</tr>";
            }
        }
        oReader.Close();
        sTxt = sTxt + "<tr></table>";

        // Approvers
        string oApprovers = "";
        sCommand = "select BACId, NAME1, ApprovingLimit, ApprovedDt from ( SELECT t1.BACId, t1.LastName + ', ' + t1.FirstName + ' ' + t1.MiddleName AS Name1, t2.ApprovedDt_1 ApprovedDt, 1 AS ApprovingLimit  FROM tblBidAwardingCommittee t1, tblBacBidItems t2   WHERE t1.BACId = t2.Approver_1 AND t2.BidRefNo = " + Session["BuyerBidForBac"] + " UNION SELECT t1.BACId, t1.LastName + ', ' + t1.FirstName + ' ' + t1.MiddleName AS Name1, t2.ApprovedDt_2 ApprovedDt, 2 AS ApprovingLimit  FROM tblBidAwardingCommittee t1, tblBacBidItems t2   WHERE t1.BACId = t2.Approver_2 AND t2.BidRefNo = " + Session["BuyerBidForBac"] + " UNION SELECT t1.BACId, t1.LastName + ', ' + t1.FirstName + ' ' + t1.MiddleName AS Name1, t2.ApprovedDt_3 ApprovedDt, 3 AS ApprovingLimit  FROM tblBidAwardingCommittee t1, tblBacBidItems t2   WHERE t1.BACId = t2.Approver_3 AND t2.BidRefNo = " + Session["BuyerBidForBac"] + " UNION SELECT t1.BACId, t1.LastName + ', ' + t1.FirstName + ' ' + t1.MiddleName AS Name1, t2.ApprovedDt_4 ApprovedDt, 4 AS ApprovingLimit  FROM tblBidAwardingCommittee t1, tblBacBidItems t2   WHERE t1.BACId = t2.Approver_4 AND t2.BidRefNo = " + Session["BuyerBidForBac"] + " UNION SELECT t1.BACId, t1.LastName + ', ' + t1.FirstName + ' ' + t1.MiddleName AS Name1, t2.ApprovedDt_5 ApprovedDt, 5 AS ApprovingLimit  FROM tblBidAwardingCommittee t1, tblBacBidItems t2   WHERE t1.BACId = t2.Approver_5 AND t2.BidRefNo = " + Session["BuyerBidForBac"] + " UNION SELECT t1.BACId, t1.LastName + ', ' + t1.FirstName + ' ' + t1.MiddleName AS Name1, t2.ApprovedDt_6 ApprovedDt, 6 AS ApprovingLimit  FROM tblBidAwardingCommittee t1, tblBacBidItems t2   WHERE t1.BACId = t2.Approver_6 AND t2.BidRefNo = " + Session["BuyerBidForBac"] + " UNION SELECT t1.BACId, t1.LastName + ', ' + t1.FirstName + ' ' + t1.MiddleName AS Name1, t2.ApprovedDt_7 ApprovedDt, 7 AS ApprovingLimit  FROM tblBidAwardingCommittee t1, tblBacBidItems t2   WHERE t1.BACId = t2.Approver_7 AND t2.BidRefNo = " + Session["BuyerBidForBac"] + " UNION SELECT t1.BACId, t1.LastName + ', ' + t1.FirstName + ' ' + t1.MiddleName AS Name1, t2.ApprovedDt_8 ApprovedDt,  8 AS ApprovingLimit  FROM tblBidAwardingCommittee t1, tblBacBidItems t2   WHERE t1.BACId = t2.Approver_8 AND t2.BidRefNo = " + Session["BuyerBidForBac"] + " UNION SELECT t1.BACId, t1.LastName + ', ' + t1.FirstName + ' ' + t1.MiddleName AS Name1, t2.ApprovedDt_9 ApprovedDt,  9 AS ApprovingLimit  FROM tblBidAwardingCommittee t1, tblBacBidItems t2   WHERE t1.BACId = t2.Approver_9 AND t2.BidRefNo = " + Session["BuyerBidForBac"] + " UNION SELECT t1.BACId, t1.LastName + ', ' + t1.FirstName + ' ' + t1.MiddleName AS Name1, t2.ApprovedDt_10 ApprovedDt, 10 AS ApprovingLimit  FROM tblBidAwardingCommittee t1, tblBacBidItems t2 WHERE t1.BACId = t2.Approver_10 AND t2.BidRefNo = " + Session["BuyerBidForBac"] + " UNION SELECT t1.PurchasingID BACId, t1.LastName + ', ' + t1.FirstName + ' ' + t1.MiddleName AS Name1, t2.ApprovedDt_0 ApprovedDt, 0 AS ApprovingLimit FROM tblPurchasing t1, tblBacBidItems t2 WHERE t1.PurchasingID=t2.Approver_0 AND t2.BidRefNo = " + Session["BuyerBidForBac"] + ") as table_1 order by ApprovingLimit";
        oReader = SqlHelper.ExecuteReader(connstring, CommandType.Text, sCommand);
        if (oReader.HasRows)
        {
            while (oReader.Read())
            {
                if (oReader["ApprovingLimit"].ToString() == "0")
                {
                    oApprovers = oApprovers + "Purchasing: " + oReader["Name1"].ToString() + "<br>";
                }
                else
                {
                    oApprovers = oApprovers + "Approver " + oReader["ApprovingLimit"].ToString() + ": " + oReader["Name1"].ToString() + "<br>";
                }
            }
        }
        oReader.Close();

        string ItemDesc = "";
        sCommand = "SELECT ItemDesc FROM tblBacBidItems WHERE BidRefNo=" + Session["BuyerBidForBac"];
        oReader = SqlHelper.ExecuteReader(connstring, CommandType.Text, sCommand);
        if (oReader.HasRows)
        {
            oReader.Read();
            ItemDesc = oReader["ItemDesc"].ToString();
        } oReader.Close();

        sb.Append("<tr><td><p><strong>BAC Approved to Award</strong> <br> Sent: " + DateTime.Now.ToLongDateString() + "</p> <p>From: " + BuyersName1 + "<br><br> To: " + BuyersName1 + "<br><br> Dear " + BuyersName1 + ", <br><br> Re: Approved to Award – <strong>" + ItemDesc + "</strong><br><br> We are pleased to inform you that your Bid for Award was approved as follows:<br><br> <b>Bid Reference Number:</b> " + Session["BuyerBidForBac"] + "<br> <b>Bid Event Name:</b> " + ItemDesc + "<br> </p> " + sTxt + "  <p>Very truly yours,<br><br><br> <strong>" + oApprovers + "</strong></p><p>&nbsp;</p> Please do not reply to this auto-generated  message.&nbsp;</td></tr>");

        return MailTemplate.IntegrateBodyIntoTemplate(sb.ToString());
    }

    private string CreateNotificationBodyReject()
    {
        StringBuilder sb = new StringBuilder();

        string sCommand;
        string BuyersName1 = "";
        string connstring = HttpContext.Current.Session["ConnectionString"].ToString();
        SqlDataReader oReader;

        sCommand = "SELECT t1.BuyerFirstName + ' ' + t1.BuyerMidName + ' ' + t1.BuyerLastName AS Name1 FROM tblBuyers t1, tblBacBidItems t2 ";
        sCommand = sCommand + "WHERE t1.BuyerID=t2.BuyerId AND t2.BidRefNo=" + Session["BuyerBidForBac"];
        oReader = SqlHelper.ExecuteReader(connstring, CommandType.Text, sCommand);
        if (oReader.HasRows)
        {
            oReader.Read();
            BuyersName1 = oReader["Name1"].ToString();
        } oReader.Close();

        // Approvers
        string oApprovers = "";
        sCommand = "select BACId, NAME1, ApprovingLimit, ApprovedDt from ( SELECT t1.BACId, t1.LastName + ', ' + t1.FirstName + ' ' + t1.MiddleName AS Name1, t2.ApprovedDt_1 ApprovedDt, 1 AS ApprovingLimit  FROM tblBidAwardingCommittee t1, tblBacBidItems t2   WHERE t1.BACId = t2.Approver_1 AND t2.BidRefNo = " + Session["BuyerBidForBac"] + " UNION SELECT t1.BACId, t1.LastName + ', ' + t1.FirstName + ' ' + t1.MiddleName AS Name1, t2.ApprovedDt_2 ApprovedDt, 2 AS ApprovingLimit  FROM tblBidAwardingCommittee t1, tblBacBidItems t2   WHERE t1.BACId = t2.Approver_2 AND t2.BidRefNo = " + Session["BuyerBidForBac"] + " UNION SELECT t1.BACId, t1.LastName + ', ' + t1.FirstName + ' ' + t1.MiddleName AS Name1, t2.ApprovedDt_3 ApprovedDt, 3 AS ApprovingLimit  FROM tblBidAwardingCommittee t1, tblBacBidItems t2   WHERE t1.BACId = t2.Approver_3 AND t2.BidRefNo = " + Session["BuyerBidForBac"] + " UNION SELECT t1.BACId, t1.LastName + ', ' + t1.FirstName + ' ' + t1.MiddleName AS Name1, t2.ApprovedDt_4 ApprovedDt, 4 AS ApprovingLimit  FROM tblBidAwardingCommittee t1, tblBacBidItems t2   WHERE t1.BACId = t2.Approver_4 AND t2.BidRefNo = " + Session["BuyerBidForBac"] + " UNION SELECT t1.BACId, t1.LastName + ', ' + t1.FirstName + ' ' + t1.MiddleName AS Name1, t2.ApprovedDt_5 ApprovedDt, 5 AS ApprovingLimit  FROM tblBidAwardingCommittee t1, tblBacBidItems t2   WHERE t1.BACId = t2.Approver_5 AND t2.BidRefNo = " + Session["BuyerBidForBac"] + " UNION SELECT t1.BACId, t1.LastName + ', ' + t1.FirstName + ' ' + t1.MiddleName AS Name1, t2.ApprovedDt_6 ApprovedDt, 6 AS ApprovingLimit  FROM tblBidAwardingCommittee t1, tblBacBidItems t2   WHERE t1.BACId = t2.Approver_6 AND t2.BidRefNo = " + Session["BuyerBidForBac"] + " UNION SELECT t1.BACId, t1.LastName + ', ' + t1.FirstName + ' ' + t1.MiddleName AS Name1, t2.ApprovedDt_7 ApprovedDt, 7 AS ApprovingLimit  FROM tblBidAwardingCommittee t1, tblBacBidItems t2   WHERE t1.BACId = t2.Approver_7 AND t2.BidRefNo = " + Session["BuyerBidForBac"] + " UNION SELECT t1.BACId, t1.LastName + ', ' + t1.FirstName + ' ' + t1.MiddleName AS Name1, t2.ApprovedDt_8 ApprovedDt,  8 AS ApprovingLimit  FROM tblBidAwardingCommittee t1, tblBacBidItems t2   WHERE t1.BACId = t2.Approver_8 AND t2.BidRefNo = " + Session["BuyerBidForBac"] + " UNION SELECT t1.BACId, t1.LastName + ', ' + t1.FirstName + ' ' + t1.MiddleName AS Name1, t2.ApprovedDt_9 ApprovedDt,  9 AS ApprovingLimit  FROM tblBidAwardingCommittee t1, tblBacBidItems t2   WHERE t1.BACId = t2.Approver_9 AND t2.BidRefNo = " + Session["BuyerBidForBac"] + " UNION SELECT t1.BACId, t1.LastName + ', ' + t1.FirstName + ' ' + t1.MiddleName AS Name1, t2.ApprovedDt_10 ApprovedDt, 10 AS ApprovingLimit  FROM tblBidAwardingCommittee t1, tblBacBidItems t2 WHERE t1.BACId = t2.Approver_10 AND t2.BidRefNo = " + Session["BuyerBidForBac"] + " UNION SELECT t1.PurchasingID BACId, t1.LastName + ', ' + t1.FirstName + ' ' + t1.MiddleName AS Name1, t2.ApprovedDt_0 ApprovedDt, 0 AS ApprovingLimit FROM tblPurchasing t1, tblBacBidItems t2 WHERE t1.PurchasingID=t2.Approver_0 AND t2.BidRefNo = " + Session["BuyerBidForBac"] + ") as table_1 order by ApprovingLimit";
        oReader = SqlHelper.ExecuteReader(connstring, CommandType.Text, sCommand);
        if (oReader.HasRows)
        {
            while (oReader.Read())
            {
                if (oReader["ApprovingLimit"].ToString() == "0")
                {
                    oApprovers = oApprovers + "Purchasing: " + oReader["Name1"].ToString() + "<br>";
                }
                else
                {
                    oApprovers = oApprovers + "Approver " + oReader["ApprovingLimit"].ToString() + ": " + oReader["Name1"].ToString() + "<br>";
                }
            }
        }
        oReader.Close();

        string ItemDesc = "";
        sCommand = "SELECT ItemDesc FROM tblBacBidItems WHERE BidRefNo=" + Session["BuyerBidForBac"];
        oReader = SqlHelper.ExecuteReader(connstring, CommandType.Text, sCommand);
        if (oReader.HasRows)
        {
            oReader.Read();
            ItemDesc = oReader["ItemDesc"].ToString();
        } oReader.Close();

        sb.Append("<tr><td><p><strong>BAC Rejected</strong> <br> Sent: " + DateTime.Now.ToLongDateString() + "</p> <p>From: Reviewers<br><br>  Dear " + BuyersName1 + ", <br><br> Re: Notice of Rejected BAC – " + ItemDesc + "<br><br> This is to inform all Bid Award reviewers and approvers that the following Bid was rejected:<br><br> <b>Bid Reference Number:</b> " + Session["BuyerBidForBac"] + "<br> <b>Bid Event Name:</b> " + ItemDesc + "<br><br> Very truly yours,<br><br><br> <strong>" + oApprovers + "</strong></p><p>&nbsp;</p>  Please do not reply to this auto-generated  message.&nbsp;</td></tr>");

        return MailTemplate.IntegrateBodyIntoTemplate(sb.ToString());
    }

    private string CreateNotificationBodyClarify()
    {
        StringBuilder sb = new StringBuilder();

        string sCommand;
        string WhoClarifyName1 = "";
        string ClarifyToName1 = "";
        string connstring = HttpContext.Current.Session["ConnectionString"].ToString();
        SqlDataReader oReader;

        sCommand = "SELECT t1.BuyerFirstName + ' ' + t1.BuyerMidName + ' ' + t1.BuyerLastName AS Name1 FROM tblBuyers t1, tblBacBidItems t2 ";
        sCommand = sCommand + "WHERE t1.BuyerID=t2.BuyerId AND t2.BidRefNo=" + Session["BuyerBidForBac"];
        oReader = SqlHelper.ExecuteReader(connstring, CommandType.Text, sCommand);
        if (oReader.HasRows)
        {
            oReader.Read();
            WhoClarifyName1 = oReader["Name1"].ToString();
        } oReader.Close();

        sCommand = "SELECT t1.PurchasingID, t2.LastName + ', ' + t2.FirstName + ' ' + t2.MiddleName AS Name1, CONVERT(VARCHAR(17), t3.ApprovedDt_0, 113) ApprovedDt ";
        sCommand = sCommand + "FROM tblSupervisor t1, tblPurchasing t2, tblBacBidItems t3 WHERE ";
        sCommand = sCommand + "t1.PurchasingID=t2.PurchasingID and t1.BuyerId = t3.buyerId AND t2.PurchasingID=t3.Approver_0 and t3.BidRefNo=" + Session["BuyerBidForBac"];
        oReader = SqlHelper.ExecuteReader(connstring, CommandType.Text, sCommand);
        if (oReader.HasRows)
        {
            oReader.Read();
            ClarifyToName1 = oReader["Name1"].ToString();
        } oReader.Close();

        string ItemDesc = "";
        sCommand = "SELECT ItemDesc FROM tblBacBidItems WHERE BidRefNo=" + Session["BuyerBidForBac"];
        oReader = SqlHelper.ExecuteReader(connstring, CommandType.Text, sCommand);
        if (oReader.HasRows)
        {
            oReader.Read();
            ItemDesc = oReader["ItemDesc"].ToString();
        } oReader.Close();

        //sb.Append("<tr><td align='right'><h5>" + DateTime.Now.ToLongDateString() + "</h5></td></tr>");
        sb.Append("<tr><td><p><strong>BAC for Clarification</strong> <br> Sent: " + DateTime.Now.ToLongDateString() + "</p> <p>From: " + WhoClarifyName1 + "<br><br> To: " + ClarifyToName1 + "<br><br> Attention:<br> Re: Request for Clarification: " + ItemDesc + "<br><br> Dear " + ClarifyToName1 + ", <br><br> There is a request for clarification on Bid for Award as follows:<br><br> <b>Bid Reference Number:</b> " + Session["BuyerBidForBac"] + "<br> <b>Bid Event Name:</b> " + ItemDesc + "<br><br></p>  <p>Very truly yours,<br><br><br> <strong>e-Sourcing Procurement</strong></p><p>&nbsp;</p> <p><strong>Instructions:</strong></p> <ol> <li>Go to <a href='https://e-sourcing.Trans-Asia.com.ph/'>https://e-sourcing.Trans-Asia.com.ph</a></li> <li>Enter your Username and Password then  click Login</li> <li>Click Bid Award for clarification</li> <li>Click Bid Events Name</li> <li>See Remarks / Comments box for items to clarify</li> <li>Click Clarify & provide response to Clarification</li> </ol> Please do not reply to this auto-generated  message.&nbsp;</td></tr>");

        return MailTemplate.IntegrateBodyIntoTemplate(sb.ToString());
    }
}