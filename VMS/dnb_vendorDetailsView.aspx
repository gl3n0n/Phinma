<%@ Page Title="" Language="C#" MasterPageFile="~/MasterSubPage.master" AutoEventWireup="true" CodeFile="dnb_vendorDetailsView.aspx.cs" Inherits="dnb_vendorDetailsView" %>
<%@ Register TagPrefix="Ava" TagName="Tabsnav" Src="usercontrols/tabs.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitlePlaceHolder1" runat="Server">
Trans-Asia VMS Accreditation :: Vendor Information</asp:Content>
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
    <form id="Form1" runat="server">
<%@ Register TagPrefix="Ava" TagName="TopNav" Src="usercontrols/TopNav_dnb.ascx" %>
<ava:topnav ID="TopNav1" runat="server" />
<div style="margin:10px 0px; color:#333; font-size:18px; width:450px; float:left;">Vendor Name: <asp:Label ID="CompanyNameLbl" runat="server" Text=""></asp:Label>
    <br />
  Authentication Ticket: <asp:Label ID="AuthenticationTicketLbl" runat="server" Text=""></asp:Label></div>
<div class="stepsdiv" style="margin:10px 0px; font-size:12px; width:450px; float:right; text-align:right; font-weight:bold;"><asp:Label ID="errNotification" runat="server" Text="" style="display:block; float:none; clear:both;" ></asp:Label>
    <div style="float:right;"><label for="dnbDuns">Assessment No.</label>
      <%--<input type="text" name="dnbDuns" id="dnbDuns" runat="server"  readonly="readonly" />--%>
          <h3><asp:Label ID="dnbDuns" runat="server"></asp:Label></h3>
    </div>
</div>

<!--Business activities STARTS-->
        <div class="clearfix"></div>
<div class="separator1"></div>
<h3 style="margin:10px 0px; text-align:left">Scores</h3>

<table width="100%" border="0" cellspacing="0" cellpadding="8">
  <tr>
      <td style="width: 106px"><label for="">Assessment Score: </label></td>
      <td>
      <%--<input type="text" name="dnbScore" id="dnbScore" runat="server" class="integer"  readonly="readonly" />--%>
          <h3><asp:Label ID="dnbScore" runat="server"></asp:Label></h3>
      </td>
      </tr>
    </table>

<table width="100%" border="0" cellspacing="0" cellpadding="8">
  <tr>
    <td class="style1" style="width: 372px"><label for="dnbCompRating">Financial Capability Score (45%)<%--<span style="font-size:10px; display:block">Numeric only</span>--%></label></td>
    <td><label for="">Legal Conformance Score (15%)<%--<span style="font-size:10px; display:block">Numeric only</span>--%></label></td>
    <td><label for="">Technical Qualification Score (40%)<%--<span style="font-size:10px; display:block">Numeric only</span>--%></label></td>
  </tr>
  <tr>
    <td class="style1" style="width: 372px">
      <%--<input type="text" name="dnbFinCapScore" id="dnbFinCapScore" runat="server" class="integer"  readonly="readonly" />--%>
        <h3><asp:Label ID="dnbFinCapScore" runat="server"></asp:Label></h3>
      </td>
    <td>
        <%--<input type="text" name="dnbLegalConfScore" id="dnbLegalConfScore" runat="server" class="integer"  readonly="readonly" />--%>
        <h3><asp:Label ID="dnbLegalConfScore" runat="server"></asp:Label></h3>
    </td>
    <td>
        <%--<input type="text" name="dnbTechCompScore" id="dnbTechCompScore" runat="server" class="integer"  readonly="readonly"/>--%>
        <h3><asp:Label ID="dnbTechCompScore" runat="server"></asp:Label></h3>
    </td>
  </tr>
  <tr>
    <td class="style1" style="width: 372px; height: 32px;">
        <label for="condition">Remarks</label></td>
    <td class="style1" style="width: 372px; height: 32px;">
        <label for="condition">Remarks</label></td>
    <td class="style1" style="width: 372px; height: 32px;">
        <label for="condition">Remarks</label></td>
  </tr>
  <tr>
    <td class="style1" style="width: 372px">
        <%--<textarea id="dnbFinCapScore_Remarks" cols="28" name="dnbFinCapScore_Remarks" rows="3" style="font-family:Arial" runat="server" readonly="readonly"></textarea>--%>
        <h3><asp:Label ID="dnbFinCapScore_Remarks" runat="server"></asp:Label></h3>
    </td>
    <td class="style1" style="width: 372px">
        <%--<textarea id="dnbLegalConfScore_Remarks" cols="28" name="dnbLegalConfScore_Remarks" rows="3" style="font-family:Arial"  runat="server" readonly="readonly"></textarea>--%>
        <h3><asp:Label ID="dnbLegalConfScore_Remarks" runat="server"></asp:Label></h3>
    </td>
    <td class="style1" style="width: 372px">
        <%--<textarea id="dnbTechCompScore_Remarks" cols="28" name="dnbTechCompScore_Remarks" rows="3" style="font-family:Arial"  runat="server" readonly="readonly"></textarea>--%>
        <h3><asp:Label ID="dnbTechCompScore_Remarks" runat="server"></asp:Label></h3></td>
  </tr>
</table>
<br />


<div style="display:none;">
<!--Business activities STARTS-->
<div class="separator1"></div>
<h3 style="margin:10px 0px;">Financial Report</h3>
<table border="0" cellspacing="0" cellpadding="5">
  <tr>
    <td><strong>Latest Full Year  Revenue</strong><%--<span style="font-size:10px; display:block">Numeric only</span>--%></td>
    <td><%--<input name="dnbCurrentRevenue" type="text" id="dnbCurrentRevenue" runat="server" class="integer" onblur="$('#ContentPlaceHolder1_MaxExposureLimit').val(parseInt($(this).val())*(.5)) " readonly="readonly" />--%>
        <h3><asp:Label ID="dnbCurrentRevenue" runat="server"></asp:Label></h3></td>
  </tr>
  <tr>
    <td><strong>Maximum Exposure Limit</strong></td>
    <td><%--<input name="MaxExposureLimit" type="text" id="MaxExposureLimit" runat="server"  class="integer"  readonly="readonly" />--%>
        <h3><asp:Label ID="MaxExposureLimit" runat="server"></asp:Label></h3></td>
  </tr>
  </table>
<br />
<br />


<%--
<!--Business activities STARTS-->
<div class="separator1"></div>
<h3 style="margin:10px 0px;">Legal Report</h3>--%>
<!--Business activities STARTS-->
<div class="separator1"></div>
<h3 style="margin:10px 0px;">Legal Report</h3>
<table width="100%" border="0" cellspacing="0" cellpadding="5">
    <tr>
    <td width="120"><label for="TypeOfCase">Type of case</label></td>
    <td width="120"><label for="DateFiled">Date Filed</label></td>
    <td><label style="display:block; float:none">Attachment</label></td>
  </tr>
    
<asp:Repeater ID="repeaterdsDnbLegalReport" runat="server" DataSourceID="dsDnbLegalReport">
<ItemTemplate>
  <tr>
    <td>
      <h3 style="clear:both"><asp:Label ID="TypeOfCase" runat="server" Text='<%# Eval("TypeOfCase").ToString() %>'></asp:Label></h3></td>
    <td style="width: 320px">
      <h3 style="clear:both"><asp:Label ID="DateFiled" runat="server" Text='<%# Eval("DateFiled").ToString() %>'></asp:Label></h3></td>
    <td>
        <div style="float:left; width:30px;"><img src="images/attachment.png" /></div> 
        <asp:Label ID="fileuploaded_1" CssClass="fileuploaded_1" runat="server" Text='<%# Eval("Attachment").ToString()!="" ? "<a href=\"" + Eval("Attachment").ToString() + "\" target=\"_blank\">Attached file</a>" : "-" %>' style="float:left; padding-top:3px; display:block"></asp:Label>
        <input id="Attachment1" name="Attachment1" runat="server" type="hidden" value="" /></td>
  </tr>
  </ItemTemplate>
</asp:Repeater>
  </table>
<br />
<asp:SqlDataSource ID="dsDnbLegalReport" runat="server" ConnectionString="<%$ ConnectionStrings:AVAConnectionString %>"
            SelectCommand="IF EXISTS(SELECT 1 FROM tblDnbLegalReport WHERE VendorId = @VendorId) BEGIN SELECT ID, VendorId, TypeOfCase, DateFiled, Attachment, DateCreated FROM tblDnbLegalReport WHERE VendorId = @VendorId END ELSE BEGIN SELECT NULL as ID,  '' AS VendorId, '-' AS TypeOfCase, '-' AS DateFiled, '' AS Attachment, '-' AS DateCreated END" >
    <SelectParameters>
        <asp:SessionParameter Name="VendorId" SessionField="VendorId" Type="Int32" />
	</SelectParameters>
  </asp:SqlDataSource>
<div class="separator1"></div>

</div>

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
            }
        });
    });
    // ]]>
    </script>
        <div style="float:left; width:30px;"></div> 
        <asp:Label ID="dnbSupplierInfoReportLbl" CssClass="dnbSupplierInfoReportLbl" runat="server" Text="File Attachment" style="float:left; padding-top:3px; display:block"></asp:Label>
        <input id="dnbSupplierInfoReport" name="dnbSupplierInfoReport" runat="server" type="hidden" value="" />
        <div class="clearfix"></div> 


        <h3 style="margin:10px 0px;">Other Documents</h3>

        <div style="float:left; width:30px;"></div> 
        <asp:Label ID="dnbOtherDocumentsLbl" CssClass="dnbOtherDocumentsLbl" runat="server" Text="File Attachment" style="float:left; padding-top:3px; display:block"></asp:Label>
        <input id="dnbOtherDocuments" name="dnbOtherDocuments" runat="server" type="hidden" value="" />
        <div class="clearfix"></div> 
<br />

        
<!--VendorGroupCode STARTS-->
<div class="separator1"></div>
        <table style="width: 60%;">
            <tr>
                <td style="width:50%">
<h3 style="margin:10px 0px;">Vendor Group Code</h3>
        <asp:Label ID="VendorGroupCode_Lbl" runat="server"></asp:Label>
                </td>
                <td style="width:50%">
<h3 style="margin:10px 0px;">Trade / Non-Trade</h3>
        <asp:Label ID="TradeNonTrade_Lbl" runat="server"></asp:Label></td>
            </tr>
        </table>
<!--VendorGroupCode ENDS-->


<div class="separator1"></div>
<br />
<br />
<br />
<%--<asp:LinkButton ID="createBt" runat="server" CssClass="bt1" onclientclick="javascript: __doPostBack('continueStp', ''); return false;" ><span>ENDORSE &raquo;</span></asp:LinkButton>&nbsp;<asp:LinkButton ID="createBt1" runat="server" CssClass="bt1"  onclientclick="javascript: __doPostBack('justSave', ''); return false;"><span>SAVE</span></asp:LinkButton>&nbsp;--%>
<a href="dnb_VendorList.aspx" class="bt1"><span>BACK</span></a>
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