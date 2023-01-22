<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ViewRequests.ascx.cs" Inherits="MakeNMake.UserControl.ViewRequests" %>

<asp:Repeater ID="RptService" runat="server">
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
