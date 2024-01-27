 
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
                url:  '/MUsers/GetDataIndex',
                type: "POST",
                dataSrc: function (json) {
                    if (json.errorMessage == null) {
                        return json.data;
                    } else {
                        //swal("Validation Message", json.errorMessage, "error");
                        return json.data = [];
                    }

                }
            },
            "columns": [
                { "data": "txtusername" },
                { "data": "txtfullname" },
                { "data": "bitactive" }, 
                {
                    render: function (data, type, full) { 
                        return '<a href="/MUsers/Detail?id=' + encodeURIComponent(full.intuserid) + '" class="btn btn-success center" style="font-size:12px;"> <i class="fa fa-edit" style="font-size:12px;"></i>  VIEW </a>';
                    }
                }
            ]
        });
        }

//})(jQuery)