<%@ Page Language="C#" AutoEventWireup="true" CodeFile="awardedbiditemdetails.aspx.cs" Inherits="web_purchasingscreens_awardedbiditemdetails" %>

<%@ Register Src="../usercontrol/bids/pbuyer_bidtenderdetails.ascx" TagName="pbuyer_bidtenderdetails"
    TagPrefix="uc3" %>

<%@ Register Src="../usercontrol/bids/biditemdetails.ascx" TagName="biditemdetails"
    TagPrefix="uc2" %>

<%@ Register Src="../usercontrol/bids/biddetails_items.ascx" TagName="biddetails_items"
    TagPrefix="uc1" %>

<%@ Register Src="../usercontrol/bids/bidtenderdetails.ascx" TagName="bidtenderdetails"
    TagPrefix="uc2" %>

<%@ Register TagPrefix="EBid" TagName="TopDate" Src="~/WEB/usercontrol/TopDate.ascx" %>
<%@ Register TagPrefix="EBid" TagName="GlobalLinksNav" Src="~/web/usercontrol/GlobalLinksNav.ascx" %>
<%@ Register TagPrefix="EBid" TagName="Purchasing_TopNav_Bids" Src="~/web/usercontrol/Purchasing/Purchasing_TopNav_Bids.ascx" %>
<%@ Register TagPrefix="EBid" TagName="Purchasing_LeftNav" Src="~/web/usercontrol/Purchasing/Purchasing_LeftNav_Bids.ascx" %>
<%@ Register TagPrefix="EBid" TagName="Footer" Src="~/web/usercontrol/Purchasing/Footer.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml" style="height: 100%;">
<head>
<%@ Register TagPrefix="EBid" TagName="jqueryLoader" Src="../usercontrol/jQuery_loader.ascx" %>
    <EBid:jqueryLoader runat="server" ID="jqueryLoader1" />
    <title id="PageTitle" runat="server"></title>
    <meta http-equiv="Content-Language" content="en-us" />
    <meta http-equiv="Content-Type" content="text/html; charset=windows-1252" />
    <link rel="stylesheet" type="text/css" href='<%= "../themes/"+Session["configTheme"]+"/css/style.css" %>' />
    <link rel="stylesheet" type="text/css" href='<%="../themes/"+Session["configTheme"]+"/css/style_ph.css" %>' />
    <link href='<%= "../themes/"+Session["configTheme"]+"/css/style.css" %>' rel="stylesheet" type="text/css" />
    <link href='<%= "../themes/"+Session["configTheme"]+"/css/style.css" %>' rel="stylesheet" type="text/css" />
    <link href="../../css/style_ph.css' rel="stylesheet" type="text/css" />
 
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
                    <td>
                        <table border="0" cellpadding="0" cellspacing="0" width="100%" height="100%">
                            <tr>
                                <td id="relatedInfo">
                                    <div align="left">
                                    <EBid:Purchasing_LeftNav runat="server" ID="Purchasing_LeftNav" />
                                    </div>
                                    <h2>
                                                    Comments</h2>                                                
                                                <div align="center">
                                                    <div align="left" style="padding-left: 3px; text-align: justify;">
                                                        <asp:DataList ID="dlComments" runat="server" Width="100%">
                                                            <ItemStyle Font-Names="Arial" Font-Size="11px" />
                                                            <SeparatorStyle HorizontalAlign="Center" />
                                                            <FooterStyle HorizontalAlign="Center" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label3" runat="server" Text="Date Posted : " ForeColor="DimGray"></asp:Label>
                                                                <asp:Label ID="DatePostedLabel" runat="server" Text='<%# Eval("DatePosted") %>'></asp:Label><br />
                                                                <asp:Label ID="Label2" runat="server" Text="Author : " ForeColor="DimGray"></asp:Label>
                                                                <asp:Label ID="AuthorLabel" runat="server" Text='<%# Eval("FullName") %>'></asp:Label><br />
                                                                <asp:Label ID="Label1" runat="server" Text="Comment : " ForeColor="DimGray"></asp:Label><br />
                                                                &nbsp;&nbsp;<asp:Label ID="CommentLabel" runat="server" Text='<%# Eval("Comment") %>'></asp:Label>
                                                                <br />
                                                                <br />
                                                            </ItemTemplate>
                                                            <SeparatorTemplate>
                                                                - - -</SeparatorTemplate>
                                                            <FooterTemplate>
                                                                - - - - -</FooterTemplate>
                                                        </asp:DataList>
                                                    </div>
                                                    <div align="center" style="width: 100%; font-family: arial; font-size: 11px;">
                                                        <table width="100%">
                                                            <tr>
                                                                <td colspan="3" align="center">
                                                                    <asp:Label ID="lblCounts" Font-Names="Arial" Font-Size="11px" runat="server" />
                                                                </td>
                                                            </tr>
                                                            <tr id="trPagers" runat="server">
                                                                <td>
                                                                    &nbsp;</td>
                                                                <td valign="bottom" align="center">
                                                                    <asp:ImageButton ID="btnFirstPage" ImageUrl="~/web/themes/images/pager/firstpage.gif" BorderWidth="0"
                                                                        runat="server" OnClick="btnFirstPage_Click" CausesValidation="false" ToolTip="First Page" />
                                                                    <asp:ImageButton ID="btnPreviousPage" ImageUrl="~/web/themes/images/pager/prevpage.gif"
                                                                        BorderWidth="0" runat="server" OnClick="btnPreviousPage_Click" CausesValidation="false"
                                                                        ToolTip="Previous Page" />
                                                                    <asp:ImageButton ID="btnNextPage" ImageUrl="~/web/themes/images/pager/nextpage.gif" BorderWidth="0"
                                                                        runat="server" OnClick="btnNextPage_Click" CausesValidation="false" ToolTip="Next Page" />
                                                                    <asp:ImageButton ID="btnLastPage" ImageUrl="~/web/themes/images/pager/lastpage.gif" BorderWidth="0"
                                                                        runat="server" OnClick="btnLastPage_Click" CausesValidation="false" ToolTip="Last Page" />
                                                                </td>
                                                                <td>
                                                                    &nbsp;</td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                    <div>
                                                        <asp:Label ID="lblRecordCount" Visible="False" runat="server" />
                                                        <asp:Label ID="lblCurrentIndex" Visible="False" Text="0" runat="server" />
                                                        <asp:Label ID="lblPageSize" Visible="False" Text="5" runat="server" />
                                                    </div>
                                                    <br />
                                                </div>
                                </td>
                                <td id="content">
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%" id="page0">
                                        <tr>
                                            <td valign="top">
                                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                    <tr>
                                                        <td id="content0">
                                                            <br />
                                                            <h1>Awarded Bid Item Details</h1>                                                            
                                                            <uc2:biditemdetails ID="Biditemdetails1" runat="server" />
                                                            <uc3:pbuyer_bidtenderdetails ID="Buyer_pbidtenderdetails1" runat="server" />
                                                            <br />                                                            
                                                         </td>
                                                    </tr>
                                                                                                        
                                                </table>
                                                <table border="0" cellpadding="0" cellspacing="0" width="100%" id="actions">
                                                    <tr>
                                                        <td>
                                                            <asp:LinkButton runat="server" ID="lnkCancel" Text="Back" OnClick="lnkCancel_Click"></asp:LinkButton>
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
                    <td id="footer" style="height: 38px">
                        <EBid:Footer runat="server" ID="Footer1" />
                    </td>
                </tr>
            </table>
        </form>
    </div>
</body>
</html>
