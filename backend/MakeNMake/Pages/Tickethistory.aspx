<%@ Page Title="Ticket History" Language="C#" MasterPageFile="AdminMaster.Master" AutoEventWireup="true" CodeBehind="Tickethistory.aspx.cs" Inherits="MakeNMake.Admin.Tickethistory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../Scripts/jquery-2.0.1.min.js"></script>
    <link href="../Datatable/jquery.dataTables.css" rel="stylesheet" />
    <script src="../Datatable/MakeNMakeDataTableJs.js"></script>


        <link href="../Static/bootstrap/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <script src="../Static/bootstrap/bootstrap.min.js"></script>
    
    <link href="../Static/bootstrap/bootstrap-dialog.min.css" rel="stylesheet" type="text/css" />
    <script src="../Static/bootstrap/bootstrap-dialog.min.js"></script>



    <style type="text/css">

        .dataTables_length {
            display: none; 
        }
       
        #example_info {
            display: none; 
        }
         .dataTables_filter {
        display: none; 
        }
        .modal-content {
            width:800px;
        }
        .modal-dialog {
            width: 800px;
            margin: 0px;
        }
        td.details-control {
            background: url('../Static/images/details_open.png') no-repeat center center;
            cursor: pointer;
        }

        tr.shown td.details-control {
            background: url('../Static/images/details_close.png') no-repeat center center;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="panel panel-success table-responsive">
        <div class="panel-heading">
            <h3 class="panel-title paneltitle">Ticket History</h3>
        </div>
        <div class="panel-body">
            <div class="input-group input-group-xs">
                <asp:TextBox ID="txtSearchclient" placeholder="Search User By Name,Ticket ID,Status" Width="400px"
                    CssClass="form-control" runat="server"></asp:TextBox>
               <input type="button" value="Search" id="Button1" style="margin-left: 10px;"
                    class="btn btn-success" />
            </div>
            <br />
            <table id="example"  class="display table table-hover  table-bordered  table-condensed">
                <thead style="color: White; background-color: #6b9297;">
                    <tr>
                        <th></th>
                        <th>Ticket Id</th>
                        <th>Client Name</th>
                        <th>Status </th>
                        <th>Creation : Date & Time(mm/dd/yy & hh:mm)</th>
                        <th>Closure : Date & Time(mm/dd/yy & hh:mm)</th>
                    </tr>
                </thead>
            </table>
        </div>
    </div>
    <script src="../Datatable/jquery.dataTables.min.js"></script>
</asp:Content>
