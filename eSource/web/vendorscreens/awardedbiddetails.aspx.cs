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
using EBid.ConnectionString;

namespace EBid.web.vendor_screens
{
    public partial class AwardedBidDetails : System.Web.UI.Page
	{
	
    private string connstring = "";

		protected void Page_Load(object sender, System.EventArgs e)
		{
            FormsAuthenticationHelper.AuthenticateUserWithReturnUrl();
            connstring = Client.ConnectionString(HttpContext.Current.Session["clientid"].ToString());
            if (Int32.Parse(Session[Constant.SESSION_USERTYPE].ToString().Trim()) != (int)Constant.USERTYPE.VENDOR)
                Response.Redirect("../unauthorizedaccess.aspx");

            if (!(IsPostBack))
            {
                if (Session[Constant.SESSION_USERID] != null)
                {
                    ViewState[Constant.SESSION_USERID] = Session[Constant.SESSION_USERID].ToString().Trim();
                    // Get bid general details
                    if (Session[Constant.SESSION_BIDREFNO] != null)
                        ViewState[Constant.SESSION_BIDREFNO] = Session[Constant.SESSION_BIDREFNO].ToString().Trim();

                    CategoryTransaction catTrans = new CategoryTransaction();

                    BidItem bidItem = BidItemTransaction.GetBidDetailsByRefNo(ViewState[Constant.SESSION_BIDREFNO].ToString().Trim());

                    lblPRNumber.Text = bidItem.PRRefNo.ToString();
                    lblBidReferenceNumber.Text = bidItem.BidRefNo.ToString();
                    lblCategory.Text = catTrans.GetCategoryByID(bidItem.Category).CategoryName;
                    lblBidSubmissionDeadline.Text = bidItem.Deadline;
                    lblDeliveryDate.Text = bidItem.DeliveryDate;
                    lblBidItemDescription.Text = bidItem.ItemDescription;
                    ShowBidTenders();
                }                
            }
		}


        private void ShowBidTenders()
        {

            #region Code for preparing the DataTable
            BidTenderTransaction bt = new BidTenderTransaction();
            DataTable dtTenders = bt.GetAwardedBidTenders(connstring, ViewState[Constant.SESSION_BIDREFNO].ToString().Trim(), ViewState[Constant.SESSION_USERID].ToString().Trim());
            //Session["Mode_BidTenders"] = ((count == 0) ? "Add" : "Edit");

            //Create an instance of DataTable
            DataTable dt = new DataTable();
            DataColumn dcol = new DataColumn(ID, typeof(System.String));
            dcol = new DataColumn("ITEM", typeof(System.String));
            dt.Columns.Add(dcol);
            dcol = new DataColumn("AMOUNT", typeof(System.String));
            dt.Columns.Add(dcol);
            dcol = new DataColumn("DATECREATED", typeof(System.String));
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
                drow["DATECREATED"] = "DATE|" + dtTenders.Rows[i]["DATECREATEDMONTH"].ToString().Trim() + "/" + dtTenders.Rows[i]["DATECREATEDDAY"].ToString().Trim() + "/" + dtTenders.Rows[i]["DATECREATEDYEAR"].ToString().Trim();
                drow["BIDTENDERNO"] = dtTenders.Rows[i]["BIDTENDERNO"].ToString().Trim();
                drow["BIDDETAILNO"] = dtTenders.Rows[i]["BIDDETAILNO"].ToString().Trim();
                dt.Rows.Add(drow);
            }

            IOClass IO = new IOClass();
            IO.WriteIndexToFile(dtTenders.Rows.Count.ToString().Trim(), Session[Constant.SESSION_USERID].ToString().Trim());

            BidTenderGeneral bidtender = new BidTenderGeneral();
            bidtender = bt.QueryBidTendersGeneral(ViewState[Constant.SESSION_BIDREFNO].ToString().Trim(), ViewState[Constant.SESSION_USERID].ToString().Trim());
            Session["Mode_BidTendersGeneral"] = bidtender.Mode_BidTendersGeneral.ToString().Trim();

            DataRow drow1 = dt.NewRow();
            drow1["ITEM"] = "Discount";
            drow1["AMOUNT"] = "Discount|" + bidtender.Discount.ToString().Trim();
            drow1["DATECREATED"] = "Discount|NONE";
            dt.Rows.Add(drow1);
            DataRow drow2 = dt.NewRow();
            drow2["ITEM"] = "Total Cost";
            drow2["AMOUNT"] = "Total Cost|" + bidtender.TotalCost.ToString().Trim();
            drow2["DATECREATED"] = "Total Cost|NONE";
            dt.Rows.Add(drow2);
            DataRow drow3 = dt.NewRow();
            drow3["ITEM"] = "Delivery Cost";
            drow3["AMOUNT"] = "Delivery Cost|" + bidtender.DeliveryCost.ToString().Trim();
            drow3["DATECREATED"] = "Delivery Cost|NONE";
            dt.Rows.Add(drow3);
            DataRow drow4 = dt.NewRow();
            drow4["ITEM"] = "Total Extended Cost";
            drow4["AMOUNT"] = "Total Extended Cost|" + bidtender.TotalExtendedCost.ToString().Trim();
            drow4["DATECREATED"] = "Total Extended Cost|NONE";
            dt.Rows.Add(drow4);
            DataRow drow5 = dt.NewRow();
            drow5["ITEM"] = "Incoterm";
            drow5["AMOUNT"] = "Incoterm|" + bidtender.Incoterm.ToString().Trim();
            drow5["DATECREATED"] = "Incoterm|NONE";
            dt.Rows.Add(drow5);
            DataRow drow6 = dt.NewRow();
            drow6["ITEM"] = "Payment Terms";
            drow6["AMOUNT"] = "Payment Terms|" + bidtender.PaymentTerms.ToString().Trim();
            drow6["DATECREATED"] = "Payment Terms|NONE";
            dt.Rows.Add(drow6);
            DataRow drow7 = dt.NewRow();
            drow7["ITEM"] = "Warranty";
            drow7["AMOUNT"] = "Warranty|" + bidtender.Warranty.ToString().Trim();
            drow7["DATECREATED"] = "Warranty|NONE";
            dt.Rows.Add(drow7);
            DataRow drow8 = dt.NewRow();
            drow8["ITEM"] = "Remarks";
            drow8["AMOUNT"] = "Remarks|" + bidtender.Remarks.ToString().Trim();
            drow8["DATECREATED"] = "Remarks|NONE";
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
                        bfield.VendorId = Session[Constant.SESSION_USERID].ToString().Trim();
                        bfield.Editable = false;
                        bfield.Date = false;
                        bfield.Label2 = false;
                        bfield.Label4 = true;
                        bfield.LabelDate = false;
                        break;
                    case "AMOUNT":
                        //Initialize the HeaderText field value.
                        bfield.HeaderText = "Bids";
                        bfield.HeaderType = Constant.HEADER_TYPE_LABEL.ToString().Trim();
                        bfield.DataField = "AMOUNT";
                        bfield.ItemStyle.CssClass = "valueGridItem";
                        bfield.VendorId = Session[Constant.SESSION_USERID].ToString().Trim();
                        bfield.Editable = false;
                        bfield.Date = false;
                        bfield.Label2 = true;
                        bfield.Label4 = false;
                        bfield.LabelDate = false;
                    break;
                    case "DATECREATED":
                        //Initialize the HeaderText field value.
                        bfield.HeaderText = "Delivery Date";
                        bfield.HeaderType = Constant.HEADER_TYPE_LABEL.ToString().Trim();
                        bfield.ItemStyle.CssClass = "valueGridItem";
                        bfield.DataField = "DATECREATED";
                        bfield.VendorId = Session[Constant.SESSION_USERID].ToString().Trim();
                        bfield.Editable = false;
                        bfield.Date = false;
                        bfield.Label2 = false;
                        bfield.Label4 = false;
                        bfield.LabelDate = true;
                        break;
                    case "BIDTENDERNO":
                        //Initialize the HeaderText field value.
                        bfield.HeaderText = col.ColumnName;
                        bfield.Visible = false;
                        bfield.HeaderType = Constant.HEADER_TYPE_LABEL.ToString().Trim();
                        bfield.DataField = "BIDTENDERNO";
                        bfield.Editable = false;
                        bfield.Date = false;
                        break;
                    case "BIDDETAILNO":
                        //Initialize the HeaderText field value.
                        bfield.HeaderText = col.ColumnName;
                        bfield.Visible = false;
                        bfield.HeaderType = Constant.HEADER_TYPE_LABEL.ToString().Trim();
                        bfield.DataField = "BIDDETAILNO";
                        bfield.Editable = false;
                        bfield.Date = false;
                        break;
                }

                //Add the newly created bound field to the GridView.
                gvBids.Columns.Add(bfield);
            }


            gvBids.DataSource = dt;
            gvBids.DataBind();
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

		protected void btnOk_Click(object sender, EventArgs e)
		{
			Response.Redirect("awardedbids.aspx");
		}
}
}
