<%@ Page Title="" Language="C#" MasterPageFile="AdminMaster.Master" AutoEventWireup="true" CodeBehind="ViewAppointments.aspx.cs" Inherits="MakeNMake.Excalation.ViewAppointments" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="panel panel-success">
        <div class="panel-heading">
            <h3 class="panel-title paneltitle">Search</h3>
        </div>
        <br />
        <div class="input-group input-group-xs table-responsive">
            <%-- <span class="input-group-addon" id="Span1"></span>--%>

            <asp:TextBox ID="txtSearchclient" Width="500px" style="margin-left:20px;"
                CssClass="form-control" runat="server" PlaceHolder="Search by Customer Name,AppointmentId,Status or Engineer Name"></asp:TextBox>
            <asp:Button ID="btnSubmit" style="margin-left:10px;" runat="server" Text="Search" CssClass="btn btn-success" ValidationGroup="user" OnClick="btnSubmit1_Click" />

        </div>
        <br />
        <div class="panel-heading">
            <h3 class="panel-title paneltitle">All Appointments </h3>
        </div>
        <div class="panel-body">
            <div class="panel-heading table-responsive" id="divClientList" runat="server" style="padding-left: 0px;">

                <asp:Repeater ID="RptTickets" runat="server" OnItemCommand="RptTickets_ItemCommand">
                    <HeaderTemplate>
                        <table class="table table-hover  table-bordered  table-condensed" data-height="400">
                            <thead style="color: White; background-color: #6b9297;">
                                <tr>
                                    <th>AppointmentID</th>
                                    <th>Customer Name</th>
                                    <th>Engineer Name</th>
                                    <th>Appointmentdate</th>
                                    <th>Appointmenttime</th>
                                    <th>Status</th>



                                </tr>
                            </thead>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tbody>
                            <tr>
                                <td>
                                    <asp:LinkButton ID="lblAppointmentID" CommandName="AppointmentID" Text='<%#Eval("AppointmentID")%>' CommandArgument='<%#Eval("AppointmentID") %>' runat="server"></asp:LinkButton>



                                </td>

                                <td>
                                    <asp:Label ID="lblCustomerName" runat="server" Text='<%#Eval("CustomerName") %>'></asp:Label>
                                </td>
                                <%--<td>
                                <asp:Label ID="lblEngineerID" runat="server" Text='<%#Eval("EngineerID") %>'></asp:Label>
                            </td>--%>
                                <td>
                                    <asp:Label ID="lblEngineerName" runat="server" Text='<%#Eval("EngineerName") %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblAppointmentdate" runat="server" Text='<%#Eval("Appointmentdate") %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:HiddenField ID="hdnEngineerID" runat="server" Value='<%#Eval("EngineerID") %>' />
                                    <asp:Label ID="lblAppointmenttime" runat="server" Text='<%#Eval("Appointmenttime") %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblStatus" runat="server" Text='<%#Eval("Status") %>'></asp:Label>
                                </td>
                                <%-- <td>
                                                <asp:Button ID="btnchkHistory" runat="server" CommandName="checkhistory" CommandArgument='<%#Eval("AppointmentID") %>' Text="Check History" CssClass="btn-xs btn-success" />
                                            </td>--%>
                            </tr>
                        </tbody>
                    </ItemTemplate>
                </asp:Repeater>
                <table id="Table1" runat="server" class="tblpaging" style="font-size: 12px; clear: both; margin-top: 15px;">
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
