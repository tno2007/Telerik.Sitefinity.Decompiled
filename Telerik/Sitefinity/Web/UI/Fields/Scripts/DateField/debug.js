/// <reference name="MicrosoftAjax.js"/>
/// <reference name="Telerik.Sitefinity.Resources.Scripts.jquery-1.6.3-vsdoc.js" assembly="Telerik.Sitefinity.Resources"/>
Type.registerNamespace("Telerik.Sitefinity.Web.UI.Fields");

Telerik.Sitefinity.Web.UI.Fields.DateField = function (element) {
    this._element = element;
    this._datePickerId = null;
    this._datePicker = null;
    this._textControl = null;
    this._onLoadDelegate = null;
    this._dateTimeDisplayMode = null;

    //TODO: Set these on the server.
    this._dateFormat = "mm/dd/yy";
    this._timeFormat = "HH:mm";
    this._setDateTimeCommand = null;
    //delegates
    this._datePickerOnPopupClosingDelegate = null;
    this._datePickerOnPopupOpeningDelegate = null;

    Telerik.Sitefinity.Web.UI.Fields.DateField.initializeBase(this, [element]);
}

Telerik.Sitefinity.Web.UI.Fields.DateField.prototype =
{
    initialize: function () {
        Telerik.Sitefinity.Web.UI.Fields.DateField.callBaseMethod(this, "initialize");
        if (this.get_displayMode() == Telerik.Sitefinity.Web.UI.Fields.FieldDisplayMode.Write) {
            if (_userPreferences == null) {
                _userPreferences = new Telerik.Sitefinity.Web.UI.UserPreferences();
            }

            this._onLoadDelegate = Function.createDelegate(this, this.on_load);
            Sys.Application.add_load(this._onLoadDelegate);

            if (this._datePickerOnPopupOpeningDelegate == null) {
                this._datePickerOnPopupOpeningDelegate = Function.createDelegate(this, this._datePickerOnPopupOpeningHandler);
            }

            if (this._datePickerOnPopupClosingDelegate == null) {
                this._datePickerOnPopupClosingDelegate = Function.createDelegate(this, this._datePickerOnPopupClosingHandler);
            }

            this.set_datePickerFormat(this._dateFormat, this._timeFormat);
        }
    },

    dispose: function () {
        if (this._onLoadDelegate) {
            Sys.Application.remove_load(this._onLoadDelegate);
            delete this._onLoadDelegate;
        }

        if (this._datePicker != null) {
            this._datePicker.beforeShow = null;
            this._datePicker.onClose = null;
        }

        if (this._datePickerOnPopupOpeningDelegate) {
            delete this._datePickerOnPopupOpeningDelegate;
        }

        if (this._datePickerOnPopupClosingDelegate) {
            delete this._datePickerOnPopupClosingDelegate;
        }

        Telerik.Sitefinity.Web.UI.Fields.DateField.callBaseMethod(this, "dispose");
    },

    /* --------------------  public methods ----------- */

    // Returns true if the value of the field is changed
    isChanged: function () {
        var originalValue = new Date(this._value);
        var originalDate = new Date(originalValue.getTime());
        var originalDateUtc = originalDate.toUTCString();

        var currentValue = this.get_rawValue(); // This returns null when the date is not chosen or empty
        if (currentValue == null)
            return false;

        var currentDate = new Date(currentValue.getTime());
        var currentDateUtc = currentDate.toUTCString();

        var notChanged = (originalDateUtc == currentDateUtc);
        if (notChanged) {
            return false;
        }
        else {
            return true;
        }
    },

    updateInputValue: function (dateTimePickerInput) {
        if (dateTimePickerInput.val() && this.get_dateTimeDisplayMode() !== Telerik.Sitefinity.Web.UI.Fields.DateFieldDisplayMode.Time) {
            // there is value, but it cannot be parsed by the datepicker and it will throw console errors and not appear
            try {
                $.datepicker._base_parseDate(this._datePicker.datepicker("option", "dateFormat"), dateTimePickerInput.val(), null);
            } catch (err) {
                dateTimePickerInput.val("");
            }
        }
    },

    set_datePickerFormat: function (dFormat, tFormat) {

        this._resetDateTimePickers();

        switch (this.get_dateTimeDisplayMode()) {
            case Telerik.Sitefinity.Web.UI.Fields.DateFieldDisplayMode.DateTime:
                this._setDateTimeMode(dFormat, tFormat);
                break;
            case Telerik.Sitefinity.Web.UI.Fields.DateFieldDisplayMode.Date:
                this._setDateMode(dFormat);
                break;
            case Telerik.Sitefinity.Web.UI.Fields.DateFieldDisplayMode.Time:
                this._setTimeMode(tFormat);
                break;
        }
    },

    // Gets the value of the field control.
    get_value: function () {
        if (this.get_displayMode() === Telerik.Sitefinity.Web.UI.Fields.FieldDisplayMode.Write) {
            var res;
            try{
                res = this._datePicker.datepicker("getDate");
            } catch (err) {}

            if (res) {
                if (this.get_IsUtcOffsetModeClient()) {
                    return GetUserPreferences().sitefinityToUniversalDate(res);
                }
                else {
                    return new Date(res.getTime() - res.getTimezoneOffset() * 60 * 1000);
                }
            }
            else if (this._datePicker[0].value) {
                if (this.get_IsUtcOffsetModeClient()) {
                    this._value = GetUserPreferences().sitefinityToUniversalDate(new Date(this._datePicker.val()));
                }
                else {
                    var selectedValue = new Date(this._datePicker.val());
                    value = new Date(selectedValue.getTime() - selectedValue.getTimezoneOffset() * 60 * 1000);
                    return value;
                }
            }
            else {
                return null;
            }
        }
        return this._value;
    },

    // Sets the value of the field control.
    set_value: function (value) {
        //        var timeOffset = GetUserPreferences().get_timeOffset();
        this._value = value;

        if (this.get_displayMode() === Telerik.Sitefinity.Web.UI.Fields.FieldDisplayMode.Write) {
            if (this._datePicker) {
                if (value === undefined || value === null || value === "") {
                    this._datePicker.datepicker(this._setDateTimeCommand, "");
                    jQuery(this._datePicker).val("");
                }
                else {
                    if (this.get_IsUtcOffsetModeClient()) {
                        this._value = GetUserPreferences().sitefinityToLocalDate(value);
                    }
                    else {
                        var providedUtcOffset = this.get_ProvidedUtcOffset();
                        this._value = new Date(value.getTime() + (value.getTimezoneOffset() + providedUtcOffset) * 60 * 1000);
                    }
                    this._datePicker.datepicker(this._setDateTimeCommand, this._value);
                    this._datePicker.datepicker("hide");
                }
            }
        }
        else {

            if (value === undefined || value === null) {
                if (this._textControl) {
                    this._textControl.innerHTML = "";
                }
            }
            else {
                if (Date.prototype.isPrototypeOf(value)) {
                    if (!this.get_IsUtcOffsetModeClient()) {
                        var providedUtcOffset = this.get_ProvidedUtcOffset();
                        value = new Date(value.getTime() + (value.getTimezoneOffset() + providedUtcOffset) * 60 * 1000);
                    }                    
                }
                
                this._setTextControlValue(value);
            }
        }

        this.raisePropertyChanged("value");
        this._valueChangedHandler();
    },
    //gets the value directly from the date picker without any modifications.
    get_rawValue: function () {
        if (this.get_displayMode() === Telerik.Sitefinity.Web.UI.Fields.FieldDisplayMode.Write) {
            var res = this._datePicker.datepicker("getDate");
            if (res)
                return new Date(res.getTime());
            else if (this._datePicker[0].value)
                return new Date(this._datePicker.val());
        }
        return null;
    },
    // Sets the value of the field control, regardless the provided UTC offset.
    set_valueRegardlessProvidedUtcOffset: function (value) {
        this._value = value;

        if (this.get_displayMode() === Telerik.Sitefinity.Web.UI.Fields.FieldDisplayMode.Write) {
            if (this._datePicker) {
                if (value === undefined || value === null || value === "") {
                    this._datePicker.datepicker(this._setDateTimeCommand, "");
                    jQuery(this._datePicker).val("");
                }
                else {
                    value = new Date(value.getTime() + (value.getTimezoneOffset()) * 60 * 1000);

                    this._datePicker.datepicker(this._setDateTimeCommand, value);
                    this._datePicker.datepicker("hide");
                }
            }
        }
        else {

            if (value === undefined || value === null) {
                if (this._textControl) {
                    this._textControl.innerHTML = "";
                }
            }
            else {
                if (Date.prototype.isPrototypeOf(value)) {
                    value = new Date(value.getTime() + (value.getTimezoneOffset()) * 60 * 1000);
                }
                
                this._setTextControlValue(value);
            }
        }
        this.raisePropertyChanged("value");
        this._valueChangedHandler();
    },

    // Sets the value of the field control without using any time zone offsets.
    set_valueIgnoreTimeZone: function (value) {
        this._value = value;

        if (this.get_displayMode() === Telerik.Sitefinity.Web.UI.Fields.FieldDisplayMode.Write) {
            if (this._datePicker) {
                if (value === undefined || value === null || value === "") {
                    this._datePicker.datepicker(this._setDateTimeCommand, "");
                    jQuery(this._datePicker).val("");
                }
                else {
                    this._datePicker.datepicker(this._setDateTimeCommand, value);
                    this._datePicker.datepicker("hide");
                }
            }
        }
        else {
            if (value === undefined || value === null) {
                if (this._textControl) {
                    this._textControl.innerHTML = "";
                }
            }
            else {
                this._setTextControlValue(value);
            }
        }
        this.raisePropertyChanged("value");
        this._valueChangedHandler();
    },

    reset: function () {
        if (this.get_displayMode() === Telerik.Sitefinity.Web.UI.Fields.FieldDisplayMode.Write) {
            this._datePicker.datepicker(this._setDateTimeCommand, "");
            jQuery(this._datePicker).val("");
        }
        else {
            if (this._textControl) {
                this._textControl.innerHTML = "";
            }
        }
        Telerik.Sitefinity.Web.UI.Fields.DateField.callBaseMethod(this, "reset");
    },

    validate: function () {
        if (this._datePicker) {
            var datePickerInput = this._datePicker.get(0);
            try {
                $.datepicker._base_parseDate(this._datePicker.datepicker("option", "dateFormat"), datePickerInput.value, null);
            } catch(err) {
                this.updateInputValue(jQuery(datePickerInput)); 
            }
        }

         return Telerik.Sitefinity.Web.UI.Fields.DateField.callBaseMethod(this, "validate");
     },

    get_valueRegardingProvidedUtcOffset: function () {
        var val = this.get_value();
        if (val != null) {
            val = new Date(val.getTime() - this.get_ProvidedUtcOffset() * 60 * 1000);
        }
        return val;
    },

    get_ProvidedUtcOffset: function () {
        var providedUtcOffset = 0;

        var dataItem = this.get_dataItem();
        if (dataItem && dataItem[this.get_utcOffsetFieldName()]) {
            providedUtcOffset = dataItem[this.get_utcOffsetFieldName()];
        }
        return providedUtcOffset;
    },

    // Focuses the input element.
    // Overridden from field control
    focus: function () {
        var dateElement = this.get_datePicker();
        if (jQuery(dateElement).is(":visible")) {
            try {
                //this.get_datePicker().focus();
            }
            catch (err) {
            }
        }
    },

    // Blurs the input element.
    // Overridden from field control
    blur: function () {
        var behavior = Sys.UI.Behavior.getBehaviorByName(this._element, "ExpandableExtender");
        if (!behavior.get_originalExpandedState()) {
            var val = this.get_value();
            if (!val) {
                behavior.collapse();
            }
        }
    },

    /* -------------------- events -------------------- */

    /* -------------------- event handlers ------------ */
    //This function will raise 'onPopupOpening' event.
    _datePickerOnPopupOpeningHandler: function (inputField, dateTimePickerIntance) {
        this.updateInputValue(dateTimePickerIntance.input);        
        if (typeof this.get_events == 'function') {
            var h = this.get_events().getHandler('onPopupOpening');
            if (h) h(this, Sys.EventArgs.Empty);
            return Sys.EventArgs.Empty;
        }
    },

    //This function will raise 'onPopupClosing' event.
    _datePickerOnPopupClosingHandler: function (dateText, dateTimePickerIntance) {
        this.updateInputValue(dateTimePickerIntance.input);
        if (typeof this.get_events == 'function') {
            var h = this.get_events().getHandler('onPopupClosing');
            if (h) h(this, Sys.EventArgs.Empty);
            return Sys.EventArgs.Empty;
        }
    },

    // This function allows other objects to subscribe to the 'onPopupOpening' event of the RadDatePicker control via DateField control
    add_datePickerOnPopupOpeningHandler: function (handler) {
        this.get_events().addHandler('onPopupOpening', handler);
    },

    // This function allows other objects to unsubscribe to the 'onPopupOpening' event of the RadDatePicker control via DateField control
    remove_datePickerOnPopupOpeningHandler: function (handler) {
        this.get_events().removeHandler('onPopupOpening', handler);
    },

    // This function allows other objects to subscribe to the 'onPopupClosing' event of the RadDatePicker control via DateField control
    add_datePickerOnPopupClosingHandler: function (handler) {
        this.get_events().addHandler('onPopupClosing', handler);
    },

    // This function allows other objects to unsubscribe to the 'onPopupClosing' event of the RadDatePicker control via DateField control
    remove_datePickerOnPopupClosingHandler: function (handler) {
        this.get_events().removeHandler('onPopupClosing', handler);
    },

    on_load: function () {
        if (this._value) {
            this.set_value(this._value);
        }
    },

    /* -------------------- private methods ----------- */

    _resetDateTimePickers: function () {
        jQuery("#" + this._datePickerId).datetimepicker("destroy");
        jQuery("#" + this._datePickerId).datepicker("destroy");
        jQuery("#" + this._datePickerId).timepicker("destroy");
    },

    _setDateTimeMode: function (dFormat, tFormat) {

        this._datePicker = jQuery("#" + this._datePickerId).datetimepicker({
            dateFormat: dFormat,
            hourGrid: tFormat,
            minuteGrid: 10,
            beforeShow: this._datePickerOnPopupOpeningDelegate,
            onClose: this._datePickerOnPopupClosingDelegate,
            showOn: "focus",
            controlType: "select"
        });

        if (this.get_value() == null && this._datePicker.val()) {
            this._value = GetUserPreferences().sitefinityToUniversalDate(new Date(this._datePicker.val()));
        }

        this._setDateTimeCommand = "setDate";
        if (this.get_IsUtcOffsetModeClient()) {
            this._datePicker.datepicker(this._setDateTimeCommand, this.get_value() ? this.get_value() : "");
        }
        else {
            this.set_value(this.get_valueRegardingProvidedUtcOffset());
        }
    },

    _setDateMode: function (dFormat) {

        this._datePicker = jQuery("#" + this._datePickerId).datepicker({
            dateFormat: dFormat,
            beforeShow: this._datePickerOnPopupOpeningDelegate,
            onClose: this._datePickerOnPopupClosingDelegate,
            showOn: "focus"
        });

        if (this.get_value() == null && this._datePicker.val()) {
            this._value = GetUserPreferences().sitefinityToUniversalDate(new Date(this._datePicker.val()));
        }

        this._setDateTimeCommand = "setDate";
        if (this.get_IsUtcOffsetModeClient()) {
            this._datePicker.datepicker(this._setDateTimeCommand, this.get_value() ? this.get_value() : "");
        }
        else {
            this.set_value(this.get_valueRegardingProvidedUtcOffset());
        }
    },

    _setTimeMode: function (tFormat) {

        this._datePicker = jQuery("#" + this._datePickerId).timepicker({
            hourGrid: tFormat,
            minuteGrid: 10,
            beforeShow: this._datePickerOnPopupOpeningDelegate,
            onClose: this._datePickerOnPopupClosingDelegate,
            showOn: "focus",
            controlType: "select"
        });

        this._setDateTimeCommand = "setDate";
        if (this.get_IsUtcOffsetModeClient()) {
            this._datePicker.datepicker(this._setDateTimeCommand, this.get_value() ? this.get_value() : "");
        }
        else {
            this.set_value(this.get_valueRegardingProvidedUtcOffset());
        }
    },

    _setTextControlValue: function (value) {
        var textControl = this.get_textControl();
        var dataFormat = this.get_dataFormatString();
        if (value && Date.prototype.isPrototypeOf(value)) {
            if (dataFormat) {
                textControl.innerHTML = value.format(dataFormat)
            } else {
                textControl.innerHTML = value.toLocaleDateString();
            }
        } else {
            textControl.innerHTML = value;
        }
    },

    /* -------------------- properties ---------------- */

    get_dataItem: function (value) {
        return this._dataItem;
    },

    // inherited from IRequiresDataItem
    set_dataItem: function (value) {
        this._dataItem = value;
    },

    get_datePicker: function () {
        return this._datePicker;
    },

    set_datePicker: function (value) {
        this._datePicker = value;
    },

    get_textControl: function () {
        return this._textControl;
    },

    set_textControl: function (value) {
        this._textControl = value;
    },

    get_datePickerId: function () {
        return this._datePickerId;
    },

    set_datePickerId: function (value) {
        this._datePickerId = value;
    },

    get_dateFormat: function () {
        return this._dateFormat;
    },

    set_dateFormat: function (value) {
        this._dateFormat = value;
    },

    get_timeFormat: function () {
        return this._timeFormat;
    },

    set_timeFormat: function (value) {
        this._timeFormat = value;
    },

    get_dateTimeDisplayMode: function () {
        return this._dateTimeDisplayMode;
    },

    set_dateTimeDisplayMode: function (value) {
        this._dateTimeDisplayMode = value;
    },

    get_utcOffsetMode: function () {
        return this._utcOffsetMode;
    },

    set_utcOffsetMode: function (value) {
        this._utcOffsetMode = value;
    },

    get_utcOffsetFieldName: function () {
        return this._utcOffsetFieldName;
    },

    set_utcOffsetFieldName: function (value) {
        this._utcOffsetFieldName = value;
    },

    get_isLocalizable: function () {
        return this._isLocalizable;
    },

    set_isLocalizable: function (value) {
        this._isLocalizable = value;
    },

    get_IsUtcOffsetModeClient: function () {
        return this.get_utcOffsetMode() == Telerik.Sitefinity.Web.UI.Fields.UtcOffsetMode.Client;
    },
};

Telerik.Sitefinity.Web.UI.Fields.DateField.registerClass("Telerik.Sitefinity.Web.UI.Fields.DateField", Telerik.Sitefinity.Web.UI.Fields.FieldControl, Telerik.Sitefinity.Web.UI.Fields.IRequiresDataItem);
