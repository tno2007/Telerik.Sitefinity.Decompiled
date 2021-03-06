define([], function () {
    function EditableField(options) {
        this.isInEdit = false;
        this.saveTempParams = {};
        this.fieldName = options.fieldName;
        this.element = options.element;
        this.originalValue = $.trim(this.element.html());
        this.value = this.originalValue;
        this.siteBaseUrl = options.siteBaseUrl;
    }

    EditableField.prototype = {
        enableEditing: function () {
            var that = this;
            this.element.unbind('click.editableField');
            this.element.bind('click.editableField', function (e) {
                that.focusAndShowEditMode();
            });
            this.element.unbind('clickoutside.editableField');
            this.element.bind('clickoutside.editableField', function (e) {
                var clickedItem = $(e.target);
                if (clickedItem.hasClass('k-overlay') ||
                    clickedItem.hasClass('sfPreventClickOutside') || clickedItem.parents('.sfPreventClickOutside').length ||
                    clickedItem.hasClass('k-animation-container') || clickedItem.parents('.k-animation-container').length) {
                    return;
                }
                that.unselectField();
            });
        },

        disableEditing: function () {
            this.element.unbind('click.editableField');
            this.element.unbind('clickoutside.editableField');
        },

        cancelChanges: function () {
            this.value = this.originalValue;
            this.element.html(this.originalValue);
            this.isInEdit = false;
        },

        applyChanges: function () {
            this.originalValue = this.value;
        },

        isChanged: function () {
            return this.value !== this.originalValue;
        },

        needsReRender: function () {
            return false;
        },

        unselectField: function () {
            $(this.element).removeClass('sfFieldEditMode').addClass('sfFieldEditable');
        },

        focusAndShowEditMode: function () {
            this.showEditMode();
            this.isInEdit = true;
            if (!$(this.element).hasClass('sfFieldEditMode')) {
                $(this.element).addClass('sfFieldEditMode').removeClass('k-state-active sfFieldEditable');
            }
        }
    }

    return (EditableField);
});