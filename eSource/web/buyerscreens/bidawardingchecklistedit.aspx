<%@ Page Language="C#" AutoEventWireup="true" CodeFile="bidawardingchecklistedit.aspx.cs" Inherits="web_buyerscreens_bidawardingchecklistdraft" %>
<%@ Register TagPrefix="EBid" TagName="LeftNav" Src="~/WEB/usercontrol/Buyer/LeftNav.ascx" %>
<%@ Register TagPrefix="EBid" TagName="TopNavBids" Src="~/WEB/usercontrol/Buyer/TopNavBids.ascx" %>
<%@ Register TagPrefix="EBid" TagName="TopNav3" Src="~/WEB/usercontrol/Buyer/TopNav3.ascx" %>
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
    <link rel="stylesheet" type="text/css" href='<%="../themes/"+Session["configTheme"]+"/css/style_buyer.css" %>' />
    <script type="text/javascript">
        $(document).ready(function () {
            $("button, input:submit, input:button").button();

            var iSelected = $("#BuyerBidForBac").val();
            if (iSelected <= 0) {

                var pickerOpts = {
                    showOtherMonths: true,
                    changeMonth: true,
                    changeYear: true,
                    dateFormat: "yy-MM-dd"
                };
                $("#<%=PRDate.ClientID%>").datepicker(pickerOpts);
                $("#<%=DateApprovedLbl.ClientID%>").datepicker(pickerOpts);
            }
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

        function ChangeToLink(oID, sTxt) {
            document.getElementById(oID).innerHTML = sTxt;
        }

        function DeleteAttach(sDoc, oID, iID) {
            // a workaround for a flaw in the demo system (http://dev.jqueryui.com/ticket/4375), ignore!
            $("#dialog:ui-dialog").dialog("destroy");

            if ($("#dialog-message").length != 0) $('div').remove("#dialog-message");

            if ($("#dialog-message").length == 0) {
                sDiv = "<div id='dialog-message' title='Bid Awarding Checklist' style='display:none;'>";
                sDiv = sDiv + "<p>Delete this attachment?</p>";
                sDiv = sDiv + "</div>";
                $(sDiv).appendTo('body');
            }

            $("#dialog-message").dialog({
                modal: true,
                buttons: {
                    OK: function () {
                        $(this).dialog("close");
                        __doPostBack('DeleteAttach_' + sDoc, iID);
                    },
                    Cancel: function () {
                        $(this).dialog("close");
                        document.getElementById(oID).checked = true;
                    }
                }
            });
        }

        function btnAddCol_onclick() {
            var i;
            i = $("#BuyerBidForBac").val();
            if (i != -1) {
                i = $("#txtNewVendor").val();
                i++;
                $("#txtNewVendor").val(i);

                i = $("#txtCDVendor").val();
                i++;
                $("#txtCDVendor").val(i);

                __doPostBack("NewVendor", "");
            }
        }

        function btnDelCol_onclick() {
            var i;
            i = $("#BuyerBidForBac").val();
            if (i != -1) {
                i = $("#txtNewVendor").val();
                if (i > 0) {
                    i--;
                    $("#txtNewVendor").val(i);
                    __doPostBack("NewVendor", "");
                }
            }
        }

        function HideCriteria() {
            var i;
            for (i = 0; i < 10; i++) {
                $("#tr" + i).hide();
            }
        }

        function btnAddRow_onclick() {
            var i;
            i = $("#txtNewRow").val();
            if (i < 9) i++;
            $("#txtNewRow").val(i);
            $("#tr" + i).show('slide');
        }

        function btnDelRow_onclick() {
            var i;
            i = $("#txtNewRow").val();
            var sCtrl;
            $("#tr" + i).hide('slide');
            $("#txtR" + i).val('');
            for (var j = 0; j < 100; j++) {
                if (j < 10) {
                    sCtrl = "#RepeaterC" + i + "_ctl0" + j + "_txtC" + i;
                }
                else {
                    sCtrl = "#RepeaterC" + i + "_ctl" + j + "_txtC" + i;
                }
                if ($(sCtrl).length == 0) {
                    break;
                }
                else {
                    $(sCtrl).val('');
                }
            }
            if (i > 0) i--;
            $("#txtNewRow").val(i);
        }

        function ShowNewRowsOnLoad() {
            var i = $("#txtNewRow").val();
            var j = 0;
            while (j <= i) {
                $("#tr" + j).show();
                j++;
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
                    sNewRow = "<input type='checkbox' name='" + sId + sTxt + "' id='" + sId + sTxt + "' onclick='RemoveRowOnCheck(\"" + sCookieId + sTxt + "\", \"" + sCookieId + sTxt.toLowerCase() + "\");' checked />";
                    sNewRow = sNewRow + "<label for='" + sId + sTxt + "' >" + unescape(sDispTxt) + "</label>";
                    $(sTable).last().append("<tr id='" + sCookieId + sTxt + "'><td>" + sNewRow + "</td></tr>");
                    createCookie(sCookieId + sTxt.toLowerCase(), unescape(sDispTxt), 1);
                }
                else {
                    ShowDialog("Item already exists.", "#form1");
                }
            }
            else {
                sNewRow = "<input type='checkbox' name='" + sId + sTxt + "' id='" + sId + sTxt + "' onclick='RemoveRowOnCheck(\"" + sCookieId + sTxt + "\", \"" + sCookieId + sTxt.toLowerCase() + "\");' checked />";
                sNewRow = sNewRow + "<label for='" + sId + sTxt + "' >" + unescape(sDispTxt) + "</label>";
                $(sTable).last().append("<tr id='" + sCookieId + sTxt + "'><td>" + sNewRow + "</td></tr>");
            }
        }

        function cbPaymentTerms_SelectedIndexChanged() {
            var oPT = document.getElementById("cbPaymentTerms");
            if (oPT.value != -1) {
                var sTxt = oPT.options[oPT.selectedIndex].text;
                AddTRCheckBox("#myPaymentTerms", "pt", sTxt, true);
            }
        }

        function AddOtherPaymentTerm() {
            if ($("#txtNewPaymentTerm").val() == "") {
                ShowDialog("Please type new payment term.", "#txtNewPaymentTerm");
            }
            else {
                var sTxt = $("#txtNewPaymentTerm").val();
                AddTRCheckBox("#myPaymentTerms", "pt", sTxt, true);
            }
        }

        function cbSourcingStrategy_SelectedIndexChanged() {
            var oPT = document.getElementById("cbSourcingStrategy");
            if (oPT.value != -1) {
                var sTxt = oPT.options[oPT.selectedIndex].text;
                AddTRCheckBox("#mySourcingStrategy", "ss", sTxt, true);
            }
        }

        function AddOtherSourcingStrategy() {
            if (jQuery.trim($("#txtNewSourcingStrategy").val()) == "") {
                ShowDialog("Please type new sourcing strategy.", "#txtNewSourcingStrategy");
            }
            else {
                var sTxt = $("#txtNewSourcingStrategy").val();
                AddTRCheckBox("#mySourcingStrategy", "ss", sTxt, true);
            }
        }

        function AddOtherNatureOfSaving() {
            if ($("#txtNewNatureOfSavings").val() == "") {
                ShowDialog("Please type new nature of service.", "#txtNewNatureOfSavings");
            }
            else {
                var sTxt = $("#txtNewNatureOfSavings").val();
                AddTRCheckBox("#myNatureOfSavings", "nos", sTxt, true);
            }
        }

        function cbNatureOfSavings_SelectedIndexChanged() {
            var oPT = document.getElementById("cbNatureOfSavings");
            if (oPT.value != -1) {
                var sTxt = oPT.options[oPT.selectedIndex].text;
                AddTRCheckBox("#myNatureOfSavings", "nos", sTxt, true);
            }
        }

        function cbTypeOfPurchase_SelectedIndexChanged() {
            var oPT = document.getElementById("cbTypeOfPurchase");
            if (oPT.value != -1) {
                var sTxt = oPT.options[oPT.selectedIndex].text;
                AddTRCheckBox("#myTypeOfPurchase", "top", sTxt, true);
            }
        }

        function AddOtherTypeOfPurchase() {
            if ($("#txtNewTypeOfPurchase").val() == "") {
                ShowDialog("Please type new type of purchase.", "#txtNewTypeOfPurchase");
            }
            else {
                var sTxt = $("#txtNewTypeOfPurchase").val();
                AddTRCheckBox("#myTypeOfPurchase", "top", sTxt, true);
            }
        }

        function cbSupplyPosition_SelectedIndexChanged() {
            var oPT = document.getElementById("cbSupplyPosition");
            if (oPT.value != -1) {
                var sTxt = oPT.options[oPT.selectedIndex].text;
                AddTRCheckBox("#mySupplyPosition", "sp", sTxt, true);
            }
        }

        function AddOtherSupplyPosition() {
            if ($("#txtNewSupplyPosition").val() == "") {
                ShowDialog("Please type new supply position.", "#txtNewSupplyPosition");
            }
            else {
                var sTxt = $("#txtNewSupplyPosition").val();
                AddTRCheckBox("#mySupplyPosition", "sp", sTxt, true);
            }
        }

        function cbBasisForAwarding_SelectedIndexChanged() {
            var oPT = document.getElementById("cbBasisForAwarding");
            if (oPT.value != -1) {
                var sTxt = oPT.options[oPT.selectedIndex].text;
                AddTRCheckBox("#myBasisForAwarding", "bfa", sTxt, true);
            }
        }

        function AddOtherBasisForAwarding() {
            if (jQuery.trim($("#txtNewBasisForAwarding").val()) == "") {
                ShowDialog("Please type new basis for awarding.", "#txtNewBasisForAwarding");
            }
            else {
                var sTxt = $("#txtNewBasisForAwarding").val();
                AddTRCheckBox("#myBasisForAwarding", "bfa", sTxt, true);
            }
        }

        function AddCRC() {
            if (jQuery.trim($("#txtNewCRC").val()) == "") {
                ShowDialog("Please type new CRC No.", "#txtNewCRC");
            }
            else {
                var sTxt = $("#txtNewCRC").val();
                AddTRCheckBox("#myCRC", "crc", sTxt, true);
            }
        }

        function AddPO() {
            if (jQuery.trim($("#txtNewPO").val()) == "") {
                ShowDialog("Please type new PO No.", "#txtNewPO");
            }
            else {
                var sTxt = $("#txtNewPO").val();
                AddTRCheckBox("#myPO1", "po1", sTxt, true);
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
            // nature of savings
            nameEQ = "mynos_";
            ca = document.cookie.split(';');
            for (i = 0; i < ca.length; i++) {
                c = ca[i];
                while (c.charAt(0) == ' ') c = c.substring(1, c.length);
                if (c.indexOf(nameEQ) == 0) {
                    iVal = c.substring(nameEQ.length, c.length);
                    iVal1 = iVal.split("=");
                    AddTRCheckBox("#myNatureOfSavings", "nos", iVal1[1], false);
                }
            }
            // CRC
            nameEQ = "mycrc_";
            ca = document.cookie.split(';');
            for (i = 0; i < ca.length; i++) {
                c = ca[i];
                while (c.charAt(0) == ' ') c = c.substring(1, c.length);
                if (c.indexOf(nameEQ) == 0) {
                    iVal = c.substring(nameEQ.length, c.length);
                    iVal1 = iVal.split("=");
                    AddTRCheckBox("#myCRC", "crc", iVal1[1], false);
                }
            }
            // PO
            nameEQ = "mypo1_";
            ca = document.cookie.split(';');
            for (i = 0; i < ca.length; i++) {
                c = ca[i];
                while (c.charAt(0) == ' ') c = c.substring(1, c.length);
                if (c.indexOf(nameEQ) == 0) {
                    iVal = c.substring(nameEQ.length, c.length);
                    iVal1 = iVal.split("=");
                    AddTRCheckBox("#myPO1", "po1", iVal1[1], false);
                }
            }
        }

        function ChangeItemIndex(dropdownitem) {
            var oPT;
            var oPT1;
            if (dropdownitem == "details") {
                oPT = document.getElementById("cbItemNo");
                oPT1 = document.getElementById("cbItemD");
            }
            else {
                oPT = document.getElementById("cbItemD");
                oPT1 = document.getElementById("cbItemNo");
            }
            var iIndex = oPT.selectedIndex;
            oPT1.selectedIndex = iIndex;
        }
    </script>

    <style type="text/css">
        .ui-widget { font-family: Arial; font-size: 11; }
        .style1
        {
            width: 81px;
        }
        .style2
        {
            width: 210px;
        }
        .style3
        {
            width: 200px;
        }
        </style>
</head>
<body id="Body1" runat="server">
    <div>
        <form enctype="multipart/form-data" id="form1" title="form1" runat="server">&nbsp;
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
                                    <EBid:TopNav3 ID="TopNav3" runat="server" />
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
                                        s</div>
                                    <p>&nbsp;</p>
                                </td>
                                <td id="content">
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                        <tr>
                                            <td valign="top">
												<h1><br />Bid Awarding Checklist - Draft</h1>
												<p><asp:Label ID="lblMessage" runat="server" Font-Size="11px" ForeColor="red"></asp:Label></p>
                                                <table cellspacing="5" style="width:100%;">
                                                    <tr>
                                                        <td colspan="2" valign="top">
															<table width="50%" border="1" cellpadding="0" cellspacing="0" rules="all" class="itemDetails" id="Biddetails_details1_dvEventDetails" style="border-color:#71A9D2; font-family: Arial; font-size: 11px; border-collapse: collapse; background-color: #F4F1C4; height:100%">
																<tbody>
																	<tr>
																		<td colspan="4" class="ui-widget-header" style="height:26px; vertical-align:middle;">BID DETAILS</td>
																	</tr>
																	<tr valign="middle" >
																		<td colspan="2" align="center" valign="middle" style="padding:5px; font-size:12px; width:50%">Bid Reference No. </td>
																		<td colspan="2" align="center" valign="middle" style="padding:5px; font-size:12px; width:50%">Bid Events Description: </td>
																	</tr>
																	<tr valign="middle" >
																		<td colspan="2" align="center" valign="middle" bgcolor="#FFFFFF" style="padding:15px 5px; font-size:12px; width:50%">
																			<div id="fromDS">
																				<asp:dropdownlist ID="BuyerBidForBac" runat="server"
																					DataSourceID="dsBuyerBidForBac"
																					DataTextField="ItemDesc"
																					DataValueField="BacRefNo" AutoPostBack="True"
																					OnDataBound="BuyerBidForBac_DataBound" onload="Page_LoadComplete" >
																				</asp:dropdownlist>

																				<asp:SqlDataSource ID="dsBuyerBidForBac" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>'
																					SelectCommand="SELECT * FROM tblBACBidItems WHERE BacRefNo=@BacRefNo AND Status=@Status" >
																					<SelectParameters>
																						<asp:SessionParameter Name="BacRefNo" SessionField="BuyerBacRefNo" Type="Int32" />
																						<asp:Parameter Name="Status" DefaultValue="0" Type="Int32" />
																					</SelectParameters>
																				</asp:SqlDataSource>
																			</div>

																			<input type="hidden" id="lblTotalAll" name="lblTotalAll" runat="server" />
																		</td>
																		<td colspan="2" align="center" valign="middle" bgcolor="#FFFFFF" style="padding:15px 5px; font-size:12px; width:50%">
																			<asp:TextBox ID="ItemDescLbl" runat="server" Width="350"></asp:TextBox>
																		</td>
																	</tr>
																	<tr valign="middle">
																		<td align="center" valign="middle" style="width:25%;">SAP PR No.:</td>
																		<td align="center" valign="middle" style="width:25%;">Amount:</td>
																		<td align="center" valign="middle" style="width:25%;">SAP PR Date:</td>
																		<td align="center" valign="middle" style="width:25%;">Date Received</td>
																	</tr>
																	<tr valign="middle">
																		<td align="center" valign="middle" style="background-color:#FFFFFF; width:25%;">
																			<asp:TextBox ID="PRRefNoLbl" runat="server" Width="75"></asp:TextBox>
																		</td>
																		<td align="center" valign="middle" bgcolor="#FFFFFF" style="width:25%;">
																			<asp:TextBox ID="EstItemValueLbl" runat="server" Text="0" Width="75"></asp:TextBox>
																		</td>
																		<td align="center" valign="middle" bgcolor="#FFFFFF" style="width:25%;">
																			<asp:TextBox ID="PRDate" runat="server" Width="75"></asp:TextBox>
																		</td>
																		<td align="center" valign="middle" bgcolor="#FFFFFF" style="width:25%;">
																			<asp:TextBox ID="DateApprovedLbl" runat="server" Width="75"></asp:TextBox>
																		</td>
																	</tr>
																	<tr valign="middle">
																		<td colspan="2" align="center" valign="middle" style="width:25%;">
																			<table width="100%" border="0" cellspacing="0" cellpadding="0">
																				<tr>
																					<td><input type="radio" runat="server" id="Budgeted" name="Budgeted" value="1" />
																						<label for="Budgeted">Budgeted</label>
																					</td>
																					<td>
																						<input type="radio" runat="server" id="UnBudgeted" name="Budgeted" value="0" checked="true" />
																						<label for="UnBudgeted">UnBudgeted</label>
																					</td>
																				</tr>
																			</table>
																		</td>
																		<td colspan="2" align="center" valign="middle" style="width:25%;">
																			<table width="100%" border="0" cellspacing="0" cellpadding="0">
																				<tr>
																					<td class="style1"><input name="CompanyId" runat="server" value="0" id="CompanyIdGT" type="radio" checked="true" />
																						<label for="CompanyIdGT">GT</label>
																					</td>
																					<td>
																						<input name="CompanyId"  id="CompanyIdIC" runat="server" value="1" type="radio" />
																						<label for="CompanyIdIC">IC</label>
																					</td>
																					<td>
																						<input name="CompanyId"  id="CompanyIdGXI" runat="server" value="2" type="radio" />
																						<label for="CompanyIdGXI">GXI</label>
																					</td>
																					<td>
																						<input name="CompanyId"  id="CompanyIdEGG" runat="server" value="4" type="radio" />
																						<label for="CompanyIdEGG">EGG</label>
																					</td>
																				</tr>
																			</table>
																		</td>
																	</tr>
																</tbody>
															</table>
															<asp:SqlDataSource ID="dsBuyerBidDetailsForBac" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>'
																	SelectCommand="SELECT t1.*, t3.Company FROM tblBidItems t1 INNER JOIN rfcCompany t3 ON t3.CompanyId = t1.CompanyId WHERE t1.Status = @Status
																	AND t1.ForAuction = 0 AND t1.BuyerId = @BuyerId AND t1.BidRefNo = @BidRefNo AND t3.CompanyId = t1.CompanyId ORDER BY t1.DateCreated DESC" >
																<SelectParameters>
																	<asp:SessionParameter Name="BuyerId" SessionField="UserId" Type="Int32" />
                                                                    <asp:SessionParameter Name="BidRefNo" SessionField="BuyerBidForBac" Type="Int32" />
																	<asp:Parameter Name="Status" DefaultValue="4" Type="Int32" />
																</SelectParameters>
															</asp:SqlDataSource>
														</td>
													</tr>
                                                    <tr>
                                                        <td colspan="2">
                                                            <table width="100%" border="1" cellpadding="0" cellspacing="0" rules="all" class="itemDetails" id="Biddetails_details1_dvEventDetails9" style="border-color:#71A9D2; font-family: Arial; font-size: 11px; border-collapse: collapse; background-color: #F4F1C4; height:100%">
																<tbody>
																	<tr>
																		<td class="ui-widget-header" style="height:26px; vertical-align:middle;">EXECUTIVE SUMMARY</td>
																	</tr>
																	<tr valign="middle" >
																		<td align="center" valign="middle" style="padding:5px; font-size:12px; width:50%"><label for="textarea"></label>
																			<textarea name="executive_summary" id="executive_summary" cols="45" rows="10" runat="server" style="width:100%"></textarea>
																		</td>
																	</tr>
																</tbody>
															</table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">
                                                            <table width="50%" border="1" cellpadding="0" cellspacing="0" rules="all" class="itemDetails" id="Biddetails_details1_dvEventDetails8" style="border-color:#71A9D2; font-family: Arial; font-size: 11px; border-collapse: collapse; background-color: #F4F1C4; height:100%">
																<tbody>
																	<tr>
																		<td colspan="2" class="ui-widget-header" style="height:26px; vertical-align:middle;">REMARKS / COMMENTS</td>
																	</tr>
																	<tr valign="middle" >
																		<td align="center" valign="middle" style="padding:5px; font-size:12px; width:50%">
																			<label for="textarea"></label>
																			<textarea name="remarks_comments" id="remarks_comments" cols="45" rows="10" style="width:100%" runat="server" ></textarea>
																		</td>
																	</tr>
																</tbody>
															</table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td valign="top">
															<table width="50%" border="1" cellpadding="0" cellspacing="0" rules="all" class="itemDetails" id="Table5" style="border-color:#71A9D2; font-family: Arial; font-size: 11px; border-collapse: collapse; background-color: #F4F1C4;">
																<tbody>
																	<tr>
																		<td class="ui-widget-header" style="height:26px; vertical-align:middle;">SUPPLY POSITION</td>
																	</tr>
																	<tr valign="middle">
																		<td align="center" valign="middle" style="width:25%;">
																			<table width="100%" border="0" cellspacing="0" cellpadding="0">
																				<tr>
																					<td width="60%">
																						<table>
																							<tr><td class="style2">
																								<asp:DropDownList ID="cbSupplyPosition" runat="server"
																									DataSourceID="dsSupplyPosition" DataTextField="SupplyPosition"
																									DataValueField="ID" ondatabound="cbSupplyPosition_DataBound">
																								</asp:DropDownList>
																								<asp:SqlDataSource ID="dsSupplyPosition" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>'
																									SelectCommand="SELECT * FROM rfcSupplyPosition ORDER BY ID" >
																								</asp:SqlDataSource>
																							</td></tr>
																							<tr><td class="style2">
																								<asp:TextBox ID="txtNewSupplyPosition" runat="server" Width="150"></asp:TextBox>
																								<input id="Button1" type="button" value="Others" onclick="AddOtherSupplyPosition();" />
																							</td></tr>
																						</table>
																					</td>
																					<td>
																						<table id="mySupplyPosition"></table>
																					</td>
																				</tr>
																			</table>
																		</td>
																	</tr>
																</tbody>
															</table>
                                                        </td>
                                                        <td valign="top">
															<table width="50%" border="1" cellpadding="0" cellspacing="0" rules="all" class="itemDetails" id="Biddetails_details1_dvEventDetails5" style="border-color:#71A9D2; font-family: Arial; font-size: 11px; border-collapse: collapse; background-color: #F4F1C4;">
																<tbody>
																	<tr>
																		<td class="ui-widget-header" style="height:26px; vertical-align:middle;">SOURCING STRATEGY</td>
																	</tr>
																	<tr valign="middle">
																		<td align="center" valign="middle" style="width:25%;">
																			<table width="100%" border="0" cellspacing="0" cellpadding="0">
																				<tr>
																					<td width="60%">
																						<table>
																							<tr><td>
																								<asp:DropDownList ID="cbSourcingStrategy" runat="server"
																									DataSourceID="dsSourcingStrategy" DataTextField="SourcingStrategy"
																									DataValueField="ID" ondatabound="cbSourcingStrategy_DataBound">
																								</asp:DropDownList>
																								<asp:SqlDataSource ID="dsSourcingStrategy" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>'
																									SelectCommand="SELECT * FROM rfcSourcingStrategy ORDER BY ID" >
																								</asp:SqlDataSource>
																							</td></tr>
																							<tr><td>
																								<asp:TextBox ID="txtNewSourcingStrategy" runat="server" Width="150"></asp:TextBox>
																								<input id="btnNewSS" type="button" value="Others" onclick="AddOtherSourcingStrategy();" />
																							</td></tr>
																						</table>
																					</td>
																					<td>
																						<table>
																							<tr><td>
																								<label for="ss_nobidders">No. of Invited Bidders: </label>
																								<input name="ss_nobidders" type="text" id="ss_nobidders" size="3" value="0" runat="server"  />
																							</td></tr>
																							<tr><td>
																								<label for="ss_nobidsrcvd">No. of Bids Received: </label>
																								<input name="ss_nobidsrcvd" type="text" id="ss_nobidsrcvd" size="3" value="0" runat="server"  />
																							</td></tr>
																							<tr><td>
																								<label for="ss_techcompliance">Tech. Compliance: </label>
																								<input name="ss_techcompliance" type="text" id="ss_techcompliance" size="3" value="0" runat="server"  />
																							</td></tr>
																						</table>
																					</td>
																				</tr>
																			</table>
																		</td>
																	</tr>
																	<tr><td>
																		<table id="mySourcingStrategy" runat="server"></table>
																	</td></tr>
																</tbody>
															</table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td valign="top">
															<table width="60%" border="1" cellpadding="0" cellspacing="0" rules="all" class="itemDetails" id="Biddetails_details1_dvEventDetails6" style="border-color:#71A9D2; font-family: Arial; font-size: 11px; border-collapse: collapse; background-color: #F4F1C4; width:99%; ">
																<tbody>
																	<tr>
																		<td class="ui-widget-header" style="height:26px; vertical-align:middle;">TYPE OF PURCHASE</td>
																	</tr>
																	<tr valign="middle">
																		<td align="center" valign="middle" style="width:25%;">
																			<table width="100%" border="0" cellspacing="0" cellpadding="0">
																				<tr>
																					<td width="60%">
																						<table>
																							<tr><td>
																								<asp:DropDownList ID="cbTypeOfPurchase" runat="server"
																									DataSourceID="dsTypeOfPurchase" DataTextField="TypeOfPurchase"
																									DataValueField="ID" ondatabound="cbTypeOfPurchase_DataBound" >
																								</asp:DropDownList>
																								<asp:SqlDataSource ID="dsTypeOfPurchase" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>'
																									SelectCommand="SELECT * FROM rfcTypeOfPurchase ORDER BY ID" >
																								</asp:SqlDataSource>
																							</td></tr>
																							<tr><td>
																								<asp:TextBox ID="txtNewTypeOfPurchase" runat="server" Width="150"></asp:TextBox>
																								<input id="btnTOP" type="button" value="Others" onclick="AddOtherTypeOfPurchase();" />
																							</td></tr>
																						</table>
																					</td>
																					<td>
																						<table id="myTypeOfPurchase"></table>
																					</td>
																				</tr>
																			</table>
																		</td>
																	</tr>
																</tbody>
															</table>
														</td>
                                                        <td valign="top">
															<table width="50%" border="1" cellpadding="0" cellspacing="0" rules="all" class="itemDetails" id="Biddetails_details1_dvEventDetails4" style="border-color:#71A9D2; font-family: Arial; font-size: 11px; border-collapse: collapse; background-color: #F4F1C4; width:99%;">
																<tbody>
																	<tr>
																		<td class="ui-widget-header" style="height:26px; vertical-align:middle;">PAYMENT TERMS</td>
																	</tr>
																	<tr valign="middle">
																		<td align="center" valign="middle" style="width:25%;">
																			<table width="100%" border="0" cellspacing="0" cellpadding="0">
																				<tr>
																					<td width="60%">
																						<table>
																							<tr><td style="border:none">
																								<asp:DropDownList ID="cbPaymentTerms" runat="server"
																									DataSourceID="dsPaymentTerms" DataTextField="PaymentTerm" DataValueField="ID"
																									ondatabound="cbPaymentTerms_DataBound">
																								</asp:DropDownList>
																								<asp:SqlDataSource ID="dsPaymentTerms" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>'
																									SelectCommand="SELECT * FROM rfcPaymentTerms ORDER BY ID" >
																								</asp:SqlDataSource>
																							</td>
                                                                                            <td>
                                                                                                <textarea id="txtPaymentOthers" name="txtPaymentOthers" cols="20" rows="3" runat="server"></textarea>
                                                                                            </td></tr>
																						</table>
																					</td>
																					<td>
																						<table id="myPaymentTerms"></table>
																					</td>
																				</tr>
																			</table>
																		</td>
																	</tr>
																</tbody>
															</table>
                                                        </td>
                                                    </tr>
                                                    <tr>
														<td valign="top">
															<table border="1" cellpadding="0" cellspacing="0" rules="all" class="itemDetails" id="Biddetails_details1_dvEventDetails2" style="border-color:#71A9D2; font-family: Arial; font-size: 11px; border-collapse: collapse; background-color: #F4F1C4;">
																<tbody>
																	<tr>
																		<td class="ui-widget-header" style="height:26px; vertical-align:middle;">SUPPORTING DOCUMENTS ATTACHED</td>
																	</tr>
																	<tr valign="middle" >
																		<td align="center" valign="middle" style="padding:5px; font-size:12px; width:50%">
																			<table width="100%" border="0" cellspacing="0" cellpadding="0">
																				<tr>
																					<td width="50%">
																						<strong><label id="doc_ApprovedPR" runat="server">Approved PR</label></strong><br />
																						<asp:Repeater ID="Repeater_SDA_APR" runat="server" DataSourceID="dsSDA_APR" onitemdatabound="Repeater_SDA_APR_ItemDataBound" >
																							<ItemTemplate>
																								<input id="chkSDA_APR" name="chkSDA_APR" type="checkbox" runat="server" checked="true" />
																								<a id="lnkSDA_APR" href="bidawardingchecklist.aspx?ShowAttachment=<%# Eval("FileUploadID") %>" target="_blank"><%# Eval("OriginalFileName") %></a><br />
																							</ItemTemplate>
																						</asp:Repeater>
																						<asp:SqlDataSource ID="dsSDA_APR" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>'
																							SelectCommand="SELECT * FROM tblBACSupportingDocuments WHERE BuyerID=@BuyerID AND BidRefNo=@BidRefNo AND DocuName='Approved_PR' ORDER BY FileUploadID"
																							onload="dsSDA_APR_Load" >
																							<SelectParameters>
																								<asp:SessionParameter Name="BuyerId" SessionField="UserId" Type="Int32" />
                                                                                                <asp:SessionParameter Name="BidRefNo" SessionField="BuyerBidForBac" Type="Int32" />
																							</SelectParameters>
																						</asp:SqlDataSource>
																					</td>
																					<td width="50%">
																						<strong><label id="doc_Commercial" runat="server">Commercial Evaluation</label></strong><br />
																						<asp:Repeater ID="Repeater_SDA_CE" runat="server" DataSourceID="dsSDA_CE"
																							onitemdatabound="Repeater_SDA_CE_ItemDataBound">
																							<ItemTemplate>
																								<input id="chkSDA_CE" name="chkSDA_CE" type="checkbox" runat="server" checked="true" />
																								<a href="../fileattachments/<% =Session["UserId"] %>/BAC/<%# Eval("ActualFileName") %>" target="_blank"><%# Eval("OriginalFileName") %></a><br />
																							</ItemTemplate>
																						</asp:Repeater>
																						<asp:SqlDataSource ID="dsSDA_CE" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>'
																							SelectCommand="SELECT * FROM tblBACSupportingDocuments WHERE BuyerID=@BuyerID AND BidRefNo=@BidRefNo AND DocuName='Commercial_Evaluation' ORDER BY FileUploadID"
																							onload="dsSDA_CE_Load" >
																							<SelectParameters>
																								<asp:SessionParameter Name="BuyerId" SessionField="UserId" Type="Int32" />
																								<asp:SessionParameter Name="BidRefNo" SessionField="BuyerBidForBac" Type="Int32" />
																							</SelectParameters>
																						</asp:SqlDataSource>
																					</td>
																				</tr>
																				<tr>
																					<td>
																						<strong><label id="doc_ApprovedBC" runat="server">Approved Business Case</label></strong><br />
																						<asp:Repeater ID="Repeater_SDA_ABC" runat="server" DataSourceID="dsSDA_ABC"
																							onitemdatabound="Repeater_SDA_ABC_ItemDataBound">
																							<ItemTemplate>
																								<input id="chkSDA_ABC" name="chkSDA_ABC" type="checkbox" runat="server" checked="true" />
																								<a href="../fileattachments/<% =Session["UserId"] %>/BAC/<%# Eval("ActualFileName") %>" target="_blank"><%# Eval("OriginalFileName") %></a><br />
																							</ItemTemplate>
																						</asp:Repeater>
																						<asp:SqlDataSource ID="dsSDA_ABC" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>'
																							SelectCommand="SELECT * FROM tblBACSupportingDocuments WHERE BuyerID=@BuyerID AND BidRefNo=@BidRefNo AND DocuName='Approved_Business_Case' ORDER BY FileUploadID"
																							onload="dsSDA_ABC_Load" >
																							<SelectParameters>
																								<asp:SessionParameter Name="BuyerId" SessionField="UserId" Type="Int32" />
																								<asp:SessionParameter Name="BidRefNo" SessionField="BuyerBidForBac" Type="Int32" />
																							</SelectParameters>
																						</asp:SqlDataSource>
																					</td>
																					<td>
																						<strong><label id="doc_Negotiation" runat="server">Negotiation Results</label></strong><br />
																						<asp:Repeater ID="Repeater_SDA_NR" runat="server" DataSourceID="dsSDA_NR"
																							onitemdatabound="Repeater_SDA_NR_ItemDataBound">
																							<ItemTemplate>
																								<input id="chkSDA_NR" name="chkSDA_NR" type="checkbox" runat="server" checked="true" />
																								<a href="../fileattachments/<% =Session["UserId"] %>/BAC/<%# Eval("ActualFileName") %>" target="_blank"><%# Eval("OriginalFileName") %></a><br />
																							</ItemTemplate>
																						</asp:Repeater>
																						<asp:SqlDataSource ID="dsSDA_NR" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>'
																							SelectCommand="SELECT * FROM tblBACSupportingDocuments WHERE BuyerID=@BuyerID AND BidRefNo=@BidRefNo AND DocuName='Negotiation_Results' ORDER BY FileUploadID"
																							onload="dsSDA_NR_Load" >
																							<SelectParameters>
																								<asp:SessionParameter Name="BuyerId" SessionField="UserId" Type="Int32" />
																								<asp:SessionParameter Name="BidRefNo" SessionField="BuyerBidForBac" Type="Int32" />
																							</SelectParameters>
																						</asp:SqlDataSource>
																					</td>
																				</tr>
																				<tr>
																					<td>
																						<strong><label id="doc_Board" runat="server">Board Resolution</label></strong><br />
																						<asp:Repeater ID="Repeater_SDA_BR" runat="server" DataSourceID="dsSDA_BR"
																							onitemdatabound="Repeater_SDA_BR_ItemDataBound">
																							<ItemTemplate>
																								<input id="chkSDA_BR" name="chkSDA_BR" type="checkbox" runat="server" checked="true" />
																								<a href="../fileattachments/<% =Session["UserId"] %>/BAC/<%# Eval("ActualFileName") %>" target="_blank"><%# Eval("OriginalFileName") %></a><br />
																							</ItemTemplate>
																						</asp:Repeater>
																						<asp:SqlDataSource ID="dsSDA_BR" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>'
																							SelectCommand="SELECT * FROM tblBACSupportingDocuments WHERE BuyerID=@BuyerID AND BidRefNo=@BidRefNo AND DocuName='Board_Resolution' ORDER BY FileUploadID"
																							onload="dsSDA_BR_Load" >
																							<SelectParameters>
																								<asp:SessionParameter Name="BuyerId" SessionField="UserId" Type="Int32" />
																								<asp:SessionParameter Name="BidRefNo" SessionField="BuyerBidForBac" Type="Int32" />
																							</SelectParameters>
																						</asp:SqlDataSource>
																					</td>
																					<td>
																						<strong><label id="doc_Others" runat="server">Others</label></strong><br />
																						<asp:Repeater ID="Repeater_SDA_Oth" runat="server" DataSourceID="dsSDA_Oth"
																							onitemdatabound="Repeater_SDA_Oth_ItemDataBound">
																							<ItemTemplate>
																								<input id="chkSDA_Oth" name="chkSDA_Oth" type="checkbox" runat="server" checked="true" />
																								<a href="../fileattachments/<% =Session["UserId"] %>/BAC/<%# Eval("ActualFileName") %>" target="_blank"><%# Eval("OriginalFileName") %></a><br />
																							</ItemTemplate>
																						</asp:Repeater>
																						<asp:SqlDataSource ID="dsSDA_Oth" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>'
																							SelectCommand="SELECT * FROM tblBACSupportingDocuments WHERE BuyerID=@BuyerID AND BidRefNo=@BidRefNo AND DocuName='Others' ORDER BY FileUploadID"
																							onload="dsSDA_Oth_Load" >
																							<SelectParameters>
																								<asp:SessionParameter Name="BuyerId" SessionField="UserId" Type="Int32" />
																								<asp:SessionParameter Name="BidRefNo" SessionField="BuyerBidForBac" Type="Int32" />
																							</SelectParameters>
																						</asp:SqlDataSource>
																					</td>
																				</tr>
																				<tr>
																					<td>
																						<strong><label id="doc_Technical" runat="server">Technical Evaluation</label></strong><br />
																						<asp:Repeater ID="Repeater_SDA_TE" runat="server" DataSourceID="dsSDA_TE"
																							onitemdatabound="Repeater_SDA_TE_ItemDataBound">
																							<ItemTemplate>
																								<input id="chkSDA_TE" name="chkSDA_TE" type="checkbox" runat="server" checked="true" />
																								<a href="../fileattachments/<% =Session["UserId"] %>/BAC/<%# Eval("ActualFileName") %>" target="_blank"><%# Eval("OriginalFileName") %></a><br />
																							</ItemTemplate>
																						</asp:Repeater>
																						<asp:SqlDataSource ID="dsSDA_TE" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>'
																							SelectCommand="SELECT * FROM tblBACSupportingDocuments WHERE BuyerID=@BuyerID AND BidRefNo=@BidRefNo AND DocuName='Technical_Evaluation' ORDER BY FileUploadID"
																							onload="dsSDA_TE_Load" >
																							<SelectParameters>
																								<asp:SessionParameter Name="BuyerId" SessionField="UserId" Type="Int32" />
																								<asp:SessionParameter Name="BidRefNo" SessionField="BuyerBidForBac" Type="Int32" />
																							</SelectParameters>
																						</asp:SqlDataSource>
																					</td>
																					<td>&nbsp;</td>
																				</tr>
																				<tr>
																					<td style="border-bottom:none">&nbsp;
																						
																					</td>
																					<td style="border-bottom:none">&nbsp;
																						
																					</td>
																				</tr>
																				<tr>
																					<td style="border-bottom:none" colspan="2">
																						<asp:DropDownList ID="SupportDocuAttached" runat="server">
																							<asp:ListItem Value="Approved_PR">Approved PR</asp:ListItem>
																							<asp:ListItem Value="Approved_Business_Case">Approved Business Case</asp:ListItem>
																							<asp:ListItem Value="Board_Resolution">Board Resolution</asp:ListItem>
																							<asp:ListItem Value="Technical_Evaluation">Technical Evaluation</asp:ListItem>
																							<asp:ListItem Value="Commercial_Evaluation">Commercial Evaluation</asp:ListItem>
																							<asp:ListItem Value="Negotiation_Results">Negotiation Results</asp:ListItem>
																							<asp:ListItem Value="Others">Others</asp:ListItem>
																						</asp:DropDownList>
																					</td>
																				</tr>
																				<tr>
																					<td style="border-bottom:none" colspan="2">
																						<asp:FileUpload ID="fileUpload" runat="server" Width="350px" /><br />
																						<asp:LinkButton ID="lnkAttach" runat="server" CausesValidation="false">Add To Attachments List</asp:LinkButton><br />
																						<asp:Literal ID="addAttachmentMsg" runat="server"></asp:Literal>
																					</td>
																				</tr>
																				<tr>
																					<td style="border-bottom:none" colspan="2">&nbsp;
																						
																					</td>
																				</tr>
																			</table>
																		</td>
																	</tr>
																	<tr valign="middle">
																		<td valign="middle"></td>
																	</tr>
																</tbody>
															</table>
														</td>
														<td valign="top">
															<table width="60%" border="1" cellpadding="0" cellspacing="0" rules="all" class="itemDetails" id="Biddetails_details1_dvEventDetails3" style="border-color:#71A9D2; font-family: Arial; font-size: 11px; border-collapse: collapse; background-color: #F4F1C4;">
																<tbody>
																	<tr>
																		<td class="ui-widget-header" style="height:26px; vertical-align:middle;">BASIS FOR AWARDING</td>
																	</tr>
																	<tr valign="middle">
																		<td align="center" valign="middle" style="width:25%;">
																			<table width="100%" border="0" cellspacing="0" cellpadding="0">
																				<tr>
																					<td width="60%">
																						<table>
																							<tr><td>
																								<asp:DropDownList ID="cbBasisForAwarding" runat="server"
																									DataSourceID="dsBasisForAwarding" DataTextField="BasisForAwarding"
																									DataValueField="ID" ondatabound="cbBasisForAwarding_DataBound" >
																								</asp:DropDownList>
																								<asp:SqlDataSource ID="dsBasisForAwarding" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>'
																									SelectCommand="SELECT * FROM rfcBasisForAwarding ORDER BY ID" >
																								</asp:SqlDataSource>
																							</td></tr>
																							<tr><td>
																								<asp:TextBox ID="txtNewBasisForAwarding" runat="server" Width="150"></asp:TextBox>
																								<input id="btnBFA" type="button" value="Others" onclick="AddOtherBasisForAwarding();" />
																							</td></tr>
																						</table>
																					</td>
																				
																				</tr>
																			</table>
																		</td>

																	</tr>
                                                                    <tr>
                                                                    <td>
																						<table id="myBasisForAwarding"></table>
																					</td>
                                                                    </tr>
																</tbody>
															</table>
														</td>
													</tr>
                                                    <tr>
														<td colspan="2" valign="top">
															<table width="60%" border="1" cellpadding="0" cellspacing="0" rules="all" class="itemDetails" id="Biddetails_details1_dvEventDetails10" style="border-color:#71A9D2; font-family: Arial; font-size: 11px; border-collapse: collapse; background-color: #F4F1C4; height:100%">
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
																		<td style="padding:5px; font-size:12px; width:90%;">
																			<div style="width:100%; overflow:auto;">
																				<table width="100%" border="0" cellspacing="0" cellpadding="0" id="tblCriteria" runat="server">
                                                                                <tbody>
																					<tr id="Tr1" bgcolor="#E4EFF8" runat="server">
																						<td id="Td1" runat="server">
																							<strong>VENDOR / CRITERIA</strong>
																						</td>
                                                                                        <td id="Td2" runat="server">
																						    <asp:Repeater id="Repeater4" runat="server" DataSourceID="dsBidItemTendersSummary" onitemdatabound="Repeater4_ItemDataBound" >
																							    <ItemTemplate>
                                                                                                    <td id="Td3" runat="server">
                                                                                                        <span style="width:150px">
																									        <asp:TextBox ID="txtVendorName" runat="server" Width="120" Text='<%# Eval("VendorName") %>'></asp:TextBox>
                                                                                                            <asp:Image ID="imgVendor" runat="server" ImageUrl="~/web/buyerscreens/remove.png" />
																									        <input type="hidden" name="fromDB" id="fromDB" value='<%# Eval("fromDB") %>' runat="server" />
                                                                                                        </span>
                                                                                                    </td>
																							    </ItemTemplate>
																						    </asp:Repeater>
                                                                                        </td>
																					</tr>
																					
                                                                                    <tr id="Tr2" runat="server">
																						<td id="Td4" runat="server">
																							<strong>Accreditation Status</strong>
																						</td>
                                                                                        <td id="Td5" runat="server">
																						    <asp:Repeater id="Repeater100" runat="server" DataSourceID="dsBidItemTendersSummary" onitemdatabound="Repeater100_ItemDataBound" >
																							    <ItemTemplate>
                                                                                                    <td id="Td6" runat="server">
																								        <span style="width:150px">
																									        <asp:DropDownList ID="cbAccreditation" runat="server" DataSourceID="dsAccreditation" DataTextField="SupplierTypeDesc" DataValueField="SupplierTypeId" ></asp:DropDownList>
																								        </span>
                                                                                                    </td>
																							    </ItemTemplate>
																						    </asp:Repeater>
                                                                                        </td>
																					</tr>
																					<tr id="Tr3" bgcolor="#E4EFF8" runat="server">
																						<td id="Td7" runat="server">
																							<b>Performance Rating</b>
																						</td>
                                                                                        <td id="Td8" runat="server">
																						    <asp:Repeater id="RepeaterPRating" runat="server" DataSourceID="dsBidItemTendersSummary" onitemdatabound="RepeaterPRating_ItemDataBound" >
																							    <ItemTemplate>
																								    <td id="Td9" runat="server">
                                                                                                        <span style="width:150px">
																									        <asp:TextBox ID="txtPRating" runat="server" Width="40" Text='<%# Eval("PerformanceRating") %>'></asp:TextBox>
                                                                                                        </span>
																								    </td>
																							    </ItemTemplate>
																						    </asp:Repeater>
                                                                                        </td>
																					</tr>
																					<tr id="Tr4" runat="server">
																						<td id="Td10" runat="server">Technical Compliance</td>
                                                                                        <td id="Td11" runat="server">
																						    <asp:Repeater id="Repeater5" runat="server" DataSourceID="dsBidItemTendersSummary" onitemdatabound="Repeater5_ItemDataBound" >
																							    <ItemTemplate>
																								    <td id="Td12" runat="server">
                                                                                                        <span style="width:150px">
                                                                                                            <asp:DropDownList ID="txtTechnical" runat="server">
                                                                                                                <asp:ListItem>Non-Compliance</asp:ListItem>
                                                                                                                <asp:ListItem>Fully Complied</asp:ListItem>
                                                                                                            </asp:DropDownList>
                                                                                                        </span>
                                                                                                    </td>
																							    </ItemTemplate>
																						    </asp:Repeater>
                                                                                        </td>
																					</tr>
																					<tr id="Tr5" bgcolor="#E4EFF8" runat="server">
																						<td id="Td13" runat="server">Commercial Compliance</td>
                                                                                        <td id="Td14" runat="server">
																						    <asp:Repeater id="Repeater6" runat="server" DataSourceID="dsBidItemTendersSummary" onitemdatabound="Repeater6_ItemDataBound" >
																							    <ItemTemplate>
																								    <td id="Td15" runat="server">
                                                                                                        <span style="width:150px">
                                                                                                            <asp:DropDownList ID="txtCommercial" runat="server">
                                                                                                                <asp:ListItem>Non-Compliance</asp:ListItem>
                                                                                                                <asp:ListItem>Partial Complied</asp:ListItem>
                                                                                                                <asp:ListItem>Fully Complied</asp:ListItem>
                                                                                                            </asp:DropDownList>
                                                                                                        </span>
                                                                                                    </td>
																							    </ItemTemplate>
																						    </asp:Repeater>
                                                                                        </td>
																					</tr>
																					<tr id="Tr6" runat="server">
																						<td id="Td16" runat="server">Contract Compliance</td>
                                                                                        <td id="Td17" runat="server">
																						    <asp:Repeater id="Repeater7" runat="server" DataSourceID="dsBidItemTendersSummary" onitemdatabound="Repeater7_ItemDataBound" >
																							    <ItemTemplate>
																								    <td id="Td18" runat="server">
                                                                                                        <span style="width:150px">
                                                                                                            <asp:DropDownList ID="txtContract" runat="server">
                                                                                                                <asp:ListItem>Not Applicable</asp:ListItem>
                                                                                                                <asp:ListItem>Non-Compliance</asp:ListItem>
                                                                                                                <asp:ListItem>Partial Complied</asp:ListItem>
                                                                                                                <asp:ListItem>Fully Complied</asp:ListItem>
                                                                                                            </asp:DropDownList>
                                                                                                        </span>
                                                                                                    </td>
																							    </ItemTemplate>
																						    </asp:Repeater>
                                                                                        </td>
																					</tr>
                                                                                    
																					<tr runat="server" id="trHidden">
																						<td id="Td19" runat="server"><h3 style="border:none; color:#000"></h3></td>
                                                                                        <td id="Td20" runat="server">
																						    <asp:Repeater id="Repeater13" runat="server" DataSourceID="dsBidItemTendersSummary" onitemdatabound="Repeater13_ItemDataBound" >
																							    <ItemTemplate>
																								    <td id="Td21" runat="server">
                                                                                                        <span style="width:150px">
																									        <asp:CheckBox ID="chkAwarded" runat="server" Visible="false" />
                                                                                                        </span>
																								    </td>
																							    </ItemTemplate>
																						    </asp:Repeater>
                                                                                        </td>
																					</tr></tbody>
																				</table>
																				<table border="0" cellpadding="0" cellspacing="0"><tr>
																					<td align="center">
																						<input type="button" name="btnAddRow" id="btnAddRow" value="Add New Criteria (Row)" onclick="return btnAddRow_onclick();" />
																						&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
																						<input type="button" name="btnAddCol" id="btnAddCol" value="Add New Vendor (Column)" onclick="return btnAddCol_onclick();" />
																					</td>
																				</tr></table>
																			</div>
																		</td>
																	</tr>
																</tbody>
															</table>

                                                            <asp:SqlDataSource ID="dsBidItemTendersSummary" runat="server"
																ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>'
																SelectCommand="SELECT * FROM tblBACEvaluationSummary_tmp WHERE BidRefNo=@BidRefNo AND UserId=@UserId" onload="dsBidItemTendersSummary_Load" >
																<SelectParameters>
																	<asp:SessionParameter Name="BidRefNo" SessionField="BuyerBidForBac" Type="Int32" />
                                                                    <asp:SessionParameter Name="UserId" SessionField="UserId" Type="Int32" />
																</SelectParameters>
															</asp:SqlDataSource>
																			
															<asp:SqlDataSource ID="dsAccreditation" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>'
																SelectCommand="SELECT * FROM rfcSupplierType ORDER BY SupplierTypeId" >
															</asp:SqlDataSource>

															<table width="100%" border="1" cellpadding="0" cellspacing="0" rules="all" class="itemDetails" id="Table2" style="border-color:#71A9D2; font-family: Arial; font-size: 11px; border-collapse: collapse; background-color: #F4F1C4; height:100%">
																<tbody>
																	<tr>
																		<td class="ui-widget-header" style="height:26px; vertical-align:middle;">
																			ITEMS
																		</td>
																	</tr>
																	<tr><td>
																		<asp:GridView ID="gvItems" runat="server" AutoGenerateColumns="False"
																			DataSourceID="dsItems" Width="100%" onrowcommand="gvItems_RowCommand" >
																			<Columns>
																				<asp:TemplateField HeaderText="Item #" HeaderStyle-CssClass="ui-widget-header" >
																					<ItemTemplate>
																						&nbsp;<asp:LinkButton ID="lblItemNo" runat="server" Text='<%# Bind("SKU") %>' CommandArgument='<%# Bind("BidDetailNo") %>' CommandName="AddItemRow"></asp:LinkButton>
																					</ItemTemplate>
																				</asp:TemplateField>
																				<asp:TemplateField HeaderText="Item Details" HeaderStyle-CssClass="ui-widget-header" >
																					<ItemTemplate>
																						&nbsp;<asp:LinkButton ID="lblItemD" runat="server" Text='<%# Bind("ItemName") %>' CommandArgument='<%# Bind("BidDetailNo") %>' CommandName="AddItemRow"></asp:LinkButton>
																					</ItemTemplate>
																				</asp:TemplateField>
																				<asp:TemplateField HeaderText="Remove Item" HeaderStyle-CssClass="ui-widget-header" >
																					<ItemTemplate>
																						&nbsp;<asp:LinkButton ID="lblRemoveItem" runat="server" Text='Remove Item' CommandArgument='<%# Bind("BidDetailNo") %>' CommandName="DelItemRow"></asp:LinkButton>
																					</ItemTemplate>
																				</asp:TemplateField>
																			</Columns>
																		</asp:GridView>
																	</td></tr>
																</tbody>
															</table>
                                                            <asp:SqlDataSource ID="dsItems" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>'
																SelectCommand="SELECT DISTINCT t2.BidDetailNo, t2.Item SKU, t2.DetailDesc ItemName
																FROM tblBidTenders t1
																INNER JOIN tblBidItemDetails t2 ON t2.BidDetailNo = t1.BidDetailNo
																INNER JOIN tblBidItems t4 ON t4.BidRefNo = t2.BidRefNo
																WHERE t2.BidRefNo = @BidRefNo AND t1.Status > 0 ORDER BY t2.DetailDesc" >
																<SelectParameters>
                                                                    <asp:SessionParameter Name="BidRefNo" SessionField="BuyerBidForBac" Type="Int32" />
																</SelectParameters>
															</asp:SqlDataSource>

															<table width="100%" border="1" cellpadding="0" cellspacing="0" rules="all" class="itemDetails" id="Table1" style="border-color:#71A9D2; font-family: Arial; font-size: 11px; border-collapse: collapse; background-color: #F4F1C4; height:100%">
																<tbody>
																	<tr>
																		<td class="ui-widget-header" style="height:26px; vertical-align:middle;">
																			COMMERCIAL DETAILS
																		</td>
																	</tr>
																	<tr><td>
                                                                        <asp:DataList ID="lstCommDetails" runat="server" DataSourceID="dsItems1" 
                                                                            DataKeyField="BidDetailNo" onitemdatabound="lstCommDetails_ItemDataBound" >
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="BidDetailNo1" runat="server" Text='<%# Eval("BidDetailNo") %>' Visible="false"></asp:TextBox>
                                                                                <br />
                                                                                <b>Item #: </b>
                                                                                <asp:Label ID="SKULabel" runat="server" Text='<%# Eval("SKU") %>' />
                                                                                <br />
                                                                                <b>Item Name: </b>
                                                                                <asp:Label ID="ItemNameLabel" runat="server" Text='<%# Eval("ItemName") %>' />
                                                                                <br /><br />
                                                                                <table border="0" cellpadding="0" cellspacing="0"><tbody>
                                                                                    <tr>
                                                                                        <td bgcolor="#E4EFF8">
                                                                                           Vendor Name
                                                                                        </td>
                                                                                        <asp:Repeater id="rptVendorName" runat="server" DataSourceID="dsBidItemTendersDetails_Vendor" >
																					        <ItemTemplate>
                                                                                                <td style="width:150px" bgcolor="#E4EFF8">
																						            <asp:TextBox ID="txtVendorName" runat="server" Width="150" Text='<%# Eval("VendorName") %>' ></asp:TextBox>
                                                                                                    <asp:TextBox ID="txtBidTenderNo1" runat="server" Width="150" Text='<%# Eval("BidTenderNo") %>' Visible="false" ></asp:TextBox>
                                                                                                </td>
																					        </ItemTemplate>
																				        </asp:Repeater>
                                                                                        <asp:SqlDataSource ID="dsBidItemTendersDetails_Vendor" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>'
																	                        SelectCommand="SELECT VendorName, BidTenderNo FROM tblBACEvaluationDetails_tmp WHERE BidRefNo=@BidRefNo AND BidDetailNo=@BidDetailNo AND UserId=@UserId ORDER BY BidTenderNo" >
																	                        <SelectParameters>
                                                                                                <asp:SessionParameter Name="UserId" SessionField="UserId" Type="Int32" />
                                                                                                <asp:SessionParameter Name="BidRefNo" SessionField="BuyerBidForBac" Type="Int32" />
                                                                                                <asp:ControlParameter Name="BidDetailNo" ControlID="BidDetailNo1" Type="Int32" />
																	                        </SelectParameters>
																                        </asp:SqlDataSource>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>
                                                                                            Quantity
                                                                                        </td>
                                                                                        <asp:Repeater id="rptQty" runat="server" DataSourceID="dsBidItemTendersDetails_Qty" >
																					        <ItemTemplate>
                                                                                                <td style="width:150px">
																						            <asp:TextBox ID="txtQty" runat="server" Width="30" Text='<%# Eval("Qty") %>' ></asp:TextBox>
                                                                                                    <asp:TextBox ID="txtBidTenderNo1" runat="server" Width="150" Text='<%# Eval("BidTenderNo") %>' Visible="false" ></asp:TextBox>
                                                                                                </td>
																					        </ItemTemplate>
																				        </asp:Repeater>
                                                                                        <asp:SqlDataSource ID="dsBidItemTendersDetails_Qty" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>'
																	                        SelectCommand="SELECT BidTenderNo, Qty FROM tblBACEvaluationDetails_tmp WHERE BidRefNo=@BidRefNo AND BidDetailNo=@BidDetailNo AND UserId=@UserId ORDER BY BidTenderNo" >
																	                        <SelectParameters>
                                                                                                <asp:SessionParameter Name="UserId" SessionField="UserId" Type="Int32" />
																		                        <asp:SessionParameter Name="BidRefNo" SessionField="BuyerBidForBac" Type="Int32" />
                                                                                                <asp:ControlParameter Name="BidDetailNo" ControlID="BidDetailNo1" Type="Int32" />
																	                        </SelectParameters>
																                        </asp:SqlDataSource>
                                                                                    </tr>

                                                                                    <tr>
                                                                                        <td bgcolor="#E4EFF8">
                                                                                            Unit Of Measure
                                                                                        </td>
                                                                                        <asp:Repeater id="rptUnit" runat="server" DataSourceID="dsBidItemTendersDetails_Unit" >
																					        <ItemTemplate>
                                                                                                <td style="width:150px" bgcolor="#E4EFF8">
																						            <asp:TextBox ID="txtUnit" runat="server" Width="70" Text='<%# Eval("UnitMeasure") %>' ></asp:TextBox>
                                                                                                    <asp:TextBox ID="txtBidTenderNo1" runat="server" Width="150" Text='<%# Eval("BidTenderNo") %>' Visible="false" ></asp:TextBox>
                                                                                                </td>
																					        </ItemTemplate>
																				        </asp:Repeater>
                                                                                        <asp:SqlDataSource ID="dsBidItemTendersDetails_Unit" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>'
																	                        SelectCommand="SELECT BidTenderNo, UnitMeasure FROM tblBACEvaluationDetails_tmp WHERE BidRefNo=@BidRefNo AND BidDetailNo=@BidDetailNo AND UserId=@UserId ORDER BY BidTenderNo" >
																	                        <SelectParameters>
                                                                                                <asp:SessionParameter Name="UserId" SessionField="UserId" Type="Int32" />
																		                        <asp:SessionParameter Name="BidRefNo" SessionField="BuyerBidForBac" Type="Int32" />
                                                                                                <asp:ControlParameter Name="BidDetailNo" ControlID="BidDetailNo1" Type="Int32" />
																	                        </SelectParameters>
																                        </asp:SqlDataSource>
                                                                                    </tr>

                                                                                    <tr>
                                                                                        <td>
                                                                                            Currency
                                                                                        </td>
                                                                                        <asp:Repeater id="rptCurr" runat="server" DataSourceID="dsBidItemTendersDetails_Curr" >
																					        <ItemTemplate>
                                                                                                <td style="width:150px">
																						            <asp:TextBox ID="txtCurr" runat="server" Width="30" Text='<%# Eval("Currenzy") %>' ></asp:TextBox>
                                                                                                    <asp:TextBox ID="txtBidTenderNo1" runat="server" Width="150" Text='<%# Eval("BidTenderNo") %>' Visible="false" ></asp:TextBox>
                                                                                                </td>
																					        </ItemTemplate>
																				        </asp:Repeater>
                                                                                        <asp:SqlDataSource ID="dsBidItemTendersDetails_Curr" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>'
																	                       SelectCommand="SELECT BidTenderNo, Currenzy FROM tblBACEvaluationDetails_tmp WHERE BidRefNo=@BidRefNo AND BidDetailNo=@BidDetailNo AND UserId=@UserId ORDER BY BidTenderNo" >
																	                        <SelectParameters>
                                                                                                <asp:SessionParameter Name="UserId" SessionField="UserId" Type="Int32" />
																		                        <asp:SessionParameter Name="BidRefNo" SessionField="BuyerBidForBac" Type="Int32" />
                                                                                                <asp:ControlParameter Name="BidDetailNo" ControlID="BidDetailNo1" Type="Int32" />
																	                        </SelectParameters>
																                        </asp:SqlDataSource>
                                                                                    </tr>

                                                                                    <tr>
                                                                                        <td bgcolor="#E4EFF8">
                                                                                            Unit Cost
                                                                                        </td>
                                                                                        <asp:Repeater id="rptUnitCost" runat="server" DataSourceID="dsBidItemTendersDetails_UnitCost" >
																					        <ItemTemplate>
                                                                                                <td style="width:150px" bgcolor="#E4EFF8">
																						            <asp:TextBox ID="txtUnitCost" runat="server" Width="70" Text='<%# Eval("UnitCost") %>' ></asp:TextBox>
                                                                                                    <asp:TextBox ID="txtBidTenderNo1" runat="server" Width="150" Text='<%# Eval("BidTenderNo") %>' Visible="false" ></asp:TextBox>
                                                                                                </td>
																					        </ItemTemplate>
																				        </asp:Repeater>
                                                                                        <asp:SqlDataSource ID="dsBidItemTendersDetails_UnitCost" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>'
																	                       SelectCommand="SELECT BidTenderNo, UnitCost FROM tblBACEvaluationDetails_tmp WHERE BidRefNo=@BidRefNo AND BidDetailNo=@BidDetailNo AND UserId=@UserId ORDER BY BidTenderNo" >
																	                        <SelectParameters>
                                                                                                <asp:SessionParameter Name="UserId" SessionField="UserId" Type="Int32" />
																		                        <asp:SessionParameter Name="BidRefNo" SessionField="BuyerBidForBac" Type="Int32" />
                                                                                                <asp:ControlParameter Name="BidDetailNo" ControlID="BidDetailNo1" Type="Int32" />
																	                        </SelectParameters>
																                        </asp:SqlDataSource>
                                                                                    </tr>

                                                                                    <tr>
                                                                                        <td>
                                                                                            Total Cost
                                                                                        </td>
                                                                                        <asp:Repeater id="rptTotalCost" runat="server" DataSourceID="dsBidItemTendersDetails_TotalCost" >
																					        <ItemTemplate>
                                                                                                <td style="width:150px">
																						            <asp:TextBox ID="txtTotalCost" runat="server" Width="70" Text='<%# Eval("TotalCost") %>' ></asp:TextBox>
                                                                                                    <asp:TextBox ID="txtBidTenderNo1" runat="server" Width="150" Text='<%# Eval("BidTenderNo") %>' Visible="false" ></asp:TextBox>
                                                                                                </td>
																					        </ItemTemplate>
																				        </asp:Repeater>
                                                                                        <asp:SqlDataSource ID="dsBidItemTendersDetails_TotalCost" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>'
																	                       SelectCommand="SELECT BidTenderNo, TotalCost FROM tblBACEvaluationDetails_tmp WHERE BidRefNo=@BidRefNo AND BidDetailNo=@BidDetailNo AND UserId=@UserId ORDER BY BidTenderNo" >
																	                        <SelectParameters>
                                                                                                <asp:SessionParameter Name="UserId" SessionField="UserId" Type="Int32" />
																		                        <asp:SessionParameter Name="BidRefNo" SessionField="BuyerBidForBac" Type="Int32" />
                                                                                                <asp:ControlParameter Name="BidDetailNo" ControlID="BidDetailNo1" Type="Int32" />
																	                        </SelectParameters>
																                        </asp:SqlDataSource>
                                                                                    </tr>

                                                                                    <tr>
                                                                                        <td bgcolor="#E4EFF8">
                                                                                            Ranking
                                                                                        </td>
                                                                                        <asp:Repeater id="rptRanking" runat="server" DataSourceID="dsBidItemTendersDetails_Ranking" >
																					        <ItemTemplate>
                                                                                                <td style="width:150px" bgcolor="#E4EFF8">
																						            <asp:TextBox ID="txtRanking" runat="server" Width="30" Text='<%# Eval("Ranking") %>' ></asp:TextBox>
                                                                                                    <asp:TextBox ID="txtBidTenderNo1" runat="server" Width="150" Text='<%# Eval("BidTenderNo") %>' Visible="false" ></asp:TextBox>
                                                                                                </td>
																					        </ItemTemplate>
																				        </asp:Repeater>
                                                                                        <asp:SqlDataSource ID="dsBidItemTendersDetails_Ranking" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>'
																	                        SelectCommand="SELECT BidTenderNo, Ranking FROM tblBACEvaluationDetails_tmp WHERE BidRefNo=@BidRefNo AND BidDetailNo=@BidDetailNo AND UserId=@UserId ORDER BY BidTenderNo" >
																	                        <SelectParameters>
                                                                                                <asp:SessionParameter Name="UserId" SessionField="UserId" Type="Int32" />
																		                        <asp:SessionParameter Name="BidRefNo" SessionField="BuyerBidForBac" Type="Int32" />
                                                                                                <asp:ControlParameter Name="BidDetailNo" ControlID="BidDetailNo1" Type="Int32" />
																	                        </SelectParameters>
																                        </asp:SqlDataSource>
                                                                                    </tr>

                                                                                    <tr>
                                                                                        <td>
                                                                                            Award   
                                                                                        </td>
                                                                                        <asp:Repeater id="rptAward" runat="server" DataSourceID="dsBidItemTendersDetails_Award" >
																					        <ItemTemplate>
                                                                                                <td style="width:150px">
                                                                                                    <asp:CheckBox ID="chkAward" runat="server" />
                                                                                                    <asp:TextBox ID="txtAward" runat="server" Width="150" Text='<%# Eval("Chkd") %>' Visible="false" ></asp:TextBox>
                                                                                                    <asp:TextBox ID="txtBidTenderNo1" runat="server" Width="150" Text='<%# Eval("BidTenderNo") %>' Visible="false" ></asp:TextBox>
                                                                                                </td>
																					        </ItemTemplate>
																				        </asp:Repeater>
                                                                                        <asp:SqlDataSource ID="dsBidItemTendersDetails_Award" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>'
																	                        SelectCommand="SELECT BidTenderNo, Chkd FROM tblBACEvaluationDetails_tmp WHERE BidRefNo=@BidRefNo AND BidDetailNo=@BidDetailNo AND UserId=@UserId ORDER BY BidTenderNo" >
																	                        <SelectParameters>
                                                                                                <asp:SessionParameter Name="UserId" SessionField="UserId" Type="Int32" />
																		                        <asp:SessionParameter Name="BidRefNo" SessionField="BuyerBidForBac" Type="Int32" />
                                                                                                <asp:ControlParameter Name="BidDetailNo" ControlID="BidDetailNo1" Type="Int32" />
																	                        </SelectParameters>
																                        </asp:SqlDataSource>
                                                                                    </tr>
                                                                                </tbody></table>
                                                                            </ItemTemplate>
                                                                        </asp:DataList>
																	</td></tr>
																</tbody>
															</table>
                                                            <asp:SqlDataSource ID="dsItems1" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>'
																SelectCommand="SELECT DISTINCT BidDetailNo, ItemName, SKU FROM tblBACEvaluationDetails_tmp WHERE BidRefNo = @BidRefNo AND UserId=@UserId" >
																<SelectParameters>
                                                                    <asp:SessionParameter Name="UserId" SessionField="UserId" Type="Int32" />
                                                                    <asp:SessionParameter Name="BidRefNo" SessionField="BuyerBidForBac" Type="Int32" />
																</SelectParameters>
															</asp:SqlDataSource>

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
																					<td colspan="2">Reviewed by / date: <br />&nbsp;</td>
																				</tr>
																				<tr>
																					<td>
																						<label for="clarify4">Clarify</label>
																						<input name="clarify4" type="checkbox" id="clarify4" disabled="disabled" />
																					</td>
																					<td>
																						<label for="approved4">Approved</label>
																						<input type="checkbox" name="approved4" id="approved4" disabled="disabled" />
																					</td>
																				</tr>
																				<tr>
																					<td colspan="2"><strong><asp:Literal ID="lblApprover4" runat="server" Text="PURCHASING_APPROVER"></asp:Literal></strong></td>
																				</tr>
																				<tr>
																					<td colspan="2" style="border-top:2px #000 solid;">Purchasing Approver</td>
																				</tr>
																			</table>
																		</td>
																	</tr>
																</tbody>
															</table>

															<table width="100%" border="1" cellpadding="0" cellspacing="0" rules="all"
																class="itemDetails" id="BidTendersAccumulatedCost"
																style="border-color:#71A9D2; font-family: Arial; font-size: 11px; border-collapse: collapse; background-color: #F4F1C4; height:100%">
																<tbody>
																	<tr>
																		<td class="ui-widget-header" style="height:26px; vertical-align:middle;">NATURE OF SAVINGS</td>
																		<td class="ui-widget-header" style="height:26px; vertical-align:middle;">SAVINGS GENERATED</td>
																	</tr>
																	<tr valign="middle" >
																		<td width="50%" align="center">
																			<table width="50%">
																				<tr valign="middle"><td class="style2">
																					<asp:DropDownList ID="cbNatureOfSavings" runat="server"
																						DataSourceID="dsNatureOfSavings" DataTextField="NatureOfSavings"
																						DataValueField="ID" ondatabound="cbNatureOfSavings_DataBound">
																					</asp:DropDownList>
																					<asp:SqlDataSource ID="dsNatureOfSavings" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>'
																						SelectCommand="SELECT * FROM rfcNatureOfSavings ORDER BY ID" >
																					</asp:SqlDataSource>
																				</td></tr>
																				<tr><td class="style2">
																					<asp:TextBox ID="txtNewNatureOfSavings" runat="server" Width="150px"></asp:TextBox>
																					<input id="btnNewNOS" type="button" value="Others" onclick="AddOtherNatureOfSaving();" />
																				</td></tr>
																				<tr><td>
																					<table id="myNatureOfSavings" runat="server"></table>
																				</td></tr>
																			</table>
																		</td>
																		<td align="center" valign="middle" style="padding:5px; font-size:12px;" class="style4">
																			<table style="width: 80%;">
																				<tr>
																					<td class="style5">Accumulated Awarded Cost:</td>
																					<td>
                                                                                    <%--<label id="lblAccCost" runat="server">1,524,573.25</label>--%>
                                                                                    <input name="lblAccCost" type="text" id="lblAccCost" size="4" value="0" runat="server"  />
                                                                                    </td>
																				</tr>
																				<tr>
																					<td class="style5">Savings Amount:</td>
																					<td><%--<label id="lblNegDisc" runat="server">2,104,999.35</label>--%>
                                                                                    <input name="lblNegDisc" type="text" id="lblNegDisc" size="4" value="0" runat="server"  /></td>
																				</tr>
																				<tr>
																					<td class="style5">% from Savings:</td>
																					<td><%--<label id="lblPercentSpend" runat="server">11%</label>--%>
                                                                                    <input name="lblPercentSpend" type="text" id="lblPercentSpend" size="4" value="0" runat="server"  /></td>
																				</tr>
																			</table>
																		</td>
																	</tr>
																</tbody>
															</table>

															<!--<table width="100%" border="1" cellpadding="0" cellspacing="0" rules="all"
																class="itemDetails" id="Table3"
																style="border-color:#71A9D2; font-family: Arial; font-size: 11px; border-collapse: collapse; background-color: #F4F1C4; height:100%">
																<tbody>
																	<tr>
																		<td colspan="2" class="ui-widget-header" style="height:26px; vertical-align:middle;">CRC No. and PO No.</td>
																	</tr>
																	<tr valign="middle" >
																		<td width="50%" align="center">
																		    <table width="100%">
                                                                                <tr>
                                                                                    <td style="width:45%;">
                                                                                        <table><tr><td><strong>CRC No.</strong></td></tr></table>
                                                                                        <table id="myCRC"></table>
                                                                                    </td>
                                                                                    <td style="width:10%;">&nbsp;</td>
                                                                                    <td style="width:45%;">
                                                                                        <table><tr><td><strong>PO No.</strong></td></tr></table>
                                                                                        <table id="myPO1"></table>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
																		</td>
																		<td align="center" valign="middle" style="padding:5px; font-size:12px;" class="style4">
																			<table border="0" cellpadding="0" cellspacing="1">
                                                                                <tr>
                                                                                    <td>
                                                                                        CRC No.<br />
                                                                                        <asp:TextBox ID="txtNewCRC" runat="server" Width="150px"></asp:TextBox>
																					    <input id="btnewCRC" type="button" value="Add" onclick="AddCRC();" />
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        PO No.<br />
                                                                                        <asp:TextBox ID="txtNewPO" runat="server" Width="150px"></asp:TextBox>
																					    <input id="btnNewPO" type="button" value="Add" onclick="AddPO();" />
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
																		</td>
																	</tr>
																</tbody>
															</table>-->

															<table border="1" cellpadding="0" cellspacing="0" rules="all" class="itemDetails" id="Biddetails_details1_dvEventDetails12" style="border-color:#71A9D2; font-family: Arial; font-size: 11px; border-collapse: collapse; background-color: #F4F1C4; height:100%; width:100%;">
																<tbody>
																	<tr>
																		<td class="ui-widget-header" style="height:26px; vertical-align:middle;">BID APPROVING AUTHORITY</td>
																	</tr>
																	<tr valign="middle" >
																		<td width="100" align="center" valign="middle" style="padding:5px; font-size:12px;">
																			<asp:Repeater ID="RepeaterApprover" runat="server" DataSourceID="dsApprover" onitemdatabound="RepeaterApprover_ItemDataBound">
																				<ItemTemplate>
																					<div style="float:left; margin-right:15px; width:150px; padding:10px 20px;">
																						<table width="150px" border="0" cellspacing="0" cellpadding="0">
																							<tr>
																								<td colspan="2">Approved by / date: <br />&nbsp;</td>
																							</tr>
																							<tr>
																								<td><label for="clarify1">Clarify</label>
																									<asp:CheckBox ID="clarify1" runat="server" Enabled="false" />
																								</td>
																								<td><label for="approved1">Approved</label>
																									<asp:CheckBox ID="approved1" runat="server" Enabled="false" />
																								</td>
																							</tr>
																							<tr>
																								<td colspan="2"><strong><%# Eval("Name1") %></strong></td>
																							</tr>
																							<tr>
																								<td colspan="2"><%# String.Format("{0:0,0.00}", Eval("ApprovingMaxLimit")) %></td>
																							</tr>
																							<tr>
																								<td colspan="2" style="border-top:2px #000 solid;"><asp:Literal ID="lblApprover" runat="server" Text="APPROVER"></asp:Literal></td>
																							</tr>
																						</table>
																					</div>
																				</ItemTemplate>
																			</asp:Repeater>
																		</td>
																	</tr>
																</tbody>
															</table>
														</td>
                                                    </tr>
                                                    <tr>
														<td>
															<asp:SqlDataSource ID="dsApprover" runat="server"
																ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>'
																SelectCommand="SELECT t1.BACId, t1.LastName + ', ' + t1.FirstName + ' ' + t1.MiddleName AS Name1, t1.ApprovingMaxLimit
                                                                FROM tblBidAwardingCommittee t1
                                                                JOIN tblUsers t2 ON (t1.BACId=t2.UserId AND t2.IsAuthenticated=1)
                                                                ORDER BY t1.ApprovingMaxLimit ASC" >
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
                                                            <input id="btnDraft" type="button" value="Draft" onclick="document.getElementById('status').value=0; document.getElementById('form1').submit();" />
                                                            <input id="btnEndorse" type="button" value="Endorse"onclick="document.getElementById('status').value=1; document.getElementById('form1').submit();" />
                                                            <input id="btnCancel" type="button" value="Cancel" onclick="history.back();" />
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
<script type="text/javascript">HideCriteria(); ShowSavedCookies(); ShowRowsOnLoad(); ShowNewRowsOnLoad(); ComputeAccCost();</script>