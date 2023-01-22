<%@ Page Title="Service Time" Language="C#" MasterPageFile="AdminMaster.Master" AutoEventWireup="true" CodeBehind="ServicesTime.aspx.cs" Inherits="MakeNMake.ServiceEngineer.ServicesTime" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
   <script src="//ajax.googleapis.com/ajax/libs/jquery/1.11.1/jquery.min.js"></script>
    <link href="../Static/bootstrap/bootstrap.css" rel="stylesheet" />
    <script type="text/javascript">
        function doSomeStuff() {
            var count = 0;
            var imageName = "";
            $("#uploaderDiv div > img").each(function (ind, ele) {
                debugger;
                count++;
                imageName += $(ele).attr("ImageName") + ";";
            });
            if (count > 0) {
                if (count > 3) {
                    alert("You can upload maximum 3 photos");
                    return false;
                }
                else {
                    $("#NewImages").val(imageName);
                    return true;
                }
            }
            //else {
            //    alert("Please upload photos");
            //    return false;
            //}
        }
        $(document).ready(function () {

            //$('#btnSubmit"').live('click', function (e) {

            // });
        });

        function showimagepreview(input) {
            var dvPreview = $("#uploaderDiv");
            dvPreview.html("");
            var regex = /^([a-zA-Z0-9\s_\\.\-:])+(.jpg|.jpeg|.gif|.png|.bmp)$/;
            $(input.files).each(function (ind, ele) {
                if (regex.test(ele.name.toLowerCase())) {
                    var reader = new FileReader();
                    reader.onload = function (e) {
                        dvPreview.append('<div style="float:left;width:100px;margin-right:10px;"><img style="float:left;" ImageName="' +
                        ele.name + '" height="100" width="100" id="theImg" src="' + e.target.result
                        + '" /><a style="float:left;cursor:pointer;" onclick="deleteFile(this)">Delete</a> </div>');
                    }
                    reader.readAsDataURL(input.files[ind]);
                } else {
                    alert(file[ind].name + " is not a valid image file.");
                    dvPreview.html("");
                    return false;
                }
            });
        }
        function deleteFile(ele) {
            ele.parentElement.remove();
        }
        
      function ShowMsg(ele) {
          alert(ele.nextElementSibling.value);
          return false;
      }
    </script>
  
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="panel panel-success">
        <div class="panel-heading">
            <h3 class="panel-title paneltitle">Service Time</h3>
        </div>
        <div class="panel-body" style="padding-left:0px;">
            <div class="col-sm-12" style="padding-left:0px;">

                <asp:HiddenField ID="NewImages" runat="server" ClientIDMode="Static" />
                <div id="dvticket" visible="false" runat="server" class="col-sm-6 text-left linkBottom">
                    <div class="input-group input-group-sm">
                        <span class="input-group-addon" id="Span1">Select TicketID</span>
                        <asp:DropDownList ID="ddlticket" CssClass="form-control dropdown" AutoPostBack="true" 
                            OnSelectedIndexChanged="ddlticket_SelectedIndexChanged" runat="server">
                        </asp:DropDownList>
                    </div>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="service" runat="server" ErrorMessage="Select Ticket ID"
                        ControlToValidate="ddlticket"
                        InitialValue="0" Display="Dynamic" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                </div>
                <div id="dvOTP" runat="server" class="col-sm-12" visible="false" style="padding: 0;">
                    <div class="col-sm-12 text-left linkBottom" style="margin-bottom:30px;">
                        <asp:RadioButtonList ID="rdbUserNumbers" runat="server"></asp:RadioButtonList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ValidationGroup="service" runat="server" ErrorMessage="Please select number"
                         ControlToValidate="rdbUserNumbers"  Display="Dynamic" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                        </div>
                    <div class="col-sm-6 text-left linkBottom">
                        <asp:Button ID="btnSent" runat="server" ValidationGroup="service" CssClass="btn-sm btn-success"  OnClick="btnSent_Click" />
                    </div>
                    <div class="col-sm-3 text-left linkBottom">
                        <div class="input-group input-group-sm">
                            <%--   <span class="input-group-addon" id="Span2">Send OTP for confirmation</span>--%>
                            <asp:HiddenField ID="hdnMobileNumber" runat="server" />
                            <asp:HiddenField ID="hdnName" runat="server" />
                            <asp:HiddenField ID="hdnUserID" runat="server" />
                        </div>
                    </div>
                </div>
                <div id="dvVerifyOTP" class="col-sm-12" runat="server" visible="false" style="padding: 0;">
                    <div class="col-sm-6 text-left linkBottom">
                        <div class="input-group input-group-sm">
                            <span class="input-group-addon" id="Span8">Enter OTP</span>
                            <asp:TextBox ID="txtOTP" AutoComplete="off"
                                placeholder="Enter OTP" MaxLength="4" CssClass="form-control" aria-describedby="basic-addon1" runat="server"></asp:TextBox>
                        </div>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7"
                            runat="server" ValidationGroup="otp" Display="Dynamic" ForeColor="Red"
                            ControlToValidate="txtOTP" SetFocusOnError="true" ErrorMessage="Enter OTP"></asp:RequiredFieldValidator>
                    </div>
                    <div class="col-sm-6 text-left linkBottom">
                        <asp:Button ID="btnVerify" ValidationGroup="otp" runat="server" CssClass="btn btn-success" Text="Verify" OnClick="btnVerify_Click" />
                    </div>
                </div>
                <asp:HiddenField ID="hdntblID" runat="server" />
                <div id="dvServiceTime" class="col-sm-12" runat="server" visible="false" style="padding: 0;">
                    <div id="checkinTime" runat="server" visible="false" class="col-sm-6 text-left linkBottom" style="padding: 0;">
                        <div class="col-sm-9 text-left linkBottom">
                            <div class="input-group input-group-sm">
                                <span class="input-group-addon" id="Span3">Check In Time</span>
                                <asp:TextBox ID="txttimeFrom" AutoComplete="off"
                                    placeholder="Enter ServiceTime" CssClass="form-control" aria-describedby="basic-addon1" runat="server"></asp:TextBox>

                            </div>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2"
                                runat="server" ValidationGroup="otp1" Display="Dynamic" ForeColor="Red"
                                ControlToValidate="txttimeFrom" SetFocusOnError="true" ErrorMessage="Enter Service From Time"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="rev" runat="server" ErrorMessage="InvalidTime" Display="Dynamic" ForeColor="Red"
                                ValidationGroup="otp1" ControlToValidate="txttimeFrom"
                                ValidationExpression="^([0-1]?[0-9]|2[0-4]):([0-5][0-9])(:[0-5][0-9])?">
                            </asp:RegularExpressionValidator>
                        </div>
                        <div class="col-sm-3 text-left linkBottom">
                            <asp:DropDownList ID="ddlFromtime" CssClass="form-control dropdown" runat="server">
                                <asp:ListItem Text="AM"></asp:ListItem>
                                <asp:ListItem Text="PM"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div id="checkOutTime" runat="server" visible="false" class="col-sm-6 text-left linkBottom" style="padding: 0;">
                        <div class="col-sm-9 text-left linkBottom">
                            <div class="input-group input-group-sm">
                                <span class="input-group-addon" id="Span4">Check Out Time</span>
                                <asp:TextBox ID="txttimeto" AutoComplete="off"
                                    placeholder="Enter ServiceTime" CssClass="form-control" aria-describedby="basic-addon1" runat="server"></asp:TextBox>

                            </div>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3"
                                runat="server" ValidationGroup="otp1" Display="Dynamic" ForeColor="Red"
                                ControlToValidate="txttimeto" SetFocusOnError="true" ErrorMessage="Enter Service To Time"></asp:RequiredFieldValidator>

                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="InvalidTime" Display="Dynamic" ForeColor="Red"
                                ValidationGroup="otp1" ControlToValidate="txttimeto"
                                ValidationExpression="^([0-1]?[0-9]|2[0-4]):([0-5][0-9])(:[0-5][0-9])?">
                            </asp:RegularExpressionValidator>
                        </div>
                        <div class="col-sm-3 text-left linkBottom">
                            <asp:DropDownList ID="ddlToTime" CssClass="form-control dropdown" runat="server">
                                <asp:ListItem Text="PM"></asp:ListItem>
                                <asp:ListItem Text="AM"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-sm-12 text-left linkBottom" style="clear: both;">
                        <div class="col-sm-3 text-left" style="padding-left:0px;">
                            <div class="input-group input-group-sm">
                                <span class="input-group-addon" id="Span5">Upload Issue Image</span>
                            </div>
                        </div>
                        <div class="col-sm-9 text-left">
                            <asp:FileUpload ID="uploadFile" ForeColor="white" runat="server" AllowMultiple="true" onchange="showimagepreview(this)" ClientIDMode="Static" />Maximum 3 photos
                        </div>
                    </div>
                    <div class="col-sm-12" id="uploaderDiv" style="padding:10px;">
                       
                    </div>
                    <div class="col-sm-6 text-left linkBottom" style="clear: both; margin-bottom: 60px;">
                        <div class="input-group input-group-sm">
                            <span class="input-group-addon" id="Span2">Work Description</span>
                            <asp:TextBox ID="txtwork" AutoComplete="off" MaxLength="300" TextMode="MultiLine" Height="70" Width="500"
                                placeholder="Enter Description" CssClass="form-control" aria-describedby="basic-addon1" runat="server"></asp:TextBox>
                        </div>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4"
                            runat="server" ValidationGroup="otp1" Display="Dynamic" ForeColor="Red"
                            ControlToValidate="txtwork" SetFocusOnError="true" ErrorMessage="Enter work description"></asp:RequiredFieldValidator>
                    </div>
                    <div class="col-sm-12 text-left linkBottom" style="padding-left: 0px; clear: both;">
                        <div class="col-sm-1 text-left linkBottom">
                            <asp:UpdatePanel ID="updButton" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:Button ID="btnSubmit" ValidationGroup="otp1" runat="server" OnClientClick="return doSomeStuff();"
                                       ClientIDMode="Static"  CssClass="btn btn-success servicetime" Text="Submit" OnClick="btnSubmit_Click" />

                                </ContentTemplate>
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="btnSubmit" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                        <div class="col-sm-4 text-left linkBottom">
                            <asp:Button ID="btnReset" runat="server" CssClass="btn btn-success" Text="Reset" OnClick="Button1_Click" />

                            <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-success" Text="Back" OnClick="btnCancel_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="panel-heading">
            <h3 class="panel-title paneltitle">Service Time Log's</h3>
        </div>
        <div class="panel-body table-responsive">

            <asp:Repeater ID="RptTickets" runat="server" OnItemCommand="RptTickets_ItemCommand" OnItemDataBound="RptTickets_ItemDataBound">
                <HeaderTemplate>
                    <table class="table table-hover  table-bordered  table-condensed" data-height="400">
                        <thead style="color: White; background-color: #6b9297;">
                            <tr>
                                <th>TicketID</th>
                                <th>Service Date</th>
                                <th>Check In Time</th>
                                <th>Check Out Time</th>
                                <th>Work Description</th>
                                <th>Time Spent (in mins)</th>
                                <th>Issue Photo</th>
                                <th>Ticket Status</th>
                            </tr>
                        </thead>
                </HeaderTemplate>
                <ItemTemplate>
                    <tbody>
                        <tr>
                            <td>
                                <asp:Label ID="lblService" runat="server" Text='<%#Eval("TicketID") %>'></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblCategory" runat="server" Text='<%#Convert.ToDateTime(Eval("Servicedate")).ToString("dd/MM/yyyy") %>'></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblServiceType" runat="server" Text='<%#Eval("ServiceFrom") %>'></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblPlans" runat="server" Text='<%#Eval("ServiceTo") %>'></asp:Label>
                            </td>
                            <td>
                             <asp:Label ID="Label3" runat="server" Text='<%#Convert.ToString(Eval("Work")).Replace("$","<br/><hr/>") %>'></asp:Label>
                                
                                <asp:LinkButton ID="lnkBtnMore" ClientIDMode="Static" runat="server">....read more</asp:LinkButton>
                                 <asp:HiddenField ID="hdnFullMsg" ClientIDMode="Static" runat="server" Value='<%#Convert.ToString(Eval("Work")).Replace("$","")%>' />
                            </td>
                            <td>
                                <asp:Label ID="Label1" runat="server" Text='<%#Eval("totalMins") %>'></asp:Label>
                            </td>
                            <td>
                                <asp:LinkButton ID="as" CommandName="viewimage" CommandArgument='<%#Eval("tblID") %>' runat="server">View Photo</asp:LinkButton>
                            </td>
                            <td>
                                <asp:Label ID="Label2" runat="server" Text='<%#Eval("TicketStatus") %>'></asp:Label>
                            </td>
                        </tr>
                    </tbody>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>
    <asp:HiddenField ID="hdnCustomer" ClientIDMode="Static" runat="server" />
    <asp:HiddenField ID="hdnEmailid" runat="server" ClientIDMode="Static" />
    <%--<div class="modal fade" id="imagemodal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
  <div class="modal-dialog">
    <div class="modal-content">
      <div class="modal-header">
        <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>
        <h4 class="modal-title" id="myModalLabel">Image preview</h4>
      </div>
      <div class="modal-body">
        <img src="" id="imagepreview" style="width: 400px; height: 400px;" >
      </div>
      <div class="modal-footer">
        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
      </div>
    </div>
  </div>
</div>--%>
</asp:Content>
