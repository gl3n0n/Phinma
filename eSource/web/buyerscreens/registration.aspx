<%@ Page Language="C#" AutoEventWireup="true" CodeFile="registration.aspx.cs" Inherits="web_buyer_screens_registration"
    MaintainScrollPositionOnPostback="true" %>

<%@ Register TagPrefix="EBid" TagName="TopNavSuppliers" Src="~/WEB/usercontrol/Buyer/TopNavSuppliers.ascx" %>
<%@ Register TagPrefix="EBid" TagName="TopNav2" Src="~/WEB/usercontrol/Buyer/TopNav2.ascx" %>
<%@ Register TagPrefix="EBid" TagName="LeftNavSuppliers" Src="~/WEB/usercontrol/Buyer/LeftNav.ascx" %>
<%@ Register TagPrefix="EBid" TagName="GlobalLinksNav" Src="~/web/usercontrol/GlobalLinksNav.ascx" %>
<%@ Register TagPrefix="EBid" TagName="Footer" Src="~/web/usercontrol/Footer.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml" style="height: 100%;">
<!-- #BeginTemplate "../templates/suppliers.dwt" -->
<head>
    <meta http-equiv="Content-Language" content="en-us" />
    <meta http-equiv="Content-Type" content="text/html; charset=windows-1252" />
    <script language="javascript" type="text/javascript" src="../include/customValidation.js"></script>
    <script language="javascript" type="text/javascript" src="../include/generalJSFunctions.js"></script>
    <script language="javascript" src="../include/supplierRegistration.js"></script>
    <link rel="stylesheet" type="text/css" href='<%= "../themes/"+Session["configTheme"]+"/css/style.css" %>' />
    <link rel="stylesheet" type="text/css" href='<%="../themes/"+Session["configTheme"]+"/css/style_buyer.css" %>' />    
    <link href='<%= "../themes/"+Session["configTheme"]+"/css/style.css" %>' rel="stylesheet" type="text/css" />
</head>

<script src="../include/generalJSFunctions.js"></script>

<script src="../include/CheckBoxGroup.js"></script>

<script language="javascript"> 
    var _req; 
</script>

<script language="javascript">

function SetFocus()
{
   if  (document.getElementById("hdnFocusToLastPresentService").value == "1")
   {
     var grid = document.getElementById("gvPresentServices");
     var index = grid.rows.length-1; 
     var selectedrow = grid.getElementsByTagName("tr")[index];
     var selectedcell = selectedrow.getElementsByTagName("td")[0];
     var txtName = selectedcell.childNodes[0];
     txtName.focus();
     document.getElementById("hdnFocusToLastPresentService").value = "";
   } 
   if  (document.getElementById("hdnFocusToLastMajorCustomer").value == "1")
   {
     var grid = document.getElementById("gvMajorCustomers");
     var index = grid.rows.length-1; 
     var selectedrow = grid.getElementsByTagName("tr")[index];
     var selectedcell = selectedrow.getElementsByTagName("td")[0];
     var txtName = selectedcell.childNodes[0];
     txtName.focus();
     document.getElementById("hdnFocusToLastMajorCustomer").value = "";
   } 
   if  (document.getElementById("hdnFocusToLastBank").value == "1")
   {
     var grid = document.getElementById("gvBanks");
     var index = grid.rows.length-1; 
     var selectedrow = grid.getElementsByTagName("tr")[index];
     var selectedcell = selectedrow.getElementsByTagName("td")[0];
     var txtName = selectedcell.childNodes[0];
     txtName.focus();
     document.getElementById("hdnFocusToLastBank").value = "";
   } 
   if  (document.getElementById("hdnFocusToLastAffiliatedCompany").value == "1")
   {
     var grid = document.getElementById("gvAffiliatedCompanies");
     var index = grid.rows.length-1; 
     var selectedrow = grid.getElementsByTagName("tr")[index];
     var selectedcell = selectedrow.getElementsByTagName("td")[0];
     var txtName = selectedcell.childNodes[0];
     txtName.focus();
     document.getElementById("hdnFocusToLastAffiliatedCompany").value = "";
   } 
   if  (document.getElementById("hdnFocusToLastExternalAuditor").value == "1")
   {
     var grid = document.getElementById("gvExternalAuditors");
     var index = grid.rows.length-1; 
     var selectedrow = grid.getElementsByTagName("tr")[index];
     var selectedcell = selectedrow.getElementsByTagName("td")[0];
     var txtName = selectedcell.childNodes[0];
     txtName.focus();
     document.getElementById("hdnFocusToLastExternalAuditor").value = "";
   } 
   if  (document.getElementById("hdnFocusToLastEquipment").value == "1")
   {
     var grid = document.getElementById("gvEquipment");
     var index = grid.rows.length-1; 
     var selectedrow = grid.getElementsByTagName("tr")[index];
     var selectedcell = selectedrow.getElementsByTagName("td")[0];
     var txtName = selectedcell.childNodes[0];
     txtName.focus();
     document.getElementById("hdnFocusToLastEquipment").value = "";
   } 
   if  (document.getElementById("hdnFocusToLastRelative").value == "1")
   {
     var grid = document.getElementById("gvRelatives");
     var index = grid.rows.length-1; //-2 because of footer row
     var selectedrow = grid.getElementsByTagName("tr")[index];
     var selectedcell = selectedrow.getElementsByTagName("td")[0];
     var txtName = selectedcell.childNodes[0];
     txtName.focus();
     document.getElementById("hdnFocusToLastRelative").value = "";
   } 
   
   DisableBranchValidator();
}

function DisableBranchValidator()
{
    document.getElementById("rfvBranchAddress1").enabled = false;
}

function BranchValidator()
{
    if (trim(document.getElementById("txtBranchAddress1").value) == "")
        document.getElementById("rfvBranchTel").enabled=false;
    else 
        document.getElementById("rfvBranchTel").enabled=true;
    
    
    if ((trim(document.getElementById("txtBranchAddress2").value) != "")||
    (trim(document.getElementById("txtBranchTel").value) != "")||
    (trim(document.getElementById("txtBranchExt").value) != "")||
    (trim(document.getElementById("txtBranchFax").value) != ""))
        document.getElementById("rfvBranchAddress1").enabled = true;
    else
        document.getElementById("rfvBranchAddress1").enabled = false;
}

  function ConfirmPresentServices()
 {
    frmRegistration.hdnConfirmRemovePS.value = window.confirm("Are you sure you want to delete this item?");
    frmRegistration.hdnFocusToLastPresentService.value="1";
 }
 
  function ConfirmMajorCustomers()
 {
    frmRegistration.hdnConfirmRemoveMC.value = window.confirm("Are you sure you want to delete this item?");
    frmRegistration.hdnFocusToLastMajorCustomer.value="1";
 }
 
  function ConfirmBanks()
 {
    frmRegistration.hdnConfirmRemoveBank.value = window.confirm("Are you sure you want to delete this item?");
    frmRegistration.hdnFocusToLastBank.value="1";
 }
 
  function ConfirmAffiliatedCompanies()
 {
    frmRegistration.hdnConfirmRemoveCompany.value = window.confirm("Are you sure you want to delete this item?");
    frmRegistration.hdnFocusToLastAffiliatedCompany.value="1";
 }
 
  function ConfirmExternalAuditors()
 {
    frmRegistration.hdnConfirmRemoveAuditor.value = window.confirm("Are you sure you want to delete this item?");
    frmRegistration.hdnFocusToLastExternalAuditor.value="1";
 }
 
  function ConfirmEquipment()
 {
    frmRegistration.hdnConfirmRemoveEquipment.value = window.confirm("Are you sure you want to delete this item?");
    frmRegistration.hdnFocusToLastEquipment.value="1";
 }
 
  function ConfirmRelative()
 {
    frmRegistration.hdnConfirmRemoveRelative.value = window.confirm("Are you sure you want to delete this item?");
    frmRegistration.hdnFocusToLastRelative.value="1";
 }
 
 function ValidateOrgType(source, args)
 {
    if ((document.getElementById("chkOrgType_Individual").checked)||(document.getElementById("chkOrgType_Partnership").checked)||(document.getElementById("chkOrgType_Corporation").checked))
        args.IsValid = true;
    else
        args.IsValid = false;
 }
 
  function ValidateCompanyClass(source, args)
 {
    if ((document.getElementById("chkCompanyClassification_0").checked)||
        (document.getElementById("chkCompanyClassification_1").checked)||
        (document.getElementById("chkCompanyClassification_2").checked)||
        (document.getElementById("chkCompanyClassification_3").checked)||
        (document.getElementById("chkCompanyClassification_4").checked))
        args.IsValid = true;
    else
        args.IsValid = false;
 }
 function AutoCompute()
 {
    var vFil = trim(document.getElementById("txtOwnershipFilipino").value);
    var vOther = trim(document.getElementById("txtOtherNationality").value);
    var tempSum;
    var roundedSum;
   
    if ((!isNaN(vFil)) && (!isNaN(vOther)) && (vFil!='') && (vOther!=''))
        {
            tempSum = parseFloat(vFil) +  parseFloat(vOther);
            roundedSum = Math.round(tempSum*100)/100;
            document.getElementById("txtSum").value = roundedSum;
            return;
        }
    else
        {
            if ((isNaN(vFil)) ||(vFil=='')) 
            {
                 if ((isNaN(vOther)) ||(vOther=='')) 
                    {
                       document.getElementById("txtSum").value =0;
                    }   
                 else
                 {
                    tempSum = parseFloat(vOther);
                    roundedSum = Math.round(tempSum*100)/100;
                    document.getElementById("txtSum").value = roundedSum;
                 }
            }
            if ((isNaN(vOther)) ||(vOther=='')) 
            {
                if ((isNaN(vFil)) ||(vFil=='')) 
                {
                   document.getElementById("txtSum").value =0;
                }
                else
                {
                    tempSum = parseFloat(vFil);
                    roundedSum = Math.round(tempSum*100)/100;
                    document.getElementById("txtSum").value = roundedSum;
                }
            }   
        }
 }
</script>

<body onload="SetFocus()">
    <form runat="server" id="frmRegistration">
        <div align="left">
            <div align="left">
                <table border="0" cellpadding="0" cellspacing="0" width="100%" id="page">
                    <tr>
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
                                <tr>
                                    <td>
                                        <EBid:TopNav2 runat="server" ID="TopNav2_1" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td id="relatedInfo">
                                        <div align="left">
                                            <EBid:LeftNavSuppliers runat="server" ID="LeftNavSuppliers" />
                                        </div>
                                        <div align="left">
                                            <!-- #BeginEditable "commentArea" -->
                                            <table border="0" cellpadding="0" cellspacing="0" width="100%" id="table3">
                                                <tr>
                                                    <td>
                                                        <div align="left">
                                                            <table border="0" cellpadding="0" cellspacing="0" width="100%" id="table4">
                                                                <tr>
                                                                    <td>
                                                                        &nbsp;</td>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                    </td>
                                                </tr>
                                            </table>
                                            <!-- #EndEditable -->
                                        </div>
                                    </td>
                                    <td id="content" style="width: 100%" accesskey="a">
                                        <!-- #BeginEditable "contentArea" -->
                                        <h1>
                                            <br />
                                            Supplier Registration</h1>
                                        <p>
                                            Fill up the fields below then click submit to complete the registration Process.</p>
                                        <div align="left">
                                            <table border="0" cellpadding="0" cellspacing="0" width="100%" id="itemDetails">
                                                <tr>
                                                    <th>
                                                        Supplier Type</th>
                                                </tr>
                                                <tr>
                                                    <td class="value">
                                                        <asp:DropDownList runat="server" ID="ddlSupplierType">
                                                        </asp:DropDownList><asp:RequiredFieldValidator ID="rfvSupplierType" runat="server"
                                                            ErrorMessage="Supplier Type is a required field." ControlToValidate="ddlSupplierType"
                                                            Display="None" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                            </table>
                                            <table border="0" cellpadding="0" cellspacing="0" width="100%" id="itemDetails">
                                                <tr>
                                                    <th>
                                                        Category</th>
                                                </tr>
                                                <tr>
                                                    <td class="value">
                                                        <table border="0" cellpadding="0" cellspacing="0" width="100%" id="table8">
                                                            <tr>
                                                                <td width="167" align="right">
                                                                    <asp:ListBox runat="server" ID="lstCategories" Style="width: 250; height: 150" SelectionMode="Multiple">
                                                                    </asp:ListBox>
                                                                </td>
                                                                <td width="82" align="right">
                                                                    <p align="center" style="text-align: center">
                                                                        <asp:TextBox runat="server" ID="hdnCategories" Width="0" Height="0" />
                                                                        <input type="button" value=">" id="btnSelectCategory" runat="server" /><br />
                                                                        <input type="button" value=">>" id="btnSelectAllCategories" runat="server" /><br />
                                                                        <input type="button" value="<<" id="btnDeSelectCategory" runat="server" /><br />
                                                                        <input type="button" value="<" id="btnDeSelectAllCategories" runat="server" />
                                                                    </p>
                                                                </td>
                                                                <td align="right">
                                                                    <p align="center">
                                                                        <asp:ListBox runat="server" ID="lstSelectedCategories" Style="width: 250; height: 150"
                                                                            SelectionMode="Multiple"></asp:ListBox>
                                                                        <asp:RequiredFieldValidator ID="rfvCategories" runat="server" ErrorMessage="Categories is a required field."
                                                                            ControlToValidate="hdnCategories" SetFocusOnError="True" Display="None"></asp:RequiredFieldValidator>
                                                                    </p>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                            <table border="0" cellpadding="0" cellspacing="0" width="100%" id="itemDetails">
                                                <tr>
                                                    <th>
                                                        Sub-category</th>
                                                </tr>
                                                <tr>
                                                    <td class="value">
                                                        <table border="0" cellpadding="0" cellspacing="0" width="100%" id="table1">
                                                            <tr>
                                                                <td width="167" align="right">
                                                                    <asp:ListBox runat="server" ID="lstSubCategory" Style="width: 250; height: 150" SelectionMode="Multiple">
                                                                    </asp:ListBox>
                                                                </td>
                                                                <td width="82" align="right">
                                                                    <p align="center" style="text-align: center">
                                                                        <asp:TextBox runat="server" ID="hdnSubCategory" Width="0" Height="0" />
                                                                        <input type="button" value=">" id="btnSelectSubCategory" runat="server" /><br />
                                                                        <input type="button" value=">>" id="btnSelectAllSubCategories" runat="server" /><br />
                                                                        <input type="button" value="<<" id="btnDeSelectAllSubCategories" runat="server" /><br />
                                                                        <input type="button" value="<" id="btnDeSelectSubCategory" runat="server" />
                                                                    </p>
                                                                </td>
                                                                <td align="right">
                                                                    <p align="center">
                                                                        <asp:ListBox runat="server" ID="lstSelectedSubCategories" Style="width: 250; height: 150"
                                                                            SelectionMode="Multiple" onDblClick="removeABrand();"></asp:ListBox>
                                                                        <asp:RequiredFieldValidator ID="rfvSubCategories" runat="server" ErrorMessage="Sub-categories is a required field."
                                                                            ControlToValidate="hdnSubCategory" SetFocusOnError="True" Display="None"></asp:RequiredFieldValidator>
                                                                    </p>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                        <table border="0" cellpadding="0" cellspacing="0" width="100%" id="itemDetails">
                                            <tr>
                                                <th colspan="2">
                                                    Company information</th>
                                            </tr>
                                            <tr>
                                                <td width="101%" colspan="2" class="value">
                                                    Company Name:
                                                    <input type="text" runat="server" id="txtCompanyName" size="56"><asp:RequiredFieldValidator
                                                        ID="rfvCompanyName" runat="server" ErrorMessage="Company Name is a required field."
                                                        ControlToValidate="txtCompanyName" Display="None" SetFocusOnError="True"></asp:RequiredFieldValidator></td>
                                            </tr>
                                            <tr>
                                                <td width="17%" class="evenCells">
                                                    Address: (Head Office)</td>
                                                <td class="evenCells" width="84%">
                                                    <input type="text" size="56" runat="server" id="txtHeadAddress1"><asp:RequiredFieldValidator
                                                        ID="rfvAddressHead" runat="server" ErrorMessage="Address: (Head Office) is a required field."
                                                        ControlToValidate="txtHeadAddress1" Display="None" SetFocusOnError="True"></asp:RequiredFieldValidator><br />
                                                    <br />
                                                    <input type="text" size="56" runat="server" id="txtHeadAddress2"></td>
                                            </tr>
                                            <tr>
                                                <td width="101%" class="value" colspan="2">
                                                    Telephone:&nbsp;
                                                    <input type="text" size="20" runat="server" id="txtHeadTel" />&nbsp;<asp:RequiredFieldValidator
                                                        ID="rfvTelephone" runat="server" ErrorMessage="Telephone is a required field."
                                                        ControlToValidate="txtHeadTel" Display="None" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                    Extension:
                                                    <input type="text" size="20" runat="server" id="txtHeadExt" /></td>
                                            </tr>
                                            <tr>
                                                <td width="101%" class="evenCells" colspan="2">
                                                    Fax:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                    <input type="text" size="20" runat="server" id="txtHeadFax" />&nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="17%" class="value">
                                                    Address: (Branch)</td>
                                                <td class="value" width="84%">
                                                    <input type="text" size="56" runat="server" id="txtBranchAddress1" /><asp:RequiredFieldValidator
                                                        ID="rfvBranchAddress1" runat="server" ErrorMessage="Address: (Branch) is a required field."
                                                        ControlToValidate="txtBranchAddress1" Display="None" SetFocusOnError="True"></asp:RequiredFieldValidator><br />
                                                    <br />
                                                    <input type="text" size="56" runat="server" id="txtBranchAddress2" /></td>
                                            </tr>
                                            <tr>
                                                <td width="101%" class="evenCells" colspan="2">
                                                    Telephone:&nbsp;
                                                    <input type="text" size="20" runat="server" id="txtBranchTel" />&nbsp;<asp:RequiredFieldValidator
                                                        ID="rfvBranchTel" runat="server" ErrorMessage="Telephone is a required field."
                                                        ControlToValidate="txtBranchTel" Display="None" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                    Extension:
                                                    <input type="text" size="20" runat="server" id="txtBranchExt" /></td>
                                            </tr>
                                            <tr>
                                                <td width="101%" class="value" colspan="2">
                                                    Fax:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                    <input type="text" size="20" runat="server" id="txtBranchFax" />&nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="85%" class="evenCells" colspan="2">
                                                    VAT Reg. No.:
                                                    <input type="text" size="31" runat="server" id="txtVatRegNo" /><asp:RequiredFieldValidator
                                                        ID="rfvVatRegNo" runat="server" ErrorMessage="VAT Reg. No. is a required field."
                                                        ControlToValidate="txtVatRegNo" Display="None" SetFocusOnError="True"></asp:RequiredFieldValidator><br />
                                                    <br />
                                                    TIN:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                    <input type="text" size="31" runat="server" id="txtTIN" /><asp:RequiredFieldValidator
                                                        ID="rfvTIN" runat="server" ErrorMessage="TIN is a required field." ControlToValidate="txtTIN"
                                                        Display="None" SetFocusOnError="True"></asp:RequiredFieldValidator></td>
                                            </tr>
                                            <tr>
                                                <td width="85%" class="value" colspan="2">
                                                    P.O. Box:
                                                    <input type="text" size="12" runat="server" id="txtPOBOX"><asp:RequiredFieldValidator
                                                        ID="rfvPOBox" runat="server" ErrorMessage="P.O. Box is a required field." ControlToValidate="txtPOBOX"
                                                        Display="None" SetFocusOnError="True"></asp:RequiredFieldValidator>&nbsp;&nbsp;&nbsp;&nbsp;
                                                    Postal Code:
                                                    <input type="text" size="10" runat="server" id="txtPostalCode"><asp:RequiredFieldValidator
                                                        ID="rfvPostalCode" runat="server" ErrorMessage="Postal Code is a required field."
                                                        ControlToValidate="txtPostalCode" Display="None" SetFocusOnError="True"></asp:RequiredFieldValidator>&nbsp;&nbsp;&nbsp;&nbsp;
                                                    <br />
                                                    <br />
                                                    Email:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                    <input type="text" size="24" runat="server" id="txtEmail">
                                                    <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ErrorMessage="Email  is a required field."
                                                        ControlToValidate="txtEmail" Display="None" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="regEmail" runat="server" ControlToValidate="txtEmail"
                                                        Display="None" ErrorMessage="Email is not in correct format." SetFocusOnError="True"
                                                        ValidationExpression="^(([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5}){1,25})+([;.](([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5}){1,25})+)*$"></asp:RegularExpressionValidator></td>
                                            </tr>
                                            <tr>
                                                <td width="85%" class="evenCells" colspan="2">
                                                    Standard Terms of Payment:
                                                    <input type="text" size="20" runat="server" id="txtStandardTermsOfPayment" /><asp:RequiredFieldValidator
                                                        ID="rfvStandardTermsOfPayment" runat="server" ErrorMessage="Standard Terms of Payment is a required field."
                                                        ControlToValidate="txtStandardTermsOfPayment" Display="None" SetFocusOnError="True"></asp:RequiredFieldValidator>&nbsp;
                                                    <br />
                                                    <br />
                                                    Special Terms:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                    <input type="text" size="20" runat="server" id="txtSpecialTerms" /><asp:RequiredFieldValidator
                                                        ID="rfvSpecialTerms" runat="server" ErrorMessage="Special Terms is a required field."
                                                        ControlToValidate="txtSpecialTerms" Display="None" SetFocusOnError="True"></asp:RequiredFieldValidator>&nbsp;%PPD
                                                    <br />
                                                    <br />
                                                    Minimum Order Value:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                    <input type="text" size="20" runat="server" id="txtMinOrderValue" onkeypress="AcceptDecimals(this);" /><asp:RequiredFieldValidator
                                                        ID="rfvMinOrderValue" runat="server" ErrorMessage="Minimum Order Value is a required field."
                                                        ControlToValidate="txtMinOrderValue" Display="None" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="regMinOrderValue" runat="server" ControlToValidate="txtMinOrderValue"
                                                        Display="None" ErrorMessage="Minimum Order Value is a numeric field with 2 decimal places."
                                                        SetFocusOnError="True" ValidationExpression="^[-]?([1-9]{1}[0-9]{0,}(\.[0-9]{0,2})?|0(\.[0-9]{0,2})?|\.[0-9]{1,2})$"></asp:RegularExpressionValidator>
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="85%" class="value" colspan="2">
                                                    Sales Person:
                                                    <input type="text" size="32" runat="server" id="txtSalesPerson"><asp:RequiredFieldValidator
                                                        ID="rfvSalesPerson" runat="server" ErrorMessage="Sales Person is a required field."
                                                        ControlToValidate="txtSalesPerson" Display="None" SetFocusOnError="True"></asp:RequiredFieldValidator>&nbsp;&nbsp;&nbsp;&nbsp;
                                                    Telephone:
                                                    <input type="text" size="14" runat="server" id="txtSalesPersonTel"><asp:RequiredFieldValidator
                                                        ID="rfvSalesPersonTel" runat="server" ErrorMessage="Telephone is a required field."
                                                        ControlToValidate="txtSalesPersonTel" Display="None" SetFocusOnError="True"></asp:RequiredFieldValidator></td>
                                            </tr>
                                            <tr>
                                                <td width="85%" class="evenCells" colspan="2">
                                                    Type of Business Organization:
                                                    <br />
                                                    <br />

                                                    <script language="JavaScript">
                                var limitGroup = new CheckBoxGroup();
                                limitGroup.addToGroup("chkOrgType_*");
                                limitGroup.setMaxAllowed(1,"Only 1 Type of Business Organization is allowed!");
                                                    </script>

                                                    <table id="chkTypeOfOrganization" border="0">
                                                        <tr>
                                                            <td>
                                                                <input runat="server" type="checkbox" name="chkOrgType" id="chkOrgType_Individual"
                                                                    onclick="limitGroup.check(this);" /><label for="chkTypeOfOrganization_0">Individual</label></td>
                                                            <td>
                                                                <input runat="server" type="checkbox" name="chkOrgType" id="chkOrgType_Partnership"
                                                                    onclick="limitGroup.check(this);" /><label for="chkTypeOfOrganization_1">Partnership</label></td>
                                                            <td>
                                                                <input runat="server" type="checkbox" name="chkOrgType" id="chkOrgType_Corporation"
                                                                    onclick="limitGroup.check(this);" /><label for="chkTypeOfOrganization_2">Corporation</label></td>
                                                        </tr>
                                                    </table>
                                                    <asp:CustomValidator ID="cvTypeOfOrganizaton" runat="server" ClientValidationFunction="ValidateOrgType(this, args);"
                                                        Display="None" ErrorMessage="Select at least 1 Organization Type."></asp:CustomValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="85%" class="value" colspan="2">
                                                    Ownership:<br />
                                                    <br />
                                                    <input type="text" runat="server" id="txtOwnershipFilipino" size="5" onkeypress="AcceptDecimals(this);" />%
                                                    Filipino&nbsp;&nbsp;&nbsp;&nbsp;<asp:RegularExpressionValidator ID="regUnits" runat="server"
                                                        ControlToValidate="txtOwnershipFilipino" Display="None" ErrorMessage="Ownership % Filipino is a numeric value with 2 decimal places."
                                                        SetFocusOnError="True" ValidationExpression="^[-]?([1-9]{1}[0-9]{0,}(\.[0-9]{0,2})?|0(\.[0-9]{0,2})?|\.[0-9]{1,2})$"></asp:RegularExpressionValidator>
                                                    <input type="text" runat="server" id="txtOtherNationality" size="5" onkeypress="AcceptDecimals(this);" />%
                                                    Other Nationality<asp:RegularExpressionValidator ID="regOther" runat="server" ControlToValidate="txtOtherNationality"
                                                        Display="None" ErrorMessage="Ownership % Other Nationality is a numeric value with 2 decimal places."
                                                        SetFocusOnError="True" ValidationExpression="^[-]?([1-9]{1}[0-9]{0,}(\.[0-9]{0,2})?|0(\.[0-9]{0,2})?|\.[0-9]{1,2})$"></asp:RegularExpressionValidator>
                                                    <asp:TextBox runat="server" ID="txtSum" Width="0"></asp:TextBox>
                                                    <asp:CompareValidator ID="cvSum" runat="server" ControlToValidate="txtSum" Display="None"
                                                        ErrorMessage="Sum of Ownership % Filipino and % Other Nationality should equal to 100."
                                                        SetFocusOnError="True" Type="Integer" ValueToCompare="100"></asp:CompareValidator></td>
                                            </tr>
                                            <tr>

                                                <script language="JavaScript">
                        function CheckCompanyClass()
                        {
                        //alert(document.frmRegistration.chkCompanyClass[4]);
                            if (document.frmRegistration.chkCompanyClassification_4.checked)
                              {
                                document.frmRegistration.chkCompanyClassification_0.checked = false; 
                                document.frmRegistration.chkCompanyClassification_0.disabled = true;
                                document.frmRegistration.chkCompanyClassification_1.checked = false; 
                                document.frmRegistration.chkCompanyClassification_1.disabled = true;
                                document.frmRegistration.chkCompanyClassification_2.checked = false; 
                                document.frmRegistration.chkCompanyClassification_2.disabled = true;
                                document.frmRegistration.chkCompanyClassification_3.checked = false; 
                                document.frmRegistration.chkCompanyClassification_3.disabled = true;
                              }
                             else
                             {
                                document.frmRegistration.chkCompanyClassification_0.disabled = false;
                                document.frmRegistration.chkCompanyClassification_1.disabled = false;
                                document.frmRegistration.chkCompanyClassification_2.disabled = false;
                                document.frmRegistration.chkCompanyClassification_3.disabled = false;
                             }
                             
                        }
                       
                                                </script>

                                                <td width="85%" class="evenCells" colspan="2">
                                                    Company Classification (for goods Supplier Only):<br />
                                                    <br />
                                                    <table id="chkCompanyClassification" border="0">
                                                        <tr>
                                                            <td>
                                                                <input runat="server" name="chkCompanyClass" id="chkCompanyClassification_0" type="checkbox"
                                                                    onclick="CheckCompanyClass();" /><label for="chkCompanyClassification_0">Manufacturer</label></td>
                                                            <td>
                                                                <input runat="server" name="chkCompanyClass" id="chkCompanyClassification_1" type="checkbox"
                                                                    onclick="CheckCompanyClass();" /><label for="chkCompanyClassification_1">Importer</label></td>
                                                            <td>
                                                                <input runat="server" name="chkCompanyClass" id="chkCompanyClassification_2" type="checkbox"
                                                                    onclick="CheckCompanyClass();" /><label for="chkCompanyClassification_2">Dealer</label></td>
                                                            <td>
                                                                <input runat="server" name="chkCompanyClass" id="chkCompanyClassification_3" type="checkbox"
                                                                    onclick="CheckCompanyClass();" /><label for="chkCompanyClassification_3">Trader</label></td>
                                                            <td>
                                                                <input runat="server" name="chkCompanyClass" id="chkCompanyClassification_4" type="checkbox"
                                                                    onclick="CheckCompanyClass();" /><label for="chkCompanyClassification_4">N/A</label></td>
                                                        </tr>
                                                        <asp:CustomValidator ID="cvCompanyClass" runat="server" ClientValidationFunction="ValidateCompanyClass(this, args);"
                                                            Display="None" ErrorMessage="Select at least 1 Company Classification." SetFocusOnError="true"></asp:CustomValidator></table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="85%" class="value" colspan="2">
                                                    Sole Supplier:&nbsp;
                                                    <input type="text" id="txtSoleSupplier" size="57" runat="server" /><asp:RequiredFieldValidator
                                                        ID="rfvSoleSupplier" runat="server" ErrorMessage="Sole Supplier is a required field."
                                                        ControlToValidate="txtSoleSupplier" Display="None" SetFocusOnError="True"></asp:RequiredFieldValidator><br />
                                                    <br />
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                    <input type="text" id="txtSoleSupplier2" size="57" runat="server" /></td>
                                            </tr>
                                            <tr>
                                                <td width="85%" class="evenCells" colspan="2">
                                                    Specialization:
                                                    <input type="text" id="txtSpecialization" size="57" runat="server"><asp:RequiredFieldValidator
                                                        ID="rfvSpecialization" runat="server" ErrorMessage="Specialization is a required field."
                                                        ControlToValidate="txtSpecialization" Display="None" SetFocusOnError="True"></asp:RequiredFieldValidator>&nbsp;&nbsp;&nbsp;&nbsp;
                                                </td>
                                            </tr>
                                        </table>
                                        <br />
                                        <div>
                                            <table border="0" cellpadding="0" cellspacing="0" width="100%" class="vendorDetails_all">
                                                <tr>
                                                    <th colspan="3" class="itemDetails_th">
                                                        Key Personnel</th>
                                                </tr>
                                                <tr>
                                                    <td valign="top">
                                                        <asp:GridView Width='100%' ID="gvKeyPersonnel" runat="server" AutoGenerateColumns="False"
                                                            CssClass="vendorDetails" BorderWidth="0px">
                                                            <AlternatingRowStyle CssClass="evenCells" />
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Names">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtName" runat="server" Text='<%# Bind("Name")%>' MaxLength="100"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="rfvNames" runat="server" ControlToValidate="txtName"
                                                                            Display="None" ErrorMessage="Key Personnel Name is a required field." SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                                    </ItemTemplate>
                                                                    <ItemStyle CssClass="vendorDetails_td" />
                                                                    <HeaderStyle CssClass="vendorDetails_th" Font-Bold="False" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Position">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtTitlePosition" runat="server" Text='<%# Bind("Position") %>'
                                                                            MaxLength="100"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="rfvPosition" runat="server" ControlToValidate="txtTitlePosition"
                                                                            Display="None" ErrorMessage="Key Personnel Position is a required field." SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                                    </ItemTemplate>
                                                                    <ItemStyle CssClass="vendorDetails_td" />
                                                                    <HeaderStyle CssClass="vendorDetails_th" Font-Bold="False" />
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                        <br />
                                        <div>
                                            <table border="0" cellpadding="0" cellspacing="0" width="100%" class="vendorDetails_all">
                                                <tr>
                                                    <th colspan="3" class="itemDetails_th" style="height: 18px">
                                                        Present Services availed from Trans-Asia </th>
                                                </tr>
                                                <tr>
                                                    <td valign="top">
                                                        <asp:GridView Width='100%' ID="gvPresentServices" runat="server" AutoGenerateColumns="False"
                                                            CssClass="vendorDetails" BorderWidth="0" OnRowCommand="gvPresentServices_RowCommand"
                                                            OnRowDataBound="gvPresentServices_RowDataBound">
                                                            <AlternatingRowStyle CssClass="evenCells" />
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Type Of Plan">
                                                                    <ItemTemplate>
                                                                        <asp:DropDownList runat="server" ID="ddlTypeOfPlan">
                                                                        </asp:DropDownList>
                                                                        <asp:HiddenField runat="server" ID="hdnTypeOfPlan" Value='<%# Bind("Plan")%>'></asp:HiddenField>
                                                                        <asp:RequiredFieldValidator ID="rfvTypeOfPlan" runat="server" ControlToValidate="ddlTypeOfPlan"
                                                                            Display="None" ErrorMessage="Type of Plan is a required field." SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                                    </ItemTemplate>
                                                                    <ItemStyle CssClass="vendorDetails_td" />
                                                                    <HeaderStyle CssClass="vendorDetails_th" Font-Bold="False" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Acct No.">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtAcctNo" runat="server" Text='<%# Bind("AcctNo") %>' MaxLength="100"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="rfvAccountNo" runat="server" ControlToValidate="txtAcctNo"
                                                                            Display="None" ErrorMessage="Acct. No. is a required field." SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                                    </ItemTemplate>
                                                                    <ItemStyle CssClass="vendorDetails_td" />
                                                                    <HeaderStyle CssClass="vendorDetails_th" Font-Bold="False" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Credit Limit">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtCreditLimit" runat="server" Text='<%# Bind("CreditLimit") %>'
                                                                            MaxLength="20"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="rfvCreditLimit" runat="server" ControlToValidate="txtCreditLimit"
                                                                            Display="None" ErrorMessage="Credit Limit is a required field." SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                                    </ItemTemplate>
                                                                    <ItemStyle CssClass="vendorDetails_td" />
                                                                    <HeaderStyle CssClass="vendorDetails_th" Font-Bold="False" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="lnkRemove" runat="server" CausesValidation="False" CommandName="Remove"
                                                                            CommandArgument='<%# Bind("Index") %>' Text="Remove"></asp:LinkButton>
                                                                    </ItemTemplate>
                                                                    <ItemStyle CssClass="vendorDetails_td" />
                                                                    <HeaderStyle CssClass="vendorDetails_th" Font-Bold="False" />
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </td>
                                                </tr>
                                            </table>
                                            <asp:LinkButton CausesValidation="false" ID="lnkPresentServices" runat="server" OnClick="lnkPresentServices_Click">Add Present Services</asp:LinkButton>
                                        </div>
                                        <br />
                                        <div>
                                            <table border="0" cellpadding="0" cellspacing="0" width="100%" class="vendorDetails_all">
                                                <tr>
                                                    <th colspan="3" class="itemDetails_th">
                                                        References</th>
                                                </tr>
                                                <tr>
                                                    <td valign="top">
                                                        <asp:GridView Width='100%' ID="gvMajorCustomers" runat="server" AutoGenerateColumns="False"
                                                            CssClass="vendorDetails" BorderWidth="0" OnRowCommand="gvMajorCustomers_RowCommand"
                                                            OnRowDataBound="gvMajorCustomers_RowDataBound">
                                                            <AlternatingRowStyle CssClass="evenCells" />
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Major Customers">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtMajorCust" runat="server" Text='<%# Bind("Customer")%>' MaxLength="100"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="rfvMajorCustomer" runat="server" ControlToValidate="txtMajorCust"
                                                                            Display="None" ErrorMessage="Major Customer is a required field." SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                                    </ItemTemplate>
                                                                    <ItemStyle CssClass="vendorDetails_td" />
                                                                    <HeaderStyle CssClass="vendorDetails_th" Font-Bold="False" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Average Monthly Sales">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtAveMonthlySales" runat="server" Text='<%# Bind("Sale")%>' MaxLength="20"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="rfvAveMonthlySales" runat="server" ControlToValidate="txtAveMonthlySales"
                                                                            Display="None" ErrorMessage="Average Monthly Sales is a required field." SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                                    </ItemTemplate>
                                                                    <ItemStyle CssClass="vendorDetails_td" />
                                                                    <HeaderStyle CssClass="vendorDetails_th" Font-Bold="False" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="lnkRemove" runat="server" CausesValidation="False" CommandName="Remove"
                                                                            CommandArgument='<%# Bind("Index") %>' Text="Remove"></asp:LinkButton>
                                                                    </ItemTemplate>
                                                                    <ItemStyle CssClass="vendorDetails_td" />
                                                                    <HeaderStyle CssClass="vendorDetails_th" Font-Bold="False" />
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td valign="top">
                                                        <asp:GridView Width='100%' ID="gvBanks" runat="server" AutoGenerateColumns="False"
                                                            CssClass="vendorDetails" BorderWidth="0" OnRowCommand="gvBanks_RowCommand" OnRowDataBound="gvBanks_RowDataBound">
                                                            <AlternatingRowStyle CssClass="evenCells" />
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Banks">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtBanks" runat="server" Text='<%# Bind("Bank") %>' MaxLength="100"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="rfvBank" runat="server" ControlToValidate="txtBanks"
                                                                            Display="None" ErrorMessage="Bank is a required field." SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                                    </ItemTemplate>
                                                                    <ItemStyle CssClass="vendorDetails_td" />
                                                                    <HeaderStyle CssClass="vendorDetails_th" Font-Bold="False" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Credit Line">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtCreditLine" runat="server" Text='<%# Bind("CreditLine") %>' MaxLength="20"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="rfvCreditLine" runat="server" ControlToValidate="txtCreditLine"
                                                                            Display="None" ErrorMessage="Credit Line is a required field." SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                                    </ItemTemplate>
                                                                    <ItemStyle CssClass="vendorDetails_td" />
                                                                    <HeaderStyle CssClass="vendorDetails_th" Font-Bold="False" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="lnkRemove" runat="server" CausesValidation="False" CommandName="Remove"
                                                                            CommandArgument='<%# Bind("Index") %>' Text="Remove"></asp:LinkButton>
                                                                    </ItemTemplate>
                                                                    <ItemStyle CssClass="vendorDetails_td" />
                                                                    <HeaderStyle CssClass="vendorDetails_th" Font-Bold="False" />
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td valign="top">
                                                        <asp:GridView Width='100%' ID="gvAffiliatedCompanies" runat="server" AutoGenerateColumns="False"
                                                            CssClass="vendorDetails" BorderWidth="0" OnRowCommand="gvAffiliatedCompanies_RowCommand"
                                                            OnRowDataBound="gvAffiliatedCompanies_RowDataBound">
                                                            <AlternatingRowStyle CssClass="evenCells" />
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Affiliated Companies">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtAffiliatedCompanies" runat="server" Text='<%# Bind("Company") %>'
                                                                            MaxLength="100"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="rfvAffiliatedCompany" runat="server" ControlToValidate="txtAffiliatedCompanies"
                                                                            Display="None" ErrorMessage="Affiliated Company is a required field." SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                                    </ItemTemplate>
                                                                    <ItemStyle CssClass="vendorDetails_td" />
                                                                    <HeaderStyle CssClass="vendorDetails_th" Font-Bold="False" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Kind Of Business">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtKindOfBusiness" runat="server" Text='<%# Bind("Business") %>'
                                                                            MaxLength="100"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="rfvKindOfBusiness" runat="server" ControlToValidate="txtKindOfBusiness"
                                                                            Display="None" ErrorMessage="Kind Of Business is a required field." SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                                    </ItemTemplate>
                                                                    <ItemStyle CssClass="vendorDetails_td" />
                                                                    <HeaderStyle CssClass="vendorDetails_th" Font-Bold="False" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="lnkRemove" runat="server" CausesValidation="False" CommandName="Remove"
                                                                            CommandArgument='<%# Bind("Index") %>' Text="Remove"></asp:LinkButton>
                                                                    </ItemTemplate>
                                                                    <ItemStyle CssClass="vendorDetails_td" />
                                                                    <HeaderStyle CssClass="vendorDetails_th" Font-Bold="False" />
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td valign="top">
                                                        <asp:GridView Width='100%' ID="gvExternalAuditors" runat="server" AutoGenerateColumns="False"
                                                            CssClass="vendorDetails" BorderWidth="0" OnRowCommand="gvExternalAuditors_RowCommand"
                                                            OnRowDataBound="gvExternalAuditors_RowDataBound">
                                                            <AlternatingRowStyle CssClass="evenCells" />
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="External Auditors">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtExternalAuditors" runat="server" Text='<%# Bind("Auditor") %>'
                                                                            MaxLength="100"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="rfvExternalAuditor" runat="server" ControlToValidate="txtExternalAuditors"
                                                                            Display="None" ErrorMessage="External Auditor is a required field." SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                                    </ItemTemplate>
                                                                    <ItemStyle CssClass="vendorDetails_td" />
                                                                    <HeaderStyle CssClass="vendorDetails_th" Font-Bold="False" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Legal Counsel">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtLegalCounsel" runat="server" Text='<%# Bind("Counsel") %>' MaxLength="100"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="rfvLegalCounsel" runat="server" ControlToValidate="txtLegalCounsel"
                                                                            Display="None" ErrorMessage="Legal Counsel is a required field." SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                                    </ItemTemplate>
                                                                    <ItemStyle CssClass="vendorDetails_td" />
                                                                    <HeaderStyle CssClass="vendorDetails_th" Font-Bold="False" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="lnkRemove" runat="server" CausesValidation="False" CommandName="Remove"
                                                                            CommandArgument='<%# Bind("Index") %>' Text="Remove"></asp:LinkButton>
                                                                    </ItemTemplate>
                                                                    <ItemStyle CssClass="vendorDetails_td" />
                                                                    <HeaderStyle CssClass="vendorDetails_th" Font-Bold="False" />
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </td>
                                                </tr>
                                            </table>
                                            <asp:LinkButton CausesValidation="false" ID="lnkMajorCustomers" runat="server" OnClick="lnkMajorCustomers_Click"
                                                Text="Add Major Customers"></asp:LinkButton>&nbsp;&nbsp;<asp:LinkButton CausesValidation="false"
                                                    ID="lnkBanks" runat="server" OnClick="lnkBanks_Click" Text="Add Banks"></asp:LinkButton>&nbsp;&nbsp;<asp:LinkButton
                                                        CausesValidation="false" ID="lnkAffiliatedCompanies" runat="server" OnClick="lnkAffiliatedCompanies_Click"
                                                        Text="Add Affiliated Companies"></asp:LinkButton>&nbsp;&nbsp;<asp:LinkButton CausesValidation="false"
                                                            ID="lnkExternalAuditors" runat="server" OnClick="lnkExternalAuditors_Click" Text="Add External Auditors"></asp:LinkButton>
                                        </div>
                                        <br />
                                        <div>
                                            <table border="0" cellpadding="0" cellspacing="0" width="100%" class="vendorDetails_all">
                                                <tr>
                                                    <th colspan="3" class="itemDetails_th">
                                                        Equipment</th>
                                                </tr>
                                                <tr>
                                                    <td valign="top">
                                                        <asp:GridView Width='100%' ID="gvEquipment" runat="server" AutoGenerateColumns="False"
                                                            CssClass="vendorDetails" BorderWidth="0" OnRowCommand="gvEquipment_RowCommand"
                                                            OnRowDataBound="gvEquipment_RowDataBound">
                                                            <AlternatingRowStyle CssClass="evenCells" />
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Type">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtType" runat="server" Text='<%# Bind("Type") %>' MaxLength="100"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="rfvEquipment" runat="server" ControlToValidate="txtType"
                                                                            Display="None" ErrorMessage="Equipment is a required field." SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                                    </ItemTemplate>
                                                                    <ItemStyle CssClass="vendorDetails_td" />
                                                                    <HeaderStyle CssClass="vendorDetails_th" Font-Bold="False" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Units">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtUnit" runat="server" Text='<%# Bind("Unit") %>' onKeyPress="AcceptDecimals(this);"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="rfvUnits" runat="server" ControlToValidate="txtUnit"
                                                                            Display="None" ErrorMessage="Units is a required field." SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                                        <asp:RegularExpressionValidator ID="regUnits" runat="server" ControlToValidate="txtUnit"
                                                                            Display="None" ErrorMessage="Units is a numeric value with 2 decimal places."
                                                                            SetFocusOnError="True" ValidationExpression="^[-]?([1-9]{1}[0-9]{0,}(\.[0-9]{0,2})?|0(\.[0-9]{0,2})?|\.[0-9]{1,2})$"></asp:RegularExpressionValidator>
                                                                    </ItemTemplate>
                                                                    <ItemStyle CssClass="vendorDetails_td" />
                                                                    <HeaderStyle CssClass="vendorDetails_th" Font-Bold="False" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Remarks">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtRemarks" runat="server" Text='<%# Bind("Remarks") %>' MaxLength="100"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="rfvRemarks" runat="server" ControlToValidate="txtRemarks"
                                                                            Display="None" ErrorMessage="Remarks is a required field." SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                                    </ItemTemplate>
                                                                    <ItemStyle CssClass="vendorDetails_td" />
                                                                    <HeaderStyle CssClass="vendorDetails_th" Font-Bold="False" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="lnkRemove" runat="server" CausesValidation="False" CommandName="Remove"
                                                                            CommandArgument='<%# Bind("Index") %>' Text="Remove"></asp:LinkButton>
                                                                    </ItemTemplate>
                                                                    <ItemStyle CssClass="vendorDetails_td" />
                                                                    <HeaderStyle CssClass="vendorDetails_th" Font-Bold="False" />
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </td>
                                                </tr>
                                            </table>
                                            <asp:LinkButton CausesValidation="false" ID="lnkEquipment" runat="server" OnClick="lnkEquipment_Click">Add Equipment</asp:LinkButton>
                                        </div>
                                        <br />
                                        <div>
                                            <table border="0" cellpadding="0" cellspacing="0" width="100%" class="vendorDetails_all">
                                                <tr>
                                                    <th colspan="3" class="itemDetails_th">
                                                        Relative Working in Trans-Asia </th>
                                                </tr>
                                                <tr>
                                                    <td valign="top">
                                                        <asp:GridView Width='100%' ID="gvRelatives" runat="server" AutoGenerateColumns="False"
                                                            CssClass="vendorDetails" BorderWidth="0" OnRowCommand="gvRelatives_RowCommand"
                                                            OnRowDataBound="gvRelatives_RowDataBound">
                                                            <AlternatingRowStyle CssClass="evenCells" />
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Name">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtName" runat="server" Text='<%# Bind("Name") %>' MaxLength="100"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="rfvName" runat="server" ControlToValidate="txtName"
                                                                            Display="None" ErrorMessage="Relative is a required field." SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                                    </ItemTemplate>
                                                                    <ItemStyle CssClass="vendorDetails_td" />
                                                                    <HeaderStyle CssClass="vendorDetails_th" Font-Bold="False" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Title/Position">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtTitlePosition" runat="server" Text='<%# Bind("TitlePosition") %>'
                                                                            MaxLength="50"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="rfvTitlePosition" runat="server" ControlToValidate="txtTitlePosition"
                                                                            Display="None" ErrorMessage="Title/Position is a required field." SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                                    </ItemTemplate>
                                                                    <ItemStyle CssClass="vendorDetails_td" />
                                                                    <HeaderStyle CssClass="vendorDetails_th" Font-Bold="False" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Relationship">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtRelationship" runat="server" Text='<%# Bind("Relationship") %>'
                                                                            MaxLength="50"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="rfvRelationship" runat="server" ControlToValidate="txtRelationship"
                                                                            Display="None" ErrorMessage="Relationship is a required field." SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                                    </ItemTemplate>
                                                                    <ItemStyle CssClass="vendorDetails_td" />
                                                                    <HeaderStyle CssClass="vendorDetails_th" Font-Bold="False" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="lnkRemove" runat="server" CausesValidation="False" CommandName="Remove"
                                                                            CommandArgument='<%# Bind("Index") %>' Text="Remove"></asp:LinkButton>
                                                                    </ItemTemplate>
                                                                    <ItemStyle CssClass="vendorDetails_td" />
                                                                    <HeaderStyle CssClass="vendorDetails_th" Font-Bold="False" />
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </td>
                                                </tr>
                                            </table>
                                            <asp:LinkButton CausesValidation="false" ID="lnkAddRelative" runat="server" OnClick="lnkAddRelative_Click">Add Relative</asp:LinkButton>
                                        </div>
                                        <br />
                                        <table border="0" cellpadding="0" cellspacing="0" width="100%" id="actions">
                                            <tr>
                                                <td style="height: 34px">
                                                    <asp:LinkButton runat="server" ID="btnNext" OnClick="btnNext_Click">Next</asp:LinkButton></td>
                                            </tr>
                                        </table>
                                        <p>
                                            &nbsp;<!-- #EndEditable -->
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td id="footer">
                            <EBid:Footer runat="server" ID="Footer" />
                        </td>
                    </tr>
                </table>
                <asp:ListBox runat="server" ID="lstTempRemoveSubCategory" Width="0"></asp:ListBox>
                <input type="hidden" id="hdnXMLFileName" runat="server" />
                <input type="hidden" id="hdnFocusToLastPresentService" runat="server" />
                <input type="hidden" id="hdnAddAPresentService" runat="server" />
                <input type="hidden" id="hdnConfirmRemovePS" runat="server" />
                <input type="hidden" id="hdnFocusToLastMajorCustomer" runat="server" />
                <input type="hidden" id="hdnAddMajorCustomer" runat="server" />
                <input type="hidden" id="hdnConfirmRemoveMC" runat="server" />
                <input type="hidden" id="hdnFocusToLastBank" runat="server" />
                <input type="hidden" id="hdnAddABank" runat="server" />
                <input type="hidden" id="hdnConfirmRemoveBank" runat="server" />
                <input type="hidden" id="hdnFocusToLastAffiliatedCompany" runat="server" />
                <input type="hidden" id="hdnAddACompany" runat="server" />
                <input type="hidden" id="hdnConfirmRemoveCompany" runat="server" />
                <input type="hidden" id="hdnFocusToLastExternalAuditor" runat="server" />
                <input type="hidden" id="hdnAddAnAuditor" runat="server" />
                <input type="hidden" id="hdnConfirmRemoveAuditor" runat="server" />
                <input type="hidden" id="hdnFocusToLastEquipment" runat="server" />
                <input type="hidden" id="hdnAddAnEquipment" runat="server" />
                <input type="hidden" id="hdnConfirmRemoveEquipment" runat="server" />
                <input type="hidden" id="hdnFocusToLastRelative" runat="server" />
                <input type="hidden" id="hdnAddARelative" runat="server" />
                <input type="hidden" id="hdnConfirmRemoveRelative" runat="server" />
                <asp:CustomValidator ID="cuvValidate" runat="server" ClientValidationFunction="ValidatorIndividualAlert(this, args);"
                    Display="None"></asp:CustomValidator>

                <script language="javascript">
		//------Begin Category------//
        function copyToCategoryList()
        {
            fromList = document.getElementById("lstCategories");
            toList = document.getElementById("lstSelectedCategories");
            if (toList.options.length > 0 && toList.options[0].value == 'temp')
            {
                toList.options.length = 0;
            }
            var sel = false;
            var allval = "";
            for (i=0;i<fromList.options.length;i++)
            {
                var current = fromList.options[i];
                if (current.selected)
                {
                    sel = true;
                    if (current.value == 'temp')
                    {
                    alert ('You cannot move this text!');
                    return;
                    }
                    txt = current.text;
                    val = current.value;
                    allval = ((trim(allval)=='') ? trim(val) : trim(allval) + ',' + trim(val));
                    toList.options[toList.length] = new Option(txt,val);
                    fromList.options[i] = null;
                    StoreToHiddenValue(trim(val));
                    i--;
                }
            }
            SendQuery("cat",trim(allval)); 
            if (!sel) alert ('You haven\'t selected any options!');
       }
       
       function removeFromCategoryList()
        {
            fromList = document.getElementById("lstSelectedCategories");
            toList = document.getElementById("lstCategories");
            if (toList.options.length > 0 && toList.options[0].value == 'temp')
            {
                toList.options.length = 0;
            }
            var sel = false;
            var allval ="";
            for (i=0;i<fromList.options.length;i++)
            {
                var current = fromList.options[i];
                if (current.selected)
                {
                    sel = true;
                    if (current.value == 'temp')
                    {
                    alert ('You cannot move this text!');
                    return;
                    }
                    txt = current.text;
                    val = current.value;
                    allval = ((trim(allval)=='') ? trim(val) : trim(allval) + ',' + trim(val));
                    toList.options[toList.length] = new Option(txt,val);
                    fromList.options[i] = null;
                    RemoveFromHiddenValue(trim(val));
                    i--;
                }
            }
             SendQuery("remcat",allval);
            if (!sel) alert ('You haven\'t selected any options!');
       }
       
       function copyAllCategories(){
            
            fromList = document.getElementById("lstCategories");
            toList = document.getElementById("lstSelectedCategories");
            var allval ="";
            for(i=0; i<fromList.options.length; i++)
            {
                //get value before it is appended
                var selectedvalue = fromList.options.item(i).value;
                //--store value to allval for SendQuery()
                allval = ((trim(allval)=='') ? trim(selectedvalue) : trim(allval) + ',' + trim(selectedvalue));
                //---
                toList.options[toList.length] = new Option(fromList.options.item(i).text,fromList.options.item(i).value);
                fromList.options[i] = null;
                //store selectedvalue to hidden
                StoreToHiddenValue(selectedvalue);
                i--;
            }
            selectNone(toList,fromList);
            setSize(toList,fromList);
            SendQuery("cat",allval);
        }
        
        function removeAllCategories(){
            var allval ="";
            fromList = document.getElementById("lstSelectedCategories");
            toList = document.getElementById("lstCategories");
            for(i=0; i<fromList.options.length; i++)
            {
                var selectedvalue = fromList.options.item(i).value;
                //--store value to allval for SendQuery()
                allval = ((trim(allval)=='') ? trim(selectedvalue) : trim(allval) + ',' + trim(selectedvalue));
                //---
                toList.options[toList.length] = new Option(fromList.options.item(i).text,fromList.options.item(i).value);
                fromList.options[i] = null;
                RemoveFromHiddenValue(selectedvalue);
                i--;
            }
            selectNone(toList,fromList);
            setSize(toList,fromList);
            SendQuery("remcat",allval);
        }
    
        function copyACategory()
        {
           fromList = document.getElementById("lstCategories");
           toList = document.getElementById("lstSelectedCategories");
           var addIndex = fromList.selectedIndex;
           if(addIndex < 0)
              return;
           //get selected value before object is appended to destination list
           var selectedvalue = trim(fromList.options.item(addIndex).value);
           //object is appended
           toList.appendChild(fromList.options.item(addIndex));
           //store selected item to hidden value
           StoreToHiddenValue(selectedvalue);
           selectNone(toList,fromList);
           setSize(toList,fromList);
           SendQuery("cat",selectedvalue); 
        }
        
        function removeACategory()
        {
           fromList = document.getElementById("lstSelectedCategories");
           toList = document.getElementById("lstCategories");
           var addIndex = fromList.selectedIndex;
           if(addIndex < 0)
              return;
           //get selected value before object is appended to destination list
           var selectedvalue = trim(fromList.options.item(addIndex).value);
           //object is appended
           toList.appendChild(fromList.options.item(addIndex));
           //remove selected item from hidden value
           RemoveFromHiddenValue(selectedvalue);
           selectNone(toList,fromList);
           setSize(toList,fromList);
           SendQuery("remcat",selectedvalue);
        } 
        
        function CheckIfValueExists(selectedCategoryA, selectedvalue)
        {
            for (var i=0; i<selectedCategoryA.length; i++)
            {
                if (trim(selectedCategoryA[i]) == trim(selectedvalue))
                    return true;
            }
            return false;
        }

        function  StoreToHiddenValue(selectedvalue)
        {
           if (trim(document.getElementById("hdnCategories").value) != "")
           {
            var selectedCategory = trim(document.getElementById("hdnCategories").value);
            var selectedCategoryA =  selectedCategory.split(",");
            var exists = CheckIfValueExists(selectedCategoryA, selectedvalue);
            if  (!exists)
                 document.getElementById("hdnCategories").value = selectedCategory + "," + selectedvalue;               
           }
           else
                document.getElementById("hdnCategories").value = selectedvalue;
        }
        
        function RemoveFromHiddenValue(selectedvalue)
        {
           if (trim(document.getElementById("hdnCategories").value) != "")
           {
           
            var selectedCategory = trim(document.getElementById("hdnCategories").value);
            var selectedCategoryA = selectedCategory.split(",");
            var newString = "";
            for (var i=0; i<selectedCategoryA.length; i++)
            {
                if (selectedCategoryA[i]!=selectedvalue)
                {
                    if (newString == "")
                        newString = trim(selectedCategoryA[i]);
                    else
                        newString = newString + "," + trim(selectedCategoryA[i]);
                }
            }
            document.getElementById("hdnCategories").value = trim(newString);
           }
        }
        //------End Categories------//
        //------Begin Subcategory------//
        function copyToSubCategoryList()
        {
            fromList = document.getElementById("lstSubCategory");
            toList = document.getElementById("lstSelectedSubCategories");
            if (toList.options.length > 0 && toList.options[0].value == 'temp')
            {
                toList.options.length = 0;
            }
            var sel = false;
            for (i=0;i<fromList.options.length;i++)
            {
                var current = fromList.options[i];
                if (current.selected)
                {
                    sel = true;
                    if (current.value == 'temp')
                    {
                    alert ('You cannot move this text!');
                    return;
                    }
                    txt = current.text;
                    val = current.value;
                    toList.options[toList.length] = new Option(txt,val);
                    fromList.options[i] = null;
                    StoreSubCategoryToHiddenValue(trim(val));
                    i--;
                }
            }
            if (!sel) alert ('You haven\'t selected any options!');
       }
       
       function removeFromSubCategoryList()
        {
            fromList = document.getElementById("lstSelectedSubCategories");
            toList = document.getElementById("lstSubCategory");
            if (toList.options.length > 0 && toList.options[0].value == 'temp')
            {
                toList.options.length = 0;
            }
            var sel = false;
            for (i=0;i<fromList.options.length;i++)
            {
                var current = fromList.options[i];
                if (current.selected)
                {
                    sel = true;
                    if (current.value == 'temp')
                    {
                    alert ('You cannot move this text!');
                    return;
                    }
                    txt = current.text;
                    val = current.value;
                    toList.options[toList.length] = new Option(txt,val);
                    fromList.options[i] = null;
                    RemoveSubCategoryFromHiddenValue(trim(val));
                    i--;
                }
            }
            if (!sel) alert ('You haven\'t selected any options!');
       }
       
       function copyAllSubCategories(){
            
            fromList = document.getElementById("lstSubCategory");
            toList = document.getElementById("lstSelectedSubCategories");
            for(i=0; i<fromList.options.length; i++)
            {
                //get value before it is appended
                var selectedvalue = fromList.options.item(i).value;
                toList.options[toList.length] = new Option(fromList.options.item(i).text,fromList.options.item(i).value);
                fromList.options[i] = null;
                //store selectedvalue to hidden
                StoreSubCategoryToHiddenValue(selectedvalue);
                i--;
            }
            selectNone(toList,fromList);
            setSize(toList,fromList);
        }
        
        function removeAllSubCategories(){
            fromList = document.getElementById("lstSelectedSubCategories");
            toList = document.getElementById("lstSubCategory");
            for(i=0; i<fromList.options.length; i++)
            {
                var selectedvalue = fromList.options.item(i).value;
                toList.options[toList.length] = new Option(fromList.options.item(i).text,fromList.options.item(i).value);
                fromList.options[i] = null;
                RemoveSubCategoryFromHiddenValue(selectedvalue);
                i--;
            }
            selectNone(toList,fromList);
            setSize(toList,fromList);
       }
    
        function copyASubCategory()
        {
           fromList = document.getElementById("lstSubCategory");
           toList = document.getElementById("lstSelectedSubCategories");
           var addIndex = fromList.selectedIndex;
           if(addIndex < 0)
              return;
           //get selected value before object is appended to destination list
           var selectedvalue = trim(fromList.options.item(addIndex).value);
           //object is appended
           toList.appendChild(fromList.options.item(addIndex));
           //store selected item to hidden value
           StoreSubCategoryToHiddenValue(selectedvalue);
           selectNone(toList,fromList);
           setSize(toList,fromList);
         }
        
        function removeASubCategory()
        {
           fromList = document.getElementById("lstSelectedSubCategories");
           toList = document.getElementById("lstSubCategory");
           var addIndex = fromList.selectedIndex;
           if(addIndex < 0)
              return;
           //get selected value before object is appended to destination list
           var selectedvalue = trim(fromList.options.item(addIndex).value);
           //object is appended
           toList.appendChild(fromList.options.item(addIndex));
           //remove selected item from hidden value
           RemoveSubCategoryFromHiddenValue(selectedvalue);
           selectNone(toList,fromList);
           setSize(toList,fromList);
         } 
        
        function CheckIfSubCategoryValueExists(selectedSubCategoryA, selectedvalue)
        {
            for (var i=0; i<selectedSubCategoryA.length; i++)
            {
                if (trim(selectedSubCategoryA[i]) == trim(selectedvalue))
                    return true;
            }
            return false;
        }

        function  StoreSubCategoryToHiddenValue(selectedvalue)
        {
           if (trim(document.getElementById("hdnSubCategory").value) != "")
           {
            var selectedSubCategory = trim(document.getElementById("hdnSubCategory").value);
            var selectedSubCategoryA =  selectedSubCategory.split(",");
            var exists = CheckIfSubCategoryValueExists(selectedSubCategoryA, selectedvalue);
            if  (!exists)
                 document.getElementById("hdnSubCategory").value = selectedSubCategory + "," + selectedvalue;               
           }
           else
                document.getElementById("hdnSubCategory").value = selectedvalue;
        }
        
        function RemoveSubCategoryFromHiddenValue(selectedvalue)
        {
           if (trim(document.getElementById("hdnSubCategory").value) != "")
           {
           
            var selectedSubCategory = trim(document.getElementById("hdnSubCategory").value);
            var selectedSubCategoryA = selectedSubCategory.split(",");
            var newString = "";
            for (var i=0; i<selectedSubCategoryA.length; i++)
            {
                if (selectedSubCategoryA[i]!=selectedvalue)
                {
                    if (newString == "")
                        newString = trim(selectedSubCategoryA[i]);
                    else
                        newString = newString + "," + trim(selectedSubCategoryA[i]);
                }
            }
            document.getElementById("hdnSubCategory").value = trim(newString);
           }
        }
        //------End Subcategory------//
                </script>

                <script language="javascript">

function Init() 
{ 
   if (window.XMLHttpRequest) { // Non-IE browsers 
      _req = new XMLHttpRequest(); 
   } 
   else if (window.ActiveXObject){ // IE 
      _req = new ActiveXObject("Microsoft.XMLHTTP"); 
   } 
} 

SendQuery=function(key, val) //get data 
{ 
   Init(); 
   //alert(key + "----" + val);  
   var url="../../GetAjaxData/GetSubCategoryData.aspx?" + key +"=" + val; 
   if(_req!=null) { 
      _req.onreadystatechange = processStateChange; 
      _req.open("GET", url, true); 
      _req.send(null); 
   } 
} 

processStateChange = function()
{ 
   if (_req.readyState == 4){// only if "OK" 
      if (_req.status == 200) { 
         if(_req.responseText=="") 
            return false; 
         else{ 
            eval(_req.responseText); 
         } 
      } 
   } 
} 

populateDDL = function(v,t,ddlID)
{ 
    _ddl = document.getElementById(ddlID); 
   var count = v.length; 
   // alert(v.length); 
   for (var i=0; i<v.length; ++i){ 
      var op = document.createElement("Option"); 
      op.innerHTML = t[i]; 
      op.value = v[i];  
      _ddl.appendChild(op); 
   } 
} 

RemoveFromList = function(v,t,ddlID,hiddenID)
{ 
    _ddl = document.getElementById(ddlID); 
   while (_ddl.childNodes.length >0){ 
      _ddl.removeChild(_ddl.childNodes[0]); 
   } 
   var count = v.length; 
   // alert(v.length); 
   for (var i=0; i<v.length; ++i){ 
      var op = document.createElement("Option"); 
      op.innerHTML = t[i]; 
      op.value = v[i];  
      _ddl.appendChild(op); 
   } 
   
   var lstTempRemoveSubCategory = document.getElementById("lstTempRemoveSubCategory")
   var tempLength = lstTempRemoveSubCategory.options.length;
   var lstSubCategory = document.getElementById("lstSubCategory");
   var lstSelectedSubCategories = document.getElementById("lstSelectedSubCategories");        
           
   for (var i=0; i<tempLength; ++i){
       for (var j=0; j<lstSubCategory.options.length; j++)
           {
              if (lstSubCategory.options.item(j).value == lstTempRemoveSubCategory.options.item(i).value)
                 {
                 lstSubCategory.options[j] = null;
                 }
           }   
       for (var k=0; k<lstSelectedSubCategories.options.length; k++)
        {
           if (lstSelectedSubCategories.options.item(k).value == lstTempRemoveSubCategory.options.item(i).value)
                 {
                 lstSelectedSubCategories.options[k] = null;
                 }
        }
   }   
} 

var setHiddenfieldValue= function(fieldName,val)
{ 
   document.getElementById(fieldName).value=val; 
} 

                </script>

            </div>
        </div>
    </form>
</body>
<!-- #EndTemplate -->
</html>
