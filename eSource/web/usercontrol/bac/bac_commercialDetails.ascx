                                                            <%@ Control Language="C#" AutoEventWireup="true" CodeFile="bac_commercialDetails.ascx.cs" Inherits="web_usercontrol_bac_bac_commercialDetails" %>
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
                                                            <table width="100%" border="1" cellpadding="0" cellspacing="0" rules="all" class="itemDetails" id="Table1" style="border-color:#71A9D2; font-family: Arial; font-size: 11px; border-collapse: collapse; background-color: #F4F1C4; height:100%">
																<tbody>
																	<tr>
																		<td class="ui-widget-header" style="height:26px; vertical-align:middle;">
																			COMMERCIAL DETAILS
																		</td>
																	</tr>
																	<tr><td>
                                                                    <script type="text/javascript">
                                                                        $(window).resize(function () {
                                                                            $("#divContainer3").width($(window).width() - 310);
                                                                        });
                                                                        $(document).ready(function () {
                                                                            $("#divContainer3").width($(window).width() - 310);
                                                                        });
                                                                        </script>
																			<div style="overflow:auto; padding:0px" id="divContainer3">
                                                                                <div id="divContainer4" style="margin:0; ">
																		<asp:DataList ID="lstCommDetails" runat="server" DataSourceID="dsItems1" 
                                                                            DataKeyField="BidDetailNo" 
                                                                            Width="100%" >
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
																						            <asp:Label ID="txtVendorName" runat="server" Width="150" Text='<%# Eval("VendorName") %>' ReadOnly="true" Font-Bold="True" ></asp:Label>
                                                                                                    <asp:TextBox ID="txtBidTenderNo1" runat="server" Width="150" Text='<%# Eval("BidTenderNo") %>' Visible="false" ></asp:TextBox>
                                                                                                </td>
																					        </ItemTemplate>
																				        </asp:Repeater>
                                                                                        <asp:SqlDataSource ID="dsBidItemTendersDetails_Vendor" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>'
																	                        SelectCommand="SELECT VendorName, BidTenderNo FROM tblBACEvaluationDetails WHERE BidRefNo=@BidRefNo AND BidDetailNo=@BidDetailNo AND VendorName not in (select VendorName from tblBACEvaluationSummary where (TechCompliance=1 or CommCompliance=1 or ContCompliance=2) AND BidRefNo=@BidRefNo) ORDER BY Ranking, VendorName ASC" >
																	                        <SelectParameters>
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
																						            <asp:Label ID="txtQty" runat="server" Width="30" Text='<%# Eval("Qty") %>' ReadOnly="true" Font-Bold="True" ></asp:Label>
                                                                                                    <asp:TextBox ID="txtBidTenderNo1" runat="server" Width="150" Text='<%# Eval("BidTenderNo") %>' Visible="false" ></asp:TextBox>
                                                                                                </td>
																					        </ItemTemplate>
																				        </asp:Repeater>
                                                                                        <asp:SqlDataSource ID="dsBidItemTendersDetails_Qty" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>'
																	                        SelectCommand="SELECT BidTenderNo, Qty FROM tblBACEvaluationDetails WHERE BidRefNo=@BidRefNo AND BidDetailNo=@BidDetailNo AND VendorName not in (select VendorName from tblBACEvaluationSummary where (TechCompliance=1 or CommCompliance=1 or ContCompliance=2) AND BidRefNo=@BidRefNo) ORDER BY Ranking, VendorName ASC" >
																	                        <SelectParameters>
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
																						            <asp:Label ID="txtUnit" runat="server" Width="70" Text='<%# Eval("UnitMeasure") %>' ReadOnly="true" Font-Bold="True" ></asp:Label>
                                                                                                    <asp:TextBox ID="txtBidTenderNo1" runat="server" Width="150" Text='<%# Eval("BidTenderNo") %>' Visible="false" ></asp:TextBox>
                                                                                                </td>
																					        </ItemTemplate>
																				        </asp:Repeater>
                                                                                        <asp:SqlDataSource ID="dsBidItemTendersDetails_Unit" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>'
																	                        SelectCommand="SELECT BidTenderNo, UnitMeasure FROM tblBACEvaluationDetails WHERE BidRefNo=@BidRefNo AND BidDetailNo=@BidDetailNo AND VendorName not in (select VendorName from tblBACEvaluationSummary where (TechCompliance=1 or CommCompliance=1 or ContCompliance=2) AND BidRefNo=@BidRefNo) ORDER BY Ranking, VendorName ASC" >
																	                        <SelectParameters>
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
																						            <asp:Label ID="txtCurr" runat="server" Width="30" Text='<%# Eval("Currenzy") %>' ReadOnly="true" Font-Bold="True" ></asp:Label>
                                                                                                    <asp:TextBox ID="txtBidTenderNo1" runat="server" Width="150" Text='<%# Eval("BidTenderNo") %>' Visible="false" ></asp:TextBox>
                                                                                                </td>
																					        </ItemTemplate>
																				        </asp:Repeater>
                                                                                        <asp:SqlDataSource ID="dsBidItemTendersDetails_Curr" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>'
																	                       SelectCommand="SELECT BidTenderNo, Currenzy FROM tblBACEvaluationDetails WHERE BidRefNo=@BidRefNo AND BidDetailNo=@BidDetailNo AND VendorName not in (select VendorName from tblBACEvaluationSummary where (TechCompliance=1 or CommCompliance=1 or ContCompliance=2) AND BidRefNo=@BidRefNo) ORDER BY Ranking, VendorName ASC" >
																	                        <SelectParameters>
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
																						            <asp:Label ID="txtUnitCost" runat="server" Width="100" Text='<%# String.Format("{0:00,0.00}", Convert.ToDouble(Eval("UnitCost"))) %>' ReadOnly="true" Font-Bold="True"></asp:Label>
                                                                                                    <asp:TextBox ID="txtBidTenderNo1" runat="server" Width="150" Text='<%# Eval("BidTenderNo") %>' Visible="false" ></asp:TextBox>
                                                                                                </td>
																					        </ItemTemplate>
																				        </asp:Repeater>
                                                                                        <asp:SqlDataSource ID="dsBidItemTendersDetails_UnitCost" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>'
																	                       SelectCommand="SELECT BidTenderNo, CAST(UnitCost as varchar) UnitCost FROM tblBACEvaluationDetails WHERE BidRefNo=@BidRefNo AND BidDetailNo=@BidDetailNo AND VendorName not in (select VendorName from tblBACEvaluationSummary where (TechCompliance=1 or CommCompliance=1 or ContCompliance=2) AND BidRefNo=@BidRefNo) ORDER BY Ranking, VendorName ASC" >
																	                        <SelectParameters>
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
																						            <asp:Label ID="txtTotalCost" runat="server" Width="70" Text='<%# String.Format("{0:00,0.00}", Convert.ToDouble(Eval("TotalCost"))) %>' ReadOnly="true" Font-Bold="True"></asp:Label>
                                                                                                    <asp:TextBox ID="txtBidTenderNo1" runat="server" Width="150" Text='<%# Eval("BidTenderNo") %>' Visible="false" ></asp:TextBox>
                                                                                                </td>
																					        </ItemTemplate>
																				        </asp:Repeater>
                                                                                        <asp:SqlDataSource ID="dsBidItemTendersDetails_TotalCost" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>'
																	                       SelectCommand="SELECT BidTenderNo, TotalCost FROM tblBACEvaluationDetails WHERE BidRefNo=@BidRefNo AND BidDetailNo=@BidDetailNo AND VendorName not in (select VendorName from tblBACEvaluationSummary where (TechCompliance=1 or CommCompliance=1 or ContCompliance=2) AND BidRefNo=@BidRefNo) ORDER BY Ranking, VendorName ASC" >
																	                        <SelectParameters>
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
																						            <asp:Label ID="txtRanking" runat="server" Width="30" Text='<%# Eval("Ranking") %>' ReadOnly="true" ></asp:Label>
                                                                                                    <asp:TextBox ID="txtBidTenderNo1" runat="server" Width="150" Text='<%# Eval("BidTenderNo") %>' Visible="false" ></asp:TextBox>
                                                                                                </td>
																					        </ItemTemplate>
																				        </asp:Repeater>
                                                                                        <asp:SqlDataSource ID="dsBidItemTendersDetails_Ranking" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>'
																	                        SelectCommand="SELECT BidTenderNo, Ranking FROM tblBACEvaluationDetails WHERE BidRefNo=@BidRefNo AND BidDetailNo=@BidDetailNo AND VendorName not in (select VendorName from tblBACEvaluationSummary where (TechCompliance=1 or CommCompliance=1 or ContCompliance=2) AND BidRefNo=@BidRefNo) ORDER BY Ranking, VendorName ASC" >
																	                        <SelectParameters>
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
                                                                                                    <asp:CheckBox ID="chkAward" runat="server" Enabled="false" />
                                                                                                    <asp:TextBox ID="txtAward" runat="server" Width="150" Text='<%# Eval("Chkd") %>' Visible="true" ></asp:TextBox>
                                                                                                    <asp:TextBox ID="txtBidTenderNo1" runat="server" Width="150" Text='<%# Eval("BidTenderNo") %>' Visible="false" ></asp:TextBox>
                                                                                                </td>
																					        </ItemTemplate>
																				        </asp:Repeater>
                                                                                        <asp:SqlDataSource ID="dsBidItemTendersDetails_Award" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>'
																	                        SelectCommand="SELECT BidTenderNo, Chkd FROM tblBACEvaluationDetails WHERE BidRefNo=@BidRefNo AND BidDetailNo=@BidDetailNo AND VendorName not in (select VendorName from tblBACEvaluationSummary where (TechCompliance=1 or CommCompliance=1 or ContCompliance=2) AND BidRefNo=@BidRefNo) ORDER BY Ranking, VendorName ASC" >
																	                        <SelectParameters>
																		                        <asp:SessionParameter Name="BidRefNo" SessionField="BuyerBidForBac" Type="Int32" />
                                                                                                <asp:ControlParameter Name="BidDetailNo" ControlID="BidDetailNo1" Type="Int32" />
																	                        </SelectParameters>
																                        </asp:SqlDataSource>
                                                                                    </tr>
                                                                                </tbody></table>
                                                                            </ItemTemplate>
                                                                        </asp:DataList>
                                                                        </div></div>
																	</td></tr>
																</tbody>

                                                                <asp:SqlDataSource ID="dsItems1" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>'
																SelectCommand="SELECT DISTINCT BidDetailNo, ItemName, SKU, PR_No FROM tblBACEvaluationDetails WHERE BidRefNo = @BidRefNo ORDER BY BidDetailNo, ItemName, SKU, PR_No" >
																<SelectParameters>
																	<asp:SessionParameter Name="BidRefNo" SessionField="BuyerBidForBac" Type="Int32" />
																</SelectParameters>
															</asp:SqlDataSource>

															</table>