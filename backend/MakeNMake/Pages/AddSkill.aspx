<%@ Page Title="Add Skills" Language="C#" MasterPageFile="AdminMaster.Master" AutoEventWireup="true" CodeBehind="AddSkill.aspx.cs" Inherits="MakeNMake.Admin.AddSkill" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="panel panel-success">
        <div class="panel-heading" runat="server" id="divaddskill">
            <h3 class="panel-title paneltitle">Add Skill</h3>
        </div>
        <div class="panel-heading" visible="false" runat="server" id="diveditskill">
            <h3 class="panel-title paneltitle">Edit Skill</h3>
        </div>
        <br />
        <div class="panel-body" style="padding: 0px;">
            <div class="col-sm-12" style="padding: 0px;">
                <asp:HiddenField ID="hdnSkillid" runat="server" />
                <div class="col-sm-6 text-left linkBottom">
                    <div class="input-group input-group-sm">
                        <span class="input-group-addon" id="Span1">Name</span>
                        <asp:Label ID="lblname" CssClass="form-control" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="col-sm-6 text-left linkBottom">
                    <div class="input-group input-group-sm">
                        <span class="input-group-addon" id="Span2">Total Skills</span>
                        <asp:Label ID="lbltotalskills" CssClass="form-control" runat="server"></asp:Label>          
                    </div>
                </div>
                <div class="col-sm-6 text-left linkBottom">
                    <div class="input-group input-group-sm">
                        <span class="input-group-addon" id="Span5">Select Skill</span>
                        <asp:DropDownList ID="ddlpskill" runat="server" CssClass="form-control  dropdown"></asp:DropDownList>
                    </div>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlpskill"
                        ErrorMessage="Select Skill" ValidationGroup="user" InitialValue="0" Display="Dynamic" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                </div>
                <div class="col-sm-6 text-left linkBottom">
                    <div class="input-group input-group-sm">
                        <span class="input-group-addon" id="Span4">Skill Type</span>
                        <asp:DropDownList ID="ddlskilltype" runat="server" CssClass="form-control  dropdown">
                            <asp:ListItem Enabled="true" Value="0" Text="Select Skill Type"></asp:ListItem>
                            <asp:ListItem Value="P" Text="Primary Skill" Enabled="true"></asp:ListItem>
                            <asp:ListItem Value="S" Text="Secondary Skill"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" InitialValue="0" ControlToValidate="ddlskilltype"
                        ErrorMessage="Select Skill Type" ValidationGroup="user" Display="Dynamic" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                </div>
                <div class="col-sm-6 text-left linkBottom linkBottom">
                    <div class="input-group input-group-sm">
                        <span class="input-group-addon" >Skill Rate 1-10</span>
                        <asp:TextBox ID="txtskillrate" CssClass="form-control" placeholder="Enter skill rate" runat="server"></asp:TextBox>
                    </div>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtskillrate"
                        ErrorMessage="Enter Skill Rate" ValidationGroup="user" Display="Dynamic" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1"
                        ControlToValidate="txtskillrate"
                        ValidationExpression="\d+"
                        ValidationGroup="user" Display="Dynamic" SetFocusOnError="true"
                        ForeColor="Red"
                        ErrorMessage="Enter numbers only"
                        runat="server" />
                    <asp:RangeValidator ID="rangeValidator2" runat="server" Type="Integer" Display="Dynamic" ForeColor="Red"
                        ValidationGroup="user" ControlToValidate="txtskillrate" MaximumValue="10" MinimumValue="1"
                        ErrorMessage="Enter value between 1-10" />
                </div>
            </div>

            <div class="col-sm-6 text-left linkBottom linkBottom">
                <asp:Button ID="btnSubmit" runat="server" Text="Add" OnClick="btnSubmit_Click"
                    CssClass="btn btn-success" ValidationGroup="user" />
                <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click"
                    CssClass="btn btn-success" />
            </div>
        </div>
    </div>
    <div class="panel panel-success">
        <div class="panel-body">
            <asp:Repeater ID="Rptskill" runat="server" OnItemCommand="RptClient_ItemCommand">
                <HeaderTemplate>
                    <table class="table table-hover  table-bordered  table-condensed" data-height="400">
                        <thead style="color: White; background-color: #6b9297;">
                            <tr>
                                <th>Skills</th>
                                <th>Rate</th>
                                <th>Skill Type</th>
                                <th>Edit</th>
                                <th>Delete</th>
                            </tr>
                        </thead>
                </HeaderTemplate>
                <ItemTemplate>
                    <tbody>
                        <tr>
                            <td>
                                <asp:Label ID="lblName" runat="server" Text='<%#Eval("ServiceName") %>'></asp:Label>
                            </td>
                            <td>
                                <asp:HiddenField ID="serviceid" runat="server" Value='<%#Eval("serviceid") %>' />
                                <asp:Label ID="lblEmail" runat="server" Text='<%#Eval("Rate") %>'></asp:Label></td>
                            <td>
                                <asp:Label ID="lblMobile" runat="server" Text='<%#Convert.ToString(Eval("SkillType"))=="P"?"Primary":"Secondary" %>'></asp:Label>
                                <asp:Label ID="lblservicetype" Visible="false" runat="server" Text='<%#Eval("SkillType") %>'></asp:Label>
                            </td>


                            <td>
                                <center>   <asp:ImageButton ID="ImgBtnEdit" runat="server" Width="24" Height="24"
                                                ImageUrl="~/Static/images/edit.png" CommandName="edit" CommandArgument='<%#Eval("SkillID") %>' /></center>
                            </td>

                            <%--<td>
                                <center><asp:Button ID="btndelete" runat="server" CommandName="Delete"
                                                Text="Delete" CssClass="btn-xs btn-success" CommandArgument='<%#Eval("SkillID") %>' /></center>
                            </td>--%>

                            <td>
                                    <center><asp:ImageButton ID="ImgBtnDelete" OnClientClick="javascript:return DeleteConfirmation(this);"
                                    runat="server" ImageUrl="~/Static/images/trash.png"
                                    CommandName="Delete" CommandArgument='<%#Eval("SkillID") %>' /></center>
                                </td>
                    </tbody>
                </ItemTemplate>
                <FooterTemplate>
                    </table>
                </FooterTemplate>
            </asp:Repeater>
            <table id="tblPaging" runat="server" class="tblpaging" style="font-size: 12px; clear: both; margin-top: 15px;">
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
