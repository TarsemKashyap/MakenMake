<%@ Page Title="Package Selection" Language="C#" MasterPageFile="AdminMaster.Master" AutoEventWireup="true" CodeBehind="PackageSelection.aspx.cs" Inherits="MakeNMake.CustomerCare.PackageSelection" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="act" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../Scripts/jquery-2.0.1.min.js"></script>
    <script src="../Static/js/validation.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#btnCheckOtp").click(function (e) {
                if ($("#txtVOtp").val() == "") {
                    alert("Please Enter OTP");
                    e.preventDefault();
                }
            });

        });
        function checkDate(sender, args) {
            if (sender._selectedDate.format("dd/MM/yyyy") == new Date().format("dd/MM/yyyy")) {
                sender._textbox.set_Value("");
                alert("You cannot select a day later than today!");
            }
            else if (sender._selectedDate <new Date()) {
                alert("You cannot select a day later than today!");
                sender._textbox.set_Value("");
            }
        }
        function sayKeyCode(event, v) {
            var TextBox = document.getElementById('<%=txtaviltime.ClientID%>');
            //alert(event.keyCode);  
            if (event.keyCode != 8) {
                if (TextBox.value.length == 2 && TextBox.value.length != 3) {
                    TextBox.value = TextBox.value + ":";
                }
                    //else if (TextBox.value.length == 5) {
                    //    TextBox.value = TextBox.value + ":";
                    //}
                    //else if (TextBox.value.length == 8) {
                    //    TextBox.value = TextBox.value + ".";
                    //}
                else {
                    TextBox.value = TextBox.value;
                }
            }
        }
        function get() {
            $("#btnCheckOtp").trigger("click");
        }
        function ValidateEnter(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode
            if (charCode == 13) {
                get();
                return false;
            }
            else {
                return true;
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="panel panel-success" >
        <div runat="server" id="divotp" class="panel-heading">
            <h3 class="panel-title paneltitle">
                <asp:Label ID="lblHeading" runat="server" Text="Verify OTP"></asp:Label></h3>
        </div>

        <div runat="server" id="divservice" visible="false" class="panel-heading">
            <h3 class="panel-title paneltitle">Fix Appointment</h3>
        </div>

        <div class="panel-body" style="padding-left:0px;">
            <div class="col-md-12 " id="mainPanel" runat="server">
                <%-- <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="appoint" ShowMessageBox="true" ShowSummary="false" />--%>
                <div id="dvverify" runat="server" class="col-md-10 ">
                    <%--contarea--%>
                    <div class="col-md-4  linkBottom">Enter verification code sent to your mobile:</div>
                    <%--otptext--%>
                    <div class="col-md-2 linkBottom">
                        <asp:TextBox ID="txtVOtp" ClientIDMode="Static" CssClass="form-control" onKeyPress="return ValidateEnter(event);" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-md-2 linkBottom">
                        <asp:Button ID="btnCheckOtp" ClientIDMode="Static"
                            OnClick="btnCheckOtp_Click" CssClass="btn-group-sm  btn otpbutton" runat="server" Text="Verify" />
                    </div>
                    <%--<div class="col-md-2 linkBottom"><asp:Button ID="btnResend" ClientIDMode="Static" OnClick="btnResend_Click" CssClass="btn-group-sm  btn otpbutton" runat="server" Text="Resend OTP" /></div>--%>
                </div>
                <asp:Panel ID="pnlAppointment" Visible="false" runat="server" Style="margin-top: 10px;">
                    <div class="col-md-4 text-left linkBottom">
                        <div class="input-group input-group-sm">
                            <span class="input-group-addon">Name
                            </span>
                            <asp:Label ID="lblname" CssClass="form-control" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div class="col-md-4 text-left linkBottom">
                        <div class="input-group input-group-sm">
                            <span class="input-group-addon">Mobile Number
                            </span>
                            <asp:Label ID="lblmob" CssClass="form-control" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div class="col-md-4 text-left linkBottom">
                        <div class="input-group input-group-sm">
                            <span class="input-group-addon">EmailID
                            </span>
                            <asp:Label ID="lblemailid" CssClass="form-control" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div class="col-md-10 text-left linkBottom clear">
                        <div class="input-group input-group-sm">
                            <span class="input-group-addon">Interested in Buying the services now
                            </span>
                            <asp:DropDownList ID="ddlappointment" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlappointment_SelectedIndexChanged"
                                CssClass="form-control dropdown">
                                <asp:ListItem Text="--Select Client mood--" Value="0"></asp:ListItem>
                                <asp:ListItem Text="Yes" Value="2"></asp:ListItem>
                                <asp:ListItem Text="Not now, later" Value="1"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div id="avaiability" visible="false" runat="server" class="col-md-12 text-left linkBottom row clear">
                        <div class="col-md-4 text-left linkBottom">
                            <div class="input-group input-group-sm">
                                <span class="input-group-addon">Available Date: 
                                </span>
                                <asp:TextBox ID="txtdate" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:TextBox>
                                <%--<span class="input-group-addon  glyphicon glyphicon-calendar"></span>--%>
                                <asp:CalendarExtender ID="CalendarExtender1" PopupPosition="BottomRight" Animated="false"
                                     OnClientDateSelectionChanged="checkDate" TargetControlID="txtdate" runat="server">
                                </asp:CalendarExtender>
                            </div>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" Display="Dynamic" ForeColor="Red" ValidationGroup="appoint" runat="server" ControlToValidate="txtdate" ErrorMessage="Please enter Appointment Date"></asp:RequiredFieldValidator>
                        </div>


                        <div class="col-sm-6 text-left linkBottom">

                            <div class="col-sm-8 text-left linkBottom" style="padding-right:0px;">
                                <div class="input-group input-group-sm">
                                <span class="input-group-addon" id="Span1">Available Time: </span>
                                <asp:TextBox ID="txtaviltime"  AutoComplete="off" ClientIDMode="Static"
                                    CssClass="form-control" aria-describedby="Span1" onkeyup="sayKeyCode(event,this.value)" MaxLength="5" runat="server"></asp:TextBox>
                              
                            </div>
                                  <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="appoint" Display="Dynamic"
                                    ForeColor="Red" ControlToValidate="txtaviltime" SetFocusOnError="true" ErrorMessage="Enter the Available Time">
                                </asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="rev" runat="server" ErrorMessage="InvalidTime" Display="Dynamic" ForeColor="Red"
                                    ValidationGroup="appoint" ControlToValidate="txtaviltime"
                                    ValidationExpression="^(1[0-2]|0?[1-9]):([0-5]?[0-9])">
                                </asp:RegularExpressionValidator>
                                <act:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" FilterType="Custom,numbers" ValidChars=":" TargetControlID="txtaviltime"
                                    runat="server">
                                </act:FilteredTextBoxExtender>
                            </div>
                            <div class="col-sm-3 text-left linkBottom" style="padding-left: 0px;">
                                <asp:DropDownList ID="ddlTime" CssClass="form-control dropdown" runat="server">
                                    <asp:ListItem Text="AM"></asp:ListItem>
                                    <asp:ListItem Text="PM"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>

                        <%--<div class="col-md-4 text-left linkBottom">
                            <div class="input-group input-group-sm">
                                <span class="input-group-addon">Available  Time: 
                                </span>
                               <asp:DropDownList ID="ddlTime" CssClass="form-control dropdown" runat="server">
                                   <asp:ListItem Text="--Select time--" Value="0"></asp:ListItem>
                                   <asp:ListItem Text="06:00 AM" ></asp:ListItem>
                                   <asp:ListItem Text="07:00 AM" ></asp:ListItem>
                                   <asp:ListItem Text="08:00 AM" ></asp:ListItem>
                                   <asp:ListItem Text="09:00 AM" ></asp:ListItem>
                                   <asp:ListItem Text="10:00 AM" ></asp:ListItem>
                                   <asp:ListItem Text="11:00 AM" ></asp:ListItem>
                                   <asp:ListItem Text="12:00 PM" ></asp:ListItem>
                                   <asp:ListItem Text="1:00 PM" ></asp:ListItem>
                                   <asp:ListItem Text="2:00 PM" ></asp:ListItem>
                                   <asp:ListItem Text="3:00 PM" ></asp:ListItem>
                                   <asp:ListItem Text="4:00 PM" ></asp:ListItem>
                                   <asp:ListItem Text="5:00 PM" ></asp:ListItem>
                                   <asp:ListItem Text="6:00 PM" ></asp:ListItem>
                                   <asp:ListItem Text="7:00 PM" ></asp:ListItem>
                                   <asp:ListItem Text="8:00 PM" ></asp:ListItem>
                                   <asp:ListItem Text="9:00 PM" ></asp:ListItem>
                                   <asp:ListItem Text="10:00 PM" ></asp:ListItem>
                               </asp:DropDownList>
                           
                        </div><asp:RequiredFieldValidator ID="RequiredFieldValidator1"  Display="Dynamic" ForeColor="Red"
                             ValidationGroup="appoint" ControlToValidate="ddlTime" runat="server" InitialValue="0" ErrorMessage="Please select Appoitment Time"></asp:RequiredFieldValidator> </div>
                        --%>
                        <div class=" text-left linkBottom">
                            <asp:Button ID="btnSubmit" ClientIDMode="Static" ValidationGroup="appoint" CssClass="btn-group-sm  btn otpbutton" runat="server" Text="Fix Appoitnment" OnClick="btnSubmit_Click" />
                            <asp:Button ID="btnUpdateInfo" ClientIDMode="Static" Visible="false" CssClass="btn btn-danger" runat="server" Text="Update Info" OnClick="btnUpdateInfo_Click" />
                        </div>
                    </div>
                    <div id="dvServices" visible="false" runat="server" class="col-md-8 text-left linkBottom row clear">
                        <asp:Button ID="btnServices" ClientIDMode="Static" CssClass="btn-group-sm  btn otpbutton" runat="server" Text="Basic + AddOn  Services" OnClick="btnServices_Click" />
                        <asp:Button ID="btnAddOnServices" ClientIDMode="Static" CssClass="btn-group-sm  btn otpbutton" runat="server" Text="AddOn Services" OnClick="btnAddOnServices_Click" />
                    </div>
                </asp:Panel>
            </div>
            <div class="col-md-12 " id="dvBack" runat="server" visible="false">
                <asp:Button ID="btnBack" runat="server" Text="Go To DashBoard" CssClass="btn-group-sm  btn otpbutton" OnClick="btnBack_Click" />
            </div>
        </div>
    </div>
</asp:Content>

