<%@ Page Language="C#" AutoEventWireup="true" CodeFile="bideventdetails.aspx.cs" Inherits="web_boc_bideventdetails" %>

<%@ Register Src="../usercontrol/bids/biddetails_suppliers_boc.ascx" TagName="biddetails_suppliers_boc" TagPrefix="uc5" %>

<%@ Register Src="../usercontrol/commentlist.ascx" TagName="commentlist" TagPrefix="uc4" %>

<%@ Register Src="../usercontrol/bids/biddetails_wo_suppliers.ascx" TagName="biddetails_wo_suppliers" TagPrefix="uc3" %>
<%@ Register Src="../usercontrol/bids/biddetails.ascx" TagName="biddetails" TagPrefix="uc2" %>
<%@ Register Src="../usercontrol/bids/biddetails_attachments.ascx" TagName="biddetails_attachments" TagPrefix="uc1" %>

<%@ Register TagPrefix="EBid" TagName="LeftNav" Src="~/WEB/usercontrol/boc/BOC_LeftNav_Bids.ascx" %>
<%@ Register TagPrefix="EBid" TagName="TopNavBids" Src="~/WEB/usercontrol/boc/BOC_TopNav_Bids.ascx" %>t

<%@ Register Src="../usercontrol/announcementdetail.ascx" TagName="announcementdetail" TagPrefix="uc2" %>
<%@ Register Src="../usercontrol/news_announcements_nav.ascx" TagName="news_announcements_nav" TagPrefix="uc3" %>

<%@ Register Src="../usercontrol/boc/BOC_TopNav_Home.ascx" TagName="BOC_TopNav_Home"
    TagPrefix="uc1" %>
<%@ Register TagPrefix="EBid" TagName="TopDate" Src="~/WEB/usercontrol/TopDate.ascx" %>
<%@ Register TagPrefix="EBid" TagName="GlobalLinksNav" Src="~/web/usercontrol/GlobalLinksNav.ascx" %>
<%@ Register TagPrefix="EBid" TagName="Footer" Src="~/web/usercontrol/Footer.ascx" %>
<html xmlns="http://www.w3.org/1999/xhtml" style="height: 100%;">
<head>
    <title id="PageTitle" runat="server"></title>
    <meta http-equiv="Content-Language" content="en-us" />
    <meta http-equiv="Content-Type" content="text/html; charset=windows-1252" />
    <link rel="stylesheet" type="text/css" href='<%= "../themes/"+Session["configTheme"]+"/css/style.css" %>' />
    <link rel="stylesheet" type="text/css" href='<%= "../themes/"+Session["configTheme"]+"/css/style_ph.css" %>' />

</head>
<body style="height: 100%;">
    <div align="left">
        <form id="Form1" runat="server" defaultbutton="lnkBack">
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
                                    <EBid:TopNavBids runat="server" ID="TopNavBids" />
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
                                    <h2>
                                        Bid Events</h2>
                                    <EBid:LeftNav runat="server" ID="LeftNav" />
     
                                </td>
                                <td id="content" style="width: 100%">
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%" id="table5">
                                        <tr>
                                            <td id="content0">
                                                <h1>
                                                    <br />
                                                    Bid Event Details</h1>
                                                <br />
                                                <div align="left">
                                                    <p>
                                                        <uc3:biddetails_wo_suppliers ID="Biddetails_wo_suppliers1" runat="server" />
                                                    </p>
                                                    <p>
                                                        <uc1:biddetails_attachments ID="Biddetails_attachments1" runat="server" />
                                                    </p>
                                                    <p>
                                                        <uc5:biddetails_suppliers_boc ID="Biddetails_suppliers1" runat="server" />
                                                    </p>
                                                    <p style="text-align: center;">
                                                        <asp:Label ID="lblMessage" runat="server"></asp:Label>
                                                    </p>
                                                    <table border="0" cellpadding="0" cellspacing="0" width="100%" id="actions">
                                                        <tr>
                                                            <td style="height: 34px">
                                                                &nbsp;
                                                                <asp:HyperLink ID="lnkViewReport" runat="server" Width="160px" Visible="False">Comparison By Bid Event</asp:HyperLink> 
                                                                <asp:LinkButton ID="lnkBack" runat="server" OnClick="lnkBack_Click" Width="100px">Back</asp:LinkButton>
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
                    </td>
                </tr>
            </table>
        </form>
    </div>
</body>
</html>
