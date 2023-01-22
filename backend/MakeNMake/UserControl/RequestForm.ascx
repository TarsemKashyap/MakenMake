<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RequestForm.ascx.cs" Inherits="MakeNMake.UserControl.RequestForm" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
  <script type="text/javascript">
      function PageReload(pageid,id) {          
          var getValue = confirm(' Visiting Request with Id ' + id + ' has been submitted successfully');
          if (pageid==1) {
              window.location.href = "DashBoard.aspx";
          }
          else {
              window.location.href = "Clients.aspx";
          }
      }
    </script>


<script type="text/javascript">
    function checkDate(sender, args) {
        if (sender._selectedDate.format("dd/MM/yyyy") == new Date().format("dd/MM/yyyy")) {
            sender._textbox.set_Value("");
            alert("You cannot select a day later than today!");
        }
        else if (sender._selectedDate < new Date()) {
            alert("You cannot select a day later than today!");
            sender._textbox.set_Value("");
        }
    }

    function sayKeyCode(event, v) {
        var TextBox = document.getElementById('<%=txtPrefferedtime.ClientID%>');
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

</script>

<div class="panel panel-success">
    <div class="panel-heading">
        <h3 class="panel-title paneltitle">Visiting Request Form</h3>
    </div>
    <div class="panel-body clear" style="padding-left: 0px;">
      <div class="table-responsive">
              <div class="col-md-12" style="padding-left: 0px;"> 

            <div class="col-sm-6 text-left linkBottom">
                <div class="input-group">
                    <span class="input-group-addon">First Name</span>
                    <asp:TextBox ID="txtfirstName" onkeypress="return ValidateCharacters(event)" aria-describedby="basic-addon1"
                        AutoCompleteType="None" autocomplete="off" runat="server" class="form-control"></asp:TextBox>
                </div>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Enter Firstname" ValidationGroup="signup"
                    ForeColor="Red" Display="Dynamic" SetFocusOnError="true" ControlToValidate="txtfirstName"></asp:RequiredFieldValidator>
            </div>
            <div class="col-sm-6 text-left linkBottom">
                <div class="input-group">
                    <span class="input-group-addon">Last Name</span>
                    <asp:TextBox ID="txtlastName" AutoCompleteType="None" aria-describedby="basic-addon1" onkeypress="return ValidateCharacters(event)" autocomplete="off" runat="server" class="form-control normalinput"></asp:TextBox>
                </div>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Enter LastName" ValidationGroup="signup"
                    ForeColor="Red" Display="Dynamic" SetFocusOnError="true" ControlToValidate="txtlastName"></asp:RequiredFieldValidator>

            </div>
            <div class="col-sm-6 text-left linkBottom">
                <div class="input-group">
                    <span class="input-group-addon">Mobile Number</span>
                    <asp:TextBox ID="txtmobile" onkeypress="return ValidateNumber(event)" aria-describedby="basic-addon1" MaxLength="10" AutoCompleteType="None" runat="server" class="form-control normalinput"></asp:TextBox>
                </div>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="Enter Mobile Number" ValidationGroup="signup"
                    ForeColor="Red" Display="Dynamic" ControlToValidate="txtmobile"></asp:RequiredFieldValidator>
            </div>
            <div class="col-sm-6 text-left linkBottom">
                <div class="input-group">
                    <span class="input-group-addon">Landline(Optional)</span>
                    <asp:TextBox ID="txtlandline" onkeypress="return ValidateNumber(event)"
                        aria-describedby="basic-addon1" MaxLength="11" AutoCompleteType="None" runat="server" class="form-control normalinput"></asp:TextBox>
                </div>
            </div>
            <div class="col-sm-6 text-left linkBottom">
                <div class="input-group input-group-sm">
                    <span class="input-group-addon" id="Span2">Email ID</span>
                    <asp:TextBox ID="txtEmailID" CssClass="form-control" aria-describedby="basic-addon1" runat="server"></asp:TextBox>
                </div>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtEmailID"
                    ErrorMessage="Please Enter EmailID" ValidationGroup="signup" Display="Dynamic" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Enter Correct Email ID"
                    Display="Dynamic" ForeColor="Red" ControlToValidate="txtEmailID" ValidationGroup="user"
                    ValidationExpression="\b[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}\b"></asp:RegularExpressionValidator>

            </div>
            <div class="col-sm-6 text-left linkBottom">
                <div class="input-group">
                    <span class="input-group-addon">Address </span>
                    <asp:TextBox ID="txtaddress" AutoComplete="off" MaxLength="300"
                        CssClass="form-control" aria-describedby="basic-addon1" runat="server"></asp:TextBox>
                </div>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator11"
                    runat="server" ValidationGroup="signup" Display="Dynamic" ForeColor="Red"
                    ControlToValidate="txtaddress" ErrorMessage="Enter Address"></asp:RequiredFieldValidator>
            </div>
            <div class="col-sm-6 text-left linkBottom">
                <div class="input-group">
                    <span class="input-group-addon">Total Site Area</span>
                    <asp:TextBox ID="txttotalarea" AutoComplete="off" MaxLength="300"
                        CssClass="form-control" aria-describedby="basic-addon1" runat="server"></asp:TextBox>
                </div>
                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2"
                    runat="server" ValidationGroup="signup" Display="Dynamic" ForeColor="Red"
                    ControlToValidate="txttotalarea"  SetFocusOnError="true" ErrorMessage="Enter Total Site Area"></asp:RequiredFieldValidator>--%>
            </div>
            <div class="col-sm-6 text-left linkBottom">
                <div class="input-group">
                    <span class="input-group-addon">Built up Area</span>
                    <asp:TextBox ID="txtbuildArea" AutoComplete="off" MaxLength="300"
                        CssClass="form-control" aria-describedby="basic-addon1" runat="server"></asp:TextBox>
                </div>
                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator5"
                    runat="server" ValidationGroup="signup" Display="Dynamic" ForeColor="Red"
                    ControlToValidate="txtbuildArea"  SetFocusOnError="true" ErrorMessage="Enter Built up Area"></asp:RequiredFieldValidator>--%>
            </div>
            <div class="col-sm-6 text-left linkBottom">
                <div class="input-group">
                    <span class="input-group-addon">Preffered Date</span>
                    <asp:TextBox ID="txtPrefferedate" AutoComplete="off" Style="z-index: 11111;"
                        CssClass="form-control" aria-describedby="basic-addon1" runat="server"></asp:TextBox>
                </div>
                <asp:CalendarExtender ID="CalendarExtender1" PopupPosition="BottomRight" Animated="false"
                    SelectedDate='<%#DateTime.Now %>' OnClientDateSelectionChanged="checkDate" TargetControlID="txtPrefferedate" runat="server">
                </asp:CalendarExtender>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" Display="Dynamic"
                    ForeColor="Red" runat="server" ValidationGroup="signup" ControlToValidate="txtPrefferedate" ErrorMessage="Please enter Date"></asp:RequiredFieldValidator>
            </div>
            <div class="col-sm-6 text-left linkBottom">
                <div class="input-group">
                    <span class="input-group-addon">Preffered Time</span>
                    <asp:TextBox ID="txtPrefferedtime" placeholder="HH:MM" Width="80px" AutoComplete="off" ClientIDMode="Static"
                        CssClass="form-control" aria-describedby="Span1" MaxLength="5" runat="server" onkeyup="sayKeyCode(event,this.value)"></asp:TextBox>
                    <asp:DropDownList ID="ddlTime" CssClass="form-control" Width="80px" runat="server">
                        <asp:ListItem Text="AM" Selected="True"></asp:ListItem>
                        <asp:ListItem Text="PM"></asp:ListItem>
                    </asp:DropDownList>
                </div>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ValidationGroup="signup" Display="Dynamic"
                    ForeColor="Red" ControlToValidate="txtPrefferedtime" SetFocusOnError="true" ErrorMessage="Enter  Time">
                </asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="rev" runat="server" ErrorMessage="InvalidTime" Display="Dynamic" ForeColor="Red"
                    ValidationGroup="signup" ControlToValidate="txtPrefferedtime"
                    ValidationExpression="^(1[0-2]|0?[1-9]):([0-5]?[0-9])">
                </asp:RegularExpressionValidator>
            </div>

            <div class="input-group" style="padding-left: 1.2em;">
                <span class="input-group-addon" style="width: 100px; margin-left: 10px;">Contactable Address(Same As Above)</span> &nbsp;
              <span style="width: 110px;">
                  <asp:CheckBox ID="chkContactable" ClientIDMode="Static"
                      OnCheckedChanged="chkContactable_CheckedChanged" AutoPostBack="true" runat="server" /></span>
            </div>
            <br />
            <div class="col-sm-6 text-left linkBottom">
                <div class="input-group input-group-sm">
                    <span class="input-group-addon">First Name</span>
                    <asp:TextBox ID="txtfname1" onkeypress="return ValidateCharacters(event)"
                        AutoCompleteType="None" autocomplete="off" runat="server" class="form-control normalinput"></asp:TextBox>
                </div>

                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Enter Contactable Firstname"
                    ForeColor="Red" Display="Dynamic" SetFocusOnError="true" ControlToValidate="txtfname1"></asp:RequiredFieldValidator>--%>
            </div>
            <div class="col-sm-6 text-left linkBottom">
                <div class="input-group input-group-sm">
                    <span class="input-group-addon">Last Name</span>
                    <asp:TextBox ID="txtlname1" AutoCompleteType="None"
                        onkeypress="return ValidateCharacters(event)" autocomplete="off" runat="server" class="form-control normalinput"></asp:TextBox>
                </div>
                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ErrorMessage="Enter Contactable LastName"
                    ForeColor="Red" Display="Dynamic" SetFocusOnError="true" ControlToValidate="txtlname1"></asp:RequiredFieldValidator>--%>

            </div>
            <div class="col-sm-6 text-left linkBottom">
                <div class="input-group input-group-sm">
                    <span class="input-group-addon">Mobile Number</span>
                    <asp:TextBox ID="txtmobilenumber" onkeypress="return ValidateNumber(event)" MaxLength="10" AutoCompleteType="None"
                        runat="server" class="form-control normalinput"></asp:TextBox>
                </div>
                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ErrorMessage="Enter Contactable Mobile Number"
                    ForeColor="Red" Display="Dynamic" ControlToValidate="txtmobilenumber"></asp:RequiredFieldValidator>--%>
            </div>

            <div class="col-sm-6 text-left linkBottom">
                <div class="input-group">
                    <span class="input-group-addon">Contactable Address </span>
                    <asp:TextBox ID="txtcont_address" AutoComplete="off" MaxLength="300"
                        CssClass="form-control" aria-describedby="basic-addon1" runat="server"></asp:TextBox>
                </div>
                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2"
                    runat="server" ValidationGroup="signup" Display="Dynamic" ForeColor="Red"
                    ControlToValidate="txtcont_address" ErrorMessage="Enter Contactable Address"></asp:RequiredFieldValidator>--%>
            </div>
            <div class="col-sm-6 text-left linkBottom">
                <div class="input-group input-group-sm">
                    <span class="input-group-addon" id="Span2">Email ID</span>
                    <asp:TextBox ID="txtemailid1" CssClass="form-control normalinput" runat="server"></asp:TextBox>
                </div>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ValidationGroup="signup" ErrorMessage="Enter Correct Email ID"
                    Display="Dynamic" ForeColor="Red" ControlToValidate="txtemailid1"
                    ValidationExpression="\b[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}\b"></asp:RegularExpressionValidator>

            </div>
                 
         <div class="col-sm-6 text-left linkBottom">
                <div class="input-group input-group-sm">
                   
  </div>
     
                       </div>
              <div class="panel-body">
           <asp:DataList ID="RptServices" runat="server" class="table-responsive">
                <HeaderTemplate>
                    <table class="table table-hover  table-bordered  table-condensed"  data-height="400">
                        <thead style="color: White; background-color: #6b9297;">
                            <tr>
                                <th></th>
                                <th>Service</th>
                                <th>Category</th>
                                <th>ServiceType</th>
                                <th>Plan</th>
                                <%--<th>Unlimited Plan Category</th>--%>
                            </tr>
                        </thead>
                        <tbody>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td>
                            <asp:CheckBox ID="chkService" runat="server" />
                            <asp:HiddenField ID="hdnID" runat="server"
                                Value='<%#Eval("ServiceID") %>' />
                        </td>
                        <td>
                            <asp:Label ID="lblName" runat="server" Text='<%#Eval("ServiceName") %>'></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="Label1" runat="server" Text='<%#Eval("ServiceCategory") %>'></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="Label2" runat="server" Text='<%#Eval("ServiceType") %>'></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="Label3" runat="server" Text='<%#Eval("ServicePlan") %>'></asp:Label>
                            <asp:HiddenField ID="hdnPlanID" runat="server"
                                Value='<%#Eval("ServicePlanID") %>' />
                        </td>
                        <%--  <td>
                                <asp:Label ID="Label4" runat="server" Text='<%#Eval("category") %>'></asp:Label>
                                <asp:HiddenField ID="hdnUnlimitedID" runat="server"
                                    Value='<%#Eval("planid") %>' />
                            </td>--%>
                    </tr>
                </ItemTemplate>
            </asp:DataList>
        </div>
        



                   <div class="col-sm-12 text-left linkBottom">
                    <asp:Button ID="btnSignUp" runat="server" ClientIDMode="Static" style="float:left;margin-right:10px;" ValidationGroup="signup" Text="Submit"
                        class="btn btn-success" OnClick="btnSignUp_Click" />
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" style="float:left;"
                        class="btn btn-success" OnClick="btnCancel_Click" />

     
                       </div>
            </div>
            
        </div>
              
         
    </div>

</div>

 

