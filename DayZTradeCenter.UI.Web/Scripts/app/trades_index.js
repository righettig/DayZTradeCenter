$(function() {
    $('.search-panel .dropdown-menu').find('a').click(function(e) {
        e.preventDefault();
        var param = $(this).attr("href").replace("#", "");
        var concept = $(this).text();
        $('.search-panel span#search_concept').text(concept);
        $('#searchType').val(param);
    });

    $('#items').selectize({
        create: true,
        sortField: 'text'
    });

    // resets the search params before posting
    $('#resetBtn').click(function() {
        $('select[name=itemId]').val(null);
        $('#searchType').val(null);
    });
})