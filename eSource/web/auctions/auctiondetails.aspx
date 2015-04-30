<%@ Page Language="C#" AutoEventWireup="true" CodeFile="auctiondetails.aspx.cs" Inherits="web_onlineAuction_AuctionDetails"
	Theme="default" %>

<%@ Register Src="../usercontrol/AuctionVendor/auctionattachments.ascx" TagName="auctionattachments" TagPrefix="uc2" %>

<%@ Register Src="../usercontrol/AuctionVendor/AuctionVendor_TopNav.ascx" TagName="AuctionVendor_TopNav"
	TagPrefix="uc1" %>
<%@ Register TagPrefix="EBid" TagName="GlobalLinksNav" Src="~/web/usercontrol/GlobalLinksNav.ascx" %>
<%@ Register TagPrefix="EBid" TagName="Footer" Src="~/web/usercontrol/Footer.ascx" %>
<%@ Register TagPrefix="EBid" TagName="TopDate" Src="~/web/usercontrol/TopDate.ascx" %>
<html xmlns="http://www.w3.org/1999/xhtml" style="height: 100%;">
<head>
	<title id="PageTitle" runat="server"></title>
	<meta http-equiv="Content-Language" content="en-us" />
	<meta http-equiv="Content-Type" content="text/html; charset=windows-1252" />
	<link rel="stylesheet" type="text/css" href='<%= "../themes/"+Session["configTheme"]+"/css/style.css" %>' />
    <link rel="stylesheet" type="text/css" href='<%="../themes/"+Session["configTheme"]+"/css/style_oa.css" %>' />
</head>
<body style="height: 100%;">
	<div>
		<form runat="server">
			<table border="0" cellpadding="0" cellspacing="0" width="100%" id="page">
				<tr>
					<td valign="top" height="137px">
						<table border="0" cellpadding="0" cellspacing="0" width="100%">
							<tr>
								<td>
									<div align="left" id="masthead">
										<EBid:GlobalLinksNav runat="server" ID="GlobalLinksNav" />
									</div>
								</td>
							</tr>
							<tr>
								<td>
									<uc1:AuctionVendor_TopNav ID="AuctionVendor_TopNav1" runat="server" />
								</td>
							</tr>
							<tr>
								<td>
									<EBid:TopDate runat="server" ID="TopDate" />
								</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td valign="top">
						<table border="0" cellpadding="0" cellspacing="0" width="100%">
							<tr>
								<td valign="top" id="content">
									<h1>
										<br />
										Auction Event Details</h1>
								</td>
							</tr>
							<tr>
								<td valign="top" style="padding-left: 20px; padding-right: 2px">
									<br />
									<asp:DetailsView ID="dvAuctionDetails" runat="server" SkinID="EventDetails" AutoGenerateRows="False"
										DataKeyNames="AuctionRefNo" DataSourceID="dsAuctionDetails" HeaderText="&nbsp;">
										<Fields>
											<asp:TemplateField HeaderText="&nbsp;Description" SortExpression="ItemDesc">
												<ItemTemplate>
													&nbsp;<asp:Label ID="Label1" runat="server" Text='<%# Eval("ItemDesc") %>'></asp:Label>
												</ItemTemplate>
											</asp:TemplateField>
											<asp:TemplateField HeaderText="&nbsp;Company" SortExpression="Company">
												<ItemTemplate>
													&nbsp;<asp:Label ID="Label1" runat="server" Text='<%# Eval("Company") %>'></asp:Label>
												</ItemTemplate>
											</asp:TemplateField>
											<asp:TemplateField HeaderText="&nbsp;Requestor" SortExpression="Requestor">
												<ItemTemplate>
													&nbsp;<asp:Label ID="Label2" runat="server" Text='<%# Eval("Requestor") %>'></asp:Label>
												</ItemTemplate>
											</asp:TemplateField>
											<asp:TemplateField HeaderText="&nbsp;PR Number" SortExpression="PRRefNo">
												<ItemTemplate>
													&nbsp;<asp:Label ID="Label3" runat="server" Text='<%# Eval("PRRefNo") %>'></asp:Label>
												</ItemTemplate>
											</asp:TemplateField>
											<asp:TemplateField HeaderText="&nbsp;PR Date" SortExpression="PRDate">
												<ItemTemplate>
													&nbsp;<asp:Label ID="Label4" runat="server" Text='<%# Eval("PRDate", "{0:D}") %>'></asp:Label>
												</ItemTemplate>
											</asp:TemplateField>
											<asp:TemplateField HeaderText="&nbsp;Group/Department/Section" SortExpression="GroupDeptSecName">
												<ItemTemplate>
													&nbsp;<asp:Label ID="Label5" runat="server" Text='<%# Eval("GroupDeptSecName") %>'></asp:Label>
												</ItemTemplate>
											</asp:TemplateField>
											<asp:TemplateField HeaderText="&nbsp;Auction Reference Number" SortExpression="BidRefNo">
												<ItemTemplate>
													&nbsp;<asp:Label ID="Label6" runat="server" Text='<%# Eval("AuctionRefNo") %>'></asp:Label>
												</ItemTemplate>
											</asp:TemplateField>
											<asp:TemplateField HeaderText="&nbsp;Category" SortExpression="CategoryName">
												<ItemTemplate>
													&nbsp;<asp:Label ID="Label7" runat="server" Text='<%# Eval("CategoryName") %>'></asp:Label>
												</ItemTemplate>
											</asp:TemplateField>
										</Fields>
									</asp:DetailsView>
									<asp:SqlDataSource ID="dsAuctionDetails" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>'
										SelectCommand="sp_GetAuctionDetails" SelectCommandType="StoredProcedure">
										<SelectParameters>
											<asp:SessionParameter DefaultValue="0" Name="AuctionRefNo" SessionField="AuctionRefNo"
												Type="Int32" />
										</SelectParameters>
									</asp:SqlDataSource>
								</td>
								<td valign="top" style="padding-left: 2; padding-right: 10px">
									<br />
									<asp:DetailsView ID="dvEventDetails" runat="server" SkinID="EventDetails" AutoGenerateRows="False"
										DataKeyNames="AuctionRefNo" DataSourceID="dsAuctionDetails" HeaderText="&nbsp;">
										<Fields>
											<asp:TemplateField HeaderText="&nbsp;Auction Type" SortExpression="Type">
												<ItemTemplate>
													&nbsp;<asp:Label ID="Label4" runat="server" Text='<%# Eval("Type") %>'></asp:Label>
												</ItemTemplate>
											</asp:TemplateField>
											<asp:TemplateField HeaderText="&nbsp;Confirmation Deadline" SortExpression="AuctionDeadline">
												<ItemTemplate>
													&nbsp;<asp:Label ID="Label4" runat="server" Text='<%# Format(Eval("AuctionDeadline").ToString()) %>'></asp:Label>
												</ItemTemplate>
											</asp:TemplateField>
											<asp:TemplateField HeaderText="&nbsp;Start Date & Time" SortExpression="AuctionStartDateTime">
												<ItemTemplate>
													&nbsp;<asp:Label ID="Label3" runat="server" Text='<%# Format(Eval("AuctionStartDateTime").ToString()) %>'></asp:Label>
												</ItemTemplate>
											</asp:TemplateField>											
											<asp:TemplateField HeaderText="&nbsp;End Date & Time" SortExpression="AuctionEndDateTime">
												<ItemTemplate>
													&nbsp;<asp:Label ID="Label1" runat="server" Text='<%# Format(Eval("AuctionEndDateTime").ToString()) %>'></asp:Label>
												</ItemTemplate>
											</asp:TemplateField>
											<asp:TemplateField HeaderText="&nbsp;Currency">
												<ItemTemplate>
													&nbsp;<asp:Label ID="lblCurrency" runat="server" Text='<%# Eval("Currency") %>'></asp:Label>
												</ItemTemplate>
											</asp:TemplateField>
										</Fields>
									</asp:DetailsView>
								</td>
							</tr>
							<tr>
								<td colspan="2">
									<p>
										&nbsp;</p>
								</td>
							</tr>
							<tr>
								<td valign="top" colspan="2" style="padding-left: 20px; padding-right: 10px">
									<asp:GridView ID="gvAuctionedItems" runat="server" SkinID="AuctionedItems" AllowPaging="True"
										AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="AuctionDetailNo"
										DataSourceID="dsAuctionedItems">
										<Columns>
											<asp:TemplateField HeaderText="&nbsp;Item&nbsp;" SortExpression="ItemName">
												<ItemTemplate>
													&nbsp;<asp:Label ID="Label1" runat="server" Text='<%# Eval("ItemName") %>'></asp:Label>
												</ItemTemplate>
											</asp:TemplateField>
											<asp:TemplateField HeaderText="&nbsp;Description&nbsp;" SortExpression="Description">
												<ItemTemplate>
													&nbsp;<asp:Label ID="Label2" runat="server" Text='<%# Eval("Description") %>'></asp:Label>
												</ItemTemplate>
											</asp:TemplateField>
											<asp:TemplateField HeaderText="&nbsp;Quantity&nbsp;" SortExpression="Quantity">
											    <HeaderStyle HorizontalAlign="Center" />
												<ItemStyle Width="80px" HorizontalAlign="Center" />
												<ItemTemplate>
													&nbsp;<asp:Label ID="Label3" runat="server" Text='<%# Eval("Quantity") %>'></asp:Label>
												</ItemTemplate>
											</asp:TemplateField>
											<asp:TemplateField HeaderText="&nbsp;Unit Of Measure&nbsp;" SortExpression="UnitOfMeasure">
												<HeaderStyle HorizontalAlign="Center" />
												<ItemStyle Width="94px" HorizontalAlign="Center" />
												<ItemTemplate>
													&nbsp;<asp:Label ID="Label4" runat="server" Text='<%# Eval("UnitOfMeasure") %>'></asp:Label>
												</ItemTemplate>
											</asp:TemplateField>
											<asp:TemplateField HeaderText="Starting Price" SortExpression="StartingPrice">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle Width="120px" HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    &nbsp;<asp:Label ID="Label6" runat="server" Text='<%# Bind("StartingPrice", "{0:#,##0.00}") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Increment / Decrement" SortExpression="IncrementDecrement">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle Width="130px" HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    &nbsp;<asp:Label ID="Label7" runat="server" Text='<%# Bind("IncrementDecrement", "{0:#,##0.00}") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
										</Columns>
									</asp:GridView>
									<asp:SqlDataSource ID="dsAuctionedItems" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>'
										SelectCommand="sp_GetAuctionedItemsByAuctionRefNo" SelectCommandType="StoredProcedure">
										<SelectParameters>
											<asp:SessionParameter Name="AuctionRefNo" SessionField="AuctionRefNo" Type="Int32" />
										</SelectParameters>
									</asp:SqlDataSource>
                                    <uc2:auctionattachments ID="Auctionattachments1" runat="server" />
								</td>
							</tr>
							<tr>
								<td colspan="2">
									<p>
										&nbsp;</p>
								</td>
							</tr>
							<tr>
								<td colspan="2" style="padding-left: 20px; padding-right: 10px" id="actions">
									<asp:LinkButton ID="lnkOk" runat="server" OnClick="lnkOk_Click">OK</asp:LinkButton>
								</td>
							</tr>
							<tr>
								<td colspan="2">
									<p>
										&nbsp;</p>
								</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td id="footer" height="50px">
						<EBid:Footer runat="server" ID="Footer" />
					</td>
				</tr>
			</table>
		</form>
	</div>
</body>
</html>
