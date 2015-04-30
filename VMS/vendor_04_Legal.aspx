<%@ Page Title="" Language="C#" MasterPageFile="~/MasterSubPage.master" AutoEventWireup="true" CodeFile="vendor_04_Legal.aspx.cs" Inherits="vendor_04_Legal" %>
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

	<!--### DATEPICKER ###-->
    <link href="plugins/jquery-ui/css/smoothness/jquery-ui-1.9.2.custom.css" rel="stylesheet" />
    <script src="plugins/jquery-ui/js/jquery-ui-1.9.2.custom.js"></script>
     <script>
         $(function () {
             $(".date").datepicker({
                 changeMonth: true,
                 changeYear: true,
                 yearRange: '-150:-1',
                 dateFormat: 'm/d/yy'
             });
         });
    </script>
	<%--<script type="text/javascript" src="Scripts/cal.js"></script>
	<link rel="stylesheet" type="text/css" href="Styles/calendar_picker.css" />
	<script type="text/javascript">
	    function reloadDatepicker(id) {
	        $('#' + id).click();
	    }
	    jQuery(document).ready(function () {
	        $('input.date').simpleDatepicker({ startdate: '6/10/1900' });
	        $('input.date').keyup(function (e) {
	            //console.log('keyup called');
	            var code = e.keyCode || e.which;
	            if (code == '9') {
	                reloadDatepicker(this.id);
	                //alert(this.id);
	            }

	        });
	        $('input.date').blur(function () {
	            //alert(this.id);
	            //$(this.id).simpleDatepicker("hide");
	        });
	    });
	</script>  --%>
	<!--### DATEPICKER ENDS###-->

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
<div style="margin:10px 0px; color:#333; font-size:18px; width:450px; float:left;">Government Regulatory Information</div>

<form id="formVendorInfo" runat="server">


<div class="separator1"></div>
<h3 style="margin:10px 0px;">1. Legal structure</h3>
<table border="0" cellpadding="5" cellspacing="0" id="tbl01" runat="server">
  <tr>
	<td style="width: 150px"><strong>Organiztion type</strong></td>
	<td style="width: 138px"><select name="legalStrucOrgType" id="legalStrucOrgType" runat="server">
	  <option>Select</option>
	  <option value="Corporation">Corporation</option>
	  <option value="Proprietorship">Proprietorship</option>
	  <option value="Partnership">Partnership</option>
	  <option value="Foreign Brach">Foreign Branch</option>
	  <option value="Others">Others</option>
	</select></td>
	<td style="width: 550px; border-left:1px dotted #ccc; padding-left:10px;" rowspan="6">
		<table style="width: 100%;" cellpadding="5" id="tblSec01" runat="server">
						<tr>
							<td><b>SEC Registration (i.e. SEC Certificate, General Information Sheet, Articles of Incorporation/Partnership and By Laws)</b></td>
						</tr>
						<tr>
							<td>
								<script type="text/javascript">
									// <![CDATA[
									$(document).ready(function () {
										$('#legalStrucCorpAttch_geninfoFile').uploadify({
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
											    $('.legalStrucCorpAttch_geninfoLbl').html('<a href="' + response + '" target="_blank">General Information Sheet</a>');
												$('#ContentPlaceHolder1_legalStrucCorpAttch_geninfo').attr('value', response);
											}
										});
									});
									// ]]>
								</script>
								<div class="clearfix"></div>
									<div style="float:left; width:30px;"><input id="legalStrucCorpAttch_geninfoFile" type="file"/></div> 
									<asp:Label ID="legalStrucCorpAttch_geninfoLbl" CssClass="legalStrucCorpAttch_geninfoLbl" runat="server" Text="General Information Sheet" style="float:left; padding-top:3px; display:block"></asp:Label>
									<input id="legalStrucCorpAttch_geninfo" name="legalStrucCorpAttch_geninfo" runat="server" type="hidden" value="" />
                                <div style="font-size:9px; clear:both;">(Single attachment, max file size: 10 MB)</div>
								<div class="clearfix"></div>
							</td>
						</tr>
						
						<tr>
							<td>
								
								<script type="text/javascript">
									// <![CDATA[
									$(document).ready(function () {
										$('#legalStrucCorpAttch_geninfo2File').uploadify({
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
											    $('.legalStrucCorpAttch_geninfo2Lbl').html('<a href="' + response + '" target="_blank">SEC Certificate</a>');
												$('#ContentPlaceHolder1_legalStrucCorpAttch_geninfo2').attr('value', response);
											}
										});
									});
									// ]]>
								</script>
								<div class="clearfix"></div>
									<div style="float:left; width:30px;"><input id="legalStrucCorpAttch_geninfo2File" type="file"/></div> 
									<asp:Label ID="legalStrucCorpAttch_geninfo2Lbl" CssClass="legalStrucCorpAttch_geninfo2Lbl" runat="server" Text="SEC Certificate" style="float:left; padding-top:3px; display:block"></asp:Label>
									<input id="legalStrucCorpAttch_geninfo2" name="legalStrucCorpAttch_geninfo2" runat="server" type="hidden" value="" />
                                <div style="font-size:9px; clear:both;">(Single attachment, max file size: 10 MB)</div>
								<div class="clearfix"></div></td>
						</tr>
						
						<tr>
							<td><script type="text/javascript">
									// <![CDATA[
									$(document).ready(function () {
										$('#legalStrucCorpAttch_geninfo3File').uploadify({
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
											    $('.legalStrucCorpAttch_geninfo3Lbl').html('<a href="' + response + '" target="_blank">Articles of Incorporation/Partnership and By Laws</a>');
												$('#ContentPlaceHolder1_legalStrucCorpAttch_geninfo3').attr('value', response);
											}
										});
									});
									// ]]>
								</script>
								<div class="clearfix"></div>
									<div style="float:left; width:30px;"><input id="legalStrucCorpAttch_geninfo3File" type="file"/></div> 
									<asp:Label ID="legalStrucCorpAttch_geninfo3Lbl" CssClass="legalStrucCorpAttch_geninfo3Lbl" runat="server" Text="Articles of Incorporation/Partnership and By Laws" style="float:left; padding-top:3px; display:block"></asp:Label>
									<input id="legalStrucCorpAttch_geninfo3" name="legalStrucCorpAttch_geninfo3" runat="server" type="hidden" value="" />
                                <div style="font-size:9px; clear:both;">(Single attachment, max file size: 10 MB)</div>
								<div class="clearfix"></div></td>
						</tr>
						
						<tr>
							<td><script type="text/javascript">
									// <![CDATA[
									$(document).ready(function () {
										$('#legalStrucCorpAttch_geninfo4File').uploadify({
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
											    $('.legalStrucCorpAttch_geninfo4Lbl').html('<a href="' + response + '" target="_blank">Others</a>');
												$('#ContentPlaceHolder1_legalStrucCorpAttch_geninfo4').attr('value', response);
											}
										});
									});
									// ]]>
								</script>
								<div class="clearfix"></div>
									<div style="float:left; width:30px;"><input id="legalStrucCorpAttch_geninfo4File" type="file"/><input id="legalStrucCorpAttch_geninfo4Text" name="legalStrucCorpAttch_geninfo4Text" runat="server" type="text" value="" /></div> 
									<asp:Label ID="legalStrucCorpAttch_geninfo4Lbl" CssClass="legalStrucCorpAttch_geninfo4Lbl" runat="server" Text="Others" style="float:left; padding-top:3px; display:block"></asp:Label>
									<input id="legalStrucCorpAttch_geninfo4" name="legalStrucCorpAttch_geninfo4" runat="server" type="hidden" value="" />
                                <div style="font-size:9px; clear:both;">(Single attachment, max file size: 10 MB)</div>
								<div class="clearfix"></div>
								</td>
						</tr>
						
						<tr>
							<td><br /><br /><b>Competent evidence of identity</b></td>
						</tr>
						
						<tr>
							<td>
								<script type="text/javascript">
									// <![CDATA[
									$(document).ready(function () {
										$('#legalStrucCorpAttch_IdentityAuthorizdSignaFile').uploadify({
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
												$('.legalStrucCorpAttch_IdentityAuthorizdSignaLbl').html('<a href="' + response + '" target="_blank">Authorized signatories - Any government issued ID with picture</a>');
												$('#ContentPlaceHolder1_legalStrucCorpAttch_IdentityAuthorizdSigna').attr('value', response);
											}
										});
									});
									// ]]>
								</script>
								<div class="clearfix"></div>
									<div style="float:left; width:30px;"><input id="legalStrucCorpAttch_IdentityAuthorizdSignaFile" type="file"/></div> 
									<asp:Label ID="legalStrucCorpAttch_IdentityAuthorizdSignaLbl" CssClass="legalStrucCorpAttch_IdentityAuthorizdSignaLbl" runat="server" Text="Authorized signatories - Any government issued ID with picture" style="float:left; padding-top:3px; display:block"></asp:Label>
									<input id="legalStrucCorpAttch_IdentityAuthorizdSigna" name="legalStrucCorpAttch_IdentityAuthorizdSigna" runat="server" type="hidden" value="" />
                                <div style="font-size:9px; clear:both;">(Single attachment, max file size: 10 MB)</div>
								<div class="clearfix"></div>
							</td>
						</tr>
						<tr>
							<td>
								<script type="text/javascript">
									// <![CDATA[
									$(document).ready(function () {
										$('#legalStrucCorpAttch_IdentitytaxcertFile').uploadify({
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
												$('.legalStrucCorpAttch_IdentitytaxcertLbl').html('<a href="' + response + '" target="_blank">Company – Community Tax Certificate</a>');
												$('#ContentPlaceHolder1_legalStrucCorpAttch_Identitytaxcert').attr('value', response);
											}
										});
									});
									// ]]>
								</script>
								<div class="clearfix"></div>
									<div style="float:left; width:30px;"><input id="legalStrucCorpAttch_IdentitytaxcertFile" type="file"/></div> 
									<asp:Label ID="legalStrucCorpAttch_IdentitytaxcertLbl" CssClass="legalStrucCorpAttch_IdentitytaxcertLbl" runat="server" Text="Company – Community Tax Certificate" style="float:left; padding-top:3px; display:block"></asp:Label>
									<input id="legalStrucCorpAttch_Identitytaxcert" name="legalStrucCorpAttch_Identitytaxcert" runat="server" type="hidden" value="" />
                                <div style="font-size:9px; clear:both;">(Single attachment, max file size: 10 MB)</div>
								<div class="clearfix"></div>
							</td>
						</tr>

						<tr>
							<td><br /><br />
								<script type="text/javascript">
									// <![CDATA[
									$(document).ready(function () {
										$('#legalStrucCorpAttch_BoardAuthorizdSignaFile').uploadify({
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
												$('.legalStrucCorpAttch_BoardAuthorizdSignaLbl').html('<a href="' + response + '" target="_blank">Board Resolution / Secretary certificate of authorized signatories</a>');
												$('#ContentPlaceHolder1_legalStrucCorpAttch_BoardAuthorizdSigna').attr('value', response);
											}
										});
									});
									// ]]>
								</script>
								<div class="clearfix"></div>
									<div style="float:left; width:30px;"><input id="legalStrucCorpAttch_BoardAuthorizdSignaFile" type="file"/></div> 
									<asp:Label ID="legalStrucCorpAttch_BoardAuthorizdSignaLbl" CssClass="legalStrucCorpAttch_BoardAuthorizdSignaLbl" runat="server" Text="Board Resolution / Secretary certificate of authorized signatories" style="float:left; padding-top:3px; display:block; font-weight:bold"></asp:Label>
									<input id="legalStrucCorpAttch_BoardAuthorizdSigna" name="legalStrucCorpAttch_BoardAuthorizdSigna" runat="server" type="hidden" value="" />
                                <div style="font-size:9px; clear:both;">(Single attachment, max file size: 10 MB)</div>
								<div class="clearfix"></div>
							</td>
						</tr>
					</table>



		<table style="width: 100%;" cellpadding="5" id="tblSec02" runat="server">

						<tr>
							<td>
								<script type="text/javascript">
									// <![CDATA[
									$(document).ready(function () {
										$('#legalStrucSoleAttch_DTIRegFile').uploadify({
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
												$('.legalStrucSoleAttch_DTIRegLbl').html('<a href="' + response + '" target="_blank">DTI Registration – attach letter of certification from Proprietor stating registered / authorized capital</a>');
												$('#ContentPlaceHolder1_legalStrucSoleAttch_DTIReg').attr('value', response);
											}
										});
									});
									// ]]>
								</script>
								<div class="clearfix"></div>
									<div style="float:left; width:30px;"><input id="legalStrucSoleAttch_DTIRegFile" type="file"/></div> 
									<asp:Label ID="legalStrucSoleAttch_DTIRegLbl" CssClass="legalStrucSoleAttch_DTIRegLbl" runat="server" Text="DTI Registration – attach letter of certification from Proprietor stating registered / authorized capital" style="float:left; padding-top:3px; display:block; font-weight:bold;"></asp:Label>
									<input id="legalStrucSoleAttch_DTIReg" name="legalStrucSoleAttch_DTIReg" runat="server" type="hidden" value="" />
                                <div style="font-size:9px; clear:both;">(Single attachment, max file size: 10 MB)</div>
								<div class="clearfix"></div>
							</td>
						</tr>

						<tr>
							<td><br /><br />
								<script type="text/javascript">
									// <![CDATA[
									$(document).ready(function () {
										$('#legalStrucSoleAttch_OwnersId1File').uploadify({
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
												$('.legalStrucSoleAttch_OwnersId1Lbl').html('<a href="' + response + '" target="_blank">Owner’s Identification ** valid IDs include: Driver’s License, Passport or NBI Clearance</a>');
												$('#ContentPlaceHolder1_legalStrucSoleAttch_OwnersId1').attr('value', response);
											}
										});
									});
									// ]]>
								</script>
								<div class="clearfix"></div>
									<div style="float:left; width:30px;"><input id="legalStrucSoleAttch_OwnersId1File" type="file"/></div> 
									<asp:Label ID="legalStrucSoleAttch_OwnersId1Lbl" CssClass="legalStrucSoleAttch_OwnersId1Lbl" runat="server" Text="Owner’s Identification ** valid IDs include: Driver’s License, Passport or NBI Clearance" style="float:left; padding-top:3px; display:block; font-weight:bold"></asp:Label>
									<input id="legalStrucSoleAttch_OwnersId1" name="legalStrucSoleAttch_OwnersId1" runat="server" type="hidden" value="" />
                                <div style="font-size:9px; clear:both;">(Single attachment, max file size: 10 MB)</div>
								<div class="clearfix"></div>
							</td>
						</tr>
			
						<tr>
							<td><br /><br />
								<script type="text/javascript">
									// <![CDATA[
									$(document).ready(function () {
										$('#legalStrucSoleAttch_CTCFile').uploadify({
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
												$('.legalStrucSoleAttch_CTCLbl').html('<a href="' + response + '" target="_blank">Community Tax Certificate of the owner (CTC)</a>');
												$('#ContentPlaceHolder1_legalStrucSoleAttch_CTC').attr('value', response);
											}
										});
									});
									// ]]>
								</script>
								<div class="clearfix"></div>
									<div style="float:left; width:30px;"><input id="legalStrucSoleAttch_CTCFile" type="file"/></div> 
									<asp:Label ID="legalStrucSoleAttch_CTCLbl" CssClass="legalStrucSoleAttch_CTCLbl" runat="server" Text="Community Tax Certificate of the owner (CTC)" style="float:left; padding-top:3px; display:block; font-weight:bold"></asp:Label>
									<input id="legalStrucSoleAttch_CTC" name="legalStrucSoleAttch_CTC" runat="server" type="hidden" value="" />
                                <div style="font-size:9px; clear:both;">(Single attachment, max file size: 10 MB)</div>
								<div class="clearfix"></div>
							</td>
						</tr>
					</table>

				

		<script type="text/javascript">
			$(document).ready(function () {
				$("#ContentPlaceHolder1_legalStrucOrgType").change(function () {

					if ($("#ContentPlaceHolder1_legalStrucOrgType").val() == "Corporation" || $("#ContentPlaceHolder1_legalStrucOrgType").val() == "Partnership" || $("#ContentPlaceHolder1_legalStrucOrgType").val() == "Foreign Brach") {
						$("#ContentPlaceHolder1_tblSec01").show();
						$("#ContentPlaceHolder1_tblSec02").hide();
					}
					else if ($("#ContentPlaceHolder1_legalStrucOrgType").val() == "Select") {
					    $("#ContentPlaceHolder1_tblSec02").hide();
					    $("#ContentPlaceHolder1_tblSec01").hide();
					} else {
					    $("#ContentPlaceHolder1_tblSec02").show();
					    $("#ContentPlaceHolder1_tblSec01").hide();
					}
				});

				if ($("#ContentPlaceHolder1_legalStrucOrgType").val() == "Corporation" || $("#ContentPlaceHolder1_legalStrucOrgType").val() == "Partnership" || $("#ContentPlaceHolder1_legalStrucOrgType").val() == "Foreign Brach") {
						$("#ContentPlaceHolder1_tblSec01").show();
						$("#ContentPlaceHolder1_tblSec02").hide();
				} else if ($("#ContentPlaceHolder1_legalStrucOrgType").val() == "Select") {
				        $("#ContentPlaceHolder1_tblSec02").hide();
						$("#ContentPlaceHolder1_tblSec01").hide();
				}else {
						$("#ContentPlaceHolder1_tblSec02").show();
				        $("#ContentPlaceHolder1_tblSec01").hide();
			}
			});
		</script>
	</td>
  </tr>
  <tr>
	<td style="width: 150px"><strong>Date registered</strong><div style="font-size:10px">m/d/yyyy</div></td>
	<td style="width: 138px">
	  <input type="text" name="legalStrucDateReg" id="legalStrucDateReg" class="date" runat="server" title="m/d/yyyy" /></td>
  </tr>
  <tr>
	<td style="width: 150px"><strong>
	  <label>Registration number</label>
	</strong></td>
	<td style="width: 138px"><input name="legalStrucRegNo" type="text" id="legalStrucRegNo" runat="server" maxlength="100" /></td>
  </tr>
  <%--<tr>
	<td><strong>SEC Attachment</strong></td>
	<td style="width: 138px">
	<script type="text/javascript">
	// <![CDATA[
		$(document).ready(function () {
			$('#fileUpload1').uploadify({
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
					$('.fileuploaded1').html('<a href="' + response + '" target="_blank">' + response + '</a>');
					$('#ContentPlaceHolder1_legalStrucSECAttachement').attr('value', response);
				}
			});
		});
	// ]]>
	</script>
		<div style="float:left; width:30px;"><input id="fileUpload1" type="file"/></div> 
		<asp:Label ID="fileuploaded1" CssClass="fileuploaded1" runat="server" Text="" style="float:left; padding-top:3px; display:block"></asp:Label>
		<input id="legalStrucSECAttachement" name="legalStrucSECAttachement" runat="server" type="hidden" value="" />
		</td>
	<td style="width: 138px">
		&nbsp;</td>
	</tr>--%>
  <tr>
	<td style="width: 150px"><strong>Date started operations</strong><div style="font-size:10px">m/d/yyyy</div></td>
	<td style="width: 138px"><input type="text" name="legalStrucDateStartedOp" id="legalStrucDateStartedOp" class="date" runat="server" /></td>
  </tr>
  <tr>
	<td style="width: 150px"><strong>
	  <label>Previous business name</label>
	</strong></td>
	<td style="width: 138px"><input name="legalStrucPrevBusName" type="text" id="legalStrucPrevBusName"  runat="server" maxlength="100" /></td>
  </tr>
  <tr>
	<td style="width: 150px"><strong>Date of name change</strong><div style="font-size:10px">m/d/yyyy</div></td>
	<td style="width: 138px"><input type="text" name="legalStrucDateChanged" id="legalStrucDateChanged" class="date" runat="server" /></td>
  </tr>
  <%--<tr>
	<td><strong>
	  <label>Tax Identification number</label>
	</strong></td>
	<td><input name="legalStrucTIN" type="text" id="legalStrucTIN"  runat="server" /></td>
  </tr>--%>
</table><input name="legalStrucTIN" type="hidden" id="legalStrucTIN"  runat="server" />
<table border="0" cellpadding="5" cellspacing="0" id="tbl01_Lbl" runat="server">
  <tr>
	<td style="width: 138px"><strong>Organiztion type</strong></td>
	<td style="width: 138px"><h3><asp:Label runat="server" ID="legalStrucOrgType_Lbl"></asp:Label></h3></td>
	<td style="width: 550px" rowspan="6"><table style="width: 100%;" cellpadding="5" id="tblSec01_Lbl" runat="server">
						<tr>
							<td><b>SEC Registration (i.e. SEC Certificate, Articles of Incorporation/Partnership and By Laws)</b></td>
						</tr>
						<tr>
							<td>
								<div class="clearfix"></div>
									<asp:Label ID="legalStrucCorpAttch_geninfo_Lbl" CssClass="legalStrucCorpAttch_geninfo_Lbl" runat="server"></asp:Label>
								<div class="clearfix"></div>
									<asp:Label ID="legalStrucCorpAttch_geninfo2_Lbl" CssClass="legalStrucCorpAttch_geninfo2_Lbl" runat="server"></asp:Label>
								<div class="clearfix"></div>
									<asp:Label ID="legalStrucCorpAttch_geninfo3_Lbl" CssClass="legalStrucCorpAttch_geninfo3_Lbl" runat="server"></asp:Label>
								<div class="clearfix"></div>
									<asp:Label ID="legalStrucCorpAttch_geninfo4_Lbl" CssClass="legalStrucCorpAttch_geninfo4_Lbl" runat="server"></asp:Label>
								<div class="clearfix"></div>
									<asp:Label ID="legalStrucCorpAttch_geninfo4Txt_Lbl" CssClass="legalStrucCorpAttch_geninfo4Txt_Lbl" runat="server"></asp:Label>
								<div class="clearfix"></div>
							</td>
						</tr>
						
						<tr>
							<td><br /><br /><b>Competent evidence of identity</b></td>
						</tr>
						
						<tr>
							<td>
								<div class="clearfix"></div>
									<asp:Label ID="legalStrucCorpAttch_IdentityAuthorizdSigna_Lbl" CssClass="legalStrucCorpAttch_IdentityAuthorizdSigna_Lbl" runat="server"></asp:Label>
								<div class="clearfix"></div>
							</td>
						</tr>
						<tr>
							<td>
								<div class="clearfix"></div>
									<asp:Label ID="legalStrucCorpAttch_Identitytaxcert_Lbl" CssClass="legalStrucCorpAttch_Identitytaxcert_Lbl" runat="server"></asp:Label>
								<div class="clearfix"></div>
							</td>
						</tr>

						<tr>
							<td><br /><br />
								<div class="clearfix"></div>
									<asp:Label ID="legalStrucCorpAttch_BoardAuthorizdSigna_Lbl" CssClass="legalStrucCorpAttch_BoardAuthorizdSigna_Lbl" runat="server"></asp:Label>
								<div class="clearfix"></div>
							</td>
						</tr>
					</table>



		<table style="width: 100%;" cellpadding="5" id="tblSec02_Lbl" runat="server">

						<tr>
							<td>
								<div class="clearfix"></div>
									<asp:Label ID="legalStrucSoleAttch_DTIReg_Lbl" CssClass="legalStrucSoleAttch_DTIReg_Lbl" runat="server"></asp:Label>
								<div class="clearfix"></div>
							</td>
						</tr>

						<tr>
							<td><br /><br />
								<div class="clearfix"></div>
									<asp:Label ID="legalStrucSoleAttch_OwnersId1_Lbl" CssClass="legalStrucSoleAttch_OwnersId1_Lbl" runat="server"></asp:Label>
								<div class="clearfix"></div>
							</td>
						</tr>
			
						<tr>
							<td><br /><br />
								<div class="clearfix"></div>
									<asp:Label ID="legalStrucSoleAttch_CTC_Lbl" CssClass="legalStrucSoleAttch_CTC_Lbl" runat="server"></asp:Label>
								<div class="clearfix"></div>
							</td>
						</tr>
					</table>
		<script type="text/javascript">
			$(document).ready(function () {
				if ($("#ContentPlaceHolder1_legalStrucOrgType_Lbl").html() == "Corporation" || $("#ContentPlaceHolder1_legalStrucOrgType_Lbl").html() == "Partnership" || $("#ContentPlaceHolder1_legalStrucOrgType_Lbl").html() == "Foreign Brach") {
					$("#ContentPlaceHolder1_tblSec01_Lbl").show();
					$("#ContentPlaceHolder1_tblSec02_Lbl").hide();
					} else {
					$("#ContentPlaceHolder1_tblSec02_Lbl").show();
					$("#ContentPlaceHolder1_tblSec01_Lbl").hide();
					}
			});
		</script>
&nbsp;</td>
  </tr>
  <tr>
	<td><strong>Date registered</strong><div style="font-size:10px">dd/mm/yyyy</div></td>
	<td><h3><asp:Label runat="server" ID="legalStrucDateReg_Lbl"></asp:Label></h3></td>
  </tr>
  <tr>
	<td><strong>
	  <label>Registration number</label>
	</strong></td>
	<td><h3><asp:Label runat="server" ID="legalStrucRegNo_Lbl"></asp:Label></h3></td>
  </tr>
  <%--<tr>
	<td><strong>SEC Attachment</strong></td>
	<td><asp:Label runat="server" ID="legalStrucSECAttachement_Lbl"></asp:Label></td>
	</tr>--%>
  <tr>
	<td><strong>Date started operations</strong></td>
	<td><h3><asp:Label runat="server" ID="legalStrucDateStartedOp_Lbl"></asp:Label></h3></td>
  </tr>
  <tr>
	<td><strong>
	  <label>Previous business name</label>
	</strong></td>
	<td><h3><asp:Label runat="server" ID="legalStrucPrevBusName_Lbl"></asp:Label></h3></td>
  </tr>
  <tr>
	<td><strong>Date of name change</strong><div style="font-size:10px">dd/mm/yyyy</div></td>
	<td><h3><asp:Label runat="server" ID="legalStrucDateChanged_Lbl"></asp:Label></h3></td>
  </tr>
  <%--<tr>
	<td><strong>
	  <label>Tax Identification number</label>
	</strong></td>
	<td><h3><asp:Label runat="server" ID="legalStrucTIN_Lbl"></asp:Label></h3></td>
  </tr>--%>
</table><asp:Label runat="server" ID="legalStrucTIN_Lbl" style="display:none"></asp:Label>
<br />



<div class="separator1"></div>
<h3 style="margin:10px 0px;">2. Business permit</h3>
<table border="0" cellpadding="5" cellspacing="0" id="tbl02" runat="server">
  <tr>
	<td width="180"><strong>Date registered</strong><div style="font-size:10px">m/d/yyyy</div></td>
	<td><input type="text" name="busPermitDateReg" id="busPermitDateReg" class="date"  runat="server" /></td>
  </tr>
  <tr>
	<td><strong>
	  <label>Permit number</label>
	  </strong></td>
	<td><input name="busPermitNo" type="text" id="busPermitNo"  runat="server" maxlength="100" /></td>
  </tr>
  <tr>
	<td><strong>Business permit attachment</strong></td>
	<td>
	<script type="text/javascript">
	// <![CDATA[
		$(document).ready(function () {
			$('#fileupload2').uploadify({
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
					$('.fileuploaded2').html('<a href="' + response + '" target="_blank">Attached file</a>');
					$('#ContentPlaceHolder1_busPermitAttachement').attr('value', response);
				}
			});
		});
	// ]]>
	</script>
		<div style="float:left; width:30px;"><input id="fileupload2" type="file"/></div> 
		<asp:Label ID="fileuploaded2" CssClass="fileuploaded2" runat="server" Text="" style="float:left; padding-top:3px; display:block"></asp:Label>
		<input id="busPermitAttachement" name="busPermitAttachement" runat="server" type="hidden" value="" />
        <div style="font-size:9px; clear:both;">(Single attachment, max file size: 10 MB)</div>
	</td>
  </tr>
<tr>
	<td><strong>Other permit attachment</strong></td>
	<td><div class="clearfix"></div>
<script type="text/javascript">
    // <![CDATA[
    $(document).ready(function () {
        FieldFile = 'busPermitAttachementOthersFile';
        FieldLbl = 'busPermitAttachementOthersLbl';
        FieldHidden = 'busPermitAttachementOthers';
        $('#' + FieldFile + '_btup').hide();
        $('#ContentPlaceHolder1_' + FieldFile).uploadify({
            'uploader': 'uploadify/uploadify.swf',
            'script': 'upload.ashx',

            'cancelImg': 'uploadify/cancel.png',
            'auto': false,
            'multi': true,
            'fileDesc': 'Attach File',
            'fileExt': '*.jpg;*.png;*.gif;*bmp;*.jpeg;*.doc;*.docx;*.xls;*.xlsx;*.zip;*.rar;*.ppt;*.pdf',
            'queueSizeLimit': 5,
            'sizeLimit': 10000000,
            'folder': 'uploads/<%= Session["VendorId"] %>',
            'onSelect': function (event, ID, fileObj) {
                FieldFile = 'busPermitAttachementOthersFile';
                FieldLbl = 'busPermitAttachementOthersLbl';
                FieldHidden = 'busPermitAttachementOthers';
                $('#' + FieldFile + '_btup').show();
            },
            'onCancel': function (event, ID, fileObj, data) {
                FieldFile = 'busPermitAttachementOthersFile';
                FieldLbl = 'busPermitAttachementOthersLbl';
                FieldHidden = 'busPermitAttachementOthers';
                $('#' + FieldFile + '_btup').hide();
            },
            'onComplete': function (event, queueID, fileObj, response, data) {
                FieldFile = 'busPermitAttachementOthersFile';
                FieldLbl = 'busPermitAttachementOthersLbl';
                FieldHidden = 'busPermitAttachementOthers';
                //alert(response);
                $('#ContentPlaceHolder1_' + FieldLbl).append('<div><a href="' + response + '" target="_blank">Attached file</a> <img src=\"images/xicon.png\" style=\"margin-left:10px; padding-top:5px; \" id=\"' + FieldHidden + 'x\" onclick=\"$(this).parent(\'div\').html(\'\'); FileattchValues($(\'#ContentPlaceHolder1_' + FieldHidden + '\').val(), \'' + response + '\', \'' + FieldHidden + '\');\" /><br></div>');
                $('#ContentPlaceHolder1_' + FieldHidden).attr('value', $('#ContentPlaceHolder1_' + FieldHidden).val() + ';' + response);
                $('#' + FieldFile + '_btup').hide();
            }
        });
    });
    function FileattchValues(Str, StrRemove, FieldHidden1) {
        $('#ContentPlaceHolder1_' + FieldHidden1).attr('value', '');
        var myArray = Str.split(';');
        for (var i = 0; i < myArray.length; i++) {
            if (myArray[i] != StrRemove)
                if (myArray[i] != "") {
                    $('#ContentPlaceHolder1_' + FieldHidden1).attr('value', $('#ContentPlaceHolder1_' + FieldHidden1).attr('value') + ';' + myArray[i]);
                }
        } //alert($('#ContentPlaceHolder1_'+FieldHidden1).attr('value'));
    }
    // ]]>
    </script>
        <div style="float:left; width:30px;"><input id="busPermitAttachementOthersFile"  class="busPermitAttachementOthersFile" type="file" runat="server"/></div> 
        <asp:Label ID="busPermitAttachementOthersLbl" CssClass="busPermitAttachementOthersLbl" runat="server" Text="File Attachment" style="float:left; padding-top:3px; display:block"></asp:Label>
        <input id="busPermitAttachementOthers" name="busPermitAttachementOthers" runat="server" type="hidden" value="" />
        <div style="font-size:9px; clear:both;">(Max size each file: 10 MB), click paperclip again to upload multiple files. </div>
        <a href="javascript:$('#<%= busPermitAttachementOthersFile.ClientID %>').uploadifyUpload();" id="busPermitAttachementOthersFile_btup">Upload Selection</a>
        <div class="clearfix"></div>
	</td>
  </tr>
</table>
	
<table border="0" cellpadding="5" cellspacing="0" id="tbl02_Lbl" runat="server">
  <tr>
	<td width="180"><strong>Date registered</strong><div style="font-size:10px">dd/dd/mm/yyyy</div></td>
	<td><h3><asp:Label runat="server" ID="busPermitDateReg_Lbl"></asp:Label></h3></td>
  </tr>
  <tr>
	<td style="height: 33px"><strong>
	  <label>Permit number</label>
	  </strong></td>
	<td style="height: 33px"><h3><asp:Label runat="server" ID="busPermitNo_Lbl"></asp:Label></h3></td>
  </tr>
  <tr>
	<td><strong>Business permit attachment</strong></td>
	<td><h3><asp:Label runat="server" ID="busPermitAttachement_Lbl"></asp:Label></h3></td>
  </tr>
  <tr>
	<td><strong>Other permit attachment</strong></td>
	<td><h3><asp:Label runat="server" ID="busPermitAttachementOthers_Lbl"></asp:Label></h3></td>
  </tr>
</table>
<br />



<div class="separator1"></div>
<h3 style="margin:10px 0px;">3. BIR Registration</h3>

<table border="0" cellpadding="5" cellspacing="0" runat="server" id="tbl03">
	<tbody>
  <tr>
	<td style="width: 180px"><strong>BIR Registration, Sample of Invoice and Official Receipt</strong></td>
	<td >
        <div class="clearfix"></div>
<script type="text/javascript">
    // <![CDATA[
    $(document).ready(function () {
        FieldFile = 'birRegAttachementFile';
        FieldLbl = 'birRegAttachementLbl';
        FieldHidden = 'birRegAttachement';
        $('#' + FieldFile + '_btup').hide();
        $('#ContentPlaceHolder1_' + FieldFile).uploadify({
            'uploader': 'uploadify/uploadify.swf',
            'script': 'upload.ashx',

            'cancelImg': 'uploadify/cancel.png',
            'auto': false,
            'multi': true,
            'fileDesc': 'Attach File',
            'fileExt': '*.jpg;*.png;*.gif;*bmp;*.jpeg;*.doc;*.docx;*.xls;*.xlsx;*.zip;*.rar;*.ppt;*.pdf',
            'queueSizeLimit': 5,
            'sizeLimit': 10000000,
            'folder': 'uploads/<%= Session["VendorId"] %>',
            'onSelect': function (event, ID, fileObj) {
                FieldFile = 'birRegAttachementFile';
                FieldLbl = 'birRegAttachementLbl';
                FieldHidden = 'birRegAttachement';
                $('#' + FieldFile + '_btup').show();
            },
            'onCancel': function (event, ID, fileObj, data) {
                FieldFile = 'birRegAttachementFile';
                FieldLbl = 'birRegAttachementLbl';
                FieldHidden = 'birRegAttachement';
                $('#' + FieldFile + '_btup').hide();
            },
            'onComplete': function (event, queueID, fileObj, response, data) {
                FieldFile = 'birRegAttachementFile';
                FieldLbl = 'birRegAttachementLbl';
                FieldHidden = 'birRegAttachement';
                //alert(response);
                $('#ContentPlaceHolder1_' + FieldLbl).append('<div><a href="' + response + '" target="_blank">Attached file</a> <img src=\"images/xicon.png\" style=\"margin-left:10px; padding-top:5px; \" id=\"' + FieldHidden + 'x\" onclick=\"$(this).parent(\'div\').html(\'\'); FileattchValues($(\'#ContentPlaceHolder1_' + FieldHidden + '\').val(), \'' + response + '\', \'' + FieldHidden + '\');\" /><br></div>');
                $('#ContentPlaceHolder1_' + FieldHidden).attr('value', $('#ContentPlaceHolder1_' + FieldHidden).val() + ';' + response);
                $('#' + FieldFile + '_btup').hide();
            }
        });
    });
    function FileattchValues(Str, StrRemove, FieldHidden1) {
        $('#ContentPlaceHolder1_' + FieldHidden1).attr('value', '');
        var myArray = Str.split(';');
        for (var i = 0; i < myArray.length; i++) {
            if (myArray[i] != StrRemove)
                if (myArray[i] != "") {
                    $('#ContentPlaceHolder1_' + FieldHidden1).attr('value', $('#ContentPlaceHolder1_' + FieldHidden1).attr('value') + ';' + myArray[i]);
                }
        } //alert($('#ContentPlaceHolder1_'+FieldHidden1).attr('value'));
    }
    // ]]>
    </script>
        <div style="float:left; width:30px;"><input id="birRegAttachementFile"  class="birRegAttachementFile" type="file" runat="server"/></div> 
        <asp:Label ID="birRegAttachementLbl" CssClass="birRegAttachementLbl" runat="server" Text="File Attachment" style="float:left; padding-top:3px; display:block"></asp:Label>
        <input id="birRegAttachement" name="birRegAttachement" runat="server" type="hidden" value="" />
        <div style="font-size:9px; clear:both;">(Max size each file: 10 MB), click paperclip again to upload multiple files. </div>
        <a href="javascript:$('#<%= birRegAttachementFile.ClientID %>').uploadifyUpload();" id="birRegAttachementFile_btup">Upload Selection</a>
        <div class="clearfix"></div>
	</td>
  </tr>
  <tr>
	<td style="width: 180px"><strong>Tax Identification number</strong></td>
	<td><input name="birRegTIN" type="text" id="birRegTIN"  runat="server" maxlength="100" /> <select name="birRegTINVAT" id="birRegTINVAT" runat="server">
	  <option>Select</option>
	  <option value="VAT">VAT</option>
	  <option value="Non-Vat">Non-Vat</option>
      <option value="Non-Vat">Zero Rated</option>
	  
	</select></td>
<%--      <td style="width: 138px"></td>--%>

  </tr>

	</tbody>
</table>
<table border="0" cellpadding="5" cellspacing="0" id="tbl03_Lbl" runat="server">
	<tbody>
  <tr>
	<td style="height: 27px; width: 180px"><strong>BIR Registration, Sample of Invoice and Official Receipt</strong></td>
	<td style="height: 27px; width: 229px;" ><h3><asp:Label runat="server" ID="birRegAttachement_Lbl"></asp:Label></h3></td>
  </tr>
  <tr>
	<td style="width: 180px"><strong>Tax Identification number</strong></td>
	<td style="width: 229px"><h3><asp:Label runat="server" ID="birRegTIN_Lbl"></asp:Label>&nbsp;&nbsp;&nbsp;<asp:Label runat="server" ID="birRegTINVAT_Lbl"></asp:Label></h3></td>
  </tr>

	</tbody>
</table>
<br />



<div style="display:none">
<!--Products/Services applied for accreditation STARTS-->
<div class="separator1"></div>
<h3 style="margin:10px 0px; float:left">4. For Corporation and Partnership</h3>
	
	<div class="clearfix"></div>
	<table cellpadding="5" id="tbl04a" runat="server">
		<tr>
			<td><strong>As of</strong><div style="font-size:10px">dd/mm/yyyy</div></td>
			<td style="width: 136px"><input type="text" name="corpAsOfDate" id="corpAsOfDate" class="date" runat="server" /></td>
		</tr>
		<tr>
			<td>&nbsp;</td>
			<td style="width: 136px">&nbsp;</td>
		</tr>
	</table>    
	<table cellpadding="5" id="tbl04a_Lbl" runat="server">
		<tr>
			<td><strong>As of</strong><div style="font-size:10px">dd/mm/yyyy</div></td>
			<td style="width: 136px"><h3><asp:Label id="corpAsOfDate_Lbl" runat="server" ></asp:Label></h3></td>
		</tr>
		<tr>
			<td>&nbsp;</td>
			<td style="width: 136px">&nbsp;</td>
		</tr>
	</table>
	<div class="clearfix"></div>

<table width="75%" border="0" cellspacing="0" cellpadding="5">
  <tr>
	<th style="width:180px;"><strong>Shareholders/Partners</strong></th>
	<th style="width:180px;"><strong>Nationality</strong></th>
	<th style="width:180px;"><strong>Subscribed capital</strong><div style="font-size:10px">Numeric only</div></th>
	<th style="width:180px;"><strong>Paid Up capital</strong><div style="font-size:10px">Numeric only</div></th>
	<th style="width:40px;"><input type="hidden" name="ShareHoldersCounter" id="ShareHoldersCounter" class="rowCount" /><a href="javascript:void(0)" value="Add Row" class="alternativeRow" runat="server" id="add4a">+Add</a></th>
	</tr>
	<tbody>
	<asp:Repeater ID="repeaterShareHolders" runat="server" DataSourceID="dsVendorShareHolders">
<ItemTemplate>  
  <tr>
	<td><input name="shShareHolderName" type="text" id="shShareHolderName" value="<%# Eval("shShareHolderName") %>"  /></td>
	<td><input name="shNationality" type="text" id="shNationality" value="<%# Eval("shNationality") %>" /></td>
	<td><input name="shSubsribedCapital" type="text" id="shSubsribedCapital" class="numeric SubsribedCapital" onfocus="reloadNumeric()" onblur="totalSubsribedCapital(); reloadNumeric();" value="<%# Eval("shSubsribedCapital") %>" /></td>
	<td align="left"><input name="shPaidupCapital" type="text" id="shPaidupCapital" class="numeric PaidupCapital" onfocus="reloadNumeric()" onblur="totalPaidupCapital(); reloadNumeric();" value="<%# Eval("shPaidupCapital") %>" /></td>
	<td valign="bottom"><img src="images/trash.png" width="9" height="13" border="0" class="delRow" /></td>
	</tr>
   </ItemTemplate>
</asp:Repeater>

	<asp:Repeater ID="repeaterShareHolders_Lbl" runat="server" DataSourceID="dsVendorShareHolders">
<ItemTemplate>  
  <tr>
	<td><h3><asp:Label runat="server" ID="shShareHolderName_Lbl" Text='<%# Eval("shShareHolderName") %>'></asp:Label></h3></td>
	<td><h3><asp:Label runat="server" ID="shNationality_Lbl" Text='<%# Eval("shNationality") %>'></asp:Label></h3></td>
	<td><h3><asp:Label runat="server" ID="shSubsribedCapital_Lbl" Text='<%# Eval("shSubsribedCapital") %>'></asp:Label></h3></td>
	<td><h3><asp:Label runat="server" ID="shPaidupCapital_Lbl" Text='<%# Eval("shPaidupCapital") %>'></asp:Label></h3></td>
	<td valign="bottom">&nbsp;</td>
	</tr>
   </ItemTemplate>
</asp:Repeater>
		</tbody>
  </table>

    

	<table width="75%" border="0" cellspacing="0" cellpadding="5" id="tbl04" runat="server">
		<tr>
			<td width="155">&nbsp;</td>
			<td width="150"><b>Total</b></td>
			<td width="155"><input name="corpSubscribedCapital" type="text" id="corpSubscribedCapital" runat="server" class="numeric"  /></td>
			<td width="150"><input name="corpPaidUpCapital" type="text" id="corpPaidUpCapital" runat="server" class="numeric"  /></td>
			<td><a href="javascript:void(0)" style="display:none;">+Add</a></td>
		</tr>
		<tr>
			<td>&nbsp;</td>
			<td><b>Authorized capital</b></td>
			<td><input name="corpAuthorizedCapital" type="text" id="corpAuthorizedCapital" runat="server" class="numeric" /></td>
			<td>&nbsp;</td>
			<td>&nbsp;</td>
		</tr>
		<tr>
			<td>&nbsp;</td>
			<td>&nbsp;</td>
			<td>&nbsp;</td>
			<td style="width: 116px">&nbsp;</td>
			<td>&nbsp;</td>
		</tr>
		<%--<tr>
			<td>&nbsp;</td>
			<td><label>Per Value</label></td>
			<td><input name="corpPerValue" type="text" id="corpPerValue" runat="server" class="numeric"  /></td>
			<td>&nbsp;</td>
		</tr>--%>
	</table><input name="corpPerValue" type="hidden" id="corpPerValue" runat="server" class="numeric"  />
    
    <script type="text/javascript">
        function totalSubsribedCapital() {
            $("#ContentPlaceHolder1_corpSubscribedCapital").val("0");
            $(".SubsribedCapital").each(function () {
                $("#ContentPlaceHolder1_corpSubscribedCapital").val(parseInt($("#ContentPlaceHolder1_corpSubscribedCapital").val().replace(new RegExp(",", "g"), "")) + parseInt($(this).val().replace(new RegExp(",", "g"), "")));
            });
            reloadNumeric();
        }
        function totalPaidupCapital() {
            $("#ContentPlaceHolder1_corpPaidUpCapital").val("0");
            $(".PaidupCapital").each(function () {
                $("#ContentPlaceHolder1_corpPaidUpCapital").val(parseInt($("#ContentPlaceHolder1_corpPaidUpCapital").val().replace(new RegExp(",", "g"), "")) + parseInt($(this).val().replace(new RegExp(",", "g"), "")));
            });
            reloadNumeric();
        }
    </script>

<br />
  <asp:SqlDataSource ID="dsVendorShareHolders" runat="server" ConnectionString="<%$ ConnectionStrings:AVAConnectionString %>"
			SelectCommand="IF EXISTS (SELECT 1 FROM tblVendorShareHolders WHERE VendorId = @VendorId) BEGIN SELECT * FROM tblVendorShareHolders WHERE VendorId = @VendorId END ELSE BEGIN SELECT 0 AS ShareHolderId,'' AS VendorId, '' AS shShareHolderName, '' AS shNationality, '' AS shSubsribedCapital, '' AS DateCreated, '' AS shAuthorizedCapital, '' AS shPaidupCapital END" OnSelecting="rptGeneral_Selecting"  >
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

<table border="0" cellpadding="5" cellspacing="0" id="tbl04_Lbl" runat="server">
  <tr>
	<td style="width: 180px"><strong>Authorized capital</strong></td>
	<td><h3><asp:Label runat="server" ID="corpAuthorizedCapital_Lbl"></asp:Label></h3></td>
  </tr>
  <tr>
	<td style="height: 33px; width: 180px"><strong>
	  <label>Subscribed capital</label>
	</strong></td>
	<td style="height: 33px"><h3><asp:Label runat="server" ID="corpSubscribedCapital_Lbl"></asp:Label></h3></td>
  </tr>
  <tr>
	<td style="width: 180px"><strong>Paid up capital</strong></td>
	<td><h3><asp:Label runat="server" ID="corpPaidUpCapital_Lbl"></asp:Label></h3></td>
  </tr>
  <%--<tr>
	<td style="width: 180px"><strong>Per value</strong></td>
	<td><h3><asp:Label runat="server" ID="corpPerValue_Lbl"></asp:Label></h3></td>
  </tr>--%>
  <%--<tr>
	<td><strong>As of</strong><div style="font-size:10px">dd/mm/yyyy</div></td>
	<td><h3><asp:Label runat="server" ID="corpAsOfDate_Lbl"></asp:Label></h3></td>
  </tr>--%>
</table>
<br />


<div class="separator1"></div>
<h3 style="margin:10px 0px;">5. PROPRIETOR/PARTNERS/INCORPORATORS</h3>
<table width="75%" border="0" cellspacing="0" cellpadding="5">
  <tr>
	<th width="180"><strong>Name</strong></th>
	<th width="180"><strong>Nationality</strong></th>
	<th><strong>Position</strong></th>
	<th><input type="hidden" name="BoardMembersCounter" id="BoardMembersCounter" class="rowCount" /><a href="javascript:void(0)" value="Add Row" class="alternativeRow" runat="server" id="add4b">+Add</a></th>
  </tr>
<asp:Repeater ID="repeaterBoardMembers" runat="server" DataSourceID="dsVendorBoardMembers">
<ItemTemplate> 
  <tr>
	<td><input name="bmMemberOfTheBoard" type="text" id="bmMemberOfTheBoard"  value="<%# Eval("bmMemberOfTheBoard") %>" /></td>
	<td><input name="bmNationality" type="text" id="bmNationality"  value="<%# Eval("bmNationality") %>" /></td>
	<td><input name="bmPostion" type="text" id="bmPostion"  value="<%# Eval("bmPostion") %>" /></td>
	<td valign="bottom"><img src="images/trash.png" width="9" height="13" border="0" class="delRow" /></td>
  </tr>
</ItemTemplate>
</asp:Repeater>

<asp:Repeater ID="repeaterBoardMembers_Lbl" runat="server" DataSourceID="dsVendorBoardMembers">
<ItemTemplate> 
  <tr>
	<td><h3><asp:Label runat="server" ID="bmMemberOfTheBoard_Lbl" Text='<%# Eval("bmMemberOfTheBoard") %>'></asp:Label></td>
	<td><h3><asp:Label runat="server" ID="bmNationality_Lbl" Text='<%# Eval("bmNationality") %>'></asp:Label></h3></td>
	<td><h3><asp:Label runat="server" ID="bmPostion_Lbl" Text='<%# Eval("bmPostion") %>'></asp:Label></h3></td>
	<td valign="bottom">&nbsp;</td>
  </tr>
</ItemTemplate>
</asp:Repeater>
  </table>
<br />
  <asp:SqlDataSource ID="dsVendorBoardMembers" runat="server" ConnectionString="<%$ ConnectionStrings:AVAConnectionString %>"
			SelectCommand="IF EXISTS (SELECT 1 FROM tblVendorBoardMembers WHERE VendorId = @VendorId) BEGIN SELECT * FROM tblVendorBoardMembers WHERE VendorId = @VendorId END ELSE BEGIN SELECT 0 AS ID,'' AS VendorId, '' AS bmMemberOfTheBoard, '' AS bmNationality, '' AS bmPostion, '' AS DateCreated END" OnSelecting="rptGeneral_Selecting"  >
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





<%--<table border="0" cellpadding="5" cellspacing="0" id="tbl04" runat="server">
  <tr>
	<td width="180"><strong>Authorized capital</strong></td>
	<td><input name="corpAuthorizedCapital" type="text" id="corpAuthorizedCapital" runat="server" class="numeric" /></td>
  </tr>--%>
  <%--<tr>
	<td><strong>
	  <label>Subscribed capital</label>
	</strong></td>
	<td><input name="corpSubscribedCapital" type="text" id="corpSubscribedCapital" runat="server" class="numeric"  /></td>
  </tr>--%>
  <%--<tr>
	<td><strong>Paid up capital</strong></td>
	<td><input name="corpPaidUpCapital" type="text" id="corpPaidUpCapital" runat="server" class="numeric"  /></td>
  </tr>--%>
  <%--<tr>
	<td><strong>Per value</strong></td>
	<td><input name="corpPerValue" type="text" id="corpPerValue" runat="server" class="numeric"  /></td>
  </tr>--%>
  <%--<tr>
	<td><strong>As of</strong><div style="font-size:10px">dd/mm/yyyy</div></td>
	<td><input type="text" name="corpAsOfDate" id="corpAsOfDate" class="date" runat="server" /></td>
  </tr>
</table>--%>
</div>


<div style="display:none">
<!--Business activities STARTS-->
<div class="separator1"></div>
<h3 style="margin:10px 0px;">6. Other regulatory requirements</h3>
<table width="65%" border="0" cellspacing="0" cellpadding="5">
  <tr>
	<td><strong>Regulatory requirement</strong></td>
	<td><strong>Date registered</strong><div style="font-size:10px">dd/mm/yyyy</div></td>
	<td><strong>Permit number</strong></td>
	<td><input type="hidden" name="RegulatoryRequirementsCounter" id="RegulatoryRequirementsCounter" class="rowCount" /><a href="javascript:void(0)" value="Add Row" class="alternativeRow" id="add5" runat="server">+Add</a></td>
  </tr>
<asp:Repeater ID="repeaterRegulatoryRequirements" runat="server" DataSourceID="dsRegulatoryRequirements">
<ItemTemplate>  
  <tr>
	<td><input name="RegulatoryRequirement" type="text" id="RegulatoryRequirement" value="<%# Eval("RegulatoryRequirement") %>"  /></td>
	<td><input name="DateRegistered" type="text" id="DateRegistered" class="date" onfocus="reloadDatepicker()"  value="<%# Eval("DateRegistered").ToString() !="1/1/1900 12:00:00 AM" ? string.Format("{0:MM/dd/yyyy}", Eval("DateRegistered")) : "" %>" /></td>
	<td><input name="PermitNo" type="text" id="PermitNo" value="<%# Eval("PermitNo") %>" /></td>
	<td valign="bottom"><img src="images/trash.png" width="9" height="13" border="0" class="delRow" /></td>
  </tr>
  </ItemTemplate>
  </asp:Repeater>

<asp:Repeater ID="repeaterRegulatoryRequirements_Lbl" runat="server" DataSourceID="dsRegulatoryRequirements">
<ItemTemplate>  
  <tr>
	<td><h3><asp:Label runat="server" ID="RegulatoryRequirement_Lbl" Text='<%# Eval("RegulatoryRequirement") %>'></asp:Label></h3></td>
	<td><h3><asp:Label runat="server" ID="DateRegistered_Lbl" Text='<%# Eval("DateRegistered").ToString()!="1/1/1900 12:00:00 AM" ? string.Format("{0:MM/dd/yyyy}", Eval("DateRegistered")) : "" %>'></asp:Label></h3></td>
	<td><h3><asp:Label runat="server" ID="PermitNo_Lbl" Text='<%# Eval("PermitNo") %>'></asp:Label></h3></td>
	<td valign="bottom">&nbsp;</td>
  </tr>
  </ItemTemplate>
  </asp:Repeater>
  </table>
<br /> 
  <asp:SqlDataSource ID="dsRegulatoryRequirements" runat="server" ConnectionString="<%$ ConnectionStrings:AVAConnectionString %>"
			SelectCommand="IF EXISTS (SELECT 1 FROM tblVendorRegulatoryRequirements WHERE VendorId = @VendorId) BEGIN SELECT * FROM tblVendorRegulatoryRequirements WHERE VendorId = @VendorId END ELSE BEGIN SELECT 0 AS ID,'' AS VendorId, '' AS RegulatoryRequirement, '' AS DateRegistered, '' AS PermitNo, '' AS DateCreated END" OnSelecting="rptGeneral_Selecting"  >
	<SelectParameters>
		<asp:Parameter Name="VendorId" Type="Int32" />
	</SelectParameters>
  </asp:SqlDataSource>
</div>
<div class="separator1"></div>
<br />
<br />
<br />
<asp:LinkButton ID="createBt" runat="server" CssClass="bt1"  onclientclick="javascript: __doPostBack('continueStp', ''); return false;"><span>SAVE &amp; CONTINUE STEP 5 »</span></asp:LinkButton>
&nbsp;<asp:LinkButton ID="createBt1" runat="server" CssClass="bt1"  onclientclick="javascript: __doPostBack('justSave', ''); return false;"><span>SAVE</span></asp:LinkButton>
&nbsp;<a href="vendor_03_businessOperational.aspx<%= queryString %>" class="bt1"><span>BACK</span></a>
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