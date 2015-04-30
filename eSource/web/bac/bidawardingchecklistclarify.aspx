<%@ Page Language="C#" AutoEventWireup="true" CodeFile="bidawardingchecklistclarify.aspx.cs" Inherits="web_bac_bidawardingchecklistclarify" %>
<%@ Register TagPrefix="EBid" TagName="TopDate" Src="~/WEB/usercontrol/TopDate.ascx" %>
<%@ Register TagPrefix="EBid" TagName="GlobalLinksNav" Src="~/web/usercontrol/GlobalLinksNav.ascx" %>
<%@ Register src="../usercontrol/Bac/BAC_LeftNav_Bids.ascx" tagname="BAC_LeftNav" tagprefix="EBid" %>
<%@ Register src="../usercontrol/Bac/BAC_TopNav_Bids.ascx" tagname="BAC_TopNav_Bids" tagprefix="ebid" %>
<%@ Register TagPrefix="EBid" TagName="Footer" Src="~/web/usercontrol/Footer.ascx" %>
<%@ Register src="../usercontrol/bac/bac_executiveSummary.ascx" tagname="bac_executiveSummary" tagprefix="EBid" %>
<%@ Register src="../usercontrol/bac/bac_BidDetails.ascx" tagname="bac_BidDetails" tagprefix="EBid" %>
<%@ Register src="../usercontrol/bac/bac_remarksComments.ascx" tagname="bac_remarksComments" tagprefix="EBid" %>
<%@ Register src="../usercontrol/bac/bac_supplyPosition.ascx" tagname="bac_supplyPosition" tagprefix="EBid" %>
<%@ Register src="../usercontrol/bac/bac_sourcingStrategy.ascx" tagname="bac_sourcingStrategy" tagprefix="EBid" %>
<%@ Register src="../usercontrol/bac/bac_typeOfPurchase.ascx" tagname="bac_typeOfPurchase" tagprefix="EBid" %>
<%@ Register src="../usercontrol/bac/bac_paymentTerms.ascx" tagname="bac_paymentTerms" tagprefix="EBid" %>
<%@ Register src="../usercontrol/bac/bac_Items.ascx" tagname="bac_Items" tagprefix="EBid" %>
<%@ Register src="../usercontrol/bac/bac_awardedto.ascx" tagname="bac_awardedTo" tagprefix="EBid" %>
<%@ Register src="../usercontrol/bac/bac_natureOfSavings.ascx" tagname="bac_NatureOfSavings" tagprefix="EBid" %>
<%@ Register src="../usercontrol/bac/bac_commercialDetails.ascx" tagname="bac_commercialDetails" tagprefix="EBid" %>
<%@ Register src="../usercontrol/bac/bac_bidEvaluationDetails.ascx" tagname="bac_bidEvaluationDetails" tagprefix="EBid" %>
<%@ Register src="../usercontrol/bac/bac_supportingDocument.ascx" tagname="bac_supportingDocument" tagprefix="EBid" %>
<%@ Register src="../usercontrol/bac/bac_basisForAwarding.ascx" tagname="bac_basisForAwarding" tagprefix="EBid" %>
<%@ Register src="../usercontrol/bac/bac_bidApprovingCommittee.ascx" tagname="bac_bidApprovingCommittee" tagprefix="EBid" %>
<%@ Register src="../usercontrol/bac/bac_purchasingApprover.ascx" tagname="bac_purchasingApprover" tagprefix="EBid" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml" style="height: 100%;">
<head>
    <title id="PageTitle" runat="server"></title>
    <meta http-equiv="Content-Language" content="en-us" />
    <meta http-equiv="Content-Type" content="text/html; charset=windows-1252" />
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

    <link href='<%= "../themes/"+Session["configTheme"]+"/css/style.css" %>' rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href='<%= "../themes/"+Session["configTheme"]+"/css/style.css" %>' />
    <link rel="stylesheet" type="text/css" href='<%= "../themes/"+Session["configTheme"]+"/css/style_ph.css" %>' />
    <script type="text/javascript">
        $(document).ready(function () {
            $("button, input:submit, input:button").button();
        });

        function ShowDialog(dialogText, ctrl2Focus) {
            $("#dialog:ui-dialog").dialog("destroy");

            if ($("#dialog-message").length != 0) $('div').remove("#dialog-message");

            if ($("#dialog-message").length == 0) {
                sDiv = "<div id='dialog-message' title='Bid Awarding Checklist' style='display:none;'>";
                sDiv = sDiv + "<p>" + dialogText + "</p>";
                sDiv = sDiv + "</div>";
                $(sDiv).appendTo('body');
            }

            $("#dialog-message").dialog({
                modal: true,
                buttons: {
                    OK: function () {
                        $(this).dialog("close");
                        $(ctrl2Focus).focus();
                    }
                }
            });
        }

        function ShowClarify(myClientID) {
            $("#dialog:ui-dialog").dialog("destroy");

            //if ($("#dialog-message").length != 0) $('div').remove("#dialog-message");

            $("#dialog-message").appendTo('body');

            $("#dialog-message1").dialog({
                modal: true,
                buttons: {
                    Clarify: function () {
                        $(this).dialog("close");
                        if ($("#txtClarifyX").val() == '') {
                            ShowDialog("Comment  must have a value.", "#txtClarifyX");
                            $("#" + myClientID).removeAttr('checked');
                        }
                        else {
                            $("#txtClarify").val($("#txtClarifyX").val());
                            //alert($("#cbApproverList").val());
                            __doPostBack('Clarify', $("#cbApproverList").val());
                        }
                    },
                    Cancel: function () {
                        $(this).dialog("close");
                        $("#" + myClientID).removeAttr('checked');
                    }
                }
            });
        }

        function ShowApprove(myClientID) {
            $("#dialog:ui-dialog").dialog("destroy");

            if ($("#dialog-message").length != 0) $('div').remove("#dialog-message");

            if ($("#dialog-message").length == 0) {
                sDiv = "<div id='dialog-message' title='APPROVE: Bid Awarding Checklist' style='display:none;'>";
                sDiv = sDiv + "<p>";
                sDiv = sDiv + "<table>";
                sDiv = sDiv + "<tr>";
                sDiv = sDiv + "<td style='font-family: Arial; font-size: 11;'>Comment:</td>";
                sDiv = sDiv + "<td><textarea id='txtApprovedX' name='txtApprovedX' cols='35' rows='5' style='font-family: Arial; font-size: 11;'></textarea></td>";
                sDiv = sDiv + "</tr>";
                sDiv = sDiv + "<tr>";
                sDiv = sDiv + "<td colspan='2' align='right'>";
                sDiv = sDiv + "</td>";
                sDiv = sDiv + "</tr></table>";
                sDiv = sDiv + "</p>";
                sDiv = sDiv + "</div>";
                $(sDiv).appendTo('body');
            }

            $("#dialog-message").dialog({
                modal: true,
                buttons: {
                    Approve: function () {
                        $(this).dialog("close");
                        if ($("#txtApprovedX").val() == '') {
                            ShowDialog("Comment  must have a value.", "#txtApprovedX");
                            $("#" + myClientID).removeAttr('checked');
                        }
                        else {
                            $("#txtClarify").val($("#txtApprovedX").val());
                            __doPostBack('Approve', '');
                        }
                    },
                    Cancel: function () {
                        $(this).dialog("close");
                        $("#" + myClientID).removeAttr('checked');
                    }
                }
            });
        }

        function ShowReject(myClientID) {
            $("#dialog:ui-dialog").dialog("destroy");

            if ($("#dialog-reject").length != 0) $('div').remove("#dialog-reject");

            if ($("#dialog-reject").length == 0) {
                sDiv = "<div id='dialog-reject' title='REJECT: Bid Awarding Checklist' style='display:none;'>";
                sDiv = sDiv + "<p>";
                sDiv = sDiv + "<table>";
                sDiv = sDiv + "<tr>";
                sDiv = sDiv + "<td style='font-family: Arial; font-size: 11;'>Comment:</td>";
                sDiv = sDiv + "<td><textarea id='txtRejectX' name='txtRejectX' cols='35' rows='5' style='font-family: Arial; font-size: 11;'></textarea></td>";
                sDiv = sDiv + "</tr>";
                sDiv = sDiv + "<tr>";
                sDiv = sDiv + "<td colspan='2' align='right'>";
                sDiv = sDiv + "</td>";
                sDiv = sDiv + "</tr></table>";
                sDiv = sDiv + "</p>";
                sDiv = sDiv + "</div>";
                $(sDiv).appendTo('body');
            }

            $("#dialog-reject").dialog({
                modal: true,
                buttons: {
                    Reject: function () {
                        $(this).dialog("close");
                        if ($("#txtRejectX").val() == '') {
                            ShowDialog("Comment  must have a value.", "#txtRejectX");
                            $("#" + myClientID).removeAttr('checked');
                        }
                        else {
                            $("#txtClarify").val($("#txtRejectX").val());
                            __doPostBack('Reject', '');
                        }
                    },
                    Cancel: function () {
                        $(this).dialog("close");
                        $("#" + myClientID).removeAttr('checked');
                    }
                }
            });
        }

        function ChangeToLink(oID, sTxt) {
            document.getElementById(oID).innerHTML = sTxt;
        }

        function HideCriteria() {
            var i;
            for (i = 0; i < 10; i++) {
                $("#tr" + i).hide();
            }
        }

        function ShowNewRowsOnLoad() {
            var i = $("#txtNewRow").val();
            var j = 0;
            while (j <= i) {
                $("#tr" + j).show();
                j++;
            }
        }

        function ShowRowsOnLoad() {
            var i;
            for (i = 0; i < 10; i++) {
                var j = $("#txtR" + i).val();
                if (j == "") {
                    $("#txtNewRow").val(i - 1);
                    break;
                }
            }
        }

        function ComputeAccCost() {
            var sCtrl;
            var sCtrl_1;
            var iTotal = 0;
            for (var j = 0; j < 100; j++) {
                if (j < 10) {
                    sCtrl = "#Repeater13_ctl0" + j + "_chkAwarded";
                    sCtrl_1 = "#Repeater10_ctl0" + j + "_txtBidTotal";
                }
                else {
                    sCtrl = "#Repeater13_ctl" + j + "_chkAwarded";
                    sCtrl_1 = "#Repeater10_ctl" + j + "_txtBidTotal";
                }
                if ($(sCtrl).length == 0) {
                    break;
                }
                else {
                    var chkd = $(sCtrl).is(':checked') ? 1 : 0;
                    if (chkd == 1) {
                        iTotal = iTotal + parseFloat($(sCtrl_1).val());
                    }
                }
            }
            document.getElementById('lblAccCost').innerHTML = add_commas(iTotal.toFixed(2));
            var iTotalAll = parseFloat(document.getElementById('lblTotalAll').value);
            var iNegDisc = iTotalAll - iTotal;
            document.getElementById('lblNegDisc').innerHTML = add_commas(iNegDisc.toFixed(2));
            var iPercent = (iTotal / iTotalAll) * 100;
            document.getElementById('lblPercentSpend').innerHTML = add_commas(iPercent.toFixed(2)) + "%";
        }

        function add_commas(nStr) {
            nStr += '';
            x = nStr.split('.');
            x1 = x[0];
            x2 = x.length > 1 ? '.' + x[1] : '';
            var rgx = /(\d+)(\d{3})/;
            while (rgx.test(x1)) {
                x1 = x1.replace(rgx, '$1' + ',' + '$2');
            }
            return x1 + x2;
        }

        function createCookie(name, value, days) {
            var expires;
            if (days) {
                var date = new Date();
                date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
                expires = "; expires=" + date.toGMTString();
            }
            else expires = "";
            document.cookie = name + "=" + value + expires + "; path=/";
        }

        function readCookie(name) {
            var nameEQ = name + "=";
            var ca = document.cookie.split(';');
            for (var i = 0; i < ca.length; i++) {
                var c = ca[i];
                while (c.charAt(0) == ' ') c = c.substring(1, c.length);
                if (c.indexOf(nameEQ) == 0) return c.substring(nameEQ.length, c.length);
            }
            return null;
        }

        function eraseCookie(name) {
            createCookie(name, "", -1);
        }

        function AddTRCheckBoxFromDB(sTable, sId, sTxt) {
            var sNewRow;
            sTxt = escape(sTxt);
            sDispTxt = sTxt;
            sTxt = sTxt.replace(new RegExp("/", "g"), "_slash_");
            sTxt = sTxt.replace(new RegExp("%", "g"), "_percent_");
            sId = sId + "_";
            var sCookieId = "my" + sId;
            if (readCookie(sCookieId + sTxt.toLowerCase()) == null) {
                createCookie(sCookieId + sTxt.toLowerCase(), unescape(sDispTxt), 1);
            }
        }

        function AddTRCheckBox(sTable, sId, sTxt, bCheckCookie) {
            var sNewRow;
            sTxt = escape(sTxt);
            sDispTxt = sTxt;
            sTxt = sTxt.replace(new RegExp("/", "g"), "_slash_");
            sTxt = sTxt.replace(new RegExp("%", "g"), "_percent_");
            sId = sId + "_";
            var sCookieId = "my" + sId;
            if (bCheckCookie == true) {
                if (readCookie(sCookieId + sTxt.toLowerCase()) == null) {
                    sNewRow = "<input type='checkbox' name='" + sId + sTxt + "' id='" + sId + sTxt + "' disabled='disabled' checked />";
                    sNewRow = sNewRow + "<label for='" + sId + sTxt + "' >" + unescape(sDispTxt) + "</label>";
                    $(sTable).last().append("<tr id='" + sCookieId + sTxt + "'><td>" + sNewRow + "</td></tr>");
                    createCookie(sCookieId + sTxt.toLowerCase(), unescape(sDispTxt), 1);
                }
                else {
                    ShowDialog("Item already exists.", "#form1");
                }
            }
            else {
                sNewRow = "<input type='checkbox' name='" + sId + sTxt + "' id='" + sId + sTxt + "' disabled='disabled' checked />";
                sNewRow = sNewRow + "<label for='" + sId + sTxt + "' >" + unescape(sDispTxt) + "</label>";
                $(sTable).last().append("<tr id='" + sCookieId + sTxt + "'><td>" + sNewRow + "</td></tr>");
            }
        }

        function RemoveRowOnCheck(sRow, sCookie) {
            $("#" + sRow).remove();
            eraseCookie(sCookie);
        }

        function ShowSavedCookies() {
            var i, nameEQ, ca, c;
            var iVal, iVal1, sTxt, sNewRow;

            // payment terms
            nameEQ = "mypt_";
            ca = document.cookie.split(';');
            for (i = 0; i < ca.length; i++) {
                c = ca[i];
                while (c.charAt(0) == ' ') c = c.substring(1, c.length);
                if (c.indexOf(nameEQ) == 0) {
                    iVal = c.substring(nameEQ.length, c.length);
                    iVal1 = iVal.split("=");
                    AddTRCheckBox("#myPaymentTerms", "pt", iVal1[1], false);
                }
            }
            // sourcing strategy
            nameEQ = "myss_";
            ca = document.cookie.split(';');
            for (i = 0; i < ca.length; i++) {
                c = ca[i];
                while (c.charAt(0) == ' ') c = c.substring(1, c.length);
                if (c.indexOf(nameEQ) == 0) {
                    iVal = c.substring(nameEQ.length, c.length);
                    iVal1 = iVal.split("=");
                    AddTRCheckBox("#mySourcingStrategy", "ss", iVal1[1], false);
                }
            }
            // type of purchase
            nameEQ = "mytop_";
            ca = document.cookie.split(';');
            for (i = 0; i < ca.length; i++) {
                c = ca[i];
                while (c.charAt(0) == ' ') c = c.substring(1, c.length);
                if (c.indexOf(nameEQ) == 0) {
                    iVal = c.substring(nameEQ.length, c.length);
                    iVal1 = iVal.split("=");
                    AddTRCheckBox("#myTypeOfPurchase", "top", iVal1[1], false);
                }
            }
            // supply position
            nameEQ = "mysp_";
            ca = document.cookie.split(';');
            for (i = 0; i < ca.length; i++) {
                c = ca[i];
                while (c.charAt(0) == ' ') c = c.substring(1, c.length);
                if (c.indexOf(nameEQ) == 0) {
                    iVal = c.substring(nameEQ.length, c.length);
                    iVal1 = iVal.split("=");
                    AddTRCheckBox("#mySupplyPosition", "sp", iVal1[1], false);
                }
            }
            // basis for awarding
            nameEQ = "mybfa_";
            ca = document.cookie.split(';');
            for (i = 0; i < ca.length; i++) {
                c = ca[i];
                while (c.charAt(0) == ' ') c = c.substring(1, c.length);
                if (c.indexOf(nameEQ) == 0) {
                    iVal = c.substring(nameEQ.length, c.length);
                    iVal1 = iVal.split("=");
                    AddTRCheckBox("#myBasisForAwarding", "bfa", iVal1[1], false);
                }
            }
        }
    </script>
    <style type="text/css">
        .ui-widget { font-family: Arial; font-size: 11; }
        
.activeTab 
{		
	background-image: url(../images/TabBGactive.jpg);
	background-repeat: repeat-x;			
	border-right: solid 3px #fff;	
	height: 38px;	
	width: 150px;
	text-align: center;
	background-color: #2682C3;
}
        </style>
</head>
<body id="Body1" runat="server">
    <div>
        <form enctype="multipart/form-data" id="form1" runat="server">
                                                                            <input id="txtHasZeroID" type="hidden" name="txtHasZeroID" runat="server" />
																			<input id="txtNewRow" type="hidden" name="txtNewRow" runat="server" />
																			<input id="txtCDVendor" type="hidden" name="txtCDVendor" runat="server" />
																			<input id="txtNewVendor" type="hidden" name="txtNewVendor" runat="server" />
                                                                            <input type="hidden" id="txtClarify" name="txtClarify" runat="server" />

																			<input type="hidden" id="lblTotalAll" name="lblTotalAll" runat="server" />
                                                                            <input type="hidden" id="ResponseClarifyTo" name="ResponseClarifyTo" runat="server" />
                                                                            &nbsp;
            <!-- div to display clarification -->
            <div id='dialog-message1' title='CLARIFY: Bid Awarding Checklist' style='display:none;'>
                <table>
                    <tr>
                        <td style='font-family: Arial; font-size: 10;'>Respond To:</td>
                        <td><script language="javascript">
                                $(document).ready(function () {
                                    $("#txtResponseClarifyTo").html($("#ResponseClarifyTo").val());
                                });
                         </script><span id="txtResponseClarifyTo"></span></td>
                    </tr>
                    <tr>
                        <td style='font-family: Arial; font-size: 11;'>Comment:</td>
                        <td><textarea id='txtClarifyX' name='txtClarifyX' cols='28' rows='5' style='font-family: Arial; font-size: 11;'></textarea></td>
                    </tr>
                    <tr>
                        <td colspan='2' align='right'>
                        </td>
                    </tr>
                </table>
            </div>
            <!-- end of div to display clarification -->
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
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td>
                                    <ebid:BAC_TopNav_Bids ID="BAC_TopNav_Bids1" runat="server" />
                                    <ebid:TopDate ID="TopDate" runat="server" />
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
                                    <h2>Bid Events</h2>
                                                <EBid:BAC_LeftNav runat="server" ID="BAC_LeftNav1" />
                                </td>
                                <td id="content">
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                        <tr>
                                            <td valign="top">
												<h1><br />BAC For Clarifications</h1>
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
                                                            SelectCommand="SELECT * FROM tblBACBidItems WHERE BacRefNo=@BacRefNo AND Status > 0" >
																	<SelectParameters>
																		<asp:SessionParameter Name="BacRefNo" SessionField="BuyerBacRefNo" Type="Int32" />
																		<asp:Parameter Name="Status" DefaultValue="4" Type="Int32" />
																	</SelectParameters>
															</asp:SqlDataSource>

                                                            <EBid:bac_BidDetails ID="bac_BidDetails1" runat="server" />
                                                            
														</td>
													</tr>
                                                    <tr>
                                                        <td colspan="2">
                                                            



                                                            <EBid:bac_executiveSummary ID="bac_executiveSummary2" runat="server" />




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
                                                        <td valign="top" width="50%">
															


                                                            <EBid:bac_sourcingStrategy runat="server" ID="bac_sourcingStrategy1" />



                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td valign="top">

															<EBid:bac_typeOfPurchase runat="server" ID="bac_typeOfPurchase1" />

														</td>
                                                        <td valign="top">

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
                                                    <tr>
														<td>
															<asp:SqlDataSource ID="dsApprover" runat="server"  ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>' 
SelectCommand="SELECT t2.BACId, t2.LastName + ', ' + t2.FirstName + ' ' + t2.MiddleName AS Name1, t2.ApprovingLimitOnNonLowestPrice AS ApprovingLimit
FROM tblBacBidItems t1, tblBidAwardingCommittee t2
WHERE (t2.BACId=t1.Approver_1 OR t2.BACId=t1.Approver_2 OR t2.BACId=t1.Approver_3 OR t2.BACId=t1.Approver_4 OR t2.BACId=t1.Approver_5 OR t2.BACId=t1.Approver_6 OR t2.BACId=t1.Approver_7 OR t2.BACId=t1.Approver_8 OR t2.BACId=t1.Approver_9 OR t2.BACId=t1.Approver_10) AND t1.BidRefNo=@BidRefNo">
		<SelectParameters>
			<asp:SessionParameter Name="BidRefNo" SessionField="BuyerBidForBac" Type="Int32" />
		</SelectParameters>
</asp:SqlDataSource>
														</td>
													</tr>
                                                    <tr>
														<td colspan="2" valign="top" align="right">
														</td>
                                                    </tr>
                                                </table>
												<br />
												
                                                <table border="0" cellpadding="0" cellspacing="0" width="100%" id="actions">
                                                    <tr>
                                                        <td align="right">
                                                        <input id="btnClarify" type="button" value="Clarify" onclick="ShowClarify()" />
                                                        <input id="btnReject" runat="server" type="button" value="Reject"  onclick="ShowReject()" />
                                                            <input id="btnCancel" type="button" value="BACK" onclick="history.back();" />
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
<script type="text/javascript">    HideCriteria(); ShowSavedCookies(); ShowRowsOnLoad(); ShowNewRowsOnLoad(); ComputeAccCost();</script>