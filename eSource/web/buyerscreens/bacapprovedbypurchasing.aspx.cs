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
using EBid.ConnectionString;
using System.IO;
using System.IO.Compression;
using System.Xml;
using System.Text;
using CalendarControl;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using EBid.ConnectionString;

public partial class web_bac_bacapprovedbypurchasing : System.Web.UI.Page
{

    public float iTotal;
    int iApproverCount = 0;
    int iVendor;
    int iVendorCnt = 0, iAccreditation = 0, iTechComp = 0, iCommComp = 0, iContComp = 0, iPRating = 0;
    int iC0 = 0, iC1 = 0, iC2 = 0, iC3 = 0, iC4 = 0, iC5 = 0, iC6 = 0, iC7 = 0, iC8 = 0, iC9 = 0;
    int iQty = 0, iUnitMeasure = 0, iUnitCost = 0, iTotal1 = 0, iCurrency = 0, iRanking = 0, iAwarded = 0;

    bool IsNumber(string text)
    {
        Regex regex = new Regex(@"^[-+]?[0-9]*\.?[0-9]+$");
        return regex.IsMatch(text);
    }

    public static void CompressFile(FileInfo fi)
    {
        // Get the stream of the source file.
        using (FileStream inFile = fi.OpenRead())
        {
            // Prevent compressing hidden and 
            // already compressed files.
            if ((File.GetAttributes(fi.FullName) & FileAttributes.Hidden) != FileAttributes.Hidden & fi.Extension != ".gz")
            {
                // Create the compressed file.
                using (FileStream outFile = File.Create(fi.FullName + ".gz"))
                {
                    using (GZipStream Compress = new GZipStream(outFile, CompressionMode.Compress))
                    {
                        // Copy the source file into the compression stream.
                        inFile.CopyTo(Compress);
                    }
                }
            }
        }
        fi = null;
    }

    public static void Decompress(FileInfo fi)
    {
        // Get the stream of the source file.
        using (FileStream inFile = fi.OpenRead())
        {
            // Get original file extension, for example
            // "doc" from report.doc.gz.
            string curFile = fi.FullName;
            string origName = curFile.Remove(curFile.Length - fi.Extension.Length);

            //Create the decompressed file.
            using (FileStream outFile = File.Create(origName))
            {
                using (GZipStream Decompress = new GZipStream(inFile, CompressionMode.Decompress))
                {
                    // Copy the decompression stream into the output file.
                    Decompress.CopyTo(outFile);
                }
            }
        }
        fi = null;
    }

    protected void doBuyerBidForBac()
    {
        string sCommand;
        int i;

        // main
        sCommand = "SELECT * FROM tblBACBidItems WHERE BacRefNo=" + Session["BuyerBidForBac"] + " AND Status=1";
        string connstring = HttpContext.Current.Session["ConnectionString"].ToString();
        SqlDataReader oReader = SqlHelper.ExecuteReader(connstring, CommandType.Text, sCommand);
        //if (oReader.HasRows)
        //{
        //    oReader.Read();
        //    string sBudgeted = oReader["Budgeted"].ToString();
        //    switch (sBudgeted)
        //    {
        //        case "0": UnBudgeted.Checked = true; break;
        //        case "1": Budgeted.Checked = true; break;
        //    }
        //    string sCompanyID = oReader["CompanyID"].ToString();
        //    switch (sCompanyID)
        //    {
        //        case "0": CompanyIdGT.Checked = true; break;
        //        case "1": CompanyIdIC.Checked = true; break;
        //        case "2": CompanyIdGXI.Checked = true; break;
        //        case "3": CompanyIdEGG.Checked = true; break;
        //    }
        //}
        //oReader.Close();

        // sourcing strategy
        i = 0;
        sCommand = "SELECT * FROM tblBACSourcingStrategy WHERE BidRefNo=" + Session["BuyerBidForBac"].ToString() + " AND UserId=" + Session["BuyerBuyerId"].ToString();
        oReader = SqlHelper.ExecuteReader(connstring, CommandType.Text, sCommand);
        if (oReader.HasRows)
        {
            while (oReader.Read())
            {
                i++;
                ClientScript.RegisterStartupScript(this.GetType(), "ss_" + i, "<script language='Javascript'>AddTRCheckBoxFromDB('#mySourcingStrategy', 'ss', '" + oReader["SourcingStrategy"].ToString() + "');</script>");
            }
        }
        oReader.Close();

        // type of purchase
        i = 0;
        sCommand = "SELECT * FROM tblBACTypeOfPurchase WHERE BidRefNo=" + Session["BuyerBidForBac"].ToString() + " AND UserId=" + Session["BuyerBuyerId"].ToString();
        oReader = SqlHelper.ExecuteReader(connstring, CommandType.Text, sCommand);
        if (oReader.HasRows)
        {
            while (oReader.Read())
            {
                i++;
                ClientScript.RegisterStartupScript(this.GetType(), "top_" + i, "<script language='Javascript'>AddTRCheckBoxFromDB('#myTypeOfPurchase', 'top', '" + oReader["TypeOfPurchase"].ToString() + "');</script>");
            }
        }
        oReader.Close();

        // supply position
        i = 0;
        sCommand = "SELECT * FROM tblBACSupplyPosition WHERE BidRefNo=" + Session["BuyerBidForBac"].ToString() + " AND UserId=" + Session["BuyerBuyerId"].ToString();
        oReader = SqlHelper.ExecuteReader(connstring, CommandType.Text, sCommand);
        if (oReader.HasRows)
        {
            while (oReader.Read())
            {
                i++;
                ClientScript.RegisterStartupScript(this.GetType(), "sp_" + i, "<script language='Javascript'>AddTRCheckBoxFromDB('#mySupplyPosition', 'sp', '" + oReader["SupplyPosition"].ToString() + "');</script>");
            }
        }
        oReader.Close();

        // basis for awarding
        i = 0;
        sCommand = "SELECT * FROM tblBACBasisForAwarding WHERE BidRefNo=" + Session["BuyerBidForBac"].ToString() + " AND UserId=" + Session["BuyerBuyerId"].ToString();
        oReader = SqlHelper.ExecuteReader(connstring, CommandType.Text, sCommand);
        if (oReader.HasRows)
        {
            while (oReader.Read())
            {
                i++;
                ClientScript.RegisterStartupScript(this.GetType(), "bfa_" + i, "<script language='Javascript'>AddTRCheckBoxFromDB('#myBasisForAwarding', 'bfa', '" + oReader["BasisForAwarding"].ToString() + "');</script>");
            }
        }
        oReader.Close();

        // comments
        sCommand = "SELECT * FROM tblBACComments WHERE BidRefNo=" + Session["BuyerBidForBac"].ToString() + " AND UserId=" + Session["BuyerBuyerId"].ToString();
        oReader = SqlHelper.ExecuteReader(connstring, CommandType.Text, sCommand);
        if (oReader.HasRows)
        {
            oReader.Read();
            //string tempReRequest.Form("remarks_comments")
            if (Request.Form["remarks_comments"] != null)
            {
                //remarks_comments.InnerText = oReader["Comment"].ToString();
            }
        }
        oReader.Close();
        
    }

    protected void ShowAttachments()
    {

    }

    protected void Page_LoadComplete(object sender, EventArgs e)
    {
        if (IsPostBack)
        {
            string control1 = Request.Form["__EVENTTARGET"];
            //Response.Write(control1);
            if (control1 == "BuyerBidForBac")
            {
                //ShowBidItemTenders();
            }
            else if (control1 == "Clarify")
            {
                doClarify(Request.Form["__EVENTARGUMENT"]);
            }
            else if (control1 == "Approve")
            {
                doApprove();
            }else if (control1 == "Reject")
            {
                doReject();
            }
        }
        ShowApprover();
    }

    protected void doClarify(string ToUserID)
    {
        //Response.Write(ToUserID);
        string connstring = HttpContext.Current.Session["ConnectionString"].ToString();
        string sCommand;
        //SqlDataReader oReader;


        // add row to tblBACComments table
        sCommand = "INSERT INTO tblBACClarifications (BidRefNo, FrUserId, Comment, ToUserId, DatePosted) VALUES (";
        sCommand = sCommand + Session["BuyerBidForBac"].ToString() + ", " + Session["UserId"] + ", '" + Request.Form["txtClarify"] + "', " + ToUserID + ", GETDATE())";
        SqlHelper.ExecuteNonQuery(connstring, CommandType.Text, sCommand);

        // update tblBacBidItems table
        int i = Convert.ToInt32(Session["approverPosition"].ToString().Trim()) + 1;
        sCommand = "UPDATE tblBacBidItems SET Status=2, ClarifyDt_" + Session["approverPosition"].ToString() + "=GETDATE() WHERE BacRefNo=" + Session["BuyerBacRefNo"];
        SqlHelper.ExecuteNonQuery(connstring, CommandType.Text, sCommand);

        if (SendEmailNotificationClarify())
        {
            Session["Message"] = "Notification sent successfully.";
        }
        else
        {
            // failed
            Session["Message"] = "Failed to send notification. Please try again or contact adminitrator for assistance.";
        }
        Session["Message"] = "";
        Response.Redirect("bacapprovedpurchasing.aspx");
    }

    protected void doApprove()
    {
        string connstring = HttpContext.Current.Session["ConnectionString"].ToString();
        string sCommand;

        // add row to tblBACComments table
        sCommand = "INSERT INTO tblBACComments (BidRefNo, UserId, Comment, DatePosted) VALUES (";
        sCommand = sCommand + Session["BuyerBidForBac"].ToString() + ", " + Session["UserId"] + ", '" + Request.Form["txtClarify"].Replace("'", "''") + "', GETDATE())";
        SqlHelper.ExecuteNonQuery(connstring, CommandType.Text, sCommand);

        int myStat = 4;
        bool bUnchecked = false;
        foreach (RepeaterItem oItem in RepeaterApprover.Items)
        {
            if (((CheckBox)oItem.FindControl("approved1")).Checked == false)
            {
                bUnchecked = true;
                break;
            }
        }

        if (bUnchecked == true)
            myStat = 3;


        // update tblBacBidItems table
        if (Session["approverNext"].ToString() == "1")
        {
            sCommand = "UPDATE tblBacBidItems SET Status=1, ApprovedDt_" + Session["approverPosition"] + "=GETDATE() WHERE BacRefNo=" + Session["BuyerBacRefNo"];
        }
        else {
            sCommand = "UPDATE tblBacBidItems SET Status=3, ApprovedDt_" + Session["approverPosition"] + "=GETDATE() WHERE BacRefNo=" + Session["BuyerBacRefNo"];
        }
        SqlHelper.ExecuteNonQuery(connstring, CommandType.Text, sCommand);

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
        Response.Redirect("bacapprovedpurchasing.aspx");
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


        if (SendEmailNotificationReject()){
            Session["Message"] = "Notification sent successfully.";
        }
        else
        {
            // failed
            Session["Message"] = "Failed to send notification. Please try again or contact adminitrator for assistance.";
        }
        Session["Message"] = "";
        //Response.Redirect("bacrejected.aspx");
    }

    protected void ShowApprover()
    {

        // approver 4: purchasing
        
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        string connstring = HttpContext.Current.Session["ConnectionString"].ToString();
        string sCommand;
        SqlDataReader oReader;


        //get position of approving user
        Session["approverPosition"] = "";
        Session["approverNext"] = "";
        sCommand = "SELECT * FROM tblBacBidItems WHERE BacRefNo=" + Session["BuyerBacRefNo"];
        oReader = SqlHelper.ExecuteReader(connstring, CommandType.Text, sCommand);
        if (oReader.HasRows)
        {
            oReader.Read();
            //string i = oReader["ItemDesc"].ToString();
            if (oReader["Approver_0"].ToString() == Session["UserId"].ToString()) Session["approverPosition"] = 0;
            if (oReader["Approver_1"].ToString() == Session["UserId"].ToString()) Session["approverPosition"] = 1;
            if (oReader["Approver_2"].ToString() == Session["UserId"].ToString()) Session["approverPosition"] = 2;
            if (oReader["Approver_3"].ToString() == Session["UserId"].ToString()) Session["approverPosition"] = 3;
            if (oReader["Approver_4"].ToString() == Session["UserId"].ToString()) Session["approverPosition"] = 4;
            if (oReader["Approver_5"].ToString() == Session["UserId"].ToString()) Session["approverPosition"] = 5;
            if (oReader["Approver_6"].ToString() == Session["UserId"].ToString()) Session["approverPosition"] = 6;
            if (oReader["Approver_7"].ToString() == Session["UserId"].ToString()) Session["approverPosition"] = 7;
            if (oReader["Approver_8"].ToString() == Session["UserId"].ToString()) Session["approverPosition"] = 8;
            if (oReader["Approver_9"].ToString() == Session["UserId"].ToString()) Session["approverPosition"] = 9;
            if (oReader["Approver_10"].ToString() == Session["UserId"].ToString()) Session["approverPosition"] = 10;
            int approverNext = Convert.ToInt32(Session["approverPosition"].ToString().Trim()) + 1;
            Session["approverNext"] = (oReader["Approver_" + approverNext].ToString() != "") ? 1 : 0;
        }
        oReader.Close();
        
        //Clarify Dropdownlist, insert buyer
        sCommand = "SELECT t1.BuyerId,  'Buyer: '+ t2.BuyerFirstName + ' ' + t2.BuyerLastName  AS Name1 FROM tblBacBidItems t1, tblBuyers t2 WHERE t1.BacRefNo=" + Session["BuyerBacRefNo"] + "AND t2.BuyerId = t1.BuyerId";
        oReader = SqlHelper.ExecuteReader(connstring, CommandType.Text, sCommand);
        if (oReader.HasRows)
        {
            oReader.Read();
            cbApproverList.Items.Insert(0, new ListItem(oReader["Name1"].ToString(), oReader["BuyerId"].ToString()));
        }
        //Clarify Dropdownlist, insert purchasing
        sCommand = "SELECT t1.PurchasingID, 'Purchasing: '+t2.FirstName+ ' ' +t2.LastName AS Name1 FROM tblSupervisor t1 INNER JOIN tblPurchasing t2 ON t1.PurchasingID=t2.PurchasingID WHERE t1.BuyerID=" + Session["BuyerBuyerId"];
        oReader = SqlHelper.ExecuteReader(connstring, CommandType.Text, sCommand);
        if (oReader.HasRows)
        {
            oReader.Read();
            cbApproverList.Items.Insert(1, new ListItem(oReader["Name1"].ToString(), oReader["PurchasingID"].ToString()));
        }
        oReader.Close();
        //Clarify Dropdownlist, insert bac
        for(int i=1; i < Convert.ToInt32(Session["approverPosition"].ToString().Trim()); i++){
            sCommand = "SELECT t1.Approver_"+i+", 'BAC: '+t2.FirstName+' '+t2.LastName AS Name1 FROM tblBacBidItems t1, tblBidAwardingCommittee t2 WHERE t1.BacRefNo=" + Session["BuyerBacRefNo"] + " AND t2.BACId = t1.Approver_"+i;
            oReader = SqlHelper.ExecuteReader(connstring, CommandType.Text, sCommand);
            if (oReader.HasRows)
            {
                oReader.Read();
                cbApproverList.Items.Insert(i + 1, new ListItem(oReader["Name1"].ToString(), oReader["Approver_"+i].ToString()));
            }
            oReader.Close();
        }


        // Reject
        sCommand = "SELECT Approver FROM tblBidAwardingCommittee WHERE BACId=" + Session["UserId"].ToString();
        oReader = SqlHelper.ExecuteReader(connstring, CommandType.Text, sCommand);
        if (oReader.HasRows)
        {
            oReader.Read();
            if (oReader["Approver"].ToString() == "0")
            {
                btnReject.Visible = true;
            }
            else { btnReject.Visible = false; }
        } 
        oReader.Close();

        //SELECT t2.BACId, t2.LastName + ', ' + t2.FirstName + ' ' + t2.MiddleName AS Name1, t2.ApprovingLimitOnNonLowestPrice AS ApprovingLimit FROM tblBacBidItems t1, tblBidAwardingCommittee t2 WHERE (t2.BACId=t1.Approver_1 OR t2.BACId=t1.Approver_2 OR t2.BACId=t1.Approver_3 OR t2.BACId=t1.Approver_4 OR t2.BACId=t1.Approver_5 OR t2.BACId=t1.Approver_6 OR t2.BACId=t1.Approver_7 OR t2.BACId=t1.Approver_8 OR t2.BACId=t1.Approver_9 OR t2.BACId=t1.Approver_10) AND t1.BidRefNo=@BidRefNo
    }

    protected void BuyerBidForBac_DataBound(object sender, EventArgs e)
    {
        //BuyerBidForBac.Items.Insert(0, new ListItem("---- SELECT BID ----", "-1"));
        //BuyerBidForBac.Items.FindByValue(Session["BuyerBacRefNo"].ToString()).Selected = true;
        //BuyerBidForBac.Enabled = false;
    }
    protected void RepeaterApprover_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        iApproverCount++;
        Session["ApproverNumber"] = 1;
        string sID;
        ((Literal)e.Item.FindControl("lblApprover")).Text = "Approver " + iApproverCount.ToString();
        if (((DataRowView)e.Item.DataItem)["BACId"].ToString() == Session["UserId"].ToString())
        {
            Session["ApproverNumber"] = iApproverCount + 1;
            Session["ApproverCount"] = iApproverCount;
            ((CheckBox)e.Item.FindControl("clarify1")).Enabled = true;
            ((CheckBox)e.Item.FindControl("clarify1")).Attributes.Add("onClick", "ShowClarify('" + ((CheckBox)e.Item.FindControl("clarify1")).ClientID.ToString() + "');");
            ((CheckBox)e.Item.FindControl("approved1")).Enabled = true;
            ((CheckBox)e.Item.FindControl("approved1")).Attributes.Add("onClick", "ShowApprove('" + ((CheckBox)e.Item.FindControl("approved1")).ClientID.ToString() + "');");
        }

         //CheckBox checkBox = (CheckBox)e.Item.FindControl("approved1");
         //checkBox.Checked = true;
         int i = Convert.ToInt32(Session["approverPosition"].ToString().Trim())-1;
         if (i != 0)
         {
             foreach (RepeaterItem item in RepeaterApprover.Items)
             {
                 i--;
                 if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
                 {
                     CheckBox checkBox = (CheckBox)item.FindControl("approved1");
                     checkBox.Checked = true;
                 }
                 if (i == 0)
                     break;
             }
         }

    }


    private string connstring = Client.ConnectionString(HttpContext.Current.Session["clientid"].ToString());

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
        string subject = "Trans-Asia  : Notification";

        //GET STATUS IF FOR APPROVAL OR ALREADY AWARDED
        string Status = "";
        sCommand = "SELECT Status FROM tblBacBidItems WHERE BidRefNo=" + Session["BuyerBidForBac"];
        oReader = SqlHelper.ExecuteReader(connstring, CommandType.Text, sCommand);
        if (oReader.HasRows)
        {
            oReader.Read();
            Status = oReader["Status"].ToString();
        } oReader.Close();


        if (Status == "1")
        {
            //GET BUYER EMAIL AS THE SENDER
            sCommand = "SELECT t1.BuyerFirstName + ' ' + t1.BuyerMidName + ' ' + t1.BuyerLastName AS Name1, t1.EmailAdd  FROM tblBuyers t1, tblBacBidItems t2 WHERE t1.BuyerID=t2.BuyerId AND t2.BidRefNo=" + Session["BuyerBidForBac"];
            oReader = SqlHelper.ExecuteReader(connstring, CommandType.Text, sCommand);
            if (oReader.HasRows)
            {
                oReader.Read();
                fromName = oReader["Name1"].ToString();
                fromEmail = oReader["EmailAdd"].ToString();
                from = '"' + oReader["Name1"].ToString() + '"' + " <" + oReader["EmailAdd"].ToString() + ">";
            } oReader.Close();


            //GET APPROVER EMAIL AS THE RECEPIENT OR IF NULL, NOTIFY BUYER AS AWARDED BAC //Session["approverNext"]
            sCommand = "SELECT t1.BACId, t1.LastName + ', ' + t1.FirstName + ' ' + t1.MiddleName AS Name1, t1.EmailAdd  FROM tblBidAwardingCommittee t1, tblBacBidItems t2   WHERE t1.BACId = t2.Approver_" + (Convert.ToInt32(Session["approverNext"].ToString().Trim()) + 1).ToString() + " AND t2.BidRefNo = " + Session["BuyerBidForBac"];

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
        else
        {
            //GET BAC EMAIL AS THE SENDER
            sCommand = "SELECT t1.BACId, t1.LastName + ', ' + t1.FirstName + ' ' + t1.MiddleName AS Name1, t1.EmailAdd  FROM tblBidAwardingCommittee t1, tblBacBidItems t2   WHERE t1.BACId = t2.Approver_" + Session["approverNext"].ToString() + " AND t2.BidRefNo = " + Session["BuyerBidForBac"];
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
            //Response.Write(CreateNotificationBodyAwarded() + "<br>");
            //Response.Write(MailTemplate.GetTemplateLinkedResources(this) + "<br>");
            try
            {
                if (!MailHelper.SendEmail(MailTemplate.GetDefaultSMTPServer(),
                        from,
                        to,
                        subject,
                        CreateNotificationBodyAwarded(),
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
        string subject = "Trans-Asia  : Notification";


        //GET BAC EMAIL AS THE SENDER
        sCommand = "SELECT t1.BACId, t1.LastName + ', ' + t1.FirstName + ' ' + t1.MiddleName AS Name1, t1.EmailAdd  FROM tblBidAwardingCommittee t1, tblBacBidItems t2   WHERE t1.BACId = t2.Approver_" + Session["approverNext"] + " AND t2.BidRefNo = " + Session["BuyerBidForBac"];
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
                    CreateNotificationBodyReject(),
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
        string subject = "Trans-Asia  : Notification";


        //GET BAC EMAIL AS THE SENDER
        sCommand = "SELECT t1.BACId, t1.FirstName + ' ' + t1.MiddleName + ' ' + t1.LastName AS Name1, t1.EmailAdd  FROM tblBidAwardingCommittee t1 WHERE t1.BACId=" + Session["UserId"];
        oReader = SqlHelper.ExecuteReader(connstring, CommandType.Text, sCommand);
        if (oReader.HasRows)
        {
            oReader.Read();
            fromName = oReader["Name1"].ToString();
            fromEmail = oReader["EmailAdd"].ToString();
            from = '"' + oReader["Name1"].ToString() + '"' + " <" + oReader["EmailAdd"].ToString() + ">";
        } oReader.Close();


        //GET EMAIL FOR THE RECEPIENT
        sCommand = "SELECT PurchasingID, Name1, EmailAdd FROM (SELECT t2.PurchasingID, t2.FirstName + ' ' + t2.MiddleName + ' ' + t2.LastName AS Name1, t2.EmailAdd  FROM tblPurchasing t2, tblBacBidItems t3 WHERE t2.PurchasingID=t3.Approver_0 and t3.BidRefNo= " + Session["BuyerBidForBac"] + " UNION SELECT t1.BuyerId PurchasingID, t1.BuyerLastName + ', ' + t1.BuyerFirstName + ' ' + t1.BuyerMidName AS Name1, t1.EmailAdd FROM tblBuyers t1, tblBacBidItems t2 WHERE t1.BuyerId = t2.BuyerId AND t2.BidRefNo = " + Session["BuyerBidForBac"] + " UNION SELECT t1.BACId PurchasingID, t1.LastName + ', ' + t1.FirstName + ' ' + t1.MiddleName AS Name1, t1.EmailAdd FROM tblBidAwardingCommittee t1 ) as a1 INNER JOIN (SELECT TOP 1 * FROM tblBACClarifications d  WHERE  d.BidRefNo = " + Session["BuyerBidForBac"] + " ORDER BY d.DatePosted desc) t2 ON t2.ToUserId = a1.PurchasingID";
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
        //Response.Write(CreateNotificationBodyClarify() + "<br>");
        //Response.Write(MailTemplate.GetTemplateLinkedResources(this) + "<br>");
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

        sCommand = "SELECT t1.BuyerFirstName + ' ' + t1.BuyerMidName + ' ' + t1.BuyerLastName AS Name1 FROM tblBuyers t1, tblBacBidItems t2 WHERE t1.BuyerID=t2.BuyerId AND t2.BidRefNo=" + Session["BuyerBidForBac"];
        oReader = SqlHelper.ExecuteReader(connstring, CommandType.Text, sCommand);
        if (oReader.HasRows)
        {
            oReader.Read();
            BuyersName1 = oReader["Name1"].ToString();
        } oReader.Close();

        sCommand = "SELECT t1.BACId, t1.LastName + ', ' + t1.FirstName + ' ' + t1.MiddleName AS Name1, t1.EmailAdd  FROM tblBidAwardingCommittee t1, tblBacBidItems t2   WHERE t1.BACId = t2.Approver_" + (Convert.ToInt32(Session["approverNext"].ToString().Trim()) + 1).ToString() + " AND t2.BidRefNo = " + Session["BuyerBidForBac"];
        oReader = SqlHelper.ExecuteReader(connstring, CommandType.Text, sCommand);
        if (oReader.HasRows)
        {
            oReader.Read();
            ApproverName1 = oReader["Name1"].ToString();
        } oReader.Close();

        // Awarded To
        string sTxt = "<table border='1'><tr>";
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
        sb.Append("<tr><td><p><strong>BAC for Approval</strong> <br> Sent: " + DateTime.Now.ToLongDateString() + "</p> <p>From: " + BuyersName1 + "<br><br> To: " + ApproverName1 + "<br><br> Subject: " + ItemDesc + "<br><br> Dear Bid Award Approvers, <br><br> Re: Request for Bid Award Approval – <strong>" + ItemDesc + "</strong><br><br> This is to request for your Bid Award Approval of the ff:<br><br> <b>Bid Reference Number:</b> " + Session["BuyerBidForBac"] + "<br> <b>Bid Event Name:</b> " + ItemDesc + "<br><br> <b>Awarded to:</b></p> " + sTxt + "  <p>Very truly yours,<br><br><br> <strong>e-Sourcing Procurement</strong></p><p>&nbsp;</p> <p><strong>Instructions:</strong></p> <ol> <li>Go to <a href='https://e-sourcing.Trans-Asia.com.ph/'>https://e-sourcing.Trans-Asia.com.ph</a></li> <li>Enter your Username and Password then  click Login</li> <li>Click Received Bid Events for Awarding</li> <li>Click Bid Events Name</li> <li>Review / Endorse / Approve Bid event  for Awarding</li> <li>Click Clarify if you have clarification  or click Approved to award Bid Events</li> </ol> Please do not reply to this auto-generated  message.&nbsp;</td></tr>");

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

        sCommand = "SELECT t1.BACId, t1.LastName + ', ' + t1.FirstName + ' ' + t1.MiddleName AS Name1, t1.EmailAdd  FROM tblBidAwardingCommittee t1, tblBacBidItems t2   WHERE t1.BACId = t2.Approver_" + Session["approverNext"] + " AND t2.BidRefNo = " + Session["BuyerBidForBac"];
        oReader = SqlHelper.ExecuteReader(connstring, CommandType.Text, sCommand);
        if (oReader.HasRows)
        {
            oReader.Read();
            ApproverName1 = oReader["Name1"].ToString();
        } oReader.Close();

        // Awarded To
        string sTxt = "<table border='1'><tr>";
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

        sb.Append("<tr><td><p><strong>BAC Approved to Award</strong> <br> Sent: " + DateTime.Now.ToLongDateString() + "</p> <p>From: " + BuyersName1 + "<br><br> To: " + BuyersName1 + "<br><br> Dear " + BuyersName1 + ", <br><br> Re: Approved to Award – <strong>" + ItemDesc + "</strong><br><br> We are pleased to inform you that your Bid for Award was approved as follows:<br><br> <b>Bid Reference Number:</b> " + Session["BuyerBidForBac"] + "<br> <b>Bid Event Name:</b> " + ItemDesc + "<br><br> <b>Awarded to:</b></p> " + sTxt + "  <p>Very truly yours,<br><br><br> <strong>" + oApprovers + "</strong></p><p>&nbsp;</p> Please do not reply to this auto-generated  message.&nbsp;</td></tr>");

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

        sCommand = "SELECT t1.FirstName + ' ' + t1.MiddleName + ' ' + t1.LastName AS Name1 FROM tblBidAwardingCommittee t1 ";
        sCommand = sCommand + "WHERE t1.BACId=" + Session["UserId"];
        oReader = SqlHelper.ExecuteReader(connstring, CommandType.Text, sCommand);
        if (oReader.HasRows)
        {
            oReader.Read();
            WhoClarifyName1 = oReader["Name1"].ToString();
        } oReader.Close();

        sCommand = "SELECT PurchasingID, Name1, EmailAdd FROM (SELECT t2.PurchasingID, t2.FirstName + ' ' + t2.MiddleName + ' ' + t2.LastName AS Name1, t2.EmailAdd  FROM tblPurchasing t2, tblBacBidItems t3 WHERE t2.PurchasingID=t3.Approver_0 and t3.BidRefNo= " + Session["BuyerBidForBac"] + " UNION SELECT t1.BuyerId PurchasingID, t1.BuyerLastName + ', ' + t1.BuyerFirstName + ' ' + t1.BuyerMidName AS Name1, t1.EmailAdd FROM tblBuyers t1, tblBacBidItems t2 WHERE t1.BuyerId = t2.BuyerId AND t2.BidRefNo = " + Session["BuyerBidForBac"] + " UNION SELECT t1.BACId PurchasingID, t1.LastName + ', ' + t1.FirstName + ' ' + t1.MiddleName AS Name1, t1.EmailAdd FROM tblBidAwardingCommittee t1 ) as a1 INNER JOIN (SELECT TOP 1 * FROM tblBACClarifications d  WHERE  d.BidRefNo = " + Session["BuyerBidForBac"] + " ORDER BY d.DatePosted desc) t2 ON t2.ToUserId = a1.PurchasingID";
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