<%@ Page Title="Fill Diary" Language="C#" MasterPageFile="AdminMaster.Master" AutoEventWireup="true" CodeBehind="ServiceEngineerDiary.aspx.cs" Inherits="MakeNMake.ServiceEngineer.ServiceEngineerDiary" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../Static/js/validation.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="panel panel-success">
        <div class="panel-heading">
            <h3 class="panel-title paneltitle">Daily Diary</h3>
        </div>

        <div class="panel-body" style="padding-left:0px;">
            <div class="col-sm-12" style="padding-left:0px;">
                <div class="col-sm-6 text-left linkBottom">
                    <div class="input-group input-group-sm">
                        <span class="input-group-addon" id="Span1">Ticket Id</span>
                        <asp:DropDownList ID="ddlticketid" runat="server" CssClass="form-control  dropdown" AutoPostBack="true" OnSelectedIndexChanged="ddlticketid_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                      <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ValidationGroup="user" runat="server" ErrorMessage="Select Ticket Id"
                        ControlToValidate="ddlticketid"
                        InitialValue="0" Display="Dynamic" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                </div>
                <div id="divtitle" runat="server" visible="false">

                <div class="col-sm-6 text-left linkBottom" >
                    <div class="input-group input-group-sm">
                        <span class="input-group-addon" id="Span5">Title</span>
                        <asp:TextBox onkeypress="return ValidateCharacters(event)" ID="TextTitle" CssClass="form-control" placeholder="Enter Title" runat="server"></asp:TextBox>
                    </div>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="TextTitle"
                        ErrorMessage="Enter Title" ValidationGroup="user" Display="Dynamic" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>

                </div>
                <div class="col-sm-6 text-left linkBottom">
                    <div class="input-group input-group-sm">
                        <span class="input-group-addon" id="Span4">Description</span>
                        <asp:TextBox ID="TextDesc" CssClass="form-control" TextMode="MultiLine" placeholder="Enter Description" runat="server"></asp:TextBox>

                    </div>
                    <asp:RequiredFieldValidator Textmode="multiline" ID="RequiredFieldValidator2" runat="server" ControlToValidate="TextDesc"
                        ErrorMessage="Enter Description" ValidationGroup="user" Display="Dynamic" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>

                </div>
                <div class="col-sm-10 text-left linkBottom">

                    <asp:Button ID="btnSubmit" runat="server" Text="Add" OnClick="btnSubmit_Click"
                        CssClass="btn btn-success" ValidationGroup="user" />
                    <asp:Button ID="btncancel" runat="server" Text="Cancel"
                        CssClass="btn btn-success" OnClick="btncancel_Click" />
                </div>
              </div>

            </div>
        </div>

    </div>

    <div class="panel panel-success">
        <div class="panel-heading">
            <h3 class="panel-title paneltitle">Ticketwise Daily Diary</h3>
        </div>
        <div class="panel-body" style="padding-left:0px;">
            <div class="col-sm-12" style="padding-left:0px;">
                <div class="table-responsive">
                    <asp:Repeater ID="RptDiary" runat="server">
                        <HeaderTemplate>
                            <table class="table table-hover  table-bordered  table-condensed" data-height="400">
                                <thead style="color: White; background-color: #6b9297;">
                                    <tr>
                                        <th>Ticket ID</th>
                                        <th>Opened Date</th>
                                        <th>Closed Date</th>
                                        <th>Service Start Time</th>
                                        <th>Service End Time</th>
                                        <th>Actual Time</th>
                                        <th>Check In</th>
                                        <th>Check Out</th>

                                    </tr>
                                </thead>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tbody>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblTicketID" runat="server" Text='<%#Eval("TicketID") %>'></asp:Label></td>
                                    <td>

                                        <asp:Label ID="lblOpendate" runat="server" Text='<%#Eval("Opendate") %>'></asp:Label></td>
                                    <td>
                                        <asp:Label ID="lblCloseddate" runat="server" Text='<%#Eval("Closeddate") %>'></asp:Label></td>
                                    <td>
                                        <asp:Label ID="lblServiceStart" runat="server" Text='<%#Eval("ServiceStart") %>'></asp:Label></td>
                                     <td>
                                        <asp:Label ID="lblServiceEnd" runat="server" Text='<%#Eval("ServiceEnd") %>'></asp:Label></td>
                                     <td>
                                        <asp:Label ID="lblActualTime" runat="server" Text='<%#Eval("ActualTime") %>'></asp:Label></td>
                                     <td>
                                         
                                        <asp:Label ID="lblWorkHistory" runat="server" Text='<%#Eval("WorkDEscCheckIn") %>'></asp:Label></td>
                                    <td>
                                        
                                        <asp:Label ID="lblWorkDescCheckOut" runat="server" Text='<%#Eval("WorkDescCheckOut") %>'></asp:Label></td>


                                </tr>
                            </tbody>
                        </ItemTemplate>
                        <FooterTemplate>
                            </table>
                        </FooterTemplate>
                    </asp:Repeater>

                    <asp:Label Visible="false" runat="server" ID="lblmsg" CssClass="label label-info"></asp:Label>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
