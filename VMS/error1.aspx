<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" %>

<%-- Add content controls here --%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content">
    <div class="content_logo">
    <img src="images/final_logo_w.png"  border="0" />
    </div>
    <div style="background:#FFF; min-height:445px;" class="rounded-corners">
    <div style="width:700px; padding:5px 20px; text-transform:uppercase; background:#67893F; margin:0 0 0 70px; color:#FFF" class="rounded-corners-bottom">
      <h1>Welcome to Trans-Asia VMS Accreditation </h1>
    </div>
    <form runat="server" id="form1" name="form1" action="#" method="post"> 
    <div style="float:left; width:370px; min-height:300px; border-right:#ccc 1px solid; margin:25px 0 0 100px; padding-top:30px; padding-right:10px;">
    <asp:Label runat="server" ID="txtNote" ForeColor="#FF0000" Style="font-weight: bold"></asp:Label>
        <h2>
									SORRY FOR THE INCONVENIENCE
								</h2>
								<h3>									
									Server Is Temporarily Unavailable</h3>
      <div class="clearfix"></div>
      <div style="margin-top:25px;">
      <%--<div style="float:left; margin:5px 20px 0 0;"> <a href="#" style="font-size:12px">Forgot your password?</a></div>--%>
          <p>Click here login back.</p>
      <!--<input name="" type="image" src="images/002_11.png" onclick="window.location='login.aspx'" />-->
          <div class="clearfix" style="font-size:18px;"><a href="login.aspx" style="text-decoration:none;">LOGIN</a></div>
      </div>
      <div class="clearfix" style="font-size:18px;"><br /><br /><p>New Vendor? <a href="vendor_signup.aspx" style="text-decoration:none;">Sign up here.</a></p>
          <div style="float:none; width:400px; margin:5px 0 0 0px; padding-top:30px; padding-right:10px; font-size:14px;">
    <%--<div style="font-size:16px;">
        Log in to access accreditation <br />
    status and manage Vendors <br />
    request for Trans-Asia accreditation.</div>
    <div style="margin-top:25px;">
        &raquo; Manage accreditations requests for approval<br />

    &raquo; Review, approve or disapprove request for accreditations</div>--%>

    <div style="color:#666; margin-top:25px;">
        <%--Dont have Trans-Asia VMS Accreditation 
        account yet? <br /><a href="#" style="text-decoration:none;">Sign up here.</a>--%></div>
    </div>
        </div>
      </div>
      </form>
        
    <div class="clearfix"></div>
    </div>
    </div>
    <div class="clearfix"></div>
    </div><!-- content ends -->
    <script>$(document).ready(function () { $("#ContentPlaceHolder1_txtUserName").focus().select(); });</script>
    <div class="clearfix"></div>
</asp:Content>