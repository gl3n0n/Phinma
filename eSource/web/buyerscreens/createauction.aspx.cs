using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using EBid.lib.auction.trans;
using EBid.lib.bid.trans;
using EBid.lib.utils;
using EBid.lib.constant;
using EBid.lib.auction.data;
using EBid.lib.bid.data;
using System.Xml;
using System.IO;
using EBid.lib;
using EBid.ConnectionString;

public partial class web_buyer_screens_createauction : System.Web.UI.Page
{
    private const string BR = "<br />";
    private const string BULLET = "&#187;";
    private const string BR_BULLET = "<br />&#187;";

    private string connstring = "";


    protected void Page_LoadComplete(object sender, EventArgs e)
    {
    }
    protected void Page_Load(object sender, EventArgs e)
    {

        FormsAuthenticationHelper.AuthenticateUserWithReturnUrl();
        connstring = Client.ConnectionString(HttpContext.Current.Session["clientid"].ToString());
        if (Int32.Parse(Session[Constant.SESSION_USERTYPE].ToString().Trim()) != (int)Constant.USERTYPE.BUYER)
            Response.Redirect("../unauthorizedaccess.aspx");

        PageTitle.InnerText = String.Format(Constant.TITLEFORMAT, "Create New Auction Event");

        if (!(IsPostBack))
        {
            Session[Constant.SESSION_COMMENT_TYPE] = "1";
            btnSaveDraft.Attributes.Add("onClick", "Draft();");
            btnSubmit.Attributes.Add("onClick", "Submit()");
            btnAddSubItem.Attributes.Add("onClick", "AddSubItem();");
            ddlGroupDeptSec.Attributes.Add("onchange", "setHiddenfieldValue(\'__groupID\',this.value);");
            ddlSubCategory.Attributes.Add("onchange", "setHiddenfieldValue(\'__subcatID\',this.value);");
            ddlAuctionType.Attributes.Add("onchange", "ChangeTitle();");
            FillDropDownList();
            lblItemType.Text = Constant.AUCTIONITEMTYPE.ToString().Trim(); //automatically set ItemType to Bid
            jscPRDate.Attributes.Add("style", "text-align:center;");
            jscDeliveryDate.Attributes.Add("style", "text-align:center;");
            jscAuctionConfirmationDeadline.Attributes.Add("style", "text-align:center;");
            jscAuctionStartDate.Attributes.Add("style", "text-align:center;");
            jscAuctionEndDate.Attributes.Add("style", "text-align:center;");
            jscPRDate.Text = DateTime.Now.ToShortDateString();
            jscDeliveryDate.Text = DateTime.Now.AddDays(7.0).ToShortDateString();
            //ddlCategory.SelectedIndex = 0;
            //ddlSubCategory.SelectedIndex = 0;
            ddlBidCurrency.SelectedValue = "PHP";
            lnkAddOTLS.NavigateUrl = "javascript:AddOTLS('" + ddlCategory.ClientID + "', '" + ddlSubCategory.ClientID + "');";
            if (ddlCategory.SelectedValue != "---- SELECT CATEGORY ----")
            {
		lnkAddOTLS.Visible = true;
            }else {
		lnkAddOTLS.Visible = false;
            }

            web_usercontrol_CommentBox uctrlCommentBox = (web_usercontrol_CommentBox)FindControl("CommentBox");

            if (Session[Constant.SESSION_AUCTIONREFNO] != null)
            {
                if (Session[Constant.SESSION_AUCTIONREFNO].ToString().Trim() != "")
                {
                    lstSupplierA.Attributes.Add("ondblclick", "__doPostBack('btnSelectOne','')");
                    lstSupplierB.Attributes.Add("ondblclick", "__doPostBack('btnDeselectOne','')");

                    hdnAuctionRefNo.Value = Session[Constant.SESSION_AUCTIONREFNO].ToString().Trim();
                    FillAuctionInfo();
                    ShowAuctionItems();
                    InitializeRowsForGridViews();
                    hdnDetailNo.Value = GetAuctionItemForConversion(hdnAuctionRefNo.Value.ToString());
                    FillSuppliersB();
                    if (hdnDetailNo.Value.Length > 0)
                    {
                        InitializeControls();
                    }
                }
            }
            else
            {
                if (Session[Constant.SESSION_BIDDETAILNO] != null)
                {
                    InitializeControls();
                    FillBidEventDetails();
                    AddItemtoList();
                    hdnDetailNo.Value = Session[Constant.SESSION_BIDDETAILNO].ToString().Trim();
                    Session[Constant.SESSION_BIDDETAILNO] = null;
                    Session[Constant.SESSION_BIDREFNO] = null;
                    InitializeEmptyRowsForGridViews();
                    gvAuctionItems.Columns[6].Visible = false;
                    FillInvitedSuppliersB();
                    uctrlCommentBox.Visible = false;
                }
                else
                {
                    lstSupplierA.Attributes.Add("ondblclick", "__doPostBack('btnSelectOne','')");
                    lstSupplierB.Attributes.Add("ondblclick", "__doPostBack('btnDeselectOne','')");

                    ShowAuctionItems();
                    InitializeEmptyRowsForGridViews();
                    uctrlCommentBox.Visible = false;
                }
            }
        }
        else
        {
            if (Request.Form["__EVENTTARGET"] == "ddlCategory" && ddlCategory.SelectedIndex > 0)
            {
                SubCategory s = new SubCategory();
                //ddlSubCategory.DataSource = s.GetAllSubCategories();
                ddlSubCategory.DataSource = s.GetSubCategoryByCategoryId(connstring, ddlCategory.SelectedValue);
                ddlSubCategory.DataTextField = "SubCategoryName";
                ddlSubCategory.DataValueField = "SubCategoryId";
                ddlSubCategory.DataBind();
                ddlSubCategory.Items.Insert(0, "");
                ddlSubCategory.Items.FindByText("").Value = "";
                ddlSubCategory.Items.FindByText("").Text = "-- SELECT SUBCATEGORY --";
            }

            if (Request.Form["__EVENTTARGET"] == "ddlCompany" && ddlCompany.SelectedIndex > 0)
            {
                GroupDeptSecTransaction grp = new GroupDeptSecTransaction();
               // ddlGroupDeptSec.DataSource = grp.GetGroupDeptSec("");
                ddlGroupDeptSec.DataSource = grp.GetGroupDeptSec(connstring, ddlCompany.SelectedValue);
                ddlGroupDeptSec.DataTextField = "GroupDeptSecName";
                ddlGroupDeptSec.DataValueField = "GroupDeptSecId";
                ddlGroupDeptSec.DataBind();
                ddlGroupDeptSec.Items.Insert(0, "");
                ddlGroupDeptSec.Items.FindByText("").Value = "";
                ddlGroupDeptSec.Items.FindByText("").Text = "-- SELECT GROUP/DEPT/SEC --";
            }
            if (ddlCategory.SelectedValue != "---- SELECT CATEGORY ----")
            {
		lnkAddOTLS.Visible = true;
            }else {
		lnkAddOTLS.Visible = false;
            }
        }
        if (Session[Constant.SESSION_AUCTIONREFNO] != null) {
        	AddOTLS(); }
    }

    private string GetAuctionItemForConversion(string auctionRefNo)
    {
        SqlParameter[] sqlParams = new SqlParameter[1];
        sqlParams[0] = new SqlParameter("@AuctionRefNo", SqlDbType.Int);
        sqlParams[0].Value = auctionRefNo;

        return SqlHelper.ExecuteScalar(connstring, CommandType.StoredProcedure, "sp_GetAuctionItemForConversion", sqlParams).ToString().Trim();
        
    }

    private DataTable CreateEmptySupplierTable()
    {
        DataTable dt = new DataTable();
        DataColumn dc = new DataColumn("Supplier", typeof(System.String));
        dt.Columns.Add(dc);
        DataRow dr = dt.NewRow();
        dr["Supplier"] = "";
        dt.Rows.Add(dr);
        return dt;
    }

    private void AddNewItem()
    {
        DataTable dt = new DataTable();
        DataColumn dc = new DataColumn("AuctionDetailNo", typeof(System.String));
        dt.Columns.Add(dc);
        dc = new DataColumn("PItemName2", typeof(System.String));
        dt.Columns.Add(dc);
        dc = new DataColumn("Description", typeof(System.String));
        dt.Columns.Add(dc);
        dc = new DataColumn("Quantity", typeof(System.String));
        dt.Columns.Add(dc);
        dc = new DataColumn("UnitOfMeasure", typeof(System.String));
        dt.Columns.Add(dc);
        dc = new DataColumn("StartingPrice", typeof(System.String));
        dt.Columns.Add(dc);
        dc = new DataColumn("IncrementDecrement", typeof(System.String));
        dt.Columns.Add(dc);
        dc = new DataColumn("SKU", typeof(System.String));
        dt.Columns.Add(dc);
        dc = new DataColumn("RecNum", typeof(System.String));
        dt.Columns.Add(dc);
        dc = new DataColumn("ForConversion", typeof(System.String));
        dt.Columns.Add(dc);

        foreach (GridViewRow gvr in gvAuctionItems.Rows)
        {
            // get controls        
            string vItemName = ((TextBox)gvr.FindControl("txtItem")).Text.Trim();
            string vDesc = ((TextBox)gvr.FindControl("txtDesc")).Text.Trim();
            string vQty = ((TextBox)gvr.FindControl("txtQty")).Text.Trim();
            string vUOM = ((TextBox)gvr.FindControl("txtUOM")).Text.Trim();
            string vSKU = ((TextBox)gvr.FindControl("txtSKU")).Text.Trim();
            string vStartingPrice = ((TextBox)gvr.FindControl("txtStartingPrice")).Text.Trim();
            string vIncrementDecrement = ((TextBox)gvr.FindControl("txtIncrementDecrement")).Text.Trim();
            string vAuctionDetailNo = ((Label)gvr.FindControl("lblAuctionDetailNo")).Text.Trim();
            string vForConversion = ((Label)gvr.FindControl("lblForConversion")).Text.Trim();

            DataRow dr = dt.NewRow();
            int nxtCounter = 0;
            if (dt.Rows.Count > 0)
                nxtCounter = int.Parse(dt.Rows[dt.Rows.Count - 1]["RecNum"].ToString()) + 1;

            dr["RecNum"] = nxtCounter;
            dr["PItemName2"] = vItemName;
            dr["Description"] = vDesc;
            dr["Quantity"] = vQty;
            dr["UnitOfMeasure"] = vUOM;
            dr["StartingPrice"] = vStartingPrice;
            dr["IncrementDecrement"] = vIncrementDecrement;
            dr["SKU"] = vSKU;
            dr["ForConversion"] = vForConversion;
            dt.Rows.Add(dr);            
        }              

        if (hdnAddNewRow.Value.ToString().Trim() == "true")
        {
            CreateEmptyRow(ref dt);
            hdnAddNewRow.Value = "";
        }

        ViewState["AuctionEventItems"] = dt;
        gvAuctionItems.DataSource = dt;
        gvAuctionItems.DataBind();

    }

    private void FillAuctionInfo()
    {
        AuctionTransaction auc = new AuctionTransaction();
        lblAuctionTitle.Text = hdnAuctionRefNo.Value.Trim();

        AuctionItem ai = auc.GetAuctionByAuctionRefNo(connstring, hdnAuctionRefNo.Value.Trim());
        lblItemType.Text = Constant.AUCTIONITEMTYPE.ToString().Trim(); //automatic for auction items;
        ddlCompany.SelectedIndex = ddlCompany.Items.IndexOf(ddlCompany.Items.FindByValue(ai.CompanyId.ToString().Trim()));
        txtRequestor.Text = ai.Requestor.ToString().Trim();
        txtPRNumber.Text = ai.PRRefNo.ToString().Trim();
        txtPRDate.Text = ai.PRDateMonth.ToString().Trim() + "/" + ai.PRDateDay.ToString().Trim() + "/" + ai.PRDateYear.ToString().Trim();
        jscPRDate.Text = ai.PRDateMonth.ToString().Trim() + "/" + ai.PRDateDay.ToString().Trim() + "/" + ai.PRDateYear.ToString().Trim();
        ddlGroupDeptSec.SelectedIndex = ddlGroupDeptSec.Items.IndexOf(ddlGroupDeptSec.Items.FindByValue(ai.GroupDeptSec.ToString().Trim()));
        __groupID.Value = ai.GroupDeptSec.ToString().Trim();
        lblReferenceNumber.Text = hdnAuctionRefNo.Value.ToString().Trim();
        ddlCategory.SelectedIndex = ddlCategory.Items.IndexOf(ddlCategory.Items.FindByValue(ai.Category.ToString().Trim()));
        ddlSubCategory.SelectedIndex = ddlSubCategory.Items.IndexOf(ddlSubCategory.Items.FindByValue(ai.SubCategory.ToString().Trim()));
        __subcatID.Value = ai.SubCategory.ToString().Trim();
        txtDeliveryDate.Text = ai.DeliveryDateMonth.ToString().Trim() + "/" + ai.DeliveryDateDay.ToString().Trim() + "/" + ai.DeliveryDateYear.ToString().Trim();
        jscDeliveryDate.Text = ai.DeliveryDateMonth.ToString().Trim() + "/" + ai.DeliveryDateDay.ToString().Trim() + "/" + ai.DeliveryDateYear.ToString().Trim();
        txtItemDescription.Text = ai.ItemDescription.ToString().Trim();
        ddlAuctionType.SelectedIndex = ddlAuctionType.Items.IndexOf(ddlAuctionType.Items.FindByValue(ai.AuctionType.ToString().Trim()));
        txtAuctionConfirmationDeadline.Text = ai.AuctionDeadlineMonth.ToString().Trim() + "/" + ai.AuctionDeadlineDay.ToString().Trim() + "/" + ai.AuctionDeadlineYear.ToString().Trim();
        jscAuctionConfirmationDeadline.Text = ai.AuctionDeadlineMonth.ToString().Trim() + "/" + ai.AuctionDeadlineDay.ToString().Trim() + "/" + ai.AuctionDeadlineYear.ToString().Trim();
        txtAuctionStartDate.Text = ai.AuctionStartMonth.ToString().Trim() + "/" + ai.AuctionStartDay.ToString().Trim() + "/" + ai.AuctionStartYear.ToString().Trim();
        jscAuctionStartDate.Text = ai.AuctionStartMonth.ToString().Trim() + "/" + ai.AuctionStartDay.ToString().Trim() + "/" + ai.AuctionStartYear.ToString().Trim();
        ddlStartHour.SelectedIndex = ddlStartHour.Items.IndexOf(ddlStartHour.Items.FindByValue(ai.AuctionStartTimeHour.ToString().Trim()));
        txtStartMin.Text = ai.AuctionStartTimeMin.ToString().Trim();
        txtStartSec.Text = ai.AuctionStartTimeSec.ToString().Trim();
        ddlStartAMPM.SelectedIndex = ddlStartAMPM.Items.IndexOf(ddlStartAMPM.Items.FindByValue(ai.AuctionStartTimeAMPM.ToString().Trim()));
        txtAuctionEndDate.Text = ai.AuctionEndMonth.ToString().Trim() + "/" + ai.AuctionEndDay.ToString().Trim() + "/" + ai.AuctionEndYear.ToString().Trim();
        jscAuctionEndDate.Text = ai.AuctionEndMonth.ToString().Trim() + "/" + ai.AuctionEndDay.ToString().Trim() + "/" + ai.AuctionEndYear.ToString().Trim();
        ddlEndHour.SelectedIndex = ddlEndHour.Items.IndexOf(ddlEndHour.Items.FindByValue(ai.AuctionEndTimeHour.ToString().Trim()));
        txtEndMin.Text = ai.AuctionEndTimeMin.ToString().Trim();
        txtEndSec.Text = ai.AuctionEndTimeSec.ToString().Trim();
        ddlEndAMPM.SelectedIndex = ddlEndAMPM.Items.IndexOf(ddlEndAMPM.Items.FindByValue(ai.AuctionEndTimeAMPM.ToString().Trim()));
        txtCompleteAuctionStartTime.Text = ai.AuctionStartMonth.ToString().Trim() + "/" + ai.AuctionStartDay.ToString().Trim() + "/" + ai.AuctionStartYear.ToString().Trim() + " " + ai.AuctionStartTimeHour.ToString().Trim() + ":" + ai.AuctionStartTimeMin.ToString().Trim() + ":" + ai.AuctionStartTimeSec.ToString().Trim() + " " + ai.AuctionStartTimeAMPM.ToString().Trim();
        txtCompleteAuctionEndTime.Text = ai.AuctionEndMonth.ToString().Trim() + "/" + ai.AuctionEndDay.ToString().Trim() + "/" + ai.AuctionEndYear.ToString().Trim() + " " + ai.AuctionEndTimeHour.ToString().Trim() + ":" + ai.AuctionEndTimeMin.ToString().Trim() + ":" + ai.AuctionEndTimeSec.ToString().Trim() + " " + ai.AuctionEndTimeAMPM.ToString().Trim();
        ddlBidCurrency.SelectedIndex = ddlBidCurrency.Items.IndexOf(ddlBidCurrency.Items.FindByValue(ai.BidCurrency.ToString().Trim()));
    }

    private DataTable CreateEmptyTable()
    {
        DataTable dt = new DataTable();
        DataColumn dc = new DataColumn("AuctionDetailNo", typeof(System.String));
        dt.Columns.Add(dc);
        dc = new DataColumn("PItemName2", typeof(System.String));
        dt.Columns.Add(dc);
        dc = new DataColumn("Description", typeof(System.String));
        dt.Columns.Add(dc);
        dc = new DataColumn("Quantity", typeof(System.String));
        dt.Columns.Add(dc);
        dc = new DataColumn("UnitOfMeasure", typeof(System.String));
        dt.Columns.Add(dc);
        dc = new DataColumn("StartingPrice", typeof(System.String));
        dt.Columns.Add(dc);
        dc = new DataColumn("IncrementDecrement", typeof(System.String));
        dt.Columns.Add(dc);
        dc = new DataColumn("SKU", typeof(System.String));
        dt.Columns.Add(dc);
        dc = new DataColumn("RecNum", typeof(System.String));
        dt.Columns.Add(dc);
        dc = new DataColumn("ForConversion", typeof(System.String));
        dt.Columns.Add(dc);

        DataRow dr = dt.NewRow();
        dr["AuctionDetailNo"] = "";
        dr["PItemName2"] = "";
        dr["Description"] = "";
        dr["Quantity"] = "";
        dr["UnitOfMeasure"] = "";
        dr["StartingPrice"] = "";
        dr["IncrementDecrement"] = "";
        dr["SKU"] = "";
        dr["RecNum"] = 0;
        dr["ForConversion"] = "0";
        dt.Rows.Add(dr);
        return dt;
    }

    private void CreateEmptyRow(ref DataTable dt)
    {

        DataRow dr = dt.NewRow();
        dr["AuctionDetailNo"] = System.DBNull.Value;
        dr["PItemName2"] = "";
        dr["Description"] = "";
        dr["Quantity"] = System.DBNull.Value;
        dr["UnitOfMeasure"] = "";
        dr["StartingPrice"] = "";
        dr["IncrementDecrement"] = "";
        dr["SKU"] = "";
        dr["RecNum"] = dt.Rows.Count;
        dr["ForConversion"] = "0";
        dt.Rows.Add(dr);
    }

    private void InitializeControls()
    {
        jscDeliveryDate.Text = DateTime.Now.AddDays(7.0).Date.ToShortDateString();
        ddlBidCurrency.SelectedValue = "PHP";
        ddlAuctionType.SelectedIndex = 2;
        ddlAuctionType.Enabled = false;
        btnAddSubItem.Visible = false;
        ddlCategory.Enabled = false;
        ddlSubCategory.Enabled = false;
        dsVendors.SelectCommand = "sp_GetAllBidEventParticipantsByBidDetailNo";
        dsVendors.SelectParameters.Clear();
        dsVendors.SelectParameters.Add("BidDetailNo",hdnDetailNo.Value.ToString().Trim());
        dsVendors.SelectParameters.Add("Status","1");
       	lnkAddOTLS.Visible = false;
    }

    protected bool IsVisible(string status)
    {
        return !(status == "1");
    }

    protected bool IsReadOnly(string status)
    {
        return (status == "1");
    }

    protected bool IsDisabled(string status)
    {
        return (status == "1");
    }

    private void FillBidEventDetails()
    {
        IEnumerator iEnum = dsEventDetails.Select(DataSourceSelectArguments.Empty).GetEnumerator();

        while (iEnum.MoveNext())
        {
            DataRowView dRView = (DataRowView)iEnum.Current;
            txtItemDescription.Text = dRView["ItemDesc"].ToString();
            ddlCompany.SelectedValue = dRView["CompanyId"].ToString();
            txtRequestor.Text = dRView["Requestor"].ToString();
            txtPRNumber.Text = dRView["PRRefNo"].ToString();
            jscPRDate.Text = DateTime.Parse(dRView["PRDate"].ToString()).ToShortDateString();
            ddlGroupDeptSec.SelectedIndex = ddlGroupDeptSec.Items.IndexOf(ddlGroupDeptSec.Items.FindByValue(dRView["GroupDeptSec"].ToString().Trim()));
            __groupID.Value = dRView["GroupDeptSec"].ToString();
            lblReferenceNumber.Text = dRView["BidRefNo"].ToString();
            hdnRefNo.Value = dRView["BidRefNo"].ToString();
            ddlCategory.SelectedValue = dRView["Category"].ToString();
            if (dRView["SubCategory"].ToString().Length > 0)
            {
                ddlSubCategory.SelectedIndex = ddlSubCategory.Items.IndexOf(ddlSubCategory.Items.FindByValue(dRView["SubCategory"].ToString().Trim()));
                __subcatID.Value = dRView["SubCategory"].ToString().Trim();
            }
            ddlBidCurrency.SelectedValue = dRView["CurrencyID"].ToString();            
        }
    }

    private DataTable CreateItemsTable()
    {
        DataTable dt = new DataTable();
        DataColumn dc = new DataColumn("AuctionDetailNo", typeof(System.String));
        dt.Columns.Add(dc);
        dc = new DataColumn("PItemName2", typeof(System.String));
        dt.Columns.Add(dc);
        dc = new DataColumn("Description", typeof(System.String));
        dt.Columns.Add(dc);
        dc = new DataColumn("Quantity", typeof(System.String));
        dt.Columns.Add(dc);
        dc = new DataColumn("UnitOfMeasure", typeof(System.String));
        dt.Columns.Add(dc);
        dc = new DataColumn("StartingPrice", typeof(System.String));
        dt.Columns.Add(dc);
        dc = new DataColumn("IncrementDecrement", typeof(System.String));
        dt.Columns.Add(dc);
        dc = new DataColumn("SKU", typeof(System.String));
        dt.Columns.Add(dc);
        dc = new DataColumn("RecNum", typeof(System.String));
        dt.Columns.Add(dc);
        dc = new DataColumn("ForConversion", typeof(System.String));
        dt.Columns.Add(dc);

        return dt;
    }

    private void FillItemDetails(ref DataTable dt)
    {
        IEnumerator iEnum = dsItemDetails.Select(DataSourceSelectArguments.Empty).GetEnumerator();

        while (iEnum.MoveNext())
        {
            DataRow dr = dt.NewRow();
            DataRowView dRView = (DataRowView)iEnum.Current;

            dr["AuctionDetailNo"] = System.DBNull.Value;
            dr["PItemName2"] = dRView["SKU"].ToString() + " - " + dRView["DetailDesc"].ToString();
            dr["Description"] = dRView["DetailDesc"].ToString();
            dr["Quantity"] = dRView["Qty"].ToString();
            dr["UnitOfMeasure"] = dRView["UnitOfMeasure"].ToString();
            if (ddlAuctionType.SelectedValue == "1")
                dr["StartingPrice"] = dRView["MinBid"];
            else
                dr["StartingPrice"] = dRView["MaxBid"];
            dr["IncrementDecrement"] = "";
            dr["SKU"] = dRView["SKU"].ToString();
            dr["RecNum"] = dt.Rows.Count;
            dr["ForConversion"] = "1";

            dt.Rows.Add(dr);
        }
    }

    private void AddItemtoList()
    {
        DataTable dt = new DataTable();

        if (dt.Rows.Count == 0)
            dt = CreateItemsTable();

        FillItemDetails(ref dt);
        ViewState["AuctionEventItems"] = dt;
        gvAuctionItems.Visible = true;
        gvAuctionItems.DataSource = dt;
        gvAuctionItems.DataBind();
    }

    private void ShowAuctionItems()
    {
        AuctionTransaction auc = new AuctionTransaction();
        DataTable dt = new DataTable();
        if (hdnAuctionRefNo.Value.ToString().Trim() != "")
        {
            dt = auc.GetAuctionItems(connstring, hdnAuctionRefNo.Value.ToString().Trim());
            if (dt.Rows.Count == 0)
                dt = CreateEmptyTable();
        }
        else
        {
            dt = CreateEmptyTable();
        }

        if (hdnAddNewRow.Value.ToString().Trim() == "true")
        {
            CreateEmptyRow(ref dt);
            hdnAddNewRow.Value = "";
        }

        ViewState["AuctionEventItems"] = dt;
        gvAuctionItems.Visible = true;
        gvAuctionItems.DataSource = dt;
        gvAuctionItems.DataBind();

    }

    protected void gvAuctionItems_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("Remove"))
        {
            if (hdnConfirmRemoveSubItem.Value.ToLower().Trim() == "true")
            {   
                Session["SubItemIndex"] = e.CommandArgument.ToString().Trim();
              
                RemoveFromBidItemList(int.Parse(e.CommandArgument.ToString()));
                
                DataTable dtAuctionItems = (DataTable)ViewState["AuctionEventItems"];
                if (dtAuctionItems.Rows.Count == 0)
                {
                    DataTable dt = new DataTable();
                    dt = CreateEmptyTable();
                    ViewState["AuctionEventItems"] = dt;
                    gvAuctionItems.DataSource = dt;
                    gvAuctionItems.DataBind();
                }
            }
        }
    }

    private void FillDropDownList()
    {
        CompanyTransaction cmp = new CompanyTransaction();
        ddlCompany.DataTextField = "Company";
        ddlCompany.DataValueField = "CompanyId";
        ddlCompany.DataSource = cmp.GetCompanies(connstring);
        ddlCompany.DataBind();
        ddlCompany.Items.Insert(0, "---- SELECT COMPANY ----");

        OtherTransaction oth = new OtherTransaction();
        ddlStartHour.DataSource = oth.GetHour();
        ddlStartHour.DataTextField = "Text";
        ddlStartHour.DataValueField = "Value";
        ddlStartHour.DataBind();

        ddlStartAMPM.DataSource = oth.GetAMPM();
        ddlStartAMPM.DataTextField = "Text";
        ddlStartAMPM.DataValueField = "Value";
        ddlStartAMPM.DataBind();

        ddlEndAMPM.DataSource = oth.GetAMPM();
        ddlEndAMPM.DataTextField = "Text";
        ddlEndAMPM.DataValueField = "Value";
        ddlEndAMPM.DataBind();

        ddlEndHour.DataSource = oth.GetHour();
        ddlEndHour.DataTextField = "Text";
        ddlEndHour.DataValueField = "Value";
        ddlEndHour.DataBind();
        
        AuctionTransaction auc = new AuctionTransaction();
        ddlAuctionType.DataSource = auc.GetAuctionType(connstring);
        ddlAuctionType.DataTextField = "AuctionType";
        ddlAuctionType.DataValueField = "AuctionTypeId";
        ddlAuctionType.DataBind();
        ddlAuctionType.Items.Insert(0, "---- SELECT TYPE ----");

        GroupDeptSecTransaction grp = new GroupDeptSecTransaction();
        ddlGroupDeptSec.DataSource = grp.GetGroupDeptSec(connstring, "");
        ddlGroupDeptSec.DataTextField = "GroupDeptSecName";
        ddlGroupDeptSec.DataValueField = "GroupDeptSecId";
        ddlGroupDeptSec.DataBind();
        ddlGroupDeptSec.Items.Insert(0, "");

        CategoryTransaction cat = new CategoryTransaction();
        ddlCategory.DataSource = cat.GetAllCategories(connstring);
        ddlCategory.DataTextField = "CategoryName";
        ddlCategory.DataValueField = "CategoryId";
        ddlCategory.DataBind();
        ddlCategory.Items.Insert(0, "---- SELECT CATEGORY ----");

        //Response.Write(ddlCategory.SelectedIndex.ToString());
        SubCategory s = new SubCategory();
        ddlSubCategory.DataSource = s.GetAllSubCategories(connstring);
        //ddlSubCategory.DataSource = s.GetSubCategoryByCategoryId();
        ddlSubCategory.DataTextField = "SubCategoryName";
        ddlSubCategory.DataValueField = "SubCategoryId";
        ddlSubCategory.DataBind();
        ddlSubCategory.Items.Insert(0, "");
    }

    private void SaveSuppliers(string vAuctionRefNo)
    {
        AuctionTransaction auc = new AuctionTransaction();
        auc.DeleteSupplierForAnAuction(connstring, vAuctionRefNo);

        if (txtSuppliers.Text.Trim() != "")
            auc.InsertSuppliersForAnAuction(connstring, vAuctionRefNo, txtSuppliers.Text.Trim());
    }

    private string GetDateTime(string date, string hour, string min, string sec, string ampm)
    {
        return String.Format("{0} {1}:{2}:{3} {4}", date, hour, min, sec, ampm);
    }

    private void SaveAuction(string vStatus)
    {
        string vAuctionRefNo = "";
        string vSuppliers = txtSuppliers.Text.ToString().Trim();
 
        AuctionTransaction auc = new AuctionTransaction();

        if (hdnAuctionRefNo.Value.ToString().Trim() != "")
        {
            auc.UpdateAuctionItem(connstring, txtPRNumber.Text.ToString().Trim(),
                 txtRequestor.Text.ToString().Trim(),
                 txtItemDescription.Text.ToString().Trim(),
                 Session[Constant.SESSION_USERID].ToString().Trim(),
                 ddlGroupDeptSec.SelectedItem.Value.ToString().Trim(),
                 ddlCategory.SelectedItem.Value.ToString().Trim(),
                 ddlSubCategory.SelectedItem.Value.ToString().Trim(),
                 vStatus,
                 jscDeliveryDate.Text.ToString().Trim(),
                 ddlCompany.SelectedItem.Value.ToString().Trim(),
                 ddlAuctionType.SelectedItem.Value.ToString().Trim(),
                 jscAuctionConfirmationDeadline.Text.ToString().Trim(),
                 GetDateTime(jscAuctionStartDate.Text.Trim(), ddlStartHour.SelectedValue.Trim(), txtStartMin.Text.Trim(), txtStartSec.Text.Trim(), ddlStartAMPM.SelectedValue.Trim()),
                 GetDateTime(jscAuctionEndDate.Text.Trim(), ddlEndHour.SelectedValue.Trim(), txtEndMin.Text.Trim(), txtEndSec.Text.Trim(), ddlEndAMPM.SelectedValue.Trim()),
                 ddlBidCurrency.SelectedItem.Value.ToString(),
                 hdnRefNo.Value.Length == 0 ? "-1" : hdnRefNo.Value.ToString().Trim(),
                 jscPRDate.Text.ToString().Trim(),
                 hdnAuctionRefNo.Value.Trim());

            SaveSuppliers(hdnAuctionRefNo.Value.Trim());
        }
        else
        {
            auc.InsertAuctionItem(connstring, txtPRNumber.Text.ToString().Trim(),
                 txtRequestor.Text.ToString().Trim(),
                 txtItemDescription.Text.ToString().Trim(),
                 Session[Constant.SESSION_USERID].ToString().Trim(),
                 ddlGroupDeptSec.SelectedItem.Value.ToString().Trim(),
                 ddlCategory.SelectedItem.Value.ToString().Trim(),
                 ddlSubCategory.SelectedItem.Value.ToString().Trim(),
                 vStatus,
                 jscDeliveryDate.Text.ToString().Trim(),
                 ddlCompany.SelectedItem.Value.ToString().Trim(),
                 ddlAuctionType.SelectedItem.Value.ToString().Trim(),
                 jscAuctionConfirmationDeadline.Text.ToString().Trim(),
                 GetDateTime(jscAuctionStartDate.Text.Trim(), ddlStartHour.SelectedValue.Trim(), txtStartMin.Text.Trim(), txtStartSec.Text.Trim(), ddlStartAMPM.SelectedValue.Trim()),
                 GetDateTime(jscAuctionEndDate.Text.Trim(), ddlEndHour.SelectedValue.Trim(), txtEndMin.Text.Trim(), txtEndSec.Text.Trim(), ddlEndAMPM.SelectedValue.Trim()),
                 ddlBidCurrency.SelectedItem.Value.ToString(),
                 hdnRefNo.Value.Length == 0 ? "-1" : hdnRefNo.Value.ToString().Trim(),
                 jscPRDate.Text.ToString().Trim(),
                 ref vAuctionRefNo);

            hdnAuctionRefNo.Value = vAuctionRefNo;
            lblReferenceNumber.Text = vAuctionRefNo;

            if (txtSuppliers.Text.Trim() != "")
            {
                auc.InsertSuppliersForAnAuction(connstring, vAuctionRefNo, txtSuppliers.Text.Trim());
            }
        }
    }

    protected void gvAuctionItems_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ProductsTransaction p = new ProductsTransaction();
            Label lbl = (Label)e.Row.FindControl("lblRowIndex");
            HtmlInputButton btn = (HtmlInputButton)e.Row.FindControl("btnSearch");
            btn.Attributes.Add("onClick", "ShowProducts('" + lbl.Text.Trim() + "')");
            LinkButton lnk = (LinkButton)e.Row.FindControl("lnkRemove");
            lnk.Attributes.Add("onClick", "ConfirmRemoveSubItem();");
            TextBox tb = new TextBox();
            tb = (TextBox)e.Row.FindControl("txtQty");
            tb.Attributes.Add("style", "text-align: right");
            tb = (TextBox)e.Row.FindControl("txtStartingPrice");
            tb.Attributes.Add("style", "text-align: right");
            tb = (TextBox)e.Row.FindControl("txtIncrementDecrement");
            tb.Attributes.Add("style", "text-align: right");
        }
    }

    private DataTable createUOMTable(string strUOM)
    {
        string[] strUOM1 = strUOM.Split(Convert.ToChar(","));

        DataTable dt = new DataTable();
        DataColumn dc = new DataColumn("UnitOfMeasureValue", typeof(System.String));
        dt.Columns.Add(dc);
        dc = new DataColumn("UnitOfMeasureText", typeof(System.String));
        dt.Columns.Add(dc);

        if (strUOM1.Length > 0)
        {
            foreach (string str in strUOM1)
            {
                DataRow dr = dt.NewRow();
                dr["UnitOfMeasureValue"] = str.Trim();
                dr["UnitOfMeasureText"] = str.Trim();
                dt.Rows.Add(dr);
            }
        }
        else
        {
            DataRow dr = dt.NewRow();
            dr["UnitOfMeasureValue"] = "";
            dr["UnitOfMeasureText"] = "None";
            dt.Rows.Add(dr);
        }
        return dt;
    }

    private void SaveAuctionDetails(string vStatus)
    {
        string strAuctionDetailNo = "";
        AuctionTransaction auc = new AuctionTransaction();
        foreach (GridViewRow row in gvAuctionItems.Rows)
        {
            string vDesc = ((TextBox)(row.FindControl("txtDesc"))).Text;
            string vQty = ((TextBox)(row.FindControl("txtQty"))).Text;
            string vUnitOfMeasurement = ((TextBox)(row.FindControl("txtUOM"))).Text;
            string vSKU = ((TextBox)(row.FindControl("txtSKU"))).Text;
            string vStartingPrice = ((TextBox)(row.FindControl("txtStartingPrice"))).Text;
            string vIncrementDecrement = ((TextBox)(row.FindControl("txtIncrementDecrement"))).Text;
            string vAuctionDetailNo = ((Label)(row.FindControl("lblAuctionDetailNo"))).Text;
            string vForConversion = ((Label)(row.FindControl("lblForConversion"))).Text;
            string vBidDetailNo = hdnDetailNo.Value.ToString();

            if (vAuctionDetailNo != "")
            {
                if (vSKU.Length > 0)
                {
                    auc.UpdateAuctionItemDetails(connstring, vSKU, vDesc, vQty, vUnitOfMeasurement, vStatus, vAuctionDetailNo, ddlCategory.SelectedItem.Value.Trim(), ddlSubCategory.SelectedItem.Value.Trim(), vStartingPrice, vIncrementDecrement, vForConversion);
                    strAuctionDetailNo = ((strAuctionDetailNo != "") ? strAuctionDetailNo + ", " + vAuctionDetailNo : vAuctionDetailNo);
                }
                else
                {
                    auc.UpdateAuctionItemDetails(connstring, "999999", vDesc, vQty, vUnitOfMeasurement, vStatus, vAuctionDetailNo, ddlCategory.SelectedItem.Value.Trim(), ddlSubCategory.SelectedItem.Value.Trim(), vStartingPrice, vIncrementDecrement, vForConversion);
                    strAuctionDetailNo = ((strAuctionDetailNo != "") ? strAuctionDetailNo + ", " + vAuctionDetailNo : vAuctionDetailNo);
                }
            }
            else
            {
                if (vSKU.Length > 0)
                {
                    if (hdnDetailNo.Value.ToString().Length > 0)
                    {
                        auc.InsertAuctionItemDetails(connstring, vSKU, vDesc, vQty, vUnitOfMeasurement, vStatus, hdnAuctionRefNo.Value.ToString().Trim(), ddlCategory.SelectedItem.Value.Trim(), ddlSubCategory.SelectedItem.Value.Trim(), vStartingPrice, vIncrementDecrement, vForConversion, vBidDetailNo, ref vAuctionDetailNo);
                        ((Label)(row.FindControl("lblAuctionDetailNo"))).Text = vAuctionDetailNo;
                    }
                    else
                    {
                        auc.InsertAuctionItemDetails(connstring, vSKU, vDesc, vQty, vUnitOfMeasurement, vStatus, hdnAuctionRefNo.Value.ToString().Trim(), ddlCategory.SelectedItem.Value.Trim(), ddlSubCategory.SelectedItem.Value.Trim(), vStartingPrice, vIncrementDecrement, vForConversion, "0", ref vAuctionDetailNo);
                        ((Label)(row.FindControl("lblAuctionDetailNo"))).Text = vAuctionDetailNo;
                    }
                }
                else
                {
                    if (hdnDetailNo.Value.ToString().Length > 0)
                    {
                        auc.InsertAuctionItemDetails(connstring, "999999", vDesc, vQty, vUnitOfMeasurement, vStatus, hdnAuctionRefNo.Value.ToString().Trim(), ddlCategory.SelectedItem.Value.Trim(), ddlSubCategory.SelectedItem.Value.Trim(), vStartingPrice, vIncrementDecrement, vForConversion, vBidDetailNo, ref vAuctionDetailNo);
                        ((Label)(row.FindControl("lblAuctionDetailNo"))).Text = vAuctionDetailNo;
                    }
                    else
                    {
                        auc.InsertAuctionItemDetails(connstring, "999999", vDesc, vQty, vUnitOfMeasurement, vStatus, hdnAuctionRefNo.Value.ToString().Trim(), ddlCategory.SelectedItem.Value.Trim(), ddlSubCategory.SelectedItem.Value.Trim(), vStartingPrice, vIncrementDecrement, vForConversion, "0", ref vAuctionDetailNo);
                        ((Label)(row.FindControl("lblAuctionDetailNo"))).Text = vAuctionDetailNo;
                    }

                }
                strAuctionDetailNo = ((strAuctionDetailNo != "") ? strAuctionDetailNo + ", " + vAuctionDetailNo : vAuctionDetailNo);
            }
        }
        auc.DeleteAuctionDetail(connstring, hdnAuctionRefNo.Value.Trim(), strAuctionDetailNo.Trim());

    }

    private void UpdateBidItemAuctionInfo(int biddetailno, int status, int arefno)
    {
        SqlConnection sqlConnect = new SqlConnection(connstring);
        SqlTransaction sqlTransact = null;

        try
        {
            sqlConnect.Open();
            sqlTransact = sqlConnect.BeginTransaction();

            SqlParameter[] sqlParams = new SqlParameter[3];
            sqlParams[0] = new SqlParameter("@Status", SqlDbType.Int);
            sqlParams[0].Value = status;
            sqlParams[1] = new SqlParameter("@BidDetailNo", SqlDbType.Int);
            sqlParams[1].Value = biddetailno;
            sqlParams[2] = new SqlParameter("@AuctionRefNoOnAuction", SqlDbType.Int);
            sqlParams[2].Value = arefno;
    
            SqlHelper.ExecuteNonQuery(connstring, CommandType.StoredProcedure, "sp_UpdateBidItemAuctionInfo", sqlParams);
            sqlTransact.Commit();
        }
        catch
        {
            sqlTransact.Rollback();
        }
        finally
        {
            sqlConnect.Close();
        }
    }

    private void ChangeConversionStatus(int biddetailno, int status)
    {
        SqlConnection sqlConnect = new SqlConnection(connstring);
        SqlTransaction sqlTransact = null;

        try
        {
            sqlConnect.Open();
            sqlTransact = sqlConnect.BeginTransaction();

            SqlParameter[] sqlParams = new SqlParameter[2];
            sqlParams[0] = new SqlParameter("@Status", SqlDbType.Int);
            sqlParams[0].Value = status;
            sqlParams[1] = new SqlParameter("@BidDetailNo", SqlDbType.Int);
            sqlParams[1].Value = biddetailno;

            SqlHelper.ExecuteNonQuery(connstring, CommandType.StoredProcedure, "sp_UpdateBidItemConversionStatus", sqlParams);
            sqlTransact.Commit();
        }
        catch
        {
            sqlTransact.Rollback();
        }
        finally
        {
            sqlConnect.Close();
        }
    }

    protected void btnSaveDraft_Click(object sender, EventArgs e)
    {
        SaveAuction(Constant.AUCTION_STATUS_DRAFT.ToString().Trim());
        SaveAuctionDetails(Constant.AUCTION_STATUS_DRAFT.ToString().Trim());
        Session[Constant.SESSION_AUCTIONREFNO] = hdnAuctionRefNo.Value.Trim();

        if (hdnAuctionRefNo.Value.Length > 0)
        {
            DeleteExistingAuctionAttachments(int.Parse(hdnAuctionRefNo.Value.Trim()));
            SaveAuctionAttachments(int.Parse(hdnAuctionRefNo.Value.Trim()));
        }
        else
        {
            DeleteExistingAuctionAttachments(int.Parse(Session[Constant.SESSION_AUCTIONREFNO].ToString()));
            SaveAuctionAttachments(int.Parse(Session[Constant.SESSION_AUCTIONREFNO].ToString().Trim()));
        }

        if (hdnDetailNo.Value.Length > 0)
        {
            //for conversion item only, tag the bid item saved as draft on auction to prevet multiple drafts
            UpdateBidItemAuctionInfo(int.Parse(hdnDetailNo.Value.ToString().Trim()), 1, int.Parse(hdnAuctionRefNo.Value.ToString()));
        }

        if (Session[Constant.SESSION_BIDDETAILNO] != null)
        {
            Session[Constant.SESSION_BIDDETAILNO] = null;
            Session[Constant.SESSION_BIDREFNO] = null;
        }
        Response.Redirect("draftauctiondetails.aspx");        
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (hdnDetailNo.Value.Length > 0)
        {          
            // if from conversion, set to approved automatically
            SaveAuction(Constant.AUCTION_STATUS_APPROVED.ToString());
            SaveAuctionDetails(Constant.AUCTION_STATUS_APPROVED.ToString());

            if (hdnAuctionRefNo.Value.Length > 0)
            {
                DeleteExistingAuctionAttachments(int.Parse(hdnAuctionRefNo.Value.Trim()));
                SaveAuctionAttachments(int.Parse(hdnAuctionRefNo.Value.Trim()));
            }
            else
            {
                DeleteExistingAuctionAttachments(int.Parse(Session[Constant.SESSION_AUCTIONREFNO].ToString()));
                SaveAuctionAttachments(int.Parse(Session[Constant.SESSION_AUCTIONREFNO].ToString().Trim()));
            }

            Session[Constant.SESSION_AUCTIONREFNO] = hdnAuctionRefNo.Value.Trim();            
            Session[Constant.SESSION_BIDDETAILNO] = null;
            Session[Constant.SESSION_BIDREFNO] = null;

            if (hdnDetailNo.Value.Length > 0)
            {
                ChangeConversionStatus(int.Parse(hdnDetailNo.Value.ToString().Trim()), Constant.BIDITEM_STATUS.CONVERSION_STATUS.CONVERTED);
            }
            int auctionrefno = int.Parse(Session[Constant.SESSION_AUCTIONREFNO].ToString());
            // add participants immediately
            AuctionItemTransaction.InsertAuctionParticipants(connstring, auctionrefno.ToString());
            // send email invitations to suppliers immediately
            AuctionDetails details = GetAuctionItemDetails(auctionrefno);
            ArrayList list = AuctionItemTransaction.GetAuctionParticipants(connstring, auctionrefno);
            int failedcount = 0, successcount = 0;

            //get verndors bid tender
            GetVendorsTender(int.Parse(hdnAuctionRefNo.Value.ToString()));
            SubmitAllTenders();

            SendEmailInvitation(details, list, ref failedcount, ref successcount);

            Response.Redirect("approvedauctionevents.aspx");
        }
        else
        {
            SaveAuction(Constant.AUCTION_STATUS_SUBMITTED.ToString().Trim());
            SaveAuctionDetails(Constant.AUCTION_STATUS_SUBMITTED.ToString().Trim());

            if (hdnAuctionRefNo.Value.Length > 0)
            {
                DeleteExistingAuctionAttachments(int.Parse(hdnAuctionRefNo.Value.Trim()));
                SaveAuctionAttachments(int.Parse(hdnAuctionRefNo.Value.Trim()));
            }
            else
            {
                DeleteExistingAuctionAttachments(int.Parse(Session[Constant.SESSION_AUCTIONREFNO].ToString()));
                SaveAuctionAttachments(int.Parse(Session[Constant.SESSION_AUCTIONREFNO].ToString().Trim()));
            }
                       
            Session[Constant.SESSION_AUCTIONREFNO] = hdnAuctionRefNo.Value.Trim();
            Session[Constant.SESSION_BIDDETAILNO] = null;
            Session[Constant.SESSION_BIDREFNO] = null;
            Response.Redirect("auctionsubmit.aspx");
        }
    }

    protected void btnAddSubItem_Click(object sender, EventArgs e)
    {
        hdnAddNewRow.Value = "true";
        AddNewItem();
    }

    private void RemoveFromBidItemList(int id)
    {
        DataTable dtAuctionItems = (DataTable)ViewState["AuctionEventItems"];
        int toBeRemoved = -1;

        for (int i = 0; i < dtAuctionItems.Rows.Count; i++)
        {
            if (dtAuctionItems.Rows[i]["RecNum"].ToString() == id.ToString())
            {
                toBeRemoved = i;
                break;
            }
        }

        if (toBeRemoved > -1)
            dtAuctionItems.Rows.RemoveAt(toBeRemoved);

        ViewState["AuctionEventItems"] = dtAuctionItems;

        gvAuctionItems.DataSource = dtAuctionItems;
        gvAuctionItems.DataBind();

    }
    private DataTable CreateAttachmentsTable()
    {
        DataTable dtAttachments = new DataTable();
        DataColumn dc;
        dc = new DataColumn("ID", typeof(System.Int32));
        dtAttachments.Columns.Add(dc);
        dc = new DataColumn("Original", typeof(System.String));
        dtAttachments.Columns.Add(dc);
        dc = new DataColumn("Actual", typeof(System.String));
        dtAttachments.Columns.Add(dc);
        dc = new DataColumn("Attached", typeof(System.Int32));
        dtAttachments.Columns.Add(dc);

        return dtAttachments;
    }

    private DataTable CreateEmptyAttachmentsTable()
    {
        DataTable dtAttachments = new DataTable();
        DataColumn dc;
        dc = new DataColumn("ID", typeof(System.Int32));
        dtAttachments.Columns.Add(dc);
        dc = new DataColumn("Original", typeof(System.String));
        dtAttachments.Columns.Add(dc);
        dc = new DataColumn("Actual", typeof(System.String));
        dtAttachments.Columns.Add(dc);
        dc = new DataColumn("Attached", typeof(System.Int32));
        dtAttachments.Columns.Add(dc);

        //DataRow dr = dtAttachments.NewRow();
        //dr["ID"] = 0;
        //dr["Original"] = "";
        //dr["Actual"] = "";
        //dr["Attached"] = 1;
        //dtAttachments.Rows.Add(dr);

        return dtAttachments;
    }

    private void InitializeRowsForGridViews()
    {
        #region Attachments
        DataTable dtAttachments = CreateAttachmentsTable();
        // add empty row
        AddEmptyAttachmentRow(ref dtAttachments);
        // save to viewstate
        ViewState["AuctionAttachments"] = dtAttachments;
        // bind to gridview
        gvFileAttachment.DataSource = dtAttachments;
        gvFileAttachment.DataBind();
        FillAuctionAttachments(ref dtAttachments);
        // save to viewstate
        ViewState["AuctionAttachments"] = dtAttachments;
        ViewState["AuctionExistingAttachments"] = dtAttachments;
        // bind to gridview
        gvFileAttachment.DataSource = dtAttachments;
        gvFileAttachment.DataBind();
        #endregion

    }

    private void InitializeEmptyRowsForGridViews()
    {   
        #region Attachments
        DataTable dtAttachments = CreateAttachmentsTable();
        // add empty row
        AddEmptyAttachmentRow(ref dtAttachments);
        // save to viewstate
        ViewState["AuctionAttachments"] = dtAttachments;
        ViewState["AuctionExistingAttachments"] = dtAttachments;
        // bind to gridview
        gvFileAttachment.DataSource = dtAttachments;
        gvFileAttachment.DataBind();
        #endregion
    }

    private void Attach(string original, string actual)
    {
        DataTable dtAttachments = (DataTable)ViewState["AuctionAttachments"];

        DataRow dr = dtAttachments.NewRow();
        int nxtCounter = 0;
        if (dtAttachments.Rows.Count > 0)
            nxtCounter = int.Parse(dtAttachments.Rows[dtAttachments.Rows.Count - 1]["ID"].ToString()) + 1;

        dr["ID"] = nxtCounter;
        dr["Original"] = original;
        dr["Actual"] = actual;
        dr["Attached"] = 1;
        dtAttachments.Rows.Add(dr);

        ViewState["AuctionAttachments"] = dtAttachments;
        gvFileAttachment.DataSource = dtAttachments;
        gvFileAttachment.DataBind();
    }

    private void Remove(int id)
    {
        DataTable dtAttachments = (DataTable)ViewState["AuctionAttachments"];
        int toBeRemoved = -1;
        string actualfilepath = Constant.FILEATTACHMENTSFOLDERDIR;

        for (int i = 0; i < dtAttachments.Rows.Count; i++)
        {
            if (dtAttachments.Rows[i]["ID"].ToString() == id.ToString())
            {
                toBeRemoved = i;
                actualfilepath += dtAttachments.Rows[i]["Actual"].ToString();
                break;
            }
        }
        if (toBeRemoved > -1)
        {
            // remove from datatable
            dtAttachments.Rows.RemoveAt(toBeRemoved);

            // remove the actual file
            FileInfo fInfo = new FileInfo(actualfilepath);
            if (fInfo.Exists)
                fInfo.Delete();
        }

        ViewState["AuctionAttachments"] = dtAttachments;

        gvFileAttachment.DataSource = dtAttachments;
        gvFileAttachment.DataBind();
    }

    private bool SaveAuctionAttachments(int auctionrefno)
    {
        bool isSuccessful = false;
        try
        {
            DataTable dtAttachments = (DataTable)ViewState["AuctionAttachments"];
            string s = string.Empty;

            for (int i = 1; i < dtAttachments.Rows.Count; i++)
            {
                s += SaveAuctionAttachments(auctionrefno, int.Parse(Session[Constant.SESSION_USERID].ToString()), dtAttachments.Rows[i]["Original"].ToString(), dtAttachments.Rows[i]["Actual"].ToString());
            }
            isSuccessful = s.IndexOf("0") >= 0 ? false : true;
        }
        catch
        {
            isSuccessful = false;
        }
        return isSuccessful;
    }

    protected void gvFileAttachment_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "Attach":
                {
                    if (gvFileAttachment.FooterRow != null)
                    {
                        FileUpload fu = (FileUpload)gvFileAttachment.FooterRow.FindControl("fileUpload");
                        Literal litMsg = (Literal)gvFileAttachment.FooterRow.FindControl("addAttachmentMsg");
                        if (fu.HasFile)
                        {
                            FileInfo fInfo = new FileInfo(fu.PostedFile.FileName);

                            if (!FileUploadHelper.IsFileForbidden(fInfo.Extension))
                            {
                                int fileSize = (fu.PostedFile.ContentLength + 512) / 1024;
                                int maxFileSize = int.Parse(ConfigurationManager.AppSettings["MaxFileSize"].ToString());

                                if (fileSize < maxFileSize)
                                {
                                    string original = fu.FileName.ToString().Trim();
                                    string actual = FileUploadHelper.GetAlternativeFileName(fInfo.Extension);

                                    try
                                    {
                                        fu.SaveAs(Constant.FILEATTACHMENTSFOLDERDIR + actual);
                                        Attach(original, actual);
                                    }
                                    catch
                                    {
                                        litMsg.Text = BR + "&nbsp;&nbsp;&nbsp;" + BULLET + " File cannot be uploaded.";
                                    }
                                }
                                else
                                {
                                    litMsg.Text = BR + "&nbsp;&nbsp;&nbsp;" + BULLET + " File size exceeds limit(" + maxFileSize + "KB).";
                                }
                            }
                            else
                            {
                                litMsg.Text = BR + "&nbsp;&nbsp;&nbsp;" + BULLET + " File cannot be uploaded. The file type is forbidden to be uploaded.";
                            }
                        }
                    }
                } break;
            case "Remove":
                {
                    Remove(int.Parse(e.CommandArgument.ToString()));
                } break;
        }
    }

    private void FillAuctionAttachments(ref DataTable dtAttachments)
    {
        IEnumerator iEnum = dsAttachments.Select(DataSourceSelectArguments.Empty).GetEnumerator();

        while (iEnum.MoveNext())
        {
            DataRow dr = dtAttachments.NewRow();
            DataRowView dRView = (DataRowView)iEnum.Current;
            dr["ID"] = dRView["FileUploadId"].ToString();
            dr["Original"] = dRView["OriginalFileName"].ToString();
            dr["Actual"] = dRView["ActualFileName"].ToString();
            dr["Attached"] = 1;
            dtAttachments.Rows.Add(dr);
        }
    }

    private void AddEmptyAttachmentRow(ref DataTable dtAttachments)
    {
        DataRow dr = dtAttachments.NewRow();
        int nxtCounter = 0;
        if (dtAttachments.Rows.Count > 0)
            nxtCounter = int.Parse(dtAttachments.Rows[dtAttachments.Rows.Count - 1]["ID"].ToString()) + 1;

        dr["ID"] = nxtCounter;
        dr["Original"] = "";
        dr["Actual"] = "";
        dr["Attached"] = 0;
        dtAttachments.Rows.Add(dr);
    }

    protected bool IsAttached(string isattached)
    {
        return isattached == "1" ? true : false;
    }

    private string SaveAuctionAttachments(int auctionrefno, int buyerid, string originalfilename, string actualfilename)
    {
        SqlConnection sqlConnect = new SqlConnection(connstring);
        SqlTransaction sqlTransact = null;
        string isSuccessful = string.Empty;

        try
        {
            sqlConnect.Open();
            sqlTransact = sqlConnect.BeginTransaction();

            SqlParameter[] sqlParams = new SqlParameter[5];
            sqlParams[0] = new SqlParameter("@AuctionRefNo", SqlDbType.Int);
            sqlParams[0].Value = auctionrefno;
            sqlParams[1] = new SqlParameter("@BuyerId", SqlDbType.Int);
            sqlParams[1].Value = buyerid;
            sqlParams[2] = new SqlParameter("@OriginalFileName", SqlDbType.VarChar);
            sqlParams[2].Value = originalfilename;
            sqlParams[3] = new SqlParameter("@ActualFileName", SqlDbType.VarChar);
            sqlParams[3].Value = actualfilename;
            sqlParams[4] = new SqlParameter("@FileUploadID", SqlDbType.Int);
            sqlParams[4].Direction = ParameterDirection.Output;

            SqlHelper.ExecuteNonQuery(connstring, CommandType.StoredProcedure, "sp_InsertAuctionAttachment", sqlParams);

            sqlTransact.Commit();

            int r = int.Parse(sqlParams[4].Value.ToString().Trim());
            isSuccessful = (r <= 0 ? "0" : "1");
        }
        catch
        {
            sqlTransact.Rollback();
            isSuccessful = "0";
        }
        finally
        {
            sqlConnect.Close();
        }
        return isSuccessful;
    }

    private void DeleteAttachmentsFiles(int id)
    {
        DataTable dtAttachments = (DataTable)ViewState["AuctionAttachments"];
        int toBeRemoved = -1;
        string actualfilepath = Constant.FILEATTACHMENTSFOLDERDIR;

        for (int i = 0; i < dtAttachments.Rows.Count; i++)
        {
            if (dtAttachments.Rows[i]["ID"].ToString() == id.ToString())
            {
                toBeRemoved = i;
                actualfilepath += dtAttachments.Rows[i]["Actual"].ToString();
                break;
            }
        }
        if (toBeRemoved > -1)
        {
            // remove from datatable
            //dtAttachments.Rows.RemoveAt(toBeRemoved);

            // remove the actual file
            FileInfo fInfo = new FileInfo(actualfilepath);
            if (fInfo.Exists)
                fInfo.Delete();
        }

        //ViewState["BidEventAttachments"] = dtAttachments;

        gvFileAttachment.DataSource = dtAttachments;
        gvFileAttachment.DataBind();
    }

    private void DeleteAuctionAttachments(int auctionrefno, int fileuploadid)
    {
        SqlConnection sqlConnect = new SqlConnection(connstring);
        SqlTransaction sqlTransact = null;

        try
        {
            sqlConnect.Open();
            sqlTransact = sqlConnect.BeginTransaction();

            SqlParameter[] sqlParams = new SqlParameter[3];
            sqlParams[0] = new SqlParameter("@AuctionRefNo", SqlDbType.Int);
            sqlParams[0].Value = auctionrefno;
            sqlParams[1] = new SqlParameter("@FileUploadID", SqlDbType.Int);
            sqlParams[1].Value = fileuploadid;
            sqlParams[2] = new SqlParameter("@Result", SqlDbType.Int);
            sqlParams[2].Direction = ParameterDirection.Output;

            SqlHelper.ExecuteNonQuery(connstring, CommandType.StoredProcedure, "sp_DeleteAuctionAttachments", sqlParams);

            sqlTransact.Commit();
            sqlConnect.Close();
        }
        catch
        {
            sqlTransact.Rollback();
        }
        finally
        {
            sqlConnect.Close();
        }


    }

    private void DeleteExistingAuctionAttachments(int auctionrefno)
    {
        try
        {
            DataTable dtAttachments = (DataTable)ViewState["AuctionExistingAttachments"];

            for (int i = 0; i < dtAttachments.Rows.Count; i++)
            {
                string fid = dtAttachments.Rows[i]["ID"].ToString();

                if (fid != "")
                {
                    DeleteAuctionAttachments(auctionrefno, int.Parse(fid));
                    //DeleteAttachmentsFiles(int.Parse(fid));
                }
            }

        }
        catch
        {

        }

    }

    protected void lnkCancel_Click(object sender, EventArgs e)
    {
        if (Session[Constant.SESSION_LASTPAGE] != null)
            Response.Redirect(Session[Constant.SESSION_LASTPAGE].ToString());
        else
            Response.Redirect("auctions.aspx");
    }

    private AuctionDetails GetAuctionItemDetails(int auctionrefno)
    {
        DataTable dt = SqlHelper.ExecuteDataset(connstring, "sp_GetAuctionInvitationInfo", new SqlParameter[] { new SqlParameter("@AuctionRefNo", auctionrefno) }).Tables[0];
        AuctionDetails item = new AuctionDetails();

        if (dt.Rows.Count > 0)
            item = new AuctionDetails(dt.Rows[0]);

        return item;
    }
        
    protected void btnSelectAll_ServerClick(object sender, EventArgs e)
    {
        txtSuppliers.Text = "";

        while (lstSupplierA.Items.Count > 0)
        {
            // get topmost item, at a time
            ListItem li = lstSupplierA.Items[0];
            li.Selected = false;
            // add to the right list
            lstSupplierB.Items.Add(li);
            // remove from left list
            lstSupplierA.Items.Remove(li);            
        }

        for (int i = 0; i < lstSupplierB.Items.Count; i++)
        {
            txtSuppliers.Text = txtSuppliers.Text + lstSupplierB.Items[i].Value.ToString()  + "," ; 
        }

        txtSuppliers.Text = txtSuppliers.Text.Remove(txtSuppliers.Text.Length - 1, 1);

    }

    protected void btnDeselectAll_ServerClick(object sender, EventArgs e)
    {
        while (lstSupplierB.Items.Count > 0)
        {
            // get topmost item, at a time
            ListItem li = lstSupplierB.Items[0];
            li.Selected = false;
            // add to the left list
            lstSupplierA.Items.Add(li);
            // remove from right list
            lstSupplierB.Items.Remove(li);
        }
        txtSuppliers.Text = "";
    }

    protected void btnSelectOne_ServerClick(object sender, EventArgs e)
    {
        ListItemCollection listCollection = new ListItemCollection();
        // get selected items
        for (int i = 0; i < lstSupplierA.Items.Count; i++)
        {
            ListItem li = lstSupplierA.Items[i];
            if (li.Selected)
            {
                listCollection.Add(li);

                if (txtSuppliers.Text.Trim().Length == 0)
                {
                    txtSuppliers.Text = lstSupplierA.Items[i].Value.ToString();
                }
                else
                {
                    txtSuppliers.Text = txtSuppliers.Text + "," + lstSupplierA.Items[i].Value.ToString();
                    //string vendorids = txtSuppliers.Text;
                    //txtSuppliers.Text = vendorids.Replace(", ", "");
                }                
            }
        }
        // add selected items to the right, then remove it from the left
        for (int j = 0; j < listCollection.Count; j++)
        {
            ListItem li = listCollection[j];
            li.Selected = false;
            // add to the right
            lstSupplierB.Items.Add(li);
            // remove from the left
            lstSupplierA.Items.Remove(li);           
        }
    }

    protected void btnDeselectOne_ServerClick(object sender, EventArgs e)
    {
        ListItemCollection listCollection = new ListItemCollection();
        // get selected items
        for (int i = 0; i < lstSupplierB.Items.Count; i++)
        {
            ListItem li = lstSupplierB.Items[i];
            if (li.Selected)
            {
                if (lstSupplierB.Items.Count == 1)
                {
                    string vendorids = txtSuppliers.Text;
                    txtSuppliers.Text = vendorids.Replace(lstSupplierB.Items[i].Value.ToString(), "");
                }
                else
                {
                    string vendorids = txtSuppliers.Text + ",";
                    txtSuppliers.Text = vendorids.Replace(lstSupplierB.Items[i].Value.ToString() + ",", "");
                    string vendorids2 = txtSuppliers.Text;
                    txtSuppliers.Text = vendorids2.Remove(vendorids2.Length - 1, 1);
                }
                listCollection.Add(li);
            }
        }
        // add selected items to the left, then remove it from the right
           for (int j = 0; j < listCollection.Count; j++)
           {
               ListItem li = listCollection[j];
               li.Selected = false;
               // add to the left
               lstSupplierA.Items.Add(li);
               // remove from the right
               lstSupplierB.Items.Remove(li);
           }
    }

    private void FillSuppliersB()
    {
        IEnumerator iEnum = dsParticipants.Select(DataSourceSelectArguments.Empty).GetEnumerator();

        while (iEnum.MoveNext())
        {
            DataRowView dRView = (DataRowView)iEnum.Current;

            ListItem li = new ListItem(dRView["AccType"].ToString() + dRView["Supplier"].ToString(), dRView["VendorId"].ToString());

            lstSupplierA.Items.Remove(li);
            lstSupplierB.Items.Add(li);

            if (lstSupplierB.Items.Count == 0)
            {
                txtSuppliers.Text = txtSuppliers.Text + dRView["VendorId"].ToString();
            }
            else
            {
                txtSuppliers.Text = txtSuppliers.Text + dRView["VendorId"].ToString() + ",";
            }                
        }

        string vendorids = txtSuppliers.Text;
	if (txtSuppliers.Text != "")
        txtSuppliers.Text = vendorids.Remove(vendorids.Length - 1, 1);        
    }

    private void FillInvitedSuppliersB()
    {
        IEnumerator iEnum = dsInvitedParticipants.Select(DataSourceSelectArguments.Empty).GetEnumerator();

        while (iEnum.MoveNext())
        {
            DataRowView dRView = (DataRowView)iEnum.Current;

            ListItem li = new ListItem(dRView["AccType"].ToString() + dRView["Supplier"].ToString(), dRView["VendorId"].ToString());

            lstSupplierA.Items.Remove(li);
            lstSupplierB.Items.Add(li);

            if (lstSupplierB.Items.Count == 0)
            {
                txtSuppliers.Text = txtSuppliers.Text + dRView["VendorId"].ToString();
            }
            else
            {
                txtSuppliers.Text = txtSuppliers.Text + dRView["VendorId"].ToString() + ",";
            }
        }

        string vendorids = txtSuppliers.Text;
	if (txtSuppliers.Text != "")
        txtSuppliers.Text = vendorids.Remove(vendorids.Length - 1, 1);
    }

    private void AddOTLS()
    {
        if (hdnVendor.Value != "")
        {
            string vendor = hdnVendor.Value.ToString();

            string[] arrvendor = vendor.Split(new char[] { ';' });

            ListItem li = new ListItem(arrvendor[1].ToString(), arrvendor[0].ToString());

            lstSupplierB.Items.Add(li);
            hdnVendor.Value = "";
        }
    }

    protected void lstSupplierA_DataBound(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session[Constant.SESSION_AUCTIONREFNO] != null)
            {
              RemoveDuplicateA();
            }
            if (hdnDetailNo.Value != "")
            {
                RemoveDuplicateSuppliersA();
            }
        }

    }

    private void RemoveDuplicateA()
    {
        IEnumerator iEnum = dsParticipants.Select(DataSourceSelectArguments.Empty).GetEnumerator();

        while (iEnum.MoveNext())
        {
            DataRowView dRView = (DataRowView)iEnum.Current;

            ListItem li = new ListItem(dRView["Supplier"].ToString(), dRView["VendorId"].ToString());

            lstSupplierA.Items.Remove(li);
        }
    }

    private void RemoveDuplicateSuppliersA()
    {
        IEnumerator iEnum = dsInvitedParticipants.Select(DataSourceSelectArguments.Empty).GetEnumerator();

        while (iEnum.MoveNext())
        {
            DataRowView dRView = (DataRowView)iEnum.Current;

            ListItem li = new ListItem(dRView["Supplier"].ToString(), dRView["VendorId"].ToString());

            lstSupplierA.Items.Remove(li);
        }
    }

    public string SubmitBid(int auctiondetailno, int vendorid, float bidprice)
    {
        SqlConnection sqlConnect = new SqlConnection(connstring);
        SqlTransaction sqlTransact = null;
        string success = "00";

        try
        {
            sqlConnect.Open();
            sqlTransact = sqlConnect.BeginTransaction();

            SqlParameter[] sqlParams = new SqlParameter[3];
            sqlParams[0] = new SqlParameter("@AuctionDetailNo", SqlDbType.Int);
            sqlParams[1] = new SqlParameter("@Vendorid", SqlDbType.Int);
            sqlParams[2] = new SqlParameter("@Bid", SqlDbType.Money);
            sqlParams[0].Value = auctiondetailno;
            sqlParams[1].Value = vendorid;
            sqlParams[2].Value = bidprice;

            string str = SqlHelper.ExecuteScalar(sqlTransact, "sp_SubmitVendorsBid", sqlParams).ToString();

            sqlTransact.Commit();

            if (str.Trim() != "")
            {
                success = str.Trim();
            }
        }
        catch
        {
            sqlTransact.Rollback();
            success = "00";
        }
        finally
        {
            sqlConnect.Close();
        }
        return success;
    }

    private bool SubmitAllTenders()
    {
        bool isSuccessful = false;
        try
        {
            DataTable dtVendorsTenders = (DataTable)ViewState["VendorsTenderDetails"];
            string s = string.Empty;

            for (int i = 0; i < dtVendorsTenders.Rows.Count; i++)
            {
                s += SubmitBid(int.Parse(dtVendorsTenders.Rows[i]["AuctionDetailNo"].ToString()), int.Parse(dtVendorsTenders.Rows[i]["VendorId"].ToString()), float.Parse(dtVendorsTenders.Rows[i]["BidPrice"].ToString()));
            }
            isSuccessful = s.IndexOf("0") >= 0 ? false : true;
        }
        catch
        {
            isSuccessful = false;
        }
        return isSuccessful;
    }

    private void GetVendorsTender(int vAuctionRefNo)
    {
        SqlParameter[] sqlparams = new SqlParameter[1];
        sqlparams[0] = new SqlParameter("@AuctionRefNo", SqlDbType.Int);
        sqlparams[0].Value = Int32.Parse(vAuctionRefNo.ToString().Trim());

        DataSet vendorData = SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "sp_GetAllVendorsTender", sqlparams);

        DataTable vendorDataTable = vendorData.Tables[0];
        ViewState["VendorsTenderDetails"] = vendorDataTable;
    }

    #region Email/SMS Invitation
    private string CreateInvitationBody(AuctionDetails auctiondetails, AuctionParticipant participant)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("<tr><td align='right'><h5>" + DateTime.Now.ToLongDateString() + "</h5></td></tr>");
        sb.Append("<tr><td align='center'><h3>INVITATION TO AUCTION</h3></td></tr>");
        sb.Append("<tr>");
        sb.Append("<td valign='top'>");
        sb.Append("<p>");
        sb.Append("<b>TO&nbsp&nbsp;:&nbsp&nbsp;<u>" + participant.Name + "</u></b>");
        sb.Append("<br /><br />");
        sb.Append("Good Day!");
        sb.Append("<br /><br />");
        sb.Append("We are glad to inform you that you have been invited to participate in an online auction event which was initiated by Trans-Asia / Commnunications.");
        sb.Append("</p>");

        sb.Append("<table style='font-size: 12px;width:100%;'>");
        sb.Append("<tr>");
        sb.Append("<td width='10px'></td>");
        sb.Append("<td style='font-weight:bold;width:20px;'>1.</td>");
        sb.Append("<td style='font-weight:bold;'>Auction Description</td>");
        sb.Append("</tr>");
        sb.Append("<tr>");
        sb.Append("<td width='30px' colspan='2'></td>");
        sb.Append("<td>" + auctiondetails.Description + "</td>");
        sb.Append("</tr>");
        sb.Append("<tr><td height='10px' colspan='3'></td></tr>");

        sb.Append("<tr>");
        sb.Append("<td width='10px'></td>");
        sb.Append("<td style='font-weight:bold;width:20px;'>2.</td>");
        sb.Append("<td style='font-weight:bold;'>Schedule of Auction Event</td>");
        sb.Append("</tr>");
        sb.Append("<tr>");
        sb.Append("<td width='30px' colspan='2'></td>");
        sb.Append("<td>");
        sb.Append("Confirmation Deadline : " + FormattingHelper.FormatDateToString(auctiondetails.ConfirmationDeadline) + "<br />");
        sb.Append("Start Date & Time : " + FormattingHelper.FormatDateToLongString(auctiondetails.StartDateTime) + "<br />");
        sb.Append("End Date & Time : " + FormattingHelper.FormatDateToLongString(auctiondetails.EndDateTime) + "<br />");
        sb.Append("Duration : " + auctiondetails.Duration + "<br />");
        sb.Append("</td>");
        sb.Append("</tr>");
        sb.Append("<tr><td height='10px' colspan='3'></td></tr>");

        sb.Append("<tr>");
        sb.Append("<td width='10px'></td>");
        sb.Append("<td style='font-weight:bold;width:20px;'>3.</td>");
        sb.Append("<td style='font-weight:bold;'>Payment Details</td>");
        sb.Append("</tr>");
        sb.Append("<tr>");
        sb.Append("<td width='30px' colspan='2'></td>");
        sb.Append("<td>");
        sb.Append("<ul>");
        sb.Append("<li>Payment Terms - depends on the item to be purchased</li>");
        sb.Append("<br />");
        sb.Append("<li>Billing Details</li>");
        sb.Append("<ul>");
        sb.Append("<li>Contact Person: Rose Soteco T# 730 2413</li>");
        sb.Append("<li>Contact Details: 2F GT Plaza Tower 1, Pioneer cor Madison Sts., Mandaluyong City</li>");
        sb.Append("</ul>");
        sb.Append("</ul>");
        sb.Append("</td>");
        sb.Append("</tr>");
        sb.Append("<tr><td height='10px' colspan='3'></td></tr>");

        sb.Append("<tr>");
        sb.Append("<td width='10px'></td>");
        sb.Append("<td style='font-weight:bold;width:20px;'>4.</td>");
        sb.Append("<td style='font-weight:bold;'>Bid Price Details</td>");
        sb.Append("</tr>");
        sb.Append("<tr>");
        sb.Append("<td width='30px' colspan='2'></td>");
        sb.Append("<td>The bid price submitted by the supplier shall be exclusive of VAT.</td>");
        sb.Append("</tr>");
        sb.Append("<tr><td height='10px' colspan='3'></td></tr>");

        sb.Append("<tr>");
        sb.Append("<td width='10px'></td>");
        sb.Append("<td style='font-weight:bold;width:20px;'>5.</td>");
        sb.Append("<td style='font-weight:bold;'>Price Validity</td>");
        sb.Append("</tr>");
        sb.Append("<tr>");
        sb.Append("<td width='30px' colspan='2'></td>");
        sb.Append("<td>");
        sb.Append("The price quoted must be valid and firm for a period of 90 days.");
        sb.Append("No change in price quoted shall be allowed after bid submission unless negotiated by Trans-Asia / Commnunications.");
        sb.Append("</td.");
        sb.Append("</tr>");
        sb.Append("<tr><td height='10px' colspan='3'></td></tr>");

        sb.Append("<tr>");
        sb.Append("<td width='10px'></td>");
        sb.Append("<td style='font-weight:bold;width:20px;'>6.</td>");
        sb.Append("<td style='font-weight:bold;'>Price Confirmation</td>");
        sb.Append("</tr>");
        sb.Append("<tr>");
        sb.Append("<td width='30px' colspan='2'></td>");
        sb.Append("<td>");
        sb.Append("All bidders shall e-mail the price breakdown in Acrobat Format (i.e. PDF) according to");
        sb.Append("the final bid price submitted during the auction event not later than 24 hours after the");
        sb.Append("event has ended.  The sum of the breakdown must be equal to the supplier’s final bid");
        sb.Append("price submitted during the actual auction event.  Any attempt to submit a breakdown");
        sb.Append("which totals significantly higher, or lower than the final bid price during the event");
        sb.Append("may incur sanctions from Trans-Asia / Commnunications.");
        sb.Append("</td>");
        sb.Append("</tr>");
        sb.Append("<tr><td height='10px' colspan='3'></td></tr>");

        sb.Append("<tr>");
        sb.Append("<td width='10px'></td>");
        sb.Append("<td style='font-weight:bold;width:20px;'>7.</td>");
        sb.Append("<td style='font-weight:bold;'>Grounds for Invalidating Bids</td>");
        sb.Append("</tr>");
        sb.Append("<tr>");
        sb.Append("<td width='30px' colspan='2'></td>");
        sb.Append("<td>");
        sb.Append("A supplier's bid may be invalidated under any of the following circumstances:");
        sb.Append("<ul>");
        sb.Append("<li>Incomplete bid documents</li>");
        sb.Append("<li>Late or non-transmittal of the price breakdown of the final auction tender</li>");
        sb.Append("</ul>");
        sb.Append("</td>");
        sb.Append("</tr>");
        sb.Append("<tr><td height='10px' colspan='3'></td></tr>");

        sb.Append("<tr>");
        sb.Append("<td width='10px'></td>");
        sb.Append("<td style='font-weight:bold;width:20px;'>8.</td>");
        sb.Append("<td style='font-weight:bold;'>Awarding of Bid</td>");
        sb.Append("</tr>");
        sb.Append("<tr>");
        sb.Append("<td width='30px' colspan='2'></td>");
        sb.Append("<td>");
        sb.Append("The lowest/highest bidder is not necessarily the winning bidder. Trans-Asia / Commnunications shall not be bound to assign any reason for not accepting any bid or accepting it in part.");
        sb.Append("Bids are still subject to further ecaluation. Trans-Asia / Commnunications shall award the winning supplier through a Purchase Order/Sales Order.");
        sb.Append("</td>");
        sb.Append("</tr>");
        sb.Append("<tr><td height='10px' colspan='3'></td></tr>");

        sb.Append("<tr>");
        sb.Append("<td width='10px'></td>");
        sb.Append("<td style='font-weight:bold;width:20px;'>9.</td>");
        sb.Append("<td style='font-weight:bold;'>Penalties (depends on the items to be purchased)</td>");
        sb.Append("</tr>");
        sb.Append("<tr><td height='10px' colspan='3'></td></tr>");
        sb.Append("</table>");

        sb.Append("<p>");
        sb.Append("To know more about this auction, click <a href='" + ConfigurationManager.AppSettings["ServerUrl"] + "web/auctions/auctiondetails.aspx?aid=" + HttpUtility.UrlEncode(EncryptionHelper.Encrypt(auctiondetails.ID.ToString())) + "' target='_blank'>here</a>. ");
        sb.Append("<br />");
        sb.Append("To confirm/decline your invitation, click <a href='" + ConfigurationManager.AppSettings["ServerUrl"] + "web/auctions/confirmauctionevent.aspx?aid=" + HttpUtility.UrlEncode(EncryptionHelper.Encrypt(auctiondetails.ID.ToString())) + "' target='_blank'>here</a>.");
        sb.Append("<br />");
        sb.Append("<a href='" + ConfigurationManager.AppSettings["ServerUrl"] + "rules.htm' target='_blank' title='Click here or copy the link'>Rules and Regulations</a> : " + ConfigurationManager.AppSettings["ServerUrl"] + "rules.htm");
        sb.Append("<br /><br />");
        sb.Append("######################################################################################<br />");
        sb.Append("&nbsp;Credentials:<br />");
        sb.Append("&nbsp;&nbsp;&nbsp;Username: " + participant.Username + "<br />");
        sb.Append("&nbsp;&nbsp;&nbsp;Ticket: " + EncryptionHelper.Decrypt(participant.EncryptedTicket) + "<br /><br />");
        sb.Append("&nbsp;Notes:<br />");
        sb.Append("&nbsp;&nbsp;&nbsp;Ticket and password are CASE SENSITIVE.<br />");
        sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Ticket is for confirming/declining/participating an auction.<br />");
        sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Ticket is different for each supplier for each auction event.<br />");
        sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Password is for login.<br />");
        sb.Append("&nbsp;&nbsp;&nbsp;Username is NOT CASE SENSITIVE.<br />");
        sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;If you don't know or forgot your password, go to eBid login page and click forgot password.<br />");
        sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Use the username provided. Click Send. Your password will be sent to this email address.<br />");
        sb.Append("######################################################################################<br />");
        sb.Append("<br /><br /><br />");
        sb.Append("Sincerely Yours,");
        sb.Append("<br /><br />");
        sb.Append(auctiondetails.Creator);
        sb.Append("<br /><br />");
        sb.Append("</p>");
        sb.Append("</td>");
        sb.Append("</tr>");

        return MailTemplate.IntegrateBodyIntoTemplate(sb.ToString());
    }

    private string CreateSMSInvitationBody(AuctionDetails auctiondetails, AuctionParticipant participant)
    {
        return String.Format("You are invited to participate in an auction event;Ref. No.:{0},initiated by Trans-Asia . Deadline: {2} Start Date: {1}",auctiondetails.ID,auctiondetails.StartDateTime.ToString("MM/dd/yyyy hh:mm tt"),auctiondetails.ConfirmationDeadline.ToString("MM/dd/yyyy"));
    }

    private bool SendEmailInvitation(AuctionDetails auctiondetails, ArrayList recipients, ref int failedcount, ref int successcount)
    {
        bool success = false;
        string subject = "Trans-Asia / Commnunications : Invitation to Auction";
        failedcount = 0;
        successcount = 0;

        try
        {
            for (int i = 0; i < recipients.Count; i++)
            {
                AuctionParticipant p = (AuctionParticipant)recipients[i];

                if (!MailHelper.SendEmail(MailTemplate.GetDefaultSMTPServer(),
                        MailHelper.ChangeToFriendlyName(auctiondetails.Creator, auctiondetails.CreatorEmail),
                        MailHelper.ChangeToFriendlyName(p.Name, p.EmailAddress),
                        subject,
                        CreateInvitationBody(auctiondetails, p),
                        MailTemplate.GetTemplateLinkedResources(this)))
                {	// if sending failed					
                    failedcount++;
                    LogHelper.EventLogHelper.Log("Auction > Send Invitation : Sending Failed to " + p.EmailAddress, System.Diagnostics.EventLogEntryType.Error);
                }
                else
                {	// if sending successful
                    successcount++;
                    LogHelper.EventLogHelper.Log("Auction > Send Invitation : Email Sent to " + p.EmailAddress, System.Diagnostics.EventLogEntryType.Information);
                    // update sent mail count
                    SqlHelper.ExecuteNonQuery(connstring, "sp_SendEmailInvitation", new SqlParameter[] { new SqlParameter("@ParticipantId", p.ID) });
                }

                if (SMSHelper.AreValidMobileNumbers(p.MobileNo.Trim()))
                {
                    SMSHelper.SendSMS(new SMSMessage(CreateSMSInvitationBody(auctiondetails, p).Trim(), p.MobileNo.Trim())).ToString();
                }
            }
            success = true;
           
        }
        catch (Exception ex)
        {
            success = false;
            LogHelper.EventLogHelper.Log("Auction > Send Invitation : " + ex.Message, System.Diagnostics.EventLogEntryType.Error);
        }

        return success;
    }
    #endregion

}
