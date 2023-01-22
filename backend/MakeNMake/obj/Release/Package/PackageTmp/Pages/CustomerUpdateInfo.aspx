﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/AdminMaster.Master" AutoEventWireup="true" CodeBehind="CustomerUpdateInfo.aspx.cs" Inherits="MakeNMake.Pages.CustomerUpdateInfo" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/UserControl/UpdateUserInfo.ascx" TagPrefix="uc1" TagName="UpdateUserInfo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function checkDate(sender, args) {
            var selectedDate = sender._selectedDate;
            var todayDate = new Date();
            if (selectedDate > todayDate) {
                alert("You cannot select a future date!");
            }
            else if (todayDate.getFullYear() - selectedDate.getFullYear() < 18) {

                alert("You must be 18 year older to use this application");
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="panel panel-success">
        <div class="panel-heading">
            <h3 class="panel-title paneltitle">Update Client Info</h3>
        </div>
        <div id="info" runat="server" class="panel-body" style="padding-left:0px;">
            <%-- <asp:ValidationSummary ID="ValidationSummary1" ValidationGroup="service" ShowSummary="true" ForeColor="Red" runat="server" />--%>
            <div class="col-sm-6 text-left linkBottom">
                <div class="input-group input-group-sm">
                    <span class="input-group-addon" id="Span9">Name Of Customer</span>
                    <asp:Label ID="lblName" runat="server" CssClass="form-control"></asp:Label>
                </div>
            </div>
            <div class="col-sm-6 text-left linkBottom">
                <div class="input-group input-group-sm">
                    <span class="input-group-addon" id="Span10">EmailID</span>
                    <asp:Label ID="lblemailid" runat="server" CssClass="form-control"></asp:Label>
                </div>
            </div>
            <div class="col-sm-6 text-left linkBottom">
                <div class="input-group input-group-sm">
                    <span class="input-group-addon" id="Span1">MobileNumber</span>
                    <asp:TextBox ID="txtMobileNumber" onkeypress="return ValidateNumber(event)" CssClass="form-control" MaxLength="10" placeholder="Enter Mobile Number" runat="server"></asp:TextBox>
                </div>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="service"
                    runat="server" ErrorMessage="Enter Mobile Number"
                    ControlToValidate="txtMobileNumber" Display="Dynamic" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" SetFocusOnError="true" runat="server" ControlToValidate="txtMobileNumber"
                    ErrorMessage="Enter Valid Mobile No" Display="Dynamic" ForeColor="Red" ValidationGroup="service" ValidationExpression="[0-9]{10}"></asp:RegularExpressionValidator>

            </div>
            <div id="dvAlternateMobile" runat="server">
                     <div class="col-sm-6 text-left linkBottom">
                    <div class="input-group input-group-sm">
                        <span class="input-group-addon" id="Span15">Alternate Mobile Number 1</span>
                        <asp:TextBox ID="txtAltMob1" MaxLength="10" placeholder="Mobile Number" onkeypress="return ValidateNumber(event)" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                   </div>
                      <div class="col-sm-6 text-left linkBottom">
                    <div class="input-group input-group-sm">
                        <span class="input-group-addon" id="Span16">Alternate Mobile Number 2</span>
                        <asp:TextBox ID="txtAltMob2" MaxLength="10" placeholder="Mobile Number" onkeypress="return ValidateNumber(event)" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                   </div>
                      <div class="col-sm-6 text-left linkBottom">
                    <div class="input-group input-group-sm">
                        <span class="input-group-addon" id="Span17">Alternate Mobile Number 3</span>
                        <asp:TextBox ID="txtAltMob3" MaxLength="10" placeholder="Mobile Number" onkeypress="return ValidateNumber(event)" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                   </div>
                      <div class="col-sm-6 text-left linkBottom">
                    <div class="input-group input-group-sm">
                        <span class="input-group-addon" id="Span18">Alternate Mobile Number 4</span>
                        <asp:TextBox ID="txtAltMob4" MaxLength="10" placeholder="Mobile Number" onkeypress="return ValidateNumber(event)" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                   </div>
                </div>
            <%-- <div class="col-sm-6 text-left linkBottom">
                    <div class="input-group input-group-sm">
                        <span class="input-group-addon" id="Span7">Gender</span>
                        <asp:DropDownList ID="ddlGender" CssClass="form-control dropdown" runat="server">
                            <asp:ListItem Text="--Select Gender--" Value="0"></asp:ListItem>
                            <asp:ListItem Text="Male" Value="M"></asp:ListItem>
                            <asp:ListItem Text="Female" Value="F"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ValidationGroup="service" runat="server"
                        ErrorMessage="Enter Gender"
                        ControlToValidate="ddlGender"
                        InitialValue="0"  Display="Dynamic" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                </div>--%>
            <div class="col-sm-6 text-left linkBottom">
                <div class="input-group input-group-sm">
                    <span class="input-group-addon" id="Span8">Date of Birth</span>
                    <asp:TextBox ID="txtDob" AutoComplete="off" ClientIDMode="Static"
                        placeholder="Enter Date of Birth" CssClass="form-control" aria-describedby="basic-addon1" runat="server"></asp:TextBox>
                </div>
                <asp:CalendarExtender ID="CalendarExtender1" OnClientDateSelectionChanged="checkDate" TargetControlID="txtDob" Format="MM/dd/yyyy" SelectedDate='<%#DateTime.Now %>' runat="server"></asp:CalendarExtender>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator7"
                    runat="server" ValidationGroup="service" Display="Dynamic" ForeColor="Red"
                    ControlToValidate="txtDob" SetFocusOnError="true" ErrorMessage="Enter Date of Birth"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" SetFocusOnError="true" runat="server" ControlToValidate="txtDob"
                    ErrorMessage="Enter Valid date" Display="Dynamic" ForeColor="Red" ValidationGroup="service" ValidationExpression="^(0[1-9]|1[012])[- /.](0[1-9]|[12][0-9]|3[01])[- /.](19|20)\d\d+$"></asp:RegularExpressionValidator>

            </div>
             <div class="col-sm-6 text-left linkBottom">
        <div class="input-group input-group-sm">
            <span class="input-group-addon" id="Span5">Locality</span>
            <asp:TextBox ID="txtaddresslocality" AutoComplete="off" TextMode="MultiLine" MaxLength="300"
                placeholder="Enter Locality" CssClass="form-control" aria-describedby="basic-addon1" runat="server"></asp:TextBox>
        </div>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator6"
            runat="server" ValidationGroup="service"  Display="Dynamic" ForeColor="Red"
            ControlToValidate="txtaddresslocality" SetFocusOnError="true" ErrorMessage="Enter Your Locality"></asp:RequiredFieldValidator>
    </div>
    <div class="col-sm-6 text-left linkBottom">
        <div class="input-group input-group-sm">
            <span class="input-group-addon" id="Span5">Street</span>
            <asp:TextBox ID="txtstreet" AutoComplete="off" TextMode="MultiLine" MaxLength="300"
                placeholder="Enter Street" CssClass="form-control" aria-describedby="basic-addon1" runat="server"></asp:TextBox>
        </div>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator9"
            runat="server" ValidationGroup="service"  Display="Dynamic" ForeColor="Red"
            ControlToValidate="txtstreet" SetFocusOnError="true" ErrorMessage="Enter Your Street"></asp:RequiredFieldValidator>
    </div>
            <div class="col-sm-6 text-left linkBottom">
                <div class="input-group input-group-sm">
                    <span class="input-group-addon" id="Span2">Country</span>
                    <asp:DropDownList ID="ddlCountry" AutoPostBack="true" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged" CssClass="form-control dropdown" runat="server">
                    </asp:DropDownList>
                </div>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="service" runat="server"
                    ErrorMessage="Enter Country"
                    ControlToValidate="ddlCountry" Display="Dynamic" InitialValue="0" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
            </div>
            <%--<div class="col-sm-6 text-left linkBottom">
                <div class="input-group input-group-sm">
                    <span class="input-group-addon" id="Span5">Address</span>
                    <asp:TextBox ID="txtaddress" AutoComplete="off" TextMode="MultiLine" MaxLength="300"
                        placeholder="Enter Address" CssClass="form-control" aria-describedby="basic-addon1" runat="server"></asp:TextBox>
                </div>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5"
                    runat="server" ValidationGroup="service" Display="Dynamic" ForeColor="Red"
                    ControlToValidate="txtaddress" SetFocusOnError="true" ErrorMessage="Enter Address"></asp:RequiredFieldValidator>
            </div>--%>

            <div class="col-sm-6 text-left linkBottom">
                <div class="input-group input-group-sm">
                    <span class="input-group-addon" id="Span3">State</span>
                    <asp:DropDownList ID="ddlState" AutoPostBack="true" OnSelectedIndexChanged="ddlState_SelectedIndexChanged" CssClass="form-control dropdown" runat="server">
                    </asp:DropDownList>
                </div>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="service" runat="server"
                    ErrorMessage="Enter State"
                    ControlToValidate="ddlState" Display="Dynamic" InitialValue="0" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
            </div>
            <div class="col-sm-6 text-left linkBottom">
                <div class="input-group input-group-sm">
                    <span class="input-group-addon" id="Span6">District</span>
                    <asp:DropDownList ID="ddlDistrict" CssClass="form-control dropdown" AutoPostBack="true" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged" runat="server">
                    </asp:DropDownList>
                </div>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ValidationGroup="service" runat="server"
                    ErrorMessage="Enter District"
                    ControlToValidate="ddlDistrict" InitialValue="0" Display="Dynamic" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
            </div>
            <div class="col-sm-6 text-left linkBottom">
                <div class="input-group input-group-sm">
                    <span class="input-group-addon" id="Span4">City</span>
                    <asp:DropDownList ID="ddlCity" CssClass="form-control dropdown" runat="server">
                    </asp:DropDownList>
                </div>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ValidationGroup="service" runat="server"
                    ErrorMessage="Enter City"
                    ControlToValidate="ddlCity" InitialValue="0" Display="Dynamic" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
            </div>
            <div class="col-sm-6 text-left linkBottom">
                <asp:Button ID="btnUPdateInfo" runat="server" OnClick="btnUPdateInfo_Click" ClientIDMode="Static"
                    ValidationGroup="service" Text="Save" CssClass="btn btn-success" />
                <asp:Button ID="btnCancel" ClientIDMode="Static" CssClass="btn btn-success" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
            </div>
        </div>
        <div id="bookAppoinment" visible="false" runat="server" class="panel-body">
            <div class="col-md-4 text-left linkBottom">
                <div class="input-group input-group-sm">
                    <span class="input-group-addon">Available Date: 
                    </span>
                    <asp:TextBox ID="txtdate" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:TextBox>
                    <asp:CalendarExtender ID="CalendarExtender2" PopupPosition="BottomRight" Animated="false"
                        SelectedDate='<%#DateTime.Now %>' TargetControlID="txtdate" runat="server">
                    </asp:CalendarExtender>
                </div>
            </div>
            <div class="col-md-4 text-left linkBottom">
                <div class="input-group input-group-sm">
                    <span class="input-group-addon">Available  Time: 
                    </span>
                    <asp:TextBox ID="txttime" onkeypress="return isNumberKey(event)" placeholder="in hrs" CssClass="form-control" runat="server"></asp:TextBox>
                </div>
            </div>
            <div class="col-md-4 text-left linkBottom">
                <asp:Button ID="btnSubmit" ClientIDMode="Static" CssClass="btn btn-success" runat="server" Text="Fix Appointment" OnClick="btnSubmit_Click" />
            </div>
        </div>
    </div>

</asp:Content>
