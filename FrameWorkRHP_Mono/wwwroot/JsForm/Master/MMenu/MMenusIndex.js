
$(document).ready(function () {
    renderDataTable();
});
function renderDataTable() {
    $('#dataTableMain').DataTable({
        "searching": false,
        "ordering": false,
        "info": true,
        "autoWidth": false,
        "responsive": true,
        "serverSide": true,
        "ajax": {
            url: '/MMenus/GetDataIndex',
            type: "POST",
            dataSrc: function (json) {
                if (json.errorMessage == null) {
                    return json.data;
                } else {
                    globalShowAlertError(json.errorMessage);
                    return json.data = [];
                }

            }
        },
        "columns": [
            { "data": "txtmenuname" },
            { "data": "txtParentMenu" },
            { "data": "txtmenudisplay" },
            { "data": "txturl" }, 
            {
                "data": "bitactive",
                "className": "dt-center",
                render: function (data, type, full) {
                    return data ? '<input type="checkbox" checked disabled>' : '<input type="checkbox" disabled>';
                }
            }, 
            {
                "data": "intmenuid",
                "className": "dt-center", 
                render: function (data, type, full) {
                    return '<a href="/MMenus/Details?id=' + encodeURIComponent(full.intmenuid) + '" class="btn btn-success center" style="font-size:12px;"> <i class="fa fa-edit" style="font-size:12px;"></i>  VIEW </a>';
                }
            }
        ]
    });
} 