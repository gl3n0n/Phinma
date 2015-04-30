<%@ Page Language="C#" AutoEventWireup="true" CodeFile="bidawardingchecklistendorsed.aspx.cs" Inherits="web_buyerscreens_bidawardingchecklistendorsed" %>
<%@ Register TagPrefix="EBid" TagName="LeftNav" Src="~/WEB/usercontrol/Buyer/LeftNav.ascx" %>
<%@ Register TagPrefix="EBid" TagName="TopNavBids" Src="~/WEB/usercontrol/Buyer/TopNavBids.ascx" %>
<%@ Register TagPrefix="EBid" TagName="TopNav3" Src="~/WEB/usercontrol/Buyer/TopNav3.ascx" %>
<%@ Register TagPrefix="EBid" TagName="GlobalLinksNav" Src="~/web/usercontrol/GlobalLinksNav.ascx" %>
<%@ Register TagPrefix="EBid" TagName="bac_BidDetails" Src="~/web/usercontrol/bac/bac_BidDetails.ascx" %>
<%@ Register TagPrefix="EBid" TagName="bac_executiveSummary" Src="~/web/usercontrol/bac/bac_executiveSummary.ascx" %>
<%@ Register TagPrefix="EBid" TagName="bac_remarksComments" Src="~/web/usercontrol/bac/bac_remarksComments.ascx" %>
<%@ Register TagPrefix="EBid" TagName="Footer" Src="~/web/usercontrol/Footer.ascx" %>
<%@ Register src="../usercontrol/bac/bac_supplyPosition.ascx" tagname="bac_supplyPosition" tagprefix="EBid" %>
<%@ Register src="../usercontrol/bac/bac_sourcingStrategy.ascx" tagname="bac_sourcingStrategy" tagprefix="EBid" %>
<%@ Register src="../usercontrol/bac/bac_typeOfPurchase.ascx" tagname="bac_typeOfPurchase" tagprefix="EBid" %>
<%@ Register src="../usercontrol/bac/bac_paymentTerms.ascx" tagname="bac_paymentTerms" tagprefix="EBid" %>
<%@ Register src="../usercontrol/bac/bac_natureOfSavings.ascx" tagname="bac_NatureOfSavings" tagprefix="EBid" %>
<%@ Register src="../usercontrol/bac/bac_bidApprovingCommittee.ascx" tagname="bac_bidApprovingCommittee" tagprefix="EBid" %>
<%@ Register src="../usercontrol/bac/bac_awardedto.ascx" tagname="bac_awardedTo" tagprefix="EBid" %>
<%@ Register src="../usercontrol/bac/bac_Items.ascx" tagname="bac_Items" tagprefix="EBid" %>
<%@ Register src="../usercontrol/bac/bac_purchasingApprover.ascx" tagname="bac_purchasingApprover" tagprefix="EBid" %>
<%@ Register src="../usercontrol/bac/bac_bidEvaluationDetails.ascx" tagname="bac_bidEvaluationDetails" tagprefix="EBid" %>
<%@ Register src="../usercontrol/bac/bac_commercialDetails.ascx" tagname="bac_commercialDetails" tagprefix="EBid" %>
<%@ Register src="../usercontrol/bac/bac_supportingDocument.ascx" tagname="bac_supportingDocument" tagprefix="EBid" %>
<%@ Register src="../usercontrol/bac/bac_basisForAwarding.ascx" tagname="bac_basisForAwarding" tagprefix="EBid" %>
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
    <script type="text/javascript" src="../../jquery/ui/jquery.effects.slide.js"></script>
    <link rel="stylesheet" href='../../jquery/themes/redmond/jquery.ui.all.css' type="text/css" />
    <link href='<%= "../themes/"+Session["configTheme"]+"/css/style.css" %>' rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href='<%= "../themes/"+Session["configTheme"]+"/css/style.css" %>' />
    <link rel="stylesheet" type="text/css" href='<%="../themes/"+Session["configTheme"]+"/css/style_buyer.css" %>' />
    <script type="text/javascript">
        $(document).ready(function () {
            $("button, input:submit, input:button").button();

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
                    sCtrl = "#bac_commercialDetails1_lstCommDetails_ctl0" + i + "_rptQty_ctl00_txtQty";
                }
                else {
                    sCtrl = "#bac_commercialDetails1_lstCommDetails_ctl" + i + "_rptQty_ctl00_txtQty";
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
                        sCtrl = "#bac_commercialDetails1_lstCommDetails_ctl0" + i;
                        sCtrl1 = "#bac_commercialDetails1_lstCommDetails_ctl0" + i;
                    }
                    else {
                        sCtrl = "#bac_commercialDetails1_lstCommDetails_ctl" + i;
                        sCtrl1 = "#bac_commercialDetails1_lstCommDetails_ctl" + i;
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
                            sCtrl1 = "#bac_bidEvaluationDetails1_RepeaterC" + j + "_ctl0" + j1 + "_item";
                        }
                        else {
                            sCtrl1 = "#bac_bidEvaluationDetails1_RepeaterC" + j + "_ctl" + j1 + "_item";
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
                            sCtrl = "#bac_bidEvaluationDetails1_txtR" + j;
                            if ($(sCtrl).length == 0) {
                                break;
                            }
                            else {
                                $(sCtrl).val(readCookie("Criteria_" + i1 + "_"));
                            }

                            for (j1 = 1; j1 < 100; j1++) {
                                if (j1 < 10) {
                                    sCtrl1 = "#bac_bidEvaluationDetails1_RepeaterC" + j + "_ctl0" + j1 + "_item";
                                }
                                else {
                                    sCtrl1 = "#bac_bidEvaluationDetails1_RepeaterC" + j + "_ctl" + j1 + "_item";
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
    </script>
    <style type="text/css">
        .ui-widget { font-family: Arial; font-size: 11; }
        .style1
        {
            width: 81px;
        }
        .style4
        {
            height: 19px;
        }
        .style5
        {
            width: 50%;
        }
    </style>
</head>
<body id="Body1" runat="server">
    <div>
        <form enctype="multipart/form-data" id="form1" runat="server">&nbsp;
            <input id="lblPreparedDate_" type="hidden" runat="server" />
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
															<asp:SqlDataSource ID="dsBuyerBidForBac" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>'
                                                            SelectCommand="SELECT * FROM tblBACBidItems WHERE BacRefNo=@BacRefNo"  >
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
                                                        <td valign="top" class="style5">
															


                                                            <EBid:bac_supplyPosition runat="server" ID="Bac_supplyPosition" />



                                                        </td>
                                                        <td valign="top" width="50%">
															


                                                            <EBid:bac_sourcingStrategy runat="server" ID="bac_sourcingStrategy1" />



                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td valign="top" class="style5">

															<EBid:bac_typeOfPurchase runat="server" ID="bac_typeOfPurchase1" />

														</td>
                                                        <td valign="top">

															<EBid:bac_paymentTerms runat="server" ID="bac_paymentTerms1" />
                                                            
                                                        </td>
                                                    </tr>
                                                    <tr>
														<td valign="top" class="style5">
															
                                                            <EBid:bac_supportingDocument runat="server" ID="bac_supportingDocument1" />

														</td>
														<td valign="top">


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
														<td class="style5" colspan="2">

                                                        <EBid:bac_bidApprovingCommittee runat="server" ID="bac_bidApprovingCommittee1" />

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
                                                            <input id="btnPDF" type="button" value="Save as PDF" onclick="__doPostBack('SaveAsPDF','');" />
                                                            <%--<input id="btnPDF2" type="button" value="Save as PDF2" onclick="__doPostBack('Savepdf','');" />--%>
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
<script type="text/javascript">
    HideCriteria();
    ShowSavedCookies();
    ShowRowsOnLoad();
    ShowNewRowsOnLoad();
    ComputeAccCost();
</script>