$(function () {
    $('.trade').click(function () {
        var trade = $(this).attr("data-target");
        location.href = trade;
    });
})