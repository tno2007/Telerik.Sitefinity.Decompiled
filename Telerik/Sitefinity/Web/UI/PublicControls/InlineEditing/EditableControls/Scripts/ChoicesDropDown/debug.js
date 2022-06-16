define(["EditableWindowField", "text!ChoicesDropDownTemplate!strip"], function (EditableWindowField, html) {
    function ChoicesDropDown(options) {
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

    ChoicesDropDown.prototype = {
        success: function (data, textStatus, jqXHR) {
            this.editWindowViewModel.set("groupName", data.GroupName);
            this.editWindowViewModel.set("optionsDataSource", data.OptionsDataSource);
            var value = null;
            for (var i = 0; i < data.OptionsDataSource.length; i++) {
                if (data.OptionsDataSource[i].Selected) {
                    value = data.OptionsDataSource[i].Value;
                    break;
                }
            }
            this.editWindowViewModel.set("selectedValue", value);
            this.originalValue = value;
            this.value = this.originalValue;
            kendo.bind(this.dialog.getContentPlaceHolder(), this.editWindowViewModel);
            this.dialog.viewModel.set('titleText', this.editWindowViewModel.titleText);
            
            this.viewModel.set("showEditButton", true);
            this.isInitialized = true;
        },

        onDone: function (event, sender) {
            this.value = this.editWindowViewModel.selectedValue;
            EditableWindowField.prototype.onDone.call(this, event, sender);
        },

        onClose: function (event, sender) {
            this.editWindowViewModel.set('selectedValue', this.originalValue);
            EditableWindowField.prototype.onClose.call(this, event, sender);
        }
    }

    ChoicesDropDown.prototype = $.extend(Object.create(EditableWindowField.prototype), ChoicesDropDown.prototype);
    return (ChoicesDropDown);
});