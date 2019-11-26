// search code
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
//$(document).ready(function () {
//    $('#userSearchBox').autocomplete({
//        // change source so that it gets json produced
//        // by search method in userservice
//        source: '/search'
//        //source: availableTags
//    });
//});

$(document).ready(function () {
    $("#userSearchBox").autocomplete({
        source: function (request, response) {
            $.ajax({
                url: "/search",
                type: "POST",
                dataType: "json",
                data: { Prefix: request.term },
                success: function (data) {
                    response($.map(data, function (item) {
                        return { label: item.Name, value: item.Name };
                    }))

                }
            })
        },
        messages: {
            noResults: "", results: ""
        }
    });
}) 