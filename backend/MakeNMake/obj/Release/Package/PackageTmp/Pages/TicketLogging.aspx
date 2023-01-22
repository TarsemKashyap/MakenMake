<%@ Page Title="Ticket Logging" Language="C#" MasterPageFile="AdminMaster.Master" AutoEventWireup="true" CodeBehind="TicketLogging.aspx.cs" Inherits="MakeNMake.Customer.TicketLogging" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../Scripts/jquery-2.0.1.min.js"></script>
    <script type="text/javascript">
        function CheckRadion(ele) {
            $(".repair tr").find('input[type=radio]').attr('checked', false);
            $(ele).prop("checked", true);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="panel panel-success">
        <div class="panel-heading">
            <h3 class="panel-title paneltitle">Open Ticket</h3>
        </div>
        <%--   <div class="panel-body">
            <div class="col-md-12 ">
                <div class="col-md-10 contarea">
                    <div class="col-md-4 otptext linkBottom">Enter verification code sent to your mobile:</div>
                    <div class="col-md-2 linkBottom">
                        <asp:TextBox ID="txtVOtp" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-md-2 linkBottom">
                        <asp:Button ID="btnCheckOtp" ClientIDMode="Static" OnClick="btnCheckOtp_Click" CssClass="btn-group-sm  btn otpbutton" runat="server" Text="Verify OTP" />
                    </div>                    
                </div>
                </div>
            </div>--%>
        <div class="panel-body"  style="padding-left:0px;">
            <div class="col-sm-12" style="padding-left:0px;">
                <%-- <asp:ValidationSummary ID="ValidationSummary1" ValidationGroup="service" ShowSummary="true" ForeColor="Red" runat="server" />--%>
                <div class="col-sm-6 text-left linkBottom">
                    <div class="input-group input-group-sm">
                        <span class="input-group-addon" id="Span1">Ticket Type </span>
                        <asp:DropDownList ID="ddlticket" CssClass="form-control dropdown" runat="server" AutoPostBack="true" >
                            <asp:ListItem Text="--Select Ticket Type--" Value="0"></asp:ListItem>
                            <asp:ListItem Text="Inspection/Quote" Value="1"></asp:ListItem>
                            <asp:ListItem Text="Repair" Value="2"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="service" runat="server" ErrorMessage="Select Ticket Type"
                        ControlToValidate="ddlticket"
                        InitialValue="0" Display="Dynamic" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                </div>
                <div class="col-sm-6 text-left linkBottom">
                    <div class="input-group input-group-sm">
                        <span class="input-group-addon" id="Span2">Service Category </span>
                        <asp:DropDownList ID="ddlService" CssClass="form-control dropdown" AutoPostBack="true" OnSelectedIndexChanged="ddlService_SelectedIndexChanged" runat="server">
                            <asp:ListItem Text="--Select Category type--" Value="0"></asp:ListItem>
                            <asp:ListItem Text="Domestic" Value="D"></asp:ListItem>
                            <asp:ListItem Text="Commercial" Value="C"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="service" runat="server" ErrorMessage="Select Service Category"
                        ControlToValidate="ddlService"
                        InitialValue="0" Display="Dynamic" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>

                    <asp:Label ID="lblvalidationMsg" ForeColor="Red" runat="server" Visible="false"></asp:Label>
                </div>

                <div id="dvPlan" runat="server" visible="false" class="col-sm-6 text-left linkBottom">
                    <div class="input-group input-group-sm">
                        <span class="input-group-addon" id="Span3">Service Plan </span>
                        <asp:DropDownList ID="ddlPlan" CssClass="form-control dropdown" OnSelectedIndexChanged="ddlPlan_SelectedIndexChanged" AutoPostBack="true" runat="server">
                            <asp:ListItem Text="--Select Category Plan--" Value="0"></asp:ListItem>
                            <asp:ListItem Text="Unlimited" Value="U"></asp:ListItem>
                            <asp:ListItem Text="Make your Plan" Value="M"></asp:ListItem>
                            <asp:ListItem Text="Flexi" Value="F"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <asp:Label ID="lblvadtionPlan" ForeColor="Red" runat="server" Visible="false"></asp:Label>
                </div>
                <div id="dvDesc" runat="server" visible="false" >
                    <div class="col-sm-6 text-left linkBottom">
                        <div class="input-group input-group-sm">
                            <span class="input-group-addon" id="Span8">Description Details</span>
                            <asp:TextBox ID="txtdesc" TextMode="MultiLine" AutoComplete="off"
                                placeholder="Enter Description Details" MaxLength="240" CssClass="form-control" aria-describedby="basic-addon1" runat="server"></asp:TextBox>
                        </div>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7"
                            runat="server" ValidationGroup="service" Display="Dynamic" ForeColor="Red"
                            ControlToValidate="txtdesc" SetFocusOnError="true" ErrorMessage="Enter the Description Details"></asp:RequiredFieldValidator>
                    </div>
                    <div id="dvRepair"  runat="server" style="clear:both;height:100%;" visible="false" class="col-sm-12 text-left linkBottom">
                        <div class="table-responsive">
                            <asp:HiddenField ID="hdnCustomer" ClientIDMode="Static" runat="server" />
                            <asp:Label ID="lblMsg" CssClass="label label-success" runat="server"></asp:Label>
                            <asp:Repeater ID="RptService" runat="server" >
                                <HeaderTemplate>
                                    <table class="table table-hover  table-bordered  table-condensed repair" >
                                        <thead style="color: White; background-color: #6b9297;">
                                            <tr>
                                                <th></th>
                                                <th>Service Name</th>
                                                <th>Plan</th>
                                                <th>Issue Description </th>
                                                <th>Date Of Open</th>
                                            </tr>
                                        </thead>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tbody>
                                        <tr>
                                            <td>
                                                <asp:RadioButton ID="chkID" runat="server" onclick="CheckRadion(this)" /></td>
                                            <td>
                                                 <asp:HiddenField ID="hdnAgreementID" runat="server" Value='<%#Eval("AgreementID") %>' />
                                                <asp:HiddenField ID="hdnID" runat="server" Value='<%#Eval("ServiceID") %>' />
                                                <asp:Label ID="lblService" runat="server" Text='<%#Eval("ServiceName") %>'></asp:Label></td>
                                            <td>
                                                <asp:HiddenField ID="hdntype" runat="server" Value='<%#Eval("ServiceType") %>' />
                                                <asp:Label ID="lblPlan" runat="server" Text='<%#Eval("Plans") %>'></asp:Label></td>

                                            <td>
                                                <asp:TextBox ID="txtDesc" MaxLength="250" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                                                <asp:Label ID="lblmsg" runat="server" ForeColor="Red" ></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label3" runat="server" Text='<%#DateTime.Now.ToString("MM/dd/yyyy") %>'></asp:Label></td>
                                    </tbody>
                                </ItemTemplate>
                                <FooterTemplate>
                                    </table>
                                </FooterTemplate>
                            </asp:Repeater>
                        </div>
                    </div>
                    <div id="dvInspection" style="clear:both; height: 100%;" runat="server" visible="false" class="col-sm-12 text-left linkBottom">
                        <div class="table-responsive">
                            <asp:HiddenField ID="HiddenField1" ClientIDMode="Static" runat="server" />
                            <asp:Label ID="Label2" CssClass="label label-success" runat="server"></asp:Label>
                            <asp:Repeater ID="RptInspection" runat="server">
                                <HeaderTemplate>
                                    <table class="table table-hover  table-bordered  table-condensed" data-height="400">
                                        <thead style="color: White; background-color: #6b9297;">
                                            <tr>
                                                <th></th>
                                                <th>Service Name</th>
                                                <th>Servic type</th>
                                                <th>Plan</th>
                                                <th>Comments</th>
                                            </tr>
                                        </thead>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tbody>
                                        <tr>
                                            <td>
                                                <asp:CheckBox ID="chkID" runat="server" /></td>
                                            <td> <asp:HiddenField ID="hdnAgreementID" runat="server" Value="0" />
                                                <asp:HiddenField ID="hdnID" runat="server" Value='<%#Eval("ServiceID") %>' />
                                                <asp:Label ID="lblService" runat="server" Text='<%#Eval("ServiceName") %>'></asp:Label></td>
                                            <td>
                                                <asp:Label ID="Label1" runat="server" Text='<%#Eval("ServiceType") %>'></asp:Label></td>

                                            <td>
                                                <asp:HiddenField ID="hdntype" runat="server" Value='<%#Eval("ServiceType") %>' />
                                                <asp:Label ID="lblPlan" runat="server" Text='<%#Eval("Plans") %>'></asp:Label></td>
                                            <td>
                                                <asp:TextBox ID="txtDesc" MaxLength="250" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                                                   <asp:Label ID="lblmsg" runat="server" ForeColor="Red" ></asp:Label>
                                            </td>
                                    </tbody>
                                </ItemTemplate>
                                <FooterTemplate>
                                    </table>
                                </FooterTemplate>
                            </asp:Repeater>
                        </div>
                    </div>
                    <div style="clear:both;" class="col-sm-12 text-left linkBottom">
                        <asp:Button ID="btnComplaint" runat="server" ClientIDMode="Static" ValidationGroup="service" CssClass="btn btn-success" Text="Submit Ticket" OnClick="btnComplaint_Click" />
                        <asp:Button ID="btnReset" runat="server" CssClass="btn btn-success" Text="Reset" OnClick="btnReset_Click" />
                        <asp:Button ID="btnCancel" runat="server"  CssClass="btn btn-success" Text="Cancel" OnClick="Button1_Click" />
                    </div>
                </div>

            </div>
        </div>
    </div>
</asp:Content>

