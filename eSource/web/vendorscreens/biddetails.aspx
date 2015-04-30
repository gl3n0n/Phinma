<%@ Page Language="c#" Inherits="EBid.web.vendor_screens.BidDetails2" CodeFile="BidDetails.aspx.cs" %>

<%@ Register Src="../usercontrol/commentlist.ascx" TagName="commentlist" TagPrefix="uc6" %>
<%@ Register Src="../usercontrol/bids/biddetails_w_bidtenderdetails.ascx" TagName="biddetails_w_bidtenderdetails" TagPrefix="uc5" %>
<%@ Register Src="../usercontrol/bids/buyer_bidtenderdetails.ascx" TagName="buyer_bidtenderdetails" TagPrefix="uc3" %>
<%@ Register Src="../usercontrol/bids/biddetails_wo_suppliers_w_status.ascx" TagName="biddetails_wo_suppliers_w_status" TagPrefix="uc4" %>
<%@ Register Src="../usercontrol/bids/biddetails.ascx" TagName="biddetails" TagPrefix="uc2" %>
<%@ Register Src="../usercontrol/bids/biddetails_attachments.ascx" TagName="biddetails_attachments" TagPrefix="uc1" %>
<%@ Register TagPrefix="EBid" TagName="GlobalLinksNav" Src="~/web/usercontrol/GlobalLinksNav.ascx" %>
<%@ Register TagPrefix="EBid" TagName="CommentArea" Src="~/web/usercontrol/CommentArea.ascx" %>
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
    <link rel="stylesheet" type="text/css" href='<%= "../themes/"+Session["configTheme"]+"/css/style.css" %>' />
    <link rel="stylesheet" type="text/css" href='../themes/<%= Session["configTheme"] %>/css/style_v.css' />
    <link href='<%= "../themes/"+Session["configTheme"]+"/css/style.css" %>' rel="stylesheet" type="text/css" />
</head>
<body style="height: 100%;">
    <div align="left">
        <form id="Form1" runat="server" defaultbutton="btnBids">
            <table border="0" cellpadding="0" cellspacing="0" width="100%" id="page">
                <tr>
                    <td valign="top" height="137px">
                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td>
                                    <div align="left" id="masthead">
                                        <ebid:globallinksnav runat="server" id="GlobalLinksNav" />
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <ebid:topdate runat="server" id="TopDate" />
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
                                                <ebid:vendor_leftnav_bids runat="server" id="Vendor_LeftNav_Bids" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <br />
                                                <h2>
                                                    Comments</h2>
                                                <uc6:commentlist id="Commentlist1" runat="server" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td id="content"  >
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%" id="table5">
                                        <tr>
                                            <td id="content0">
                                                <h1>
                                                    <br />
                                                    Bid Event Details</h1>
                                                <div align="left">
                                                    <p>
                                                        <uc5:biddetails_w_bidtenderdetails id="Biddetails_w_bidtenderdetails1" runat="server" />
                                                        <uc1:biddetails_attachments id="Biddetails_attachments1" runat="server" />
                                                    </p>
                                                    <p style="text-align: center;">
                                                        <asp:Label ID="lblMessage" runat="server"></asp:Label>
                                                    </p>
                                                    <asp:Panel ID="pnlComments" runat="server">
                                                     <table border="0" cellpadding="0" cellspacing="0" width="100%" id="Table1">
                                                     <tr>
                                                        <td align="center">
                                                            <p style="text-align: center;">
                                                                Comment</p>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center">
                                                            <asp:TextBox ID="txtComment" runat="server" TextMode="MultiLine" Width="360px" Height="90px" Font-Names="Arial" MaxLength="999"></asp:TextBox><br />
                                                            <asp:CustomValidator ID="cvValidator" runat="server" ErrorMessage="* comments is required" OnServerValidate="cvValidator_ServerValidate" Font-Size="11px"></asp:CustomValidator></td>
                                                    </tr>
                                                    </table>
                                                    </asp:Panel>
                                                    <table border="0" cellpadding="0" cellspacing="0" width="100%" id="actions">
                                                        <tr>
                                                            <td style="height: 34px">                                                                
                                                                <asp:LinkButton runat="server" ID="btnBids" Width="90px" OnClick="btnBids_Click" OnClientClick="return confirm('Are you sure you want to confirm your participation to this bid event?');">Confirm</asp:LinkButton>
                                                                <asp:LinkButton runat="server" ID="btnDecline" Width="90px" OnClick="btnDecline_Click" OnClientClick="return confirm('Are you sure you want to decline your participation to this bid event?');">Decline</asp:LinkButton>
                                                                <asp:LinkButton ID="lnkBack" runat="server" Width="90px" OnClick="lnkBack_Click">Back</asp:LinkButton>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
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
                        <ebid:footer runat="server" id="Footer" />
                    </td>
                </tr>
            </table>
        </form>
    </div>
</body>
</html>
