<%@ Page Title="Pay Contract Amount" Language="C#" MasterPageFile="~/Pages/AdminMaster.Master" AutoEventWireup="true" CodeBehind="PayContractAmount.aspx.cs" Inherits="MakeNMake.Pages.PayContractAmount" %>

<%@ Register Src="~/UserControl/PayServiceContract.ascx" TagPrefix="uc1" TagName="PayServiceContract" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:PayServiceContract runat="server" id="PayServiceContract" />
</asp:Content>
