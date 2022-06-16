﻿define(["EditableWindowField", "text!YesNoTemplate!strip"], function (EditableWindowField, html) {
    function EditableYesNo(options) {
        // Call the super constructor.
        EditableWindowField.call(this, options);

        this.fieldType = "YesNo";
        this.editWindowContentTemplate = html;

        this.editWindowViewModel = kendo.observable({
            titleText: '',
            isChecked: false,
            currentValue: ""
        });

        // Return this object reference.
        return (this);
    }

    EditableYesNo.prototype = {
        success: function (data, textStatus, jqXHR) {
            this.editWindowViewModel.set("isChecked", data.Selected);
            this.editWindowViewModel.set("currentValue", data.Text);
            this.originalValue = data.Selected;
            this.value = this.originalValue;
            
            kendo.bind(this.dialog.getContentPlaceHolder(), this.editWindowViewModel);
            this.dialog.viewModel.set('titleText', this.editWindowViewModel.titleText);
            this.isInitialized = true;
            
            this.viewModel.set("showEditButton", true);
        },
        onDone: function (event, sender) {
            this.value = this.editWindowViewModel.isChecked;
            EditableWindowField.prototype.onDone.call(this, event, sender);
        },

        onClose: function (event, sender) {
            this.value = this.originalValue;
            this.editWindowViewModel.set("isChecked", this.originalValue);
            EditableWindowField.prototype.onClose.call(this, event, sender);
        }
    };

    EditableYesNo.prototype = $.extend(Object.create(EditableWindowField.prototype), EditableYesNo.prototype);
    return (EditableYesNo);
});