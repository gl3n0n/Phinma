<%@ Page Language="C#" MasterPageFile="~/MasterPageLogin.master" AutoEventWireup="true" CodeFile="login.aspx.cs" Inherits="login" %>



<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content" style="">
    <div class="content_logo" style="background-color: #000000;">
    <img src="images/logo_hck.png" width="369" height="105" border="0" />
    </div>
    <div style="background-color:#ccc; min-height:445px; background:url(images/MG_1379-688x458.jpg) no-repeat; margin-top:20px; " class="rounded-corners1">
    <%--<div style="width:450px;text-align: center; padding:5px 20px; text-transform:uppercase; background-color: #F5BA22; margin:0 0 0 70px; color:#FFF" class="rounded-corners-bottom">
      <h2>Welcome to Yuicon e-Sourcing</h2>
    </div>--%>

        <div style="float:left; width:400px; margin:25px 0 0 30px; padding-top:0px; font-size:14px;">
    <div style="font-size:24px;"><%--Don't Know Where to Look?--%></div>
    <div style="margin-top:25px;">
       <br><br><br><br><br> <%--&raquo;<br>It's a big world out there. Find what you need<br> through the <%= HttpContext.Current.Session["configCompanyName"] %> e-sourcing system.--%>
        &nbsp;</div>

    <div style="color:#666; margin-top:25px;"></div>
    </div>

    <div style="float:left; width:300px; min-height:200px; margin:0px; padding-top:0px; margin-left:180px; padding-left:30px;">
    <asp:MultiView ID="mView" runat="server" ActiveViewIndex="0">
                                <asp:View ID="viewLogin" runat="server">
                                    <div>
                                        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="itemDetails">
                                            <tr>
                                                <th style="height: 20px" align="left" valign="top">
                                                    <h2>Welcome to Trans-Asia <br />e-Sourcing Portal!</h2>
                                                    <br /><br />
                                                    </th>
                                            </tr>
                                            <tr>
                                                <td align="left">
                                                    <div style="text-align:left; ">Username:&nbsp;<asp:RequiredFieldValidator ID="rfvUsername" runat="server" ControlToValidate="txtUserName" ErrorMessage="*" SetFocusOnError="True"></asp:RequiredFieldValidator></div>
                                                        <asp:TextBox runat="server" ID="txtUserName" TabIndex="1" Width="150px" CausesValidation="True" Text="" style="text-align:left; margin:0px; height:20px;"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td align="left">
                                                <div class="clearfix" style="height:10px;"></div>
                                                    <div style="text-align:left; margin:0px; padding:0px">
                                                        Password: &nbsp;<asp:RequiredFieldValidator ID="rfvPassword" runat="server" ControlToValidate="txtPassword" ErrorMessage="*" SetFocusOnError="True"></asp:RequiredFieldValidator></div>
                                                        <asp:TextBox runat="server" ID="txtPassword" TextMode="Password" TabIndex="2" Height="20px" Width="150px" CausesValidation="True" style="text-align:left; margin:0px;" ></asp:TextBox>
                                                        </td>
                                            </tr>
                                            <tr>
                                                <td align="left" style="height: 35px">
                                                    <asp:Label runat="server" ID="txtNote" ForeColor="#FF0000" Style="font-weight: bold"></asp:Label><br /><asp:LinkButton ID="lnkForceLogout" runat="server" OnClick="lnkForceLogout_Click" Visible="False" CausesValidation="false">Yes</asp:LinkButton>  <asp:Label ID="lnkForceSeparator" runat="server" Text=""></asp:Label>
                                                        <asp:LinkButton ID="lnkForceLogoutNo" runat="server" Visible="False" OnClick="lnkForceLogoutNo_Click" CausesValidation="false">No</asp:LinkButton>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
      <div style="margin-top:0px; margin-bottom:10px;"><%--<input type="submit" id="btnLogin" name="btnLogin" value="Login" runat="server" onserverclick="btnLogin_Click" tabindex="3" />--%><input id="btnLogin" name="btnLogin" type="image" src="images/002_11.png" runat="server"  onserverclick="btnLogin_Click" /><input type="reset" id="btnClear" value="Clear" runat="server" onclick="Focus2();" tabindex="4" visible="false" /></div></td>
                                            </tr>
                                            <tr>
                                                <td valign="middle" height="35">
                                                    <asp:LinkButton ID="lnkForgotPwd" runat="server" ToolTip="Click here if you want to retrieve your password" CausesValidation="false" OnClick="lnkForgotPwd_Click" >Forgot your password?</asp:LinkButton>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </asp:View>


                                



                                <asp:View ID="viewForgot" runat="server">
                                    <div>
                                        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="itemDetails">
                                            <tr>
                                                <th height="20px" style="text-align:left">Forgot Password</th>
                                            </tr>
                                            <tr>
                                            <td>
                                                    <div style="text-align:left; ">
                                                        Username: <asp:RequiredFieldValidator ID="rfvUserName2" runat="server" ControlToValidate="txtUserName2" ErrorMessage="*" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                    </div><asp:TextBox runat="server" ID="txtUserName2" TabIndex="1" Width="150px" CausesValidation="True" style="text-align:left; margin:0px;"></asp:TextBox>
                                            </td>
                                            </tr>
                                            <tr>
                                                <td align="left">
                                                    <div style="text-align:left; ">
                                                    <asp:Label runat="server" ID="txtNote2" ForeColor="#FF0000" style="color:Red;font-weight: bold"></asp:Label>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td><div class="clearfix" style="height:10px;"></div><input type="submit" id="btnSend" name="btnSend" value="Send" runat="server" onserverclick="btnSend_Click" tabindex="2" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="middle">
                                                    <asp:LinkButton ID="lnkBackToLogin" runat="server" ToolTip="Click here to go back to login page" CausesValidation="false" OnClick="lnkBackToLogin_Click">Back to Login</asp:LinkButton>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </asp:View>
                                <asp:View ID="viewChangePassword" runat="server">
                                    <div>
                                        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="itemDetails">
                                            <tr>
                                                <th height="20px" style="text-align:left">Change Password</th>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <p style="padding-left: 5px; font-size:10px;">
                                                        This is your first time to login. Please change your password.
                                                        <br />
                                                        Password must be:
                                                        <br />
                                                        &nbsp;?Atleast 7 characters<br />
                                                        &nbsp;?Contains atleast 1 letter<br />
                                                        &nbsp;?Contains atleast 1 number<br />
                                                        &nbsp;?Contains atleast 1
                                                        <asp:Label ID="Label1" runat="server" Text="symbol" ToolTip="Allowed Symbols (-,+,?,*,$,.,|,!,@,#,%,&,_,=,:,)" ForeColor="blue" Style="cursor: hand;"></asp:Label>
                                                        <br />
                                                    </p>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <label><br />
                                                        Current Password:<br />
                                                        <asp:TextBox ID="txtCurrentPwd" runat="server" CausesValidation="True" TabIndex="1" Width="150px" TextMode="Password" style="margin-top:0px; height:20px;"></asp:TextBox>&nbsp;
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtCurrentPwd" ErrorMessage="*" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                    </label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <label><br />
                                                        New Password:&nbsp;<br />
                                                        <asp:TextBox ID="txtNewPwd" runat="server" CausesValidation="True" TabIndex="1" Width="150px" TextMode="Password" style="margin-top:0px; height:20px;"></asp:TextBox>&nbsp;
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtNewPwd" ErrorMessage="*" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                    </label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <label>
                                                        <br />
                                                        Confirm Password:
                                                        <br />
                                                        <asp:TextBox ID="txtConfirmPwd" runat="server" CausesValidation="True" TabIndex="1" Width="150px" TextMode="Password" style="margin-top:0px; height:20px;"></asp:TextBox>&nbsp;
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtConfirmPwd" ErrorMessage="*" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                    </label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left">
                                                    <asp:CompareValidator ID="cvPasswordCompare" runat="server" ErrorMessage="Passwords doesn't match.<br />" Display="Dynamic" ControlToValidate="txtConfirmPwd" ControlToCompare="txtNewPwd"></asp:CompareValidator>
                                                    <asp:CustomValidator ID="cvVerifyPasswords" runat="server" ControlToValidate="txtCurrentPwd" Display="Dynamic" ErrorMessage="Invalid current password.<br />" OnServerValidate="cvVerifyPasswords_ServerValidate"
                                                        SetFocusOnError="True"></asp:CustomValidator></td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <input type="submit" id="btnContinue" name="btnContinue" value="Continue" runat="server" onserverclick="btnContinue_Click" tabindex="2" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="middle">
                                                    &nbsp; &nbsp;&nbsp;
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </asp:View>
                            </asp:MultiView>
    </div>
    

    </div>
    </div>
    </div>
</asp:Content>



<%--
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" width="100%" onload="SetStatus();">
        <tr>
            <td height="150px" valign="top">
                <table width="100%" height="130" border="0" cellpadding="0" cellspacing="0">
                    <tr class="banner">
                        <td style="width: 715px;">
                            <table style="width: 100%;" border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td width="353">
                                        <img border="0" src="web/themes/<%= HttpContext.Current.Session["configTheme"] %>/images/banner.jpg" />
                                    </td>
                                    <td>
                                    <div style="padding-left:10px;">
                                        <div style="font-size: 28px; font-family:Arial, Helvetica, sans-serif; font-weight:bolder; color:#fff">Don't Know Where to Look?</div><br /><br />

                                        <div style="font-size: 14px; font-family:Arial, Helvetica, sans-serif; font-weight:bolder; color:#fff">It's a big world out there. Find what you need<br> through the <%= HttpContext.Current.Session["configCompanyName"] %> e-sourcing system.</div>
                                        </div>
                                    </td>
                                </tr>
                                </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td id="loginContent" valign="top" height="100%">
                <table border="0" cellpadding="0" cellspacing="0" width="100%" height="100%">
                    <tr>
                        <td valign="top" rowspan="2">
                            <p>
                                &nbsp;</p>
                        </td>
                        <td valign="top" style="width: 250px; border-left: dashed 1px #dcdcdc;">
                            
                        </td>
                    </tr>
                    <tr>
                        <td style="border-left: dashed 1px #dcdcdc; height: 200" align="center" valign="top">
                            <div style="font-family: Arial; font-size: 11px; color: Gray; width: 250px;">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>M--%>
