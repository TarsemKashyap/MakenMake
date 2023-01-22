<%@ Page Title="Assessment Form" Language="C#" MasterPageFile="~/Pages/AdminMaster.Master" AutoEventWireup="true" CodeBehind="AssessmentForm.aspx.cs" Inherits="MakeNMake.Pages.AssessmentForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../Scripts/jquery-2.0.1.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#btnSubmit").click(function () {
                var itemData = "<data>";
                $(".paneldynamic").children().find('input').each(function (i, ele) {
                    debugger;
                    var element = ele;
                    var propertyid = ele.attributes.getNamedItem("propertyid");
                    itemData += "<child><id>" + propertyid.value + "</id><pvalue>" + ele.value + "</pvalue></child>";
                });
                itemData += "</data>";
                $("#hdndata").val(htmlEncode(itemData));
            });
        });

        function htmlEncode(value) {
            return $('<div/>').text(value).html();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="panel panel-success" id="outerdv" runat="server">
        <div class="panel-heading">
            <h3 class="panel-title paneltitle">Fill Assessment Form For Commercial Request</h3>
        </div>
        <div class="panel-body">
            <asp:HiddenField ID="hdndata" runat="server" ClientIDMode="Static" />
            <div class="col-sm-12 text-left linkBottom">
                <div class="input-group input-group-sm">
                    <span class="input-group-addon" id="Span31">Request ID</span>
                    <asp:TextBox ID="lblhreqserviceID" CssClass="form-control" Enabled="false" runat="server"></asp:TextBox>
                </div>
            </div>
            <div class="col-sm-12 text-left linkBottom">
                <div class="input-group input-group-sm">
                    <span class="input-group-addon" id="Span1">Remarks</span>
                    <asp:TextBox ID="txtRemarks" CssClass="form-control" placeholder="Enter Remarks" runat="server"></asp:TextBox>
                </div>
                <asp:RequiredFieldValidator ID="RequiredValidator1" runat="server" ForeColor="Red" ValidationGroup="fillform"
                    ErrorMessage="Enter Remarks" ControlToValidate="txtRemarks"></asp:RequiredFieldValidator>
            </div>

            <div class="col-sm-12 text-left linkBottom paneldynamic" runat="server" visible="false" id="dvPanel" style="padding-left: 0px;">
            </div>
            <div id="allbuttons" runat="server" class="col-sm-12 text-left linkBottom">
                <asp:Button ID="btnSubmit" runat="server" Visible="false" ClientIDMode="Static"
                    Text="Submit" OnClick="btnSubmit_Click" CssClass="btn btn-success" ValidationGroup="fillform" />
                <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" CssClass="btn btn-success" />
            </div>
        </div>
    </div>
</asp:Content>
