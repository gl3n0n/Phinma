<%@ Page Language="C#" AutoEventWireup="true" CodeFile="bideventtenders.aspx.cs" Inherits="web_buyerscreens_bideventtenders" MaintainScrollPositionOnPostback="true" EnableViewState="true" %>

<%@ Reference Control="~/web/usercontrol/commentlist_tender.ascx" %>
<%@ Register Src="../usercontrol/bids/bidtender_attachments.ascx" TagName="bidtender_attachments" TagPrefix="uc3" %>
<%@ Register Src="../usercontrol/bids/bidtenderdetails.ascx" TagName="bidtenderdetails" TagPrefix="uc1" %>
<%@ Register Src="../usercontrol/bids/biditemdetails.ascx" TagName="biditemdetails" TagPrefix="uc2" %>
<%@ Register TagPrefix="EBid" TagName="GlobalLinksNav" Src="~/web/usercontrol/GlobalLinksNav.ascx" %>
<%@ Register TagPrefix="EBid" TagName="TopDate" Src="~/web/usercontrol/TopDate.ascx" %>
<%@ Register TagPrefix="EBid" TagName="LeftNav" Src="~/WEB/usercontrol/Buyer/LeftNav.ascx" %>
<%@ Register TagPrefix="EBid" TagName="TopNavBids" Src="~/WEB/usercontrol/Buyer/TopNavBids.ascx" %>
<%@ Register TagPrefix="EBid" TagName="TopNav2" Src="~/WEB/usercontrol/Buyer/TopNav2.ascx" %>
<%@ Register TagPrefix="EBid" TagName="Footer" Src="~/web/usercontrol/Footer.ascx" %>
<%@ Register src="../usercontrol/bids/biddetails_wo_suppliers_new.ascx" tagname="biddetails_wo_suppliers" tagprefix="uc3" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<html xmlns="http://www.w3.org/1999/xhtml" style="height: 100%;">
<head>
    <%@ Register TagPrefix="EBid" TagName="jqueryLoader" Src="../usercontrol/jQuery_loader.ascx" %>
    <EBid:jqueryLoader runat="server" ID="jqueryLoader1" />
    <title id="PageTitle" runat="server"></title>
    <meta http-equiv="Content-Language" content="en-us" />
    <meta http-equiv="Content-Type" content="text/html; charset=windows-1252" />
    <link rel="stylesheet" type="text/css" href='<%= "../themes/"+Session["configTheme"]+"/css/style.css" %>' />
    <link rel="stylesheet" type="text/css" href='<%="../themes/"+Session["configTheme"]+"/css/style_buyer.css" %>' />
    <script type="text/javascript">
        function Calculate(id) {
            var VendorIds = new Array(<%= getVendorIds() %>);
            for (var i in VendorIds)
            {
                    var totalCosts = 0;
                    var subtotalCosts = 0;
                    var addedCosts = 0;

                    subtotalCosts = $(".subtotal" + VendorIds[i]).html()
                    subtotalCosts_r = subtotalCosts.split(" ");
                    subtotalCosts = subtotalCosts_r[1].replace(/,/g, '');
                    //alert(subtotalCosts);

                    $("." + VendorIds[i]).each(function () {
                        if ($("#" + this.id).attr("type") == "radio" && $("#" + this.id).val() == VendorIds[i])
                        {
                            if ($("#" + this.id).attr("checked") == true)
                            {
                                //subtotalCosts = parseFloat(subtotalCosts) + parseFloat($("#" + this.id).attr("amount"));
                                //totalCosts = parseFloat(totalCosts) + parseFloat($("#" + this.id).attr("amount"));
                            }
                            else
                            {
                                //alert($("#" + this.id).attr("checked"));
                                //subtotalCosts = parseFloat(subtotalCosts);
                                //totalCosts = parseFloat(totalCosts);
                            }
                            //totalCosts = parseFloat(totalCosts) + parseFloat($(".subtotal" + VendorIds[i]).html());
                        }
                        else
                        {
                            addedCosts = parseFloat(addedCosts) + parseFloat($("#" + this.id).val().replace(/,/g, ''));
                            //totalCosts = parseFloat(totalCosts) + parseFloat(addedCosts) + parseFloat($(".subtotal" + VendorIds[i]).html());
                        }
                    });
                    totalCosts = parseFloat(subtotalCosts) + parseFloat(addedCosts);
                    //alert(addedCosts);
                    //alert(subtotalCosts);
                    //alert(totalCosts);
                    //$(".subtotal" + VendorIds[i]).html(subtotalCosts);
                    //$(".total" + VendorIds[i]).html("PHP " + Math.round(totalCosts * 100)/100);
                    //$(".total" + VendorIds[i]).html("PHP " + totalCosts);
            }
            //alert(parseFloat('1,212,121.23'.replace(/,/g, '')));

        }

        $(document).ready(function () {
            Calculate();
            var VendorIds = new Array(<%= getVendorIds() %>);
            for (var i in VendorIds) {
                $("." + VendorIds[i]).each(function () {
                    if ($("#" + this.id).attr("type") == "radio") {
                        //$("#" + this.id).click(function () { Calculate(this.id); Calculate(this.id); });
                    }
                    else {

                        $("#" + this.id).blur(function () { Calculate(this.id); Calculate(this.id); });
                    }
                });
            }
        });
    </script>
</head>
<body style="height: 100%;">
    <div align="left">
        <form id="Form1" runat="server">
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
                                    <EBid:TopNav2 runat="server" ID="TopNav2" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table border="0" cellpadding="0" cellspacing="0" width="100%" height="100%">
                            <tr>
                                <td id="relatedInfo" style="width: 250px">
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                        <tr>
                                            <td id="Td1">
                                                <div align="left">
                                                    <EBid:LeftNav runat="server" ID="LeftNav" />
                                                </div>
                                                <h2>
                                                    Comments</h2>
                                                <div align="left">
                                                    <asp:PlaceHolder ID="phComments" runat="server"></asp:PlaceHolder>
                                                </div>
                                                <p>
                                                    &nbsp;</p>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td id="content" >
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%" id="table5">
                                        <tr>
                                            <td id="content0">
                                                <h1>
                                                    <br />
                                                    Bid Tender Details</h1>
                                                <p>
                                                    These are the submitted tenders for this bid item.
                                                    <br />
<uc3:biddetails_wo_suppliers ID="Biddetails_wo_suppliers1" runat="server" />


                                                <p>


                                                <asp:GridView runat="server" ID="GvTenders"  SkinID="AuctionedItems"  DataSourceID="dsTendersbyBidEvent" OnRowDataBound="GvTenders_RowDataBound" EnableModelValidation="false" CellPadding="5">
                                                            <SelectedRowStyle BackColor="#50A4D1" />
                                                </asp:GridView>
                                                
<%--<asp:GridView runat="server" ID="GvTendersAddedCosts"  SkinID="AuctionedItems"  Visible="false"  DataSourceID="dsTendersAddedCosts"  EnableModelValidation="false" CellPadding="5"> 
                                                            <SelectedRowStyle BackColor="#50A4D1" />
                                                </asp:GridView>--%>


                                                    &nbsp;<p>
                                                        
                                                    <table border="0" cellpadding="0" cellspacing="0" width="100%" id="actions1">
                                                        <tr>
                                                            <td style="height: 34px">
                                                       <asp:LinkButton ID="lnkSave0" runat="server" OnClick="lnkSave_Click" Width="100px">Compute</asp:LinkButton>
                                                                </td>
                                                            </tr>
                                                        </table>


                                                        <asp:SqlDataSource ID="dsTendersbyBidEvent" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>' SelectCommand="sp_GetBidTendersByVendors" SelectCommandType="StoredProcedure">
                                                            <SelectParameters>
                                                                <asp:SessionParameter DefaultValue="0" Name="BidRefNo" SessionField="BidRefNo" Type="Int32" />
                                                            </SelectParameters>
                                                        </asp:SqlDataSource>   

                                                        <%--<asp:SqlDataSource ID="dsTendersAddedCosts" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>' SelectCommand="sp_GetBidTendersAddedCostsByVendors" SelectCommandType="StoredProcedure">
                                                            <SelectParameters>
                                                                <asp:SessionParameter DefaultValue="0" Name="BidRefNo" SessionField="BidRefNo" Type="Int32" />
                                                            </SelectParameters>
                                                        </asp:SqlDataSource> --%> 



                                                <div align="left">
                                                    <p>
                                                        <asp:SqlDataSource ID="dsBidItemTenders" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>' SelectCommand="sp_GetBidItemTenders" SelectCommandType="StoredProcedure">
                                                            <SelectParameters>
                                                                <asp:SessionParameter DefaultValue="0" Name="BidDetailNo" SessionField="BidDetailNo" Type="Int32" />
                                                            </SelectParameters>
                                                        </asp:SqlDataSource>

                  <%--                                      
        <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AutoGenerateColumns="False"
            EmptyDataText="There are no data records to display.">
            <Columns>
                <asp:BoundField DataField="VendorName" HeaderText="VendorName" InsertVisible="False" ReadOnly="True"
                    SortExpression="VendorName" />
                <asp:BoundField DataField="VendorEmail" HeaderText="VendorEmail" />
                <asp:BoundField DataField="MobileNo" HeaderText="MobileNo" />
                <asp:BoundField DataField="VendorAddress" HeaderText="VendorAddress" />
            </Columns>
        </asp:GridView>--%>
<div style="display:none">
                             <asp:LinkButton ID="lnkExportToExcel" runat="server" Font-Names="Arial" Font-Size="11px" Width="100px" OnClick="lnkExportToExcel_Click">Export To Excel</asp:LinkButton>                                           
                    <rsweb:ReportViewer ID="rvBidEventTendersComparisons" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="1140px" Height="780px" BackColor="WhiteSmoke" BorderWidth="1px"
                        ShowDocumentMapButton="False" ShowExportControls="False" ShowFindControls="False" ShowRefreshButton="False" ShowZoomControl="False" SizeToReportContent="True">
                        <LocalReport>
                            <DataSources>
                                <rsweb:ReportDataSource DataSourceId="ObjectDataSource1" Name="dsBidEventTenderComparisons_sp_GetEventTendersComparison" />
                            </DataSources>
                        </LocalReport>
                    </rsweb:ReportViewer>
                    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetData" TypeName="dsBidEventTenderComparisonsTableAdapters.sp_GetEventTendersComparisonTableAdapter" OldValuesParameterFormatString="original_{0}">
                        <SelectParameters>
                            <asp:SessionParameter DefaultValue="0" Name="BidRefNo" SessionField="BidRefNo" Type="Int32" />   
                            <asp:Parameter DefaultValue="0" Name="UseAlias" Type="Int32" />
                            <asp:SessionParameter SessionField="ClientId" Name="ClientId" Type="Int32" />
                        </SelectParameters>
                    </asp:ObjectDataSource>        
   
                </div>                                                     
                   
<%--<div style="display:none">                                     
                                                        
                                                         
                             <asp:LinkButton ID="LinkButton2" runat="server" Font-Names="Arial" Font-Size="11px" Width="100px" OnClick="lnkExportToExcel2_Click">Export To Excel</asp:LinkButton>                                           
                    <rsweb:ReportViewer ID="rvBidTendersForPOFileImport" runat="server" Font-Names="Verdana" Font-Size="8pt" Width="1140px" Height="780px" BackColor="WhiteSmoke" BorderWidth="1px" AsyncRendering="false"
                        ShowDocumentMapButton="False" ShowExportControls="False" ShowFindControls="False" ShowRefreshButton="False" ShowZoomControl="False" SizeToReportContent="True">
                        <LocalReport>
                            <DataSources>
                                <rsweb:ReportDataSource DataSourceId="ObjectDataSource2" Name="dsBidTendersForPOFileImport_sp_GetBidTendersForPOFileImport" />
                            </DataSources>
                        </LocalReport>
                    </rsweb:ReportViewer>                         

                    <asp:ObjectDatasource id="objectdatasource2" runat="server" selectmethod="getdata" typename="dsbidtendersforpofileimporttableadapters.sp_getbidtendersforpofileimporttableadapter" oldvaluesparameterformatstring="original_{0}">
                        <selectparameters>
                            <asp:sessionparameter defaultvalue="0" name="bidrefno" sessionfield="bidrefno" type="int32" /> 
                        </selectparameters>
                    </asp:objectdatasource>


                </div>--%>






                                                    </p>
                                                    <center>
                                                        <asp:Label ID="lblMessage" runat="server" Text="" ForeColor="red" Font-Size="11px"></asp:Label></center>
                                                    <p>
                                                        &nbsp;<asp:Panel runat="server" ID="pnlBidTenderAttachments" Visible=false>     
                                                          <uc3:bidtender_attachments ID="Bidtender_attachments1" runat="server" />
                                                        </asp:Panel>
                                                    </p>
                                                    <br />
                                                    <table border="0" cellpadding="0" cellspacing="0" width="100%" id="actions">
                                                        <tr>
                                                            <td style="height: 34px">
                                                                &nbsp;
                                                                
<script language="javascript" type="text/javascript">
    function confirmIt() {
        if (confirm("Are you sure you want to endorse?")) {
            //alert("confirmed");
            return true;
        } else {
            //alert("not confirmed");
            return false;
        }
    }
    </script>                                                                  
                                                                <%--<asp:LinkButton ID="LinkButton0" runat="server" OnClick="btnExportToExcel_Click" Width="1px" Visible="false">.</asp:LinkButton>--%>
                                                                <%--<asp:LinkButton ID="lnkSave" runat="server" OnClick="lnkSave_Click" Width="100px" Visible="false">Endorse</asp:LinkButton>--%>
                                                                <asp:LinkButton ID="lnkEndorse" runat="server" OnClick="lnkEndorse_Click" OnclientClick="return confirmIt()" Width="120px">Endorse</asp:LinkButton>
                                                                <%--<asp:LinkButton ID="LinkButton1" runat="server" OnClick="lnkExportToExcel2_Click" Width="150px">PO Import File</asp:LinkButton>--%>
                                                                <%--<asp:LinkButton ID="lnkComparison" runat="server" OnClick="lnkExportToExcel_Click" Width="100px" Visible="false">Canvass Sheet</asp:LinkButton>--%>
                                                                <asp:LinkButton ID="LinkButton3" runat="server" OnClick="lnkExportToPdf_Click" Width="150px">Canvass Sheet (PDF)</asp:LinkButton>
                                                                <%--<asp:HyperLink ID="lnkComparison" runat="server" Width="120px">Canvass Sheet</asp:HyperLink>--%>
                                                                <asp:LinkButton ID="lnkBack" runat="server" OnClick="lnkBack_Click" Width="100px">Back</asp:LinkButton>
                                                                <asp:LinkButton ID="lnkDownloadAll" runat="server" Width="160px" onclick="lnkDownloadAll_Click">Download All Attachments</asp:LinkButton>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <br />
                                                    <br />
                                                </div>
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
        </form>
    </div>
</body>
</html>
