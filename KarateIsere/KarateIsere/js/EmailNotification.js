function NotificationStart() {
    $('#NotifyEmail').attr("disabled", "disabled");    
}

function NotificationEnd(data) {
    $('#NotifyEmail').removeAttr("disabled");
}

function NotificationSuccess(data) {
    $('#NotifyEmail').removeAttr("disabled");
    $('#notifyRes').addClass("alert-success");
    $('#notifyRes').show();

    setTimeout(function () {
        $('#notifyRes').hide();
    }, 3000);
}

function NotificationFailed(data) {
    $('#notifyRes').addClass("alert-danger");
    $('#notifyRes').show();
}