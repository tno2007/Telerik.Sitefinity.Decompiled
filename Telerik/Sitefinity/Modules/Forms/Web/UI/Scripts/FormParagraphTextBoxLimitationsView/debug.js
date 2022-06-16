﻿Type.registerNamespace("Telerik.Sitefinity.Modules.Forms.Web.UI.Designers.Views");

Telerik.Sitefinity.Modules.Forms.Web.UI.Designers.Views.FormParagraphTextBoxLimitationsView = function (element) {
    Telerik.Sitefinity.Modules.Forms.Web.UI.Designers.Views.FormParagraphTextBoxLimitationsView.initializeBase(this, [element]);
}

Telerik.Sitefinity.Modules.Forms.Web.UI.Designers.Views.FormParagraphTextBoxLimitationsView.prototype = {
    /* --------------------------------- set up and tear down --------------------------------- */
    initialize: function () {
        Telerik.Sitefinity.Modules.Forms.Web.UI.Designers.Views.FormParagraphTextBoxLimitationsView.callBaseMethod(this, 'initialize');
    },
    dispose: function () {
        Telerik.Sitefinity.Modules.Forms.Web.UI.Designers.Views.FormParagraphTextBoxLimitationsView.callBaseMethod(this, 'dispose');
    },

    /* --------------------------------- public methods --------------------------------- */

    // implementation of IDesignerViewControl: Forces the control to refersh from the control Data
    refreshUI: function () {
        var controlData = this.get_controlData();
        if (controlData.ValidatorDefinition && controlData.ValidatorDefinition.MinLength) {
            this._minRangeField.set_value(controlData.ValidatorDefinition.MinLength);
        }
        else {
            this._minRangeField.set_value("0");
        }
        if (controlData.ValidatorDefinition && controlData.ValidatorDefinition.MaxLength) {
            this._maxRangeField.set_value(controlData.ValidatorDefinition.MaxLength);
        }
        else {
            this._maxRangeField.reset();
        }
        if (controlData.ValidatorDefinition.MaxLengthViolationMessage) {
            this._rangeViolationMessageField.set_value(controlData.ValidatorDefinition.MaxLengthViolationMessage);
        }
    },

    // implementation of IDesignerViewControl: forces the designer view to apply the changes on UI to the control Data
    applyChanges: function () {
        var controlData = this.get_controlData();
        controlData.ValidatorDefinition.MinLength = this._minRangeField.get_value() ? this._minRangeField.get_value() : 0;
        controlData.ValidatorDefinition.MaxLength = this._maxRangeField.get_value() ? this._maxRangeField.get_value() : 0;
        if (this._rangeViolationMessageField) {
            controlData.ValidatorDefinition.MaxLengthViolationMessage = this._rangeViolationMessageField.get_value();
            controlData.ValidatorDefinition.MinLengthViolationMessage = this._rangeViolationMessageField.get_value();
        }
    },

    // gets the javascript control object that is being designed
    get_controlData: function () {
        return this.get_parentDesigner().get_propertyEditor().get_control();
    },

    /* --------------------------------- event handlers --------------------------------- */

    /* --------------------------------- private methods --------------------------------- */

    /* --------------------------------- properties --------------------------------- */

    // IDesignerViewControl: gets the reference fo the propertyEditor control
    get_parentDesigner: function () { return this._parentDesigner; },

    // IDesignerViewControl: sets the reference fo the propertyEditor control
    set_parentDesigner: function (value) { this._parentDesigner = value; },

    // Returns the property editor of the current view
    get_propertyEditor: function () {
        if (this.get_parentDesigner()) {
            return this.get_parentDesigner().get_propertyEditor();
        }
        return null;
    },

    //Gets the minRangeField component.
    get_minRangeField: function () {
        return this._minRangeField;
    },
    //Sets the minRangeField component.
    set_minRangeField: function (value) {
        this._minRangeField = value;
    },

    //Gets the maxRangeField component.
    get_maxRangeField: function () {
        return this._maxRangeField;
    },
    //Sets the maxRangeField component.
    set_maxRangeField: function (value) {
        this._maxRangeField = value;
    },

    //Gets the rangeVioalationMessageField component.
    get_rangeViolationMessageField: function () {
        return this._rangeViolationMessageField;
    },
    //Sets the rangeVioalationMessageField component.
    set_rangeViolationMessageField: function (value) {
        this._rangeViolationMessageField = value;
    }
}

Telerik.Sitefinity.Modules.Forms.Web.UI.Designers.Views.FormParagraphTextBoxLimitationsView.registerClass('Telerik.Sitefinity.Modules.Forms.Web.UI.Designers.Views.FormParagraphTextBoxLimitationsView', Sys.UI.Control, Telerik.Sitefinity.Web.UI.ControlDesign.IDesignerViewControl);