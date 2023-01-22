<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/AdminMaster.Master" AutoEventWireup="true" CodeBehind="Reports.aspx.cs" Inherits="MakeNMake.Pages.Reports" %>

<%@ Register Src="~/UserControl/RegistredUser.ascx" TagPrefix="uc1" TagName="RegistredUser" %>
<%@ Register Src="~/UserControl/TicketsUsers.ascx" TagPrefix="uc1" TagName="TicketsUsers" %>
<%@ Register Src="~/UserControl/ServiceEngineersReport.ascx" TagPrefix="uc1" TagName="ServiceEngineersReport" %>
<%@ Register Src="~/UserControl/CreditUser.ascx" TagPrefix="uc1" TagName="CreditUser" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="http://maxcdn.bootstrapcdn.com/bootstrap/3.3.2/css/bootstrap.min.css" />
<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.9.1/jquery.min.js"></script>
<script type="text/javascript" src="http://maxcdn.bootstrapcdn.com/bootstrap/3.3.2/js/bootstrap.min.js"></script>
    <style>
        .nav-tabs > li > a:hover {
   background: #37A7E8 !important;
   color:#fff;
}
        .nav > li > a:hover, .nav > li > a:focus
        {
            background: #37A7E8 !important;
            color:#fff;

        }
        .nav > li >a:active
        {
            background: #37A7E8 !important;
            color:#fff;
        }
    </style>
  
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="panel panel-default" style="padding: 10px; margin: 10px">
    <div id="Tabs" role="tabpanel">
        <!-- Nav tabs -->
        <ul class="nav nav-tabs" role="tablist">
            <li  class="active"><a href="#RegisterUser" aria-controls="RegisterUser" role="tab" data-toggle="tab" style="background:#eee">
              Registered  User </a></li>
            <li><a href="#Ticketuser" aria-controls="Ticketusers" role="tab" data-toggle="tab" style="background:#eee">Ticket Users</a></li>
              <li><a href="#ServiceEngineer" aria-controls="ServiceEngineer" role="tab" data-toggle="tab" style="background:#eee">Service Engineers</a></li>
            <li><a href="#AddCredit" aria-controls="AddCredit" role="tab" data-toggle="tab" style="background:#eee">Add Credit</a></li>
        </ul>
        <!-- Tab panes -->
        <div class="tab-content" style="padding-top: 20px">
            <div role="tabpanel" class="tab-pane active" id="RegisterUser">
                  <uc1:RegistredUser runat="server" id="RegistredUser" />
            </div>
            <div role="tabpanel" class="tab-pane" id="Ticketuser">
             <uc1:TicketsUsers runat="server" ID="TicketsUsers" />

            </div>
             <div role="tabpanel" class="tab-pane" id="ServiceEngineer">
              <uc1:ServiceEngineersReport runat="server" id="ServiceEngineersReport" />

            </div>
             <div role="tabpanel" class="tab-pane" id="AddCreditUser">
                <uc1:credituser runat="server" id="CreditUser"/>

            </div>
        </div>
    </div>
</div>
    <asp:HiddenField ID="TabName" runat="server" />
    <script type="text/javascript">
        $(function () {
           
            var tabName = $("#ContentPlaceHolder1_TabName").val() != "" ? $("#ContentPlaceHolder1_TabName").val() : "personal";
            $('#Tabs a[href="#' + tabName + '"]').tab('show');
            $("#Tabs a").click(function () {
                
                $("#ContentPlaceHolder1_TabName").val($(this).attr("href").replace("#", ""));
            });
        });
</script>
   
</asp:Content>
