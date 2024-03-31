// === BLOCK UI ===
$(document).ajaxStart(function () {

    $.blockUI({ message: '<h1><img src="~/../../plugins/blockui/giphy.gif" /> Just a moment...</h1>' });
});


$(document).ajaxStop($.unblockUI); 
// === END BLOCK UI ===


 
//  === TOAST === 
// UNTUK ALERT MESSAGE
var Toast = Swal.mixin({
    toast: true,
    position: 'top-end',
    showConfirmButton: false,
    timer: 10000
});


function globalShowAlertError(paramTitle) {
    Toast.fire({
        icon: 'error',
        title: paramTitle
    })
}
function globalShowAlertSuccess(paramTitle) {
    Toast.fire({
        icon: 'success',
        title: paramTitle
    })
}
function globalShowAlertWarning(paramTitle) {
    Toast.fire({
        icon: 'warning',
        title: paramTitle
    })
}
function globalShowAlertInfo(paramTitle) {
    Toast.fire({
        icon: 'info',
        title: paramTitle
    })
}
//  === END TOAST ===

// === SWAL ===
function globalGetConfirmationSwal(TxtInformation, callback) {

    Swal.fire({
        title: TxtInformation,
        //showDenyButton: true,
        showCancelButton: true,
        confirmButtonText: "Save",
        //denyButtonText: `Don't save`
    }).then((isOkAction) => {
        // isOkAction will be true if "Yes" is clicked, false if "No" is clicked or if the dialog is dismissed
        callback(isOkAction);
    }); 
}

// === END SWAL ===


// === GLOBAL FUNCTION

function globalGetParamaterFromCurrentUrl(paramParameterName) {
    var url = new URL(window.location);
    return url.searchParams.get(paramParameterName);

}

function globalDownloadFile(url, fileName) {
    window.open(url + fileName); 
}


function globalIsNullOrEmptyString(param) {
    var isNullOrEmpty = false;
    if (param == null || param == "" || param == " ") {
        isNullOrEmpty = true;
    }
    return isNullOrEmpty;
}

function globalConvertFormSerializeToJson(paramString) {
    var result = {};
    paramString.split('&').forEach(function (keyValue) {
        var parts = keyValue.split('=');
        parts[0] = parts[0].replace(".Value", "");
        result[parts[0]] = decodeURIComponent(parts[1].replace(/\+/g, ' '));
    });

    return result;
}

// === END GLOBAL FUNCTION
