﻿Type.registerNamespace("Telerik.Sitefinity.Modules.Forms.Web.UI.Designers.Views");

Telerik.Sitefinity.Modules.Forms.Web.UI.Designers.Views.FormFileUploadLimitationsView = function (element) {
    Telerik.Sitefinity.Modules.Forms.Web.UI.Designers.Views.FormFileUploadLimitationsView.initializeBase(this, [element]);

    this._minRangeField = null;
    this._maxRangeField = null;
    this._allowedFileTypesChoiceField = null;
    this._selectedFileTypesChoiceField = null;
    this._otherExtensionsTextField = null;
    this._rangeViolationMessageField = null;
    this._multipleAttachmentsChoiceField = null;
    this._selectedFileTypesDiv = null;
    this._otherExtensionsDiv = null;

    this._allowedFileTypesChoiceFieldChangedDelegate = null;
    this._selectedFileTypesChoiceFieldChangedDelegate = null;
}

Telerik.Sitefinity.Modules.Forms.Web.UI.Designers.Views.FormFileUploadLimitationsView.prototype = {
    /* --------------------------------- set up and tear down --------------------------------- */
    initialize: function () {
        Telerik.Sitefinity.Modules.Forms.Web.UI.Designers.Views.FormFileUploadLimitationsView.callBaseMethod(this, 'initialize');

        this._allowedFileTypesChoiceFieldChangedDelegate = Function.createDelegate(this, this._allowedFileTypesChoiceFieldChangedHandler);
        this.get_allowedFileTypesChoiceField().add_valueChanged(this._allowedFileTypesChoiceFieldChangedDelegate);

        this._selectedFileTypesChoiceFieldChangedDelegate = Function.createDelegate(this, this._selectedFileTypesChoiceFieldChangedHandler);
        this.get_selectedFileTypesChoiceField().add_valueChanged(this._selectedFileTypesChoiceFieldChangedDelegate);
    },
    dispose: function () {
        if (this._selectedFileTypesChoiceFieldChangedDelegate) {
            if (this.get_allowedFileTypesChoiceField()) {
                this.get_allowedFileTypesChoiceField().remove_valueChanged(this._allowedFileTypesChoiceFieldChangedDelegate);
            }
            delete this._allowedFileTypesChoiceFieldChangedDelegate;
        }
        if (this._selectedFileTypesChoiceFieldChangedDelegate) {
            if (this.get_selectedFileTypesChoiceField()) {
                this.get_selectedFileTypesChoiceField().remove_valueChanged(this._selectedFileTypesChoiceFieldChangedDelegate);
            }
            delete this._selectedFileTypesChoiceFieldChangedDelegate;
        }

        Telerik.Sitefinity.Modules.Forms.Web.UI.Designers.Views.FormFileUploadLimitationsView.callBaseMethod(this, 'dispose');
    },

    /* --------------------------------- public methods --------------------------------- */

    // implementation of IDesignerViewControl: Forces the control to refresh from the control Data
    refreshUI: function () {
        this.get_minRangeField().set_value(this.get_controlData().MinFileSizeInMb);
        this.get_maxRangeField().set_value(this.get_controlData().MaxFileSizeInMb);

        if (this.get_controlData().AllowedFileTypes == Telerik.Sitefinity.Modules.Forms.Web.UI.Designers.Views.AllowedFileTypes.All) {
            this.get_allowedFileTypesChoiceField().set_value("AllFiles");
        }
        else {
            this.get_allowedFileTypesChoiceField().set_value("SelectedFileTypes");

            var selectedTypes = this.get_controlData().AllowedFileTypes.split(",");
            jQuery.each(selectedTypes, function (i, val) {
                selectedTypes[i] = jQuery.trim(val);
            });

            this.get_selectedFileTypesChoiceField().set_value(selectedTypes);

            if (jQuery.inArray(Telerik.Sitefinity.Modules.Forms.Web.UI.Designers.Views.AllowedFileTypes.Other, selectedTypes) > -1) {
                var otherTypes = this.get_controlData().OtherFileTypes ? this.get_controlData().OtherFileTypes.split(";") : null;
                if (jQuery.isArray(otherTypes)) {
                    this.get_otherExtensionsTextField().set_value(otherTypes.join(", "));
                }
                else {
                    this.get_otherExtensionsTextField().set_value("");
                }
            }
        }

        this.get_rangeViolationMessageField().set_value(this.get_controlData().RangeViolationMessage);

        this.get_multipleAttachmentsChoiceField().set_value(this.get_controlData().AllowMultipleAttachments);

        this._updateSelectedFileTypesChoiceFieldVisibility();
        this._updateOtherExtensionsTextFieldVisibility();
    },

    // implementation of IDesignerViewControl: forces the designer view to apply the changes on UI to the control Data
    applyChanges: function () {
        var minSize = parseInt(this.get_minRangeField().get_value());
        if (!isNaN(minSize)) {
            this.get_controlData().MinFileSizeInMb = minSize;
        }
        var maxSize = parseInt(this.get_maxRangeField().get_value());
        if (!isNaN(maxSize)) {
            this.get_controlData().MaxFileSizeInMb = maxSize;
        }

        if (this.get_allowedFileTypesChoiceField().get_value() == "AllFiles") {
            this.get_controlData().AllowedFileTypes = Telerik.Sitefinity.Modules.Forms.Web.UI.Designers.Views.AllowedFileTypes.All;
        }
        else {
            var selectedTypes = this.get_selectedFileTypesChoiceField().get_value();
            if (!jQuery.isArray(selectedTypes)) {
                selectedTypes = [selectedTypes];
            }
            if (jQuery.inArray(Telerik.Sitefinity.Modules.Forms.Web.UI.Designers.Views.AllowedFileTypes.Other, selectedTypes) > -1) {
                var otherTypes = this.get_otherExtensionsTextField().get_value().split(",");
                jQuery.each(otherTypes, function (i, val) {
                    otherTypes[i] = jQuery.trim(val);
                });
                this.get_controlData().OtherFileTypes = otherTypes.join(";");
            }
            selectedTypes = selectedTypes.join(",");
            this.get_controlData().AllowedFileTypes = selectedTypes;
        }

        this.get_controlData().RangeViolationMessage = this.get_rangeViolationMessageField().get_value();

        this.get_controlData().AllowMultipleAttachments = this.get_multipleAttachmentsChoiceField().get_value() === "true";
    },

    // gets the JavaScript control object that is being designed
    get_controlData: function () {
        return this.get_parentDesigner().get_propertyEditor().get_control();
    },

    /* --------------------------------- event handlers --------------------------------- */

    _allowedFileTypesChoiceFieldChangedHandler: function (sender, args) {
        this._updateSelectedFileTypesChoiceFieldVisibility();
    },

    _selectedFileTypesChoiceFieldChangedHandler: function (sender, args) {
        this._updateOtherExtensionsTextFieldVisibility();
    },

    /* --------------------------------- private methods --------------------------------- */

    _updateSelectedFileTypesChoiceFieldVisibility: function () {
        var showSelectFileTypes = this.get_allowedFileTypesChoiceField().get_value() == "SelectedFileTypes";
        jQuery(this.get_selectedFileTypesDiv()).toggle(showSelectFileTypes);

        dialogBase.resizeToContent();
    },

    _updateOtherExtensionsTextFieldVisibility: function () {
        var selectedValues = this.get_selectedFileTypesChoiceField().get_value();
        if (!jQuery.isArray(selectedValues)) {
            selectedValues = [selectedValues];
        }
        var otherSelected = jQuery.inArray(Telerik.Sitefinity.Modules.Forms.Web.UI.Designers.Views.AllowedFileTypes.Other, selectedValues) > -1;
        jQuery(this.get_otherExtensionsDiv()).toggle(otherSelected);

        dialogBase.resizeToContent();
    },

    /* --------------------------------- properties --------------------------------- */

    // IDesignerViewControl: gets the reference for the propertyEditor control
    get_parentDesigner: function () {
        return this._parentDesigner;
    },

    // IDesignerViewControl: sets the reference for the propertyEditor control
    set_parentDesigner: function (value) {
        this._parentDesigner = value;
    },

    // Returns the property editor of the current view
    get_propertyEditor: function () {
        if (this.get_parentDesigner()) {
            return this.get_parentDesigner().get_propertyEditor();
        }
        return null;
    },

    get_minRangeField: function () { return this._minRangeField; },
    set_minRangeField: function (value) { this._minRangeField = value; },

    get_maxRangeField: function () { return this._maxRangeField; },
    set_maxRangeField: function (value) { this._maxRangeField = value; },

    get_allowedFileTypesChoiceField: function () { return this._allowedFileTypesChoiceField; },
    set_allowedFileTypesChoiceField: function (value) { this._allowedFileTypesChoiceField = value; },

    get_selectedFileTypesChoiceField: function () { return this._selectedFileTypesChoiceField; },
    set_selectedFileTypesChoiceField: function (value) { this._selectedFileTypesChoiceField = value; },

    get_otherExtensionsTextField: function () { return this._otherExtensionsTextField; },
    set_otherExtensionsTextField: function (value) { this._otherExtensionsTextField = value; },

    get_rangeViolationMessageField: function () { return this._rangeViolationMessageField; },
    set_rangeViolationMessageField: function (value) { this._rangeViolationMessageField = value; },

    get_multipleAttachmentsChoiceField: function () { return this._multipleAttachmentsChoiceField; },
    set_multipleAttachmentsChoiceField: function (value) { this._multipleAttachmentsChoiceField = value; },

    get_selectedFileTypesDiv: function () { return this._selectedFileTypesDiv; },
    set_selectedFileTypesDiv: function (value) { this._selectedFileTypesDiv = value; },

    get_otherExtensionsDiv: function () { return this._otherExtensionsDiv; },
    set_otherExtensionsDiv: function (value) { this._otherExtensionsDiv = value; }
}

Telerik.Sitefinity.Modules.Forms.Web.UI.Designers.Views.FormFileUploadLimitationsView.registerClass('Telerik.Sitefinity.Modules.Forms.Web.UI.Designers.Views.FormFileUploadLimitationsView', Sys.UI.Control, Telerik.Sitefinity.Web.UI.ControlDesign.IDesignerViewControl);


Telerik.Sitefinity.Modules.Forms.Web.UI.Designers.Views.AllowedFileTypes = function () {
};
Telerik.Sitefinity.Modules.Forms.Web.UI.Designers.Views.AllowedFileTypes.prototype = {
    All: "All",
    Images: "Images",
    Documents: "Documents",
    Audio: "Audio",
    Video: "Video",
    Other: "Other"
};
Telerik.Sitefinity.Modules.Forms.Web.UI.Designers.Views.AllowedFileTypes.registerEnum("Telerik.Sitefinity.Modules.Forms.Web.UI.Designers.Views.AllowedFileTypes");