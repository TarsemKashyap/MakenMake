<%@ Page Title="Apointment Tickets" Language="C#" MasterPageFile="AdminMaster.Master" AutoEventWireup="true" CodeBehind="AppoinmentTickets.aspx.cs" Inherits="MakeNMake.ServiceEngineer.AppoinmentTickets" %>

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
                $(".modal-backdrop").hide();
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
            <h3 class="panel-title paneltitle">Appointment Status</h3>
        </div>
        <%-- <div class="panel-body">
            <div class="col-sm-12 clear">
                <div class="table-responsive">
                    <asp:HiddenField ID="hdnCustomer" ClientIDMode="Static" runat="server" />
                    <asp:Label ID="lblMsg" CssClass="label label-success" runat="server"></asp:Label>
                    <asp:Repeater ID="RptAppointment" runat="server" OnItemCommand="RptAppointment_ItemCommand">
                        <HeaderTemplate>
                            <table class="table table-hover  table-bordered  table-condensed" data-height="400">
                                <thead style="color: White; background-color: #6b9297;">
                                    <tr>
                                        <th>Appoinment ID</th>
                                        <th>Customer</th>
                                        <th>Booked By</th>
                                        <th>Appointment Date</th>
                                        <th>Appointment Time</th>
                                        <th>Status</th>
                                        <th>Reason</th>
                                        <th></th>
                                    </tr>
                                </thead>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tbody>
                                <tr> <td>

                                    <asp:LinkButton ID="LinkButtonApp_ID" Text='<%#Eval("AppointmentID") %>'
                                            CommandName="statusChange" CommandArgument='<%#Eval("AppointmentID")+","+ Eval("Status") %>' runat="server"></asp:LinkButton>
                                     
                                    <td>
                                        <asp:LinkButton ID="lnkBtnClient" Text='<%#Eval("Name") %>' ToolTip="Click to view Info"
                                            CommandName="Customer" CommandArgument='<%#Eval("CustomerId") %>' runat="server"></asp:LinkButton></td>
                                    <td>
                                        <asp:Label ID="lblCustomerCare" runat="server" Text='<%#Eval("CustomerCare") %>'></asp:Label></td>
                                    <td>
                                        <asp:Label ID="lblAppointmentdate" runat="server" Text='<%#Convert.ToDateTime(Eval("Appointmentdate")).ToString("dd/MM/yyyy") %>'></asp:Label></td>
                                    <td>
                                        <asp:Label ID="lblAppointmenttime" runat="server" Text='<%#Eval("Appointmenttime")%>'></asp:Label></td>
                                    <td>
                                        <asp:Label ID="lblStatus" runat="server"  Text='<%# Eval("Status")%>'></asp:Label></td>
                                     <td>
                                        <asp:Label ID="lblReason" runat="server"  Text='<%# Eval("Reason")%>'></asp:Label></td>
                                    <td>
                                        <asp:Button ID="btnReject" CommandName="statusChange" 
                                          CssClass="btn-xs btn-success" 
                                            CommandArgument='<%#Eval("AppointmentID")+","+ Eval("Status") %>'
                                            runat="server" Text="Change Status" /></td>
                            </tbody>
                        </ItemTemplate>
                        <FooterTemplate>
                            </table>
                        </FooterTemplate>
                    </asp:Repeater>
                </div>
            </div>
        </div>--%>

        <div class="panel-body">

            <asp:HiddenField ID="hdnCustomer" ClientIDMode="Static" runat="server" />
            <asp:Label ID="lblMsg" CssClass="label label-success" runat="server"></asp:Label>
            <asp:Repeater ID="RptAppointment" runat="server" OnItemCommand="RptAppointment_ItemCommand" OnItemDataBound="RptAppointment_ItemDataBound">
                <HeaderTemplate>
                    <table class="table table-hover  table-bordered  table-condensed" data-height="400">
                        <thead style="color: White; background-color: #6b9297;">
                            <tr>
                                <th>Appoinment ID</th>
                                <th>Customer</th>
                                <th>Booked By</th>
                                <th>Appointment Date</th>
                                <th>Appointment Time</th>
                                <th>Status</th>
                                <th>Reason</th>
                            </tr>
                        </thead>
                </HeaderTemplate>
                <ItemTemplate>
                    <tbody>
                        <tr>
                            <td>

                                <asp:LinkButton ID="LinkButtonApp_ID" Text='<%#Eval("AppointmentID") %>'
                                    CommandName="statusChange" CommandArgument='<%#Eval("AppointmentID")+","+ Eval("Status") %>' runat="server"></asp:LinkButton>

                                <td>
                                    <asp:LinkButton ID="lnkBtnClient" Text='<%#Eval("Name") %>' ToolTip="Click to view Info"
                                        CommandName="Customer" CommandArgument='<%#Eval("CustomerId") %>' runat="server"></asp:LinkButton></td>
                                <td>
                                    <asp:Label ID="lblCustomerCare" runat="server" Text='<%#Eval("CustomerCare") %>'></asp:Label></td>
                                <td>
                                    <asp:Label ID="lblAppointmentdate" runat="server" Text='<%#Convert.ToDateTime(Eval("Appointmentdate")).ToString("dd/MM/yyyy") %>'></asp:Label></td>
                                <td>
                                    <asp:Label ID="lblAppointmenttime" runat="server" Text='<%#Eval("Appointmenttime")%>'></asp:Label></td>
                                <td>
                                    <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("Status")%>'></asp:Label></td>
                                <td>
                                    <asp:Label ID="lblReason" runat="server" Text='<%# Eval("Reason")%>'></asp:Label>
                                    <asp:LinkButton ID="lnkBtnMore" ClientIDMode="Static" runat="server">....read more</asp:LinkButton>
                                    <asp:HiddenField ID="hdnFullMsg" ClientIDMode="Static" runat="server" Value='<%#Convert.ToString(Eval("Reason"))%>' />

                                </td>
                                <%-- <td>
                                        <asp:Button ID="btnReject" CommandName="statusChange" 
                                          CssClass="btn-xs btn-success" 
                                            CommandArgument='<%#Eval("AppointmentID")+","+ Eval("Status") %>'
                                            runat="server" Text="Change Status" /></td>--%>
                    </tbody>
                </ItemTemplate>
                <FooterTemplate>
                    </table>
                </FooterTemplate>
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
    <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-hidden="true">
        <uc1:UserInfo runat="server" ID="UserInfo" />
    </div>
</asp:Content>
