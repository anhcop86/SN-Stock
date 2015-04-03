function showNotification(text) {
    $('#textNoti').html(text);
    $('.NotificationCenter').fadeIn(300).fadeOut(5000);
}
$.wait = function (callback, seconds) {
    return window.setTimeout(callback, seconds * 1000);
}