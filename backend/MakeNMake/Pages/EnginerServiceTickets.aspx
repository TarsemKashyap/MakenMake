<%@ Page Title="Engineer Service Tickets" Language="C#" MasterPageFile="~/Pages/AdminMaster.Master" AutoEventWireup="true" CodeBehind="EnginerServiceTickets.aspx.cs" Inherits="MakeNMake.Pages.EnginerServiceTickets" %>
<%@ Register Src="~/UserControl/UserInfo.ascx" TagPrefix="uc1" TagName="UserInfo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script src="../Scripts/jquery-2.0.1.min.js"></script>
    <link href="../Static/bootstrap/bootstrap.css" rel="stylesheet" />
    <link href="../Datatable/jquery.dataTables.css" rel="stylesheet" />
    <script src="../Static/bootstrap/bootstrap.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            if ($("#hdnCustomer").val() == "1") {
                $('#myModal').modal({ show: true });
                $("#hdnCustomer").val("");

                $(".modal-backdrop").hide();
            }
            $("#btnSearch").click(function (e) {
                var val = $("#ddlSearch").val();
                if (val == "0") {
                    var txtval = $("#txtIDName").val();
                    if (txtval == "") {
                        alert("Please enter Ticket ID");
                        e.preventDefault();
                    }
                }
                else if (val == "1") {
                    var txtval = $("#txtIDName").val();
                    if (txtval == "") {
                        alert("Please enter Customer Name");
                        e.preventDefault();
                    }
                }
            });
        });

        function ShowMsg(ele) {
            alert(ele.nextElementSibling.value);
            return false;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">



    <div class="panel panel-success ">
        <div class="panel-heading">
            <h3 class="panel-title paneltitle">Service Ticket</h3>
        </div>
        <div class="panel-body" style="padding: 0px;">

            <div class=" col-sm-12 linkBottom" style="padding-left: 0px;padding-top:15px;padding-bottom:30px;background-color:white;">
                <div class="col-sm-4 text-left linkBottom">
                    <div class="input-group input-group-sm">
                        <span class="input-group-addon" id="Span7">Search Ticket By</span>
                        <asp:DropDownList ID="ddlSearch" ClientIDMode="Static" AutoPostBack="true" OnSelectedIndexChanged="ddlSearch_SelectedIndexChanged" CssClass="form-control dropdown" runat="server">
                            <asp:ListItem Text="--Select--" Value="-1"></asp:ListItem>
                            <asp:ListItem Text="Ticket ID" Value="0"></asp:ListItem>
                            <asp:ListItem Text="Customer Name" Value="1"></asp:ListItem>
                            <asp:ListItem Text="Ticket Type" Value="2"></asp:ListItem>
                        </asp:DropDownList>
                    </div>

                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ValidationGroup="tickets" runat="server"
                        ErrorMessage="Select Search Type"
                        ControlToValidate="ddlSearch"
                        InitialValue="-1" Display="Dynamic" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                </div>
                <div id="dvTicketIDName" runat="server" visible="false" class="col-sm-4 text-left linkBottom">
                    <asp:TextBox ID="txtIDName" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:TextBox>

                </div>
                <div id="ddltype" runat="server" visible="false" class="col-sm-4 text-left linkBottom">
                    <asp:DropDownList ID="ddlTicketType" CssClass="form-control dropdown" runat="server">
                        <asp:ListItem Text="Insepection" Value="1"></asp:ListItem>
                        <asp:ListItem Text="Repair" Value="2"></asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div id="DvSubmit" runat="server" visible="false" class="col-sm-4 text-left linkBottom">
                    <asp:Button ID="btnSearch" ValidationGroup="tickets" ClientIDMode="Static"
                        CssClass="btn btn-success" OnClick="btnSearch_Click" runat="server" Text="Search" />
                </div>


                <div class="col-sm-12 text-left linkBottom" style="padding: 0px; background-color:white;">
                    <div class="table-responsive">
                    <asp:HiddenField ID="hdnCustomer" ClientIDMode="Static" runat="server" />
                    <asp:Label ID="lblMsg" CssClass="label label-danger" runat="server"></asp:Label>
                    <br />
                    <asp:Repeater ID="RptTickets" runat="server" OnItemCommand="RptTickets_ItemCommand" OnItemDataBound="RptTickets_ItemDataBound">
                        <HeaderTemplate>
                            <table class="table table-hover  table-bordered  table-condensed" style="background-color:white;" data-height="400">
                                <thead style="color: White; background-color: #6b9297;">
                                    <tr>
                                        <th>TicketID</th>
                                        <th>TicketType</th>
                                        <th>Customer Name</th>
                                        <th>Description</th>
                                        <th>Created</th>
                                        <th>Status</th>


                                    </tr>
                                </thead>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tbody>
                                <tr>
                                    <td>
                                       <asp:Label ID="lblTicketID" runat="server" Visible="false" Text='<%# Eval("TicketID")%>'></asp:Label>
                                        <asp:LinkButton ID="LinkButton1" Text='<%#Eval("TicketID") %>' ToolTip="Click to view details"
                                            CommandName="detail" CommandArgument='<%#Eval("TicketID") %>' runat="server"></asp:LinkButton>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblTicketType" runat="server" Text='<%#Eval("TicketType") %>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:LinkButton ID="lnkBtnClient" Text='<%#Eval("Name") %>' ToolTip="Click to view Info"
                                            CommandName="Customer" CommandArgument='<%#Eval("CustomerId") %>' runat="server"></asp:LinkButton></td>
                                    <td>
                                        <asp:Label ID="lblDescription" runat="server" Text='<%#Eval("Description") %>'></asp:Label>
                                        <asp:LinkButton ID="lnkBtnMore" ClientIDMode="Static" runat="server">....read more</asp:LinkButton>
                                 <asp:HiddenField ID="hdnFullMsg" ClientIDMode="Static" runat="server" Value='<%#Convert.ToString(Eval("Description"))%>' />

                                    </td>
                                    <td>
                                        <asp:Label ID="lblCreated" runat="server" Text='<%#Convert.ToDateTime(Eval("created")).ToString("dd/MM/yyyy")%>'></asp:Label></td>
                                    <td>
                                        <asp:HiddenField ID="hdnStat" runat="server" Value='<%# Eval("SStatus")%>' />
                                        <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("Status")%>'></asp:Label></td>
                            </tbody>
                        </ItemTemplate>
                        <FooterTemplate>
                            </table>
                        </FooterTemplate>
                    </asp:Repeater>
                    <table class="tblpaging" id="tblPaging" runat="server" style="font-size: 12px; clear: both; margin-top: 15px;">
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
        </div>
    </div>
    <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-hidden="true">
        <uc1:UserInfo runat="server" ID="UserInfo" />
    </div>

</asp:Content>
