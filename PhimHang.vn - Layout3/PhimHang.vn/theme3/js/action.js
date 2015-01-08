/* 
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
$(document).ready(function () {
    if (!$('#myonoffswitch').is(':checked')) {
        $("#msg-new-status").hide();
    }
    //$("#radio").buttonset();
    $("#tabs").tabs();    
    $('#abcww').click(function () {
        if ($('#myonoffswitch').is(':checked')) {
            $("#msg-new-status").hide();
        }
        else
            $("#msg-new-status").show();
    });
    $('#status').focusin(function () {
        $(this).css('height', '75');
        $('.status-control').show();
		//$('#top-status-box').addClass('open');
    });
    //$(document).click(function (evt) {        
    //    if ($(evt.target).parents('.status-box').length == 0) {
    //        if ($("#status").val() == "") {
    //            $("#status").css('height', '25');
    //            $('.status-control').hide();
    //        }
    //    }
    //    if ($(evt.target).parents('.list-status').length == 0) {
    //        $('.open').removeClass('open');
    //        $('.list-comment').hide();            
    //    }
    //    e.stopPropagation();
    //});

    $(".list-item-status,.btn-answer").on('click',  function (e) {
		if($(e.target).parents('.open').length == 0){
			$('.open').removeClass('open');
			$('.list-comment').hide();
			if (!$(this).hasClass('btn-answer')) {
				$(this).next().show();
				$(this).parent('.bg-white').addClass('open');
			}
			else {
				$(this).parent().parent().parent().next().show();            
				$(this).parent().parent().parent().parent('.bg-white').addClass('open');
			}
		}
		else {
			$('.open').removeClass('open');
			$('.list-comment').hide();
		}
    }).on('click', 'a,button,input,img', function (e) {
        // clicked on descendant div                    
        e.stopPropagation();
    });

    $("#tab-baiphim").click(function(){
        if(!$(this).hasClass('category-tab-active')){
            $('.category-tab-active').removeClass('category-tab-active');
            $(this).addClass('category-tab-active');
            $('#page3-tab1').show();
            $('#page3-tab2').hide();
            $('#page3-tab3').hide();
        }
        return false;
    });
    $("#tab-DauTu").click(function(){
        if(!$(this).hasClass('category-tab-active')){
            $('.category-tab-active').removeClass('category-tab-active');
            $(this).addClass('category-tab-active');
            $('#page3-tab1').hide();
            $('#page3-tab2').show();
            $('#page3-tab3').hide();
        }
        return false;
    });
    $("#tab-TheoDoi").click(function(){
        if(!$(this).hasClass('category-tab-active')){
            $('.category-tab-active').removeClass('category-tab-active');
            $(this).addClass('category-tab-active');
            $('#page3-tab1').hide();
            $('#page3-tab2').hide();
            $('#page3-tab3').show();
        }
        return false;
    });
	$(".filer-bar-item a").on('click', function (e) {			
		$(".filer-bar-item a").removeClass("active");
		$(this).addClass("active");
		if(!$(this).attr('id')){			
			return false;
		}
	});		
});

