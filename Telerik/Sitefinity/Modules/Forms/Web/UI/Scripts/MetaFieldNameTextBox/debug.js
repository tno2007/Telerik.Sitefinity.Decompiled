Type.registerNamespace("Telerik.Sitefinity.Modules.Forms.Web.UI.Fields");

Telerik.Sitefinity.Modules.Forms.Web.UI.Fields.MetaFieldNameTextBox = function (element) {
    Telerik.Sitefinity.Modules.Forms.Web.UI.Fields.MetaFieldNameTextBox.initializeBase(this, [element]);
    this._changeButton = null;
    this._metaFieldNameTextField = null;
    this._fieldName = null;
    this._readOnly = false;
    this._blurInitialized = false;
    this._expandableSection = null;
    this._expandButton = null;
    this._isEditModeEntered = false;

    this._changeButtonClickDelegate = null;
    this._metaFieldNameBlurDelegate = null;
    this._expandButtonClickDelegate = null;
}

Telerik.Sitefinity.Modules.Forms.Web.UI.Fields.MetaFieldNameTextBox.prototype = {
    /* --------------------------------- set up and tear down --------------------------------- */
    initialize: function () {
        Telerik.Sitefinity.Modules.Forms.Web.UI.Fields.MetaFieldNameTextBox.callBaseMethod(this, 'initialize');




        this.get_metaFieldNameTextField().set_value(this._fieldName);

        if (!this._readOnly) {
            this._changeButtonClickDelegate = Function.createDelegate(this, this._changeButtonClickHandler);
            $addHandler(this._changeButton, "click", this._changeButtonClickDelegate, true);
            this._metaFieldNameBlurDelegate = Function.createDelegate(this, this._metaFieldNameBlurHandler);
        }

        this._expandButtonClickDelegate = Function.createDelegate(this, this._expandButtonClickHandler);
        $addHandler(this._expandButton, "click", this._expandButtonClickDelegate, true);

    },

    dispose: function () {
        Telerik.Sitefinity.Modules.Forms.Web.UI.Fields.MetaFieldNameTextBox.callBaseMethod(this, 'dispose');
        if (this._metaFieldNameBlurDelegate) {
            delete this._metaFieldNameBlurDelegate;
        }

        if (this._changeButtonClickDelegate) {
            delete this._changeButtonClickDelegate;
        }

        if (this._expandButtonClickDelegate) {
            delete this._expandButtonClickDelegate;
        }
    },

    /* --------------------------------- public methods ---------------------------------- */

    /* --------------------------------- event handlers ---------------------------------- */
    _changeButtonClickHandler: function (e) {
        this._metaFieldNameTextField.set_displayMode(Telerik.Sitefinity.Web.UI.Fields.FieldDisplayMode.Write);
        jQuery(this._changeButton).hide();
        if (!this._blurInitialized) {
            $addHandler(this.get_metaFieldNameTextField().get_textElement(), "blur", this._metaFieldNameBlurDelegate, true);
        }

        this._isEditModeEntered = true;
    },

    _metaFieldNameBlurHandler: function (e) {
        this._metaFieldNameTextField.set_displayMode(Telerik.Sitefinity.Web.UI.Fields.FieldDisplayMode.Read);
        jQuery(this._changeButton).show();
    },

    _expandButtonClickHandler: function (e) {
        jQuery(this._expandableSection).toggleClass("sfExpandedSection");
        dialogBase.resizeToContent();
    },

    /* --------------------------------- private methods --------------------------------- */

    /* --------------------------------- properties -------------------------------------- */

    // Gets the meta field name value
    get_value: function () {
        var val;
        if (this._isEditModeEntered) {
            val = this._metaFieldNameTextField._get_writeModeValue();

            if (typeof val === "string" && val.length > 0) {
                return val;
            }
        }

        return this._metaFieldNameTextField.get_value();
    },
    // Sets the meta field name value
    set_value: function (value) {
        this._metaFieldNameTextField.set_value(value);
    },

    // Gets the text field for the meta field name
    get_metaFieldNameTextField: function () { return this._metaFieldNameTextField; },
    // Sets the text field for the meta field name
    set_metaFieldNameTextField: function (value) { this._metaFieldNameTextField = value; },

    get_changeButton: function () { return this._changeButton; },
    set_changeButton: function (value) { this._changeButton = value; },

    get_readOnly: function () { return this._readOnly; },
    set_readOnly: function (value) { this._readOnly = value; },

    get_expandableSection: function () { return this._expandableSection; },
    set_expandableSection: function (value) { this._expandableSection = value; },

    get_expandButton: function () { return this._expandButton; },
    set_expandButton: function (value) { this._expandButton = value; }
}

Telerik.Sitefinity.Modules.Forms.Web.UI.Fields.MetaFieldNameTextBox.registerClass('Telerik.Sitefinity.Modules.Forms.Web.UI.Fields.MetaFieldNameTextBox', Sys.UI.Control);
