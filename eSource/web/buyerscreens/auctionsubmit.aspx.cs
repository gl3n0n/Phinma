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
using EBid.lib.auction.trans;
using EBid.lib.auction.data;
using EBid.lib.bid.trans;
using EBid.lib.constant;
using EBid.lib;
using EBid.ConnectionString;

public partial class web_buyer_screens_AuctionSubmit : System.Web.UI.Page
{
	private string connstring = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        FormsAuthenticationHelper.AuthenticateUserWithReturnUrl();
		connstring = Client.ConnectionString(HttpContext.Current.Session["clientid"].ToString());
        if (Int32.Parse(Session[Constant.SESSION_USERTYPE].ToString().Trim()) != (int)Constant.USERTYPE.BUYER)
            Response.Redirect("../unauthorizedaccess.aspx");

        if (!(IsPostBack))
        {
            Session[Constant.SESSION_COMMENT_TYPE] = "1";
            if (Session["AuctionRefNo"] != null)
            {
                hdnAuctionRefNo.Value = Session["AuctionRefNo"].ToString().Trim();
            }
            else
            {
                Response.Redirect("bids.aspx");
            }
                if (hdnAuctionRefNo.Value.Trim() != "")
                {
                    AuctionTransaction au = new AuctionTransaction();
                    AuctionItem ai = au.GetAuctionByAuctionRefNo(connstring, hdnAuctionRefNo.Value.Trim());
                    //lblAuctionTitle.Text = au.GetAuctionTitle(connstring, hdnAuctionRefNo.Value.Trim());
                    //CompanyTransaction cmp = new CompanyTransaction();
                    //GroupDeptSecTransaction grp = new GroupDeptSecTransaction();
                    //CategoryTransaction cat = new CategoryTransaction();
                    //lblCompany.Text = cmp.GetCompanyName(ai.CompanyId.ToString().Trim());
                    //lblRequestor.Text = ai.Requestor.ToString().Trim();
                    //lblPRNumber.Text = ai.PRRefNo.ToString().Trim();
                    //lblPRDate.Text = ai.PRDate.ToString().Trim();
                    //lblGroup.Text = grp.GetGroupDeptSecNameById(ai.GroupDeptSec.ToString().Trim());
                    //lblSubCategory.Text = cat.GetCategoryNameById(ai.Category.ToString().Trim());
                    //lblDeadline.Text = ai.AuctionDeadline.ToString().Trim();
                    //lblDeliveryDate.Text = ai.DeliveryDate.ToString().Trim();
                    //lblItemDescription.Text = ai.ItemDescription.ToString().Trim();
                    //lblReferenceNumber.Text = hdnAuctionRefNo.Value.Trim();

                    lblAuctionType.Text = au.GetAuctionTypeNameById(connstring, ai.AuctionType.ToString().Trim());
                    lblAuctionConfirmationDeadline.Text = ai.AuctionDeadline.ToString().Trim();
                    lblAuctionEventDate.Text = ai.AuctionStartDate.ToString().Trim();
                    lblAuctionStartTime.Text = ai.AuctionStartTimeHour.ToString().Trim() + ":" +
                                               ai.AuctionStartTimeMin.ToString().Trim() + ":" +
                                               ai.AuctionStartTimeSec.ToString().Trim() + " " +
                                               ai.AuctionStartTimeAMPM.ToString().Trim();
                    lblAuctionEndTime.Text = ai.AuctionEndTimeHour.ToString().Trim() + ":" +
                                            ai.AuctionEndTimeMin.ToString().Trim() + ":" +
                                                ai.AuctionEndTimeSec.ToString().Trim() + " " +
                                                ai.AuctionEndTimeAMPM.ToString().Trim();

                    DataTable dt = new DataTable();
                    dt = au.GetSuppliers(connstring, hdnAuctionRefNo.Value.Trim());

                    gvSuppliers.DataSource = dt;
                    gvSuppliers.DataBind();

                }
        }
    }

    private DataTable CreateEmptyTable()
    {
        DataTable dt = new DataTable();
        DataColumn dc = new DataColumn("AuctionDetailNo", typeof(System.String));
        dt.Columns.Add(dc);
        dc = new DataColumn("Item", typeof(System.String));
        dt.Columns.Add(dc);
        dc = new DataColumn("Description", typeof(System.String));
        dt.Columns.Add(dc);
        dc = new DataColumn("Quantity", typeof(System.String));
        dt.Columns.Add(dc);
        dc = new DataColumn("UnitOfMeasure", typeof(System.String));
        dt.Columns.Add(dc);
        DataRow dr = dt.NewRow();
        dr["AuctionDetailNo"] = "";
        dr["Item"] = "";
        dr["Description"] = "";
        dr["Quantity"] = "";
        dr["UnitOfMeasure"] = "";
        dt.Rows.Add(dr);
        return dt;
    }

    protected void btnOk_Click(object sender, EventArgs e)
    {
        if (Session[Constant.SESSION_LASTPAGE] != null)
            Response.Redirect(Session[Constant.SESSION_LASTPAGE].ToString());
        else
            Response.Redirect("auctions.aspx");
    }
}
