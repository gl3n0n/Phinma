<%@ Page Title="" Language="C#" MasterPageFile="~/MasterSubPage.master" AutoEventWireup="true" CodeFile="vendor_Home.aspx.cs" Inherits="vendor_Home" %>
<%@ Register TagPrefix="Ava" TagName="Tabsnav" Src="usercontrols/tabs.ascx" %>
<%@ Register TagPrefix="Ava" TagName="StepNav" Src="usercontrols/StepNav_vendor.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitlePlaceHolder1" runat="Server">
Trans-Asia VMS Accreditation :: Vendor
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <form id="formVendorHome" runat="server">
    <!--### UPLOADIFY ###-->
    <%--<script src="uploadify/jquery-1.4.2.min.js" type="text/javascript"></script>--%>
    <script src="uploadify/swfobject.js" type="text/javascript"></script>
    <script src="uploadify/jquery.uploadify.v2.1.4.js" type="text/javascript"></script>
    <script src="uploadify/jquery.uploadify.v2.1.4.min.js" type="text/javascript"></script>
    <link href="uploadify/uploadify.css" rel="stylesheet" type="text/css" />
    <!--### UPLOADIFY ENDS ###-->
    <div class="content">
<div class="content_logo">
<img src="images/final_logo_w.png"  border="0" />
</div>
<div class="rounded-corners-top" id="menuAVA">
<ava:tabsnav ID="Tabsnav1" runat="server" />
</div>
<div style="background:#FFF; min-height:445px; padding:10px;" class="rounded-corners-bottom2 menu">
<!--##################-->
<!--BODY CONTENT STARTS-->
<%--<div class="topnav"><a href="#">Application</a>&nbsp;&nbsp;&nbsp;|&nbsp;&nbsp;&nbsp;<a href="#">Enter Authentication Ticket</a>&nbsp;&nbsp;&nbsp;|&nbsp;&nbsp;&nbsp;<a href="#">List of Vendors</a>&nbsp;&nbsp;&nbsp;|&nbsp;&nbsp;&nbsp;<a href="#">Manage Report</a></div>--%>
<ava:stepnav ID="StepNav1" runat="server" />
    <div class="clearfix"></div>
<div style="float:left; width:450px; min-height:300px; margin:0px 10px 0 0px; padding-top:0px; padding-right:10px; border-right:#ccc 1px solid; ">
  <div style="margin-top:0px;">
    <div style="float:left; margin:0px 0px 0 0;"> 
        <div style="height:75px;">
            <asp:Label ID="CompanyNameLbl" runat="server" Text=""></asp:Label>
            <div style="margin:10px 0px; color:#333; font-size:18px; width:450px;">Vendor Information</div>
        </div>
    <div class="separator1"></div>
    <style type="text/css">
        .tblHome td
        {
            font-size:14px;
         }
        .tblHome th
        {
            font-size:14px;
         }
        .auto-style1 {
            height: 12.75pt;
            width: 146pt;
            color: black;
            font-size: 10.0pt;
            font-weight: 400;
            font-style: normal;
            text-decoration: none;
            font-family: Arial, sans-serif;
            text-align: general;
            vertical-align: bottom;
            white-space: nowrap;
            border-style: none;
            border-color: inherit;
            border-width: medium;
            padding: 0px;
        }
        .auto-style2 {
            height: 12.75pt;
            color: black;
            font-size: 10.0pt;
            font-weight: 400;
            font-style: normal;
            text-decoration: none;
            font-family: Arial, sans-serif;
            text-align: general;
            vertical-align: bottom;
            white-space: nowrap;
            border-style: none;
            border-color: inherit;
            border-width: medium;
            padding: 0px;
        }
        </style>
        <table style="width: 100%;" cellpadding="10">
            <%--<tr>
                <td valign="top"><h3>1.</h3></td>
                <td><a href="vendor_generalprocedure.aspx<%= queryString %>"  style="font-size:16px; font-weight:bold;">General Guidelines and Procedure</a></td>
            </tr>--%>
            <tr>
                <td valign="top" class="auto-style5"><h3>1.</h3></td>
                <td class="auto-style5"><a href="vendor_requirements.aspx<%= queryString %>"  style="font-size:16px; font-weight:bold;">MMD Guidelines for Suppliers</a></td>
            </tr>
            <tr>
                <td valign="top"><h3>2.</h3></td>
                <td><span style="font-size:16px; font-weight:bold;">Vendor Information</span></td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>
                    <table style="width: 100%;" class="tblHome" cellpadding="5">
            <tr>
                <th width="25">i.</th>
                <th>
                    &nbsp;
                <a href="vendor_01_vendorInfo.aspx<%= queryString %>" >Vendor Information</a></th>
                </td>
            </tr>
            <tr>
                <td>ii.</td>
                <td>
                    &nbsp;
                <a href="vendor_02_productServices.aspx<%= queryString %>" >Product & Services</a></td>
                </td>
            </tr>
            <tr>
                <td>iii.</td>
                <td>
                    &nbsp;
                <a href="vendor_03_businessOperational.aspx<%= queryString %>" >Business Operational</a></td>
                </td>
            </tr>
            <tr>
                <td>iv.</td>
                <td>
                    &nbsp;
                <a href="vendor_04_Legal.aspx<%= queryString %>" >Legal Structure</a></td>
                </td>
            </tr>
            <tr>
                <td>v.</td>
                <td>
                    &nbsp;
                <a href="vendor_05_financialInfo.aspx<%= queryString %>" >Financial Information</a></td>
                </td>
            </tr>
            <%--<tr>
                <td>vi.</td>
                <td>
                    &nbsp;
                <a href="vendor_06_Others.aspx<%= queryString %>" >Other Information</a></td>
                </td>
            </tr>--%>
            <tr>
                <td>vi.</td>
                <td>
                    &nbsp;
                <a href="vendor_07_Conflict.aspx<%= queryString %>" >Conflict of Interest</a></td>
                </td>
            </tr>
            <tr>
                <td>vii.</td>
                <td>
                    &nbsp;
                <a href="vendor_08_Undertakings.aspx<%= queryString %>" >Undertakings</a></td>
                </td>
            </tr>
        </table>
                </td>
            </tr>
           
            <tr style="display:none;" >
                <td valign="top"><h3>3.</h3></td>
                <td>
                    <span style="font-size:16px; font-weight:bold;">CERTIFICATION AND WARRANTY</span>
                    <br />
                    <a href="Certification_and_Warranty.pdf" target="_blank"  style="font-size:12px; font-weight:bold; display:none">Download & Print</a>
                    <br />
                     <%--&bull;<a href="#"  style="font-size:12px; font-weight:bold;">Attach signed form</a>--%>

                    <script type="text/javascript">
                        // <![CDATA[
                        $(document).ready(function () {
                            $('.CertAndWarranty_AttachedSignedFile').uploadify({
                                'uploader': 'uploadify/uploadify.swf',
                                'script': 'upload.ashx',

                                'cancelImg': 'uploadify/cancel.png',
                                'auto': true,
                                'multi': true,
                                'fileDesc': 'Attach signed form',
                                'fileExt': '*.jpg;*.png;*.gif;*bmp;*.jpeg;*.doc;*.docx;*.xls;*.xlsx;*.zip;*.rar;*.ppt;*.pdf',
                                'queueSizeLimit': 5,
                                'sizeLimit': 10000000,
                                'folder': 'uploads/<%= VendorId.ToString() %>',
                            'onComplete': function (event, queueID, fileObj, response, data) {
                                //alert(response);
                                $('.CertAndWarranty_AttachedSignedLbl').html('<a href="' + response + '" target="_blank">' + response + '</a>');
                                $('#ContentPlaceHolder1_CertAndWarranty_AttachedSigned').attr('value', response);
                                $('#CertAndWarranty_AttachedSignedx').show();
                            }
                        });
                    });
                    // ]]>
    </script>
        <div style="float:left; width:30px;"><input id="CertAndWarranty_AttachedSignedFile" class="CertAndWarranty_AttachedSignedFile" type="file" runat="server"/></div> 
        <asp:Label ID="CertAndWarranty_AttachedSignedLbl" CssClass="CertAndWarranty_AttachedSignedLbl" runat="server" Text="Attach signed form" style="float:left; padding-top:3px; display:block"></asp:Label>  <img src="images/xicon.png" style="margin-left:10px; padding-top:5px;display:none;" id="CertAndWarranty_AttachedSignedx" onclick="$('#<%= CertAndWarranty_AttachedSigned.ClientID %>').val('');$('#<%= CertAndWarranty_AttachedSignedLbl.ClientID %>').html('Attach signed form');$(this).hide();" />
        <input id="CertAndWarranty_AttachedSigned" name="CertAndWarranty_AttachedSigned" runat="server" type="hidden" value="" />
                    <div style="font-size:9px; clear:both;">(Max file size: 10 MB)</div>
                </td>
            </tr>
        </table>
    </div>
  </div>
  <div class="clearfix"></div>
</div>


<div style="float:left; width:450px; min-height:300px; margin:0px 0 0 0px; padding-top:0px;">
  
  <div class="clearfix" style="height:75px;">
    
      <h3 style="color:orange; font-size:24px; padding-top:30px;"><asp:Label runat="server" id="clarifyTxt"></asp:Label></h3>
  </div>

    <%--<asp:LinkButton ID="createBt" runat="server" CssClass="bt1"  onclientclick="javascript: if(confirm('Are you sure to submit all information and agree to all terms and conditions?\nOnce submitted, you will no longer be able to edit all informations.'))__doPostBack('continueStp', ''); return false;"><span>AGREE »</span></asp:LinkButton>--%>
    
    <div class="separator1"></div>
    <table style="width: 100%;" cellpadding="10">
        <tr>
            <td>
                <div id="clarifyCommentDiv" runat="server" style="clear:both" visible="false" >
                <h3 style="margin:10px 0px;">Comments</h3>
                <asp:Repeater ID="repeaterCommentsDnbClarify" runat="server" DataSourceID="dsCommentsDnbClarify">
                <ItemTemplate>
                <p><strong><%# Eval("Firstname")%> <%# Eval("Lastname")%></strong>&nbsp;&nbsp;&nbsp;<em><%# Eval("DateCreated")%></em></p>
                <p><%# Eval("Comment")%><br />
                  <br />
                  </ItemTemplate>
                </asp:Repeater>
                <asp:SqlDataSource ID="dsCommentsDnbClarify" runat="server" ConnectionString="<%$ ConnectionStrings:AVAConnectionString %>"
                            SelectCommand="select t1.Comment, t1.DateCreated, t2.FirstName, t2.Lastname from tblCommentsDnbClarify t1, tblUsers t2 WHERE t2.UserId = t1.UserId AND t1.VendorId=@VendorId ORDER BY t1.DateCreated DESC" >
                    <SelectParameters>
                        <asp:SessionParameter Name="VendorId" SessionField="VendorId" Type="Int32" />
	                </SelectParameters>
                  </asp:SqlDataSource>
                    <%--<strong>Comment:</strong><br />
                    <textarea id="Comment" runat="server" cols="40" rows="3"></textarea>--%>
                    <div class="separator1"></div>
                <br /><br />
                 </div>
                <h3>Please address or send your communications to:</h3>

            </td>
        </tr>
        <tr>
            <td>
                <table style="width:100%;">
                    <tr>
                        <td width="50%"><br /><b>Ms. Merlyn Cabahug</b><br />Purchasing Administrator
</td>
                    </tr>
                    <tr>
                        <td><br /><b>Trans-Asia Oil and Energy Development Corporation
</b>
                            <br />Level 11 PHINMA Plaza, 39 Plaza Drive,	
                            <br />Rockwell Center, 1200 Makati City<br />
&nbsp;<table border="0" cellpadding="0" cellspacing="0" style="border-collapse:
 collapse;width:146pt" width="193">
                                <tr height="17">
                                    <td class="auto-style1" colspan="3" height="17" style="mso-ignore: colspan;" width="193">E-mail:<span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span>mmcabahug@phinma.com.ph</td>
                                </tr>
                                <tr height="17">
                                    <td class="auto-style2" colspan="3" height="17" style="mso-ignore: colspan">Tel.<span>&nbsp; </span>#:<span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span>+63.2.8700.291</td>
                                </tr>
                                <tr height="17">
                                    <td class="auto-style2" colspan="3" height="17" style="mso-ignore: colspan">Mobile #:<span>&nbsp;&nbsp; </span>+63.917.5150.291</td>
                                </tr>
                                <tr height="17">
                                    <td class="auto-style2" colspan="3" height="17" style="mso-ignore: colspan">Fax. #<span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span>+63.2.8700.484</td>
                                </tr>
                            </table>
<br />
</td>
                        
                    </tr>
                    
                </table>
                <br /><br />
                <br />
                <br />
            </td>
        </tr>
        <tr style="display:none;">
            <td><script type="text/javascript">
                    // <![CDATA[
                    $(document).ready(function () {
                        $('.paymentProofFile').uploadify({
                            'uploader': 'uploadify/uploadify.swf',
                            'script': 'upload.ashx',

                            'cancelImg': 'uploadify/cancel.png',
                            'auto': true,
                            'multi': true,
                            'fileDesc': 'Attach File',
                            'fileExt': '*.jpg;*.png;*.gif;*bmp;*.jpeg;*.doc;*.docx;*.xls;*.xlsx;*.zip;*.rar;*.ppt;*.pdf',
                            'queueSizeLimit': 5,
                            'sizeLimit': 10000000,
                            'folder': 'uploads/<%= VendorId.ToString() %>',
                                'onComplete': function (event, queueID, fileObj, response, data) {
                                    //alert(response);
                                    $('.paymentProofLbl').html('<a href="' + response + '" target="_blank">' + response + '</a>');
                                    $('#ContentPlaceHolder1_paymentProof').attr('value', response);
                                    $('#paymentProofx').show();
                                }
                            });
                        });
                        // ]]>
    </script>
        <div style="float:left; width:30px;"><input id="paymentProofFile" class="paymentProofFile" type="file" runat="server"/></div> 
        <asp:Label ID="paymentProofLbl" CssClass="paymentProofLbl" runat="server" Text="Attach Proof of Payment" style="float:left; padding-top:3px; display:block"></asp:Label><img src="images/xicon.png" style="margin-left:10px; padding-top:5px;display:none;" id="paymentProofx" onclick="$('#<%= paymentProof.ClientID %>').val('');$('#<%= paymentProofLbl.ClientID %>').html('Attach Proof of Payment');$(this).hide();" />
        <input id="paymentProof" name="paymentProof" runat="server" type="hidden" value="" />
                <div style="font-size:9px; clear:both;">(Max file size: 10 MB)</div>
            </td>
        </tr>
        <tr>
            <td><br />
                <script type="text/javascript">
                    var Msgs = "";
                    function validateForm() {
                        if ($('#<%= paymentProof.ClientID %>').val() == "") {
                            //Msgs = Msgs + "There is no Proof of Payment attached.\n";
                        }
                        if ($('#<%= CertAndWarranty_AttachedSigned.ClientID %>').val() == "") {
                            //Msgs = Msgs + "There is no signed Certification & Warranty attached.\n";
                        }
                        if (Msgs == "") {
                            //javascript: __doPostBack('continueStp', '');
                            javascript: __doPostBack('endorse', '');
                            return false;
                        }
                        else {
                            alert(Msgs); Msgs = "";
                        }
                    }
        </script>
                <%--<asp:LinkButton ID="submitToDNBbt" runat="server" CssClass="bt1"  onclientclick="javascript: if(confirm('Are you sure to submit all vendor information for evaluation?')){__doPostBack('endorse', ''); } return false;"><span>SUBMIT</span></asp:LinkButton>--%>
                <a href="javascript:void(0)" class="bt1" id="submitToDNBbt" runat="server" onclick="validateForm()"><span>SUBMIT &raquo;</span></a>&nbsp;
                <asp:LinkButton ID="SaveBt1" runat="server" CssClass="bt1"  onclientclick="javascript: __doPostBack('justSave', ''); return false;"><span>SAVE</span></asp:LinkButton>
            </td>
        </tr>
        <tr>
            <td><p>&nbsp;</p></td>
        </tr>
        
        <%--<tr>
            <td><asp:LinkButton ID="generateAuthBt" runat="server" CssClass="bt1"  onclientclick="javascript: __doPostBack('generateAuth', ''); return false;"><span>GET AUTH CODE</span></asp:LinkButton></td>
        </tr>--%>
        <tr>
            <td><div id="authDiv" runat="server">
                <h3>Authentication Ticket</h3>
                <div class="clearfix"></div>
                <asp:Label ID="AuthenticationTicketLbl" Text="n/a" runat="server" style="margin:10px 0px; color:#333; font-size:18px; width:450px; font-family:'Times New Roman'" CssClass=""></asp:Label>
                </div>
                </td>
        </tr>
        <tr>
            <td>
                
            </td>
        </tr>
    </table>
</div>

<!--BODY CONTENT ENDS-->
<!--##################-->
  <div class="clearfix"><br /><br /></div>
</div>
</div>
  <div class="clearfix"></div>
</div><!-- content ends --> 
    
  <div class="clearfix"></div>
        </form>
</asp:Content>