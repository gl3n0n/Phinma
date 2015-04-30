<%@ Page Language="C#" AutoEventWireup="true" CodeFile="report_totalbidevents_itemdetails.aspx.cs" Inherits="web_purchasingscreens_report_totalbidevents_itemdetails" %>

<%@ Register Src="~/web/usercontrol/Purchasing/Purchasing_LeftNav_Bids.ascx" TagName="Purchasing_LeftNav_Reports" TagPrefix="uc1" %>
<%@ Register TagPrefix="EBid" TagName="TopDate" Src="~/WEB/usercontrol/TopDate.ascx" %>
<%@ Register TagPrefix="EBid" TagName="GlobalLinksNav" Src="~/web/usercontrol/GlobalLinksNav.ascx" %>
<%@ Register TagPrefix="EBid" TagName="Purchasing_TopNav_Reports" Src="~/web/usercontrol/Purchasing/Purchasing_TopNav_Reports.ascx" %>
<%@ Register TagPrefix="EBid" TagName="Footer" Src="~/web/usercontrol/Purchasing/Footer.ascx"%>

<%@ Register Src="../usercontrol/bids/biddetails_suppliers.ascx" TagName="biddetails_suppliers" TagPrefix="uc5" %>
<%@ Register Src="../usercontrol/commentlist.ascx" TagName="commentlist" TagPrefix="uc4" %>
<%@ Register Src="../usercontrol/bids/biddetails_wo_suppliers.ascx" TagName="biddetails_wo_suppliers" TagPrefix="uc3" %>
<%@ Register Src="../usercontrol/bids/biddetails.ascx" TagName="biddetails" TagPrefix="uc2" %>
<%@ Register Src="../usercontrol/bids/biddetails_attachments.ascx" TagName="biddetails_attachments" TagPrefix="uc1" %>


<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml" style="height: 100%;">
<head>
<%@ Register TagPrefix="EBid" TagName="jqueryLoader" Src="../usercontrol/jQuery_loader.ascx" %>
    <EBid:jqueryLoader runat="server" ID="jqueryLoader1" />
    <title id="PageTitle" runat="server"></title>
    <meta http-equiv="Content-Language" content="en-us" />
    <meta http-equiv="Content-Type" content="text/html; charset=windows-1252" />
    <link href='<%= "../themes/"+Session["configTheme"]+"/css/style.css" %>' rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href='<%= "../themes/"+Session["configTheme"]+"/css/style.css" %>' />
    <link rel="stylesheet" type="text/css" href='<%="../themes/"+Session["configTheme"]+"/css/style_ph.css" %>' />
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
                                    <EBid:TopDate runat="server" ID="TopDate" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="content">
                        <table border="0" cellpadding="0" cellspacing="0" width="100%" style="height: 100%">
                            <tr>
                                <td id="relatedInfo">
                                    <uc1:Purchasing_LeftNav_Reports ID="Purchasing_LeftNav_Reports1" runat="server" />
                                </td>
                                <td id="content">
                                    <div align="left">
                                        <br />
                                        <h1>
                                            Bid Event Details </h1>
                                        <div>
                                        <!-- contents here //-->

                                        <p>
                                            <uc3:biddetails_wo_suppliers ID="Biddetails_wo_suppliers1" runat="server" />
                                        </p>
                                        <p>
                                            <uc1:biddetails_attachments ID="Biddetails_attachments1" runat="server" />
                                        </p>
                                        <p>
                                            <uc5:biddetails_suppliers ID="Biddetails_suppliers1" runat="server" />
                                        </p>
    
                                        <table border="0" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td id="actions">
                                                <asp:LinkButton ID="LinkButton1" runat="server" Width="100px" OnClick="LinkButton1_Click">Back</asp:LinkButton>
                                            </td>
                                        </tr>
                                        </table>
                                        <br />

                                        <!-- contents here //-->
                                        </div>
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
