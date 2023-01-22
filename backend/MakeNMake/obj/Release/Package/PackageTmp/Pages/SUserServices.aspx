<%@ Page Title="Select Services" Language="C#" MasterPageFile="~/Pages/AdminMaster.Master" AutoEventWireup="true" CodeBehind="SUserServices.aspx.cs" Inherits="MakeNMake.Pages.SUserServices" %>

<%@ Register Src="~/UserControl/UserServices.ascx" TagPrefix="uc1" TagName="UserServices" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:UserServices runat="server" ID="UserServices" />
</asp:Content>
