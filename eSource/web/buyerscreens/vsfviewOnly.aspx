<%@ Page Language="C#" AutoEventWireup="true" CodeFile="vsfviewOnly.aspx.cs" Inherits="vsfviewOnly" EnableViewState="true" MaintainScrollPositionOnPostback="true"  %>
<%@ Register TagPrefix="EBid" TagName="LeftNav" Src="~/WEB/usercontrol/Buyer/LeftNav.ascx" %>
<%@ Register TagPrefix="EBid" TagName="TopNavBids" Src="~/WEB/usercontrol/Buyer/TopNavSuppliers.ascx" %>
<%@ Register TagPrefix="EBid" TagName="TopNav5" Src="~/WEB/usercontrol/Buyer/TopNav2.ascx" %>
<%@ Register TagPrefix="EBid" TagName="GlobalLinksNav" Src="~/web/usercontrol/GlobalLinksNav.ascx" %>
<%@ Register TagPrefix="EBid" TagName="Footer" Src="~/web/usercontrol/Footer.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml" style="height: 100%;">
<head>
<%@ Register TagPrefix="EBid" TagName="jqueryLoader" Src="../usercontrol/jQuery_loader.ascx" %>
    <EBid:jqueryLoader runat="server" ID="jqueryLoader1" />
    <title id="PageTitle" runat="server"></title>
    <meta http-equiv="Content-Language" content="en-us" />
    <meta http-equiv="Content-Type" content="text/html; charset=windows-1252" />
    
    <link href='<%= "../themes/"+Session["configTheme"]+"/css/style.css" %>' rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href='<%= "../themes/"+Session["configTheme"]+"/css/style.css" %>' />
    <link rel="stylesheet" type="text/css" href='<%="../themes/"+Session["configTheme"]+"/css/style_buyer.css" %>' />

    <script type="text/javascript" src="../../jquery/jquery-1.5.1.js"></script>
    <script type="text/javascript" src="../../jquery/jquery.numeric.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $(".numeric").css('text-align', 'right');
            $(".integer").css('text-align', 'right');
            $(".numeric").numeric();
            $(".integer").numeric(false, function () { alert("Integers only"); this.value = ""; this.focus(); });

            $(".integer").digits();
            $(".integer").blur(function () {
                $(".integer").digits(); $(".integer").css('text-align', 'right');
            });
            $(".numeric").digits();
            $(".numeric").blur(function () {
                $(".numeric").digits(); $(".numeric").css('text-align', 'right');
            });
        });
        function reloadNumeric() {
            $(".numeric").numeric();
            $(".integer").numeric(false, function () { alert("Integers only"); this.value = ""; this.focus(); });
        }
        $.fn.digits = function () {
            return this.each(function () {
                $(this).val($(this).val().replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,"));
                $(this).html($(this).html().replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,"));
            })
        }
    </script>
    <script type="text/javascript" src="../../jquery/jquery.table.addrow.js"></script>



    <style type="text/css">
        .ui-widget { font-family: Arial; font-size: 11; }
        </style>
</head>
<body id="Body1" runat="server">
    <div>
        <form enctype="multipart/form-data" id="form1" name="form1" runat="server">
                                                                            <input type="hidden" id="Hidden2" name="lblLoad" runat="server" />

																			<input type="hidden" id="Hidden1" name="lblTotalAll" runat="server" />
                                                                            &nbsp;
			<table border="0" cellpadding="0" cellspacing="0" width="100%" id="page">
                <tr>
                    <td valign="top" height="137px">
                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td>
                                    <div align="left" id="masthead">
                                        <EBid:GlobalLinksNav runat="server" ID="GlobalLinksNav" />
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <EBid:TopNav5 ID="TopNav5" runat="server" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table border="0" cellpadding="0" cellspacing="0" width="100%" height="100%">
                            <tr>
                                <td id="relatedInfo">
                                    <div align="left">
                                        <EBid:LeftNav ID="LeftNav" runat="server" />
                                    </div>
                                    <p>&nbsp;</p>
                                </td>
                                <td id="content">
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                        <tr>
                                            <td valign="top">
												<h1><br />Vendor Shortlisting Form</h1>
                                                <script type="text/javascript">
                                                    $(document).ready(function () {
                                                        $('#relatedInfo').hide();
                                                    });
                                                 </script>
												<p><a href="javascript:void(0)" onclick="$('#relatedInfo').toggle()">Show/Hide Left Panel</a><asp:Label ID="lblMessage" runat="server" Font-Size="11px" ForeColor="red"></asp:Label></p>
                                                <table cellspacing="5" style="width:100%;">
                                                    <tr>
                                                        <td colspan="2" valign="top">
                                                        <table width="25%" border="1" cellpadding="0" cellspacing="0" rules="all" class="itemDetails" style="border-color:#71A9D2; font-family: Arial; font-size: 11px; border-collapse: collapse; background-color: #F4F1C4; height:100%">
																<tbody>
																	<tr>
																		<td colspan="4" class="ui-widget-header" style="height:26px; vertical-align:middle;">Vendor Shortlisting Form</td>
																	</tr>
																	<tr valign="middle">
																		<td align="left" valign="middle" style="background-color:#FFFFFF; width:15%;">Date</td>
																		<td align="left" valign="middle" bgcolor="#FFFFFF" style="width:35%;">
																			<asp:Label ID="VSFDate" runat="server" Text='' Font-Bold="True" Font-Size="10pt"></asp:Label>
																		</td>
																		<td align="left" valign="middle" bgcolor="#FFFFFF" style="width:15%;">Proponent's Name</td>
																		<td align="left" valign="middle" bgcolor="#FFFFFF" style="width:35%;">
																			<asp:Label ID="ProponentName" runat="server" Text='' Font-Bold="True" Font-Size="10pt"></asp:Label>
																		</td>
																	</tr>
																	<tr valign="middle">
																		<td align="left" valign="middle" bgcolor="#FFFFFF" >PR No.</td>
																		<td align="left" valign="middle" bgcolor="#FFFFFF" >
																			<asp:Label ID="PRNo" runat="server" Text='' Font-Bold="True" Font-Size="10pt"></asp:Label>
																		</td>
																		<td align="left" valign="middle" bgcolor="#FFFFFF" >Group / Department</td>
																		<td align="left" valign="middle" bgcolor="#FFFFFF">
																			<asp:Label ID="GroupDept" runat="server" Text='' Font-Bold="True" Font-Size="10pt"></asp:Label>
																		</td>
																	</tr>
																	<tr valign="middle">
																		<td align="left" valign="middle" bgcolor="#FFFFFF" >Project Name</td>
																		<td align="left" valign="middle" bgcolor="#FFFFFF" >
																			<asp:Label ID="ProjectName" runat="server" Text='' Font-Bold="True" Font-Size="10pt"></asp:Label>
																		</td>
																		<td align="left" valign="middle" bgcolor="#FFFFFF" >No. of potential vendors</td>
																		<td align="left" valign="middle" bgcolor="#FFFFFF">
																			<asp:Label ID="NumPotentialVendor" runat="server" CssClass="numeric" Text='' Font-Bold="True" Font-Size="10pt"></asp:Label>
																		</td>
																	</tr>
																	<tr valign="middle">
																		<td align="left" valign="middle" bgcolor="#FFFFFF" >PR Amount</td>
																		<td align="left" valign="middle" bgcolor="#FFFFFF" >
																			<asp:Label ID="PRAmount" runat="server" CssClass="numeric" Text='' Font-Bold="True" Font-Size="10pt"></asp:Label>
																		</td>
																		<td align="left" valign="middle" bgcolor="#FFFFFF" >No. of short-listed vendors</td>
																		<td align="left" valign="middle" bgcolor="#FFFFFF">
																			<asp:Label ID="NumShortlistedVendor" runat="server" CssClass="numeric" Text='' Font-Bold="True" Font-Size="10pt"></asp:Label>
																		</td>
																	</tr>
																	<tr valign="middle">
																		<td align="left" valign="middle" bgcolor="#FFFFFF" >PR Description</td>
																		<td align="left" valign="middle" bgcolor="#FFFFFF" >
																			<asp:Label ID="PRDescription" runat="server" Text='' Font-Bold="True" Font-Size="10pt"></asp:Label>
																		</td>
																		<td align="left" valign="middle" bgcolor="#FFFFFF" >&nbsp;</td>
																		<td align="left" valign="middle" bgcolor="#FFFFFF">&nbsp;</td>
																	</tr>
																</tbody>
															</table>
                                                        </td>
                                                     </tr>
                                                    



                                                    <tr>
                                                        <td colspan="2" valign="top">
                                                        <style type="text/css">.itemDetails td{ border:1px solid #ccc; }
                                                            .style1
                                                            {
                                                                border: 1px solid #4297d7;
                                                                background: #5c9ccc url('../../jquery/themes/redmond/images/ui-bg_gloss-wave_55_5c9ccc_500x100.png') repeat-x 50% 50%;
                                                                color: #ffffff;
                                                                font-weight: bold;
                                                                height: 21px;
                                                            }
                                                            .style2
                                                            {
                                                                width: 10%;
                                                            }
                                                        .font6
	{color:windowtext;
	font-size:10.0pt;
	font-weight:700;
	font-style:normal;
	text-decoration:none;
	font-family:Arial, sans-serif;
	}
.font5
	{color:windowtext;
	font-size:10.0pt;
	font-weight:400;
	font-style:normal;
	text-decoration:none;
	font-family:Arial, sans-serif;
	}
                                                            </style>
                                                        <table width="25%" border="1" cellpadding="5" cellspacing="0" rules="all" class="itemDetails" style="border-color:#71A9D2; font-family: Arial; font-size: 11px; border-collapse: collapse; background-color: #F4F1C4; height:100%">
																<tbody>
																	<tr>
																		<td colspan="11" class="style1" style="vertical-align:middle;">Shortlisting Criteria</td>
																	</tr>
																	<tr valign="middle">
																		<td align="center" valign="middle" style="width:5%; vertical-align:middle" rowspan="2">Selected
                                                                        </td>
																		<td align="center" valign="middle" style="width:15%; vertical-align:middle" rowspan="2">
                                                                            Vendor
                                                                        </td>
																		<td align="center" valign="middle" style="width:10%; vertical-align:middle" rowspan="2">Accreditation Status</td>
																		<td align="center" valign="middle" style="width:35%; color:#fff; vertical-align:middle" colspan="2" bgcolor="#71a8d1">SAP Performance Rating</td>
																		<td align="center" valign="middle" style="width:15%; color:#fff; vertical-align:middle" colspan="4" bgcolor="#71a8d1">Financial Capacity</td>
																		<td align="center" valign="middle" style="width:5%; color:#fff; vertical-align:middle" bgcolor="#71a8d1">Preferred Vendor</td>
																		<td align="center" valign="middle" style="color:#fff; vertical-align:middle; width:5%;" bgcolor="#71a8d1">Product Type Approval (as applicable)</td>
																		<td align="center" valign="middle" style="vertical-align:middle; " rowspan="2">Overall Ranking</td>
																	</tr>
																	<tr valign="middle">
																		<td align="center" valign="middle" style="width:5%;">% Score</td>
																		<td align="center" valign="middle" style="width:5%;">Rank</td>
																		<td align="center" valign="middle" style="width:10%;">Maximum Exposure Limit</td>
																		<td align="center" valign="middle" style="width:10%;">Amount Unserved POs</td>
																		<td align="center" valign="middle" style="width:10%;">Available Balance</td>
																		<td align="center" valign="middle" style="width:5%;">Rank</td>
																		<td align="center" valign="middle" >Endorsed By</td>
																		<td align="center" valign="middle" class="style2" >Y/N</td>
																	</tr>
                                                                    <asp:Repeater ID="repeaterVSFDetails" runat="server" DataSourceID="dsVSFDetails" OnItemDataBound="repeaterVSFDetails_ItemDataBound">
                                                                    <ItemTemplate>
																	<tr valign="middle">
																		<td align="center" valign="middle" bgcolor="#FFFFFF">
                                                                            <%--<input id="Selected" type="checkbox" disabled="disabled" runat="server"  />--%>
                                                                            <asp:CheckBox ID="Selected" runat="server" Enabled="false" />
                                                                        </td>
																		<td align="center" valign="middle" bgcolor="#FFFFFF"><asp:Label ID="VendorName" runat="server" Text='<%# Eval("VendorName") %>' Font-Bold="True" Font-Size="10pt"></asp:Label></td>
																		<td align="center" valign="middle" bgcolor="#FFFFFF"><asp:Label ID="Accreditation" runat="server" Text='<%# Eval("SupplierTypeDesc") %>' Font-Bold="True" Font-Size="10pt"></asp:Label></td>
																		<td align="center" valign="middle" bgcolor="#FFFFFF"><asp:Label ID="SAPRatingScore" runat="server" CssClass="numeric" Text='<%# Eval("SAPRatingScore") %>' Font-Bold="True" Font-Size="10pt"></asp:Label></td>
																		<td align="center" valign="middle" bgcolor="#FFFFFF"><asp:Label ID="SAPRatingRank" runat="server" CssClass="numeric" Text='<%# Eval("SAPRatingRank") %>' Font-Bold="True" Font-Size="10pt"></asp:Label></td>
																		<td align="center" valign="middle" bgcolor="#FFFFFF"><asp:Label ID="MaxExposureLimit" runat="server" CssClass="numeric" Text='<%# Eval("MaxExposureLimit") %>' Font-Bold="True" Font-Size="10pt"></asp:Label></td>
																		<td align="center" valign="middle" bgcolor="#FFFFFF"><asp:Label ID="AmountUnservedPO" runat="server"  CssClass="numeric" Text='<%# Eval("AmountUnservedPO") %>' Font-Bold="True" Font-Size="10pt"></asp:Label></td>
																		<td align="center" valign="middle" bgcolor="#FFFFFF"><asp:Label ID="AvailBalance" runat="server" CssClass="numeric" Text='<%# Eval("AvailBalance") %>' Font-Bold="True" Font-Size="10pt"></asp:Label></td>
																		<td align="center" valign="middle" bgcolor="#FFFFFF"><asp:Label ID="FCRank" runat="server" CssClass="numeric" Text='<%# Eval("FCRank") %>' Font-Bold="True" Font-Size="10pt"></asp:Label></td>
																		<td align="center" valign="middle" bgcolor="#FFFFFF"><asp:Label ID="EndoresedBy" runat="server" Text='<%# Eval("EndoresedBy") %>' Font-Bold="True" Font-Size="10pt"></asp:Label></td>
																		<td align="center" valign="middle" bgcolor="#FFFFFF"><asp:Label ID="ProductTypeApproval" runat="server" Text='<%# Eval("ProductTypeApproval") %>' Font-Bold="True" Font-Size="10pt"></asp:Label></td>
																		<td align="center" valign="middle" bgcolor="#FFFFFF"><asp:Label ID="OverallRanking" CssClass="numeric" runat="server" Text='<%# Eval("OverallRanking") %>' Font-Bold="True" Font-Size="10pt"></asp:Label></td>
																	</tr>
                                                                    </ItemTemplate>
                                                                    </asp:Repeater>
																	<tr valign="middle">
																		<td align="center" valign="middle" bgcolor="#71a8d1" colspan="2" 
                                                                            style="color:#fff; vertical-align:middle">
                                                                            Summary of Recommendation</td>
																		<td align="left" valign="middle" bgcolor="#FFFFFF" colspan="9"><asp:Label ID="Recomendatation" runat="server" Text='<%# Eval("Recomendatation") %>' Font-Bold="True" Font-Size="10pt"></asp:Label>
                                                                        </td>
																	</tr>
																</tbody>
															</table>
                                                            <asp:SqlDataSource ID="dsVSFDetails" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>'
																SelectCommand="SELECT t1.VendorId, t2.VendorName, t1.Accreditation, t3.SupplierTypeDesc, t1.SAPRatingScore, t1.SAPRatingScore, t1.SAPRatingRank, t1.MaxExposureLimit, t1.AmountUnservedPO, t1.AvailBalance, t1.FCRank, t1.EndoresedBy, t1.ProductTypeApproval, t1.OverallRanking, t1.Selected    FROM tblVSFDetails t1, tblVendors t2, rfcSupplierType t3 WHERE t2.VendorId=t1.VendorId AND t3.SupplierTypeId = t2.Accredited AND t1.VSFId = @VSFId ORDER BY t1.OverallRanking" >
                                                                <SelectParameters>
                                                                    <%--<asp:SessionParameter Name="VSFId" SessionField="VSFId" Type="Int32" />--%>
                                                                    <asp:QueryStringParameter Name="VSFId" QueryStringField = "VSFId" Type="Int32" />
																</SelectParameters>
															</asp:SqlDataSource>


                                                            <br />
                                                            <br />
                                                            <p style="font-size:12px">
                                                            <strong>SAP Performance Rating:</strong> This is the average performance rating of a Vendor in the SAP system. <br />
                                                            <strong>Maximum Exposure Limit (MEL): </strong>This is 50% of a Vendor? revenue or total amount of a Vendor? PO transactions for the previous calendar year whichever is higher.<br />
                                                            <strong>Amount of Unserved POs:</strong> Amount of open undelivered PO transactions for the past 12 months.<br />
                                                            <strong>Rank:</strong> Highest value is 1 and succeeding values in increment of 
                                                            1.<br />
                                                            <strong>Preferred Vendor:</strong> Preferred Vendor by End-user, Endorsed by 
                                                            SingTel or Principal Vendor.<br />
                                                            <strong>Product Type Approved:</strong> Indicate whether or not the product has 
                                                            been type-approved by the End-user, if applicable.<br />
                                                            <strong>Overall Ranking:</strong> Computed as the sum of all Ranks divided by 
                                                            the number of criteria and other considerations in the evaluation of 
                                                            shortlisting vendors with Rank 1 being the highest.<br />
                                                            <strong>Summary of Recommendation: </strong>This is the result of the 
                                                            shortlisting criteria, wherein the top ranked vendors are indicated based from 
                                                            the overall minimum criteria and other considerations in the evaluation of 
                                                            shortlisting vendors. 
                                                                <br />
                                                                <br />
                                                                <strong>List of Attachments</strong><table style="width:400;">
                                                                    <tr>
                                                                        <td><input id="ApprovedMemo" type="checkbox" runat="server" disabled="disabled" /> <label>Approved PR/Memo</label></td>
                                                                        <td><input id="BoardApproval" type="checkbox" runat="server" disabled="disabled" /> <label>Board Approval</label></td>
                                                                        <td>&nbsp;</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td><input id="EndorsementMemo" type="checkbox" runat="server" disabled="disabled" /> <label>End-User's 
                                                                            Endorsement Memo</label></td>
                                                                        <td><input id="ExcomApproval" type="checkbox" runat="server" disabled="disabled" /> <label>Excom Approval</label></td>
                                                                        <td>&nbsp;
                                                                            </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td><input id="Others" type="checkbox" runat="server" disabled="disabled" /> <label>Others</label>
                                                                            <asp:Label ID="OthersDesc" runat="server" Text="Label" Font-Bold="True" Font-Size="10pt"></asp:Label></td>
                                                                        <td>&nbsp;
                                                                            </td>
                                                                        <td>&nbsp;
                                                                            </td>
                                                                    </tr>
                                                                </table>
                                                            </p></td>
                                                     </tr>



                                                     


                                                     
                                                     <tr>
                                                     <td colspan="2" valign="top">
                                                     <br /><br />

                                                         <strong>Comments</strong><br />
                                                         <div style="height:120px; overflow:scroll; border: 2px solid #71A9D2; padding:10px;">
                                                     <asp:Repeater ID="repeaterVSFComments" runat="server" DataSourceID="dsVSFComments">
                                                        <ItemTemplate>
                                                        <p><strong><%# Eval("Name")%></strong>&nbsp;&nbsp;&nbsp;<em><%# Eval("DateCreated")%></em><br /><%# Eval("Comment") %></p>
                                                          </ItemTemplate>
                                                        </asp:Repeater>
                                                         <asp:SqlDataSource ID="dsVSFComments" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>'
                                                                    SelectCommand="SELECT * FROM tblVSFComments WHERE VSFId = @VSFId" >
                                                            <SelectParameters>
                                                                <asp:QueryStringParameter Name="VSFId" QueryStringField = "VSFId" Type="Int32" />
	                                                        </SelectParameters>
                                                          </asp:SqlDataSource>
                                                          </div>
                                                          </td>
                                                     </tr>



                                                    <tr>
														<td colspan="2" valign="top">


															<table width="100%" border="1" cellpadding="0" cellspacing="0" rules="all" class="itemDetails" id="Biddetails_details1_dvEventDetails11" style="border-color:#71A9D2; font-family: Arial; font-size: 11px; border-collapse: collapse; background-color: #F4F1C4; height:100%">
																<tbody>
																	<tr>
																		<td colspan="4" class="ui-widget-header" style="height:26px; vertical-align:middle;">PURCHASING</td>
																	</tr>
																	<tr valign="middle" >
																		<td width="25%" align="center" valign="middle" style="padding:5px; font-size:12px;">
																			<table width="150" border="0" cellspacing="0" cellpadding="0">
																				<tr>
																					<td>Prepared by / date: <br /><asp:Literal ID="lblPreparedDate" runat="server"></asp:Literal></td>
																				</tr>
																				<tr>
																					<td>&nbsp;</td>
																				</tr>
																				<tr>
																					<td><strong><asp:Literal ID="lblBuyer" runat="server" Text="BUYER"></asp:Literal></strong></td>
																				</tr>
																				<tr>
																					<td style="border-top:2px #000 solid;">Buyer</td>
																				</tr>
																			</table>
																		</td>
																		<td width="25%" align="center" valign="middle" style="padding:5px; font-size:12px;">
																			<table width="150" border="0" cellspacing="0" cellpadding="0">
																				<tr>
																					<td colspan="2">Approved by / date: <br /><asp:Literal ID="lblApprovedDate" runat="server">&nbsp;</asp:Literal></td>
																				</tr><td>&nbsp;</td><tr>
														                            <td colspan="2"><strong><asp:Literal ID="lblPurchasing" runat="server" Text="PURCHASING_APPROVER"></asp:Literal></strong>
                                                                                    </td>
																				</tr>
																				<tr>
				                                                                    <td colspan="2" style="border-top:2px #000 solid;">Purchasing Head</td>
					                                                            </tr>
																			</table>
																		</td>
							                                        </tr>
																</tbody>
															</table>



                                                            </td>
                                                    </tr>
                                                </table>
												<br />
												
                                                <table border="0" cellpadding="0" cellspacing="0" width="100%" id="actions">
                                                    <tr>
                                                        <td align="right">
                                                            <%--<input id="btnCancel" type="button" value="Cancel BAC" onclick="if(confirm('This will delete the current session. Continue?')){ __doPostBack('Cancel'); }" />
                                                            <input id="btnDraft" type="button" value="Draft" onclick="__doPostBack('Draft');" />

                                                            <input id="btnEndorse" type="button" value="Endorse" onclick="if(confirm('Are you sure to endorse this vendor shortlisting now?')){ __doPostBack('Endorse'); }" />--%>
                                                            <input id="btnBack" type="button" value="BACK" onclick="history.back();"  />
                                                            <%--<input id="btnSreenshot" type="button" value="SaveItem" />
                                                            <asp:Button ID="Printit" runat="server" Text="Button" onclick="$('#form1').submit();" />--%>
                                                        </td>
                                                    </tr>
												</table>
                                                <br />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
							
						</table>
                    </td>
                </tr>
                <tr>
					<td id="footer" height="50px">
						<EBid:Footer runat="server" ID="Footer1" />
					</td>
				</tr>
            </table>
            <input type="hidden" name="status" id="status" runat="server" />
        </form>
    </div>
</body>
</html>