<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PaymentResponsePageApp.aspx.cs" Inherits="MakeNMake.Pages.PaymentResponsePageApp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Payment status</title>
    <script type="text/javascript" src="../Static/js/validation.js"></script>
    <link href="../Static/css/makenmake.css" rel="stylesheet" />
    <link rel="icon" type="image/png" href="../Static/images/ico.ico" sizes="96x96" />
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7; IE=EmulateIE9" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1.0, user-scalable=no" />


    <link rel="stylesheet" type="text/css" media="all" />
    <link href="../Static/cssinner/style.css" rel="stylesheet" type="text/css" media="all" />
    <link href="../Static/font-awesome/css/font-awesome.min.css" rel="stylesheet" />
    <link href="../Static/bootstrap/bootstrap.min.css" rel="stylesheet" />
    <script src="../Scripts/jquery-2.0.1.min.js"></script>
    <script src="../Static/bootstrap/bootstrap.min.js"></script>
    
    

    <style type="text/css">
        .table > thead > tr > th {
            text-align: center;
        }

        .table > tbody > tr > td {
            text-align: left;
        }

        .navbar-toggle {
            background-color: #99FF33;
        }

        .navbar {
            margin-bottom: 20px;
        }

        .navbar-default .navbar-nav > li > a {
            color: white;
            margin-left: 15px;
        }
    </style>
</head>
<body>
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
          
        </div>
    </div>

    <%--<div id="subscriberForm" runat="server" visible="false">
        <div class="panel panel-success">
            <div class="panel-heading">
                <h3 class="panel-title paneltitle">Click Close return back to mobile app</h3>
            </div>
            <div class="panel-body" style="padding-left: 0px;">
                   <div class="col-sm-12"> 
                    
                       <a href="http://www.mypage.com/closeInAppBrowser.html">close</a>
                   </div>
            </div>
        </div>
    </div>--%>
</body>
</html>
