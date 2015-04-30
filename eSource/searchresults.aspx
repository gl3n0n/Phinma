<%@ Page Language="C#" AutoEventWireup="true" CodeFile="searchresults.aspx.cs" Inherits="SearchResults" %>

<%@ Register TagPrefix="EBid" TagName="Footer" Src="~/web/usercontrol/Footer.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml" style="height: 100%;">
<head>
    <title id="PageTitle" runat="server"></title>
    <meta http-equiv="Content-Language" content="en-us" />
    <meta http-equiv="Content-Type" content="text/html; charset=windows-1252" />
    <link rel="stylesheet" type="text/css" href='<%= "../themes/"+Session["configTheme"]+"/css/style.css" %>' />

    <script type="text/javascript">
        <!--
        function RedirectToItemDetails(url, refNo, isAuction) 
        {
          if(isAuction != 1)
          {
            window.opener.location.href = url + '?brn=' + refNo;
          }
          else
            window.opener.location.href = url + '?arn=' + refNo;
         
          window.close();
        }
        function RedirectToSupplierDetails(url, vendorId) 
        {
          window.opener.location.href = url + '?VendorID=' + vendorId;
             
          window.close();
        }
        -->
    </script>

</head>
<body style="height: 100%;">
    <form id="form1" runat="server">
        <div align="left">
            &nbsp;<img border="0" src="../../clients/<%= HttpContext.Current.Session["clientid"] %>/images/logo.jpg" width="151" height="63">
        </div>
        <div align="left" id="d3Search" runat="server">
            <h3>
                &nbsp;&nbsp;&nbsp;&nbsp; &nbsp; &nbsp; &nbsp;Search Results</h3>
        </div>
        <p>
            <asp:Label ID="lblIsEmpty" runat="server" Visible="false" /></p>
        <asp:GridView ID="gvSearchResultForItems" AllowPaging="true" PageSize="15" AutoGenerateColumns="false" runat="server" CssClass="pageDetails" Visible="false" OnPageIndexChanging="gridView_PageIndexChanging" OnRowDataBound="gv_RowDataBound">
            <HeaderStyle CssClass="pageDetails_th" ForeColor="#FFFFFF" />
            <AlternatingRowStyle CssClass="evenCells" />
            <Columns>
                <asp:TemplateField ItemStyle-Width="30%">
                    <ItemTemplate>
                        <asp:LinkButton ID="lblRefNo" runat="server" Text='<%# Bind("RefNo") %>' CommandArgument='<%# Bind("RefNo") %>'></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Label ID="lblItems" runat="server" Text='<%# Bind("ItemDesc") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Status">
                    <ItemTemplate>
                        <asp:Label ID="lblStatusID" runat="server" Text='<%# Bind("Status") %>' Visible="false"></asp:Label>
                        <asp:Label ID="lblStatus" runat="server" Text=""></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <asp:GridView ID="gvSuppliers" AllowPaging="true" PageSize="15" AutoGenerateColumns="false" runat="server" CssClass="pageDetails" Visible="false" OnPageIndexChanging="gridView_PageIndexChanging" OnRowDataBound="gv_RowDataBound">
            <HeaderStyle CssClass="pageDetails_th" ForeColor="#FFFFFF" />
            <AlternatingRowStyle CssClass="evenCells" />
            <Columns>
                <asp:TemplateField HeaderText="Suppliers">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbVendorName" runat="server" Text='<%# Bind("VendorName") %>'></asp:LinkButton>
                        <asp:Label ID="lblVendorID" runat="server" Text='<%# Bind("VendorId") %>' Visible="false"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Blacklist Status">
                    <ItemTemplate>
                        <asp:Label ID="lblStatus" runat="server" Text='<%# Bind("Status") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <asp:GridView ID="gvProducts" AllowPaging="true" PageSize="15" AutoGenerateColumns="false" runat="server" CssClass="pageDetails" Visible="false" OnPageIndexChanging="gridView_PageIndexChanging">
            <HeaderStyle CssClass="pageDetails_th" ForeColor="#FFFFFF" />
            <AlternatingRowStyle CssClass="evenCells" />
            <Columns>
                <asp:TemplateField HeaderText="SKU Number">
                    <ItemTemplate>
                        <asp:Label ID="lbSku" runat="server" Text='<%# Bind("SKU") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Item Name">
                    <ItemTemplate>
                        <asp:Label ID="lbItemName" runat="server" Text='<%# Bind("ItemName") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Description">
                    <ItemTemplate>
                        <asp:Label ID="lbDescription" runat="server" Text='<%# Bind("ProductDescription") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Sub-Category">
                    <ItemTemplate>
                        <asp:Label ID="lbSubCat" runat="server" Text='<%# Bind("SubCategoryName") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Category">
                    <ItemTemplate>
                        <asp:Label ID="lbCategory" runat="server" Text='<%# Bind("CategoryName") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </form>
</body>
</html>
