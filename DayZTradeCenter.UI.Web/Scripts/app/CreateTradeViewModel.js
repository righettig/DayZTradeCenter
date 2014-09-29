function TradeItem(name, quantity) {
    var self = this;

    self.name = ko.observable(name);
    self.quantity = ko.observable(quantity);

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

    self.item_id = ko.observable();

    return self;
}

function CreateTradeViewModel(data) {
    var self = this;

    self.gameItems = data;
        
    self.quantities = [1, 2, 3, 4, 5];

    self.have = new ItemsCollection();

    self.want = new ItemsCollection();

    self.save = function () {
        // TODO: use real data from the view model.
        var submitData = {
            // STUB
            have: [
                { id: 2, quantity: 1 },
                { id: 4, quantity: 3 }
            ],
            want: [
                { id: 1, quantity: 2 },
                { id: 5, quantity: 1 }
            ],
            
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

    return self;
}
