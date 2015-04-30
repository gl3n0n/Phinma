<%@ Control Language="C#" AutoEventWireup="true" CodeFile="LeftNavSuppliers.ascx.cs" Inherits="web_user_control_LeftNavSuppliers" %>
<LINK type="text/css" href="../../css/style.css" rel=stylesheet/>
<table border="0" cellpadding="0" cellspacing="0" width="100%" id="related">
	<tr>
		<td>
			<asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/web/buyerscreens/suppliers.aspx">Accredited Suppliers</asp:HyperLink>			
		</td>
    </tr>    
</table>
<h2>Vendor Shortlisting Form</h2>
<table border="0" cellpadding="0" cellspacing="0" width="100%" class="related">
	<tr>
		<td>
			<asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/web/buyerscreens/vsfcreate.aspx?create=new">Create VSF</asp:HyperLink>		
		</td>
	</tr>
	<tr>
		<td>
			<asp:HyperLink ID="HyperLink13" runat="server" NavigateUrl="~/web/buyerscreens/vsfdrafts.aspx">Draft VSF</asp:HyperLink>		
		</td>
	</tr>
	<tr>
		<td>
			<asp:HyperLink ID="HyperLink4" runat="server" NavigateUrl="~/web/buyerscreens/vsfendorsed.aspx">Endorsed VSF</asp:HyperLink>		
		</td>
	</tr>	
	<tr>
		<td>
			<asp:HyperLink ID="HyperLink5" runat="server" NavigateUrl="~/web/buyerscreens/vsfapproved.aspx">Approved VSF</asp:HyperLink>		
		</td>
	</tr>	
	<tr>
		<td>
			<asp:HyperLink ID="HyperLink6" runat="server" NavigateUrl="~/web/buyerscreens/vsfclarifications.aspx">VSF for Clarifications</asp:HyperLink>		
		</td>
	</tr>	
</table>