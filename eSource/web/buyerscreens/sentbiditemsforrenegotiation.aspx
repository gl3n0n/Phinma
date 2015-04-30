<%@ Page Language="C#" AutoEventWireup="true" CodeFile="sentbiditemsforrenegotiation.aspx.cs"
	Inherits="WEB_buyer_screens_BidItemsForRenegotiation" %>

<%@ Register TagPrefix="EBid" TagName="LeftNav" Src="~/WEB/usercontrol/Buyer/LeftNav.ascx" %>
<%@ Register TagPrefix="EBid" TagName="TopNavBids" Src="~/WEB/usercontrol/Buyer/TopNavBids.ascx" %>
<%@ Register TagPrefix="EBid" TagName="TopNav2" Src="~/WEB/usercontrol/Buyer/TopNav2.ascx" %>
<%@ Register TagPrefix="EBid" TagName="GlobalLinksNav" Src="~/web/usercontrol/GlobalLinksNav.ascx" %>
<%@ Register TagPrefix="EBid" TagName="Footer" Src="~/web/usercontrol/Footer.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml" style="height: 100%;">
<head runat="server">
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
									<p>
										&nbsp;</p>
								</td>
								<td id="content">
									<div align="left">
										<table border="0" cellpadding="0" cellspacing="0" width="100%" id="table4">
											<tr>
												<td>
													<h1>
														<br />
														Bid Items Sent for Renegotiation</h1>
													<br />
													<asp:GridView ID="gvBids" runat="server" SkinID="BidEvents"
													    AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False"
														OnRowCommand="gvBids_RowCommand" OnSorting="gvBids_Sorting" OnPageIndexChanging="gvBids_PageIndexChanging">
														<Columns>
															<asp:TemplateField HeaderText="Bid Items" ItemStyle-Width="60%">
																<ItemTemplate>
																	<asp:LinkButton CommandName="Select" ID="lnkItemDesc" runat="server" Text='<%# Bind("ItemDesc") %>'
																		CommandArgument='<%# Bind("BidRefNo") %>'></asp:LinkButton>
																</ItemTemplate>
															</asp:TemplateField>
															<asp:TemplateField HeaderText="Date Renegotiated" SortExpression="DateRenegotiated"
																ItemStyle-Width="30%">
																<HeaderStyle HorizontalAlign="Left" />
																<ItemTemplate>
																	<asp:Label ID="lblDateSubmitted" runat="server" Text='<%# Bind("DateRenegotiated") %>'></asp:Label>
																</ItemTemplate>
															</asp:TemplateField>
															<asp:TemplateField Visible="false">
																<ItemTemplate>
																	<asp:Label ID="lblBidRefNo" runat="server" Text='<%# Bind("BidRefNo") %>'></asp:Label>
																</ItemTemplate>
															</asp:TemplateField>
														</Columns>
													</asp:GridView>
													<br />
													<table border="0" cellpadding="0" cellspacing="0" width="100%" id="actions">
														<tr>
															<td align="left">
																Total Records:
																<asp:Label runat="server" ID="lblBidCount"></asp:Label></td>
															<td>
																<asp:LinkButton runat="server" ID="btnCancel" Text="Cancel" OnClick="btnCancel_Click" Width="100px"></asp:LinkButton></td>
														</tr>
													</table>
													<p>
														&nbsp;</p>
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
