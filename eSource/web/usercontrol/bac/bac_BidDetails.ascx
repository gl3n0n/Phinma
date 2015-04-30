                                                            <%@ Control Language="C#" AutoEventWireup="true" CodeFile="bac_BidDetails.ascx.cs" Inherits="web_usercontrol_bac_BidDetails" %>
                                                            <table width="100%" border="1" cellpadding="0" cellspacing="0" rules="all" class="itemDetails" id="Biddetails_details1_dvEventDetails" style="border-color:#71A9D2; font-family: Arial; font-size: 11px; border-collapse: collapse; background-color: #F4F1C4; height:100%">
																<tbody>
																	<tr>
																		<td colspan="4" class="ui-widget-header" style="height:26px; vertical-align:middle;">BID DETAILS</td>
																	</tr>
																	<tr valign="middle" >
																		<td colspan="2" align="center" valign="middle" style="padding:5px; font-size:12px; width:50%">Bid Reference No. </td>
																		<td colspan="2" align="center" valign="middle" style="padding:5px; font-size:12px; width:50%">Bid Events Description: </td>
																	</tr>
																	<tr valign="middle" >
																		<td colspan="2" align="center" valign="middle" bgcolor="#FFFFFF" style="padding:15px 5px; font-size:12px; width:50%">
																			<div id="fromDS">
																				<%--<asp:dropdownlist ID="BuyerBidForBac" runat="server"
																					DataSourceID="dsBuyerBidForBac"
																					DataTextField="ItemDesc"
																					DataValueField="BacRefNo" AutoPostBack="True"
																					OnDataBound="BuyerBidForBac_DataBound" onload="Page_LoadComplete" >
																				</asp:dropdownlist>--%>
                                                                                <asp:DataList runat="server"  DataSourceID="dsBuyerBidForBac" ID="details1" >
                                                                                  <ItemTemplate>
                                                                                  <asp:label runat="server" ID="Label1" Name="ItemDescLbl" Text='<%# Eval("BidRefNo") %>' Font-Bold="True" Font-Size="14">
                                                                                  </asp:label>
                                                                                  </ItemTemplate>
                                                                                  </asp:DataList>
																			</div>

																			<input type="hidden" id="lblTotalAll" name="lblTotalAll" runat="server" />
                                                                            <input type="hidden" id="txtClarify" name="txtClarify" runat="server" />
																		</td>
																		<td colspan="2" align="center" valign="middle" bgcolor="#FFFFFF" style="padding:15px 5px; font-size:12px; width:50%">
																			<%--<asp:TextBox ID="ItemDescLbl" runat="server" Width="350"></asp:TextBox>--%>
                                                                            <asp:DataList runat="server"  DataSourceID="dsBuyerBidForBac" ID="DataList1" 
                                                                                Font-Bold="True" >
                                                                                  <ItemTemplate>
                                                                                  <asp:label runat="server" ID="ItemDesc" Name="ItemDesc" Text='<%# Eval("ItemDesc") %>'  Font-Size="12">
                                                                                  </asp:label>
                                                                                  </ItemTemplate>
                                                                             </asp:DataList>
																		</td>
																	</tr>
																	<tr valign="middle">
																		<td align="center" valign="middle" style="width:25%;">SAP PR No.:</td>
																		<td align="center" valign="middle" style="width:25%;">Amount:</td>
																		<td align="center" valign="middle" style="width:25%;">SAP PR Date:</td>
																		<td align="center" valign="middle" style="width:25%;">Date Received</td>
																	</tr>
																	<tr valign="middle">
																		<td align="center" valign="middle" style="background-color:#FFFFFF; width:25%;">
																			<%--<asp:TextBox ID="PRRefNoLbl" runat="server" Width="75"></asp:TextBox>--%>
                                                                            <asp:DataList runat="server"  DataSourceID="dsBuyerBidForBac" ID="DataList2" 
                                                                                Font-Bold="True" >
                                                                                  <ItemTemplate>
                                                                                  <asp:label runat="server" ID="PRRefNoLbl" Name="PRRefNoLbl" Text='<%# Eval("SAPPRNo") %>'  >
                                                                                  </asp:label>
                                                                                  </ItemTemplate>
                                                                             </asp:DataList>
																		</td>
																		<td align="center" valign="middle" bgcolor="#FFFFFF" style="width:25%;">
																			<%--<asp:TextBox ID="EstItemValueLbl" runat="server" Text="0" Width="75"></asp:TextBox>--%>
                                                                            <asp:DataList runat="server"  DataSourceID="dsBuyerBidForBac" ID="DataList3" 
                                                                                Font-Bold="True" >
                                                                                  <ItemTemplate>
                                                                                  <asp:label runat="server" ID="EstItemValueLbl" Name="EstItemValueLbl" Text='<%# String.Format("{0:0,0.00}", Eval("BidAmount")) %>'  >
                                                                                  </asp:label>
                                                                                  </ItemTemplate>
                                                                             </asp:DataList>
																		</td>
																		<td align="center" valign="middle" bgcolor="#FFFFFF" style="width:25%;">
																			<%--<asp:TextBox ID="PRDate" runat="server" Width="75"></asp:TextBox>--%>
                                                                            <asp:DataList runat="server"  DataSourceID="dsBuyerBidForBac" ID="DataList4" 
                                                                                Font-Bold="True" >
                                                                                  <ItemTemplate>
                                                                                  <asp:label runat="server" ID="PRDate" Name="PRDate" Text='<%# String.Format("{0:yyyy-MMM-dd}", Eval("SAPPRDate")) %>'  >
                                                                                  </asp:label>
                                                                                  </ItemTemplate>
                                                                             </asp:DataList>
																		</td>
																		<td align="center" valign="middle" bgcolor="#FFFFFF" style="width:25%;">
																			<%--<asp:TextBox ID="DateApprovedLbl" runat="server" Width="75"></asp:TextBox>--%>
                                                                            <asp:DataList runat="server"  DataSourceID="dsBuyerBidForBac" ID="DataList5" 
                                                                                Font-Bold="True" >
                                                                                  <ItemTemplate>
                                                                                  <asp:label runat="server" ID="DateApprovedLbl" Name="DateApprovedLbl" Text='<%# String.Format("{0:yyyy-MMM-dd}", Eval("RcvdDate")) %>'  >
                                                                                  </asp:label>
                                                                                  </ItemTemplate>
                                                                             </asp:DataList>
																		</td>
																	</tr>
																	<tr valign="middle">
																		<td colspan="2" align="center" valign="middle" style="width:25%;">
																			<table width="100%" border="0" cellspacing="0" cellpadding="0">
																				<tr>
																					<td><input type="radio" runat="server" id="Budgeted" name="Budgeted" value="1"  disabled="disabled" />
																						<label for="Budgeted">Budgeted</label>
																					</td>
																					<td>
																						<input type="radio" runat="server" id="UnBudgeted" name="Budgeted" value="0" disabled="disabled" />
																						<label for="UnBudgeted">UnBudgeted</label>
																					</td>
																				</tr>
																			</table>
																		</td>
																		<td colspan="2" align="center" valign="middle" style="width:25%;">
																			<table width="100%" border="0" cellspacing="0" cellpadding="0">
																				<tr>
																					<td class="style1">
                                                                                        <input name="CompanyId" runat="server" value="0"  id="CompanyIdGT" type="radio" disabled="disabled"  />
																						<label for="CompanyIdGT">GT</label>
																					</td>
																					<td>
																						<input name="CompanyId"  id="CompanyIdIC" runat="server" value="1" type="radio" disabled="disabled"  />
																						<label for="CompanyIdIC">IC</label>
																					</td>
																					<td>
																						<input name="CompanyId"  id="CompanyIdGXI" runat="server" value="2" type="radio" disabled="disabled"  />
																						<label for="CompanyIdGXI">GXI</label>
																					</td>
																					<td>
																						<input name="CompanyId"  id="CompanyIdEGG" runat="server" value="3" type="radio"  disabled="disabled"  />
																						<label for="CompanyIdEGG">EGG</label>
																					</td>
																				</tr>
																			</table>
																		</td>
																	</tr>
																</tbody>
															</table>
                                                            