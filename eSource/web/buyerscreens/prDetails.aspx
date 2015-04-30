<%@ Page Language="C#" AutoEventWireup="true" CodeFile="prDetails.aspx.cs" Inherits="web_buyerscreens_prDetails" %>

<%@ Register TagPrefix="EBid" TagName="LeftNav" Src="~/WEB/usercontrol/Buyer/LeftNav.ascx" %>
<%@ Register TagPrefix="EBid" TagName="TopNavBids" Src="~/WEB/usercontrol/Buyer/TopNavBids.ascx" %>
<%@ Register TagPrefix="EBid" TagName="TopNav2" Src="~/WEB/usercontrol/Buyer/TopNav2.ascx" %>
<%@ Register TagPrefix="EBid" TagName="GlobalLinksNav" Src="~/web/usercontrol/GlobalLinksNav.ascx" %>
<%@ Register TagPrefix="EBid" TagName="Footer" Src="~/web/usercontrol/Footer.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<%@ Register TagPrefix="EBid" TagName="jqueryLoader" Src="../usercontrol/jQuery_loader.ascx" %>
    <EBid:jqueryLoader runat="server" ID="jqueryLoader1" />
    <title id="PageTitle" runat="server"></title>
    <meta http-equiv="Content-Language" content="en-us" />
    <meta http-equiv="Content-Type" content="text/html; charset=windows-1252" />
    <link rel="stylesheet" type="text/css" href='<%= "../themes/"+Session["configTheme"]+"/css/style.css" %>' />
    <link rel="stylesheet" type="text/css" href='<%="../themes/"+Session["configTheme"]+"/css/style_buyer.css" %>' />
    <style type="text/css">
        .auto-style2 {
            width: 134px;
            height: 712px;
        }
        .auto-style3 {
            height: 712px;
        }
        .auto-style4 {
            height: 19px;
        }
    </style>
</head>
<body>
    <div>
        <form id="form1" runat="server">
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
                                    <EBid:TopNav2 ID="TopNav2" runat="server" />
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
                                    <p>
                                        &nbsp</p>
                                </td>
                                <td id="content">
                                    <div align="left">
                                        <table border="0" cellpadding="0" cellspacing="0" width="100%" id="table4">
                                        <tr>    
                                            <td>
                                                <h1 style="margin-top:0px;">
                                                    <br />
                                                    PR Details
                                                </h1>
                                                <p>
                                                    <%--To edit the details of this PR, make the necessary changes and click "Save Changes"--%>
                                                    
                                                </p>
                                                <asp:DetailsView ID="DetailsView1" runat="server" AutoGenerateRows="False" DataSourceID="dsPRDetails" 
                                                    HeaderText="Details" SkinID="BidDetails">
                                                    <Fields>
                                                        <asp:TemplateField HeaderText="PR No" InsertVisible="False" SortExpression="PRNo">
                                                            <HeaderStyle Width="133px" />
                                                            <ItemStyle BackColor="white" />
                                                            <ItemTemplate>
                                                                <asp:HiddenField ID="PRNo" runat="server" Value='<%# Bind("PRNo") %>' />
                                                                <asp:Label ID="lblPRNo" runat="server" ForeColor="Black" Text='<%# Bind("RQN") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="PR Line No" SortExpression="PR Line No">
                                                            <HeaderStyle Width="133px" />
                                                            <ItemStyle BackColor="white" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPRLineNo" runat="server" ForeColor="Black" Text='<%# Bind("PRLineNo") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="PR Date" SortExpression="PRDate">
                                                            <HeaderStyle Width="133px" />
                                                            <ItemStyle BackColor="white" />
                                                            <ItemTemplate>
                                                               <asp:Label ID="lblPRDate" runat="server" ForeColor="Black" Text='<%# Bind("PRDate") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Item Code" SortExpression="ItemCode">
                                                            <HeaderStyle Width="133px" />
                                                            <ItemStyle BackColor="white"/>
                                                            <ItemTemplate>
                                                                <asp:Label ID="txtItemCode" runat="server" Width="350px" Text='<%# Bind("ItemCode") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Description" SortExpression="PRDescription">
                                                            <HeaderStyle Width="133px" />
                                                            <ItemStyle BackColor="white" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="txtPRDescription" runat="server" Width="350px" Text='<%# Bind("PRDescription") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Delivery Date" SortExpression="DeliveryDate">
                                                            <HeaderStyle Width="133px" />
                                                            <ItemStyle BackColor="white" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="txtDeliveryDate" runat="server" Width="350px" Text='<%# Bind("DeliveryDate") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Unit of Measure" SortExpression="UOM">
                                                            <HeaderStyle Width="133px" />
                                                            <ItemStyle BackColor="white" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="txtUOM" runat="server" Width="350px" Text='<%# Bind("UOM") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Quantity" SortExpression="Qty">
                                                            <HeaderStyle Width="133px" />
                                                            <ItemStyle BackColor="white" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="txtQty" runat="server" Width="350px" Text='<%# Bind("Qty", "{0:N}") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>                                                       
                                                        <asp:TemplateField HeaderText="Unit Price" SortExpression="UnitPrice">
                                                            <HeaderStyle Width="133px" />
                                                            <ItemStyle BackColor="white" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="txtUnitPrice" runat="server" Width="350px" Text='<%# Bind("UnitPrice", "{0:N}") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Currency" SortExpression="Currency">
                                                            <HeaderStyle Width="133px" />
                                                            <ItemStyle BackColor="white" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="txtCurrency" runat="server" Width="350px" Text='<%# Bind("Currency") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <%--<asp:TemplateField HeaderText="Commodity" SortExpression="Commodity">
                                                            <HeaderStyle Width="133px" />
                                                            <ItemStyle BackColor="white" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="txtCommodity" runat="server" Text='<%# Bind("Commodity") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>--%>
                                                        <asp:TemplateField HeaderText="Company" SortExpression="Company">
                                                            <HeaderStyle Width="133px" />
                                                            <ItemStyle BackColor="white" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCompany" runat="server" ForeColor="Black" Text='<%# Bind("CompanyName") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Location" SortExpression="Company">
                                                            <HeaderStyle Width="133px" />
                                                            <ItemStyle BackColor="white" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblLocation" runat="server" ForeColor="Black" Text='<%# Bind("Location") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Buyer" SortExpression="Buyer">
                                                            <HeaderStyle Width="133px" />
                                                            <ItemStyle BackColor="white" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblBuyerName" runat="server" ForeColor="Black" Text='<%# Bind("Buyer") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Purchasing Officer" SortExpression="PurchasingOfficer">
                                                            <HeaderStyle Width="133px" />
                                                            <ItemStyle BackColor="white" />
                                                            <ItemTemplate>
                                                                 <asp:Label ID="lblPurchasingOfficer" ForeColor="Black" runat="server" Text='<%# Bind("PurchasingOfficer") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Remarks">
                                                            <HeaderStyle Width="133px" />
                                                            <ItemStyle BackColor="white" />
                                                            <ItemTemplate>
                                                                 <asp:Label ID="lblRemarks" ForeColor="Black" runat="server" Text='<%# Eval("Remarks").ToString().Replace("\n","<br>") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Group" SortExpression="GroupName">
                                                            <HeaderStyle Width="133px" />
                                                            <ItemStyle BackColor="white" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="txtGroupName" runat="server" Width="200px" Text='<%# Bind("GroupName") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Budget" SortExpression="Budget">
                                                            <HeaderStyle Width="133px" />
                                                            <ItemStyle BackColor="white" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="txtBudget" runat="server" Width="200px" Text='<%# Bind("Budget", "{0:N}") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Fields>
                                                </asp:DetailsView>
                                                <asp:SqlDataSource ID="dsPRDetails" runat="server"  ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>' SelectCommand="sp_GetPRDetails" SelectCommandType="StoredProcedure">
                                                    <SelectParameters>
                                                        <asp:SessionParameter Name="PrRefNo" SessionField="PrRefNo" Type="Int32" />
                                                    </SelectParameters>
                                                </asp:SqlDataSource>
                                                
                                                <br />
                                                    <p style="text-align: center;">
                                                    <asp:Label ID="lblMessage" runat="server"></asp:Label>
                                                </p>
                                                <table border="0" cellpadding="0" cellspacing="0" width="100%" id="actions">
                                                    <tr>
                                                        <td style="height: 34px">
                                                            &nbsp;
                                                            <%--<asp:LinkButton ID="lnkSave" runat="server" OnClick="lnkSave_Click" Width="150px">Save Changes</asp:LinkButton> --%> 
                                                            <asp:LinkButton ID="lnkBack" runat="server" OnClick="lnkBack_Click" Width="120px">Back</asp:LinkButton>
                                                        </td>
                                                    </tr>
                                                </table>
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
                        <EBid:Footer runat="server" ID="Footer1" />
                    </td>
                </tr>
            </table>
        </form>
    </div>
</body>
</html>
