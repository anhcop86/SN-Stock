$(document).ready(function () {
    $(".button-flow").click(function () {
        var stock = $('#stockHidenPage').val();
        $.ajax({
            cache: false,
            type: "POST",
            url: '/FollowStock/Create',
            data: { stock: stock },
            beforeSend: function (xhr) {
                //Add your image loader here

            },
            beforeSend: function (xhr) {
                
            },
            success: function (data) {
                if (data == "A") {
                    showNotification('Đã thêm vào danh mục theo dõi');
                    $(".button-flow").hide();
                    $.wait(function () { $(".button-flow").html("<i class='fa fa-minus'></i>Đang theo dõi").fadeIn('slow'); }, 5);                    
                }
                else if (data == "R") {
                    showNotification('Đã loại khỏi danh mục theo dõi');
                    $(".button-flow").hide();
                    $.wait(function () { $(".button-flow").html("<i class='fa fa-plus'></i>Theo dõi").fadeIn('slow'); }, 5);
                } else {
                    alert('Lỗi');
                }
            }
        })
    });
});