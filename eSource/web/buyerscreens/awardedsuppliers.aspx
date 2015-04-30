<%@ Page Language="C#" AutoEventWireup="true" CodeFile="awardedsuppliers.aspx.cs" Inherits="WEB_buyer_screens_AwardedSuppliers" MaintainScrollPositionOnPostback="true" %>
<%@ Register  TagPrefix="EBid" TagName="LeftNav" Src="~/WEB/usercontrol/Buyer/LeftNav.ascx" %>
<%@ Register  TagPrefix="EBid" TagName="TopNavBids" Src="~/WEB/usercontrol/Buyer/TopNavBids.ascx" %>
<%@ Register  TagPrefix="EBid" TagName="TopNav2" Src="~/WEB/usercontrol/Buyer/TopNav2.ascx" %>
<%@ Register  TagPrefix="EBid" TagName="GlobalLinksNav" Src="~/web/usercontrol/GlobalLinksNav.ascx" %>
<%@ Register  TagPrefix="EBid" TagName="Footer" Src="~/web/usercontrol/Footer.ascx" %>
<%@ Register  TagPrefix="EBid" TagName="CommentBox" Src="~/web/usercontrol/CommentBox.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >

<html  >
<head runat="server">
    <%@ Register TagPrefix="EBid" TagName="jqueryLoader" Src="../usercontrol/jQuery_loader.ascx" %>
    <EBid:jqueryLoader runat="server" ID="jqueryLoader1" />
    <meta http-equiv="Content-Language" content="en-us" />
    <meta http-equiv="Content-Type" content="text/html; charset=windows-1252" />
    <link rel="stylesheet" type="text/css" href='<%= "../themes/"+Session["configTheme"]+"/css/style.css" %>' />
    <link rel="stylesheet" type="text/css" href='<%="../themes/"+Session["configTheme"]+"/css/style_buyer.css" %>' />
	<link href='<%= "../themes/"+Session["configTheme"]+"/css/style.css" %>' rel="stylesheet/" type="text/css" />
    
</head>
<body style="height: 100%;">
   
    <div>
    <form id="form1" runat="server">
    <table border="0" cellpadding="0" cellspacing="0" width="100%" id="page">
		<tr>
			<td valign=top>
			<table border="0" cellpadding="0" cellspacing="0" width=100%>
				<tr>
					<td>
					<div align="left" id="masthead">
						<EBid:GlobalLinksNav runat="server" id=GlobalLinksNav />
					</div>
					</td>
				</tr>
				<tr>
					<td>
					<EBid:TopNav2 id="TopNav2"  runat="server"/>
					</td>
				</tr>
			</table>
			</td>
		</tr>
		<tr>
			<td>
			<table border="0" cellpadding="0" cellspacing="0" width="100%" >
				<tr>
					<td id="relatedInfo">
					<div align="left">
						<EBid:LeftNav id="LeftNav"  runat="server"/>
					</div>
					<div align="left">
						<table border="0" cellpadding="0" cellspacing="0" width="100%" id="table3">
							<tr>
								<td><!-- #BeginEditable "commentArea" --><EBid:CommentBox runat="server" ID="CommentBox" /><!-- #EndEditable --></td>
							</tr>
						</table>
					</div>
					<p>&nbsp;</p>
					<p>&nbsp;</p>
					<p>&nbsp;</p>
					<p>&nbsp;</p>
					<p>&nbsp;</p>
					<p>&nbsp;</p>
					<p>&nbsp;</p>
					<p>&nbsp;</p>
					<p>&nbsp;</p>
					<p>&nbsp;</p>
					<p>&nbsp;</p>
					<p>&nbsp;</p>
					<p>&nbsp;</p>
					<p>&nbsp;</p>
					<p>&nbsp;</p>
					<p>&nbsp;</p>
					<p>&nbsp;</p>
						</td>
					<td id="content">
					<!-- #BeginEditable "contentArea" -->
					<div align="left">
						<table border="0" cellpadding="0" cellspacing="0" width="100%" id="table4">
							<tr>
								<td>
								<h1>
								<br />
								Awarding Details</h1>
								<table border="0" cellpadding="0" cellspacing="0" width="100%" id="itemDetails">
									<tr>
										<th colspan="2" style="height: 18px"><asp:label runat="server" ID="lblBidRefNo"></asp:label></th>
									</tr>
								</table>
								<br />
								<asp:GridView ID="gvBids" runat="server" AllowPaging="True" AllowSorting="True"
									AutoGenerateColumns="False"   PageSize="20" CssClass="pageDetails" BorderColor=#FFFFFF  OnRowCommand="gvBids_RowCommand" 
									OnSorting="gvBids_Sorting" OnPageIndexChanging="gvBids_PageIndexChanging">
									
									<PagerSettings Mode=NumericFirstLast FirstPageText="First" LastPageText="Last" NextPageText="Next" PreviousPageText="Prev"  />
									<PagerStyle HorizontalAlign=Right />
									<HeaderStyle CssClass =pageDetails_th ForeColor=#FFFFFF/>
									<AlternatingRowStyle CssClass="evenCells"/>
									<FooterStyle CssClass="evenCells" />

									<Columns>
										<asp:TemplateField HeaderText="Item" ItemStyle-Width=40%>
											<ItemTemplate>
												<asp:Label ID="lnkItemDesc" runat="server" Text='&nbsp;<%# Bind("Item") %>'></asp:Label>
											</ItemTemplate>
										</asp:TemplateField>
										<asp:TemplateField  HeaderText="Cost" ItemStyle-Width=30%>
											<HeaderStyle HorizontalAlign="Left" />
											<ItemTemplate>
												<asp:Label ID="lblCost" runat="server" Text='<%# Bind("Amount") %>'></asp:Label>
											</ItemTemplate>
										</asp:TemplateField>
										<asp:TemplateField  HeaderText="Supplier" SortExpression="VendorName"  ItemStyle-Width=30%>
											<HeaderStyle HorizontalAlign="Left" />
											<ItemTemplate>
												<asp:Label ID="lblSupplier" runat="server" Text='<%# Bind("VendorName") %>'></asp:Label>
											</ItemTemplate>
										</asp:TemplateField>
									</Columns>
								</asp:GridView>
					
					
					
					<br />
					<table border="0" cellpadding="0" cellspacing="0" width="100%" id="actions">
						<tr>
							<td align=left>Total Records: <asp:label runat="server" ID=lblBidCount ></asp:label></td>
							<td><asp:linkbutton runat="server" ID=btnCancel Text=Ok OnClick="btnCancel_Click"></asp:linkbutton></td>
						</tr>
					</table>
					<p>&nbsp;</p>
								<p>&nbsp;</td>
							</tr>
						</table>
					</div>
					<!-- #EndEditable -->
					</td>
				</tr>
			</table>
			</td>
		</tr>
		<tr>
			<td id="footer"><EBid:Footer runat="server" ID="Footer" /></td>
		</tr>
	</table>
    </form>
    </div>
    
</body>
</html>
