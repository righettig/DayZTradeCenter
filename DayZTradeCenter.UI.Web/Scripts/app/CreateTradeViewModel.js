Array.prototype.compare = function(testArr) {
    if (this.length != testArr.length) return false;
    for (var i = 0; i < testArr.length; i++) {
        if (this[i].compare) {
            if (!this[i].compare(testArr[i])) return false;
        }
        if (this[i] !== testArr[i]) return false;
    }
    return true;
};

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

    self.isValid = ko.computed(function () {
        if (self.items().length == 0) {
            return false;
        }

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

    self.isHardcore = ko.observable(false);

    self.save = function () {
        var submitData = {
            have: self.have.getItemDetails(),
            want: self.want.getItemDetails(),

            isHardcore: self.isHardcore(),

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
        return self.have.isValid() && self.want.isValid() && !self.sameItems();
    });

    self.sameItems = ko.computed(function () {
        var have = self.have.getItemDetails();
        var want = self.want.getItemDetails();

        var mapFn = function(el) {
            return el.id;
        };

        var filterFn = function(el, index, array) {
            return index == array.indexOf(el);
        };

        var haveItems =
            have.map(mapFn).filter(filterFn);

        var wantItems =
            want.map(mapFn).filter(filterFn);

        if (!haveItems[0] && !wantItems[0]) {
            return false;
        }

        if (haveItems.compare(wantItems)) {
            return true;
        } else {
            return false;
        }
    });

    self.isWithoutHSection = ko.computed(function () {
        return self.have.items().length == 0;
    });

    self.isWithoutWSection = ko.computed(function () {
        return self.want.items().length == 0;
    });

    return self;
}
