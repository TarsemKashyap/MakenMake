<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/AdminMaster.Master" AutoEventWireup="true" CodeBehind="ServiceEngineerLeave.aspx.cs" Inherits="MakeNMake.Pages.ServiceEngineerLeave" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">

        function checkDate(sender, args) {
            if (sender._selectedDate < new Date()) {
                alert("You cannot select a day earlier than today!");
                sender._selectedDate = new Date();
                // set the date back to the current date
                sender._textbox.set_Value(sender._selectedDate.format(sender._format))
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div class="panel panel-success">
        <div runat="server" id="divconsumer" class="panel-heading">
            <h3 class="panel-title paneltitle">Service Engineer Leave</h3>
        </div>
      
        
        <div class="panel-body" style="padding-left: 0px;">
            <div class="col-sm-12" style="padding-left: 0px;">
                <%-- <asp:ValidationSummary ID="ValidationSummary1" ValidationGroup="user" ForeColor="Red" ShowModelStateErrors="true" ShowMessageBox="false" ShowSummary="true" ShowValidationErrors="true" runat="server" />--%>
                <div class="col-sm-12 text-left linkBottom linkBottom">
                    <div class="input-group input-group-sm">
                        <span class="input-group-addon" >Leave On</span>
                        <asp:TextBox ID="txtleaveon" AutoComplete="off"  ClientIDMode="Static" CssClass="form-control"  placeholder="Select Date" runat="server"></asp:TextBox>
                        <ajaxToolkit:CalendarExtender ID="CalendarExtender1" TargetControlID="txtleaveon"  OnClientDateSelectionChanged="checkDate"
                         Format="MM/dd/yyyy" SelectedDate='<%#Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy")) %>' runat="server" />
                    </div>
                    
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtleaveon"
                        ErrorMessage="Select Leave Date" ValidationGroup="user" Display="Dynamic" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                </div>
                 <div class="col-sm-12 text-left linkBottom linkBottom">
                    <div class="input-group input-group-sm">
                        <span class="input-group-addon" >Reason</span>
                        <asp:TextBox ID="txtReason" CssClass="form-control"  placeholder="Leave Reason" runat="server" TextMode="MultiLine"></asp:TextBox>
                    
                    </div>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtReason"
                        ErrorMessage="Enter Reason For leave" ValidationGroup="user" Display="Dynamic" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                </div>
                <div class="col-sm-12 text-left linkBottom" style="margin-top: 20px;">
                    <asp:Button ID="btnSubmit" runat="server" Text="Add"  CssClass="btn btn-success" ValidationGroup="user" OnClick="btnSubmit_Click" />

                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-success"  />
                </div>
                </div>
           <asp:Repeater ID="rptleave" runat="server" OnItemDataBound="rptleave_ItemDataBound">
                <HeaderTemplate>
                    <table class="table table-hover  table-bordered  table-condensed" data-height="400">
                        <thead style="color: White; background-color: #6b9297;">
                            <tr style="width: 80%">
                                <th>Leave Date</th>
                                 <th>Reason</th>
                                 <th>Applied Date</th>
                                <th>Status</th>
                            </tr>
                        </thead>
                </HeaderTemplate>
                <ItemTemplate>
                    <tbody>
                        <tr>
                            <td>
                                <asp:HiddenField ID="hdleaveid" runat="server" Value='<%#Eval("LeaveID") %>' />
                                <asp:Label ID="lblLeaveOn" runat="server" Text='<%#Eval("LeaveOn") %>'></asp:Label></td>
                              <td>
                                <asp:Label ID="lblLeaveReason" runat="server" Text='<%#Eval("Reason") %>'></asp:Label></td>
                             <td>
                                <asp:Label ID="lblAppliedon" runat="server" Text='<%#Eval("Created") %>'></asp:Label></td>
                              <td>
                                  <asp:HiddenField ID="hdstatus" runat="server" Value='<%#Eval("Status") %>'/>
                                <asp:Label ID="lblstatus" runat="server" ></asp:Label></td></td>
                            </tr>
                    </tbody>
                </ItemTemplate>
                <FooterTemplate>
                    </table>
                </FooterTemplate>
            </asp:Repeater>
            </div>
         </div>
</asp:Content>
