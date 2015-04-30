using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using EBid.lib.bid.data;
using EBid.lib.bid.trans;
using EBid.lib.user.trans;
using EBid.lib.user.data;
using EBid.lib.constant;
using EBid.lib;
using EBid.ConnectionString;

namespace EBid.WEB.buyer_screens
{
	/// <summary>
	/// Summary description for DraftBidDetails.
	/// </summary>
	public partial class DraftBidDetails : System.Web.UI.Page
	{

    private string connstring = "";

		private string GetUnitOfMeasurement(string unitid) 
		{
			UnitOfMeasurementTransaction unit = new UnitOfMeasurementTransaction();			
			return unit.GetUnitNameById(connstring, unitid);
		}

		private void Display() 
		{
            BidItem biditem = BidItemTransaction.QueryBidItemInfo(connstring, ViewState["BidRefNo"].ToString().Trim());
            //BidItemInfo biditem = obj.Query("91000");
			BuyerTransaction buyer = new BuyerTransaction();
			CategoryTransaction category = new CategoryTransaction();
			GroupDeptSecTransaction grp = new GroupDeptSecTransaction();
			CompanyTransaction cmp = new CompanyTransaction();
            IncotermTransaction inc = new IncotermTransaction();
            OtherTransaction dte = new OtherTransaction();

			lblCompany.Text = cmp.GetCompanyName(connstring, biditem.CompanyId.ToString().Trim());
			lblRequestorName.Text = biditem.Requestor.ToString().Trim();
			lblPRNumber.Text = biditem.PRRefNo.ToString().Trim();
            lblPRDate.Text = biditem.PRDate.ToString().Trim();
			lblGroupDeptSec.Text = grp.GetGroupDeptSecNameById(connstring, biditem.GroupDeptSec.ToString().Trim());
            lblBidRefNum.Text = biditem.BidRefNo.ToString().Trim();
			lblSubCategory.Text = category.GetCategoryNameById(connstring, biditem.Category.ToString().Trim());
			lblBidSubmissionDeadline.Text = biditem.Deadline.ToString().Trim();
			lblBidItemDescription.Text = biditem.ItemDescription;
            lblDeliverTo.Text = biditem.DeliverTo;
            lblIncoterm.Text = inc.GetIncotermName(connstring, biditem.Incoterm);

            DataTable dtSuppliers = new DataTable();
            DataColumn dcol = new DataColumn("SUPPLIER", typeof(System.String));
            dtSuppliers.Columns.Add(dcol);

            string suppliers = biditem.Suppliers;
            /*
			if (suppliers != "")
                    btnSubmit.Enabled = true;
			*/
            string[] suppliers1 = suppliers.Split(Convert.ToChar(","));

            for (int i = 0; i < suppliers1.Length ; i++)
            {
                //Create a new row
                DataRow drow = dtSuppliers.NewRow();
                drow["SUPPLIER"] = suppliers1[i];
                dtSuppliers.Rows.Add(drow);
            }

            gvSuppliers.DataSource = dtSuppliers;
            gvSuppliers.DataBind();

            DataTable dtFileAttachment = new DataTable();
            DataColumn dcol1 = new DataColumn("FILES", typeof(System.String));
            dtFileAttachment.Columns.Add(dcol1);

            string Files = biditem.FileAttachments;
            string[] Files1 = Files.Split(Convert.ToChar("|"));

            for (int i = 0; i < Files1.Length; i++)
            {
                //Create a new row
                DataRow drow = dtFileAttachment.NewRow();
                drow["FILES"] = Files1[i];
                dtFileAttachment.Rows.Add(drow);
            }

            gvFileAttachments.DataSource = dtFileAttachment;
            gvFileAttachments.DataBind();
		}

        private void FillBidItemDetails()
        {
            BidItemDetailTransaction bid = new BidItemDetailTransaction();
            DataView dv = bid.GetBidItemDetails(connstring, ViewState["BidRefNo"].ToString().Trim());

            if (dv.Count > 0)
            {
                gvBidItemDetails.DataSource = dv;
                gvBidItemDetails.DataBind();
            }
            else
            {
                DataTable dt = new DataTable();
                dt = CreateEmptyTable();
                gvBidItemDetails.DataSource = dt;
                gvBidItemDetails.DataBind();
            }
           
        }

        private DataTable CreateEmptyTable()
        {
           
            DataTable dt = new DataTable();
            DataColumn dcol = new DataColumn("Item", typeof(System.String));
            dt.Columns.Add(dcol);
            DataColumn dcol1 = new DataColumn("DetailDesc", typeof(System.String));
            dt.Columns.Add(dcol1);
            DataColumn dcol2 = new DataColumn("Qty", typeof(System.String));
            dt.Columns.Add(dcol2);
            DataColumn dcol3 = new DataColumn("UnitOfMeasure", typeof(System.String));
            dt.Columns.Add(dcol3);
            DataColumn dcol4 = new DataColumn("DeliveryDate1", typeof(System.String));
            dt.Columns.Add(dcol4);

            DataRow dr = dt.NewRow();
            dr["Item"] = "";
            dr["DetailDesc"] = "";
            dr["Qty"] = "";
            dr["UnitOfMeasure"] = "";
            dr["DeliveryDate1"] = "";
            dt.Rows.Add(dr);

            return dt;
        }

		protected void Page_Load(object sender, System.EventArgs e)
		{
            FormsAuthenticationHelper.AuthenticateUserWithReturnUrl();
            connstring = Client.ConnectionString(HttpContext.Current.Session["clientid"].ToString());
            if (Int32.Parse(Session[Constant.SESSION_USERTYPE].ToString().Trim()) != (int)Constant.USERTYPE.BUYER)
                Response.Redirect("../unauthorizedaccess.aspx");

            if (!(Page.IsPostBack))
            {
                if (Session["BidRefNo"] != null)
                        ViewState["BidRefNo"] = Session["BidRefNo"]; 
                Display();
                FillBidItemDetails();
               
            }
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
      
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            Session["BidRefNo"] = lblBidRefNum.Text;
            Session["mode"] = "EDIT";
            Response.Redirect("createnewitem.aspx");
        }

		protected void btnSubmit_Click(object sender, EventArgs e)
        {
            BidItemTransaction.UpdateBidStatus(connstring, ViewState["BidRefNo"].ToString().Trim(), Constant.BID_STATUS_SUBMITTED.ToString().Trim());
            Session["BidRefNo"] = lblBidRefNum.Text;
            Response.Redirect("bidsubmit.aspx");
        }

		protected void btnCancel_Click(object sender, EventArgs e)
        {
			Response.Redirect("deniedbiditemsforconversion.aspx");
        }
    }
}
