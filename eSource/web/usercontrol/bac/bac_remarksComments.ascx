<%@ Control Language="C#" AutoEventWireup="true" CodeFile="bac_remarksComments.ascx.cs" Inherits="web_usercontrol_bac_bac_remarksComments" %>
<table width="100%" border="1" cellpadding="0" cellspacing="0" rules="all" class="itemDetails" id="Biddetails_details1_dvEventDetails8" style="border-color:#71A9D2; font-family: Arial; font-size: 11px; border-collapse: collapse; background-color: #F4F1C4; height:100%">
																<tbody>
																	<tr>
																		<td colspan="2" class="ui-widget-header" style="height:26px; vertical-align:middle;">REMARKS / COMMENTS</td>
																	</tr>
																	<tr valign="middle" >
																		<td align="left" valign="middle" style="padding:5px; font-size:12px; width:50%">
                                                                        <div style="height:150px; overflow:scroll;">
																			<asp:DataList runat="server"  DataSourceID="dsBACComments" ID="DataList7" >
                                                                                  <ItemTemplate>
                                                                                  <div style="margin-bottom:15px;">
                                                                                  <b><asp:label runat="server" ID="comments" Name="comments" Text='<%# Eval("FrName") %>' ></asp:label></b>
                                                                                  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                                  <asp:label runat="server" ID="Label2" Name="comments" Text='<%# Eval("DatePosted") %>' ></asp:label>
                                                                                  </b><br />
                                                                                  <asp:label runat="server" ID="Label3" Name="comments" Text='<%# Eval("Comment") %>' ></asp:label>
                                                                                  </div>
                                                                                  </ItemTemplate>
                                                                             </asp:DataList>
                                                                            <asp:SqlDataSource ID="dsBACComments" runat="server" 
                                                                                ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>'
                                                                                SelectCommand="SELECT A.FrUserId, B.Name1 FrName, A.Comment, A.ToUserId, C.Name1 ToName, A.DatePosted FROM
                                                                                (SELECT FrUserId, Comment, ToUserId, DatePosted FROM tblBACClarifications
                                                                                WHERE BidRefNo=@BidRefNo AND (Comment IS NOT NULL )
                                                                                UNION ALL
                                                                                SELECT UserId FrUserId, Comment, UserId ToUserId, DatePosted FROM tblBACComments
                                                                                WHERE BidRefNo=@BidRefNo AND (Comment IS NOT NULL)
                                                                                ) AS A
                                                                                INNER JOIN
                                                                                (SELECT BuyerId UserId, BuyerLastName + ', ' + BuyerFirstName + ' ' + BuyerMidName AS Name1 FROM tblBuyers
                                                                                UNION ALL
                                                                                SELECT PurchasingID UserId, LastName + ', ' + FirstName + ' ' + MiddleName AS Name1 FROM tblPurchasing
                                                                                UNION ALL
                                                                                SELECT BACId UserId, LastName + ', ' + FirstName + ' ' + MiddleName AS Name1 FROM tblBidAwardingCommittee)
                                                                                B ON A.FrUserId=B.UserId
                                                                                INNER JOIN
                                                                                (SELECT BuyerId UserId, BuyerLastName + ', ' + BuyerFirstName + ' ' + BuyerMidName AS Name1 FROM tblBuyers
                                                                                UNION ALL
                                                                                SELECT PurchasingID UserId, LastName + ', ' + FirstName + ' ' + MiddleName AS Name1 FROM tblPurchasing
                                                                                UNION ALL
                                                                                SELECT BACId UserId, LastName + ', ' + FirstName + ' ' + MiddleName AS Name1 FROM tblBidAwardingCommittee)
                                                                                C ON A.ToUserId=C.UserId
                                                                                ORDER BY DatePosted DESC" >
																				<SelectParameters>
																					<asp:SessionParameter Name="BidRefNo" SessionField="BuyerBidForBac" Type="Int32" />
																				</SelectParameters>
																			</asp:SqlDataSource>
																			</div>
																		</td>
																	</tr>
																</tbody>
															</table>