<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SubscriptionForm.ascx.cs" Inherits="MakeNMake.UserControl.SubscriptionForm" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<script src="../Scripts/jquery-2.0.1.min.js"></script>
<script src="../Dialog/jquery-ui.min.js"></script>
<link href="../Dialog/jquery-ui.css" rel="stylesheet" />
<script type="text/javascript">

    function checkDate(sender, args) {
        if (sender._selectedDate > new Date()) {
            var selectedDate = sender._selectedDate;

            var todayDate = new Date();
            if (selectedDate > todayDate) {
                alert("You cannot select a future date!");
            }
            else if (todayDate.getFullYear() - selectedDate.getFullYear() < 18) {

                alert("You must be 18 year older to use this application");
            }
        }
    }
       
    function PageReload(val) {
        var getValue = confirm('Subscription Form has been submitted successfully');
        if (getValue) {
            if (val == 1) {
                window.location.href = "SUserServices.aspx";
            }
            else {
                window.location.href = "Clients.aspx";
            }
        }
        else {
            if (val == 1) {
                window.location.href = "SUserServices.aspx";
            }
            else {
                window.location.href = "Clients.aspx";
            }
        }
    }
    function OpenInvoice(userid) {
        $('#MyDialog').html('<iframe border=0 width="979px" height ="670px" src= "ClientInvoice.aspx?UserID=' + userid + '"> </iframe>').dialog({
            title: '',
            modal: true,
            autoOpen: true,
            height: '650',
            width: '1000',
            resizable: false,
            position: ['left+40', 'top+30'],
            closeOnEscape: false,
            dialogClass: "alert"
        });
    }
</script>
<div class="panel panel-success">
    <div class="panel-heading">
        <h3 class="panel-title paneltitle">Client Subscription Details</h3>
    </div>
    <div class="panel-body" style="padding-left:0px;">
        <div class="col-sm-12" style="padding-left:0px;">
            <div class="col-sm-12 text-left linkBottom">
                    <asp:LinkButton ID="lnkBtnContract" Text="View Contract" style="color:green;" runat="server"></asp:LinkButton>
            </div>
            <div class="col-sm-6 text-left linkBottom">
                <div class="input-group input-group-sm">
                    <span class="input-group-addon" id="Span1">ClientID</span>
                    <asp:Label ID="lblclientid" CssClass="form-control" runat="server"></asp:Label>
                </div>

            </div>
            <div class="col-sm-6 text-left linkBottom">
                <div class="input-group input-group-sm">
                    <span class="input-group-addon" id="Span6">First Name</span>
                    <asp:Label ID="lblfname" CssClass="form-control" runat="server"></asp:Label>

                </div>

            </div>
            <div class="col-sm-6 text-left linkBottom">
                <div class="input-group input-group-sm">
                    <span class="input-group-addon" id="Span8">Last Name</span>
                    <asp:Label ID="lbllname" CssClass="form-control" runat="server"></asp:Label>
                </div>

            </div>
            <div class="col-sm-6 text-left linkBottom">
                <div class="input-group input-group-sm">
                    <span class="input-group-addon" id="Span13">Email Id</span>
                    <asp:Label ID="lblemailid" CssClass="form-control" runat="server"></asp:Label>
                </div>

            </div>
            <div class="col-sm-6 text-left linkBottom">
                <div class="input-group input-group-sm">
                    <span class="input-group-addon" id="Span10">Alternate Email Id</span>
                    <asp:TextBox ID="txtemail2" AutoComplete="off" ClientIDMode="Static"
                        placeholder="Enter Email Id" CssClass="form-control" aria-describedby="basic-addon1" runat="server"></asp:TextBox>
                </div>
                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtemail2"
                    ErrorMessage="Please Enter EmailID" ValidationGroup="signup" Display="Dynamic" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Enter Correct Email ID"
                    Display="Dynamic" ForeColor="Red" ControlToValidate="txtemail2" ValidationGroup="user"
                    ValidationExpression="\b[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}\b"></asp:RegularExpressionValidator>--%>


            </div>
            <div class="col-sm-6 text-left linkBottom">
                <div class="input-group input-group-sm">
                    <span class="input-group-addon" id="Span11">Mobile No</span>
                    <asp:Label ID="lblmobile" CssClass="form-control" runat="server"></asp:Label>
                </div>

            </div>
            <div class="col-sm-6 text-left linkBottom">
                <div class="input-group input-group-sm">
                    <span class="input-group-addon" id="Span22">Alternate Mobile No</span>
                    <asp:TextBox ID="txtmob2" AutoComplete="off" ClientIDMode="Static" MaxLength="10" onkeypress="return ValidateNumber(event)"
                        placeholder="Enter Mobile No" CssClass="form-control" aria-describedby="basic-addon1" runat="server"></asp:TextBox>
                </div>
                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="Enter Mobile Number" ValidationGroup="signup"
                    ForeColor="Red" Display="Dynamic" ControlToValidate="txtmob2"></asp:RequiredFieldValidator>--%>
            </div>
            <div class="col-sm-6 text-left linkBottom">
                <div class="input-group input-group-sm">
                    <span class="input-group-addon" id="Span25">Service Category</span>
                    <asp:DropDownList ID="ddlcategory" runat="server" CssClass="form-control dropdown">
                        <asp:ListItem Text="--Select Category--" Value="0"></asp:ListItem>
                        <asp:ListItem Text="Domestic" Value="D"></asp:ListItem>
                        <asp:ListItem Text="Commercial" Value="C"></asp:ListItem>
                    </asp:DropDownList>
                </div>

            </div>
            <div class="col-sm-6 text-left linkBottom">
                <div class="input-group input-group-sm">
                    <span class="input-group-addon" id="Span18">Service Type</span>
                    <asp:TextBox ID="txtServiceType" AutoComplete="off" ClientIDMode="Static" CssClass="form-control" aria-describedby="basic-addon1" runat="server"></asp:TextBox>

                </div>
            </div>
            <div class="col-sm-6 text-left linkBottom">
                <div class="input-group input-group-sm">
                    <span class="input-group-addon" id="Span12">Service Plan</span>
                    <asp:TextBox ID="lblServicePlan" AutoComplete="off" ClientIDMode="Static" CssClass="form-control" aria-describedby="basic-addon1" runat="server"></asp:TextBox>

                </div>

            </div>
            <div class="col-sm-6 text-left linkBottom">
                <div class="input-group input-group-sm">
                    <span class="input-group-addon" id="Span26">Date Of Birth</span>
                    <asp:TextBox ID="txtDob" AutoComplete="off" ClientIDMode="Static"
                        placeholder="Enter Date of Birth" CssClass="form-control" aria-describedby="basic-addon1" runat="server"></asp:TextBox>
                </div>
                <asp:CalendarExtender ID="CalendarExtender1" TargetControlID="txtDob"
                     OnClientDateSelectionChanged="checkDate" Format="MM/dd/yyyy"  runat="server"></asp:CalendarExtender>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator16"
                    runat="server" ValidationGroup="signup" Display="Dynamic" ForeColor="Red"
                    ControlToValidate="txtDob" SetFocusOnError="true" ErrorMessage="Enter Date of Birth"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" SetFocusOnError="true" runat="server" ControlToValidate="txtDob"
                    ErrorMessage="Enter Valid date" Display="Dynamic" ForeColor="Red" ValidationGroup="signup" ValidationExpression="^(0[1-9]|1[012])[- /.](0[1-9]|[12][0-9]|3[01])[- /.](19|20)\d\d+$"></asp:RegularExpressionValidator>


            </div>
            <div class="col-sm-6 text-left linkBottom">
                <div class="input-group input-group-sm">
                    <span class="input-group-addon" id="Span14">Current Address</span>
                    <asp:TextBox ID="txtaddress0" AutoComplete="off" TextMode="MultiLine" MaxLength="300"
                        placeholder="Enter Address" CssClass="form-control" aria-describedby="basic-addon1" runat="server"></asp:TextBox>
                </div>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4"
                    runat="server" ValidationGroup="signup" Display="Dynamic" ForeColor="Red"
                    ControlToValidate="txtaddress0" ErrorMessage="Enter Address"></asp:RequiredFieldValidator>

            </div>
            <div class="col-sm-6 text-left linkBottom">
                <div class="input-group input-group-sm">
                    <span class="input-group-addon" id="Span15">Country</span>
                    <asp:DropDownList ID="ddlCountry0" AutoPostBack="true" OnSelectedIndexChanged="ddlCountry0_SelectedIndexChanged" CssClass="form-control dropdown" runat="server">
                    </asp:DropDownList>
                </div>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator12"
                    runat="server" ValidationGroup="signup" Display="Dynamic" ForeColor="Red"
                    ControlToValidate="ddlCountry0" ErrorMessage="Enter Country"></asp:RequiredFieldValidator>
            </div>
            <div class="col-sm-6 text-left linkBottom">
                <div class="input-group input-group-sm">
                    <span class="input-group-addon" id="Span16">State</span>

                    <asp:DropDownList ID="ddlState0" AutoPostBack="true" OnSelectedIndexChanged="ddlState0_SelectedIndexChanged" CssClass="form-control dropdown" runat="server">
                    </asp:DropDownList>

                </div>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator13"
                    runat="server" ValidationGroup="signup" Display="Dynamic" ForeColor="Red"
                    ControlToValidate="ddlState0" ErrorMessage="EnterState"></asp:RequiredFieldValidator>
            </div>
            <div class="col-sm-6 text-left linkBottom">
                <div class="input-group input-group-sm">
                    <span class="input-group-addon" id="Span2">District</span>

                    <asp:DropDownList ID="ddlDistrict0" CssClass="form-control dropdown" AutoPostBack="true" OnSelectedIndexChanged="ddlDistrict0_SelectedIndexChanged" runat="server">
                    </asp:DropDownList>

                </div>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator14"
                    runat="server" ValidationGroup="signup" Display="Dynamic" ForeColor="Red"
                    ControlToValidate="ddlDistrict0" ErrorMessage="Enter District"></asp:RequiredFieldValidator>

            </div>
            <div class="col-sm-6 text-left linkBottom">
                <div class="input-group input-group-sm">
                    <span class="input-group-addon" id="Span17">City</span>

                    <asp:DropDownList ID="ddlCity0" CssClass="form-control dropdown" runat="server">
                    </asp:DropDownList>

                </div>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator15"
                    runat="server" ValidationGroup="signup" Display="Dynamic" ForeColor="Red"
                    ControlToValidate="ddlCity0" ErrorMessage="Enter City"></asp:RequiredFieldValidator>

            </div>

            <div class="input-group" style="padding-left: 1.2em;">
                <span class="input-group-addon" style="width: 100px; margin-left: 10px;">Contactable</span> &nbsp;
              <span style="width: 110px;">
                  <asp:CheckBox ID="chkContactable" ClientIDMode="Static"
                      AutoPostBack="true" runat="server" OnCheckedChanged="chkContactable_CheckedChanged" /></span>
            </div>

            <br />

            <div class="col-sm-6 text-left linkBottom">
                <div class="input-group input-group-sm">
                    <span class="input-group-addon" id="Span5">Permanent Address </span>
                    <asp:TextBox ID="txtaddress" AutoComplete="off" TextMode="MultiLine" MaxLength="300"
                        placeholder="Enter Address" CssClass="form-control" aria-describedby="basic-addon1" runat="server"></asp:TextBox>
                </div>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator11"
                    runat="server" ValidationGroup="signup" Display="Dynamic" ForeColor="Red"
                    ControlToValidate="txtaddress" ErrorMessage="Enter Address"></asp:RequiredFieldValidator>
            </div>
            <div class="col-sm-6 text-left linkBottom">
                <div class="input-group input-group-sm">
                    <span class="input-group-addon" id="Span3">Country</span>
                    <asp:DropDownList ID="ddlCountry" AutoPostBack="true" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged" CssClass="form-control dropdown" runat="server">
                    </asp:DropDownList>
                </div>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5"
                    runat="server" ValidationGroup="signup" Display="Dynamic" ForeColor="Red"
                    ControlToValidate="ddlCountry" ErrorMessage="Enter Country"></asp:RequiredFieldValidator>
            </div>
            <div class="col-sm-6 text-left linkBottom">
                <div class="input-group input-group-sm">
                    <span class="input-group-addon" id="Span4">State</span>

                    <asp:DropDownList ID="ddlState" AutoPostBack="true" OnSelectedIndexChanged="ddlState_SelectedIndexChanged" CssClass="form-control dropdown" runat="server">
                    </asp:DropDownList>

                </div>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator10"
                    runat="server" ValidationGroup="signup" Display="Dynamic" ForeColor="Red"
                    ControlToValidate="ddlState" ErrorMessage="Enter State"></asp:RequiredFieldValidator>

            </div>
            <div class="col-sm-6 text-left linkBottom">
                <div class="input-group input-group-sm">
                    <span class="input-group-addon" id="Span21">District</span>

                    <asp:DropDownList ID="ddlDistrict" CssClass="form-control dropdown" AutoPostBack="true" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged" runat="server">
                    </asp:DropDownList>

                </div>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator8"
                    runat="server" ValidationGroup="signup" Display="Dynamic" ForeColor="Red"
                    ControlToValidate="ddlDistrict" ErrorMessage="Enter District"></asp:RequiredFieldValidator>

            </div>
            <div class="col-sm-6 text-left linkBottom">
                <div class="input-group input-group-sm">
                    <span class="input-group-addon" id="Span9">City</span>

                    <asp:DropDownList ID="ddlCity" CssClass="form-control dropdown" runat="server">
                    </asp:DropDownList>

                </div>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator9"
                    runat="server" ValidationGroup="signup" Display="Dynamic" ForeColor="Red"
                    ControlToValidate="ddlCity" ErrorMessage="Enter City"></asp:RequiredFieldValidator>

            </div>

            <div class="col-sm-6 text-left linkBottom">
                <div class="input-group input-group-sm">
                    <span class="input-group-addon" id="Span23">How Do You Know About Us?</span>
                    <asp:TextBox ID="txtabout" AutoComplete="off" ClientIDMode="Static"
                        CssClass="form-control" aria-describedby="basic-addon1" runat="server"></asp:TextBox>
                </div>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1"
                    runat="server" ValidationGroup="signup" Display="Dynamic" ForeColor="Red"
                    ControlToValidate="txtabout" ErrorMessage="Enter how do u know about us?"></asp:RequiredFieldValidator>
            </div>

            <div class="col-sm-6 text-left linkBottom">
                <div class="input-group input-group-sm">
                    <span class="input-group-addon" id="Span7">Nearest Mile Stone</span>
                    <asp:TextBox ID="txtnear" AutoComplete="off" ClientIDMode="Static"
                        CssClass="form-control" aria-describedby="basic-addon1" runat="server"></asp:TextBox>
                </div>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator17"
                    runat="server" ValidationGroup="signup" Display="Dynamic" ForeColor="Red"
                    ControlToValidate="txtnear" ErrorMessage="Enter Nearest Mile Stone"></asp:RequiredFieldValidator>
            </div>
            <div class="col-sm-6 text-left linkBottom">
                <div class="input-group input-group-sm">
                    <span class="input-group-addon" id="Span24">SMS Enable</span>
                    <asp:RadioButtonList ID="rdsms" runat="server" AutoPostBack="True" RepeatDirection="Horizontal">
                        <asp:ListItem Value="0" Text="No"></asp:ListItem>
                        <asp:ListItem Value="1" Text="Yes"></asp:ListItem>
                    </asp:RadioButtonList>
                </div>

            </div>
            <div class="col-sm-6 text-left linkBottom">
                <div class="col-sm controls">
                    <asp:Button ID="btnUPdateInfo" runat="server" ClientIDMode="Static" ValidationGroup="signup" Text="Submit"
                        class="btn btn-success" OnClick="btnUPdateInfo_Click" />
                   <%--  <asp:Button ID="btnBack" runat="server" ClientIDMode="Static" Text="Back"
                        class="btn btn-success" OnClick="btnBack_Click" />--%>
                </div>
            </div>


        </div>
    </div>
    <div id="MyDialog" >
                
    </div>
</div>
