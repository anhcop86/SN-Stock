function showNotification(text) {
    $('#textNoti').html(text);
    $('.NotificationCenter').fadeIn(0).fadeOut(5000);
}
$.wait = function (callback, seconds) {
    return window.setTimeout(callback, seconds * 1000);
}
convertDateFormat = function (varDate) {
    var d = varDate.replace('T', '-').slice(0, 19).split('-');
    return d[2] + '/' + d[1] + '/' + d[0] + ' ' + d[3];
}
getTimeAgo = function (varDate) {
    return $.timeago(varDate);
}