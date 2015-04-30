<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Vendor_LeftNav_Comments.ascx.cs"
	Inherits="web_user_control_Vendor_Vendor_LeftNav_Comments" %>
<table border="0" cellpadding="0" cellspacing="0" width="100%" id="table3">
	<tr>
		<td>			
			<table border="0" cellpadding="0" cellspacing="0" id="pageDetails">
				<tr>
					<th>
						Comments</th>
				</tr>
				<tr>
					<td>
						<p>
							<br />
							<textarea rows="9" name="txtComment" id="txtComment" cols="19" runat="server"></textarea></p>
						<p>
							<input type="button" value="Post Comment" name="btnPost" id="btnPost" runat="server"></p>
					</td>
				</tr>
			</table>
			<table border="0" cellpadding="0" cellspacing="0" id="pageDetails">
				<tr>
					<th>
						Recent comments</th>
				</tr>
				<tr>
					<td>
						&nbsp;<p class="date">
							11 November 2005</p>
						<p>
							Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor
							incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud
							exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.
						</p>
						<p>
							&nbsp;</p>
						<h4>
							Name2</h4>
					</td>
				</tr>
			</table>			
		</td>
	</tr>
</table>
