<%@ Page Title="Customer Feedback" Language="C#" MasterPageFile="AdminMaster.Master" AutoEventWireup="true" CodeBehind="CustomerFeedback.aspx.cs" Inherits="MakeNMake.Customer.CustomerFeedback" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script src="../Scripts/jquery-2.0.1.min.js"></script>
    <link href="../Static/bootstrap/bootstrap.css" rel="stylesheet" />
    <script src="../Static/bootstrap/bootstrap.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            if ($("#hdnCustomer").val() != "") {
                $('#imagemodal').modal({ show: true });
                $('#imagepreview').attr('src', $("#hdnCustomer").val());
                $("#hdnCustomer").val("");
            }
        });

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server" >
    <div class="panel panel-success">
        <div class="panel-heading">
            <h3 class="panel-title paneltitle">Feedback</h3>
        </div>
        <div class="panel-body" style="padding-left: 0px;">
            <div class="col-sm-12" style="padding-left: 0px;">
                <div id="dvticket" runat="server" class="col-sm-6 text-left linkBottom">
                    <div class="input-group input-group-sm">
                        <span class="input-group-addon" id="Span1">Select TicketID</span>
                        <asp:DropDownList ID="ddlticket" CssClass="form-control dropdown" runat="server" OnSelectedIndexChanged="ddlticket_SelectedIndexChanged" AutoPostBack="true">
                        </asp:DropDownList>
                    </div>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="feedback" runat="server" ErrorMessage="Select Ticket ID"
                        ControlToValidate="ddlticket"
                        InitialValue="0" Display="Dynamic" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                </div>
                <div id="dvSatisfied" runat="server" visible="false" class="col-sm-6 text-left linkBottom">
                    <div class="input-group input-group-sm">
                        <span class="input-group-addon" id="Span2">Are you Satisfied ?</span>

                        <asp:DropDownList ID="ddlSatisfied" CssClass="form-control dropdown" AutoPostBack="true" runat="server">
                            <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                            <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                            <asp:ListItem Text="No" Value="2"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="feedback" runat="server" ErrorMessage="Select Satisfaction"
                        ControlToValidate="ddlSatisfied" Display="Dynamic" InitialValue="0" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                </div>
                <div id="dvrating" runat="server" visible="false" class="col-sm-6 text-left linkBottom">
                    <div class="input-group input-group-sm">
                        <span class="input-group-addon" id="Span3">Rating (0-10)</span>
                        <asp:TextBox ID="txtRating" runat="server" class="form-control normalinput"></asp:TextBox>
                    </div>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="feedback" runat="server" ErrorMessage="Enter Rating"
                        ControlToValidate="txtRating" Display="Dynamic" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>

                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1"
                        ControlToValidate="txtRating"
                        ValidationExpression="\d+"
                        ValidationGroup="feedback" Display="Dynamic" SetFocusOnError="true"
                        ForeColor="Red"
                        ErrorMessage="Please enter numbers only"
                        runat="server" />
                    <asp:RangeValidator ID="rangeValidator2" runat="server" Type="Integer" Display="Dynamic" ForeColor="Red"
                        ValidationGroup="feedback" ControlToValidate="txtRating" MaximumValue="10" MinimumValue="1"
                        ErrorMessage="Please enter value between 1-10" />
                </div>
                <div id="Dvdescription" visible="false" runat="server" class="col-sm-6 text-left linkBottom">
                    <div class="input-group input-group-sm">
                        <span class="input-group-addon" id="Span5">Description</span>
                        <asp:TextBox ID="txtdscrpt" runat="server" TextMode="MultiLine" class="form-control normalinput"></asp:TextBox>
                    </div>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ValidationGroup="feedback" runat="server" ErrorMessage="Enter Description"
                        ControlToValidate="txtdscrpt" Display="Dynamic" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                </div>
                <div class="col-sm-12">
                    <div class="input-group">
                        <div class="col-sm controls">
                            <asp:Button ID="btnAdd" runat="server" ClientIDMode="Static" ValidationGroup="feedback" Text="Submit"
                                class="btn btn-success" OnClick="btnSubmit_Click" />

                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click"
                                class="btn btn-success" />
                        </div>
                    </div>
                </div>


            </div>

        </div>
        <div class="panel-heading">
            <h3 class="panel-title paneltitle">Feedback Details</h3>
        </div>
        <div class="panel-body table-responsive">

            <asp:Repeater ID="RptTickets" runat="server">
                <HeaderTemplate>
                    <table class="table table-hover  table-bordered  table-condensed table-responsive" data-height="400">
                        <thead style="color: White; background-color: #6b9297;">
                            <tr>
                               <%-- <th>FeedBackID</th>--%>
                                <th>RelatedTicketID</th>
                                <th>Engineer Name</th>
                                <th>Satisfied</th>
                                <th>Rating</th>

                                <th>Description</th>
                                <th>Created</th>

                            </tr>
                        </thead>
                </HeaderTemplate>
                <ItemTemplate>
                    <tbody>
                        <tr>
                           <%-- <td>
                                <asp:Label ID="lblFeedBackID" runat="server" Text='<%#Eval("FeedBackID") %>'></asp:Label>
                            </td>--%>
                            <td>
                                <asp:Label ID="lblRelatedTicketID" runat="server" Text='<%#Eval("RelatedTicketID") %>'></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblengName" runat="server" Text='<%#Eval("EngineerName") %>'></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblSatisfied" runat="server" Text='<%#Eval("Satisfied") %>'></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblRating" runat="server" Text='<%#Eval("Rating") %>'></asp:Label>
                            </td>

                            <td>
                                <asp:Label ID="lblFeedbackDesc" runat="server" Text='<%#Eval("FeedbackDesc") %>'></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="tblCreated" runat="server" Text='<%#Eval("Created") %>'></asp:Label>
                            </td>
                        </tr>
                    </tbody>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>
</asp:Content>
