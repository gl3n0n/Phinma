<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Login_TopNav.ascx.cs"
	Inherits="web_user_control_Login_TopNav" %>
<link rel="stylesheet" type="text/css" href="../css/style.css" />
<table border="0" cellpadding="0" cellspacing="0" width="100%" id="table2">
	<tr>
		<td class="tabs" width="20%">
			<asp:LinkButton runat="server" ID="lnkHome" CausesValidation="false" OnClick="lnkHome_Click">Home</asp:LinkButton></td>
		<td width="20%" class="tabs">
			<asp:LinkButton ID="lnkAbout" runat="server" CausesValidation="false">About</asp:LinkButton></td>
		<td width="20%" class="tabs">
			<asp:LinkButton ID="lnkHelp" runat="server" CausesValidation="false">Help</asp:LinkButton></td>
		<td width="20%" class="tabs">
			<asp:LinkButton ID="lnkFAQs" runat="server" CausesValidation="false">FAQs</asp:LinkButton></td>
	</tr>
</table>
