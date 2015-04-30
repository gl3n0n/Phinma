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
using EBid.lib.constant;
using EBid.lib.bid.trans;
using EBid.lib.user.trans;
using EBid.lib;
using EBid.ConnectionString;
using System.Data.SqlClient;
using System.Configuration;
using EBid.ConnectionString;

namespace EBid.WEB.boc
{
    public partial class bideventsforopening : System.Web.UI.Page
    {
        private string connstring = "";

        protected void Page_Load(object sender, System.EventArgs e)
        {
            connstring = Client.ConnectionString(HttpContext.Current.Session["clientid"].ToString());
            FormsAuthenticationHelper.AuthenticateUserWithReturnUrl();
            if (Int32.Parse(Session[Constant.SESSION_USERTYPE].ToString().Trim()) != (int)Constant.USERTYPE.BUYER)
                Response.Redirect("../unauthorizedaccess.aspx");

            Session[Constant.SESSION_LASTPAGE] = Request.AppRelativeCurrentExecutionFilePath;
            PageTitle.InnerText = String.Format(Constant.TITLEFORMAT, "Bid Events For Opening");
        }

        protected void gvBidEventsForOpening_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("openEvent"))
            {
                try
                {
                    string committeeType = GetCommitteeType(Session[Constant.SESSION_USERID].ToString()).ToString().Trim();

                    SqlParameter[] sqlParams = new SqlParameter[2];
                    sqlParams[0] = new SqlParameter("@BidRefNo", SqlDbType.Int);
                    sqlParams[1] = new SqlParameter("@CommitteeId", SqlDbType.Int);

                    sqlParams[0].Value = Int32.Parse(e.CommandArgument.ToString().Trim());
                    sqlParams[1].Value = Int32.Parse(committeeType);

                    SqlHelper.ExecuteNonQuery(connstring, CommandType.StoredProcedure, "sp_UpdateBidEvent", sqlParams);

                    gvBidEventsForOpening.DataBind();

                }
                catch
                {

                }
            }
            else if (e.CommandName.Equals("bidEvent"))
            {
                Session[Constant.SESSION_BIDREFNO] = e.CommandArgument.ToString().Trim();
                Response.Redirect("biddetails.aspx");
            }
        }

        private int GetCommitteeType(string userId)
        {
            SqlParameter[] sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@Userid", SqlDbType.Int);
            sqlParams[0].Value = Int32.Parse(userId);

            return Convert.ToInt32(SqlHelper.ExecuteScalar(connstring, CommandType.StoredProcedure, "sp_GetCommiteeType", sqlParams));
        }

        protected bool ISOpened(string usercommittee, string purchasing, string finance, string audit)
        {
            bool isOpened = false;

            switch (usercommittee)
            {
                case "1": isOpened = (purchasing == "1") ? true : false; break;
                case "2": isOpened = (finance == "1") ? true : false; break;
                case "3": isOpened = (audit == "1") ? true : false; break;
            }

            return isOpened;
        }

        protected bool IsEnabled(string deadline)
        {
            //if (DateTime.Parse(deadline.ToString()) < DateTime.Now)
            //{
            //    return true;
           // }

           // else
                return false;

        }

        private Control FindControl(Control parent, string ctrlName)
        {
            Control ctrl = null;

            ctrl = parent.FindControl(ctrlName);

            if (ctrl != null)
                return ctrl;

            for (int i = 0; i < parent.Controls.Count; i++)
            {
                ctrl = FindControl(parent.Controls[i], ctrlName);

                if (ctrl != null)
                    return ctrl;
            }

            return ctrl;
        }

        protected void gvBidEventsForOpening_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                
                string key1 = gvBidEventsForOpening.DataKeys[e.Row.RowIndex].Values[0].ToString();
                string key2 = gvBidEventsForOpening.DataKeys[e.Row.RowIndex].Values[1].ToString();
                string key3 = gvBidEventsForOpening.DataKeys[e.Row.RowIndex].Values[2].ToString();
                string key4 = gvBidEventsForOpening.DataKeys[e.Row.RowIndex].Values[3].ToString();
                string key5 = gvBidEventsForOpening.DataKeys[e.Row.RowIndex].Values[4].ToString();
                string key6 = gvBidEventsForOpening.DataKeys[e.Row.RowIndex].Values[5].ToString();
               
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                if (gvBidEventsForOpening.DataKeys.Count > 0)
                {
                    // Use the GetEnumerator method to create an enumerator that 
                    // contains the DataKey objects for the GridView control.
                    IEnumerator keyEnumerator = gvBidEventsForOpening.DataKeys.GetEnumerator();

                    // Iterate though the enumerator and display the primary key
                    // value of each record displayed.
                    int i = 0;
                    while (keyEnumerator.MoveNext())
                    {
                        DataKey key = (DataKey)keyEnumerator.Current;
                        Panel pnl1 = (Panel)gvBidEventsForOpening.Rows[i].Cells[3].Controls[1];
                        Panel pnl2 = (Panel)gvBidEventsForOpening.Rows[i].Cells[3].Controls[3];
                        LinkButton lb = (LinkButton)gvBidEventsForOpening.Rows[i].Cells[3].Controls[3].Controls[1];

                        pnl1.Visible = ISOpened(key[1].ToString(), key[2].ToString(), key[3].ToString(), key[4].ToString());
                        pnl2.Visible = !pnl1.Visible;
                        if (pnl2.Visible)
                        {
                            lb.Enabled = IsEnabled(key[5].ToString());
                            if (lb.Enabled)
                                lb.Attributes.Add("onclick", "return confirm('Open this event?');");
                            else
                                lb.Attributes.Add("style", "cursor:default;");
                        }
                        // move counter
                        i++;
                    }                    
                }                
            }
        }
    }
}
