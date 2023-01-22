<%@ Page Title="Add Customer" Language="C#" MasterPageFile="AdminMaster.Master" Debug="true" AutoEventWireup="true" CodeBehind="AddClient.aspx.cs" Inherits="MakeNMake.CustomerCare.AddClient" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../Scripts/jquery-2.0.1.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#btnAdd").click(function (e) {
                var from = $("#txtservicestart").val();
                var to = $("#txtserviceend").val();
                if (from == "") {
                    alert("Please enter  service start time");
                    e.preventDefault();
                }
                if (to == "") {
                    alert("Please enter  service end time");
                    e.preventDefault();
                }
                if (from != "" && to != "") {
                    if (parseInt(from) > parseInt(to)) {
                        alert("service start should be lesser than service end");
                        e.preventDefault();
                    }
                }
            });
            $(window).keydown(function (event) {
                if (event.keyCode == 13) {
                    event.preventDefault();
                    return false;
                }
            });
        });
        function showlatrno(altrnos)
        {
            alert(altrnos);
        }
        function DeleteConfirmation(ele) {
            var getValue = confirm('Are you sure you want to deleted  ?');
            if (getValue) {
                return true;
            }
            else {
                return false;
            }
        }
        function checkDate(sender, args) {
            var selectedDate = sender._selectedDate;

            var todayDate = new Date().format("dd/MM/yyyy");
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
        <div runat="server" id="divconsumer" class="panel-heading">
            <h3 class="panel-title paneltitle">Customer</h3>
        </div>
        <div runat="server" id="diveditCustomer" visible="false" class="panel-heading">
            <h3 class="panel-title paneltitle">Edit Customer</h3>
        </div>

        <div class="panel-body" style="padding-left: 0px;">
            <div class="col-sm-12" style="padding-left: 0px;">
                <%-- <asp:ValidationSummary ID="ValidationSummary1" ValidationGroup="user" ForeColor="Red" ShowModelStateErrors="true" ShowMessageBox="false" ShowSummary="true" ShowValidationErrors="true" runat="server" />--%>
                <div class="col-sm-6 text-left linkBottom linkBottom">
                    <div class="input-group input-group-sm">
                        <span class="input-group-addon" >First Name</span>
                        <asp:TextBox ID="txtfirstname" CssClass="form-control" onkeypress="return ValidateCharacters(event)" placeholder="First Name" runat="server"></asp:TextBox>
                    </div>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtfirstname"
                        ErrorMessage="Enter First Name" ValidationGroup="user" Display="Dynamic" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                </div>
                <div class="col-sm-6 text-left linkBottom">
                    <div class="input-group input-group-sm">
                        <span class="input-group-addon" id="Span1">Last Name</span>
                        <asp:TextBox ID="txtlastname" placeholder="Last Name" onkeypress="return ValidateCharacters(event)" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="txtlastname"
                        ErrorMessage="Enter Last Name" ValidationGroup="user" Display="Dynamic" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>

                </div>
                <div class="col-sm-6 text-left linkBottom">
                    <div class="input-group input-group-sm">
                        <span class="input-group-addon" id="Span2">Email ID</span>
                        <asp:TextBox ID="txtEmailID" placeholder="Email ID" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtEmailID"
                        ErrorMessage="Enter Email ID" ValidationGroup="user" Display="Dynamic" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Enter Correct Email ID"
                        Display="Dynamic" ForeColor="Red" ControlToValidate="txtEmailID" ValidationGroup="user"
                        ValidationExpression="\b[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}\b"></asp:RegularExpressionValidator>

                </div>
                <div class="col-sm-6 text-left linkBottom">
                    <div class="input-group input-group-sm">
                        <span class="input-group-addon" id="Span4">Assign Role</span>
                        <asp:DropDownList ID="ddlRole" runat="server" CssClass="form-control  dropdown">
                            <asp:ListItem Text="--Select Role--" Value="0"></asp:ListItem>
                            <asp:ListItem Text="Customer" Value="4"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" InitialValue="0" runat="server" ControlToValidate="ddlRole"
                        ErrorMessage="Select role" ValidationGroup="user" Display="Dynamic" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                </div>


                <div class="col-sm-6 text-left linkBottom">
                    <div class="input-group input-group-sm">
                        <span class="input-group-addon" id="Span3">MobileNumber</span>
                        <asp:TextBox ID="txtMobileNumber" onkeypress="return ValidateNumber(event)" CssClass="form-control" MaxLength="10" placeholder="Enter Mobile Number" runat="server"></asp:TextBox>
                    </div>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ValidationGroup="user"
                        runat="server" ErrorMessage="Enter Mobile Number"
                        ControlToValidate="txtMobileNumber" Display="Dynamic" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" SetFocusOnError="true" runat="server" ControlToValidate="txtMobileNumber"
                        ErrorMessage="Enter Valid Mobile No" Display="Dynamic" ForeColor="Red" ValidationGroup="user" ValidationExpression="[0-9]{10}"></asp:RegularExpressionValidator>

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
                <div class="col-sm-6 text-left linkBottom">
                    <div class="input-group input-group-sm">
                        <span class="input-group-addon" id="Span7">Gender</span>
                        <asp:DropDownList ID="ddlGender" CssClass="form-control dropdown" runat="server">
                            <asp:ListItem Text="--Select Gender--" Value="0"></asp:ListItem>
                            <asp:ListItem Text="Male" Value="M"></asp:ListItem>
                            <asp:ListItem Text="Female" Value="F"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ValidationGroup="user" runat="server"
                        ErrorMessage="Enter Gender"
                        ControlToValidate="ddlGender"
                        InitialValue="0" Display="Dynamic" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                </div>
                <div class="col-sm-6 text-left linkBottom">
                    <div class="input-group input-group-sm">
                        <span class="input-group-addon" id="Span8">Date of Birth</span>
                        <asp:TextBox ID="txtDob" AutoComplete="off" ClientIDMode="Static"
                            placeholder="Enter Date of Birth" CssClass="form-control" aria-describedby="basic-addon1" runat="server"></asp:TextBox>
                    </div>
                    <asp:CalendarExtender ID="CalendarExtender1" OnClientDateSelectionChanged="checkDate" TargetControlID="txtDob" Format="MM/dd/yyyy" SelectedDate='<%#Convert.ToDateTime(DateTime.Now.ToString("MM/dd/yyyy")) %>' runat="server"></asp:CalendarExtender>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7"
                        runat="server" ValidationGroup="user" Display="Dynamic" ForeColor="Red"
                        ControlToValidate="txtDob" SetFocusOnError="true" ErrorMessage="Enter Date of Birth"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" SetFocusOnError="true" runat="server" ControlToValidate="txtDob"
                        ErrorMessage="Enter Valid date" Display="Dynamic" ForeColor="Red" ValidationGroup="user" ValidationExpression="^(0[1-9]|1[012])[- /.](0[1-9]|[12][0-9]|3[01])[- /.](19|20)\d\d+$"></asp:RegularExpressionValidator>

                </div>
                <div class="col-sm-6 text-left linkBottom"  runat="server" >
                    <div class="input-group input-group-sm">
                        <span class="input-group-addon" id="Span12">Zone</span>
                        <asp:DropDownList ID="ddlZone" AutoPostBack="true" OnSelectedIndexChanged="ddlZone_SelectedIndexChanged" CssClass="form-control dropdown" runat="server">
                        </asp:DropDownList>
                    </div>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator13" ValidationGroup="user" runat="server"
                        ErrorMessage="Enter Zone" InitialValue="0"
                        ControlToValidate="ddlZone" Display="Dynamic" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                </div>
                <div class="col-sm-6 text-left linkBottom"  runat="server" >
                    <div class="input-group input-group-sm">
                        <span class="input-group-addon" id="Span14">Subzone</span>
                        <asp:DropDownList ID="ddlSubZone" AutoPostBack="true" CssClass="form-control dropdown" runat="server">
                        </asp:DropDownList>
                    </div>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator14" ValidationGroup="user" runat="server"
                        ErrorMessage="Enter SubZone" InitialValue="0"
                        ControlToValidate="ddlSubZone" Display="Dynamic" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                </div>
                <div class="col-sm-6 text-left linkBottom">
                    <div class="input-group input-group-sm">
                        <span class="input-group-addon" id="Span5">Locality</span>
                        <asp:TextBox ID="txtaddresslocality" AutoComplete="off" Height="46px" Style="resize: none;"  MaxLength="300"
                            placeholder="Enter Locality" CssClass="form-control" aria-describedby="basic-addon1" runat="server"></asp:TextBox>
                    </div>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5"
                        runat="server" ValidationGroup="user" Display="Dynamic" ForeColor="Red"
                        ControlToValidate="txtaddresslocality" SetFocusOnError="true" ErrorMessage="Enter Locality"></asp:RequiredFieldValidator>
                </div>
                <div class="col-sm-6 text-left linkBottom">
                    <div class="input-group input-group-sm">
                        <span class="input-group-addon" id="Span5">Street</span>
                        <asp:TextBox ID="txtstreet" AutoComplete="off" Height="46px" Style="resize: none;"  MaxLength="300"
                            placeholder="Enter Street" CssClass="form-control" aria-describedby="basic-addon1" runat="server"></asp:TextBox>
                    </div>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator15"
                        runat="server" ValidationGroup="user" Display="Dynamic" ForeColor="Red"
                        ControlToValidate="txtstreet" SetFocusOnError="true" ErrorMessage="Enter Street"></asp:RequiredFieldValidator>
                </div>
                <div class="col-sm-6 text-left linkBottom">
                    <div class="input-group input-group-sm">
                        <span class="input-group-addon" id="Span6">Country</span>
                        <asp:DropDownList ID="ddlCountry" AutoPostBack="true" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged" CssClass="form-control dropdown" runat="server">
                        </asp:DropDownList>
                    </div>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ValidationGroup="user" runat="server"
                        ErrorMessage="Enter Country" InitialValue="0"
                        ControlToValidate="ddlCountry" Display="Dynamic" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                </div>
                <div class="col-sm-6 text-left linkBottom">
                    <div class="input-group input-group-sm">
                        <span class="input-group-addon" id="Span9">State</span>
                        <asp:DropDownList ID="ddlState" AutoPostBack="true" OnSelectedIndexChanged="ddlState_SelectedIndexChanged" CssClass="form-control dropdown" runat="server">
                        </asp:DropDownList>
                    </div>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" ValidationGroup="user" runat="server"
                        ErrorMessage="Enter State" InitialValue="0"
                        ControlToValidate="ddlState" Display="Dynamic" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                </div>
                <div class="col-sm-6 text-left linkBottom">
                    <div class="input-group input-group-sm">
                        <span class="input-group-addon" id="Span10">District</span>
                        <asp:DropDownList ID="ddlDistrict" AutoPostBack="true" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged" CssClass="form-control dropdown" runat="server">
                        </asp:DropDownList>
                    </div>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" ValidationGroup="user" runat="server"
                        ErrorMessage="Enter District" InitialValue="0"
                        ControlToValidate="ddlDistrict" Display="Dynamic" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                </div>
                <div class="col-sm-6 text-left linkBottom">
                    <div class="input-group input-group-sm">
                        <span class="input-group-addon" id="Span11">City</span>
                        <asp:DropDownList ID="ddlCity" CssClass="form-control dropdown" runat="server">
                        </asp:DropDownList>
                    </div>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" ValidationGroup="user" runat="server"
                        ErrorMessage="Enter City" InitialValue="0"
                        ControlToValidate="ddlCity" Display="Dynamic" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                </div>
                <div class="col-sm-6 text-left linkBottom" id="dvStatus" runat="server">
                    <div class="input-group input-group-sm">
                        <span class="input-group-addon" id="Span13">Status</span>
                        <asp:RadioButtonList ID="rdbStatus" Margin="0.5em;" runat="server" RepeatDirection="Horizontal">

                            <asp:ListItem style="margin-right: 20px;" Text="Active" Value="1" />

                            <asp:ListItem Text="InActive" Value="0" />

                        </asp:RadioButtonList>
                    </div>
                </div>
                <div class="col-sm-12">

                    <asp:Button ID="btnSubmit" runat="server" Text="Add" OnClick="btnSubmit_Click" CssClass="btn btn-success" ValidationGroup="user" />
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-success" OnClick="btnCancel_Click" />
                </div>

            </div>
        </div>

    </div>

    <div class="panel panel-success table-responsive" style="padding-left: 15px; padding-top: 15px;">

        <div class="input-group input-group-xs">

            <asp:TextBox ID="txtSearchclient" placeholder="Search Client By Name,EmailID,Status Or Mobile Number" Width="400px"
                CssClass="form-control" runat="server"></asp:TextBox>
            <asp:Button ID="Btnsearch" runat="server" Text="Search" OnClick="btnSearch_OnClick"
                CssClass="btn btn-success" />
        </div>
        <br />



        <div class="panel-heading table-responsive" id="divClientList" runat="server">
            <h3 class="panel-title paneltitle">Customer List</h3>
        </div>

        <asp:HiddenField ID="hdnUserID" runat="server" />


        <asp:Repeater ID="RptAllUser" runat="server" OnItemCommand="RptAllUser_ItemCommand" OnItemDataBound="RptAllUser_ItemDataBound">
            <HeaderTemplate>
                <table class="table table-hover  table-bordered  table-condensed" data-height="400">
                    <thead style="color: White; background-color: #6b9297;">
                        <tr>
                            <th>Name</th>
                            <%--<th>Address</th>--%>
                            <th>Email</th>
                            <%--<th>Gender</th>
                            <th>DOB</th>
                            <th>Country</th>
                            <th>State</th>
                            <th>Distict</th>
                            <th>City</th>--%>
                            <th>Mobile No</th>
                           
                            <th>Created Date</th>
                            <th>Modified Date</th>
                            <th>Status</th>
                            <th>Edit</th>
                            <th>Delete</th>
                        </tr>
                    </thead>
            </HeaderTemplate>
            <ItemTemplate>
                <tbody>
                    <tr>

                        <asp:HiddenField ID="hdnID" runat="server" Value='<%#Eval("UserID") %>' />
                         <asp:HiddenField ID="hdndob" runat="server" Value='<%#Eval("DOB", "{0:MM/dd/yyyy}") %>' />
                        <td>
                            <asp:Label ID="lblname" runat="server" Text='<%#Eval("FirstName") %>'></asp:Label>
                         <b>&nbsp;</b>
                             <asp:Label ID="lblastname" runat="server" Text='<%#Eval("LastName") %>'></asp:Label>
                        </td>
                       
                        <asp:Label ID="lbladdress" Visible="false" runat="server" Text='<%#Eval("CurrentAddress") %>'></asp:Label>

                        <td>
                            <asp:Label ID="lblemail" runat="server" Text='<%#Eval("EmailID") %>'></asp:Label></td>

                        <asp:Label ID="Lblgender" runat="server" Visible="false" Text='<%#Eval("Gender") %>'></asp:Label>
                        <asp:Label ID="lblDOB" runat="server" Visible="false" Text='<%#Eval("DOB") %>'></asp:Label>
                        <asp:Label ID="Lblcname" runat="server" Visible="false" Text='<%#Eval("CountryName") %>'></asp:Label>
                        <asp:Label ID="lblcntryID" runat="server" Visible="false" Text='<%#Eval("CountryID") %>'></asp:Label>
                        <asp:Label ID="Lblsname" runat="server" Visible="false" Text='<%#Eval("StateName") %>'></asp:Label>
                        <asp:Label ID="lblstateID" runat="server" Visible="false" Text='<%#Eval("StateID") %>'></asp:Label>
                        <asp:Label ID="LblDnme" runat="server" Visible="false" Text='<%#Eval("DistrictName") %>'></asp:Label>
                        <asp:Label ID="lbldstrictID" runat="server" Visible="false" Text='<%#Eval("DistrictID") %>'></asp:Label>
                        <asp:Label ID="Lblctyname" runat="server" Visible="false" Text='<%#Eval("CityName") %>'></asp:Label>
                        <asp:Label ID="lblctyID" runat="server" Visible="false" Text='<%#Eval("CityId") %>'></asp:Label>
                        <asp:Label ID="lblzoneid" runat="server" Visible="false" Text='<%#Eval("zoneid") %>'></asp:Label>
                        <asp:Label ID="lblsubzone" runat="server" Visible="false" Text='<%#Eval("subzoneid") %>'></asp:Label>
                        <td>
                            <asp:LinkButton ID="lblmobile" runat="server" Text='<%# Eval("MobileNumber")%>'></asp:LinkButton></td>
                         
                        <td>
                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("Created")%>'></asp:Label>

                        </td>
                        <td>
                            <asp:Label ID="Label2" runat="server" Text='<%# Eval("Modified")%>'></asp:Label>

                        </td>

                        <td>
                            <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("Status")%>'></asp:Label>

                        </td>
                        <td>
                            <center>   <asp:ImageButton ID="ImgBtnEdit" runat="server" Width="24" Height="24"
                                             ImageUrl="~/Static/images/edit.png" CommandName="edit" CommandArgument='<%#Eval("UserID") %>' /></center>
                        </td>
                        <td>
                            <center>   <asp:ImageButton ID="ImgDelete" runat="server" Width="24" Height="24"
                                             ImageUrl="~/Static/images/trash.png" CommandName="delete" CommandArgument='<%#Eval("UserID") %>' /></center>
                        </td>
                    </tr>
                </tbody>
            </ItemTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
        </asp:Repeater>
        <table id="tblPaging" runat="server" class="tblpaging" style="font-size: 12px; clear: both; margin-top: 15px;">
            <tr>
                <td colspan="5"></td>
            </tr>
            <tr>
                <td width="32" valign="top" align="center">
                    <asp:LinkButton ID="lnkFirst" runat="server" OnClick="lnkFirst_Click">First</asp:LinkButton>
                </td>
                <td width="80" valign="top" align="center">
                    <asp:LinkButton ID="lnkPrevious" runat="server" OnClick="lnkPrevious_Click">Previous</asp:LinkButton>
                </td>
                <td>
                    <asp:DataList ID="RepeaterPaging" runat="server" RepeatDirection="Horizontal" OnItemCommand="RepeaterPaging_ItemCommand"
                        OnItemDataBound="RepeaterPaging_ItemDataBound">
                        <ItemTemplate>
                            <asp:LinkButton ID="Pagingbtn" runat="server" CommandArgument='<%# Eval("PageIndex") %>'
                                CommandName="newpage" Text='<%# Eval("PageText") %> ' Width="20px"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:DataList>
                </td>
                <td width="80" valign="top" align="center">
                    <asp:LinkButton ID="lnkNext" runat="server" OnClick="lnkNext_Click">Next</asp:LinkButton>
                </td>
                <td width="80" valign="top" align="center">
                    <asp:LinkButton ID="lnkLast" runat="server" OnClick="lnkLast_Click">Last</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td colspan="2" align="left" height="30">
                    <asp:Label Style="padding-left: 4px;" ID="lblpage" runat="server" Text=""></asp:Label>
                </td>
            </tr>
        </table>
            
    </div>
</asp:Content>
