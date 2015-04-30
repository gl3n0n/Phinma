<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ongoingauctionevents.aspx.cs"
	Inherits="web_onlineAuction_OngoingAuctionEvents" Theme="default" %>

<%@ Register Src="../usercontrol/AuctionVendor/AuctionVendor_TopNav.ascx" TagName="AuctionVendor_TopNav"
	TagPrefix="uc1" %>
<%@ Register TagPrefix="EBid" TagName="GlobalLinksNav" Src="~/web/usercontrol/GlobalLinksNav.ascx" %>
<%@ Register TagPrefix="EBid" TagName="Footer" Src="~/web/usercontrol/Footer.ascx" %>
<%@ Register TagPrefix="EBid" TagName="TopDate" Src="~/web/usercontrol/TopDate.ascx" %>
<%@ Register TagPrefix="EBid" TagName="AuctionVendor_TopNav_Home" Src="~/web/usercontrol/AuctionVendor/AuctionVendor_TopNav_Ongoing.ascx" %>
<html xmlns="http://www.w3.org/1999/xhtml" style="height: 100%;">
<head>
	<title id="PageTitle" runat="server"></title>	
	<meta http-equiv="Content-Language" content="en-us" />
	<meta http-equiv="Content-Type" content="text/html; charset=windows-1252" />
	<link rel="stylesheet" type="text/css" href='<%= "../themes/"+Session["configTheme"]+"/css/style.css" %>' />
    <link rel="stylesheet" type="text/css" href='<%="../themes/"+Session["configTheme"]+"/css/style_oa.css" %>' />
	
	<script type="text/javascript">
	<!--	    
	    function showWindow(redirecturl,title)
        {
            window.open(redirecturl,title, 'toolbar=no, menubar=no, resizable=yes , scrollbars=yes');
        }
    //-->
	</script>
</head>
<body style="height: 100%;">
	<div>
		<form runat="server">
			<table border="0" cellpadding="0" cellspacing="0" width="100%" id="page">
				<tr>
					<td valign="top" style="height: 137px">
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
									<EBid:AuctionVendor_TopNav_Home ID="AuctionVendor_TopNav_Home1" runat="server" />
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
										Ongoing Auction Events</h1>
									<div align="left">
										<br />
										<asp:GridView ID="gvAuctionEvents" runat="server" AutoGenerateColumns="False" SkinID="AuctionEvents"
											DataKeyNames="AuctionRefNo" DataSourceID="dsOngoingAuctions" AllowPaging="True"
											AllowSorting="True" EmptyDataText="There are no ongoing auctions at this moment." 
											OnRowDataBound='gvAuctionEvents_RowDataBound'>
											<EmptyDataRowStyle HorizontalAlign="center" Height="25px" />
											<Columns>
											    <asp:TemplateField HeaderText="&#160;Reference No.&#160;" SortExpression="ItemDesc">
													<HeaderStyle HorizontalAlign="Center" />
													<ItemStyle Width="90px" HorizontalAlign="Center" />
													<ItemTemplate>
														&nbsp;<asp:LinkButton ID="lblauctionrefno" runat="server" Text='<%# Bind("AuctionRefNo") %>'
															CommandName="SelectAuctionItem" CommandArgument='<%#Bind("AuctionRefNo") %>'
															OnCommand="lblAuctionEvents_Command">'></asp:LinkButton>
													</ItemTemplate>
												</asp:TemplateField>
												
												<asp:TemplateField HeaderText="&#160;Auction Events&#160;" SortExpression="ItemDesc">
													<HeaderStyle HorizontalAlign="Center" />
													<ItemTemplate>
														&nbsp;<asp:LinkButton ID="lblAuctionEvents" runat="server" Text='<%# Bind("ItemDesc") %>'
															CommandName="SelectAuctionItem" CommandArgument='<%#Bind("AuctionRefNo") %>'
															OnCommand="lblAuctionEvents_Command">'></asp:LinkButton>
													</ItemTemplate>
												</asp:TemplateField>
												<asp:TemplateField HeaderText="&#160;Start Date and Time&#160;" SortExpression="AuctionStartDateTime">
													<HeaderStyle HorizontalAlign="Center" />
													<ItemStyle Width="200px" HorizontalAlign="Center" />
													<ItemTemplate>
														<asp:Label ID="lblStartDate" runat="server" Text='<%# Eval("AuctionStartDateTime", "{0:D}<br / >{0:t}") %>'></asp:Label>
													</ItemTemplate>
												</asp:TemplateField>
												<asp:TemplateField HeaderText="&#160;End Date and Time&#160;" SortExpression="AuctionEndDateTime">
													<HeaderStyle HorizontalAlign="Center" />
													<ItemStyle Width="200px" HorizontalAlign="Center" />
													<ItemTemplate>
														<asp:Label ID="lblEndDate" runat="server" Text='<%# Eval("AuctionEndDateTime", "{0:D}<br / >{0:t}") %>'></asp:Label>
													</ItemTemplate>
												</asp:TemplateField>
												<asp:TemplateField>
													<HeaderStyle HorizontalAlign="Center" />
													<ItemStyle Width="100px" HorizontalAlign="Center" />
													<ItemTemplate>
                                                        <asp:HyperLink ID="lnkParticipate" runat="server" Visible='<%# ShowParticipateLink(Eval("ParticipationStatus").ToString()) %>'>Participate</asp:HyperLink>
														<asp:LinkButton ID="lnkConfirm" runat="server" Text="Confirm | Decline" Visible='<%# ShowConfirmDeclineLink(Eval("ParticipationStatus").ToString(), Eval("AuctionDeadlineDiff").ToString()) %>'
														    CommandName="ConfirmCommand" CommandArgument='<%# Eval("AuctionRefNo") %>' OnCommand="lnkConfirm_Command"></asp:LinkButton>
														<asp:Label ID="lblConfirmationStatus" runat="server" Font-Size="11px" Text='<%# ConfirmationStatus(Eval("ParticipationStatus").ToString(), Eval("AuctionDeadlineDiff").ToString())  %>'
															Visible='<%# !ShowConfirmDeclineLink(Eval("ParticipationStatus").ToString(), Eval("AuctionDeadlineDiff").ToString()) %>'></asp:Label>
													</ItemTemplate>
												</asp:TemplateField>
											</Columns>
										</asp:GridView>
										<asp:SqlDataSource ID="dsOngoingAuctions" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>'
											SelectCommand="sp_GetOngoingAuctions" SelectCommandType="StoredProcedure">
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
				<tr>
					<td>
						&nbsp;</td>
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
