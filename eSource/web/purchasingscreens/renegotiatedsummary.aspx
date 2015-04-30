<%@ Page Language="C#" AutoEventWireup="true" CodeFile="renegotiatedsummary.aspx.cs" Inherits="web_purchasing_screens_renegotiatedsummary" Theme="default" %>

<%@ Reference Control="~/web/usercontrol/commentlist_tender.ascx" %>
<%@ Register Src="../usercontrol/bids/bidtender_attachments.ascx" TagName="bidtender_attachments" TagPrefix="uc1" %>
<%@ Register TagPrefix="EBid" TagName="TopDate" Src="~/web/usercontrol/TopDate.ascx" %>
<%@ Register TagPrefix="EBid" TagName="GlobalLinksNav" Src="~/web/usercontrol/GlobalLinksNav.ascx" %>
<%@ Register TagPrefix="EBid" TagName="Purchasing_TopNav_Bids" Src="~/web/usercontrol/Purchasing/Purchasing_TopNav_Bids.ascx" %>
<%@ Register TagPrefix="EBid" TagName="Purchasing_LeftNav" Src="~/web/usercontrol/Purchasing/Purchasing_LeftNav_Bids.ascx" %>
<%@ Register TagPrefix="EBid" TagName="Footer" Src="~/web/usercontrol/Purchasing/Footer.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml" style="height: 100%;">
<head>
<%@ Register TagPrefix="EBid" TagName="jqueryLoader" Src="../usercontrol/jQuery_loader.ascx" %>
    <EBid:jqueryLoader runat="server" ID="jqueryLoader1" />
    <title id="PageTitle" runat="server"></title>
    <meta http-equiv="Content-Language" content="en-us" />
    <meta http-equiv="Content-Type" content="text/html; charset=windows-1252" />
    <link rel="stylesheet" type="text/css" href='<%= "../themes/"+Session["configTheme"]+"/css/style.css" %>' />
    <link rel="stylesheet" type="text/css" href='<%="../themes/"+Session["configTheme"]+"/css/style_ph.css" %>' />
    <link href='<%= "../themes/"+Session["configTheme"]+"/css/style.css" %>' rel="stylesheet" type="text/css" />
</head>
<body style="height: 100%;">
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
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                        <tr>
                                            <td id="Td1">
                                                <div align="left">
                                                    <EBid:Purchasing_LeftNav runat="server" ID="Purchasing_LeftNav" />
                                                </div>
                                                <h2>
                                                    Comments</h2>
                                                <div align="center">
                                                    <div align="left" style="padding-left: 3px; text-align: justify;">
                                                        <asp:DataList ID="dlComments" runat="server" Width="100%">
                                                            <ItemStyle Font-Names="Arial" Font-Size="11px" />
                                                            <SeparatorStyle HorizontalAlign="Center" />
                                                            <FooterStyle HorizontalAlign="Center" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label3" runat="server" Text="Date Posted : " ForeColor="DimGray"></asp:Label>
                                                                <asp:Label ID="DatePostedLabel" runat="server" Text='<%# Eval("DatePosted") %>'></asp:Label><br />
                                                                <asp:Label ID="Label2" runat="server" Text="Author : " ForeColor="DimGray"></asp:Label>
                                                                <asp:Label ID="AuthorLabel" runat="server" Text='<%# Eval("FullName") %>'></asp:Label><br />
                                                                <asp:Label ID="Label1" runat="server" Text="Comment : " ForeColor="DimGray"></asp:Label><br />
                                                                &nbsp;&nbsp;<asp:Label ID="CommentLabel" runat="server" Text='<%# Eval("Comment") %>'></asp:Label>
                                                                <br />
                                                                <br />
                                                            </ItemTemplate>
                                                            <SeparatorTemplate>
                                                                - - -</SeparatorTemplate>
                                                            <FooterTemplate>
                                                                - - - - -</FooterTemplate>
                                                        </asp:DataList>
                                                    </div>
                                                    <div align="center" style="width: 100%; font-family: arial; font-size: 11px;">
                                                        <table width="100%">
                                                            <tr>
                                                                <td colspan="3" align="center">
                                                                    <asp:Label ID="lblCounts" Font-Names="Arial" Font-Size="11px" runat="server" />
                                                                </td>
                                                            </tr>
                                                            <tr id="trPagers" runat="server">
                                                                <td>
                                                                    &nbsp;</td>
                                                                <td valign="bottom" align="center">
                                                                    <asp:ImageButton ID="btnFirstPage" ImageUrl="~/web/themes/images/pager/firstpage.gif" BorderWidth="0" runat="server" OnClick="btnFirstPage_Click" CausesValidation="false" ToolTip="First Page" />
                                                                    <asp:ImageButton ID="btnPreviousPage" ImageUrl="~/web/themes/images/pager/prevpage.gif" BorderWidth="0" runat="server" OnClick="btnPreviousPage_Click" CausesValidation="false" ToolTip="Previous Page" />
                                                                    <asp:ImageButton ID="btnNextPage" ImageUrl="~/web/themes/images/pager/nextpage.gif" BorderWidth="0" runat="server" OnClick="btnNextPage_Click" CausesValidation="false" ToolTip="Next Page" />
                                                                    <asp:ImageButton ID="btnLastPage" ImageUrl="~/web/themes/images/pager/lastpage.gif" BorderWidth="0" runat="server" OnClick="btnLastPage_Click" CausesValidation="false" ToolTip="Last Page" />
                                                                </td>
                                                                <td>
                                                                    &nbsp;</td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                    <div>
                                                        <asp:Label ID="lblRecordCount" Visible="False" runat="server" />
                                                        <asp:Label ID="lblCurrentIndex" Visible="False" Text="0" runat="server" />
                                                        <asp:Label ID="lblPageSize" Visible="False" Text="5" runat="server" />
                                                    </div>
                                                    <br />
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td id="content">
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%" id="page0">
                                        <tr>
                                            <td valign="top">
                                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                    <tr>
                                                        <td id="content0">
                                                            <h1>
                                                                <br />
                                                                Endorsement Summary</h1>
                                                            <br />
                                                            <div align="left">
                                                                <asp:DetailsView ID="dvEventDetails" runat="server" AutoGenerateRows="False" DataSourceID="dsEventDetails" Width="100%" CssClass="itemDetails" HeaderText="Details" HeaderStyle-BackColor="#10659E" HeaderStyle-ForeColor="white"
                                                                    HeaderStyle-Font-Bold="true">
                                                                    <Fields>
                                                                        <asp:BoundField DataField="BidDetailNo" HeaderText="Bid Detail No." InsertVisible="False" ReadOnly="True" >
                                                                            <ItemStyle BackColor="White" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="SKU" HeaderText="SKU" ReadOnly="True">
                                                                            <ItemStyle BackColor="White" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="DetailDesc" HeaderText="Description">
                                                                            <ItemStyle BackColor="White" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="Qty" HeaderText="Quantity">
                                                                            <ItemStyle BackColor="White" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="UnitOfMeasure" HeaderText="Unit of measure">
                                                                            <ItemStyle BackColor="White" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="Currency" HeaderText="Currency" SortExpression="Currency">
                                                                            <ItemStyle BackColor="White" />
                                                                        </asp:BoundField>
                                                                        <asp:TemplateField HeaderText="Delivery Date" SortExpression="DeliveryDate">
                                                                            <ItemStyle BackColor="White" />
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lbldeliverydate" runat="server" Text='<%# Bind("DeliveryDate", "{0:D}") %>'></asp:Label>
                                                                                <asp:HiddenField ID="hdn_bidrefno" runat="server" Value='<%# Bind("BidRefNo") %>' />
                                                                                <asp:HiddenField ID="hdn_bidevent" runat="server" Value='<%# Bind("ItemDesc") %>' />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    </Fields>
                                                                    <HeaderStyle BackColor="#10659E" Font-Bold="True" ForeColor="White" />
                                                                    <FieldHeaderStyle Width="120px" />
                                                                </asp:DetailsView>
                                                                <asp:SqlDataSource ID="dsEventDetails" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>' SelectCommand="sp_GetBidItemDetails" SelectCommandType="StoredProcedure">
                                                                    <SelectParameters>
                                                                        <asp:SessionParameter Name="BidDetailNo" SessionField="BidDetailNo" Type="Int32" />
                                                                    </SelectParameters>
                                                                </asp:SqlDataSource>
                                                            </div>
                                                            <br />
                                                            <p>
                                                                <asp:GridView Width="100%" runat="server" ID="gvTenders" SkinID="AuctionedItems" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" EmptyDataText="No Bid Tenders." DataSourceID="dsTenders"
                                                                    OnRowCommand="gvTenders_RowCommand" DataKeyNames="BidTenderNo,Status" OnSelectedIndexChanged="gvTenders_SelectedIndexChanged" OnDataBound="gvTenders_DataBound">
                                                                    <SelectedRowStyle BackColor="#50A4D1" />
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="Tender No.">
                                                                            <HeaderStyle HorizontalAlign="Center" Width="85px" />
                                                                            <ItemTemplate>
                                                                                &nbsp;<asp:Label ID="lblTenderNo" Text='<%# Bind("BidTenderNo") %>' runat="server"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Item Bid Price">
                                                                            <HeaderStyle HorizontalAlign="Center" />
                                                                            <ItemStyle HorizontalAlign="right" />
                                                                            <ItemTemplate>
                                                                                &nbsp;<asp:Label ID="lblAmount" Text='<%# Bind("Amount", "{0:#,##0.00}") %>' runat="server"></asp:Label>&nbsp;
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Discount">
                                                                            <HeaderStyle HorizontalAlign="Center" />
                                                                            <ItemStyle HorizontalAlign="right" />
                                                                            <ItemTemplate>
                                                                                &nbsp;<asp:Label ID="lblDiscount" Text='<%# Bind("Discount", "{0:#,##0.00}") %>' runat="server"></asp:Label>&nbsp;
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Delivery Cost">
                                                                            <HeaderStyle HorizontalAlign="Center" />
                                                                            <ItemStyle HorizontalAlign="right" />
                                                                            <ItemTemplate>
                                                                                &nbsp;<asp:Label ID="lblDeliveryCost" Text='<%# Bind("DeliveryCost", "{0:#,##0.00}") %>' runat="server"></asp:Label>&nbsp;
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Total Bid Price">
                                                                            <HeaderStyle HorizontalAlign="Center" />
                                                                            <ItemStyle HorizontalAlign="right" />
                                                                            <ItemTemplate>
                                                                                &nbsp;<asp:Label ID="lblPrice" Text='<%# Bind("BidPrice", "{0:#,##0.00}") %>' runat="server"></asp:Label>&nbsp;
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Bidder">
                                                                            <HeaderStyle HorizontalAlign="Center" />
                                                                            <ItemStyle HorizontalAlign="Center" />
                                                                            <ItemTemplate>
                                                                                &nbsp;<asp:Label ID="lblVendorName" Text='<%# Bind("VendorName") %>' runat="server"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField>
                                                                            <HeaderStyle HorizontalAlign="Center" />
                                                                            <ItemStyle HorizontalAlign="Center" Width="160px" />
                                                                            <ItemTemplate>
                                                                                <asp:Panel ID="pnlCommands" runat="server" Visible='<%# IsPanelVisible(Eval("RenegotiationStatus", "{0}")) %>' HorizontalAlign="Center">
                                                                                    <asp:LinkButton ID="LinkButton1" runat="server" CommandName="Select" CommandArgument='<%# Bind("BidTenRef") %>' Text='Details' />&nbsp;|
                                                                                    <asp:LinkButton ID="lnkAward" runat="server" Text='Award' CommandName="SetComments" CommandArgument='<%# Bind("BidTenderNo", "Award|{0}") %>' ToolTip="Click to award this tender.">
                                                                                    </asp:LinkButton>&nbsp;|
                                                                                    <asp:LinkButton ID="lnkRenegotiate" runat="server" Text='clarify' CommandName="SetComments" CommandArgument='<%# Bind("BidTenderNo", "Renegotiate|{0}") %>' ToolTip="Click to send this tender to buyer for clarification.">
                                                                                    </asp:LinkButton>                                                                                    
                                                                                </asp:Panel>
                                                                                <asp:Panel ID="pnlComments" runat="server" Visible='false' HorizontalAlign="Center">
                                                                                    <asp:Label ID="lblComment" runat="server" Text="Comment:"></asp:Label><br />
                                                                                    <asp:TextBox ID="txtComment" runat="server" TextMode="MultiLine" Rows="4" Font-Names="Arial" Font-Size="11px" Width="100%"></asp:TextBox><br />
                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Comment is required<br>" ControlToValidate="txtComment" Display="Dynamic"></asp:RequiredFieldValidator>
                                                                                    <asp:LinkButton ID="btnOkAward" runat="server" Text="OK" CommandName="AwardItem" CommandArgument='<%# Eval("BidTenderNo", "{0}") %>' OnClientClick="return confirm('Award this item?')" Visible="false" />
                                                                                    <asp:LinkButton ID="btnOkRenegotiate" runat="server" Text="OK" CommandName="RenegotiateItem" CommandArgument='<%# Eval("BidTenderNo", "{0}") %>' OnClientClick="return confirm('Clarify this item?')"
                                                                                        Visible="false" />&nbsp;
                                                                                    <asp:LinkButton ID="btnCancelComment" Text="Cancel" runat="server" CommandName="CancelComments" CausesValidation="false" />
                                                                                    <br /><br />
                                                                                </asp:Panel>
                                                                                <asp:Panel ID="Panel1" runat="server" Visible='<%# !IsPanelVisible(Eval("RenegotiationStatus", "{0}")) %>' HorizontalAlign="Center">
                                                                                    <asp:LinkButton ID="lblDetails" runat="server" CommandName="Select" CommandArgument='<%# Bind("BidTenRef") %>' Text='Details' />&nbsp;|
                                                                                    <asp:Label ID="lblrenegotiatestatus" runat="server" Text='<%# GetRenegotiateDesc(Eval("RenegotiationStatus", "{0}")) %>' />
                                                                                </asp:Panel>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                </asp:GridView>
                                                                <asp:SqlDataSource ID="dsTenders" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>' SelectCommand="sp_GetItemTenderRenegotiated" SelectCommandType="StoredProcedure">
                                                                    <SelectParameters>
                                                                        <asp:SessionParameter Name="BidDetailNo" SessionField="BidDetailNo" Type="Int32" />
                                                                    </SelectParameters>
                                                                </asp:SqlDataSource>
                                                            </p>
                                                            <asp:Panel ID="pnlDetails" runat="server" Visible='false' HorizontalAlign="Center">
                                                            <p>
                                                                <asp:DetailsView ID="dvTenderDetails" runat="server" Width="100%" AutoGenerateRows="False" DataKeyNames="BidTenderNo" DataSourceID="dsTenders" SkinID="BidDetails">
                                                                    <HeaderTemplate>
                                                                        &nbsp;Bid Tender Details
                                                                    </HeaderTemplate>
                                                                    <Fields>
                                                                        <asp:TemplateField HeaderText="Tender No." InsertVisible="False">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("BidTenderNo") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Bidder">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="Label3" runat="server" Text='<%# Bind("VendorName") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Amount">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="Label5" runat="server" Text='<%# Bind("Amount", "{0:#,##0.00}") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Discount">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="Label6" runat="server" Text='<%# Bind("Discount", "{0:#,##0.00}") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Delivery Cost">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="Label7" runat="server" Text='<%# Bind("DeliveryCost", "{0:#,##0.00}") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Bid Price">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="Label2" runat="server" Font-Bold="true" Text='<%# Bind("BidPrice", "{0:#,##0.00}") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Remarks">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="Label8" runat="server" Text='<%# Eval("Remarks").ToString().Trim().Length > 0 ? Eval("Remarks").ToString() : "No Remarks" %>' ForeColor='<%# Eval("Remarks").ToString().Trim().Length > 0 ? System.Drawing.Color.Black : System.Drawing.Color.Gray %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Date Submitted">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="Label4" runat="server" Text='<%# Bind("DateSubmitted", "{0:D}") %>'></asp:Label>,
                                                                                <asp:Label ID="Label9" runat="server" Text='<%# Bind("DateSubmitted", "{0:T}") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    </Fields>
                                                                </asp:DetailsView>
                                                            </p>
                                                            <p>
                                                                <uc1:bidtender_attachments ID="Bidtender_attachments1" runat="server" />
                                                            </p>
                                                            </asp:Panel>
                                                            <br />
                                                            <table border="0" cellpadding="0" cellspacing="0" width="100%" id="actions">
                                                                <tr>
                                                                    <td>
                                                                        <asp:LinkButton ID="lnkCancel" runat="server" OnClick="lnkCancel_Click" Text="Back"></asp:LinkButton>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            <p>
                                                                &nbsp;</p>
                                                        </td>
                                                    </tr>
                                                </table>
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
                        <EBid:Footer runat="server" ID="Footer1" />
                    </td>
                </tr>
            </table>
        </form>
    </div>
</body>
</html>
