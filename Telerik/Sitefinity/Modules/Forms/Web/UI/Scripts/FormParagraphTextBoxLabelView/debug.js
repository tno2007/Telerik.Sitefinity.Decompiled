/// <reference name="MicrosoftAjax.js"/>
/// <reference name="Telerik.Sitefinity.Resources.Scripts.jquery-1.6.3-vsdoc.js" assembly="Telerik.Sitefinity.Resources"/>
Type.registerNamespace("Telerik.Sitefinity.Modules.Forms.Web.UI.Designers.Views");

Telerik.Sitefinity.Modules.Forms.Web.UI.Designers.Views.FormParagraphTextBoxLabelView = function (element) {
    Telerik.Sitefinity.Modules.Forms.Web.UI.Designers.Views.FormParagraphTextBoxLabelView.initializeBase(this, [element]);
    this._labelTextField = null;
    this._requiredChoiceField = null;
    this._instructionsTextField = null;
    this._predefinedValueTextField = null;
    this._errorMessageTextField = null;
    this._defaultRequiredMessage = null;
    this._metaFieldNameTextBox = null;
    this._defaultValueValidationMessage = null;

    this._requiredChoiceFieldValueChangedDelegate = null;
    this._pageLoadDelegate = null;
}

Telerik.Sitefinity.Modules.Forms.Web.UI.Designers.Views.FormParagraphTextBoxLabelView.prototype = {
    /* --------------------------------- set up and tear down --------------------------------- */
    initialize: function () {
        Telerik.Sitefinity.Modules.Forms.Web.UI.Designers.Views.FormParagraphTextBoxLabelView.callBaseMethod(this, 'initialize');

        this._requiredChoiceFieldValueChangedDelegate = Function.createDelegate(this, this._requiredChoiceFieldValueChangedHandler);
        this._requiredChoiceField.add_valueChanged(this._requiredChoiceFieldValueChangedDelegate);
        this._pageLoadDelegate = Function.createDelegate(this, this._pageLoadHandler);
        Sys.Application.add_load(this._pageLoadDelegate);
    },

    dispose: function () {
        Telerik.Sitefinity.Modules.Forms.Web.UI.Designers.Views.FormParagraphTextBoxLabelView.callBaseMethod(this, 'dispose');
        Sys.Application.remove_load(this._pageLoadDelegate);
        if (this._requiredChoiceFieldValueChangedDelegate) {
            if (this._requiredChoiceField) {
                this._requiredChoiceField.remove_valueChanged(this._requiredChoiceFieldValueChangedDelegate);
            }
        }
    },

    /* --------------------------------- public methods --------------------------------- */

    // implementation of IDesignerViewControl: Forces the control to refresh from the control Data
    refreshUI: function () {
        var controlData = this.get_controlData();

        this._labelTextField.set_value(controlData.Title);
        this._instructionsTextField.set_value(controlData.Example);
        this._predefinedValueTextField.set_value(controlData.DefaultStringValue);
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

        controlData.DefaultStringValue = this._predefinedValueTextField.get_value();
        if (this._requiredChoiceField.get_value() === "true") {
            controlData.ValidatorDefinition.Required = true;
            controlData.ValidatorDefinition.RequiredViolationMessage = this._errorMessageTextField.get_value();
        }
        else {
            controlData.ValidatorDefinition.Required = false;
        }

    },
    // gets the javascript control object that is being designed
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
    // Handles the page load event
    _pageLoadHandler: function () {
        this._beforeSaveChangesDelegate = Function.createDelegate(this, this._beforeSaveChangesHandler);
        this.get_propertyEditor().add_beforeSaveChanges(this._beforeSaveChangesDelegate);
    },

    // the event is fired when the user chooses save, before the data processing
    _beforeSaveChangesHandler: function (sender, cancelEventArgs) {
        cancelEventArgs.set_cancel(!this._validate());
    },
    /* --------------------------------- private methods --------------------------------- */
    _validate: function () {
        var limitationsView = this._getFormParagraphTextBoxLimitationsView();
        if (limitationsView && limitationsView.get_maxRangeField()) {
            var maxLength = limitationsView.get_maxRangeField().get_value();
            var defaultValue = this._predefinedValueTextField.get_value();
            if (!this._validateDefaultValue(maxLength, defaultValue)) {
                alert(this.get_defaultValueValidationMessage());
                return false;
            }
        }
        return true;
    },
    _validateDefaultValue: function (maxLength, defaultValue) {
        if (defaultValue && maxLength && 0 < maxLength && maxLength < defaultValue.length) {
            return false;
        }

        return true;
    },

    _getFormParagraphTextBoxLimitationsView: function () {
        var parent = this.get_propertyEditor().get_designer();
        if (parent) {
            var formParagraphTextBoxLimitationsViewId = parent.get_designerViewsMap()["FormParagraphTextBoxLimitationsView"];
            var formParagraphTextBoxLimitationsView = $find(formParagraphTextBoxLimitationsViewId);
            return formParagraphTextBoxLimitationsView;
        }

        return null;
    },
    /* --------------------------------- properties --------------------------------- */

    // IDesignerViewControl: gets the reference fo the propertyEditor control
    get_parentDesigner: function () {
        return this._parentDesigner;
    },

    // IDesignerViewControl: sets the reference fo the propertyEditor control
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

    // Gets the textfield for the label of the control
    get_labelTextField: function () {
        return this._labelTextField;
    },
    // Sets the textfield for the label of the control
    set_labelTextField: function (value) {
        this._labelTextField = value;
    },

    // Gets the choicefield for setting the required option
    get_requiredChoiceField: function () {
        return this._requiredChoiceField;
    },
    // Sets the choicefield for setting the required option
    set_requiredChoiceField: function (value) {
        this._requiredChoiceField = value;
    },

    // Gets the textfield for the instructions text
    get_instructionsTextField: function () {
        return this._instructionsTextField;
    },
    // Sets the textfield for the instructions text
    set_instructionsTextField: function (value) {
        this._instructionsTextField = value;
    },

    // Gets the textfield for the default value text
    get_predefinedValueTextField: function () {
        return this._predefinedValueTextField;
    },
    // Sets the textfield for the default value text
    set_predefinedValueTextField: function (value) {
        this._predefinedValueTextField = value;
    },

    // Gets the textfield for the error message
    get_errorMessageTextField: function () {
        return this._errorMessageTextField;
    },
    // Sets the textfield for the error message
    set_errorMessageTextField: function (value) {
        this._errorMessageTextField = value;
    },

    get_metaFieldNameTextBox: function () {
        return this._metaFieldNameTextBox;
    },
    set_metaFieldNameTextBox: function (value) {
        this._metaFieldNameTextBox = value;
    },
    get_defaultValueValidationMessage: function () {
        return this._defaultValueValidationMessage;
    },
    set_defaultValueValidationMessage: function (value) {
        this._defaultValueValidationMessage = value;
    }
}

Telerik.Sitefinity.Modules.Forms.Web.UI.Designers.Views.FormParagraphTextBoxLabelView.registerClass('Telerik.Sitefinity.Modules.Forms.Web.UI.Designers.Views.FormParagraphTextBoxLabelView', Sys.UI.Control, Telerik.Sitefinity.Web.UI.ControlDesign.IDesignerViewControl);
