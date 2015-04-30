<%@ Page Language="C#" AutoEventWireup="true" CodeFile="confirmedinvitations.aspx.cs"
    Inherits="web_vendorscreens_ConfirmedInvitations" %>

<%@ Register Src="../usercontrol/Vendor/Vendor_LeftNav_Notifications.ascx" TagName="Vendor_LeftNav_Notifications"
    TagPrefix="uc1" %>
<%@ Register TagPrefix="EBid" TagName="GlobalLinksNav" Src="~/web/usercontrol/GlobalLinksNav.ascx" %>
<%@ Register TagPrefix="EBid" TagName="Footer" Src="~/web/usercontrol/Vendor/Footer.ascx"  %>
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
            <table border="0" cellpadding="0" cellspacing="0" id="page">
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
                                    <EBid:TopDate runat="server" ID="TopDate" />
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
                                        <EBid:Vendor_LeftNav_Auctions runat="server" ID="Vendor_LeftNav_Auctions" />
                                    </div>
                                </td>
                                <td id="content">
                                    <h1>
                                        <br />
                                        Confirmed Auction Invitations</h1>
                                    <br />
                                    <asp:GridView runat="server" ID="gvConfirmedInvitations" AllowPaging="True"
                                        AllowSorting="True" AutoGenerateColumns="False" SkinID="BidEvents"
                                        DataKeyNames="AuctionRefNo" DataSourceID="dsConfirmedInvitations" 
                                        OnRowCommand="lnkAuctionEvent_Command" EmptyDataText="You have not confirmed any auction events yet.">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Reference No." SortExpression="AuctionRefNo">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle Width="90px" />
                                                <ItemTemplate>
                                                    &nbsp;<asp:LinkButton ID="lnkRefNo" runat="server" CommandArgument='<%# Eval("AuctionRefNo") %>' CommandName="ViewDetails"
                                                        Text='<%# Eval("AuctionRefNo") %>' Width="95%"></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Auction Event" SortExpression="ItemDesc">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    &nbsp;<asp:LinkButton ID="lnkItemDesc" runat="server" CommandArgument='<%# Eval("AuctionRefNo") %>' CommandName="ViewDetails"
                                                        Text='<%# Eval("ItemDesc") %>'></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Start Date" SortExpression="AuctionStartDateTime">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Center" Width="180px" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblStartDate" runat="server" Text='<%# Bind("AuctionStartDateTime", "{0:D}") %>'></asp:Label>
                                                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("AuctionStartDateTime", "<br />{0:T}") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="End Date" SortExpression="AuctionEndDateTime">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Center" Width="180px" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblEndDate" runat="server" Text='<%# Bind("AuctionEndDateTime", "{0:D}") %>'></asp:Label>
                                                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("AuctionEndDateTime", "<br />{0:T}") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                    <asp:SqlDataSource ID="dsConfirmedInvitations" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>'
                                        SelectCommand="s3p_EBid_QueryConfirmedAuctionByVendorID" SelectCommandType="StoredProcedure">
                                        <SelectParameters>
                                            <asp:SessionParameter Name="VendorID" SessionField="userid" Type="Int32" />
                                        </SelectParameters>
                                    </asp:SqlDataSource>
                                    <p>&nbsp;
                                        </p>
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
