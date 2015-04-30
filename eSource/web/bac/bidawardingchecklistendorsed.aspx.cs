using System;
using System.Collections;
using System.Data;
using System.Configuration;
using System.Collections;
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

public partial class web_buyerscreens_bidawardingchecklistendorsed : System.Web.UI.Page
{
    public float iTotal;
    int iApproverCount = 0;
    int iVendor;
    int iVendorCnt = 0, iAccreditation = 0, iTechComp = 0, iCommComp = 0, iContComp = 0, iPRating = 0;
    int iC0 = 0, iC1 = 0, iC2 = 0, iC3 = 0, iC4 = 0, iC5 = 0, iC6 = 0, iC7 = 0, iC8 = 0, iC9 = 0;
    int iQty = 0, iUnitMeasure = 0, iUnitCost = 0, iTotal1 = 0, iCurrency = 0, iRanking = 0, iAwarded = 0;
    string sSQLUnion;

    bool IsNumber(string text)
    {
        Regex regex = new Regex(@"^[-+]?[0-9]*\.?[0-9]+$");
        return regex.IsMatch(text);
    }

    void SaveToDB()
    {
        //string status1 = Request.Form["status"].ToString();
        //string connstring = HttpContext.Current.Session["ConnectionString"].ToString();
        //string sCommand;

        //// get buyer bid details
        //string sItemDesc = "";
        //string iSAP = "";
        //string cAmt = "0";
        //string sRefNo = "";
        //string dPRdate = DateTime.Now.Date.ToString();
        //string dReceived = DateTime.Now.Date.ToString();
        //string sPaymentTerm = "";

        //// reference number
        //sRefNo = Session["BuyerBacRefNo"].ToString();
        //// bid events description
        //sItemDesc = ItemDescLbl.Text;
        //// SAP PR number
        //iSAP = PRRefNoLbl.Text;
        //// bid amount
        //cAmt = EstItemValueLbl.Text;
        //// SAP PR date
        //dPRdate = PRDate.Text;
        //// date received
        //dReceived = DateApprovedLbl.Text;
        //// payment terms
        //sPaymentTerm = ((DropDownList)Form.FindControl("cbPaymentTerms")).SelectedValue.ToString();
        //// sourcing strategy
        //sCommand = "DELETE FROM tblBACSourcingStrategy WHERE BidRefNo=" + Session["BuyerBidForBac"] + " AND UserId=" + Session["UserId"];
        //SqlHelper.ExecuteNonQuery(connstring, CommandType.Text, sCommand);
        //foreach (string myCookie in Request.Cookies)
        //{
        //    if (Request.Cookies[myCookie].Name.Contains("myss_"))
        //    {
        //        sCommand = "INSERT INTO tblBACSourcingStrategy (BidRefNo, UserId, SourcingStrategy) VALUES(" + Session["BuyerBidForBac"] + ", " + Session["UserId"] + ", '" + Request.Cookies[myCookie].Value + "')";
        //        SqlHelper.ExecuteNonQuery(connstring, CommandType.Text, sCommand);
        //    }
        //}
        //// type of purchase
        //sCommand = "DELETE FROM tblBACTypeOfPurchase WHERE BidRefNo=" + Session["BuyerBidForBac"] + " AND UserId=" + Session["UserId"];
        //SqlHelper.ExecuteNonQuery(connstring, CommandType.Text, sCommand);
        //foreach (string myCookie in Request.Cookies)
        //{
        //    if (Request.Cookies[myCookie].Name.Contains("mytop_"))
        //    {
        //        sCommand = "INSERT INTO tblBACTypeOfPurchase (BidRefNo, UserId, TypeOfPurchase) VALUES(" + Session["BuyerBidForBac"] + ", " + Session["UserId"] + ", '" + Request.Cookies[myCookie].Value + "')";
        //        SqlHelper.ExecuteNonQuery(connstring, CommandType.Text, sCommand);
        //    }
        //}
        //// supply position
        //sCommand = "DELETE FROM tblBACSupplyPosition WHERE BidRefNo=" + Session["BuyerBidForBac"] + " AND UserId=" + Session["UserId"];
        //SqlHelper.ExecuteNonQuery(connstring, CommandType.Text, sCommand);
        //foreach (string myCookie in Request.Cookies)
        //{
        //    if (Request.Cookies[myCookie].Name.Contains("mysp_"))
        //    {
        //        sCommand = "INSERT INTO tblBACSupplyPosition (BidRefNo, UserId, SupplyPosition) VALUES(" + Session["BuyerBidForBac"] + ", " + Session["UserId"] + ", '" + Request.Cookies[myCookie].Value + "')";
        //        SqlHelper.ExecuteNonQuery(connstring, CommandType.Text, sCommand);
        //    }
        //}
        //// basis for awarding
        //sCommand = "DELETE FROM tblBACBasisForAwarding WHERE BidRefNo=" + Session["BuyerBidForBac"] + " AND UserId=" + Session["UserId"];
        //SqlHelper.ExecuteNonQuery(connstring, CommandType.Text, sCommand);
        //foreach (string myCookie in Request.Cookies)
        //{
        //    if (Request.Cookies[myCookie].Name.Contains("mybfa_"))
        //    {
        //        sCommand = "INSERT INTO tblBACBasisForAwarding (BidRefNo, UserId, BasisForAwarding) VALUES(" + Session["BuyerBidForBac"] + ", " + Session["UserId"] + ", '" + Request.Cookies[myCookie].Value + "')";
        //        SqlHelper.ExecuteNonQuery(connstring, CommandType.Text, sCommand);
        //    }
        //}
        //// nature of savings
        //sCommand = "DELETE FROM tblBACNatureOfSavings WHERE BidRefNo=" + Session["BuyerBidForBac"] + " AND UserId=" + Session["UserId"];
        //SqlHelper.ExecuteNonQuery(connstring, CommandType.Text, sCommand);
        //foreach (string myCookie in Request.Cookies)
        //{
        //    if (Request.Cookies[myCookie].Name.Contains("mynos_"))
        //    {
        //        sCommand = "INSERT INTO tblBACNatureOfSavings (BidRefNo, UserId, NatureOfSavings) VALUES(" + Session["BuyerBidForBac"] + ", " + Session["UserId"] + ", '" + Request.Cookies[myCookie].Value + "')";
        //        SqlHelper.ExecuteNonQuery(connstring, CommandType.Text, sCommand);
        //    }
        //}
        //// CRC
        //sCommand = "DELETE FROM tblBACCRCPO WHERE BidRefNo=" + Session["BuyerBidForBac"] + " AND UserId=" + Session["UserId"] + " AND CRCPO_Type='CRC'";
        //SqlHelper.ExecuteNonQuery(connstring, CommandType.Text, sCommand);
        //foreach (string myCookie in Request.Cookies)
        //{
        //    if (Request.Cookies[myCookie].Name.Contains("mycrc_"))
        //    {
        //        sCommand = "INSERT INTO tblBACCRCPO (BidRefNo, UserId, CRCPO_Type, CRCPO) VALUES(" + Session["BuyerBidForBac"] + ", " + Session["UserId"] + ", 'CRC', '" + Request.Cookies[myCookie].Value + "')";
        //        SqlHelper.ExecuteNonQuery(connstring, CommandType.Text, sCommand);
        //    }
        //}
        //// PO
        //sCommand = "DELETE FROM tblBACCRCPO WHERE BidRefNo=" + Session["BuyerBidForBac"] + " AND UserId=" + Session["UserId"] + " AND CRCPO_Type='PO'";
        //SqlHelper.ExecuteNonQuery(connstring, CommandType.Text, sCommand);
        //foreach (string myCookie in Request.Cookies)
        //{
        //    if (Request.Cookies[myCookie].Name.Contains("mypo1_"))
        //    {
        //        sCommand = "INSERT INTO tblBACCRCPO (BidRefNo, UserId, CRCPO_Type, CRCPO) VALUES(" + Session["BuyerBidForBac"] + ", " + Session["UserId"] + ", 'PO', '" + Request.Cookies[myCookie].Value + "')";
        //        SqlHelper.ExecuteNonQuery(connstring, CommandType.Text, sCommand);
        //    }
        //}

        //string ss_nobidders = "0";
        //if (IsNumber(Request.Form["ss_nobidders"].ToString()) == true) ss_nobidders = Request.Form["ss_nobidders"];
        //string ss_nobidsrcvd = "0";
        //if (IsNumber(Request.Form["ss_nobidsrcvd"]) == true) ss_nobidsrcvd = Request.Form["ss_nobidsrcvd"];
        //string ss_techcompliance = "0";
        //if (IsNumber(Request.Form["ss_techcompliance"]) == true) ss_techcompliance = Request.Form["ss_techcompliance"];

        //// add row to tblBacBidItems table
        //sCommand = "DELETE tblBacBidItems WHERE BidRefNo=" + Session["BuyerBidForBac"] + " AND BuyerID=" + Session["UserId"] + " AND (Status=0 OR Status=1)";
        //SqlHelper.ExecuteNonQuery(connstring, CommandType.Text, sCommand);

        //sCommand = "INSERT INTO tblBacBidItems ";
        //sCommand = sCommand + "(BidRefNo, Budgeted, CompanyID, ItemDesc, BuyerID, SAPPRNo, BidAmount, SAPPRDate, RcvdDate, ";
        //sCommand = sCommand + "Payment_Terms, SS_NoBidders, SS_NoBidsRcvd, SS_TechCompliance, Status) ";
        //sCommand = sCommand + "VALUES (";
        //sCommand = sCommand + Session["BuyerBidForBac"] + ",  " + Request.Form["Budgeted"] + ", " + Request.Form["CompanyId"] + ", '" + sItemDesc + "', " + Session["UserId"] + ", '" + iSAP + "', " + cAmt + ", '" + dPRdate + "', '" + dReceived + "', ";
        //sCommand = sCommand + sPaymentTerm + ", " + ss_nobidders + ", " + ss_nobidsrcvd + ", " + ss_techcompliance + ", " + status1 + ")";
        ////LogHelper.TextLogHelper.Log(Server.MapPath("ebid.log"), sCommand + "\\n");
        //SqlHelper.ExecuteNonQuery(connstring, CommandType.Text, sCommand);

        //// add row to tblBACExecutiveSummary table
        //sCommand = "DELETE tblBACExecutiveSummary WHERE BidRefNo=" + Session["BuyerBidForBac"] + " AND UserID=" + Session["UserId"];
        //SqlHelper.ExecuteNonQuery(connstring, CommandType.Text, sCommand);

        //sCommand = "INSERT INTO tblBACExecutiveSummary (BidRefNo, UserID, Comment) VALUES (";
        //sCommand = sCommand + Session["BuyerBidForBac"] + ", " + Session["UserId"] + ", '" + Request.Form["executive_summary"] + "')";
        //SqlHelper.ExecuteNonQuery(connstring, CommandType.Text, sCommand);

        //// add row to tblBACComments table
        //sCommand = "DELETE tblBACComments WHERE BidRefNo=" + Session["BuyerBidForBac"] + " AND UserID=" + Session["UserId"];
        //SqlHelper.ExecuteNonQuery(connstring, CommandType.Text, sCommand);

        //sCommand = "INSERT INTO tblBACComments (BidRefNo, UserID, Comment) VALUES (";
        //sCommand = sCommand + Session["BuyerBidForBac"] + ", " + Session["UserId"] + ", '" + Request.Form["remarks_comments"] + "')";
        //SqlHelper.ExecuteNonQuery(connstring, CommandType.Text, sCommand);

        //// bid evaluation summary
        //string sBidTenderNo = "", sVendorID = "", sVendorName = "", sTechnical = "", sCommercial = "", sContract = "";
        //string sPRating = "", sFromDB = "", sAccredited = "";
        //string sCritA = "", sCritB = "", sCritC = "", sCritD = "", sCritE = "", sCritF = "", sCritG = "", sCritH = "", sCritI = "", sCritJ = "";
        //string sCrit0 = "", sCrit1 = "", sCrit2 = "", sCrit3 = "", sCrit4 = "", sCrit5 = "", sCrit6 = "", sCrit7 = "", sCrit8 = "", sCrit9 = "";

        //sCommand = "DELETE FROM tblBACEvaluationSummary WHERE BidRefNo=" + Session["BuyerBidForBac"];
        //SqlHelper.ExecuteNonQuery(connstring, CommandType.Text, sCommand);

        //foreach (RepeaterItem rpItem in Repeater13.Items) // get checked items
        //{
        //    CheckBox chkbx = rpItem.FindControl("chkAwarded") as CheckBox;
        //    sVendorID = chkbx.Attributes["VendorID"].ToString();
        //    sBidTenderNo = "0";

        //    sVendorName = "";
        //    sTechnical = "";
        //    sCommercial = "";
        //    sContract = "";
        //    sPRating = "";
        //    sCritA = ""; sCritB = ""; sCritC = ""; sCritD = ""; sCritE = ""; sCritF = ""; sCritG = ""; sCritH = ""; sCritI = ""; sCritJ = "";
        //    sCrit0 = ""; sCrit1 = ""; sCrit2 = ""; sCrit3 = ""; sCrit4 = ""; sCrit5 = ""; sCrit6 = ""; sCrit7 = ""; sCrit8 = ""; sCrit9 = "";
        //    sFromDB = "0"; sAccredited = "0";

        //    foreach (RepeaterItem rpItem1 in Repeater4.Items) // get vendor name
        //    {
        //        TextBox txt1 = rpItem1.FindControl("txtVendorName") as TextBox;
        //        if (txt1.Attributes["VendorID"].ToString() == sVendorID)
        //        {
        //            sVendorName = txt1.Text;
        //            break;
        //        }
        //    }
        //    foreach (RepeaterItem rpItem1 in Repeater4.Items) // get fromDB
        //    {
        //        System.Web.UI.HtmlControls.HtmlInputHidden txt1 = rpItem1.FindControl("fromDB") as System.Web.UI.HtmlControls.HtmlInputHidden;
        //        if (txt1.Attributes["VendorID"].ToString() == sVendorID)
        //        {
        //            sFromDB = txt1.Value;
        //            break;
        //        }
        //    }
        //    foreach (RepeaterItem rpItem1 in Repeater100.Items) // get accreditation
        //    {
        //        System.Web.UI.HtmlControls.HtmlInputHidden txt1 = rpItem1.FindControl("txtAccreditation") as System.Web.UI.HtmlControls.HtmlInputHidden;
        //        if (txt1.Attributes["VendorID"].ToString() == sVendorID)
        //        {
        //            sAccredited = txt1.Value;
        //            break;
        //        }
        //    }
        //    foreach (RepeaterItem rpItem1 in RepeaterPRating.Items) // get performance rating
        //    {
        //        TextBox txt1 = rpItem1.FindControl("txtPRating") as TextBox;
        //        if (txt1.Attributes["VendorID"].ToString() == sVendorID)
        //        {
        //            sPRating = txt1.Text;
        //            break;
        //        }
        //    }
        //    foreach (RepeaterItem rpItem1 in Repeater5.Items) // get technical compliance
        //    {
        //        TextBox txt1 = rpItem1.FindControl("txtTechnical") as TextBox;
        //        if (txt1.Attributes["VendorID"].ToString() == sVendorID)
        //        {
        //            sTechnical = txt1.Text;
        //            break;
        //        }
        //    }
        //    foreach (RepeaterItem rpItem1 in Repeater6.Items) // get commercial compliance
        //    {
        //        TextBox txt1 = rpItem1.FindControl("txtCommercial") as TextBox;
        //        if (txt1.Attributes["VendorID"].ToString() == sVendorID)
        //        {
        //            sCommercial = txt1.Text;
        //            break;
        //        }
        //    }
        //    foreach (RepeaterItem rpItem1 in Repeater7.Items) // get contract compliance
        //    {
        //        TextBox txt1 = rpItem1.FindControl("txtContract") as TextBox;
        //        if (txt1.Attributes["VendorID"].ToString() == sVendorID)
        //        {
        //            sContract = txt1.Text;
        //            break;
        //        }
        //    }
        //    foreach (RepeaterItem rpItem1 in RepeaterC0.Items) // get criteria 0
        //    {
        //        sCritA = txtR0.Text;
        //        TextBox txt1 = rpItem1.FindControl("txtC0") as TextBox;
        //        if (txt1.Attributes["VendorID"].ToString() == sVendorID)
        //        {
        //            sCrit0 = txt1.Text;
        //            break;
        //        }
        //    }
        //    foreach (RepeaterItem rpItem1 in RepeaterC1.Items) // get criteria 1
        //    {
        //        sCritB = txtR1.Text;
        //        TextBox txt1 = rpItem1.FindControl("txtC1") as TextBox;
        //        if (txt1.Attributes["VendorID"].ToString() == sVendorID)
        //        {
        //            sCrit1 = txt1.Text;
        //            break;
        //        }
        //    }
        //    foreach (RepeaterItem rpItem1 in RepeaterC2.Items) // get criteria 2
        //    {
        //        sCritC = txtR2.Text;
        //        TextBox txt1 = rpItem1.FindControl("txtC2") as TextBox;
        //        if (txt1.Attributes["VendorID"].ToString() == sVendorID)
        //        {
        //            sCrit2 = txt1.Text;
        //            break;
        //        }
        //    }
        //    foreach (RepeaterItem rpItem1 in RepeaterC3.Items) // get criteria 3
        //    {
        //        sCritD = txtR3.Text;
        //        TextBox txt1 = rpItem1.FindControl("txtC3") as TextBox;
        //        if (txt1.Attributes["VendorID"].ToString() == sVendorID)
        //        {
        //            sCrit3 = txt1.Text;
        //            break;
        //        }
        //    }
        //    foreach (RepeaterItem rpItem1 in RepeaterC4.Items) // get criteria 4
        //    {
        //        sCritE = txtR4.Text;
        //        TextBox txt1 = rpItem1.FindControl("txtC4") as TextBox;
        //        if (txt1.Attributes["VendorID"].ToString() == sVendorID)
        //        {
        //            sCrit4 = txt1.Text;
        //            break;
        //        }
        //    }
        //    foreach (RepeaterItem rpItem1 in RepeaterC5.Items) // get criteria 5
        //    {
        //        sCritF = txtR5.Text;
        //        TextBox txt1 = rpItem1.FindControl("txtC5") as TextBox;
        //        if (txt1.Attributes["VendorID"].ToString() == sVendorID)
        //        {
        //            sCrit5 = txt1.Text;
        //            break;
        //        }
        //    }
        //    foreach (RepeaterItem rpItem1 in RepeaterC6.Items) // get criteria 6
        //    {
        //        sCritG = txtR6.Text;
        //        TextBox txt1 = rpItem1.FindControl("txtC6") as TextBox;
        //        if (txt1.Attributes["VendorID"].ToString() == sVendorID)
        //        {
        //            sCrit6 = txt1.Text;
        //            break;
        //        }
        //    }
        //    foreach (RepeaterItem rpItem1 in RepeaterC7.Items) // get criteria 7
        //    {
        //        sCritH = txtR7.Text;
        //        TextBox txt1 = rpItem1.FindControl("txtC7") as TextBox;
        //        if (txt1.Attributes["VendorID"].ToString() == sVendorID)
        //        {
        //            sCrit7 = txt1.Text;
        //            break;
        //        }
        //    }
        //    foreach (RepeaterItem rpItem1 in RepeaterC8.Items) // get criteria 8
        //    {
        //        sCritI = txtR8.Text;
        //        TextBox txt1 = rpItem1.FindControl("txtC8") as TextBox;
        //        if (txt1.Attributes["VendorID"].ToString() == sVendorID)
        //        {
        //            sCrit8 = txt1.Text;
        //            break;
        //        }
        //    }
        //    foreach (RepeaterItem rpItem1 in RepeaterC9.Items) // get criteria 9
        //    {
        //        sCritJ = txtR9.Text;
        //        TextBox txt1 = rpItem1.FindControl("txtC9") as TextBox;
        //        if (txt1.Attributes["VendorID"].ToString() == sVendorID)
        //        {
        //            sCrit9 = txt1.Text;
        //            break;
        //        }
        //    }

        //    sCommand = "INSERT INTO tblBACEvaluationSummary (BidRefNo, VendorID, VendorName, TechCompliance, CommCompliance, ContCompliance, ";
        //    sCommand = sCommand + "Criteria_0_name, Criteria_1_name, Criteria_2_name, Criteria_3_name, Criteria_4_name, Criteria_5_name, Criteria_6_name, Criteria_7_name, Criteria_8_name, Criteria_9_name, ";
        //    sCommand = sCommand + "Criteria_0_value, Criteria_1_value, Criteria_2_value, Criteria_3_value, Criteria_4_value, Criteria_5_value, Criteria_6_value, Criteria_7_value, Criteria_8_value, Criteria_9_value, ";
        //    sCommand = sCommand + "BidTenderNo, fromDB, Accredited, PerformanceRating";
        //    sCommand = sCommand + ") VALUES (";
        //    sCommand = sCommand + Session["BuyerBidForBac"] + ", '" + sVendorID + "', '" + sVendorName + "', '" + sTechnical + "', '" + sCommercial + "', '" + sContract + "', '";
        //    sCommand = sCommand + sCritA + "', '" + sCritB + "', '" + sCritC + "', '" + sCritD + "', '" + sCritE + "', '" + sCritF + "', '" + sCritG + "', '" + sCritH + "', '" + sCritI + "', '" + sCritJ + "', '";
        //    sCommand = sCommand + sCrit0 + "', '" + sCrit1 + "', '" + sCrit2 + "', '" + sCrit3 + "', '" + sCrit4 + "', '" + sCrit5 + "', '" + sCrit6 + "', '" + sCrit7 + "', '" + sCrit8 + "', '" + sCrit9 + "', ";
        //    sCommand = sCommand + sBidTenderNo + ", " + sFromDB + ", " + sAccredited + ", '" + sPRating + "')";
        //    SqlHelper.ExecuteNonQuery(connstring, CommandType.Text, sCommand);
        //}

        //UpdateCommercialDetails();

        //sCommand = "DELETE FROM tblBACEvaluationDetails WHERE BidRefNo=" + Session["BuyerBidForBac"];
        //SqlHelper.ExecuteNonQuery(connstring, CommandType.Text, sCommand);

        //sCommand = "INSERT INTO tblBACEvaluationDetails (BidRefNo, BidDetailNo, BidTenderNo, SKU, ItemName, Chkd, Qty, UnitCost, TotalCost, ";
        //sCommand = sCommand + "Ranking, FromDB, UnitMeasure, VendorName, Currenzy) ";
        //sCommand = sCommand + "SELECT BidRefNo, BidDetailNo, BidTenderNo, SKU, ItemName, Chkd, Qty, UnitCost, TotalCost, ";
        //sCommand = sCommand + "Ranking, FromDB, UnitMeasure, VendorName, Currenzy ";
        //sCommand = sCommand + "FROM tblBACEvaluationDetails_tmp WHERE BidRefNo=" + Session["BuyerBidForBac"];
        ////LogHelper.TextLogHelper.Log(Server.MapPath("ebid.log"), sCommand + "\\n");
        //SqlHelper.ExecuteNonQuery(connstring, CommandType.Text, sCommand);

        //sCommand = "DELETE FROM tblBACEvaluationDetails_tmp WHERE BidRefNo=" + Session["BuyerBidForBac"];
        //SqlHelper.ExecuteNonQuery(connstring, CommandType.Text, sCommand);

        //// redirect
        //if (status1 == "0")
        //    Response.Redirect("bacdrafts.aspx");
        //else
        //    Response.Redirect("bacendorsed.aspx");
    }

    void AttachMyFile()
    {
        //string connstring = HttpContext.Current.Session["ConnectionString"].ToString();
        //string sCommand, sCommand1;
        //SqlDataReader oReader;

        //// upload file
        //if (fileUpload.HasFile)
        //{
        //    string myFile = "";
        //    FileInfo fInfo = new FileInfo(fileUpload.PostedFile.FileName);
        //    string sContentType = fileUpload.PostedFile.ContentType;
        //    if (!FileUploadHelper.IsFileForbidden(fInfo.Extension))
        //    {
        //        int fileSize = (fileUpload.PostedFile.ContentLength + 512) / 1024;
        //        int maxFileSize = int.Parse(ConfigurationManager.AppSettings["MaxFileSize"].ToString());
        //        FileInfo delFile;
        //        DirectoryInfo oDir;

        //        if (fileSize < maxFileSize)
        //        {
        //            string original = fileUpload.FileName.ToString().Trim();
        //            string actual = FileUploadHelper.GetNewAlternativeFileName(Session[Constant.SESSION_USERID].ToString(), fInfo.Extension);
        //            try
        //            {
        //                sCommand = "SELECT * FROM tblBACSupportingDocuments WHERE BidRefNo=" + Session["BuyerBidForBac"] + " AND BuyerID=" + Session["UserId"] + " AND DocuName='" + Request.Form["SupportDocuAttached"] + "' AND OriginalFileName='" + original + "'";
        //                oReader = SqlHelper.ExecuteReader(connstring, CommandType.Text, sCommand);
        //                if (oReader.HasRows)
        //                {
        //                    oReader.Read();
        //                    sCommand = (string)oReader["ActualFileName"];
        //                    delFile = new FileInfo(Constant.FILEATTACHMENTSFOLDERDIR + Session["UserId"] + "\\BAC\\" + sCommand + ".gz");
        //                    if (delFile.Exists)
        //                    {
        //                        File.Delete(Constant.FILEATTACHMENTSFOLDERDIR + Session["UserId"] + "\\BAC\\" + sCommand + ".gz");
        //                    }
        //                }
        //                oReader.Close(); oReader = null;

        //                oDir = new DirectoryInfo(Constant.FILEATTACHMENTSFOLDERDIR + Session["UserId"]);
        //                if (oDir.Exists == false) oDir.Create();
        //                oDir = new DirectoryInfo(Constant.FILEATTACHMENTSFOLDERDIR + Session["UserId"] + "\\BAC");
        //                if (oDir.Exists == false) oDir.Create();
        //                fileUpload.SaveAs((Constant.FILEATTACHMENTSFOLDERDIR) + Session["UserId"] + "\\BAC\\" + actual);
        //                delFile = new FileInfo((Constant.FILEATTACHMENTSFOLDERDIR) + Session["UserId"] + "\\BAC\\" + actual);
        //                CompressFile(delFile);
        //                delFile.Delete();

        //                // upload file to other server
        //                /*System.Net.NetworkCredential oNC = new System.Net.NetworkCredential("Charles", "\\");
        //                WebClient myWebClient = new WebClient();
        //                myFile = delFile + ".gz";
        //                myWebClient.Credentials = oNC;
        //                FtpWebRequest reqObj = (FtpWebRequest)FtpWebRequest.Create("ftp://Charles-PC/ebidftp");
        //                reqObj.Method = WebRequestMethods.Ftp.UploadFile;
        //                reqObj.Credentials = oNC;
        //                FileStream streamObj = File.OpenRead(myFile);
        //                byte[] buffer = new byte[streamObj.Length + 1];
        //                streamObj.Read(buffer, 0, buffer.Length);
        //                streamObj.Close();
        //                streamObj = null;
        //                reqObj.GetRequestStream().Write(buffer, 0, buffer.Length);
        //                reqObj = null;*/

        //                // update table
        //                sCommand = "DELETE FROM tblBACSupportingDocuments WHERE BidRefNo=" + Session["BuyerBidForBac"] + " ";
        //                sCommand = sCommand + "AND BuyerID=" + Session["UserId"] + " AND OriginalFileName='" + original + "' AND DocuName='" + Request.Form["SupportDocuAttached"] + "'";

        //                sCommand1 = "INSERT INTO tblBACSupportingDocuments (BidRefNo, BuyerID, DocuName, OriginalFileName, ActualFileName, ContentType, DateUploaded, IsDetachable) VALUES (";
        //                sCommand1 = sCommand1 + Session["BuyerBidForBac"] + ", " + Session["UserId"] + ", '" + Request.Form["SupportDocuAttached"] + "', '";
        //                sCommand1 = sCommand1 + original + "', '" + actual + "', '" + sContentType + "', GETDATE(), 1)";
        //                //Response.Write(sCommand1);

        //                switch (Request.Form["SupportDocuAttached"])
        //                {
        //                    case "Approved_PR":
        //                        dsSDA_APR.DeleteCommand = sCommand;
        //                        dsSDA_APR.Delete();
        //                        dsSDA_APR.UpdateCommand = sCommand1;
        //                        dsSDA_APR.Update();
        //                        break;
        //                    case "Commercial_Evaluation":
        //                        dsSDA_CE.DeleteCommand = sCommand;
        //                        dsSDA_CE.Delete();
        //                        dsSDA_CE.UpdateCommand = sCommand1;
        //                        dsSDA_CE.Update();
        //                        break;
        //                    case "Approved_Business_Case":
        //                        dsSDA_ABC.DeleteCommand = sCommand;
        //                        dsSDA_ABC.Delete();
        //                        dsSDA_ABC.UpdateCommand = sCommand1;
        //                        dsSDA_ABC.Update();
        //                        break;
        //                    case "Negotiation_Results":
        //                        dsSDA_NR.DeleteCommand = sCommand;
        //                        dsSDA_NR.Delete();
        //                        dsSDA_NR.UpdateCommand = sCommand1;
        //                        dsSDA_NR.Update();
        //                        break;
        //                    case "Board_Resolution":
        //                        dsSDA_BR.DeleteCommand = sCommand;
        //                        dsSDA_BR.Delete();
        //                        dsSDA_BR.UpdateCommand = sCommand1;
        //                        dsSDA_BR.Update();
        //                        break;
        //                    case "Others":
        //                        dsSDA_Oth.DeleteCommand = sCommand;
        //                        dsSDA_Oth.Delete();
        //                        dsSDA_Oth.UpdateCommand = sCommand1;
        //                        dsSDA_Oth.Update();
        //                        break;
        //                    case "Technical_Evaluation":
        //                        dsSDA_TE.DeleteCommand = sCommand;
        //                        dsSDA_TE.Delete();
        //                        dsSDA_TE.UpdateCommand = sCommand1;
        //                        dsSDA_TE.Update();
        //                        break;
        //                }
        //                addAttachmentMsg.Text = myFile;
        //            }
        //            catch (Exception ex)
        //            {
        //                addAttachmentMsg.Text = "• File cannot be uploaded.<br />" + myFile + "<br />" + ex.ToString();
        //            }
        //        }
        //        else
        //        {
        //            addAttachmentMsg.Text = "• File size exceeds limit(" + maxFileSize + "KB).";
        //        }
        //        delFile = null;
        //        oDir = null;
        //    }
        //    else
        //    {
        //        addAttachmentMsg.Text = "• File cannot be uploaded. The file type is forbidden to be uploaded.";
        //    }
        //}
        //else
        //{
        //    addAttachmentMsg.Text = "• File cannot be uploaded.";
        //}
    }

    public static void CompressFile(FileInfo fi)
    {
        //// Get the stream of the source file.
        //using (FileStream inFile = fi.OpenRead())
        //{
        //    // Prevent compressing hidden and 
        //    // already compressed files.
        //    if ((File.GetAttributes(fi.FullName) & FileAttributes.Hidden) != FileAttributes.Hidden & fi.Extension != ".gz")
        //    {
        //        // Create the compressed file.
        //        using (FileStream outFile = File.Create(fi.FullName + ".gz"))
        //        {
        //            using (GZipStream Compress = new GZipStream(outFile, CompressionMode.Compress))
        //            {
        //                // Copy the source file into the compression stream.
        //                inFile.CopyTo(Compress);
        //            }
        //        }
        //    }
        //}
        //fi = null;
    }

    public static void Decompress(FileInfo fi)
    {
        //// Get the stream of the source file.
        //using (FileStream inFile = fi.OpenRead())
        //{
        //    // Get original file extension, for example
        //    // "doc" from report.doc.gz.
        //    string curFile = fi.FullName;
        //    string origName = curFile.Remove(curFile.Length - fi.Extension.Length);

        //    //Create the decompressed file.
        //    using (FileStream outFile = File.Create(origName))
        //    {
        //        using (GZipStream Decompress = new GZipStream(inFile, CompressionMode.Decompress))
        //        {
        //            // Copy the decompression stream into the output file.
        //            Decompress.CopyTo(outFile);
        //        }
        //    }
        //}
        //fi = null;
    }

    protected void doBuyerBidForBac()
    {
        //string sCommand;
        //DateTime date1;
        //ItemDescLbl.Text = "";
        //PRRefNoLbl.Text = "";
        //EstItemValueLbl.Text = "0";
        //PRDate.Text = String.Format("{0:yyyy-MMM-dd}", DateTime.Today.Date);
        //DateApprovedLbl.Text = String.Format("{0:yyyy-MMM-dd}", DateTime.Today.Date);
        //int i;

        //// main
        //sCommand = "SELECT * FROM tblBACBidItems WHERE BacRefNo=" + Session["BuyerBacRefNo"] + " AND Status=1";
        //string connstring = HttpContext.Current.Session["ConnectionString"].ToString();
        //SqlDataReader oReader = SqlHelper.ExecuteReader(connstring, CommandType.Text, sCommand);
        //if (oReader.HasRows)
        //{
        //    oReader.Read();
        //    ItemDescLbl.Text = oReader["ItemDesc"].ToString();
        //    PRRefNoLbl.Text = oReader["SAPPRNo"].ToString();
        //    EstItemValueLbl.Text = oReader["BidAmount"].ToString();
        //    date1 = DateTime.Parse(oReader["SAPPRDate"].ToString());
        //    PRDate.Text = String.Format("{0:yyyy-MMM-dd}", date1);
        //    date1 = DateTime.Parse(oReader["RcvdDate"].ToString());
        //    DateApprovedLbl.Text = String.Format("{0:yyyy-MMM-dd}", date1);
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
        //    ss_nobidders.Value = oReader["SS_NoBidders"].ToString();
        //    ss_nobidsrcvd.Value = oReader["SS_NoBidsRcvd"].ToString();
        //    ss_techcompliance.Value = oReader["SS_TechCompliance"].ToString();
        //}
        //oReader.Close();

        //// sourcing strategy
        //i = 0;
        //sCommand = "SELECT * FROM tblBACSourcingStrategy WHERE BidRefNo=" + Session["BuyerBidForBac"] + " AND UserId=" + Session["UserId"];
        //oReader = SqlHelper.ExecuteReader(connstring, CommandType.Text, sCommand);
        //if (oReader.HasRows)
        //{
        //    while (oReader.Read())
        //    {
        //        i++;
        //        ClientScript.RegisterStartupScript(this.GetType(), "ss_" + i, "<script language='Javascript'>AddTRCheckBoxFromDB('#mySourcingStrategy', 'ss', '" + oReader["SourcingStrategy"].ToString() + "');</script>");
        //    }
        //}
        //oReader.Close();

        //// type of purchase
        //i = 0;
        //sCommand = "SELECT * FROM tblBACTypeOfPurchase WHERE BidRefNo=" + Session["BuyerBidForBac"] + " AND UserId=" + Session["UserId"];
        //oReader = SqlHelper.ExecuteReader(connstring, CommandType.Text, sCommand);
        //if (oReader.HasRows)
        //{
        //    while (oReader.Read())
        //    {
        //        i++;
        //        ClientScript.RegisterStartupScript(this.GetType(), "top_" + i, "<script language='Javascript'>AddTRCheckBoxFromDB('#myTypeOfPurchase', 'top', '" + oReader["TypeOfPurchase"].ToString() + "');</script>");
        //    }
        //}
        //oReader.Close();

        //// supply position
        //i = 0;
        //sCommand = "SELECT * FROM tblBACSupplyPosition WHERE BidRefNo=" + Session["BuyerBidForBac"] + " AND UserId=" + Session["UserId"];
        //oReader = SqlHelper.ExecuteReader(connstring, CommandType.Text, sCommand);
        //if (oReader.HasRows)
        //{
        //    while (oReader.Read())
        //    {
        //        i++;
        //        ClientScript.RegisterStartupScript(this.GetType(), "sp_" + i, "<script language='Javascript'>AddTRCheckBoxFromDB('#mySupplyPosition', 'sp', '" + oReader["SupplyPosition"].ToString() + "');</script>");
        //    }
        //}
        //oReader.Close();

        //// basis for awarding
        //i = 0;
        //sCommand = "SELECT * FROM tblBACBasisForAwarding WHERE BidRefNo=" + Session["BuyerBidForBac"] + " AND UserId=" + Session["UserId"];
        //oReader = SqlHelper.ExecuteReader(connstring, CommandType.Text, sCommand);
        //if (oReader.HasRows)
        //{
        //    while (oReader.Read())
        //    {
        //        i++;
        //        ClientScript.RegisterStartupScript(this.GetType(), "bfa_" + i, "<script language='Javascript'>AddTRCheckBoxFromDB('#myBasisForAwarding', 'bfa', '" + oReader["BasisForAwarding"].ToString() + "');</script>");
        //    }
        //}
        //oReader.Close();

        //// executive summary
        //sCommand = "SELECT * FROM tblBACExecutiveSummary WHERE BidRefNo=" + Session["BuyerBidForBac"] + " AND UserId=" + Session["UserId"];
        //oReader = SqlHelper.ExecuteReader(connstring, CommandType.Text, sCommand);
        //if (oReader.HasRows)
        //{
        //    oReader.Read();
        //    executive_summary.InnerText = oReader["Comment"].ToString();
        //}
        //oReader.Close();

        //// comments
        //sCommand = "SELECT * FROM tblBACComments WHERE BidRefNo=" + Session["BuyerBidForBac"] + " AND UserId=" + Session["UserId"];
        //oReader = SqlHelper.ExecuteReader(connstring, CommandType.Text, sCommand);
        //if (oReader.HasRows)
        //{
        //    oReader.Read();
        //    remarks_comments.InnerText = oReader["Comment"].ToString();
        //}
        //oReader.Close();

        //bool bReadOnly;
        //if (BuyerBidForBac.SelectedIndex == 1) bReadOnly = false;
        //else bReadOnly = true;
        //TextBox lbl1 = (TextBox)Form.FindControl("EstItemValueLbl");
        //iTotal = iTotal + float.Parse(lbl1.Text);
        //lblTotalAll.Value = iTotal.ToString();
        //lbl1.ReadOnly = bReadOnly;
    }

    protected void ShowAttachments()
    {
        dsSDA_APR.SelectCommand = "SELECT * FROM tblBACSupportingDocuments WHERE BuyerID=" + Session["UserId"] + " AND BidRefNo=" + Session["BuyerBidForBac"] + " AND DocuName='Approved_PR' ORDER BY FileUploadID";
        dsSDA_APR.DataBind();
        dsSDA_CE.SelectCommand = "SELECT * FROM tblBACSupportingDocuments WHERE BuyerID=" + Session["UserId"] + " AND BidRefNo=" + Session["BuyerBidForBac"] + " AND DocuName='Commercial_Evaluation' ORDER BY FileUploadID";
        dsSDA_CE.DataBind();
        dsSDA_ABC.SelectCommand = "SELECT * FROM tblBACSupportingDocuments WHERE BuyerID=" + Session["UserId"] + " AND BidRefNo=" + Session["BuyerBidForBac"] + " AND DocuName='Approved_Business_Case' ORDER BY FileUploadID";
        dsSDA_ABC.DataBind();
        dsSDA_NR.SelectCommand = "SELECT * FROM tblBACSupportingDocuments WHERE BuyerID=" + Session["UserId"] + " AND BidRefNo=" + Session["BuyerBidForBac"] + " AND DocuName='Negotiation_Results' ORDER BY FileUploadID";
        dsSDA_NR.DataBind();
        dsSDA_BR.SelectCommand = "SELECT * FROM tblBACSupportingDocuments WHERE BuyerID=" + Session["UserId"] + " AND BidRefNo=" + Session["BuyerBidForBac"] + " AND DocuName='Board_Resolution' ORDER BY FileUploadID";
        dsSDA_BR.DataBind();
        dsSDA_Oth.SelectCommand = "SELECT * FROM tblBACSupportingDocuments WHERE BuyerID=" + Session["UserId"] + " AND BidRefNo=" + Session["BuyerBidForBac"] + " AND DocuName='Others' ORDER BY FileUploadID";
        dsSDA_Oth.DataBind();
        dsSDA_TE.SelectCommand = "SELECT * FROM tblBACSupportingDocuments WHERE BuyerID=" + Session["UserId"] + " AND BidRefNo=" + Session["BuyerBidForBac"] + " AND DocuName='Technical_Evaluation' ORDER BY FileUploadID";
        dsSDA_TE.DataBind();
    }

    protected void Page_LoadComplete(object sender, EventArgs e)
    {
        if (IsPostBack)
        {
            UpdateCommercialDetails();

            string control1 = Request.Form["__EVENTTARGET"];
            //Response.Write(control1);
            if (control1 == "") // save to database
            {
                SaveToDB();
            }
            else if (control1 == "lnkAttach")
            {
                //doBuyerBidForBac();
                AttachMyFile();
            }
            else if (control1 == "BuyerBidForBac")
            {
                //doBuyerBidForBac();
                ShowBidItemTenders();
            }
        }
        else
        {
            //lblAccCost.InnerText = "0.00";
            //lblNegDisc.InnerText = "0.00";
            //lblPercentSpend.InnerText = "0%";
            lblTotalAll.Value = "0.00";

            System.Web.UI.HtmlControls.HtmlInputHidden NewVendor;
            NewVendor = ((System.Web.UI.HtmlControls.HtmlInputHidden)Form.FindControl("txtNewVendor"));
            NewVendor.Value = "0";

            System.Web.UI.HtmlControls.HtmlInputHidden NewRow;
            NewRow = ((System.Web.UI.HtmlControls.HtmlInputHidden)Form.FindControl("txtNewRow"));
            NewRow.Value = "-1";

            doBuyerBidForBac();

            ShowBidItemTenders();
        }

        //// javascript selected indices
        ////cbPaymentTerms.Attributes.Add("onChange", "cbPaymentTerms_SelectedIndexChanged();");
        //cbSourcingStrategy.Attributes.Add("onChange", "cbSourcingStrategy_SelectedIndexChanged();");
        //cbTypeOfPurchase.Attributes.Add("onChange", "cbTypeOfPurchase_SelectedIndexChanged();");
        //cbSupplyPosition.Attributes.Add("onChange", "cbSupplyPosition_SelectedIndexChanged();");
        //cbBasisForAwarding.Attributes.Add("onChange", "cbBasisForAwarding_SelectedIndexChanged();");
        //cbNatureOfSavings.Attributes.Add("onChange", "cbNatureOfSavings_SelectedIndexChanged();");

        ShowAttachments();

        //// show approver
        ShowApprover();
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
        }
        oReader.Close();
    }

    protected void DeleteAttachment(string sOperation, string iFileUploadID)
    {
        //string connstring = HttpContext.Current.Session["ConnectionString"].ToString();
        //string sCommand;
        //SqlDataReader oReader;

        //sCommand = "SELECT * FROM tblBACSupportingDocuments WHERE FileUploadID=" + iFileUploadID;
        //oReader = SqlHelper.ExecuteReader(connstring, CommandType.Text, sCommand);
        //if (oReader.HasRows)
        //{
        //    oReader.Read();
        //    sCommand = (string)oReader["ActualFileName"];
        //    FileInfo delFile = new FileInfo(Constant.FILEATTACHMENTSFOLDERDIR + Session["UserId"] + "\\BAC\\" + sCommand + ".gz");
        //    if (delFile.Exists)
        //    {
        //        File.Delete(Constant.FILEATTACHMENTSFOLDERDIR + Session["UserId"] + "\\BAC\\" + sCommand + ".gz");
        //    }
        //    delFile = null;
        //}
        //oReader.Close();

        //sCommand = "DELETE FROM tblBACSupportingDocuments WHERE FileUploadID=" + iFileUploadID;
        //switch (sOperation)
        //{
        //    case "APR":
        //        dsSDA_APR.DeleteCommand = sCommand;
        //        dsSDA_APR.Delete();
        //        break;
        //    case "CE":
        //        dsSDA_CE.DeleteCommand = sCommand;
        //        dsSDA_CE.Delete();
        //        break;
        //    case "ABC":
        //        dsSDA_ABC.DeleteCommand = sCommand;
        //        dsSDA_ABC.Delete();
        //        break;
        //    case "NR":
        //        dsSDA_NR.DeleteCommand = sCommand;
        //        dsSDA_NR.Delete();
        //        break;
        //    case "BR":
        //        dsSDA_BR.DeleteCommand = sCommand;
        //        dsSDA_BR.Delete();
        //        break;
        //    case "Oth":
        //        dsSDA_Oth.DeleteCommand = sCommand;
        //        dsSDA_Oth.Delete();
        //        break;
        //    case "TE":
        //        dsSDA_TE.DeleteCommand = sCommand;
        //        dsSDA_TE.Delete();
        //        break;
        //}

    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //DateTime thisDate1 = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day);
        //lblPreparedDate.Text = thisDate1.ToString("MMMM dd, yyyy h:mm tt");

        //// force to show attachment
        //if (Request.Params["ShowAttachment"] is object)
        //{
        //    string connstring = HttpContext.Current.Session["ConnectionString"].ToString();
        //    string sCommand, sContentType;
        //    SqlDataReader oReader;

        //    addAttachmentMsg.Text = "";

        //    sCommand = "SELECT * FROM tblBACSupportingDocuments WHERE FileUploadID=" + Request.Params["ShowAttachment"];
        //    oReader = SqlHelper.ExecuteReader(connstring, CommandType.Text, sCommand);
        //    if (oReader.HasRows)
        //    {
        //        oReader.Read();
        //        sCommand = (string)oReader["ActualFileName"];
        //        sContentType = (string)oReader["ContentType"];
        //        string myBuyerID = (string)oReader["BuyerID"].ToString();
        //        FileInfo OrigFile = new FileInfo(Constant.FILEATTACHMENTSFOLDERDIR + myBuyerID + "\\BAC\\" + sCommand);
        //        FileInfo GZFile = new FileInfo(Constant.FILEATTACHMENTSFOLDERDIR + myBuyerID + "\\BAC\\" + sCommand + ".gz");
        //        if (GZFile.Exists)
        //        {
        //            try
        //            {
        //                Decompress(GZFile);
        //                //Response.Redirect("../../web/fileattachments/" + myBuyerID + "/BAC/" + sCommand);
        //                Response.Clear();
        //                Response.ContentType = sContentType;
        //                Response.AppendHeader("Content-Disposition", "attachment; filename=" + OrigFile.ToString());
        //                Response.TransmitFile(OrigFile.ToString(), 0, OrigFile.Length);
        //                //File.Delete(Constant.FILEATTACHMENTSFOLDERDIR + myBuyerID + "\\BAC\\" + sCommand);
        //                Response.Flush();
        //                Response.End();
        //            }
        //            finally
        //            {
        //                File.Delete(Constant.FILEATTACHMENTSFOLDERDIR + myBuyerID + "\\BAC\\" + sCommand);
        //            }
        //        }
        //        OrigFile = null;
        //        GZFile = null;
        //    }
        //    oReader.Close();
        //}
    }

    protected void BuyerBidForBac_DataBound(object sender, EventArgs e)
    {
        //BuyerBidForBac.Items.Insert(0, new ListItem("---- SELECT BID ----", "-1"));
        //BuyerBidForBac.Items.FindByValue(Session["BuyerBacRefNo"].ToString()).Selected = true;
        //BuyerBidForBac.Enabled = false;
    }

    protected void Repeater13_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            iAwarded++;
            ((CheckBox)e.Item.FindControl("chkAwarded")).Attributes.Add("VendorID", ((DataRowView)e.Item.DataItem)["VendorID"].ToString());
            ((CheckBox)e.Item.FindControl("chkAwarded")).Attributes.Add("onclick", "ComputeAccCost();");
            // restore session value
            if (Session["BAC_BES_Awarded" + iAwarded.ToString()] is Object)
                ((CheckBox)e.Item.FindControl("chkAwarded")).Checked = bool.Parse(Session["BAC_BES_Awarded" + iAwarded.ToString()].ToString());
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
    protected void Repeater11_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            iRanking++;
            ((TextBox)e.Item.FindControl("txtRanking")).Attributes.Add("VendorID", ((DataRowView)e.Item.DataItem)["VendorID"].ToString());
            // restore session value
            if (Session["BAC_BES_Ranking" + iRanking.ToString()] is Object)
                ((TextBox)e.Item.FindControl("txtRanking")).Text = Session["BAC_BES_Ranking" + iRanking.ToString()].ToString();
        }
    }
    protected void Repeater4_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            iVendorCnt++;
            ((TextBox)e.Item.FindControl("txtVendorName")).Attributes.Add("VendorID", ((DataRowView)e.Item.DataItem)["VendorID"].ToString());
            ((System.Web.UI.HtmlControls.HtmlInputHidden)e.Item.FindControl("fromDB")).Attributes.Add("VendorID", ((DataRowView)e.Item.DataItem)["VendorID"].ToString());
            if (((DataRowView)e.Item.DataItem)["fromDB"].ToString() == "1")
            {
                ((TextBox)e.Item.FindControl("txtVendorName")).ReadOnly = true;
            }
            else
            {
                ((TextBox)e.Item.FindControl("txtVendorName")).ReadOnly = false;
                // restore session value
                if (Session["BAC_BES_VendorName" + iVendorCnt.ToString()] is Object)
                    ((TextBox)e.Item.FindControl("txtVendorName")).Text = Session["BAC_BES_VendorName" + iVendorCnt.ToString()].ToString();
                if (Session["BAC_BES_fromDB" + iVendorCnt.ToString()] is Object)
                    ((System.Web.UI.HtmlControls.HtmlInputHidden)e.Item.FindControl("fromDB")).Value = Session["BAC_BES_fromDB" + iVendorCnt.ToString()].ToString();
            }
        }
    }
    protected void Repeater9_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            iQty++;
            ((TextBox)e.Item.FindControl("txtBidQty")).Attributes.Add("VendorID", ((DataRowView)e.Item.DataItem)["VendorID"].ToString());
            ((TextBox)e.Item.FindControl("txtBidQty")).Attributes.Add("fromDB", ((DataRowView)e.Item.DataItem)["fromDB"].ToString());
            if (((DataRowView)e.Item.DataItem)["fromDB"].ToString() == "1")
            {
                ((TextBox)e.Item.FindControl("txtBidQty")).ReadOnly = true;
            }
            else
            {
                ((TextBox)e.Item.FindControl("txtBidQty")).ReadOnly = false;
                // restore session value
                if (Session["BAC_BES_Qty" + iQty.ToString()] is Object)
                    ((TextBox)e.Item.FindControl("txtBidQty")).Text = Session["BAC_BES_Qty" + iQty.ToString()].ToString();
            }
        }
    }
    protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            iUnitMeasure++;
            ((TextBox)e.Item.FindControl("txtBidUnit")).Attributes.Add("VendorID", ((DataRowView)e.Item.DataItem)["VendorID"].ToString());
            if (((DataRowView)e.Item.DataItem)["fromDB"].ToString() == "1")
            {
                ((TextBox)e.Item.FindControl("txtBidUnit")).ReadOnly = true;
            }
            else
            {
                ((TextBox)e.Item.FindControl("txtBidUnit")).ReadOnly = false;
                // restore session value
                if (Session["BAC_BES_UnitMeasure" + iUnitMeasure.ToString()] is Object)
                    ((TextBox)e.Item.FindControl("txtBidUnit")).Text = Session["BAC_BES_UnitMeasure" + iUnitMeasure.ToString()].ToString();
            }
        }
    }
    protected void Repeater12_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            iUnitCost++;
            ((TextBox)e.Item.FindControl("txtBidCost")).Attributes.Add("VendorID", ((DataRowView)e.Item.DataItem)["VendorID"].ToString());
            if (((DataRowView)e.Item.DataItem)["fromDB"].ToString() == "1")
            {
                ((TextBox)e.Item.FindControl("txtBidCost")).ReadOnly = true;
            }
            else
            {
                ((TextBox)e.Item.FindControl("txtBidCost")).ReadOnly = false;
                // restore session value
                if (Session["BAC_BES_UnitCost" + iUnitCost.ToString()] is Object)
                    ((TextBox)e.Item.FindControl("txtBidCost")).Text = Session["BAC_BES_UnitCost" + iUnitCost.ToString()].ToString();
            }
        }
    }
    protected void Repeater10_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            iTotal++;
            ((System.Web.UI.HtmlControls.HtmlInputText)e.Item.FindControl("txtBidTotal")).Attributes.Add("VendorID", ((DataRowView)e.Item.DataItem)["VendorID"].ToString());
            ((System.Web.UI.HtmlControls.HtmlInputText)e.Item.FindControl("txtBidTotal")).Attributes.Add("value", ((DataRowView)e.Item.DataItem)["Amount"].ToString());
            if (((DataRowView)e.Item.DataItem)["fromDB"].ToString() == "1")
            {
                ((System.Web.UI.HtmlControls.HtmlInputText)e.Item.FindControl("txtBidTotal")).Disabled = true;
            }
            else
            {
                ((System.Web.UI.HtmlControls.HtmlInputText)e.Item.FindControl("txtBidTotal")).Disabled = false;
                // restore session value
                if (Session["BAC_BES_Total" + iTotal.ToString()] is Object)
                    ((System.Web.UI.HtmlControls.HtmlInputText)e.Item.FindControl("txtBidTotal")).Value = Session["BAC_BES_Total" + iTotal.ToString()].ToString();
            }
        }
    }
    protected void Repeater102_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            iCurrency++;
            ((TextBox)e.Item.FindControl("txtBidCurrency")).Attributes.Add("VendorID", ((DataRowView)e.Item.DataItem)["VendorID"].ToString());
            if (((DataRowView)e.Item.DataItem)["fromDB"].ToString() == "1")
            {
                ((TextBox)e.Item.FindControl("txtBidCurrency")).ReadOnly = true;
            }
            else
            {
                ((TextBox)e.Item.FindControl("txtBidCurrency")).ReadOnly = false;
                // restore session value
                if (Session["BAC_BES_Currency" + iCurrency.ToString()] is Object)
                    ((TextBox)e.Item.FindControl("txtBidCurrency")).Text = Session["BAC_BES_Currency" + iCurrency.ToString()].ToString();
            }
        }
    }
    protected void RepeaterC0_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            iC0++;
            ((TextBox)e.Item.FindControl("txtC0")).Attributes.Add("VendorID", ((DataRowView)e.Item.DataItem)["VendorID"].ToString());
            // restore session value
            if (Session["BAC_BES_C0" + iC0.ToString()] is Object)
                ((TextBox)e.Item.FindControl("txtC0")).Text = Session["BAC_BES_C0" + iC0.ToString()].ToString();
        }
    }
    protected void RepeaterC1_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            iC1++;
            ((TextBox)e.Item.FindControl("txtC1")).Attributes.Add("VendorID", ((DataRowView)e.Item.DataItem)["VendorID"].ToString());
            // restore session value
            if (Session["BAC_BES_C1" + iC1.ToString()] is Object)
                ((TextBox)e.Item.FindControl("txtC1")).Text = Session["BAC_BES_C1" + iC1.ToString()].ToString();
        }
    }
    protected void RepeaterC2_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            iC2++;
            ((TextBox)e.Item.FindControl("txtC2")).Attributes.Add("VendorID", ((DataRowView)e.Item.DataItem)["VendorID"].ToString());
            // restore session value
            if (Session["BAC_BES_C2" + iC2.ToString()] is Object)
                ((TextBox)e.Item.FindControl("txtC2")).Text = Session["BAC_BES_C2" + iC2.ToString()].ToString();
        }
    }
    protected void RepeaterC3_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            iC3++;
            ((TextBox)e.Item.FindControl("txtC3")).Attributes.Add("VendorID", ((DataRowView)e.Item.DataItem)["VendorID"].ToString());
            // restore session value
            if (Session["BAC_BES_C3" + iC3.ToString()] is Object)
                ((TextBox)e.Item.FindControl("txtC3")).Text = Session["BAC_BES_C3" + iC3.ToString()].ToString();
        }
    }
    protected void RepeaterC4_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            iC4++;
            ((TextBox)e.Item.FindControl("txtC4")).Attributes.Add("VendorID", ((DataRowView)e.Item.DataItem)["VendorID"].ToString());
            // restore session value
            if (Session["BAC_BES_C4" + iC4.ToString()] is Object)
                ((TextBox)e.Item.FindControl("txtC4")).Text = Session["BAC_BES_C4" + iC4.ToString()].ToString();
        }
    }
    protected void RepeaterC5_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            iC5++;
            ((TextBox)e.Item.FindControl("txtC5")).Attributes.Add("VendorID", ((DataRowView)e.Item.DataItem)["VendorID"].ToString());
            // restore session value
            if (Session["BAC_BES_C5" + iC5.ToString()] is Object)
                ((TextBox)e.Item.FindControl("txtC5")).Text = Session["BAC_BES_C5" + iC5.ToString()].ToString();
        }
    }
    protected void RepeaterC6_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            iC6++;
            ((TextBox)e.Item.FindControl("txtC6")).Attributes.Add("VendorID", ((DataRowView)e.Item.DataItem)["VendorID"].ToString());
            // restore session value
            if (Session["BAC_BES_C6" + iC6.ToString()] is Object)
                ((TextBox)e.Item.FindControl("txtC6")).Text = Session["BAC_BES_C6" + iC6.ToString()].ToString();
        }
    }
    protected void RepeaterC7_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            iC7++;
            ((TextBox)e.Item.FindControl("txtC7")).Attributes.Add("VendorID", ((DataRowView)e.Item.DataItem)["VendorID"].ToString());
            // restore session value
            if (Session["BAC_BES_C7" + iC7.ToString()] is Object)
                ((TextBox)e.Item.FindControl("txtC7")).Text = Session["BAC_BES_C7" + iC7.ToString()].ToString();
        }
    }
    protected void RepeaterC8_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            iC8++;
            ((TextBox)e.Item.FindControl("txtC8")).Attributes.Add("VendorID", ((DataRowView)e.Item.DataItem)["VendorID"].ToString());
            // restore session value
            if (Session["BAC_BES_C8" + iC8.ToString()] is Object)
                ((TextBox)e.Item.FindControl("txtC8")).Text = Session["BAC_BES_C8" + iC8.ToString()].ToString();
        }
    }
    protected void RepeaterC9_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            iC9++;
            ((TextBox)e.Item.FindControl("txtC9")).Attributes.Add("VendorID", ((DataRowView)e.Item.DataItem)["VendorID"].ToString());
            // restore session value
            if (Session["BAC_BES_C9" + iC9.ToString()] is Object)
                ((TextBox)e.Item.FindControl("txtC9")).Text = Session["BAC_BES_C9" + iC9.ToString()].ToString();
        }
    }
    protected void RepeaterApprover_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        iApproverCount++;
        ((Literal)e.Item.FindControl("lblApprover")).Text = "Approver " + iApproverCount.ToString();
    }
    protected void cbPaymentTerms_DataBound(object sender, EventArgs e)
    {
        //cbPaymentTerms.Items.Insert(0, new ListItem("---- SELECT PAYMENT TERMS ----", "-1"));
        //string sPayTerm = "";
        //string sCommand = "SELECT Payment_Terms FROM tblBACBidItems WHERE BacRefNo=" + Session["BuyerBidForBac"];
        //string connstring = HttpContext.Current.Session["ConnectionString"].ToString();
        //SqlDataReader oReader = SqlHelper.ExecuteReader(connstring, CommandType.Text, sCommand);
        //if (oReader.HasRows)
        //{
        //    oReader.Read();
        //    sPayTerm = oReader["Payment_Terms"].ToString();
        //}
        //oReader.Close();
        //foreach (ListItem oItem in cbPaymentTerms.Items)
        //{
        //    if (oItem.Value.ToString() == sPayTerm) oItem.Selected = true;
        //}
    }

    protected void cbSourcingStrategy_DataBound(object sender, EventArgs e)
    {
        //cbSourcingStrategy.Items.Insert(0, new ListItem("---- SELECT SOURCING STRATEGY ----", "-1"));
    }

    protected void cbTypeOfPurchase_DataBound(object sender, EventArgs e)
    {
        //cbTypeOfPurchase.Items.Insert(0, new ListItem("---- SELECT TYPE OF PURCHASE ----", "-1"));
    }

    protected void cbSupplyPosition_DataBound(object sender, EventArgs e)
    {
       //cbSupplyPosition.Items.Insert(0, new ListItem("---- SELECT SUPPLY POSITION ----", "-1"));
    }

    protected void cbBasisForAwarding_DataBound(object sender, EventArgs e)
    {
        //cbBasisForAwarding.Items.Insert(0, new ListItem("---- SELECT BASIS FOR AWARDING ----", "-1"));
    }

    protected void Repeater_SDA_APR_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        //if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        //{
        //    ((System.Web.UI.HtmlControls.HtmlInputCheckBox)e.Item.FindControl("chkSDA_APR")).Attributes.Add("onclick", "DeleteAttach('APR', '" + ((System.Web.UI.HtmlControls.HtmlInputCheckBox)e.Item.FindControl("chkSDA_APR")).ClientID + "','" + ((DataRowView)e.Item.DataItem)["FileUploadID"].ToString() + "');");
        //}
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
        //if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        //{
        //    ((System.Web.UI.HtmlControls.HtmlInputCheckBox)e.Item.FindControl("chkSDA_ABC")).Attributes.Add("onclick", "DeleteAttach('ABC', '" + ((System.Web.UI.HtmlControls.HtmlInputCheckBox)e.Item.FindControl("chkSDA_ABC")).ClientID + "','" + ((DataRowView)e.Item.DataItem)["FileUploadID"].ToString() + "');");
        //}
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
        //if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        //{
        //    ((System.Web.UI.HtmlControls.HtmlInputCheckBox)e.Item.FindControl("chkSDA_NR")).Attributes.Add("onclick", "DeleteAttach('NR', '" + ((System.Web.UI.HtmlControls.HtmlInputCheckBox)e.Item.FindControl("chkSDA_NR")).ClientID + "','" + ((DataRowView)e.Item.DataItem)["FileUploadID"].ToString() + "');");
        //}
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
        //if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        //{
        //    ((System.Web.UI.HtmlControls.HtmlInputCheckBox)e.Item.FindControl("chkSDA_BR")).Attributes.Add("onclick", "DeleteAttach('BR', '" + ((System.Web.UI.HtmlControls.HtmlInputCheckBox)e.Item.FindControl("chkSDA_BR")).ClientID + "','" + ((DataRowView)e.Item.DataItem)["FileUploadID"].ToString() + "');");
        //}
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
        //if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        //{
        //    ((System.Web.UI.HtmlControls.HtmlInputCheckBox)e.Item.FindControl("chkSDA_Oth")).Attributes.Add("onclick", "DeleteAttach('Oth', '" + ((System.Web.UI.HtmlControls.HtmlInputCheckBox)e.Item.FindControl("chkSDA_Oth")).ClientID + "','" + ((DataRowView)e.Item.DataItem)["FileUploadID"].ToString() + "');");
        //}
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
        //if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        //{
        //    ((System.Web.UI.HtmlControls.HtmlInputCheckBox)e.Item.FindControl("chkSDA_TE")).Attributes.Add("onclick", "DeleteAttach('TE', '" + ((System.Web.UI.HtmlControls.HtmlInputCheckBox)e.Item.FindControl("chkSDA_TE")).ClientID + "','" + ((DataRowView)e.Item.DataItem)["FileUploadID"].ToString() + "');");
        //}
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

    protected void Repeater100_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            DropDownList cbAcc = ((DropDownList)e.Item.FindControl("cbAccreditation"));
            cbAcc.Attributes.Add("VendorID", ((DataRowView)e.Item.DataItem)["VendorID"].ToString());
            cbAcc.SelectedIndex = int.Parse(((DataRowView)e.Item.DataItem)["Accredited"].ToString());
        }
    }

    protected void ShowBidItemTenders()
    {
        string sCommand;
        string connstring = HttpContext.Current.Session["ConnectionString"].ToString();
        SqlDataReader oReader;

        SaveEvaluationSummaryToSession();

        sCommand = "SELECT COUNT(*) AS count1 FROM tblBACEvaluationSummary WHERE BidRefNo = " + Session["BuyerBidForBac"] + " AND fromDB=0";
        oReader = SqlHelper.ExecuteReader(connstring, CommandType.Text, sCommand);
        if (oReader.HasRows)
        {
            oReader.Read();
            txtHasZeroID.Value = oReader["count1"].ToString();
            //iVendor = int.Parse(oReader["count1"].ToString());
        }

        sCommand = "";
        /*sCommand = sCommand + "SELECT 1 fromDB, t1.BidTenderNo, t3.Accredited, t4.Currency, CAST(((t1.Amount * t2.Qty) + (t1.DeliveryCost - (t1.Discount * t2.Qty))) AS Decimal(18,2)) AS BidPrice, ";
        sCommand = sCommand + "t2.Qty, t2.UnitOfMeasure, t2.UnitPrice, t3.VendorName, t3.VendorId, t1.DateSubmitted, t1.Amount, t1.Discount, t1.DeliveryCost, ";
        sCommand = sCommand + "t1.Remarks, t1.Warranty, t1.Status, t1.RenegotiationStatus, t4.RenegotiationDeadline, ";
        sCommand = sCommand + "CAST(t1.BidTenderNo AS varchar) + '|' + CAST(t3.VendorId AS varchar) + '|' + CAST(t4.BidRefNo AS varchar) AS BidTenRef ";
        sCommand = sCommand + "FROM tblBidTenders t1 INNER JOIN tblBidItemDetails t2 ON t2.BidDetailNo = t1.BidDetailNo ";
        sCommand = sCommand + "INNER JOIN tblVendors t3 ON t3.VendorId = t1.VendorId INNER JOIN tblBidItems t4 ON t4.BidRefNo = t2.BidRefNo ";
        sCommand = sCommand + "WHERE t2.BidRefNo = " + Session["BuyerBidForBac"] + " AND t1.Status > 0 ";
        sCommand = sCommand + "UNION ALL ";*/
        sCommand = sCommand + "SELECT * FROM tblBACEvaluationSummary ";
        sCommand = sCommand + "WHERE BidRefNo = " + Session["BuyerBidForBac"];

        //Response.Write(sCommand);
        dsBidItemTendersSummary.SelectCommand = sCommand;
        dsBidItemTendersSummary.DataBind();

        int iCnt;

        // compliances and performance rating
        iCnt = 0;
        sCommand = "SELECT TechCompliance, CommCompliance, ContCompliance, PerformanceRating FROM tblBACEvaluationSummary WHERE BidRefNo=" + Session["BuyerBidForBac"] + " ORDER BY ID ASC";
        oReader = SqlHelper.ExecuteReader(connstring, CommandType.Text, sCommand);
        if (oReader.HasRows)
        {
            while (oReader.Read())
            {
                iCnt++;
                Session.Add("BAC_BES_TechComp" + iCnt.ToString(), oReader["TechCompliance"].ToString());
                Session.Add("BAC_BES_CommComp" + iCnt.ToString(), oReader["CommCompliance"].ToString());
                Session.Add("BAC_BES_ContComp" + iCnt.ToString(), oReader["ContCompliance"].ToString());
                Session.Add("BAC_BES_PRating" + iCnt.ToString(), oReader["PerformanceRating"].ToString());
            }
        }
        oReader.Close();
    }

    protected void SaveEvaluationSummaryToSession()
    {
        //int i;
        //if (iVendor > 0)
        //{
        //    string arr = "";

        //    // remove all BAC sessions first
        //    foreach (string sItem in Session.Contents)
        //        if (sItem.Contains("BAC_BES")) arr = arr + sItem + ";";
        //    char[] mySeparator = new char[] { ';' };
        //    string[] arr1 = arr.Split(mySeparator);
        //    foreach (string arrStr in arr1)
        //        Session.Remove(arrStr);
        //    // store vendor name to session
        //    i = 0;
        //    foreach (RepeaterItem oItem in Repeater4.Items)
        //    {
        //        i++;
        //        TextBox oTxt = ((TextBox)oItem.FindControl("txtVendorName"));
        //        Session.Add("BAC_BES_VendorName" + i.ToString(), oTxt.Text);
        //        System.Web.UI.HtmlControls.HtmlInputHidden oTxt1 = ((System.Web.UI.HtmlControls.HtmlInputHidden)oItem.FindControl("fromDB"));
        //        Session.Add("BAC_BES_fromDB" + i.ToString(), oTxt1.Value);
        //    }
        //    // store accreditation to session
        //    i = 0;
        //    foreach (RepeaterItem oItem in Repeater100.Items)
        //    {
        //        i++;
        //        DropDownList oTxt = ((DropDownList)oItem.FindControl("cbAccreditation"));
        //        Session.Add("BAC_BES_Accreditation" + i.ToString(), oTxt.SelectedIndex);
        //    }
        //    // store performance to session
        //    i = 0;
        //    foreach (RepeaterItem oItem in RepeaterPRating.Items)
        //    {
        //        i++;
        //        TextBox oTxt = ((TextBox)oItem.FindControl("txtPRating"));
        //        Session.Add("BAC_BES_PRating" + i.ToString(), oTxt.Text);
        //    }
        //    // store technical compliance to session
        //    i = 0;
        //    foreach (RepeaterItem oItem in Repeater5.Items)
        //    {
        //        i++;
        //        TextBox oTxt = ((TextBox)oItem.FindControl("txtTechnical"));
        //        Session.Add("BAC_BES_TechComp" + i.ToString(), oTxt.Text);
        //    }
        //    // store commercial compliance to session
        //    i = 0;
        //    foreach (RepeaterItem oItem in Repeater6.Items)
        //    {
        //        i++;
        //        TextBox oTxt = ((TextBox)oItem.FindControl("txtCommercial"));
        //        Session.Add("BAC_BES_CommComp" + i.ToString(), oTxt.Text);
        //    }
        //    // store contract compliance to session
        //    i = 0;
        //    foreach (RepeaterItem oItem in Repeater7.Items)
        //    {
        //        i++;
        //        TextBox oTxt = ((TextBox)oItem.FindControl("txtContract"));
        //        Session.Add("BAC_BES_ContComp" + i.ToString(), oTxt.Text);
        //    }
        //    // store criteria to session
        //    for (var j = 0; j < 10; j++)
        //    {
        //        i = 0;
        //        Repeater oRepeater = ((Repeater)Form.FindControl("RepeaterC" + j));
        //        if (oRepeater is object)
        //        {
        //            foreach (RepeaterItem oItem in oRepeater.Items)
        //            {
        //                i++;
        //                TextBox oTxt = ((TextBox)oItem.FindControl("txtC" + j));
        //                Session.Add("BAC_BES_C" + j + i.ToString(), oTxt.Text);
        //            }
        //        }
        //    }
        //}
    }

    protected void dsApprover_Load(object sender, EventArgs e)
    {
        dsApprover.SelectCommand = " SELECT t1.BACId, t1.LastName + ', ' + t1.FirstName + ' ' + t1.MiddleName AS Name1, t1.ApprovingMaxLimit FROM tblBidAwardingCommittee t1 JOIN tblUsers t2 ON (t1.BACId=t2.UserId AND t2.IsAuthenticated=1) WHERE t1.ApprovingMaxLimit >= " + lblTotalAll.Value.ToString() + " ORDER BY t1.ApprovingMaxLimit ASC";
        dsApprover.DataBind();
    }

    protected void RepeaterPRating_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            iPRating++;
            ((TextBox)e.Item.FindControl("txtPRating")).Attributes.Add("VendorID", ((DataRowView)e.Item.DataItem)["VendorID"].ToString());
            ((TextBox)e.Item.FindControl("txtPRating")).Attributes.Add("VendorID", ((DataRowView)e.Item.DataItem)["VendorID"].ToString());
            // restore session value
            if (Session["BAC_BES_PRating" + iPRating.ToString()] is Object)
            {
                ((TextBox)e.Item.FindControl("txtPRating")).Text = Session["BAC_BES_PRating" + iPRating.ToString()].ToString();
            }
        }
    }

    protected void dsBidItemTendersSummary_Load(object sender, EventArgs e)
    {
        //string control1 = Request.Form["__EVENTTARGET"];
        //if (control1 == "NewVendor")
        //{
        //    string sCommand, sCommand1;
        //    System.Web.UI.HtmlControls.HtmlInputHidden NewVendor;
        //    NewVendor = ((System.Web.UI.HtmlControls.HtmlInputHidden)Form.FindControl("txtNewVendor"));
        //    iVendor = int.Parse(NewVendor.Value);

        //    int i = 0;
        //    sCommand1 = "";
        //    while (true)
        //    {
        //        if (i == iVendor)
        //        {
        //            break;
        //        }
        //        else
        //        {
        //            i++;
        //            sCommand1 = sCommand1 + "UNION ALL ";
        //            sCommand1 = sCommand1 + "SELECT TOP 1 0 fromDB, 2 Accredited, '' VendorName, 0 VendorId, 1 Status, 0 BidTenderNo ";
        //            sCommand1 = sCommand1 + "FROM tblBidTenders ";
        //        }
        //    }

        //    sCommand = "SELECT DISTINCT 1 fromDB, t3.Accredited, t3.VendorName, t3.VendorId, t1.Status, t1.BidTenderNo ";
        //    sCommand = sCommand + "FROM tblBidTenders t1 INNER JOIN tblBidItemDetails t2 ON t2.BidDetailNo = t1.BidDetailNo ";
        //    sCommand = sCommand + "INNER JOIN tblVendors t3 ON t3.VendorId = t1.VendorId INNER JOIN tblBidItems t4 ON t4.BidRefNo = t2.BidRefNo ";
        //    sCommand = sCommand + "WHERE t2.BidRefNo = @BidRefNo AND t1.Status > 0 ";

        //    sCommand = sCommand + sCommand1;
        //    //Response.Write(sCommand);
        //    dsBidItemTendersSummary.SelectCommand = sCommand;
        //    dsBidItemTendersSummary.DataBind();
        //}
    }

    protected void UpdateCommercialDetails()
    {
        
    }

    protected void cbNatureOfSavings_DataBound(object sender, EventArgs e)
    {
        //cbNatureOfSavings.Items.Insert(0, new ListItem("---- SELECT NATURE OF SAVINGS ----", "-1"));
    }

    protected void gvCommDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    HtmlInputHidden oHid = ((HtmlInputHidden)e.Row.FindControl("Chkd"));
        //    if (oHid.Value == "1")
        //    {
        //        ((CheckBox)e.Row.FindControl("chkAward")).Checked = true;
        //    }
        //}
    }

    protected void lstCommDetails_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        Repeater oRpt;

        foreach (DataListItem oItem in lstCommDetails.Items)
        {
            // award
            oRpt = (Repeater)oItem.FindControl("rptAward");
            foreach (RepeaterItem oRptItem in oRpt.Items)
            {
                if (oRptItem.ItemType == ListItemType.Item || oRptItem.ItemType == ListItemType.AlternatingItem)
                {
                    string sBidTender1 = ((TextBox)oRptItem.FindControl("txtBidTenderNo1")).Text;
                    TextBox oChk = (TextBox)oRptItem.FindControl("txtAward");
                    ((CheckBox)oRptItem.FindControl("chkAward")).Checked = false;
                    if (oChk.Text == "1")
                        ((CheckBox)oRptItem.FindControl("chkAward")).Checked = true;
                }
            }
        }
    }
}