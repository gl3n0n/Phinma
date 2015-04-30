<%@ Page Language="c#" Inherits="EBid.web.vendor_screens.TenderDrafts" CodeFile="tenderdrafts.aspx.cs" MaintainScrollPositionOnPostback="true" %>

<%@ Register TagPrefix="EBid" TagName="GlobalLinksNav" Src="~/web/usercontrol/GlobalLinksNav.ascx" %>
<%@ Register TagPrefix="EBid" TagName="Footer" Src="~/web/usercontrol/Vendor/Footer.ascx"  %>
<%@ Register TagPrefix="EBid" TagName="TopDate" Src="~/web/usercontrol/TopDate.ascx" %>
<%@ Register TagPrefix="EBid" TagName="Vendor_TopNav_Bids" Src="~/web/usercontrol/Vendor/Vendor_TopNav_Bids.ascx" %>
<%@ Register TagPrefix="EBid" TagName="Vendor_LeftNav_Bids" Src="~/web/usercontrol/Vendor/Vendor_LeftNav_Bids.ascx" %>
<%@ Register TagPrefix="EBid" TagName="Vendor_LeftNav_Comments" Src="~/web/usercontrol/Vendor/Vendor_LeftNav_Comments.ascx" %>
<html xmlns="http://www.w3.org/1999/xhtml" style="height: 100%;">
<head><%@ Register TagPrefix="EBid" TagName="jqueryLoader" Src="../usercontrol/jQuery_loader.ascx" %>
    <EBid:jqueryLoader runat="server" ID="jqueryLoader1" />
    <title id="PageTitle" runat="server"></title>
    <meta http-equiv="Content-Language" content="en-us" />
    <meta http-equiv="Content-Type" content="text/html; charset=windows-1252" />
    <link rel="stylesheet" type="text/css" href='<%= "../themes/"+Session["configTheme"]+"/css/style.css" %>' />
    <link rel="stylesheet" type="text/css" href='<%="../themes/"+Session["configTheme"]+"/css/style_v.css" %>' />
    <script type="text/javascript" src="../include/util.js"></script>
</head>
<body onload="SetStatus();">
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
                                        <EBid:Vendor_LeftNav_Bids runat="server" ID="Vendor_LeftNav_Bids" />
                                    </div>
                                    <div align="left">
                                        <table border="0" cellpadding="0" cellspacing="0" width="100%" id="table3">
                                            <tr>
                                                <td>
                                                    &nbsp;</td>
                                            </tr>
                                        </table>
                                    </div>
                                </td>
                                <td id="content">
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                        <tr>
                                            <td valign="top">
                                                <h1>
                                                    <br />
                                                    Bid Tender Drafts</h1>
                                                <br />
                                                <asp:GridView ID="gvTenderDrafts" runat="server" SkinID="BidEvents"
                                                    AutoGenerateColumns="False" AllowPaging="True" AllowSorting="True" 
                                                    OnRowCommand="gvTenderDrafts_RowCommand" DataKeyNames="BidRefNo,AsClarified" DataSourceID="dsGetBidTenderDraft">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Tender No." SortExpression="BidRefNo">
                                                            <HeaderStyle HorizontalAlign="Center" Width="90px" />
                                                            <ItemTemplate>
                                                                &nbsp;
                                                                <asp:LinkButton ID="lnkTenderNo" runat="server" Text='<%# Bind("BidTenderNo") %>' CommandName="EditTender"
                                                                    CommandArgument='<%# Bind("BidTenderNoRenegotiationStat", "{0}") %>' Width="90%" ToolTip="Click to view/edit bid tender for this bid item."></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Bid Item" SortExpression="DetailDesc">
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <ItemStyle HorizontalAlign="Left" />
                                                            <ItemTemplate>                                                                
                                                                &nbsp;<asp:LinkButton ID="lnkDetailDesc" runat="server" Text='<%# Bind("DetailDesc") %>' CommandName="EditTender"
                                                                    CommandArgument='<%# Bind("BidTenderNoRenegotiationStat", "{0}") %>' ToolTip="Click to view/edit bid tender for this bid item."></asp:LinkButton>
                                                                <asp:Label ID="lblBidPrice" runat="server" Text='<%# Bind("BidPrice" , " @ <b>{0:#,000.00}</b>") %>'></asp:Label>
                                                                <asp:Label ID="lblCurrency" runat="server" Text='<%# Bind("Currency", " {0}") %>'></asp:Label>                                                                
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Bid Event" SortExpression="BidEvent">
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <ItemStyle HorizontalAlign="Left" />
                                                            <ItemTemplate>                                                                
                                                                &nbsp;<asp:LinkButton ID="lnkItemDesc" runat="server" Text='<%# Bind("BidEvent") %>' CommandName="EditBidEventTenders"
                                                                    CommandArgument='<%# Bind("RefNoTenderNo", "{0}") %>' ToolTip="Click to view/edit bid tenders for this event."></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Date Created" SortExpression="DateCreated">
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <ItemStyle HorizontalAlign="Center" Width="180px" />
                                                            <ItemTemplate>
                                                                &nbsp;<asp:Label ID="lblDateSavedAsDraft" runat="server" Text='<%# Bind("DateCreated", "{0:D}<br />{0:T}") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="For Clarification" SortExpression="ForRenegotiation">
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <ItemStyle HorizontalAlign="Center" Width="100px" />
                                                            <ItemTemplate>
                                                                &nbsp;<asp:Label ID="lblForRenegotiation" runat="server" Text='<%# Bind("ForRenegotiation") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                                <asp:SqlDataSource ID="dsGetBidTenderDraft" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>'
                                                    SelectCommand="sp_GetVendorBidTenderDrafts" SelectCommandType="StoredProcedure">
                                                    <SelectParameters>
                                                        <asp:SessionParameter Name="VendorId" SessionField="UserId" Type="Int32" />                                                        
                                                    </SelectParameters>
                                                </asp:SqlDataSource>
                                                <br />
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
