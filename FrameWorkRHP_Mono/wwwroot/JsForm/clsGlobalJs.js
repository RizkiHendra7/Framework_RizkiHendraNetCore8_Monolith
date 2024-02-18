// === BLOCK UI ===
$(document).ajaxStart(function () {
    $.blockUI({ message: '<h1><img src="~/plugins/blockui/busy.gif" /> Just a moment...</h1>' });
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



// === END GLOBAL FUNCTION
