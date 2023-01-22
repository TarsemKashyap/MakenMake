<%@ Page Title="Show Customer Feedback" Language="C#" MasterPageFile="AdminMaster.Master" AutoEventWireup="true" CodeBehind="ShowCustomerFeedback.aspx.cs" Inherits="MakeNMake.Admin.ShowCustomerFeedback" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script src="../Scripts/jquery-2.0.1.min.js"></script>
    <link href="../Static/bootstrap/bootstrap.css" rel="stylesheet" />
    <script src="../Static/bootstrap/bootstrap.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            if ($("#hdnCustomer").val() != "") {
                $('#imagemodal').modal({ show: true });
                $('#imagepreview').attr('src', $("#hdnCustomer").val());
                $("#hdnCustomer").val("");
            }
        });


        function ShowMsg(ele) {
            alert(ele.nextElementSibling.value);
            return false;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">



    <div class="panel panel-success">
        <div class="panel-heading">
            <h3 class="panel-title paneltitle">Customers Feedback</h3>
        </div>
        <div class="panel-body">
            <div class="panel-heading table-responsive" id="divClientList" runat="server" style="padding-left: 0px;">

                <asp:Repeater ID="RptTickets" runat="server" OnItemDataBound="RptTickets_ItemDataBound">
                    <HeaderTemplate>
                        <table class="table table-hover  table-bordered  table-condensed" data-height="400">
                            <thead style="color: White; background-color: #6b9297;">
                                <tr>
                                    <th>FeedBackID</th>
                                    <th>RelatedTicketID</th>
                                    <th>Engineer Name</th>
                                    <th>Customer Name</th>
                                    <th>Customer EmailID</th>
                                    <th>Satisfied</th>
                                    <th>Rating</th>

                                    <th>Description</th>
                                    <th>Created</th>

                                </tr>
                            </thead>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tbody>
                            <tr>
                                <td>
                                    <asp:Label ID="lblFeedBackID" runat="server" Text='<%#Eval("FeedBackID") %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblRelatedTicketID" runat="server" Text='<%#Eval("RelatedTicketID") %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblEngName" runat="server" Text='<%#Eval("EngineerName") %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblCustName" runat="server" Text='<%#Eval("CustomerName") %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblCustEmail" runat="server" Text='<%#Eval("CustomerEmail") %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblSatisfied" runat="server" Text='<%#Eval("Satisfied") %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblRating" runat="server" Text='<%#Eval("Rating") %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblFeedbackDesc" runat="server" Text='<%#Eval("FeedbackDesc") %>'></asp:Label>                                    
                                <asp:LinkButton ID="lnkBtnMore" ClientIDMode="Static" runat="server">....read more</asp:LinkButton>
                                 <asp:HiddenField ID="hdnFullMsg" ClientIDMode="Static" runat="server" Value='<%#Convert.ToString(Eval("FeedbackDesc"))%>' />
                                </td>
                                <td>
                                    <asp:Label ID="tblCreated" runat="server" Text='<%#Eval("Created") %>'></asp:Label>
                                </td>
                            </tr>
                        </tbody>
                    </ItemTemplate>
                </asp:Repeater>
                <table class="tblpaging" style="font-size: 12px; clear: both; margin-top: 15px;">
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
    </div>

</asp:Content>
