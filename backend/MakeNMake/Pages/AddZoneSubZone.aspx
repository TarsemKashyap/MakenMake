<%@ Page Title="Zone SubZone" Language="C#" MasterPageFile="AdminMaster.Master" AutoEventWireup="true" CodeBehind="AddZoneSubZone.aspx.cs" Inherits="MakeNMake.Admin.AddZoneSubZone" %>

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
    <div class="panel panel-success">
        <div class="panel-heading">
            <h3 class="panel-title paneltitle">Zone-Subzone</h3>
        </div>
        <div class="panel-body" style="padding-left:0px;">
            <div class="col-sm-12" style="padding-left:0px;">
                <%-- <asp:ValidationSummary ID="ValidationSummary1" ValidationGroup="zone" ShowSummary="true" ForeColor="Red" runat="server" />--%>

                <div class="col-sm-6 text-left linkBottom">
                    <div class="input-group input-group-sm">
                        <span class="input-group-addon" id="Span4">Select Zone </span>
                        <asp:DropDownList ID="ddlZone" CssClass="form-control dropdown" OnSelectedIndexChanged="ddlZone_SelectedIndexChanged"
                            AutoPostBack="true" runat="server">
                        </asp:DropDownList>
                    </div>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" InitialValue="0" ControlToValidate="ddlZone"
                        ErrorMessage="Select Zone" ValidationGroup="zone" Display="Dynamic" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>

                </div>
                <div class="col-sm-6 text-left linkBottom">
                    <div class="input-group input-group-sm">
                        <span class="input-group-addon" id="Span1">Select Subzone</span>
                        <asp:DropDownList ID="ddlSubZone" CssClass="form-control dropdown" runat="server">
                        </asp:DropDownList>
                    </div>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" InitialValue="0" ControlToValidate="ddlSubZone"
                        ErrorMessage="Select SubZone" ValidationGroup="zone" Display="Dynamic" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>

                </div>
                <div class="col-sm-6 text-left linkBottom">
                    <div class="input-group input-group-sm">
                        <span class="input-group-addon" id="Span2">Status</span>
                        <%-- <asp:DropDownList ID="ddlZStatus" CssClass="form-control dropdown" runat="server">
                            <asp:ListItem Text="--Select Status--" Value="0"></asp:ListItem>
                            <asp:ListItem Text="True" Value="1"></asp:ListItem>
                            <asp:ListItem Text="False" Value="2"></asp:ListItem>
                        </asp:DropDownList>--%>
                        <asp:RadioButtonList ID="rdbStatus" Style="margin-left: 15px; margin-top: 10px;" CellPadding="10" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Text="Active" style="margin-right: 20px;" Value="1" />
                            <asp:ListItem Text="Inactive" Value="0" />
                        </asp:RadioButtonList>
                    </div>
                </div>
                <%-- <div class="col-sm-6 text-left linkBottom">
                    <div class="input-group input-group-sm">
                        <span class="input-group-addon" id="Span3">Select SubZone</span>
                        <asp:DropDownList ID="DropDownList2" CssClass="form-control dropdown"  runat="server">
                        </asp:DropDownList>
                    </div>
                         <asp:RequiredFieldValidator ID="RequiredFieldValidator4" CssClass="form-control" runat="server" InitialValue="0" ControlToValidate="ddlSubZone"
                        ErrorMessage="Select SubZone" ValidationGroup="zone"  Display="Dynamic" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
             
                </div>--%>
                <div class="col-sm-12 text-left linkBottom">
                    <asp:Button ID="btnADD" runat="server" Text="Add" OnClick="btnADD_Click" CssClass="btn btn-success" ValidationGroup="zone" />
                    <asp:Button ID="btnCancel" OnClick="btnCancel_Click" runat="server" Text="Cancel" CssClass="btn btn-success" />
                </div>
            </div>
        </div>
    </div>

     
             


    <div class="panel panel-success table-responsive">
      

        <div class="panel-body ">
             

         <div class="input-group input-group-xs">

                <asp:TextBox ID="txtSearchclient" placeholder="Search By Zone and Subzone" Width="400px"
                    CssClass="form-control" runat="server"></asp:TextBox>
                <asp:Button ID="Btnsearch" runat="server" Text="Search" Style="margin-left: 30px;"
                    CssClass="btn btn-success" OnClick="Button1_Click"  />
            </div>
             <br />
            <asp:Repeater ID="RptZone" runat="server" OnItemCommand="RptZone_ItemCommand1">
                <HeaderTemplate>
                    <table class="table table-hover  table-bordered  table-condensed" data-height="400">
                        <thead style="color: White; background-color: #6b9297;">
                            <tr>
                                <th>Zone</th>
                                <th>Subzone</th>
                                <th>Created By</th>
                                <th>Created Date</th>
                                <th>Modified By</th>
                                <th>Modified Date</th>
                                <th>Status</th>
                                <th>Edit</th>
                                <th>Delete</th>
                            </tr>
                        </thead>
                </HeaderTemplate>
                <ItemTemplate>
                    <tbody>
                        <tr>
                            <td>
                                <asp:HiddenField ID="hdnZoneID" runat="server" Value='<%#Eval("ZoneID") %>' />
                                <asp:HiddenField ID="hdnSubZone" runat="server" Value='<%#Eval("SubZoneID") %>' />
                                <asp:Label ID="lblZone" runat="server" Text='<%#Eval("ZoneName") %>'></asp:Label>
                                <asp:Label ID="lblzoneID" runat="server" Visible="false" Text='<%#Eval("ZoneID") %>'></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblSubZone" runat="server" Text='<%#Eval("SubZoneName") %>'></asp:Label>
                                <asp:Label ID="lblSubzoneID" runat="server" Visible="false" Text='<%#Eval("SubZoneID") %>'></asp:Label>
                            </td>

                            <td>
                                <asp:Label ID="lblcreatedby" runat="server" Text='<%#Eval("Name") %>'></asp:Label></td>
                            <td>
                                <asp:Label ID="lblCreate" runat="server" Text='<%#Convert.ToDateTime(Eval("Created")).ToString("MMMM dd, yyyy") %>'></asp:Label></td>
                           
                             <td>
                                <asp:Label ID="lblmodifiedby" runat="server" Text='<%#Eval("Name") %>'></asp:Label></td> 
                            <td>
                                <asp:Label ID="Label1" runat="server" Text='<%#Convert.ToDateTime(Eval("Modified")).ToString("MMMM dd, yyyy") %>'></asp:Label></td>
                            <td>
                                <asp:Label ID="lblStatus" runat="server" Text='<%#Convert.ToInt32(Eval("ZoneStatus"))==1?"Active":"Inactive"%>'></asp:Label></td>

                            <td>
                                <center><asp:ImageButton ID="ImgBtnEdit" runat="server" Width="24" Height="24"
                                             ImageUrl="~/Static/images/edit.png" CommandName="edit" CommandArgument='<%#Eval("ZoneID") %>' /></center>
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
    </div>
</asp:Content>
