<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AdminLeftNavList.ascx.cs"
    Inherits="web_usercontrol_admin_AdminLeftNavList" %>
<link type="text/css" href="../../css/style.css" rel="stylesheet" />
<table border="0" cellpadding="0" cellspacing="0" width="100%" id="related">
    <tr>
        <td>
            <asp:HyperLink runat="server" ID="lnkCategories" NavigateUrl="~/admin/categories.aspx">Categories</asp:HyperLink></td>
    </tr>
    <tr>
        <td>
            <asp:HyperLink runat="server" ID="lnkSubCategories" NavigateUrl="~/admin/subcategories.aspx">Sub Categories</asp:HyperLink></td>
    </tr>
    <tr style="display:none">
        <td>
            <asp:HyperLink runat="server" ID="lnkBrands" NavigateUrl="~/admin/brands.aspx">Brands</asp:HyperLink>
        </td>
    </tr>
    <tr>
        <td>
            <asp:HyperLink runat="server" ID="lnkVendors" NavigateUrl="~/admin/vendorcategories.aspx">Vendor Categories</asp:HyperLink>
        </td>
    </tr>
    <tr style="display:none">
        <td>
            <asp:HyperLink runat="server" ID="lnkServices" NavigateUrl="~/admin/services.aspx">Services</asp:HyperLink>
        </td>
    </tr>
    <tr>
        <td>
            <asp:HyperLink runat="server" ID="lnkItems" NavigateUrl="~/admin/items.aspx">Items</asp:HyperLink>
        </td>
    </tr>
    <tr>
        <td>
            <asp:HyperLink runat="server" ID="lnkLocations" NavigateUrl="~/admin/locations.aspx">Locations</asp:HyperLink>
        </td>
    </tr>
    <tr>
        <td>
            <asp:HyperLink runat="server" ID="lnkCurrencies" NavigateUrl="~/admin/currencies.aspx">Currencies</asp:HyperLink>
        </td>
    </tr>
    <tr>
		<td>			
			<asp:HyperLink ID="lnkRegisteredProduct" runat="server" NavigateUrl="~/admin/product.aspx">Registered Products</asp:HyperLink>
		</td>
	</tr>
	<tr>
		<td>
			<asp:LinkButton runat="server" ID="lnkRegisterProduct" OnClick="lnkRegisterProduct_Click" CausesValidation="False">Register Product</asp:LinkButton>
		</td>
	</tr>
	<tr>
		<td>
			<asp:LinkButton runat="server" ID="lnkDeletedProducts" CausesValidation="False" OnClick="lnkDeletedProducts_Click">Deleted Products</asp:LinkButton>
		</td>
	</tr>
    <!-- <tr>
        <td>
            <asp:HyperLink runat="server" ID="lnkCategoriesPerBOC" NavigateUrl="~/admin/categoriesperboc.aspx">Categories per BOC Procurement</asp:HyperLink></td>
    </tr> -->
</table>
