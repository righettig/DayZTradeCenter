$(".dropdown-menu li a").click(function () {
    // updates the caption on the combobox
    var selText = $(this).text();
    $(this).parents('.btn-group').find('.dropdown-toggle').html(selText + ' <span class="caret"></span>');

    // assigns the score to the hidden input
    $('#score').val(selText);

    // enables the "save" btn
    $("input[type=submit]").removeAttr("disabled");
})