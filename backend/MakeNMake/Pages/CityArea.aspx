<%@ Page Title="Add City" Language="C#" MasterPageFile="AdminMaster.Master" AutoEventWireup="true" CodeBehind="CityArea.aspx.cs" Inherits="MakeNMake.Admin.CityArea" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="panel panel-success">
        <div class="panel-heading">
            <h3 class="panel-title paneltitle">City</h3>
        </div>
      
        <div class="panel-body" style="padding-left:0px;">
              <%--<asp:ValidationSummary ID="ValidationSummary1" ValidationGroup="user2" ShowSummary="true" ForeColor="Red" runat="server" />  --%>
            <div class="col-sm-6 text-left linkBottom">
                    <div class="input-group input-group-sm">
                        <span class="input-group-addon" id="Span5">Country</span>
                        <asp:DropDownList ID="ddlCountry" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged" CssClass="form-control  dropdown"></asp:DropDownList>
                    </div>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" InitialValue="0" runat="server" ControlToValidate="ddlCountry"
                        ErrorMessage="Enter Country" ValidationGroup="user2"  Display="Dynamic" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                </div>
             <div class="col-sm-6 text-left linkBottom">
                    <div class="input-group input-group-sm">
                        <span class="input-group-addon" id="Span2">State</span>
                        <asp:DropDownList ID="ddlstate" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlstate_SelectedIndexChanged" CssClass="form-control  dropdown"></asp:DropDownList>
                    </div>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" InitialValue="0" runat="server" ControlToValidate="ddlstate"
                        ErrorMessage="Select State" ValidationGroup="user2"  Display="Dynamic" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                </div>
             <div class="col-sm-6 text-left linkBottom">
                    <div class="input-group input-group-sm">
                        <span class="input-group-addon" id="Span4">District</span>
                        <asp:DropDownList ID="ddlDistrict" runat="server"  CssClass="form-control  dropdown"></asp:DropDownList>
                    </div>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" InitialValue="0" runat="server" ControlToValidate="ddlDistrict"
                        ErrorMessage="Select District" ValidationGroup="user2"  Display="Dynamic" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                </div>

            <div class="col-sm-6 text-left linkBottom ">
                    <div class="input-group input-group-sm">
                        <span class="input-group-addon" id="Span3">City</span><asp:TextBox ID="Txtcity" CssClass="form-control" placeholder="Enter City" runat="server"></asp:TextBox>
                    </div>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5"  runat="server" ControlToValidate="Txtcity"
                        ErrorMessage="Enter City" ValidationGroup="user2"  Display="Dynamic" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                </div>
            <div class="col-sm-6 text-left linkBottom linkBottom">
                    <div class="input-group input-group-sm">
                        <span class="input-group-addon" id="Span1">PinCode</span>
                        <asp:TextBox ID="txtcode" onkeypress="return ValidateNumber(event)" CssClass="form-control" MaxLength="6" placeholder="Enter Pin code" runat="server"></asp:TextBox>
                    </div>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1"   runat="server" ControlToValidate="txtcode"
                        ErrorMessage="Enter Pincode" ValidationGroup="user2"  Display="Dynamic" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                </div>
            <div class="col-sm-12 text-left linkBottom linkBottom">
                
            <asp:Button ID="btncity" runat="server" Text="Add"  CssClass="btn btn-success" ValidationGroup="user2" OnClick="btncity_Click"  />
                <asp:Button ID="btnAll" runat="server" Text="View All Cities"  CssClass="btn btn-success" OnClick="btnAll_Click" />
                <asp:Button ID="btnCancel" OnClick="btnCancel_Click" runat="server" Text="Cancel"  CssClass="btn btn-success"  />
            </div>
        </div>
            
    </div>
</asp:Content>