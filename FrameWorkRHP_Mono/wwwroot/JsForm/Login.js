$('#idBtnSignIn').click(function () {

    var objData = {};
    objData.Txtusername = $("#Txtusername").val(); 
    objData.Txtpassword = $("#Txtpassword").val(); 

    $.ajax({
        type: "POST",
        url: "/Login/Login",
        data: {
            paramData: objData,
            __RequestVerificationToken: $('#frmDetail input[name=__RequestVerificationToken]').val()
        },
        datatype: "json",
        success: function (result) {
            if (result) { 
                window.location.href = "/LandingPage";

            } else {
                globalShowAlertInfo("The password that you've entered is incorrect.")
            }
        },
        error: function (xhr, error, thrown) {
            globalShowAlertError(xhr.responseText);
        }
    });
});