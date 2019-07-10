//import { clearFields } from "./clearFields";

//import * from './clearFields.js'
// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your Javascript code.

// applies only to manage/index page

//let Captain_StokerFields;
//let CaptainFields;
//let StokerFields;
//let UserFields;
//let RoleDropdown;
// hides all type-specific information until type is selected
//adds event listener to load below code on page load

$(document).ready(function () {
    RoleDropdown = document.getElementById("RoleDropdown");
    //RoleDropdown = $("#RoleDropdown");

    //Captain_StokerFields = $(".captain+stoker-field");
    //CaptainFields = $(".captain-field");
    //StokerFields = $(".stoker-field");
    UserFields = $(".user-field");
    changeShownFields(RoleDropdown);
    RoleDropdown.onchange = function () {
        changeShownFields(RoleDropdown);
        //clearFields(RoleDropdown);
    }
    //console.log(RoleDropdown);

    for (let i = 0; i < UserFields.length; i++) {
        //console.log(UserFields[i]);
        if ($(UserFields[i]).hasClass("captain+stoker-field")) {
            //add event handler
            //clearField(UserFields[i]);
            console.log("check for captain+stoker field works")
        }
        if ($(UserFields[i]).hasClass("captain-field")) {
            //add event handler
            //clearField(UserFields[i]);
            console.log("check for captain field works")
        }
        if ($(UserFields[i]).hasClass("stoker-field")) {
            //add event handler
            //clearField(UserFields[i]);
            console.log("check for stoker field works")
        }

    }
});

function changeShownFields(Dropdown) {
    console.log("function has started");
    //console.log(Dropdown);
    if (Dropdown !== null) {
        let UserType = Dropdown.options[Dropdown.selectedIndex].value;

        if (UserType === "Stoker") {

            // show captain+stoker info
            $("#captain_stoker").show();
            // show stoker-specific info
            $("#stoker").show();
            $("#captain").hide();
        }
        if (UserType === "Captain") {
            // show captain+stoker info
            $("#captain_stoker").show();
            // show captain-specific info
            $("#captain").show();
            $("#stoker").hide();
        }
        if (UserType === "Volunteer"
            || UserType === ""
            || UserType === null)
        {
            
            // show no specific information for volunteer
            $("#captain_stoker").hide();
            $("#captain").hide();
            $("#stoker").hide();
        }
    }
}

function clearField(field) {
    console.log(field);
    if (field.nodeName === "SELECT") {
        field.selectedIndex = -1;
    }
    if (field.nodeName === "INPUT") {
        field.value = "";
    }

}
