<%@ Page Title="" Language="C#" MasterPageFile="~/MasterSubPage.master" AutoEventWireup="true" CodeFile="vmofficer_Home.aspx.cs" Inherits="vmofficer_Home" %>
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

<div style="float:left; width:450px; min-height:300px; border-right:#ccc 1px solid; margin:25px 0 0 0px; padding-top:0px;">
  <div style="margin-top:5px;">
    <div style="float:left; margin:5px 20px 0 0;"> 
    <%--<a href="#" style="font-size:16px; font-weight:bold;">Enter Authentication Ticket</a><br /><br />--%>
	<a href="vmofficer_NewApplicants_List.aspx" style="font-size:16px; font-weight:bold;">New Applicants (<asp:Label ID="countVendors" runat="server" Text=""></asp:Label>)</a><br /><br />
	<a href="vmofficer_VendorApproved_List.aspx" style="font-size:16px; font-weight:bold;">Approved Applicants (<asp:Label ID="countApproved" runat="server" Text=""></asp:Label>)</a><br /><br />
	<a href="vmofficer_VendorRejected_List.aspx" style="font-size:16px; font-weight:bold;">Rejected Applicants (<asp:Label ID="countRejected" runat="server" Text=""></asp:Label>)</a><br /><br /><hr /><br />
	<a href="vmofficer_VendorSIR_List.aspx" style="font-size:16px; font-weight:bold;">New Supplier Information Report (<asp:Label ID="vmofficer_VendorSIR_List_Count" runat="server" Text="0"></asp:Label>)</a><br /><br />
        
	<a href="vmofficer_VendorEndorsed_List.aspx" style="font-size:16px; font-weight:bold;">Endorsed Report for Recommendation (<asp:Label ID="vmofficer_VendorEndorsed_List_Count" runat="server" Text="0"></asp:Label>)</a><br /><br />

    
    </div>
  </div>
  <div class="clearfix"></div>
</div>
<div style="float:left; width:400px; margin:25px 0 0 30px; padding-top:0px; font-size:14px;">
    <div style="margin-top:5px">
	<a href="vmofficer_VendorOngoing_List.aspx" style="font-size:16px; font-weight:bold;">Ongoing Accreditation  (<asp:Label ID="vmofficer_VendorOngoing_List_Count" runat="server" Text="0"></asp:Label>)</a><br /><br />
	<a href="vmofficer_VendorAcrredited_List.aspx" style="font-size:16px; font-weight:bold;">Approved Vendors  (<asp:Label ID="vmofficer_VendorAcrredited_List_Count" runat="server" Text="0"></asp:Label>)</a><br /><br />
	<a href="vmofficer_VendorDisapproved_List.aspx" style="font-size:16px; font-weight:bold;">Disapproved Vendors  (<asp:Label ID="vmofficer_VendorDisapproved_List_Count" runat="server" Text="0"></asp:Label>)</a><br /><br />
        
	<a href="vmofficer_VendorForNotification_List_Sent.aspx" style="font-size:16px; font-weight:bold;">Exported Vendor Information (<asp:Label ID="vmofficer_VendorForNotification_List_Sent_Count" runat="server" Text=""></asp:Label>)</a><br />
	<a href="vmofficer_VendorForNotification_List.aspx" style="font-size:16px; font-weight:bold;">Generation of Vendor Import file (<asp:Label ID="vmofficer_VendorForNotification_List_Count" runat="server" Text=""></asp:Label>)</a><br /><span style="font-size:12px;color:#969696">Send vendor accreditation certificate, <br />Export vendor login to eSource & <br />Generate import file to ACCPAC</span><br /><br />
	<a href="#" style="font-size:16px; font-weight:bold; display:none;">For Accreditation Renewal  (<asp:Label ID="Label2" runat="server" Text="0"></asp:Label>)</a><br /><br />
	<a href="#" style="font-size:16px; font-weight:bold; display:none;">Provide / Edit Report</a>
        </div>
<%--<form action="" method="post" name="formVendorInfo" id="formVendorInfo">
  <h3>Enter Authentication Ticket</h3><br />
    <input name="textfield" type="text" id="textfield" size="28" />
  <br /><br />
  <div class="clearfix"></div>
  <a href="dnb2_listofvendors.html" class="bt1" style="margin-right:110px; float:left;"><span>Submit</span></a>
  <div class="clearfix"></div>
</form><br />
<br />

<form action="" method="post" name="formVendorInfo" id="formVendorInfo">
  <h3>Enter Vendor Name</h3>
  <br />
    <input name="textfield" type="text" id="textfield" size="28" />
  <br /><br />
  <div class="clearfix"></div>
  <a href="dnb2_listofvendors.html" class="bt1" style="margin-right:110px; float:left;"><span>Submit</span></a>
  <div class="clearfix"></div>
</form>--%>
</div>

<!--BODY CONTENT ENDS-->
<!--##################-->
</div>
</div>
</div><!-- content ends --> 

</asp:Content>