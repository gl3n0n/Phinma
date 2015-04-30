<%@ Page Language="c#" Inherits="EBid.web.vendor_screens.auctions" CodeFile="auctions.aspx.cs" %>

<%@ Register Src="../usercontrol/Vendor/Vendor_LeftNav_Notifications.ascx" TagName="Vendor_LeftNav_Notifications"
    TagPrefix="uc1" %>
<%@ Register TagPrefix="EBid" TagName="GlobalLinksNav" Src="~/web/usercontrol/GlobalLinksNav.ascx" %>
<%@ Register TagPrefix="EBid" TagName="Footer" Src="~/web/usercontrol/Vendor/Footer.ascx" %>
<%@ Register TagPrefix="EBid" TagName="TopDate" Src="~/web/usercontrol/TopDate.ascx" %>
<%@ Register TagPrefix="EBid" TagName="Vendor_TopNav_Auction" Src="~/web/usercontrol/Vendor/Vendor_TopNav_Auction.ascx" %>
<%@ Register TagPrefix="EBid" TagName="Vendor_LeftNav_Bids" Src="~/web/usercontrol/Vendor/Vendor_LeftNav_Bids.ascx" %>
<%@ Register TagPrefix="EBid" TagName="Vendor_LeftNav_Auctions" Src="~/web/usercontrol/Vendor/Vendor_LeftNav_Bids.ascx" %>
<html xmlns="http://www.w3.org/1999/xhtml" style="height: 100%;">
<head>
<%@ Register TagPrefix="EBid" TagName="jqueryLoader" Src="../usercontrol/jQuery_loader.ascx" %>
    <EBid:jqueryLoader runat="server" ID="jqueryLoader1" />
    <title id="PageTitle" runat="server"></title>
    <meta http-equiv="Content-Language" content="en-us" />
    <meta http-equiv="Content-Type" content="text/html; charset=windows-1252" />
    <link rel="stylesheet" type="text/css" href='<%= "../themes/"+Session["configTheme"]+"/css/style.css" %>' />
    <link rel="stylesheet" type="text/css" href='<%="../themes/"+Session["configTheme"]+"/css/style_v.css" %>' />
    <script type="text/javascript" src="../include/util.js"></script>
</head>
<body onLoad="SetStatus();">
    <div>
        <form id="Form1" runat="server">
            <table border="0" cellpadding="0" cellspacing="0" width="100%" id="page">
                <tr height="137px">
                    <td valign="top">
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
                                    <EBid:TopDate runat="server" ID="TopDate" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table border="0" cellpadding="0" cellspacing="0" width="100%" style="height: 100%">
                            <tr>
                                <td id="relatedInfo">
                                    <div align="left">
                                        <EBid:Vendor_LeftNav_Auctions runat="server" ID="Vendor_LeftNav_Auctions" />
                                    </div>
                                </td>
                                <td id="content">
                                    <h1>
                                        <br />
                                        New Auction Events</h1>
                                    <br />
                                    <div align="left">
                                        <asp:GridView runat="server" ID="gvAuctionsForApproval" AllowPaging="True"
                                            AllowSorting="True" AutoGenerateColumns="False" SkinID="BidEvents"
                                            EmptyDataText="No Auction Events to display at the moment." 
					    DataSourceID="dsVendorAuctions" OnRowCommand="gvAuctionsForApproval_RowCommand" >
                                            <Columns>
                                                <asp:TemplateField HeaderText="Reference No." SortExpression="AuctionRefNo">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle Width="90px" />
                                                    <ItemTemplate>
                                                        &nbsp;<asp:LinkButton CommandName="Select" ID="lnkRefNo" runat="server" Text='<%# Bind("AuctionRefNo") %>'
                                                            CommandArgument='<%# Bind("AuctionRefNo") %>' Width="95%" Visible='<%# ShowConfirmDeclineLink(Eval("ParticipationStatus").ToString(), Eval("AuctionDeadlineDiff").ToString()) %>' ></asp:LinkButton>
                                                           <asp:LinkButton CommandName="Select2" ID="lnkRefNo2" runat="server" Text='<%# Bind("AuctionRefNo") %>'
                                                            CommandArgument='<%# Bind("AuctionRefNo") %>' Visible='<%# !ShowConfirmDeclineLink(Eval("ParticipationStatus").ToString(), Eval("AuctionDeadlineDiff").ToString()) %>' ></asp:LinkButton> 
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Auction Event" SortExpression="ItemDesc">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        &nbsp;<asp:LinkButton CommandName="Select" ID="lnkDesc" runat="server" Text='<%# Bind("ItemDesc") %>'
                                                            CommandArgument='<%# Bind("AuctionRefNo") %>'  Visible='<%# ShowConfirmDeclineLink(Eval("ParticipationStatus").ToString(), Eval("AuctionDeadlineDiff").ToString()) %>' ></asp:LinkButton>
                                                           <asp:LinkButton CommandName="Select2" ID="lnkDesc2" runat="server" Text='<%# Bind("ItemDesc") %>' CommandArgument='<%# Bind("AuctionRefNo") %>'
                                                           Visible='<%# !ShowConfirmDeclineLink(Eval("ParticipationStatus").ToString(), Eval("AuctionDeadlineDiff").ToString()) %>' ></asp:LinkButton> 
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Start Date" SortExpression="AuctionStartDateTime">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" Width="140px" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblStartDate" runat="server" Text='<%# Bind("AuctionStartDateTime", "{0:D}") %>'></asp:Label><br />
                                                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("AuctionStartDateTime", "{0:T}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="End Date" SortExpression="AuctionEndDateTime">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" Width="140px" />
                                                    <ItemTemplate>                                                        
                                                        <asp:Label ID="lblEndDate" runat="server" Text='<%# Bind("AuctionEndDateTime", "{0:D}") %>'></asp:Label><br />
                                                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("AuctionEndDateTime", "{0:T}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Confirmation Deadline" SortExpression="AuctionDeadline">
													                          <HeaderStyle HorizontalAlign="Center" />
													                          <ItemStyle Width="140px" HorizontalAlign="Center" />
													                          <ItemTemplate>
													                               <asp:Label ID="lblDeadline" runat="server" Text='<%# Bind("AuctionDeadline", "{0:D}") %>' Visible='<%# ShowConfirmDeclineLink(Eval("ParticipationStatus").ToString(), Eval("AuctionDeadlineDiff").ToString()) %>'></asp:Label>
													                          	<asp:Label ID="lblConfirmationStatus" runat="server" Font-Size="11px" Text='<%# ConfirmationStatus(Eval("ParticipationStatus").ToString(), Eval("AuctionDeadlineDiff").ToString())  %>'
													                          		Visible='<%# !ShowConfirmDeclineLink(Eval("ParticipationStatus").ToString(), Eval("AuctionDeadlineDiff").ToString()) %>'></asp:Label>
													                      </ItemTemplate>
												                        </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                        <asp:SqlDataSource ID="dsVendorAuctions" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>'
                                            SelectCommand="sp_GetVendorAuctions" SelectCommandType="StoredProcedure">
                                            <SelectParameters>
                                                <asp:SessionParameter Name="VendorId" SessionField="userid" Type="Int32" />
                                                <asp:Parameter DefaultValue="0" Name="Status" Type="Int32" />
                                            </SelectParameters>
                                        </asp:SqlDataSource>
                                        <p>&nbsp;
                                            </p>
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
