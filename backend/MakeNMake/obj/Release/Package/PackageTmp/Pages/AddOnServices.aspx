<%@ Page Title="AddOn Services" Language="C#" MasterPageFile="~/Pages/AdminMaster.Master" AutoEventWireup="true" CodeBehind="AddOnServices.aspx.cs" Inherits="MakeNMake.Pages.AddOnServices" %>

<%@ Register Src="~/UserControl/AddOnServicesUserControl.ascx" TagPrefix="uc1" TagName="AddOnServicesUserControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:AddOnServicesUserControl runat="server" id="AddOnServicesUserControl" />
</asp:Content>
