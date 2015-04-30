<%@ Page Title="" Language="C#" MasterPageFile="~/MasterSubPage.master" AutoEventWireup="true" CodeFile="vendor_Declarations_Business.aspx.cs" Inherits="vendor_Declarations_Business" %>
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
<div style="margin:10px 0px; color:#333; font-size:18px; width:650px; float:left;">Supplier’s Declaration on Business Continuity Management</div>


<form id="formVendorInfo" runat="server">

<!--Business activities STARTS-->
<div class="separator1"></div>

<%--<h3 style="margin:10px 0px;"><asp:Label runat="server" ID="VendorName" Text="Vendor Name"></asp:Label></h3>--%>
<table border="0" cellspacing="0" cellpadding="5" id="tbl01" runat="server">
    <tr>
        <td style="width: 138px"><label>Vendor</label></td>
        <td><input name="CompanyName" type="text" id="CompanyName" size="60" runat="server" readonly="readonly"/></td>
    </tr>
    <tr>
        <td><label for="CompanyName">Date of evalution</label></td>
        <td><input name="DateOfEvaluation" type="text" id="DateOfEvaluation" class="date" runat="server" /></td>
    </tr>
  </table>

<table border="0" cellspacing="0" cellpadding="5" id="tbl01_Lbl" runat="server">
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
    <table>
        <tr>
            <td colspan="2" valign="bottom" width="451">
                <p align="center" class="MsoNormal" style="margin-bottom:0in;margin-bottom:.0001pt;
  text-align:center;line-height:normal">
                    <h4><span>Legend<o:p></o:p></span></h4></p>
            </td>
        </tr>
        <tr>
            <td valign="bottom" width="56">
                <p align="center" class="MsoNormal" style="margin-bottom:0in;margin-bottom:.0001pt;
  text-align:center;line-height:normal">
                    <b><span>Rating<o:p></o:p></span></b></p>
            </td>
            <td width="395">
                <p align="left" class="MsoNormal" style="margin-bottom:0in;margin-bottom:.0001pt;
  text-align:left;line-height:normal">
                    <b><span>Compliance<o:p></o:p></span></b></p>
            </td>
        </tr>
        <tr>
            <td valign="bottom" width="56">
                <p align="center" class="MsoNormal" style="margin-bottom:0in;margin-bottom:.0001pt;
  text-align:center;line-height:normal">
                    <span>1<o:p></o:p></span></p>
            </td>
            <td width="395">
                <p class="MsoNormal" style="margin-bottom:0in;margin-bottom:.0001pt;line-height:
  normal">
                    <span>Nothing in Place / Not Compliant<o:p></o:p></span></p>
            </td>
        </tr>
        <tr>
            <td valign="bottom" width="56">
                <p align="center" class="MsoNormal" style="margin-bottom:0in;margin-bottom:.0001pt;
  text-align:center;line-height:normal">
                    <span>2<o:p></o:p></span></p>
            </td>
            <td width="395">
                <p class="MsoNormal" style="margin-bottom:0in;margin-bottom:.0001pt;line-height:
  normal">
                    <span>Just Starting to Develop<o:p></o:p></span></p>
            </td>
        </tr>
        <tr>
            <td valign="bottom" width="56">
                <p align="center" class="MsoNormal" style="margin-bottom:0in;margin-bottom:.0001pt;
  text-align:center;line-height:normal">
                    <span>3<o:p></o:p></span></p>
            </td>
            <td width="395">
                <p class="MsoNormal" style="margin-bottom:0in;margin-bottom:.0001pt;line-height:
  normal">
                    <span>Developed but not yet approved or tested or implemented<o:p></o:p></span></p>
            </td>
        </tr>
        <tr>
            <td valign="bottom" width="56">
                <p align="center" class="MsoNormal" style="margin-bottom:0in;margin-bottom:.0001pt;
  text-align:center;line-height:normal">
                    <span>4<o:p></o:p></span></p>
            </td>
            <td width="395">
                <p class="MsoNormal" style="margin-bottom:0in;margin-bottom:.0001pt;line-height:
  normal">
                    <span>Documented and approved but not yet cascaded AND tested or implemented)<o:p></o:p></span></p>
            </td>
        </tr>
        <tr>
            <td valign="bottom" width="56">
                <p align="center" class="MsoNormal" style="margin-bottom:0in;margin-bottom:.0001pt;
  text-align:center;line-height:normal">
                    <span>5<o:p></o:p></span></p>
            </td>
            <td width="395">
                <p class="MsoNormal" style="margin-bottom:0in;margin-bottom:.0001pt;line-height:
  normal">
                    <span>Fully Compliant (documented, approved, cascaded AND tested or implemented)<o:p></o:p></span></p>
            </td>
        </tr>
    </table>
    <div class="clearfix"></div>





<div class="separator1"></div>

<table  border="0" cellspacing="0" cellpadding="5" runat="server" id="tbl02">
  <tr>
    <td width="700"><h3>A. BCM Program</h3></td>
    <td width="60">
        <label>Rating</label></td>
    <td width="200">
        <label>Remarks</label></td>
  </tr>
  <tr>
    <td><label>Are your services which are currently offered to Trans-Asia, certified for business continuity management by accredited Organizations or certification bodies?</label></td>
    <td>
        <input type="text" id="A_Rating_Q1" class="numeric" runat="server" size="4" maxlength="1"    /></td>
    <td>
        <input type="text" id="A_Remarks_Q1" runat="server" maxlength="50"    /></td>
  </tr>
  <tr>
    <td><label>Does your Organization have a calendar plan for the implementation and maintenance of its Business Continuity Program?</label></td>
    <td >
        <input type="text" id="A_Rating_Q2" class="numeric" runat="server" size="4" maxlength="1"    /></td>
    <td >
        <input type="text" id="A_Remarks_Q2" runat="server" maxlength="50"    /></td>
  </tr>
  <tr>
    <td><label>Does your Organization have identified and assigned roles and responsibilities for business continuity management purposes?</label></td>
    <td>
        <input type="text" id="A_Rating_Q3" class="numeric" runat="server" size="4" maxlength="1"    /></td>
    <td>
        <input type="text" id="A_Remarks_Q3" runat="server" maxlength="50"    /></td>
  </tr>
  <tr>
    <td><label>Does your Organization have an approved business continuity policy and defined objectives for its business continuity management program ?</label></td>
    <td>
        <input type="text" id="A_Rating_Q4" class="numeric" runat="server" size="4" maxlength="1"    /></td>
    <td>
        <input type="text" id="A_Remarks_Q4" runat="server" maxlength="50"    /></td>
  </tr>
  <tr>
    <td><label>Are the observations and preventive and corrective action points which were identified based on incidents, reviews, exercises and audits tracked to closure?</label></td>
    <td>
        <input type="text" id="A_Rating_Q5" class="numeric" runat="server" size="4" maxlength="1"    /></td>
    <td>
        <input type="text" id="A_Remarks_Q5" runat="server" maxlength="50"    /></td>
  </tr>
  <tr>
    <td><label>Have there been any incidents of discontinuity of services at your end and have corrective actions been taken to prevent such incidents in the future ?</label></td>
    <td>
        <input type="text" id="A_Rating_Q6" class="numeric" runat="server" size="4" maxlength="1"    /></td>
    <td>
        <input type="text" id="A_Remarks_Q6" runat="server" maxlength="50"    /></td>
  </tr>
  <tr>
    <td><label>Has your Organization defined and documented an Incident Management plan which includes incident detection, escalation, handling and logging procedures and the response teams required for all critical functions?</label></td>
    <td>
        <input type="text" id="A_Rating_Q7" class="numeric" runat="server" size="4" maxlength="1"    /></td>
    <td>
        <input type="text" id="A_Remarks_Q7" runat="server" maxlength="50"    /></td>
  </tr>
  <tr>
    <td><label>Has your Organization shared and obtained required approvals from Trans-Asia Management for your business continuity planning (for e.g. Recovery Time Objective, risk assessments and recovery plans) for all functions ?</label></td>
    <td>
        <input type="text" id="A_Rating_Q8" class="numeric" runat="server" size="4" maxlength="1"    /></td>
    <td>
        <input type="text" id="A_Remarks_Q8" runat="server" maxlength="50"    /></td>
  </tr>
  </table>


    <h3><asp:Label runat="server" ID="DateofEvaluation0_Lbl3"></asp:Label></h3>


<table  border="0" cellspacing="0" cellpadding="5" runat="server" id="tbl02_Lbl">
  <tr>
    <td width="700"><h3>A. BCM Program</h3></td>
    <td width="60">
        <label>Rating</label></td>
    <td width="200">
        <label>Remarks</label></td>
  </tr>
  <tr>
    <td><label>Are your services which are currently offered to Trans-Asia, certified for business continuity management by accredited Organizations or certification bodies?</label></td>
    <td><h3><asp:Label runat="server" ID="A_Rating_Q1_Lbl"></asp:Label></h3></td>
    <td><h3><asp:Label runat="server" ID="A_Remarks_Q1_Lbl"></asp:Label></h3>
        </td>
  </tr>
  <tr>
    <td><label>Does your Organization have a calendar plan for the implementation and maintenance of its Business Continuity Program?</label></td>
    <td >
        <h3><asp:Label runat="server" ID="A_Rating_Q2_Lbl"></asp:Label></h3></td>
    <td >
        <h3><asp:Label runat="server" ID="A_Remarks_Q2_Lbl"></asp:Label></h3>
        </td>
  </tr>
  <tr>
    <td><label>Does your Organization have identified and assigned roles and responsibilities for business continuity management purposes?</label></td>
    <td>
        <h3><asp:Label runat="server" ID="A_Rating_Q3_Lbl"></asp:Label></h3></td>
    <td>
        <h3><asp:Label runat="server" ID="A_Remarks_Q3_Lbl"></asp:Label></h3>
        </td>
  </tr>
  <tr>
    <td><label>Does your Organization have an approved business continuity policy and defined objectives for its business continuity management program ?</label></td>
    <td>
        <h3><asp:Label runat="server" ID="A_Rating_Q4_Lbl"></asp:Label></h3></td>
    <td>
        <h3><asp:Label runat="server" ID="A_Remarks_Q4_Lbl"></asp:Label></h3>
        </td>
  </tr>
  <tr>
    <td><label>Are the observations and preventive and corrective action points which were identified based on incidents, reviews, exercises and audits tracked to closure?</label></td>
    <td>
        <h3><asp:Label runat="server" ID="A_Rating_Q5_Lbl"></asp:Label></h3></td>
    <td>
        <h3><asp:Label runat="server" ID="A_Remarks_Q5_Lbl"></asp:Label></h3>
        </td>
  </tr>
  <tr>
    <td><label>Have there been any incidents of discontinuity of services at your end and have corrective actions been taken to prevent such incidents in the future ?</label></td>
    <td>
        <h3><asp:Label runat="server" ID="A_Rating_Q6_Lbl"></asp:Label></h3></td>
    <td>
        <h3><asp:Label runat="server" ID="A_Remarks_Q6_Lbl"></asp:Label></h3>
        </td>
  </tr>
  <tr>
    <td><label>Has your Organization defined and documented an Incident Management plan which includes incident detection, escalation, handling and logging procedures and the response teams required for all critical functions?</label></td>
    <td>
        <h3><asp:Label runat="server" ID="A_Rating_Q7_Lbl"></asp:Label></h3></td>
    <td>
        <h3><asp:Label runat="server" ID="A_Remarks_Q7_Lbl"></asp:Label></h3>
        </td>
  </tr>
  <tr>
    <td><label>Has your Organization shared and obtained required approvals from Trans-Asia Management for your business continuity planning (for e.g. Recovery Time Objective, risk assessments and recovery plans) for all functions ?</label></td>
    <td>
        <h3><asp:Label runat="server" ID="A_Rating_Q8_Lbl"></asp:Label></h3></td>
    <td>
        <h3><asp:Label runat="server" ID="A_Remarks_Q8_Lbl"></asp:Label></h3>
        </td>
  </tr>
  </table>
 
    
    <div class="separator1"></div>
    
    
       
<table  border="0" cellspacing="0" cellpadding="5" id="tbl03" runat="server">
  <tr>
    <td width="700"><h3>B. Function Recovery</h3></td>
    <td width="60">
        <label>Rating</label></td>
    <td width="200">
        <label>Remarks</label></td>
  </tr>
  <tr>
    <td><label>Does your Organization periodically conduct a business impact analysis to identify critical functions, recovery time objective, dependencies and recovery priorities?</label></td>
    <td>
        <input type="text" id="B_Rating_Q1" class="numeric" runat="server" size="4" maxlength="1"    /></td>
    <td>
        <input type="text" id="B_Remarks_Q1" runat="server" maxlength="50"    /></td>
  </tr>
  <tr>
    <td><label>Does your Organization periodically conduct risk assessments for all business functions and facilities which support Trans-Asia's network operations?</label></td>
    <td>
        <input type="text" id="B_Rating_Q2" class="numeric" runat="server" size="4" maxlength="1"    /></td>
    <td>
        <input type="text" id="B_Remarks_Q2" runat="server" maxlength="50"    /></td>
  </tr>
  <tr>
    <td><label>Are the recovery strategies identified during the risk assessment exercise tracked to closure?</label></td>
    <td>
        <input type="text" id="B_Rating_Q3" class="numeric" runat="server" size="4" maxlength="1"    /></td>
    <td>
        <input type="text" id="B_Remarks_Q3" runat="server" maxlength="50"    /></td>
  </tr>
  <tr>
    <td><label>Are plans developed for crisis management with regards to services delivered for Trans-Asia?</label></td>
    <td>
        <input type="text" id="B_Rating_Q4" class="numeric" runat="server" size="4" maxlength="1"    /></td>
    <td>
        <input type="text" id="B_Remarks_Q4" runat="server" maxlength="50"    /></td>
  </tr>
  <tr>
    <td><label>Does the crisis management plan address the requirement to communicate to your Organization's internal stakeholders and Trans-Asia SPOCs in the event of an outage/ incident / strikes / labourstrifes?</label></td>
    <td>
        <input type="text" id="B_Rating_Q5" class="numeric" runat="server" size="4" maxlength="1"    /></td>
    <td>
        <input type="text" id="B_Remarks_Q5" runat="server" maxlength="50"    /></td>
  </tr>
  <tr>
    <td><label>Has your Organization assigned designated coordinators in case of pandemic situations who have been trained with their roles and responsibilities?</label></td>
    <td>
        <input type="text" id="B_Rating_Q6" class="numeric" runat="server" size="4" maxlength="1"    /></td>
    <td>
        <input type="text" id="B_Remarks_Q6" runat="server" maxlength="50"    /></td>
  </tr>
  <tr>
    <td><label>Has your Organization developed a pandemic response plan to mitigate the risks of a pandemic event ?</label></td>
    <td>
        <input type="text" id="B_Rating_Q7" class="numeric" runat="server" size="4" maxlength="1"    /></td>
    <td>
        <input type="text" id="B_Remarks_Q7" runat="server" maxlength="50"    /></td>
  </tr>
  <tr>
    <td><label>Do you have documented business continuity plans (including disaster recovery plans) to ensure continuous delivery of key business services to Trans-Asia in the event of a crisis?</label></td>
    <td>
        <input type="text" id="B_Rating_Q8" class="numeric" runat="server" size="4" maxlength="1"    /></td>
    <td>
        <input type="text" id="B_Remarks_Q8" runat="server" maxlength="50"    /></td>
  </tr>
  <tr>
    <td><label>Have you identified alternate/ fallback team members for operations in the event of a crisis. Are the details of alternate team members shared with Trans-Asia and also incorporated in the Disaster Recovery (DR) plans ?</label></td>
    <td>
        <input type="text" id="B_Rating_Q9" class="numeric" runat="server" size="4" maxlength="1"    /></td>
    <td>
        <input type="text" id="B_Remarks_Q9" runat="server" maxlength="50"    /></td>
  </tr>
  </table>

       
<table  border="0" cellspacing="0" cellpadding="5" id="tbl03_Lbl" runat="server">
  <tr>
    <td width="700"><h3>B. Function Recovery</h3></td>
    <td width="60">
        <label>Rating</label></td>
    <td width="200">
        <label>Remarks</label></td>
  </tr>
  <tr>
    <td><label>Does your Organization periodically conduct a business impact analysis to identify critical functions, recovery time objective, dependencies and recovery priorities?</label></td>
    <td>
        <h3><asp:Label runat="server" ID="B_Rating_Q1_Lbl"></asp:Label></h3></td>
    <td>
        <h3><asp:Label runat="server" ID="B_Remarks_Q1_Lbl"></asp:Label></h3></td>
  </tr>
  <tr>
    <td><label>Does your Organization periodically conduct risk assessments for all business functions and facilities which support Trans-Asia's network operations?</label></td>
    <td>
        <h3><asp:Label runat="server" ID="B_Rating_Q2_Lbl"></asp:Label></h3></td>
    <td>
        <h3><asp:Label runat="server" ID="B_Remarks_Q2_Lbl"></asp:Label></h3></td>
  </tr>
  <tr>
    <td><label>Are the recovery strategies identified during the risk assessment exercise tracked to closure?</label></td>
    <td>
        <h3><asp:Label runat="server" ID="B_Rating_Q3_Lbl"></asp:Label></h3></td>
    <td>
        <h3><asp:Label runat="server" ID="B_Remarks_Q3_Lbl"></asp:Label></h3></td>
  </tr>
  <tr>
    <td><label>Are plans developed for crisis management with regards to services delivered for Trans-Asia?</label></td>
    <td>
        <h3><asp:Label runat="server" ID="B_Rating_Q4_Lbl"></asp:Label></h3></td>
    <td>
        <h3><asp:Label runat="server" ID="B_Remarks_Q4_Lbl"></asp:Label></h3></td>
  </tr>
  <tr>
    <td><label>Does the crisis management plan address the requirement to communicate to your Organization's internal stakeholders and Trans-Asia SPOCs in the event of an outage/ incident / strikes / labourstrifes?</label></td>
    <td>
        <h3><asp:Label runat="server" ID="B_Rating_Q5_Lbl"></asp:Label></h3></td>
    <td>
        <h3><asp:Label runat="server" ID="B_Remarks_Q5_Lbl"></asp:Label></h3></td>
  </tr>
  <tr>
    <td><label>Has your Organization assigned designated coordinators in case of pandemic situations who have been trained with their roles and responsibilities?</label></td>
    <td>
        <h3><asp:Label runat="server" ID="B_Rating_Q6_Lbl"></asp:Label></h3></td>
    <td>
        <h3><asp:Label runat="server" ID="B_Remarks_Q6_Lbl"></asp:Label></h3></td>
  </tr>
  <tr>
    <td><label>Has your Organization developed a pandemic response plan to mitigate the risks of a pandemic event ?</label></td>
    <td>
        <h3><asp:Label runat="server" ID="B_Rating_Q7_Lbl"></asp:Label></h3></td>
    <td>
        <h3><asp:Label runat="server" ID="B_Remarks_Q7_Lbl"></asp:Label></h3></td>
  </tr>
  <tr>
    <td><label>Do you have documented business continuity plans (including disaster recovery plans) to ensure continuous delivery of key business services to Trans-Asia in the event of a crisis?</label></td>
    <td>
        <h3><asp:Label runat="server" ID="B_Rating_Q8_Lbl"></asp:Label></h3></td>
    <td>
        <h3><asp:Label runat="server" ID="B_Remarks_Q8_Lbl"></asp:Label></h3></td>
  </tr>
  <tr>
    <td><label>Have you identified alternate/ fallback team members for operations in the event of a crisis. Are the details of alternate team members shared with Trans-Asia and also incorporated in the Disaster Recovery (DR) plans ?</label></td>
    <td>
        <h3><asp:Label runat="server" ID="B_Rating_Q9_Lbl"></asp:Label></h3></td>
    <td>
        <h3><asp:Label runat="server" ID="B_Remarks_Q9_Lbl"></asp:Label></h3></td>
  </tr>
  </table>

    
    <div class="separator1"></div>
    
    
       
<table  border="0" cellspacing="0" cellpadding="5" id="tbl04" runat="server">
  <tr>
    <td width="700" style="height: 33px"><h3>C. Technology Resilience</h3></td>
    <td width="60" style="height: 33px">
        <label>Rating</label></td>
    <td width="200" style="height: 33px">
        <label>Remarks</label></td>
  </tr>
  <tr>
    <td><label>Is component level Disaster Recovery (DR) capability present to support Trans-Asia's products and services?</label></td>
    <td>
        <input type="text" id="C_Rating_Q1" class="numeric" runat="server" size="4" maxlength="1"    /></td>
    <td>
        <input name="DateofEvaluation12" type="text" id="C_Remarks_Q1" runat="server" maxlength="50"    /></td>
  </tr>
  <tr>
    <td><label>Is there a defined calendar plan for implementing and testing disaster recovery capability for all critical technological components?</label></td>
    <td>
        <input type="text" id="C_Rating_Q2" class="numeric" runat="server" size="4" maxlength="1"    /></td>
    <td>
        <input type="text" id="C_Remarks_Q2" runat="server" maxlength="50"    /></td>
  </tr>
  <tr>
    <td><label>Does the Disaster Recovery capability effectively meet Trans-Asia's service delivery requirements?</label></td>
    <td>
        <input type="text" id="C_Rating_Q3" class="numeric" runat="server" size="4" maxlength="1"    /></td>
    <td>
        <input type="text" id="C_Remarks_Q3" runat="server" maxlength="50"    /></td>
  </tr>
  </table>
<table  border="0" cellspacing="0" cellpadding="5" id="tbl04_Lbl" runat="server">
  <tr>
    <td width="700" style="height: 33px"><h3>C. Technology Resilience</h3></td>
    <td width="60" style="height: 33px">
        <label>Rating</label></td>
    <td width="200" style="height: 33px">
        <label>Remarks</label></td>
  </tr>
  <tr>
    <td><label>Is component level Disaster Recovery (DR) capability present to support Trans-Asia's products and services?</label></td>
    <td>
        <h3><asp:Label runat="server" ID="C_Rating_Q1_Lbl"></asp:Label></h3></td>
    <td>
        <h3><asp:Label runat="server" ID="C_Remarks_Q1_Lbl"></asp:Label></h3></td>
  </tr>
  <tr>
    <td><label>Is there a defined calendar plan for implementing and testing disaster recovery capability for all critical technological components?</label></td>
    <td>
        <h3><asp:Label runat="server" ID="C_Rating_Q2_Lbl"></asp:Label></h3></td>
    <td>
        <h3><asp:Label runat="server" ID="C_Remarks_Q2_Lbl"></asp:Label></h3></td>
  </tr>
  <tr>
    <td><label>Does the Disaster Recovery capability effectively meet Trans-Asia's service delivery requirements?</label></td>
    <td>
        <h3><asp:Label runat="server" ID="C_Rating_Q3_Lbl"></asp:Label></h3></td>
    <td>
        <h3><asp:Label runat="server" ID="C_Remarks_Q3_Lbl"></asp:Label></h3></td>
  </tr>
  </table>

    <div class="separator1"></div>
    
    
       
<table  border="0" cellspacing="0" cellpadding="5" id="tbl05" runat="server">
  <tr>
    <td width="700"><h3>D. Training and Awareness</h3></td>
    <td width="60">
        <label>Rating</label></td>
    <td width="200">
        <label>Remarks</label></td>
  </tr>
  <tr>
    <td><label>Does your Organization have an employee business continuity training and awareness program in place ?</label></td>
    <td>
        <input type="text" id="D_Rating_Q1" class="numeric" runat="server" size="4" maxlength="1"    /></td>
    <td>
        <input type="text" id="D_Remarks_Q1" runat="server" maxlength="50"    /></td>
  </tr>
  <tr>
    <td><label>Have the roles and responsibilities been defined for BCMS personnel to act in the event of a disaster ?</label></td>
    <td>
        <input type="text" id="D_Rating_Q2" class="numeric" runat="server" size="4" maxlength="1"    /></td>
    <td>
        <input type="text" id="D_Remarks_Q2" runat="server" maxlength="50"    /></td>
  </tr>
  <tr>
    <td><label>Is the training program conducted effectively? Please share the results/ feedback received of the training programme.</label></td>
    <td>
        <input type="text" id="D_Rating_Q3" class="numeric" runat="server" size="4" maxlength="1"    /></td>
    <td>
        <input type="text" id="D_Remarks_Q3" runat="server" maxlength="50"    /></td>
  </tr>
  <tr>
    <td><label>Are you ensuring sufficient coverage of trainings to all employees and specific BCMS personnel ?</label></td>
    <td>
        <input type="text" id="D_Rating_Q4" class="numeric" runat="server" size="4" maxlength="1"    /></td>
    <td>
        <input type="text" id="D_Remarks_Q4" runat="server" maxlength="50"    /></td>
  </tr>
  </table>

<table  border="0" cellspacing="0" cellpadding="5" id="tbl05_Lbl" runat="server">
  <tr>
    <td width="700"><h3>D. Training and Awareness</h3></td>
    <td width="60">
        <label>Rating</label></td>
    <td width="200">
        <label>Remarks</label></td>
  </tr>
  <tr>
    <td><label>Does your Organization have an employee business continuity training and awareness program in place ?</label></td>
    <td>
        <h3><asp:Label runat="server" ID="D_Rating_Q1_Lbl"></asp:Label></h3></td>
    <td>
        <h3><asp:Label runat="server" ID="D_Remarks_Q1_Lbl"></asp:Label></h3></td>
  </tr>
  <tr>
    <td><label>Have the roles and responsibilities been defined for BCMS personnel to act in the event of a disaster ?</label></td>
    <td>
        <h3><asp:Label runat="server" ID="D_Rating_Q2_Lbl"></asp:Label></h3></td>
    <td>
        <h3><asp:Label runat="server" ID="D_Remarks_Q2_Lbl"></asp:Label></h3></td>
  </tr>
  <tr>
    <td><label>Is the training program conducted effectively? Please share the results/ feedback received of the training programme.</label></td>
    <td>
        <h3><asp:Label runat="server" ID="D_Rating_Q3_Lbl"></asp:Label></h3></td>
    <td>
        <h3><asp:Label runat="server" ID="D_Remarks_Q3_Lbl"></asp:Label></h3></td>
  </tr>
  <tr>
    <td><label>Are you ensuring sufficient coverage of trainings to all employees and specific BCMS personnel ?</label></td>
    <td>
        <h3><asp:Label runat="server" ID="D_Rating_Q4_Lbl"></asp:Label></h3></td>
    <td>
        <h3><asp:Label runat="server" ID="D_Remarks_Q4_Lbl"></asp:Label></h3></td>
  </tr>
  </table>

    <div class="separator1"></div>
    
    
       
<table  border="0" cellspacing="0" cellpadding="5" id="tbl06" runat="server">
  <tr>
    <td width="700"><h3>E. Exercising</h3></td>
    <td width="60">
        <label>Rating</label></td>
    <td width="200">
        <label>Remarks</label></td>
  </tr>
  <tr>
    <td><label>Are your business continuity plans tested on a periodic basis within a period of past twelve months?</label></td>
    <td>
        <input type="text" id="E_Rating_Q1" class="numeric" runat="server" size="4" maxlength="1"    /></td>
    <td>
        <input type="text" id="E_Remarks_Q1"  runat="server"   maxlength="50"   /></td>
  </tr>
  <tr>
    <td><label>Are the observations from the exercises and testing documented and closed within expected timelines ?</label></td>
    <td>
        <input type="text" id="E_Rating_Q2" class="numeric" runat="server" size="4" maxlength="1"    /></td>
    <td>
        <input type="text" id="E_Remarks_Q2" runat="server" maxlength="50"   /></td>
  </tr>
  </table>
       
<table  border="0" cellspacing="0" cellpadding="5" id="tbl06_Lbl" runat="server">
  <tr>
    <td width="700"><h3>E. Exercising</h3></td>
    <td width="60">
        <label>Rating</label></td>
    <td width="200">
        <label>Remarks</label></td>
  </tr>
  <tr>
    <td><label>Are your business continuity plans tested on a periodic basis within a period of past twelve months?</label></td>
    <td>
        <h3><asp:Label runat="server" ID="E_Rating_Q1_Lbl"></asp:Label></h3></td>
    <td>
        <h3><asp:Label runat="server" ID="E_Remarks_Q1_Lbl"></asp:Label></h3></td>
  </tr>
  <tr>
    <td><label>Are the observations from the exercises and testing documented and closed within expected timelines ?</label></td>
    <td>
        <h3><asp:Label runat="server" ID="E_Rating_Q2_Lbl"></asp:Label></h3></td>
    <td>
        <h3><asp:Label runat="server" ID="E_Remarks_Q2_Lbl"></asp:Label></h3></td>
  </tr>
  </table>



    <div class="separator1"></div>
    
    
       
<table  border="0" cellspacing="0" cellpadding="5" id="tbl07" runat="server">
  <tr>
    <td width="700"><h3>F. Reviewing</h3></td>
    <td width="60">
        <label>Rating</label></td>
    <td width="200">
        <label>Remarks</label></td>
  </tr>
  <tr>
    <td><label>Is there a review calendar maintained to check for the business continuity arrangements your Organization has for the services offered to Trans-Asia ?</label></td>
    <td>
        <input type="text" id="F_Rating_Q1" class="numeric" runat="server" size="4" maxlength="1"    /></td>
    <td>
        <input type="text" id="F_Remarks_Q1" runat="server" maxlength="50"    /></td>
  </tr>
  <tr>
    <td><label>Does your Organization have a review program to monitor and track the readiness of all third parties associated with Trans-Asia functions and systems ?</label></td>
    <td>
        <input type="text" id="F_Rating_Q2" class="numeric" runat="server" size="4" maxlength="1"    /></td>
    <td>
        <input type="text" id="F_Remarks_Q2" runat="server" maxlength="50"    /></td>
  </tr>
  </table>
<table  border="0" cellspacing="0" cellpadding="5" id="tbl07_Lbl" runat="server">
  <tr>
    <td width="700"><h3>F. Reviewing</h3></td>
    <td width="60">
        <label>Rating</label></td>
    <td width="200">
        <label>Remarks</label></td>
  </tr>
  <tr>
    <td><label>Is there a review calendar maintained to check for the business continuity arrangements your Organization has for the services offered to Trans-Asia ?</label></td>
    <td>
        <h3><asp:Label runat="server" ID="F_Rating_Q1_Lbl"></asp:Label></h3></td>
    <td>
        <h3><asp:Label runat="server" ID="F_Remarks_Q1_Lbl"></asp:Label></h3></td>
  </tr>
  <tr>
    <td><label>Does your Organization have a review program to monitor and track the readiness of all third parties associated with Trans-Asia functions and systems ?</label></td>
    <td>
        <h3><asp:Label runat="server" ID="F_Rating_Q2_Lbl"></asp:Label></h3></td>
    <td>
        <h3><asp:Label runat="server" ID="F_Remarks_Q2_Lbl"></asp:Label></h3></td>
  </tr>
  </table>


    <div class="separator1"></div>
    
    
       
<table  border="0" cellspacing="0" cellpadding="5" id="tbl08" runat="server">
  <tr>
    <td width="700"><h3>G. Contract Governance</h3></td>
    <td width="60">
        <label>Rating</label></td>
    <td width="200">
        <label>Remarks</label></td>
  </tr>
  <tr>
    <td><label>Is the SLA compliance reported on a monthly basis to Trans-Asia ?</label></td>
    <td>
        <input type="text" id="G_Rating_Q1" class="numeric" runat="server" size="4" maxlength="1"    /></td>
    <td>
        <input type="text" id="G_Remarks_Q1" runat="server" maxlength="50"    /></td>
  </tr>
  <tr>
    <td><label>Is there any provision to accommodate modifications in the contract in case of changes in the business continuity requirements?</label></td>
    <td>
        <input type="text" id="G_Rating_Q2" class="numeric" runat="server" size="4" maxlength="1"    /></td>
    <td>
        <input type="text" id="G_Remarks_Q2" runat="server" maxlength="50"    /></td>
  </tr>
  <tr>
    <td><label>Does your Organization's business continuity program ensure delivery of contracted products / services provided by your vendors ?</label></td>
    <td>
        <input type="text" id="G_Rating_Q3" class="numeric" runat="server" size="4" maxlength="1"    /></td>
    <td>
        <input type="text" id="G_Remarks_Q3" runat="server" maxlength="50"    /></td>
  </tr>
  </table>

<table  border="0" cellspacing="0" cellpadding="5" id="tbl08_Lbl" runat="server">
  <tr>
    <td width="700"><h3>G. Contract Governance</h3></td>
    <td width="60">
        <label>Rating</label></td>
    <td width="200">
        <label>Remarks</label></td>
  </tr>
  <tr>
    <td><label>Is the SLA compliance reported on a monthly basis to Trans-Asia ?</label></td>
    <td>
        <h3><asp:Label runat="server" ID="G_Rating_Q1_Lbl"></asp:Label></h3></td>
    <td>
        <h3><asp:Label runat="server" ID="G_Remarks_Q1_Lbl"></asp:Label></h3></td>
  </tr>
  <tr>
    <td><label>Is there any provision to accommodate modifications in the contract in case of changes in the business continuity requirements?</label></td>
    <td>
        <h3><asp:Label runat="server" ID="G_Rating_Q2_Lbl"></asp:Label></h3></td>
    <td>
        <h3><asp:Label runat="server" ID="G_Remarks_Q2_Lbl"></asp:Label></h3></td>
  </tr>
  <tr>
    <td><label>Does your Organization's business continuity program ensure delivery of contracted products / services provided by your vendors ?</label></td>
    <td>
        <h3><asp:Label runat="server" ID="G_Rating_Q3_Lbl"></asp:Label></h3></td>
    <td>
        <h3><asp:Label runat="server" ID="G_Remarks_Q3_Lbl"></asp:Label></h3></td>
  </tr>
  </table>







<br />
<div class="separator1"></div>
    <table border="0" cellspacing="0" cellpadding="5" id="tbl09" runat="server">
    <tr>
        <td style="width: 138px"><label for="CompanyName">Approved Date</label></td>
        <td style="width: 138px"><input type="text" id="ApprovedDate" class="date" runat="server"    /></td>
    </tr>
    <tr>
        <td style="width: 138px"><label for="CompanyName">Printed Name</label></td>
        <td style="width: 138px"><input name="DateofEvaluation" type="text" id="PrintedName" runat="server"    /></td>
    </tr>
    <tr>
        <td style="width: 138px"><label for="CompanyName">Position</label></td>
        <td style="width: 138px"><input type="text" id="Position" runat="server"    /></td>
    </tr>
  </table>
<table border="0" cellspacing="0" cellpadding="5" id="tbl09_Lbl" runat="server">
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
<a href="vendor_Declarations_Safety_etal.aspx<%= queryString %>" class="bt1"><span>BACK</span></a>
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