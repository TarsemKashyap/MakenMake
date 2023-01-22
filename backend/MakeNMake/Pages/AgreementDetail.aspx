<%@ Page Title="Agreement Detail" Language="C#" MasterPageFile="AdminMaster.Master" AutoEventWireup="true" CodeBehind="AgreementDetail.aspx.cs" Inherits="MakeNMake.Customer.AgreementDetail" %>

<%@ Register Src="~/UserControl/UserInfo.ascx" TagPrefix="uc1" TagName="UserInfo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../Scripts/jquery-2.0.1.min.js"></script>
    <link href="../Static/bootstrap/bootstrap.css" rel="stylesheet" />
    <script src="../Static/bootstrap/bootstrap.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            if ($("#hdnCustomer").val() == "1") {
                $('#myModal').modal({ show: true });
                $("#hdnCustomer").val("");
            }
        });
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
                <asp:Repeater ID="Rptagreement" runat="server" OnItemCommand="Rptagreement_ItemCommand" OnItemDataBound="Rptagreement_ItemDataBound">
                    <HeaderTemplate>
                        <table class="table table-hover  table-bordered  table-condensed" data-height="400">
                            <thead style="color: White; background-color: #6b9297;">
                                <tr>
                                    <th>Category</th>
                                    <th>Plan Type</th>
                                    <th>Service Plan </th>
                                    <th>Service Name</th>
                                    <th id="unlimitedarea" runat="server">Area (Sqft)</th>
                                    <th id="unlimitedcategory" runat="server">Category</th>
                                    <th>No Of Calls</th>
                                    <th>Amount</th>
                                </tr>
                            </thead>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tbody>
                            <tr>
                                <td>
                                    <asp:Label ID="lblcategory" runat="server" Text='<%#Eval("Category") %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lbltype" runat="server" Text='<%#Eval("Type") %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblplan" runat="server" Text='<%#Eval("ServicePlan") %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblname" runat="server" Text='<%#Eval("ServiceName") %>'></asp:Label>
                                </td>
                                <td id="tdArea" runat="server">
                                    <asp:Label ID="lblArea" runat="server" Text='<%# Eval("Area")%>'></asp:Label></td>
                                <td id="tdCategory" runat="server">
                                    <asp:Label ID="lblUCategory" runat="server" Text='<%# Eval("UCategory")%>'></asp:Label></td>
                                <td>
                                        <asp:Label ID="lblcalls" runat="server" Text='<%#Eval("NoFCalls") %>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblamt" runat="server" Text='<%#Eval("Amount") %>'></asp:Label>
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
    </div>
    <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-hidden="true">
        <uc1:UserInfo runat="server" ID="UserInfo" />
    </div>
</asp:Content>
