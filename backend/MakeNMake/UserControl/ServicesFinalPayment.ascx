<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ServicesFinalPayment.ascx.cs" Inherits="MakeNMake.UserControl.ServicesFinalPayment" %>
<%@ Register Src="~/UserControl/SubscriptionForm.ascx" TagPrefix="uc1" TagName="SubscriptionForm" %>
<script src="../Dialog/jquery-ui.min.js"></script>
<link href="../Dialog/jquery-ui.css" rel="stylesheet" />

<script type="text/javascript">

    $(document).ready(function () {

        $(".extraMoney").mouseover(function () {
            alert("If you pay from wallet , your money will be adjusted from wallet amount and you will have to pay the extra amount if any.");
        });

    });
    function PlanChangeConfirmation(ele) {
        var getValue = confirm(ele);
        if (getValue) {
            return true;
        }
        else {
            return false;
        }
    }
    function TermsCondition() {
        var text = $(".serviceMakeNMakePlan").text();
        if (text == "Flexi") {
            testwindow = window.open("../TermsConditions_Flexi.aspx", "mywindow", "location=1,status=1,scrollbars=1,width=500,height=600");
            testwindow.moveTo(500, 0);
        }
        else if (text == "Unlimited") {
            testwindow = window.open("../TermsConditions_Unlimited.aspx", "mywindow", "location=1,status=1,scrollbars=1,width=500,height=600");
            testwindow.moveTo(500, 0);
        }
        else if (text == "Make your Plan") {
            testwindow = window.open("../TermsCondition_MYP.aspx", "mywindow", "location=1,status=1,scrollbars=1,width=500,height=600");
            testwindow.moveTo(500, 0);
        }
    }

    function OpenInvoice(userid) {
        $('#MyDialog').html('<iframe border=0 width="950px" height ="600px" src= "ClientInvoice.aspx?UserID=' + userid + '"> </iframe>').dialog({
            title: '',
            modal: true,
            autoOpen: true,
            height: '650',
            width: '1000',
            resizable: false,
            position: ['left+40', 'top+30'],
            closeOnEscape: false,
            dialogClass: "alert"
        });
        // $('#modalC').modal('show');
    }
    function closedialog()
    {
        $('#MyDialog').dialog('close');
        return false;
    }
    function StartPop() {
        $('#modalR').modal('show');
    }
    
</script>
<div class="panel panel-success" id="payment" runat="server">
    <div class="panel-heading">
        <h3 class="panel-title paneltitle">Pay</h3>
    </div>
    <asp:HiddenField ID="hdnCurrent" runat="server" />
    <asp:HiddenField ID="hdnPrevious" runat="server" />
    <asp:HiddenField ID="hdnRemainingAmount" runat="server" />
    <asp:HiddenField ID="hdnWalletMoney" runat="server" />
    <div id="dvPayment" runat="server" style="padding-bottom: 0px;">
        <div class="panel-body" style="padding-left: 0px; padding-bottom: 0px;">
            <div class="col-sm-12" style="padding-left: 0px;">
                <div class="col-sm-6 text-left linkBottom">
                    <div class="input-group input-group-sm">
                        <span class="input-group-addon" id="Span1">Category</span>
                        <asp:Label ID="lblcategory" CssClass="form-control" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="col-sm-6 text-left linkBottom">
                    <div class="input-group input-group-sm">
                        <span class="input-group-addon" id="Span2">Service Type</span>
                        <asp:Label ID="lblServiceType" CssClass="form-control" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="col-sm-12 text-left linkBottom">
                    <div class="input-group input-group-sm">
                        <span class="input-group-addon" id="Span3">Services / No. Of calls</span>
                        <asp:Label ID="lblServices" CssClass="form-control" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="col-sm-6 text-left linkBottom">
                    <div class="input-group input-group-sm">
                        <span class="input-group-addon" id="Span4">Service Plan</span>
                        <asp:Label ID="lblSevicePlan"  CssClass="form-control serviceMakeNMakePlan" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="col-sm-6 text-left linkBottom">
                    <div class="input-group input-group-sm">
                        <span class="input-group-addon" id="Span5">Savings</span>
                        <asp:Label ID="lblSavngs" CssClass="form-control" runat="server"></asp:Label>
                    </div>
                </div>
                 <div class="col-sm-6 text-left ">
                    <div class="input-group input-group-sm">
                        <span class="input-group-addon" id="Span7">Total(Exclude Sr. Tax)</span>
                        <asp:Label ID="lblActualtotalPayment" CssClass="form-control" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="col-sm-6 text-left ">
                    <div class="input-group input-group-sm">
                        <span class="input-group-addon" id="Span6">Total(Including Sr. Tax)</span>
                        <asp:Label ID="lblTotalWithoutTax" CssClass="form-control" runat="server"></asp:Label>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div id="AlreadyPairServices" runat="server" visible="false" style="margin-top: -7px;">

        <div class="panel-body" style="padding-top: 0px;">
            <div class="col-sm-12" style="padding: 10px;">
                <asp:LinkButton ID="lnkBtnInvoice" CssClass="invoice" runat="server" Text="Previous Contract"></asp:LinkButton>
            </div>
            <div class="col-sm-12" id="dvRemaingAmount" runat="server" visible="false" style="padding-left: 0px; border: 1px black solid;">
                <div class="col-sm-8">
                    <asp:Label ID="lblInfo" runat="server"></asp:Label>
                    <asp:HiddenField ID="hdnPreviousPlan" runat="server" />
                </div>
            </div>
        </div>
    </div>
    <div class="panel-body">
        <asp:HiddenField ID="hdnAmount" runat="server" />
        <div class="col-sm-12 " style="padding-left: 0px; padding-bottom: 10px;">
            <div class="col-sm-1" style="padding-left: 0px;">
                <asp:CheckBox ID="chkTerms" runat="server" />
            </div>
            <div class="col-sm-11">
                I agree to the "<asp:LinkButton ID="lnkBtnTerms" OnClientClick="return TermsCondition();" runat="server">Terms & Conditions</asp:LinkButton>" mentioned by MNM
            </div>
        </div>
    </div>
    <div class="panel panel-success">
        <div class="panel-heading">
            <h3 class="panel-title paneltitle">Pay using :-</h3>
        </div>
        <div class="panel-body">
            <div class="col-sm-12" id="dvWallet" visible="false" runat="server" style="padding-left: 0px; padding-bottom: 10px;">
                <div class="col-sm-12" style="padding-left:0px;margin-bottom:20px;">Note : Your remaining amount from existing plan will automatically transferred to your Wallet</div>
                <div class="col-sm-1"><asp:RadioButton ID="rdbMakenMakeWallet" GroupName="pay" runat="server" /></div>
                <div class="col-sm-3" style="padding-left: 0px;">MakenMake Wallet</div>
                <div class="col-sm-3 extraMoney" style="padding-left: 0px;">
                    Existing Amount :
                    <asp:Label ID="lblwalletMoney" runat="server"></asp:Label>
                </div>
            </div>
            <div class="col-sm-12" id="dvCitrus" visible="false" runat="server" style="padding-left: 0px; padding-bottom: 10px;">
                <div class="col-sm-1"><asp:RadioButton ID="rdbCitrus" GroupName="pay" runat="server" /></div>
                <div class="col-sm-3" style="padding-left: 0px;">Citrus </div>
            </div>
            <div class="col-sm-12" id="dvWalletAndCitrus" visible="false" runat="server" style="padding-left: 0px; padding-bottom: 10px;">
                <div class="col-sm-1"><asp:RadioButton ID="rdbWalletandCitrus" GroupName="pay" runat="server" /></div>
                <div class="col-sm-6" style="padding-left: 0px;">MakenMake Wallet + Citrus<br /> (This will fetch all money from wallet and charge <br /> remaining from citrus)</div>
                <div class="col-sm-3 extraMoney" style="padding-left: 0px;">  Amount in wallet:
                    <asp:Label ID="lblBothWalletmoney" runat="server"></asp:Label></div>                
            </div>
            <div class="col-sm-12 " style="padding-left: 0px; padding-bottom: 10px;">
                 <asp:Button ID="btnPay" CssClass="btn btn-success" runat="server" Text="Pay Now" OnClick="btnPay_Click1" />
                 <asp:Button ID="btnBack" CssClass="btn btn-success" runat="server" Text="Back" OnClick="btnBack_Click" />
            </div>
        </div>
    </div>
</div>
<div id="MyDialog">
</div>



