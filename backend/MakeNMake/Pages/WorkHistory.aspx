<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/AdminMaster.Master" AutoEventWireup="true" CodeBehind="WorkHistory.aspx.cs" Inherits="MakeNMake.Pages.WorkHistory" %>
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
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="panel panel-success">
        <div class="panel-heading">
            <h3 class="panel-title paneltitle">Work History</h3>
        </div>
        <div class="panel-body">
          

                 <asp:HiddenField ID="hdnCustomer" ClientIDMode="Static" runat="server" />
                <asp:Label ID="lblMsg" CssClass="label label-success" runat="server"></asp:Label>
                <asp:Repeater ID="RptTickets" runat="server" >
                    <HeaderTemplate>
                        <table class="table table-hover  table-bordered  table-condensed" data-height="400">
                            <thead style="color: White; background-color: #6b9297;">
                                <tr>
                                      <th>Service Date</th>
                                    <th>Service From</th>
                                    <th>Service To</th>
                                     <th>Time Spent (in mins)</th>
                                    <th>Work Check In</th>
                                    <th>Work Check Out</th>                                   
                                   
                                </tr>
                            </thead>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tbody>
                            <tr>
                                <td>
                                    
                                    <asp:Label ID="lblservicedate" runat="server" Text='<%#Eval("Servicedate") %>'></asp:Label>
                                </td>
                                 <td>
                                    <asp:Label ID="lblservicefrom" runat="server" Text='<%#Eval("ServiceFrom") %>'></asp:Label></td>
                                <td>
                                    <asp:Label ID="lblserviceto" runat="server" Text='<%#Eval("ServiceTo") %>'></asp:Label></td>
                               <td>
                                    <asp:Label ID="lblspenttime" runat="server" Text='<%#Eval("totalMins") %>'></asp:Label></td>
                                <td>
                                    <asp:Label ID="lblcheckin" runat="server" Text='<%#Eval("WorkDescCheckIn")%>'></asp:Label></td>
                                <td>
                                    <asp:Label ID="lblcheckout" runat="server" Text='<%# Eval("WorkDescCheckOut")%>'></asp:Label></td>
                               
                            </tr>
                        </tbody>
                    </ItemTemplate>
                    <FooterTemplate>
                        </table>
                    </FooterTemplate>
                </asp:Repeater>
                <%--<table class="tblpaging" style="font-size: 12px;clear:both;margin-top:15px;">
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
                </table>--%>
        </div>
    </div>
    <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-hidden="true">
        <uc1:UserInfo runat="server" ID="UserInfo" />
    </div>
</asp:Content>