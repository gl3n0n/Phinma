<%@ Page Title="" Language="C#" MasterPageFile="~/MasterSubPage.master" AutoEventWireup="true" CodeFile="dnb_Home.aspx.cs" Inherits="dnb_Home" %>
<%@ Register TagPrefix="Ava" TagName="Tabsnav" Src="usercontrols/tabs.ascx" %>
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
<div class="content_logo">
<img src="images/final_logo_w.png"  border="0" />
</div>
<div class="rounded-corners-top" id="menuAVA">
    <Ava:Tabsnav ID="Tabsnav1" runat="server" />
</div>
<div style="background:#FFF; min-height:445px; padding:10px;" class="rounded-corners-bottom2 menu">
<!--##################-->
<!--BODY CONTENT STARTS-->
<%@ Register TagPrefix="Ava" TagName="TopNav" Src="usercontrols/TopNav_dnb.ascx" %>
<ava:topnav ID="TopNav1" runat="server" />
<div style="float:left; width:450px; min-height:300px; border-right:#ccc 1px solid; margin:25px 0 0 0px; padding-top:0px;">
  <div style="margin-top:5px;">
    <div style="float:left; margin:5px 20px 0 0;"> 
    <%--<a href="#" style="font-size:16px; font-weight:bold;">Enter Authentication Ticket</a><br /><br />--%>
	<a href="dnb_VendorListAuthentication.aspx" style="font-size:16px; font-weight:bold;">Vendors for Authentication
        (<asp:Label ID="countAuthentication" runat="server" Text=""></asp:Label>)</a><br /><br />
	<a href="dnb_VendorList.aspx" style="font-size:16px; font-weight:bold;">Authenticated Vendors for Evaluation
        (<asp:Label ID="countVendors" runat="server" Text=""></asp:Label>)</a><br /><br />
	<%--<a href="#" style="font-size:16px; font-weight:bold;">Vendors for Clarifications (0)</a><br /><br />--%>
	<a href="dnb_VendorListApproved.aspx" style="font-size:16px; font-weight:bold;">Endorsed Vendors (<asp:Label ID="countApproved" runat="server" Text=""></asp:Label>)</a><br /><br />
	<a href="#" style="font-size:16px; font-weight:bold; display:none">Provide / Edit Report</a>
    
    </div>
  </div>
  <div class="clearfix"></div>
</div>
<div style="float:left; width:400px; margin:25px 0 0 30px; padding-top:0px; font-size:14px;">
<form  name="formVendorInfo" id="formVendorInfo" runat="server">
  <%--<h3>Search Authentication Ticket</h3><br />
    <asp:Label ID="errNotificationAuthTicket" runat="server" Text=""></asp:Label>
    <input name="AuthenticationTicket" type="text" id="AuthenticationTicket" size="28"  runat="server"/>
  <br /><br /><br />
  <div class="clearfix"></div>
  <asp:LinkButton ID="createBt1" runat="server" CssClass="bt1"  onclientclick="javascript: __doPostBack('AuthTicket', ''); return false;" style="float:left"><span>Search</span></asp:LinkButton>
  <div class="clearfix"></div>
<br />
<br />--%>


<%--  <h3>Enter Vendor Name</h3> <br />
  <asp:Label ID="errNotificationCompanyName" runat="server" Text="" ></asp:Label>
    <input name="CompanyName" type="text" id="CompanyName" size="28" runat="server" />
  <br /><br /><br />
  <div class="clearfix"></div>
  <asp:LinkButton ID="createBt2" runat="server" CssClass="bt1"  onclientclick="javascript: __doPostBack('CompanyName', ''); return false;" style="float:left"><span>Authenticate </span></asp:LinkButton>--%>
  <div class="clearfix"></div>
</form>
</div>

<!--BODY CONTENT ENDS-->
<!--##################-->
</div>
</div>
</div><!-- content ends --> 

</asp:Content>