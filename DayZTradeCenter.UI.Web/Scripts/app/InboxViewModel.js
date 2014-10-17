function Message(id, text, timestamp) {
    var self = this;

    self.id = id;
    self.selected = ko.observable();
    self.text = ko.observable(text);
    self.timestamp = ko.observable(
        moment(timestamp).format("MM/DD/YYYY, h:mm:ss A"));
    
    return self;
}

function InboxViewModel(data) {
    var self = this;

    self.messages = ko.observableArray();
    data.forEach(function(el) {
        self.messages.push(new Message(el.id, el.text, el.timestamp));
    });

    self.allSelected = ko.observable();

    self.toggleSelectAll = function () {
        var state = self.allSelected();

        self.messages().forEach(function (message) {
            message.selected(state);
        });

        return true;
    };

    var atLeastOneSelected = function() {
        return self.messages().some(function(el) {
            return el.selected();
        });
    };

    self.deleteVisible = ko.computed(function() {
        return self.allSelected() || atLeastOneSelected();
    });

    var getAllSelectedMessages = function() {
        return self.messages()
            .filter(function (el) {
                return el.selected();
            })
            .map(function(el) {
                return el.id;
            });
    };

    self.removeMessageById = function(id) {
        self.messages.remove(function(message) {
            return message.id == id;
        });
    };

    self.deleteMessages = function() {
        var messages = {
            ids: getAllSelectedMessages()
        };

        $.ajax({
            type: "POST",
            contentType: "application/x-www-form-urlencoded",
            url: "/Profile/DeleteInboxMessages",
            data: messages
        }).done(function (result) {
            if (result.success) {
                messages.ids.forEach(function(id) {
                    self.removeMessageById(id);
                });

                alertify.success('Ok');
            } else {
                alertify.error(result.error);
            }
        }).error(function (ex) {
            alertify.error("Unknown error! " + ex);
        });
    };

    return self;
}
