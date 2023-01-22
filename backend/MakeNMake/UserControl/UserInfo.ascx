<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserInfo.ascx.cs" Inherits="MakeNMake.UserControl.UserInfo" %>
<style type="text/css">
    .modal-content {
        min-height: 390px;
    }

    .modal-title {
        color: white;
    }

    .modal-header {
        background-color: #2BA2E7;
    }

    .user-row {
        margin-bottom: 14px;
    }

        .user-row:last-child {
            margin-bottom: 0;
        }

    .dropdown-user {
        margin: 13px 0;
        padding: 5px;
        height: 100%;
    }

        .dropdown-user:hover {
            cursor: pointer;
        }

    .table-user-information > tbody > tr {
        border-top: 1px solid rgb(221, 221, 221);
    }

        .table-user-information > tbody > tr:first-child {
            border-top: 0;
        }


        .table-user-information > tbody > tr > td {
            border-top: 0;
        }

    .toppad {
        margin-top: 20px;
    }
</style>
<div class="modal-dialog">
    <div class="col-xs-12">
        <div class="panel panel-info">
            <div class="panel-heading">
                <h3 class="panel-title">
                    <asp:Label ID="lblName" runat="server"></asp:Label></h3>
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="col-md-3 col-lg-3 " align="center">
                        <img alt="User Pic" src="../Static/images/userphoto.png" class="img-circle">
                    </div>
                    <div class=" col-md-9 col-lg-9 ">
                        <table class="table table-user-information">
                            <tbody>
                                <tr>
                                    <td>Mobile Number:</td>
                                    <td>
                                        <asp:Label ID="lblMobileNumber" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                 <tr>
                                    <td>EmailID:</td>
                                    <td>
                                        <asp:Label ID="lblEmailID" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Address:</td>
                                    <td>
                                        <asp:Label ID="lblAddress" runat="server"></asp:Label></td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
