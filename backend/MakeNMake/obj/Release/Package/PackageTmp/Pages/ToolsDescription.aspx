<%@ Page Title="Tools" Language="C#" MasterPageFile="~/Pages/AdminMaster.Master" AutoEventWireup="true" CodeBehind="ToolsDescription.aspx.cs" Inherits="MakeNMake.ToolsDescription" %>
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


        <div class="panel panel-success table-responsive">
        <div class="panel-heading" runat="server" id="divaddskill">
            <h3 class="panel-title paneltitle">Tools Description</h3>
        </div>
        <br />
        <div class="panel-body" style="padding: 0px;">
            <div class="col-sm-12" style="padding: 0px;">
                <div class="col-sm-6 text-left linkBottom">
                <div class="input-group input-group-sm">
                    <span class="input-group-addon">Tool Name</span>
                    <asp:TextBox ID="txttoolName"  placeholder="Enter Tool Name" CssClass="form-control"  runat="server"></asp:TextBox>
                         
                </div>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Enter Tool Name" ValidationGroup="signup"
                    ForeColor="Red" Display="Dynamic" SetFocusOnError="true" ControlToValidate="txttoolName"></asp:RequiredFieldValidator>
            </div>
                
                <div class="col-sm-6 text-left linkBottom">
                    <div class="input-group input-group-sm">
                        <span class="input-group-addon" id="Span4">Tool Type</span>
                        <asp:DropDownList ID="ddltooltype" runat="server" CssClass="form-control  dropdown">
                            <asp:ListItem Value="0" Text="Select Tool Type"></asp:ListItem>
                            <asp:ListItem Value="1" Text="For Repair" Enabled="true"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" InitialValue="0" ControlToValidate="ddltooltype"
                        ErrorMessage="Select Tool Type" ValidationGroup="user" Display="Dynamic" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                </div>
                <div class="col-sm-6 text-left linkBottom linkBottom">
                    <div class="input-group input-group-sm">
                        <span class="input-group-addon" >Quantity</span>
                        <asp:TextBox ID="txtquantity" CssClass="form-control" placeholder="Enter Quantity" runat="server"></asp:TextBox>
                    </div>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtquantity"
                        ErrorMessage="Enter Quantity" ValidationGroup="user" Display="Dynamic" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                   
                </div>
                <div class="col-sm-6 text-left linkBottom linkBottom">
                    <div class="input-group input-group-sm">
                        <span class="input-group-addon" id="Span1">Description</span>
                        <asp:TextBox ID="txtdescription" CssClass="form-control" placeholder="Enter Description" runat="server"></asp:TextBox>
                    </div>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtdescription"
                        ErrorMessage="Enter Description" ValidationGroup="user" Display="Dynamic" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                   
                </div>
                 <div class="btn-group col-sm-6 ">
                    <div class="input-group input-group-sm">
                        
                        <asp:RadioButtonList ID="rdbStatus" Margin="0.5em;" runat="server" RepeatDirection="Horizontal">

                            <asp:ListItem style="margin-right: 20px;" Text="Active" Value="1" />

                            <asp:ListItem Text="Inactive" Value="0" />

                        </asp:RadioButtonList>
                    </div>
                </div>
                <br />
             

            </div>

            <div class="col-sm-6 text-left linkBottom linkBottom">
                <asp:Button ID="btnSubmit" runat="server" Text="Add" 
                    CssClass="btn btn-success" ValidationGroup="user" OnClick="btnSubmit_Click" />
                <asp:Button ID="btnCancel" runat="server" Text="Cancel" 
                    CssClass="btn btn-success" OnClick="btnCancel_Click" />
            </div>
        </div>
    </div>
    <div id="Div1" runat="server" visible="false" class="col-sm-6 text-left linkBottom table-responsive" style="padding-left: 0px;">
                <div class="input-group input-group-sm">


                    <span class="input-group-addon" id="Span2">Select number of items to be displayed per page </span>

                    <asp:DropDownList ID="ddlIndex" CssClass="form-control dropdown" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlIndex_SelectedIndexChanged">
                    </asp:DropDownList>
                </div>
            </div>
     <div class="panel panel-success">
        <div class="panel-heading">
            <h3 class="panel-title paneltitle">Tool List</h3>
        </div>
     <div class="panel-body table-responsive" id="divClientList" runat="server">
              
                    <asp:HiddenField ID="hdnServiceID" runat="server"/>
                    <asp:Repeater ID="RptService" runat="server" OnItemCommand="RptService_ItemCommand" >
                        <HeaderTemplate>
                            <table class="table table-hover  table-bordered  table-condensed" data-height="400">
                                <thead style="color: White; background-color: #6b9297;">
                                    <tr>
                                        <th>Tool Name</th>
                                        <th>Tool Type</th>
                                        <th>Total Quantity</th>
                                        <th>Issued</th>
                                        <th>Description</th>
                                        <th>Status</th>
                                        <th>Edit </th>
                                        <th>Delete</th>
                                    </tr>
                                </thead>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tbody>
                                <tr>
                                    <td><asp:Label ID="Llblname" runat="server" Text='<%#Eval("ToolName") %>'></asp:Label></td>
                                    <td>
                                        <asp:HiddenField ID="hdnID" runat="server" Value='<%#Eval("ToolId") %>' />
                                        <asp:Label ID="lblType" runat="server" Text='<%#Eval("ToolType") %>'></asp:Label></td>
                                    <td>
                                       
                                        <asp:Label ID="lblQuantity" runat="server" Text='<%#Eval("Quantity") %>'></asp:Label></td>
                                    <td>                                       
                                        <asp:Label ID="lblStock" runat="server" Text='<%#Eval("issued") %>'></asp:Label></td><td>                                       
                                        <asp:Label ID="lbldescription" runat="server" Text='<%#Eval("Description") %>'></asp:Label></td>
                                    
                                 <td>
                                    <asp:Label ID="lblStatus" ClientIDMode="Static" runat="server" Text='<%# Convert.ToInt16( Eval("Status"))==1?"Active":"Unactive"%>'></asp:Label></td>
                             
                                  <td>
                                        <center>     <asp:ImageButton ID="ImgBtnEdit" runat="server" 
                                             ImageUrl="~/Static/images/edit.png" CommandName="edit" CommandArgument='<%#Eval("ToolId") %>' /></center>
                                    </td>
                                    <td>
                                        <center> <asp:ImageButton ID="ImgBtnDelete" OnClientClick="javascript:return DeleteConfirmation(this);"
                                            runat="server" ImageUrl="~/Static/images/trash.png"
                                            CommandName="delete" CommandArgument='<%#Eval("ToolId") %>' /></center>

                                    </td>
                                    </tr>
                            </tbody>
                        </ItemTemplate>
                        <FooterTemplate>
                            </table>
                        </FooterTemplate>
                    </asp:Repeater>

              <table id="tblPaging" runat="server" class="tblpaging" style="font-size: 12px;clear:both;margin-top:15px;">
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

