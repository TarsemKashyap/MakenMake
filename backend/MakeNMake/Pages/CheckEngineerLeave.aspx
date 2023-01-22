<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/AdminMaster.Master" AutoEventWireup="true" CodeBehind="CheckEngineerLeave.aspx.cs" Inherits="MakeNMake.Pages.CheckEngineerLeave" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="panel-body" style="padding-left: 0px;">
        <div id="divUpdateStatus" runat="server" visible="false">
            <div class="col-sm-12" style="padding-left: 0px;">
                <div class="col-sm-12 text-left linkBottom linkBottom">
                    <div class="input-group input-group-sm">
                        <span class="input-group-addon" >Reason</span>
                        <asp:TextBox ID="txtengineername" ReadOnly="true" CssClass="form-control"  placeholder="Leave Reason" runat="server" TextMode="MultiLine"></asp:TextBox>
                    
                    </div>
                   
                </div>
                <%-- <asp:ValidationSummary ID="ValidationSummary1" ValidationGroup="user" ForeColor="Red" ShowModelStateErrors="true" ShowMessageBox="false" ShowSummary="true" ShowValidationErrors="true" runat="server" />--%>
                <div class="col-sm-12 text-left linkBottom linkBottom">
                    <div class="input-group input-group-sm">
                        <span class="input-group-addon" >Leave On</span>
                        <asp:TextBox ID="txtleaveon" ReadOnly="true" AutoComplete="off"  ClientIDMode="Static" CssClass="form-control"  placeholder="Select Date" runat="server"></asp:TextBox>
                        <ajaxToolkit:CalendarExtender ID="CalendarExtender1" TargetControlID="txtleaveon"  OnClientDateSelectionChanged="checkDate"
                         Format="MM/dd/yyyy" SelectedDate='<%#Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy")) %>' runat="server" />
                    </div>
                    
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtleaveon"
                        ErrorMessage="Select Leave Date" ValidationGroup="user" Display="Dynamic" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                </div>
                 <div class="col-sm-12 text-left linkBottom linkBottom">
                    <div class="input-group input-group-sm">
                        <span class="input-group-addon" >Reason</span>
                        <asp:TextBox ID="txtReason" ReadOnly="true" CssClass="form-control"  placeholder="Leave Reason" runat="server" TextMode="MultiLine"></asp:TextBox>
                    
                    </div>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtReason"
                        ErrorMessage="Enter Reason For leave" ValidationGroup="user" Display="Dynamic" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                </div>
                 <div class="col-sm-12 text-left linkBottom linkBottom">
                    <div class="input-group input-group-sm">
                        <span class="input-group-addon" >Status</span>
                       <asp:DropDownList ID="ddlStaus" runat="server">

                           <asp:ListItem Value="-1">Select</asp:ListItem>
                           <asp:ListItem Value="0">Unapproved</asp:ListItem>
                              <asp:ListItem Value="1">Approved</asp:ListItem>
                              <asp:ListItem Value="2">Denied</asp:ListItem>
                       </asp:DropDownList>
                    <asp:HiddenField ID="hdleaveIDmain" runat="server" />
                    </div> 

                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlStaus" InitialValue="Select"
                        ErrorMessage="Please Select Status" ValidationGroup="user" Display="Dynamic" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                </div>
                <div class="col-sm-12 text-left linkBottom" style="margin-top: 20px;">
                    <asp:Button ID="btnSubmit" runat="server" Text="Update"  CssClass="btn btn-success" ValidationGroup="user" OnClick="btnSubmit_Click" />

                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-success" OnClick="btnCancel_Click"  />
                </div>
                </div>
            </div>
           <asp:Repeater ID="rptleave" runat="server" OnItemCommand="rptleave_ItemCommand" OnItemDataBound="rptleave_ItemDataBound">
                <HeaderTemplate>
                    <table class="table table-hover  table-bordered  table-condensed" data-height="400">
                        <thead style="color: White; background-color: #6b9297;">
                            <tr style="width: 80%">
                                <th>Leave Applied By</th>
                                <th>Leave Date</th>
                                 <th>Reason</th>
                                 <th>Applied Date</th>
                                <th>Status</th>
                                <th>Update Status</th>
                            </tr>
                        </thead>
                </HeaderTemplate>
                <ItemTemplate>
                    <tbody>
                        <tr>
                            <td>
                                <asp:Label ID="lbLeaveappliedby" runat="server" ></asp:Label>
                                 <asp:HiddenField ID="hdEngineerID" runat="server" Value='<%#Eval("EngineerID") %>' />
                            </td>
                            <td>
                                <asp:HiddenField ID="hdleaveid" runat="server" Value='<%#Eval("LeaveID") %>' />
                                <asp:Label ID="lblLeaveOn" runat="server" Text='<%#Eval("LeaveOn") %>'></asp:Label></td>
                              <td>
                                <asp:Label ID="lblLeaveReason" runat="server" Text='<%#Eval("Reason") %>'></asp:Label></td>
                             <td>
                                <asp:Label ID="lblAppliedon" runat="server" Text='<%#Eval("Created") %>'></asp:Label></td>
                              <td>
                                  <asp:HiddenField ID="hdstatus" runat="server" Value='<%#Eval("Status") %>'/>
                                <asp:Label ID="lblstatus" runat="server" ></asp:Label></td>
                            <td>
                                <asp:LinkButton ID="btnUpdateStatus" runat="server" CommandArgument='<%#Eval("LeaveID") %>' CommandName="UpdateStatus" >Update Status</asp:LinkButton>
                            </td>
                            </tr>
                    </tbody>
                </ItemTemplate>
                <FooterTemplate>
                    </table>
                </FooterTemplate>
            </asp:Repeater>
            </div>
</asp:Content>
