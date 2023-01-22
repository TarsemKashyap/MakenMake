<%@ Page Title="Services" Language="C#" MasterPageFile="AdminMaster.Master" AutoEventWireup="true" CodeBehind="Services.aspx.cs" Inherits="MakeNMake.Admin.Services" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="act" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
    <script src="../Scripts/jquery-2.0.1.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            //$("#btnAdd").click(function (e) {
            //    var from = $("#txtservicestart").val();
            //    var to = $("#txtserviceend").val();
            //    if (from == "") {
            //        alert("Please enter  service start time");
            //        e.preventDefault();
            //    }
            //    if (to == "") {
            //        alert("Please enter  service end time");
            //        e.preventDefault();
            //    }
            //    if (from != "" && to != "") {
            //        if (parseInt(from) > parseInt(to)) {
            //            alert("service start should be lesser than service end");
            //            e.preventDefault();
            //        }
            //    }
            //});
            $(window).keydown(function (event) {
                if (event.keyCode == 13) {
                    event.preventDefault();
                    return false;
                }
            });
        });
        function DeleteConfirmation(ele) {
            var getValue = confirm('Are you sure you want to delete,deletion of service will delete all plan and discount related to it ?');
            if (getValue) {
                return true;
            }
            else {
                return false;
            }
        }
        function ShowMsg(ele) {
            alert(ele.nextElementSibling.value);
        }
        function sayKeyCode(event, v) {
            var TextBox = document.getElementById('<%=txtservicestart.ClientID%>');
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
        function sayKeyCode1(event, v) {
            var TextBox = document.getElementById('<%=txtserviceend.ClientID%>');
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="panel panel-success">
        <div class="panel-heading">
            <h3 class="panel-title paneltitle">Services</h3>
        </div>
        <div class="panel-body" style="padding-left:0px;">
            <div class="col-sm-12" style="padding-left:0px;">

                <%--<asp:ValidationSummary ID="ValidationSummary1" ValidationGroup="service" ShowSummary="true" ForeColor="Red" runat="server" />--%>
                <div class="col-sm-6 text-left linkBottom">
                    <div class="input-group input-group-sm">
                        <span class="input-group-addon" >Service name</span>
                        <asp:TextBox ID="txtname" AutoComplete="off" onKeyPress="return ValidateServiceCharacters(event);"
                            placeholder="Enter Service Name" CssClass="form-control" aria-describedby="basic-addon1" runat="server"></asp:TextBox>
                    </div>
                    <asp:RequiredFieldValidator ID="validservicename" runat="server" ValidationGroup="service" Display="Dynamic" ForeColor="Red" ControlToValidate="txtname" SetFocusOnError="true" ErrorMessage="Enter the Service Name"></asp:RequiredFieldValidator>
                </div>
                <div class="col-sm-6 text-left linkBottom">
                    <div class="input-group input-group-sm">
                        <span class="input-group-addon" id="Span5">Service Description</span>
                        <asp:TextBox ID="txtdesc" AutoComplete="off" onKeyPress="return ValidateCharacters(event);" CssClass="form-control"
                            placeholder="Enter Description" aria-describedby="basic-addon1" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="col-sm-12 text-left linkBottom" style="padding:0px;">
                    <div class="col-sm-2">
                        <div class=" input-group input-group-sm">
                            <span class="input-group-addon" id="Span1">Service Available<br /> (12 hr format)</span>
                        </div>
                    </div>
                    <div class="col-sm-1" >
                        <div class=" input-group input-group-sm" >
                            <span class="input-group-addon" id="Span6">From</span>
                        </div>
                    </div>
                    <div class="col-sm-2">
                        <asp:TextBox ID="txtservicestart" placeholder="HH:MM" AutoComplete="off" onkeyup="sayKeyCode(event,this.value)" 
                             ClientIDMode="Static" CssClass="form-control input-sm" aria-describedby="Span1" MaxLength="5" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="service" Display="Dynamic" 
                            ForeColor="Red" ControlToValidate="txtservicestart" SetFocusOnError="true" ErrorMessage="Enter Service Available Time From"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="rev" runat="server" ErrorMessage="InvalidTime" Display="Dynamic" ForeColor="Red" 
                            ValidationGroup="service" ControlToValidate="txtservicestart"
                            ValidationExpression="^([0-1]?[0-9]|2[0-4]):([0-5][0-9])(:[0-5][0-9])?">
                        </asp:RegularExpressionValidator>
                        <act:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" FilterType="Custom,numbers" ValidChars=":" TargetControlID="txtservicestart" runat="server"></act:FilteredTextBoxExtender>
                    </div>
                    <div class="col-sm-2">
                        <asp:DropDownList ID="ddlFrmTime" CssClass="form-control dropdown" runat="server">
                            <asp:ListItem Text="AM"></asp:ListItem> 
                            <asp:ListItem Text="PM"></asp:ListItem>
                        </asp:DropDownList>
                        </div>
                    <div class="col-sm-1 ">
                        <div class=" input-group input-group-sm">
                            <span class="input-group-addon" id="Span8">To</span>
                        </div>
                    </div>
                    <div class="col-sm-2">
                        <asp:TextBox ID="txtserviceend" AutoComplete="off" placeholder="HH:MM" onkeyup="sayKeyCode1(event,this.value)"
                              ClientIDMode="Static" CssClass="form-control input-sm" aria-describedby="Span2" MaxLength="5" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ValidationGroup="service" Display="Dynamic" 
                            ForeColor="Red" ControlToValidate="txtserviceend" SetFocusOnError="true" ErrorMessage="Enter the Service Available Time To"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="rvendtime" runat="server" ErrorMessage="InvalidTime" Display="Dynamic" 
                            ForeColor="Red" ValidationGroup="service" ControlToValidate="txtserviceend"
                            ValidationExpression="^([0-1]?[0-9]|2[0-4]):([0-5][0-9])(:[0-5][0-9])?">
                        </asp:RegularExpressionValidator>
                        <act:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" FilterType="Custom,numbers" ValidChars=":" TargetControlID="txtserviceend" runat="server"></act:FilteredTextBoxExtender>

                    </div>
                    <div class="col-sm-2">
                        <asp:DropDownList ID="ddlToTime" CssClass="form-control dropdown" runat="server">
                            <asp:ListItem Text="PM"></asp:ListItem> 
                            <asp:ListItem Text="AM"></asp:ListItem>
                        </asp:DropDownList>
                        </div>
                </div>

                <div class="col-sm-6 text-left linkBottom">
                    <div class="input-group input-group-sm">
                        <span class="input-group-addon" id="Span3">Service Catgory</span>
                        <asp:DropDownList ID="ddlCategory" CssClass="form-control dropdown" runat="server">
                            <asp:ListItem Text="--Select Service Category--" Value="0"></asp:ListItem>
                            <asp:ListItem Text="Domestic" Value="D"></asp:ListItem>
                            <asp:ListItem Text="Commercial" Value="C"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="service" ErrorMessage="Enter Service Category" ControlToValidate="ddlCategory"
                        InitialValue="0" Display="Dynamic" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                </div>
                <div class="col-sm-6 text-left linkBottom">
                    <div class="input-group input-group-sm">
                        <span class="input-group-addon" id="Span4">Service Type</span>
                        <asp:DropDownList ID="ddlServiceType" CssClass="form-control dropdown" runat="server">
                            <asp:ListItem Text="--Select Service Type--" Value="0"></asp:ListItem>
                            <asp:ListItem Text="Add On" Value="A"></asp:ListItem>
                            <asp:ListItem Text="Basic" Value="B"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="service" runat="server" ErrorMessage="Enter Service Type" ControlToValidate="ddlServiceType"
                        InitialValue="0" Display="Dynamic" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                </div>
                 <div class="col-sm-6 text-left linkBottom">
                     <div class="input-group input-group-sm">
                   <span class="input-group-addon" id="Span2">Status</span>
                 <asp:RadioButtonList ID="rdbstatus" style="margin-top:5px; margin-left:10px;" RepeatLayout="Table"  RepeatDirection="Horizontal" runat="server">
                                <asp:ListItem Value="0" Text="Active" Selected="True" style="margin-right:20px"></asp:ListItem>
                                <asp:ListItem Value="1" Text="Inactive"></asp:ListItem>
                            </asp:RadioButtonList>
                    </div>
                    </div>
                <div class="col-sm-12 text-left linkBottom">
                        <asp:Button ID="btnAdd" runat="server" ClientIDMode="Static" CssClass="btn btn-success" Text="Add" OnClick="btnAdd_Click" ValidationGroup="service" />
            <asp:Button ID="btnCancel" OnClick="btnCancel_Click" runat="server" Text="Cancel" CssClass="btn btn-success" />
        
                    </div>
            </div>

        </div>


    </div>
    <div class="panel panel-success">
        <div class="panel-body table-responsive" style="padding-left:0px;">
                 <div class="input-group input-group-xs" style="margin-bottom:20px;clear:both;padding-left:18px;">

                <asp:TextBox ID="txtSearchclient" placeholder="Search: By Service Name,Category,Service Type or Plan" Width="500px"
                    CssClass="form-control" runat="server"></asp:TextBox>
                <asp:Button ID="Button1" runat="server" Text="Search" Style="margin-left: 10px;"
                    CssClass="btn btn-success" OnClick="Button1_Click" />
            </div>
            <div class="col-sm-12 clear">
                <div class="table-responsive">
                    <asp:HiddenField ID="hdnServiceID" runat="server" />
                    <asp:Repeater ID="RptService" runat="server" OnItemCommand="RptService_ItemCommand" OnItemDataBound="RptService_ItemDataBound">
                        <HeaderTemplate>
                            <table class="table table-hover  table-bordered  table-condensed" data-height="400">
                                <thead style="color: White; background-color: #6b9297;">
                                    <tr>
                                        <th>Service</th>
                                        <th>Description</th>
                                        <th>Category</th>
                                        <th>ServiceType</th>
                                        <th>PlanType</th>
                                        <th>Available From</th>
                                        <th>Available To</th>
                                        <th>Status</th>
                                        <th>Edit</th>
                                        <th>Delete</th>
                                    </tr>
                                </thead>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tbody>
                                <tr>
                                    <td>
                                        <asp:HiddenField ID="hdnID" runat="server" Value='<%#Eval("ServiceID") %>' />
                                        <asp:Label ID="lblName" runat="server" Text='<%#Eval("ServiceName") %>'></asp:Label></td>
                                    <td>
                                        <asp:Label ID="lbldesc" runat="server" Text='<%#Eval("ServiceDesc") %>'></asp:Label>
                                        <asp:LinkButton ID="lnkBtnMore" ClientIDMode="Static" runat="server">....read more</asp:LinkButton>
                                    <asp:HiddenField ID="hdnFullMsg" ClientIDMode="Static" runat="server" Value='<%#Eval("ServiceDesc") %>' />
                                    </td>
                                    <td>
                                        <asp:Label ID="lblCategory" runat="server" Text='<%#Eval("category") %>'></asp:Label></td>
                                    <td>
                                        <asp:Label ID="lblType" runat="server" Text='<%#Eval("Servicetype") %>'></asp:Label></td>
                                    <td>
                                        <asp:Label ID="lblPlan" runat="server" Text='<%#Eval("PlanType") %>'></asp:Label></td>
                                    <td>
                                        <asp:Label ID="lblFrom" runat="server" Text='<%# Eval("Fromtime")%>'></asp:Label></td>
                                    <td>
                                        <asp:Label ID="lblTo" runat="server" Text='<%# Eval("ToTime")%>'></asp:Label></td>
                                     <td>
                                        <asp:Label ID="lblstatus" runat="server" Text='<%# Eval("EStatus")%>'></asp:Label></td>
                                    <td>
                                     <center>  <asp:ImageButton ID="ImgBtnEdit" runat="server" ImageUrl="~/Static/images/edit.png" CommandName="edit" CommandArgument='<%#Eval("ServiceID") %>' /></center>
                                    </td>
                                    <td>
                                     <center>    <asp:ImageButton ID="ImgBtnDelete" OnClientClick="javascript:return DeleteConfirmation(this);"
                                            runat="server" ImageUrl="~/Static/images/trash.png"
                                            CommandName="delete" CommandArgument='<%#Eval("ServicePlanID") %>' /></center>
                                    </td>
                            </tbody>
                        </ItemTemplate>
                        <FooterTemplate>
                            </table>
                        </FooterTemplate>
                    </asp:Repeater>
                </div>
            </div>
        </div>
         <table class="tblpaging" runat="server" id="tblpaging" style="font-size: 12px;clear:both;margin-top:15px;">
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
