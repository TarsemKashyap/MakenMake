<%@ Page Title="Services Purchased" Language="C#" MasterPageFile="AdminMaster.Master" AutoEventWireup="true" CodeBehind="ServicePurchasedByClient.aspx.cs" Inherits="MakeNMake.Customer.ServicePurchasedByClient" %>
<%@ Register Src="~/UserControl/UserInfo.ascx" TagPrefix="uc1" TagName="UserInfo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
 <script src="../Scripts/jquery-2.0.1.min.js"></script>
 <%--   <link href="../Static/bootstrap/bootstrap.css" rel="stylesheet" />
    <script src="../Static/bootstrap/bootstrap.min.js"></script>--%>
    
  
    <script type="text/javascript">
        $(document).ready(function () {
            if ($("#hdnCustomer").val() == "1") {
                $('#myModal').modal({ show: true });
                $("#hdnCustomer").val("");
            }
        });
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
            return false;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="panel panel-success">
        <div class="panel-heading">
            <h3 class="panel-title paneltitle">Purchased Services </h3>
        </div>
        <div class="panel-body">
            <div class="table-responsive">
                  <asp:HiddenField ID="hdnCustomer" ClientIDMode="Static" runat="server" />
                <asp:Label ID="lblMsg" CssClass="label label-success" runat="server"></asp:Label>
                <asp:Repeater ID="RptService" runat="server" OnItemCommand="RptService_ItemCommand"  OnItemDataBound="RptService_ItemDataBound">
                    <HeaderTemplate>
                        <table class="table table-hover  table-bordered  table-condensed" data-height="400">
                            <thead style="color: White; background-color: #6b9297;">
                                <tr>
                                      <th>Invoice No</th>
                                    <th>Payment Mode </th>
                                    <th>Agreement No</th>
                                    <th>Total Amount</th>
                                    <th>Service Purchased Date</th>
                                    <th>Service Expiration Date</th>
                                   
                                </tr>
                            </thead>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tbody>
                            <tr><td>
                                    <%--<asp:Label ID="Label1" runat="server" Text='<%#Eval("InvoiceNumber") %>'></asp:Label>--%>
                                <asp:LinkButton ID="LinkButton1"  Text='<%#Eval("InvoiceNumber") %>'  runat="server" ToolTip="Click to view Info">

                                    </asp:LinkButton>
                                </td>
                                <td>
                                    <asp:Label ID="lblpayment" runat="server" Text='<%#Eval("PaymentMode") %>'></asp:Label>
                                </td>
                               
                                <td>
                                    <asp:LinkButton ID="lnkBtnClient" Text='<%#Eval("AgreementNumber") %>'  runat="server" ToolTip="Click to view Info"
                                        CommandName="Agreement"  CommandArgument='<%#Eval("AgreementID") %>'>

                                    </asp:LinkButton>

                                </td>
                                 <td>
                                    <asp:Label ID="lblamt" runat="server" Text='<%#Eval("Amount") %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblinvoicedate" runat="server" Text='<%#Eval("Invoicedate") %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lbldate" runat="server" Text='<%#Eval("ExpiryDate") %>'></asp:Label></td>
                                
                            </tr>
                        </tbody>
                    </ItemTemplate>
                    <FooterTemplate>
                        </table>
                    </FooterTemplate>
                </asp:Repeater>
            </div>
        </div>
    </div>
    <div id="MyDialog" >        
                
    </div>
    <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-hidden="true">
        <uc1:userinfo runat="server" ID="UserInfo" />
    </div>
    
<script src="../Dialog/jquery-ui.min.js"></script>
<link href="../Dialog/jquery-ui.css" rel="stylesheet" />
</asp:Content>

