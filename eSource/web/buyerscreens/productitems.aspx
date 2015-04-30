<%@ Page Language="C#" AutoEventWireup="true" CodeFile="productitems.aspx.cs" Inherits="web_buyerscreens_ProductItems" %>

<html xmlns="http://www.w3.org/1999/xhtml" style="height: 100%;">
<head>
<%@ Register TagPrefix="EBid" TagName="jqueryLoader" Src="../usercontrol/jQuery_loader.ascx" %>
    <EBid:jqueryLoader runat="server" ID="jqueryLoader1" />
    <title>Product Items</title>
	<link rel="stylesheet" type="text/css" href='<%= "../themes/"+Session["configTheme"]+"/css/style.css" %>' />
	<meta http-equiv="Content-Language" content="en-us" />
	<meta http-equiv="Content-Type" content="text/html; charset=windows-1252" />
	<script type="text/javascript" src="../include/generaljsfunctions.js"></script>
	<script type="text/javascript" src="../include/events.js"></script>	
</head>
<body style="height: 100%;">		
	<div>
		<form id="frmProducts" runat="server" defaultfocus="txtSearch">
			<table border="0" cellpadding="0" cellspacing="0" class="searchDetails" style="width: 100%; vertical-align: top; height: 100%;">
				<tr>
					<td colspan="2" height="25px" style="background-color:White">
						<h1>Search Products</h1>
					</td>
				</tr>
				<tr>
					<th colspan="2" height="20px">						
						<asp:Label runat="server" ID="lblCategory"></asp:Label>
						-
						<asp:Label runat="server" ID="lblSubCategory"></asp:Label>
					</th>
				</tr>
				<tr>
					<td height="35px" style="min-width: 100px;">
						<asp:TextBox runat="server" ID="txtSearch" Width="270px" onKeyUp="AutoComplete(this,this.form.lstProducts,'text',false);"></asp:TextBox>
					</td>
					<td align="right" width="250px">
						<asp:Button runat="server" ID="btnSearch" Text="Search" OnClick="btnSearch_Click" Width="75px"></asp:Button>
						<asp:Button runat="server" ID="btnShowAll" Text="Show All" OnClick="btnShowAll_Click" Width="75px"></asp:Button>
						<asp:Button ID="btnSKU" runat="server" Text="Hide SKU" OnClick="btnSKU_Click" Width="75px" />
					</td>
				</tr>
				<tr>
					<td colspan="2" height="100%">
						<asp:ListBox runat="server" ID="lstProducts" Width="99%" Height="100%"></asp:ListBox>						
					</td>
				</tr>
				<tr>
					<td colspan="2" id="actions" align="right" height="50px">
						<asp:LinkButton runat="server" Width="70px" ID="btnOK" Text="Select"></asp:LinkButton>
						<asp:LinkButton runat="server" Width="70px" ID="btnClose" CausesValidation="false" Text="Close"></asp:LinkButton>
						<input type="hidden" runat="server" id="hdnCategoryId" />
						<input type="hidden" runat="server" id="hdnSubCategoryId" />
						<input type="hidden" runat="server" id="hdnSelectedProduct" />
						<input type="hidden" runat="server" id="hdnControlId" />
					</td>
				</tr>
			</table>
		</form>	
	</div>
</body>
</html>
