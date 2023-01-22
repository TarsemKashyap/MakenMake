<%@ Page Title="Service Plan" Language="C#" MasterPageFile="AdminMaster.Master" AutoEventWireup="true" CodeBehind="ServicePlan.aspx.cs" Inherits="MakeNMake.Admin.ServicePlan" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../Scripts/jquery-2.0.1.min.js"></script>
    <script src="../Static/js/validation.js"></script>
    <script type="text/javascript">
        function DeleteConfirmation(ele) {
            var getValue = confirm('Are you sure you want to delete,deletion of serviceplan will delete  discount related to it ?');
            if (getValue) {
                return true;
            }
            else {
                return false;
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
    <div class="panel panel-success table-responsive">
        <div class="panel-heading">
            <h3 class="panel-title paneltitle">Service Plan</h3>
        </div>
        <div class="panel-body" style="padding-left: 0px;">
            <div class="col-sm-12" style="padding: 0px;">

                <div class="col-sm-6 text-left linkBottom">
                    <div class="input-group input-group-sm">
                        <span class="input-group-addon" id="Span1">Service </span>
                        <asp:DropDownList ID="ddlService" CssClass="form-control dropdown" AutoPostBack="true" OnSelectedIndexChanged="ddlService_SelectedIndexChanged" runat="server">
                        </asp:DropDownList>
                    </div>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="service" runat="server" ErrorMessage="Select Service"
                        ControlToValidate="ddlService"
                        InitialValue="0" Display="Dynamic" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                </div>
                <div class="col-sm-6 text-left linkBottom">
                    <div class="input-group input-group-sm">
                        <span class="input-group-addon" id="Span7">Service Plan</span>
                        <asp:DropDownList ID="ddlPlan" CssClass="form-control dropdown"
                            OnSelectedIndexChanged="ddlPlan_SelectedIndexChanged" AutoPostBack="true" runat="server">
                            <asp:ListItem Text="--Select Service Plan--" Value="0"></asp:ListItem>
                            <asp:ListItem Text="Unlimited" Value="U"></asp:ListItem>
                            <asp:ListItem Text="Make Your Plan" Value="M"></asp:ListItem>
                            <asp:ListItem Text="Flexi" Value="F"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ValidationGroup="service" runat="server" ErrorMessage="Select Service Plan"
                        ControlToValidate="ddlPlan"
                        InitialValue="0" Display="Dynamic" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                </div>
                <div id="dvFixedmake" runat="server" visible="false">
                    <div class="col-sm-6 text-left linkBottom">
                        <div class="input-group input-group-sm">
                            <span class="input-group-addon" id="Span9">Duration</span>
                            <asp:TextBox ID="txtDuration" AutoComplete="off" MaxLength="3" onKeyPress="return ValidateNumber(event);"
                                placeholder="Enter Duration in minutes" CssClass="form-control" aria-describedby="basic-addon1" runat="server"></asp:TextBox>
                            <span class="input-group-addon" id="Span11">minutes</span>
                        </div>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8"
                            runat="server" ValidationGroup="service" Display="Dynamic" ForeColor="Red"
                            ControlToValidate="txtDuration" SetFocusOnError="true" ErrorMessage="Enter the Duration"></asp:RequiredFieldValidator>
                    </div>

                    <div class="col-sm-6 text-left linkBottom">
                        <div class="input-group input-group-sm">
                            <span class="input-group-addon" id="Span10">Amount</span>
                            <asp:TextBox ID="txtamount" AutoComplete="off" onKeyPress="return ValidateNumber(event);"
                                placeholder="Enter Amount" CssClass="form-control" aria-describedby="basic-addon1" runat="server"></asp:TextBox>
                        </div>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9"
                            runat="server" ValidationGroup="service" Display="Dynamic" ForeColor="Red"
                            ControlToValidate="txtamount" SetFocusOnError="true" ErrorMessage="Enter the Amount"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" Display="Dynamic" ForeColor="Red"
                            ErrorMessage="Enter valid Ammount" ValidationGroup="abd" ValidationExpression="^-?\d*\.?\d*" ControlToValidate="txtamount"></asp:RegularExpressionValidator>

                    </div>
                     <div id="dvDiscount" runat="server" visible="false" class="col-sm-6 text-left linkBottom">
                        <div class="input-group input-group-sm">
                            <span class="input-group-addon" id="Span14">Discount</span>
                            <asp:TextBox ID="txtFixedDiscount" AutoComplete="off"
                                placeholder="Enter Discount" CssClass="form-control" aria-describedby="basic-addon1" runat="server"></asp:TextBox>
                        </div>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10"
                            runat="server" ValidationGroup="service" Display="Dynamic" ForeColor="Red"
                            ControlToValidate="txtFixedDiscount" SetFocusOnError="true" ErrorMessage="Enter the Discount"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" Display="Dynamic" ForeColor="Red"
                            ErrorMessage="Enter valid discount" ValidationGroup="service" ValidationExpression="^-?\d*\.?\d*"
                            ControlToValidate="txtFixedDiscount"></asp:RegularExpressionValidator>

                     </div>
                    <div id="dvmakeVisitRequired" visible="false" runat="server" class="col-sm-6 text-left linkBottom">
                        <div class="input-group input-group-sm">
                            <span class="input-group-addon" id="Span6">Visit Required</span>
                            <asp:RadioButtonList ID="rdbMakeVisit" RepeatDirection="Horizontal" runat="server">
                                <asp:ListItem Text="Yes" style="margin-right: 20px;" Value="1"></asp:ListItem>
                                <asp:ListItem Text="No" Value="0"></asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                    </div>
                </div>
                <div id="dvunlimited" runat="server" visible="false">
                    <div class="col-sm-6 text-left linkBottom">
                        <div class="input-group input-group-sm">
                            <span class="input-group-addon" id="Span3">Category</span>
                            <asp:DropDownList ID="ddlCategory" CssClass="form-control dropdown" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged" AutoPostBack="true" runat="server">
                                <asp:ListItem Text="--Select Category--" Value="0"></asp:ListItem>
                                <asp:ListItem Text="A" Value="A"></asp:ListItem>
                                <asp:ListItem Text="B" Value="B"></asp:ListItem>
                                <asp:ListItem Text="C" Value="C"></asp:ListItem>
                                <asp:ListItem Text="D" Value="D"></asp:ListItem>
                                <asp:ListItem Text="E" Value="E"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3"
                            runat="server" ValidationGroup="service" InitialValue="0" Display="Dynamic" ForeColor="Red"
                            ControlToValidate="ddlCategory" SetFocusOnError="true" ErrorMessage="Select Category"></asp:RequiredFieldValidator>
                        
                    </div>
                    <div class="col-sm-12 text-left linkBottom" style="padding-left: 0px;">
                        <div class="col-sm-2">
                            <div class="input-group input-group-sm">
                                <span class="input-group-addon" id="Span2">Area (Sqft)</span>
                            </div>
                        </div>
                        <div class="col-sm-1">
                            <div class="input-group input-group-sm">
                                <span class="input-group-addon" id="Span12">From</span>
                            </div>
                        </div>
                        <div class="col-sm-4">
                            <asp:DropDownList ID="ddlArea" Enabled="false" ClientIDMode="Static" CssClass="form-control dropdown" runat="server">
                                <asp:ListItem Text="--Select Area--" Value="0"></asp:ListItem>
                                <asp:ListItem Text="0 Sqft" Value="01"></asp:ListItem>
                                <asp:ListItem Text="500 Sqft" Value="500"></asp:ListItem>
                                <asp:ListItem Text="1501 Sqft" Value="1501"></asp:ListItem>
                                <asp:ListItem Text="3001 Sqft" Value="3001"></asp:ListItem>
                                <asp:ListItem Text="4501 Sqft" Value="4501"></asp:ListItem>
                                <asp:ListItem Text="6001 Sqft" Value="6001"></asp:ListItem>
                                <asp:ListItem Text="7500 Sqft" Value="7500"></asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" InitialValue="0"
                                runat="server" ValidationGroup="service" Display="Dynamic" ForeColor="Red"
                                ControlToValidate="ddlArea" SetFocusOnError="true" ErrorMessage="Select the Category"></asp:RequiredFieldValidator>
                        </div>
                        <div id="dvto" runat="server" class="col-sm-1">
                            <div class="input-group input-group-sm">
                                <span class="input-group-addon" id="Span13">To</span>
                            </div>
                        </div>
                        <div class="col-sm-4">
                            <asp:DropDownList ID="ddlAreaToSqft" Enabled="false" ClientIDMode="Static" CssClass="form-control dropdown" runat="server">
                                <asp:ListItem Text="--Select Area--" Value="0"></asp:ListItem>
                                <asp:ListItem Text="1500 Sqft" Value="1500"></asp:ListItem>
                                <asp:ListItem Text="3000 Sqft" Value="3000"></asp:ListItem>
                                <asp:ListItem Text="4500 Sqft" Value="4500"></asp:ListItem>
                                <asp:ListItem Text="6000 Sqft" Value="6000"></asp:ListItem>
                                <asp:ListItem Text="7500 Sqft" Value="7500"></asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" InitialValue="0"
                                runat="server" ValidationGroup="service" Display="Dynamic" ForeColor="Red"
                                ControlToValidate="ddlAreaToSqft" SetFocusOnError="true" ErrorMessage="Select the Category"></asp:RequiredFieldValidator>

                        </div>
                    </div>
                    
                    <div class="col-sm-6 text-left linkBottom">
                        <div class="input-group input-group-sm">
                            <span class="input-group-addon" id="Span4">Amount</span>
                            <asp:TextBox ID="txtuAmount" AutoComplete="off" onKeyPress="return ValidateNumber(event);"
                                placeholder="Enter Amount" CssClass="form-control" aria-describedby="basic-addon1" runat="server"></asp:TextBox>
                        </div>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4"
                            runat="server" ValidationGroup="service" Display="Dynamic" ForeColor="Red"
                            ControlToValidate="txtuAmount" SetFocusOnError="true" ErrorMessage="Enter the Amount"></asp:RequiredFieldValidator>
                    </div>
                    <div class="col-sm-6 text-left linkBottom">
                        <div class="input-group input-group-sm">
                            <span class="input-group-addon" id="Span5">Discount</span>
                            <asp:TextBox ID="txtUDiscount" AutoComplete="off"
                                placeholder="Enter Discount" CssClass="form-control" aria-describedby="basic-addon1" runat="server"></asp:TextBox>
                        </div>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5"
                            runat="server" ValidationGroup="service" Display="Dynamic" ForeColor="Red"
                            ControlToValidate="txtUDiscount" SetFocusOnError="true" ErrorMessage="Enter the Discount"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" Display="Dynamic" ForeColor="Red"
                            ErrorMessage="Enter valid discount" ValidationGroup="service" ValidationExpression="^-?\d*\.?\d*"
                            ControlToValidate="txtUDiscount"></asp:RegularExpressionValidator>

                    </div>
                    <div id="dvVisitRequired" runat="server" class="col-sm-6 text-left linkBottom">
                        <div class="input-group input-group-sm">
                            <span class="input-group-addon" id="Span8">Visit Required</span>
                            <asp:RadioButtonList ID="rdbUnimitedVisit" RepeatDirection="Horizontal" runat="server">
                                <asp:ListItem style="margin-right: 20px;" Text="Yes" Value="1"></asp:ListItem>
                                <asp:ListItem Text="No" Value="0"></asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                    </div>

                </div>
                <div class="col-sm-12 text-left linkBottom">
                    <asp:Button ID="btnAdd" runat="server" ClientIDMode="Static" CssClass="btn btn-success" Text="Add" OnClick="btnAdd_Click" ValidationGroup="service" />
                    <asp:Button ID="btnCancel" OnClick="btnCancel_Click" runat="server" Text="Cancel" CssClass="btn btn-success" />
                </div>
            </div>

        </div>
    </div>
    <div class="panel panel-success" id="divService" runat="server" visible="false">
        <div class="panel-body table-responsive">

            <div class="input-group input-group-xs">

                <asp:TextBox ID="txtSearchclient" placeholder="Search: By Service Name,Category,Service Type or Plan" Width="500px"
                    CssClass="form-control" runat="server"></asp:TextBox>
                <asp:Button ID="Button1" runat="server" Text="Search" Style="margin-left: 10px;"
                    CssClass="btn btn-success" OnClick="Button1_Click" />
            </div>
            <br />
            <div class="col-sm-12 clear" style="padding-left: 0px;">
                <div class="table-responsive">
                    <asp:HiddenField ID="hdnServiceID" runat="server" />
                    <asp:Repeater ID="RptService" runat="server" OnItemCommand="RptService_ItemCommand">
                        <HeaderTemplate>
                            <table class="table table-hover  table-bordered  table-condensed" data-height="400">
                                <thead style="color: White; background-color: #6b9297;">
                                    <tr>
                                        <th>Service</th>
                                        <th>Category</th>
                                        <th>ServiceType</th>
                                        <th>PlanType</th>
                                        <th>Area-Category<%---Amount--%></th>
                                        <th>Duration</th>
                                        <th>Amount</th>
                                        <th>Visit Required</th>
                                        <th>Edit </th>
                                        <th>Delete</th>
                                    </tr>
                                </thead>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tbody>
                                <tr>
                                    <td>
                                        <asp:HiddenField ID="hdnID" runat="server" Value='<%#Eval("ServiceID") %>' />
                                           <asp:HiddenField ID="hdnFDiscount" runat="server" Value='<%#Eval("FDiscount") %>' />
                                        <asp:Label ID="lblName" runat="server" Text='<%#Eval("ServiceName") %>'></asp:Label></td>
                                    <td>
                                        <asp:Label ID="lblCategory" runat="server" Text='<%#Eval("category") %>'></asp:Label></td>
                                    <td>
                                        <asp:Label ID="lblType" runat="server" Text='<%#Eval("Servicetype") %>'></asp:Label></td>
                                    <td>
                                        <asp:Label ID="lblPlan" runat="server" Text='<%#Eval("PlanType") %>'></asp:Label></td>
                                    <td>
                                        <asp:Label ID="lblArea" runat="server" Text='<%# Convert.ToString(Eval("AreaCatAmount")).Replace("$","<br/><hr/>")%>'></asp:Label></td>
                                    <td>
                                        <asp:Label ID="lblDuration" runat="server" Text='<%# Eval("Duration")%>'></asp:Label></td>
                                    <td>
                                        <asp:Label ID="lblAmount" runat="server" Text='<%# Convert.ToString(Eval("Amount")).Replace("$","<br/><hr/>")%>'></asp:Label></td>
                                    <td>
                                        <asp:Label ID="lblvisit" runat="server" Text='<%# Convert.ToString(Eval("VisitRequired")).Replace("$","<br/><hr/>")%>'></asp:Label></td>
                                    <td>
                                        <center>     <asp:ImageButton ID="ImgBtnEdit" runat="server" Visible='<%#ISDefined(Convert.ToInt32(Eval("ServicePlanID")))%>'
                                             ImageUrl="~/Static/images/edit.png" CommandName="edit" CommandArgument='<%#Eval("ServicePlanID") %>' /></center>
                                    </td>
                                    <td>
                                        <center> <asp:ImageButton ID="ImgBtnDelete" OnClientClick="javascript:return DeleteConfirmation(this);"
                                            runat="server" ImageUrl="~/Static/images/trash.png" Visible='<%#ISDefined(Convert.ToInt32(Eval("ServicePlanID")))%>'
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
            <table class="tblpaging" style="font-size: 12px; clear: both; margin-top: 15px;">
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
</asp:Content>
