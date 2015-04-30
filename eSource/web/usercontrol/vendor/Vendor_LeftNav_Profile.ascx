<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Vendor_LeftNav_Profile.ascx.cs"
	Inherits="web_usercontrol_Vendor_Vendor_LeftNav_Profile" %>
<table border="0" cellpadding="0" cellspacing="0" width="100%" id="related">
	<tr>
		<td style="height: 19px">			
			<asp:HyperLink ID="lnk1" runat="server" NavigateUrl="~/web/vendorscreens/profile.aspx">My Details</asp:HyperLink>
		</td>
	</tr>
	<tr>
		<td>			
			<asp:HyperLink ID="lnk2" runat="server" NavigateUrl="~/web/vendorscreens/changepassword.aspx">Change Password</asp:HyperLink>
		</td>
	</tr>
</table>
