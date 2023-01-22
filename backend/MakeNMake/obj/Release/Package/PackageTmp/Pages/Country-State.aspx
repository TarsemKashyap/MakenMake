<%@ Page Title="Country State" Language="C#" MasterPageFile="AdminMaster.Master" AutoEventWireup="true" CodeBehind="Country-State.aspx.cs" Inherits="MakeNMake.Admin.CountryState" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="panel panel-success">
        <div class="panel-heading">
            <h3 class="panel-title paneltitle">Country-State</h3>
        </div>
        <div class="panel-body" style="padding-left: 0px;">
            <div class="col-sm-12" style="padding: 0px;">
                <%--<asp:ValidationSummary ID="ValidationSummary1" ValidationGroup="user" ForeColor="Red" ShowModelStateErrors="true" ShowMessageBox="false" ShowSummary="true" ShowValidationErrors="true" runat="server" />--%>
                <div class="col-sm-3 text-left linkBottom linkBottom">
                    <div class="input-group input-group-sm">
                        <span class="input-group-addon" >Country</span>
                        <asp:TextBox ID="txtcountry" CssClass="form-control" placeholder="Enter Country" runat="server"></asp:TextBox>
                    </div>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtcountry"
                        ErrorMessage="Enter Country" ValidationGroup="user" Display="Dynamic" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="col-sm-3 ">
                <asp:Button ID="btnSubmit" runat="server" Text="Add" CssClass="btn btn-success" ValidationGroup="user" OnClick="btnSubmit_Click" />
                <asp:Button ID="btnCancel" Visible="false" OnClick="btnCancel_Click" runat="server" Text="Cancel" CssClass="btn btn-success" />
            </div>
        </div>



        <div class="panel-body" style="padding-left: 0px;">
            <div class="col-sm-12" style="padding: 0px;">
                <div class="col-sm-6 text-left linkBottom">
                    <div class="input-group input-group-sm">
                        <span class="input-group-addon" id="Span4">Country</span>
                        <asp:DropDownList ID="ddlcountry" runat="server" OnSelectedIndexChanged="ddlcountry_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control  dropdown"></asp:DropDownList>
                    </div>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" InitialValue="0" runat="server" ControlToValidate="ddlcountry"
                        ErrorMessage="Select Country" ValidationGroup="user1" Display="Dynamic" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                </div>
                <div class="col-sm-6 text-left linkBottom linkBottom">
                    <div class="input-group input-group-sm">
                        <span class="input-group-addon" id="Span1">State</span><asp:TextBox ID="txtstate" CssClass="form-control" placeholder="Enter State" runat="server"></asp:TextBox>
                    </div>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtstate"
                        ErrorMessage="Enter State" ValidationGroup="user1" Display="Dynamic" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="col-sm-10 ">
                <asp:Button ID="btnstate" runat="server" Text="Add" CssClass="btn btn-success" ValidationGroup="user1" OnClick="btnstate_Click" />
                <asp:Button ID="btnAll" runat="server" Text="View All States" CssClass="btn btn-success" OnClick="btnAll_Click" />
                <asp:Button ID="btnCancels" OnClick="btnCancels_Click" runat="server" Text="Cancel" CssClass="btn btn-success" />
            </div>
        </div>



    </div>


</asp:Content>
