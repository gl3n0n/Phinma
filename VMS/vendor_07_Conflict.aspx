<%@ Page Title="" Language="C#" MasterPageFile="~/MasterSubPage.master" AutoEventWireup="true" CodeFile="vendor_07_Conflict.aspx.cs" Inherits="vendor_07_Conflict" %>
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
<div style="margin:10px 0px; color:#333; font-size:18px; width:450px; float:left;">Conflict of Interest</div>

<form id="formVendorInfo" runat="server">


<div class="separator1"></div>
<h3 style="margin:10px 0px;">1. Conflict of interest declaration</h3>


<div style="display:none">
<table width="100%" border="0" cellpadding="5" cellspacing="0">
<tr>
    <td style="height: 25px"><strong>DPlease list down names of relatives or acquaintances connected with any of our companies?<br />If yes, please indicate so:</strong>
    </td>
  </tr>
  <tr>
    <td><strong>
          <asp:RadioButtonList ID="YesNo1" runat="server" 
            RepeatDirection="Horizontal" RepeatLayout="Flow">
            <asp:ListItem Value="1">Yes</asp:ListItem>
            <asp:ListItem Value="0" Selected="True">No</asp:ListItem>
        </asp:RadioButtonList>
        </strong>
        <h3><asp:Label runat="server" ID="YesNo1_Lbl"></asp:Label></h3>
    </td>
    </tr>
</table>
<table width="75%" border="0" cellpadding="5" cellspacing="0">
  <tr>
    <td><strong>Nature of business</strong></td>
    <td><strong>Trans-Asia Competitor Name</strong></td>
    <td><strong>No. of years</strong></td>
    <td><input type="hidden" name="ConflictOfInterestCounter1" id="ConflictOfInterestCounter1" class="rowCount" /><a href="javascript:void(0)" value="Add Row" class="alternativeRow">+Add</a></td>
  </tr>
  <asp:Repeater ID="repeaterConflictOfInterest1" runat="server" DataSourceID="dsVendorConflictOfInterest1" >
  <ItemTemplate>
  <tr>
    <td><select id="1NatureOfBusinessId" name="1NatureOfBusinessId" style="width:200px;">
        <asp:Repeater ID="repeaterNatureOfBusiness1" runat="server" DataSourceID="dsrfcNatureOfBusiness" >
        <ItemTemplate>
        <option value="<%# DataBinder.Eval(Container.DataItem, "NatureOfBusinessId") %>" <%# (DataBinder.Eval(Container.DataItem, "NatureOfBusinessId")).ToString() == (DataBinder.Eval(((RepeaterItem)Container.Parent.Parent).DataItem, "NatureOfBusinessId")).ToString() ? "selected='selected'" : ""%> ><%#DataBinder.Eval(Container.DataItem, "NatureOfBusinessName")%></option>
        </ItemTemplate>
        </asp:Repeater>
        </select></td>
    <td><input name="1CompetitorName" type="text" id="1CompetitorName" size="28" value="<%# Eval("CompetitorName")%>" /></td>
    <td><input name="1NoYears" type="text" id="1NoYears" size="8" class="integer" onfocus="reloadNumeric()" value="<%# Eval("NoYears")%>" /></td>
    <td valign="bottom"><img alt="" src="images/trash.png" width="9" height="13" border="0" class="delRow" /></td>
  </tr>
    </ItemTemplate>
    </asp:Repeater>

  <asp:Repeater ID="repeaterConflictOfInterest1_Lbl" runat="server" DataSourceID="dsVendorConflictOfInterest1" >
  <ItemTemplate>
  <tr>
    <td><h3><asp:Label runat="server" ID="a1NatureOfBusinessId_Lbl" Text='<%# Eval("NatureOfBusinessName") %>' ></asp:Label></h3></td>
    <td><h3><asp:Label runat="server" ID="a1CompetitorName_Lbl" Text='<%# Eval("CompetitorName") %>' ></asp:Label></h3></td>
    <td><h3><asp:Label runat="server" ID="a1NoYears_Lbl" Text='<%# Eval("NoYears") %>'></asp:Label></h3></td>
    <td valign="bottom">&nbsp;</td>
  </tr>
    </ItemTemplate>
    </asp:Repeater>
  </table>
<br />
<asp:SqlDataSource ID="dsrfcNatureOfBusiness" runat="server" ConnectionString="<%$ ConnectionStrings:AVAConnectionString %>"
            SelectCommand="SELECT 0 AS NatureOfBusinessId, '--SELECT A NATURE OF BUSINESS--' AS NatureOfBusinessName, '' AS DateCreated UNION SELECT NatureOfBusinessId, NatureOfBusinessName, DateCreated FROM rfcNatureOfBusiness ORDER BY NatureOfBusinessId" >
	</asp:SqlDataSource>
  <asp:SqlDataSource ID="dsVendorConflictOfInterest1" runat="server" ConnectionString="<%$ ConnectionStrings:AVAConnectionString %>"
            SelectCommand="IF EXISTS (SELECT 1 FROM tblVendorConflictOfInterest WHERE VendorId = @VendorId AND Description='Q1' AND NatureOfBusinessId <> 0 AND CompetitorName IS NOT NULL AND NoYears <> 0) BEGIN SELECT t1.*, t2.NatureOfBusinessName FROM tblVendorConflictOfInterest t1, rfcNatureOfBusiness t2 WHERE t1.VendorId = @VendorId AND t1.Description='Q1' AND t2.NatureOfBusinessId=t1.NatureOfBusinessId END ELSE BEGIN SELECT 0 AS ID,'' AS VendorId, '' AS Description, '' AS YesNo, '' AS NatureOfBusinessId, '' AS NatureOfBusinessName, '' AS CompetitorName, '' AS NoYears, '' AS DateCreated END"  OnSelecting="rptGeneral_Selecting"  >
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
</div>

<div style="display:none">
<div class="separator1"></div>
<table width="100%" border="0" cellpadding="5" cellspacing="0">
<tr>
    <td style="height: 25px"><strong>Any Executives / Directors in your company with direct / indirect business with Trans-Asia competitors (PLDT/Smart/Digitel/LibertyTelecom/Others)? <br />If yes, please indicate so:</strong>
    </td>
  </tr>
  <tr>
    <td><strong>
          <asp:RadioButtonList ID="YesNo2" runat="server" 
            RepeatDirection="Horizontal" RepeatLayout="Flow">
            <asp:ListItem Value="1">Yes</asp:ListItem>
            <asp:ListItem Value="0" Selected="True">No</asp:ListItem>
        </asp:RadioButtonList>
        </strong>
        <h3><asp:Label runat="server" ID="YesNo2_Lbl"></asp:Label></h3>
    </td>
    </tr>
</table>
<table width="75%" border="0" cellpadding="5" cellspacing="0">
  <tr>
    <td><strong>Vendor Executive / Director Name</strong></td>
    <td><strong>Position</strong></td>
    <td><strong>Nature of business</strong></td>
    <td><input type="hidden" name="ConflictOfInterestCounter2" id="ConflictOfInterestCounter2" class="rowCount" /><a href="javascript:void(0)" value="Add Row" class="alternativeRow">+Add</a></td>
  </tr>
  <asp:Repeater ID="repeaterConflictOfInterest2" runat="server" DataSourceID="dsVendorConflictOfInterest2" >
  <ItemTemplate>
  <tr>
    <td><input name="2CompetitorName" type="text" id="2CompetitorName" size="28" value="<%# Eval("CompetitorName")%>" /></td>
    <td><input name="Position" type="text" id="Position" size="8" value="<%# Eval("Position")%>" /></td>
    <td><select id="2NatureOfBusinessId" name="2NatureOfBusinessId" style="width:200px;">
        <asp:Repeater ID="repeaterNatureOfBusiness2" runat="server" DataSourceID="dsrfcNatureOfBusiness" >
        <ItemTemplate>
        <option value="<%# DataBinder.Eval(Container.DataItem, "NatureOfBusinessId") %>" <%# (DataBinder.Eval(Container.DataItem, "NatureOfBusinessId")).ToString() == (DataBinder.Eval(((RepeaterItem)Container.Parent.Parent).DataItem, "NatureOfBusinessId")).ToString() ? "selected='selected'" : ""%> ><%#DataBinder.Eval(Container.DataItem, "NatureOfBusinessName")%></option>
        </ItemTemplate>
        </asp:Repeater>
        </select></td>
    <td valign="bottom"><img alt="" src="images/trash.png" width="9" height="13" border="0" class="delRow" /></td>
  </tr>
    </ItemTemplate>
    </asp:Repeater>

  <asp:Repeater ID="repeaterConflictOfInterest2_Lbl" runat="server" DataSourceID="dsVendorConflictOfInterest2" >
  <ItemTemplate>
  <tr>
    <td><h3><asp:Label runat="server" ID="a2CompetitorName_Lbl" Text='<%# Eval("CompetitorName") %>' ></asp:Label></h3></td>
    <td><h3><asp:Label runat="server" ID="a2NoYears_Lbl" Text='<%# Eval("Position") %>'></asp:Label></h3></td>
    <td><h3><asp:Label runat="server" ID="a2NatureOfBusinessId_Lbl" Text='<%# Eval("NatureOfBusinessName") %>' ></asp:Label></h3></td>
    <td valign="bottom">&nbsp;</td>
  </tr>
    </ItemTemplate>
    </asp:Repeater>
  </table>
<br />
  <asp:SqlDataSource ID="dsVendorConflictOfInterest2" runat="server" ConnectionString="<%$ ConnectionStrings:AVAConnectionString %>"
            SelectCommand="IF EXISTS (SELECT 1 FROM tblVendorConflictOfInterest WHERE VendorId = @VendorId AND Description='Q2' AND NatureOfBusinessId <> 0 AND CompetitorName IS NOT NULL AND Position IS NOT NULL) BEGIN SELECT t1.*, t2.NatureOfBusinessName FROM tblVendorConflictOfInterest t1, rfcNatureOfBusiness t2 WHERE t1.VendorId = @VendorId AND t1.Description='Q2' AND t2.NatureOfBusinessId=t1.NatureOfBusinessId END ELSE BEGIN SELECT 0 AS ID,'' AS VendorId, '' AS Description, '' AS YesNo, '' AS NatureOfBusinessId, '' AS NatureOfBusinessName, '' AS CompetitorName, '' AS Position, '' AS DateCreated END" OnSelecting="rptGeneral_Selecting"  >
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
</div>



<table width="100%" border="0" cellpadding="5" cellspacing="0">
<tr>
    <td style="height: 25px"><strong>Please list down names of relatives or acquaintances connected with any of our companies?</strong>
    </td>
  </tr>
  <tr style="display:none">
    <td><strong>
          <asp:RadioButtonList ID="YesNo3" runat="server" 
            RepeatDirection="Horizontal" RepeatLayout="Flow">
            <asp:ListItem Value="1">Yes</asp:ListItem>
            <asp:ListItem Value="0" Selected="True">No</asp:ListItem>
        </asp:RadioButtonList>
        </strong>
        <h3><asp:Label runat="server" ID="YesNo3_Lbl"></asp:Label></h3>
    </td>
    </tr>
</table>
<table width="75%" border="0" cellpadding="5" cellspacing="0">
  <tr>
    <%--<td><strong>Vendor Executive / Director Name</strong></td>--%>
    <td><strong>Employee Name</strong></td>
    <td><strong>Employee Position</strong></td>
    <td><strong> Relationship</strong></td>
    <td><input type="hidden" name="ConflictOfInterestCounter3" id="ConflictOfInterestCounter3" class="rowCount" /><a href="javascript:void(0)" value="Add Row" class="alternativeRow">+Add</a></td>
  </tr>
  <asp:Repeater ID="repeaterConflictOfInterest3" runat="server" DataSourceID="dsVendorConflictOfInterest3" >
  <ItemTemplate>
  <tr>
    <%--<td>
        <input name="3CompetitorName" type="text" id="3CompetitorName" size="20" value="<%# Eval("CompetitorName")%>" />
    </td>--%>
    <td><input name="3Position" type="text" id="3Position" size="15"  value="<%# Eval("Position")%>" /></td>
    <td><input name="3GTEmployee" type="text" id="3GTEmployee" size="20" value="<%# Eval("GTEmployee")%>" /></td>
    <td><input name="3Relationship" type="text" id="3Relationship" size="15" value="<%# Eval("Relationship")%>" /></td>
    <td valign="bottom"><img alt="" src="images/trash.png" width="9" height="13" border="0" class="delRow" /></td>
  </tr>
    </ItemTemplate>
    </asp:Repeater>

  <asp:Repeater ID="repeaterConflictOfInterest3_Lbl" runat="server" DataSourceID="dsVendorConflictOfInterest3" >
  <ItemTemplate>
  <tr>
    <td><h3><asp:Label runat="server" ID="a3Position_Lbl" Text='<%# Eval("Position") %>' ></asp:Label></h3></td>
    <td><h3><asp:Label runat="server" ID="a3GTEmployee_Lbl" Text='<%# Eval("GTEmployee") %>' ></asp:Label></h3></td>
    <td><h3><asp:Label runat="server" ID="a3Relationship_Lbl" Text='<%# Eval("Relationship") %>'></asp:Label></h3></td>
    <td valign="bottom">&nbsp;</td>
  </tr>
    </ItemTemplate>
    </asp:Repeater>
  </table>
<br />
  <asp:SqlDataSource ID="dsVendorConflictOfInterest3" runat="server" ConnectionString="<%$ ConnectionStrings:AVAConnectionString %>"
            SelectCommand="IF EXISTS (SELECT 1 FROM tblVendorConflictOfInterest WHERE VendorId = @VendorId AND GTEmployee IS NOT NULL AND Relationship IS NOT NULL AND Description ='Q3') BEGIN SELECT t1.* FROM tblVendorConflictOfInterest t1 WHERE t1.VendorId = @VendorId AND t1.Description='Q3' END ELSE BEGIN SELECT 0 AS ID,'' AS VendorId, '' AS Description, '' AS YesNo, '' AS Position, '' AS NatureOfBusinessId, '' AS GTEmployee, '' AS GTEmployeePosition, '' AS NatureOfBusinessName, '' AS CompetitorName, '' AS NoYears, '' AS DateCreated, '' AS Relationship END"  OnSelecting="rptGeneral_Selecting"  >
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




<%--<div class="separator1"></div>
<table width="100%" border="0" cellpadding="5" cellspacing="0">
<tr>
    <td style="height: 25px"><strong>Any Executive / Directors in your company with relatives in Trans-Asia?</strong>
    </td>
  </tr>
  <tr>
    <td><strong>
          <asp:RadioButtonList ID="YesNo4" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
            <asp:ListItem Value="1">Yes</asp:ListItem>
            <asp:ListItem Value="0" Selected="True">No</asp:ListItem>
        </asp:RadioButtonList>
        </strong>
        <h3><asp:Label runat="server" ID="YesNo4_Lbl"></asp:Label></h3>
    </td>
    </tr>
</table>
<table width="75%" border="0" cellpadding="5" cellspacing="0">
  <tr>
    <td><strong>Nature of business</strong></td>
    <td><strong>Vendor Executive / Director Name</strong></td>
    <td ><strong>Position</strong></td>
    <td ><strong>No. of years</strong></td>
    <td><input type="hidden" name="ConflictOfInterestCounter4" id="ConflictOfInterestCounter4" class="rowCount" /><a href="javascript:void(0)" value="Add Row" class="alternativeRow">+Add</a></td>
  </tr>
  <asp:Repeater ID="repeaterConflictOfInterest4" runat="server" DataSourceID="dsVendorConflictOfInterest4" >
  <ItemTemplate>
  <tr>
    <td><select id="4NatureOfBusinessId" name="4NatureOfBusinessId" style="width:200px;">
        <asp:Repeater ID="repeaterNatureOfBusiness4" runat="server" DataSourceID="dsrfcNatureOfBusiness" >
        <ItemTemplate>
        <option value="<%# DataBinder.Eval(Container.DataItem, "NatureOfBusinessId") %>" <%# (DataBinder.Eval(Container.DataItem, "NatureOfBusinessId")).ToString() == (DataBinder.Eval(((RepeaterItem)Container.Parent.Parent).DataItem, "NatureOfBusinessId")).ToString() ? "selected='selected'" : ""%> ><%#DataBinder.Eval(Container.DataItem, "NatureOfBusinessName")%></option>
        </ItemTemplate>
        </asp:Repeater>
        </select></td>
    <td><input name="4CompetitorName" type="text" id="Text1" size="28" value="<%# Eval("CompetitorName")%>" /></td>
    <td><input name="4Position" type="text" id="4Position" value="<%# Eval("Position")%>" /></td>
    <td><input name="4NoYears" type="text" id="4NoYears" size="8" class="integer" onfocus="reloadNumeric()" value="<%# Eval("NoYears")%>" /></td>
    <td valign="bottom"><img alt="" src="images/trash.png" width="9" height="13" border="0" class="delRow" /></td>
  </tr>
    </ItemTemplate>
    </asp:Repeater>

  <asp:Repeater ID="repeaterConflictOfInterest4_Lbl" runat="server" DataSourceID="dsVendorConflictOfInterest4" >
  <ItemTemplate>
  <tr>
    <td><h3><asp:Label runat="server" ID="a4NatureOfBusinessId_Lbl" Text='<%# Eval("NatureOfBusinessName") %>' ></asp:Label></h3></td>
    <td><h3><asp:Label runat="server" ID="a4Position_Lbl" Text='<%# Eval("Position") %>' ></asp:Label></h3></td>
    <td><h3><asp:Label runat="server" ID="a4CompetitorName_Lbl" Text='<%# Eval("CompetitorName") %>' ></asp:Label></h3></td>
    <td><h3><asp:Label runat="server" ID="a4NoYears_Lbl" Text='<%# Eval("NoYears") %>'></asp:Label></h3></td>
    <td valign="bottom">&nbsp;</td>
  </tr>
    </ItemTemplate>
    </asp:Repeater>
  </table>
<br />
  <asp:SqlDataSource ID="dsVendorConflictOfInterest4" runat="server" ConnectionString="<%$ ConnectionStrings:AVAConnectionString %>"
            SelectCommand="IF EXISTS (SELECT 1 FROM tblVendorConflictOfInterest WHERE VendorId = @VendorId AND Description='Q4') BEGIN SELECT t1.*, t2.NatureOfBusinessName FROM tblVendorConflictOfInterest t1, rfcNatureOfBusiness t2 WHERE t1.VendorId = @VendorId AND t1.Description='Q4' AND t2.NatureOfBusinessId=t1.NatureOfBusinessId END ELSE BEGIN SELECT 0 AS ID,'' AS VendorId, '' AS Description, '' AS Position, '' AS YesNo, '' AS NatureOfBusinessId, '' AS NatureOfBusinessName, '' AS CompetitorName, '' AS NoYears, '' AS DateCreated END"  OnSelecting="rptGeneral_Selecting"  >
    <SelectParameters>
        <asp:Parameter Name="VendorId" Type="Int32" />
	</SelectParameters>
  </asp:SqlDataSource>
<script type="text/javascript">
    $("document").ready(function () {
        $(".alternativeRow").btnAddRow({ inputBoxAutoNumber: true, inputBoxAutoId: true, displayRowCountTo: "rowCount" });
        $(".delRow").btnDelRow();
    });
</script>--%>


<div class="separator1"></div>
<br />
<br />
<br />
<asp:LinkButton ID="createBt" runat="server" CssClass="bt1" onclientclick="javascript: __doPostBack('continueStp', ''); return false;"><span>SAVE &amp; CONTINUE STEP 7 &raquo;</span></asp:LinkButton>&nbsp;<asp:LinkButton ID="createBt1" runat="server" CssClass="bt1"  onclientclick="javascript: __doPostBack('justSave', ''); return false;"><span>SAVE</span></asp:LinkButton>&nbsp;
<a href="vendor_05_financialInfo.aspx<%= queryString %>" class="bt1"><span>BACK</span></a>
<br />
<br />
<br />
</form>




<!--BODY CONTENT ENDS-->
<!--##################-->
</div>
</div>
<!-- content ends --> 

</asp:Content>