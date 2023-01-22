<%@ Page Title="Client History" Language="C#" MasterPageFile="~/Pages/AdminMaster.Master" AutoEventWireup="true" CodeBehind="FullClientHistory.aspx.cs" Inherits="MakeNMake.Pages.FullClientHistory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../Scripts/jquery-2.0.1.min.js"></script>
    <script src="../Dialog/jquery-ui.min.js"></script>
<link href="../Dialog/jquery-ui.css" rel="stylesheet" />
    <style type="text/css">
        .clear {
            clear: both;
            padding-left: 4px;
            padding-right: 4px;
        }
    </style>
    <script type="text/javascript">
        function CheckHistory(Id) {
            $('#MyDialog').html('<iframe border=0 width="950px" height ="600px" src= "CheckPlanHistory.aspx?AgreementID=' + Id + '"> </iframe>').dialog({
                title: '',
                modal: true,
                autoOpen: true,
                height: '650',
                width: '1000',
                resizable: true,
                position: ['left+40', 'top+30'],
                closeOnEscape: false,
                dialogClass: "alert"
            });
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="panel panel-success">
        <div class="panel-heading">
            <h3 class="panel-title paneltitle">Check Client History</h3>
        </div>
        <div class="panel-body" style="padding-left: 0px;">
            <div class="col-sm-12" style="padding: 0px;">
                <div class="col-sm-6 text-left linkBottom">
                    <div class="input-group input-group-sm">
                        <span class="input-group-addon" id="Span4">Select Client</span>
                        <asp:DropDownList ID="ddlClient" runat="server" CssClass="form-control  dropdown"></asp:DropDownList>
                    </div>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" InitialValue="0" runat="server" ControlToValidate="ddlClient"
                        ErrorMessage="Select Client" ValidationGroup="user" Display="Dynamic" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="col-sm-12 ">
                <asp:Button ID="btnRole" runat="server" Text="Check History" CssClass="btn btn-success" ValidationGroup="user" OnClick="btnRole_Click" />
                <asp:Button ID="btnCancels" runat="server" Text="Cancel" CssClass="btn btn-success" OnClick="btnCancels_Click" />
            </div>
            <div class="col-sm-12 " style="margin-top:50px;">
            <asp:Repeater ID="RptServices" runat="server" OnItemCommand="RptServices_ItemCommand" OnItemDataBound="RptServices_ItemDataBound">
                <ItemTemplate>
                    <div class="col-sm-5" style="margin: 5px; border: 1px #35A6E8 solid; padding: 0px;">
                        <div class="col-sm-12 clear">
                            <asp:LinkButton ID="lnkBtnPlan" runat="server" Text='<%#string.Format("Status : {0}",Eval("Status"))%>'></asp:LinkButton>
                        </div>
                        <div class="col-sm-12 clear">
                            <asp:Label ID="lblStatus" runat="server" Text='<%#string.Format("Agreement Number : {0}",Eval("AgreementNumber"))%>'></asp:Label>
                        </div>
                        <div class="col-sm-12 clear">
                            <asp:Label ID="lblPurchaseDate" runat="server" Text='<%#string.Format("PurchasedDate : {0}",Eval("PurchasedDate"))%>'>Purchased Date</asp:Label>
                        </div>
                        <div class="col-sm-12 clear">
                            <asp:Label ID="lblAmount" runat="server" Text='<%#string.Format("Amount : {0}",Eval("Amount"))%>'>Amount</asp:Label>
                        </div>
                        <div class="col-sm-12 clear">
                            <asp:Label ID="lblServices" runat="server" Text='<%#string.Format("Services : {0}",Eval("Services"))%>'></asp:Label>
                        </div>
                       <%-- <div class="col-sm-12 clear">
                            <asp:HiddenField ID="hdnAgreementID" runat="server" Value='<%#Eval("Agreementid") %>' />
                            <asp:LinkButton ID="lblHistory" runat="server" Text="Check history of Plan" CommandName="CheckHistory" CommandArgument='<%#Eval("Agreementid") %>'>
                            </asp:LinkButton>
                        </div>--%>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
            </div>
        </div>
    </div>
    <div id="MyDialog" style="height:700px;width:1000px">        
                
    </div>
</asp:Content>
