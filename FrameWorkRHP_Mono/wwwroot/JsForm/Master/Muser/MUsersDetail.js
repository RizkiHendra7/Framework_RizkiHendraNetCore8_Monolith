var data = {};
    $(document).ready(function () { 
        getDataDetail();
    }); 


//AJAX

function getDataDetail() { 
    $.ajax({
        type: "GET",
        url: "/MUsers/GetById",
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

function saveData() {
    var wording = "Save";
    globalGetConfirmationSwal(wording + " this data?", function (result) {
        if (result.isConfirmed) {
            if (validateData(data)) {
                $.ajax({
                    type: "POST",
                    url: "/MUsers/Create",
                    data: {
                        ParamMUserModel: $('#frmDetail').serialize(),
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
    $("#Txtusername").val(paramData.txtusername);
    $("#Txtfullname").val(paramData.txtfullname);
    $("#Txtpassword").val(paramData.txtpassword); 
    document.getElementById("Bitactive_Value").checked = paramData.bitactive;
}


//validation 
function validateData() {
    var isValid = true; 
    if (globalIsNullOrEmptyString($("#Txtusername").val())) {
        globalShowAlertInfo("User name is mandatory! ");
        isValid = false;
    } else if (globalIsNullOrEmptyString($("#Txtfullname").val())) {
        globalShowAlertInfo("Full name is mandatory!");
        isValid = false;
    } else if (globalIsNullOrEmptyString($("#Txtpassword").val())) {
        globalShowAlertInfo("Password is mandatory!");
        isValid = false;
    }  

    return isValid;
}

//EVENT
$('#btnSave').click(function () {
    saveData();
});
