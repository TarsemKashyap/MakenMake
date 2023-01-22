<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/AdminMaster.Master" AutoEventWireup="true" CodeBehind="CustomerAddress2.aspx.cs" Inherits="MakeNMake.Pages.CustomerAddress2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.9.1/jquery.min.js"></script>
  
    <script src="https://maps.googleapis.com/maps/api/js"></script>
   
    <style>
      html, body {
        height: 100%;
        margin: 0;
        padding: 0;
      }
      #map {
        height: 100%;
      }
#floating-panel {
  position: absolute;
  top: 10px;
  left: 25%;
  z-index: 5;
  background-color: #fff;
  padding: 5px;
  border: 1px solid #999;
  text-align: center;
  font-family: 'Roboto','sans-serif';
  line-height: 30px;
  padding-left: 10px;
}

#right-panel {
  font-family: 'Roboto','sans-serif';
  line-height: 30px;
  padding-left: 10px;
  height:500px;
  overflow:scroll;
}

#right-panel select, #right-panel input {
  font-size: 15px;
}

#right-panel select {
  width: 100%;
}

#right-panel i {
  font-size: 12px;
}

      #right-panel {
        height: 100%;
        float: right;
        width: 390px;
       height:500px;
  overflow:scroll;
      }

      #map {
        margin-right: 400px;
       height: 500px;
        width:500px;
      }

      #floating-panel {
        background: #fff;
        padding: 5px;
        font-size: 14px;
        font-family: Arial;
        border: 1px solid #ccc;
        box-shadow: 0 2px 2px rgba(33, 33, 33, 0.4);
        display: none;
      }

      @media print {
        #map {
          height: 500px;
          margin: 0;
        }

        #right-panel {
          float: none;
          width: auto;
        }
      }
    </style>
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="panel panel-success">
        <div class="panel-heading">
            <h3 class="panel-title paneltitle">Customer Address Navigation</h3>
        </div>
        <div class="panel-body" style="padding-left:0px;">
            <div class="col-sm-12" style="padding-left:0px;">
                <div id="dvticket" runat="server" class="col-sm-6 text-left linkBottom">
                    <div class="input-group input-group-sm">
                        <span class="input-group-addon" id="Span1">Select TicketID</span>
                        <asp:DropDownList ID="ddlticket" AutoPostBack="true" OnSelectedIndexChanged="ddlticket_SelectedIndexChanged" CssClass="form-control dropdown" runat="server">
                        </asp:DropDownList>
                    </div>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="service" runat="server" ErrorMessage="Select Ticket ID"
                        ControlToValidate="ddlticket"
                        InitialValue="0" Display="Dynamic" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                </div>

                <div class="col-sm-12 linkbottom" style="padding-left:0px;" id="clientinfo" runat="server" visible="false">
                    <div class="col-sm-12 text-left linkBottom">
                        <div class="input-group input-group-sm">
                            <span class="input-group-addon" id="Span2">Engineer's Current Address</span>
                            <asp:TextBox ID="txtaddresss" MaxLength="490" placeholder="e.g. 15 Parliament Street, Connaught Place, New Delhi, India" CssClass="form-control input" runat="server"></asp:TextBox>
                        </div>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7"
                            runat="server" ValidationGroup="service" Display="Dynamic" ForeColor="Red"
                            ControlToValidate="txtaddresss" SetFocusOnError="true"
                            ErrorMessage="Enter Address"></asp:RequiredFieldValidator>
                    </div>
                    <div class="col-sm-12" style="padding-left:0px;">
                        <div class="col-sm-5 text-left linkBottom">
                            <div class="input-group input-group-sm">
                                <span class="input-group-addon" id="Span3">Name</span>
                                <asp:TextBox ID="txtname" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-sm-7 text-left linkBottom">
                            <div class="input-group input-group-sm">
                                <span class="input-group-addon" id="Span5">Address</span>
                                <asp:TextBox ID="txtaddress2" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-sm-5 text-left linkBottom">
                            <div class="input-group input-group-sm">
                                <span class="input-group-addon" id="Span4">Mobile Number</span>
                                <asp:TextBox ID="txtmobileNumber" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                        </div>


                    </div>
                    <div class="col-sm-12 text-left linkBottom">
                     
                        <a id="btnSubmit" class="btn btn-success" href="#" > Submit</a>
                      <%--  <asp:Button ID="btnSubmit" ValidationGroup="service"   runat="server" Text="Submit" CssClass="btn btn-success" />--%>
                        <asp:Button ID="btnReset" OnClick="btnReset_Click" runat="server" Text="Reset" CssClass="btn btn-success" />
                    </div>
                </div>

            </div>
        </div>
        
    </div>
  
    <div id="right-panel"></div>
    <div id="map"></div>
    <script>
        $(document).ready(function () {



            $("#btnSubmit").click(function () {

                if (document.getElementById('ContentPlaceHolder1_txtaddresss').value == '') {
                    alert("Please Enter Your current Address");
                }
                else {
                    var directionsDisplay = new google.maps.DirectionsRenderer;
                    var directionsService = new google.maps.DirectionsService;
                    var map = new google.maps.Map(document.getElementById('map'), {
                        zoom: 7,
                        center: { lat: 41.85, lng: -87.65 }
                    });
                    directionsDisplay.setMap(map);
                    directionsDisplay.setPanel(document.getElementById('right-panel'));

                    //var control = document.getElementById('floating-panel');
                    //control.style.display = 'block';
                    //map.controls[google.maps.ControlPosition.TOP_CENTER].push(control);


                    calculateAndDisplayRoute(directionsService, directionsDisplay);

                }
                //document.getElementById('txtaddress2').addEventListener('change', onChangeHandler);
            });

        });
       

        function calculateAndDisplayRoute(directionsService, directionsDisplay) {
            var start = document.getElementById('ContentPlaceHolder1_txtaddresss').value;
            var end = document.getElementById('ContentPlaceHolder1_txtaddress2').value;
            directionsService.route({
                origin: start,
                destination: end,
                travelMode: google.maps.TravelMode.DRIVING
            }, function (response, status) {
                if (status === google.maps.DirectionsStatus.OK) {
                    directionsDisplay.setDirections(response);
                } else {
                    window.alert('Directions request failed due to ' + status);
                }
            });
        }

    </script>
   
</asp:Content>
