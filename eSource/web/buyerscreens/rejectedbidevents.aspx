<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rejectedbidevents.aspx.cs"
    Inherits="web_buyer_screens_rejectedBidEvents" %>

<%@ Register TagPrefix="EBid" TagName="LeftNav" Src="~/WEB/usercontrol/Buyer/LeftNav.ascx" %>
<%@ Register TagPrefix="EBid" TagName="TopNavBids" Src="~/WEB/usercontrol/Buyer/TopNavBids.ascx" %>
<%@ Register TagPrefix="EBid" TagName="TopNav2" Src="~/WEB/usercontrol/Buyer/TopNav2.ascx" %>
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
</head>
<body style="height: 100%;">
    <div>
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
                                        <EBid:TopNav2 runat="server" ID="TopNav2" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr valign="top">
                        <td valign="top">
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td id="relatedInfo">
                                        <div align="left">
                                            <EBid:LeftNav runat="server" ID="LeftNav" />
                                        </div>
                                        <div align="left">
                                            <table border="0" cellpadding="0" cellspacing="0" width="100%" id="table3">
                                                <tr>
                                                    <td>
                                                        <!-- #BeginEditable "commentArea" -->
                                                        &nbsp;<!-- #EndEditable --></td>
                                                </tr>
                                            </table>
                                        </div>
                                        <p>&nbsp;
                                            </p>
                                        <p>&nbsp;
                                            </p>
                                        <p>&nbsp;
                                            </p>
                                        <p>&nbsp;
                                            </p>
                                        <p>&nbsp;
                                            </p>
                                        <p>&nbsp;
                                            </p>
                                        <p>&nbsp;
                                            </p>
                                        <p>&nbsp;
                                            </p>
                                        <p>&nbsp;
                                            </p>
                                        <p>&nbsp;
                                            </p>
                                        <p>&nbsp;
                                            </p>
                                        <p>&nbsp;
                                            </p>
                                        <p>&nbsp;
                                            </p>
                                        <p>&nbsp;
                                            </p>
                                        <p>&nbsp;
                                            </p>
                                        <p>&nbsp;
                                            </p>
                                        <p>&nbsp;
                                            </p>
                                    </td>
                                    <td id="content">
                                        <!-- #BeginEditable "contentArea" -->
                                        <div align="left">
                                            <table border="0" cellpadding="0" cellspacing="0" width="100%" id="table4">
                                                <tr>
                                                    <td>
                                                        <h1>
                                                            <br />
                                                            Rejected Bid Events
                                                        </h1>
                                                        <table border="0" cellpadding="0" cellspacing="0" width="90%" id="pageDetails">
                                                            <tr>
                                                                <th>
                                                                    Bid items</th>
                                                                <th>
                                                                    <a href="#">Date rejected </a>
                                                                </th>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <a href="#">Bid Item 1001 Computer Parts</a></td>
                                                                <td>
                                                                    &nbsp;November 18, 2005</td>
                                                            </tr>
                                                            <tr>
                                                                <td class="evenCells">
                                                                    <a href="#">Bid Item 1002 Hard Disks</a></td>
                                                                <td class="evenCells">
                                                                    &nbsp;December 15, 2005</td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <a href="#">Bid Item 1001 Computer Parts</a></td>
                                                                <td>
                                                                    &nbsp;November 18, 2005</td>
                                                            </tr>
                                                            <tr>
                                                                <td class="evenCells">
                                                                    <a href="#">Bid Item 1002 Hard Disks</a></td>
                                                                <td class="evenCells">
                                                                    &nbsp;December 15, 2005</td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <a href="#">Bid Item 1001 Computer Parts</a></td>
                                                                <td>
                                                                    &nbsp;November 18, 2005</td>
                                                            </tr>
                                                            <tr>
                                                                <td class="evenCells">
                                                                    <a href="#">Bid Item 1002 Hard Disks</a></td>
                                                                <td class="evenCells">
                                                                    &nbsp;December 15, 2005</td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <a href="#">Bid Item 1001 Computer Parts</a></td>
                                                                <td>
                                                                    &nbsp;November 18, 2005</td>
                                                            </tr>
                                                            <tr class="evenCells">
                                                                <td>
                                                                    <a href="#">Bid Item 1002 Hard Disks</a></td>
                                                                <td>
                                                                    &nbsp;December 15, 2005</td>
                                                            </tr>
                                                        </table>
                                                        <table border="0" cellpadding="0" cellspacing="0" width="100%" id="actions">
                                                            <tr>
                                                                <td>
                                                                    <asp:LinkButton runat="server" ID="btnCancel" OnClick="btnCancel_Click">Cancel</asp:LinkButton></td>
                                                            </tr>
                                                        </table>
                                                        <p>&nbsp;
                                                            </p>
                                                        <p>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                        <!-- #EndEditable -->
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td id="footer">
                            <EBid:Footer runat="server" ID="Footer" />
                        </td>
                    </tr>
                </form>
            </table>
        </div>
        &nbsp;</div>
</body>
<!-- #EndTemplate -->
</html>
