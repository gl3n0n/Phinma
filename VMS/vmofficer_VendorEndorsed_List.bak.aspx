<%@ Page Title="" Language="C#" MasterPageFile="~/MasterSubPage.master" AutoEventWireup="true" CodeFile="vmofficer_VendorEndorsed_List.bak.aspx.cs" Inherits="vmofficer_VendorEndorsed_List" %>
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
<%@ Register TagPrefix="Ava" TagName="TopNav" Src="usercontrols/TopNav_vmofficer.ascx" %>
<ava:topnav ID="TopNav1" runat="server" />

<!--Business activities STARTS-->
<div class="separator1"></div>
<h3 style="margin:10px 0px;">Approved Applicants</h3>
<form action="" method="post" runat="server">
<asp:SqlDataSource ID="dsVendorApplicants" runat="server" ConnectionString="<%$ ConnectionStrings:AVAConnectionString %>"
            SelectCommand="SELECT t1.VendorId, t1.CompanyName, t1.approvedbyDnbDate, t1.DateCreated FROM tblVendor t1 WHERE t1.Status = 2 ORDER BY t1.DateCreated DESC" >
            <SelectParameters>
                <asp:SessionParameter Name="ProcurementId" SessionField="UserId" Type="Int32" />
            </SelectParameters>
	    </asp:SqlDataSource>
<asp:GridView ID="GridView1" runat="server" DataSourceID="dsVendorApplicants" 
    AllowPaging="True" AllowSorting="True" BorderColor="Silver" OnRowCommand="gvBids_RowCommand"
    BorderStyle="Dotted" BorderWidth="1px" CellPadding="5" ClientIDMode="AutoID" 
    Width="100%" AutoGenerateColumns="False" PageSize="15">
    <Columns>
        <asp:TemplateField HeaderText="Company Name" InsertVisible="False" SortExpression="CompanyName" ItemStyle-HorizontalAlign="Center">
            <HeaderStyle HorizontalAlign="Center" Width="90px" />
            <ItemTemplate>
                &nbsp;<asp:LinkButton ID="lnkRefNo" runat="server" Text='<%# Bind("CompanyName") %>' CommandArgument='<%# Bind("VendorId") %>' CommandName="Details"></asp:LinkButton>
                <%--&nbsp;<asp:Label ID="Label0" runat="server" Text='<%# Bind("CompanyName") %>'></asp:Label>--%>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Date Endorsed" InsertVisible="False" SortExpression="approvedbyDnbDate" ItemStyle-HorizontalAlign="Center">
            <HeaderStyle HorizontalAlign="Center" Width="90px" />
            <ItemTemplate>
                &nbsp;<asp:Label ID="Label1" runat="server" Text='<%# Bind("approvedbyDnbDate") %>'></asp:Label>
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
                &nbsp;<asp:LinkButton ID="lnkRefNo1" runat="server" Text='View' CommandArgument='<%# Bind("VendorId") %>' CommandName="Details"></asp:LinkButton>
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