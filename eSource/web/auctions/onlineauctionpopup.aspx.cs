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
using EBid.lib.bid.trans;
using EBid.lib.auction.data;
using EBid.lib.auction.trans;
using EBid.lib.constant;
using EBid.lib.user.trans;
using EBid.lib;
using System.Data.SqlClient;
using System.Text;
using EBid.ConnectionString;

public partial class web_onlineAuction_onlineauctionpopup : System.Web.UI.Page
{
    private int _count = 0;
    private string connstring = "";

    protected void Page_PreLoad(object sender, EventArgs e)
    {
		FormsAuthenticationHelper.AuthenticateUserWithReturnUrl();
		connstring = Client.ConnectionString(HttpContext.Current.Session["clientid"].ToString());
        if (Session[Constant.SESSION_USERID] == null)
            Response.Redirect("sessionexpired.aspx");

        PageTitle.InnerText = String.Format(Constant.TITLEFORMAT, "Online Auction");

        ProcessQs();

        hdnAID.Value = OnlineAuctionRefNo;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //pnlComments.Visible = Session[Constant.SESSION_USERTYPE].ToString() == ((int)Constant.USERTYPE.BUYER).ToString() ? true : false;

        string item = String.Format("ItemName:{0}", OnlineAuctionRefNo);        
        if (Session[item] != null)
            lblItemName.Text = Session[item].ToString();
        

        #region Check auction status : ongoing, stopped, finished
        SqlParameter[] sqlParam1 = new SqlParameter[1];
        sqlParam1[0] = new SqlParameter("@AuctionRefNo", SqlDbType.Int);
        sqlParam1[0].Value = OnlineAuctionRefNo;
        int auctionstat = (int)SqlHelper.ExecuteScalar(connstring, CommandType.StoredProcedure, "sp_GetAuctionStatus", sqlParam1);
        switch (auctionstat)
        {
            // if stopped, redirect to auction stopped page
            case 5:
                {
                    // close window
                    CloseWindow();
                    Session[Constant.SESSION_AUCTIONREFNO] = Session[Constant.SESSION_ONLINE_AUCTION];
                    Response.Redirect("auctionstopped.aspx?arn=" + OnlineAuctionRefNo);
                } break;
            // if finished, redirect to auction awards page
            case 6:
                {
                    //ClientScript.RegisterStartupScript(GetType(), "onunload", "<script>alert('Auction Finished!');</script>");
                    CloseWindow();
                    Session[Constant.SESSION_AUCTIONREFNO] = Session[Constant.SESSION_ONLINE_AUCTION];
                    Response.Redirect("auctionawards.aspx");
                } break;
            // if ongoing, continue
            case 4: break;
            // if not yet published, redirect to error page
            //default: { Response.Redirect("ongoingauctions.aspx"); } break;
        }
        #endregion

        #region Check user type
        switch (Int32.Parse(Session[Constant.SESSION_USERTYPE].ToString()))
        {
            //	 if vendor, verify , load settings for vendor
            case ((int)Constant.USERTYPE.VENDOR):
                {
                    relatedInfo.Visible = true;
                    vendorPanel.Visible = true;
                    creatorPanel.Visible = false;

                    // Check if current auction ref no is valid (status=ongoing)
                    int stat = GetAuctionStatus();
                    //   if not, redirect back to ongoing auctions
                    switch (stat)
                    {
                        case 0:
                            {
                                Server.Transfer("confirmauctionevent.aspx?aid=" + OnlineAuctionRefNo);
                            } break;
                        case 1:
                            {
                                Server.Transfer("participateauctionevent.aspx?" + Request.QueryString);
                            } break;
                        case 2:
                            {
                                Response.Redirect("ongoingauctionevents.aspx");
                            } break;
                        case 3: break;
                    }

                } break;
            //	 if buyer
            case ((int)Constant.USERTYPE.BUYER):
                {
                    //Response.AppendHeader("Refresh", "5");

                    // load settings, view only. exclusive rights for creator(can stop the auction)
                    // check if current user is the creator of the auction
                    if (IsAuctionCreator())
                    // if so, show creator panel
                    {
                        relatedInfo.Visible = true;
                        vendorPanel.Visible = false;
                        creatorPanel.Visible = true;
                    }
                    // if not, hide creator panel
                    else
                    {
                        relatedInfo.Visible = false;
                        vendorPanel.Visible = false;
                        creatorPanel.Visible = false;
                    }
                } break;
            // if purchasing
            case ((int)Constant.USERTYPE.PURCHASING):
                {
                    //Response.AppendHeader("Refresh", "5");

                    // load settings, view only
                    relatedInfo.Visible = false;
                } break;
        }
        #endregion

        #region Check Message
        if (Session["Message"] != null)
        {
            lblMessage.Text = Session["Message"].ToString();
            Session["Message"] = null;
        }
        #endregion

        #region Check Auto Reload
        if (!Page.IsPostBack)
        {
            if (Session["AutoReload"] != null)
                chkAutoReload.Checked = bool.Parse(Session["AutoReload"].ToString());
            else
                chkAutoReload.Checked = false;
        }
        if (chkAutoReload.Checked)
        {
            Response.AppendHeader("Refresh", "10");
        }
        Session["AutoReload"] = chkAutoReload.Checked ? "true" : "false";
        #endregion

        // Get number of seconds elapsed and number of seconds remaining for this auction
        //string secRemaining = "0";
        //string secElapsed = "0";
        //string secDuration = "0";

        //GetSecondsElapsedAndRemaining(ref secRemaining, ref secElapsed, ref secDuration);

        //ClientScript.RegisterStartupScript(GetType(), "", "<script>var s = " + secRemaining + ";var se = " + secElapsed + ";var sd = " + secDuration + "; </script>");
    }

    #region Event Methods
    protected void gvAuctionEvent_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.Cells[1] != null)
                e.Row.Cells[1].Width = Unit.Pixel(130);
            if (e.Row.RowIndex == 1)
            {
                for (int i = 0; i < e.Row.Cells.Count; i++)
                    if (i != 1)
                    {
                        e.Row.Cells[i].Font.Bold = true;
                        e.Row.Cells[i].Font.Size = FontUnit.Point(11);
                        e.Row.Cells[i].ForeColor = System.Drawing.Color.DarkBlue;
                    }
            }
            if (e.Row.RowIndex == 2)
            {
                for (int i = 0; i < e.Row.Cells.Count; i++)
                    if (i != 1)
                    {
                        e.Row.Cells[i].Font.Bold = true;
                        e.Row.Cells[i].Font.Size = FontUnit.Point(11);
                    }
            }
            for (int i = 0; i < e.Row.Cells.Count; i++)
            {
                if (i != 1)
                {
                    e.Row.Cells[i].BackColor = System.Drawing.Color.White;
                    if (i != e.Row.Cells.Count - 1)
                        e.Row.Cells[i].Width = Unit.Pixel(120);
                }
            }
        }
    }

    protected void gvAuctionLastBids_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.Cells[0] != null)
                e.Row.Cells[0].Width = Unit.Pixel(130);
            for (int i = 0; i < e.Row.Cells.Count; i++)
            {
                if (i != 0)
                {
                    e.Row.Cells[i].BackColor = System.Drawing.Color.White;
                    if (i != e.Row.Cells.Count - 1)
                        e.Row.Cells[i].Width = Unit.Pixel(120);
                }
            }
        }
    }

    protected void gvBidInput_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            TextBox tb1 = (TextBox)(e.Row.FindControl("txtLeft"));
            TextBox tb2 = (TextBox)(e.Row.FindControl("txtRight"));

            // add extra attributes
            if (tb2 != null)
            {
                // numbers only
                tb2.Attributes.Add("onkeypress", "return NumbersOnly(event);");
                // reset if blank
                tb2.Attributes.Add("onfocusout", "ResetIfEmpty(this);");
                tb2.Attributes["id"] = "txtRight" + e.Row.RowIndex;
            }               
            
            // add extra attributes
            if (tb1 != null)
            {
                // right aligned
                tb1.Attributes.Add("style", "text-align:right;");
                // numbers only
                //tb1.Attributes.Add("onkeypress", "return NumbersOnly(event, 'gvBidInput_ctl" + (e.Row.RowIndex + 2) + "_txtRight');");                
                tb1.Attributes.Add("onkeypress", "return(currencyFormatNoDec(this,',','.',event))");
            }            
        }
    }

    protected void dsAuctionedItemBidHistory_Selected(object sender, SqlDataSourceStatusEventArgs e)
    {
        if (e.AffectedRows > 0)
        {
            trHistory.Visible = true;
            trEmptyHistory.Visible = false;
        }
        else
        {
            trHistory.Visible = false;
            trEmptyHistory.Visible = true;
        }
    }

    protected void dsAuctionItemDetails2_Selected(object sender, SqlDataSourceStatusEventArgs e)
    {
        try
        {
            e.Command.Connection.Open();
            System.Data.Common.DbDataReader dr = e.Command.ExecuteReader(CommandBehavior.SingleRow);
            if (dr.HasRows)
            {
                if (dr.Read())
                {
                    lblUserName.Text = "Welcome " + dr.GetValue(0).ToString() + "!";
                    lblAuctionTitle.Text = dr.GetValue(2).ToString();
                    Session["AuctionType"] = dr.GetValue(9).ToString();
                }
            }
        }
        catch { }
        finally { e.Command.Connection.Close(); }
    }

    protected void dsAuctionedItemBidHistory_Selecting(object sender, SqlDataSourceSelectingEventArgs e)
    {
        string adn = String.Format("AuctionDetailNo:{0}", OnlineAuctionRefNo);

        System.Data.Common.DbParameter p = e.Command.CreateParameter();
        p.ParameterName = "AuctionDetailNo";
        p.Value = Session[adn] == null ? "0" : Session[adn].ToString();
        e.Command.Parameters.Add(p);
    }

    protected void gvAuctionEvent_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        e.Row.Cells[0].Visible = false;
    }

    protected void lnkBidHistory_Command(object sender, CommandEventArgs e)
    {
        String[] tmpstr = e.CommandArgument.ToString().Split('»');
        if (tmpstr.Length > 1)
        {
            string adn = String.Format("AuctionDetailNo:{0}", OnlineAuctionRefNo);
            string item = String.Format("ItemName:{0}", OnlineAuctionRefNo);
            Session[adn] = tmpstr[0];
            Session[item] = tmpstr[1];            
        }        
        Response.Redirect(String.Format("onlineauctionpopup.aspx?{0}", Request.QueryString));
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {

        bool ok = false;
        int successcount = 0;
        int failedcount = 0;
        int invalidinputs = 0;

        ArrayList bids = null;
        //try
        //{
        bids = GetBids(ref successcount, ref invalidinputs);

        if (successcount + invalidinputs > 0)
            ok = true;
        else
            ok = false;
        //}
        //catch
        //{
        //    ok = false;
        //}

        if (ok)
        {
            successcount = failedcount = 0;

            for (int i = 0; i < bids.Count; i++)
            {
                Bid bid = (Bid)bids[i];
                string result = SubmitBid(bid.AuctionDetailNo, Int32.Parse(Session[Constant.SESSION_USERID].ToString()), bid.BidPrice);

                switch (result)
                {
                    case "11":
                        {
                            successcount++;
                            Session["Message" + bid.AuctionDetailNo] = "Black";
                        } break;
                    case "12":
                        {
                            successcount++;
                            Session["Message" + bid.AuctionDetailNo] = "Black";
                        } break;
                    case "21":
                        {
                            failedcount++;
                            Session["Message" + bid.AuctionDetailNo] = "Red";
                        } break;
                    case "22":
                        {
                            failedcount++;
                            Session["Message" + bid.AuctionDetailNo] = "Red";
                        } break;
                    default:
                        {
                            failedcount++;
                            Session["Message" + bid.AuctionDetailNo] = "Red";
                        } break;
                }
            }

            Session["Message"] = "<font color='black'>Successful Bid Submission(s) : " + successcount + " / " + (bids.Count + invalidinputs) + "</font><br />" +
                                 "<font color='red'>Failed Bid Submission(s) : " + failedcount + " / " + (bids.Count + invalidinputs) + "</font><br />" +
                                 "<font color='blue'>Invalid Bid Submission(s) : " + invalidinputs + " / " + (bids.Count + invalidinputs) + "</font>";


        }
        else
        {
            Session["Message"] = "Please enter your bid.";
        }
        Session.Add("ticket", Session.SessionID);
        Response.Redirect(String.Format("onlineauctionpopup.aspx?{0}", Request.QueryString));
    }

    protected void btnStop_Click(object sender, ImageClickEventArgs e)
    {
        if (IsValid)
        {
            if (StopAuction(Int32.Parse(OnlineAuctionRefNo)))
            {	// if successful	

                UserTransaction.InsertItemComments(connstring, OnlineAuctionRefNo, txtComment.Text.Trim(), Session[Constant.SESSION_USERID].ToString().Trim(), int.Parse(Session[Constant.SESSION_USERTYPE].ToString().Trim()), true, true);

                Session[Constant.SESSION_AUCTIONREFNO] = Session[Constant.SESSION_ONLINE_AUCTION];
                Response.Redirect("auctionstopped.aspx?arn=" + OnlineAuctionRefNo);
            }
            else
            {	// failed to stop, likely to happen
            }
        }
    }
    #endregion

    #region Custom Methods
    /// <summary>
    /// Submit Bid
    /// </summary>
    /// <param name="auctiondetailno"></param>
    /// <param name="vendorid"></param>
    /// <param name="bidprice"></param>
    /// <returns>
    /// A.) FAILED
    ///    + 21 - Bid was not entered succesfully, bidding has ended already
    ///    + 22 - Bid was not entered succesfully, bid already exists
    ///
    /// B.) SUCCESS
    ///    + 11 - Bid was entered succesfully
    ///    + 12 - Bid was entered succesfully, & time was extended
    /// </returns>
    public string SubmitBid(int auctiondetailno, int vendorid, float bidprice)
    {
        SqlConnection sqlConnect = new SqlConnection(connstring);
        SqlTransaction sqlTransact = null;
        string success = "00";

        try
        {
            sqlConnect.Open();
            sqlTransact = sqlConnect.BeginTransaction();

            SqlParameter[] sqlParams = new SqlParameter[3];
            sqlParams[0] = new SqlParameter("@AuctionDetailNo", SqlDbType.Int);
            sqlParams[1] = new SqlParameter("@Vendorid", SqlDbType.Int);
            sqlParams[2] = new SqlParameter("@Bid", SqlDbType.Money);
            sqlParams[0].Value = auctiondetailno;
            sqlParams[1].Value = vendorid;
            sqlParams[2].Value = bidprice;

            string str = SqlHelper.ExecuteScalar(sqlTransact, "sp_SubmitBid", sqlParams).ToString();

            sqlTransact.Commit();

            if (str.Trim() != "")
            {
                success = str.Trim();
            }
        }
        catch
        {
            sqlTransact.Rollback();
            success = "00";
        }
        finally
        {
            sqlConnect.Close();
        }
        return success;
    }

    public ArrayList GetBids(ref int successcount, ref int failedcount)
    {
        ArrayList bidlist = new ArrayList();
        successcount = failedcount = 0;

        for (int i = 0; i < gvBidInput.Rows.Count; i++)
        {
            string input = "";
            // check if txtbox has input

            TextBox tb1 = (TextBox)gvBidInput.Rows[i].FindControl("txtLeft");
            if (tb1 != null)
                input = String.IsNullOrEmpty(tb1.Text.Trim()) ? "0" : tb1.Text.Trim();

            TextBox tb2 = (TextBox)gvBidInput.Rows[i].FindControl("txtRight");
            if (tb2 != null)
                input += String.IsNullOrEmpty(tb2.Text.Trim()) ? ".00" : "." + tb2.Text.Trim();

            if (input != "0.00") // item has a bid
            {
                // check bid
                string auctiontype = Session["AuctionType"].ToString().Trim();
                float nextbid = float.Parse(gvBidInput.DataKeys[i].Values[1].ToString());
                float incdec = float.Parse(gvBidInput.DataKeys[i].Values[2].ToString());
                float input2 = float.Parse(input);

                if (auctiontype == "Forward") // forward, equal to nextbid or more than but divisible by increment
                {
                    if (input2 >= nextbid)
                    {
                        float tocheck = input2 - nextbid;
                        tocheck = (float)Convert.ToInt32(tocheck * 100);
                        incdec = (float)Convert.ToInt32(incdec * 100);

                        if ((tocheck % incdec) == 0) // divisible by increment increment
                        {
                            bidlist.Add(new Bid(Int32.Parse(gvBidInput.DataKeys[i].Value.ToString()), float.Parse(input)));
                            successcount++;
                        }
                        else
                        {
                            //throw new InvalidInputException("Make sure your input is a multiple of " + incdec);
                            failedcount++;
                            Session["Message" + Int32.Parse(gvBidInput.DataKeys[i].Value.ToString())] = "Blue";
                        }
                    }
                    else
                    {
                        //throw new InvalidInputException("Input must be more than " + nextbid);
                        failedcount++;
                        Session["Message" + Int32.Parse(gvBidInput.DataKeys[i].Value.ToString())] = "Blue";
                    }
                }
                else if (auctiontype == "Reverse") // reverse, equal to nextbid or less than but divisible by decrement
                {
                    if (input2 <= nextbid)
                    {
                        float tocheck = nextbid - input2;
                        if (tocheck < 1)
                        {
                            tocheck = (float)Convert.ToInt32(tocheck * 100);
                            incdec = (float)Convert.ToInt32(incdec * 100);
                        }
                        else 
                        {
                            tocheck = (float)Convert.ToInt32(tocheck * 100);
                            incdec = (float)Convert.ToInt32(incdec * 100);
                        }

                        if ((tocheck % incdec) == 0) // divisible by increment decrement
                        {
                            bidlist.Add(new Bid(Int32.Parse(gvBidInput.DataKeys[i].Value.ToString()), float.Parse(input)));
                            successcount++;
                        }
                        else
                        {
                            //throw new InvalidInputException("Make sure your input is a multiple of " + incdec);
                            failedcount++;
                            Session["Message" + Int32.Parse(gvBidInput.DataKeys[i].Value.ToString())] = "Blue";
                        }
                    }
                    else
                    {
                        //throw new InvalidInputException("Input must be less than " + nextbid);
                        failedcount++;
                        Session["Message" + Int32.Parse(gvBidInput.DataKeys[i].Value.ToString())] = "Blue";
                    }
                }
            }
        }

        return bidlist;
    }

    private bool StopAuction(int auctionrefno)
    {
        SqlConnection sqlConnect = new SqlConnection(connstring);
        SqlTransaction sqlTransact = null;
        bool success = false;

        try
        {
            sqlConnect.Open();
            sqlTransact = sqlConnect.BeginTransaction();

            SqlParameter[] sqlParams = new SqlParameter[1];
            sqlParams[0] = new SqlParameter("@AuctionRefNo", SqlDbType.Int);
            sqlParams[0].Value = auctionrefno;

            SqlHelper.ExecuteNonQuery(sqlTransact, "sp_StopAuction", sqlParams);

            sqlTransact.Commit();

            success = true;
        }
        catch
        {
            sqlTransact.Rollback();
            success = false;
        }
        finally
        {
            sqlConnect.Close();
        }
        return success;
    }

    private int GetAuctionStatus()
    {
        SqlParameter[] sqlParam = new SqlParameter[2];
        sqlParam[0] = new SqlParameter("@AuctionRefNo", SqlDbType.Int);
        sqlParam[1] = new SqlParameter("@VendorId", SqlDbType.Int);
        sqlParam[0].Value = OnlineAuctionRefNo;
        sqlParam[1].Value = Session[Constant.SESSION_USERID].ToString();

        // Check if current auction ref no is valid (status=ongoing)
        return (int)SqlHelper.ExecuteScalar(connstring, CommandType.StoredProcedure, "sp_GetAuctionParticipationStatus", sqlParam);
    }

    public bool IsAuctionCreator()
    {
        SqlParameter[] sqlParam = new SqlParameter[2];
        sqlParam[0] = new SqlParameter("@AuctionRefNo", SqlDbType.Int);
        sqlParam[1] = new SqlParameter("@BuyerId", SqlDbType.Int);
        sqlParam[0].Value = OnlineAuctionRefNo;
        sqlParam[1].Value = Session[Constant.SESSION_USERID].ToString();

        // Check if current user is the creator of the auction
        return (int)SqlHelper.ExecuteScalar(connstring, CommandType.StoredProcedure, "sp_ConfirmIsUserAuctionCreator", sqlParam) == 1 ? true : false;
    }

    public string Format(string date)
    {
        return Convert.ToDateTime(date).ToLongDateString() + " " + Convert.ToDateTime(date).ToString("HH:mm:ss tt");
    }

    public string GetLatestBid(string bid, string datesubmitted)
    {
        if (_count == 0)
            lblLatestBid.Text = bid;

        _count++;

        return String.Format("<b>{0}</b> [{1}]", bid, Convert.ToDateTime(datesubmitted).ToString("HH:mm:ss tt"));
    }

    public System.Drawing.Color ItemColor(string auctiondetailno)
    {
        System.Drawing.Color r = System.Drawing.Color.Navy;


        if (Session["Message" + auctiondetailno] != null)
        {
            switch (Session["Message" + auctiondetailno].ToString().ToUpper())
            {
                case "RED": r = System.Drawing.Color.Red; break;
                case "BLACK": r = System.Drawing.Color.Black; break;
                case "BLUE": r = System.Drawing.Color.Blue; break;
            }
            Session["Message" + auctiondetailno] = null;
        }
        return r;
    }

    public void GetSecondsElapsedAndRemaining(ref string strRemaining, ref string strElapsed, ref string strDuration)
    {
        SqlDataReader dr;
        try
        {
            dr = SqlHelper.ExecuteReader(connstring, "sp_GetAuctionElapsedAndRemainingTime", new SqlParameter[] { new SqlParameter("@AuctionRefNo", OnlineAuctionRefNo) });
            if (dr.HasRows)
            {
                dr.Read();
                strRemaining = dr["SecondsRemaining"].ToString();
                strElapsed = dr["SecondsElapsed"].ToString();
                strDuration = dr["SecondsDuration"].ToString();
            }
            else
            {
                strRemaining = "0";
                strElapsed = "0";
                strDuration = "0";
            }
        }
        catch
        {
        }
    }
    #endregion

    public class Bid
    {
        int _auctiondetailno = 0;
        float _bidprice = 0.00F;

        public Bid() { }

        public Bid(int auctiondetailno, float bidprice)
        {
            AuctionDetailNo = auctiondetailno;
            BidPrice = bidprice;
        }

        public int AuctionDetailNo
        {
            get { return _auctiondetailno; }
            set { _auctiondetailno = value; }
        }

        public float BidPrice
        {
            get { return _bidprice; }
            set { _bidprice = value; }
        }
    }

    protected void lnkbreload_Command(object sender, EventArgs e)
    {
        //lnkreload.NavigateUrl = String.Format("onlineauctionpopup.aspx?{0}", Request.QueryString);
        Session.Add("ticket", Session.SessionID);
        Response.Redirect(String.Format("onlineauctionpopup.aspx?{0}", Request.QueryString));
    }

    protected string GetStringValue(object tmpobj)
    {
        return tmpobj != null ? tmpobj.ToString() : "";
    }

    public bool UpdateStatus(int auctionrefno, int vendorid, int task, string ticket)
    {
        string connstring = HttpContext.Current.Session["ConnectionString"].ToString();
        SqlConnection sqlConnect = new SqlConnection(connstring);
        SqlTransaction sqlTransact = null;
        bool success = false;

        try
        {
            sqlConnect.Open();
            sqlTransact = sqlConnect.BeginTransaction();

            SqlParameter[] sqlParams = new SqlParameter[4];
            sqlParams[0] = new SqlParameter("@AuctionRefNo", SqlDbType.Int);
            sqlParams[1] = new SqlParameter("@Vendorid", SqlDbType.Int);
            sqlParams[2] = new SqlParameter("@Task", SqlDbType.Int);
            sqlParams[3] = new SqlParameter("@Ticket", SqlDbType.NVarChar);
            sqlParams[0].Value = auctionrefno;
            sqlParams[1].Value = vendorid;
            sqlParams[2].Value = task;
            sqlParams[3].Value = ticket;

            int i = (int)SqlHelper.ExecuteScalar(sqlTransact, CommandType.StoredProcedure, "sp_SetVendorAuctionParticipationStatus", sqlParams);

            sqlTransact.Commit();

            if (i == 1)
                success = true;
        }
        catch
        {
            sqlTransact.Rollback();
            success = false;
        }
        finally
        {
            sqlConnect.Close();
        }
        return success;
    }

    private void ProcessQs()
    {
        try
        {
            if (OnlineAuctionRefNo == null)
            {   // query string is empty / invalid
                CloseWindow();
            }
            else if (!HasOnlineAuctionTicket)
            {   // session is invalid
                if (Session[Constant.SESSION_USERTYPE].ToString() != "2")
                {
                    Session.Add(String.Format("AUCTIONTICKET:{0}", OnlineAuctionRefNo), Session.SessionID);
                }
                else
                {
                    Response.Redirect("participateauctionevent.aspx?" + Request.QueryString);
                }
            }
        }
        catch
        {
            CloseWindow();
        }
    }

    private void CloseWindow()
    {
        if (!ClientScript.IsClientScriptBlockRegistered("CloseWindow"))
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(),
                "CloseWindow",
                "<script type='text/javascript'> window.close(); </script>");
        }
    }

    private string OnlineAuctionRefNo
    {
        get
        {
            return FormattingHelper.DecryptQueryString(HttpUtility.UrlDecode(Request.QueryString["qs"].Trim()));
        }
    }

    private string OnlineAuctionTicket
    {
        get
        {
            if (HasOnlineAuctionTicket)
                return Session[String.Format("AUCTIONTICKET:{0}", OnlineAuctionRefNo)].ToString();
            else
                return null;
        }
    }

    private bool HasOnlineAuctionTicket
    {
        get
        {
            if (OnlineAuctionRefNo == null)
                return false;
            else
            {
                if (Session[String.Format("AUCTIONTICKET:{0}", OnlineAuctionRefNo)] == null)
                    return false;
                else
                {
                    if (Session[String.Format("AUCTIONTICKET:{0}", OnlineAuctionRefNo)].ToString() != Session.SessionID)
                        return false;
                    else
                        return true;
                }
            }
        }
    }

    protected void cvValidator_ServerValidate(object source, ServerValidateEventArgs args)
    {
        args.IsValid = (txtComment.Text.Trim().Length > 0);
    }

    protected void tmrAutoReload_Tick(object sender, EventArgs e)
    {
        //dsAuctionItemDetails.DataBind();
        gvAuctionEvent.DataBind();
        //dsAuctionLatestBids.DataBind();
        gvAuctionLastBids.DataBind();
        //dsHighesLowestBids.DataBind();
        gvAuctionHighestLowestBids.DataBind();
    }

    protected void tmrRefreshTime_Tick(object sender, EventArgs e)
    {
        dvAuctionDetails.DataBind();
        //dsAuctionItemDetails2.DataBind();
    }
}