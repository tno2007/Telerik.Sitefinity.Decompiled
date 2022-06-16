/// <reference name="MicrosoftAjax.js"/>
/// <reference name="Telerik.Sitefinity.Resources.Scripts.jquery-1.6.3-vsdoc.js" assembly="Telerik.Sitefinity.Resources"/>

Type.registerNamespace("Telerik.Sitefinity.Web.UI.Fields.Designers.Views");

Telerik.Sitefinity.Web.UI.Fields.Designers.Views.ImageFieldLabelView = function (element) {
    Telerik.Sitefinity.Web.UI.Fields.Designers.Views.ImageFieldLabelView.initializeBase(this, [element]);
    this._labelTextField = null;
    this._requiredChoiceField = null;
    this._instructionsTextField = null;
    this._predefinedImageField = null;
    this._errorMessageTextField = null;
    this._defaultRequiredMessage = null;
    this._metaFieldNameTextBox = null;
    this._defaultItemTypeName = "";
    this._interceptSaveChanges = true;

    this._requiredChoiceFieldValueChangedDelegate = null;
    this._successDelegate = null;
    this._failureDelegate = null;
    this._webServiceUrl = null;
    this._onLoadDelegate = null;
}


Telerik.Sitefinity.Web.UI.Fields.Designers.Views.ImageFieldLabelView.prototype = {
    /* --------------------------------- set up and tear down --------------------------------- */
    initialize: function () {
        Telerik.Sitefinity.Web.UI.Fields.Designers.Views.ImageFieldLabelView.callBaseMethod(this, 'initialize');

        this._requiredChoiceFieldValueChangedDelegate = Function.createDelegate(this, this._requiredChoiceFieldValueChangedHandler);
        this._requiredChoiceField.add_valueChanged(this._requiredChoiceFieldValueChangedDelegate);
        this._successDelegate = Function.createDelegate(this, this._successHandler);
        this._failureDelegate = Function.createDelegate(this, this._failureHandler);
        
        this._onLoadDelegate = Function.createDelegate(this, this._onLoad);
        Sys.Application.add_load(this._onLoadDelegate);
    },

    dispose: function () {
        Telerik.Sitefinity.Web.UI.Fields.Designers.Views.ImageFieldLabelView.callBaseMethod(this, 'dispose');

        if (this._requiredChoiceFieldValueChangedDelegate) {
            if (this._requiredChoiceField) {
                this._requiredChoiceField.remove_valueChanged(this._requiredChoiceFieldValueChangedDelegate);
            }
        }

        if (this._onLoadDelegate) {
            Sys.Application.remove_load(this._onLoadDelegate);
            delete this._onLoadDelegate;
        }
    },
    /* ------------------ Events --------------*/
    _onLoad: function () {
        // This is causing an issue when trying to select a library during upload.
        //jQuery("body").addClass("sfNoJQDialogOverlay");
    },
    /* --------------------------------- public methods --------------------------------- */

    // implementation of IDesignerViewControl: Forces the control to refersh from the control Data
    refreshUI: function () {
        var controlData = this.get_controlData();

        this._labelTextField.set_value(controlData.Title);
        this._instructionsTextField.set_value(controlData.Example);
        // Pavel TODO: set DefaultItemId and DefaultItemProviderName to the image field        
        //todo:set the item as selected        
        this._loadDefaultImage(controlData.DefaultImageId, controlData.ProviderNameForDefaultImage);
        if (controlData.DefaultImageSrc) {
            this._predefinedImageField._defaultSrc = controlData.DefaultImageSrc;
        }

        // Ivan: TEMP HACK
        if (controlData.ValidatorDefinition == null) {
            controlData.ValidatorDefinition = { 'Required': false }
        }

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

        var selectedImage = this._predefinedImageField.get_selectedImageItem();
        if (selectedImage) {
            controlData.DefaultImageId = selectedImage.Id;
            controlData.ProviderNameForDefaultImage = selectedImage.ProviderName;
        }

        controlData.DefaultItemTypeName = this._defaultItemTypeName;

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

    /* --------------------------------- private methods --------------------------------- */
    _loadDefaultImage: function (defaultImageId, providerName) {
		if (!defaultImageId || defaultImageId == "00000000-0000-0000-0000-000000000000") {
            return;
        }
        var clientManager = new Telerik.Sitefinity.Data.ClientManager();
        var serviceUrl = this._webServiceUrl;
        var urlParams = [];        
        if (this._defaultItemTypeName) {
            urlParams["itemType"] = this._defaultItemTypeName;
        }
        if (providerName) {
            urlParams["provider"] = providerName;
        }
        urlParams["published"] = true;

        var keys = [defaultImageId];
        clientManager.InvokeGet(serviceUrl, urlParams, keys, this._successDelegate, this._failureDelegate, this);
    },

    _successHandler: function (caller, data, request, context) {
        var item = data.Item;
        this._predefinedImageField._selectedImageItem = item;
        this._predefinedImageField.get_imageElement().src = item.ThumbnailUrl;
    },

    _failureHandler: function (error, caller, context) {

        alert(error.Detail);
    },

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
    get_requiredChoiceField: function () { return this._requiredChoiceField; },
    // Sets the choicefield for setting the required option
    set_requiredChoiceField: function (value) { this._requiredChoiceField = value; },

    // Gets the textfield for the instructions text
    get_instructionsTextField: function () { return this._instructionsTextField; },
    // Sets the textfield for the instructions text
    set_instructionsTextField: function (value) { this._instructionsTextField = value; },

    // Gets the textfield for the default image
    get_predefinedImageField: function () { return this._predefinedImageField; },
    // Sets the textfield for the default image
    set_predefinedImageField: function (value) { this._predefinedImageField = value; },

    // Gets the textfield for the error message
    get_errorMessageTextField: function () { return this._errorMessageTextField; },
    // Sets the textfield for the error message
    set_errorMessageTextField: function (value) { this._errorMessageTextField = value; },

    get_metaFieldNameTextBox: function () { return this._metaFieldNameTextBox; },
    set_metaFieldNameTextBox: function (value) { this._metaFieldNameTextBox = value; }

}

Telerik.Sitefinity.Web.UI.Fields.Designers.Views.ImageFieldLabelView.registerClass('Telerik.Sitefinity.Web.UI.Fields.Designers.Views.ImageFieldLabelView', Sys.UI.Control, Telerik.Sitefinity.Web.UI.ControlDesign.IDesignerViewControl);
