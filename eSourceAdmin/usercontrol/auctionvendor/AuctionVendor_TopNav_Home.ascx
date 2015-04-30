<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AuctionVendor_TopNav_Home.ascx.cs" Inherits="web_user_control_Vendor_AuctionVendor_TopNav_Home" %>
<table border="0" cellpadding="0" cellspacing="0" width="100%" class="tabs" id="table1">
	<tr>
		<td class="activeTab" width="200px">
		<asp:linkbutton runat="server" ID="lnkHome" OnClick="lnkHome_Click">Home</asp:linkbutton>
		</td>
		<td width="200px">
		<asp:linkbutton runat="server" ID="lnkUpcomingAuctions" OnClick="lnkUpcomingAuctions_Click">Upcoming Auctions</asp:linkbutton>
		</td>
		<td width="200px">
		<asp:linkbutton runat="server" ID="lnkFinishedAuctions" OnClick="lnkFinishedAuctions_Click">Finished Auctions</asp:linkbutton>
		</td>
		<td>&nbsp;</td>
	</tr>
</table>