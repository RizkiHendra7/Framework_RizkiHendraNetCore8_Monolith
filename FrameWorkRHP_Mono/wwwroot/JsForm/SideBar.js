
$(document).ready(function () {
    renderSideBar();
});

function renderSideBar() {
    $.ajax({
        type: "POST",
        url: "/DynamicMenu/GenerateMenu",
        data: {
            paramModuleExisting: activePage 
        },
        datatype: "json",
        success: function (result) { 
            document.getElementById("idRender").innerHTML = result

        },
        error: function (retDat) {
        }
    });
}