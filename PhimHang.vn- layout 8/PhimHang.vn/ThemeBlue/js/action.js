/* 
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
$(document).ready(function () {
    dialog = $("#dialog").dialog({
        modal: true,
        width: 650,        
        resizable: true,
        autoOpen: false,
        open: function (event, ui) {
            $('body').css('overflow', 'hidden');
            //$(window).scrollTop(0);
            $('.avata-genaral-size').focus(); // trang profile focus cái này
            if ($('#bg_dialog').length == 0) {
                $("<div id='bg_dialog'></div>").appendTo("body");                
                $(".ui-dialog").first().appendTo('#bg_dialog');
                $('#bg_dialog').show();
            }
            if ($('#close_dialog').length == 0) {
                $('<i id="close_dialog" class="fa fa-times-circle"></i>').appendTo("body");                
                $('#close_dialog').show();
            }
            //$('.area-center').css('height', '1000px');
        },
        close: function (event, ui) {
            $('#close_dialog').hide();
            $('#bg_dialog').hide();            
            $('body').css('overflow', 'auto');
        }
    });

    $(".ui-dialog-titlebar").hide();
    if (!$('#myonoffswitch').is(':checked')) {
        $("#msg-new-status").hide();
    }
    $(document).scrollTop(0); // set scrool top khi load lai trang
    //$("body").scrollTop(0);
    //$("#radio").buttonset();
    $('.divBull, .divBear').click(function () {
        if (!$(this).hasClass('switch3button-select')) {
            $(this).parent().children('.switch3button-select').removeClass('switch3button-select');
            $(this).addClass('switch3button-select');
        }
    });
    $("#tabs").tabs();
    $('#abcww').click(function () {
        if ($('#myonoffswitch').is(':checked')) {
            $("#msg-new-status").hide();
        }
        else
            $("#msg-new-status").show();
    });
    $('#btnBottomNext,#btnBottomPrev').click(function () {
        var leftPos = $('ul.alpha-bottom').scrollLeft();
        //compute the width from z to far right
        var rightScroll = $('ul.alpha-bottom').get(0).scrollWidth - $('ul.alpha-bottom').width() - leftPos;
        if ($(this).attr('id') == 'btnBottomNext')
        {
            leftPos += 74;
        }
        else
            leftPos -= 74;
        //The "22805" is width of last <li>
        if (rightScroll > 22805) {
            $('ul.alpha-bottom').animate({
                scrollLeft: leftPos
            }, 500);
        }
    });
    window.onresize = function onresize() {
        //reset position of scoll
        $('ul.alpha-bottom').animate({
            scrollLeft: 0
        }, 500);
    }
    $('.alpha-bottom > li:not(:first-child):not(:last-child)').click(function () {
        if (!$(this).hasClass('selected')) {
            $('.alpha-bottom').children('li').removeClass('selected');
            $(this).addClass('selected');
            $('.listCP').hide();
            $(this).children('.listCP').show();
        }
        else
        {
            $('.alpha-bottom').children('li').removeClass('selected');
            $(this).children('.listCP').hide();
        }
    });
    $('#status, #comment-text').focusin(function () {
        $(this).css('height', '75');
        $(this).parent().next('.status-control').show();
        //$('#top-status-box').addClass('open');
    });
    $('.status-cm').keyup(function () {
        if ($(this).val() != "") {
            $(this).parent().next('.status-control').find('.button-phim').removeClass('disable');
        }
        else
            $(this).parent().next('.status-control').find('.button-phim').addClass('disable');
    });
    $(document).click(function (evt) {
        if ($(evt.target).parents('.status-box').length == 0) {
            if ($("#status").val() == "") {
                $("#status").css('height', '25');
                $('#status').parent().next('.status-control').hide();
            }
        }
        if ($(evt.target).parents('#input-comment').length == 0) {
            if ($("#comment-text").val() == "") {
                $("#comment-text").css('height', '25');
                $('#comment-text').parent().next('.status-control').hide();
            }
        }
        if ($(evt.target).parents('.alpha-bottom').length == 0) {
            $('.alpha-bottom').children('li.selected').children('.listCP').hide();
            $('.alpha-bottom').children('li').removeClass('selected');
        }
//        if ($(evt.target).parents('.list-status').length == 0) {
//            $('.open').removeClass('open');
//            $('.list-comment').hide();            
//        }
        evt.stopPropagation();
    });
    //$(".list-item-status,.btn-answer").on('click', function (e) {
    //    if (!$(e.target).hasClass('btnMore')) {
    //        document.body.style.overflow = 'hidden';
    //        dialog.dialog("open");
    //        $(".ui-widget-overlay").click(function () {
    //            dialog.dialog('close');
    //            document.body.style.overflow = 'auto';
    //        });
    //    }
    //})//.on('click', 'a:not(.btnMore),button,input,img', function (e) {
    //    // clicked on descendant div            
    //    e.stopPropagation();
    //});
 


    //$("#tab-baiphim").click(function () {
    //    if (!$(this).hasClass('category-tab-active')) {
    //        $('.category-tab-active').removeClass('category-tab-active');
    //        $(this).addClass('category-tab-active');
    //        //$('#page3-tab1').show();
    //        //$('#page3-tab2').hide();
    //        //$('#page3-tab3').hide();
    //    }
    //    //return false;
    //});
    //$("#tab-DauTu").click(function () {
    //    if (!$(this).hasClass('category-tab-active')) {
    //        $('.category-tab-active').removeClass('category-tab-active');
    //        $(this).addClass('category-tab-active');
    //        //$('#page3-tab1').hide();
    //        //$('#page3-tab2').show();
    //        //$('#page3-tab3').hide();
    //    }
    //    //return false;
    //});
    //$("#tab-TheoDoi").click(function () {
    //    if (!$(this).hasClass('category-tab-active')) {
    //        $('.category-tab-active').removeClass('category-tab-active');
    //        $(this).addClass('category-tab-active');
    //        //$('#page3-tab1').hide();
    //        //$('#page3-tab2').hide();
    //        //$('#page3-tab3').show();
    //    }
    //    //return false;
    //});
    $(".filer-bar-item a").on('click', function (e) {
        $(".filer-bar-item a").removeClass("active");
        $(this).addClass("active");
        if (!$(this).attr('id')) {
            return false;
        }
    });

    //Array.prototype.contains = function containsArray(needle) {
    //    for (i in this) {
    //        if (this[i] == needle) return true;
    //    }
    //    return false;
    //}
    
   
    $("html").click(function () { // for dropdown menu
        if ($("#jq-dropdown-2").length > 0) {
            $("#jq-dropdown-2").remove();
        }
    });

// báo cáo vi phạm
    $("body").append('<div id="dialog-confirm" title="Báo cáo vi phạm"><p>'
                        + '<span class="ui-icon ui-icon-alert" style="float:left; margin:0 7px 20px 0;"></span>'
                        + 'Nội dung này không phù hợp?. </br>Chúng tôi sẽ xem xét bản tin này</p>'
                    + '</div>');
       

    $("#dialog-confirm").dialog({
        resizable: false,
        draggable: false,
        height: 180,        
        modal: true,        
        autoOpen: false,        
        create: function (event) { $(event.target).parent().css('position', 'fixed'); },
        open: function (event, ui) {            
            $('body').css('overflow', 'hidden');            
        },
        close: function (event, ui) {            
            $('body').css('overflow', 'auto');            
        },
        buttons: {
            "Báo cáo": function () {
                var postid = $(this).data('postid');
                // post data on server
                reportPostUpServer(postid);
                $(this).dialog("close");
            },
            Cancel: function () {
                $(this).dialog("close");
            }
        }
    });

    function reportPostUpServer(postid) {
        $.ajax({
            url: '/Post/ReportError',
            type: 'POST',
            data: { postid: postid },
            cache: false,            
        }).success(function (data) {
            if (data === "Y") {
                showNotification('Báo cáo vi phạm thành công, chúng tôi sẽ phản hồi sớm nhất');
                return;
            }
            else {
                showNotification('Có lỗi');
            }
        }).fail(function () {
            showNotification('Có lỗi fail');
        })
    }
    
    // xóa cổ phiếu đang theo dõi
    addStock = function (stockOject) {
        var stock = stockOject.val().toUpperCase();
        if (stock == '' || stock.length < 3 || stock.length > 10) {
            stockOject.focus();
            stockOject.addClass("forcusAddAlertStock");
            return;
        }
        else{
            addorDelete(stock);
            stockOject.removeClass("forcusAddAlertStock");
        }
    }

    $("#addStock").keyup(function (e) {
        if (e.keyCode == 13) {
            // Do something
            addStock($(this));
        }
    });
    // thêm cổ phiếu đang theo doi

    deleteStock = function (stock) {
        if (confirm("Bạn muốn loại khỏi danh mục theo dõi?")) {
            addorDelete(stock.toUpperCase());
        }
    }

    function addorDelete(stock) {
        $.ajax({
            cache: false,
            type: "POST",
            url: '/FollowStock/Create',
            data: { stock: stock },
            beforeSend: function (xhr) {
                //Add your image loader here

            },
            beforeSend: function (xhr) {
                //$(".button-flow").hide();
            },
            success: function (data) {
                if (data == "A") {
                    showNotification('Đã thêm vào danh mục theo dõi');
                    location.reload();
                    //$.wait(function () { $(".button-flow").html("<i class='fa fa-minus'></i>Đang theo dõi").fadeIn('slow'); }, 2);
                }
                else if (data == "R") {
                    showNotification('Đã loại khỏi danh mục theo dõi');
                    $("#banner-item-" + stock).remove();
                } else {
                    showNotification('Lỗi hoặc cổ phiếu không tồn tại.');
                }
            }
        })
    }

    // add menu hide
    //$(".toSpan top-banner-icon2 f-right").append('<span><i class="fa fa-plus-square"></i></span>');
   
   
    closeMenu = function () {
        $('body').css('overflow', 'auto');
        $("#MenuReposive").fadeOut();        
    }

    openMenu = function () {
        $('body').css('overflow', 'hidden');
        $("#MenuReposive").fadeIn();
        
    }

    // thay doi hinh cover va avata tai trang user

    uploadCoverUser = function (files) {
        file = files[0];
        if (file == null) {
            return;
        }
        if (file.size > 5000000) {
            showNotification('Hình quá lớn, vui lòng chọn hình khác nhỏ hơn 5MB');
            return;
        }
        var ext = file.name.split('.').pop().toLowerCase();
        if ($.inArray(ext, ['png', 'jpg', 'jpeg']) == -1) {
            showNotification('File hình không đúng định dạng!. Chỉ hỗ trợ file hình png, jpg, jpeg');
            return;
        }
        // Add the uploaded image content to the form data collection
        if (files.length > 0) {
            var formData = new FormData();
            formData.append("UploadedImage", file);
            //upload via ajax
            $.ajax({
                url: '/Account/CoverUpload',
                type: 'POST',
                data: formData,
                cache: false,
                contentType: false,
                processData: false,
                crossDomain: false,
                beforeSend: function (xhr) {
                    $('.uploadStatus').html('<img src="/img/uploading.gif" />');
                },
                success: function (data) {
                    if (data === "error") {
                        showNotification('Có lỗi khi upload ảnh, vui lòng thử lại');
                        return
                    }
                    else {
                        showNotification('Cập nhật hình cover thành công, có thể kéo ảnh để thay đổi góc nhìn');
                        $('#CoverImageId').attr("src", data.replace("YES|", "") + "?w=1366&mode=crop");
                    }
                    $('.uploadStatus').html('');

                },
                fail: function () {
                    showNotification('Có lỗi khi upload ảnh, vui lòng thử lại');
                    $('.uploadStatus').html('');
                }
            })
        }
    }
    uploadAvata = function (files) {
        file = files[0];
        if (file == null) {
            return;
        }
        if (file.size > 4000000) {
            showNotification('Hình quá lớn, vui lòng chọn hình khác nhỏ hơn 4MB');
            return;
        }
        var ext = file.name.split('.').pop().toLowerCase();
        if ($.inArray(ext, ['png', 'jpg', 'jpeg']) == -1) {
            showNotification('File hình không đúng định dạng!. Chỉ hỗ trợ file hình png, jpg, jpeg');
            return;
        }
        // Add the uploaded image content to the form data collection
        if (files.length > 0) {
            var formData = new FormData();
            formData.append("UploadedImage", file);
            //upload via ajax
            $.ajax({
                url: '/Account/AvataUpload',
                type: 'POST',
                data: formData,
                cache: false,
                contentType: false,
                processData: false,
                crossDomain: false,
                beforeSend: function (xhr) {
                    $('.uploadStatus').html('<img src="/img/uploading.gif" />');
                },
                success: function (data) {
                    if (data === "error") {
                        showNotification('Có lỗi khi upload ảnh, vui lòng thử lại');
                        $('#avataInput').val('');
                        return
                    }
                    else {
                        $('#AvataImage').attr("src", "../" + data.replace("YES|", "") + "?w=190&h=190&mode=crop");
                        showNotification('Cập nhật avata thành công');
                        $('#avataInput').val('');
                    }
                    $('.uploadStatus').html('');
                },
                fail: function () {
                    showNotification('Có lỗi khi upload ảnh, vui lòng thử lại:');
                    $('#avataInput').val('');
                }
            })
        }
    }
    
    //loadNormalText = function (chartYN, chartUrl, message) {
    //    return chartYN == 1 ? message + '<br/><img src=' + chartUrl + '?width=215&height=120&mode=crop>' : message;
    //}
    clickloadNextText = function (postid, ojecthtml) {
        var messageId = '#messageId' + postid;
        //console.log(messageId);
        if ($(messageId).hasClass("less")) {
            $(messageId).removeClass("less");
            $(ojecthtml).html("Thu nhỏ");            
        } else {            
            $(messageId).addClass("less");
            $(ojecthtml).html("Xem thêm");
        }
        //$(messageId).parent().prev().toggle();
        //$(messageId).prev().toggle();
        return false;
    }
});
