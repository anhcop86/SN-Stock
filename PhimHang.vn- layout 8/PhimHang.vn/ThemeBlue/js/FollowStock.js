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
            success: function (data) {
                if (data == "A") {

                    showNotification('Đã thêm vào danh mục theo dõi');
                    $(".button-flow").html("<i class='fa fa-minus'></i>Đang theo dõi");
                }
                else if (data == "R") {
                    showNotification('Đã loại khỏi danh mục theo dõi');
                    $(".button-flow").html("<i class='fa fa-plus'></i>Theo dõi");
                } else {
                    alert('Lỗi');
                }
            }
        })
    });
});