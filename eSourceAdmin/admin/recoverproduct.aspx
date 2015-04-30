<%@ Page Language="C#" AutoEventWireup="true" CodeFile="recoverproduct.aspx.cs" Inherits="web_buyerscreens_recoverproduct" %>

<%@ Register Src="../usercontrol/admin/AdminLeftNavList.ascx" TagName="AdminLeftNavList" TagPrefix="uc1" %>
<%@ Register Src="../usercontrol/admin/Admin_TopNav_List.ascx" TagName="Admin_TopNav_List" TagPrefix="uc2" %>
<%@ Register TagPrefix="EBid" TagName="TopDate" Src="~/usercontrol/TopDate.ascx" %>
<%@ Register TagPrefix="EBid" TagName="AdminTopNav" Src="~/usercontrol/admin/adminTopNav.ascx" %>
<%@ Register TagPrefix="EBid" TagName="Footer" Src="~/usercontrol/Footer.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>.:| Trans-Asia | Deleted Products |:.</title>
    <meta http-equiv="Content-Language" content="en-us" />
    <meta http-equiv="Content-Type" content="text/html; charset=windows-1252" />
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="../css/style_ua.css" />
   <script type="text/javascript" src="../include/util.js"></script> 
    <script language="javascript" type="text/javascript" src="../include/customValidation.js"></script>

</head>
<body>
    <div align="left" height="100%">
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
                    <td height="100%">
                        <table border="0" cellpadding="0" cellspacing="0" width="100%" height="100%">
                            <tr>
                                <td id="relatedInfo">
                                    <h2>
                                        Products</h2>
                                    <div align="left">
                                        <uc1:AdminLeftNavList ID="AdminLeftNavList1" runat="server" />
                                    </div>
                                    <br />
                                </td>
                                <td id="content">
                                    <br />
                                    <h1>
                                        Deleted Products</h1>
                                    <p><asp:Label ID="lblMessage" runat="server" CssClass="messagelabels"></asp:Label></p>
                                    <div align="left">
                                        <br />
                                         <asp:GridView ID="gvProducts" runat="server" AutoGenerateColumns="False"
                                            BorderWidth="1px" PageSize="15" AllowPaging="True" AllowSorting="True" DataSourceID="dsProducts"
                                            EmptyDataText="None" EmptyDataRowStyle-HorizontalAlign="Center" SkinID="AuctionEvents" OnRowCommand="gvProducts_RowCommand">                                                                
                                            <Columns>
                                                <asp:TemplateField HeaderText="SKU" SortExpression="SKU">
												    <HeaderStyle HorizontalAlign="Center" />
												    <ItemStyle HorizontalAlign="left" />																		
                                                    <ItemTemplate>
                                                        &nbsp;<asp:Label ID="lblSKU" runat="server" Text='<%# Bind("SKU") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Products" SortExpression="ProductDescription">
												    <HeaderStyle HorizontalAlign="Center" />                                                                        
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblProductiDesc" runat="server" Text='<%# Bind("ProductDescription") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkRecover" runat="server" Text="Recover" OnClientClick="return confirm('Recover this product?')"
                                                            CommandName="recoverProduct" CommandArgument='<%# Eval("SKU") %>'></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                        <asp:SqlDataSource ID="dsProducts" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>'
                                            SelectCommand="sp_GetDeletedProducts" SelectCommandType="StoredProcedure">
                                        </asp:SqlDataSource>
                                        
                                        <br />
                                    </div>
                                    <div align="left">
                                        <table border="0" cellpadding="0" cellspacing="0" width="100%" id="actions">
                                            <tr>
                                                <td align="left" style="height: 34px; width: 65%;">
                                                    &nbsp;
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
                    <td id="footer">
                        <EBid:Footer runat="server" ID="Footer1" />
                        <asp:CustomValidator ID="cuvValidate" runat="server" ClientValidationFunction="ValidatorIndividualAlert(this, args);" Display="None"></asp:CustomValidator>
                    </td>
                </tr>
            </table>
        </form>
    </div>
</body>
</html>