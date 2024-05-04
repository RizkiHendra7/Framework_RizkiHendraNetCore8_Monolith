var DtMain;
$(document).ready(function () {
    renderDataTable();
});
//Data Table
function renderDataTable() {
   DtMain= $('#dataTableMain').DataTable({
       "info": true,
       "ordering": false,
       "autoWidth": false,
       "responsive": true,
       "serverSide": true,
        "ajax": {
            url: '/MRoleXMenus/GetDataIndex',
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
            { "data": "txtMenuName" },
            { "data": "txtRoleName" }, 
            {
                "data": "bitActive",
                "className": "dt-center",
                render: function (data, type, full) {
                    return data ? '<input type="checkbox" checked disabled>' : '<input type="checkbox" disabled>';
                }
            }, 
            {
                "data": "intMRolexMenuId",
                "className": "dt-center", 
                render: function (data, type, full) {
                    return '<a href="/MRoleXMenus/Details?id=' + encodeURIComponent(full.intMRolexMenuId) + '" class="btn btn-success center" style="font-size:12px;"> <i class="fa fa-edit" style="font-size:12px;"></i>  VIEW </a>';
                }
            }
        ]
    });
} 

//Function
function searchDataTableMain() {
    var search = $("#inptSearchTableMain").val();
    DtMain.search(search).draw();
}

//Event
$('#btnSearchTableMain').click(function () {
    searchDataTableMain();
});
