<%@ Page Title="Ticket Detail" Language="C#" MasterPageFile="AdminMaster.Master" AutoEventWireup="true" CodeBehind="TicketHistoryViaTicketID.aspx.cs" Inherits="MakeNMake.Admin.TicketHistoryViaTicketID" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function ShowMsg(ele) {
            alert(ele.nextElementSibling.value);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   


     <div class="panel panel-success" runat="server" id="DvTicket_History" visible="false">
        <div class="panel-heading">
            <h3 class="panel-title paneltitle">Inspection & Quote History</h3>
        </div>
        <div class="panel-body">
            <div id="Div1" class="panel-heading"  runat="server" style="padding-left: 0px;">

                <div class="col-sm-6 text-left linkBottom" style="width:300px">
                    <div class="input-group input-group-sm">
                        <span class="input-group-addon" id="Span1">Ticket ID</span>
                        <asp:TextBox ID="lblhstryTicketID" CssClass="form-control" Enabled="false" Width="150px" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="col-sm-6 text-left linkBottom" style="width:300px">
                    <div class="input-group input-group-sm">
                        <span class="input-group-addon" id="Span4">Select Status</span>
                        <asp:DropDownList ID="ddlStatus" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged" Width="200px" CssClass="form-control  dropdown">
                             <asp:ListItem  Value="0" Text="--Select Status--"></asp:ListItem>
                            <asp:ListItem  Value="1" Text="Assign"></asp:ListItem>
                            <asp:ListItem Value="2" Text="Reject" ></asp:ListItem>
                        </asp:DropDownList>
                        </div><asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlStatus"
                        ErrorMessage="Please Select Status" InitialValue="0" ValidationGroup="changeStatus"  Display="Dynamic" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                    
                   
                </div>
                <div id="dvAssigned" runat="server" class="col-sm-6 text-left linkBottom" style="width:300px">
                    <div class="input-group input-group-sm">
                        <span class="input-group-addon" id="Span4">Assigned To</span>
                        <asp:DropDownList ID="ddlEngineer" runat="server" AutoPostBack="true" Width="200px"  OnSelectedIndexChanged="ddlEngineer_SelectedIndexChanged" CssClass="form-control  dropdown"></asp:DropDownList>
                    </div>
                   <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlEngineer"
                        ErrorMessage="Please Select Engineer" ValidationGroup="changeStatus" InitialValue="0"
                         Display="Dynamic" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                </div>

                <div id="DivComments" class="col-sm-6 text-left linkBottom" runat="server" visible="false">
                    <div class="input-group input-group-sm">
                        <span class="input-group-addon" id="Span4">Comments</span>
                        <asp:TextBox ID="txtComments" CssClass="form-control" placeholder="Please Enter Comments" TextMode="MultiLine" Width="300px" runat="server"></asp:TextBox>
                    </div>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtComments"
                        ErrorMessage="Please Enter Comments" ValidationGroup="changeStatus"  Display="Dynamic" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                  
                </div>
                <div id="divSubmitButton" runat="server"  class="col-sm-6 text-left linkBottom">
                      <asp:Button ID="btnSubmit" runat="server" Text="Add"
                           CssClass="btn btn-success" ValidationGroup="changeStatus" OnClick="btnSubmit_Click" />
               </div>

             
         <asp:Repeater ID="RptTickets" runat="server" OnItemCommand="RptTickets_ItemCommand" OnItemDataBound="RptTickets_ItemDataBound">
                    <HeaderTemplate>
                        <table class="table table-hover  table-bordered  table-condensed" data-height="400">
                            <thead style="color: White; background-color: #6b9297;">
                                <tr>
                                    <th>AssignedTo</th>
                                    <th>Last Modified By</th>
                                    <th>Reason</th>
                                    <th>Modified Date</th>
                                    <th>Status</th>
                                    <th>Work History</th>
                                </tr>
                            </thead>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tbody style="text-align: center !important">
                            <tr>

                               <td>
                                    <asp:Label ID="LablblEngineer" runat="server" Text='<%#Eval("AssignedTo") %>'></asp:Label>
                                    
                                </td>


                                <td>
                                    <asp:Label ID="lblmodified" runat="server" Text='<%#Eval("ModifiedBy") %>'></asp:Label></td>
                                <td>
                                    <asp:Label ID="lblcreated" Text='<%#Eval("Remark") %>' runat="server"></asp:Label>
                                    <asp:LinkButton ID="lnkBtnMore" ClientIDMode="Static" runat="server">....read more</asp:LinkButton>
                                    <asp:HiddenField ID="hdnFullMsg" ClientIDMode="Static" runat="server" Value='<%#Eval("Remark") %>' />
                                </td>
                                <td>
                                    <asp:Label ID="Label1" runat="server" Text='<%#Eval("Modified") %>'></asp:Label></td>
                                <td>
                                    <asp:Label ID="lblType" runat="server" Text='<%#Eval("Status") %>'></asp:Label></td>
                                <td>
                                    <asp:LinkButton ID="lknworkhis" runat="server" CommandName="WorkHistory" CommandArgument='<%Eval("TicketID") %>'>WorkHistory</asp:LinkButton>
                                </td>

                            </tr>
                        </tbody>
                    </ItemTemplate>
                    <FooterTemplate>
                        </table>
                    </FooterTemplate>
                </asp:Repeater>
           <table id="tblticket" runat="server" class="tblpaging" style="font-size: 12px; clear: both; margin-top: 15px;">
                <tr>
                    <td colspan="5"></td>
                </tr>
                <tr>
                    <td width="32" valign="top" align="center">
                        <asp:LinkButton ID="lnkFirst2" runat="server" OnClick="lnkFirst2_Click">First</asp:LinkButton>
                    </td>
                    <td width="80" valign="top" align="center">
                        <asp:LinkButton ID="lnkPrevious2" runat="server" OnClick="lnkPrevious2_Click">Previous</asp:LinkButton>
                    </td>
                    <td>
                        <asp:DataList ID="RepeaterPaging2" runat="server" RepeatDirection="Horizontal" OnItemCommand="RepeaterPaging2_ItemCommand"
                            OnItemDataBound="RepeaterPaging2_ItemDataBound">
                            <ItemTemplate>
                                <asp:LinkButton ID="Pagingbtn2" runat="server" CommandArgument='<%# Eval("PageIndex2") %>'
                                    CommandName="newpage" Text='<%# Eval("PageText2") %> ' Width="20px"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:DataList>
                    </td>
                    <td width="80" valign="top" align="center">
                        <asp:LinkButton ID="lnkNext2" runat="server" OnClick="lnkNext2_Click">Next</asp:LinkButton>
                    </td>
                    <td width="80" valign="top" align="center">
                        <asp:LinkButton ID="lnkLast2" runat="server" OnClick="lnkLast2_Click">Last</asp:LinkButton>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="left" height="30">
                        <asp:Label Style="padding-left: 4px;" ID="lblpage2" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
            </table>
                </div>

              <div class="input-group">
                     <div class="col-sm controls">                           
                            <asp:Button ID="btnBack" runat="server" Text="Back" OnClick="btnBack_Click"
                                class="btn btn-success" />
                      </div>
               </div>

        </div>
    </div>

</asp:Content>
