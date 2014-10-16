function deleteItem(name, id) {
    var tableRow = $("td").filter(function() {
        return $(this).text() == name;
    }).parent("tr");

    alertify.confirm("Do you really want to delete the item '" + name + "'?",
        function() {
            var submitData = {
                id: id,
                __RequestVerificationToken: $("input[name='__RequestVerificationToken']").val()
            };

            $.ajax({
                type: "POST",
                contentType: "application/x-www-form-urlencoded",
                url: "/Items/Delete",
                data: submitData
            }).done(function(result) {
                if (result.success) {
                    // updates the total number of items.
                    var oldItemsCount = $('#totalItemCount').text();
                    $('#totalItemCount').text(oldItemsCount - 1);

                    // deletes the corresponding row of the table.
                    tableRow.remove();
                    
                    alertify.success('Ok');
                } else {
                    alertify.error(result.error);
                }
            }).error(function(ex) {
                alertify.error("Unknown error! " + ex);
            });
        }).setting('movable', false);
}