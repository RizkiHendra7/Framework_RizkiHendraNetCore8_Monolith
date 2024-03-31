var dataModel = {};
var allDataMenu = [];  
var firstRenderSelect2 = true;
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
            __RequestVerificationToken: $('#frmDetail input[Name=__RequestVerificationToken]').val()
        },
        datatype: "json",
        success: function (result) {
            dataModel = result;
            mappingDataIntoUI(result)
        },
        error: function (xhr, error, thrown) {
            globalShowAlertError(xhr.responseText);
        }
    });
}
 
function renderParentMenu() { 
    $("#idParentMenu").select2({
        ajax: {
            url: '/MMenus/GetAllData', // URL to fetch data from
            dataType: 'json',
            delay: 250, // Delay in milliseconds before the request is sent 
            minimumInputLength: 3, 
            headers: {
                '__RequestVerificationToken': $('#frmDetail input[Name=__RequestVerificationToken]').val()
            },
            data: function (params) {
                var query = {
                    search: params.term  
                };
                return query;
            },
            placeholder: 'Search for a Menu...',
            processResults: function (data) {
                return {
                    results: data.map(function (item) {  

                        if (firstRenderSelect2 && dataModel.idParent == item.id) {
                            //Ini buat akal2 an agar bisa getdata pas render awal dan wajib dipanggil setelah getdatadetail
                            //agar tau id existing apa
                            $('#idParentMenu').append('<option value="' + item.id + '">' + item.txtMenuName + '</option>'); 
                            firstRenderSelect2 = false;
                        }

                        return {
                            id: item.id,
                            text: item.txtMenuName
                        };
                    }) 
                }; 
            },
            cache: true
        }
    });
    $('#idParentMenu').on('select2:open', function () {
        $(this).select2('open');
    });
     
    $('#idParentMenu').trigger('select2:open');
    $('#idParentMenu').select2('close');
     
    
}

 
function saveData() {
    var wording = "Save";
    var dt = globalConvertFormSerializeToJson($("#frmDetail").serialize());

    globalGetConfirmationSwal(wording + " this data?", function (result) {
        if (result.isConfirmed) {
            if (validateData()) {
                $.ajax({
                    type: "POST",
                    url: "/MMenus/Create",
                    data: {
                        ParamDt: dt,
                        __RequestVerificationToken: $('#frmDetail input[Name=__RequestVerificationToken]').val()
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

function updateData() {
    var wording = "Update";
    globalGetConfirmationSwal(wording + " this data?", function (result) {
        if (result.isConfirmed) {
            if (validateData()) {
                $.ajax({
                    type: "POST",
                    url: "/MMenus/Create",
                    data: {
                        ParamDt: $("#frmDetail").serialize(),
                        __RequestVerificationToken: $('#frmDetail input[Name=__RequestVerificationToken]').val()
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
    $("#txtMenuName").val(paramData.txtMenuName);
    $("#txtMenuDisplay").val(paramData.txtMenuDisplay);
    $("#txtMenuIcon").val(paramData.txtMenuIcon);
    $('#idParentMenu').val(paramData.idParent).trigger('change');
    document.getElementById("bitActive_Value").checked = paramData.bitActive;
}




//validation 
function validateData() {
    var isValid = true;
    if (globalIsNullOrEmptyString($("#txtMenuName ").val())) {
        globalShowAlertInfo("Menu Name is mandatory! ");
        isValid = false;
    } else if (globalIsNullOrEmptyString($("#txtMenuDisplay").val())) {
        globalShowAlertInfo("Menu Display is mandatory!");
        isValid = false;
    } else if (globalIsNullOrEmptyString($("#TxtUrl").val())) {
        globalShowAlertInfo("Path url is mandatory!");
        isValid = false;
    } 

    return isValid;
}

//EVENT
$('#btnSave').click(function (e) {
    if (globalIsNullOrEmptyString(dataModel.id)) {
        saveData(); 
    } else {
        updateData();
    }
}); 
 