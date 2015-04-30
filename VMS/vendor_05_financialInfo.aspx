<%@ Page Title="" Language="C#" MasterPageFile="~/MasterSubPage.master" AutoEventWireup="true" CodeFile="vendor_05_financialInfo.aspx.cs" Inherits="vendor_05_financialInfo" %>
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
            $(".numeric").numeric(false, function () { alert("Integers only"); this.value = ""; this.focus(); });
            $(".integer").numeric(false, function () { alert("Integers only"); this.value = ""; this.focus(); });
            $(".wholenumber").numeric(false, function () { alert("Integers only"); this.value = ""; this.focus(); });

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
<div style="margin:10px 0px; color:#333; font-size:18px; width:450px; float:left;">Financial Information</div>

<form id="formVendorInfo" runat="server">


<div class="separator1"></div>
<h3 style="margin:10px 0px;">1. Financial Information</h3>
<b>Audited FINANCIAL STATEMENTS for the last three (3) consecutive years with ITR (Annual Income Tax Return), proof of BIR Payment and SEC stamp) and all its attachments</b>

    <asp:RadioButtonList ID="finanInfo_Type" runat="server">
        <asp:ListItem Value="Propiertorship">&nbsp;&nbsp;For Sole Proprietor: Include a copy of Annual Income Tax Return (BIR Form 1701) – FS should have BIR stamp</asp:ListItem>
        <asp:ListItem Value="Corporation">&nbsp;&nbsp;For Corporations: Include all the attachments – Annual Income Tax Return (BIR Form 1702),FS should have BIR and SEC Stamp</asp:ListItem>
        <asp:ListItem Value="Partnership">&nbsp;&nbsp;For Partnership: Include all the attachments – Annual Income Tax Return (BIR Form 1702),FS should have BIR Stamp</asp:ListItem>
    </asp:RadioButtonList>

<table width="100%" border="0" cellspacing="0" cellpadding="5" id="tbl01" runat="server">
  <tr>
    <td>&nbsp;</td>
    <td>&nbsp;</td>
    <td>&nbsp;</td>
    <td>&nbsp;</td>
  </tr>
  <tr>
    <td>&nbsp;</td>
    <td><strong>Year 1</strong><div style="font-size:10px">Numeric only</div></td>
    <td><strong>Year 2</strong><div style="font-size:10px">Numeric only</div></td>
    <td><strong>Year 3 (Latest)</strong><div style="font-size:10px">Numeric only</div></td>
  </tr>
  <tr>
    <td><strong>Year</strong></td>
    <td><label id="yr1"></label></td>
    <td><label id="yr2"></label></td>
    <td><input name="yr3" type="text" id="yr3" class="wholenumber" runat="server" maxlength="4" />
    <script type="text/javascript">
        $(document).ready(function () {
            if ($("#ContentPlaceHolder1_yr3").val() != "") {
                $("#yr2").html(parseInt($("#ContentPlaceHolder1_yr3").val()) - 1);
                $("#yr1").html(parseInt($("#ContentPlaceHolder1_yr3").val()) - 2);
            }
        });
        $("#ContentPlaceHolder1_yr3").blur(function () {
            if ($("#ContentPlaceHolder1_yr3").val() != "") {
                $("#yr2").html(parseInt($("#ContentPlaceHolder1_yr3").val()) - 1);
                $("#yr1").html(parseInt($("#ContentPlaceHolder1_yr3").val()) - 2);
            }
        });
        $("#ContentPlaceHolder1_yr3").change(function () {
            if ($("#ContentPlaceHolder1_yr3").val() != "") {
                $("#yr2").html(parseInt($("#ContentPlaceHolder1_yr3").val()) - 1);
                $("#yr1").html(parseInt($("#ContentPlaceHolder1_yr3").val()) - 2);
            }
        });
    </script>

    </td>
  </tr>
  <tr>
    <td><strong>Revenue</strong></td>
    <td><input name="yr1Revenue" type="text" id="yr1Revenue" class="numeric"  runat="server" /></td>
    <td><input name="yr2Revenue" type="text" id="yr2Revenue" class="numeric" runat="server" /></td>
    <td><input name="yr3Revenue" type="text" id="yr3Revenue" class="numeric" runat="server" /></td>
  </tr>
  <tr>
    <td><strong>Net Income</strong></td>
    <td><input name="yr1NetIncome" type="text" id="yr1NetIncome" class="numeric"  runat="server" /></td>
    <td><input name="yr2NetIncome" type="text" id="yr2NetIncome" class="numeric" runat="server" /></td>
    <td><input name="yr3NetIncome" type="text" id="yr3NetIncome" class="numeric" runat="server" /></td>
  </tr>
  <tr>
    <td><strong>Current assets</strong></td>
    <td><input name="yr1CurrentAssets" type="text" id="yr1CurrentAssets" class="numeric" runat="server"  /></td>
    <td><input name="yr2CurrentAssets" type="text" id="yr2CurrentAssets" class="numeric" runat="server" /></td>
    <td><input name="yr3CurrentAssets" type="text" id="yr3CurrentAssets" class="numeric" runat="server" /></td>
  </tr>
  <tr>
    <td><strong>Total assets</strong></td>
    <td><input name="yr1TotalAssets" type="text" id="yr1TotalAssets" class="numeric"  runat="server" /></td>
    <td><input name="yr2TotalAssets" type="text" id="yr2TotalAssets" class="numeric" runat="server" /></td>
    <td><input name="yr3TotalAssets" type="text" id="yr3TotalAssets" class="numeric" runat="server" /></td>
  </tr>
  <tr>
    <td><strong>Total liabilities</strong></td>
    <td><input name="yr1TotalLiabilities" type="text" id="yr1TotalLiabilities" class="numeric" runat="server"  /></td>
    <td><input name="yr2TotalLiabilities" type="text" id="yr2TotalLiabilities" class="numeric" runat="server" /></td>
    <td><input name="yr3TotalLiabilities" type="text" id="yr3TotalLiabilities" class="numeric" runat="server" /></td>
  </tr>
  <tr>
    <td><strong>Current liabilities</strong></td>
    <td><input name="yr1CurrentLiabilities" type="text" id="yr1CurrentLiabilities" class="numeric" runat="server" /></td>
    <td><input name="yr2CurrentLiabilities" type="text" id="yr2CurrentLiabilities" class="numeric" runat="server" /></td>
    <td><input name="yr3CurrentLiabilities" type="text" id="yr3CurrentLiabilities" class="numeric" runat="server" /></td>
  </tr>
  <tr>
  <%--<tr>
    <td><strong>Net equity</strong></td>
    <td><input name="yr1NetEquity" type="text" id="yr1NetEquity" class="numeric" runat="server"  /></td>
    <td><input name="yr2NetEquity" type="text" id="yr2NetEquity" class="numeric" runat="server" /></td>
    <td><input name="yr3NetEquity" type="text" id="yr3NetEquity" class="numeric" runat="server" /></td>
  </tr>--%>
  <%--<tr>
    <td><strong>Inventories</strong></td>
    <td><input name="yr1Inventories" type="text" id="yr1Inventories" class="numeric" runat="server"  /></td>
    <td><input name="yr2Inventories" type="text" id="yr2Inventories" class="numeric" runat="server" /></td>
    <td><input name="yr3Inventories" type="text" id="yr3Inventories" class="numeric" runat="server" /></td>
  </tr>--%>
    <td>&nbsp;</td>
    <td>
        <script type="text/javascript">
            // <![CDATA[
            $(document).ready(function () {
                $('#yr1FileNameFile').uploadify({
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
                                    $('.yr1FileNameLbl').html('<a href="' + response + '" target="_blank"> Attached file</a>');
                                    $('#ContentPlaceHolder1_yr1FileName').attr('value', response);
                                }
                            });
                        });
                        // ]]>
    </script>
        
    </td>
    <td>
        <%--<script type="text/javascript">
            // <![CDATA[
            $(document).ready(function () {
                $('#yr2FileNameFile').uploadify({
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
                                    $('.yr2FileNameLbl').html('<a href="' + response + '" target="_blank"> Attached file</a>');
                                    $('#ContentPlaceHolder1_yr2FileName').attr('value', response);
                                }
                            });
                        });
                        // ]]>
    </script>--%>
        <div style="float:left; width:30px;"><input id="yr2FileNameFile" type="hidden"/></div> 
        <asp:Label ID="yr2FileNameLbl" CssClass="yr2FileNameLbl" runat="server" Text="" style="float:left; padding-top:3px; display:none"></asp:Label>
        <input id="yr2FileName" name="yr2FileName" runat="server" type="hidden" value="" />
    </td>
    <td>
        <%--<script type="text/javascript">
            // <![CDATA[
            $(document).ready(function () {
                $('#yr3FileNameFile').uploadify({
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
                                    $('.yr3FileNameLbl').html('<a href="' + response + '" target="_blank"> Attached file</a>');
                                    $('#ContentPlaceHolder1_yr3FileName').attr('value', response);
                                }
                            });
                        });
                        // ]]>
    </script>--%>
        <div style="float:left; width:30px;"><input id="yr3FileNameFile" type="hidden"/></div> 
        <asp:Label ID="yr3FileNameLbl" CssClass="yr3FileNameLbl" runat="server" Text="" style="float:left; padding-top:3px; display:none"></asp:Label>
        <input id="yr3FileName" name="yr2FileName" runat="server" type="hidden" value="" />
    </td>
  </tr>
  <tr>
    <td colspan="4">
    <strong>Attach Latest Audited Financial Statement</strong><br /><br />
    <div style="float:left; width:30px;"><input id="yr1FileNameFile" type="file"/></div> 
        <asp:Label ID="yr1FileNameLbl" CssClass="yr1FileNameLbl" runat="server" Text="Attach file" style="float:left; padding-top:3px; display:block"></asp:Label>
        <input id="yr1FileName" name="yr1FileName" runat="server" type="hidden" value="" />
        <div style="font-size:9px; clear:both;">(Single attachment, max file size: 10 MB)</div>
    </td>
  </tr>
</table>
<table width="100%" border="0" cellspacing="0" cellpadding="5" id="tbl01_Lbl" runat="server">
  <tr>
    <td>&nbsp;</td>
    <td>&nbsp;</td>
    <td>&nbsp;</td>
    <td>&nbsp;</td>
  </tr>
  <tr>
    <td>&nbsp;</td>
    <td><strong>Year 1</strong><div style="font-size:10px">Numeric only</div></td>
    <td><strong>Year 2</strong><div style="font-size:10px">Numeric only</div></td>
    <td><strong>Year 3</strong><div style="font-size:10px">Numeric only</div></td>
  </tr>
    <tr>
    <td>&nbsp;</td>
    <td><asp:Label runat="server" ID="yr1YearInfo_Lbl"></asp:Label></td>
    <td><asp:Label runat="server" ID="yr2YearInfo_Lbl"></asp:Label></td>
    <td><asp:Label runat="server" ID="yr3YearInfo_Lbl"></asp:Label></td>
  </tr>
    <tr>
    <td><strong>Revenue</strong></td>
    <td><h3><asp:Label runat="server" ID="yr1Revenue_Lbl"></asp:Label></h3></td>
    <td><h3><asp:Label runat="server" ID="yr2Revenue_Lbl"></asp:Label></h3></td>
    <td><h3><asp:Label runat="server" ID="yr3Revenue_Lbl"></asp:Label></h3></td>
  </tr>
    <tr>
    <td><strong>Net Income</strong></td>
    <td><h3><asp:Label runat="server" ID="yr1NetIncome_Lbl"></asp:Label></h3></td>
    <td><h3><asp:Label runat="server" ID="yr2NetIncome_Lbl"></asp:Label></h3></td>
    <td><h3><asp:Label runat="server" ID="yr3NetIncome_Lbl"></asp:Label></h3></td>
  </tr>
  <tr>
    <td><strong>Current assets</strong></td>
    <td><h3><asp:Label runat="server" ID="yr1CurrentAssets_Lbl"></asp:Label></h3></td>
    <td><h3><asp:Label runat="server" ID="yr2CurrentAssets_Lbl"></asp:Label></h3></td>
    <td><h3><asp:Label runat="server" ID="yr3CurrentAssets_Lbl"></asp:Label></h3></td>
  </tr>
  <tr>
    <td><strong>Total assets</strong></td>
    <td><h3><asp:Label runat="server" ID="yr1TotalAssets_Lbl"></asp:Label></h3></td>
    <td><h3><asp:Label runat="server" ID="yr2TotalAssets_Lbl"></asp:Label></h3></td>
    <td><h3><asp:Label runat="server" ID="yr3TotalAssets_Lbl"></asp:Label></h3></td>
  </tr>
  <tr>
    <td><strong>Current liabilities</strong></td>
    <td><h3><asp:Label runat="server" ID="yr1CurrentLiabilities_Lbl"></asp:Label></h3></td>
    <td><h3><asp:Label runat="server" ID="yr2CurrentLiabilities_Lbl"></asp:Label></h3></td>
    <td><h3><asp:Label runat="server" ID="yr3CurrentLiabilities_Lbl"></asp:Label></h3></td>
  </tr>
  <tr>
    <td><strong>Total liabilities</strong></td>
    <td><h3><asp:Label runat="server" ID="yr1TotalLiabilities_Lbl"></asp:Label></h3></td>
    <td><h3><asp:Label runat="server" ID="yr2TotalLiabilities_Lbl"></asp:Label></h3></td>
    <td><h3><asp:Label runat="server" ID="yr3TotalLiabilities_Lbl"></asp:Label></h3></td>
  </tr>
  <%--<tr>
    <td><strong>Net equity</strong></td>
    <td><h3><asp:Label runat="server" ID="yr1NetEquity_Lbl"></asp:Label></h3></td>
    <td><h3><asp:Label runat="server" ID="yr2NetEquity_Lbl"></asp:Label></h3></td>
    <td><h3><asp:Label runat="server" ID="yr3NetEquity_Lbl"></asp:Label></h3></td>
  </tr>--%>
 <%-- <tr>
    <td><strong>Inventories</strong></td>
    <td><h3><asp:Label runat="server" ID="yr1Inventories_Lbl"></asp:Label></h3></td>
    <td><h3><asp:Label runat="server" ID="yr2Inventories_Lbl"></asp:Label></h3></td>
    <td><h3><asp:Label runat="server" ID="yr3Inventories_Lbl"></asp:Label></h3></td>
  </tr>--%>
  <tr>
    <td>&nbsp;</td>
    <td><asp:Label runat="server" ID="yr1FileName_Lbl"></asp:Label></td>
    <td><asp:Label runat="server" ID="yr2FileName_Lbl"></asp:Label></td>
    <td><asp:Label runat="server" ID="yr3FileName_Lbl"></asp:Label></td>
  </tr>
  <tr>
    <td colspan="4"><strong>Attach Latest Audited Financial Statement</strong><br /><br />
        <asp:Label ID="yr1FileNameLbl_Lbl" CssClass="yr1FileNameLbl_Lbl" runat="server" Text="Attached file" style="float:left; padding-top:3px; display:block"></asp:Label></td>
  </tr>
</table>
<br />


<div class="separator1"></div>
<br />
<br />
<br />
<asp:LinkButton ID="createBt" runat="server" CssClass="bt1"  onclientclick="javascript: __doPostBack('continueStp', ''); return false;"><span>SAVE &amp; CONTINUE STEP 6 »</span></asp:LinkButton>
&nbsp;<asp:LinkButton ID="createBt1" runat="server" CssClass="bt1"  onclientclick="javascript: __doPostBack('justSave', ''); return false;"><span>SAVE</span></asp:LinkButton>
&nbsp;<a href="vendor_04_Legal.aspx<%= queryString %>" class="bt1"><span>BACK</span></a>
<br />
<br />
<br />
</form>




<!--BODY CONTENT ENDS-->
<!--##################-->
</div>
</div>
</div><!-- content ends --> 

</asp:Content>