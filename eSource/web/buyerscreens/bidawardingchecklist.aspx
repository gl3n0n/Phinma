<%@ Page Language="C#" AutoEventWireup="true" CodeFile="bidawardingchecklist.aspx.cs" Inherits="web_buyerscreens_bidawardingchecklist" EnableViewState="true" MaintainScrollPositionOnPostback="true"  %>
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

            var pickerOpts = {
                showOtherMonths: true,
                changeMonth: true,
                changeYear: true,
                dateFormat: "yy-M-dd"
            };

            $("#<%=PRDate.ClientID%>").datepicker(pickerOpts);
            $("#<%=DateApprovedLbl.ClientID%>").datepicker(pickerOpts);
            SetCriteria();
            BACCheckBox();
        });

        function BACCheckBox() {
            var sCtrl, sCtrl1;
            var iItemCount;

            // count total items first
            iItemCount = 0;
            for (var i = 0; i < 100; i++) {
                if (i < 10) {
                    sCtrl = "#lstCommDetails_ctl0" + i + "_rptQty_ctl00_txtQty";
                }
                else {
                    sCtrl = "#lstCommDetails_ctl" + i + "_rptQty_ctl00_txtQty";
                }
                if ($(sCtrl).length == 0) {
                    break;
                }
                else {
                    iItemCount++;
                }
            }
            // loop through items
            for (var i = 0; i < iItemCount; i++) {
                for (var j = 0; j < 100; j++) {
                    if (i < 10) {
                        sCtrl = "#lstCommDetails_ctl0" + i;
                        sCtrl1 = "#lstCommDetails_ctl0" + i;
                    }
                    else {
                        sCtrl = "#lstCommDetails_ctl" + i;
                        sCtrl1 = "#lstCommDetails_ctl" + i;
                    }

                    if (j < 10) {
                        sCtrl = sCtrl + "_rptAward_ctl0" + j + "_txtAward";
                        sCtrl1 = sCtrl1 + "_rptAward_ctl0" + j + "_chkAward";
                    }
                    else {
                        sCtrl = sCtrl + "_rptAward_ctl" + j + "_txtAward";
                        sCtrl1 = sCtrl1 + "_rptAward_ctl" + j + "_chkAward";
                    }
                    if ($(sCtrl).length == 0) {
                        break;
                    }
                    else {
                        $(sCtrl1).attr("onclick", "chkAward_Click('" + sCtrl1 + "')");
                        if ($(sCtrl).val() == "1") {
                            $(sCtrl1).attr("checked", "true");
                        }
                        $(sCtrl).hide();
                    }
                }
            }
        }

        function chkAward_Click(AwardID) {
            var sCtrl;
            sCtrl = AwardID.replace("chkAward", "txtAward");
            if ($(AwardID).attr("checked") == true) {
                $(sCtrl).val("1");
            }
            else {
                $(sCtrl).val("0");
            }
        }

        function ValidateBeforeSave() {
            var SaveOK;
            var cnt;
            var sVal;

            SaveOK = true;
            // *** basis for awarding, check first items on the repeater controls ***
            cnt = 0;
            if ($("#Repeater_BFA_LowestPr_ctl00_chkBFA_LowestPr").length > 0) cnt++;
            if ($("#Repeater_BFA_SoleLOA_ctl00_chkBFA_SoleLOA").length > 0) cnt++;
            if ($("#Repeater_BFA_SoleCSD_ctl00_chkBFA_SoleCSD").length > 0) cnt++;
            if ($("#Repeater_BFA_TechSpec_ctl00_chkBFA_TechSpec").length > 0) cnt++;
            if ($("#Repeater_BFA_LeadTime_ctl00_chkBFA_LeadTime").length > 0) cnt++;
            if ($("#Repeater_BFAOth_ctl00_chkBFAOth").length > 0) cnt++;
            if (cnt == 0) {
                //ShowDialog("Please select at least one (1) <b>Basis for Awarding</b>.", "#BasisForAwardingAttchd");
                //SaveOK = false;
            }
            // *** executive summary ***
            sVal = $("#executive_summary").val();
            sVal = sVal.replace("<tbody>", "");
            sVal = sVal.replace("</tbody>", "");
            if (jQuery.trim(sVal) == "") {
                ShowDialog("Please provide <b>Executive Summary</b>.", "#executive_summary");
                SaveOK = false;
            }
            // *** payment terms ***
            sVal = $("#myPaymentTerms").html();
            sVal = sVal.replace("<tbody>", "");
            sVal = sVal.replace("</tbody>", "");
            if (jQuery.trim(sVal) == "") {
                ShowDialog("Please select at least one (1) <b>Payment Term</b>.", "#cbPaymentTerms");
                SaveOK = false;
            }
            // *** type of purchase ***
            sVal = $("#myTypeOfPurchase").html();
            sVal = sVal.replace("<tbody>", "");
            sVal = sVal.replace("</tbody>", "");
            if (jQuery.trim(sVal) == "") {
                ShowDialog("Please select at least one (1) <b>Type of Purchase</b>.", "#cbTypeOfPurchase");
                SaveOK = false;
            }
            // *** supply position ***
            sVal = $("#mySupplyPosition").html();
            sVal = sVal.replace("<tbody>", "");
            sVal = sVal.replace("</tbody>", "");
            if (jQuery.trim(sVal) == "") {
                ShowDialog("Please select at least one (1) <b>Supply Position</b>.", "#cbSupplyPosition");
                SaveOK = false;
            }
            // *** sourcing strategy ***
            sVal = $("#mySourcingStrategy").html();
            sVal = sVal.replace("<tbody>", "");
            sVal = sVal.replace("</tbody>", "");
            if (jQuery.trim(sVal) == "") {
                ShowDialog("Please select at least one (1) <b>Sourcing Strategy</b>.", "#cbSourcingStrategy");
                SaveOK = false;
            }
            // *** nature of savings ***
            sVal = $("#myNatureOfSavings").html();
            sVal = sVal.replace("<tbody>", "");
            sVal = sVal.replace("</tbody>", "");
            if (jQuery.trim(sVal) == "") {
                ShowDialog("Please select at least one (1) <b>Nature of Saving</b>.", "#cbNatureOfSavings");
                SaveOK = false;
            }
            // *** awarded to ***
            var sCtrl, sCtrl1;
            var iItemCount;

            // count total items first
            iItemCount = 0;
            for (var i = 0; i < 100; i++) {
                if (i < 10) {
                    sCtrl = "#lstCommDetails_ctl0" + i + "_rptQty_ctl00_txtQty";
                }
                else {
                    sCtrl = "#lstCommDetails_ctl" + i + "_rptQty_ctl00_txtQty";
                }
                if ($(sCtrl).length == 0) {
                    break;
                }
                else {
                    iItemCount++;
                }
            }
            // loop through items
            for (var i = 0; i < iItemCount; i++) {
                cnt = 0;
                for (var j = 0; j < 100; j++) {
                    if (i < 10) {
                        sCtrl = "#lstCommDetails_ctl0" + i;
                        sCtrl1 = "#lstCommDetails_ctl0" + i;
                    }
                    else {
                        sCtrl = "#lstCommDetails_ctl" + i;
                        sCtrl1 = "#lstCommDetails_ctl" + i;
                    }

                    if (j < 10) {
                        sCtrl = sCtrl + "_rptAward_ctl0" + j + "_txtAward";
                        sCtrl1 = sCtrl1 + "_rptAward_ctl0" + j + "_chkAward";
                    }
                    else {
                        sCtrl = sCtrl + "_rptAward_ctl" + j + "_txtAward";
                        sCtrl1 = sCtrl1 + "_rptAward_ctl" + j + "_chkAward";
                    }
                    if ($(sCtrl).length == 0) {
                        break;
                    }
                    else {
                        if ($(sCtrl1).attr("checked") == true) {
                            $(sCtrl).val("1");
                            cnt++;
                        }
                    }
                }
                if (cnt == 0) {
                    ShowDialog("Please select at least one (1) <b>Awarded To</b> on every commercial detail item.", "#cbNatureOfSavings");
                    SaveOK = false;
                    break;
                }
            }
            if (SaveOK == true) {
                $("#form1").submit();
            }
        }

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
        function CheckVendor() {
            var bVendorValueOK = CheckVendorValue();
            if (bVendorValueOK == true) {
                for (var i = 0; i < 100; i++) {
                    if (i < 10) {
                        sCtrl = "#Repeater4_ctl0" + i + "_txtVendorName";
                    }
                    else {
                        sCtrl = "#Repeater4_ctl" + i + "_txtVendorName";
                    }
                    if ($(sCtrl).length == 0) {
                        break;
                    }
                    else {
                        if ($(sCtrl).attr("Enabled").toString() == "true") {
                            __doPostBack("VendorChange", $(sCtrl).attr("value"));
                        }
                    }
                }
            }
        }

        function CheckVendorValue() {
            var bVendorValueOK = true;
            for (var i = 0; i < 100; i++) {
                if (i < 10) {
                    sCtrl = "#Repeater4_ctl0" + i + "_txtVendorName";
                }
                else {
                    sCtrl = "#Repeater4_ctl" + i + "_txtVendorName";
                }
                if ($(sCtrl).length == 0) {
                    break;
                }
                else {
                    if ($(sCtrl).val().toString() == "-- SELECT --") {
                        bVendorValueOK = false;
                    }
                }
            }
            if (bVendorValueOK == false) {
                ShowDialog("Please select vendor first.", "#form1");
            }
            return bVendorValueOK;
        }
        function btnAddCol_onclick() {
            var bVendorValueOK = CheckVendorValue();
            if (bVendorValueOK == true) {
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
        }
        function btnDelCol_onclick(iVendorID) {
            var bVendorValueOK = CheckVendorValue();
            if (bVendorValueOK == true) {
                var i;
                i = $("#BuyerBidForBac").val();
                if (i != -1) {
                    i = $("#txtNewVendor").val();
                    if (i > 0) {
                        i--;
                        $("#txtNewVendor").val(i);
                        __doPostBack("NewVendor", iVendorID);
                    }
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
            i++;
            $("#txtNewRow").val(i);
            __doPostBack("CriteriaAddRow", "");
        }

        function btnDelRow_onclick(trID) {
            var i;
            i = $("#txtNewRow").val();
            if (i >= 0) i--;
            $("#txtNewRow").val(i);
            __doPostBack("CriteriaAddRow", trID);
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

        function GetCriteria() {
            var sCtrl, sCtrl_1;
            var j, j1;
            var sID;

            ClearCriteria("ALL");
            for (j = 0; j < 100; j++) {
                sCtrl = "#txtR" + j;
                if ($(sCtrl).length == 0) {
                    break;
                }
                else {
                    createCookie("Criteria_" + j + "_", $(sCtrl).val());
                    for (j1 = 1; j1 < 100; j1++) {
                        if (j1 < 10) {
                            sCtrl1 = "#RepeaterC" + j + "_ctl0" + j1 + "_item";
                        }
                        else {
                            sCtrl1 = "#RepeaterC" + j + "_ctl" + j1 + "_item";
                        }
                        if ($(sCtrl1).length == 0) {
                            break;
                        }
                        else {
                            sID = $(sCtrl1).attr("VendorID");
                            createCookie("Criteria_" + j + "_" + sID, $(sCtrl1).val());
                        }
                    }
                }
            }
        }

        function SetCriteria() {
            var i, i1, nameEQ, ca, c;
            var iVal, iVal1, iVal2;
            var sCtrl, sCtrl_1;
            var j, j1;
            var sID;

            j = -1;
            for (i1 = 0; i1 < 100; i1++) {
                nameEQ = "Criteria_" + i1 + "_";
                if (readCookie(nameEQ) != null) {
                    j++;
                    ca = document.cookie.split(';');
                    for (i = 0; i < ca.length; i++) {
                        c = ca[i];
                        while (c.charAt(0) == ' ') c = c.substring(1, c.length);
                        if (c.indexOf(nameEQ) == 0) {
                            iVal = c;
                            iVal1 = iVal.split("=");
                            iVal2 = iVal1[0].split("_");
                            //while (j < 100) {
                            //    j++;
                            sCtrl = "#txtR" + j;
                            if ($(sCtrl).length == 0) {
                                break;
                            }
                            else {
                                $(sCtrl).val(readCookie("Criteria_" + i1 + "_"));
                            }

                            for (j1 = 1; j1 < 100; j1++) {
                                if (j1 < 10) {
                                    sCtrl1 = "#RepeaterC" + j + "_ctl0" + j1 + "_item";
                                }
                                else {
                                    sCtrl1 = "#RepeaterC" + j + "_ctl" + j1 + "_item";
                                }
                                if ($(sCtrl1).length == 0) {
                                    break;
                                }
                                else {
                                    sID = $(sCtrl1).attr("VendorID");
                                    $(sCtrl1).val(readCookie("Criteria_" + i1 + "_" + sID));
                                }
                            }
                            //}
                        }
                    }
                }
            }
        }

        function ClearCriteria(CriteriaRowID) {
            var i, nameEQ, ca, c;
            var iVal, iVal1;

            if (CriteriaRowID == "ALL") {
                nameEQ = "Criteria_";
                ca = document.cookie.split(';');
                for (i = 0; i < ca.length; i++) {
                    c = ca[i];
                    while (c.charAt(0) == ' ') c = c.substring(1, c.length);
                    if (c.indexOf(nameEQ) == 0) {
                        iVal = c;
                        iVal1 = iVal.split("=");
                        eraseCookie(iVal1[0]);
                    }
                }
            }
            else {
                row1 = CriteriaRowID.substring(3);
                nameEQ = "Criteria_" + row1 + "_";
                ca = document.cookie.split(';');
                for (i = 0; i < ca.length; i++) {
                    c = ca[i];
                    while (c.charAt(0) == ' ') c = c.substring(1, c.length);
                    if (c.indexOf(nameEQ) == 0) {
                        iVal = c;
                        iVal1 = iVal.split("=");
                        eraseCookie(iVal1[0]);
                    }
                }
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

        //        function cbPaymentTerms_SelectedIndexChanged() {
        //            var oPT = document.getElementById("cbPaymentTerms");
        //            if (oPT.value == -1) {
        //                $("#txtPaymentOthers").show();
        //            }
        //            else {
        //                $("#txtPaymentOthers").hide();
        //            }
        //        }

        //        function AddOtherPaymentTerms() {
        //            if ($("#txtNewPaymentTerms").val() == "") {
        //                ShowDialog("Please type new payment term.", "#txtNewPaymentTerms");
        //            }
        //            else {
        //                var sTxt = $("#txtNewPaymentTerms").val();
        //                AddTRCheckBox("#myPaymentTerms", "pt", sTxt, true);
        //            }
        //        }

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

        function cbPaymentTerms_SelectedIndexChanged() {
            var oPT = document.getElementById("cbPaymentTerms");
            if (oPT.value != -1) {
                var sTxt = oPT.options[oPT.selectedIndex].text;
                AddTRCheckBox("#myPaymentTerms", "pt", sTxt, true);
            }
        }

        function AddOtherPaymentTerms() {
            if ($("#txtNewPaymentTerms").val() == "") {
                ShowDialog("Please type new payment term.", "#txtNewPaymentTerms");
            }
            else {
                var sTxt = $("#txtNewPaymentTerms").val();
                AddTRCheckBox("#myPaymentTerms", "pt", sTxt, true);
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

        function ClearSavedCookies() {
            var i, nameEQ, ca, c;
            var iVal, iVal1, sTxt, sNewRow;

            // payment terms
            nameEQ = "mypt_";
            ca = document.cookie.split(';');
            for (i = 0; i < ca.length; i++) {
                c = ca[i];
                while (c.charAt(0) == ' ') c = c.substring(1, c.length);
                if (c.indexOf(nameEQ) == 0) {
                    iVal = c;
                    iVal1 = iVal.split("=");
                    eraseCookie(iVal1[0]);
                }
            }
            // sourcing strategy
            nameEQ = "myss_";
            ca = document.cookie.split(';');
            for (i = 0; i < ca.length; i++) {
                c = ca[i];
                while (c.charAt(0) == ' ') c = c.substring(1, c.length);
                if (c.indexOf(nameEQ) == 0) {
                    iVal = c;
                    iVal1 = iVal.split("=");
                    eraseCookie(iVal1[0]);
                }
            }
            // type of purchase
            nameEQ = "mytop_";
            ca = document.cookie.split(';');
            for (i = 0; i < ca.length; i++) {
                c = ca[i];
                while (c.charAt(0) == ' ') c = c.substring(1, c.length);
                if (c.indexOf(nameEQ) == 0) {
                    iVal = c;
                    iVal1 = iVal.split("=");
                    eraseCookie(iVal1[0]);
                }
            }
            // supply position
            nameEQ = "mysp_";
            ca = document.cookie.split(';');
            for (i = 0; i < ca.length; i++) {
                c = ca[i];
                while (c.charAt(0) == ' ') c = c.substring(1, c.length);
                if (c.indexOf(nameEQ) == 0) {
                    iVal = c;
                    iVal1 = iVal.split("=");
                    eraseCookie(iVal1[0]);
                }
            }
            // basis for awarding
            nameEQ = "mybfa_";
            ca = document.cookie.split(';');
            for (i = 0; i < ca.length; i++) {
                c = ca[i];
                while (c.charAt(0) == ' ') c = c.substring(1, c.length);
                if (c.indexOf(nameEQ) == 0) {
                    iVal = c;
                    iVal1 = iVal.split("=");
                    eraseCookie(iVal1[0]);
                }
            }
            // nature of savings
            nameEQ = "mynos_";
            ca = document.cookie.split(';');
            for (i = 0; i < ca.length; i++) {
                c = ca[i];
                while (c.charAt(0) == ' ') c = c.substring(1, c.length);
                if (c.indexOf(nameEQ) == 0) {
                    iVal = c;
                    iVal1 = iVal.split("=");
                    eraseCookie(iVal1[0]);
                }
            }
            // CRC
            nameEQ = "mycrc_";
            ca = document.cookie.split(';');
            for (i = 0; i < ca.length; i++) {
                c = ca[i];
                while (c.charAt(0) == ' ') c = c.substring(1, c.length);
                if (c.indexOf(nameEQ) == 0) {
                    iVal = c;
                    iVal1 = iVal.split("=");
                    eraseCookie(iVal1[0]);
                }
            }
            // PO
            nameEQ = "mypo1_";
            ca = document.cookie.split(';');
            for (i = 0; i < ca.length; i++) {
                c = ca[i];
                while (c.charAt(0) == ' ') c = c.substring(1, c.length);
                if (c.indexOf(nameEQ) == 0) {
                    iVal = c;
                    iVal1 = iVal.split("=");
                    eraseCookie(iVal1[0]);
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
        .style4
        {
            height: 32px;
        }
        .style5
        {
            height: 33px;
        }
        .style6
        {
        }
        .style7
        {
            width: 50%;
            height: 26px;
        }
    </style>
</head>
<body id="Body1" runat="server">
    <div>
        <form enctype="multipart/form-data" id="form1" name="form1" runat="server" onsubmit="GetCriteria();">&nbsp;
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
                                        <EBid:LeftNav ID="LeftNav" runat="server" />
                                    </div>
                                    <p>&nbsp;</p>
                                </td>
                                <td id="content">
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                        <tr>
                                            <td valign="top">
												<h1><br />Bid Awarding Checklist</h1>
                                                <script type="text/javascript">
                                                    $(document).ready(function () {
                                                        $('#relatedInfo').hide();
                                                    });
                                                 </script>
												<p><a href="javascript:void(0)" onclick="$('#relatedInfo').toggle()">Show/Hide Left Panel</a><asp:Label ID="lblMessage" runat="server" Font-Size="11px" ForeColor="red"></asp:Label></p>
                                                <table cellspacing="5" style="width:100%;">
                                                    <tr>
                                                        <td colspan="2" valign="top">
															<table width="50%" border="1" cellpadding="0" cellspacing="0" rules="all" class="itemDetails" id="Biddetails_details1_dvEventDetails" style="border-color:#71A9D2; font-family: Arial; font-size: 11px; border-collapse: collapse; background-color: #F4F1C4; height:100%">
																<tbody>
																	<tr>
																		<td colspan="4" class="ui-widget-header" style="height:26px; vertical-align:middle;">BID DETAILS</td>
																	</tr>
																	<tr valign="middle" >
																		<td colspan="2" align="center" valign="middle" 
                                                                            style="padding:5px; font-size:12px; " class="style7">Bid Reference No. </td>
																		<td colspan="2" align="center" valign="middle" 
                                                                            style="padding:5px; font-size:12px; " class="style7">Bid Events Description: </td>
																	</tr>
																	<tr valign="middle" >
																		<td colspan="2" align="center" valign="middle" bgcolor="#FFFFFF" style="padding:15px 5px; font-size:12px; width:50%">
																			<div id="fromDS">
																				<asp:dropdownlist ID="BuyerBidForBac" runat="server"
																					DataSourceID="dsBuyerBidForBac"
																					DataTextField="BidRefNoItemDesc"
																					DataValueField="BidRefNo" AutoPostBack="True"
																					OnDataBound="BuyerBidForBac_DataBound" onload="Page_LoadComplete" Font-Bold="True" >
																				</asp:dropdownlist>

																				<asp:SqlDataSource ID="dsBuyerBidForBac" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>'
																					SelectCommand="SELECT t1.BidRefNo,t1.ItemDesc, Convert(varchar, t1.BidRefNo) + ': ' + SUBSTRING(t1.ItemDesc,1,25)+'...' AS BidRefNoItemDesc,
																						t1.BidRefNo AS BidDetailNo FROM tblBidItems t1 WHERE t1.Status = @Status AND t1.ForAuction = 0 AND t1.BuyerId = @BuyerId
																						and not exists (select 1 from  tblBacBidItems t2 where t1.BidRefNo = t2.BidRefNo) ORDER BY t1.DateCreated DESC" >
																					<SelectParameters>
																						<asp:SessionParameter Name="BuyerId" SessionField="UserId" Type="Int32" />
																						<asp:Parameter Name="Status" DefaultValue="4" Type="Int32" />
																					</SelectParameters>
																				</asp:SqlDataSource>
																			</div>
																			<div id="fromNew">
																				<asp:Label ID="lblNew" runat="server" Text="New Bid Reference No.:"></asp:Label>
                                                                                <asp:TextBox ID="txtNew" runat="server" Enabled="False"></asp:TextBox>
																			</div>

																			<input type="hidden" id="lblTotalAll" name="lblTotalAll" runat="server" />
                                                                            <input type="hidden" id="lblLoad" name="lblLoad" runat="server" />
																		</td>
																		<td colspan="2" align="center" valign="middle" bgcolor="#FFFFFF" style="padding:15px 5px; font-size:12px; width:50%">
																			<asp:TextBox ID="ItemDescLbl" runat="server" Width="350" Font-Bold="True"></asp:TextBox>
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
																			<asp:TextBox ID="PRRefNoLbl" runat="server" Width="125px" 
                                                                                style="text-align:center" Height="20px" Font-Bold="True"></asp:TextBox>
																		</td>
																		<td align="center" valign="middle" bgcolor="#FFFFFF" style="width:25%;">
																			<asp:TextBox ID="EstItemValueLbl" runat="server" Text="0" Width="125px" 
                                                                                style="text-align:right" Font-Bold="True"></asp:TextBox>
																		</td>
																		<td align="center" valign="middle" bgcolor="#FFFFFF" style="width:25%;">
																			<asp:TextBox ID="PRDate" runat="server" Width="125px" Font-Bold="True"></asp:TextBox>
																		</td>
																		<td align="center" valign="middle" bgcolor="#FFFFFF" style="width:25%;">
																			<asp:TextBox ID="DateApprovedLbl" runat="server" Width="125px" Font-Bold="True"></asp:TextBox>
																		</td>
																	</tr>
																	<tr valign="middle">
																		<td colspan="2" align="center" valign="middle" style="width:25%;">
																			<table width="100%" border="0" cellspacing="0" cellpadding="0">
																				<tr>
																					<td><input type="radio" runat="server" id="Budgeted" name="Budgeted" value="1" checked="true" />
																						<label for="Budgeted">Budgeted</label>
																					</td>
																					<td>
																						<input type="radio" runat="server" id="UnBudgeted" name="Budgeted" value="0" />
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
																						<input name="CompanyId"  id="CompanyIdEGG" runat="server" value="3" type="radio" />
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
																	<asp:ControlParameter Name="BidRefNo" ControlID="BuyerBidForBac" PropertyName="SelectedValue" Type="Int32" />
																	<asp:Parameter Name="Status" DefaultValue="4" Type="Int32" />
																</SelectParameters>
															</asp:SqlDataSource>
														</td>
													</tr>
                                                    
                                                    <tr>
                                                        <td valign="top">
															
                                                            <table width="50%" border="1" cellpadding="0" cellspacing="0" rules="all" class="itemDetails" id="Table3" style="border-color:#71A9D2; font-family: Arial; font-size: 11px; border-collapse: collapse; background-color: #F4F1C4; width:99%;">
																<tbody>
																	<tr>
																		<td class="ui-widget-header" style="height:26px; vertical-align:middle;">SUPPLY POSITION</td>
																	</tr>
																	<tr valign="middle">
																		<td align="center" valign="middle" style="width:25%; border:none;">
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
																						<table id="mySupplyPosition" style="font-weight:bold"></table>
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
																					<td width="60%" style="border:none">
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
																	<tr><td>
																		<table id="mySourcingStrategy" runat="server" style="font-weight:bold"></table>
																	</td></tr>
																						</table>
																					</td>
																					<td style="border:none">
																						<table>
																							<tr>
                                                                                                <td>
																								    <label for="ss_nobidders">No. of Invited Bidders: </label>
                                                                                                </td>
                                                                                                <td>
																								    <input name="ss_nobidders" type="text" id="ss_nobidders" size="6" value="0" runat="server"  style="text-align:right; font-weight:bold" />
																							    </td>
                                                                                            </tr>
																							<tr>
                                                                                                <td>
																								    <label for="ss_nobidsrcvd">No. of Bids Received: </label>
                                                                                                </td>
                                                                                                <td>
																								    <input name="ss_nobidsrcvd" type="text" id="ss_nobidsrcvd" size="6" value="0" runat="server"  style="text-align:right; font-weight:bold" />
																							    </td>
                                                                                            </tr>
																							<tr>
                                                                                                <td class="style5">
																								    <label for="ss_techcompliance">Tech. Compliance: </label>
                                                                                                </td>
																								<td>
                                                                                                    <input name="ss_techcompliance" type="text" id="ss_techcompliance" size="6" value="0" runat="server"  style="text-align:right; font-weight:bold" />
																							    </td>
                                                                                            </tr>
																							<tr>
                                                                                                <td class="style5">
																								    <label for="ss_crcnumber">CRC No: </label>
                                                                                                </td>
																								<td>
                                                                                                    <input name="ss_crcnumber" type="text" id="ss_crcnumber" size="12" runat="server"  style="text-align:left; font-weight:bold" />
																							    </td>
                                                                                            </tr>
																						</table>
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
															<table width="60%" border="1" cellpadding="0" cellspacing="0" rules="all" class="itemDetails" id="Biddetails_details1_dvEventDetails6" style="border-color:#71A9D2; font-family: Arial; font-size: 11px; border-collapse: collapse; background-color: #F4F1C4; width:99%; ">
																<tbody>
																	<tr>
																		<td class="ui-widget-header" style="height:26px; vertical-align:middle;">TYPE OF PURCHASE</td>
																	</tr>
																	<tr valign="middle">
																		<td align="center" valign="middle" style="width:25%; border:none;">
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
																						<table id="myTypeOfPurchase" style="font-weight:bold"></table>
																					</td>
																				</tr>
																			</table>
																		</td>
																	</tr>
																</tbody>
															</table>
														</td>
                                                        <td valign="top">
															<table width="50%" border="1" cellpadding="0" cellspacing="0" rules="all" class="itemDetails" id="Biddetails_details1_dvEventDetails4" style="border-color:#71A9D2; font-family: Arial; font-size: 11px; border-collapse: collapse; background-color: #F4F1C4; width:100%;">
																<tbody>
																	<tr>
																		<td class="ui-widget-header" style="height:26px; vertical-align:middle;">PAYMENT TERMS</td>
																	</tr>
																	<tr valign="middle">
																		<td align="center" valign="middle" style="width:25%; border:none;">
																			<table width="100%" border="0" cellspacing="0" cellpadding="0">
																				<tr>
																					<td width="50%">
																						<table width="100%">
																							<tr><td style="border:none">
																								<asp:DropDownList ID="cbPaymentTerms" runat="server"
																									DataSourceID="dsPaymentTerms" DataTextField="PaymentTerm" DataValueField="ID"
																									ondatabound="cbPaymentTerms_DataBound">
																								</asp:DropDownList>
																								<asp:SqlDataSource ID="dsPaymentTerms" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>'
																									SelectCommand="SELECT * FROM rfcPaymentTerms ORDER BY ID" >
																								</asp:SqlDataSource>
																							</td>
                                                                                            </tr>
																						    <tr>
                                                                                                <td>
																								<asp:TextBox ID="txtNewPaymentTerms" runat="server" Width="150"></asp:TextBox>
																								<input id="btnTOP0" type="button" value="Others" 
                                                                                                        onclick="AddOtherPaymentTerms();" />
																							</td>
                                                                                            </tr>
																						    <tr>
                                                                                                <td style="border:none;">
                                                                                                <table id="myPaymentTerms"  style="font-weight:bold;"></table>
                                                                                                </td>
                                                                                            </tr>
																						</table>
																					</td>
																					<td>
																						Description:<br />
                                                                                                <asp:TextBox ID="descPaymentTerms" runat="server" Rows="3" TextMode="MultiLine" Font-Names="Arial" style="width:192px; font-weight:bold" ></asp:TextBox>
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
   <table border="1" cellpadding="0" cellspacing="0" rules="all" class="itemDetails" 
           id="Biddetails_details1_dvEventDetails2" 
           style="border-color:#71A9D2; font-family: Arial; font-size: 11px; border-collapse: collapse; background-color: #F4F1C4; width:100%;" 
           width="100%">
   <tbody>
      <tr>
                                                              <td class="ui-widget-header" style="height:26px; vertical-align:middle;">SUPPORTING DOCUMENTS ATTACHED</td>
                                                            </tr>
                                                            <tr valign="middle" >
                                                              <td align="center" valign="middle" style="padding:5px; font-size:12px; width:50%"><table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                                  <tr>
                                                                    <td width="99%" align="right">
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
                                                                    <td width="99%">
                                                                        <table style="width: 100%;">
                                                                            <tr>
                                                                                <td style="border:none">
                                                                                    <strong><a id="btcollapseAPR" href="javascript:void(0)" 
                                                                                        onclick="$('#collapseAPR').toggle(); showhideBT();" 
                                                                                        style="color:Black;" >- Approved SAP PR</a></strong>
                                                                                </td>
                                                                                <td width="15" style="border:none">
                                                                                    <b>(<asp:Label ID="SDA_APR_count" runat="server" ></asp:Label>)</b>
                                                                                </td>
                                                                            </tr>
                                                                            </table>
                                                                        <div id="collapseAPR">
                                                                            <asp:Repeater ID="Repeater_SDA_APR" runat="server" DataSourceID="dsSDA_APR" OnItemDataBound="Repeater_SDA_APR_ItemDataBound">
                                                                                <ItemTemplate>
                                                                                <div style="clear:both">
                                                                                    <input id="chkSDA_APR" name="chkSDA_APR" type="checkbox" runat="server" checked="true" style="float:left; " />
                                                                                    <a id="lnkSDA_APR" href="bidawardingchecklist.aspx?ShowAttachment=<%# Eval("FileUploadID") %>"
                                                                                        target="_blank" title='<%# Eval("ActualFileName") %>'>
                                                                                        <%# Eval("OriginalFileName") %></a></div>
                                                                                </ItemTemplate>
                                                                            </asp:Repeater>
                                                                        </div >
                                                                        <asp:SqlDataSource ID="dsSDA_APR" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>'
                                                                            SelectCommand="SELECT * FROM tblBACSupportingDocuments WHERE BuyerID=@BuyerID AND BidRefNo=@BidRefNo AND DocuName='Approved_PR' ORDER BY FileUploadID" 
                                                                            onload="dsSDA_APR_Load" >
                                                                            <SelectParameters>
                                                                                <asp:SessionParameter Name="BuyerId" SessionField="UserId" Type="Int32" />
                                                                                <asp:ControlParameter Name="BidRefNo" ControlID="BuyerBidForBac" PropertyName="SelectedValue" Type="Int32" />
                                                                            </SelectParameters>
                                                                        </asp:SqlDataSource>
                                                                        <asp:SqlDataSource ID="dsSDA_APR_count" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>'
                                                                            SelectCommand="SELECT Count(*) as Count FROM tblBACSupportingDocuments WHERE BuyerID=@BuyerID AND BidRefNo=@BidRefNo AND DocuName='Approved_PR'" >
                                                                            <SelectParameters>
                                                                                <asp:SessionParameter Name="BuyerId" SessionField="UserId" Type="Int32" />
                                                                                <asp:ControlParameter Name="BidRefNo" ControlID="BuyerBidForBac" PropertyName="SelectedValue" Type="Int32" />
                                                                            </SelectParameters>
                                                                        </asp:SqlDataSource>
                                                                    </td>
                                                                </tr>
                                                                
                                                                <tr>
                                                                    <td>
                                                                    <table style="width: 100%;">
                                                                            <tr>
                                                                                <td style="border:none">
                                                                                   <strong><a id="btcollapseABC" href="javascript:void(0)" onclick="$('#collapseABC').toggle(); showhideBT();" style="color:Black" >- Approved Business Case</a></strong>
                                                                                </td>
                                                                                <td width="15" style="border:none">
                                                                                    <b>(<asp:Label ID="SDA_ABC_count" runat="server"></asp:Label>)</b>
                                                                                </td>
                                                                            </tr>
                                                                            </table>
                                                                        <div id="collapseABC">
                                                                            <asp:Repeater ID="Repeater_SDA_ABC" runat="server" DataSourceID="dsSDA_ABC" OnItemDataBound="Repeater_SDA_ABC_ItemDataBound">
                                                                                <ItemTemplate>
                                                                                <div style="clear:both">
                                                                                    <input id="chkSDA_ABC" name="chkSDA_ABC" type="checkbox" runat="server" checked="true" style="float:left" />
                                                                                    <a href="../fileattachments/<% =Session["UserId"] %>/BAC/<%# Eval("ActualFileName") %>"
                                                                                        target="_blank" title='<%# Eval("ActualFileName") %>'>
                                                                                        <%# Eval("OriginalFileName") %></a></div>
                                                                                </ItemTemplate>
                                                                            </asp:Repeater>
                                                                        </div>
                                                                        <asp:SqlDataSource ID="dsSDA_ABC" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>' 
                                                                            
                                                                            SelectCommand="SELECT * FROM tblBACSupportingDocuments WHERE BuyerID=@BuyerID AND BidRefNo=@BidRefNo AND DocuName='Approved_Business_Case' ORDER BY FileUploadID" 
                                                                            onload="dsSDA_ABC_Load" >
                                                                            <SelectParameters>
                                                                                <asp:SessionParameter Name="BuyerId" SessionField="UserId" Type="Int32" />
                                                                                <asp:ControlParameter Name="BidRefNo" ControlID="BuyerBidForBac" PropertyName="SelectedValue" Type="Int32" />
                                                                            </SelectParameters>
                                                                        </asp:SqlDataSource>
                                                                    </td>
                                                                </tr>

                                                                  <tr>
                                                                    <td>
                                                                    <table style="width: 100%;">
                                                                            <tr>
                                                                                <td style="border:none">
                                                                                  <strong><a id="btcollapseBR" href="javascript:void(0)" onclick="$('#collapseBR').toggle(); showhideBT();" style="color:Black" >- Board Resolution</a> </strong> 
                                                                                </td>
                                                                                <td width="15" style="border:none">
                                                                                    <b>(<asp:Label ID="SDA_BR_count" runat="server"></asp:Label>)</b>
                                                                                </td>
                                                                            </tr>
                                                                            </table>
                                                                        <div id="collapseBR">
                                                                            <asp:Repeater ID="Repeater_SDA_BR" runat="server" DataSourceID="dsSDA_BR" OnItemDataBound="Repeater_SDA_BR_ItemDataBound">
                                                                                <ItemTemplate>
                                                                                <div style="clear:both">
                                                                                    <input id="chkSDA_BR" name="chkSDA_BR" type="checkbox" runat="server" checked="true" style="float:left" />
                                                                                    <a href="../fileattachments/<% =Session["UserId"] %>/BAC/<%# Eval("ActualFileName") %>"
                                                                                        target="_blank" title='<%# Eval("ActualFileName") %>'>
                                                                                        <%# Eval("OriginalFileName") %></a></div>
                                                                                </ItemTemplate>
                                                                            </asp:Repeater>
                                                                        </div>
                                                                        <asp:SqlDataSource ID="dsSDA_BR" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>' 
                                                                            
                                                                            SelectCommand="SELECT * FROM tblBACSupportingDocuments WHERE BuyerID=@BuyerID AND BidRefNo=@BidRefNo AND DocuName='Board_Resolution' ORDER BY FileUploadID" 
                                                                            onload="dsSDA_BR_Load" >
                                                                            <SelectParameters>
                                                                                <asp:SessionParameter Name="BuyerId" SessionField="UserId" Type="Int32" />
                                                                                <asp:ControlParameter Name="BidRefNo" ControlID="BuyerBidForBac" PropertyName="SelectedValue" Type="Int32" />
                                                                            </SelectParameters>
                                                                        </asp:SqlDataSource>
                                                                    </td>
                                                                </tr>


                                                                <tr>
                                                                    <td width="50%">
                                                                    <table style="width: 100%;">
                                                                            <tr>
                                                                                <td style="border:none">
                                                                                    <strong>
                                                                        <a id="btcollapseCE" href="javascript:void(0)" onclick="$('#collapseCE').toggle(); showhideBT();" style="color:Black" >- Commercial Evaluation</a></strong>
                                                                                </td>
                                                                                <td width="15" style="border:none">
                                                                                    <b>(<asp:Label ID="SDA_CE_count" runat="server"></asp:Label>)</b>
                                                                                </td>
                                                                            </tr>
                                                                            </table>
                                                                        <div id="collapseCE">
                                                                            <asp:Repeater ID="Repeater_SDA_CE" runat="server" DataSourceID="dsSDA_CE" OnItemDataBound="Repeater_SDA_CE_ItemDataBound">
                                                                                <ItemTemplate>
                                                                                <div style="clear:both">
                                                                                    <input id="chkSDA_CE" name="chkSDA_CE" type="checkbox" runat="server" checked="true" style="float:left" />
                                                                                    <a href="../fileattachments/<% =Session["UserId"] %>/BAC/<%# Eval("ActualFileName") %>"
                                                                                        target="_blank" title='<%# Eval("ActualFileName") %>'>
                                                                                        <%# Eval("OriginalFileName") %></a></div>
                                                                                </ItemTemplate>
                                                                            </asp:Repeater>
                                                                        </div>
                                                                        <asp:SqlDataSource ID="dsSDA_CE" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>' 
                                                                            
                                                                            SelectCommand="SELECT * FROM tblBACSupportingDocuments WHERE BuyerID=@BuyerID AND BidRefNo=@BidRefNo AND DocuName='Commercial_Evaluation' ORDER BY FileUploadID" 
                                                                            onload="dsSDA_CE_Load" >
                                                                            <SelectParameters>
                                                                                <asp:SessionParameter Name="BuyerId" SessionField="UserId" Type="Int32" />
                                                                                <asp:ControlParameter Name="BidRefNo" ControlID="BuyerBidForBac" PropertyName="SelectedValue" Type="Int32" />
                                                                            </SelectParameters>
                                                                        </asp:SqlDataSource>
                                                                    </td>
                                                                  </tr>


                                                                <tr>
                                                                    <td>
                                                                    <table style="width: 100%;">
                                                                            <tr>
                                                                                <td style="border:none">
                                                                                   <strong><a id="btcollapseNR" href="javascript:void(0)" onclick="$('#collapseNR').toggle(); showhideBT();" style="color:Black" >- Negotiation Results</a></strong>
                                                                                </td>
                                                                                <td width="15" style="border:none">
                                                                                    <b>(<asp:Label ID="SDA_NR_count" runat="server"></asp:Label>)</b>
                                                                                </td>
                                                                            </tr>
                                                                            </table>
                                                                        <div id="collapseNR">
                                                                            <asp:Repeater ID="Repeater_SDA_NR" runat="server" DataSourceID="dsSDA_NR" OnItemDataBound="Repeater_SDA_NR_ItemDataBound">
                                                                                <ItemTemplate>
                                                                                <div style="clear:both">
                                                                                    <input id="chkSDA_NR" name="chkSDA_NR" type="checkbox" runat="server" checked="true" style="float:left" />
                                                                                    <a href="../fileattachments/<% =Session["UserId"] %>/BAC/<%# Eval("ActualFileName") %>"
                                                                                        target="_blank" title='<%# Eval("ActualFileName") %>'>
                                                                                        <%# Eval("OriginalFileName") %></a></div>
                                                                                </ItemTemplate>
                                                                            </asp:Repeater>
                                                                        </div>
                                                                        <asp:SqlDataSource ID="dsSDA_NR" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>' 
                                                                            
                                                                            SelectCommand="SELECT * FROM tblBACSupportingDocuments WHERE BuyerID=@BuyerID AND BidRefNo=@BidRefNo AND DocuName='Negotiation_Results' ORDER BY FileUploadID" 
                                                                            onload="dsSDA_NR_Load" >
                                                                            <SelectParameters>
                                                                                <asp:SessionParameter Name="BuyerId" SessionField="UserId" Type="Int32" />
                                                                                <asp:ControlParameter Name="BidRefNo" ControlID="BuyerBidForBac" PropertyName="SelectedValue" Type="Int32" />
                                                                            </SelectParameters>
                                                                        </asp:SqlDataSource>
                                                                    </td>
                                                                  </tr>

                                                                
                                                                  <tr>
                                                                    <td>
                                                                    <table style="width: 100%;">
                                                                            <tr>
                                                                                <td style="border:none">
                                                                                   <strong><a id="btcollapseTE" href="javascript:void(0)" onclick="$('#collapseTE').toggle(); showhideBT();" style="color:Black" >- Technical Evaluation</a></strong>
                                                                                </td>
                                                                                <td width="15" style="border:none">
                                                                                    <b>(<asp:Label ID="SDA_TE_count" runat="server"></asp:Label>)</b>
                                                                                </td>
                                                                            </tr>
                                                                            </table>
                                                                        <div id="collapseTE">
                                                                            <asp:Repeater ID="Repeater_SDA_TE" runat="server" DataSourceID="dsSDA_TE" OnItemDataBound="Repeater_SDA_TE_ItemDataBound">
                                                                                <ItemTemplate>
                                                                                <div style="clear:both">
                                                                                    <input id="chkSDA_TE" name="chkSDA_TE" type="checkbox" runat="server" checked="true" style="float:left" />
                                                                                    <a href="../fileattachments/<% =Session["UserId"] %>/BAC/<%# Eval("ActualFileName") %>"
                                                                                        target="_blank" title='<%# Eval("ActualFileName") %>'>
                                                                                        <%# Eval("OriginalFileName") %></a><div>
                                                                                </ItemTemplate>
                                                                            </asp:Repeater>
                                                                        </div>
                                                                        <asp:SqlDataSource ID="dsSDA_TE" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>' 
                                                                            
                                                                            SelectCommand="SELECT * FROM tblBACSupportingDocuments WHERE BuyerID=@BuyerID AND BidRefNo=@BidRefNo AND DocuName='Technical_Evaluation' ORDER BY FileUploadID" 
                                                                            onload="dsSDA_TE_Load" >
                                                                            <SelectParameters>
                                                                                <asp:SessionParameter Name="BuyerId" SessionField="UserId" Type="Int32" />
                                                                                <asp:ControlParameter Name="BidRefNo" ControlID="BuyerBidForBac" PropertyName="SelectedValue" Type="Int32" />
                                                                            </SelectParameters>
                                                                        </asp:SqlDataSource>
                                                                    </td>
                                                                    <td>&nbsp;</td>
                                                                </tr>

                                                                <tr>
                                                                    <td>
                                                                    <table style="width: 100%;">
                                                                            <tr>
                                                                                <td style="border:none">
                                                                                   <strong><a id="btcollapseOth" href="javascript:void(0)" onclick="$('#collapseOth').toggle(); $('#collapseOth').is(':visible')?$(this).html('- Others'):$(this).html('+ Others')" style="color:Black" >- Others</a></strong>
                                                                                </td>
                                                                                <td width="15" style="border:none">
                                                                                    <b>(<asp:Label ID="SDA_Oth_count" runat="server"></asp:Label>)</b>
                                                                                </td>
                                                                            </tr>
                                                                            </table>
                                                                        <div id="collapseOth">
                                                                            <asp:Repeater ID="Repeater_SDA_Oth" runat="server" DataSourceID="dsSDA_Oth" OnItemDataBound="Repeater_SDA_Oth_ItemDataBound">
                                                                                <ItemTemplate>
                                                                                <div style="clear:both">
                                                                                    <input id="chkSDA_Oth" name="chkSDA_Oth" type="checkbox" runat="server" checked="true" style="float:left" />
                                                                                    <a href="../fileattachments/<% =Session["UserId"] %>/BAC/<%# Eval("ActualFileName") %>"
                                                                                        target="_blank" title='<%# Eval("ActualFileName") %>'>
                                                                                        <%# Eval("OriginalFileName") %></a></div>
                                                                                </ItemTemplate>
                                                                            </asp:Repeater>
                                                                        </div>
                                                                        <asp:SqlDataSource ID="dsSDA_Oth" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>' 
                                                                            
                                                                            SelectCommand="SELECT * FROM tblBACSupportingDocuments WHERE BuyerID=@BuyerID AND BidRefNo=@BidRefNo AND DocuName='Others' ORDER BY FileUploadID" 
                                                                            onload="dsSDA_Oth_Load" >
                                                                            <SelectParameters>
                                                                                <asp:SessionParameter Name="BuyerId" SessionField="UserId" Type="Int32" />
                                                                                <asp:ControlParameter Name="BidRefNo" ControlID="BuyerBidForBac" PropertyName="SelectedValue" Type="Int32" />
                                                                            </SelectParameters>
                                                                        </asp:SqlDataSource>
                                                                    </td>
                                                                  </tr>

                                                                <tr>
                                                                  <td style="border-bottom:none" colspan="2">
                                                                      <table style="width:100%;">
                                                                          <tr>
                                                                              <td class="style6" style="border:none;">
                                                                      <asp:DropDownList ID="SupportDocuAttached" runat="server">
                                                                          <asp:ListItem Value="Approved_PR">Approved SAP PR</asp:ListItem>
                                                                          <asp:ListItem Value="Approved_Business_Case">Approved Business Case</asp:ListItem>
                                                                          <asp:ListItem Value="Board_Resolution">Board Resolution</asp:ListItem>
                                                                          <asp:ListItem Value="Technical_Evaluation">Technical Evaluation</asp:ListItem>
                                                                          <asp:ListItem Value="Commercial_Evaluation">Commercial Evaluation</asp:ListItem>
                                                                          <asp:ListItem Value="Negotiation_Results">Negotiation Results</asp:ListItem>
                                                                          <asp:ListItem Value="Others">Others</asp:ListItem>
                                                                      </asp:DropDownList><br />
                                                                      <asp:FileUpload ID="fileUpload" runat="server" /><br />
                                                                    <asp:LinkButton ID="lnkAttach" runat="server" CausesValidation="false">Add To Attachments List</asp:LinkButton><br />
                                                                    <asp:Literal ID="addAttachmentMsg" runat="server"></asp:Literal>

                                                                                  </td>
                                                                          </tr>
                                                                      </table>
                                                                  </td>
                                                                </tr>
                                                                </table></td>
                                                            </tr>
                                                            <tr valign="middle">
                                                              <td valign="middle"></td>
                                                            </tr>
                                                          </tbody>
                                                        </table>
                                                      </td>
                                                      <td valign="top" width="50%">
                                                      <table width="100%" border="1" cellpadding="0" cellspacing="0" rules="all" class="itemDetails" id="Biddetails_details1_dvEventDetails3" style="border-color:#71A9D2; font-family: Arial; font-size: 11px; border-collapse: collapse; background-color: #F4F1C4; width:99%">
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
                                                                    <td class="style7">
                                                                    <table style="width: 100%;">
                                                                            <tr>
                                                                                <td style="border:none">
                                                                                  <strong><a id="btcollapseLowestPr" href="javascript:void(0)" onclick="$('#collapseLowestPr').toggle(); showhideBT2();" style="color:Black;">- Lowest-Price Bidder</a> </strong> 
                                                                                </td>
                                                                                <td width="15" style="border:none">
                                                                                    <b>(<asp:Label ID="bfa_LowestPr_count" runat="server"></asp:Label>)</b>
                                                                                </td>
                                                                            </tr>
                                                                            </table>
                                                                        
                                                                        <div id="collapseLowestPr">
                                                                            <asp:Repeater ID="Repeater_BFA_LowestPr" runat="server" DataSourceID="dsBFA_LowestPr" OnItemDataBound="Repeater_BFA_LowestPr_ItemDataBound">
                                                                                <ItemTemplate>
                                                                                <div style="clear:both">
                                                                                    <input id="chkBFA_LowestPr" name="chkBFA_LowestPr" type="checkbox" runat="server" checked="true" style="float:left" />
                                                                                    <a id="lnkBFA_LowestPr" href="bidawardingchecklist.aspx?ShowAttachment=<%# Eval("FileUploadID") %>"
                                                                                        target="_blank" title='<%# Eval("ActualFileName") %>'>
                                                                                        <%# Eval("OriginalFileName") %></a></div>
                                                                                </ItemTemplate>
                                                                            </asp:Repeater>
                                                                        </div>
                                                                        <asp:SqlDataSource ID="dsBFA_LowestPr" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>'
                                                                            SelectCommand="SELECT * FROM tblBACSupportingDocuments WHERE BuyerID=@BuyerID AND BidRefNo=@BidRefNo AND DocuName='bfa_LowestPr' ORDER BY FileUploadID" 
                                                                            onload="dsBFA_LowestPr_Load" >
                                                                            <SelectParameters>
                                                                                <asp:SessionParameter Name="BuyerId" SessionField="UserId" Type="Int32" />
                                                                                <asp:ControlParameter Name="BidRefNo" ControlID="BuyerBidForBac" PropertyName="SelectedValue" Type="Int32" />
                                                                            </SelectParameters>
                                                                        </asp:SqlDataSource>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td width="50%">
                                                                    <table style="width: 100%;">
                                                                            <tr>
                                                                                <td style="border:none">
                                                                                   <strong><a id="btcollapseSoleLOA" href="javascript:void(0)" onclick="$('#collapseSoleLOA').toggle(); showhideBT2();" style="color:Black;">- Sole Source w/ LOA Approved Waiver of Competition</a> </strong>
                                                                                </td>
                                                                                <td width="15" style="border:none">
                                                                                    <b>(<asp:Label ID="bfa_SoleLOA_count" runat="server"></asp:Label>)</b>
                                                                                </td>
                                                                            </tr>
                                                                            </table>
                                                                        
                                                                        <div id="collapseSoleLOA">
                                                                            <asp:Repeater ID="Repeater_BFA_SoleLOA" runat="server" DataSourceID="dsBFA_SoleLOA" OnItemDataBound="Repeater_BFA_SoleLOA_ItemDataBound">
                                                                                <ItemTemplate>
                                                                                <div style="clear:both">
                                                                                    <input id="chkBFA_SoleLOA" name="chkBFA_SoleLOA" type="checkbox" runat="server" checked="true" style="float:left" />
                                                                                    <a href="../fileattachments/<% =Session["UserId"] %>/BAC/<%# Eval("ActualFileName") %>"
                                                                                        target="_blank" title='<%# Eval("ActualFileName") %>'>
                                                                                        <%# Eval("OriginalFileName") %></a></div>
                                                                                </ItemTemplate>
                                                                            </asp:Repeater>
                                                                        </div>
                                                                        <asp:SqlDataSource ID="dsBFA_SoleLOA" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>' 
                                                                            
                                                                            SelectCommand="SELECT * FROM tblBACSupportingDocuments WHERE BuyerID=@BuyerID AND BidRefNo=@BidRefNo AND DocuName='bfa_SoleLOA' ORDER BY FileUploadID" 
                                                                            onload="dsBFA_SoleLOA_Load" >
                                                                            <SelectParameters>
                                                                                <asp:SessionParameter Name="BuyerId" SessionField="UserId" Type="Int32" />
                                                                                <asp:ControlParameter Name="BidRefNo" ControlID="BuyerBidForBac" PropertyName="SelectedValue" Type="Int32" />
                                                                            </SelectParameters>
                                                                        </asp:SqlDataSource>
                                                                    </td>
                                                                  </tr>
                                                                  <tr>
                                                                    <td class="style7">
                                                                    <table style="width: 100%;">
                                                                            <tr>
                                                                                <td style="border:none">
                                                                                   <strong><a id="btcollapseSoleCSD" href="javascript:void(0)" onclick="$('#collapseSoleCSD').toggle(); showhideBT2();" style="color:Black;">- Sole Source w/ Certificate of Sole Distributorship</a>  </strong>
                                                                                </td>
                                                                                <td width="15" style="border:none">
                                                                                    <b>(<asp:Label ID="bfa_SoleCSD_count" runat="server"></asp:Label>)</b>
                                                                                </td>
                                                                            </tr>
                                                                            </table>
                                                                        
                                                                        <div id="collapseSoleCSD">
                                                                            <asp:Repeater ID="Repeater_BFA_SoleCSD" runat="server" DataSourceID="dsBFA_SoleCSD" OnItemDataBound="Repeater_BFA_SoleCSD_ItemDataBound">
                                                                                <ItemTemplate>
                                                                                <div style="clear:both">
                                                                                    <input id="chkBFA_SoleCSD" name="chkBFA_SoleCSD" type="checkbox" runat="server" checked="true" style="float:left" />
                                                                                    <a href="../fileattachments/<% =Session["UserId"] %>/BAC/<%# Eval("ActualFileName") %>"
                                                                                        target="_blank" title='<%# Eval("ActualFileName") %>'>
                                                                                        <%# Eval("OriginalFileName") %></a></div>
                                                                                </ItemTemplate>
                                                                            </asp:Repeater>
                                                                        </div>
                                                                        <asp:SqlDataSource ID="dsBFA_SoleCSD" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>' 
                                                                            
                                                                            SelectCommand="SELECT * FROM tblBACSupportingDocuments WHERE BuyerID=@BuyerID AND BidRefNo=@BidRefNo AND DocuName='bfa_SoleCSD' ORDER BY FileUploadID" 
                                                                            onload="dsBFA_SoleCSD_Load" >
                                                                            <SelectParameters>
                                                                                <asp:SessionParameter Name="BuyerId" SessionField="UserId" Type="Int32" />
                                                                                <asp:ControlParameter Name="BidRefNo" ControlID="BuyerBidForBac" PropertyName="SelectedValue" Type="Int32" />
                                                                            </SelectParameters>
                                                                        </asp:SqlDataSource>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                    <table style="width: 100%;">
                                                                            <tr>
                                                                                <td style="border:none">
                                                                                   <strong><a id="btcollapseTechSpec" href="javascript:void(0)" onclick="$('#collapseTechSpec').toggle(); showhideBT2();" style="color:Black;" >- Compliance to Technical/ Functional Specifications</a> </strong>
                                                                                </td>
                                                                                <td width="15" style="border:none">
                                                                                    <b>(<asp:Label ID="bfa_TechSpec_count" runat="server"></asp:Label>)</b>
                                                                                </td>
                                                                            </tr>
                                                                            </table>
                                                                        
                                                                        <div id="collapseTechSpec">
                                                                            <asp:Repeater ID="Repeater_BFA_TechSpec" runat="server" DataSourceID="dsBFA_TechSpec" OnItemDataBound="Repeater_BFA_TechSpec_ItemDataBound">
                                                                                <ItemTemplate>
                                                                                <div style="clear:both">
                                                                                    <input id="chkBFA_TechSpec" name="chkBFA_TechSpec" type="checkbox" runat="server" checked="true" style="float:left" />
                                                                                    <a href="../fileattachments/<% =Session["UserId"] %>/BAC/<%# Eval("ActualFileName") %>"
                                                                                        target="_blank" title='<%# Eval("ActualFileName") %>'>
                                                                                        <%# Eval("OriginalFileName") %></a></div>
                                                                                </ItemTemplate>
                                                                            </asp:Repeater>
                                                                        </div>
                                                                        <asp:SqlDataSource ID="dsBFA_TechSpec" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>' 
                                                                            
                                                                            SelectCommand="SELECT * FROM tblBACSupportingDocuments WHERE BuyerID=@BuyerID AND BidRefNo=@BidRefNo AND DocuName='bfa_TechSpec' ORDER BY FileUploadID" 
                                                                            onload="dsBFA_TechSpec_Load" >
                                                                            <SelectParameters>
                                                                                <asp:SessionParameter Name="BuyerId" SessionField="UserId" Type="Int32" />
                                                                                <asp:ControlParameter Name="BidRefNo" ControlID="BuyerBidForBac" PropertyName="SelectedValue" Type="Int32" />
                                                                            </SelectParameters>
                                                                        </asp:SqlDataSource>
                                                                    </td>
                                                                  </tr>
                                                                  <tr>
                                                                    <td class="style7">
                                                                    <table style="width: 100%;">
                                                                            <tr>
                                                                                <td style="border:none">
                                                                                   <strong><a id="btcollapseLeadTime" href="javascript:void(0)" onclick="$('#collapseLeadTime').toggle(); showhideBT2();" style="color:Black;" >- Compliance to Delivery Lead-Time</a> </strong>
                                                                                </td>
                                                                                <td width="15" style="border:none">
                                                                                    <b>(<asp:Label ID="bfa_LeadTime_count" runat="server"></asp:Label>)</b>
                                                                                </td>
                                                                            </tr>
                                                                            </table>
                                                                        
                                                                        <div id="collapseLeadTime">
                                                                        <div style="clear:both">
                                                                            <asp:Repeater ID="Repeater_BFA_LeadTime" runat="server" DataSourceID="dsBFA_LeadTime" OnItemDataBound="Repeater_BFA_LeadTime_ItemDataBound">
                                                                                <ItemTemplate>
                                                                                    <input id="chkBFA_LeadTime" name="chkBFA_LeadTime" type="checkbox" runat="server" checked="true" style="float:left" />
                                                                                    <a href="../fileattachments/<% =Session["UserId"] %>/BAC/<%# Eval("ActualFileName") %>"
                                                                                        target="_blank" title='<%# Eval("ActualFileName") %>'>
                                                                                        <%# Eval("OriginalFileName") %></a></div>
                                                                                </ItemTemplate>
                                                                            </asp:Repeater>
                                                                        </div>
                                                                        <asp:SqlDataSource ID="dsBFA_LeadTime" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>' 
                                                                            
                                                                            SelectCommand="SELECT * FROM tblBACSupportingDocuments WHERE BuyerID=@BuyerID AND BidRefNo=@BidRefNo AND DocuName='bfa_LeadTime' ORDER BY FileUploadID" 
                                                                            onload="dsBFA_LeadTime_Load" >
                                                                            <SelectParameters>
                                                                                <asp:SessionParameter Name="BuyerId" SessionField="UserId" Type="Int32" />
                                                                                <asp:ControlParameter Name="BidRefNo" ControlID="BuyerBidForBac" PropertyName="SelectedValue" Type="Int32" />
                                                                            </SelectParameters>
                                                                        </asp:SqlDataSource>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                    <table style="width: 100%;">
                                                                            <tr>
                                                                                <td style="border:none">
                                                                                   <strong><a id="btcollapseBFAOth" href="javascript:void(0)" onclick="$('#collapseBFAOth').toggle(); showhideBT2();" style="color:Black;">- Others</a> </strong>
                                                                                </td>
                                                                                <td width="15" style="border:none">
                                                                                    <b>(<asp:Label ID="bfa_BFAOth_count" runat="server"></asp:Label>)</b>
                                                                                </td>
                                                                            </tr>
                                                                            </table>
                                                                        
                                                                        <div id="collapseBFAOth">
                                                                            <asp:Repeater ID="Repeater_BFAOth" runat="server" DataSourceID="dsBFAOth" OnItemDataBound="Repeater_BFAOth_ItemDataBound">
                                                                                <ItemTemplate>
                                                                                <div style="clear:both">
                                                                                    <input id="chkBFAOth" name="chkBFAOth" type="checkbox" runat="server" checked="true" style="float:left" />
                                                                                    <a href="../fileattachments/<% =Session["UserId"] %>/BAC/<%# Eval("ActualFileName") %>"
                                                                                        target="_blank" title='<%# Eval("ActualFileName") %>'>
                                                                                        <%# Eval("OriginalFileName") %></a></div>
                                                                                </ItemTemplate>
                                                                            </asp:Repeater>
                                                                        </div>
                                                                        <asp:SqlDataSource ID="dsBFAOth" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>' 
                                                                            
                                                                            SelectCommand="SELECT * FROM tblBACSupportingDocuments WHERE BuyerID=@BuyerID AND BidRefNo=@BidRefNo AND DocuName='bfa_BFAOth' ORDER BY FileUploadID" 
                                                                            onload="dsBFAOth_Load" >
                                                                            <SelectParameters>
                                                                                <asp:SessionParameter Name="BuyerId" SessionField="UserId" Type="Int32" />
                                                                                <asp:ControlParameter Name="BidRefNo" ControlID="BuyerBidForBac" PropertyName="SelectedValue" Type="Int32" />
                                                                            </SelectParameters>
                                                                        </asp:SqlDataSource>
                                                                    </td>
                                                                  </tr>
                                                                <tr>
                                                                  <td style="border-bottom:none" colspan="2">
                                                                      <table style="width:100%;">
                                                                          <tr>
                                                                              <td class="style6" style="border:none;">
                                                                      <asp:DropDownList ID="BasisForAwardingAttchd" runat="server">
                                                                          <asp:ListItem Value="bfa_LowestPr">Lowest-Price Bidder</asp:ListItem>
                                                                          <asp:ListItem Value="bfa_SoleLOA">Sole Source w/ LOA Approved Waiver of Competition</asp:ListItem>
                                                                          <asp:ListItem Value="bfa_SoleCSD">Sole Source w/ Certificate of Sole Distributorship</asp:ListItem>
                                                                          <asp:ListItem Value="bfa_TechSpec">Compliance to Technical/Functional Specifications</asp:ListItem>
                                                                          <asp:ListItem Value="bfa_LeadTime">Compliance to Delivery Lead-Time</asp:ListItem>
                                                                          <asp:ListItem Value="bfa_BFAOth">Others</asp:ListItem>
                                                                      </asp:DropDownList><br />
                                                                      <asp:FileUpload ID="fileUpload2" runat="server" /><br />
                                                                    <asp:LinkButton ID="lnkAttach2" runat="server" CausesValidation="false">Add To Attachments List</asp:LinkButton><br />
                                                                    <asp:Literal ID="addAttachmentMsg2" runat="server"></asp:Literal>
                                                                                  </td>
                                                                          </tr>
                                                                      </table>
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
														<td colspan="2" valign="top">
															<table width="100%" border="1" cellpadding="0" cellspacing="0" rules="all" class="itemDetails" id="Biddetails_details1_dvEventDetails10" style="border-color:#71A9D2; font-family: Arial; font-size: 11px; border-collapse: collapse; background-color: #F4F1C4; height:100%">
																<tbody>
																	<tr>
																		<td class="ui-widget-header" style="height:26px; vertical-align:middle;">
																			BID EVALUATION SUMMARY
																			<input id="txtNewVendor" type="hidden" name="txtNewVendor" runat="server" />
																			<input id="txtCDVendor" type="hidden" name="txtCDVendor" runat="server" />
																			<input id="txtNewRow" type="hidden" name="txtNewRow" runat="server" />
																		</td>
																	</tr>
																	<tr>
																		<td style="padding:5px; font-size:12px; width:100%;">
                                                                        <script type="text/javascript">
                                                                            $(window).resize(function () {
                                                                                $("#divContainer1").width($(window).width() - 80);
                                                                            });
                                                                            $(document).ready(function () {
                                                                                $("#divContainer1").width($(window).width() - 80);
                                                                            });
                                                                        </script>
																			<div style="overflow:auto; padding:0px; height:270px;" id="divContainer1">
                                                                                <div id="divContainer2" style="margin:0; height:auto; ">
																				<table width="100%" border="0" cellspacing="0" cellpadding="0" id="tblCriteria" runat="server">
                                                                                <tbody>
																					<tr bgcolor="#E4EFF8" runat="server">
																						<td runat="server">
																							<strong>VENDOR / CRITERIA</strong>
                                                                                            <asp:Repeater id="Repeater4" runat="server" DataSourceID="dsBidItemTendersSummary" onitemdatabound="Repeater4_ItemDataBound" >
																							    <ItemTemplate>
                                                                                                    <td id="Td1" runat="server">
                                                                                                        <span style="width:150px">
                                                                                                        <asp:DropDownList ID="txtVendorName" runat="server" DataSourceID="dsBidItemTendersSummary" DataValueField="VendorID" DataTextField="VendorName" Width="115px">
                                                                                                    </asp:DropDownList>
																									        <%--<asp:TextBox ID="txtVendorName" runat="server" Width="120" Text='<%# Eval("VendorName") %>'></asp:TextBox>--%>
                                                                                                            <asp:Image ID="imgVendor" runat="server" ImageUrl="~/web/buyerscreens/remove.png" style="cursor:pointer" AlternateText="Remove Vendor" />
																									        <input type="hidden" name="fromDB" id="fromDB" value='<%# Eval("fromDB") %>' runat="server" />
                                                                                                        </span>
                                                                                                    </td>
																							    </ItemTemplate>
																						    </asp:Repeater>
																						</td>
																					</tr>
																					
                                                                                    <tr runat="server">
																						<td runat="server">
																							<strong>Accreditation Status</strong>
                                                                                            <asp:Repeater id="Repeater100" runat="server" DataSourceID="dsBidItemTendersSummary" onitemdatabound="Repeater100_ItemDataBound" >
																							    <ItemTemplate>
                                                                                                    <td id="Td2" runat="server">
																								        <span style="width:150px">
																									        <asp:DropDownList ID="cbAccreditation" runat="server" DataSourceID="dsAccreditation" DataTextField="SupplierTypeDesc" DataValueField="SupplierTypeId" > </asp:DropDownList>
																								        </span>
                                                                                                    </td>
																							    </ItemTemplate>
																						    </asp:Repeater>
																						</td>
																					</tr>
																					<tr bgcolor="#E4EFF8" runat="server">
																						<td runat="server">
																							<b>Performance Rating</b>
                                                                                            <asp:Repeater id="RepeaterPRating" runat="server" DataSourceID="dsBidItemTendersSummary" onitemdatabound="RepeaterPRating_ItemDataBound" >
																							    <ItemTemplate>
																								    <td id="Td3" runat="server">
                                                                                                        <span style="width:150px">
																									        <asp:TextBox ID="txtPRating" runat="server" Width="40" Text='<%# Eval("PerformanceRating") %>' style="text-align:right"  Enabled="False"></asp:TextBox>
                                                                                                        </span>
																								    </td>
																							    </ItemTemplate>
																						    </asp:Repeater>
																						</td>
																					</tr>
																					<tr runat="server">
																						<td runat="server">
                                                                                            Technical Compliance
                                                                                            <asp:Repeater id="Repeater5" runat="server" DataSourceID="dsBidItemTendersSummary" onitemdatabound="Repeater5_ItemDataBound" >
																							    <ItemTemplate>
																								    <td id="Td4" runat="server">
                                                                                                        <span style="width:150px">
                                                                                                            <asp:DropDownList ID="txtTechnical" runat="server">
                                                                                                            <asp:ListItem></asp:ListItem>
                                                                                                                <asp:ListItem>Not-Compliant</asp:ListItem>
                                                                                                                <asp:ListItem>Fully Compliant</asp:ListItem>
                                                                                                            </asp:DropDownList>
                                                                                                        </span>
                                                                                                    </td>
																							    </ItemTemplate>
																						    </asp:Repeater>
                                                                                        </td>
																					</tr>
																					<tr bgcolor="#E4EFF8" runat="server">
																						<td runat="server">
                                                                                            Commercial Compliance
                                                                                            <asp:Repeater id="Repeater6" runat="server" DataSourceID="dsBidItemTendersSummary" onitemdatabound="Repeater6_ItemDataBound" >
																							    <ItemTemplate>
																								    <td id="Td5" runat="server">
                                                                                                        <span style="width:150px">
                                                                                                            <asp:DropDownList ID="txtCommercial" runat="server">
                                                                                                                 <asp:ListItem></asp:ListItem>
                                                                                                                <asp:ListItem>Not-Compliant</asp:ListItem>
                                                                                                                <asp:ListItem>Partial Compliant</asp:ListItem>
                                                                                                                <asp:ListItem>Fully Compliant</asp:ListItem>
                                                                                                            </asp:DropDownList>
                                                                                                        </span>
                                                                                                    </td>
																							    </ItemTemplate>
																						    </asp:Repeater>
                                                                                        </td>
																					</tr>
																					<tr runat="server">
																						<td runat="server">
                                                                                            Contract Compliance
																						    <asp:Repeater id="Repeater7" runat="server" DataSourceID="dsBidItemTendersSummary" onitemdatabound="Repeater7_ItemDataBound" >
																							    <ItemTemplate>
																								    <td runat="server">
                                                                                                        <span style="width:150px">
                                                                                                            <asp:DropDownList ID="txtContract" runat="server">
                                                                                                                <asp:ListItem></asp:ListItem>
                                                                                                                <asp:ListItem>Not Applicable</asp:ListItem>
                                                                                                                <asp:ListItem>Not-Compliant</asp:ListItem>
                                                                                                                <asp:ListItem>Partial Compliant</asp:ListItem>
                                                                                                                <asp:ListItem>Fully Compliant</asp:ListItem>
                                                                                                            </asp:DropDownList>
                                                                                                        </span>
                                                                                                    </td>
																							    </ItemTemplate>
																						    </asp:Repeater>
                                                                                        </td>
																					</tr>
                                                                                    
																					<tr runat="server" id="trHidden">
																						<td runat="server"><h3 style="border:none; color:#000"></h3>
																						    <asp:Repeater id="Repeater13" runat="server" DataSourceID="dsBidItemTendersSummary" onitemdatabound="Repeater13_ItemDataBound" >
																							    <ItemTemplate>
																								    <td runat="server">
                                                                                                        <span style="width:150px">
																									        <asp:CheckBox ID="chkAwarded" runat="server" Visible="false" />
                                                                                                        </span>
																								    </td>
																							    </ItemTemplate>
																						    </asp:Repeater>
                                                                                        </td>
																					</tr></tbody>
																				</table>
																				<table border="0" cellpadding="0" cellspacing="0" width="100%" ><tr>
																					<td align="left">
																						<input type="button" name="btnAddRow" id="btnAddRow" value="Add New Criteria (Row)" onclick="return btnAddRow_onclick();" />
																						&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
																						<input type="button" name="btnAddCol" id="btnAddCol" value="Add New Vendor (Column)" onclick="return btnAddCol_onclick();" />
																					</td>
																				</tr></table>
																			</div></div>
                                                                            <%--<asp:LinkButton ID="SaveVendors" runat="server" Font-Bold="True">Save Vendors</asp:LinkButton>--%>
																		</td>
																	</tr>
																</tbody>
															</table>

                                                            <asp:SqlDataSource ID="dsBidItemTendersSummary" runat="server"
																ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>'
																SelectCommand="SELECT * FROM tblBACEvaluationSummary WHERE BidRefNo=@BidRefNo ORDER BY VendorName ASC" onload="dsBidItemTendersSummary_Load" >
																<SelectParameters>
																	<asp:ControlParameter Name="BidRefNo" ControlID="BuyerBidForBac" PropertyName="SelectedValue" Type="Int32" />
																</SelectParameters>
															</asp:SqlDataSource>
                                                            
                                                            <asp:SqlDataSource ID="dsVendors" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>'
																SelectCommand="select CAST(t1.VendorId as char) as VendorId, t1.VendorName from tblVendors t1, tblUsers t3 where t1.VendorId = t3.UserId and t3.status = 1 and CAST(t1.VendorId as char) NOT IN (select t2.VendorId from tblBACEvaluationSummary t2 where t2.BidRefNo = @BidRefNo) order by t1.VendorName" >
                                                                <SelectParameters>
																	<asp:ControlParameter Name="BidRefNo" ControlID="BuyerBidForBac" PropertyName="SelectedValue" Type="Int32" />
																</SelectParameters>
															</asp:SqlDataSource>
																			
															<asp:SqlDataSource ID="dsAccreditation" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>'
																SelectCommand="SELECT * FROM rfcSupplierType ORDER BY SupplierTypeId" >
															</asp:SqlDataSource>

                                                            </td>
                                                    </tr>
                                                    <tr>
														<td colspan="2" valign="top">

                                                            <asp:GridView ID="gvItems" runat="server" AutoGenerateColumns="False"
																			DataSourceID="dsItems" Width="100%" onrowcommand="gvItems_RowCommand" onBorderColor="#71A9D2" BorderStyle="Solid" 
                                                                BorderWidth="2px" CellPadding="3" BorderColor="#71A9D2" >
																			<Columns>
																				<asp:TemplateField HeaderText="Item #" HeaderStyle-CssClass="ui-widget-header" HeaderStyle-Font-Size="12px" HeaderStyle-Height="25px" >
																					<ItemTemplate>
																						&nbsp;<asp:Label ID="lblItemNo" runat="server" Text='<%# Eval("BidDetailNo") %>' Font-Size="12px"></asp:Label>
																					</ItemTemplate>

<HeaderStyle CssClass="ui-widget-header" Font-Size="12px" Height="25px"></HeaderStyle>
																				</asp:TemplateField>
																				
                                                                                <asp:TemplateField HeaderText="Item Details" HeaderStyle-CssClass="ui-widget-header" HeaderStyle-Font-Size="12px" >
																					<ItemTemplate>
																						<asp:TextBox ID="lblItemD" runat="server" Text='<%# Bind("ItemName") %>' Width="350" Font-Bold="True"></asp:TextBox><%--CommandArgument='<%# Bind("BidDetailNo") %>' CommandName="AddItemRow"--%>
																					</ItemTemplate>

<HeaderStyle CssClass="ui-widget-header" Font-Size="12px"></HeaderStyle>
																				</asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="PR No" HeaderStyle-CssClass="ui-widget-header" HeaderStyle-Font-Size="12px" HeaderStyle-Width="120px" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
																					<ItemTemplate>
																						<asp:TextBox ID="lblPRNo" runat="server"  Width="200px" Font-Bold="True" Text='<%# Eval("PR_No") %>'  ></asp:TextBox>
																					</ItemTemplate>

<HeaderStyle CssClass="ui-widget-header" Font-Size="12px" Width="150px"></HeaderStyle>

<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
																				</asp:TemplateField>
																				<asp:TemplateField HeaderText="Add | Remove Item" HeaderStyle-CssClass="ui-widget-header" HeaderStyle-Font-Size="12px" HeaderStyle-Width="160px" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
																					<ItemTemplate>
																						<asp:LinkButton ID="lblAddItem" runat="server" Text='<%# (Eval("PR_No").ToString()!="")?"Edit Item" : "Add Item" %>' CommandArgument='<%# Bind("BidDetailNo") %>' CommandName="AddItemRow" ></asp:LinkButton>&nbsp;&nbsp;|&nbsp;&nbsp;<asp:LinkButton ID="lblRemoveItem" runat="server" Text='Remove Item' CommandArgument='<%# Bind("BidDetailNo") %>' CommandName="DelItemRow"></asp:LinkButton>
																					</ItemTemplate>

<HeaderStyle CssClass="ui-widget-header" Font-Size="12px" Width="160px"></HeaderStyle>

<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
																				</asp:TemplateField>
																			</Columns>
																		    <RowStyle Font-Bold="True" />
																		</asp:GridView>
                                                            <asp:SqlDataSource ID="dsItems" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>'
																SelectCommand="SELECT BidDetailNo, max(ItemName) as ItemName, Max(PR_No) AS PR_No 
																FROM (
																SELECT DISTINCT t2.BidDetailNo, t2.DetailDesc ItemName, '' PR_No 
																FROM tblBidTenders t1
																INNER JOIN tblBidItemDetails t2 ON t2.BidDetailNo = t1.BidDetailNo
																INNER JOIN tblBidItems t4 ON t4.BidRefNo = t2.BidRefNo
																WHERE t2.BidRefNo = @BidRefNo AND t1.Status > 0 
																UNION 
																SELECT t2.BidDetailNo, t2.ItemName, t2.PR_No 
																FROM tblBACEvaluationDetails t2 
																WHERE t2.BidRefNo = @BidRefNo ) as a2 
																group by BidDetailNo  
																UNION  select ISNULL(max(BidDetailNo),1) AS BidDetailNo, '--- Enter New Item ---' AS ItemName, '' PR_No 
																FROM (SELECT max(t2.BidDetailNo)+1 AS BidDetailNo FROM tblBidItemDetails t2 
																WHERE  t2.BidRefNo = @BidRefNo UNION SELECT max(t2.BidDetailNo)+1 AS BidDetailNo 
																FROM   tblBACEvaluationDetails t2 WHERE  t2.BidRefNo = @BidRefNo) AS a1
																ORDER BY BidDetailNo">
																<SelectParameters>
																	<%--<asp:ControlParameter Name="BidRefNo" ControlID="BuyerBidForBac" PropertyName="SelectedValue" Type="Int32" />--%>
                                                                    <asp:SessionParameter Name="BidRefNo" SessionField="BuyerBidForBac" Type="Int32" />
																</SelectParameters>
															</asp:SqlDataSource>


                                                            </td>
                                                    </tr>
                                                    <tr>
														<td colspan="2" valign="top">

															<table width="100%" border="1" cellpadding="0" cellspacing="0" rules="all" class="itemDetails" id="Table1" style="border-color:#71A9D2; font-family: Arial; font-size: 11px; border-collapse: collapse; background-color: #F4F1C4; height:100%">
																<tbody>
																	<tr>
																		<td class="ui-widget-header" style="height:26px; vertical-align:middle;">
																			COMMERCIAL DETAILS
																		</td>
																	</tr>
																	<tr><td>
<script type="text/javascript">
    $(document).ready(function () {
        $(".clickQty").change(function () {
            var qtyID = $(this).attr('id');
            var costID = qtyID.replace("_rptQty_", "_rptUnitCost_");
            costID = costID.replace("_txtQty", "_txtUnitCost");
            var totalID = qtyID.replace("_rptQty_", "_rptTotalCost_");
            totalID = totalID.replace("_txtQty", "_txtTotalCost");
            var qty = $("#" + qtyID).val();
            qty = qty.replace(new RegExp(",", "g"), "");
            var cost = $("#" + costID).val();
            cost = cost.replace(new RegExp(",", "g"), "");
            $("#" + totalID).val(((qty * cost) == 'NaN') ? "0" : (qty * cost));

            var value1;
            value1 = parseFloat($("#" + totalID).val()).toFixed(2);
            $("#" + totalID).val((add_commas(value1) == 'NaN') ? "0" : add_commas(value1));
        });
        $(".clickCost").click(function () {
            var costID = $(this).attr('id');
            var qtyID = costID.replace("_rptUnitCost_", "_rptQty_");
            qtyID = qtyID.replace("_txtUnitCost", "_txtQty");
            var totalID = costID.replace("_rptUnitCost_", "_rptTotalCost_");
            totalID = totalID.replace("_txtUnitCost", "_txtTotalCost");
            var qty = $("#" + qtyID).val();
            qty = qty.replace(new RegExp(",", "g"), "");
            var cost = $("#" + costID).val();
            cost = cost.replace(new RegExp(",", "g"), "");

            var value1;
            value1 = parseFloat($("#" + totalID).val()).toFixed(2);
            $("#" + costID).val(cost);
            $("#" + costID).select();
        });
        $(".clickCost").focus(function () {
            var costID = $(this).attr('id');
            var qtyID = costID.replace("_rptUnitCost_", "_rptQty_");
            qtyID = qtyID.replace("_txtUnitCost", "_txtQty");
            var totalID = costID.replace("_rptUnitCost_", "_rptTotalCost_");
            totalID = totalID.replace("_txtUnitCost", "_txtTotalCost");
            var qty = $("#" + qtyID).val();
            qty = qty.replace(new RegExp(",", "g"), "");
            var cost = $("#" + costID).val();
            cost = cost.replace(new RegExp(",", "g"), "");

            var value1;
            value1 = parseFloat($("#" + totalID).val()).toFixed(2);
            $("#" + costID).val(cost);
            $("#" + costID).select();
        });
        $(".clickCost").blur(function () {
            var costID = $(this).attr('id');
            var qtyID = costID.replace("_rptUnitCost_", "_rptQty_");
            qtyID = qtyID.replace("_txtUnitCost", "_txtQty");
            var totalID = costID.replace("_rptUnitCost_", "_rptTotalCost_");
            totalID = totalID.replace("_txtUnitCost", "_txtTotalCost");
            var qty = $("#" + qtyID).val();
            qty = qty.replace(new RegExp(",", "g"), "");
            var cost = $("#" + costID).val();
            cost = cost.replace(new RegExp(",", "g"), "");
            $("#" + totalID).val(((qty * cost) == 'NaN') ? "0" : (qty * cost));

            var value1;
            value1 = parseFloat($("#" + totalID).val()).toFixed(2);
            $("#" + totalID).val((add_commas(value1) == 'NaN') ? "0" : add_commas(value1));
            value1 = parseFloat($("#" + costID).val()).toFixed(2);
            $("#" + costID).val((add_commas(value1) == 'NaN') ? "0" : add_commas(value1));
        });
    });
</script>
                                                                    <script type="text/javascript">
                                                                        $(window).resize(function () {
                                                                            $("#divContainer3").width($(window).width() - 80);
                                                                        });
                                                                        $(document).ready(function () {
                                                                            $("#divContainer3").width($(window).width() - 80);
                                                                        });
                                                                    </script>
																			<div style="overflow:auto; padding:0px; height:350px;" id="divContainer3">
                                                                                <div id="divContainer4" style="margin:0; ">
                                                                        <asp:DataList ID="lstCommDetails" runat="server" DataSourceID="dsItems1" 
                                                                            DataKeyField="BidDetailNo" >
                                                                            <ItemTemplate>
                                                                                <asp:TextBox ID="BidDetailNo1" runat="server" Text='<%# Eval("BidDetailNo") %>' Visible="false"></asp:TextBox>
                                                                                <br />
                                                                                <b>Item #: </b>
                                                                                <asp:Label ID="SKULabel" runat="server" Text='<%# Eval("BidDetailNo") %>' />
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
																						            <asp:TextBox ID="txtVendorName" runat="server" Width="150" Text='<%# Eval("VendorName") %>' Enabled="False"></asp:TextBox>
                                                                                                    <asp:TextBox ID="txtBidTenderNo1" runat="server" Text='<%# Eval("BidTenderNo") %>' Visible="false" ></asp:TextBox>
                                                                                                </td>
																					        </ItemTemplate>
																				        </asp:Repeater>
                                                                                        <asp:SqlDataSource ID="dsBidItemTendersDetails_Vendor" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>'
																	                        SelectCommand="SELECT VendorName, BidTenderNo FROM tblBACEvaluationDetails WHERE BidRefNo=@BidRefNo AND BidDetailNo=@BidDetailNo AND UserId=@UserId AND VendorName not in (select VendorName from tblBACEvaluationSummary where (TechCompliance=1 or CommCompliance=1 or ContCompliance=2) AND BidRefNo=@BidRefNo) ORDER BY Ranking asc, VendorName ASC" >
																	                        <SelectParameters>
                                                                                                <asp:SessionParameter Name="UserId" SessionField="UserId" Type="Int32" />
																		                        <asp:ControlParameter Name="BidRefNo" ControlID="BuyerBidForBac" PropertyName="SelectedValue" Type="Int32" />
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
																						            <asp:TextBox ID="txtQty" runat="server" Width="30" Text='<%# Eval("Qty") %>' style="text-align:right" CssClass="clickQty" ></asp:TextBox>
                                                                                                    <asp:TextBox ID="txtBidTenderNo1" runat="server" Text='<%# Eval("BidTenderNo") %>' Visible="false" ></asp:TextBox>
                                                                                                </td>
																					        </ItemTemplate>
																				        </asp:Repeater>
                                                                                        <asp:SqlDataSource ID="dsBidItemTendersDetails_Qty" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>'
																	                        SelectCommand="SELECT BidTenderNo, Qty FROM tblBACEvaluationDetails WHERE BidRefNo=@BidRefNo AND BidDetailNo=@BidDetailNo AND UserId=@UserId AND VendorName not in (select VendorName from tblBACEvaluationSummary where (TechCompliance=1 or CommCompliance=1 or ContCompliance=2) AND BidRefNo=@BidRefNo) ORDER BY Ranking asc, VendorName ASC" >
																	                        <SelectParameters>
                                                                                                <asp:SessionParameter Name="UserId" SessionField="UserId" Type="Int32" />
																		                        <asp:ControlParameter Name="BidRefNo" ControlID="BuyerBidForBac" PropertyName="SelectedValue" Type="Int32" />
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
                                                                                                    <asp:TextBox ID="txtBidTenderNo1" runat="server" Text='<%# Eval("BidTenderNo") %>' Visible="false" ></asp:TextBox>
                                                                                                </td>
																					        </ItemTemplate>
																				        </asp:Repeater>
                                                                                        <asp:SqlDataSource ID="dsBidItemTendersDetails_Unit" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>'
																	                        SelectCommand="SELECT BidTenderNo, UnitMeasure FROM tblBACEvaluationDetails WHERE BidRefNo=@BidRefNo AND BidDetailNo=@BidDetailNo AND UserId=@UserId AND VendorName not in (select VendorName from tblBACEvaluationSummary where (TechCompliance=1 or CommCompliance=1 or ContCompliance=2) AND BidRefNo=@BidRefNo) ORDER BY Ranking asc, VendorName ASC" >
																	                        <SelectParameters>
                                                                                                <asp:SessionParameter Name="UserId" SessionField="UserId" Type="Int32" />
																		                        <asp:ControlParameter Name="BidRefNo" ControlID="BuyerBidForBac" PropertyName="SelectedValue" Type="Int32" />
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
                                                                                                    <asp:TextBox ID="txtBidTenderNo1" runat="server" Text='<%# Eval("BidTenderNo") %>' Visible="false" ></asp:TextBox>
                                                                                                </td>
																					        </ItemTemplate>
																				        </asp:Repeater>
                                                                                        <asp:SqlDataSource ID="dsBidItemTendersDetails_Curr" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>'
																	                       SelectCommand="SELECT BidTenderNo, Currenzy FROM tblBACEvaluationDetails WHERE BidRefNo=@BidRefNo AND BidDetailNo=@BidDetailNo AND UserId=@UserId AND VendorName not in (select VendorName from tblBACEvaluationSummary where (TechCompliance=1 or CommCompliance=1 or ContCompliance=2) AND BidRefNo=@BidRefNo) ORDER BY Ranking asc, VendorName ASC" >
																	                        <SelectParameters>
                                                                                                <asp:SessionParameter Name="UserId" SessionField="UserId" Type="Int32" />
																		                        <asp:ControlParameter Name="BidRefNo" ControlID="BuyerBidForBac" PropertyName="SelectedValue" Type="Int32" />
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
																						            <asp:TextBox ID="txtUnitCost" runat="server" Width="85" Text='<%# String.Format("{0:0,0.00}", Convert.ToDouble(Eval("UnitCost"))) %>' style="text-align:right" CssClass="clickCost"></asp:TextBox>
                                                                                                    <asp:TextBox ID="txtBidTenderNo1" runat="server" Text='<%# Eval("BidTenderNo") %>' Visible="false" ></asp:TextBox>
                                                                                                </td>
																					        </ItemTemplate>
																				        </asp:Repeater>
                                                                                        <asp:SqlDataSource ID="dsBidItemTendersDetails_UnitCost" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>'
																	                       SelectCommand="SELECT BidTenderNo, UnitCost FROM tblBACEvaluationDetails WHERE BidRefNo=@BidRefNo AND BidDetailNo=@BidDetailNo AND UserId=@UserId AND VendorName not in (select VendorName from tblBACEvaluationSummary where (TechCompliance=1 or CommCompliance=1 or ContCompliance=2) AND BidRefNo=@BidRefNo) ORDER BY Ranking asc, VendorName ASC" >
																	                        <SelectParameters>
                                                                                                <asp:SessionParameter Name="UserId" SessionField="UserId" Type="Int32" />
																		                        <asp:ControlParameter Name="BidRefNo" ControlID="BuyerBidForBac" PropertyName="SelectedValue" Type="Int32" />
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
																						            <asp:TextBox ID="txtTotalCost" runat="server" Width="85" Text='<%# String.Format("{0:0,0.00}", Eval("TotalCost")) %>' style="text-align:right" ></asp:TextBox>
                                                                                                    <asp:TextBox ID="txtBidTenderNo1" runat="server" Text='<%# Eval("BidTenderNo") %>' Visible="false" ></asp:TextBox>
                                                                                                </td>
																					        </ItemTemplate>
																				        </asp:Repeater>
                                                                                        <asp:SqlDataSource ID="dsBidItemTendersDetails_TotalCost" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>'
																	                       SelectCommand="SELECT BidTenderNo, TotalCost FROM tblBACEvaluationDetails WHERE BidRefNo=@BidRefNo AND BidDetailNo=@BidDetailNo AND UserId=@UserId AND VendorName not in (select VendorName from tblBACEvaluationSummary where (TechCompliance=1 or CommCompliance=1 or ContCompliance=2) AND BidRefNo=@BidRefNo) ORDER BY Ranking asc, VendorName ASC" >
																	                        <SelectParameters>
                                                                                                <asp:SessionParameter Name="UserId" SessionField="UserId" Type="Int32" />
																		                        <asp:ControlParameter Name="BidRefNo" ControlID="BuyerBidForBac" PropertyName="SelectedValue" Type="Int32" />
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
																						            <asp:TextBox ID="txtRanking" runat="server" Width="30" Text='<%# Eval("Ranking") %>' style="text-align:right" ></asp:TextBox>
                                                                                                    <asp:TextBox ID="txtBidTenderNo1" runat="server" Width="150" Text='<%# Eval("BidTenderNo") %>' Visible="false" ></asp:TextBox>
                                                                                                </td>
																					        </ItemTemplate>
																				        </asp:Repeater>
                                                                                        <asp:SqlDataSource ID="dsBidItemTendersDetails_Ranking" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>'
																	                        SelectCommand="SELECT BidTenderNo, Ranking FROM tblBACEvaluationDetails WHERE BidRefNo=@BidRefNo AND BidDetailNo=@BidDetailNo AND UserId=@UserId AND VendorName not in (select VendorName from tblBACEvaluationSummary where (TechCompliance=1 or CommCompliance=1 or ContCompliance=2) AND BidRefNo=@BidRefNo) ORDER BY Ranking ASC, VendorName ASC" >
																	                        <SelectParameters>
                                                                                                <asp:SessionParameter Name="UserId" SessionField="UserId" Type="Int32" />
																		                        <asp:ControlParameter Name="BidRefNo" ControlID="BuyerBidForBac" PropertyName="SelectedValue" Type="Int32" />
                                                                                                <asp:ControlParameter Name="BidDetailNo" ControlID="BidDetailNo1" Type="Int32" />
																	                        </SelectParameters>
																                        </asp:SqlDataSource>
                                                                                    </tr>

                                                                                    <tr>
                                                                                        <td>
                                                                                            Award   
                                                                                        </td>
                                                                                        <asp:Repeater id="rptAward" runat="server" DataSourceID="dsBidItemTendersDetails_Award" OnItemDataBound="rptAward1_OnItemDataBound" >
																					        <ItemTemplate>
                                                                                                <td style="width:150px">
                                                                                                    <input id="chkAward" type="checkbox" runat="server" />
                                                                                                    <asp:TextBox ID="txtAward" runat="server" Width="0" Height="0" BorderColor="White" BorderStyle="Solid" BorderWidth="1" Visible="true" Text='<%# Eval("Chkd") %>' ></asp:TextBox>
                                                                                                    <asp:TextBox ID="txtBidTenderNo1" runat="server" Width="150" Text='<%# Eval("BidTenderNo") %>' Visible="false" ></asp:TextBox>
                                                                                                </td>
																					        </ItemTemplate>
																				        </asp:Repeater>
                                                                                        <asp:SqlDataSource ID="dsBidItemTendersDetails_Award" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>'
																	                        SelectCommand="SELECT BidTenderNo, Chkd FROM tblBACEvaluationDetails WHERE BidRefNo=@BidRefNo AND BidDetailNo=@BidDetailNo AND UserId=@UserId AND VendorName not in (select VendorName from tblBACEvaluationSummary where (TechCompliance=1 or CommCompliance=1 or ContCompliance=2) AND BidRefNo=@BidRefNo) ORDER BY Ranking asc, VendorName ASC" >
																	                        <SelectParameters>
                                                                                                <asp:SessionParameter Name="UserId" SessionField="UserId" Type="Int32" />
																		                        <asp:ControlParameter Name="BidRefNo" ControlID="BuyerBidForBac" PropertyName="SelectedValue" Type="Int32" />
                                                                                                <asp:ControlParameter Name="BidDetailNo" ControlID="BidDetailNo1" Type="Int32" />
																	                        </SelectParameters>
																                        </asp:SqlDataSource>
                                                                                    </tr>
                                                                                </tbody></table>
                                                                               
                                                                            </ItemTemplate>
                                                                        </asp:DataList>
                                                                        </div></div>
                                                                         <asp:LinkButton ID="SaveItem" runat="server" Font-Bold="True">Save Item</asp:LinkButton>
                                                                    </td></tr>
																</tbody>
															</table>
                                                            <asp:SqlDataSource ID="dsItems1" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>' >
																<%--<SelectParameters>
                                                                    <asp:SessionParameter Name="UserId" SessionField="UserId" Type="Int32" />
																	<asp:ControlParameter Name="BidRefNo" ControlID="BuyerBidForBac" PropertyName="SelectedValue" Type="Int32" />
                                                                    <asp:Parameter  Name="BidRefNo" DefaultValue="0" Type="Int32" />
																</SelectParameters>--%>
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
																		<td align="center" valign="middle" style="padding:5px; font-size:12px; width:100%"><label for="textarea"></label>
																			<textarea name="executive_summary" id="executive_summary" cols="45" rows="10" 
                                                                                runat="server" style="width:100%; font-family: Arial;"></textarea>
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
																			<textarea name="remarks_comments" id="remarks_comments" cols="45" rows="10" style="width:100%; font-family:Arial;" runat="server" ></textarea>
																		</td>
																	</tr>
																</tbody>
															</table>
                                                        </td>
                                                    </tr>
                                                    <tr>
														<td colspan="2" valign="top">


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
																					<table id="myNatureOfSavings" runat="server" style="font-weight:bold"></table>
																				</td></tr>
																			</table>
																		</td>
																		<td align="center" valign="middle" style="padding:5px; font-size:12px;" class="style4">
<script type="text/javascript">
    $(document).ready(function () {
        $(".AwardedPercent").change(function () {
            var AwardedCost = $("#lblAccCost").val();
            var Savings = $("#lblNegDisc").val();
            AwardedCost = AwardedCost.replace(new RegExp(",", "g"), "");
            Savings = Savings.replace(new RegExp(",", "g"), "");
            $("#lblPercentSpend").val((Savings / AwardedCost) * 100);

            var value1;
            value1 = parseFloat(AwardedCost).toFixed(2);
            var value2;
            value2 = (add_commas(value1) == 'NaN') ? "0.00" : add_commas(value1);
            $("#lblAccCost").val(value2);
            value1 = parseFloat(Savings).toFixed(2);
            value2 = (add_commas(value1) == 'NaN') ? "0.00" : add_commas(value1);
            $("#lblNegDisc").val(value2);
            value1 = parseFloat($("#lblPercentSpend").val()).toFixed(2);
            value2 = (add_commas(value1) == 'NaN') ? "0.00" : add_commas(value1);
            $("#lblPercentSpend").val(value2);
        });
    });
</script>
																			<table style="width: 80%;">
																				<tr>
																					<td class="style5">Accumulated Awarded Cost:</td>
																					<td>
                                                                                        <asp:TextBox ID="lblAccCost" runat="server" style="text-align:right;" 
                                                                                            Text="0.00" Width="185px" CssClass="AwardedPercent" Font-Bold="True"></asp:TextBox>
                                                                                    </td>
																				</tr>
																				<tr>
																					<td class="style5">Savings Amount:</td>
																					<td>
                                                                                        <asp:TextBox ID="lblNegDisc" runat="server" style="text-align:right;" 
                                                                                            Text="0.00" Width="185px" CssClass="AwardedPercent" Font-Bold="True"></asp:TextBox>
                                                                                    </td>
																				</tr>
																				<tr>
																					<td class="style5">% from Savings:</td>
																					<td>
                                                                                        <asp:TextBox ID="lblPercentSpend" runat="server" style="text-align:right;" 
                                                                                            Text="0.00" Width="185px" Font-Bold="True"></asp:TextBox>
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
																					<td colspan="2">Reviewed by / date: <br />&nbsp;</td>
																				</tr>
                                                                                <%# Eval("OriginalFileName") %>
															                    <tr>
														                            <td colspan="2"><strong><asp:Literal ID="lblApprover4" runat="server" Text="PURCHASING_APPROVER"></asp:Literal></strong><asp:HiddenField ID="approver4" runat="server" />
                                                                                    </td>
																				</tr>
																				<tr>
				                                                                    <td colspan="2" style="border-top:2px #000 solid;">Purchasing Approver</td>
					                                                            </tr>
																			</table>
																		</td>
							                                        </tr>
																</tbody>
															</table>



                                                            </td>
                                                    </tr>
                                                    <tr>
														<td colspan="2" valign="top">


															<table border="1" cellpadding="0" cellspacing="0" rules="all" class="itemDetails" id="Biddetails_details1_dvEventDetails12" style="border-color:#71A9D2; font-family: Arial; font-size: 11px; border-collapse: collapse; background-color: #F4F1C4; height:100%; width:100%;">
																<tbody>
																	<tr>
																		<td class="ui-widget-header" style="height:26px; vertical-align:middle;">BID APPROVING AUTHORITY</td>
																	</tr>
																	<tr valign="middle" >
																		<td width="100%" align="center" valign="middle" style="padding:5px; font-size:12px;">
                                                                        <asp:Repeater ID="RepeaterApprover" runat="server" DataSourceID="dsApprover" 
                                                                                onitemdatabound="RepeaterApprover_ItemDataBound" 
                                                                                onload="RepeaterApprover_Load" >
                                                                        <%----%>
																				<ItemTemplate>
																					<div style="float:left; margin-right:15px; width:150px; padding:10px 20px; display:block;">
																						<table width="150px" border="0" cellspacing="0" cellpadding="0">
																							<tr>
																								<td colspan="2">Approved by / date: <br />&nbsp;</td>
																							</tr>
																							<%--<tr>
																								<td><label for="clarify1">Clarify</label>
																									<asp:CheckBox ID="clarify1" runat="server" Enabled="false" />
																								</td>
																								<td><label for="approved1">Approved</label>
																									<asp:CheckBox ID="approved1" runat="server" Enabled="false" />
                                                                                                    </td>
                                                                                                    </tr>--%>
                                                                                            <tr>
                                                                                            <td colspan="2"><strong><%# Eval("Name1") %></strong></td>
																							</tr>
																							<tr>
																								<td colspan="2"><%# String.Format("{0:0,0.00}", Eval("ApprovingMaxLimit")) %> <%--<%# Eval("BACId") %>--%></td>
																							</tr>
																							<tr>
																								<td colspan="2" style="border-top:2px #000 solid;">
                                                                                                    <asp:Literal ID="lblApprover" runat="server" Text="APPROVER"></asp:Literal>
                                                                                                    <%--<asp:CheckBox ID="chkApproverLevel" runat="server" Value='<%# Eval("BACId") %>' />--%>
                                                                                                    <input id="chkApproverLevel" type="checkbox" runat="server" value='<%# Eval("BACId") %>'  />
                                                                                                </td>
																							</tr>
																						</table>
																					</div>
																				</ItemTemplate>
																			</asp:Repeater>
																		</td>
																	</tr>
																</tbody>
															</table>

                                                            <asp:SqlDataSource ID="dsApprover" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>' >
                                                                <SelectParameters>
		                                                            <asp:ControlParameter Name="BidRefNo" ControlID="BuyerBidForBac" PropertyName="SelectedValue" Type="Int32" />
	                                                            </SelectParameters>
                                                            </asp:SqlDataSource>

														</td>
                                                    </tr>
                                                </table>
												<br />
												
                                                <table border="0" cellpadding="0" cellspacing="0" width="100%" id="actions">
                                                    <tr>
                                                        <td align="right">
                                                            <input id="btnCancel" type="button" value="Cancel BAC" onclick="if(confirm('This will delete the current session. Continue?')){ document.getElementById('status').value=-1;  $('#form1').submit(); }" />
                                                            <%--<asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClientClick="if(confirm('This will delete the current session. Continue?')){ document.getElementById('status').value=-1;  $('#form1').submit(); }" />--%>
                                                            <input id="btnDraft" type="button" value="Draft" onclick="document.getElementById('status').value=0; GetCriteria(); $('#form1').submit();" />
                                                            <input id="btnEndorse" type="button" value="Endorse" onclick="document.getElementById('status').value=1;  GetCriteria(); ValidateBeforeSave();" />
                                                            <input id="btnBack" type="button" value="BACK" onclick="__doPostBack('HistoryBack');"  />
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
<script type="text/javascript">
    HideCriteria();
    if ($("#lblLoad").val() == "true") {
        ClearSavedCookies();
    }
    ShowSavedCookies();
    ShowNewRowsOnLoad();
    ComputeAccCost();
</script>