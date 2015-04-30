<%@ Page Language="C#" AutoEventWireup="true" CodeFile="regproduct.aspx.cs" Inherits="web_buyer_screens_RegProduct" MaintainScrollPositionOnPostback="true" %>

<%@ Register Src="../usercontrol/admin/AdminLeftNavList.ascx" TagName="AdminLeftNavList" TagPrefix="uc1" %>
<%@ Register Src="../usercontrol/admin/Admin_TopNav_List.ascx" TagName="Admin_TopNav_List" TagPrefix="uc2" %>
<%@ Register TagPrefix="EBid" TagName="TopDate" Src="~/usercontrol/TopDate.ascx" %>
<%@ Register TagPrefix="EBid" TagName="AdminTopNav" Src="~/usercontrol/admin/adminTopNav.ascx" %>
<%@ Register TagPrefix="EBid" TagName="Footer" Src="~/usercontrol/Footer.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>.:| Trans-Asia | Register Product |:.</title>
    <meta http-equiv="Content-Language" content="en-us" />
    <meta http-equiv="Content-Type" content="text/html; charset=windows-1252" />
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="../css/style_ua.css" />
   <script type="text/javascript" src="../include/util.js"></script> 
    <style type="text/css">
        .txtbox
        {
            font-family:Arial;
            font-size:11px;
        }
    </style>
    <script language="javascript" type="text/javascript" src="../include/customValidation.js"></script>

</head>
<body>
    <div align="left" height="100%">
        <form runat="server">
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
                                    <uc2:Admin_TopNav_List ID="Admin_TopNav_List1" runat="server"></uc2:Admin_TopNav_List>
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
                    <td height="100%">
                        <table border="0" cellpadding="0" cellspacing="0" width="100%" height="100%">
                            <tr>
                                <td id="relatedInfo">
                                    <h2>
                                        Products</h2>
                                    <div align="left">
                                        <uc1:AdminLeftNavList ID="AdminLeftNavList1" runat="server" />
                                    </div>
                                    <br />
                                </td>
                                <td id="content">
                                    <br />
                                    <h1>
                                        Register Product</h1>
                                    <p><asp:Label ID="lblMessage" runat="server" CssClass="messagelabels"></asp:Label></p>
                                    <div align="left">
                                        <table border="0" cellpadding="0" cellspacing="0" width="100%" id="itemDetails">
                                            <tr>
                                                <th colspan="2">
                                                    &nbsp;Details</th>
                                            </tr>
                                            <tr>
                                                <td style="width: 130px">
                                                    SKU:</td>
                                                <td class="value">
                                                    <asp:TextBox runat="server" ID="txtSKU"></asp:TextBox>
                                                    <asp:Label ID="lblSKUAlreadyExists" runat="server" CssClass="messagelabels" Text=""></asp:Label>
                                                    <asp:RequiredFieldValidator ID="rfvSKU" runat="server" ControlToValidate="txtSKU" Display="None" ErrorMessage="SKU is a required field." SetFocusOnError="True"></asp:RequiredFieldValidator></td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Item Name :</td>
                                                <td class="value">
                                                    <asp:TextBox runat="server" ID="txtItemName"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvItemName" runat="server" ControlToValidate="txtItemName" Display="None" ErrorMessage="Item Name is a required field." SetFocusOnError="True"></asp:RequiredFieldValidator></td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Product Description:</td>
                                                <td class="value">
                                                    <asp:TextBox runat="server" ID="txtProductDescription" TextMode="MultiLine" Rows="4" Width="250px" CssClass="txtbox"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvProductDescription" runat="server" ControlToValidate="txtProductDescription" Display="None" ErrorMessage="Product Description is a required field." SetFocusOnError="True"></asp:RequiredFieldValidator></td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Unit Of Measure :</td>
                                                <td class="value">
                                                    <asp:DropDownList runat="server" ID="ddlUnitOfMeasure">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvUnitOfMeasure" runat="server" ControlToValidate="ddlUnitOfMeasure" Display="None" ErrorMessage="Unit Of Measure is a required field." SetFocusOnError="True"></asp:RequiredFieldValidator></td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Category</td>
                                                <td class="value">
                                                    <table cellpadding="0" cellspacing="0" border="0" id="clearTable">
                                                        <tr>
                                                            <td>
                                                                <asp:DropDownList runat="server" ID="ddlCategory" AutoPostBack="true" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged" Width="250px">
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="rfvCategory" runat="server" ControlToValidate="ddlCategory" Display="None" ErrorMessage="Category is a required field." SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                            </td>
                                                            <td>
                                                                &nbsp;</td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Sub-Category:</td>
                                                <td class="value" style="height: 47px">
                                                    <table cellpadding="0" cellspacing="0" border="0" id="clearTable">
                                                        <tr>
                                                            <td>
                                                                <asp:DropDownList runat="server" ID="ddlSubCategory" Width="250px">
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="rfvSubCategory" runat="server" ControlToValidate="ddlSubCategory" Display="None" ErrorMessage="Sub-Category is a required field." SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                            </td>
                                                            <td>
                                                                &nbsp;</td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Brand :</td>
                                                <td class="value">
                                                    <asp:DropDownList runat="server" ID="ddlBrand">
                                                    </asp:DropDownList>
                                                    </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Service type :
                                                </td>
                                                <td class="value">
                                                    <asp:DropDownList runat="server" ID="ddlServiceType">
                                                    </asp:DropDownList>
                                                    </td>
                                            </tr>
                                        </table>
                                        <br />
                                    </div>
                                    <div align="left">
                                        <table border="0" cellpadding="0" cellspacing="0" width="100%" id="actions">
                                            <tr>
                                                <td align="right" style="height: 34px">
                                                    <asp:LinkButton runat="server" ID="btnSave" Width="100px" OnClick="btnSave_Click">Save</asp:LinkButton>
                                                    <asp:LinkButton runat="server" ID="btnDelete" Width="100px" Visible="false" OnClick="btnDelete_Click" OnClientClick="return confirm('Are you sure you want to delete this product?');">Delete</asp:LinkButton>
                                                    <asp:LinkButton runat="server" ID="btnClear" Width="100px" CausesValidation="False" OnClick="btnClear_Click">Clear</asp:LinkButton>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td id="footer">
                        <EBid:Footer runat="server" ID="Footer" />
                        <asp:CustomValidator ID="cuvValidate" runat="server" ClientValidationFunction="ValidatorIndividualAlert(this, args);" Display="None"></asp:CustomValidator>
                    </td>
                </tr>
            </table>
        </form>
    </div>
</body>
</html>
