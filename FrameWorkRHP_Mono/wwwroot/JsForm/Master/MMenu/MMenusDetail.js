var data = {};
var allDataMenu = [];
$(document).ready(function () {
    getDataDetail(); 
    renderParentMenu();
});


//AJAX

function getDataDetail() {
    $.ajax({
        type: "GET",
        url: "/MMenus/GetById",
        data: {
            ParamUserId: globalGetParamaterFromCurrentUrl("id"),
            __RequestVerificationToken: $('#frmDetail input[name=__RequestVerificationToken]').val()
        },
        datatype: "json",
        success: function (result) {
            data = result;
            mappingDataIntoUI(result)
        },
        error: function (xhr, error, thrown) {
            globalShowAlertError(xhr.responseText);
        }
    });
}
 
function renderParentMenu() {
    //Initialize Select2 Elements
    $('.select2bs4').select2({
        theme: 'bootstrap4'
    })


    $("#idParentMenu").select2({
        ajax: {
            url: '/MMenus/GetAllData', // URL to fetch data from
            dataType: 'json',
            delay: 250, // Delay in milliseconds before the request is sent 
            headers: {
                '__RequestVerificationToken': $('#frmDetail input[name=__RequestVerificationToken]').val()
            },
            data: function (params) {
                var query = {
                    search: params.term  
                };
                return query;
            },
            minimumInputLength: 3, 
            placeholder: 'Search for a menu...',
            processResults: function (data) {
                return {
                    results: data.map(function (item) {
                        return {
                            id: item.id,
                            text: item.txtmenuname
                        };
                    })
                };
            },
            cache: true
        }
    });
}

 
function saveData() {
    var wording = "Save";
    globalGetConfirmationSwal(wording + " this data?", function (result) {
        if (result.isConfirmed) {
            if (validateData(data)) {
                $.ajax({
                    type: "POST",
                    url: "/MMenus/Create",
                    data: {
                        ParamMUserModel: data,
                        __RequestVerificationToken: $('#frmDetail input[name=__RequestVerificationToken]').val()
                    },
                    datatype: "json",
                    success: function (result) {
                        data = result.data;
                        mappingDataIntoUI(result.data)
                    },
                    error: function (xhr, error, thrown) {
                        globalShowAlertError(xhr.responseText);
                    }
                });
            }
        }
    });


}


//Mapping
function mappingDataIntoUI(paramData) {
    $("#Txtmenuname").val(paramData.txtmenuname);
    $("#Txtmenudisplay").val(paramData.txtmenudisplay);
    $("#Txtmenuicon").val(paramData.txtmenuicon);
    document.getElementById("Bitactive_Value").checked = paramData.bitactive;
}




//validation 
function validateData() {
    var isValid = true;
    if (globalIsNullOrEmptyString($("#Txtmenuname").val())) {
        globalShowAlertInfo("Menu name is mandatory! ");
        isValid = false;
    } else if (globalIsNullOrEmptyString($("#Txtmenudisplay").val())) {
        globalShowAlertInfo("Menu Display is mandatory!");
        isValid = false;
    } 

    return isValid;
}

//EVENT
$('#btnSave').click(function () {
    saveData();
});
