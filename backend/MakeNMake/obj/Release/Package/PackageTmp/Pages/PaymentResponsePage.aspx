<%@ Page Title="Payment Response " Language="C#" MasterPageFile="~/Pages/AdminMaster.Master" AutoEventWireup="true" CodeBehind="PaymentResponsePage.aspx.cs" Inherits="MakeNMake.Pages.PaymentResponsePage" %>

<%@ Register Src="~/UserControl/SubscriptionForm.ascx" TagPrefix="uc1" TagName="SubscriptionForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:HiddenField ID="hdnPlan" runat="server" />
    <asp:HiddenField ID="hdnType" runat="server" />
    <asp:HiddenField ID="hdnCategory" runat="server" />
    <div class="panel panel-success">
        <div class="panel-heading">
            <h3 class="panel-title paneltitle">Payment Details </h3>
        </div>
        <div class="panel-body" style="padding-left: 0px;">
            <div class="col-md-6 text-left linkBottom">

                <div class="input-group input-group-sm">
                    <span class="input-group-addon">Payment TransactionId
                    </span>
                    <asp:Label ID="lbltransactionID" CssClass="form-control" runat="server"></asp:Label>
                </div>
            </div>
            <div class="col-md-6 text-left linkBottom">
                <div class="input-group input-group-sm">
                    <span class="input-group-addon">Payment Status
                    </span>
                    <asp:Label ID="lblPaymentStatus" CssClass="form-control" runat="server"></asp:Label>
                </div>
            </div>
            <div class="col-md-6 text-left linkBottom">
                <div class="input-group input-group-sm">
                    <span class="input-group-addon">Amount Received
                    </span>
                    <asp:Label ID="lblamount" CssClass="form-control" runat="server"></asp:Label>
                </div>
            </div>
            <div class="col-md-6 text-left linkBottom">
                <div class="input-group input-group-sm">
                    <span class="input-group-addon">Account Holder
                    </span>
                    <asp:Label ID="lblName" CssClass="form-control" runat="server"></asp:Label>
                </div>
            </div>
            <div class="col-sm-12">
                <div class="panel-danger">Please do not refresh the page and press the back button of browser</div>
            </div>
        </div>
    </div>

    <div id="subscriberForm" runat="server" visible="false">
        <div class="panel panel-success">
            <div class="panel-heading">
                <h3 class="panel-title paneltitle">Fill Client Subscription Details</h3>
            </div>
            <div class="panel-body" style="padding-left: 0px;">
                   <div class="col-sm-12">  <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-success" Text="Proceed to Details" OnClick="btnSubmit_Click" /></div>
            </div>
        </div>
    </div>
</asp:Content>
