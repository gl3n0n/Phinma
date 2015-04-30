<%@ Page Language="C#" AutoEventWireup="true" CodeFile="adduser.aspx.cs" Inherits="admin_addUser"
    MaintainScrollPositionOnPostback="true" %>

<%@ Register TagPrefix="EBid" TagName="TopDate" Src="~/usercontrol/TopDate.ascx" %>
<%@ Register TagPrefix="EBid" TagName="AdminTopNav" Src="~/usercontrol/admin/adminTopNav.ascx" %>
<%@ Register TagPrefix="EBid" TagName="Admin_TopNav_User" Src="~/usercontrol/admin/Admin_TopNav_User.ascx" %>
<%@ Register TagPrefix="EBid" TagName="AdminLeftNavUser" Src="~/usercontrol/admin/AdminLeftNavUser.ascx" %>
<%@ Register TagPrefix="EBid" TagName="AdminLeftNav" Src="~/usercontrol/admin/AdminLeftNav.ascx" %>
<%@ Register TagPrefix="EBid" TagName="Footer" Src="~/usercontrol/Footer.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>.:| Trans-Asia | Add User |:.</title>
    <meta http-equiv="Content-Language" content="en-us" />
    <meta http-equiv="Content-Type" content="text/html; charset=windows-1252" />
    <script type="text/javascript" src="../include/util.js"></script>
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="../css/style_ua.css" />     
    <link href="../../css/style.css" rel="stylesheet" type="text/css" />
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
                                                                Add New Buyer</h1>
                                                        </td>
                                                    </tr>
                                                    <tr align="right">
                                                        <td valign="top" height="30px">                                                            
                                                            <asp:DropDownList ID="ddlUserTypes" runat="server" DataSourceID="dsUserTypes" DataTextField="UserType"
                                                                DataValueField="UserTypeId" Width="150px" AutoPostBack="True" OnSelectedIndexChanged="ddlUserTypes_SelectedIndexChanged">
                                                            </asp:DropDownList>&nbsp;
                                                            <asp:SqlDataSource ID="dsUserTypes" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>'
                                                                SelectCommand="s3p_EBid_UserTypes" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <table align="center" cellpadding="0" cellspacing="0" width="100%" id="itemDetails">
                                                                <tr>
                                                                    <th colspan="2">
                                                                        &nbsp;User information</th>
                                                                </tr>
                                                                <tr class="value">
                                                                    <td style="width: 120px;padding-top: 7px" >
                                                                        Username:</td>
                                                                    <td align="left">
                                                                        <asp:TextBox ID="tbUserName" runat="server" Width="250px" />
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="* Username is required"
                                                                            ControlToValidate="tbUserName" Display="Dynamic" SetFocusOnError="True">* Required</asp:RequiredFieldValidator>
                                                                        <asp:Label runat="server" ID="lblUserError" Visible="false" ForeColor="red" />
                                                                    </td>
                                                                </tr>
                                                                <%--<tr class="evenCells">
                                                                    <td style="width: 120px;padding-top: 7px" >
                                                                        Password:</td>
                                                                    <td>
                                                                        <asp:TextBox ID="tbPassword" runat="server" Width="250px" MaxLength="25" TextMode="Password" >*</asp:TextBox>&nbsp;                                                                        
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                                                            ErrorMessage="* Password is required" ControlToValidate="tbPassword" Display="Dynamic">* Required</asp:RequiredFieldValidator>
                                                                        <asp:Label runat="server" ID="lblPassError" Visible="false" ForeColor="red" />
                                                                        <asp:Label ID="lblCheckPassStrength" runat="server" ForeColor="Red" Visible="False"></asp:Label>
                                                                    </td>
                                                                </tr>--%>
                                                                <tr class="evenCells">
                                                                    <td style="width: 120px;padding-top: 7px" >
                                                                        Email Address:</td>
                                                                    <td>
                                                                        <asp:TextBox ID="tbEmail" runat="server" Width="250px" />
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                                                                            ErrorMessage="* Emaill Address is required" ControlToValidate="tbEmail" Display="Dynamic" SetFocusOnError="True">* Required</asp:RequiredFieldValidator>
                                                                        <asp:RegularExpressionValidator ID="valEmail" runat="server" ControlToValidate="tbEmail"
                                                                            EnableClientScript="False" ErrorMessage="Invalid Email Address" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="height: 244px">
                                                            <table align="center" border="0" cellpadding="0" cellspacing="0" width="100%" id="itemDetails1">
                                                                <tr>
                                                                    <th colspan="2">
                                                                        &nbsp;Buyer Details</th>
                                                                </tr>
                                                                <tr class="value">
                                                                    <td style="width: 120px;padding-top: 7px" >
                                                                        Buyer Code:</td>
                                                                    <td>
                                                                        <asp:TextBox ID="tbBuyerCode" runat="server" Width="250px" />
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server"
                                                                            ErrorMessage="* Buyer Code is required" ControlToValidate="tbBuyerCode" Display="Dynamic" SetFocusOnError="True">* Required</asp:RequiredFieldValidator>
                                                                        <asp:Label runat="server" ID="lblBCError" Visible="false" ForeColor="red"></asp:Label></td>
                                                                </tr>
                                                                <tr class="evenCells">
                                                                    <td style="width: 120px;padding-top: 7px" >
                                                                        First Name:</td>
                                                                    <td style="height: 30px">
                                                                        <asp:TextBox ID="tbFirstName" runat="server" Width="250px" />
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server"
                                                                            ErrorMessage="* Firstname is required" ControlToValidate="tbFirstName" SetFocusOnError="True">* Required</asp:RequiredFieldValidator></td>
                                                                </tr>
                                                                <tr class="value">
                                                                    <td style="width: 120px;padding-top: 7px" >
                                                                        Middle Name:</td>
                                                                    <td style="height: 30px">
                                                                        <asp:TextBox ID="tbMidInitial" runat="server" Width="250px" />
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server"
                                                                            ErrorMessage="* Middlename is required" ControlToValidate="tbMidInitial" SetFocusOnError="True">* Required</asp:RequiredFieldValidator></td>
                                                                </tr>
                                                                <tr class="evenCells">
                                                                    <td style="width: 120px;padding-top: 7px" >
                                                                        Last Name:</td>
                                                                    <td>
                                                                        <asp:TextBox ID="tbLastName" runat="server" Width="250px" />
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server"
                                                                            ErrorMessage="* Lastname is required" ControlToValidate="tbLastName" SetFocusOnError="True">* Required</asp:RequiredFieldValidator></td>
                                                                </tr>                                                                
                                                                <tr class="value">
                                                                    <td style="width: 120px;padding-top: 7px" >
                                                                        Company:</td>
                                                                    <td>
                                                                        <asp:RadioButtonList runat="server" ID="rbCompany" DataSourceID="dsCompanies" DataTextField="Company"
                                                                            DataValueField="CompanyId" RepeatDirection="Horizontal">
                                                                        </asp:RadioButtonList>
                                                                        <asp:SqlDataSource ID="dsCompanies" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>'
                                                                            SelectCommand="s3p_EBid_QueryCompanies" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
                                                                    </td>
                                                                </tr>
                                                                <!--<tr class="evenCells">
                                                                    <td style="width: 120px;padding-top: 7px" >
                                                                        Immediate Supervisor:</td>
                                                                    <td>
                                                                        <asp:DropDownList ID="ddlImmediateSupervisor" runat="server" DataSourceID="dsPurchasingHeads"
                                                                            DataTextField="Name" DataValueField="PurchasingId" Width="250px">
                                                                        </asp:DropDownList>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="* Immediate Supervisor is required." ControlToValidate="ddlImmediateSupervisor" Display="Dynamic" SetFocusOnError="True">* Required</asp:RequiredFieldValidator>
                                                                        <asp:SqlDataSource ID="dsPurchasingHeads" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>'
                                                                            SelectCommand="sp_GetAllPurchasingHeads" SelectCommandType="StoredProcedure">                                                                            
                                                                        </asp:SqlDataSource>
                                                                        &nbsp;
                                                                    </td>
                                                                </tr>-->
                                                            </table>
                                                            <br />
                                                            <div style="text-align: justify; width: 100%; padding-left: 10px; align: center;">
                                                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" Font-Size="11px" HeaderText="&lt;b&gt;Please review the following:&lt;/b&gt;"
                                                                    DisplayMode="List" Font-Names="Arial"></asp:ValidationSummary>
                                                            </div>
                                                            <br />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>                                                            
                                                            <table border="0" cellpadding="0" cellspacing="0" width="100%" id="actions">
                                                                <tr>
                                                                    <td>
                                                                        <asp:LinkButton ID="lnkSave" runat="server" Text="Save" OnClientClick="return confirm('Add New User?');" OnClick="lnkSave_Click"></asp:LinkButton>
                                                                        <a href="adduser.aspx">Cancel</a>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr><td style="height: 20px;"></td></tr>
                                                </table>
                                            </td>
                                        </tr>                                        
                                    </table>
                                </td>
                            </tr>                            
                            <tr>
                                <td>
                                </td>
                                <td>
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
