<%@ Page Title="EngineerSkills" Language="C#" MasterPageFile="AdminMaster.Master" AutoEventWireup="true" CodeBehind="EngineerSkills.aspx.cs" Inherits="MakeNMake.Admin.EngineerSkills" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../Scripts/jquery-2.0.1.min.js"></script>
     <script type="text/javascript">
       
         function PageReload() {
             var getValue = confirm('No Record Found ');
             if (getValue) {
                 window.location.href = window.location.href;
             }
             else {
                 window.location.href = window.location.href;
             }
         }
         $(document).ready(function () {
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
            <h3 class="panel-title paneltitle">Engineer skills</h3>
        </div>
        <br />
          <div class="input-group input-group-xs" style="padding-left:15px;">
                        
                        
                        <asp:TextBox ID="txtSearchclient" placeholder="Search By Name , Email Id or Zone" Width="500px"  
                             CssClass="form-control" runat="server"></asp:TextBox>&nbsp;&nbsp;
                            <asp:Button ID="btnSubmit" runat="server" Text="Search" OnClick="btnSubmit_OnClick"
                                 CssClass="btn btn-success" ValidationGroup="user" />
          </div>
        <br />
          <div class="panel-body">
           
                  <asp:Repeater ID="RptTickets" runat="server" OnItemCommand="RptTickets_ItemCommand">
                <HeaderTemplate>
                    <table class="table table-hover  table-bordered  table-condensed" data-height="400">
                        <thead style="color: White; background-color: #6b9297;">
                            <tr>
                                <th>Engineer Name</th>
                                <th>Engineer EmailID</th>
                               <%-- <th>TotalSkills</th>--%>
                                <th>Zone</th>
                                <th>Subzone</th>
                                <th>Add/Edit Skills</th>
                                <%--<th>Edit Engineer Info</th>--%>
                            </tr>
                        </thead>
                </HeaderTemplate>
                <ItemTemplate>
                    <tbody>
                        <tr>
                            <td>
                                <asp:Label ID="Label1" runat="server" Text='<%#Eval("Name") %>'></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="EmailID" runat="server" Text='<%#Eval("EmailID") %>'></asp:Label>
                            </td>
                           <%-- <td>
                                <asp:Label ID="Label3" runat="server" Text='<%#Eval("TotalSkills") %>'></asp:Label></td>--%>
                            <td>
                                <asp:Label ID="Label2" runat="server" Text='<%#Eval("Zone") %>'></asp:Label></td>
                            <td>
                                <asp:Label ID="lblType" runat="server" Text='<%#Eval("SubZone") %>'></asp:Label></td>
                            <td>
                                <asp:LinkButton ID="lnkBtnName" CommandName="AddSkill" CommandArgument='<%#Eval("EngineerID") %>' runat="server">Add Skills</asp:LinkButton>
                            </td>
                         <%--    <td>
                                <asp:LinkButton ID="LinkButton1" CommandName="updateInfo" CommandArgument='<%#Eval("EngineerID") %>' runat="server">Update Info</asp:LinkButton>
                            </td>--%>
                        </tr>
                    </tbody>
                </ItemTemplate>
                <FooterTemplate>
                    </table>
                </FooterTemplate>
            </asp:Repeater>
              <table id="tblPaging" runat="server" class="tblpaging" style="font-size: 12px;clear:both;margin-top:15px;">
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
