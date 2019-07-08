// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your Javascript code.

// applies only to manage/index page

// hides all type-specific information until type is selected
$(document).ready(function () {
    changeShownFields(document.getElementById("RoleDropdown"));
});
//adds event listener to load below code on page load


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
    //console.log("function has started");
    //console.log(Dropdown);
    if (Dropdown !== null) {
        let UserType = Dropdown.options[Dropdown.selectedIndex].value;

        if (UserType === "Stoker") {

            // show captain+stoker info
            $("#captain_stoker").show();
            // show stoker-specific info
            $("#stoker").show();
            $("#captain").hide();
            //HasTandem.selectedIndex = -1;
            //HasSingleBike.selectedIndex = -1;
        }
        if (UserType === "Captain") {

            // show captain+stoker info
            $("#captain_stoker").show();
            // show captain-specific info
            $("#captain").show();
            $("#stoker").hide();
            /*if ((Dog !== null) && (SpecialEquipment !== null)) {
                Dog.value = "";
                SpecialEquipment.value = "";
            }*/
                
        }
        if (UserType === "Volunteer") {

            // show no specific information for volunteer
            $("#captain_stoker").hide();
            /*if ((Height !== null) && (Weight !== null) && (HasSeat !== null)) {
                Height.value = "";
                Weight.value = "";
                HasSeat.selectedIndex = -1;
            }*/
            
            $("#captain").hide();
            /*if ((HasTandem !== null) && (HasSingleBike !== null)) {
                HasTandem.selectedIndex = -1;
                HasSingleBike.selectedIndex = -1;
            }*/
            $("#stoker").hide();
            /*if ((Dog !== null) && (SpecialEquipment !== null)) {
                Dog.selectedIndex = -1;
                SpecialEquipment.value = "";
            }*/
        }
    }
}