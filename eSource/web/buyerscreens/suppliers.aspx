<%@ Page Language="C#" AutoEventWireup="true" CodeFile="suppliers.aspx.cs" Inherits="WEB_buyer_screens_suppliers" %>

<%@ Register Src="../usercontrol/Footer.ascx" TagName="Footer" TagPrefix="uc1" %>
<%@ Register TagPrefix="EBid" TagName="TopNavSuppliers" Src="~/WEB/usercontrol/Buyer/TopNavSuppliers.ascx" %>
<%@ Register TagPrefix="EBid" TagName="TopNav5" Src="~/WEB/usercontrol/Buyer/TopNav2.ascx" %>
<%@ Register TagPrefix="EBid" TagName="LeftNavSuppliers" Src="~/WEB/usercontrol/Buyer/LeftNav.ascx" %>
<%@ Register TagPrefix="EBid" TagName="GlobalLinksNav" Src="~/web/usercontrol/GlobalLinksNav.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml" style="height: 100%;">
<head>
<%@ Register TagPrefix="EBid" TagName="jqueryLoader" Src="../usercontrol/jQuery_loader.ascx" %>
    <EBid:jqueryLoader runat="server" ID="jqueryLoader1" />
    <title id="PageTitle" runat="server"></title>
    <meta http-equiv="Content-Language" content="en-us" />
    <meta http-equiv="Content-Type" content="text/html; charset=windows-1252" />
    <link rel="stylesheet" type="text/css" href='<%= "../themes/"+Session["configTheme"]+"/css/style.css" %>' />
    <link rel="stylesheet" type="text/css" href='<%="../themes/"+Session["configTheme"]+"/css/style_buyer.css" %>' />
</head>
<body style="height: 100%;">
    <div align="left">
        <form runat="server" method="post">
            <table border="0" cellpadding="0" cellspacing="0" width="100%" id="page">
                <tr height="137px">
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
                                    <EBid:TopNav5 runat="server" ID="TopNav5" />
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
                                        <EBid:LeftNavSuppliers runat="server" ID="LeftNavSuppliers1" />
                                    </div>
                                    <div align="left">
                                        <table border="0" cellpadding="0" cellspacing="0" width="100%" id="table3">
                                            <tr>
                                                <td>
                                                    <div align="left">
                                                        <table border="0" cellpadding="0" cellspacing="0" width="100%" id="table4">
                                                            <tr>
                                                                <td>
                                                                    <br />
                                                                    <br />
                                                                    &nbsp;</td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    <p>&nbsp;
                                        </p>
                                </td>
                                <td id="content">
                                    <div align="left">
                                        <table border="0" cellpadding="0" cellspacing="0" width="100%" id="table5">
                                            <tr>
                                                <td>
                                                    <h1>
                                                        <br />
                                                        Suppliers</h1>
                                                    <div align="left">
                                                        <table border="0" cellpadding="0" cellspacing="0" width="100%" id="itemDetails">
                                                            <tr>
                                                                <th class="value" colspan="2">
                                                                    Display suppliers under :</th>
                                                            </tr>
                                                            <tr>
                                                                <td class="value">
                                                                    <asp:DropDownList runat="server" ID="ddlCategory" AutoPostBack="True" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged">
                                                                    </asp:DropDownList></td>
                                                                <td class="value">
                                                                    <asp:DropDownList runat="server" ID="ddlSubCategory">
                                                                    </asp:DropDownList></td>
                                                            </tr>
                                                            <tr class="evencells">
                                                                <td>
                                                                    <table border="0" cellpadding="0" cellspacing="0" width="100%" id="table6">
                                                                        <tr>
                                                                            <td style="border: 0" valign="top">
                                                                                <asp:CheckBoxList ID="chkISOStandard" runat="server" VAlign="middle" NoWrap="nowrap" Align="left">
                                                                                </asp:CheckBoxList>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                                <td>
                                                                    <asp:DropDownList runat="server" ID="ddlPCAAB">
                                                                    </asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                            <tr class="value">
                                                                <td>
                                                                    <asp:DropDownList runat="server" ID="ddlBrands">
                                                                    </asp:DropDownList></td>
                                                                <td>
                                                                    <asp:DropDownList runat="server" ID="ddlServices">
                                                                    </asp:DropDownList></td>
                                                            </tr>
                                                            <tr class="evencells">
                                                                <td>
                                                                    <asp:DropDownList runat="server" ID="ddlLocations">
                                                                    </asp:DropDownList></td>
                                                                <td>
                                                                    <asp:DropDownList runat="server" ID="ddlItemsCarried">
                                                                    </asp:DropDownList></td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                    <div align="left">
                                                        <br />
                                                        <div align="left">
                                                            <table border="0" cellpadding="0" cellspacing="0" width="100%" id="actions">
                                                                <tr>
                                                                    <td>
                                                                        <asp:LinkButton ID="ddlSearch" runat="server" Text="Search" OnClick="ddlSearch_Click"></asp:LinkButton>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                        <p>
                                                        &nbsp;</div>
                                                    <div align="left">
                                                        <div align="left">
                                                            <asp:GridView runat="server" ID="gvSuppliers" AutoGenerateColumns="False" AllowPaging="true" SkinID="AuctionedItems"
                                                                OnRowCommand="gvSuppliers_RowCommand" OnPageIndexChanging="gvSuppliers_PageIndexChanging">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="Supplier">
                                                                        <ItemTemplate>
                                                                            &nbsp;<asp:LinkButton runat="server" ID="lnkSuppliers" Text='<%# Bind("VendorName") %>' CommandArgument='<%# Bind("VendorId") %>' CommandName="Select"></asp:LinkButton>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                            </asp:GridView>
                                                        </div>
                                                    </div>
                                                    <p>&nbsp;
                                                        </p>
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
                        <uc1:Footer ID="Footer1" runat="server" />
                    </td>
                </tr>
            </table>
        </form>
    </div>
</body>
</html>
