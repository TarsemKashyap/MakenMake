<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="ClientInvoice.aspx.cs" Inherits="MakeNMake.Pages.ClientInvoice" %>


<!DOCTYPE html>


<head id="Head1" runat="server">
    <title></title>
    <script type="text/javascript">
        function PrintPanel() {
            var panel = document.getElementById("rd");
            var rdheader = document.getElementById("rdHeader");
            var printWindow = window.open('', '', 'height=800,width=600');
            printWindow.document.write('<html><head>');
            printWindow.document.write(rdheader.innerHTML);
            printWindow.document.write('</head><body >');
            printWindow.document.write(panel.innerHTML);
            printWindow.document.write('</body></html>');
            printWindow.document.close();
            setTimeout(function () {
                printWindow.print();
            }, 500);
            return false;
        }

    </script>
    <meta http-equiv="content-type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link href="../Static/bootstrap/bootstrap.css" rel="stylesheet" />
    <link href="../Static/bootstrap/bootstrap-theme.min.css" rel="stylesheet" />
    <script src="../Scripts/jquery-2.0.1.min.js"></script>
    <script>
    // function closedialog()
    //{
    //    $('#MyDialog').dialog('close');
    //    return false;
    //}
    </script>
    <script src="../Static/bootstrap/bootstrap.min.js"></script>
    <script type="text/javascript" src="../Static/js/validation.js"></script>
    <link href="../Static/css/makenmake.css" rel="stylesheet" />
    <link rel="icon" type="image/png" href="../Static/images/ico.ico" sizes="96x96" />
    <style type="text/css">
        .table > thead > tr > th {
            text-align: center;
        }

        .table > tbody > tr > td {
            text-align: left;
        }

        table td[class*="col-"], table th[class*="col-"] {
    position: static;
    float: left !important;
    display: table-cell;
}

    </style>


</head>

<body>
    <form id="form1" runat="server">
        <asp:Panel ID="pnlInvoiceData" runat="server">

            <div id="rdHeader">
                         <meta http-equiv="content-type" content="text/html; charset=utf-8" />
            <meta name="viewport" content="width=device-width, initial-scale=1" />
            <link href="http://122.180.87.82/MNM/Static/bootstrap/bootstrap.css" rel="stylesheet" />
            <link href="http://122.180.87.82/MNM/Static/bootstrap/bootstrap-theme.min.css" rel="stylesheet" />
            <script src="http://122.180.87.82/MNM/Scripts/jquery-2.0.1.min.js"></script>

            <script src="http://122.180.87.82/MNM/Static/bootstrap/bootstrap.min.js"></script>
            <script type="text/javascript" src="http://122.180.87.82/MNM/Static/js/validation.js"></script>
            <link href="http://122.180.87.82/MNM/Static/css/makenmake.css" rel="stylesheet" />
                </div>
   
           <div id="rd" runat="server" style="width: 100%;  padding: 13px 20px 10px 20px;">
            <div class="container-fluid ">

              <div class="row header" style="padding: 20px 20px 20px 20px;">
                  <div style="width:100%;">
                      <div style="width:50%; float:left;"><img src="http://122.180.87.82/MNM/Static/images/12.png" alt="" style="width: 90px;height: 90px;"/></div>
                       <div style="width:50%; float:left;">
                           <div class="input-group input-group-sm" style="padding-top: 14px;">                              
                                       <span class="">MnM Services Pvt. Ltd.</span><BR />
                                        <span class="glyphicon glyphicon-home"></span>C-503, UNITECH BUSINESS ZONE, NIRVANA COUNTRY<BR />
                                        <span class="glyphicon glyphicon-home"></span>SECTOR 50 GURGAON-122018
                                        
                                    </div>
                       </div>

                  </div>


  <%--                <table>
                          <tr style="width:100%;">
                            <td class="col-sm-6 " style="width:50%;">
                                <div class="input-group input-group-sm">
                                    <img src="http://122.180.87.82/MNM/Static/images/12.png" alt="" style="width: 90px;height: 90px;"/>
                                </div>
                            </td>
                            <td class="col-sm-6" style="width:50%;">  
                                <div class="input-group input-group-sm">                              
                                       <span class="input-group-addon">MnM Services Pvt. Ltd.</span><BR />
                                        <span class="glyphicon glyphicon-home"></span>C-503, UNITECH BUSINESS ZONE, NIRVANA COUNTRY<BR />
                                        <span class="glyphicon glyphicon-home"></span>SECTOR 50 GURGAON-122018
                                        
                                    </div>
                                                                  
                            </td>
                             </tr>

                    </table>--%>
                </div>
            </div>

           
               
                <div class="panel panel-success table-responsive">
                    <div runat="server" id="divconsumer" class="panel-heading">
                        <h3 class="panel-title paneltitle">Contract and Invoice Form:</h3>
                    </div>
                   
                    <div class="panel-body table-responsive"  style="padding-left: 0px; background-image:url('http://122.180.87.82/MNM/Static/images/makenmake.png')">
                         
                        <table class="col-sm-12" style="padding-left: 0px; background-image:url('http://122.180.87.82/MNM/Static/images/makenmake.png')">
                         <tr style="width:100%;">
                            <td class="col-sm-6 text-left linkBottom" style="width:50%;">
                                <div class="input-group input-group-sm">
                                    <span class="input-group-addon" >Client Id:</span>
                                    <label id="lblUserID" class="form-control" runat="server"></label>
                                </div>
                            </td>
                            <td class="col-sm-6 text-left linkBottom" style="width:50%;">
                                <div class="input-group input-group-sm">
                                    <span class="input-group-addon" id="Span1">Agreement No:</span>
                                    <label id="lblAgreementNumber" class="form-control" runat="server"></label>
                                </div>
                            </td>
                             </tr>
                            <tr >
                            <td class="col-sm-6 text-left linkBottom">
                                <div class="input-group input-group-sm">
                                    <span class="input-group-addon" id="Span2">Invoice No:</span>
                                    <label id="lblInvoice" class="form-control" runat="server"></label>
                                </div>

                            </td>
                            <td class="col-sm-6 text-left linkBottom">
                                <div class="input-group input-group-sm">
                                    <span class="input-group-addon" id="Span8">Name:</span>
                                    <label id="lblName" class="form-control" runat="server"></label>
                                </div>
                            </td>
                                </tr>
                            <tr >
                            <td class="col-sm-6 text-left linkBottom">
                                <div class="input-group input-group-sm">
                                   
                                        <span id="Span4" class="input-group-addon">Address:</span>
                                        <label id="lblAddress" runat="server" class="form-control">
                                        </label>
                                   
                                </div>
                            </td>

                                
                                    <td class="col-sm-6 text-left linkBottom" >
                                        <div class="input-group input-group-sm">
                                            <span id="Span3" class="input-group-addon">Email Id:</span>
                                            <label id="lblEmailID" runat="server" class="form-control">
                                            </label>
                                        </div>
                                    </td>
                                </tr>
                                <tr >
                                    <td class="col-sm-6 text-left linkBottom" >
                                        <div class="input-group input-group-sm">
                                            <span id="Span7" class="input-group-addon">Mobile No:</span>
                                            <label id="lblMobileNo" runat="server" class="form-control">
                                            </label>
                                        </div>
                                    </td>
                                    <td class="col-sm-6 text-left linkBottom" >
                                        <div class="input-group input-group-sm">
                                            <span id="Span5" class="input-group-addon">Service Plan:</span>
                                            <label id="lblServicePlan" runat="server" class="form-control">
                                            </label>
                                        </div>
                                    </td>
                                </tr>
                                <tr >
                                    <td class="col-sm-6 text-left linkBottom" >
                                        <div class="input-group input-group-sm">
                                            <span id="Span10" class="input-group-addon">Plan Start Date:</span>
                                            <label id="lblStartDate" runat="server" class="form-control">
                                            </label>
                                        </div>
                                    </td>
                                    <td class="col-sm-12 text-left linkBottom" >
                                        <div class="input-group input-group-sm">
                                            <span id="Span6" class="input-group-addon">Name of Service/No. of Call:</span>
                                            <label class="form-control input-lg" runat="server" style="resize:none;height:42px;" id="lblServiceCall" />
                                        </div>
                                    </td>
                                </tr>
                               
                                <tr>
                                    <td class="col-sm-6 text-left linkBottom">
                                        <div class="input-group input-group-sm">
                                            <span id="Span9" class="input-group-addon">Plan Expiry Date:</span>
                                            <label id="lblExpirydate" runat="server" class="form-control">
                                            </label>
                                        </div>
                                    </td>
                                    <td class="col-sm-6 text-left linkBottom">
                                        <div class="input-group input-group-sm">
                                            <span id="Span11" class="input-group-addon">Amount:</span>
                                            <label id="lblAmount" runat="server" class="form-control">
                                            </label>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="col-sm-6 text-left linkBottom">
                                        <div class="input-group input-group-sm">
                                            <span id="Span12" class="input-group-addon">Amount (inclusive tax):</span>
                                            <label id="lblAmountInclusive" runat="server" class="form-control">
                                            </label>
                                        </div>
                                    </td>
                                    <td>
                                    </td>
                                </tr>

                          
                        </table>
                       
                    </div>
                       
                    
                </div>
                   
            </div>
          
     
          
        </asp:Panel>
        <asp:Panel ID="dvterms" runat="server" style="margin-bottom:10px;">
            <div class="col-sm-10">
                <div class="col-sm-1">
                    <asp:CheckBox ID="chkTerms"  Checked="true" Enabled="false"  runat="server" />
                </div>
               <div class="col-sm-11">
                   You had already agreed to the Terms & Conditions mentioned by MNM before payment
                </div>
            </div>
        </asp:Panel>
        <asp:Panel ID="sendingdv" runat ="server" >
        <div class="col-sm-12 text-left linkBottom">
            <asp:Button ID="btnSavePdf" Text="Save As PDF" runat="server" OnClick="btnSavePdf_Click" />
            <asp:Button ID="btnEmailSent" Text="Send As PDF" runat="server" OnClick="btnEmailSent_Click" />
            <asp:Button ID="btnPrint" Text="Print" runat="server" OnClientClick = "return PrintPanel();" OnClick="btnPrint_Click" />
        </div>
        <asp:Panel ID="panelemail" runat="server" Visible="false" style="margin-bottom:20px;" CssClass="col-sm-12 ">
            <div class="input-group input-group-xs">
                <asp:TextBox ID="txtEmailID" placeholder="Enter the Mail ID" Width="200px"
                    CssClass="form-control" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ForeColor="Red" ControlToValidate="txtEmailID"
                    ErrorMessage="Enter EmailID" ValidationGroup="email"></asp:RequiredFieldValidator>
                <asp:Button ID="btnEmail" runat="server" Text="Send" Style="margin-left: 10px;" ValidationGroup="email"
                    CssClass="btn btn-success" OnClick="btnEmail_Click" />
            </div>
        </asp:Panel>
      </asp:Panel>
    
    </form>
</body>

