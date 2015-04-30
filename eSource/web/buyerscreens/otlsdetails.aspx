<%@ Page Language="c#" Inherits="EBid.WEB.buyer_screens.OTLSdetails" CodeFile="OTLSdetails.aspx.cs" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml" style="height: 100%;">
<head>
<%@ Register TagPrefix="EBid" TagName="jqueryLoader" Src="../usercontrol/jQuery_loader.ascx" %>
    <EBid:jqueryLoader runat="server" ID="jqueryLoader1" />
    <title id="PageTitle" runat="server"></title>
    <link rel="stylesheet" type="text/css" href='<%= "../themes/"+Session["configTheme"]+"/css/style.css" %>' />
    <meta http-equiv="Content-Language" content="en-us" />
    <meta http-equiv="Content-Type" content="text/html; charset=windows-1252" />

    <script type="text/javascript">
	<!--	
	function Close()
	{
	    var OTS = document.getElementById("hdnOTS").value;
	    var newvendorid = document.getElementById("hdnVendorId").value;
	     var Suppliers = document.getElementById("hdnSuppliers").value;
	     
	    if (OTS =="")
	        OTS = newvendorid;
	    else
	        {
	        var vendoralreadyselected = OTS.indexOf(newvendorid);
	        if (vendoralreadyselected == -1)
	            OTS = OTS + "," + newvendorid;
	        }
	   document.getElementById("hdnOTS").value = OTS;
	   window.opener.RefreshMe(document.getElementById("hdnOTS").value, document.getElementById("hdnCategoryId").value, document.getElementById("hdnSuppliers").value);
	   window.close();
	}	
	-->
    </script>

</head>
<body id="page">
    <form runat="server">
        <div align="left">
            <h1>
                &nbsp;OTLS Details</h1>
             <br />
            <table border="0" cellpadding="0" cellspacing="0" width="100%" id="itemDetails">
                <tr>
                    <th width="149">&nbsp;
                        </th>
                    <th>&nbsp;
                        </th>
                </tr>
                <tr>
                    <td width="149">
                        Supplier Name:
                    </td>
                    <td class="value">
                        <asp:DropDownList runat="server" ID="ddlVendorName" Width="397px" AutoPostBack="True"
                            OnSelectedIndexChanged="ddlVendorName_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td width="149">
                        Address:
                    </td>
                    <td class="value">
                        <asp:Label runat="server" ID="txtAddress1" Width="400px"></asp:Label><br />
                        <br />
                        <asp:Label runat="server" ID="txtAddress2" Width="400px"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td width="149">
                        Contact Person:</td>
                    <td class="value">
                        <asp:Label runat="server" ID="txtContactPerson" Width="400px"></asp:Label></td>
                </tr>
                <tr>
                    <td width="149">
                        Telephone Number:
                    </td>
                    <td class="value">
                        <asp:Label runat="server" ID="txtTelephoneNumber" Width="400px"></asp:Label></td>
                </tr>
                <tr>
                    <td width="149">
                        Email Address:
                    </td>
                    <td class="value">
                        <asp:Label runat="server" ID="txtEmailAddress" Width="400px"></asp:Label></td>
                </tr>
            </table>
            <table border="0" cellpadding="0" cellspacing="0" width="100%" id="actions">
                <tr>
                    <td align="right">
                        <asp:LinkButton runat="server" ID="btnAddNewVendor" OnClick="btnAddNewVendor_Click"
                            Text="Add New Supplier"></asp:LinkButton>
                        <asp:LinkButton runat="server" ID="lnkOK" CausesValidation="false" Text="Ok"></asp:LinkButton>
                    </td>
                </tr>
            </table>
            <table border="0" cellpadding="0" cellspacing="0" width="100%" id="Table1">
                <tr>
                    <td align="left" width="70%" class="value">
                        <asp:Label runat="server" ID="lblMessage" CssClass="discount"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <input type="hidden" runat="server" id="hdnCategoryId" name="hdnCategoryId" />
                        <input type="hidden" runat="server" id="hdnVendorId" name="hdnVendorId" />
                        <input type="hidden" runat="server" id="hdnSuppliers" name="hdnSuppliers" />
                        <input type="hidden" runat="server" id="hdnOTS" name="hdnOTS" />
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
