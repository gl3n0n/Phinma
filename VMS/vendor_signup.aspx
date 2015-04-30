<%@ Page Title="" Language="C#" MasterPageFile="~/MasterSubPage.master" AutoEventWireup="true" CodeFile="vendor_signup.aspx.cs" Inherits="vendor_signup" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
	
	<!--### FORM STYLING ###-->
	<link rel="stylesheet" href="/plugins/jqtransformplugin/jqtransform.css" type="text/css" media="all" />
	<link rel="stylesheet" href="/plugins/jqtransformplugin/demo.css" type="text/css" media="all" />
	<script type="text/javascript" src="plugins/jqtransformplugin/jquery.jqtransform.js" ></script>
	<script language="javascript">
		$(function () {
			//$('form').jqTransform({ imgPath: 'plugins/jqtransformplugin/img/' });
		});
	</script>
	<!--### FORM STYLING ENDS ###-->

	<%--<script src="/plugins/uploadify/jquery-1.4.2.min.js" type="text/javascript"></script>
	<script src="/plugins/uploadify/swfobject.js" type="text/javascript"></script>
	<script src="/plugins/uploadify/jquery.uploadify.v2.1.4.js" type="text/javascript"></script>
	<script src="/plugins/uploadify/jquery.uploadify.v2.1.4.min.js" type="text/javascript"></script>--%>

	<!--### UPLOADIFY ###-->
    <script src="uploadify/swfobject.js" type="text/javascript"></script>
	<script src="uploadify/jquery.uploadify.v2.1.4.js" type="text/javascript"></script>
	<script src="uploadify/jquery.uploadify.v2.1.4.min.js" type="text/javascript"></script>
	<link href="uploadify/uploadify.css" rel="stylesheet" type="text/css" />
	<!--### UPLOADIFY ENDS ###-->
	
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
	        $('#'+id).click();
		}
		jQuery(document).ready(function () {
		    $('input.date').simpleDatepicker({ startdate: '6/10/1900' });
		    $('input.date').keyup(function (e) {
		        //console.log('keyup called');
		        var code = e.keyCode || e.which;
		        if (code == '9') {
		            reloadDatepicker(this.id);
		            //alert();
		        }

		    });
		});
	</script>--%> 
	<!--### DATEPICKER ENDS###-->
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
    <script type="text/javascript">
        $(document).ready(function () {

            //$(".date").parent().css('background-image', 'url(images/calendar_icon1.png)');
            $(".date").parent().css('background-image', "none");
            //$(".date").css({ 'background-image': "images/calendar_icon1.png", 'background-repeat': 'no-repeat', 'background-position': 'center right' });
            //$(".date").parent().css('backgroundRepeat', 'no-repeat');
        });
	</script>
	<script type="text/javascript" src="Scripts/jquery.table.addrow.js" ></script>
<link href="Styles/ava_pages.css" rel="stylesheet" type="text/css" />

<form id="form1" runat="server">
<div class="content" style="width:550px;">
<div class="content_logo">
<img src="images/final_logo_w.png"  border="0" />
</div>
<div style="background:#FFF; min-height:445px; padding:10px;" class="rounded-corners menu">
<!--##################-->
<!--BODY CONTENT STARTS-->
<div style="margin:10px 0px; color:#333; font-size:18px; width:450px; float:left;">Vendor Information</div>
<div class="clearfix" style="font-size:12px;">
<%--New applications to be filled are subject to pay the data processing fee again. Your 
	Vendor Accrediation application is subject to approval by the Trans-Asia Automated 
	Vendor Accreditation administrator. Once approved, your username and password 
	will be sent to you via email.--%>
</div>

<!--Business activities STARTS-->
<div class="separator1"></div>
<h3 style="margin:10px 0px;" runat="server" id="titleRegister"><b>Register your business</b></h3>
<asp:Label ID="errNotification" runat="server" Font-Bold="True" Text="" 
		style="display:block; padding:0px 5px; "></asp:Label>
<asp:Label ID="errNotification2" runat="server" Font-Bold="True" Text="" 
		style="display:block; padding:10px 5px; "></asp:Label>

<br />

<div style="width:450px; margin:auto;">
<table border="0" cellspacing="0" cellpadding="5" id="tblForm" runat="server">
  <tr class="alt">
	<td style="width: 378px"><label for="CompanyName"><b>Company name</b></label>
	  <div class="clearfix"></div>
<input name="CompanyName" type="text" id="CompanyName" size="60" runat="server" 
			causesvalidation="False" maxlength="100" /><div class="clearfix"></div>
<span style="font-size:12px; font-style:italic;">Registered business name (Exact name as in SEC Registration/DTI)</span></td>
	</tr>
  <tr>
	<td style="width: 378px"><label for="CompanyName"><b>Date Established</b><span> (d/m/yyyy)</span></label>
	  <div class="clearfix"></div>
<input name="DateStarted" type="text" id="DateStarted" class="date" runat="server" title="dd/mm/yyyy" 
			causesvalidation="False" style="background:url(images/calendar_icon1.png) no-repeat center right" readonly="readonly" /><%--<img src="images/calendar_icon1.png" style="height: 22px; width: 21px; margin-bottom:-7px;" /><div class="clearfix"></div>--%>
<%--span style="font-size:12px; font-style:italic;">Registered business name (Exact name as in SEC Registration/DTI)</span>--%>
	</td>
	</tr>
  <tr  class="alt" style="display:none;">
	<td style="width: 378px"><label for="CompanyName"><b>Financial statement</b></label>
	  <div class="clearfix"></div>
		<asp:RadioButtonList ID="FinancialStatement" runat="server">
			<asp:ListItem Value="Yes" Selected="True"> If latest Financial statement covers 12 months</asp:ListItem>
			<asp:ListItem Value="No"> If latest Financial statement covers less than 12 months, or no FS available.</asp:ListItem>
		</asp:RadioButtonList>
		<div class="clearfix"></div>
	</td>
	</tr>
  <tr class="alt">
	<td style="width: 378px"><label for="EmailAdd"><b>Email</b></label>
	  <div class="clearfix"></div>
<input name="EmailAdd" type="text" id="EmailAdd" size="60" runat="server" maxlength="100" />

</td>
</tr>
	
  <tr>
	<td style="width: 378px; padding:0px;">
		<%--<label for="CategoryId">Core Business</label>--%>



	  <div class="clearfix"></div>
	  <%--<asp:DropDownList ID="CategoryId" runat="server" 
			DataSourceID="dsProductCategoryId" DataTextField="CategoryName" 
			DataValueField="CategoryId">
	</asp:DropDownList>
	<asp:SqlDataSource ID="dsProductCategoryId" runat="server" ConnectionString="<%$ ConnectionStrings:AVAConnectionString %>" SelectCommand="SELECT '0' CategoryId, '--SELECT A CATEGORY--' CategoryName FROM rfcProductCategory UNION SELECT CategoryId, CategoryName FROM rfcProductCategory">
</asp:SqlDataSource>--%>
		<table border="0" width="100%" cellspacing="0" cellpadding="5" class="atable">
        <tr>
	        <td style="width: 213px">
		        <label for="nature_of_business"><b>Core Business</b></label></td>
	        <td valign="bottom" style="width: 32px">
		        <input id="CategoryCounter" class="rowCount" name="CategoryCounter" type="hidden" />
		        <a href="javascript:void(0)" value="Add Row" class="alternativeRow" runat="server" id="add1">+Add</a></td>
	    </tr>
		<asp:Repeater ID="repeaterSelectedCategory" runat="server" DataSourceID="dsSelectedCategory" >
		<ItemTemplate>
        <tr>
	        <td style="border-bottom:1px dotted #ccc; height:30px; width: 213px;">
		        <select id="CategoryId" name="CategoryId" style="z-index:1000">
		        <asp:Repeater ID="repeaterCategory" runat="server" DataSourceID="dsrfcCategory" >
		        <ItemTemplate>
		        <option value="<%# DataBinder.Eval(Container.DataItem, "CategoryId") %>" <%# (DataBinder.Eval(Container.DataItem, "CategoryId")).ToString() == (DataBinder.Eval(((RepeaterItem)Container.Parent.Parent).DataItem, "CategoryId")).ToString() ? "selected='selected'" : ""%> ><%#DataBinder.Eval(Container.DataItem, "CategoryName")%></option>
		        </ItemTemplate>
		        </asp:Repeater>
		        </select>
	        </td>
	        <td style="border-bottom:1px dotted #ccc; height:30px;">
		        <img src="images/trash.png" width="9" height="13" border="0" class="delRow" /></td>
	    </tr>
		</ItemTemplate>
		</asp:Repeater>
	</table>
	<asp:SqlDataSource ID="dsrfcCategory" runat="server" ConnectionString="<%$ ConnectionStrings:AVAConnectionString %>"
			SelectCommand="SELECT '0' as CategoryId, 'Select Category' as CategoryName, 1 as Visible UNION SELECT CategoryId, CategoryName, Visible FROM rfcProductCategory WHERE Visible = 1" >
	</asp:SqlDataSource>
	<asp:SqlDataSource ID="dsSelectedCategory" runat="server" ConnectionString="<%$ ConnectionStrings:AVAConnectionString %>"  ></asp:SqlDataSource>
		
    <script type="text/javascript">
        $("document").ready(function () {
            $(".alternativeRow").btnAddRow({ inputBoxAutoNumber: true, inputBoxAutoId: true, displayRowCountTo: "rowCount" });
            $(".delRow").btnDelRow();
        });
    </script>
	
</td>
	</tr>
  <tr class="alt">
	<td style="width: 378px"><label for="attachment"><b>Attach Letter of Intent</b></label>
	  <div class="clearfix"></div>
		<%--<asp:FileUpload ID="LOIFileName" runat="server" />--%>
		<script type="text/javascript">
		    // <![CDATA[
		    $(document).ready(function () {
		        $('#fileUpload1').uploadify({
		            'uploader': 'uploadify/uploadify.swf',
		            'script': 'upload_signup.ashx',

		            'cancelImg': 'uploadify/cancel.png',
		            'auto': true,
		            'multi': true,
		            'fileDesc': 'Attach File',
		            'fileExt': '*.jpg;*.png;*.gif;*bmp;*.jpeg;*.doc;*.docx;*.xls;*.xlsx;*.zip;*.rar;*.ppt;*.pdf;*.pdf;*.PDF',
		            'queueSizeLimit': 1,
		            'sizeLimit': 2000000,
		            'folder': 'uploads/Applicants',
		            'onComplete': function (event, queueID, fileObj, response, data) {
		                //alert(response);
		                $('.fileuploaded1').html('<a href="' + response + '" target="_blank">Attach file</a>');
		                $('#<%=LOIFileName.ClientID %>').attr('value', response);
					    $('#LOIFileNamex').show();
					}
			    });
			});
	</script>
		<div style="float:left; width:30px;"><input id="fileUpload1" type="file"/></div> 
		<asp:Label ID="fileuploaded1" CssClass="fileuploaded1" runat="server" Text="Attach file" style="float:left; padding-top:3px; display:block"></asp:Label>   <img src="images/xicon.png" style="margin-left:10px; padding-top:5px;display:none;" id="LOIFileNamex" onclick="$('#<%= LOIFileName.ClientID %>').val('');$('#<%= fileuploaded1.ClientID %>').html('Attach file');$(this).hide();" />
		<input id="LOIFileName" name="LOIFileName" type="hidden" runat="server" value='<%# Eval("Filename")%>' />
	  </td>
	</tr>
  <tr>
	<td style="width: 378px">
	<%--<label for="captcha">Captcha</label>
	  <div class="clearfix"></div>
	<div style="font-size:10px;">Just to prove you are a human, please answer the
following math challenge. </div>
	  <div class="clearfix"></div>
		<uc:ASPNET_Captcha ID="ucCaptcha" runat="server" Align = "Middle" Color = "#FF0000" style="float:left;" />--%>
		<%--<uc:ASPNET_Captcha ID="ASPNET_Captcha1" runat="server" Align = "Middle" Color = "#FF0000" />--%>
		<%--<asp:TextBox ID="txtCaptcha" runat="server" size="10"></asp:TextBox>--%>
	  <div class="clearfix"></div>
		<asp:Label ID="lblmsg" runat="server" Font-Bold="True" 
	ForeColor="Red" Text=""></asp:Label>
		 <br />
	<asp:Image ID="Image1" runat="server" ImageUrl="~/CImage.aspx"/>
	<br />
	<div style="width:300px">Type the characters you see in this picture. This ensures that a person, not an automated program, is submitting this form.</div> 
	<br />
	<asp:TextBox ID="txtimgcode" runat="server" MaxLength="5"></asp:TextBox>
	<br />
	<%--<asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="Button" />--%>
	
	    <div class="clearfix"><br /><br /></div>
	  </td>
	</tr>
  <tr class="alt">
	<td style="width: 378px">
<a href="login.aspx" class="bt1" style="float:left;" ><asp:Label ID="cancelBtLbl" runat="server" Text="CANCEL" ></asp:Label></a>
<asp:LinkButton ID="createBt" runat="server" CssClass="bt1" 
		onclientclick="javascript: __doPostBack('createBt', ''); return false;" style="float:left;" ><span>APPLY</span></asp:LinkButton>
	</td>
	</tr>
  </table>
</div>

<div class="separator1"></div>

<div class="clearfix" style="font-size:12px;"><br /><p>Already registered? <a href="login.aspx" style="text-decoration:none;">Login here</a></p></div>
	  </div>
<br />
<br />
<br />
<br />
<br />
<br />
<br />
<br />




<!--BODY CONTENT ENDS-->
<!--##################-->
</div>
<div class="clearfix"></div>
</form>
</asp:Content>
