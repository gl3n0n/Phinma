<%@ Page Title="" Language="C#" MasterPageFile="~/MasterSubPage.master" AutoEventWireup="true" CodeFile="vendor_06_Others.aspx.cs" Inherits="vendor_06_Others" %>
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
<div style="margin:10px 0px; color:#333; font-size:18px; width:450px; float:left;">Others</div>

<form id="formVendorInfo" runat="server">

<div class="separator1"></div>
<h3 style="margin:10px 0px;">1. Court Case (if any)</h3>
<table border="0" cellspacing="0" cellpadding="5">
  <tr>
    <td width="180"><strong>Type of Case</strong></td>
    <td width="180"><strong>Date Registered</strong></td>
    <td width="180"><strong>Status</strong></td>
    <td width="180"><strong>Attachment</strong></td>
    <td><input type="hidden" name="VendorCourtCaseCounter" id="VendorCourtCaseCounter" class="rowCount" value="1" /><a href="javascript:void(0)" value="Add Row" class="alternativeRow" id="add1" runat="server" style="display:none">+Add</a></td>
    </tr>
    <asp:Repeater ID="repeaterVendorCourtCases" runat="server" DataSourceID="dsVendorCourtCases">
<ItemTemplate>  
  <tr>
    <td><input name="TypeOfCase" type="text" id="TypeOfCase" value="<%# Eval("TypeOfCase") %>"  /></td>
    <td><input name="DateRegistered" type="text" id="DateRegistered" class="date" onfocus="reloadDatepicker()"  value='<%# string.Format("{0:MM/dd/yyyy}", Eval("DateRegistered"))%>' /></td>
    <td><input name="Status" type="text" id="Status" value="<%# Eval("Status") %>" /></td>
    <td style="border-top: thin #CCC dotted;">
        <div style="float:left; width:30px;"><input id="fileUpload"  name="fileUpload"  type="file"/></div> 
        <asp:Label ID="fileuploadedLbl" CssClass="fileuploadedLbl" runat="server" Text='<%# Eval("Attachment").ToString()!="" ? "<a href=\"" + Eval("Attachment").ToString() + "\" target=\"_blank\">Attached file</a>" : "Attach file" %>' style="float:left; padding-top:3px; display:block"></asp:Label>
        <input id="Attachment" name="Attachment" type="hidden" value="<%# Eval("Attachment") %>" />
        <div style="font-size:9px; clear:both;">(Max file size: 10 MB)</div>
    </td>
    <td valign="bottom"><img src="images/trash.png" width="9" height="13" border="0" class="delRow" /></td>
    </tr>
   </ItemTemplate>
</asp:Repeater>

    <asp:Repeater ID="repeaterVendorCourtCases_Lbl" runat="server" DataSourceID="dsVendorCourtCases">
<ItemTemplate>  
  <tr>
    <td><h3><asp:Label runat="server" ID="TypeOfCase_Lbl" Text='<%# Eval("TypeOfCase") %>'></asp:Label></h3></td>
    <td><h3><asp:Label runat="server" ID="DateRegistered_Lbl" Text='<%# string.Format("{0:MM/dd/yyyy}", Eval("DateRegistered"))%>'></asp:Label></h3></td>
    <td><h3><asp:Label runat="server" ID="Status_Lbl" Text='<%# Eval("Status") %>'></asp:Label></h3></td>
    
    <td style="border-top: thin #CCC dotted;">
        <%# Eval("Attachment").ToString()!="" ? "<div style=\"float:left; width:30px;\"><img src=\"images/attachment.png\" /></div> <a href='" + Eval("Attachment").ToString() + "' target='_blank'>Attachment</a>" : "<div style=\"float:left; width:30px;\"><img src=\"images/attachment.png\" /></div> No attached file" %>
    </td>
    <td valign="bottom">&nbsp;</td>
    </tr>
   </ItemTemplate>
</asp:Repeater>
  </table>
  <asp:SqlDataSource ID="dsVendorCourtCases" runat="server" ConnectionString="<%$ ConnectionStrings:AVAConnectionString %>"
            SelectCommand="IF ((SELECT COUNT(*) FROM tblVendorCourtCases WHERE VendorId = @VendorId) >= 1 AND (SELECT COUNT(*) FROM tblVendorCourtCases WHERE VendorId = @VendorId) < 3) BEGIN SELECT CourtCaseId,VendorId, TypeOfCase, DateRegistered, Status, DateCreated, Attachment FROM tblVendorCourtCases WHERE VendorId = @VendorId UNION SELECT 0 AS CourtCaseId,'' AS VendorId, '' AS TypeOfCase, NULL AS DateRegistered, '' AS Status, NULL AS DateCreated, '' AS Attachment END ELSE IF ((SELECT COUNT(*) FROM tblVendorCourtCases WHERE VendorId = @VendorId) = 3) BEGIN SELECT CourtCaseId,VendorId, TypeOfCase, DateRegistered, Status, DateCreated, Attachment FROM tblVendorCourtCases WHERE VendorId = @VendorId END ELSE BEGIN SELECT 1 AS CourtCaseId,'' AS VendorId, '' AS TypeOfCase, NULL AS DateRegistered, '' AS Status, '' AS DateCreated, '' AS Attachment UNION SELECT 2 AS CourtCaseId,'' AS VendorId, '' AS TypeOfCase, NULL AS DateRegistered, '' AS Status, NULL AS DateCreated, '' AS Attachment UNION SELECT 3 AS CourtCaseId,'' AS VendorId, '' AS TypeOfCase, NULL AS DateRegistered, '' AS Status, NULL AS DateCreated, '' AS Attachment END" OnSelecting="rptGeneral_Selecting"  >
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
    <script type="text/javascript">
        // <![CDATA[
        $(document).ready(function () {
            $('#fileUpload1').uploadify({
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
                    $('#ContentPlaceHolder1_repeaterVendorCourtCases_fileuploadedLbl_0').html('<a href="' + response + '" target="_blank">Attached file</a>');
                    $('#Attachment1').attr('value', response);
                }
            });
            $('#fileUpload2').uploadify({
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
                    $('#ContentPlaceHolder1_repeaterVendorCourtCases_fileuploadedLbl_1').html('<a href="' + response + '" target="_blank">Attached file</a>');
                    $('#Attachment2').attr('value', response);
                }
            });
            $('#fileUpload3').uploadify({
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
                    $('#ContentPlaceHolder1_repeaterVendorCourtCases_fileuploadedLbl_2').html('<a href="' + response + '" target="_blank">Attached file</a>');
                    $('#Attachment3').attr('value', response);
                }
            });
        });
        // ]]>
</script>

<!--Business activities STARTS-->
<div class="separator1"></div>
<h3 style="margin:10px 0px;">SIR Attachment</h3>

<script type="text/javascript">
    // <![CDATA[
    $(document).ready(function () {
        $('#dnbSupplierInfoReportFile').uploadify({
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
                $('.dnbSupplierInfoReportLbl').html('<a href="' + response + '" target="_blank">' + response + '</a>');
                $('#ContentPlaceHolder1_dnbSupplierInfoReport').attr('value', response);
                $('#dnbSupplierInfoReportx').show();
            }
        });
    });
    // ]]>
    </script>

<div class="separator1"></div>
<h3 style="margin:10px 0px;">2. Quality Management System</h3>
    Certified to Quality Management System?

<asp:DropDownList ID="othersQltyMangmtSys" runat="server">
        <asp:ListItem Value="Yes">Yes</asp:ListItem>
        <asp:ListItem Value="No">No</asp:ListItem>
        </asp:DropDownList>
<h3><asp:Label runat="server" ID="othersQltyMangmtSys_Lbl"></asp:Label></h3>
        <div class="clearfix"></div>
    <strong>If Yes, attach certificate/s</strong><div class="clearfix"></div>
    <div id="othersQltyMangmtSys_File_Div" runat="server">

        <script type="text/javascript">
            // <![CDATA[
            $(document).ready(function () {
                $('#othersQltyMangmtSys_FileFile').uploadify({
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
                        $('.othersQltyMangmtSys_FileLbl').html('<a href="' + response + '" target="_blank">' + response + '</a>');
                        $('#ContentPlaceHolder1_othersQltyMangmtSys_File').attr('value', response);
                    }
                });
            });
            // ]]>
    </script>
        <div style="float:left; width:30px;">
            <input id="othersQltyMangmtSys_FileFile" type="file"/></div> 
        <asp:Label ID="othersQltyMangmtSys_FileLbl" CssClass="othersQltyMangmtSys_FileLbl" runat="server" Text="Attach file" style="float:left; padding-top:3px; display:block"></asp:Label>
        <input id="othersQltyMangmtSys_File" name="othersQltyMangmtSys_File" runat="server" type="hidden" value="" />

        
      <br />
        <div class="clearfix"></div>

        <script type="text/javascript">
            // <![CDATA[
            $(document).ready(function () {
                $('#othersQltyMangmtSys_File2File').uploadify({
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
                        $('.othersQltyMangmtSys_File2Lbl').html('<a href="' + response + '" target="_blank">' + response + '</a>');
                        $('#ContentPlaceHolder1_othersQltyMangmtSys_File2').attr('value', response);
                    }
                });
            });
            // ]]>
    </script>
        <div style="float:left; width:30px;">
            <input id="othersQltyMangmtSys_File2File" type="file"/></div> 
        <asp:Label ID="othersQltyMangmtSys_File2Lbl" CssClass="othersQltyMangmtSys_File2Lbl" runat="server" Text="Attach file" style="float:left; padding-top:3px; display:block"></asp:Label>
        <input id="othersQltyMangmtSys_File2" name="othersQltyMangmtSys_File2" runat="server" type="hidden" value="" />
        <div style="font-size:9px; clear:both;">(Max file size: 10 MB)</div>
    </div>
      <br />
        <div class="clearfix"></div>

    
<asp:Label ID="othersQltyMangmtSys_File_Lbl" runat="server" Text="Attach file"></asp:Label>
    
        <div class="clearfix"></div><br />
<asp:Label ID="othersQltyMangmtSys_File2_Lbl" runat="server" Text="Attach file"></asp:Label>
<br />


<div class="separator1"></div>
<br />
<br />
<br />
<asp:LinkButton ID="createBt" runat="server" CssClass="bt1"  onclientclick="javascript: __doPostBack('continueStp', ''); return false;"><span>SAVE &amp; CONTINUE STEP 7 »</span></asp:LinkButton>
&nbsp;<asp:LinkButton ID="createBt1" runat="server" CssClass="bt1"  onclientclick="javascript: __doPostBack('justSave', ''); return false;"><span>SAVE</span></asp:LinkButton>
&nbsp;<a href="vendor_05_financialInfo.aspx<%= queryString %>" class="bt1"><span>BACK</span></a>
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