<%@ Page Language="C#" AutoEventWireup="true" CodeFile="viewPR.aspx.cs" Inherits="web_buyerscreens_viewPR" %>

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
                                                    View PR
                                                </h1>
                                                <p>
                                                    Click on the PR No. or the PR description to view the details of the PR.
                                                </p>
                                                <asp:GridView ID="gvPR" runat="server" SkinID="BidEvents" AllowPaging="True" AllowSorting="True"
                                                    AutoGenerateColumns="False" DataKeyNames="PRNo" DataSourceID ="dsSubmitted"
                                                    OnRowCommand="gvPR_RowCommand" PageSize="20">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText=" " InsertVisible="False">
                                                            <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                                            <ItemTemplate>
                                                                <%--<asp:LinkButton ID="lnkPRNo" runat="server" Text='<%# Bind("PRNo") %>' CommandArgument='<%# Bind("PRNo") %>'   CommandName="Details" ></asp:LinkButton>--%>
                                                                <asp:CheckBox ID="CheckBox1" runat="server" />
                                                                <asp:HiddenField ID="HiddenField1" runat="server" Value='<%# Bind("PRRefNo") %>' />
                                                                <%--<asp:Label ID="Label1" runat="server" Text='<%# Bind("PRRefNo") %>'></asp:Label>--%>
                                                                <asp:LinkButton ID="lnkPRRefNo1" runat="server" Text='<%# Bind("PRRefNo") %>' CommandArgument='<%# Bind("PrRefNo") %>'   CommandName="Details" ></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="PR" InsertVisible="False" SortExpression="PRNo">
                                                            <HeaderStyle HorizontalAlign="Center" Width="90px" />
                                                            <ItemTemplate>
                                                                &nbsp<%--<asp:LinkButton ID="lnkPRNo" runat="server" Text='<%# Bind("RQN") %>' CommandArgument='<%# Bind("PrRefNo") %>'   CommandName="Details" ></asp:LinkButton>--%>
                                                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("RQN") %>'></asp:Label>

                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Description" SortExpression="PrDescription">
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <ItemTemplate>
                                                                &nbsp<asp:Label ID="lnkDesc" runat="server" Text='<%# Bind("PRDescription") %>'  ></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Item Code" SortExpression="ItemCode">
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <ItemTemplate>
                                                                &nbsp<asp:Label ID="lnkItemCode" runat="server" Text='<%# Bind("ItemCode") %>' ></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="PR Date" SortExpression="PRDate">
                                                            <HeaderStyle HorizontalAlign="Center" Width="140px" />
                                                            <ItemTemplate>
                                                                &nbsp<asp:Label ID="lnkDate" runat="server" Text='<%# Bind("PRDate") %>' ></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="UOM" SortExpression="UOM">
                                                            <HeaderStyle HorizontalAlign="Center" Width="70px"/>
                                                            <ItemTemplate>
                                                                &nbsp<asp:Label ID="UOM" runat="server" Text='<%# Bind("UOM") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Quantity" SortExpression="Qty">
                                                            <HeaderStyle HorizontalAlign="Center" Width="70px"/>
                                                            <ItemTemplate>
                                                                &nbsp<asp:Label ID="lblQuantity" runat="server" Text='<%# Bind("Qty", "{0:N}") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Unit Price" SortExpression="UnitPrice">
                                                            <HeaderStyle HorizontalAlign="Center" Width="90px"/>
                                                            <ItemTemplate>
                                                                &nbsp<asp:Label ID="lblUnitPrice" runat="server" Text='<%# Bind("UnitPrice", "{0:N}") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Company" SortExpression="CompanyName">
                                                            <HeaderStyle HorizontalAlign="Center" Width="120px"/>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCompanyName" runat="server" Text='<%# Bind("CompanyName") %>' Font-Size="9px"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="">
                                                            <HeaderStyle HorizontalAlign="Center" Width="60px"/>
                                                            <ItemTemplate>
                                                                &nbsp<asp:LinkButton ID="lnkEditPRGroup" runat="server" Text="Details" CommandArgument='<%# Bind("PrRefNo") %>' 
                                                                        CommandName="Details"></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                                <asp:SqlDataSource ID="dsSubmitted" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>'
                                                        SelectCommand="sp_GetViewPR" SelectCommandType="StoredProcedure">
                                                        <SelectParameters>
                                                            <asp:SessionParameter Name="BuyerId" SessionField="UserId" Type="Int32" />
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
                                                            <asp:LinkButton ID="lnkSave" runat="server" Width="220px" OnClientClick="return confirm('Are you sure you want to create a new bid event from the selected item/s?');" OnClick="lnkSave_Click">Group Selected and Create Bid</asp:LinkButton>  
                                                            <%--<asp:LinkButton ID="lnkBack" runat="server" OnClick="lnkBack_Click" Width="120px">Back</asp:LinkButton>--%>
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
                        <EBid:Footer runat="server" ID="Footer" />
                    </td>
                </tr>
            </table>
        </form>
    </div>
</body>
</html>
