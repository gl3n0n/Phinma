<%@ Control Language="C#" AutoEventWireup="true" CodeFile="bac_Items.ascx.cs" Inherits="web_usercontrol_bac_Items" %>
<table width="100%" border="1" cellpadding="0" cellspacing="0" rules="all" class="itemDetails" id="Table2" style="border-color:#71A9D2; font-family: Arial; font-size: 11px; border-collapse: collapse; background-color: #F4F1C4; margin-bottom:2px;">
																<tbody>
																	<tr>
																		<td class="ui-widget-header" style="height:26px; vertical-align:middle;">
																			ITEMS
																		</td>
																	</tr>
																</tbody>
															</table>
                                                            <asp:GridView ID="gvItems" runat="server" AutoGenerateColumns="False"
																			DataSourceID="dsItems" Width="100%" BorderColor="#71A9D2" BorderStyle="Solid" BorderWidth="2px" CellPadding="3" 
                                                                Font-Size="12px" >
																			<Columns>
																				<asp:TemplateField HeaderText="Item #" HeaderStyle-CssClass="ui-widget-header" >
																					<ItemTemplate>
																						&nbsp;<asp:Label ID="lblItemNo" runat="server" Text='<%# Bind("BidDetailNo") %>'  Font-Bold="True"></asp:Label>
																					</ItemTemplate>
																				</asp:TemplateField>
																				<asp:TemplateField HeaderText="Item Details" HeaderStyle-CssClass="ui-widget-header" >
																					<ItemTemplate>
																						&nbsp;<asp:Label ID="lblItemD" runat="server" Text='<%# Bind("ItemName") %>' CommandName="AddItemRow" Font-Bold="True"></asp:Label>
																					</ItemTemplate>
																				</asp:TemplateField>
																				<asp:TemplateField HeaderText="PR No." HeaderStyle-CssClass="ui-widget-header" >
																					<ItemTemplate>
																						&nbsp;<asp:Label ID="lblPRNo" runat="server" Text='<%# Bind("PR_No") %>' CommandName="AddItemRow" Font-Bold="True"></asp:Label>
																					</ItemTemplate>
																				</asp:TemplateField>
																			</Columns>
																		</asp:GridView>
																
																<asp:SqlDataSource ID="dsItems" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>'
																	SelectCommand="SELECT DISTINCT BidDetailNo, ItemName, SKU, PR_No FROM tblBACEvaluationDetails WHERE BidRefNo = @BidRefNo ORDER BY BidDetailNo, ItemName, SKU, PR_No" >
																	<SelectParameters>
                                                                        <asp:SessionParameter Name="BidRefNo" SessionField="BuyerBidForBac" Type="Int32" />
																	</SelectParameters>
																</asp:SqlDataSource>