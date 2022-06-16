define(["EditableWindowField", "text!ChoicesCheckBoxesTemplate!strip"], function (EditableWindowField, html) {
    function ChoicesCheckBoxes(options) {
        // Call the super constructor.
        EditableWindowField.call(this, options);

        this.choiceFieldType = options.choiceFieldType;
        this.editWindowContentTemplate = html;
        this.fieldType = "Choice";
        this.editWindowViewModel = null;
        this.isInitialized = false;

        var that = this;
        this.editWindowViewModel = kendo.observable({
            titleText: '',
            groupName: null,
            optionsDataSource: null,
            selectedValue: null
        });

        // Return this object reference.
        return this;
    }

    ChoicesCheckBoxes.prototype = {
        success: function (data, textStatus, jqXHR) {
            this.editWindowViewModel.set("groupName", data.GroupName);
            this.editWindowViewModel.set("optionsDataSource", data.OptionsDataSource);

            this.value = this.getSelectedValues();
            this.originalValue = this.value;

            this.viewModel.set("showEditButton", true);
            kendo.bind(this.dialog.getContentPlaceHolder(), this.editWindowViewModel);
            this.dialog.viewModel.set('titleText', this.editWindowViewModel.titleText);

            this.isInitialized = true;
        },

        onDone: function (event, sender) {
            this.value = this.getSelectedValues();
            this.saveTempParams.data = {
                Name: this.fieldName,
                ComplexValue: this.value
            }

            EditableWindowField.prototype.onDone.call(this, event, sender);
        },
        getSelectedValues: function () {
            var values = [];
            for (var i = 0; i < this.editWindowViewModel.optionsDataSource.length; i++) {
                if (this.editWindowViewModel.optionsDataSource[i].Selected) {
                    values.push({ 'Value': this.editWindowViewModel.optionsDataSource[i].Value });
                }
            }
            return values;
        },
        setSelectedValues: function (values) {
            var that = this;
            for (var i = 0; i < that.editWindowViewModel.optionsDataSource.length; i++) {
                var dataItem = jQuery.grep(values, function (item) {
                    return that.editWindowViewModel.optionsDataSource[i].Value == item.Value;
                })[0];
                if (dataItem) {
                    that.editWindowViewModel.optionsDataSource[i].set('Selected', true);
                } else {
                    that.editWindowViewModel.optionsDataSource[i].set('Selected', false);
                }
            }
        },
        isChanged: function () {
            if (this.value) {
                return !this.value.compare(this.originalValue);
            }
            return false;
        },
        onClose: function (event, sender) {
            this.value = this.originalValue;
            this.setSelectedValues(this.value);
            EditableWindowField.prototype.onClose.call(this, event, sender);
        }
    }

    ChoicesCheckBoxes.prototype = $.extend(Object.create(EditableWindowField.prototype), ChoicesCheckBoxes.prototype);
    return (ChoicesCheckBoxes);
});