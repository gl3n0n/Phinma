using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using EBid.lib.constant;
using EBid.lib.bid.data;
using EBid.lib.bid.trans;
using EBid.lib;

namespace EBid.web.vendor_screens
{
    public partial class BidsForRenegotiation : System.Web.UI.Page
    {
        protected void Page_Load(object sender, System.EventArgs e)
        {
            FormsAuthenticationHelper.AuthenticateUserWithReturnUrl();
            if (Int32.Parse(Session[Constant.SESSION_USERTYPE].ToString().Trim()) != (int)Constant.USERTYPE.VENDOR)
                Response.Redirect("../unauthorizedaccess.aspx");

            PageTitle.InnerText = String.Format(Constant.TITLEFORMAT, "Bid Tenders For Clarification");
            Session[Constant.SESSION_LASTPAGE] = Request.AppRelativeCurrentExecutionFilePath;
        }

        protected void gvTenderDrafts_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            GridViewRow gvr = ((Control)e.CommandSource).NamingContainer as GridViewRow;

            if (e.CommandName.Equals("EditTender"))
            {
                string[] args = e.CommandArgument.ToString().Split(new char[] { '|' });
                if (args[1] == "1")
                {
                    Session[Constant.SESSION_BIDTENDERNO] = args[0];
                    Session[Constant.SESSION_BIDDETAILNO] = args[2];
                    Session[Constant.SESSION_BIDREFNO] = args[3];
                    Session["Renegotiated"] = "1";
                    Response.Redirect("biditemdetails.aspx");
                }
                else
                {
                    Session[Constant.SESSION_BIDTENDERNO] = args[0];
                    Session[Constant.SESSION_BIDDETAILNO] = args[2];
                    Session[Constant.SESSION_BIDREFNO] = args[3];
                    Session["Renegotiated"] = "1";
                    Session["AsClarified"] = gvTenderDrafts.DataKeys[gvr.RowIndex].Values[1].ToString();
                    Response.Redirect("tenderdetails.aspx");
                }
            }
            else if (e.CommandName.Equals("SelectBidEvent"))
            {
                Session["ViewOption"] = "AsBuyer";
                Session[Constant.SESSION_BIDREFNO] = e.CommandArgument.ToString().Trim();
                Response.Redirect("biddetails.aspx");
            }
        }

        protected bool IsEnabled(string deadline)
        {

            if (deadline.Length > 0)
            {
                DateTime datenow = DateTime.Now;
                DateTime rdeadline = DateTime.Parse(deadline);

                if (DateTime.Compare(datenow, rdeadline) < 0)
                {
                    return true;
                }
                else
                    return false;
            }
            else
                return true;
        }
    }
}