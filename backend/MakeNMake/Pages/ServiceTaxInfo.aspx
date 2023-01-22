<%@ Page Title="Overhead on services" Language="C#" MasterPageFile="~/Pages/AdminMaster.Master" AutoEventWireup="true" CodeBehind="ServiceTaxInfo.aspx.cs" Inherits="MakeNMake.Pages.ServiceTaxInfo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="panel panel-success">
        <div runat="server" id="divconsumer" class="panel-heading">
            <h3 class="panel-title paneltitle">Overhead on services</h3>
        </div>

        <div class="panel-body" style="padding-left: 0px;">
            <div class="col-sm-12" style="padding-left: 0px;">
                <%-- <asp:ValidationSummary ID="ValidationSummary1" ValidationGroup="user" ForeColor="Red" ShowModelStateErrors="true" ShowMessageBox="false" ShowSummary="true" ShowValidationErrors="true" runat="server" />--%>
                <div class="col-sm-6 text-left linkBottom linkBottom">
                    <div class="input-group input-group-sm">
                        <span class="input-group-addon">Property Name</span>
                        <asp:TextBox ID="txtServiceName" CssClass="form-control" onkeypress="return ValidateCharacters(event)" placeholder="Property Type" runat="server"></asp:TextBox>
                    </div>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtServiceName"
                        ErrorMessage="Enter Property Name" ValidationGroup="user" Display="Dynamic" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                </div>
                <div class="col-sm-6 text-left linkBottom linkBottom">
                    <div class="input-group input-group-sm">
                        <span class="input-group-addon">Property Description</span>
                        <asp:TextBox ID="txtDescription" CssClass="form-control" placeholder="Description" runat="server"></asp:TextBox>
                    </div>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtDescription"
                        ErrorMessage="Enter Property Description" ValidationGroup="user" Display="Dynamic" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                </div>
                <div class="col-sm-6 text-left linkBottom linkBottom">
                    <div class="input-group input-group-sm">
                        <span class="input-group-addon">Value</span>
                        <%--onkeypress="return ValidateNumber(event)"--%>
                        <asp:TextBox ID="txtValue" CssClass="form-control" placeholder="Value" runat="server"></asp:TextBox>
                        <span class="input-group-addon">in %</span>
                    </div>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtValue"
                        ErrorMessage="Enter Property Value" ValidationGroup="user" Display="Dynamic" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="Regex1" runat="server" ForeColor="Red" ValidationExpression="((\d+)((\.\d{1,2})?))$"
                        ErrorMessage="Please enter valid integer or decimal number with 2 decimal places."
                        ControlToValidate="txtValue" />
                </div>
                <div class="col-sm-12">

                    <asp:Button ID="btnSubmit" runat="server" Text="Add" CssClass="btn btn-success" ValidationGroup="user" OnClick="btnSubmit_Click" />
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-success" OnClick="btnCancel_Click" />
                    <asp:HiddenField ID="hd_overheadId" runat="server" />
                </div>
            </div>

        </div>
         <div class="panel-body" style="padding-left: 0px;">
            <div class="col-sm-12">

                <asp:Repeater ID="rp_SericeTaxs" runat="server" OnItemCommand="rp_SericeTaxs_ItemCommand">
                    <HeaderTemplate>
                        <table class="table table-hover  table-bordered  table-condensed" data-height="400">
                            <thead style="color: White; background-color: #6b9297;">
                                <tr>
                                    <th>Property</th>
                                    <th>Description</th>
                                    <th>Value</th>
                                    <th>Created On</th>
                                    <th>Modified On</th>
                                    <th>Edit</th>
                                    <th>Delete</th>
                                </tr>
                            </thead>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tbody>
                            <tr>
                                <td>
                                    <asp:HiddenField ID="hd_propertyId" runat="server" Value='<%#Eval("OverheadID") %>' />
                                    <asp:Label ID="lb_propertyName" runat="server" Text='<%#Eval("Property") %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lb_description" runat="server" Text='<%#Eval("Descriiption") %>'>

                                    </asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lbvalue" runat="server" Text='<%#string.Format ("{0}{1}",Eval("Value")," %") %>'></asp:Label>
                                </td>
                             <%--   <td>
                                    <asp:Label ID="lbcreatedby" runat="server" Text='<%#Eval("CreatedBy") %>'></asp:Label>
                                </td>--%>
                                <td>
                                    <asp:Label ID="lbCreated" runat="server" Text='<%#Eval("Created") %>'></asp:Label>
                                </td>
                             <%--   <td>
                                    <asp:Label ID="lbmodifiedby" runat="server" Text='<%#Eval("ModifiedBy") %>'></asp:Label>
                                </td>--%>
                                <td>
                                    <asp:Label ID="lbmodified" runat="server" Text='<%#Eval("Modified") %>'></asp:Label>
                                </td>
                                <td>
                                    <center>   <asp:ImageButton ID="ImgBtnEdit" runat="server" Width="24" Height="24"
                                             ImageUrl="~/Static/images/edit.png" CommandName="edit" CommandArgument='<%#Eval("OverheadID") %>' /></center>
                                </td>
                                <td>
                                    <center>   <asp:ImageButton ID="ImgDelete" runat="server" Width="24" Height="24"
                                             ImageUrl="~/Static/images/trash.png" CommandName="delete" CommandArgument='<%#Eval("OverheadID") %>' /></center>
                                </td>
                            </tr>
                        </tbody>
                    </ItemTemplate>

                    <FooterTemplate>
                        </table>
                    </FooterTemplate>
                </asp:Repeater>
            </div>
             </div>    </div>
</asp:Content>
