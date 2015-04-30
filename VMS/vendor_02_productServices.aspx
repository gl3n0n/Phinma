<%@ Page Title="" Language="C#" MasterPageFile="~/MasterSubPage.master" AutoEventWireup="true" CodeFile="vendor_02_productServices.aspx.cs" Inherits="vendor_02_productServices" MaintainScrollPositionOnPostback="true" %>
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
<ava:topnav ID="TopNav1" runat="server" /><ava:stepnav ID="StepNav1" runat="server" />
<div style="margin:10px 0px; color:#333; font-size:18px; width:450px; float:left;">Products &amp; Services</div>

<form id="formVendorInfo" runat="server">




<!--Business activities STARTS-->
<div class="separator1"></div>
<h3 style="margin:10px 0px;">1. Business activities</h3>
<table border="0" cellspacing="0" cellpadding="2" class="atable">
  <tr>
    <td style="width: 213px">
        <label for="nature_of_business">Nature of business</label></td>
    <td valign="bottom" style="width: 32px">
        <input id="NatureOfBusinessCounter" class="rowCount" name="NatureOfBusinessCounter" type="hidden" />
        <a href="javascript:void(0)" value="Add Row" class="alternativeRow" runat="server" id="add1">+Add</a></td>
    </tr>
  <asp:Repeater ID="repeaterVendorNatureOfBusiness" runat="server" DataSourceID="dsVendorNatureOfBusiness" >
  <ItemTemplate>
  <tr>
    <td style="border-bottom:1px dotted #ccc; height:30px; width: 213px;">
        <select id="NatureOfBusinessId" name="NatureOfBusinessId" style="z-index:1000">
        <asp:Repeater ID="repeaterNatureOfBusiness" runat="server" DataSourceID="dsrfcNatureOfBusiness" >
        <ItemTemplate>
        <option value="<%# DataBinder.Eval(Container.DataItem, "NatureOfBusinessId") %>" <%# (DataBinder.Eval(Container.DataItem, "NatureOfBusinessId")).ToString() == (DataBinder.Eval(((RepeaterItem)Container.Parent.Parent).DataItem, "NatureOfBusinessId")).ToString() ? "selected='selected'" : ""%> ><%#DataBinder.Eval(Container.DataItem, "NatureOfBusinessName")%></option>
        </ItemTemplate>
        </asp:Repeater>
        </select>
    </td>
    <td style="border-bottom:1px dotted #ccc; height:30px;">
        <img src="images/trash.png" width="9" height="13" border="0" class="delRow" /></td>
    </tr>
    </ItemTemplate>
    </asp:Repeater>

    <asp:Repeater ID="repeaterVendorNatureOfBusiness_Lbl" runat="server" DataSourceID="dsVendorNatureOfBusiness" >
  <ItemTemplate>
  <tr>
    <td style="border-bottom:1px dotted #ccc; height:30px; width: 213px;">
        <h3><asp:Label ID="NatureOfBusinessId_Lbl" runat="server" Text='<%# Eval("NatureOfBusinessName") %>'></asp:Label></h3>
    </td>
    <td style="border-bottom:1px dotted #ccc; height:30px;">&nbsp;</td>
    </tr>
    </ItemTemplate>
    </asp:Repeater>
    </table>
    <asp:SqlDataSource ID="dsrfcNatureOfBusiness" runat="server" ConnectionString="<%$ ConnectionStrings:AVAConnectionString %>"
            SelectCommand="SELECT 0 AS NatureOfBusinessId, '--SELECT A NATURE OF BUSINESS--' AS NatureOfBusinessName, '' AS DateCreated UNION SELECT NatureOfBusinessId, NatureOfBusinessName, DateCreated FROM rfcNatureOfBusiness ORDER BY NatureOfBusinessName" >
	</asp:SqlDataSource>

    <asp:SqlDataSource ID="dsVendorNatureOfBusiness" runat="server" ConnectionString="<%$ ConnectionStrings:AVAConnectionString %>"
            SelectCommand="IF EXISTS (SELECT 1 FROM tblVendorNatureOfBusiness WHERE VendorId = @VendorId AND NatureOfBusinessId<>0) BEGIN SELECT t1.VendorId, t1.NatureOfBusinessId, t1.DateCreated, t2.NatureOfBusinessName FROM tblVendorNatureOfBusiness t1, rfcNatureOfBusiness t2 WHERE t1.VendorId = @VendorId AND t2.NatureOfBusinessId = t1.NatureOfBusinessId  END ELSE BEGIN SELECT @VendorId VendorId, 0 NatureOfBusinessId, '' DateCreated, '' NatureOfBusinessName END" OnSelecting="rptGeneral_Selecting"  >
    <SelectParameters>
        <asp:Parameter Name="VendorId" Type="Int32" />
	</SelectParameters>
	</asp:SqlDataSource>
    <br />
    <div class="clearfix"></div>
    <label>Description of Line of Business</label>
    <div class="clearfix"></div>
    <textarea id="prodServ_DescLineOfBusiness" cols="60" name="prodServ_DescLineOfBusiness" rows="4" runat="server" style="font-family:Arial"></textarea>
    <h3><asp:Label runat="server" ID="prodServ_DescLineOfBusiness_Lbl"></asp:Label></h3>
    <br />
<script type="text/javascript">
    $("document").ready(function () {
        $(".alternativeRow").btnAddRow({ inputBoxAutoNumber: true, inputBoxAutoId: true, displayRowCountTo: "rowCount" });
        $(".delRow").btnDelRow();
    });
</script>


<!--Products/Services applied for accreditation STARTS-->
<div class="separator1"></div>
<h3 style="margin:10px 0px;">2. What different lines (merchandise) do you carry?</h3>
<table width="100%" border="0" cellspacing="0" cellpadding="5" id="atable2">
  <tr>
    <td><label for="select">Main category</label>
      </td>
    <td><label for="select">Sub category</label>
      </td>
    <td><label for="textfield2">Specific (ex: Brand, etc.)</label>
      </td>
    <td><label for="textfield3">No. of years carried</label><div style="font-size:10px; clear:both  ">Numeric only</div>
      </td>
    <td><label for="textfield4">Exclusive <br />Distributorship</label>
      </td>
    <td valign="bottom"><input type="hidden" name="ProductsAndServicesCounter" id="ProductsAndServicesCounter" class="rowCount" /><a href="javascript:void(0)" value="Add Row" class="alternativeRow"  runat="server" id="add2">+Add</a></td>
  </tr>
<asp:Repeater ID="repeaterVendorProductsAndServices" runat="server" DataSourceID="dsVendorProductsAndServices">
<ItemTemplate>
  <tr>
    <td style="border-top: thin #CCC dotted;">
    <select id="CategoryId" name="CategoryId" style="width:200px;" onchange="doCategoryPost(this.id)">
        <asp:Repeater ID="repeaterProductCategory" runat="server" DataSourceID="dsProductCategory">
        <ItemTemplate>
        <option value="<%# DataBinder.Eval(Container.DataItem, "CategoryId") %>" <%# (DataBinder.Eval(Container.DataItem, "CategoryId")).ToString() == (DataBinder.Eval(((RepeaterItem)Container.Parent.Parent).DataItem, "CategoryId")).ToString() ? "selected='selected'" : ""%> ><%#DataBinder.Eval(Container.DataItem, "CategoryName")%></option>
        </ItemTemplate>
        </asp:Repeater>
        </select>
    <asp:SqlDataSource ID="dsProductCategory" runat="server" ConnectionString="<%$ ConnectionStrings:AVAConnectionString %>"
            SelectCommand="SELECT '0' as CategoryId, 'Select Category' as CategoryName UNION SELECT CategoryId, CategoryName FROM rfcProductCategory" >
	    </asp:SqlDataSource>
      </td>
    <td style="border-top: thin #CCC dotted;">
    <select id="SubCategoryId"  name="SubCategoryId" style="width:200px;">
        <asp:Repeater ID="repeaterProductSubCategory" runat="server" DataSourceID="dsProductSubCategory">
        <ItemTemplate>
        <option value="<%# DataBinder.Eval(Container.DataItem, "SubCategoryId") %>" <%# (DataBinder.Eval(Container.DataItem, "SubCategoryId")).ToString() == (DataBinder.Eval(((RepeaterItem)Container.Parent.Parent).DataItem, "SubCategoryId")).ToString() ? "selected='selected'" : ""%> ><%#DataBinder.Eval(Container.DataItem, "SubCategoryName")%></option>
        <%--<option value='<%# Eval("SubCategoryId")%>'><%# Eval("SubCategoryName")%></option>--%>
        </ItemTemplate>
        </asp:Repeater>
        </select>
    <asp:SqlDataSource ID="dsProductSubCategory" runat="server" ConnectionString="<%$ ConnectionStrings:AVAConnectionString %>"
            SelectCommand="SELECT '' as SubCategoryId, 'Select SubCategory' as SubCategoryName UNION SELECT SubCategoryId, SubCategoryName FROM rfcProductSubCategory" >
	    </asp:SqlDataSource>
    </td>
    <td style="display:none;">
    <select id="BrandId" name="BrandId" style="width:200px;">
        <asp:Repeater ID="repeaterProductBrands" runat="server" DataSourceID="dsProductBrands">
        <ItemTemplate>
        <option value="<%# DataBinder.Eval(Container.DataItem, "BrandId") %>" <%# (DataBinder.Eval(Container.DataItem, "BrandId")).ToString() == (DataBinder.Eval(((RepeaterItem)Container.Parent.Parent).DataItem, "BrandId")).ToString() ? "selected='selected'" : ""%> ><%#DataBinder.Eval(Container.DataItem, "BrandName")%></option>
        <option value='<%# Eval("BrandId")%>'><%# Eval("BrandName")%></option>
        </ItemTemplate>
        </asp:Repeater>
        </select>
    <asp:SqlDataSource ID="dsProductBrands" runat="server" ConnectionString="<%$ ConnectionStrings:AVAConnectionString %>"
            SelectCommand="SELECT '' as BrandId, 'Select Brand' as BrandName UNION SELECT BrandId, BrandName FROM rfcProductBrands" >
	    </asp:SqlDataSource>
    </td>
   <td style="border-top: thin #CCC dotted;"><input type="text" name="BrandTxt" id="BrandTxt" size="25" maxlength="100"  value='<%# Eval("BrandTxt")%>' /></td>
    <td style="border-top: thin #CCC dotted;"><input type="text" name="NoYears" id="NoYears" size="3" maxlength="3" class="numeric" onfocus="reloadNumeric()" value='<%# Eval("NoYears")%>' /></td>
    <td style="display:none;" style="border-top: thin #CCC dotted;"><textarea name="MajorClients" id="MajorClients"   style="font-family:Arial;" /><%# Eval("MajorClients")%></textarea></td>

    <td style="width: 138px">
        <select name="Exclusive" id="Exclusive">
	  <option>Select</option>
	  <option value="Yes" <%# Eval("Exclusive").ToString()=="Yes" ? "selected='selected'":"" %> >Yes</option>
	  <option value="No" <%# Eval("Exclusive").ToString()=="No" ? "selected='selected'":"" %> >No</option>
	</select></td>

    <td style="border-top: thin #CCC dotted;" valign="bottom"><img alt="" src="images/trash.png" width="9" height="13" border="0" class="delRow" /></td>
  </tr>
  </ItemTemplate>
</asp:Repeater>

<asp:Repeater ID="repeaterVendorProductsAndServices_Lbl" runat="server" DataSourceID="dsVendorProductsAndServices">
<ItemTemplate>
  <tr>
    <td style="border-top: thin #CCC dotted;">
        <h3><asp:Label runat="server" ID="Label4" Text='<%# Eval("CategoryName")%>'></asp:Label></h3>
      </td>
    <td style="border-top: thin #CCC dotted;">
        <h3><asp:Label runat="server" ID="Label3" Text='<%# Eval("SubCategoryName")%>'></asp:Label></h3>
    </td>
    <td style="border-top: thin #CCC dotted;">
        <h3><asp:Label runat="server" ID="Label2" Text='<%# Eval("BrandTxt")%>'></asp:Label></h3>
    </td>
    <td style="border-top: thin #CCC dotted;">
        <h3><asp:Label runat="server" ID="Label1" Text='<%# Eval("NoYears")%>'></asp:Label></h3>
    </td>
    <td style="border-top: thin #CCC dotted; display:none">
        <h3><asp:Label runat="server" ID="MajorClients_Lbl" Text='<%# Eval("MajorClients")%>'></asp:Label></h3>
    </td>
    <td style="border-top: thin #CCC dotted;">
        <h3><asp:Label runat="server" ID="Exclusive_Lbl" Text='<%# Eval("Exclusive")%>'></asp:Label></h3>
    </td>
    <td style="border-top: thin #CCC dotted;" valign="bottom">&nbsp;</td>
  </tr>
  </ItemTemplate>
</asp:Repeater>
</table><br />
<asp:SqlDataSource ID="dsVendorProductsAndServices" runat="server" ConnectionString="<%$ ConnectionStrings:AVAConnectionString %>"
            SelectCommand="IF EXISTS (SELECT 1 FROM tblVendorProductsAndServices WHERE VendorId = @VendorId) BEGIN SELECT t1.*, t2.CategoryName, t3.SubCategoryName, t4.BrandName  FROM tblVendorProductsAndServices t1 LEFT JOIN rfcProductCategory t2 ON t2.CategoryId = t1.CategoryId LEFT JOIN rfcProductSubCategory t3 ON t3.SubCategoryId = t1.SubCategoryId LEFT JOIN rfcProductBrands t4 ON t4.BrandId = t1.BrandId WHERE t1.VendorId = @VendorId  END ELSE BEGIN SELECT 0 AS ID,'' AS VendorId,'' AS CategoryId, '' AS SubCategoryId, '' AS BrandId,'' AS BrandName,'' AS BrandTxt, '' AS NoYears, '' AS MajorClients, '' AS DateCreated, '' AS CategoryName, '' AS SubCategoryName, '' AS BrandName, '' AS Exclusive END" OnSelecting="rptGeneral_Selecting"  >
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
<script type="application/javascript">
    function doCategoryPost(thisId) {
        //alert(thisId.substring(10));
        $("#SubCategoryId" + thisId.substring(10)).empty();
        //$("#SubCategoryId" + thisId.substring(10)).append($('<option></option>').val("").html("Select SubCategory"));
        $.ajax({
            type: "POST",
            url: "usercontrols/subcategory.aspx",
            data: "CategoryId=" + $("#" + thisId).val(),
            success: function(resp){
                //alert(resp);
                $("#SubCategoryId" + thisId.substring(10)).append(resp);
                },
            error: function(e){
                alert('Error: ' + e);
            }
        });
    }
    function doSubCategoryPost(thisId) {
        $("#BrandId" + thisId.substring(10)).empty();
        $.ajax({
            type: "POST",
            url: "usercontrols/brands.aspx",
            data: "SubCategoryId=" + $("#" + thisId).val(),
            success: function (resp) {
                alert(resp);
                $("#BrandId" + thisId.substring(10)).append(resp);
            },
            error: function (e) {
                alert('Error: ' + e);
            }
        });
    }
</script>
    <%--<script type="application/javascript"> 
        $(document).ready(function () {
            alert($("#add2)
            //for(i=0; i
            //doCategoryPost('');
        });
</script>--%>
<div class="clearfix"></div>
    <label><b>Please furnish us a complete list of the items/products that you carry particularly those items, which are needed by a power plant. If possible, provide, brochures/catalogues/certificate of distributorship)</b></label>
<div class="clearfix"></div>
<script type="text/javascript">
    // <![CDATA[
    $(document).ready(function () {
        FieldFile = 'prodServ_DAC_AttachmentFile';
        FieldLbl = 'prodServ_DAC_AttachmentLbl';
        FieldHidden = 'prodServ_DAC_Attachment';
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
                FieldFile = 'prodServ_DAC_AttachmentFile';
                FieldLbl = 'prodServ_DAC_AttachmentLbl';
                FieldHidden = 'prodServ_DAC_Attachment';
                $('#' + FieldFile + '_btup').show();
            },
            'onCancel': function (event, ID, fileObj, data) {
                FieldFile = 'prodServ_DAC_AttachmentFile';
                FieldLbl = 'prodServ_DAC_AttachmentLbl';
                FieldHidden = 'prodServ_DAC_Attachment';
                $('#' + FieldFile + '_btup').hide();
            },
            'onComplete': function (event, queueID, fileObj, response, data) {
                FieldFile = 'prodServ_DAC_AttachmentFile';
                FieldLbl = 'prodServ_DAC_AttachmentLbl';
                FieldHidden = 'prodServ_DAC_Attachment';
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
        <div style="float:left; width:30px;"><input id="prodServ_DAC_AttachmentFile"  class="prodServ_DAC_AttachmentFile" type="file" runat="server"/></div> 
        <asp:Label ID="prodServ_DAC_AttachmentLbl" CssClass="prodServ_DAC_AttachmentLbl" runat="server" Text="File Attachment" style="float:left; padding-top:3px; display:block"></asp:Label>
        <input id="prodServ_DAC_Attachment" name="prodServ_DAC_Attachment" runat="server" type="hidden" value="" />
        <div style="font-size:9px; clear:both;">(Max size each file: 10 MB), click paperclip again to upload multiple files. </div>
        <a href="javascript:$('#<%= prodServ_DAC_AttachmentFile.ClientID %>').uploadifyUpload();" id="prodServ_DAC_AttachmentFile_btup">Upload Selection</a>

<div class="clearfix"></div>



<!--Products/Services applied for accreditation STARTS-->
<div class="separator1"></div>
<h3 style="margin:10px 0px;">3. Please list down your five (5) major suppliers (retailer, wholesaler, trader)</h3>
<table width="100%" border="0" cellspacing="0" cellpadding="5" id="atable3">
  <tr>
    <td colspan="5">Please list local suppliers including subcontractors and partners with whom you have current transactions up to the last 6 months as trade reference</td>
    </tr>
  <tr>
    <td style="width: 138px"><label for="select">Name of supplier </label></td>
    <td style="width: 138px"><label for="select">Contact person</label> </td>
      <td style="width: 200px"><label for="select">Address</label> </td>
    <td style="width: 138px"><label for="select">Contact No. </label><%--<div style="font-size:10px">Numeric only</div>--%></td>
    <td style="width: 138px"><label for="select">Payment Terms </label></td>
    <td valign="bottom"><input type="hidden" name="SupplierReferencesCounter" id="SupplierReferencesCounter" class="rowCount" /><a href="javascript:void(0)" value="Add Row" class="alternativeRow" runat="server" id="add3">+Add</a></td>
  </tr>
  <asp:Repeater ID="repeaterSupplierReferences" runat="server" DataSourceID="dsVendorSupplierReferences">
<ItemTemplate>
  <tr >
    <td style="border-top: thin #CCC dotted;">
      <input name="SupplierName" type="text" id="SupplierName" size="17" value="<%# Eval("SupplierName")%>" maxlength="100" /></td>
    <td style="border-top: thin #CCC dotted;">
      <input name="ContactPerson" type="text" id="ContactPerson" size="17" value="<%# Eval("ContactPerson")%>" maxlength="100" /></td>
    <td style="border-top: thin #CCC dotted;">
        <input name="majaddress" type="text" id="majaddress" size="27" value="<%# Eval("majaddress")%>" maxlength="100" /></td>
    <td style="border-top: thin #CCC dotted;">
      <input name="ContactNo" type="text" id="ContactNo" size="17" value="<%# Eval("ContactNo")%>" maxlength="100" /></td>
    <td style="border-top: thin #CCC dotted;">
      <input name="Terms" type="text" id="Terms" size="17" value="<%# Eval("Terms")%>" maxlength="30" /></td>
    <td style="border-top: thin #CCC dotted;" valign="bottom"><img alt="" src="images/trash.png" width="9" height="13" border="0" class="delRow" /></td>
  </tr>
    </ItemTemplate>
</asp:Repeater>

    <asp:Repeater ID="repeaterSupplierReferences_Lbl" runat="server" DataSourceID="dsVendorSupplierReferences">
<ItemTemplate>
  <tr >
    <td style="border-top: thin #CCC dotted;">
      <h3><asp:Label runat="server" ID="Label2" Text='<%# Eval("SupplierName")%>'></asp:Label></h3>
    </td>
    <td style="border-top: thin #CCC dotted;">
        <h3><asp:Label runat="server" ID="Label5" Text='<%# Eval("ContactPerson")%>'></asp:Label></h3>
    </td>
      <td style="border-top: thin #CCC dotted;">
        <h3><asp:Label runat="server" ID="Label12" Text='<%# Eval("majaddress")%>'></asp:Label></h3>
    </td>
    <td style="border-top: thin #CCC dotted;">
        <h3><asp:Label runat="server" ID="Label6" Text='<%# Eval("ContactNo")%>'></asp:Label></h3>
    </td>
    <td style="border-top: thin #CCC dotted;">
        <h3><asp:Label runat="server" ID="Label7" Text='<%# Eval("Terms")%>'></asp:Label></h3>
    </td>
    <td style="border-top: thin #CCC dotted;" valign="bottom">&nbsp;</td>
  </tr>
    </ItemTemplate>
</asp:Repeater>
  </table>
<br />
<asp:SqlDataSource ID="dsVendorSupplierReferences" runat="server" ConnectionString="<%$ ConnectionStrings:AVAConnectionString %>"
            SelectCommand="IF EXISTS (SELECT 1 FROM tblVendorSupplierReferences WHERE VendorId = @VendorId) BEGIN SELECT * FROM tblVendorSupplierReferences WHERE VendorId = @VendorId END ELSE BEGIN SELECT 0 AS ID,'' AS VendorId,'' AS SupplierName, '' AS ContactPerson,'' AS majaddress, '' AS ContactNo, '' AS Terms, '' AS DateCreated UNION SELECT 1 AS ID,'' AS VendorId,'' AS SupplierName, '' AS ContactPerson,'' AS majaddress, '' AS ContactNo, '' AS Terms, '' AS DateCreated UNION SELECT 2 AS ID,'' AS VendorId,'' AS SupplierName, '' AS ContactPerson, '' AS majaddress,'' AS ContactNo, '' AS Terms, '' AS DateCreated UNION SELECT 3 AS ID,'' AS VendorId,'' AS SupplierName, '' AS ContactPerson, '' AS majaddress,'' AS ContactNo, '' AS Terms, '' AS DateCreated UNION SELECT 4 AS ID,'' AS VendorId,'' AS SupplierName, '' AS ContactPerson, '' AS majaddress,'' AS ContactNo, '' AS Terms, '' AS DateCreated END" OnSelecting="rptGeneral_Selecting"  >
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

<div style="display:none">
<!--Business activities STARTS-->
<div class="separator1"></div>
<h3 style="margin:10px 0px;">4. Top competitors</h3>
<table  border="0" cellspacing="0" cellpadding="2" id="atable4">
  <tr>
    <td width="471"><label for="nature_of_business">Company name</label></td>
    <td width="28"><input type="hidden" name="TopCompetitorsCounter" id="TopCompetitorsCounter" class="rowCount" /><a href="javascript:void(0)" value="Add Row" class="alternativeRow" runat="server" id="add4">+Add</a></td>
  </tr>
  <asp:Repeater ID="repeaterTopCompetitors" runat="server" DataSourceID="dsVendorTopCompetitors">
<ItemTemplate>
  <tr>
    <td style="border-top: thin #CCC dotted;"> <input name="CompanyName" type="text" id="CompanyName" size="60" value="<%# Eval("CompanyName")%>" /></td>
    <td style="border-top: thin #CCC dotted;"><img alt="" src="images/trash.png" width="9" height="13" border="0" class="delRow" /></td>
  </tr>
  </ItemTemplate>
</asp:Repeater>

    <asp:Repeater ID="repeaterTopCompetitors_Lbl" runat="server" DataSourceID="dsVendorTopCompetitors">
<ItemTemplate>
  <tr>
    <td style="border-top: thin #CCC dotted;">
        <h3><asp:Label runat="server" ID="Label7" Text='<%# Eval("CompanyName")%>'></asp:Label></h3>
    </td>
    <td style="border-top: thin #CCC dotted;">&nbsp;</td>
  </tr>
  </ItemTemplate>
</asp:Repeater>
  </table>
<br />
<asp:SqlDataSource ID="dsVendorTopCompetitors" runat="server" ConnectionString="<%$ ConnectionStrings:AVAConnectionString %>"
            SelectCommand="IF EXISTS (SELECT 1 FROM tblVendorTopCompetitors WHERE VendorId = @VendorId) BEGIN SELECT * FROM tblVendorTopCompetitors WHERE VendorId = @VendorId END ELSE BEGIN SELECT 0 AS ID,'' AS VendorId,'' AS CompanyName, '' AS DateCreated END" OnSelecting="rptGeneral_Selecting"  >
    <SelectParameters>
        <asp:Parameter Name="VendorId" Type="Int32" />
	</SelectParameters>
  </asp:SqlDataSource>
</div>


<!--Products/Services applied for accreditation STARTS-->
<div class="separator1"></div>
<h3 style="margin:10px 0px;">4. Please list down your five (5) major customers:</h3>
<table border="0" cellspacing="0" cellpadding="5" width="100%" >
  <tr>
    <td colspan="5" style="height: 40px">Please list local customers with whom you have current transactions up to the last 6 months as trade references:<br />
Select the major ones.</td>
    </tr>
  <tr>
    <td style="width: 138px"><label for="select">Name of customer </label></td>
    <td style="width: 138px"><label for="select">Contact person</label> </td>
      <td style="width: 200px"><label for="select">Address</label> </td>
    <td style="width: 138px"><label for="select">Contact No. </label><%--<div style="font-size:10px">Numeric only</div>--%></td>
    <td style="width: 138px"><label for="select">Payment Terms </label></td>
    <td valign="bottom"><input type="hidden" name="CustomerReferencesCounter" id="CustomerReferencesCounter" class="rowCount" value="5" /><a href="javascript:void(0)" value="Add Row" class="alternativeRow"  runat="server" id="add5">+Add</a></td>
  </tr>
  <asp:Repeater ID="repeaterCustomerReferences" runat="server" DataSourceID="dsVendorCustomerReferences">
<ItemTemplate>
  <tr >
    <td style="border-top: thin #CCC dotted;">
      <input name="custrefCustomerName" type="text" id="custrefCustomerName" size="17" value="<%# Eval("custrefCustomerName")%>" maxlength="100" /></td>
    <td style="border-top: thin #CCC dotted;">
      <input name="custrefContactPerson" type="text" id="custrefContactPerson" size="17" value="<%# Eval("custrefContactPerson")%>" maxlength="100" /></td>
    <td style="border-top: thin #CCC dotted;">
         <input name="custrefAddress" type="text" id="custrefAddress" size="27" value="<%# Eval("custrefAddress")%>" maxlength="100" /></td>
    <td style="border-top: thin #CCC dotted;">
      <input name="custrefContactNo" type="text" id="custrefContactNo" size="17" value="<%# Eval("custrefContactNo")%>" maxlength="100" /></td>
    <td style="border-top: thin #CCC dotted;">
      <input name="custrefTerms" type="text" id="custrefTerms" size="17" value="<%# Eval("custrefTerms")%>" maxlength="30" /></td>
    <td style="border-top: thin #CCC dotted;" valign="bottom"><img alt="" src="images/trash.png" width="9" height="13" border="0" class="delRow" /></td>
  </tr>
    </ItemTemplate>
</asp:Repeater>

  <asp:Repeater ID="repeaterCustomerReferences_Lbl" runat="server" DataSourceID="dsVendorCustomerReferences">
<ItemTemplate>
  <tr >
    <td style="border-top: thin #CCC dotted;">
        <h3><asp:Label runat="server" ID="Label7" Text='<%# Eval("custrefCustomerName")%>'></asp:Label></h3>
    </td>
    <td style="border-top: thin #CCC dotted;">
        <h3><asp:Label runat="server" ID="Label8" Text='<%# Eval("custrefContactPerson")%>'></asp:Label></h3>
    </td>
       <td style="border-top: thin #CCC dotted;">
        <h3><asp:Label runat="server" ID="Label11" Text='<%# Eval("custrefAddress")%>'></asp:Label></h3>
    </td>
    <td style="border-top: thin #CCC dotted;">
        <h3><asp:Label runat="server" ID="Label9" Text='<%# Eval("custrefContactNo")%>'></asp:Label></h3>
    </td>
    <td style="border-top: thin #CCC dotted;">
        <h3><asp:Label runat="server" ID="Label10" Text='<%# Eval("custrefTerms")%>'></asp:Label></h3>
    </td>
    <td style="border-top: thin #CCC dotted;" valign="bottom">&nbsp;</td>
  </tr>
    </ItemTemplate>
</asp:Repeater>
  </table>
<br />
<asp:SqlDataSource ID="dsVendorCustomerReferences" runat="server" ConnectionString="<%$ ConnectionStrings:AVAConnectionString %>"
            SelectCommand="IF EXISTS (SELECT 1 FROM tblVendorCustomerReferences WHERE VendorId = @VendorId) BEGIN SELECT * FROM tblVendorCustomerReferences WHERE VendorId = @VendorId END ELSE BEGIN SELECT 0 AS ID,'' AS VendorId,'' AS custrefCustomerName, '' AS custrefContactPerson, '' AS custrefAddress,'' AS custrefContactNo, '' AS custrefTerms, '' AS DateCreated
    
     UNION SELECT 1 AS ID,'' AS VendorId,'' AS custrefCustomerName, '' AS custrefContactPerson, '' AS custrefAddress,'' AS custrefContactNo, '' AS custrefTerms, '' AS DateCreated           
    UNION SELECT 2 AS ID,'' AS VendorId,'' AS custrefCustomerName, '' AS custrefContactPerson, '' AS custrefAddress,'' AS custrefContactNo, '' AS custrefTerms, '' AS DateCreated 
    
    UNION SELECT 3 AS ID,'' AS VendorId,'' AS custrefCustomerName, '' AS custrefContactPerson, '' AS custrefAddress,'' AS custrefContactNo, '' AS custrefTerms, '' AS DateCreated
    
     UNION SELECT 4 AS ID,'' AS VendorId,'' AS custrefCustomerName, '' AS custrefContactPerson, '' AS custrefAddress,'' AS custrefContactNo, '' AS custrefTerms, '' AS DateCreated END" OnSelecting="rptGeneral_Selecting"  >
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


<div class="separator1"></div>
<br />
<br />
<br />
<asp:LinkButton ID="createBt" runat="server" CssClass="bt1" onclientclick="javascript: __doPostBack('continueStp', ''); return false;"><span>SAVE &amp; CONTINUE STEP 3 &raquo;</span></asp:LinkButton>&nbsp;<asp:LinkButton ID="createBt1" runat="server" CssClass="bt1"  onclientclick="javascript: __doPostBack('justSave', ''); return false;"><span>SAVE</span></asp:LinkButton>&nbsp;
<a href="vendor_01_vendorInfo.aspx<%= queryString %>" class="bt1"><span>BACK</span></a>
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

    </b>

</asp:Content>