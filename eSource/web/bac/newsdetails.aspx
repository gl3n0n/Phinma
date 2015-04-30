<%@ Page Language="c#" EnableEventValidation="true" Inherits="EBid.WEB.bac_screens.newsdetails" CodeFile="newsdetails.aspx.cs" %>

<%@ Register Src="../usercontrol/newsdetail.ascx" TagName="newsdetail" TagPrefix="uc2" %>
<%@ Register Src="../usercontrol/news_announcements_nav.ascx" TagName="news_announcements_nav" TagPrefix="uc1" %>
<%@ Register TagPrefix="EBid" TagName="TopDate" Src="~/WEB/usercontrol/TopDate.ascx" %>
<%@ Register TagPrefix="EBid" TagName="GlobalLinksNav" Src="~/web/usercontrol/GlobalLinksNav.ascx" %>
<%@ Register TagPrefix="EBid" TagName="BAC_TopNav_Home" Src="~/web/usercontrol/bac/BAC_TopNav_Home.ascx" %>
<%@ Register TagPrefix="EBid" TagName="Footer" Src="~/web/usercontrol/Footer.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml" style="height: 100%;">
<head>    
    <title id="PageTitle" runat="server"></title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1" />
    <meta name="CODE_LANGUAGE" content="C#" />
    <meta name="vs_defaultClientScript" content="JavaScript" />
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5" />
    <meta http-equiv="Content-Language" content="en-us" />
    <meta http-equiv="Content-Type" content="text/html; charset=windows-1252" />
    <link rel="stylesheet" type="text/css" href='<%= "../themes/"+Session["configTheme"]+"/css/style_ph.css" %>' />
    <link href='<%= "../themes/"+Session["configTheme"]+"/css/style.css" %>' rel="stylesheet" type="text/css" />

</head>
<body style="height: 100%;">
    <div align="left">
        <form id="Form1" method="post" runat="server">
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
                                    <EBid:BAC_TopNav_Home runat="server" ID="BAC_TopNav_Home" />
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
                        <table border="0" cellpadding="0" cellspacing="0" width="100%" style="height: 100%;">
                            <tr>
                                <td id="relatedInfo">
                                    <uc1:news_announcements_nav ID="News_announcements_nav1" runat="server" />
                                    
                                </td>
                                <td id="content">
                                    <div align="left">                                        
                                        <uc2:newsdetail ID="Newsdetail1" runat="server" />
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
