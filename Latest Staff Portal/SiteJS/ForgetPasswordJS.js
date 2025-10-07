$(document).ready(function () {
    $("#btnReset").click(function () {
        var username = $("#username").val();
        if (username == "") {
            Swal.fire('Warning', 'Enter Username', 'warning');
            return;
        }
        else {
            var Authedication = {};
            Authedication.Username = username;
            ShowProgress();
            debugger
            $.ajax({
                url: "/Login/ForgotPassword",
                datatype: "json",
                type: "POST",
                data: JSON.stringify({ userlogin: Authedication }),
                contentType: "application/json; charset = utf-8",
                success: function (data) {
                    if (data.success == true) {
                        HideProgress();
                        Swal.fire('Success', data.message, 'success');
                       
                    }
                    else {
                        HideProgress();
                        Swal.fire('Warning', data.message, 'warning');
                        
                    }
                },
                error: function (err) {
                    HideProgress();
                    Swal.fire('Error', err, 'error');
                }
            });
        }
    });
});
$("#username").keyup(function (event) {
    if (event.keyCode === 13) {
        $("#btnReset").click();
    }
});