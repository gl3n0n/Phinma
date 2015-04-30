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
using System.Data.SqlClient;
using EBid.lib.bid.trans;
using EBid.lib.auction.data;
using EBid.lib.constant;
using EBid.lib;
using EBid.ConnectionString;

public partial class web_buyer_screens_bidsforrenegotiation : System.Web.UI.Page
{
    private string connstring = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        //connstring = System.Configuration.ConfigurationManager.ConnectionStrings["EBidConnectionString"].ConnectionString;
	connstring = Client.ConnectionString(HttpContext.Current.Session["clientid"].ToString());
	FormsAuthenticationHelper.AuthenticateUserWithReturnUrl();
        if (Int32.Parse(Session[Constant.SESSION_USERTYPE].ToString().Trim()) != (int)Constant.USERTYPE.BUYER)
            Response.Redirect("../unauthorizedaccess.aspx");

        Session[Constant.SESSION_LASTPAGE] = Request.AppRelativeCurrentExecutionFilePath;
        PageTitle.InnerText = String.Format(Constant.TITLEFORMAT, "Bid Tenders For Renegotiation");
    }

    protected string showBidTenderCount(Object itemCount)
    {
        int count = Int32.Parse(itemCount.ToString());

        if (count == 0)
            return "(There are no renegotiated bid tenders for this item.)";
        else if (count == 1)
            return "(There is 1 renegotiated bid tender for this item.)";
        else
            return "(There are " + itemCount + " renegotiated bid tenders for this item.)";
    }

    protected void gvBids_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridViewRow gvr = ((Control)e.CommandSource).NamingContainer as GridViewRow;

        switch (e.CommandName.ToString().Trim())
        {
            case "ViewBidEventDetails":
                {
                    Session["ViewOption"] = "AsBuyer";
                    Session[Constant.SESSION_BIDREFNO] = e.CommandArgument.ToString().Trim();
                    Response.Redirect("bideventdetails.aspx");
                } break;
            case "ViewBidItemDetails":
                {
                    string[] args = e.CommandArgument.ToString().Split('|');
                    Session[Constant.SESSION_BIDREFNO] = args[0];
                    Session[Constant.SESSION_BIDDETAILNO] = args[1];
                    Response.Redirect("renegotiatedbiditemdetails.aspx");
                } break;
            case "Select":
                {
                    Panel pnlRenegotiationDeadline = (Panel)gvr.FindControl("pnlRenegotiationDeadline");
                    Panel pnlSetRenegotiationDeadline = (Panel)pnlRenegotiationDeadline.FindControl("pnlSetRenegotiationDeadline");
                    LinkButton lnkSetRenegotiationDeadline = (LinkButton)pnlRenegotiationDeadline.FindControl("lnkSetRenegotiationDeadline");

                    lnkSetRenegotiationDeadline.Visible = false;
                    pnlSetRenegotiationDeadline.Visible = true;
                } break;
            case "OK":
                {
                    if (IsValid)
                    {
                        if (Session["RenegotiationDeadline"] != null)
                        {
                            DateTime deadline = (DateTime)Session["RenegotiationDeadline"];
                            if (UpdateBidEventRenegotiationDeadline(int.Parse(e.CommandArgument.ToString()), deadline))
                            {
                                // success
                                Response.Redirect("bidsforrenegotiation.aspx");
                            }
                            else
                            {
                                // failed
                            }
                        }

                        else
                        {
                            // failed
                        }

                    }

                } break;
            case "Cancel":
                {
                    Panel pnlRenegotiationDeadline = (Panel)gvr.FindControl("pnlRenegotiationDeadline");
                    Panel pnlSetRenegotiationDeadline = (Panel)pnlRenegotiationDeadline.FindControl("pnlSetRenegotiationDeadline");
                    LinkButton lnkSetRenegotiationDeadline = (LinkButton)pnlRenegotiationDeadline.FindControl("lnkSetRenegotiationDeadline");

                    pnlSetRenegotiationDeadline.Visible = false;
                    lnkSetRenegotiationDeadline.Visible = true;
                } break;
        }
    }

    protected void gvBids_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Control c;
            c = e.Row.FindControl("clndrRenegotiationDeadline");
            if (c != null)
            {
                ((CalendarControl.JSCalendar)c).Attributes.Add("style", "text-align: center;");
            }

            c = e.Row.FindControl("txtDeadlineHH");
            if (c != null)
            {
                TextBox tb = (TextBox)c;
                tb.Attributes.Add("style", "text-align: center;");
                tb.Attributes.Add("onfocus", "this.select();");
            }

            c = e.Row.FindControl("txtDeadlineMM");
            if (c != null)
            {
                TextBox tb = (TextBox)c;
                tb.Attributes.Add("style", "text-align: center;");
                tb.Attributes.Add("onfocus", "this.select();");
            }

            c = e.Row.FindControl("txtDeadlineSS");
            if (c != null)
            {
                TextBox tb = (TextBox)c;
                tb.Attributes.Add("style", "text-align: center;");
                tb.Attributes.Add("onfocus", "this.select();");
            }
        }
    }

    protected void cvRenegotiationDeadline_ServerValidate(object source, ServerValidateEventArgs args)
    {
        DateTime dt = new DateTime();
        TextBox tbHH = (TextBox)gvBids.SelectedRow.FindControl("txtDeadlineHH");
        TextBox tbMM = (TextBox)gvBids.SelectedRow.FindControl("txtDeadlineMM");
        TextBox tbSS = (TextBox)gvBids.SelectedRow.FindControl("txtDeadlineSS");
        DropDownList ddl = (DropDownList)gvBids.SelectedRow.FindControl("ddlDeadline");

        string val = args.Value + " " + tbHH.Text.Trim() + ":" + tbMM.Text.Trim() + ":" + tbSS.Text.Trim() + " " + ddl.SelectedValue;

        if (DateTime.TryParse(val, out dt))
        {
            // greated than date now
            if (DateTime.Compare(dt, DateTime.Now) > 0)
            {
                args.IsValid = true;
                Session["RenegotiationDeadline"] = dt;
            }
            else
            {
                Session["RenegotiationDeadline"] = null;
                args.IsValid = false;
            }
        }
        else
            args.IsValid = false;
    }

    protected bool showSetRenegotiationDeadline(Object itemIsDeadlineSet)
    {
        return (itemIsDeadlineSet.ToString() == "NOT SET");
    }

    protected bool showStatus(Object itemIsDeadlineSet)
    {
        return !(showSetRenegotiationDeadline(itemIsDeadlineSet));
    }

    private bool UpdateBidEventRenegotiationDeadline(int bidrefno, DateTime deadline)
    {
        SqlConnection sqlConnect = new SqlConnection(connstring);
	SqlTransaction sqlTransact = null;
        bool isSuccessful = false;

        try
        {
            sqlConnect.Open();
            sqlTransact = sqlConnect.BeginTransaction();

	    //Response.Write("<br>ConnString:" + connstring + " bidrefno=" + bidrefno + " deadline=" + deadline);

            SqlParameter[] sqlparams = new SqlParameter[2];
            sqlparams[0] = new SqlParameter("@BidRefNo", SqlDbType.Int);
            sqlparams[0].Value = bidrefno;
            sqlparams[1] = new SqlParameter("@RenegotiationDeadline", SqlDbType.DateTime);
            sqlparams[1].Value = deadline;

            SqlHelper.ExecuteNonQuery(connstring, CommandType.StoredProcedure, "sp_UpdateBidRenegotiationDeadline", sqlparams);
            sqlTransact.Commit();

            isSuccessful = true;
        }
        catch
        {
            sqlTransact.Rollback();
            isSuccessful = false;
        }
        finally
        {
            sqlConnect.Close();
        }
        return isSuccessful;
    }
    
}
