<%@ Page Language="c#" Inherits="EBid.web.boc.biddetails" CodeFile="biddetails.aspx.cs" MaintainScrollPositionOnPostback="true" EnableEventValidation="false" %>

<%@ Register Src="../usercontrol/bids/biddetails_suppliers_boc.ascx" TagName="biddetails_suppliers_boc" TagPrefix="uc6" %>
<%@ Register Src="../usercontrol/boc/BOC_LeftNav_Bids.ascx" TagName="BOC_LeftNav_Bids" TagPrefix="uc4" %>
<%@ Register Src="../usercontrol/boc/BOC_TopNav_Bids.ascx" TagName="BOC_TopNav_Bids" TagPrefix="uc5" %>
<%@ Register Src="../usercontrol/bids/biddetails_wo_suppliers.ascx" TagName="biddetails_wo_suppliers" TagPrefix="uc3" %>
<%@ Register Src="../usercontrol/bids/biddetails.ascx" TagName="biddetails" TagPrefix="uc2" %>
<%@ Register Src="../usercontrol/bids/biddetails_attachments.ascx" TagName="biddetails_attachments" TagPrefix="uc1" %>
<%@ Register TagPrefix="EBid" TagName="GlobalLinksNav" Src="~/web/usercontrol/GlobalLinksNav.ascx" %>
<%@ Register TagPrefix="EBid" TagName="CommentArea" Src="~/web/usercontrol/CommentArea.ascx" %>
<%@ Register TagPrefix="EBid" TagName="Footer" Src="~/web/usercontrol/Footer.ascx" %>
<%@ Register TagPrefix="EBid" TagName="TopDate" Src="~/web/usercontrol/TopDate.ascx" %>
<html xmlns="http://www.w3.org/1999/xhtml" style="height: 100%;">
<head>
    <title id="PageTitle" runat="server"></title>
    <meta http-equiv="Content-Language" content="en-us" />
    <meta http-equiv="Content-Type" content="text/html; charset=windows-1252" />
    <link href='<%= "../themes/"+Session["configTheme"]+"/css/style_ph.css" %>' rel="stylesheet" type="text/css" />
    <link href='<%= "../themes/"+Session["configTheme"]+"/css/style.css" %>' rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../include/util.js"></script>
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
                                    <uc5:BOC_TopNav_Bids ID="BOC_TopNav_Bids1" runat="server" />
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
                        <table border="0" cellpadding="0" cellspacing="0" width="100%" style="height:100%">
                            <tr>
                                <td id="relatedInfo">
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                        <tr>
                                            <td>
                                                <h2>
                                                    Bids</h2>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <uc4:BOC_LeftNav_Bids ID="BOC_LeftNav_Bids1" runat="server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <br />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td id="content" style="width: 100%">
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%" id="table5">
                                        <tr>
                                            <td id="content0">
                                                <h1>
                                                    <br />
                                                    Bid Event Details</h1>
                                                <br />
                                                <uc3:biddetails_wo_suppliers ID="Biddetails_wo_suppliers1" runat="server" />
                                                <br />
                                                <div align="left">
                                                    <p>
                                                        <uc6:biddetails_suppliers_boc ID="Biddetails_suppliers1" runat="server" />
                                                    </p>
                                                    <p>
                                                        <uc1:biddetails_attachments ID="Biddetails_attachments1" runat="server" />
                                                    </p>
                                                </div>
                                                <div align="left">
                                                    <table border="0" cellpadding="0" cellspacing="0" width="100%" id="actions">
                                                        <tr>
                                                            <td>
                                                                <asp:HyperLink ID="lnkComparison" runat="server">Comparison</asp:HyperLink>
                                                                <asp:LinkButton runat="server" ID="lnkBack" OnClick="lnkBack_Click">Back</asp:LinkButton>
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
                        <EBid:Footer runat="server" ID="Footer1" />
                        <input type="hidden" runat="server" id="hdnFileAttachments" />
                        <input type="hidden" runat="server" id="hdnActualFileNames" />
                        <input type="hidden" runat="server" id="hdnBidRefNo" />
                    </td>
                </tr>
            </table>
        </form>
    </div>
</body>
</html>
