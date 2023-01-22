<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/AdminMaster.Master" AutoEventWireup="true" CodeBehind="SubscriptionDetail.aspx.cs" Inherits="MakeNMake.Pages.SubscriptionDetail" %>

<%@ Register Src="~/UserControl/SubscriptionForm.ascx" TagPrefix="uc1" TagName="SubscriptionForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:SubscriptionForm runat="server" ID="SubscriptionForm" />
</asp:Content>
