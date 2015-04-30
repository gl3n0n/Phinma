<%@ Page Title="" Language="C#" MasterPageFile="~/MasterSubPage.master" AutoEventWireup="true" CodeFile="vendor_01_vendorInfo.aspx.cs" Inherits="vendor_01_vendorInfo" %>
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
    
    
    <!--### UPLOADIFY ###-->
    <%--<script src="uploadify/jquery-1.4.2.min.js" type="text/javascript"></script>--%>
    <script src="uploadify/swfobject.js" type="text/javascript"></script>
    <script src="uploadify/jquery.uploadify.v2.1.4.js" type="text/javascript"></script>
    <script src="uploadify/jquery.uploadify.v2.1.4.min.js" type="text/javascript"></script>
    <link href="uploadify/uploadify.css" rel="stylesheet" type="text/css" />
    <!--### UPLOADIFY ENDS ###-->


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

    
<script src="Scripts/jquery-ui.min.js"></script>
<script type="text/javascript" src="plugins/sessionTimeout/js/timeout-dialog.js"></script>
<link rel="stylesheet" href="plugins/sessionTimeout/css/timeout-dialog.css" type="text/css" media="screen, projection" />

	<script type="text/javascript" src="Scripts/jquery.table.addrow.js" ></script>
<link href="Styles/ava_pages.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        h3 {
            clear: both;
        }
        .auto-style1 {
            width: 219px;
        }
        .auto-style2 {
            width: 236px;
        }
        .auto-style3 {
            width: 219px;
            height: 27px;
        }
        .auto-style4 {
            width: 236px;
            height: 27px;
        }
    </style>
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
<ava:topnav ID="TopNav1" runat="server" /><ava:stepnav ID="StepNav1" runat="server" />
<div style="margin:10px 0px; color:#333; font-size:18px; width:450px; float:left;">Vendor Information</div>


<form id="formVendorInfo" runat="server">

<!--Business activities STARTS-->
<div class="separator1"></div>
<h3 style="margin:10px 0px;">1. Registered Business name</h3>
<table width="100" border="0" cellspacing="0" cellpadding="5" runat="server" id="tbl01">
  <tr>
    <td colspan="4" style="height: 101px"><label for="CompanyName">Company name</label>
        <br />
<input name="CompanyName" type="text" id="CompanyName" size="60" runat="server"  causesvalidation="True" />
<%--        <h3><asp:Label ID="CompanyName_Lbl" runat="server"></asp:Label></h3>--%>
        <br />
<span style="font-size:12px; font-style:italic; display:block; clear:both">Registered business name (Exact name as in SEC Registration/DTI)</span></td>
    </tr>
  <tr>
    <td><label for="regBldgUnit">Unit/Floor/Room</label></td>
    <td><label for="regBldgBldg">Building</label></td>
    <td><label for="regBldgLotNo">Lot No.</label></td>
    <td><label for="regBldgBlock">Block No.</label></td>
  </tr>
  <tr>
    <td>
      <input type="text" name="regBldgUnit" id="regBldgUnit" class="field regBldgUnit" runat="server" maxlength="80" />
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="RequiredFieldValidator" ControlToValidate="regBldgUnit"></asp:RequiredFieldValidator>
    </td>
    <td>
        <input type="text" name="regBldgBldg" id="regBldgBldg" runat="server" maxlength="80"  />
      </td>
    <td>
        <input type="text" name="regBldgLotNo" id="regBldgLotNo" runat="server" maxlength="80" />
    </td>
    <td>
        <input type="text" name="regBldgBlock" id="regBldgBlock" runat="server" maxlength="80" />
    </td>
  </tr>
  <tr>
    <td><label for="regBldgPhase">Phase No.</label></td>
    <td><label for="regBldgHouseNo">House No.</label></td>
    <td><label for="regBldgStreet">Street</label></td>
    <td><label for="regBldgSubd">Subdivision</label></td>
  </tr>
  <tr>
    <td>
      <input type="text" name="regBldgPhase" id="regBldgPhase" class="field regBldgPhase" runat="server" maxlength="80" />
    </td>
    <td>
        <input type="text" name="regBldgHouseNo" id="regBldgHouseNo" runat="server" maxlength="80" />
      </td>
    <td>
        <input type="text" name="regBldgStreet" id="regBldgStreet" runat="server" maxlength="80" />
    </td>
    <td>
        <input type="text" name="regBldgSubd" id="regBldgSubd" runat="server" maxlength="80" />
    </td>
  </tr>
  <tr>
    <td><label for="regBrgy">Barangay</label></td>
    <td><label for="regCity">Municipality/City</label></td>
    <td><label for="regProvince">Province</label></td>
    <td><label for="regCountry">Country</label></td>
  </tr>
  <tr>
    <td>
        <input type="text" name="regBrgy" id="regBrgy" runat="server" maxlength="80" />
    </td>
    <td>
        <input type="text" name="regCity" id="regCity" runat="server" maxlength="80" />
    </td>
    <td>
        <input type="text" name="regProvince" id="regProvince" runat="server" maxlength="80" />
    </td>
    <td>
        <input type="text" name="regCountry" id="regCountry" runat="server" maxlength="80" />
    </td>
  </tr>
  <tr>
    <td><label for="regPostal">Zip code</label></td>
    <td><label for="regOwned">Owned/ Renting to Own/ Rented</label></td>
    <td colspan="2"><label for="regOwnedAttachment" id="regOwnedAttachmentLblfor">If Owned, Attach Transfer Certificate Title (TCT), Lease Agreement, 6 months utility bills</label></td>
  </tr>
  <tr>
    <td>
        <input type="text" name="regPostal" id="regPostal" runat="server" maxlength="80" />
    </td>
    <td>
        <input type="hidden" name="regArea" id="regArea" runat="server" />
        <asp:DropDownList ID="regOwned" runat="server">
                    <asp:ListItem>Owned</asp:ListItem>
                    <asp:ListItem>Renting to Own</asp:ListItem>
                    <asp:ListItem>Rented</asp:ListItem>
                </asp:DropDownList>
        <script type="text/javascript">
            $(document).ready(function () {
                if ($("#ContentPlaceHolder1_regOwned").val() == "Owned") {
                    $("#regOwnedAttachmentLblfor").html("If Owned, Attach Transfer Certificate Title (TCT)");
                } else {

                    $("#regOwnedAttachmentLblfor").html("If Leased, Attach Lease Agreement");
                }
            });
            $("#ContentPlaceHolder1_regOwned").change(function () {
                if ($("#ContentPlaceHolder1_regOwned").val() == "Owned") {
                    $("#regOwnedAttachmentLblfor").html("If Owned, Attach Transfer Certificate Title (TCT)");
                } else {

                    $("#regOwnedAttachmentLblfor").html("If Leased, Attach Lease Agreement");
                }
            });
        </script>
    </td>
    <td colspan="2"><div class="clearfix"></div>
    <script type="text/javascript">
        // <![CDATA[
        $(document).ready(function () {
            $('#regOwnedAttachmentFile_btup').hide();
            $('#regOwnedAttachmentFile').uploadify({
                'uploader': 'uploadify/uploadify.swf',
                'script': 'upload.ashx',

                'cancelImg': 'uploadify/cancel.png',
                'auto': false,
                'multi': true,
                'fileDesc': 'Attach File',
                'fileExt': '*.jpg;*.png;*.gif;*bmp;*.jpeg;*.doc;*.docx;*.xls;*.xlsx;*.zip;*.rar;*.ppt;*.pdf',
                'queueSizeLimit': 5,
                'sizeLimit': 4000000,
                'folder': 'uploads/<%= Session["VendorId"] %>',
                'onSelect': function (event, ID, fileObj) {
                    $('#regOwnedAttachmentFile_btup').show();
                },
                'onCancel': function (event, ID, fileObj, data) {
                    $('#regOwnedAttachmentFile_btup').hide();
                },
                'onComplete': function (event, queueID, fileObj, response, data) {
                    //alert(response);
                    $('.regOwnedAttachmentLbl').append('<div><a href="' + response + '" target="_blank">Attached file</a> <img src=\"images/xicon.png\" style=\"margin-left:10px; padding-top:5px; \" id=\"regOwnedAttachmentx\" onclick=\"$(this).parent(\'div\').html(\'\'); FileattchValues($(\'#ContentPlaceHolder1_regOwnedAttachment\').val(), \'' + response + '\');\" /><br></div>');
                    $('#ContentPlaceHolder1_regOwnedAttachment').attr('value', $('#ContentPlaceHolder1_regOwnedAttachment').val() + ';'+response);
                    $('#regOwnedAttachmentFile_btup').hide();
                }
            });
        });
        function FileattchValues(Str, StrRemove)
        {
            $('#ContentPlaceHolder1_regOwnedAttachment').attr('value', '');
            var myArray = Str.split(';');
            for (var i = 0; i < myArray.length; i++) {
                if (myArray[i] != StrRemove)
                    if (myArray[i] != "") {
                        $('#ContentPlaceHolder1_regOwnedAttachment').attr('value', $('#ContentPlaceHolder1_regOwnedAttachment').attr('value') + ';' + myArray[i]);
                    }
            }

            //alert($('#ContentPlaceHolder1_regOwnedAttachment').attr('value'));
        }
        // ]]>
    </script>
        <div style="float:left; width:30px;"><input id="regOwnedAttachmentFile" type="file"/></div> 
        <asp:Label ID="regOwnedAttachmentLbl" CssClass="regOwnedAttachmentLbl" runat="server" Text="" style="float:left; padding-top:3px; display:block"></asp:Label> 
        <input id="regOwnedAttachment" name="regOwnedAttachment" runat="server" type="hidden" value="" />
        <div style="font-size:9px; clear:both;">(Max size each file: 4 MB), click paperclip again to upload multiple files. </div>
        <a href="javascript:$('#regOwnedAttachmentFile').uploadifyUpload();" id="regOwnedAttachmentFile_btup">Upload Selection</a>
        <div class="clearfix"></div>
    </td>
  </tr>
</table>
<table width="100%" border="0" cellspacing="0" cellpadding="5" runat="server" id="tbl01_Lbl">
  <tr>
    <td colspan="4" style="height: 101px"><label for="CompanyName">Company name</label><%--<asp:RegularExpressionValidator
                ID="RegularExpressionValidator1" runat="server" Font-Bold="True" 
            Font-Italic="True" ForeColor="Red" ErrorMessage="RegularExpressionValidator" 
            ControlToValidate="CompanyName" 
            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>--%>  
        <br />
        <h3><asp:Label ID="CompanyName_Lbl" runat="server"></asp:Label></h3>
        <br />
<span style="font-size:12px; font-style:italic; display:block; clear:both">Registered business name (Exact name as in SEC Registration/DTI)</span></td>
    </tr>
  <tr>
    <td style="width: 25%" valign="top"><label for="regBldgCode">Unit/Floor/Room</label>
        <h3><asp:Label ID="regBldgUnit_Lbl" runat="server"></asp:Label></h3>
    </td>
    <td style="width: 25%" valign="top"><label for="regBldgBldg">Building</label>
        <h3><asp:Label ID="regBldgBldg_Lbl" runat="server"></asp:Label></h3>
    </td>
    <td style="width: 25%" valign="top"><label for="regBldgLotNo">Lot No.</label>
        <h3><asp:Label ID="regBldgLotNo_Lbl" runat="server"></asp:Label></h3>
      </td>
    <td style="width: 25%" valign="top"><label for="regBldgBlock">Block No.</label>
        <h3><asp:Label ID="regBldgBlock_Lbl" runat="server"></asp:Label></h3>
    </td>
  </tr>
  <tr>
    <td style="width: 25%" valign="top"><label for="regBldgPhase">Phase No.</label>
        <h3><asp:Label ID="regBldgPhase_Lbl" runat="server"></asp:Label></h3>
    </td>
    <td style="width: 25%" valign="top"><label for="regBldgHouseNo">House No.</label>
        <h3><asp:Label ID="regBldgHouseNo_Lbl" runat="server"></asp:Label></h3>
    </td>
    <td style="width: 25%" valign="top"><label for="regBldgStreet">Street</label>
        <h3><asp:Label ID="regBldgStreet_Lbl" runat="server"></asp:Label></h3>
      </td>
    <td style="width: 25%" valign="top"><label for="regBldgSubd">Subdivision</label>
        <h3><asp:Label ID="regBldgSubd_Lbl" runat="server"></asp:Label></h3>
    </td>
    
  </tr>
  <tr>
    <td style="width: 25%" valign="top"><label for="regBrgy">Barangay</label>
        <h3><asp:Label ID="regBrgy_Lbl" runat="server"></asp:Label></h3>
    </td>
    <td style="width: 25%" valign="top"><label for="regCity">City</label>
        <h3><asp:Label ID="regCity_Lbl" runat="server"></asp:Label></h3>
    </td>
    <td style="width: 25%" valign="top"><label for="regProvince">Province</label>
        <h3><asp:Label ID="regProvince_Lbl" runat="server"></asp:Label></h3>
    </td>
    <td style="width: 25%" valign="top"><label for="regCountry">country</label>
        <h3><asp:Label ID="regCountry_Lbl" runat="server"></asp:Label></h3>
    </td>
  </tr>
  <tr>
    <td style="width: 25%" valign="top"><label for="regPostal">Zip code</label>
        <h3><asp:Label ID="regPostal_Lbl" runat="server"></asp:Label></h3>
    </td>
    <td style="width: 25%" valign="top"><label for="regOwned">Owned/ Renting to Own/ Rented</label>
        <h3><asp:Label ID="regOwned_Lbl" runat="server"></asp:Label></h3>
    </td>
    <td  colspan="2" style="width: 25%" valign="top"><label for="regOwnedAttachment">If Owned, Attach Transfer Certificate Title (TCT), Lease Agreement, 6 months utility bills</label>
        <div class="clearfix"></div>
        <h3><asp:Label ID="regOwnedAttachment_Lbl" runat="server"></asp:Label></h3>
        <h3 style="display:none"><asp:Label ID="regArea_Lbl" runat="server"></asp:Label></h3>
    </td>
  </tr>
</table>
<br />



<!--Products/Services applied for accreditation STARTS-->
<div class="separator1"></div>
<h3 style="margin:10px 0px;">2. Contact Details</h3>
<table border="0" cellspacing="0" cellpadding="5" runat="server" id="tbl02">
  <tr>
    <td>&nbsp;</td>
    <td><strong>Name</strong></td>
    <td><strong>Position</strong></td>
    <td><strong>Email address</strong></td>
    <td valign="bottom"><strong>Mobile number</strong><br><span style="font-size:10px; font-style:italic">Country+Area+Phone</span></td>
    <td valign="bottom"><strong>Tel.  number</strong><br><span style="font-size:10px; font-style:italic">Country+Area+Phone</span></td>
    
    <td valign="bottom"><%--<strong>Fax  number</strong>--%></td>
    </tr>
  <tr>
    <td><label>CEO/President/GM</label></td>
    <td>
        <input name="conBidName" type="text" id="conBidName" size="10" runat="server" maxlength="80" />
    </td>
    <td>
        <input name="conBidPosition" type="text" id="conBidPosition" size="10" runat="server" maxlength="60" />
    </td>
    <td>
        <input name="conBidEmail" type="text" id="conBidEmail" size="10" runat="server" maxlength="80" />
    </td>
    <td >
        <input name="conBidMobile" type="text" id="conBidMobile" size="10" runat="server" maxlength="40" />
    </td>
    <td >
        <input name="conBidTelNo" type="text" id="conBidTelNo" size="10" runat="server" maxlength="40" />
    </td>
    <td >
        <input name="conBidFaxNo" type="hidden" id="conBidFaxNo" size="10" runat="server" maxlength="40" />

    </td>
    </tr>
  <tr>
    <td><label>Sales and Marketing</label></td>
    <td>
        <input name="conSalName" type="text" id="conSalName" size="10" runat="server" maxlength="80" />
    </td>
    <td>
        <input name="conSalPosition" type="text" id="conSalPosition" size="10" runat="server" maxlength="60" />
    </td>
    <td>
        <input name="conSalEmail" type="text" id="conSalEmail" size="10" runat="server" maxlength="80" />
    </td>
    <td >
        <input name="conSalMobile" type="text" id="conSalMobile" size="10" runat="server" maxlength="40" />
    </td>
    <td >
        <input name="conSalTelNo" type="text" id="conSalTelNo" size="10" runat="server" maxlength="40" />
    </td>
    <td >
        <input name="conSalFaxNo" type="hidden" id="conSalFaxNo" size="10" runat="server" maxlength="40" />

    </td>
    </tr>
  <tr>
    <td><label>Technical/ Engineering</label></td>
    <td>
        <input name="conTecName" type="text" id="conTecName" size="10" runat="server" maxlength="80" />
    </td>
    <td>
        <input name="conTecPosition" type="text" id="conTecPosition" size="10" runat="server" maxlength="60" />
    </td>
    <td>
        <input name="conTecEmail" type="text" id="conTecEmail" size="10" runat="server" maxlength="80" />
    </td>
    <td >
        <input name="conTecMobile" type="text" id="conTecMobile" size="10" runat="server" maxlength="40" />
    </td>
    <td >
        <input name="conTecTelNo" type="text" id="conTecTelNo" size="10" runat="server" maxlength="40" />
    </td>
    <td >
        <input name="conTecFaxNo" type="hidden" id="conTecFaxNo" size="10" runat="server" maxlength="40" />

    </td>
    </tr>
  <tr>
    <td><label>Accounting</label></td>
    <td>
        <input name="conLegName" type="text" id="conLegName" size="10" runat="server" maxlength="80" />
    </td>
    <td>
        <input name="conLegPosition" type="text" id="conLegPosition" size="10" runat="server" maxlength="60" />
    </td>
    <td>
        <input name="conLegEmail" type="text" id="conLegEmail" size="10" runat="server" maxlength="80" />
    </td>
    <td >
        <input name="conLegMobile" type="text" id="conLegMobile" size="10" runat="server" maxlength="40" />
    </td>
    <td >
        <input name="conLegTelNo" type="text" id="conLegTelNo" size="10" runat="server" maxlength="40" />
    </td>
    <td >
        <input name="conLegFaxNo" type="hidden" id="conLegFaxNo" size="10" runat="server" maxlength="40" />

    </td>
    </tr>

  <tr>
    <td><label>Credit and Collection</label></td>
    <td>
        <input name="conBonName" type="text" id="conBonName" size="10" runat="server" maxlength="80" />
    </td>
    <td>
        <input name="conBonPosition" type="text" id="conBonPosition" size="10" runat="server" maxlength="60" />
    </td>
    <td>
        <input name="conBonEmail" type="text" id="conBonEmail" size="10" runat="server" maxlength="80" />
    </td>
    <td >
        <input name="conBonMobile" type="text" id="conBonMobile" size="10" runat="server" maxlength="40" />
    </td>
    <td >
        <input name="conBonTelNo" type="text" id="conBonTelNo" size="10" runat="server"  maxlength="40" />
    </td>
    <td >
        <input name="conBonFaxNo" type="hidden" id="conBonFaxNo" size="10" runat="server" maxlength="40" />

    </td>
    </tr>


</table>


<table border="0" cellspacing="0" cellpadding="5" width="50%" runat="server" id="tbl02_Lbl" >
  <tr>
    <td colspan="6"><label>CEO/President/GM</label></td>
    </tr>
  <tr>
    <td wdth="25%" class="auto-style3"><strong>Name</strong></td>
    <td class="auto-style4"><strong>Position</strong></td>
    </tr>
  <tr>
    <td class="auto-style1">
        <h3><asp:Label ID="conBidName_Lbl" runat="server"></asp:Label></h3>
    </td>
    <td class="auto-style2">
        <h3><asp:Label ID="conBidPosition_Lbl" runat="server"></asp:Label></h3>
    </td>
    </tr>

  <tr>
    <td class="auto-style1"><strong>Email address</strong></td>
    <td class="auto-style2"><strong>Mobile number</strong></td>
    </tr>

  <tr>
    <td class="auto-style1">
        <h3><asp:Label ID="conBidEmail_Lbl" runat="server"></asp:Label></h3>
    </td>
    <td class="auto-style2" >
        <h3><asp:Label ID="conBidMobile_Lbl" runat="server"></asp:Label></h3>
    </td>
    </tr>
  <tr>
    <td class="auto-style1"><strong>Tel.  number</strong></td>
    <td class="auto-style2"><strong><%--Fax  number--%></strong></td>
    </tr>

  <tr>
    <td class="auto-style1">
        <h3><asp:Label ID="conBidTelNo_Lbl" runat="server"></asp:Label></h3>
    </td>
    <td class="auto-style2" >
        <h3><asp:Label ID="conBidFaxNo_Lbl" runat="server" style="display:none"></asp:Label></h3>

    </td>
    </tr>


    <tr>
    <td colspan="6"><br /><br /><label>Sales and Marketing</label></td>
    </tr>
  <tr>
    <td wdth="25%" class="auto-style3"><strong>Name</strong></td>
    <td class="auto-style4"><strong>Position</strong></td>
    </tr>
  <tr>
    <td class="auto-style1">
        <h3><asp:Label ID="conSalName_Lbl" runat="server"></asp:Label></h3>
    </td>
    <td class="auto-style2">
        <h3><asp:Label ID="conSalPosition_Lbl" runat="server"></asp:Label></h3>
    </td>
    </tr>

  <tr>
    <td class="auto-style1"><strong>Email address</strong></td>
    <td class="auto-style2"><strong>Mobile number</strong></td>
    </tr>

  <tr>
    <td class="auto-style1">
        <h3><asp:Label ID="conSalEmail_Lbl" runat="server"></asp:Label></h3>
    </td>
    <td class="auto-style2" >
        <h3><asp:Label ID="conSalMobile_Lbl" runat="server"></asp:Label></h3>
    </td>
    </tr>
  <tr>
    <td class="auto-style1"><strong>Tel.  number</strong></td>
    <td class="auto-style2"><strong><%--Fax  number--%></strong></td>
    </tr>

  <tr>
    <td class="auto-style1">
        <h3><asp:Label ID="conSalTelNo_Lbl" runat="server"></asp:Label></h3>
    </td>
    <td class="auto-style2" >
        <h3><asp:Label ID="conSalFaxNo_Lbl" runat="server" style="display:none"></asp:Label></h3>

    </td>
    </tr>

 <tr>
    <td colspan="6"><br /><br /><label>Technical/ Engineering</label></td>
    </tr>
  <tr>
    <td wdth="25%" class="auto-style3"><strong>Name</strong></td>
    <td class="auto-style4"><strong>Position</strong></td>
    </tr>
  <tr>
    <td class="auto-style1">
        <h3><asp:Label ID="conTecName_Lbl" runat="server"></asp:Label></h3>
    </td>
    <td class="auto-style2">
        <h3><asp:Label ID="conTecPosition_Lbl" runat="server"></asp:Label></h3>
    </td>
    </tr>

  <tr>
    <td class="auto-style1"><strong>Email address</strong></td>
    <td class="auto-style2"><strong>Mobile number</strong></td>
    </tr>

  <tr>
    <td class="auto-style1">
        <h3><asp:Label ID="conTecEmail_Lbl" runat="server"></asp:Label></h3>
    </td>
    <td class="auto-style2" >
        <h3><asp:Label ID="conTecMobile_Lbl" runat="server"></asp:Label></h3>
    </td>
    </tr>
  <tr>
    <td class="auto-style1"><strong>Tel.  number</strong></td>
    <td class="auto-style2"><strong><%--Fax  number--%></strong></td>
    </tr>

  <tr>
    <td class="auto-style1">
        <h3><asp:Label ID="conTecTelNo_Lbl" runat="server"></asp:Label></h3>
    </td>
    <td class="auto-style2" >
        <h3><asp:Label ID="conTecFaxNo_Lbl" runat="server" style="display:none"></asp:Label></h3>

    </td>
    </tr>


     <tr>
    <td colspan="6"><br /><br /><label>Accounting</label></td>
    </tr>
  <tr>
    <td wdth="25%" class="auto-style3"><strong>Name</strong></td>
    <td class="auto-style4"><strong>Position</strong></td>
    </tr>
  <tr>
    <td class="auto-style1">
        <h3><asp:Label ID="conLegName_Lbl" runat="server"></asp:Label></h3>
    </td>
    <td class="auto-style2">
        <h3><asp:Label ID="conLegPosition_Lbl" runat="server"></asp:Label></h3>
    </td>
    </tr>

  <tr>
    <td class="auto-style1"><strong>Email address</strong></td>
    <td class="auto-style2"><strong>Mobile number</strong></td>
    </tr>

  <tr>
    <td class="auto-style1">
        <h3><asp:Label ID="conLegEmail_Lbl" runat="server"></asp:Label></h3>
    </td>
    <td class="auto-style2" >
        <h3><asp:Label ID="conLegMobile_Lbl" runat="server"></asp:Label></h3>
    </td>
    </tr>
  <tr>
    <td class="auto-style1"><strong>Tel.  number</strong></td>
    <td class="auto-style2"><strong><%--Fax  number--%></strong></td>
    </tr>

  <tr>
    <td class="auto-style1">
        <h3><asp:Label ID="conLegTelNo_Lbl" runat="server"></asp:Label></h3>
    </td>
    <td class="auto-style2" >
        <h3><asp:Label ID="conLegFaxNo_Lbl" runat="server" style="display:none"></asp:Label></h3>

    </td>
    </tr>

       <tr>
    <td colspan="6"><br /><br /><label>Credit and Collection</label></td>
    </tr>
  <tr>
    <td wdth="25%" class="auto-style3"><strong>Name</strong></td>
    <td class="auto-style4"><strong>Position</strong></td>
    </tr>
  <tr>
    <td class="auto-style1">
        <h3><asp:Label ID="conBonName_Lbl" runat="server"></asp:Label></h3>
    </td>
    <td class="auto-style2">
        <h3><asp:Label ID="conBonPosition_Lbl" runat="server"></asp:Label></h3>
    </td>
    </tr>

  <tr>
    <td class="auto-style1"><strong>Email address</strong></td>
    <td class="auto-style2"><strong>Mobile number</strong></td>
    </tr>

  <tr>
    <td class="auto-style1">
        <h3><asp:Label ID="conBonEmail_Lbl" runat="server"></asp:Label></h3>
    </td>
    <td class="auto-style2" >
        <h3><asp:Label ID="conBonMobile_Lbl" runat="server"></asp:Label></h3>
    </td>
    </tr>
  <tr>
    <td class="auto-style1"><strong>Tel.  number</strong></td>
    <td class="auto-style2"><strong><%--Fax  number--%></strong></td>
    </tr>

  <tr>
    <td class="auto-style1">
        <h3><asp:Label ID="conBonTelNo_Lbl" runat="server"></asp:Label></h3>
    </td>
    <td class="auto-style2" >
        <h3><asp:Label ID="conBonFaxNo_Lbl" runat="server" style="display:none"></asp:Label></h3>

    </td>
    </tr>


</table>
<br />

<div style="display:none">
<!--Business activities STARTS-->
<div class="separator1"></div>
<h3 style="margin:10px 0px;">3. Parent company</h3>
<table  border="0" cellspacing="0" cellpadding="2" runat="server" id="tbl03">
  <tr>
    <td width="114" style="height: 50px"><label for="parentCompanyName">Company name</label></td>
    <td width="471" style="height: 50px">
        <input name="parentCompanyName" type="text" id="parentCompanyName" size="60" runat="server" />
    </td>
  </tr>
  <tr>
    <td><label for="parentCompanyAddr">Complete address</label></td>
    <td>
        <input name="parentCompanyAddr" type="text" id="parentCompanyAddr" size="60" runat="server" />
    </td>
  </tr>
</table>
<table  border="0" cellspacing="0" cellpadding="2" runat="server" id="tbl03_Lbl">
  <tr>
    <td width="114" style="height: 50px"><label for="parentCompanyName">Company name</label></td>
    <td width="471" style="height: 50px">
        <h3><asp:Label ID="parentCompanyName_Lbl" runat="server"></asp:Label></h3>
    </td>
  </tr>
  <tr>
    <td><label for="parentCompanyAddr">Complete address</label></td>
    <td>
        <h3><asp:Label ID="parentCompanyAddr_Lbl" runat="server"></asp:Label></h3>
    </td>
  </tr>
</table>
<br />
</div>

<!--Business activities STARTS-->
    <div style="display:none">
<div class="separator1"></div>
<h3 style="margin:10px 0px;">4. Representative office in the Philippines (if any, for foreign vendor)</h3>
<table  border="0" cellspacing="0" cellpadding="5" runat="server" id="tbl04">
  <tr>
    <td width="114"><label for="repOfcCompanyName">Company name</label></td>
    <td class="auto-style3">
        <input name="repOfcCompanyName" type="text" id="repOfcCompanyName" size="60" runat="server" />
    </td>
  </tr>
  <tr>
    <td><label for="repOfcCompanyAddr">Complete address</label></td>
    <td class="auto-style3">
        <input name="repOfcCompanyAddr" type="text" id="repOfcCompanyAddr" size="60" runat="server" />
    </td>
  </tr>
  <tr>
    <td><strong>Telephone number</strong><br><span style="font-size:10px; font-style:italic">Country+Area+Phone</span></td>
    <td class="auto-style3"><input type="text" name="repOfcTelNo" id="repOfcTelNo" runat="server" />
    </td>
    </tr>
  <tr>
    <td><strong>Fax number</strong><br><span style="font-size:10px; font-style:italic">Country+Area+Phone</span></td>
    <td class="auto-style3"><input type="text" name="repOfcFaxNo" id="repOfcFaxNo" runat="server" />
    </td>
    </tr>
  <tr>
    <td><strong>Email address</strong></td>
    <td class="auto-style3"><input type="text" name="repOfcEmail" id="repOfcEmail" runat="server" />
    </td>
    </tr>
</table>
<table  border="0" cellspacing="0" cellpadding="5" runat="server" id="tbl04_Lbl">
  <tr>
    <td width="114"><label for="repOfcCompanyName">Company name</label></td>
    <td>
        <h3><asp:Label ID="repOfcCompanyName_Lbl" runat="server"></asp:Label></h3>
    </td>
  </tr>
  <tr>
    <td><label for="repOfcCompanyAddr">Complete address</label></td>
    <td>
        <h3><asp:Label ID="repOfcCompanyAddr_Lbl" runat="server"></asp:Label></h3>
    </td>
  </tr>
  <tr>
    <td><strong>Telephone number</strong></td>
    <td>
        <h3><asp:Label ID="repOfcTelNo_Lbl" runat="server"></asp:Label></h3>
    </td>
    </tr>
  <tr>
    <td><strong>Fax number</strong></td>
    <td>
        <h3><asp:Label ID="repOfcFaxNo_Lbl" runat="server"></asp:Label></h3>
    </td>
    </tr>
  <tr>
    <td><strong>Email address</strong></td>
    <td>
        <h3><asp:Label ID="repOfcEmail_Lbl" runat="server"></asp:Label></h3>
    </td>
    </tr>
</table>
<br />
    </div>

    <div style="display:none">
<!--Business activities STARTS-->
<div class="separator1"></div>
<h3 style="margin:10px 0px;">4. Other offices / branches or affiliates (Including Foreign)</h3>
<table width="100%" border="0" cellspacing="0" cellpadding="5"  class="atable"  id="aTable1" >
  <tr>
    <td><label for="brAddr">Branches/Address</label></td>
    <td><label for="brUsedAs">Used as</label></td>
    <td class="auto-style4"><label for="brEmplNo">No. of Employees</label></td>
    <td><label for="brEmplNo">Area</label></td>
    <td><label for="brOwned">Owned/Renting to Own/Rented</label></td>
    <td valign="bottom"><input id="VendorBranchesCounter" class="rowCount" name="VendorBranchesCounter" type="hidden" /><a href="javascript:void(0)" value="Add Row" class="alternativeRow" runat="server" id="add5">+Add</a></td>
  </tr>

<asp:Repeater ID="repeaterBranches" runat="server" DataSourceID="dstblVendorBranches">
<ItemTemplate>
<tr id="aTableRow1" >
    <td><input type="text" name="brAddr" id="brAddr" value="<%# Eval("brAddr")%>" /></td>
    <td><input type="text" name="brUsedAs" id="brUsedAs" value="<%# Eval("brUsedAs")%>" /></td>
    <td><input type="text" name="brEmplNo" id="brEmplNo" value="<%# Eval("brEmplNo")%>" /></td>
    <td><input type="text" name="brArea" id="brArea" value="<%# Eval("brArea")%>" /></td>
    <td>
        <%--<input type="text" name="brOwned" id="brOwned" value="<%# Eval("brOwned")%>" />--%>
        <select id="brOwned" name="brOwned" style="z-index:1000">
            <option>Select</option>
            <option value="Owned" <%# Eval("brOwned").ToString() == "Owned" ? "selected='selected'" : ""%>>Owned</option>
            <option value="Renting to Own" <%# Eval("brOwned").ToString() == "Renting to Own" ? "selected='selected'" : ""%>>Renting to Own</option>
            <option value="Rented" <%# Eval("brOwned").ToString() == "Rented" ? "selected='selected'" : ""%>>Rented</option>
        </select>
    </td>
    <td valign="bottom"><img alt="" src="images/trash.png" width="9" height="13" border="0" class="delRow" /></td>
  </tr>
</ItemTemplate>
</asp:Repeater>

    
<asp:Repeater ID="repeaterBranches_Lbl" runat="server" DataSourceID="dstblVendorBranches">
<ItemTemplate>
<tr id="aTableRow1" >
    <td><h3><asp:Label ID="Label0" runat="server" Text='<%# Eval("brAddr")%>' ></asp:Label></h3></td>
    <td><h3><asp:Label ID="Label1" runat="server" Text='<%# Eval("brUsedAs")%>'></asp:Label></h3></td>
    <td><h3><asp:Label ID="Label2" runat="server" Text='<%# Eval("brEmplNo")%>'></asp:Label></h3></td>
    <td><h3><asp:Label ID="Label3" runat="server" Text='<%# Eval("brArea")%>'></asp:Label></h3></td>
    <td><h3><asp:Label ID="Label4" runat="server" Text='<%# Eval("brOwned")%>'></asp:Label></h3></td>
    <td valign="bottom">&nbsp;</td>
  </tr>
</ItemTemplate>
</asp:Repeater>
</table>
  <asp:SqlDataSource ID="dstblVendorBranches" runat="server" ConnectionString="<%$ ConnectionStrings:AVAConnectionString %>"
            SelectCommand="IF EXISTS (SELECT 1 FROM tblVendorBranches WHERE VendorId = @VendorId) BEGIN SELECT * FROM tblVendorBranches WHERE VendorId = @VendorId END ELSE BEGIN SELECT 0 AS BranchId,'' AS VendorId,'' AS brAddr, '' AS brUsedAs, '' AS brEmplNo, '' AS brArea, '' AS brOwned, '' AS DateCreated END" OnSelecting="rptGeneral_Selecting"  >
    <SelectParameters>
        <asp:Parameter Name="VendorId" Type="Int32" />
	</SelectParameters>
  </asp:SqlDataSource> 
<br /> 
<script type="text/javascript">
    $("document").ready(function () {
        $(".alternativeRow").btnAddRow({ inputBoxAutoNumber: true, inputBoxAutoId: true, displayRowCountTo: "rowCount" });
        $(".delRow").btnDelRow();
    });
</script>
</div>


<!--Business activities STARTS-->
<div class="separator1"></div>
<h3 style="margin:10px 0px;">3. Other offices / branches or affiliates (Including Foreign)</h3>
<table width="90%" border="0" cellspacing="0" cellpadding="5" id="aTable2">
  <tr>
    <td style="width: 138px"><label for="subCompanyName">Company name</label></td>
    <td><label for="subAddr">Address</label></td>
    <td><label for="SubContact">Contact No.</label></td>
    <td><label for="SubFax">Fax No.</label></td>
    <td><label for="SubEmailAdd">Email Address</label></td>
    <%--<td style="display:none"><label for="subEquity">% Equity Held</label></td>--%>
    <td><label for="subOwned">Owned/Renting</label></td>
    <td valign="bottom"><input id="SubsidiaryCounter" class="rowCount" name="SubsidiaryCounter" type="hidden" /><a href="javascript:void(0)" value="Add Row" class="alternativeRow" runat="server" id="add6">+Add</a></td>
  </tr>
  <asp:Repeater ID="repeaterSubsidiary" runat="server" DataSourceID="dstblVendorSubsidiary">
    <ItemTemplate>
  <tr>
    <td><input type="text" name="subCompanyName" id="subCompanyName" value="<%# Eval("subCompanyName")%>" maxlength="100" /></td>
    <td><input type="text" name="subAddr" id="subAddr" value="<%# Eval("subAddr")%>" style="width:120px;" maxlength="100" /></td>
    <td><input type="text" name="SubContact" id="SubContact" value="<%# Eval("SubContact")%>" style="width:80px;" maxlength="80" /></td>
    <td><input type="text" name="SubFax" id="SubFax" value="<%# Eval("SubFax")%>" style="width:80px;" maxlength="80"/></td>
    <td><input type="text" name="SubEmailAdd" id="SubEmailAdd" value="<%# Eval("SubEmailAdd")%>" style="width:100px;" maxlength="80" /></td>
    <%--<td style="display:none"><input type="text" name="subEquity" id="subEquity" class="numeric" value="0" onfocus="reloadNumeric()" /></td>--%>
    <td>
        <%--<input type="text" name="subOwned" id="subOwned" value="<%# Eval("subOwned")%>" />--%>
        <select id="subOwned" name="subOwned" style="z-index:1000">
            <option>Select</option>
            <option value="Owned" <%# Eval("subOwned").ToString() == "Owned" ? "selected='selected'" : ""%>>Owned</option>
            <option value="Renting to Own" <%# Eval("subOwned").ToString() == "Renting to Own" ? "selected='selected'" : ""%>>Renting to Own</option>
            <option value="Rented" <%# Eval("subOwned").ToString() == "Rented" ? "selected='selected'" : ""%>>Rented</option>
        </select>

    </td>
    <td valign="bottom"><input type="hidden" name="subSubsidiaryId" id="subSubsidiaryId" value="<%# Eval("SubsidiaryId")%>" /><img alt="" src="images/trash.png" width="9" height="13" border="0" class="delRow" /></td>
  </tr>
  </ItemTemplate>
  </asp:Repeater>
    
  <asp:Repeater ID="repeaterSubsidiary_Lbl" runat="server" DataSourceID="dstblVendorSubsidiary">
    <ItemTemplate>
  <tr>
    <td><h3><asp:Label ID="Label4" runat="server" Text='<%# Eval("subCompanyName")%>'></asp:Label></h3></td>
    <td><h3><asp:Label ID="Label5" runat="server" Text='<%# Eval("subAddr")%>'></asp:Label></h3></td>
      <td><h3><asp:Label ID="Label8" runat="server" Text='<%# Eval("SubContact")%>'></asp:Label></h3></td>
      <td><h3><asp:Label ID="Label9" runat="server" Text='<%# Eval("SubFax")%>'></asp:Label></h3></td>
      <td><h3><asp:Label ID="Label10" runat="server" Text='<%# Eval("SubEmailAdd")%>'></asp:Label></h3></td>
    <td style="display:none"><h3><asp:Label ID="Label6" runat="server" Text='<%# Eval("subEquity")%>'></asp:Label></h3></td>
    <td><h3><asp:Label ID="Label7" runat="server" Text='<%# Eval("subOwned")%>'></asp:Label></h3></td>
    <td valign="bottom">&nbsp;</td>
  </tr>
  </ItemTemplate>
  </asp:Repeater>
  </table>
  <asp:SqlDataSource ID="dstblVendorSubsidiary" runat="server" ConnectionString="<%$ ConnectionStrings:AVAConnectionString %>"
            SelectCommand="IF EXISTS (SELECT 1 FROM tblVendorSubsidiaries WHERE VendorId = @VendorId) BEGIN SELECT * FROM tblVendorSubsidiaries WHERE VendorId = @VendorId END ELSE BEGIN SELECT 0 AS SubsidiaryId,'' AS VendorId,'' AS subCompanyName, '' AS subAddr, '' AS SubContact, '' AS SubFax, '' AS SubEmailAdd,  '' AS subEquity, '' AS subOwned, '' AS DateCreated END" OnSelecting="rptGeneral_Selecting" >
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



<div class="separator1"></div>
<br />
<br />
<br />

    <asp:LinkButton ID="createBt" runat="server" CssClass="bt1" onclientclick="javascript: __doPostBack('continueStp'); return false;" CausesValidation="True"><span>SAVE &amp; CONTINUE STEP 2 &raquo;</span></asp:LinkButton>
    <%--<a href="javascript: voi(0)" onclick="if(validateForm1()){ __doPostBack('<%=createBt.ClientID%>', ''); }" runat="server" id="createBt" class="bt1"><span>SAVE &amp; CONTINUE STEP 2 &raquo;</span></a>--%>
    &nbsp;
    <asp:LinkButton ID="createBt1" runat="server" CssClass="bt1"  onclientclick=" __doPostBack('justSave'); return false;"  CausesValidation="True"><span>SAVE</span></asp:LinkButton>
    <%--<a href="javascript: voi(0)" onclick="if(validateForm1()){ __doPostBack('ContentPlaceHolder1_createBt1', ''); }" runat="server" id="createBt1" class="bt1"><span>SAVE</span></a>--%>
    &nbsp;
    <a href="vendor_Home.aspx<%= queryString %>" class="bt1"><span>BACK</span></a> <br />
<br />
<br />
    <!--### FORM VALIDATION STARTS #########-->
    <script type="text/javascript">
        function validateForm1() {
            //alert(id);
            var err = "";
            //addresss
            if (
                    $("#ContentPlaceHolder1_regBldgUnit").val() == "" &&
                    $("#ContentPlaceHolder1_regBldgBldg").val() == "" &&
                    $("#ContentPlaceHolder1_regBldgLotNo").val() == "" &&
                    $("#ContentPlaceHolder1_regBldgBlock").val() == "" &&
                    $("#ContentPlaceHolder1_regBldgPhase").val() == "" &&
                    $("#ContentPlaceHolder1_regBldgHouseNo").val() == "" &&
                    $("#ContentPlaceHolder1_regBldgStreet").val() == "" &&
                    $("#ContentPlaceHolder1_regBldgSubd").val() == "" &&
                    $("#ContentPlaceHolder1_regBrgy").val() == "" &&
                    $("#ContentPlaceHolder1_regCity").val() == "" &&
                    $("#ContentPlaceHolder1_regProvince").val() == "" &&
                    $("#ContentPlaceHolder1_regCountry").val() == "" &&
                    $("#ContentPlaceHolder1_regPostal").val() == ""
                ) {
                err = err + "Complete address is required.\n";
            }

            if (err != "") {
                alert(err);
                return false;
            }
            else {
                // alert("ok");
                //alert(action);
                //__doPostBack();
                return true;
            }
        }
    </script>
    <!--### FORM VALIDATION ENDS #########-->
</form>




<!--BODY CONTENT ENDS-->
<!--##################-->
</div>
</div>
</div><!-- content ends --> 

    </div>

    </div>
    
<script type="text/javascript">
    /*<![CDATA[*/
    $(function () {
        $("#timeout-example").click(function (e) {
            e.preventDefault();
            $.timeoutDialog({ timeout: 1, countdown: 60, logout_redirect_url: 'logout.aspx', restart_on_yes: false });
        });
    });
    /*]]>*/
</script>
</asp:Content>