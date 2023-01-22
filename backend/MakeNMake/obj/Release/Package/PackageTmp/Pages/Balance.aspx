<%@ Page Title="Balance History" Language="C#" MasterPageFile="AdminMaster.Master" AutoEventWireup="true"
    CodeBehind="Balance.aspx.cs" Inherits="MakeNMake.Customer.Balance" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="panel panel-success">
        <div class="panel-heading">
            <h3 class="panel-title paneltitle">Balance </h3>
        </div>
        <div class="panel-body">
            <div class="table-responsive">
                <asp:HiddenField ID="hdnCustomer" ClientIDMode="Static" runat="server" />
                <asp:Label ID="lblMsg" CssClass="label label-success" runat="server"></asp:Label>
                <asp:Repeater ID="RptService" runat="server" OnItemCommand="RptService_ItemCommand">
                    <HeaderTemplate>
                        <table class="table table-hover  table-bordered  table-condensed" data-height="400">
                            <thead style="color: White; background-color: #6b9297;">
                                <tr>
                                    <th>Agreement Number</th>
                                    <th>Plan Status </th>
                                    <th>Plan Expiration Date</th>
                                    <th>Total Calls</th>
                                    <th>Total Amount</th>
                                    <%--   <th>Total Duration (in mins)</th>--%>
                                    <%--<th>Used Duration (in mins)</th>--%>
                                    <th>Remaining Calls</th>
                                    <th>Remaining Amount</th>
                                    <%--<th>Remaining Duration</th>--%>
                                    <%-- <th>Call Ticket History</th>--%>
                                    <th>Wallet Money</th>
                                </tr>
                            </thead>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tbody>
                            <tr>
                                <td>
                                    <asp:HiddenField ID="hdnAgreementID" runat="server" />
                                    <asp:Label ID="Label1" runat="server" Text='<%#Eval("AgreementNumber") %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblpayment" runat="server" Text='<%#Eval("PlanStatus") %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="Label5" runat="server" Text='<%#Eval("Expirationdate") %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="Label2" runat="server" Text='<%#Eval("TotalCalls") %>'></asp:Label>

                                </td>
                                <td>
                                    <asp:Label ID="lblamt" runat="server" Text='<%#Eval("PlanAmount") %>'></asp:Label>
                                </td>
                               <%-- <td>
                                    <asp:Label ID="lblinvoicedate" runat="server" Text='<%#Eval("TotalCallDuration") %>'></asp:Label>
                                </td>--%>
                            <%--    <td>
                                    <asp:Label ID="Label6" runat="server" Text='<%#Eval("TotalUsedMinutes") %>'></asp:Label>
                                </td>--%>
                                <td>
                                    <asp:Label ID="lbldate" runat="server" Text='<%#Eval("RemainingCalls") %>'></asp:Label></td>
                                <td>
                                    <asp:Label ID="Label4" runat="server" Text='<%#Eval("RemainingAmount") %>'></asp:Label></td>
                               <%-- <td>
                                    <asp:Label ID="Label3" runat="server" Text='<%#Eval("RemainingDuration") %>'></asp:Label></td>--%>
                                <%--   <td>
                                    <asp:LinkButton ID="lnkBtnhistory" Text="View history"  runat="server" ToolTip="Click to view Info"
                                        CommandName="history"  >

                                    </asp:LinkButton>
                                </td>--%>
                                <td>
<asp:Label ID="Label3" runat="server" Text='<%#Eval("WalletMoney") %>'></asp:Label></td>
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

</asp:Content>
