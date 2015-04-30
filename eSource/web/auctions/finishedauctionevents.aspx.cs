using System;
using System.Collections;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using EBid.lib.auction.trans;
using EBid.lib.constant;
using System.Data.Sql;
using System.Data.SqlClient;
using EBid.lib;
using EBid.ConnectionString;

public partial class web_onlineAuction_UpcomingAuctionEvents : System.Web.UI.Page
{
	private string connstring = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (HttpContext.Current.Session["clientid"] != null)
        {
            connstring = Client.ConnectionString(HttpContext.Current.Session["clientid"].ToString());
        }
        else
        {
            Response.Redirect("../../login.aspx");
        }
		FormsAuthenticationHelper.AuthenticateUserWithReturnUrl();

        PageTitle.InnerText = String.Format(Constant.TITLEFORMAT, "Finished Auction Events");

		if (Session["Message"] != null)
		{
			lblMessage.Text = Session["Message"].ToString();
			Session["Message"] = null;
		}

        pnl_buyerMenu.Visible = Session[Constant.SESSION_USERTYPE].ToString() == ((int)Constant.USERTYPE.BUYER).ToString() ?  true : false;

        if (!IsPostBack)
        {
            // set checkbox default checked value
            if (Session["ForConversion"] != null)
            {
                chkbuyeropts.Items[0].Selected = bool.Parse(Session["ForConversion"].ToString());
            }

            if (Session["Elapsed"] != null)
            {
                chkbuyeropts.Items[1].Selected = bool.Parse(Session["Elapsed"].ToString());
            }

            if (Session["Awarded"] != null)
            {
                chkbuyeropts.Items[2].Selected = bool.Parse(Session["Awarded"].ToString());
            }

            if (Session["Forward"] != null)
            {
                chkauctiontype.Items[0].Selected = bool.Parse(Session["Forward"].ToString());
            }

            if (Session["Reverse"] != null)
            {
                chkauctiontype.Items[1].Selected = bool.Parse(Session["Reverse"].ToString());
            }            
        }

        SetFilters();        
    }
    	
    public bool IsBuyer()
    {
        if (Session[Constant.SESSION_USERTYPE] != null)
            return Session[Constant.SESSION_USERTYPE].ToString() == ((int)Constant.USERTYPE.BUYER).ToString() ? true : false;
        return false;
    }    

    protected bool IsEndorsement(string Statusdesc)
    {
        if (Statusdesc.ToUpper() == "For Endorsement".ToUpper())
            return true;        
        else
            return false;
    }

	protected void lblAuctionEvents_Command(object sender, CommandEventArgs e)
	{
		Session["CurrentTab"] = "3";
		Session[Constant.SESSION_AUCTIONREFNO] = e.CommandArgument.ToString();
		Response.Redirect("auctionawards.aspx");		
	}

	protected void lnkEndorse_Command(object sender, CommandEventArgs e)
	{
		Session[Constant.SESSION_AUCTIONREFNO] = e.CommandArgument.ToString();
		Response.Redirect("endorseauction.aspx");
	}

	protected void gvAuctionEvents_RowCreated(object sender, GridViewRowEventArgs e)
	{
		if ((e.Row.RowType == DataControlRowType.DataRow) || (e.Row.RowType == DataControlRowType.Header))
		{
			e.Row.Cells[3].Visible = IsBuyer();
			e.Row.Cells[4].Visible = IsBuyer();
		}
	}    

    #region Filtering
    private void SetFilters()
    {
        if (Session[Constant.SESSION_USERTYPE].ToString() == ((int)Constant.USERTYPE.BUYER).ToString())
            dsFinishedAuctions.FilterExpression = String.Format("{0} AND {1}", SetTaskFilter(), SetAuctionTypeFilter());
        else
            dsFinishedAuctions.FilterExpression = String.Format("{0}", SetAuctionTypeFilter());

        gvAuctionEvents.DataBind();
    }

    private String SetTaskFilter()
    {
        String selFilter = "";
        Boolean nocheck_flag = true;

        for (int ind = 0; ind < chkbuyeropts.Items.Count; ind++)
        {
            if (chkbuyeropts.Items[ind].Selected == true)
            {
                nocheck_flag = false;
                Session["ForEndorsement"] = chkbuyeropts.Items[0].Selected.ToString();
                Session["Elapsed"] = chkbuyeropts.Items[1].Selected.ToString();
                Session["Awarded"] = chkbuyeropts.Items[2].Selected.ToString();
                Session["Failed"] = chkbuyeropts.Items[3].Selected.ToString();
                switch (chkbuyeropts.Items[ind].Value)
                {
                    case "0"://endorsement                                                
                        selFilter += selFilter == "" ? "'For Endorsement'"
                                                     : ",'For Endorsement'";
                        break;
                    case "1"://elapsed                                                
                        selFilter += selFilter == "" ? "'Elapsed'"
                                                     : ",'Elapsed'";
                        break;
                    case "2"://awarded                                                
                        selFilter += selFilter == "" ? "'Awarded'"
                                                     : ",'Awarded'";
                        break;
                    case "3"://awarded                                                
                        selFilter += selFilter == "" ? "'Failed'"
                                                     : ",'Failed'";
                        break;
                }
            }
        }

        selFilter = String.Format(" (StatusDesc IN ({0})) ", selFilter);

        if (nocheck_flag)
        {   //forces a "no records found..."
            selFilter = "(ApprovedCount = DetailsCount) and (ApprovedCount <> DetailsCount)";
        }

        return selFilter;
    }

    private String SetAuctionTypeFilter()
    {
        String selFilter = "";
        Boolean nocheck_flag = true;

        for (int ind = 0; ind < chkauctiontype.Items.Count; ind++)
        {
            if (chkauctiontype.Items[ind].Selected == true)
            {
                nocheck_flag = false;
                switch (chkauctiontype.Items[ind].Value)
                {
                    case "0"://Forward
                        Session["Forward"] = chkauctiontype.Items[0].Selected.ToString();
                        selFilter = "0";
                        break;
                    case "1"://Reverse.
                        Session["Reverse"] = chkauctiontype.Items[1].Selected.ToString();
                        selFilter += selFilter == "" ? "1" : ",1";
                        break;
                }
            }
        }
        
        selFilter = String.Format(" (AuctionType IN ({0})) ", selFilter);
        
        if (nocheck_flag)
        {
            selFilter = "(AuctionType IN (2)) ";
        }       

        return selFilter;
    }
    #endregion    
}
