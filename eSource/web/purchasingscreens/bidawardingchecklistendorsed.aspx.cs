using System;
using System.Configuration;
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

public partial class web_buyerscreens_bidawardingchecklistendorsed : System.Web.UI.Page
{

    private string connstring = "";

    protected void Page_LoadComplete(object sender, EventArgs e)
    {
        if (IsPostBack)
        {
            FormsAuthenticationHelper.AuthenticateUserWithReturnUrl();
            connstring = HttpContext.Current.Session["ConnectionString"].ToString();
            string sCommand;
            sCommand = "UPDATE tblBACExecutiveSummary SET Comment='" + (Request.Form["executive_summary"].Replace("\n", "<br />")).Replace("'", "''") + "' WHERE BidRefNo= " + Session["BuyerBidForBac"] + " ";
            SqlHelper.ExecuteNonQuery(connstring, CommandType.Text, sCommand);

            string control1 = Request.Form["__EVENTTARGET"];
            if (control1 == "Clarify")
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
        else
        {
            //bac_bidEvaluationDetails1.Page.LoadComplete += new EventHandler(Page_LoadCompleteX);

            string sCommand;
            string connstring = HttpContext.Current.Session["ConnectionString"].ToString();

            // prepared date
            sCommand = "SELECT PreparedDt FROM tblBACBidItems WHERE BacRefNo=" + Session["BuyerBacRefNo"] + " AND Status=1";
            SqlDataReader oReader = SqlHelper.ExecuteReader(connstring, CommandType.Text, sCommand);
            if (oReader.HasRows)
            {
                oReader.Read();
                DateTime date1 = DateTime.Parse(oReader["PreparedDt"].ToString());
                lblPreparedDate.Text = String.Format("{0:MMMM dd, yyyy}", date1);
            }
            oReader.Close();

            HtmlInputHidden txtNewVendor = (HtmlInputHidden)bac_bidEvaluationDetails1.FindControl("txtNewVendor");
            txtNewVendor.Value = "0";
            HtmlInputHidden txtCDVendor = (HtmlInputHidden)bac_bidEvaluationDetails1.FindControl("txtCDVendor");
            txtCDVendor.Value = "0";
            HtmlInputHidden NewRow = ((System.Web.UI.HtmlControls.HtmlInputHidden)bac_bidEvaluationDetails1.FindControl("txtNewRow"));
            NewRow.Value = "-1";

            // criteria
            sCommand = "SELECT COUNT(DISTINCT RowNum) count1 FROM tblBACCriteria WHERE BidRefNo=" + Session["BuyerBidForBac"];
            oReader = SqlHelper.ExecuteReader(connstring, CommandType.Text, sCommand);
            if (oReader.HasRows)
            {
                oReader.Read();
                int iRow = int.Parse(oReader["count1"].ToString());
                iRow--;
                NewRow.Value = iRow.ToString();
            }
            oReader.Close();

            string sCookieName1;
            sCommand = "SELECT * FROM tblBACCriteria WHERE BidRefNo=" + Session["BuyerBidForBac"] + " ORDER BY RowNum";
            oReader = SqlHelper.ExecuteReader(connstring, CommandType.Text, sCommand);
            while (oReader.Read())
            {
                if (oReader["VendorID"].ToString() == "0")
                {
                    sCookieName1 = "Criteria_" + oReader["RowNum"].ToString() + "_";
                }
                else
                {
                    sCookieName1 = "Criteria_" + oReader["RowNum"].ToString() + "_" + oReader["VendorID"].ToString();
                }
                ClientScript.RegisterStartupScript(this.GetType(), sCookieName1, "<script language='Javascript'>createCookie('" + sCookieName1 + "', '" + oReader["CriteriaText"].ToString() + "', 1);</script>");
            }

            CreateCriteriaRow();
            ShowApprover();
            UpdateClarifyTo();

            // rebind commercial details
            //lstCommDetails.DataSourceID = null;
            //lstCommDetails.DataSourceID = "dsItems1";

            sCommand = "SELECT * FROM tblBACExecutiveSummary WHERE BidRefNo=" + Session["BuyerBidForBac"] + " ";
            oReader = SqlHelper.ExecuteReader(connstring, CommandType.Text, sCommand);
            while (oReader.Read())
            {
                executive_summary.Text = oReader["Comment"].ToString().Replace("<br />", "\n");
            }
            oReader.Close();
        }
    }

    protected void CreateCriteriaRow()
    {
        UserControl bac_bidEvaluationDetails = (UserControl)this.FindControl("bac_bidEvaluationDetails1");

        HtmlInputHidden oNewRow = (HtmlInputHidden)bac_bidEvaluationDetails.FindControl("txtNewRow");
        Session["iRowTemplate"] = int.Parse(oNewRow.Value);
        HtmlTableRow tr;
        HtmlTableCell td, td1;
        TextBox oCrit;
        Repeater oRep;
        //Image oImg;

        HtmlTable tbl = (HtmlTable)bac_bidEvaluationDetails.FindControl("tblCriteria");

        //tbl.Rows[tbl.Rows.Count - 1].Visible = false;

        int i;
        for (i = 0; i <= int.Parse(Session["iRowTemplate"].ToString()); i++)
        {
            if (bac_bidEvaluationDetails.FindControl("tr" + i.ToString()) == null)
            {
                if (this.form1.FindControl("txtR" + i.ToString()) == null)
                {
                    if (this.form1.FindControl("RepeaterC" + i.ToString()) == null)
                    {
                        oCrit = new TextBox();
                        oRep = new Repeater();

                        tr = new HtmlTableRow();
                        td = new HtmlTableCell();

                        //oImg = new Image();
                        //oImg.ID = "imgRem" + i.ToString();
                        //oImg.ImageUrl = "~/web/buyerscreens/remove.png";
                        //oImg.Attributes.Add("onclick", "btnDelRow_onclick('#tr" + i.ToString() + "');");
                        oCrit.ID = "txtR" + i.ToString();
                        oCrit.Width = 100;
                        oCrit.ReadOnly = true;
                        //td.Controls.Add(oImg);
                        td.Controls.Add(oCrit);

                        td1 = new HtmlTableCell();
                        LiteralControl oLit = new LiteralControl();
                        oLit.Text = " ";
                        td1.Controls.Add(oLit);

                        oRep.ID = "RepeaterC" + i.ToString();
                        oRep.HeaderTemplate = new MyTemplate1(ListItemType.Header);
                        oRep.ItemTemplate = new MyTemplate1(ListItemType.Item);
                        oRep.AlternatingItemTemplate = new MyTemplate1(ListItemType.AlternatingItem);
                        oRep.FooterTemplate = new MyTemplate1(ListItemType.Footer);
                        oRep.ItemDataBound += new RepeaterItemEventHandler(Criteria_ItemDataBoundX);
                        oRep.DataSourceID = "dsBidItemTendersSummary";

                        tr.Cells.Add(td);
                        tr.Cells.Add(td1);
                        td.Controls.Add(oRep);

                        tr.ID = "tr" + i.ToString();
                        tbl.Rows.Add(tr);
                    }
                }
            }
        }
    }

    protected void Criteria_ItemDataBoundX(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            ((TextBox)e.Item.FindControl("item")).Attributes.Add("VendorID", ((DataRowView)e.Item.DataItem)["VendorID"].ToString());
        }
    }

    protected void Page_LoadCompleteX(object sender, EventArgs e)
    {
        CreateCriteriaRow();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //bac_bidEvaluationDetails1.Page.LoadComplete += new EventHandler(Page_LoadCompleteX);
    }

    protected void ShowApprover()
    {
        string connstring = HttpContext.Current.Session["ConnectionString"].ToString();
        string sCommand;
        SqlDataReader oReader;

        // buyer
        sCommand = "SELECT BuyerLastName + ', ' + BuyerFirstName + ' ' + BuyerMidName AS Name1 FROM tblBuyers WHERE BuyerID=" + Session["BuyerBuyerId"];
        oReader = SqlHelper.ExecuteReader(connstring, CommandType.Text, sCommand);
        if (oReader.HasRows)
        {
            oReader.Read();
            lblBuyer.Text = oReader["Name1"].ToString();
        }
        oReader.Close();

        // prepared date
        sCommand = "SELECT PreparedDt FROM tblBACBidItems WHERE BacRefNo=" + Session["BuyerBacRefNo"] + " AND Status=1";
        oReader = SqlHelper.ExecuteReader(connstring, CommandType.Text, sCommand);
        if (oReader.HasRows)
        {
            oReader.Read();
            System.Web.UI.WebControls.Literal lblPreparedDate1;

            lblPreparedDate1 = (System.Web.UI.WebControls.Literal)this.form1.FindControl("lblPreparedDate");
            DateTime date1 = DateTime.Parse(oReader["PreparedDt"].ToString());
            lblPreparedDate1.Text = String.Format("{0:MMMM dd, yyyy}", date1);
        }

        // approver 4: purchasing
        sCommand = "SELECT t1.PurchasingID, t2.LastName + ', ' + t2.FirstName + ' ' + t2.MiddleName AS Name1 FROM tblSupervisor t1 ";
        sCommand = sCommand + "INNER JOIN tblPurchasing t2 ON t1.PurchasingID=t2.PurchasingID WHERE BuyerID=" + Session["BuyerBuyerId"];
        oReader = SqlHelper.ExecuteReader(connstring, CommandType.Text, sCommand);
        if (oReader.HasRows)
        {
            oReader.Read();
            lblApprover4.Text = oReader["Name1"].ToString();
            approver4.Value = oReader["PurchasingID"].ToString();
        }
        oReader.Close();

        // awarding committee
        SqlDataSource dsApprover = (SqlDataSource)bac_bidApprovingCommittee1.FindControl("dsApprover");
        // dsApprover.SelectCommand = "IF EXISTS (SELECT 1 FROM tblBACSupportingDocuments WHERE BidRefNo=" + Session["BuyerBidForBac"] + " AND DocuName='bfa_LowestPr') BEGIN SELECT t1.BACId, t1.LastName + ', ' + t1.FirstName + ' ' + t1.MiddleName AS Name1, t1.ApprovingLimitOnLowestPrice AS ApprovingLimit FROM tblBidAwardingCommittee t1  WHERE t1.ApprovingLimitOnLowestPrice <= (SELECT EstItemValue FROM tblBidItems WHERE BidRefNo=" + Session["BuyerBidForBac"] + ") END ELSE BEGIN SELECT t1.BACId, t1.LastName + ', ' + t1.FirstName + ' ' + t1.MiddleName AS Name1, t1.ApprovingLimitOnNonLowestPrice AS ApprovingLimit  FROM tblBidAwardingCommittee t1 WHERE t1.ApprovingLimitOnNonLowestPrice <= (SELECT EstItemValue FROM tblBidItems WHERE BidRefNo=" + Session["BuyerBidForBac"] + ") END";
        dsApprover.SelectCommand = "select BACId, NAME1, ApprovingLimit, ApprovedDt from ( SELECT t1.BACId, t1.LastName + ', ' + t1.FirstName + ' ' + t1.MiddleName AS Name1, t2.ApprovedDt_1 ApprovedDt, 1 AS ApprovingLimit  FROM tblBidAwardingCommittee t1, tblBacBidItems t2   WHERE t1.BACId = t2.Approver_1 AND t2.BidRefNo = " + Session["BuyerBidForBac"] + " UNION SELECT t1.BACId, t1.LastName + ', ' + t1.FirstName + ' ' + t1.MiddleName AS Name1, t2.ApprovedDt_2 ApprovedDt, 2 AS ApprovingLimit  FROM tblBidAwardingCommittee t1, tblBacBidItems t2   WHERE t1.BACId = t2.Approver_2 AND t2.BidRefNo = " + Session["BuyerBidForBac"] + " UNION SELECT t1.BACId, t1.LastName + ', ' + t1.FirstName + ' ' + t1.MiddleName AS Name1, t2.ApprovedDt_3 ApprovedDt, 3 AS ApprovingLimit  FROM tblBidAwardingCommittee t1, tblBacBidItems t2   WHERE t1.BACId = t2.Approver_3 AND t2.BidRefNo = " + Session["BuyerBidForBac"] + " UNION SELECT t1.BACId, t1.LastName + ', ' + t1.FirstName + ' ' + t1.MiddleName AS Name1, t2.ApprovedDt_4 ApprovedDt, 4 AS ApprovingLimit  FROM tblBidAwardingCommittee t1, tblBacBidItems t2   WHERE t1.BACId = t2.Approver_4 AND t2.BidRefNo = " + Session["BuyerBidForBac"] + " UNION SELECT t1.BACId, t1.LastName + ', ' + t1.FirstName + ' ' + t1.MiddleName AS Name1, t2.ApprovedDt_5 ApprovedDt, 5 AS ApprovingLimit  FROM tblBidAwardingCommittee t1, tblBacBidItems t2   WHERE t1.BACId = t2.Approver_5 AND t2.BidRefNo = " + Session["BuyerBidForBac"] + " UNION SELECT t1.BACId, t1.LastName + ', ' + t1.FirstName + ' ' + t1.MiddleName AS Name1, t2.ApprovedDt_6 ApprovedDt, 6 AS ApprovingLimit  FROM tblBidAwardingCommittee t1, tblBacBidItems t2   WHERE t1.BACId = t2.Approver_6 AND t2.BidRefNo = " + Session["BuyerBidForBac"] + " UNION SELECT t1.BACId, t1.LastName + ', ' + t1.FirstName + ' ' + t1.MiddleName AS Name1, t2.ApprovedDt_7 ApprovedDt, 7 AS ApprovingLimit  FROM tblBidAwardingCommittee t1, tblBacBidItems t2   WHERE t1.BACId = t2.Approver_7 AND t2.BidRefNo = " + Session["BuyerBidForBac"] + " UNION SELECT t1.BACId, t1.LastName + ', ' + t1.FirstName + ' ' + t1.MiddleName AS Name1, t2.ApprovedDt_8 ApprovedDt,  8 AS ApprovingLimit  FROM tblBidAwardingCommittee t1, tblBacBidItems t2   WHERE t1.BACId = t2.Approver_8 AND t2.BidRefNo = " + Session["BuyerBidForBac"] + " UNION SELECT t1.BACId, t1.LastName + ', ' + t1.FirstName + ' ' + t1.MiddleName AS Name1, t2.ApprovedDt_9 ApprovedDt,  9 AS ApprovingLimit  FROM tblBidAwardingCommittee t1, tblBacBidItems t2   WHERE t1.BACId = t2.Approver_9 AND t2.BidRefNo = " + Session["BuyerBidForBac"] + " UNION SELECT t1.BACId, t1.LastName + ', ' + t1.FirstName + ' ' + t1.MiddleName AS Name1, t2.ApprovedDt_10 ApprovedDt, 10 AS ApprovingLimit  FROM tblBidAwardingCommittee t1, tblBacBidItems t2 WHERE t1.BACId = t2.Approver_10 AND t2.BidRefNo = " + Session["BuyerBidForBac"] + ") as table_1 order by ApprovingLimit";
        dsApprover.DataBind();
        Repeater RepeaterApprover1 = (Repeater)bac_bidApprovingCommittee1.FindControl("RepeaterApprover1");
        RepeaterApprover1.DataSourceID = null;
        RepeaterApprover1.DataBind();
        RepeaterApprover1.DataSourceID = "dsApprover";
        RepeaterApprover1.DataBind();
    }

    protected void UpdateClarifyTo()
    {
        string connstring = HttpContext.Current.Session["ConnectionString"].ToString();
        string sCommand;
        SqlDataReader oReader;

        sCommand = "SELECT BuyerID UserId, BuyerLastName + ', ' + BuyerFirstName + ' ' + BuyerMidName Name1 ";
        sCommand = sCommand + "FROM tblBuyers WHERE BuyerID=" + Session["ClarifiedBy"].ToString() + " ";
        sCommand = sCommand + "UNION ALL ";
        sCommand = sCommand + "SELECT PurchasingID UserId, LastName + ', ' + FirstName + ' ' + MiddleName Name1 ";
        sCommand = sCommand + "FROM tblPurchasing WHERE PurchasingID=" + Session["ClarifiedBy"].ToString() + " ";
        sCommand = sCommand + "UNION ALL ";
        sCommand = sCommand + "SELECT BACId UserId, LastName + ', ' + FirstName + ' ' + MiddleName Name1 ";
        sCommand = sCommand + "FROM tblBidAwardingCommittee WHERE BACId=" + Session["ClarifiedBy"].ToString() + " ";

        oReader = SqlHelper.ExecuteReader(connstring, CommandType.Text, sCommand);
        if (oReader.HasRows)
        {
            oReader.Read();
            txtClarifyTo.Value = oReader["Name1"].ToString();
        }
        oReader.Close();
    }

    protected void doClarify()
    {
        string connstring = HttpContext.Current.Session["ConnectionString"].ToString();
        string sCommand;

        // add row to tblBACComments table
        sCommand = "INSERT INTO tblBACClarifications (BidRefNo, FrUserId, Comment, ToUserId, DatePosted) VALUES (";
        sCommand = sCommand + Session["BuyerBidForBac"].ToString() + ", " + Session["UserId"] + ", '" + Request.Form["txtClarify"].Replace("'","''") + "', " + Session["BuyerBuyerId"] + ", GETDATE())";
        SqlHelper.ExecuteNonQuery(connstring, CommandType.Text, sCommand);

        // update tblBacBidItems table
        sCommand = "UPDATE tblBacBidItems SET Status=2, ClarifyDt_0=GETDATE(), ApprovedDt_0=NULL WHERE BacRefNo=" + Session["BuyerBacRefNo"];
        SqlHelper.ExecuteNonQuery(connstring, CommandType.Text, sCommand);
        //Response.Write(Request.Form["txtClarify"].Replace("'", "''"));

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
        Response.Redirect("bacendorsed.aspx");
    }

    protected void doApprove()
    {
        string connstring = HttpContext.Current.Session["ConnectionString"].ToString();
        string sCommand;
        //SqlDataReader oReader;

        // add row to tblBACComments table
        sCommand = "INSERT INTO tblBACComments (BidRefNo, UserId, Comment, DatePosted) VALUES (";
        sCommand = sCommand + Session["BuyerBidForBac"].ToString() + ", " + Session["UserId"] + ", '" + Request.Form["txtClarify"].Replace("'", "''") + "', GETDATE())";
        SqlHelper.ExecuteNonQuery(connstring, CommandType.Text, sCommand);


        // update tblBacBidItems table
        sCommand = "IF EXISTS (SELECT 1 FROM tblBacBidItems WHERE BacRefNo=" + Session["BuyerBacRefNo"] + " AND Approver_1 IS NULL) ";
        sCommand = sCommand + " BEGIN ";
        sCommand = sCommand + "UPDATE tblBacBidItems SET Status=3, ApprovedDt_0=GETDATE(), ClarifyDt_0=NULL, ClarifyDt_1=NULL, ClarifyDt_2=NULL, ClarifyDt_3=NULL, ClarifyDt_4=NULL, ClarifyDt_5=NULL, ClarifyDt_6=NULL, ClarifyDt_7=NULL, ClarifyDt_8=NULL, ClarifyDt_9=NULL, ClarifyDt_10=NULL WHERE BacRefNo=" + Session["BuyerBacRefNo"] + " ";
        sCommand = sCommand + " END ELSE BEGIN ";
        sCommand = sCommand + " UPDATE tblBacBidItems SET Status=1, ApprovedDt_0=GETDATE(), ClarifyDt_0=NULL, ClarifyDt_1=NULL, ClarifyDt_2=NULL, ClarifyDt_3=NULL, ClarifyDt_4=NULL, ClarifyDt_5=NULL, ClarifyDt_6=NULL, ClarifyDt_7=NULL, ClarifyDt_8=NULL, ClarifyDt_9=NULL, ClarifyDt_10=NULL WHERE BacRefNo=" + Session["BuyerBacRefNo"] + " ";
        sCommand = sCommand + " END";
        //Response.Write(sCommand);
        SqlHelper.ExecuteNonQuery(connstring, CommandType.Text, sCommand);
        //Response.Write(Request.Form["txtClarify"]);
        //Response.Write(Request.Form["txtApproved"]);

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


        if (Status == "1")
        {
           subject = "BAC For Approval";
        }
        else
        {
           subject = "Approved BAC";
        }



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


            //GET APPROVER EMAIL AS THE RECEPIENT OR IF NULL, NOTIFY BUYER AS AWARDED BAC
            sCommand = "IF EXISTS (SELECT 1 FROM tblBacBidItems WHERE BacRefNo=" + Session["BuyerBacRefNo"] + " AND Approver_1 IS NULL) ";
            sCommand = sCommand + " BEGIN ";
            sCommand = sCommand + "SELECT t1.PurchasingID, t2.LastName + ', ' + t2.FirstName + ' ' + t2.MiddleName AS Name1, t2.EmailAdd FROM tblSupervisor t1, tblPurchasing t2, tblBacBidItems t3 WHERE t1.PurchasingID=t2.PurchasingID and t1.BuyerId = t3.buyerId AND t2.PurchasingID=t3.Approver_0 and t3.BidRefNo=" + Session["BuyerBidForBac"];
            sCommand = sCommand + " END ELSE BEGIN ";
            sCommand = sCommand + "SELECT t1.BACId, t1.LastName + ', ' + t1.FirstName + ' ' + t1.MiddleName AS Name1, t1.EmailAdd  FROM tblBidAwardingCommittee t1, tblBacBidItems t2   WHERE t1.BACId = t2.Approver_1 AND t2.BidRefNo = " + Session["BuyerBidForBac"];
            sCommand = sCommand + " END";

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
        //Response.Write(CreateNotificationBodyClarify() + "<br>");
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
        sb.Append("<tr><td><p><strong>BAC for Approval</strong> <br> Sent: " + DateTime.Now.ToLongDateString() + "</p> <p>From: " + BuyersName1 + "<br><br> To: " + ApproverName1 + "<br><br> Subject: " + ItemDesc + "<br><br> Dear Bid Award Approvers, <br><br> Re: Request for Bid Award Approval – <strong>" + ItemDesc + "</strong><br><br> This is to request for your Bid Award Approval of the ff:<br><br> <b>Bid Reference Number:</b> " + Session["BuyerBidForBac"] + "<br> <b>Bid Event Name:</b> " + ItemDesc + "<br> </p> " + sTxt + "  <p>Very truly yours,<br><br><br> <strong>e-Sourcing Procurement</strong></p><p>&nbsp;</p> <p><strong>Instructions:</strong></p> <ol> <li>Go to <a href='https://e-sourcing.Trans-Asia.com.ph/'>https://e-sourcing.Trans-Asia.com.ph</a></li> <li>Enter your Username and Password then  click Login</li> <li>Click Received Bid Events for Awarding</li> <li>Click Bid Events Name</li> <li>Review / Endorse / Approve Bid event  for Awarding</li> <li>Click Clarify if you have clarification  or click Approved to award Bid Events</li> </ol> Please do not reply to this auto-generated  message.&nbsp;</td></tr>");

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

        //receiver
        sCommand = "SELECT t1.BuyerFirstName + ' ' + t1.BuyerMidName + ' ' + t1.BuyerLastName AS Name1 FROM tblBuyers t1, tblBacBidItems t2 WHERE t1.BuyerID=t2.BuyerId AND t2.BidRefNo=" + Session["BuyerBidForBac"];
        oReader = SqlHelper.ExecuteReader(connstring, CommandType.Text, sCommand);
        if (oReader.HasRows)
        {
            oReader.Read();
            BuyersName1 = oReader["Name1"].ToString();
        } oReader.Close();

        //sender
        //sCommand = "SELECT t1.BACId, t1.LastName + ', ' + t1.FirstName + ' ' + t1.MiddleName AS Name1, t1.EmailAdd  FROM tblBidAwardingCommittee t1, tblBacBidItems t2   WHERE t1.BACId = t2.Approver_1 AND t2.BidRefNo = " + Session["BuyerBidForBac"];
        sCommand = "SELECT t1.PurchasingID, t2.LastName + ', ' + t2.FirstName + ' ' + t2.MiddleName AS Name1, CONVERT(VARCHAR(17), t3.ApprovedDt_0, 113) ApprovedDt ";
        sCommand = sCommand + "FROM tblSupervisor t1, tblPurchasing t2, tblBacBidItems t3 WHERE ";
        sCommand = sCommand + "t1.PurchasingID=t2.PurchasingID and t1.BuyerId = t3.buyerId AND t2.PurchasingID=t3.Approver_0 and t3.BidRefNo=" + Session["BuyerBidForBac"];
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

        sb.Append("<tr><td><p><strong>BAC Approved to Award</strong> <br> Sent: " + DateTime.Now.ToLongDateString() + "</p> <p>From: " + ApproverName1 + "<br><br> To: " + BuyersName1 + "<br><br> Dear " + BuyersName1 + ", <br><br> Re: Approved to Award – <strong>" + ItemDesc + "</strong><br><br> We are pleased to inform you that your Bid for Award was approved as follows:<br><br> <b>Bid Reference Number:</b> " + Session["BuyerBidForBac"] + "<br> <b>Bid Event Name:</b> " + ItemDesc + "<br> </p> " + sTxt + "  <p>Very truly yours,<br><br><br> <strong>" + oApprovers + "</strong></p><p>&nbsp;</p> Please do not reply to this auto-generated  message.&nbsp;</td></tr>");

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