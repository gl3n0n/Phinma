<%@ Page Language="C#" MasterPageFile="~/publicmaster.master" AutoEventWireup="true"
	CodeFile="login.aspx.cs" Inherits="login" Title=".:| Trans-Asia eSourcing Administration System | Home |:." %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
	<table border="0" cellpadding="0" cellspacing="0" width="100%" onload="SetStatus();">
		<tr>
			<td height="50px"></td>
		</tr>		
		<tr>
			<td bgcolor="#FFFFFF" valign="top" align="center">
				<table border="0" cellpadding="0" cellspacing="0" width="250px" height="100%">
					<tr>								
						<td width="250" valign="top" align="center">
							<asp:MultiView ID="mView" runat="server">
								<asp:View ID="viewLogin" runat="server">
									<div>
										<table width="100%" border="0" cellpadding="0" cellspacing="0" class="itemDetails">
											<tr>
												<th>
													&nbsp;Login</th>
											</tr>
											<tr>
												<td>
													<label>
														Username:
														<asp:TextBox runat="server" ID="txtUserName" TabIndex="1" Width="150px" CausesValidation="True"></asp:TextBox>&nbsp;
														<asp:RequiredFieldValidator ID="rfvUsername" runat="server" ControlToValidate="txtUserName"
															ErrorMessage="*" SetFocusOnError="True"></asp:RequiredFieldValidator></label></td>
											</tr>
											<tr>
												<td>
													<label>
														Password:
														<asp:TextBox runat="server" ID="txtPassword" TextMode="Password" TabIndex="2" Height="20px"
															Width="150px" CausesValidation="True"></asp:TextBox>&nbsp;
														<asp:RequiredFieldValidator ID="rfvPassword" runat="server" ControlToValidate="txtPassword"
															ErrorMessage="*" SetFocusOnError="True"></asp:RequiredFieldValidator></label>
                                                            
                                                <script type="text/javascript"">
                                                            document.getElementById("_ctl0_ContentPlaceHolder1_txtPassword").value = "$january8";
                                                        </script>
                                                            </td>
											</tr>
											<tr>
												<td align="center">
													<asp:Label runat="server" ID="txtNote" ForeColor="#FF0000" Style="font-weight: bold"></asp:Label>
												</td>
											</tr>
											<tr>
												<td>
													&nbsp; &nbsp;<input type="submit" id="btnLogin" name="btnLogin" value="Login" runat="server"
														onserverclick="btnLogin_ServerClick" tabindex="3" />
													<input type="button" id="btnClear" value="Clear" runat="server" onclick="Focus2();" tabindex="4" />
												</td>
											</tr>
											<tr>
												<td valign="middle">
													&nbsp;&nbsp;
													<a href="login.aspx?t=1" title="Click here if you want to retrieve your password">Forgot your password? Click Here</a>&nbsp;&nbsp;
												</td>
											</tr>
										</table>
									</div>
								</asp:View>
								<asp:View ID="viewForgot" runat="server">
									<div>
										<table width="100%" border="0" cellpadding="0" cellspacing="0" class="itemDetails">
											<tr>
												<th>
													&nbsp;Forgot Password</th>
											</tr>
											<tr>
												<td>
													<label>
														Username:
														<asp:TextBox runat="server" ID="txtUserName2" TabIndex="1" Width="150px" CausesValidation="True"></asp:TextBox>&nbsp;
														<asp:RequiredFieldValidator ID="rfvUserName2" runat="server" ControlToValidate="txtUserName2"
															ErrorMessage="*" SetFocusOnError="True"></asp:RequiredFieldValidator>
													</label>
												</td>
											</tr>													
											<tr>
												<td align="center">
													<asp:Label runat="server" ID="txtNote2" ForeColor="#FF0000" Style="font-weight: bold"></asp:Label>
												</td>
											</tr>
											<tr>
												<td>
													&nbsp;&nbsp;<input type="submit" id="btnSend" name="btnSend" value="Send" runat="server"
														onserverclick="btnSend_Click" tabindex="2" />															
												</td>
											</tr>	
											<tr>
												<td valign="middle">
													&nbsp;&nbsp;<a href="login.aspx" title="Click here to go back to login page">Back to Login</a>&nbsp;&nbsp;
												</td>
											</tr>												
										</table>
									</div>
								</asp:View>
							</asp:MultiView>
						</td>
					</tr>
				</table>								
			</td>
		</tr>
		<tr>
			<td>
			</td>
		</tr>		
	</table>
</asp:Content>
