<%@ Control Language="C#" AutoEventWireup="true" CodeFile="bac_sourcingStrategy.ascx.cs" Inherits="web_usercontrol_bac_sourcingStrategy" %>
<table width="100%" border="1" cellpadding="0" cellspacing="0" rules="all" class="itemDetails" id="Biddetails_details1_dvEventDetails5" style="border-color:#71A9D2; font-family: Arial; font-size: 11px; border-collapse: collapse; background-color: #F4F1C4; min-height:125px; width:99%">
																<tbody>
																	<tr>
																		<td class="ui-widget-header" style="height:26px; vertical-align:middle;">SOURCING STRATEGY</td>
																	</tr>
																	<tr valign="middle">
																		<td align="center" valign="middle" style="width:50%;">
																			<table width="100%" border="0" cellspacing="0" cellpadding="0">
																				<tr>
																					<td width="50%" style="border:none; min-height:80px; display:block; "> 
                                                                                    <asp:DataList runat="server"  DataSourceID="dsBACSourcingStrategy" ID="DataList12" 
                                                                                            Font-Bold="True" >
                                                                                                      <ItemTemplate>
                                                                                                      <asp:label runat="server" ID="SourcingStrategy" Name="SourcingStrategy" Text='<%# Eval("SourcingStrategy") %>'  >
                                                                                                      </asp:label>
                                                                                                      </ItemTemplate>
                                                                                                 </asp:DataList>
                                                                                    <asp:SqlDataSource ID="dsBACSourcingStrategy" runat="server" 
                                                                            ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>'
                                                                            SelectCommand="SELECT SourcingStrategy FROM tblBACSourcingStrategy WHERE BidRefNo=@BidRefNo" >
																					<SelectParameters>
																						<asp:SessionParameter Name="BidRefNo" SessionField="BuyerBidForBac" Type="Int32" />
																					</SelectParameters>
                                                                                    </asp:SqlDataSource>
	    
																					</td>
																					<td style="border:none" width="50%">
																						<table>
																							<tr><td style="border:none">
                                                                                                <asp:DataList runat="server"  DataSourceID="dsBuyerBidForBac" ID="DataList9" Font-Bold="True" >
                                                                                                      <ItemTemplate>
                                                                                                      <td>
                                                                                                          <asp:label runat="server" ID="ss_nobidders" Name="ss_nobidders" Text="No. of Invited Bidders:&nbsp;"></asp:label>
                                                                                                      </td>
                                                                                                      <td>
                                                                                                          <asp:label runat="server" ID="ss_nobidders1" Name="ss_nobidders1" Text='<%# Eval("SS_Nobidders") %>'></asp:label>
                                                                                                      </td>
                                                                                                      </ItemTemplate>
                                                                                                 </asp:DataList>
																							</td></tr>
																							<tr><td style="border:none">
                                                                                                <asp:DataList runat="server"  DataSourceID="dsBuyerBidForBac" ID="DataList10" Font-Bold="True" >
                                                                                                      <ItemTemplate>
                                                                                                      <td>
                                                                                                          <asp:label runat="server" ID="ss_nobidsrcvd" Name="ss_nobidsrcvd" Text="No. of Bids Received:&nbsp;"></asp:label>
                                                                                                      </td>
                                                                                                      <td>
                                                                                                          <asp:label runat="server" ID="ss_nobidsrcvd1" Name="ss_nobidsrcvd1" Text='<%# Eval("SS_NoBidsRcvd") %>'></asp:label>
                                                                                                      </td>
                                                                                                      </ItemTemplate>
                                                                                                 </asp:DataList>
																							</td></tr>
																							<tr><td style="border:none">
                                                                                                <asp:DataList runat="server"  DataSourceID="dsBuyerBidForBac" ID="DataList11" Font-Bold="True" >
                                                                                                      <ItemTemplate>
                                                                                                      <td>
                                                                                                          <asp:label runat="server" ID="ss_techcompliance" Name="ss_techcompliance" Text="Tech. Compliance:&nbsp;"></asp:label>
                                                                                                      </td>
                                                                                                      <td>
                                                                                                          <asp:label runat="server" ID="ss_techcompliance1" Name="ss_techcompliance1" Text='<%# Eval("SS_TechCompliance") %>'></asp:label>
                                                                                                      </td>
                                                                                                      </ItemTemplate>
                                                                                                 </asp:DataList>
																							</td></tr>
																							<tr><td style="border:none">
                                                                                                <asp:DataList runat="server"  DataSourceID="dsBuyerBidForBac" ID="DataList13" Font-Bold="True" >
                                                                                                      <ItemTemplate>
                                                                                                      <td>
                                                                                                          <asp:label runat="server" ID="ss_crcnumber" Name="ss_crcnumber" Text="CRC No.:&nbsp;"></asp:label>
                                                                                                      </td>
                                                                                                      <td>
                                                                                                          <asp:label runat="server" ID="ss_crcnumber1" Name="ss_crcnumber1" Text='<%# Eval("ss_crc_no") %>'></asp:label>
                                                                                                      </td>
                                                                                                      </ItemTemplate>
                                                                                                 </asp:DataList>
																							</td></tr>

			</table>
																					</td>
																				</tr>
																			</table>
																		</td>
																	</tr>
																	<tr><td>
																		<%--<table id="mySourcingStrategy" runat="server"></table>--%>
																	</td></tr>
																</tbody>
															</table>