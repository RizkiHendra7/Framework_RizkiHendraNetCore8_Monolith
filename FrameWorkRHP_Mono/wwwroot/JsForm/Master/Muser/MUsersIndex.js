var DtMain; 
$(document).ready(function () { 
        renderDataTable();
    });


//Data Table
function renderDataTable() { 
    DtMain =  $('#dataTableMain').DataTable({   
        "ordering": false, 
        "autoWidth": false,
        "responsive": true,
        "serverSide": true,
        "ajax": {
            url:  '/MUsers/GetDataIndex',
            type: "POST",
            dataSrc: function (json) {
                if (json.errorMessage == null) {
                    return json.data;
                } else {
                    globalShowAlertError(json.errorMessage); 
                    return json.data = [];
                }

            },
            error: function (xhr, error, thrown) { 
                globalShowAlertError(xhr.responseText); 
            }
        },
        "columns": [
            { "data": "txtusername" },
            { "data": "txtfullname" }, 
            {
                "data": "bitactive",
                "className": "dt-center", 
                render: function (data, type, full) { 
                    return data ? '<input type="checkbox" checked disabled>' : '<input type="checkbox" disabled>';
                }
            }, 
            {
                "data": "intuserid",
                "className": "dt-center", 
                render: function (data, type, full) { 
                    return '<a href="/MUsers/Details?id=' + encodeURIComponent(data) + '" class="btn btn-success center" style="font-size:12px;"> <i class="fa fa-edit" style="font-size:12px;"></i>  VIEW </a>';
                }
            }
        ] 
    });
     
} 

//Mapping Data Table

function mappingData(paramData) {
    DtMain.clear();

    for (var i = 0; i < paramData.length; i++) {
        DtMain.row.add(paramData[i]);
    }

    DtMain.draw(true);
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
