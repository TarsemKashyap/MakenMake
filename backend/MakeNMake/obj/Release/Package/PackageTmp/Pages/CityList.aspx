<%@ Page Title="CityList" Language="C#" MasterPageFile="AdminMaster.Master" AutoEventWireup="true" CodeBehind="CityList.aspx.cs" Inherits="MakeNMake.Admin.CityList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="panel panel-success">
        <div class="panel-heading">
            <h3 class="panel-title paneltitle">Cities</h3>
        </div>
        <div class="panel-body" style="padding-left:0px;">
            <div class="col-sm-12" style="padding-left:0px;">

                <div class="col-sm-6 text-left linkBottom">
                    <div class="input-group input-group-sm">
                        <span class="input-group-addon" id="Span4">Select Country </span>
                        <asp:DropDownList ID="ddlCountry" CssClass="form-control dropdown" AutoPostBack="true" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged" runat="server">
                        </asp:DropDownList>
                    </div>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ValidationGroup="service" runat="server" ErrorMessage="Select Country"
                        ControlToValidate="ddlCountry"
                        InitialValue="0" Display="Dynamic" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                </div>
                <div class="col-sm-6 text-left linkBottom">
                    <div class="input-group input-group-sm">
                        <span class="input-group-addon" id="Span1">Select State </span>
                        <asp:DropDownList ID="ddlState" CssClass="form-control dropdown" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlState_SelectedIndexChanged">
                        </asp:DropDownList>
                    </div>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="service" runat="server" ErrorMessage="Select State"
                        ControlToValidate="ddlState"
                        InitialValue="0" Display="Dynamic" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                </div>

                <div class="col-sm-6 text-left linkBottom">
                    <div class="input-group input-group-sm">
                        <span class="input-group-addon" id="Span2">Select District </span>
                        <asp:DropDownList ID="ddlDistrict" CssClass="form-control dropdown" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged">
                        </asp:DropDownList>
                    </div>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="service" runat="server" ErrorMessage="Select District"
                        ControlToValidate="ddlDistrict"
                        InitialValue="0" Display="Dynamic" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                </div>

            </div>
        </div>

    </div>

    <div class="panel panel-success" id="divrptdistrict" runat="server" visible="false">
        <div class="panel-heading">
            <h3 class="panel-title paneltitle">Cities In District
                <asp:Label ID="lblhdcity_name" runat="server" /></h3>
        </div>
        <div class="panel-body">
            <asp:Repeater ID="RptCity" runat="server">
                <HeaderTemplate>
                    <table class="table table-hover  table-bordered  table-condensed" data-height="400">
                        <thead style="color: White; background-color: #6b9297;">
                            <tr style="width: 80%">
                                <th>City Name</th>

                                <th>City Code</th>

                            </tr>
                        </thead>
                </HeaderTemplate>
                <ItemTemplate>
                    <tbody>
                        <tr>
                            <td>
                                <asp:HiddenField ID="hdnID" runat="server" />
                                <asp:Label ID="lblC_name" runat="server" Text='<%#Eval("CityName") %>'>></asp:Label></td>
                            <td>
                                <asp:Label ID="lblStates" runat="server" Text='<%#Eval("CityCode") %>'>></asp:Label></td>
                    </tbody>
                </ItemTemplate>
                <FooterTemplate>
                    </table>
                </FooterTemplate>
            </asp:Repeater>
            <table class="tblpaging" style="font-size: 12px; clear: both; margin-top: 15px;">
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
