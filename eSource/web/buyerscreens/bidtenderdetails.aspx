<%@ Page Language="C#" AutoEventWireup="true" CodeFile="bidtenderdetails.aspx.cs" Inherits="web_buyerscreens_bidtenderdetails" %>

<%@ Register Src="../usercontrol/bids/bidtender_attachments.ascx" TagName="bidtender_attachments" TagPrefix="uc5" %>
<%@ Register Src="../usercontrol/commentlist_tender.ascx" TagName="commentlist_tender" TagPrefix="uc4" %>
<%@ Register Src="../usercontrol/bids/buyer_bidtenderdetails.ascx" TagName="buyer_bidtenderdetails" TagPrefix="uc3" %>
<%@ Register Src="../usercontrol/bids/bidtenderdetails.ascx" TagName="bidtenderdetails" TagPrefix="uc2" %>
<%@ Register Src="../usercontrol/bids/biddetails_details.ascx" TagName="biddetails_details" TagPrefix="uc1" %>
<%@ Register TagPrefix="EBid" TagName="GlobalLinksNav" Src="~/web/usercontrol/GlobalLinksNav.ascx" %>
<%@ Register TagPrefix="EBid" TagName="Footer" Src="~/web/usercontrol/Footer.ascx" %>
<%@ Register TagPrefix="EBid" TagName="TopDate" Src="~/web/usercontrol/TopDate.ascx" %>
<%@ Register TagPrefix="EBid" TagName="LeftNav" Src="~/WEB/usercontrol/Buyer/LeftNav.ascx" %>
<%@ Register TagPrefix="EBid" TagName="TopNavBids" Src="~/WEB/usercontrol/Buyer/TopNavBids.ascx" %>
<%@ Register TagPrefix="EBid" TagName="TopNav2" Src="~/WEB/usercontrol/Buyer/TopNav2.ascx" %>
<%@ Register TagPrefix="EBid" TagName="TendersCommentBox" Src="~/web/usercontrol/TendersCommentBox.ascx" %>
<html xmlns="http://www.w3.org/1999/xhtml" style="height: 100%;">
<head>
<%@ Register TagPrefix="EBid" TagName="jqueryLoader" Src="../usercontrol/jQuery_loader.ascx" %>
    <EBid:jqueryLoader runat="server" ID="jqueryLoader1" />
    <title id="PageTitle" runat="server"></title>
    <meta http-equiv="Content-Language" content="en-us" />
    <meta http-equiv="Content-Type" content="text/html; charset=windows-1252" />
    <link rel="stylesheet" type="text/css" href='<%= "../themes/"+Session["configTheme"]+"/css/style.css" %>' />
    <link rel="stylesheet" type="text/css" href='<%="../themes/"+Session["configTheme"]+"/css/style_buyer.css" %>' />
    <script type="text/javascript" src="../include/customValidation.js"></script>
    <script type="text/javascript" src="../include/util.js"></script>
    <script type="text/javascript" src="../include/events.js"></script>
</head>
<body style="height: 100%;">
    <div align="left">
        <form runat="server" id="frmSubmitTender">
            <table border="0" cellpadding="0" cellspacing="0" width="100%" id="page">
                <tr>
                    <td valign="top" height="137px">
                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td>
                                    <div align="left" id="masthead">
                                        <ebid:globallinksnav id="GlobalLinksNav" runat="server" />
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
                                                <ebid:leftnav runat="server" id="LeftNav_Bids" />
                                                <br />
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
                                                <uc4:commentlist_tender id="Commentlist_tender1" runat="server" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td id="content">
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%" id="table9">
                                        <tr>
                                            <td id="content0">
                                                <div align="left">
                                                    <h1>
                                                        <br />
                                                        Bid Tender Details</h1>
                                                    <div align="left">
                                                        <uc2:bidtenderdetails id="dvBidtenderdetails" runat="server" />
                                                        <br />
                                                        <uc3:buyer_bidtenderdetails id="Buyer_bidtenderdetails1" runat="server"></uc3:buyer_bidtenderdetails>  &nbsp;
                                                    </div>
                                                    <div>
                                                        <asp:SqlDataSource ID="dsIncoterm" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>'
                                                            SelectCommand="s3p_EBid_GetIncoterm1" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
                                                        <asp:SqlDataSource ID="dsCurrencies" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>'
                                                            SelectCommand="sp_GetCurrencies" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
                                                    </div>
                                                    <asp:Panel ID="pnlComments" runat="server" Visible="false">
                                                        <p style="text-align: center;">
                                                            &nbsp;<br />
                                                            Comment<br />
                                                            <asp:TextBox ID="txtComment" runat="server" TextMode="MultiLine" Width="360px" Height="90px"
                                                                Font-Names="Arial" MaxLength="999"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="rfvComment" runat="server" Display="Dynamic" ErrorMessage="<br />Include a comment please."
                                                                ControlToValidate="txtComment" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                            <br />
                                                        </p>
                                                    </asp:Panel>
                                                    <br />
                                                    <div>
                                                        <table border="0" cellpadding="0" cellspacing="0" width="100%" id="actions">
                                                            <tr>
                                                                <td>
                                                                    &nbsp;<asp:LinkButton runat="server" ID="btnEndorse" OnClick="btnEndorse_Click" OnClientClick="return confirm('Are you sure you want to endorse this bid tender?');"
                                                                        Visible="False">Endorse</asp:LinkButton>
                                                                    <asp:LinkButton runat="server" ID="btnRenegotiate" OnClick="btnRenegotiate_Click"
                                                                        OnClientClick="return confirm('Are you sure you want to renegotiate this bid tender?');"
                                                                        Visible="False">Renegotiate</asp:LinkButton>
                                                                    <asp:LinkButton ID="lnkBack" runat="server" CausesValidation="False" OnClick="lnkBack_Click">Back</asp:LinkButton></td>
                                                            </tr>
                                                        </table>
                                                        <br />
                                                        <br />
                                                    </div>
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
                        <ebid:footer runat="server" id="Footer1" />
                    </td>
                </tr>
            </table>
        </form>
    </div>
</body>
</html>
