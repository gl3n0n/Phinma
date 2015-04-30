<%@ Page Language="C#" AutoEventWireup="true" CodeFile="brands.aspx.cs" Inherits="admin_items" Theme="default" %>
<%@ Register Src="../usercontrol/admin/AdminLeftNavList.ascx" TagName="AdminLeftNavList" TagPrefix="uc1" %>
<%@ Register Src="../usercontrol/admin/Admin_TopNav_List.ascx" TagName="Admin_TopNav_List" TagPrefix="uc2" %>
<%@ Register TagPrefix="EBid" TagName="TopDate" Src="~/usercontrol/TopDate.ascx" %>
<%@ Register TagPrefix="EBid" TagName="AdminTopNav" Src="~/usercontrol/admin/adminTopNav.ascx" %>
<%@ Register TagPrefix="EBid" TagName="Footer" Src="~/usercontrol/Footer.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>.:| Trans-Asia | Brands |:.</title>
    <meta http-equiv="Content-Language" content="en-us" />
    <meta http-equiv="Content-Type" content="text/html; charset=windows-1252" />
    <script type="text/javascript" src="../include/util.js"></script>
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="../css/style_ua.css" />    
    <link href="../../css/style.css" rel="stylesheet" type="text/css" />
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
                                    <uc2:Admin_TopNav_List id="Admin_TopNav_List1" runat="server"></uc2:Admin_TopNav_List>
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
                                    <h2>Manage Lists</h2>
                                    <uc1:AdminLeftNavList ID="AdminLeftNavList1" runat="server" />
                                    <p>                                        
                                        &nbsp;</p>
                                </td>
                                <td id="content">
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                        <tr>
                                            <td id="content0">
                                                <h1><br />
                                                    Manage Brands
                                                </h1>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 30px" align="right">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label id="lblMsg" runat="server" ForeColor="Red" Visible="False" Font-Names="Arial" Font-Size="11px"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 30px;">                                                            
                                                <h3>Edit / Delete Brand</h3>
                                                <br />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center">
                                                <asp:GridView ID="gvBrands" runat="server" AllowPaging="True" AllowSorting="true" AutoGenerateColumns="False" 
                                                    SkinID="AuctionEvents" Width="100%" DataKeyNames="BrandId,BrandCount" DataSourceID="dsBrands" OnRowDataBound="gvBrands_RowDataBound">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="&nbsp;BrandName" SortExpression="BrandName">
                                                            <ItemStyle HorizontalAlign="Left" />
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("BrandName") %>' Width="100px" MaxLength="10"></asp:TextBox>
                                                            </EditItemTemplate>
                                                            <ItemTemplate>
                                                                &nbsp;<asp:Label ID="Label1" runat="server" Text='<%# Bind("BrandName") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="&nbsp;CategoryId" SortExpression="CategoryId">
                                                            <ItemStyle HorizontalAlign="Left" />
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <EditItemTemplate>
                                                                <asp:DropDownList ID="TextBox2" runat="server" AutoPostBack="False" 
                                                                    DataSourceID="dsCategories" DataTextField="CategoryName" DataValueField="CategoryId" 
                                                                    Width="200px">
                                                                </asp:DropDownList>
                                                                <asp:SqlDataSource ID="dsCategories" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>' SelectCommand="sp_GetAllProductCategory" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
                                                            </EditItemTemplate>
                                                            <ItemTemplate>
                                                                &nbsp;<asp:Label ID="Label2" runat="server" Text='<%# Bind("CategoryId") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="&nbsp;SubCategoryId" SortExpression="SubCategoryId">
                                                            <ItemStyle HorizontalAlign="Left" />
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <EditItemTemplate>
                                                                <asp:DropDownList ID="TextBox3" runat="server" AutoPostBack="False" 
                                                                    DataSourceID="dsSubCategories" DataTextField="SubCategoryName" DataValueField="SubCategoryId" 
                                                                    Width="200px">
                                                                </asp:DropDownList>
                                                                <asp:SqlDataSource ID="dsSubCategories" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>' SelectCommand="s3p_Ebid_GetAllProductSubCategory" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
                                                            </EditItemTemplate>
                                                            <ItemTemplate>
                                                                &nbsp;<asp:Label ID="Label3" runat="server" Text='<%# Bind("SubCategoryId") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:CommandField ShowEditButton="True">
                                                            <ItemStyle HorizontalAlign="Center" Width="40px" />
                                                        </asp:CommandField>
                                                        <asp:CommandField ShowDeleteButton="True">
                                                            <ItemStyle HorizontalAlign="Center" Width="50px" />
                                                        </asp:CommandField>
                                                    </Columns>
                                                </asp:GridView>                                                
                                                <asp:SqlDataSource ID="dsBrands" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>'
                                                    DeleteCommand="sp_DeleteBrand" DeleteCommandType="StoredProcedure" 
                                                    InsertCommand="sp_AddBrand" InsertCommandType="StoredProcedure" 
                                                    SelectCommand="s3p_EBid_GetAllBrands" SelectCommandType="StoredProcedure"
                                                    UpdateCommand="sp_UpdateBrand" UpdateCommandType="StoredProcedure" 
                                                    OnDeleted="dsBrands_Deleted" OnDeleting="dsBrands_Deleting" OnInserted="dsBrands_Inserted" 
                                                    OnUpdated="dsBrands_Updated" OnUpdating="dsBrands_Updating">
                                                    <DeleteParameters>
                                                        <asp:Parameter Name="BrandId" Type="Int32" />
                                                    </DeleteParameters>
                                                    <UpdateParameters>
                                                        <asp:Parameter Name="BrandId" Type="Int32" />
                                                        <asp:Parameter Name="BrandName" Type="String" />
                                                        <asp:Parameter Name="CategoryId" Type="String" />
                                                        <asp:Parameter Name="SubCategoryId" Type="Int32" />
                                                    </UpdateParameters>                                                    
                                                </asp:SqlDataSource>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 10px;" class="actions">                                                            
                                            </td>
                                        </tr> 
                                        <tr>
                                            <td><br /></td>
                                        </tr>
                                        <tr>
                                            <td style="height: 30px;">                                                            
                                                <h3>Add Brand</h3>
                                                <br />
                                            </td>
                                        </tr>                                                   
                                        <tr>
                                            <td>
                                                <p>                                                            
                                                Brand Name :&nbsp;
                                                <asp:TextBox ID="txtBrandName" Runat="server" Width="250px" MaxLength="10"></asp:TextBox>                                                            
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtBrandName"
                                                        Display="Dynamic" ErrorMessage="* Required" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                    <asp:CustomValidator ID="CustomValidator1" runat="server" Display="Dynamic" ErrorMessage="* Brand Name Already Exists" OnServerValidate="CustomValidator1_ServerValidate"></asp:CustomValidator><br /><br />
                                                </p>                                                            
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>                                                            
                                                <table border="0" cellpadding="0" cellspacing="0" width="100%" id="actions">
                                                    <tr>
                                                        <td>
                                                            <asp:LinkButton ID="lnkAdd" runat="server" OnClientClick="return confirm('Are you sure you want to add this new brand?');" OnClick="lnkAdd_Click">Add</asp:LinkButton>
                                                            <a href="brands.aspx">Cancel</a>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr><td style="height: 20px;"></td></tr>
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
