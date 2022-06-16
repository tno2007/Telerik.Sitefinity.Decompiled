/// <reference name="MicrosoftAjax.js"/>
/// <reference name="Telerik.Sitefinity.Resources.Scripts.jquery-1.6.3-vsdoc.js" assembly="Telerik.Sitefinity.Resources"/>

Type.registerNamespace("Telerik.Sitefinity.Modules.Forms.Web.UI.Designers");

Telerik.Sitefinity.Modules.Forms.Web.UI.Designers.FormRadCaptchaDesignerView = function (element) {
    Telerik.Sitefinity.Modules.Forms.Web.UI.Designers.FormRadCaptchaDesignerView.initializeBase(this, [element]);
    this._imageTextLength = null;
    this._fontFamily = null;
    this._textColorField = null;
    this._backgroundColorField = null;
    this._lineNoiseField = null;
    this._fontWarpField = null;
    this._backgroundNoiseLevelField = null;
    this._displayOnlyForUnauthenticatedUsers = null;
}


Telerik.Sitefinity.Modules.Forms.Web.UI.Designers.FormRadCaptchaDesignerView.prototype = {
    /* --------------------------------- set up and tear down --------------------------------- */
    initialize: function () {
        Telerik.Sitefinity.Modules.Forms.Web.UI.Designers.FormRadCaptchaDesignerView.callBaseMethod(this, 'initialize');
    },

    dispose: function () {
        Telerik.Sitefinity.Modules.Forms.Web.UI.Designers.FormRadCaptchaDesignerView.callBaseMethod(this, 'dispose');
    },

    /* --------------------------------- public methods --------------------------------- */

    // implementation of IDesignerViewControl: Forces the control to refersh from the control Data
    refreshUI: function () {
        var controlData = this.get_controlData();

        this.get_imageTextLength().set_value(controlData.ImageTextLength);
        this.get_fontFamily().set_value(controlData.FontFamily);
        this.get_textColorField().set_value(controlData.TextColor);
        this.get_backgroundColorField().set_value(controlData.BackgroundColor);
        this.get_lineNoiseField().set_value(controlData.LineNoise);
        this.get_fontWarpField().set_value(controlData.FontWarp);
        this.get_backgroundNoiseLevelField().set_value(controlData.BackgroundNoiseLevel);
        this.get_displayOnlyForUnauthenticatedUsers().set_value(controlData.DisplayOnlyForUnauthenticatedUsers);
    },

    // implementation of IDesignerViewControl: forces the designer view to apply the changes on UI to the control Data
    applyChanges: function () {
        var controlData = this.get_controlData();
        controlData.ImageTextLength = this.get_imageTextLength().get_value();

        controlData.FontFamily = this.get_fontFamily().get_value();

        controlData.TextColor = this.get_textColorField().get_value();

        controlData.BackgroundColor = this.get_backgroundColorField().get_value();

        controlData.LineNoise = this.get_lineNoiseField().get_value();

        controlData.FontWarp = this.get_fontWarpField().get_value();

        controlData.BackgroundNoiseLevel = this.get_backgroundNoiseLevelField().get_value();


        if (this.get_displayOnlyForUnauthenticatedUsers().get_value() == "true") {
            controlData.DisplayOnlyForUnauthenticatedUsers = true;
        }
        else {
            controlData.DisplayOnlyForUnauthenticatedUsers = false;
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

    get_imageTextLength: function () { return this._imageTextLength; },
    set_imageTextLength: function (value) { this._imageTextLength = value; },

    get_fontFamily: function () { return this._fontFamily; },
    set_fontFamily: function (value) { this._fontFamily = value; },

    get_textColorField: function () { return this._textColorField; },
    set_textColorField: function (value) { this._textColorField = value; },

    get_backgroundColorField: function () { return this._backgroundColorField; },
    set_backgroundColorField: function (value) { this._backgroundColorField = value; },

    get_backgroundColorField: function () { return this._backgroundColorField; },
    set_backgroundColorField: function (value) { this._backgroundColorField = value; },

    get_lineNoiseField: function () { return this._lineNoiseField; },
    set_lineNoiseField: function (value) { this._lineNoiseField = value; },

    get_fontWarpField: function () { return this._fontWarpField; },
    set_fontWarpField: function (value) { this._fontWarpField = value; },

    get_backgroundNoiseLevelField: function () { return this._backgroundNoiseLevelField; },
    set_backgroundNoiseLevelField: function (value) { this._backgroundNoiseLevelField = value; },

    get_displayOnlyForUnauthenticatedUsers: function () { return this._displayOnlyForUnauthenticatedUsers; },
    set_displayOnlyForUnauthenticatedUsers: function (value) { this._displayOnlyForUnauthenticatedUsers = value; },


    // Gets the textfield for the title of the section
    get_titleTextField: function () { return this._titleTextField; },
    // Sets the textfield for the title of the section
    set_titleTextField: function (value) { this._titleTextField = value; }
}

Telerik.Sitefinity.Modules.Forms.Web.UI.Designers.FormRadCaptchaDesignerView.registerClass('Telerik.Sitefinity.Modules.Forms.Web.UI.Designers.FormRadCaptchaDesignerView', Sys.UI.Control, Telerik.Sitefinity.Web.UI.ControlDesign.IDesignerViewControl);
