Type.registerNamespace("Telerik.Sitefinity.Modules.Pages.Web.UI.Fields");

Telerik.Sitefinity.Modules.Pages.Web.UI.Fields.PageUrlMirrorTextField = function (element) {
    Telerik.Sitefinity.Modules.Pages.Web.UI.Fields.PageUrlMirrorTextField.initializeBase(this, [element]);

    this._customText = null;
    this._createCustomUrlButton = null;
    this._defaultStructureButton = null;
    this._onCreateCustomUrlButtonClickDelegate = null;
    this._onDefaultStructureButtonClickDelegate = null;
    this._onUrlMouseOverDelegate = null;
    this._onUrlMouseLeaveDelegate = null;
    this._urlButtonClicked = null;
    this._isCustomUrl = false;
    this._normalUrl = "";
    this._dataItem = null;
    this._customUrlValidationMessage = null;
}

Telerik.Sitefinity.Modules.Pages.Web.UI.Fields.PageUrlMirrorTextField.prototype = {

    initialize: function () {
        this._initDelegates();
        this._addHandlers();
        Telerik.Sitefinity.Modules.Pages.Web.UI.Fields.PageUrlMirrorTextField.callBaseMethod(this, 'initialize');
    },

    dispose: function () {
        this._removeHandlers();
        Telerik.Sitefinity.Modules.Pages.Web.UI.Fields.PageUrlMirrorTextField.callBaseMethod(this, 'dispose');
    },

    /* Overridden Methods */

    _showAsLabel: function() {
        Telerik.Sitefinity.Modules.Pages.Web.UI.Fields.PageUrlMirrorTextField.callBaseMethod(this, '_showAsLabel');
        this.get_createCustomUrlButton().style.display = "none";
        this.get_defaultStructureButton().style.display = "none";
    },

    _parentChanged: function (sender, args) {
        if (sender._selectedNode) {
            this.get_createCustomUrlButton().style.display = "none";
            this.get_defaultStructureButton().style.display = "none";
        }
        Telerik.Sitefinity.Modules.Pages.Web.UI.Fields.PageUrlMirrorTextField.callBaseMethod(this, '_parentChanged', [sender, args]);
        if (this._isCustomUrl) {
            this.get_urlLabelControl().innerHTML = "/";
            this.get_customText().style.display = "";
        } else {
            this.get_customText().style.display = "none";
        }
    },

    _textBoxFocusLost: function (sender, args) {
        if (!this._urlButtonClicked) {
            if (this._isCustomUrl) {
                this.get_customText().style.display = "";
            }
            this.get_mirroredValueLabel().innerHTML = this.get_textElement().value;
            this._set_isToShowAsLabel(true);
        }
        this._urlButtonClicked = null;
    },

    _changeClicked: function () {
        Telerik.Sitefinity.Modules.Pages.Web.UI.Fields.PageUrlMirrorTextField.callBaseMethod(this, '_changeClicked');
        if (this._isCustomUrl) {
            this._setCustomUrlMode();
        } else {
            this._setNormalUrlMode();
        }
    },

    onLoadURL: function() {
        Telerik.Sitefinity.Modules.Pages.Web.UI.Fields.PageUrlMirrorTextField.callBaseMethod(this, 'onLoadURL')
        $addHandler(this.get_urlLabelControl(), "mouseover", this._onUrlMouseOverDelegate);
        $addHandler(this.get_urlLabelControl(), "mouseleave", this._onUrlMouseLeaveDelegate);
    },

    get_value: function() { 
        var value = Telerik.Sitefinity.Modules.Pages.Web.UI.Fields.PageUrlMirrorTextField.callBaseMethod(this, 'get_value');
        if (this._isCustomUrl) {
            return "~/" + value;
        }
        return value;
    },

    set_value: function (value) {
        var trimmedVal = this._trimUrl(value);
        Telerik.Sitefinity.Modules.Pages.Web.UI.Fields.PageUrlMirrorTextField.callBaseMethod(this, 'set_value', [trimmedVal]);
    },

    refreshURL: function () {
        var url = this.getCurrentParentURL();
        var urlLabelElm = this.get_urlLabelControl();
        urlLabelElm.innerHTML = this._trimUrl(url);
    },

    validate: function () {
        if (this._validator && this._isToValidate()) {
            var isValid = true;
            var urlParts = this.get_value().split('/');
            if (urlParts.length > 1 && !this._isCustomUrl) {
                isValid = false;
                this._validator.set_violationMessages([]);
                this._validator.get_violationMessages().push(this.get_customUrlValidationMessage());
            } else {
                for (var i = 0; i < urlParts.length; i++) {
                    isValid = isValid && this._validator.validate(urlParts[i]);
                }
            }
            
            var addWrappingErrorCssClass = this._controlErrorCssClass.length > 0;
            var violationMessageElement = this._validator.get_violationMessageElement();
            if (isValid) {
                if (addWrappingErrorCssClass) {
                    jQuery(this._element).removeClass(this._controlErrorCssClass);
                }
                violationMessageElement.style.display = 'none';
            }
            else {
                // We add the wrapping css class if set                 
                if (addWrappingErrorCssClass) {
                    if (!jQuery(this._element).hasClass(this._controlErrorCssClass)) {
                        jQuery(this._element).addClass(this._controlErrorCssClass);
                    }
                }
                this._expandParentSections(this._element);
                this._showViolationMessageElement(violationMessageElement);

            }
            return isValid;
        }
        return true;
    },

    /* Overridden Methods */

    /* Private Methods */

    _initDelegates: function () {
        this._onCreateCustomUrlButtonClickDelegate = Function.createDelegate(this, this._onCreateCustomUrlButtonClickHandler);
        this._onDefaultStructureButtonClickDelegate = Function.createDelegate(this, this._onDefaultStructureButtonClickHandler);
        this._onUrlMouseOverDelegate = Function.createDelegate(this, this._onUrlMouseOverHandler);
        this._onUrlMouseLeaveDelegate = Function.createDelegate(this, this._onUrlMouseLeaveHandler);
    },

    _addHandlers: function () {
        $addHandler(this.get_defaultStructureButton(), "mousedown", this._onDefaultStructureButtonClickDelegate);
        $addHandler(this.get_createCustomUrlButton(), "mousedown", this._onCreateCustomUrlButtonClickDelegate);
        $addHandler(this.get_changeControl(), "mouseover", this._onUrlMouseOverDelegate);
        $addHandler(this.get_mirroredValueLabel(), "mouseover", this._onUrlMouseOverDelegate);
        $addHandler(this.get_changeControl(), "mouseleave", this._onUrlMouseLeaveDelegate);
        $addHandler(this.get_mirroredValueLabel(), "mouseleave", this._onUrlMouseLeaveDelegate);
    },

    _removeHandlers: function () {
        if (this._onCreateCustomUrlButtonClickDelegate) {
            if (this.get_createCustomUrlButton()) {
                $removeHandler(this.get_createCustomUrlButton(), "mousedown", this._onCreateCustomUrlButtonClickDelegate);
            }
            delete this._onCreateCustomUrlButtonClickDelegate;
        }
        if (this._onDefaultStructureButtonClickDelegate) {
            if (this.set_defaultStructureButton()) {
                $removeHandler(this.get_defaultStructureButton(), "mousedown", this._onDefaultStructureButtonClickDelegate);
            }
            delete this.onDefaultStructureButtonClickDelegate;
        }
        if (this._onUrlMouseOverDelegate) {
            if (this.get_changeControl()) {
                $removeHandler(this.get_changeControl(), "mouseover", this._onUrlMouseOverDelegate);
            }
            if (this.get_mirroredValueLabel()) {
                $removeHandler(this.get_mirroredValueLabel(), "mouseover", this._onUrlMouseOverDelegate);
            }
            if (this.get_urlLabelControl()) {
                $removeHandler(this.get_urlLabelControl(), "mouseover", this._onUrlMouseOverDelegate);
            }
            delete this._onUrlMouseOverDelegate;
        }
        if (this._onUrlMouseLeaveDelegate) {
            if (this.get_changeControl()) {
                $removeHandler(this.get_changeControl(), "mouseleave", this._onUrlMouseLeaveDelegate);
            }
            if (this.get_mirroredValueLabel()) {
                $removeHandler(this.get_mirroredValueLabel(), "mouseleave", this._onUrlMouseLeaveDelegate);
            }
            if (this.get_urlLabelControl()) {
                $removeHandler(this.get_urlLabelControl(), "mouseleave", this._onUrlMouseLeaveDelegate);
            }
            delete this._onUrlMouseLeaveDelegate;
        }
    },

    _setCustomUrlMode: function() {
        this.get_defaultStructureButton().style.display = "";
        this.get_textElement().style.display = "";

        this.get_customText().style.display = "none";
        this.get_changeControl().style.display = "none";
        this.get_mirroredValueLabel().style.display = "none";
        this.get_createCustomUrlButton().style.display = "none";
        this.get_urlLabelControl().innerHTML = "/";
    },

    _setNormalUrlMode: function() {
        this.get_createCustomUrlButton().style.display = "";
        this.get_defaultStructureButton().style.display = "none";
        this.get_customText().style.display = "none";
        this.get_urlLabelControl().innerHTML = this.getCurrentParentURL();
    },

    _setInitialUrlMode: function () {
        this._isCustomUrl = false;
        this.get_createCustomUrlButton().style.display = "none";
        this.get_defaultStructureButton().style.display = "none";
        this.get_customText().style.display = "none";
        this.get_urlLabelControl().style.display = "";
        if (this._dataItem.Parent && this._dataItem.Parent.FullUrl == "~/") {
            this.get_urlLabelControl().innerHTML = "/";
        } else {
            this.get_urlLabelControl().innerHTML = this.getCurrentParentURL();
        }
        var filteredValue = this._getFilteredMirroredControlValue();
        this.set_value(filteredValue);
    },

    _focusTextBox: function () {
        var that = this;
        window.setTimeout(function () {
            that.get_textElement().focus();
        }, 0);
    },

    _setCustomUrl: function(value) {
        this._isCustomUrl = value;
        if (this._isCustomUrl) {
            this._setCustomUrlMode();
        } else {
            this._setNormalUrlMode();
        }
    },

    _trimUrl: function (value) {
        if (value) {
            if (value.indexOf('~/') == 0) {
                return value.substr(2).trim();
            }
            return value.trim();
        }
        return value;
    },
    /* Private Methods */

    /* Event Handlers */

    _onCreateCustomUrlButtonClickHandler: function (sender, args) {
        this._setCustomUrl(true);
        //trim slash
        this.set_value(this.getCurrentParentURL().substr(1) + this.get_textElement().value);
        this._urlButtonClicked = this.get_createCustomUrlButton();
        this._focusTextBox();
    },

    _onDefaultStructureButtonClickHandler: function (sender, args) {
        this._setCustomUrl(false);
        this.set_value(this._getFilteredMirroredControlValue());
        this.get_mirroredValueLabel().innerHTML = this.get_mirroredControlValue();
        this._urlButtonClicked = this.get_defaultStructureButton();
        
        this._focusTextBox();
    },

    _onUrlMouseOverHandler: function (sender, args) {
        this.get_mirroredValueLabel().style.backgroundColor = "#ffff99"
    },

    _onUrlMouseLeaveHandler: function(sender, args) {
        this.get_mirroredValueLabel().style.backgroundColor = "";
    },

    /* Event Handlers */

    /* IRequiresDataItem */

    set_dataItem: function(value, isDefault) { 
        this._dataItem = value;
        if (this._dataItem && this._dataItem.UrlName.indexOf('~/') == 0) {
            this._isCustomUrl = true;
        } else {
            this._isCustomUrl = false;
        }
        if (this._dataItem.WasPublished) {
            this._isToMirror = false;
        }
    },

    /* IRequiresDataItem */

    /* Properties */

    get_customText: function () {
        return this._customText;
    },
    set_customText: function (value) {
        this._customText = value;
    },

    get_createCustomUrlButton: function () {
        return this._createCustomUrlButton;
    },
    set_createCustomUrlButton: function (value) {
        this._createCustomUrlButton = value;
    },

    get_defaultStructureButton: function () {
        return this._defaultStructureButton;
    },
    set_defaultStructureButton: function (value) {
        this._defaultStructureButton = value;
    },

    get_customUrlValidationMessage: function () {
        return this._customUrlValidationMessage;
    },
    set_customUrlValidationMessage: function (value) {
        this._customUrlValidationMessage = value;
    }

    /* Properties */
}
Telerik.Sitefinity.Modules.Pages.Web.UI.Fields.PageUrlMirrorTextField.registerClass("Telerik.Sitefinity.Modules.Pages.Web.UI.Fields.PageUrlMirrorTextField", Telerik.Sitefinity.Modules.Pages.Web.UI.Fields.UrlMirrorTextField, Telerik.Sitefinity.Web.UI.Fields.IRequiresDataItem);
