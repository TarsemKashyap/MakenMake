<%@ Page Title="Client History" Language="C#" MasterPageFile="AdminMaster.Master" AutoEventWireup="true" CodeBehind="ClientHistory.aspx.cs" Inherits="MakeNMake.Admin.ClientHistory" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../Scripts/jquery-2.0.1.min.js"></script>

    <link href="../Static/bootstrap/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <script src="../Static/bootstrap/bootstrap.min.js"></script>
    
    <link href="../Static/bootstrap/bootstrap-dialog.min.css" rel="stylesheet" type="text/css" />
    <script src="../Static/bootstrap/bootstrap-dialog.min.js"></script>

    <link href="../Datatable/jquery.dataTables.css" rel="stylesheet" />
   <%-- <script src="../Datatable/MakeNMakeDataTableJs.js"></script>--%>
    <script src="../Datatable/ClientHis.js"></script>
    
    
    <style type="text/css">
        .dataTables_length {
            display: none; 
        }
         #example_info {
            display: none; 
        }
        .dataTables_paginate {
            float:left;
        }
         .dataTables_filter {
        display: none; 
        }
        td.details-control {
            background: url('../Static/images/details_open.png') no-repeat center center;
            cursor: pointer;
        }

        tr.shown td.details-control {
            background: url('../Static/images/details_close.png') no-repeat center center;
        }
    </style>
    <script type="text/javascript">
        function OpenInvoice(userid) {
            $('#MyDialog').html('<iframe border=0 width="950px" height ="600px" src= "ClientInvoice.aspx?UserID=' + userid + '"> </iframe>').dialog({
                title: '',
                modal: true,
                autoOpen: true,
                height: '650',
                width: '1000',
                resizable: false,
                position: ['left+40', 'top+30'],
                closeOnEscape: false,
                dialogClass: "alert"
            });
            
            $(".modal-backdrop").hide();
            $(".modal").hide();
            return false;

        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="panel panel-success table-responsive">
        <div class="panel-heading">
            <h3 class="panel-title paneltitle">Client History</h3>
        </div>
        <div class="panel-body">
             <div class="input-group input-group-xs">

                <asp:TextBox ID="txtSearchclient" placeholder="Search User By Name,Client ID" Width="400px"
                    CssClass="form-control" runat="server"></asp:TextBox>
                <input type="button" value="Search" id="Button1" style="margin-left: 10px;"
                    class="btn btn-success"/>
                <%--  <input type="button" value="Refresh" id="Button2" style="margin-left: 10px;"
                    class="btn btn-success"/>--%>
            </div>
            <br />
            <table id="example"  class="display table table-hover  table-bordered  table-condensed">
                <thead style="color: White; background-color: #6b9297;">
                    <tr>
                        <th></th>
                        <th>Client Id</th>
                        <th>Client Name</th>
                        <th>Profile</th> 
                         <th>Bill Information</th>
                    </tr></thead>
                 
            </table>
        </div>
    </div>
    <script src="../Datatable/jquery.dataTables.min.js"></script>



    <div id="MyDialog" >        
                
    </div>
    
    <script src="../Dialog/jquery-ui.min.js"></script>
<link href="../Dialog/jquery-ui.css" rel="stylesheet" />
</asp:Content>