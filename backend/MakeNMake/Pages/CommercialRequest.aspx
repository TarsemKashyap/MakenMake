<%@ Page Title="Commercial Request" Language="C#" MasterPageFile="AdminMaster.Master" AutoEventWireup="true" CodeBehind="CommercialRequest.aspx.cs" Inherits="MakeNMake.Admin.CommercialRequest" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../Scripts/jquery-2.0.1.min.js"></script>
    <link href="../Static/bootstrap/bootstrap.css" rel="stylesheet" />
    <script src="../Static/bootstrap/bootstrap.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            if ($("#hdnCustomer").val() == "1") {
                $('#myModal').modal({ show: true });
                $("#hdnCustomer").val("");

                $(".modal-backdrop").hide();
            }
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="panel panel-success">
        <div class="panel-heading">
            <h3 class="panel-title paneltitle">Visit Request</h3>
        </div>
        <div class="panel-body" style="padding-left:0px;">
            <div class="col-sm-12 clear">
                <div class="table-responsive">
                    <asp:HiddenField ID="hdnCustomer" runat="server" ClientIDMode="Static" />
                    <asp:HiddenField ID="hdnServiceID" runat="server" />
                    <asp:Repeater ID="RptService" runat="server" OnItemCommand="RptService_ItemCommand">
                        <HeaderTemplate>
                            <table class="table table-hover  table-bordered  table-condensed" data-height="400">
                                <thead style="color: White; background-color: #6b9297;">
                                    <tr>
                                        <th>Request Id</th>
                                        <th>Services-Category</th>
                                        <th>Plan</th>
                                        <th>Name</th>
                                        <th>Address</th>
                                        <th>Status</th>
                                        <th>Assigend To</th>
                                        <%--  <th>Total Site Area</th>
                                        <th>Build Up Area</th>--%>
                                        <th>Preferred Time</th>
                                        <th>Request Received On</th>
                                        <th>View Request</th>
                                        <th>Create Quotation</th>
                                    </tr>
                                </thead>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tbody>
                                <tr>
                                    <td>
                                        <asp:LinkButton ID="lblrequest" CommandName="RequestID" Text='<%#Eval("RequestID") %>' CommandArgument='<%#Eval("RequestID") %>' runat="server"></asp:LinkButton>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label5" runat="server" Text='<%#Eval("Services") %>'></asp:Label></td>

                                    <td>
                                        <asp:Label ID="Label6" runat="server" Text='<%#Eval("ServicePlan") %>'></asp:Label></td>
                                    <td>
                                        <asp:Label ID="lblquantFrom" runat="server" Text='<%#Eval("name") %>'></asp:Label></td>
                                    <td>
                                        <asp:Label ID="lblDiscount" runat="server" Text='<%#Eval("Address") %>'></asp:Label></td>
                                    <td>
                                        <asp:Label ID="lblStatus" runat="server" Text='<%#Eval("Status") %>'></asp:Label></td>
                                    <td>
                                        <asp:Label ID="lblassign" runat="server" Text='<%#Eval("AssignedTo") %>'></asp:Label></td>
                                    <asp:HiddenField ID="hdnEngineerID" runat="server" Value='<%#Eval("SEngineerID") %>' />
                                    <%-- <td>
                                        <asp:Label ID="Label1" runat="server" Text='<%#Eval("TotalSiteArea") %>'></asp:Label></td>
                                    
                                    <td>
                                        <asp:Label ID="Label2" runat="server" Text='<%#Eval("BuildUpArea") %>'></asp:Label></td>
                                    --%>
                                    <td>
                                        <asp:Label ID="Label3" runat="server" Text='<%#Eval("PreferredTime") %>'></asp:Label></td>
                                    <td>
                                        <asp:Label ID="Label4" runat="server" Text='<%#Eval("Created") %>'></asp:Label></td>
                                    <td>
                                        <asp:LinkButton ID="LinkButton1" Text="View Request" runat="server" CommandArgument='<%#Eval("RequestID") %>' CommandName="viewRequest"></asp:LinkButton>
                                    </td>
                                    <td>
                                        <asp:HiddenField ID="hdnUserid" runat="server" Value='<%#Eval("Userid") %>' />
                                        <asp:LinkButton ID="lnkBtnQuotation" Text="Create" runat="server" CommandArgument='<%#Eval("RequestID") %>' CommandName="quotation" Visible='<%#Convert.ToString(Eval("Status")).ToLower()=="completed"?true:false %>'></asp:LinkButton>
                                    </td>
                            </tbody>
                        </ItemTemplate>
                        <FooterTemplate>
                            </table>
                        </FooterTemplate>
                    </asp:Repeater>
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
        </div>
    </div>
    <div class="modal" id="myModal" tabindex="-1" role="dialog" aria-hidden="true">

        <div class="col-xs-12">
            <div class="panel panel-info">
                <div class="panel-heading">
                    <h3 class="panel-title">Request Status</h3>
                </div>
                <div class="panel-body">
                    <asp:Repeater ID="Repeater1" runat="server">
                        <HeaderTemplate>
                            <table class="table table-hover  table-bordered  table-condensed" data-height="400">
                                <thead style="color: White; background-color: #6b9297;">
                                    <tr>
                                        <th>Request Id</th>
                                        <th>Services-Category</th>
                                        <th>Plan</th>
                                        <th>Name</th>
                                        <th>Address</th>
                                        <th>Status</th>
                                        <th>Assigend To</th>
                                        <th>Total Site Area</th>
                                        <th>Build Up Area</th>
                                        <th>Preferred Time</th>
                                        <th>Request Received On</th>
                                    </tr>
                                </thead>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tbody>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblrequest" Text='<%#Eval("RequestID") %>' runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label5" runat="server" Text='<%#Eval("Services") %>'></asp:Label></td>

                                    <td>
                                        <asp:Label ID="Label6" runat="server" Text='<%#Eval("ServicePlan") %>'></asp:Label></td>
                                    <td>
                                        <asp:Label ID="lblquantFrom" runat="server" Text='<%#Eval("name") %>'></asp:Label></td>
                                    <td>
                                        <asp:Label ID="lblDiscount" runat="server" Text='<%#Eval("Address") %>'></asp:Label></td>
                                    <td>
                                        <asp:Label ID="lblStatus" runat="server" Text='<%#Eval("Status") %>'></asp:Label></td>
                                    <td>
                                        <asp:Label ID="lblassign" runat="server" Text='<%#Eval("AssignedTo") %>'></asp:Label></td>
                                    <asp:HiddenField ID="hdnEngineerID" runat="server" Value='<%#Eval("SEngineerID") %>' />
                                    <td>
                                        <asp:Label ID="Label1" runat="server" Text='<%#Eval("TotalSiteArea") %>'></asp:Label></td>

                                    <td>
                                        <asp:Label ID="Label2" runat="server" Text='<%#Eval("BuildUpArea") %>'></asp:Label></td>

                                    <td>
                                        <asp:Label ID="Label3" runat="server" Text='<%#Eval("PreferredTime") %>'></asp:Label></td>
                                    <td>
                                        <asp:Label ID="Label4" runat="server" Text='<%#Convert.ToDateTime(Eval("Created") ).ToString("dd/MM/yyyy hh:mm")%>'></asp:Label></td>
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
