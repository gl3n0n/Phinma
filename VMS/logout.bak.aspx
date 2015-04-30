<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="logout.aspx.cs" Inherits="logout" %>

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
    <div style="float:left; width:370px; min-height:300px; border-right:#ccc 1px solid; margin:25px 0 0 100px; padding-top:30px;">
    <asp:Label runat="server" ID="txtNote" ForeColor="#3366FF" Style="font-weight: bold">You have successfully logged out.</asp:Label>
      <div class="clearfix"></div>
      <div style="margin-top:25px;">
      <div style="float:left">
      <br /><br /><p>Click here login back.</p>
      <input name="" type="image" src="images/002_11.png" onclick="window.location='login.aspx'" />

      </div>
      </div>
      <div class="clearfix" style="font-size:18px;"><br /><br /><p>New Vendor? <a href="vendor_signup.aspx" style="text-decoration:none;">Sign up here.</a></p></div>
      </div>
    <div style="float:left; width:400px; margin:25px 0 0 30px; padding-top:30px; font-size:14px;">
    <div style="font-size:20px;"> Vendor Accreditation</div>
    <div style="margin-top:15px; height:250px; overflow:scroll; padding:10px; border:1px solid #ccc">
        An objective process to provide assurance that Vendors who would like to do business with Trans-Asia are financially capable, technically qualified and legally-conforming entities that can provide quality goods and services.<br /><br />
It is recommended that the prospective Vendor check first if its product/service is needed by Trans-Asia. Trans-Asia will conduct an initial evaluation and advise the Vendor on the next steps to follow which may include submissions of additional documents and product samples.   A payment of a non-refundable accreditation fee is required for the accreditation process. The amount will be advised once the prospective Vendor passes the initial evaluation. Regardless of the result of the evaluation, the applicant Vendor will be formally advised. <br /><br />

    </div><br /><br />
        <%--<div style="font-size:20px;"> Products & Services Needed</div>--%>
    <%--<div style="margin-top:15px; height:150px; overflow:scroll; padding:10px; border:1px solid #ccc">
        <i>December 12, 2012</i><br />
        <b>Lorem ipsum dolor sit amet, consectetur adipiscing elit.</b><br />
        Sed odio mi, consequat ac pretium vel, rutrum eu massa. Nam volutpat tristique velit, vel elementum erat dapibus ac. Curabitur eu justo ut nisl congue consectetur a ac sem.<br /><br />
        <div style="height:1px; border-top: dotted 1px #666; margin: 5px 0px"></div>
        <i>December 1, 2012</i><br />
        <b>Curabitur eu justo ut nisl congue consectetur a ac sem.</b><br /> 
        Duis hendrerit, mauris quis sollicitudin vehicula, dui diam tincidunt velit, non dictum lectus est vel metus. Sed nisi massa, vulputate id rhoncus eget, feugiat et erat. <br /><br />


    </div>--%>
    <div style="color:#666; margin-top:25px;">
        <%--Dont have Trans-Asia VMS Accreditation <br />
        account yet? <a href="#" style="text-decoration:none;">Sign up here.</a>--%></div>
    </div>
        
    <div class="clearfix"></div>
    </div>
    </div>
    </div><!-- content ends -->
</asp:Content>