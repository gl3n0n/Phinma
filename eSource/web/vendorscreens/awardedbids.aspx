<%@ Page Language="c#" Inherits="EBid.web.vendor_screens.AwardedBids" CodeFile="awardedbids.aspx.cs" %>

<%@ Register TagPrefix="EBid" TagName="GlobalLinksNav" Src="~/web/usercontrol/GlobalLinksNav.ascx" %>
<%@ Register TagPrefix="EBid" TagName="Footer" Src="~/web/usercontrol/Vendor/Footer.ascx"  %>
<%@ Register TagPrefix="EBid" TagName="TopDate" Src="~/web/usercontrol/TopDate.ascx" %>
<%@ Register TagPrefix="EBid" TagName="Vendor_TopNav_Bids" Src="~/web/usercontrol/Vendor/Vendor_TopNav_Bids.ascx" %>
<%@ Register TagPrefix="EBid" TagName="Vendor_LeftNav_Bids" Src="~/web/usercontrol/Vendor/Vendor_LeftNav_Bids.ascx" %>
<html xmlns="http://www.w3.org/1999/xhtml" style="height: 100%;">
<head>
<%@ Register TagPrefix="EBid" TagName="jqueryLoader" Src="../usercontrol/jQuery_loader.ascx" %>
    <EBid:jqueryLoader runat="server" ID="jqueryLoader1" />
    <title id="PageTitle" runat="server"></title>
    <meta http-equiv="Content-Language" content="en-us" />
    <meta http-equiv="Content-Type" content="text/html; charset=windows-1252" />
    <link href='<%= "../themes/"+Session["configTheme"]+"/css/style.css" %>' rel="stylesheet" type="text/css" />
    <link href='../themes/<%= Session["configTheme"] %>/css/style_v.css' rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../include/util.js"></script>
</head>
<body onLoad="SetStatus();">
    <div align="left">
        <form id="Form1" runat="server">
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
                                        <EBid:Vendor_LeftNav_Bids runat="server" ID="LeftNav" />
                                    </div>
                                   
                                </td>
                                <td id="content">
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                        <tr>
                                            <td valign="top">
                                                <h1>
                                                    <br />
                                                    Awarded Bid Items</h1>
                                                <br />
                                               													<asp:GridView ID="gvBids" runat="server" SkinID="BidEvents" 
													    AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" 
													    DataSourceID="dsAwardedItems" OnRowCommand="gvBids_RowCommand" DataKeyNames="BidTenderNo,BidDetailNo,BidRefNo">
														<Columns>
                                                            <asp:TemplateField HeaderText="Detail No." InsertVisible="False" SortExpression="BidDetailNo">
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle Width="90px" />
                                                                <ItemTemplate>
                                                                    &nbsp;<asp:LinkButton ID="lnkBidDetailNo" runat="server" Text='<%# Bind("BidDetailNo") %>' Width="95%"
                                                                        CommandName="ViewBidItemDetails" CommandArgument='<%# Bind("BidTenRef") %>'></asp:LinkButton>                                                                    
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Bid Item" SortExpression="DetailDesc">
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemTemplate>
                                                                    &nbsp;<asp:LinkButton ID="lnkBidItem" runat="server" Text='<%# Bind("DetailDesc") %>'
                                                                        CommandName="ViewBidItemDetails" CommandArgument='<%# Bind("BidTenRef") %>'></asp:LinkButton>                                                                     
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Bid Event" SortExpression="ItemDesc">
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="Center" />
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lnkBidEvent" runat="server" Text='<%# Bind("ItemDesc") %>'
                                                                        CommandName="ViewBidEventDetails" CommandArgument='<%# Bind("BidTenRef") %>'></asp:LinkButton>                                                                     
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="PO Number" SortExpression="PONumber">
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="Center" Width="150px" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblPONumber" runat="server" Text='<%# Bind("PONumber") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Date Awarded" SortExpression="DateAwarded">
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="Center" Width="150px" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblDateAwarded" runat="server" Text='<%# Bind("DateAwarded", "{0:D}<br />{0:T}") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
														</Columns>
													</asp:GridView>
                                                    <asp:SqlDataSource ID="dsAwardedItems" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>'
                                                        SelectCommand="sp_GetVendorAwardedItems" SelectCommandType="StoredProcedure">
                                                        <SelectParameters>
                                                            <asp:SessionParameter DefaultValue="0" Name="VendorId" SessionField="UserId" Type="Int32" />
                                                        </SelectParameters>
                                                    </asp:SqlDataSource>
                                                <p>&nbsp;
                                                    </p>
                                            </td>
                                        </tr>
                                    </table>
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
