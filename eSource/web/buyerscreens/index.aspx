<%@ Page Language="c#" Inherits="EBid.WEB.buyer_screens.index" CodeFile="index.aspx.cs" %>

<%@ Register Src="../usercontrol/news_announcements_nav.ascx" TagName="news_announcements_nav" TagPrefix="uc1" %>
<%@ Register TagPrefix="EBid" TagName="TopNavHome" Src="~/WEB/usercontrol/Buyer/TopNavHome.ascx" %>
<%@ Register TagPrefix="EBid" TagName="TopNav2" Src="~/WEB/usercontrol/Buyer/TopNav2.ascx" %>
<%@ Register TagPrefix="EBid" TagName="LeftNavBids" Src="~/WEB/usercontrol/Buyer/LeftNavBids.ascx" %>
<%@ Register TagPrefix="EBid" TagName="GlobalLinksNav" Src="~/web/usercontrol/GlobalLinksNav.ascx" %>
<%@ Register TagPrefix="EBid" TagName="Footer" Src="~/web/usercontrol/Footer.ascx" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml" style="height: 100%;">
<head>
    <%@ Register TagPrefix="EBid" TagName="jqueryLoader" Src="../usercontrol/jQuery_loader.ascx" %>
    <EBid:jqueryLoader runat="server" ID="jqueryLoader1" />
    <title id="PageTitle" runat="server"></title>
    <meta http-equiv="Content-Language" content="en-us" />
    <meta http-equiv="Content-Type" content="text/html; charset=windows-1252" />
    <link rel="stylesheet" type="text/css" href='<%= "../themes/"+Session["configTheme"]+"/css/style.css" %>' />
    <link rel="stylesheet" type="text/css" href='<%="../themes/"+Session["configTheme"]+"/css/style_buyer.css" %>' />
    <script type="text/javascript" src="../include/util.js"></script>
    <link rel="stylesheet" href="jquery/themes/base/jquery-ui.css" />
<script src="../../jquery/ui/jquery-ui.js"></script>
</head>
<body onload="SetStatus();" text="#c0ea0e" style="height:100%">
    <div align="left">
        <form id="Form1" runat="server">
            <table border="0" cellpadding="0" cellspacing="0" width="100%" id="page">
                <tr height="137px">
                    <td valign="top">
                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td>
                                    <div align="left" id="masthead">
                                        <EBid:GlobalLinksNav runat="server" ID="GlobalLinksNav" />
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td style="background: black;">
                                    <EBid:TopNav2 runat="server" ID="TopNav2" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr valign="top">
                    <td class="content">
                        <table border="0" cellpadding="0" cellspacing="0" width="100%" height="100%">
                            <tr>
                                <td id="content">
                                    <div align="left">
                                        <h1 style="margin-top:0px; padding-top:10px;">                                            
                                            <asp:Label ID="lblUserName" runat="server"></asp:Label></h1>
                                        <script type="text/javascript">
                                            $(document).ready(function () {
                                                $("#accordion1").accordion({
                                                    
                                                    collapsible: false,
                                                    toggle: false,
                                                    heightStyle: "content",
                                                    active: 0,
                                                });
                                            });
</script>
<style type="text/css">
    #accordion1 h3 {
        margin-bottom:2px;
        color:black;
        font-size:14px;
        background-color:#67893F;
        color:#fff;
        padding: 5px 2px;
        font-weight:bold;
        cursor:pointer;
        margin-bottom:1px;
        margin-top:0px;
    }
        #accordion1 h3 a {
       text-decoration:none;
       color:#fff;

        }
       
</style>
                                        <div id="accordion1">
                                            
                                           
                                            <h3></h3>
                                            <div>
                                        <p style="color:#000">
                                            Listed below are the items that need your attention.</p>
                                        <table width="100%">
                                            <tr>
                                                <td valign="top" width="50%" style="padding-right: 10px;">
                                                    <h3>
                                                        Bid Events</h3>
                                                    <p style="padding-left: 10px;">
                                                        <a href="bids.aspx">Bid Event Drafts
                                                            <asp:Label runat="server" ID="lblCountDrafts" Text=""></asp:Label>
                                                        </a>
                                                        <br />
                                                        <a href="submittedbiditems.aspx" style="display:none">Bid Events For Approval
                                                            <asp:Label runat="server" ID="lblCountSubmitted" Text=""></asp:Label>
                                                        </a>
                                                        <a href="approvedbiditems.aspx">Approved Bid Events
                                                            <asp:Label runat="server" ID="lblCountApproved" Text=""></asp:Label>
                                                        </a>
                                                        <span style="display:none">
                                                        <br />
                                                        <a href="rejectedbiditems.aspx">Rejected Bid Events
                                                            <asp:Label runat="server" ID="lblCountRejected" Text=""></asp:Label>
                                                        </a>
                                                        </span>
                                                        <span style="display:none">
                                                        <br />
                                                        <a href="bidsreedit.aspx">Bid Events For Re-editing
                                                            <asp:Label runat="server" ID="lblCountReedit" Text=""></asp:Label>
                                                        </a>
                                                        </span>
                                                        <br />
                                                        <a href="bidinvitations.aspx">Bid Event Invitations                                                            
                                                        </a>
                                                    </p>
                                                    <h3>
                                                        Bid Items</h3>
                                                    <p style="padding-left: 10px;">
                                                        <a href="biditemsforconversion.aspx" style="display:none">Converted Bid Items
                                                            <asp:Label runat="server" ID="lblCountConvertedBidItems" Text=""></asp:Label>
                                                        </a>
                                                        <a href="withdrawnedbiditems.aspx">Withdrawn Bid Items
                                                            <asp:Label runat="server" ID="lblCountWithdrawnedBidItems" Text=""></asp:Label>
                                                        </a>
                                                        <br />
                                                        <a href="awardedbiditems.aspx">Awarded Bid Items
                                                            <asp:Label runat="server" ID="lblCountAwardedBidItems" Text=""></asp:Label>
                                                        </a>
                                                        <br />
                                                    </p>
                                                    

                                                    
                                        <p style="padding-left:15px;  display:none">
                                            <a href="bidseventsforopening.aspx">Bids Events For Opening
                                                <asp:Label runat="server" ID="lblCountBidsForOpening" Text="(0)"></asp:Label>
                                            </a>
                                            <br />
                                            <a href="bidsopened.aspx">Bids Events Opened
                                                <asp:Label runat="server" ID="lblCountBidsOpened" Text="(0)"></asp:Label>
                                            </a>
                                                    </p>

                                        <p style="padding-left:15px; display:none">
                                            <a href="bidawardingchecklist.aspx">Create BAC
                                            </a>
                                            <br />
                                            <a href="bacdrafts.aspx">Draft BAC
                                                <asp:Label runat="server" ID="lblCountBacDrafts" Text="(0)"></asp:Label></a>
                                            <br />
                                            <a href="bacendorsed.aspx">View Endorsed BAC
                                                <asp:Label runat="server" ID="lblCountBacEndorsed" Text="(0)"></asp:Label></a>
                                            <br />
                                            <a href="bacawarded.aspx">View Awarded BAC
                                                <asp:Label runat="server" ID="lblCountBacAwarded" Text="(0)"></asp:Label></a>
                                            <br />
                                            <a href="bacforclarifications.aspx">View BAC for Clarifications
                                                <asp:Label runat="server" ID="lblCountBacClarifications" Text="(0)"></asp:Label></a>
                                            <br />
                                            <a href="bacrejected.aspx">View Rejected BAC
                                                <asp:Label runat="server" ID="lblCountBacRejected" Text="(0)"></asp:Label></a>
                                                    </p>
             
                                                </td>                                                
                                                <td valign="top" width="50%" style="padding-left: 10px;">



                                                    <div style="display:none">
                                                    <h3>
                                                        Auction Events</h3>
                                                    <p style="padding-left: 10px;">
                                                        <a href="auctions.aspx">Auction Event Drafts
                                                            <asp:Label runat="server" ID="lblCountDraftsAuction" Text=""></asp:Label>
                                                        </a>
                                                        <br />
                                                        <a href="submittedauctionevents.aspx">Auction Events For Approval
                                                            <asp:Label runat="server" ID="lblCountAuctionEventsForApproval" Text=""></asp:Label>
                                                        </a>
                                                        <br />
                                                        <a href="approvedauctionevents.aspx">Approved Auction Events
                                                            <asp:Label runat="server" ID="lblCountApprovedAuctionEvents" Text=""></asp:Label>
                                                        </a>
                                                        <br />
                                                        <a href="rejectedauctionevents.aspx">Rejected Auction Events
                                                            <asp:Label runat="server" ID="lblCountRejectedAuctionEvents" Text=""></asp:Label>
                                                        </a>
                                                        <br />
                                                        <a href="auctionitemsforre-editing.aspx">Auction Events For Re-editing
                                                            <asp:Label runat="server" ID="lblCountAuctionsForReedit" Text=""></asp:Label>
                                                        </a>
                                                        <br />
                                                        <a href="auctioninvitations.aspx">Auction Event Invitations
                                                            <asp:Label runat="server" ID="lblCountConfirmedAuctionInvitations" Visible="false" Text=""></asp:Label>
                                                        </a>
                                                    </p>
                                                    <h3>
                                                        Auction Items</h3>
                                                    <p style="padding-left: 10px;">
                                                        <a href="awardedauctions.aspx">Awarded Auction Items
                                                            <asp:Label runat="server" ID="lblAwardedAuctionItems" Text=""></asp:Label>
                                                        </a>
                                                        <br /><br /><br />
                                                     </p>
                                                    <h3>
                                                        Auction Notifications</h3>
                                                    <p style="padding-left: 10px;">
                                                        <a href="../auctions/ongoingauctionevents.aspx">Ongoing Auction Events
                                                            <asp:Label runat="server" ID="lblCountOngoingAuctionEvents" Text=""></asp:Label>
                                                        </a>
                                                        <br />
                                                        <a href="../auctions/upcomingauctionevents.aspx">Upcoming Auction Events
                                                            <asp:Label runat="server" ID="lblCountUpcomingAuctionEvents" Text=""></asp:Label>
                                                        </a>
                                                        <br />
                                                        <a href="../auctions/finishedauctionevents.aspx">Finished Auction Events
                                                            <asp:Label runat="server" ID="lblCountFinishedAuctions" Text=""></asp:Label>
                                                        </a>
                                                    </p>
                                                    </div>
                                                    <p style="padding-left: 10px;  display:none">
                                            <a href="vsfcreate.aspx?create=new">Create
                                            </a>
                                            <br />
                                            <a href="vsfdrafts.aspx">Draft
                                                <asp:Label runat="server" ID="LabelVSF1" Text="(0)"></asp:Label></a>
                                            <br />
                                            <a href="vsfendorsed.aspx">View Endorsed
                                                <asp:Label runat="server" ID="LabelVSF2" Text="(0)"></asp:Label></a>
                                            <br />
                                            <a href="vsfapproved.aspx">View Approved
                                                <asp:Label runat="server" ID="LabelVSF4" Text="(0)"></asp:Label></a>
                                            <br />
                                            <a href="vsfclarifications.aspx">View Clarifications
                                                <asp:Label runat="server" ID="LabelVSF3" Text="(0)"></asp:Label></a>
                                            <br />
                                                    </p>

                                                   


                                                    <!--<img src="../../images/home_bid.jpg" usemap="#ImgMap0" border="0" style="height: 268px; width: 420px" />-->
                                                    <h3>
                                                        Bid Tenders</h3>
                                                    <p style="padding-left: 10px;">
                                                        <a href="bidsforeval.aspx">Received Bid Tenders
                                                            <asp:Label runat="server" ID="lblCountReceivedBidTenders" Text=""></asp:Label>
                                                        </a>
                                                        <br />
                                                        <a href="endorsedbidtenders.aspx">Endorsed Bid Tenders
                                                            <asp:Label runat="server" ID="lblCountEndorsedBidTenders" Text=""></asp:Label>
                                                        </a>
                                                        <a href="endorsedbidtenderswithpo.aspx" style="display:none ">Endorsed Bid Tenders PO
                                                            <asp:Label runat="server" ID="Label1" Text=""></asp:Label>
                                                        </a>
                                                        <br />
                                                        <!--<a href="bidsforrenegotiation.aspx">Bid Tenders For Clarification
                                                            <asp:Label runat="server" ID="lblCountBidsTendersForRenegotiation" Text=""></asp:Label>
                                                        </a>-->
                                                    </p>
                                                    <h3>PR</h3>
                                            <p style="padding-left:15px">
                                                <a href="viewPR.aspx">View PR
                                                    <asp:Label runat="server" ID="lblCountPR" Text="(0)"></asp:Label></a>
                                                <br />
                                            </p>

                                                     <h3>
                                                        Others</h3>
                                                    <p style="padding-left: 10px;">
                                                        <a href="changepwd.aspx">Change Password</a>
                                                    </p>
                                                </td>
                                            </tr>
                                        </table>
                                            
                                        <p>

                                            <br />
                                        </p>
                                               
                                            </div>
                                            </div>
                                    </div>
                                </td>
                                
                                <!--<td id="relatedInfo" style="background-color:#808080">
                                    <uc1:news_announcements_nav ID="News_announcements_nav1" runat="server" />
                                </td>-->
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td id="footer" height="50px">
                        <EBid:Footer runat="server" ID="Footer" />                        
                    </td>
                </tr>
            </table>
        </form>
    </div>

<map name="Map">
  <area shape="rect" coords="195, 10, 310, 53" href="createnewevent.aspx">
  <area shape="rect" coords="155, 73, 284, 130" href="submittedbiditems.aspx">
  <area shape="rect" coords="127, 155, 228, 212" href="bidsforeval.aspx">
</map>

<map name="Map2">
  <area shape="rect" coords="185, 7, 304, 48" href="#">
  <area shape="rect" coords="152, 73, 288, 132" href="#">
  <area shape="rect" coords="126, 154, 228, 210" href="#">
</map>
</body>
</html>
