<%@ Page Title="" Language="C#" MasterPageFile="~/MasterSubPage.master" AutoEventWireup="true" CodeFile="vmofficer_VendorSIR_List.aspx.cs" Inherits="vmofficer_VendorSIR_List" %>
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
<h3 style="margin:10px 0px;">New Supplier Information Report</h3>
<form action="" method="post" runat="server">
<asp:SqlDataSource ID="dsVendorApplicants" runat="server" ConnectionString="<%$ ConnectionStrings:AVAConnectionString %>"
            SelectCommand="SELECT *,DATEDIFF(d, DateSubmittedToDnb , DateAuthenticatedByDnb) as datediff1, DATEDIFF(d, DateAuthenticatedByDnb, approvedbyDnbDate) as datediff2, DATEDIFF(d, approvedbyDnbDate, approvedbyVMOfficerDate) as datediff3, DATEDIFF(d, approvedbyVMOfficerDate, approvedbyVMRecoDate) as datediff4, DATEDIFF(d, approvedbyVMRecoDate, approvedbyFAALogisticsDate) as datediff5, DATEDIFF(d, approvedbyFAALogisticsDate, approvedbyFAAFinanceDate) as datediff6, DATEDIFF(d, DateSubmittedToDnb, approvedbyFAALogisticsDate) as datetotal1, DATEDIFF(d, DateSubmittedToDnb, approvedbyFAAFinanceDate) as datetotal2 FROM tblVendor WHERE Status = 2 ORDER BY approvedbyDnbDate DESC" >
            <SelectParameters>
                <asp:SessionParameter Name="ProcurementId" SessionField="UserId" Type="Int32" />
            </SelectParameters>
	    </asp:SqlDataSource>
<asp:GridView ID="GridView1" runat="server" DataSourceID="dsVendorApplicants" 
    AllowPaging="True" AllowSorting="True" BorderColor="Silver" OnRowCommand="gvBids_RowCommand"
    BorderStyle="Dotted" BorderWidth="1px" CellPadding="5" ClientIDMode="AutoID" EmptyDataText="No new supplier information report to display." 
    Width="100%" AutoGenerateColumns="False" PageSize="15">
    <AlternatingRowStyle BackColor="#EEEEEE" />
    <Columns>
        <asp:TemplateField HeaderText="Company Name" InsertVisible="False" SortExpression="CompanyName" ItemStyle-HorizontalAlign="Center">
            <HeaderStyle HorizontalAlign="Center" Width="90px" />
            <ItemTemplate>
                &nbsp;<%--<asp:Label ID="lnkRefNo" runat="server" Text='<%# Bind("CompanyName") %>' ></asp:Label>--%><%--&nbsp;<asp:Label ID="Label0" runat="server" Text='<%# Bind("CompanyName") %>'></asp:Label>--%><a href="<%# "vendor_Home.aspx?VendorId=" + Eval("VendorId") %>" class="iframe" title='<%# Eval("CompanyName") %>' ><%# Eval("CompanyName") %></a>  
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Submitted to Commodity Buyer" InsertVisible="False" SortExpression="DateSubmittedToDnb" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="10px">
            <HeaderStyle HorizontalAlign="Center" Width="90px" />
            <ItemTemplate>
                &nbsp;<asp:Label ID="Label1" runat="server" Text='<%# String.Format("{0:M/d/yyyy<br><i>&nbsp;&nbsp;HH:mm tt</i>}", Eval("DateSubmittedToDnb")) %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Authenticated" InsertVisible="False" SortExpression="DateAuthenticatedByDnb" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="10px">
            <HeaderStyle HorizontalAlign="Center" Width="90px" />
            <ItemTemplate>
                &nbsp;<asp:Label ID="Label2" runat="server" Text='<%# Eval("DateAuthenticatedByDnb").ToString()!="" ? "["+Eval("datediff1").ToString() + "]" + String.Format("{0:M/d/yyyy<br><i>&nbsp;&nbsp;HH:mm tt</i>}", Eval("DateAuthenticatedByDnb")) : "" %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Submitted to Vendor Admin" InsertVisible="False" SortExpression="approvedbyDnbDate" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="10px">
            <HeaderStyle HorizontalAlign="Center" Width="90px" />
            <ItemTemplate>
                &nbsp;<asp:Label ID="Label12" runat="server" Text='<%# Eval("approvedbyDnbDate").ToString()!="" ? "["+Eval("datediff2").ToString() + "]" +  String.Format("{0:M/d/yyyy<br><i>&nbsp;&nbsp;HH:mm tt</i>}", Eval("approvedbyDnbDate")) : "" %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <%--<asp:TemplateField HeaderText="Endorsed to VM Head" InsertVisible="False" SortExpression="approvedbyVMOfficerDate" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="10px">
            <HeaderStyle HorizontalAlign="Center" Width="90px" />
            <ItemTemplate>
                &nbsp;<asp:Label ID="Label13" runat="server" Text='<%# Eval("approvedbyVMOfficerDate").ToString()!="" ? "["+Eval("datediff3").ToString() + "]" + String.Format("{0:M/d/yyyy<br><i>&nbsp;&nbsp;HH:mm tt</i>}", Eval("approvedbyVMOfficerDate")) : "" %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>--%>
        <%--<asp:TemplateField HeaderText="Endorsed to VP" InsertVisible="False" SortExpression="approvedbyVMRecoDate" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="10px">
            <HeaderStyle HorizontalAlign="Center" Width="90px" />
            <ItemTemplate>
                &nbsp;<asp:Label ID="Label131" runat="server" Text='<%# Eval("approvedbyVMRecoDate").ToString()!="" ? "["+Eval("datediff4").ToString() + "]" + String.Format("{0:M/d/yyyy<br><i>&nbsp;&nbsp;HH:mm tt</i>}", Eval("approvedbyVMRecoDate")) : "" %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Endorsed to CFO" InsertVisible="False" SortExpression="approvedbyFAALogisticsDate" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="10px">
            <HeaderStyle HorizontalAlign="Center" Width="90px" />
            <ItemTemplate>
                &nbsp;<asp:Label ID="Label14" runat="server" Text='<%# Eval("approvedbyFAAFinanceDate").ToString()!=""? "["+Eval("datediff5").ToString() + "]" + String.Format("{0:M/d/yyyy<br><i>&nbsp;&nbsp;HH:mm tt</i>}", Eval("approvedbyFAAFinanceDate")) : "" %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Status" InsertVisible="False" SortExpression="approvedbyFAALogisticsDate" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="10px">
            <HeaderStyle HorizontalAlign="Center" Width="90px" />
            <ItemTemplate>
                &nbsp;<asp:Label ID="Label15" runat="server" Text='<%# Eval("Status").ToString()!="8" ? Eval("Status").ToString()=="6"?"Approved: <br>"+ "["+Eval("datediff6").ToString() + "]" + String.Format("{0:M/d/yyyy<br><i>&nbsp;&nbsp;HH:mm tt</i>}", Eval("approvedbyFAAFinanceDate")) : "" : Eval("approvedbyFAAFinanceDate").ToString()!="" ? "Disapproved: <br>"+  "["+Eval("datediff1").ToString() + "]" + String.Format("{0:M/d/yyyy <i>HH:mm tt</i>}", Eval("approvedbyFAAFinanceDate")) : "Disapproved: <br>"+  "["+Eval("datediff5").ToString() + "]" + String.Format("{0:M/d/yyyy <i>HH:mm tt</i>}", Eval("approvedbyFAALogisticsDate")) %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Total" InsertVisible="False" SortExpression="approvedbyFAALogisticsDate" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="10px">
            <HeaderStyle HorizontalAlign="Center" Width="20px" />
            <ItemTemplate>
                &nbsp;<asp:Label ID="Label16" runat="server" Text='<%# (Eval("Status").ToString()=="8" || Eval("Status").ToString()=="6") && Eval("approvedbyFAAFinanceDate").ToString()!="" ? Eval("datetotal2").ToString() :  Eval("datetotal1").ToString() %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>--%>
        <%--<asp:TemplateField HeaderText="Endorsed to CFO" InsertVisible="False" SortExpression="DateCreated" ItemStyle-HorizontalAlign="Center">
            <HeaderStyle HorizontalAlign="Center" Width="90px" />
            <ItemTemplate>
                &nbsp;<asp:Label ID="Label2" runat="server" Text='<%# Bind("approvedbyFAALogisticsDate") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>--%>
        <asp:TemplateField HeaderText="Action" InsertVisible="False" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="10px">
            <HeaderStyle HorizontalAlign="Center" Width="10px" />
            <ItemTemplate>
                &nbsp;<asp:LinkButton ID="lnkRefNo1" runat="server" Text='View' CommandArgument='<%# Bind("VendorId") %>' CommandName="Details"></asp:LinkButton><%-- | <%# Eval("Status").ToString()!="8" ? Eval("Status").ToString()!="6" ? "":"Approved" : "Disapproved" %>--%>
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