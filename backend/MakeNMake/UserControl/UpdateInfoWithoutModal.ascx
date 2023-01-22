<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UpdateInfoWithoutModal.ascx.cs" Inherits="MakeNMake.UserControl.UpdateInfoWithoutModal" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<script type="text/javascript">

    function checkDate(sender, args) {
        //if (sender._selectedDate > new Date()) {
        //    alert("You cannot select a future date!");
        //    sender._selectedDate = new Date();
        //    // set the date back to the current date
        //    sender._textbox.set_Value(sender._selectedDate.format(sender._format))
        //}
        var selectedDate = sender._selectedDate;

        var todayDate = new Date();
        if (selectedDate > todayDate) {
            alert("You cannot select a future date!");
            //    sender._selectedDate = new Date();
            // set the date back to the current date
            //     sender._textbox.set_Value(sender._selectedDate.format(sender._format))
        }
        else if (todayDate.getFullYear() - selectedDate.getFullYear() < 18) {

            alert("You must be 18 year older to use this application");
        }
    }
</script>


<div class="col-sm-12">
    <%--<asp:ValidationSummary ID="ValidationSummary1" ValidationGroup="service" ShowSummary="true" ForeColor="Red" runat="server" />--%>
    <div class="col-sm-6 text-left linkBottom">
        <div class="input-group input-group-sm">
            <span class="input-group-addon" id="Span1">MobileNumber</span>
            <asp:TextBox ID="txtMobileNumber" onkeypress="return ValidateNumber(event)" CssClass="form-control" MaxLength="10" placeholder="Enter Mobile Number" runat="server"></asp:TextBox>
        </div>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="service"
            runat="server" ErrorMessage="Enter Mobile Number"
            ControlToValidate="txtMobileNumber"  Display="Dynamic" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" SetFocusOnError="true" runat="server" ControlToValidate="txtMobileNumber"
            ErrorMessage="Enter Valid Mobile No"  Display="Dynamic" ForeColor="Red" ValidationGroup="service" ValidationExpression="[0-9]{10}"></asp:RegularExpressionValidator>

    </div>
    <div class="col-sm-6 text-left linkBottom">
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
    </div>
    <div class="col-sm-6 text-left linkBottom">
        <div class="input-group input-group-sm">
            <span class="input-group-addon" id="Span8">Date of Birth</span>
            <asp:TextBox ID="txtDob" AutoComplete="off" ClientIDMode="Static"
                placeholder="Enter Date of Birth" CssClass="form-control" aria-describedby="basic-addon1" runat="server"></asp:TextBox>
        </div>
        <asp:CalendarExtender ID="CalendarExtender1" TargetControlID="txtDob" OnClientDateSelectionChanged="checkDate" Format="MM/dd/yyyy" SelectedDate='<%#DateTime.Now %>' runat="server"></asp:CalendarExtender>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator7"
            runat="server" ValidationGroup="service"  Display="Dynamic" ForeColor="Red"
            ControlToValidate="txtDob" SetFocusOnError="true" ErrorMessage="Enter Date of Birth"></asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" SetFocusOnError="true" runat="server" ControlToValidate="txtDob"
            ErrorMessage="Enter Valid date"  Display="Dynamic" ForeColor="Red" ValidationGroup="service" ValidationExpression="^(0[1-9]|1[012])[- /.](0[1-9]|[12][0-9]|3[01])[- /.](19|20)\d\d+$"></asp:RegularExpressionValidator>

    </div> <div class="col-sm-6 text-left linkBottom">
        <div class="input-group input-group-sm">
            <span class="input-group-addon" id="Span2">Country</span>
            <asp:DropDownList ID="ddlCountry" AutoPostBack="true" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged" CssClass="form-control dropdown" runat="server">
            </asp:DropDownList>
        </div>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="service" runat="server"
            ErrorMessage="Enter Country" InitialValue="0"
            ControlToValidate="ddlCountry"  Display="Dynamic" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
    </div>
    <div class="col-sm-6 text-left linkBottom">
        <div class="input-group input-group-sm">
            <span class="input-group-addon" id="Span5">Address</span>
            <asp:TextBox ID="txtaddress" AutoComplete="off" TextMode="MultiLine" MaxLength="300"
                placeholder="Enter Address" CssClass="form-control" aria-describedby="basic-addon1" runat="server"></asp:TextBox>
        </div>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator5"
            runat="server" ValidationGroup="service"  Display="Dynamic" ForeColor="Red"
            ControlToValidate="txtaddress" SetFocusOnError="true" ErrorMessage="Enter Address"></asp:RequiredFieldValidator>
    </div>
   
    <div class="col-sm-6 text-left linkBottom">
        <div class="input-group input-group-sm">
            <span class="input-group-addon" id="Span3">State</span>
            <asp:DropDownList ID="ddlState" AutoPostBack="true" OnSelectedIndexChanged="ddlState_SelectedIndexChanged" CssClass="form-control dropdown" runat="server">
            </asp:DropDownList>
        </div>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="service" runat="server"
            ErrorMessage="Enter State" InitialValue="0"
            ControlToValidate="ddlState"  Display="Dynamic" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
    </div>
     <div class="col-sm-6 text-left linkBottom">
                    <div class="input-group input-group-sm">
                        <span class="input-group-addon" id="Span6">District</span>
                        <asp:DropDownList ID="ddlDistrict" CssClass="form-control dropdown" AutoPostBack="true" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged" runat="server">
                        </asp:DropDownList>
                    </div>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8"  ValidationGroup="service" runat="server"
                        ErrorMessage="Enter District" InitialValue="0"
                        ControlToValidate="ddlDistrict"  Display="Dynamic" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                </div>
    <div class="col-sm-6 text-left linkBottom">
        <div class="input-group input-group-sm">
            <span class="input-group-addon" id="Span4">City</span>
            <asp:DropDownList ID="ddlCity" CssClass="form-control dropdown" runat="server">
            </asp:DropDownList>
        </div>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ValidationGroup="service" runat="server"
            ErrorMessage="Enter City" InitialValue="0"
            ControlToValidate="ddlCity"  Display="Dynamic" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
    </div>
    <div class="col-sm-6 text-left linkBottom">
        <asp:Button ID="btnUPdateInfo" runat="server" OnClick="btnUPdateInfo_Click" ClientIDMode="Static"
             ValidationGroup="service" Text="Save Changes" CssClass="btn btn-success" />
                <asp:Button ID="Button1" runat="server" Text="Cancel" CssClass="btn btn-success" OnClick="Button1_Click" />
    </div>

</div>


