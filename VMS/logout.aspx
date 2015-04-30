<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="logout.aspx.cs" Inherits="logout" %>

<%-- Add content controls here --%>
<asp:Content ID="Title1" ContentPlaceHolderID="TitlePlaceHolder1" runat="server"><%= ConfigurationManager.AppSettings["ApplicationTitle"].ToString() %></asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content" style="width:550px;">
    <div class="content_logo">
    &nbsp;</div>
    <div style="background:#FFF; min-height:445px;" >
    <div style="text-transform:uppercase; background:#242424 ; margin:auto; padding-top:10px; color:#FFF; text-align:center;" >
            <img src="images/final_logo_w.png"  border="0" />
      <h3 style="background-color:#666; padding:5px; margin-top:10px;">Welcome to <%= ConfigurationManager.AppSettings["ApplicationTitle"].ToString() %></h3>
    </div>
    <form runat="server" id="form1" name="form1" action="#" method="post"> 
    <div style="width:280px; min-height:300px; margin:auto; padding-top:30px; text-align:center">
    <asp:Label runat="server" ID="txtNote" ForeColor="#FF0000" Style="font-weight: bold"></asp:Label>
<div style="float:none; margin:auto; font-size:15px; color:#1541ce; font-weight:bold;">You have succesfully logout</div>
<div style="float:none; margin:auto; font-size:10px; color:#9b9b9b;">Click here to login back</div>
      
      <div style="margin-top:25px;">
      <%--<div style="float:left; margin:5px 20px 0 0;"> <a href="#" style="font-size:12px">Forgot your password?</a></div>--%>
      <div style="float:none; margin:auto;clear:both;">
      <%--<input name="" type="image" src="images/002_11.png" runat="server"  onserverclick="btnLogin_Click" />--%>
<a href="login.aspx" Class="bt1" style="float:none; "><span>LOGIN</span></a>
          
      <div class="clearfix"></div>
      </div><br />
          <div style="float:none; margin:auto; font-size:18px;"><a href="forgotpassword.aspx" style="font-size:14px">Forgot your password?</a></div>
      </div>
      <div class="clearfix" style="font-size:14px;"><br /><br /><p><a href="vendor_signup.aspx" style="text-decoration:none;">Sign up here for New / Renewal</a></p>
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
	<!--<table width="135" border="0" cellpadding="2" cellspacing="0" title="Click to Verify - This site chose Symantec SSL for secure e-commerce and confidential communications.">
<tr>
<td width="135" align="center" valign="top"><script type="text/javascript" src="https://seal.verisign.com/getseal?host_name=https://gtva.Trans-Asia.com.ph/&amp;size=L&amp;use_flash=NO&amp;use_transparent=NO&amp;lang=en"></script><br />
<a href="http://www.symantec.com/ssl-certificates" target="_blank"  style="color:#000000; text-decoration:none; font:bold 7px verdana,sans-serif; letter-spacing:.5px; text-align:center; margin:0px; padding:0px;">ABOUT SSL CERTIFICATES</a></td>
</tr>
</table>-->
        
    <div class="clearfix"></div>
    </div>
    </div>
    <div class="clearfix"></div>
    </div><!-- content ends -->
    <script>$(document).ready(function () { $("#ContentPlaceHolder1_txtUserName").focus().select(); });</script>
    <div class="clearfix"></div>
</asp:Content>