    <%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ServicesProceedToPayment.ascx.cs" Inherits="MakeNMake.UserControl.ServicesProceedToPayment" %>
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
    function BackConfirmation(ele) {
        var getValue = confirm('Are you sure you want to go to back, it will delete all these services and let you select new services?');
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
        <h3 class="panel-title paneltitle">Benefits</h3>
    </div>
    <div class="panel-body" style="padding-left:0px;">
        <div class="col-sm-12 clear">
            <div class="table-responsive">
                <asp:HiddenField ID ="hdnPage" runat="server" />
                <asp:Repeater ID="RptService" runat="server" OnItemCommand="RptService_ItemCommand" OnItemDataBound="RptService_ItemDataBound">
                    <HeaderTemplate>
                        <table class="table table-hover  table-bordered  table-condensed" data-height="400">
                            <thead style="color: White; background-color: #6b9297;">
                                <tr>
                                    <th>Service</th>
                                    <th>Category</th>
                                    <th>ServiceType</th>
                                   <%-- <th>Quantity</th>--%>
                                    <th>Price(₹)/Time(mins)</th>
                                    <th>No Of Calls</th>
                                   <%-- <th>Amount after discount</th>
                                    <th>Saving (Vertical)</th>
                                    <th>Saving (Horizontal)</th>--%>
                                    <th>Saving</th>
                                    <%--<th></th>--%>
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
                               <%-- <td>
                                    <asp:Label ID="lblQuant" runat="server" Text='<%# Eval("Quantity")%>'></asp:Label></td>--%>
                                <td>
                                    <asp:Label ID="lblAmount" runat="server" Text='<%# Eval("Amount")%>'></asp:Label>/
                                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("duration")%>'></asp:Label>
                                </td>
                                <td>
                                                    <asp:HiddenField ID ="hdnOriginal" runat="server" Value='<%# Eval("original")%>' />
                                    <asp:Label ID="lblCalls" runat="server" Text='<%# Eval("NoOfCalls")%>'></asp:Label></td>
                             <%--   <td>
                                    <asp:Label ID="lblAfteramunt" runat="server" Text='<%# Convert.ToDecimal( Eval("AmountAfter")).ToString("0.00")%>'></asp:Label></td>
                              --%>  
                            <%--    <asp:Label ID="lblAfteramunt" Visible="false" runat="server" Text='<%# Convert.ToDecimal( Eval("AmountAfter")).ToString("0.00")%>'></asp:Label>
                                <td>
                                    <asp:Label ID="lblSaving" style="color:green;font-weight:bold;" runat="server" Text='<%# Convert.ToDecimal( Eval("Saving")).ToString("0.00")%>'></asp:Label></td>
                                 <td>
                                    <asp:Label ID="lblSavingHorizontal" style="color:green;font-weight:bold;" runat="server" Text='<%# Convert.ToDecimal( Eval("SavingHoriZontal")).ToString("0.00")%>'></asp:Label></td>--%>
                                
                                <td>
                                    <asp:Label ID="lblTotalSaving" runat="server" Text='<%# Eval("Saving")%>'></asp:Label></td>
                               <%-- <td>
                               <%-- <td>
                                    <asp:Button ID="btnDelete" runat="server" OnClientClick="javascript:return DeleteConfirmation(this);" CommandArgument='<%#Eval("TempOrderID") %>' CommandName="delete" Text="Delete Service" /></td>--%>

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
        <div class="col-sm-12 clear">
            <asp:Button ID="btnFinalPayment" runat="server" CssClass="btn btn-success"  Text="Amount to Pay" OnClick="btnFinalPayment_Click" />
            <asp:Button ID="btnBack" runat="server" CssClass="btn btn-success" Text="Back" OnClick="btnBack_Click" OnClientClick="javascript:return BackConfirmation(this);" />
        </div>
    </div>
</div>
