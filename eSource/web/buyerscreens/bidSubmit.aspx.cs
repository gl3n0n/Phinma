using System;
using System.Collections;
using System.Configuration;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
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

namespace EBid.WEB.buyer_screens
{
	/// <summary>
	/// Summary description for bidSubmit.
	/// </summary>
	public partial class bidSubmit : System.Web.UI.Page
	{

        private string connstring = "";

		protected void Page_Load(object sender, System.EventArgs e)
		{
            FormsAuthenticationHelper.AuthenticateUserWithReturnUrl();
			connstring = Client.ConnectionString(HttpContext.Current.Session["clientid"].ToString());
            if (Int32.Parse(Session[Constant.SESSION_USERTYPE].ToString().Trim()) != (int)Constant.USERTYPE.BUYER)
                Response.Redirect("../unauthorizedaccess.aspx");


			if (!(Page.IsPostBack))
			{
                if (Session["BidRefNo"] != null)
                {
                    hdnBidRefNo.Value = Session["BidRefNo"].ToString().Trim();
                    BidItem bid = BidItemTransaction.QueryBidItemInfo(connstring, hdnBidRefNo.Value.Trim());
                    BuyerTransaction buyer = new BuyerTransaction();
                    CategoryTransaction category = new CategoryTransaction();
                    GroupDeptSecTransaction grp = new GroupDeptSecTransaction();
                    CompanyTransaction cmp = new CompanyTransaction();
                    SupplierTransaction vnd = new SupplierTransaction();
                    IncotermTransaction inc = new IncotermTransaction();
                    OtherTransaction dte = new OtherTransaction();

                    lblCompany.Text = cmp.GetCompanyName(connstring, bid.CompanyId.ToString().Trim());
                    lblRequestor.Text = bid.Requestor.ToString().Trim();
                    lblPRNumber.Text = bid.PRRefNo.ToString().Trim();
                    lblPRDate.Text = bid.PRDate.ToString().Trim();
                    lblGroup.Text = grp.GetGroupDeptSecNameById(connstring, bid.GroupDeptSec.ToString().Trim());
                    lblBidReferenceNo.Text = bid.BidRefNo.ToString().Trim();
                    lblSubCategory.Text = category.GetCategoryNameById(connstring, bid.Category.ToString().Trim());
                    lblBidSubmissionDeadline.Text = bid.Deadline.ToString().Trim();
                    lblBidItemDescription.Text = bid.ItemDescription.ToString().Trim();
                    lblDeliverTo.Text = bid.DeliverTo.ToString().Trim();
                    lblIncoterm.Text = inc.GetIncotermName(connstring, bid.Incoterm.ToString().Trim());
                    lblBidRefNo2.Text = bid.BidRefNo.ToString().Trim();

                    FillBidItemDetails();
                    //no checking because when bids are submitted they have suppliers
                    gvSuppliers.DataSource = BidItemTransaction.GetSuppliers(connstring, hdnBidRefNo.Value.Trim());
                    gvSuppliers.DataBind();

                    //DataTable dtFileAttachment = new DataTable();
                    //DataColumn dcol1 = new DataColumn("FILES", typeof(System.String));
                    //dtFileAttachment.Columns.Add(dcol1);

                    //string Files = bid.FileAttachments;
                    //string[] Files1 = Files.Split(Convert.ToChar("|"));

                    //for (int i = 0; i < Files1.Length; i++)
                    //{
                    //    //Create a new row
                    //    DataRow drow = dtFileAttachment.NewRow();
                    //    drow["FILES"] = Files1[i];
                    //    dtFileAttachment.Rows.Add(drow);
                    //}

                    //gvFileAttachments.DataSource = dtFileAttachment;
                    //gvFileAttachments.DataBind();
                }
                else
                {
                    Response.Redirect("index.aspx");
                }
            }
		}

        private void FillBidItemDetails()
        {
            BidItemDetailTransaction bid = new BidItemDetailTransaction();
            DataView dv = bid.GetBidItemDetails(connstring, hdnBidRefNo.Value.Trim());

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
            DataColumn dcol = new DataColumn("SKU", typeof(System.String));
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
            dr["SKU"] = "";
            dr["DetailDesc"] = "";
            dr["Qty"] = "";
            dr["UnitOfMeasure"] = "";
            dr["DeliveryDate1"] = "";
            dt.Rows.Add(dr);

            return dt;
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
            try
            {
                BidDetails details = GetBidItemDetails(int.Parse(Session["BidRefNo"].ToString()));
                PurchaseOfficerInfo info = GetPurchaseOfficerInfo(Convert.ToInt32(details.BuyerID.ToString()));

                if (SendEmailNotification(details,info))
                {
                    Session["Message"] = "Notification sent successfully.";
                }
                else
                {
                    // failed
                    Session["Message"] = "Failed to send notification. Please try again or contact adminitrator for assistance.";
                }
            }
            catch
            {
                // failed
                Session["Message"] = "Failed to send notification. Please try again or contact adminitrator for assistance.";
            }

            ClearVars();
            Response.Redirect("index.aspx");
        }

        private void ClearVars()
        {
            Session["Message"] = "";
            Session["ItemType"] = "";
            Session["Company"] = "";
            Session["Requestor"] = "";
            Session["PRNumber"] = "";
            Session["PRDate"] = "";
            Session["GroupDeptSec"] = "";
            Session["BidRefNo"] = "";
            Session["CategoryId"] = "";
            Session["BidSubmissionDeadline"] = "";
            Session["BidItemDescription"] = "";
            Session["DeliverTo"] = "";
            Session["Incoterm"] = "";
            Session["SubItems"] = null;
            Session["mode"] = null;
            Session["FileNames"] = null;
            Session["moreSubItems"] = null;
            Session["SubItemId"] = null;
            Session["Suppliers"] = null;
        }

        private BidDetails GetBidItemDetails(int bidrefno)
        {
            DataTable dt = SqlHelper.ExecuteDataset(connstring, "sp_GetBidInvitationInfo", new SqlParameter[] { new SqlParameter("@BidRefNo", bidrefno) }).Tables[0];
            BidDetails item = new BidDetails();

            if (dt.Rows.Count > 0)
                item = new BidDetails(dt.Rows[0]);

            return item;
        }

        private  PurchaseOfficerInfo GetPurchaseOfficerInfo(int buyerid)
        {
            DataTable dt = SqlHelper.ExecuteDataset(connstring, "sp_GetBuyerSupervisorInfo", new SqlParameter[] { new SqlParameter("@BuyerId", buyerid) }).Tables[0];
            PurchaseOfficerInfo info = new PurchaseOfficerInfo();

            if (dt.Rows.Count > 0)
                info = new PurchaseOfficerInfo(dt.Rows[0]);

            return info;
        }

        private bool SendEmailNotification(BidDetails biddetails, PurchaseOfficerInfo info)
        {
            bool success = false;

            string subject = "Trans-Asia  : Notification";
            
            try
            {
                if (!MailHelper.SendEmail(MailTemplate.GetDefaultSMTPServer(),
                        MailHelper.ChangeToFriendlyName(biddetails.Creator, biddetails.CreatorEmail),
                        MailHelper.ChangeToFriendlyName(info.Supervisor, info.EmailAdd),
                        subject,
                        CreateNotificationBody(),
                        MailTemplate.GetTemplateLinkedResources(this)))
                {	// if sending failed					
                    LogHelper.EventLogHelper.Log("Bid > Send Notification : Sending Failed to " + info.EmailAdd, System.Diagnostics.EventLogEntryType.Error);
                }
                else
                {	// if sending successful
                    LogHelper.EventLogHelper.Log("Bid > Send Notification : Email Sent to " + info.EmailAdd, System.Diagnostics.EventLogEntryType.Information);
                    
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

            sb.Append("<tr><td align='right'><h5>" + DateTime.Now.ToLongDateString() + "</h5></td></tr>");

            return MailTemplate.IntegrateBodyIntoTemplate(sb.ToString());
        }

}
}
