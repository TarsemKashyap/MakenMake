<%@ Page Title="" Language="C#" MasterPageFile="AdminMaster.Master" AutoEventWireup="true" CodeBehind="PageInfo.aspx.cs" Inherits="MakeNMake.Admin.PageInfo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript">
      
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
    <div class="panel panel-success">

        <div runat="server" id="divuser" class="panel-heading">
            <h3 class="panel-title paneltitle">Parent Page</h3>
        </div>
        <div runat="server" id="divedituser" visible="false" class="panel-heading">
            <h3 class="panel-title paneltitle">Edit User</h3>
        </div>

        <div class="panel-body">
            <div class="col-sm-12">
                <%-- <asp:ValidationSummary ID="ValidationSummary1" ValidationGroup="user" ForeColor="Red" ShowModelStateErrors="true" ShowMessageBox="false" ShowSummary="true" ShowValidationErrors="true" runat="server" />--%>
               <%-- <div class="col-sm-6 text-left linkBottom linkBottom">
                    <div class="input-group input-group-sm">
                        <span class="input-group-addon" >First Name</span>
                        <asp:TextBox ID="txtfirstname" CssClass="form-control" onkeypress="return ValidateCharacters(event)" placeholder="First Name" runat="server"></asp:TextBox>
                    </div>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtfirstname"
                        ErrorMessage="Enter First Name" ValidationGroup="user" Display="Dynamic" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                </div>
                <div class="col-sm-6 text-left linkBottom">
                    <div class="input-group input-group-sm">
                        <span class="input-group-addon" id="Span1">Last Name</span>
                        <asp:TextBox ID="txtlastname" placeholder="Last Name" onkeypress="return ValidateCharacters(event)" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="col-sm-6 text-left linkBottom">
                    <div class="input-group input-group-sm">
                        <span class="input-group-addon" id="Span2">Email Id</span>
                        <asp:TextBox ID="txtEmailID" placeholder="Email Id" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtEmailID"
                        ErrorMessage="Enter EmailID" ValidationGroup="user" Display="Dynamic" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Enter Correct Email ID"
                        Display="Dynamic" ForeColor="Red" ControlToValidate="txtEmailID" ValidationGroup="user"
                        ValidationExpression="\b[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}\b"></asp:RegularExpressionValidator>

                </div>--%>
                <div class="col-sm-6 text-left linkBottom">
                    <div class="input-group input-group-sm">
                        <span class="input-group-addon" id="Span4"> Role</span>
                        <asp:DropDownList ID="ddlRole" runat="server" AutoPostBack="true"  CssClass="form-control  dropdown"></asp:DropDownList>
                    </div>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" InitialValue="0" runat="server" ControlToValidate="ddlRole"
                        ErrorMessage="Select role" ValidationGroup="user" Display="Dynamic" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                </div>
                <div class="col-sm-6 text-left linkBottom">
                    <div class="input-group input-group-sm">
                        <span class="input-group-addon" id="Span1">Parent Name</span>
                        
                        <asp:TextBox ID="txtparentname" placeholder=" Parent Name"  CssClass="form-control" runat="server"></asp:TextBox>
                    <asp:HiddenField ID="parendid" runat="server" />
                    </div>
                </div>
                
               <%-- <div class="col-sm-6 text-left linkBottom">
                    <div class="input-group input-group-sm">
                        <span class="input-group-addon" id="Span2">Description</span>
                        <asp:TextBox ID="txtdescription" placeholder="Description" onkeypress="return ValidateCharacters(event)" CssClass="form-control" runat="server"></asp:TextBox>
                    <asp:Label ID="lblcreate" runat="server" Visible="false"></asp:Label>
                    </div>
                </div>
                <div class="col-sm-6 text-left linkBottom">
                    <div class="input-group input-group-sm">
                        <span class="input-group-addon" id="Span3">Title</span>
                        <asp:TextBox ID="txttitle" placeholder="Title" onkeypress="return ValidateCharacters(event)" CssClass="form-control" runat="server"></asp:TextBox>
                        <asp:Label ID="lblmod" runat="server" Visible="false"></asp:Label>
                    </div>
                </div>
                --%>
                
                
            </div>
        </div>
        <div class="panel-footer">
            <asp:Button ID="btnSubmit" runat="server" Text="Add"  CssClass="btn btn-success" ValidationGroup="user" OnClick="btnSubmit_Click" />
            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-success" OnClick="btnCancel_Click"  />
        </div>
    </div>
<div class="panel panel-success">
        <div class="panel-body">
             <table id="Table1" runat="server" align="center" width="100%" cellspacing="0" cellpadding="0">
            <tr>
                <td style="padding-left: 10px;" colspan="2" align="center">
                    <asp:HiddenField ID="hdnParentID" runat="server" />
                    <asp:Repeater ID="RptParent" runat="server" OnItemCommand="RptParent_ItemCommand">
                        <HeaderTemplate>
                            <table class="table table-hover  table-bordered  table-condensed" data-height="400">
                                <thead style="color: White; background-color: #6b9297;">
                                    <tr>
                                        <th>Role Name </th>
                                        <th>Parent Name</th>
                                        
                                        <th>Edit</th>
                                        <th>Delete</th>
                                    </tr>
                                </thead>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tbody>
                                <tr>
                                   
                                    <td>
                                       <%-- <asp:HiddenField ID="hdnroleid" runat="server" Value='<%#Eval("RoleID") %>' />--%>
                                        <asp:Label ID="lblrolename" runat="server" Text='<%#Eval("RoleName") %>'></asp:Label></td>
                                     <td>
                                        <asp:HiddenField ID="hdnparentId" runat="server" Value='<%#Eval("ParentNodeID") %>' />
                                        <asp:Label ID="lblparentName" runat="server" Text='<%#Eval("ParentName") %>'></asp:Label></td>
                                    <td >
                                        <asp:ImageButton ID="ImgBtnEdit" runat="server"
                                             ImageUrl="~/Static/images/edit.png" CommandName="edit" CommandArgument='<%#Eval("ParentNodeID") %>' />
                                    </td>
                                    <td>
                                        <asp:ImageButton ID="ImgBtnDelete" OnClientClick="javascript:return DeleteConfirmation(this);"
                                            runat="server" ImageUrl="~/Static/images/trash.png"
                                            CommandName="delete" CommandArgument='<%#Eval("ParentNodeID") %>' />
                                    </td>
                            </tbody>
                        </ItemTemplate>
                        <FooterTemplate>
                            </table>
                        </FooterTemplate>
                          </asp:Repeater>
                </td>
            </tr>
            
            <tr>
            <td colspan="2" align="center">
                <table>
                    <tr>
                        <td colspan="5">
                             
                        </td>
                    </tr>
                    <tr>
                        <td width="80" valign="top" align="center">
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
                </table>
            </td>
            </tr>            
            <tr>
                <td colspan="2" align="center" height="30">
                    <asp:Label ID="lblpage" runat="server" Text=""></asp:Label>
                </td>
            </tr>
        </table>

        </div>
    </div>
</asp:Content>
