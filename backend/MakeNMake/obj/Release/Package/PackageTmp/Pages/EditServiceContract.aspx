<%@ Page Title="Edit Contract" Language="C#" MasterPageFile="~/Pages/AdminMaster.Master" AutoEventWireup="true" CodeBehind="EditServiceContract.aspx.cs" Inherits="MakeNMake.Pages.EditServiceContract" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="panel panel-success table-responsive">
        <div class="panel-heading">
            <h3 class="panel-title paneltitle">Edit Contract of Customer</h3>
        </div>
        <div class="panel-body">
            <div class="table-responsive" style="padding-left: 10px;">
                <asp:Repeater ID="RptClient" runat="server" OnItemCommand="RptClient_ItemCommand">
                    <HeaderTemplate>
                        <table class="table table-hover  table-bordered  table-condensed" data-height="400">
                            <thead style="color: White; background-color: #6b9297;">
                                <tr>
                                    <th>UserID</th>
                                    <th>Name</th>
                                    <th>Agreement Number</th>
                                    <th>Invoice Number</th>
                                    <th>Invoice Date</th>
                                    <th></th>
                                </tr>
                            </thead>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tbody>
                            <tr>
                                <td>
                                    <asp:Label ID="Label1" runat="server" Text='<%#Eval("UserID") %>'></asp:Label></td>
                                <td>
                                    <asp:Label ID="lblName" runat="server" Text='<%#Eval("NAME") %>'></asp:Label></td>
                                <td>
                                    <asp:Label ID="lblEmail" runat="server" Text='<%#Eval("AgreementNumber") %>'></asp:Label></td>
                                <td>
                                    <asp:Label ID="hdnInvoice" runat="server" Text='<%#Eval("InvoiceNumber") %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblCity" runat="server" Text='<%#Eval("invoicedate") %>'></asp:Label></td>
                                <td>
                                    <asp:Button ID="btnCreatePackage" runat="server" CommandName="editcontract" CommandArgument='<%# string.Format("{0}:{1}:{2}:{3}:{4}",Eval("UserID"),Eval("Agreementid"),Eval("AgreementNumber"),Eval("category"),Eval("serviceplan"))%>' Text="Edit Contract" CssClass="btn-xs btn-success" />
                                </td>
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
