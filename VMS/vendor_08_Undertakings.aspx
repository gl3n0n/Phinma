<%@ Page Title="" Language="C#" MasterPageFile="~/MasterSubPage.master" AutoEventWireup="true" CodeFile="vendor_08_Undertakings.aspx.cs" Inherits="vendor_08_Undertakings" %>
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
<%@ Register TagPrefix="Ava" TagName="TopNav" Src="usercontrols/TopNav_vendor.ascx" %>
<%@ Register TagPrefix="Ava" TagName="StepNav" Src="usercontrols/StepNav_vendor.ascx" %>
<ava:topnav ID="TopNav1" runat="server" /><ava:stepnav ID="StepNav1" runat="server" />
<div style="margin:10px 0px; color:#333; font-size:18px; width:450px; float:left;">Undertaking</div>

<form id="formVendorInfo" runat="server">


<div class="separator1"></div>
<h3 style="margin:10px 0px;">Undertaking</h3>
<table width="100%" border="0" cellpadding="5" cellspacing="0">
  <tr>
    <td>
        <p>In consideration of the accreditation as supplier of Trans-Asia , I  and our company, employees, stockholders, directors, agents, officers and representatives hereby unconditionally undertake the following:<br />
        <br />
        </p>
        <ol>
            <li>To hold with utmost confidence all information relative to the operation or otherwise of Trans-Asia disclosed to the vendor during the CUSTOMER VENDOR relationship of Trans-Asia and the Vendor, including but not limited to technical information, specifications, drawings, documentation and “know-how” of every kind and description whatsoever disclosed by Trans-Asia (the “information”), except insofar as it may be in the public domain or to be established to have been independently developed and so documented by the Vendor or obtained from any person not in breach of any confidentiality obligations to Trans-Asia, is the exclusive property of Trans-Asia, and the other party except as specifically authorized in writing by Trans-Asia, or as may be permitted shall treat and protect the information as confidential, shall not reproduce the information except to the extent reasonably required for the performance of the Vendor’s obligations to Trans-Asia, shall not divulge the information in whole or in part to any third parties, and shall use the information only for purposes necessary for the performance of  Vendor’s Information. The Vendor shall disclose the information only to those of its employees and agents who shall have a “need-to-know” the information for purposes of complying with Vendor’s obligations.<br /><br /></li>
            <li>To make available all pertinent documents requested by Trans-Asia.<br /><br /></li>
            <li>To obtain prior written consent of Trans-Asia if the Vendor intends to publish, advertise, promote, and issue press release or other publicity matters with regard to its accreditation with Trans-Asia.<br /><br /></li>
            <li>To submit annually copies of the Vendor’s audited financial statements and an updated Vendor Information Sheet as soon as they are available and to immediately advise Trans-Asia of any changes in the corporate organization, beneficial ownership of the Vendor and products/services offered. Changes must be stated in writing and provided on official company stationary.<br /><br /></li>
                <p >
                    <![if !supportLists]><span lang="EN-US">1.<span>&nbsp;&nbsp;&nbsp; </span></span><![endif]><span lang="EN-US">Upon receipt and initial use of Trans-Asia’S e-Sourcing Portal USER ID and PASSWORD <span>(“USER ID and PASSWORD”)</span> which will be sent to me through my official e-mail address only upon approval of my company’s accreditation, <o:p></o:p></span>
                </p>
                <p style="mso-margin-top-alt: auto; mso-margin-bottom-alt: auto; margin-left: 85px; mso-add-space: auto; text-indent: -.25in;  mso-list: l0 level2 lfo1; margin-right: 0in; margin-top: 0in; margin-bottom: .0001pt;">
                    <![if !supportLists]><span lang="EN-US">a.<span>&nbsp;&nbsp;&nbsp; </span></span><![endif]><span lang="EN-US">To take full responsibility of securing and safeguarding the secrecy and strict confidentiality of my company’s USER ID and PASSWORD, and the proper and authorized use of Trans-Asia’s e-Sourcing Portal <o:p></o:p></span>
                </p>
                <p style="mso-margin-top-alt: auto; mso-margin-bottom-alt: auto; margin-left: 150px; mso-add-space: auto; text-indent: -1.5in; mso-text-indent-alt: -9.0pt;  mso-list: l0 level3 lfo1; margin-right: 0in; margin-top: 0in; margin-bottom: .0001pt;">
                    <![if !supportLists]><span lang="EN-US"><span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span>i.<span>&nbsp;&nbsp;&nbsp; </span></span><![endif]><span lang="EN-US">My company shall use the e-Sourcing Portal only for official purposes pertinent to Trans-Asia<span>’s</span> official invitations to bid or related activity.<o:p></o:p></span></p>
                <p style="mso-margin-top-alt: auto; mso-margin-bottom-alt: auto; margin-left: 150px; mso-add-space: auto; text-indent: -1.5in; mso-text-indent-alt: -9.0pt; ; mso-list: l0 level3 lfo1;  margin-right: 0in; margin-top: 0in; margin-bottom: .0001pt;">
                    <![if !supportLists]><span lang="EN-US"><span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span>ii.<span>&nbsp;&nbsp;&nbsp; </span></span><![endif]><span lang="EN-US">I shall not allow anyone to perform any unauthorized activity with the USER ID;<o:p></o:p></span></p>
                <p style="mso-margin-top-alt: auto; mso-margin-bottom-alt: auto; margin-left: 150px; mso-add-space: auto; text-indent: -1.5in; mso-text-indent-alt: -9.0pt;  mso-list: l0 level3 lfo1; margin-right: 0in; margin-top: 0in; margin-bottom: .0001pt;">
                    <![if !supportLists]><span lang="EN-US"><span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span>iii.<span>&nbsp;&nbsp;&nbsp; </span></span><![endif]><span lang="EN-US">I shall not disclose the USER ID and PASSWORD to any person under any circumstances except to my authorized representative, whose name I will formally submit to Trans-Asia.<o:p></o:p></span></p>
                <p style="mso-margin-top-alt: auto; mso-margin-bottom-alt: auto; margin-left: 85px; mso-add-space: auto; text-indent: -.25in;  mso-list: l0 level2 lfo1; margin-right: 0in; margin-top: 0in; margin-bottom: .0001pt;">
                    <![if !supportLists]><span lang="EN-US">b.<span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span></span><![endif]><span lang="EN-US">To formally <span>and immediately</span> inform Trans-Asia, through a written request addressed to the Head-Vendor Management, of any change in the authorized representative<s><span>s</span></s> who will have access to the e-Sourcing Portal.&nbsp;<o:p></o:p></span></p>
                <p style="text-indent: -.25in; mso-list: l0 level1 lfo1;  margin-left: .5in; margin-right: 0in; margin-top: 0in; margin-bottom: 8.0pt;">
                    <![if !supportLists]><span>2.&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><![endif]><span lang="EN-US">To strictly abide by the rules and procedures promulgated by Trans-Asia particularly on but not limited to bidding procedures.</span><o:p></o:p></p>
                

      <p>Likewise, I warrant that the information I provided in the Vendor Information Sheet and copies of the required submitted accreditation documents are true and correct. I fully understand and agree that for any omission or any misrepresentation with regards to the information provided therein, if later found to be untrue or incorrect, or for any violation of the undertakings, our Company shall be subject to an immediate deletion from the list of Accredited Vendors of Trans-Asia, its subsidiaries and affiliates. without need of further notice. Furthermore, in such event, any pending project or order shall ipso facto be terminated or withdrawn at the option of Trans-Asia.</p><br />
      <p>The undersigned hereby understands that any violation of the foregoing shall not mean only deletion from the list of Trans-Asia Accredited Vendors but may, likewise, hold the Vendor liable for any damages, whether directly or indirectly incurred as a result of the Vendor’s non-compliance with this undertaking.</p>
    </td>
  </tr>
  </table>
<br />

<div runat="server" id="tbl01">
<table border="0" cellpadding="5" cellspacing="0">
  <tr>
    <td><strong>I,</strong></td>
    <td style="width: 186px"><input name="step8FullName" type="text" id="step8FullName" runat="server" value="Full name" size="28"  /></td>
    <td><strong>, in my capability as </strong></td>
    <td><input name="step8OfficialTitle" type="text" id="step8OfficialTitle" runat="server" value="Official Title" size="28"  /></td>
  </tr>
</table>
<table border="0" cellpadding="5" cellspacing="0">
  <tr>
    <td><strong>of</strong></td>
    <td><input name="step8OfCompanyName" type="text" id="step8OfCompanyName" runat="server" value="Company name" size="28"  readonly="readonly" /></td>
    <td><strong>acknowledge that I am duly authorized and I have full authority to sign this undertakings </strong></td>
    </tr>
</table>
<table border="0" cellpadding="5" cellspacing="0">
  <tr>
    <td><strong>and bind</strong></td>
    <td><input name="step8bindCompanyName" type="text" id="step8bindCompanyName" runat="server" value="Company name" size="28" readonly="readonly"  /></td>
    <td><strong>.</strong></td>
  </tr>
</table>
</div>
    
<div runat="server" id="tbl01_Lbl">
<table border="0" cellpadding="5" cellspacing="0">
  <tr>
    <td><strong>I,</strong></td>
    <td style="width: 186px"><h3><asp:Label runat="server" ID="step8FullName_Lbl"></asp:Label></h3></td>
    <td><strong>, in my capability as </strong></td>
    <td><h3><asp:Label runat="server" ID="step8OfficialTitle_Lbl"></asp:Label></h3></td>
  </tr>
</table>
<table border="0" cellpadding="5" cellspacing="0">
  <tr>
    <td><strong>of</strong></td>
    <td><h3><asp:Label runat="server" ID="step8OfCompanyName_Lbl"></asp:Label></h3></td>
    <td><strong>acknowledge that I am duly authorized and I have full authority to sign this undertakings </strong></td>
    </tr>
</table>
<table border="0" cellpadding="5" cellspacing="0">
  <tr>
    <td><strong>and bind</strong></td>
    <td><h3><asp:Label runat="server" ID="step8bindCompanyName_Lbl"></asp:Label></h3></td>
    <td><strong>.</strong></td>
  </tr>
</table>
</div>
<br />
<br />
<br />


<div class="separator1"></div>
<br />
<br />
<br />
<%--<asp:LinkButton ID="createBt" runat="server" CssClass="bt1"  onclientclick="javascript: __doPostBack('agreeBt', ''); return false;"><span>AGREE »</span></asp:LinkButton>
<asp:LinkButton ID="createBt2" runat="server" CssClass="bt1"  onclientclick="javascript: __doPostBack('agreeBt2', ''); return false;"><span>&laquo; DISAGREE</span></asp:LinkButton>
&nbsp;<a href="vendor_07_Conflict.aspx" class="bt1"><span>BACK</span></a>--%>
<asp:LinkButton ID="createBt" runat="server" CssClass="bt1" onclientclick="javascript: if(confirm('Are you sure to submit all information and agree to all terms and conditions?'))__doPostBack('continueStp', ''); return false;"><span>AGREE & CONTINUE »</span></asp:LinkButton>
&nbsp;<a href="vendor_Home.aspx" class="bt1" ID="createBt1" runat="server" ><span>&laquo; DISAGREE</span></a>
&nbsp;<a href="vendor_07_Conflict.aspx<%= queryString %>" class="bt1"><span>BACK</span></a>
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