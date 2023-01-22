<%@ Page Title="Make N Make Support" Language="C#" MasterPageFile="~/Pages/AdminMaster.Master" AutoEventWireup="true" CodeBehind="MakeNMakeSupport.aspx.cs" Inherits="MakeNMake.Pages.MakeNMakeSupport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div id="Div1" class="panel panel-success">
            <div class="panel-heading">
                <h3 class="panel-title paneltitle">Customer HelpLine</h3>
            </div>
            <div class="panel-body" style="padding-left: 15px;">
                <asp:ImageButton ID="Imgsexygirl" style="width:230px;height:200px;margin-right:50px" runat="server" ImageUrl="~/Static/images/sexygirl.jpg" />
                <asp:ImageButton ID="ImageButton1" style="width:230px;height:200px;margin-right:50px" runat="server" ImageUrl="~/Static/images/ZOZO.jpg" />
                <%--<asp:ImageButton ID="ImageButton2" runat="server"  ImageUrl="~/Static/images/Funny.jpg" />--%>
                <asp:ImageButton ID="ImageButton3" style="width:250px;height:200px;margin-right:50px" runat="server" ImageUrl="~/Static/images/NaughtyGirl.jpg" />

                <div class="clear"></div>
                <div class="grid_5">
                    <h3>Contact Information</h3>
                    <p>24/7 support is on for all <span class="color1"><a href="#" rel="nofollow">Preminum Services</a></span> of MakenMake. any Services go without it.</p>
                    <p>Need help in customization? Ask guys from <span class="color1"><a href="#" rel="nofollow">MakenMake Team</a></span>.</p>
                    MakenMake Team
                                    <br>
                    Sector-62,A/56<br>
                    Block-C, Noida<br>
                    Mobile No: 919582100923<br>
                    FAX: +1 800 889 9898<br>
                    E-mail: <a href="#">bintesh@wiredsoft.org</a>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
