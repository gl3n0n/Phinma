using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using EBid.lib.auction.trans;
using EBid.lib.bid.trans;
using EBid.lib.utils;
using EBid.lib.constant;
using EBid.lib.auction.data;
using EBid.lib.bid.data;
using System.Xml;
using System.IO;
using EBid.lib;
using EBid.ConnectionString;
using EBid.ConnectionString;

public partial class web_buyer_screens_DraftAuctionDetails : System.Web.UI.Page
{
    private string connstring = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        FormsAuthenticationHelper.AuthenticateUserWithReturnUrl();
		connstring = Client.ConnectionString(HttpContext.Current.Session["clientid"].ToString());
        if (Int32.Parse(Session[Constant.SESSION_USERTYPE].ToString().Trim()) != (int)Constant.USERTYPE.BUYER)
            Response.Redirect("../unauthorizedaccess.aspx");

        if (!(IsPostBack))
        {
            Session[Constant.SESSION_COMMENT_TYPE] = "1";
            if (Request.QueryString["arn"] != null)
            {
                Session[Constant.SESSION_AUCTIONREFNO] = Request.QueryString["arn"].ToString().Trim();
            }

            if (Session[Constant.SESSION_AUCTIONREFNO] != null)
            {
                hdnAuctionRefNo.Value = Session[Constant.SESSION_AUCTIONREFNO].ToString().Trim();
            }
            else
            {
                Response.Redirect("auctions.aspx");
            }

            if (hdnAuctionRefNo.Value.Trim() != "")
            {
                hdnDetailNo.Value = GetAuctionItemForConversion(hdnAuctionRefNo.Value.ToString());
                AuctionTransaction au = new AuctionTransaction();
                AuctionItem ai = au.GetAuctionByAuctionRefNo(connstring, hdnAuctionRefNo.Value.Trim());
                //CompanyTransaction cmp = new CompanyTransaction();
                //GroupDeptSecTransaction grp = new GroupDeptSecTransaction();
                //CategoryTransaction cat = new CategoryTransaction();
                //OtherTransaction oth = new OtherTransaction();
                //lblCompany.Text = cmp.GetCompanyName(ai.CompanyId.ToString().Trim());
                //lblRequestor.Text = ai.Requestor.ToString().Trim();
                //lblPRNumber.Text = ai.PRRefNo.ToString().Trim();
                //lblPRDate.Text = ai.PRDate.ToString().Trim();
                //lblGroup.Text = grp.GetGroupDeptSecNameById(ai.GroupDeptSec.ToString().Trim());
                //lblSubCategory.Text = cat.GetCategoryNameById(ai.Category.ToString().Trim());
                //lblDeliveryDate.Text = ai.DeliveryDate.ToString().Trim();
                //lblItemDescription.Text = ai.ItemDescription.ToString().Trim();
                //lblReferenceNumber.Text = hdnAuctionRefNo.Value.Trim();

                lblAuctionType.Text = au.GetAuctionTypeNameById(connstring, ai.AuctionType.ToString().Trim());
                lblAuctionConfirmationDeadline.Text = ai.AuctionDeadline.ToString().Trim();
                lblAuctionEventDate.Text = ai.AuctionStartDate.ToString().Trim();
                lblAuctionStartTime.Text = ai.AuctionStartTimeHour.ToString().Trim() + ":" +
                                           ai.AuctionStartTimeMin.ToString().Trim() + ":" +
                                           ai.AuctionStartTimeSec.ToString().Trim() + " " +
                                           ai.AuctionStartTimeAMPM.ToString().Trim();
                lblAuctionEndTime.Text = ai.AuctionEndTimeHour.ToString().Trim() + ":" +
                                        ai.AuctionEndTimeMin.ToString().Trim() + ":" +
                                            ai.AuctionEndTimeSec.ToString().Trim() + " " +
                                            ai.AuctionEndTimeAMPM.ToString().Trim();
                
                //lblBidCurrency.Text = oth.GetBidCurrency(ai.BidCurrency.ToString().Trim());

                DataTable dtSuppliers = au.GetSuppliers(connstring, hdnAuctionRefNo.Value.Trim());
                if (dtSuppliers.Rows.Count > 0)
                {
                    string strSuppliers = "";
                    string strSupplierList = "";
                    for (int i = 0; i < dtSuppliers.Rows.Count; i++)
                    {
                        strSuppliers = dtSuppliers.Rows[i]["VendorId"].ToString().Trim();
                        if (strSupplierList == "")

                            strSupplierList = strSuppliers.Trim();
                        else
                            strSupplierList = strSupplierList.Trim() + "," + strSuppliers.Trim();
                    }
                    txtSuppliers.Text = strSupplierList.Trim();
                }
                else
                {
                    dtSuppliers = CreateEmptySupplier();
                    txtSuppliers.Text = "";
                }
                gvSuppliers.DataSource = dtSuppliers;
                gvSuppliers.DataBind();
            }
        }
    }

    private string GetAuctionItemForConversion(string auctionRefNo)
    {
        SqlParameter[] sqlParams = new SqlParameter[1];
        sqlParams[0] = new SqlParameter("@AuctionRefNo", SqlDbType.Int);
        sqlParams[0].Value = auctionRefNo;

        return SqlHelper.ExecuteScalar(connstring, CommandType.StoredProcedure, "sp_GetAuctionItemForConversion", sqlParams).ToString().Trim();
    }

    private void ChangeConversionStatus(int biddetailno, int status)
    {
        SqlConnection sqlConnect = new SqlConnection(connstring);
        SqlTransaction sqlTransact = null;

        try
        {
            sqlConnect.Open();
            sqlTransact = sqlConnect.BeginTransaction();

            SqlParameter[] sqlParams = new SqlParameter[2];
            sqlParams[0] = new SqlParameter("@Status", SqlDbType.Int);
            sqlParams[0].Value = status;
            sqlParams[1] = new SqlParameter("@BidDetailNo", SqlDbType.Int);
            sqlParams[1].Value = biddetailno;

            SqlHelper.ExecuteNonQuery(connstring, CommandType.StoredProcedure, "sp_UpdateBidItemConversionStatus", sqlParams);
            sqlTransact.Commit();
        }
        catch
        {
            sqlTransact.Rollback();
        }
        finally
        {
            sqlConnect.Close();
        }
    }

    private DataTable CreateEmptySupplier()
    {

        DataTable dt = new DataTable();
        DataColumn dcol = new DataColumn("Supplier", typeof(System.String));
        dt.Columns.Add(dcol);
        DataColumn dcol1 = new DataColumn("AccType", typeof(System.String));
        dt.Columns.Add(dcol1);

        DataRow dr = dt.NewRow();
        dr["Supplier"] = "";
        dr["AccType"] = "";
        dt.Rows.Add(dr);

        return dt;
    }

    private DataTable CreateEmptyTable()
    {
        DataTable dt = new DataTable();
        DataColumn dc = new DataColumn("AuctionDetailNo", typeof(System.String));
        dt.Columns.Add(dc);
        dc = new DataColumn("Item", typeof(System.String));
        dt.Columns.Add(dc);
        dc = new DataColumn("Description", typeof(System.String));
        dt.Columns.Add(dc);
        dc = new DataColumn("Quantity", typeof(System.String));
        dt.Columns.Add(dc);
        dc = new DataColumn("UnitOfMeasure", typeof(System.String));
        dt.Columns.Add(dc);
        DataRow dr = dt.NewRow();
        dr["AuctionDetailNo"] = "";
        dr["Item"] = "";
        dr["Description"] = "";
        dr["Quantity"] = "";
        dr["UnitOfMeasure"] = "";
        dt.Rows.Add(dr);
        return dt;
    }

   
    protected void btnEdit1_Click(object sender, EventArgs e)
    {
        Session["AuctionRefNo"] = hdnAuctionRefNo.Value.Trim();
        DeleteXMLFile();
        Session["XMLFile"] = null;
        Session["Suppliers"] = null;
        Session["FirstLoad"] = null;
        Response.Redirect("createauction.aspx");
    }

    private AuctionDetails GetAuctionItemDetails(int auctionrefno)
    {
        DataTable dt = SqlHelper.ExecuteDataset(connstring, "sp_GetAuctionInvitationInfo", new SqlParameter[] { new SqlParameter("@AuctionRefNo", auctionrefno) }).Tables[0];
        AuctionDetails item = new AuctionDetails();

        if (dt.Rows.Count > 0)
            item = new AuctionDetails(dt.Rows[0]);

        return item;
    }

    protected void btnSubmit1_Click(object sender, EventArgs e)
    {
        if (hdnDetailNo.Value.Length > 0)
        {
            AuctionTransaction au = new AuctionTransaction();
            au.SubmitApprovedAuction(connstring, hdnAuctionRefNo.Value.Trim());
            ChangeConversionStatus(int.Parse(hdnDetailNo.Value.Trim()), Constant.BIDITEM_STATUS.CONVERSION_STATUS.CONVERTED);

            // add participants immediately
            AuctionItemTransaction.InsertAuctionParticipants(connstring, hdnAuctionRefNo.Value.ToString().Trim());

            AuctionDetails details = GetAuctionItemDetails(int.Parse(hdnAuctionRefNo.Value.Trim()));
            ArrayList list = AuctionItemTransaction.GetAuctionParticipants(connstring, int.Parse(hdnAuctionRefNo.Value.Trim()));
            int failedcount = 0, successcount = 0;

            GetVendorsTender(int.Parse(hdnAuctionRefNo.Value.ToString()));
            SubmitAllTenders();
            SendEmailInvitation(details, list, ref failedcount, ref successcount);

            Response.Redirect("approvedauctionevents.aspx");
        }
        else
        {
            AuctionTransaction au = new AuctionTransaction();
            au.SubmitAnAuction(connstring, hdnAuctionRefNo.Value.Trim());

            Response.Redirect("auctionsubmit.aspx");
        }
    }
 
    private void DeleteXMLFile()
    {
        if (Session["XMLFile"] != null)
        {
            if (File.Exists(Session["XMLFile"].ToString().Trim()))
                File.Delete(Session["XMLFile"].ToString().Trim());
        }
    }

    private void GetVendorsTender(int vAuctionRefNo)
    {
        SqlParameter[] sqlparams = new SqlParameter[1];
        sqlparams[0] = new SqlParameter("@AuctionRefNo", SqlDbType.Int);
        sqlparams[0].Value = Int32.Parse(vAuctionRefNo.ToString().Trim());

        DataSet vendorData = SqlHelper.ExecuteDataset(connstring, CommandType.StoredProcedure, "sp_GetAllVendorsTender", sqlparams);

        DataTable vendorDataTable = vendorData.Tables[0];
        ViewState["VendorsTenderDetails"] = vendorDataTable;
    }

    private bool SubmitAllTenders()
    {
        bool isSuccessful = false;
        try
        {
            DataTable dtVendorsTenders = (DataTable)ViewState["VendorsTenderDetails"];
            string s = string.Empty;

            for (int i = 0; i < dtVendorsTenders.Rows.Count; i++)
            {
                s += SubmitBid(int.Parse(dtVendorsTenders.Rows[i]["AuctionDetailNo"].ToString()), int.Parse(dtVendorsTenders.Rows[i]["VendorId"].ToString()), float.Parse(dtVendorsTenders.Rows[i]["BidPrice"].ToString()));
            }
            isSuccessful = s.IndexOf("0") >= 0 ? false : true;
        }
        catch
        {
            isSuccessful = false;
        }
        return isSuccessful;
    }

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

            string str = SqlHelper.ExecuteScalar(sqlTransact, "sp_SubmitVendorsBid", sqlParams).ToString();

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

    protected void lnkOk_Click(object sender, EventArgs e)
    {
        Response.Redirect(Session[Constant.SESSION_LASTPAGE].ToString());
    }
    protected void lnkOk1_Click(object sender, EventArgs e)
    {
        Response.Redirect("index.aspx");
    }

    #region Email/SMS Invitation
    private string CreateInvitationBody(AuctionDetails auctiondetails, AuctionParticipant participant)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("<tr><td align='right'><h5>" + DateTime.Now.ToLongDateString() + "</h5></td></tr>");
        sb.Append("<tr><td align='center'><h3>INVITATION TO AUCTION</h3></td></tr>");
        sb.Append("<tr>");
        sb.Append("<td valign='top'>");
        sb.Append("<p>");
        sb.Append("<b>TO&nbsp&nbsp;:&nbsp&nbsp;<u>" + participant.Name + "</u></b>");
        sb.Append("<br /><br />");
        sb.Append("Good Day!");
        sb.Append("<br /><br />");
        sb.Append("We are glad to inform you that you have been invited to participate in an online auction event which was initiated by Trans-Asia  Incorporated.");
        sb.Append("</p>");

        sb.Append("<table style='font-size: 12px;width:100%;'>");
        sb.Append("<tr>");
        sb.Append("<td width='10px'></td>");
        sb.Append("<td style='font-weight:bold;width:20px;'>1.</td>");
        sb.Append("<td style='font-weight:bold;'>Auction Description</td>");
        sb.Append("</tr>");
        sb.Append("<tr>");
        sb.Append("<td width='30px' colspan='2'></td>");
        sb.Append("<td>" + auctiondetails.Description + "</td>");
        sb.Append("</tr>");
        sb.Append("<tr><td height='10px' colspan='3'></td></tr>");

        sb.Append("<tr>");
        sb.Append("<td width='10px'></td>");
        sb.Append("<td style='font-weight:bold;width:20px;'>2.</td>");
        sb.Append("<td style='font-weight:bold;'>Schedule of Auction Event</td>");
        sb.Append("</tr>");
        sb.Append("<tr>");
        sb.Append("<td width='30px' colspan='2'></td>");
        sb.Append("<td>");
        sb.Append("Confirmation Deadline : " + FormattingHelper.FormatDateToString(auctiondetails.ConfirmationDeadline) + "<br />");
        sb.Append("Start Date & Time : " + FormattingHelper.FormatDateToLongString(auctiondetails.StartDateTime) + "<br />");
        sb.Append("End Date & Time : " + FormattingHelper.FormatDateToLongString(auctiondetails.EndDateTime) + "<br />");
        sb.Append("Duration : " + auctiondetails.Duration + "<br />");
        sb.Append("</td>");
        sb.Append("</tr>");
        sb.Append("<tr><td height='10px' colspan='3'></td></tr>");

        sb.Append("<tr>");
        sb.Append("<td width='10px'></td>");
        sb.Append("<td style='font-weight:bold;width:20px;'>3.</td>");
        sb.Append("<td style='font-weight:bold;'>Payment Details</td>");
        sb.Append("</tr>");
        sb.Append("<tr>");
        sb.Append("<td width='30px' colspan='2'></td>");
        sb.Append("<td>");
        sb.Append("<ul>");
        sb.Append("<li>Payment Terms</li>");
        sb.Append("<ul><li>Trans-Asia  shall pay supplier 10% Down Payment, Progress Billing.</li></ul>");
        sb.Append("<br />");
        sb.Append("<li>Billing Details</li>");
        sb.Append("<ul>");
        sb.Append("<li>Contact Person: Rose Soteco T# 730 2413</li>");
        sb.Append("<li>Contact Details: 2F GT Plaza Tower 1, Pioneer cor Madison Sts., Mandaluyong City</li>");
        sb.Append("</ul>");
        sb.Append("</ul>");
        sb.Append("</td>");
        sb.Append("</tr>");
        sb.Append("<tr><td height='10px' colspan='3'></td></tr>");

        sb.Append("<tr>");
        sb.Append("<td width='10px'></td>");
        sb.Append("<td style='font-weight:bold;width:20px;'>4.</td>");
        sb.Append("<td style='font-weight:bold;'>Bid Price Details</td>");
        sb.Append("</tr>");
        sb.Append("<tr>");
        sb.Append("<td width='30px' colspan='2'></td>");
        sb.Append("<td>The bid price submitted by the supplier shall be exclusive of VAT.</td>");
        sb.Append("</tr>");
        sb.Append("<tr><td height='10px' colspan='3'></td></tr>");

        sb.Append("<tr>");
        sb.Append("<td width='10px'></td>");
        sb.Append("<td style='font-weight:bold;width:20px;'>5.</td>");
        sb.Append("<td style='font-weight:bold;'>Price Validity</td>");
        sb.Append("</tr>");
        sb.Append("<tr>");
        sb.Append("<td width='30px' colspan='2'></td>");
        sb.Append("<td>");
        sb.Append("The price quoted must be valid and firm for a period of 30 days.");
        sb.Append("No change in price quoted shall be allowed after bid submission unless negotiated by Trans-Asia .");
        sb.Append("</td.");
        sb.Append("</tr>");
        sb.Append("<tr><td height='10px' colspan='3'></td></tr>");

        sb.Append("<tr>");
        sb.Append("<td width='10px'></td>");
        sb.Append("<td style='font-weight:bold;width:20px;'>6.</td>");
        sb.Append("<td style='font-weight:bold;'>Price Confirmation</td>");
        sb.Append("</tr>");
        sb.Append("<tr>");
        sb.Append("<td width='30px' colspan='2'></td>");
        sb.Append("<td>");
        sb.Append("All participants must submit the price breakdown according to the final bid price submitted during the e-BID event not later than 24 hours after the e-BIDding event has ended.");
        sb.Append("The sum of the breakdown must be equal to the supplier's final bid price submitted during the e-BIDding event.");
        sb.Append("Any attempt to submit a breakdown which totals significantlly higher or lower than the final bid price submitted during the event may be subject to sanctions from Trans-Asia .");
        sb.Append("</td>");
        sb.Append("</tr>");
        sb.Append("<tr><td height='10px' colspan='3'></td></tr>");

        sb.Append("<tr>");
        sb.Append("<td width='10px'></td>");
        sb.Append("<td style='font-weight:bold;width:20px;'>7.</td>");
        sb.Append("<td style='font-weight:bold;'>Grounds for Invalidating Bids</td>");
        sb.Append("</tr>");
        sb.Append("<tr>");
        sb.Append("<td width='30px' colspan='2'></td>");
        sb.Append("<td>");
        sb.Append("A supplier's bid may be invalidated under any of the following circumstances:");
        sb.Append("<ul>");
        sb.Append("<li>Incomplete bid documents</li>");
        sb.Append("<li>Bid documents without bidder's signature</li>");
        sb.Append("<li>Late submission of hard copy of bid price breakdown</li>");
        sb.Append("</ul>");
        sb.Append("</td>");
        sb.Append("</tr>");
        sb.Append("<tr><td height='10px' colspan='3'></td></tr>");

        sb.Append("<tr>");
        sb.Append("<td width='10px'></td>");
        sb.Append("<td style='font-weight:bold;width:20px;'>8.</td>");
        sb.Append("<td style='font-weight:bold;'>Awarding of Bid</td>");
        sb.Append("</tr>");
        sb.Append("<tr>");
        sb.Append("<td width='30px' colspan='2'></td>");
        sb.Append("<td>");
        sb.Append("The lowest/highest bidder is not necessarily the winning bidder. Trans-Asia  shall not be bound to assign any reason for not accepting any bid or accepting it in part.");
        sb.Append("Bids are still subject to further ecaluation. Trans-Asia  shall award the winning supplier through a Purchase Order/Sales Order.");
        sb.Append("</td>");
        sb.Append("</tr>");
        sb.Append("<tr><td height='10px' colspan='3'></td></tr>");

        sb.Append("<tr>");
        sb.Append("<td width='10px'></td>");
        sb.Append("<td style='font-weight:bold;width:20px;'>9.</td>");
        sb.Append("<td style='font-weight:bold;'>Penalties (depends on the items to be purchased)</td>");
        sb.Append("</tr>");
        sb.Append("<tr>");
        sb.Append("<td width='30px' colspan='2'></td>");
        sb.Append("<td>");
        sb.Append("<ul>");
        sb.Append("<li>10K - 49.99K</li>");
        sb.Append("<li>50K - 99.99K</li>");
        sb.Append("<li>100K - 199.99K</li>");
        sb.Append("<li>200K - 299.99K</li>");
        sb.Append("<li>300K - 499.99K</li>");
        sb.Append("<li>500K - 999.99K</li>");
        sb.Append("<li>1M - 1.999M</li>");
        sb.Append("<li>2M - 19.999M</li>");
        sb.Append("<li>20M and above</li>");
        sb.Append("</ul>");
        sb.Append("</td>");
        sb.Append("</tr>");
        sb.Append("<tr><td height='10px' colspan='3'></td></tr>");
        sb.Append("</table>");

        sb.Append("<p>");
        sb.Append("To know more about this auction, click <a href='" + ConfigurationManager.AppSettings["ServerUrl"] + "web/auctions/auctiondetails.aspx?aid=" + HttpUtility.UrlEncode(EncryptionHelper.Encrypt(auctiondetails.ID.ToString())) + "' target='_blank'>here</a>. ");
        sb.Append("<br />");
        sb.Append("To confirm/decline your invitation, click <a href='" + ConfigurationManager.AppSettings["ServerUrl"] + "web/auctions/confirmauctionevent.aspx?aid=" + HttpUtility.UrlEncode(EncryptionHelper.Encrypt(auctiondetails.ID.ToString())) + "' target='_blank'>here</a>.");
        sb.Append("<br />");
        sb.Append("<a href='" + ConfigurationManager.AppSettings["ServerUrl"] + "rules.htm' target='_blank' title='Click here or copy the link'>Rules and Regulations</a> : " + ConfigurationManager.AppSettings["ServerUrl"] + "rules.htm");
        sb.Append("<br /><br />");
        sb.Append("######################################################################################<br />");
        sb.Append("&nbsp;Credentials:<br />");
        sb.Append("&nbsp;&nbsp;&nbsp;Username: " + participant.Username + "<br />");
        sb.Append("&nbsp;&nbsp;&nbsp;Ticket: " + EncryptionHelper.Decrypt(participant.EncryptedTicket) + "<br /><br />");
        sb.Append("&nbsp;Notes:<br />");
        sb.Append("&nbsp;&nbsp;&nbsp;Ticket and password are CASE SENSITIVE.<br />");
        sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Ticket is for confirming/declining/participating an auction.<br />");
        sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Ticket is different for each supplier for each auction event.<br />");
        sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Password is for login.<br />");
        sb.Append("&nbsp;&nbsp;&nbsp;Username is NOT CASE SENSITIVE.<br />");
        sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;If you don't know or forgot your password, go to eBid login page and click forgot password.<br />");
        sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Use the username provided. Click Send. Your password will be sent to this email address.<br />");
        sb.Append("######################################################################################<br />");
        sb.Append("<br /><br /><br />");
        sb.Append("Sincerely Yours,");
        sb.Append("<br /><br />");
        sb.Append(auctiondetails.Creator);
        sb.Append("<br /><br />");
        sb.Append("</p>");
        sb.Append("</td>");
        sb.Append("</tr>");

        return MailTemplate.IntegrateBodyIntoTemplate(sb.ToString());
    }

    private string CreateSMSInvitationBody(AuctionDetails auctiondetails, AuctionParticipant participant)
    {
        return String.Format("You are invited to participate in an auction event;Ref. No.:{0},initiated by Trans-Asia . Deadline: {2} Start Date: {1}", auctiondetails.ID, auctiondetails.StartDateTime.ToString("MM/dd/yyyy hh:mm tt"), auctiondetails.ConfirmationDeadline.ToString("MM/dd/yyyy"));
    }

    private bool SendEmailInvitation(AuctionDetails auctiondetails, ArrayList recipients, ref int failedcount, ref int successcount)
    {
        bool success = false;
        string subject = "Trans-Asia  : Invitation to Auction";
        failedcount = 0;
        successcount = 0;

        try
        {
            for (int i = 0; i < recipients.Count; i++)
            {
                AuctionParticipant p = (AuctionParticipant)recipients[i];

                if (!MailHelper.SendEmail(MailTemplate.GetDefaultSMTPServer(),
                        MailHelper.ChangeToFriendlyName(auctiondetails.Creator, auctiondetails.CreatorEmail),
                        MailHelper.ChangeToFriendlyName(p.Name, p.EmailAddress),
                        subject,
                        CreateInvitationBody(auctiondetails, p),
                        MailTemplate.GetTemplateLinkedResources(this)))
                {	// if sending failed					
                    failedcount++;
                    LogHelper.EventLogHelper.Log("Auction > Send Invitation : Sending Failed to " + p.EmailAddress, System.Diagnostics.EventLogEntryType.Error);
                }
                else
                {	// if sending successful
                    successcount++;
                    LogHelper.EventLogHelper.Log("Auction > Send Invitation : Email Sent to " + p.EmailAddress, System.Diagnostics.EventLogEntryType.Information);
                    // update sent mail count
                    SqlHelper.ExecuteNonQuery(connstring, "sp_SendEmailInvitation", new SqlParameter[] { new SqlParameter("@ParticipantId", p.ID) });
                }

                if (SMSHelper.AreValidMobileNumbers(p.MobileNo.Trim()))
                {
                    SMSHelper.SendSMS(new SMSMessage(CreateSMSInvitationBody(auctiondetails, p).Trim(), p.MobileNo.Trim())).ToString();
                }
            }
            success = true;

        }
        catch (Exception ex)
        {
            success = false;
            LogHelper.EventLogHelper.Log("Auction > Send Invitation : " + ex.Message, System.Diagnostics.EventLogEntryType.Error);
        }

        return success;
    }
    #endregion

}
