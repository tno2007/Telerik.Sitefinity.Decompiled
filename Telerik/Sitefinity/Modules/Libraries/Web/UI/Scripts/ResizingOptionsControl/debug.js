/// <reference name="MicrosoftAjax.js"/>
/// <reference name="Telerik.Sitefinity.Resources.Scripts.jquery-1.6.3-vsdoc.js" assembly="Telerik.Sitefinity.Resources"/>
Type._registerScript("ResizingOptionsControl.js");
Type.registerNamespace("Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers");

Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.ResizingOptionsControl = function (element) {
    Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.ResizingOptionsControl.initializeBase(this, [element]);

    this._resizeChoiceField = null;
    this._sizesChoiceField = null;
    this._customWidthTextField = null;
    this._resizeSettingsGroup = null;
    this._resizeSettingsExpander = null;
    this._openOriginalChoiceField = null;

    this._showOpenOriginalSizeCheckBox = true;

    this._sizesChoiceFieldValueChangedDelegate = null;
    this._resizeChoiceFieldValueChangedDelegate = null;
    this._resizeSettingsExpanderClickDelegate = null;
}

Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.ResizingOptionsControl.prototype = {

    /* --------------------------------- set up and tear down --------------------------------- */

    initialize: function () {
        Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.ResizingOptionsControl.callBaseMethod(this, 'initialize');

        this._resizeChoiceFieldValueChangedDelegate = Function.createDelegate(this, this._resizeChoiceFieldValueChangedHandler);
        this._resizeChoiceField.add_valueChanged(this._resizeChoiceFieldValueChangedDelegate);

        this._sizesChoiceFieldValueChangedDelegate = Function.createDelegate(this, this._sizesChoiceFieldValueChangedHandler);
        this._sizesChoiceField.add_valueChanged(this._sizesChoiceFieldValueChangedDelegate);

        this._resizeSettingsExpanderClickDelegate = Function.createDelegate(this, this._resizeSettingsExpanderClickHandler);
        $addHandler(this._resizeSettingsExpander, "click", this._resizeSettingsExpanderClickDelegate);
    },

    dispose: function () {
        Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.ResizingOptionsControl.callBaseMethod(this, 'dispose');

        if (this._resizeChoiceFieldValueChangedDelegate) {
            if (this._resizeChoiceField) {
                this._resizeChoiceField.remove_valueChanged(this._resizeChoiceFieldValueChangedDelegate);
            }
            delete this._resizeChoiceFieldValueChangedDelegate;
        }

        if (this._sizesChoiceFieldValueChangedDelegate) {
            if (this._sizesChoiceField) {
                this._sizesChoiceField.remove_valueChanged(this._sizesChoiceFieldValueChangedDelegate);
            }
            delete this._sizesChoiceFieldValueChangedDelegate;
        }

        if (this._resizeSettingsExpanderClickDelegate) {
            $removeHandler(this._resizeSettingsExpander, "click", this._resizeSettingsExpanderClickDelegate);
            delete this._resizeSettingsExpanderClickDelegate;
        }
    },

    /* --------------------------------- public methods --------------------------------- */

    /* --------------------------------- event handlers --------------------------------- */

    // Fired when the resize expander is clicked
    _resizeSettingsExpanderClickHandler: function (e) {
        jQuery(this._resizeSettingsGroup).toggleClass("sfExpandedSection");
        if (dialogBase._implementsDesigner || dialogBase._hostedInRadWindow) {
			dialogBase.resizeToContent();
		}
    },

    // Fired when the radio buttons for resizing are changed
    _resizeChoiceFieldValueChangedHandler: function (sender, args) {
        if (sender.get_value() == "resize") {
            jQuery(this.get_sizesChoiceField().get_element()).show();

            if (this.get_sizesChoiceField().get_value() == "custom") {
                jQuery(this.get_customWidthTextField().get_element()).show();
            }
            else {
                jQuery(this.get_customWidthTextField().get_element()).hide();
            }

            if (this.get_showOpenOriginalSizeCheckBox()) {
                jQuery(this.get_openOriginalChoiceField().get_element()).show();
            }

            if (dialogBase._implementsDesigner || dialogBase._hostedInRadWindow) {
				dialogBase.resizeToContent();
            }
        }
        else {
            jQuery(this.get_sizesChoiceField().get_element()).hide();

            if (this.get_showOpenOriginalSizeCheckBox()) {
                jQuery(this.get_openOriginalChoiceField().get_element()).hide();
            }
            jQuery(this.get_customWidthTextField().get_element()).hide();
        }

        if (dialogBase._implementsDesigner || dialogBase._hostedInRadWindow) {
			dialogBase.resizeToContent();
		}
    },

    // Fired when the selected item in drop down with sizes is changed
    _sizesChoiceFieldValueChangedHandler: function (sender, args) {
        if (sender.get_value() == "custom") {
            jQuery(this.get_customWidthTextField().get_element()).show();
        }
        else {
            jQuery(this.get_customWidthTextField().get_element()).hide();
        }

        if (dialogBase._implementsDesigner || dialogBase._hostedInRadWindow) {
			dialogBase.resizeToContent();
		}
    },

    /* --------------------------------- private methods --------------------------------- */

    /* --------------------------------- properties --------------------------------- */

    get_resizeChoiceField: function () {
        return this._resizeChoiceField;
    },
    set_resizeChoiceField: function (value) {
        this._resizeChoiceField = value;
    },

    get_sizesChoiceField: function () {
        return this._sizesChoiceField;
    },
    set_sizesChoiceField: function (value) {
        this._sizesChoiceField = value;
    },

    get_resizeSettingsGroup: function () {
        return this._resizeSettingsGroup;
    },
    set_resizeSettingsGroup: function (value) {
        this._resizeSettingsGroup = value;
    },

    get_resizeSettingsExpander: function () {
        return this._resizeSettingsExpander;
    },
    set_resizeSettingsExpander: function (value) {
        this._resizeSettingsExpander = value;
    },

    get_customWidthTextField: function () {
        return this._customWidthTextField;
    },
    set_customWidthTextField: function (value) {
        this._customWidthTextField = value;
    },

    get_openOriginalChoiceField: function () {
        return this._openOriginalChoiceField;
    },
    set_openOriginalChoiceField: function (value) {
        this._openOriginalChoiceField = value;
    },

    // Returns true if a resizing option is selected
    get_itemIsResized: function () {
        return this.get_resizeChoiceField().get_value() == "resize";
    },

    // If resize is selected returns the selected widht
    get_resizedWidth: function () {
        if (this.get_resizeChoiceField().get_value() != "resize") {
            return null;
        }

        var width = this.get_sizesChoiceField().get_value();
        if (width) {
            if (width == "custom") {
                var customWidth = this.get_customWidthTextField().get_value();
                if (customWidth && customWidth != "") {
                    width = customWidth;
                }
            }
            if (width == "") {
                return null;
            }
            return width;
        }
        else {
            return null;
        }
    },

    get_showOpenOriginalSizeCheckBox: function () {
        return this._showOpenOriginalSizeCheckBox;
    },
    set_showOpenOriginalSizeCheckBox: function (value) {
        this._showOpenOriginalSizeCheckBox = value;
    },

    // Returns true if the resized item should open the original one
    get_resizedItemOpensOriginal: function () {
        if (this._openOriginalChoiceField) {
            var result = this._openOriginalChoiceField.get_value();
            return result === "true";
        }
        return false;
    }
}

Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.ResizingOptionsControl.registerClass('Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.ResizingOptionsControl', Sys.UI.Control);

if (typeof (Sys) !== 'undefined') Sys.Application.notifyScriptLoaded();
