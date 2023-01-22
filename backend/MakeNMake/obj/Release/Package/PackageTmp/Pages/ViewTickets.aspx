<%@ Page Title="" Language="C#" MasterPageFile="AdminMaster.Master" AutoEventWireup="true" CodeBehind="ViewTickets.aspx.cs" Inherits="MakeNMake.Excalation.ViewTickets" %>

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
            $(window).keydown(function (event) {
                if (event.keyCode == 13) {
                    event.preventDefault();
                    return false;
                }
            });
        });
    </script>    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="panel panel-success table-responsive">
        <div class="panel-heading">
            <h3 class="panel-title paneltitle">Search Customer</h3>
        </div>
         <br/>
                    <div class="input-group input-group-xs">
                       <%-- <span class="input-group-addon" id="Span1"></span>--%>
                        
                        <asp:TextBox ID="txtSearchclient" Width="500px"  
                             CssClass="form-control" runat="server" style="margin-left:20px;" PlaceHolder="Search by Customer Name,TicketId,Status or Engineer Name"></asp:TextBox>
                          <asp:Button ID="btnSubmit" runat="server" Text="Search"  style="margin-left:10px;" CssClass="btn btn-success" ValidationGroup="user" OnClick="btnSubmit1_Click" />
                      
                    </div>
        <br />
       
        <div class="panel-heading" id="divClientList" runat="server" >
            <h3 class="panel-title paneltitle">All Tickets</h3>
        </div>
        <div class="panel-body table-responsive">

                        <asp:HiddenField ID="hdnCustomer" ClientIDMode="Static" runat="server" />
                        <asp:Label ID="lblMsg" CssClass="label label-success" runat="server"></asp:Label>
                        <asp:Repeater ID="RptTickets" runat="server" OnItemCommand="RptTickets_ItemCommand">
                            <HeaderTemplate>
                                <table class="table table-hover  table-bordered  table-condensed" data-height="400">
                                    <thead style="color: White; background-color: #6b9297;">
                                        <tr>
                                            <th>Ticket Id</th>
                                            <th>Customer Name</th>
                                            <th>Created</th>
                                            <th>Status</th>
                                            <th>Assigned To</th>
                                            <th>Last Modified</th>
                                            <th></th>
                                        </tr>
                                    </thead>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tbody style="text-align: center !important">
                                    <tr>
                                        <td>
                                           
                                <asp:LinkButton ID="Label1" CommandName="TicketID" Text='<%#Eval("TicketID")%>' CommandArgument='<%#Eval("TicketID") %>' runat="server"></asp:LinkButton>
                           
                                            <%--<asp:Label ID="Label1" runat="server" Text='<%#Eval("TicketID") %>'></asp:Label>--%>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblcustname" runat="server" Text='<%#Eval("Name") %>'></asp:Label>
                                        </td>
                                        <td>
                                            
                                            <asp:Label ID="lblcreated" Text='<%#Eval("created") %>' runat="server"></asp:Label></td>
                                        <td><asp:HiddenField ID="hdnEngineerID" runat="server" Value='<%#Eval("SEngineerID") %>' />
                                            <asp:Label ID="lblStatus" runat="server" Text='<%#Eval("Status") %>'></asp:Label></td>
                                        <td>
                                            <asp:Label ID="LablblEngineer" runat="server" Text='<%#Eval("AssignedTo") %>'></asp:Label></td>

                                        <td>
                                            <asp:Label ID="lblmodified" runat="server" Text='<%#Eval("Modified") %>'></asp:Label></td>

                                        <%--<td>
                                            <asp:Button ID="btncheckhistory" runat="server" CommandName="CheckHistory"
                                                 CommandArgument='<%#Eval("TicketID") %>' Text="Check History" CssClass="btn-xs btn-success" />
                                        </td>--%>
                                         <%--<td>
                                <asp:LinkButton ID="lnkBtnName" CommandName="AddSkill" CommandArgument='<%#Eval("EngineerID") %>' runat="server">Add Skills</asp:LinkButton>
                            </td>--%>

                                    </tr>
                                </tbody>
                            </ItemTemplate>
                            <FooterTemplate>
                                </table>
                            </FooterTemplate>
                        </asp:Repeater>
                 <table class="tblpaging" id="Table1" runat="server" style="font-size: 12px;clear:both;margin-top:15px;">
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
         <div class="panel-footer">
            <%--<asp:Button ID="btnView" runat="server" Text="View tickets with Unassigned,Rejected and Escalated status"
                CssClass="btn btn-success" OnClick="btnView_Click"  />--%>
        </div>
    </div>
    <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-hidden="true">
        <uc1:userinfo runat="server" id="UserInfo" />
    </div>
</asp:Content>
