<%@ Page Language="C#" AutoEventWireup="true" CodeFile="locations.aspx.cs" Inherits="admin_items" Theme="default" %>
<%@ Register Src="../usercontrol/admin/AdminLeftNavList.ascx" TagName="AdminLeftNavList" TagPrefix="uc1" %>
<%@ Register Src="../usercontrol/admin/Admin_TopNav_List.ascx" TagName="Admin_TopNav_List" TagPrefix="uc2" %>
<%@ Register TagPrefix="EBid" TagName="TopDate" Src="~/usercontrol/TopDate.ascx" %>
<%@ Register TagPrefix="EBid" TagName="AdminTopNav" Src="~/usercontrol/admin/adminTopNav.ascx" %>
<%@ Register TagPrefix="EBid" TagName="Footer" Src="~/usercontrol/Footer.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>.:| Trans-Asia | Locations |:.</title>
    <meta http-equiv="Content-Language" content="en-us" />
    <meta http-equiv="Content-Type" content="text/html; charset=windows-1252" />
    <script type="text/javascript" src="../include/util.js"></script>
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="../css/style_ua.css" />    
    <link href="../../css/style.css" rel="stylesheet" type="text/css" />    
    <!-- Created By: GA Sacramento 09122006 -->        
    <link href="../../css/style.css" rel="stylesheet" type="text/css" />
</head>
<body onload="SetStatus();">
    <div align="left">
        <form id="Form1" runat="server" defaultfocus="txtLocationName" defaultbutton="lnkAdd">
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
                                    <uc2:Admin_TopNav_List id="Admin_TopNav_List1" runat="server"></uc2:Admin_TopNav_List>
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
                                    <h2>Manage Lists</h2>
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
                                                            <h1><br />
                                                                Manage Locations
                                                            </h1>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="height: 30px" align="right">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label id="lblMsg" runat="server" ForeColor="Red" Visible="False" Font-Names="Arial" Font-Size="11px"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="height: 30px;">                                                            
                                                            <h3>Edit / Delete Location</h3>
                                                            <br />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center">
                                                            <asp:GridView ID="gvLocations" runat="server" AllowPaging="True" AllowSorting="true" AutoGenerateColumns="False" 
                                                                DataSourceID="dsLocations" SkinID="AuctionEvents" Width="100%" OnRowDataBound="gvLocations_RowDataBound" 
                                                                DataKeyNames="LocationId,LocationCount">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="&#160;Location Name" SortExpression="LocationName">
                                                                        <ItemStyle HorizontalAlign="Left" />
                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                        <EditItemTemplate>
                                                                            <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("LocationName") %>' Width="250px" MaxLength="50"></asp:TextBox>
                                                                        </EditItemTemplate>                                                                        
                                                                        <ItemTemplate>
                                                                            &nbsp;<asp:Label ID="Label1" runat="server" Text='<%# Bind("LocationName") %>'></asp:Label>
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
                                                            <asp:SqlDataSource ID="dsLocations" runat="server" ConnectionString='<%$ Code: (string) Session["ConnectionString"] ?? ConfigurationManager.ConnectionStrings["EbidConnectionString"].ConnectionString %>'
                                                                SelectCommand="s3p_EBid_GetAllLocations" SelectCommandType="StoredProcedure" 
                                                                DeleteCommand="sp_DeleteLocation" DeleteCommandType="StoredProcedure" 
                                                                InsertCommand="sp_AddLocation" InsertCommandType="StoredProcedure" 
                                                                UpdateCommand="sp_UpdateLocation" UpdateCommandType="StoredProcedure" 
                                                                OnDeleting="dsLocations_Deleting" OnUpdating="dsLocations_Updating" OnDeleted="dsLocations_Deleted" 
                                                                OnUpdated="dsLocations_Updated" OnInserted="dsLocations_Inserted">
                                                                <DeleteParameters>
                                                                    <asp:Parameter Name="LocationId" Type="Int32" />
                                                                </DeleteParameters>
                                                                <UpdateParameters>              
                                                                    <asp:Parameter Name="LocationId" Type="Int32" />                                                              
                                                                    <asp:Parameter Name="LocationName" Type="String" />                                                                            
                                                                </UpdateParameters>                                                                
                                                            </asp:SqlDataSource>                                                                                                                       
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="height: 10px;" class="actions">                                                            
                                                        </td>
                                                    </tr> 
                                                    <tr>
                                                        <td><br /></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="height: 30px;">                                                            
                                                            <h3>Add Location</h3>
                                                            <br />
                                                        </td>
                                                    </tr>                                                   
                                                    <tr>
                                                        <td>
                                                            <p>                                                            
                                                            Location Name :&nbsp;
                                                            <asp:TextBox ID="txtLocationName" Runat="server" Width="250px" MaxLength="50"></asp:TextBox>                                                            
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtLocationName"
                                                                    Display="Dynamic" ErrorMessage="* Required" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                                <asp:CustomValidator ID="cvLocation" runat="server" ErrorMessage="* Location Name Already Exist" OnServerValidate="cvItem_ServerValidate" SetFocusOnError="True"></asp:CustomValidator><br /><br />
                                                            </p>                                                            
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>                                                            
                                                            <table border="0" cellpadding="0" cellspacing="0" width="100%" id="actions">
                                                                <tr>
                                                                    <td>
                                                                        <asp:LinkButton ID="lnkAdd" runat="server" OnClientClick="return confirm('Are you sure you want to add this new location?');" OnClick="lnkAdd_Click">Add</asp:LinkButton>
                                                                        <a href="locations.aspx">Cancel</a>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr><td style="height: 20px;"></td></tr>
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
