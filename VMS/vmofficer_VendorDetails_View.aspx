<%@ Page Title="" Language="C#" MasterPageFile="~/MasterSubPage.master" AutoEventWireup="true" CodeFile="vmofficer_VendorDetails_View.aspx.cs" Inherits="vmofficer_VendorDetails_View" %>
<%@ Register TagPrefix="Ava" TagName="Tabsnav" Src="usercontrols/tabs.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitlePlaceHolder1" runat="Server">
Trans-Asia VMS Accreditation :: Vendor</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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

<!--Business activities STARTS-->
<div class="separator1"></div>
<h3 style="margin:10px 0px;">Registered Business name</h3>
<form name="form1" id="form1" runat="server">
<asp:label id="errNotification" runat="server" Font-Bold="True"></asp:label> 
<asp:SqlDataSource ID="dsVendorApplicants" runat="server" ConnectionString="<%$ ConnectionStrings:AVAConnectionString %>"
            SelectCommand="SELECT t1.*, (SELECT STUFF((SELECT '; ' + CategoryId FROM rfcProductCategory WHERE CategoryId IN (SELECT CategoryId FROM tblVendorApplicantCategory WHERE VendorApplicantId = @ID) FOR XML PATH ('')), 1, 1, '')) as CategoryName, t3.FirstName+' '+t3.LastName as EndorsedTo1 FROM tblVendorApplicants t1 LEFT JOIN tblUsers t3 ON t3.UserId = t1.EndorsedTo  WHERE t1.ID=@ID " >
    <SelectParameters>
        <asp:SessionParameter Name="ID" SessionField="VendorApplicantId" Type="Int32" />
	</SelectParameters>
</asp:SqlDataSource>
<asp:DetailsView ID="DetailsView1" runat="server" Height="50px" Width="50%" 
    BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" 
    CellPadding="10" DataSourceID="dsVendorApplicants" ForeColor="Black" OnDataBound="DetailsView1_OnDataBound" 
    GridLines="Horizontal" AutoGenerateRows="False" >
    <EditRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
    <Fields>
        <asp:BoundField AccessibleHeaderText="CompanyName" DataField="CompanyName" 
            HeaderText="Company Name" />
        <asp:BoundField AccessibleHeaderText="DateStarted" DataField="DateStarted" 
            HeaderText="Date Started" />
        <asp:BoundField AccessibleHeaderText="FinancialStatement" DataField="FinancialStatement" 
            HeaderText="Financial Statement" />
        <asp:BoundField AccessibleHeaderText="EmailAdd" DataField="EmailAdd" 
            HeaderText="Email" />
        <asp:HyperLinkField DataNavigateUrlFields="LOIFileName" 
            DataTextField="LOIFileName" HeaderText="LOI File" Target="_blank" />
        <asp:BoundField AccessibleHeaderText="CategoryName" DataField="CategoryName" 
            HeaderText="Category" />
        <asp:BoundField AccessibleHeaderText="DateCreated" DataField="DateCreated" 
            HeaderText="Application Date" />
        <asp:BoundField AccessibleHeaderText="ApprovedDt" DataField="ApprovedDt" 
            HeaderText="Date Approved" />
        <asp:BoundField AccessibleHeaderText="EndorsedTo1" DataField="EndorsedTo1" 
            HeaderText="Endorsed To" />
    </Fields>
    <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
    <HeaderStyle BackColor="#333333" Font-Bold="True" ForeColor="White" />
    <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
</asp:DetailsView>
<br /><br />
<h3 style="margin:10px 0px;">Comments</h3>
<asp:Repeater ID="repeaterCommentsProc" runat="server" DataSourceID="dsCommentsProc">
<ItemTemplate>
<p><strong><%# Eval("Firstname")%> <%# Eval("Lastname")%></strong>&nbsp;&nbsp;&nbsp;<em><%# Eval("DateCreated")%></em></p>
<p><%# Eval("Comment")%><br />
  <br />
  </ItemTemplate>
</asp:Repeater>
<asp:SqlDataSource ID="dsCommentsProc" runat="server" ConnectionString="<%$ ConnectionStrings:AVAConnectionString %>"
            SelectCommand="select t1.Comment, t1.DateCreated, t2.FirstName, t2.Lastname from tblCommentsProcurement t1, tblUsers t2 WHERE t2.UserId = t1.UserId AND t1.VendorApplicantId=@VendorApplicantId  ORDER BY t1.DateCreated DESC" >
    <SelectParameters>
        <asp:SessionParameter Name="VendorApplicantId" SessionField="VendorApplicantId" Type="String" />
	</SelectParameters>
  </asp:SqlDataSource>

<br /><br />
<div class="clearfix">&nbsp;</div>
<div style="float:left">
<%--<asp:LinkButton ID="createBt" runat="server" CssClass="bt1" onclientclick="javascript: __doPostBack('createBt', ''); return false;"><asp:Label ID="createBtLbl" runat="server" Text="ENDORSE"></asp:Label></asp:LinkButton>
<asp:LinkButton ID="rejectBt" runat="server" CssClass="bt1" onclientclick="javascript: __doPostBack('rejectBt', ''); return false;"><asp:Label ID="rejectBtLbl" runat="server" Text="REJECT"></asp:Label></asp:LinkButton>--%>
<a href='<%= Session["PrevUrl"] %>' class="bt1" ><span>BACK</span></a>

<%--<a href="procurement_VendorListApproved.aspx" class="bt1" runat="server" id="backBt"><span>BACK</span></a>--%>
</div>
<div style="clear:both"></div>
</form>
<br />
<!--BODY CONTENT ENDS-->
<!--##################-->
</div>
</div>
</div><!-- content ends --> 

</asp:Content>