<%@ Control Language="C#" AutoEventWireup="true" CodeFile="list_ongoing.ascx.cs" Inherits="usercontrols_list_ongoing" %>
<div class="separator1"></div>
<h3 style="margin:10px 0px;">Ongoing Accreditation</h3>
<form id="Form1" action="" method="post" runat="server">
        <asp:SqlDataSource ID="dsVendorOngoing" runat="server" ConnectionString="<%$ ConnectionStrings:AVAConnectionString %>"
            SelectCommand="SELECT *,DATEDIFF(d, DateSubmittedToDnb , DateAuthenticatedByDnb) as datediff1, DATEDIFF(d, DateAuthenticatedByDnb, approvedbyDnbDate) as datediff2, DATEDIFF(d, approvedbyDnbDate, approvedbyVMOfficerDate) as datediff3, DATEDIFF(d, approvedbyVMOfficerDate, approvedbyVMRecoDate) as datediff4, DATEDIFF(d, approvedbyVMRecoDate, approvedbyFAALogisticsDate) as datediff5, DATEDIFF(d, approvedbyFAALogisticsDate, approvedbyFAAFinanceDate) as datediff6, DATEDIFF(d, DateSubmittedToDnb, approvedbyFAALogisticsDate) as datetotal1, case when approvedbyFAAFinanceDate is not null THEN DATEDIFF(d, DateSubmittedToDnb, approvedbyFAAFinanceDate) when approvedbyFAALogisticsDate is not null THEN DATEDIFF(d, DateSubmittedToDnb, approvedbyFAALogisticsDate) when approvedbyVMRecoDate is not null THEN DATEDIFF(d, DateSubmittedToDnb, approvedbyVMRecoDate) when approvedbyVMOfficerDate is not null THEN DATEDIFF(d, DateSubmittedToDnb, approvedbyVMOfficerDate) when DateAuthenticatedByDnb is not null THEN DATEDIFF(d, DateSubmittedToDnb, DateAuthenticatedByDnb) when approvedbyDnbDate is not null THEN DATEDIFF(d, DateSubmittedToDnb, approvedbyDnbDate) ELSE 0 END as 'datetotal2' FROM tblVendor WHERE Status >= 1 AND Status <> 7 AND Status <> 8 AND Status <> 6  ORDER BY DateCreated DESC" >
	    </asp:SqlDataSource>
<asp:GridView ID="GridView1" runat="server" DataSourceID="dsVendorOngoing" 
    AllowPaging="True" AllowSorting="True" BorderColor="Silver" OnRowCommand="gvBids_RowCommand" RowStyle-Font-Size="Smaller"
    BorderStyle="Dotted" BorderWidth="1px" CellPadding="5" Font-Size="Smaller" ClientIDMode="AutoID" EmptyDataText="No ongoing vendor to display." 
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
        <asp:TemplateField HeaderText="Endorsed to MMD Manager" InsertVisible="False" SortExpression="approvedbyVMOfficerDate" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="10px">
            <HeaderStyle HorizontalAlign="Center" Width="90px" />
            <ItemTemplate>
                &nbsp;<asp:Label ID="Label13" runat="server" Text='<%# Eval("approvedbyVMOfficerDate").ToString()!="" ? "["+Eval("datediff3").ToString() + "]" + String.Format("{0:M/d/yyyy<br><i>&nbsp;&nbsp;HH:mm tt</i>}", Eval("approvedbyVMOfficerDate")) : "" %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <%--<asp:TemplateField HeaderText="Endorsed to PVMD" InsertVisible="False" SortExpression="approvedbyVMRecoDate" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="10px">
            <HeaderStyle HorizontalAlign="Center" Width="90px" />
            <ItemTemplate>
                &nbsp;<asp:Label ID="Label131" runat="server" Text='<%# Eval("approvedbyVMRecoDate").ToString()!="" ? "["+Eval("datediff4").ToString() + "]" + String.Format("{0:M/d/yyyy<br><i>&nbsp;&nbsp;HH:mm tt</i>}", Eval("approvedbyVMRecoDate")) : "" %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>--%>
        <asp:TemplateField HeaderText="Endorsed to MMD VP" InsertVisible="False" SortExpression="approvedbyFAALogisticsDate" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="10px">
            <HeaderStyle HorizontalAlign="Center" Width="90px" />
            <ItemTemplate>
                &nbsp;<asp:Label ID="Label14" runat="server" Text='<%# Eval("approvedbyFAALogisticsDate").ToString()!=""? "["+Eval("datediff5").ToString() + "]" + String.Format("{0:M/d/yyyy<br><i>&nbsp;&nbsp;HH:mm tt</i>}", Eval("approvedbyFAALogisticsDate")) : "" %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <%--<asp:TemplateField HeaderText="Status" InsertVisible="False" SortExpression="approvedbyFAALogisticsDate" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="10px">
            <HeaderStyle HorizontalAlign="Center" Width="90px" />
            <ItemTemplate>
                &nbsp;<asp:Label ID="Label15" runat="server" Text='<%# Eval("Status").ToString()!="8" ? Eval("Status").ToString()=="6"?"Approved: <br>"+ "["+Eval("datediff6").ToString() + "]" + String.Format("{0:M/d/yyyy<br><i>&nbsp;&nbsp;HH:mm tt</i>}", Eval("approvedbyFAAFinanceDate")) : "" : Eval("approvedbyFAAFinanceDate").ToString()!="" ? "Disapproved: <br>"+  "["+Eval("datediff1").ToString() + "]" + String.Format("{0:M/d/yyyy <i>HH:mm tt</i>}", Eval("approvedbyFAAFinanceDate")) : "Disapproved: <br>"+  "["+Eval("datediff5").ToString() + "]" + String.Format("{0:M/d/yyyy <i>HH:mm tt</i>}", Eval("approvedbyFAALogisticsDate")) %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>--%>
        <asp:TemplateField HeaderText="Total" InsertVisible="False" SortExpression="approvedbyFAALogisticsDate" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="10px">
            <HeaderStyle HorizontalAlign="Center" Width="20px" />
            <ItemTemplate>
                &nbsp;<asp:Label ID="Label16" runat="server" Text='<%# Eval("datetotal2").ToString() %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <%--<asp:TemplateField HeaderText="Endorsed to CFO" InsertVisible="False" SortExpression="DateCreated" ItemStyle-HorizontalAlign="Center">
            <HeaderStyle HorizontalAlign="Center" Width="90px" />
            <ItemTemplate>
                &nbsp;<asp:Label ID="Label2" runat="server" Text='<%# Bind("approvedbyFAALogisticsDate") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>--%>
        <asp:TemplateField HeaderText="Action" InsertVisible="False" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="10px">
            <HeaderStyle HorizontalAlign="Center" Width="20px" />
            <ItemTemplate>
                &nbsp;<asp:LinkButton ID="lnkRefNo1" runat="server" Text='View' CommandArgument='<%# Bind("VendorId") %>' CommandName="Details"></asp:LinkButton><%-- | <%# Eval("Status").ToString()!="8" ? Eval("Status").ToString()!="6" ? "":"Approved" : "Disapproved" %>--%>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>
</form>