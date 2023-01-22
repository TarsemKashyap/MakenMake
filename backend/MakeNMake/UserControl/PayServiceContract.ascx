<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PayServiceContract.ascx.cs" Inherits="MakeNMake.UserControl.PayServiceContract" %>

<script type="text/javascript">
    function DeleteConfirmation(ele) {
        var getValue = confirm('Are you sure you want to delete?');
        if (getValue) {
            return true;
        }
        else {
            return false;
        }
    }
</script>

<div class="panel panel-success">
    <div class="panel-heading">
        <h3 class="panel-title paneltitle">Pay Contract Amount</h3>
    </div>
    <div class="panel-body" style="padding-left: 0px;">
        <div class="col-sm-12" style="padding-left: 0px;">
            <div class="col-sm-6 text-left linkBottom linkBottom">
                <div class="input-group input-group-sm">
                    <span class="input-group-addon">Service Plan </span>
                    <asp:DropDownList ID="ddlplan" runat="server"
                        CssClass="form-control dropdown">
                        <asp:ListItem Text="--Select Service Plan--" Value="0"></asp:ListItem>
                        <asp:ListItem Text="Unlimited" Value="U"></asp:ListItem>
                        <asp:ListItem Text="Make Your Plan" Value="M"></asp:ListItem>
                        <asp:ListItem Text="Flexi" Value="F"></asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
            <div class="col-md-6 text-left linkBottom">
                <div class="input-group input-group-sm">
                    <span class="input-group-addon" id="Span7">Service Type</span>
                    <asp:DropDownList ID="ddltype" CssClass="form-control dropdown" OnSelectedIndexChanged="ddlPlan_SelectedIndexChanged" AutoPostBack="true" runat="server">
                        <asp:ListItem Text="--Select Service Type--" Value="0"></asp:ListItem>
                        <asp:ListItem Text="Basic" Value="B"></asp:ListItem>
                        <asp:ListItem Text="Add On" Value="A"></asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
        </div>
        <div class="col-sm-12">
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
                                <th></th>
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
                                <asp:Label ID="lblTotalSaving" runat="server" Text='<%# Eval("totalSaving")%>'></asp:Label></td>
                            <td>
                                <asp:Button ID="btnDelete" runat="server" OnClientClick="javascript:return DeleteConfirmation(this);" CommandArgument='<%#Eval("TempOrderID") %>' CommandName="delete" Text="Delete Service from Contract" /></td>
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
        <div class="col-sm-12" style="padding-left: 0px;">
            <div class="col-sm-6 text-left linkBottom linkBottom">
                <asp:Button ID="btnFinalPayment" runat="server" CssClass="btn btn-success" Text="Amount to Pay" OnClick="btnFinalPayment_Click" />
                <asp:Button ID="btnBack" runat="server" CssClass="btn btn-success" Text="Back" OnClick="btnBack_Click" />
            </div>
        </div>
    </div>
</div>
