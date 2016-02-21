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
	});

    adjustHeight = function (el, minHeight) {
        // compute the height difference which is caused by border and outline
        var outerHeight = parseInt(window.getComputedStyle(el).height, 10);
        var diff = outerHeight - el.clientHeight;

        // set the height to 0 in case of it has to be shrinked
        el.style.height = 0;

        // set the correct height
        // el.scrollHeight is the full height of the content, not just the visible part
        el.style.height = Math.max(minHeight, el.scrollHeight + diff) + 'px';
    }


    // we use the "data-adaptheight" attribute as a marker
    var textAreas = [].slice.call(document.querySelectorAll('textarea[data-adaptheight]'));

    // iterate through all the textareas on the page
    textAreas.forEach(function (el) {

        // we need box-sizing: border-box, if the textarea has padding
        el.style.boxSizing = el.style.mozBoxSizing = 'border-box';

        // we don't need any scrollbars, do we? :)
        el.style.overflowY = 'hidden';

        // the minimum height initiated through the "rows" attribute
        var minHeight = el.scrollHeight;

        el.addEventListener('input', function () {
            adjustHeight(el, minHeight);
        });

        // we have to readjust when window size changes (e.g. orientation change)
        window.addEventListener('resize', function () {
            adjustHeight(el, minHeight);
        });

        // we adjust height to the initial content
        adjustHeight(el, minHeight);

    });
});
