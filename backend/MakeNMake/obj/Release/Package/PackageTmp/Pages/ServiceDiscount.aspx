<%@ Page Title="Service Discount" Language="C#" MasterPageFile="AdminMaster.Master" AutoEventWireup="true" CodeBehind="ServiceDiscount.aspx.cs" Inherits="MakeNMake.Admin.ServiceDiscount" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"><script src="../Scripts/jquery-2.0.1.min.js"></script>
    <script src="../Static/js/validation.js"></script>
    <script type="text/javascript">
      

        $(document).on('click', '#btnAdd', function (e) {
            var val = $("#txtdiscount").val();
            if (val != "") {
                if (parseInt(val) > 100) {
                    alert("Please enter discount less than 100");
                    e.preventDefault();
                }
            }

        });
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="panel panel-success">
        <div class="panel-heading">
            <h3 class="panel-title paneltitle">Service Vertical Discount</h3>
        </div>
        <div class="panel-body" style="padding-left:0px;">
            <div class="col-sm-12" style="padding-left:0px;">
             <%--   <asp:ValidationSummary ID="ValidationSummary1" ValidationGroup="service" ShowSummary="true" ForeColor="Red" runat="server" />  --%>
                 <div class="col-sm-6 text-left linkBottom">
                  <div class="input-group input-group-sm">
                        <span class="input-group-addon" id="Span1">Service Plan</span> 
                         <asp:DropDownList ID="ddlService" runat="server" CssClass="form-control dropdown"  >
                              <asp:ListItem Text="--Select Service Plan--" Value="0"></asp:ListItem>
                            <asp:ListItem Text="Unlimited" Value="U"></asp:ListItem>
                            <asp:ListItem Text="Make Your Plan" Value="M"></asp:ListItem>
                            <asp:ListItem Text="Flexi" Value="F"></asp:ListItem>
                        </asp:DropDownList>
                    </div>                    
                   <asp:RequiredFieldValidator ID="RequiredFieldValidator1"  ValidationGroup="service" runat="server" ErrorMessage="Select Service"
                        ControlToValidate="ddlService"
                  InitialValue="0"  Display="Dynamic" ForeColor="Red"  SetFocusOnError="true"></asp:RequiredFieldValidator>
                </div>   
             <div class="col-sm-6 text-left linkBottom">
                  <div class="input-group input-group-sm">
                        <span class="input-group-addon" id="Span4">Plan </span> 
                         <asp:DropDownList ID="ddlPlanMode" CssClass="form-control dropdown" OnSelectedIndexChanged="ddlPlan_SelectedIndexChanged"  AutoPostBack="true" runat="server">
                            <asp:ListItem Text="--Select Service Discount Mode--" Value="0"></asp:ListItem>
                            <asp:ListItem Text="Fixed" Value="1"></asp:ListItem>
                            <asp:ListItem Text="Variable" Value="2"></asp:ListItem>
                        </asp:DropDownList>
                    </div>                    
                   <asp:RequiredFieldValidator ID="RequiredFieldValidator4"  ValidationGroup="service" runat="server" ErrorMessage="Select Discount Mode"
                        ControlToValidate="ddlPlanMode"
                  InitialValue="0"  Display="Dynamic" ForeColor="Red"  SetFocusOnError="true"></asp:RequiredFieldValidator>
                </div>         
                

                   <div id="dvFromCall" runat="server"  visible="false" class="col-sm-6 text-left linkBottom">
                    <div class="input-group input-group-sm">
                        <span class="input-group-addon" id="Span5">From Call</span>
                        <asp:TextBox ID="txtFromCall" AutoComplete="off" MaxLength="4" onKeyPress="return ValidateNumber(event);" 
                            placeholder="Enter From Call" CssClass="form-control" aria-describedby="basic-addon1" runat="server"></asp:TextBox>                        
                    </div>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5"
                         runat="server" ValidationGroup="service"  Display="Dynamic" ForeColor="Red" 
                        ControlToValidate="txtFromCall" SetFocusOnError="true"   ErrorMessage="Enter the From Call"></asp:RequiredFieldValidator>
                </div>
                   <div id="dvToCall" runat="server" visible="false"  class="col-sm-6 text-left linkBottom">
                    <div class="input-group input-group-sm">
                        <span class="input-group-addon" id="Span6">To Call</span>
                        <asp:TextBox ID="txtToCall" AutoComplete="off" MaxLength="4" onKeyPress="return ValidateNumber(event);" 
                            placeholder="Enter To Call" CssClass="form-control" aria-describedby="basic-addon1" runat="server"></asp:TextBox>                        
                    </div>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6"
                         runat="server" ValidationGroup="service"  Display="Dynamic" ForeColor="Red" 
                        ControlToValidate="txtToCall" SetFocusOnError="true"   ErrorMessage="Enter the To Call"></asp:RequiredFieldValidator>
                </div>
                <div class="col-sm-6 text-left linkBottom">
                <div class="input-group input-group-sm">
                 <span class="input-group-addon" id="Span3">Discount</span>
                   <asp:TextBox ID="txtdiscount" runat="server" ClientIDMode="Static" MaxLength="3" CssClass="form-control" aria-describedby="Span3"
                         AutoComplete="off" ></asp:TextBox> 
                 </div>      <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"  Display="Dynamic" ForeColor="Red" ErrorMessage="Enter Discount" ValidationGroup="service" ControlToValidate="txtdiscount"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"   Display="Dynamic" ForeColor="Red"
                        ErrorMessage="Enter valid discount" ValidationGroup="service" ValidationExpression="^-?\d*\.?\d*" ControlToValidate="txtdiscount"></asp:RegularExpressionValidator>
                    
            </div>
                 <div class="col-sm-6 text-left linkBottom">
                  <div class="input-group input-group-sm">
                        <span class="input-group-addon" id="Span2">Status </span> 
                         
                        <asp:RadioButtonList ID="ddlStatus" style="margin-left:10px;margin-top:3px;" 
                             RepeatLayout="Table" RepeatDirection="Horizontal" runat="server">
                                <asp:ListItem Text="Active" Value="true" style="margin-right: 20px"></asp:ListItem>
                                <asp:ListItem Text="Inactive" Value="false"></asp:ListItem>
                            </asp:RadioButtonList>
                    </div>                    
                   <asp:RequiredFieldValidator ID="RequiredFieldValidator3"  ValidationGroup="service" runat="server" ErrorMessage="Select Status"
                        ControlToValidate="ddlStatus"
                  InitialValue="0"  Display="Dynamic" ForeColor="Red"  SetFocusOnError="true"></asp:RequiredFieldValidator>
                </div> 
                 <div class="col-sm-12 text-left linkBottom">
                     
            <asp:Button ID="btnAdd" runat="server" ClientIDMode="Static" CssClass="btn btn-success" Text="Add"  OnClick="btnAdd_Click" ValidationGroup="service"/>
            
<asp:Button ID="btnCancel" OnClick="btnCancel_Click" runat="server" Text="Cancel"  CssClass="btn btn-success"  />
                     </div>
            </div>
        </div>
    </div>

    <div class="panel panel-success">
        <div class="panel-body">
            <div class="col-sm-12 clear" style="padding-left:0px;">
                <div class="table-responsive">
                    <asp:HiddenField ID="hdnDiscountID" runat="server" />
                    <asp:Repeater ID="RptService" runat="server" OnItemCommand="RptService_ItemCommand">
                        <HeaderTemplate>
                            <table class="table table-hover  table-bordered  table-condensed" data-height="400">
                                <thead style="color: White; background-color: #6b9297;">
                                    <tr>
                                        <th>Service Plan</th>
                                        <th>Discount On Calls</th>
                                        <th>Discount (in %)</th>
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
                                        <asp:HiddenField ID="hdnID" runat="server" Value='<%#Eval("DiscountID") %>' />
                                         <asp:HiddenField ID="hdnIsFixed" runat="server" Value='<%#Eval("IsFixed") %>' />
                                        <asp:Label ID="lblName" runat="server" Text='<%#Eval("ServicePlan") %>'></asp:Label></td>
                                    <td>
                                        <asp:Label ID="lblCallDiscount" runat="server" Text='<%#Eval("CallDiscount") %>'></asp:Label></td>
                                    <td>
                                        <asp:Label ID="lblDiscount" runat="server" Text='<%# Convert.ToString(Eval("Discount")).Replace("$","<br/>")%>'></asp:Label></td>
                                    <td>
                                        <asp:Label ID="lblStatus" runat="server" Text='<%# Convert.ToInt32(Eval("Status"))==1?"Active":"InActive"%>'></asp:Label></td>
                                    <td>
                                      <center>  <asp:ImageButton ID="ImgBtnEdit" runat="server" Visible='<%#ISDefined(Convert.ToInt32(Eval("DiscountID")))%>'
                                             ImageUrl="~/Static/images/edit.png" CommandName="edit"/></center>
                                    </td>
                                    <td>
                                        <center><asp:ImageButton ID="ImgBtnDelete" OnClientClick="javascript:return DeleteConfirmation(this);"
                                            runat="server" ImageUrl="~/Static/images/trash.png" Visible='<%#ISDefined(Convert.ToInt32(Eval("DiscountID")))%>'
                                            CommandName="delete" CommandArgument='<%#Eval("DiscountID") %>' /></center>
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
    </div>
</asp:Content>
