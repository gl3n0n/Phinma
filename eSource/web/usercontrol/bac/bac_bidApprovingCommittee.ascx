<%@ Control Language="C#" AutoEventWireup="true" CodeFile="bac_bidApprovingCommittee.ascx.cs" Inherits="web_usercontrol_bac_bac_bidApprovingCommittee" %>
<table border="1" cellpadding="0" cellspacing="0" rules="all" class="itemDetails" id="Biddetails_details1_dvEventDetails12" style="border-color:#71A9D2; font-family: Arial; font-size: 11px; border-collapse: collapse; background-color: #F4F1C4; height:100%; width:100%;">
																<tbody>
																	<tr>
																		<td class="ui-widget-header" style="height:26px; vertical-align:middle;">BID APPROVING AUTHORITY</td>
																	</tr>
																	<tr valign="middle" >
																		<td width="100%" align="center" valign="middle" style="padding:5px; font-size:12px;">
																			<asp:Repeater ID="RepeaterApprover1" runat="server" DataSourceID="dsApprover"  
                                                                                OnItemDataBound="RepeaterApprover1_ItemDataBound" 
                                                                                onload="RepeaterApprover1_Load" >
																				<ItemTemplate>
																					<div style="float:left; margin-right:15px; width:150px; padding:10px 20px;">
																						<table width="150px" border="0" cellspacing="0" cellpadding="0">
																							<tr>
																								<td colspan="2">Approved by / date: <br /><%# Eval("ApprovedDt").ToString() !="" ? Eval("ApprovedDt") : "&nbsp;" %></td>
                                                                                                
																							</tr>
																							<tr>
																								<td><label for="clarify1">Clarify</label>
																									<asp:CheckBox ID="clarify1" runat="server" Enabled="false" />
																								</td>
																								<td>
                                                                                                <label for="approved1">Approved</label>
                                                                                                <%# (Eval("ApprovedDt").ToString() != "") ? "<input type='checkbox' disabled='disabled' checked='true'>" : "<input type='checkbox' disabled='disabled'>"%>
																								</td>
																							</tr>
																							<tr>
																								<td colspan="2"><strong><%# Eval("Name1") %></strong></td>
																							</tr>
																							<tr>
																								<td colspan="2"></td>
																							</tr>
																							<tr>
																								<td colspan="2" style="border-top:2px #000 solid;">
                                                                                                    <asp:Literal ID="lblApprover" runat="server" Text="APPROVER"></asp:Literal>
                                                                                                    <input id="chkApproverLevel" type="checkbox" runat="server" value='<%# Eval("BACId") %>' disabled="disabled" visible="false" />
                                                                                                </td>
																							</tr>
																						</table>
																					</div>
																				</ItemTemplate>
																			</asp:Repeater>
																		</td>
																	</tr>
																</tbody>
															</table>
                                                            <asp:SqlDataSource ID="dsApprover" runat="server"  ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>'>
		<SelectParameters>
			<asp:SessionParameter Name="BidRefNo" SessionField="BuyerBidForBac" Type="Int32" />
		</SelectParameters>
</asp:SqlDataSource>