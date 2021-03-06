Type.registerNamespace("Telerik.Sitefinity.Web.UI.Fields");

Telerik.Sitefinity.Web.UI.Fields.MirrorTextField = function (element) {
    Telerik.Sitefinity.Web.UI.Fields.MirrorTextField.initializeBase(this, [element]);
    this._element = element;
    this._regularExpressionFilter = null;
    this._mirroredControlId = null;
    this._enableChangeButton = true
    this._replaceWith = null;
    this._editMode = false;
    this._regularExpression = null;
    this._mirroredControl = null;

    this._mirroredValueLabel = null;
    this._changeControl = null;
    this._isToShowAsLabel = true;
    this._toLower = true;
    this._trim = true;
    this._prefixText = null;

    this._mirroredControlKeyUpDelegate = null;
    this._mirroredControlKeyDownDelegate = null;
    this._mirroredControlPasteDelegate = null;
    this._subscribeForKeyUpDelegate = null;
    this._subscribeForKeyDownDelegate = null;
    this._subscribeForPasteDelegate = null;
    this._changeClickedDelegate = null;
    this._propertyChangedDelegate = null;
    this._pageLoadDelegate = null;
    this._textBoxFocusLostDelegate = null;

    this._ckeckConditionalMirroring = true;
    this._isToMirror = true;
    this._valueChangedTracking = false;
}

Telerik.Sitefinity.Web.UI.Fields.MirrorTextField.prototype =
{
    initialize: function () {
        Telerik.Sitefinity.Web.UI.Fields.MirrorTextField.callBaseMethod(this, "initialize");
        this._mirroredControlKeyUpDelegate = Function.createDelegate(this, this._mirroredControlKeyUp);
        this._mirroredControlKeyDownDelegate = Function.createDelegate(this, this._mirroredControlKeyDown);
        this._mirroredControlPasteDelegate = Function.createDelegate(this, this._mirroredControlPaste);
        this._subscribeForKeyUpDelegate = Function.createDelegate(this, this._subscribeForKeyUp);
        this._subscribeForKeyDownDelegate = Function.createDelegate(this, this._subscribeForKeyDown);
        this._subscribeForPasteDelegate = Function.createDelegate(this, this._subscribeForPaste);
        this._pageLoadDelegate = Function.createDelegate(this, this.onLoad);
        this._textBoxFocusLostDelegate = Function.createDelegate(this, this._textBoxFocusLost);
        Sys.Application.add_load(this._subscribeForKeyUpDelegate);
        Sys.Application.add_load(this._subscribeForKeyDownDelegate);
        Sys.Application.add_load(this._subscribeForPasteDelegate);
        Sys.Application.add_load(this._pageLoadDelegate);
    },

    dispose: function () {
        if (this._mirroredControl && this._mirroredControlKeyUpDelegate)
            $(this._mirroredControl).unbind("keyup", this._mirroredControlKeyUpDelegate);
        if (this._mirroredControl && this._mirroredControlKeyDownDelegate)
            $(this._mirroredControl).unbind("keydown", this._mirroredControlKeyDownDelegate);

        if (this._mirroredControlKeyUpDelegate)
            delete this._mirroredControlKeyUpDelegate;
        if (this._mirroredControlKeyDownDelegate)
            delete this._mirroredControlKeyDownDelegate;
        if (this._mirroredControlPasteDelegate)
            delete this._mirroredControlPasteDelegate;

        if (this._subscribeForKeyUpDelegate)
            delete this._subscribeForKeyUpDelegate;
        if (this._subscribeForKeyDownDelegate)
            delete this._subscribeForKeyDownDelegate;
        if (this._subscribeForPasteDelegate)
            delete this._subscribeForPasteDelegate;

        if (this.get_changeControl() && this._changeClickedDelegate)
            $removeHandler(this.get_changeControl(), "click", this._changeClickedDelegate);
        if (this._changeClickedDelegate)
            delete this._changeClickedDelegate;

        if (this._propertyChangedDelegate) {
            this.remove_propertyChanged(this._propertyChangedDelegate);
            delete this._propertyChangedDelegate;
        }
        if (this._textBoxFocusLostDelegate)
            delete this._textBoxFocusLostDelegate;
        if (this._pageLoadDelegate)
            delete this._pageLoadDelegate;
        if (this._regularExpression) {
            delete this._regularExpression;
        }
        Telerik.Sitefinity.Web.UI.Fields.MirrorTextField.callBaseMethod(this, "dispose");
    },

    onLoad: function () {
        if (this._enableChangeButton) {
            this._changeClickedDelegate = Function.createDelegate(this, this._changeClicked);
            $addHandler(this.get_changeControl(), "click", this._changeClickedDelegate);
        }

        this._propertyChangedDelegate = Function.createDelegate(this, this._onPropertyChanged);
        this.add_propertyChanged(this._propertyChangedDelegate);
    },

    /* --------------------  public methods ----------- */

    mirror: function () {
        if (this._isToMirror) {
            var filteredMirroredControlValue = this._getFilteredMirroredControlValue();

            //If there is prefix text defined and there is a mirrored value add the prefix to the mirrored value.
            if (this.get_prefixText() && filteredMirroredControlValue)
                filteredMirroredControlValue = this.get_prefixText() + filteredMirroredControlValue;

            this.set_value(filteredMirroredControlValue);
        }
    },

    reset: function () {
        this._set_isToShowAsLabel(this._enableChangeButton);
        this._isToMirror = true;
        this._ckeckConditionalMirroring = true;
        Telerik.Sitefinity.Web.UI.Fields.MirrorTextField.callBaseMethod(this, "reset");
    },

    checkIfIsToMirror: function () {
        this._checkIfIsToMirror();
    },


    /* -------------------- events -------------------- */

    /* -------------------- event handlers ------------ */

    /* -------------------- private methods ----------- */
    _getFilteredMirroredControlValue: function () {
        var mirroredControlValue = this.get_mirroredControlValue()
        if (this._isToFilterValue(mirroredControlValue)) {
            return this._getFilteredlValue(mirroredControlValue);
        }
        else {
            return mirroredControlValue;
        }
    },

    _getFilteredlValue: function (value) {
        var filteredValue = value;
        if (this.get_toLower())
            filteredValue = filteredValue.toLowerCase();
        if (this.get_trim())
            filteredValue = filteredValue.trim();
        if (filteredValue == "")
            return filteredValue;

        var regularExpression = this._get_regularExpression();
        if (regularExpression) {
            var replaceValue = this.get_replaceWith();
            if (replaceValue === undefined || replaceValue === null)
                replaceValue = "";

            if (replaceValue != "")
            {
                var customFilter = this.get_regularExpressionFilter();

                var leftCustomFilter = "^(" + customFilter + ")";
                var regExpression = this._create_regularExpression(leftCustomFilter);
                filteredValue = filteredValue.replace(regExpression, "");

                var rightCustomFilter = "(" + customFilter + ")$";
                regExpression = this._create_regularExpression(rightCustomFilter);
                filteredValue = filteredValue.replace(regExpression, "");
            }

            filteredValue = filteredValue.replace(regularExpression, replaceValue);
        }
        if (this._maxChars && this._maxChars > 0) {
            filteredValue = filteredValue.substring(0, this._maxChars);
        }
        return filteredValue;
    },

    _isToFilterValue: function (value) {
        return value != null && value !== undefined && typeof value === typeof "";
    },

    _create_regularExpression: function (filter) {
        return new XRegExp(filter);
    },

    _get_regularExpression: function () {
        if (!this._regularExpression && this.get_regularExpressionFilter())
            this._regularExpression = new XRegExp(this.get_regularExpressionFilter(), "g");
        return this._regularExpression;
    },

    _subscribeForKeyUp: function () {
        $(this.get_mirroredControl().get_textElement()).bind("keyup", this._mirroredControlKeyUpDelegate)
    },
    _subscribeForKeyDown: function () {
        $(this.get_mirroredControl().get_textElement()).bind("keydown", this._mirroredControlKeyDownDelegate)
    },
    _subscribeForPaste: function () {
        // this handles the case when pasting text in the textbox from the regular browser edit menu or the context menu
        // ... and it will never work in IE unless it learns how to raise DOM events correctly.
        $(this.get_mirroredControl().get_textElement()).bind("input", this._mirroredControlPasteDelegate)
    },

    _mirroredControlKeyUp: function () {
        this.mirror();
        this.calculateCharactersCount();
    },
    _mirroredControlKeyDown: function () {
        if (this._ckeckConditionalMirroring)
            this.checkIfIsToMirror();
        this.calculateCharactersCount();
    },
    _mirroredControlPaste: function (e, a) {
        this.mirror();
        this.calculateCharactersCount();
    },

    _onPropertyChanged: function (sender, eventArgs) {
        var propertyName = eventArgs.get_propertyName();
        if (propertyName == "isToShowAsLabel") {
            this._determinShowMode();
        }
        else if (propertyName == "valueChangedTracking") {
            this._configureValueChangedTracking();
        }
    },

    _configureValueChangedTracking: function () {
        if (this._get_valueChangedTracking()) {
            $addHandler(this.get_textElement(), "blur", this._textBoxFocusLostDelegate);
        }
        else {
            $removeHandler(this.get_textElement(), "blur", this._textBoxFocusLostDelegate);
        }
    },
    _textBoxFocusLost: function () {
        if (this.get_mirroredValueLabel().innerHTML === this.get_value()) {
            this._set_isToShowAsLabel(this._enableChangeButton);
            this._isToMirror = true;
        }
    },
    _changeClicked: function () {
        this._set_isToShowAsLabel(false);
        this._set_valueChangedTracking(true);
        this._isToMirror = false;
    },
    _get_isToShowAsLabel: function () {
        return this._isToShowAsLabel;
    },
    _set_isToShowAsLabel: function (value) {
        if (this._isToShowAsLabel != value) {
            this._isToShowAsLabel = value;
            this.raisePropertyChanged('isToShowAsLabel');
        }
    },
    _determinShowMode: function () {
        if (this._get_isToShowAsLabel()) {
            this._showAsLabel();
        }
        else {
            this._showAsTextBox();
        }
    },
    _showAsLabel: function () {
        this.get_changeControl().style.display = "";
        this.get_mirroredValueLabel().style.display = "";
        this.get_textElement().style.display = "none";
    },
    _showAsTextBox: function () {
        this.get_changeControl().style.display = "none";
        this.get_mirroredValueLabel().style.display = "none";
        this.get_textElement().style.display = "";
        this.set_value(this.get_mirroredValueLabel().innerHTML);
        this.get_textElement().focus();
    },
    _set_valueChangedTracking: function (value) {
        if (this._valueChangedTracking != value) {
            this._valueChangedTracking = value;
            this.raisePropertyChanged('valueChangedTracking');
        }
    },
    _get_valueChangedTracking: function () {
        return this._valueChangedTracking;
    },
    //overriding TextField _set_writeModeValue implementation
    _set_writeModeValue: function (value) {
        if (this._get_isToShowAsLabel()) {
            if (value == null || value === undefined) {
                this.get_mirroredValueLabel().innerHTML = "";
            }
            else {
                this.get_mirroredValueLabel().innerHTML = value;
            }
        }
        Telerik.Sitefinity.Web.UI.Fields.MirrorTextField.callBaseMethod(this, "_set_writeModeValue", [value]);
    },

    //overriding TextField _set_writeModeValue implementation
    _get_writeModeValue: function () {
        if (this._get_isToShowAsLabel())
            return this.get_mirroredValueLabel().innerHTML;
        else
            return Telerik.Sitefinity.Web.UI.Fields.MirrorTextField.callBaseMethod(this, "_get_writeModeValue");
    },
    _checkIfIsToMirror: function () {
        this._ckeckConditionalMirroring = false;
        if (this._currentCondition.toLowerCase() === "displaymode-equal-read") {
            this._isToMirror = false;
        } else {
            var filteredMirroredControlValue = this._getFilteredMirroredControlValue().toLowerCase();
            if (!this.get_prefixText()) {
                if (filteredMirroredControlValue == null || filteredMirroredControlValue == undefined || this.get_value().toLowerCase() !== filteredMirroredControlValue)
                    this._isToMirror = false;
            }
            else {
                if (filteredMirroredControlValue == null || filteredMirroredControlValue == undefined || (this.get_value().toLowerCase() !== filteredMirroredControlValue && (this.get_prefixText() + this.get_value()).toLowerCase() !== filteredMirroredControlValue))
                    this._isToMirror = false;
            }
        }
    },

    _trimChars: function () {
        if (this._maxChars && this._maxChars > 0) {
            var value = Telerik.Sitefinity.Web.UI.Fields.MirrorTextField.callBaseMethod(this, "_get_writeModeValue");
            value = value.substring(0, this._maxChars);
            Telerik.Sitefinity.Web.UI.Fields.MirrorTextField.callBaseMethod(this, "_set_writeModeValue", [value]);
        }
    },

    /* -------------------- properties ---------------- */

    get_mirroredControlValue: function () {
        //TODO make this work for other controls
        if (this.get_mirroredControl() && this.get_mirroredControl().get_textElement) {
            return jQuery(this.get_mirroredControl().get_textElement()).val()
        }
    },

    get_mirroredControl: function () {
        if (!this._mirroredControl) {
            if (this.get_mirroredControlId()) {
                if (!$get_clientId)
                    throw "The page does not have the required FormManager or you are trying to use it before it is initialized.";
                var mirroredControlClientId = $get_clientId(this.get_mirroredControlId());
                if (!mirroredControlClientId)
                    throw "No control with ID:'" + this.get_mirroredControlId() + "' have been registered to the FormManager. Please make sure that the control that you want to mirror calls FormManager.GetCurrent().Register(this);";
                //TODO make this work for other controls
                this._mirroredControl = $find(mirroredControlClientId);
            }
            else {
                throw "The MirroredControlId is not set.";
            }
        }
        return this._mirroredControl;
    },

    get_regularExpressionFilter: function () {
        return this._regularExpressionFilter;
    },
    set_regularExpressionFilter: function (value) {
        this._regularExpressionFilter = value;
    },

    get_replaceWith: function () {
        return this._replaceWith;
    },
    set_replaceWith: function (value) {
        this._replaceWith = value;
    },

    get_mirroredControlId: function () {
        return this._mirroredControlId;
    },
    set_mirroredControlId: function (value) {
        this._mirroredControlId = value;
    },

    get_changeControl: function () {
        return this._changeControl;
    },
    set_changeControl: function (value) {
        this._changeControl = value;
    },

    get_mirroredValueLabel: function () {
        return this._mirroredValueLabel;
    },
    set_mirroredValueLabel: function (value) {
        this._mirroredValueLabel = value;
    },

    get_toLower: function () {
        return this._toLower;
    },
    set_toLower: function (value) {
        this._toLower = value;
    },

    get_trim: function () {
        return this._trim;
    },
    set_trim: function (value) {
        this._trim = value;
    },

    get_prefixText: function () {
        return this._prefixText;
    },
    set_prefixText: function (value) {
        this._prefixText = value;
    },
    get_ckeckConditionalMirroring: function () {
        return this._ckeckConditionalMirroring;
    },
    set_ckeckConditionalMirroring: function (value) {
        this._ckeckConditionalMirroring = value;
    },
    get_isToMirror: function () {
        return this._isToMirror;
    },
    set_isToMirror: function (value) {
        this._isToMirror = value;
    },

    // Gets the default value of the field control.
    get_defaultValue: function () {
        if (this.get_isInitialized()) {
            this._defaultValue = this.get_mirroredControlValue();
        }
        return this._defaultValue;
    }

};

Telerik.Sitefinity.Web.UI.Fields.MirrorTextField.registerClass("Telerik.Sitefinity.Web.UI.Fields.MirrorTextField", Telerik.Sitefinity.Web.UI.Fields.TextField);