<%@ Page Language="C#" AutoEventWireup="true" CodeFile="registration2.aspx.cs" Inherits="web_buyerscreens_registration2" %>
<%@ Register  TagPrefix="EBid" TagName="TopNavSuppliers" Src="~/WEB/usercontrol/Buyer/TopNavSuppliers.ascx" %>
<%@ Register  TagPrefix="EBid" TagName="TopNav2" Src="~/WEB/usercontrol/Buyer/TopNav2.ascx" %>
<%@ Register  TagPrefix="EBid" TagName="LeftNavSuppliers" Src="~/WEB/usercontrol/Buyer/LeftNav.ascx" %>
<%@ Register  TagPrefix="EBid" TagName="GlobalLinksNav" Src="~/web/usercontrol/GlobalLinksNav.ascx" %>
<%@ Register  TagPrefix="EBid" TagName="Footer" Src="~/web/usercontrol/Footer.ascx" %>

<html xmlns="http://www.w3.org/1999/xhtml" style="height: 100%;">

<!-- #BeginTemplate "../templates/suppliers.dwt" -->

<head>
<meta http-equiv="Content-Language" content="en-us" />
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252" />
<link rel="stylesheet" type="text/css" href='<%= "../themes/"+Session["configTheme"]+"/css/style.css" %>' />
    <link rel="stylesheet" type="text/css" href='<%="../themes/"+Session["configTheme"]+"/css/style_buyer.css" %>' />
<script language="javascript" src="../include/generalJSFunctions.js" ></script>
<script language="javascript" src="../include/supplierRegistration.js"></script>
<script language="javascript" type="text/javascript" src="../include/customValidation.js"></script>
<!-- #BeginEditable "doctitle" -->
<!-- #EndEditable -->
</head>

<body style="height: 100%;">

<div>
<div align="left">
	<table border="0" cellpadding="0" cellspacing="0" width="100%" id="page">
		<form runat="server" id="registration2">
		<tr>
			<td valign=top>
			<table border="0" cellpadding="0" cellspacing="0" width=100%>
				<tr>
					<td>
					<div align="left" id="masthead">
						<EBid:GlobalLinksNav runat="server" id="GlobalLinksNav" />
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
		<tr height="100%">
			<td>
			<table border="0" cellpadding="0" cellspacing="0" width="100%" >
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
					<td id="content">
					<!-- #BeginEditable "contentArea" -->
					<h1><br />Register</h1>
					<p>Select all items that apply. Scroll down to select more 
					brands. Press ctrl+click to make multiple selections.</p>
					<table border="0" cellpadding="0" cellspacing="0" width="100%" id="itemDetails">
						<tr>
							<th>Brands Carried </th>
						</tr>
						<tr>
							<td width="101%" class="value">
											&nbsp;<table border="0" cellpadding="0" cellspacing="0" width="100%" id="table8">
												<tr>
													<td width="167" align="right">
													<asp:ListBox runat="server" ID="lstBrandsCarried" style="width:250; height:150" SelectionMode="Multiple" onDblClick="copyABrand();"></asp:ListBox>
													</td>
													<td width="82" align="right">
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
                                                    <asp:ListBox runat="server" ID="lstSelectedBrandsCarried" style="width:250; height:150" SelectionMode="Multiple" onDblClick="removeABrand();"></asp:ListBox>
                                                    <asp:RequiredFieldValidator ID="rfvBrandsCarried" runat="server" ErrorMessage="Brands Carried is a required field." ControlToValidate="hdnBrands" SetFocusOnError="True" Display="None"></asp:RequiredFieldValidator>
                                                    </p>
												</td>
							                    </tr>
											</table>
											</td>
						</tr>
						</table>
					<table border="0" cellpadding="0" cellspacing="0" width="100%" id="itemDetails" >
						<tr>
							<th>ISO Standard</th>
						</tr>
						<tr>
							<td width="98%" class="value">Check all that apply</td>
						</tr>
						<tr>
							<td width="98%" class="value">
                                <asp:CheckBoxList ID="chkISOStandard_white" runat="server">
                                </asp:CheckBoxList>
							</td>
						</tr>
						
						</table>
					<table border="0" cellpadding="0" cellspacing="0" width="100%" id="itemDetails">
						<tr>
							<th>Items Carried</th>
						</tr>
						<tr>
							<td class="value">
											<table border="0" cellpadding="0" cellspacing="0" width="100%" id="tableA">
												<tr>
													<td width="167" align="right">
													
<asp:ListBox runat="server" ID=lstItemsCarried style="width:250; height:150" SelectionMode="Multiple" onDblClick="copyAnItem();"></asp:ListBox></td>
													<td width="82" align="right">
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

<asp:ListBox runat="server" ID=lstSelectedItemsCarried style="width:250; height:150" SelectionMode="Multiple" onDblClick="removeAnItem();"></asp:ListBox>
<asp:RequiredFieldValidator ID="rfvItemsCarried" runat="server" ErrorMessage="Items Carried is a required field." ControlToValidate="hdnItems" SetFocusOnError="true" Display="None"></asp:RequiredFieldValidator>
</p>
												</td>
							</tr>
											</table>
											</td>
						</tr>
						</table>
					<table border="0" cellpadding="0" cellspacing="0" width="100%" id="itemDetails">
						<tr>
							<th>Services Offered</th>
						</tr>
						<tr>
							<td class="value">
											<table border="0" cellpadding="0" cellspacing="0" width="100%" id="table10">
												<tr>
													<td width="167" align="right">
													
<asp:ListBox runat="server" ID=lstServicesOffered style="width:250; height:150" SelectionMode="Multiple" onDblClick="copyAServiceOffered();"></asp:ListBox></td>
													<td width="82" align="right">
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

<asp:ListBox runat="server" ID=lstSelectedServicesOffered style="width:250; height:150" SelectionMode="Multiple" onDblClick="removeAServiceOffered();"></asp:ListBox>
<asp:RequiredFieldValidator ID="rfvServicesOffered" runat="server" ErrorMessage="Services Offered is a required field." ControlToValidate="hdnServices" SetFocusOnError="true" Display="None"></asp:RequiredFieldValidator>
</p>
												</td>
							</tr>
											</table>
											</td>
						</tr>
						</table>
					<table border="0" cellpadding="0" cellspacing="0" width="100%" id="itemDetails">
						<tr>
							<th>Location</th>
						</tr>
						<tr>
							<td class="value">
											<table border="0" cellpadding="0" cellspacing="0" width="100%" id="table12">
												<tr>
													<td width="167" align="right">
													
<asp:ListBox runat="server" ID=lstLocation style="width:250; height:150" SelectionMode="Multiple" onDblClick="copyALocation();"></asp:ListBox></td>
													<td width="82" align="right">
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
<asp:ListBox runat="server" ID=lstSelectedLocation style="width:250; height:150" SelectionMode="Multiple" onDblClick="removeALocation();"></asp:ListBox>
<asp:RequiredFieldValidator ID="rfvLocation" runat="server" ErrorMessage="Location is a required field." ControlToValidate="hdnLocation" SetFocusOnError="true" Display="None"></asp:RequiredFieldValidator>
												</p>
												</td>
							</tr>
											</table>
											</td>
						</tr>
						</table>
					<table border="0" cellpadding="0" cellspacing="0" width="100%" id="itemDetails">
						<tr>
							<th>Philippine Contractor Association Board (PCAB) Class</th>
						</tr>
						<tr>
							<td class="value">Select Class:
							<asp:DropDownList runat="server" ID="ddlPCABClass" style="width:150"></asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvPCABClass" runat="server" ErrorMessage="Philippine Contractor Association Board (PCAB) Class is a required field." ControlToValidate="ddlPCABClass" SetFocusOnError="true" Display="None" ></asp:RequiredFieldValidator>
							</td>
						</tr>
						</table>
					<br />
					<table border="0" cellpadding="0" cellspacing="0" width="100%" id="actions">
						<tr>
						<td align="left" width="75%"><asp:Label runat="server" CssClass="messagelabels" ID="lblMessage"></asp:Label></td>
						<td align="right" width="25%"><asp:LinkButton runat="server" ID="btnRegister" OnClick="btnRegister_Click">Register</asp:LinkButton></td>
						</tr>
					</table>
					<p>&nbsp;<!-- #EndEditable --></td>
				</tr>
			</table>
			</td>
		</tr>
		<tr>
			<td id="footer"><EBid:Footer runat="server" ID="Footer" /></td>
		</tr>
		<input type="hidden" runat="server" id="hdnXMLFileName" />
		<asp:CustomValidator ID="cuvValidate" runat="server" ClientValidationFunction="ValidatorIndividualAlert(this, args);" Display="None"></asp:CustomValidator>
		
		</form>
	</table>
</div>
&nbsp;</div>

</body>

<!-- #EndTemplate -->

</html>
