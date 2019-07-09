// parameter is a HTMLCollection of elements
export function clearFields(fields) {
    for (let i = 0; i < fields.length; i++) {
        console.log(fields[i]);
        if (fields[i].nodeName === "SELECT") {
            fields[i].selectedIndex = -1;
        }
        if (fields[i].nodeName === "INPUT") {
            fields[i].value = "";
        }
    }
}