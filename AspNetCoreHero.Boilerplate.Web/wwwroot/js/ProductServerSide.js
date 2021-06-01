$(document).ready(function () {
    $("#reload").click(() => {
        $("#productTable1").DataTable().ajax.reload();

    });

    var table = $("#productTable1").DataTable({
        "processing": true,
        "serverSide": true,
        "filter": true,
        "ajax": {
            "url": "/catalog/brandMD/GetProduct",
            "type": "POST",
            "datatype": "json",
            data: function (data) {
                var tableBrand = $("#brandTable").DataTable();

                if (tableBrand && tableBrand.row('.selected').data()) {
                    console.log(tableBrand.row('.selected').data()[0]);

                    data.brandId = tableBrand.row('.selected').data()[0];
                }
            },
        },
        "columnDefs": [{
            "targets": [0],
            "visible": false,
            "searchable": false
        }],
        "columns": [
            { "data": "name", "name": "Name", "autoWidth": true },
            { "data": "barcode", "name": "Barcode", "autoWidth": true },
            { "data": "description", "name": "Description", "autoWidth": true },
            { "data": "rate", "name": "Rate", "autoWidth": true }

        ]
    });


});