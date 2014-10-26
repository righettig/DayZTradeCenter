function deleteTrade(id, el) {
    var tableRow =
        $(el).parent().parent();

    alertify.confirm("Do you really want to delete the trade?",
        function () {
            var submitData = {
                tradeId: id,
                __RequestVerificationToken: $("input[name='__RequestVerificationToken']").val()
            };

            $.ajax({
                type: "POST",
                contentType: "application/x-www-form-urlencoded",
                url: "/Trades/Delete",
                data: submitData
            }).done(function (result) {
                if (result.success) {
                    // updates the total number of items.
                    var oldTradesCount = $('#totalTradeCount').text();
                    $('#totalTradeCount').text(oldTradesCount - 1);

                    // deletes the corresponding row of the table.
                    tableRow.remove();

                    // enables the Add btn & hides the alert message
                    $('#addTradeBtn').removeClass("disabled");
                    $('.text-danger').hide();

                    alertify.success('Ok');
                } else {
                    alertify.error(result.error);
                }
            }).error(function (ex) {
                alertify.error("Unknown error! " + ex);
            });
        }).setting('movable', false);
}

$(function () {
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