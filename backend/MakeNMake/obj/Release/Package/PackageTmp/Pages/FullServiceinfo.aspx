<%@ Page Title="All Services" Language="C#" MasterPageFile="AdminMaster.Master" AutoEventWireup="true" CodeBehind="FullServiceinfo.aspx.cs" Inherits="MakeNMake.Admin.FullServiceinfo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function PageReload() {
            var getValue = confirm('No Record Found');
            if (getValue) {
                window.location.href = window.location.href;
            }
            else {
                window.location.href = window.location.href;
            }
        }
    </script>
    <style type="text/css">
         .table > tbody > tr > td >hr {
            margin:0px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="panel panel-success table-responsive">
        <div class="panel-heading">
            <h3 class="panel-title paneltitle">Services Info</h3>
        </div>
        <div class="panel-body" id="divService" runat="server" visible="false">

               <div class="input-group input-group-xs">

                <asp:TextBox ID="txtSearchclient" placeholder="Search: By Service Name,Category,Service Type or Plan" Width="400px"
                    CssClass="form-control" runat="server"></asp:TextBox>
                <asp:Button ID="Button1" runat="server" Text="Search" Style="margin-left: 10px;"
                    CssClass="btn btn-success" OnClick="Button1_Click" />
            </div>

            <br />


            <div class="col-sm-12 clear " style="padding-left:0px;">
                <div class="table-responsive">
                    <asp:Repeater ID="RptService" runat="server">
                        <HeaderTemplate>
                            <table class="table table-hover  table-bordered  table-condensed" data-height="400">
                                <thead style="color: White; background-color: #6b9297;">
                                    <tr>
                                        <th>Service</th>
                                        <th>Category</th>
                                        <th>ServiceType</th>
                                        <th>PlanType</th>
                                        <th>Duration</th>
                                        <th>Area-Category-Amount</th>
                                        <%-- <th>Calls In Plan</th>--%>
                                        <th>Amount</th>
                                        <%-- <th>Discount On Calls</th>
                                        <th>Discount</th>
                                        <th>Status</th>--%>
                                    </tr>
                                </thead>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tbody>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblName" runat="server" Text='<%#Eval("ServiceName") %>'></asp:Label></td>
                                    <td>
                                        <asp:Label ID="lblCategory" runat="server" Text='<%#Eval("category") %>'></asp:Label></td>
                                    <td>
                                        <asp:Label ID="lblType" runat="server" Text='<%#Eval("Servicetype") %>'></asp:Label></td>
                                    <td>
                                        <asp:Label ID="lblPlan" runat="server" Text='<%#Eval("PlanType") %>'></asp:Label></td>
                                    <td>
                                        <asp:Label ID="lblDuration" runat="server" Text='<%# Eval("Duration")%>'></asp:Label></td>
                                    <td style="width: 350px;">
                                        <asp:Label ID="lblArea" runat="server" Text='<%# Convert.ToString(Eval("AreaCatAmount")).Replace("$","<br/><hr/>")%>'></asp:Label>

                                    </td>

                                    <%-- <td>
                                        <asp:Label ID="lblCalls" runat="server" Text='<%# Eval("NofCalls")%>'></asp:Label></td>--%>
                                    <td>
                                        <asp:Label ID="lblAmount" runat="server" Text='<%# Convert.ToString(Eval("Amount")).Replace("$","<br/><hr/>")%>'></asp:Label></td>
                                    <%--  <td>
                                        <asp:Label ID="lblDCalls" runat="server" Text='<%# Eval("DiscountOnCalls")%>'></asp:Label></td>
                                    <td>
                                        <asp:Label ID="lblDiscunt" runat="server" Text='<%# Convert.ToString(Eval("Discount")).Replace("$","<br/>")%>'></asp:Label></td>
                                    <td>
                                        <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("Status")%>'></asp:Label></td>--%>
                            </tbody>
                        </ItemTemplate>
                        <FooterTemplate>
                            </table>
                        </FooterTemplate>
                    </asp:Repeater>
                </div>
            </div>
            <div class="col-sm-12 ">
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
</asp:Content>
