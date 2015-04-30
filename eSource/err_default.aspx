<%@ Page Language="C#" %>
<html>
<head>
	<meta http-equiv='Content-Language' content='en-us'>
	<meta http-equiv='Content-Type' content='text/html; charset=windows-1252'>
	<style type='text/css'>
		body{font-family:Arial;margin:0px;background-color:#fff;height:250px;}	
		p{font:Arial;font-size:11px;text-align:left;padding-right:10px;padding-left:0px;}
		.tabs a{display: visible;background-image:url(web/images/TabBGactive.jpg);background-repeat:repeat-x;width:100%;color:#ffffff;font-weight:bold;font-size:11px;padding:10px;background-color:#E0DFE3;border-right:solid 1px #fff;border-top:solid 1px gray;border-bottom:solid 3px #fff;}
		.tabs a:hover{background-image:url(web/images/TabBGhover.jpg);background-repeat:repeat-x;font-size:11px;width: 100%;color: #000000;padding:10px;background-color:#778899;}
		.activeTab a{background-image:url(web/images/TabBG.jpg);background-repeat:repeat-x;width:100%;color:#fff;font-weight:bold;padding:10px;background-color:#3399CC;border-top:#778899;}
		.activeTab a:hover{background-image: url(web/images/TabBGactive.jpg);background-repeat: repeat-x;background-color: #3399cc;width: 100%;color: #ffffff;padding: 10px;border-top: #778899;}
		.table {border-width: 0px;}
		#page{position:absolute;width:100%;vertical-align:top;height:100%;border:solid 1px #999999;background-color:#FFFFFF;}
		#content{background-image:url(web/images/contentCorner.jpg);background-repeat:no-repeat;vertical-align:top;padding-left:20px;padding-right:10px;}
		#content a{font-size:11px;color:#003399;}
		#content a:hover{background-color:none;font-size:11px;text-decoration:none;}			
		#masthead{padding-top:10px;}
		#masthead h1{font-size:16px;color:#3399CC;padding-left:20px;margin-bottom:-10px;}
		#masthead a{font-size:11px;font-weight:bold;color:#cc0000;padding-left:2px;padding-right:2px;}
		#footer{color:white;font-weight:bold;padding-top:10px;padding-left:10px;padding-bottom:10px;font-size:10px;border:solid 1px #999999;background-color:gray;}
		#footer a{color:white;}
	</style>
</head>
<body style="height: 100%;">
	<div>
		<table border='0' cellpadding='0' cellspacing='0' id='page'>
			<tr>
				<td valign='top' style='height: 135px'>
					<table border='0' cellpadding='0' cellspacing='0' width='100%'>
						<tr>
							<td>
								<div align='left' id='masthead'>
									<table cellpadding='0' cellspacing='0' id='table1'>
										<tr>
											<td>
												<h1>
													<img border='0' src='web/images/logo.jpg' width='151' height='63'></h1>
												<br />
											</td>
										</tr>
									</table>
								</div>
							</td>
						</tr>
						<tr>
							<td height='100%'>
								<table cellpadding='0' cellspacing='0' width='100%' id='table2'>
									<tr>
										<td class='tabs' width='25%'>
											<a href='login.aspx' title='Yuicon eBid Home'>Home</a></td>									
										<td class='tabs' width='25%'>
											<a href='about.aspx' title='About Yuicon eBid'>About</a></td>
										<td class='tabs' width='25%'>
											<a href='help.aspx' title='Help on Yuicon eBid'>Help</a></td>
										<td class='tabs' width='25%'>
											<a href='faqs.aspx' title='Yuicon eBid FAQs'>FAQs</a></td>
									</tr>
								</table>
							</td>
						</tr>
					</table>
				</td>
			</tr>
			<tr>
				<td class='content' height='100%' style='padding-left: 15px; padding-right: 15px;'>
					<table cellpadding='0' cellspacing='0' height='100%' width='100%'>
						<!--CONTENT STARTS HERE-->
						<tr>
							<td align='center'>
								<h2>
									SORRY FOR THE INCONVENIENCE
								</h2>
								<h3>									
									Server Is Temporarily Unavailable</h3>
							</td>
						</tr>																		
						<!--CONTENT ENDS HERE-->
					</table>
				</td>
			</tr>
			<tr>
				<td height='10px'>
				</td>
			</tr>
			<tr>
				<td id='footer' height='50px'>
					Yuicon E-Sourcing System<br />
					Copyright � 2008 Yuicon Unlimited Concepts, Inc. All rights reserved.<br />
					<a id="_ctl0_footer_hlSitemap" href="sitemap.aspx">Sitemap</a>
					- <a id="_ctl0_footer_hlTerms" href="termsofuse.aspx">Terms of Use</a> 
					- <a id="_ctl0_footer_hlPrivacy" href="policy.aspx">Privacy Policy</a>
				</td>
			</tr>
		</table>
	</div>
</body>
</html>
