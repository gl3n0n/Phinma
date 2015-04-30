<table width="100%" border="1" cellpadding="0" cellspacing="0" rules="all" class="itemDetails" id="Table4" style="border-color:#71A9D2; font-family: Arial; font-size: 11px; border-collapse: collapse; background-color: #F4F1C4; height:100%; margin-bottom:2px;">
																<tbody>
																	<tr>
																		<td class="ui-widget-header" style="height:26px; vertical-align:middle;">
																			AWARDED TO
																		</td>
																	</tr>
																</tbody>
															</table>
                                                            <asp:GridView ID="gvAwarded" SkinID="BidEvents" runat="server" AllowPaging="False" 
                                                                            AllowSorting="False" AutoGenerateColumns="False" DataSourceID="dsAwarded"
																			DataKeyNames="BidDetailNo" BorderColor="#71A9D2" BorderStyle="Solid" BorderWidth="2px" CellPadding="3" Font-Size="12px" 
                                                                Width="99%" Font-Overline="False" >
																			<Columns>
                                                                                <asp:TemplateField HeaderText="Item #" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
																					<HeaderStyle HorizontalAlign="Center" CssClass="ui-widget-header" />
																					<ItemTemplate>
																						&nbsp;<asp:Label ID="lblItemNo_" runat="server" Text='<%# Bind("BidDetailNo") %>' style="font-weight:bold;"></asp:Label>
																					</ItemTemplate>
																				</asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Item Details" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
																					<HeaderStyle HorizontalAlign="Center" CssClass="ui-widget-header" />
																					<ItemTemplate>
																						&nbsp;<asp:Label ID="lblItemD_" runat="server" Text='<%# Bind("ItemName") %>'></asp:Label>
																					</ItemTemplate>
																				</asp:TemplateField>
																				<asp:TemplateField HeaderText="Vendor Name" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
																					<HeaderStyle HorizontalAlign="Center" CssClass="ui-widget-header" />
																					<ItemTemplate>
																						&nbsp;<asp:Label ID="lblVendorName_" runat="server" Text='<%# Bind("VendorName") %>'></asp:Label>
																					</ItemTemplate>
																				</asp:TemplateField>
																				<asp:TemplateField HeaderText="Awarded Qty" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle"  ItemStyle-HorizontalAlign="Center">
																					<HeaderStyle HorizontalAlign="Center" CssClass="ui-widget-header" />
																					<ItemTemplate>
																						&nbsp;<asp:Label ID="lblQty_" runat="server" Text='<%# Bind("Qty") %>'></asp:Label>
																					</ItemTemplate>
																				</asp:TemplateField>
																				<asp:TemplateField HeaderText="Total" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center">
																					<HeaderStyle HorizontalAlign="Center" CssClass="ui-widget-header" />
																					<ItemTemplate>
																						&nbsp;<asp:Label ID="lblBidPrice_" runat="server" Text='<%# String.Format("{0:0,0.00}", Eval("TotalCost")) %>' Font-Bold="True"></asp:Label>
																					</ItemTemplate>
																				</asp:TemplateField>
																				<asp:TemplateField HeaderText="Ranking" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center">
																					<HeaderStyle HorizontalAlign="Center" CssClass="ui-widget-header" />
																					<ItemTemplate>
																						&nbsp;<asp:Label ID="txtRanking_" runat="server" Text='<%# Bind("Ranking") %>'></asp:Label>
																					</ItemTemplate>
																				</asp:TemplateField>
																			</Columns>
																		    <HeaderStyle Font-Bold="True" Font-Size="12px" Height="25px" />
																		</asp:GridView>
                                                            <asp:SqlDataSource ID="dsAwarded" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>'
																	SelectCommand="SELECT * FROM tblBACEvaluationDetails WHERE BidRefNo=@BidRefNo AND Chkd=1" >
																	<SelectParameters>
																		<asp:SessionParameter Name="BidRefNo" SessionField="BuyerBidForBac" Type="Int32" />
																	</SelectParameters>
																</asp:SqlDataSource>