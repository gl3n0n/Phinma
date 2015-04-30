<%@ Page Language="C#" AutoEventWireup="true" CodeFile="logout.aspx.cs" Inherits="logout" MasterPageFile="~/publicmaster.master" Title=".:| Trans-Asia eSourcing System | Log Out |:." %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
<script language="JavaScript">
<!--
	// Disables back button
	javascript:window.history.forward(1);
//-->
</script>
<script language="javascript" src="include/xp_progress.js">
</script>
<table border="0" cellpadding="0" cellspacing="0" width="100%">
	<tr>
		<td height="30px">			
		</td>
	</tr>
	<tr>
		<td align="center">
			<asp:Label ID="lblMessage" Font-Bold="true" Font-Size="Small" runat="server" Text="Logging Out.<br />Please wait while we redirect you back to the home page."></asp:Label>
		</td>
	</tr>
	<tr>
		<td height="30px" align="center">
			<script type="text/javascript">
			<!--
				var bar1= createBar(300,15,'white',1,'black','blue',100,7,1,"window.location='login.aspx'");
			//-->
			</script>
		</td>
	</tr>
</table>
</asp:Content>