﻿define(["EditableField"], function (EditableField) {
    function EditableShortText(options) {
        // Call the super constructor.
        EditableField.call(this, options);

        // Return this object reference.
        return (this);
    }
    
    EditableShortText.prototype = {
        showEditMode: function() {
            this.element.attr('contenteditable', true);
            $(this.element).focus();
        },

        disableEditing: function () {
            this.element.removeAttr('contenteditable');
        },

        isChanged: function () {
            if (this.isInEdit) {
                this.value = this.element.html();
                var container = $('<div>').html(this.value);
                if (container.find('[data-sf-ignore]').remove().length > 0) {
                    this.value = container.text();
                }
                else {
                    this.value = this.element.text();
                }
                this.value = $.trim(this.value);
            }
            return this.value !== this.originalValue;
        }
    };

    EditableShortText.prototype = $.extend(Object.create(EditableField.prototype), EditableShortText.prototype);
    return (EditableShortText);
});