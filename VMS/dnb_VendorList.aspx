<%@ Page Title="" Language="C#" MasterPageFile="~/MasterSubPage.master" AutoEventWireup="true" CodeFile="dnb_VendorList.aspx.cs" Inherits="dnb_VendorList" %>
<%@ Register TagPrefix="Ava" TagName="Tabsnav" Src="usercontrols/tabs.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitlePlaceHolder1" runat="Server">
Trans-Asia VMS Accreditation :: Vendor
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content">
<div class="content_logo">
<img src="images/final_logo_w.png"  border="0" />
</div>
<div class="rounded-corners-top" id="menuAVA">
<ava:tabsnav ID="Tabsnav1" runat="server" />
</div>
<div style="background:#FFF; min-height:445px; padding:10px;" class="rounded-corners-bottom2 menu">
<!--##################-->
<!--BODY CONTENT STARTS-->
<%@ Register TagPrefix="Ava" TagName="TopNav" Src="usercontrols/TopNav_dnb.ascx" %>
<ava:topnav ID="TopNav1" runat="server" />

<!--Business activities STARTS-->
<div class="separator1"></div>
<h3 style="margin:10px 0px;">Vendors for Evaluation</h3>
<form action="" method="post" runat="server">
<asp:SqlDataSource ID="dsVendorAuthenticated" runat="server" ConnectionString="<%$ ConnectionStrings:AVAConnectionString %>"
            SelectCommand="SELECT t1.VendorId, t1.CompanyName, t1.AuthenticationTicket, t1.DateCreated FROM tblVendor t1 WHERE (t1.AuthenticationTicket IS NOT NULL OR t1.AuthenticationTicket!='') AND t1.IsAuthenticated = 1 AND t1.Status = 1 AND t1.EndorsedTo=@EndorsedTo ORDER BY t1.DateCreated DESC" >
    <SelectParameters>
        <asp:SessionParameter Name="EndorsedTo" Type="Int32" SessionField="UserId" />
    </SelectParameters>
	    </asp:SqlDataSource>
<asp:GridView ID="GridView1" runat="server" DataSourceID="dsVendorAuthenticated" 
    AllowPaging="True" AllowSorting="True" BorderColor="Silver" OnRowCommand="gvVendors_RowCommand"
    BorderStyle="Dotted" BorderWidth="1px" CellPadding="5" ClientIDMode="AutoID" EmptyDataText="No authenticated vendors to display." 
    Width="100%" AutoGenerateColumns="False" PageSize="15">
    <AlternatingRowStyle BackColor="#EEEEEE" />
    <Columns>
        <asp:TemplateField HeaderText="Company Name" InsertVisible="False" SortExpression="CompanyName" ItemStyle-HorizontalAlign="Center">
            <HeaderStyle HorizontalAlign="Center" Width="90px" />
            <ItemTemplate>
                &nbsp;<%--<asp:LinkButton ID="lnkRefNo" runat="server" Text='<%# Bind("CompanyName") %>' ></asp:LinkButton>--%><%--<asp:HyperLink ID="lnkRefNo" runat="server" NavigateUrl='<%# "~/view_vendor_01_vendorInfo.aspx?VendorId=" + Eval("VendorId") %>' Target="_blank"><%# Eval("CompanyName") %></asp:HyperLink>--%><%--<a href="" onclick="window.open('<%# "view_vendor_01_vendorInfo.aspx?VendorId=" + Eval("VendorId") %>', 'window name', 'window settings')" ><%# Eval("CompanyName") %></a>--%><a href="<%# "vendor_Home.aspx?VendorId=" + Eval("VendorId") %>" class="iframe" title='<%# Eval("CompanyName") %>' ><%# Eval("CompanyName") %></a><%--&nbsp;<asp:Label ID="Label0" runat="server" Text='<%# Bind("CompanyName") %>'></asp:Label>--%>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Authentication Ticket" InsertVisible="False" SortExpression="AuthenticationTicket" ItemStyle-HorizontalAlign="Center">
            <HeaderStyle HorizontalAlign="Center" Width="90px" />
            <ItemTemplate>
                &nbsp;<asp:Label ID="Label1" runat="server" Text='<%# Bind("AuthenticationTicket") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Date Created" InsertVisible="False" SortExpression="DateCreated" ItemStyle-HorizontalAlign="Center">
            <HeaderStyle HorizontalAlign="Center" Width="90px" />
            <ItemTemplate>
                &nbsp;<asp:Label ID="Label2" runat="server" Text='<%# Bind("DateCreated") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Action" InsertVisible="False" ItemStyle-HorizontalAlign="Center">
            <HeaderStyle HorizontalAlign="Center" Width="90px" />
            <ItemTemplate>
                &nbsp;<asp:LinkButton ID="lnkRefNo1" runat="server" Text='Create / Edit Report' CommandArgument='<%# Bind("VendorId") %>' CommandName="Details"></asp:LinkButton>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>
</form>
<br />
<!--BODY CONTENT ENDS-->
<!--##################-->
</div>
</div>
</div><!-- content ends --> 

</asp:Content>