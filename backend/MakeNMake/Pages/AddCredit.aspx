<%@ Page Title="Add Credit" Language="C#" MasterPageFile="~/Pages/AdminMaster.Master" AutoEventWireup="true" CodeBehind="AddCredit.aspx.cs" Inherits="MakeNMake.AddCredit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../Scripts/jquery-2.0.1.min.js"></script>
    <script type="text/javascript">

        $(document).ready(function () {
            $(window).keydown(function (event) {
                if (event.keyCode == 13) {
                    event.preventDefault();
                    return false;
                }
            });
        });

        function PageReload() {
            var getValue = confirm('No Record Found ');
            if (getValue) {
                window.location.href = window.location.href;
            }
            else {
                window.location.href = window.location.href;
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="panel panel-success" id="divlist" runat="server" visible="false">

        <div runat="server" id="divuser" class="panel-heading">
            <h3 class="panel-title paneltitle">Add Credit</h3>
        </div>
        <div runat="server" id="divedituser" visible="false" class="panel-heading">
            <h3 class="panel-title paneltitle">Edit User</h3>
        </div>

        <div class="panel-body" style="padding-left:0px;">

            <div class="col-sm-12" style="padding-left:0px;">
                <div class="col-sm-6 text-left linkBottom">
                    <div class="input-group input-group-sm">
                        <span class="input-group-addon" id="Span1">Name</span>
                        <asp:Label ID="lblname" runat="server" CssClass="form-control"></asp:Label>
                    </div>
                </div>

                <div class="col-sm-6 text-left linkBottom">
                    <div class="input-group input-group-sm">
                        <span class="input-group-addon" id="Span2">Email ID</span>
                        <asp:Label ID="Lblemail" runat="server" CssClass="form-control"></asp:Label>

                    </div>
                </div>

                <div class="col-sm-6 text-left linkBottom">
                    <div class="input-group input-group-sm">
                        <span class="input-group-addon" id="Span3">Amount</span>
                        <asp:TextBox ID="txtamt" placeholder="Amount" onkeypress="return ValidateNumber(event)" CssClass="form-control" runat="server"></asp:TextBox>

                    </div>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtamt"
                        ErrorMessage="Enter Amount" ValidationGroup="user" Display="Dynamic" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                </div>
                 <div class="col-sm-6 text-left linkBottom" style="visibility:hidden">
                    <div class="input-group input-group-sm">
                        <span class="input-group-addon" id="Span4" >Mobile Number</span>
                        <asp:Label ID="txtMobileNumber" placeholder="Amount" CssClass="form-control" runat="server" Visible="false"></asp:Label>

                    </div>
                    
                </div>


            </div>
        </div>
        <div class="panel-footer" style="padding-left:15px;">
            <asp:Button ID="btnSubmit" runat="server" Text="Add Amount" CssClass="btn btn-success" ValidationGroup="user" OnClick="btnSubmit_Click" />
            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-success" />
        </div>
    </div>
    <div class="panel panel-success">
        <div class="panel-heading">
            <h3 class="panel-title paneltitle"></h3>
        </div>
        <div class="panel-body table-responsive">
            <div class="input-group input-group-xs">

                <asp:TextBox ID="txtSearchclient" placeholder="Search User By Name,EmailID Or Mobile Number" Width="400px"
                    CssClass="form-control" runat="server"></asp:TextBox>
                <asp:Button ID="Button1" runat="server" Text="Search" Style="margin-left: 10px;"
                    CssClass="btn btn-success" OnClick="Button1_Click" />
            </div>
            <div id="Div1" runat="server" visible="false" class="col-sm-6 text-left linkBottom" style="padding-left: 0px;">

                <br />
                <div class="input-group input-group-sm">


                    <span class="input-group-addon" id="Span4">Select number of items to be displayed per page </span>

                    <asp:DropDownList ID="ddlIndex" CssClass="form-control dropdown" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlIndex_SelectedIndexChanged">
                    </asp:DropDownList>
                </div>
            </div>

            <div class="panel-body table-responsive" id="divClientList" style="padding-left: 0px;" runat="server">

                <asp:HiddenField ID="hdnuserID" runat="server" />
                <asp:Repeater ID="Rptclient" runat="server" OnItemCommand="Rptclient_ItemCommand">
                    <HeaderTemplate>
                        <table class="table table-hover  table-bordered  table-condensed" data-height="400">
                            <thead style="color: White; background-color: #6b9297;">
                                <tr>
                                    <th>Name</th>
                                    <th>EmailID</th>
                                    <th>Mobile Number</th>
                                    <th>Total Amount in Wallet</th>
                                   <%-- <th>Service Type</th>
                                    <th>Service Category</th>
                                    <th>Service Plan</th>--%>
                                    <th>Edit</th>
                                </tr>
                            </thead>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tbody>
                            <tr>
                                <td>
                                    <asp:HiddenField ID="hdnID" runat="server" Value='<%#Eval("UserID") %>' />

                                    <asp:Label ID="Label1" runat="server" Text='<%#Eval("Name") %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblEmailID" runat="server" Text='<%#Eval("EmailID") %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblmobilenumber" runat="server" Text='<%#Eval("MobileNumber") %>'></asp:Label></td>
                                <td>
                                    <asp:Label ID="lblamt" runat="server" Text='<%#Eval("Amount") %>'></asp:Label></td>
                               <%-- <td>
                                    <asp:Label ID="lbltype" runat="server" Text='<%#Eval("Type") %>'></asp:Label></td>
                                <td>
                                    <asp:Label ID="lblcategory" runat="server" Text='<%#Eval("Category") %>'></asp:Label></td>
                                <td>
                                    <asp:Label ID="lblplan" runat="server" Text='<%#Eval("ServicePlan") %>'></asp:Label></td>--%>
                                <td>
                                    <%--<asp:Button ID="btnaddcredit" runat="server" CommandName="AddCredit" />--%>
                                    <asp:ImageButton ID="ImgBtnEdit" runat="server" Width="24" Height="24"
                                        ImageUrl="~/Static/images/edit.png" CommandName="AddCredit" CommandArgument='<%#Eval("UserID") %>' />
                                </td>
                            </tr>
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
