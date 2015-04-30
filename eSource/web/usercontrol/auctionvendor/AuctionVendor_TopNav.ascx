<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AuctionVendor_TopNav.ascx.cs"
	Inherits="AuctionVendor_TopNav" %>
<table border="0" cellpadding="0" cellspacing="0" width="100%" class="tabs" id="table1">
	<tr>
		<td width="200px">
		<asp:linkbutton runat="server" ID="lnkHome" OnClick="lnkHome_Click">Home</asp:linkbutton>
		</td>
		<td id="tdOngoing" runat="server" width="200px">
		<asp:linkbutton runat="server" ID="lnkOngoingAuctions" OnClick="lnkOngoingAuctions_Click">Ongoing Auctions</asp:linkbutton>
		</td>
		<td id="tdUpcoming" runat="server" width="200px">
		<asp:linkbutton runat="server" ID="lnkUpcomingAuctions" OnClick="lnkUpcomingAuctions_Click">Upcoming Auctions</asp:linkbutton>
		</td>
		<td id="tdFinished" runat="server" width="200px">
		<asp:linkbutton runat="server" ID="lnkFinishedAuctions" OnClick="lnkFinishedAuctions_Click">Finished Auctions</asp:linkbutton>
		</td>
		<td>&nbsp;</td>
	</tr>
</table>
