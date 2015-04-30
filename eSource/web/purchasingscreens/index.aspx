<%@ Page Language="c#" EnableEventValidation="true" Inherits="EBid.WEB.purchasing_screens.index" CodeFile="index.aspx.cs" %>

<%@ Register Src="../usercontrol/news_announcements_nav.ascx" TagName="news_announcements_nav"
    TagPrefix="uc1" %>
<%@ Register TagPrefix="EBid" TagName="TopDate" Src="~/WEB/usercontrol/TopDate.ascx" %>
<%@ Register TagPrefix="EBid" TagName="GlobalLinksNav" Src="~/web/usercontrol/GlobalLinksNav.ascx" %>
<%@ Register TagPrefix="EBid" TagName="Purchasing_LeftNav" Src="~/web/usercontrol/Purchasing/Purchasing_LeftNav_Bids.ascx" %>
<%@ Register TagPrefix="EBid" TagName="Purchasing_TopNav_Home" Src="~/web/usercontrol/Purchasing/Purchasing_TopNav_Home.ascx" %>
<%@ Register TagPrefix="EBid" TagName="Purchasing_LeftNav_Home" Src="~/web/usercontrol/Purchasing/Purchasing_LeftNav_Home.ascx" %>
<%@ Register TagPrefix="EBid" TagName="Purchasing_LeftNav_Home2" Src="~/web/usercontrol/Purchasing/Purchasing_LeftNav_Home2.ascx" %>
<%@ Register TagPrefix="EBid" TagName="Footer" Src="~/web/usercontrol/Purchasing/Footer.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml" style="height: 100%;">
<head>
<%@ Register TagPrefix="EBid" TagName="jqueryLoader" Src="../usercontrol/jQuery_loader.ascx" %>
    <EBid:jqueryLoader runat="server" ID="jqueryLoader1" />
    <title id="PageTitle" runat="server"></title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1" />
    <meta name="CODE_LANGUAGE" content="C#" />
    <meta name="vs_defaultClientScript" content="JavaScript" />
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5" />
    <meta http-equiv="Content-Language" content="en-us" />
    <meta http-equiv="Content-Type" content="text/html; charset=windows-1252" />
    <link rel="stylesheet" type="text/css" href='<%= "../themes/"+Session["configTheme"]+"/css/style.css" %>' />
    <link rel="stylesheet" type="text/css" href='<%= "../themes/"+Session["configTheme"]+"/css/style.css" %>' />
    <link rel="stylesheet" type="text/css" href='<%= "../themes/"+Session["configTheme"]+"/css/style_ph.css" %>' />
</head>
<body style="height: 100%;">
    <div align="left">
        <form id="Form1" method="post" runat="server">
            <table border="0" cellpadding="0" cellspacing="0" width="100%" id="page">
                <tr>
                    <td valign="top" height="137px">
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
                    <td>
                        <table border="0" cellpadding="0" cellspacing="0" width="100%" style="height: 100%;">
                            <tr>
                                <td id="relatedInfo">
                                    <EBid:Purchasing_LeftNav runat="server" ID="Purchasing_LeftNav" />
                                    <uc1:news_announcements_nav ID="News_announcements_nav1" runat="server" />
                                                                    
                                </td>
                                <td id="content">
                                    <div align="left">
                                                                                
                                            <asp:Label ID="lblName" runat="server" /></h1>
                                        <p>
                                            Listed below are the items that need your attention.</p>
                                            <table width="100%" cellpadding="0" cellspacing="0">                                                
                                                <tr>                                                    
                                                    <td valign="top" width="50%" style="padding-right: 10px;">
                                                        <h3>
                                                            Bids</h3>
                                                        <p style="padding-left:15px">
                                                            <a href="bids.aspx">
                                                                Bid Events For Approval
                                                                <asp:Label runat="server" ID="lblBidEventsForApproval" Text=""></asp:Label>
                                                            </a>
                                                            <br />
                                                            <a href="approvedbidevents.aspx">
                                                                Approved Bid Events
                                                                <asp:Label runat="server" ID="lblApprovedBidEvents" Text="" />
                                                            </a>
                                                           <br />
                                                           <a href="bidinvitations.aspx">
                                                                 Bid Event Invitations
                                                                <asp:Label runat="server" ID="lblBidInvitations" Text="" />
                                                            </a>                                                             
                                                        </p>
                                                        <br />
                                                        <h3>
                                                            Bids Status</h3>
                                                        <p style="padding-left:15px">
                                                            <a href="bidsforconversion.aspx">
                                                                Converted Bid Items
                                                                <asp:Label runat="server" ID="lblConvertedBidItems" Text=""></asp:Label>
                                                            </a>                                                            
                                                         <br /> 
                                                            <a href="awardedbiditems.aspx">
                                                                Awarded Bid Items
                                                                <asp:Label runat="server" ID="lblAwardedBidItems" Text=""></asp:Label>
                                                            </a>
                                                         <br /> 
                                                            <a href="withdrawnedbiditems.aspx">
                                                                 Withdrawn Bid Items
                                                                <asp:Label runat="server" ID="lblWithdrawnItems" Text=""></asp:Label>
                                                            </a>                                                                    
                                                        </p>
                                                        <br />
                                                        <h3>
                                                            Tenders</h3>
                                                         <p style="padding-left:15px">
                                                            <a href="bidsforeval.aspx">
                                                                Recieved Endorsements
                                                                <asp:Label runat="server" ID="lblRecievedEndorsements" Text="" />
                                                            </a>
                                                            <br />
                                                            <a href="bidsforrenegotiation.aspx">
                                                                Bid Tenders For Clarification
                                                                <asp:Label runat="server" ID="lblBidTendersForRenegotiation" Text="" />
                                                            </a>
                                                         </p> 
                                                         <br />
                                                         <br />
                                                                                                                          
                                                    </td>                                                    
                                                    <td valign="top" width="50%" style="padding-left: 10px;">
                                                        <h3>
                                                            Auctions</h3>
                                                        <p style="padding-left:15px">
                                                            <a href="auctions.aspx">
                                                                Auction Events For Approval
                                                                <asp:Label runat="server" ID="lblAuctionEventsForApproval" Text="" />
                                                            </a>
                                                            <br />
                                                            <a href="auctioninvitations.aspx">
                                                                Auction Event Invitations
                                                                <asp:Label runat="server" ID="lblConfirmedAuctionInvitations" Text="" />
                                                            </a>
                                                        </p>
                                                        <br />
                                                        <br />
                                                         <h3>
                                                            Auctions Status</h3>
                                                          <p style="padding-left:15px">
                                                            <a href="awardedauctions.aspx">
                                                                Awarded Auction Items
                                                                <asp:Label runat="server" ID="lblAwardedAuctionItems" Text="" />
                                                            </a>
                                                            <br />
                                                            <a href="auctionitemsforawarding.aspx">
                                                                Endorsed Auction Items
                                                                <asp:Label runat="server" ID="lblEndorsedAuctionItems" Text="" />
                                                            </a>
                                                          </p>
                                                          <br />
                                                          <br />
                                                        <h3>
                                                            Auction Events</h3>
                                                        <p style="padding-left:15px">
                                                            <a href="../auctions/ongoingauctionevents.aspx">Ongoing Auction Events 
                                                            <asp:Label runat="server" ID="lblOngoingAuctionEvents" Text="" />
                                                            </a>
                                                            <br />
                                                            <a href="../auctions/upcomingauctionevents.aspx">Upcoming Auction Events 
                                                            <asp:Label runat="server" ID="lblUpcomingAuctionEvents" Text="" />
                                                            </a>
                                                            <br />
                                                            <a href="../auctions/finishedauctionevents.aspx">Finished Auction Events 
                                                            <asp:Label runat="server" ID="lblFinishedAuctionEvents" Text="" />
                                                            </a>
                                                        </p>
                                                            <br />
                                                   

                                                        <h3>
                                                            Others</h3>
                                                        <p style="padding-left:15px">
                                                        <a href="../purchasingscreens/changepwd.aspx">Change Password                                                        
                                                        </a>                                                        
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
