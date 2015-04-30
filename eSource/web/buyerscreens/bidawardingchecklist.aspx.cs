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
using EBid.ConnectionString;
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
using EBid.ConnectionString;
using EBid.lib.constant;
using EBid.ConnectionString;


public partial class web_buyerscreens_bidawardingchecklist : System.Web.UI.Page
{
    int iApproverCount = 0;

    public float iTotal;
    int iVendor;
    string sSQLUnion;

    bool IsNumber(string text)
    {
       Regex regex = new Regex(@"^[-+]?[0-9]*\.?[0-9]+$");
       return regex.IsMatch(text);
    }


    protected void Page_LoadComplete(object sender, EventArgs e)
    {
        if (IsPostBack)
        {
           


            if (Request.Form["status"].ToString() != "-1")
            {
                //work around for error during cancel/delete bac session
                SaveToDB_init();
                UpdateBACEvaluationSummary();
                UpdateCommercialDetails();
            }

            lblLoad.Value = "false";

            string control1 = Request.Form["__EVENTTARGET"];
            //Response.Write(control1);
            if (control1 == "") // save to database
            {
                CreateCriteriaRow();
                this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "SetCriteriaSave", "<script>SetCriteria();</script>");
                SaveToDB();
            }
            else if (control1 == "HistoryBack")
            {
                Response.Redirect("bacendorsed.aspx");
            }
            else if (control1 == "PrintPage")
            {
                UpdateDB();
                doBuyerBidForBac();
                UpdateCommercialDetails();
                count_attachements();
                CreateCriteriaRow();

            }
            else if (control1 == "SaveItem")
            {
                UpdateDB();
                doBuyerBidForBac();
                UpdateCommercialDetails();
                count_attachements();
                CreateCriteriaRow();
            }
            else if (control1 == "SaveVendors")
            {
                UpdateBACEvaluationSummary();

                Repeater4.DataBind();
                Repeater100.DataBind();
                RepeaterPRating.DataBind();
                Repeater5.DataBind();
                Repeater6.DataBind();
                Repeater7.DataBind();

                UpdateDB();
                doBuyerBidForBac();
                UpdateCommercialDetails();
                count_attachements();
                CreateCriteriaRow();
            }
            else if (control1 == "VendorChange")
            {
                string iArg = Request.Form["__EVENTARGUMENT"];
                Int32 iArg1 = Int32.Parse(iArg);
                string connstring = HttpContext.Current.Session["ConnectionString"].ToString();
                string sCommand;

                sCommand = "SELECT Accredited, ISNULL(Perf_Evaluation_Rate, 0) Perf_Evaluation_Rate FROM tblVendors WHERE VendorId=" + iArg1;
                //Response.Write(sCommand);
                SqlDataReader oReader;
                oReader = SqlHelper.ExecuteReader(connstring, CommandType.Text, sCommand);
                if (oReader.HasRows)
                {
                    oReader.Read();
                    int iAccred = int.Parse(oReader["Accredited"].ToString());
                    string iPerf = oReader["Perf_Evaluation_Rate"].ToString();
                    oReader.Close();
                    sCommand = "UPDATE tblBACEvaluationSummary SET VendorID=" + iArg1 + ", Accredited=" + iAccred + ", PerformanceRating= '" + iPerf + "' ";
                    sCommand = sCommand + "WHERE BidRefNo=" + Request.Form["BuyerBidForBac"] + " AND VendorID LIKE 'V%'";
                    SqlHelper.ExecuteNonQuery(connstring, CommandType.Text, sCommand);

                    sCommand = "UPDATE tblBACEvaluationSummary SET Accredited=" + iAccred + ", PerformanceRating= '" + iPerf + "' ";
                    sCommand = sCommand + "WHERE BidRefNo=" + Session["BuyerBidForBac"] + " AND VendorID ='" + iArg1 + "'";
                    SqlHelper.ExecuteNonQuery(connstring, CommandType.Text, sCommand);
                }
                oReader.Close();

                UpdateBACEvaluationSummary();

                Repeater4.DataBind();
                Repeater100.DataBind();
                RepeaterPRating.DataBind();
                Repeater5.DataBind();
                Repeater6.DataBind();
                Repeater7.DataBind();

                UpdateDB();
                doBuyerBidForBac();
                UpdateCommercialDetails();
                count_attachements();
                CreateCriteriaRow();
            }
            else if (control1 == "lnkAttach")
            {
                UpdateDB();
                doBuyerBidForBac();
                UpdateCommercialDetails();
                AttachMyFile();
                count_attachements();
                CreateCriteriaRow();
            }
            else if (control1 == "lnkAttach2")
            {
                UpdateDB();
                doBuyerBidForBac();
                UpdateCommercialDetails();
                AttachMyFile2();
                count_attachements();
                CreateCriteriaRow();
            }
            else if (control1 == "BuyerBidForBac")
            {
                Session["BuyerBidForBac"] = Request.Form["BuyerBidForBac"];
                this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "ClearCriteria", "<script>ClearCriteria('ALL');</script>");
                doBuyerBidForBac();
                UpdateCommercialDetails();
                lblLoad.Value = "true";
                count_attachements();
                CreateCriteriaRow();
            }
            else if (control1 == "CriteriaAddRow")
            {
                string iArg = Request.Form["__EVENTARGUMENT"];
                this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "ClearCriteria", "<script>ClearCriteria('" + iArg + "');</script>");
                CreateCriteriaRow();
            }

        }
        else
        {
            //lblAccCost.InnerText = "0.00";
            //lblNegDisc.InnerText = "0.00";
            //lblPercentSpend.InnerText = "0%";
            lblAccCost.Text = "0.00";
            lblNegDisc.Text = "0.00";
            lblPercentSpend.Text = "0.00";
            lblTotalAll.Value = "0.00";
            isNewBid(false);

            txtNewVendor.Value = "0";
            txtCDVendor.Value = "0";

            System.Web.UI.HtmlControls.HtmlInputHidden NewRow;
            NewRow = ((System.Web.UI.HtmlControls.HtmlInputHidden)Form.FindControl("txtNewRow"));
            NewRow.Value = "-1";

            ClearBACSessions();

            lblLoad.Value = "true";
            Session["BuyerBidForBac"] = "";
        }

        CreateCriteriaRow();

        // javascript selected indices
        cbPaymentTerms.Attributes.Add("onChange", "cbPaymentTerms_SelectedIndexChanged();");
        cbSourcingStrategy.Attributes.Add("onChange", "cbSourcingStrategy_SelectedIndexChanged();");
        cbTypeOfPurchase.Attributes.Add("onChange", "cbTypeOfPurchase_SelectedIndexChanged();");
        cbSupplyPosition.Attributes.Add("onChange", "cbSupplyPosition_SelectedIndexChanged();");
        //cbBasisForAwarding.Attributes.Add("onChange", "cbBasisForAwarding_SelectedIndexChanged();");
        cbNatureOfSavings.Attributes.Add("onChange", "cbNatureOfSavings_SelectedIndexChanged();");

        // show approver
        ShowApprover();

        BACCheckBoxAttr();
    }

    void SaveToDB_init()
    {
        string status1 = Request.Form["status"].ToString();
        string connstring = HttpContext.Current.Session["ConnectionString"].ToString();
        string sCommand;

        sCommand = "IF EXISTS (SELECT BidRefNo FROM tblBidItems WHERE BidRefNo = " + Request.Form["BuyerBidForBac"] + ") BEGIN ";
        sCommand = sCommand + "IF NOT EXISTS (SELECT 1 FROM tblBacBidItems WHERE BidRefNo = " + Request.Form["BuyerBidForBac"] + ") ";
        sCommand = sCommand + "BEGIN INSERT INTO tblBacBidItems ";
        sCommand = sCommand + "(BidRefNo, Budgeted, CompanyID, ItemDesc, BuyerID, SAPPRNo, BidAmount, SAPPRDate, RcvdDate, ";
        sCommand = sCommand + "Payment_Terms, SS_NoBidders, SS_NoBidsRcvd, SS_TechCompliance, Status, AccumulativeCost, SavingsGen_Amount, SavingsGen_PctSpend, descPaymentTerms) ";
        sCommand = sCommand + "SELECT t1.BidRefNo AS BidRefNo, 1 AS Budgeted, t1.CompanyId AS CompanyID, t1.ItemDesc AS ItemDesc, t1.BuyerId AS BuyerId, t1.PRRefNo AS SAPPRNo, t1.EstItemValue AS BidAmount, t1.PRDate AS SAPPRDate, t1.PRDate AS RcvdDate, null AS Payment_Terms, t3.SS_NoBidders, t2.SS_NoBidsRcvd, 0 AS SS_TechCompliance, 0 AS AccumulativeCost, 0 AS SavingsGen_Amount, 0 AS SavingsGen_PctSpend, 0 AS SavingsGen_PctSpend, null AS descPaymentTerms FROM tblBidItems t1, (select count(DISTINCT a3.VendorId) AS SS_NoBidsRcvd FROM tblBidItemDetails a2, tblBidTenders a3 WHERE a2.BidDetailNo = a3.BidDetailNo AND a2.BidRefNo = " + Request.Form["BuyerBidForBac"] + " AND a3.Status > 0) as t2, (select count(VendorInBidsId) as SS_NoBidders from tblVendorsInBids WHERE BidRefNo = " + Request.Form["BuyerBidForBac"] + ") as t3  WHERE t1.BuyerId = " + Session["UserId"] + "  AND t1.BidRefNo =" + Request.Form["BuyerBidForBac"] + " AND t1.Status > 0  AND t1.ForAuction = 0 ";
        sCommand = sCommand + "END ";
        sCommand = sCommand + "END ";
        sCommand = sCommand + "ELSE ";
        sCommand = sCommand + "IF NOT EXISTS (SELECT 1 FROM tblBacBidItems WHERE BidRefNo = " + Request.Form["BuyerBidForBac"] + ") ";
        sCommand = sCommand + "BEGIN INSERT INTO tblBacBidItems ";
        sCommand = sCommand + "(BidRefNo, Budgeted, CompanyID, ItemDesc, BuyerID, SAPPRNo, BidAmount, SAPPRDate, RcvdDate, ";
        sCommand = sCommand + "Payment_Terms, SS_NoBidders, SS_NoBidsRcvd, SS_TechCompliance, Status, AccumulativeCost, SavingsGen_Amount, SavingsGen_PctSpend, descPaymentTerms) ";
        sCommand = sCommand + "SELECT " + Request.Form["BuyerBidForBac"] + " AS BidRefNo, 1 AS Budgeted, 1 AS CompanyID, '--- New BID event ---' AS ItemDesc, " + Session["UserId"] + " AS BuyerId, null AS SAPPRNo, 0 AS BidAmount, getdate() AS SAPPRDate, getdate() AS RcvdDate, null AS Payment_Terms, 0 SS_NoBidders, 0 SS_NoBidsRcvd, 0 AS SS_TechCompliance, 0 AS AccumulativeCost, 0 AS SavingsGen_Amount, 0 AS SavingsGen_PctSpend, 0 AS SavingsGen_PctSpend, null AS descPaymentTerms ";
        sCommand = sCommand + "END ";
        //Response.Write(sCommand);
        SqlHelper.ExecuteNonQuery(connstring, CommandType.Text, sCommand);

        sCommand = "IF NOT EXISTS (SELECT 1 FROM tblBACEvaluationSummary WHERE BidRefNo = " + Request.Form["BuyerBidForBac"] + ") BEGIN ";
        sCommand = sCommand + "INSERT INTO tblBACEvaluationSummary (BidRefNo, VendorID, VendorName, TechCompliance, CommCompliance, ContCompliance, fromDB, Accredited, PerformanceRating) ";
        sCommand = sCommand + "SELECT DISTINCT t2.BidRefNo, CONVERT(VARCHAR, t3.VendorId) VendorId, t3.VendorName, ";
        sCommand = sCommand + "0 , 0 , 0 , 1 fromDB, t3.Accredited, ";
        sCommand = sCommand + "ISNULL(t3.Perf_Evaluation_Rate, 0) ";
        sCommand = sCommand + "FROM tblBidTenders t1 ";
        sCommand = sCommand + "INNER JOIN tblBidItemDetails t2 ON t2.BidDetailNo = t1.BidDetailNo ";
        sCommand = sCommand + "INNER JOIN tblVendors t3 ON t3.VendorId = t1.VendorId ";
        sCommand = sCommand + "INNER JOIN tblBidItems t4 ON t4.BidRefNo = t2.BidRefNo ";
        sCommand = sCommand + "WHERE t2.BidRefNo = " + Request.Form["BuyerBidForBac"] + " AND t1.Status > 0 END";
        //Response.Write(sCommand);
        SqlHelper.ExecuteNonQuery(connstring, CommandType.Text, sCommand);
        //doBuyerBidForBac();
    }

    void SaveToDB()
    {
        string status1 = Request.Form["status"].ToString();
        string connstring = HttpContext.Current.Session["ConnectionString"].ToString();
        string sCommand;

        // CANCEL, Delete all related bac
        if (status1 == "-1")
        {
            if (Session["BuyerBidForBac"] != "")
            {
                //del tblbacbiditems
                sCommand = "DELETE FROM tblBacBidItems WHERE BidRefNo=" + Session["BuyerBidForBac"] + " ";
                SqlHelper.ExecuteNonQuery(connstring, CommandType.Text, sCommand);
                sCommand = "DELETE FROM tblBACEvaluationSummary WHERE BidRefNo=" + Session["BuyerBidForBac"] + " ";
                SqlHelper.ExecuteNonQuery(connstring, CommandType.Text, sCommand);
                sCommand = "DELETE FROM tblBACEvaluationDetails WHERE BidRefNo=" + Session["BuyerBidForBac"] + " ";
                SqlHelper.ExecuteNonQuery(connstring, CommandType.Text, sCommand);
                sCommand = "DELETE FROM tblBACExecutiveSummary WHERE BidRefNo=" + Session["BuyerBidForBac"] + " ";
                SqlHelper.ExecuteNonQuery(connstring, CommandType.Text, sCommand);
                sCommand = "DELETE FROM tblBACNatureOfSavings WHERE BidRefNo=" + Session["BuyerBidForBac"] + " ";
                SqlHelper.ExecuteNonQuery(connstring, CommandType.Text, sCommand);
                sCommand = "DELETE FROM tblBACPaymentTerms WHERE BidRefNo=" + Session["BuyerBidForBac"] + " ";
                SqlHelper.ExecuteNonQuery(connstring, CommandType.Text, sCommand);
                sCommand = "DELETE FROM tblBACSourcingStrategy WHERE BidRefNo=" + Session["BuyerBidForBac"] + " ";
                SqlHelper.ExecuteNonQuery(connstring, CommandType.Text, sCommand);
                sCommand = "DELETE FROM tblBACSupplyPosition WHERE BidRefNo=" + Session["BuyerBidForBac"] + " ";
                SqlHelper.ExecuteNonQuery(connstring, CommandType.Text, sCommand);
                sCommand = "DELETE FROM tblBACSupportingDocuments WHERE BidRefNo=" + Session["BuyerBidForBac"] + " ";
                SqlHelper.ExecuteNonQuery(connstring, CommandType.Text, sCommand);
                sCommand = "DELETE FROM tblBACTypeOfPurchase WHERE BidRefNo=" + Session["BuyerBidForBac"] + " ";
                SqlHelper.ExecuteNonQuery(connstring, CommandType.Text, sCommand);

                sCommand = "DELETE FROM tblBACClarifications WHERE BidRefNo=" + Session["BuyerBidForBac"] + " ";
                SqlHelper.ExecuteNonQuery(connstring, CommandType.Text, sCommand);
                sCommand = "DELETE FROM tblBACComments WHERE BidRefNo=" + Session["BuyerBidForBac"] + " ";
                SqlHelper.ExecuteNonQuery(connstring, CommandType.Text, sCommand);
                sCommand = "DELETE FROM tblBACCriteria WHERE BidRefNo=" + Session["BuyerBidForBac"] + " ";
                SqlHelper.ExecuteNonQuery(connstring, CommandType.Text, sCommand);
            }

            Response.Redirect("bacendorsed.aspx");

        }

        // get buyer bid details
        string sItemDesc = "";
        string iSAP = "";
        string cAmt = "0";
        //string sRefNo = "";
        string dPRdate = DateTime.Now.Date.ToString();
        string dReceived = DateTime.Now.Date.ToString();
        string sPaymentTerm = "";

        // bid events description
        sItemDesc = ItemDescLbl.Text;
        // SAP PR number
        iSAP = PRRefNoLbl.Text;
        // bid amount
        cAmt = EstItemValueLbl.Text;
        cAmt = cAmt.Replace(",", "");

        // SAP PR date
        dPRdate = PRDate.Text.ToString() != "" ? DateTime.Parse(PRDate.Text.ToString().Trim()).ToShortDateString() : DateTime.Now.Date.ToString();
        // date received
        dReceived = DateApprovedLbl.Text.ToString() != "" ? DateTime.Parse(DateApprovedLbl.Text.ToString().Trim()).ToShortDateString() : DateTime.Now.Date.ToString();

        sPaymentTerm = ((DropDownList)Form.FindControl("cbPaymentTerms")).SelectedValue.ToString();
        
        // sourcing strategy
        sCommand = "DELETE FROM tblBACSourcingStrategy WHERE BidRefNo=" + Request.Form["BuyerBidForBac"] + " AND UserId=" + Session["UserId"];
        SqlHelper.ExecuteNonQuery(connstring, CommandType.Text, sCommand);
        foreach (string myCookie in Request.Cookies)
        {
            if (Request.Cookies[myCookie].Name.Contains("myss_"))
            {
                sCommand = "INSERT INTO tblBACSourcingStrategy (BidRefNo, UserId, SourcingStrategy) VALUES(" + Request.Form["BuyerBidForBac"] + ", " + Session["UserId"] + ", '" + Request.Cookies[myCookie].Value.ToString().Replace("'", "") + "')";
                SqlHelper.ExecuteNonQuery(connstring, CommandType.Text, sCommand);
            }
        }
        // type of purchase
        sCommand = "DELETE FROM tblBACTypeOfPurchase WHERE BidRefNo=" + Request.Form["BuyerBidForBac"] + " AND UserId=" + Session["UserId"];
        SqlHelper.ExecuteNonQuery(connstring, CommandType.Text, sCommand);
        foreach (string myCookie in Request.Cookies)
        {
            if (Request.Cookies[myCookie].Name.Contains("mytop_"))
            {
                sCommand = "INSERT INTO tblBACTypeOfPurchase (BidRefNo, UserId, TypeOfPurchase) VALUES(" + Request.Form["BuyerBidForBac"] + ", " + Session["UserId"] + ", '" + Request.Cookies[myCookie].Value.ToString().Replace("'", "") + "')";
                SqlHelper.ExecuteNonQuery(connstring, CommandType.Text, sCommand);
            }
        }
        // payment terms
        sCommand = "DELETE FROM tblBACPaymentTerms WHERE BidRefNo=" + Request.Form["BuyerBidForBac"] + " AND UserId=" + Session["UserId"];
        SqlHelper.ExecuteNonQuery(connstring, CommandType.Text, sCommand);
        foreach (string myCookie in Request.Cookies)
        {
            if (Request.Cookies[myCookie].Name.Contains("mypt_"))
            {
                sCommand = "INSERT INTO tblBACPaymentTerms (BidRefNo, UserId, PaymentTerm) VALUES(" + Request.Form["BuyerBidForBac"] + ", " + Session["UserId"] + ", '" + Request.Cookies[myCookie].Value.ToString().Replace("'", "") + "')";
                SqlHelper.ExecuteNonQuery(connstring, CommandType.Text, sCommand);
            }
        }
        // supply position
        sCommand = "DELETE FROM tblBACSupplyPosition WHERE BidRefNo=" + Request.Form["BuyerBidForBac"] + " AND UserId=" + Session["UserId"];
        SqlHelper.ExecuteNonQuery(connstring, CommandType.Text, sCommand);
        foreach (string myCookie in Request.Cookies)
        {
            if (Request.Cookies[myCookie].Name.Contains("mysp_"))
            {
                sCommand = "INSERT INTO tblBACSupplyPosition (BidRefNo, UserId, SupplyPosition) VALUES(" + Request.Form["BuyerBidForBac"] + ", " + Session["UserId"] + ", '" + Request.Cookies[myCookie].Value.ToString().Replace("'", "") + "')";
                SqlHelper.ExecuteNonQuery(connstring, CommandType.Text, sCommand);
            }
        }
        // basis for awarding
        sCommand = "DELETE FROM tblBACBasisForAwarding WHERE BidRefNo=" + Request.Form["BuyerBidForBac"] + " AND UserId=" + Session["UserId"];
        SqlHelper.ExecuteNonQuery(connstring, CommandType.Text, sCommand);
        foreach (string myCookie in Request.Cookies)
        {
            if (Request.Cookies[myCookie].Name.Contains("mybfa_"))
            {
                sCommand = "INSERT INTO tblBACBasisForAwarding (BidRefNo, UserId, BasisForAwarding) VALUES(" + Request.Form["BuyerBidForBac"] + ", " + Session["UserId"] + ", '" + Request.Cookies[myCookie].Value.ToString().Replace("'", "") + "')";
                SqlHelper.ExecuteNonQuery(connstring, CommandType.Text, sCommand);
            }
        }
        // nature of savings
        sCommand = "DELETE FROM tblBACNatureOfSavings WHERE BidRefNo=" + Request.Form["BuyerBidForBac"] + " AND UserId=" + Session["UserId"];
        SqlHelper.ExecuteNonQuery(connstring, CommandType.Text, sCommand);
        foreach (string myCookie in Request.Cookies)
        {
            if (Request.Cookies[myCookie].Name.Contains("mynos_"))
            {
                sCommand = "INSERT INTO tblBACNatureOfSavings (BidRefNo, UserId, NatureOfSavings) VALUES(" + Request.Form["BuyerBidForBac"] + ", " + Session["UserId"] + ", '" + Request.Cookies[myCookie].Value.ToString().Replace("'", "") + "')";
                SqlHelper.ExecuteNonQuery(connstring, CommandType.Text, sCommand);
            }
        }
        string ss_nobidders = "0";
        if (IsNumber(Request.Form["ss_nobidders"].ToString()) == true) ss_nobidders = Request.Form["ss_nobidders"];
        string ss_nobidsrcvd = "0";
        if (IsNumber(Request.Form["ss_nobidsrcvd"]) == true) ss_nobidsrcvd = Request.Form["ss_nobidsrcvd"];
        string ss_techcompliance = "0";
        if (IsNumber(Request.Form["ss_techcompliance"]) == true) ss_techcompliance = Request.Form["ss_techcompliance"];
        string lblNegDisc = "0";
        if (IsNumber(Request.Form["lblNegDisc"].Replace(",", "").Replace("%", "")) == true) lblNegDisc = Request.Form["lblNegDisc"].Replace(",", "").Replace("%", "");
        string lblPercentSpend = "0";
        if (IsNumber(Request.Form["lblPercentSpend"].Replace(",", "").Replace("%", "")) == true) lblPercentSpend = Request.Form["lblPercentSpend"].Replace(",", "").Replace("%", "");

        // add row to tblBacBidItems table
        //sCommand = "DELETE tblBacBidItems WHERE BidRefNo=" + Request.Form["BuyerBidForBac"] + " AND BuyerID=" + Session["UserId"] + " AND (Status=0 OR Status=1)";
        //SqlHelper.ExecuteNonQuery(connstring, CommandType.Text, sCommand);

        sCommand = "UPDATE tblBacBidItems SET ";
        sCommand = sCommand + "BidRefNo = " + Request.Form["BuyerBidForBac"] + ", Budgeted=" + Request.Form["Budgeted"] + ", CompanyID=" + Request.Form["CompanyId"] + ", ItemDesc='" + sItemDesc.Replace("'", "''") + "', BuyerID=" + Session["UserId"] + ", SAPPRNo='" + iSAP + "', BidAmount=" + cAmt + ", SAPPRDate='" + dPRdate + "', RcvdDate= '" + dReceived + "', ";
        sCommand = sCommand + " SS_NoBidders=" + ss_nobidders + ", SS_NoBidsRcvd=" + ss_nobidsrcvd + ", SS_TechCompliance=" + ss_techcompliance + ", Status=" + status1 + ",  SavingsGen_Amount=" + lblNegDisc + ", SavingsGen_PctSpend=" + lblPercentSpend + ", descPaymentTerms='" + Request.Form["descPaymentTerms"].Replace("'", "''") + "', ss_crc_no='" + Request.Form["ss_crcnumber"].Replace("'", "''") + "'  WHERE BidRefNo =" + Request.Form["BuyerBidForBac"];
        //LogHelper.TextLogHelper.Log(Server.MapPath("ebid.log"), sCommand);
        SqlHelper.ExecuteNonQuery(connstring, CommandType.Text, sCommand);

        // add row to tblBACExecutiveSummary table
        sCommand = "DELETE tblBACExecutiveSummary WHERE BidRefNo=" + Request.Form["BuyerBidForBac"] + " AND UserID=" + Session["UserId"];
        SqlHelper.ExecuteNonQuery(connstring, CommandType.Text, sCommand);

        sCommand = "INSERT INTO tblBACExecutiveSummary (BidRefNo, UserID, Comment) VALUES (";
        sCommand = sCommand + Request.Form["BuyerBidForBac"] + ", " + Session["UserId"] + ", '" + Request.Form["executive_summary"].Replace("\n", "<br />").Replace("'", "''") + "')";
        SqlHelper.ExecuteNonQuery(connstring, CommandType.Text, sCommand);

        // add row to tblBACCriteria table
        sCommand = "DELETE tblBACCriteria WHERE BidRefNo=" + Request.Form["BuyerBidForBac"];
        SqlHelper.ExecuteNonQuery(connstring, CommandType.Text, sCommand);

        string[] kuki;
        char[] separator = new char[] {'_'};

        for (int i = 0; i < 100; i++)
        {
            foreach (string myCookie in Request.Cookies)
            {
                if (Request.Cookies[myCookie].Name.Contains("Criteria_" + i.ToString() + "_"))
                {
                    kuki = myCookie.Split(separator);
                    if (kuki[2] == "")
                    {
                        sCommand = "INSERT INTO tblBACCriteria (BidRefNo, RowNum, VendorID, CriteriaText) VALUES (" + Request.Form["BuyerBidForBac"] + ", ";
                        sCommand = sCommand + i + ", '0', '" + Request.Cookies[myCookie].Value.Replace("'", "") + "')";
                    }
                    else
                    {
                        sCommand = "INSERT INTO tblBACCriteria (BidRefNo, RowNum, VendorID, CriteriaText) VALUES (" + Request.Form["BuyerBidForBac"] + ", ";
                        sCommand = sCommand + i + ", '" + kuki[2] + "', '" + Request.Cookies[myCookie].Value.Replace("'", "") + "')";
                    }
                    SqlHelper.ExecuteNonQuery(connstring, CommandType.Text, sCommand);
                }
            }
        }

        this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "ClearCriteria", "<script>ClearCriteria('ALL');</script>");

        // add row to tblBACComments table
        sCommand = "DELETE tblBACComments WHERE BidRefNo=" + Request.Form["BuyerBidForBac"] + " AND UserID=" + Session["UserId"];
        SqlHelper.ExecuteNonQuery(connstring, CommandType.Text, sCommand);

        sCommand = "INSERT INTO tblBACComments (BidRefNo, UserID, Comment) VALUES (";
        sCommand = sCommand + Request.Form["BuyerBidForBac"] + ", " + Session["UserId"] + ", '" + Request.Form["remarks_comments"].Replace("\n", "<br />").Replace("'", "''") + "')";
        SqlHelper.ExecuteNonQuery(connstring, CommandType.Text, sCommand);

        // bid evaluation summary


        // commercial details
        UpdateCommercialDetails();


        //approvers
        //purchasing
        sCommand = "UPDATE tblBacBidItems SET Approver_0=" + approver4.Value.ToString() + " WHERE BidRefNo=" + Request.Form["BuyerBidForBac"] + " AND BuyerId=" + Session["UserId"];
        //LogHelper.TextLogHelper.Log(Server.MapPath("ebid.log"), sCommand);
        SqlHelper.ExecuteNonQuery(connstring, CommandType.Text, sCommand);

        //bid awarding committee
        Int32 chkApproverLevelCount = 0;
        foreach (RepeaterItem rpItem in RepeaterApprover.Items)
        {
            HtmlInputCheckBox chkbx = rpItem.FindControl("chkApproverLevel") as HtmlInputCheckBox;
            sCommand = "UPDATE tblBacBidItems SET Approver_" + chkApproverLevelCount.ToString() + "=null WHERE BidRefNo=" + Request.Form["BuyerBidForBac"] + " AND BuyerId=" + Session["UserId"];
            //LogHelper.TextLogHelper.Log(Server.MapPath("ebid.log"), sCommand);
            //SqlHelper.ExecuteNonQuery(connstring, CommandType.Text, sCommand);
            if (chkbx.Checked == true)
            {
                chkApproverLevelCount++;
                sCommand = "UPDATE tblBacBidItems SET Approver_" + chkApproverLevelCount.ToString() + "=" + Convert.ToInt32(chkbx.Attributes["value"].ToString()) + " WHERE BidRefNo=" + Request.Form["BuyerBidForBac"] + " AND BuyerId=" + Session["UserId"];
                SqlHelper.ExecuteNonQuery(connstring, CommandType.Text, sCommand);
                //Response.Write(sCommand);
            }
        }

        this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "ClearSavedCookies", "<script>ClearSavedCookies();</script>");

        // redirect
        if (status1 == "0")
        {
            sCommand = "UPDATE tblBacBidItems SET DateSavedAsDraft=getdate() WHERE BidRefNo=" + Request.Form["BuyerBidForBac"];
            SqlHelper.ExecuteNonQuery(connstring, CommandType.Text, sCommand);
            Response.Redirect("bacdrafts.aspx");
        }
        else if (status1 == "1")
        {
            sCommand = "UPDATE tblBacBidItems SET status = 1, DateSubmitted=getdate() WHERE BidRefNo=" + Request.Form["BuyerBidForBac"];
            SqlHelper.ExecuteNonQuery(connstring, CommandType.Text, sCommand);


            if (SendEmailNotification())
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
    }

    void UpdateDB()
    {
        
        //string status1 = Request.Form["status"].ToString();
        string status1 = "0";
        string connstring = HttpContext.Current.Session["ConnectionString"].ToString();
        string sCommand;

        // get buyer bid details
        string sItemDesc = "";
        string iSAP = "";
        string cAmt = "0";
        //string sRefNo = "";
        string dPRdate = DateTime.Now.Date.ToString();
        string dReceived = DateTime.Now.Date.ToString();
        // SAP PR date
        dPRdate = PRDate.Text.ToString() != "" ? DateTime.Parse(PRDate.Text.ToString().Trim()).ToShortDateString() : DateTime.Now.Date.ToString();
        // date received
        dReceived = DateApprovedLbl.Text.ToString() != "" ? DateTime.Parse(DateApprovedLbl.Text.ToString().Trim()).ToShortDateString() : DateTime.Now.Date.ToString();

        // bid events description
        sItemDesc = ItemDescLbl.Text;
        // SAP PR number
        iSAP = PRRefNoLbl.Text;
        // bid amount
        cAmt = EstItemValueLbl.Text;
        cAmt = cAmt.Replace(",", "");

        string ss_nobidders = "0";
        if (IsNumber(Request.Form["ss_nobidders"].ToString()) == true) ss_nobidders = Request.Form["ss_nobidders"];
        string ss_nobidsrcvd = "0";
        if (IsNumber(Request.Form["ss_nobidsrcvd"]) == true) ss_nobidsrcvd = Request.Form["ss_nobidsrcvd"];
        string ss_techcompliance = "0";
        if (IsNumber(Request.Form["ss_techcompliance"]) == true) ss_techcompliance = Request.Form["ss_techcompliance"];
        string lblNegDisc = "0";
        if (IsNumber(Request.Form["lblNegDisc"].Replace(",", "").Replace("%", "")) == true) lblNegDisc = Request.Form["lblNegDisc"].Replace(",", "").Replace("%", "");
        string lblPercentSpend = "0";
        if (IsNumber(Request.Form["lblPercentSpend"].Replace(",", "").Replace("%", "")) == true) lblPercentSpend = Request.Form["lblPercentSpend"].Replace(",", "").Replace("%", "");

       if (Request.Form["BuyerBidForBac"] !=  "-1") {
            sCommand = "UPDATE tblBacBidItems SET ";
            sCommand = sCommand + "BidRefNo = " + Request.Form["BuyerBidForBac"] + ", Budgeted=" + Request.Form["Budgeted"] + ", CompanyID=" + Request.Form["CompanyId"] + ", ItemDesc='" + sItemDesc.Replace("'", "''") + "', BuyerID=" + Session["UserId"] + ", SAPPRNo='" + iSAP + "', BidAmount=" + cAmt + ", SAPPRDate='" + dPRdate + "', RcvdDate= '" + DateTime.Parse(Request.Form["DateApprovedLbl"].ToString().Trim()).ToShortDateString() + "', ";
            sCommand = sCommand + " SS_NoBidders=" + ss_nobidders;
            sCommand = sCommand + ", SS_NoBidsRcvd=" + ss_nobidsrcvd;
            sCommand = sCommand + ", SS_TechCompliance=" + ss_techcompliance;
            sCommand = sCommand + ", Status=" + status1;
            sCommand = sCommand + ", SavingsGen_Amount=" + lblNegDisc;
            sCommand = sCommand + ", SavingsGen_PctSpend=" + lblPercentSpend;
            sCommand = sCommand + ", descPaymentTerms='" + Request.Form["descPaymentTerms"].Replace("'", "''");
            sCommand = sCommand + "', ss_crc_no='" + Request.Form["ss_crcnumber"].Replace("'", "''");
            sCommand = sCommand + "'  WHERE BidRefNo =" + Request.Form["BuyerBidForBac"];
            //Response.Write(sCommand);
            SqlHelper.ExecuteNonQuery(connstring, CommandType.Text, sCommand);


            // sourcing strategy
            sCommand = "DELETE FROM tblBACSourcingStrategy WHERE BidRefNo=" + Request.Form["BuyerBidForBac"] + " AND UserId=" + Session["UserId"];
            SqlHelper.ExecuteNonQuery(connstring, CommandType.Text, sCommand);
            foreach (string myCookie in Request.Cookies)
            {
                if (Request.Cookies[myCookie].Name.Contains("myss_"))
                {
                    sCommand = "INSERT INTO tblBACSourcingStrategy (BidRefNo, UserId, SourcingStrategy) VALUES(" + Request.Form["BuyerBidForBac"] + ", " + Session["UserId"] + ", '" + Request.Cookies[myCookie].Value.ToString().Replace("'", "") + "')";
                    SqlHelper.ExecuteNonQuery(connstring, CommandType.Text, sCommand);
                }
            }
            // type of purchase
            sCommand = "DELETE FROM tblBACTypeOfPurchase WHERE BidRefNo=" + Request.Form["BuyerBidForBac"] + " AND UserId=" + Session["UserId"];
            SqlHelper.ExecuteNonQuery(connstring, CommandType.Text, sCommand);
            foreach (string myCookie in Request.Cookies)
            {
                if (Request.Cookies[myCookie].Name.Contains("mytop_"))
                {
                    sCommand = "INSERT INTO tblBACTypeOfPurchase (BidRefNo, UserId, TypeOfPurchase) VALUES(" + Request.Form["BuyerBidForBac"] + ", " + Session["UserId"] + ", '" + Request.Cookies[myCookie].Value.ToString().Replace("'", "") + "')";
                    SqlHelper.ExecuteNonQuery(connstring, CommandType.Text, sCommand);
                }
            }
            // payment terms
            sCommand = "DELETE FROM tblBACPaymentTerms WHERE BidRefNo=" + Request.Form["BuyerBidForBac"] + " AND UserId=" + Session["UserId"];
            SqlHelper.ExecuteNonQuery(connstring, CommandType.Text, sCommand);
            foreach (string myCookie in Request.Cookies)
            {
                if (Request.Cookies[myCookie].Name.Contains("mypt_"))
                {
                    sCommand = "INSERT INTO tblBACPaymentTerms (BidRefNo, UserId, PaymentTerm) VALUES(" + Request.Form["BuyerBidForBac"] + ", " + Session["UserId"] + ", '" + Request.Cookies[myCookie].Value.ToString().Replace("'", "") + "')";
                    SqlHelper.ExecuteNonQuery(connstring, CommandType.Text, sCommand);
                }
            }
            // supply position
            sCommand = "DELETE FROM tblBACSupplyPosition WHERE BidRefNo=" + Request.Form["BuyerBidForBac"] + " AND UserId=" + Session["UserId"];
            SqlHelper.ExecuteNonQuery(connstring, CommandType.Text, sCommand);
            foreach (string myCookie in Request.Cookies)
            {
                if (Request.Cookies[myCookie].Name.Contains("mysp_"))
                {
                    sCommand = "INSERT INTO tblBACSupplyPosition (BidRefNo, UserId, SupplyPosition) VALUES(" + Request.Form["BuyerBidForBac"] + ", " + Session["UserId"] + ", '" + Request.Cookies[myCookie].Value.ToString().Replace("'", "") + "')";
                    SqlHelper.ExecuteNonQuery(connstring, CommandType.Text, sCommand);
                }
            }
            // basis for awarding
            sCommand = "DELETE FROM tblBACBasisForAwarding WHERE BidRefNo=" + Request.Form["BuyerBidForBac"] + " AND UserId=" + Session["UserId"];
            SqlHelper.ExecuteNonQuery(connstring, CommandType.Text, sCommand);
            foreach (string myCookie in Request.Cookies)
            {
                if (Request.Cookies[myCookie].Name.Contains("mybfa_"))
                {
                    sCommand = "INSERT INTO tblBACBasisForAwarding (BidRefNo, UserId, BasisForAwarding) VALUES(" + Request.Form["BuyerBidForBac"] + ", " + Session["UserId"] + ", '" + Request.Cookies[myCookie].Value.ToString().Replace("'", "") + "')";
                    SqlHelper.ExecuteNonQuery(connstring, CommandType.Text, sCommand);
                }
            }
            // nature of savings
            sCommand = "DELETE FROM tblBACNatureOfSavings WHERE BidRefNo=" + Request.Form["BuyerBidForBac"] + " AND UserId=" + Session["UserId"];
            SqlHelper.ExecuteNonQuery(connstring, CommandType.Text, sCommand);
            foreach (string myCookie in Request.Cookies)
            {
                if (Request.Cookies[myCookie].Name.Contains("mynos_"))
                {
                    sCommand = "INSERT INTO tblBACNatureOfSavings (BidRefNo, UserId, NatureOfSavings) VALUES(" + Request.Form["BuyerBidForBac"] + ", " + Session["UserId"] + ", '" + Request.Cookies[myCookie].Value.ToString().Replace("'", "") + "')";
                    SqlHelper.ExecuteNonQuery(connstring, CommandType.Text, sCommand);
                }
            }
       }
    }

    void AttachMyFile()
    {
        string connstring = HttpContext.Current.Session["ConnectionString"].ToString();
        string sCommand, sCommand1;
        SqlDataReader oReader;

        // upload file
        if (fileUpload.HasFile)
        {
            string myFile = "";
            FileInfo fInfo = new FileInfo(fileUpload.PostedFile.FileName);
            string sContentType = fileUpload.PostedFile.ContentType;
            if (!FileUploadHelper.IsFileForbidden(fInfo.Extension))
            {
                int fileSize = (fileUpload.PostedFile.ContentLength + 512) / 1024;
                int maxFileSize = int.Parse(ConfigurationManager.AppSettings["MaxFileSize"].ToString());
                FileInfo delFile;
                DirectoryInfo oDir;

                if (fileSize < maxFileSize)
                {
                    string original = fileUpload.FileName.ToString().Trim().Replace("'", "''");
                    string actual = FileUploadHelper.GetNewAlternativeFileName(Session[Constant.SESSION_USERID].ToString(), fInfo.Extension);
                    try
                    {
                        sCommand = "SELECT * FROM tblBACSupportingDocuments WHERE BidRefNo=" + Request.Form["BuyerBidForBac"] + " AND BuyerID=" + Session["UserId"] + " AND DocuName='" + Request.Form["SupportDocuAttached"] + "' AND OriginalFileName='" + original + "'";
                        oReader = SqlHelper.ExecuteReader(connstring, CommandType.Text, sCommand);
                        if (oReader.HasRows)
                        {
                            oReader.Read();
                            sCommand = (string)oReader["ActualFileName"];
                            delFile = new FileInfo(Constant.FILEATTACHMENTSFOLDERDIR + Session["UserId"] + "\\BAC\\" + sCommand + ".gz");
                            if (delFile.Exists)
                            {
                                File.Delete(Constant.FILEATTACHMENTSFOLDERDIR + Session["UserId"] + "\\BAC\\" + sCommand + ".gz");
                            }
                        }

                        oDir = new DirectoryInfo(Constant.FILEATTACHMENTSFOLDERDIR + Session["UserId"]);
                        if (oDir.Exists == false) oDir.Create();
                        oDir = new DirectoryInfo(Constant.FILEATTACHMENTSFOLDERDIR + Session["UserId"] + "\\BAC");
                        if (oDir.Exists == false) oDir.Create();
                        fileUpload.SaveAs((Constant.FILEATTACHMENTSFOLDERDIR) + Session["UserId"] + "\\BAC\\" + actual);
                        delFile = new FileInfo((Constant.FILEATTACHMENTSFOLDERDIR) + Session["UserId"] + "\\BAC\\" + actual);

                        CompressFile(delFile);
                        delFile.Delete();

                        // update table
                        sCommand = "DELETE FROM tblBACSupportingDocuments WHERE BidRefNo=" + Request.Form["BuyerBidForBac"] + " ";
                        sCommand = sCommand + "AND BuyerID=" + Session["UserId"] + " AND OriginalFileName='" + original + "' AND DocuName='" + Request.Form["SupportDocuAttached"] + "'";

                        sCommand1 = "INSERT INTO tblBACSupportingDocuments (BidRefNo, BuyerID, DocuName, OriginalFileName, ActualFileName, ContentType, DateUploaded, IsDetachable, Filesize) VALUES (";
                        sCommand1 = sCommand1 + Request.Form["BuyerBidForBac"] + ", " + Session["UserId"] + ", '" + Request.Form["SupportDocuAttached"] + "', '";
                        sCommand1 = sCommand1 + original + "', '" + actual + "', '" + sContentType + "', GETDATE(), 1, " + fileSize + ")";
                        //Response.Write(sCommand1);

                        switch(Request.Form["SupportDocuAttached"]) {
                            case "Approved_PR":
                                dsSDA_APR.DeleteCommand = sCommand;
                                dsSDA_APR.Delete();
                                dsSDA_APR.UpdateCommand = sCommand1;
                                dsSDA_APR.Update();
                                break;
                            case "Commercial_Evaluation":
                                dsSDA_CE.DeleteCommand = sCommand;
                                dsSDA_CE.Delete();
                                dsSDA_CE.UpdateCommand = sCommand1;
                                dsSDA_CE.Update();
                                break;
                            case "Approved_Business_Case":
                                dsSDA_ABC.DeleteCommand = sCommand;
                                dsSDA_ABC.Delete();
                                dsSDA_ABC.UpdateCommand = sCommand1;
                                dsSDA_ABC.Update();
                                break;
                            case "Negotiation_Results":
                                dsSDA_NR.DeleteCommand = sCommand;
                                dsSDA_NR.Delete();
                                dsSDA_NR.UpdateCommand = sCommand1;
                                dsSDA_NR.Update();
                                break;
                            case "Board_Resolution":
                                dsSDA_BR.DeleteCommand = sCommand;
                                dsSDA_BR.Delete();
                                dsSDA_BR.UpdateCommand = sCommand1;
                                dsSDA_BR.Update();
                                break;
                            case "Others":
                                dsSDA_Oth.DeleteCommand = sCommand;
                                dsSDA_Oth.Delete();
                                dsSDA_Oth.UpdateCommand = sCommand1;
                                dsSDA_Oth.Update();
                                break;
                            case "Technical_Evaluation":
                                dsSDA_TE.DeleteCommand = sCommand;
                                dsSDA_TE.Delete();
                                dsSDA_TE.UpdateCommand = sCommand1;
                                dsSDA_TE.Update();
                                break;
                        }
                        addAttachmentMsg.Text = myFile;
                    }
                    catch(Exception ex)
                    {
                        addAttachmentMsg.Text = "• File cannot be uploaded.<br />" + myFile + "<br />" + ex.ToString();
                    }
                }
                else
                {
                    addAttachmentMsg.Text = "• File size exceeds limit(" + maxFileSize + "KB).";
                }
            }
            else
            {
                addAttachmentMsg.Text = "• File cannot be uploaded. The file type is forbidden to be uploaded.";
            }
        }
        else 
        { 
            addAttachmentMsg.Text = "• File cannot be uploaded."; 
        }
    }


    void AttachMyFile2()
    {
        string connstring = HttpContext.Current.Session["ConnectionString"].ToString();
        string sCommand, sCommand1;
        SqlDataReader oReader;

        // upload file
        if (fileUpload2.HasFile)
        {
            string myFile = "";
            FileInfo fInfo = new FileInfo(fileUpload2.PostedFile.FileName);
            string sContentType = fileUpload2.PostedFile.ContentType;
            if (!FileUploadHelper.IsFileForbidden(fInfo.Extension))
            {
                int fileSize = (fileUpload2.PostedFile.ContentLength + 512) / 1024;
                int maxFileSize = int.Parse(ConfigurationManager.AppSettings["MaxFileSize"].ToString());
                FileInfo delFile;
                DirectoryInfo oDir;

                if (fileSize < maxFileSize)
                {
                    string original = fileUpload2.FileName.ToString().Trim().Replace("'", "''");
                    string actual = FileUploadHelper.GetNewAlternativeFileName(Session[Constant.SESSION_USERID].ToString(), fInfo.Extension);
                    try
                    {
                        sCommand = "SELECT * FROM tblBACSupportingDocuments WHERE BidRefNo=" + Request.Form["BuyerBidForBac"] + " AND BuyerID=" + Session["UserId"] + " AND DocuName='" + Request.Form["BasisForAwardingAttchd"] + "' AND OriginalFileName='" + original + "'";
                        oReader = SqlHelper.ExecuteReader(connstring, CommandType.Text, sCommand);
                        if (oReader.HasRows)
                        {
                            oReader.Read();
                            sCommand = (string)oReader["ActualFileName"];
                            delFile = new FileInfo(Constant.FILEATTACHMENTSFOLDERDIR + Session["UserId"] + "\\BAC\\" + sCommand + ".gz");
                            if (delFile.Exists)
                            {
                                File.Delete(Constant.FILEATTACHMENTSFOLDERDIR + Session["UserId"] + "\\BAC\\" + sCommand + ".gz");
                            }
                        }

                        oDir = new DirectoryInfo(Constant.FILEATTACHMENTSFOLDERDIR + Session["UserId"]);
                        if (oDir.Exists == false) oDir.Create();
                        oDir = new DirectoryInfo(Constant.FILEATTACHMENTSFOLDERDIR + Session["UserId"] + "\\BAC");
                        if (oDir.Exists == false) oDir.Create();
                        fileUpload2.SaveAs((Constant.FILEATTACHMENTSFOLDERDIR) + Session["UserId"] + "\\BAC\\" + actual);
                        delFile = new FileInfo((Constant.FILEATTACHMENTSFOLDERDIR) + Session["UserId"] + "\\BAC\\" + actual);
                        CompressFile(delFile);
                        delFile.Delete();

                        // update table
                        sCommand = "DELETE FROM tblBACSupportingDocuments WHERE BidRefNo=" + Request.Form["BuyerBidForBac"] + " ";
                        sCommand = sCommand + "AND BuyerID=" + Session["UserId"] + " AND OriginalFileName='" + original + "' AND DocuName='" + Request.Form["BasisForAwardingAttchd"] + "'";

                        sCommand1 = "INSERT INTO tblBACSupportingDocuments (BidRefNo, BuyerID, DocuName, OriginalFileName, ActualFileName, ContentType, DateUploaded, IsDetachable, Filesize) VALUES (";
                        sCommand1 = sCommand1 + Request.Form["BuyerBidForBac"] + ", " + Session["UserId"] + ", '" + Request.Form["BasisForAwardingAttchd"] + "', '";
                        sCommand1 = sCommand1 + original + "', '" + actual + "', '" + sContentType + "', GETDATE(), 1, " + fileSize + ")";
                        //Response.Write(sCommand1);

                        switch (Request.Form["BasisForAwardingAttchd"])
                        {
                            case "bfa_LowestPr":
                                dsBFA_LowestPr.DeleteCommand = sCommand;
                                dsBFA_LowestPr.Delete();
                                dsBFA_LowestPr.UpdateCommand = sCommand1;
                                dsBFA_LowestPr.Update();
                                break;
                            case "bfa_SoleLOA":
                                dsBFA_SoleLOA.DeleteCommand = sCommand;
                                dsBFA_SoleLOA.Delete();
                                dsBFA_SoleLOA.UpdateCommand = sCommand1;
                                dsBFA_SoleLOA.Update();
                                break;
                            case "bfa_SoleCSD":
                                dsBFA_SoleCSD.DeleteCommand = sCommand;
                                dsBFA_SoleCSD.Delete();
                                dsBFA_SoleCSD.UpdateCommand = sCommand1;
                                dsBFA_SoleCSD.Update();
                                break;
                            case "bfa_TechSpec":
                                dsBFA_TechSpec.DeleteCommand = sCommand;
                                dsBFA_TechSpec.Delete();
                                dsBFA_TechSpec.UpdateCommand = sCommand1;
                                dsBFA_TechSpec.Update();
                                break;
                            case "bfa_LeadTime":
                                dsBFA_LeadTime.DeleteCommand = sCommand;
                                dsBFA_LeadTime.Delete();
                                dsBFA_LeadTime.UpdateCommand = sCommand1;
                                dsBFA_LeadTime.Update();
                                break;
                            case "bfa_BFAOth":
                                dsBFAOth.DeleteCommand = sCommand;
                                dsBFAOth.Delete();
                                dsBFAOth.UpdateCommand = sCommand1;
                                dsBFAOth.Update();
                                break;
                        }
                        addAttachmentMsg2.Text = myFile;
                    }
                    catch (Exception ex)
                    {
                        addAttachmentMsg2.Text = "• File cannot be uploaded.<br />" + myFile + "<br />" + ex.ToString();
                    }
                }
                else
                {
                    addAttachmentMsg2.Text = "• File size exceeds limit(" + maxFileSize + "KB).";
                }
            }
            else
            {
                addAttachmentMsg2.Text = "• File cannot be uploaded. The file type is forbidden to be uploaded.";
            }
        }
        else
        {
            addAttachmentMsg2.Text = "• File cannot be uploaded.";
        }
    }

    protected void count_attachements()
    {
        string sCommand;
        string connstring = HttpContext.Current.Session["ConnectionString"].ToString();
        SqlDataReader oReader;

        //SUPPORTING DOCUMENTS ATTACHED COUNTS
        //Approved_PR
        sCommand = "SELECT Count(*) as Count FROM tblBACSupportingDocuments WHERE BuyerID=" + Session["UserId"] + " AND BidRefNo=" + Request.Form["BuyerBidForBac"] + " AND DocuName='Approved_PR'";
        oReader = SqlHelper.ExecuteReader(connstring, CommandType.Text, sCommand);
        if (oReader.HasRows)
        {
            oReader.Read();
            SDA_APR_count.Text = oReader["Count"].ToString();
        } oReader.Close();

        //Commercial_Evaluation
        sCommand = "SELECT Count(*) as Count FROM tblBACSupportingDocuments WHERE BuyerID=" + Session["UserId"] + " AND BidRefNo=" + Request.Form["BuyerBidForBac"] + " AND DocuName='Commercial_Evaluation'";
        oReader = SqlHelper.ExecuteReader(connstring, CommandType.Text, sCommand);
        if (oReader.HasRows)
        {
            oReader.Read();
            SDA_CE_count.Text = oReader["Count"].ToString();
        } oReader.Close();

        //Approved_Business_Case
        sCommand = "SELECT Count(*) as Count FROM tblBACSupportingDocuments WHERE BuyerID=" + Session["UserId"] + " AND BidRefNo=" + Request.Form["BuyerBidForBac"] + " AND DocuName='Approved_Business_Case'";
        oReader = SqlHelper.ExecuteReader(connstring, CommandType.Text, sCommand);
        if (oReader.HasRows)
        {
            oReader.Read();
            SDA_ABC_count.Text = oReader["Count"].ToString();
        } oReader.Close();

        //Board_Resolution
        sCommand = "SELECT Count(*) as Count FROM tblBACSupportingDocuments WHERE BuyerID=" + Session["UserId"] + " AND BidRefNo=" + Request.Form["BuyerBidForBac"] + " AND DocuName='Board_Resolution'";
        oReader = SqlHelper.ExecuteReader(connstring, CommandType.Text, sCommand);
        if (oReader.HasRows)
        {
            oReader.Read();
            SDA_BR_count.Text = oReader["Count"].ToString();
        } oReader.Close();

        //Technical_Evaluation
        sCommand = "SELECT Count(*) as Count FROM tblBACSupportingDocuments WHERE BuyerID=" + Session["UserId"] + " AND BidRefNo=" + Request.Form["BuyerBidForBac"] + " AND DocuName='Technical_Evaluation'";
        oReader = SqlHelper.ExecuteReader(connstring, CommandType.Text, sCommand);
        if (oReader.HasRows)
        {
            oReader.Read();
            SDA_TE_count.Text = oReader["Count"].ToString();
        } oReader.Close();

        //Negotiation_Results
        sCommand = "SELECT Count(*) as Count FROM tblBACSupportingDocuments WHERE BuyerID=" + Session["UserId"] + " AND BidRefNo=" + Request.Form["BuyerBidForBac"] + " AND DocuName='Negotiation_Results'";
        oReader = SqlHelper.ExecuteReader(connstring, CommandType.Text, sCommand);
        if (oReader.HasRows)
        {
            oReader.Read();
            SDA_NR_count.Text = oReader["Count"].ToString();
        } oReader.Close();

        //Others
        sCommand = "SELECT Count(*) as Count FROM tblBACSupportingDocuments WHERE BuyerID=" + Session["UserId"] + " AND BidRefNo=" + Request.Form["BuyerBidForBac"] + " AND DocuName='Others'";
        oReader = SqlHelper.ExecuteReader(connstring, CommandType.Text, sCommand);
        if (oReader.HasRows)
        {
            oReader.Read();
            SDA_Oth_count.Text = oReader["Count"].ToString();
        } oReader.Close();

        //BASIS FOR AWARDING
        //bfa_LowestPr
        sCommand = "SELECT Count(*) as Count FROM tblBACSupportingDocuments WHERE BuyerID=" + Session["UserId"] + " AND BidRefNo=" + Request.Form["BuyerBidForBac"] + " AND DocuName='bfa_LowestPr'";
        oReader = SqlHelper.ExecuteReader(connstring, CommandType.Text, sCommand);
        if (oReader.HasRows)
        {
            oReader.Read();
            bfa_LowestPr_count.Text = oReader["Count"].ToString();
        } oReader.Close();
        //bfa_SoleLOA
        sCommand = "SELECT Count(*) as Count FROM tblBACSupportingDocuments WHERE BuyerID=" + Session["UserId"] + " AND BidRefNo=" + Request.Form["BuyerBidForBac"] + " AND DocuName='bfa_SoleLOA'";
        oReader = SqlHelper.ExecuteReader(connstring, CommandType.Text, sCommand);
        if (oReader.HasRows)
        {
            oReader.Read();
            bfa_SoleLOA_count.Text = oReader["Count"].ToString();
        } oReader.Close();
        //bfa_SoleCSD
        sCommand = "SELECT Count(*) as Count FROM tblBACSupportingDocuments WHERE BuyerID=" + Session["UserId"] + " AND BidRefNo=" + Request.Form["BuyerBidForBac"] + " AND DocuName='bfa_SoleCSD'";
        oReader = SqlHelper.ExecuteReader(connstring, CommandType.Text, sCommand);
        if (oReader.HasRows)
        {
            oReader.Read();
            bfa_SoleCSD_count.Text = oReader["Count"].ToString();
        } oReader.Close();
        //bfa_TechSpec
        sCommand = "SELECT Count(*) as Count FROM tblBACSupportingDocuments WHERE BuyerID=" + Session["UserId"] + " AND BidRefNo=" + Request.Form["BuyerBidForBac"] + " AND DocuName='bfa_TechSpec'";
        oReader = SqlHelper.ExecuteReader(connstring, CommandType.Text, sCommand);
        if (oReader.HasRows)
        {
            oReader.Read();
            bfa_TechSpec_count.Text = oReader["Count"].ToString();
        } oReader.Close();
        //bfa_LeadTime
        sCommand = "SELECT Count(*) as Count FROM tblBACSupportingDocuments WHERE BuyerID=" + Session["UserId"] + " AND BidRefNo=" + Request.Form["BuyerBidForBac"] + " AND DocuName='bfa_LeadTime'";
        oReader = SqlHelper.ExecuteReader(connstring, CommandType.Text, sCommand);
        if (oReader.HasRows)
        {
            oReader.Read();
            bfa_LeadTime_count.Text = oReader["Count"].ToString();
        } oReader.Close();
        //bfa_BFAOth
        sCommand = "SELECT Count(*) as Count FROM tblBACSupportingDocuments WHERE BuyerID=" + Session["UserId"] + " AND BidRefNo=" + Request.Form["BuyerBidForBac"] + " AND DocuName='bfa_BFAOth'";
        oReader = SqlHelper.ExecuteReader(connstring, CommandType.Text, sCommand);
        if (oReader.HasRows)
        {
            oReader.Read();
            bfa_BFAOth_count.Text = oReader["Count"].ToString();
        } oReader.Close();


        //Label1.Text = Repeater_SDA_APR.Items.Count.ToString();
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
                        // Copy the source file into 
                        // the compression stream.
                        inFile.CopyTo(Compress);

                        //Console.WriteLine("Compressed {0} from {1} to {2} bytes.",
                        //    fi.Name, fi.Length.ToString(), outFile.Length.ToString());
                    }
                }
            }
        }
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
                    // Copy the decompression stream 
                    // into the output file.
                    Decompress.CopyTo(outFile);

                    //Console.WriteLine("Decompressed: {0}", fi.Name);
                }
            }
        }
    }

    protected void isNewBid(bool isNew)
    {
        bool NotIsNew;
        if (isNew == true) NotIsNew = false;
        else NotIsNew = true;
        txtNew.Visible = isNew;
        lblNew.Visible = isNew;
        //ItemDescLbl.ReadOnly = NotIsNew;
        //PRRefNoLbl.ReadOnly = NotIsNew;
        //EstItemValueLbl.ReadOnly = NotIsNew;
        PRDate.ReadOnly = NotIsNew;
        DateApprovedLbl.ReadOnly = NotIsNew;
    }

    protected void doBuyerBidForBac()
    {
        string sCommand;
        DateTime date1;
        ItemDescLbl.Text = "";
        PRRefNoLbl.Text = "";
        EstItemValueLbl.Text = String.Format("{0:0,0.00}", 0);
        PRDate.Text = String.Format("{0:yyyy-MMM-dd}", DateTime.Today.Date);
        DateApprovedLbl.Text = String.Format("{0:yyyy-MMM-dd}", DateTime.Today.Date);
        ss_nobidders.Value = "0";

        ////sCommand = "SELECT t1.*, t3.Company FROM tblBidItems t1 INNER JOIN rfcCompany t3 ON t3.CompanyId = t1.CompanyId WHERE t1.Status=4 ";
        ////sCommand = sCommand + "AND t1.ForAuction=0 AND t1.BuyerId=" + Session["UserId"] + " AND t1.BidRefNo=" + Request.Form["BuyerBidForBac"] + " AND t3.CompanyId=t1.CompanyId ORDER BY t1.DateCreated DESC";
        sCommand = "SELECT * FROM tblBacBidItems WHERE BidRefNo=" + Request.Form["BuyerBidForBac"] + " ";
        string connstring = HttpContext.Current.Session["ConnectionString"].ToString();
        SqlDataReader oReader = SqlHelper.ExecuteReader(connstring, CommandType.Text, sCommand);
        if (oReader.HasRows)
        {
            oReader.Read();
            ItemDescLbl.Text = oReader["ItemDesc"].ToString();
            PRRefNoLbl.Text = oReader["SAPPRNo"].ToString();
            EstItemValueLbl.Text = String.Format("{0:0,0.00}", Convert.ToDouble(oReader["BidAmount"].ToString()));
            date1 = DateTime.Parse(oReader["SAPPRDate"].ToString());
            PRDate.Text = String.Format("{0:yyyy-MMM-dd}", date1);
            date1 = DateTime.Parse(oReader["RcvdDate"].ToString());
            DateApprovedLbl.Text = String.Format("{0:yyyy-MMM-dd}", date1);
            ss_nobidsrcvd.Value = oReader["SS_NoBidsRcvd"].ToString();
            ss_nobidders.Value = oReader["SS_NoBidders"].ToString();
            lblAccCost.Text = String.Format("{0:0,0.00}", Convert.ToDouble(oReader["AccumulativeCost"].ToString())); oReader["AccumulativeCost"].ToString();
            ss_crcnumber.Value = oReader["ss_crc_no"].ToString();
            descPaymentTerms.Text = oReader["descPaymentTerms"].ToString();

            string sCompanyID = oReader["CompanyId"].ToString();
            switch (sCompanyID)
            {
                case "0": CompanyIdGT.Checked = true; break;
                case "1": CompanyIdIC.Checked = true; break;
                case "2": CompanyIdGXI.Checked = true; break;
                case "3": CompanyIdEGG.Checked = true; break;
            }
            oReader.Close();
        }

        if (BuyerBidForBac.SelectedIndex == 1)
        {
            sCommand = "SELECT TOP 1 0 BidRefNo, 0 PRRefNo, '' ItemDesc, 0 EstItemValue, " + Session["UserId"] + " BuyerId,  GETDATE() ApprovedBy, 4 Status, ";
            sCommand = sCommand + "0 ForAuction, 0 CompanyId, GETDATE() PRDate, GETDATE() DateApproved, '' BidRefNoItemDesc ";
            sCommand = sCommand + "FROM tblBidItems";
            dsBuyerBidDetailsForBac.SelectCommand = sCommand;
            dsBuyerBidDetailsForBac.DataBind();
            isNewBid(true);
        }
        else
        {
            isNewBid(false);
        }

        bool bReadOnly;
        if (BuyerBidForBac.SelectedIndex == 1) bReadOnly = false;
        else bReadOnly = true;
        TextBox lbl1 = (TextBox)Form.FindControl("EstItemValueLbl");
        iTotal = iTotal + float.Parse(lbl1.Text);
        lblTotalAll.Value = iTotal.ToString();
        lbl1.ReadOnly = bReadOnly;
        iApproverCount = 0;
        dsApprover.SelectCommand = "IF EXISTS (SELECT 1 FROM tblBACSupportingDocuments WHERE BidRefNo=" + Request.Form["BuyerBidForBac"] + " AND DocuName='bfa_LowestPr') BEGIN SELECT t1.BACId, t1.Initials AS Name1, t1.LimitLowestChar AS ApprovingMaxLimit, t1.LimitLowestTo, t1.LimitNonLowestTo, t1.Committee, CASE WHEN t2.Approver IS NULL THEN 0 ELSE 1 END AS isApprover FROM vwApprovers t1 LEFT OUTER JOIN vmBacBidApprovers t2 on t1.BACId = t2.Approver AND BidRefNo =" + Request.Form["BuyerBidForBac"] + "  WHERE t1.Approver = 0 AND t1.OIC = 0 AND t1.LimitLowest <= (SELECT SUM(TotalCost) FROM tblBACEvaluationDetails WHERE BidRefNo=" + Request.Form["BuyerBidForBac"] + " AND Chkd=1) UNION SELECT t1.BACId, t1.Initials AS Name1, t1.LimitLowestChar AS ApprovingMaxLimit, t1.LimitLowestTo, t1.LimitNonLowestTo, t1.Committee, CASE WHEN t2.Approver IS NULL THEN 0 ELSE 1 END AS isApprover FROM vwApprovers t1 LEFT OUTER JOIN vmBacBidApprovers t2 on t1.BACId = t2.Approver AND BidRefNo =" + Request.Form["BuyerBidForBac"] + "  WHERE t1.Approver = 1 AND t1.OIC = 0 AND (SELECT SUM(TotalCost) FROM tblBACEvaluationDetails WHERE BidRefNo=" + Request.Form["BuyerBidForBac"] + " AND Chkd=1) BETWEEN t1.LimitLowest AND t1.LimitLowestTo ORDER BY LimitLowestTo ASC, Committee, Name1 END ELSE BEGIN SELECT t1.BACId, t1.Initials AS Name1, t1.LimitNonLowestChar AS ApprovingMaxLimit, t1.LimitLowestTo, t1.LimitNonLowestTo, t1.Committee, CASE WHEN t2.Approver IS NULL THEN 0 ELSE 1 END AS isApprover FROM vwApprovers t1 LEFT OUTER JOIN vmBacBidApprovers t2 on t1.BACId = t2.Approver AND BidRefNo =" + Request.Form["BuyerBidForBac"] + " WHERE t1.Approver = 0 AND t1.OIC = 0 AND t1.LimitNonLowest <= (SELECT SUM(TotalCost) FROM tblBACEvaluationDetails WHERE BidRefNo=" + Request.Form["BuyerBidForBac"] + " AND Chkd=1) UNION SELECT t1.BACId, t1.Initials AS Name1, t1.LimitNonLowestChar AS ApprovingMaxLimit, t1.LimitLowestTo, t1.LimitNonLowestTo, t1.Committee, CASE WHEN t2.Approver IS NULL THEN 0 ELSE 1 END AS isApprover FROM vwApprovers t1 LEFT OUTER JOIN vmBacBidApprovers t2 on t1.BACId = t2.Approver AND BidRefNo =" + Request.Form["BuyerBidForBac"] + " WHERE t1.Approver = 1 AND t1.OIC = 0 AND (SELECT SUM(TotalCost) FROM tblBACEvaluationDetails WHERE BidRefNo=" + Request.Form["BuyerBidForBac"] + " AND Chkd=1) BETWEEN t1.LimitNonLowest AND t1.LimitNonLowestTo ORDER BY LimitNonLowestTo ASC, LimitLowestTo ASC, Committee, Name1 END";
        //Response.Write(dsApprover.SelectCommand);
        dsApprover.DataBind();
        RepeaterApprover.DataBind();
    }

    protected void UpdateCommercialDetails()
    {
        Repeater oRpt;
        TextBox oTxt;
        string connstring = HttpContext.Current.Session["ConnectionString"].ToString();
        string sCommand;

        TextBox oPRNo;
        TextBox oItemD;
        foreach (GridViewRow oGrid in gvItems.Rows)
        {
            //LinkButton oTxt2 = ((LinkButton)oGrid.FindControl("lblAddItem"));
            //if (oTxt.CommandArgument.ToString() == e.CommandArgument.ToString()) oTxt.Text = "Edit Item";

            oPRNo = (TextBox)oGrid.FindControl("lblPRNo");
            oItemD = (TextBox)oGrid.FindControl("lblItemD");
            LinkButton oTxt2 = ((LinkButton)oGrid.FindControl("lblAddItem"));
            sCommand = "UPDATE tblBACEvaluationDetails SET PR_No='" + oPRNo.Text.Replace("'", "''").Trim() + "' , ItemName='" + oItemD.Text.Replace("'", "''").Trim() + "' WHERE BidRefNo=" + Request.Form["BuyerBidForBac"] + " ";
            sCommand = sCommand + "AND BidDetailNo=" + oTxt2.CommandArgument.ToString() + " AND UserId=" + Session["UserId"];

            if (oTxt2.CommandArgument.ToString() != "") SqlHelper.ExecuteNonQuery(connstring, CommandType.Text, sCommand);
        }

        foreach (DataListItem oItem in lstCommDetails.Items)
        {
            string sBidDetailNo1 = ((TextBox)oItem.FindControl("BidDetailNo1")).Text;

            // vendor name
            oRpt = (Repeater)oItem.FindControl("rptVendorName");
            foreach(RepeaterItem oRptItem in oRpt.Items)
            {
                if (oRptItem.ItemType == ListItemType.Item || oRptItem.ItemType == ListItemType.AlternatingItem)
                {
                    string sBidTender1 = ((TextBox)oRptItem.FindControl("txtBidTenderNo1")).Text;
                    TextBox oTxt2 = (TextBox)oRptItem.FindControl("txtVendorName");
                    sCommand = "UPDATE tblBACEvaluationDetails SET VendorName='" + oTxt2.Text.Replace("'", "''").Trim() + "' WHERE BidRefNo=" + Request.Form["BuyerBidForBac"] + " ";
                    sCommand = sCommand + "AND BidDetailNo=" + sBidDetailNo1 + " AND BidTenderNo='" + sBidTender1 + "' AND UserId=" + Session["UserId"];
                    SqlHelper.ExecuteNonQuery(connstring, CommandType.Text, sCommand);
                }
            }
            // quantity
            oRpt = (Repeater)oItem.FindControl("rptQty");
            foreach (RepeaterItem oRptItem in oRpt.Items)
            {
                if (oRptItem.ItemType == ListItemType.Item || oRptItem.ItemType == ListItemType.AlternatingItem)
                {
                    string sBidTender1 = ((TextBox)oRptItem.FindControl("txtBidTenderNo1")).Text;
                    oTxt = (TextBox)oRptItem.FindControl("txtQty");
                    sCommand = "UPDATE tblBACEvaluationDetails SET Qty=" + oTxt.Text + " WHERE BidRefNo=" + Request.Form["BuyerBidForBac"] + " ";
                    sCommand = sCommand + "AND BidDetailNo=" + sBidDetailNo1 + " AND BidTenderNo='" + sBidTender1 + "' AND UserId=" + Session["UserId"];
                    SqlHelper.ExecuteNonQuery(connstring, CommandType.Text, sCommand);
                }
            }
            // unit of measure
            oRpt = (Repeater)oItem.FindControl("rptUnit");
            foreach (RepeaterItem oRptItem in oRpt.Items)
            {
                if (oRptItem.ItemType == ListItemType.Item || oRptItem.ItemType == ListItemType.AlternatingItem)
                {
                    string sBidTender1 = ((TextBox)oRptItem.FindControl("txtBidTenderNo1")).Text;
                    oTxt = (TextBox)oRptItem.FindControl("txtUnit");
                    sCommand = "UPDATE tblBACEvaluationDetails SET UnitMeasure='" + oTxt.Text.Replace("'", "''").Trim() + "' WHERE BidRefNo=" + Request.Form["BuyerBidForBac"] + " ";
                    sCommand = sCommand + "AND BidDetailNo=" + sBidDetailNo1 + " AND BidTenderNo='" + sBidTender1 + "' AND UserId=" + Session["UserId"];
                    SqlHelper.ExecuteNonQuery(connstring, CommandType.Text, sCommand);
                }
            }
            // currency
            oRpt = (Repeater)oItem.FindControl("rptCurr");
            foreach (RepeaterItem oRptItem in oRpt.Items)
            {
                if (oRptItem.ItemType == ListItemType.Item || oRptItem.ItemType == ListItemType.AlternatingItem)
                {
                    string sBidTender1 = ((TextBox)oRptItem.FindControl("txtBidTenderNo1")).Text;
                    oTxt = (TextBox)oRptItem.FindControl("txtCurr");
                    sCommand = "UPDATE tblBACEvaluationDetails SET Currenzy='" + oTxt.Text.Replace("'", "''").Trim() + "' WHERE BidRefNo=" + Request.Form["BuyerBidForBac"] + " ";
                    sCommand = sCommand + "AND BidDetailNo=" + sBidDetailNo1 + " AND BidTenderNo='" + sBidTender1 + "' AND UserId=" + Session["UserId"];
                    SqlHelper.ExecuteNonQuery(connstring, CommandType.Text, sCommand);
                }
            }
            // unit cost
            oRpt = (Repeater)oItem.FindControl("rptUnitCost");
            foreach (RepeaterItem oRptItem in oRpt.Items)
            {
                if (oRptItem.ItemType == ListItemType.Item || oRptItem.ItemType == ListItemType.AlternatingItem)
                {
                    string sBidTender1 = ((TextBox)oRptItem.FindControl("txtBidTenderNo1")).Text;
                    oTxt = (TextBox)oRptItem.FindControl("txtUnitCost");
                    sCommand = "UPDATE tblBACEvaluationDetails SET UnitCost=" + oTxt.Text.Replace(",", "") + " WHERE BidRefNo=" + Request.Form["BuyerBidForBac"] + " ";
                    sCommand = sCommand + "AND BidDetailNo=" + sBidDetailNo1 + " AND BidTenderNo='" + sBidTender1 + "' AND UserId=" + Session["UserId"];
                    SqlHelper.ExecuteNonQuery(connstring, CommandType.Text, sCommand);
                }
            }
            // total cost
            oRpt = (Repeater)oItem.FindControl("rptTotalCost");
            foreach (RepeaterItem oRptItem in oRpt.Items)
            {
                if (oRptItem.ItemType == ListItemType.Item || oRptItem.ItemType == ListItemType.AlternatingItem)
                {
                    string sBidTender1 = ((TextBox)oRptItem.FindControl("txtBidTenderNo1")).Text;
                    oTxt = (TextBox)oRptItem.FindControl("txtTotalCost");
                    sCommand = "UPDATE tblBACEvaluationDetails SET TotalCost=" + oTxt.Text.Replace(",", "") + " WHERE BidRefNo=" + Request.Form["BuyerBidForBac"] + " ";
                    sCommand = sCommand + "AND BidDetailNo=" + sBidDetailNo1 + " AND BidTenderNo='" + sBidTender1 + "' AND UserId=" + Session["UserId"];
                    SqlHelper.ExecuteNonQuery(connstring, CommandType.Text, sCommand);
                }
            }
            // ranking
            oRpt = (Repeater)oItem.FindControl("rptRanking");
            foreach (RepeaterItem oRptItem in oRpt.Items)
            {
                if (oRptItem.ItemType == ListItemType.Item || oRptItem.ItemType == ListItemType.AlternatingItem)
                {
                    string sBidTender1 = ((TextBox)oRptItem.FindControl("txtBidTenderNo1")).Text;
                    oTxt = (TextBox)oRptItem.FindControl("txtRanking");
                    sCommand = "UPDATE tblBACEvaluationDetails SET Ranking=" + oTxt.Text + " WHERE BidRefNo=" + Request.Form["BuyerBidForBac"] + " ";
                    sCommand = sCommand + "AND BidDetailNo=" + sBidDetailNo1 + " AND BidTenderNo='" + sBidTender1 + "' AND UserId=" + Session["UserId"];
                    SqlHelper.ExecuteNonQuery(connstring, CommandType.Text, sCommand);
                }
            }
            // award
            oRpt = (Repeater)oItem.FindControl("rptAward");
            foreach (RepeaterItem oRptItem in oRpt.Items)
            {
                if (oRptItem.ItemType == ListItemType.Item || oRptItem.ItemType == ListItemType.AlternatingItem)
                {
                    int Chkd1 = 0;
                    string sBidTender1 = ((TextBox)oRptItem.FindControl("txtBidTenderNo1")).Text;
                    TextBox otxtAward = (TextBox)oRptItem.FindControl("txtAward");
                    HtmlInputCheckBox oChk = ((System.Web.UI.HtmlControls.HtmlInputCheckBox)oRptItem.FindControl("chkAward"));
                    if (oChk.Checked == true) Chkd1 = 1;
                    sCommand = "UPDATE tblBACEvaluationDetails SET Chkd=" + Chkd1 + " WHERE BidRefNo=" + Request.Form["BuyerBidForBac"] + " ";
                    sCommand = sCommand + "AND BidDetailNo=" + sBidDetailNo1 + " AND BidTenderNo='" + sBidTender1 + "' AND UserId=" + Session["UserId"];
                    SqlHelper.ExecuteNonQuery(connstring, CommandType.Text, sCommand);
                }
            }

            // Update accumulated cost
            sCommand = "UPDATE tblBacBidItems SET AccumulativeCost = ISNULL((select sum(TotalCost) from tblBACEvaluationDetails WHERE BidRefNo=" + Request.Form["BuyerBidForBac"] + " ";
            sCommand = sCommand + "AND Chkd=1 AND UserId=" + Session["UserId"] + "),0) Where BidRefNo=" + Request.Form["BuyerBidForBac"];
            SqlHelper.ExecuteNonQuery(connstring, CommandType.Text, sCommand);
        }
    }

    protected void ShowApprover()
    {
        string connstring = HttpContext.Current.Session["ConnectionString"].ToString();
        string sCommand;
        SqlDataReader oReader;

        // buyer
        sCommand = "SELECT BuyerLastName + ', ' + BuyerFirstName + ' ' + BuyerMidName AS Name1 FROM tblBuyers WHERE BuyerID=" + Session["UserId"];
        oReader = SqlHelper.ExecuteReader(connstring, CommandType.Text, sCommand);
        if (oReader.HasRows)
        {
            oReader.Read();
            lblBuyer.Text = oReader["Name1"].ToString();
        }
        oReader.Close();

        // approver 4: purchasing
        sCommand = "SELECT t1.PurchasingID, t2.LastName + ', ' + t2.FirstName + ' ' + t2.MiddleName AS Name1 FROM tblSupervisor t1 ";
        sCommand = sCommand + "INNER JOIN tblPurchasing t2 ON t1.PurchasingID=t2.PurchasingID WHERE BuyerID=" + Session["UserId"];
        oReader = SqlHelper.ExecuteReader(connstring, CommandType.Text, sCommand);
        if (oReader.HasRows)
        {
            oReader.Read();
            lblApprover4.Text = oReader["Name1"].ToString();
            approver4.Value = oReader["PurchasingID"].ToString();
        }
        oReader.Close();

        iApproverCount = 0;
        dsApprover.SelectCommand = "IF EXISTS (SELECT 1 FROM tblBACSupportingDocuments WHERE BidRefNo=" + Request.Form["BuyerBidForBac"] + " AND DocuName='bfa_LowestPr') BEGIN SELECT t1.BACId, t1.Initials AS Name1, t1.LimitLowestChar AS ApprovingMaxLimit, t1.LimitLowestTo, t1.LimitNonLowestTo, t1.Committee, CASE WHEN t2.Approver IS NULL THEN 0 ELSE 1 END AS isApprover FROM vwApprovers t1 LEFT OUTER JOIN vmBacBidApprovers t2 on t1.BACId = t2.Approver AND BidRefNo =" + Request.Form["BuyerBidForBac"] + "  WHERE t1.Approver = 0 AND t1.OIC = 0 AND t1.LimitLowest <= (SELECT SUM(TotalCost) FROM tblBACEvaluationDetails WHERE BidRefNo=" + Request.Form["BuyerBidForBac"] + " AND Chkd=1) UNION SELECT t1.BACId, t1.Initials AS Name1, t1.LimitLowestChar AS ApprovingMaxLimit, t1.LimitLowestTo, t1.LimitNonLowestTo, t1.Committee, CASE WHEN t2.Approver IS NULL THEN 0 ELSE 1 END AS isApprover FROM vwApprovers t1 LEFT OUTER JOIN vmBacBidApprovers t2 on t1.BACId = t2.Approver AND BidRefNo =" + Request.Form["BuyerBidForBac"] + "  WHERE t1.Approver = 1 AND t1.OIC = 0 AND (SELECT SUM(TotalCost) FROM tblBACEvaluationDetails WHERE BidRefNo=" + Request.Form["BuyerBidForBac"] + " AND Chkd=1) BETWEEN t1.LimitLowest AND t1.LimitLowestTo ORDER BY LimitLowestTo ASC, Committee, Name1 END ELSE BEGIN SELECT t1.BACId, t1.Initials AS Name1, t1.LimitNonLowestChar AS ApprovingMaxLimit, t1.LimitLowestTo, t1.LimitNonLowestTo, t1.Committee, CASE WHEN t2.Approver IS NULL THEN 0 ELSE 1 END AS isApprover FROM vwApprovers t1 LEFT OUTER JOIN vmBacBidApprovers t2 on t1.BACId = t2.Approver AND BidRefNo =" + Request.Form["BuyerBidForBac"] + " WHERE t1.Approver = 0 AND t1.OIC = 0 AND t1.LimitNonLowest <= (SELECT SUM(TotalCost) FROM tblBACEvaluationDetails WHERE BidRefNo=" + Request.Form["BuyerBidForBac"] + " AND Chkd=1) UNION SELECT t1.BACId, t1.Initials AS Name1, t1.LimitNonLowestChar AS ApprovingMaxLimit, t1.LimitLowestTo, t1.LimitNonLowestTo, t1.Committee, CASE WHEN t2.Approver IS NULL THEN 0 ELSE 1 END AS isApprover FROM vwApprovers t1 LEFT OUTER JOIN vmBacBidApprovers t2 on t1.BACId = t2.Approver AND BidRefNo =" + Request.Form["BuyerBidForBac"] + " WHERE t1.Approver = 1 AND t1.OIC = 0 AND (SELECT SUM(TotalCost) FROM tblBACEvaluationDetails WHERE BidRefNo=" + Request.Form["BuyerBidForBac"] + " AND Chkd=1) BETWEEN t1.LimitNonLowest AND t1.LimitNonLowestTo ORDER BY LimitNonLowestTo ASC, LimitLowestTo ASC, Committee, Name1 END";
        dsApprover.DataBind();
        //Response.Write(dsApprover.SelectCommand);
        RepeaterApprover.DataBind();
    }

    protected void DeleteAttachment(string sOperation, string iFileUploadID)
    {
        string connstring = HttpContext.Current.Session["ConnectionString"].ToString();
        string sCommand;
        SqlDataReader oReader;

        sCommand = "SELECT * FROM tblBACSupportingDocuments WHERE FileUploadID=" + iFileUploadID;
        oReader = SqlHelper.ExecuteReader(connstring, CommandType.Text, sCommand);
        if (oReader.HasRows)
        {
            oReader.Read();
            sCommand = (string)oReader["ActualFileName"];
            FileInfo delFile = new FileInfo(Constant.FILEATTACHMENTSFOLDERDIR + Session["UserId"] + "\\BAC\\" + sCommand + ".gz");
            if (delFile.Exists)
            {
                File.Delete(Constant.FILEATTACHMENTSFOLDERDIR + Session["UserId"] + "\\BAC\\" + sCommand + ".gz");
            }
        }

        sCommand = "DELETE FROM tblBACSupportingDocuments WHERE FileUploadID=" + iFileUploadID;
        switch (sOperation)
        {
            //Supporting Document Attached
            case "APR":
                dsSDA_APR.DeleteCommand = sCommand;
                dsSDA_APR.Delete();
                break;
            case "CE":
                dsSDA_CE.DeleteCommand = sCommand;
                dsSDA_CE.Delete();
                break;
            case "ABC":
                dsSDA_ABC.DeleteCommand = sCommand;
                dsSDA_ABC.Delete();
                break;
            case "NR":
                dsSDA_NR.DeleteCommand = sCommand;
                dsSDA_NR.Delete();
                break;
            case "BR":
                dsSDA_BR.DeleteCommand = sCommand;
                dsSDA_BR.Delete();
                break;
            case "Oth":
                dsSDA_Oth.DeleteCommand = sCommand;
                dsSDA_Oth.Delete();
                break;
            case "TE":
                dsSDA_TE.DeleteCommand = sCommand;
                dsSDA_TE.Delete();
                break;

            //Basis for Awarding
            case "BFA_LowestPr":
                dsBFA_LowestPr.DeleteCommand = sCommand;
                dsBFA_LowestPr.Delete();
                break;
            case "BFA_SoleLOA":
                dsBFA_SoleLOA.DeleteCommand = sCommand;
                dsBFA_SoleLOA.Delete();
                break;
            case "BFA_SoleCSD":
                dsBFA_SoleCSD.DeleteCommand = sCommand;
                dsBFA_SoleCSD.Delete();
                break;
            case "BFA_TechSpec":
                dsBFA_TechSpec.DeleteCommand = sCommand;
                dsBFA_TechSpec.Delete();
                break;
            case "BFA_LeadTime":
                dsBFA_LeadTime.DeleteCommand = sCommand;
                dsBFA_LeadTime.Delete();
                break;
            case "BFAOth":
                dsBFAOth.DeleteCommand = sCommand;
                dsBFAOth.Delete();
                break;
        }
		count_attachements();        
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //FormsAuthenticationHelper.AuthenticateUserWithReturnUrl();
        connstring = Client.ConnectionString(HttpContext.Current.Session["clientid"].ToString());

        dsBuyerBidForBac.ConnectionString = HttpContext.Current.Session["ConnectionString"].ToString();
        dsBuyerBidDetailsForBac.ConnectionString = HttpContext.Current.Session["ConnectionString"].ToString();
        dsSupplyPosition.ConnectionString = HttpContext.Current.Session["ConnectionString"].ToString();
        dsSourcingStrategy.ConnectionString = HttpContext.Current.Session["ConnectionString"].ToString();
        dsTypeOfPurchase.ConnectionString = HttpContext.Current.Session["ConnectionString"].ToString();
        dsPaymentTerms.ConnectionString = HttpContext.Current.Session["ConnectionString"].ToString();
        dsSDA_APR.ConnectionString = HttpContext.Current.Session["ConnectionString"].ToString();
        dsSDA_APR_count.ConnectionString = HttpContext.Current.Session["ConnectionString"].ToString();
        dsSDA_ABC.ConnectionString = HttpContext.Current.Session["ConnectionString"].ToString();
        dsSDA_BR.ConnectionString = HttpContext.Current.Session["ConnectionString"].ToString();
        dsSDA_CE.ConnectionString = HttpContext.Current.Session["ConnectionString"].ToString();
        dsSDA_NR.ConnectionString = HttpContext.Current.Session["ConnectionString"].ToString();
        dsSDA_TE.ConnectionString = HttpContext.Current.Session["ConnectionString"].ToString();
        dsSDA_Oth.ConnectionString = HttpContext.Current.Session["ConnectionString"].ToString();
        dsBFA_LowestPr.ConnectionString = HttpContext.Current.Session["ConnectionString"].ToString();
        dsBFA_SoleLOA.ConnectionString = HttpContext.Current.Session["ConnectionString"].ToString();
        dsBFA_SoleCSD.ConnectionString = HttpContext.Current.Session["ConnectionString"].ToString();
        dsBFA_TechSpec.ConnectionString = HttpContext.Current.Session["ConnectionString"].ToString();
        dsBFA_LeadTime.ConnectionString = HttpContext.Current.Session["ConnectionString"].ToString();
        dsBFAOth.ConnectionString = HttpContext.Current.Session["ConnectionString"].ToString();
        dsBidItemTendersSummary.ConnectionString = HttpContext.Current.Session["ConnectionString"].ToString();
        dsVendors.ConnectionString = HttpContext.Current.Session["ConnectionString"].ToString();
        dsAccreditation.ConnectionString = HttpContext.Current.Session["ConnectionString"].ToString();
        dsItems.ConnectionString = HttpContext.Current.Session["ConnectionString"].ToString();
        //dsBidItemTendersDetails_Vendor.ConnectionString = HttpContext.Current.Session["ConnectionString"].ToString();

        iApproverCount = 0;
        DateTime thisDate1 = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day);
        lblPreparedDate.Text = thisDate1.ToString("MMMM dd, yyyy");

        // force to show attachment
        if (Request.Params["ShowAttachment"] is object)
        {
            connstring = HttpContext.Current.Session["ConnectionString"].ToString();
            string sCommand, sContentType;
            SqlDataReader oReader;

            addAttachmentMsg.Text = "";

            sCommand = "SELECT * FROM tblBACSupportingDocuments WHERE FileUploadID=" + Request.Params["ShowAttachment"];
            oReader = SqlHelper.ExecuteReader(connstring, CommandType.Text, sCommand);
            if (oReader.HasRows)
            {
                oReader.Read();
                sCommand = (string)oReader["ActualFileName"];
                sContentType = (string)oReader["ContentType"];
                string myBuyerID = (string)oReader["BuyerID"].ToString();
                FileInfo OrigFile = new FileInfo(Constant.FILEATTACHMENTSFOLDERDIR + myBuyerID + "\\BAC\\" + sCommand);
                FileInfo GZFile = new FileInfo(Constant.FILEATTACHMENTSFOLDERDIR + myBuyerID + "\\BAC\\" + sCommand + ".gz");
                if (GZFile.Exists)
                {
                    try
                    {
                        Decompress(GZFile);
                        //Response.Redirect("../../web/fileattachments/" + myBuyerID + "/BAC/" + sCommand);
                        Response.Clear();
                        Response.ContentType = sContentType;
                        Response.AppendHeader("Content-Disposition", "attachment; filename=" + OrigFile.ToString());
                        Response.TransmitFile(OrigFile.ToString(), 0, OrigFile.Length);
                        //File.Delete(Constant.FILEATTACHMENTSFOLDERDIR + myBuyerID + "\\BAC\\" + sCommand);
                        Response.Flush();
                        Response.End();
                    }
                    finally
                    {
                        File.Delete(Constant.FILEATTACHMENTSFOLDERDIR + myBuyerID + "\\BAC\\" + sCommand);
                    }
                }
            }
        }
    }

    protected void BuyerBidForBac_DataBound(object sender, EventArgs e)
    {
        BuyerBidForBac.Items.Insert(0, new ListItem("---- SELECT BID ----", "-1"));

        string connstring = HttpContext.Current.Session["ConnectionString"].ToString();
        string sCommand;
        SqlDataReader oReader;
        //sCommand = "select max(BidRefNo) as maxBidRefNo from (select \"" + Session["UserID"].ToString() + "\" + max(BidRefNo) AS BidRefNo tblBidItems where BuyerId="+Session["UserId"].ToString() + " UNION select  max(BidRefNo)+1 AS BidRefNo tblBacBidItems where BuyerId=" + Session["UserId"].ToString() + ") as t1";
        sCommand = "select max(BidRefNo) as maxBidRefNo from (select cast( ('" + Session["UserID"].ToString() + "' + cast(max(BidRefNo) as varchar)) as int) AS BidRefNo FROM tblBidItems where BuyerId=" + Session["UserID"].ToString() + " UNION select max(BidRefNo)+1 AS BidRefNo FROM tblBacBidItems where BuyerId=" + Session["UserID"].ToString() + ") as t1 ";

        //Response.Write(sCommand);
        oReader = SqlHelper.ExecuteReader(connstring, CommandType.Text, sCommand);
        if (oReader.HasRows){
            oReader.Read();
            //string NewBidRefNo = Session["UserID"].ToString() + (string)oReader["maxBidRefNo"].ToString();
            string NewBidRefNo = (string)oReader["maxBidRefNo"].ToString();
            BuyerBidForBac.Items.Insert(1, new ListItem("ADD NEW", NewBidRefNo));
            txtNew.Text = NewBidRefNo;
        }
        oReader.Close();
    }

    protected void Repeater13_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            ((CheckBox)e.Item.FindControl("chkAwarded")).Attributes.Add("VendorID", ((DataRowView)e.Item.DataItem)["VendorID"].ToString());
        }
    }

    protected void Repeater5_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            ((DropDownList)e.Item.FindControl("txtTechnical")).Attributes.Add("VendorID", ((DataRowView)e.Item.DataItem)["VendorID"].ToString());
            ((DropDownList)e.Item.FindControl("txtTechnical")).SelectedIndex = int.Parse(((DataRowView)e.Item.DataItem)["TechCompliance"].ToString());
        }
    }

    protected void Repeater6_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            ((DropDownList)e.Item.FindControl("txtCommercial")).Attributes.Add("VendorID", ((DataRowView)e.Item.DataItem)["VendorID"].ToString());
            ((DropDownList)e.Item.FindControl("txtCommercial")).SelectedIndex = int.Parse(((DataRowView)e.Item.DataItem)["CommCompliance"].ToString());
        }
    }

    protected void Repeater7_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            ((DropDownList)e.Item.FindControl("txtContract")).Attributes.Add("VendorID", ((DataRowView)e.Item.DataItem)["VendorID"].ToString());
            ((DropDownList)e.Item.FindControl("txtContract")).SelectedIndex = int.Parse(((DataRowView)e.Item.DataItem)["ContCompliance"].ToString());
        }
    }

    protected void Repeater4_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            DropDownList oDropVendors = ((DropDownList)e.Item.FindControl("txtVendorName"));
            if (((DataRowView)e.Item.DataItem)["VendorID"].ToString().Contains("V"))
            {
                oDropVendors.DataSourceID = "dsVendors";
                oDropVendors.DataBind();
                oDropVendors.Enabled = true;
                oDropVendors.Items.Insert(0, "-- SELECT --");
                oDropVendors.Attributes.Add("VendorID", "0");
                oDropVendors.Attributes.Add("onChange", "CheckVendor();");
                oDropVendors.Attributes.Add("Enabled", "true");
            }
            else
            {
                oDropVendors.DataSourceID = "dsBidItemTendersSummary";
                oDropVendors.DataBind();
                oDropVendors.Enabled = false;
                oDropVendors.Attributes.Add("VendorID", ((DataRowView)e.Item.DataItem)["VendorID"].ToString());
                oDropVendors.Attributes.Add("Enabled", "false");
            }
            oDropVendors.SelectedValue = ((DataRowView)e.Item.DataItem)["VendorID"].ToString();
            ((System.Web.UI.HtmlControls.HtmlInputHidden)e.Item.FindControl("fromDB")).Attributes.Add("VendorID", ((DataRowView)e.Item.DataItem)["VendorID"].ToString());
            if (((System.Web.UI.HtmlControls.HtmlInputHidden)e.Item.FindControl("fromDB")).Value == "1")
                ((System.Web.UI.WebControls.Image)e.Item.FindControl("imgVendor")).Visible = false;
            else
                ((System.Web.UI.WebControls.Image)e.Item.FindControl("imgVendor")).Attributes.Add("onclick", "btnDelCol_onclick('" + ((DataRowView)e.Item.DataItem)["VendorID"].ToString() + "');");
        }
    }


    protected void Repeater100_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            DropDownList cbAcc = ((DropDownList)e.Item.FindControl("cbAccreditation"));
            if (((DataRowView)e.Item.DataItem)["VendorID"].ToString().Contains("V"))
            {
            }
            else
            {
            }
            cbAcc.Items.Insert(0, new ListItem("", "0"));
            cbAcc.Attributes.Add("VendorID", ((DataRowView)e.Item.DataItem)["VendorID"].ToString());
            cbAcc.SelectedIndex = int.Parse(((DataRowView)e.Item.DataItem)["Accredited"].ToString());
            cbAcc.Enabled = false;
        }
    }

    protected void RepeaterPRating_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            ((TextBox)e.Item.FindControl("txtPRating")).Attributes.Add("VendorID", ((DataRowView)e.Item.DataItem)["VendorID"].ToString());
            //if (((DataRowView)e.Item.DataItem)["VendorID"].ToString().Contains("V"))
            //{
            //    ((TextBox)e.Item.FindControl("txtPRating")).Enabled = true;
            //}
            //else
            //{
            //    ((TextBox)e.Item.FindControl("txtPRating")).Enabled = false;
            //}
        }
    }

    protected void RepeaterApprover_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            iApproverCount++;
            //((Literal)e.Item.FindControl("lblApprover")).Text = "Approver " + iApproverCount.ToString();
            ((Literal)e.Item.FindControl("lblApprover")).Text = "Approver ";
        }
    }

    protected void cbPaymentTerms_DataBound(object sender, EventArgs e)
    {
        cbPaymentTerms.Items.Insert(0, new ListItem("---- SELECT PAYMENT TERMS ----", "-1"));
    }

    protected void cbSourcingStrategy_DataBound(object sender, EventArgs e)
    {
        cbSourcingStrategy.Items.Insert(0, new ListItem("---- SELECT SOURCING STRATEGY ----", "-1"));
    }

    protected void cbTypeOfPurchase_DataBound(object sender, EventArgs e)
    {
        cbTypeOfPurchase.Items.Insert(0, new ListItem("---- SELECT TYPE OF PURCHASE ----", "-1"));
    }

    protected void cbSupplyPosition_DataBound(object sender, EventArgs e)
    {
        cbSupplyPosition.Items.Insert(0, new ListItem("---- SELECT SUPPLY POSITION ----", "-1"));
    }

    //#########################################################
    //SUPPORTING DOCUMENT ATTACHED
    //#########################################################
    protected void Repeater_SDA_APR_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            ((System.Web.UI.HtmlControls.HtmlInputCheckBox)e.Item.FindControl("chkSDA_APR")).Attributes.Add("onclick", "DeleteAttach('APR', '" + ((System.Web.UI.HtmlControls.HtmlInputCheckBox)e.Item.FindControl("chkSDA_APR")).ClientID + "','" + ((DataRowView)e.Item.DataItem)["FileUploadID"].ToString() + "');");
        }
    }
    protected void dsSDA_APR_Load(object sender, EventArgs e)
    {
        string control1 = Request.Form["__EVENTTARGET"];
        if (control1 == "DeleteAttach_APR")
        {
            string iArg = Request.Form["__EVENTARGUMENT"];
            DeleteAttachment("APR", iArg);
        }
    }
    protected void Repeater_SDA_CE_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            ((System.Web.UI.HtmlControls.HtmlInputCheckBox)e.Item.FindControl("chkSDA_CE")).Attributes.Add("onclick", "DeleteAttach('CE', '" + ((System.Web.UI.HtmlControls.HtmlInputCheckBox)e.Item.FindControl("chkSDA_CE")).ClientID + "','" + ((DataRowView)e.Item.DataItem)["FileUploadID"].ToString() + "');");
        }
    }
    protected void dsSDA_CE_Load(object sender, EventArgs e)
    {
        string control1 = Request.Form["__EVENTTARGET"];
        if (control1 == "DeleteAttach_CE")
        {
            string iArg = Request.Form["__EVENTARGUMENT"];
            DeleteAttachment("CE", iArg);
        }
    }
    protected void Repeater_SDA_ABC_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            ((System.Web.UI.HtmlControls.HtmlInputCheckBox)e.Item.FindControl("chkSDA_ABC")).Attributes.Add("onclick", "DeleteAttach('ABC', '" + ((System.Web.UI.HtmlControls.HtmlInputCheckBox)e.Item.FindControl("chkSDA_ABC")).ClientID + "','" + ((DataRowView)e.Item.DataItem)["FileUploadID"].ToString() + "');");
        }
    }
    protected void dsSDA_ABC_Load(object sender, EventArgs e)
    {
        string control1 = Request.Form["__EVENTTARGET"];
        if (control1 == "DeleteAttach_ABC")
        {
            string iArg = Request.Form["__EVENTARGUMENT"];
            DeleteAttachment("ABC", iArg);
        }
    }
    protected void Repeater_SDA_NR_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            ((System.Web.UI.HtmlControls.HtmlInputCheckBox)e.Item.FindControl("chkSDA_NR")).Attributes.Add("onclick", "DeleteAttach('NR', '" + ((System.Web.UI.HtmlControls.HtmlInputCheckBox)e.Item.FindControl("chkSDA_NR")).ClientID + "','" + ((DataRowView)e.Item.DataItem)["FileUploadID"].ToString() + "');");
        }
    }
    protected void dsSDA_NR_Load(object sender, EventArgs e)
    {
        string control1 = Request.Form["__EVENTTARGET"];
        if (control1 == "DeleteAttach_NR")
        {
            string iArg = Request.Form["__EVENTARGUMENT"];
            DeleteAttachment("NR", iArg);
        }
    }
    protected void Repeater_SDA_BR_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            ((System.Web.UI.HtmlControls.HtmlInputCheckBox)e.Item.FindControl("chkSDA_BR")).Attributes.Add("onclick", "DeleteAttach('BR', '" + ((System.Web.UI.HtmlControls.HtmlInputCheckBox)e.Item.FindControl("chkSDA_BR")).ClientID + "','" + ((DataRowView)e.Item.DataItem)["FileUploadID"].ToString() + "');");
        }
    }
    protected void dsSDA_BR_Load(object sender, EventArgs e)
    {
        string control1 = Request.Form["__EVENTTARGET"];
        if (control1 == "DeleteAttach_BR")
        {
            string iArg = Request.Form["__EVENTARGUMENT"];
            DeleteAttachment("BR", iArg);
        }
    }
    protected void Repeater_SDA_Oth_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            ((System.Web.UI.HtmlControls.HtmlInputCheckBox)e.Item.FindControl("chkSDA_Oth")).Attributes.Add("onclick", "DeleteAttach('Oth', '" + ((System.Web.UI.HtmlControls.HtmlInputCheckBox)e.Item.FindControl("chkSDA_Oth")).ClientID + "','" + ((DataRowView)e.Item.DataItem)["FileUploadID"].ToString() + "');");
        }
    }
    protected void dsSDA_Oth_Load(object sender, EventArgs e)
    {
        string control1 = Request.Form["__EVENTTARGET"];
        if (control1 == "DeleteAttach_Oth")
        {
            string iArg = Request.Form["__EVENTARGUMENT"];
            DeleteAttachment("Oth", iArg);
        }
    }
    protected void Repeater_SDA_TE_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            ((System.Web.UI.HtmlControls.HtmlInputCheckBox)e.Item.FindControl("chkSDA_TE")).Attributes.Add("onclick", "DeleteAttach('TE', '" + ((System.Web.UI.HtmlControls.HtmlInputCheckBox)e.Item.FindControl("chkSDA_TE")).ClientID + "','" + ((DataRowView)e.Item.DataItem)["FileUploadID"].ToString() + "');");
        }
    }
    protected void dsSDA_TE_Load(object sender, EventArgs e)
    {
        string control1 = Request.Form["__EVENTTARGET"];
        if (control1 == "DeleteAttach_TE")
        {
            string iArg = Request.Form["__EVENTARGUMENT"];
            DeleteAttachment("TE", iArg);
        }
    }

    //###########################################################
    //BASIS FOR AWARDING
    //#############################################################
    protected void Repeater_BFA_LowestPr_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            ((System.Web.UI.HtmlControls.HtmlInputCheckBox)e.Item.FindControl("chkBFA_LowestPr")).Attributes.Add("onclick", "DeleteAttach('BFA_LowestPr', '" + ((System.Web.UI.HtmlControls.HtmlInputCheckBox)e.Item.FindControl("chkBFA_LowestPr")).ClientID + "','" + ((DataRowView)e.Item.DataItem)["FileUploadID"].ToString() + "');");
        }
    }

    protected void dsBFA_LowestPr_Load(object sender, EventArgs e)
    {
        string control1 = Request.Form["__EVENTTARGET"];
        if (control1 == "DeleteAttach_BFA_LowestPr")
        {
            string iArg = Request.Form["__EVENTARGUMENT"];
            DeleteAttachment("BFA_LowestPr", iArg);
        }
    }

    protected void Repeater_BFA_SoleLOA_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            ((System.Web.UI.HtmlControls.HtmlInputCheckBox)e.Item.FindControl("chkBFA_SoleLOA")).Attributes.Add("onclick", "DeleteAttach('BFA_SoleLOA', '" + ((System.Web.UI.HtmlControls.HtmlInputCheckBox)e.Item.FindControl("chkBFA_SoleLOA")).ClientID + "','" + ((DataRowView)e.Item.DataItem)["FileUploadID"].ToString() + "');");
        }
    }

    protected void dsBFA_SoleLOA_Load(object sender, EventArgs e)
    {
        string control1 = Request.Form["__EVENTTARGET"];
        if (control1 == "DeleteAttach_BFA_SoleLOA")
        {
            string iArg = Request.Form["__EVENTARGUMENT"];
            DeleteAttachment("BFA_SoleLOA", iArg);
        }
    }

    protected void Repeater_BFA_SoleCSD_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            ((System.Web.UI.HtmlControls.HtmlInputCheckBox)e.Item.FindControl("chkBFA_SoleCSD")).Attributes.Add("onclick", "DeleteAttach('BFA_SoleCSD', '" + ((System.Web.UI.HtmlControls.HtmlInputCheckBox)e.Item.FindControl("chkBFA_SoleCSD")).ClientID + "','" + ((DataRowView)e.Item.DataItem)["FileUploadID"].ToString() + "');");
        }
    }

    protected void dsBFA_SoleCSD_Load(object sender, EventArgs e)
    {
        string control1 = Request.Form["__EVENTTARGET"];
        if (control1 == "DeleteAttach_BFA_SoleCSD")
        {
            string iArg = Request.Form["__EVENTARGUMENT"];
            DeleteAttachment("BFA_SoleCSD", iArg);
        }
    }

    protected void Repeater_BFA_TechSpec_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            ((System.Web.UI.HtmlControls.HtmlInputCheckBox)e.Item.FindControl("chkBFA_TechSpec")).Attributes.Add("onclick", "DeleteAttach('BFA_TechSpec', '" + ((System.Web.UI.HtmlControls.HtmlInputCheckBox)e.Item.FindControl("chkBFA_TechSpec")).ClientID + "','" + ((DataRowView)e.Item.DataItem)["FileUploadID"].ToString() + "');");
        }
    }

    protected void dsBFA_TechSpec_Load(object sender, EventArgs e)
    {
        string control1 = Request.Form["__EVENTTARGET"];
        if (control1 == "DeleteAttach_BFA_TechSpec")
        {
            string iArg = Request.Form["__EVENTARGUMENT"];
            DeleteAttachment("BFA_TechSpec", iArg);
        }
    }

    protected void Repeater_BFA_LeadTime_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            ((System.Web.UI.HtmlControls.HtmlInputCheckBox)e.Item.FindControl("chkBFA_LeadTime")).Attributes.Add("onclick", "DeleteAttach('BFA_LeadTime', '" + ((System.Web.UI.HtmlControls.HtmlInputCheckBox)e.Item.FindControl("chkBFA_LeadTime")).ClientID + "','" + ((DataRowView)e.Item.DataItem)["FileUploadID"].ToString() + "');");
        }
    }

    protected void dsBFA_LeadTime_Load(object sender, EventArgs e)
    {
        string control1 = Request.Form["__EVENTTARGET"];
        if (control1 == "DeleteAttach_BFA_LeadTime")
        {
            string iArg = Request.Form["__EVENTARGUMENT"];
            DeleteAttachment("BFA_LeadTime", iArg);
        }
    }

    protected void Repeater_BFAOth_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            ((System.Web.UI.HtmlControls.HtmlInputCheckBox)e.Item.FindControl("chkBFAOth")).Attributes.Add("onclick", "DeleteAttach('BFAOth', '" + ((System.Web.UI.HtmlControls.HtmlInputCheckBox)e.Item.FindControl("chkBFAOth")).ClientID + "','" + ((DataRowView)e.Item.DataItem)["FileUploadID"].ToString() + "');");
        }
    }

    protected void dsBFAOth_Load(object sender, EventArgs e)
    {
        string control1 = Request.Form["__EVENTTARGET"];
        if (control1 == "DeleteAttach_BFAOth")
        {
            string iArg = Request.Form["__EVENTARGUMENT"];
            DeleteAttachment("BFAOth", iArg);
        }
    }

    //############################################################
    //##############################################################


    protected void ClearBACSessions()
    {
        string arr = "";

        // remove all BAC sessions first
        foreach (string sItem in Session.Contents)
            if (sItem.Contains("BAC_BES")) arr = arr + sItem + ";";
        char[] mySeparator = new char[] { ';' };
        string[] arr1 = arr.Split(mySeparator);
        foreach (string arrStr in arr1)
            Session.Remove(arrStr);
    }

    protected void SaveEvaluationSummaryToSession()
    {
    }

    protected void UpdateBACEvaluationSummary()
    {
        Repeater oRpt;
        TextBox oTxt;
        DropDownList oDrop;
        string sCommand;
        string connstring = HttpContext.Current.Session["ConnectionString"].ToString();

        // vendor name
        oRpt = (Repeater)this.form1.FindControl("Repeater4");
        foreach (RepeaterItem oItem in oRpt.Items)
        {
            DropDownList oTxt3 = ((DropDownList)oItem.FindControl("txtVendorName"));
        
            sCommand = "UPDATE tblBACEvaluationSummary SET VendorID='" + oTxt3.SelectedValue + "', VendorName='" + oTxt3.SelectedItem.ToString().Replace("'", "''")+ "' WHERE BidRefNo=" + Request.Form["BuyerBidForBac"] + " ";
            sCommand = sCommand + " AND VendorID='" + oTxt3.Attributes["VendorID"] + "'";
            //Response.Write(sCommand);
            if (oTxt3.SelectedValue != "-- SELECT --")
            {
               SqlHelper.ExecuteNonQuery(connstring, CommandType.Text, sCommand);
            }
            oTxt3.Attributes["VendorID"] = oTxt3.SelectedValue;
        }
        // accreditation status
        oRpt = (Repeater)this.form1.FindControl("Repeater100");
        foreach (RepeaterItem oItem in oRpt.Items)
        {
            oDrop = (DropDownList)oItem.FindControl("cbAccreditation");
            sCommand = "UPDATE tblBACEvaluationSummary SET Accredited=" + oDrop.SelectedIndex.ToString() + " WHERE BidRefNo=" + Request.Form["BuyerBidForBac"] + " ";
            sCommand = sCommand + " AND  VendorID='" + oDrop.Attributes["VendorID"] + "'";
            SqlHelper.ExecuteNonQuery(connstring, CommandType.Text, sCommand);
        }
        // performance rating
        oRpt = (Repeater)this.form1.FindControl("RepeaterPRating");
        foreach (RepeaterItem oItem in oRpt.Items)
        {
            oTxt = (TextBox)oItem.FindControl("txtPRating");
            sCommand = "UPDATE tblBACEvaluationSummary SET PerformanceRating='" + oTxt.Text + "' WHERE BidRefNo=" + Request.Form["BuyerBidForBac"] + " ";
            sCommand = sCommand + " AND VendorID='" + oTxt.Attributes["VendorID"] + "'";
            SqlHelper.ExecuteNonQuery(connstring, CommandType.Text, sCommand);
        }
        // technical compliance
        oRpt = (Repeater)this.form1.FindControl("Repeater5");
        foreach (RepeaterItem oItem in oRpt.Items)
        {
            oDrop = (DropDownList)oItem.FindControl("txtTechnical");
            sCommand = "UPDATE tblBACEvaluationSummary SET TechCompliance=" + oDrop.SelectedIndex.ToString() + " WHERE BidRefNo=" + Request.Form["BuyerBidForBac"] + " ";
            sCommand = sCommand + " AND VendorID='" + oDrop.Attributes["VendorID"] + "'";
            SqlHelper.ExecuteNonQuery(connstring, CommandType.Text, sCommand);
        }
        // commercial compliance
        oRpt = (Repeater)this.form1.FindControl("Repeater6");
        foreach (RepeaterItem oItem in oRpt.Items)
        {
            oDrop = (DropDownList)oItem.FindControl("txtCommercial");
            sCommand = "UPDATE tblBACEvaluationSummary SET CommCompliance=" + oDrop.SelectedIndex.ToString() + " WHERE BidRefNo=" + Request.Form["BuyerBidForBac"] + " ";
            sCommand = sCommand + " AND VendorID='" + oDrop.Attributes["VendorID"] + "'";
            SqlHelper.ExecuteNonQuery(connstring, CommandType.Text, sCommand);
        }
        // contrat compliance
        oRpt = (Repeater)this.form1.FindControl("Repeater7");
        foreach (RepeaterItem oItem in oRpt.Items)
        {
            oDrop = (DropDownList)oItem.FindControl("txtContract");
            sCommand = "UPDATE tblBACEvaluationSummary SET ContCompliance=" + oDrop.SelectedIndex.ToString() + " WHERE BidRefNo=" + Request.Form["BuyerBidForBac"] + " ";
            sCommand = sCommand + " AND VendorID='" + oDrop.Attributes["VendorID"] + "'";
            SqlHelper.ExecuteNonQuery(connstring, CommandType.Text, sCommand);
        }
    }

    protected void dsBidItemTendersSummary_Load(object sender, EventArgs e)
    {
        string control1 = Request.Form["__EVENTTARGET"];
        if (control1 == "NewVendor")
        {
            string sCommand;
            string connstring = HttpContext.Current.Session["ConnectionString"].ToString();

            if (Request.Form["__EVENTARGUMENT"] != "") // delete
            {
                sCommand = "DELETE FROM tblBACEvaluationSummary ";
                sCommand = sCommand + "WHERE BidRefNo=" + Request.Form["BuyerBidForBac"] + " ";
                sCommand = sCommand + "AND VendorID='" + Request.Form["__EVENTARGUMENT"] + "'";
                SqlHelper.ExecuteNonQuery(connstring, CommandType.Text, sCommand);
            }
            else // add
            {
                string iVendor = ((HtmlInputHidden)this.form1.FindControl("txtNewVendor")).Value.ToString();
                ((HtmlInputHidden)this.form1.FindControl("txtNewVendor")).Value = iVendor + 1;
                sCommand = "IF NOT EXISTS (SELECT 1 FROM tblBACEvaluationSummary WHERE BidRefNo = " + Session["BuyerBidForBac"] + " AND VendorID LIKE 'V%') BEGIN INSERT INTO tblBACEvaluationSummary ( BidRefNo, VendorID, VendorName, TechCompliance, CommCompliance, ContCompliance, BidTenderNo, fromDB, Accredited, PerformanceRating) VALUES (";
                sCommand = sCommand + " " + Request.Form["BuyerBidForBac"] + ", CONVERT(VARCHAR, 'V'+'" + iVendor.ToString() + "'), null, ";
                sCommand = sCommand + " 0, 0, 0, 1, 0, 2, 0 ) END";
                SqlHelper.ExecuteNonQuery(connstring, CommandType.Text, sCommand);
            }

            CreateCriteriaRow();
            dsBidItemTendersSummary.SelectCommand = "SELECT * FROM tblBACEvaluationSummary WHERE BidRefNo=" + Request.Form["BuyerBidForBac"] + " ";
            dsBidItemTendersSummary.DataBind();
            CreateCriteriaRow();
        }
    }

    protected void CreateCriteriaRow()
    {
        HtmlInputHidden oNewRow = (HtmlInputHidden)this.form1.FindControl("txtNewRow");
        Session["iRowTemplate"] = int.Parse(oNewRow.Value);
        HtmlTableRow tr;
        HtmlTableCell td, td1;
        TextBox oCrit;
        Repeater oRep;
        Image oImg;
        HtmlTable tbl = (HtmlTable)this.form1.FindControl("tblCriteria");

        //tbl.Rows[tbl.Rows.Count - 1].Visible = false;

        int i;
        for (i = 0; i <= int.Parse(Session["iRowTemplate"].ToString()); i++)
        {
            if (tbl.FindControl("tr" + i.ToString()) == null)
            {
                if (this.form1.FindControl("txtR" + i.ToString()) == null)
                {
                    if (this.form1.FindControl("RepeaterC" + i.ToString()) == null)
                    {
                        oCrit = new TextBox();
                        oRep = new Repeater();

                        tr = new HtmlTableRow();
                        td = new HtmlTableCell();

                        oImg = new Image();
                        oImg.ID = "imgRem" + i.ToString();
                        oImg.ImageUrl = "~/web/buyerscreens/remove.png";
                        oImg.Attributes.Add("onclick", "btnDelRow_onclick('#tr" + i.ToString() + "');");
                        oCrit.ID = "txtR" + i.ToString();
                        oCrit.Width = 100;
                        td.Controls.Add(oImg);
                        td.Controls.Add(oCrit);

                        td1 = new HtmlTableCell();
                        LiteralControl oLit = new LiteralControl();
                        //oLit.Text = " ";
                        //td1.Controls.Add(oLit);

                        oRep.ID = "RepeaterC" + i.ToString();
                        oRep.HeaderTemplate = new MyTemplate(ListItemType.Header);
                        oRep.ItemTemplate = new MyTemplate(ListItemType.Item);
                        oRep.AlternatingItemTemplate = new MyTemplate(ListItemType.AlternatingItem);
                        oRep.FooterTemplate = new MyTemplate(ListItemType.Footer);
                        oRep.ItemDataBound += new RepeaterItemEventHandler(Criteria_ItemDataBoundX);
                        oRep.DataSourceID = "dsBidItemTendersSummary";

                        tr.Cells.Add(td);
                        //tr.Cells.Add(td1);
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

    protected void gvItems_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string sCommand;
        string connstring = HttpContext.Current.Session["ConnectionString"].ToString();

        sCommand = "";

        if (e.CommandName.Equals("AddItemRow"))
        {
            //lblAddItem.Visible = false;
            //doBuyerBidForBac();
            sCommand = sCommand + "IF EXISTS (select BidDetailNo from tblBidItemDetails where BidRefNo=" + Request.Form["BuyerBidForBac"] + " and BidDetailNo=" + e.CommandArgument.ToString() + ") BEGIN ";
            sCommand = sCommand + "INSERT INTO tblBACEvaluationDetails (BidRefNo, BidDetailNo, BidTenderNo, SKU, ItemName, Chkd, Qty, UnitCost, ";
            sCommand = sCommand + "TotalCost, Ranking, fromDB, UnitMeasure, VendorName, Currenzy, UserId) ";
            sCommand = sCommand + "SELECT BidRefNo, BidDetailNo, CONVERT(VARCHAR, BidTenderNo) BidTenderNo, SKU, ItemName, Chkd, Qty, ";
            sCommand = sCommand + "CONVERT(NUMERIC(18, 2), UnitCost) UnitCost, CONVERT(NUMERIC(18, 2), UnitCost * Qty) TotalCost, Ranking, fromDB, UnitMeasure, VendorName, Currenzy, " + Session["UserId"].ToString() + " UserId FROM (";
            sCommand = sCommand + "SELECT 1 fromDB, t2.BidDetailNo, 0 Chkd, ";
            sCommand = sCommand + "t2.Item SKU, t2.DetailDesc ItemName, CONVERT(VARCHAR, t1.BidTenderNo) BidTenderNo, t4.Currency Currenzy, t2.BidRefNo, ";
            sCommand = sCommand + "CAST(((t1.Amount * t2.Qty) + (t1.DeliveryCost - (t1.Discount * t2.Qty))) AS Decimal(18,2)) AS TotalCost, ";
            sCommand = sCommand + "t2.Qty, t2.UnitOfMeasure UnitMeasure, CONVERT(NUMERIC(18, 2), t1.Amount) UnitCost, t3.VendorName, t3.VendorId, t1.Amount, t1.Discount, ";
            sCommand = sCommand + "t1.DeliveryCost, t1.Status, Ranking = (select count(t3n.Amount) from tblBidTenders t3n where t3n.BidDetailNo=t1.BidDetailNo and t3n.Amount < t1.Amount AND t3n.VendorId not in (select VendorId from tblBACEvaluationSummary where (TechCompliance=1 or CommCompliance=1 or ContCompliance=2) AND BidRefNo= " + Request.Form["BuyerBidForBac"] + "))+1 ";
            sCommand = sCommand + "FROM tblBidTenders t1 INNER JOIN tblBidItemDetails t2 ON t2.BidDetailNo = t1.BidDetailNo ";
            sCommand = sCommand + "INNER JOIN tblVendors t3 ON t3.VendorId = t1.VendorId INNER JOIN tblBidItems t4 ON t4.BidRefNo = t2.BidRefNo ";
            sCommand = sCommand + "WHERE t2.BidRefNo = " + Request.Form["BuyerBidForBac"] + " AND t1.Status > 0 AND ";
            sCommand = sCommand + "t2.BidDetailNo = " + e.CommandArgument.ToString() + " AND VendorName not in (select VendorName from tblBACEvaluationSummary where (TechCompliance=1 or CommCompliance=1 or ContCompliance=2) AND BidRefNo= " + Request.Form["BuyerBidForBac"] + " ) ";
            sSQLUnion = "";
            int i = 0;
            foreach (RepeaterItem oItem in Repeater4.Items)
            {
                i++;
                //TextBox oTxt = ((TextBox)oItem.FindControl("txtVendorName"));
                DropDownList oTxt = ((DropDownList)oItem.FindControl("txtVendorName"));
                System.Web.UI.HtmlControls.HtmlInputHidden oTxt1 = ((System.Web.UI.HtmlControls.HtmlInputHidden)oItem.FindControl("fromDB"));
                if (oTxt1.Value == "0")
                {
                    sSQLUnion = sSQLUnion + " UNION ALL ";
                    sSQLUnion = sSQLUnion + "SELECT DISTINCT 0 FromDB, t2.BidDetailNo, 0 Chkd, t2.Item SKU, t2.DetailDesc ItemName, 0 BidTenderNo, t4.Currency Currenzy, ";
                    sSQLUnion = sSQLUnion + Request.Form["BuyerBidForBac"] + " BidRefNo, 0 TotalCost, t2.Qty, t2.UnitOfMeasure UnitMeasure, 0 UnitCost, '" + oTxt.SelectedItem.ToString().Replace("'","''")  + "' VendorName, ";
                    sSQLUnion = sSQLUnion + "0 VendorID, 0 Amount, 0 Discount, 0 DeliveryCost, 4 Status, 0 Ranking ";
                    sSQLUnion = sSQLUnion + "FROM tblBidItemDetails t2  ";
                    sSQLUnion = sSQLUnion + "INNER JOIN tblBidItems t4 ON t4.BidRefNo = t2.BidRefNo ";
                    sSQLUnion = sSQLUnion + "WHERE t2.BidRefNo = " + Request.Form["BuyerBidForBac"] + " ";
                    sSQLUnion = sSQLUnion + "AND t2.BidDetailNo = " + e.CommandArgument.ToString() + " ";
                }
            }
            sCommand = sCommand + sSQLUnion + ") AS TBL1 ";
            sCommand = sCommand + "WHERE BidDetailNo NOT IN (SELECT BidDetailNo FROM tblBACEvaluationDetails WHERE BidRefNo = " + Request.Form["BuyerBidForBac"] + " AND UserId=" + Session["UserId"] + ") ";
            sCommand = sCommand + "ORDER BY ItemName ";
            sCommand = sCommand + "END ELSE BEGIN ";
            sCommand = sCommand + "INSERT INTO tblBACEvaluationDetails (BidRefNo, BidDetailNo, BidTenderNo, SKU, ItemName, Chkd, Qty, UnitCost, ";
            sCommand = sCommand + "TotalCost, Ranking, fromDB, UnitMeasure, VendorName, Currenzy, UserId) ";
            sCommand = sCommand + "SELECT BidRefNo, BidDetailNo, CONVERT(VARCHAR, BidTenderNo) BidTenderNo, SKU, ItemName, Chkd, Qty, ";
            sCommand = sCommand + "CONVERT(NUMERIC(18, 2), UnitCost) UnitCost, CONVERT(NUMERIC(18, 2), UnitCost * Qty) TotalCost, Ranking, fromDB, UnitMeasure, VendorName, Currenzy, " + Session["UserId"].ToString() + " UserId FROM (";
            sCommand = sCommand + "SELECT 0 fromDB, " + e.CommandArgument.ToString() + " as BidDetailNo, 0 Chkd, ";
            sCommand = sCommand + "'' SKU, '" + e.CommandArgument.ToString() + "' ItemName, BidTenderNo = (select count(distinct vendorname) from tblBACEvaluationSummary t2 where t1.vendorname > t2.vendorname and t2.bidrefno=t1.bidrefno), 'PHP' Currenzy, " + Request.Form["BuyerBidForBac"] + " as BidRefNo, ";
            sCommand = sCommand + "0 AS TotalCost, ";
            sCommand = sCommand + "0 Qty, 'pc' UnitMeasure, 0 UnitCost, t1.VendorName, t1.VendorId, 0 Amount,  0 Discount, ";
            sCommand = sCommand + "0 DeliveryCost, 1 Status, 0 Ranking ";
            sCommand = sCommand + "FROM tblBACEvaluationSummary t1 ";
            sCommand = sCommand + "WHERE t1.BidRefNo = " + Request.Form["BuyerBidForBac"] + " AND VendorName not in (select VendorName from tblBACEvaluationSummary where (TechCompliance=1 or CommCompliance=1 or ContCompliance=2) AND BidRefNo = " + Request.Form["BuyerBidForBac"] + ") ";
            
            sSQLUnion = "";
            i = 0;
            foreach (RepeaterItem oItem in Repeater4.Items)
            {
                i++;
                //TextBox oTxt = ((TextBox)oItem.FindControl("txtVendorName"));
                DropDownList oTxt = ((DropDownList)oItem.FindControl("txtVendorName"));
                System.Web.UI.HtmlControls.HtmlInputHidden oTxt1 = ((System.Web.UI.HtmlControls.HtmlInputHidden)oItem.FindControl("fromDB"));
                if (oTxt1.Value == "0")
                {
                    sSQLUnion = sSQLUnion + " UNION ALL ";
                    sSQLUnion = sSQLUnion + "SELECT DISTINCT 0 FromDB, t2.BidDetailNo, 0 Chkd, t2.Item SKU, t2.DetailDesc ItemName, CONVERT(VARCHAR, '" + oTxt.SelectedValue.ToString() + "') BidTenderNo, t4.Currency Currenzy, ";
                    sSQLUnion = sSQLUnion + Request.Form["BuyerBidForBac"] + " BidRefNo, 0 TotalCost, t2.Qty, t2.UnitOfMeasure UnitMeasure, 0 UnitCost, '" + oTxt.Text + "' VendorName, ";
                    sSQLUnion = sSQLUnion + "0 VendorID, 0 Amount, 0 Discount, 0 DeliveryCost, 4 Status, 0 Ranking ";
                    sSQLUnion = sSQLUnion + "FROM tblBidItemDetails t2 ";
                    sSQLUnion = sSQLUnion + "INNER JOIN tblBidItems t4 ON t4.BidRefNo = t2.BidRefNo ";
                    sSQLUnion = sSQLUnion + "WHERE t2.BidRefNo = " + Request.Form["BuyerBidForBac"] + " ";
                    sSQLUnion = sSQLUnion + "AND t2.BidDetailNo = " + e.CommandArgument.ToString() + " ";
                }
            }
            sCommand = sCommand + sSQLUnion + ") AS TBL1 ";
            sCommand = sCommand + "WHERE BidDetailNo NOT IN (SELECT BidDetailNo FROM tblBACEvaluationDetails WHERE BidRefNo = " + Request.Form["BuyerBidForBac"] + " AND UserId=" + Session["UserId"] + ") ";
            sCommand = sCommand + "ORDER BY ItemName ";
            sCommand = sCommand + " END ";
            //Response.Write(sCommand);
            //LogHelper.TextLogHelper.Log(Server.MapPath("ebid.log"), sCommand);
            SqlHelper.ExecuteNonQuery(connstring, CommandType.Text, sCommand);

            foreach (GridViewRow oGrid in gvItems.Rows)
            {
                LinkButton oTxt = ((LinkButton)oGrid.FindControl("lblAddItem"));
                if (oTxt.CommandArgument.ToString() == e.CommandArgument.ToString()) oTxt.Text = "Edit Item";
            }
        
        } // DELETE ITEM ROW
        else if (e.CommandName.Equals("DelItemRow"))
        {
            //doBuyerBidForBac();
            sCommand = "DELETE FROM tblBACEvaluationDetails WHERE BidRefNo=" + Request.Form["BuyerBidForBac"] + " AND UserId=" + Session["UserId"] + " ";
            sCommand = sCommand + " AND BidDetailNo=" + e.CommandArgument.ToString();
            SqlHelper.ExecuteNonQuery(connstring, CommandType.Text, sCommand);

            // Update accumulated cost
            sCommand = "UPDATE tblBacBidItems SET AccumulativeCost = ISNULL((select sum(TotalCost) from tblBACEvaluationDetails WHERE BidRefNo=" + Request.Form["BuyerBidForBac"] + " ";
            sCommand = sCommand + "AND Chkd=1 AND UserId=" + Session["UserId"] + "),0) Where BidRefNo=" + Request.Form["BuyerBidForBac"];
            SqlHelper.ExecuteNonQuery(connstring, CommandType.Text, sCommand);

            doBuyerBidForBac();

            foreach (GridViewRow oGrid in gvItems.Rows)
            {
                LinkButton oTxt = ((LinkButton)oGrid.FindControl("lblAddItem"));
                if (oTxt.CommandArgument.ToString() == e.CommandArgument.ToString()) { oTxt.Text = "Add Item"; }
                TextBox oTxt2 = ((TextBox)oGrid.FindControl("lblPRNo"));
                //oTxt2.Text = "";
            }

        }
        else { 
        
        }

        // rebind commercial details
        lstCommDetails.DataSourceID = null;
        lstCommDetails.DataSourceID = "dsItems1";
        dsItems1.SelectCommand = "SELECT DISTINCT BidDetailNo, ItemName, SKU FROM tblBACEvaluationDetails WHERE BidRefNo = " + Request.Form["BuyerBidForBac"] + " AND UserId=" + Session["UserId"] + " AND BidDetailNo=" + e.CommandArgument.ToString() + " ORDER BY BidDetailNo, ItemName, SKU";

        //FindControl("lblAddItem").Visible = false;
        UpdateCommercialDetails();
        gvItems.DataBind();
    }

    

    protected void cbNatureOfSavings_DataBound(object sender, EventArgs e)
    {
        cbNatureOfSavings.Items.Insert(0, new ListItem("---- SELECT NATURE OF SAVINGS ----", "-1"));
    }

    protected void BACCheckBoxAttr()
    {
        Repeater oRpt;
        DataList lstCommDetails1 = (DataList)this.form1.FindControl("lstCommDetails");

        foreach (DataListItem oItem in lstCommDetails1.Items)
        {
            // award
            oRpt = (Repeater)oItem.FindControl("rptAward");
            foreach (RepeaterItem oRptItem in oRpt.Items)
            {
                if (oRptItem.ItemType == ListItemType.Item || oRptItem.ItemType == ListItemType.AlternatingItem)
                {
                    string sBidTender1 = ((TextBox)oRptItem.FindControl("txtBidTenderNo1")).Text;
                    TextBox oTxt = (TextBox)oRptItem.FindControl("txtAward");
                    System.Web.UI.HtmlControls.HtmlInputCheckBox oChk = (System.Web.UI.HtmlControls.HtmlInputCheckBox)oRptItem.FindControl("chkAward");
                    //oChk.Attributes.Add("onclick", "chkAward_Click('xxx');"); // ayaw gumana, try sa javascript
                    if (oTxt.Text == "1")
                    {
                        oChk.Checked = true;
                    }
                }
            }
        }
    }
    protected void rptAward1_OnItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            TextBox oTxt = (TextBox)e.Item.FindControl("txtAward");
            if (oTxt.Text == "1")
            {
                ((System.Web.UI.HtmlControls.HtmlInputCheckBox)e.Item.FindControl("chkAward")).Checked = true;
            }
        }
    }

    protected void RepeaterApprover_Load(object sender, EventArgs e)
    {
        iApproverCount = 0;
    }

    private string connstring = "";

    private bool SendEmailNotification()
    {
        //"Judith Mercado. Ponce" <email@globetel.com.ph>
        //"Consolacion G.. Lim" <email@globetel.com.ph>
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


        //GET BUYER EMAIL AS THE SENDER
        sCommand = "SELECT t1.BuyerFirstName + ' ' + t1.BuyerMidName + ' ' + t1.BuyerLastName  AS Name1, t1.EmailAdd FROM tblBuyers t1, tblBacBidItems t2  WHERE t1.BuyerID=t2.BuyerId AND t2.BidRefNo=" + Request.Form["BuyerBidForBac"];
        oReader = SqlHelper.ExecuteReader(connstring, CommandType.Text, sCommand);
        if (oReader.HasRows)
        {
            oReader.Read();
            fromName = oReader["Name1"].ToString();
            fromEmail = oReader["EmailAdd"].ToString();
            from = '"' + oReader["Name1"].ToString() + '"' + " <" + oReader["EmailAdd"].ToString() + ">";

        } oReader.Close();

        //GET PURCHASING EMAIL AS THE RECEPIENT
        sCommand = "SELECT t1.PurchasingID, t2.FirstName + ' ' + t2.MiddleName + ' ' + t2.LastName AS Name1, CONVERT(VARCHAR(17), t3.ApprovedDt_0, 113) ApprovedDt, t2.EmailAdd  FROM tblSupervisor t1, tblPurchasing t2, tblBacBidItems t3 WHERE t1.PurchasingID=t2.PurchasingID and t1.BuyerId = t3.buyerId AND t2.PurchasingID=t3.Approver_0 and t3.BidRefNo=" + Request.Form["BuyerBidForBac"];
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
        //Response.Write(CreateNotificationBody() + "<br>");
        //Response.Write(MailTemplate.GetTemplateLinkedResources(this) + "<br>");
        try
        {
            if (!MailHelper.SendEmail(MailTemplate.GetDefaultSMTPServer(),
                    from,
                    to,
                    subject,
                    CreateNotificationBody(),
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
    private string CreateNotificationBody()
    {
        StringBuilder sb = new StringBuilder();

        string sCommand;
        string BuyersName1 = "";
        string PurchasingName1 = "";
        string connstring = HttpContext.Current.Session["ConnectionString"].ToString();
        SqlDataReader oReader;

        sCommand = "SELECT t1.BuyerLastName + ', ' + t1.BuyerFirstName + ' ' + t1.BuyerMidName AS Name1 FROM tblBuyers t1, tblBacBidItems t2 ";
        sCommand = sCommand + "WHERE t1.BuyerID=t2.BuyerId AND t2.BidRefNo=" + Request.Form["BuyerBidForBac"];
        oReader = SqlHelper.ExecuteReader(connstring, CommandType.Text, sCommand);
        if (oReader.HasRows)
        {
            oReader.Read();
            BuyersName1 = oReader["Name1"].ToString();
        } oReader.Close();

        sCommand = "SELECT t1.PurchasingID, t2.LastName + ', ' + t2.FirstName + ' ' + t2.MiddleName AS Name1, CONVERT(VARCHAR(17), t3.ApprovedDt_0, 113) ApprovedDt ";
        sCommand = sCommand + "FROM tblSupervisor t1, tblPurchasing t2, tblBacBidItems t3 WHERE ";
        sCommand = sCommand + "t1.PurchasingID=t2.PurchasingID and t1.BuyerId = t3.buyerId AND t2.PurchasingID=t3.Approver_0 and t3.BidRefNo=" + Request.Form["BuyerBidForBac"];
        oReader = SqlHelper.ExecuteReader(connstring, CommandType.Text, sCommand);
        if (oReader.HasRows)
        {
            oReader.Read();
            PurchasingName1 = oReader["Name1"].ToString();
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
        sCommand = sCommand + "FROM tblBACEvaluationDetails WHERE BidRefNo=" + Request.Form["BuyerBidForBac"] + " AND Chkd=1";
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
        //contents = contents.Replace("[BidAwardedTo]", sTxt);

        //sb.Append("<tr><td align='right'><h5>" + DateTime.Now.ToLongDateString() + "</h5></td></tr>");
        sb.Append("<tr><td><p><strong>BAC for Approval</strong> <br> Sent: " + DateTime.Now.ToLongDateString() + "</p> <p>From: " + BuyersName1 + "<br><br> To: " + PurchasingName1 + "<br><br> Subject: " + Request.Form["ItemDescLbl"] + "<br><br> Dear Bid Award Approvers, <br><br> Re: Request for Bid Award Approval – <strong>" + Request.Form["ItemDescLbl"] + "</strong><br><br> This is to request for your Bid Award Approval of the ff:<br><br> <b>Bid Reference Number:</b> " + Request.Form["BuyerBidForBac"] + "<br> <b>Bid Event Name:</b> " + Request.Form["ItemDescLbl"] + "<br></p> " + sTxt + "  <p>Very truly yours,<br><br><br> <strong>e-Sourcing Procurement</strong></p><p>&nbsp;</p> <p><strong>Instructions:</strong></p> <ol> <li>Go to <a href='https://e-sourcing.Trans-Asia.com.ph/'>https://e-sourcing.Trans-Asia.com.ph</a></li> <li>Enter your Username and Password then  click Login</li> <li>Click Received Bid Events for Awarding</li> <li>Click Bid Events Name</li> <li>Review / Endorse / Approve Bid event  for Awarding</li> <li>Click Clarify if you have clarification  or click Approved to award Bid Events</li> </ol> Please do not reply to this auto-generated  message.&nbsp;</td></tr>");

        return MailTemplate.IntegrateBodyIntoTemplate(sb.ToString());
    }

}