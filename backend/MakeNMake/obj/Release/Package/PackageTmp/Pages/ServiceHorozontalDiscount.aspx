<%@ Page Title="Horizontal Discount" Language="C#" MasterPageFile="AdminMaster.Master" AutoEventWireup="true" CodeBehind="ServiceHorozontalDiscount.aspx.cs" Inherits="MakeNMake.Admin.ServiceHorozontalDiscount" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../Scripts/jquery-2.0.1.min.js"></script>
    <script type="text/javascript">
        $(document).on('click', '#btnAdd', function (e) {
            var val = $("#txtdiscount").val();
            if (val != "") {
                if (parseInt(val) > 100) {
                    alert("Please enter discount less than 100");
                    e.preventDefault();
                }
            }

        });
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
        <div class="panel-heading">
            <h3 class="panel-title paneltitle">Service Horizontal Discount</h3>
        </div>
        <div class="panel-body" style="padding-left:0px;">
            <div class="col-sm-12" style="padding-left:0px;">
                <%--<asp:ValidationSummary ID="ValidationSummary1" ValidationGroup="service" ShowSummary="true" ForeColor="Red" runat="server" />--%>

                <div class="col-sm-6 text-left linkBottom">
                    <div class="input-group input-group-sm">
                        <span class="input-group-addon" id="Span4">Select Quantity </span>
                        <asp:DropDownList ID="ddlQuantity" CssClass="form-control dropdown" OnSelectedIndexChanged="ddlPlan_SelectedIndexChanged" AutoPostBack="true" runat="server">
                            <asp:ListItem Text="--Select Service Quantity--" Value="0"></asp:ListItem>
                            <%--<asp:ListItem Text="Variable Quantity" Value="1"></asp:ListItem>--%>
                            <asp:ListItem Text="Fixed Quantity" Value="2"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ValidationGroup="service" runat="server" ErrorMessage="Select Service Quantity"
                        ControlToValidate="ddlQuantity"
                        InitialValue="0"  Display="Dynamic" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                </div>
                <div id="dvFixed" runat="server" visible="false" class="col-sm-6 text-left linkBottom">
                    <div class="input-group input-group-sm">
                        <span class="input-group-addon" id="Span8">Service No. </span>
                        <asp:TextBox ID="txtquantityfrom" AutoComplete="off" MaxLength="2" onKeyPress="return ValidateNumber(event);"
                            placeholder="Enter Service Number" CssClass="form-control" aria-describedby="basic-addon1" runat="server"></asp:TextBox>
                    </div>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7"
                        runat="server" ValidationGroup="service"  Display="Dynamic" ForeColor="Red"
                        ControlToValidate="txtquantityfrom" SetFocusOnError="true" ErrorMessage="Enter the Service Number"></asp:RequiredFieldValidator>
                </div>

                <div id="dvVariableFrom" runat="server" visible="false" class="col-sm-6 text-left linkBottom">
                    <div class="input-group input-group-sm">
                        <span class="input-group-addon" id="Span5">Service Number From</span>
                        <asp:TextBox ID="txtQuanFrom" AutoComplete="off" MaxLength="2" onKeyPress="return ValidateNumber(event);"
                            placeholder="Enter Service Number From" CssClass="form-control" aria-describedby="basic-addon1" runat="server"></asp:TextBox>
                    </div>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5"
                        runat="server" ValidationGroup="service"  Display="Dynamic" ForeColor="Red"
                        ControlToValidate="txtQuanFrom" SetFocusOnError="true" ErrorMessage="Enter the Service Number From"></asp:RequiredFieldValidator>
                </div>
                <div id="dvVariableTo" runat="server" visible="false" class="col-sm-6 text-left linkBottom">
                    <div class="input-group input-group-sm">
                        <span class="input-group-addon" id="Span6">Service Number To</span>
                        <asp:TextBox ID="txtQuantityTo" AutoComplete="off" MaxLength="2" onKeyPress="return ValidateNumber(event);"
                            placeholder="Enter Service Number To" CssClass="form-control" aria-describedby="basic-addon1" runat="server"></asp:TextBox>
                    </div>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6"
                        runat="server" ValidationGroup="service"  Display="Dynamic" ForeColor="Red"
                        ControlToValidate="txtQuantityTo" SetFocusOnError="true" ErrorMessage="Enter the Service Number To"></asp:RequiredFieldValidator>
                </div>
                <div class="col-sm-6 text-left linkBottom">
                    <div class="input-group input-group-sm">
                        <span class="input-group-addon" id="Span3">Discount</span>
                        <asp:TextBox ID="txtdiscount" runat="server" ClientIDMode="Static" MaxLength="3" CssClass="form-control" aria-describedby="Span3"
                            AutoComplete="off"></asp:TextBox>
                    </div>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"  Display="Dynamic" ForeColor="Red" ErrorMessage="Enter Discount" ValidationGroup="service" ControlToValidate="txtdiscount"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"  Display="Dynamic" ForeColor="Red"
                        ErrorMessage="Enter valid discount" ValidationGroup="service" ValidationExpression="^-?\d*\.?\d*" ControlToValidate="txtdiscount"></asp:RegularExpressionValidator>

                </div>
                <div class="col-sm-6 text-left linkBottom">
                    <div class="input-group input-group-sm">
                        <span class="input-group-addon" id="Span2">Status</span>
                        

                        <asp:RadioButtonList ID="ddlStatus" style="margin-left:10px;margin-top:3px;" 
                             RepeatLayout="Table" RepeatDirection="Horizontal" runat="server">
                                <asp:ListItem Text="Active" Value="true" style="margin-right: 20px"></asp:ListItem>
                                <asp:ListItem Text="Inactive" Value="false"></asp:ListItem>
                            </asp:RadioButtonList>


                    </div>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="service" runat="server" ErrorMessage="Select Status"
                        ControlToValidate="ddlStatus"
                        InitialValue="0"  Display="Dynamic" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                </div>
                <div class="col-sm-12 text-left linkBottom">
            <asp:Button ID="btnAdd" runat="server" ClientIDMode="Static" CssClass="btn btn-success" Text="Add" OnClick="btnAdd_Click" ValidationGroup="service" />            
<asp:Button ID="btnCancel" OnClick="btnCancel_Click" runat="server" Text="Cancel"  CssClass="btn btn-success"  />
                    </div>

            </div>
        </div>
    </div>
    <div class="panel panel-success">
        <div class="panel-body">
            <div class="col-sm-12 clear" style="padding-left:0px;">
                <div class="table-responsive">
                    <asp:HiddenField ID="hdnServiceID" runat="server" />
                    <asp:Repeater ID="RptService" runat="server" OnItemCommand="RptService_ItemCommand">
                        <HeaderTemplate>
                            <table class="table table-hover  table-bordered  table-condensed" data-height="400">
                                <thead style="color: White; background-color: #6b9297;">
                                    <tr>
                                        <th>Service Number </th>
                                        <th>Discount (in %)</th>
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
                                        <asp:HiddenField ID="hdnID" runat="server" Value='<%#Eval("DisID") %>' />
                                        <asp:Label ID="lblquantFrom" runat="server" Text='<%#Eval("ServiceQuantity") %>'></asp:Label></td>
                                    <td>
                                        <asp:Label ID="lblDiscount" runat="server" Text='<%#Eval("Discount") %>'></asp:Label></td>
                                    <td>
                                        <asp:Label ID="lblStatus" runat="server" Text='<%# Convert.ToInt32(Eval("Status"))==1?"Active":"InActive"%>'></asp:Label></td>
                                    <td >
                                    <center>    <asp:ImageButton ID="ImgBtnEdit" runat="server"
                                             ImageUrl="~/Static/images/edit.png" CommandName="edit" CommandArgument='<%#Eval("DisID") %>' /></center>
                                    </td>
                                    <td>
                                    <center>    <asp:ImageButton ID="ImgBtnDelete" OnClientClick="javascript:return DeleteConfirmation(this);"
                                            runat="server" ImageUrl="~/Static/images/trash.png"
                                            CommandName="delete" CommandArgument='<%#Eval("DisID") %>' /></center>
                                    </td>
                            </tbody>
                        </ItemTemplate>
                        <FooterTemplate>
                            </table>
                        </FooterTemplate>
                    </asp:Repeater>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
