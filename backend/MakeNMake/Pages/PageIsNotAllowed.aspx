<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/AdminMaster.Master" AutoEventWireup="true" CodeBehind="PageIsNotAllowed.aspx.cs" Inherits="MakeNMake.Pages.PageIsNotAllowed" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div class="panel panel-success">

        <div runat="server" id="divuser" class="panel-heading">
            <h3 class="panel-title paneltitle">You are not Permitted</h3>
        </div>
        <div class="panel-body">
            <img src="../Static/images/not%20permitted.jpg" />
        </div>
        <div class="panel-footer">
            <asp:Button ID="btnCancel" Text="Go to Dashboard" runat="server" CssClass="btn btn-success" OnClick="btnCancel_Click" />
            </div>
    </div>
</asp:Content>
