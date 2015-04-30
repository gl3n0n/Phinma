<%@ Page Title="" Language="C#" MasterPageFile="~/MasterSubPage.master" AutoEventWireup="true" CodeFile="vendor_03_businessOperational.aspx.cs" Inherits="vendor_03_businessOperational" %>
<%@ Register TagPrefix="Ava" TagName="Tabsnav" Src="usercontrols/tabs.ascx" %>
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


    <!--### UPLOADIFY ###-->
    <%--<script src="uploadify/jquery-1.4.2.min.js" type="text/javascript"></script>--%>
    <script src="uploadify/swfobject.js" type="text/javascript"></script>
    <script src="uploadify/jquery.uploadify.v2.1.4.js" type="text/javascript"></script>
    <script src="uploadify/jquery.uploadify.v2.1.4.min.js" type="text/javascript"></script>
    <link href="uploadify/uploadify.css" rel="stylesheet" type="text/css" />
    <!--### UPLOADIFY ENDS ###-->

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
<%@ Register TagPrefix="Ava" TagName="TopNav" Src="usercontrols/TopNav_vendor.ascx" %>
<%@ Register TagPrefix="Ava" TagName="StepNav" Src="usercontrols/StepNav_vendor.ascx" %>
<ava:topnav ID="TopNav1" runat="server" /><ava:stepnav ID="StepNav1" runat="server" />
<div style="margin:10px 0px; color:#333; font-size:18px; width:450px; float:left;">Business Operational</div>

<form id="formVendorInfo" runat="server">


<div class="separator1"></div>
<table width="100%" border="0" cellspacing="0" cellpadding="0">
  <tr>
    <td width="33%" valign="top">
<!--Business activities STARTS-->
<h3 style="margin:10px 0px;">1. Manpower Resources</h3>
<table width="0"  border="0" cellpadding="5" cellspacing="0" id="tbl01" runat="server">
  <tr>
    <td style="border-top: thin #CCC dotted; width: 100px"><strong>In Office</strong><div style="font-size:10px">Numeric only</div></td>
    <td style="border-top: thin #CCC dotted; width: 138px">
    <input type="text" name="manResOffice" id="manResOffice" class="integer" runat="server" onblur="$('#ContentPlaceHolder1_manResourceTotal').val(parseInt($('#ContentPlaceHolder1_manResOffice').val().replace(new RegExp(',', 'g'), '')) + parseInt($('#ContentPlaceHolder1_manResSales').val().replace(new RegExp(',', 'g'), ''))+ parseInt($('#ContentPlaceHolder1_manResEngr').val().replace(new RegExp(',', 'g'), '')) + parseInt($('#ContentPlaceHolder1_manResWare').val().replace(new RegExp(',', 'g'), '')) + parseInt($('#ContentPlaceHolder1_manResProd').val().replace(new RegExp(',', 'g'), '')) + parseInt($('#ContentPlaceHolder1_manResOthers').val().replace(new RegExp(',', 'g'), ''))  )" value="0" maxlength="9" />
    </td>
  </tr>

  <tr>
    <td style="border-top: thin #CCC dotted; width: 100px"><strong>In Sales and Marketing</strong><div style="font-size:10px">Numeric only</div></td>
    <td style="border-top: thin #CCC dotted;"><input type="text" name="manResSales" id="manResSales" class="integer" runat="server" onblur="$('#ContentPlaceHolder1_manResourceTotal').val(parseInt($('#ContentPlaceHolder1_manResSales').val().replace(new RegExp(',', 'g'), ''))
         + parseInt($('#ContentPlaceHolder1_manResOffice').val().replace(new RegExp(',', 'g'), ''))
        + parseInt($('#ContentPlaceHolder1_manResEngr').val().replace(new RegExp(',', 'g'), '')) 
        + parseInt($('#ContentPlaceHolder1_manResWare').val().replace(new RegExp(',', 'g'), '')) 
        + parseInt($('#ContentPlaceHolder1_manResProd').val().replace(new RegExp(',', 'g'), '')) 
        + parseInt($('#ContentPlaceHolder1_manResOthers').val().replace(new RegExp(',', 'g'), ''))  )" value="0" maxlength="9" />

    </td>
    </tr>


     <tr>
    <td style="border-top: thin #CCC dotted; width: 100px"><strong>In Engineering</strong><div style="font-size:10px">Numeric only</div></td>
    <td style="border-top: thin #CCC dotted; width: 138px">
    <input type="text" name="manResEngr" id="manResEngr" class="integer" runat="server" onblur="$('#ContentPlaceHolder1_manResourceTotal').val(parseInt($('#ContentPlaceHolder1_manResEngr').val().replace(new RegExp(',', 'g'), ''))  
        + parseInt($('#ContentPlaceHolder1_manResOffice').val().replace(new RegExp(',', 'g'), ''))
        + parseInt($('#ContentPlaceHolder1_manResSales').val().replace(new RegExp(',', 'g'), '')) 
        + parseInt($('#ContentPlaceHolder1_manResWare').val().replace(new RegExp(',', 'g'), ''))
        + parseInt($('#ContentPlaceHolder1_manResProd').val().replace(new RegExp(',', 'g'), '')) 
        + parseInt($('#ContentPlaceHolder1_manResOthers').val().replace(new RegExp(',', 'g'), ''))  )" value="0" maxlength="9" />
    </td>
  </tr>
     <tr>
    <td style="border-top: thin #CCC dotted; width: 100px"><strong>In Warehouse/ Plant</strong><div style="font-size:10px">Numeric only</div></td>
    <td style="border-top: thin #CCC dotted; width: 138px">
    <input type="text" name="manResWare" id="manResWare" class="integer" runat="server" onblur="$('#ContentPlaceHolder1_manResourceTotal').val(parseInt($('#ContentPlaceHolder1_manResWare').val().replace(new RegExp(',', 'g'), '')) 
        + parseInt($('#ContentPlaceHolder1_manResOffice').val().replace(new RegExp(',', 'g'), ''))
        + parseInt($('#ContentPlaceHolder1_manResSales').val().replace(new RegExp(',', 'g'), '')) 
        + parseInt($('#ContentPlaceHolder1_manResEngr').val().replace(new RegExp(',', 'g'), ''))
        + parseInt($('#ContentPlaceHolder1_manResProd').val().replace(new RegExp(',', 'g'), '')) 
        + parseInt($('#ContentPlaceHolder1_manResOthers').val().replace(new RegExp(',', 'g'), ''))  )" value="0" maxlength="9" />
    </td>
  </tr>
    <tr>
    <td style="border-top: thin #CCC dotted; width: 100px"><strong>In Production</strong><div style="font-size:10px">Numeric only</div></td>
    <td style="border-top: thin #CCC dotted; width: 138px">
    <input type="text" name="manResProd" id="manResProd" class="integer" runat="server" onblur="$('#ContentPlaceHolder1_manResourceTotal').val(parseInt($('#ContentPlaceHolder1_manResProd').val().replace(new RegExp(',', 'g'), '')) 
         + parseInt($('#ContentPlaceHolder1_manResOffice').val().replace(new RegExp(',', 'g'), ''))
        + parseInt($('#ContentPlaceHolder1_manResSales').val().replace(new RegExp(',', 'g'), '')) 
        + parseInt($('#ContentPlaceHolder1_manResEngr').val().replace(new RegExp(',', 'g'), ''))
        + parseInt($('#ContentPlaceHolder1_manResWare').val().replace(new RegExp(',', 'g'), '')) 
        + parseInt($('#ContentPlaceHolder1_manResOthers').val().replace(new RegExp(',', 'g'), ''))  )" value="0" maxlength="9" />
    </td>
  </tr>
    <tr>
    <td style="border-top: thin #CCC dotted; width: 100px"><strong>In Other Branches</strong><div style="font-size:10px">Numeric only</div></td>
    <td style="border-top: thin #CCC dotted; width: 138px">
    <input type="text" name="manResOthers" id="manResOthers" class="integer" runat="server" onblur="$('#ContentPlaceHolder1_manResourceTotal').val(parseInt($('#ContentPlaceHolder1_manResOthers').val().replace(new RegExp(',', 'g'), '')) 
        + parseInt($('#ContentPlaceHolder1_manResOffice').val().replace(new RegExp(',', 'g'), ''))
        + parseInt($('#ContentPlaceHolder1_manResSales').val().replace(new RegExp(',', 'g'), '')) 
        + parseInt($('#ContentPlaceHolder1_manResEngr').val().replace(new RegExp(',', 'g'), ''))
        + parseInt($('#ContentPlaceHolder1_manResWare').val().replace(new RegExp(',', 'g'), '')) 
        + parseInt($('#ContentPlaceHolder1_manResProd').val().replace(new RegExp(',', 'g'), ''))  )" value="0" maxlength="9" />
    </td>
  </tr>


  <tr>
    <td style="border-top: thin #CCC dotted; width: 100px"><strong>Total</strong><div style="font-size:10px">Numeric only</div></td>
    <td style="border-top: thin #CCC dotted;"><input type="text" name="manResourceTotal" id="manResourceTotal" class="integer" runat="server" readonly="readonly" style="color:#666;" value="0" /></td>
    </tr>
</table>
        
<table width="0"  border="0" cellpadding="5" cellspacing="0" id="tbl01_Lbl" runat="server">
  <tr>
    <td style="border-top: thin #CCC dotted; width: 76px"><strong>In Office</strong><div style="font-size:10px">Numeric only</div></td>
    <td style="border-top: thin #CCC dotted; width: 138px">
    <h3><asp:Label name="manResOffice" id="manResOffice_Lbl" runat="server"></asp:Label></h3></td>
  </tr>
  <tr>
    <td style="border-top: thin #CCC dotted;"><strong>In Sales and Marketing</strong><div style="font-size:10px">Numeric only</div></td>
    <td style="border-top: thin #CCC dotted;"><h3><asp:Label name="manResSales" id="manResSales_Lbl" runat="server"></asp:Label></h3></td>
    </tr>

   
     <tr>
    <td style="border-top: thin #CCC dotted;"><strong>In Engineering</strong><div style="font-size:10px">Numeric only</div></td>
    <td style="border-top: thin #CCC dotted;"><h3><asp:Label name="manResEngr" id="manResEngr_Lbl" runat="server"></asp:Label></h3></td>
    </tr>
     <tr>
    <td style="border-top: thin #CCC dotted;"><strong>In Warehouse/Plant</strong><div style="font-size:10px">Numeric only</div></td>
    <td style="border-top: thin #CCC dotted;"><h3><asp:Label name="manResWare" id="manResWare_Lbl" runat="server"></asp:Label></h3></td>
    </tr>
     <tr>
    <td style="border-top: thin #CCC dotted;"><strong>In Production</strong><div style="font-size:10px">Numeric only</div></td>
    <td style="border-top: thin #CCC dotted;"><h3><asp:Label name="manResProd" id="manResProd_Lbl" runat="server"></asp:Label></h3></td>
    </tr>
      <tr>
    <td style="border-top: thin #CCC dotted;"><strong>In Other Branches</strong><div style="font-size:10px">Numeric only</div></td>
    <td style="border-top: thin #CCC dotted;"><h3><asp:Label name="manResOthers" id="manResOthers_Lbl" runat="server"></asp:Label></h3></td>
    </tr>

  <tr>
    <td style="border-top: thin #CCC dotted;"><strong>Total</strong><div style="font-size:10px">Numeric only</div></td>
    <td style="border-top: thin #CCC dotted;"><h3><asp:Label name="manResourceTotal" id="manResourceTotal_Lbl" runat="server"></asp:Label></h3></td>
    </tr>
</table>
<br />


    </td>
    <td width="67%" valign="top">
 

<!--Business activities STARTS-->
<div style="display:none">
<h3 style="margin:10px 0px;">2. Benefits received</h3>
<table width="484"  border="0" cellpadding="5" cellspacing="0" id="tbl02" runat="server">
  <tr>
    <td style="border-top: thin #CCC dotted; width: 129px;"><strong>Pag-IBIG/HDMF</strong></td>
    <td style="border-top: thin #CCC dotted; width: 67px;">
        <asp:RadioButtonList ID="benefitsPagibig" runat="server" 
            RepeatDirection="Horizontal" RepeatLayout="Flow">
            <asp:ListItem Value="1">Yes</asp:ListItem>
            <asp:ListItem Value="0">No</asp:ListItem>
        </asp:RadioButtonList>
    </td>
    <td width="137" style="border-top: thin #CCC dotted;">
    <script type="text/javascript">
    // <![CDATA[
        $(document).ready(function () {
            $('#benefitsPagibigFileNameFile').uploadify({
                'uploader': 'uploadify/uploadify.swf',
                'script': 'upload.ashx',

                'cancelImg': 'uploadify/cancel.png',
                'auto': true,
                'multi': true,
                'fileDesc': 'Attach File',
                'fileExt': '*.jpg;*.png;*.gif;*bmp;*.jpeg;*.doc;*.docx;*.xls;*.xlsx;*.zip;*.rar;*.ppt;*.pdf',
                'queueSizeLimit': 5,
                'sizeLimit': 10000000,
                'folder': 'uploads/<%= Session["VendorId"] %>',
                'onComplete': function (event, queueID, fileObj, response, data) {
                    //alert(response);
                    $('.benefitsPagibigFileNameLbl').html('<a href="' + response + '" target="_blank">' + response + '</a>');
                    $('#ContentPlaceHolder1_benefitsPagibigFileName').attr('value', response);
                }
            });
        });
    // ]]>
    </script>
        <div style="float:left; width:30px;"><input id="benefitsPagibigFileNameFile" type="file"/></div> 
        <asp:Label ID="benefitsPagibigFileNameLbl" CssClass="benefitsPagibigFileNameLbl" runat="server" Text="" style="float:left; padding-top:3px; display:block"></asp:Label>
        <input id="benefitsPagibigFileName" name="benefitsPagibigFileName" runat="server" type="hidden" value="" />
        <div style="font-size:9px; clear:both;">(Max file size: 10 MB)</div>
        </td>
  </tr>
  <tr>
    <td style="border-top: thin #CCC dotted;"><strong>Philhealth</strong></td>
    <td style="border-top: thin #CCC dotted;">
    <asp:RadioButtonList ID="benefitsPHIC" runat="server" 
            RepeatDirection="Horizontal" RepeatLayout="Flow">
            <asp:ListItem Value="1">Yes</asp:ListItem>
            <asp:ListItem Value="0">No</asp:ListItem>
        </asp:RadioButtonList></td>
    <td style="border-top: thin #CCC dotted;">
    <script type="text/javascript">
    // <![CDATA[
        $(document).ready(function () {
            $('#benefitsPHICFileNameFile').uploadify({
                'uploader': 'uploadify/uploadify.swf',
                'script': 'upload.ashx',

                'cancelImg': 'uploadify/cancel.png',
                'auto': true,
                'multi': true,
                'fileDesc': 'Attach File',
                'fileExt': '*.jpg;*.png;*.gif;*bmp;*.jpeg;*.doc;*.docx;*.xls;*.xlsx;*.zip;*.rar;*.ppt;*.pdf',
                'queueSizeLimit': 5,
                'sizeLimit': 10000000,
                'folder': 'uploads/<%= Session["VendorId"] %>',
                'onComplete': function (event, queueID, fileObj, response, data) {
                    //alert(response);
                    $('.benefitsPHICFileNameLbl').html('<a href="' + response + '" target="_blank">' + response + '</a>');
                    $('#ContentPlaceHolder1_benefitsPHICFileName').attr('value', response);
                }
            });
        });
    // ]]>
    </script>
        <div style="float:left; width:30px;"><input id="benefitsPHICFileNameFile" type="file"/></div> 
        <asp:Label ID="benefitsPHICFileNameLbl" CssClass="benefitsPHICFileNameLbl" runat="server" Text="" style="float:left; padding-top:3px; display:block"></asp:Label>
        <input id="benefitsPHICFileName" name="benefitsPHICFileName" runat="server" type="hidden" value="" />
        <div style="font-size:9px; clear:both;">(Max file size: 10 MB)</div>
    </td>
    </tr>
  <tr>
    <td style="border-top: thin #CCC dotted; width: 129px;"><strong>SSS</strong></td>
    <td style="border-top: thin #CCC dotted; width: 67px;">
    <asp:RadioButtonList ID="benefitsSSS" runat="server" 
            RepeatDirection="Horizontal" RepeatLayout="Flow">
            <asp:ListItem Value="1">Yes</asp:ListItem>
            <asp:ListItem Value="0">No</asp:ListItem>
        </asp:RadioButtonList>  
    </td>
    <td style="border-top: thin #CCC dotted;">
    <script type="text/javascript">
    // <![CDATA[
        $(document).ready(function () {
            $('#benefitsSSSFileNameFile').uploadify({
                'uploader': 'uploadify/uploadify.swf',
                'script': 'upload.ashx',

                'cancelImg': 'uploadify/cancel.png',
                'auto': true,
                'multi': true,
                'fileDesc': 'Attach File',
                'fileExt': '*.jpg;*.png;*.gif;*bmp;*.jpeg;*.doc;*.docx;*.xls;*.xlsx;*.zip;*.rar;*.ppt;*.pdf',
                'queueSizeLimit': 5,
                'sizeLimit': 10000000,
                'folder': 'uploads/<%= Session["VendorId"] %>',
                'onComplete': function (event, queueID, fileObj, response, data) {
                    //alert(response);
                    $('.benefitsSSSFileNameLbl').html('<a href="' + response + '" target="_blank">' + response + '</a>');
                    $('#ContentPlaceHolder1_benefitsSSSFileName').attr('value', response);
                }
            });
        });
    // ]]>
    </script>
        <div style="float:left; width:30px;"><input id="benefitsSSSFileNameFile" type="file"/></div> 
        <asp:Label ID="benefitsSSSFileNameLbl" CssClass="benefitsSSSFileNameLbl" runat="server" Text="" style="float:left; padding-top:3px; display:block"></asp:Label>
        <input id="benefitsSSSFileName" name="benefitsSSSFileName" runat="server" type="hidden" value="" />
        <div style="font-size:9px; clear:both;">(Max file size: 10 MB)</div>
    </td>
  </tr>
  <tr>
    <td style="border-top: thin #CCC dotted;"><strong>13th Month Pay</strong></td>
    <td style="border-top: thin #CCC dotted;">
    <asp:RadioButtonList ID="benefits13th" runat="server" 
            RepeatDirection="Horizontal" RepeatLayout="Flow">
            <asp:ListItem Value="1">Yes</asp:ListItem>
            <asp:ListItem Value="0">No</asp:ListItem>
        </asp:RadioButtonList></td>
    <td style="border-top: thin #CCC dotted;">&nbsp;</td>
  </tr>
  <tr>
    <td style="border-top: thin #CCC dotted;"><strong>Other Medical Benefits</strong></td>
    <td style="border-top: thin #CCC dotted;">
    <asp:RadioButtonList ID="benefitsOtherMed" runat="server" 
            RepeatDirection="Horizontal" RepeatLayout="Flow">
            <asp:ListItem Value="1">Yes</asp:ListItem>
            <asp:ListItem Value="0">No</asp:ListItem>
        </asp:RadioButtonList></td>
    <td style="border-top: thin #CCC dotted;">&nbsp;</td>
  </tr>
  <tr>
    <td style="border-top: thin #CCC dotted;"><strong>Others</strong></td>
    <td style="border-top: thin #CCC dotted;"><input name="benefitsOthers" type="text" id="benefitsOthers" size="10" runat="server" /></td>
    <td style="border-top: thin #CCC dotted;">
    <script type="text/javascript">
    // <![CDATA[
    $(document).ready(function () {
        $('#benefitsOthersFileNameFile').uploadify({
            'uploader': 'uploadify/uploadify.swf',
            'script': 'upload.ashx',

            'cancelImg': 'uploadify/cancel.png',
            'auto': true,
            'multi': true,
            'fileDesc': 'Files',
            'fileExt': '*.jpg;*.png;*.gif;*bmp;*.jpeg;*.doc;*.docx;*.xls;*.xlsx;*.zip;*.rar;*.ppt;*.pdf',
            'queueSizeLimit': 5,
            'sizeLimit': 10000000,
            'folder': 'uploads/<%= Session["VendorId"] %>',
            'onComplete': function (event, queueID, fileObj, response, data) {
                //alert(response);
                $('.benefitsOthersFileNameLbl').html('<a href="' + response + '" target="_blank">' + response + '</a>');
                $('#ContentPlaceHolder1_benefitsOthersFileName').attr('value', response);
            }
        });
    });
    // ]]>
    </script>
        <div style="float:left; width:30px;"><input id="benefitsOthersFileNameFile" type="file"/></div> 
        <asp:Label ID="benefitsOthersFileNameLbl" CssClass="benefitsOthersFileNameLbl" runat="server" Text="" style="float:left; padding-top:3px; display:block"></asp:Label>
        <input id="benefitsOthersFileName" name="benefitsOthersFileName" runat="server" type="hidden" value="" />
        <div style="font-size:9px; clear:both;">(Max file size: 10 MB)</div>
        </td>
  </tr>
</table>

<table width="484"  border="0" cellpadding="5" cellspacing="0" id="tbl02_Lbl" runat="server">
  <tr>
    <td style="border-top: thin #CCC dotted; width: 129px;"><strong>Pag-IBIG/HDMF</strong></td>
    <td style="border-top: thin #CCC dotted; width: 67px;"><h3><asp:Label runat="server" ID="benefitsPagibig_Lbl"></asp:Label></h3></td>
    <td width="137" style="border-top: thin #CCC dotted;"><asp:Label ID="benefitsPagibigFileName_Lbl" runat="server"></asp:Label></td>
  </tr>
  <tr>
    <td style="border-top: thin #CCC dotted;"><strong>Philhealth</strong></td>
    <td style="border-top: thin #CCC dotted;"><h3><asp:Label runat="server" ID="benefitsPHIC_Lbl"></asp:Label></h3></td>
    <td style="border-top: thin #CCC dotted;"><asp:Label ID="benefitsPHICFileName_Lbl" runat="server"></asp:Label></td>
    </tr>
  <tr>
    <td style="border-top: thin #CCC dotted; width: 129px;"><strong>SSS</strong></td>
    <td style="border-top: thin #CCC dotted; width: 67px;"><h3><asp:Label runat="server" ID="benefitsSSS_Lbl"></asp:Label></h3></td>
    <td style="border-top: thin #CCC dotted;"><asp:Label ID="benefitsSSSFileName_Lbl" runat="server"></asp:Label></td>
  </tr>
  <tr>
    <td style="border-top: thin #CCC dotted;"><strong>13th Month Pay</strong></td>
    <td style="border-top: thin #CCC dotted;"><h3><asp:Label runat="server" ID="benefits13th_Lbl"></asp:Label></h3></td>
    <td style="border-top: thin #CCC dotted;">&nbsp;</td>
  </tr>
  <tr>
    <td style="border-top: thin #CCC dotted;"><strong>Other Medical Benefits</strong></td>
    <td style="border-top: thin #CCC dotted;"><h3><asp:Label runat="server" ID="benefitsOtherMed_Lbl"></asp:Label></h3></td>
    <td style="border-top: thin #CCC dotted;">&nbsp;</td>
  </tr>
  <tr>
    <td style="border-top: thin #CCC dotted;"><strong>Others</strong></td>
    <td style="border-top: thin #CCC dotted;"><h3><asp:Label runat="server" ID="benefitsOthers_Lbl"></asp:Label></h3></td>
    <td style="border-top: thin #CCC dotted;"><asp:Label ID="benefitsOthersFilename_Lbl" runat="server"></asp:Label></td>
  </tr>
</table>
<br />   
    
    </td>
  </tr>
</table>

<div style="display:none">
<!--Products/Services applied for accreditation STARTS-->
<div class="separator1"></div>
<h3 style="margin:10px 0px;">3. Background information on key personnel</h3>
<table width="100%" border="0" cellspacing="0" cellpadding="5" id="tbl03" runat="server">
  <tr>
    <td style="width: 138px">&nbsp;</td>
    <td><strong>CEO/PRESIDENT/GM</strong></td>
    <td><strong>CFO</strong></td>
    <td><strong>COO</strong></td>
    <td><strong>Production/ Technical Manager</strong></td>
    <td>&nbsp;</td>
    </tr>
  <tr>
    <td style="border-top: thin #CCC dotted;"><label>Name</label></td>
    <td style="border-top: thin #CCC dotted;"><input name="ceoName" type="text" id="ceoName" runat="server" /></td>
    <td style="border-top: thin #CCC dotted;"><input name="cfoName" type="text" id="cfoName" runat="server" /></td>
    <td style="border-top: thin #CCC dotted;"><input name="cooName" type="text" id="cooName" runat="server" /></td>
    <td style="border-top: thin #CCC dotted;"><input name="ptmName" type="text" id="ptmName" runat="server" /></td>
    <td>&nbsp;</td>
    </tr>
  <tr >
    <td style="border-top: thin #CCC dotted;"><label>Degree earned</label></td>
    <td style="border-top: thin #CCC dotted;"><input name="ceoDegreeEarned" type="text" id="ceoDegreeEarned" runat="server" /></td>
    <td style="border-top: thin #CCC dotted;"><input name="cfoDegreeEarned" type="text" id="cfoDegreeEarned" runat="server" /></td>
    <td style="border-top: thin #CCC dotted;"><input name="cooDegreeEarned" type="text" id="cooDegreeEarned" runat="server" /></td>
    <td style="border-top: thin #CCC dotted;"><input name="ptmDegreeEarned" type="text" id="ptmDegreeEarned" runat="server" /></td>
    <td>&nbsp;</td>
    </tr>
  <tr >
    <td style="border-top: thin #CCC dotted;"><label>Educational institution</label></td>
    <td style="border-top: thin #CCC dotted;"><input name="ceoEducInstitution" type="text" id="ceoEducInstitution" runat="server" /></td>
    <td style="border-top: thin #CCC dotted;"><input name="cfoEducInstitution" type="text" id="cfoEducInstitution" runat="server" /></td>
    <td style="border-top: thin #CCC dotted;"><input name="cooEducInstitution" type="text" id="cooEducInstitution" runat="server" /></td>
    <td style="border-top: thin #CCC dotted;"><input name="ptmEducInstitution" type="text" id="ptmEducInstitution" runat="server" /></td>
    <td>&nbsp;</td>
    </tr>
  <tr >
    <td style="border-top: thin #CCC dotted;"><label>Year graduated</label></td>
    <td style="border-top: thin #CCC dotted;"><input name="ceoYearGraduated" type="text" id="ceoYearGraduated" runat="server" /></td>
    <td style="border-top: thin #CCC dotted;"><input name="cfoYearGraduated" type="text" id="cfoYearGraduated" runat="server" /></td>
    <td style="border-top: thin #CCC dotted;"><input name="cooYearGraduated" type="text" id="cooYearGraduated" runat="server" /></td>
    <td style="border-top: thin #CCC dotted;"><input name="ptmYearGraduated" type="text" id="ptmYearGraduated" runat="server" /></td>
    <td>&nbsp;</td>
  </tr>
  <tr >
    <td style="border-top: thin #CCC dotted;"><label>Nationality</label></td>
    <td style="border-top: thin #CCC dotted;"><input name="ceoNationality" type="text" id="ceoNationality" runat="server" /></td>
    <td style="border-top: thin #CCC dotted;"><input name="cfoNationality" type="text" id="cfoNationality" runat="server" /></td>
    <td style="border-top: thin #CCC dotted;"><input name="cooNationality" type="text" id="cooNationality" runat="server" /></td>
    <td style="border-top: thin #CCC dotted;"><input name="ptmNationality" type="text" id="ptmNationality" runat="server" /></td>
    <td>&nbsp;</td>
  </tr>
  <tr >
    <td style="border-top: thin #CCC dotted;"><label>Age</label></td> 
    <td style="border-top: thin #CCC dotted;"><input name="ceoAge" type="text" id="ceoAge" runat="server" /></td>
    <td style="border-top: thin #CCC dotted;"><input name="cfoAge" type="text" id="cfoAge" runat="server" /></td>
    <td style="border-top: thin #CCC dotted;"><input name="cooAge" type="text" id="cooAge" runat="server" /></td>
    <td style="border-top: thin #CCC dotted;"><input name="ptmAge" type="text" id="ptmAge" runat="server" /></td>
    <td>&nbsp;</td>
  </tr>
  <tr >
    <td style="border-top: thin #CCC dotted;"><label>Past work experience/s</label></td>
    <td style="border-top: thin #CCC dotted;">
        <textarea id="ceoPastWorkExp" name="ceoPastWorkExp" runat="server" cols="17" rows="4" style="font-family:Arial; width:150px;"></textarea>
    </td>
    <td style="border-top: thin #CCC dotted;">
        <textarea id="cfoPastWorkExp" name="cfoPastWorkExp" runat="server" cols="17" rows="4" style="font-family:Arial; width:150px;"></textarea>
    </td>
    <td style="border-top: thin #CCC dotted;">
        <textarea id="cooPastWorkExp" name="cooPastWorkExp" runat="server" cols="17" rows="4" style="font-family:Arial; width:150px;"></textarea>
    </td>
    <td style="border-top: thin #CCC dotted;">
        <textarea id="ptmPastWorkExp" name="ptmPastWorkExp" runat="server" cols="17" rows="4" style="font-family:Arial; width:150px;"></textarea>
    </td>
    <td>&nbsp;</td>
  </tr>
  <tr >
    <td style="border-top: thin #CCC dotted;"><label>Curriculum Vitae</label></td>
    <td style="border-top: thin #CCC dotted;">
        <script type="text/javascript">
            // <![CDATA[
            $(document).ready(function () {
                $('#ceoCRFile').uploadify({
                    'uploader': 'uploadify/uploadify.swf',
                    'script': 'upload.ashx',

                    'cancelImg': 'uploadify/cancel.png',
                    'auto': true,
                    'multi': true,
                    'fileDesc': 'Files',
                    'fileExt': '*.jpg;*.png;*.gif;*bmp;*.jpeg;*.doc;*.docx;*.xls;*.xlsx;*.zip;*.rar;*.ppt;*.pdf',
                    'queueSizeLimit': 5,
                    'sizeLimit': 10000000,
                    'folder': 'uploads/<%= Session["VendorId"] %>',
                'onComplete': function (event, queueID, fileObj, response, data) {
                    //alert(response);
                    $('.ceoCRLbl').html('<a href="' + response + '" target="_blank">Attachment</a>');
                    $('#ContentPlaceHolder1_ceoCR').attr('value', response);
                }
            });
        });
        // ]]>
    </script>
        <div style="float:left; width:30px;"><input id="ceoCRFile" type="file"/></div> 
        <asp:Label ID="ceoCRLbl" CssClass="ceoCRLbl" runat="server" Text="Attachment" style="float:left; padding-top:3px; display:block"></asp:Label>
        <input id="ceoCR" name="ceoCR" runat="server" type="hidden" value="" />
        <div style="font-size:9px; clear:both;">(Max file size: 10 MB)</div>
    </td>
    <td style="border-top: thin #CCC dotted;">
        <script type="text/javascript">
            // <![CDATA[
            $(document).ready(function () {
                $('#cfoCRFile').uploadify({
                    'uploader': 'uploadify/uploadify.swf',
                    'script': 'upload.ashx',

                    'cancelImg': 'uploadify/cancel.png',
                    'auto': true,
                    'multi': true,
                    'fileDesc': 'Files',
                    'fileExt': '*.jpg;*.png;*.gif;*bmp;*.jpeg;*.doc;*.docx;*.xls;*.xlsx;*.zip;*.rar;*.ppt;*.pdf',
                    'queueSizeLimit': 5,
                    'sizeLimit': 10000000,
                    'folder': 'uploads/<%= Session["VendorId"] %>',
                    'onComplete': function (event, queueID, fileObj, response, data) {
                        //alert(response);
                        $('.cfoCRLbl').html('<a href="' + response + '" target="_blank">Attachment</a>');
                        $('#ContentPlaceHolder1_cfoCR').attr('value', response);
                    }
                });
            });
            // ]]>
    </script>
        <div style="float:left; width:30px;"><input id="cfoCRFile" type="file"/></div> 
        <asp:Label ID="cfoCRLbl" CssClass="cfoCRLbl" runat="server" Text="Attachment" style="float:left; padding-top:3px; display:block"></asp:Label>
        <input id="cfoCR" name="cfoCR" runat="server" type="hidden" value="" />
        <div style="font-size:9px; clear:both;">(Max file size: 10 MB)</div>
    </td>
    <td style="border-top: thin #CCC dotted;">
        <script type="text/javascript">
            // <![CDATA[
            $(document).ready(function () {
                $('#cooCRFile').uploadify({
                    'uploader': 'uploadify/uploadify.swf',
                    'script': 'upload.ashx',

                    'cancelImg': 'uploadify/cancel.png',
                    'auto': true,
                    'multi': true,
                    'fileDesc': 'Files',
                    'fileExt': '*.jpg;*.png;*.gif;*bmp;*.jpeg;*.doc;*.docx;*.xls;*.xlsx;*.zip;*.rar;*.ppt;*.pdf',
                    'queueSizeLimit': 5,
                    'sizeLimit': 10000000,
                    'folder': 'uploads/<%= Session["VendorId"] %>',
                    'onComplete': function (event, queueID, fileObj, response, data) {
                        //alert(response);
                        $('.cooCRLbl').html('<a href="' + response + '" target="_blank">Attachment</a>');
                        $('#ContentPlaceHolder1_cooCR').attr('value', response);
                    }
                });
            });
            // ]]>
    </script>
        <div style="float:left; width:30px;"><input id="cooCRFile" type="file"/></div> 
        <asp:Label ID="cooCRLbl" CssClass="cooCRLbl" runat="server" Text="Attachment" style="float:left; padding-top:3px; display:block"></asp:Label>
        <input id="cooCR" name="cooCR" runat="server" type="hidden" value="" />
        <div style="font-size:9px; clear:both;">(Max file size: 10 MB)</div>
    </td>
    <td style="border-top: thin #CCC dotted;">
        <script type="text/javascript">
            // <![CDATA[
            $(document).ready(function () {
                $('#ptmCRFile').uploadify({
                    'uploader': 'uploadify/uploadify.swf',
                    'script': 'upload.ashx',

                    'cancelImg': 'uploadify/cancel.png',
                    'auto': true,
                    'multi': true,
                    'fileDesc': 'Files',
                    'fileExt': '*.jpg;*.png;*.gif;*bmp;*.jpeg;*.doc;*.docx;*.xls;*.xlsx;*.zip;*.rar;*.ppt;*.pdf',
                    'queueSizeLimit': 5,
                    'sizeLimit': 10000000,
                    'folder': 'uploads/<%= Session["VendorId"] %>',
                    'onComplete': function (event, queueID, fileObj, response, data) {
                        //alert(response);
                        $('.ptmCRLbl').html('<a href="' + response + '" target="_blank">Attachment</a>');
                        $('#ContentPlaceHolder1_ptmCR').attr('value', response);
                    }
                });
            });
            // ]]>
    </script>
        <div style="float:left; width:30px;"><input id="ptmCRFile" type="file"/></div> 
        <asp:Label ID="ptmCRLbl" CssClass="ptmCRLbl" runat="server" Text="Attachment" style="float:left; padding-top:3px; display:block"></asp:Label>
        <input id="ptmCR" name="ptmCR" runat="server" type="hidden" value="" />
        <div style="font-size:9px; clear:both;">(Max file size: 10 MB)</div>
    </td>
    <td>
        
    </td>
  </tr>
</table>


<table width="100%" border="0" cellspacing="0" cellpadding="5" id="tbl03_Lbl" runat="server">
  <tr>
    <td style="width: 138px">&nbsp;</td>
    <td style="width: 170px"><strong>CEO/PRESIDENT/GM</strong></td>
    <td style="width: 170px"><strong>CFO</strong></td>
    <td style="width: 170px"><strong>COO</strong></td>
    <td style="width: 170px"><strong>Production/ Technical Manager</strong></td>
    </tr>
  <tr>
    <td style="border-top: thin #CCC dotted;"><label>Name</label></td>
    <td style="border-top: thin #CCC dotted;"><h3><asp:Label name="ceoName" id="ceoName_Lbl" runat="server" ></asp:Label></h3></td>
    <td style="border-top: thin #CCC dotted;"><h3><asp:Label name="cfoName" id="cfoName_Lbl" runat="server" ></asp:Label></h3></td>
    <td style="border-top: thin #CCC dotted;"><h3><asp:Label name="cooName" id="cooName_Lbl" runat="server" ></asp:Label></h3></td>
    <td style="border-top: thin #CCC dotted;"><h3><asp:Label name="ptmName" id="ptmName_Lbl" runat="server" ></asp:Label></h3></td>
    <td>&nbsp;</td>
    </tr>
  <tr >
    <td style="border-top: thin #CCC dotted;"><label>Degree earned</label></td>
    <td style="border-top: thin #CCC dotted;"><h3><asp:Label name="ceoDegreeEarned" id="ceoDegreeEarned_Lbl" runat="server" ></asp:Label></h3></td>
    <td style="border-top: thin #CCC dotted;"><h3><asp:Label name="cfoDegreeEarned" id="cfoDegreeEarned_Lbl" runat="server" ></asp:Label></h3></td>
    <td style="border-top: thin #CCC dotted;"><h3><asp:Label name="cooDegreeEarned" id="cooDegreeEarned_Lbl" runat="server" ></asp:Label></h3></td>
    <td style="border-top: thin #CCC dotted;"><h3><asp:Label name="ptmDegreeEarned" id="ptmDegreeEarned_Lbl" runat="server" ></asp:Label></h3></td>
    </tr>
  <tr >
    <td style="border-top: thin #CCC dotted;"><label>Educational institution</label></td>
    <td style="border-top: thin #CCC dotted;"><h3><asp:Label name="ceoEducInstitution" id="ceoEducInstitution_Lbl" runat="server" ></asp:Label></h3></td>
    <td style="border-top: thin #CCC dotted;"><h3><asp:Label name="cfoEducInstitution" id="cfoEducInstitution_Lbl" runat="server" ></asp:Label></h3></td>
    <td style="border-top: thin #CCC dotted;"><h3><asp:Label name="cooEducInstitution" id="cooEducInstitution_Lbl" runat="server" ></asp:Label></h3></td>
    <td style="border-top: thin #CCC dotted;"><h3><asp:Label name="ptmEducInstitution" id="ptmEducInstitution_Lbl" runat="server" ></asp:Label></h3></td>
    </tr>
  <tr >
    <td style="border-top: thin #CCC dotted;"><label>Year graduated</label></td>
    <td style="border-top: thin #CCC dotted;"><h3><asp:Label name="ceoYearGraduated" id="ceoYearGraduated_Lbl" runat="server" ></asp:Label></h3></td>
    <td style="border-top: thin #CCC dotted;"><h3><asp:Label name="cfoYearGraduated" id="cfoYearGraduated_Lbl" runat="server" ></asp:Label></h3></td>
    <td style="border-top: thin #CCC dotted;"><h3><asp:Label name="cooYearGraduated" id="cooYearGraduated_Lbl" runat="server" ></asp:Label></h3></td>
    <td style="border-top: thin #CCC dotted;"><h3><asp:Label name="ptmYearGraduated" id="ptmYearGraduated_Lbl" runat="server" ></asp:Label></h3></td>
  </tr>
  <tr >
    <td style="border-top: thin #CCC dotted;"><label>Nationality</label></td>
    <td style="border-top: thin #CCC dotted;"><h3><asp:Label name="ceoNationality" id="ceoNationality_Lbl" runat="server" ></asp:Label></h3></td>
    <td style="border-top: thin #CCC dotted;"><h3><asp:Label name="cfoNationality" id="cfoNationality_Lbl" runat="server" ></asp:Label></h3></td>
    <td style="border-top: thin #CCC dotted;"><h3><asp:Label name="cooNationality" id="cooNationality_Lbl" runat="server" ></asp:Label></h3></td>
    <td style="border-top: thin #CCC dotted;"><h3><asp:Label name="ptmNationality" id="ptmNationality_Lbl" runat="server" ></asp:Label></h3></td>
  </tr>
  <tr >
    <td style="border-top: thin #CCC dotted;"><label>Age</label></td> 
    <td style="border-top: thin #CCC dotted;"><h3><asp:Label name="ceoAge" id="ceoAge_Lbl" runat="server" ></asp:Label></h3></td>
    <td style="border-top: thin #CCC dotted;"><h3><asp:Label name="cfoAge" id="cfoAge_Lbl" runat="server" ></asp:Label></h3></td>
    <td style="border-top: thin #CCC dotted;"><h3><asp:Label name="cooAge" id="cooAge_Lbl" runat="server" ></asp:Label></h3></td>
    <td style="border-top: thin #CCC dotted;"><h3><asp:Label name="ptmAge" id="ptmAge_Lbl" runat="server" ></asp:Label></h3></td>
  </tr>
  <tr >
    <td style="border-top: thin #CCC dotted;"><label>Past work experience</label></td>
    <td style="border-top: thin #CCC dotted;"><h3><asp:Label name="ceoPastWorkExp" id="ceoPastWorkExp_Lbl" runat="server" ></asp:Label></h3></td>
    <td style="border-top: thin #CCC dotted;"><h3><asp:Label name="cfoPastWorkExp" id="cfoPastWorkExp_Lbl" runat="server" ></asp:Label></h3></td>
    <td style="border-top: thin #CCC dotted;"><h3><asp:Label name="cooPastWorkExp" id="cooPastWorkExp_Lbl" runat="server" ></asp:Label></h3></td>
    <td style="border-top: thin #CCC dotted;"><h3><asp:Label name="ptmPastWorkExp" id="ptmPastWorkExp_Lbl" runat="server" ></asp:Label></h3></td>
  </tr>
  <tr >
    <td style="border-top: thin #CCC dotted;"><label>Curriculum Vitae</label></td>
    <td style="border-top: thin #CCC dotted;"><asp:Label id="ceoCR_Lbl" runat="server" ></asp:Label></td>
    <td style="border-top: thin #CCC dotted;"><asp:Label id="cfoCR_Lbl" runat="server" ></asp:Label></td>
    <td style="border-top: thin #CCC dotted;"><asp:Label id="cooCR_Lbl" runat="server" ></asp:Label></td>
    <td style="border-top: thin #CCC dotted;"><asp:Label id="ptmCR_Lbl" runat="server" ></asp:Label></td>
  </tr>
</table>
<br />
</div>

    

<!--Business activities STARTS-->
<div class="separator1"></div>
<h3 style="margin:10px 0px;">2. Assets and Investments</h3>
<table width="0"  border="0" cellpadding="5" cellspacing="0" id="tbl04" runat="server">
  <tr>
    <td width="220px" style="border-top: thin #CCC dotted;"><strong>Machineries/Equipment/Vehicles</strong></td>
    <td style="border-top: thin #CCC dotted;">
    <script type="text/javascript">
    // <![CDATA[
        $(document).ready(function () {
            $('#fileupload3').uploadify({
                'uploader': 'uploadify/uploadify.swf',
                'script': 'upload.ashx',

                'cancelImg': 'uploadify/cancel.png',
                'auto': true,
                'multi': false,
                'fileDesc': 'Files',
                'fileExt': '*.jpg;*.png;*.gif;*bmp;*.jpeg;*.doc;*.docx;*.xls;*.xlsx;*.zip;*.rar;*.ppt;*.pdf',
                'queueSizeLimit': 5,
                'sizeLimit': 4000000,
                'folder': 'uploads/<%= Session["VendorId"] %>',
                'onComplete': function (event, queueID, fileObj, response, data) {
                    //alert(response);
                    $('.fileuploaded3').html('<a href="' + response + '" target="_blank">Attached file</a>');
                    $('#ContentPlaceHolder1_assetsMachineriesFileName').attr('value', response);
                }
            });
        });
    // ]]>
    </script>
        <div style="float:left; width:30px;"><input id="fileupload3" type="file"/></div> 
        <asp:Label ID="fileuploaded3" CssClass="fileuploaded3" runat="server" Text="" style="float:left; padding-top:3px; display:block"></asp:Label>
        <input id="assetsMachineriesFileName" name="assetsMachineriesFileName" runat="server" type="hidden" value="" />
        <div style="font-size:9px; clear:both;">(Single attachment, max file size: 4 MB)</div>
    </td>
  </tr>
  <tr>
    <td style="border-top: thin #CCC dotted;"><strong>Company Profile</strong></td>
    <td style="border-top: thin #CCC dotted;">
    <script type="text/javascript">
    // <![CDATA[
        $(document).ready(function () {
            $('#fileupload4').uploadify({
                'uploader': 'uploadify/uploadify.swf',
                'script': 'upload.ashx',

                'cancelImg': 'uploadify/cancel.png',
                'auto': true,
                'multi': false,
                'fileDesc': 'Files',
                'fileExt': '*.jpg;*.png;*.gif;*bmp;*.jpeg;*.doc;*.docx;*.xls;*.xlsx;*.zip;*.rar;*.ppt;*.pdf',
                'queueSizeLimit': 5,
                'sizeLimit': 4000000,
                'folder': 'uploads/<%= Session["VendorId"] %>',
                'onComplete': function (event, queueID, fileObj, response, data) {
                    //alert(response);
                    $('.fileuploaded4').html('<a href="' + response + '" target="_blank">Attached file</a>');
                    $('#ContentPlaceHolder1_assetsCompanyProfileFileName').attr('value', response);
                }
            });
        });
    // ]]>
    </script>
        <div style="float:left; width:30px;"><input id="fileupload4" type="file"/></div> 
        <asp:Label ID="fileuploaded4" CssClass="fileuploaded4" runat="server" Text="" style="float:left; padding-top:3px; display:block"></asp:Label>
        <input id="assetsCompanyProfileFileName" name="assetsCompanyProfileFileName" runat="server" type="hidden" value="" />
        <div style="font-size:9px; clear:both;">(Single attachment, max file size: 4 MB)</div>
    </td>
  </tr>
  <tr>
    <td style="border-top: thin #CCC dotted;"><strong>Others</strong></td>
    <td style="border-top: thin #CCC dotted;">
    <script type="text/javascript">
    // <![CDATA[
        $(document).ready(function () {
            $('#fileupload5').uploadify({
                'uploader': 'uploadify/uploadify.swf',
                'script': 'upload.ashx',

                'cancelImg': 'uploadify/cancel.png',
                'auto': true,
                'multi': false,
                'fileDesc': 'Files',
                'fileExt': '*.jpg;*.png;*.gif;*bmp;*.jpeg;*.doc;*.docx;*.xls;*.xlsx;*.zip;*.rar;*.ppt;*.pdf',
                'queueSizeLimit': 5,
                'sizeLimit': 10000000,
                'folder': 'uploads/<%= Session["VendorId"] %>',
                'onComplete': function (event, queueID, fileObj, response, data) {
                    //alert(response);
                    $('.fileuploaded5').html('<a href="' + response + '" target="_blank">Attached file</a>');
                    $('#ContentPlaceHolder1_assetsOthersFileName').attr('value', response);
                }
            });
        });
    // ]]>
    </script>
        <div style="float:left; width:30px;"><input id="fileupload5" type="file"/></div> 
        <asp:Label ID="fileuploaded5" CssClass="fileuploaded5" runat="server" Text="" style="float:left; padding-top:3px; display:block"></asp:Label>
        <input id="assetsOthersFileName" name="assetsOthersFileName" runat="server" type="hidden" value="" />
        <div style="font-size:9px; clear:both;">(Single attachment, max file size: 4 MB)</div>
        </td>
  </tr>
</table>

<table width="0"  border="0" cellpadding="5" cellspacing="0" id="tbl04_Lbl" runat="server">
  <tr>
    <td width="220px" style="border-top: thin #CCC dotted;"><strong>Machineries/Equipment/Vehicles</strong></td>
    <td style="border-top: thin #CCC dotted;"><asp:Label ID="assetsMachineriesFileName_Lbl" runat="server"></asp:Label></td>
  </tr>
  <tr>
    <td style="border-top: thin #CCC dotted;"><strong>Company Profile</strong></td>
    <td style="border-top: thin #CCC dotted;"><asp:Label ID="assetsCompanyProfileFileName_Lbl" runat="server"></asp:Label></td>
  </tr>
  <tr>
    <td style="border-top: thin #CCC dotted;"><strong>Others</strong></td>
    <td style="border-top: thin #CCC dotted;"><asp:Label ID="assetsOthersFileName_Lbl" runat="server"></asp:Label></td>
  </tr>
</table>
<br /> 

<table width="40%" border="0" cellspacing="0" cellpadding="5" id="aTable2">
  <tr>
    <td style="width: 138px; height: 27px;"><label><strong>Assets</strong></label></td>
    <td style="height: 27px"><label>Owned/Renting</label></td>
    <td valign="bottom" style="height: 27px"><input id="AssetsCounter" class="rowCount" name="AssetsCounter" type="hidden" /><a href="javascript:void(0)" value="Add Row" class="alternativeRow" runat="server" id="add6">+Add</a></td>
  </tr>
  <asp:Repeater ID="repeaterVendorAssets" runat="server" DataSourceID="dstblVendorAssets">
    <ItemTemplate>
  <tr>
    <td><input type="text" name="aAssets" id="aAssets" value="<%# Eval("Assets")%>" maxlength="50" /></td>
    <td>
        <select id="aOwned" name="aOwned" style="z-index:1000">
            <option>Select</option>
            <option value="Owned" <%# Eval("Owned").ToString() == "Owned" ? "selected='selected'" : ""%>>Owned</option>
            <option value="Renting to Own" <%# Eval("Owned").ToString() == "Renting to Own" ? "selected='selected'" : ""%>>Renting to Own</option>
            <option value="Rented" <%# Eval("Owned").ToString() == "Rented" ? "selected='selected'" : ""%>>Rented</option>
        </select>

    </td>
    <td valign="bottom"><input type="hidden" name="aAssetsId" id="aAssetsId" value="<%# Eval("ID")%>" /><img alt="" src="images/trash.png" width="9" height="13" border="0" class="delRow" /></td>
  </tr>
  </ItemTemplate>
  </asp:Repeater>
    
  <asp:Repeater ID="repeaterVendorAssets_Lbl" runat="server" DataSourceID="dstblVendorAssets">
    <ItemTemplate>
  <tr>
    <td><h3><asp:Label ID="aAssets_Lbl" runat="server" Text='<%# Eval("Assets")%>'></asp:Label></h3></td>
    <td><h3><asp:Label ID="aOwned_Lbl" runat="server" Text='<%# Eval("Owned")%>'></asp:Label></h3></td>
    <td valign="bottom">&nbsp;</td>
  </tr>
  </ItemTemplate>
  </asp:Repeater>
  </table>
  <asp:SqlDataSource ID="dstblVendorAssets" runat="server" ConnectionString="<%$ ConnectionStrings:AVAConnectionString %>"
            SelectCommand="IF EXISTS (SELECT 1 FROM tblVendorInformationAssets WHERE VendorId = @VendorId) BEGIN SELECT * FROM tblVendorInformationAssets WHERE VendorId = @VendorId END ELSE BEGIN SELECT 0 as ID, '' Assets,'' AS VendorId, '' AS Owned, '' AS DateCreated END" OnSelecting="rptGeneral_Selecting" >
    <SelectParameters>
        <asp:Parameter Type="Int32" Name="VendorId" />
	</SelectParameters>
    </asp:SqlDataSource>
<br />
<script type="text/javascript">
    $("document").ready(function () {
        $(".alternativeRow").btnAddRow({ inputBoxAutoNumber: true, inputBoxAutoId: true, displayRowCountTo: "rowCount" });
        $(".delRow").btnDelRow();
    });
</script>


    
<br /> 

<table width="40%" border="0" cellspacing="0" cellpadding="5" id="Table1">
  <tr>
    <td style="width: 138px"><strong>Facilities</strong></td>
    <%--<td style="display:none"><label for="subEquity">% Equity Held</label></td>--%>
    <td><label>Owned/Renting</label></td>
    <td valign="bottom"><input id="FacilitiesCounter" class="rowCount" name="FacilitiesCounter" type="hidden" /><a href="javascript:void(0)" value="Add Row" class="alternativeRow" runat="server" id="a1">+Add</a></td>
  </tr>
  <asp:Repeater ID="repeaterVendorFacilities" runat="server" DataSourceID="dstblVendorFacilities">
    <ItemTemplate>
  <tr>
    <td><input type="text" name="fFacilities" id="fFacilities" value="<%# Eval("Facilities")%>" maxlength="100" /></td>
    <td>
        <%--<input type="text" name="subOwned" id="subOwned" value="<%# Eval("subOwned")%>" />--%>
        <select id="fOwned" name="fOwned" style="z-index:1000">
            <option>Select</option>
            <option value="Owned" <%# Eval("Owned").ToString() == "Owned" ? "selected='selected'" : ""%>>Owned</option>
            <option value="Renting to Own" <%# Eval("Owned").ToString() == "Renting to Own" ? "selected='selected'" : ""%>>Renting to Own</option>
            <option value="Rented" <%# Eval("Owned").ToString() == "Rented" ? "selected='selected'" : ""%>>Rented</option>
        </select>

    </td>
    <td valign="bottom"><input type="hidden" name="fFacilitiesId" id="fFacilitiesId" value="<%# Eval("ID")%>" /><img alt="" src="images/trash.png" width="9" height="13" border="0" class="delRow" /></td>
  </tr>
  </ItemTemplate>
  </asp:Repeater>
    
  <asp:Repeater ID="repeaterVendorFacilities_Lbl" runat="server" DataSourceID="dstblVendorFacilities">
    <ItemTemplate>
  <tr>
    <td><h3><asp:Label ID="fFacilities_Lbl" runat="server" Text='<%# Eval("Facilities")%>'></asp:Label></h3></td>
    <td><h3><asp:Label ID="fOwned_Lbl" runat="server" Text='<%# Eval("Owned")%>'></asp:Label></h3></td>
    <td valign="bottom">&nbsp;</td>
  </tr>
  </ItemTemplate>
  </asp:Repeater>
  </table>
  <asp:SqlDataSource ID="dstblVendorFacilities" runat="server" ConnectionString="<%$ ConnectionStrings:AVAConnectionString %>"
            SelectCommand="IF EXISTS (SELECT 1 FROM tblVendorInformationFacilities WHERE VendorId = @VendorId) BEGIN SELECT * FROM tblVendorInformationFacilities WHERE VendorId = @VendorId END ELSE BEGIN SELECT 0 as ID, '' Facilities,'' AS VendorId, '' AS Owned, '' AS DateCreated END" OnSelecting="rptGeneral_Selecting" >
    <SelectParameters>
        <asp:Parameter Type="Int32" Name="VendorId" />
	</SelectParameters>
    </asp:SqlDataSource>
<br />
<script type="text/javascript">
    $("document").ready(function () {
        $(".alternativeRow").btnAddRow({ inputBoxAutoNumber: true, inputBoxAutoId: true, displayRowCountTo: "rowCount" });
        $(".delRow").btnDelRow();
    });
</script>






<span style="display:none">
<!--Business activities STARTS-->
<div class="separator1"></div>
<h3 style="margin:10px 0px;">Facilities</h3>
    <table cellpadding="5" id="tbl04a" runat="server">
        <tr>
            <td width="180" style="border-top: thin #CCC dotted;"><b>Land Area</b></td>
            <td width="220" style="border-top: thin #CCC dotted;"><input type="text" name="facltyLandTxt" id="facltyLandTxt" runat="server" value="" size="20" /></td>
            <td width="120" style="border-top: thin #CCC dotted;">
                <asp:DropDownList ID="facltyLandOwned" runat="server">
                    <asp:ListItem>Owned</asp:ListItem>
                    <asp:ListItem>Lease</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="border-top: thin #CCC dotted;"><b>Building Area</b></td>
            <td style="border-top: thin #CCC dotted;"><input type="text" name="facltyBldgTxt" id="facltyBldgTxt" runat="server" value="" size="20" /></td>
            <td style="border-top: thin #CCC dotted;">
                <asp:DropDownList ID="facltyBldgOwned" runat="server">
                    <asp:ListItem>Owned</asp:ListItem>
                    <asp:ListItem>Lease</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="border-top: thin #CCC dotted;"><b>Location</b></td>
            <td style="border-top: thin #CCC dotted;">
                <asp:DropDownList ID="facltyLocation" runat="server">
                    <asp:ListItem>Residential</asp:ListItem>
                    <asp:ListItem>Commercial</asp:ListItem>
                    <asp:ListItem>Industrial</asp:ListItem>
                    <asp:ListItem>Mixed</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td style="border-top: thin #CCC dotted;">&nbsp;</td>
        </tr>
        <tr>
            <td style="border-top: thin #CCC dotted;"><b>Premises used as</b></td>
            <td style="border-top: thin #CCC dotted;">
                <asp:DropDownList ID="facltyPremissesAs" runat="server">
                    <asp:ListItem>Head Office</asp:ListItem>
                    <asp:ListItem>Admin Office</asp:ListItem>
                    <asp:ListItem>Sales Office</asp:ListItem>
                    <asp:ListItem>Branch / Outlet</asp:ListItem>
                    <asp:ListItem>Plant</asp:ListItem>
                    <asp:ListItem>Warehouse</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td style="border-top: thin #CCC dotted;">&nbsp;</td>
        </tr>
    </table>

    <table cellpadding="5" id="tbl04a_Lbl" runat="server">
        <tr>
            <td width="180" style="border-top: thin #CCC dotted;"><b>Land Area</b></td>
            <td width="220" style="border-top: thin #CCC dotted;"><h3><asp:Label id="facltyLandTxt_Lbl" runat="server"></asp:Label></h3></td>
            <td width="120" style="border-top: thin #CCC dotted;"><h3><asp:Label id="facltyLandOwned_Lbl" runat="server"></asp:Label></h3></td>
        </tr>
        <tr>
            <td style="border-top: thin #CCC dotted;"><b>Building Area</b></td>
            <td style="border-top: thin #CCC dotted;"><h3><asp:Label id="facltyBldgTxt_Lbl" runat="server"></asp:Label></h3></td>
            <td style="border-top: thin #CCC dotted;"><h3><asp:Label id="facltyBldgOwned_Lbl" runat="server"></asp:Label></h3></td>
        </tr>
        <tr>
            <td style="border-top: thin #CCC dotted;"><b>Location</b></td>
            <td style="border-top: thin #CCC dotted;"><h3><asp:Label id="facltyLocation_Lbl" runat="server"></asp:Label></h3></td>
            <td style="border-top: thin #CCC dotted;">&nbsp;</td>
        </tr>
        <tr>
            <td style="border-top: thin #CCC dotted;"><b>Premises used as</b></td>
            <td style="border-top: thin #CCC dotted;"><h3><asp:Label id="facltyPremissesAs_Lbl" runat="server"></asp:Label></h3></td>
            <td style="border-top: thin #CCC dotted;">&nbsp;</td>
        </tr>
    </table>
    <br />
    </span>

<!--Business activities STARTS-->
<div class="separator1"></div>

<h3 style="margin:10px 0px;">3. Bank Information</h3>
<table width="100%" border="0" cellspacing="0" cellpadding="5">
  <tr>
    <td><label for="select3">Bank</label> </td>
    <td><label for="select4">Branch</label> </td>
      <td><label for="select4">Address</label> </td>
    <td><label for="textfield17">Account Type</label> </td>
      <td><label for="select4">Account Number</label> </td>
    <td><label for="textfield18">Contact No</label> </td>
    <td><label for="textfield18">Credit line certificate</label> </td>
    <td valign="bottom"><input id="BankInformationCounter" class="rowCount" value="3" name="BankInformationCounter" type="hidden" /><a href="javascript:void(0)" value="Add Row" class="alternativeRow" runat="server" id="add5" style="display:none;">+Add</a></td>
  </tr>
<asp:Repeater ID="repeaterBankInformation" runat="server" DataSourceID="dsVendorBankInformation">
<ItemTemplate>
  <tr>
    <td style="border-top: thin #CCC dotted;">
      <input type="text" name="biBranch" id="biBranch" value="<%# Eval("biBranch")%>" style="width:110px" maxlength="100" /></td>
    <td style="border-top: thin #CCC dotted;">
      <input type="text" name="biBankName" id="biBankName"  value="<%# Eval("biBankName")%>" style="width:110px" maxlength="100" /></td>
    <td style="border-top: thin #CCC dotted;">
        <input type="text" name="biBankAddress" id="biBankAddress" value="<%# Eval("biBankAddress")%>" style="width:110px" maxlength="100" /></td>
    <td style="border-top: thin #CCC dotted;">
        <input type="text" name="biAccountNo" id="biAccountNo" value="<%# Eval("biAccountNo")%>" style="width:110px" maxlength="100" /></td>
    <td style="border-top: thin #CCC dotted;">
      <input type="text" name="biAccountType" id="biAccountType"  value="<%# Eval("biAccountType")%>" style="width:110px" maxlength="60" /></td>
    <td style="border-top: thin #CCC dotted;">
      <input type="text" name="biContact" id="biContact"  value="<%# Eval("biContact") %>" style="width:110px" maxlength="100" /></td>
    <td style="border-top: thin #CCC dotted;">
        <div style="float:left; width:30px;"><input id="biAttachmentFile" runat="server" class="biAttachmentFile" type="file"/></div> 
        <asp:Label ID="biAttachmentLbl" CssClass="biAttachmentLbl" runat="server" Text='<%# Eval("biAttachment").ToString()!="" ? "<a href=\"" + Eval("biAttachment").ToString() + "\" target=\"_blank\">Attached file</a>" : "Attach file" %>' style="float:left; padding-top:3px; display:block"></asp:Label>
        <input id="biAttachment" name="biAttachment" type="hidden" value='<%# Eval("biAttachment") %>' />
        <div style="font-size:9px; clear:both;">(Single attachment, <br>max file size: 4 MB)</div>
    </td>
    <td style="border-top: thin #CCC dotted;display:none;" valign="bottom"><img alt="" src="images/trash.png" width="9" height="13" border="0" class="delRow" style="display:none;" /></td>
  </tr>
      </ItemTemplate>
</asp:Repeater>

<asp:Repeater ID="repeaterBankInformation_Lbl" runat="server" DataSourceID="dsVendorBankInformation">
<ItemTemplate>
  <tr>
    <td style="border-top: thin #CCC dotted;">
        <h3><asp:Label runat="server" ID="biBranch_Lbl" Text='<%# Eval("biBranch") %>'></asp:Label></h3>
    </td>
    <td style="border-top: thin #CCC dotted;">
        <h3><asp:Label runat="server" ID="biBankName_Lbl" Text='<%# Eval("biBankName") %>'></asp:Label></h3>
    </td>
      <td style="border-top: thin #CCC dotted;">
        <h3><asp:Label runat="server" ID="biBankAdd_Lbl" Text='<%# Eval("biBankAddress") %>'></asp:Label></h3>
    </td>
    <td style="border-top: thin #CCC dotted;">
        <h3><asp:Label runat="server" ID="biAccountType_Lbl" Text='<%# Eval("biAccountType") %>'></asp:Label></h3>
    </td>
      <td style="border-top: thin #CCC dotted;">
        <h3><asp:Label runat="server" ID="biAccountNo_Lbl" Text='<%# Eval("biAccountNo") %>'></asp:Label></h3>
    </td>
    <td style="border-top: thin #CCC dotted;">
        <h3><asp:Label runat="server" ID="biContact_Lbl" Text='<%# Eval("biContact") %>'></asp:Label></h3>
    </td>
    <td style="border-top: thin #CCC dotted;" valign="bottom"><%# Eval("biAttachment").ToString()!="" ? "<div style=\"float:left; width:30px;\"><img src=\"images/attachment.png\" /></div> <a href='" + Eval("biAttachment").ToString() + "' target='_blank'>Attached file</a>" : "<div style=\"float:left; width:30px;\"><img src=\"images/attachment.png\" /></div> No attached file" %></td>
  </tr>
      </ItemTemplate>
</asp:Repeater>
  </table>
  <asp:SqlDataSource ID="dsVendorBankInformation" runat="server" ConnectionString="<%$ ConnectionStrings:AVAConnectionString %>"
            SelectCommand="IF ((SELECT COUNT(*) FROM tblVendorBankInformation WHERE VendorId = @VendorId) >= 1 AND (SELECT COUNT(*) FROM tblVendorBankInformation WHERE VendorId = @VendorId) < 3) BEGIN SELECT BankId, VendorId, biBankName, biBranch, biBankAddress, biAccountType ,biAccountNo, biContact, DateCreated, biAttachment FROM tblVendorBankInformation WHERE VendorId = @VendorId UNION ALL SELECT 1 AS BankId, NULL AS VendorId, '' AS biBankName, '' AS biBranch, '' AS biBankAddress,'' AS biAccountType, '' AS biAccountNo, '' AS biContact, NULL AS DateCreated, '' AS biAttachment END ELSE IF ((SELECT COUNT(*) FROM tblVendorBankInformation WHERE VendorId = @VendorId) = 3) BEGIN SELECT BankId, VendorId, biBankName, biBranch, biBankAddress, biAccountType, biAccountNo, biAccountType, biContact, DateCreated, biAttachment FROM tblVendorBankInformation WHERE VendorId = @VendorId END ELSE BEGIN SELECT 1 AS BankId,'' AS VendorId, '' AS biBankName, '' AS biBranch,'' AS biBankAddress,'' AS biAccountType,'' AS biAccountNo, '' AS biAccountType, '' AS biContact, '' AS DateCreated, '' AS biAttachment UNION SELECT 2 AS BankId,'' AS VendorId, '' AS biBankName, '' AS biBranch,'' AS biBankAddress,'' AS biAccountType,'' AS biAccountNo, '' AS biAccountType, '' AS biContact, '' AS DateCreated, '' AS biAttachment UNION SELECT 3 AS BankId,'' AS VendorId, '' AS biBankName, '' AS biBranch,'' AS biBankAddress,'' AS biAccountType,'' AS biAccountNo, '' AS biAccountType, '' AS biContact, '' AS DateCreated, '' AS biAttachment END" OnSelecting="rptGeneral_Selecting"  >
      <%-- SelectCommand="IF ((SELECT COUNT(*) FROM tblDnbLegalReport WHERE VendorId = @VendorId) >= 1 AND (SELECT COUNT(*) FROM tblDnbLegalReport WHERE VendorId = @VendorId) < 3) BEGIN SELECT ID, VendorId, TypeOfCase, DateFiled, Attachment, DateCreated FROM tblDnbLegalReport WHERE VendorId = @VendorId UNION SELECT NULL as ID,  '' AS VendorId, '' AS TypeOfCase, '' AS DateFiled, '' AS Attachment, '' AS DateCreated END ELSE IF ((SELECT COUNT(*) FROM tblDnbLegalReport WHERE VendorId = @VendorId) = 3) BEGIN SELECT ID, VendorId, TypeOfCase, DateFiled, Attachment, DateCreated FROM tblDnbLegalReport WHERE VendorId = @VendorId END ELSE BEGIN SELECT 1 as ID, '' AS VendorId, '' AS TypeOfCase, '' AS DateFiled, '' AS Attachment, '' AS DateCreated UNION SELECT 2 as ID,  '' AS VendorId, '' AS TypeOfCase, '' AS DateFiled, '' AS Attachment, '' AS DateCreated UNION SELECT 3 as ID,  '' AS VendorId, '' AS TypeOfCase, '' AS DateFiled, '' AS Attachment, '' AS DateCreated END" --%>
    <SelectParameters>
        <asp:Parameter Name="VendorId" Type="Int32" />
	</SelectParameters>
  </asp:SqlDataSource>
<script type="text/javascript">
    $("document").ready(function () {
        $(".alternativeRow").btnAddRow({ inputBoxAutoNumber: true, inputBoxAutoId: true, displayRowCountTo: "rowCount" });
        $(".delRow").btnDelRow();
    });
</script>
    <script type="text/javascript">
        // <![CDATA[
        $(document).ready(function () {
            $('#ContentPlaceHolder1_repeaterBankInformation_biAttachmentFile_0').uploadify({
                'uploader': 'uploadify/uploadify.swf',
                'script': 'upload.ashx',

                'cancelImg': 'uploadify/cancel.png',
                'auto': true,
                'multi': true,
                'fileDesc': 'Files',
                'fileExt': '*.jpg;*.png;*.gif;*bmp;*.jpeg;*.doc;*.docx;*.xls;*.xlsx;*.zip;*.rar;*.ppt;*.pdf',
                'queueSizeLimit': 5,
                'sizeLimit': 4000000,
                'folder': 'uploads/<%= Session["VendorId"] %>',
            'onComplete': function (event, queueID, fileObj, response, data) {
                $('#ContentPlaceHolder1_repeaterBankInformation_biAttachmentLbl_0').html('<a href="' + response + '" target="_blank">Attached file</a>');
                $('#biAttachment0').attr('value', response);
            }
        });
        $('#ContentPlaceHolder1_repeaterBankInformation_biAttachmentFile_1').uploadify({
            'uploader': 'uploadify/uploadify.swf',
            'script': 'upload.ashx',

            'cancelImg': 'uploadify/cancel.png',
            'auto': true,
            'multi': true,
            'fileDesc': 'Files',
            'fileExt': '*.jpg;*.png;*.gif;*bmp;*.jpeg;*.doc;*.docx;*.xls;*.xlsx;*.zip;*.rar;*.ppt;*.pdf',
            'queueSizeLimit': 5,
            'sizeLimit': 4000000,
            'folder': 'uploads/<%= Session["VendorId"] %>',
            'onComplete': function (event, queueID, fileObj, response, data) {
                $('#ContentPlaceHolder1_repeaterBankInformation_biAttachmentLbl_0').html('<a href="' + response + '" target="_blank">Attached file</a>');
                $('#biAttachment1').attr('value', response);
            }
        });
        $('#ContentPlaceHolder1_repeaterBankInformation_biAttachmentFile_2').uploadify({
            'uploader': 'uploadify/uploadify.swf',
            'script': 'upload.ashx',

            'cancelImg': 'uploadify/cancel.png',
            'auto': true,
            'multi': true,
            'fileDesc': 'Files',
            'fileExt': '*.jpg;*.png;*.gif;*bmp;*.jpeg;*.doc;*.docx;*.xls;*.xlsx;*.zip;*.rar;*.ppt;*.pdf',
            'queueSizeLimit': 5,
            'sizeLimit': 4000000,
            'folder': 'uploads/<%= Session["VendorId"] %>',
            'onComplete': function (event, queueID, fileObj, response, data) {
                $('#ContentPlaceHolder1_repeaterBankInformation_biAttachmentLbl_1').html('<a href="' + response + '" target="_blank">Attached file</a>');
                $('#biAttachment2').attr('value', response);
            }
        });
        $('#ContentPlaceHolder1_repeaterBankInformation_biAttachmentFile_3').uploadify({
            'uploader': 'uploadify/uploadify.swf',
            'script': 'upload.ashx',

            'cancelImg': 'uploadify/cancel.png',
            'auto': true,
            'multi': true,
            'fileDesc': 'Files',
            'fileExt': '*.jpg;*.png;*.gif;*bmp;*.jpeg;*.doc;*.docx;*.xls;*.xlsx;*.zip;*.rar;*.ppt;*.pdf',
            'queueSizeLimit': 5,
            'sizeLimit': 4000000,
            'folder': 'uploads/<%= Session["VendorId"] %>',
            'onComplete': function (event, queueID, fileObj, response, data) {
                $('#ContentPlaceHolder1_repeaterBankInformation_biAttachmentLbl_2').html('<a href="' + response + '" target="_blank">Attached file</a>');
                $('#biAttachment3').attr('value', response);
            }
        });
        $('#ContentPlaceHolder1_repeaterBankInformation_biAttachmentFile_4').uploadify({
            'uploader': 'uploadify/uploadify.swf',
            'script': 'upload.ashx',

            'cancelImg': 'uploadify/cancel.png',
            'auto': true,
            'multi': true,
            'fileDesc': 'Files',
            'fileExt': '*.jpg;*.png;*.gif;*bmp;*.jpeg;*.doc;*.docx;*.xls;*.xlsx;*.zip;*.rar;*.ppt;*.pdf',
            'queueSizeLimit': 5,
            'sizeLimit': 4000000,
            'folder': 'uploads/<%= Session["VendorId"] %>',
            'onComplete': function (event, queueID, fileObj, response, data) {
                $('#ContentPlaceHolder1_repeaterBankInformation_biAttachmentLbl_3').html('<a href="' + response + '" target="_blank">Attached file</a>');
                $('#biAttachment4').attr('value', response);
            }
        });
    });
    // ]]>
</script>
<br />



    <div style="display:none">
<!--Business activities STARTS-->
<div class="separator1"></div>
<h3 style="margin:10px 0px;">6. Insurance Information</h3>


<table style="" cellpadding="10" id="tbl06" runat="server">
        <tr>
            <td style="height: 26px; width:220px">&nbsp;</td>
            <td style="height: 26px; width:220px"><label>Limit of Liability</label></td>
            <td style="height: 26px; width: 220px;"><label>Insurance Company</label></td>
        </tr>
        <tr>
            <td style="border-top: thin #CCC dotted;"><label>Employers Liability</label></td>
            <td style="border-top: thin #CCC dotted;"><input type="text" runat="server" name="insurInfoEmplyrLia_Limit" id="insurInfoEmplyrLia_Limit"  value="" size="20" /></td>
            <td style="border-top: thin #CCC dotted;">
                <input type="text" runat="server" name="insurInfoEmplyrLia_InsuCo" id="insurInfoEmplyrLia_InsuCo"  value="" size="20" /></td>
        </tr>
        <tr>
            <td style="border-top: thin #CCC dotted;"><label>Property Insurance</label></td>
            <td style="border-top: thin #CCC dotted;"><input type="text" runat="server" name="insurInfoPropInsu_Limit" id="insurInfoPropInsu_Limit"  value="" size="20" /></td>
            <td style="border-top: thin #CCC dotted;">
                <input type="text" runat="server" name="insurInfoPropInsu_InsuCo" id="insurInfoPropInsu_InsuCo"  value="" size="20" /></td>
        </tr>
        <tr>
            <td style="border-top: thin #CCC dotted;"><label>Third Party Liability</label></td>
            <td style="border-top: thin #CCC dotted;">
                <input type="text" runat="server" name="insurInfoPartyLia_Limit" id="insurInfoPartyLia_Limit"  value="" size="20" /></td>
            <td style="border-top: thin #CCC dotted;"><input type="text" runat="server" name="insurInfoPartyLia_InsuCo" id="insurInfoPartyLia_InsuCo"  value="" size="20" /></td>
        </tr>
        <tr>
            <td style="border-top: thin #CCC dotted;"><label>Others</label></td>
            <td style="border-top: thin #CCC dotted;">
                <input type="text" runat="server" name="insurInfoOthers_Limit" id="insurInfoOthers_Limit"  value="" size="20" /></td>
            <td style="border-top: thin #CCC dotted;"><input type="text" runat="server" name="insurInfoOthers_InsuCo" id="insurInfoOthers_InsuCo"  value="" size="20" /></td>
        </tr>
    </table>

    <table style="" cellpadding="10" id="tbl06_Lbl" runat="server">
        <tr>
            <td style="height: 26px; width: 220px">&nbsp;</td>
            <td style="height: 26px; width: 220px;"><label>Limit of Liability</label></td>
            <td style="height: 26px; width: 220px;"><label>Insurance Company</label></td>
        </tr>
        <tr>
            <td style="border-top: thin #CCC dotted;"><label>Employers Liability</label></td>
            <td style="border-top: thin #CCC dotted;">
                <h3><asp:Label runat="server" ID="insurInfoEmplyrLia_Limit_Lbl"></asp:Label></h3>
            </td>
            <td style="border-top: thin #CCC dotted;">
                <h3><asp:Label runat="server" ID="insurInfoEmplyrLia_InsuCo_Lbl"></asp:Label></h3>
            </td>
        </tr>
        <tr>
            <td style="border-top: thin #CCC dotted;"><label>Property Insurance</label></td>
            <td style="border-top: thin #CCC dotted;">
                <h3><asp:Label runat="server" ID="insurInfoPropInsu_Limit_Lbl"></asp:Label></h3>
            </td>
            <td style="border-top: thin #CCC dotted;">
                <h3><asp:Label runat="server" ID="insurInfoPropInsu_InsuCo_Lbl"></asp:Label></h3>
            </td>
        </tr>
        <tr>
            <td style="border-top: thin #CCC dotted;"><label>Party Liability</label></td>
            <td style="border-top: thin #CCC dotted;">
                <h3><asp:Label runat="server" ID="insurInfoPartyLia_Limit_Lbl"></asp:Label></h3>
            </td>
            <td style="border-top: thin #CCC dotted;">
                <h3><asp:Label runat="server" ID="insurInfoPartyLia_InsuCo_Lbl"></asp:Label></h3>
            </td>
        </tr>
        <tr>
            <td style="border-top: thin #CCC dotted;"><label>Others</label></td>
            <td style="border-top: thin #CCC dotted;">
                <h3><asp:Label runat="server" ID="insurInfoOthers_Limit_Lbl"></asp:Label></h3>
            </td>
            <td style="border-top: thin #CCC dotted;">
                <h3><asp:Label runat="server" ID="insurInfoOthers_InsuCo_Lbl"></asp:Label></h3>
            </td>
        </tr>
    </table>
</div>





<div class="separator1"></div>
<br />
<br />
<br />
<asp:LinkButton ID="createBt" runat="server" CssClass="bt1"  onclientclick="javascript: __doPostBack('continueStp', ''); return false;"><span>SAVE &amp; CONTINUE STEP 4 »</span></asp:LinkButton>
&nbsp;<asp:LinkButton ID="createBt1" runat="server" CssClass="bt1"  onclientclick="javascript: __doPostBack('justSave', ''); return false;"><span>SAVE</span></asp:LinkButton>
&nbsp;<a href="vendor_02_productServices.aspx<%= queryString %>" class="bt1"><span>BACK</span></a>
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

</asp:Content>