<%@ Page Title="Quotation" Language="C#" MasterPageFile="~/Pages/AdminMaster.Master" AutoEventWireup="true" CodeBehind="Quotation.aspx.cs" Inherits="MakeNMake.Pages.Quotation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="panel panel-success">
        <div class="panel-heading">
            <h3 class="panel-title paneltitle">Quotation</h3>
        </div>
        <div class="panel-body">
            <div class="panel panel-success">
                <div class="panel-heading">
                    <h3 class="panel-title paneltitle">Assessment Form Filled by Engineer</h3>
                </div>
            </div>
           
                <div class="col-sm-6 text-left linkBottom">
                    <div class="input-group input-group-sm">
                        <span class="input-group-addon" id="Span5">Engineer's Remarks</span>
                        <asp:TextBox ID="txtEngineerRemarks" TextMode="MultiLine"  style="resize:none;height:50px" 
                            CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="col-sm-6 text-left linkBottom">
                    <div class="input-group input-group-sm">
                        <span class="input-group-addon" id="Span1">Interested in Service </span>
                        <asp:TextBox ID="txtService" Enabled="false" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                </div>
            <asp:Repeater ID="RptAssesmentForm" runat="server">
                <ItemTemplate>
                    <div class="col-sm-6 text-left linkBottom">
                        <div class="input-group input-group-sm">
                            <span class="input-group-addon" id="Span7"><%#Eval("ServiceName") %></span>
                            <span class="input-group-addon" id="Span5"><%#Eval("PropertyName") %></span>
                            <asp:TextBox ID="txtPropertyValue" Enabled="false" CssClass="form-control" Text='<%#Eval("PropertyValue") %>' runat="server"></asp:TextBox>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
        <div class="panel-body">
            <div class="panel panel-success">
                <div class="panel-heading">
                    <h3 class="panel-title paneltitle">Fill Quotation For Client</h3>
                </div>
            </div>
            <div class="col-sm-12">
                <div class="col-sm-6 text-left linkBottom">
                    <div class="input-group input-group-sm">
                        <span class="input-group-addon" id="Span2">Quote Title</span>
                        <asp:TextBox ID="txtQuote"  CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtQuote"
                        ForeColor="Red" ValidationGroup="quote"
                        ErrorMessage="Enter Quote Title"></asp:RequiredFieldValidator>
                </div>
                <div class="col-sm-6 text-left linkBottom">
                    <div class="input-group input-group-sm">
                        <span class="input-group-addon" id="Span3">Price</span>
                        <asp:TextBox ID="txtPrice" onKeyPress="return ValidateNumber(event);"  CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtPrice"
                        ForeColor="Red" ValidationGroup="quote"
                        ErrorMessage="Enter Price"></asp:RequiredFieldValidator>
                </div>
                 <div class="col-sm-6 text-left linkBottom">
                    <div class="input-group input-group-sm">
                        <span class="input-group-addon" id="Span4">Consumables</span>
                        <asp:TextBox ID="txtConsumable"  CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtConsumable"
                        ForeColor="Red" ValidationGroup="quote"
                        ErrorMessage="Enter Consumables"></asp:RequiredFieldValidator>
                </div>
                 <%--<div class="col-sm-6 text-left linkBottom">
                    <div class="input-group input-group-sm">
                        <span class="input-group-addon" id="Span6">Payment Mode</span>
                        <asp:TextBox ID="txtPMode"  CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtPMode"
                        ForeColor="Red" ValidationGroup="quote"
                        ErrorMessage="Enter PaymentMode"></asp:RequiredFieldValidator>
                </div>--%>
                <div class="col-sm-6 text-left linkBottom">
                    <div class="input-group input-group-sm">
                        <span class="input-group-addon" id="Span8">Activation Time</span>
                        <asp:TextBox ID="txtactivation"  CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtactivation"
                        ForeColor="Red" ValidationGroup="quote"
                        ErrorMessage="Enter Activation Time"></asp:RequiredFieldValidator>
                </div>
                <div class="col-sm-12 text-left linkBottom">
                    <asp:Button ID="btnSubmit" runat="server" ValidationGroup="quote" CssClass="btn btn-success" OnClick="btnSubmit_Click" Text="Submit and Send Quote" />
                    <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-success" OnClick="btnCancel_Click" Text="Cancel" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
