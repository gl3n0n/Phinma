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
using System.Data.SqlClient;
using EBid.lib.bid.data;
using EBid.lib.bid.trans;
using EBid.lib.constant;
using System.IO;
using System.Configuration;
using EBid.lib;
using EBid.ConnectionString;

namespace EBid.web.boc
{
    public partial class biddetails : System.Web.UI.Page
    {
        private string connstring = "";

        protected void Page_Load(object sender, System.EventArgs e)
        {
            connstring = Client.ConnectionString(HttpContext.Current.Session["clientid"].ToString());
            FormsAuthenticationHelper.AuthenticateUserWithReturnUrl();
            if (Int32.Parse(Session[Constant.SESSION_USERTYPE].ToString().Trim()) != (int)Constant.USERTYPE.BIDOPENINGCOMMITTEE)
                Response.Redirect("../unauthorizedaccess.aspx");

            PageTitle.InnerText = String.Format(Constant.TITLEFORMAT, "Bid Details");

            if (Session[Constant.SESSION_BIDREFNO] == null)
                Response.Redirect("bidseventsforopening.aspx");

            if (!IsPostBack)
            {             
                lnkComparison.NavigateUrl = "javascript://";
                lnkComparison.Attributes.Add("onclick", "window.open('../reports/eventtenderscomparison.aspx?brn=" + Session[Constant.SESSION_BIDREFNO].ToString().Trim() + "','r1', 'toolbar=no, menubar=no, width=800; height=600, top=80, left=80, resizable=yes , scrollbars=yes');");
                
                if (IsForOpening() != 0)
                {
                    lnkComparison.Enabled = false;
                }
            }

        }

        protected void lnkBack_Click(object sender, EventArgs e)
        {
            if (Session[Constant.SESSION_LASTPAGE] != null)
                Response.Redirect(Session[Constant.SESSION_LASTPAGE].ToString());
            else
                Response.Redirect("bidsopened.aspx");
        }

        private int IsForOpening()
        {

            SqlConnection sqlConnect = new SqlConnection(connstring);

            int count = 0;

            sqlConnect.Open();

            SqlParameter[] sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@BidRefNo", SqlDbType.Int);
            sqlParams[0].Value = int.Parse(Session[Constant.SESSION_BIDREFNO].ToString());

            count =  Convert.ToInt32(SqlHelper.ExecuteScalar(connstring, CommandType.StoredProcedure, "sp_IsBidEventForOpening", sqlParams));

            sqlConnect.Close();

            return count;
        }
    }
}
