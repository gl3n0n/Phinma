<%@ Page Language="C#" AutoEventWireup="true" CodeFile="bidawardingchecklistawarded.aspx.cs" Inherits="web_buyerscreens_bidawardingchecklistawarded" %>
<%@ Register TagPrefix="EBid" TagName="TopDate" Src="~/WEB/usercontrol/TopDate.ascx" %>
<%@ Register TagPrefix="EBid" TagName="GlobalLinksNav" Src="~/web/usercontrol/GlobalLinksNav.ascx" %>
<%@ Register TagPrefix="EBid" TagName="Purchasing_TopNav_Bids" Src="~/web/usercontrol/Purchasing/Purchasing_TopNav_Bids.ascx" %>
<%@ Register TagPrefix="EBid" TagName="Purchasing_LeftNav" Src="~/web/usercontrol/Purchasing/Purchasing_LeftNav_Bids.ascx" %>
<%@ Register TagPrefix="EBid" TagName="Footer" Src="~/web/usercontrol/Purchasing/Footer.ascx" %>
<%@ Register src="../usercontrol/bac/bac_basisForAwarding.ascx" tagname="bac_basisForAwarding" tagprefix="EBid" %>
<%@ Register src="../usercontrol/bac/bac_supportingDocument.ascx" tagname="bac_supportingDocument" tagprefix="EBid" %>
<%@ Register src="../usercontrol/bac/bac_paymentTerms.ascx" tagname="bac_paymentTerms" tagprefix="EBid" %>
<%@ Register src="../usercontrol/bac/bac_typeOfPurchase.ascx" tagname="bac_typeOfPurchase" tagprefix="EBid" %>
<%@ Register src="../usercontrol/bac/bac_BidDetails.ascx" tagname="bac_BidDetails" tagprefix="EBid" %>
<%@ Register src="../usercontrol/bac/bac_supplyPosition.ascx" tagname="bac_supplyPosition" tagprefix="EBid" %>
<%@ Register src="../usercontrol/bac/bac_natureOfSavings.ascx" tagname="bac_NatureOfSavings" tagprefix="EBid" %>
<%@ Register src="../usercontrol/bac/bac_Items.ascx" tagname="bac_Items" tagprefix="EBid" %>
<%@ Register src="../usercontrol/bac/bac_awardedto.ascx" tagname="bac_awardedTo" tagprefix="EBid" %>
<%@ Register src="../usercontrol/bac/bac_executiveSummary.ascx" tagname="bac_executiveSummary" tagprefix="EBid" %>
<%@ Register src="../usercontrol/bac/bac_remarksComments.ascx" tagname="bac_remarksComments" tagprefix="EBid" %>
<%@ Register src="../usercontrol/bac/bac_sourcingStrategy.ascx" tagname="bac_sourcingStrategy" tagprefix="EBid" %>
<%@ Register src="../usercontrol/bac/bac_commercialDetails.ascx" tagname="bac_commercialDetails" tagprefix="EBid" %>
<%@ Register src="../usercontrol/bac/bac_bidEvaluationDetails.ascx" tagname="bac_bidEvaluationDetails" tagprefix="EBid" %>
<%@ Register src="../usercontrol/bac/bac_bidApprovingCommittee.ascx" tagname="bac_bidApprovingCommittee" tagprefix="EBid" %>
<%@ Register src="../usercontrol/bac/bac_purchasingApprover.ascx" tagname="bac_purchasingApprover" tagprefix="EBid" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml" style="height: 100%;">
<head>
<%@ Register TagPrefix="EBid" TagName="jqueryLoader" Src="../usercontrol/jQuery_loader.ascx" %>
    <EBid:jqueryLoader runat="server" ID="jqueryLoader1" />
    <title id="PageTitle" runat="server"></title>
    <meta http-equiv="Content-Language" content="en-us" />
    <meta http-equiv="Content-Type" content="text/html; charset=windows-1252" />
    <link rel="stylesheet" type="text/css" href='<%= "../themes/"+Session["configTheme"]+"/css/style.css" %>' />
    <link rel="stylesheet" type="text/css" href='<%="../themes/"+Session["configTheme"]+"/css/style_ph.css" %>' />
    <link href='<%= "../themes/"+Session["configTheme"]+"/css/style.css" %>' rel="stylesheet" type="text/css" />
    <style type="text/css">
        

.ui-widget-header { border: 1px solid #4297d7; background: #5c9ccc url('../../jquery/themes/redmond/images/ui-bg_gloss-wave_55_5c9ccc_500x100.png') repeat-x 50% 50%; 
color: #ffffff; font-weight: bold; }
    </style>

    <script type="text/javascript" src="../../jquery/jquery-1.5.1.js"></script>
    <script type="text/javascript" src="../../jquery/ui/jquery.ui.core.js"></script>
    <script type="text/javascript" src="../../jquery/ui/jquery.ui.widget.js"></script>
    <script type="text/javascript" src="../../jquery/ui/jquery.ui.mouse.js"></script>
    <script type="text/javascript" src="../../jquery/ui/jquery.ui.tabs.js"></script>
    <script type="text/javascript" src="../../jquery/ui/jquery.ui.button.js"></script>
    <script type="text/javascript" src="../../jquery/ui/jquery.ui.draggable.js"></script>
    <script type="text/javascript" src="../../jquery/ui/jquery.ui.position.js"></script>
    <script type="text/javascript" src="../../jquery/ui/jquery.ui.dialog.js"></script>
    <script type="text/javascript" src="../../jquery/ui/jquery.ui.datepicker.js"></script>
    <link rel="stylesheet" href='../../jquery/themes/redmond/jquery.ui.all.css' type="text/css" />
</head>
<body style="height: 100%;">
    <div>
        <form enctype="multipart/form-data" id="form1" runat="server">
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
                                    <EBid:TopDate runat="server" ID="TopDate" />
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
                                        <EBid:Purchasing_LeftNav runat="server" ID="Purchasing_LeftNav" />
                                    </div>
                                    <p>&nbsp;
                                        </p>
                                </td>
                                <td id="content">
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                        <tr>
                                            <td valign="top">
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                        <tr>
                                            <td valign="top">
												<h1><br />Bid Awarding Checklist Rejected</h1>
						<script type="text/javascript">
                                                    $(document).ready(function () {
                                                        $('#relatedInfo').hide();
                                                    });
                                                 </script>
												<p><a href="javascript:void(0)" onclick="$('#relatedInfo').toggle()">Show/Hide Left Panel</a><asp:Label ID="lblMessage" runat="server" Font-Size="11px" ForeColor="red"></asp:Label></p>
                                                <table cellspacing="5" style="width:100%;">
                                                    <tr>
                                                        <td colspan="2" valign="top">

                                                        <asp:SqlDataSource ID="dsBuyerBidForBac" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>'
                                                            SelectCommand="SELECT * FROM tblBACBidItems WHERE BacRefNo=@BacRefNo" >
																	<SelectParameters>
																		<asp:SessionParameter Name="BacRefNo" SessionField="BuyerBacRefNo" Type="Int32" />
																	</SelectParameters>
															</asp:SqlDataSource>
                                                            
                                                            <EBid:bac_BidDetails ID="bac_BidDetails1" runat="server" />
                                                            
														</td>
													</tr>
                                                    <tr>
                                                        <td colspan="2">
                                                            

                                                            <EBid:bac_executiveSummary ID="bac_executiveSummary1" runat="server" />


                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">
                                                            




                                                            <EBid:bac_remarksComments ID="bac_remarksComments1" runat="server" />





                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td valign="top" width="50%">
															
                                                            <EBid:bac_supplyPosition runat="server" ID="Bac_supplyPosition" />

                                                        </td>
                                                        <td valign="top">
															


                                                            <EBid:bac_sourcingStrategy runat="server" ID="bac_sourcingStrategy1" />



                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td valign="top" width="50%">

															<EBid:bac_typeOfPurchase runat="server" ID="bac_typeOfPurchase1" />

                                                        </td>
                                                        <td valign="top" width="50%">

															<EBid:bac_paymentTerms runat="server" ID="bac_paymentTerms1" />
                                                            
                                                        </td>
                                                    </tr>
                                                    <tr>
														<td valign="top" width="50%">


															<EBid:bac_supportingDocument runat="server" ID="bac_supportingDocument1" />


														</td>
														<td valign="top" width="50%">

															<EBid:bac_basisForAwarding runat="server" ID="bac_basisForAwarding1" />

														</td>
													</tr>
                                                    <tr>
														<td colspan="2" valign="top">
															

															<EBid:bac_bidEvaluationDetails runat="server" ID="bac_bidEvaluationDetails1" />


                                                          </td>
													</tr>
                                                    <tr>
														<td colspan="2" valign="top">

                                                        <EBid:bac_Items runat="server" ID="Bac_Items1" />
															

                                                            </td>
													</tr>
                                                    <tr>
														<td colspan="2" valign="top">

															

															<EBid:bac_commercialDetails runat="server" ID="bac_commercialDetails1" />

															

                                                            </td>
													</tr>
                                                    <tr>
														<td colspan="2" valign="top">

                                                            <EBid:bac_awardedTo runat="server" ID="awardedTo1" />

                                                         </td>
													</tr>
                                                    <tr>
														<td colspan="2" valign="top">



															
                                                        <EBid:bac_NatureOfSavings runat="server" ID="bac_NatureOfSavings1" />



															
                                                        </td>
													</tr>
                                                    <tr>
														<td colspan="2" valign="top">



                                                            <EBid:bac_purchasingApprover runat="server" ID="bac_purchasingApprover1" />

                                                         </td>
													</tr>
                                                    <tr>
														<td colspan="2" valign="top">

															<EBid:bac_bidApprovingCommittee runat="server" ID="bac_bidApprovingCommittee1" />

                                                        </td>
													</tr>
                                                    </table>
												<br />
												
                                                <br />
                                            </td>
                                        </tr>
                                    </table>
                                            <br />
                                                <table border="0" cellpadding="0" cellspacing="0" width="100%" id="actions">
                                                    <tr>                                                        
                                                        <td><input id="btnCancel" type="button" value="BACK" onclick="history.back();" /></td>
                                                    </tr>
                                                </table>
                                                <br />
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
        </form>
    </div>
</body>
</html>