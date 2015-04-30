<%@ Page Title="" Language="C#" MasterPageFile="~/MasterSubPage.master" AutoEventWireup="true" CodeFile="vmofficer_VendorForRenewal_List.aspx.cs" Inherits="vmofficer_VendorForRenewal_List" %>
<%@ Register TagPrefix="Ava" TagName="Tabsnav" Src="usercontrols/tabs.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitlePlaceHolder1" runat="Server">
Trans-Asia VMS Accreditation :: Vendor
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link rel="stylesheet" type="text/css" href="Styles/jquery-ui.css" />
<script type="text/javascript" src="Scripts/jquery-ui.js"></script>
 <style>
input.text { margin-bottom:12px; width:95%; padding: .4em; }
fieldset { padding:0; border:0; margin-top:25px; }
h1 { font-size: 1.2em; margin: .6em 0; }
div#users-contain { width: 350px; margin: 20px 0; }
div#users-contain table { margin: 1em 0; border-collapse: collapse; width: 100%; }
div#users-contain table td, div#users-contain table th { border: 1px solid #eee; padding: .6em 10px; text-align: left; }
.ui-dialog .ui-state-error { padding: .3em; }
.validateTips { border: 1px solid transparent; padding: 0.3em; }
</style>
<script>
    $(function () {
        
        $("#dialog-form").dialog({
            autoOpen: false,
            height: 250,
            width: 350,
            modal: true,
            buttons: {
                "Send": function () {
                    //alert($("#dialog-form").data('VendorName'));
                    if (confirm("Are you sure to send the email notification to " + $("#dialog-form").data('VendorName') + "?")) {
                        __doPostBack("Details", $("#dialog-form").data('id') + "|" + $("#accgroup").val() + "|" + $("#vendorcode").val() + "|" + $("#purchasingorg").val() + "|" + $("#CountryCode").val() + "|" + $("#Currency").val() + "|" + $("#vat").val());
                        //$(this).dialog("close");
                    }
                },
                Cancel: function () {
                    $(this).dialog("close");
                }
            },
            close: function () {
                allFields.val("").removeClass("ui-state-error");
            }
        });
        //$(".bt10")
        //.button()
        //.click(function () {
        //    $("#dialog-form").dialog("open");
        //});
    });
</script>

	

<div id="dialog-form" title="Send Email Notification">
<p class="VendorNameHere" style="font-weight:bold"></p>
<fieldset>
   Reset accreditaion status and enable renewal process. <br />
    <i>This will allow user for this vendor to login and edit vendor information.</i>
<%--<label for="name">Vendor Code</label>
<input type="text" name="vendorcode" id="vendorcode" class="text ui-widget-content ui-corner-all" />
<label for="email">Account Group</label>
<input type="text" name="accgroup" id="accgroup" value="" class="text ui-widget-content ui-corner-all" />
<label for="email">Purchasing Org</label>
<input type="text" name="purchasingorg" id="purchasingorg" value="" class="text ui-widget-content ui-corner-all" />
<label for="email">Country Code for: <span id="CountryCode_Lbl"></span></label>
<input type="text" name="CountryCode" id="CountryCode" value="" class="text ui-widget-content ui-corner-all" />
<label for="email">Currency</label>
<input type="text" name="Currency" id="Currency" value="" class="text ui-widget-content ui-corner-all" />
<label for="vat">VAT</label>
<input type="text" name="vat" id="vat" value="" class="text ui-widget-content ui-corner-all"/>--%>
</fieldset>
</div>
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
<%@ Register TagPrefix="Ava" TagName="TopNav" Src="usercontrols/TopNav_vmofficer.ascx" %>
<ava:topnav ID="TopNav1" runat="server" />

<%--<%@ Register TagPrefix="Ava" TagName="list_approved" Src="usercontrols/list_approved.ascx" %>
<ava:list_approved ID="list_approved1" runat="server" />--%>

<h3 style="margin:10px 0px;">Vendors For Renewal</h3>
<form id="Form1" action="" method="post" runat="server">
<asp:SqlDataSource ID="dsVendorAuthenticated" runat="server" ConnectionString="<%$ ConnectionStrings:AVAConnectionString %>"
			SelectCommand="SELECT *, t3.AccreDuration , dbo.CalculateNumberOFWorkDays(DateSubmittedToDnb , DateAuthenticatedByDnb) as datediff1,  dbo.CalculateNumberOFWorkDays(DateAuthenticatedByDnb, approvedbyDnbDate) as datediff2,  dbo.CalculateNumberOFWorkDays(approvedbyDnbDate, approvedbyVMOfficerDate) as datediff3,  dbo.CalculateNumberOFWorkDays(approvedbyVMOfficerDate, approvedbyVMRecoDate) as datediff4,  dbo.CalculateNumberOFWorkDays(approvedbyVMRecoDate, approvedbyFAALogisticsDate) as datediff5,  dbo.CalculateNumberOFWorkDays(approvedbyFAALogisticsDate, approvedbyFAAFinanceDate) as datediff6,  dbo.CalculateNumberOFWorkDays(DateSubmittedToDnb, approvedbyFAALogisticsDate) as datetotal1,  dbo.CalculateNumberOFWorkDays(DateSubmittedToDnb, approvedbyFAAFinanceDate) as datetotal2, t2.regCountry, CASE t3.AccreDuration
		WHEN '2 years' THEN DATEADD(month, 22, tblVendor.approvedbyFAALogisticsDate)
		WHEN '1 year' THEN DATEADD(month, 10, tblVendor.approvedbyFAALogisticsDate)
		WHEN '6 months' THEN DATEADD(month, 4, tblVendor.approvedbyFAALogisticsDate)
		ELSE NULL
	END as 'Due Date' FROM tblVendor, tblVendorInformation t2, tblVendorApprovalbyVmReco t3  WHERE IsAuthenticated = 1 AND (Status = 6) AND t2.VendorId = tblVendor.VendorId AND t3.VendorId = tblVendor.VendorId AND tblVendor.renewaldate <= getdate() ORDER BY approvedbyFAAFinanceDate, approvedbyFAALogisticsDate  DESC" >
		</asp:SqlDataSource>
<asp:GridView ID="GridView1" runat="server" DataSourceID="dsVendorAuthenticated" 
	AllowPaging="True" AllowSorting="True" BorderColor="Silver" 
	BorderStyle="Dotted" BorderWidth="1px" CellPadding="5" ClientIDMode="AutoID"  EmptyDataText="No vendors for renewal to display."
	Width="100%" AutoGenerateColumns="False" PageSize="15" ShowFooter="True">
	<AlternatingRowStyle BackColor="#EEEEEE" />
	<Columns>
		<asp:TemplateField HeaderText="Company Name" InsertVisible="False" SortExpression="CompanyName" ItemStyle-HorizontalAlign="Center">
			<HeaderStyle HorizontalAlign="Center" Width="90px" />
			<ItemTemplate>
				&nbsp;<%--<asp:Label ID="lnkRefNo" runat="server" Text='<%# Bind("CompanyName") %>' ></asp:Label>--%><%--&nbsp;<asp:Label ID="Label0" runat="server" Text='<%# Bind("CompanyName") %>'></asp:Label>--%><a href="<%# "vendor_Home.aspx?VendorId=" + Eval("VendorId") %>" target="_blank"><%# Eval("CompanyName") %></a> 
			</ItemTemplate>
		</asp:TemplateField>
		<%--<asp:TemplateField HeaderText="Submitted to D&B" InsertVisible="False" SortExpression="DateSubmittedToDnb" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="10px">
			<HeaderStyle HorizontalAlign="Center" Width="90px" />
			<ItemTemplate>
				&nbsp;<asp:Label ID="Label1" runat="server" Text='<%# String.Format("{0:M/d/yyyy<br><i>&nbsp;&nbsp;HH:mm tt</i>}", Eval("DateSubmittedToDnb")) %>'></asp:Label>
			</ItemTemplate>
		</asp:TemplateField>
		<asp:TemplateField HeaderText="Authenticated" InsertVisible="False" SortExpression="DateAuthenticatedByDnb" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="10px">
			<HeaderStyle HorizontalAlign="Center" Width="90px" />
			<ItemTemplate>
				&nbsp;<asp:Label ID="Label2" runat="server" Text='<%# Eval("DateAuthenticatedByDnb").ToString()!="" ? "["+Eval("datediff1").ToString() + "]" + String.Format("{0:M/d/yyyy<br><i>&nbsp;&nbsp;HH:mm tt</i>}", Eval("DateAuthenticatedByDnb")) : "" %>'></asp:Label>
			</ItemTemplate>
		</asp:TemplateField>
		<asp:TemplateField HeaderText="Submitted to VM Officer" InsertVisible="False" SortExpression="approvedbyDnbDate" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="10px">
			<HeaderStyle HorizontalAlign="Center" Width="90px" />
			<ItemTemplate>
				&nbsp;<asp:Label ID="Label12" runat="server" Text='<%# Eval("approvedbyDnbDate").ToString()!="" ? "["+Eval("datediff2").ToString() + "]" +  String.Format("{0:M/d/yyyy<br><i>&nbsp;&nbsp;HH:mm tt</i>}", Eval("approvedbyDnbDate")) : "" %>'></asp:Label>
			</ItemTemplate>
		</asp:TemplateField>
		<asp:TemplateField HeaderText="Endorsed to VM Head" InsertVisible="False" SortExpression="approvedbyVMOfficerDate" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="10px">
			<HeaderStyle HorizontalAlign="Center" Width="90px" />
			<ItemTemplate>
				&nbsp;<asp:Label ID="Label13" runat="server" Text='<%# Eval("approvedbyVMOfficerDate").ToString()!="" ? "["+Eval("datediff3").ToString() + "]" + String.Format("{0:M/d/yyyy<br><i>&nbsp;&nbsp;HH:mm tt</i>}", Eval("approvedbyVMOfficerDate")) : "" %>'></asp:Label>
			</ItemTemplate>
		</asp:TemplateField>
		<asp:TemplateField HeaderText="Endorsed to PVMD" InsertVisible="False" SortExpression="approvedbyVMRecoDate" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="10px">
			<HeaderStyle HorizontalAlign="Center" Width="90px" />
			<ItemTemplate>
				&nbsp;<asp:Label ID="Label131" runat="server" Text='<%# Eval("approvedbyVMRecoDate").ToString()!="" ? "["+Eval("datediff4").ToString() + "]" + String.Format("{0:M/d/yyyy<br><i>&nbsp;&nbsp;HH:mm tt</i>}", Eval("approvedbyVMRecoDate")) : "" %>'></asp:Label>
			</ItemTemplate>
		</asp:TemplateField>
		<asp:TemplateField HeaderText="Endorsed to CFO" InsertVisible="False" SortExpression="approvedbyFAALogisticsDate" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="10px">
			<HeaderStyle HorizontalAlign="Center" Width="90px" />
			<ItemTemplate>
				&nbsp;<asp:Label ID="Label14" runat="server" Text='<%# Eval("approvedbyFAALogisticsDate").ToString()!=""? "["+Eval("datediff5").ToString() + "]" + String.Format("{0:M/d/yyyy<br><i>&nbsp;&nbsp;HH:mm tt</i>}", Eval("approvedbyFAALogisticsDate")) : "" %>'></asp:Label>
			</ItemTemplate>
		</asp:TemplateField>--%>
		<asp:TemplateField HeaderText="Duration" InsertVisible="False" SortExpression="AccreDuration" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="10px">
			<HeaderStyle HorizontalAlign="Center" Width="90px" />
			<ItemTemplate>
				&nbsp;<asp:Label ID="AccreDurationLbl" runat="server" Text='<%# Eval("AccreDuration").ToString() %>'></asp:Label>
			</ItemTemplate>
		</asp:TemplateField>
		<asp:TemplateField HeaderText="Status" InsertVisible="False" SortExpression="approvedbyFAALogisticsDate" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="10px">
			<HeaderStyle HorizontalAlign="Center" Width="90px" />
			<ItemTemplate>
				<asp:Label ID="Label15" runat="server" Text='<%# Eval("Status").ToString()!="8" ? Eval("Status").ToString()=="6"?"Accredited: <br>"+ String.Format("{0:M/d/yyyy <i>&nbsp;&nbsp;HH:mm tt</i>}", Eval("approvedbyFAAFinanceDate")) : "" : Eval("approvedbyFAAFinanceDate").ToString()!="" ? "Disapproved: <br>"+  "["+Eval("datediff1").ToString() + "]" + String.Format("{0:M/d/yyyy <i>HH:mm tt</i>}", Eval("approvedbyFAAFinanceDate")) : "Disapproved: <br>"+  "["+Eval("datediff5").ToString() + "]" + String.Format("{0:M/d/yyyy <i>HH:mm tt</i>}", Eval("approvedbyFAALogisticsDate")) %>'></asp:Label>
			</ItemTemplate>
		</asp:TemplateField>
		<asp:TemplateField HeaderText="Due for renewal" InsertVisible="False" SortExpression="approvedbyFAALogisticsDate" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="10px">
			<HeaderStyle HorizontalAlign="Center" Width="90px" />
			<ItemTemplate>
				&nbsp;<asp:Label ID="Label15" runat="server" Text='<%# Eval("Due Date").ToString() %>'></asp:Label>
			</ItemTemplate>
		</asp:TemplateField>
		<%--<asp:TemplateField HeaderText="Total" InsertVisible="False" SortExpression="approvedbyFAALogisticsDate" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="10px">
			<HeaderStyle HorizontalAlign="Center" Width="20px" />
			<ItemTemplate>
				&nbsp;<asp:Label ID="Label16" runat="server" Text='<%# (Eval("Status").ToString()=="8" || Eval("Status").ToString()=="6") && Eval("approvedbyFAAFinanceDate").ToString()!="" ? Eval("datetotal2").ToString() :  Eval("datetotal1").ToString() %>'></asp:Label>
			</ItemTemplate>
		</asp:TemplateField>--%>
		<%--<asp:TemplateField HeaderText="Endorsed to CFO" InsertVisible="False" SortExpression="DateCreated" ItemStyle-HorizontalAlign="Center">
			<HeaderStyle HorizontalAlign="Center" Width="90px" />
			<ItemTemplate>
				&nbsp;<asp:Label ID="Label2" runat="server" Text='<%# Bind("approvedbyFAALogisticsDate") %>'></asp:Label>
			</ItemTemplate>
		</asp:TemplateField>--%>
		<asp:TemplateField HeaderText="Action" InsertVisible="False" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="10px">
			<HeaderStyle HorizontalAlign="Center" Width="14px" />
			<ItemTemplate>
				&nbsp;
                <a href="javascript:void(0)" class="bt0" id="sendMailBt" onclick="validateForm('<%# Eval("VendorId").ToString() + "|" + Eval("Status").ToString() %>', '<%# Eval("CompanyName").ToString().Replace("'","") %>','<%# Eval("regCountry").ToString() %>')"><span>Send</span></a>
                <%--<asp:LinkButton ID="lnkRefNo1" runat="server" Text='Send Email' CommandArgument='<%# Eval("VendorId").ToString() + "|" + Eval("Status").ToString() %>' CommandName="Details" OnClientClick="javascript:return confirm('Are you sure to Send Email notification to this Vendor?');"></asp:LinkButton>--%>
                <%-- | <%# Eval("Status").ToString()!="8" ? Eval("Status").ToString()!="6" ? "":"Approved" : "Disapproved" %>--%>
			</ItemTemplate>
		</asp:TemplateField>
	</Columns>
</asp:GridView>
<script type="text/javascript">
    function validateForm(id, VendorName, Country) {
        //alert(id);
        $('#dialog-form').data('id', id);
        $('#dialog-form').data('VendorName', VendorName);
        $('.VendorNameHere').html(VendorName);
        $('#CountryCode_Lbl').html(Country);
        $("#dialog-form").dialog("open");
         
    }
</script>
</form>
<br />
<!--BODY CONTENT ENDS-->
<!--##################-->
</div>
</div>
</div><!-- content ends --> 

</asp:Content>