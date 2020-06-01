// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.
// Write your Javascript code.
// applies only to manage/index page

// to implement the caching fields functionality
// will have to figure out a way to define the clearMultipleFields
// function without three parameters, look at jQuery Docs
// hides all type-specific information until type is selected
// adds event listener to load below code on page load
// will have to store data in fields in structure that can be updated
// without adding or removing items, without touching the length
// will have to add checks on .each calls to not push empty values to arrays
let cachedCaptainValues = [];
let cachedCaptainStokerValues = [];
let cachedStokerValues = [];
//console.log("register page reached");
$(document).ready(function () {
    const RoleDropdown = document.getElementById("RoleDropdown");
    // clears fields not pertaining to user selection on page load
    changeShownFields(RoleDropdown);

    // adds event handler to user dropdown menu to clear fields on change
    RoleDropdown.onchange = function () {
        changeShownFields(RoleDropdown);
    }
});

function changeShownFields(Dropdown) {
    const CaptainStokerFields = $(".CaptainStokerField");
    const CaptainFields = $(".CaptainField");
    const StokerFields = $(".StokerField");
    $(".CaptainStokerField").each(function () {
        if ((this.nodeName === "SELECT") && (this.value !== "")) {
            cachedCaptainStokerValues.push($(this).prop("selectedIndex"));
        }
        if ((this.nodeName === "INPUT") && (this.value !== "")) {
            cachedCaptainStokerValues.push(this.value);
        }
    });
    $(".CaptainField").each(function () {
        if (this.nodeName === "SELECT") {
            cachedCaptainValues.push(this.options[this.selectedIndex]);
        }
        if (this.nodeName === "INPUT") {
            cachedCaptainValues.push(this.value);
        }
    });
    $(".StokerField").each(function () {
        if (this.nodeName === "SELECT") {
            cachedStokerValues.push(this.options[this.selectedIndex]);
        }
        if (this.nodeName === "INPUT") {
            cachedStokerValues.push(this.value);
        }
    });
    console.table(cachedCaptainStokerValues);

    if (Dropdown !== null) {
        const UserType = Dropdown.options[Dropdown.selectedIndex].value;
        if (UserType === "Stoker") {

            // show captain+stoker info
            $("#captain_stoker").show();
            // show stoker-specific info
            $("#stoker").show();
            $("#captain").hide();
            changeMultipleFields(CaptainFields, "clear")
            // clears information not pertaining to stoker

        }
        if (UserType === "Captain") {
            // show captain+stoker info
            $("#captain_stoker").show();

            // show captain-specific info
            $("#captain").show();

            // hides/clears information not pertaining to captain
            $("#stoker").hide();
            changeMultipleFields(StokerFields, "clear")
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
            changeMultipleFields(CaptainFields, "clear")
            changeMultipleFields(StokerFields, "clear")
            changeMultipleFields(CaptainStokerFields, "clear")
        }
    }
}


function changeMultipleFields(fields, newValues) {
    // first param is data structure containing html elements
    // loops through array-like jquery object
    // assumed that indeces of both parameters correspond exactly to a given element and its cached value
    $(fields).each(function (index) {
        // if <select> element is detected
        if (this.nodeName === "SELECT") {
            // ternary operator
            // if newValues is a string "clear"
            // then fields are cleared
            // if not then newValues is assumed to be an array of values
            (newValues === "clear") ?
                this.selectedIndex = -1 :
                this.selectedIndex = newValues[index];
        }
        // if <input> element is detected
        if (this.nodeName === "INPUT") {
            // ternary operator
            // if newValues is a string "clear"
            // then fields are cleared
            // if not then newValues is assumed to be an array of values
            (newValues === "clear") ?
                this.value = "" :
                this.value = newValues[index];
        }
    });
}