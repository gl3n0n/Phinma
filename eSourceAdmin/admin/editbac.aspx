<%@ Page Language="C#" AutoEventWireup="true" CodeFile="editbac.aspx.cs" Inherits="admin_editbac" %>
<%@ Register TagPrefix="EBid" TagName="TopDate" Src="~/usercontrol/TopDate.ascx" %>
<%@ Register TagPrefix="EBid" TagName="AdminTopNav" Src="~/usercontrol/admin/adminTopNav.ascx" %>
<%@ Register TagPrefix="EBid" TagName="Admin_TopNav_User" Src="~/usercontrol/admin/Admin_TopNav_User.ascx" %>
<%@ Register TagPrefix="EBid" TagName="AdminLeftNavUser" Src="~/usercontrol/admin/AdminLeftNavUser.ascx" %>
<%@ Register TagPrefix="EBid" TagName="AdminLeftNav" Src="~/usercontrol/admin/AdminLeftNav.ascx" %>
<%@ Register TagPrefix="EBid" TagName="Footer" Src="~/usercontrol/Footer.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>.:| Trans-Asia | Edit User Profile |:.</title>
    <meta http-equiv="Content-Language" content="en-us">
    <meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
    <link href="../css/style_ua.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../include/util.js"></script>
</head>
<body onload="SetStatus();">
    <div align="left">
        <form id="Form1" runat="server">
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
                        <table border="0" cellpadding="0" cellspacing="0" width="100%" style="height: 100%;">
                            <tr>
                                <td id="relatedInfo">
                                    <h2>
                                        User Functions</h2>
                                    <EBid:AdminLeftNavUser runat="server" ID="AdminLeftNavUser" />
                                    <p>
                                        &nbsp;</p>
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
                                                                Edit BAC Information</h1>
                                                            <table align="center" border="0" cellpadding="0" cellspacing="0" width="300px" id="itemDetails">
                                                                <tr>
                                                                    <th colspan="2">
                                                                        User information</th>
                                                                </tr>
                                                                <tr class="value">
                                                                    <td style="width: 19%">
                                                                        User Name:</td>
                                                                    <td>
                                                                        <asp:TextBox ID="tbUserName" runat="server" Width="35%" MaxLength="50" ReadOnly="True" />                                                                        </td>
                                                                </tr>
                                                                <tr class="value">
                                                                    <td style="width: 19%; height: 30px;">
                                                                        Email Address:</td>
                                                                    <td style="height: 30px">
                                                                        <asp:TextBox ID="tbEmail" runat="server" Width="35%" MaxLength="100" />
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="tbEmail"
                                                                            EnableClientScript="False" ErrorMessage="* Required"></asp:RequiredFieldValidator>
                                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="tbEmail"
                                                                            EnableClientScript="False" ErrorMessage="* Invalid Email Address" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator></td>
                                                                </tr>
                                                            </table>
                                                            <table align="center" border="0" cellpadding="0" cellspacing="0" width="300px" id="itemDetails1">
                                                                <tr class="value">
                                                                    <td style="width: 19%; height: 30px;">
                                                                        First Name:</td>
                                                                    <td style="height: 30px">
                                                                        <asp:TextBox ID="tbFirstName" runat="server" Width="35%" MaxLength="70" />
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" EnableClientScript="False"
                                                                            ErrorMessage="* Required" ControlToValidate="tbFirstName" Display="Dynamic"></asp:RequiredFieldValidator></td>
                                                                </tr>
                                                                <tr class="evenCells">
                                                                    <td style="width: 19%; height: 30px;">
                                                                        Middle Name:</td>
                                                                    <td style="height: 30px">
                                                                        <asp:TextBox ID="tbMidInitial" runat="server" Width="35%" MaxLength="50" />
                                                                    </td>
                                                                </tr>
                                                                <tr class="value">
                                                                    <td style="width: 19%; height: 30px;">
                                                                        Last Name:</td>
                                                                    <td style="height: 30px">
                                                                        <asp:TextBox ID="tbLastName" runat="server" Width="35%" MaxLength="70" />
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" EnableClientScript="False"
                                                                            ErrorMessage="* Required" ControlToValidate="tbLastName"></asp:RequiredFieldValidator></td>
                                                                </tr>
                                                                <tr class="evenCells">
                                                                    <td style="width: 19%; height: 30px;">
                                                                        Approving 
                                                                        Limit on Lowest Price Bidder</td>
                                                                    <td style="height: 30px">
                                                                        <asp:TextBox ID="ApprovingLimitOnLowestPrice" runat="server" Width="17%" MaxLength="70" />
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" EnableClientScript="False"
                                                                            ErrorMessage="* Required" ControlToValidate="ApprovingLimitOnLowestPrice"></asp:RequiredFieldValidator>
                                                                        <asp:TextBox ID="ApprovingLimitOnLowestPriceTo" runat="server" Width="17%" MaxLength="70" />
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" EnableClientScript="False"
                                                                            ErrorMessage="* Required" ControlToValidate="ApprovingLimitOnLowestPriceTo"></asp:RequiredFieldValidator></td>
                                                                </tr>
                                                                <tr class="value">
                                                                    <td style="width: 19%; height: 30px;">
                                                                        Approving 
                                                                        Limit on Other Than Lowest Price Bidder</td>
                                                                    <td style="height: 30px">
                                                                        <asp:TextBox ID="ApprovingLimitOnNonLowestPrice" runat="server" Width="17%" MaxLength="70" />
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" EnableClientScript="False"
                                                                            ErrorMessage="* Required" ControlToValidate="ApprovingLimitOnNonLowestPrice"></asp:RequiredFieldValidator>
                                                                        <asp:TextBox ID="ApprovingLimitOnNonLowestPriceTo" runat="server" Width="17%" MaxLength="70" />
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" EnableClientScript="False"
                                                                            ErrorMessage="* Required" ControlToValidate="ApprovingLimitOnNonLowestPriceTo"></asp:RequiredFieldValidator></td>
                                                                </tr>
	                                                        <tr class="value">
	                                                                <td style="width: 120px; padding-top: 7px">
	                                                                    Chairman:</td>
	                                                                <td>
	                                                                    <asp:RadioButtonList ID="Committee" runat="server" RepeatDirection="Horizontal">
	                                                                        <asp:ListItem Value="0">No</asp:ListItem>
	                                                                        <asp:ListItem Value="1">Yes</asp:ListItem>
	                                                                    </asp:RadioButtonList>
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" EnableClientScript="False"
                                                                            ErrorMessage="* Required" ControlToValidate="Committee"></asp:RequiredFieldValidator></td>
	                                                                </td>
                                                                </tr>


	                                                        <tr class="value">
	                                                                <td style="width: 120px; padding-top: 7px">
	                                                                    Approver:</td>
	                                                                <td>
	                                                                    <asp:RadioButtonList ID="Approver" runat="server" RepeatDirection="Horizontal">
	                                                                        <asp:ListItem Value="0">No</asp:ListItem>
	                                                                        <asp:ListItem Value="1">Yes</asp:ListItem>
	                                                                    </asp:RadioButtonList>
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" EnableClientScript="False"
                                                                            ErrorMessage="* Required" ControlToValidate="Approver"></asp:RequiredFieldValidator></td>
	                                                                </td>
                                                                </tr>



                                                                <tr class="evenCells">
                                                                    <td style="width: 120px;padding-top: 7px" >
                                                                       OIC</td>
                                                                    <td>
								<script type="text/javascript">
                                                                        function alertSelected() {
                                                                         selObj = document.getElementById('OIC').selectedIndex;
                                                                         if (confirm("are you sure to select this OIC?")) {
                                                                         } else {
                                                                         initOIC_c = document.getElementById('initOIC').value;
                                                                         var ddl = document.getElementById('OIC');
                                                                         for (var i = 0; i < ddl.options.length; i++) {
                                                                             if (ddl.options[i].value == initOIC_c) {
                                                                                 if (ddl.selectedIndex != i) {
                                                                                     ddl.selectedIndex = i;
                                                                                     if (change)
                                                                                         ddl.onchange();
                                                                                 }
                                                                                 break;
                                                                             }
                                                                         }
                                                                         }
                                                                        }
                                                                    </script>
								<asp:HiddenField ID="initOIC" runat="server" />                                                            
                                                                <asp:DropDownList ID="OIC" runat="server" DataSourceID="dsOIC" DataTextField="Name"
                                                                DataValueField="BACId" Width="150px" onchange="alertSelected()">
                                                            </asp:DropDownList>&nbsp;
								<asp:SqlDataSource ID="dsOIC" runat="server" 
								ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>'
								SelectCommandType="Text"  SelectCommand="SELECT t1.Lastname + ', ' + t1.Firstname + ' ' + t1.Middlename + ' (' + CAST(t1.BACId AS VARCHAR) + ')' as Name, BACId FROM tblBidAwardingCommittee t1, tblUsers t2 WHERE t1.BACId= t2.UserId AND t2.Status = 1 AND t1.BACId <> @BacUserId UNION Select NULL AS Name, 0 AS BACId ORDER BY Name" >
																									                        <SelectParameters>
                                                                <asp:QueryStringParameter Name="BacUserId" QueryStringField="UserId" Type="Int32" />
								</SelectParameters>
								</asp:SqlDataSource>
                                                                </tr>
                                                            </table>
                                                            <br />
                                                            <table border="0" cellpadding="0" cellspacing="0" width="100%" id="actions">
                                                                <tr>
                                                                    <td>
                                                                        <asp:LinkButton ID="lnkSave" runat="server" Text="Save" OnClientClick="return confirm('Overwrite Previous Data?');" OnClick="lnkSave_Click" ></asp:LinkButton>
                                                                        <a href="users.aspx">Cancel</a>
                                                                    </td>
                                                                </tr>
                                                            </table>
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