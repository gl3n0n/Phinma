<%@ Page Title="" Language="C#" MasterPageFile="~/MasterSubPage.master" AutoEventWireup="true" CodeFile="vmofficer_vendorDetails_Endorsed.aspx.cs" Inherits="vmofficer_vendorDetails_Endorsed" %>
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
            $(".numeric").numeric();
            $(".integer").numeric(false, function () { alert("Integers only"); this.value = ""; this.focus(); });
        });
        function reloadNumeric() {
            $(".numeric").numeric();
            $(".integer").numeric(false, function () { alert("Integers only"); this.value = ""; this.focus(); });
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
<%@ Register TagPrefix="Ava" TagName="TopNav" Src="usercontrols/TopNav_legal.ascx" %>
<ava:topnav ID="TopNav1" runat="server" />

<div style="margin:10px 0px; color:#333; font-size:18px; width:450px; float:left;"><%--Vendor Name: 
    <br />
  Authentication Ticket: <asp:Label ID="AuthenticationTicketLbl" runat="server" Text=""></asp:Label>--%>
    Legal Review</div>
<div class="stepsdiv" style="margin:10px 0px; font-size:12px; width:450px; float:right; text-align:right; font-weight:bold;">
    <asp:Label ID="errNotification" runat="server" Text=""></asp:Label>
</div>

<!--Business activities STARTS-->
<div class="separator1" style="clear:both"></div>
<table width="100%" border="0" cellspacing="0" cellpadding="8" class="GridTbl">
  <tr>
    <td width="26%">Name of vendor</td>
    <td width="74%"><h3><asp:Label ID="CompanyNameLbl" runat="server" Text=""></asp:Label></h3></td>
  </tr>
  <tr>
    <td>VM Assessment No.</td>
    <td><h3><asp:Label ID="dnbDuns" runat="server" Text=""></asp:Label></h3></td>
    </tr>
  <tr>
    <td>Legal structure / Company type</td>
    <td><h3><asp:Label ID="legalStrucOrgType" runat="server" Text=""></asp:Label></h3></td>
    </tr>
  <tr>
    <td>Nature of business</td>
    <td><h3><asp:Label ID="NatureOfBusiness" runat="server" Text=""></asp:Label></h3></td>
    </tr>
  <tr>
    <td>Products/Services offered</td>
    <td><h3><asp:Label ID="Category" runat="server" Text=""></asp:Label></h3></td>
    </tr>
</table>
<br />

<div class="separator1"></div>
<h3 style="margin:10px 0px;">Rating</h3>
<table width="70%" border="0" cellspacing="0" cellpadding="8">
  <tr>
    <td class="style1">VM Assessment No. <h3><asp:Label ID="dnbRating" runat="server" Text=""></asp:Label></h3>
      </td>
    <td>Composite Rating<h3><asp:Label ID="dnbCompRating" runat="server" Text=""></asp:Label></h3>
      </td>
    <td>Condition<h3><asp:Label ID="condition" runat="server" Text=""></asp:Label></h3></td>
  </tr>
</table>
<br />


<!--Business activities STARTS-->
<div class="separator1"></div>
<h3 style="margin:10px 0px;">Financial Report</h3>
<table border="0" cellspacing="0" cellpadding="5" style="border:1px dotted #ccc">
  <tr>
    <td style="height: 32px; border-right:1px dotted #ccc"><strong>As of mm/dd/yyyy</strong></td>
    <td style="height: 32px; border-right:1px dotted #ccc"><h3><asp:Label ID="Year1" runat="server" Text=""></asp:Label></h3></td>
    <td style="height: 32px; border-right:1px dotted #ccc"><h3><asp:Label ID="Year2" runat="server" Text=""></asp:Label></h3></td>
    <td style="height: 32px;"><h3><asp:Label ID="Year3" runat="server" Text=""></asp:Label></h3></td>
  </tr>
  <tr>
    <td style="border-top:1px dotted #ccc; border-right:1px dotted #ccc"><strong>Revenue</strong><span style="font-size:10px; display:block">Numeric only</span></td>
    <td style="border-top:1px dotted #ccc; border-right:1px dotted #ccc"><h3><asp:Label ID="yr1Revenue" runat="server" Text=""></asp:Label></h3></td>
    <td style="border-top:1px dotted #ccc; border-right:1px dotted #ccc"><h3><asp:Label ID="yr2Revenue" runat="server" Text=""></asp:Label></h3></td>
    <td style="border-top:1px dotted #ccc;"><h3><asp:Label ID="yr3Revenue" runat="server" Text=""></asp:Label></h3></td>
  </tr>
  <tr>
    <td  style="border-top:1px dotted #ccc; border-right:1px dotted #ccc"><strong>Net Income</strong><span style="font-size:10px; display:block">Numeric only</span></td>
    <td  style="border-top:1px dotted #ccc; border-right:1px dotted #ccc"><h3><asp:Label ID="yr1NetIncome" runat="server" Text=""></asp:Label></h3></td>
    <td  style="border-top:1px dotted #ccc; border-right:1px dotted #ccc"><h3><asp:Label ID="yr2NetIncome" runat="server" Text=""></asp:Label></h3></td>
    <td  style="border-top:1px dotted #ccc;"><h3><asp:Label ID="yr3NetIncome" runat="server" Text=""></asp:Label></h3></td>
  </tr>
  <tr>
    <td style="border-top:1px dotted #ccc; border-right:1px dotted #ccc"><strong>Net equity</strong><span style="font-size:10px; display:block">Numeric only</span></td>
    <td style="border-top:1px dotted #ccc; border-right:1px dotted #ccc"><h3><asp:Label ID="yr1NetEquity" runat="server" Text=""></asp:Label></h3></td>
    <td style="border-top:1px dotted #ccc; border-right:1px dotted #ccc"><h3><asp:Label ID="yr2NetEquity" runat="server" Text=""></asp:Label></h3></td>
    <td style="border-top:1px dotted #ccc;"><h3><asp:Label ID="yr3NetEquity" runat="server" Text=""></asp:Label></h3></td>
  </tr>
  </table>
<br />
<table border="0" cellspacing="0" cellpadding="8">
  <tr>
    <td><label for="maxExpLimit">Maximum Exposure Limit</label>
      <h3><asp:Label ID="maxExpLimit" runat="server" Text=""></asp:Label></h3></td>
    <td><label for="creditExpLimit">Credit Exposure Limit</label>
      <h3><asp:Label ID="creditExpLimit" runat="server" Text=""></asp:Label></h3></td>
    </tr>
</table>
<br />






<div class="separator1"></div>
<h3 style="margin:10px 0px;">Legal Report</h3>
<p><strong>RECOLLECTION</strong></p>
  <br />
<p><%--Based on the report presented, the company--%><div style="float:left">Trans-Asia</div>
<asp:RadioButtonList ID="legalRecollection" runat="server" 
        RepeatDirection="Horizontal" RepeatLayout="Flow">
        <asp:ListItem Value="1" Selected="True">has</asp:ListItem>
        <asp:ListItem Value="0">has not</asp:ListItem>
        </asp:RadioButtonList>
<%--encountered any legal issues that may have been previously raised against the vendor.--%>
encountered legal issues that may have involved the vendor.
<div class="clearfix"></div>
<br />
  <br />
  
  <strong>REGISTERED CHARGES</strong><br />
</p>
<table width="100%" border="0" cellspacing="0" cellpadding="5">
  <tr>
    <td style="width: 250px"><label for="TypeOfCase">Type of case</label><div class="clearfix"></div>
      <h3><asp:Label ID="TypeOfCase" runat="server" Text=""></asp:Label></h3></td>
    <td style="width: 250px"><label for="DateFiled">Date Filed</label><div class="clearfix"></div>
      <h3><asp:Label ID="DateFiled" runat="server" Text=""></asp:Label></h3></td>
    <td><label for="textfield17" style="display:block; float:none">Attachment</label><div class="clearfix"></div>
        <div style="float:left; width:30px;"></div> 
        <asp:Label ID="fileuploaded_1" CssClass="fileuploaded_1" runat="server" Text="" style="float:left; padding-top:3px; display:block"></asp:Label>
        <%--<input id="Attachment1" name="Attachment1" runat="server" type="hidden" value="" />--%></td>
  </tr>
  </table>
<p>&nbsp;</p>
<span style="display:block; float:left">The above pending court case (s) of the vendor </span>
<asp:RadioButtonList ID="legalApproved" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
        <asp:ListItem Value="1" Selected="True">Will</asp:ListItem>
        <asp:ListItem Value="0">Will Not</asp:ListItem>
        </asp:RadioButtonList>
materially pose a business risk exposure to Trans-Asia.<br /><br /><br />



<div class="separator1"></div>
<h3 style="margin:10px 0px;">Comments</h3>
<textarea name="Comment" cols="156" rows="8" id="Comment" style="font-family:Arial, Helvetica, sans-serif; color:#666" runat="server"></textarea>
<script type="text/javascript">
    // <![CDATA[
    $(document).ready(function () {
        $('#FileAttachementFile').uploadify({
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
                $('.FileAttachementLbl').html('<a href="' + response + '" target="_blank">' + response + '</a>');
                $('#ContentPlaceHolder1_FileAttachement').attr('value', response);
            }
        });
    });
    // ]]>
    </script>
        <div style="float:left; width:30px;"><input id="FileAttachementFile" type="file"/></div> 
        <asp:Label ID="FileAttachementLbl" CssClass="FileAttachementLbl" runat="server" Text="File Attachment" style="float:left; padding-top:3px; display:block"></asp:Label>
        <input id="FileAttachement" name="FileAttachement" runat="server" type="hidden" value="" /><div class="clearfix"></div>
<br />
  <br />
  <span style="display:block; float:left">The Vendor is </span>
<asp:RadioButtonList ID="Recommendation" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
        <asp:ListItem Value="1" Selected="True">recommended </asp:ListItem>
        <asp:ListItem Value="0">not recommended </asp:ListItem>
        </asp:RadioButtonList>
for accreditation.<br />
<br />
  <br />


<div class="separator1"></div>
<br />
<br />
<br />
<asp:LinkButton ID="createBt" runat="server" CssClass="bt1" onclientclick="javascript: __doPostBack('continueStp', ''); return false;"><span>REVIEW &raquo;</span></asp:LinkButton>&nbsp;
<a href="legal_VendorList.aspx" class="bt1"><span>BACK</span></a>
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

    </div>

    </div>

</asp:Content>