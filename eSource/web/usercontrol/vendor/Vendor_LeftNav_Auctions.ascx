<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Vendor_LeftNav_Auctions.ascx.cs" Inherits="web_user_control_Vendor_Vendor_LeftNav_Auctions" %>
<table id="related" cellSpacing="0" cellPadding="0" width="100%" border="0">
	<tr>
		<td>			
			<asp:HyperLink ID="lnk1" runat="server" NavigateUrl="~/web/vendorscreens/auctions.aspx">New Auction Events</asp:HyperLink>
		</td>
	</tr>
	<tr>
		<td>			
			<asp:HyperLink ID="lnk2" runat="server" NavigateUrl="~/web/vendorscreens/confirmedinvitations.aspx">Confirmed Invitations</asp:HyperLink>
		</td>
	</tr>
	<tr>
		<td>			
			<asp:HyperLink ID="lnk4" runat="server" NavigateUrl="~/web/vendorscreens/auctioncalendar.aspx">Auction Calendar</asp:HyperLink>
		</td>
	</tr>
</table>
<h2>Auction Items</h2>
<table cellSpacing="0" cellPadding="0" width="100%" border="0" class="related">
<tr>
	<td>			
		<asp:HyperLink ID="lnk3" runat="server" NavigateUrl="~/web/vendorscreens/awardedauctions.aspx">Awarded Auction Items</asp:HyperLink>
	</td>
</tr>
</table>