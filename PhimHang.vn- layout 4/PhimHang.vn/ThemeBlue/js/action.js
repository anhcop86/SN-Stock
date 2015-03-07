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
    });
    $(".ui-dialog-titlebar").hide();
    if (!$('#myonoffswitch').is(':checked')) {
        $("#msg-new-status").hide();
    }
    $(document).scrollTop(50);
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
    
    
});
