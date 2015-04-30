<%@ Page language="c#" Inherits="EBid.WEB.buyer_screens.DraftBidDetails" CodeFile="deniedbiditemdetails.aspx.cs" %>
<%@ Register  TagPrefix="EBid" TagName="LeftNav" Src="~/WEB/usercontrol/Buyer/LeftNav.ascx" %>
<%@ Register  TagPrefix="EBid" TagName="TopNavBids" Src="~/WEB/usercontrol/Buyer/TopNavBids.ascx" %>
<%@ Register  TagPrefix="EBid" TagName="TopNav2" Src="~/WEB/usercontrol/Buyer/TopNav2.ascx" %>
<%@ Register  TagPrefix="EBid" TagName="GlobalLinksNav" Src="~/web/usercontrol/GlobalLinksNav.ascx" %>
<%@ Register  TagPrefix="EBid" TagName="Footer" Src="~/web/usercontrol/Footer.ascx" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >

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
</head>

<body style="height: 100%;">

<div>
<div align="left">
	<form id="Form1" runat="server">
	<table border="0" cellpadding="0" cellspacing="0" width="100%" id="page">
		<tr>
			<td valign=top>
			<table border="0" cellpadding="0" cellspacing="0" width=100%>
				<tr>
					<td>
					<div align="left" id="masthead">
						<EBid:GlobalLinksNav runat="server" ID="GlobalLinksNav" />
					</div>
					</td>
				</tr>
				<tr>
					<td>
					<EBid:TopNav2 runat="server" ID="TopNav2" />
					</td>
				</tr>
			</table>
			</td>
		</tr>
		<tr valign="top">
			<td valign="top">
			<table border="0" cellpadding="0" cellspacing="0" width="100%" >
				<tr>
					<td id="relatedInfo">
					<h2>Bids</h2>
					<div align="left">
						<EBid:LeftNav runat="server" ID="LeftNav" />
					</div>
					<div align="left">
						<table border="0" cellpadding="0" cellspacing="0" width="100%" id="table3">
							<tr>
																<td><!-- #BeginEditable "commentArea" -->
											&nbsp;<!-- #EndEditable --></td>

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
									<h1><br />Denied Bid Item Details</h1>
									<p>This Bid Item is still in Draft status. You may make 
					edits to this document before submitting it for Approval by 
					the Purchasing head.</p>
									<div align="left">
										<table border="0" cellpadding="0" cellspacing="0" width="100%" id="itemDetails" >
											<tr>
												<th colspan="2">Details</th></tr>
											<tr>
												<td width="133">Company:</td>
												<td class="value"><asp:label runat="server" ID="lblCompany"></asp:label></td>
											</tr>
											<tr>
												<td width="133">Requestor:</td>
												<td class="value"><asp:label runat="server" ID="lblRequestorName"></asp:label></td>
											</tr>
											<tr>
												<td width="133">PR Number:</td>
												<td class="value"><asp:label runat="server" ID="lblPRNumber"></asp:label></td>
											</tr>
											<tr>
												<td width="133">PR Date</td>
												<td class="value"><asp:label runat="server" ID="lblPRDate"></asp:label></td>
											</tr>
											<tr>
												<td width="133">Group/Department/Section</td>
												<td class="value"><asp:label runat="server" ID="lblGroupDeptSec"></asp:label></td>
											</tr>
											<tr>
												<td width="133">Bid reference Number:</td>
												<td class="value"><asp:label runat="server" ID="lblBidRefNum"></asp:label></td>
											</tr>
											<tr>
												<td width="133">Sub Category:</td>
												<td class="value"><asp:label runat="server" ID="lblSubCategory"></asp:label></td>
											</tr>
											<tr>
												<td width="133">Bid Submission 
											Deadline:</td>
												<td class="value"><asp:label runat="server" ID="lblBidSubmissionDeadline"></asp:label></td>
											</tr>
											<tr>
												<td width="133">Bid Item Description</td>
												<td class="value"><asp:label runat="server" ID="lblBidItemDescription"></asp:label></td>
											</tr>
											<tr>
											<td width="133">Deliver to: </td>
												<td class="value"><asp:label runat="server" ID="lblDeliverTo"></asp:label></td>
											</tr>
											<tr>
									<td width="132">Incoterm</td>
												<td class="value"><asp:label runat="server" ID="lblIncoterm"></asp:label></td>
											</tr>
										</table></div>
										<div>
										<asp:GridView   AutoGenerateColumns=false runat="server" ID=gvBidItemDetails CssClass=itemDetails >
						<Columns>
						<asp:TemplateField  HeaderText="Item" HeaderStyle-CssClass="itemDetails_th" ItemStyle-CssClass=itemDetails_td >
						<ItemStyle CssClass="itemDetails_td"  />
						<ItemTemplate>
						<asp:Label runat="server" ID="lblItem" Text='<%#Bind("Item") %>'></asp:Label>
						</ItemTemplate>
						
						</asp:TemplateField>
						<asp:TemplateField   HeaderText="Description" ItemStyle-CssClass="valueGridItem" HeaderStyle-CssClass="itemDetails_th">
                        <ItemTemplate >
						<asp:Label runat="server" ID="lblDescription" Text='<%#Bind("DetailDesc") %>'></asp:Label>
						</ItemTemplate>
						</asp:TemplateField>
						<asp:TemplateField    HeaderText="Quantity" ItemStyle-CssClass="valueGridItem" HeaderStyle-CssClass="itemDetails_th">
						<ItemTemplate>
						<asp:Label runat="server" ID="lblQuantity" Text='<%#Bind("Qty") %>'></asp:Label>
						</ItemTemplate>
						</asp:TemplateField>
						<asp:TemplateField    HeaderText="Unit Of Measure" ItemStyle-CssClass="valueGridItem" HeaderStyle-CssClass="itemDetails_th">
						<ItemTemplate>
						<asp:Label runat="server" ID="lblUnitOfMeasure" Text='<%#Bind("UnitOfMeasure") %>'></asp:Label>
						</ItemTemplate>
						</asp:TemplateField>
						<asp:TemplateField   HeaderText="Delivery Date" ItemStyle-CssClass="valueGridItemRight" HeaderStyle-CssClass="itemDetails_th_right">
						<ItemTemplate >
						<asp:Label runat="server" ID="lblDeliveryDate" Text='<%#Bind("DeliveryDate1") %>'></asp:Label>
						</ItemTemplate>
						</asp:TemplateField>
						</Columns>
						</asp:GridView>
						<asp:Label runat="server" ID=lblBidItemDetails ></asp:Label>
										
										</div>
									
										<div align="left">
											<asp:GridView   AutoGenerateColumns=false runat="server" ID=gvSuppliers CssClass=itemDetails >
						                    <Columns>
						                    <asp:TemplateField   HeaderText="Invited Suppliers" ItemStyle-CssClass="valueGridItem" HeaderStyle-CssClass="itemDetails_th">
                                            <ItemTemplate >
						                    <asp:Label runat="server" ID="lblDescription" Text='<%#Bind("SUPPLIER") %>'></asp:Label>
						                    </ItemTemplate>
						                    </asp:TemplateField>
						                    </Columns>
						                    </asp:GridView>
						                </div>
									<div>
										<div align="left">
											<asp:GridView   AutoGenerateColumns=false runat="server" ID=gvFileAttachments CssClass=itemDetails >
						<Columns>
						<asp:TemplateField   HeaderText="File Attachments" ItemStyle-CssClass="valueGridItem" HeaderStyle-CssClass="itemDetails_th">
                        <ItemTemplate >
						<asp:Label runat="server" ID="lblDescription" Text='<%#Bind("FILES") %>'></asp:Label>
						</ItemTemplate>
						</asp:TemplateField>
						</Columns>
						</asp:GridView>
										</div>
					&nbsp;</div>
<table border="0" cellpadding="0" cellspacing="0" width="100%"  id="actions">
											<tr>
												<td>
												<asp:LinkButton runat="server" ID="btnCancel" OnClick="btnCancel_Click">Cancel</asp:LinkButton>
												</td>
											</tr>
										</table>					</td>
								</tr>
							</table></td>
						</tr>
					</table>&nbsp;<!-- #EndEditable --></td>
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
&nbsp;</div>

</body>

<!-- #EndTemplate -->

</html>
