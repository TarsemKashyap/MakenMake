<%@ Page Title="" Language="C#" MasterPageFile="AdminMaster.Master" AutoEventWireup="true" CodeBehind="AppointMentDetails.aspx.cs" Inherits="MakeNMake.ServiceEngineer.AppointMentDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="panel panel-success">
       <%-- <asp:ValidationSummary ID="ValidationSummary1" ValidationGroup="ticket" ShowSummary="true" ForeColor="Red" runat="server" />--%>
        <div class="panel-heading">
            <h3 class="panel-title paneltitle">Ticket Details</h3>
        </div>
        <div class="panel-body">
            <div class="col-sm-12 clear" style="margin-bottom:20px;">
                <asp:Label ID="lblTicketID" CssClass="label label-success" runat="server"></asp:Label>
              
               <%-- <asp:Label ID="lblcurrentstatusname" Text="Current Status : " CssClass="label label-success" Visible="true" runat="server"></asp:Label>
                <asp:Label ID="lblcurrentstatus" CssClass="label label-success" Visible="true" runat="server"></asp:Label>
                    <br /><br />--%>
            </div>
            <div>
                <div class="col-sm-6 text-left linkBottom">
                    <div class="input-group input-group-sm">
                        <span class="input-group-addon" id="Span9">Change Status</span>
                        <asp:DropDownList ID="ddlStatus" CssClass="form-control" runat="server">
                            <asp:ListItem Text="--Select Status--" Value="0"></asp:ListItem>
                            <asp:ListItem Text="Accept" Value="5"></asp:ListItem>
                            <asp:ListItem Text="Reject" Value="2"></asp:ListItem>
                            <asp:ListItem Text="Completed" Value="3"></asp:ListItem>
                        </asp:DropDownList>

                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" InitialValue="0" runat="server" ControlToValidate="ddlStatus"
                            ErrorMessage="Please Select Status" ValidationGroup="ticket"  Display="Dynamic" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                    </div>
                </div>

                <div class="col-sm-6 text-left linkBottom">
                    <div class="input-group input-group-sm">
                        <span class="input-group-addon" id="Span1">Reason</span>
                        <asp:TextBox ID="txtReason" MaxLength="490"  Height="46px" style="resize: none;" TextMode="MultiLine"  CssClass="form-control input" runat="server"></asp:TextBox>
                    </div>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7"
                        runat="server" ValidationGroup="ticket"  Display="Dynamic" ForeColor="Red"
                        ControlToValidate="txtReason" SetFocusOnError="true"
                        ErrorMessage="Enter the Reason"></asp:RequiredFieldValidator>
                </div>

              

            </div>
        </div>
        <div class="panel-footer">
            <asp:Button ID="btnSubmit" ValidationGroup="ticket" runat="server" Text="Submit" OnClick="btnSubmit_Click" CssClass="btn-xs btn-success" />
            <asp:Button ID="btncancel" CssClass="btn-xs btn-success" runat="server" Text="Go To Appointments" OnClick="btncancel_Click" />
        </div>
    </div>
</asp:Content>
