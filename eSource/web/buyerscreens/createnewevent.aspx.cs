using System;
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
using EBid.lib.user.trans;
using EBid.lib.auction.data;
using EBid.lib;
using EBid.ConnectionString;
using System.IO;
using System.Xml;
using System.Text;
using CalendarControl;
using System.Data.Sql;
using System.Data.SqlClient;
using EBid.ConnectionString;
using System.Text.RegularExpressions;

public partial class web_buyer_screens_createNewItem : System.Web.UI.Page
{
    private const string BR = "<br />";
    private const string BULLET = "&#187;";
    private const string BR_BULLET = "<br />&#187;";
    private string connstring = "";
    private string selectedCurrency = "";
    private string PRGroupName = "";

    private bool checkDate(string text)
    {
       Regex regex = new Regex(@"^((0[1-9])|(1[0-2]))\/((0[1-9])|(1[0-9])|(2[0-9])|(3[0-1]))\/(\d{4})$");
       if (regex.IsMatch(text)) 
       {
          try 
          {
              string sCommand;
              connstring =  HttpContext.Current.Session["ConnectionString"].ToString();  
              sCommand = "SELECT CONVERT(date, '" + text + "', 101) ValidDate";
              SqlDataReader oReader;
              oReader = SqlHelper.ExecuteReader(connstring, CommandType.Text, sCommand);
              if (oReader.HasRows)
              {
                  oReader.Read();
                  string sValidDate = oReader["ValidDate"].ToString();
                  oReader.Close();
                  return true;
              }
          }
          catch 
          {

              return false;
          }
          return false;
       } 
       else 
       {
          return false;
       }
    }


    protected void Page_Load(object sender, EventArgs e)
    {


        FormsAuthenticationHelper.AuthenticateUserWithReturnUrl();
        connstring = HttpContext.Current.Session["ConnectionString"].ToString();

        PageTitle.InnerText = String.Format(Constant.TITLEFORMAT, "Create New Bid Event");
        
        if (Session["AddBidEventMessage"] != null)
        {
            lblMessage.Visible = true;
            lblMessage.Text = Session["AddBidEventMessage"].ToString();
            Session["AddBidEventMessage"] = null;
        }
        else
            lblMessage.Visible = false;        

        if (!IsPostBack)
        {
            if ((Request.QueryString[Constant.QS_TASKTYPE] == "2") && (Session[Constant.SESSION_BIDREFNO] != null))
            {
                // initialize for editing
                InitializeControls();
                InitializeRowsForGridViews();
                FillBidEventComments();
                FillSuppliersB();
            }
            else
            {
                // initialize for adding
                InitializeControls();
                InitializeEmptyRowsForGridViews();
            }

            ViewState["RemovedCtr"] = "0";
            GetPrInfo();
            //Response.Write(Session[Constant.SESSION_BIDREFNO].ToString()); 
        }
        else
        {
            GetPrInfo();
        }      

        AddOTLS();
        if (Request.Form["__EVENTTARGET"] == "VSF") 
        {
            //Response.Write(GetEstimatedEventCostInPHP().ToString());
            //VSFtoSupplierB();
        }
		
    }


    void GetPrInfo()
    {
        DropDownList ddlPrInfo = (DropDownList)gvBidItems.FooterRow.FindControl("PRInfo");
        Label lbPrInfoLink = (Label)gvBidItems.FooterRow.FindControl("PRInfoLink");
        TextBox txtItem = (TextBox)gvBidItems.FooterRow.FindControl("txtItem");
        TextBox txtDescription = (TextBox)gvBidItems.FooterRow.FindControl("txtDescription");
        TextBox clndrDeliveryDate = (TextBox)gvBidItems.FooterRow.FindControl("clndrDeliveryDate");
        TextBox txtUnitOfMeasure = (TextBox)gvBidItems.FooterRow.FindControl("txtUnitOfMeasure");
        TextBox txtQuantity = (TextBox)gvBidItems.FooterRow.FindControl("txtQuantity");
        TextBox txtUnitPrice = (TextBox)gvBidItems.FooterRow.FindControl("txtUnitPrice");

        //if (ddlPrInfo.SelectedValue.ToString() != "0")
        //{
        //    SqlDataReader oReader;
        //    string query;
        //    SqlCommand cmd;
        //    SqlConnection conn;

        //    lbPrInfoLink.Text = "<a href='prItems.aspx?GroupName=" + ddlPrInfo.SelectedItem.ToString() + "' target='_blank'>" + ddlPrInfo.SelectedItem.ToString() + "</a>";
            
        //    //txtItem.Text = "sdfsdf";
        //    query = "sp_GetPRGroupAsItem";
        //    using (conn = new SqlConnection(connstring))
        //    {
        //        using (cmd = new SqlCommand(query, conn))
        //        {
        //            cmd.CommandType = CommandType.StoredProcedure; 
        //            cmd.Parameters.AddWithValue("@GroupName", ddlPrInfo.SelectedItem.ToString().Trim());
        //            conn.Open();
        //            oReader = cmd.ExecuteReader();
        //            if (oReader.HasRows)
        //            {
        //                while (oReader.Read())
        //                {
        //                    txtItem.Text = oReader["ItemCode"].ToString();
        //                    txtDescription.Text = oReader["PRDescription"].ToString();
        //                    DateTime date = DateTime.MinValue;
        //                    DateTime.TryParse(oReader["PRDate"].ToString(), out date);
        //                    //Response.Write(date.ToString("MM/dd/yyyy"));
        //                    clndrDeliveryDate.Text = date.ToString("MM/dd/yyyy");
        //                    txtUnitOfMeasure.Text = oReader["UOM"].ToString();
        //                    txtQuantity.Text = oReader["Qty"].ToString();
        //                    txtUnitPrice.Text = oReader["UnitPrice"].ToString();
        //                }
        //            }
        //        }
        //    } 
        //}
        //else
        //    lbPrInfoLink.Text = "";
        if (Request.QueryString[Constant.QS_TASKTYPE] == "2")
        {
            SqlDataReader oReader;
            string query;
            SqlCommand cmd;
            SqlConnection conn;


            //txtItem.Text = "sdfsdf";
            query = "sp_GetPRGroupOfBidrefNo";
            using (conn = new SqlConnection(connstring))
            {
                using (cmd = new SqlCommand(query, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@BidRefNo", Convert.ToInt32(Session[Constant.SESSION_BIDREFNO].ToString()));
                    conn.Open(); oReader = cmd.ExecuteReader();
                    if (oReader.HasRows)
                    {
                        while (oReader.Read())
                        {
                            lbPrInfoLink.Text = "<a href='prItems.aspx?GroupName=" + oReader["PRGroupName"].ToString() + "' target='_blank'>" + oReader["PRGroupName"].ToString() + "</a> ";
                            PRGroupName = oReader["PRGroupName"].ToString();

                        }
                    }
                }
            }
        }
    }

    void VSFtoSupplierB()
    {
        
        //string connstring = HttpContext.Current.Session["ConnectionString"].ToString();
        //string query;
        //SqlDataReader oReader;
        //lstSupplierB.Items.Clear();

        //query = "SELECT DISTINCT(t1.VendorId),  CASE   WHEN t2.Accredited = 5 THEN    'Due For Renewal - ' +  t2.VendorName  WHEN t2.Accredited = 4 THEN    'Exempted - ' +  t2.VendorName  WHEN t2.Accredited = 3 THEN    'OTS - ' +  t2.VendorName  ELSE t2.VendorName END AS VendorName,    t2.VendorName + ' ( ' + t4.SupplierTypeDesc + ' - ' + cast((CASE WHEN t2.Composite_Rating_Rate is null THEN 0 ELSE t2.Composite_Rating_Rate END) as varchar(10)) + ' - ' + STR(CASE WHEN t2.Maximum_Exposure_Amount is null THEN 0 ELSE t2.Maximum_Exposure_Amount END) + ' - ' + cast((CASE WHEN t2.Perf_Evaluation_Rate is null THEN 0 ELSE t2.Perf_Evaluation_Rate END) as varchar(10)) + ' )' AS VendorLabel, t2.Accredited FROM tblVSFDetails t1 INNER JOIN tblVendors t2 ON t1.VendorId = t2.VendorId INNER JOIN rfcSupplierType t4 ON t2.Accredited = t4.SupplierTypeId WHERE t1.VSFId = " + VSF.SelectedValue.ToString() +" AND t1.Selected = 1 ORDER BY t2.VendorName";
        //oReader = SqlHelper.ExecuteReader(connstring, CommandType.Text, query);
        //if (oReader.HasRows)
        //{
        //    while (oReader.Read())
        //    {
        //        ListItem li = new ListItem(oReader["VendorLabel"].ToString(), oReader["VendorId"].ToString());
        //        lstSupplierB.Items.Add(li);
        //    }
        //}
        ////RemoveDuplicateB();

        //if (VSF.SelectedValue.ToString() != "0")
        //{
        //    VSFLink.Text = "<a href='vsfviewOnBid.aspx?VSFId=" + VSF.SelectedValue.ToString() + "' target='_blank'>" + VSF.SelectedItem.ToString() + "</a>";
        //    SuppliersLst_Disabled();
        //    ddlCategory.Enabled = false;
        //    ddlSubCategory.Enabled = false;

        //    query = "SELECT CASE WHEN (CategoryId IS NULL) THEN ' ' ELSE CategoryId END AS CategoryId, CASE WHEN (SubCategoryId IS NULL) THEN ' ' WHEN (SubCategoryId = 0) THEN ' ' ELSE CAST(SubCategoryId AS Varchar) END AS SubCategoryId FROM tblVendorShortlistingForm WHERE VSFId = " + VSF.SelectedValue.Trim().ToString();
        //    oReader = SqlHelper.ExecuteReader(connstring, CommandType.Text, query);
        //    if (oReader.HasRows)
        //    {
        //        while (oReader.Read())
        //        {
        //            ddlCategory.SelectedValue = oReader["CategoryId"].ToString();
        //            ddlSubCategory.DataBind();
        //            ddlSubCategory.SelectedValue = oReader["SubCategoryId"].ToString();
        //            //Response.Write(oReader["CategoryId"].ToString() + ", " + oReader["SubCategoryId"].ToString());
        //        }
        //    }
        //    oReader.Close();

        //}
        //else 
        //{
        //    VSFLink.Text = "";
        //    SuppliersLst_Enabled();
        //    ddlCategory.Enabled = true;
        //    ddlSubCategory.Enabled = true;
        //}
        
    }

    void SuppliersLst_Disabled()
    {
        lstSupplierB.Enabled = false;
        btnSelectAll.Disabled = true;
        btnSelectOne.Disabled = true;
        btnDeselectOne.Disabled = true;
        btnDeselectAll.Disabled = true;
        lstSupplierA.Enabled = false;
    }
    void SuppliersLst_Enabled()
    {
        lstSupplierB.Enabled = true;
        btnSelectAll.Disabled = false;
        btnSelectOne.Disabled = false;
        btnDeselectOne.Disabled = false;
        btnDeselectAll.Disabled = false;
        lstSupplierA.Enabled = true;
    }

    protected void Page_Unload(object sender, EventArgs e)
    {
        double s = GetEstimatedEventCostInPHP();
    }

    #region INITIALIZE

    private void InitializeControls()
    {
        txtDeadlineHH.Attributes.Add("onfocus", txtDeadlineHH.ClientID + ".select();");
        txtDeadlineMM.Attributes.Add("onfocus", txtDeadlineMM.ClientID + ".select();");
        txtDeadlineSS.Attributes.Add("onfocus", txtDeadlineSS.ClientID + ".select();");
        txtDeadlineHH.Attributes.Add("style", "text-align:center;");
        txtDeadlineMM.Attributes.Add("style", "text-align:center;");
        txtDeadlineSS.Attributes.Add("style", "text-align:center;");
        ddlDeadline.SelectedValue = "PM";

        clndrDeadline.Attributes.Add("style", "text-align:center;");
        clndrPRDate.Attributes.Add("style", "text-align:center;");

        lstSupplierA.Attributes.Add("ondblclick", "__doPostBack('btnSelectOne','')");
        lstSupplierB.Attributes.Add("ondblclick", "__doPostBack('btnDeselectOne','')");

        txtPRNumber.Attributes.Add("onkeydown", "return DigitsOnly(event);");
        txtPRSubLineNumber.Attributes.Add("onkeydown", "return DigitsOnly(event);");
        txtDeadlineHH.Attributes.Add("onkeydown", "return DigitsOnly(event);");
        txtDeadlineMM.Attributes.Add("onkeydown", "return DigitsOnly(event);");
        txtDeadlineSS.Attributes.Add("onkeydown", "return DigitsOnly(event);");        

        txtDeadlineHH.Attributes.Add("onfocusout", "ResetIfEmpty(this);");
        txtDeadlineMM.Attributes.Add("onfocusout", "ResetIfEmpty(this);");
        txtDeadlineSS.Attributes.Add("onfocusout", "ResetIfEmpty(this);");

        ddlIncoterm.SelectedValue = "11";
        ddlCurrency.Attributes.Add("onchange", "ShowPHPConversion();");        
        
        Page.ClientScript.RegisterStartupScript(this.GetType(), "show1", "ShowPHPConversion();", true);
        //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "show1", "ShowPHPConversion();", true);
                
        clndrPRDate.Text = DateTime.Now.ToShortDateString();
        clndrDeadline.Text = DateTime.Now.AddDays(7.0).ToShortDateString();

        txtDeadlineHH.Text = "6";

        lnkAddOTLS.NavigateUrl = "javascript:AddOTLS('" + ddlCategory.ClientID + "', '" + ddlSubCategory.ClientID + "');";
    }
    
    private void InitializeRowsForGridViews()
    {
        FillBidEventDetails();

        #region Bid Items
        DataTable dtBidItems = CreateBidItemsTable();
        // add empty row
        AddEmptyItemsRow(ref dtBidItems);
        // save to viewstate
        ViewState["BidEventItems"] = dtBidItems;
        // bind to gridview
        gvBidItems.DataSource = dtBidItems;
        gvBidItems.DataBind();
        // fill bid event items
        FillBidEventItems(ref dtBidItems);
        // save to viewstate
        ViewState["BidEventItems"] = dtBidItems;
        ViewState["BidEventExistingItems"] = dtBidItems;
        // bind to gridview
        gvBidItems.DataSource = dtBidItems;
        gvBidItems.DataBind();
        #endregion

        #region Attachments
        DataTable dtAttachments = CreateAttachmentsTable();
        // add empty row
        AddEmptyAttachmentRow(ref dtAttachments);
        // save to viewstate
        ViewState["BidEventAttachments"] = dtAttachments;
        // bind to gridview
        gvFileAttachment.DataSource = dtAttachments;
        gvFileAttachment.DataBind();
        FillBidEventAttachments(ref dtAttachments);
         // save to viewstate
        ViewState["BidEventAttachments"] = dtAttachments;
        ViewState["BidEventExistingAttachments"] = dtAttachments;
        // bind to gridview
        gvFileAttachment.DataSource = dtAttachments;
        gvFileAttachment.DataBind();
        #endregion

        DataTable dtParticipants = CreateParticipantsTable();
        FillBidEventParticipants(ref dtParticipants);
        ViewState["BidEventParticipants"] = dtParticipants;

    }

    private void InitializeEmptyRowsForGridViews()
    {
        #region Bid Items
        DataTable dtBidItems = CreateBidItemsTable();
        // add empty row
        AddEmptyItemsRow(ref dtBidItems);
        // save to viewstate
        ViewState["BidEventItems"] = dtBidItems;
        // bind to gridview
        gvBidItems.DataSource = dtBidItems;
        gvBidItems.DataBind();
        #endregion

        #region Attachments
        DataTable dtAttachments = CreateAttachmentsTable();
        // add empty row
        AddEmptyAttachmentRow(ref dtAttachments);
        // save to viewstate
        ViewState["BidEventAttachments"] = dtAttachments;
        // bind to gridview
        gvFileAttachment.DataSource = dtAttachments;
        gvFileAttachment.DataBind();
        #endregion
    }

    #endregion

    private int GetSelectedIndex(DropDownList ddl, string start)
    {
        for(int i = 0; i< ddl.Items.Count; i++)
        {
            ListItem item = ddl.Items[i];
            if (item.Value.StartsWith(start))
                return i;
        }
        return 0;
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
        dr["Filesize"] = 0;
        dtAttachments.Rows.Add(dr);
    }

    private void AddEmptyItemsRow(ref DataTable dtBidItems)
    {
        DataRow dr = dtBidItems.NewRow();
        int nxtCounter = 0;
        if (dtBidItems.Rows.Count > 0)
            nxtCounter = int.Parse(dtBidItems.Rows[dtBidItems.Rows.Count - 1]["ID"].ToString()) + 1;

        dr["ID"] = nxtCounter;
        dr["Item"] = "";
        dr["Description"] = "";
        dr["Quantity"] = 0.0;
        dr["UnitOfMeasure"] = "";
        dr["UnitPrice"] = 0.0;
        dr["DeliveryDate"] = DateTime.Now;
        dr["SKU"] = "";
        dr["EstimatedTotal"] = 0.0;
        dr["IsEmpty"] = 0;
        dr["PRGroupName"] = "";
        dtBidItems.Rows.Add(dr);
    }

    #region FILL

    private void FillBidEventDetails()
    {
        IEnumerator iEnum = dsEventDetails.Select(DataSourceSelectArguments.Empty).GetEnumerator();

        while (iEnum.MoveNext())
        {
            DataRowView dRView = (DataRowView)iEnum.Current;
            if (dRView["BidRefNo"] != null)
            {
                lblRefNo.Text = dRView["BidRefNo"].ToString();
                lblRefNo.ForeColor = System.Drawing.Color.Black;
                lblRefNo.Font.Bold = true;
            }
            else
            {
                lblRefNo.Text = "NONE";
                lblRefNo.ForeColor = System.Drawing.Color.Gray;
                lblRefNo.Font.Bold = false;
            }
            txtItemDesc.Text = dRView["ItemDesc"].ToString();   
            txtRequestor.Text = dRView["Requestor"].ToString();
            txtPRNumber.Text = dRView["PRRefNo"].ToString();
            txtPRSubLineNumber.Text = dRView["PRSubRefNo"].ToString();
            clndrPRDate.Text = DateTime.Parse(dRView["PRDate"].ToString()).ToShortDateString();
            //Response.Write(dRView["CompanyId"].ToString());
            ddlCompany.SelectedValue = dRView["CompanyId"].ToString();
            ddlGroupDeptSec.SelectedValue = dRView["GroupDeptSec"].ToString();
            //ddlMainCategory.SelectedValue = dRView["MainCategory"].ToString();          
            ddlCategory.SelectedValue = dRView["Category"].ToString();
            if (dRView["Category"].ToString().Length > 0)
                ViewState["category"] = dRView["Category"].ToString().Trim();
            if (dRView["SubCategory"].ToString().Length > 0)
                ViewState["subcategory"] = dRView["SubCategory"].ToString().Trim();
            clndrDeadline.Text = DateTime.Parse(dRView["Deadline"].ToString()).ToShortDateString();
            txtDeadlineHH.Text = DateTime.Parse(dRView["Deadline"].ToString()).ToString("hh");
            txtDeadlineMM.Text = DateTime.Parse(dRView["Deadline"].ToString()).Minute.ToString();
            txtDeadlineSS.Text = DateTime.Parse(dRView["Deadline"].ToString()).Second.ToString();
            ddlDeadline.SelectedValue = DateTime.Parse(dRView["Deadline"].ToString()).ToString("tt");
            txtDeliverTo.Text = dRView["DeliverTo"].ToString();
            ddlIncoterm.SelectedValue = dRView["IncotermId"].ToString();            
            selectedCurrency = dRView["CurrencyId"].ToString();            
            txtTotalEventPrice.Text = dRView["EstItemValue"].ToString()!="" ? String.Format("{0:#,##0.00}", double.Parse(dRView["EstItemValue"].ToString())) : "0.00";
            chkQualifiedSourcing.Checked = bool.Parse(dRView["QualifiedSourcing"].ToString());
            //VSF.SelectedValue = dRView["VSFId"].ToString();
            //if (dRView["VSFId"].ToString() != "0") { 
            //    SuppliersLst_Disabled();
            //    ddlCategory.Enabled = false;
            //    ddlSubCategory.Enabled = false;
            //}
            //else { 
            //    SuppliersLst_Enabled();  
            //    ddlCategory.Enabled = true;
            //    ddlSubCategory.Enabled = true;
            //}
            SuppliersLst_Enabled();
            ddlCategory.Enabled = true;
            ddlSubCategory.Enabled = true;

        }
    }

    private void FillBidEventSKU()
    {
        IEnumerator iEnum = dsItemDetails.Select(DataSourceSelectArguments.Empty).GetEnumerator();
        HiddenField hdSKU = (HiddenField)gvBidItems.FooterRow.FindControl("hdnSKU");

        while (iEnum.MoveNext())
        {
            
            DataRowView dRView = (DataRowView)iEnum.Current;
            hdSKU.Value = dRView["SKU"].ToString();
           
        }
    }

    private void FillBidEventComments()
    {
        IEnumerator iEnum = dsComments.Select(DataSourceSelectArguments.Empty).GetEnumerator();
       
        while (iEnum.MoveNext())
        {

            DataRowView dRView = (DataRowView)iEnum.Current;
            txtComment.Text = dRView["Comment"].ToString();

        }
    }

    private void FillBidEventItems(ref DataTable dtBidItems)
    {
        IEnumerator iEnum = dsItemDetails.Select(DataSourceSelectArguments.Empty).GetEnumerator();
        
        while (iEnum.MoveNext())
        {
            DataRow dr = dtBidItems.NewRow();

            int nxtCounter = 0;
            if (dtBidItems.Rows.Count > 0)
                nxtCounter = int.Parse(dtBidItems.Rows[dtBidItems.Rows.Count - 1]["ID"].ToString()) + 1;
           
            DataRowView dRView = (DataRowView)iEnum.Current;
            dr["BidDetailNo"] = dRView["BidDetailNo"].ToString();
            dr["ID"] = nxtCounter;
            dr["Item"] = dRView["Item"].ToString();
            dr["Description"] = dRView["DetailDesc"].ToString();
            dr["Quantity"] = dRView["Qty"].ToString();
            dr["UnitOfMeasure"] = dRView["UnitOfMeasure"].ToString();
            dr["UnitPrice"] = dRView["UnitPrice"].ToString();
            dr["DeliveryDate"] = dRView["DeliveryDate"].ToString();
            dr["SKU"] = dRView["SKU"].ToString();
            dr["EstimatedTotal"] = dRView["TotalUnitCost"].ToString();
            dr["IsEmpty"] = 1;
            dr["PRGroupName"] = dRView["PRGroupName"].ToString();
            //hdSKU.Value = dRView["SKU"].ToString();
            dtBidItems.Rows.Add(dr);
        }
    }

    private void FillBidEventAttachments(ref DataTable dtAttachments)
    {
        IEnumerator iEnum = dsAttachments.Select(DataSourceSelectArguments.Empty).GetEnumerator();

        while (iEnum.MoveNext())
        {
            DataRow dr = dtAttachments.NewRow();
            DataRowView dRView = (DataRowView)iEnum.Current;
            dr["ID"] = dRView["FileUploadId"].ToString();
            dr["Original"] = dRView["OriginalFileName"].ToString();
            dr["Actual"] = dRView["ActualFileName"].ToString();
            dr["Filesize"] = dRView["Filesize"].ToString();
            dr["Attached"] = 1;
            dtAttachments.Rows.Add(dr);
        }
    }

    private void FillBidEventParticipants(ref DataTable dtParticipants)
    {
        IEnumerator iEnum = dsParticipants.Select(DataSourceSelectArguments.Empty).GetEnumerator();

        while (iEnum.MoveNext())
        {
            DataRow dr = dtParticipants.NewRow();
            DataRowView dRView = (DataRowView)iEnum.Current;
            dr["VendorId"] = dRView["VendorId"].ToString();
            dtParticipants.Rows.Add(dr);
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
        }
    }

    #endregion

    #region CREATE DATATABLES

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
        dc = new DataColumn("Filesize", typeof(System.Double));
        dtAttachments.Columns.Add(dc);

        return dtAttachments;
    }

    private DataTable CreateParticipantsTable()
    {
        DataTable dtParticipants = new DataTable();
        DataColumn dc;
        dc = new DataColumn("VendorId", typeof(System.String));
        dtParticipants.Columns.Add(dc);

        return dtParticipants;
    }

    private DataTable CreateBidItemsTable()
    {
        DataTable dtBidItems = new DataTable();
        DataColumn dc;
        dc = new DataColumn("BidDetailNo", typeof(System.Int32));
        dtBidItems.Columns.Add(dc);
        dc = new DataColumn("ID", typeof(System.Int32));
        dtBidItems.Columns.Add(dc);
        dc = new DataColumn("Item", typeof(System.String));
        dtBidItems.Columns.Add(dc);
        dc = new DataColumn("Description", typeof(System.String));
        dtBidItems.Columns.Add(dc);
        dc = new DataColumn("Quantity", typeof(System.Double));
        dtBidItems.Columns.Add(dc);
        dc = new DataColumn("UnitOfMeasure", typeof(System.String));
        dtBidItems.Columns.Add(dc);
        dc = new DataColumn("UnitPrice", typeof(System.Double));
        dtBidItems.Columns.Add(dc);
        dc = new DataColumn("DeliveryDate", typeof(System.DateTime));
        dtBidItems.Columns.Add(dc);
        dc = new DataColumn("SKU", typeof(System.String));
        dtBidItems.Columns.Add(dc);
        dc = new DataColumn("EstimatedTotal", typeof(System.Double));
        dtBidItems.Columns.Add(dc);
        dc = new DataColumn("IsEmpty", typeof(System.Int32));
        dtBidItems.Columns.Add(dc);
        dc = new DataColumn("PRGroupName", typeof(System.String));
        dtBidItems.Columns.Add(dc);

        return dtBidItems;
    }

    #endregion

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
                                    //string actual = FileUploadHelper.GetAlternativeFileName(fInfo.Extension);
                                    string actual = FileUploadHelper.GetNewAlternativeFileName(Session[Constant.SESSION_USERID].ToString(), fInfo.Extension);

                                    try
                                    {
                                            fu.SaveAs((Constant.FILEATTACHMENTSFOLDERDIR) + actual);
                                            Attach(original, actual, fileSize);
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

    protected void ddlMainCategory_DataBound(object sender, EventArgs e)
    {
        //ddlMainCategory.Items.Insert(0, new ListItem("---- SELECT MAIN CATEGORY ----", " "));
    }

    protected void ddlCategory_DataBound(object sender, EventArgs e)
    {
            ddlCategory.Items.Insert(0, new ListItem("---- SELECT CATEGORY ----", " "));
            if (ViewState["category"] != null)
            {
                if (ddlCategory.Items.FindByValue(ViewState["category"].ToString()) != null)
                {
                    ddlCategory.SelectedValue = ViewState["category"].ToString();
                }
            }
    }

    protected void ddlSubCategory_DataBound(object sender, EventArgs e)
    {
        ddlSubCategory.Items.Insert(0, new ListItem("---- ALL SUB CATEGORIES ----", " "));

        if (ViewState["subcategory"] != null)
        {
            if (ddlSubCategory.Items.FindByValue(ViewState["subcategory"].ToString()) != null)
            {
                ddlSubCategory.SelectedValue = ViewState["subcategory"].ToString();
            }
        }

    }

    protected void gvBidItems_DataBound(object sender, EventArgs e)
    {
        // get controls
        JSCalendar jc = (JSCalendar)gvBidItems.FooterRow.FindControl("clndrDeliveryDate");
        TextBox tbQty = (TextBox)gvBidItems.FooterRow.FindControl("txtQuantity");
        TextBox tbPrice = (TextBox)gvBidItems.FooterRow.FindControl("txtUnitPrice");
        TextBox tbPriceCents = (TextBox)gvBidItems.FooterRow.FindControl("txtUnitPriceCents");
        TextBox tbTotal = (TextBox)gvBidItems.FooterRow.FindControl("txtTotalPrice");
        TextBox tbItem = (TextBox)gvBidItems.FooterRow.FindControl("txtItem");        
        HtmlAnchor lnkSelectProducts = (HtmlAnchor)gvBidItems.FooterRow.FindControl("lnkSelectProduct");
        LinkButton lnkAddItem = (LinkButton)gvBidItems.FooterRow.FindControl("lnkAddItem");

        if (jc != null)        
            jc.Attributes.Add("style", "text-align:center;");

        string ctrlid = tbQty.ClientID.Split(new char[] { '_' })[1];
        lnkSelectProducts.Attributes.Add("onclick", "ShowProducts(" + ddlCategory.ClientID + "," + ddlSubCategory.ClientID + "," + tbItem.ClientID + ",'" + ctrlid + "');");

        tbPriceCents.Attributes.Add("style", "text-align: center;");
        tbPrice.Attributes.Add("style", "text-align: right;");
        tbQty.Attributes.Add("style", "text-align: right;");        

        string computeTotal = "ComputeTotal(" + tbQty.ClientID + "," + tbPrice.ClientID + "," + tbPriceCents.ClientID + "," + tbTotal.ClientID + ");";
        
        tbQty.Attributes.Add("onkeydown", "return DigitsOnly(event);" + computeTotal);
        tbPrice.Attributes.Add("onkeydown", "return DigitsOnlyAndTransferFocus(event, " + tbPriceCents.ClientID + ");" + computeTotal);
        tbPriceCents.Attributes.Add("onkeydown", "return DigitsOnly(event);" + computeTotal);

        tbQty.Attributes.Add("onkeyup", computeTotal);
        tbPrice.Attributes.Add("onkeyup", computeTotal);
        tbPriceCents.Attributes.Add("onkeyup", computeTotal);
        
        tbQty.Attributes.Add("onfocus", tbQty.ClientID + ".select();");
        tbPrice.Attributes.Add("onfocus", tbPrice.ClientID + ".select();");
        tbPriceCents.Attributes.Add("onfocus", tbPriceCents.ClientID + ".select();");

        tbQty.Attributes.Add("onkeypress", "return DigitsOnly(event);");
        tbPrice.Attributes.Add("onkeypress", "return DigitsOnlyAndTransferFocus(event, " + tbPriceCents.ClientID + ");");
        tbPriceCents.Attributes.Add("onkeypress", "return DigitsOnly(event);");

        tbQty.Attributes.Add("onfocusout", "ResetIfEmpty2(this);" + computeTotal);
        tbPrice.Attributes.Add("onfocusout", "ResetIfEmpty2(this);" + computeTotal);
        tbPriceCents.Attributes.Add("onfocusout", "ResetIfEmpty(this);" + computeTotal);

        tbQty.Attributes.Add("onblur", "ResetIfEmpty2(this);" + computeTotal);
        tbPrice.Attributes.Add("onblur", "ResetIfEmpty2(this);" + computeTotal);
        tbPriceCents.Attributes.Add("onblur", "ResetIfEmpty(this);" + computeTotal);

        txtItemDesc.Attributes.Add("onfocus", computeTotal);

        if (lnkAddItem != null)
            lnkAddItem.Attributes.Add("onclick", "ShowPHPConversion();");

        gvBidItems.FooterRow.Visible = false;
    }

    private void Attach(string original, string actual, double Filesize)
    {
        DataTable dtAttachments = (DataTable)ViewState["BidEventAttachments"];

        DataRow dr = dtAttachments.NewRow();
        int nxtCounter = 0;
        if (dtAttachments.Rows.Count > 0)
            nxtCounter = int.Parse(dtAttachments.Rows[dtAttachments.Rows.Count - 1]["ID"].ToString()) + 1;

        dr["ID"] = nxtCounter;
        dr["Original"] = original;
        dr["Actual"] = actual;
        dr["Attached"] = 1;
        dr["Filesize"] = Filesize;
        dtAttachments.Rows.Add(dr);

        ViewState["BidEventAttachments"] = dtAttachments;
        gvFileAttachment.DataSource = dtAttachments;
        gvFileAttachment.DataBind();
    }

    private void Remove(int id)
    {
        DataTable dtAttachments = (DataTable)ViewState["BidEventAttachments"];
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

        ViewState["BidEventAttachments"] = dtAttachments;

        gvFileAttachment.DataSource = dtAttachments;
        gvFileAttachment.DataBind();
    }

    protected bool IsAttached(string isattached)
    {
        return isattached == "1" ? true : false;
    }

    protected void btnSelectAll_ServerClick(object sender, EventArgs e)
    {
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
    }

    protected void btnSelectOne_ServerClick(object sender, EventArgs e)
    {
        ListItemCollection listCollection = new ListItemCollection();
        // get selected items
        for (int i = 0; i < lstSupplierA.Items.Count; i++)
        {
            ListItem li = lstSupplierA.Items[i];
            if (li.Selected)
                listCollection.Add(li);
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
                listCollection.Add(li);
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

    //protected void ddlMainCategory_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    lstSupplierB.Items.Clear();
    //}

    protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        lstSupplierB.Items.Clear();
    }

    protected void gvBidItems_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        Literal litMsg = (Literal)gvBidItems.FooterRow.FindControl("addItemMsg");
        switch (e.CommandName)
        {
            case "Add":
                {
                    litMsg.Text = AddToBidItemsList();
                }
                break;
            case "Remove":
                {
                    RemoveFromBidItemList(int.Parse(e.CommandArgument.ToString()));
                }
                break;
            case "EditItem":
                {
                    gvBidItems.FooterRow.Visible = true;
                    EditItem(int.Parse(e.CommandArgument.ToString()));
                }
                break;
            case "UpdateItem":
                {
                    litMsg.Text = UpdateBidEventItem();
                    gvBidItems.FooterRow.Visible = false;
                }
                break;
            case "CancelUpdate":
                {
                    CancelUpdateItem();
                    gvBidItems.FooterRow.Visible = false;
                }
                break;
        }
    }

    private string AddToBidItemsList()
    {
        // get controls        
        TextBox tbItem = (TextBox)gvBidItems.FooterRow.FindControl("txtItem");
        TextBox tbDescription = (TextBox)gvBidItems.FooterRow.FindControl("txtDescription");
        HiddenField hdSKU = (HiddenField)gvBidItems.FooterRow.FindControl("hdnSKU");
        JSCalendar jc = (JSCalendar)gvBidItems.FooterRow.FindControl("clndrDeliveryDate");                
        TextBox tbQty = (TextBox)gvBidItems.FooterRow.FindControl("txtQuantity");
        TextBox tbUOF = (TextBox)gvBidItems.FooterRow.FindControl("txtUnitOfMeasure");
        TextBox tbPrice = (TextBox)gvBidItems.FooterRow.FindControl("txtUnitPrice");        
        TextBox tbPriceCents = (TextBox)gvBidItems.FooterRow.FindControl("txtUnitPriceCents");
        TextBox tbTotal = (TextBox)gvBidItems.FooterRow.FindControl("txtTotalPrice");

        string msg = "";
        if (checkDate(jc.Text.Trim()))
        {
            if (IsBidItemInputsValid(tbItem.Text.Trim(), tbDescription.Text.Trim(), hdSKU.Value.Trim(), jc.Text.Trim(), 
                tbQty.Text.Trim(), tbUOF.Text.Trim(), tbPrice.Text.Trim() + "." + tbPriceCents.Text.Trim(), tbTotal.Text.Trim(), ref msg))
            {   // if input is ok, proceed to adding of items in the list
                DataTable dtBidItems = (DataTable)ViewState["BidEventItems"];
            
                DataRow dr = dtBidItems.NewRow();
                int nxtCounter = 0;
                if (dtBidItems.Rows.Count > 0)
                    nxtCounter = int.Parse(dtBidItems.Rows[dtBidItems.Rows.Count - 1]["ID"].ToString()) + 1;
            
                dr["ID"] = nxtCounter;
                dr["Item"] = tbItem.Text.Trim();
                dr["Description"] = tbDescription.Text.Trim();
                dr["Quantity"] = double.Parse(tbQty.Text.Trim());
                dr["UnitOfMeasure"] = tbUOF.Text.Trim();
                dr["UnitPrice"] = double.Parse(tbPrice.Text.Trim() + "." + tbPriceCents.Text.Trim());
                dr["DeliveryDate"] = DateTime.Parse(jc.Text.Trim());
	        dr["SKU"] = hdSKU.Value.Trim();
                double total = double.Parse(tbQty.Text.Trim()) * double.Parse(tbPrice.Text.Trim() + "." + tbPriceCents.Text.Trim());
                dr["EstimatedTotal"] = total;
                dr["IsEmpty"] = 1;
                //dr["PRGroupName"] = tbPRInfo.SelectedValue;
                dtBidItems.Rows.Add(dr);
                
                // add to total event cost
                double newTotal = total + double.Parse(txtTotalEventPrice.Text.Trim());
                txtTotalEventPrice.Text = String.Format("{0:#,##0.00}", newTotal);            
            
                ViewState["BidEventItems"] = dtBidItems;
                gvBidItems.DataSource = dtBidItems;
                gvBidItems.DataBind();
            
                ddlCategory.Enabled = false;
                ddlSubCategory.Enabled = false;
            }
            else
            {   // if input is not ok
            }
        }
        else {  
            msg = "Invalid Delivery Date!";
        }
        return msg;
    }

    private void EditItem(int id)
    {      
        JSCalendar jc = (JSCalendar)gvBidItems.FooterRow.FindControl("clndrDeliveryDate");
        TextBox tbQty = (TextBox)gvBidItems.FooterRow.FindControl("txtQuantity");
        TextBox tbPrice = (TextBox)gvBidItems.FooterRow.FindControl("txtUnitPrice");
        TextBox tbPriceCents = (TextBox)gvBidItems.FooterRow.FindControl("txtUnitPriceCents");
        TextBox tbTotal = (TextBox)gvBidItems.FooterRow.FindControl("txtTotalPrice");
        TextBox tbItem = (TextBox)gvBidItems.FooterRow.FindControl("txtItem");
        TextBox tbDesc = (TextBox)gvBidItems.FooterRow.FindControl("txtDescription");
        TextBox tbUnitOfMeasure = (TextBox)gvBidItems.FooterRow.FindControl("txtUnitOfMeasure");
        HiddenField hfSKU = (HiddenField)gvBidItems.FooterRow.FindControl("hdnSKU");
        HiddenField hfID = (HiddenField)gvBidItems.FooterRow.FindControl("hdnID");
        LinkButton lnkAddItem = (LinkButton)gvBidItems.FooterRow.FindControl("lnkAddItem");
        LinkButton lnkUpdate = (LinkButton)gvBidItems.FooterRow.FindControl("lnkUpdateItem");
        LinkButton lnkCancel = (LinkButton)gvBidItems.FooterRow.FindControl("lnkCancelUpdate");
        //LinkButton lnkEdit = (LinkButton)gvBidItems.FindControl("lnkEditItem");
        //LinkButton lnkRemove = (LinkButton)gvBidItems.FindControl("lnkRemove");
        
        DataTable dtBidItems = (DataTable)ViewState["BidEventItems"];
        int toBeEdited = -1;
 
        for (int i = 0; i < dtBidItems.Rows.Count; i++)
        {
            if (dtBidItems.Rows[i]["ID"].ToString() == id.ToString())
            {
                toBeEdited = i;
                break;
            }
        }

        //string item = dtBidItems.Rows[i]["Item"].ToString().Trim();
        //string[] splitItem = item.Split(new Char[] { '-' });
        string unitprice = dtBidItems.Rows[toBeEdited]["UnitPrice"].ToString().Trim();
        string up = String.Format("{0:F}", Double.Parse(unitprice));
        string[] splitUnitPrice = up.Split(new Char[] { '.' });

        tbItem.Text = dtBidItems.Rows[toBeEdited]["Item"].ToString().Trim();
        tbDesc.Text = dtBidItems.Rows[toBeEdited]["Description"].ToString().Trim();
        jc.Text = DateTime.Parse(dtBidItems.Rows[toBeEdited]["DeliveryDate"].ToString().Trim()).ToShortDateString();
        tbUnitOfMeasure.Text = dtBidItems.Rows[toBeEdited]["UnitOfMeasure"].ToString().Trim();
        tbQty.Text = dtBidItems.Rows[toBeEdited]["Quantity"].ToString().Trim();
        tbPrice.Text = splitUnitPrice[0];
        tbPriceCents.Text = splitUnitPrice[1];
        tbTotal.Text = dtBidItems.Rows[toBeEdited]["EstimatedTotal"].ToString().Trim();
        hfSKU.Value = dtBidItems.Rows[toBeEdited]["SKU"].ToString().Trim();
        hfID.Value = id.ToString();

        //lnkEdit.Visible = false;
        //lnkRemove.Visible = false;
        lnkAddItem.Visible = false;
        lnkUpdate.Visible = true;
        lnkCancel.Visible = true;
    }

    private void CancelUpdateItem()
    {
        ClearAllItemInfo();
    }

    private void ClearAllItemInfo()
    {
        JSCalendar jc = (JSCalendar)gvBidItems.FooterRow.FindControl("clndrDeliveryDate");
        TextBox tbQty = (TextBox)gvBidItems.FooterRow.FindControl("txtQuantity");
        TextBox tbPrice = (TextBox)gvBidItems.FooterRow.FindControl("txtUnitPrice");
        TextBox tbPriceCents = (TextBox)gvBidItems.FooterRow.FindControl("txtUnitPriceCents");
        TextBox tbTotal = (TextBox)gvBidItems.FooterRow.FindControl("txtTotalPrice");
        TextBox tbItem = (TextBox)gvBidItems.FooterRow.FindControl("txtItem");
        TextBox tbDesc = (TextBox)gvBidItems.FooterRow.FindControl("txtDescription");
        TextBox tbUnitOfMeasure = (TextBox)gvBidItems.FooterRow.FindControl("txtUnitOfMeasure");
        HiddenField hfSKU = (HiddenField)gvBidItems.FooterRow.FindControl("hdnSKU");
        HiddenField hfID = (HiddenField)gvBidItems.FooterRow.FindControl("hdnID");
        LinkButton lnkAddItem = (LinkButton)gvBidItems.FooterRow.FindControl("lnkAddItem");
        LinkButton lnkUpdate = (LinkButton)gvBidItems.FooterRow.FindControl("lnkUpdateItem");
        LinkButton lnkCancel = (LinkButton)gvBidItems.FooterRow.FindControl("lnkCancelUpdate");
        //LinkButton lnkEdit = (LinkButton)gvBidItems.FindControl("lnkEditItem");
        //LinkButton lnkRemove = (LinkButton)gvBidItems.FindControl("lnkRemove");

        tbItem.Text = "";
        tbDesc.Text = "";
        jc.Text = "";
        tbUnitOfMeasure.Text = "";
        tbQty.Text = "0";
        tbPrice.Text = "0";
        tbPriceCents.Text = "00";
        tbTotal.Text = "0:00";
        hfSKU.Value = "";
       
        //lnkEdit.Visible = true;
        //lnkRemove.Visible = true;
        lnkAddItem.Visible = true;
        lnkUpdate.Visible = false;
        lnkCancel.Visible = false;
    }

    private void RemoveFromBidItemList(int id)
    {
        DataTable dtBidItems = (DataTable)ViewState["BidEventItems"];
        int toBeRemoved = -1;
        double itemCost = 0.0;
        int ctr = 0;

        for (int i = 0; i < dtBidItems.Rows.Count; i++)
        {
            if (dtBidItems.Rows[i]["ID"].ToString() == id.ToString())
            {
                toBeRemoved = i;             
                itemCost = double.Parse(dtBidItems.Rows[i]["EstimatedTotal"].ToString());
                break;              
            }
        }

        // subtract to total event cost
        double newTotal = double.Parse(txtTotalEventPrice.Text.Trim()) - itemCost;
        txtTotalEventPrice.Text = String.Format("{0:#,##0.00}", newTotal);

        if (toBeRemoved > -1)
            dtBidItems.Rows.RemoveAt(toBeRemoved);

        if (ViewState["RemovedCtr"] != null)
        {
            ctr = int.Parse(ViewState["RemovedCtr"].ToString());
            ctr = ctr + 1;
        }

        ViewState["RemovedCtr"] = ctr;
        ViewState["BidEventItems"] = dtBidItems;

        gvBidItems.DataSource = dtBidItems;
        gvBidItems.DataBind();

        if (gvBidItems.Rows.Count == 1)
        {
            ddlCategory.Enabled = true;
            ddlSubCategory.Enabled = true;
        }

    }
        
    private bool IsBidItemInputsValid(string item, string description, string sku, string deliverydate, 
        string qty, string uom, string price, string total, ref string msg)
    {
        StringBuilder sb = new StringBuilder();

        if (String.IsNullOrEmpty(item))
            sb.Append(BR_BULLET + " Item required.");
        if (String.IsNullOrEmpty(description))
            sb.Append(BR_BULLET + " Description required.");
        if (String.IsNullOrEmpty(deliverydate))
            sb.Append(BR_BULLET + " Delivery date required.");
        if (String.IsNullOrEmpty(qty))
            sb.Append(BR_BULLET + " Quantity required.");
        //if (String.IsNullOrEmpty(price))
        //    sb.Append(BR_BULLET + " Estimated unit price required.");
        //if (double.Parse(qty) <= 0.0)
        //    sb.Append(BR_BULLET + " Quantity required.");
        //if (double.Parse(price) <= 0.0)
        //    sb.Append(BR_BULLET + " Estimated unit price required.");

        if (sb.ToString().Trim().Length > 0)
        {
            msg = BR + "Please check the following:" + sb.ToString().Trim();
            return false;
        }
        else
            return true;
    }

    protected void btn_Command(object sender, CommandEventArgs e)
    {
        bool isSuccessful = false;

        
        switch (e.CommandName)
        {
            case "Submit":
                ViewState["command"] = "submit";
                if (IsValid)
                {
                    Session["AddBidEventMessage"] = Save(ref isSuccessful);
                }
                else
                {
                    isSuccessful = false;
                }
                break;
            case "Draft":
                ViewState["command"] = "draft";
                if (IsValid)
                {
                    Session["AddBidEventMessage"] = SaveAsDraft(ref isSuccessful);
                }
                else
                {
                    isSuccessful = false;
                }
                break;
            case "Cancel":
                Response.Redirect("bids.aspx");
                break;
        }

        if (isSuccessful)
        {
            Session["AddBidEventMessage"] = null;
            if (ViewState["command"].ToString() == "draft")
            {
                Response.Redirect("bids.aspx");
            }
            else
                if ((GetEstimatedEventCostInPHP() < Constant.BIDLIMIT_BEFOREAPPROVAL) && (lstSupplierB.Items.Count >= 3))
                {
                    Response.Redirect("approvedbiditems.aspx"); //Approved Bid Events 
                }
                else
                {
                    Response.Redirect("submittedbiditems.aspx"); //Bid Events For Approval                      
                }
        }
            
        if (Session["AddBidEventMessage"] != null)
        {
            lblMessage.Visible = true;
            lblMessage.Text = Session["AddBidEventMessage"].ToString();            
        }
        else
            lblMessage.Visible = false;

    }

    private string Save(ref bool isSuccessful)
    {
        int brf = 0;
        string msg = string.Empty;
     
        isSuccessful = false;

        //Edit bid events
        if (Request.QueryString[Constant.QS_TASKTYPE] == "2")
        {
            try
            {
                int stat = Constant.BID_STATUS_APPROVED;
                double d = GetEstimatedEventCostInPHP();
                
                if ((d >= Constant.BIDLIMIT_BEFOREAPPROVAL) || ((lstSupplierB.Items.Count < 3) & (d < Constant.BIDLIMIT_BEFOREAPPROVAL)))
                    stat = Constant.BID_STATUS_SUBMITTED;

                if (d > Constant.BIDLIMIT_BEFOREAPPROVAL & chkQualifiedSourcing.Checked == false)
                {
                    //msg = "Total Event Cost is equal or more than 2,000,000 (Php). Please select a VSF.";
                    //return msg;
                }

                // Check PR Date
                DateTime clndrPRDate1 = Convert.ToDateTime(clndrPRDate.Text.Trim());
                int resultPr = DateTime.Compare(clndrPRDate1, DateTime.Now);
                if (resultPr > 0)
                {
                    //msg = "PR Date must be less than current date.";
                    //return msg;
                }

                // Check Deadline
                DateTime clndrDeadline1;
                if (int.Parse(txtDeadlineHH.Text) > 12)
                {
                    clndrDeadline1 = DateTime.Parse(clndrDeadline.Text.Trim() + ' ' + txtDeadlineHH.Text.Trim() + ':' + txtDeadlineMM.Text.Trim() + ':' + txtDeadlineSS.Text.Trim() + ' ' + "PM");
                }
                else
                {
                    clndrDeadline1 = DateTime.Parse(clndrDeadline.Text.Trim() + ' ' + txtDeadlineHH.Text.Trim() + ':' + txtDeadlineMM.Text.Trim() + ':' + txtDeadlineSS.Text.Trim() + ' ' + ddlDeadline.SelectedValue);
                }

                int resultDl = DateTime.Compare(clndrDeadline1, DateTime.Now);
                if (resultDl <= 0)
                {
                    msg = "Submission Deadline must be greater than current date.";
                    return msg;
                }

                if (UpdateBidEvent(int.Parse(Session[Constant.SESSION_BIDREFNO].ToString()), stat))
                {
                    //Delete All
                    DeleteExistingBidEventItems(int.Parse(Session[Constant.SESSION_BIDREFNO].ToString()));
                    if (SaveBidEventItems(int.Parse(Session[Constant.SESSION_BIDREFNO].ToString())))
                    {
                        //Delete All
                        DeleteExistingBidEventAttachments(int.Parse(Session[Constant.SESSION_BIDREFNO].ToString()));
                        if (SaveBidEventAttachments(int.Parse(Session[Constant.SESSION_BIDREFNO].ToString())))
                        {
                            //Delete All
                            DeleteExistingBidEventParticipants(int.Parse(Session[Constant.SESSION_BIDREFNO].ToString()));
                            if (SaveBidEventParticipants(int.Parse(Session[Constant.SESSION_BIDREFNO].ToString())))
                            {
                                if (stat == Constant.BID_STATUS_APPROVED)
                                {
                                    UpdateBidEventComments(int.Parse(Session[Constant.SESSION_BIDREFNO].ToString()), int.Parse(Session[Constant.SESSION_USERID].ToString()));
                                    msg = "Bid event was successfully saved.";
                                    InitiateSendMail();
                                }
                                else
                                {
                                    UpdateBidEventComments(int.Parse(Session[Constant.SESSION_BIDREFNO].ToString()), int.Parse(Session[Constant.SESSION_USERID].ToString()));
                                    msg = "Bid event was successfully submitted for approval.";
                                }
                                isSuccessful = true;
                            }
                            else
                            {   // participants were not saved
                                msg = "There was an error saving the partcipants for this bid event. Please try again or contact administrator for help. 1";
                                //roll back, delete created bid event
                            }
                        }
                        else
                        {   // attachments were not saved
                            msg = "There was an error saving the attachments for this bid event. Please try again or contact administrator for help.. 2";
                            //roll back, delete created bid event attachments
                            DeleteCurrentBidEventAttachments(int.Parse(Session[Constant.SESSION_BIDREFNO].ToString()));
                        }
                    }
                    else
                    {   // bid items were not saved
                        msg = "There was an error saving the bid items for this bid event. Please try again or contact administrator for help... 3";
                        // roll back, delete created bid event items
                        DeleteCurrentBidEventItems(int.Parse(Session[Constant.SESSION_BIDREFNO].ToString()));

                    }
                }
                else
                {   // bid event was not saved successfully
                    msg = "There was an error saving this bid event. Please try again or contact administrator for help.... 4";
                }
            }
            catch (Exception ex)
            {
                // error saving
                Response.Write(ex.Message);
                msg = "There was an error saving this bid event. Please try again or contact administrator for help.....5<br>" + ex.Message;
            }

            return msg;

        }
        //Add new bid events
        else
        {
            try
            {
                int stat = Constant.BID_STATUS_APPROVED;
                double d = GetEstimatedEventCostInPHP();
                
                if ((d >= Constant.BIDLIMIT_BEFOREAPPROVAL) || ((lstSupplierB.Items.Count < 3) & (d < Constant.BIDLIMIT_BEFOREAPPROVAL)))
                    stat = Constant.BID_STATUS_SUBMITTED;

                if (d > Constant.BIDLIMIT_BEFOREAPPROVAL   & chkQualifiedSourcing.Checked == false)
                {
                    //msg = "Total Event Cost is equal or more than 2,000,000 (Php). Please select a VSF.";
                    //return msg;
                }

                // Check PR Date
                DateTime clndrPRDate1 = Convert.ToDateTime(clndrPRDate.Text.Trim());
                int resultPr = DateTime.Compare(clndrPRDate1, DateTime.Now);
                if (resultPr > 0)
                {
                    //msg = "PR Date must be less than current date.";
                    //return msg;
                }

                // Check Deadline
                DateTime clndrDeadline1;
                if (int.Parse(txtDeadlineHH.Text) > 12)
                {
                    clndrDeadline1 = DateTime.Parse(clndrDeadline.Text.Trim() + ' ' + txtDeadlineHH.Text.Trim() + ':' + txtDeadlineMM.Text.Trim() + ':' + txtDeadlineSS.Text.Trim() + ' ' + "PM");
                }
                else
                {
                    clndrDeadline1 = DateTime.Parse(clndrDeadline.Text.Trim() + ' ' + txtDeadlineHH.Text.Trim() + ':' + txtDeadlineMM.Text.Trim() + ':' + txtDeadlineSS.Text.Trim() + ' ' + ddlDeadline.SelectedValue);
                }
                int resultDl = DateTime.Compare(clndrDeadline1, DateTime.Now);
                if (resultDl <= 0)
                {
                    msg = "Submission Deadline must be greater than current date.";
                    return msg;
                }
                
                if (SaveBidEvent(ref brf, stat))
                {   // bid event was saved 
                    if (SaveBidEventItems(brf))
                    {
                        if (SaveBidEventAttachments(brf))
                        {
                            if (SaveBidEventParticipants(brf))
                            {
                                if (stat == Constant.BID_STATUS_APPROVED)
                                {
                                    if (txtComment.Text.Trim() != string.Empty)
                                        SaveBidEventComments(brf, int.Parse(Session[Constant.SESSION_USERID].ToString()), txtComment.Text.Trim());
                                    msg = "Bid event was successfully saved.";
                                }
                                else
                                {

                                    if (txtComment.Text.Trim() != string.Empty)
                                        SaveBidEventComments(brf, int.Parse(Session[Constant.SESSION_USERID].ToString()), txtComment.Text.Trim());
                                    msg = "Bid event was successfully submitted for approval.";
                                    // email immediate superior
                                }
                                isSuccessful = true;
                            }
                            else
                            {   // participants were not saved
                                msg = "There was an error saving the partcipants for this bid event. Please try again or contact administrator for help.";
                                // roll back, delete created bid event
                            }
                        }
                        else
                        {   // attachments were not saved
                            msg = "There was an error saving the attachments for this bid event. Please try again or contact administrator for help.";
                            // roll back, delete created bid event attachments
                            DeleteCurrentBidEventAttachments(int.Parse(Session[Constant.SESSION_BIDREFNO].ToString()));
                        }
                    }
                    else
                    {   // bid items were not saved
                        msg = "There was an error saving the bid items for this bid event. Please try again or contact administrator for help.";
                        // roll back, delete created bid event items
                        DeleteCurrentBidEventItems(int.Parse(Session[Constant.SESSION_BIDREFNO].ToString()));
                    }
                }
                else
                {   // bid event was not saved successfully
                    msg = "There was an error saving this bid event. Please try again or contact administrator for help.";
                }

            }
            catch
            {   // error saving
                msg = "There was an error saving this bid event. Please try again or contact administrator for help.";
            }

            return msg;
        }
    }
            
    private string SaveAsDraft(ref bool isSuccessful)
    {
        int brf = 0;
        string msg = string.Empty;
        isSuccessful = false;

        //Edit bid events as draft
        if (Request.QueryString[Constant.QS_TASKTYPE] == "2")
        {
            int stat = Constant.BID_STATUS_DRAFT;

            try
            {
                if (UpdateBidEvent(int.Parse(Session[Constant.SESSION_BIDREFNO].ToString()), stat))
                {
                    //Delete All
                    DeleteExistingBidEventItems(int.Parse(Session[Constant.SESSION_BIDREFNO].ToString()));
                    if (SaveBidEventItems(int.Parse(Session[Constant.SESSION_BIDREFNO].ToString())))
                    {
                        //Delete All
                        DeleteExistingBidEventAttachments(int.Parse(Session[Constant.SESSION_BIDREFNO].ToString()));
                        if (SaveBidEventAttachments(int.Parse(Session[Constant.SESSION_BIDREFNO].ToString())))
                        {
                            //Delete All
                            DeleteExistingBidEventParticipants(int.Parse(Session[Constant.SESSION_BIDREFNO].ToString()));
                            if (SaveBidEventParticipants(int.Parse(Session[Constant.SESSION_BIDREFNO].ToString())))
                            {
                                UpdateBidEventComments(int.Parse(Session[Constant.SESSION_BIDREFNO].ToString()), int.Parse(Session[Constant.SESSION_USERID].ToString()));
                                msg = "Bid event was successfully saved as draft.";
                                isSuccessful = true;
                              
                            }
                            else
                            {   // participants were not saved
                                msg = "There was an error saving the partcipants for this bid event. Please try again or contact administrator for help.";
                                //roll back, delete created bid event
                            }
                        }
                        else
                        {   // attachments were not saved
                            msg = "There was an error saving the attachments for this bid event. Please try again or contact administrator for help..";
                            //roll back, delete created bid event attachments
                            DeleteCurrentBidEventAttachments(int.Parse(Session[Constant.SESSION_BIDREFNO].ToString()));                          
                        }
                    }
                    else
                    {   // bid items were not saved
                        msg = "There was an error saving the bid items for this bid event. Please try again or contact administrator for help...";
                        // roll back, delete created bid event items
                        DeleteCurrentBidEventItems(int.Parse(Session[Constant.SESSION_BIDREFNO].ToString()));
                    }
                }
                else
                {   // bid event was not saved successfully
                    msg = "There was an error saving this bid event. Please try again or contact administrator for help....";
                }
            }
            catch
            {  // bid event was not saved successfully
                msg = "There was an error saving this bid event. Please try again or contact administrator for help.....";
            }

            return msg;
            
        }
        //Add bid events as draft
        else
        {
            try
            {
                int stat = Constant.BID_STATUS_DRAFT;

                    if (SaveBidEvent(ref brf, stat))
                    {   // bid event was saved 
                        if (SaveBidEventItems(brf))
                        {
                            if (SaveBidEventAttachments(brf))
                            {
                                if (SaveBidEventParticipants(brf))
                                {
                                    if (txtComment.Text.Trim() != string.Empty)
                                        SaveBidEventComments(brf, int.Parse(Session[Constant.SESSION_USERID].ToString()), txtComment.Text.Trim());
                                    msg = "Bid event was successfully saved as draft.";
                                    isSuccessful = true;
                                }
                                else
                                {   // participants were not saved
                                    msg = "There was an error saving the partcipants for this bid event. Please try again or contact administrator for help.";
                                    // roll back, delete created bid event
                                }
                            }
                            else
                            {   // attachments were not saved
                                msg = "There was an error saving the attachments for this bid event. Please try again or contact administrator for help.";
                                // roll back, delete created bid event attachments
                                DeleteCurrentBidEventAttachments(int.Parse(Session[Constant.SESSION_BIDREFNO].ToString()));
                            }
                        }
                        else
                        {   // bid items were not saved
                            msg = "There was an error saving the bid items for this bid event. Please try again or contact administrator for help..";
                            // roll back, delete created bid event items
                            DeleteCurrentBidEventItems(int.Parse(Session[Constant.SESSION_BIDREFNO].ToString()));
                        }
                    }
                    else
                    {   // bid event was not saved successfully
                        msg = "There was an error saving this bid event. Please try again or contact administrator for help...";
                    }
                
            }
            catch
            {   // error saving
                msg = "There was an error saving this bid event. Please try again or contact administrator for help....";
            }

            return msg;
        }
    }

    #region SAVE

    /// <summary>
    /// Save the bid event
    /// </summary>
    /// <param name="bidrefno"></param>
    /// <returns></returns>
    private bool SaveBidEvent(ref int bidrefno, int stat)
    {
        SqlConnection sqlConnect = new SqlConnection(connstring);
        SqlTransaction sqlTransact = null;
        bool isSuccessful = false;
        //Response.Write(connstring);
        try
        {
            sqlConnect.Open();
            sqlTransact = sqlConnect.BeginTransaction();

            SqlParameter[] sqlParams = new SqlParameter[20];
            sqlParams[0] = new SqlParameter("@PRRefNo", SqlDbType.BigInt);
            sqlParams[0].Value = Int64.Parse(txtPRNumber.Text.Trim());
            sqlParams[1] = new SqlParameter("@Requestor", SqlDbType.VarChar);
            sqlParams[1].Value = txtRequestor.Text.Trim();
            sqlParams[2] = new SqlParameter("@ItemDesc", SqlDbType.VarChar);
            sqlParams[2].Value = txtItemDesc.Text.Trim();
            sqlParams[3] = new SqlParameter("@Deadline", SqlDbType.DateTime);
            if (int.Parse(txtDeadlineHH.Text) > 12)
            {
                sqlParams[3].Value = DateTime.Parse(clndrDeadline.Text.Trim() + ' ' + txtDeadlineHH.Text.Trim() + ':' + txtDeadlineMM.Text.Trim() + ':' + txtDeadlineSS.Text.Trim() + ' ' + "PM");
            }
            else
            {
                sqlParams[3].Value = DateTime.Parse(clndrDeadline.Text.Trim() + ' ' + txtDeadlineHH.Text.Trim() + ':' + txtDeadlineMM.Text.Trim() + ':' + txtDeadlineSS.Text.Trim() + ' ' + ddlDeadline.SelectedValue);
            } 
            sqlParams[4] = new SqlParameter("@PRDate", SqlDbType.DateTime);
            sqlParams[4].Value = DateTime.Parse(clndrPRDate.Text.Trim());
            sqlParams[5] = new SqlParameter("@BuyerId", SqlDbType.Int);
            sqlParams[5].Value = int.Parse(Session[Constant.SESSION_USERID].ToString());
            sqlParams[6] = new SqlParameter("@GroupDeptSec", SqlDbType.Int);
            sqlParams[6].Value = int.Parse(ddlGroupDeptSec.SelectedValue);
            sqlParams[7] = new SqlParameter("@Category", SqlDbType.NVarChar);
            sqlParams[7].Value = ddlCategory.SelectedValue;
            sqlParams[8] = new SqlParameter("@SubCategory", SqlDbType.Int);
            if (ddlSubCategory.SelectedValue.Trim() == "")
                sqlParams[8].Value = System.DBNull.Value;
            else
                sqlParams[8].Value = ddlSubCategory.SelectedValue.Trim();
            sqlParams[9] = new SqlParameter("@Status", SqlDbType.Int);
            sqlParams[9].Value = stat;
            sqlParams[10] = new SqlParameter("@CompanyId", SqlDbType.Int);
            sqlParams[10].Value = int.Parse(ddlCompany.SelectedValue);
            sqlParams[11] = new SqlParameter("@ForAuction", SqlDbType.Int);
            sqlParams[11].Value = 0;
            sqlParams[12] = new SqlParameter("@DeliverTo", SqlDbType.VarChar);
            sqlParams[12].Value = txtDeliverTo.Text.Trim();
            sqlParams[13] = new SqlParameter("@Incoterm", SqlDbType.Int);
            sqlParams[13].Value = int.Parse(ddlIncoterm.SelectedValue);
            sqlParams[14] = new SqlParameter("@Currency", SqlDbType.Char);
            sqlParams[14].Value = ddlCurrency.SelectedValue.Substring(0, 3);
            sqlParams[15] = new SqlParameter("@EstItemValue", SqlDbType.Float);            
            sqlParams[15].Value = GetEstimatedEventCostInPHP();// double.Parse(txtTotalEventPrice.Text.Trim());
            sqlParams[16] = new SqlParameter("@QualifiedSourcing", SqlDbType.Int);
            sqlParams[16].Value = int.Parse(chkQualifiedSourcing.Checked  ? "1" : "0" );
            sqlParams[17] = new SqlParameter("@PRSubRefNo", SqlDbType.BigInt);
            sqlParams[17].Value = Int64.Parse(txtPRSubLineNumber.Text.Trim());
            sqlParams[18] = new SqlParameter("@VSFId", SqlDbType.Int);
            //sqlParams[18].Value = int.Parse(VSF.SelectedValue);
            sqlParams[18].Value = int.Parse("0");
            //sqlParams[19] = new SqlParameter("@MainCategory", SqlDbType.NVarChar);
            //sqlParams[19].Value = ddlMainCategory.SelectedValue;
            sqlParams[19] = new SqlParameter("@BidRefNo", SqlDbType.Int);
            sqlParams[19].Direction = ParameterDirection.Output;

            SqlHelper.ExecuteNonQuery(connstring, CommandType.StoredProcedure, "s3p_Ebid_InsertBid", sqlParams);

            sqlTransact.Commit();

            bidrefno = int.Parse(sqlParams[20].Value.ToString().Trim());
            isSuccessful = (bidrefno <= 0 ? false : true);
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString());
            sqlTransact.Rollback();
            isSuccessful = false;
        }
        finally
        {
            sqlConnect.Close();
        }
        return isSuccessful;
    }

    /// <summary>
    /// Save bid items for this bid event
    /// </summary>
    /// <param name="bidrefno"></param>
    /// <returns></returns>
    private bool SaveBidEventItems(int bidrefno)
    {
        bool isSuccessful = false;

        try
        {
            DataTable dtBidItems = (DataTable)ViewState["BidEventItems"];
            string s = string.Empty;

            for (int i = 1; i < dtBidItems.Rows.Count; i++)
            {
                s += SaveBidEventItems(bidrefno, dtBidItems.Rows[i]["SKU"].ToString().Length == 0 ? Constant.DEFAULTPRODUCTITEM.ToString() : dtBidItems.Rows[i]["SKU"].ToString(),
                    dtBidItems.Rows[i]["Description"].ToString(), double.Parse(dtBidItems.Rows[i]["Quantity"].ToString()),
                    dtBidItems.Rows[i]["UnitOfMeasure"].ToString(), DateTime.Parse(dtBidItems.Rows[i]["DeliveryDate"].ToString()),
                    double.Parse(dtBidItems.Rows[i]["UnitPrice"].ToString()));
            }

            isSuccessful = s.IndexOf("0") >= 0 ? false : true;
        }
        catch
        {            
            isSuccessful = false;
        }        
        return isSuccessful;
    }

    private string SaveBidEventItems(int bidrefno, string item, string description, double quantity, string unitofmeasure, DateTime deliverydate, double unitprice)
    {
        SqlConnection sqlConnect = new SqlConnection(connstring);
        SqlTransaction sqlTransact = null;
        string isSuccessful = string.Empty;

        try
        {
            sqlConnect.Open();
            sqlTransact = sqlConnect.BeginTransaction();

            SqlParameter[] sqlParams = new SqlParameter[9];
            sqlParams[0] = new SqlParameter("@Item", SqlDbType.VarChar);
            sqlParams[0].Value = item;
            sqlParams[1] = new SqlParameter("@BidRefNo", SqlDbType.Int);
            sqlParams[1].Value = bidrefno;
            sqlParams[2] = new SqlParameter("@DetailDesc", SqlDbType.VarChar);
            sqlParams[2].Value = description;
            sqlParams[3] = new SqlParameter("@Qty", SqlDbType.Float);
            sqlParams[3].Value = quantity;
            sqlParams[4] = new SqlParameter("@UnitOfMeasure", SqlDbType.VarChar);
            sqlParams[4].Value = unitofmeasure;
            sqlParams[5] = new SqlParameter("@DeliveryDate", SqlDbType.DateTime);
            sqlParams[5].Value = deliverydate;
            sqlParams[6] = new SqlParameter("@UnitPrice", SqlDbType.Float);
            sqlParams[6].Value = unitprice;
            sqlParams[7] = new SqlParameter("@PRGroupName", SqlDbType.VarChar);
            sqlParams[7].Value = PRGroupName;
            sqlParams[8] = new SqlParameter("@BidDetailNo", SqlDbType.Int);
            sqlParams[8].Direction = ParameterDirection.Output;

            SqlHelper.ExecuteNonQuery(connstring, CommandType.StoredProcedure, "s3p_Ebid_InsertBidDetail", sqlParams);

            sqlTransact.Commit();

            int r = int.Parse(sqlParams[8].Value.ToString().Trim());
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
    
    /// <summary>
    /// Save attachments for this bid event
    /// </summary>
    /// <param name="bidrefno"></param>
    /// <returns></returns>
    private bool SaveBidEventAttachments(int bidrefno)
    {
        bool isSuccessful = false;
        try
        {            
            DataTable dtAttachments = (DataTable)ViewState["BidEventAttachments"];
            string s = string.Empty;            

            for (int i = 1; i < dtAttachments.Rows.Count; i++)
            {
                s += SaveBidEventAttachments(bidrefno, int.Parse(Session[Constant.SESSION_USERID].ToString()), dtAttachments.Rows[i]["Original"].ToString(), dtAttachments.Rows[i]["Actual"].ToString(), double.Parse(dtAttachments.Rows[i]["Filesize"].ToString()));
            }
            isSuccessful = s.IndexOf("0") >= 0 ? false : true;
        }
        catch
        {
            isSuccessful = false;
        }        
        return isSuccessful;
    }

    private string SaveBidEventAttachments(int bidrefno, int buyerid, string originalfilename, string actualfilename, double Filesize)
    {
        SqlConnection sqlConnect = new SqlConnection(connstring);
        SqlTransaction sqlTransact = null;
        string isSuccessful = string.Empty;

        try
        {
            sqlConnect.Open();
            sqlTransact = sqlConnect.BeginTransaction();

            SqlParameter[] sqlParams = new SqlParameter[6];
            sqlParams[0] = new SqlParameter("@BidRefNo", SqlDbType.Int);
            sqlParams[0].Value = bidrefno;
            sqlParams[1] = new SqlParameter("@BuyerId", SqlDbType.Int);
            sqlParams[1].Value = buyerid;
            sqlParams[2] = new SqlParameter("@OriginalFileName", SqlDbType.VarChar);
            sqlParams[2].Value = originalfilename;
            sqlParams[3] = new SqlParameter("@ActualFileName", SqlDbType.VarChar);
            sqlParams[3].Value = actualfilename;
            sqlParams[4] = new SqlParameter("@Filesize", SqlDbType.VarChar);
            sqlParams[4].Value = Filesize;
            sqlParams[5] = new SqlParameter("@FileUploadID", SqlDbType.Int);
            sqlParams[5].Direction = ParameterDirection.Output;

            SqlHelper.ExecuteNonQuery(connstring, CommandType.StoredProcedure, "s3p_Ebid_InsertBidAttachment", sqlParams);

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


    /// <summary>
    /// Save the participants for this bid event
    /// </summary>
    /// <param name="bidrefno"></param>
    /// <returns></returns>
    private bool SaveBidEventParticipants(int bidrefno)
    {
        bool isSuccessful = false;
        try
        {
            string s = string.Empty;

            for (int i = 0; i < lstSupplierB.Items.Count; i++)
            {
                s += SaveBidEventParticipants(bidrefno, int.Parse(lstSupplierB.Items[i].Value));
            }

            isSuccessful = s.IndexOf("0") >= 0 ? false : true;
        }
        catch
        {
            isSuccessful = false;
        }        
        return isSuccessful;
    }

    private string SaveBidEventParticipants(int bidrefno, int vendorid)
    {
        SqlConnection sqlConnect = new SqlConnection(connstring);
        SqlTransaction sqlTransact = null;
        string isSuccessful = string.Empty;

        try
        {
            sqlConnect.Open();
            sqlTransact = sqlConnect.BeginTransaction();

            SqlParameter[] sqlParams = new SqlParameter[3];
            sqlParams[0] = new SqlParameter("@BidRefNo", SqlDbType.Int);
            sqlParams[0].Value = bidrefno;
            sqlParams[1] = new SqlParameter("@VendorId", SqlDbType.Int);
            sqlParams[1].Value = vendorid;
            sqlParams[2] = new SqlParameter("@VendorInBidsId", SqlDbType.Int);
            sqlParams[2].Direction = ParameterDirection.Output;

            SqlHelper.ExecuteNonQuery(connstring, CommandType.StoredProcedure, "s3p_Ebid_InsertBidParticipants", sqlParams);

            sqlTransact.Commit();

            int r = int.Parse(sqlParams[2].Value.ToString().Trim());
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

    private void SaveBidEventComments(int bidrefno, int userid, string comment)
    {
        SqlConnection sqlConnect = new SqlConnection(connstring);
        SqlTransaction sqlTransact = null;
       
            sqlConnect.Open();
            sqlTransact = sqlConnect.BeginTransaction();

            SqlParameter[] sqlParams = new SqlParameter[5];
            sqlParams[0] = new SqlParameter("RefNo", SqlDbType.Int);
            sqlParams[0].Value = bidrefno;
            sqlParams[1] = new SqlParameter("@UserId", SqlDbType.Int);
            sqlParams[1].Value = userid;
            sqlParams[2] = new SqlParameter("@UserType", SqlDbType.Int);
            sqlParams[2].Value = 1;
            sqlParams[3] = new SqlParameter("@IsAuction", SqlDbType.Int);
            sqlParams[3].Value = 0;
            sqlParams[4] = new SqlParameter("@Comment", SqlDbType.NVarChar);
            sqlParams[4].Value = comment;
            

            SqlHelper.ExecuteNonQuery(connstring, CommandType.StoredProcedure, "sp_InsertBidItemsComments", sqlParams);
            sqlTransact.Commit();

        }

    #endregion

    #region UPDATE
    private void UpdateBidEventComments(int bidrefno, int userid)
    {
        SqlConnection sqlConnect = new SqlConnection(connstring);
        SqlTransaction sqlTransact = null;

        try
        {
            sqlConnect.Open();
            sqlTransact = sqlConnect.BeginTransaction();

            SqlParameter[] sqlParams = new SqlParameter[3];
            sqlParams[0] = new SqlParameter("refNo", SqlDbType.Int);
            sqlParams[0].Value = bidrefno;
            sqlParams[1] = new SqlParameter("@userId", SqlDbType.Int);
            sqlParams[1].Value = userid;
            sqlParams[2] = new SqlParameter("@comment", SqlDbType.NVarChar);
            sqlParams[2].Value = txtComment.Text;


            SqlHelper.ExecuteNonQuery(connstring, CommandType.StoredProcedure, "sp_UpdateBidItemsComments", sqlParams);
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

    /// <summary>
    /// Update the bid event
    /// </summary>
    /// <param name="bidrefno"></param>
    /// <returns></returns>
    
    private bool UpdateBidEvent(int bidrefno, int stat)
    {
        SqlConnection sqlConnect = new SqlConnection(connstring);
        SqlTransaction sqlTransact = null;
        bool isSuccessful = false;

        try
        {
            sqlConnect.Open();
            sqlTransact = sqlConnect.BeginTransaction();

            SqlParameter[] sqlParams = new SqlParameter[20];
            sqlParams[0] = new SqlParameter("@PRRefNo", SqlDbType.BigInt);
            sqlParams[0].Value = Int64.Parse(txtPRNumber.Text.Trim());
            sqlParams[1] = new SqlParameter("@Requestor", SqlDbType.VarChar);
            sqlParams[1].Value = txtRequestor.Text.Trim();
            sqlParams[2] = new SqlParameter("@ItemDesc", SqlDbType.VarChar);
            sqlParams[2].Value = txtItemDesc.Text.Trim();
            sqlParams[3] = new SqlParameter("@Deadline", SqlDbType.DateTime);
            if (int.Parse(txtDeadlineHH.Text) > 12)
            {
                sqlParams[3].Value = DateTime.Parse(clndrDeadline.Text.Trim() + ' ' + txtDeadlineHH.Text.Trim() + ':' + txtDeadlineMM.Text.Trim() + ':' + txtDeadlineSS.Text.Trim() + ' ' + "PM");
            }
            else
            {
                sqlParams[3].Value = DateTime.Parse(clndrDeadline.Text.Trim() + ' ' + txtDeadlineHH.Text.Trim() + ':' + txtDeadlineMM.Text.Trim() + ':' + txtDeadlineSS.Text.Trim() + ' ' + ddlDeadline.SelectedValue);
            }
            sqlParams[4] = new SqlParameter("@PRDate", SqlDbType.DateTime);
            sqlParams[4].Value = DateTime.Parse(clndrPRDate.Text.Trim());
            sqlParams[5] = new SqlParameter("@BuyerId", SqlDbType.Int);
            sqlParams[5].Value = int.Parse(Session[Constant.SESSION_USERID].ToString());
            sqlParams[6] = new SqlParameter("@GroupDeptSec", SqlDbType.Int);
            sqlParams[6].Value = int.Parse(ddlGroupDeptSec.SelectedValue);
            sqlParams[7] = new SqlParameter("@Category", SqlDbType.NVarChar);
            sqlParams[7].Value = ddlCategory.SelectedValue;
            sqlParams[8] = new SqlParameter("@SubCategory", SqlDbType.Int);
            if (ddlSubCategory.SelectedValue.Trim() == "")
                sqlParams[8].Value = System.DBNull.Value;
            else
                sqlParams[8].Value = ddlSubCategory.SelectedValue.Trim();
            sqlParams[9] = new SqlParameter("@Status", SqlDbType.Int);
            sqlParams[9].Value = stat;
            sqlParams[10] = new SqlParameter("@CompanyId", SqlDbType.Int);
            sqlParams[10].Value = int.Parse(ddlCompany.SelectedValue);
            sqlParams[11] = new SqlParameter("@ForAuction", SqlDbType.Int);
            sqlParams[11].Value = 0;
            sqlParams[12] = new SqlParameter("@DeliverTo", SqlDbType.VarChar);
            sqlParams[12].Value = txtDeliverTo.Text.Trim();
            sqlParams[13] = new SqlParameter("@Incoterm", SqlDbType.Int);
            sqlParams[13].Value = int.Parse(ddlIncoterm.SelectedValue);
            sqlParams[14] = new SqlParameter("@EstItemValue", SqlDbType.Float);
            sqlParams[14].Value = GetEstimatedEventCostInPHP();//double.Parse(txtTotalEventPrice.Text.Trim());
            sqlParams[15] = new SqlParameter("@Currency", SqlDbType.Char);
            sqlParams[15].Value = ddlCurrency.SelectedValue.Substring(0, 3);
            sqlParams[16] = new SqlParameter("@QualifiedSourcing", SqlDbType.Char);
            sqlParams[16].Value = int.Parse(chkQualifiedSourcing.Checked ? "1" : "0");
            sqlParams[17] = new SqlParameter("@VSFId", SqlDbType.Int);
            //sqlParams[17].Value = int.Parse(VSF.SelectedValue);
            sqlParams[17].Value = int.Parse("0");
            sqlParams[18] = new SqlParameter("@BidRefNo", SqlDbType.Int);
            sqlParams[18].Value = bidrefno;
            //sqlParams[19] = new SqlParameter("@MainCategory", SqlDbType.NVarChar);
            //sqlParams[19].Value = ddlMainCategory.SelectedValue;
            sqlParams[19] = new SqlParameter("@Result", SqlDbType.Int);
            sqlParams[19].Direction = ParameterDirection.Output;

            SqlHelper.ExecuteNonQuery(connstring, CommandType.StoredProcedure, "s3p_Ebid_UpdateBid", sqlParams);

            sqlTransact.Commit();
            int result;

            result = int.Parse(sqlParams[19].Value.ToString().Trim());
            isSuccessful = (result > 0 ? false : true);
            isSuccessful = true;
        }
        catch
        {
            sqlTransact.Rollback();
            isSuccessful = false;
        }
        finally
        {
            sqlConnect.Close();
        }
        return isSuccessful;
    }

    private void UpdateBidItemsTable(ref DataTable dtBidItems,
            string id, string item, string description, string quantity,
                string unitofmeasure, string unitprice, string deliverydate, string SKU, string estimatedtotal)
    {
        for (int i = 0; i < dtBidItems.Rows.Count; i++)
        {
            if (dtBidItems.Rows[i]["ID"].ToString() == id.ToString())
            {
                dtBidItems.Rows[i]["Item"] = item;
                dtBidItems.Rows[i]["Description"] = description;
                dtBidItems.Rows[i]["Quantity"] = quantity;
                dtBidItems.Rows[i]["UnitOfMeasure"] = unitofmeasure;
                dtBidItems.Rows[i]["UnitPrice"] = unitprice;
                //if (deliverydate != "")
                if (checkDate(deliverydate))
                   dtBidItems.Rows[i]["DeliveryDate"] = deliverydate;
                else
                   dtBidItems.Rows[i]["DeliveryDate"] = DateTime.Now;
                dtBidItems.Rows[i]["SKU"] = SKU;
                dtBidItems.Rows[i]["EstimatedTotal"] = estimatedtotal;
            }
        }
    }

    private string UpdateBidEventItem()
    {

        JSCalendar jc = (JSCalendar)gvBidItems.FooterRow.FindControl("clndrDeliveryDate");
        TextBox tbQty = (TextBox)gvBidItems.FooterRow.FindControl("txtQuantity");
        TextBox tbPrice = (TextBox)gvBidItems.FooterRow.FindControl("txtUnitPrice");
        TextBox tbPriceCents = (TextBox)gvBidItems.FooterRow.FindControl("txtUnitPriceCents");
        TextBox tbTotal = (TextBox)gvBidItems.FooterRow.FindControl("txtTotalPrice");
        TextBox tbItem = (TextBox)gvBidItems.FooterRow.FindControl("txtItem");
        TextBox tbDesc = (TextBox)gvBidItems.FooterRow.FindControl("txtDescription");
        TextBox tbUnitOfMeasure = (TextBox)gvBidItems.FooterRow.FindControl("txtUnitOfMeasure");
        HiddenField hfSKU = (HiddenField)gvBidItems.FooterRow.FindControl("hdnSKU");
        HiddenField hfID = (HiddenField)gvBidItems.FooterRow.FindControl("hdnID");
        
         DataTable dtBidItems = (DataTable)ViewState["BidEventItems"];

         if (hfID.Value.Length > 0)
         {
             if (dtBidItems.Rows.Count == 2)
             {                
                 double itemCost = double.Parse(dtBidItems.Rows[1]["EstimatedTotal"].ToString());
                 double TotalEventCost = double.Parse(txtTotalEventPrice.Text.Trim()) - itemCost;
                 double newTotal = TotalEventCost + double.Parse(tbTotal.Text.ToString());
                 txtTotalEventPrice.Text = String.Format("{0:#,##0.00}", newTotal);
             }
             else
             {
                 if (int.Parse(hfID.Value) - int.Parse(ViewState["RemovedCtr"].ToString()) == 0)
                 {
                     double itemCost = double.Parse(dtBidItems.Rows[1]["EstimatedTotal"].ToString());
                     double TotalEventCost = double.Parse(txtTotalEventPrice.Text.Trim()) - itemCost;
                     double newTotal = TotalEventCost + double.Parse(tbTotal.Text.ToString());
                     txtTotalEventPrice.Text = String.Format("{0:#,##0.00}", newTotal);
                 }
                 else if (int.Parse(hfID.Value) - int.Parse(ViewState["RemovedCtr"].ToString()) < 0)
                 {
                     double itemCost = double.Parse(dtBidItems.Rows[int.Parse(hfID.Value)]["EstimatedTotal"].ToString());
                     double TotalEventCost = double.Parse(txtTotalEventPrice.Text.Trim()) - itemCost;
                     double newTotal = TotalEventCost + double.Parse(tbTotal.Text.ToString());
                     txtTotalEventPrice.Text = String.Format("{0:#,##0.00}", newTotal);
                 }
                 else
                 {
                     double itemCost = double.Parse(dtBidItems.Rows[int.Parse(hfID.Value) - int.Parse(ViewState["RemovedCtr"].ToString())]["EstimatedTotal"].ToString());
                     double TotalEventCost = double.Parse(txtTotalEventPrice.Text.Trim()) - itemCost;
                     double newTotal = TotalEventCost + double.Parse(tbTotal.Text.ToString());
                     txtTotalEventPrice.Text = String.Format("{0:#,##0.00}", newTotal);
                 }
             }

             string msg = "";
             if (IsBidItemInputsValid(tbItem.Text.Trim(), tbDesc.Text.Trim(), hfSKU.Value.Trim(), jc.Text.Trim(),
                 tbQty.Text.Trim(), tbUnitOfMeasure.Text.Trim(), tbPrice.Text.Trim() + "." + tbPriceCents.Text.Trim(), tbTotal.Text.Trim(), ref msg))
             {
                 UpdateBidItemsTable(ref dtBidItems, hfID.Value.ToString().Trim(), tbItem.Text.Trim(), tbDesc.Text.Trim(), tbQty.Text.Trim(), tbUnitOfMeasure.Text.Trim(),
                    tbPrice.Text.Trim() + "." + tbPriceCents.Text.Trim(), jc.Text.Trim(), hfSKU.Value.Trim(), tbTotal.Text.Trim());
             }
             else
             {
                 return msg;
             }
         }

         ViewState["BidEventItems"] = dtBidItems;
         gvBidItems.DataSource = dtBidItems;
         gvBidItems.DataBind();

         ClearAllItemInfo();
         return "";
    }

    #endregion

    #region DELETE

    private void DeleteAttachmentsFiles(int id)
    {
        DataTable dtAttachments = (DataTable)ViewState["BidEventAttachments"];
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

    private void DeleteExistingBidEventItems(int bidrefno)
    {
        try
        {
        DataTable dtBidItems = (DataTable)ViewState["BidEventExistingItems"];

            for (int i = 0; i < dtBidItems.Rows.Count; i++)
            {
                string bdno = dtBidItems.Rows[i]["BidDetailNo"].ToString();
                if (bdno != "")
                    DeleteBidEventItems(bidrefno, int.Parse(bdno));
            }

        }
        catch
        {

        }
    }

    private void DeleteCurrentBidEventItems(int bidrefno)
    {
        try
        {
            DataTable dtBidItems = (DataTable)ViewState["BidEventItems"];

            for (int i = 0; i < dtBidItems.Rows.Count; i++)
            {
                string bdno = dtBidItems.Rows[i]["BidDetailNo"].ToString();
                if (bdno != "")
                    DeleteBidEventItems(bidrefno, int.Parse(bdno));
            }

        }
        catch
        {

        }
    }

    private void DeleteBidEventItems(int bidrefno, int bidetailno)
    {
        SqlConnection sqlConnect = new SqlConnection(connstring);
        SqlTransaction sqlTransact = null;

        try
        {
            sqlConnect.Open();
            sqlTransact = sqlConnect.BeginTransaction();

            SqlParameter[] sqlParams = new SqlParameter[3];
            sqlParams[0] = new SqlParameter("@BidRefNo", SqlDbType.Int);
            sqlParams[0].Value = bidrefno;
            sqlParams[1] = new SqlParameter("@BidDetailNo", SqlDbType.Int);
            sqlParams[1].Value = bidetailno;
            sqlParams[2] = new SqlParameter("@Result", SqlDbType.Int);
            sqlParams[2].Direction = ParameterDirection.Output;

            SqlHelper.ExecuteNonQuery(connstring, CommandType.StoredProcedure, "sp_DeleteBidItemDetails", sqlParams);

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

    private void DeleteExistingBidEventParticipants(int bidrefno)
    {

        DataTable dtParticipants = (DataTable)ViewState["BidEventParticipants"];

        for (int i = 0; i < dtParticipants.Rows.Count; i++)
        {
            string vid = dtParticipants.Rows[i]["VendorId"].ToString();

            if (vid != "")
            {
                DeleteBidEventParticipants(bidrefno, int.Parse(vid));
            }
        }

       
    }

    private void DeleteBidEventParticipants(int bidrefno, int vendorid)
    {

        SqlConnection sqlConnect = new SqlConnection(connstring);
        SqlTransaction sqlTransact = null;
         try
        {
            sqlConnect.Open();
            sqlTransact = sqlConnect.BeginTransaction();

            SqlParameter[] sqlParams = new SqlParameter[3];
            sqlParams[0] = new SqlParameter("@BidRefNo", SqlDbType.Int);
            sqlParams[0].Value = bidrefno;
            sqlParams[1] = new SqlParameter("@VendorId", SqlDbType.Int);
            sqlParams[1].Value = vendorid;

            SqlHelper.ExecuteNonQuery(connstring, CommandType.StoredProcedure, "sp_DeleteBidEventParticipants", sqlParams);

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

    private void DeleteExistingBidEventAttachments(int bidrefno)
    {
        try
        {
        DataTable dtAttachments = (DataTable)ViewState["BidEventExistingAttachments"];

        for (int i = 0; i < dtAttachments.Rows.Count; i++)
        {
            string fid = dtAttachments.Rows[i]["ID"].ToString();
           
            if (fid != "")
            {
                DeleteBidEventAttachments(bidrefno, int.Parse(fid));
                //DeleteAttachmentsFiles(int.Parse(fid));
            }
        }

        }
        catch
        {

        }

    }

    private void DeleteCurrentBidEventAttachments(int bidrefno)
    {
        try
        {
            DataTable dtAttachments = (DataTable)ViewState["BidEventAttachments"];

            for (int i = 0; i < dtAttachments.Rows.Count; i++)
            {
                string fid = dtAttachments.Rows[i]["ID"].ToString();

                if (fid != "")
                {
                    DeleteBidEventAttachments(bidrefno, int.Parse(fid));
                    DeleteAttachmentsFiles(int.Parse(fid));
                }
            }

        }
        catch
        {

        }

    }

    private void DeleteBidEventAttachments(int bidrefno, int fileuploadid)
    {
        SqlConnection sqlConnect = new SqlConnection(connstring);
        SqlTransaction sqlTransact = null;

        try
        {
            sqlConnect.Open();
            sqlTransact = sqlConnect.BeginTransaction();

            SqlParameter[] sqlParams = new SqlParameter[3];
            sqlParams[0] = new SqlParameter("@BidRefNo", SqlDbType.Int);
            sqlParams[0].Value = bidrefno;
            sqlParams[1] = new SqlParameter("@FileUploadID", SqlDbType.Int);
            sqlParams[1].Value = fileuploadid;
            sqlParams[2] = new SqlParameter("@Result", SqlDbType.Int);
            sqlParams[2].Direction = ParameterDirection.Output;

            SqlHelper.ExecuteNonQuery(connstring, CommandType.StoredProcedure, "sp_DeleteBidEventAttachments", sqlParams);

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
    #endregion

    private double GetEstimatedEventCostInPHP()
    {
        if (ddlCurrency.SelectedValue != "")
        {
            double d = double.Parse(ddlCurrency.SelectedValue.Split(new char[] { ':' })[1]);
            double amt = double.Parse(txtTotalEventPrice.Text.Trim());
            return amt * d;
        }
        else
            return 0.0;
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

    private void RemoveDuplicateB()
    {
        if (lstSupplierB.Items.Count > 1)
        {
            for (int i = 0; i < lstSupplierB.Items.Count; i++)
            {
                for (int j = i; i < lstSupplierB.Items.Count; i++)
                {
                    if (lstSupplierB.Items[i].Value == lstSupplierB.Items[j].Value)
                    {
                        lstSupplierB.Items.RemoveAt(j);
                    }

                }
            }
        }
    }

    protected void lstSupplierA_DataBound(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString[Constant.QS_TASKTYPE] == "2")
            {
                RemoveDuplicateA();
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

    protected void lstSupplierB_DataBound(object sender, EventArgs e)
    {
        
    }

    protected void cfvSuppliers_ServerValidate(object source, ServerValidateEventArgs args)
    {
        StringBuilder sb = new StringBuilder();

        if (gvBidItems.Rows.Count <= 1)
        {
            sb.Append(BR + " Please select atleast one(1) item.");
        }

        if (!chkQualifiedSourcing.Checked)
        {
            if (lstSupplierB.Items.Count < 3)
            {
                sb.Append(BR + " Please select atleast three(3) supplier/participant.");
            }
        }

        if (chkQualifiedSourcing.Checked)
        {
            if (lstSupplierB.Items.Count < 1)
            {
                sb.Append(BR + " Please select atleast one(1) supplier/participant.");
            }
        }

        cfvSuppliers.Text = sb.ToString().Trim();
        args.IsValid = sb.ToString().Trim().Length <= 0;
    }

    protected void ddlCurrency_DataBound(object sender, EventArgs e)
    {
        if (Request.QueryString[Constant.QS_TASKTYPE] != "2")
            ddlCurrency.SelectedIndex = GetSelectedIndex(ddlCurrency, "PHP");
        else
            ddlCurrency.SelectedIndex = GetSelectedIndex(ddlCurrency, selectedCurrency);
    }

    protected void ddlCompany_DataBound(object sender, EventArgs e)
    {
        //ddlCompany.Items.Insert(0, new ListItem("---- SELECT COMPANY ----", "-1"));
    }


    void InitiateSendMail()
    {
        try
        {
            ArrayList list = GetSelectedSuppliersFromGv();
            BidDetails details = GetBidItemDetails(int.Parse(Session["BidRefNo"].ToString()));
            int failedcount = 0, successcount = 0;

            if (list.Count > 0)
            {
                if (SendEmailInvitation(details, list, ref failedcount, ref successcount))
                {
                    if ((failedcount == 0) && (successcount > 0))
                    {
                        // success
                        Session["Message"] = (successcount == 1 ? "Invitation" : "Invitations") + " were sent successfully.";
                    }
                    else
                        // failed
                        Session["Message"] = "Failed to send " + (list.Count == 1 ? "invitation" : "invitations") + " to " + failedcount + " out of " + list.Count + (list.Count == 1 ? " recipient" : " recipients") + ". Please try again or contact adminitrator for assistance.";
                }
                else
                {
                    // failed
                    Session["Message"] = "Failed to send invitations. Please try again or contact adminitrator for assistance.";
                }
            }
            else
            {
                Session["Message"] = "No invitations sent. Please select suppliers from the given list.";
            }
        }
        catch
        {
            // failed
            Session["Message"] = "Failed to send invitations. Please try again or contact adminitrator for assistance.";
        }
    }


    #region Email
    private BidDetails GetBidItemDetails(int bidrefno)
    {
        DataTable dt = SqlHelper.ExecuteDataset(connstring, "sp_GetBidInvitationInfo", new SqlParameter[] { new SqlParameter("@BidRefNo", bidrefno) }).Tables[0];
        BidDetails item = new BidDetails();

        if (dt.Rows.Count > 0)
            item = new BidDetails(dt.Rows[0]);

        return item;
    }

    private ArrayList GetSelectedSuppliers()
    {
        ArrayList suppliersList = new ArrayList();

        SqlParameter[] sqlParams = new SqlParameter[1];
        sqlParams[0] = new SqlParameter("@BidRefNo", SqlDbType.Int);
        sqlParams[0].Value = Int32.Parse(Session["BidRefNo"].ToString());
        DataTable dt = new DataTable();

        dt = SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "sp_GetEmailAddresses", sqlParams).Tables[0];

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            BidParticipant participant = new BidParticipant();
            participant.ID = int.Parse(dt.Rows[i]["VendorId"].ToString().Trim());
            participant.Name = dt.Rows[i]["VendorName"].ToString().Trim();
			participant.Username = dt.Rows[i]["UserName"].ToString().Trim();
            participant.EmailAddress = dt.Rows[i]["VendorEmail"].ToString().Trim();
            participant.MobileNo = dt.Rows[i]["MobileNo"].ToString().Trim();
			
            suppliersList.Add(participant);
        }

        return suppliersList;
    }

    private ArrayList GetSelectedSuppliersFromGv()
    {
        ArrayList suppliersList = new ArrayList(); 
        SqlDataReader oReader;
        string query;
        SqlCommand cmd;
        SqlConnection conn;

        for (int i = 0; i < lstSupplierB.Items.Count; i++)
        {
            query = "select t1.VendorId, t1.VendorName, t1.VendorEmail, t1.MobileNo, t2.UserName from tblVendors t1 inner join tblUsers t2 on t1.VendorId = t2.UserId where t1.VendorId =@VendorId";
            //query = "sp_GetVendorInformation"; //##storedProcedure
            using (conn = new SqlConnection(connstring))
            {
                using (cmd = new SqlCommand(query, conn))
                {
                    //cmd.CommandType = CommandType.StoredProcedure; //##storedProcedure
                    cmd.Parameters.AddWithValue("@VendorId", Convert.ToInt32(lstSupplierB.Items[i].Value));
                    conn.Open();
                    //Process results
                    oReader = cmd.ExecuteReader();
                    if (oReader.HasRows)
                    {
                        while (oReader.Read())
                        {
                            BidParticipant participant = new BidParticipant();

                            participant.ID = int.Parse(oReader["VendorId"].ToString());
                            participant.Name = oReader["VendorName"].ToString();
							participant.Username = oReader["UserName"].ToString();
                            participant.EmailAddress = oReader["VendorEmail"].ToString();
                            participant.MobileNo = oReader["MobileNo"].ToString();
							
                            suppliersList.Add(participant);
                        }
                    }
                }
            }
            //Response.Write(lstSupplierB.Items[i].Value);
        }


        
        //foreach (GridViewRow gr in gvSuppliers.Rows)
        //{
        //     TODO: Check if checkbox is checked, if yes, add supplier in the sendlist
        //    CheckBox chkRow = (CheckBox)gr.FindControl("chkRow");

        //    if (chkRow.Checked)
        //    {
        //        BidParticipant participant = new BidParticipant();
        //        int i = gr.DataItemIndex;

        //        participant.ID = int.Parse(((HiddenField)gvSuppliers.Rows[i].Cells[0].FindControl("hdnvendorid")).Value.ToString());
        //        participant.Name = ((HiddenField)gvSuppliers.Rows[i].Cells[0].FindControl("hdnname")).Value.ToString();
        //        participant.EmailAddress = ((HiddenField)gvSuppliers.Rows[i].Cells[0].FindControl("hdnemail")).Value.ToString();
        //        participant.MobileNo = ((HiddenField)gvSuppliers.Rows[i].Cells[0].FindControl("hdnmobileno")).Value.ToString();

        //        suppliersList.Add(participant);
        //    }
        //}

        return suppliersList;
    }

    private bool SendEmailInvitation(BidDetails biddetails, ArrayList recipients, ref int failedcount, ref int successcount)
    {
        bool success = false;
        string subject = "Trans-Asia  Incorporated/ Commnunications : Invitation to Bid";
        failedcount = 0;
        successcount = 0;

        try
        {
            for (int i = 0; i < recipients.Count; i++)
            {
                BidParticipant p = (BidParticipant)recipients[i];

                if (!MailHelper.SendEmail(MailTemplate.GetDefaultSMTPServer(),
                        MailHelper.ChangeToFriendlyName(biddetails.Creator, biddetails.CreatorEmail),
                        MailHelper.ChangeToFriendlyName(p.Name, p.EmailAddress),
                        subject,
                        CreateInvitationBody(biddetails, p),
                        MailTemplate.GetTemplateLinkedResources(this)))
                {	// if sending failed					
                    failedcount++;
                    LogHelper.EventLogHelper.Log("Bid Event > Send Invitation : Sending Failed to " + p.EmailAddress, System.Diagnostics.EventLogEntryType.Error);
                }
                else
                {	// if sending successful
                    successcount++;
                    LogHelper.EventLogHelper.Log("Bid Event > Send Invitation : Email Sent to " + p.EmailAddress, System.Diagnostics.EventLogEntryType.Information);

                    //add 1 to emailsent field based on vendorID and BidRefNo
                    SqlParameter[] sqlparams = new SqlParameter[2];
                    sqlparams[0] = new SqlParameter("@Vendorid", SqlDbType.Int);
                    sqlparams[0].Value = p.ID;
                    sqlparams[1] = new SqlParameter("@BidRefNo", SqlDbType.VarChar);
                    sqlparams[1].Value = Int32.Parse(Session["BidRefNo"].ToString());
                    SqlHelper.ExecuteNonQuery(connstring, CommandType.StoredProcedure, "sp_BidInvitationAddEmailSent", sqlparams);
                }
            }

            success = true;
        }
        catch (Exception ex)
        {
            success = false;
            LogHelper.EventLogHelper.Log("Bid Event > Send Invitation : " + ex.Message, System.Diagnostics.EventLogEntryType.Error);
        }

        try
        {
            for (int j = 0; j < recipients.Count; j++)
            {
                BidParticipant p = (BidParticipant)recipients[j];

                if (SMSHelper.AreValidMobileNumbers(p.MobileNo.Trim()))
                {
                    SMSHelper.SendSMS(new SMSMessage(CreateInvitationSmsBody(biddetails, p).Trim(), p.MobileNo.Trim())).ToString();
                }
            }
        }

        catch (Exception ex)
        {
            LogHelper.EventLogHelper.Log("Bid Event > Send SMS Invitation : " + ex.Message, System.Diagnostics.EventLogEntryType.Error);
        }

        return success;
    }

    private string CreateInvitationSmsBody(BidDetails biddetails, BidParticipant participant)
    {
        return String.Format("You are invited to participate in a bid event;Ref. No.:{0}, initiated by Trans-Asia . Deadline: {1}", biddetails.ID, biddetails.SubmissionDeadline.ToString("MM/dd/yyyy hh:mm:ss tt"));
    }

    private string CreateInvitationBody(BidDetails biddetails, BidParticipant participant)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("<tr><td align='left'><h5>" + DateTime.Now.ToLongDateString() + "</h5></td></tr>");
        sb.Append("<tr><td align='left'><h3>INVITATION TO BID</h3></td></tr>");
        sb.Append("<tr>");
        sb.Append("<td valign='top'>");
        sb.Append("<p>");
        sb.Append("<b>TO: &nbsp;&nbsp;<u>" + participant.Name + "</u></b>");
        sb.Append("<br /><br />");
        sb.Append("Good Day!");
        sb.Append("<br /><br />");
        sb.Append("We are glad to inform you that you have been invited to participate in an online bidding event which was initiated by Trans-Asia");
        sb.Append("</p>");

        sb.Append("<table style='font-size: 12px;width:100%;'>");
        sb.Append("<tr>");
        sb.Append("<td width='10px'></td>");
        sb.Append("<td style='font-weight:bold;width:20px;'>1.</td>");
        sb.Append("<td style='font-weight:bold;'>Bid Description</td>");
        sb.Append("</tr>");
        sb.Append("<tr>");
        sb.Append("<td width='30px' colspan='2'></td>");
        sb.Append("<td>" + biddetails.Description + "</td>");
        sb.Append("</tr>");
        sb.Append("<tr><td height='10px' colspan='3'></td></tr>");

        sb.Append("<tr>");
        sb.Append("<td width='10px'></td>");
        sb.Append("<td style='font-weight:bold;width:20px;'>2.</td>");
        sb.Append("<td style='font-weight:bold;'>Schedule of Bid</td>");
        sb.Append("</tr>");
        sb.Append("<tr>");
        sb.Append("<td width='30px' colspan='2'></td>");
        sb.Append("<td>");
        sb.Append("Submission Deadline : " + biddetails.SubmissionDeadline + "<br />");
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
        sb.Append("<li>Payment Terms - indicate applicable terms.</li>");
        // Commented by Angel 10/22/2008 ::  requested by Sir Seth
        //sb.Append("<br />");
        //sb.Append("<li>Billing Details</li>");
        //sb.Append("<ul>");
        //sb.Append("<li>Contact Person: Rose Soteco T# 730 2413</li>");
        //sb.Append("<li>Contact Details: 2F GT Plaza Tower 1, Pioneer cor Madison Sts., Mandaluyong City</li>");
        //sb.Append("</ul>");
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
        sb.Append("No change in price quoted shall be allowed after bid submission unless negotiated by Trans-Asia");
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
        sb.Append("Responses to the Invitation to Bid/Tender shall be sent by the vendors using the e-Sourcing Portal.");
        sb.Append("Price schedules (details) and other attachments shall be in Acrobat Format(i.e. PDF),"); ;
        sb.Append("or in any password-protected file (e.g. MS Word, Excel)");
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
        sb.Append("<li>Scanned Summary documents without bidder's signature</li>");
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
        sb.Append("The lowest/highest bidder is not necessarily the winning bidder. Trans-Asia shall not be bound to assign any reason for not accepting any bid or accepting it in part.");
        sb.Append("Bids are still subject to further ecaluation. Trans-Asia shall award the winning supplier through a Purchase Order.");
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
        sb.Append("<br /><br />");
        sb.Append("######################################################################################<br />");
        sb.Append("&nbsp;Credentials:<br />");
        sb.Append("&nbsp;&nbsp;&nbsp;Username: " + participant.Username + "<br /><br />");
        sb.Append("&nbsp;Notes:<br />");
        sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Password is for login.<br />");
        sb.Append("&nbsp;&nbsp;&nbsp;Username is NOT CASE SENSITIVE.<br />");
        sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;If you don't know or forgot your password, go to eBid login page and click forgot password.<br />");
        sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Use the username provided. Click Send. Your password will be sent to this email address.<br />");
        sb.Append("######################################################################################<br />");
        sb.Append("<br /><br /><br />");
        sb.Append("Sincerely Yours,");
        sb.Append("<br /><br />");
        sb.Append(biddetails.Creator);
        sb.Append("<br /><br />");
        sb.Append("</p>");
        sb.Append("</td>");
        sb.Append("</tr>");

        return MailTemplate.IntegrateBodyIntoTemplate(sb.ToString());
    }
    #endregion



}
