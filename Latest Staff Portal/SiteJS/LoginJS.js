//var logoutTimer;

//$(document).ready(function () {
//    $("#btnLogin").click(function () {
//        var username = $("#username").val();
//        var password = $("#userpassword").val();
//        if (username == "") {
//            Swal.fire('Warning', 'Enter Username', 'warning');
//            return;
//        }
//        else if (password == "") {
//            Swal.fire('Warning', 'Enter Your Password', 'warning');
//            return;
//        }
//        else {
//            var UserCred = {};
//            UserCred.Username = username;
//            UserCred.Password = password;
//            ShowProgress();
//            $.ajax({
//                url: "/Login/LoginUser",
//                datatype: "json",
//                type: "POST",
//                data: JSON.stringify({ userlogin: UserCred }),
//                contentType: "application/json; charset = utf-8",
//                success: function (data) {
//                    if (data.success == true) {
//                        window.location = data.message;
//                    }
//                    else {
//                        HideProgress();
//                        Swal.fire('Warning', data.message, 'warning');
//                    }
//                },
//                error: function () {
//                    if (data.success == false) {
//                        HideProgress();
//                        Swal.fire('Error', data.message, 'error');
//                    }
//                }
//            });
//        }
//    });

//    $("#username").keyup(function (event) {
//        if (event.keyCode === 13) {
//            $("#btnLogin").click();
//        }
//    });

//    $("#userpassword").keyup(function (event) {
//        if (event.keyCode === 13) {
//            $("#btnLogin").click();
//        }
//    });

//    // Initialize the logout timer on document load
//    resetLogoutTimer();
//});

//function resetLogoutTimer() {
//    clearTimeout(logoutTimer);
//    //window.alert('Your session timeout has been reset.');

//    logoutTimer = setTimeout(function () {
//        logout();
//    }, 60000); // 1 minute timeout

//}


//// Function to perform logout
//function logout() {
//    // Perform logout action, such as redirecting to the logout page
//    window.location.href = "/Login/Login";
//}

//// Function to check if the user is active and reset the logout timer
//function checkUserActivity() {
//    $(document).on("mousemove keydown", function () {
//        resetLogoutTimer();
//    });
//}

//// Call the checkUserActivity function to start monitoring user activity
//checkUserActivity();
