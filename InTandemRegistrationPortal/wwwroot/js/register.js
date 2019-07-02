// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your Javascript code.

// applies only to registration page
// hides all type-specific information until type is selected
//let captain = document.getElementById('captain');
//let captain_stoker = document.getElementById('captain_stoker');
//let stoker = document.getElementById('stoker');
/*var HeightSpan = document.getElementById('HeightSpan');
var WeightSpan = document.getElementById('WeightSpan');
var HasSeatSpan = document.getElementById('HasSeatSpan');
var HasTandemSpan = document.getElementById('HasTandemSpan');
var HasSingleBikeSpan = document.getElementById('HasSingleBikeSpan');
*/
$(document).ready(function () {
    $('#captain_stoker').hide();
    $('#captain').hide();
    $('#stoker').hide();
});

function templating() {

    let e = document.getElementById('RoleDropdown');
    if (e != null) {
        let UserType = e.options[e.selectedIndex].value;
        if (UserType == 'Stoker') {
            // show captain+stoker info
            $('#captain_stoker').show();
            // show stoker-specific info
            $('#stoker').show();
            $('#captain').hide();
            // disable validation for captain-specific fields
            /*if (HasTandemSpan.hasAttribute('asp-validation-for')
                && HasSingleBikeSpan.hasAttribute('asp-validation-for') {
                HasTandemSpan.removeAttr('asp-validation-for')
            } else {
                HasTandemSpan.attr('asp-validation-for', 'Input.HasTandem');
                HasSingleBikeSpan.attr('asp-validation-for', 'Input.HasSingleBike');
            }*/
        }
        if (UserType == 'Captain') {
            // check that validation is applied for applicable fields
            /*if (!(HeightSpan.hasAttribute('asp-validation-for'))
                && !(WeightSpan.hasAttribute('asp-validation-for'))
                && !(HasTandemSpan.hasAttribute('asp-validation-for'))
                && !(HasSingleBikeSpan.hasAttribute('asp-validation-for')) 
            {


            }*/
            // show captain+stoker info
            $('#captain_stoker').show();
            // show captain-specific info
            $('#captain').show();

            $('#stoker').hide();;
            // no need to disable validation for stoker 
            // since those fields are not required
            
        }
        if (UserType == 'Volunteer') {
            // show no specific information for volunteer
            $('#captain_stoker').hide();
            $('#captain').hide();
            $('#stoker').hide();
            
        }
    }
}