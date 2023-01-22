<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMaster.Master" AutoEventWireup="true" CodeBehind="ForgotPassword.aspx.cs" Inherits="MakeNMake.ForgotPassword" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
      <link href="Static/css/makenmake.css" rel="stylesheet" />
      <link href="Static/bootstrap/bootstrap.min.css" rel="stylesheet" />
    <link href="Static/bootstrap/bootstrap-theme.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div class="container-fluid" >
        <div id="loginbox" style="margin-top:5px;" class="mainbox col-md-6 col-md-offset-3 col-sm-8 col-sm-offset-2">
            <div class="panel panel-info">
        <a href="Default.aspx"><img src="Static/images/logo_mnm.png" style="margin-left: 2%;" class="logo" /></a>        
            </div>
            <div class="panel panel-info">
                <div class="panel-heading">
                    <div class="panel-title">Forgot Password</div>                    
                </div>
                <div style="padding-top: 30px" class="panel-body">
                   <%-- <div class="col-sm-12">
                        <asp:ValidationSummary ID="ValidationSummary1" ForeColor="Red" ValidationGroup="login" ShowSummary="true" runat="server" />
                    </div>--%>
                  <div id="divforgotinfo" runat="server" >

                  
                    <div style="margin-bottom: 25px" >
                        <div class="input-group">
                        <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                        <asp:TextBox ID="txtloginid" placeholder="Enter Email ID" AutoCompleteType="None" autocomplete="off" runat="server" class="form-control normalinput"></asp:TextBox>
                            </div>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Enter Email ID" ValidationGroup="login"
                            ForeColor="Red"  Display="Dynamic" Font-Size="12px" Font-Bold="true" ControlToValidate="txtloginid"></asp:RequiredFieldValidator>

                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Enter Correct Email ID"
                             Display="Dynamic" ForeColor="Red" Font-Bold="True" Font-Size="12px" ControlToValidate="txtloginid" ValidationGroup="login"
                            ValidationExpression="\b[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}\b"></asp:RegularExpressionValidator>
                    </div>
                    
                    <div class="input-group">
                        <div >
                            <asp:Button ID="btnForgot" runat="server" OnClick="btnForgot_Click" ValidationGroup="login" Text="Forgot Password"
                                class="btn btn-success" />
                              <asp:Button ID="btnCancel" runat="server" OnClick="btnCancel_Click"  Text="Cancel"
                                class="btn btn-success" />
                        </div>
                    </div>
                      </div>
                     <asp:HiddenField ID="hdnUserID" runat="server" />
                     <div id="dvVerifyOTP" class="col-sm-12" runat="server" visible="false" style="padding: 0;">
                    <div class="col-sm-6 text-left linkBottom">
                        <div class="input-group input-group-sm">
                            <span class="input-group-addon" id="Span8">Enter OTP</span>
                            <asp:TextBox ID="txtOTP" AutoComplete="off"
                                placeholder="Enter OTP" MaxLength="4" CssClass="form-control" aria-describedby="basic-addon1" runat="server"></asp:TextBox>
                        </div>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7"
                            runat="server" ValidationGroup="otp" Display="Dynamic" ForeColor="Red"
                            ControlToValidate="txtOTP" SetFocusOnError="true" ErrorMessage="Enter OTP"></asp:RequiredFieldValidator>
                    </div>
                    <div class="col-sm-6 text-left linkBottom">
                        <asp:Button ID="btnVerify" ValidationGroup="otp" runat="server" CssClass="btn btn-success" Text="Verify" OnClick="btnVerify_Click" />
                    </div>
                </div>
                </div>
            </div>
        </div>
    </div>

      
    
</asp:Content>
