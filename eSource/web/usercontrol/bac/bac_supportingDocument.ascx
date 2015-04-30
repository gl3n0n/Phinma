<%@ Control Language="C#" AutoEventWireup="true" CodeFile="bac_supportingDocument.ascx.cs" Inherits="web_usercontrol_bac_bac_supportingDocument" %>
<table border="1" cellpadding="0" cellspacing="0" rules="all" class="itemDetails" id="Biddetails_details1_dvEventDetails2" style="border-color:#71A9D2; font-family: Arial; font-size: 11px; border-collapse: collapse; background-color: #F4F1C4;">
																<tbody>
																	<tr>
																		<td class="ui-widget-header" style="height:26px; vertical-align:middle;">SUPPORTING DOCUMENTS ATTACHED</td>
																	</tr>
																	<tr valign="middle" >
																		<td align="center" valign="middle" style="padding:5px; font-size:12px; width:50%; ">
                                                                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                                  <tr>
                                                                    <td width="50%" align="right">
                                                                        <a href="javascript:void(0)" onclick="collapseAll_SPD()" id="collapseAll_SPD">Collapse All</a>
<script type="text/javascript">
    function collapseAll_SPD() {
        if ($('#collapseAll_SPD').html() == "Collapse All") {
            $('#collapseAll_SPD').html('Expand All');
            $('#collapseAPR').hide();
            $('#collapseCE').hide();
            $('#collapseABC').hide();
            $('#collapseNR').hide();
            $('#collapseBR').hide();
            $('#collapseOth').hide();
            $('#collapseTE').hide();
        } else {
            $('#collapseAll_SPD').html('Collapse All');
            $('#collapseAPR').show();
            $('#collapseCE').show();
            $('#collapseABC').show();
            $('#collapseNR').show();
            $('#collapseBR').show();
            $('#collapseOth').show();
            $('#collapseTE').show();
        }

        showhideBT();

    }
    function showhideBT() {
        $('#collapseAPR').is(':visible') ? $('#btcollapseAPR').html('- Approved SAP PR') : $('#btcollapseAPR').html('+ Approved SAP PR');
        $('#collapseCE').is(':visible') ? $('#btcollapseCE').html('- Commercial Evaluation') : $('#btcollapseCE').html('+ Commercial Evaluation');
        $('#collapseABC').is(':visible') ? $('#btcollapseABC').html('- Approved Business Case') : $('#btcollapseABC').html('+ Approved Business Case');
        $('#collapseNR').is(':visible') ? $('#btcollapseNR').html('- Negotiation Results') : $('#btcollapseNR').html('+ Negotiation Results');
        $('#collapseBR').is(':visible') ? $('#btcollapseBR').html('- Board Resolution') : $('#btcollapseBR').html('+ Board Resolution');
        $('#collapseOth').is(':visible') ? $('#btcollapseOth').html('- Others') : $('#btcollapseOth').html('+ Others');
        $('#collapseTE').is(':visible') ? $('#btcollapseTE').html('- Technical Evaluation') : $('#btcollapseTE').html('+ Technical Evaluation');
    }
</script>
                                                                        </td>
                                                                  </tr>
                                                                  <tr>
                                                                    <td width="50%">
                                                                    <table style="width: 100%;">                                                                        <tr>                                                                <td style="border:none"><strong><a id="btcollapseAPR" href="javascript:void(0)" onclick="$('#collapseAPR').toggle(); showhideBT();" style="color:Black" >- Approved SAP PR</a> </strong></td>
                                                                    <td width="15" style="border:none"><b>(<asp:Label ID="SDA_APR_count" runat="server"></asp:Label>)</b></td>
                                                                    </tr>
                                                                    </table>
                                                                        <div id="collapseAPR">
                                                                            <asp:Repeater ID="Repeater_SDA_APR" runat="server" DataSourceID="dsSDA_APR" >
                                                                                <ItemTemplate>
                                                                                    <a id="lnkSDA_APR" href='<%# Session["pageDetails"] %>?ShowAttachment=<%# Eval("FileUploadID") %>'
                                                                                        target="_blank" title='<%# Eval("ActualFileName") %>'>
                                                                                        <%# Eval("OriginalFileName") %></a><br />
                                                                                </ItemTemplate>
                                                                            </asp:Repeater>
                                                                        </div id="collapseAPR">
                                                                        <asp:SqlDataSource ID="dsSDA_APR" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>'
                                                                            SelectCommand="SELECT * FROM tblBACSupportingDocuments WHERE BidRefNo=@BidRefNo AND DocuName='Approved_PR' ORDER BY FileUploadID"  >
                                                                            <SelectParameters>
                                                                                <asp:SessionParameter Name="BidRefNo" SessionField="BuyerBidForBac" Type="Int32" />
                                                                            </SelectParameters>
                                                                        </asp:SqlDataSource>
                                                                    </td>
                                                                </tr>
                                                                  <tr>
                                                                    <td>
                                                                    <table style="width: 100%;">                                                                        <tr>                                                                <td style="border:none"><strong><a id="btcollapseABC" href="javascript:void(0)" onclick="$('#collapseABC').toggle(); showhideBT();" style="color:Black" >- Approved Business Case</a> </strong></td>
                                                                    <td width="15" style="border:none"><b>(<asp:Label ID="SDA_ABC_count" runat="server"></asp:Label>)</b></td>
                                                                    </tr>
                                                                    </table>
                                                                        <div id="collapseABC">
                                                                            <asp:Repeater ID="Repeater_SDA_ABC" runat="server" DataSourceID="dsSDA_ABC" >
                                                                                <ItemTemplate>
                                                                                    <a href="<%# Session["pageDetails"] %>?ShowAttachment=<%# Eval("FileUploadID") %>"
                                                                                        target="_blank" title='<%# Eval("ActualFileName") %>'>
                                                                                        <%# Eval("OriginalFileName") %></a><br />
                                                                                </ItemTemplate>
                                                                            </asp:Repeater>
                                                                        </div id="collapseABC">
                                                                        <asp:SqlDataSource ID="dsSDA_ABC" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>' 
                                                                            
                                                                            SelectCommand="SELECT * FROM tblBACSupportingDocuments WHERE BidRefNo=@BidRefNo AND DocuName='Approved_Business_Case' ORDER BY FileUploadID" >
                                                                            <SelectParameters>
                                                                                <asp:SessionParameter Name="BidRefNo" SessionField="BuyerBidForBac" Type="Int32" />
                                                                            </SelectParameters>
                                                                        </asp:SqlDataSource>
                                                                    </td>
                                                                </tr>
                                                                  <tr>
                                                                    <td><table style="width: 100%;">                                                                        <tr>                                                                <td style="border:none"><strong><a id="btcollapseBR" href="javascript:void(0)" onclick="$('#collapseBR').toggle(); showhideBT();" style="color:Black" >- Board Resolution</a> </strong></td>
                                                                    <td width="15" style="border:none"><b>(<asp:Label ID="SDA_BR_count" runat="server"></asp:Label>)</b></td>
                                                                    </tr>
                                                                    </table>
                                                                        <div id="collapseBR">
                                                                            <asp:Repeater ID="Repeater_SDA_BR" runat="server" DataSourceID="dsSDA_BR">
                                                                                <ItemTemplate>
                                                                                    <a href="<%# Session["pageDetails"] %>?ShowAttachment=<%# Eval("FileUploadID") %>"
                                                                                        target="_blank" title='<%# Eval("ActualFileName") %>'>
                                                                                        <%# Eval("OriginalFileName") %></a><br />
                                                                                </ItemTemplate>
                                                                            </asp:Repeater>
                                                                        </div id="collapseBR">
                                                                        <asp:SqlDataSource ID="dsSDA_BR" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>' 
                                                                            
                                                                            SelectCommand="SELECT * FROM tblBACSupportingDocuments WHERE BidRefNo=@BidRefNo AND DocuName='Board_Resolution' ORDER BY FileUploadID"  >
                                                                            <SelectParameters>
                                                                                <asp:SessionParameter Name="BidRefNo" SessionField="BuyerBidForBac" Type="Int32" />
                                                                            </SelectParameters>
                                                                        </asp:SqlDataSource>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td width="50%">
                                                                    <table style="width: 100%;">                                                                        <tr>                                                                <td style="border:none"><strong>
                                                                        <a id="btcollapseCE" href="javascript:void(0)" onclick="$('#collapseCE').toggle(); showhideBT();" style="color:Black" >- Commercial Evaluation</a> </strong></td>
                                                                    <td width="15" style="border:none"><b>(<asp:Label ID="SDA_CE_count" runat="server"></asp:Label>)</b></td>
                                                                    </tr>
                                                                    </table>
                                                                        <div id="collapseCE">
                                                                            <asp:Repeater ID="Repeater_SDA_CE" runat="server" DataSourceID="dsSDA_CE" >
                                                                                <ItemTemplate>
                                                                                    <a href="<%# Session["pageDetails"] %>?ShowAttachment=<%# Eval("FileUploadID") %>"
                                                                                        target="_blank" title='<%# Eval("ActualFileName") %>'>
                                                                                        <%# Eval("OriginalFileName") %></a><br />
                                                                                </ItemTemplate>
                                                                            </asp:Repeater>
                                                                        </div id="collapseCE">
                                                                        <asp:SqlDataSource ID="dsSDA_CE" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>' 
                                                                            
                                                                            SelectCommand="SELECT * FROM tblBACSupportingDocuments WHERE BidRefNo=@BidRefNo AND DocuName='Commercial_Evaluation' ORDER BY FileUploadID"   >
                                                                            <SelectParameters>
                                                                                <asp:SessionParameter Name="BidRefNo" SessionField="BuyerBidForBac" Type="Int32" />
                                                                            </SelectParameters>
                                                                        </asp:SqlDataSource>
                                                                    </td>
                                                                  </tr>
                                                                <tr>
                                                                    <td><table style="width: 100%;">                                                                        <tr>                                                                <td style="border:none"><strong><a id="btcollapseNR" href="javascript:void(0)" onclick="$('#collapseNR').toggle(); showhideBT();" style="color:Black" >- Negotiation Results</a> </strong></td>
                                                                    <td width="15" style="border:none"><b>(<asp:Label ID="SDA_NR_count" runat="server"></asp:Label>)</b></td>
                                                                    </tr>
                                                                    </table>
                                                                        <div id="collapseNR">
                                                                            <asp:Repeater ID="Repeater_SDA_NR" runat="server" DataSourceID="dsSDA_NR" >
                                                                                <ItemTemplate>
                                                                                    <a href="<%# Session["pageDetails"] %>?ShowAttachment=<%# Eval("FileUploadID") %>"
                                                                                        target="_blank" title='<%# Eval("ActualFileName") %>'>
                                                                                        <%# Eval("OriginalFileName") %></a><br />
                                                                                </ItemTemplate>
                                                                            </asp:Repeater>
                                                                        </div id="collapseNR">
                                                                        <asp:SqlDataSource ID="dsSDA_NR" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>' 
                                                                            
                                                                            SelectCommand="SELECT * FROM tblBACSupportingDocuments WHERE BidRefNo=@BidRefNo AND DocuName='Negotiation_Results' ORDER BY FileUploadID"  >
                                                                            <SelectParameters>
                                                                                <asp:SessionParameter Name="BidRefNo" SessionField="BuyerBidForBac" Type="Int32" />
                                                                            </SelectParameters>
                                                                        </asp:SqlDataSource>
                                                                    </td>
                                                                  </tr>
                                                                  <tr>
                                                                    <td><table style="width: 100%;">                                                                        <tr>                                                                <td style="border:none"><strong><a id="btcollapseTE" href="javascript:void(0)" onclick="$('#collapseTE').toggle(); showhideBT();" style="color:Black" >- Technical Evaluation</a> </strong></td>
                                                                    <td width="15" style="border:none"><b>(<asp:Label ID="SDA_TE_count" runat="server"></asp:Label>)</b></td>
                                                                    </tr>
                                                                    </table>
                                                                        <div id="collapseTE">
                                                                            <asp:Repeater ID="Repeater_SDA_TE" runat="server" DataSourceID="dsSDA_TE" >
                                                                                <ItemTemplate>
                                                                                    <a href="<%# Session["pageDetails"] %>?ShowAttachment=<%# Eval("FileUploadID") %>"
                                                                                        target="_blank" title='<%# Eval("ActualFileName") %>'>
                                                                                        <%# Eval("OriginalFileName") %></a><br />
                                                                                </ItemTemplate>
                                                                            </asp:Repeater>
                                                                        </div id="collapseTE">
                                                                        <asp:SqlDataSource ID="dsSDA_TE" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>' 
                                                                            
                                                                            SelectCommand="SELECT * FROM tblBACSupportingDocuments WHERE  BidRefNo=@BidRefNo AND DocuName='Technical_Evaluation' ORDER BY FileUploadID" >
                                                                            <SelectParameters>
                                                                                <asp:SessionParameter Name="BidRefNo" SessionField="BuyerBidForBac" Type="Int32" />
                                                                            </SelectParameters>
                                                                        </asp:SqlDataSource>
                                                                    </td>
                                                                  </tr>                                                                <tr>
                                                                    <td><table style="width: 100%;">                                                                        <tr>                                                                <td style="border:none"><strong><a id="btcollapseOth" href="javascript:void(0)" onclick="$('#collapseOth').toggle(); $('#collapseOth').is(':visible')?$(this).html('- Others'):$(this).html('+ Others')" style="color:Black" >- Others</a> </strong></td>
                                                                    <td width="15" style="border:none"><b>(<asp:Label ID="SDA_Oth_count" runat="server"></asp:Label>)</b></td>
                                                                    </tr>
                                                                    </table>
                                                                        <div id="collapseOth">
                                                                            <asp:Repeater ID="Repeater_SDA_Oth" runat="server" DataSourceID="dsSDA_Oth" >
                                                                                <ItemTemplate>
                                                                                    <a href="<%# Session["pageDetails"] %>?ShowAttachment=<%# Eval("FileUploadID") %>"
                                                                                        target="_blank" title='<%# Eval("ActualFileName") %>'>
                                                                                        <%# Eval("OriginalFileName") %></a><br />
                                                                                </ItemTemplate>
                                                                            </asp:Repeater>
                                                                        </div id="collapseOth">
                                                                        <asp:SqlDataSource ID="dsSDA_Oth" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>' 
                                                                            
                                                                            SelectCommand="SELECT * FROM tblBACSupportingDocuments WHERE BidRefNo=@BidRefNo AND DocuName='Others' ORDER BY FileUploadID" >
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