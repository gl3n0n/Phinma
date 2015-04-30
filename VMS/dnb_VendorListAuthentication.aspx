<%@ Page Title="" Language="C#" MasterPageFile="~/MasterSubPage.master" AutoEventWireup="true" CodeFile="dnb_VendorListAuthentication.aspx.cs" Inherits="dnb_VendorListAuthentication" %>
<%@ Register TagPrefix="Ava" TagName="Tabsnav" Src="usercontrols/tabs.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitlePlaceHolder1" runat="Server">
Trans-Asia VMS Accreditation :: Vendor
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!--jquery ui dialog starts -->
    <link rel="stylesheet" href="Styles/jquery-ui.css" />
    <script src="Scripts/jquery-ui.js"></script><style>
        label, input { display:block; }
        input.text { margin-bottom:12px; width:95%; padding: .4em; }
        fieldset { padding:0; border:0; margin-top:25px; }
        h1 { font-size: 1.2em; margin: .6em 0; }
        div#users-contain { width: 350px; margin: 20px 0; }
        div#users-contain table { margin: 1em 0; border-collapse: collapse; width: 100%; }
        div#users-contain table td, div#users-contain table th { border: 1px solid #eee; padding: .6em 10px; text-align: left; }
        .ui-dialog .ui-state-error { padding: .3em; }
        .validateTips { border: 1px solid transparent; padding: 0.3em; }
    </style>
    <script type="text/javascript">
        function ShowClarify(vendorid, vendorname) {

            $("#dialog:ui-dialog").dialog("destroy");

            if ($("#dialog-message").length != 0) $('div').remove("#dialog-message");

            if ($("#dialog-message").length == 0) {
                sDiv = "<div id='dialog-message' title='Clarify Vendor Information' style='display:none; overflow:hidden; width:400px;'>";
                sDiv = sDiv + "<p>";
                sDiv = sDiv + "<table style='height:220px;'>";
                sDiv = sDiv + "<tr>";
                sDiv = sDiv + "<td><b>To: " + vendorname +"</b>";
                sDiv = sDiv + "</td></tr>";
                sDiv = sDiv + "<tr><td><b>Comment:</b></td></tr>";
                sDiv = sDiv + "</td></tr>";
                sDiv = sDiv + "<tr>";
                sDiv = sDiv + "<td><textarea id='txtClarifyX' name='txtClarifyX' cols='32' rows='8' style='font-family: Arial; font-size: 11; width:255px;'></textarea></td>";
                sDiv = sDiv + "</tr>";
                sDiv = sDiv + "<tr>";
                sDiv = sDiv + "<td colspan='2' align='right'>";
                sDiv = sDiv + "</td>";
                sDiv = sDiv + "</tr></table>";
                sDiv = sDiv + "</p>";
                sDiv = sDiv + "</div>";
                $(sDiv).appendTo('body');
            }

            $("#dialog-message").dialog({
                modal: true,
                buttons: {
                    Clarify: function () {
                        $(this).dialog("close");
                        if ($("#txtClarifyX").val() == '') {
                            ShowDialog("Comment  must have a value.", "#txtClarifyX");
                        }
                        else {
                            $("#ContentPlaceHolder1_txtClarify").val($("#txtClarifyX").val());
                            __doPostBack('Clarify', vendorid);
                        }
                    },
                    Cancel: function () {
                        $(this).dialog("close");
                        return false;
                    }
                }
            });
        }

    </script>

    <%--<button id="create-user">Create new user</button>--%>
    <!--jquery ui dialog ends -->
    <div class="content">
<div class="content_logo">
<img src="images/final_logo_w.png"  border="0" />
</div>
<div class="rounded-corners-top" id="menuAVA">
<ava:tabsnav ID="Tabsnav1" runat="server" />
</div>
<div style="background:#FFF; min-height:445px; padding:10px;" class="rounded-corners-bottom2 menu">
<!--##################-->
<!--BODY CONTENT STARTS-->
<%@ Register TagPrefix="Ava" TagName="TopNav" Src="usercontrols/TopNav_dnb.ascx" %>
<ava:topnav ID="TopNav1" runat="server" />

<!--Business activities STARTS-->
<div class="separator1"></div>
<h3 style="margin:10px 0px;">Vendors for Authentication</h3>
<form action="" method="post" runat="server">
    <input type="hidden" id="txtClarify" name="txtClarify" runat="server" />
    <input type="hidden" id="ResponseClarifyTo" name="ResponseClarifyTo" runat="server" />
    <h3 style="text-align:right"><asp:Label ID="errNotificationAuthTicket" runat="server" Text=""></asp:Label></h3>
<asp:SqlDataSource ID="dsVendorAuthenticated" runat="server" ConnectionString="<%$ ConnectionStrings:AVAConnectionString %>"
            SelectCommand="SELECT t1.* FROM tblVendor t1 WHERE (t1.AuthenticationTicket IS NOT NULL OR t1.AuthenticationTicket!='') AND (t1.IsAuthenticated IS NULL OR t1.IsAuthenticated = '') AND (t1.Status = 1 OR t1.Status = 9) AND t1.EndorsedTo=@EndorsedTo ORDER BY t1.DateCreated DESC" >
    <SelectParameters>
        <asp:SessionParameter Name="EndorsedTo" Type="Int32" SessionField="UserId" />
    </SelectParameters>
	    </asp:SqlDataSource>
<asp:GridView ID="GridView1" runat="server" DataSourceID="dsVendorAuthenticated" 
    AllowPaging="True" AllowSorting="True" BorderColor="Silver" OnRowCommand="gvVendors_RowCommand" OnRowDataBound="Gridview1_RowDataBound"
    BorderStyle="Dotted" BorderWidth="1px" CellPadding="5" ClientIDMode="AutoID" 
    Width="100%" AutoGenerateColumns="False" PageSize="15" EmptyDataText="No submitted vendors for authentication.">
    <AlternatingRowStyle BackColor="#EEEEEE" />
    <Columns>
        <asp:TemplateField HeaderText="Company Name" InsertVisible="False" SortExpression="CompanyName" ItemStyle-HorizontalAlign="Center">
            <HeaderStyle HorizontalAlign="Center" Width="90px" />
            <ItemTemplate>
                &nbsp;<%--<asp:LinkButton ID="lnkRefNo" runat="server" Text='<%# Bind("CompanyName") %>' CommandArgument='<%# Bind("VendorId") %>' CommandName="Details"></asp:LinkButton>--%><a href="<%# "vendor_Home.aspx?VendorId=" + Eval("VendorId") %>" class="iframe" title='<%# Eval("CompanyName") %>' ><%# Eval("CompanyName") %></a><%--<asp:Label ID="Label3" runat="server" Text='<%# Bind("CompanyName") %>'></asp:Label>--%><%--&nbsp;<asp:Label ID="Label0" runat="server" Text='<%# Bind("CompanyName") %>'></asp:Label>--%>
            </ItemTemplate>

<ItemStyle HorizontalAlign="Center"></ItemStyle>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Authentication Ticket" InsertVisible="False" SortExpression="AuthenticationTicket" ItemStyle-HorizontalAlign="Center">
            <HeaderStyle HorizontalAlign="Center" Width="90px" />
            <ItemTemplate>
                &nbsp;<asp:Label ID="Label1" runat="server" Text='<%# Bind("AuthenticationTicket") %>'></asp:Label>
            </ItemTemplate>

<ItemStyle HorizontalAlign="Center"></ItemStyle>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Date Submitted" InsertVisible="False" SortExpression="DateSubmittedToDnb" ItemStyle-HorizontalAlign="Center">
            <HeaderStyle HorizontalAlign="Center" Width="90px" />
            <ItemTemplate>
                &nbsp;<asp:Label ID="Label2" runat="server" Text='<%# Bind("DateSubmittedToDnb") %>'></asp:Label>
            </ItemTemplate>

<ItemStyle HorizontalAlign="Center"></ItemStyle>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Action" InsertVisible="False" ItemStyle-HorizontalAlign="Center">
            <HeaderStyle HorizontalAlign="Center" Width="90px" />
            <ItemTemplate>
                &nbsp;<asp:LinkButton ID="lnkAuth" runat="server" Text='Authenticate' CommandArgument='<%# Bind("VendorId") %>' CommandName="Authenticate"></asp:LinkButton>&nbsp;&nbsp;|&nbsp;&nbsp;
                <%--<asp:LinkButton ID="lnkClarify" runat="server" Text='Clarify' OnClientClick="javascript: ShowClarify();"></asp:LinkButton>--%>
                <%--<a runat="server" id="lnkClarify" href="javascript:__doPostBack('ctl00$ContentPlaceHolder1$GridView1$ctl04$lnkClarify','')">Clarify</a>--%>
                <%--<asp:HyperLink runat="server" ID="lnkClarify" Text="Clarify" NavigateUrl='<%# "javascript:__doPostBack(\x0027ctl00$ContentPlaceHolder1$GridView1$ctl"+(Container.DataItemIndex + 102).ToString().Substring(1)+"$lnkClarify\x0027,\x0027\x0027)" %>'></asp:HyperLink>--%>
                <%--<asp:HyperLink runat="server" ID="lnkClarify" Text="Clarify" Onclick='<%# "javascript:ShowClarify(\x0027"+Eval("VendorId")+"\x0027,\x0027"+Eval("CompanyName").ToString().Replace("'","''")+"\x0027)" %>' NavigateUrl="javascript:void(0)" ></asp:HyperLink>--%>
                <asp:HyperLink runat="server" ID="lnkClarify" Text="Clarify" NavigateUrl="javascript:void(0)" Onclick="" ></asp:HyperLink>
            </ItemTemplate>

<ItemStyle HorizontalAlign="Center"></ItemStyle>
        </asp:TemplateField>
    </Columns>
</asp:GridView>
</form>
<br />
<!--BODY CONTENT ENDS-->
<!--##################-->
</div>
</div>
</div><!-- content ends --> 

</asp:Content>