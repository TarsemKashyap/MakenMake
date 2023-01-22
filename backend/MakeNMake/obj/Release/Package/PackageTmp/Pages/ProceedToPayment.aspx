<%@ Page Title="Service With Discount" Language="C#" MasterPageFile="AdminMaster.Master" EnableEventValidation="true" AutoEventWireup="true" CodeBehind="ProceedToPayment.aspx.cs" Inherits="MakeNMake.Customer.ProceedToPayment" %>

<%@ Register Src="~/UserControl/ServicesProceedToPayment.ascx" TagPrefix="uc1" TagName="ServicesProceedToPayment" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">    
    <uc1:ServicesProceedToPayment runat="server" ID="ServicesProceedToPayment" />
</asp:Content>
