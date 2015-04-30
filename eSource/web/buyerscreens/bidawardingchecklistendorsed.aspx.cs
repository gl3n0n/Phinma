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
using EBid.ConnectionString;
using EBid.lib.constant;
using System.Text;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using iTextSharp.text.html;
using iTextSharp.text.html.simpleparser;
using PDFBuilder;
using System.Net;

public partial class web_buyerscreens_bidawardingchecklistendorsed : System.Web.UI.Page
{

    protected void Page_LoadComplete(object sender, EventArgs e)
    {

        //bac_bidEvaluationDetails1.Page.LoadComplete += new EventHandler(Page_LoadCompleteX);



        string sCommand, sSubCommand;
        string connstring = HttpContext.Current.Session["ConnectionString"].ToString();
        SqlDataReader oReader, oSubReader;



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

        // rebind commercial details
        //lstCommDetails.DataSourceID = null;
        //lstCommDetails.DataSourceID = "dsItems1";

        // awarding committee
        SqlDataSource dsApprover = (SqlDataSource)bac_bidApprovingCommittee1.FindControl("dsApprover");
        dsApprover.SelectCommand = "select BACId, NAME1, ApprovingLimit, ApprovedDt from ( SELECT t1.BACId, t1.LastName + ', ' + t1.FirstName + ' ' + t1.MiddleName AS Name1, t2.ApprovedDt_1 ApprovedDt, 1 AS ApprovingLimit  FROM tblBidAwardingCommittee t1, tblBacBidItems t2   WHERE t1.BACId = t2.Approver_1 AND t2.BidRefNo = " + Session["BuyerBidForBac"] + " UNION SELECT t1.BACId, t1.LastName + ', ' + t1.FirstName + ' ' + t1.MiddleName AS Name1, t2.ApprovedDt_2 ApprovedDt, 2 AS ApprovingLimit  FROM tblBidAwardingCommittee t1, tblBacBidItems t2   WHERE t1.BACId = t2.Approver_2 AND t2.BidRefNo = " + Session["BuyerBidForBac"] + " UNION SELECT t1.BACId, t1.LastName + ', ' + t1.FirstName + ' ' + t1.MiddleName AS Name1, t2.ApprovedDt_3 ApprovedDt, 3 AS ApprovingLimit  FROM tblBidAwardingCommittee t1, tblBacBidItems t2   WHERE t1.BACId = t2.Approver_3 AND t2.BidRefNo = " + Session["BuyerBidForBac"] + " UNION SELECT t1.BACId, t1.LastName + ', ' + t1.FirstName + ' ' + t1.MiddleName AS Name1, t2.ApprovedDt_4 ApprovedDt, 4 AS ApprovingLimit  FROM tblBidAwardingCommittee t1, tblBacBidItems t2   WHERE t1.BACId = t2.Approver_4 AND t2.BidRefNo = " + Session["BuyerBidForBac"] + " UNION SELECT t1.BACId, t1.LastName + ', ' + t1.FirstName + ' ' + t1.MiddleName AS Name1, t2.ApprovedDt_5 ApprovedDt, 5 AS ApprovingLimit  FROM tblBidAwardingCommittee t1, tblBacBidItems t2   WHERE t1.BACId = t2.Approver_5 AND t2.BidRefNo = " + Session["BuyerBidForBac"] + " UNION SELECT t1.BACId, t1.LastName + ', ' + t1.FirstName + ' ' + t1.MiddleName AS Name1, t2.ApprovedDt_6 ApprovedDt, 6 AS ApprovingLimit  FROM tblBidAwardingCommittee t1, tblBacBidItems t2   WHERE t1.BACId = t2.Approver_6 AND t2.BidRefNo = " + Session["BuyerBidForBac"] + " UNION SELECT t1.BACId, t1.LastName + ', ' + t1.FirstName + ' ' + t1.MiddleName AS Name1, t2.ApprovedDt_7 ApprovedDt, 7 AS ApprovingLimit  FROM tblBidAwardingCommittee t1, tblBacBidItems t2   WHERE t1.BACId = t2.Approver_7 AND t2.BidRefNo = " + Session["BuyerBidForBac"] + " UNION SELECT t1.BACId, t1.LastName + ', ' + t1.FirstName + ' ' + t1.MiddleName AS Name1, t2.ApprovedDt_8 ApprovedDt,  8 AS ApprovingLimit  FROM tblBidAwardingCommittee t1, tblBacBidItems t2   WHERE t1.BACId = t2.Approver_8 AND t2.BidRefNo = " + Session["BuyerBidForBac"] + " UNION SELECT t1.BACId, t1.LastName + ', ' + t1.FirstName + ' ' + t1.MiddleName AS Name1, t2.ApprovedDt_9 ApprovedDt,  9 AS ApprovingLimit  FROM tblBidAwardingCommittee t1, tblBacBidItems t2   WHERE t1.BACId = t2.Approver_9 AND t2.BidRefNo = " + Session["BuyerBidForBac"] + " UNION SELECT t1.BACId, t1.LastName + ', ' + t1.FirstName + ' ' + t1.MiddleName AS Name1, t2.ApprovedDt_10 ApprovedDt, 10 AS ApprovingLimit  FROM tblBidAwardingCommittee t1, tblBacBidItems t2 WHERE t1.BACId = t2.Approver_10 AND t2.BidRefNo = " + Session["BuyerBidForBac"] + ") as table_1 order by ApprovingLimit";
        dsApprover.DataBind();
        Repeater RepeaterApprover1 = (Repeater)bac_bidApprovingCommittee1.FindControl("RepeaterApprover1");
        RepeaterApprover1.DataSourceID = null;
        RepeaterApprover1.DataBind();
        RepeaterApprover1.DataSourceID = "dsApprover";
        RepeaterApprover1.DataBind();

        string control1 = Request.Form["__EVENTTARGET"];

        if (control1 == "SaveAsPDF")
        {

            HtmlToPdfBuilder builder = new HtmlToPdfBuilder(PageSize.LETTER);
            HtmlPdfPage pge = builder.AddPage();

            string contents = File.ReadAllText(Server.MapPath("PDF/BAC.htm"));
            string sTxt, SS_Nobidders, SS_NoBidsRcvd, SS_TechCompliance, descPaymentTerms, SS_crc_no;
            string SavingsGen_PctSpend, AccumulativeCost, SavingsGen_Amount, PreparedDt, sBuyerID;

            SS_Nobidders = "0";
            SS_NoBidsRcvd = "0";
            SS_TechCompliance = "0";
            SS_crc_no = "";
            descPaymentTerms = "";
            SavingsGen_PctSpend = "0";
            AccumulativeCost = "0.00";
            SavingsGen_Amount = "0.00";
            PreparedDt = "";
            sBuyerID = "";

            // BAC Bid Details
            sCommand = "SELECT ItemDesc, BidRefNo, SAPPRNo, CONVERT(VARCHAR(20), CONVERT(MONEY, BidAmount), 1) BidAmount, BuyerId, ";
            sCommand = sCommand + "CONVERT(VARCHAR(11), SAPPRDate, 106) SAPPRDate, CONVERT(VARCHAR(17), PreparedDt, 113) PreparedDt, ";
            sCommand = sCommand + "CASE Budgeted WHEN 1 THEN 'Budgeted' ELSE 'Unbudgeted' END Budgeted, ";
            sCommand = sCommand + "CASE CompanyId WHEN 0 THEN 'GT' WHEN 1 THEN 'IC' WHEN 2 THEN 'GXI' ELSE 'EGG' END CompanyId, ";
            sCommand = sCommand + "SS_Nobidders, SS_NoBidsRcvd, SS_TechCompliance, ss_crc_no, descPaymentTerms, SavingsGen_PctSpend, ";
            sCommand = sCommand + "CONVERT(VARCHAR(20), CONVERT(MONEY, AccumulativeCost), 1) AccumulativeCost, ";
            sCommand = sCommand + "CONVERT(VARCHAR(20), CONVERT(MONEY, SavingsGen_Amount), 1) SavingsGen_Amount ";
            sCommand = sCommand + "FROM tblBACBidItems WHERE BacRefNo=" + Session["BuyerBacRefNo"];
            oReader = SqlHelper.ExecuteReader(connstring, CommandType.Text, sCommand);
            if (oReader.HasRows)
            {
                oReader.Read();
                contents = contents.Replace("[BidRefNo]", oReader["BidRefNo"].ToString());
                contents = contents.Replace("[ItemDesc]", oReader["ItemDesc"].ToString());
                contents = contents.Replace("[SAPPRNo]", oReader["SAPPRNo"].ToString());
                contents = contents.Replace("[BidAmount]", oReader["BidAmount"].ToString());
                contents = contents.Replace("[SAPPRDate]", oReader["SAPPRDate"].ToString());
                contents = contents.Replace("[PreparedDt]", oReader["PreparedDt"].ToString());
                contents = contents.Replace("[Budgeted]", oReader["Budgeted"].ToString());
                contents = contents.Replace("[CompanyId]", "Company: " + oReader["CompanyId"].ToString());
                SS_Nobidders = oReader["SS_Nobidders"].ToString() != "" ? oReader["SS_Nobidders"].ToString() : "&nbsp;";
                SS_NoBidsRcvd = oReader["SS_NoBidsRcvd"].ToString() != "" ? oReader["SS_NoBidsRcvd"].ToString() : "&nbsp;";
                SS_TechCompliance = oReader["SS_TechCompliance"].ToString() != "" ? oReader["SS_TechCompliance"].ToString() : "&nbsp;";
                SS_crc_no = oReader["ss_crc_no"].ToString() != "" ? oReader["ss_crc_no"].ToString() : "&nbsp;";
                descPaymentTerms = oReader["descPaymentTerms"].ToString() != "" ? oReader["descPaymentTerms"].ToString() : "&nbsp;";
                SavingsGen_PctSpend = oReader["SavingsGen_PctSpend"].ToString() != "" ? oReader["SavingsGen_PctSpend"].ToString() : "&nbsp;";
                AccumulativeCost = oReader["AccumulativeCost"].ToString() != "" ? oReader["AccumulativeCost"].ToString() : "&nbsp;";
                SavingsGen_Amount = oReader["SavingsGen_Amount"].ToString() != "" ? oReader["SavingsGen_Amount"].ToString() : "&nbsp;";
                PreparedDt = oReader["PreparedDt"].ToString() != "" ? oReader["PreparedDt"].ToString() : "&nbsp;";
                sBuyerID = oReader["BuyerId"].ToString() != "" ? oReader["BuyerId"].ToString() : "&nbsp;";
            }

            // Executive Summary
            sCommand = "SELECT Comment FROM tblBACExecutiveSummary WHERE BidRefNo=" + Session["BuyerBidForBac"];
            oReader = SqlHelper.ExecuteReader(connstring, CommandType.Text, sCommand);
            if (oReader.HasRows)
            {
                oReader.Read();
                contents = contents.Replace("[ExecSummary]", "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + oReader["Comment"].ToString());
            }

            // Remarks/Comments
            sTxt = "";
            sCommand = "SELECT A.FrUserId, B.Name1 FrName, A.Comment, A.ToUserId, C.Name1 ToName, CONVERT(VARCHAR(20), A.DatePosted, 100) DatePosted FROM ";
            sCommand = sCommand + "(SELECT FrUserId, Comment, ToUserId, DatePosted FROM tblBACClarifications ";
            sCommand = sCommand + "WHERE BidRefNo=" + Session["BuyerBidForBac"] + " AND (Comment IS NOT NULL ) ";
            sCommand = sCommand + "UNION ALL ";
            sCommand = sCommand + "SELECT UserId FrUserId, Comment, UserId ToUserId, DatePosted FROM tblBACComments ";
            sCommand = sCommand + "WHERE BidRefNo=" + Session["BuyerBidForBac"] + " AND (Comment IS NOT NULL) ";
            sCommand = sCommand + ") AS A ";
            sCommand = sCommand + "INNER JOIN ";
            sCommand = sCommand + "(SELECT BuyerId UserId, BuyerLastName + ', ' + BuyerFirstName + ' ' + BuyerMidName AS Name1 FROM tblBuyers ";
            sCommand = sCommand + "UNION ALL ";
            sCommand = sCommand + "SELECT PurchasingID UserId, LastName + ', ' + FirstName + ' ' + MiddleName AS Name1 FROM tblPurchasing ";
            sCommand = sCommand + "UNION ALL ";
            sCommand = sCommand + "SELECT BACId UserId, LastName + ', ' + FirstName + ' ' + MiddleName AS Name1 FROM tblBidAwardingCommittee) ";
            sCommand = sCommand + "B ON A.FrUserId=B.UserId ";
            sCommand = sCommand + "INNER JOIN ";
            sCommand = sCommand + "(SELECT BuyerId UserId, BuyerLastName + ', ' + BuyerFirstName + ' ' + BuyerMidName AS Name1 FROM tblBuyers ";
            sCommand = sCommand + "UNION ALL ";
            sCommand = sCommand + "SELECT PurchasingID UserId, LastName + ', ' + FirstName + ' ' + MiddleName AS Name1 FROM tblPurchasing ";
            sCommand = sCommand + "UNION ALL ";
            sCommand = sCommand + "SELECT BACId UserId, LastName + ', ' + FirstName + ' ' + MiddleName AS Name1 FROM tblBidAwardingCommittee) ";
            sCommand = sCommand + "C ON A.ToUserId=C.UserId ";
            sCommand = sCommand + "ORDER BY DatePosted DESC ";
            oReader = SqlHelper.ExecuteReader(connstring, CommandType.Text, sCommand);
            if (oReader.HasRows)
            {
                while (oReader.Read())
                {
                    sTxt = sTxt + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;•&nbsp;" + oReader["FrName"].ToString() + "<br />";
                    sTxt = sTxt + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + oReader["DatePosted"].ToString() + "<br />";
                    sTxt = sTxt + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + oReader["Comment"].ToString() + "&nbsp;<br />";
                }
            }
			sTxt = sTxt + "&nbsp;";
            contents = contents.Replace("[RemComments]", sTxt);

            // Supply Position
            sTxt = "";
            sCommand = "SELECT SupplyPosition FROM tblBACSupplyPosition WHERE BidRefNo=" + Session["BuyerBidForBac"];
            oReader = SqlHelper.ExecuteReader(connstring, CommandType.Text, sCommand);
            if (oReader.HasRows)
            {
                while (oReader.Read())
                {
                    sTxt = sTxt + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;•&nbsp;" + oReader["SupplyPosition"].ToString() + "<br />";
                }
            }
			sTxt = sTxt + "&nbsp;";
            contents = contents.Replace("[SupplyPosition]", sTxt);

            // Sourcing Strategy
            sTxt = "";
            sCommand = "SELECT SourcingStrategy FROM tblBACSourcingStrategy WHERE BidRefNo=" + Session["BuyerBidForBac"];
            oReader = SqlHelper.ExecuteReader(connstring, CommandType.Text, sCommand);
            if (oReader.HasRows)
            {
                while (oReader.Read())
                {
                    sTxt = sTxt + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;•&nbsp;" + oReader["SourcingStrategy"].ToString() + "<br />";
                }
            }
			sTxt = sTxt + "&nbsp;";
            contents = contents.Replace("[SourcingStrategy]", sTxt);

            contents = contents.Replace("[SS_Nobidders]", SS_Nobidders);
            contents = contents.Replace("[SS_NoBidsRcvd]", SS_NoBidsRcvd);
            contents = contents.Replace("[SS_TechCompliance]", SS_TechCompliance);
            contents = contents.Replace("[SS_crc_no]", SS_crc_no);

            // Type of Purchase
            sTxt = "";
            sCommand = "SELECT TypeOfPurchase FROM tblBACTypeOfPurchase WHERE BidRefNo=" + Session["BuyerBidForBac"];
            oReader = SqlHelper.ExecuteReader(connstring, CommandType.Text, sCommand);
            if (oReader.HasRows)
            {
                oReader.Read();
                sTxt = sTxt + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;•&nbsp;" + oReader["TypeOfPurchase"].ToString() + "<br />";
            }
			sTxt = sTxt + "&nbsp;";
            contents = contents.Replace("[TypeOfPurchase]", sTxt);

            // Payment Terms
            sTxt = "";
            sCommand = "SELECT PaymentTerm FROM tblBACPaymentTerms WHERE BidRefNo=" + Session["BuyerBidForBac"];
            oReader = SqlHelper.ExecuteReader(connstring, CommandType.Text, sCommand);
            if (oReader.HasRows)
            {
                while (oReader.Read())
                {
                    sTxt = sTxt + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;•&nbsp;" + oReader["PaymentTerm"].ToString() + "<br />";
                }
            }
			sTxt = sTxt + "&nbsp;";
            contents = contents.Replace("[PaymentTerms]", sTxt);

            contents = contents.Replace("[PaymentTermsDesc]", descPaymentTerms);

            // Supporting Documents Attached
            sTxt = ""; // Approved SAP PR
            sCommand = "SELECT OriginalFileName ActualFileName FROM tblBACSupportingDocuments WHERE BidRefNo=" + Session["BuyerBidForBac"] + " AND DocuName='Approved_PR' ORDER BY FileUploadID";
            oReader = SqlHelper.ExecuteReader(connstring, CommandType.Text, sCommand);
            if (oReader.HasRows)
            {
                while (oReader.Read())
                {
                    sTxt = sTxt + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;•&nbsp;" + oReader["ActualFileName"].ToString() + "&nbsp;<br />";
                }
            }
			sTxt = sTxt + "&nbsp;";
            contents = contents.Replace("[ApprovedSAPPR]", sTxt);

            sTxt = ""; // Commercial Evaluation
            sCommand = "SELECT OriginalFileName ActualFileName FROM tblBACSupportingDocuments WHERE BidRefNo=" + Session["BuyerBidForBac"] + " AND DocuName='Commercial_Evaluation' ORDER BY FileUploadID";
            oReader = SqlHelper.ExecuteReader(connstring, CommandType.Text, sCommand);
            if (oReader.HasRows)
            {
                while (oReader.Read())
                {
                    sTxt = sTxt + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;•&nbsp;" + oReader["ActualFileName"].ToString() + "&nbsp;<br />";
                }
            }
			sTxt = sTxt + "&nbsp;";
            contents = contents.Replace("[CommercialEvaluation]", sTxt);

            sTxt = ""; // Approved Business Case
            sCommand = "SELECT OriginalFileName ActualFileName FROM tblBACSupportingDocuments WHERE BidRefNo=" + Session["BuyerBidForBac"] + " AND DocuName='Approved_Business_Case' ORDER BY FileUploadID";
            oReader = SqlHelper.ExecuteReader(connstring, CommandType.Text, sCommand);
            if (oReader.HasRows)
            {
                while (oReader.Read())
                {
                    sTxt = sTxt + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;•&nbsp;" + oReader["ActualFileName"].ToString() + "&nbsp;<br />";
                }
            }
			sTxt = sTxt + "&nbsp;";
            contents = contents.Replace("[ApprovedBusinessCase]", sTxt);

            sTxt = ""; // Negotiation Results
            sCommand = "SELECT OriginalFileName ActualFileName FROM tblBACSupportingDocuments WHERE BidRefNo=" + Session["BuyerBidForBac"] + " AND DocuName='Negotiation_Results' ORDER BY FileUploadID";
            oReader = SqlHelper.ExecuteReader(connstring, CommandType.Text, sCommand);
            if (oReader.HasRows)
            {
                while (oReader.Read())
                {
                    sTxt = sTxt + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;•&nbsp;" + oReader["ActualFileName"].ToString() + "&nbsp;<br />";
                }
            }
			sTxt = sTxt + "&nbsp;";
            contents = contents.Replace("[NegotiationResults]", sTxt);

            sTxt = ""; // Board Resolution
            sCommand = "SELECT OriginalFileName ActualFileName FROM tblBACSupportingDocuments WHERE BidRefNo=" + Session["BuyerBidForBac"] + " AND DocuName='Board_Resolution' ORDER BY FileUploadID";
            oReader = SqlHelper.ExecuteReader(connstring, CommandType.Text, sCommand);
            if (oReader.HasRows)
            {
                while (oReader.Read())
                {
                    sTxt = sTxt + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;•&nbsp;" + oReader["ActualFileName"].ToString() + "&nbsp;<br />";
                }
            }
			sTxt = sTxt + "&nbsp;";
            contents = contents.Replace("[BoardResolution]", sTxt);

            sTxt = ""; // Others
            sCommand = "SELECT OriginalFileName ActualFileName FROM tblBACSupportingDocuments WHERE BidRefNo=" + Session["BuyerBidForBac"] + " AND DocuName='Others' ORDER BY FileUploadID";
            oReader = SqlHelper.ExecuteReader(connstring, CommandType.Text, sCommand);
            if (oReader.HasRows)
            {
                while (oReader.Read())
                {
                    sTxt = sTxt + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;•&nbsp;" + oReader["ActualFileName"].ToString() + "&nbsp;<br />";
                }
            }
			sTxt = sTxt + "&nbsp;";
            contents = contents.Replace("[Others]", sTxt);

            sTxt = ""; // Technical Evaluation
            sCommand = "SELECT OriginalFileName ActualFileName FROM tblBACSupportingDocuments WHERE BidRefNo=" + Session["BuyerBidForBac"] + " AND DocuName='Technical_Evaluation' ORDER BY FileUploadID";
            oReader = SqlHelper.ExecuteReader(connstring, CommandType.Text, sCommand);
            if (oReader.HasRows)
            {
                while (oReader.Read())
                {
                    sTxt = sTxt + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;•&nbsp;" + oReader["ActualFileName"].ToString() + "&nbsp;<br />";
                }
            }
			sTxt = sTxt + "&nbsp;";
            contents = contents.Replace("[TechnicalEvaluation]", sTxt);

            // Basis for Awarding
            sTxt = ""; // Lowest-Price Bidder
            sCommand = "SELECT OriginalFileName ActualFileName FROM tblBACSupportingDocuments WHERE BidRefNo=" + Session["BuyerBidForBac"] + " AND DocuName='bfa_LowestPr' ORDER BY FileUploadID";
            oReader = SqlHelper.ExecuteReader(connstring, CommandType.Text, sCommand);
            if (oReader.HasRows)
            {
                while (oReader.Read())
                {
                    sTxt = sTxt + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;•&nbsp;" + oReader["ActualFileName"].ToString() + "&nbsp;<br />";
                }
            }
			sTxt = sTxt + "&nbsp;";
            contents = contents.Replace("[LowestPriceBidder]", sTxt);

            sTxt = ""; // Sole Source w/ LOA Approved Waiver of Completion
            sCommand = "SELECT OriginalFileName ActualFileName FROM tblBACSupportingDocuments WHERE BidRefNo=" + Session["BuyerBidForBac"] + " AND DocuName='bfa_SoleLOA' ORDER BY FileUploadID";
            oReader = SqlHelper.ExecuteReader(connstring, CommandType.Text, sCommand);
            if (oReader.HasRows)
            {
                while (oReader.Read())
                {
                    sTxt = sTxt + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;•&nbsp;" + oReader["ActualFileName"].ToString() + "&nbsp;<br />";
                }
            }
			sTxt = sTxt + "&nbsp;";
            contents = contents.Replace("[SoleSourceLOA]", sTxt);

            sTxt = ""; // Sole Source w/ Certificate of Sale Distributorship
            sCommand = "SELECT OriginalFileName ActualFileName FROM tblBACSupportingDocuments WHERE BidRefNo=" + Session["BuyerBidForBac"] + " AND DocuName='bfa_SoleCSD' ORDER BY FileUploadID";
            oReader = SqlHelper.ExecuteReader(connstring, CommandType.Text, sCommand);
            if (oReader.HasRows)
            {
                while (oReader.Read())
                {
                    sTxt = sTxt + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;•&nbsp;" + oReader["ActualFileName"].ToString() + "&nbsp;<br />";
                }
            }
			sTxt = sTxt + "&nbsp;";
            contents = contents.Replace("[SoleSourceCertificate]", sTxt);

            sTxt = ""; // Compliance to Technical/ Functional Specifications
            sCommand = "SELECT OriginalFileName ActualFileName FROM tblBACSupportingDocuments WHERE BidRefNo=" + Session["BuyerBidForBac"] + " AND DocuName='bfa_TechSpec' ORDER BY FileUploadID";
            oReader = SqlHelper.ExecuteReader(connstring, CommandType.Text, sCommand);
            if (oReader.HasRows)
            {
                while (oReader.Read())
                {
                    sTxt = sTxt + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;•&nbsp;" + oReader["ActualFileName"].ToString() + "&nbsp;<br />";
                }
            }
            contents = contents.Replace("[ComplianceTechnicalFunctional]", sTxt);

            sTxt = ""; // Compliance to Delivery Lead-Time
            sCommand = "SELECT OriginalFileName ActualFileName FROM tblBACSupportingDocuments WHERE BidRefNo=" + Session["BuyerBidForBac"] + " AND DocuName='bfa_LeadTime' ORDER BY FileUploadID";
            oReader = SqlHelper.ExecuteReader(connstring, CommandType.Text, sCommand);
            if (oReader.HasRows)
            {
                while (oReader.Read())
                {
                    sTxt = sTxt + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;•&nbsp;" + oReader["ActualFileName"].ToString() + "&nbsp;<br />";
                }
            }
			sTxt = sTxt + "&nbsp;";
            contents = contents.Replace("[ComplianceDelivery]", sTxt);

            sTxt = ""; // Others
            sCommand = "SELECT OriginalFileName ActualFileName FROM tblBACSupportingDocuments WHERE BidRefNo=" + Session["BuyerBidForBac"] + " AND DocuName='bfa_BFAOth' ORDER BY FileUploadID";
            oReader = SqlHelper.ExecuteReader(connstring, CommandType.Text, sCommand);
            if (oReader.HasRows)
            {
                while (oReader.Read())
                {
                    sTxt = sTxt + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;•&nbsp;" + oReader["ActualFileName"].ToString() + "&nbsp;<br />";
                }
            }
			sTxt = sTxt + "&nbsp;";
            contents = contents.Replace("[Others1]", sTxt);

            // Bid Evaluation Summary
            sTxt = "<tr><td><b>&nbsp;VENDOR / CRITERIA</b></td>"; // Vendor / Criteria
            sCommand = "SELECT VendorName FROM tblBACEvaluationSummary WHERE BidRefNo=" + Session["BuyerBidForBac"] + " ORDER BY VendorName ASC";
            oReader = SqlHelper.ExecuteReader(connstring, CommandType.Text, sCommand);
            if (oReader.HasRows)
            {
                while (oReader.Read())
                {
                    sTxt = sTxt + "<td>";
                    sTxt = sTxt + "&nbsp;" + oReader["VendorName"].ToString() + "&nbsp;";
                    sTxt = sTxt + "</td>";
                }
            }
			sTxt = sTxt + "&nbsp;";
            //sTxt = sTxt + "<tr>";

            sTxt = sTxt + "<tr><td><b>&nbsp;Accreditation Status</b></td>"; // Accreditation Status
            sCommand = "SELECT ";
            sCommand = sCommand + "    CASE Accredited ";
            sCommand = sCommand + "        WHEN 1 THEN 'Accredited' ";
            sCommand = sCommand + "        WHEN 2 THEN 'Unaccredited' ";
            sCommand = sCommand + "        WHEN 3 THEN 'One Time Supplier' ";
            sCommand = sCommand + "        WHEN 4 THEN 'Exempted' ";
            sCommand = sCommand + "        ELSE 'Due For Renewal' ";
            sCommand = sCommand + "    END Accredited ";
            sCommand = sCommand + "FROM tblBACEvaluationSummary ";
            sCommand = sCommand + "WHERE BidRefNo=" + Session["BuyerBidForBac"] + " ";
            sCommand = sCommand + "ORDER BY VendorName ASC";
            oReader = SqlHelper.ExecuteReader(connstring, CommandType.Text, sCommand);
            if (oReader.HasRows)
            {
                while (oReader.Read())
                {
                    sTxt = sTxt + "<td>";
                    sTxt = sTxt + "&nbsp;" + oReader["Accredited"].ToString() + "&nbsp;";
                    sTxt = sTxt + "</td>";
                }
            }
			sTxt = sTxt + "&nbsp;";
            //sTxt = sTxt + "<tr>";

            sTxt = sTxt + "<tr><td><b>&nbsp;Performance Rating</b></td>"; // Performance Rating
            sCommand = "SELECT PerformanceRating FROM tblBACEvaluationSummary WHERE BidRefNo=" + Session["BuyerBidForBac"] + " ORDER BY VendorName ASC";
            oReader = SqlHelper.ExecuteReader(connstring, CommandType.Text, sCommand);
            if (oReader.HasRows)
            {
                while (oReader.Read())
                {
                    sTxt = sTxt + "<td>";
                    sTxt = sTxt + "&nbsp;" + oReader["PerformanceRating"].ToString() + "&nbsp;";
                    sTxt = sTxt + "</td>";
                }
            }
			sTxt = sTxt + "&nbsp;";
            //sTxt = sTxt + "<tr>";

            sTxt = sTxt + "<tr><td><b>&nbsp;Technical Compliance</b></td>"; // Technical Compliance
            sCommand = "SELECT ";
            sCommand = sCommand + "    CASE TechCompliance ";
            sCommand = sCommand + "        WHEN 1 THEN 'Not-Compliant' ";
            sCommand = sCommand + "        WHEN 2 THEN 'Fully Compliant' ";
            sCommand = sCommand + "        ELSE '' ";
            sCommand = sCommand + "    END TechCompliance ";
            sCommand = sCommand + "FROM tblBACEvaluationSummary ";
            sCommand = sCommand + "WHERE BidRefNo=" + Session["BuyerBidForBac"] + " ";
            sCommand = sCommand + "ORDER BY VendorName ASC";
            oReader = SqlHelper.ExecuteReader(connstring, CommandType.Text, sCommand);
            if (oReader.HasRows)
            {
                while (oReader.Read())
                {
                    sTxt = sTxt + "<td>";
                    sTxt = sTxt + "&nbsp;" + oReader["TechCompliance"].ToString() + "&nbsp;";
                    sTxt = sTxt + "</td>";
                }
            }
			sTxt = sTxt + "&nbsp;";
            //sTxt = sTxt + "<tr>";

            sTxt = sTxt + "<tr><td><b>&nbsp;Commercial Compliance</b></td>"; // Commercial Compliance
            sCommand = "SELECT ";
            sCommand = sCommand + "    CASE CommCompliance ";
            sCommand = sCommand + "        WHEN 1 THEN 'Not-Compliant' ";
            sCommand = sCommand + "        WHEN 2 THEN 'Partial Compliant' ";
            sCommand = sCommand + "        WHEN 3 THEN 'Fully Compliant' ";
            sCommand = sCommand + "        ELSE '' ";
            sCommand = sCommand + "    END CommCompliance ";
            sCommand = sCommand + "FROM tblBACEvaluationSummary ";
            sCommand = sCommand + "WHERE BidRefNo=" + Session["BuyerBidForBac"] + " ";
            sCommand = sCommand + "ORDER BY VendorName ASC";
            oReader = SqlHelper.ExecuteReader(connstring, CommandType.Text, sCommand);
            if (oReader.HasRows)
            {
                while (oReader.Read())
                {
                    sTxt = sTxt + "<td>";
                    sTxt = sTxt + "&nbsp;" + oReader["CommCompliance"].ToString() + "&nbsp;";
                    sTxt = sTxt + "</td>";
                }
            }
			sTxt = sTxt + "&nbsp;";
            //sTxt = sTxt + "<tr>";

            sTxt = sTxt + "<tr><td><b>&nbsp;Contract Compliance</b></td>"; // Contract Compliance
            sCommand = "SELECT ";
            sCommand = sCommand + "    CASE ContCompliance ";
            sCommand = sCommand + "        WHEN 1 THEN 'Not-Applicable' ";
            sCommand = sCommand + "        WHEN 2 THEN 'Not-Compliant' ";
            sCommand = sCommand + "        WHEN 3 THEN 'Partial Compliant' ";
            sCommand = sCommand + "        WHEN 4 THEN 'Fully Compliant' ";
            sCommand = sCommand + "        ELSE '' ";
            sCommand = sCommand + "    END ContCompliance ";
            sCommand = sCommand + "FROM tblBACEvaluationSummary ";
            sCommand = sCommand + "WHERE BidRefNo=" + Session["BuyerBidForBac"] + " ";
            sCommand = sCommand + "ORDER BY VendorName ASC";
            oReader = SqlHelper.ExecuteReader(connstring, CommandType.Text, sCommand);
            if (oReader.HasRows)
            {
                while (oReader.Read())
                {
                    sTxt = sTxt + "<td>";
                    sTxt = sTxt + "&nbsp;" + oReader["ContCompliance"].ToString() + "&nbsp;";
                    sTxt = sTxt + "</td>";
                }
            }
			sTxt = sTxt + "&nbsp;";
            //sTxt = sTxt + "<tr>";

            contents = contents.Replace("[BidEvaluationSummary]", sTxt);

            // Items
            sTxt = "<tr>";
            sTxt = sTxt + "<td><h2>&nbsp;Item #</h2></td>";
            sTxt = sTxt + "<td><h2>&nbsp;Item Details</h2></td>";
            sTxt = sTxt + "<td><h2>&nbsp;PR No</h2></td>";
            sTxt = sTxt + "</tr>";
            sCommand = "SELECT DISTINCT BidDetailNo, ItemName, PR_No ";
            sCommand = sCommand + "FROM  tblBACEvaluationDetails ";
            sCommand = sCommand + "WHERE BidRefNo = " + Session["BuyerBidForBac"] + " ORDER BY BidDetailNo";
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
                    sTxt = sTxt + "&nbsp;" + oReader["PR_No"].ToString() + "&nbsp;";
                    sTxt = sTxt + "</td>";
                    sTxt = sTxt + "</tr>";
                }
            }
			sTxt = sTxt + "&nbsp;";
            //sTxt = sTxt + "<tr>";
            contents = contents.Replace("[BidEvalItems]", sTxt);
    
    
            // COMMERCIAL DETAIL
            sTxt = "<tr><td><b>&nbsp;Item Description</b><br><b>&nbsp;Qty in base unit</b></td><td><b>&nbsp;BIDDER:</b></td>"; // Vendor / ITEMS
            sCommand = "SELECT VendorName FROM tblBACEvaluationDetails WHERE BidRefNo=" + Session["BuyerBidForBac"] + " ORDER BY VendorName ASC";
            oReader = SqlHelper.ExecuteReader(connstring, CommandType.Text, sCommand);
            if (oReader.HasRows)
            {
                while (oReader.Read())
                {
                   sTxt = sTxt + "<td>";
                   sTxt = sTxt + "&nbsp;" + oReader["VendorName"].ToString() + "&nbsp;";
                   sTxt = sTxt + "</td>";
               }
            }
            
            sCommand = "SELECT t1.ItemName, t1.ItemName + '<br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;' + cast(max(t1.Qty) as varchar) + ' ' + max(t1.UnitMeasure) ItemNameD FROM tblBACEvaluationDetails t1 WHERE t1.BidRefNo = " + Session["BuyerBidForBac"] + " GROUP BY t1.ItemName Order by t1.ItemName";
            oReader = SqlHelper.ExecuteReader(connstring, CommandType.Text, sCommand);
            if (oReader.HasRows)
            {
                while (oReader.Read())
                {
                   sTxt = sTxt + "<tr>";
                   sTxt = sTxt + "<td>";
                   sTxt = sTxt + "&nbsp;" + oReader["ItemNameD"].ToString() + "&nbsp;";
                   sTxt = sTxt + "</td>";
                   sTxt = sTxt + "<td>";
                   sTxt = sTxt + "&nbsp;Value:<br>&nbsp;Price:<br>&nbsp;Currency<br>&nbsp;Rank:";
                   sTxt = sTxt + "</td>";

                      sSubCommand = "SELECT CASE WHEN t1.TotalCost is NOT null THEN CONVERT(VARCHAR(20), CONVERT(MONEY, t1.TotalCost), 1) + '<br>&nbsp;&nbsp;&nbsp;' + CONVERT(VARCHAR(20), CONVERT(MONEY, t1.UnitCost), 1) + '<br>&nbsp;&nbsp;&nbsp;' + t1.Currenzy + '<br>&nbsp;&nbsp;&nbsp;' + CAST(t1.Ranking as varchar) ELSE 'NO PRICE' END AS ItemDtl ";
                      sSubCommand = sSubCommand + "FROM tblBACEvaluationDetails t1 RIGHT OUTER JOIN tblBACEvaluationSummary t2 ";
                      sSubCommand = sSubCommand + "ON t1.BidRefNO = t2.BidRefNO AND t1.VendorName = t2.VendorName ";
                      sSubCommand = sSubCommand + "WHERE t2.BidRefNO = " + Session["BuyerBidForBac"] + " ";
                      sSubCommand = sSubCommand + "AND   t1.ItemName = '" + oReader["ItemName"].ToString() + "' ";
                      sSubCommand = sSubCommand + "Order by t2.VendorName";
                      oSubReader = SqlHelper.ExecuteReader(connstring, CommandType.Text, sSubCommand);
                      if (oSubReader.HasRows)
                      {
                          while (oSubReader.Read())
                          {
                              sTxt = sTxt + "<td>";
                              sTxt = sTxt + "&nbsp;&nbsp;&nbsp;" + oSubReader["ItemDtl"].ToString() + "&nbsp;";
                              sTxt = sTxt + "</td>";
                          }
                      }

                   //sTxt = sTxt + "<tr>";
 			       sTxt = sTxt + "&nbsp;";
               }
            }
            
			sTxt = sTxt + "&nbsp;";
            contents = contents.Replace("BidCommercialDetail", sTxt);
            
            // Awarded To
            sTxt = "<tr>";
            sTxt = sTxt + "<td><h2>&nbsp;Item #</h2></td>";
            sTxt = sTxt + "<td><h2>&nbsp;Item Details</h2></td>";
            sTxt = sTxt + "<td><h2>&nbsp;Vendor Name</h2></td>";
            sTxt = sTxt + "<td><h2>&nbsp;Qty</h2></td>";
            sTxt = sTxt + "<td><h2>&nbsp;Total</h2></td>";
            sTxt = sTxt + "<td><h2>&nbsp;Ranking</h2></td>";
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
            //sTxt = sTxt + "<tr>";
            sTxt = sTxt + "&nbsp;";
            contents = contents.Replace("[BidAwardedTo]", sTxt);

            // Nature of Savings
            sTxt = "";
            sCommand = "SELECT NatureOfSavings FROM tblBACNatureOfSavings WHERE BidRefNo=" + Session["BuyerBidForBac"];
            oReader = SqlHelper.ExecuteReader(connstring, CommandType.Text, sCommand);
            if (oReader.HasRows)
            {
                while (oReader.Read())
                {
                    sTxt = sTxt + "&nbsp;•&nbsp;" + oReader["NatureOfSavings"].ToString() + "&nbsp;<br />";
                }
            }
            sTxt = sTxt + "&nbsp;";
            contents = contents.Replace("[NatureOfSavings]", sTxt);

            // Savings Generated
            contents = contents.Replace("[AccumulatedCost]", AccumulativeCost);
            contents = contents.Replace("[SavingsAmount]", SavingsGen_Amount);
            contents = contents.Replace("[PercentSavings]", SavingsGen_PctSpend);

            // Purchasing
            contents = contents.Replace("[PreparedDate]", PreparedDt);

            sCommand = "SELECT t1.BuyerLastName + ', ' + t1.BuyerFirstName + ' ' + t1.BuyerMidName AS Name1 FROM tblBuyers t1, tblBacBidItems t2 ";
            sCommand = sCommand + "WHERE t1.BuyerID=t2.BuyerId AND t2.BacRefNo=" + Session["BuyerBacRefNo"];
            oReader = SqlHelper.ExecuteReader(connstring, CommandType.Text, sCommand);
            if (oReader.HasRows)
            {
                oReader.Read();
                contents = contents.Replace("[PreparedBy]", oReader["Name1"].ToString() != "" ? oReader["Name1"].ToString() : "&nbsp;");
            }

            sCommand = "SELECT t1.PurchasingID, t2.LastName + ', ' + t2.FirstName + ' ' + t2.MiddleName AS Name1, CONVERT(VARCHAR(17), t3.ApprovedDt_0, 113) ApprovedDt ";
            sCommand = sCommand + "FROM tblSupervisor t1, tblPurchasing t2, tblBacBidItems t3 WHERE ";
            sCommand = sCommand + "t1.PurchasingID=t2.PurchasingID and t1.BuyerId = t3.buyerId AND t2.PurchasingID=t3.Approver_0 and t3.BidRefNo=" + Session["BuyerBidForBac"];
            oReader = SqlHelper.ExecuteReader(connstring, CommandType.Text, sCommand);
            if (oReader.HasRows)
            {
                oReader.Read();
                contents = contents.Replace("[ReviewedBy]", oReader["Name1"].ToString() != "" ? oReader["Name1"].ToString() : "&nbsp;");
                contents = contents.Replace("[ReviewedDt]", oReader["ApprovedDt"].ToString() != "" ? oReader["ApprovedDt"].ToString() : "&nbsp;");
            }



            // Bid Approving Authority
            sTxt = "<tr>";
            sCommand = "SELECT Name1, CASE WHEN ApprovedDt IS NOT NULL THEN CONVERT(VARCHAR(17), ApprovedDt, 113) + '<br>&nbsp;Approver' else 'Approver' END ApprovedDt FROM vwBacBidApprovers WHERE BidRefNo=" + Session["BuyerBidForBac"] + " Order by ApprovingLimit";
            oReader = SqlHelper.ExecuteReader(connstring, CommandType.Text, sCommand);
            if (oReader.HasRows)
            {
                while (oReader.Read())
                {
                    sTxt = sTxt + "<td><br>&nbsp;Approved By / Date:<br><br /><b>&nbsp;" + oReader["Name1"].ToString() + "&nbsp;</b><br>&nbsp;" + oReader["ApprovedDt"].ToString() + "</td>";
                }
            }else
            {
				sTxt = sTxt + "<td>&nbsp;</td>";
            }
            sTxt = sTxt + "</tr>";
			contents = contents.Replace("[BacApprovers]", sTxt);

			//Response.Write (contents);
            // Convert to PDF
            pge.AppendHtml(contents);
            //color:#FFFFFF;
            builder.AddStyle("H1", "font-family:Arial; font-size:12px; font-weight:bold; bgcolor:#70A8D2;");
            builder.AddStyle("H2", "font-family:Arial; font-size:10px; font-weight:bold; bgcolor:#DBEAF5;");
            builder.AddStyle("p", "font-family:Arial; font-size:10px; bgcolor:#FFFFFF;");
            byte[] file = builder.RenderPdf();
            File.WriteAllBytes(Server.MapPath("PDF/BAC-" + Session["BuyerBidForBac"] + ".pdf"), file);
            
            Response.Redirect("PDF/BAC-" + Session["BuyerBidForBac"] + ".pdf");
            
            builder = null;
            pge = null;
            file = null;

            // redirect
            //Response.Redirect("bacendorsed.aspx");
            // END SaveAsPDF

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
        if (Session["BuyerBidForBac"] == null)
        {
            Response.Redirect(System.Configuration.ConfigurationManager.AppSettings["BuyerHomePage"]);
        }

    }
}