<%@ Page Title="Add User" Language="C#" MasterPageFile="AdminMaster.Master" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeBehind="AddUser.aspx.cs" Inherits="MakeNMake.Admin.AddUser" %>

<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../Scripts/jquery-2.0.1.min.js"></script>
    <title>Create or Edit User</title>
    <script type="text/javascript">
        window.onload = function () {
        //    $(".table tr.danger").find("input").attr("disabled", "disabled");
        }
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
        });
        function showlatrno(altrnos) {
            alert(altrnos);
        }
        function rdFunction() {
            $("[id*=treeMap] input[type=checkbox]").bind("click", function () {
                var table = $(this).closest("table");
                if (table.next().length > 0 && table.next()[0].tagName == "DIV") {
                    //Is Parent CheckBox
                    var childDiv = table.next();
                    var isChecked = $(this).is(":checked");
                    $("input[type=checkbox]", childDiv).each(function () {
                        if (isChecked) {
                            $(this).prop("checked", true);
                        } else {
                            $(this).removeAttr("checked");
                        }
                    });
                } else {
                    ////Is Child CheckBox

                    var isChildChecked = $(this).is(":checked");
                    var parentDIV = $(this).closest("DIV");
                    if (isChildChecked) {
                        $("input[type=checkbox]", parentDIV.prev()).prop("checked", true);
                    }
                    else {
                        var noOfChildSelected = $("input[type=checkbox]:checked", parentDIV).length;
                        if (noOfChildSelected == 0) {
                            $("input[type=checkbox]", parentDIV.prev()).removeAttr("checked");
                        }
                    }
                }
            });
        };
        function checkDate(sender, args) {
            var selectedDate = sender._selectedDate;
            var todayDate = new Date().format("dd/MM/yyyy");
            if (selectedDate > todayDate) {
                $("#txtDob").val("");
                alert("You cannot select a future date!");
            }
            else if (todayDate.getFullYear() - selectedDate.getFullYear() < 18) {
                $("#txtDob").val("");
                alert("You must be 18 year older to use this application");
            }
        }
        function PageReload() {
            var getValue = confirm('No Record Found ');
            if (getValue) {
                window.location.href = window.location.href;
            }
            else {
                window.location.href = window.location.href;
            }
        }
        function DeleteConfirmation(ele) {
            var getValue = confirm('Are you sure you want to delete ?');
            if (getValue) {
                return true;
            }
            else {
                return false;
            }
        }
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $(window).keydown(function (event) {
                if (event.keyCode == 13) {
                    event.preventDefault();
                    return false;
                }
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="panel panel-success">


        <div runat="server" id="divuser" class="panel-heading">
            <h3 class="panel-title paneltitle">User</h3>
        </div>
        <div runat="server" id="divedituser" visible="false" class="panel-heading">
            <h3 class="panel-title paneltitle">Edit User</h3>
        </div>

        <div class="panel-body" style="padding-left:0px;">
            <div class="col-sm-12" style="padding-left:0px;">
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
                           <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtlastname"
                        ErrorMessage="Enter Last Name" ValidationGroup="user" Display="Dynamic" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
             
                </div>
                <div class="col-sm-6 text-left linkBottom">
                    <div class="input-group input-group-sm">
                        <span class="input-group-addon" id="Span2">Email Id</span>
                        <asp:TextBox ID="txtEmailID" MaxLength="190" placeholder="Email Id" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtEmailID"
                        ErrorMessage="Enter Email Id" ValidationGroup="user" Display="Dynamic" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Enter Correct Email ID"
                        Display="Dynamic" ForeColor="Red" ControlToValidate="txtEmailID" ValidationGroup="user"
                        ValidationExpression="\b[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}\b"></asp:RegularExpressionValidator>

                </div>
                    <div class="col-sm-6 text-left linkBottom">
                    <div class="input-group input-group-sm">
                        <span class="input-group-addon" id="Span7">Date Of Birth</span>
                         <asp:TextBox ID="txtDob" AutoComplete="off"  ClientIDMode="Static"
                            placeholder="Enter Date of Birth" CssClass="form-control" aria-describedby="basic-addon1" runat="server"></asp:TextBox>
                    </div>
                    <asp:CalendarExtender ID="CalendarExtender1" TargetControlID="txtDob" OnClientDateSelectionChanged="checkDate"
                          Format="dd/MM/yyyy" SelectedDate='<%#Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy")) %>'  runat="server"></asp:CalendarExtender>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7"
                        runat="server" ValidationGroup="user"  Display="Dynamic" ForeColor="Red"
                        ControlToValidate="txtDob" SetFocusOnError="true" ErrorMessage="Enter Date of Birth"></asp:RequiredFieldValidator>
                     <asp:RegularExpressionValidator ID="RegularExpressionValidator2"  SetFocusOnError="true" runat="server" ControlToValidate="txtDob"
                                            ErrorMessage="Enter Valid date"  Display="Dynamic" ForeColor="Red" ValidationGroup="user" ValidationExpression="(((0|1)[1-9]|2[1-9]|3[0-1])\/(0[1-9]|1[0-2])\/((19|20)\d\d))$"></asp:RegularExpressionValidator>
                                  
                </div>
               
                
                <div class="col-sm-6 text-left linkBottom">
                    <div class="input-group input-group-sm">
                        <span class="input-group-addon" id="Span11">Mobile Number</span>
                        <asp:TextBox ID="txtMobile" MaxLength="10" placeholder="Mobile Number" onkeypress="return ValidateNumber(event)" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtMobile"
                        ErrorMessage="Enter Mobile Number" ValidationGroup="user" Display="Dynamic" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>

                </div>
                <div id="dvAlternateMobile" runat="server" visible="false">
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

                     <div class="col-sm-6 text-left linkBottom" id="modifiedDate" runat="server" visible="false">
                    <div class="input-group input-group-sm">
                        <span class="input-group-addon" id="Span8">Modified Date</span>
                        <asp:TextBox ID="txtModified" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>

                </div>
                <div class="col-sm-6 text-left linkBottom">
                    <div class="input-group input-group-sm">
                        <span class="input-group-addon" id="Span4">Assign Role</span>
                        <asp:DropDownList ID="ddlRole" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlRole_SelectedIndexChanged" CssClass="form-control  dropdown"></asp:DropDownList>
                    </div>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" InitialValue="0" runat="server" ControlToValidate="ddlRole"
                        ErrorMessage="Select Role" ValidationGroup="user" Display="Dynamic" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                </div>
                 <div class="col-sm-6 text-left linkBottom" id="dvaddress" runat="server" visible="false" >
                    <div class="input-group input-group-sm">
                        <span class="input-group-addon" id="Span9">Locality</span>
                        <asp:TextBox ID="txtaddresslocality" AutoComplete="off" style="resize: none;"  MaxLength="300"
                            placeholder="Enter Locality" CssClass="form-control" aria-describedby="basic-addon1" runat="server"></asp:TextBox>
                    </div>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8"
                        runat="server" ValidationGroup="user"  Display="Dynamic" ForeColor="Red"
                        ControlToValidate="txtaddresslocality" SetFocusOnError="true" ErrorMessage="Enter Locality"></asp:RequiredFieldValidator>
                </div>
                 <div class="col-sm-6 text-left linkBottom" id="divadd2" runat="server" visible="false" >
                    <div class="input-group input-group-sm">
                        <span class="input-group-addon" id="Span9">Street</span>
                        <asp:TextBox ID="txtstreet" AutoComplete="off" style="resize: none;" MaxLength="300"
                            placeholder="Enter Street" CssClass="form-control" aria-describedby="basic-addon1" runat="server"></asp:TextBox>
                    </div>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator14"
                        runat="server" ValidationGroup="user"  Display="Dynamic" ForeColor="Red"
                        ControlToValidate="txtstreet" SetFocusOnError="true" ErrorMessage="Enter Street"></asp:RequiredFieldValidator>
                </div>
                <div class="col-sm-6 text-left linkBottom" id="dvcountry" runat="server" visible="false" >
                    <div class="input-group input-group-sm">
                        <span class="input-group-addon" id="Span10">Country</span>
                        <asp:DropDownList ID="ddlCountry" AutoPostBack="true" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged" CssClass="form-control dropdown" runat="server">
                        </asp:DropDownList>
                    </div>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" ValidationGroup="user" runat="server"
                        ErrorMessage="Enter Country" InitialValue="0"
                        ControlToValidate="ddlCountry"  Display="Dynamic" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                </div>
                 <div class="col-sm-6 text-left linkBottom" id="dvstate" runat="server" visible="false" >
                    <div class="input-group input-group-sm">
                        <span class="input-group-addon" id="Span12">State</span>
                        <asp:DropDownList ID="ddlState" AutoPostBack="true" OnSelectedIndexChanged="ddlState_SelectedIndexChanged" CssClass="form-control dropdown" runat="server">
                        </asp:DropDownList>
                    </div>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" ValidationGroup="user" runat="server"
                        ErrorMessage="Enter State" InitialValue="0"
                        ControlToValidate="ddlState"  Display="Dynamic" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                </div>
                 <div class="col-sm-6 text-left linkBottom" id="dvdistrict" runat="server" visible="false" >
                    <div class="input-group input-group-sm">
                        <span class="input-group-addon" id="Span13">District</span>
                        <asp:DropDownList ID="ddlDistrict" CssClass="form-control dropdown" AutoPostBack="true" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged" runat="server">
                        </asp:DropDownList>
                    </div>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator12"  ValidationGroup="user" runat="server"
                        ErrorMessage="Enter District" InitialValue="0"
                        ControlToValidate="ddlDistrict"  Display="Dynamic" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                </div>
                 <div id="dvcity" runat="server" visible="false" class="col-sm-6 text-left linkBottom">
                    <div class="input-group input-group-sm">
                        <span class="input-group-addon" id="Span14">City</span>
                        <asp:DropDownList ID="ddlCity" CssClass="form-control dropdown" runat="server">
                        </asp:DropDownList>
                    </div>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator13" ValidationGroup="user" runat="server"
                        ErrorMessage="Enter City" InitialValue="0"
                        ControlToValidate="ddlCity"  Display="Dynamic" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                </div>
                <div id="dvZone" runat="server" visible="false" class="col-sm-6 text-left linkBottom">
                    <div class="input-group input-group-sm">
                        <span class="input-group-addon" id="Span5">Zone</span>
                        <asp:DropDownList ID="ddlZone" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlZone_SelectedIndexChanged" CssClass="form-control  dropdown"></asp:DropDownList>
                    </div>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" InitialValue="0" runat="server" ControlToValidate="ddlZone"
                        ErrorMessage="Select Zone" ValidationGroup="user" Display="Dynamic" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                </div>
                <div id="dvSubZone" runat="server" visible="false" class="col-sm-6 text-left linkBottom">
                    <div class="input-group input-group-sm">
                        <span class="input-group-addon" id="Span6">Subzone</span>
                        <asp:DropDownList ID="ddlSubZone" runat="server" CssClass="form-control  dropdown"></asp:DropDownList>
                    </div>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" InitialValue="0" runat="server" ControlToValidate="ddlSubZone"
                        ErrorMessage="Select Subzone cities" ValidationGroup="user" Display="Dynamic" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                </div>


                <div id="dvPages" runat="server" visible="false" style="width: 600px; height: 300px; overflow: scroll; position: relative;" class="col-sm-12 text-left linkBottom">
                    <div style="position: absolute; font-size: 14px; color: #6B9297; clear: both; height: 40px; line-height: 40px;">Access to Pages</div>
                    <div style="position: absolute; top: 45px; font-size: 12px; clear: both;">
                        <asp:TreeView ID="treeMap" Width="500" ClientIDMode="Static"
                            ShowCheckBoxes="All" ShowLines="true" runat="server" ImageSet="XPFileExplorer">
                        </asp:TreeView>
                    </div>
                </div>
                <div class="btn-group col-sm-6 ">
                    <div class="input-group input-group-sm">
                        <%--<span class="input-group-addon" id="Span7">Status</span>--%>
                        <asp:RadioButtonList ID="rdbStatus" Margin="0.5em;" runat="server" RepeatDirection="Horizontal">

                            <asp:ListItem style="margin-right: 20px;" Text="Active" Value="1" />

                            <asp:ListItem Text="Inactive" Value="0" />

                        </asp:RadioButtonList>
                    </div>
                </div>
                <div class="col-sm-12 text-left linkBottom" style="margin-top: 20px;">
                    <asp:Button ID="btnSubmit" runat="server" Text="Add" OnClick="btnSubmit_Click" CssClass="btn btn-success" ValidationGroup="user" />
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-success" OnClick="btnCancel_Click" />
                </div>
            </div>
        </div>
    </div>



    <div class="panel panel-success">
        <div class="panel-heading">
            <h3 class="panel-title paneltitle">User List</h3>
        </div>
        <div class="panel-body table-responsive">
            <div runat="server" visible="false" class="col-sm-6 text-left linkBottom" style="padding-left: 0px;">
                <div class="input-group input-group-sm">


                    <span class="input-group-addon" id="Span3">Select number of items to be displayed per page </span>

                    <asp:DropDownList ID="ddlIndex" CssClass="form-control dropdown" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlIndex_SelectedIndexChanged">
                    </asp:DropDownList>
                </div>
            </div>
            <div class="input-group input-group-xs">

                <asp:TextBox ID="txtSearchclient" placeholder="Search User By Name,Role,EmailID Or Mobile Number" Width="400px"
                    CssClass="form-control" runat="server"></asp:TextBox>
                <asp:Button ID="Button1" runat="server" Text="Search" Style="margin-left: 10px;"
                    CssClass="btn btn-success" OnClick="Button1_Click" />
                <asp:Button ID="btnRefresh" runat="server" Text="Refresh" Style="margin-left: 10px;"
                    CssClass="btn btn-success" OnClick="btnRefresh_Click"/>
            </div>
             <asp:HiddenField ID="hdnuserID" runat="server" Value="" />
            <br />
            <div class="panel-heading table-responsive" id="divClientList" runat="server" style="padding-left: 0px;">
               
                
                <asp:Repeater ID="RptAllUser" runat="server" OnItemCommand="RptAllUser_ItemCommand" OnItemDataBound="RptAllUser_ItemDataBound">
                    <HeaderTemplate>
                        <table class="table table-hover  table-bordered  table-condensed" data-height="400">
                            <thead style="color: White; background-color: #6b9297;">
                                <tr>
                                    <th>Name</th>
                                    <th>Email ID</th>
                                    <th>Role</th>
                                    <th>Mobile No</th>
                                     
                                    <th>Status</th>
                                    <th>Created</th>
                                    <th>Modified</th>
                                  
                                    <th>Edit</th>
                                    <th>Delete</th>
                                </tr>
                            </thead>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tbody>
                            <tr class='<%# Convert.ToInt32( Eval("Status"))==1?"":"danger"%>'>
                                <td>
                                    <asp:HiddenField ID="hdnID" runat="server" Value='<%#Eval("UserID") %>' />
                                    <asp:HiddenField ID="hdnRoleID" runat="server" Value='<%#Eval("RoleID") %>' />
                                     <asp:HiddenField ID="hdncountry" runat="server" Value='<%#Eval("Country") %>' />
                                     <asp:HiddenField ID="hdnstate" runat="server" Value='<%#Eval("State") %>' />
                                     <asp:HiddenField ID="hdndistrict" runat="server" Value='<%#Eval("District") %>' />
                                     <asp:HiddenField ID="hdncity" runat="server" Value='<%#Eval("CityID") %>' />
                                    <asp:HiddenField ID="hdnZone" runat="server" Value='<%#Eval("ZoneID") %>' />
                                    <asp:HiddenField ID="hdnSubZone" runat="server" Value='<%#Eval("SubZoneID") %>' />
                                   <asp:HiddenField ID="hdndob" runat="server" Value='<%#Eval("DOB", "{0:MM/dd/yyyy}") %>' />
                                    <asp:HiddenField ID="hdnaddress" runat="server" Value='<%#Eval("Address") %>' />
                                    <asp:Label ID="lblname" runat="server" Text='<%#Eval("FName") %>'>

                                    </asp:Label><asp:Label ID="lbllname" runat="server" Text='<%#Eval("LName") %>'></asp:Label></td>

                                <td>
                                    <asp:Label ID="lblEmailid" runat="server" Text='<%#Eval("EmailID") %>'></asp:Label></td>
                                <td>
                                    <asp:Label ID="Lblrolename" runat="server" Text='<%#Eval("RoleName") %>'></asp:Label>
                                </td>
                                <td>
                                    <asp:LinkButton ID="lblmobile" runat="server" Text='<%# Eval("MobileNumber")%>'></asp:LinkButton></td>
                                   
                                <td>
                                    <asp:Label ID="lblStatus" ClientIDMode="Static" runat="server" Text='<%# Convert.ToInt32( Eval("Status"))==1?"Active":"InActive"%>'></asp:Label></td>
                                <td>
                                    <asp:Label ID="lblcreated" runat="server" Text='<%# Eval("Created", "{0:dd/MM/yyyy}")%>'></asp:Label></td>
                                <td>
                                    <asp:Label ID="lblModified" runat="server" Text='<%# (Eval("Modified")==DBNull.Value?(Convert.ToDateTime(Eval("Created")).ToString("dd/MM/yyyy")): Convert.ToDateTime(Eval("Modified")).ToString("dd/MM/yyyy")) %>'></asp:Label></td>
                              
                           
                                 <td>
                                    <center>  <asp:ImageButton ID="ImgBtnEdit" runat="server" Width="24" Height="24"
                                    ImageUrl="~/Static/images/edit.png" CommandName="edit" CommandArgument='<%#Eval("UserID") %>' /></center>
                                </td>
                                <td>
                                    <center><asp:ImageButton ID="ImgBtnDelete" OnClientClick="javascript:return DeleteConfirmation(this);"
                                    runat="server" ImageUrl="~/Static/images/trash.png"
                                    CommandName="delete" /></center>
                                </td>
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
        </div>
    </div>
</asp:Content>
