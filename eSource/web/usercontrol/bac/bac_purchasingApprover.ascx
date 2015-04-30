<%@ Control Language="C#" AutoEventWireup="true" CodeFile="bac_purchasingApprover.ascx.cs" Inherits="web_usercontrol_bac_bac_purchasingApprover" %>
<table width="100%" border="1" cellpadding="0" cellspacing="0" rules="all" class="itemDetails" id="Biddetails_details1_dvEventDetails11" style="border-color:#71A9D2; font-family: Arial; font-size: 11px; border-collapse: collapse; background-color: #F4F1C4; height:100%">
																<tbody>
																	<tr>
																		<td colspan="4" class="ui-widget-header" style="height:26px; vertical-align:middle;">PURCHASING</td>
																	</tr>
																	<tr valign="middle" >
																		<td width="25%" align="center" valign="middle" style="padding:5px; font-size:12px;">
																			<asp:SqlDataSource ID="dsDates" runat="server"  ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>' 
SelectCommand="SELECT DateSubmitted as PreparedDt, ApprovedDt_0 AS ApprovedDt FROM tblBACBidItems WHERE BacRefNo=@BuyerBacRefNo" >
		<SelectParameters>
			<asp:SessionParameter Name="BuyerBacRefNo" SessionField="BuyerBacRefNo" Type="Int32" />
		</SelectParameters>
</asp:SqlDataSource><table width="150" border="0" cellspacing="0" cellpadding="0">
																				<tr>
																					<td>Prepared by / date: <br />
                                                                                    <asp:Repeater ID="Repeater2" runat="server" DataSourceID="dsDates">
                                                                                    <ItemTemplate>
                                                                                    <asp:Literal ID="lblPreparedDate" runat="server" Text='<%# Eval("PreparedDt") %>'></asp:Literal>
                                                                                    </ItemTemplate></asp:Repeater>
                                                                                    </td>
																				</tr>
																				<tr>
																					<td>&nbsp;</td>
																				</tr>
																				<tr>
																					<td>
                                                                                    <asp:DataList runat="server"  DataSourceID="dsBuyerDetails" ID="DataList21" 
                                                                                            Font-Bold="True" Font-Size="12px" >
                                                                                  <ItemTemplate>
                                                                                  <asp:label runat="server" ID="lblApprover5" Name="lblApprover5" Text='<%# Eval("Name1") %>'  Font-Size="12px">
                                                                                  </asp:label>
                                                                                  </ItemTemplate>
                                                                             </asp:DataList>
                                                                                    </td>
																				</tr>
																				<tr>
																					<td style="border-top:2px #000 solid;">Buyer</td>
																				</tr>
																			</table>
                                                                            <asp:SqlDataSource ID="dsBuyerDetails" runat="server"  ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>' 
SelectCommand="SELECT t1.BuyerLastName + ', ' + t1.BuyerFirstName + ' ' + t1.BuyerMidName AS Name1 FROM tblBuyers t1, tblBacBidItems t2 WHERE t1.BuyerID=t2.BuyerId AND t2.BacRefNo = @BuyerBacRefNo" >
		<SelectParameters>
			<asp:SessionParameter Name="BuyerBacRefNo" SessionField="BuyerBacRefNo" Type="Int32" />
		</SelectParameters>
</asp:SqlDataSource>
																		</td>
																		<td width="25%" align="center" valign="middle" style="padding:5px; font-size:12px;">
																			<table width="150" border="0" cellspacing="0" cellpadding="0">
																				<tr>
																					<td colspan="2">Reviewed by / date: <br />
                                                                                    <asp:Repeater ID="Repeater1" runat="server" DataSourceID="dsDates">
                                                                                    <ItemTemplate>
                                                                                    <asp:Literal ID="lblApprovedDt" runat="server" Text='<%# Eval("ApprovedDt") %>'></asp:Literal>
                                                                                    </ItemTemplate></asp:Repeater>

                                                                                    </td>
																				</tr>
																				<tr>
																					<td>
																						<label for="clarify4">Clarify</label>
																						<input name="clarify4" type="checkbox" id="clarify4" disabled="disabled" />
																					</td>
																					<td>
																						<label for="approved4">Approved</label>
                                                                                        <asp:Repeater ID="RepeaterCurApproved" runat="server" DataSourceID="dsCurApproved">
                                                                                        <ItemTemplate>
                                                                                            <%# (Eval("ApprovedDt_0").ToString() != "" || Eval("ClarifyDt_1").ToString() != "" || Eval("ApprovedDt_1").ToString() != "" || Eval("ClarifyDt_2").ToString() != "" || Eval("ApprovedDt_2").ToString() != "" || Eval("ClarifyDt_3").ToString() != "" || Eval("ApprovedDt_3").ToString() != "" || Eval("ClarifyDt_4").ToString() != "" || Eval("ApprovedDt_4").ToString() != "" || Eval("ClarifyDt_5").ToString() != "" || Eval("ApprovedDt_5").ToString() != "" || Eval("ClarifyDt_6").ToString() != "" || Eval("ApprovedDt_6").ToString() != "" || Eval("ClarifyDt_7").ToString() != "" || Eval("ApprovedDt_7").ToString() != "" || Eval("ClarifyDt_8").ToString() != "" || Eval("ApprovedDt_8").ToString() != "" || Eval("ClarifyDt_9").ToString() != "" || Eval("ApprovedDt_9").ToString() != "" || Eval("ClarifyDt_10").ToString() != "" || Eval("ApprovedDt_10").ToString() != "") ? "<input id='Ck0' type='checkbox' disabled='disabled' checked='true'/>" : "<input id='Ck0' type='checkbox' disabled='disabled'/>" %>
                                                                                        </ItemTemplate>
                                                                                        </asp:Repeater>
                                                                                     <asp:SqlDataSource ID="dsCurApproved" runat="server"  
                                                                                     ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>' 
                                                                                     SelectCommand="SELECT * FROM tblBacBidItems WHERE BidRefNo=@BuyerBidForBac" >
		                                                                                    <SelectParameters>
			                                                                                    <asp:SessionParameter Name="BuyerBidForBac" SessionField="BuyerBidForBac" Type="Int32" />
		                                                                                    </SelectParameters>
                                                                                    </asp:SqlDataSource>

																					</td>
																				</tr>
																				<tr>
																					<td colspan="2">
                                                                                        <asp:DataList runat="server"  DataSourceID="dsPurchasing" ID="DataList20" 
                                                                                            Font-Bold="True" Font-Size="12px"  >
                                                                                  <ItemTemplate>
                                                                                  
                                                                                  <asp:label runat="server" ID="lblApprover4" Name="lblApprover4" Text='<%# Eval("Name1") %>'  Font-Size="12px">
                                                                                  </asp:label>
                                                                                  </ItemTemplate>
                                                                             </asp:DataList>
																				</tr>
																				<tr>
																					<td colspan="2" style="border-top:2px #000 solid;">Purchasing Approver</td>
																				</tr>
																			</table>

<asp:SqlDataSource ID="dsPurchasing" runat="server"  ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>' 
SelectCommand="SELECT t1.PurchasingID, t2.LastName + ', ' + t2.FirstName + ' ' + t2.MiddleName AS Name1, t3.ApprovedDt_0 AS ApprovedDt FROM tblSupervisor t1, tblPurchasing t2, tblBacBidItems t3 WHERE t1.PurchasingID=t2.PurchasingID and t1.BuyerId = t3.buyerId AND t2.PurchasingID=t3.Approver_0 and t3.BidRefNo=@BuyerBidForBac" >
		<SelectParameters>
			<asp:SessionParameter Name="BuyerBidForBac" SessionField="BuyerBidForBac" Type="Int32" />
		</SelectParameters>
</asp:SqlDataSource>
																		</td>
																	</tr>
																</tbody>
															</table>