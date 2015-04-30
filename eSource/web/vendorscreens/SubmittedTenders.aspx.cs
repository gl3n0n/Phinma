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
using EBid.lib.bid.data;
using EBid.lib.bid.trans;
using EBid.lib;

namespace EBid.web.vendor_screens
{
    public partial class SubmittedTenders : System.Web.UI.Page
    {
        protected void Page_Load(object sender, System.EventArgs e)
        {
            FormsAuthenticationHelper.AuthenticateUserWithReturnUrl();
            if (Int32.Parse(Session[Constant.SESSION_USERTYPE].ToString().Trim()) != (int)Constant.USERTYPE.VENDOR)
                Response.Redirect("../unauthorizedaccess.aspx");
            Session[Constant.SESSION_LASTPAGE] = Request.AppRelativeCurrentExecutionFilePath;

            PageTitle.InnerText = String.Format(Constant.TITLEFORMAT, "Submitted Bid Tenders");
            if (Session["Renegotiated"] != null)
                Session["Renegotiated"] = null;
        }

        protected void gvSubmittedTenders_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "EditTender":
                    {
                        string[] args = e.CommandArgument.ToString().Split(new char[] { '|' });
                        Session[Constant.SESSION_BIDDETAILNO] = args[0];
                        Session[Constant.SESSION_BIDTENDERNO] = args[1];
                        Session[Constant.SESSION_BIDREFNO] = args[3];
                        DateTime deadline = DateTime.Parse(args[2]);
                        if (DateTime.Compare(deadline, DateTime.Now) > 0 )
                        {
                            Response.Redirect("tenderdetails.aspx");
                        }
                        else
                        {
                            Response.Redirect("biditemdetails.aspx");                               
                        }
                    } break;
                case "EditBidEventTenders":
                    {
                        string[] args = e.CommandArgument.ToString().Split(new char[] { '|' });
                        Session[Constant.SESSION_BIDREFNO] = args[0];
                        Session[Constant.SESSION_BIDTENDERNO] = args[1];
                        DateTime deadline = DateTime.Parse(args[2]);
                        if (DateTime.Compare(deadline, DateTime.Now) > 0)
                        {
                            Session["ViewOption"] = "AsBuyer";
                            Response.Redirect("submittender.aspx?t=2");
                        }
                        else
                        {
                            Session["ViewOption"] = "AsBuyer";
                            Response.Redirect("biddetails.aspx");                            
                        }
                    } break;
            }
        }
    }
}
