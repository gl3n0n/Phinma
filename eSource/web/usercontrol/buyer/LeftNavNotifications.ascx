<%@ Control Language="C#" AutoEventWireup="true" CodeFile="LeftNavNotifications.ascx.cs" Inherits="web_user_control_LeftNavNotifications" %>
<table border="0" cellpadding="0" cellspacing="0" width="100%" id="related">
	<tr>
		<td style="height: 19px">			
			<asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/web/auctions/ongoingauctionevents.aspx">Ongoing Auction Events</asp:HyperLink>
		</td>
	</tr>
	<tr>
		<td>			
			<asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/web/auctions/upcomingauctionevents.aspx">Upcoming Auction Events</asp:HyperLink>
		</td>
	</tr>
	<tr>
		<td>			
			<asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl="~/web/auctions/finishedauctionevents.aspx">Finished Auction Events</asp:HyperLink>
		</td>
	</tr>	
</table>
