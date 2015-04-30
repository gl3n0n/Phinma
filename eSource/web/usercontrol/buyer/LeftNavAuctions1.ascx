<%@ Control Language="C#" AutoEventWireup="true" CodeFile="LeftNavAuctions1.ascx.cs" Inherits="web_user_control_LeftNavAuctions1" %>

<h2>
    Auction
</h2>
<table border="0" cellpadding="0" cellspacing="0" width="100%" id="related">
	<tr>
		<td>
			<asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/web/buyerscreens/auctions.aspx">Auction Event Drafts</asp:HyperLink>
		</td>
	</tr>
	<tr>
		<td>
			<asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/web/buyerscreens/submittedauctionevents.aspx">Auction Events For Approval</asp:HyperLink>			
		</td>
	</tr>
	<tr>
		<td>
			<asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl="~/web/buyerscreens/approvedauctionevents.aspx">Approved Auction Events</asp:HyperLink>			
		</td>
	</tr>
	<tr>
		<td>
			<asp:HyperLink ID="HyperLink4" runat="server" NavigateUrl="~/web/buyerscreens/rejectedauctionevents.aspx">Rejected Auction Events</asp:HyperLink>
		</td>
	</tr>
	<tr>
		<td>
			<asp:HyperLink ID="HyperLink5" runat="server" NavigateUrl="~/web/buyerscreens/auctioninvitations.aspx">Auction Invitations</asp:HyperLink>			
		</td>
	</tr>
	<tr>
		<td>
			<asp:HyperLink ID="HyperLink10" runat="server" NavigateUrl="~/web/buyerscreens/auctionitemsforre-editing.aspx">Auction Events For Re-Editing</asp:HyperLink>			
		</td>
	</tr>	
	<tr>
		<td>
			<asp:HyperLink ID="HyperLink7" runat="server" NavigateUrl="~/web/buyerscreens/auctioncalendar.aspx">Auction Calendar</asp:HyperLink>			
		</td>
	</tr>		
</table>
<h2>
    Auction Items
</h2>
<table border="0" cellpadding="0" cellspacing="0" width="100%" class="related">
    <tr>
		<td>
			<asp:HyperLink ID="HyperLink8" runat="server" NavigateUrl="~/web/buyerscreens/endorseditems.aspx">Endorsed Auction Items</asp:HyperLink>
		</td>
	</tr>
	<tr>
		<td>
			<asp:HyperLink ID="HyperLink6" runat="server" NavigateUrl="~/web/buyerscreens/awardedauctions.aspx">Awarded Auction Items</asp:HyperLink>			
		</td>
	</tr>
</table>
