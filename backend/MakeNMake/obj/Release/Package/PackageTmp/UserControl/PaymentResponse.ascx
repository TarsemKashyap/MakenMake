<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PaymentResponse.ascx.cs" Inherits="MakeNMake.UserControl.PaymentResponse" %>
<%@ Register Src="~/UserControl/SubscriptionForm.ascx" TagPrefix="uc1" TagName="SubscriptionForm" %>



<div class="panel panel-success">
    <div class="panel-heading">
        <h3 class="panel-title paneltitle" id="paymentResponse" runat="server"></h3>
    </div>
    <div class="panel-body" style="padding-left: 0px;">

        <div id="divSubscriptionForm" runat="server">
            <uc1:SubscriptionForm runat="server" ID="SubscriptionForm" />
        </div>
    </div>
</div>
