using System;
using System.Collections;
using System.ComponentModel;
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
using EBid.lib.utils;
using System.IO;
using EBid.ConnectionString;

namespace EBid.web.vendor_screens
{
    public partial class BidTenderRenegotiateSubmit : System.Web.UI.Page
    {
        private string connstring = "";
        protected void Page_Load(object sender, System.EventArgs e)
        {
            FormsAuthenticationHelper.AuthenticateUserWithReturnUrl();
            connstring = Client.ConnectionString(HttpContext.Current.Session["clientid"].ToString());
            if (Int32.Parse(Session[Constant.SESSION_USERTYPE].ToString().Trim()) != (int)Constant.USERTYPE.VENDOR)
                Response.Redirect("../unauthorizedaccess.aspx");

            if (!(Page.IsPostBack))
            {
                if (Session[Constant.SESSION_USERID] != null)
                {
                    //bdy.Attributes.Add("onLoad", "GetAllAmounts()");
                    FillDropDownList();
                    AssignGlobalVariables();
                    ShowBidInfo();
                    ShowBidItems();
                    ShowBidTenders();
                }                
            }
        }

        private void AssignGlobalVariables()
        {
            if (Session[Constant.SESSION_USERID] != null)
                ViewState[Constant.SESSION_USERID] = Session[Constant.SESSION_USERID].ToString().Trim();
            if (Session[Constant.SESSION_BIDREFNO] != null)
                ViewState[Constant.SESSION_BIDREFNO] = Session[Constant.SESSION_BIDREFNO].ToString().Trim();
        }

        private void FillDropDownList()
        {
            OtherTransaction oth = new OtherTransaction();
            ddlPreferredCurrency.DataSource = oth.GetCurrency();
            ddlPreferredCurrency.DataValueField = "Value";
            ddlPreferredCurrency.DataTextField = "Text";
            ddlPreferredCurrency.DataBind();

        }

        private void ShowBidInfo()
        {
            CategoryTransaction catTrans = new CategoryTransaction();
            BidItem bid = new BidItem();

            bid = BidItemTransaction.QueryFactsAboutABidItemByBidRefNo(ViewState[Constant.SESSION_BIDREFNO].ToString().Trim());
            lblBidReferenceNumber.Text = bid.BidRefNo.ToString();
            lblCategory.Text = ((Category)catTrans.GetCategoryByID(bid.Category)).CategoryName;
            lblBidSubmissionDeadline.Text = bid.Deadline;
            lblDeliveryDate.Text = bid.DeliveryDate;
            lblDeliverTo.Text = bid.DeliverTo;

        }

        private void ShowBidItems()
        {
            BidItemDetailTransaction biditemdetail = new BidItemDetailTransaction();
            gvBidDetails.DataSource = biditemdetail.GetBidItemDetails(connstring, ViewState[Constant.SESSION_BIDREFNO].ToString().Trim());
            gvBidDetails.DataBind();

        }

        private void ShowBidTenders()
        {
            #region Code for preparing the DataTable
            BidTenderTransaction bt = new BidTenderTransaction();
            int count = 0;
            DataTable dtTenders = bt.GetBidTenders(connstring, ViewState[Constant.SESSION_BIDREFNO].ToString().Trim(), ViewState[Constant.SESSION_USERID].ToString().Trim(), ref count);


            //Create an instance of DataTable
            DataTable dt = new DataTable();
            DataColumn dcol = new DataColumn(ID, typeof(System.String));
            dcol = new DataColumn("ITEM", typeof(System.String));
            dt.Columns.Add(dcol);
            dcol = new DataColumn("AMOUNT", typeof(System.String));
            dt.Columns.Add(dcol);
            dcol = new DataColumn("TENDERDATE", typeof(System.String));
            dt.Columns.Add(dcol);
            dcol = new DataColumn("BIDTENDERNO", typeof(System.String));
            dt.Columns.Add(dcol);
            dcol = new DataColumn("BIDDETAILNO", typeof(System.String));
            dt.Columns.Add(dcol);

            //Now add data for dynamic columns
            for (int i = 0; i < dtTenders.Rows.Count; i++)
            {
                //Create a new row
                DataRow drow = dt.NewRow();
                drow["ITEM"] = dtTenders.Rows[i]["ITEM"].ToString().Trim();
                drow["AMOUNT"] = ((i == dtTenders.Rows.Count - 1) ? "Last|" + dtTenders.Rows[i]["AMOUNT"].ToString().Trim() : dtTenders.Rows[i]["AMOUNT"].ToString().Trim());
                drow["TENDERDATE"] = dtTenders.Rows[i]["TENDERDATEMONTH"].ToString().Trim() + "/" + dtTenders.Rows[i]["TENDERDATEDAY"].ToString().Trim() + "/" + dtTenders.Rows[i]["TENDERDATEYEAR"].ToString().Trim();
                drow["BIDTENDERNO"] = dtTenders.Rows[i]["BIDTENDERNO"].ToString().Trim();
                drow["BIDDETAILNO"] = dtTenders.Rows[i]["BIDDETAILNO"].ToString().Trim();
                dt.Rows.Add(drow);
            }

            IOClass IO = new IOClass();
            IO.WriteIndexToFile(dtTenders.Rows.Count.ToString().Trim(), ViewState[Constant.SESSION_USERID].ToString().Trim());

            BidTenderGeneral bidtender = new BidTenderGeneral();
            bidtender = bt.QueryBidTendersGeneral(ViewState[Constant.SESSION_BIDREFNO].ToString().Trim(), ViewState[Constant.SESSION_USERID].ToString().Trim());
            Session["Mode_BidTendersGeneral"] = bidtender.Mode_BidTendersGeneral.ToString().Trim();

            ddlPreferredCurrency.SelectedIndex = ddlPreferredCurrency.Items.IndexOf(ddlPreferredCurrency.Items.FindByValue(bidtender.Currency.ToString().Trim()));
            DataRow drow1 = dt.NewRow();
            drow1["ITEM"] = "Discount";
            drow1["AMOUNT"] = "Discount|" + bidtender.Discount.ToString().Trim();
            drow1["TENDERDATE"] = "Discount|NONE";
            dt.Rows.Add(drow1);
            DataRow drow2 = dt.NewRow();
            drow2["ITEM"] = "Total Cost";
            drow2["AMOUNT"] = "Total Cost|" + bidtender.TotalCost.ToString().Trim();
            drow2["TENDERDATE"] = "Total Cost|NONE";
            dt.Rows.Add(drow2);
            DataRow drow3 = dt.NewRow();
            drow3["ITEM"] = "Delivery Cost";
            drow3["AMOUNT"] = "Delivery Cost|" + bidtender.DeliveryCost.ToString().Trim();
            drow3["TENDERDATE"] = "Delivery Cost|NONE";
            dt.Rows.Add(drow3);
            DataRow drow4 = dt.NewRow();
            drow4["ITEM"] = "Total Extended Cost";
            drow4["AMOUNT"] = "Total Extended Cost|" + bidtender.TotalExtendedCost.ToString().Trim();
            drow4["TENDERDATE"] = "Total Extended Cost|NONE";
            dt.Rows.Add(drow4);
            DataRow drow5 = dt.NewRow();
            drow5["ITEM"] = "Incoterm";
            drow5["AMOUNT"] = "Incoterm|" + bidtender.Incoterm.ToString().Trim();
            drow5["TENDERDATE"] = "Incoterm|NONE";
            dt.Rows.Add(drow5);
            DataRow drow6 = dt.NewRow();
            drow6["ITEM"] = "Payment Terms";
            drow6["AMOUNT"] = "Payment Terms|" + bidtender.PaymentTerms.ToString().Trim();
            drow6["TENDERDATE"] = "Payment Terms|NONE";
            dt.Rows.Add(drow6);
            DataRow drow7 = dt.NewRow();
            drow7["ITEM"] = "Warranty";
            drow7["AMOUNT"] = "Warranty|" + bidtender.Warranty.ToString().Trim();
            drow7["TENDERDATE"] = "Warranty|NONE";
            dt.Rows.Add(drow7);
            DataRow drow8 = dt.NewRow();
            drow8["ITEM"] = "Remarks";
            drow8["AMOUNT"] = "Remarks|" + bidtender.Remarks.ToString().Trim();
            drow8["TENDERDATE"] = "Remarks|NONE";
            dt.Rows.Add(drow8);

            #endregion

            //Iterate through the columns of the datatable to set the data bound field dynamically.
            foreach (DataColumn col in dt.Columns)
            {
                //Declare the bound field and allocate memory for the bound field.
                CustomBoundField bfield = new CustomBoundField();
                //Initalize the DataField value.
                bfield.DataField = col.ColumnName;
                bfield.HeaderStyle.CssClass = "itemDetails_th";
                switch (col.ColumnName)
                {
                    case "ITEM":
                        //Initialize the HeaderText field value.
                        bfield.HeaderText = "Item";
                        bfield.HeaderType = Constant.HEADER_TYPE_LABEL.ToString().Trim();
                        bfield.ItemStyle.CssClass = "itemDetails_td";
                        bfield.DataField = "ITEM";
                        bfield.Editable = false;
                        bfield.Date = false;
                        bfield.Label4 = true;
                        bfield.VendorId = ViewState[Constant.SESSION_USERID].ToString().Trim();
                        break;
                    case "AMOUNT":
                        //Initialize the HeaderText field value.
                        bfield.HeaderText = "Item Cost";
                        bfield.HeaderType = Constant.HEADER_TYPE_LABEL.ToString().Trim();
                        bfield.DataField = "AMOUNT";
                        bfield.VendorId = ViewState[Constant.SESSION_USERID].ToString().Trim();
                        bfield.ItemStyle.CssClass = "valueGridItem";
                        bfield.Editable = true;
                        break;
                    case "TENDERDATE":
                        //Initialize the HeaderText field value.
                        bfield.HeaderText = "Delivery Date";
                        bfield.HeaderType = Constant.HEADER_TYPE_LABEL.ToString().Trim();
                        bfield.ItemStyle.CssClass = "valueGridItem";
                        bfield.DataField = "TENDERDATE";
                        bfield.VendorId = ViewState[Constant.SESSION_USERID].ToString().Trim();
                        bfield.Editable = false;
                        bfield.Label4 = false;
                        bfield.Date = true;
                        break;
                    case "BIDTENDERNO":
                        //Initialize the HeaderText field value.
                        bfield.HeaderText = col.ColumnName;
                        bfield.Visible = false;
                        bfield.HeaderType = Constant.HEADER_TYPE_LABEL.ToString().Trim();
                        bfield.DataField = "BIDTENDERNO";
                        bfield.Editable = false;
                        bfield.Date = false;
                        bfield.Label4 = false;
                        break;
                    case "BIDDETAILNO":
                        //Initialize the HeaderText field value.
                        bfield.HeaderText = col.ColumnName;
                        bfield.Visible = false;
                        bfield.HeaderType = Constant.HEADER_TYPE_LABEL.ToString().Trim();
                        bfield.DataField = "BIDDETAILNO";
                        bfield.Editable = false;
                        bfield.Date = false;
                        bfield.Label4 = false;
                        break;
                }

                //Add the newly created bound field to the GridView.
                gvBidDetails2.Columns.Add(bfield);
            }

            gvBidDetails2.DataSource = dt;
            gvBidDetails2.DataBind();
        }

        #region Web Form Designer generated code
        override protected void OnInit(EventArgs e)
        {
            //
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            //
            InitializeComponent();
            base.OnInit(e);
        }

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {

        }
        #endregion

        private string SaveTenderToDataBase(string vBidDetailNo, string vAmount, string vTenderDate, string vVendorId, string vBidTenderNo, string vTotalItemCost)
        {
            BidTenderTransaction bt = new BidTenderTransaction();
            if (vBidTenderNo == "")
            {
                //vBidTenderNo = bt.InsertBidTenderToDataBase(connstring, vBidDetailNo, vAmount, vTenderDate, vVendorId, vTotalItemCost);
            }
            else
            {
                bt.UpdateBidTenderToDataBase(vBidDetailNo, vAmount, vTenderDate, vVendorId, vTotalItemCost);
                bt.UpdateBidTenderStatus(vBidTenderNo, Constant.BID_TENDER_STATUS_SUBMITTED);
            }
            return vBidTenderNo;
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("biddetailsrenegotiate.aspx");
        }


        private void SaveBidTender(string Status)
        {
            #region GetBidTenderCount
            IOClass IO = new IOClass();
            int line = IO.GetTenderCount(ViewState[Constant.SESSION_USERID].ToString().Trim());
            int i = 0;
            #endregion
            #region Declare Variables

            double dblTotalAmount = 0;
            double dblTotalCost = 0;
            double dblTotalExtendedCost = 0;
            string vTotalCost = "";
            string vTotalExtendedCost = "";
            string vAmount = "";
            string vDiscount = "";
            string vDeliveryCost = "";
            string vIncoterm = "";
            string vPayment = "";
            string vWarranty = "";
            string vRemarks = "";

            #endregion
            #region GetValuesFromRowsAndSave

            foreach (GridViewRow gvr in gvBidDetails2.Rows)
            {
                if (i < line)
                {
                    vAmount = ((TextBox)gvr.Cells[1].Controls[0]).Text;
                    if (vAmount != "")
                    {
                        dblTotalAmount = dblTotalAmount + Convert.ToDouble(vAmount);
                    }
                    string vDay = ((TextBox)gvr.Cells[2].Controls[1]).Text;
                    string vMonth = ((DropDownList)gvr.Cells[2].Controls[0]).SelectedItem.Value;
                    string vYear = ((TextBox)gvr.Cells[2].Controls[2]).Text;
                    string vBidTenderNo = ((Label)gvr.Cells[3].Controls[0]).Text;
                    string vBidDetailNo = ((Label)gvr.Cells[4].Controls[0]).Text;
                    //vBidTenderNo = SaveTenderToDataBase(vBidDetailNo, vAmount, vMonth + '/' + vDay + '/' + vYear, ViewState[Constant.SESSION_USERID].ToString().Trim(), vBidTenderNo, "0");
                }
                else
                {
                    switch (i - line)
                    {
                        case 0:
                            vDiscount = ((TextBox)gvr.Cells[1].Controls[1]).Text;
                            if (vDiscount != "")
                                dblTotalCost = dblTotalAmount - Convert.ToDouble(vDiscount);
                            else
                                dblTotalCost = dblTotalAmount;
                            break;
                        case 1:
                            vTotalCost = ((TextBox)gvr.Cells[1].Controls[0]).Text;
                            vTotalCost = dblTotalCost.ToString().Trim();
                            break;
                        case 2:
                            vDeliveryCost = ((TextBox)gvr.Cells[1].Controls[0]).Text;
                            if (vDeliveryCost != "")
                                dblTotalExtendedCost = dblTotalCost + Convert.ToDouble(vDeliveryCost);
                            else
                                dblTotalExtendedCost = dblTotalCost;

                            break;
                        case 3:
                            vTotalExtendedCost = ((TextBox)gvr.Cells[1].Controls[0]).Text;
                            vTotalExtendedCost = dblTotalExtendedCost.ToString().Trim();
                            break;
                        case 4:
                            vIncoterm = ((DropDownList)gvr.Cells[1].Controls[0]).SelectedItem.Value;
                            break;
                        case 5:
                            vPayment = ((TextBox)gvr.Cells[1].Controls[0]).Text;
                            break;
                        case 6:
                            vWarranty = ((TextBox)gvr.Cells[1].Controls[0]).Text;
                            break;
                        case 7:
                            vRemarks = ((TextBox)gvr.Cells[1].Controls[0]).Text;
                            break;
                    }
                }
                i++;
            }

            BidTenderTransaction bt = new BidTenderTransaction();
            if (Session["Mode_BidTendersGeneral"].ToString().Trim() != "Edit")
            {
                bt.InsertBidTenderGeneralToDataBase(connstring, ViewState["BidRefNo"].ToString().Trim(), ViewState[Constant.SESSION_USERID].ToString().Trim(), ddlPreferredCurrency.SelectedItem.Value, vDiscount, vTotalCost, vDeliveryCost, vTotalExtendedCost, vIncoterm, vPayment, vWarranty, vRemarks, Status);
                Session["Mode_BidTendersGeneral"] = "Edit";
            }
            else
            {
                bt.UpdateBidTenderGeneralToDataBase(ViewState["BidRefNo"].ToString().Trim(), ViewState[Constant.SESSION_USERID].ToString().Trim(), ddlPreferredCurrency.SelectedItem.Value, vDiscount, vTotalCost, vDeliveryCost, vTotalExtendedCost, vIncoterm, vPayment, vWarranty, vRemarks, Status);
            }
            //SaveComment();
            #endregion
        }

        protected void btnDraft_Click(object sender, EventArgs e)
        {
            SaveBidTender(Constant.BID_TENDER_STATUS_DRAFT.ToString().Trim());
            Session[Constant.SESSION_BIDREFNO] = ViewState[Constant.SESSION_BIDREFNO].ToString().Trim();
            Session[Constant.SESSION_USERID] = ViewState[Constant.SESSION_USERID].ToString().Trim();
            Response.Redirect("biddetailsrenegotiate.aspx");
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            SaveBidTender(Constant.BID_TENDER_STATUS_SUBMITTED.ToString().Trim());
            ComputeIfBidTenderIs2MAbove();
            Session[Constant.SESSION_BIDREFNO] = ViewState[Constant.SESSION_BIDREFNO].ToString().Trim();
            Session[Constant.SESSION_USERID] = ViewState[Constant.SESSION_USERID].ToString().Trim();
            Response.Redirect("bidconfirmation.aspx");
        }


        private void ComputeIfBidTenderIs2MAbove()
        {
            BidTenderTransaction bt = new BidTenderTransaction();
            bt.MarkIf2MAbove(connstring, ViewState[Constant.SESSION_BIDREFNO].ToString().Trim(), ViewState[Constant.SESSION_USERID].ToString().Trim());
        }
    }
}
