Type.registerNamespace("Telerik.Sitefinity.Modules.Forms.Web.UI.Designers.Views");

Telerik.Sitefinity.Modules.Forms.Web.UI.Designers.Views.FormFileUploadLabelView = function (element) {
    Telerik.Sitefinity.Modules.Forms.Web.UI.Designers.Views.FormFileUploadLabelView.initializeBase(this, [element]);
    this._labelTextField = null;
    this._requiredChoiceField = null;

    this._instructionsTextField = null;
    this._errorMessageTextField = null;
    this._defaultRequiredMessage = null;
    this._metaFieldNameTextBox = null;

    this._requiredChoiceFieldValueChangedDelegate = null;
}

Telerik.Sitefinity.Modules.Forms.Web.UI.Designers.Views.FormFileUploadLabelView.prototype = {
    /* --------------------------------- set up and tear down --------------------------------- */
    initialize: function () {
        Telerik.Sitefinity.Modules.Forms.Web.UI.Designers.Views.FormFileUploadLabelView.callBaseMethod(this, 'initialize');

        this._requiredChoiceFieldValueChangedDelegate = Function.createDelegate(this, this._requiredChoiceFieldValueChangedHandler);
        this._requiredChoiceField.add_valueChanged(this._requiredChoiceFieldValueChangedDelegate);
    },

    dispose: function () {
        if (this._requiredChoiceFieldValueChangedDelegate) {
            if (this._requiredChoiceField) {
                this._requiredChoiceField.remove_valueChanged(this._requiredChoiceFieldValueChangedDelegate);
            }
        }

        Telerik.Sitefinity.Modules.Forms.Web.UI.Designers.Views.FormFileUploadLabelView.callBaseMethod(this, 'dispose');
    },

    /* --------------------------------- public methods --------------------------------- */

    // implementation of IDesignerViewControl: Forces the control to refresh from the control Data
    refreshUI: function () {
        var controlData = this.get_controlData();

        this._labelTextField.set_value(controlData.Title);
        this._instructionsTextField.set_value(controlData.Example);

        if (controlData.ValidatorDefinition.Required) {
            this._requiredChoiceField.set_value(true);
            jQuery(this._errorMessageTextField.get_element()).show();
            this._errorMessageTextField.set_value(controlData.ValidatorDefinition.RequiredViolationMessage);
        }
        else {
            this._requiredChoiceField.set_value(false);
            this._errorMessageTextField.set_value(this._defaultRequiredMessage);
            jQuery(this._errorMessageTextField.get_element()).hide();
        }
    },

    // implementation of IDesignerViewControl: forces the designer view to apply the changes on UI to the control Data
    applyChanges: function () {
        var controlData = this.get_controlData();
        controlData.Title = this._labelTextField.get_value();
        controlData.Example = this._instructionsTextField.get_value();

        var metaFieldName = this.get_metaFieldNameTextBox();
        if (metaFieldName && !metaFieldName.get_readOnly() && controlData.MetaField) {
            controlData.MetaField.FieldName = metaFieldName.get_value();
        }
        if (this._requiredChoiceField.get_value() === "true") {
            controlData.ValidatorDefinition.Required = true;
            controlData.ValidatorDefinition.RequiredViolationMessage = this._errorMessageTextField.get_value();
        }
        else {
            controlData.ValidatorDefinition.Required = false;
        }
    },

    // gets the JavaScript control object that is being designed
    get_controlData: function () {
        return this.get_parentDesigner().get_propertyEditor().get_control();
    },

    /* --------------------------------- event handlers --------------------------------- */

    // Handles the required choice field change
    _requiredChoiceFieldValueChangedHandler: function (sender) {
        if (this._requiredChoiceField.get_value() === "true") {
            jQuery(this._errorMessageTextField.get_element()).show();
        }
        else {
            jQuery(this._errorMessageTextField.get_element()).hide();
        }
        dialogBase.resizeToContent();
    },

    /* --------------------------------- properties --------------------------------- */

    // IDesignerViewControl: gets the reference for the propertyEditor control
    get_parentDesigner: function () { return this._parentDesigner; },

    // IDesignerViewControl: sets the reference for the propertyEditor control
    set_parentDesigner: function (value) { this._parentDesigner = value; },

    // Returns the property editor of the current view
    get_propertyEditor: function () {
        if (this.get_parentDesigner()) {
            return this.get_parentDesigner().get_propertyEditor();
        }
        return null;
    },

    // Gets the textfield for the label of the control
    get_labelTextField: function () { return this._labelTextField; },
    // Sets the textfield for the label of the control
    set_labelTextField: function (value) { this._labelTextField = value; },

    // Gets the choicefield for setting the required option
    get_requiredChoiceField: function () { return this._requiredChoiceField; },
    // Sets the choicefield for setting the required option
    set_requiredChoiceField: function (value) { this._requiredChoiceField = value; },

    // Gets the textfield for the instructions text
    get_instructionsTextField: function () { return this._instructionsTextField; },
    // Sets the textfield for the instructions text
    set_instructionsTextField: function (value) { this._instructionsTextField = value; },

    // Gets the textfield for the error message
    get_errorMessageTextField: function () { return this._errorMessageTextField; },
    // Sets the textfield for the error message
    set_errorMessageTextField: function (value) { this._errorMessageTextField = value; },

    get_metaFieldNameTextBox: function () { return this._metaFieldNameTextBox; },
    set_metaFieldNameTextBox: function (value) { this._metaFieldNameTextBox = value; },

    get_defaultRequiredMessage: function () { return this._defaultRequiredMessage; },
    set_defaultRequiredMessage: function (value) { this._defaultRequiredMessage = value; }
}

Telerik.Sitefinity.Modules.Forms.Web.UI.Designers.Views.FormFileUploadLabelView.registerClass('Telerik.Sitefinity.Modules.Forms.Web.UI.Designers.Views.FormFileUploadLabelView', Sys.UI.Control, Telerik.Sitefinity.Web.UI.ControlDesign.IDesignerViewControl);
