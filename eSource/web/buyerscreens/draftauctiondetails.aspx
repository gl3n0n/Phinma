<%@ Page Language="C#" AutoEventWireup="true" CodeFile="draftauctiondetails.aspx.cs" Inherits="web_buyer_screens_DraftAuctionDetails" MaintainScrollPositionOnPostback="true" %>

<%@ Register Src="../usercontrol/auctionvendor/auctionparticipants.ascx" TagName="auctionparticipants" TagPrefix="uc4" %>

<%@ Register Src="../usercontrol/auctionvendor/auctionitems.ascx" TagName="auctionitems" TagPrefix="uc3" %>

<%@ Register Src="../usercontrol/auctionvendor/auctiondetail.ascx" TagName="auctiondetail" TagPrefix="uc2" %>

<%@ Register Src="../usercontrol/AuctionVendor/auctionattachments.ascx" TagName="auctionattachments"
    TagPrefix="uc1" %>

<%@ Register TagPrefix="EBid" TagName="TopNavAuction" Src="~/WEB/usercontrol/Buyer/TopNavAuctions.ascx" %>
<%@ Register TagPrefix="EBid" TagName="TopNav_Auctions" Src="~/web/usercontrol/Buyer/TopNav2_Auction.ascx" %>
<%@ Register TagPrefix="EBid" TagName="LeftNavAuctions1" Src="~/WEB/usercontrol/Buyer/LeftNav.ascx" %>
<%@ Register TagPrefix="EBid" TagName="LeftNavNotifications" Src="~/web/usercontrol/Buyer/LeftNavNotifications.ascx" %>
<%@ Register TagPrefix="EBid" TagName="CommentBox" Src="~/web/usercontrol/CommentBox.ascx" %>
<%@ Register TagPrefix="EBid" TagName="GlobalLinksNav" Src="~/web/usercontrol/GlobalLinksNav.ascx" %>
<%@ Register TagPrefix="EBid" TagName="Footer" Src="~/web/usercontrol/Footer.ascx" %>
<html xmlns="http://www.w3.org/1999/xhtml" style="height: 100%;">
<head>
<%@ Register TagPrefix="EBid" TagName="jqueryLoader" Src="../usercontrol/jQuery_loader.ascx" %>
    <EBid:jqueryLoader runat="server" ID="jqueryLoader1" />
    <title></title>
    <meta http-equiv="Content-Language" content="en-us" />
    <meta http-equiv="Content-Type" content="text/html; charset=windows-1252" />
    <link rel="stylesheet" type="text/css" href='<%= "../themes/"+Session["configTheme"]+"/css/style.css" %>' />
    <link rel="stylesheet" type="text/css" href='<%="../themes/"+Session["configTheme"]+"/css/style_buyer.css" %>' />
    <link href='<%= "../themes/"+Session["configTheme"]+"/css/style.css" %>' rel="stylesheet" type="text/css" />
    <link href="../../css/style.css' rel="stylesheet" type="text/css" />
    <link href='<%= "../themes/"+Session["configTheme"]+"/css/style.css" %>' rel="stylesheet" type="text/css" />
    <link href='<%= "../themes/"+Session["configTheme"]+"/css/style.css" %>' rel="stylesheet" type="text/css" />
    <link href='<%= "../themes/"+Session["configTheme"]+"/css/style.css" %>' rel="stylesheet" type="text/css" />
    <link href='<%= "../themes/"+Session["configTheme"]+"/css/style.css" %>' rel="stylesheet" type="text/css" />
    <link href='<%= "../themes/"+Session["configTheme"]+"/css/style.css" %>' rel="stylesheet" type="text/css" />
    <link href='<%= "../themes/"+Session["configTheme"]+"/css/style.css" %>' rel="stylesheet" type="text/css" />
</head>
<body style="height: 100%;">
    <div align="left">
        <table border="0" cellpadding="0" cellspacing="0" width="100%" id="page">
            <form runat="server">
                <tr>
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
                                    <EBid:TopNav_Auctions runat="server" ID="TopNav_Auctions" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr height='100%'>
                    <td>
                        <table border="0" cellpadding="0" cellspacing="0" width="100%" height='100%'>
                            <tr>
                                <td id="relatedInfo">
                                    <div align="left">
                                        <EBid:LeftNavAuctions1 runat="server" ID="LeftNavAuctions1" />
                                    </div>
                                    <div align="left">
                                        <EBid:CommentBox runat="server" ID="CommentBox" />
                                    </div>
                                    <p>&nbsp;</p>
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
                                                                Auction Event Details</h1>
                                                            <div>
                                                                <uc2:auctiondetail ID="Auctiondetail1" runat="server" />
                                                                <table border="0" cellpadding="0" cellspacing="0" width="100%" id="itemDetails">
                                                                    <tr>
                                                                        <th colspan="2">
                                                                            Auction Event details</th>
                                                                    </tr>
                                                                    <tr>
                                                                        <td width="160px">
                                                                            Type:
                                                                        </td>
                                                                        <td class="value" width="80%">
                                                                            <asp:Label runat="server" ID="lblAuctionType"></asp:Label></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td width="160px">
                                                                            Confirmation Deadline:</td>
                                                                        <td class="value" width="80%">
                                                                            <asp:Label runat="server" ID="lblAuctionConfirmationDeadline"></asp:Label></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td width="160px">
                                                                            Event Date:</td>
                                                                        <td class="value" width="80%">
                                                                            <asp:Label runat="server" ID="lblAuctionEventDate"></asp:Label></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td width="160px">
                                                                            Start Time:</td>
                                                                        <td class="value" width="80%">
                                                                            <asp:Label runat="server" ID="lblAuctionStartTime"></asp:Label></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td width="160px">
                                                                            End Time:</td>
                                                                        <td class="value" width="80%">
                                                                            <asp:Label runat="server" ID="lblAuctionEndTime"></asp:Label></td>
                                                                    </tr>
                                                                </table>
                                                                <uc3:auctionitems ID="Auctionitems1" runat="server" />
                                                                <asp:GridView runat="server" ID="gvAuctionItems" AutoGenerateColumns="false" CssClass="itemDetails">
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="Item" HeaderStyle-CssClass="itemDetails_th" ItemStyle-CssClass="itemDetails_td">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblItem" Text='<%# Bind("PItemName2") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Description" HeaderStyle-CssClass="itemDetails_th" ItemStyle-CssClass="value">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblDesc" Text='<%# Bind("Description") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Quantity" HeaderStyle-CssClass="itemDetails_th" ItemStyle-CssClass="value">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblQuantity" Text='<%# Bind("Quantity") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Unit of Measure" HeaderStyle-CssClass="itemDetails_th" ItemStyle-CssClass="value">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblUnitOfMeasure" Text='<%# Bind("UnitOfMeasure") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                </asp:GridView>
                                                                <uc1:auctionattachments id="Auctionattachments1" runat="server">
                                                                </uc1:auctionattachments>
                                                                <asp:GridView runat="server" ID="gvSuppliers" AutoGenerateColumns="false" CssClass="itemDetails">
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="Invited Suppliers" HeaderStyle-CssClass="itemDetails_th" ItemStyle-CssClass="value">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblAccType" Text='<% #Bind("AccType")%>'></asp:Label><asp:Label runat="server" ID="lblSuppliers" Text='<% #Bind("Supplier")%>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                </asp:GridView>
                                                                <br />
                                                                <table border="0" cellpadding="0" cellspacing="0" width="100%" id="actions">
                                                                    <tr>
                                                                        <td>
                                                                            <asp:LinkButton runat="server" ID="btnEdit1" OnClick="btnEdit1_Click" CausesValidation="false">Edit</asp:LinkButton>
                                                                            <asp:LinkButton runat="server" ID="btnSubmit1" OnClick="btnSubmit1_Click">Submit</asp:LinkButton>
                                                                            <asp:LinkButton runat="server" ID="lnkOk" CausesValidation="false" OnClick="lnkOk_Click">OK</asp:LinkButton></td>
                                                                    </tr>
                                                                </table>
                                                                <p>
                                                                    &nbsp;<asp:TextBox runat="server" ID="txtSuppliers" Width="0"></asp:TextBox></p>
                                                                <p>
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
                    <td id="footer">
                        <EBid:Footer runat="server" ID="Footer" />
                        <input type="hidden" runat="server" id="hdnAuctionRefNo" name="hdnAuctionRefNo" />
                        <input type="hidden" runat="server" id="hdnDetailNo" />
                        <asp:RequiredFieldValidator ID="rfvSuppliers" runat="server" ErrorMessage="Suppliers is a required field." ControlToValidate="txtSuppliers" Display="None"></asp:RequiredFieldValidator>
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowSummary="false" ShowMessageBox="true" />
                    </td>
                </tr>
            </form>
        </table>
    </div>
</body>
</html>
