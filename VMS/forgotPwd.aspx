<%@ Page MasterPageFile="~/MasterPage.master" Language="C#" AutoEventWireup="true" CodeFile="forgotPwd.aspx.cs" Inherits="forgotPwd" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitlePlaceHolder1" runat="Server">
Globe Automated Vendor Accreditation :: Forgot Password
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="content">
    <div class="content_logo">
        <img src="images/logo_hck.png" width="229" height="105" border="0" />
    </div>
    <div style="width:700px; padding:5px 20px; background:#29c5ee; margin: 20px 0 0 20px; color:#FFF" class="rounded-corners">
      <h1>Forgot Password </h1>
    </div>
    <form runat="server" id="form1" name="form1">
        <div style="float:left; width:600px; min-height:200px; margin:25px 0 0 50px; background-color:White; padding: 30px 30px 30px 30px">
            <asp:Label runat="server" ID="txtNote" ForeColor="#FF0000" Style="font-weight: bold"></asp:Label>
            <asp:TextBox ID="txtUserName" runat="server" CausesValidation="True" value="ENTER YOUR USERNAME" onclick="if($(this).val()=='ENTER YOUR USERNAME'){ $(this).select(); } " onblur="if($(this).val()==''){ $(this).val('ENTER YOUR USERNAME'); }" ></asp:TextBox>
        <div class="clearfix"></div>
	<br />
        <div style="float:none">
             <input id="Image1" name="" type="image" src="images/002_11a.png" runat="server" onserverclick="btnForgotPwd_Click"/>
        </div>
        <div style="float:none; margin:5px 20px 0 0; font-size:14px;"> <a href="login.aspx" style="font-size:14px">Back to Login Page</a></div>
        </div>
    </form>
    </div>
</asp:Content>