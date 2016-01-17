function showNotification(text) {
    $('#textNoti').html(text);
    $('.NotificationCenter').fadeIn(0).fadeOut(5000);
}
$.wait = function (callback, seconds) {
    return window.setTimeout(callback, seconds * 1000);
}

function addStock(stock) {
    alert('added');
}