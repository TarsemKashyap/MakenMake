<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/AdminMaster.Master" AutoEventWireup="true" CodeBehind="CustomerTicketHistory.aspx.cs" Inherits="MakeNMake.Pages.CustomerTicketHistory" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="panel panel-success">
        <div class="panel-heading">
            <h3 class="panel-title paneltitle"> Ticket History </h3>
        </div>
        <div class="panel-body" style="padding-left: 0px;">
            <div class="col-sm-4">
                <div class="input-group">
                    <span class="input-group-addon" id="Span2">Select Ticket Id</span>
                <asp:DropDownList ID="ddlTickets" AutoPostBack="true" OnSelectedIndexChanged="ddlTickets_SelectedIndexChanged" CssClass="form-control" runat="server"></asp:DropDownList></div>
                </div>
            <div class="col-sm-12 table-responsive" style="margin-top:20px;">
                <asp:Repeater ID="RptTickets" runat="server">
                    <HeaderTemplate>
                        <table class="table table-hover  table-bordered  table-condensed" data-height="400">
                            <thead style="color: White; background-color: #6b9297;">
                                <tr>
                                    <th>Assigned To</th>
                                    <th>Last Modified</th>
                                    <th>Remark</th>
                                    <th>Modified On</th>
                                    <th>Status</th>
                                </tr>
                            </thead>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tbody style="text-align: center !important">
                            <tr>

                                <td>
                                    <asp:Label ID="LablblEngineer" runat="server" Text='<%#Eval("AssignedTo") %>'></asp:Label>
                                </td>


                                <td>
                                    <asp:Label ID="lblmodified" runat="server" Text='<%#Eval("ModifiedBy") %>'></asp:Label></td>
                                <td>
                                    <asp:Label ID="lblcreated" Text='<%#Eval("Remark") %>' runat="server"></asp:Label></td>
                                <td>
                                    <asp:Label ID="Label1" runat="server" Text='<%#Eval("Modified") %>'></asp:Label></td>
                                <td>
                                    <asp:Label ID="lblType" runat="server" Text='<%#Eval("Status") %>'></asp:Label></td>

                            </tr>
                        </tbody>
                    </ItemTemplate>
                    <FooterTemplate>
                        </table>
                    </FooterTemplate>
                </asp:Repeater>
                  <table id="tblticket" runat="server" class="tblpaging" style="font-size: 12px; clear: both; margin-top: 15px;">
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
