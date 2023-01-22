<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/AdminMaster.Master" AutoEventWireup="true" CodeBehind="Announcements.aspx.cs" Inherits="MakeNMake.Pages.Announcements" %>

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
    <style type="text/css">
        .table > thead > tr > th {
            text-align: center;
        }

        .table > tbody > tr > td {
            text-align: left!important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="panel panel-success table-responsive">
        <div class="panel-heading">
            <h3 class="panel-title paneltitle">Message Circulate</h3>
        </div>


        <div class="panel-body" style="padding-left: 0px;">
            <div class="col-sm-12" style="padding: 0px;">

                <div class="col-sm-6 text-left linkBottom">
                    <div class="input-group input-group-sm">
                        <span class="input-group-addon" id="Span4">Select Role</span>
                        <asp:DropDownList ID="ddlRole" runat="server" CssClass="form-control  dropdown"></asp:DropDownList>
                    </div>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" InitialValue="0" runat="server" ControlToValidate="ddlRole"
                        ErrorMessage="Select Role" ValidationGroup="user" Display="Dynamic" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                </div>
            </div>


            <div class="col-sm-12 text-left linkBottom" style="height:100px;width:700px;">
                <div class="input-group input-group-sm">
                    <span class="input-group-addon" id="Span2">Message</span>
                    <asp:TextBox ID="txtmsg" AutoComplete="off" MaxLength="300" style="resize: none;" TextMode="MultiLine"  Height="70" Width="500"
                        placeholder="Enter Message" CssClass="form-control" aria-describedby="basic-addon1" runat="server"></asp:TextBox>
                </div>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4"
                    runat="server" ValidationGroup="user" Display="Dynamic" ForeColor="Red"
                    ControlToValidate="txtmsg" SetFocusOnError="true" ErrorMessage="Enter Message"></asp:RequiredFieldValidator>
            </div>
            <div class="col-sm-12" style="padding: 0px;">

                <div class="col-sm-6 text-left linkBottom">
                    <div class="input-group input-group-sm">
                        <span class="input-group-addon" style="width: 100px; margin-left: 10px;">Send Sms</span> &nbsp;
              
                  <asp:CheckBox ID="chksendsms" ClientIDMode="Static"
                      AutoPostBack="true" runat="server" />  </div>
                    
                </div>
            </div>
           <%-- <div class="btn-group col-sm-6 ">
                    <div class="input-group input-group-sm">
                       <span class="input-group-addon" id="Span7">Status</span>
                        <asp:RadioButtonList ID="rdbStatus" Margin="0.5em;" runat="server" RepeatDirection="Horizontal">

                            <asp:ListItem style="margin-right: 20px;" Text="Active" Value="1" />

                            <asp:ListItem Text="Inactive" Value="0" />

                        </asp:RadioButtonList>
                    </div>
                </div>--%>
            <div class="col-sm-12 ">
                <asp:Button ID="btnRole" runat="server" Text="Save" CssClass="btn btn-success" ValidationGroup="user" OnClick="btnRole_Click" />

                <asp:Button ID="btnCancels" OnClick="btnCancels_Click" runat="server" Text="Cancel" CssClass="btn btn-success" />
            </div>
        </div>



    </div>

    <div class="panel panel-success table-responsive">
        <div class="panel-body">           
            <asp:Repeater ID="RptMsg" runat="server" OnItemCommand="RptMsg_ItemCommand1">
                <HeaderTemplate>
                    <table class="table table-hover  table-bordered  table-condensed" data-height="400">
                        <thead style="color: White; background-color: #6b9297;">
                            <tr>
                                <th>Role</th>
                                <th>Message</th>
                                <th>Created Date</th>
                                <th>Send Sms</th>
                                <th>Edit</th>
                                <th>Delete</th>
                            </tr>
                        </thead>
                </HeaderTemplate>
                <ItemTemplate>
                    <tbody>
                        <tr>
                            <td>
                                <asp:HiddenField ID="hdnroleid" runat="server" Value='<%#Eval("RoleId") %>' />
                                <asp:HiddenField ID="hdnmessageid" runat="server" Value='<%#Eval("MessageId") %>' />

                                <asp:Label ID="lblMessageId" runat="server" Visible="false" Text='<%#Eval("MessageId") %>'></asp:Label>

                                <asp:Label ID="lblRoleId" runat="server" Visible="false" Text='<%#Eval("RoleId") %>'></asp:Label>
                                  <asp:Label ID="Label1" runat="server" Text='<%#Eval("RoleName") %>'></asp:Label>

                            </td>

                            <td>
                                <asp:Label ID="lblMessage" runat="server" Text='<%#Eval("Message") %>'></asp:Label></td>

                            
                             <td>   <asp:Label ID="lblCreatedBy" runat="server" Visible="false" Text='<%#Eval("CreatedBy") %>'></asp:Label>                            
                                <asp:Label ID="lblCreate" runat="server" Text='<%#Convert.ToDateTime(Eval("CreatedDate")).ToString("MMMM dd, yyyy") %>'></asp:Label></td>
                             <%-- <td>
                                    <asp:Label ID="lblStatus" ClientIDMode="Static" runat="server" Text='<%# Convert.ToInt16( Eval("Status"))==1?"Sent":"Unsent"%>'></asp:Label></td>--%>
                              <td>
                                    <asp:Label ID="lblsms" ClientIDMode="Static" runat="server" Text='<%# Convert.ToInt16( Eval("SendSms"))==1?"Allowed":"Unallowed"%>'></asp:Label></td>
                                
                            <td>
                                <center><asp:ImageButton ID="ImgBtnEdit" runat="server" Width="24" Height="24"
                                             ImageUrl="~/Static/images/edit.png" CommandName="edit" CommandArgument='<%#Eval("MessageId") %>' /></center>
                            </td>
                            <td>
                                <center> <asp:ImageButton ID="ImgBtnDelete" OnClientClick="javascript:return DeleteConfirmation(this);"
                                            runat="server" ImageUrl="~/Static/images/trash.png"
                                            CommandName="delete" /></center>
                            </td>
                        </tr>
                    </tbody>
                </ItemTemplate>
                <FooterTemplate>
                    </table>
                </FooterTemplate>
            </asp:Repeater>
            <table class="tblpaging" runat="server" id="tblpaging" style="font-size: 12px; clear: both; margin-top: 15px;">
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
