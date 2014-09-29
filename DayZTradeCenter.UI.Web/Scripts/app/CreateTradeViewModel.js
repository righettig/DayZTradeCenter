function TradeItem(id, quantity) {
    var self = this;

    self.id = ko.observable(id);
    self.quantity = ko.observable(quantity);

    self.isValid = ko.computed(function() {
        return self.id() !== "" && self.quantity() > 0;
    });

    return self;
}

function ItemsCollection() {
    var self = this;

    self.items = ko.observableArray([
        new TradeItem("", 1)
    ]);

    self.add = function() {
        self.items.push(new TradeItem("", 0));
    };

    self.canAdd = ko.computed(function() {
        return self.items().length < 5;
    });

    self.remove = function (item) {
        self.items.remove(item);
    };

    self.isValid = ko.computed(function() {
        for (var i = 0; i < self.items().length; i++) {
            if (!self.items()[i].isValid()) {
                return false;
            }
        }

        return true;
    });

    self.getItemDetails = function() {
        var result = [];
        for (var i = 0; i < self.items().length; i++) {
            var item = self.items()[i];
            result.push({ id: item.id(), quantity: item.quantity() });
        }
        return result;
    };

    return self;
}

function CreateTradeViewModel(data) {
    var self = this;

    self.gameItems = data;
        
    self.quantities = [1, 2, 3, 4, 5];

    self.have = new ItemsCollection();

    self.want = new ItemsCollection();

    self.save = function () {
        var submitData = {
            have: self.have.getItemDetails(),
            want: self.want.getItemDetails(),

            // STUB
            //have: [
            //    { id: 2, quantity: 1 },
            //    { id: 4, quantity: 3 }
            //],
            //want: [
            //    { id: 1, quantity: 2 },
            //    { id: 5, quantity: 1 }
            //],
            
            // invalid trade case #1
            //have: [
            //    { id: 2, quantity: 1 }
            //],
            //want: [
            //    { id: 2, quantity: 2 }
            //],
            
            // invalid trade case #2
            //have: [
            //    { id: 2, quantity: 1 },
            //    { id: 1, quantity: 7 }
            //],
            //want: [
            //    { id: 1, quantity: 4 },
            //    { id: 2, quantity: 2 }
            //],

            // <-- end STUB
            __RequestVerificationToken: $("input[name='__RequestVerificationToken']").val()
        };
        $.ajax({
            type: "POST",
            contentType: "application/x-www-form-urlencoded",
            url: "/Trades/Create",
            data: submitData
        }).done(function(result) {
            if (result.success) {
                document.location.href = '/Trades';
            } else {
                alert(result.error);
            }
        }).error(function(ex) {
            alert("Unknown error! " + ex);
        });
    };

    self.canSave = ko.computed(function() {
        return self.have.isValid() && self.want.isValid();
    });

    return self;
}
