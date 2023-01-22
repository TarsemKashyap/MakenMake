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


    $("#Button2").click(function () {
        $('#example').DataTable({
            retrieve: true,
            paging: false
        });
    });

});

function checkPagging() {
    loadDt();
    alert("No Data Found");
}

function loadDt() {
    $.ajax({
        type: "POST",
        url: "ClientHistory.aspx/BindData",
        data: "{}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            var dataset = data.d;
            var ClientWithDetail = dataset.ClientId;
            var datafull = "";
            $.each(ClientWithDetail, function (index, data) {

                // for same page
                datafull += '{"ClientId":' + data.UserID + ',"ClientName":"' + data.Name + '","Profile":"' + "<div id='rd12' onclick='rd(" + data.UserID + ")'><a style='cursor:pointer;'>Profile</a></div>" + '","BillInformation":"' + "<div id='rd123' onclick='rd2_billInfo(" + data.UserID + ")'><a style='cursor:pointer;'>Bill Information</a></div>" + '"},';

                // for next page 
                // datafull += '{"ClientId":' + data.UserID + ',"ClientName":"' + data.Name + '","Profile":"' + "<div id='rd12' onclick='rd(" + data.UserID + ")'><a>Profile</a></div>" + '","BillInformation":"' + "<div id='rd12'><a href='ClientBillInformation.aspx?clientId=" + data.UserID + "'>Bill Information</a></div>" + '"},';
            });
            datafull = datafull.substring(0, datafull.length - 1);

            var strData = jQuery.parseJSON('[' + datafull + ']');

            var dtable = $('#example').dataTable({
                "bProcessing": true,
                "data": strData,
                "bPaging": true,
                "pagingType": "full_numbers",
                "bLengthChange": false,   // this line is used to hide the show entries at top of table
                "columns": [
                       {
                           "className": 'details-control',
                           "orderable": false,
                           "data": null,
                           "defaultContent": ''
                       },
                    { "data": "ClientId", "bSearchable": true },
                    { "data": "ClientName", "bSearchable": true },
                    { "data": "Profile", "bSearchable": true },

                    { "data": "BillInformation", "bSearchable": false }
                ],
                "order": [[1, 'asc']]
            });
            $('#example tbody').on('click', 'td.details-control', function () {
                var tr = $(this).closest('tr');
                //---------------------------------------------------------------------------
                var otherTr = tr.siblings();
                otherTr.each(function (index, item) {
                    var rd1 = item.firstChild;
                    var jjj = $(rd1).closest('tr');
                    if (jjj.hasClass('shown')) {
                        jjj.next().remove();
                        jjj.removeClass('shown');
                    }
                });
                //----------------------------------------------------------------------------
                if (tr.hasClass('shown')) {
                    tr.next().remove();
                    tr.removeClass('shown');
                }
                else {
                    var tdTick = tr.children()[1];
                    $.ajax({
                        type: "POST",
                        url: "ClientHistory.aspx/BindTicketData",
                        data: JSON.stringify({ UserID: tdTick.textContent }),
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (data) {
                            var dataset = data.d;
                            var tickedetail = "";
                            $.each(dataset, function (i, detail) {
                                tickedetail += "<tr><td>" + detail.ServiceName + "</td><td>" + detail.ServiceCategory + "</td><td>" + detail.PlanType + "</td><td>" + detail.IssueDate + "</td><td>" +
                                    detail.ExpirationDate + "</td><td>" + detail.Calls + "</td><td>" + detail.Amount + "</td></tr>";
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

function searchClient() {
    var nr = $('#example').DataTable();
    if ($.fn.dataTable.isDataTable('#example')) {
        var clientName = document.getElementById('ContentPlaceHolder1_txtSearchclient').value;
        nr.search(clientName);
        nr.draw();
    }
}

function format(d) {
    return '<tr><td colspan="5"><table class="table table-hover  table-bordered  table-condensed">' +
        '<thead>' +
            '<tr><th>Service Name</th>' +
            '<th>Service Category</th>' +
            '<th>Plan Type</th>' +
            '<th>Issue Date</th>' +
            '<th>Expiration Date</th>' +
            '<th>No. of Calls</th>' +
            '<th>Amount</th></tr>' +
            '</thead>' + '<tbody>' + d + '</tbody>' +
      '</table></td></tr>';
}



function format_bill(d) {
    return '<table class="table table-hover  table-bordered  table-condensed">' +
        '<thead>' +
            '<tr><th>Sr.No.</th>' +
            '<th>Invoice No</th>' +
            '<th>Invoice Date</th>' +
            '<th>Amount</th>' +
            '</tr>' +
        '</thead>' + '<tbody>' + d + '</tbody>' +
      '</table>';
}







function rd(e,f) {

    $.ajax({
        type: "POST",
        url: "ClientHistory.aspx/BindProfileData",
        data: JSON.stringify({ UserID: e }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            var dataset = data.d[0];
            var detail=dataset;
            var profile = "";

         
            BootstrapDialog.show({
                title: 'Profile',


                message: 'Reference : ' + detail.Reference + '<br> Status :' + detail.Status + '<br> Source  :' + detail.Source + '<br> Service Start Date  :' + detail.ServiceDate,
             
                buttons: [ {
                    label: 'Close',
                    action: function (dialogItself) {
                        dialogItself.close();
                    }
                }]
            });
        }
    });
  
}




function rd2_billInfo(e) {

    $.ajax({
        type: "POST",
        url: "ClientHistory.aspx/BindBillData",
        data: JSON.stringify({ UserID: e }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            var dataset = data.d;
            var detail = dataset;
            var profile = "";
            var j = 1;


            $.each(dataset, function (i, detail) {
                profile += "<tr><td>" + j + "</td><td><a style=' cursor: pointer;' onclick='return OpenInvoice(" + detail.UserID + ")'>" + detail.InvoiceNumber + "</a></td><td>" + detail.InvoiceDate + "</td><td>" + detail.Amount + "</td></tr>";

                j = j + 1;
            });


            var billInfo = format_bill(profile);



            BootstrapDialog.show({
                title: 'Bill Information',


                message:billInfo,

                //message: 'S.R.No : ' + detail.S.R.No + ' <br/>InvoiceNumber :' + detail.InvoiceNumber + ' InvoiceDate  :' + detail.InvoiceDate + ' Amount  :' + detail.Amount,

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