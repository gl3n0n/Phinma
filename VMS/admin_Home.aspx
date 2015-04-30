<%@ Page Title="" Language="C#" MasterPageFile="~/MasterSubPage.master" AutoEventWireup="true" CodeFile="admin_Home.aspx.cs" Inherits="admin_Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitlePlaceHolder1" runat="Server">
Trans-Asia VMS Accreditation :: Vendor
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content">
    <!--### FORM STYLING ###-->
	<link rel="stylesheet" href="plugins/jqtransformplugin/jqtransform.css" type="text/css" media="all" />
	<link rel="stylesheet" href="plugins/jqtransformplugin/demo.css" type="text/css" media="all" />
	<script type="text/javascript" src="plugins/jqtransformplugin/jquery.jqtransform.js" ></script>
	<script language="javascript">
	    $(function () {
	        //$('form').jqTransform({ imgPath: 'plugins/jqtransformplugin/img/' });
	    });
	</script>
    <!--### FORM STYLING ENDS ###-->
    <script type="text/javascript" src="Scripts/jquery.table.addrow.js" ></script>
<div class="content_logo">
<img src="images/final_logo_w.png"  border="0" />
</div>
<div class="rounded-corners-top" id="menuAVA">
    <ul>
    <li id="current" class="rounded-corners-top-left" onclick="window.location='admin_Home.aspx'"><span class="rounded-corners-top-left">Home</span></li>
    <li onclick="window.location='admin_UsersList.aspx'"><span class="rounded-corners-top-left">List of Users</span></li>
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
<div style="float:left; width:350px; min-height:300px; border-right:#ccc 1px solid; margin:25px 0 0 0px; padding-top:0px;">
  <div style="margin-top:5px;">
    <div style="float:left; margin:5px 20px 0 0;"> 
    <%--<a href="#" style="font-size:16px; font-weight:bold;">Enter Authentication Ticket</a><br /><br />--%>
	<a href="admin_UsersList.aspx" style="font-size:16px; font-weight:bold;">List of Users</a><br /><br />
	<a href="admin_VendorUsersList.aspx" style="font-size:16px; font-weight:bold;">List of Vendor Users</a><br /><br />
	<a href="admin_DeletedUsersList.aspx" style="font-size:16px; font-weight:bold;">Deleted Users</a><br /><br />
	<%--<a href="#" style="font-size:16px; font-weight:bold;">Provide / Edit Report</a>--%>
    
    </div>
  </div>
  <div class="clearfix"></div>
</div>
<div style="float:left; width:400px; margin:25px 0 0 30px; padding-top:0px; font-size:14px;" >
<form action="" method="post" name="formVendorInfo" id="formVendorInfo" runat="server">
  <h3><asp:Label ID="Label1" runat="server" Text="Create a user"></asp:Label></h3><br />
  <asp:label id="errNotification" runat="server"></asp:label> 
  <div style="clear:both; height:4px;"></div>

    <table style="width: 100%;">
        <tr>
            <td style="width: 130px">
  <label for="UserName" style="clear:both; width:110px;">Username*</label></td>
            <td>
    <input name="UserName" type="text" id="UserName" runat="server" /></td>
        </tr>
        <tr>
            <td style="width: 130px">
  <label for="UserPassword" style="clear:both; width:110px;">Password*</label></td>
            <td>
    <input name="UserPassword" type="text" id="UserPassword" runat="server"  /></td>
        </tr>
        <tr>
            <td style="width: 130px; height: 28px;">
  <label for="EmailAdd" style="clear:both; width:110px;">Email*</label></td>
            <td style="height: 28px">
    <input name="EmailAdd" type="text" id="EmailAdd" runat="server"  /></td>
        </tr>
        <tr>
            <td style="width: 130px; height: 28px;">
  <label for="FirstName" style="clear:both; width:110px;">FirstName*</label></td>
            <td style="height: 28px">
    <input name="FirstName" type="text" id="FirstName" runat="server"  /></td>
        </tr>
        <tr>
            <td style="width: 130px; height: 28px;">
  <label for="LastName" style="clear:both; width:110px;">LastName*</label></td>
            <td style="height: 28px">
    <input name="LastName" type="text" id="LastName" runat="server"  /></td>
        </tr>
        <tr>
            <td style="width: 130px; height: 28px;">
  <label for="CompanyName" style="clear:both; width:110px;">CompanyName*</label></td>
            <td style="height: 28px">
    <input name="CompanyName" type="text" id="CompanyName" class="CompanyName" runat="server"  /></td>
        </tr>
        </table>
  <div style="clear:both; height:4px;"></div>
<table style="width: 100%;">
    <tr>
        <td valign="top">
            <label for="UserType" style="clear:both; width:110px;">User Type*</label>
        </td>
        <td>
            <asp:RadioButtonList ID="UserType" runat="server" RepeatDirection="Horizontal" 
      RepeatColumns="2" DataSourceID="dsUserTypes" DataTextField="UserTypeDesc" 
      DataValueField="UserType" CellPadding="1" CellSpacing="1" Width="300px" 
                Font-Size="9px" CausesValidation="True" >
</asp:RadioButtonList>
      <asp:SqlDataSource ID="dsUserTypes" runat="server" ConnectionString="<%$ ConnectionStrings:AVAConnectionString %>"
            SelectCommand="SELECT * FROM rfcUserTypes WHERE UserType != 11 AND UserType<>13 AND UserType<>14 AND UserType<>15 AND UserType<>17 ORDER BY UserType">
	    </asp:SqlDataSource>
        </td>
    </tr>
    </table>
  

  
  <div class="separator1"></div>
  <div style="clear:both; height:2px;"></div>
  <asp:LinkButton ID="createBt1" runat="server" CssClass="bt1"  onclientclick="javascript: __doPostBack('saveUser', ''); return false;" style="float:left"><asp:Label ID="Label2" runat="server" Text="SAVE"></asp:Label></asp:LinkButton>
  
  <asp:LinkButton ID="deleteBt1" runat="server" CssClass="bt1"  onclientclick="javascript: if(confirm('Are you sure to delete this user?')) __doPostBack('deleteUser', ''); return false;" style="float:left"><asp:Label ID="Label3" runat="server" Text="DELETE"></asp:Label></asp:LinkButton>
  <div class="clearfix"></div>
</form><br />
<br />

</div>

<!--BODY CONTENT ENDS-->
<!--##################-->
</div>
</div>
<!-- content ends --> 

</asp:Content>