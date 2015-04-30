<%@ Page Language="C#" AutoEventWireup="true" CodeFile="bacendorsed.aspx.cs" Inherits="web_buyer_screens_bacendorsed" %>

<%@ Register Src="../usercontrol/commentlist.ascx" TagName="commentlist" TagPrefix="uc1" %>
<%@ Register TagPrefix="EBid" TagName="LeftNav" Src="~/WEB/usercontrol/Buyer/LeftNav.ascx" %>
<%@ Register TagPrefix="EBid" TagName="TopNavBids" Src="~/WEB/usercontrol/Buyer/TopNavBids.ascx" %>
<%@ Register TagPrefix="EBid" TagName="TopNav3" Src="~/WEB/usercontrol/Buyer/TopNav3.ascx" %>
<%@ Register TagPrefix="EBid" TagName="GlobalLinksNav" Src="~/web/usercontrol/GlobalLinksNav.ascx" %>
<%@ Register TagPrefix="EBid" TagName="Footer" Src="~/web/usercontrol/Footer.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml" style="height: 100%;">
<head>
<%@ Register TagPrefix="EBid" TagName="jqueryLoader" Src="../usercontrol/jQuery_loader.ascx" %>
    <EBid:jqueryLoader runat="server" ID="jqueryLoader1" />
    <title id="PageTitle" runat="server"></title>
    <meta http-equiv="Content-Language" content="en-us" />
    <meta http-equiv="Content-Type" content="text/html; charset=windows-1252" />
    <link rel="stylesheet" type="text/css" href='<%= "../themes/"+Session["configTheme"]+"/css/style.css" %>' />
    <link rel="stylesheet" type="text/css" href='<%="../themes/"+Session["configTheme"]+"/css/style_buyer.css" %>' />
</head>
<body style="height: 100%;">
    <div>
        <form runat="server">
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
                                    <EBid:TopNav3 runat="server" ID="TopNav3" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr valign="top">
                    <td valign="top">
                        <table border="0" cellpadding="0" cellspacing="0" width="100%" height="100%">
                            <tr>
                                <td id="relatedInfo">
                                    <div align="left">
                                        <EBid:LeftNav ID="LeftNav" runat="server" />
                                    </div>
                                    <br />
                                </td>
                                <td id="content">
                                    <div align="left">
                                        <table border="0" cellpadding="0" cellspacing="0" width="100%" id="table4">
                                            <tr>
                                                <td>
                                                    <h1>
                                                        <br />
                                                        Endorsed BAC
                                                    </h1>
                                                    <p>
                                                        These are your endorsed BAC. Click on the BAC reference number or the BAC event 
                                                        description to view the details.<br />
                                                        <!--<asp:CheckBox ID="chkShowEventsBeforeDeadline" runat="server" Text="Show only bid events before bid submission deadline" AutoPostBack="True" OnCheckedChanged="chkShowEventsBeforeDeadline_CheckedChanged" />-->
                                                    </p>
                                                    <asp:GridView ID="gvBids" runat="server" SkinID="BidEvents" AllowPaging="True" 
                                                        AllowSorting="True" AutoGenerateColumns="False" 
                                                        OnRowCommand="gvBids_RowCommand" DataKeyNames="BidRefNo" 
                                                        DataSourceID="dsAproved" Width="100%">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Reference No." InsertVisible="False" SortExpression="BidRefNo">
                                                                <HeaderStyle HorizontalAlign="Center" Width="90px" />
                                                                <ItemTemplate>
                                                                    &nbsp;<asp:LinkButton ID="lnkRefNo" runat="server" Text='<%# Bind("BidRefNo") %>' CommandArgument='<%# Bind("BACBID") %>' CommandName="Details"></asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Bid Event" SortExpression="ItemDesc">
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemTemplate>
                                                                    &nbsp;<asp:LinkButton ID="lnkDesc" runat="server" Text='<%# Bind("ItemDesc") %>' CommandArgument='<%# Bind("BACBID") %>' CommandName="Details"></asp:LinkButton><br />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Approving Status" >
                                                                <HeaderStyle HorizontalAlign="Center"  Width="230px" />
                                                                <ItemTemplate>
                                                                    <table cellpadding="2">
                                                                        <tr align="center">
                                                                            <td><%# (Eval("Approver_0").ToString() == "") ? "" : (Eval("ApprovedDt_0").ToString() != "" || Eval("ClarifyDt_1").ToString() != "" || Eval("ApprovedDt_1").ToString() != "" || Eval("ClarifyDt_2").ToString() != "" || Eval("ApprovedDt_2").ToString() != "" || Eval("ClarifyDt_3").ToString() != "" || Eval("ApprovedDt_3").ToString() != "" || Eval("ClarifyDt_4").ToString() != "" || Eval("ApprovedDt_4").ToString() != "" || Eval("ClarifyDt_5").ToString() != "" || Eval("ApprovedDt_5").ToString() != "" || Eval("ClarifyDt_6").ToString() != "" || Eval("ApprovedDt_6").ToString() != "" || Eval("ClarifyDt_7").ToString() != "" || Eval("ApprovedDt_7").ToString() != "" || Eval("ClarifyDt_8").ToString() != "" || Eval("ApprovedDt_8").ToString() != "" || Eval("ClarifyDt_9").ToString() != "" || Eval("ApprovedDt_9").ToString() != "" || Eval("ClarifyDt_10").ToString() != "" || Eval("ApprovedDt_10").ToString() != "") ? "<input id='Ck0' type='checkbox' disabled='disabled' checked='true'/>" : "<input id='Ck0' type='checkbox' disabled='disabled'/>"%></td>
                                                                            <td><%# (Eval("Approver_1").ToString() == "") ? "" : (Eval("ApprovedDt_1").ToString() != "" || Eval("ClarifyDt_2").ToString() != "" || Eval("ApprovedDt_2").ToString() != "" || Eval("ClarifyDt_3").ToString() != "" || Eval("ApprovedDt_3").ToString() != "" || Eval("ClarifyDt_4").ToString() != "" || Eval("ApprovedDt_4").ToString() != "" || Eval("ClarifyDt_5").ToString() != "" || Eval("ApprovedDt_5").ToString() != "" || Eval("ClarifyDt_6").ToString() != "" || Eval("ApprovedDt_6").ToString() != "" || Eval("ClarifyDt_7").ToString() != "" || Eval("ApprovedDt_7").ToString() != "" || Eval("ClarifyDt_8").ToString() != "" || Eval("ApprovedDt_8").ToString() != "" || Eval("ClarifyDt_9").ToString() != "" || Eval("ApprovedDt_9").ToString() != "" || Eval("ClarifyDt_10").ToString() != "" || Eval("ApprovedDt_10").ToString() != "") ? "<input id='Ck1' type='checkbox' disabled='disabled' checked='true'/>" : "<input id='Ck1' type='checkbox' disabled='disabled'/>"%></td>
                                                                            <td><%# (Eval("Approver_2").ToString() == "") ? "" : (Eval("ApprovedDt_2").ToString() != "" || Eval("ClarifyDt_3").ToString() != "" || Eval("ApprovedDt_3").ToString() != "" || Eval("ClarifyDt_4").ToString() != "" || Eval("ApprovedDt_4").ToString() != "" || Eval("ClarifyDt_5").ToString() != "" || Eval("ApprovedDt_5").ToString() != "" || Eval("ClarifyDt_6").ToString() != "" || Eval("ApprovedDt_6").ToString() != "" || Eval("ClarifyDt_7").ToString() != "" || Eval("ApprovedDt_7").ToString() != "" || Eval("ClarifyDt_8").ToString() != "" || Eval("ApprovedDt_8").ToString() != "" || Eval("ClarifyDt_9").ToString() != "" || Eval("ApprovedDt_9").ToString() != "" || Eval("ClarifyDt_10").ToString() != "" || Eval("ApprovedDt_10").ToString() != "") ? "<input id='Ck2' type='checkbox' disabled='disabled' checked='true'/>" : "<input id='Ck2' type='checkbox' disabled='disabled'/>"%></td>
                                                                            <td><%# (Eval("Approver_3").ToString() == "") ? "" : (Eval("ApprovedDt_3").ToString() != "" || Eval("ClarifyDt_4").ToString() != "" || Eval("ApprovedDt_4").ToString() != "" || Eval("ClarifyDt_5").ToString() != "" || Eval("ApprovedDt_5").ToString() != "" || Eval("ClarifyDt_6").ToString() != "" || Eval("ApprovedDt_6").ToString() != "" || Eval("ClarifyDt_7").ToString() != "" || Eval("ApprovedDt_7").ToString() != "" || Eval("ClarifyDt_8").ToString() != "" || Eval("ApprovedDt_8").ToString() != "" || Eval("ClarifyDt_9").ToString() != "" || Eval("ApprovedDt_9").ToString() != "" || Eval("ClarifyDt_10").ToString() != "" || Eval("ApprovedDt_10").ToString() != "") ? "<input id='Ck3' type='checkbox' disabled='disabled' checked='true'/>" : "<input id='Ck3' type='checkbox' disabled='disabled'/>"%></td>
                                                                            <td><%# (Eval("Approver_4").ToString() == "") ? "" : (Eval("ApprovedDt_4").ToString() != "" || Eval("ClarifyDt_5").ToString() != "" || Eval("ApprovedDt_5").ToString() != "" || Eval("ClarifyDt_6").ToString() != "" || Eval("ApprovedDt_6").ToString() != "" || Eval("ClarifyDt_7").ToString() != "" || Eval("ApprovedDt_7").ToString() != "" || Eval("ClarifyDt_8").ToString() != "" || Eval("ApprovedDt_8").ToString() != "" || Eval("ClarifyDt_9").ToString() != "" || Eval("ApprovedDt_9").ToString() != "" || Eval("ClarifyDt_10").ToString() != "" || Eval("ApprovedDt_10").ToString() != "") ? "<input id='Ck4' type='checkbox' disabled='disabled' checked='true'/>" : "<input id='Ck4' type='checkbox' disabled='disabled'/>"%></td>
                                                                            <td><%# (Eval("Approver_5").ToString() == "") ? "" : (Eval("ClarifyDt_5").ToString() != "" || Eval("ApprovedDt_5").ToString() != "" || Eval("ClarifyDt_6").ToString() != "" || Eval("ApprovedDt_6").ToString() != "" || Eval("ClarifyDt_7").ToString() != "" || Eval("ApprovedDt_7").ToString() != "" || Eval("ClarifyDt_8").ToString() != "" || Eval("ApprovedDt_8").ToString() != "" || Eval("ClarifyDt_9").ToString() != "" || Eval("ApprovedDt_9").ToString() != "" || Eval("ClarifyDt_10").ToString() != "" || Eval("ApprovedDt_10").ToString() != "") ? "<input id='Ck5' type='checkbox' disabled='disabled' checked='true'/>" : "<input id='Ck5' type='checkbox' disabled='disabled'/>"%></td>
                                                                            <td><%# (Eval("Approver_6").ToString() == "") ? "" : (Eval("ApprovedDt_6").ToString() != "" || Eval("ClarifyDt_7").ToString() != "" || Eval("ApprovedDt_7").ToString() != "" || Eval("ClarifyDt_8").ToString() != "" || Eval("ApprovedDt_8").ToString() != "" || Eval("ClarifyDt_9").ToString() != "" || Eval("ApprovedDt_9").ToString() != "" || Eval("ClarifyDt_10").ToString() != "" || Eval("ApprovedDt_10").ToString() != "") ? "<input id='Ck6' type='checkbox' disabled='disabled' checked='true'/>" : "<input id='Ck6' type='checkbox' disabled='disabled'/>"%></td>
                                                                            <td><%# (Eval("Approver_7").ToString() == "") ? "" : (Eval("ApprovedDt_7").ToString() != "" || Eval("ClarifyDt_8").ToString() != "" || Eval("ApprovedDt_8").ToString() != "" || Eval("ClarifyDt_9").ToString() != "" || Eval("ApprovedDt_9").ToString() != "" || Eval("ClarifyDt_10").ToString() != "" || Eval("ApprovedDt_10").ToString() != "") ? "<input id='Ck7' type='checkbox' disabled='disabled' checked='true'/>" : "<input id='Ck7' type='checkbox' disabled='disabled'/>"%></td>
                                                                            <td><%# (Eval("Approver_8").ToString() == "") ? "" : (Eval("ApprovedDt_8").ToString() != "" || Eval("ClarifyDt_9").ToString() != "" || Eval("ApprovedDt_9").ToString() != "" || Eval("ClarifyDt_10").ToString() != "" || Eval("ApprovedDt_10").ToString() != "") ? "<input id='Ck8' type='checkbox' disabled='disabled' checked='true'/>" : "<input id='Ck8' type='checkbox' disabled='disabled'/>"%></td>
                                                                            <td><%# (Eval("Approver_9").ToString() == "") ? "" : (Eval("ApprovedDt_9").ToString() != "" || Eval("ClarifyDt_10").ToString() != "" || Eval("ApprovedDt_10").ToString() != "") ? "<input id='Ck9' type='checkbox' disabled='disabled' checked='true'/>" : "<input id='Ck9' type='checkbox' disabled='disabled'/>"%></td>
                                                                            <td><%# (Eval("Approver_10").ToString() == "") ? "" : (Eval("ApprovedDt_10").ToString() != "") ? "<input id='Ck10' type='checkbox' disabled='disabled' checked='true'/>" : "<input id='Ck10' type='checkbox' disabled='disabled'/>"%></td>
                                                                        </tr>
                                                                        <tr style='font-size:10px;' align="center">
                                                                            <td><%# (Eval("Approver_0").ToString() != "") ? Eval("Approver_0_n").ToString() : ""%></td>
                                                                            <td><%# (Eval("Approver_1").ToString() != "") ? Eval("Approver_1_n").ToString() : ""%></td>
                                                                            <td><%# (Eval("Approver_2").ToString() != "") ? Eval("Approver_2_n").ToString() : ""%></td>
                                                                            <td><%# (Eval("Approver_3").ToString() != "") ? Eval("Approver_3_n").ToString() : ""%></td>
                                                                            <td><%# (Eval("Approver_4").ToString() != "") ? Eval("Approver_4_n").ToString() : ""%></td>
                                                                            <td><%# (Eval("Approver_5").ToString() != "") ? Eval("Approver_5_n").ToString() : ""%></td>
                                                                            <td><%# (Eval("Approver_6").ToString() != "") ? Eval("Approver_6_n").ToString() : ""%></td>
                                                                            <td><%# (Eval("Approver_7").ToString() != "") ? Eval("Approver_7_n").ToString() : ""%></td>
                                                                            <td><%# (Eval("Approver_8").ToString() != "") ? Eval("Approver_8_n").ToString() : ""%></td>
                                                                            <td><%# (Eval("Approver_9").ToString() != "") ? Eval("Approver_9_n").ToString() : ""%></td>
                                                                            <td><%# (Eval("Approver_10").ToString() != "") ? Eval("Approver_10_n").ToString() : ""%></td>
                                                                        </tr>
                                                                    </table>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Date Submitted" SortExpression="DateSubmitted">
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="Center" Width="180px" />
                                                                <ItemTemplate>
                                                                    &nbsp;<asp:Label ID="Label3" runat="server" Text='<%# Bind("DateSubmitted", "{0:D}<br />{0:T}") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                    <asp:SqlDataSource ID="dsAproved" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>'
                                                        SelectCommand="SELECT t1.*, CONVERT(VARCHAR, t1.BidRefNo)+';'+CONVERT(VARCHAR, t1.BacRefNo) AS BACBID,
                                                                              substring(v0.FirstName,1,1) + substring(v0.MiddleName,1,1) + substring(v0.LastName,1,1) approver_0_n, 
                                                                              v1.initials approver_1_n, v2.initials approver_2_n, v3.initials approver_3_n,
                                                                              v4.initials approver_4_n, v5.initials approver_5_n, v6.initials approver_6_n,
                                                                              v7.initials approver_7_n, v8.initials approver_8_n, v9.initials approver_9_n,
                                                                              v10.initials approver_10_n
                                                                       FROM tblBacBidItems t1 LEFT OUTER JOIN tblPurchasing v0 ON t1.approver_0 = v0.PurchasingId
                                                                                              LEFT OUTER JOIN vwApprovers v1 ON t1.approver_1 = v1.bacid
                                                                                              LEFT OUTER JOIN vwApprovers v2 ON t1.approver_2 = v2.bacid 
                                                                                              LEFT OUTER JOIN vwApprovers v3 ON t1.approver_3 = v3.bacid 
                                                                                              LEFT OUTER JOIN vwApprovers v4 ON t1.approver_4 = v4.bacid 
                                                                                              LEFT OUTER JOIN vwApprovers v5 ON t1.approver_5 = v5.bacid 
                                                                                              LEFT OUTER JOIN vwApprovers v6 ON t1.approver_6 = v6.bacid 
                                                                                              LEFT OUTER JOIN vwApprovers v7 ON t1.approver_7 = v7.bacid 
                                                                                              LEFT OUTER JOIN vwApprovers v8 ON t1.approver_8 = v8.bacid 
                                                                                              LEFT OUTER JOIN vwApprovers v9 ON t1.approver_9 = v9.bacid 
                                                                                              LEFT OUTER JOIN vwApprovers v10 ON t1.approver_10 = v10.bacid 
                                                                       WHERE BuyerID=@BuyerID AND Status=@Status  ORDER BY DateSubmitted DESC"  SelectCommandType="Text">
                                                        <SelectParameters>
                                                            <asp:SessionParameter Name="BuyerId" SessionField="UserId" Type="Int32" />
                                                            <asp:Parameter Name="BidRefNo" DefaultValue="0" Type="Int32" />
                                                            <asp:Parameter Name="Status" DefaultValue="1" Type="Int32" />
                                                        </SelectParameters>
                                                    </asp:SqlDataSource>
                                                    <br />
                                                    <table border="0" cellpadding="0" cellspacing="0" width="100%" id="actions">
                                                        <tr>
                                                            <td>&nbsp;
                                                                
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <p>&nbsp;
                                                        </p>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td id="footer" height="50px">
                        <EBid:Footer runat="server" ID="Footer" />
                        <input type="hidden" id="hdnUserId" name="hdnUserId" runat="server" />
                    </td>
                </tr>
            </table>
        </form>
    </div>
</body>
</html>
