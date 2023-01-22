<%@ Page Title="Quotation History" Language="C#" MasterPageFile="~/Pages/AdminMaster.Master" AutoEventWireup="true" CodeBehind="QutationHistroy.aspx.cs" Inherits="MakeNMake.Pages.QutationHistroy" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .tddisplay {
            display: none;
            border-top-style: none;
            border-top-width: 0px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="panel panel-success">
        <div class="panel-heading">
            <h3 class="panel-title paneltitle">Quotation History</h3>
        </div>
        <div class="panel-body table-responsive" >
            <asp:Repeater ID="rpQuotation" runat="server" OnItemCommand="rpQuotation_ItemCommand" OnItemDataBound="rpQuotation_ItemDataBound">
                <HeaderTemplate>
                    <table class="table table-hover  table-bordered  table-condensed" data-height="400">
                        <thead style="color: White; background-color: #6b9297;">
                            <tr>
                                <th>Consumer Name</th>
                                <th>Opt for Services</th>
                                <th>Quotation Title</th>
                                <th>Price</th>
                                <th>Created</th>
                                <th>Create Contract</th>

                            </tr>
                        </thead>
                </HeaderTemplate>
                <ItemTemplate>
                    <tbody>
                        <tr>
                            <td>
                                <asp:HiddenField ID="hdQuotationId" runat="server" Value='<%#Eval("QuotationID") %>' />
                                <asp:HiddenField ID="hdRequestId" runat="server" Value='<%#Eval("RequestID") %>' />
                                <asp:HiddenField ID="hdUserId" runat="server" Value='<%#Eval("Userid") %>' />
                                <asp:Label ID="lbusername" runat="server" Text='<%#Eval("Name") %>'></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lbServiceName" runat="server" Text='<%#Eval("ServiceName") %>'></asp:Label>
                            </td>
                           
                            <td>
                                <asp:Label ID="lbQuotationTitle" runat="server" Text='<%#Eval("QuotationTitle") %>'></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lbPrice" runat="server" class='<%#Eval("Price").ToString()!=""?"" : "tddisplay"%>' Text='<%#Eval("Price") %>'></asp:Label>
                            </td>
                          
                            <td>
                                <asp:Label ID="lbCreated" runat="server" Text='<%#Eval("Created") %>'></asp:Label>
                            </td>
                            <td>
                                    
                                <asp:LinkButton ID="btnCreateQuotation" CssClass='<%#Convert.ToString(Eval("RequestID"))!=""?"" : "tddisplay"%>' runat="server" CommandArgument='<%#Eval("RequestID") %>' CommandName="Create Contract">Create Contract </asp:LinkButton>

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
</asp:Content>
