/* 
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
$(document).ready(function () {
    $('input').on('focus', function () {
		$(this).parent().addClass('search_active');
	}).focusout(function(){
		$(this).parent().removeClass('search_active');
	}) ;
});
