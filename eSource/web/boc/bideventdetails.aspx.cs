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
using System.Data.Sql;
using System.Data.SqlClient;
using EBid.ConnectionString;
    
public partial class web_boc_bideventdetails : System.Web.UI.Page
{
    private string connstring = "";

    protected void Page_Load(object sender, System.EventArgs e)
    {
	connstring = Client.ConnectionString(HttpContext.Current.Session["clientid"].ToString());
        FormsAuthenticationHelper.AuthenticateUserWithReturnUrl();
        if (Int32.Parse(Session[Constant.SESSION_USERTYPE].ToString().Trim()) != (int)Constant.USERTYPE.BIDOPENINGCOMMITTEE)
            Response.Redirect("../unauthorizedaccess.aspx");
        
        if (Request.QueryString["brn"] != null)
        {
            Session[Constant.SESSION_BIDREFNO] = Request.QueryString["brn"].ToString();
        }

        if (Session[Constant.SESSION_BIDREFNO] == null)
            Response.Redirect("bids.aspx");

        PageTitle.InnerText = String.Format(Constant.TITLEFORMAT, "Bid Event Details");

        if (Session[Constant.SESSION_LASTPAGE] != null)
        {
            if (Session[Constant.SESSION_LASTPAGE].ToString() == "~/web/buyerscreens/bidsforeval.aspx")
            {
                lnkViewReport.Visible = true;
            }
            else
            {
                lnkViewReport.Visible = false;
            }
        }

        if (!IsPostBack)
        {
            lnkViewReport.NavigateUrl = "javascript://";
            lnkViewReport.Attributes.Add("onclick", "window.open('../reports/eventtenderscomparison.aspx?brn=" + Session[Constant.SESSION_BIDREFNO].ToString().Trim() + "','r1', 'toolbar=no, menubar=no, width=800; height=600, top=80, left=80, resizable=yes , scrollbars=yes');");

            if (IsForOpening() != 0)
            {
                lnkViewReport.Enabled = false;
            }
            else
            {
                lnkViewReport.Enabled = !isEnabledComparison();
            }
            Session["ViewOption"] = "AsBuyer";
        }
    }

    protected void lnkBack_Click(object sender, EventArgs e)
    {
        if (Session[Constant.SESSION_LASTPAGE] != null)
            Response.Redirect(Session[Constant.SESSION_LASTPAGE].ToString());
        else
            Response.Redirect("bids.aspx");
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

    private int IsForOpening()
    {

        SqlConnection sqlConnect = new SqlConnection(connstring);

        int count = 0;

        sqlConnect.Open();

        SqlParameter[] sqlParams = new SqlParameter[1];
        sqlParams[0] = new SqlParameter("@BidRefNo", SqlDbType.Int);
        sqlParams[0].Value = int.Parse(Session[Constant.SESSION_BIDREFNO].ToString());

        count = Convert.ToInt32(SqlHelper.ExecuteScalar(connstring, CommandType.StoredProcedure, "sp_IsBidEventForOpening", sqlParams));

        sqlConnect.Close();

        return count;
    }
}

