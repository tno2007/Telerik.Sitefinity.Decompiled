/// <reference name="MicrosoftAjax.js"/>
/// <reference name="Telerik.Sitefinity.Resources.Scripts.jquery-1.6.3-vsdoc.js" assembly="Telerik.Sitefinity.Resources"/>

Type.registerNamespace("Telerik.Sitefinity.Modules.Forms.Web.UI.Designers.Views");

Telerik.Sitefinity.Modules.Forms.Web.UI.Designers.Views.FormChoiceFieldLabelView = function (element) {
    Telerik.Sitefinity.Modules.Forms.Web.UI.Designers.Views.FormChoiceFieldLabelView.initializeBase(this, [element]);
    this._labelTextField = null;
    this._addOtherChoiceField = null;
    this._choiceItemsBuilder = null;
    this._defaultSelectedChoiceField = null;
    this._otherTitleTextField = null;
    this._requiredChoiceField = null;
    this._errorMessageTextField = null;
    this._defaultRequiredMessage = null;
    this._sortChoicesAlphabeticallyChoiceField = null;
    this._metaFieldNameTextBox = null;

    this._requiredChoiceFieldValueChangedDelegate = null;
    this._addOtherChoiceFieldValueChangedDelegate = null;
    this._defaultSelectedChoiceFieldValueChangedDelegate = null;
    this._pageLoadDelegate = null;
    this._beforeSaveChangesDelegate = null;
}

Telerik.Sitefinity.Modules.Forms.Web.UI.Designers.Views.FormChoiceFieldLabelView.prototype = {
    /* --------------------------------- set up and tear down --------------------------------- */
    initialize: function () {
        Telerik.Sitefinity.Modules.Forms.Web.UI.Designers.Views.FormChoiceFieldLabelView.callBaseMethod(this, 'initialize');

        if (this._addOtherChoiceField) {
            this._addOtherChoiceFieldValueChangedDelegate = Function.createDelegate(this, this._addOtherChoiceFieldValueChangedHandler);
            this._addOtherChoiceField.add_valueChanged(this._addOtherChoiceFieldValueChangedDelegate);
        }

        if (this._requiredChoiceField) {
            this._requiredChoiceFieldValueChangedDelegate = Function.createDelegate(this, this._requiredChoiceFieldValueChangedHandler);
            this._requiredChoiceField.add_valueChanged(this._requiredChoiceFieldValueChangedDelegate);
        }

        if (this._defaultSelectedChoiceField) {
            this._defaultSelectedChoiceFieldValueChangedDelegate = Function.createDelegate(this, this._defaultSelectedChoiceFieldValueChangedHandler);
            this._defaultSelectedChoiceField.add_valueChanged(this._defaultSelectedChoiceFieldValueChangedDelegate);
        }

        this._pageLoadDelegate = Function.createDelegate(this, this._pageLoadHandler);

        Sys.Application.add_load(this._pageLoadDelegate);
    },

    dispose: function () {
        Telerik.Sitefinity.Modules.Forms.Web.UI.Designers.Views.FormChoiceFieldLabelView.callBaseMethod(this, 'dispose');
        Sys.Application.remove_load(this._pageLoadDelegate);
        if (this._addOtherChoiceFieldValueChangedDelegate) {
            if (this._addOtherChoiceField) {
                this._addOtherChoiceField.remove_valueChanged(this._addOtherChoiceFieldValueChangedDelegate);
            }
        }
        if (this._requiredChoiceFieldValueChangedDelegate) {
            if (this._requiredChoiceField) {
                this._requiredChoiceField.remove_valueChanged(this._requiredChoiceFieldValueChangedDelegate);
            }
        }
    },

    /* --------------------------------- public methods --------------------------------- */

    // implementation of IDesignerViewControl: Forces the control to refersh from the control Data
    refreshUI: function () {
        var controlData = this.get_controlData();
        this._labelTextField.set_value(controlData.Title);
        if (controlData.ChoiceItemsTitles && controlData.ChoiceItemsTitles.length > 0) {
            // TODO: work with choice items, not titles

            if (controlData.DefaultSelectedTitle !== undefined) {
                this._choiceItemsBuilder.set_choiceItemsTitles(controlData.ChoiceItemsTitles, controlData.DefaultSelectedTitle);
            }
            else {
                this._choiceItemsBuilder.set_choiceItemsTitles(controlData.ChoiceItemsTitles, null);
            }
        }

        if (this._sortChoicesAlphabeticallyChoiceField) {
            this._sortChoicesAlphabeticallyChoiceField.set_value(controlData.SortAlphabetically);
        }

        if (this._requiredChoiceField) {

            // temp workaround
            if (controlData.ValidatorDefinition == null) {
                controlData.ValidatorDefinition = { 'Required': false };
            }

            if (controlData.ValidatorDefinition.Required) {
                this._requiredChoiceField.set_value(true);
                if (this._errorMessageTextField) {
                    jQuery(this._errorMessageTextField.get_element()).show();
                    this._errorMessageTextField.set_value(controlData.ValidatorDefinition.RequiredViolationMessage);
                }
            }
            else {
                this._requiredChoiceField.set_value(false);
                if (this._errorMessageTextField) {
                    this._errorMessageTextField.set_value(this._defaultRequiredMessage);
                    jQuery(this._errorMessageTextField.get_element()).hide();
                }
            }
        }

        if (this._defaultSelectedChoiceField) {
            if (controlData.FirstItemIsSelected) {
                this.enable_requiredChoiceField(false);
                this._defaultSelectedChoiceField.set_value("First");
            }
            else {
                this.enable_requiredChoiceField(true);
                this._defaultSelectedChoiceField.set_value("None");
            }
        }

        if (this._otherTitleTextField && this._otherTitleTextField) {
            if (controlData.EnableAddOther) {
                jQuery(this._otherTitleTextField.get_element()).show();
                this._addOtherChoiceField.set_value(true);
            }
            else {
                jQuery(this._otherTitleTextField.get_element()).hide();
                this._addOtherChoiceField.set_value(false);
            }
            this._otherTitleTextField.set_value(controlData.OtherTitleText);
        }
    },

    // implementation of IDesignerViewControl: forces the designer view to apply the changes on UI to the control Data
    applyChanges: function () {
        var controlData = this.get_controlData();
        controlData.Title = this._labelTextField.get_value();

        var metaFieldName = this.get_metaFieldNameTextBox();
        if (metaFieldName && !metaFieldName.get_readOnly() && controlData.MetaField) {
            controlData.MetaField.FieldName = metaFieldName.get_value();
        }

        // TODO: work with choice items : Choices => set_choiceItems
        if (controlData.DefaultSelectedTitle !== undefined) {
            controlData.DefaultSelectedTitle = this._choiceItemsBuilder.get_defaultSelectedTitle();
        }
        controlData.ChoiceItemsTitles = this._choiceItemsBuilder.get_choiceItemsTitles();

        if (this._sortChoicesAlphabeticallyChoiceField) {
            if (this._sortChoicesAlphabeticallyChoiceField.get_value() == "true") {
                controlData.SortAlphabetically = true;
            }
            else {
                controlData.SortAlphabetically = false;
            }
        }

        if (this._requiredChoiceField) {

            // temp workaround
            if (controlData.ValidatorDefinition === null) {
                controlData.ValidatorDefinition = { 'Required': false };
            }

            if (this._requiredChoiceField.get_value() === "true") {
                controlData.ValidatorDefinition.Required = true;
                controlData.ValidatorDefinition.RequiredViolationMessage = this._errorMessageTextField.get_value();
            }
            else {
                controlData.ValidatorDefinition.Required = false;
            }
        }

        if (this._defaultSelectedChoiceField) {
            if (this._defaultSelectedChoiceField.get_value() == "First") {
                this.enable_requiredChoiceField(false);
                controlData.FirstItemIsSelected = true;
            }
            else {
                this.enable_requiredChoiceField(true);
                controlData.FirstItemIsSelected = false;
            }
        }

        if (this._otherTitleTextField && this._otherTitleTextField) {
            if (this._addOtherChoiceField.get_value() === "true") {
                controlData.EnableAddOther = true;
                controlData.OtherTitleText = this._otherTitleTextField.get_value();
            }
            else {
                controlData.EnableAddOther = false;
            }
        }
    },

    // gets the javascript control object that is being designed
    get_controlData: function () {
        return this.get_parentDesigner().get_propertyEditor().get_control();
    },

    enable_requiredChoiceField: function (enable) {
        if (enable) {
            jQuery(this._requiredChoiceField._element).find("input").removeAttr("disabled");
            jQuery(this._requiredChoiceField._element).find("label").removeClass("sfDisabledLinkBtn");
        } else {
            jQuery(this._requiredChoiceField._element).find("input").attr("disabled", "disabled");
            jQuery(this._requiredChoiceField._element).find("label").addClass("sfDisabledLinkBtn");
        }
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

    _defaultSelectedChoiceFieldValueChangedHandler: function(sender) {
        if (this.get_defaultSelectedChoiceField().get_value() == "None") {
            this.enable_requiredChoiceField(true);
        }
        else {
            this.enable_requiredChoiceField(false);
            this._requiredChoiceField.set_value(false);
            if (this._errorMessageTextField) {
                jQuery(this._errorMessageTextField.get_element()).hide();
            }
        }
    },

    // Handles the add other choice field change
    _addOtherChoiceFieldValueChangedHandler: function (sender) {
        if (this._addOtherChoiceField.get_value() === "true") {
            jQuery(this._otherTitleTextField.get_element()).show();
        }
        else {
            jQuery(this._otherTitleTextField.get_element()).hide();
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
        cancelEventArgs.set_cancel(!this._choiceItemsBuilder.validate());
    },

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

    // Gets the textfield for the label of the control
    get_labelTextField: function () { return this._labelTextField; },
    // Sets the textfield for the label of the control
    set_labelTextField: function (value) { this._labelTextField = value; },

    // Gets the choicefield for setting the required option
    get_addOtherChoiceField: function () { return this._addOtherChoiceField; },
    // Sets the choicefield for setting the required option
    set_addOtherChoiceField: function (value) { this._addOtherChoiceField = value; },

    // Gets the textfield for the instructions text
    get_choiceItemsBuilder: function () { return this._choiceItemsBuilder; },
    // Sets the textfield for the instructions text
    set_choiceItemsBuilder: function (value) { this._choiceItemsBuilder = value; },

    // Gets the textfield for the default value text
    get_defaultSelectedChoiceField: function () { return this._defaultSelectedChoiceField; },
    // Sets the textfield for the default value text
    set_defaultSelectedChoiceField: function (value) { this._defaultSelectedChoiceField = value; },

    get_otherTitleTextField: function () { return this._otherTitleTextField; },
    set_otherTitleTextField: function (value) { this._otherTitleTextField = value; },

    get_requiredChoiceField: function () { return this._requiredChoiceField; },
    set_requiredChoiceField: function (value) { this._requiredChoiceField = value; },

    get_sortChoicesAlphabeticallyChoiceField: function () { return this._sortChoicesAlphabeticallyChoiceField; },
    set_sortChoicesAlphabeticallyChoiceField: function (value) { this._sortChoicesAlphabeticallyChoiceField = value; },

    get_metaFieldNameTextBox: function () { return this._metaFieldNameTextBox; },
    set_metaFieldNameTextBox: function (value) { this._metaFieldNameTextBox = value; },

    get_errorMessageTextField: function () { return this._errorMessageTextField; },
    set_errorMessageTextField: function (value) { this._errorMessageTextField = value; }
}

Telerik.Sitefinity.Modules.Forms.Web.UI.Designers.Views.FormChoiceFieldLabelView.registerClass('Telerik.Sitefinity.Modules.Forms.Web.UI.Designers.Views.FormChoiceFieldLabelView', Sys.UI.Control, Telerik.Sitefinity.Web.UI.ControlDesign.IDesignerViewControl);