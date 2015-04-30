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
using EBid.lib.bid.data;
using EBid.lib.bid.trans;
using EBid.lib.user.trans;
using EBid.lib.constant;
using EBid.lib;

public partial class web_purchasing_screens_ReceivedTenders : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        FormsAuthenticationHelper.AuthenticateUserWithReturnUrl();
        if (Int32.Parse(Session[Constant.SESSION_USERTYPE].ToString().Trim()) != (int)Constant.USERTYPE.PURCHASING)
            Response.Redirect("../unauthorizedaccess.aspx");

        //if (!IsPostBack)
        //{
        //    if (Session[Constant.SESSION_USERID] != null)
        //    {
        //        if (Session[Constant.SESSION_BIDREFNO] != null)
        //        {
        //            ViewState[Constant.SESSION_BIDREFNO] = Session[Constant.SESSION_BIDREFNO].ToString().Trim();
        //            lblBidRefNo.Text = ViewState[Constant.SESSION_BIDREFNO].ToString().Trim();
        //            BidItemTransaction biditem = new BidItemTransaction();
        //            DataTable dt = new DataTable();

        //            DataColumn dcol = new DataColumn("Item", typeof(System.String));
        //            dt.Columns.Add(dcol);

        //            SupplierTransaction sup = new SupplierTransaction();
        //            DataTable dtvendors = new DataTable();
        //            //dtvendors = sup.GetVendorsWithSubmittedTenders(connstring, Session["BidRefNo"].ToString().Trim());
        //            dtvendors = sup.GetVendorsWithEndorsedTenders(connstring, ViewState[Constant.SESSION_BIDREFNO].ToString().Trim());

        //            for (int j = 0; j < dtvendors.Rows.Count; j++)
        //            {

        //                string strSupplierName = sup.GetVendorNameById(dtvendors.Rows[j]["vendorid"].ToString().Trim());
        //                dcol = new DataColumn(strSupplierName, typeof(System.String));
        //                dt.Columns.Add(dcol);
        //                dcol = new DataColumn("BidTenderNo" + j.ToString().Trim(), typeof(System.String));
        //                dt.Columns.Add(dcol);
        //            }

        //            //---create rows
        //            BidTenderTransaction bidtender = new BidTenderTransaction();
        //            BidItemDetailTransaction biditemdetail = new BidItemDetailTransaction();
        //            DataTable dtitems = biditemdetail.QueryBidItemDetail_Items_ByBidRefNo(connstring, ViewState[Constant.SESSION_BIDREFNO].ToString().Trim());
        //            //DataRow ItemRow = dt.NewRow();
        //            //write biditems and amount
        //            for (int i = 0; i < dtitems.Rows.Count; i++)
        //            {
        //                DataRow ItemRow = dt.NewRow();
        //                ItemRow["Item"] = dtitems.Rows[i]["Item"].ToString().Trim();
        //                for (int j = 0; j < dtvendors.Rows.Count; j++)
        //                {
        //                    BidTender bt = new BidTender();
        //                    bt = bidtender.QueryBidTenderAmount(dtitems.Rows[i]["BidDetailNo"].ToString().Trim(), dtvendors.Rows[j]["vendorid"].ToString().Trim());
        //                    double dblAmount = bt.Amount;
        //                    string strAmount = dblAmount.ToString("N2");
        //                    string strSupplierName = sup.GetVendorNameById(dtvendors.Rows[j]["vendorid"].ToString().Trim());
        //                    //CheckBox chk = new CheckBox();
        //                    //chk.Text = j.ToString().Trim() + "|Item" + "|true" + "|" + bt.BidTenderNo;
        //                    //ItemRow["Checkbox|" + j.ToString().Trim()] = chk;
        //                    if (j == dtvendors.Rows.Count - 1)
        //                    {
        //                        ItemRow[strSupplierName] = "Last|" + strAmount;
        //                    }
        //                    else
        //                    {
        //                        ItemRow[strSupplierName] = "Item|" + strAmount;
        //                    }
        //                    ItemRow["BidTenderNo" + j.ToString().Trim()] = bt.BidTenderNo;
        //                }
        //                dt.Rows.Add(ItemRow);
        //            }

        //            //write discount
        //            DataRow DiscountRow = dt.NewRow();
        //            DiscountRow["Item"] = "Discount";
        //            for (int j = 0; j < dtvendors.Rows.Count; j++)
        //            {
        //                double dblDiscount = bidtender.GetDiscount(ViewState[Constant.SESSION_BIDREFNO].ToString().Trim(), dtvendors.Rows[j]["vendorid"].ToString().Trim());
        //                string strDiscount = dblDiscount.ToString("N2");
        //                string strSupplierName = sup.GetVendorNameById(dtvendors.Rows[j]["vendorid"].ToString().Trim());
        //                //CheckBox chk = new CheckBox();
        //                //chk.Text = j.ToString().Trim() + "|Discount" + "|false|";
        //                //DiscountRow["Checkbox|" + j.ToString().Trim()] = chk;
        //                DiscountRow[strSupplierName] = "Discount|" + strDiscount;
        //                DiscountRow["BidTenderNo" + j.ToString().Trim()] = "";
        //            }

        //            dt.Rows.Add(DiscountRow);

        //            //write total cost
        //            DataRow TotalCostRow = dt.NewRow();
        //            TotalCostRow["Item"] = "Total Cost";
        //            for (int j = 0; j < dtvendors.Rows.Count; j++)
        //            {
        //                double dblTotalCost = bidtender.GetTotalCost(ViewState[Constant.SESSION_BIDREFNO].ToString().Trim(), dtvendors.Rows[j]["vendorid"].ToString().Trim());
        //                string strTotalCost = dblTotalCost.ToString("N2");
        //                string strSupplierName = sup.GetVendorNameById(dtvendors.Rows[j]["vendorid"].ToString().Trim());
        //                //CheckBox chk = new CheckBox();
        //                //chk.Text = j.ToString().Trim() + "|TotalCost" + "|false|";
        //                //TotalCostRow["Checkbox|" + j.ToString().Trim()] = chk;
        //                TotalCostRow[strSupplierName] = "TotalCost|" + strTotalCost;
        //                TotalCostRow["BidTenderNo" + j.ToString().Trim()] = "";
        //            }
        //            dt.Rows.Add(TotalCostRow);

        //            //write delivery cost
        //            DataRow DeliveryCostRow = dt.NewRow();
        //            DeliveryCostRow["Item"] = "Delivery Cost";
        //            for (int j = 0; j < dtvendors.Rows.Count; j++)
        //            {
        //                double dblDeliveryCost = bidtender.GetDeliveryCost(ViewState[Constant.SESSION_BIDREFNO].ToString().Trim(), dtvendors.Rows[j]["vendorid"].ToString().Trim());
        //                string strDeliveryCost = dblDeliveryCost.ToString("N2");
        //                string strSupplierName = sup.GetVendorNameById(dtvendors.Rows[j]["vendorid"].ToString().Trim());
        //                //CheckBox chk = new CheckBox();
        //                //chk.Text = j.ToString().Trim() + "|DeliveryCost" + "|false|";
        //                //DeliveryCostRow["Checkbox|" + j.ToString().Trim()] = chk;
        //                DeliveryCostRow[strSupplierName] = "DeliveryCost|" + strDeliveryCost;
        //                DeliveryCostRow["BidTenderNo" + j.ToString().Trim()] = "";
        //            }
        //            dt.Rows.Add(DeliveryCostRow);

        //            //write total extended cost
        //            DataRow TotalExtendedCostRow = dt.NewRow();
        //            TotalExtendedCostRow["Item"] = "Total Extended Cost";
        //            for (int j = 0; j < dtvendors.Rows.Count; j++)
        //            {
        //                double dblTotalExtendedCost = bidtender.GetTotalExtendedCost(ViewState[Constant.SESSION_BIDREFNO].ToString().Trim(), dtvendors.Rows[j]["vendorid"].ToString().Trim());
        //                string strTotalExtendedCost = dblTotalExtendedCost.ToString("N2");
        //                string strSupplierName = sup.GetVendorNameById(dtvendors.Rows[j]["vendorid"].ToString().Trim());
        //                //CheckBox chk = new CheckBox();
        //                //chk.Text = j.ToString().Trim() + "|TotalExtendedCost" + "|false|";
        //                //TotalExtendedCostRow["Checkbox|" + j.ToString().Trim()] = chk;
        //                TotalExtendedCostRow[strSupplierName] = "TotalExtendedCost|" + strTotalExtendedCost;
        //                TotalExtendedCostRow["BidTenderNo" + j.ToString().Trim()] = "";
        //            }
        //            dt.Rows.Add(TotalExtendedCostRow);

        //            //write incoterm
        //            DataRow IncotermRow = dt.NewRow();
        //            IncotermRow["Item"] = "Incoterm";
        //            for (int j = 0; j < dtvendors.Rows.Count; j++)
        //            {
        //                IncotermTransaction inc = new IncotermTransaction();
        //                string strIncoTerm = inc.GetIncotermName(bidtender.GetIncoterm(connstring, ViewState[Constant.SESSION_BIDREFNO].ToString().Trim(), dtvendors.Rows[j]["vendorid"].ToString().Trim()));
        //                string strSupplierName = sup.GetVendorNameById(dtvendors.Rows[j]["vendorid"].ToString().Trim());
        //                //CheckBox chk = new CheckBox();
        //                //chk.Text = j.ToString().Trim() + "|Incoterm" + "|false|";
        //                //IncotermRow["Checkbox|" + j.ToString().Trim()] = chk;
        //                IncotermRow[strSupplierName] = "Incoterm|" + strIncoTerm;
        //                IncotermRow["BidTenderNo" + j.ToString().Trim()] = "";
        //            }
        //            dt.Rows.Add(IncotermRow);

        //            //write payment terms
        //            DataRow PaymentTermsRow = dt.NewRow();
        //            PaymentTermsRow["Item"] = "Payment Terms";
        //            for (int j = 0; j < dtvendors.Rows.Count; j++)
        //            {
        //                string strPaymentTerms = bidtender.GetPaymentTerms(ViewState[Constant.SESSION_BIDREFNO].ToString().Trim(), dtvendors.Rows[j]["vendorid"].ToString().Trim());
        //                string strSupplierName = sup.GetVendorNameById(dtvendors.Rows[j]["vendorid"].ToString().Trim());
        //                //CheckBox chk = new CheckBox();
        //                //chk.Text = j.ToString().Trim() + "|PaymentTerms" + "|false|";
        //                //PaymentTermsRow["Checkbox|" + j.ToString().Trim()] = chk;
        //                PaymentTermsRow[strSupplierName] = "PaymentTerms|" + strPaymentTerms;
        //                PaymentTermsRow["BidTenderNo" + j.ToString().Trim()] = "";
        //            }
        //            dt.Rows.Add(PaymentTermsRow);

        //            //write warranty
        //            DataRow WarrantyRow = dt.NewRow();
        //            WarrantyRow["Item"] = "Warranty";
        //            for (int j = 0; j < dtvendors.Rows.Count; j++)
        //            {
        //                string strWarranty = bidtender.GetWarranty(ViewState[Constant.SESSION_BIDREFNO].ToString().Trim(), dtvendors.Rows[j]["vendorid"].ToString().Trim());
        //                string strSupplierName = sup.GetVendorNameById(dtvendors.Rows[j]["vendorid"].ToString().Trim());
        //                //CheckBox chk = new CheckBox();
        //                //chk.Text = j.ToString().Trim() + "|Warranty" + "|false|";
        //                //WarrantyRow["Checkbox|" + j.ToString().Trim()] = chk;
        //                WarrantyRow[strSupplierName] = "Warranty|" + strWarranty;
        //                WarrantyRow["BidTenderNo" + j.ToString().Trim()] = "";
        //            }
        //            dt.Rows.Add(WarrantyRow);

        //            //write remarks
        //            DataRow RemarksRow = dt.NewRow();
        //            RemarksRow["Item"] = "Remarks";
        //            for (int j = 0; j < dtvendors.Rows.Count; j++)
        //            {
        //                string strRemarks = bidtender.GetRemarks(ViewState[Constant.SESSION_BIDREFNO].ToString().Trim(), dtvendors.Rows[j]["vendorid"].ToString().Trim());
        //                string strSupplierName = sup.GetVendorNameById(dtvendors.Rows[j]["vendorid"].ToString().Trim());
        //                //CheckBox chk = new CheckBox();
        //                //chk.Text = j.ToString().Trim() + "|Remarks" + "|false|";
        //                //RemarksRow["Checkbox|" + j.ToString().Trim()] = chk;
        //                RemarksRow[strSupplierName] = "Remarks|" + strRemarks;
        //                RemarksRow["BidTenderNo" + j.ToString().Trim()] = "";
        //            }
        //            dt.Rows.Add(RemarksRow);

        //            foreach (DataColumn col in dt.Columns)
        //            {
        //                CustomBoundField bfield1 = new CustomBoundField();
        //                string colname = col.ColumnName.ToUpper().Trim();

        //                if (colname.IndexOf("BIDTENDERNO") > -1)
        //                {
        //                    //Initalize the DataField value.
        //                    bfield1.DataField = col.ColumnName;
        //                    bfield1.ItemStyle.CssClass = "value";
        //                    //Initialize the HeaderText field value.
        //                    bfield1.HeaderText = col.ColumnName;
        //                    bfield1.HeaderType = Constant.HEADER_TYPE_LABEL.ToString().Trim();
        //                    bfield1.HeaderStyle.CssClass = "itemDetails_th";
        //                    bfield1.Editable = false;
        //                    //bfield1.ShowCheckBox = false;
        //                    bfield1.Date = false;
        //                    bfield1.Label3 = false;
        //                    bfield1.Visible = false;
        //                    GridView1.Columns.Add(bfield1);
        //                }
        //                else if (colname.IndexOf("ITEM") > -1)
        //                {
        //                    bfield1.DataField = col.ColumnName;
        //                    bfield1.ItemStyle.CssClass = "itemDetails_td";
        //                    bfield1.Label3 = false;
        //                    bfield1.HeaderText = col.ColumnName;
        //                    bfield1.HeaderType = Constant.HEADER_TYPE_LABEL.ToString().Trim();
        //                    bfield1.HeaderStyle.CssClass = "itemDetails_th";
        //                    bfield1.Editable = false;
        //                    //bfield1.ShowCheckBox = false;
        //                    bfield1.Date = false;
        //                    GridView1.Columns.Add(bfield1);
        //                }
        //                else
        //                {
        //                    bfield1.DataField = col.ColumnName;
        //                    bfield1.ItemStyle.CssClass = "value";
        //                    bfield1.Label3 = true;
        //                    bfield1.HeaderText = col.ColumnName;
        //                    bfield1.HeaderType = Constant.HEADER_TYPE_LABEL.ToString().Trim();
        //                    bfield1.HeaderStyle.CssClass = "itemDetails_th";
        //                    bfield1.Editable = false;
        //                    //bfield1.ShowCheckBox = false;
        //                    bfield1.Date = false;
        //                    GridView1.Columns.Add(bfield1);
        //                }
        //            }

        //            GridView1.DataSource = dt;
        //            GridView1.DataBind();
        //        }
        //    }
           
        //}
    }
}
