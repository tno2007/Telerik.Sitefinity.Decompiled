Type.registerNamespace("Telerik.Sitefinity.Modules.Forms.Web.UI.Designers");

Telerik.Sitefinity.Modules.Forms.Web.UI.Designers.FormSubmitButtonDesigner = function (element) {
    Telerik.Sitefinity.Modules.Forms.Web.UI.Designers.FormSubmitButtonDesigner.initializeBase(this, [element]);

    this._useImageButton = null;
    this._buttonImage = null;
    this._changeImageButton = null;
    this._useButtonButton = null;
    this._labelTextField = null;
    this._buttonSizeChoiceField = null;
    this._cssClassTextField = null;
    this._tooltipTextField = null;
    this._imageSettingsDesigner = null;
    this._multiPage = null;

    //delegates
    this._useImageButtonClickDelegate = null;
    this._changeImageButtonClickDelegate = null;
    this._useButtonButtonClickDelegate = null;

}

Telerik.Sitefinity.Modules.Forms.Web.UI.Designers.FormSubmitButtonDesigner.prototype = {
    /* --------------------------------- set up and tear down --------------------------------- */
    initialize: function () {
        Telerik.Sitefinity.Modules.Forms.Web.UI.Designers.FormSubmitButtonDesigner.callBaseMethod(this, 'initialize');



        this._useImageButtonClickDelegate = Function.createDelegate(this, this._useImageButtonClickHandler);
        $addHandler(this._useImageButton, "click", this._useImageButtonClickDelegate, true);

        /*
        // TODO: Image settings designer should be visible and be able to upload images,invoke its applyChanges method
        this._changeImageButtonClickDelegate = Function.createDelegate(this, this._changeImageButtonClickHandler);
        $addHandler(this._changeImageButton, "click", this._changeImageButtonClickDelegate, true);

        this._useButtonButtonClickDelegate = Function.createDelegate(this, this._useButtonButtonClickHandler);
        $addHandler(this._useButtonButton, "click", this._useButtonButtonClickDelegate, true);
        */

    },
    dispose: function () {
        Telerik.Sitefinity.Modules.Forms.Web.UI.Designers.FormSubmitButtonDesigner.callBaseMethod(this, 'dispose');

        if (this._useImageButtonClickDelegate) {
            delete this._useImageButtonClickDelegate;
        }

        if (this._changeImageButtonClickDelegate) {
            delete this._changeImageButtonClickDelegate;
        }

        if (this._useButtonButtonClickDelegate) {
            delete this._useButtonButtonClickDelegate;
        }
    },

    /* --------------------------------- public methods ---------------------------------- */

    // implementation of IControlDesigner: Forces the control to refersh from the control Data
    refreshUI: function () {
        var controlData = this.get_controlData();
        this._labelTextField.set_value(controlData.Text);
        this._cssClassTextField.set_value(controlData.CssClass);
        this._buttonSizeChoiceField.set_value(controlData.ButtonSize);

    },

    // implementation of IControlDesigner: forces the designer view to apply the changes on UI to the control Data
    applyChanges: function () {
        var controlData = this.get_controlData();
        controlData.Text = this._labelTextField.get_value();
        controlData.CssClass = this._cssClassTextField.get_value();
        controlData.ButtonSize = this._buttonSizeChoiceField.get_value();

    },

    // gets the GoogleAnalytics control object that is being designed
    get_controlData: function () {
        return this.get_propertyEditor().get_control();
    },

    /* --------------------------------- event handlers ---------------------------------- */
    // Fires when the button for selecting an image is clicked
    _useImageButtonClickHandler: function (e) {
        this._multiPage.set_selectedIndex(1); // Use image
    },

    _changeImageButtonClickHandler: function (e) {
        this._multiPage.set_selectedIndex(0);   // change image
    },

    _useButtonButtonClickHandler: function (e) {
        this._multiPage.set_selectedIndex(2); // Use button

    },
    /* --------------------------------- private methods --------------------------------- */

    /* --------------------------------- properties -------------------------------------- */

    get_useImageButton: function () { return this._useImageButton; },
    set_useImageButton: function (value) { this._useImageButton = value; },

    get_buttonImage: function () { return this._buttonImage; },
    set_buttonImage: function (value) { this._buttonImage = value; },

    get_changeImageButton: function () { return this._changeImageButton; },
    set_changeImageButton: function (value) { this._changeImageButton = value; },

    get_useButtonButton: function () { return this._useButtonButton; },
    set_useButtonButton: function (value) { this._useButtonButton = value; },

    get_labelTextField: function () { return this._labelTextField; },
    set_labelTextField: function (value) { this._labelTextField = value; },

    get_buttonSizeChoiceField: function () { return this._buttonSizeChoiceField; },
    set_buttonSizeChoiceField: function (value) { this._buttonSizeChoiceField = value; },

    get_cssClassTextField: function () { return this._cssClassTextField; },
    set_cssClassTextField: function (value) { this._cssClassTextField = value; },

    get_tooltipTextField: function () { return this._tooltipTextField; },
    set_tooltipTextField: function (value) { this._tooltipTextField = value; },

    get_imageSettingsDesigner: function () { return this._imageSettingsDesigner; },
    set_imageSettingsDesigner: function (value) { this._imageSettingsDesigner = value; },

    get_multiPage: function () { return this._multiPage; },
    set_multiPage: function (value) { this._multiPage = value; }

}

Telerik.Sitefinity.Modules.Forms.Web.UI.Designers.FormSubmitButtonDesigner.registerClass('Telerik.Sitefinity.Modules.Forms.Web.UI.Designers.FormSubmitButtonDesigner', Telerik.Sitefinity.Web.UI.ControlDesign.ControlDesignerBase);
