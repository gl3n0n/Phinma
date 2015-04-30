<%@ Page Language="C#" AutoEventWireup="true" CodeFile="categoriesperboc.aspx.cs" Inherits="admin_categoriesperboc" %>

<%@ Register Src="../usercontrol/admin/AdminLeftNavList.ascx" TagName="AdminLeftNavList" TagPrefix="uc1" %>
<%@ Register Src="../usercontrol/admin/Admin_TopNav_List.ascx" TagName="Admin_TopNav_List" TagPrefix="uc2" %>
<%@ Register TagPrefix="EBid" TagName="TopDate" Src="~/usercontrol/TopDate.ascx" %>
<%@ Register TagPrefix="EBid" TagName="AdminTopNav" Src="~/usercontrol/admin/adminTopNav.ascx" %>
<%@ Register TagPrefix="EBid" TagName="Footer" Src="~/usercontrol/Footer.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>.:| Trans-Asia | Categories |:.</title>
    <meta http-equiv="Content-Language" content="en-us" />
    <meta http-equiv="Content-Type" content="text/html; charset=windows-1252" />

    <script type="text/javascript" src="../include/util.js"></script>

    <link href="../css/style.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="../css/style_ua.css" />
</head>
<body onload="SetStatus();">
    <div align="left">
        <form id="Form1" runat="server">
            <table border="0" cellpadding="0" cellspacing="0" width="100%" id="page">
                <tr>
                    <td valign="top" height="137px">
                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td>
                                    <div align="left" id="masthead">
                                        <EBid:AdminTopNav runat="server" ID="GlobalLinksNav" />
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <uc2:Admin_TopNav_List ID="Admin_TopNav_List1" runat="server"></uc2:Admin_TopNav_List>
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
                                    <h2>
                                        Manage Lists</h2>
                                    <uc1:AdminLeftNavList ID="AdminLeftNavList1" runat="server" />
                                    <p>
                                        &nbsp;</p>
                                </td>
                                <td id="content">
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                        <tr>
                                            <td id="content0">
                                                <h1>
                                                    <br />
                                                    Manage Categories per BOC Procurement
                                                </h1>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 30px" align="right">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblMsg" runat="server" ForeColor="Red" Visible="False" Font-Names="Arial" Font-Size="11px"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center">
                                                <asp:GridView ID="gvBOCProcurement" runat="server" AllowPaging="True" AllowSorting="true" AutoGenerateColumns="False" SkinID="AuctionEvents" Width="100%" DataSourceID="dsBOCProcurement"
                                                  OnRowCommand="gvBOCProcurement_RowCommand">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Name" SortExpression="FullName">
                                                            <ItemStyle HorizontalAlign="Left" />
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkEditBOCProcurement" runat="server" CommandName="EditBOCProcurement" CommandArgument='<%# Eval("BOCId") %>' Text='<%# Bind("FullName") %>'></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                                <asp:SqlDataSource ID="dsBOCProcurement" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>'
                                                    SelectCommand="sp_GetBOCProcurement" SelectCommandType="StoredProcedure">
                                                </asp:SqlDataSource>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 10px;" class="actions">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 20px;">
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
