<%@ Page Title="Request Form" Language="C#" MasterPageFile="AdminMaster.Master" AutoEventWireup="true" CodeBehind="VisitRequestForm.aspx.cs" Inherits="MakeNMake.Customer.VisitRequestForm" %>

<%@ Register Src="~/UserControl/RequestForm.ascx" TagPrefix="uc1" TagName="RequestForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:RequestForm runat="server" id="RequestForm" />
</asp:Content>
