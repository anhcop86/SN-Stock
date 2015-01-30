$(document).ready(function () {

    //quickSearch
    $('#quickSearch a.Toggle').click(function () {
        $(this).next('fieldset').toggle();
        if ($(this).attr('title') == 'Collapse') {
            $(this).attr('title', 'Expand')
                .html('Expand')
                .toggleClass('Invert');
        }
        else {
            $(this).attr('title', 'Collapse')
                .html('Collapse')
                .toggleClass('Invert');
        }
        return false;
    });

    //nav
    $('#nav > li').each(function () {

        var subList = $(this).children('ul');
        if (subList.length > 0) {
            subList.hide();
            $(this).addClass('HasChild')
                            .click(function () {                                
                                subList.toggle();
                                $(this).toggleClass('HasChild');
                            });
        }
        var selectedfeature = $.cookie("selectmenu");
        
        if (selectedfeature != null) {
            if (selectedfeature.indexOf($(this).attr("id")) >= 0) {
                $(this).removeClass('HasChild');
                var selectedSubList = $(this).children('ul');
                if (selectedSubList.length > 0) {
                    selectedSubList.toggle();
                }
            }
        }

    });

    //tabs
    $('#tab li, #subtab li, #content .Panel .Pager li, #content .Table table tr').hover(
        function () { $(this).addClass('Over'); },
        function () { $(this).removeClass('Over'); }
    );

    //functions
    $('#content .Panel .Functions li').each(function () {
        var subList = $(this).children('ul');
        if (subList.length > 0) {
            $(this).addClass('HasChild')
                .hover(
                    function () { subList.show(); },
                    function () { subList.hide(); }
                );
        }
    });

    //theme
    $('#header #themes li a').each(function () {
        $(this).click(function () {
            styleSwitcher($(this).attr('rel'));
            return false;
        });
    });
    
    $('#nav li ul li').each(function () {        
        $(this).click(function () {        
            $.cookie("selectmenu", $(this).attr("id"));
        });
    });   
});

//styleSwitcher
function styleSwitcher(styleName){
    $('link[title]').each(function(i){
        this.disabled = true;
        if ($(this).attr('title') == styleName) this.disabled = false;
    });
}