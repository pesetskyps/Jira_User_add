function showError(error) {
    alert("hhhhh");
};


$(document).ready(function() {


    $('form').submit(function(event) {
        var postData = $(this).serializeArray();
        var formURL = $(this).attr("action");
        // process the form
        $.ajax({
            type: 'POST', // define the type of HTTP verb we want to use (POST for our form)
            url: formURL, // the url where we want to POST
            data: postData, // our data object
            dataType: 'json', // what type of data do we expect back from the server
            encode: true,
            error: function(xhr, textStatus, error) {
                debugger;alert("hhhhh");
            }
        });
        return false;
    });
});