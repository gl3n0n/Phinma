<%@ Page Language="C#" AutoEventWireup="true" CodeFile="bidsforrenegotiation.aspx.cs" Inherits="web_buyer_screens_bidsforrenegotiation" EnableEventValidation="false"%>

<%@ Register Assembly="Calendar" Namespace="CalendarControl" TagPrefix="cc1" %>

<%@ Register TagPrefix="EBid" TagName="LeftNav" Src="~/WEB/usercontrol/Buyer/LeftNav.ascx" %>
<%@ Register TagPrefix="EBid" TagName="TopNavBids" Src="~/WEB/usercontrol/Buyer/TopNavBids.ascx" %>
<%@ Register TagPrefix="EBid" TagName="TopNav2" Src="~/WEB/usercontrol/Buyer/TopNav2.ascx" %>
<%@ Register TagPrefix="EBid" TagName="GlobalLinksNav" Src="~/web/usercontrol/GlobalLinksNav.ascx" %>
<%@ Register TagPrefix="EBid" TagName="Footer" Src="~/web/usercontrol/Footer.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<%@ Register TagPrefix="EBid" TagName="jqueryLoader" Src="../usercontrol/jQuery_loader.ascx" %>
    <EBid:jqueryLoader runat="server" ID="jqueryLoader1" />
    <title id="PageTitle" runat="server"></title>
	<meta http-equiv="Content-Language" content="en-us" />
	<meta http-equiv="Content-Type" content="text/html; charset=windows-1252" />
    <link rel="stylesheet" type="text/css" href='<%= "../themes/"+Session["configTheme"]+"/css/style.css" %>' />
    <link rel="stylesheet" type="text/css" href='<%="../themes/"+Session["configTheme"]+"/css/style_buyer.css" %>' />
</head>
<body>
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
														Bid Tenders For Clarification</h1>
													<p>
													    These are the bid tenders for clarification or under clarification with vendor/supplier.
													</p>
													<asp:GridView ID="gvBids" runat="server" SkinID="BidEvents" 
													    AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="BidDetailNo,BidRefNo" DataSourceID="dsRenegotiatedBidItemTenders" OnRowCommand="gvBids_RowCommand" OnRowCreated="gvBids_RowCreated">														
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Detail No" InsertVisible="False" SortExpression="BidDetailNo">
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle Width="90px" />
                                                                <ItemTemplate>
                                                                    &nbsp;<asp:LinkButton ID="LinkButton1" runat="server" Text='<%# Bind("BidDetailNo") %>'
                                                                        CommandName="ViewBidItemDetails" CommandArgument='<%# String.Format("{1}|{0}", Eval("BidDetailNo"), Eval("BidRefNo")) %>' Width="95%"></asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Bid Item" SortExpression="DetailDesc">
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemTemplate>
                                                                    &nbsp;
                                                                    <asp:LinkButton ID="LinkButton2" runat="server" Text='<%# Bind("DetailDesc") %>'
                                                                        CommandName="ViewBidItemDetails" CommandArgument='<%# String.Format("{1}|{0}", Eval("BidDetailNo"), Eval("BidRefNo")) %>'></asp:LinkButton>
                                                                    <br />&nbsp;&nbsp;<asp:Label ID="lblShowCount" runat="server" ForeColor="gray" Text='<%# showBidTenderCount(Eval("RenegotiatedTenderCount")) %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Bid Event" SortExpression="ItemDesc">
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="Center" />
                                                                <ItemTemplate>                                                                    
                                                                    <asp:LinkButton ID="LinkButton3" runat="server" Text='<%# Bind("ItemDesc") %>'
                                                                        CommandName="ViewBidEventDetails" CommandArgument='<%# Bind("BidRefNo") %>'></asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Clarification Deadline" SortExpression="RenegotiationDeadline">
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="Center" Width="180px" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("RenegotiationDeadline", "{0:D}<br />{0:T}") %>'></asp:Label>                                                                    
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Status" SortExpression="Status">
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="Center" />
                                                                <ItemTemplate>
                                                                    <asp:Panel ID="pnlStatus" runat="server" Visible='<%# showStatus(Eval("IsRenegotationDeadlineSet")) %>'>
                                                                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("Status") %>'></asp:Label>
                                                                    </asp:Panel>           
                                                                    <asp:Panel ID="pnlRenegotiationDeadline" runat="server" Visible='<%# showSetRenegotiationDeadline(Eval("IsRenegotationDeadlineSet")) %>'>
                                                                        <asp:LinkButton ID="lnkSetRenegotiationDeadline" runat="server" 
                                                                            CommandName="Select">Set Clarification Deadline</asp:LinkButton>
                                                                        <asp:Panel ID="pnlSetRenegotiationDeadline" runat="server" Visible="false">
                                                                            <cc1:JSCalendar ID="clndrRenegotiationDeadline" runat="server" ImageURL="../../calendar/img.gif"
                                                                                EnableViewState="true" ScriptsBasePath="../../calendar/" DateFormat="MM/dd/yyyy"
                                                                                Width="120px" ReadOnly="false" MaxLength="10" TabIndex="-1"></cc1:JSCalendar>
                                                                            <br />
                                                                            &nbsp;
                                                                            <asp:TextBox ID="txtDeadlineHH" runat="server" MaxLength="2" Width="20px">00</asp:TextBox>
                                                                            <asp:RangeValidator ID="RangeValidator1" runat="server" ErrorMessage="*" SetFocusOnError="true"
                                                                                ControlToValidate="txtDeadlineHH" MinimumValue="1" Type="Integer" MaximumValue="12" Display="Dynamic"></asp:RangeValidator>
                                                                            :
                                                                            <asp:TextBox ID="txtDeadlineMM" runat="server" MaxLength="2" Width="20px">00</asp:TextBox>
                                                                            <asp:RangeValidator ID="RangeValidator2" runat="server" ErrorMessage="*"
                                                                                ControlToValidate="txtDeadlineMM" MinimumValue="0" Type="Integer" MaximumValue="59" Display="Dynamic"></asp:RangeValidator>
                                                                            :
                                                                            <asp:TextBox ID="txtDeadlineSS" runat="server" MaxLength="2" Width="20px">00</asp:TextBox>
                                                                            <asp:RangeValidator ID="RangeValidator3" runat="server" ErrorMessage="*"
                                                                                ControlToValidate="txtDeadlineSS" MinimumValue="0" Type="Integer" MaximumValue="59" Display="Dynamic"></asp:RangeValidator>
                                                                            &nbsp;<asp:DropDownList
                                                                                ID="ddlDeadline" runat="server">
                                                                                <asp:ListItem Value="AM">AM</asp:ListItem>
                                                                                <asp:ListItem Value="PM">PM</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                            <br />
                                                                            <asp:RequiredFieldValidator ID="rfvRenegotiationDeadline" runat="server" 
                                                                                ErrorMessage="Clarification deadline required.<br />" ControlToValidate="clndrRenegotiationDeadline"
                                                                                Display="Dynamic"></asp:RequiredFieldValidator>
                                                                            <asp:CustomValidator ID="cvRenegotiationDeadline" runat="server" 
                                                                                ErrorMessage="Invalid clarification deadline.<br />" ControlToValidate="clndrRenegotiationDeadline" 
                                                                                Display="Dynamic" OnServerValidate="cvRenegotiationDeadline_ServerValidate" ></asp:CustomValidator>
                                                                           <asp:LinkButton ID="lnkOk" runat="server" CommandName="OK" CommandArgument='<%# Bind("BidRefNo") %>' >OK</asp:LinkButton>&nbsp;&nbsp;
                                                                            <asp:LinkButton ID="lnkCancel" runat="server" CommandName="Cancel" CausesValidation="false">Cancel</asp:LinkButton>
                                                                        </asp:Panel>
                                                                    </asp:Panel>                                                                                                                             
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
													</asp:GridView>
                                                    <asp:SqlDataSource ID="dsRenegotiatedBidItemTenders" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>'
                                                        SelectCommand="sp_GetBuyerRenegotiatedItemTenders" SelectCommandType="StoredProcedure">
                                                        <SelectParameters>
                                                            <asp:SessionParameter DefaultValue="0" Name="BuyerId" SessionField="UserId" Type="Int32" />
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
