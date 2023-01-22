<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ConfirmCitrusPaymentAction.aspx.cs" Inherits="MakeNMake.Pages.ConfirmCitrusPaymentAction" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Confirm Payment</title>
    <link href="../Static/bootstrap/bootstrap.min.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
        </div>
    </form>
    <div class="col-sm-12">
        <div class="col-sm-3"></div>
        <div class="col-sm-6"></div>
        <div class="panel panel-success">
            <div class="panel-heading">
                <h3 class="panel-title paneltitle">Confirm Payment Details</h3>
            </div>
            <div class="panel-body" style="padding-left: 0px;">
                <div class="col-sm-12">TransactionId :-
                    <asp:Label ID="lblMerchantID" runat="server"></asp:Label>
                    ,Please use it for future reference</div>
                <div class="col-sm-12">Amount to be paid :-
                    <asp:Label ID="lblAmount" runat="server"></asp:Label></div>
                <div class="col-sm-12">
                    <div style="color:red;">Please do not refresh the page and press the back button of browser</div>
                </div>
                
            </div>
        </div>
        <div class="col-sm-12">
            <form id="form2" runat="server" method="post"  action="<%=formPostUrl%>">
           <input type="hidden" id="merchantTxnId" name="merchantTxnId" value="<%=merchantTxnId%>" />
             <input type="hidden" id="orderAmount" name="orderAmount" value="<%=orderAmount%>" />
             <input type="hidden" id="currency" name="currency" value="<%=currency%>" />
             <input type="hidden" name="returnUrl" value="<%=returnUrl%>" />
             <input type="hidden" id="notifyUrl" name="notifyUrl" value="<%=notifyUrl%>" />
             <input type="hidden" id="secSignature" name="secSignature" value="<%=securitySignature%>" />
                <input type="submit" name="Pay Now" class="btn btn-success" value="Pay Now" />
            </form>
        </div>

        <div class="col-sm-3"></div>
    </div>
</body>

</html>

