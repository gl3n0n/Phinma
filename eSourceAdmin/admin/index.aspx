<%@ Page Language="C#" AutoEventWireup="true" CodeFile="index.aspx.cs" Inherits="web_useradministration_index" %>

<%@ Register TagPrefix="EBid" TagName="TopDate" Src="~/usercontrol/TopDate.ascx" %>
<%@ Register TagPrefix="EBid" TagName="AdminTopNav" Src="~/usercontrol/admin/adminTopNav.ascx" %>
<%@ Register TagPrefix="EBid" TagName="Admin_TopNav_Home" Src="~/usercontrol/admin/Admin_TopNav_Home.ascx" %>
<%@ Register TagPrefix="EBid" TagName="AdminLeftNavUser" Src="~/usercontrol/admin/AdminLeftNavUser.ascx" %>
<%@ Register TagPrefix="EBid" TagName="AdminLeftNav" Src="~/usercontrol/admin/AdminLeftNav.ascx" %>
<%@ Register TagPrefix="EBid" TagName="Footer" Src="~/usercontrol/Footer.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
	<title>.:| Trans-Asia | Admin Home |:.</title>
    <meta http-equiv="Content-Language" content="en-us">
    <meta http-equiv="Content-Type" content="text/html; charset=windows-1252">    
    <link href="../css/style.css" rel="stylesheet" type="text/css" />    
    <link rel="stylesheet" type="text/css" href="../css/style_ua.css" />
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
                                    <EBid:Admin_TopNav_Home runat="server" ID="Admin_TopNav_Home" />
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
                                        Admin Functions</h2>
                                    <EBid:AdminLeftNav runat="server" ID="AdminLeftNav" />
                                </td>
                                <td id="content">
                                    <div align="left">
                                        <h1>
                                            <br />
                                            Welcome <%= Session["USERNAME"].ToString()%>!</h1>                                        
                                        <h3>
                                            Tasks</h3>
                                        <p>
                                            <asp:HyperLink ID="lnkAddNewBuyer" runat="server" NavigateUrl="~/admin/adduser.aspx">Add New Buyer</asp:HyperLink><br />
                                            <asp:HyperLink ID="lnkAddNewSupplier" runat="server" NavigateUrl="~/admin/aevendor.aspx">Add New Vendor</asp:HyperLink><br />
                                            <!--<asp:HyperLink ID="lnkAddNewPurchasingOfficer" runat="server" NavigateUrl="~/admin/addnewpurchasing.aspx">Add New Purchasing Officer</asp:HyperLink><br />-->
                                           
                                           
                                            <asp:HyperLink ID="lnkAddAdmin" runat="server" NavigateUrl="~/admin/addadmin.aspx">Add New Administrator</asp:HyperLink><br />
                                            <br />
                                            <asp:HyperLink ID="lnkEditBuyer" runat="server" NavigateUrl="~/admin/users.aspx?t=0">Edit / Delete Buyer</asp:HyperLink><br />
                                            <asp:HyperLink ID="lnkEditSupplier" runat="server" NavigateUrl="~/admin/users.aspx?t=1">Edit / Delete Vendor</asp:HyperLink><br />
                                            <!--<asp:HyperLink ID="lnkEditPurchasingOfficer" runat="server" NavigateUrl="~/admin/users.aspx?t=2">Edit / Delete Purchasing Officer</asp:HyperLink><br />-->
                                            
                                            <asp:HyperLink ID="lnkEditAdmin" runat="server" NavigateUrl="~/admin/users.aspx?t=3">Edit / Delete Administrator</asp:HyperLink><br />
                                            <br />                                            
                                            <asp:HyperLink ID="lnkRecoverBuyer" runat="server" NavigateUrl="~/admin/deletedusers.aspx?t=0">Recover Deleted Buyer</asp:HyperLink><br />
                                            <asp:HyperLink ID="lnkRecoverSupplier" runat="server" NavigateUrl="~/admin/deletedusers.aspx?t=1">Recover Deleted Vendor</asp:HyperLink><br />
                                            <!--<asp:HyperLink ID="lnkRecoverPurchasingOfficer" runat="server" NavigateUrl="~/admin/deletedusers.aspx?t=2">Recover Deleted Purchasing Officer</asp:HyperLink><br />-->
                                            
                                            <asp:HyperLink ID="lnkRecoverAdmin" runat="server" NavigateUrl="~/admin/deletedusers.aspx?t=3">Recover Deleted Administrator</asp:HyperLink><br />

                                            <br />
                                            <asp:HyperLink ID="lnkActivateBuyerr" runat="server" NavigateUrl="~/admin/users.aspx?t=0">Activate / Deactivate Buyer</asp:HyperLink><br />
                                            <asp:HyperLink ID="lnkActivateSupplier" runat="server" NavigateUrl="~/admin/users.aspx?t=1">Activate / Deactivate Vendor</asp:HyperLink><br />
                                            <!--<asp:HyperLink ID="lnkActivatePurchasingOfficer" runat="server" NavigateUrl="~/admin/users.aspx?t=2">Activate / Deactivate Purchasing Officer</asp:HyperLink><br />-->
                                           
                                            <br />
                                            <asp:HyperLink ID="lnkBlacklistUser" runat="server" NavigateUrl="~/admin/vendorblacklisting.aspx">Blacklist Vendor</asp:HyperLink><br />
                                            <asp:HyperLink ID="lnkSearchUser" runat="server" NavigateUrl="~/admin/searchUser.aspx">User Search</asp:HyperLink>
                                            <br />
                                            <br />
                                        </p>
                                    </div>
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
