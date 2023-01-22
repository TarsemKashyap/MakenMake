<%@ Page Title="Service Assessment properties" Language="C#" MasterPageFile="~/Pages/AdminMaster.Master" AutoEventWireup="true" CodeBehind="ServiceAssesment.aspx.cs" Inherits="MakeNMake.ServiceAssesment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript">
        function DeleteConfirmation(ele) {
            var getValue = confirm('Are you sure , you want to delete the record ?');
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
        <div class="panel-heading">
            <h3 class="panel-title paneltitle">Service Assesment</h3>
        </div>
        <div class="panel-body" style="padding-left: 0px;">
            <div class="col-md-12" style="padding: 0px;">
                <%--<asp:ValidationSummary ID="ValidationSummary1" ValidationGroup="service" ForeColor="Red" ShowModelStateErrors="true" ShowMessageBox="false" ShowSummary="true" ShowValidationErrors="true" runat="server" />--%>
                <div class="col-md-10 text-left linkBottom">
                    <div class="input-group input-group-sm">
                        <span class="input-group-addon">Services (Unlimited -(Domestic/Commercial)- Basic) </span>
                        <asp:DropDownList ID="ddlcategory" runat="server" AutoPostBack="true"
                            CssClass="form-control dropdown">
                        </asp:DropDownList>
                    </div>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please select Category"
                        ControlToValidate="ddlcategory" Display="Dynamic" ForeColor="Red" InitialValue="0"
                        ValidationGroup="service" SetFocusOnError="true"></asp:RequiredFieldValidator>
                </div>
                <div class="col-md-6 text-left linkBottom">
                    <div class="input-group input-group-sm">
                        <span class="input-group-addon" id="Span7">Field Name</span>
                        <asp:TextBox ID="txtservice" AutoComplete="off" ClientIDMode="Static"
                            placeholder="Enter Field Name" CssClass="form-control" aria-describedby="basic-addon1" runat="server"></asp:TextBox>
                    </div>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ValidationGroup="service" runat="server" ErrorMessage="Enter Fieldname"
                        ControlToValidate="txtservice" Display="Dynamic" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>

                </div>
                <div class="col-md-6 text-left linkBottom">
                    <div class="input-group input-group-sm">
                        <span class="input-group-addon" id="Span1">Validation For Field Name</span>
                        <asp:TextBox ID="txtValidation" AutoComplete="off" ClientIDMode="Static"
                            placeholder="Enter Field Validation" CssClass="form-control" aria-describedby="basic-addon1" runat="server"></asp:TextBox>
                    </div>
                   <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="service" runat="server" ErrorMessage="Enter Validation"
                        ControlToValidate="txtValidation" Display="Dynamic" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>--%>

                </div>
                <div class="col-md-6 text-left linkBottom">
                    <asp:Button ID="btnadd" runat="server" Text="Add" ValidationGroup="service" CssClass="btn btn-success" OnClick="btnadd_Click" />
                       <asp:Button ID="btnCancel" runat="server" Text="Cancel"  CssClass="btn btn-success" OnClick="btnCancel_Click"/>
                </div>
            </div>
        </div>
    </div>
    <div class="panel panel-success" id="divService" runat="server" visible="false">
        <div class="col-sm-12 clear" >
            <div class="table-responsive">
                <asp:HiddenField ID="hdnServiceID" runat="server" />
                <asp:Repeater ID="RptService" runat="server" OnItemCommand="RptService_ItemCommand">
                    <HeaderTemplate>
                        <table class="table table-hover  table-bordered  table-condensed" data-height="400">
                            <thead style="color: White; background-color: #6b9297;">
                                <tr>
                                    <th>Service</th>
                                    <th>Field Name</th>
                                    <th>Field Validation</th>
                                    <%--<th>ServiceType</th>
                                        <th>PlanType</th>--%>


                                    <th>Edit </th>
                                    <th>Delete</th>
                                </tr>
                            </thead>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tbody>
                            <tr>
                                <td>
                                    <asp:Label ID="Label1" runat="server" Text='<%#Eval("ServiceName") %>'></asp:Label></td>
                                <td>
                                    <asp:HiddenField ID="hdnID" runat="server" Value='<%#Eval("PropertyID") %>' />
                                    <asp:Label ID="lblName" runat="server" Text='<%#Eval("PropertyName") %>'></asp:Label></td>
                                <td>
                                    <asp:HiddenField ID="hdserviceid" runat="server" Value='<%#Eval("ServiceID") %>' />
                                    <asp:Label ID="lblvalidation" runat="server" Text='<%#Eval("Validation") %>'></asp:Label></td>
                                <%--<td>
                                        <asp:Label ID="lblType" runat="server" Text='<%#Eval("Servicetype") %>'></asp:Label></td>
                                    <td>
                                        <asp:Label ID="lblPlan" runat="server" Text='<%#Eval("PlanType") %>'></asp:Label></td>--%>
                                <td>
                                    <center>     <asp:ImageButton ID="ImgBtnEdit" runat="server" 
                                             ImageUrl="~/Static/images/edit.png" CommandName="edit" CommandArgument='<%#Eval("PropertyID") %>' /></center>
                                </td>
                                <td>
                                    <center> <asp:ImageButton ID="ImgBtnDelete" OnClientClick="javascript:return DeleteConfirmation(this);"
                                            runat="server" ImageUrl="~/Static/images/trash.png"
                                            CommandName="delete" CommandArgument='<%#Eval("ServiceID") %>' /></center>

                                </td>
                            </tr>
                        </tbody>
                    </ItemTemplate>
                    <FooterTemplate>
                        </table>
                    </FooterTemplate>
                </asp:Repeater>
            </div>
        </div>
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

</asp:Content>
