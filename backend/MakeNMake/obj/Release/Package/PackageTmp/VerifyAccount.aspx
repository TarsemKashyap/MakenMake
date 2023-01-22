<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMaster.Master" AutoEventWireup="true" CodeBehind="VerifyAccount.aspx.cs" Inherits="MakeNMake.VerifyAccount" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
      <link href="Static/bootstrap/bootstrap.min.css" rel="stylesheet" />
      <link href="Static/css/makenmake.css" rel="stylesheet" />
    <link href="Static/bootstrap/bootstrap-theme.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div class="container-fluid">
        <div id="loginbox" style="margin-top:5px;" class="mainbox col-md-6 col-md-offset-3 col-sm-8 col-sm-offset-2">
            <div class="panel panel-info">
               <a href="Default.aspx"><img src="Static/images/logo_mnm.png" style="margin-left: 2%;" class="logo" /></a>        
            </div>
            <div class="panel panel-info">
                <div class="panel-heading">
                    <div class="panel-title">Set New Password</div>                    
                </div>
                <div style="padding-top: 30px" class="panel-body">
                    <%--<div class="col-sm-12">
                        <asp:ValidationSummary ID="ValidationSummary1" ForeColor="Red" ValidationGroup="password" ShowSummary="true" runat="server" />
                    </div>--%>

                      <div class="col-md-6 text-left linkBottom">
                    <div class="input-group input-group-sm">
                        <span class="input-group-addon">New Password
                        </span>
                       <asp:TextBox ID="txtnewpass" runat="server" TextMode="Password"  CssClass="form-control"  placeholder="Enter New Password "/>
                    </div>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Enter new Password" ValidationGroup="password"
                        ForeColor="Red"  Display="Dynamic"  ControlToValidate="txtnewpass"></asp:RequiredFieldValidator>

                </div>
                <div class="col-md-6 text-left linkBottom">
                    <div class="input-group input-group-sm">
                        <span class="input-group-addon">Confirm Password
                        </span>
                        <asp:TextBox ID="txtconfirmpass" runat="server" TextMode="Password"  CssClass="form-control"  placeholder="Enter Confirm Password "/>
                    </div>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Enter confirm Password"
                                            ForeColor="Red"  Display="Dynamic" ValidationGroup="password" ControlToValidate="txtconfirmpass"></asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="CompareValidator1"
                                            runat="server"
                                            ControlToCompare="txtnewpass"
                                            ControlToValidate="txtconfirmpass"
                                            Operator="Equal"  Display="Dynamic" ForeColor="Red"
                                            ErrorMessage="Password and confirm password do not match"
                                            ValidationGroup="password" SetFocusOnError="True">                       
                                        </asp:CompareValidator>

                </div>

                    <div class="input-group">
                        <div class="col-sm-12 controls">
                            <asp:Button ID="btnChangePassword" runat="server"  ValidationGroup="password" Text="Set New Password" OnClick="btnChangePassword_Click"
                                class="btn btn-success" />
                        </div>
                    </div>
                    
                </div>
            </div>
        </div>
    </div>
</asp:Content>
