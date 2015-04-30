<%@ Page Title="" Language="C#" MasterPageFile="~/MasterSubPage.master" AutoEventWireup="true" CodeFile="vmhead_vendorDetails_View.aspx.cs" Inherits="vmhead_vendorDetails_View" %>
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
<%@ Register TagPrefix="Ava" TagName="TopNav" Src="usercontrols/TopNav_vmreco.ascx" %>
<ava:topnav ID="TopNav1" runat="server" />

<div style="margin:10px 0px; color:#333; font-size:18px; width:450px; float:left;"><%--Vendor Name: 
    <br />
  Authentication Ticket: <asp:Label ID="AuthenticationTicketLbl" runat="server" Text=""></asp:Label>--%>
    Assessment Report</div>
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
    <td>Assessment No.</td>
    <td><h3><asp:Label ID="dnbDuns" runat="server" Text=""></asp:Label></h3></td>
    </tr>
  <tr>
    <td>Legal structure / Company type</td>
    <td><h3><asp:Label ID="legalStrucOrgType" runat="server" Text=""></asp:Label></h3></td>
    </tr>
  <tr>
    <td style="height: 39px">Nature of business</td>
    <td style="height: 39px"><h3><asp:Label ID="NatureOfBusiness" runat="server" Text=""></asp:Label></h3></td>
    </tr>
  <tr>
    <td>Products/Services offered</td>
    <td><h3><asp:Label ID="Category" runat="server" Text=""></asp:Label></h3></td>
    </tr>
</table>
<br />

<div class="separator1"></div>
<h3 style="margin:10px 0px;">SUPPLIER INFORMATION REPORT & OTHER DOCUMENTS</h3>

        <%--<table style="width: 100%;">
            <tr>
                <td style="width: 30px">
                    <asp:CheckBox ID="Vendor_new" runat="server" />
                </td>
                <td style="width: 159px">New Vendor</td>
                <td style="width: 30px">
                    <asp:CheckBox ID="Vendor_renewal" runat="server" />
                </td>
                <td>Renewal Vendor</td>
            </tr>
        </table>--%>

        <asp:RadioButtonList ID="vmoNew_Vendor" runat="server" RepeatDirection="Horizontal" Enabled="false">
            <asp:ListItem Value="1" Selected="True">New Vendor</asp:ListItem>
            <asp:ListItem Value="0">Renewal Vendor</asp:ListItem>
        </asp:RadioButtonList>

<br /><br />

<div class="separator1 clearfix"></div>
<h3 style="margin:10px 0px;">VENDOR EVALUATION REPORT</h3>
        <table style="width: 100%;" border="0" cellspacing="0" cellpadding="8">
            <tr>
                <td style="width: 108px; height: 37px;"><h4>
                    <label>&nbsp;Assessment Score</label></h4></td>
                <td style="width: 149px; height: 37px;"><h3><asp:Label runat="server" ID="dnbScore" Text="0"></asp:Label></h3></td>
                <td style="width: 115px; height: 37px;"><h4><label>Overall Score</label></h4></td>
                <td style="height: 37px"><h3><asp:Label runat="server" ID="vmoOverallScore" Text="0"></asp:Label></h3></td> 
            </tr>
        </table>
<br /><br />
<table width="100%" border="0" cellspacing="0" cellpadding="8">
  <tr>
    <td class="style1" style="width: 68px; height: 31px;"><label for="dnbCompRating">Financial Capability Score</label></td>
    <td style="height: 31px; width: 28px;"><h3><asp:Label ID="dnbFinCapScore" runat="server" Text="0"></asp:Label></h3></td>
    <td style="height: 31px"><label for="">Remarks </label><div class="clearfix"></div>
        <asp:Label ID="dnbFinCapScore_Remarks" runat="server" Text="No Comment."></asp:Label></td>
  </tr>
  <tr>
    <td class="style1" style="width: 68px; height: 31px;"><label for="">Legal Conformance Score</label></td>
    <td style="height: 31px; width: 28px;"><h3><asp:Label ID="dnbLegalConfScore" runat="server" Text="0"></asp:Label></h3></td>
    <td style="height: 31px"><label for="">Remarks </label><div class="clearfix"></div>
        <asp:Label ID="dnbLegalConfScore_Remarks" runat="server" Text="No Comment."></asp:Label></td>
  </tr>
  <tr style="display:none;">
    <td class="style1" style="width: 68px">&nbsp;</td>
    <td style="width: 28px">&nbsp;</td>
    <td>
        <table style="width:89%;">
            <tr>
                <td style="width: 306px; height: 26px;"><strong>Will the issue pose significant adverse risk to Trans-Asia?</strong></td>
                <td style="height: 26px"><asp:CheckBox ID="vmoIssue_risk_to_Globe" runat="server" Enabled="false" /></td>
            </tr>
            <tr>
                <td style="width: 306px; height: 39px;" valign="top"><strong>With Conflict of Interest?</strong></td>
                <td style="height: 39px" valign="top"><asp:CheckBox ID="vmoConflict_of_Interest" runat="server" Enabled="false" /></td>
            </tr>
            <tr>
                <td style="width: 306px" valign="top"><strong>Registered Court Case:</strong></td>
                <td valign="top"><strong><asp:Label ID="TypeOfCase" runat="server" Text="No Case"></asp:Label></strong>
                    <br />
                    <span><asp:Label runat="server" ID="DateFiled" ></asp:Label></span>
                    <div class="clearfix"></div>
                    <div style="float:left; width:30px;"><img alt="" src="images/attachment.png" /></div> 
        <asp:Label ID="fileuploaded_1" CssClass="fileuploaded1" runat="server" Text="No Attachment" style="float:left; padding-top:3px; display:block"></asp:Label>
                </td>
            </tr>
            </table>
    </td>
  </tr>
  <tr>
    <td class="style1" style="width: 68px"><label for="">Technical Qualification Score</label></td>
    <td style="width: 28px"><h3><asp:Label ID="dnbTechCompScore" runat="server" Text="0"></asp:Label></h3></td>
    <td><label for="">Remarks </label><div class="clearfix"></div>
        <asp:Label ID="dnbTechCompScore_Remarks" runat="server" Text="No Comment."></asp:Label></td>
  </tr>
  <tr style="display:none;">
    <td class="style1" style="width: 68px"><h3>&nbsp;</h3>
      </td>
    <td style="width: 28px"></td>
    <td>
        <table style="width:75%;"  valign="top" >
            <tr>
                <td style="width: 307px"><strong>With type-approved products</strong></td>
                <td><asp:CheckBox ID="vmoWith_Type_Approved_Products" runat="server" Enabled="false" /></td>
            </tr>
            <tr>
                <td style="width: 307px"><strong>With approved Proof of Concept</strong></td>
                <td><asp:CheckBox ID="vmoWith_Approved_Proof_of_Concept" runat="server"  Enabled="false" /></td>
            </tr>
            <tr>
                <td colspan="2" style="width: 244px"> </td>
            </tr>
            </table>
    </td>
  </tr>
  <tr>
    <td class="style1" style="width: 68px" valign="top"><label for="">SIR Attachment</label></td>
    <td class="style1" style="width: 28px" valign="top">
        &nbsp;</td>
    <td><asp:Label runat="server" ID="dnbSupplierInfoReport"></asp:Label></td>

  </tr>
  <tr>
    <td class="style1" style="width: 68px" valign="top"><label for="">Other Documents</label></td>
    <td class="style1" style="width: 28px" valign="top">
        &nbsp;</td>
    <td><asp:Label runat="server" ID="dnbOtherDocumentsLbl"></asp:Label></td>

  </tr>
  </table>

        <table style="width: 70%;" cellpadding="5">
            <tr>
                <td style="width: 250px">If Renewal, Performance Evaluation</td>
                <td style="width: 30px">
                    <h3><asp:Label ID="vmoGTPerf_Eval" runat="server"  Text="0"></asp:Label></h3>
                </td>
                <td style="display:none;" align="right" style="text-align:right">MAXIMUM EXPOSURE LIMIT&nbsp;&nbsp;&nbsp;</td>
                <td style="display:none;"width: 30px">
                    <h3><asp:Label ID="dnbMaxExposureLimit" runat="server"  Text="0" ></asp:Label></h3>
                </td>
            </tr>
        </table>
        
<br />


<!--Business activities STARTS-->
<div class="separator1"></div>
<table style="width: 100%;">
          <tr>
              <td style="height: 19px; width: 438px" valign="top">
                <h3 style="margin:10px 0px;">SPEND PROFILE</h3>
                <br />
                  <table border="0" cellspacing="0" cellpadding="8">
                      <tr>
                        <td><label for="">No. of POs:</label>
                          </td>
                        <td style="width: 156px">&nbsp;<h3><asp:Label ID="vmoNo_POs" runat="server"></asp:Label></h3>
                          </td>
                        </tr>
      
                        <td><label for="">Spend:</label>
                          </td>
                        <td style="width: 156px">&nbsp;<h3><asp:Label ID="vmoSpend" runat="server"></asp:Label></h3>
                          </td>
                        </tr>
                      <tr>
                        <td><label for="">With existing Frame Agreement?</label>
                          </td>
                        <td style="width: 156px">&nbsp;<asp:DropDownList ID="vmoWith_Existing_Frame_Arg" runat="server" Enabled="false">
                            <asp:ListItem Value="1">Yes</asp:ListItem>
                            <asp:ListItem Value="0">No</asp:ListItem>
                            </asp:DropDownList>
                          </td>
                        </tr>
                    </table>
              </td>
              <td style="height: 19px">
                  <h3 style="margin:10px 0px;">ISSUES ENCOUNTERED</h3>
                <br />
                  <table border="0" cellspacing="0" cellpadding="8">
                      <tr>
                        <td>
                            <table style="width: 100%;">
                                <tr>
                                    <td><asp:CheckBox ID="vmoIssues_bond_claims" runat="server" Enabled="false" /></td>
                                    <td> <label>Bond / LD Claims</label></td>
                                </tr>
                            </table>
</td>
                          
                        <td style="width: 156px">
                            <table style="width: 100%;">
                                <tr>
                                    <td><asp:CheckBox ID="vmoIssues_ISR_involvement" runat="server" Enabled="false" /></td>
                                    <td> <label>ISR Involvement</label></td>
                                </tr>
                            </table>
                          </td>
                        </tr>
      
                        <td>
                            <table style="width: 100%;">
                                <tr>
                                    <td><asp:CheckBox ID="vmoIssues_Others" runat="server" Enabled="false" /></td>
                                    <td> <label>Others</label></td>
                                </tr>
                            </table>
                          </td>
                        <td style="width: 156px">
                        <table style="width: 100%;">
                                <tr>
                                    <td><asp:CheckBox ID="vmoIssues_Loss_Incidents" runat="server" Enabled="false" /></td>
                                    <td> <label>Bond / LD Claims</label></td>
                                </tr>
                            </table>

                        </td>
                        </tr>
                      <tr>
                        <td colspan="2" style="height: 55px"><label for="">Remarks:</label>
                            <asp:Label id="vmoIssues_Remarks"  runat="server"></asp:Label>
                          &nbsp;
                            
                            </td>
                        </tr>
                    </table>
              </td>
          </tr>
          </table>

<br />






<div class="separator1"></div>
        <table style="width: 100%;">
            <tr>
                <td valign="top" width="50%">
                    <h3 style="margin:10px 0px;">RECOMMENDATIONS</h3>
                <br /><asp:Label runat="server" ID="recoby" Font-Bold="true" ></asp:Label>
                <br /><asp:Label runat="server" ID="recodate" ></asp:Label><br /><br />
                    <strong><asp:Literal runat="server" ID="OverallEvalRemarks"></asp:Literal></strong>
                    <div class="clearfix"></div>
                <br />
                    <strong>This is a recommendation to <asp:Label ID="recommendation" runat="server" Text="______" ForeColor="Blue"></asp:Label> the accreditation.</strong>
                    <div class="clearfix"><br /><br /></div>
        <div style="float:left; width:30px;"><img alt="" src="images/attachment.png" /></div> 
        <asp:Label ID="FileAttachementLbl" CssClass="fileuploaded1" runat="server" Text="No Attachment" style="float:left; padding-top:3px; display:block"></asp:Label>
                    <div class="clearfix"></div> 
                </td>
                <td valign="top" width="50%">
                
                <h3 style="margin:10px 0px;">CONDITIONS</h3>
                <br />
                    <label>Accreditation Duration</label>
                    <div class="clearfix"></div>
                    <asp:DropDownList ID="AccreDuration" runat="server" Enabled="false">
                        <asp:ListItem Selected="True">2 years</asp:ListItem>
                        <asp:ListItem>1 year</asp:ListItem>
                        <asp:ListItem>6 months</asp:ListItem>
                    </asp:DropDownList>
                    <div class="clearfix"><br /><br /></div>
                    <h3 style="margin:10px 0px;">COMMENTS: </h3>
                <br />
        <div class="clearfix"></div>
        <asp:DataList runat="server"  DataSourceID="dsBACComments" ID="DataList7" >
                                                                                  <ItemTemplate>
                                                                                  <div style="margin-bottom:15px;">
                                                                                  <b><asp:label runat="server" ID="comments" Name="comments" Text='<%# Eval("FullName") %>' ></asp:label></b>
                                                                                  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                                  <asp:label runat="server" ID="Label2" Name="comments" Text='<%# Eval("DateCreated") %>' ></asp:label>
                                                                                  </b><br />
                                                                                  <asp:label runat="server" ID="Label3" Name="comments" Text='<%# Eval("Comment").ToString().Replace("\n","<br>") %>' ></asp:label>
                                                                                  </div>
                                                                                  </ItemTemplate>
                                                                             </asp:DataList>
        <asp:SqlDataSource ID="dsBACComments" runat="server"   ConnectionString='<%$ ConnectionStrings:AVAConnectionString %>'  SelectCommand="select t1.*, t2.FirstName +' '+t2.LastName as FullName from tblComments t1, tblUsers t2 WHERE t1.VendorId=@VendorId AND t2.UserId=t1.UserId ORDER BY t1.DateCreated DESC" >
																				<SelectParameters>
																					<asp:SessionParameter Name="VendorId" SessionField="VendorId" Type="Int32" />
																				</SelectParameters>
																			</asp:SqlDataSource>
        <div class="clearfix"></div>
                    <div class="clearfix"></div>
                    <%--<textarea id="Others" runat="server" cols="35" rows="4" readonly="readonly"></textarea>--%>
                    <strong><asp:Literal runat="server" ID="Others"></asp:Literal></strong>
                </td>
            </tr>
        </table>
<br />
<br />
<br />
<div class="separator1"></div>
<br />
<%--<asp:LinkButton ID="createBt" runat="server" CssClass="bt1" onclientclick="javascript: __doPostBack('Approve', ''); return false;"><span>APPROVE &raquo;</span></asp:LinkButton>&nbsp;
<asp:LinkButton ID="LinkButton1" runat="server" CssClass="bt1" onclientclick="javascript: __doPostBack('Disapprove', ''); return false;"><span>DISAPPROVE &raquo;</span></asp:LinkButton>&nbsp;--%>
<a href="vmhead_VendorList.aspx" class="bt1"><span>BACK</span></a>
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