<%@ Page Title="Customer Address" Language="C#" MasterPageFile="AdminMaster.Master" AutoEventWireup="true" CodeBehind="CustomerAddress.aspx.cs" Inherits="MakeNMake.ServiceEngineer.CustomerAddress" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        #map-container {
            height: 450px;
            top: 46px;
            right: 2px;
            bottom: 2px;
            border: 1px solid #cccccc;
            position: absolute;
            width: 700px;
            left: 319px;
        }

        #menu {
            position: absolute;
            left: 2px;
            height: 450px;
            top: 46px;
            width: 306px;
            bottom: 2px;
            border: 1px solid #cccccc;
            background-color: #FAFAFA;
            overflow-x: hidden;
            overflow-y: auto;
        }

        .textbox {
            width: 240px;
            margin: 0 10px 5px 0px;
            padding: 5px;
            border: 1px solid #ddd;
            color: #555;
        }

        select {
            color: #555;
            border: 1px solid #ddd;
        }

        a, img {
            outline: none;
            border: none;
            color: #047CC8;
            text-decoration: none;
        }

            a:hover {
                text-decoration: underline;
            }
    </style>
    <script type="text/javascript" src="https://api.mapmyindia.com/v3?fun=load_api&scope=nt-india&lang=none&v=0.8&lic_key=mq7orqvhg9df5l1zctcvh9tfjmps4bj2"></script>
    <script type="text/javascript">
        var wm = null; var alternate_route = null; var poly = []; var advice_direct_route; var direct_route_info;

        var via_points = ""; var alternatives_o;

        function BindMap(lat1, lng1, lat2, lng2, add1, add2) {
            var start = document.getElementById("start");
            start.value = lat1 + "," + lng1;
            var destination = document.getElementById("destination");
            destination.value = lat2 + "," + lng2;
            get_route_result();
        }
        function get_route_result() {
            var map_div = document.getElementById('map-container');/****map HTML container*************/
            map_div.innerHTML = "";
            var start_points = document.getElementById('start').value;

            center = new mireo.wgs.point(start_points.split(",")[0], start_points.split(",")[1])
            wm = new mireo.map(map_div, { center: center, zoom: 16 });
            wm.on("multitap", function (e) { alert(e.wgs.lat + ',' + e.wgs.lng); });
            /***get start points**/
            var destination_points = document.getElementById('destination').value;
            via_points = document.getElementById('via').value;/**get via points**/
            var rtype = document.getElementById('rtype').value;/**get route type**/
            var vtype = document.getElementById('vtype').value;/**get vehicle type**/
            var avoids = document.getElementById('avoids').value;/**get avoids**/
            var advices_o = document.getElementById('advices_o').value;/**get advices option**/
            alternatives_o = document.getElementById('alternatives_o').value;/**get alternatives option**/
            /**put your REST api lisense key here***/
            var route_api_url = "https://api.mapmyindia.com/v3?fun=route&lic_key=9zzhzm4joibbs2w9dplb29x8qjtmaxri&start=" + start_points
                + "&destination=" + destination_points + "&viapoints=" + via_points +
                "&rtype=" + rtype + "&vtype=" + vtype + "&avoids=" + avoids + "&with_advices="
                + advices_o + "&alternatives=" + alternatives_o + "&callback=route_api_result";

            var scriptTag = document.createElement('SCRIPT'); scriptTag.src = route_api_url;
            document.getElementsByTagName('HEAD')[0].appendChild(scriptTag);
            var start_points_array = start_points.split(",");
            var destination_points_array = destination_points.split(",");
            show_markers("start", start_points_array);/*********show start points marker********/
            show_markers("destination", destination_points_array); /*********show destination points marker********/
            if (advice_marker) advice_marker.map(null);/***remove if any existing marker***/
            mapmyindia_fit_into_bound(start_points_array, destination_points_array);
            if (start_info_window) start_info_window.visible(null); /*******remove existing info_windows***/
            document.getElementById('direct_advices').style.display = "inline-block";
            document.getElementById('direct_advices').innerHTML = "<font color='red'>loading..</font>";
            document.getElementById('alternatives_advices').innerHTML = "";
            if (poly['direct']) poly['direct'].map(null);
            if (poly['alternate']) poly['alternate'].map(null);/*********remove direct route polyline*************/
        }


        function route_api_result(data) {
            var alternate_route1_text = ""; var alternate_route2_text = ""; var direct_route = 'Route';

            alternate_route = data.alternatives; document.getElementById("alternate").style.display = "none";
            if (typeof alternate_route[0] != 'undefined') /***get first alternative route***/ {
                var duration1 = alternate_route[0].duration;/**time in seconds*************/
                var hours1 = Math.floor(duration1 / 3600); duration1 %= 3600; var minutes1 = Math.floor(duration1 / 60);
                var total_time1 = (hours1 >= 1 ? hours1 + " hrs " : '') + (minutes1 >= 1 ? minutes1 + " min" : '');
                var length1 = (alternate_route[0].length) / 1000;
                alternate_route1_text = '<td ><div style="padding:5px 5px 5px 15px;color:#000;border-left:1px solid #ddd;cursor:pointer" onclick="document.getElementById(\'direct_advices\').style.display=\'none\';document.getElementById(\'alternatives_advices\').style.display=\'inline-block\';alternative_route(0)"><span style="font-size:13px;padding:2px 0 20px 0;color:#222">Route 2</span><br><span style="font-size:11px;line-height:16px;color:#555">' + total_time1 + '<br>' + length1.toFixed(1) + ' km</div></td>';
                direct_route = 'Route 1';
            }
            if (typeof alternate_route[1] != 'undefined') /***get second alternative route***/ {
                var duration2 = alternate_route[1].duration;/**time in seconds*************/
                var hours2 = Math.floor(duration2 / 3600); duration2 %= 3600; var minutes2 = Math.floor(duration2 / 60);
                var total_time2 = (hours2 >= 1 ? hours2 + " hrs " : '') + (minutes2 >= 1 ? minutes2 + " min" : '');
                var length2 = (alternate_route[1].length) / 1000;
                alternate_route2_text = '<td ><div style="padding:5px 5px 5px 15px;color:#000;border-left:1px solid #ddd;cursor:pointer" onclick="document.getElementById(\'direct_advices\').style.display=\'none\';document.getElementById(\'alternatives_advices\').style.display=\'inline-block\';alternative_route(1)"><span style="font-size:13px;padding:2px 0 20px 0;color:#222">Route 3</span><br><span style="font-size:11px;line-height:16px;color:#555">' + total_time2 + '<br>' + length2.toFixed(1) + ' km</div></td>';

            }
            /***check & display alternative route option*****/
            if (typeof data.trips[0] != 'undefined') {
                var way = data.trips[0]; var way1 = data.trips[1];
                if (via_points == "") {
                    var trips = data.trips;
                    var duration = way.duration;/**time in seconds*************/
                    var hours = Math.floor(duration / 3600); duration %= 3600; var minutes = Math.floor(duration / 60);
                    var total_time = (hours >= 1 ? hours + " hrs " : '') + (minutes >= 1 ? minutes + " min" : '');
                    var length = (way.length) / 1000;
                    var levels = decode_levels(way.lvls);
                    var pts = decode_path(way.pts);
                    var advices = way.advices; /****advice & display **************/
                }
                else {
                    /*******if via points is provided use trip[0] & trip[1] also************/
                    var duration = way.duration + way1.duration;/**time in seconds*************/
                    var hours = Math.floor(duration / 3600); duration %= 3600; var minutes = Math.floor(duration / 60);
                    var total_time = (hours >= 1 ? hours + " hrs " : '') + (minutes >= 1 ? minutes + " min" : '');
                    var length = (way.length + way1.length) / 1000;
                    var levels = decode_levels(way.lvls).concat(decode_levels(way1.lvls));
                    var pts = decode_path(way.pts).concat(decode_path(way1.pts));/****points trip[0] & trip[1] to display **************/
                    var advices = way.advices.concat(way1.advices); /****advice trip[0] & trip[1] to display **************/
                }
            }
            /***********display advices***********/
            direct_route_info = '<table width="100%"><tr><td ><div style="padding:5px;cursor:pointer;background:#f7f7f7" onclick="document.getElementById(\'direct_advices\').style.display=\'inline-block\';document.getElementById(\'alternatives_advices\').style.display=\'none\';poly[\'alternate\'].map(null);"><span style="font-size:13px;padding:2px 0 20px 0;color:#222">'
                + direct_route + '</span><br><span style="font-size:11px;line-height:16px">' + total_time + '<br>' +
                length.toFixed(1) + ' km</span></div></td>' + alternate_route1_text + alternate_route2_text + '</tr></table>';
            document.getElementById('info').innerHTML = direct_route_info;
            advice_direct_route = '<span style="font-size:13px;padding-left:5px">' + direct_route + '</span><table width="100%" align="center">';
            var num_rec = 1; var distance; var go = "";
            advices.forEach(function (advice) {
                var icon = advice.icon_id;
                var meters = advice.meters;
                var distance_meters = meters - distance;
                distance = meters; 1
                var advice_meters = (distance_meters >= 1000 ? (distance_meters / 1000).toFixed(1) + " km " : distance_meters + " mts ")
                var text = advice.text;
                if (meters != 0) { go = "<br>Go " + advice_meters; advice_direct_route += go + '</td></tr>'; }
                var advice_pt = advice.pt;

                advice_direct_route += '<tr onclick="show_route_details(' + advice_pt.lat + ',' + advice_pt.lng + ',\'' + text + '\')" style="cursor:pointer;"><td valign="top" style="padding:5px 0px 5px 0px"><img src="https://api.mapmyindia.com/images/step_' + icon + '.png" width="30px"></td><td style="padding:5px;border-top: 1px solid #e9e9e9;">' + text;
            })
            document.getElementById('direct_advices').innerHTML = advice_direct_route + "</table>";
            /***********display path***********/
            var pathArr = [];
            pts.forEach(function (pt) {
                pathArr.push(new mireo.wgs.point(pt[0], pt[1]));
            })
            draw_polyline("direct", levels, pathArr);/***********draw polyline***/
        }

        function alternative_route(route_no) {
            if (advice_marker) advice_marker.map(null); if (start_info_window) start_info_window.map(null); /***remove advices marker & info windows if exist**/
            var way = alternate_route[route_no]; var way1 = alternate_route[1];
            var levels = decode_levels(way.lvls);
            var pts = decode_path(way.pts);
            var advices = way.advices; /****advice & display **************/
            var advice_alternative_route = '<span style="font-size:13px;padding-left:5px">Route ' + (route_no + 2) + '</span><table width="100%" align="center">';
            var num_rec = 1; var distance; var go = "";
            advices.forEach(function (advice) {
                var icon = advice.icon_id;
                var meters = advice.meters;
                var distance_meters = meters - distance;
                distance = meters; 1
                var advice_meters = (distance_meters >= 1000 ? (distance_meters / 1000).toFixed(1) + " km " : distance_meters + " mts ")
                var text = advice.text;
                if (meters != 0) { go = "<br>Go " + advice_meters; advice_alternative_route += go + '</td></tr>'; }
                var advice_pt = advice.pt;

                advice_alternative_route += '<tr onclick="show_route_details(' + advice_pt.lat + ',' + advice_pt.lng + ',\'' + text + '\')" style="cursor:pointer;"><td valign="top" style="padding:5px 0px 5px 0px"><img src="https://api.mapmyindia.com/images/step_' + icon + '.png" width="30px"></td><td style="padding:5px;border-top: 1px solid #e9e9e9;">' + text;
            })
            document.getElementById('alternatives_advices').innerHTML = advice_alternative_route + "</table>";
            document.getElementById('direct_advices').style.display = 'none';/************hide direct advices******/
            document.getElementById('alternatives_advices').style.display = 'inline-block';/************hide direct advices******/
            /***********display path***********/
            var pathArr = [];
            pts.forEach(function (pt) {
                pathArr.push(new mireo.wgs.point(pt[0], pt[1]));
            })
            if (poly['alternate']) poly['alternate'].map(null); draw_polyline("alternate", levels, pathArr);/***********draw polyline***/
        }

        function draw_polyline(route, levels, pathArr) {	/**draw polyline******************************/
            var polyline_color = 'orange';
            if (route == 'direct') { if (poly[route]) poly[route].map(null); var polyline_color = 'blue'; }
            poly[route] = new mireo.map.polyline({
                map: wm,
                path: new mireo.map.path({
                    points: pathArr,
                    levels: levels,
                    width: 12,
                    color: polyline_color
                })
            });
        }
        var show_marker = [];
        function show_markers(marker_name, points) {
            if (show_marker[marker_name]) show_marker[marker_name].map(null);/***remove if any existing marker***/
            var pos = new mireo.wgs.point(points[0], points[1]);
            if (marker_name == 'start') { var icon = mireo.stock_pins.pin_circle_green(); var title = "Start Point"; } else { var icon = mireo.stock_pins.pin_circle_red(); var title = "Destination Point"; }
            /****marker display, for more about marker, please refer our marker documentation****/
            show_marker[marker_name] = new mireo.map.marker({
                icon: icon,
                handle_input: true,
                draggable: true,
                title: title,
                position: pos,
                z_order: 100,
                map: wm
            });
            show_marker[marker_name].on('changed', function () { var point = show_marker[marker_name].position(); document.getElementById(marker_name).value = point.lat + "," + point.lng; get_route_result() });
        }

        var advice_marker;
        function show_route_details(advice_lat, advice_lng, advice_text) {
            var advice_pos = new mireo.wgs.point(advice_lat, advice_lng);
            /****marker display, for more about marker, please refer our marker documentation****/
            advice_marker = new mireo.map.marker({
                icon: mireo.stock_pins.pin_circle_blue(),
                handle_input: true,
                draggable: false,
                title: advice_text,
                position: advice_pos,
                z_order: 100,
                map: wm
            });
            wm.set_center_and_zoom(advice_pos, 4);/***set map position & zoom***/
            show_info_window(advice_pos, advice_text)
        }
        /*******************************/
        var decode_path = function (encoded) {
            var pts = [];
            var index = 0, len = encoded.length;
            var lat = 0, lng = 0;
            while (index < len) {
                var b, shift = 0, result = 0;
                do {
                    b = encoded.charAt(index++).charCodeAt(0) - 63;
                    result |= (b & 0x1f) << shift;
                    shift += 5;
                } while (b >= 0x20);

                var dlat = ((result & 1) != 0 ? ~(result >> 1) : (result >> 1));
                lat += dlat;
                shift = 0;
                result = 0;
                do {
                    b = encoded.charAt(index++).charCodeAt(0) - 63;
                    result |= (b & 0x1f) << shift;
                    shift += 5;
                } while (b >= 0x20);
                var dlng = ((result & 1) != 0 ? ~(result >> 1) : (result >> 1));
                lng += dlng;
                pts.push([lat / 1E6, lng / 1E6]);
            }
            return pts;
        };

        var decode_levels = function (str) {
            var lvs = new Array(parseInt(str.length / 2));
            var val = 0, i = 0, j = 0, k = 0;
            while (i < str.length) {
                val = 0;
                k = 0;
                for (; i < str.length; i++) {
                    var b = str.charCodeAt(i) - 63;
                    val |= (b & 0x1F) << k;
                    if (!(b & 0x20))
                        break;
                    k += 5;
                } ++i;
                lvs[j++] = val;
            }
            lvs.length = j;
            return lvs;
        };

        Array.max = function (array) { return Math.max.apply(Math, array); };
        Array.min = function (array) { return Math.min.apply(Math, array); };
        function mapmyindia_fit_into_bound(start_points_array, destination_points_array) {
            var latitudeArr = [start_points_array[0], destination_points_array[0]];
            var longitudeArr = [start_points_array[1], destination_points_array[1]];
            var sw = new mireo.wgs.point(Array.max(latitudeArr), Array.max(longitudeArr));/*south-west WGS location object*/
            var ne = new mireo.wgs.point(Array.min(latitudeArr), Array.min(longitudeArr));/*north-east WGS location object*/
            var bounds = new mireo.wgs.bounds(sw, ne);/*This class represents bounds on the Earth sphere, defined by south-west and north-east corners.i.e Creates a new WGS bounds.*/
            wm.fit_bounds(bounds);/*Sets the center map position and level so that all markers is the area of the map that is displayed in the map area*/
        }

        var start_info_window;
        function show_info_window(pos, text) {
            if (start_info_window) start_info_window.visible(null); /*******remove existing info_windows***/
            start_info_window = new mireo.map.info_window({/****info_window display, for more visit detail documentation **/
                position: pos,
                auto_close: false,
                arrow_pos: mireo.map.info_window.arrow_left,
                info_content: '<table style=\"width:250px;padding:10px;font-size: 10px;font-type: bold;\"><tr><td>' + text + '</td></tr></table>',
                pix_offset: new mireo.base.point(20, -15),
                map: wm,
            });
        }
    </script>
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
                                <asp:TextBox ID="txtaddress" CssClass="form-control" runat="server"></asp:TextBox>
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
                        <asp:Button ID="btnSubmit" ValidationGroup="service" OnClick="btnSubmit_Click" runat="server" Text="Submit" CssClass="btn btn-success" />
                        <asp:Button ID="btnReset" OnClick="btnReset_Click" runat="server" Text="Reset" CssClass="btn btn-success" />
                    </div>
                </div>

            </div>
        </div>
        
    </div><div class="panel-body" id="dvmap" visible="false" style="padding:0px;margin-bottom:40px;clear:both;" runat="server">

            <div class="col-sm-12 linkbottom" >

                <div>
                    <div id="menu">
                        <div style="padding: 0 12px 0 17px; font-size: 11px;">
                            <div style="padding: 5px 0; font-size: 16px; color: #222">Route Navigation </div>
                            <div style="padding: 5px 0; font-size: 13px; color: #222">Start points</div>
                            <input type="text" class="textbox" id="start" value="28.610981,77.227434" placeholder="e.g:28.612960,77.229455" autocomplete="off" /><br />
                            <div style="padding: 0px 0 5px 0; font-size: 13px;">Via points (optional) </div>
                            <input type="text" class="textbox" id="via" value="" placeholder="e.g:28.570841,77.325929" autocomplete="off" /><br>
                            <div style="padding: 5px 0 5px 0; font-size: 13px; color: #222">Destination points</div>
                            <div>
                                <input type="text" class="textbox" id="destination" value="28.616679,77.212021" placeholder="e.g:27.157015,77.991600" autocomplete="off" />
                            </div>
                            <div style="padding: 10px 0 15px 0; font-size: 13px; width: 250px;">
                                <div style="float: left">Route type</div>
                                <div style="float: right">
                                    <select id="rtype" style="width: 165px">
                                        <option value="0">Quickest</option>
                                        <option value="1">Shortest</option>
                                    </select>
                                </div>
                            </div>
                            <div style="padding: 10px 0 15px 0; font-size: 13px; width: 250px;">
                                <div style="float: left">Vehicle type</div>
                                <div style="float: right">
                                    <select id="vtype" style="width: 165px">
                                        <option value="1" >Taxi</option>
                                        <option value="0">Passenger</option>
                                    </select>
                                </div>
                            </div>
                            <div style="padding: 10px 0 15px 0; font-size: 13px; width: 250px;">
                                <div style="float: left">Avoids</div>
                                <div style="float: right">
                                    <select id="avoids" style="width: 165px">
                                        <option value="1">Toll roads</option>
                                        <option value="2">Ferries</option>
                                        <option value="4">Unpaved roads</option>
                                        <option value="8">Highways</option>
                                    </select>
                                </div>
                            </div>
                            <div style="padding: 10px 0 15px 0; font-size: 13px; width: 250px;">
                                <div style="float: left">Advices</div>
                                <div style="float: right">
                                    <select id="advices_o" style="width: 165px">
                                        <option value="1" >With advices</option>
                                        <option value="0">Without advices</option>
                                    </select>
                                </div>
                            </div>
                            <div style="padding: 10px 0 15px 0; font-size: 13px; width: 250px;">
                                <div style="float: left">With alternatives route</div>
                                <div style="float: right">
                                    <select id="alternatives_o" style="width: 96px">
                                        <option value="true" >True</option>
                                        <option value="false">False</option>
                                    </select>
                                </div>
                            </div>

                            <div style="margin: 20px 0 5px 0px;">
                                <button type="button" onclick="get_route_result()">Get Route</button>
                            </div>
                            <div id="alternate" style="padding: 2px 5px 2px 5px; border: 1px solid #ccc; border-radius: 10px; width: 254px; display: none">
                                <label>
                                    <input type="checkbox" id="alternatives" onclick="alternative_route()" style="float: left">
                                    <div style="padding: 3px 0px 3px 10px; float: left">Show available alternative route</div>
                                </label>
                            </div>
                        </div>
                        <div id="info" style="border-top: 1px solid #e9e9e9; font-size: 12px; padding-left: 10px; background: #f7f7f7; margin-top: 10px"></div>
                        <div style="padding: 10px; font-size: 11px; overflow: auto" id="direct_advices"></div>
                        <div style="padding: 10px; font-size: 11px; overflow: auto; display: none" id="alternatives_advices"></div>
                    </div>
                    <div id="result"></div>
                    <div id="map-container"></div>
                </div>
            </div>
        </div>
</asp:Content>
