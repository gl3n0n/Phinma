<%@ Page Language="C#" AutoEventWireup="true" CodeFile="eventtenderscomparison.aspx.cs" Inherits="web_reports_eventtenderscomparison" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<html xmlns="http://www.w3.org/1999/xhtml" style="height: 100%;">
<head id="Head1" runat="server">
    <title id="PageTitle" runat="server"></title>
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css' rel="stylesheet" type="text/css" />        
    <script type="text/javascript">
    //This code is the beginning of the right click disable
    function right(e) 
    {
        //This function is for Net 4.
        if (navigator.appName == 'Netscape' && (e.which == 3 || e.which == 2))
        {
            return false;
        }
        return true;
    }

    function context()
    {
        //This function takes care of Net 6 and IE.
        return false;
    }

    document.onmousedown = right;
    document.oncontextmenu = context;
    </script>
</head>
<body style="height: 100%;">
    <form id="form1" runat="server">
        <table ID="Table1" border="0" cellpadding="0" cellspacing="0" Width="100%" height="100%">
            <tr>
                <td>
                    <asp:CheckBox ID="chkShowVendorName" runat="server" AutoPostBack="true" Text="Hide Vendor Name"
                        Font-Names="Arial" Font-Size="11px" OnCheckedChanged="chkShowVendorName_CheckedChanged" />
                </td>
            </tr>                        
            <tr>
                <td valign="top" align="center">
                    <table border="0" cellpadding="0" cellspacing="4" width="308px">
                        <tr>
                            <td align="right">
                                <asp:LinkButton ID="lnkExportToPdf" runat="server" Font-Names="Arial" Font-Size="11px" Width="100px" OnClick="lnkExportToPdf_Click">Export To PDF</asp:LinkButton>
                            </td>
                            <td align="center" style="width: 4px;">
                                |
                            </td>
                            <td align="center">
                                <asp:LinkButton ID="lnkRefresh" runat="server" Font-Names="Arial" Font-Size="11px" Width="100px" OnClick="lnkRefresh_Click">Refresh</asp:LinkButton>
                            </td>
                            <td align="center" style="width: 4px;">
                                |
                            </td>
                            <td align="left">
                                <asp:LinkButton ID="lnkExportToExcel" runat="server" Font-Names="Arial" Font-Size="11px" Width="100px" OnClick="lnkExportToExcel_Click">Export To Excel</asp:LinkButton>
                            </td>
                        </tr>
                    </table>                    
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
                            <asp:QueryStringParameter DefaultValue="0" Name="BidRefNo" QueryStringField="brn" Type="Int32" /> 
                            <asp:ControlParameter ControlID="chkShowVendorName" DefaultValue="0" Name="UseAlias"
                                PropertyName="Checked" Type="Int32" />      
                            <%--<asp:SessionParameter SessionField="ClientId" Name="ClientId" Type="Int32" />--%>
                        </SelectParameters>
                    </asp:ObjectDataSource>
                </td>
            </tr>
        </table>
    </form>    
</body>
</html>
