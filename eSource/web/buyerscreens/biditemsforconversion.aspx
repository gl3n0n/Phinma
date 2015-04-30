<%@ Page Language="C#" AutoEventWireup="true" CodeFile="biditemsforconversion.aspx.cs" Inherits="web_buyerscreens_biditemsforconversion" %>

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
									<p>&nbsp;
										</p>
								</td>
								<td id="content">									
									<div align="left">
										<table border="0" cellpadding="0" cellspacing="0" width="100%" id="table4">
											<tr>
												<td>
													<h1>
														<br />
														Converted Bid Items</h1>
													<p>
													     These are your bid items that was converted to auction or bid items for conversion to auction.
													</p>
													 <asp:GridView ID="gvBidsForConversion" runat="server" SkinID="BidEvents"
                                                                AutoGenerateColumns="False" AllowPaging="True" AllowSorting="True" 
                                                                OnRowCommand="gvBidsForConversion_RowCommand" DataKeyNames="BidRefNo"
                                                                EmptyDataText="No Bid Events To Display."
                                                                DataSourceID="dsBidsForConversion">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="Detail No." SortExpression="BidDetailNo">
                                                                        <HeaderStyle HorizontalAlign="Center" Width="85px" />
                                                                        <ItemTemplate>
                                                                            &nbsp;<asp:LinkButton runat="server" ID="lnkDetailNo" CommandName="SelectBidItem" Text='<%# Bind("BidDetailNo") %>' 
                                                                                       CommandArgument='<%# Bind("BidDetailNo") %>'></asp:LinkButton>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Bid Item" SortExpression="Item">
                                                                        <ItemTemplate>
                                                                             &nbsp;<asp:LinkButton runat="server" ID="lnkItem" CommandName="SelectBidItem" Text='<%# Bind("BidItem") %>' 
                                                                                       CommandArgument='<%# Bind("BidDetailNo") %>'></asp:LinkButton>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Bid Event" SortExpression="BidEvent">
                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                        <ItemTemplate>
                                                                             &nbsp;
                                                                             <asp:LinkButton runat="server" ID="lnkEvent" CommandName="SelectBidEvent" Text='<%# Bind("BidEvent") %>' 
                                                                                       CommandArgument='<%# Bind("BidRefNo") %>'></asp:LinkButton>
                                                                        </ItemTemplate>
                                                                        <ItemStyle HorizontalAlign="Center" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Status" SortExpression="ItemStatus">
                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblStatus" runat="server" Text='<%# Bind("ItemStatus") %>' Visible='<%# !IsVisible(Eval("ItemStatus","{0}"))%>'></asp:Label>
                                                                            <asp:LinkButton ID="lnkConvert" runat="server" Text="Convert"  Visible='<%# IsForConversion(Eval("SavedAsDraftOnAuction","{0}"),Eval("ItemStatus","{0}"))%>' CommandName="ConvertItem" CommandArgument='<%# Eval("BidDetailNo", "{0}") %>' />
                                                                            <asp:LinkButton ID="lnkContConvert" runat="server" Text='On Draft' Visible='<%# IsDrafted(Eval("SavedAsDraftOnAuction","{0}"),Eval("ItemStatus","{0}"))%>' CommandName="EditAuctionDraft" CommandArgument='<%# Eval("AuctionRefNoOnAuction", "{0}") %>'></asp:LinkButton>
                                                                        </ItemTemplate>
                                                                        <ItemStyle HorizontalAlign="Center" width="120px"/>
                                                                    </asp:TemplateField>                                                                    
                                                                </Columns>
                                                            </asp:GridView>
                                                            <asp:SqlDataSource ID="dsBidsForConversion" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>'
                                                                SelectCommand="sp_GetBuyerBidItemsForConversion" SelectCommandType="StoredProcedure">
                                                                 <SelectParameters>
                                                                    <asp:SessionParameter Name="BuyerId" SessionField="UserId" Type="Int32" />
                                                                </SelectParameters>
                                                            </asp:SqlDataSource>
													<br />
													<table border="0" cellpadding="0" cellspacing="0" width="100%" id="actions">
														<tr>
														    <td>&nbsp;
														    
														    </td>
														</tr>
													</table>
													<p>&nbsp;
														</p>
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
						<EBid:Footer runat="server" ID="Footer1" />						
					</td>
				</tr>
			</table>
		</form>
	</div>
</body>
</html>
