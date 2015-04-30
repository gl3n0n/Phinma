using System;
using System.Collections;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using EBid.lib.bid.data;
using EBid.lib.bid.trans;
using EBid.lib.constant;
using EBid.lib;
using System.IO;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Text;
using EBid.ConnectionString;

namespace EBid.web.vendor_screens
{
    public partial class submittender : System.Web.UI.Page
    {
        private const string BR = "<br />";
        private const string BULLET = "&#187;";
        private const string BR_BULLET = "<br />&#187;";
        private string connstring = "";

        protected void Page_LoadComplete(object sender, System.EventArgs e)
        {
            //viewDT();
        }

        void viewDT()
        {
            DataTable table = (DataTable)ViewState["BidTenders"];
            if (table != null)
            {
                foreach (DataRow row in table.Rows)
                {
                    int i = 0;
                    foreach (object item in row.ItemArray)
                    {
                        Response.Write(table.Columns[i] + ": " + item.ToString() + "<br>");
                        i++;
                    }
                }
            }
        }

        protected void Page_Load(object sender, System.EventArgs e)
        {
            FormsAuthenticationHelper.AuthenticateUserWithReturnUrl();
            connstring = Client.ConnectionString(HttpContext.Current.Session["clientid"].ToString());
            if (Int32.Parse(Session[Constant.SESSION_USERTYPE].ToString().Trim()) != (int)Constant.USERTYPE.VENDOR)
                Response.Redirect("../unauthorizedaccess.aspx");

            PageTitle.InnerText = String.Format(Constant.TITLEFORMAT, "Bid Tender Creation / Submission");

            if (Session[Constant.SESSION_BIDREFNO] == null)
                Response.Redirect("bids.aspx");

            if (!IsPostBack)
            {
                if (Request.QueryString[Constant.QS_TASKTYPE] == "2")
                {
                    InitializeGridViews();
                }
                else if (Request.QueryString[Constant.QS_TASKTYPE] == "3")
                {
                    InitializeGridViewsWithFilter();
                }
                else
                {
                    Session[Constant.SESSION_BIDTENDERNO] = 0;
                    InitializeGridViews();
                }
            }
            litMsg.Text = "";
            litErrMsg.Text = "";
        }

        #region Initializations
        private void InitializeGridViewsWithFilter()
        {
            InitializeBidTendersWithFilter();
            InitializeFileAttachments();
        }


        private void InitializeGridViews()
        {
            InitializeBidTenders();
            InitializeFileAttachments();
        }

        private void InitializeBidTendersWithFilter()
        {// get bid tenders of this vendor for this bid event
            IEnumerator iEnum = dsItemDetails.Select(DataSourceSelectArguments.Empty).GetEnumerator();
            DataTable dtBidTenders = (DataTable)ViewState["BidTenders"];
            if (dtBidTenders == null)
                dtBidTenders = CreateEmptyBidTendersTable();
            int selected = 0, ctr = 0;

            while (iEnum.MoveNext())
            {
                DataRowView dr = (DataRowView)iEnum.Current;

                AddBidTenderRow(ref dtBidTenders,
                    int.Parse(dr["BidTenderNo"].ToString()),
                    int.Parse(dr["BidDetailNo"].ToString()),
                    dr["SKU"].ToString(),
                    dr["DetailDesc"].ToString(),
                    dr["UnitOfMeasure"].ToString(),
                    int.Parse(dr["Qty"].ToString()),
                    double.Parse(dr["Amount"].ToString()),
                    double.Parse(dr["DeliveryCost"].ToString()),
                    double.Parse(dr["Discount"].ToString()),
                    dr["Warranty"].ToString(),
                    dr["Remarks"].ToString(),
                    DateTime.Parse(dr["DeliveryDate"].ToString()),
                    "",
                    "",
                    "",
                    "");

                if (
                            ((dr["ConversionStatus"].ToString() != "2") &&
                            (dr["ConversionStatus"].ToString() != "4")) &&
                            ((dr["WithdrawalStatus"].ToString() != "2") &&
                            (dr["WithdrawalStatus"].ToString() != "4"))
                            )
                {
                    selected = ctr;
                }
                ctr++;
            }

            ViewState["BidTenders"] = dtBidTenders;
            dvBidTender.DataSource = dtBidTenders;
            dvBidTender.DataBind();

            gvBidItemDetails.SelectedIndex = 0;

            dvBidTender.PageIndex = 0;
            dvBidTender.DataSource = dtBidTenders;
            dvBidTender.DataBind();

        }

        private void InitializeBidTenders()
        {   // get bid tenders of this vendor for this bid event
            IEnumerator iEnum = dsItemDetails.Select(DataSourceSelectArguments.Empty).GetEnumerator();
            DataTable dtBidTenders = (DataTable)ViewState["BidTenders"];
            if (dtBidTenders == null)
                dtBidTenders = CreateEmptyBidTendersTable();
            int selected = 0, ctr = 0;

            while (iEnum.MoveNext())
            {
                DataRowView dr = (DataRowView)iEnum.Current;

                AddBidTenderRow(ref dtBidTenders,
                    int.Parse(dr["BidTenderNo"].ToString()),
                    int.Parse(dr["BidDetailNo"].ToString()),
                    dr["SKU"].ToString(),
                    dr["DetailDesc"].ToString(),
                    dr["UnitOfMeasure"].ToString(),
                    int.Parse(dr["Qty"].ToString()),
                    double.Parse(dr["Amount"].ToString()),
                    double.Parse(dr["DeliveryCost"].ToString()),
                    double.Parse(dr["Discount"].ToString()),
                    dr["Warranty"].ToString(),
                    dr["Remarks"].ToString(),
                    DateTime.Parse(dr["DeliveryDate"].ToString()), "", "", "", ""
                    //dr["Incoterm"].ToString(), 
                    //dr["PaymentTerms"].ToString(),
                    //dr["Currency"].ToString(),
                    //dr["LeadTime"].ToString()
                    );

                if (Session[Constant.SESSION_BIDTENDERNO] != null)
                {
                    if (selected == 0)
                        selected = (dr["BidTenderNo"].ToString() == Session[Constant.SESSION_BIDTENDERNO].ToString()) ? ctr : 0;
                }
                ctr++;
            }

            ViewState["BidTenders"] = dtBidTenders;
            dvBidTender.DataSource = dtBidTenders;
            dvBidTender.DataBind();

            gvBidItemDetails.SelectedIndex = 0;

            dvBidTender.PageIndex = 0;
            dvBidTender.DataSource = dtBidTenders;
            dvBidTender.DataBind();
        }

        private void InitializeFileAttachments()
        {   // get vendor file attachments for this bid event
            IEnumerator iEnum = dsFileAttachments.Select(DataSourceSelectArguments.Empty).GetEnumerator();
            DataTable dtAttachments = (DataTable)ViewState["BidTenderAttachments"];
            if (dtAttachments == null)
            {
                dtAttachments = CreateAttachmentsTable();
            }
            // add empty row
            AddEmptyAttachmentRow(ref dtAttachments);

            while (iEnum.MoveNext())
            {
                DataRowView dr = (DataRowView)iEnum.Current;

                AddAttachmentRow(ref dtAttachments, dr["OriginalFileName"].ToString(), dr["ActualFileName"].ToString(), dr["IsDetachable"].ToString() == "True" ? "1" : "0", dr["FileAttachment"].ToString(), dr["AsDraft"].ToString() == "True" ? "1" : "0");
            }

            // save to viewstate
            ViewState["BidTenderAttachments"] = dtAttachments;
            // bind to gridview
            gvFileAttachment.DataSource = dtAttachments;
            gvFileAttachment.DataBind();
        }
        #endregion

        #region Action
        // save tender as draft
        protected void btnDraft_Click(object sender, EventArgs e)
        {
            if (isAllowSubmit())
            {
                // check inputs
                if (IsValid)
                {
                    BidTender[] _bidtenders = new BidTender[int.Parse(ViewState["BIDTENDERCOUNT"].ToString())];
                    // get inputs here, store in a class
                    GetBidTenders(ref _bidtenders, Constant.BID_TENDER_STATUS_DRAFT);

                    if (Request.QueryString[Constant.QS_TASKTYPE] == "2")
                    {   // update draft
                        if (UpdateBidTender(_bidtenders))
                        {   // update was successful                        
                            if (UpdateBidTenderAttachments(int.Parse(Session[Constant.SESSION_BIDREFNO].ToString()), int.Parse(Session[Constant.SESSION_USERID].ToString()), 1))
                            {   // updating of bid tender attachments was successful
                                // save comments
                                //int lastbidtenderno = int.Parse(Session["LastBidTenderNo"].ToString());

                                //if (SaveBidTenderComment(lastbidtenderno))
                                Response.Redirect("tenderdrafts.aspx");
                                //else
                                //    litErrMsg.Text = "<p style='color:red;'>" + "Bid tender comment was not saved successfully." + "</p>";                            
                            }
                            else
                            {   // updating of bid tender attachments was unsuccessful
                                litErrMsg.Text = "<p style='color:red;'>" + "Bid tender attachments were not updated successfully." + "</p>";
                            }
                        }
                        else
                        {   // update was not successful
                            litErrMsg.Text = "<p style='color:red;'>" + "Bid tender was not updated successfully." + "</p>";
                        }
                    }
                    else
                    {   // save as draft
                        if (SaveBidTender(_bidtenders))
                        {   // saving was successful                        
                            if (SaveBidTenderAttachments(int.Parse(Session[Constant.SESSION_BIDREFNO].ToString()), int.Parse(Session[Constant.SESSION_USERID].ToString()), 1))
                            {   // updating of bid tender attachments was successful
                                // save comments
                                //int lastbidtenderno = int.Parse(Session["LastBidTenderNo"].ToString());

                                //if (SaveBidTenderComment(lastbidtenderno))
                                Response.Redirect("tenderdrafts.aspx");
                                //else
                                //    litErrMsg.Text = "<p style='color:red;'>" + "Bid tender comment was not saved successfully." + "</p>";
                            }
                            else
                            {   // updating of bid tender attachments was unsuccessful
                                litErrMsg.Text = "<p style='color:red;'>" + "Bid tender attachments were not saved successfully." + "</p>";
                            }
                        }
                        else
                        {   // saving was not successful
                            litErrMsg.Text = "<p style='color:red;'>" + "Bid tender was not saved successfully." + "</p>";
                        }
                    }

                }
                
            }
            else
            {
                litErrMsg.Text = "<p style='color:red; align:center;'>" + "Bid submission deadline (" + GetSubmissionDeadline().ToString() + ") has been reached." + "</p>";
            }
        }

        // submit tender for approval / awarding
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (isAllowSubmit())
            {
                string msg = string.Empty;
                // check inputs
                if (IsValid)
                {
                    BidTender[] _bidtenders = new BidTender[int.Parse(ViewState["BIDTENDERCOUNT"].ToString())];
                    // get inputs here, store in a class
                    GetBidTenders(ref _bidtenders, Constant.BID_TENDER_STATUS_SUBMITTED);

                    if (Request.QueryString[Constant.QS_TASKTYPE] == "2")
                    {   // update and submit
                        if (UpdateBidTender(_bidtenders))
                        {   // update was successful
                            if (UpdateBidTenderAttachments(int.Parse(Session[Constant.SESSION_BIDREFNO].ToString()), int.Parse(Session[Constant.SESSION_USERID].ToString()), 0))
                            {   // updating of bid tender attachments was successful
                                // save comments
                                //int lastbidtenderno = int.Parse(Session["LastBidTenderNo"].ToString());

                                //if (SaveBidTenderComment(lastbidtenderno))
                                Response.Redirect("submittedtenders.aspx");
                                //else
                                //    litErrMsg.Text = "<p style='color:red;'>" + "Bid tender comment was not saved successfully." + "</p>";
                            }
                            else
                            {   // updating of bid tender attachments was unsuccessful
                                litErrMsg.Text = "<p style='color:red;'>" + "Bid tender attachments were not updated successfully." + "</p>";
                            }
                        }
                        else
                        {   // update was not successful
                            litErrMsg.Text = "<p style='color:red;'>" + "Bid tender was not updated successfully." + "</p>";
                        }
                    }
                    else
                    {   // save and submit
                        if (SaveBidTender(_bidtenders))
                        {   // saving was successful
                            if (SaveBidTenderAttachments(int.Parse(Session[Constant.SESSION_BIDREFNO].ToString()), int.Parse(Session[Constant.SESSION_USERID].ToString()), 0))
                            {   // updating of bid tender attachments was successful
                                // save comments
                                //int lastbidtenderno = int.Parse(Session["LastBidTenderNo"].ToString());

                                //if (SaveBidTenderComment(lastbidtenderno))
                                Response.Redirect("submittedtenders.aspx");
                                //else
                                //    litErrMsg.Text = "<p style='color:red;'>" + "Bid tender comment was not saved successfully." + "</p>";
                            }
                            else
                            {   // updating of bid tender attachments was unsuccessful
                                litErrMsg.Text = "<p style='color:red;'>" + "Bid tender attachments were not saved successfully." + "</p>";
                            }
                        }
                        else
                        {   // saving was not successful
                            litErrMsg.Text = "<p style='color:red;'>" + "Bid tender was not saved successfully." + "</p>";
                        }
                    }
                }
                
            }
            else
            {
                litErrMsg.Text = "<p style='color:red; align:center;'>" + "Bid submission deadline (" + GetSubmissionDeadline().ToString() + ") has been reached." + "</p>";
            }

        }

        // cancel submission/creation of bid tender
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            if (Session[Constant.SESSION_LASTPAGE] != null)
                Response.Redirect(Session[Constant.SESSION_LASTPAGE].ToString());
            Response.Redirect("biddetails.aspx");
        }

        #region Bid Tender
        private bool SaveBidTender(BidTender[] bidtenders)
        {
            string s = string.Empty;

            for (int i = 0; i < bidtenders.Length; i++)
            {
                BidTender bt = bidtenders[i];

                if (!AddEditTender(bt.BidTenderNo, bt.BidDetailNo, bt.VendorId, bt.Status, bt.Amount, bt.DeliveryCost, bt.Discount, bt.Warranty, bt.Remarks, bt.Comment, bt.Incoterm, bt.PaymentTerms, bt.Currency, bt.LeadTime))
                    s += "0";
            }
            return (s.Length == 0);
        }

        private bool UpdateBidTender(BidTender[] bidtenders)
        {
            string s = string.Empty;

            for (int i = 0; i < bidtenders.Length; i++)
            {
                BidTender bt = bidtenders[i];

                if (!AddEditTender(bt.BidTenderNo, bt.BidDetailNo, bt.VendorId, bt.Status, bt.Amount, bt.DeliveryCost, bt.Discount, bt.Warranty, bt.Remarks, bt.Comment, bt.Incoterm, bt.PaymentTerms, bt.Currency, bt.LeadTime))
                    s += "0";
            }
            return (s.Length == 0);
        }

        public bool AddEditTender(string vBidTenderNo, string vBidDetailNo, string vVendorId, string vStatus, string vAmount,
            string vDeliveryCost, string vDiscount, string vWarranty, string vRemarks, string vComments, string vIncoterm, string vPaymentTerms, string vCurrency, string vLeadTime)
        {
            SqlConnection sqlConnect = new SqlConnection(connstring);
            SqlTransaction sqlTransact = null;
            bool isSuccessful = false;

            try
            {
                sqlConnect.Open();
                sqlTransact = sqlConnect.BeginTransaction();

                SqlParameter[] sqlparams = new SqlParameter[15];
                sqlparams[0] = new SqlParameter("@BidTenderNo", SqlDbType.Int);
                sqlparams[0].Value = Int32.Parse(vBidTenderNo);
                sqlparams[1] = new SqlParameter("@BidDetailNo", SqlDbType.Int);
                sqlparams[1].Value = Int32.Parse(vBidDetailNo);
                sqlparams[2] = new SqlParameter("@VendorId", SqlDbType.Int);
                sqlparams[2].Value = Int32.Parse(vVendorId);
                sqlparams[3] = new SqlParameter("@Status", SqlDbType.Int);
                sqlparams[3].Value = Int32.Parse(vStatus);
                sqlparams[4] = new SqlParameter("@Amount", SqlDbType.Money);
                sqlparams[4].Value = Convert.ToDouble(vAmount);
                sqlparams[5] = new SqlParameter("@DeliveryCost", SqlDbType.Money);
                sqlparams[5].Value = Convert.ToDouble(vDeliveryCost);
                sqlparams[6] = new SqlParameter("@Discount", SqlDbType.Money);
                sqlparams[6].Value = Convert.ToDouble(vDiscount);
                sqlparams[7] = new SqlParameter("@Warranty", SqlDbType.VarChar);
                sqlparams[7].Value = vWarranty;
                sqlparams[8] = new SqlParameter("@Remarks", SqlDbType.VarChar);
                sqlparams[8].Value = vRemarks;
                sqlparams[9] = new SqlParameter("@Comment", SqlDbType.VarChar);
                sqlparams[9].Value = vComments;
                sqlparams[10] = new SqlParameter("@Incoterm", SqlDbType.VarChar);
                sqlparams[10].Value = vIncoterm;
                sqlparams[11] = new SqlParameter("@PaymentTerms", SqlDbType.VarChar);
                sqlparams[11].Value = vPaymentTerms;
                sqlparams[12] = new SqlParameter("@Currency", SqlDbType.VarChar);
                sqlparams[12].Value = vCurrency;
                sqlparams[13] = new SqlParameter("@LeadTime", SqlDbType.VarChar);
                sqlparams[13].Value = vLeadTime;
                sqlparams[14] = new SqlParameter("@Result", SqlDbType.Int);
                sqlparams[14].Direction = ParameterDirection.Output;

                SqlHelper.ExecuteNonQuery(sqlTransact, CommandType.StoredProcedure, "sp_AddEditBidTender", sqlparams);
                sqlTransact.Commit();
                Session["LastBidTenderNo"] = sqlparams[14].Value;
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

        private void GetBidTenders(ref BidTender[] bidtenders, int status)
        {   // we assume that there is already atleast one bid tender to submit/save
            DataTable dtBidTenders = (DataTable)ViewState["BidTenders"];
            int ctr = 0;

            if (dtBidTenders.Rows.Count > 0)
            {
                foreach (DataRow dr in dtBidTenders.Rows)
                {
                    if (double.Parse(dr["Amount"].ToString()) > 0)
                    {
                        BidTender bt = new BidTender();
                        bt.Amount = dr["Amount"].ToString();
                        bt.BidDetailNo = dr["BidDetailNo"].ToString();
                        bt.BidTenderNo = dr["BidTenderNo"].ToString();
                        bt.DeliveryCost = dr["DeliveryCost"].ToString();
                        bt.Discount = dr["Discount"].ToString();
                        bt.Remarks = dr["Remarks"].ToString();
                        bt.Status = status.ToString();
                        bt.VendorId = Session[Constant.SESSION_USERID].ToString();
                        bt.Warranty = dr["Warranty"].ToString();
                        bt.Comment = dr["Comment"].ToString();
                        bt.Incoterm = dr["Incoterm"].ToString();
                        bt.PaymentTerms = dr["PaymentTerms"].ToString();
                        bt.Currency = dr["Currency"].ToString();
                        bt.LeadTime = dr["LeadTime"].ToString();
                        bidtenders[ctr] = bt;
                        ctr++;
                    }
                }
            }
        }

        #endregion

        #region Bid Tender Attachments
        private bool SaveBidTenderAttachments(int bidRefNo, int vendorId, int asDraft)
        {
            bool isSuccessful = false;
            try
            {
                DataTable dtAttachments = (DataTable)ViewState["BidTenderAttachments"];
                string s = string.Empty;

                for (int i = 1; i < dtAttachments.Rows.Count; i++)
                {
                    if (asDraft == 1)
                    {
                        if (int.Parse(dtAttachments.Rows[i]["isDetachable"].ToString()) == 1)
                        {
                            s += SaveBidTenderAttachments(bidRefNo, vendorId, dtAttachments.Rows[i]["Original"].ToString(), dtAttachments.Rows[i]["Actual"].ToString(), int.Parse(dtAttachments.Rows[i]["isDetachable"].ToString()), 1);
                        }
                        else
                        {
                            s += SaveBidTenderAttachments(bidRefNo, vendorId, dtAttachments.Rows[i]["Original"].ToString(), dtAttachments.Rows[i]["Actual"].ToString(), int.Parse(dtAttachments.Rows[i]["isDetachable"].ToString()), int.Parse(dtAttachments.Rows[i]["AsDraft"].ToString()));
                        }
                    }
                    else
                    {
                        s += SaveBidTenderAttachments(bidRefNo, vendorId, dtAttachments.Rows[i]["Original"].ToString(), dtAttachments.Rows[i]["Actual"].ToString(), int.Parse(dtAttachments.Rows[i]["isDetachable"].ToString()), 0);
                    }
                }
                isSuccessful = s.IndexOf("0") >= 0 ? false : true;
            }
            catch
            {
                isSuccessful = false;
            }
            return isSuccessful;
        }

        private string SaveBidTenderAttachments(int bidRefNo, int vendorId, string originalFileName, string actualFileName, int isDetachable, int asDraft)
        {
            SqlConnection sqlConnect = new SqlConnection(connstring);
            SqlTransaction sqlTransact = null;
            string isSuccessful = string.Empty;

            try
            {
                sqlConnect.Open();
                sqlTransact = sqlConnect.BeginTransaction();

                SqlParameter[] sqlParams = new SqlParameter[7];
                sqlParams[0] = new SqlParameter("@BidRefNo", SqlDbType.Int);
                sqlParams[0].Value = bidRefNo;
                sqlParams[1] = new SqlParameter("@VendorId", SqlDbType.Int);
                sqlParams[1].Value = vendorId;
                sqlParams[2] = new SqlParameter("@OriginalFileName", SqlDbType.VarChar);
                sqlParams[2].Value = originalFileName;
                sqlParams[3] = new SqlParameter("@ActualFileName", SqlDbType.VarChar);
                sqlParams[3].Value = actualFileName;
                sqlParams[4] = new SqlParameter("@IsDetachable", SqlDbType.Int);
                sqlParams[4].Value = isDetachable;
                sqlParams[5] = new SqlParameter("@AsDraft", SqlDbType.Int);
                sqlParams[5].Value = asDraft;
                sqlParams[6] = new SqlParameter("@FileUploadID", SqlDbType.Int);
                sqlParams[6].Direction = ParameterDirection.Output;

                SqlHelper.ExecuteNonQuery(connstring, CommandType.StoredProcedure, "sp_AddVendorTenderFileAttachment", sqlParams);

                sqlTransact.Commit();

                int r = int.Parse(sqlParams[6].Value.ToString().Trim());
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

        private bool UpdateBidTenderAttachments(int bidRefNo, int vendorId, int asDraft)
        {
            // just delete all previos attachments of this vendor
            if (DeleteBidTenderAttachments(bidRefNo, vendorId))
            {
                // then add them again                
                return SaveBidTenderAttachments(bidRefNo, vendorId, asDraft);
            }
            else
                return false;
        }

        private bool DeleteBidTenderAttachments(int bidRefNo, int vendorId)
        {
            SqlConnection sqlConnect = new SqlConnection(connstring);
            SqlTransaction sqlTransact = null;
            bool isSuccessful = false;

            try
            {
                sqlConnect.Open();
                sqlTransact = sqlConnect.BeginTransaction();

                SqlParameter[] sqlParams = new SqlParameter[2];
                sqlParams[0] = new SqlParameter("@BidRefNo", SqlDbType.Int);
                sqlParams[0].Value = bidRefNo;
                sqlParams[1] = new SqlParameter("@VendorId", SqlDbType.Int);
                sqlParams[1].Value = vendorId;

                SqlHelper.ExecuteNonQuery(connstring, CommandType.StoredProcedure, "sp_DeleteVendorBidEventFileAttachments", sqlParams);

                sqlTransact.Commit();

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
        #endregion
        #endregion

        #region Bid Items
        protected void gvBidItemDetails_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dtBidTenders = (DataTable)ViewState["BidTenders"];

            if (dtBidTenders != null)
            {
                Session[Constant.SESSION_BIDTENDERNO] = gvBidItemDetails.DataKeys[gvBidItemDetails.SelectedIndex].Values["BidTenderNo"].ToString();
                dvBidTender.DataSource = dtBidTenders;
                dvBidTender.PageIndex = gvBidItemDetails.SelectedIndex;
                dvBidTender.DataBind();
            }
        }
        #endregion

        #region Bid Tenders
        private DataTable CreateEmptyBidTendersTable()
        {
            DataTable dtBidTenders = new DataTable();

            DataColumn dc;

            dc = new DataColumn("BidTenderNo", typeof(System.Int32));
            dtBidTenders.Columns.Add(dc);
            dc = new DataColumn("BidDetailNo", typeof(System.Int32));
            dtBidTenders.Columns.Add(dc);
            dc = new DataColumn("SKU", typeof(System.String));
            dtBidTenders.Columns.Add(dc);
            dc = new DataColumn("Description", typeof(System.String));
            dtBidTenders.Columns.Add(dc);
            dc = new DataColumn("UnitOfMeasure", typeof(System.String));
            dtBidTenders.Columns.Add(dc);
            dc = new DataColumn("Quantity", typeof(System.Int32));
            dtBidTenders.Columns.Add(dc);
            dc = new DataColumn("Amount", typeof(System.Double));
            dtBidTenders.Columns.Add(dc);
            dc = new DataColumn("DeliveryCost", typeof(System.Double));
            dtBidTenders.Columns.Add(dc);
            dc = new DataColumn("Discount", typeof(System.Double));
            dtBidTenders.Columns.Add(dc);
            dc = new DataColumn("Warranty", typeof(System.String));
            dtBidTenders.Columns.Add(dc);
            dc = new DataColumn("Remarks", typeof(System.String));
            dtBidTenders.Columns.Add(dc);
            dc = new DataColumn("DeliveryDate", typeof(System.DateTime));
            dtBidTenders.Columns.Add(dc);
            dc = new DataColumn("Comment", typeof(System.String));
            dtBidTenders.Columns.Add(dc);
            dc = new DataColumn("Incoterm", typeof(System.String));
            dtBidTenders.Columns.Add(dc);
            dc = new DataColumn("PaymentTerms", typeof(System.String));
            dtBidTenders.Columns.Add(dc);
            dc = new DataColumn("Currency", typeof(System.String));
            dtBidTenders.Columns.Add(dc);
            dc = new DataColumn("LeadTime", typeof(System.String));
            dtBidTenders.Columns.Add(dc);


            return dtBidTenders;
        }

        private void AddEmptyBidTenderRow(ref DataTable dtBidTenders, int biddetailno, int quantity, string sku)
        {
            // query all bid items for this bid event
            DataRow dr = dtBidTenders.NewRow();

            dr["BidTenderNo"] = 0;
            dr["BidDetailNo"] = biddetailno;
            dr["SKU"] = sku;
            dr["Quantity"] = quantity;
            dr["Amount"] = 0.00;
            dr["DeliveryCost"] = 0.00;
            dr["Discount"] = 0.00;
            dr["Warranty"] = "";
            dr["Remarks"] = "";
            dr["DeliveryDate"] = DateTime.Now;
            dr["Comment"] = "";
            dr["Incoterm"] = "";
            dr["PaymentTerms"] = "";
            dr["Currency"] = "";
            dr["LeadTime"] = "";
            dtBidTenders.Rows.Add(dr);
        }

        private void AddEmptyBidTenderRow(ref DataTable dtBidTenders, int biddetailno,
            int quantity, string sku,
            string description, string uom)
        {
            // query all bid items for this bid event
            DataRow dr = dtBidTenders.NewRow();

            dr["BidTenderNo"] = 0;
            dr["BidDetailNo"] = biddetailno;
            dr["SKU"] = sku;
            dr["Description"] = description;
            dr["UnitOfMeasure"] = uom;
            dr["Quantity"] = quantity;
            dr["Amount"] = 0.00;
            dr["DeliveryCost"] = 0.00;
            dr["Discount"] = 0.00;
            dr["Warranty"] = "";
            dr["Remarks"] = "";
            dr["DeliveryDate"] = DateTime.Now;
            dr["Comment"] = "";
            dr["Incoterm"] = "";
            dr["PaymentTerms"] = "";
            dr["Currency"] = "";
            dr["LeadTime"] = "";
            dtBidTenders.Rows.Add(dr);
        }

        private void AddBidTenderRow(ref DataTable dtBidTenders, int bidtenderno, int biddetailno,
            string sku, string description, string uom, int quantity, double amount, double deliverycost,
            double discount, string warranty, string remarks, DateTime deliverydate, string incoterm, string paymentTerms, string currency, string leadtime)
        {
            // query all bid items for this bid event
            DataRow dr = dtBidTenders.NewRow();

            dr["BidTenderNo"] = bidtenderno;
            dr["BidDetailNo"] = biddetailno;
            dr["SKU"] = sku;
            dr["Description"] = description;
            dr["UnitOfMeasure"] = uom;
            dr["Quantity"] = quantity;
            dr["Amount"] = amount;
            dr["DeliveryCost"] = deliverycost;
            dr["Discount"] = discount;
            dr["Warranty"] = warranty;
            dr["Remarks"] = remarks;
            dr["DeliveryDate"] = deliverydate;
            dr["Incoterm"] = incoterm;
            dr["PaymentTerms"] = paymentTerms;
            dr["Currency"] = currency;
            dr["LeadTime"] = leadtime;
            dtBidTenders.Rows.Add(dr);
        }

        private void UpdateBidTenderRow(ref DataTable dtBidTenders, int rowindex, double amount, double discount, double deliverycost, string warranty, string remarks, string comment, string incoterm, string paymentTerms, string currency, string leadtime)
        {
            // query all bid items for this bid event
            DataRow dr = dtBidTenders.Rows[rowindex];

            dr["Amount"] = amount;
            dr["DeliveryCost"] = deliverycost;
            dr["Discount"] = discount;
            dr["Warranty"] = warranty;
            dr["Remarks"] = remarks;
            dr["Comment"] = comment;
            //Response.Write(dr["Incoterm"].ToString());
            dr["Incoterm"] = incoterm;
            dr["PaymentTerms"] = paymentTerms;
            dr["Currency"] = currency;
            dr["LeadTime"] = leadtime;
        }

        protected void dvBidTender_ItemCommand(object sender, DetailsViewCommandEventArgs e)
        {
            if (e.CommandName == "Save")
            {
                if (isAllowSubmit())
                {
                    int i = dvBidTender.PageIndex;
                    bool success = false;
                    string errMsg = string.Empty;

                    DataTable dtBidTenders = (DataTable)ViewState["BidTenders"];

                    // get controls
                    TextBox tbAmount = (TextBox)FindControlFromDetailsView(dvBidTender, "txtAmount");
                    TextBox tbAmountCents = (TextBox)FindControlFromDetailsView(dvBidTender, "txtAmountCents");
                    TextBox tbDiscount = (TextBox)FindControlFromDetailsView(dvBidTender, "txtDiscount");
                    TextBox tbDiscountCents = (TextBox)FindControlFromDetailsView(dvBidTender, "txtDiscountCents");
                    TextBox tbDeliveryCost = (TextBox)FindControlFromDetailsView(dvBidTender, "txtDeliveryCost");
                    TextBox tbDeliveryCostCents = (TextBox)FindControlFromDetailsView(dvBidTender, "txtDeliveryCostCents");
                    TextBox tbWarranty = (TextBox)FindControlFromDetailsView(dvBidTender, "txtWarranty");
                    TextBox tbRemarks = (TextBox)FindControlFromDetailsView(dvBidTender, "txtRemarks");
                    TextBox tbTotal = (TextBox)FindControlFromDetailsView(dvBidTender, "txtTotalBidTenderPrice");
                    TextBox tbComment = (TextBox)FindControlFromDetailsView(dvBidTender, "txtComment");
                    Label lbAmount = (Label)FindControlFromDetailsView(dvBidTender, "lblAmount");
                    Label lbWarranty = (Label)FindControlFromDetailsView(dvBidTender, "lblWarranty");
		    Label lbLeadTime = (Label)FindControlFromDetailsView(dvBidTender, "lblLeadTime");
                    DropDownList tbIncoterm = (DropDownList)FindControlFromDetailsView(dvBidTender, "txtIncoterm");
                    DropDownList tbCurrency = (DropDownList)FindControlFromDetailsView(dvBidTender, "txtCurrency");
                    DropDownList tbPaymentTerms = (DropDownList)FindControlFromDetailsView(dvBidTender, "txtPaymentTerms");
                    TextBox tbLeadTime = (TextBox)FindControlFromDetailsView(dvBidTender, "txtLeadTime");

                    string amount = tbAmount.Text.Trim() + "." + tbAmountCents.Text.Trim();
                    string discount = tbDiscount.Text.Trim() + "." + tbDiscountCents.Text.Trim();
                    string deliverycost = tbDeliveryCost.Text.Trim() + "." + tbDeliveryCostCents.Text.Trim();

                    if (AreBidTenderInputsValid(ref errMsg, amount, discount, deliverycost, tbTotal.Text.Trim(), tbLeadTime.Text.Trim(), tbWarranty.Text.Trim(), tbComment.Text.Trim(), tbIncoterm.Text.Trim(), tbPaymentTerms.Text.Trim(), tbCurrency.SelectedValue.Trim()))
                    {
                        // update table
                        try
                        {
                            UpdateBidTenderRow(ref dtBidTenders, i,
                                double.Parse(amount),
                                double.Parse(discount),
                                double.Parse(deliverycost),
                                tbWarranty.Text.Trim(),
                                tbRemarks.Text.Trim(),
				tbComment.Text.Trim(),
                                tbIncoterm.Text.Trim(),
                                tbPaymentTerms.Text.Trim(),
                                tbCurrency.SelectedValue.Trim(),
                                tbLeadTime.Text.Trim());

                            success = true;
                        }
                        catch
                        {
                            success = false;
                        }

                        ViewState["BidTenders"] = dtBidTenders;
                        dvBidTender.DataSource = dtBidTenders;
                        dvBidTender.DataBind();
                    }
                    else
                        success = false;

                    string style = "style='color:red;font-family:arial;font-size:11px;padding-left:15px;'";

                    if (!success)
                    {
                        litMsg.Text = "<div " + style + ">" + BR + "Please check the following:" + errMsg + "</div>";

                        if ((tbAmount.Text == "0") || (tbAmount.Text == ""))
                        {
                            lbAmount.Text = "* Required";
                        }
                        else
                        {
                            lbAmount.Text = "";
                        }
			
			if ((tbLeadTime.Text == "0") || (tbLeadTime.Text == ""))
                        {
                            lbLeadTime.Text = "* Required";
                        }
                        else
                        {
                            lbLeadTime.Text = "";
                        }


                        if ((tbWarranty.Text == "0") || (tbWarranty.Text == ""))
                        {
                            lbWarranty.Text = "* Required";
                        }
                        else
                        {
                            lbWarranty.Text = "";
                        }
                    }
                    else
                    {
                        litMsg.Text = "<div " + style + ">" + BR + "Changes saved.</div>";
                    }
                }
                else
                {
                    litMsg.Text = "<p style='color:red;'>" + "Bid submission deadline (" + GetSubmissionDeadline().ToString() + ") has been reached." + "</p>";
                }
            }
        }

        private bool AreBidTenderInputsValid(ref string msg,
            string amount, string discount, string deliverycost, string total, string leadtime, 
            string warranty, string comment, string incoterm, string paymentTerms, string currency)
        {
            StringBuilder sb = new StringBuilder();
            if (!FormattingHelper.IsNumeric(amount, System.Globalization.NumberStyles.Float | System.Globalization.NumberStyles.AllowThousands))
            {   // amount is not valid
                sb.Append(BR_BULLET + " Unit price is invalid.");
            }
            if (!FormattingHelper.IsNumeric(discount, System.Globalization.NumberStyles.Float | System.Globalization.NumberStyles.AllowThousands))
            {   // dicount is not valid
                sb.Append(BR_BULLET + " Discount is invalid.");
            }
            if (!FormattingHelper.IsNumeric(deliverycost, System.Globalization.NumberStyles.Float | System.Globalization.NumberStyles.AllowThousands))
            {   // delivery cost is not valid
                sb.Append(BR_BULLET + " Delivery Cost is invalid.");
            }
            if (!FormattingHelper.IsNumeric(total, System.Globalization.NumberStyles.Float | System.Globalization.NumberStyles.AllowThousands))
            {   // total is not valid
                sb.Append(BR_BULLET + " Total is invalid.");
            }
            if (double.Parse(amount) <= 0)
            {   // total is not valid
                sb.Append(BR_BULLET + " Unit price is required.");
            }
            else if (double.Parse(total) == 0)
            {   // total is not valid
                sb.Append(BR_BULLET + " Unit price is required.");
            }
            if (double.Parse(total) < 0)
            {   // total is not valid
                sb.Append(BR_BULLET + " Please check your inputs.");
            }
	    if (String.IsNullOrEmpty(leadtime))
            {   // leadtime is required
                sb.Append(BR_BULLET + " Lead Time is required.");
            }
            if (String.IsNullOrEmpty(warranty))
            {   // warranty is required
                sb.Append(BR_BULLET + " Warranty is required.");
            }
            if (String.IsNullOrEmpty(incoterm))
            {   // incoterm is required
                sb.Append(BR_BULLET + " Incoterm is required.");
            }
            if (String.IsNullOrEmpty(paymentTerms))
            {   // payment terms is required
                sb.Append(BR_BULLET + " Payment Terms is required.");
            }
            if (String.IsNullOrEmpty(currency))
            {   // currency terms is required
                sb.Append(BR_BULLET + " Currency is required.");
            }
            //if (String.IsNullOrEmpty(comment))
            //{   // warranty is required
            //    sb.Append(BR_BULLET + " Comment is required.");
            //}

            msg = sb.ToString();
            return !(sb.ToString().Length > 0);
        }

        protected void dvBidTender_DataBound(object sender, EventArgs e)
        {
            HiddenField hdnQuantity = (HiddenField)FindControl(dvBidTender, "hdnQuantity");
            Label lbQuantity = (Label)FindControl(dvBidTender, "lblQuantity");
            TextBox tbAmount = (TextBox)FindControl(dvBidTender, "txtAmount");
            TextBox tbAmountCents = (TextBox)FindControl(dvBidTender, "txtAmountCents");
            TextBox tbDiscount = (TextBox)FindControl(dvBidTender, "txtDiscount");
            TextBox tbDiscountCents = (TextBox)FindControl(dvBidTender, "txtDiscountCents");
            TextBox tbDeliveryCost = (TextBox)FindControl(dvBidTender, "txtDeliveryCost");
            TextBox tbDeliveryCostCents = (TextBox)FindControl(dvBidTender, "txtDeliveryCostCents");
            TextBox tbSubTotalPrice = (TextBox)FindControl(dvBidTender, "txtSubTotalPrice");
            TextBox tbTotalBidTenderPrice = (TextBox)FindControl(dvBidTender, "txtTotalBidTenderPrice");
            TextBox tbTotalBidTenderDiscount = (TextBox)FindControl(dvBidTender, "txtTotalBidTenderDiscount");
            DropDownList tbCurrency = (DropDownList)FindControl(dvBidTender, "txtCurrency");
            DropDownList tbIncoterm = (DropDownList)FindControl(dvBidTender, "txtIncoterm");
            DropDownList tbPaymentTerms = (DropDownList)FindControl(dvBidTender, "txtPaymentTerms");

            int i = dvBidTender.PageIndex;
            DataTable dtBidTenders = (DataTable)ViewState["BidTenders"];
            DataRow dr = dtBidTenders.Rows[i];

            string computeTotal = "ComputeGrossTotal(" + hdnQuantity.ClientID + "," + tbAmount.ClientID + "," + tbAmountCents.ClientID + "," + tbDiscount.ClientID + "," + tbDiscountCents.ClientID + "," + tbDeliveryCost.ClientID + "," + tbDeliveryCostCents.ClientID + "," + tbSubTotalPrice.ClientID + "," + tbTotalBidTenderPrice.ClientID + "," + tbTotalBidTenderDiscount.ClientID + ");";

            tbAmount.Attributes.Add("style", "text-align: right;");
            tbAmountCents.Attributes.Add("style", "text-align: center;");
            tbDiscount.Attributes.Add("style", "text-align: right;");
            tbDiscountCents.Attributes.Add("style", "text-align: center;");
            tbDeliveryCost.Attributes.Add("style", "text-align: right;");
            tbDeliveryCostCents.Attributes.Add("style", "text-align: center;");

            tbAmount.Attributes.Add("onkeydown", "return DigitsOnlyAndTransferFocus(event, " + tbAmountCents.ClientID + ");");
            tbAmountCents.Attributes.Add("onkeydown", "return DigitsOnly(event);");
            tbDiscount.Attributes.Add("onkeydown", "return DigitsOnlyAndTransferFocus(event, " + tbDiscountCents.ClientID + ");");
            tbDiscountCents.Attributes.Add("onkeydown", "return DigitsOnly(event);");
            tbDeliveryCost.Attributes.Add("onkeydown", "return DigitsOnlyAndTransferFocus(event, " + tbDeliveryCostCents.ClientID + ");");
            tbDeliveryCostCents.Attributes.Add("onkeydown", "return DigitsOnly(event);");

            tbAmount.Attributes.Add("onkeyup", computeTotal);
            tbAmountCents.Attributes.Add("onkeyup", computeTotal);
            tbDeliveryCost.Attributes.Add("onkeyup", computeTotal);
            tbDeliveryCostCents.Attributes.Add("onkeyup", computeTotal);
            tbDiscount.Attributes.Add("onkeyup", computeTotal);
            tbDiscountCents.Attributes.Add("onkeyup", computeTotal);

            //tbAmount.Attributes.Add("onkeypress", "return DigitsOnlyAndTransferFocus(event, " + tbAmountCents.ClientID + ");");
            tbAmountCents.Attributes.Add("onkeypress", "return DigitsOnlyAndTransferFocus(event, " + tbAmountCents.ClientID + ");");

            tbAmountCents.Attributes.Add("onfocus", tbAmountCents.ClientID + ".select();");
            tbDiscountCents.Attributes.Add("onfocus", tbDiscountCents.ClientID + ".select();");
            tbDeliveryCostCents.Attributes.Add("onfocus", tbDeliveryCostCents.ClientID + ".select();");

            tbAmount.Attributes.Add("onfocusout", "ResetIfEmpty2(this);");
            tbAmountCents.Attributes.Add("onfocusout", "ResetIfEmpty(this);");
            tbDeliveryCost.Attributes.Add("onfocusout", "ResetIfEmpty2(this);");
            tbDeliveryCostCents.Attributes.Add("onfocusout", "ResetIfEmpty(this);");
            tbDiscount.Attributes.Add("onfocusout", "ResetIfEmpty2(this);");
            tbDiscountCents.Attributes.Add("onfocusout", "ResetIfEmpty(this);");

            tbAmount.Attributes.Add("onfocus", computeTotal);
            tbIncoterm.SelectedValue = dr["Incoterm"].ToString();
            tbCurrency.SelectedValue = dr["Currency"].ToString();
            tbPaymentTerms.SelectedValue = dr["PaymentTerms"].ToString();
            
        }

        protected string GetWholeNumberPart(string input)
        {
            string[] s = input.Split(new char[] { '.' }, 2);
            return s[0];
        }

        protected string GetDecimalPart(string input)
        {
            string[] s = input.Split(new char[] { '.' }, 2);
            return s.Length >= 2 ? s[1] : "00";
        }
        #endregion

        #region FindControl
        private Control FindControlFromDetailsView(DetailsView dv, string ctrlName)
        {
            Control ctrl = null;

            for (int i = 0; i < dv.Rows.Count; i++)
            {
                DetailsViewRow dr = dv.Rows[i];
                for (int j = 0; j < dr.Cells.Count; j++)
                {
                    TableCell tc = dr.Cells[j];
                    for (int k = 0; k < tc.Controls.Count; k++)
                    {
                        if (tc.Controls[k].FindControl(ctrlName) != null)
                            return tc.Controls[k].FindControl(ctrlName);
                    }
                }
            }
            return ctrl;
        }

        private Control FindControl(Control parent, string ctrlName)
        {
            Control ctrl = null;

            ctrl = parent.FindControl(ctrlName);

            if (ctrl != null)
                return ctrl;

            for (int i = 0; i < parent.Controls.Count; i++)
            {
                ctrl = FindControl(parent.Controls[i], ctrlName);

                if (ctrl != null)
                    return ctrl;
            }

            return ctrl;
        }
        #endregion

        #region Attachments
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
            dc = new DataColumn("IsDetachable", typeof(System.Int32));
            dtAttachments.Columns.Add(dc);
            dc = new DataColumn("AsDraft", typeof(System.Int32));
            dtAttachments.Columns.Add(dc);
            dc = new DataColumn("FileAttachment", typeof(System.String));
            dtAttachments.Columns.Add(dc);

            return dtAttachments;
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
            dr["IsDetachable"] = 0;
            dr["AsDraft"] = 1;
            dr["FileAttachment"] = "";
            dtAttachments.Rows.Add(dr);
        }

        private void AddAttachmentRow(ref DataTable dtAttachments, string originalFileName, string actualFileName, string isDetachable, string fileAttachment, string asDraft)
        {
            DataRow dr = dtAttachments.NewRow();
            int nxtCounter = 0;
            if (dtAttachments.Rows.Count > 0)
                nxtCounter = int.Parse(dtAttachments.Rows[dtAttachments.Rows.Count - 1]["ID"].ToString()) + 1;

            dr["ID"] = nxtCounter;
            dr["Original"] = originalFileName;
            dr["Actual"] = actualFileName;
            dr["Attached"] = 1;
            dr["IsDetachable"] = isDetachable;
            dr["AsDraft"] = asDraft;
            dr["FileAttachment"] = fileAttachment;
            dtAttachments.Rows.Add(dr);
        }

        protected bool IsAttached(string isattached)
        {
            return isattached == "1" ? true : false;
        }

        protected bool IsRemovable(string isattached, string isdetachable)
        {
            if ((isattached == "1") && (isdetachable == "1"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private void Attach(string original, string actual)
        {
            DataTable dtAttachments = (DataTable)ViewState["BidTenderAttachments"];

            DataRow dr = dtAttachments.NewRow();
            int nxtCounter = 0;
            if (dtAttachments.Rows.Count > 0)
                nxtCounter = int.Parse(dtAttachments.Rows[dtAttachments.Rows.Count - 1]["ID"].ToString()) + 1;

            dr["ID"] = nxtCounter;
            dr["Original"] = original;
            dr["Actual"] = actual;
            dr["Attached"] = 1;
            dr["IsDetachable"] = 1;
            dr["AsDraft"] = 1;
            dr["FileAttachment"] = actual + "|" + original;
            dtAttachments.Rows.Add(dr);

            ViewState["BidTenderAttachments"] = dtAttachments;
            gvFileAttachment.DataSource = dtAttachments;
            gvFileAttachment.DataBind();
        }

        private void Remove(int id)
        {
            DataTable dtAttachments = (DataTable)ViewState["BidTenderAttachments"];
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

            ViewState["BidTenderAttachments"] = dtAttachments;

            gvFileAttachment.DataSource = dtAttachments;
            gvFileAttachment.DataBind();
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
                                        string actual = FileUploadHelper.GetNewAlternativeFileName(Constant.FILEATTACHMENTSFOLDERDIR + Session[Constant.SESSION_USERID.ToString()].ToString() + "\\" + Session[Constant.SESSION_BIDREFNO.ToString()].ToString() + "\\", Session[Constant.SESSION_USERID.ToString()].ToString() + "_" + Session[Constant.SESSION_BIDREFNO.ToString()].ToString() + "_", fInfo.Extension);

                                        try
                                        {
                                            if (!Directory.Exists((Constant.FILEATTACHMENTSFOLDERDIR) + "\\" + Session[Constant.SESSION_USERID.ToString()].ToString() + "\\" + Session[Constant.SESSION_BIDREFNO.ToString()]))
                                            {
                                                Directory.CreateDirectory((Constant.FILEATTACHMENTSFOLDERDIR) + "\\" + Session[Constant.SESSION_USERID.ToString()].ToString() + "\\" + Session[Constant.SESSION_BIDREFNO.ToString()]);
                                                fu.SaveAs((Constant.FILEATTACHMENTSFOLDERDIR) + "\\" + Session[Constant.SESSION_USERID.ToString()].ToString() + "\\" + Session[Constant.SESSION_BIDREFNO.ToString()].ToString() + "\\" + actual);
                                                Attach(original, actual);
                                            }
                                            else
                                            {
                                                fu.SaveAs((Constant.FILEATTACHMENTSFOLDERDIR) + "\\" + Session[Constant.SESSION_USERID.ToString()].ToString() + "\\" + Session[Constant.SESSION_BIDREFNO.ToString()].ToString() + "\\" + actual);
                                                Attach(original, actual);

                                            }
                                        }
                                        catch
                                        {
                                            litMsg.Text = BR + "&nbsp;&nbsp;&nbsp;" + BULLET + " File cannot be uploaded.";
                                        }
                                    }
                                    else
                                        litMsg.Text = BR + "&nbsp;&nbsp;&nbsp;" + BULLET + " File size exceeds limit(" + maxFileSize + "KB).";
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
                case "Download":
                    {
                        string[] args = e.CommandArgument.ToString().Split(new char[] { '|' });
                        string path = Constant.FILEATTACHMENTSFOLDERDIR + Session[Constant.SESSION_USERID.ToString()].ToString() + "\\" + Session[Constant.SESSION_BIDREFNO.ToString()].ToString();
                        FileHelper.DownloadFile(this.Page, path, args[0], args[1]);
                    } break;
            }
        }

        protected void gvFileAttachment_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //if (e.Row.RowType == DataControlRowType.Footer)
            //{
            //    FileUpload fu = (FileUpload)e.Row.FindControl("fileUpload");
            //    fu.Attributes.Add("onchange", "CheckFileSize('" + gvFileAttachment.ClientID + "');");
            //}
        }
        #endregion

        protected void cvCheckBidTender_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (isAllowSubmit())
            {
                // check if there is a tender to be submitted/drafted
                DataTable dtBidTenders = (DataTable)ViewState["BidTenders"];
                if (dtBidTenders.Rows.Count > 0)
                {
                    int ctr = 0;
                    foreach (DataRow dr in dtBidTenders.Rows)
                    {
                        if (double.Parse(dr["Amount"].ToString()) > 0)
                            ctr++;
                    }
                    // if ctr > 0, then there is a bid tender.. :)
                    ViewState["BIDTENDERCOUNT"] = ctr;
                    args.IsValid = ctr > 0;
                }
                else
                    args.IsValid = false;
            }
        }

        protected bool isConverted(object item_status)
        {
            return (
                (int.Parse(item_status.ToString()) == 2) ||
                (int.Parse(item_status.ToString()) == 4)
                );

        }

        protected bool isWithdrawn(object item_status)
        {
            return (
                (int.Parse(item_status.ToString()) == 2) ||
                (int.Parse(item_status.ToString()) == 4)
                );

        }

        private string GetSubmissionDeadline()
        {
            SqlParameter[] sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@BidRefNo", SqlDbType.Int);
            sqlParams[0].Value = int.Parse(Session[Constant.SESSION_BIDREFNO].ToString());

            return Convert.ToString(SqlHelper.ExecuteScalar(connstring, CommandType.StoredProcedure, "sp_GetBidEventSubmissionDeadline", sqlParams));
        }

        protected bool isAllowSubmit()
        {
            if ((GetSubmissionDeadline().ToString() != ""))
            {
                DateTime sdate = DateTime.Parse(GetSubmissionDeadline().ToString());
                DateTime dtnow = DateTime.Now;

                if (DateTime.Compare(sdate, dtnow) > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

    }
}