<%@ Page Title="Add District" Language="C#" MasterPageFile="AdminMaster.Master" AutoEventWireup="true" CodeBehind="District.aspx.cs" Inherits="MakeNMake.Admin.District" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="panel panel-success">
        <div class="panel-heading">
            <h3 class="panel-title paneltitle">District</h3>
        </div>

        <div class="panel-body" style="padding-left:0px;">
            <%-- <asp:ValidationSummary ID="ValidationSummary1" ValidationGroup="user2" ShowSummary="true" ForeColor="Red" runat="server" />  --%>
            <div class="col-sm-6 text-left linkBottom">
                <div class="input-group input-group-sm">
                    <span class="input-group-addon" id="Span1">Country</span>
                    <asp:DropDownList ID="ddlCountry" runat="server" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control  dropdown"></asp:DropDownList>
                </div>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" InitialValue="0" runat="server" ControlToValidate="ddlCountry"
                    ErrorMessage="Select Country" ValidationGroup="user2" Display="Dynamic" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
            </div>
            <div class="col-sm-6 text-left linkBottom">
                <div class="input-group input-group-sm">
                    <span class="input-group-addon" id="Span2">State</span>
                    <asp:DropDownList ID="ddlstate" runat="server" CssClass="form-control  dropdown"></asp:DropDownList>
                </div>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" InitialValue="0" runat="server" ControlToValidate="ddlstate"
                    ErrorMessage="Select State" ValidationGroup="user2" Display="Dynamic" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
            </div>

            <div class="col-sm-6 text-left linkBottom linkBottom">
                <div class="input-group input-group-sm">
                    <span class="input-group-addon" id="Span3">District</span>
                    <asp:TextBox ID="txtDistrict" CssClass="form-control" placeholder="Enter District" runat="server"></asp:TextBox>
                </div>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtDistrict"
                    ErrorMessage="Enter District" ValidationGroup="user2" Display="Dynamic" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
            </div> 
            <div class="col-sm-12 text-left linkBottom linkBottom">
                 <asp:Button ID="btncity" runat="server" Text="Add" CssClass="btn btn-success" ValidationGroup="user2" OnClick="btncity_Click" />
                <asp:Button ID="btnAll" runat="server" Text="View All District" CssClass="btn btn-success" OnClick="btnAll_Click" />
                <asp:Button ID="btnCancel" OnClick="btnCancel_Click" runat="server" Text="Cancel" CssClass="btn btn-success" />

                </div>

        </div>
    </div>
</asp:Content>
