$(document).ready(function () {    
    $('.cover-div img').css('cursor', 's-resize')
        .draggable({
            scroll: false,
            axis: "y",
            cursor: "s-resize",
            drag: function (event, ui) {
                y1 = 320;
                y2 = $('.cover-div').find('img').height();

                if (ui.position.top >= 0) {
                    ui.position.top = 0;
                }
                else
                    if (ui.position.top <= (y1 - y2)) {
                        ui.position.top = y1 - y2;
                    }
            },

            stop: function (event, ui) {
                $('input.cover-position').val(ui.position.top);
            }
        });
    $('.covertStyle').click(function() {        
        var formData = new FormData();
        formData.append("positionHeight", $('input.cover-position').val());        
        $.ajax({
            url: '/Account/resizeImage',
            type: 'POST',
            data: formData,
            cache: false,
            contentType: false,
            processData: false
        }).done(function (data) {
            if (data === "error") {

                showNotification('Có lỗi khi upload ảnh, vui lòng thử lại');
                return
            }
            else {
                $('.chartImage').show();
                $('.mb3-chart-thumb').attr("src", data);
            }
        }).fail(function () {
            showNotification('Có lỗi khi upload ảnh, vui lòng thử lại');
        })
    })
    
});

