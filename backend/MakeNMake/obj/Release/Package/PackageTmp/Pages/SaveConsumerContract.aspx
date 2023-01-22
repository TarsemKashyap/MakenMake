<%@ Page Title="Check before final Saving" Language="C#" MasterPageFile="~/Pages/AdminMaster.Master" AutoEventWireup="true" CodeBehind="SaveConsumerContract.aspx.cs" Inherits="MakeNMake.Pages.SaveConsumerContract" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function GoToPage() {
            var getValue = confirm('Payment done successfully .Email and sms sent to client');
            if (getValue) {
                window.location.href = "EditServiceContract.aspx";
            }
            else {
                window.location.href = "EditServiceContract.aspx";
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="panel panel-success">
        <div class="panel-heading">
            <h3 class="panel-title paneltitle">Please Check before Saving </h3>
        </div>
        <div class="panel-body" style="padding-left: 0px;">
            <div class="col-sm-12 clear">
                <div class="table-responsive">
                    <asp:HiddenField ID="hdnPage" runat="server" />
                    <asp:Repeater ID="RptService" runat="server" OnItemCommand="RptService_ItemCommand" OnItemDataBound="RptService_ItemDataBound">
                        <HeaderTemplate>
                            <table class="table table-hover  table-bordered  table-condensed" data-height="400">
                                <thead style="color: White; background-color: #6b9297;">
                                    <tr>
                                        <th>Service</th>
                                        <th>Category</th>
                                        <th>ServiceType</th>
                                        <th>Price(₹)/Time(mins)</th>
                                        <th>No Of Calls</th>
                                        <th>Saving</th>
                                    </tr>
                                </thead>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tbody>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblName" runat="server" Text='<%#Eval("ServiceName") %>'></asp:Label></td>
                                    <td>
                                        <asp:Label ID="lblCategory" runat="server" Text='<%#Eval("Category") %>'></asp:Label></td>
                                    <td>
                                        <asp:Label ID="lblType" runat="server" Text='<%#Eval("Servicetype") %>'></asp:Label></td>
                                    <td>
                                        <asp:Label ID="lblAmount" runat="server" Text='<%# Eval("Amount")%>'></asp:Label>/
                                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("duration")%>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:HiddenField ID="hdnOriginal" runat="server" Value='<%# Eval("original")%>' />
                                        <asp:Label ID="lblCalls" runat="server" Text='<%# Eval("NoOfCalls")%>'></asp:Label></td>

                                    <td>
                                        <asp:Label ID="lblTotalSaving" runat="server" Text='<%# Eval("Saving")%>'></asp:Label></td>
                        </ItemTemplate>
                        <FooterTemplate>
                            <tr>
                                <td colspan="11" style="color: White; background-color: #6b9297; font-weight: bold;">
                                    <asp:Label ID="lblTotal" runat="server" Text='<%# Eval("Amount")%>'></asp:Label></td>
                                </td>
                            </tr>
                            </tbody>
                            </table>
                        </FooterTemplate>
                    </asp:Repeater>
                </div>
            </div>

            <div class="col-sm-12 text-left linkBottom">
                <div class="input-group input-group-sm">
                    <span class="input-group-addon" id="Span2">Enter Payment Info</span>
                    <asp:TextBox ID="txtpaymentInfo" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic" ForeColor="Red"
                    ErrorMessage="Enter Payment Info" ControlToValidate="txtpaymentInfo" ValidationGroup="payment"></asp:RequiredFieldValidator>
            </div>

            <div class="col-sm-12 clear">
                <asp:Button ID="btnFinalPayment" runat="server" ValidationGroup="payment" CssClass="btn btn-success" Text="Save" OnClick="btnFinalPayment_Click" />
                <asp:Button ID="btnBack" runat="server" CssClass="btn btn-success" Text="Back" OnClick="btnBack_Click" />
            </div>
        </div>
    </div>
</asp:Content>
