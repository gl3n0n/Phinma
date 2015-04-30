<%@ Page Language="C#" AutoEventWireup="true" CodeFile="endorsedbidtenderdetails.aspx.cs" Inherits="WEB_buyer_screens_SentBidItemsForRenegotiationDetails" %>
<%@ Register  TagPrefix="EBid" TagName="LeftNav" Src="~/WEB/usercontrol/Buyer/LeftNav.ascx" %>
<%@ Register  TagPrefix="EBid" TagName="TopNavBids" Src="~/WEB/usercontrol/Buyer/TopNavBids.ascx" %>
<%@ Register  TagPrefix="EBid" TagName="TopNav2" Src="~/WEB/usercontrol/Buyer/TopNav2.ascx" %>
<%@ Register  TagPrefix="EBid" TagName="GlobalLinksNav" Src="~/web/usercontrol/GlobalLinksNav.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >

<html xmlns="http://www.w3.org/1999/xhtml" style="height: 100%;">

<!-- #BeginTemplate "../templates/bids.dwt" -->

<head>
<meta http-equiv="Content-Language" content="en-us" />
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252" />
<link rel="stylesheet" type="text/css" href='<%= "../themes/"+Session["configTheme"]+"/css/style.css" %>' />
    <link rel="stylesheet" type="text/css" href='<%="../themes/"+Session["configTheme"]+"/css/style_buyer.css" %>' />
<style>

</style>
<!-- #BeginEditable "doctitle" -->
<!-- #EndEditable -->
	<link href='<%= "../themes/"+Session["configTheme"]+"/css/style.css" %>' rel="stylesheet/" type="text/css" />
</head>

<body style="height: 100%;">

<div>
<div align="left">
	<form runat="server" id=frm1>
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
								<td><!-- #BeginEditable "commentArea" -->&nbsp;<!-- #EndEditable --></td>
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
                    <table border="0" cellpadding="0" cellspacing="0" width="100%" id="page0">
						<tr>
							<td valign=top>
							<table border="0" cellpadding="0" cellspacing="0" width="100%" >
								<tr>
									<td id="content0">
									<h1><br />
										Endorsed Bid Tenders</h1>
									<div align="left">
										<table border="0" cellpadding="0" cellspacing="0" width="100%" id="itemDetails">
											<tr>
												<th colspan="2">Bid Item <asp:label runat="server" ID=lblBidRefNo></asp:label></th>
											</tr>
										</table>
									</div>
									<div align="left">
										<table border="0" cellpadding="0" cellspacing="0" width="100%" id="itemDetails" >
											<tr>
												<th colspan="2">Details</th>
											</tr>
											<tr>
												<td width="133">Company:</td>
												<td class="value"><asp:label runat="server" ID=lblCompany></asp:label></td>
											</tr>
											<tr>
												<td width="133">Requestor:</td>
												<td class="value"><asp:label runat="server" ID=lblRequestor></asp:label></td>
											</tr>
											<tr>
												<td width="133">PR Number:</td>
												<td class="value"><asp:label runat="server" ID=lblPRNumber></asp:label></td>
											</tr>
											<tr>
												<td width="133">PR Date</td>
												<td class="value"><asp:label runat="server" ID=lblPRDate></asp:label></td>
											</tr>
											<tr>
												<td width="133">Group/Department/Section</td>
												<td class="value"><asp:label runat="server" ID=lblGroup></asp:label></td>
											</tr>
											<tr>
												<td width="133">Bid reference Number:</td>
												<td class="value"><asp:label runat="server" ID=lblBidReferenceNumber></asp:label></td>
											</tr>
											<tr>
												<td width="133">Sub Category:</td>
												<td class="value"><asp:label runat="server" ID=lblSubCategory></asp:label></td>
											</tr>
											<tr>
												<td width="133">Bid Submission 
											Deadline:</td>
												<td class="value"><asp:label runat="server" ID=lblBidSubmissionDeadline></asp:label></td>
											</tr>
											<tr>
												<td width="133">Bid Item Description</td>
												<td class="value"><asp:label runat="server" ID=lblBidItemDesc></asp:label></td>
											</tr>
											<tr>
											<td width="133">Deliver to: </td>
												<td class="value"><asp:label runat="server" ID=lblDeliverTo></asp:label></td>
											</tr>
											<tr>
									<td width="132">Incoterm</td>
												<td class="value"><asp:label runat="server" ID=lblIncoterm></asp:label></td>
											</tr>
										</table></div>
									<table border="0" cellpadding="0" cellspacing="0" width="100%" id="itemDetails1" runat="server">
										<tr>
											<th width="65">Item</th>
												<th width="183">Description</th>
												<th>Quantity</th>
											<th>Unit of Measure</th>
											
										</tr>
									</table>
									<br />

									<asp:GridView ID="gvBids" runat="server" AllowPaging="True" AllowSorting="True"
									AutoGenerateColumns="False"   PageSize="20" CssClass="pageDetails" BorderColor=#FFFFFF  OnRowCommand="gvBids_RowCommand" 
									OnSorting="gvBids_Sorting" OnPageIndexChanging="gvBids_PageIndexChanging">
									<PagerSettings Mode=NumericFirstLast FirstPageText="First" LastPageText="Last" NextPageText="Next" PreviousPageText="Prev"  />
									<PagerStyle HorizontalAlign=Right />
									<HeaderStyle CssClass=pageDetails_th ForeColor=#FFFFFF/>
									<AlternatingRowStyle CssClass="evenCells"/>
									<FooterStyle CssClass="evenCells" />

									<Columns>
										<asp:TemplateField HeaderText="Item" ItemStyle-Width=20%>
										<ItemTemplate>
										<asp:Label  ID="lnkItem" runat="server" Text='<%# Bind("Item") %>' ></asp:Label>
										</ItemTemplate>
										</asp:TemplateField>

										<asp:TemplateField  HeaderText="Cost" ItemStyle-Width=20%>
										<HeaderStyle HorizontalAlign="Left" />
										<ItemTemplate>
										<asp:Label ID="lblAmount" runat="server" Text='<%# Bind("Amount") %>'></asp:Label>
										</ItemTemplate>
										</asp:TemplateField>

										<asp:TemplateField  HeaderText="Supplier" ItemStyle-Width=100%>
										<HeaderStyle HorizontalAlign="Left" />
										<ItemTemplate>
										<asp:Label ID="lblVendorName" runat="server" Text='<%# Bind("VendorName") %>'></asp:Label>
										</ItemTemplate>
										</asp:TemplateField>
									</Columns>
									</asp:GridView>
									
									<br />
									<div align="left">
										<table border="0" cellpadding="0" cellspacing="0" width="100%" id="actions">
											<tr>
												<td>
												<asp:linkbutton runat="server" ID="lnkOk" OnClick="lnkOk_Click">Back</asp:linkbutton>
												</td>
											</tr>
										</table></div>
									</td>
								</tr>
							</table></td>
						</tr>
					</table>&nbsp;
					<!-- #EndEditable --></td>
				</tr>
			</table>
			</td>
		</tr>
		<tr>
			<td id="footer">Trans-Asia E-Bid Portal System<br />
			Copyright © 2002 Trans-Asia , inc. All rights reserved.&nbsp; <a href="#">
			Sitemap</a> - <a href="#">Terms of Use</a> - <a href="#">Privacy 
			policy</a></td>
		</tr>
	</table>
	</form>
</div>
&nbsp;</div>

</body>

<!-- #EndTemplate -->

</html>
