<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Vendor_TopNav_Bids.ascx.cs"
	Inherits="web_user_control_Vendor_TopNav_Bids" %>
<table border="0" cellpadding="0" cellspacing="0" width="100%" class="tabs" id="table2">
	<tr>		
		<td width="200px" style="height: 19px">
			<asp:HyperLink ID="lnk1" runat="server" NavigateUrl="~/web/vendorscreens/index.aspx">Home</asp:HyperLink>			
		</td>
		<td class="activeTab" width="200px">			
			<asp:HyperLink ID="lnk2" runat="server" NavigateUrl="~/web/vendorscreens/bids.aspx">Bids</asp:HyperLink>
		</td>
		<td width="200px">			
			<asp:HyperLink ID="lnk3" runat="server" NavigateUrl="~/web/vendorscreens/auctions.aspx">Auctions</asp:HyperLink>
		</td>
		<td width="200px">			
			<asp:HyperLink ID="lnk4" runat="server" NavigateUrl="~/web/vendorscreens/profile.aspx">My Profile</asp:HyperLink>
		</td>
		<td>
			&nbsp;
		</td>
	</tr>
</table>
