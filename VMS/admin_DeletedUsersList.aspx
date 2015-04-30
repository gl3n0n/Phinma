<%@ Page Title="" Language="C#" MasterPageFile="~/MasterSubPage.master" AutoEventWireup="true" CodeFile="admin_DeletedUsersList.aspx.cs" Inherits="admin_DeletedUsersList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitlePlaceHolder1" runat="Server">
Trans-Asia VMS Accreditation :: Vendor
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content">
<div class="content_logo">
<img src="images/final_logo_w.png"  border="0" />
</div>
<div class="rounded-corners-top" id="menuAVA">
    <ul>
    <li class="rounded-corners-top-left" onclick="window.location='admin_Home.aspx'"><span class="rounded-corners-top-left">Home</span></li>
    <li id="current" onclick="window.location='admin_UsersList.aspx'"><span>List of Users</span></li>
    <li onclick="window.location='admin_VendorUsersList.aspx'"><span>Vendor Users</span></li>
    <%--<li><span>VM Assessment</span></li>
    <li><span>Legal</span></li>
    <li><span>VM Technical Review</span></li>
    <li><span>VM Issue Management</span></li>
    <li><span style="padding:0px 15px;">VM Recommending<br />Approval</span></li>
    <li><span style="padding:0px 15px;">Final Approving <br />Authority
Logistics</span></li>
    <li style="border-right:none;"><span style="padding:0px 15px;">Final Approving <br />AuthorityFinance</span></li>--%>
    </ul>
</div>
<div style="background:#FFF; min-height:445px; padding:10px;" class="rounded-corners-bottom2 menu">
<!--##################-->
<!--BODY CONTENT STARTS-->
<div class="topnav"><a href="admin_Home.aspx?UserId=0">Create a User</a>&nbsp;&nbsp;&nbsp;|&nbsp;&nbsp;&nbsp;<a href="admin_UsersList.aspx">List of Users</a>&nbsp;&nbsp;&nbsp;|&nbsp;&nbsp;&nbsp;<a href="admin_VendorUsersList.aspx">Vendor Users</a></div>

<!--Business activities STARTS-->
<div class="separator1"></div>
<h3 style="margin:10px 0px;">Users List</h3>
<form action="" method="post" runat="server">
<asp:SqlDataSource ID="dsUsers" runat="server" ConnectionString="<%$ ConnectionStrings:AVAConnectionString %>"
            SelectCommand="SELECT t1.UserId, t1.UserName, t1.EmailAdd, (t1.FirstName + ' ' + t1.LastName) AS FullName, t1.CompanyName, t1.DateCreated, t3.UserTypeDesc FROM tblUsers t1, tblUserTypes t2, rfcUserTypes t3 WHERE t1.Status = 0 AND t2.UserId = t1.UserId AND t3.UserType = t2.UserType ORDER BY t1.DateCreated DESC" >
	    </asp:SqlDataSource>
<asp:GridView ID="GridView1" runat="server" DataSourceID="dsUsers" 
    AllowPaging="True" AllowSorting="True" BorderColor="Silver" OnRowCommand="gvBids_RowCommand"
    BorderStyle="Dotted" BorderWidth="1px" CellPadding="5" ClientIDMode="AutoID" 
    Width="100%" AutoGenerateColumns="False" PageSize="15">
    <Columns>
        <asp:TemplateField HeaderText="UserName" InsertVisible="False" SortExpression="UserName">
            <HeaderStyle HorizontalAlign="Center" Width="70px" />
            <ItemTemplate>
                &nbsp;<asp:Label ID="Label0" runat="server" Text='<%# Bind("UserName") %>' ></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Email Address" InsertVisible="False" SortExpression="EmailAdd">
            <HeaderStyle HorizontalAlign="Center" Width="90px" />
            <ItemTemplate>
                &nbsp;<asp:Label ID="Label1" runat="server" Text='<%# Bind("EmailAdd") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Name" InsertVisible="False" SortExpression="FullName">
            <HeaderStyle HorizontalAlign="Center" Width="80px" />
            <ItemTemplate>
                &nbsp;<asp:Label ID="Label2" runat="server" Text='<%# Bind("FullName") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        
        <asp:TemplateField HeaderText="User Type" InsertVisible="False" SortExpression="UserTypeDesc">
            <HeaderStyle HorizontalAlign="Center" Width="60px" />
            <ItemTemplate>
                &nbsp;<asp:Label ID="Label4" runat="server" Text='<%# Bind("UserTypeDesc") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Date Created" InsertVisible="False" SortExpression="DateCreated">
            <HeaderStyle HorizontalAlign="Center" Width="90px"/>
            <ItemTemplate>
                &nbsp;<asp:Label ID="Label3" runat="server" Text='<%# Bind("DateCreated") %>'  Font-Size="10px" ></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Action" InsertVisible="False" ItemStyle-HorizontalAlign="Center">
            <HeaderStyle HorizontalAlign="Center" Width="40px" />
            <ItemTemplate >
                <asp:LinkButton ID="lnkRefNo1" runat="server" Text='Recover' CommandArgument='<%# Bind("UserId") %>' CommandName="Details"></asp:LinkButton>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>
</form>
<br />
<!--BODY CONTENT ENDS-->
<!--##################OnClientClick='javascript: confirm("Are you sure to delete this user?")'-->
</div>
</div>
</div><!-- content ends --> 

</asp:Content>