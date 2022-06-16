define(["EditableWindowField", "text!DateTimeTemplate!strip"], function (EditableWindowField, html) {
    function EditableDateTime(options) {
        // Call the super constructor.
        EditableWindowField.call(this, options);

        this.fieldType = "DateTime";
        this.editWindowContentTemplate = html;
        this.dateChangedParams = null;
        this.editWindowViewModel = kendo.observable({
            titleText: '',
            value: ""
        });

        // Return this object reference.
        return (this);
    }

    EditableDateTime.prototype = {
        success: function (data, textStatus, jqXHR) {
            try {
                this.originalValue = kendo.parseDate(data);
            }
            catch(e) {
                this.originalValue = new Date();
            }
            this.value = this.originalValue;
            this.editWindowViewModel.set("value", this.value);

            if (!this.isInitialized) {
                kendo.bind(this.dialog.getContentPlaceHolder(), this.editWindowViewModel);
                this.dialog.viewModel.set('titleText', this.editWindowViewModel.titleText);
                this.isInitialized = true;
            }

            this.viewModel.set("showEditButton", true);
        },
        onDone: function (event, sender) {
            this.value = this.editWindowViewModel.value;
            //This is required to apply validation on this field after the apply changes method is called
            this.dateChangedParams = (this.value === null) ? "" : this.value;
            EditableWindowField.prototype.onDone.call(this, event, sender);
        },
        onClose: function (event, sender) {
            this.editWindowViewModel.set("value", this.value);
            EditableWindowField.prototype.onClose.call(this, event, sender);
        }
    };

    EditableDateTime.prototype = $.extend(Object.create(EditableWindowField.prototype), EditableDateTime.prototype);
    return (EditableDateTime);
});