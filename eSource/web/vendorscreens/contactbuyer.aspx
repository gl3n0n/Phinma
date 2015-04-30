<%@ Page Language="c#" Inherits="EBid.web.vendor_screens.contactbuyer" CodeFile="contactbuyer.aspx.cs" %>

<%@ Register Src="../usercontrol/news_announcements_nav.ascx" TagName="news_announcements_nav" TagPrefix="uc1" %>
<%@ Register TagPrefix="EBid" TagName="GlobalLinksNav" Src="~/web/usercontrol/GlobalLinksNav.ascx" %>
<%@ Register TagPrefix="EBid" TagName="Footer" Src="~/web/usercontrol/Vendor/Footer.ascx"  %>
<%@ Register TagPrefix="EBid" TagName="TopDate" Src="~/web/usercontrol/TopDate.ascx" %>
<%@ Register TagPrefix="EBid" TagName="Vendor_TopNav_Home" Src="~/web/usercontrol/Vendor/Vendor_TopNav_Home.ascx" %>
<%@ Register TagPrefix="EBid" TagName="Vendor_LeftNav_Bids" Src="~/web/usercontrol/Vendor/Vendor_LeftNav_Bids.ascx" %>
<%@ Register TagPrefix="EBid" TagName="Vendor_LeftNav_Auctions" Src="~/web/usercontrol/Vendor/Vendor_LeftNav_Auctions.ascx" %>
<html xmlns="http://www.w3.org/1999/xhtml" style="height: 100%;">
<head>
<%@ Register TagPrefix="EBid" TagName="jqueryLoader" Src="../usercontrol/jQuery_loader.ascx" %>
    <EBid:jqueryLoader runat="server" ID="jqueryLoader1" />
    <title id="PageTitle" runat="server"></title>
    <meta http-equiv="Content-Language" content="en-us" />
    <meta http-equiv="Content-Type" content="text/html; charset=windows-1252" />
    <link rel="stylesheet" type="text/css" href='<%= "../themes/"+Session["configTheme"]+"/css/style.css" %>' />
    <link rel="stylesheet" type="text/css" href='../themes/<%= Session["configTheme"] %>/css/style_v.css' />

    <script type="text/javascript" src="../include/util.js"></script>

    <link href='<%= "../themes/"+Session["configTheme"]+"/css/style.css" %>' rel="stylesheet" type="text/css" />
    <link href='<%= "../themes/"+Session["configTheme"]+"/css/style.css" %>' rel="stylesheet" type="text/css" />
    <link href='<%= "../themes/"+Session["configTheme"]+"/css/style.css" %>' rel="stylesheet" type="text/css" />
</head>
<body onLoad="SetStatus();">
    <div align="left">
        <form id="Form1" runat="server">
            <table border="0" cellpadding="0" cellspacing="0" id="page">
                <tr>
                    <td valign="top" height="137">
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
                    <td class="content" valign="top">
                        <table border="0" cellpadding="0" cellspacing="0" width="100%" height="100%">
                            <tr>
                                <td id="relatedInfo">
                                    <uc1:news_announcements_nav ID="News_announcements_nav1" runat="server" />
                                </td>
                                <td id="content">
                                    <div align="left">
                                        <br />
                                        <h1>
                                            Contact Buyer
                                        </h1>
                                        <p>
                                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                <tr>
                                                    <td width="80px">
                                                        <label>
                                                            Recipient:</label>
                                                    </td>
                                                    <td align="left">
                                                        <asp:DropDownList ID="ddlRecipient" runat="server" Width="400px" DataSourceID="sdsBuyers" DataTextField="BuyerEmail" DataValueField="BuyerId">
                                                        </asp:DropDownList>
                                                        <asp:SqlDataSource ID="sdsBuyers" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>' SelectCommand="sp_GetActiveBuyersWithEmailAds" SelectCommandType="StoredProcedure">
                                                        </asp:SqlDataSource>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" ControlToValidate="ddlRecipient" Display="Dynamic" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <label>
                                                            Subject:</label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtSubject" runat="server" Width="400px"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*" ControlToValidate="txtSubject" Display="Dynamic" SetFocusOnError="True">*</asp:RequiredFieldValidator></td>
                                                </tr>
                                                <tr>
                                                    <td valign="top">
                                                        <label>
                                                            Message:</label>
                                                    </td>
                                                    <td valign="top">
                                                        <asp:TextBox ID="txtMessage" runat="server" TextMode="MultiLine" Font-Names="Arial" Rows="15" Width="400px"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*" ControlToValidate="txtMessage" Display="Dynamic" SetFocusOnError="True">*</asp:RequiredFieldValidator></td>
                                                </tr>
                                            </table>
                                            <br />
                                            <asp:Label ID="lblMessage" runat="server" Text="" ForeColor="red"></asp:Label>
                                        </p>
                                        <table border="0" cellpadding="0" cellspacing="0" width="100%" id="actions">
                                            <tr>
                                                <td style="height: 19px">
                                                    <asp:LinkButton ID="lnkSend" runat="server" Text="Send" Width="120px" OnClick="lnkSend_Click"></asp:LinkButton>
                                                    <asp:HyperLink ID="lnkBack" runat="server" NavigateUrl="~/web/vendorscreens/index.aspx" Width="120px" Text="Back"></asp:HyperLink>
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
                        <EBid:Footer runat="server" ID="Footer1" />
                    </td>
                </tr>
            </table>
        </form>
    </div>
</body>
</html>
