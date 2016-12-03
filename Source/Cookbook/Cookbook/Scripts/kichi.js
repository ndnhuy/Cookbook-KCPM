$(function () {
    $(".type-dish > .overlay").mouseenter(function () {
        //if ($(this).is(':animated'))
        //return;
        $(this).css({ opacity: 1 });
        //$(this).animate({ opacity: 1 }, 300);
    });

    $(".type-dish > .overlay").mouseleave(function () {
        $(this).finish();
        $(this).css({ opacity: 0 });
        //$(this).animate({ opacity: 0 }, 300);
    });
});