// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your Javascript code.

// applies only to registration page
// hides all type-specific information until type is selected
$(document).ready(function () {
    $("#captain_stoker").hide();
    $("#captain").hide();
    $("#stoker").hide();
});

function templating() {

    var e = document.getElementById('TypeOfUserDropdown')
    if (e != null) {
        var UserType = e.options[e.selectedIndex].value;
        if (UserType == 'Stoker') {
            // show captain+stoker info
            $("#captain_stoker").show();
            // show stoker-specific info
            $("#stoker").show();
            $("#captain").hide();
        }
        if (UserType == 'Captain') {
            // show captain+stoker info
            $("#captain_stoker").show();
            // show captain-specific info
            $("#captain").show();
            $("#stoker").hide();
        }
        if (UserType == 'Volunteer') {
            // show no specific information for volunteer
            $("#captain_stoker").hide();
            $("#captain").hide();
            $("#stoker").hide();
            
        }
    }
}