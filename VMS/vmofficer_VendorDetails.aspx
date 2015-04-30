<%@ Page Title="" Language="C#" MasterPageFile="~/MasterSubPage.master" AutoEventWireup="true" CodeFile="vmofficer_VendorDetails.aspx.cs" Inherits="vmofficer_VendorDetails" %>
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
<asp:SqlDataSource ID="dsVendorApplicants" runat="server" ConnectionString="<%$ ConnectionStrings:AVAConnectionString %>"
            SelectCommand="SELECT t1.*, (SELECT STUFF((SELECT '; ' + CategoryId FROM rfcProductCategory WHERE CategoryId IN (SELECT CategoryId FROM tblVendorApplicantCategory WHERE VendorApplicantId = @ID) FOR XML PATH ('')), 1, 1, '')) as CategoryName FROM tblVendorApplicants t1 WHERE t1.ID=@ID" >
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
            HeaderText="Date Established" />
        <asp:BoundField AccessibleHeaderText="FinancialStatement" DataField="FinancialStatement" 
            HeaderText="Financial Statement" />
        <asp:BoundField AccessibleHeaderText="EmailAdd" DataField="EmailAdd" 
            HeaderText="Email" />
        <asp:HyperLinkField DataNavigateUrlFields="LOIFileName" 
            DataTextField="LOIFileName" HeaderText="LOI File" />
        <asp:BoundField AccessibleHeaderText="CategoryName" DataField="CategoryName" 
            HeaderText="Core Business" />
        <asp:BoundField AccessibleHeaderText="DateCreated" DataField="DateCreated" 
            HeaderText="Application Date" />
    </Fields>
    <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
    <HeaderStyle BackColor="#333333" Font-Bold="True" ForeColor="White" />
    <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
</asp:DetailsView>
<br /><br />



<h3 style="margin:10px 0px;">Approve &amp; Create User Account</h3>
<asp:label id="errNotification" runat="server" Font-Bold="True"></asp:label> 
<table width="100" border="0" cellspacing="0" cellpadding="5">
  <tr>
    <td colspan="3">
    <label for="UserName">Username</label>
    <br /><input name="UserName" type="text" id="UserName" size="60" runat="server" maxlength="60" /><br />
    </td>
  </tr>
  <tr>
    <td colspan="3">
    <label for="UserPassword">Password</label>
    <br /><input name="UserPassword" type="text" id="UserPassword" size="60" runat="server" maxlength="60" />
        <br />
    </td>
  </tr>
  <tr>
    <td class="style1">
      <input type="hidden" name="FirstName" id="FirstName" runat="server" /></td>
    <td width="17%">
<input type="hidden" name="MiddleName" id="MiddleName" runat="server" />
      </td>
    <td width="65%">
<input type="hidden" name="LastName" id="LastName" runat="server" /></td>
  </tr>
  <tr>
    <td class="style1"><label for="regStreetName">Company Name</label>
      <input type="text" name="CompanyName" id="CompanyName" runat="server" readonly="readonly"  /></td>
    <td><label for="EmailAdd">Email</label>
      <input type="text" name="EmailAdd" id="EmailAdd" runat="server"  readonly="readonly" /></td>
    <td><%--<label for="EmailAdd">User Type</label><br />
      <asp:DropDownList ID="UserTypes" runat="server" DataSourceID="dsUserTypes" DataTextField="UserTypeDesc" DataValueField="UserType">
        </asp:DropDownList>
      <asp:SqlDataSource ID="dsUserTypes" runat="server" ConnectionString="<%$ ConnectionStrings:AVAConnectionString %>"
            SelectCommand="SELECT '' as  UserType, 'SELECT A USERTYPE' as UserTypeDesc, '' as DateCreated UNION SELECT * FROM rfcUserTypes ORDER BY UserType" >
	    </asp:SqlDataSource>--%>
      </td>
  </tr>
  </table>
  <br />
<h3 style="margin:10px 0px;">Comments</h3>
<asp:Repeater ID="repeaterCommentsProc" runat="server" DataSourceID="dsCommentsProc">
<ItemTemplate>
<p><strong><%# Eval("Firstname")%> <%# Eval("Lastname")%></strong>&nbsp;&nbsp;&nbsp;<em><%# Eval("DateCreated")%></em></p>
<p><%# Eval("Comment")%><br />
  <br />
  </ItemTemplate>
</asp:Repeater>
<asp:SqlDataSource ID="dsCommentsProc" runat="server" ConnectionString="<%$ ConnectionStrings:AVAConnectionString %>"
            SelectCommand="select t1.Comment, t1.DateCreated, t2.FirstName, t2.Lastname from tblCommentsProcurement t1, tblUsers t2 WHERE t2.UserId = t1.UserId AND t1.VendorApplicantId = @VendorApplicantId" >
    <SelectParameters>
        <asp:SessionParameter Name="VendorApplicantId" SessionField="VendorApplicantId" Type="String" />
	</SelectParameters>
  </asp:SqlDataSource>
<textarea name="Comment" cols="58" rows="6" id="Comment" style="font-family:Arial, Helvetica, sans-serif; color:#666" runat="server"></textarea>
<br /><br /><br />
<h3>Endorsed To:</h3>
<br />
    <asp:DropDownList ID="EndorsedTo" runat="server" DataSourceID="dsCommodityBuyers" DataTextField="FullName" DataValueField="UserId"></asp:DropDownList>
    <asp:SqlDataSource ID="dsCommodityBuyers" runat="server" ConnectionString="<%$ ConnectionStrings:AVAConnectionString %>"
            SelectCommand="select t1.FirstName+' '+t1.LastName as FullName, t1.UserId
from tblUsers t1
where t1.UserId in (select t2.UserId from tblUserTypes t2 where t2.UserType=12) and Status = 1" >
  </asp:SqlDataSource>
<%--<input name="EndorsedBy" type="text" id="EndorsedBy" size="60" runat="server" />--%>
<br /><br />
<div class="clearfix">&nbsp;</div>
<div style="float:left">
<asp:LinkButton ID="createBt" runat="server" CssClass="bt1" onclientclick="javascript:if(confirm('Are you sure to Approve and Create user to this Applicant?')){ __doPostBack('createBt', ''); } return false;"><asp:Label ID="createBtLbl" runat="server" Text="APPROVE & CREATE USER"></asp:Label></asp:LinkButton>
<asp:LinkButton ID="rejectBt" runat="server" CssClass="bt1" onclientclick="javascript:if(confirm('Are you sure to Reject this Applicant?')) if($('#ContentPlaceHolder1_Comment').val()!=''){  __doPostBack('rejectBt', ''); }else{ alert('Comment must have a value.'); $('#ContentPlaceHolder1_Comment').focus(); } return false;"><asp:Label ID="rejectBtLbl" runat="server" Text="REJECT"></asp:Label></asp:LinkButton>
<a href="vmofficer_NewApplicants_List.aspx" class="bt1" runat="server" id="cancelBt"><span>BACK</span></a>

<a href="vmofficer_NewApplicants_List.aspx" class="bt1" runat="server" id="backBt"><span>BACK</span></a>
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