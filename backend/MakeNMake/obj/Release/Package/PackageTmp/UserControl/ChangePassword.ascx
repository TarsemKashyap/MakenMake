<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ChangePassword.ascx.cs" Inherits="MakeNMake.UserControl.ChangePassword" %>



<div class="col-md-12" style="padding-left:0px;">
                <%--<asp:ValidationSummary ID="ValidationSummary1" ValidationGroup="password" ForeColor="Red" 
                    ShowModelStateErrors="true" ShowMessageBox="false" ShowSummary="true" ShowValidationErrors="true" runat="server" />--%>
                <div class="col-md-6 text-left linkBottom">
                    <div class="input-group input-group-sm">
                        <span class="input-group-addon">Old Password
                        </span>
                        <asp:TextBox ID="txtoldpass" runat="server" TextMode="Password" CssClass="form-control" placeholder="Enter Old Password" />
                    </div>
                    <asp:RequiredFieldValidator ID="validateoldpass" runat="server" ErrorMessage="Enter Old Password"
                         ValidationGroup="password"
                        ForeColor="Red"  Display="Dynamic"   ControlToValidate="txtoldpass"></asp:RequiredFieldValidator>

                </div>
                
                <div class="col-md-6 text-left linkBottom">
                    <div class="input-group input-group-sm">
                        <span class="input-group-addon">New Password
                        </span>
                       <asp:TextBox ID="txtnewpass" runat="server" TextMode="Password"  CssClass="form-control"  placeholder="Enter New Password "/>
                    </div>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Enter new Password" ValidationGroup="password"
                        ForeColor="Red"  Display="Dynamic"   ControlToValidate="txtoldpass"></asp:RequiredFieldValidator>

                </div>
                <div class="col-md-6 text-left linkBottom">
                    <div class="input-group input-group-sm">
                        <span class="input-group-addon">Confirm Password
                        </span>
                        <asp:TextBox ID="txtconfirmpass" runat="server" TextMode="Password"  CssClass="form-control"  placeholder="Enter Confirm Password "/>
                    </div>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Enter Confirm Password"
                                            ForeColor="Red" Display="Dynamic"  ValidationGroup="password" ControlToValidate="txtconfirmpass"></asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="CompareValidator1"
                                            runat="server"
                                            ControlToCompare="txtnewpass"
                                            ControlToValidate="txtconfirmpass"
                                            Operator="Equal"  Display="Dynamic" ForeColor="Red"
                                            ErrorMessage="Password and confirm password do not match"
                                            ValidationGroup="password" SetFocusOnError="True">                       
                                        </asp:CompareValidator>

                </div>
                <div class="col-md-12 text-left linkBottom">
                    <asp:Button ID="btnChangePassword" runat="server" ValidationGroup="password" CssClass="btn btn-success" Text="Save" OnClick="btnChangePassword_Click" />
                     <asp:Button ID="btnCancel" runat="server"  CssClass="btn btn-success" Text="Cancel" OnClick="btnCancel_Click"/>
                    </div>
            </div>