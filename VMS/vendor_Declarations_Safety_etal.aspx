<%@ Page Title="" Language="C#" MasterPageFile="~/MasterSubPage.master" AutoEventWireup="true" CodeFile="vendor_Declarations_Safety_etal.aspx.cs" Inherits="vendor_Declarations_Safety_etal" %>
<%@ Register TagPrefix="Ava" TagName="Tabsnav" Src="usercontrols/tabs.ascx" %>
<%@ Register TagPrefix="Ava" TagName="TopNav" Src="usercontrols/TopNav_vendor.ascx" %>
<%@ Register TagPrefix="Ava" TagName="StepNav" Src="usercontrols/StepNav_vendor.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitlePlaceHolder1" runat="Server">
Trans-Asia VMS Accreditation :: Vendor Information
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%-- Add content controls here --%>
<!--### TOOLTIP STARTS ###-->
    <script type="text/javascript" src="Scripts/jquery.tooltip.min.js"></script>
    <link href="Styles/jquery.tooltip.css" rel="stylesheet" type="text/css" />
	<script type="text/javascript">
	    $(function () {
	        $('#stepnav li').tooltip({
	            track: true,
	            delay: 0,
	            showURL: false,
	            showBody: " - ",
	            fade: 250
	        });
	    });
    </script>
	<!--### TOOLTIP ENDS ###-->
    
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

    <!--### DATEPICKER ###-->
    <script type="text/javascript" src="Scripts/cal.js"></script>
    <link rel="stylesheet" type="text/css" href="Styles/calendar_picker.css" />
    <script type="text/javascript">
        jQuery(document).ready(function () {
            $('input.date').simpleDatepicker({ startdate: '6/10/1900' });
        });
        function reloadDatepicker() {
            $('input.date').simpleDatepicker({ startdate: '6/10/1900' });
        }
    </script> 
    <!--### DATEPICKER ENDS###-->

    <script src="Scripts/jquery.numeric.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $(".numeric").css('text-align', 'right');
            $(".integer").css('text-align', 'right');
            $(".numeric").numeric();
            $(".integer").numeric(false, function () { alert("Integers only"); this.value = ""; this.focus(); });

            $(".integer").digits();
            $(".integer").blur(function () {
                $(".integer").digits(); $(".integer").css('text-align', 'right');
            });
            $(".numeric").digits();
            $(".numeric").blur(function () {
                $(".numeric").digits(); $(".numeric").css('text-align', 'right');
            });
        });
        function reloadNumeric() {
            $(".numeric").numeric();
            $(".integer").numeric(false, function () { alert("Integers only"); this.value = ""; this.focus(); });
        }
        $.fn.digits = function () {
            return this.each(function () {
                $(this).val($(this).val().replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,"));
            })
        }
    </script>
	<script type="text/javascript" src="Scripts/jquery.table.addrow.js" ></script>
<link href="Styles/ava_pages.css" rel="stylesheet" type="text/css" />
<div class="content">
<div class="content_logo">
<a href=""><img src="images/final_logo_w.png"  border="0" /></a> 
<div style="float:right"></div>
</div>
<div class="rounded-corners-top" id="menuAVA">
<ava:tabsnav ID="Tabsnav1" runat="server" />
</div>
<div style="background:#FFF; min-height:445px; padding:10px;" class="rounded-corners-bottom2 menu">
<!--##################-->
<!--BODY CONTENT STARTS-->
<ava:topnav ID="TopNav1" runat="server" /><%--<ava:stepnav ID="StepNav1" runat="server" />--%>
<div style="margin:10px 0px; color:#333; font-size:18px; width:750px; float:left;">Supplier’s Declaration on Safety, Health, and Environmental Policies and Practices</div>


<form id="formVendorInfo" runat="server">

<!--Business activities STARTS-->
<div class="separator1"></div>

<%--<h3 style="margin:10px 0px;"><asp:Label runat="server" ID="VendorName" Text="Vendor Name"></asp:Label></h3>--%>
<table width="100" border="0" cellspacing="0" cellpadding="5" id="tbl01" runat="server">
    <tr>
        <td style="width: 238px"><label>Vendor</label></td>
        <td><input name="CompanyName" type="text" id="CompanyName" size="60" runat="server" readonly="readonly" /></td>
    </tr>
    <tr>
        <td style="width: 138px"><label for="CompanyName">Date of evalution</label></td>
        <td><input name="DateOfEvaluation" type="text" id="DateOfEvaluation" class="date" runat="server"  /></td>
    </tr>
  </table>

<table width="100%" border="0" cellspacing="0" cellpadding="5" id="tbl01_Lbl" runat="server">
    <tr>
        <td style="width: 138px"><label>Vendor</label></td>
        <td><h3><asp:Label runat="server" ID="CompanyName_Lbl"></asp:Label></h3></td>
    </tr>
    <tr>
        <td><label for="CompanyName">Date of evalution</label></td>
        <td><h3><asp:Label runat="server" ID="DateOfEvaluation_Lbl"></asp:Label></h3></td>
    </tr>
  </table>
<br />



<!--Business activities STARTS-->
<div class="separator1"></div>
<h3 style="margin:10px 0px;">&nbsp;</h3>
<table  border="0" cellspacing="0" cellpadding="5" id="tbl02" runat="server">
  <tr>
    <td width="690" ><label>1. Does your company have an EMS certified to ISO 14001?</label></td>
    <td width="271">
        <asp:RadioButtonList ID="Q1" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
            <asp:ListItem Value="Yes">Yes</asp:ListItem>
            <asp:ListItem Value="No">No</asp:ListItem>
        </asp:RadioButtonList>
      </td>
  </tr>
  <tr>
    <td ><label>2. Does your company have an OSH Management System certified to OHSAS 18001?</label></td>
    <td>
        <asp:RadioButtonList ID="Q2" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
            <asp:ListItem Value="Yes">Yes</asp:ListItem>
            <asp:ListItem Value="No">No</asp:ListItem>
        </asp:RadioButtonList>
      </td>
  </tr>
  <tr>
    <td ><label>3. If NO to items 1 and 2, does your company have a policy on occupational safety, health and environment?</label></td>
    <td>
        <asp:RadioButtonList ID="Q3" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
            <asp:ListItem Value="Yes">Yes</asp:ListItem>
            <asp:ListItem Value="No">No</asp:ListItem>
        </asp:RadioButtonList>
      </td>
  </tr>
  <tr>
    <td ><label>4. Does your company have an organization who manages the safety, health and environmental concerns of your company?</label></td>
    <td>
        <asp:RadioButtonList ID="Q4" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
            <asp:ListItem Value="Yes">Yes</asp:ListItem>
            <asp:ListItem Value="No">No</asp:ListItem>
        </asp:RadioButtonList>
      </td>
  </tr>
  <tr>
    <td ><label>5. Have you identified the significant environmental aspects of your company?</label></td>
    <td>
        <asp:RadioButtonList ID="Q5" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
            <asp:ListItem Value="Yes">Yes</asp:ListItem>
            <asp:ListItem Value="No">No</asp:ListItem>
        </asp:RadioButtonList>
      </td>
  </tr>
  <tr>
    <td ><label>6. Have you identified the occupational hazards and risks of the different activities in your operations?</label></td>
    <td>
        <asp:RadioButtonList ID="Q6" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
            <asp:ListItem Value="Yes">Yes</asp:ListItem>
            <asp:ListItem Value="No">No</asp:ListItem>
        </asp:RadioButtonList>
      </td>
  </tr>
  <tr>
    <td ><label>7. Do you have programs in place to ensure your employees' safety and wellness?</label></td>
    <td>
        <asp:RadioButtonList ID="Q7" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
            <asp:ListItem Value="Yes">Yes</asp:ListItem>
            <asp:ListItem Value="No">No</asp:ListItem>
        </asp:RadioButtonList>
      </td>
  </tr>
  <tr>
    <td ><label>8. Do you have programs in place to manage your environmental aspects?</label></td>
    <td>
        <asp:RadioButtonList ID="Q8" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
            <asp:ListItem Value="Yes">Yes</asp:ListItem>
            <asp:ListItem Value="No">No</asp:ListItem>
        </asp:RadioButtonList>
      </td>
  </tr>
  <tr>
    <td ><label>9. Do you have programs in place to save on resources like energy and water?</label></td>
    <td>
        <asp:RadioButtonList ID="Q9" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
            <asp:ListItem Value="Yes">Yes</asp:ListItem>
            <asp:ListItem Value="No">No</asp:ListItem>
        </asp:RadioButtonList>
      </td>
  </tr>
  <tr>
    <td ><label>10. Do you have a program to manage your Greenhouse Gas emissions?</label></td>
    <td>
        <asp:RadioButtonList ID="Q10" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
            <asp:ListItem Value="Yes">Yes</asp:ListItem>
            <asp:ListItem Value="No">No</asp:ListItem>
        </asp:RadioButtonList>
      </td>
  </tr>
  <tr>
    <td ><label>11. Do you evaluate your own suppliers on their SHE policies and practices?</label></td>
    <td>
        <asp:RadioButtonList ID="Q11" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
            <asp:ListItem Value="Yes">Yes</asp:ListItem>
            <asp:ListItem Value="No">No</asp:ListItem>
        </asp:RadioButtonList>
      </td>
  </tr>
  <tr>
    <td colspan="2"><label>12. Please list the major laws and other legal requirements that your company comply with.</label></td>
  </tr>
  <tr>
    <td colspan="2"><input name="Q12a" type="text" id="Q12a" size="60" runat="server"   /></td>
  </tr>
  <tr>
    <td colspan="2"><input name="Q12b" type="text" id="Q12b" size="60" runat="server"   /></td>
  </tr>
  <tr>
    <td colspan="2"><input name="Q12c" type="text" id="Q12c" size="60" runat="server"   /></td>
  </tr>
  <tr>
    <td colspan="2"><input name="Q12d" type="text" id="Q12d" size="60" runat="server"   /></td>
  </tr>
</table>


<table  border="0" cellspacing="0" cellpadding="5" id="tbl02_Lbl" runat="server">
  <tr>
    <td width="690" ><label>1. Does your company have an EMS certified to ISO 14001?</label></td>
    <td width="271"><h3><asp:Label runat="server" ID="Q1_Lbl"></asp:Label></h3></td>
  </tr>
  <tr>
    <td ><label>2. Does your company have an OSH Management System certified to OHSAS 18001?</label></td>
    <td><h3><asp:Label runat="server" ID="Q2_Lbl"></asp:Label></h3></td>
  </tr>
  <tr>
    <td ><label>3. If NO to items 1 and 2, does your company have a policy on occupational safety, health and environment?</label></td>
    <td><h3><asp:Label runat="server" ID="Q3_Lbl"></asp:Label></h3></td>
  </tr>
  <tr>
    <td ><label>4. Does your company have an organization who manages the safety, health and environmental concerns of your company?</label></td>
    <td><h3><asp:Label runat="server" ID="Q4_Lbl"></asp:Label></h3></td>
  </tr>
  <tr>
    <td ><label>5. Have you identified the significant environmental aspects of your company?</label></td>
    <td><h3><asp:Label runat="server" ID="Q5_Lbl"></asp:Label></h3></td>
  </tr>
  <tr>
    <td ><label>6. Have you identified the occupational hazards and risks of the different activities in your operations?</label></td>
    <td><h3><asp:Label runat="server" ID="Q6_Lbl"></asp:Label></h3></td>
  </tr>
  <tr>
    <td ><label>7. Do you have programs in place to ensure your employees' safety and wellness?</label></td>
    <td><h3><asp:Label runat="server" ID="Q7_Lbl"></asp:Label></h3></td>
  </tr>
  <tr>
    <td ><label>8. Do you have programs in place to manage your environmental aspects?</label></td>
    <td><h3><asp:Label runat="server" ID="Q8_Lbl"></asp:Label></h3></td>
  </tr>
  <tr>
    <td ><label>9. Do you have programs in place to save on resources like energy and water?</label></td>
    <td><h3><asp:Label runat="server" ID="Q9_Lbl"></asp:Label></h3></td>
  </tr>
  <tr>
    <td ><label>10. Do you have a program to manage your Greenhouse Gas emissions?</label></td>
    <td><h3><asp:Label runat="server" ID="Q10_Lbl"></asp:Label></h3></td>
  </tr>
  <tr>
    <td ><label>11. Do you evaluate your own suppliers on their SHE policies and practices?</label></td>
    <td><h3><asp:Label runat="server" ID="Q11_Lbl"></asp:Label></h3></td>
  </tr>
  <tr>
    <td colspan="2"><label>12. Please list the major laws and other legal requirements that your company comply with.</label></td>
  </tr>
  <tr>
    <td colspan="2"><h3><asp:Label runat="server" ID="Q12a_Lbl"></asp:Label></h3></td>
  </tr>
  <tr>
    <td colspan="2"><h3><asp:Label runat="server" ID="Q12b_Lbl"></asp:Label></h3></td>
  </tr>
  <tr>
    <td colspan="2"><h3><asp:Label runat="server" ID="Q12c_Lbl"></asp:Label></h3></td>
  </tr>
  <tr>
    <td colspan="2"><h3><asp:Label runat="server" ID="Q12d_Lbl"></asp:Label></h3></td>
  </tr>
</table>
<br />
<div class="separator1"></div>

    <table border="0" cellspacing="0" cellpadding="5" id="tbl03" runat="server">
    <tr>
        <td style="width: 138px"><label for="CompanyName">Approved Date</label></td>
        <td><input name="ApprovedDate" type="text" id="ApprovedDate" class="date" runat="server"    /></td>
    </tr>
    <tr>
        <td><label for="CompanyName">Printed Name</label></td>
        <td><input name="PrintedName" type="text" id="PrintedName" runat="server"    /></td>
    </tr>
    <tr>
        <td><label for="CompanyName">Position</label></td>
        <td><input name="Position" type="text" id="Position" runat="server"    /></td>
    </tr>
  </table>
    <table border="0" cellspacing="0" cellpadding="5" id="tbl03_Lbl" runat="server">
    <tr>
        <td style="width: 138px"><label for="CompanyName">Approved Date</label></td>
        <td style="width: 185px"><h3><asp:Label runat="server" ID="ApprovedDate_Lbl"></asp:Label></h3></td>
    </tr>
    <tr>
        <td><label for="CompanyName">Printed Name</label></td>
        <td style="width: 185px"><h3><asp:Label runat="server" ID="PrintedName_Lbl"></asp:Label></h3></td>
    </tr>
    <tr>
        <td><label for="CompanyName">Position</label></td>
        <td style="width: 185px"><h3><asp:Label runat="server" ID="Position_Lbl"></asp:Label></h3></td>
    </tr>
  </table>



<div class="separator1"></div>
<br />
<br />
<br />
<asp:LinkButton ID="createBt" runat="server" CssClass="bt1" onclientclick="javascript: __doPostBack('continueStp', ''); return false;"><span>SAVE &amp; CONTINUE &raquo;</span></asp:LinkButton>&nbsp;
<asp:LinkButton ID="createBt1" runat="server" CssClass="bt1"  onclientclick="javascript: __doPostBack('justSave', ''); return false;"><span>SAVE</span></asp:LinkButton>&nbsp;
<a href="vendor_08_Undertakings.aspx<%= queryString %>" class="bt1"><span>BACK</span></a>
<br />
<br />
<br />
</form>




<!--BODY CONTENT ENDS-->
<!--##################-->
</div>
</div>
</div><!-- content ends --> 

    </div>

    </div>

</asp:Content>