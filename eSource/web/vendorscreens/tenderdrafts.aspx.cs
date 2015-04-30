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
    public partial class TenderDrafts : System.Web.UI.Page
    {
        protected void Page_Load(object sender, System.EventArgs e)
        {
            FormsAuthenticationHelper.AuthenticateUserWithReturnUrl();
            if (Int32.Parse(Session[Constant.SESSION_USERTYPE].ToString().Trim()) != (int)Constant.USERTYPE.VENDOR)
                Response.Redirect("../unauthorizedaccess.aspx");
            
            PageTitle.InnerText = String.Format(Constant.TITLEFORMAT, "Bid Tender Drafts");

            Session[Constant.SESSION_LASTPAGE] = Request.AppRelativeCurrentExecutionFilePath;
            if (Session["Renegotiated"] != null)            
                Session["Renegotiated"] = null;            
        }

        protected void gvTenderDrafts_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            GridViewRow gvr = ((Control)e.CommandSource).NamingContainer as GridViewRow;

            switch (e.CommandName)
            {
                case "EditTender":
                    {
                        string[] args = e.CommandArgument.ToString().Split(new char[] { '|' });
                        Session[Constant.SESSION_BIDTENDERNO] = args[0];
                        Session[Constant.SESSION_BIDREFNO] = args[2];
                        if (int.Parse(args[1]) > 0)
                        {
                            Session["Renegotiated"] = "1";
                            Response.Redirect("tenderdetails.aspx");
                            Session["AsClarified"] = gvTenderDrafts.DataKeys[gvr.RowIndex].Values[1].ToString();
                        }
                        else
                        {
                            Response.Redirect("tenderdetails.aspx");
                        }
                    } break;
                case "EditBidEventTenders":
                    {
                        string[] args = e.CommandArgument.ToString().Split(new char[] { '|' });
                        Session[Constant.SESSION_BIDREFNO] = args[0];
                        Session[Constant.SESSION_BIDTENDERNO] = args[1];
                        if (int.Parse(args[2]) > 0)
                        {
                            Session["ViewOption"] = "AsBuyer";
                            Response.Redirect("biddetails.aspx");
                        }
                        else
                        {
                            Response.Redirect("submittender.aspx?t=2");
                        }
                            
                    } break;
            }
        }
    }
}