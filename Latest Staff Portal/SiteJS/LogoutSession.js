var inactivityTime = 0;
var logoutTime = 300000; // 5 minutes in milliseconds

function startTimer() {
    setInterval(function () {
        inactivityTime += 1000;
        if (inactivityTime >= logoutTime) {
            logout();
        }
    }, 1000);
}

document.addEventListener('mousemove', function (event) {
    inactivityTime = 0;
});

document.addEventListener('keypress', function (event) {
    inactivityTime = 0;
});

function logout() {
    window.location.href = "/Login/Login";
}

startTimer();
