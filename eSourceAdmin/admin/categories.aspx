<%@ Page Language="C#" AutoEventWireup="true" CodeFile="categories.aspx.cs" Inherits="admin_items" %>

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
    <style type="text/css">
        .style1
        {
            width: 195px;
        }
    </style>
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
                                                    Manage Categories
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
                                            <td style="height: 30px;">
                                                <h3>
                                                    Edit / Delete Category</h3>
                                                <br />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center">
                                                <asp:GridView ID="gvCategories" runat="server" AllowPaging="True" AllowSorting="true" AutoGenerateColumns="False" SkinID="AuctionEvents" Width="100%" DataKeyNames="CategoryId,CategoryCount" DataSourceID="dsCategories"
                                                    OnRowDataBound="gvCategories_RowDataBound">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="&nbsp;Category Id" SortExpression="CategoryId">
                                                            <ItemStyle HorizontalAlign="Left" />
                                                            <HeaderStyle HorizontalAlign="Center" Width="120px" />
                                                            <EditItemTemplate>
                                                                &nbsp;<asp:Label ID="lblCategoryId" runat="server" Text='<%# Bind("CategoryId") %>'></asp:Label>
                                                            </EditItemTemplate>
                                                            <ItemTemplate>
                                                                &nbsp;<asp:Label ID="lblCategoryId2" runat="server" Text='<%# Bind("CategoryId") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="&nbsp;Category Name" SortExpression="CategoryName">
                                                            <ItemStyle HorizontalAlign="Left" />
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="txtCategoryName" runat="server" Text='<%# Bind("CategoryName") %>' Width="250px" MaxLength="50"></asp:TextBox>
                                                            </EditItemTemplate>
                                                            <ItemTemplate>
                                                                &nbsp;<asp:Label ID="lblCategoryName" runat="server" Text='<%# Bind("CategoryName") %>'></asp:Label>
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
                                                <asp:SqlDataSource ID="dsCategories" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>' DeleteCommand="sp_DeleteCategory" DeleteCommandType="StoredProcedure" InsertCommand="sp_AddCategory"
                                                    InsertCommandType="StoredProcedure" SelectCommand="s3p_EBid_GetAllProductCategory" SelectCommandType="StoredProcedure" UpdateCommand="sp_UpdateCategory" UpdateCommandType="StoredProcedure" OnUpdated="dsCategories_Updated"
                                                    OnInserted="dsCategories_Inserted" OnDeleted="dsCategories_Deleted" OnDeleting="dsCategories_Deleting" OnUpdating="dsCategories_Updating">
                                                    <DeleteParameters>
                                                        <asp:Parameter Name="CategoryId" Type="String" />
                                                    </DeleteParameters>
                                                    <UpdateParameters>
                                                        <asp:Parameter Name="CategoryId" Type="String" />
                                                        <asp:Parameter Name="CategoryName" Type="String" />
                                                        <asp:Parameter Name="CategoryDesc" Type="String" DefaultValue="" />
                                                    </UpdateParameters>
                                                </asp:SqlDataSource>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 10px;" class="actions">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <br />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 30px;">
                                                <h3>
                                                    Add Category</h3>
                                                <br />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <p>
                                                    Category Id :&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                    <asp:TextBox ID="txtCategoryId" runat="server" Width="100px" MaxLength="7"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvCategoryId" runat="server" ControlToValidate="txtCategoryId" Display="Dynamic" ErrorMessage="* Required" SetFocusOnError="True"></asp:RequiredFieldValidator><br />
                                                    Category Name :&nbsp;
                                                    <asp:TextBox ID="txtCategoryName" runat="server" Width="250px" MaxLength="50"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvCategoryName" runat="server" ControlToValidate="txtCategoryName" Display="Dynamic" ErrorMessage="* Required" SetFocusOnError="True"></asp:RequiredFieldValidator><br />
                                                    <br />
                                                </p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <br />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 30px;">
                                                <h3>
                                                    Update Category</h3>
                                                <br />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <table>
                                                <tr>
                                                    <td align="left">
                                                        <asp:FileUpload ID="FileUpload1" runat="server" />
                                                        
                                                        <asp:LinkButton ID="Button1" runat="server" OnClick="UploadCSV"  CausesValidation="False"  Width="100px">Upload CSV File</asp:LinkButton><br /><asp:Label ID="Label1" runat="server" Font-Size="12px"></asp:Label>
                                                        <br />
                                                        
                                                    </td>
                                                    
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        
                                                
                                        <tr>
                                            <td>
                                                <table border="0" cellpadding="0" cellspacing="0" width="100%" id="actions">
                                                    <tr>
                                                        <td>
                                                        <asp:LinkButton ID="lnkDownloadCSVfromDB" runat="server" CausesValidation="False"  Width="150px">Download Categories</asp:LinkButton>
                                                        <asp:LinkButton ID="lnkUpdateDB" runat="server" Width="150px" CausesValidation="False" OnClientClick="return confirm('Are you sure you want to update entire Categories from CSV file?');">Update from CSV</asp:LinkButton>
                                                            <asp:LinkButton ID="lnkAdd" runat="server" OnClientClick="return confirm('Are you sure you want to add this new category?');" OnClick="lnkAdd_Click">Add</asp:LinkButton>
                                                            <a href="Categories.aspx">Cancel</a>
                                                        </td>
                                                    </tr>
                                                </table>
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
