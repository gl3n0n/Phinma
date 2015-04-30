<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TopNavAuctions.ascx.cs" Inherits="WEB_user_control_TopNavAuctions" %>
<link type="text/css" href="../../css/style.css" rel="stylesheet" />
<table border="0" cellpadding="0" cellspacing="0" width="100%" id="table2" class="topnav">
	<tr>
		<td class="tabs">			
			<asp:HyperLink ID="lnk1" runat="server" NavigateUrl="~/web/buyerscreens/index.aspx">Home</asp:HyperLink>
		</td>
		<td class="tabs">
			<asp:HyperLink ID="lnk2" runat="server" NavigateUrl="~/web/buyerscreens/bids.aspx">Bids</asp:HyperLink>			
		</td>
		<td class="activeTab">
			<asp:HyperLink ID="lnk3" runat="server" NavigateUrl="~/web/buyerscreens/auctions.aspx">Auctions</asp:HyperLink>			
		</td>
		<td class="tabs">
			<asp:HyperLink ID="lnk4" runat="server" NavigateUrl="~/web/buyerscreens/suppliers.aspx">Suppliers</asp:HyperLink>			
		</td>
		<td class="tabs">
			<asp:HyperLink ID="lnk5" runat="server" NavigateUrl="~/web/buyerscreens/product.aspx">Products</asp:HyperLink>			
		</td>
		<td class="tabs">
			<asp:HyperLink ID="lnk6" runat="server" NavigateUrl="~/web/buyerscreens/report_savingsbybidevent.aspx">Reports</asp:HyperLink>			
		</td>
		<td>&nbsp;</td>
	</tr>
</table>
