// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your Javascript code.

// applies only to manage/index page
import { clearFields } from "./clearFields"


// hides all type-specific information until type is selected
//adds event listener to load below code on page load

$(document).ready(function () {
    changeShownFields(document.getElementById("RoleDropdown"));
});


/*function changeUserProfileFields() {

    let Height = document.getElementById("Height");
    let Weight = document.getElementById("Weight");
    let HasSeat = document.getElementById("HasSeatDropdown");
    let HasTandem = document.getElementById("HasTandemDropdown");
    let HasSingleBike = document.getElementById("HasSingleBikeDropdown");
    let Dog = document.getElementById("DogDropdown");
    let SpecialEquipment = document.getElementById("SpecialEquipment");
    
    //set index to -1 to set dropdown value to blank
}*/

function changeShownFields(Dropdown) {
    console.log("function has started");
    //console.log(Dropdown);
    //const Captain+StokerFields = $(".captain+stoker-field");
    //const CaptainFields = $(".captain-field");
    //const StokerFields = $(".stoker-field");

    //clearFields(e);
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
        if (UserType === "Volunteer") {

            // show no specific information for volunteer
            $("#captain_stoker").hide();
            $("#captain").hide();
            $("#stoker").hide();
        }
    }
}