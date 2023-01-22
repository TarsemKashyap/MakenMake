$(document).ready(function () {
    loadDt();


    $("#Button1").click(function (event) {
        event.preventDefault();
        var nr = $('#example').DataTable();
        if ($.fn.dataTable.isDataTable('#example')) {
            var clientName = document.getElementById('ContentPlaceHolder1_txtSearchclient').value;
            nr.search(clientName);
            nr.draw();
        }
    });
    $(window).keydown(function (event) {
        if (event.keyCode == 13) {
            event.preventDefault();
            return false;
        }
    });
});
function loadDt() {
    $.ajax({
        type: "POST",
        url: "Tickethistory.aspx/BindData",
        data: "{}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            var dataset = data.d;
            var ticketWithDetail = dataset.Tickets;
            var datafull = "";
            $.each(ticketWithDetail, function (index, data) {
                datafull += '{"ticketid":"' + "<div id='rd12' onclick='rd(" + data.ticketID + ")'><a style='cursor:pointer;'>" + data.ticketID + "</a></div>" + '" ,"name":"' + data.customerName + '","status":"' + data.status + '","creation":"' + data.created + '","closure":"' + data.closure + '"},';
            });
            datafull = datafull.substring(0, datafull.length - 1);
            // datafull = '{' + 'data:' + datafull + '}';

            var strData = jQuery.parseJSON('[' + datafull + ']');

            var dtable = $('#example').dataTable({
                "bProcessing": true,
                "data": strData,
                "bPaging": true,
                "pagingType": "full_numbers",
                "columns": [
                       {
                           "className": 'details-control',
                           "orderable": false,
                           "data": null,
                           "defaultContent": ''
                       },
                    { "data": "ticketid" },
                    { "data": "name" },
                    { "data": "status" },
                    { "data": "creation" },
                    { "data": "closure" }
                ],
                "order": [[1, 'asc']]
            });
            $('#example tbody').on('click', 'td.details-control', function () {

                var tr = $(this).closest('tr');


                //-----------------------------------------------------------------------------//

                var otherTr = tr.siblings();
                otherTr.each(function (index, item) {


                    var rd1 = item.firstChild;

                    var jjj = $(rd1).closest('tr');

                    if (jjj.hasClass('shown')) {
                        jjj.next().remove();
                        jjj.removeClass('shown');
                    }


                });

                //--------------------------------------------------------------------------------//

                if (tr.hasClass('shown')) {
                    tr.next().remove();
                    tr.removeClass('shown');
                }

                else {
                    var tdTick = tr.children()[1];
                    $.ajax({
                        type: "POST",
                        url: "Tickethistory.aspx/BindTicketData",
                        data: JSON.stringify({ ticketID: tdTick.textContent }),
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (data) {
                            var dataset = data.d;
                            var tickedetail = "";
                            $.each(dataset, function (i, detail) {
                                var remarks = "";
                                var hrefdetail = "    <a href='#' onclick='readFull(this)' >read more ....</a>"
                                if (detail.description.length > 30) {
                                    remarks = detail.description.substring(0, 30);
                                }
                                else {
                                    remarks = detail.description;
                                    hrefdetail = "";
                                }
                                tickedetail += "<tr><td>" + detail.serviceName + "</td><td vdata='" + detail.description + "'>"
                                    + remarks + hrefdetail+

                                    "</td><td>"
                                    + detail.plan + "</td><td>" + "<div id='rd122' onclick='rd2(" + detail.servedByID
                                    + "," + tdTick.textContent + ")'><a style='cursor:pointer;'>" + detail.servedBy + "</a></div>" + "</td></tr>";

                            });
                            tr.after(format(tickedetail));
                        }
                    });


                    tr.addClass('shown');
                }
            });
        }
    });
}

function readFull(ele)
{
    alert(ele.parentElement.attributes["vdata"].value);
}

function format(d) {

    return '<tr><td colspan="6"><table class="table table-hover  table-bordered  table-condensed">' +
        '<thead>' +
            '<tr><th>Service</th>' +
            '<th>Description</th>' +
            '<th>Plan</th>' +
            '<th>Served By</th></tr>' +
        '</thead>' + '<tbody>' + d + '</tbody>'
        +
    '</table></td></tr>';
}

function rd(ticketId, f) {

    if (ticketId != undefined || ticketId != null) {
        window.location = 'CheckTicketByID.aspx?TicketID=' + ticketId;
    }

}

 function rd2(e, f) {
     if (e != 0) {
         $.ajax({
             type: "POST",
             url: "Tickethistory.aspx/BindWorkHistory",
             data: JSON.stringify({ UserID: e,TicketID:f }),
             contentType: "application/json; charset=utf-8",
             dataType: "json",
             success: function (data) {
                 var dataset = data;
                 var detail = dataset;
                 var profile = "";
                 BootstrapDialog.show({
                     title: 'Work History',
                     message: GetDetails(detail),
                     cssClass: 'ticketdialog',
                     buttons: [{
                         label: 'Close',
                         action: function (dialogItself) {
                             dialogItself.close();
                         }
                     }]
                 });
             }
         });
     }
     else {
         BootstrapDialog.show({
             title: 'Info',


             message: 'Either Ticket is Escalated or Not Assigned To Someone',

             buttons: [{
                 label: 'Close',
                 action: function (dialogItself) {
                     dialogItself.close();
                 }
             }]
         });
     }
 }

 function GetDetails(detail) {

     var table = '<div ><table  class="table table-hover  table-bordered  table-condensed" >';
     table += "<tr style='color: White; background-color: #6b9297;'><th>TicketID</th><th>Opendate</th><th>ServiceStart</th><th>ServiceEnd</th><th>WorkDEscCheckIn</th><th>WorkDescCheckOut</th></tr>";
     var count = 0;
     $.each(detail, function (ind, ele) {
         for (var i = 0; i < ele.length; i++) {
             table += "<tr><td>" + ele[i].TicketID + "</td><td>" + ele[i].Opendate + "</td><td>" + ele[i].ServiceStart + "</td><td>" + ele[i].ServiceEnd
                 + "</td><td>" + ele[i].WorkDEscCheckIn + "</td><td>" + ele[i].WorkDescCheckOut + "</td></tr>";
             count++;
         }
     });
     table += "</table></div>";
     return table;
 }

 function checkPagging() {
     loadDt();
     alert("No Data Found");
 }