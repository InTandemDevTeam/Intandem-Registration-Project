import clearSingleField from "./clearSingleField.js";
export default function clearMultipleFields(fields, classToCheck) {
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