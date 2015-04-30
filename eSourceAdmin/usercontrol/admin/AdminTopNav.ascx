<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AdminTopNav.ascx.cs" Inherits="web_user_control_GlobalLinksNav" %>
<table border="0" cellpadding="0" cellspacing="0" width="100%" id="table2" onload="SetStatus();" style="background-color:black;">
	<tr>
		<td align="left" width="150px">
            <asp:Image ID="LogoImg" runat="server"  ImageUrl="/images/logo.jpg" />
			<%--<img border="0" src='<%= ConfigurationManager.AppSettings["ServerUrl"] %>/clients/<%= HttpContext.Current.Session["clientid"] %>/images/logo.jpg' >--%>
		</td>
		<td>
			&nbsp;
		</td>
		<td class="globalLinks" align="right" width="360px">
				<br /><br />				
				<asp:HyperLink ID="lnk4" runat="server" NavigateUrl="~/logout.aspx">Log Out</asp:HyperLink>			
		</td>
	</tr>
</table>
