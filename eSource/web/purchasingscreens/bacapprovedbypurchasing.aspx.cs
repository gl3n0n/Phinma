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
        if (oReader.HasRows)
        {
            oReader.Read();
            string sBudgeted = oReader["Budgeted"].ToString();
            switch (sBudgeted)
            {
                case "0": UnBudgeted.Checked = true; break;
                case "1": Budgeted.Checked = true; break;
            }
            string sCompanyID = oReader["CompanyID"].ToString();
            switch (sCompanyID)
            {
                case "0": CompanyIdGT.Checked = true; break;
                case "1": CompanyIdIC.Checked = true; break;
                case "2": CompanyIdGXI.Checked = true; break;
                case "3": CompanyIdEGG.Checked = true; break;
            }
        }
        oReader.Close();

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
        dsSDA_APR.SelectCommand = "SELECT * FROM tblBACSupportingDocuments WHERE BuyerID=" + Session["BuyerBuyerId"].ToString() + " AND BidRefNo=" + Session["BuyerBidForBac"].ToString() + " AND DocuName='Approved_PR' ORDER BY FileUploadID";
        dsSDA_APR.DataBind();
        dsSDA_CE.SelectCommand = "SELECT * FROM tblBACSupportingDocuments WHERE BuyerID=" + Session["BuyerBuyerId"].ToString() + " AND BidRefNo=" + Session["BuyerBidForBac"].ToString() + " AND DocuName='Commercial_Evaluation' ORDER BY FileUploadID";
        dsSDA_CE.DataBind();
        dsSDA_ABC.SelectCommand = "SELECT * FROM tblBACSupportingDocuments WHERE BuyerID=" + Session["BuyerBuyerId"].ToString() + " AND BidRefNo=" + Session["BuyerBidForBac"].ToString() + " AND DocuName='Approved_Business_Case' ORDER BY FileUploadID";
        dsSDA_ABC.DataBind();
        dsSDA_NR.SelectCommand = "SELECT * FROM tblBACSupportingDocuments WHERE BuyerID=" + Session["BuyerBuyerId"].ToString() + " AND BidRefNo=" + Session["BuyerBidForBac"].ToString() + " AND DocuName='Negotiation_Results' ORDER BY FileUploadID";
        dsSDA_NR.DataBind();
        dsSDA_BR.SelectCommand = "SELECT * FROM tblBACSupportingDocuments WHERE BuyerID=" + Session["BuyerBuyerId"].ToString() + " AND BidRefNo=" + Session["BuyerBidForBac"].ToString() + " AND DocuName='Board_Resolution' ORDER BY FileUploadID";
        dsSDA_BR.DataBind();
        dsSDA_Oth.SelectCommand = "SELECT * FROM tblBACSupportingDocuments WHERE BuyerID=" + Session["BuyerBuyerId"].ToString() + " AND BidRefNo=" + Session["BuyerBidForBac"].ToString() + " AND DocuName='Others' ORDER BY FileUploadID";
        dsSDA_Oth.DataBind();
        dsSDA_TE.SelectCommand = "SELECT * FROM tblBACSupportingDocuments WHERE BuyerID=" + Session["BuyerBuyerId"].ToString() + " AND BidRefNo=" + Session["BuyerBidForBac"].ToString() + " AND DocuName='Technical_Evaluation' ORDER BY FileUploadID";
        dsSDA_TE.DataBind();
    }

    protected void Page_LoadComplete(object sender, EventArgs e)
    {
        if (IsPostBack)
        {
            string control1 = Request.Form["__EVENTTARGET"];
            //Response.Write(control1);
            if (control1 == "BuyerBidForBac")
            {
                ShowBidItemTenders();
            }
            else if (control1 == "Clarify")
            {
                doClarify(Request.Form["__EVENTARGUMENT"]);
            }
            else if (control1 == "Approve")
            {
                doApprove();
            }
        }
        else
        {
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

        ShowAttachments();
        dsApprover.SelectCommand = "SELECT t1.BACId, t1.LastName + ', ' + t1.FirstName + ' ' + t1.MiddleName AS Name1, t1.ApprovingMaxLimit FROM tblBidAwardingCommittee t1 JOIN tblUsers t2 ON (t1.BACId=t2.UserId AND t2.IsAuthenticated=1) WHERE t1.ApprovingMaxLimit >= " + lblTotalAll.Value.ToString() + " ORDER BY t1.ApprovingMaxLimit ASC";
        dsApprover.DataBind();

        // show approver
        ShowApprover();
    }

    protected void doClarify(string ToUserID)
    {
        string connstring = HttpContext.Current.Session["ConnectionString"].ToString();
        string sCommand;

        // add row to tblBACComments table
        sCommand = "INSERT INTO tblBACClarifications (BidRefNo, FrUserId, Comment, ToUserId, DatePosted) VALUES (";
        sCommand = sCommand + Session["BuyerBidForBac"].ToString() + ", " + Session["UserId"] + ", '" + Request.Form["txtClarify"] + "', " + ToUserID + ", GETDATE())";
        SqlHelper.ExecuteNonQuery(connstring, CommandType.Text, sCommand);

        // update tblBacBidItems table
        sCommand = "UPDATE tblBacBidItems SET Status=2, ClarifyDt_" + Session["ApproverNumber"] + "=GETDATE() WHERE BacRefNo=" + Session["BuyerBacRefNo"];
        SqlHelper.ExecuteNonQuery(connstring, CommandType.Text, sCommand);

        Response.Redirect("bacapprovedpurchasing.aspx");
    }

    protected void doApprove()
    {
        string connstring = HttpContext.Current.Session["ConnectionString"].ToString();
        string sCommand;

        // add row to tblBACComments table
        sCommand = "INSERT INTO tblBACClarifications (BidRefNo, FrUserId, Comment, ToUserId, DatePosted) VALUES (";
        sCommand = sCommand + Session["BuyerBidForBac"].ToString() + ", " + Session["UserId"] + ", '" + Request.Form["txtClarify"] + "', 0 , GETDATE())";
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
        sCommand = "UPDATE tblBacBidItems SET Status=" + myStat + ", ApprovedDt_" + Session["ApproverNumber"] + "=GETDATE(), ApprovedBy_" + Session["ApproverNumber"] + " = " + Session["UserId"] + " WHERE BacRefNo=" + Session["BuyerBacRefNo"];
        SqlHelper.ExecuteNonQuery(connstring, CommandType.Text, sCommand);

        Response.Redirect("bacapprovedpurchasing.aspx");
    }

    protected void ShowApprover()
    {
        string connstring = HttpContext.Current.Session["ConnectionString"].ToString();
        string sCommand;
        SqlDataReader oReader;

        // buyer
        sCommand = "SELECT BuyerLastName + ', ' + BuyerFirstName + ' ' + BuyerMidName AS Name1 FROM tblBuyers WHERE BuyerID=" + Session["BuyerBuyerId"].ToString();
        oReader = SqlHelper.ExecuteReader(connstring, CommandType.Text, sCommand);
        if (oReader.HasRows)
        {
            oReader.Read();
            lblBuyer.Text = oReader["Name1"].ToString();
        }
        oReader.Close();

        // approver 4: purchasing
        sCommand = "SELECT t1.PurchasingID, t2.LastName + ', ' + t2.FirstName + ' ' + t2.MiddleName AS Name1 FROM tblSupervisor t1 ";
        sCommand = sCommand + "INNER JOIN tblPurchasing t2 ON t1.PurchasingID=t2.PurchasingID WHERE BuyerID=" + Session["BuyerBuyerId"].ToString();
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
        string connstring = HttpContext.Current.Session["ConnectionString"].ToString();
        string sCommand;
        SqlDataReader oReader;

        sCommand = "SELECT * FROM tblBACSupportingDocuments WHERE FileUploadID=" + iFileUploadID;
        oReader = SqlHelper.ExecuteReader(connstring, CommandType.Text, sCommand);
        if (oReader.HasRows)
        {
            oReader.Read();
            sCommand = (string)oReader["ActualFileName"];
            FileInfo delFile = new FileInfo(Constant.FILEATTACHMENTSFOLDERDIR + Session["BuyerBuyerId"].ToString() + "\\BAC\\" + sCommand + ".gz");
            if (delFile.Exists)
            {
                File.Delete(Constant.FILEATTACHMENTSFOLDERDIR + Session["BuyerBuyerId"].ToString() + "\\BAC\\" + sCommand + ".gz");
            }
            delFile = null;
        }
        oReader.Close();

        sCommand = "DELETE FROM tblBACSupportingDocuments WHERE FileUploadID=" + iFileUploadID;
        switch (sOperation)
        {
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
        }

    }

    protected void Page_Load(object sender, EventArgs e)
    {
        DateTime thisDate1 = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day);
        lblPreparedDate.Text = thisDate1.ToString("MMMM dd, yyyy h:mm tt");

        // force to show attachment
        if (Request.Params["ShowAttachment"] is object)
        {
            string connstring = HttpContext.Current.Session["ConnectionString"].ToString();
            string sCommand, sContentType;
            SqlDataReader oReader;

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
                OrigFile = null;
                GZFile = null;
            }
            oReader.Close();
        }
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
            ((TextBox)e.Item.FindControl("txtBidQty")).ReadOnly = true;
            if (((DataRowView)e.Item.DataItem)["fromDB"].ToString() == "0")
            {
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
            ((TextBox)e.Item.FindControl("txtBidUnit")).ReadOnly = true;
            if (((DataRowView)e.Item.DataItem)["fromDB"].ToString() == "0")
            {
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
            ((TextBox)e.Item.FindControl("txtBidCost")).ReadOnly = true;
            if (((DataRowView)e.Item.DataItem)["fromDB"].ToString() == "0")
            {
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
            ((System.Web.UI.HtmlControls.HtmlInputText)e.Item.FindControl("txtBidTotal")).Disabled = true;
            if (((DataRowView)e.Item.DataItem)["fromDB"].ToString() == "0")
            {
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
            ((TextBox)e.Item.FindControl("txtBidCurrency")).ReadOnly = true;
            if (((DataRowView)e.Item.DataItem)["fromDB"].ToString() == "0")
            {
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

        sID = ((DataRowView)e.Item.DataItem)["BACId"].ToString();
        string connstring = HttpContext.Current.Session["ConnectionString"].ToString();
        string sComm;
        sComm = "SELECT ApprovedBy_2 FROM tblBacBidItems WHERE BidRefNo=" + Session["BuyerBidForBac"] + " AND ApprovedBy_2=" + sID + " ";
        sComm = sComm + "UNION ALL ";
        sComm = sComm + "SELECT ApprovedBy_3 FROM tblBacBidItems WHERE BidRefNo=" + Session["BuyerBidForBac"] + " AND ApprovedBy_3=" + sID + " ";
        sComm = sComm + "UNION ALL ";
        sComm = sComm + "SELECT ApprovedBy_4 FROM tblBacBidItems WHERE BidRefNo=" + Session["BuyerBidForBac"] + " AND ApprovedBy_4=" + sID + " ";
        sComm = sComm + "UNION ALL ";
        sComm = sComm + "SELECT ApprovedBy_5 FROM tblBacBidItems WHERE BidRefNo=" + Session["BuyerBidForBac"] + " AND ApprovedBy_5=" + sID;
        SqlDataReader oReader = SqlHelper.ExecuteReader(connstring, CommandType.Text, sComm);
        if (oReader.HasRows)
        {
            ((CheckBox)e.Item.FindControl("approved1")).Checked = true;
        }
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

        sCommand = "SELECT COUNT(*) AS count1 FROM tblBACEvaluationSummary WHERE BidRefNo = " + Session["BuyerBidForBac"].ToString() + " AND fromDB=0";
        oReader = SqlHelper.ExecuteReader(connstring, CommandType.Text, sCommand);
        if (oReader.HasRows)
        {
            oReader.Read();
            txtHasZeroID.Value = oReader["count1"].ToString();
            //iVendor = int.Parse(oReader["count1"].ToString());
        }

        sCommand = "";
        //sCommand = sCommand + "SELECT 1 fromDB, t1.BidTenderNo, t3.Accredited, t4.Currency, CAST(((t1.Amount * t2.Qty) + (t1.DeliveryCost - (t1.Discount * t2.Qty))) AS Decimal(18,2)) AS BidPrice, ";
        //sCommand = sCommand + "t2.Qty, t2.UnitOfMeasure, t2.UnitPrice, t3.VendorName, t3.VendorId, t1.DateSubmitted, t1.Amount, t1.Discount, t1.DeliveryCost, ";
        //sCommand = sCommand + "t1.Remarks, t1.Warranty, t1.Status, t1.RenegotiationStatus, t4.RenegotiationDeadline, ";
        //sCommand = sCommand + "CAST(t1.BidTenderNo AS varchar) + '|' + CAST(t3.VendorId AS varchar) + '|' + CAST(t4.BidRefNo AS varchar) AS BidTenRef ";
        //sCommand = sCommand + "FROM tblBidTenders t1 INNER JOIN tblBidItemDetails t2 ON t2.BidDetailNo = t1.BidDetailNo ";
        //sCommand = sCommand + "INNER JOIN tblVendors t3 ON t3.VendorId = t1.VendorId INNER JOIN tblBidItems t4 ON t4.BidRefNo = t2.BidRefNo ";
        //sCommand = sCommand + "WHERE t2.BidRefNo = " + Session["BuyerBidForBac"].ToString() + " AND t1.Status > 0 ";
        //sCommand = sCommand + "UNION ALL ";
        //sCommand = sCommand + "SELECT fromDB, BidTenderNo, Accredited, 'PHP' Currency, TotalCost, Qty, UnitMeasure, UnitCost, VendorName, 0 VendorId, ";
        //sCommand = sCommand + "GETDATE() DateSubmitted, TotalCost, 0 Discount, 0 DeliveryCost, '' Remarks, '' Warranty, 1 Status, 0 RenegotationStatus, ";
        //sCommand = sCommand + "NULL RenegotationDeadline, '0|0|0' BidTenRef FROM tblBACEvaluationSummary ";
        //sCommand = sCommand + "WHERE BidRefNo = " + Session["BuyerBidForBac"].ToString() + " AND fromDB=0";
        sCommand = sCommand + "SELECT * FROM tblBACEvaluationSummary ";
        sCommand = sCommand + "WHERE BidRefNo = " + Session["BuyerBidForBac"];

        //LogHelper.TextLogHelper.Log(Server.MapPath("ebid.log"), sCommand + "\\n");
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
        if (iVendor > 0)
        {
            int i;
            string arr = "";

            // remove all BAC sessions first
            foreach (string sItem in Session.Contents)
                if (sItem.Contains("BAC_BES")) arr = arr + sItem + ";";
            char[] mySeparator = new char[] { ';' };
            string[] arr1 = arr.Split(mySeparator);
            foreach (string arrStr in arr1)
                Session.Remove(arrStr);
            // store vendor name to session
            i = 0;
            foreach (RepeaterItem oItem in Repeater4.Items)
            {
                i++;
                Session.Add("BAC_BES_VendorName" + i.ToString(), ((TextBox)oItem.FindControl("txtVendorName")).Text);
            }
            // store accreditation to session
            i = 0;
            foreach (RepeaterItem oItem in Repeater100.Items)
            {
                i++;
                Session.Add("BAC_BES_Accreditation" + i.ToString(), ((DropDownList)oItem.FindControl("cbAccreditation")).SelectedIndex);
            }
            // store technical compliance to session
            i = 0;
            foreach (RepeaterItem oItem in Repeater5.Items)
            {
                i++;
                Session.Add("BAC_BES_TechComp" + i.ToString(), ((TextBox)oItem.FindControl("txtTechnical")).Text);
            }
            // store commercial compliance to session
            i = 0;
            foreach (RepeaterItem oItem in Repeater6.Items)
            {
                i++;
                Session.Add("BAC_BES_CommComp" + i.ToString(), ((TextBox)oItem.FindControl("txtCommercial")).Text);
            }
            // store contract compliance to session
            i = 0;
            foreach (RepeaterItem oItem in Repeater7.Items)
            {
                i++;
                Session.Add("BAC_BES_ContComp" + i.ToString(), ((TextBox)oItem.FindControl("txtContract")).Text);
            }
            // store criteria to session
            for (var j = 0; j < 10; j++)
            {
                i = 0;
                Repeater oRepeater = ((Repeater)Form.FindControl("RepeaterC" + j));
                if (oRepeater is object)
                {
                    foreach (RepeaterItem oItem in oRepeater.Items)
                    {
                        i++;
                        Session.Add("BAC_BES_C" + j + i.ToString(), ((TextBox)oItem.FindControl("txtC" + j)).Text);
                    }
                }
            }
        }
    }

    protected void dsApprover_Load(object sender, EventArgs e)
    {
        dsApprover.SelectCommand = "SELECT t1.BACId, t1.LastName + ', ' + t1.FirstName + ' ' + t1.MiddleName AS Name1, t1.ApprovingMaxLimit FROM tblBidAwardingCommittee t1 JOIN tblUsers t2 ON (t1.BACId=t2.UserId AND t2.IsAuthenticated=1) WHERE t1.ApprovingMaxLimit >= " + lblTotalAll.Value + " ORDER BY t1.ApprovingMaxLimit ASC";
        dsApprover.DataBind();
    }

    protected void dsBidItemTendersSummary_Load(object sender, EventArgs e)
    {
        string control1 = Request.Form["__EVENTTARGET"];
        string sCommand, sCommand1;
        string connstring = HttpContext.Current.Session["ConnectionString"].ToString();
        SqlDataReader oReader;
        if (control1 == "NewVendor")
        {
            iVendor = int.Parse(txtNewVendor.Value.ToString());
            sCommand = "SELECT COUNT(*) AS count1 FROM tblBACEvaluationSummary WHERE BidRefNo = " + Session["BuyerBidForBac"].ToString() + " AND fromDB=0";
            oReader = SqlHelper.ExecuteReader(connstring, CommandType.Text, sCommand);
            if (oReader.HasRows)
            {
                oReader.Read();
                txtHasZeroID.Value = oReader["count1"].ToString();
                //iVendor = int.Parse(oReader["count1"].ToString());
            }

            System.Web.UI.HtmlControls.HtmlInputHidden NewVendor;
            NewVendor = ((System.Web.UI.HtmlControls.HtmlInputHidden)Form.FindControl("txtNewVendor"));
            iVendor = int.Parse(NewVendor.Value);

            SaveEvaluationSummaryToSession();

            int i = 0;
            sCommand = "";
            sCommand1 = "";
            while (true)
            {
                if (i == iVendor)
                {
                    break;
                }
                else
                {
                    i++;
                    sCommand1 = sCommand1 + "UNION ALL ";
                    sCommand1 = sCommand1 + "SELECT TOP 1 0 fromDB, 2 Accredited, '' VendorName, 0 VendorId, 1 Status, 0 BidTenderNo ";
                    sCommand1 = sCommand1 + "FROM tblBidTenders ";
                }
            }

            sCommand = "SELECT DISTINCT 1 fromDB, t3.Accredited, t3.VendorName, t3.VendorId, t1.Status, t1.BidTenderNo ";
            sCommand = sCommand + "FROM tblBidTenders t1 INNER JOIN tblBidItemDetails t2 ON t2.BidDetailNo = t1.BidDetailNo ";
            sCommand = sCommand + "INNER JOIN tblVendors t3 ON t3.VendorId = t1.VendorId INNER JOIN tblBidItems t4 ON t4.BidRefNo = t2.BidRefNo ";
            sCommand = sCommand + "WHERE t2.BidRefNo = @BidRefNo AND t1.Status > 0 ";

            sCommand = sCommand + sCommand1;
            //LogHelper.TextLogHelper.Log(Server.MapPath("ebid.log"), sCommand + "\\n");
            dsBidItemTendersSummary.SelectCommand = sCommand;
            dsBidItemTendersSummary.DataBind();
        }
        else if (control1 == "DeleteLastVendorID0")
        {
            sCommand = "DELETE FROM tblBACEvaluationSummary WHERE ID IN ";
            sCommand = sCommand + "(SELECT TOP 1 ID FROM tblBACEvaluationSummary WHERE BidRefNo=" + Session["BuyerBidForBac"].ToString() + " ORDER BY ID DESC)";
            SqlHelper.ExecuteNonQuery(connstring, CommandType.Text, sCommand);
            ShowBidItemTenders();
        }
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

    protected void gvCommDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            HtmlInputHidden oHid = ((HtmlInputHidden)e.Row.FindControl("Chkd"));
            if (oHid.Value == "1")
            {
                ((CheckBox)e.Row.FindControl("chkAward")).Checked = true;
            }
        }
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