<%@ Control Language="C#" AutoEventWireup="true" CodeFile="bac_executiveSummary.ascx.cs" Inherits="web_usercontrol_bac_executiveSummary" %>
<table width="100%" border="1" cellpadding="0" cellspacing="0" rules="all" class="itemDetails" id="Biddetails_details1_dvEventDetails9" style="border-color:#71A9D2; font-family: Arial; font-size: 11px; border-collapse: collapse; background-color: #F4F1C4; height:100%">
																<tbody>
																	<tr>
																		<td class="ui-widget-header" style="height:26px; vertical-align:middle;">EXECUTIVE SUMMARY</td>
																	</tr>
																	<tr valign="middle" >
																		<td align="left" valign="middle" style="padding:5px; font-size:12px; width:50%"><label for="textarea"></label>
																			<%--<textarea name="executive_summary" id="executive_summary" cols="45" rows="10" runat="server" style="width:100%"></textarea>--%>
                                                                            <asp:DataList runat="server"  DataSourceID="dsBACExecutiveSummary" 
                                                                                ID="DataList6" >
                                                                                  <ItemTemplate>
                                                                                  <asp:label runat="server" ID="executive_summary" Name="executive_summary" Text='<%# Eval("Comment")!="" ? Eval("Comment"):"No Summary." %>' Font-Bold="True">
                                                                                  </asp:label>
                                                                                  </ItemTemplate>
                                                                                  </asp:DataList>
                                                                            <asp:SqlDataSource ID="dsBACExecutiveSummary" runat="server" 
                                                                            ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>'
                                                                            SelectCommand="SELECT * FROM tblBACExecutiveSummary WHERE BidRefNo=@BidRefNo" >
																					<SelectParameters>
																						<asp:SessionParameter Name="BidRefNo" SessionField="BuyerBidForBac" Type="Int32" />
																					</SelectParameters>
																			</asp:SqlDataSource>

																		</td>
																	</tr>
																</tbody>
															</table>