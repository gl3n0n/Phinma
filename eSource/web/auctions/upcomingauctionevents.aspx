<%@ Page Language="C#" AutoEventWireup="true" CodeFile="upcomingauctionevents.aspx.cs"
	Inherits="web_onlineAuction_UpcomingAuctionEvents" Theme="default" %>
<%@ Register TagPrefix="EBid" TagName="GlobalLinksNav" Src="~/web/usercontrol/GlobalLinksNav.ascx" %>
<%@ Register TagPrefix="EBid" TagName="Footer" Src="~/web/usercontrol/Footer.ascx" %>
<%@ Register TagPrefix="EBid" TagName="TopDate" Src="~/web/usercontrol/TopDate.ascx" %>
<%@ Register TagPrefix="EBid" TagName="AuctionVendor_TopNav_Upcoming" Src="~/web/usercontrol/AuctionVendor/AuctionVendor_TopNav_Upcoming.ascx" %>
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
					<td valign="top" height="137">
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
									<EBid:AuctionVendor_TopNav_Upcoming runat="server" ID="AuctionVendor_TopNav_Upcoming" />									
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
										Upcoming Auction Events</h1>
									<div align="left">
										<br />
										<asp:GridView ID="gvAuctionEvents" runat="server" AutoGenerateColumns="False" DataKeyNames="AuctionRefNo"
											SkinID="AuctionEvents" AllowPaging="True" AllowSorting="True" 
											EmptyDataText="There are no upcoming auctions at this moment." DataSourceID="dsUpcomingAuctions">
											<EmptyDataRowStyle HorizontalAlign="center" Height="25px" />
											<Columns>
											    <asp:TemplateField HeaderText="&#160;Reference No.&#160;" SortExpression="AuctionRefNo">
													<HeaderStyle HorizontalAlign="Center" />
													<ItemStyle Width="90px" HorizontalAlign="Center" />													
													<ItemTemplate>
														&nbsp;<asp:LinkButton ID="lblrefno" runat="server" Text='<%# Bind("AuctionRefNo") %>'
															CommandName="SelectAuctionItem" CommandArgument='<%#Bind("AuctionRefNo") %>' OnCommand="lblAuctionEvents_Command">'></asp:LinkButton>
													</ItemTemplate>
												</asp:TemplateField>
												
												<asp:TemplateField HeaderText="&#160;Auction Events&#160;" SortExpression="ItemDesc">
													<HeaderStyle HorizontalAlign="Center" />
													<ItemTemplate>
														&nbsp;<asp:LinkButton ID="lblAuctionEvents" runat="server" Text='<%# Bind("ItemDesc") %>'
															CommandName="SelectAuctionItem" CommandArgument='<%#Bind("AuctionRefNo") %>' OnCommand="lblAuctionEvents_Command">'></asp:LinkButton>
													</ItemTemplate>
												</asp:TemplateField>
												<asp:TemplateField HeaderText="&#160;Confirmation Deadline&#160;" SortExpression="AuctionDeadline">
													<HeaderStyle HorizontalAlign="Center" />
													<ItemStyle Width="180px" HorizontalAlign="Center" />
													<ItemTemplate>
														<asp:Label ID="Label1" runat="server" Text='<%# Eval("AuctionDeadline", "{0:D}") %>'></asp:Label>
													</ItemTemplate>
												</asp:TemplateField>
												<asp:TemplateField HeaderText="&#160;Event Date and Time&#160;" SortExpression="AuctionStartDateTime">
													<HeaderStyle HorizontalAlign="Center" />
													<ItemStyle Width="240px" HorizontalAlign="Center" />
													<ItemTemplate>														
														&nbsp;<label style="font-family:Arial; font-size: 9px;">FROM:</label>
														&nbsp;<asp:Label ID="lblStartDate" runat="server" Text='<%# Eval("AuctionStartDateTime", "{0:D}<br />{0:T}") %>'></asp:Label><br />
														&nbsp;<label style="font-family:Arial; font-size: 9px;">TO:</label>
														&nbsp;<asp:Label ID="lblEndDate" runat="server" Text='<%# Eval("AuctionEndDateTime", "{0:D}<br />{0:T}") %>'></asp:Label>												
													</ItemTemplate>
												</asp:TemplateField>
												<asp:TemplateField HeaderText="&#160;Duration&#160;" SortExpression="Duration">
													<HeaderStyle HorizontalAlign="Center" />
													<ItemStyle Width="90px" HorizontalAlign="Center" />
													<ItemTemplate>
														<asp:Label ID="lblDuration" runat="server" Text='<%# Eval("Duration") %>'></asp:Label>
													</ItemTemplate>
												</asp:TemplateField>
												<asp:TemplateField>
													<HeaderStyle HorizontalAlign="Center" />
													<ItemStyle Width="120px" HorizontalAlign="Center" />
													<ItemTemplate>
														<asp:Panel ID="pnlConfirmation" runat="server" Visible='<%# ShowConfirmation(Eval("ConfirmationStatus").ToString(), Eval("DeadlineReached").ToString()) %>'>
															<asp:LinkButton ID="lblAction" runat="server" Text="Confirm | Decline" CommandArgument='<%# Eval("AuctionRefNo") %>' OnCommand="lblAction_Command"></asp:LinkButton>
														</asp:Panel>
														<asp:Label ID="lblConfirmationStatus" runat="server" Visible='<%# !ShowConfirmation(Eval("ConfirmationStatus").ToString(), Eval("DeadlineReached").ToString()) %>' Text='<%# ConfirmationStatus(Eval("ConfirmationStatus").ToString()) %>'></asp:Label>														
													</ItemTemplate>
												</asp:TemplateField>
											</Columns>
										</asp:GridView>
										<asp:SqlDataSource ID="dsUpcomingAuctions" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>'
											SelectCommand="sp_GetUpcomingAuctions" SelectCommandType="StoredProcedure">
											<SelectParameters>
												<asp:SessionParameter DefaultValue="0" Name="UserId" SessionField="UserId" Type="Int32" />
												<asp:SessionParameter DefaultValue="0" Name="UserType" SessionField="UserType" Type="Int32" />
											</SelectParameters>
										</asp:SqlDataSource>
									</div>
								</td>
							</tr>
							<tr>
								<td>
									&nbsp;
								</td>
							</tr>
							<tr>
								<td align="center">
									<asp:Label ID="lblMessage" runat="server" Text="" Font-Size="11px" ForeColor="red"></asp:Label>
								</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr><td>&nbsp;</td></tr>
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
