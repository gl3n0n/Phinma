<%@ Control Language="C#" AutoEventWireup="true" CodeFile="bac_supplyPosition.ascx.cs" Inherits="web_usercontrol_bac_supplyPosition" %>
<table width="100%" border="1" cellpadding="0" cellspacing="0" rules="all" class="itemDetails" id="Biddetails_details1_dvEventDetails7" style="border-color:#71A9D2; font-family: Arial; font-size: 11px; border-collapse: collapse; background-color: #F4F1C4; width:99%; min-height:125px;">
																<tbody>
																	<tr>
																		<td class="ui-widget-header" style="height:26px; vertical-align:middle;">SUPPLY POSITION</td>
																	</tr>
																	<tr valign="middle">
																		<td align="center" valign="middle" style="width:25%; border:none">
																			<table width="100%" border="0" cellspacing="0" cellpadding="0">
																				<tr>
																					<td width="100%" style="border:none; height:80px;">
																						<asp:DataList runat="server"  DataSourceID="dsBACSupplyPosition" 
                                                                                            ID="DataList14" Font-Bold="True" >
                                                                                  <ItemTemplate>
                                                                                  <asp:label runat="server" ID="payment_terms" Name="payment_terms" Text='<%# Eval("SupplyPosition") %>' >
                                                                                  </asp:label>
                                                                                  </ItemTemplate>
                                                                             </asp:DataList>
                                                                             <asp:SqlDataSource ID="dsBACSupplyPosition" runat="server" 
                                                                            ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>'
                                                                            SelectCommand="SELECT SupplyPosition FROM tblBACSupplyPosition WHERE BidRefNo=@BidRefNo" >
																					<SelectParameters>
																						<asp:SessionParameter Name="BidRefNo" SessionField="BuyerBidForBac" Type="Int32" />
																					</SelectParameters>
																			</asp:SqlDataSource>
																					</td>
																				</tr>
																			</table>
																		</td>
																	</tr>
																</tbody>
															</table>