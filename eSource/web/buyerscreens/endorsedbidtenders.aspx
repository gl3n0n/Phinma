<%@ Page Language="C#" AutoEventWireup="true" CodeFile="endorsedbidtenders.aspx.cs"
	Inherits="WEB_buyer_screens_EndorsedBidTenders" %>

<%@ Register TagPrefix="EBid" TagName="LeftNav" Src="~/WEB/usercontrol/Buyer/LeftNav.ascx" %>
<%@ Register TagPrefix="EBid" TagName="TopNavBids" Src="~/WEB/usercontrol/Buyer/TopNavBids.ascx" %>
<%@ Register TagPrefix="EBid" TagName="TopNav2" Src="~/WEB/usercontrol/Buyer/TopNav2.ascx" %>
<%@ Register TagPrefix="EBid" TagName="GlobalLinksNav" Src="~/web/usercontrol/GlobalLinksNav.ascx" %>
<%@ Register TagPrefix="EBid" TagName="Footer" Src="~/web/usercontrol/Footer.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml" style="height: 100%;">
<head>
<%@ Register TagPrefix="EBid" TagName="jqueryLoader" Src="../usercontrol/jQuery_loader.ascx" %>
    <EBid:jqueryLoader runat="server" ID="jqueryLoader1" />
    <title id="PageTitle" runat="server"></title>
	<meta http-equiv="Content-Language" content="en-us" />
	<meta http-equiv="Content-Type" content="text/html; charset=windows-1252" />
	<link rel="stylesheet" type="text/css" href='<%= "../themes/"+Session["configTheme"]+"/css/style.css" %>' />
    <link rel="stylesheet" type="text/css" href='<%="../themes/"+Session["configTheme"]+"/css/style_buyer.css" %>' />
</head>
<body style="height: 100%;">
	<div>
		<form id="form1" runat="server">
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
									<EBid:TopNav2 ID="TopNav2" runat="server" />
								</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td>
						<table border="0" cellpadding="0" cellspacing="0" width="100%" height="100%">
							<tr>
								<td id="relatedInfo">
									<div align="left">
										<EBid:LeftNav ID="LeftNav" runat="server" />
									</div>
									
								</td>
								<td id="content">
									<div align="left">
										<table border="0" cellpadding="0" cellspacing="0" width="100%" id="table4">
											<tr>
												<td>
													<h1>
														<br />
														Endorsed Bid Tenders</h1>
													<p>
													    These are the bid tenders you've endorsed to your immediate supervisor for awarding.
													</p>													
													<asp:GridView ID="gvBids" runat="server" SkinID="BidEvents"
													    AllowPaging="True" AutoGenerateColumns="False"
														OnRowCommand="gvBids_RowCommand" DataSourceID="dsEndorsedbidtenders" AllowSorting="True">														
														<Columns>
                                                            <asp:TemplateField HeaderText="Tender No." InsertVisible="False" SortExpression="BidTenderNo">
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="Left" Width="90px" />
                                                                <ItemTemplate>
                                                                    &nbsp;<asp:LinkButton ID="lnkBidTenderNo" runat="server" Text='<%# Bind("BidTenderNo") %>'
                                                                        CommandName="ViewBidTenderDetails" CommandArgument='<%# Bind("BidTenRef") %>'
                                                                        ToolTip="Click to view bid item and bid tender details." Width="95%"></asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Bid Item" SortExpression="DetailDesc">
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemTemplate>
                                                                    &nbsp;<asp:LinkButton ID="lnkBidTender" runat="server" Text='<%# Bind("DetailDesc") %>'
                                                                        CommandName="ViewBidTenderDetails" CommandArgument='<%# Bind("BidTenRef") %>'
                                                                        ToolTip="Click to view bid item and bid tender details."></asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Bid Event" SortExpression="ItemDesc">
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="Center" />
                                                                <ItemTemplate>                                                                    
                                                                    <asp:LinkButton ID="lnkBidEvent" runat="server" Text='<%# Bind("ItemDesc") %>'
                                                                        CommandName="ViewBidEventDetails" CommandArgument='<%# Bind("BidTenRef") %>'
                                                                        ToolTip="Click to view bid event details."></asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Bid Price" SortExpression="BidPrice">
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="Center" Width="100px" />
                                                                <ItemTemplate>                                                                    
                                                                    <asp:Label ID="lnkBidPrice" runat="server" Text='<%# Bind("BidPrice", "{0:#,##0.00}") %>'></asp:Label>&nbsp;
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Bidder" SortExpression="VendorName">
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="Center" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label51" runat="server" Text='<%# Bind("VendorName") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="PO Number" SortExpression="PONumber">
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="Center" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label5" runat="server" Text='<%# Bind("PONumber") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Date Submitted" SortExpression="DateSubmitted">
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="Center" Width="180px" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label6" runat="server" Text='<%# Bind("DateSubmitted", "{0:D}<br />{0:T}") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
														</Columns>
													</asp:GridView>
                                                    <asp:SqlDataSource ID="dsEndorsedbidtenders" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>'
                                                        SelectCommand="sp_GetBuyerEndorsedTenders" SelectCommandType="StoredProcedure">
                                                        <SelectParameters>
                                                            <asp:SessionParameter Name="BuyerId" SessionField="UserId" Type="Int32" />
                                                        </SelectParameters>
                                                    </asp:SqlDataSource>
													<br />
													<table border="0" cellpadding="0" cellspacing="0" width="100%" id="actions">
														<tr>
															<td align="left">&nbsp;
															    	
															</td>
														</tr>
													</table>
													<p>&nbsp;</p>
												</td>
											</tr>
										</table>
									</div>
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
