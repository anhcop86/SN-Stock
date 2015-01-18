/* 
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
$(document).ready(function () {
    $("#radio").buttonset();
    $("#tabs").tabs();
    $("#btn-config").click(function () {
        if ($(this).next('.dropdownlist').css('display') == 'block') {
            $(this).next('.dropdownlist').hide();
        }
        else
            $(this).next('.dropdownlist').show();
    });
    $("#btn-option-more").click(function () {
        $dr = $(this).parent().next().next('.dropdownlist');
        if ($dr.css('display') == 'block') {
            $dr.hide();
        }
        else
            $dr.show();
    });
    $('#abcww').click(function () {
//        alert($('#myonoffswitch').is(':checked'));
        if ($('#myonoffswitch').is(':checked')) {
            $("#msg-new-status").hide();
        }
        else
            $("#msg-new-status").show();
    });
    $('#status').focusin(function () {
        $(this).css('height', '75');
        $('.status-control').show();
    });
    $(document).scroll(function () {
        if ($(this).scrollTop() < 50) {
            $('.menu-top-bar').removeClass('menu-top-bar-fix');
        }
        else {
            if (!$('.menu-top-bar').hasClass('menu-top-bar-fix')) {
                $('.menu-top-bar').addClass('menu-top-bar-fix');                
            }
        }
    });
});

