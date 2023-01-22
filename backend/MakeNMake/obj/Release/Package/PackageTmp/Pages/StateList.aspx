<%@ Page Title="StateList" Language="C#" MasterPageFile="AdminMaster.Master" AutoEventWireup="true" CodeBehind="StateList.aspx.cs" Inherits="MakeNMake.Admin.StateList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="panel panel-success">
        <div class="panel-heading">
            <h3 class="panel-title paneltitle">States</h3>
        </div>
        <div class="panel-body">

            <asp:Repeater ID="RptStateList" runat="server">
                <HeaderTemplate>
                    <table class="table table-hover  table-bordered  table-condensed" data-height="400">
                        <thead style="color: White; background-color: #6b9297;">
                            <tr>
                                <th style="width: 200px;">Country </th>
                                <th>State</th>
                            </tr>
                        </thead>
                </HeaderTemplate>
                <ItemTemplate>
                    <tbody>
                        <tr>
                            <td>
                                <asp:Label ID="lblquantFrom" runat="server" Text='<%#Eval("c_name") %>'></asp:Label></td>
                            <td>
                                <asp:Label ID="lblDiscount" runat="server" Text='<%#Eval("States") %>'></asp:Label></td>
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
         <div class="panel-body">
             <asp:Button ID="btnBack" runat="server" Text="Back" OnClick="btnBack_Click"  CssClass="btn btn-success"/>
         </div>
    </div>
</asp:Content>
