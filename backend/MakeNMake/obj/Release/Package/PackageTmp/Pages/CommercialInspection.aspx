<%@ Page Title="Commercial Inspection" Language="C#" MasterPageFile="AdminMaster.Master" AutoEventWireup="true" CodeBehind="CommercialInspection.aspx.cs" Inherits="MakeNMake.Pages.CommercialInspection" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .rd {
            background-color: #2fa4e7;
            border-color: #0EB6E7;
            background-image: none;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="panel panel-success">
        <div class="panel-heading">
            <h3 class="panel-title paneltitle">Commercial Inspection History</h3>
        </div>
        <div class="panel-body">
            <div id="Div1" class="panel-heading" runat="server" style="padding-left: 0px;">
                <div class="col-sm-6 text-left linkBottom" style="width: 300px">
                    <div class="input-group input-group-sm">
                        <span class="input-group-addon" id="Span31">Request ID</span>
                        <asp:TextBox ID="lblhreqserviceID" CssClass="form-control" Enabled="false" Width="150px" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="col-sm-6 text-left linkBottom" style="width: 300px">
                    <div class="input-group input-group-sm">
                        <span class="input-group-addon" id="Span32">Select Status</span>
                        <asp:DropDownList ID="ddlStatus" runat="server" Width="200px" CssClass="form-control  dropdown">
                            <asp:ListItem Value="0" Text="--Select Status--"></asp:ListItem>
                            <asp:ListItem Value="5" Text="Accept"></asp:ListItem>
                            <asp:ListItem Value="2" Text="Reject"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator32" runat="server" ControlToValidate="ddlStatus"
                        ErrorMessage="Please Select Status" InitialValue="0" ValidationGroup="changeStatus" Display="Dynamic" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>

                </div>
                <div class="col-sm-6 text-left linkBottom">
                    <div class="input-group input-group-sm">
                        <span class="input-group-addon" id="Span1">Reason</span>
                        <asp:TextBox ID="txtReason" AutoComplete="off" ClientIDMode="Static"
                            placeholder="Enter Reason" CssClass="form-control" aria-describedby="basic-addon1" runat="server"></asp:TextBox>
                    </div>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtReason"
                        ErrorMessage="Enter Reason" ValidationGroup="changeStatus" Display="Dynamic" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                </div>
            </div>

            <div class="col-sm-12 text-left linkBottom">
                <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" CssClass="btn btn-success" ValidationGroup="changeStatus" />
                <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" CssClass="btn btn-success" />
            </div>
        </div>
    </div>
</asp:Content>
