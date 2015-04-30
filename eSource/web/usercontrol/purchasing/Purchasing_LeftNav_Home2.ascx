<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Purchasing_LeftNav_Home2.ascx.cs"
    Inherits="web_user_control_Purchasing_LeftNavHome2" %>
<link type="text/css" href="../../css/style_ph.css" rel="stylesheet" />
<table border="0" cellpadding="0" cellspacing="0" width="100%" id="related">
    <tr>
        <td>
            <asp:HyperLink runat="server" ID="btnAuctForApproval" NavigateUrl="~/web/purchasingscreens/Auctions.aspx">
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
            <asp:HyperLink runat="server" ID="btnConfirmedAuction" NavigateUrl="~/web/purchasingscreens/confirmedauctioninvitations.aspx">
								Confirmed Auction Invitations</asp:HyperLink></td>
    </tr>
    <tr>
        <td>
            <asp:HyperLink runat="server" ID="btnAwardedAuction" NavigateUrl="~/web/purchasingscreens/awardedauctionitems.aspx">
								Awarded Auction Items</asp:HyperLink></td>
    </tr>
    <tr>
        <td>
            <asp:HyperLink runat="server" ID="lnkAuctionCalendar" NavigateUrl="~/web/purchasingscreens/auctioncalendar.aspx">
                                Auction Calendar</asp:HyperLink>
        </td>
    </tr>
</table>
