<%@ Page Language="C#" AutoEventWireup="true" CodeFile="auctionsubmit.aspx.cs" Inherits="web_buyer_screens_AuctionSubmit" MaintainScrollPositionOnPostback="true" %>

<%@ Register Src="../usercontrol/auctionvendor/auctionitems.ascx" TagName="auctionitems" TagPrefix="uc3" %>

<%@ Register Src="../usercontrol/auctionvendor/auctiondetail.ascx" TagName="auctiondetail" TagPrefix="uc2" %>

<%@ Register Src="../usercontrol/AuctionVendor/auctionattachments.ascx" TagName="auctionattachments"
    TagPrefix="uc1" %>

<%@ Register TagPrefix="EBid" TagName="TopNavAuction" Src="~/WEB/usercontrol/Buyer/TopNavAuctions.ascx" %>
<%@ Register TagPrefix="EBid" TagName="TopNav_Auctions" Src="~/web/usercontrol/Buyer/TopNav2_Auction.ascx" %>
<%@ Register TagPrefix="EBid" TagName="LeftNavAuctions1" Src="~/WEB/usercontrol/Buyer/LeftNav.ascx" %>
<%@ Register TagPrefix="EBid" TagName="LeftNavNotifications" Src="~/web/usercontrol/Buyer/LeftNavNotifications.ascx" %>
<%@ Register TagPrefix="EBid" TagName="GlobalLinksNav" Src="~/web/usercontrol/GlobalLinksNav.ascx" %>
<%@ Register TagPrefix="EBid" TagName="CommentBox" Src="~/web/usercontrol/CommentBox.ascx" %>
<%@ Register TagPrefix="EBid" TagName="Footer" Src="~/web/usercontrol/Footer.ascx" %>
<html xmlns="http://www.w3.org/1999/xhtml" style="height: 100%;">
<head>
    <%@ Register TagPrefix="EBid" TagName="jqueryLoader" Src="../usercontrol/jQuery_loader.ascx" %>
    <EBid:jqueryLoader runat="server" ID="jqueryLoader1" />
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
</head>
<body style="height: 100%;">
    <div align="left">
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
                            <tr>
                                <td>
                                    <EBid:TopNav_Auctions runat="server" ID="TopNav_Auctions" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td id="relatedInfo">
                                    <div align="left">
                                        <EBid:LeftNavAuctions1 runat="server" ID="LeftNavAuctions1" />
                                    </div>
                                    <div align="left">
                                        <EBid:CommentBox runat="server" ID="CommentBox" />
                                    </div>
                                    <p>
                                        &nbsp;</p>
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
                                                                Auction Item Submission Confirmation</h1>
                                                            <p>
                                                                You have successfully submitted the following Auction Event for Approval.</p>
                                                            <div align="left">
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
                                                                    <td class="value">
                                                                        <asp:Label runat="server" ID="lblAuctionType"></asp:Label></td>
                                                                </tr>
                                                                <tr>
                                                                    <td width="160px">
                                                                        Confirmation Deadline:</td>
                                                                    <td class="value">
                                                                        <asp:Label runat="server" ID="lblAuctionConfirmationDeadline"></asp:Label></td>
                                                                </tr>
                                                                <tr>
                                                                    <td width="160px">
                                                                        Event Date:</td>
                                                                    <td class="value">
                                                                        <asp:Label runat="server" ID="lblAuctionEventDate"></asp:Label></td>
                                                                </tr>
                                                                <tr>
                                                                    <td width="160px">
                                                                        Start Time:</td>
                                                                    <td class="value">
                                                                        <asp:Label runat="server" ID="lblAuctionStartTime"></asp:Label></td>
                                                                </tr>
                                                                <tr>
                                                                    <td width="160px">
                                                                        End Time:</td>
                                                                    <td class="value">
                                                                        <asp:Label runat="server" ID="lblAuctionEndTime"></asp:Label></td>
                                                                </tr>
                                                            </table>
                                                            <uc3:auctionitems ID="Auctionitems1" runat="server" />
                                                            <uc1:auctionattachments ID="Auctionattachments1" runat="server" />
                                                            <asp:GridView runat="server" ID="gvSuppliers" AutoGenerateColumns="false" CssClass="itemDetails">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="Invited Suppliers" HeaderStyle-CssClass="itemDetails_th" ItemStyle-CssClass="value">
                                                                        <ItemTemplate>
                                                                            <asp:Label runat="server" ID="lblSuppliers" Text='<% #Bind("Supplier")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                            </asp:GridView>
                                                            <br />
                                                            <table border="0" cellpadding="0" cellspacing="0" width="100%" id="actions">
                                                                <tr>
                                                                    <td>
                                                                        <asp:LinkButton runat="server" ID="btnOk" OnClick="btnOk_Click">OK</asp:LinkButton>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            <br /><br />
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
                        <EBid:Footer runat="server" ID="Footer" />
                        <input type="hidden" runat="server" id="hdnAuctionRefNo" name="hdnAuctionRefNo" />
                    </td>
                </tr>
            </table>
        </form>
    </div>
</body>
</html>
