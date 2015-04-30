using System;
using System.Collections;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Text;
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
using System.Data.Sql;
using System.Data.SqlClient;
using EBid.ConnectionString;

public partial class web_boc_biditemdetails : System.Web.UI.Page
{
    private string connstring = "";

    protected void Page_Load(object sender, System.EventArgs e)
    {
        connstring = Client.ConnectionString(HttpContext.Current.Session["clientid"].ToString());
	FormsAuthenticationHelper.AuthenticateUserWithReturnUrl();
        if (Int32.Parse(Session[Constant.SESSION_USERTYPE].ToString().Trim()) != (int)Constant.USERTYPE.BIDOPENINGCOMMITTEE)
            Response.Redirect("../unauthorizedaccess.aspx");

        if (Session[Constant.SESSION_BIDDETAILNO] == null)
            Response.Redirect("bidsforeval.aspx");

        PageTitle.InnerText = String.Format(Constant.TITLEFORMAT, "Bid Item Details");

        if (!IsPostBack)
        {
            gvBidItemTenders.SelectedIndex = 0;

            lnkComparison.NavigateUrl = "javascript://";
            lnkComparison.Attributes.Add("onclick", "window.open('../reports/bidtendercomparisons.aspx?bdn=' + " + Session[Constant.SESSION_BIDDETAILNO].ToString() + " , 'x', 'toolbar=no, menubar=no, width=800; height=600, top=80, left=80, resizable=yes, scrollbars=yes');");
        }
        
        lnkComparison.Enabled = !isEnabledComparison();

        if (Session["EndorsementMessage"] != null)
        {
            lblMessage.Text = Session["EndorsementMessage"].ToString().Trim();
            Session["EndorsementMessage"] = null;
        }
    }

    protected void lnkBack_Click(object sender, EventArgs e)
    {
        if (Session[Constant.SESSION_LASTPAGE] != null)
            Response.Redirect(Session[Constant.SESSION_LASTPAGE].ToString());
        else
            Response.Redirect("bidsforeval.aspx");
    }

    protected void gvBidItemTenders_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridViewRow gvr = ((Control)e.CommandSource).NamingContainer as GridViewRow;
        Panel pnlLinks = (Panel)gvr.FindControl("pnlLinks");
        Panel pnlComments = (Panel)gvr.FindControl("pnlComments");

        switch (e.CommandName)
        {
            case "Endorse":
                {
                    pnlLinks.Visible = false;
                    pnlComments.Visible = true;
                    gvBidItemTenders.Columns[5].ControlStyle.Width = Unit.Pixel(150);
                    Session[Constant.SESSION_BIDTENDERNO] = e.CommandArgument.ToString();
                    LoadComments();
                } break;
            case "ContinueEndorsement":
                {
                    TextBox tbComments = (TextBox)pnlComments.FindControl("txtComment");                    
                    
                    // update bid tender status to "endorsed"
                    bool updateOk = BidTransaction.UpdateBidTenderStatus(connstring, Convert.ToInt32(e.CommandArgument.ToString()), Constant.BIDTENDER_STATUS.STATUS.ENDORSED);
                    // save comment
                    bool saveCommentOk = BidTransaction.SaveBidTenderComment(connstring, int.Parse(e.CommandArgument.ToString()),
                        int.Parse(Session[Constant.SESSION_USERID].ToString()), tbComments.Text.Trim(), Constant.BIDTENDERCOMMENT_BUYER_TO_PURCHASING);

                    if (updateOk && saveCommentOk)
                    {
                        Session["EndorsementMessage"] = "Bid tender was successfully endorsed.";
                        Response.Redirect("biditemdetails.aspx");
                    }
                    else
                    {
                        Session["EndorsementMessage"] = "Bid tender was not endorsed.";
                        pnlLinks.Visible = true;
                        pnlComments.Visible = false;                        
                    }
                    Session[Constant.SESSION_BIDTENDERNO] = e.CommandArgument.ToString();
                    LoadComments();
                } break;
            case "CancelEndorsement":
                {
                    pnlLinks.Visible = true;
                    pnlComments.Visible = false;
                    gvBidItemTenders.Columns[5].ControlStyle.Width = Unit.Pixel(90);
                    pnlComments.Width = Unit.Percentage(100.00);
                    pnlLinks.Width = Unit.Percentage(100.00);
                    dvTenderDetails.PageIndex = gvBidItemTenders.SelectedIndex;
                    Session[Constant.SESSION_BIDTENDERNO] = gvBidItemTenders.DataKeys[gvBidItemTenders.SelectedIndex].Values[0].ToString();
                    LoadComments();
                } break;
            case "Select":
                {
                    string[] args = e.CommandArgument.ToString().Split(new char[] { '|' });
                    Session[Constant.SESSION_BIDTENDERNO] = args[0];
                    Session["TVendorId"] = args[1];
                    Session[Constant.SESSION_BIDREFNO] = args[2];
                    Session["ViewOption"] = "AsVendor";
                    LoadComments();
                    dvTenderDetails.Visible = true;
                    pnlBidTenderAttachments.Visible = true;
                } break;
        }
    }

    protected void gvBidItemTenders_SelectedIndexChanged(object sender, EventArgs e)
    {
        dvTenderDetails.PageIndex = gvBidItemTenders.SelectedIndex;
    }
    
    protected void gvBidItemTenders_DataBound(object sender, EventArgs e)
    {
        Session[Constant.SESSION_BIDTENDERNO] = gvBidItemTenders.DataKeys[gvBidItemTenders.SelectedIndex].Values[0].ToString();
        // load comments control
        LoadComments();
    }
    
    private void LoadComments()
    {
        phComments.Controls.Clear();
        web_usercontrol_commentlist_tender uc = (web_usercontrol_commentlist_tender)Page.LoadControl(@"~/web/usercontrol/commentlist_tender.ascx");
        phComments.Controls.Add(uc); 
    }

    protected bool isEndorsed(Object itemStatus)
    {
        int stat = int.Parse(itemStatus.ToString());

        return (stat == Constant.BIDTENDER_STATUS.STATUS.ENDORSED);
    }

    protected bool isEnabled(object itemStatus, object rdeadline)
    {
        int istat = int.Parse(itemStatus.ToString());

        if ((istat == Constant.BIDTENDER_STATUS.STATUS.RENEGOTIATED) && (rdeadline.ToString() == ""))
        {
           return true;
        }
        else if ((istat == Constant.BIDTENDER_STATUS.STATUS.RENEGOTIATED) && (rdeadline.ToString() != ""))
        {
            int stat = int.Parse(itemStatus.ToString());
            DateTime rdate = DateTime.Parse(rdeadline.ToString());
            DateTime dtnow = DateTime.Now;

            if (DateTime.Compare(rdate, dtnow) > 0)
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

    protected bool isEnabledComparison()
    {
        if ((GetBidEventRenegotiationDeadline().ToString() != ""))
        {
            DateTime rdate = DateTime.Parse(GetBidEventRenegotiationDeadline().ToString());
            DateTime dtnow = DateTime.Now;

            if (DateTime.Compare(rdate, dtnow) > 0)
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

    private string GetBidEventRenegotiationDeadline()
    {
        SqlParameter[] sqlParams = new SqlParameter[1];
        sqlParams[0] = new SqlParameter("@BidRefNo", SqlDbType.Int);
        sqlParams[0].Value = Int32.Parse(Session[Constant.SESSION_BIDREFNO].ToString());

        return SqlHelper.ExecuteScalar(connstring, CommandType.StoredProcedure, "sp_GetBidEventRenegotiationDeadline", sqlParams).ToString().Trim();
    }
}

