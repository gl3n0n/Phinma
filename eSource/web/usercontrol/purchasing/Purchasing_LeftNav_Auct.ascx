<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Purchasing_LeftNav_Auct.ascx.cs"
    Inherits="web_user_control_Purchasing_Purchasing_LeftNav_Auct" %>
<h2>Auction Events</h2>
<table border="0" cellpadding="0" cellspacing="0" width="100%" id="related">
    <tr>
        <td>
            <asp:HyperLink runat="server" ID="btnAuctForApproval" NavigateUrl="~/web/purchasingscreens/auctions.aspx">
								Auction Events For Approval</asp:HyperLink></td>
    </tr>
    <tr>
        <td>
            <asp:HyperLink runat="server" ID="btnApprovedAuction" NavigateUrl="~/web/purchasingscreens/approvedauctionevents.aspx">
								Approved Auction Events</asp:HyperLink></td>
    </tr>
    <tr>
        <td>
            <asp:HyperLink runat="server" ID="btnRejectedAuction" NavigateUrl="~/web/purchasingscreens/rejectedauctionevents.aspx">
								Rejected Auction Events</asp:HyperLink></td>
    </tr>
    <tr>
        <td>
            <asp:HyperLink runat="server" ID="btnConfirmedAuction" NavigateUrl="~/web/purchasingscreens/auctioninvitations.aspx">
								Auction Invitations</asp:HyperLink></td>
    </tr>
    <tr>
        <td>
            <asp:HyperLink runat="server" ID="lnkAuctionForReedit" NavigateUrl="~/web/purchasingscreens/auctionitemsforre-editing.aspx">
								Auction Events For Re-Editing</asp:HyperLink></td>
    </tr>    
    <tr>
        <td>
            <asp:HyperLink runat="server" ID="lnkAuctionCalendar" NavigateUrl="~/web/purchasingscreens/auctioncalendar.aspx">
                                Auction Calendar</asp:HyperLink>
        </td>
    </tr>
</table>
<h2>Auction Items</h2>
<table border="0" cellpadding="0" cellspacing="0" width="100%" class="related">
    <tr>
        <td>
            <asp:HyperLink runat="server" ID="btnEndorsedAuctItems" NavigateUrl="~/web/purchasingscreens/auctionitemsforawarding.aspx">
								Endorsed Auction Items</asp:HyperLink></td>
    </tr>
    <tr>
        <td>
            <asp:HyperLink runat="server" ID="btnAwardedAuction" NavigateUrl="~/web/purchasingscreens/awardedauctions.aspx">
								Awarded Auction Items</asp:HyperLink></td>
    </tr>
</table>
