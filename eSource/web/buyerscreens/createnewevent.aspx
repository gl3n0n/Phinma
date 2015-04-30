<%@ Page Language="C#" AutoEventWireup="true" CodeFile="createnewevent.aspx.cs" Inherits="web_buyer_screens_createNewItem" EnableEventValidation="false" MaintainScrollPositionOnPostback="true" %>

<%@ Register Src="../usercontrol/commentlist.ascx" TagName="commentlist" TagPrefix="uc1" %>
<%@ Register Assembly="Calendar" Namespace="CalendarControl" TagPrefix="cc1" %>
<%@ Register TagPrefix="EBid" TagName="LeftNav" Src="~/WEB/usercontrol/Buyer/LeftNav.ascx" %>
<%@ Register TagPrefix="EBid" TagName="TopNavBids" Src="~/WEB/usercontrol/Buyer/TopNavBids.ascx" %>
<%@ Register TagPrefix="EBid" TagName="TopNav2" Src="~/WEB/usercontrol/Buyer/TopNav2.ascx" %>
<%@ Register TagPrefix="EBid" TagName="GlobalLinksNav" Src="~/web/usercontrol/GlobalLinksNav.ascx" %>
<%@ Register TagPrefix="EBid" TagName="Footer" Src="~/web/usercontrol/Footer.ascx" %>
<%@ Register TagPrefix="EBid" TagName="CommentBox" Src="~/web/usercontrol/CommentBox.ascx" %>
<html xmlns="http://www.w3.org/1999/xhtml" style="height: 100%;">
<head>
    <%@ Register TagPrefix="EBid" TagName="jqueryLoader" Src="../usercontrol/jQuery_loader.ascx" %>
    <EBid:jqueryLoader runat="server" ID="jqueryLoader1" />
	<title id="PageTitle" runat="server"></title>
	<meta http-equiv="Content-Language" content="en-us" />
	<meta http-equiv="Content-Type" content="text/html; charset=windows-1252" /> 
	<link href='<%= "../themes/"+Session["configTheme"]+"/css/style.css" %>' rel="stylesheet" type="text/css" />
	<link href='../themes/<%= Session["configTheme"] %>/css/style_buyer.css' rel="stylesheet" type="text/css" />
	<script type="text/javascript" src="../include/util.js"></script>
	<script type="text/javascript" src="../include/events.js"></script>
	<link href='<%= "../themes/"+Session["configTheme"]+"/css/style.css" %>' rel="stylesheet" type="text/css" />
	<link href='<%= "../themes/"+Session["configTheme"]+"/css/style.css" %>' rel="stylesheet" type="text/css" />
	<script type="text/javascript" src="../include/generalJSFunctions.js"></script>
	<script type="text/javascript">
	<!--
	function ShowPHPConversion()
	{        
		var cb = document.getElementById("ddlCurrency");
		var tbPhp = document.getElementById("txtPHPCost");
		
		if (cb != null)
		{              
			var start = cb.value.substring(0,3);
			if (start != "PHP")
			{   
				tbPhp.style.visibility = 'visible';
			}
			else
			{
				tbPhp.style.visibility = 'hidden';
			}
			ComputePHPConversion();
		}        
	}
	
	function ComputePHPConversion()
	{
		var cb = document.getElementById("ddlCurrency");
		var tbPhp = document.getElementById("txtPHPCost");
		var tbTotal = document.getElementById("txtTotalEventPrice");
		var arr;        
		if ((cb) && (tbPhp))
		{
			arr = cb.value.split(":");            
			var v1 = tbTotal.value;
			var v2 = arr[1];                  
			tbPhp.value = AddComma(roundOff(removeCommas(v1) * removeCommas(v2), 2));
			tbPhp.value += " PHP";            
		}        
	}

	function VSFlstSupplierB() 
    {
        var vsf = document.getElementById("VSF");
        var vsfval = vsf.options[vsf.selectedIndex].value;

        if (vsfval != 0) {
            //alert(vsfval);
        }
    }
	//-->
	</script>
    <style type="text/css">
        .auto-style2 {
            width: 130px;
        }
    </style>
</head>
<body onload="ShowPHPConversion(); VSFlstSupplierB();">
	<form runat="server" id="frmBids" defaultbutton="btnSubmit" defaultfocus="txtItemDesc">
		<div align="left">
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
								<td id="relatedInfo" style="width: 250px">
									<div align="left">
										<EBid:LeftNav runat="server" ID="LeftNav" />
									</div>
									<h2>
										Comments</h2>
									<div>
										<uc1:commentlist ID="Commentlist1" runat="server" />
									</div>
									<br />
								</td>
								<td id="content">
									<table border="0" cellpadding="0" cellspacing="0" width="100%">
										<tr>
											<td valign="top">
												<table border="0" cellpadding="0" cellspacing="0" width="100%">
													<tr>
														<td>
															<h1>
																<br />
																Create New Bid Event
															</h1>
                                                <script type="text/javascript">
                                                    $(document).ready(function () {
                                                        $('#relatedInfo').hide();
                                                    });
                                                 </script>
															<p>
																Click <b>"Draft"</b> to save the bid event as draft. Click <b>"Submit"</b> to submit the event for approval if the <i>Total Event Cost</i> is equal or more than 2,000,000 (Php) or if the invited participants is less than 3, and publish the event to vendors
																if the <i>Total Event Cost</i> is less than 2,000,000 (Php).
															</p>
												<p><a href="javascript:void(0)" onclick="$('#relatedInfo').toggle()">Show/Hide Left Panel</a><br><asp:Label ID="lblMessage" runat="server" Font-Size="11px" ForeColor="red"></asp:Label></p>
															<table border="0" cellpadding="0" cellspacing="0" width="100%" id="itemDetails">
																<tr>
																	<th colspan="2">
																		&nbsp;Bid Event</th>
																</tr>
																<tr>
																	<td style="padding-top: 9px;" class="auto-style2">
																		Reference No.:</td>
																	<td class="value" style="padding-top: 9px; padding-left: 8px;">
																		<asp:Label ID="lblRefNo" runat="server" Text="NONE" ForeColor="Gray"></asp:Label></td>
																</tr>
																<tr>
																	<td style="padding-top: 9px;" class="auto-style2">
																		Description:</td>
																	<td class="value">
																		<asp:TextBox runat="server" ID="txtItemDesc" Width="350px" MaxLength="200"></asp:TextBox>
																		<asp:RequiredFieldValidator ID="rfvItemDescription" runat="server" ErrorMessage="*" ControlToValidate="txtItemDesc" Display="Dynamic" SetFocusOnError="True"></asp:RequiredFieldValidator>
																	</td>
																</tr>
																<tr>
																	<td style="padding-top: 9px;" class="auto-style2">
																		Requestor:</td>
																	<td class="value" style="height: 20px">
																		<asp:TextBox runat="server" ID="txtRequestor" Width="250px" MaxLength="100"></asp:TextBox>
																		<asp:RequiredFieldValidator ID="rfvRequestor" runat="server" ErrorMessage="*" ControlToValidate="txtRequestor" Display="Dynamic" SetFocusOnError="True"></asp:RequiredFieldValidator>
																	</td>
																</tr>
																<tr style="display:none">
																	<td style="padding-top: 9px;" class="auto-style2">
																		PR Number:</td>
																	<td class="value">
																		<asp:TextBox runat="server" ID="txtPRNumber" Width="250px" MaxLength="10" Text="0"></asp:TextBox>
																		<asp:RequiredFieldValidator ID="rfvPRNumber" runat="server" ErrorMessage="*" ControlToValidate="txtPRNumber" Display="Dynamic" SetFocusOnError="True"></asp:RequiredFieldValidator>
																	</td>
																</tr>
																<tr style="display:none">
																	<td style="padding-top: 9px;" class="auto-style2">
																		PR Sub-line Number:</td>
																	<td class="evenCells">
																		<asp:TextBox runat="server" ID="txtPRSubLineNumber" Width="250px" MaxLength="10" Text="0"></asp:TextBox>
																		<asp:RequiredFieldValidator ID="rfvPRSubLineNumber" runat="server" ErrorMessage="*" ControlToValidate="txtPRSubLineNumber" Display="Dynamic" SetFocusOnError="True"></asp:RequiredFieldValidator>
																	</td>
																</tr>
																<tr style="display:none">
																	<td style="padding-top: 9px;" class="auto-style2">
																		PR Date:</td>
																	<td class="value">
																		<cc1:JSCalendar ID="clndrPRDate" runat="server" ScriptsBasePath="../../calendar/" EnableViewState="true" ImageURL="../../calendar/img.gif" DateFormat="MM/dd/yyyy" Width="178px" ReadOnly="false" MaxLength="10"
																			TabIndex="-1"></cc1:JSCalendar>
																		&nbsp;<asp:Label ID="lblExDate1" runat="server" Text="eg. 01/31/2007" ForeColor="red" Font-Size="10px" />
																	</td>
																</tr>
																<tr>
																	<td style="padding-top: 9px;" class="auto-style2">
																		Company:</td>
																	<td class="value">
                                                                        <%--<asp:DropDownList runat="server" ID="ddlCompany" Width="250px" >
                                                                            <asp:ListItem Selected="True" Value="0"></asp:ListItem>
																		</asp:DropDownList>--%>
																		<asp:DropDownList runat="server" ID="ddlCompany" Enabled="False" Width="250px" DataSourceID="dsCompanies" DataTextField="Company" DataValueField="CompanyId" AutoPostBack="True" OnDataBound="ddlCompany_DataBound">
																		</asp:DropDownList>
																		<asp:SqlDataSource ID="dsCompanies" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>' SelectCommand="s3p_EBid_GetCompanies" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
																		<asp:RequiredFieldValidator ID="rfvCompany" runat="server" ErrorMessage="*" ControlToValidate="ddlCompany" Display="Dynamic" SetFocusOnError="True"></asp:RequiredFieldValidator>
																	</td>
																</tr>
																<tr style="display:none">
																	<td style="padding-top: 9px;" class="auto-style2">
																		Group/Department/Section:</td>
																	<td class="value">
                                                                        <asp:DropDownList runat="server" ID="ddlGroupDeptSec">
                                                                            <asp:ListItem Selected="True" Value="0"></asp:ListItem>
																		</asp:DropDownList>
																		<%--<asp:DropDownList runat="server" ID="ddlGroupDeptSec" Width="350px" DataSourceID="dsGroups" DataTextField="GroupDepartment" DataValueField="GroupDeptSecId">
																		</asp:DropDownList>
																		<asp:SqlDataSource ID="dsGroups" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>' SelectCommand="sp_GetAllGroupsAndDepartments" SelectCommandType="StoredProcedure">
																			<SelectParameters>
																				<asp:ControlParameter ControlID="ddlCompany" DefaultValue=" " Name="CompanyId" PropertyName="SelectedValue" />
																			</SelectParameters>
																		</asp:SqlDataSource>
																		<asp:RequiredFieldValidator ID="rfvGroupDeptSec" runat="server" ErrorMessage="*" ControlToValidate="ddlGroupDeptSec" Display="Dynamic" SetFocusOnError="True"></asp:RequiredFieldValidator>--%>
																	</td>
																</tr>
																<%--<tr>
																	<td style="padding-top: 9px;" class="auto-style2">
																		Main Category:</td>
																	<td class="value">
																		<asp:DropDownList runat="server" ID="ddlMainCategory" Width="250px" DataSourceID="dsMainCategories" DataTextField="MainCategoryName" DataValueField="MainCategoryId" AutoPostBack="True" OnSelectedIndexChanged="ddlMainCategory_SelectedIndexChanged"
																			OnDataBound="ddlMainCategory_DataBound ">
																		</asp:DropDownList><asp:SqlDataSource ID="dsMainCategories" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>' SelectCommand="s3p_EBid_GetAllProductMainCategory" SelectCommandType="StoredProcedure">
																		</asp:SqlDataSource>
																		<asp:RequiredFieldValidator ID="rfvMainCategory" runat="server" ErrorMessage="*" ControlToValidate="ddlMainCategory" Display="Dynamic" SetFocusOnError="True"></asp:RequiredFieldValidator>
																	</td>
																</tr>--%>
																<tr>
																	<td style="padding-top: 9px;" class="auto-style2">
																		Category:</td>
																	<td class="value">
																		<asp:DropDownList runat="server" ID="ddlCategory" Width="250px" DataSourceID="dsCategories" DataTextField="CategoryName" DataValueField="CategoryId" AutoPostBack="True" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged" OnDataBound="ddlCategory_DataBound" >
																		</asp:DropDownList>
                                                                        
                                                                        <%--<asp:SqlDataSource ID="dsCategories" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>' SelectCommand="s3p_EBid_GetAllProductCategory" SelectCommandType="StoredProcedure" 
																			FilterExpression="MainCategoryId = '{0}'">
																			<FilterParameters>
																				<asp:ControlParameter ControlID="ddlMainCategory" DefaultValue=" " Name="MainCategoryId" PropertyName="SelectedValue" />
																			</FilterParameters>
																		</asp:SqlDataSource>--%>
                                                                        <asp:SqlDataSource ID="dsCategories" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>' SelectCommand="s3p_EBid_GetAllProductCategory" SelectCommandType="StoredProcedure">
																			<%--<FilterParameters>
																				<asp:ControlParameter ControlID="ddlMainCategory" DefaultValue=" " Name="MainCategoryId" PropertyName="SelectedValue" />
																			</FilterParameters>--%>
																		</asp:SqlDataSource>
																		<asp:RequiredFieldValidator ID="rfvCategory" runat="server" ErrorMessage="*" ControlToValidate="ddlCategory" Display="Dynamic" SetFocusOnError="True"></asp:RequiredFieldValidator>
																	</td>
																</tr>
																<tr>
																	<td style="padding-top: 9px;" class="auto-style2">
																		Sub Category:</td>
																	<td class="value">
																		<asp:DropDownList runat="server" ID="ddlSubCategory" Width="250px" DataSourceID="dsSubCategories" DataTextField="SubCategoryName" DataValueField="SubCategoryId" AutoPostBack="true" OnDataBound="ddlSubCategory_DataBound">
																		</asp:DropDownList>
																		<asp:SqlDataSource ID="dsSubCategories" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>' SelectCommand="s3p_EBid_GetAllSubCategories" SelectCommandType="StoredProcedure"
																			FilterExpression="CategoryId = '{0}'">
																			<FilterParameters>
																				<%--<asp:ControlParameter ControlID="ddlMainCategory" DefaultValue=" " Name="MainCategoryId" PropertyName="SelectedValue" />--%>
																				<asp:ControlParameter ControlID="ddlCategory" DefaultValue=" " Name="CategoryId" PropertyName="SelectedValue" />
																			</FilterParameters>
																		</asp:SqlDataSource>
																	</td>
																</tr>
																<tr>
																	<td style="padding-top: 9px;" class="auto-style2">
																		Bid Submission Deadline:</td>
																	<td class="value">
																		<cc1:JSCalendar ID="clndrDeadline" runat="server" ImageURL="../../calendar/img.gif" EnableViewState="true" ScriptsBasePath="../../calendar/" DateFormat="MM/dd/yyyy" Width="178px" ReadOnly="false" MaxLength="10"
																			TabIndex="-1"></cc1:JSCalendar>
																		&nbsp;<asp:Label ID="lblExDate5" runat="server" Text="eg. 01/31/2007" ForeColor="red" Font-Size="10px" /><br />
																		&nbsp;<asp:TextBox ID="txtDeadlineHH" runat="server" MaxLength="2" Width="40px">00</asp:TextBox>
																		:
																		<asp:TextBox ID="txtDeadlineMM" runat="server" MaxLength="2" Width="40px">00</asp:TextBox>
																		:
																		<asp:TextBox ID="txtDeadlineSS" runat="server" MaxLength="2" Width="40px">00</asp:TextBox>&nbsp;<asp:DropDownList ID="ddlDeadline" runat="server">
																			<asp:ListItem Value="AM">AM</asp:ListItem>
																			<asp:ListItem Value="PM">PM</asp:ListItem>
																		</asp:DropDownList></td>
																</tr>
																<tr style="display:none">
																	<td style="padding-top: 9px;" class="auto-style2">
																		Deliver To:</td>
																	<td class="value" style="height: 30px">
																		<asp:TextBox runat="server" ID="txtDeliverTo" Width="250px" MaxLength="100" Text="NA"></asp:TextBox>
																		<asp:RequiredFieldValidator ID="rfvDeliverTo" runat="server" ErrorMessage="*" ControlToValidate="txtDeliverTo" Display="Dynamic" SetFocusOnError="True"></asp:RequiredFieldValidator>
																	</td>
																</tr>
																<tr style="display:none;">
																	<td style="padding-top: 9px;" class="auto-style2">
																		Incoterm:</td>
																	<td class="evenCells">
																		<asp:DropDownList runat="server" ID="ddlIncoterm" Width="250px" DataSourceID="dsIncoterm" DataTextField="Incoterm" DataValueField="Id">
																		</asp:DropDownList>
																		<asp:SqlDataSource ID="dsIncoterm" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>' SelectCommand="s3p_EBid_GetIncoterm1" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
																	</td>
																</tr>
																<tr>
																	<td style="padding-top: 9px;" class="auto-style2">
																		Currency:</td>
																	<td class="value">
																		<asp:DropDownList runat="server" ID="ddlCurrency" Width="250px" DataSourceID="dsCurrencies" DataTextField="Currency" DataValueField="IdRate" OnDataBound="ddlCurrency_DataBound">
																		</asp:DropDownList>
																		<asp:SqlDataSource ID="dsCurrencies" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>' SelectCommand="sp_GetCurrencies" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
																	</td>
																</tr>
																<tr>
																	<td style="padding-top: 9px;" class="auto-style2">
																		Total Estimate Bid Cost:</td>
																	<td class="value">
																		<asp:TextBox runat="server" ID="txtTotalEventPrice" Width="300px" ReadOnly="false" TabIndex="-1" BorderStyle="None" Font-Size="14px" Font-Bold="true" ForeColor="Black" onkeypress="return false;" onkeydown="return false;"
																			onkeyup="return false;" Style="cursor: default; padding-top: 3px;">0.00</asp:TextBox>
																		<asp:TextBox ID="txtPHPCost" runat="server" BorderStyle="None" Font-Bold="true" Font-Size="14px" ForeColor="Red" onkeydown="return false;" onkeypress="return false;" onkeyup="return false;" ReadOnly="false"
																			Style="cursor: default; padding-top: 3px" TabIndex="-1" Width="300px" Text="0.00 PHP"></asp:TextBox></td>
																</tr>

																<%--<tr>
																	<td style="padding-top: 9px;">
																		VSF</td>
																	<td class="evenCells">
																		<asp:DropDownList ID="VSF" runat="server" DataSourceID="dsVSF" 
																			DataTextField="ProjectName" DataValueField="VSFId" AutoPostBack="True">
																		</asp:DropDownList>
                                                                        <asp:Label ID="VSFLink" runat="server" Text=""></asp:Label>
																		<asp:SqlDataSource ID="dsVSF" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>' SelectCommand="SELECT 0 VSFId, '--SELECT VSF--' ProjectName UNION SELECT VSFId, ProjectName FROM tblVendorShortlistingForm WHERE BuyerID = @BuyerId AND (Status=3 OR Status=1) ORDER BY VSFId ASC" SelectCommandType="Text">
                                                        											<SelectParameters>
                                                        											  <asp:SessionParameter Name="BuyerId" SessionField="UserId" Type="Int32" />
                                                        											  <asp:SessionParameter Name="BidRefNo" SessionField="BidRefNo" Type="Int32" />
                                                        											</SelectParameters>
																		</asp:SqlDataSource>
																	</td>
																</tr>--%>

															</table>
															<p>
																<asp:SqlDataSource ID="dsEventDetails" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>' SelectCommand="sp_GetBidEventDetails" SelectCommandType="StoredProcedure">
																	<SelectParameters>
																		<asp:SessionParameter Name="BidRefNo" SessionField="BidRefNo" Type="Int32" />
																	</SelectParameters>
																</asp:SqlDataSource>
																<asp:SqlDataSource ID="dsItemDetails" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>' SelectCommand="s3p_EBid_GetBidItemDetails" SelectCommandType="StoredProcedure">
																	<SelectParameters>
																		<asp:SessionParameter Name="BidRefNo" SessionField="BidRefNo" Type="Int32" />
																	</SelectParameters>
																</asp:SqlDataSource>
																<asp:SqlDataSource ID="dsAttachments" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>' SelectCommand="sp_GetBidEventFileAttachments" SelectCommandType="StoredProcedure">
																	<SelectParameters>
																		<asp:SessionParameter Name="BidRefNo" SessionField="BidRefNo" Type="Int32" />
																	</SelectParameters>
																</asp:SqlDataSource>
																<asp:SqlDataSource ID="dsParticipants" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>' SelectCommand="s3p_EBid_GetSuppliers" SelectCommandType="StoredProcedure">
																	<SelectParameters>
																		<asp:SessionParameter Name="BidRefNo" SessionField="BidRefNo" Type="String" />
																	</SelectParameters>
																</asp:SqlDataSource>
																<asp:SqlDataSource ID="dsComments" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>' SelectCommand="sp_QueryBidItemsComments" SelectCommandType="StoredProcedure">
																	<SelectParameters>
																		<asp:SessionParameter Name="refNo" SessionField="BidRefNo" Type="String" />
																		<asp:SessionParameter Name="userId" SessionField="UserId" Type="String" />
																	</SelectParameters>
																</asp:SqlDataSource>
																<asp:GridView ID="gvBidItems" runat="server" CssClass="itemDetails_1" AutoGenerateColumns="false" ShowFooter="true" OnDataBound="gvBidItems_DataBound" OnRowCommand="gvBidItems_RowCommand">
																	<SelectedRowStyle BackColor="#50A4D1" />
																	<Columns>
																		<asp:TemplateField HeaderText="&#160;Bid Items">
																			<ItemStyle CssClass="valueGridItem" Width="100%" />
																			<HeaderStyle CssClass="itemDetails_th" />
																			<ItemTemplate>
																				<table id="tblBidItem" runat="server" border="0" cellpadding="0" cellspacing="0" width="100%" style="font-family: Arial; font-size: 11px; border-bottom-width: 0;" visible='<%# (Eval("IsEmpty").ToString() == "1" ? true : false) %>'>
																					<tr>
																						<td width="50%">
																							&nbsp;PR Group: <%--<asp:Label ID="Label8" runat="server" Text='<%# Bind("PRGroupName", "<b>{0}</b>")%>'></asp:Label>--%>
                                                                                            <a href="prItems.aspx?GroupName=<%# Eval("PRGroupName", "{0}")%>" target="_blank"><%# Eval("PRGroupName", "<b>{0}</b>")%></a>
                                                                                            <br />
																							&nbsp;<asp:Label ID="Label1" runat="server" Text='<%# Bind("Item", "Item: <b>{0}</b>")%>'></asp:Label><br />
																							&nbsp;<asp:Label ID="Label2" runat="server" Text='<%# Bind("Description", "Description: <b>{0}</b>")%>'></asp:Label><br />
																							&nbsp;<asp:Label ID="Label3" runat="server" Text='<%# Bind("DeliveryDate", "Delivery Date: <b>{0:D}</b>")%>'></asp:Label><br />
																						</td>
																						<td width="50%">
																							&nbsp;<asp:Label ID="Label4" runat="server" Text='<%# Bind("Quantity", "Quantity: <b>{0}</b>")%>'></asp:Label>
																							<asp:Label ID="Label5" runat="server" Text='<%# Bind("UnitOfMeasure", " <b>{0}</b>")%>'></asp:Label><br />
																							&nbsp;<asp:Label ID="Label6" runat="server" Text='<%# Bind("UnitPrice", "Estimated Unit Price: <b>{0:F2}</b>")%>'></asp:Label><br />
																							&nbsp;<asp:Label ID="Label7" runat="server" Text='<%# Bind("EstimatedTotal", "Total Estimated Price: <b>{0:#,##0.00}</b>")%>'></asp:Label><br />
																						</td>
																					</tr>
																					<tr>
																						<td colspan="2" style="padding-bottom: 5px;">
																							&nbsp;&nbsp;<asp:LinkButton ID="lnkEditItem" runat="server" CommandName="EditItem" CommandArgument='<%# Bind("ID")%>' CausesValidation="false">Edit</asp:LinkButton>
																							&nbsp;<asp:LinkButton ID="lnkRemove" runat="server" CommandName="Remove" CommandArgument='<%# Bind("ID")%>' CausesValidation="false" OnClientClick="return confirm('Are you sure to remove this item?')">Remove</asp:LinkButton>
																						</td>
																					</tr>
																				</table>
																			</ItemTemplate>
																			<FooterTemplate>
																				<table cellpadding="5" cellspacing="0" width="100%" class="itemDetails_1" style="font-family: Arial; font-size: 11px; border-bottom-width: 0;">
                                                                                    <tr>
                                                                                        <td><span style="width: 75px;">&nbsp;PR Group Name &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>
																		                    <asp:DropDownList ID="PRInfo" runat="server" DataSourceID="dsPRInfo" 
																			                    DataTextField="GroupName" DataValueField="GroupName" AutoPostBack="True" Visible="false">
																		                    </asp:DropDownList>
                                                                                            <asp:Label ID="PRInfoLink" runat="server" Text=""></asp:Label>  
																		                    <asp:SqlDataSource ID="dsPRInfo" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>' SelectCommand="sp_GetPRGroup" SelectCommandType ="StoredProcedure">
                                                        											                    <SelectParameters>
                                                        											                        <asp:SessionParameter Name="BuyerId" SessionField="UserId" Type="Int32" />
                                                        											                    </SelectParameters>
																		                    </asp:SqlDataSource>
																	                    </td>
                                                                                    </tr>
																					<tr>
																						<td style="border-top: 1px solid #ccc">
																							<span style="width: 75px;">&nbsp;Item&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>
																							<asp:TextBox runat="server" ID="txtItem" Width="300" ReadOnly="true" BackColor="#F4F1C4" ></asp:TextBox>
																							<a href="javascript://" id="lnkSelectProduct" runat="server" style="height: 20px; vertical-align: bottom; display:none"  >Select</a>
																							<asp:HiddenField ID="hdnSKU" runat="server" />
																							<asp:HiddenField ID="hdnID" runat="server" />
																						</td>
																					</tr>
																					<tr>
																						<td style="border-top: 1px solid #ccc">
																							<span style="width: 75px;">&nbsp;Description&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>
																							<asp:TextBox runat="server" ID="txtDescription" Width="300" ReadOnly="true" BackColor="#F4F1C4"></asp:TextBox>
																						</td>
																					</tr>
																					<tr>
																						<td style="border-top: 1px solid #ccc">
																							<span style="width: 75px;">&nbsp;Delivery Date&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>
																							<cc1:JSCalendar ID="clndrDeliveryDate" runat="server" ImageURL="../../calendar/img.gif" EnableViewState="true" ScriptsBasePath="../../calendar/" DateFormat="MM/dd/yyyy" Width="178px" ReadOnly="false" MaxLength="10" TabIndex="-1"></cc1:JSCalendar>
					<asp:Label ID="lblDrDate1" runat="server" Text="eg. 01/31/2007" ForeColor="red" Font-Size="10px" />																						</td>
																					</tr>
																					<tr>
																						<td style="border-top: 1px solid #ccc">
																							<span style="width: 110px;">&nbsp;Unit Of Measure:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>
																							<asp:TextBox runat="server" ID="txtUnitOfMeasure" Width="100" MaxLength="20"></asp:TextBox>
																						</td>
																					</tr>
																					<tr>
																						<td style="border-top: 1px solid #ccc">
																							<span style="width: 110px;">&nbsp;Quantity:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>
																							<asp:TextBox runat="server" ID="txtQuantity" Width="100" MaxLength="10">0</asp:TextBox>
																						</td>
																					</tr>
																					<tr>
																						<td style="border-top: 1px solid #ccc">
																							<span style="width: 110px;">&nbsp;Estimated Unit Price:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>
																							<asp:TextBox runat="server" ID="txtUnitPrice" Width="100" MaxLength="10">0</asp:TextBox>.
																							<asp:TextBox runat="server" ID="txtUnitPriceCents" Width="20" MaxLength="2">00</asp:TextBox>
																						</td>
																					</tr>
																					<tr>
																						<td style="border-top: 1px solid #ccc">
																							<span style="width: 110px;">&nbsp;Total Estimated Price:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>
																							<asp:TextBox runat="server" ID="txtTotalPrice" Width="250px" ReadOnly="false" TabIndex="-1" BorderStyle="None" BackColor="#DBEAF5" Font-Size="14px" Font-Bold="true" ForeColor="red" onkeypress="return false;"
																								onkeydown="return false;" onkeyup="return false;" Style="cursor: default; padding-top: 3px; padding-left:5px;">0.00</asp:TextBox>
																						</td>
																					</tr>
																					<tr>
																						<td style="height: 25px; vertical-align: bottom;border-top: 1px solid #ccc">
																							&nbsp;&nbsp;<asp:LinkButton ID="lnkAddItem" runat="server" CommandName="Add" CausesValidation="false">Add To Bid Items List</asp:LinkButton>
																							&nbsp;<asp:LinkButton ID="lnkUpdateItem" runat="server" CommandName="UpdateItem" CausesValidation="false" Visible="false">Confirm Update</asp:LinkButton>
																							&nbsp;<asp:LinkButton ID="lnkCancelUpdate" runat="server" CommandName="CancelUpdate" CausesValidation="false" Visible="false">Cancel Update</asp:LinkButton>
																						</td>
																					</tr>
																					<tr>
																						<td style="padding-left: 10px;">
																							<p style="color: Red;">
																								<asp:Literal ID="addItemMsg" runat="server"></asp:Literal>
																							</p>
																						</td>
																					</tr>
																				</table>
																			</FooterTemplate>
																		</asp:TemplateField>
																	</Columns>
																</asp:GridView>
															</p>
															<p>
																<asp:GridView ID="gvFileAttachment" runat="server" CssClass="itemDetails_1" AutoGenerateColumns="false" OnRowCommand="gvFileAttachment_RowCommand" ShowFooter="true">
																	<Columns>
																		<asp:TemplateField HeaderText="&nbsp;File Attachment">
																			<ItemStyle CssClass="valueGridItem" Width="100%" />
																			<HeaderStyle CssClass="itemDetails_th" />
																			<ItemTemplate>
																				<asp:Literal ID="litAttach" runat="server" Text="&nbsp;" Visible='<%# IsAttached(Eval("Attached").ToString()) %>' />
																				<asp:Image ID="imgAttach" runat="server" ImageUrl="~/web/themes/images/paperclip.gif" Width="10px" Height="10px" Visible='<%# IsAttached(Eval("Attached").ToString()) %>' />
																				<asp:LinkButton ID="lnkRemoveAttachment" runat="server" CommandName="Remove" CommandArgument='<% #Bind("ID")%>' Visible='<%# IsAttached(Eval("Attached").ToString()) %>' CausesValidation="false">Remove</asp:LinkButton>
																				<asp:Label ID="lblFile" runat="server" CssClass="discount" Text='<% #Bind("Original", ": {0}")%>' Visible='<%# IsAttached(Eval("Attached").ToString()) %>'></asp:Label>, Size: <asp:Label ID="Label8" runat="server" Text='<% #Bind("Filesize") %>'></asp:Label>
																			</ItemTemplate>
																			<FooterTemplate>
																				<p style="color: Red;">
																					&nbsp;<asp:FileUpload ID="fileUpload" runat="server" Width="330px" /><br />
																					&nbsp;&nbsp;<asp:LinkButton ID="lnkAttach" runat="server" CausesValidation="false" CommandName="Attach">Add To Attachments List</asp:LinkButton>
																					<asp:Literal ID="addAttachmentMsg" runat="server"></asp:Literal>
																				</p>
																			</FooterTemplate>
																		</asp:TemplateField>
																	</Columns>
																</asp:GridView>
															</p>
															<p>
															   <asp:HyperLink ID="lnkAddOTLS" runat="server" Visible="False">Add One-Time Supplier/Participant</asp:HyperLink><br /><br />
															   <asp:CheckBox ID="chkQualifiedSourcing" runat="server" Text="Qualified Sourcing"/> 
																<table cellspacing="0" cellpadding="0" width="100%" border="0" class="itemDetails_1">
																	<tbody>
																		<tr>
																			<th colspan="3" class="itemDetails_th">
																				&nbsp;Suppliers / Participant
																			</th>
																		</tr>
																		<tr>
																			<td align="right">
																				<asp:ListBox Style="width: 550px; height: 200px" ID="lstSupplierA" runat="server" DataSourceID="dsVendors" DataTextField="VendorLabel" DataValueField="VendorId" SelectionMode="Multiple" OnDataBound="lstSupplierA_DataBound">
																				</asp:ListBox>&nbsp;
																			</td>
																			<td align="center" style="width: 22px;">
																				<input style="width: 20px" id="btnSelectAll" type="button" value=">>" runat="server" onserverclick="btnSelectAll_ServerClick" causesvalidation="false" /><br />
																				<input style="width: 20px" id="btnSelectOne" type="button" value=">" runat="server" onserverclick="btnSelectOne_ServerClick" causesvalidation="false" /><br />
																				<input style="width: 20px" id="btnDeselectOne" type="button" value="<" runat="server" onserverclick="btnDeselectOne_ServerClick" causesvalidation="false" /><br />
																				<input style="width: 20px" id="btnDeselectAll" type="button" value="<<" runat="server" onserverclick="btnDeselectAll_ServerClick" causesvalidation="false" />
																			</td>
																			<td align="left">
																				<asp:ListBox Style="width: 250px; height: 200px" ID="lstSupplierB" runat="server" SelectionMode="Multiple" OnDataBound="lstSupplierB_DataBound"></asp:ListBox>
																			</td>
																		</tr>
																		<tr>
																			<td colspan="3">
																				<asp:SqlDataSource ID="dsVendors" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>' SelectCommand="sp_GetVendorsInCategoryAndSubCategory" SelectCommandType="StoredProcedure">
																					<SelectParameters>
																						<asp:ControlParameter ControlID="ddlCategory" DefaultValue=" " Name="CategoryId" PropertyName="SelectedValue" Type="String" />
																						<asp:ControlParameter ControlID="ddlSubCategory" DefaultValue=" " Name="SubCategoryId" PropertyName="SelectedValue" Type="String" />
																					</SelectParameters>
																				</asp:SqlDataSource>
																				<asp:HiddenField ID="hdnVendor" runat="server" />
																			</td>
																		</tr>
																		<tr>
																			<td colspan="2" style="font:9px Arial" align="center">(Name - Accreditation Status - Composite Rating - Maximum Exposure - Performance Rating)</td>
																		</tr>
																	</tbody>
																</table>
															</p>
															<p style="text-align: center;">
																<br />
																Comment<br />
																<asp:TextBox ID="txtComment" runat="server" TextMode="MultiLine" Width="360px" Height="90px" Font-Names="Arial" MaxLength="999"></asp:TextBox><br />
																<%--<asp:RequiredFieldValidator ID="rfvComment" runat="server" ErrorMessage="Include a comment please."
																	ControlToValidate="txtComment" SetFocusOnError="true"></asp:RequiredFieldValidator>--%>
																<asp:CustomValidator ID="cfvSuppliers" runat="server" Display="Dynamic" OnServerValidate="cfvSuppliers_ServerValidate"></asp:CustomValidator><br />
															</p>
															<table border="0" cellpadding="0" cellspacing="0" width="100%" id="actions">
																<tr>
																	<td>
																		<asp:LinkButton runat="server" ID="btnDraft" CommandName="Draft" OnCommand="btn_Command">Draft</asp:LinkButton>
																		<asp:LinkButton runat="server" ID="btnSubmit" CommandName="Submit" OnClientClick="return confirm('Are you sure you want to submit this new bid event?');" OnCommand="btn_Command">Submit</asp:LinkButton>
																		<asp:LinkButton runat="server" ID="btnCancel" CommandName="Cancel" CausesValidation="False" OnCommand="btn_Command">Cancel</asp:LinkButton>
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
						<EBid:Footer runat="server" ID="Footer" />
					</td>
				</tr>
			</table>
		</div>
	</form>
</body>
</html>
