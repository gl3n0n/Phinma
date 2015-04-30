<%@ Page Language="C#" AutoEventWireup="true" CodeFile="approvedbiddetails.aspx.cs"
    Inherits="web_purchasing_screens_approvedbiddetails" %>

<%@ Register Src="../usercontrol/CommentBox.ascx" TagName="CommentBox" TagPrefix="uc4" %>
<%@ Register Src="../usercontrol/bids/biddetails_suppliers.ascx" TagName="biddetails_suppliers"
    TagPrefix="uc3" %>
<%@ Register Src="../usercontrol/bids/biddetails_details.ascx" TagName="biddetails_details"
    TagPrefix="uc1" %>
<%@ Register Assembly="Calendar" Namespace="CalendarControl" TagPrefix="cc1" %>
<%@ Register Src="../usercontrol/commentlist.ascx" TagName="commentlist" TagPrefix="uc2" %>
<%@ Register TagPrefix="EBid" TagName="TopDate" Src="~/WEB/usercontrol/TopDate.ascx" %>
<%@ Register TagPrefix="EBid" TagName="GlobalLinksNav" Src="~/web/usercontrol/GlobalLinksNav.ascx" %>
<%@ Register TagPrefix="EBid" TagName="Purchasing_TopNav_Bids" Src="~/web/usercontrol/Purchasing/Purchasing_TopNav_Bids.ascx" %>
<%@ Register TagPrefix="EBid" TagName="Purchasing_LeftNav" Src="~/web/usercontrol/Purchasing/Purchasing_LeftNav_Bids.ascx" %>
<%@ Register TagPrefix="EBid" TagName="Footer" Src="~/web/usercontrol/Purchasing/Footer.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
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

    <script language="javascript" type="text/javascript" src="../include/util.js"></script>

</head>
<body onload="SetStatus()">
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
                                            <td>
                                                <EBid:Purchasing_LeftNav runat="server" ID="LeftNav" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>&nbsp;
                                                
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <h2>
                                                    Comments</h2>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <uc2:commentlist ID="Commentlist1" runat="server" />
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
                                                                Bid Event Details</h1>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:LinkButton ID="lnkShowHideChangeDeadline" runat="server" Text="<br />Change Submission Deadline"
                                                                Font-Bold="true" CausesValidation="False" OnClick="lnkShowHideChangeDeadline_Click"></asp:LinkButton>
                                                            <uc1:biddetails_details ID="Biddetails_details1" runat="server" />
                                                            <asp:GridView AutoGenerateColumns="False" runat="server" ID="gvBidItemDetails" SkinID="AuctionedItems"
                                                                DataSourceID="dsItemDetails" AllowSorting='true' OnRowCommand="gvBidItemDetails_RowCommand">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="Item" SortExpression="Item">
                                                                        <ItemStyle Width="65px" />
                                                                        <ItemTemplate>
                                                                            &nbsp;<asp:Label runat="server" ID="lblItem" Text='<%#Bind("Item") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <ItemStyle Width="260px" />
                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Description" SortExpression="DetailDesc">
                                                                        <ItemTemplate>
                                                                            <asp:Label runat="server" ID="lblDescription" Text='<%#Bind("DetailDesc") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <ItemStyle HorizontalAlign="Center" />
                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Quantity Measure" SortExpression="Qty">
                                                                        <ItemTemplate>
                                                                            <asp:Label runat="server" ID="lblQuantity" Text='<%#Bind("Qty", "{0:#,##0}") %>'></asp:Label>
                                                                            <asp:Label runat="server" ID="lblUnitOfMeasure" Text='<%#Bind("UnitOfMeasure") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <ItemStyle HorizontalAlign="Center" Width="120px" />
                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Unit Price" SortExpression="UnitPrice">
                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                        <ItemStyle HorizontalAlign="Center" Width="100px" />
                                                                        <ItemTemplate>
                                                                            <asp:Label runat="server" ID="lblPrice" Text='<%#Bind("UnitPrice", "{0:#,##0}") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Total Cost" SortExpression="TotalUnitCost">
                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                        <ItemStyle HorizontalAlign="Center" Width="100px" />
                                                                        <ItemTemplate>
                                                                            <asp:Label runat="server" ID="lblTotal" Text='<%#Bind("TotalUnitCost", "{0:#,##0}") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Delivery Date" SortExpression="DeliveryDate1">
                                                                        <ItemTemplate>
                                                                            <asp:Label runat="server" ID="lblDeliveryDate" Text='<%#Bind("DeliveryDate1") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <ItemStyle HorizontalAlign="Center" Width="100px" />
                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField>
                                                                        <ItemTemplate>
                                                                            <asp:Panel runat="server" ID="pnlLinks">
                                                                                <asp:LinkButton ID="lnkWithdraw" runat="server" Font-Bold="true" ToolTip="Click to withdraw this bid item."
                                                                                    CommandName="withdraw" Visible='<%# IsVisible(Eval("ItemStatus").ToString()) %>'>Withdraw</asp:LinkButton>
                                                                                <asp:Label ID="lblStatus" runat="server" Text='<%#Bind("ItemStatus") %>' Visible='<%# !IsVisible(Eval("ItemStatus").ToString()) %>'></asp:Label>
                                                                            </asp:Panel>
                                                                            <asp:Panel runat="server" ID="pnlComments" Visible="false">
                                                                                Comments
                                                                                <asp:TextBox ID="txtComment" runat="server" TextMode="MultiLine" Font-Names="Arial"
                                                                                    Width="100%" Height="45px"></asp:TextBox>
                                                                                <asp:RequiredFieldValidator ID="rfvComments" runat="server" Display="Dynamic" SetFocusOnError="true"
                                                                                    ErrorMessage="Comments Required." ControlToValidate="txtComment"></asp:RequiredFieldValidator>
                                                                                <asp:CheckBox ID="chkAlowVendorView" runat="server" Checked="false" Text="Show To Vendors" />
                                                                                <br />
                                                                                <asp:LinkButton ID="lnkOK" runat="server" CommandName="continuewithdraw" CommandArgument='<%#Bind("BidDetailNo") %>'
                                                                                    OnClientClick="return confirm('Are you sure you want to withdraw this bid item?');">OK</asp:LinkButton>&nbsp;&nbsp;
                                                                                <asp:LinkButton ID="lnkCancel" runat="server" CommandName="cancelwithdraw" CausesValidation="false">Cancel</asp:LinkButton>
                                                                                <br />
                                                                                <br />
                                                                            </asp:Panel>
                                                                        </ItemTemplate>
                                                                        <ItemStyle HorizontalAlign="Center" Width="100px" />
                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                            </asp:GridView>
                                                            <asp:SqlDataSource ID="dsItemDetails" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>'
                                                                SelectCommand="s3p_EBid_GetBidItemDetails" SelectCommandType="StoredProcedure">
                                                                <SelectParameters>
                                                                    <asp:SessionParameter Name="BidRefNo" SessionField="BidRefNo" Type="Int32" />
                                                                </SelectParameters>
                                                            </asp:SqlDataSource>
                                                            <br />
                                                            <uc3:biddetails_suppliers ID="Biddetails_suppliers1" runat="server" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <br />
                                                            <asp:Table ID="tblDeadline" runat="server" Width="100%" Font-Size="11px" Style="padding-left: 5px;"
                                                                Visible="false">
                                                                <asp:TableRow>
                                                                    <asp:TableCell Width="130px" VerticalAlign="top" Style="padding-top: 8px;">
                                                                        Bid Submission Deadline
                                                                    </asp:TableCell>
                                                                    <asp:TableCell>
                                                                        <cc1:JSCalendar ID="clndrDeadline" runat="server" ImageURL="../../calendar/img.gif"
                                                                            EnableViewState="true" ScriptsBasePath="../../calendar/" DateFormat="MM/dd/yyyy"
                                                                            Width="178px" ReadOnly="false" MaxLength="10" TabIndex="-1"></cc1:JSCalendar>
                                                                        &nbsp;<asp:Label ID="lblExDate5" runat="server" Text="eg. 01/31/2007" ForeColor="red" /><br />
                                                                        <asp:TextBox ID="txtDeadlineHH" runat="server" MaxLength="2" Width="40px">00</asp:TextBox>
                                                                        :
                                                                        <asp:TextBox ID="txtDeadlineMM" runat="server" MaxLength="2" Width="40px">00</asp:TextBox>
                                                                        :
                                                                        <asp:TextBox ID="txtDeadlineSS" runat="server" MaxLength="2" Width="40px">00</asp:TextBox>
                                                                        &nbsp;<asp:DropDownList ID="ddlDeadline" runat="server">
                                                                            <asp:ListItem Value="AM">AM</asp:ListItem>
                                                                            <asp:ListItem Value="PM">PM</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                        <asp:CustomValidator runat="server" ID="cfvDeadline" Display="Dynamic" ErrorMessage="<br />&nbsp;&nbsp;Invalid date or time. Please enter a valid date and time."
                                                                            OnServerValidate="cfvDeadline_ServerValidate"></asp:CustomValidator>
                                                                    </asp:TableCell>
                                                                </asp:TableRow>
                                                                <asp:TableRow>
                                                                    <asp:TableCell VerticalAlign="top" Style="padding-top: 8px;">
                                                                        Comment
                                                                    </asp:TableCell>
                                                                    <asp:TableCell>
                                                                        <asp:TextBox ID="txtComment" runat="server" TextMode="MultiLine" Width="360px" Height="90px"
                                                                            Font-Names="Arial" MaxLength="999"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="rfvComment" runat="server" ErrorMessage="<br />&nbsp;&nbsp;Include a comment please."
                                                                            ControlToValidate="txtComment" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                                    </asp:TableCell>
                                                                </asp:TableRow>
                                                            </asp:Table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center">
                                                            <asp:Label ID="lblMessage" runat="server" Text="" ForeColor="red" Font-Size="11px" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                    <br />
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%" id="actions">
                                        <tr>
                                            <td>
                                                <asp:LinkButton ID="lnkChangeDeadline" runat="server" OnClick="lnkChangeDeadline_Click"
                                                    OnClientClick="return confirm('Are you sure you want to change the bid submission deadline of this event?');"
                                                    Visible="False">Change</asp:LinkButton>
                                                <asp:LinkButton runat="server" ID="lnkCancel" Text="Back" OnClick="lnkCancel_Click"
                                                    CausesValidation="false"></asp:LinkButton>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                        <p>&nbsp;
                            </p>
                    </td>
                </tr>
                <tr>
                    <td id="footer" style="height: 45px">
                        <EBid:Footer runat="server" ID="Footer1" />
                    </td>
                </tr>
            </table>
        </form>
    </div>
</body>
</html>
