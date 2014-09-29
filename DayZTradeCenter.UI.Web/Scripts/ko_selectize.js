var inject_binding = function (allBindings, key, value) {
    //https://github.com/knockout/knockout/pull/932#issuecomment-26547528
    return {
        has: function (bindingKey) {
            return (bindingKey == key) || allBindings.has(bindingKey);
        },
        get: function (bindingKey) {
            var binding = allBindings.get(bindingKey);
            if (bindingKey == key) {
                binding = binding ? [].concat(binding, value) : value;
            }
            return binding;
        }
    };
}

ko.bindingHandlers.selectize = {
    init: function (element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {
        if (!allBindingsAccessor.has('optionsText'))
            allBindingsAccessor = inject_binding(allBindingsAccessor, 'optionsText', 'name');
        if (!allBindingsAccessor.has('optionsValue'))
            allBindingsAccessor = inject_binding(allBindingsAccessor, 'optionsValue', 'id');
        if (typeof allBindingsAccessor.get('optionsCaption') == 'undefined')
            allBindingsAccessor = inject_binding(allBindingsAccessor, 'optionsCaption', 'Choose...');

        ko.bindingHandlers.options.update(element, valueAccessor, allBindingsAccessor, viewModel, bindingContext);

        var options = {
            valueField: allBindingsAccessor.get('optionsValue'),
            labelField: allBindingsAccessor.get('optionsText'),
            searchField: allBindingsAccessor.get('optionsText')
        }

        if (allBindingsAccessor.has('options')){
            var passed_options = allBindingsAccessor.get('options')
            for (var attr_name in passed_options) 
                { options[attr_name] = passed_options[attr_name]; }
        }

        var $select = $(element).selectize(options)[0].selectize;
        if (typeof allBindingsAccessor.get('value') == 'function')
            $select.addItem(allBindingsAccessor.get('value')());
        var selected = allBindingsAccessor.get('selectedOptions');
        if (selected) {
            for (var k in selected) {
                $select.addItem(selected[k]);
            }
        }

        if (typeof init_selectize == 'function') {
            init_selectize($select);
        }

        if (typeof valueAccessor.subscribe == 'function') {
            valueAccessor().subscribe(function (new_value) {
                var new_obj = new_value[new_value.length - 1];
                $select.addOption(new_obj);
            });
        }
    },
    update: function (element, valueAccessor, allBindingsAccessor) {
        if (allBindingsAccessor.has('object')) {
            var value_accessor = valueAccessor();
            var selected_obj = $.grep(value_accessor(), function (i) {
                if (typeof i.id == 'function')
                    var id = i.id()
                else
                    var id = i.id
                return id == allBindingsAccessor.get('value')();
            })[0];

            if (selected_obj) {
                allBindingsAccessor.get('object')(selected_obj);
            }
        }
    }
}