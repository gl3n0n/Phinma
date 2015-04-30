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
using System.IO;
using System.Configuration;
using EBid.lib;
using EBid.ConnectionString;

public partial class web_vendorscreens_bidDetails1 : System.Web.UI.Page
{
    private string connstring = "";

    private void FillBidInfo()
    {// Get bid general details
        CategoryTransaction cat = new CategoryTransaction();
        IncotermTransaction inc = new IncotermTransaction();
        CompanyTransaction cmp = new CompanyTransaction();
        BidItem bid = BidItemTransaction.GetBidDetailsByRefNo(hdnBidRefNo.Value.Trim());

        lblCompany.Text = cmp.GetCompanyName(connstring, bid.CompanyId.ToString().Trim());
        lblCategory.Text = cat.GetCategoryNameById(bid.Category.ToString().Trim());
        lblBidReferenceNumber.Text = bid.BidRefNo.ToString();
        lblBidSubmissionDeadline.Text = bid.Deadline;
        lblBidItemDescription.Text = bid.ItemDescription;
        lblDeliverTo.Text = bid.DeliverTo;
        lblPreferredIncoterm.Text = inc.GetIncotermName(connstring, bid.Incoterm);
    }

    private void FillBidItemDetails()
    {
        BidItemDetailTransaction bid = new BidItemDetailTransaction();
        gvBidItemDetails.DataSource = bid.GetBidItemDetails(connstring, hdnBidRefNo.Value.Trim());
        gvBidItemDetails.DataBind();
    }

    protected void Page_Load(object sender, System.EventArgs e)
    {
        FormsAuthenticationHelper.AuthenticateUserWithReturnUrl();
        connstring = Client.ConnectionString(HttpContext.Current.Session["clientid"].ToString());
        if (Int32.Parse(Session[Constant.SESSION_USERTYPE].ToString().Trim()) != (int)Constant.USERTYPE.VENDOR)
            Response.Redirect("../unauthorizedaccess.aspx");

        PageTitle.InnerText = String.Format(Constant.TITLEFORMAT, "Bid Details");

        if (!(IsPostBack))
        {
            if (Session[Constant.SESSION_USERID] != null)
            {
                if (Request.QueryString["BidRefNo"] != null)
                    Session[Constant.SESSION_BIDREFNO] = Request.QueryString["BidRefNo"].ToString().Trim();

                if (Session[Constant.SESSION_BIDREFNO] != null)
                {
                    ViewState[Constant.SESSION_BIDREFNO] = Session[Constant.SESSION_BIDREFNO].ToString().Trim();
                    hdnBidRefNo.Value = ViewState[Constant.SESSION_BIDREFNO].ToString().Trim();
                    FillBidInfo();
                    FillBidItemDetails();
                }
            }            
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
}
