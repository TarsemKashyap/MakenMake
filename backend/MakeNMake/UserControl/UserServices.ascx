<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserServices.ascx.cs" Inherits="MakeNMake.UserControl.UserServices" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>


<script type="text/javascript">
    function CheckAmount(ele) {
        var quantity = parseInt(ele.selectedIndex);
        if (quantity < 2) {
            ele.selectedIndex = 1;
            alert("Please select minimun 2 calls  to avail the Make your Own plan");
        }
        else {
            var unlimited = parseFloat(ele.parentNode.previousElementSibling.value);
            var amount = parseFloat(ele.parentNode.previousElementSibling.previousElementSibling.children[0].textContent);

            var totalMake = amount * quantity;
            if (unlimited > totalMake) {
                if (confirm('Are you sure to buy this plan, as the same plan available in unlimited category with less amount.')) {
                    alert('Thanks for confirming');
                } else {
                    ele.selectedIndex = 0;
                }
            }
        }
    }
    function AllowVisit(val) {
        if (val == 1) {
            alert("Allow us to visit your site for quote");
        }
    }
    //function CheckSameIds(ele) {
    //    if (ele.checked) {
    //        var Servicevalue = ele.parentNode.attributes.getNamedItem("serviceid").value;
    //        $('.tblbasic tr td:first-child').find('span').children().attr("disabled", false).each(function (index, element) {
    //            element.setAttribute("checked", false);
    //        });//.prop("checked", false);;
    //        //ele.checked = true;
    //        //ele.disabled = true;
    //        $('.tblbasic tr td:first-child').find('span[serviceid="' + Servicevalue + '"]').children().each(function (index, element) {
    //            element.setAttribute('checked', true).attr("disabled", true);
    //        });
    //    }
    //}
    function checkDate(sender, args) {
        if (sender._selectedDate.format("dd/MM/yyyy") == new Date().format("dd/MM/yyyy")) {
        }
        else if (sender._selectedDate < new Date()) {
            alert("You cannot select a day earlier than today!");
            sender._selectedDate = new Date();
            // set the date back to the current date
            sender._textbox.set_Value(sender._selectedDate.format(sender._format))
        }
    }
    
</script>
<div class="panel panel-success">
    <div class="panel-heading">
        <h3 class="panel-title paneltitle">Select Package</h3>
    </div>
    <div class="panel-body" style="padding-left:0px;">
        <div class="col-md-12" style="padding-left: 0px;">
            <%--<asp:ValidationSummary ID="ValidationSummary1" ValidationGroup="service" ForeColor="Red" ShowModelStateErrors="true" ShowMessageBox="false" ShowSummary="true" ShowValidationErrors="true" runat="server" />--%>
            <div class="col-md-6 text-left linkBottom">
                <div class="input-group input-group-sm">
                    <span class="input-group-addon">Service category </span>
                    <asp:DropDownList ID="ddlcategory" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlcategory_SelectedIndexChanged"
                        CssClass="form-control dropdown">
                        <asp:ListItem Text="--Select Category--" Value="0"></asp:ListItem>
                        <asp:ListItem Text="Domestic" Value="D"></asp:ListItem>
                        <asp:ListItem Text="Commercial" Value="C"></asp:ListItem>
                    </asp:DropDownList>
                </div>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please select Category"
                    ControlToValidate="ddlcategory" Display="Dynamic" ForeColor="Red" InitialValue="0" ValidationGroup="service" SetFocusOnError="true"></asp:RequiredFieldValidator>
            </div>
            <div class="col-md-6 text-left linkBottom">
                <div class="input-group input-group-sm">
                    <span class="input-group-addon" id="Span7">Service Plan</span>
                    <asp:DropDownList ID="ddlPlan" CssClass="form-control dropdown" OnSelectedIndexChanged="ddlPlan_SelectedIndexChanged" AutoPostBack="true" runat="server">
                        <asp:ListItem Text="--Select Service Plan--" Value="0"></asp:ListItem>
                        <asp:ListItem Text="Unlimited" Value="U"></asp:ListItem>
                        <asp:ListItem Text="Make Your Plan" Value="M"></asp:ListItem>
                        <asp:ListItem Text="Flexi" Value="F"></asp:ListItem>
                        <%--<asp:ListItem Text="NA" Value="A"></asp:ListItem>--%>
                    </asp:DropDownList>
                </div>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ValidationGroup="service" runat="server" ErrorMessage="Enter Service Plan"
                    ControlToValidate="ddlPlan"
                    InitialValue="0" Display="Dynamic" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>

            </div>
            <%--<div class="col-md-6 text-left linkBottom">
                    <asp:Button ID="btnSearchPlan" runat="server" Text="Find Service" ValidationGroup="service" CssClass="btn btn-success" OnClick="btnSearchPla_Click" />
                </div>--%>
        </div>
    </div>
</div>

<div id="dvbasic" runat="server" visible="false" class="panel panel-success">
    <div class="panel-heading">
        <h3 class="panel-title paneltitle">Basic Services</h3>
    </div>
    <div class="panel-body">
        <asp:Label ID="lblBasicMsg" runat="server" ></asp:Label>
        <div class="table-responsive">
            <asp:Repeater ID="RptService" runat="server" OnItemDataBound="RptService_ItemDataBound" OnItemCommand="RptService_ItemCommand">
                <HeaderTemplate>
                    <table class="table table-hover  table-bordered  table-condensed tblbasic" data-height="400">
                        <thead style="color: White; background-color: #6b9297;">
                            <tr>
                                <th></th>
                                <th>Service</th>
                                <th>Category</th>
                                <%-- <th>ServiceType</th>--%>
                                <th id="unlimitedarea" runat="server">Area (Sqft)</th>
                                <th id="unlimitedcategory" runat="server">Category</th>
                                <th>Duration (in mins)</th>
                                <th>Amount</th>
                                <th>Visit Required</th>
                                <th id="calls" runat="server">Number of Calls</th>
                            </tr>
                        </thead>
                        <tbody>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td>
                            <asp:CheckBox ID="chkService" runat="server"  Enabled='<%# Convert.ToInt32(Eval("chkService"))==1?false:true %>'
                                OnCheckedChanged="chkService_CheckedChanged" dataService='<%# Eval("UCategory")%>'
                                Checked='<%# Convert.ToInt32(Eval("chkService"))==1?true:false %>' />
                            <asp:HiddenField ID="hdnID" runat="server" 
                                Value='<%#Eval("ServiceID") %>' />
                        </td>
                        <td>
                            <asp:Label ID="lblName" runat="server" Text='<%#Eval("ServiceName") %>'></asp:Label></td>
                        <td>
                            <asp:Label ID="lblEmail" runat="server" Text='<%#Eval("Category") %>'></asp:Label></td>
                        <asp:HiddenField ID="hdnServiceType" runat="server" Value='<%#Eval("ServiceType") %>' />
                        <asp:HiddenField ID="hdnplans" runat="server" Value='<%#Eval("Plans") %>' />
                        <asp:HiddenField ID="hdnPlanID" runat="server" Value='<%#Eval("planid") %>' />
                        <asp:HiddenField ID="hdnNoOfCalls" runat="server" Value='<%#Eval("NofCalls") %>' />
                        <asp:HiddenField ID="hdnUnlimitedPlanID" runat="server" Value='<%#Eval("UPlanID") %>' />
                        <td id="tdArea" runat="server">
                            <asp:Label ID="lblArea" runat="server" Text='<%# Eval("Area")%>'></asp:Label></td>
                        <td id="tdCategory" runat="server">
                            <asp:Label ID="lblUCategory" runat="server" Text='<%# Eval("UCategory")%>'></asp:Label></td>
                        <td>
                            <asp:Label ID="lblDuration" runat="server" Text='<%# Eval("Duration")%>'></asp:Label></td>
                        <td>
                            <asp:Label ID="lblAmount" runat="server" Text='<%# Eval("Amount")%>'></asp:Label>
                            <asp:LinkButton ID="lnkBtnMore" runat="server" Text="Fill Visiting Request Form" CommandName="fillform"
                                CommandArgument='<%#string.Format("{0}:{1}:{2}:{3}:{4}:{5}:{6}",Eval("ServiceID"),Eval("ServiceName"),Eval("planid"),Eval("Plans"),Eval("Category"),Eval("UPlanID"),"Basic") %>' Visible="false"></asp:LinkButton>
                        </td>
                        <td>
                            <asp:Label ID="lblVisit" runat="server" Text='<%# Eval("VisitRequired")%>'></asp:Label>
                        </td>
                        <asp:HiddenField ID="hdnUnlimted" ClientIDMode="Static" runat="server" Value='<%#Eval("unlimitedamount") %>' />

                        <td id="tdcalls" runat="server">
                            <asp:DropDownList ID="ddlQuantity" runat="server">
                          <%--      <asp:ListItem Text="0" Value="0"></asp:ListItem>--%>
                                <asp:ListItem Text="1" Value="1"></asp:ListItem>
                                <asp:ListItem Text="2" Value="2"></asp:ListItem>
                                <asp:ListItem Text="3" Value="3"></asp:ListItem>
                                <asp:ListItem Text="4" Value="4"></asp:ListItem>
                                <asp:ListItem Text="5" Value="5"></asp:ListItem>
                                <asp:ListItem Text="6" Value="6"></asp:ListItem>
                                <asp:ListItem Text="7" Value="7"></asp:ListItem>
                                <asp:ListItem Text="8" Value="8"></asp:ListItem>
                                <asp:ListItem Text="9" Value="9"></asp:ListItem>
                                <asp:ListItem Text="10" Value="10"></asp:ListItem>
                                <asp:ListItem Text="11" Value="11"></asp:ListItem>
                                <asp:ListItem Text="12" Value="12"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                </ItemTemplate>
                <FooterTemplate>
                    </tbody>
                    </table>
                </FooterTemplate>
            </asp:Repeater>
        </div>
    </div>
</div>

<div class="col-12 text-left " id="dvaddonHeading" visible="false" runat="server" style="padding: 0px; clear: both;">
    <asp:Label ID="Label1" runat="server" Text="** You can select from the AddOns too **" ></asp:Label>
</div>
<div id="dvaddon" runat="server" visible="false" class="panel panel-success">


    <div class="panel-heading">
        <h3 class="panel-title paneltitle">AddOn Services</h3>
    </div>
    <div class="panel-body">
        <asp:Label ID="lblAddOnmmsg" runat="server" ></asp:Label>
        <div class="table-responsive" style="clear: both;">
            <asp:Repeater ID="RptAddonServices" runat="server" OnItemDataBound="RptAddonServices_ItemDataBound" OnItemCommand="RptAddonServices_ItemCommand">
                <HeaderTemplate>
                    <table class="table table-hover  table-bordered  table-condensed tbladdon" data-height="400">
                        <thead style="color: White; background-color: #6b9297;">
                            <tr>
                                <th></th>
                                <th>Service</th>
                                <th>Category</th>
                                <%-- <th>ServiceType</th>--%>
                                <th id="unlimitedarea" runat="server">Area (Sqft)</th>
                                <th id="unlimitedcategory" runat="server">Category</th>
                                <th>Duration (in mins)</th>
                                <th>Amount</th>
                                <th>Visit Required</th>
                                <th id="calls" runat="server">Number of Calls</th>
                            </tr>
                        </thead>
                        <tbody>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td>
                            <asp:CheckBox ID="chkService" runat="server" 
                                Checked='<%# Convert.ToInt32(Eval("chkService"))==1?true:false %>'  />
                            <asp:HiddenField ID="hdnID" runat="server" 
                                Value='<%#Eval("ServiceID") %>' />
                        </td>
                        <td>
                            <asp:Label ID="lblName" runat="server" Text='<%#Eval("ServiceName") %>'></asp:Label></td>
                        <td>
                            <asp:Label ID="lblEmail" runat="server" Text='<%#Eval("Category") %>'></asp:Label></td>
                        <asp:HiddenField ID="hdnServiceType" runat="server" Value='<%#Eval("ServiceType") %>' />
                        <asp:HiddenField ID="hdnplans" runat="server" Value='<%#Eval("Plans") %>' />
                        <asp:HiddenField ID="hdnPlanID" runat="server" Value='<%#Eval("planid") %>' />
                        <asp:HiddenField ID="hdnNoOfCalls" runat="server" Value='<%#Eval("NofCalls") %>' />
                        <asp:HiddenField ID="hdnUnlimitedPlanID" runat="server" Value='<%#Eval("UPlanID") %>' />
                        <td id="tdArea" runat="server">
                            <asp:Label ID="lblArea" runat="server" Text='<%# Eval("Area")%>'></asp:Label></td>
                        <td id="tdCategory" runat="server">
                            <asp:Label ID="lblUCategory" runat="server" Text='<%# Eval("UCategory")%>'></asp:Label></td>
                        <td>
                            <asp:Label ID="lblDuration" runat="server" Text='<%# Eval("Duration")%>'></asp:Label></td>
                        <td>
                            <asp:Label ID="lblAmount" runat="server" Text='<%# Eval("Amount")%>'></asp:Label>

                            <asp:LinkButton ID="lnkBtnMore" runat="server" Text="Fill Visiting Request Form" CommandName="fillform"
                                CommandArgument='<%#string.Format("{0}:{1}:{2}:{3}:{4}:{5}:{6}",Eval("ServiceID"),Eval("ServiceName"),Eval("planid"),Eval("Plans"),Eval("Category"),Eval("UPlanID"),"Basic") %>' Visible="false"></asp:LinkButton>

                        </td>
                        <asp:HiddenField ID="hdnUnlimted" ClientIDMode="Static" runat="server" Value='<%#Eval("unlimitedamount") %>' />
                        <td>
                            <asp:Label ID="lblVisit" runat="server" Text='<%# Eval("VisitRequired")%>'></asp:Label>
                        </td>
                        <td id="tdcalls" runat="server">
                            <%--<asp:DropDownList ID="ddlQuantity" runat="server">
                                <asp:ListItem Text="1" Value="1"></asp:ListItem>
                                <asp:ListItem Text="2" Value="2"></asp:ListItem>
                                <asp:ListItem Text="3" Value="3"></asp:ListItem>
                                <asp:ListItem Text="4" Value="4"></asp:ListItem>
                                <asp:ListItem Text="5" Value="5"></asp:ListItem>
                                <asp:ListItem Text="6" Value="6"></asp:ListItem>
                                <asp:ListItem Text="7" Value="7"></asp:ListItem>
                                <asp:ListItem Text="8" Value="8"></asp:ListItem>
                                <asp:ListItem Text="9" Value="9"></asp:ListItem>
                                <asp:ListItem Text="10" Value="10"></asp:ListItem>
                            </asp:DropDownList>--%>
                             <asp:TextBox ID="txtQuantity" CssClass="quantity" onkeypress="return ValidateNumber(event)" runat="server"></asp:TextBox>
                        </td>
                </ItemTemplate>
                <FooterTemplate>
                    </tbody>
                    </table>
                </FooterTemplate>
            </asp:Repeater>
        </div>
    </div>
    <%--<div class="panel-footer">
            <asp:Button ID="btnProceedToPayment" runat="server" CssClass="btn btn-success" Visible="false" Text="View Benefits" OnClick="btnProceedToPayment_Click" />
        </div>--%>
</div>


<div class="col-md-6 text-left linkBottom" id="dvSubmit" runat="server" visible="false" style="padding: 0px;">
    <asp:Button ID="btnProceedToPayment" runat="server" CssClass="btn btn-success"  OnClientClick="javascript:return CheckValid();"  Text="View Benefits" OnClick="btnProceedToPayment_Click" />
</div>
<script type="text/javascript">
    function CheckValid() {
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
</script>