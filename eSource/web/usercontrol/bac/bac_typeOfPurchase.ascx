<%@ Control Language="C#" AutoEventWireup="true" CodeFile="bac_typeOfPurchase.ascx.cs" Inherits="web_usercontrol_bac_bac_typeOfPurchase" %>
<table width="60%" border="1" cellpadding="0" cellspacing="0" rules="all" class="itemDetails" id="Biddetails_details1_dvEventDetails6" style="border-color:#71A9D2; font-family: Arial; font-size: 11px; border-collapse: collapse; background-color: #F4F1C4; width:99%; ">
																<tbody>
																	<tr>
																		<td class="ui-widget-header" style="height:26px; vertical-align:middle;">TYPE OF PURCHASE</td>
																	</tr>
																	<tr valign="middle">
																		<td align="center" valign="middle" style="width:25%;">
																			<table width="100%" border="0" cellspacing="0" cellpadding="0">
																				<tr>
																					<td width="100%" style="border:none; height:80px">
																						<asp:DataList runat="server"  DataSourceID="dsBACTypeOfPurchase" ID="DataList13" >
                                                                                  <ItemTemplate>
                                                                                  <asp:label runat="server" ID="payment_terms" Name="payment_terms" Text='<%# Eval("TypeOfPurchase") %>' Font-Bold="True">
                                                                                  </asp:label>
                                                                                  </ItemTemplate>
                                                                             </asp:DataList>
                                                                             <asp:SqlDataSource ID="dsBACTypeOfPurchase" runat="server" 
                                                                            ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>'
                                                                            SelectCommand="SELECT TypeOfPurchase FROM tblBACTypeOfPurchase WHERE BidRefNo=@BidRefNo" >
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
