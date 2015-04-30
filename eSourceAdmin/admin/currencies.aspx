<%@ Page Language="C#" AutoEventWireup="true" CodeFile="currencies.aspx.cs" Inherits="admin_currencies" Theme="default" %>

<%@ Register Assembly="Calendar" Namespace="CalendarControl" TagPrefix="cc1" %>
<%@ Register Src="../usercontrol/admin/AdminLeftNavList.ascx" TagName="AdminLeftNavList" TagPrefix="uc1" %>
<%@ Register Src="../usercontrol/admin/Admin_TopNav_List.ascx" TagName="Admin_TopNav_List" TagPrefix="uc2" %>
<%@ Register TagPrefix="EBid" TagName="TopDate" Src="~/usercontrol/TopDate.ascx" %>
<%@ Register TagPrefix="EBid" TagName="AdminTopNav" Src="~/usercontrol/admin/adminTopNav.ascx" %>
<%@ Register TagPrefix="EBid" TagName="Footer" Src="~/usercontrol/Footer.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>.:| Trans-Asia | Currencies |:.</title>
    <meta http-equiv="Content-Language" content="en-us" />
    <meta http-equiv="Content-Type" content="text/html; charset=windows-1252" />
    <script type="text/javascript" src="../include/util.js"></script>
    <link rel="stylesheet" type="text/css" href="../css/style.css" />
    <link rel="stylesheet" type="text/css" href="../css/style_ua.css" />                
    <script type="text/javascript">
    <!--
        var hasDot = false;
    //-->
    </script>
</head>
<body>
    <div align="left">
        <form id="Form1" runat="server">
            <table border="0" cellpadding="0" cellspacing="0" width="100%" id="page">
                <tr>
                    <td valign="top" height="137px">
                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td>
                                    <div align="left" id="masthead">
                                        <EBid:AdminTopNav runat="server" ID="GlobalLinksNav" />
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <uc2:Admin_TopNav_List ID="Admin_TopNav_List1" runat="server"></uc2:Admin_TopNav_List>
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
                        <table border="0" cellpadding="0" cellspacing="0" width="100%" style="height: 100%;">
                            <tr>
                                <td id="relatedInfo">
                                    <h2>
                                        Manage Lists</h2>
                                    <uc1:AdminLeftNavList ID="AdminLeftNavList1" runat="server" />
                                    <p>
                                        &nbsp;</p>
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
                                                                Manage Currencies
                                                            </h1>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="height: 30px" align="right">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblMsg" runat="server" ForeColor="Red" Visible="False" Font-Names="Arial" Font-Size="11px"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="height: 30px;">
                                                            <h3>
                                                                Edit / Delete Currency</h3>
                                                            <br />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center">
                                                            <asp:GridView ID="gvCurrencies" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" SkinID="AuctionEvents" 
                                                                DataSourceID="dsCurrencies" Width="100%" DataKeyNames="Deletable, ID" OnRowDataBound="gvCurrencies_RowDataBound">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="&#160;ID" SortExpression="ID">
                                                                        <ItemStyle HorizontalAlign="Center" Width="30px" />
                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                        <EditItemTemplate>
                                                                            &nbsp;<asp:Label ID="Label11" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                                                        </EditItemTemplate>
                                                                        <ItemTemplate>
                                                                            &nbsp;<asp:Label ID="Label1" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="&#160;Currency" SortExpression="Currency">
                                                                        <ItemStyle HorizontalAlign="Left" />
                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                        <EditItemTemplate>
                                                                            <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("Currency") %>' Width="250px" MaxLength="50"></asp:TextBox>
                                                                        </EditItemTemplate>
                                                                        <ItemTemplate>
                                                                            &nbsp;<asp:Label ID="Label2" runat="server" Text='<%# Bind("Currency") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="&#160;Rate To USD" SortExpression="RateToUSD">
                                                                        <ItemStyle HorizontalAlign="Center" Width="90px" />
                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                        <EditItemTemplate>
                                                                            <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("RateToUSD") %>' Width="70px" MaxLength="30"></asp:TextBox>
                                                                        </EditItemTemplate>
                                                                        <ItemTemplate>
                                                                            &nbsp;<asp:Label ID="Label3" runat="server" Text='<%# Bind("RateToUSD") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="&#160;Rate To PHP" SortExpression="RateToPHP">
                                                                        <ItemStyle HorizontalAlign="Center" Width="90px" />
                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                        <EditItemTemplate>
                                                                            &nbsp;<asp:Label ID="Label4" runat="server" Text='<%# Bind("RateToPHP") %>'></asp:Label>
                                                                        </EditItemTemplate>
                                                                        <ItemTemplate>
                                                                            &nbsp;<asp:Label ID="Label4" runat="server" Text='<%# Bind("RateToPHP") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="&#160;Rate As Of" SortExpression="AsOf">
                                                                        <ItemStyle HorizontalAlign="Center" Width="110px" />
                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                        <EditItemTemplate>   
                                                                            <cc1:JSCalendar ID="clndrRateAsOf" runat="server" ScriptsBasePath="../calendar/" EnableViewState="true" ImageURL="../calendar/img.gif" 
                                                                            DateFormat="MM/dd/yyyy" Width="80px" ReadOnly="false" MaxLength="10" TabIndex="-1" Text='<%# Bind("AsOf", "{0:MM/dd/yyyy}") %>'></cc1:JSCalendar>                                                                                                                                                     
                                                                        </EditItemTemplate>
                                                                        <ItemTemplate>
                                                                            &nbsp;<asp:Label ID="Label5" runat="server" Text='<%# Bind("AsOf", "{0:MM/dd/yyyy}") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:CommandField ShowEditButton="True">
                                                                        <ItemStyle HorizontalAlign="Center" Width="40px" />
                                                                    </asp:CommandField>
                                                                    <asp:CommandField ShowDeleteButton="True">
                                                                        <ItemStyle HorizontalAlign="Center" Width="50px" />
                                                                    </asp:CommandField>
                                                                </Columns>
                                                            </asp:GridView>
                                                            <asp:SqlDataSource ID="dsCurrencies" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>' 
                                                                SelectCommand="sp_GetCurrencies" SelectCommandType="StoredProcedure"
                                                                InsertCommand="sp_AddCurrency" InsertCommandType="StoredProcedure"
                                                                DeleteCommand="sp_DeleteCurrency" DeleteCommandType="StoredProcedure"
                                                                UpdateCommand="sp_UpdateCurrency" UpdateCommandType="StoredProcedure" 
                                                                OnDeleted="dsCurrencies_Deleted" OnDeleting="dsCurrencies_Deleting" 
                                                                OnUpdated="dsCurrencies_Updated" OnUpdating="dsCurrencies_Updating" OnInserted="dsCurrencies_Inserted">
                                                                <DeleteParameters>
                                                                    <asp:Parameter Name="ID" Type="String" Size="3" />
                                                                </DeleteParameters>
                                                                <UpdateParameters>
                                                                    <asp:Parameter Name="ID" Type="String" Size="3" />
                                                                    <asp:Parameter Name="Currency" Type="String" />
                                                                    <asp:Parameter Name="RateToUSD" Type="Decimal" />                                                                    
                                                                    <asp:Parameter Name="AsOf" Type="DateTime" />
                                                                </UpdateParameters>
                                                                <InsertParameters>
                                                                    <asp:Parameter Name="ID" Type="String" />
                                                                    <asp:Parameter Name="Currency" Type="String" />
                                                                    <asp:Parameter Name="RateToUSD" Type="Decimal" />
                                                                    <asp:Parameter Name="RateToPHP" Type="Decimal" />
                                                                    <asp:Parameter Name="AsOf" Type="DateTime" />
                                                                </InsertParameters>
                                                            </asp:SqlDataSource>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="height: 10px;" class="actions">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <br />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="height: 30px;">
                                                            <h3>
                                                                Add Currency</h3>
                                                            <br />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <table border="0" cellpadding="0" cellspacing="0" width="100%" style="font-family:Arial; font-size:11px;">
                                                                <tr>
                                                                    <td style="width: 70px;">ID</td>
                                                                    <td style="width: 3px;">:</td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtID" runat="server" Width="40px" MaxLength="3"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtID" Display="Dynamic" ErrorMessage="* Required" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>Currency</td>
                                                                    <td>:</td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtCurrency" runat="server" Width="250px" MaxLength="50"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtCurrency" Display="Dynamic" ErrorMessage="* Required" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>Rate To USD</td>
                                                                    <td>:</td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtRateToUSD" runat="server" Width="80px" MaxLength="20"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtRateToUSD" Display="Dynamic" ErrorMessage="* Required" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                                        <asp:HiddenField ID="hdnPHPToUSD" runat="server" Value="0.0" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>Rate To PHP</td>
                                                                    <td>:</td>
                                                                    <td>                                                                        
                                                                        &nbsp;<asp:Label ID="txtRateToPHP" runat="server" Text="0.00" Font-Bold="true" Font-Size="12px"></asp:Label>                                                                       
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>Rate As Of</td>
                                                                    <td>:</td>
                                                                    <td>
                                                                        <cc1:JSCalendar ID="clndrRateAsOf" runat="server" ScriptsBasePath="../calendar/" EnableViewState="true" ImageURL="../calendar/img.gif" 
                                                                            DateFormat="MM/dd/yyyy" Width="80px" ReadOnly="false" MaxLength="10" TabIndex="-1"></cc1:JSCalendar>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="clndrRateAsOf" Display="Dynamic" ErrorMessage="* Required" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                                    </td>
                                                                </tr>
                                                            </table>                                                                   
                                                            <br />                                                     
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <table border="0" cellpadding="0" cellspacing="0" width="100%" id="actions">
                                                                <tr>
                                                                    <td>
                                                                        <asp:LinkButton ID="lnkAdd" runat="server" OnClientClick="return confirm('Are you sure you want to add this new currency?');" OnClick="lnkAdd_Click">Add</asp:LinkButton>
                                                                        <a href="currencies.aspx">Cancel</a>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="height: 20px;">
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
