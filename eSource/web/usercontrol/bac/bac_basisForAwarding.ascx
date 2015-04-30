<%@ Control Language="C#" AutoEventWireup="true" CodeFile="bac_basisForAwarding.ascx.cs" Inherits="web_usercontrol_bac_bac_basisForAwarding" %>
<table width="60%" border="1" cellpadding="0" cellspacing="0" rules="all" class="itemDetails" id="Biddetails_details1_dvEventDetails3" style="border-color:#71A9D2; font-family: Arial; font-size: 11px; border-collapse: collapse; background-color: #F4F1C4;">
																<tbody>
																	<tr>
																		<td class="ui-widget-header" style="height:26px; vertical-align:middle;">BASIS FOR AWARDING</td>
																	</tr>
																	<tr valign="middle">
																		<td align="center" valign="middle" style="width:25%;">
																			<table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                                  <tr>
                                                                    <td width="50%" align="right">
                                                                        <a href="javascript:void(0)" onclick="collapseAll_BFA()" id="collapseAll_BFA">Collapse All</a>
<script type="text/javascript">
    function collapseAll_BFA() {
        if ($('#collapseAll_BFA').html() == "Collapse All") {
            $('#collapseAll_BFA').html('Expand All');
            $('#collapseLowestPr').hide();
            $('#collapseSoleLOA').hide();
            $('#collapseSoleCSD').hide();
            $('#collapseTechSpec').hide();
            $('#collapseLeadTime').hide();
            $('#collapseBFAOth').hide();
        } else {
            $('#collapseAll_BFA').html('Collapse All');
            $('#collapseLowestPr').show();
            $('#collapseSoleLOA').show();
            $('#collapseSoleCSD').show();
            $('#collapseTechSpec').show();
            $('#collapseLeadTime').show();
            $('#collapseBFAOth').show();
        }

        showhideBT2();

    }
    function showhideBT2() {
        $('#collapseLowestPr').is(':visible') ? $('#btcollapseLowestPr').html('- Lowest-Price Bidder') : $('#btcollapseLowestPr').html('+ Lowest-Price Bidder');
        $('#collapseSoleLOA').is(':visible') ? $('#btcollapseSoleLOA').html('- Sole Source w/ LOA Approved Waiver of Competition') : $('#btcollapseSoleLOA').html('+ Sole Source w/ LOA Approved Waiver of Competition');
        $('#collapseSoleCSD').is(':visible') ? $('#btcollapseSoleCSD').html('- Sole Source w/ Certificate of Sole Distributorship') : $('#btcollapseSoleCSD').html('+ Sole Source w/ Certificate of Sole Distributorship');
        $('#collapseTechSpec').is(':visible') ? $('#btcollapseTechSpec').html('- Compliance to Technical/ Functional Specifications') : $('#btcollapseTechSpec').html('+ Compliance to Technical/ Functional Specifications');
        $('#collapseLeadTime').is(':visible') ? $('#btcollapseLeadTime').html('- Compliance to Delivery Lead-Time') : $('#btcollapseLeadTime').html('+ Compliance to Delivery Lead-Time');
        $('#collapseBFAOth').is(':visible') ? $('#btcollapseBFAOth').html('- Others') : $('#btcollapseBFAOth').html('+ Others');
    }
</script>
                                                                        </td>
                                                                  </tr>
                                                                  <tr>
                                                                    <td class="style7"><table style="width: 100%;">                                                                        <tr>                                                                <td style="border:none"><strong><a id="btcollapseLowestPr" href="javascript:void(0)" onclick="$('#collapseLowestPr').toggle(); showhideBT2();" style="color:Black;">- Lowest-Price Bidder</a> </strong></td>
                                                                    <td width="15" style="border:none"><b>(<asp:Label ID="bfa_LowestPr_count" runat="server"></asp:Label>)</b></td>
                                                                    </tr>
                                                                    </table>
                                                                        <div id="collapseLowestPr">
                                                                            <asp:Repeater ID="Repeater_BFA_LowestPr" runat="server" DataSourceID="dsBFA_LowestPr" >
                                                                                <ItemTemplate>
                                                                                    <a id="lnkBFA_LowestPr" href="<%# Session["pageDetails"] %>?ShowAttachment=<%# Eval("FileUploadID") %>"
                                                                                        target="_blank" title='<%# Eval("ActualFileName") %>'>
                                                                                        <%# Eval("OriginalFileName") %></a><br />
                                                                                </ItemTemplate>
                                                                            </asp:Repeater>
                                                                        </div id="collapseSoleLOA">
                                                                        <asp:SqlDataSource ID="dsBFA_LowestPr" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>'
                                                                            SelectCommand="SELECT * FROM tblBACSupportingDocuments WHERE BidRefNo=@BidRefNo AND DocuName='bfa_LowestPr' ORDER BY FileUploadID" >
                                                                            <SelectParameters>
                                                                                <asp:SessionParameter Name="BuyerId" SessionField="UserId" Type="Int32" />
                                                                                <asp:SessionParameter Name="BidRefNo" SessionField="BuyerBidForBac" Type="Int32" />
                                                                            </SelectParameters>
                                                                        </asp:SqlDataSource>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td width="50%"><table style="width: 100%;">                                                                        <tr>                                                                <td style="border:none"><strong><a id="btcollapseSoleLOA" href="javascript:void(0)" onclick="$('#collapseSoleLOA').toggle(); showhideBT2();" style="color:Black;">- Sole Source w/ LOA Approved Waiver of Competition</a> </strong></td>
                                                                    <td width="15" style="border:none"><b>(<asp:Label ID="bfa_SoleLOA_count" runat="server"></asp:Label>)</b></td>
                                                                    </tr>
                                                                    </table>
                                                                        <div id="collapseSoleLOA">
                                                                            <asp:Repeater ID="Repeater_BFA_SoleLOA" runat="server" DataSourceID="dsBFA_SoleLOA" >
                                                                                <ItemTemplate>
                                                                                    <a href="<%# Session["pageDetails"] %>?ShowAttachment=<%# Eval("FileUploadID") %>"
                                                                                        target="_blank" title='<%# Eval("ActualFileName") %>'>
                                                                                        <%# Eval("OriginalFileName") %></a><br />
                                                                                </ItemTemplate>
                                                                            </asp:Repeater>
                                                                        </div>
                                                                        <asp:SqlDataSource ID="dsBFA_SoleLOA" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>' 
                                                                            
                                                                            SelectCommand="SELECT * FROM tblBACSupportingDocuments WHERE BidRefNo=@BidRefNo AND DocuName='bfa_SoleLOA' ORDER BY FileUploadID" >
                                                                            <SelectParameters>
                                                                                <asp:SessionParameter Name="BuyerId" SessionField="UserId" Type="Int32" />
                                                                                <asp:SessionParameter Name="BidRefNo" SessionField="BuyerBidForBac" Type="Int32" />
                                                                            </SelectParameters>
                                                                        </asp:SqlDataSource>
                                                                    </td>
                                                                  </tr>
                                                                  <tr>
                                                                    <td class="style7"><table style="width: 100%;">                                                                        <tr>                                                                <td style="border:none"><strong><a id="btcollapseSoleCSD" href="javascript:void(0)" onclick="$('#collapseSoleCSD').toggle(); showhideBT2();" style="color:Black;">- Sole Source w/ Certificate of Sole Distributorship</a> </strong></td>
                                                                    <td width="15" style="border:none"><b>(<asp:Label ID="bfa_SoleCSD_count" runat="server"></asp:Label>)</b></td>
                                                                    </tr>
                                                                    </table>
                                                                        <div id="collapseSoleCSD">
                                                                            <asp:Repeater ID="Repeater_BFA_SoleCSD" runat="server" DataSourceID="dsBFA_SoleCSD" >
                                                                                <ItemTemplate>
                                                                                    <a href="<%# Session["pageDetails"] %>?ShowAttachment=<%# Eval("FileUploadID") %>"
                                                                                        target="_blank" title='<%# Eval("ActualFileName") %>'>
                                                                                        <%# Eval("OriginalFileName") %></a><br />
                                                                                </ItemTemplate>
                                                                            </asp:Repeater>
                                                                        </div>
                                                                        <asp:SqlDataSource ID="dsBFA_SoleCSD" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>' 
                                                                            
                                                                            SelectCommand="SELECT * FROM tblBACSupportingDocuments WHERE  BidRefNo=@BidRefNo AND DocuName='bfa_SoleCSD' ORDER BY FileUploadID" >
                                                                            <SelectParameters>
                                                                                <asp:SessionParameter Name="BuyerId" SessionField="UserId" Type="Int32" />
                                                                                <asp:SessionParameter Name="BidRefNo" SessionField="BuyerBidForBac" Type="Int32" />
                                                                            </SelectParameters>
                                                                        </asp:SqlDataSource>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td><table style="width: 100%;">                                                                        <tr>                                                                <td style="border:none"><strong><a id="btcollapseTechSpec" href="javascript:void(0)" onclick="$('#collapseTechSpec').toggle(); showhideBT2();" style="color:Black;" >- Compliance to Technical/ Functional Specifications</a> </strong></td>
                                                                    <td width="15" style="border:none"><b>(<asp:Label ID="bfa_TechSpec_count" runat="server"></asp:Label>)</b></td>
                                                                    </tr>
                                                                    </table>
                                                                        <div id="collapseTechSpec">
                                                                            <asp:Repeater ID="Repeater_BFA_TechSpec" runat="server" DataSourceID="dsBFA_TechSpec" >
                                                                                <ItemTemplate>
                                                                                    <a href="<%# Session["pageDetails"] %>?ShowAttachment=<%# Eval("FileUploadID") %>"
                                                                                        target="_blank" title='<%# Eval("ActualFileName") %>'>
                                                                                        <%# Eval("OriginalFileName") %></a><br />
                                                                                </ItemTemplate>
                                                                            </asp:Repeater>
                                                                        </div>
                                                                        <asp:SqlDataSource ID="dsBFA_TechSpec" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>' 
                                                                            
                                                                            SelectCommand="SELECT * FROM tblBACSupportingDocuments WHERE BidRefNo=@BidRefNo AND DocuName='bfa_TechSpec' ORDER BY FileUploadID" >
                                                                            <SelectParameters>
                                                                                <asp:SessionParameter Name="BuyerId" SessionField="UserId" Type="Int32" />
                                                                                <asp:SessionParameter Name="BidRefNo" SessionField="BuyerBidForBac" Type="Int32" />
                                                                            </SelectParameters>
                                                                        </asp:SqlDataSource>
                                                                    </td>
                                                                  </tr>
                                                                  <tr>
                                                                    <td class="style7"><table style="width: 100%;">                                                                        <tr>                                                                <td style="border:none"><strong><a id="btcollapseLeadTime" href="javascript:void(0)" onclick="$('#collapseLeadTime').toggle(); showhideBT2();" style="color:Black;" >- Compliance to Delivery Lead-Time</a> </strong></td>
                                                                    <td width="15" style="border:none"><b>(<asp:Label ID="bfa_LeadTime_count" runat="server"></asp:Label>)</b></td>
                                                                    </tr>
                                                                    </table>
                                                                        <div id="collapseLeadTime">
                                                                            <asp:Repeater ID="Repeater_BFA_LeadTime" runat="server" DataSourceID="dsBFA_LeadTime" >
                                                                                <ItemTemplate>
                                                                                    <a href="<%# Session["pageDetails"] %>?ShowAttachment=<%# Eval("FileUploadID") %>"
                                                                                        target="_blank" title='<%# Eval("ActualFileName") %>'>
                                                                                        <%# Eval("OriginalFileName") %></a><br />
                                                                                </ItemTemplate>
                                                                            </asp:Repeater>
                                                                        </div>
                                                                        <asp:SqlDataSource ID="dsBFA_LeadTime" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>' 
                                                                            
                                                                            SelectCommand="SELECT * FROM tblBACSupportingDocuments WHERE BidRefNo=@BidRefNo AND DocuName='bfa_LeadTime' ORDER BY FileUploadID" >
                                                                            <SelectParameters>
                                                                                <asp:SessionParameter Name="BuyerId" SessionField="UserId" Type="Int32" />
                                                                                <asp:SessionParameter Name="BidRefNo" SessionField="BuyerBidForBac" Type="Int32" />
                                                                            </SelectParameters>
                                                                        </asp:SqlDataSource>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td><table style="width: 100%;">                                                                        <tr>                                                                <td style="border:none"><strong><a id="btcollapseBFAOth" href="javascript:void(0)" onclick="$('#collapseBFAOth').toggle(); showhideBT2();" style="color:Black;">- Others</a> </strong></td>
                                                                    <td width="15" style="border:none"><b>(<asp:Label ID="bfa_BFAOth_count" runat="server"></asp:Label>)</b></td>
                                                                    </tr>
                                                                    </table>
                                                                        <div id="collapseBFAOth">
                                                                            <asp:Repeater ID="Repeater_BFAOth" runat="server" DataSourceID="dsBFAOth" >
                                                                                <ItemTemplate>
                                                                                    <a href="<%# Session["pageDetails"] %>?ShowAttachment=<%# Eval("FileUploadID") %>"
                                                                                        target="_blank" title='<%# Eval("ActualFileName") %>'>
                                                                                        <%# Eval("OriginalFileName") %></a><br />
                                                                                </ItemTemplate>
                                                                            </asp:Repeater>
                                                                        </div id="collapseOth">
                                                                        <asp:SqlDataSource ID="dsBFAOth" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>' 
                                                                            
                                                                            SelectCommand="SELECT * FROM tblBACSupportingDocuments WHERE BidRefNo=@BidRefNo AND DocuName='bfa_BFAOth' ORDER BY FileUploadID" >
                                                                            <SelectParameters>
                                                                                <asp:SessionParameter Name="BuyerId" SessionField="UserId" Type="Int32" />
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