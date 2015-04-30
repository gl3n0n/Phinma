<%@ Page Language="C#" AutoEventWireup="true" CodeFile="bidsforeval.aspx.cs" Inherits="WEB_buyer_screens_BidsforEval" %>

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
        <form runat="server">
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
                                    <EBid:TopNav2 runat="server" ID="TopNav2" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr valign="top">
                    <td valign="top">
                        <table border="0" cellpadding="0" cellspacing="0" width="100%" height="100%">
                            <tr>
                                <td id="relatedInfo">
                                    <div align="left">
                                        <EBid:LeftNav runat="server" ID="LeftNav" />
                                    </div>
                                    <p>&nbsp;
                                        </p>
                                </td>
                                <td id="content">
                                    <div align="left">
                                        <table border="0" cellpadding="0" cellspacing="0" width="100%" id="page0">
                                            <tr>
                                                <td valign="top">
                                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                        <tr>
                                                            <td id="content0">
                                                                <div align="left">
                                                                    <h1>
                                                                        <br />
                                                                        Received Bid Tenders</h1>
                                                                    <p>
                                                                        These are the tenders that have been submitted by the vendors.<br />&nbsp;
                                                                        <asp:CheckBox ID="chkShowTenderizedBidsOnly" runat="server" AutoPostBack="True"
                                                                            Text="Show only those Bid Items with Bid Tenders." ToolTip="Check to filter to show only those bid items with atleast one bid tender."
                                                                            OnCheckedChanged="chkShowTenderizedBidsOnly_CheckedChanged" />
                                                                        <asp:CheckBox ID="chkPastSubmissionDeadline" runat="server" AutoPostBack="True" 
                                                                            Text="Show only those Bid Items with past submission deadline" ToolTip="Check to filter to show only bid Items with past submission deadline." 
                                                                            OnCheckedChanged="chkPastSubmissionDeadline_CheckedChanged" Visible="false" />
                                                                        <asp:HiddenField ID="HiddenField1" runat="server" />
                                                                    </p>
                                                                    <asp:GridView ID="gvReceivedUnlockedTenders" runat="server" AutoGenerateColumns="False"
                                                                        SkinID="BidEvents" AllowPaging="True" AllowSorting="True" PageSize="20" Width="100%"
                                                                        BorderColor="White" OnRowCommand="gvReceivedUnlockedTenders_RowCommand" DataSourceID="dsReceivedItemTenders"
                                                                        OnRowDataBound="gvReceivedUnlockedTenders_RowDataBound" DataKeyNames="BidRefNo">
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderText="Bid Ref No." InsertVisible="False" SortExpression="BidRefNo">
                                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                                <ItemStyle Width="90px" />
                                                                                <ItemTemplate>
                                                                                    <asp:LinkButton ID="lnkBidDetailNo" runat="server" Text='<%# Bind("BidRefNo") %>'
                                                                                        Visible='<%# valueIsOpen(Eval("Status"), Eval("SubmittedTenderCount")) %>' Width="95%"
                                                                                        CommandName='ViewBidItemDetails' CommandArgument='<%# Bind("BidRefNo") %>'
                                                                                        ToolTip="Click to view bid tenders for this bid item."></asp:LinkButton>
                                                                                    <asp:Label ID="lblBidDetailNo" runat="server" Text='<%# Bind("BidRefNo") %>' Visible='<%# valueIsNotOpen(Eval("Status"), Eval("SubmittedTenderCount")) %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <%--<asp:TemplateField HeaderText="Bid Item" SortExpression="DetailDesc">
                                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                                <ItemTemplate>
                                                                                    <asp:LinkButton ID="lnkBidItem" runat="server" Text='<%# Bind("DetailDesc") %>' Visible='<%# valueIsOpen(Eval("Status"), Eval("SubmittedTenderCount")) %>'
                                                                                        CommandName="ViewBidItemDetails" CommandArgument='<%# Bind("BidDetailNo") %>'
                                                                                        ToolTip="Click to view bid tenders for this bid item."></asp:LinkButton>
                                                                                    <asp:Label ID="lblBidItem" runat="server" Text='<%# Bind("DetailDesc") %>' Visible='<%# valueIsNotOpen(Eval("Status"), Eval("SubmittedTenderCount")) %>'></asp:Label>
                                                                                    <br />
                                                                                    &nbsp;&nbsp;<asp:Label ID="Label1" runat="server" ForeColor="gray" Text='<%# showBidTenderCount(Eval("SubmittedTenderCount")) %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>--%>
                                                                            <asp:TemplateField HeaderText="Bid Event" SortExpression="ItemDesc">
                                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                                <ItemStyle HorizontalAlign="Center" />
                                                                                <ItemTemplate>
                                                                                    &nbsp;<asp:LinkButton ID="lnkBidEvent" runat="server" Text='<%# Bind("ItemDesc") %>'
                                                                                        CommandName="ViewBidEventDetails" CommandArgument='<%# Bind("BidRefNo") %>' ToolTip="Click to view details of this bid event."></asp:LinkButton>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Bid Submission Deadline" SortExpression="Deadline">
                                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                                <ItemStyle HorizontalAlign="Center" Width="180px" />
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblBidSubmissionDeadline" runat="server" Text='<%# Bind("Deadline", "{0:D}<br />{0:T}") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Status" SortExpression="Status">
                                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                                <ItemStyle HorizontalAlign="Center" />
                                                                                <ItemTemplate>
                                                                                    <asp:LinkButton ID="LinkButtonStatus" runat="server" Text='<%# Bind("Status") %>'
                                                                                        Visible='<%# valueIsOpen(Eval("Status"), Eval("SubmittedTenderCount")) %>' CommandName='ViewBidItemDetails'
                                                                                        CommandArgument='<%# Bind("BidRefNo") %>' ToolTip="Click to view bid tenders for this bid item."></asp:LinkButton>
                                                                                    <asp:Label ID="lblStatus" runat="server" Text='Elapsed' Visible='<%# valueIsNotOpen(Eval("Status"), Eval("SubmittedTenderCount")) %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                        </Columns>
                                                                    </asp:GridView>
                                                                    <asp:SqlDataSource ID="dsReceivedItemTenders" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>'
                                                                        SelectCommand="sp_GetBuyerReceivedBidTenders" SelectCommandType="StoredProcedure"
                                                                        FilterExpression="SubmittedTenderCount >= {0}">
                                                                        <SelectParameters>
                                                                            <asp:SessionParameter Name="BuyerId" SessionField="UserId" Type="Int32" />
                                                                        </SelectParameters>
                                                                        <FilterParameters>
                                                                            <asp:ControlParameter ControlID="HiddenField1" Name="ShowItemsWithTenders" PropertyName="Value"
                                                                                DefaultValue="0" Type="Int32" />
                                                                        </FilterParameters>
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
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </table>
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
