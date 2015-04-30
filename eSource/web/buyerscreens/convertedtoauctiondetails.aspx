<%@ Page Language="C#" AutoEventWireup="true" CodeFile="convertedtoauctiondetails.aspx.cs" Inherits="WEB_buyer_screens_ConvertedToAuctionDetails" MaintainScrollPositionOnPostback="true" %>
<%@ Register  TagPrefix="EBid" TagName="LeftNav" Src="~/WEB/usercontrol/Buyer/LeftNav.ascx" %>
<%@ Register  TagPrefix="EBid" TagName="TopNavBids" Src="~/WEB/usercontrol/Buyer/TopNavBids.ascx" %>
<%@ Register  TagPrefix="EBid" TagName="TopNav2" Src="~/WEB/usercontrol/Buyer/TopNav2.ascx" %>
<%@ Register  TagPrefix="EBid" TagName="GlobalLinksNav" Src="~/web/usercontrol/GlobalLinksNav.ascx" %>
<%@ Register  TagPrefix="EBid" TagName="CommentBox" Src="~/web/usercontrol/CommentBox.ascx" %>
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
</head>

<body style="height: 100%;">
<form runat="server" id=frm1>
<div align="left">
<div align="left">
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
		<tr height='100%'>
			<td>
			<table border="0" cellpadding="0" cellspacing="0" width="100%" height=100% >
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
						</td>
					<td id="content">
					<!-- #BeginEditable "contentArea" -->
                    <table border="0" cellpadding="0" cellspacing="0" width="100%" id="page0">
						<tr>
							<td valign=top>
							<table border="0" cellpadding="0" cellspacing="0" width="100%" >
								<tr>
									<td id="content0">
									<h1><br />Bid Event Detail for Conversion to Auction</h1>
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
										<div>
                                            <asp:GridView   AutoGenerateColumns=false runat="server" ID=gvBidItemDetails CssClass=itemDetails >
						<Columns>
						<asp:TemplateField  HeaderText="Item" HeaderStyle-CssClass="itemDetails_th" ItemStyle-CssClass=itemDetails_td >
						<ItemStyle CssClass="itemDetails_td"  />
						<ItemTemplate>
						<asp:Label runat="server" ID="lblItem" Text='<%#Bind("SKU") %>'></asp:Label>
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
										</div>
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
						                    <br />
						                </div>
										
									
									
									
									<br />
									<div align="left">
										<table border="0" cellpadding="0" cellspacing="0" width="100%" id="actions">
											<tr>
												<td>
												<asp:linkbutton runat="server" ID=lnkOk OnClick="lnkOk_Click">Ok</asp:linkbutton></td>
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
</div>
</div>
</form>
</body>

<!-- #EndTemplate -->

</html>
