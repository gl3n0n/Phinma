<%@ Control Language="C#" AutoEventWireup="true" CodeFile="bac_paymentTerms.ascx.cs" Inherits="web_usercontrol_bac_bac_paymentTerms" %>
<table width="50%" border="1" cellpadding="0" cellspacing="0" rules="all" class="itemDetails" id="Biddetails_details1_dvEventDetails4" style="border-color:#71A9D2; font-family: Arial; font-size: 11px; border-collapse: collapse; background-color: #F4F1C4; width:99%; min-height:125px;">
																<tbody>
																	<tr>
																		<td class="ui-widget-header" style="height:26px; vertical-align:middle;">PAYMENT TERMS</td>
																	</tr>
																	<tr valign="middle">
																		<td align="center" valign="middle" style="width:25%;">
																			<table width="100%" border="0" cellspacing="0" cellpadding="0">
																				<tr>
																					<td width="100%">
																						<table width="100%">
																							<tr><td style="border:none; height:80px; display:block" width="50%;">
																								
                                                                             <asp:DataList runat="server" DataSourceID="dsPaymentTerms" ID="DataList8" Font-Bold="True" >
                                                                                  <ItemTemplate>
                                                                                  <asp:label runat="server" ID="payment_terms" Name="payment_terms" Text='<%# Eval("PaymentTerm") %>' >
                                                                                  </asp:label>
                                                                                  </ItemTemplate>
                                                                             </asp:DataList>
                                                                             <asp:SqlDataSource ID="dsPaymentTerms" runat="server" 
                                                                            ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>'
                                                                            SelectCommand="SELECT PaymentTerm FROM tblBACPaymentTerms WHERE BidRefNo=@BidRefNo">
																					<SelectParameters>
																						<asp:SessionParameter Name="BidRefNo" SessionField="BuyerBidForBac" Type="Int32" />
																					</SelectParameters>
																			</asp:SqlDataSource>
                                                                            
																							</td>
                                                                                            <td width="50%">
                                                                                            Description:
                                                                                            <asp:DataList runat="server"  DataSourceID="dsBuyerBidForBac" ID="DataList22" Font-Bold="True" >
                                                                                  <ItemTemplate>
                                                                                  <asp:label runat="server" ID="descpayment_terms" Name="descpayment_terms" Text='<%# Eval("descPaymentTerms") %>' >
                                                                                  </asp:label>
                                                                                  </ItemTemplate>
                                                                             </asp:DataList>
                                                                                            </td>
                                                                                            </tr>
																						</table>
																					</td>
																				</tr>
																			</table>
																		</td>
																	</tr>
																</tbody>
															</table>