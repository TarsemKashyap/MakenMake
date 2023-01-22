<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMaster.Master" AutoEventWireup="true" CodeBehind="testAmeyo.aspx.cs" Inherits="MakeNMake.testAmeyo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table>
        <tr>
            <td>Check By Emailid</td>
            <td>
                <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox></td>
            <td>
                <asp:Button ID="Button1" runat="server" Text="Test" OnClick="Button1_Click" />

            </td>
            <td>Check By MobileNumber</td>
            <td>  <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox></td>
            <td><asp:Button ID="Button2" runat="server" Text="Test" OnClick="Button2_Click" /></td>
            <td>Check By userID</td>
            <td>  <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox></td>
            <td><asp:Button ID="Button3" runat="server" Text="Test" OnClick="Button3_Click" /></td>
        </tr>
    </table>
</asp:Content>
