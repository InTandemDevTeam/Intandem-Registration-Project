//import { clearFields } from "./clearFields";
//import clearMultipleFields from "./clearMultipleInputs.js";
//import * from './clearFields.js'
// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.
// Write your Javascript code.

// applies only to manage/index page

// hides all type-specific information until type is selected
// adds event listener to load below code on page load
console.log("manage page reached");
$(document).ready(function () {
    let RoleDropdown = document.getElementById("RoleDropdown");
    // clears fields not pertaining to user selection on page load
    changeShownFields(RoleDropdown);
    // adds event handler to user dropdown menu to clear fields on change
    RoleDropdown.onchange = function () {
        changeShownFields(RoleDropdown);
    }


});

function changeShownFields(Dropdown) {

    let CaptainStokerFields = $(".CaptainStokerField");
    let CaptainFields = $(".CaptainField");
    let StokerFields = $(".StokerField");
    if (Dropdown !== null) {
        let UserType = Dropdown.options[Dropdown.selectedIndex].value;

        if (UserType === "Stoker") {

            // show captain+stoker info
            $("#captain_stoker").show();
            // show stoker-specific info
            $("#stoker").show();
            $("#captain").hide();
            // clears information not pertaining to stoker
            clearMultipleFields(CaptainFields, "CaptainField")

        }
        if (UserType === "Captain") {
            // show captain+stoker info
            $("#captain_stoker").show();
            // show captain-specific info
            $("#captain").show();
            $("#stoker").hide();
            clearMultipleFields(StokerFields, "StokerField")
            // clears information not pertaining to captain
        }
        if (UserType === "Volunteer"
            || UserType === ""
            || UserType === null
            || UserType === "Admin") {

            // show no specific information for volunteer
            $("#captain_stoker").hide();
            $("#captain").hide();
            $("#stoker").hide();
            // clears information not pertaining to volunteer
            clearMultipleFields(CaptainFields, "CaptainField")
            clearMultipleFields(StokerFields, "StokerField")
            clearMultipleFields(CaptainStokerFields, "CaptainStokerField")
        }
    }
}


function clearSingleField(field) {
    // if select element is found, its value is cleared
    if (field.nodeName === "SELECT") {
        console.trace("select field found");
        field.selectedIndex = -1;
    }
    // if input element is found, its value is cleared
    if (field.nodeName === "INPUT") {
        console.trace("input field found");
        field.value = "";
    }
}
function clearMultipleFields(fields, classToCheck) {
    // first param is data structure containing html elements
    // loops through array-like jquery object
    // if element has class passed into function
    // clear that element's value
    $(fields).each(function () {
        if ($(this).hasClass(classToCheck)) {
            // clears single field
            clearSingleField(this);
            console.log("clearSingleField executed");
            console.log(this);
        }
    });
}