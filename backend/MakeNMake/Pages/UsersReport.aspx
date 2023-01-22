<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/AdminMaster.Master" AutoEventWireup="true" CodeBehind="UsersReport.aspx.cs" Inherits="MakeNMake.Pages.UsersReport" %>
<%@ MasterType VirtualPath="~/Pages/AdminMaster.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:Button ID="btnexcelreport" runat="server"  Text="Download as Excel"  CssClass="btn btn-success" OnClick="btnexcelreport_Click1"  />
     <div class="panel-body table-responsive">
            <div runat="server" visible="false" class="col-sm-6 text-left linkBottom" style="padding-left: 0px;">
                <div class="input-group input-group-sm">


                    <span class="input-group-addon" id="Span3">Select number of items to be displayed per page </span>

                    <asp:DropDownList ID="ddlIndex" CssClass="form-control dropdown" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlIndex_SelectedIndexChanged">
                    </asp:DropDownList>
                </div>
            </div>
     <div class="input-group input-group-xs">

                <asp:TextBox ID="txtSearchclient" placeholder="Search User By Status,Zonename,Subzonename" Width="400px"
                    CssClass="form-control" runat="server"></asp:TextBox>
                <asp:Button ID="Button1" runat="server" Text="Search" Style="margin-left: 10px;"
                    CssClass="btn btn-success" OnClick="Button1_Click" />
                <asp:Button ID="btnRefresh" runat="server" Text="Refresh" Style="margin-left: 10px;"
                    CssClass="btn btn-success" OnClick="btnRefresh_Click" />
            </div>
             <asp:HiddenField ID="hdnuserID" runat="server" Value="" />
    <div class="panel-heading table-responsive" id="divClientList" runat="server" style="padding-left: 0px;">
               
                
                <asp:Repeater ID="RptAllUser" runat="server"  OnItemDataBound="RptAllUser_ItemDataBound">
                    <HeaderTemplate>
                        <table class="table table-hover  table-bordered  table-condensed" data-height="400">
                            <thead style="color: White; background-color: #6b9297;">
                                <tr>
                                    <th>Serial No</th>
                                      <th>User ID</th>
                                     <th>First Name</th>
                                    <th>Last Name</th>
                                    <th>Email ID</th>
                                    <th>Role</th>
                                    <th>Zone</th>
                                      <th>SubZpne</th>
                                   
                                    <th>Created</th>
                                    <th>Status</th>
                                  
                                  
                                </tr>
                            </thead>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tbody>
                            <tr >
                                <td>
                                    <%#Container.ItemIndex+1 %>
                                </td>
                                 <td>
                                     <asp:Label ID="UserID" runat="server" Text='<%#Eval("UserID") %>'></asp:Label>
                                </td>
                                <td>
                                    
                                   
                                   
                                  
                                    <asp:Label ID="lblname" runat="server" Text='<%#Eval("FName") %>'>

                                    </asp:Label>
                                      </td>  <td>
                                    <asp:Label ID="lbllname" runat="server" Text='<%#Eval("LName") %>'></asp:Label></td>

                                <td>
                                    <asp:Label ID="lblEmailid" runat="server" Text='<%#Eval("EmailID") %>'></asp:Label></td>
                                <td>
                                    <asp:Label ID="Lblrolename" runat="server" Text='<%#Eval("RoleName") %>'></asp:Label>
                                </td>
                                <td>
                                     <asp:Label ID="hdnZone" runat="server" Text='<%#Eval("ZoneID") %>' style="display:none;" >  </asp:Label>
                                    <asp:Label ID="lblZonename" runat="server"></asp:Label></td>
                                 <td>
                                      <asp:Label ID="hdnSubZone" runat="server" Text='<%#Eval("SubZoneID") %>' style="display:none;" >  </asp:Label>
                                    <asp:Label ID="lblSubzone" runat="server"></asp:Label></td>
                                     <td>
                                    <asp:Label ID="lblcreated" runat="server" Text='<%# Eval("Created", "{0:dd/MM/yyyy}")%>'></asp:Label></td>
                                <td>
                                    <asp:Label ID="lblStatus" ClientIDMode="Static" runat="server" Text='<%# Convert.ToInt32( Eval("Status"))==1?"Active":"InActive"%>'></asp:Label></td>
                              
                               
                              
                           
                                
                        </tbody>
                    </ItemTemplate>
                    <FooterTemplate>
                        </table>
                    </FooterTemplate>
                </asp:Repeater>
                <table id="tblPaging" runat="server" class="tblpaging" style="font-size: 12px; clear: both; margin-top: 15px;">
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
