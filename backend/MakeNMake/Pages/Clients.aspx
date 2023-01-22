<%@ Page Title="Customers" Language="C#" MasterPageFile="AdminMaster.Master" AutoEventWireup="true" CodeBehind="Clients.aspx.cs" Inherits="MakeNMake.CustomerCare.Clients" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../Scripts/jquery-2.0.1.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $(window).keydown(function (event) {
                if (event.keyCode == 13) {
                    event.preventDefault();
                    return false;
                }
            });
        });

        function showlatrno(altrnos) {
            var list = "";
            for (i = 0; i < altrnos.Length; i++)
            {
                list = list + " " + altrnos[i];
            }
            alert(list);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="panel panel-success table-responsive">
        <div class="panel-heading">
            <h3 class="panel-title paneltitle">Search Customer</h3>
        </div>
        <br />
        <div class="input-group input-group-xs" style="padding-left: 10px;">


            <asp:TextBox ID="txtSearchclient" placeholder="Search Customer By Name,EmailID Or Mobile Number" Width="400px"
                CssClass="form-control" runat="server"></asp:TextBox>
            <asp:Button ID="btnSubmit" runat="server" Text="Search" OnClick="btnSubmit_OnClick"
                CssClass="btn btn-success" ValidationGroup="user" />
        </div>
        <br />

        <div class="panel-heading" id="divClientList" runat="server">
            <h3 class="panel-title paneltitle">Customer List</h3>
        </div>

        <div class="table-responsive" style="padding-left: 10px;">
            <asp:Repeater ID="RptClient" runat="server" OnItemCommand="RptClient_ItemCommand" OnItemDataBound="RptClient_ItemDataBound">
                <HeaderTemplate>
                    <table class="table table-hover  table-bordered  table-condensed" data-height="400">
                        <thead style="color: White; background-color: #6b9297;">
                            <tr>
                                <th>Name</th>
                                <th>Email</th>
                                <th>Mobile</th>
                               
                                <th>City</th>
                                <th>Created</th>
                                <th></th>
                                <th></th>
                                <th></th>
                                <th></th>
                            </tr>
                        </thead>
                </HeaderTemplate>
                <ItemTemplate>
                    <tbody>
                        <tr>
                            <td>
                                <asp:Label ID="lblName" runat="server" Text='<%#Eval("NAME") %>'></asp:Label></td>
                            <td>
                                <asp:Label ID="lblEmail" runat="server" Text='<%#Eval("EmailID") %>'></asp:Label></td>
                            <td>
                                <asp:LinkButton ID="lblMobile" runat="server" Text='<%#Eval("MobileNumber") %>'></asp:LinkButton>
                                  <asp:HiddenField ID="hdnID" runat="server" Value='<%#Eval("UserID") %>' />
                            </td>
                            
                            <td>
                                <asp:Label ID="lblCity" runat="server" Text='<%#Eval("CityName") %>'></asp:Label></td>
                            <td>
                                <asp:Label ID="lblCreated" runat="server" Text='<%#Convert.ToDateTime( Eval("Created") ).ToString("D")%>'></asp:Label></td>
                            <td>
                                <asp:Button ID="btnCreatePackage" runat="server" CommandName="package" CommandArgument='<%#Eval("UserID") %>' Text="Package Selection" CssClass="btn-xs btn-success" />
                            </td>
                            <td>
                                <asp:Button ID="btnUpdateInfo" runat="server" CommandName="updateInfo" CommandArgument='<%#Eval("UserID") %>' Text="Update Info" CssClass="btn-xs btn-success" />
                            </td>
                            <td>
                                <asp:Button ID="btnBookComplaint" runat="server" CommandName="complaint" CommandArgument='<%#Eval("UserID") %>' Text="Open Ticket" CssClass="btn-xs btn-success" />
                            </td>
                            <td>
                                <asp:Button ID="btnVisitRequest" runat="server" Visible='<%#Convert.ToString(Eval("QuoteContract"))=="1"?true:false %>' CommandName="contract" CommandArgument='<%#Eval("UserID") %>' Text="Create Contract" CssClass="btn-xs btn-success" />
                            </td>
                    </tbody>
                </ItemTemplate>
                <FooterTemplate>
                    </table>
                </FooterTemplate>
            </asp:Repeater>
            <table id="tblPaging" runat="server" class="tblpaging" style="font-size: 12px; clear: both; margin-top: 15px;">
                <tr>
                    <td colspan="5"></td>
                </tr>
                <tr>
                    <td width="32" valign="top" align="center">
                        <asp:LinkButton ID="lnkFirst" runat="server" OnClick="lnkFirst_Click">First</asp:LinkButton>
                    </td>
                    <td width="80" valign="top" align="center">
                        <asp:LinkButton ID="lnkPrevious" runat="server" OnClick="lnkPrevious_Click">Previous</asp:LinkButton>
                    </td>
                    <td>
                        <asp:DataList ID="RepeaterPaging" runat="server" RepeatDirection="Horizontal" OnItemCommand="RepeaterPaging_ItemCommand"
                            OnItemDataBound="RepeaterPaging_ItemDataBound">
                            <ItemTemplate>
                                <asp:LinkButton ID="Pagingbtn" runat="server" CommandArgument='<%# Eval("PageIndex") %>'
                                    CommandName="newpage" Text='<%# Eval("PageText") %> ' Width="20px"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:DataList>
                    </td>
                    <td width="80" valign="top" align="center">
                        <asp:LinkButton ID="lnkNext" runat="server" OnClick="lnkNext_Click">Next</asp:LinkButton>
                    </td>
                    <td width="80" valign="top" align="center">
                        <asp:LinkButton ID="lnkLast" runat="server" OnClick="lnkLast_Click">Last</asp:LinkButton>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="left" height="30">
                        <asp:Label Style="padding-left: 4px;" ID="lblpage" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
