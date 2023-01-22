<%@ Page Title="Appointments" Language="C#" MasterPageFile="AdminMaster.Master" AutoEventWireup="true" CodeBehind="Appointments.aspx.cs" Inherits="MakeNMake.Admin.Appointments" %>

<%@ Register Src="~/UserControl/UserInfo.ascx" TagPrefix="uc1" TagName="UserInfo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../Scripts/jquery-2.0.1.min.js"></script>
    <link href="../Static/bootstrap/bootstrap.min.css" rel="stylesheet" />
    <script src="../Static/bootstrap/bootstrap.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            if ($("#hdnCustomer").val() == "1") {
                $('#myModal').modal({ show: true });
                $("#hdnCustomer").val("");
            }
        });
        function PageReload() {
            var getValue = confirm('No Record Found ');
            if (getValue) {
                window.location.href = window.location.href;
            }
            else {
                window.location.href = window.location.href;
            }
        }
        function ShowMsg(ele) {
            alert(ele.nextElementSibling.value);
            return false;
         }

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="panel panel-success table-responsive">
        <div class="panel-heading">
            <h3 class="panel-title paneltitle">Appointment Status</h3>
        </div>
        <div class="input-group input-group-xs" style="padding-left:15px;">

            <br />
                        <asp:TextBox ID="txtSearchclient" placeholder="Search by Client Name,Engineer Name,Appointment ID and Staus" Width="400px"
                            CssClass="form-control" runat="server"></asp:TextBox>&nbsp;&nbsp;
                            <asp:Button ID="btnSubmit" runat="server" Text="Search" OnClick="btnSubmit_OnClick"
                                CssClass="btn btn-success" ValidationGroup="user" />
          </div>
        <br />
        <div class="panel-body" >
             <div class="panel-heading table-responsive" id="divClientList" runat="server" style="padding-left: 0px;">
            <asp:HiddenField ID="hdnCustomer" ClientIDMode="Static" runat="server" />
            <asp:Label ID="lblMsg" CssClass="label label-success" runat="server"></asp:Label>
            <asp:Repeater ID="RptAppointment" runat="server" OnItemCommand="RptAppointment_ItemCommand" OnItemDataBound="RptAppointment_ItemDataBound">
                <HeaderTemplate>
                    <table class="table table-hover  table-bordered  table-condensed" data-height="400">
                        <thead style="color: White; background-color: #6b9297;">
                            <tr>
                                <th>Ticket ID</th>
                                <th>Ticket Type</th>
                                <th>Customer</th>
                                <th>Assigned To</th>
                                <th>Created</th>
                                <th>Appointment Date</th>
                                <th>Appointment Time
                                    <br />
                                    <span style="font-size: 11px;">(24 hrs)</span></th>
                                <th>Status</th>
                                <th>Reason</th>
                            </tr>
                        </thead>
                </HeaderTemplate>
                <ItemTemplate>
                    <tbody>
                        <tr>
                             <td>
                                   <asp:LinkButton ID="lnkbtnID" Text='<%#Eval("AppointmentID") %>' 
                                    CommandName="changeStatus" CommandArgument='<%#Eval("AppointmentID") %>' runat="server"></asp:LinkButton>
                             </td>
                             <td>
                                <asp:Label ID="lblTicketType" runat="server" Text='<%#Eval("TicketType") %>'></asp:Label>
                             </td>
                          
                            <td>
                                <asp:LinkButton ID="lnkBtnClient" Text='<%#Eval("Name") %>' ToolTip="Click to view Info"
                                    CommandName="Customer" CommandArgument='<%#Eval("CustomerId") %>' runat="server"></asp:LinkButton></td>
                           
                             <td>
                                 <asp:HiddenField ID="EngineerID" runat="server" Value='<%#Eval("EngineerID") %>' />
                                <asp:Label ID="lbleng" runat="server" Text='<%#Eval("AssignedTo") %>'></asp:Label></td>
                            <td>
                                <asp:Label ID="Label1" runat="server" Text='<%#Convert.ToDateTime(Eval("Created")) %>'></asp:Label></td>
                            <td>
                                <asp:Label ID="lblAppointmentdate" runat="server" Text='<%#Convert.ToDateTime(Eval("Appointmentdate")).ToString("dd/MM/yyyy") %>'></asp:Label></td>
                            <td>
                                <asp:Label ID="lblAppointmenttime" runat="server" Text='<%#Eval("Appointmenttime")%>'></asp:Label></td>
                            <td>
                                <asp:HiddenField ID="hdnStatus" runat="server" Value='<%#Eval("Hstatus") %>' />
                                <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("Status")%>'></asp:Label></td>
                            <td >
                                <asp:Label ID="lblReason" runat="server" Text='<%# Eval("Reason")%>'></asp:Label>
                                <asp:LinkButton ID="lnkBtnMore" ClientIDMode="Static" runat="server">....read more</asp:LinkButton>
                                 <asp:HiddenField ID="hdnFullMsg" ClientIDMode="Static" runat="server" Value='<%#Eval("Reason") %>' />
                            </td>
                    </tbody>
                </ItemTemplate>
                <FooterTemplate>
                    </table>
                </FooterTemplate>
            </asp:Repeater>
            <table id="tblPaging" class="tblpaging" runat="server" style="font-size: 12px; clear: both; margin-top: 15px;">
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
    <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-hidden="true">
        <uc1:UserInfo runat="server" ID="UserInfo" />
    </div>
</asp:Content>
