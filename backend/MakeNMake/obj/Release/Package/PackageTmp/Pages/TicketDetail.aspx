<%@ Page Title="Ticket Details" Language="C#" MasterPageFile="AdminMaster.Master" AutoEventWireup="true" CodeBehind="TicketDetail.aspx.cs" Inherits="MakeNMake.ServiceEngineer.TicketDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function ShowMsg(ele) {
            alert(ele.nextElementSibling.value);
            return false;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="panel panel-success">
        <div class="panel-heading">
            <h3 class="panel-title paneltitle">Ticket Details</h3>
        </div>
        <div class="panel-body">
            <div class="col-sm-12 clear">
                <div class="table-responsive">
                    <asp:Label ID="lblMsg" CssClass="label label-success" runat="server"></asp:Label>
                    <asp:Repeater ID="RptTickets" runat="server" OnItemDataBound="RptTickets_ItemDataBound">
                        <HeaderTemplate>
                            <table class="table table-hover  table-bordered  table-condensed" data-height="400">
                                <thead style="color: White; background-color: #6b9297;">
                                    <tr>
                                        <th>ServiceName</th>
                                        <th>Category</th>
                                        <th>Type</th>
                                        <th>Plan</th>
                                        <th>Description</th>
                                    </tr>
                                </thead>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tbody>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblService" runat="server" Text='<%#Eval("ServiceName") %>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblCategory" runat="server" Text='<%#Eval("Category") %>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblServiceType" runat="server" Text='<%#Eval("ServiceType") %>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblPlans" runat="server" Text='<%#Eval("Plans") %>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblQuery" runat="server" Text='<%#Eval("description") %>'></asp:Label>
                                        
                                <asp:LinkButton ID="lnkBtnMore" ClientIDMode="Static" runat="server">....read more</asp:LinkButton>
                                 <asp:HiddenField ID="hdnFullMsg" ClientIDMode="Static" runat="server" Value='<%#Eval("description") %>' />
                                    </td>
                                </tr>
                            </tbody>
                        </ItemTemplate>
                        <FooterTemplate>
                            </table>
                        </FooterTemplate>
                    </asp:Repeater>
                </div>
            </div>
            <div>
                <div class="col-sm-6 text-left linkBottom">
                    <div class="input-group input-group-sm">
                        <span class="input-group-addon" id="Span9">Status</span>
                        <asp:DropDownList ID="ddlStatus" CssClass="form-control" runat="server">
                            <asp:ListItem Text="--Select Status--" Value="0"></asp:ListItem>

                            <asp:ListItem Text="Accept" Value="5"></asp:ListItem>
                            <asp:ListItem Text="Reject" Value="2"></asp:ListItem>
                            <asp:ListItem Text="Completed" Value="3"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" InitialValue="0" runat="server" ControlToValidate="ddlStatus"
                        ErrorMessage="Please Select Status" ValidationGroup="ticket" Display="Dynamic" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>

                </div>

                <div class="col-sm-6 text-left linkBottom">
                    <div class="input-group input-group-sm">
                        <span class="input-group-addon" id="Span1">Reason</span>
                        <asp:TextBox ID="txtReason" MaxLength="490" TextMode="MultiLine" CssClass="form-control input" runat="server"></asp:TextBox>
                    </div>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7"
                        runat="server" ValidationGroup="ticket" Display="Dynamic" ForeColor="Red"
                        ControlToValidate="txtReason" SetFocusOnError="true"
                        ErrorMessage="Enter Reason"></asp:RequiredFieldValidator>
                </div>
            </div>
        </div>
        <div class="panel-footer">
            <asp:Button ID="btnSubmit" ValidationGroup="ticket" OnClick="btnSubmit_Click" runat="server" Text="Submit" CssClass="btn-xs btn-success" />
            <asp:Button ID="btncancel" CssClass="btn-xs btn-success" runat="server" Text="Go To Tickets" OnClick="btncancel_Click" />
        </div>
    </div>
</asp:Content>
