<%@ Page Title="" Language="C#" MasterPageFile="~/MasterSubPage.master" AutoEventWireup="true" CodeFile="vendor_requirements.aspx.cs" Inherits="vendor_requirements" %>
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
        ul {
            list-style: none;
        }

        .box4 {
            width: 75px;
        }
        .box2 {
            width: 500px;
        }
        .box3 {
            width: 300px;
        }
      
       
        .box1 {
            width: 75px;
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
<div style="margin:10px 0px; color:#333; font-size:18px; width:480px; float:left;">Material Management Department <div style ="font-size: 16px"> MMD Guidelines for Suppliers</div></div>


<form id="formVendorInfo" runat="server">

    <input id="VendorBranchesCounter" class="rowCount" name="VendorBranchesCounter" type="hidden" /><input id="SubsidiaryCounter" class="rowCount" name="SubsidiaryCounter" type="hidden" />

<!--Business activities STARTS-->
<div class="separator1"></div>
<h3 style="margin:10px 0px;">1. <b><span>ACCCREDITATION OF SUPPLIERS</span></b></h3>
1.1     Accredited suppliers will be given priority in issuances of request for quotations. </br> </br>
1.2	    Companies who wish to be included in the list of accredited suppliers will undergo the following procedures:</br> </br>
    <ul>
        <li>1.2.1	Accomplish and submit all documentation requirements  </li>
            <ul>
                <li>1.1.1.1	Vendor Profile</li>
                <li>1.1.1.2	Supplier Accreditation Form</li>
                <li>1.1.1.3	Company Profile</li>
                <li>1.1.1.4	Latest Audited Financial Statements and other documents</li>
            </ul>

        <li>1.2.2	Undergo actual visit/inspection of facilities by our MMD Staff</li>
        <li>1.2.3	Undergo a probationary period of (6) months.</li>
    </ul>
    </BR>
1.3	    A yearly evaluation of suppliers will be conducted to determine supplier’s accreditation form status.
</BR></BR>

<div class="separator1"></div>
<h3 style="margin:10px 0px;">2. VISITING DAYS</h3>
2.1	    Regular supplier’s visiting days will be set. Transactions should be limited on these days except for urgent business matters. Suppliers should regularly check for POs assigned on their behalf. </BR></BR>
    
    <ul>
        <li>Head Office Makati : Mondays, Wednesdays, Fridays (MWF) 2:00 PM to 4:30 PM</li>
    </ul>
     <br />
    <br />
 
    <div class="separator1"></div>
<h3 style="margin:10px 0px;">3. <b><span>QUOTATIONS</span></b></h3>
3.1	All quotations must specify brand, country of origin, and delivery period. Failure to include these information may result to non-inclusion of bid. </BR></BR>
3.3	A separate quotation must be submitted for every Request for Quotation (RFQ) received. One RFQ – One Quotation.</BR></BR>

3.4	RFQ number should be indicated in the formal quotation. If there is no quote for some of the items in the RFQ, “NO QUOTE” should be indicated. If there is no quote for the entire requirement, the reason should be stated in the RFQ form and returned to MMD signed by the supplier’s authorized signatory with company’s name written in the space provided. </BR></BR>

3.5	The quotation should be submitted on or before the due date. Inability to quote within the specified period should be relayed to concerned buyer before due date. </BR></BR>


    <div class="separator1"></div>
<h3 style="margin:10px 0px;">4. <b><span>DELIVERIES</span></b></h3>
4.1	The PO clearly indicates delivery time of goods. For ex-stock items, the items should be delivered within 3-5 days. Late deliveries will not be tolerated (ex-stock or otherwise) unless reported in writing and only for justifiable reasons.</BR></BR>

4.2	PO number, PR number and buyer’s name should always be indicated in the invoices and delivery receipts.</BR></BR>

4.3	Goods should not be delivered without an approved PO and the accompanying invoices and delivery receipts. The company will not be responsible for the payment of goods delivered without the benefit of a signed PO. An exception is made for emergency requirements which may be delivered with proper authorization. </BR></BR>

     <div class="separator1"></div>
<h3 style="margin:10px 0px;">5. <b><span>DISCIPLINARY MEASURES FOR ERRING SUPPLIERS</span></b></h3> 
5.1	Practices and activities which are detrimental to the best interest of the company will not be tolerated. Erring suppliers shall be subjected to the sanctions listed below, depending on the gravity of the misdeed. Blacklisting can be resorted to if the act was intentional and the company’s interest is severely damaged.</BR></BR>
   
    <table>
    <tr>
       <td class="box1">5.1.1</td>
        <td class="box4"></td> 
       <td class="box2" style="font-weight:bold; text-decoration:underline;"> Price</td>
       <td class="box3" style="font-weight:bold; text-decoration:underline;">Sanction</td>
   </tr>
    <tr>
       <td class="box1"></td> 
        <td class="box4">5.5.1.1</td> 
       <td class="box2"> Changing of quotations due to misquote	</td>
       <td class="box3"></td>
   </tr>
    <tr>
       <td class="box1"></td> 
        <td class="box4"></td>
       <td class="box2"> a.	1st Offense	</td>
       <td class="box3">Written warning</td>
   </tr>
<tr>
       <td class="box1"></td> 
    <td class="box4"></td>
       <td class="box2"> b.	2nd Offense	</td>
       <td class="box3">3 months suspension</td>
   </tr>
    <tr>
       <td class="box1"></td> 
        <td class="box4"></td>
       <td class="box2"> c.	3rd Offense	</td>
       <td class="box3">6 months suspension</td>
   </tr>
      <tr>
       <td class="box1"></td> 
          <td class="box4"></td>
       <td class="box2"> d.	4th Offense</td>
       <td class="box3">Blacklisting</td>
   </tr>
     <tr>
       <td class="box1"></td> 
         <td class="box4"></td>
       <td class="box2"> Overpricing if found to be excessive and 
Intentional
	</td>
       <td class="box3"></td>
   </tr>
     <tr>
       <td class="box1">5.1.2</td>
         <td class="box4"></td> 
       <td class="box2"style="font-weight:bold; text-decoration:underline;"> QUALITY</td>
       <td class="box3"></td>
   </tr>
    <tr>
          <td class="box1"></td> 
          <td class="box4">5.1.2.1</td>
       <td class="box2">Intentionally supplying 2nd hand  / surplus 
items being passed off as brand new
</td>
       <td class="box3">Blacklisting</td>
   </tr>
 <tr>
          <td class="box1"></td> 
          <td class="box4">5.1.2.2</td>
       <td class="box2">Supplying replacement, fake/bogus 
parts and passing them off as genuine
parts/units

</td>
       <td class="box3">Blacklisting</td>
   </tr>
 <tr>
          <td class="box1"></td> 
          <td class="box4">5.1.2.3</td>
       <td class="box2">Intentionally supplying items of different 
brand lower in quality that what is specified</td>
       <td class="box3"></td>
   </tr>
<tr>
          <td class="box1"></td> 
          <td class="box4"></td>
       <td class="box2">in the Purchase Order</td>
       <td class="box3">Blacklisting</td>
   </tr>
<tr>
          <td class="box1"></td> 
          <td class="box4">5.1.2.4</td>
       <td class="box2">Supplying materials and services with poor quality/workmanship and/or concealment of defective portions</td>
       <td class="box3"></td>
   </tr>

<tr>
       <td class="box1"></td> 
    <td class="box4"></td>
       <td class="box2"> a.	1st Offense	</td>
       <td class="box3">3 months suspension</td>
   </tr>
    <tr>
       <td class="box1"></td> 
        <td class="box4"></td>
       <td class="box2"> b.	2nd Offense		</td>
       <td class="box3">6 months suspension</td>
   </tr>
      <tr>
       <td class="box1"></td> 
          <td class="box4"></td>
       <td class="box2"> c.	3rd Offense	</td>
       <td class="box3">Blacklisting</td>
   </tr>
     <tr>
       <td class="box1">5.1.3</td> 
          <td class="box4"></td>
       <td class="box2" style="font-weight:bold; text-decoration:underline;"> PROMPTNESS	</td>
       <td class="box3"></td>
   </tr>
   <tr>
       <td class="box1"></td> 
          <td class="box4"><span>5.1.3.1</span></td>
       <td class="box2"> Unjustified late deliveries of purchase
order / item rewarded
	</td>
       <td class="box3"></td>
   </tr>
<tr>
       <td class="box1"></td> 
        <td class="box4"></td>
       <td class="box2"> a.	1st Offense	</td>
       <td class="box3">Written warning</td>
   </tr>
<tr>
       <td class="box1"></td> 
    <td class="box4"></td>
       <td class="box2"> b.	2nd Offense	</td>
       <td class="box3">3 months suspension</td>
   </tr>
    <tr>
       <td class="box1"></td> 
        <td class="box4"></td>
       <td class="box2"> c.	3rd Offense	</td>
       <td class="box3">6 months suspension</td>
   </tr>
      <tr>
       <td class="box1"></td> 
          <td class="box4"></td>
       <td class="box2"> d.	4th Offense</td>
       <td class="box3">12 months suspension</td>
   </tr>
     
      <tr>
       <td class="box1"></td> 
          <td class="box4"></td>
       <td class="box2"> e.	5th Offense	</td>
       <td class="box3" style="height:auto;">Blacklisting plus the 
max. 10%  of the total Purchase Order amount
</td>
   </tr>
    <tr>
       <td class="box1"></td> 
          <td class="box4">5.1.3.2</td>
       <td class="box2" style="height:auto;">Inability to serve purchase orders awarded due to
unjustified reason resulting to cancellation and re-
awarding to other suppliers. For quotation stating
ex-stock subject to prior sale, MMD should be advised
in writing of non-availability of stock within twenty-
four (24) hours from date of receipt of approved order.
</td>
       <td class="box3"></td>
   </tr>
<tr>
       <td class="box1"></td> 
    <td class="box4"></td>
       <td class="box2"> a.	1st Offense	</td>
       <td class="box3">Written Warning</td>
   </tr>
    <tr>
       <td class="box1"></td> 
        <td class="box4"></td>
       <td class="box2"> b.	2nd Offense		</td>
       <td class="box3">3 months suspension</td>
   </tr>
      <tr>
       <td class="box1"></td> 
          <td class="box4"></td>
       <td class="box2"> c.	3rd Offense	</td>
       <td class="box3">Blacklisting</td>
   </tr>
    <tr>
       <td class="box1"></td> 
          <td class="box4">5.1.3.2</td>
       <td class="box2" style="height:auto;">For indent purchases, Proforma Invoices should
be submitted not later than 5 working days upon
the receipt of approved order.

</td>
       <td class="box3"></td>
   </tr>
<tr>
       <td class="box1"></td> 
        <td class="box4"></td>
       <td class="box2"> a.	1st Offense	</td>
       <td class="box3">Written warning</td>
   </tr>
<tr>
       <td class="box1"></td> 
    <td class="box4"></td>
       <td class="box2"> b.	2nd Offense	</td>
       <td class="box3">3 months suspension</td>
   </tr>
    <tr>
       <td class="box1"></td> 
        <td class="box4"></td>
       <td class="box2"> c.	3rd Offense	</td>
       <td class="box3">6 months suspension</td>
   </tr>
      <tr>
       <td class="box1"></td> 
          <td class="box4"></td>
       <td class="box2"> d.	4th Offense</td>
       <td class="box3">12 months suspension</td>
   </tr>
   <tr>
       <td class="box1">5.1.4</td> 
          <td class="box4" ></td>
       <td class="box2" style="font-weight:bold; text-decoration:underline;">OTHER MAJOR OFFENSES SUBJECT TO BLACKLISTING</td>
       <td class="box3"></td>
   </tr>
  <tr>
       <td class="box1"></td> 
          <td class="box4">5.1.4.1</td>
       <td class="box2"> Bribery	</td>
       <td class="box3">Blacklisting</td>
   </tr>
  <tr>
       <td class="box1"></td> 
          <td class="box4">5.1.4.1</td>
       <td class="box2"> Supplying false information or misrepresentation</td>
       <td class="box3">Blacklisting</td>
   </tr>
  <tr>
       <td class="box1"></td> 
          <td class="box4">5.1.4.3</td>
       <td class="box2"> Unjustified refusal to replace products failing
during warranty
</td>
       <td class="box3">Blacklisting</td>
   </tr>
    </table>





   
     <div class="separator1"></div>
<h3 style="margin:10px 0px;">6. <b><span>OTHER MATTERS</span></b></h3>  
6.1	Suppliers are not allowed to conduct plant visits without prior clearance with MMD office. Request for plant visits should be formalized in writing stating reason, contact person and date of plant visit.</BR></BR>

6.2	A formal letter is warranted under the following circumstances.</BR></BR>
    <ul> 
	<li>6.2.1	Request for extension of deadlines for quotations.</li>
	<li>6.2.2	Change in price quotation, specifications, etc.</li>
	<li>6.2.3	Cancellation of Purchase Order</li>
	<li>6.2.4	Revision in Purchase Order</li></BR>
	<li>6.2.5	Request for extension of delivery date.</li>
	<li>Reasons for changes must be justified.</li>
        
    </ul>
    </BR></BR>

6.3	All recommendations for supplier delisting shall be formally endorsed and approved by VP-MMD and circularized all plants.</BR></BR>

6.4	Materials Management Department shall quarterly circularize if any, the names of delisted suppliers to all Trans-Asia Oil and Energy Group / Phinma companies under the Phinma Group.</BR></BR>

    
    

   
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