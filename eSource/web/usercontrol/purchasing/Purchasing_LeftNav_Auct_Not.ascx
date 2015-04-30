<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Purchasing_LeftNav_Auct_Not.ascx.cs"
    Inherits="web_user_control_Purchasing_Purchasing_LeftNav_Auct_Not" %>
<table border="0" cellpadding="0" cellspacing="0" width="100%" id="related">
    <tr>
        <td>
            <asp:HyperLink runat="server" ID="btnOngoingAuction" NavigateUrl="~/web/auctions/ongoingauctionevents.aspx">
								Ongoing Auction Events</asp:HyperLink></td>
    </tr>
    <tr>
        <td style="height: 19px">
            <asp:HyperLink runat="server" ID="btnUpcomingAuction" NavigateUrl="~/web/auctions/upcomingauctionevents.aspx">
								Upcoming Auction Events</asp:HyperLink></td>
    </tr>
    <tr>
        <td style="height: 19px">
            <asp:HyperLink runat="server" ID="btnFinishedAuction" NavigateUrl="~/web/auctions/finishedauctionevents.aspx">
								Finished Auction Events</asp:HyperLink></td>
    </tr>    
</table>
