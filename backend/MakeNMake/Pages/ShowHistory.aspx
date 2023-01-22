<%@ Page Title="Show History" Language="C#" MasterPageFile="AdminMaster.Master" AutoEventWireup="true" CodeBehind="ShowHistory.aspx.cs" Inherits="MakeNMake.Admin.ShowHistory" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript">

        function checkDate(sender, args) {
            var selectedDate = sender._selectedDate;

            var todayDate = new Date();
            if (selectedDate > todayDate) {
                alert("You cannot select a future date!");
            }
            //else if (todayDate.getFullYear() - selectedDate.getFullYear() < 18) {

            //    alert("You must be 18 year older to use this application");
            //}
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="panel panel-success">
        <div class="panel-heading">
            <h3 class="panel-title paneltitle">Ticket History</h3>
        </div>
        <div class="panel-body" style="padding-left:0px;">
            <div class="col-sm-12" style="padding-left:0px;">
                <div class="col-sm-6 text-left linkBottom">
                    <div class="input-group input-group-sm">
                        <span class="input-group-addon" id="Span8">Enter Date</span>
                        <asp:TextBox ID="txtdate" AutoComplete="off" ClientIDMode="Static"
                            placeholder="Enter Date" CssClass="form-control" aria-describedby="basic-addon1" runat="server"></asp:TextBox>
                    </div>
                    <asp:CalendarExtender ID="CalendarExtender1" TargetControlID="txtdate" OnClientDateSelectionChanged="checkDate" Format="MM/dd/yyyy" SelectedDate='<%#DateTime.Now %>' runat="server"></asp:CalendarExtender>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7"
                        runat="server" ValidationGroup="submit" Display="Dynamic" ForeColor="Red"
                        ControlToValidate="txtdate" SetFocusOnError="true" ErrorMessage="Enter Date"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" SetFocusOnError="true" runat="server" ControlToValidate="txtdate"
                        ErrorMessage="Enter Valid date" Display="Dynamic" ForeColor="Red" ValidationGroup="submit" ValidationExpression="^(0[1-9]|1[012])[- /.](0[1-9]|[12][0-9]|3[01])[- /.](19|20)\d\d+$"></asp:RegularExpressionValidator>

                </div>
                <div class="col-sm-6 text-left linkBottom">
                    <div class="input-group input-group-sm">
                        <span class="input-group-addon" id="Span4">Select Engineer</span>
                        <asp:DropDownList ID="ddlEngineer" CssClass="form-control dropdown" AutoPostBack="true" runat="server">
                        </asp:DropDownList>
                    </div>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ValidationGroup="submit" runat="server" ErrorMessage="Select Country"
                        ControlToValidate="ddlEngineer"
                        InitialValue="0" Display="Dynamic" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                </div>

            </div>
            <div class="col-sm-12 text-left linkBottom" style="margin-top: 20px;">
                <asp:Button ID="btnSubmit" runat="server" Text="Add" OnClick="btnSubmit_Click" CssClass="btn btn-success" ValidationGroup="submit" />
                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-success" OnClick="btnCancel_Click" />
            </div>
        </div>

    </div>

    <div class="panel panel-success" id="divrptdistrict" runat="server" visible="false">
        <div class="panel-heading">
            <h3 class="panel-title paneltitle">Show History
                <asp:Label ID="lblhdcity_name" runat="server" /></h3>
        </div>
        <div class="panel-body">
            <asp:Repeater ID="RptShow_History" runat="server">
                <HeaderTemplate>
                    <table class="table table-hover  table-bordered  table-condensed" data-height="400">
                        <thead style="color: White; background-color: #6b9297;">
                            <tr>
                                <th>Ticket Id</th>
                                <th>ClientName</th>
                                <th>ServiceName</th>
                                <th>Service Date</th>
                                <th>Service From </th>
                                <th>Service To</th>
                                <th>Work Description CheckIn</th>
                                <th>Work Description CheckOut</th>
                            </tr>
                        </thead>
                </HeaderTemplate>
                <ItemTemplate>
                    <tbody>
                        <tr>
                            <td>
                                <asp:Label ID="lblC_name" runat="server" Text='<%#Eval("TicketID") %>'></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="Label1" runat="server" Text='<%#Eval("name") %>'></asp:Label></td>
                            <td>
                                <asp:Label ID="Label2" runat="server" Text='<%#Eval("servicename") %>'></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="Label3" runat="server" Text='<%#Eval("Servicedate") %>'></asp:Label></td>
                            <td>
                                <asp:Label ID="Label4" runat="server" Text='<%#Eval("ServiceFrom") %>'></asp:Label></td>
                            <td>
                                <asp:Label ID="Label5" runat="server" Text='<%#Eval("ServiceTo") %>'></asp:Label></td>
                            <td>
                                <asp:Label ID="Label6" runat="server" Text='<%#Eval("WorkDescCheckIn") %>'></asp:Label></td>
                            <td>
                                <asp:Label ID="Label7" runat="server" Text='<%#Eval("WorkDescCheckOut") %>'></asp:Label></td>
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
</asp:Content>
