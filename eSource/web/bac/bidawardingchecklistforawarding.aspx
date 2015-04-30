<%@ Register Src="../usercontrol/bac/BAC_LeftNav_Bids.ascx" TagName="BAC_LeftNav_Bids" TagPrefix="uc2" %>
<%@ Register Src="../usercontrol/bac/BAC_TopNav_Bids.ascx" TagName="BAC_TopNav_Bids" TagPrefix="uc1" %>
<%@ Register TagPrefix="EBid" TagName="TopDate" Src="~/WEB/usercontrol/TopDate.ascx" %>
<%@ Register TagPrefix="EBid" TagName="GlobalLinksNav" Src="~/web/usercontrol/GlobalLinksNav.ascx" %>
<%@ Register TagPrefix="EBid" TagName="Footer" Src="~/web/usercontrol/Footer.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml" style="height: 100%;">
<head>
    <title id="PageTitle" runat="server"></title>
    <meta http-equiv="Content-Language" content="en-us" />
    <meta http-equiv="Content-Type" content="text/html; charset=windows-1252" />
    <link href='<%= "../themes/"+Session["configTheme"]+"/css/style.css" %>' rel="stylesheet" type="text/css" />
    <link href='<%= "../themes/"+Session["configTheme"]+"/css/style_ph.css" %>' rel="stylesheet" type="text/css" />
</head>
<body style="height: 100%;">
    <div>
        <form enctype="multipart/form-data" id="form1" runat="server">
            <table border="0" cellpadding="0" cellspacing="0" width="100%" id="page">
                <tr>
                    <td valign="top" height="137px">
                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td>
                                    <div align="left" id="masthead">
                                        <EBid:GlobalLinksNav runat="server" ID="GlobalLinksNav" />
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <uc1:BAC_TopNav_Bids ID="BAC_TopNav_Bids1" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <EBid:TopDate runat="server" ID="TopDate" />
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
                                    <h2>
                                        Bid Events</h2>
                                    <div align="left">
                                        <uc2:BAC_LeftNav_Bids ID="BAC_LeftNav_Bids1" runat="server" />
                                    </div>
                                    <p>&nbsp;
                                        </p>
                                </td>
                                <td id="content">
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                        <tr>
                                            <td valign="top">
                                              <h1>
                                                    <br />
                                                    Bid Awarding Checklist</h1>
                                                <table cellspacing="5" style="width:100%;">
                                                    <tr>
                                                        <td colspan="2" valign="top"><table width="60%" border="1" cellpadding="0" cellspacing="0" rules="all" class="itemDetails" id="Biddetails_details1_dvEventDetails" style="border-color: rgb(223, 223, 223); font-family: Arial; font-size: 11px; border-collapse: collapse; background-color: #F4F1C4; height:100%">
                                                          <tbody>
                                                            <tr style="color: White; background-color: #F5BA22; font-size: 12px; font-weight: bold;" valign="middle">
                                                              <td colspan="4">BID DETAILS</td>
                                                            </tr>
                                                            <tr valign="middle" >
                                                              <td colspan="2" align="center" valign="middle" style="padding:5px; font-size:12px; width:50%">Bid Reference No. </td>
                                                              <td colspan="2" align="center" valign="middle" style="padding:5px; font-size:12px; width:50%">Bid Events Description: </td>
                                                            </tr>
                                                            <tr valign="middle" >
                                                              <td colspan="2" align="center" valign="middle" bgcolor="#FFFFFF" style="padding:15px 5px; font-size:12px; width:50%">
                                                                  <u>7894</u></td>
                                                              <td colspan="2" align="center" valign="middle" bgcolor="#FFFFFF" style="padding:15px 5px; font-size:12px; width:50%"><u>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</u></td>
                                                            </tr>
                                                            <tr valign="middle">
                                                              <td align="center" valign="middle" style="width:25%;">SAP PR No.:</td>
                                                              <td align="center" valign="middle" style="width:25%;">Amount:</td>
                                                              <td align="center" valign="middle" style="width:25%;">SAP PR Date:</td>
                                                              <td align="center" valign="middle" style="width:25%;">Date Received</td>
                                                            </tr>
                                                            <tr valign="middle">
                                                              <td align="center" valign="middle" bgcolor="#FFFFFF" style="width:25%;">                                                                <u>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</u></td>
                                                              <td align="center" valign="middle" bgcolor="#FFFFFF" style="width:25%;">                                                                <u>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</u>
</td>
                                                              <td align="center" valign="middle" bgcolor="#FFFFFF" style="width:25%;">                                                              <u>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</u>
</td>
                                                              <td align="center" valign="middle" bgcolor="#FFFFFF" style="width:25%;">                                                                <u>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</u></td>
                                                            </tr>
                                                            <tr valign="middle">
                                                              <td colspan="2" align="center" valign="middle" style="width:25%;"><table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                                <tr>
                                                                  <td><input type="checkbox" name="checkbox8" id="checkbox8">
                                                                  <label for="checkbox8">Budgeted</label></td>
                                                                  <td><input type="checkbox" name="checkbox9" id="checkbox9">
                                                                  <label for="checkbox9">Unbudgeted</label></td>
                                                                </tr>
                                                              </table></td>
                                                              <td colspan="2" align="center" valign="middle" style="width:25%;"><table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                                <tr>
                                                                  <td><input type="checkbox" name="checkbox10" id="checkbox10">
                                                                  <label for="checkbox10">GT</label></td>
                                                                  <td><input type="checkbox" name="checkbox11" id="checkbox11">
                                                                  <label for="checkbox11">IC</label></td>
                                                                  <td><input type="checkbox" name="checkbox12" id="checkbox12">
                                                                  GXI</td>
                                                                  <td><input type="checkbox" name="checkbox13" id="checkbox13">
                                                                  EGG</td>
                                                                </tr>
                                                              </table></td>
                                                            </tr>
                                                          </tbody>
                                  </table></td>
                                                        <td valign="top"><table border="1" cellpadding="0" cellspacing="0" rules="all" class="itemDetails" id="Biddetails_details1_dvEventDetails2" style="border-color: rgb(223, 223, 223); font-family: Arial; font-size: 11px; border-collapse: collapse; background-color: #F4F1C4;">
                                                          <tbody>
                                                            <tr style="color: White; background-color: #F5BA22; font-size: 12px; font-weight: bold;" valign="middle">
                                                              <td>SUPPORTING DOCUMENTS ATTACHED</td>
                                                            </tr>
                                                            <tr valign="middle" >
                                                              <td align="center" valign="middle" style="padding:5px; font-size:12px; width:50%"><table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                                <tr>
                                                                  <td width="50%"><input type="checkbox" name="checkbox" id="checkbox">
                                                                  <label for="checkbox">Approved PR</label></td>
                                                                  <td width="50%"><input type="checkbox" name="checkbox2" id="checkbox2">
                                                                  <label for="checkbox2">Commercial Evaluation</label></td>
                                                                </tr>
                                                                <tr>
                                                                  <td><input type="checkbox" name="checkbox3" id="checkbox3">
                                                                  <label for="checkbox3">Approved Business Case</label></td>
                                                                  <td><input type="checkbox" name="checkbox4" id="checkbox4">
                                                                  <label for="checkbox4">Negotiation Results</label></td>
                                                                </tr>
                                                                <tr>
                                                                  <td><input type="checkbox" name="checkbox5" id="checkbox5">
                                                                  Board Resolution</td>
                                                                  <td><input type="checkbox" name="checkbox6" id="checkbox6">
                                                                  Others</td>
                                                                </tr>
                                                                <tr>
                                                                  <td><input type="checkbox" name="checkbox7" id="checkbox7">
                                                                  Technical Evaluation</td>
                                                                  <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;__________________________</td>
                                                                </tr>
                                                                <tr>
                                                                  <td colspan="2" style="border-bottom:none"><label for="Attach File"></label>
                                                                  <input type="file" name="Attach File" id="Attach File"></td>
                                                                </tr>
                                                              </table></td>
                                                            </tr>
                                                            <tr valign="middle">
                                                              <td valign="middle"></td>
                                                            </tr>
                                                          </tbody>
                                                        </table></td>
                                                  </tr>
                                                    <tr>
                                                        <td valign="top"><table width="60%" border="1" cellpadding="0" cellspacing="0" rules="all" class="itemDetails" id="Biddetails_details1_dvEventDetails3" style="border-color: rgb(223, 223, 223); font-family: Arial; font-size: 11px; border-collapse: collapse; background-color: #F4F1C4;">
                                                          <tbody>
                                                            <tr style="color: White; background-color: #F5BA22; font-size: 12px; font-weight: bold;" valign="middle">
                                                              <td>BASIS FOR AWARDING</td>
                                                            </tr>
                                                            <tr valign="middle">
                                                              <td align="center" valign="middle" style="width:25%;"><table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                                <tr>
                                                                  <td><input type="checkbox" name="checkbox14" id="checkbox14">
                                                                    <label for="checkbox14">Lowest-Price Bidder</label></td>
                                                                </tr>
                                                                <tr>
                                                                  <td><input type="checkbox" name="checkbox15" id="checkbox15">
                                                                    <label for="checkbox15">Sole Source</label></td>
                                                                </tr>
                                                                <tr>
                                                                  <td style="padding-left:25px;"><input type="checkbox" name="checkbox16" id="checkbox16">
                                                                    <label for="checkbox16">W/LOA Approved Waiver of Competition</label></td>
                                                                </tr>
                                                                <tr>
                                                                  <td style="padding-left:25px;"><input type="checkbox" name="checkbox17" id="checkbox17">
                                                                    <label for="checkbox17">W/ Certificate of Sale Distributorship</label></td>
                                                                </tr>
                                                                <tr>
                                                                  <td><input type="checkbox" name="checkbox18" id="checkbox18">
                                                                    <label for="checkbox18">Other Considerations</label></td>
                                                                </tr>
                                                                <tr>
                                                                  <td style="padding-left:25px;"><input type="checkbox" name="checkbox19" id="checkbox19">
                                                                    Compliance to Technical / Functional Specifications</td>
                                                                </tr>
                                                                <tr>
                                                                  <td style="padding-left:25px;"><input type="checkbox" name="checkbox20" id="checkbox20">
                                                                    Compliance to Delivery Lead-time</td>
                                                                </tr>
                                                                <tr>
                                                                  <td><input type="checkbox" name="checkbox21" id="checkbox21">
                                                                    <label for="checkbox21">Others </label>
                                                                    <label for="textfield"></label>
                                                                    <input type="text" name="textfield" id="textfield"></td>
                                                                </tr>
                                                              </table></td>
                                                            </tr>
                                                          </tbody>
                                                        </table>
                                                            </td>
                                                        <td valign="top"><table width="60%" border="1" cellpadding="0" cellspacing="0" rules="all" class="itemDetails" id="Biddetails_details1_dvEventDetails4" style="border-color: rgb(223, 223, 223); font-family: Arial; font-size: 11px; border-collapse: collapse; background-color: #F4F1C4; width:99%;">
                                                          <tbody>
                                                            <tr style="color: White; background-color: #F5BA22; font-size: 12px; font-weight: bold;" valign="middle">
                                                              <td>PAYMENT TERMS</td>
                                                            </tr>
                                                            <tr valign="middle">
                                                              <td align="center" valign="middle" style="width:25%;"><table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                                <tr>
                                                                  <td width="50%"><input type="checkbox" name="checkbox24" id="checkbox38">
                                                                  <label for="checkbox38">Advance</label></td>
                                                                  <td><input type="checkbox" name="checkbox22" id="checkbox22">
                                                                    <label for="checkbox22">Progress Billing</label></td>
                                                                </tr>
                                                                <tr>
                                                                  <td><input type="checkbox" name="checkbox25" id="checkbox23">
                                                                  <label for="checkbox23">Open Delivery</label></td>
                                                                  <td><input type="checkbox" name="checkbox26" id="checkbox24">
                                                                    <label for="checkbox24">Milestone Payment </label>
                                                                  <label for="textfield"></label></td>
                                                                </tr>
                                                                <tr>
                                                                  <td colspan="2"><input type="checkbox" name="checkbox22" id="checkbox29">
                                                                    <label for="checkbox29">Others </label>
                                                                    <label for="textfield"></label>
                                                                  <input type="text" name="textfield2" id="textfield">
                                                                  <br>
                                                                  <br></td>
                                                                </tr>
                                                              </table></td>
                                                            </tr>
                                                          </tbody>
                                                        </table>
                                                          <table width="60%" border="1" cellpadding="0" cellspacing="0" rules="all" class="itemDetails" id="Biddetails_details1_dvEventDetails6" style="border-color: rgb(223, 223, 223); font-family: Arial; font-size: 11px; border-collapse: collapse; background-color: #F4F1C4; width:99%; ">
                                                            <tbody>
                                                              <tr style="color: White; background-color: #F5BA22; font-size: 12px; font-weight: bold;" valign="middle">
                                                                <td>PAYMENT TERMS</td>
                                                              </tr>
                                                              <tr valign="middle">
                                                                <td align="center" valign="middle" style="width:25%;"><table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                                  <tr>
                                                                    <td width="50%"><input type="checkbox" name="checkbox27" id="checkbox25">
                                                                      <label for="checkbox25">Good Only</label></td>
                                                                    <td><input type="checkbox" name="checkbox27" id="checkbox26">
                                                                      <label for="checkbox26">Goods &amp; Services</label></td>
                                                                  </tr>
                                                                  <tr>
                                                                    <td><input type="checkbox" name="checkbox27" id="checkbox27">
                                                                      Service Only
                                                                        <label for="checkbox27"></label></td>
                                                                    <td><input type="checkbox" name="checkbox28" id="checkbox28">
                                                                      <label for="checkbox28">Others </label>
                                                                      <label for="textfield"></label>
                                                                    <input type="text" name="textfield5" id="textfield">
                                                                    <br></td>
                                                                  </tr>
                                                                </table></td>
                                                              </tr>
                                                            </tbody>
                                                        </table></td>
                                                        <td valign="top"><table width="60%" border="1" cellpadding="0" cellspacing="0" rules="all" class="itemDetails" id="Biddetails_details1_dvEventDetails5" style="border-color: rgb(223, 223, 223); font-family: Arial; font-size: 11px; border-collapse: collapse; background-color: #F4F1C4;">
                                                          <tbody>
                                                            <tr style="color: White; background-color: #F5BA22; font-size: 12px; font-weight: bold;" valign="middle">
                                                              <td>SOURCING STRATEGY</td>
                                                            </tr>
                                                            <tr valign="middle">
                                                              <td align="center" valign="middle" style="width:25%;"><table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                                <tr>
                                                                  <td width="50%"><input type="checkbox" name="checkbox23" id="checkbox30">
                                                                    <label for="checkbox30">Repeat Order/Purchase</label></td>
                                                                  <td width="50%">No. of Invited Bidders: 
                                                                    <label for="textfield2"></label>
                                                                  <input name="textfield3" type="text" id="textfield2" size="3"></td>
                                                                </tr>
                                                                <tr>
                                                                  <td><input type="checkbox" name="checkbox23" id="checkbox32">
                                                                    <label for="checkbox32">Bidding</label></td>
                                                                  <td width="50%">No. of Bids Received:
                                                                    <label for="textfield2"></label>
                                                                    <input name="textfield3" type="text" id="textfield2" size="3"></td>
                                                                </tr>
                                                                <tr>
                                                                  <td style="padding-left:25px;"><input type="checkbox" name="checkbox20" id="checkbox20">
                                                                    Manual</td>
                                                                  <td width="50%">Tech. Compliance:
                                                                    <label for="textfield2"></label>
                                                                  <input name="textfield3" type="text" id="textfield2" size="3"></td>
                                                                </tr>
                                                                <tr>
                                                                  <td style="padding-left:25px;"><input type="checkbox" name="checkbox20" id="checkbox20">
                                                                    E-Sourcing</td>
                                                                  <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<br>
                                                                  <br>
                                                                  <br></td>
                                                                </tr>
                                                              </table></td>
                                                            </tr>
                                                          </tbody>
                                                        </table>
                                                          <table width="60%" border="1" cellpadding="0" cellspacing="0" rules="all" class="itemDetails" id="Biddetails_details1_dvEventDetails7" style="border-color: rgb(223, 223, 223); font-family: Arial; font-size: 11px; border-collapse: collapse; background-color: #F4F1C4;">
                                                            <tbody>
                                                              <tr style="color: White; background-color: #F5BA22; font-size: 12px; font-weight: bold;" valign="middle">
                                                                <td>SUPPLY POSITION</td>
                                                              </tr>
                                                              <tr valign="middle">
                                                                <td align="center" valign="middle" style="width:25%;"><table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                                  <tr>
                                                                    <td width="25%">Critial:                                                                      <input type="checkbox" name="checkbox29" id="checkbox31"></td>
                                                                    <td width="25%">Strategic:
                                                                    <input type="checkbox" name="checkbox30" id="checkbox33"></td>
                                                                    <td width="25%">Routine:
                                                                    <input type="checkbox" name="checkbox31" id="checkbox34"></td>
                                                                    <td width="25%">Leverage:
                                                                    <input type="checkbox" name="checkbox32" id="checkbox35"></td>
                                                                  </tr>
                                                                </table></td>
                                                              </tr>
                                                            </tbody>
                                                        </table></td>
                                                    </tr>
                                                    <tr>
                                                      <td colspan="3" valign="top"><table width="60%" border="1" cellpadding="0" cellspacing="0" rules="all" class="itemDetails" id="Biddetails_details1_dvEventDetails9" style="border-color: rgb(223, 223, 223); font-family: Arial; font-size: 11px; border-collapse: collapse; background-color: #F4F1C4; height:100%">
                                                        <tbody>
                                                          <tr style="color: White; background-color: #F5BA22; font-size: 12px; font-weight: bold;" valign="middle">
                                                            <td colspan="2">EXECUTIVE SUMMARY</td>
                                                          </tr>
                                                          <tr valign="middle" >
                                                            <td align="center" valign="middle" style="padding:5px; font-size:12px; width:50%"><label for="textarea"></label>
                                                              <textarea name="textarea2" id="textarea" cols="45" rows="5" style="width:100%"></textarea></td>
                                                          </tr>
                                                        </tbody>
                                                      </table></td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="3" valign="top"><table width="60%" border="1" cellpadding="0" cellspacing="0" rules="all" class="itemDetails" id="Biddetails_details1_dvEventDetails8" style="border-color: rgb(223, 223, 223); font-family: Arial; font-size: 11px; border-collapse: collapse; background-color: #F4F1C4; height:100%">
                                                          <tbody>
                                                            <tr style="color: White; background-color: #F5BA22; font-size: 12px; font-weight: bold;" valign="middle">
                                                              <td colspan="2">REMARKS / COMMENTS</td>
                                                            </tr>
                                                            <tr valign="middle" >
                                                              <td align="center" valign="middle" style="padding:5px; font-size:12px; width:50%"><label for="textarea"></label>
                                                              <textarea name="textarea" id="textarea" cols="45" rows="5" style="width:100%"></textarea></td>
                                                            </tr>
                                                          </tbody>
                                                        </table>
                                                      </td>
                                                    </tr>
                                                    <tr>
                                                      <td colspan="3" valign="top"><table width="60%" border="1" cellpadding="0" cellspacing="0" rules="all" class="itemDetails" id="Biddetails_details1_dvEventDetails10" style="border-color: rgb(223, 223, 223); font-family: Arial; font-size: 11px; border-collapse: collapse; background-color: #F4F1C4; height:100%">
                                                        <tbody>
                                                          <tr style="color: White; background-color: #F5BA22; font-size: 12px; font-weight: bold;" valign="middle">
                                                            <td>BID EVALUATION SUMMARY</td>
                                                          </tr>
                                                          <tr valign="middle" >
                                                            <td align="center" valign="middle" style="padding:5px; font-size:12px; width:50%"><table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                              <tr bgcolor="#E4EFF8">
                                                                <td><strong>VENDOR / CRITERIA</strong></td>
                                                                <td width="80"><strong>IBC</strong></td>
                                                                <td width="80"><strong>GMMCI</strong></td>
                                                                <td width="80"><strong>CSEMSCORP</strong></td>
                                                                <td width="80"><strong>KAPHIDS</strong></td>
                                                                <td width="80"><strong>MJ VILLANUEVA</strong></td>
                                                                <td width="80"><strong>FIL-GARTE</strong></td>
                                                                <td width="80"><strong>LBS</strong></td>
                                                              </tr>
                                                              <tr bgcolor="#C7DEEF">
                                                                <td>Technical Complicane</td>
                                                                <td>Compiled</td>
                                                                <td>Compiled</td>
                                                                <td>Compiled</td>
                                                                <td>Compiled</td>
                                                                <td>Compiled</td>
                                                                <td>Compiled</td>
                                                                <td>Compiled</td>
                                                              </tr>
                                                              <tr>
                                                                <td>Commercial Compliance</td>
                                                                <td>Compiled</td>
                                                                <td>Compiled</td>
                                                                <td>Compiled</td>
                                                                <td>Compiled</td>
                                                                <td>Compiled</td>
                                                                <td>Compiled</td>
                                                                <td>Compiled</td>
                                                              </tr>
                                                              <tr bgcolor="#C7DEEF">
                                                                <td>Contract Compliance</td>
                                                                <td>Compiled</td>
                                                                <td>Compiled</td>
                                                                <td>Compiled</td>
                                                                <td>Compiled</td>
                                                                <td>Compiled</td>
                                                                <td>Compiled</td>
                                                                <td>Compiled</td>
                                                              </tr>
                                                              <tr bgcolor="#E4EFF8">
                                                                <td><strong>Price</strong></td>
                                                                <td>&nbsp;</td>
                                                                <td>&nbsp;</td>
                                                                <td>&nbsp;</td>
                                                                <td>&nbsp;</td>
                                                                <td>&nbsp;</td>
                                                                <td>&nbsp;</td>
                                                                <td>&nbsp;</td>
                                                              </tr>
                                                              <tr bgcolor="#C7DEEF">
                                                                <td>&nbsp;&nbsp;&nbsp;&nbsp;QTY</td>
                                                                <td>3</td>
                                                                <td>2</td>
                                                                <td>3</td>
                                                                <td>1</td>
                                                                <td>0</td>
                                                                <td>0</td>
                                                                <td>0</td>
                                                              </tr>
                                                              <tr>
                                                                <td>&nbsp;&nbsp;&nbsp;&nbsp;Unit Cost</td>
                                                                <td>LOT</td>
                                                                <td>LOT</td>
                                                                <td>LOT</td>
                                                                <td>LOT</td>
                                                                <td>LOT</td>
                                                                <td>LOT</td>
                                                                <td>LOT</td>
                                                              </tr>
                                                              <tr bgcolor="#C7DEEF">
                                                                <td>&nbsp;&nbsp;&nbsp;&nbsp;Total</td>
                                                                <td>5,325,958</td>
                                                                <td>3,512,456</td>
                                                                <td>3,076,219</td>
                                                                <td>6,000,056</td>
                                                                <td>7,455,000</td>
                                                                <td>8,231,123</td>
                                                                <td>8,565,000</td>
                                                              </tr>
                                                              <tr>
                                                                <td>&nbsp;&nbsp;&nbsp;&nbsp;Ranking</td>
                                                                <td>1</td>
                                                                <td>2</td>
                                                                <td>3</td>
                                                                <td>4</td>
                                                                <td>5</td>
                                                                <td>6</td>
                                                                <td>7</td>
                                                              </tr>
                                                              <tr>
                                                                <td>&nbsp;</td>
                                                                <td bgcolor="#CCCCCC"><strong><img src="../images/check.gif" width="10" height="9"></strong></td>
                                                                <td bgcolor="#CCCCCC"><strong><img src="../images/check.gif" width="10" height="9"></strong></td>
                                                                <td bgcolor="#CCCCCC"><strong><img src="../images/check.gif" width="10" height="9"></strong></td>
                                                                <td bgcolor="#CCCCCC"><strong><img src="../images/check.gif" width="10" height="9"></strong></td>
                                                                <td bgcolor="#CCCCCC"><strong>X</strong></td>
                                                                <td bgcolor="#CCCCCC"><strong>X</strong></td>
                                                                <td bgcolor="#CCCCCC"><strong>X</strong></td>
                                                              </tr>
                                                              <tr>
                                                                <td colspan="8" bgcolor="#E4EFF8"><input type="submit" name="button" id="button" value="Add Row"> </td>
                                                              </tr>
                                                            </table></td>
                                                          </tr>
                                                        </tbody>
                                                      </table>
                                                        <table width="100%" border="1" cellpadding="0" cellspacing="0" rules="all" class="itemDetails" id="Biddetails_details1_dvEventDetails11" style="border-color: rgb(223, 223, 223); font-family: Arial; font-size: 11px; border-collapse: collapse; background-color: #F4F1C4; height:100%">
                                                          <tbody>
                                                            <tr style="color: White; background-color: #F5BA22; font-size: 12px; font-weight: bold;" valign="middle">
                                                              <td colspan="4">PURCHASING</td>
                                                            </tr>
                                                            <tr valign="middle" >
                                                              <td width="25%" align="center" valign="middle" style="padding:5px; font-size:12px;"><table width="150" border="0" cellspacing="0" cellpadding="0">
                                                                <tr>
                                                                  <td>Prepared by / date: <br>
                                                                  April 20, 2011</td>
                                                                </tr>
                                                                <tr>
                                                                  <td>&nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                  <td><strong>Guilda Gudy</strong></td>
                                                                </tr>
                                                                <tr>
                                                                  <td style="border-top:2px #000 solid;">Buyer</td>
                                                                </tr>
                                                              </table></td>
                                                              <td width="25%" align="center" valign="middle" style="padding:5px; font-size:12px;"><table width="150" border="0" cellspacing="0" cellpadding="0">
                                                                <tr>
                                                                  <td colspan="2">Reviewed by / date: <br>
                                                                    April 20, 2011</td>
                                                                </tr>
                                                                <tr>
                                                                  <td>Clarify
                                                                    <input name="checkbox33" type="checkbox" id="checkbox36"></td>
                                                                  <td>Approved
                                                                  <input type="checkbox" name="checkbox34" id="checkbox37"></td>
                                                                </tr>
                                                                <tr>
                                                                  <td colspan="2"><strong>Consolacion Lim</strong></td>
                                                                </tr>
                                                                <tr>
                                                                  <td colspan="2" style="border-top:2px #000 solid;">Approver 4</td>
                                                                </tr>
                                                              </table></td>
                                                              <td width="25%" align="center" valign="middle" style="padding:5px; font-size:12px;"><table width="150" border="0" cellspacing="0" cellpadding="0">
                                                                <tr>
                                                                  <td colspan="2">Noted by / date: <br>
                                                                    April 20, 2011</td>
                                                                </tr>
                                                                <tr>
                                                                  <td>Clarify
                                                                    <input type="checkbox" name="checkbox35" id="checkbox39" disabled="disabled"></td>
                                                                  <td>Approved
                                                                    <input type="checkbox" name="checkbox35" id="checkbox40" disabled="disabled"></td>
                                                                </tr>
                                                                <tr>
                                                                  <td colspan="2"><strong>Norben Quintos</strong></td>
                                                                </tr>
                                                                <tr>
                                                                  <td colspan="2" style="border-top:2px #000 solid;">Approver 3</td>
                                                                </tr>
                                                              </table></td>
                                                              <td width="25%" align="center" valign="middle" style="padding:5px; font-size:12px;"><table width="150" border="0" cellspacing="0" cellpadding="0">
                                                                <tr>
                                                                  <td colspan="2">Approved by / date: <br>
                                                                    April 20, 2011</td>
                                                                </tr>
                                                                <tr>
                                                                  <td>Clarify
                                                                    <input type="checkbox" name="checkbox35" id="checkbox41" disabled="disabled"></td>
                                                                  <td>Approved
                                                                    <input type="checkbox" name="checkbox35" id="checkbox42"  disabled="disabled"></td>
                                                                </tr>
                                                                <tr>
                                                                  <td colspan="2"><strong>Jerfferson De Leon</strong></td>
                                                                </tr>
                                                                <tr>
                                                                  <td colspan="2" style="border-top:2px #000 solid;">Approver 2</td>
                                                                </tr>
                                                              </table></td>
                                                            </tr>
                                                          </tbody>
                                                      </table>
                                                        <table border="1" cellpadding="0" cellspacing="0" rules="all" class="itemDetails" id="Biddetails_details1_dvEventDetails12" style="border-color: rgb(223, 223, 223); font-family: Arial; font-size: 11px; border-collapse: collapse; background-color: #F4F1C4; height:100%; width:25%;">
                                                          <tbody>
                                                            <tr style="color: White; background-color: #F5BA22; font-size: 12px; font-weight: bold;" valign="middle">
                                                              <td>BID APPROVING AUTHORITY</td>
                                                            </tr>
                                                            <tr valign="middle" >
                                                              <td width="25%" align="center" valign="middle" style="padding:5px; font-size:12px;"><table width="150" border="0" cellspacing="0" cellpadding="0">
                                                                <tr>
                                                                  <td colspan="2">Approved by / date: <br>
                                                                    April 20, 2011</td>
                                                                </tr>
                                                                <tr>
                                                                  <td>Clarify
                                                                    <input name="checkbox36" type="checkbox" id="checkbox43" disabled="disabled"></td>
                                                                  <td>Approved
                                                                    <input type="checkbox" name="checkbox36" id="checkbox44" disabled="disabled"></td>
                                                                </tr>
                                                                <tr>
                                                                  <td colspan="2"><strong>Ernesto Cu</strong></td>
                                                                </tr>
                                                                <tr>
                                                                  <td colspan="2" style="border-top:2px #000 solid;">Approver 1</td>
                                                                </tr>
                                                              </table></td>
                                                            </tr>
                                                          </tbody>
                                                      </table></td>
                                                    </tr>
                                                    <tr>
                                                      <td colspan="2" valign="top">&nbsp;</td>
                                                      <td valign="top" align="right">
                                                          &nbsp;</td>
                                                    </tr>
                                                </table>
                                            <br />
                                                <table border="0" cellpadding="0" cellspacing="0" width="100%" id="actions">
                                                    <tr>                                                        
                                                        <td><a href="javascript:void(0)">SUBMIT</a> <a href="javascript:void(0)" 
                                                                onclick="history.back();">BACK</a></td>
                                                    </tr>
                                                </table>
                                                <br />
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td id="footer" height="50px">
                        <EBid:Footer runat="server" ID="Footer1" />                        
                    </td>
                </tr>
            </table>
        </form>
    </div>
</body>
</html>