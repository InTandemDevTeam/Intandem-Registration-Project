export default function clearSingleField(field) {
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