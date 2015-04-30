<%@ Page Language="C#" AutoEventWireup="true" CodeFile="bacforclarifications.aspx.cs" Inherits="web_bac_bacforclarifications" %>
<%@ Register TagPrefix="EBid" TagName="TopDate" Src="~/WEB/usercontrol/TopDate.ascx" %>
<%@ Register TagPrefix="EBid" TagName="GlobalLinksNav" Src="~/web/usercontrol/GlobalLinksNav.ascx" %>
<%@ Register TagPrefix="EBid" TagName="BAC_TopNav_Bids" Src="~/web/usercontrol/Bac/BAC_TopNav_Bids.ascx" %>
<%@ Register TagPrefix="EBid" TagName="BAC_LeftNav" Src="~/web/usercontrol/Bac/BAC_LeftNav_Bids.ascx" %>
<%@ Register TagPrefix="EBid" TagName="Footer" Src="~/web/usercontrol/Footer.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml" style="height: 100%;">
<head>    
    <title id="PageTitle" runat="server"></title>
    <meta http-equiv="Content-Language" content="en-us" />
    <meta http-equiv="Content-Type" content="text/html; charset=windows-1252" />
    <link rel="stylesheet" type="text/css" href='<%= "../themes/"+Session["configTheme"]+"/css/style_ph.css" %>' />
    <link href='<%= "../themes/"+Session["configTheme"]+"/css/style.css" %>' rel="stylesheet" type="text/css" />
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
                                    <ebid:BAC_TopNav_Bids runat="server" ID="BAC_LeftNav_Bids1" />
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
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                        <tr>
                                            <td>
                                                <h2>
                                                    Bid Events</h2>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <EBid:BAC_LeftNav runat="server" ID="BAC_LeftNav1" />
                                                <p>
                                                    &nbsp;</p>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td id="content">
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%" id="page0">
                                        <tr>
                                            <td valign="top">
                                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                    <tr>
                                                        <td id="content0">
                                                            <h1>
                                                                <br />
                                                                BAC for Clarification</h1>
                                                            <br />
                                                            <asp:GridView ID="gvBids" runat="server" SkinID="BidEvents" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" OnRowCommand="gvBids_RowCommand" DataKeyNames="BidRefNo" DataSourceID="dsAproved">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="Reference No." InsertVisible="False" SortExpression="BidRefNo">
                                                                        <HeaderStyle HorizontalAlign="Center" Width="90px" />
                                                                        <ItemTemplate>
                                                                            &nbsp;<asp:LinkButton ID="lnkRefNo" runat="server" Text='<%# Bind("BidRefNo") %>' CommandArgument='<%# Eval("BACBID") %>' CommandName="Details"></asp:LinkButton>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Bid Event" SortExpression="ItemDesc">
                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                        <ItemTemplate>
                                                                            &nbsp;<asp:LinkButton ID="lnkDesc" runat="server" Text='<%# Bind("ItemDesc") %>' CommandArgument='<%# Eval("BACBID") %>' CommandName="Details"></asp:LinkButton><br />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Date Submitted" SortExpression="PreparedDt">
                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                        <ItemStyle HorizontalAlign="Center" Width="180px" />
                                                                        <ItemTemplate>
                                                                            &nbsp;<asp:Label ID="Label3" runat="server" Text='<%# Bind("PreparedDt", "{0:D}<br />{0:T}") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                            </asp:GridView>
                                                            <asp:SqlDataSource ID="dsAproved" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>' 
                                                            SelectCommand="SELECT DISTINCT b.BacRefNo, b.BidRefNo, b.ItemDesc, b.DateSubmitted PreparedDt, b.ClarifyDt_1, c.ToUserId, CONVERT(VARCHAR, b.BidRefNo)+';'+CONVERT(VARCHAR, b.BacRefNo)+';'+CONVERT(VARCHAR, b.BuyerId)+';'+CONVERT(VARCHAR, c.FrUserId) AS BACBID FROM tblBacBidItems b
                                                            INNER JOIN (
                                                            SELECT * FROM tblBACClarifications d WHERE d.DatePosted = (SELECT MAX(e.DatePosted) from tblBACClarifications e WHERE e.BidRefNo = d.BidRefNo))
                                                            c ON b.BidRefNo=c.BidRefNo AND c.ToUserId=@UserId
                                                            WHERE b.Status=@Status ORDER BY b.DateSubmitted DESC"
                                                        SelectCommandType="Text">
                                                        <SelectParameters>
                                                            <asp:SessionParameter Name="UserId" SessionField="UserId" Type="Int32" />
                                                            <asp:Parameter Name="Status" DefaultValue="2" Type="Int32" />
                                                        </SelectParameters>
                                                    </asp:SqlDataSource>
                                                            <br />
                                                        </td>
                                                    </tr>
                                                </table>
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