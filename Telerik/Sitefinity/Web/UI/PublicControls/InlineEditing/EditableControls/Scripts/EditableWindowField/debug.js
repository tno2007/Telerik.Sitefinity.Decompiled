define(["EditableField", "DialogBase", "text!EditableWindowFieldTemplate!strip"], function (EditableField, DialogBase, template) {
    function EditableWindowField(options) {
        var that = this;

        // Call the super constructor.
        EditableField.call(this, options);

        this.dialog = new DialogBase();
        this.fieldType = null;
        this.editWindowViewModel = null;
        this.editWindowContentTemplate = null;

        this.itemType = options.itemType;
        this.itemId = options.itemId;
        this.providerName = options.providerName;
        this.contentRepository = options.contentRepository;
        this.isInitialized = false;
        this.originalValue = null;
        this.value = null;

        this.viewModel = kendo.observable({
            edit: function (e) {
                e.preventDefault();
                that.edit();
            },
            showEditButton: false
        });
        var templatePresent = this.element.find('[data-sf-template=true]').length > 0;
        if (!templatePresent) {
            this.element.append(template);
        }
        kendo.bind(this.element, this.viewModel);

        // Return this object reference.
        return (this);
    }

    EditableWindowField.prototype = {
        initializeEditWindow: function () {
            $(this.dialog.getContentPlaceHolder()).html(this.editWindowContentTemplate);
            $(this.dialog).on("doneSelected", jQuery.proxy(this.onDone, this));
            $(this.dialog).on("closeSelected", jQuery.proxy(this.onClose, this));
        },

        showEditMode: function () {
            if (!this.isInitialized) {
                this.initializeEditWindow();
                this.contentRepository.loadFieldValues(this, Function.createDelegate(this, this.success));
            }
            else {
                this.viewModel.set("showEditButton", true);
            }
        },

        edit: function () {
            this.dialog.open();
        },

        editWindowSave: function (field) {
            $(this).trigger('saveTempField', field);
        },

        closeEditMode: function () {
            var that = this;

            if (this.isInEdit) {
                if (that.dialog) {
                    that.dialog.close();
                }
                this.isInEdit = false;
                this.viewModel.set("showEditButton", false);
            }
        },

        updateHtml: function (html) {
            this.element.html(html);
            this.element.append(template);
            kendo.bind(this.element, this.viewModel);
        },

        cancelChanges: function () {
            this.closeEditMode();
        },

        needsReRender: function () {
            return true;
        },

        onDone: function (event, sender) {
            this.editWindowSave(this);
            sender.close();
        },

        onClose: function(event, sender) {
            sender.close();
        },

        unselectField: function () {
            $(this.element).removeClass('sfFieldEditMode').addClass('sfFieldEditable');
            this.viewModel.set("showEditButton", false);
        }
    };

    EditableWindowField.prototype = $.extend(Object.create(EditableField.prototype), EditableWindowField.prototype);
    return (EditableWindowField);
});