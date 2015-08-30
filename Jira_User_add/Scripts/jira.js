function showerror(message) {
    alert(message);
}
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
            error: function(xhr) {
                try {
                    // a try/catch is recommended as the error handler
                    // could occur in many events and there might not be
                    // a JSON response from the server
                    var json = $.parseJSON(xhr.responseText);
                    sweetAlert(json.Data);
                } catch (e) {
                    alert('User or group is not added');
                }
            },
            success: function(xhr) {
                try {
                    swal("User successfully added");
                } catch (e) {
                    alert('User or group is not added');
                }
            }
    });
        return false;
    });
});