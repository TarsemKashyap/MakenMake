<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ServiceEngineersReport.ascx.cs" Inherits="MakeNMake.UserControl.ServiceEngineersReport" %>
<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" %>
<div class="panel-body">
          
             <div class="input-group input-group-xs">
         
                       
                        <asp:DropDownList Width="200px" ID="ddlZone" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlZone_SelectedIndexChanged" CssClass="form-control  dropdown"></asp:DropDownList>
                    
         
                        
                        <asp:DropDownList  Width="200px" style="margin-left: 10px;" ID="ddlSubZone" runat="server" CssClass="form-control  dropdown" OnSelectedIndexChanged="ddlSubZone_SelectedIndexChanged"></asp:DropDownList>
                   
                <asp:TextBox ID="txtfromdate" placeholder="From Date" Width="200px" Style="margin-left: 10px;"
                    CssClass="form-control" runat="server"></asp:TextBox>
         <asp:CalendarExtender ID="CalendarExtender1" TargetControlID="txtfromdate" 
                          Format="dd/MM/yyyy" SelectedDate='<%#Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy")) %>'  runat="server"></asp:CalendarExtender>
          <asp:TextBox ID="txttodate" placeholder="To Date" Width="200px"
                    CssClass="form-control" runat="server" style="margin-left: 10px;"></asp:TextBox>
         <asp:CalendarExtender ID="CalendarExtender2" TargetControlID="txttodate" 
                          Format="dd/MM/yyyy" SelectedDate='<%#Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy")) %>'  runat="server"></asp:CalendarExtender>
                <asp:Button ID="Button1" runat="server" Text="Search" Style="margin-left: 10px;margin-top: 10px;"
                    CssClass="btn btn-success" OnClick="Button1_Click" />
              <%--  <asp:Button ID="btnRefresh" runat="server" Text="Refresh" Style="margin-left: 10px;margin-top: 10px;"
                    CssClass="btn btn-success" OnClick="btnRefresh_Click" />--%>
            </div>
            <br />

             <div class="panel-heading table-responsive" id="divTicketstatus" runat="server" style="padding-left: 0px;">
                 <asp:HiddenField ID="hdnCustomer" ClientIDMode="Static" runat="server" />
                <asp:Label ID="lblMsg" CssClass="label label-success" runat="server"></asp:Label>
                <asp:Repeater ID="RptTickets" runat="server"  OnItemDataBound="RptTickets_ItemDataBound" >
                    <HeaderTemplate>
                        <table class="table table-hover  table-bordered  table-condensed" data-height="400">
                            <thead style="color: White; background-color: #6b9297;">
                                <tr>
                                      <th>Ticket Id</th>
                                    <th>First Name</th>
                                    <th>Last Name</th>
                                    <th>Service Name</th>
                                    <th>Client ID</th>
                                     <th>Status</th>                                   
                                    <th>Created</th>
                                   <th>Category</th>
                                     <th>Plans</th>
                                     <th>Description</th>
                                 <th>Zone</th>
                                    <th>SubZone</th>
                                </tr>
                            </thead>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tbody>
                            <tr><td>
                                    <asp:Label ID="Label1" runat="server" Text='<%#Eval("TicketID") %>'></asp:Label>
                                </td>
                               
                                <td>
                                    <asp:Label ID="lbfname" runat="server" Text='<%#Eval("SEngineerFirstName") %>'></asp:Label>
                                </td>
                                 <td>
                                    <asp:Label ID="lbLastname" runat="server" Text='<%#Eval("SEngineerLastName") %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lbServiceId" runat="server" Text='<%#Eval("ServiceName") %>'></asp:Label></td>

                                   
                                <td>
                                    <asp:Label ID="lbClientId" runat="server" Text='<%#Eval("CustomerId") %>'></asp:Label>
                                    </td>
                                <td>
                                    <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("Status")%>'></asp:Label></td>
                                  <td>
                                    <asp:Label ID="lblCreated" runat="server" Text='<%#Convert.ToDateTime(Eval("created")).ToString("dd/MM/yyyy")%>'></asp:Label></td>
                                <td>
                                     <asp:Label ID="lbCategoryid" runat="server" Text='<%#Eval("Category") %>' style="display:none;"></asp:Label>

                                    <asp:Label ID="lbcatgoryname" runat="server"></asp:Label>
                                </td>
                               <td>
                                    <asp:Label ID="lbplanid" runat="server" Text='<%#Eval("Plans") %>' style="display:none;"></asp:Label>

                                    <asp:Label ID="lbplanname" runat="server"></asp:Label>
                               </td>
                              
                                
                                <td>
                                    <asp:Label ID="Label2" runat="server" Text='<%# Convert.ToString(Eval("Description"))==""?"No Reason Mentioned":Eval("Description")%>'></asp:Label>                                 
                                
                                    </td>
                                <td>
                                     <asp:Label ID="hdnZone" runat="server" Text='<%#Eval("ZoneID") %>' style="display:none;" >  </asp:Label>
                                    <asp:Label ID="lblZonename" runat="server"></asp:Label></td>
                                 <td>
                                      <asp:Label ID="hdnSubZone" runat="server" Text='<%#Eval("SubZoneID") %>' style="display:none;" >  </asp:Label>
                                    <asp:Label ID="lblSubzone" runat="server"></asp:Label></td>
                            </tr>
                        </tbody>
                    </ItemTemplate>
                    <FooterTemplate>
                        </table>
                    </FooterTemplate>
                </asp:Repeater>
                <table class="tblpaging" id="tblpaging" runat="server" style="font-size: 12px;clear:both;margin-top:15px;">
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
                  <asp:Button ID="btnexcelreport" runat="server"  Text="Download as Excel"  CssClass="btn btn-success" OnClick="btnexcelreport_Click1"  />
                  <asp:Button ID="btnPdfreport" runat="server"  Text="Download as pdf"  CssClass="btn btn-success" OnClick="btnPdfreport_Click" />
        </div>
        </div>