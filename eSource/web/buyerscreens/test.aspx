<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml" style="height: 100%;">
<head>
<%@ Register TagPrefix="EBid" TagName="jqueryLoader" Src="../usercontrol/jQuery_loader.ascx" %>
    <EBid:jqueryLoader runat="server" ID="jqueryLoader1" />
    <title id="PageTitle"></title>
    <meta http-equiv="Content-Language" content="en-us" />
    <meta http-equiv="Content-Type" content="text/html; charset=windows-1252" />
    
    <link href='<%= "../themes/"+Session["configTheme"]+"/css/style.css" %>' rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href='<%= "../themes/"+Session["configTheme"]+"/css/style.css" %>' />
    <link rel="stylesheet" type="text/css" href='<%="../themes/"+Session["configTheme"]+"/css/style_buyer.css" %>' />

    <script type="text/javascript" src="../../jquery/jquery-1.7.1.min.js"></script>
    <script type="text/javascript" src="../../jquery/cal.js"></script>
    <link rel="stylesheet" type="text/css" href="../../jquery/calendar_picker.css' />
    <script type="text/javascript">
        jQuery(document).ready(function () {
            $('input.date').simpleDatepicker({ startdate: '6/10/1900' });
        });
        function reloadDatepicker() {
            $('input.date').simpleDatepicker({ startdate: '6/10/1900' });
        }
    </script>  

    <script type="text/javascript" src="../../jquery/jquery.numeric.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $(".numeric").numeric();
            $(".integer").numeric(false, function () { alert("Integers only"); this.value = ""; this.focus(); });
        });
        function reloadNumeric() {
            $(".numeric").numeric();
            $(".integer").numeric(false, function () { alert("Integers only"); this.value = ""; this.focus(); });
        }
    </script>

    <script type="text/javascript" src="../../jquery/jquery.table.addrow.js"></script>



    <style type="text/css">
        .ui-widget { font-family: Arial; font-size: 11; }
        </style>
</head>
<body id="Body1">
    <div>
        <form name="form1" method="post" action="vsfcreate.aspx" id="form1" enctype="multipart/form-data">
<div>
<input type="hidden" name="__EVENTTARGET" id="__EVENTTARGET" value="" />

<input type="hidden" name="__EVENTARGUMENT" id="__EVENTARGUMENT" value="" />
<input type="hidden" name="__VIEWSTATE" id="__VIEWSTATE" value="/wEPDwUKLTc0ODE3NzgzNQ9kFgICAw9kFgICAQ9kFgYCBQ9kFgICAw8PZBYCHgdPbkNsaWNrBQ5TaG93UmVzdWx0cygxKWQCCQ9kFgICAQ8PFgIeBFRleHQFJFRvZGF5IGlzIE1hcmNoIDA2LCAyMDEyICAwOTo0MDo1NSBQTWRkAikPZBYEAgEPDxYCHgtOYXZpZ2F0ZVVybAURfi90ZXJtc29mdXNlLmFzcHhkZAIDDw8WAh8CBQ1+L3BvbGljeS5hc3B4ZGRkOpZr8Fx+pH+Pucy8iWFv3FfLGTKoUoUZbV2q0b/7kAI=" />
</div>



<div>

	<input type="hidden" name="__EVENTVALIDATION" id="__EVENTVALIDATION" value="/wEWGQLEqJTaBgKXh8mjAgLowqJjApH3hZoCAtGP9IwBAoC6/dIOAoG6/dIOAoK6/dIOAoO6/dIOAoS6/dIOAoW6/dIOAoa6/dIOApe6/dIOAsuq/ZUCAoCSopQMAtikwOsKAuT7i7AJAueW4bQBAtGJtq4IAouan4kJApzopKEDAtSSvvwJAoiB9cYLAp2PlYELAt3SitQIfF+uVCNPx+VLvImqe9lPuTsFhG3f9bAAZ2RxWLNBFCM=" />
</div>
                                                                            <input name="Hidden2" type="hidden" id="Hidden2" />

																			<input name="Hidden1" type="hidden" id="Hidden1" />

                                                                            &nbsp;
			<table border="0" cellpadding="0" cellspacing="0" width="100%" id="page">
                <tr>
                    <td valign="top" height="137px">
                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td>
                                    <div align="left" id="masthead">
                                        

<link type="text/css" href='<%= "../themes/"+Session["configTheme"]+"/css/style.css" %>' rel="stylesheet" />


<table border="0" cellpadding="0" cellspacing="0" width="100%" id="table2">
	<tr>
		<td align="left" width="150px">
			<img border="0" src="../images/logo.jpg" width="150px" height="63">
		</td>
		<td>&nbsp;

			
		</td>
		<td class="globalLinks" align="right" width="360px">
			<p>
				<input name="GlobalLinksNav$tbSearch" type="text" id="GlobalLinksNav_tbSearch" style="height:20px;width:130px;margin:0px; padding:0px;" />
				<input type="button" name="GlobalLinksNav$btnSearch" value="Search" onclick="ShowResults(1);__doPostBack(&#39;GlobalLinksNav$btnSearch&#39;,&#39;&#39;)" id="GlobalLinksNav_btnSearch" style="margin:0px; padding:0px;" />
				<select name="GlobalLinksNav$ddlSearchOpt" id="GlobalLinksNav_ddlSearchOpt" style="margin:0px; padding:0px;">
	<option value="1">Bid Events</option>

	<option value="2">Bid Reference No.</option>
	<option value="3">Bid Event PR Number</option>
	<option value="4">Auction Events</option>
	<option value="5">Auction Reference No.</option>
	<option value="6">Auction Event PR Number</option>
	<option value="7">Product</option>

	<option value="8">Supplier</option>

</select>		
				&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;				
				<a id="GlobalLinksNav_lnkAbout" href="../../about.aspx">About Trans-Asia E-Sourcing Sytem</a>
				&nbsp;|&nbsp;				
				<a id="GlobalLinksNav_lnkHelp" href="../../help.aspx">Help</a>
				&nbsp;|&nbsp;				
				<a id="GlobalLinksNav_lnkFaqs" href="../../faqs.aspx">FAQs</a>

				&nbsp;|&nbsp;
				<a id="GlobalLinksNav_lnkLogout" href="../../logout.aspx">Log Out</a>				
			</p>
		</td>
	</tr>
</table>

                                    </div>
                                </td>

                            </tr>
                            <tr>
                                <td>
                                    
<link type="text/css" href="../../css/style.css' rel="stylesheet" />
<table border="0" cellpadding="0" cellspacing="0" width="100%" id="table2" class="topnav">
	<tr>
		<td class="tabs">			
			<a id="TopNavBids1_lnk1" href="index.aspx">Home</a>
		</td>

		<td class="tabs">
			<a id="TopNavBids1_lnk2" href="bids.aspx">Bids</a>			
		</td>
		<td class="tabs">
			<a id="TopNavBids1_lnk3" href="auctions.aspx">Auctions</a>			
		</td>
		<td class="activeTab">
			<a id="TopNavBids1_lnk4" href="suppliers.aspx">Suppliers</a>			
		</td>

		<td class="tabs">
			<a id="TopNavBids1_lnk5" href="product.aspx">Products</a>			
		</td>
		<td class="tabs">
			<a id="TopNavBids1_lnk6" href="report_savingsbybidevent.aspx">Reports</a>			
		</td>
		<td>&nbsp;</td>
	</tr>
</table>

                                </td>
                            </tr>
                            <tr>
                                <td>
                                    

<script type="text/javascript">
    (function ($) {
        $.fn.jclock = function (options) {
            var version = '1.2.0';

            // options
            var opts = $.extend({}, $.fn.jclock.defaults, options);

            return this.each(function () {
                $this = $(this);
                $this.timerID = null;
                $this.running = false;

                var o = $.meta ? $.extend({}, opts, $this.data()) : opts;

                $this.timeNotation = o.timeNotation;
                $this.am_pm = o.am_pm;
                $this.utc = o.utc;
                $this.utc_offset = o.utc_offset;

                $this.css({
                    fontFamily: o.fontFamily,
                    fontSize: o.fontSize,
                    backgroundColor: o.background,
                    color: o.foreground
                });

                $.fn.jclock.startClock($this);

            });
        };

        $.fn.jclock.startClock = function (el) {
            $.fn.jclock.stopClock(el);
            $.fn.jclock.displayTime(el);
        }
        $.fn.jclock.stopClock = function (el) {
            if (el.running) {
                clearTimeout(el.timerID);
            }
            el.running = false;
        }
        $.fn.jclock.displayTime = function (el) {
            var time = $.fn.jclock.getTime(el);
            el.html(time);
            el.timerID = setTimeout(function () { $.fn.jclock.displayTime(el) }, 1000);
        }
        $.fn.jclock.getTime = function (el) {
            var now = new Date();
            var hours, minutes, seconds;

            if (el.utc == true) {
                var localTime = now.getTime();
                var localOffset = now.getTimezoneOffset() * 60000;
                var utc = localTime + localOffset;
                var utcTime = utc + (3600000 * el.utc_offset);
                now = new Date(utcTime);
            }
            hours = now.getHours();
            minutes = now.getMinutes();
            seconds = now.getSeconds();

            var am_pm_text = '';
            (hours >= 12) ? am_pm_text = " P.M." : am_pm_text = " A.M.";

            if (el.timeNotation == '12h') {
                hours = ((hours > 12) ? hours - 12 : hours);
            } else if (el.timeNotation == '12hh') {
                hours = ((hours > 12) ? hours - 12 : hours);
                hours = ((hours < 10) ? "0" : "") + hours;
            } else {
                hours = ((hours < 10) ? "0" : "") + hours;
            }

            minutes = ((minutes < 10) ? "0" : "") + minutes;
            seconds = ((seconds < 10) ? "0" : "") + seconds;

            var timeNow = hours + ":" + minutes + ":" + seconds;
            if ((el.timeNotation == '12h' || el.timeNotation == '12hh') && (el.am_pm == true)) {
                timeNow += am_pm_text;
            }

            return timeNow;
        };

        // plugin defaults
        $.fn.jclock.defaults = {
            timeNotation: '24h',
            am_pm: false,
            utc: false,
            fontFamily: '',
            fontSize: '',
            foreground: '',
            background: '',
            utc_offset: 0
        };

    })(jQuery);
</script>

<table border="0" cellpadding="0" cellspacing="0" class="tasks">

    <tr>
        <td style="padding-left: 5px; text-align: left;">
            <span id="TopNav3_lblDate" style="font-size:12px;font-weight:bold;">Today is March 06, 2012  09:40:55 PM</span>
            <span id="TopNav3_lblTime" style="font-size:12px;font-weight:bold;"></span>
        </td>
        <td style="width: 10px;">
        </td>
        <td style="padding-right: 10px; text-align: right;">

            <a id="TopNav3_lnkCreateNewItem" href="javascript:__doPostBack(&#39;TopNav3$lnkCreateNewItem&#39;,&#39;&#39;)">Create New Bid Event</a>
        </td>
    </tr>
</table>

                                </td>
                            </tr>
                        </table>
                    </td>

                </tr>
                <tr>
                    <td>
                        <table border="0" cellpadding="0" cellspacing="0" width="100%" height="100%">
                            <tr>
                                <td id="relatedInfo">
                                    <h2>Bid Events</h2>
                                    <div align="left">

                                        
<LINK type="text/css" href="../../css/style.css' rel=stylesheet/>
<table border="0" cellpadding="0" cellspacing="0" width="100%" id="related">
	<tr>
		<td>
			<a id="LeftNav_HyperLink1" href="suppliers.aspx">Accredited Suppliers</a>			
		</td>
    </tr>    
</table>
<h2>Vendor Shortlisting Form</h2>
<table border="0" cellpadding="0" cellspacing="0" width="100%" class="related">

	<tr>
		<td>
			<a id="LeftNav_HyperLink2" href="vsfcreate.aspx">Create VSF</a>		
		</td>
	</tr>
	<tr>
		<td>
			<a id="LeftNav_HyperLink13" href="vsfdratfs.aspx">Draft VSF</a>		
		</td>

	</tr>
	<tr>
		<td>
			<a id="LeftNav_HyperLink4" href="vsfendorsed.aspx">Endorsed VSF</a>		
		</td>
	</tr>	
	<tr>
		<td>
			<a id="LeftNav_HyperLink5" href="vsfawarded.aspx">Awarded VSF</a>		
		</td>

	</tr>	
	<tr>
		<td>
			<a id="LeftNav_HyperLink6" href="vsfclarifications.aspx">VSF for Clarifications</a>		
		</td>
	</tr>	
</table>
                                    </div>
                                    <p>&nbsp;</p>
                                </td>

                                <td id="content">
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                        <tr>
                                            <td valign="top">
												<h1><br />Vendor Shortlisting Form</h1>
                                                <script type="text/javascript">
                                                    $(document).ready(function () {
                                                        $('#relatedInfo').hide();
                                                    });
                                                 </script>
												<p><a href="javascript:void(0)" onclick="$('#relatedInfo').toggle()">Show/Hide Left Panel</a><span id="lblMessage" style="color:Red;font-size:11px;"></span></p>

                                                <table cellspacing="5" style="width:100%;">
                                                    <tr>
                                                        <td colspan="2" valign="top">
                                                        <table width="25%" border="1" cellpadding="0" cellspacing="0" rules="all" class="itemDetails" style="border-color:#71A9D2; font-family: Arial; font-size: 11px; border-collapse: collapse; background-color: #F4F1C4; height:100%">
																<tbody>
																	<tr>
																		<td colspan="4" class="ui-widget-header" style="height:26px; vertical-align:middle;">Vendor Shortlisting Form</td>
																	</tr>

																	<tr valign="middle">
																		<td align="left" valign="middle" style="background-color:#FFFFFF; width:15%;">Date</td>
																		<td align="left" valign="middle" bgcolor="#FFFFFF" style="width:35%;">
																			<input name="VSFDate" type="text" id="VSFDate" class="date" style="font-weight:bold;width:255px;" />
																		</td>
																		<td align="left" valign="middle" bgcolor="#FFFFFF" style="width:15%;">Proponent's Name</td>
																		<td align="left" valign="middle" bgcolor="#FFFFFF" style="width:35%;">
																			<input name="ProponentName" type="text" id="ProponentName" style="font-weight:bold;width:255px;" />

																		</td>
																	</tr>
																	<tr valign="middle">
																		<td align="left" valign="middle" bgcolor="#FFFFFF" >PR No.</td>
																		<td align="left" valign="middle" bgcolor="#FFFFFF" >
																			<input name="PRNo" type="text" id="PRNo" style="font-weight:bold;width:255px;" />
																		</td>
																		<td align="left" valign="middle" bgcolor="#FFFFFF" >Group / Department</td>

																		<td align="left" valign="middle" bgcolor="#FFFFFF">
																			<input name="GroupDept" type="text" id="GroupDept" style="font-weight:bold;width:255px;" />
																		</td>
																	</tr>
																	<tr valign="middle">
																		<td align="left" valign="middle" bgcolor="#FFFFFF" >Project Name</td>
																		<td align="left" valign="middle" bgcolor="#FFFFFF" >
																			<input name="ProjectName" type="text" id="ProjectName" style="font-weight:bold;width:255px;" />

																		</td>
																		<td align="left" valign="middle" bgcolor="#FFFFFF" >No. of potential vendors</td>
																		<td align="left" valign="middle" bgcolor="#FFFFFF">
																			<input name="NumPotentialVendor" type="text" id="NumPotentialVendor" class="integer" style="font-weight:bold;width:255px;" />
																		</td>
																	</tr>
																	<tr valign="middle">
																		<td align="left" valign="middle" bgcolor="#FFFFFF" >PR Amount</td>

																		<td align="left" valign="middle" bgcolor="#FFFFFF" >
																			<input name="PRAmount" type="text" id="PRAmount" class="numeric" style="font-weight:bold;width:255px;" />
																		</td>
																		<td align="left" valign="middle" bgcolor="#FFFFFF" >No. of short-listed vendors</td>
																		<td align="left" valign="middle" bgcolor="#FFFFFF">
																			<input name="NumShortlistedVendor" type="text" id="NumShortlistedVendor" class="integer" style="font-weight:bold;width:255px;" />
																		</td>
																	</tr>

																	<tr valign="middle">
																		<td align="left" valign="middle" bgcolor="#FFFFFF" >PR Description</td>
																		<td align="left" valign="middle" bgcolor="#FFFFFF" >
																			<input name="PRDescription" type="text" id="PRDescription" style="font-weight:bold;width:255px;" />
																		</td>
																		<td align="left" valign="middle" bgcolor="#FFFFFF" >&nbsp;</td>
																		<td align="left" valign="middle" bgcolor="#FFFFFF">&nbsp;</td>
																	</tr>

																</tbody>
															</table>
                                                        </td>
                                                     </tr>
                                                    
                                                    <tr>
														<td colspan="2" valign="top">


															<table width="100%" border="1" cellpadding="0" cellspacing="0" rules="all" class="itemDetails" id="Biddetails_details1_dvEventDetails11" style="border-color:#71A9D2; font-family: Arial; font-size: 11px; border-collapse: collapse; background-color: #F4F1C4; height:100%">
																<tbody>

																	<tr>
																		<td colspan="4" class="ui-widget-header" style="height:26px; vertical-align:middle;">PURCHASING</td>
																	</tr>
																	<tr valign="middle" >
																		<td width="25%" align="center" valign="middle" style="padding:5px; font-size:12px;">
																			<table width="150" border="0" cellspacing="0" cellpadding="0">
																				<tr>
																					<td>Prepared by / date: <br /></td>

																				</tr>
																				<tr>
																					<td>&nbsp;</td>
																				</tr>
																				<tr>
																					<td><strong>BUYER</strong></td>
																				</tr>
																				<tr>

																					<td style="border-top:2px #000 solid;">Buyer</td>
																				</tr>
																			</table>
																		</td>
																		<td width="25%" align="center" valign="middle" style="padding:5px; font-size:12px;">
																			<table width="150" border="0" cellspacing="0" cellpadding="0">
																				<tr>
																					<td colspan="2">Reviewed by / date: <br />&nbsp;</td>

																				</tr>
                                                                                
															                    <tr>
														                            <td colspan="2"><strong>PURCHASING_APPROVER</strong><input type="hidden" name="approver4" id="approver4" />
                                                                                    </td>
																				</tr>
																				<tr>
				                                                                    <td colspan="2" style="border-top:2px #000 solid;">Purchasing Approver</td>
					                                                            </tr>

																			</table>
																		</td>
							                                        </tr>
																</tbody>
															</table>



                                                            </td>
                                                    </tr>

                                                </table>
												<br />
												
                                                <table border="0" cellpadding="0" cellspacing="0" width="100%" id="actions">
                                                    <tr>
                                                        <td align="right">
                                                            <input id="btnCancel" type="button" value="Cancel BAC" onclick="if(confirm('This will delete the current session. Continue?')){ document.getElementById('status').value=-1;  $('#form1').submit(); }" />
                                                            
                                                            <input id="btnDraft" type="button" value="Draft" onclick="javascript: $('#form1').submit();" />

                                                            <input id="btnEndorse" type="button" value="Endorse" onclick="__doPostBack('HistoryBack');" />

                                                            <input id="btnBack" type="button" value="BACK" onclick="__doPostBack('HistoryBack');"  />
                                                            
                                                        </td>
                                                    </tr>
												</table>
                                                <br />
                                            </td>
                                        </tr>
                                    </table>
                                </td>

                            </tr>
							
						</table>
                    </td>
                </tr>
                <tr>
					<td id="footer" height="50px">
						
<link type="text/css" href='<%= "../themes/"+Session["configTheme"]+"/css/style.css" %>' rel="stylesheet" />
Trans-Asia E-Bid Portal System<br />
Copyright © 2002 Trans-Asia , Inc. All rights reserved.<br />

<a id="Footer1_hlTerms" href="../../termsofuse.aspx">Terms of Use</a>
- <a id="Footer1_hlPrivacy" href="../../policy.aspx">Privacy Policy</a><br />

<a id="Footer1_HyperLink1" href="http://www.globeplatinum.com.ph/" target="_blank">Trans-Asia Platinum</a>
 | <a id="Footer1_HyperLink2" href="http://www.globehandyphone.com.ph/" target="_blank">Trans-Asia Handyphone</a>
 | <a id="Footer1_HyperLink3" href="http://www.myglobe.com.ph/" target="_blank">myGlobe</a> 
 | <a id="Footer1_HyperLink4" href="http://www.hub-eshop.com/" target="_blank">Hub Eshop</a> 
 | <a id="Footer1_HyperLink5" href="http://www.gentxt.com.ph/" target="_blank">GenTxt</a> 
 | <a id="Footer1_HyperLink6" href="http://www.globesolutions.com.ph/" target="_blank">Trans-Asia Solutions</a> 
 | <a id="Footer1_HyperLink7" href="http://www..com.ph/" target="_blank"></a> 
 | <a id="Footer1_HyperLink8" href="https://www.dealerportal.com.ph/" target="_blank">Dealers</a> 
 | <a id="Footer1_HyperLink9" href="http://www.globekababayan.com.ph/" target="_blank">Kababayan</a>

					</td>
				</tr>
            </table>
            <input name="status" type="hidden" id="status" />
        </form>
    </div>
</body>
</html>