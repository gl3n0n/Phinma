<%@ Page Language="C#" AutoEventWireup="true" CodeFile="vendorcategories.aspx.cs" Inherits="admin_items" %>

<%@ Register Src="../usercontrol/admin/AdminLeftNavList.ascx" TagName="AdminLeftNavList" TagPrefix="uc1" %>
<%@ Register Src="../usercontrol/admin/Admin_TopNav_List.ascx" TagName="Admin_TopNav_List" TagPrefix="uc2" %>
<%@ Register TagPrefix="EBid" TagName="TopDate" Src="~/usercontrol/TopDate.ascx" %>
<%@ Register TagPrefix="EBid" TagName="AdminTopNav" Src="~/usercontrol/admin/adminTopNav.ascx" %>
<%@ Register TagPrefix="EBid" TagName="Footer" Src="~/usercontrol/Footer.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>.:| Trans-Asia | Vendor Categories |:.</title>
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
                                                    Manage Vendor Categories
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
                                                    List of Vendor Categories</h3>
                                                <br />
                                                <asp:Label ID="lblVendor" runat="server" Font-Size="11px" Text="Vendor :"></asp:Label>&nbsp;
                                                <asp:DropDownList ID="ddlVendor" runat="server" AutoPostBack="True" DataSourceID="dsVendors" DataTextField="VendorName" DataValueField="VendorId" Width="200px" OnSelectedIndexChanged="ddlVendor_SelectedIndexChanged">
                                                </asp:DropDownList>
                                                <asp:SqlDataSource ID="dsVendors" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>' SelectCommand="sp_GetAllVendors_2" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
                                    <p>
                                        &nbsp;</p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center">
                                                <asp:GridView ID="gvVendorCategories" runat="server" AllowPaging="True" AllowSorting="true" AutoGenerateColumns="False" SkinID="AuctionEvents" Width="100%" DataKeyNames="CategoryId,CategoryCount" DataSourceID="dsVendorCategories"
                                                    OnRowDataBound="gvVendorCategories_RowDataBound">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="&nbsp;Vendor Id" SortExpression="VendorId">
                                                            <ItemStyle HorizontalAlign="Left" />
                                                            <HeaderStyle HorizontalAlign="Center" Width="60px" />
                                                            <EditItemTemplate>
                                                                &nbsp;<asp:Label ID="lblVendorId" runat="server" Text='<%# Bind("VendorId") %>'></asp:Label>
                                                            </EditItemTemplate>
                                                            <ItemTemplate>
                                                                &nbsp;<asp:Label ID="lblVendorId2" runat="server" Text='<%# Bind("VendorId") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="&nbsp;Vendor Name" SortExpression="VendorName">
                                                            <ItemStyle HorizontalAlign="Left" />
                                                            <HeaderStyle HorizontalAlign="Center" Width="120px" />
                                                            <ItemTemplate>
                                                                &nbsp;<asp:Label ID="lblVendorName2" runat="server" Text='<%# Bind("VendorName") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="&nbsp;Category Id" SortExpression="CategoryId">
                                                            <ItemStyle HorizontalAlign="Left" />
                                                            <HeaderStyle HorizontalAlign="Center" Width="70px" />
                                                            <ItemTemplate>
                                                                &nbsp;<asp:Label ID="lblCategoryId2" runat="server" Text='<%# Bind("CategoryId") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="&nbsp;Category Name" SortExpression="CategoryName">
                                                            <ItemStyle HorizontalAlign="Left" />
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <ItemTemplate>
                                                                &nbsp;<asp:Label ID="lblCategoryName" runat="server" Text='<%# Bind("CategoryName") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="&nbsp;Sub Category Id" SortExpression="SubCategoryId">
                                                            <ItemStyle HorizontalAlign="Left" />
                                                            <HeaderStyle HorizontalAlign="Center" Width="70px" />
                                                            <ItemTemplate>
                                                                &nbsp;<asp:Label ID="lblSubCategoryId2" runat="server" Text='<%# Bind("SubCategoryId") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="&nbsp;SubCategory Name" SortExpression="SubCategoryName">
                                                            <ItemStyle HorizontalAlign="Left" />
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <ItemTemplate>
                                                                &nbsp;<asp:Label ID="lblSubCategoryName" runat="server" Text='<%# Bind("SubCategoryName") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="&nbsp;Brand Id" SortExpression="BrandId">
                                                            <ItemStyle HorizontalAlign="Left" />
                                                            <HeaderStyle HorizontalAlign="Center" Width="40px" />
                                                            <ItemTemplate>
                                                                &nbsp;<asp:Label ID="lblBrandId2" runat="server" Text='<%# Bind("BrandId") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="&nbsp;Brand Name" SortExpression="BrandName">
                                                            <ItemStyle HorizontalAlign="Left" />
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <ItemTemplate>
                                                                &nbsp;<asp:Label ID="lblBrandName" runat="server" Text='<%# Bind("BrandName") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <EmptyDataTemplate>
                                                        <asp:Label ID="Label2" runat="server" Text="No Categories"></asp:Label>
                                                    </EmptyDataTemplate>
                                                </asp:GridView>
                                                <asp:SqlDataSource ID="dsVendorCategories" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>' SelectCommand="s3p_EBid_GetAllVendorCategory" SelectCommandType="StoredProcedure" FilterExpression="VendorId = '{0}'" >
                                                    <FilterParameters>
                                                        <asp:ControlParameter ControlID="ddlVendor" DefaultValue=" " Name="VendorId" PropertyName="SelectedValue" />
                                                    </FilterParameters>
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
                                                    Update Vendor Categories</h3>
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
                                                        <asp:LinkButton ID="lnkDownloadCSVfromDB" runat="server" CausesValidation="False"  Width="250px">Download Vendor Categories</asp:LinkButton>
                                                        <asp:LinkButton ID="lnkUpdateDB" runat="server" Width="150px" CausesValidation="False" OnClientClick="return confirm('Are you sure you want to update entire Vendor Categories from CSV file?');">Update from CSV</asp:LinkButton>
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
