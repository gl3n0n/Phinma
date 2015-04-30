<%@ Control Language="C#" AutoEventWireup="true" CodeFile="bac_bidEvaluationDetails.ascx.cs" Inherits="web_usercontrol_bac_bac_bidEvaluationDetails" %>
<table width="99%" border="1" cellpadding="0" cellspacing="0" rules="all" class="itemDetails" id="Biddetails_details1_dvEventDetails10" style="border-color:#71A9D2; font-family: Arial; font-size: 11px; border-collapse: collapse; background-color: #F4F1C4; height:100%" runat="server">
	<tbody>
		<tr>
			<td class="ui-widget-header" style="height:26px; vertical-align:middle;">
				BID EVALUATION SUMMARY
				<input id="txtNewVendor" type="hidden" name="txtNewVendor" runat="server" />
				<input id="txtCDVendor" type="hidden" name="txtCDVendor" runat="server" />
				<input id="txtNewRow" type="hidden" name="txtNewRow" runat="server" />
                <input id="txtHasZeroID" type="hidden" name="txtHasZeroID" runat="server" />
			</td>
		</tr>
		<tr>
			<td style="padding:5px; font-size:12px; width:50%">
				<script type="text/javascript">
					$(window).resize(function () {
						$("#divContainer1").width($(window).width() - 310);
					});
					$(document).ready(function () {
						$("#divContainer1").width($(window).width() - 310);
					});
            </script>
				<div style="overflow:auto; padding:0px" id="divContainer1">
                                                                                
                    <div id="divContainer2" style="margin:0; ">
                    <table width="100%" border="0" cellspacing="0" cellpadding="0" id="tblCriteria" runat="server">
						<tr bgcolor="#E4EFF8">
							<td>
								<strong>VENDOR / CRITERIA</strong>
                                <asp:Repeater id="Repeater4" runat="server" DataSourceID="dsBidItemTendersSummary" >
								<ItemTemplate>
									<td><div style="max-width:150px;">
										<asp:Label ID="txtVendorName" runat="server" Text='<%# Eval("VendorName") %>'  Font-Bold="True"></asp:Label>
									</div></td>
								</ItemTemplate>
							</asp:Repeater>
							</td>
						</tr>
						<tr>
							<td>
								<strong>Accreditation Status</strong>
                                <asp:Repeater id="Repeater100" runat="server" DataSourceID="dsBidItemTendersSummary">
								<ItemTemplate>
									<td>
                                    <asp:Label ID="cbAccreditation" runat="server" Text='<%# Eval("Accredited").ToString()=="1"?"Accredited":Eval("Accredited").ToString()=="2"?"Unaccredited":Eval("Accredited").ToString()=="3"?"One Time Supplier":Eval("Accredited").ToString()=="4"?"Exempted":Eval("Accredited").ToString()=="5"?"Due For Renewal":"" %>' ></asp:Label>
									</td>
								</ItemTemplate>
							</asp:Repeater>
							</td>
						</tr>
						<tr bgcolor="#E4EFF8">
							<td>
								<b>Performance Rating</b>
                                <asp:Repeater id="RepeaterPRating" runat="server" DataSourceID="dsBidItemTendersSummary">
								<ItemTemplate>
									<td>
										<asp:Label ID="txtPRating" runat="server" Text='<%# Eval("PerformanceRating") %>'  ></asp:Label>
									</td>
								</ItemTemplate>
							</asp:Repeater>
							</td>
						</tr>
						<tr>
							<td>
                                <b>Technical Compliance</b>
                                <asp:Repeater id="Repeater5" runat="server" DataSourceID="dsBidItemTendersSummary">
								    <ItemTemplate>
									    <td><asp:Label ID="txtTechnical" runat="server" Text='<%# Eval("TechCompliance").ToString()=="1" ?"Not-Compliant":Eval("TechCompliance").ToString()=="2" ?"Fully Compliant":"" %>' ></asp:Label></td>
								    </ItemTemplate>
							    </asp:Repeater>
                            </td>
						</tr>
						<tr bgcolor="#E4EFF8">
							<td>
                                <b>Commercial Compliance</b>
                                <asp:Repeater id="Repeater6" runat="server" DataSourceID="dsBidItemTendersSummary">
								<ItemTemplate>
									<td><asp:Label ID="txtCommercial" runat="server" Text='<%# Eval("CommCompliance").ToString()=="1"?"Not-Compliant":Eval("CommCompliance").ToString()=="2"?"Partial Compliant":Eval("CommCompliance").ToString()=="3"?"Fully Compliant":"" %>'></asp:Label></td>
								</ItemTemplate>
							</asp:Repeater>
                            </td>
						</tr>
						<tr>
							<td>
                                <b>Contract Compliance</b>
                                <asp:Repeater id="Repeater7" runat="server" DataSourceID="dsBidItemTendersSummary">
								<ItemTemplate>
									<td><asp:Label ID="txtContract" runat="server"  Text='<%# Eval("ContCompliance").ToString()=="1" ?"Not-Applicable":Eval("ContCompliance").ToString()=="2" ?"Not-Compliant":Eval("ContCompliance").ToString()=="3" ?"Partial Compliant":Eval("ContCompliance").ToString()=="4" ?"Fully Compliant":"" %>' ></asp:Label></td>
								</ItemTemplate>
							</asp:Repeater>
                            </td>
						</tr>
					</table>
																				
				</div></div>
			</td>
		</tr>
	</tbody>
</table>
<asp:SqlDataSource ID="dsBidItemTendersSummary" runat="server"
	ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>'
	SelectCommand="SELECT * FROM tblBACEvaluationSummary WHERE BidRefNo=@BidRefNo ORDER BY VendorName ASC">
	<SelectParameters>
		<asp:SessionParameter Name="BidRefNo" SessionField="BuyerBidForBac" Type="Int32" />
	</SelectParameters>
</asp:SqlDataSource>
																			
<asp:SqlDataSource ID="dsAccreditation" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>'
	SelectCommand="SELECT * FROM rfcSupplierType ORDER BY SupplierTypeId" >
</asp:SqlDataSource>