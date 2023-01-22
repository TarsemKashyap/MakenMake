<%@ Page Title="RequestData" Language="C#" MasterPageFile="AdminMaster.Master" AutoEventWireup="true" CodeBehind="RequestData.aspx.cs" Inherits="MakeNMake.Pages.RequestData" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
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
    <style type="text/css">
        .table > thead > tr > th {
            text-align: center;
        }

        .table > tbody > tr > td {
            text-align: left!important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="panel panel-success">
        <div class="panel-heading">
            <h3 class="panel-title paneltitle">Commercial Request</h3>
        </div>        
        <div class="panel-body" style="overflow:scroll;">
            <asp:Repeater ID="RptRequest" runat="server" OnItemCommand="RptRequest_ItemCommand" >
                <HeaderTemplate>
                    <table class="table table-hover  table-bordered  table-condensed" data-height="400">
                        <thead style="color: White; background-color: #6b9297;">
                            <tr>
                                <th>RequestID</th>
                                <th>Service</th>
                                <th>Client Name</th>
                                <th>Total Site Area</th>
                                <th>Build Up Area</th>
                                <th>Preffered Time</th>
                                <th>Created </th>
                                <th>Status</th>
                                <th>Contact-Name</th>
                                <th>Contact-EmailID</th>
                                 <th>Contact-Mobile</th>
                                <th>Assessment Form</th>
                            </tr>
                        </thead>
                </HeaderTemplate>
                <ItemTemplate>
                    <tbody>
                        <tr>
                             <td>
                                    <asp:LinkButton ID="lblRequestID" CommandName="RequestID" Text='<%#Eval("RequestID")%>' CommandArgument='<%#Eval("RequestID") %>' runat="server"></asp:LinkButton>
                                </td>
                            <td>
                                <asp:Label ID="lblserviceid" runat="server" Text='<%#Eval("ServiceName") %>'></asp:Label>                                
                            </td>
                            <td>
                                <asp:Label ID="lblname" runat="server" Text='<%#Eval("Name") %>'></asp:Label>                                
                            </td>
                            <td>
                                <asp:Label ID="lblTotalSiteArea" runat="server" Text='<%#Eval("TotalSiteArea") %>'></asp:Label></td>
                            <td>
                                <asp:Label ID="lblBuildUpArea" runat="server" Text='<%#Eval("BuildUpArea") %>'></asp:Label></td>
                             <td>
                                 <asp:Label ID="lblPreferredTime" runat="server" Text='<%#Convert.ToDateTime(Eval("PreferredTime")).ToString("MMMM dd, yyyy") %>'>
                                 </asp:Label></td>                              
                            <td>
                                <asp:Label ID="LblCreated" runat="server" Text='<%#Convert.ToDateTime(Eval("Created")).ToString("MMMM dd, yyyy") %>'></asp:Label></td>
                          <td>
                               <asp:Label ID="Label1" runat="server" Text='<%#Eval("Status") %>'></asp:Label></td>
                            <td>
                               <asp:Label ID="lblContactble" runat="server" Text='<%#Eval("Contact_Firstname") %>'></asp:Label></td>

                             <td>
                                <asp:Label ID="lblContact_Email_ID" runat="server" Text='<%#Eval("Contact_Email_ID") %>'></asp:Label></td>

                            <td>
                                <asp:Label ID="lblContact_mobile" runat="server" Text='<%#Eval("Contact_mobile") %>'></asp:Label></td>
                            <td>
                                <asp:LinkButton ID="lnkFill" runat="server" Text='<%#Convert.ToString(Eval("Status")).ToLower()=="accepted"  ?"Fill":"Filled"%>' CommandName="fillform" CommandArgument='<%#Eval("RequestID")%>'
                                    Visible='<%#Convert.ToString(Eval("Status")).ToLower()=="accepted" || Convert.ToString(Eval("Status")).ToLower()=="completed"?true:false %>'></asp:LinkButton>
                            </td>
                          
                          
                        </tr>
                    </tbody>
                </ItemTemplate>
                <FooterTemplate>
                    </table>
                </FooterTemplate>
            </asp:Repeater>
            <table class="tblpaging" id="paging" runat="server" style="font-size: 12px; clear: both; margin-top: 15px;">
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
