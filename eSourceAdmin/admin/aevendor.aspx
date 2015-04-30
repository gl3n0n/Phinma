<%@ Page Language="C#" AutoEventWireup="true" CodeFile="aevendor.aspx.cs" Inherits="admin_aevendor"
    MaintainScrollPositionOnPostback="true" Theme="default"%>
<%@ Register Assembly="Calendar" Namespace="CalendarControl" TagPrefix="cc1" %>
<%@ Register TagPrefix="EBid" TagName="TopDate" Src="~/usercontrol/TopDate.ascx" %>
<%@ Register TagPrefix="EBid" TagName="AdminTopNav" Src="~/usercontrol/admin/adminTopNav.ascx" %>
<%@ Register TagPrefix="EBid" TagName="Admin_TopNav_User" Src="~/usercontrol/admin/Admin_TopNav_User.ascx" %>
<%@ Register TagPrefix="EBid" TagName="AdminLeftNavUser" Src="~/usercontrol/admin/AdminLeftNavUser.ascx" %>
<%@ Register TagPrefix="EBid" TagName="AdminLeftNav" Src="~/usercontrol/admin/AdminLeftNav.ascx" %>
<%@ Register TagPrefix="EBid" TagName="Footer" Src="~/usercontrol/Footer.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>.:| Trans-Asia | Add User |:.</title>
    <meta http-equiv="Content-Language" content="en-us" />
    <meta http-equiv="Content-Type" content="text/html; charset=windows-1252" />
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
    <link href="../css/style_v.css" rel="stylesheet" type="text/css" />
    <link href="../css/style_ua.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../include/util.js"></script>
    <script type="text/javascript" src="../include/addnewvendor.js"></script>
    <script type="text/javascript" src="../include/generaljsfunctions.js"></script>
    <script type="text/javascript" src="../include/checkboxgroup.js"></script>
	
    <script type="text/javascript" src="../include/util.js"></script>
    <script type="text/javascript" src="../include/supplierregistration.js"></script>
    <script type="text/javascript" src="../include/jquery-1.7.1.min.js"></script>
    <script type="text/javascript"> 
    <!--
        var _req;
        $(document).ready(function () {
            $('#frmRegistration input[type=text]').attr("disabled", "disabled");
        });
    //-->
    </script>
</head>
<body onload="SetStatus();CheckCompanyClass();">
    <div align="left">
        <form runat="server" id="frmRegistration">
            <table border="0" cellpadding="0" cellspacing="0" width="100%" id="page">
                <tr>
                    <td valign="top" height="137px">
                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td>
                                    <div align="left" id="masthead">
                                        <EBid:AdminTopNav runat="server" ID="GlobalLinksNav" />
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <EBid:Admin_TopNav_User runat="server" ID="Admin_TopNav_User" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <EBid:TopDate runat="server" ID="TopDate" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table border="0" cellpadding="0" cellspacing="0" width="100%" style="height: 100%">
                            <tr valign="top">
                                <td id="relatedInfo">
                                    <h2>
                                        User Functions</h2>
                                    <EBid:AdminLeftNavUser runat="server" ID="AdminLeftNavUser" />
                                    <p>
                                        &nbsp;</p>
                                    <asp:HiddenField runat="server" ID="hdnBrands" />
                                    <asp:HiddenField runat="server" ID="hdnItems" />
                                    <asp:HiddenField runat="server" ID="hdnServices" />
                                    <asp:HiddenField runat="server" ID="hdnLocations" />
                                    <asp:ListBox runat="server" ID="lstTempRemoveSubCategory" Width="0" Height="0"></asp:ListBox>
                                </td>
                                <td id="content">
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%" id="page0">
                                        <tr>
                                            <td valign="top">
                                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                    <tr>
                                                        <td id="content0">
                                                            <h1>
                                                                <br />
                                                                <asp:Literal ID="litHeader" runat="server"></asp:Literal>
                                                            </h1>
                                                        </td>
                                                    </tr>
                                                    <tr align="right">
                                                        <td valign="top" style="height: 30px">
                                                            <asp:DropDownList ID="ddlUserTypes" runat="server" DataSourceID="dsUserTypes" DataTextField="UserType"
                                                                DataValueField="UserTypeId" Width="150px" AutoPostBack="True" OnSelectedIndexChanged="ddlUserTypes_SelectedIndexChanged">
                                                            </asp:DropDownList>&nbsp;
                                                            <asp:SqlDataSource ID="dsUserTypes" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>'
                                                                SelectCommand="s3p_EBid_UserTypes" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblMessage" runat="server" Font-Size="11px" ForeColor="red"></asp:Label>
                                                            <asp:Wizard ID="Wizard1" runat="server" Width="100%" DisplaySideBar="False" ActiveStepIndex="0">
                                                                <WizardSteps>
                                                                    <asp:WizardStep ID="WizardStep1" runat="server" Title="Step 1">
                                                                        <table align="center" border="0" cellpadding="0" cellspacing="0" width="100%" class="itemDetails">
                                                                            <tr>
                                                                                <th colspan="2">
                                                                                    User information</th>
                                                                            </tr>
                                                                            <tr class="value">
                                                                                <td style="width: 120px; padding-top: 7px">
                                                                                    Username:</td>
                                                                                <td>
                                                                                    <asp:TextBox ID="tbUserName" runat="server" Width="35%" MaxLength="40" />
                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="tbUserName"
                                                                                        Font-Size="11px" ErrorMessage="&lt;br /&gt;&#187; Username is required." Text="* Required"
                                                                                        SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator>
                                                                                    <asp:CustomValidator ID="CustomValidator2" runat="server" ControlToValidate="tbUserName" Text="* Username is already used" ErrorMessage="&lt;br /&gt;&#187; Username is already used. Please try another username." Display="Dynamic" OnServerValidate="CheckUsernameAvailability" SetFocusOnError="True"></asp:CustomValidator>
                                                                                </td>
                                                                            </tr>
                                                                            <tr class="evenCells">
                                                                                <td style="width: 120px; padding-top: 7px">
                                                                                    Email Address:</td>
                                                                                <td style="height: 30px">
                                                                                    <asp:TextBox ID="tbEmail" runat="server" Width="35%" MaxLength="120" />
                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="tbEmail"
                                                                                        ErrorMessage="&lt;br /&gt;&#187; Email Address is required." Text="* Required"
                                                                                        Display="Dynamic" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="tbEmail"
                                                                                        ErrorMessage="&lt;br /&gt;&#187; Invalid Email Address (e.g. gsacramento@asiaonline.net.ph)"
                                                                                        Text="* Invalid Email" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                                                        SetFocusOnError="True"></asp:RegularExpressionValidator>
                                                                                </td>
                                                                            </tr>
																			<tr class="value">
																				<td style="width: 120px; padding-top:7px">
																					Vendor Code:
																				</td>
																				<td>
																					<asp:TextBox ID="tbVendorCode" runat="server" width="35%" MaxLength="50" />
																					<asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="tbVendorCode"
																						Font-Size="11px" ErrorMessage="&lt;br /&gt;&#187; Vendor Code is required." Text="* Required"
                                                                                        SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator>
																				</td>
																			</tr>
																			<tr class="evenCells">
																				<td style="width: 120px; padding-top:7px">
																					Enrollment Date:
																				</td>
																				<td>
																					<asp:TextBox ID="tbEnrollmentDate" runat="server" width="35%" MaxLength="30" />
																					<asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="tbEnrollmentDate"
																						Font-Size="11px" ErrorMessage="&lt;br /&gt;&#187; Enrollment Date is required." Text="* Required"
                                                                                        SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator>
																				</td>
																			</tr>
                                                                        </table>
                                                                        <table border="0" cellpadding="0" cellspacing="0" width="100%" class="itemDetails">
                                                                            <tr>
                                                                                <th>
                                                                                    Type</th>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="value">
                                                                                    <asp:DropDownList runat="server" ID="ddlSupplierType" DataSourceID="dsSupplierTypes"
                                                                                        DataTextField="SupplierTypeDesc" DataValueField="SupplierTypeId" Width="175px">
                                                                                    </asp:DropDownList>
                                                                                    <asp:SqlDataSource ID="dsSupplierTypes" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>'
                                                                                        SelectCommand="s3p_EBid_ShowSupplierTypes" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
																		<!--
																		<table border="0" cellpadding="0" cellspacing="0" width="100%" class="itemDetails">
                                                                            <tr>
                                                                                <th colspan="2">
                                                                                    Accreditation Status</th>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="value" colspan="2">
                                                                                    Exempted? <asp:CheckBox ID="chkAccreditationStatus" runat="server" />
                                                                                </td>
                                                                            </tr>
                                                                        </table>
																		-->
                                                                        <table border="0" cellpadding="0" cellspacing="0" width="100%" class="itemDetails">
                                                                            <tr>
                                                                                <th>Category</th>
																				<th>Sub-category</th>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="value">
                                                                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                                        <tr>
                                                                                            <td width="180" align="left">
                                                                                                <asp:ListBox runat="server" ID="lstCategories" Style="width: 180; height: 150" SelectionMode="Multiple" DataSourceID="dsCategories" DataTextField="CategoryName" DataValueField="CategoryId" >
                                                                                                </asp:ListBox>
                                                                                                <asp:SqlDataSource ID="dsCategories" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>'
                                                                                                    SelectCommand="s3p_EBid_GetAllProductCategory" SelectCommandType="StoredProcedure">
                                                                                                </asp:SqlDataSource>
                                                                                            </td>
                                                                                            <td align="center" style="width: 30px" valign="middle">                                                                                                
                                                                                                <asp:TextBox runat="server" ID="hdnCategories" Width="0px" Height="0px" />
                                                                                                <input type="button" value=">" style="width: 25px;" id="btnSelectCategory" runat="server" /></input>
                                                                                                </input>
                                                                                                </input>
                                                                                                &nbsp;</input><br /><input type="button" value=">>" style="width: 25px;" id="btnSelectAllCategories" runat="server" /> </input>
                                                                                                </input>
                                                                                                </input>
                                                                                                </input>
                                                                                                <br />
                                                                                                <input type="button" value="<<" style="width: 25px;" id="btnDeSelectCategory" runat="server" /></input>
                                                                                                </input>
                                                                                                &nbsp;</input>&nbsp;</input><br /><input type="button" value="<" style="width: 25px;" id="btnDeSelectAllCategories" runat="server" />                                                                                                
                                                                                            </input>
                                                                                            &nbsp; &nbsp; </input>
                                                                                            &nbsp; &nbsp; &nbsp; </input>
                                                                                            </input>
                                                                                            </td>
                                                                                            <td align="left" style="padding-left: 5px;">                                                                                               
                                                                                                <asp:ListBox runat="server" ID="lstSelectedCategories" Style="width: 180; height: 150"
                                                                                                    SelectionMode="Multiple"></asp:ListBox>
                                                                                                <asp:RequiredFieldValidator ID="rfvCategories" runat="server" 
                                                                                                    ErrorMessage="&lt;br /&gt;&#187; Category is required." Text="* Required"
                                                                                                    ControlToValidate="hdnCategories" SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator>                                                                                                    
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </td>
																				<td class="value">
                                                                                    <table border="0" cellpadding="0" cellspacing="0" width="100%" id="table7">
                                                                                        <tr>
                                                                                            <td width="180" align="left">
                                                                                                <asp:ListBox runat="server" ID="lstSubCategory" Style="width: 180; height: 150" SelectionMode="Multiple"  >
                                                                                                </asp:ListBox>
                                                                                            </td>
                                                                                            <td align="center" style="width: 30px" valign="middle">                                                                                                
                                                                                                <asp:TextBox runat="server" ID="hdnSubCategory" Width="0px" Height="0px" />
                                                                                                <input type="button" value=">" style="width: 25px;" id="btnSelectSubCategory" runat="server" /></input>
                                                                                                </input>
                                                                                                </input>
                                                                                                &nbsp;</input><br /><input type="button" value=">>" style="width: 25px;" id="btnSelectAllSubCategories" runat="server" /> </input>
                                                                                                </input>
                                                                                                </input>
                                                                                                </input>
                                                                                                <br />
                                                                                                <input type="button" value="<<" style="width: 25px;" id="btnDeSelectAllSubCategories" runat="server" /></input>
                                                                                                </input>
                                                                                                &nbsp;</input>&nbsp;</input><br /><input type="button" value="<" style="width: 25px;" id="btnDeSelectSubCategory" runat="server" />                                                                                                
                                                                                            </input>
                                                                                            &nbsp; &nbsp; </input>
                                                                                            &nbsp; &nbsp; &nbsp; </input>
                                                                                            </input>
                                                                                            </td>
                                                                                            <td align="left" style="padding-left: 5px;">                                                                                                
                                                                                                <asp:ListBox runat="server" ID="lstSelectedSubCategories" Style="width: 180; height: 150"
                                                                                                    SelectionMode="Multiple" onDblClick="removeABrand();"></asp:ListBox>
                                                                                                <asp:RequiredFieldValidator ID="rfvSubCategories" runat="server"
                                                                                                    ErrorMessage="&lt;br /&gt;&#187; Sub-Category is required." Text="* Required"
                                                                                                    ControlToValidate="hdnSubCategory" SetFocusOnError="True" Display="None" Visible="False"></asp:RequiredFieldValidator>                                                                                                
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                        <br />
																		
																		<!-- OTHER INFO -->
																		<table border="0" cellpadding="0" cellspacing="0" width="100%" class="itemDetails">
																			<tr>
                                                                                <th colspan="9">Other Info</th>	
                                                                            </tr>
																			<tr>
																				<td colspan="3" width="33%" class="value">SLA Date</td>
																				<td colspan="3" width="33%" class="value">Accreditation Duration</td>
																				<td colspan="3" width="33%" class="value">Performance Evaluation Rating</td>
																			</tr>
																			<tr>
																				<td class="value" width="20">&nbsp;</td>
																				<td class="value" width="6%">SIR Date</td>
																				<td class="value" width="12%">
																				<cc1:JSCalendar ID="tbSLASIRDate" runat="server" ImageURL="../calendar/img.gif" EnableViewState="true" ScriptsBasePath="../calendar/" DateFormat="MM/dd/yyyy" Width="165px" ReadOnly="false" MaxLength="10" TabIndex="-1"></cc1:JSCalendar>
																				<!--<asp:TextBox ID="tbSLASIRDate_old" runat="server" width="180px" MaxLength="40" />--></td>
																				<td class="value" width="20">&nbsp;</td>
																				<td class="value" width="6%">FROM</td>
																				<td class="value" width="12%">
																				<!--<asp:TextBox ID="tbAccrFrom_old" runat="server" width="180px" MaxLength="50" />-->
																				<cc1:JSCalendar ID="tbAccrFrom" runat="server" ImageURL="../calendar/img.gif" EnableViewState="true" ScriptsBasePath="../calendar/" DateFormat="MM/dd/yyyy" Width="165px" ReadOnly="false" MaxLength="10" TabIndex="-1"></cc1:JSCalendar>
																				</td>
																				<td class="value" width="20">&nbsp;</td>
																				<td class="value" width="6%">Date</td>
																				<td class="value" width="12%">
																				<!--<asp:TextBox ID="tbPerfDate_old" runat="server" width="180px" MaxLength="40" />-->
																				<cc1:JSCalendar ID="tbPerfDate" runat="server" ImageURL="../calendar/img.gif" EnableViewState="true" ScriptsBasePath="../calendar/" DateFormat="MM/dd/yyyy" Width="165px" ReadOnly="false" MaxLength="10" TabIndex="-1"></cc1:JSCalendar>																			
																				</td>
																			</tr>
																			<tr>
																				<td class="value">&nbsp;</td>
																				<td class="value">Date Approved</td>
																				<td class="value">
																				<!--<asp:TextBox ID="tbSLADateApproved_old" runat="server" width="180px" MaxLength="40" />-->
																				<cc1:JSCalendar ID="tbSLADateApproved" runat="server" ImageURL="../calendar/img.gif" EnableViewState="true" ScriptsBasePath="../calendar/" DateFormat="MM/dd/yyyy" Width="165px" ReadOnly="false" MaxLength="10" TabIndex="-1"></cc1:JSCalendar>																			
																				</td>
																				</td>
																				<td class="value">&nbsp;</td>
																				<td class="value">TO</td>
																				<td class="value">
																				<!--<asp:TextBox ID="tbAccrTo_old" runat="server" width="180px" MaxLength="50" /></td>-->
																				<cc1:JSCalendar ID="tbAccrTo" runat="server" ImageURL="../calendar/img.gif" EnableViewState="true" ScriptsBasePath="../calendar/" DateFormat="MM/dd/yyyy" Width="165px" ReadOnly="false" MaxLength="10" TabIndex="-1"></cc1:JSCalendar>
																				</td>
																				<td class="value">&nbsp;</td>
																				<td class="value">Rate</td>
																				<td class="value"><asp:TextBox ID="tbPerfRate" runat="server" width="180px" MaxLength="40" /></td>
																			</tr>
																			<tr>
																				<td class="value">&nbsp;</td>
																				<td class="value">Accredited Since</td>
																				<td class="value">
																				<!--<asp:TextBox ID="tbSLAAccredited_old" runat="server" width="180px" MaxLength="40" />-->
																				<cc1:JSCalendar ID="tbSLAAccredited" runat="server" ImageURL="../calendar/img.gif" EnableViewState="true" ScriptsBasePath="../calendar/" DateFormat="MM/dd/yyyy" Width="165px" ReadOnly="false" MaxLength="10" TabIndex="-1"></cc1:JSCalendar>
																				</td>
																				<td colspan="6" class="value">&nbsp;</td>
																			</tr>
																			<tr>
																				<td colspan="3" class="value">Composite Rating</td>
																				<td colspan="3"  class="value">Maximum Exposure</td>
																				<td colspan="3" class="value">Incident Report</td>
																			</tr>
																			<tr>
																				<td class="value">&nbsp;</td>
																				<td class="value">SIR Date</td>
																				<td class="value">
																				<!--<asp:TextBox ID="tbCompSIRDate_old" runat="server" width="180px" MaxLength="40" />-->
																				<cc1:JSCalendar ID="tbCompSIRDate" runat="server" ImageURL="../calendar/img.gif" EnableViewState="true" ScriptsBasePath="../calendar/" DateFormat="MM/dd/yyyy" Width="165px" ReadOnly="false" MaxLength="10" TabIndex="-1"></cc1:JSCalendar>
																				</td>
																				<td class="value">&nbsp;</td>
																				<td class="value">SIR Date</td>
																				<td class="value">
																				<!--<asp:TextBox ID="tbMaxSIRDate_old" runat="server" width="180px" MaxLength="50" />-->
																				<cc1:JSCalendar ID="tbMaxSIRDate" runat="server" ImageURL="../calendar/img.gif" EnableViewState="true" ScriptsBasePath="../calendar/" DateFormat="MM/dd/yyyy" Width="165px" ReadOnly="false" MaxLength="10" TabIndex="-1"></cc1:JSCalendar>
																				<!--<td colspan="3" class="value">&nbsp;</td>-->
																				</td>
																				<td class="value">&nbsp;</td>
																				<td class="value">IR Date</td>
																				<td class="value">
																				<!--<asp:TextBox ID="tbIRDate_old" runat="server" width="180px" MaxLength="50" />-->
																				<cc1:JSCalendar ID="tbIRDate" runat="server" ImageURL="../calendar/img.gif" EnableViewState="true" ScriptsBasePath="../calendar/" DateFormat="MM/dd/yyyy" Width="165px" ReadOnly="false" MaxLength="10" TabIndex="-1"></cc1:JSCalendar>
																				</td>
																			</tr>
																			<tr>
																				<td class="value">&nbsp;</td>
																				<td class="value">Rate</td>
																				<td class="value"><asp:TextBox ID="tbCompRate" runat="server" width="180px" MaxLength="40" /></td>
																				<td class="value">&nbsp;</td>
																				<td class="value">Rate</td>
																				<td class="value"><asp:TextBox ID="tbMaxRate" runat="server" width="180px" MaxLength="50" /></td>
																				<!--<td colspan="3" class="value">&nbsp;</td>-->
																				<td class="value">&nbsp;</td>
																				<td class="value">IR Number</td>
																				<td class="value"><asp:TextBox ID="tbIRNumber" runat="server" width="180px" MaxLength="50" /></td>
																			</tr>
																			<tr>
																				<td colspan="7" class="value">&nbsp;</td>
																				<td class="value">IR Description</td>
																				<td class="value"><asp:TextBox ID="tbIRDescription" runat="server" width="180px" MaxLength="50" /></td>
																			</tr>
																		</table>
																		<!-- END OF OTHER INFO -->
																		
                                                                        <br />
                                                                        <div style="text-align: justify; width: 100%; padding-left: 10px;">
                                                                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" Font-Size="11px" HeaderText="&lt;b&gt;Please review the following:&lt;/b&gt;"
                                                                                DisplayMode="SingleParagraph" Font-Names="Arial"></asp:ValidationSummary>
                                                                        </div>
                                                                    </asp:WizardStep>
                                                                    <asp:WizardStep ID="WizardStep2" runat="server" Title="Step 2">
                                                                        <table border="0" cellpadding="0" cellspacing="0" width="100%" class="itemDetails">
                                                                            <tr>
                                                                                <th colspan="2">
                                                                                    Company information</th>
                                                                            </tr>
                                                                            <tr class="value">
                                                                                <td style="width: 120px; padding-top: 7px">
                                                                                    Company Name:</td>
                                                                                <td>
                                                                                    <asp:TextBox ID="tbCompanyName" runat="server" Width="35%" MaxLength="100" />
                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="tbCompanyName"
                                                                                        ErrorMessage="&lt;br /&gt;&#187; Company name is required." EnableTheming="True" SetFocusOnError="True">* Required</asp:RequiredFieldValidator>
                                                                                </td>
                                                                            </tr>
                                                                            <tr class="evenCells">
                                                                                <td style="width: 120px; padding-top: 7px" align="center" valign="middle">
                                                                                    Address: (Head Office)</td>
                                                                                <td>
                                                                                    <asp:TextBox ID="tbHeadOfficeAddress1" runat="server" Width="56%" MaxLength="100" />
                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                                                                                        ErrorMessage="&lt;br /&gt;&#187; Company address is required." Text="* Required"
                                                                                        ControlToValidate="tbHeadOfficeAddress1" SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator>
                                                                                    <br />
                                                                                    <asp:TextBox ID="tbHeadOfficeAddress2" runat="server" Width="56%" MaxLength="100" />                                                                                    
                                                                                </td>
                                                                            </tr>
                                                                            <tr class="value">
                                                                                <td style="width: 120px; padding-top: 7px">
                                                                                    Telephone:</td>
                                                                                <td>
                                                                                    <asp:TextBox ID="tbTelephone" runat="server" Width="20%" MaxLength="20" />
                                                                                    <%--<asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="tbTelephone"
                                                                                        ErrorMessage="&lt;br /&gt;&#187; Invalid Format eg. 888-8888 or (888)888-8888." Text="* Invalid Format"
                                                                                        ValidationExpression="((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}" SetFocusOnError="True"></asp:RegularExpressionValidator>--%>
                                                                                </td>
                                                                            </tr>
                                                                            <tr class="evenCells">
                                                                                <td style="width: 120px; padding-top: 7px">
                                                                                    Fax:</td>
                                                                                <td>
                                                                                    <asp:TextBox ID="tbFax" runat="server" Width="20%" MaxLength="20" />
                                                                                    &nbsp; Extension:
                                                                                    <asp:TextBox ID="tbExtension" runat="server" Width="50px" MaxLength="15" />
                                                                                </td>
                                                                            </tr>
                                                                            <tr class="value">
                                                                                <td style="width: 120px; padding-top: 7px">
                                                                                    Address: (Branch)</td>
                                                                                <td>
                                                                                    <asp:TextBox ID="tbBranchAddress1" runat="server" Width="56%" MaxLength="100" /><br />
                                                                                    <asp:TextBox ID="tbBranchAddress2" runat="server" Width="56%" MaxLength="100" />
                                                                                </td>
                                                                            </tr>
                                                                            <tr class="evenCells">
                                                                                <td style="width: 120px; padding-top: 7px">
                                                                                    Telephone:</td>
                                                                                <td>
                                                                                    <asp:TextBox ID="tbBranchPhone" runat="server" Width="20%" MaxLength="20" />
                                                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="tbBranchPhone"
                                                                                        ErrorMessage="&lt;br /&gt;&#187; Invalid Format eg. 888-8888 or (888)888-8888." Text="* Invalid Format"
                                                                                        ValidationExpression="((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}" SetFocusOnError="True"></asp:RegularExpressionValidator>
                                                                                </td>
                                                                            </tr>
                                                                            <tr class="value">
                                                                                <td style="width: 120px; padding-top: 7px">
                                                                                    Mobile No:</td>
                                                                                <td>
                                                                                    <asp:TextBox ID="tbMobileNo" runat="server" Width="20%" MaxLength="11" />
                                                                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="tbMobileNo"
                                                                                        ErrorMessage="&lt;br /&gt;&#187; Mobile No. is required." EnableTheming="True" SetFocusOnError="True">* Required</asp:RequiredFieldValidator>
                                                                                    <asp:CustomValidator ID="CustomValidator1" runat="server" ControlToValidate="tbMobileNo" Text="* Mobile No. is invalid" ErrorMessage="&lt;br /&gt;&#187; Mobile No. is invalid." Display="Dynamic" OnServerValidate="CheckMobileNo" SetFocusOnError="True"></asp:CustomValidator>--%>
                                                                                </td>
                                                                            </tr>
                                                                            <tr class="evenCells">
                                                                                <td style="width: 120px; padding-top: 7px">
                                                                                    Fax:</td>
                                                                                <td>
                                                                                    <asp:TextBox ID="tbBranchFax" runat="server" Width="20%" MaxLength="20" />
                                                                                    &nbsp; Extension:
                                                                                    <asp:TextBox ID="tbBranchExtension" runat="server" Width="50px" MaxLength="15" />
                                                                                </td>
                                                                            </tr>
                                                                            <tr class="value">
                                                                                <td style="width: 120px; padding-top: 7px">
                                                                                    VAT Reg. No.:<br />
                                                                                    <br />
                                                                                    TIN No.:</td>
                                                                                <td>
                                                                                    <asp:TextBox ID="tbVatRegNo" runat="server" Width="30%" MaxLength="100" /><br />
                                                                                    <asp:TextBox ID="tbTIN" runat="server" Width="30%" MaxLength="80" />
                                                                                </td>
                                                                            </tr>
                                                                            <tr class="evenCells">
                                                                                <td style="width: 120px; padding-top: 7px">
                                                                                    P.O. Box:<br />
                                                                                    <br />
                                                                                    Postal Code:</td>
                                                                                <td>
                                                                                    <asp:TextBox ID="tbPOBox" runat="server" Width="15%" MaxLength="30" /><br />
                                                                                    <asp:TextBox ID="tbPostalCode" runat="server" Width="10%" MaxLength="20" />
                                                                                </td>
                                                                            </tr>
                                                                            <tr class="value">
                                                                                <td style="width: 120px; padding-top: 7px">
                                                                                    Standard Terms of
                                                                                    <br />
                                                                                    &nbsp;&nbsp;Payment:<br />
                                                                                    Special Terms:<br />
                                                                                    <br />
                                                                                    Minimum Order Value:
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="tbTermsofPayment" runat="server" Width="20%" MaxLength="70" /><br />
                                                                                    <asp:TextBox ID="tbSpecialTerms" runat="server" Width="20%" MaxLength="70" />%PPD<br />
                                                                                    <asp:TextBox ID="tbMinOrderValue" runat="server" Width="20%" MaxLength="20" />
                                                                                    <asp:CustomValidator ID="CustomValidator3" runat="server" ControlToValidate="tbMinOrderValue"
                                                                                        Display="Dynamic" EnableClientScript="False" ErrorMessage="&lt;br /&gt;&#187; Invalid Format"
                                                                                        SetFocusOnError="True" OnServerValidate="CheckIfFloat">* Invalid Format</asp:CustomValidator>
                                                                                </td>
                                                                            </tr>
                                                                            <tr class="evenCells">
                                                                                <td style="width: 120px; padding-top: 7px">
                                                                                    Sales Person:<br />
                                                                                    <br />
                                                                                    Telephone:</td>
                                                                                <td>
                                                                                    <asp:TextBox ID="tbSalesPerson" runat="server" Width="30%" MaxLength="100" /><br />
                                                                                    <asp:TextBox ID="tbSalesPersonPhone" runat="server" Width="20%" MaxLength="20" />
                                                                                    <%--<asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="tbSalesPersonPhone"
                                                                                        ErrorMessage="&lt;br /&gt;&#187; Invalid Format eg. 888-8888 or (888)888-8888." Text="* Invalid Format"
                                                                                        ValidationExpression="((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}" SetFocusOnError="True"></asp:RegularExpressionValidator>--%>
                                                                                </td>
                                                                            </tr>
                                                                            <tr class="value">
                                                                                <td style="width: 120px; padding-top: 7px">
                                                                                    Type of Business Organization:</td>
                                                                                <td>
                                                                                    <asp:RadioButtonList ID="rbOrganizationType" runat="server" RepeatDirection="Horizontal">
                                                                                        <asp:ListItem Value="1">Individual</asp:ListItem>
                                                                                        <asp:ListItem Value="2">Partnership</asp:ListItem>
                                                                                        <asp:ListItem Value="3">Corporation</asp:ListItem>
                                                                                    </asp:RadioButtonList>                                                                                    
                                                                                </td>
                                                                            </tr>
                                                                            <tr class="evenCells">
                                                                                <td style="width: 120px; padding-top: 7px">
                                                                                    Ownership:</td>
                                                                                <td>
                                                                                    <asp:Label ID="lblOwnershipFilipino" runat="server" />
                                                                                    <asp:TextBox ID="tbOwnershipFilipino" runat="server" Width="30px" MaxLength="3" />%
                                                                                    Filipino&nbsp;&nbsp;&nbsp;&nbsp;
                                                                                    <asp:TextBox ID="tbOwnershipOther" runat="server" Width="30px" MaxLength="3" />%
                                                                                    Other Nationality
                                                                                </td>
                                                                            </tr>
                                                                            <tr class="value">
                                                                                <td style="width: 120px; padding-top: 7px">
                                                                                    Company Classification (for goods Supplier Only):</td>
                                                                                <td style="padding-left: 0;">
                                                                                    <table id="chkCompanyClassification" border="0">
                                                                                        <tr>
                                                                                            <td>
                                                                                                <input runat="server" id="chkCompanyClassification_0" type="checkbox" onclick="CheckCompanyClass();" />
                                                                                                &nbsp;&nbsp;&nbsp;&nbsp;<label for="chkCompanyClassification_0">Manufacturer</label></td>
                                                                                            <td>
                                                                                                <input runat="server" id="chkCompanyClassification_1" type="checkbox" onclick="CheckCompanyClass();" />
                                                                                                &nbsp;&nbsp;&nbsp;&nbsp;<label for="chkCompanyClassification_1">Importer</label></td>
                                                                                            <td>
                                                                                                <input runat="server" id="chkCompanyClassification_2" type="checkbox" onclick="CheckCompanyClass();" />
                                                                                                &nbsp;&nbsp;&nbsp;&nbsp;<label for="chkCompanyClassification_2">Dealer</label></td>
                                                                                            <td>
                                                                                                <input runat="server" id="chkCompanyClassification_3" type="checkbox" onclick="CheckCompanyClass();" />
                                                                                                &nbsp;&nbsp;&nbsp;&nbsp;<label for="chkCompanyClassification_3">Trader</label></td>
                                                                                            <td>
                                                                                                <input runat="server" id="chkCompanyClassification_4" type="checkbox" onclick="CheckCompanyClass();" />
                                                                                                &nbsp;&nbsp;&nbsp;&nbsp;<label for="chkCompanyClassification_4">N/A</label></td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </td>
                                                                            </tr>
                                                                            <tr class="evenCells">
                                                                                <td style="width: 120px; padding-top: 7px">
                                                                                    Sole Supplier:</td>
                                                                                <td>
                                                                                    <asp:TextBox ID="tbSoleSupplier1" runat="server" Width="56%" MaxLength="100" /><br />
                                                                                    <asp:TextBox ID="tbSoleSupplier2" runat="server" Width="56%" MaxLength="100" />
                                                                                </td>
                                                                            </tr>
                                                                            <tr class="value">
                                                                                <td style="width: 120px; padding-top: 7px">
                                                                                    Specialization:</td>
                                                                                <td>
                                                                                    <asp:TextBox ID="tbSpecialization" runat="server" Width="56%" MaxLength="150" />
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                        <br />
                                                                        <div style="text-align: justify; width: 100%; padding-left: 10px;">
                                                                            <asp:ValidationSummary ID="ValidationSummary2" runat="server" Font-Size="11px" HeaderText="&lt;b&gt;Please review the following:&lt;/b&gt;"
                                                                                DisplayMode="SingleParagraph" Font-Names="Arial"></asp:ValidationSummary>
                                                                        </div>
                                                                    </asp:WizardStep>
                                                                    <asp:WizardStep ID="WizardStep3" runat="server" Title="Step 3">
                                                                        <table border="0" cellpadding="0" cellspacing="0" width="100%" class="itemDetails">
                                                                            <tr>
                                                                                <th colspan="2">
                                                                                    Key Personnel</th>
                                                                            </tr>
                                                                            <tr>
                                                                                <td width="273">
                                                                                    Name</td>
                                                                                <td>
                                                                                    Position</td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="value">
                                                                                    <asp:TextBox ID="tbKeyPersonnel" runat="server" Width="250px" MaxLength="100" /></td>
                                                                                <td class="value">
                                                                                    <asp:TextBox ID="tbKpPosition" runat="server" Width="250px" MaxLength="50" />
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                        <table border="0" cellpadding="0" cellspacing="0" width="100%" class="itemDetails">
                                                                            <tr>
                                                                                <th>
                                                                                    ISO Standard</th>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    Check all that apply</td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="value">
                                                                                    <asp:CheckBox ID="cb9001" runat="server" Text="ISO 9001" Width="100px" />
                                                                                    <asp:CheckBox ID="cb9002" runat="server" Text="ISO 9002" />
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                        <table border="0" cellpadding="0" cellspacing="0" width="100%" class="itemDetails">
                                                                            <tr>
                                                                                <th colspan="2">
                                                                                    Philippine Contractor Association Board (PCAB) Class</th>
                                                                            </tr>
                                                                            <tr class="value">
                                                                                <td>
                                                                                    Select Class:
                                                                                    <asp:DropDownList ID="ddlPCABClass" runat="server" Width="94px" DataSourceID="dsPCABClass"
                                                                                        DataTextField="PCAB Class Name" DataValueField="PCAB Class Id">
                                                                                    </asp:DropDownList>
                                                                                    <asp:SqlDataSource ID="dsPCABClass" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>'
                                                                                        SelectCommand="s3p_EBid_QueryAllPCABClass" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                        <table id="Table1" border="0" cellpadding="0" cellspacing="0" width="100%" class="itemDetails"
                                                                            runat="server">
                                                                            <tr runat="server">
                                                                                <th runat="server">
                                                                                    Present Services Availed From Trans-Asia
                                                                                </th>
                                                                            </tr>
                                                                            <tr runat="server">
                                                                                <td valign="top" runat="server">
                                                                                    <asp:GridView runat="server" ID="gvPresentSvc" AutoGenerateColumns="False" CssClass="innerTable"
                                                                                        OnRowDataBound="gvPresentSvc_RowDataBound" OnRowCommand="gvPresentSvc_RowCommand"
                                                                                        BorderWidth="0px" Width="100%">
                                                                                        <RowStyle CssClass="value" />
                                                                                        <EditRowStyle CssClass="value" />
                                                                                        <AlternatingRowStyle CssClass="evenCells" />
                                                                                        <Columns>
                                                                                            <asp:TemplateField HeaderText="Type of Plan">
                                                                                                <HeaderStyle BackColor="#DBEAF5" BorderWidth="0px" Font-Bold="False" ForeColor="Black"
                                                                                                    Width="120px" />
                                                                                                <ItemTemplate>
                                                                                                    &nbsp;<asp:DropDownList runat="server" ID="ddlPlans" DataSourceID="dsGlobePlans"
                                                                                                        Width="90%" DataTextField="PlanName" DataValueField="PlanID">
                                                                                                    </asp:DropDownList>
                                                                                                    <asp:SqlDataSource ID="dsGlobePlans" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>'
                                                                                                        SelectCommand="s3p_EBid_QueryVendorPlans" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
                                                                                                    <asp:HiddenField ID="hdPlanID" runat="server" Value='<% #Bind("PlanID")%>' />
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="Account Number">
                                                                                                <HeaderStyle BackColor="#DBEAF5" BorderWidth="0px" Font-Bold="False" ForeColor="Black"
                                                                                                    Width="150px" />
                                                                                                <ItemTemplate>
                                                                                                    <asp:TextBox runat="server" ID="lblAcctNo" onkeypress="return PhoneAndNoValidator(this)"
                                                                                                        Width="100%" Text='<% #Bind("AccountNo")%>'></asp:TextBox>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="Credit Limit">
                                                                                                <HeaderStyle BackColor="#DBEAF5" BorderWidth="0px" Font-Bold="False" ForeColor="Black"
                                                                                                    Width="150px" />
                                                                                                <ItemTemplate>
                                                                                                    <asp:TextBox runat="server" ID="lblCreditLimit" Text='<% #Bind("CreditLimit")%>'
                                                                                                        Width="100%" onkeypress="return(currencyFormat(this,',','.',event))"></asp:TextBox>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField>
                                                                                                <ItemTemplate>
                                                                                                    <asp:LinkButton runat="server" ID="lnkRemove" Text="Remove" CausesValidation="false"
                                                                                                        CommandName="Remove" CommandArgument='<% #Bind("index")%>' Width="50px"></asp:LinkButton>
                                                                                                </ItemTemplate>
                                                                                                <ItemStyle VerticalAlign="Middle" />
                                                                                                <HeaderStyle BackColor="#DBEAF5" BorderWidth="0px" />
                                                                                            </asp:TemplateField>
                                                                                        </Columns>
                                                                                    </asp:GridView>
                                                                                </td>
                                                                            </tr>
                                                                            <tr runat="server">
                                                                                <td class="value" runat="server">
                                                                                    <asp:LinkButton ID="lnkAddServices" runat="server" Text="Add Services" OnClick="lnkAddServices_Click"
                                                                                        Width="80px" />
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                        <table id="Table4" border="0" cellpadding="0" cellspacing="0" width="100%" class="itemDetails"
                                                                            runat="server">
                                                                            <tr runat="server">
                                                                                <th colspan="3" runat="server">
                                                                                    Relative Working in Trans-Asia</th>
                                                                            </tr>
                                                                            <tr runat="server">
                                                                                <td valign="top" runat="server">
                                                                                    <asp:GridView runat="server" ID="gvRelatives" AutoGenerateColumns="False" CssClass="innerTable"
                                                                                        OnRowDataBound="gvRelatives_RowDataBound" OnRowCommand="gvRelatives_RowCommand"
                                                                                        BorderWidth="0px" Width="100%">
                                                                                        <RowStyle CssClass="value" />
                                                                                        <EditRowStyle CssClass="value" />
                                                                                        <AlternatingRowStyle CssClass="evenCells" />
                                                                                        <Columns>
                                                                                            <asp:TemplateField HeaderText="Name">
                                                                                                <HeaderStyle BackColor="#DBEAF5" BorderWidth="0px" Font-Bold="False" ForeColor="Black"
                                                                                                    Width="150px" />
                                                                                                <ItemTemplate>
                                                                                                    <asp:TextBox runat="server" ID="lblRelative" Text='<% #Bind("VendorRelative")%>'
                                                                                                        Width="100%"></asp:TextBox>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="Title/Position">
                                                                                                <HeaderStyle BackColor="#DBEAF5" BorderWidth="0px" Font-Bold="False" ForeColor="Black"
                                                                                                    Width="150px" />
                                                                                                <ItemTemplate>
                                                                                                    <asp:TextBox runat="server" ID="lblTitle" Text='<% #Bind("Title")%>' Width="100%"></asp:TextBox>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="Relationship">
                                                                                                <HeaderStyle BackColor="#DBEAF5" BorderWidth="0px" Font-Bold="False" ForeColor="Black"
                                                                                                    Width="150px" />
                                                                                                <ItemTemplate>
                                                                                                    <asp:TextBox runat="server" ID="lblRelationship" Text='<% #Bind("Relationship")%>'
                                                                                                        Width="100%"></asp:TextBox>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField>
                                                                                                <ItemTemplate>
                                                                                                    <asp:LinkButton runat="server" ID="lnkRemove" Text="Remove" CausesValidation="false"
                                                                                                        CommandName="Remove" CommandArgument='<% #Bind("index")%>' Width="50px"></asp:LinkButton>
                                                                                                </ItemTemplate>
                                                                                                <ItemStyle VerticalAlign="Middle" />
                                                                                                <HeaderStyle BackColor="#DBEAF5" BorderWidth="0px" />
                                                                                            </asp:TemplateField>
                                                                                        </Columns>
                                                                                    </asp:GridView>
                                                                                </td>
                                                                            </tr>
                                                                            <tr class="value" runat="server">
                                                                                <td runat="server">
                                                                                    <asp:LinkButton ID="lnkAddRelative" runat="server" Text="Add Relative" OnClick="lnkAddRelative_Click"
                                                                                        Width="80px" />
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                        <table id="Table2" border="0" cellpadding="0" cellspacing="0" width="100%" class="itemDetails"
                                                                            runat="server">
                                                                            <tr runat="server">
                                                                                <th colspan="2" runat="server">
                                                                                    References</th>
                                                                            </tr>
                                                                            <tr runat="server">
                                                                                <td valign="top" runat="server">
                                                                                    <asp:GridView runat="server" ID="gvMajCustomers" AutoGenerateColumns="False" CssClass="innerTable"
                                                                                        OnRowDataBound="gvMajCustomers_RowDataBound" OnRowCommand="gvMajCustomers_RowCommand"
                                                                                        BorderWidth="0px" Width="100%">
                                                                                        <RowStyle CssClass="value" />
                                                                                        <EditRowStyle CssClass="value" />
                                                                                        <AlternatingRowStyle CssClass="evenCells" />
                                                                                        <Columns>
                                                                                            <asp:TemplateField HeaderText="Major Customers">
                                                                                                <HeaderStyle BackColor="#DBEAF5" BorderWidth="0px" Font-Bold="False" ForeColor="Black"
                                                                                                    Width="150px" />
                                                                                                <ItemTemplate>
                                                                                                    <asp:TextBox runat="server" ID="lblName" Text='<% #Bind("CompanyName")%>' Width="100%"></asp:TextBox>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="Average Monthly Sales">
                                                                                                <HeaderStyle BackColor="#DBEAF5" BorderWidth="0px" Font-Bold="False" ForeColor="Black"
                                                                                                    Width="150px" />
                                                                                                <ItemTemplate>
                                                                                                    <asp:TextBox runat="server" ID="lblAveMonthly" Text='<% #Bind("AveMonthlySales")%>'
                                                                                                        Width="100%" onkeypress="return(currencyFormat(this,',','.',event))"></asp:TextBox>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField>
                                                                                                <ItemTemplate>
                                                                                                    <asp:LinkButton runat="server" ID="lnkRemove" Text="Remove" CausesValidation="false"
                                                                                                        CommandName="Remove" CommandArgument='<% #Bind("index")%>' Width="50px"></asp:LinkButton>
                                                                                                </ItemTemplate>
                                                                                                <ItemStyle VerticalAlign="Middle" />
                                                                                                <HeaderStyle BackColor="#DBEAF5" BorderWidth="0px" />
                                                                                            </asp:TemplateField>
                                                                                        </Columns>
                                                                                    </asp:GridView>
                                                                                </td>
                                                                            </tr>
                                                                            <tr runat="server">
                                                                                <td valign="top" runat="server">
                                                                                    <asp:GridView runat="server" ID="gvBanks" AutoGenerateColumns="False" CssClass="innerTable"
                                                                                        OnRowDataBound="gvBanks_RowDataBound" OnRowCommand="gvBanks_RowCommand" BorderWidth="0px"
                                                                                        Width="100%">
                                                                                        <RowStyle CssClass="value" />
                                                                                        <EditRowStyle CssClass="value" />
                                                                                        <AlternatingRowStyle CssClass="evenCells" />
                                                                                        <Columns>
                                                                                            <asp:TemplateField HeaderText="Banks">
                                                                                                <HeaderStyle BackColor="#DBEAF5" BorderWidth="0px" Font-Bold="False" ForeColor="Black"
                                                                                                    Width="150px" />
                                                                                                <ItemTemplate>
                                                                                                    <asp:TextBox runat="server" ID="lblName" Text='<% #Bind("CompanyName")%>' Width="100%"></asp:TextBox>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="Credit Line">
                                                                                                <HeaderStyle BackColor="#DBEAF5" BorderWidth="0px" Font-Bold="False" ForeColor="Black"
                                                                                                    Width="150px" />
                                                                                                <ItemTemplate>
                                                                                                    <asp:TextBox runat="server" ID="lblCreditLine" Text='<% #Bind("CreditLine")%>' Width="100%"
                                                                                                        onkeypress="return(currencyFormat(this,',','.',event))"></asp:TextBox>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField>
                                                                                                <ItemTemplate>
                                                                                                    <asp:LinkButton runat="server" ID="lnkRemove" Text="Remove" CausesValidation="false"
                                                                                                        CommandName="Remove" CommandArgument='<% #Bind("index")%>' Width="50px"></asp:LinkButton>
                                                                                                </ItemTemplate>
                                                                                                <ItemStyle VerticalAlign="Middle" />
                                                                                                <HeaderStyle BackColor="#DBEAF5" BorderWidth="0px" />
                                                                                            </asp:TemplateField>
                                                                                        </Columns>
                                                                                    </asp:GridView>
                                                                                </td>
                                                                            </tr>
                                                                            <tr runat="server">
                                                                                <td valign="top" runat="server">
                                                                                    <asp:GridView runat="server" ID="gvAffiliate" AutoGenerateColumns="False" CssClass="innerTable"
                                                                                        OnRowDataBound="gvAffiliate_RowDataBound" OnRowCommand="gvAffiliate_RowCommand"
                                                                                        BorderWidth="0px" Width="100%">
                                                                                        <RowStyle CssClass="value" />
                                                                                        <EditRowStyle CssClass="value" />
                                                                                        <AlternatingRowStyle CssClass="evenCells" />
                                                                                        <Columns>
                                                                                            <asp:TemplateField HeaderText="Affiliated Companies">
                                                                                                <HeaderStyle BackColor="#DBEAF5" BorderWidth="0px" Font-Bold="False" ForeColor="Black"
                                                                                                    Width="150px" />
                                                                                                <ItemTemplate>
                                                                                                    <asp:TextBox runat="server" ID="lblName" Text='<% #Bind("CompanyName")%>' Width="100%"></asp:TextBox>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="Kind of Business">
                                                                                                <HeaderStyle BackColor="#DBEAF5" BorderWidth="0px" Font-Bold="False" ForeColor="Black"
                                                                                                    Width="150px" />
                                                                                                <ItemTemplate>
                                                                                                    <asp:TextBox runat="server" ID="lblBusiness" Text='<% #Bind("KindOfBusiness")%>'
                                                                                                        Width="100%"></asp:TextBox>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField>
                                                                                                <ItemTemplate>
                                                                                                    <asp:LinkButton runat="server" ID="lnkRemove" Text="Remove" CausesValidation="false"
                                                                                                        CommandName="Remove" CommandArgument='<% #Bind("index")%>' Width="50px"></asp:LinkButton>
                                                                                                </ItemTemplate>
                                                                                                <ItemStyle VerticalAlign="Middle" />
                                                                                                <HeaderStyle BackColor="#DBEAF5" BorderWidth="0px" />
                                                                                            </asp:TemplateField>
                                                                                        </Columns>
                                                                                    </asp:GridView>
                                                                                </td>
                                                                            </tr>
                                                                            <tr runat="server">
                                                                                <td valign="top" runat="server">
                                                                                    <asp:GridView runat="server" ID="gvExtAuditors" AutoGenerateColumns="False" CssClass="innerTable"
                                                                                        OnRowDataBound="gvExtAuditors_RowDataBound" OnRowCommand="gvExtAuditors_RowCommand"
                                                                                        BorderWidth="0px" Width="100%">
                                                                                        <RowStyle CssClass="value" />
                                                                                        <EditRowStyle CssClass="value" />
                                                                                        <AlternatingRowStyle CssClass="evenCells" />
                                                                                        <Columns>
                                                                                            <asp:TemplateField HeaderText="External Auditors">
                                                                                                <HeaderStyle BackColor="#DBEAF5" BorderWidth="0px" Font-Bold="False" ForeColor="Black"
                                                                                                    Width="150px" />
                                                                                                <ItemTemplate>
                                                                                                    <asp:TextBox runat="server" ID="lblName" Text='<% #Bind("CompanyName")%>' Width="100%"></asp:TextBox>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="Legal Counsel">
                                                                                                <HeaderStyle BackColor="#DBEAF5" BorderWidth="0px" Font-Bold="False" ForeColor="Black"
                                                                                                    Width="150px" />
                                                                                                <ItemTemplate>
                                                                                                    <asp:TextBox runat="server" ID="lblLegalCounsel" Text='<% #Bind("LegalCounsel")%>'
                                                                                                        Width="100%"></asp:TextBox>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField>
                                                                                                <ItemTemplate>
                                                                                                    <asp:LinkButton runat="server" ID="lnkRemove" Text="Remove" CausesValidation="false"
                                                                                                        CommandName="Remove" CommandArgument='<% #Bind("index")%>' Width="50px"></asp:LinkButton>
                                                                                                </ItemTemplate>
                                                                                                <ItemStyle VerticalAlign="Middle" />
                                                                                                <HeaderStyle BackColor="#DBEAF5" BorderWidth="0px" />
                                                                                            </asp:TemplateField>
                                                                                        </Columns>
                                                                                    </asp:GridView>
                                                                                </td>
                                                                            </tr>
                                                                            <tr runat="server">
                                                                                <td class="value" runat="server">
                                                                                    <asp:LinkButton ID="lnkReferences" runat="server" Text="Add References" OnClick="lnkReferences_Click"
                                                                                        ToolTip="Select type of reference to be added" Width="90px" />
                                                                                    <asp:DropDownList ID="ddlReference" runat="server" Width="150px">
                                                                                        <asp:ListItem Selected="True" Value="1">Major Customers</asp:ListItem>
                                                                                        <asp:ListItem Value="2">Banks</asp:ListItem>
                                                                                        <asp:ListItem Value="3">Affiliated Companies</asp:ListItem>
                                                                                        <asp:ListItem Value="4">External Auditors</asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </asp:WizardStep>
                                                                    <asp:WizardStep ID="WizardStep4" runat="server" Title="Step 4">
                                                                        <table id="Table3" border="0" cellpadding="0" cellspacing="0" width="100%" class="itemDetails"
                                                                            runat="server">
                                                                            <tr runat="server">
                                                                                <th colspan="3" runat="server">
                                                                                    Equipment</th>
                                                                            </tr>
                                                                            <tr runat="server">
                                                                                <td valign="top" runat="server">
                                                                                    <asp:GridView runat="server" ID="gvEquipments" AutoGenerateColumns="False" CssClass="innerTable"
                                                                                        OnRowDataBound="gvEquipments_RowDataBound" OnRowCommand="gvEquipments_RowCommand"
                                                                                        BorderWidth="0px" Width="100%">
                                                                                        <RowStyle CssClass="value" />
                                                                                        <EditRowStyle CssClass="value" />
                                                                                        <AlternatingRowStyle CssClass="evenCells" />
                                                                                        <Columns>
                                                                                            <asp:TemplateField HeaderText="Type">
                                                                                                <HeaderStyle BackColor="#DBEAF5" BorderWidth="0px" Font-Bold="False" ForeColor="Black"
                                                                                                    Width="150px" />
                                                                                                <ItemTemplate>
                                                                                                    &nbsp;<asp:TextBox runat="server" ID="lblEqpmntType" Text='<% #Bind("EquipmentType")%>'
                                                                                                        Width="100%"></asp:TextBox>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="Units">
                                                                                                <HeaderStyle BackColor="#DBEAF5" BorderWidth="0px" Font-Bold="False" ForeColor="Black"
                                                                                                    Width="50px" />
                                                                                                <ItemTemplate>
                                                                                                    <asp:TextBox runat="server" ID="lblUnits" Text='<% #Bind("Units")%>' Width="100%"></asp:TextBox>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="Remarks">
                                                                                                <HeaderStyle BackColor="#DBEAF5" BorderWidth="0px" Font-Bold="False" ForeColor="Black"
                                                                                                    Width="170px" />
                                                                                                <ItemTemplate>
                                                                                                    <asp:TextBox runat="server" ID="lblRemarks" Text='<% #Bind("Remarks")%>' Width="100%"></asp:TextBox>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField>
                                                                                                <ItemTemplate>
                                                                                                    <asp:LinkButton runat="server" ID="lnkRemove" Text="Remove" CausesValidation="false"
                                                                                                        CommandName="Remove" CommandArgument='<% #Bind("index")%>' Width="50px"></asp:LinkButton>
                                                                                                </ItemTemplate>
                                                                                                <ItemStyle VerticalAlign="Middle" />
                                                                                                <HeaderStyle BackColor="#DBEAF5" BorderWidth="0px" />
                                                                                            </asp:TemplateField>
                                                                                        </Columns>
                                                                                    </asp:GridView>
                                                                                </td>
                                                                            </tr>
                                                                            <tr class="value" runat="server">
                                                                                <td runat="server">
                                                                                    <asp:LinkButton ID="lnkAddEquipment" runat="server" Text="Add Equipment" OnClick="lnkAddEquipment_Click"
                                                                                        Width="90px" />
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                        <table border="0" cellpadding="0" cellspacing="0" width="100%" class="itemDetails">
                                                                            <tr>
                                                                                <th>
                                                                                    Brands carried
                                                                                </th>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="value">
                                                                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                                        <tr>
                                                                                            <td width="250" align="left">
                                                                                                Other Brands Available<br />
                                                                                                <asp:ListBox ID="lbBrandsCarried" runat="server" Height="136px" Width="250px" SelectionMode="Multiple"
                                                                                                    onDblClick="addAttribute('Wizard1_lbBrandsCarried','Wizard1_lbBrandsSelected','hdnBrands');"
                                                                                                    DataSourceID="dsBrands" DataTextField="BrandName" DataValueField="BrandId" />
                                                                                                <asp:SqlDataSource ID="dsBrands" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>'
                                                                                                        SelectCommand="s3p_EBid_QueryAllProductBrands" SelectCommandType="StoredProcedure">
                                                                                                </asp:SqlDataSource>
                                                                                            </td>
                                                                                            <td align="center" style="width: 30px" valign="middle">
                                                                                                <br />
                                                                                                <input type="button" value=">" style="width: 25px;" name="btnAddSelected" id="btnAdSelected"
                                                                                                    onclick="addAttribute('Wizard1_lbBrandsCarried','Wizard1_lbBrandsSelected','hdnBrands');" /><br />
                                                                                                <input type="button" value=">>" style="width: 25px;" name="btnSelectAll" id="btnSelectAll"
                                                                                                    onclick="addAll('Wizard1_lbBrandsCarried','Wizard1_lbBrandsSelected','hdnBrands');" /><br />
                                                                                                <input type="button" value="<<" style="width: 25px;" name="btnRemoveAll" id="btnRemoveAll"
                                                                                                    onclick="delAll('Wizard1_lbBrandsCarried','Wizard1_lbBrandsSelected','hdnBrands');" /><br />
                                                                                                <input type="button" value="<" style="width: 25px;" name="btnRemoveSelected" id="btnRemoveSelected"
                                                                                                    onclick="delAttribute('Wizard1_lbBrandsCarried','Wizard1_lbBrandsSelected','hdnBrands');" />
                                                                                            </td>
                                                                                            <td align="left" style="padding-left: 5px;">
                                                                                                Selected Brands<br />
                                                                                                <asp:ListBox ID="lbBrandsSelected" runat="server" Height="136px" Width="250px" SelectionMode="Multiple"
                                                                                                    onDblClick="delAttribute('Wizard1_lbBrandsCarried','Wizard1_lbBrandsSelected','hdnBrands');" />
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                        <table border="0" cellpadding="0" cellspacing="0" width="100%" class="itemDetails">
                                                                            <tr>
                                                                                <th>
                                                                                    Items carried
                                                                                </th>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="value">
                                                                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                                        <tr>
                                                                                            <td width="250" align="left">
                                                                                                Other Items Available<br />
                                                                                                <asp:ListBox ID="lbItemCarried" runat="server" Height="136px" Width="250px" SelectionMode="Multiple"
                                                                                                    onDblClick="addAttribute('Wizard1_lbItemCarried','Wizard1_lbItemSelected','hdnItems');" DataSourceID="dsItems"
                                                                                                    DataTextField="ItemsCarried" DataValueField="ItemNo" /><asp:SqlDataSource ID="dsItems"
                                                                                                        runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>'
                                                                                                        SelectCommand="s3p_EBid_QueryAllItemsCarried" SelectCommandType="StoredProcedure">
                                                                                                    </asp:SqlDataSource>
                                                                                            </td>
                                                                                            <td align="center" style="width: 30px" valign="middle">
                                                                                                <br />
                                                                                                <input type="button" value=">" style="width: 25px;" name="btnAddSelectedItem" id="btnAddSelectedItem"
                                                                                                    onclick="addAttribute('Wizard1_lbItemCarried','Wizard1_lbItemSelected','hdnItems');" /><br />
                                                                                                <input type="button" value=">>" style="width: 25px;" name="btnSelectAllItem" id="btnSelectAllItem"
                                                                                                    onclick="addAll('Wizard1_lbItemCarried','Wizard1_lbItemSelected','hdnItems');" /><br />
                                                                                                <input type="button" value="<<" style="width: 25px;" name="btnRemoveAllItem" id="btnRemoveAllItem"
                                                                                                    onclick="delAll('Wizard1_lbItemCarried','Wizard1_lbItemSelected','hdnItems');" /><br />
                                                                                                <input type="button" value="<" style="width: 25px;" name="btnRemoveSelectedItem"
                                                                                                    id="btnRemoveSelectedItem" onclick="delAttribute('Wizard1_lbItemCarried','Wizard1_lbItemSelected','hdnItems');" />
                                                                                            </td>
                                                                                            <td align="left" style="padding-left: 5px;">
                                                                                                Selected Items<br />
                                                                                                <asp:ListBox ID="lbItemSelected" runat="server" Height="136px" Width="250px" SelectionMode="Multiple"
                                                                                                    onDblClick="delAttribute('Wizard1_lbItemCarried','Wizard1_lbItemSelected','hdnItems');" />
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                        <table border="0" cellpadding="0" cellspacing="0" width="100%" class="itemDetails">
                                                                            <tr>
                                                                                <th>
                                                                                    Services offered
                                                                                </th>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="value">
                                                                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                                        <tr>
                                                                                            <td width="250" align="left">
                                                                                                Other Services Available<br />
                                                                                                <asp:ListBox ID="lbServices" runat="server" Height="136px" Width="250px" SelectionMode="Multiple"
                                                                                                    onDblClick="addAttribute('Wizard1_lbServices','Wizard1_lbSelectedService','hdnServices');" DataSourceID="dsServices"
                                                                                                    DataTextField="ServiceName" DataValueField="ServiceId" /><asp:SqlDataSource ID="dsServices"
                                                                                                        runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>'
                                                                                                        SelectCommand="s3p_EBid_QueryAllServices" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
                                                                                            </td>
                                                                                            <td align="center" style="width: 30px;" valign="middle">
                                                                                                <br />
                                                                                                <input type="button" value=">" style="width: 25px;" name="btnAddSelectedSvc" id="btnAddSelectedSvc"
                                                                                                    onclick="addAttribute('Wizard1_lbServices','Wizard1_lbSelectedService','hdnServices');" /><br />
                                                                                                <input type="button" value=">>" style="width: 25px;" name="btnSelectAllSvc" id="btnSelectAllSvc"
                                                                                                    onclick="addAll('Wizard1_lbServices','Wizard1_lbSelectedService','hdnServices');" /><br />
                                                                                                <input type="button" value="<<" style="width: 25px;" name="btnRemoveAllSvc" id="btnRemoveAllSvc"
                                                                                                    onclick="delAll('Wizard1_lbServices','Wizard1_lbSelectedService','hdnServices');" /><br />
                                                                                                <input type="button" value="<" style="width: 25px;" name="btnRemoveSelectedSvc" id="btnRemoveSelectedSvc"
                                                                                                    onclick="delAttribute('Wizard1_lbServices','Wizard1_lbSelectedService','hdnServices');" />
                                                                                            </td>
                                                                                            <td align="left" style="padding-left: 5px;">
                                                                                                Selected Services<br />
                                                                                                <asp:ListBox ID="lbSelectedService" runat="server" Height="136px" Width="250px" SelectionMode="Multiple"
                                                                                                    onDblClick="delAttribute('Wizard1_lbServices','Wizard1_lbSelectedService','hdnServices');" />
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                        <table border="0" cellpadding="0" cellspacing="0" width="100%" class="itemDetails">
                                                                            <tr>
                                                                                <th>
                                                                                    Location
                                                                                </th>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="value">
                                                                                    <table border="0" cellpadding="0" cellspacing="0" width="100%" id="table8">
                                                                                        <tr>
                                                                                            <td width="250" align="left">
                                                                                                Other Locations Available<br />
                                                                                                <asp:ListBox ID="lbLocations" runat="server" Height="136px" Width="250px" SelectionMode="Multiple"
                                                                                                    onDblClick="addAttribute('Wizard1_lbLocations','Wizard1_lbSelectedLocation','hdnLocations');"
                                                                                                    DataSourceID="dsLocations" DataTextField="LocationName" DataValueField="LocationId" /><asp:SqlDataSource
                                                                                                        ID="dsLocations" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>'
                                                                                                        SelectCommand="s3p_EBid_QueryAllLocations" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
                                                                                            </td>
                                                                                            <td align="center" style="width: 30px" valign="middle">
                                                                                                <br />
                                                                                                <input type="button" value=">" style="width: 25px;" name="btnAddSelectedLoc" id="btnAddSelectedLoc"
                                                                                                    onclick="addAttribute('Wizard1_lbLocations','Wizard1_lbSelectedLocation','hdnLocations');"><br />
                                                                                                <input type="button" value=">>" style="width: 25px;" name="btnSelectAllLoc" id="btnSelectAllLoc"
                                                                                                    onclick="addAll('Wizard1_lbLocations','Wizard1_lbSelectedLocation','hdnLocations');"><br />
                                                                                                <input type="button" value="<<" style="width: 25px;" name="btnRemoveAllLoc" id="btnRemoveAllLoc"
                                                                                                    onclick="delAll('Wizard1_lbLocations','Wizard1_lbSelectedLocation','hdnLocations');"><br />
                                                                                                <input type="button" value="<" style="width: 25px;" name="btnRemoveSelectedLoc" id="btnRemoveSelectedLoc"
                                                                                                    onclick="delAttribute('Wizard1_lbLocations','Wizard1_lbSelectedLocation','hdnLocations');">
                                                                                            </td>
                                                                                            <td align="left" style="padding-left: 5px;">
                                                                                                Selected Locations<br />
                                                                                                <asp:ListBox ID="lbSelectedLocation" runat="server" Height="136px" Width="250px"
                                                                                                    SelectionMode="Multiple" onDblClick="delAttribute('Wizard1_lbLocations','Wizard1_lbSelectedLocation','hdnLocations');" />
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </asp:WizardStep>
                                                                </WizardSteps>
                                                                <StartNavigationTemplate>
                                                                    <br />
                                                                    <table cellpadding="0" cellspacing="0" border="0" id="actions" width="100%">
                                                                        <tr>
                                                                            <td>
                                                                                <asp:LinkButton ID="LinkButton1" runat="server" ForeColor="white" CommandName="Next"
                                                                                    OnCommand="WizardNavigationClick">Next</asp:LinkButton>
                                                                                <asp:LinkButton ID="LinkButton2" runat="server" ForeColor="white" CommandName="Cancel" CausesValidation="false"
                                                                                    OnCommand="WizardNavigationClick" OnClientClick="return confirm('Are you sure you want to cancel?');">Cancel</asp:LinkButton>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </StartNavigationTemplate>
                                                                <StepNavigationTemplate>
                                                                    <br />
                                                                    <table cellpadding="0" cellspacing="0" border="0" id="actions" width="100%">
                                                                        <tr>
                                                                            <td>
                                                                                <asp:LinkButton ID="LinkButton3" runat="server" ForeColor="white" CommandName="Previous"
                                                                                    OnCommand="WizardNavigationClick" CausesValidation="false">Previous</asp:LinkButton>
                                                                                <asp:LinkButton ID="LinkButton1" runat="server" ForeColor="white" CommandName="Next"
                                                                                    OnCommand="WizardNavigationClick">Next</asp:LinkButton>
                                                                                <asp:LinkButton ID="LinkButton2" runat="server" ForeColor="white" CommandName="Cancel" CausesValidation="false"
                                                                                    OnCommand="WizardNavigationClick" OnClientClick="return confirm('Are you sure you want to cancel?');">Cancel</asp:LinkButton>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </StepNavigationTemplate>
                                                                <FinishNavigationTemplate>
                                                                    <br />
                                                                    <table cellpadding="0" cellspacing="0" border="0" id="actions" width="100%">
                                                                        <tr>
                                                                            <td>
                                                                                <asp:LinkButton ID="LinkButton3" runat="server" ForeColor="white" CommandName="Previous"
                                                                                    OnCommand="WizardNavigationClick" CausesValidation="false">Previous</asp:LinkButton>
                                                                                <asp:LinkButton ID="lnkFinish" runat="server" ForeColor="white" CommandName="Finish"
                                                                                    OnCommand="WizardNavigationClick" OnClientClick="return confirm('Are you sure you want to proceed?');"><%= AddOrEdit %></asp:LinkButton>
                                                                                <asp:LinkButton ID="LinkButton2" runat="server" ForeColor="white" CommandName="Cancel" CausesValidation="false"
                                                                                    OnCommand="WizardNavigationClick" OnClientClick="return confirm('Are you sure you want to cancel?');">Cancel</asp:LinkButton>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </FinishNavigationTemplate>
                                                            </asp:Wizard>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 20px;">
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td id="footer" height="50px">
                        <EBid:Footer runat="server" ID="Footer1" />
                    </td>
                </tr>
            </table>
        </form>
    </div>
</body>
</html>
