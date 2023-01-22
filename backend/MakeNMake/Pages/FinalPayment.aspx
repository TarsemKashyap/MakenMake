<%@ Page Title="FinalPayment" Language="C#" MasterPageFile="AdminMaster.Master" AutoEventWireup="true"
     CodeBehind="FinalPayment.aspx.cs" Inherits="MakeNMake.Customer.FinalPayment" %>

<%@ Register Src="~/UserControl/ServicesFinalPayment.ascx" TagPrefix="uc1" TagName="ServicesFinalPayment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:ServicesFinalPayment runat="server" ID="ServicesFinalPayment" />
</asp:Content>
