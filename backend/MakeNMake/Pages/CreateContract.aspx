<%@ Page Title="Create Contract" Language="C#" MasterPageFile="~/Pages/AdminMaster.Master" AutoEventWireup="true" CodeBehind="CreateContract.aspx.cs" Inherits="MakeNMake.Pages.CreateContract" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="panel panel-success">
        <div class="panel-heading">
            <h3 id="Quotetitle"  runat="server" class="panel-title paneltitle"></h3>
        </div>
        <div class="panel-body" style="padding-left: 0px;">
            <asp:HiddenField ID="hdnUserID" runat="server"  />
            <div class="col-sm-12" style="padding-left: 0px;">
                <div class="col-sm-6 text-left linkBottom linkBottom">
                    <div class="input-group input-group-sm">
                        <span class="input-group-addon">First Name</span>
                        <asp:TextBox ID="txtfirstname" CssClass="form-control" ReadOnly="true" placeholder="First Name" runat="server"></asp:TextBox>
                    </div>

                </div>
                <div class="col-sm-6 text-left linkBottom linkBottom">
                    <div class="input-group input-group-sm">
                        <span class="input-group-addon">Last Name</span>
                        <asp:TextBox ID="txtlastname" CssClass="form-control" ReadOnly="true" placeholder="Last Name" runat="server"></asp:TextBox>
                    </div>

                </div>
                <div class="col-sm-6 text-left linkBottom">
                    <div class="input-group input-group-sm">
                        <span class="input-group-addon">Email Id</span>
                        <asp:TextBox ID="txtEmailID" MaxLength="190" ReadOnly="true" placeholder="Email Id" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>


                </div>
                <div class="col-sm-6 text-left linkBottom">
                    <div class="input-group input-group-sm">
                        <span class="input-group-addon">Phone No</span>
                        <asp:TextBox ID="txtphono" MaxLength="190" ReadOnly="true" CssClass="form-control" runat="server"></asp:TextBox>
                        <asp:HiddenField ID="hdUserID" runat="server" />
                    </div>


                </div>
                <div class="col-sm-6 text-left linkBottom">
                    <div class="input-group input-group-sm">
                        <span class="input-group-addon" id="Span2">Address</span>
                        <asp:TextBox ID="txtAddress" MaxLength="190" ReadOnly="true" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>


                </div>
            </div>
        </div>
        <div class="panel-body">
            <asp:Repeater ID="rp_quotatioContract" runat="server"  OnItemDataBound="rp_quotatioContract_ItemDataBound">
                <HeaderTemplate>
                    <table class="table table-hover  table-bordered  table-condensed tbladdon" data-height="400">
                        <thead style="color: White; background-color: #6b9297;">
                            <tr>
                                <th></th>
                                <th>Service Name</th>
                                <th>Service Type</th>
                                <th>Service Categry</th>
                                <th>Service Plan</th>
                                <th runat="server" id="thDuration">Duration</th>
                                <th runat="server" id="thCalls">No of calls</th>
                                <th>Discount</th>
                                <th runat="server" id="thamount">Amount</th>
                            </tr>
                        </thead>
                </HeaderTemplate>
                <ItemTemplate>
                    <tbody>
                        <tr>
                            <td>
                                <asp:CheckBox ID="cb_service" runat="server" />
                            </td>
                            <td>
                                <asp:HiddenField ID="hdnServceID" runat="server" Value='<%#Eval("serviceid") %>'/>
                                     <asp:HiddenField ID="hdnPlanID" runat="server" Value='<%#Eval("serviceplanid") %>'/>
                                <asp:Label ID="lbServiceName" runat="server" Text='<%#Eval("ServiceName") %>'></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lbServiceType" runat="server" Text='<%#Convert.ToString(Eval("ServiceType")).ToLower()=="b" ?"Basic":"AddOn" %>'></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblcategory" runat="server" Text='<%#Eval("ServiceCategory") %>'></asp:Label>
                            </td>

                            <td>
                                <asp:Label ID="lblPlan" runat="server" Text='<%#Eval("SPlan") %>'></asp:Label>
                            </td>
                             <td runat="server" id="tdDuration">
                             <asp:Label ID="lblduration" runat="server" Text='<%#Eval("duration") %>'></asp:Label>
                            </td>
                            <td runat="server" id="tdCalls">
                            <asp:TextBox ID="txtQuantity" CssClass="quantity" onkeypress="return ValidateNumber(event)" runat="server"></asp:TextBox>
                            </td>
                            <td >
                            <asp:TextBox ID="txtdiscount" CssClass="discount" onkeypress="return ValidateDecimalNumber(event,this)" runat="server"></asp:TextBox>
                            </td>
                            <td runat="server" id="tdamount">
                               <asp:TextBox ID="txtAmount" CssClass="amount" onkeypress="return ValidateNumber(event)" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                    </tbody>
                </ItemTemplate>
                <FooterTemplate>
                    </table>
                </FooterTemplate>
            </asp:Repeater>
        </div>
        <div class="panel-body" style="padding-left: 0px;">
            <div class="col-sm-6 text-left linkBottom linkBottom">
                <div class="input-group input-group-sm">
                    <span class="input-group-addon">Payment mentioned in quote</span>
                    <asp:Label ID="lbTotalPayment" CssClass="form-control" runat="server"></asp:Label>
                </div>
            </div>
            <div class="col-sm-12">
                <asp:Button ID="lb_proceedTopay" runat="server" CssClass="btn btn-success" OnClientClick="javascript:return CheckValid();"  Text="Sent to Client" OnClick="lb_proceedTopay_Click" />
                 <asp:Button ID="Button1" runat="server" CssClass="btn btn-success" Text="Back" OnClick="Button1_Click"/>
            </div>
        </div>
    </div>

    <script type="text/javascript">
        function Amount() {
            var isUncheck = false;
            $(".tbladdon input[type='checkbox']:checked").parent().parent().each(function (ele, ind) {
                var quantityEle = ind.getElementsByClassName('amount')[0].value;
                if (quantityEle == "") {
                    isUncheck = true;
                }
            });
            if (isUncheck) {
                alert("Please Enter amount");
                return false;
            }
            else {
                return true;
            }
        }

        function Discount() {
            var isUncheck = false;
            $(".tbladdon input[type='checkbox']:checked").parent().parent().each(function (ele, ind) {
                var quantityEle = ind.getElementsByClassName('discount')[0].value;
                if (quantityEle == "") {
                    isUncheck = true;
                }
            });
            if (isUncheck) {
                alert("Please Enter Discount in case of no discount please enter 0");
                return false;
            }
            else {
                return true;
            }

        }

        function Quantity() {
            var isUncheck = false;
            $(".tbladdon input[type='checkbox']:checked").parent().parent().each(function (ele, ind) {
                var quantityEle = ind.getElementsByClassName('quantity')[0].value;
                if (quantityEle == "") {
                    isUncheck = true;
                }
            });
            if (isUncheck) {
                alert("Please Enter Number of calls ");
                return false;
            }
            else {
                return true;
            }

        }
        function CheckValid() {
            if (Amount()) {
                if (Discount()) {
                    return true;
                }
                else {
                    return false;
                }
            }
            else {
                return false;
            }
        }
</script>
</asp:Content>
