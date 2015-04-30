<%@ Page Language="c#" Inherits="EBid.WEB.buyer_screens.supplierSelect" CodeFile="supplierSelect.aspx.cs" %>

<%@ Register TagPrefix="EBid" TagName="LeftNav" Src="~/WEB/usercontrol/Buyer/LeftNav.ascx" %>
<%@ Register TagPrefix="EBid" TagName="TopNavBids" Src="~/WEB/usercontrol/Buyer/TopNavBids.ascx" %>
<%@ Register TagPrefix="EBid" TagName="TopNav2" Src="~/WEB/usercontrol/Buyer/TopNav2.ascx" %>
<%@ Register TagPrefix="EBid" TagName="GlobalLinksNav" Src="~/web/usercontrol/GlobalLinksNav.ascx" %>
<%@ Register TagPrefix="EBid" TagName="Footer" Src="~/web/usercontrol/Footer.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml" style="height: 100%;">
<head>
<%@ Register TagPrefix="EBid" TagName="jqueryLoader" Src="../usercontrol/jQuery_loader.ascx" %>
    <EBid:jqueryLoader runat="server" ID="jqueryLoader1" />
    <title></title>
    <meta http-equiv="Content-Language" content="en-us" />
    <meta http-equiv="Content-Type" content="text/html; charset=windows-1252" />
    <link rel="stylesheet" type="text/css" href='<%= "../themes/"+Session["configTheme"]+"/css/style.css" %>' />
    <link rel="stylesheet" type="text/css" href='<%="../themes/"+Session["configTheme"]+"/css/style_buyer.css" %>' />

    <script type="text/javascript" src="../include/selectSupplierJSFunctions.js"></script>

</head>
<body onload="createListObjects(document.frmSelectSuppliers.lstSupplierA, document.frmSelectSuppliers.lstSupplierB);">
    <div align="left">
        <form id="frmSelectSuppliers" runat="server">
            <table border="0" cellpadding="0" cellspacing="0" width="100%" id="page">
                <tr height="137px">
                    <td valign="top">
                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td>
                                    <div align="left" id="masthead">
                                        <EBid:GlobalLinksNav runat="server" ID="GlobalLinksNav" />
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <EBid:TopNav2 runat="server" ID="TopNav2" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table border="0" cellpadding="0" cellspacing="0" width="100%" height="100%">
                            <tr>
                                <td id="relatedInfo">
                                    <div align="left">
                                        <EBid:LeftNav runat="server" ID="LeftNav" />
                                    </div>
                                    <p>&nbsp;
                                        </p>
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
                                                                Select Suppliers</h1>
                                                            <table border="0" cellpadding="0" cellspacing="0" width="60%">
                                                                <tr>
                                                                    <td>
                                                                        <table border="0" cellpadding="0" cellspacing="0" width="100%" id="pageDetails">
                                                                            <tr>
                                                                                <th colspan="3">
                                                                                    Supplier Category :&nbsp;
                                                                                    <asp:Label runat="server" ID="lblSupplierCategory"></asp:Label></th>
                                                                            </tr>
                                                                            <tr>
                                                                                <td width="167">&nbsp;
                                                                                    </td>
                                                                                <td width="82">&nbsp;
                                                                                    </td>
                                                                                <td>&nbsp;
                                                                                    </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td width="167" align="right">
                                                                                    <asp:ListBox Style="height: 200; width: 200" runat="server" ID="lstSupplierA" onDblClick="addAttribute();">
                                                                                    </asp:ListBox>
                                                                                </td>
                                                                                <td width="82" align="right">
                                                                                    <p align="center" style="text-align: center">
                                                                                        <input type="button" runat="server" id="btnSelectAll" value="&gt;&gt;" onclick="addAll();" style="width: 20px" /><br />
                                                                                        <input type="button" runat="server" id="btnSelectOne" value="&gt;" onclick="addAttribute();" style="width: 20px" /><br />
                                                                                        <input type="button" runat="server" id="btnDeselectOne" value="&lt;" onclick="delAttribute();" style="width: 20px" /><br />
                                                                                        <input type="button" runat="server" id="btnDeselectAll" value="&lt;&lt;" onclick="delAll();" style="width: 20px" />
                                                                                    </p>
                                                                                </td>
                                                                                <td align="right">
                                                                                    <p align="center">
                                                                                        <asp:ListBox Style="height: 200; width: 200" runat="server" ID="lstSupplierB" onDblClick="delAttribute();">
                                                                                        </asp:ListBox>
                                                                                    </p>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td width="167">&nbsp;
                                                                                    </td>
                                                                                <td width="82">&nbsp;
                                                                                    </td>
                                                                                <td>&nbsp;
                                                                                    </td>
                                                                            </tr>
                                                                        </table>
                                                                        <p>
                                                                            <a runat="server" id="btnClick" style="cursor: hand; font-size: 8pt; text-decoration: underline"
                                                                                onmouseover="UnderLine(this, 'over')" onmouseout="UnderLine(this, 'out')">Click
                                                                                here if you would like to include a one time supplier.</a></p>
                                                                        <table border="0" cellpadding="0" cellspacing="0" width="100%" id="actions">
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:LinkButton runat="server" ID="btnOk" OnClick="btnOk_Click">Ok</asp:LinkButton></td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
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
                        <input type="hidden" runat="server" id="hdnSuppliers" name="hdnSuppliers" />
                        <input type="hidden" runat="server" id="hdnBidRefNo" name="hdnBidRefNo" />
                        <input type="hidden" runat="server" id="hdnCategoryId" name="hdnCategoryId" />
                        <input type="hidden" runat="server" id="hdnOTS" name="hdnOTS" />
                        <input type="hidden" runat="server" id="hdnVendorsForACategoryId" name="hdnVendorsForACategoryId" />
                    </td>
                </tr>
            </table>
        </form>
    </div>
</body>
</html>
