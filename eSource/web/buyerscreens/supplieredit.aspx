<%@ Page Language="C#" AutoEventWireup="true" CodeFile="supplieredit.aspx.cs" Inherits="web_buyerscreens_supplierEdit" MaintainScrollPositionOnPostback="true" %>
<%@ Register  TagPrefix="EBid" TagName="TopNavSuppliers" Src="~/WEB/usercontrol/Buyer/TopNavSuppliers.ascx" %>
<%@ Register  TagPrefix="EBid" TagName="TopNav2" Src="~/WEB/usercontrol/Buyer/TopNav2.ascx" %>
<%@ Register  TagPrefix="EBid" TagName="LeftNavSuppliers" Src="~/WEB/usercontrol/Buyer/LeftNav.ascx" %>
<%@ Register  TagPrefix="EBid" TagName="GlobalLinksNav" Src="~/web/usercontrol/GlobalLinksNav.ascx" %>
<%@ Register  TagPrefix="EBid" TagName="Footer" Src="~/web/usercontrol/Footer.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >

<html xmlns="http://www.w3.org/1999/xhtml" style="height: 100%;">

<!-- #BeginTemplate "../templates/suppliers.dwt" -->

<head>
<meta http-equiv="Content-Language" content="en-us" />
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252" />
<link rel="stylesheet" type="text/css" href='<%= "../themes/"+Session["configTheme"]+"/css/style.css" %>' />
    <link rel="stylesheet" type="text/css" href='<%="../themes/"+Session["configTheme"]+"/css/style_buyer.css" %>' />
<script language="javascript" src="../include/generalJSFunctions.js" ></script>
<script language="javascript" type="text/javascript" src="../include/customValidation.js"></script>
<script language="javascript" src="../include/supplierRegistration.js"></script>
 <script language="javascript"> 
    var _req; 
</script> 
<!-- #BeginEditable "doctitle" -->
<!-- #EndEditable -->
    <link href='<%= "../themes/"+Session["configTheme"]+"/css/style.css" %>' rel="stylesheet" type="text/css" />
</head>

<body style="height: 100%;">
<form id="supplieredit" runat="server">
<div>
<div align="left">
	<table border="0" cellpadding="0" cellspacing="0" width="100%" id="page">
		
		<tr>
			<td valign=top>
			<table border="0" cellpadding="0" cellspacing="0" width=100%>
				<tr>
					<td>
					<div align="left" id="masthead">
						<EBid:GlobalLinksNav runat="server" id=GlobalLinksNav />
					</div>
					</td>
				</tr>
				<tr>
					<td>
					<EBid:TopNav2 runat="server" ID=TopNav2 />
					</td>
				</tr>
			</table>
			</td>
		</tr>
		<tr height="100%">
			<td>
			<table border="0" cellpadding="0" cellspacing="0" width="100%" height='100%'>
				<tr>
					<td id="relatedInfo">
					<EBid:LeftNavSuppliers runat="server" ID=LeftNavSuppliers />
					<div align="left">
<!-- #BeginEditable "commentArea" -->
						<table border="0" cellpadding="0" cellspacing="0" width="100%" id="table3">
							<tr>
								<td>
								<div align="left">
									<table border="0" cellpadding="0" cellspacing="0" width="100%" id="table4">
										<tr>
											<td>&nbsp;</td>
										</tr>
									</table>
								</div>
								</td>
							</tr>
						</table>
<!-- #EndEditable -->
					</div>
						</td>
					<td id="content" style="width: 100%">
					<!-- #BeginEditable "contentArea" -->
					<h1><br />Supplier Profile</h1>
					<table border="0" cellpadding="0" cellspacing="0" width="100%" id="itemDetails">
						<tr>
							<th >Company information</th>
						</tr>
						<tr>
							<td class="value">
							<table cellpadding=0 cellspacing=0 border=0 width=100%>
							<tr>
							<td style="border-width:0" width=20%>Company Name: 
							</td>
							<td style="border-width:0"><asp:Label runat="server" ID =lblCompanyName></asp:Label>
							</td>
							</tr>							
							</table>
							</td>
						</tr>
						<tr>
							<td class="evenCells">
							<table cellpadding=0 cellspacing=0 border=0 width=100%>
							<tr>
							<td style="border-width:0" width=20%>
							Address: (Head Office)
							</td>
							<td style="border-width:0"><asp:Label runat="server" ID =lblAddressHeadOffice></asp:Label>
							</td>
							</tr>
							</table>
							</td>
						</tr>
						<tr>
							<td class="value" >
							<table cellpadding=0 cellspacing=0 border=0 width=100%>
							<tr>
							<td style="border-width:0" width=20%>Telephone:</td>
							<td style="border-width:0"><asp:Label runat="server" ID =lblTelephone></asp:Label></td>
							</tr>
							</table>
							</td>
						</tr>
						<tr>
							<td class="evenCells" >
							<table cellpadding=0 cellspacing=0 border=0 width=100%>
							<tr>
							<td style="border-width:0" width=20%>Fax:</td>
							<td style="border-width:0" width=30%><asp:Label runat="server" ID =lblFax></asp:Label></td>
							<td style="border-width:0" width=20%>Extension:</td>
							<td style="border-width:0"><asp:Label runat="server" ID =lblExtension></asp:Label></td>
							</tr>
							</table>
							</td>
						</tr>
						<tr>
						    <td class="value">
						        <table cellpadding=0 cellspacing=0 border=0 width=100%>
							        <tr>
							            <td style="border-width:0" width=20%>
							            P.O. Box:
							            </td>
							            <td style="border-width:0" width=30%>
							                <asp:Label runat="server" ID=lblPOBox></asp:Label>
							            </td>
							            <td style="border-width:0" width=20%>
							            Postal Code:
							            </td>
							            <td style="border-width:0">
							                <asp:Label runat="server" ID=lblPostalCode></asp:Label>
							            </td>
							        </tr>
							        <tr>
							            <td style="border-width:0" width=20%>
							                Email:
							            </td>
							            <td style="border-width:0">
							                <asp:Label runat="server" ID=lblEmail></asp:Label>
							            </td>
							        </tr>
							    </table>
						    </td>
						</tr>
						</table>            
					   <table border="0" cellpadding="0" cellspacing="0" width="100%" id="ClearGrid" >
							 <tr>
								            <th colspan=6>Supplier Type:</th>
								           
							            </tr>
							            <tr>
							                <td class="value" colspan=6><asp:DropDownList runat="server" ID="ddlSupplierType" ></asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvSupplierType" runat="server" ControlToValidate="ddlSupplierType"
                                                    Display="None" ErrorMessage="Supplier Type is a required field." SetFocusOnError="True"></asp:RequiredFieldValidator></td>
							            </tr>
							            							<tr>
								
								<th colspan=6> Category</th>
								
							</tr>
							<tr>
								
								<td class="value" colspan=6>
								<table border="0" cellpadding="0" cellspacing="0" width="100%" id="table1">
												<tr>
													<td width="167" align="right">
													<asp:ListBox runat="server" ID="lstCategories" style="width:250; height:150" SelectionMode="Multiple"  ></asp:ListBox>
													</td>
													<td width="82" align="right">
													<p align="center" style="text-align: center">
													<asp:TextBox runat="server" id="hdnCategories" Width=0 Height=0/>
													<input type="button" value=">" id="btnSelectCategory" runat="server" /><br />
													<input type="button" value=">>" id="btnSelectAllCategories" runat="server"  /><br />
													<input type="button" value="<<" id="btnDeSelectCategory" runat="server"  /><br />
													<input type="button" value="<" id="btnDeSelectAllCategories" runat="server"  />
													</p>
													</td>
													<td align="right">
													<p align="center">
                                                    <asp:ListBox runat="server" ID="lstSelectedCategories" style="width:250; height:150" SelectionMode="Multiple" ></asp:ListBox>
                                                    <asp:RequiredFieldValidator ID="rfvCategories" runat="server" ErrorMessage="Categories is a required field." ControlToValidate="hdnCategories" SetFocusOnError="True" Display="None"></asp:RequiredFieldValidator>
                                                    </p>
												</td>
							                    </tr>
											</table>
					</td>
								
							</tr>

							<tr>
								<th colspan=6>Sub-category</th>
							</tr>
							<tr>
								
								
								<td class="value" colspan=6> 
					<table border="0" cellpadding="0" cellspacing="0" width="100%" id="table2">
												<tr>
													<td width="167" align="right">
													<asp:ListBox runat="server" ID="lstSubCategory" style="width:250; height:150" SelectionMode="Multiple" ></asp:ListBox>
													</td>
													<td width="82" align="right">
													<p align="center" style="text-align: center">
													<asp:TextBox runat="server" id="hdnSubCategory" Width=0 Height=0/>
													<input type="button" value=">" id="btnSelectSubCategory" runat="server"  /><br />
													<input type="button" value=">>" id="btnSelectAllSubCategories" runat="server"  /><br />
													<input type="button" value="<<" id="btnDeSelectAllSubCategories" runat="server"  /><br />
													<input type="button" value="<" id="btnDeSelectSubCategory" runat="server"  />
													</p>
													</td>
													<td align="right">
													<p align="center">
                                                    <asp:ListBox runat="server" ID="lstSelectedSubCategories" style="width:250; height:150" SelectionMode="Multiple" onDblClick="removeABrand();"></asp:ListBox>
                                                    <asp:RequiredFieldValidator ID="rfvSubCategories" runat="server" ErrorMessage="Sub-categories is a required field." ControlToValidate="hdnSubCategory" SetFocusOnError="True" Display="None"></asp:RequiredFieldValidator>
                                                    </p>
												</td>
							                    </tr>
											</table>
					</td>
							</tr>
					
							<tr>
								<th colspan=3>ISO Classification:</th>
								<th colspan=3>PCAB Class:</th>
							</tr>
							<tr>
								<td class="value" colspan=3>
								 <asp:CheckBoxList ID="chkISOStandard_white" runat="server">
                                </asp:CheckBoxList>&nbsp;
								</td>
								<td class="value" colspan=3><asp:DropDownList runat="server" ID="ddlPCABClass" ></asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvPCABClass" runat="server" ControlToValidate="ddlPCABClass"
                                        Display="None" ErrorMessage="PCAB Class is a required field." SetFocusOnError="True"></asp:RequiredFieldValidator></td>
							</tr>
							<tr>
								<th colspan=3>Brands:</th>
								<th colspan=3>Services:</th>
							</tr>
							<tr>
								<td class="value" colspan=3 bordercolor="#FFFFFF">
								    <table border="0" cellpadding="0" cellspacing="0" width="100%" id="table8">
												<tr>
													<td  align="right">
													<asp:ListBox runat="server" ID="lstBrandsCarried" style="width:150; height:150" SelectionMode="Multiple" onDblClick="copyABrand();"></asp:ListBox>
													</td>
													<td align="right">
													<p align="center" style="text-align: center">
													<asp:TextBox runat="server" id="hdnBrands" Width=0 Height=0/>
													<input type="button" value=">" id="btnSelectBrand" runat="server" onClick="copyToBrandList();" /><br />
													<input type="button" value=">>" id="btnSelectAllBrands" runat="server" onclick="copyAllBrands();" /><br />
													<input type="button" value="<<" id="btnDeSelectBrand" runat="server" onclick="removeAllBrands();" /><br />
													<input type="button" value="<" id="btnDeSelectAllBrands" runat="server" onClick="removeFromBrandList();" />
													</p>
													</td>
													<td align="right">
													<p align="center">
                                                    <asp:ListBox runat="server" ID="lstSelectedBrandsCarried" style="width:150; height:150" SelectionMode="Multiple" onDblClick="removeABrand();"></asp:ListBox>
                                                    <asp:RequiredFieldValidator ID="rfvBrandsCarried" runat="server" ErrorMessage="Brands Carried is a required field." ControlToValidate="hdnBrands" SetFocusOnError="True" Display="None"></asp:RequiredFieldValidator>
                                                    </p>
												</td>
							                    </tr>
											</table>
                                </td>
								<td class="value" colspan=3>
                                   <table border="0" cellpadding="0" cellspacing="0" width="100%" id="table10">
												<tr>
													<td  align="right">
													
<asp:ListBox runat="server" ID=lstServicesOffered style="width:150; height:150" SelectionMode="Multiple" onDblClick="copyAServiceOffered();"></asp:ListBox></td>
													<td  align="right">
													<p align="center" style="text-align: center">
													<asp:TextBox runat="server" id="hdnServices" width=0/>
													<input type="button" value=">" id="btnSelectServiceOffered" runat="server" onclick="copyToServiceOfferedList();" /><br />
													<input type="button" value=">>" id="btnSelectAllServicesOffered" runat="server" onclick="copyAllServicesOffered();" /><br />
													<input type="button" value="<<" id="btnDeSelectAllServicesOffered" runat="server" onclick="removeAllServicesOffered();" /><br />
													<input type="button" value="<" id="btnDeSelectServiceOffered" runat="server" onclick="removeFromServiceOfferedList();" />
													</p>
													</td>
													<td align="right">
													<p align="center">

<asp:ListBox runat="server" ID=lstSelectedServicesOffered style="width:150; height:150" SelectionMode="Multiple" onDblClick="removeAServiceOffered();"></asp:ListBox>
<asp:RequiredFieldValidator ID="rfvServicesOffered" runat="server" ErrorMessage="Services Offered is a required field." ControlToValidate="hdnServices" SetFocusOnError="true" Display="None"></asp:RequiredFieldValidator>
</p>
												</td>
							</tr>
											</table>													
                                </td>
							</tr>
							<tr>
								<th colspan=3>Locations</th>
								<th colspan=3>Items:</th>
							</tr>
							<tr>
							    <td class="value" colspan=3>
							   <table border="0" cellpadding="0" cellspacing="0" width="100%" id="table12">
												<tr>
													<td  align="right">
													
<asp:ListBox runat="server" ID=lstLocation style="width:150; height:150" SelectionMode="Multiple" onDblClick="copyALocation();"></asp:ListBox></td>
													<td  align="right">
													<p align="center" style="text-align: center">
													<asp:TextBox runat="server" id="hdnLocation" width=0 />
													<input type="button" value=">" id="btnSelectLocation" runat="server" onclick="copyToLocationList();" /><br />
													<input type="button" value=">>" id="btnSelectAllLocations" runat="server" onclick="copyAllLocations();"  /><br />
													<input type="button" value="<<" id="btnDeSelectAllLocations" runat="server" onclick="removeAllLocations();"  /><br />
													<input type="button" value="<" id="btnDeSelectLocation" runat="server" onclick="removeFromLocationList();" />
													</p>
													</td>
													<td align="right">
													<p align="center">
<asp:ListBox runat="server" ID=lstSelectedLocation style="width:150; height:150" SelectionMode="Multiple" onDblClick="removeALocation();"></asp:ListBox>
<asp:RequiredFieldValidator ID="rfvLocation" runat="server" ErrorMessage="Location is a required field." ControlToValidate="hdnLocation" SetFocusOnError="true" Display="None"></asp:RequiredFieldValidator>
												</p>
												</td>
							</tr>
											</table>
                                </td>
								<td class="value" colspan=3>
								<table border="0" cellpadding="0" cellspacing="0" width="100%" id="tableA">
												<tr>
													<td  align="right">
													
<asp:ListBox runat="server" ID=lstItemsCarried style="width:150; height:150" SelectionMode="Multiple" onDblClick="copyAnItem();"></asp:ListBox></td>
													<td  align="right">
													<p align="center" style="text-align: center">
													<asp:TextBox runat="server" id="hdnItems" width=0 />
													<input type="button" value=">" id="btnSelectItemCarried" runat="server" onClick="copyToItemList();"/><br />
													<input type="button" value=">>" id="btnSelectAllItemsCarried" runat="server" onClick="copyAllItems();"/><br />
													<input type="button" value="<<" id="btnDeSelectAllItemsCarried" runat="server" onClick="removeAllItems();"/><br />
													<input type="button" value="<" id="btnDeSelectItemCarried" runat="server" onClick="removeFromItemList();"/>
													</p>
													</td>
													<td align="right">
													<p align="center">

<asp:ListBox runat="server" ID=lstSelectedItemsCarried style="width:150; height:150" SelectionMode="Multiple" onDblClick="removeAnItem();"></asp:ListBox>
<asp:RequiredFieldValidator ID="rfvItemsCarried" runat="server" ErrorMessage="Items Carried is a required field." ControlToValidate="hdnItems" SetFocusOnError="true" Display="None"></asp:RequiredFieldValidator>
</p>
												</td>
							</tr>
											</table>
								</td>
							</tr>
						</table>
						<br />
						<table border="0" cellpadding="0" cellspacing="0" width="100%" id="actions">
						<tr>
							<td><asp:LinkButton runat="server" ID="btnSave" Text="Save" OnClick="btnSave_Click" ></asp:LinkButton><asp:LinkButton runat="server" ID="btnOK" Text="Ok" PostBackUrl="~/web/buyerscreens/index.aspx"  CausesValidation="false"  ></asp:LinkButton></td>
						</tr>
						
					</table>
					<br />
					<br />
					<!-- #EndEditable --></td>
				</tr>
			</table>
			</td>
		</tr>
		<tr>
			<td id="footer"><EBid:Footer runat="server" ID="Footer1" /></td>
		</tr>
		</table>
		<asp:ListBox runat="server" ID="lstTempRemoveSubCategory"  width=0></asp:ListBox>
		<asp:CustomValidator ID="cuvValidate" runat="server" ClientValidationFunction="ValidatorIndividualAlert(this, args);" Display="None"></asp:CustomValidator>
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
                    StoreCategoryToHiddenValue(trim(val));
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
                StoreCategoryToHiddenValue(selectedvalue);
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
           StoreCategoryToHiddenValue(selectedvalue);
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

        function  StoreCategoryToHiddenValue(selectedvalue)
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
