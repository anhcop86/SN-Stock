$(document).ready(function () {   
    $(".button-flow").click(function () {
        var userid = $('#HiddentUserId').val();
        $.ajax({
            cache: false,
            type: "POST",
            url: '/FollowStock/CreateUserFollow',
            data: { userid: userid },
            beforeSend: function (xhr) {
                //Add your image loader here

            },
            success: function (data) {
                if (data == "A") {

                    showNotification('Theo dõi người này thành công');
                    $(".button-flow").hide();
                    $.wait(function () { $(".button-flow").html("<i class='fa fa-minus'></i>Đang theo dõi").fadeIn('slow'); }, 5);

                }
                else if (data == "R") {
                    showNotification('Đã loại khỏi danh mục theo dõi thành công');
                    $(".button-flow").hide();
                    $.wait(function () { $(".button-flow").html("<i class='fa fa-plus'></i>Theo dõi").fadeIn('slow'); }, 5);
                } else {
                    alert('Lỗi');
                }
            }
        })
    });
});