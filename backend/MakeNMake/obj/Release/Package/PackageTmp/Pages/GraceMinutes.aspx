<%@ Page Title="Grace Minutes" Language="C#" MasterPageFile="~/Pages/AdminMaster.Master" AutoEventWireup="true" CodeBehind="GraceMinutes.aspx.cs" Inherits="MakeNMake.Pages.GraceMinutes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="panel panel-success">
        <div class="panel-heading">
            <h3 class="panel-title paneltitle">Grace Time</h3>
        </div>

        <div class="panel-body" style="padding-left: 0px;">
            <div class="col-sm-12 text-left linkBottom">
                <div class="input-group input-group-sm">
                    <span class="input-group-addon" id="Span1">Add minutes</span>
                    <asp:TextBox ID="txtminutes" onkeypress="return ValidateNumber(event)" CssClass="form-control" MaxLength="2" runat="server"></asp:TextBox>
                </div>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtminutes"
                    ErrorMessage="Enter Minutes" ValidationGroup="minutes" Display="Dynamic" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
            </div>
            <div class="col-sm-12 text-left linkBottom linkBottom">
                
            <asp:Button ID="btnadd" runat="server" Text="Add"  CssClass="btn btn-success" ValidationGroup="minutes" OnClick="btnadd_Click" />
                <asp:Button ID="btnReset" runat="server" Text="Edit"  CssClass="btn btn-success" Visible="false" OnClick="btnReset_Click" />
                <asp:Button ID="btnCancel" runat="server" Text="Cancel"  CssClass="btn btn-success" OnClick="btnCancel_Click" />
            </div>
        </div>
    </div>
</asp:Content>
