<%@ Page Title="Login" Language="C#" AutoEventWireup="true" MasterPageFile="~/SiteMaster.Master" CodeBehind="Default.aspx.cs" Inherits="MakeNMake.Default" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="Scripts/jquery-2.0.1.min.js"></script>
    <script type="text/javascript">
        $(function () {
            $(document).bind('contextmenu', function (e) {
                e.preventDefault();
                alert('Right Click is not allowed');
            });
        });
    </script>
    <script>
         $(document).ready(function () {
             $('#form1').keydown(function (e) {
                 var key = e.which;
                 if (key == 13) {
                     // As ASCII code for ENTER key is "13"
                     $('#form1').submit(); // Submit form code
                 }
             });
         });
    </script>

    <title>Login form</title>
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7; IE=EmulateIE9" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1.0, user-scalable=no" />
    <link rel="stylesheet" type="text/css" href="makenmakeNew/style.css" media="all" />
    <link rel="stylesheet" type="text/css" href="makenmakeNew/demo.css" media="all" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container"  style="background-image: url('makenmakeNew/oie_transparent.png');background-repeat: no-repeat;
    background-attachment: fixed;
    background-size: 500px;
    background-position: left;
    min-height: 650px; "><div class="logo" style="margin-left:5%;">
            <div class="img">
                 <img src="makenmakeNew/logo-new.png" style="width: 120px; " />
            </div>
        </div>
        <div class="form" style="margin-left:37.5%;" >
            <div class="img">
                <!--<img src="images/Untitled-1.png" style="padding-right:25px;">-->
                <h1>Member Login</h1>
            </div>
            <div id="contactform">

                <p class="contact">
                    <label for="email">Email</label>
                </p>
                <asp:TextBox ID="txtloginid" ClientIDMode="Static" style="clear:both;" placeholder="example@domain.com" AutoCompleteType="None" autocomplete="off" runat="server" class="form-control normalinput"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Enter Email ID" ValidationGroup="login"
                    ForeColor="Red" Display="Dynamic" Font-Size="12px" Font-Bold="true" ControlToValidate="txtloginid"></asp:RequiredFieldValidator>

                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Enter Correct Email ID"
                    Display="Dynamic" ForeColor="Red" Font-Bold="True" Font-Size="12px" ControlToValidate="txtloginid" ValidationGroup="login"
                    ValidationExpression="\b[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}\b"></asp:RegularExpressionValidator>
                <br /><br />
                <p class="contact">
                    <label for="password">Password</label>
                </p>
                <asp:TextBox ID="txtpass" runat="server" AutoCompleteType="None" style="clear:both;" autocomplete="off" placeholder="password"
                    TextMode="Password" class="form-control normalinput"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Enter Password"
                    ControlToValidate="txtpass" Font-Size="12px" Display="Dynamic" ForeColor="Red" Font-Bold="true" ValidationGroup="login"></asp:RequiredFieldValidator>
                <br /><br />
                <div class="input-group">
                    <div class="checkbox">
                        <asp:CheckBox ID="chkremember" Height="20px" runat="server" Text="Remember Me" />
                    </div>
                </div>
                <div class="demo" style="margin-bottom: 20px;margin-top:20px; height: 90px;">
                    <%--<input class="buttom" name="submit" id="submit" tabindex="1" value="LOGIN" type="submit">--%>
                    <asp:Button ID="btnlogin" runat="server" OnClick="btnlogin_Click" ValidationGroup="login" Text="LOGIN"
                        class="btn btn-success buttom" /><br /><br />
                    <div>
                        <asp:LinkButton ID="lnkBtnForgotPassword" Style="color: #00BBE5; float: left;" OnClick="lnkBtnForgotPassword_Click" runat="server">Forgot password?</asp:LinkButton>
                        <br />
                        <a href="SignUp.aspx" style="color: #00BBE5; float: left;">Don't have account ? Register</a>
                    </div>
                </div>
               <%--<div class="image" style="text-align:center; padding-top:20px;position: fixed;right: 13%; top: 100px;">
				<h2 style="font-size: 18px;font-weight: 400; padding-right: 30px; padding-bottom:15px;font-family: 'Lato', sans-serif;">Or Register With Social Site</h2>
                            
                               <asp:ImageButton ID="btn_twitterlogin" ImageUrl="~/makenmakeNew/4.png" runat="server" />
                            <div id="auth-status">
<div id="auth-loggedout">

            <div class="fb-login-button" autologoutlink="true" scope="email,user_checkins" ></div>
            </div>
<div id="auth-loggedin" style="display: none">
Hi, <span id="auth-displayname"></span>(<a href="#" id="auth-logoutlink">logout</a>)
</div>
</div>

                        </div>--%>
            </div>
        </div>
    </div>

</asp:Content>
