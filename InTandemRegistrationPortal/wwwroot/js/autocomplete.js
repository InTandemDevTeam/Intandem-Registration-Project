// search code
// works, controller maintainence needed
function getBaseUrl() {
    return window.location.href.match(/^.*\//);
}
const rootUrl = window.location.host
console.log(rootUrl)
const testvar = rootUrl.concat('/api/searchuser/search')
console.log(testvar)
var availableTags = [
      "ActionScript",
      "AppleScript",
      "Asp",
      "BASIC",
      "C",
      "C++",
      "Clojure",
      "COBOL",
      "ColdFusion",
      "Erlang",
      "Fortran",
      "Groovy",
      "Haskell",
      "Java",
      "JavaScript",
      "Lisp",
      "Perl",
      "PHP",
      "Python",
      "Ruby",
      "Scala",
      "Scheme"
];

$(document).ready(function () {
    $("#userSearchBox").autocomplete({

        source: rootUrl.concat('/api/searchuser/search')
        //source: availableTags

    })

}) 