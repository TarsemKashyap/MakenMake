<%@ Page Title="Tool Assignment" Language="C#" MasterPageFile="~/Pages/AdminMaster.Master" AutoEventWireup="true" CodeBehind="ToolAssignment.aspx.cs" Inherits="MakeNMake.ToolAssignment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>

        function DeleteConfirmation(ele) {
            var getValue = confirm('Are you sure you want to delete ?');
            if (getValue) {
                return true;
            }
            else {
                return false;
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="panel panel-success table-responsive">
        <div runat="server" id="divuser" class="panel-heading">
            <h3 class="panel-title paneltitle">Tool Assignment</h3>
        </div>
        <div runat="server" id="divedituser" visible="false" class="panel-heading">
            <h3 class="panel-title paneltitle">Edit User</h3>
        </div>
        <div class="panel-body" style="padding-left:0px;">
            <div class="col-sm-12" style="padding-left:0px;">
                <div class="col-sm-6 text-left linkBottom">
                    <div class="input-group input-group-sm">
                        <span class="input-group-addon" id="Span4">Tool Type</span>
                        <asp:DropDownList ID="ddlToolType" runat="server" AutoPostBack="true" CssClass="form-control  dropdown" OnSelectedIndexChanged="ddlToolType_SelectedIndexChanged">
                            <asp:ListItem Text="--Select Tool--" Value="0"></asp:ListItem>
                             <asp:ListItem Text="For Repair" Value="1"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" InitialValue="0" runat="server" ControlToValidate="ddlToolType"
                        ErrorMessage="Select Tool Type" ValidationGroup="user" Display="Dynamic" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                </div>

                <div class="col-sm-6 text-left linkBottom" id="dvstate" runat="server">
                    <div class="input-group input-group-sm">
                        <span class="input-group-addon" id="Span12">Tool</span>
                        <asp:DropDownList ID="ddlTool" AutoPostBack="true" CssClass="form-control dropdown" runat="server">
                        </asp:DropDownList>
                    </div>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" ValidationGroup="user" runat="server"
                        ErrorMessage="Enter Tool"
                        ControlToValidate="ddlTool" Display="Dynamic" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                </div>
                <div class="col-sm-6 text-left linkBottom" id="dvdistrict" runat="server">
                    <div class="input-group input-group-sm">
                        <span class="input-group-addon" id="Span13">Engineer</span>
                        <asp:DropDownList ID="ddlEngineer" CssClass="form-control dropdown" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlEngineer_SelectedIndexChanged">
                        </asp:DropDownList>
                    </div>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator12" ValidationGroup="user" runat="server"
                        ErrorMessage="Enter Engineer" InitialValue="0"
                        ControlToValidate="ddlEngineer" Display="Dynamic" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                </div>
                <div id="dvcity" runat="server" class="col-sm-6 text-left linkBottom">
                    <div class="input-group input-group-sm">
                        <span class="input-group-addon" id="Span14">Tickets</span>
                        <asp:DropDownList ID="ddlTickets" CssClass="form-control dropdown" runat="server">
                        </asp:DropDownList>
                    </div>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator13" ValidationGroup="user" runat="server"
                        ErrorMessage="Enter Tickets"
                        ControlToValidate="ddlTickets" Display="Dynamic" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                </div>
                <div class="col-sm-6 text-left linkBottom">
                    <div class="input-group input-group-sm">
                        <span class="input-group-addon" id="Span1">Remark</span>
                        <asp:TextBox ID="txtRemark" placeholder="Remarks" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtRemark"
                        ErrorMessage="Enter Remark" ValidationGroup="user" Display="Dynamic" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>

                </div>
                <div class="col-sm-6 text-left linkBottom">
                    <div class="input-group input-group-sm">
                        <span class="input-group-addon" id="Span2">Status</span>
                        <asp:DropDownList ID="ddlStatus" runat="server" Width="200px" CssClass="form-control  dropdown">
                        </asp:DropDownList>
                    </div>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlStatus"
                        ErrorMessage="Please Select Status" InitialValue="0" ValidationGroup="user" Display="Dynamic" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>


                </div>
                <div class="col-sm-12 text-left linkBottom" style="margin-top: 20px;">
                    <asp:Button ID="btnSubmit" runat="server" Text="Add" CssClass="btn btn-success" ValidationGroup="user" OnClick="btnSubmit_Click" />
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-success" OnClick="btnCancel_Click" />
                </div>
            </div>
        </div>
    </div>



    <div class="panel panel-success table-responsive">
        <div class="panel-heading">
            <h3 class="panel-title paneltitle">Tool Assignment List</h3>
        </div>
        <div class="panel-body">
            <div id="Div1" runat="server" visible="false" class="col-sm-6 text-left linkBottom" style="padding-left: 0px;">
                <div class="input-group input-group-sm">


                    <span class="input-group-addon" id="Span3">Select number of items to be displayed per page </span>

                    <asp:DropDownList ID="ddlIndex" CssClass="form-control dropdown" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlIndex_SelectedIndexChanged">
                    </asp:DropDownList>
                </div>
            </div>

            <div class="panel-heading table-responsive" id="divClientList" runat="server" style="padding-left: 0px;">
                <asp:HiddenField ID="hdnuserID" runat="server" />
                <asp:Label ID="lblMsg" CssClass="label label-success" runat="server"></asp:Label>
                <asp:Repeater ID="RptAllUser" runat="server" OnItemCommand="RptAllUser_ItemCommand">
                    <HeaderTemplate>
                        <table class="table table-hover  table-bordered  table-condensed" data-height="400">
                            <thead style="color: White; background-color: #6b9297;">
                                <tr>
                                    <th>Assignment ID</th>
                                    <th>Tool Type</th>
                                    <th>Tool</th>
                                    <th>Engineer</th>
                                    <th>TicketId</th>
                                    <th>Remark</th>
                                    <th>Status</t
                                    <th>Edit</th>
                                    <%--<th>Delete</th>--%>
                                </tr>
                            </thead>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tbody>
                            <tr>
                                <td>

                               <%--     <asp:LinkButton ID="lblAssigendid" CommandName="AssignmentID" Text='<%#Eval("AssignmentID") %>' CommandArgument='<%#Eval("AssignmentID") %>' runat="server"></asp:LinkButton>--%>
                                    <asp:Label ID="Label1" runat="server" Text='<%#Eval("AssignmentID") %>'></asp:Label>
                                </td>
                                <td>

                                    <asp:HiddenField ID="hdnAssignID" runat="server" Value='<%#Eval("AssignmentID") %>' />
                                    <asp:HiddenField ID="hdnToolId" runat="server" Value='<%#Eval("ToolId") %>' />
                                    <asp:HiddenField ID="hdnengid" runat="server" Value='<%#Eval("AssignedTo") %>' />
                                    <asp:Label ID="lblname" runat="server" Text='<%#Eval("ToolType") %>'></asp:Label></td>
                                <td>
                                    <asp:Label ID="lblToolName" runat="server" Text='<%#Eval("ToolName") %>'></asp:Label></td>
                                <td>
                                    <asp:Label ID="lblAssignto" runat="server" Text='<%# Eval("Name")%>'></asp:Label></td>
                                 <td>
                                    <asp:Label ID="LblticketID" runat="server" Text='<%#Eval("TicketID") %>'></asp:Label>
                                </td>
                               
                                <td>
                                    <asp:Label ID="lblremark" runat="server" Text='<%# Eval("Remark")%>'></asp:Label></td>

                                <td>
                                    <asp:Label ID="lblStatus" ClientIDMode="Static" runat="server" Text='<%# Convert.ToInt32( Eval("Status"))==1?"Issued":"Returned"%>'></asp:Label></td>


                                <td>
                                    <center>  <asp:ImageButton ID="ImgBtnEdit" runat="server" Width="24" Height="24"
                                    ImageUrl="~/Static/images/edit.png" CommandName="edit" Visible='<%# Convert.ToInt32( Eval("Status"))==1?true:false%>' CommandArgument='<%#Eval("AssignmentID") %>' /></center>
                                </td>
                               <%-- <td>
                                    <center><asp:ImageButton ID="ImgBtnDelete" OnClientClick="javascript:return DeleteConfirmation(this);"
                                    runat="server" ImageUrl="~/Static/images/trash.png"
                                    CommandName="delete" /></center>
                                </td>--%>
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
    </div>
</asp:Content>


