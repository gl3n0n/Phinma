<%@ Page Language="c#" Inherits="EBid.web.vendor_screens.index" CodeFile="index.aspx.cs" %>

<%@ Register Src="../usercontrol/news_announcements_nav.ascx" TagName="news_announcements_nav" TagPrefix="uc1" %>
<%@ Register TagPrefix="EBid" TagName="GlobalLinksNav" Src="~/web/usercontrol/GlobalLinksNav.ascx" %>
<%@ Register TagPrefix="EBid" TagName="Footer" Src="~/web/usercontrol/Vendor/Footer.ascx"  %>
<%@ Register TagPrefix="EBid" TagName="TopDate" Src="~/web/usercontrol/TopDate.ascx" %>
<%@ Register TagPrefix="EBid" TagName="Vendor_TopNav_Home" Src="~/web/usercontrol/Vendor/Vendor_TopNav_Home.ascx" %>
<%@ Register TagPrefix="EBid" TagName="Vendor_LeftNav_Bids" Src="~/web/usercontrol/Vendor/Vendor_LeftNav_Bids.ascx" %>
<%@ Register TagPrefix="EBid" TagName="Vendor_LeftNav_Auctions" Src="~/web/usercontrol/Vendor/Vendor_LeftNav_Auctions.ascx" %>
<html xmlns="http://www.w3.org/1999/xhtml" style="height: 100%;">
<head>
    <%@ Register TagPrefix="EBid" TagName="jqueryLoader" Src="../usercontrol/jQuery_loader.ascx" %>
    <EBid:jqueryLoader runat="server" ID="jqueryLoader1" />
    <title id="PageTitle" runat="server"></title>
    <meta http-equiv="Content-Language" content="en-us" />
    <meta http-equiv="Content-Type" content="text/html; charset=windows-1252" />
    <link rel="stylesheet" type="text/css" href='<%= "../themes/"+Session["configTheme"]+"/css/style.css" %>' />
    <link rel="stylesheet" type="text/css" href='../themes/<%= Session["configTheme"] %>/css/style_v.css' />
    <script type="text/javascript" src="../include/util.js"></script>

    <link href='<%= "../themes/"+Session["configTheme"]+"/css/style.css" %>' rel="stylesheet" type="text/css" />
</head>
<body onload="SetStatus();">
    <div align="left">
        <form id="Form1" runat="server">
            <table border="0" cellpadding="0" cellspacing="0" id="page">
                <tr>
                    <td valign="top" height="137">
                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td>
                                    <div align="left" id="masthead">
                                        <EBid:GlobalLinksNav runat="server" ID="GlobalLinksNav" />
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <EBid:TopDate runat="server" ID="TopDate" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="content" valign="top">
                        <table border="0" cellpadding="0" cellspacing="0" width="100%" height="100%">
                            <tr>
                                <td id="relatedInfo">
                                    <EBid:Vendor_LeftNav_Bids runat="server" ID="LeftNav" />
                                    <!--<uc1:news_announcements_nav ID="News_announcements_nav1" runat="server" />-->
                                </td>
                                <td id="content">
                                    <div align="left">
                                        <h1 style="margin-top:0px; padding-top:5px;">
                                            <asp:Label runat="server" ID="lblVendorName"></asp:Label></h1>
                                        <p>
                                            Listed below are the items that need your attention.</p>
                                        <table width="100%">
                                            <tr>
                                                <td valign="top" width="50%" style="padding-right: 10px;">
                                                    <h3>
                                                        Bid</h3>
                                                    <p style="padding-left: 10px;">
                                                        <a href="bids.aspx">New Bid Events
                                                            <asp:Label runat="server" ID="lblBidEventsCount"></asp:Label>
                                                        </a>
                                                    </p>
                                                    <h3>
                                                        Bid Status</h3>
                                                    <p style="padding-left: 10px;">
                                                        <a href="AwardedBids.aspx">Awarded Bid Items
                                                            <asp:Label runat="server" ID="lblAwardedBidItemsCnt" /></a>
                                                    </p>
                                                    
                                                </td>
                                                <td valign="top" width="50%" style="padding-left: 10px;">
                                                    <h3>
                                                        Tenders</h3>
                                                    <p style="padding-left: 10px;">
                                                        <a href="tenderdrafts.aspx">Bid Tender Drafts
                                                            <asp:Label runat="server" ID="lblDraftBidTendersCount"></asp:Label>
                                                        </a>
                                                        <br />
                                                        <a href="submittedtenders.aspx">Submitted Bid Tenders
                                                            <asp:Label runat="server" ID="lblSubmittedBidTendersCount"></asp:Label>
                                                        </a>
                                                        <br />
                                                        <!--<a href="bidsforrenegotiation.aspx">Bid Tenders for Clarification
                                                            <asp:Label runat="server" ID="lblForRenegotiationBidTendersCount"></asp:Label>
                                                        </a>-->
                                                        <br />
                                                    </p>

                                                    <div style="display:none">
                                                    <h3>
                                                        Auction</h3>
                                                    <p style="padding-left: 10px;">
                                                        <a href="auctions.aspx">New Auction Events
                                                            <asp:Label runat="server" ID="lblNewAuctionCount" /></a>
                                                    </p>
                                                    <h3>
                                                        Auction Status</h3>
                                                    <p style="padding-left: 10px;">
                                                        <a href="awardedAuctions.aspx">Awarded Auction Items
                                                            <asp:Label runat="server" ID="lblAwardedAuctionsCnt" /></a>
                                                    </p>
                                                    <h3>
                                                        Auction Events</h3>
                                                    <p style="padding-left: 10px;">
                                                        <a href="../auctions/ongoingauctionevents.aspx">Ongoing Auction Events
                                                            <asp:Label runat="server" ID="lblOngoingAuctions" /></a><br />
                                                        <a href="../auctions/upcomingauctionevents.aspx">Upcoming Auction Events
                                                            <asp:Label runat="server" ID="lblUpcomingAuctions" /></a><br />
                                                        <a href="../auctions/finishedauctionevents.aspx">Finished Auction Events
                                                            <asp:Label runat="server" ID="lblFinishedAuctions" /></a>
                                                        </p>
                                                        </div>
                                                    <h3>
                                                        Others</h3>
                                                    <p style="padding-left: 10px;">
                                                        <a href="changepassword.aspx">Change Password</a><br />                                                        
                                                        <asp:LinkButton ID="lnkContactBuyer" runat="server" OnClick="lnkContactBuyer_Click">Contact Buyer</asp:LinkButton>
                                                    </p>
                                                </td>
                                            </tr>
                                        </table>
                                        <p>
                                            <br />
                                        </p>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td id="footer" height="50px">
                        <EBid:Footer runat="server" ID="Footer1" />
                    </td>
                </tr>
            </table>
        </form>
    </div>
</body>
</html>
