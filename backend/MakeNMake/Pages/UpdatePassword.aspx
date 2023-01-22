<%@ Page Title="Change Password" Language="C#" MasterPageFile="AdminMaster.Master" AutoEventWireup="true" CodeBehind="UpdatePassword.aspx.cs" Inherits="MakeNMake.Admin.UpdatePassword" %>

<%@ Register Src="~/UserControl/ChangePassword.ascx" TagPrefix="uc1" TagName="ChangePassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../Scripts/jquery-2.0.1.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $(window).keydown(function (event) {
                if (event.keyCode == 13) {
                    event.preventDefault();
                    return false;
                }
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="panel panel-success">
        <div class="panel-heading">
            <h3 class="panel-title paneltitle">Change Password</h3>
        </div>
        <div class="panel-body" style="padding-left:0px;">
            <uc1:ChangePassword runat="server" ID="ChangePassword" />
        </div>
    </div>
</asp:Content>
