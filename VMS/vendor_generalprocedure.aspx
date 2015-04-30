<%@ Page Title="" Language="C#" MasterPageFile="~/MasterSubPage.master" AutoEventWireup="true" CodeFile="vendor_generalprocedure.aspx.cs" Inherits="vendor_generalprocedure" %>
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
    <style type="text/css">
        ol li {
            margin-top:10px;
            font-weight:bold;
            border-top: 1px dotted #ccc;
            padding-top:10px;
        }
        ul li {
            font-weight:normal;
            border-top:none;
            padding-top:3px;
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
<ava:topnav ID="TopNav1" runat="server" />
<div style="margin:10px 0px; color:#333; font-size:18px; width:480px; float:left;">GENERAL REGISTRATION PROCEDURE & GUIDELINES</div>


<form id="formVendorInfo" runat="server">

    <input id="VendorBranchesCounter" class="rowCount" name="VendorBranchesCounter" type="hidden" /><input id="SubsidiaryCounter" class="rowCount" name="SubsidiaryCounter" type="hidden" />

<!--Business activities STARTS-->
<div class="separator1"></div>
<h3 style="margin:10px 0px;">1.	Issuance of a notice to proceed accreditation including requirements</h3>
<br />



<div class="separator1"></div>
<h3 style="margin:10px 0px;">2.	Supplier will deliver accomplished Vendor Information Sheet and Required Documents (originals and photocopies, see attachment), to Trans-Asia VMS</h3>
    
    <ul>
        <li><b>Makati Office:</b><br />
            Unit 507 5th Floor Rufino Pacific Tower <br />
            6784 Ayala Avenue cor VA Rufino <br />
            Makati City 1226<br />
            dunsnumber@phinma.com.ph<br />
            <br />
            <b>Contact Person:</b><br />
            Cynthia Galope<br />
            GalopeC@phinma.com.ph<br />
           (02) 750-0082 • (02) 774-8342<br />
        </li>
        <li><b>Cebu Office:</b><br />
            5th Floor Krizzia Building Gorordo Avenue<br />
            Lahug, Cebu City<br />
            cebu@phinma.com.ph<br />
            <br />
            <b>Contact Person:</b><br />
            Sheila Negare<br />
            NegareS@phinma.com.ph<br />
           (032) 233-6053<br />
        </li>
    </ul>
    <br />
    *** For Provincial Suppliers: Option to send Supplier Registration Form and Certified True Copies of pertinent requirements via courier.
    <br />



    

<div class="separator1"></div>
<h3 style="margin:10px 0px;">3.	Payment</h3>
    The processing fee shall be paid at the Trans-Asia VMS upon submission of documents.  
    <br /><br />
The processing fee may also be paid thru bank. Please attach a copy of the deposit slip upon submission of documents
     <br /><br />
    <ul>
        <li><b>Bank Details:</b><br />
            Banco de Oro Universal Bank<br />
            Account Name: Trans-Asia VMS<br />
            Account Number: 4280-001-076<br />
            <em>* Cheques must be made payable to Trans-Asia VMS</em><br />
        </li>
    </ul>
    <br />
    An official receipt will be issued upon confirmation of payment
    <br />



    

<div class="separator1"></div>
<h3 style="margin:10px 0px;">4.	Issuance of Letter of Accreditation (issued by Trans-Asia)</h3>
    Trans-Asia shall advise the supplier either thru email when their evaluation process has been completed.
     
    <br />
    <br />
    *** Note: Only Suppliers with fully accomplished Supplier Registration Forms submitted with complete and verified documents and have paid will be processed.
    <br />


<div class="separator1"></div>
<br />
<br />
<br />
    &nbsp;&nbsp;
<a href="vendor_Home.aspx<%= queryString %>" class="bt1"><span>BACK</span></a>
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