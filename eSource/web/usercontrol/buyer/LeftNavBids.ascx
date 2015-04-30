<%@ Control Language="C#" AutoEventWireup="true" CodeFile="LeftNavBids.ascx.cs" Inherits="WEB_user_control_LeftNavBids" %>
<link type="text/css" href="../../css/style.css" rel="stylesheet" />
<table border="0" cellpadding="0" cellspacing="0" width="100%" id="related">
	<tr>
		<td>			
			<asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/web/buyerscreens/bids.aspx">Drafts</asp:HyperLink>
		</td>
	</tr>
	<tr>
		<td>
			<asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/web/buyerscreens/bidsforeval.aspx">Received Tenders</asp:HyperLink>
		</td>
	</tr>
	<tr>
		<td>
			<asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl="~/web/buyerscreens/updatedbiditems.aspx">Updated Bid Items</asp:HyperLink>
		</td>
	</tr>
</table>
