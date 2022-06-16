Type.registerNamespace("Telerik.Sitefinity.Web.UI.Fields");

Telerik.Sitefinity.Web.UI.Fields.RecurrencyField = function (element) {
    Telerik.Sitefinity.Web.UI.Fields.RecurrencyField.initializeBase(this, [element]);

    this._element = element;
    this._isInitialized = false;

    this._recurrenceTypeDropDown = null;
    this._recurrenceTypeDropDownChangeDelegate = null;
    this._allDayCheckBoxClickDelegate = null;

    this._repeatOptionsPanel = null;
    this._recurrencePatternMultiPage = null;

    this._dailyPatternPageId = null;
    this._weeklyPatternPageId = null;
    this._monthlyPatternPageId = null;
    this._yearlyPatternPageId = null;

    this._dailyPatternView = null;
    this._weeklyPatternView = null;
    this._monthlyPatternView = null;
    this._yearlyPatternView = null;
    this._rangeView = null;
    this._timeView = null;

    this._recurrenceRule = null;
}

Telerik.Sitefinity.Web.UI.Fields.RecurrencyField.prototype = {
    initialize: function () {
        Telerik.Sitefinity.Web.UI.Fields.RecurrencyField.callBaseMethod(this, "initialize");

        this._isInitialized = true;

        this._recurrenceTypeDropDownChangeDelegate = Function.createDelegate(this, this._recurrenceTypeDropDownChangeHandler);
        $addHandler(this.get_recurrenceTypeDropDown(), "change", this._recurrenceTypeDropDownChangeDelegate);

        this._allDayCheckBoxClickDelegate = Function.createDelegate(this, this._allDayCheckBoxClickHandler);
        $addHandler(this.get_timeView().get_allDayCheckBox(), "click", this._allDayCheckBoxClickDelegate);
    },

    dispose: function () {
        if (this._recurrenceTypeDropDownChangeDelegate) {
            if (this.get_recurrenceTypeDropDown()) {
                $removeHandler(this.get_recurrenceTypeDropDown(), "change", this._recurrenceTypeDropDownChangeDelegate);
            }
            delete this._recurrenceTypeDropDownChangeDelegate;
        }

        if (this._allDayCheckBoxClickDelegate) {
            if (this.get_timeView().get_allDayCheckBox()) {
                $removeHandler(this.get_timeView().get_allDayCheckBox(), "click", this._allDayCheckBoxClickDelegate);
            }
            delete this._allDayCheckBoxClickDelegate;
        }

        Telerik.Sitefinity.Web.UI.Fields.RecurrencyField.callBaseMethod(this, "dispose");
    },

    /* --------------------------------- public methods ---------------------------------- */

    reset: function () {
        this.get_dailyPatternView().reset();
        this.get_weeklyPatternView().reset();
        this.get_monthlyPatternView().reset();
        this.get_yearlyPatternView().reset();
        this.get_rangeView().reset();
        this.get_timeView().reset();

        this.set_value(null);

        Telerik.Sitefinity.Web.UI.Fields.RecurrencyField.callBaseMethod(this, "reset");
    },

    updateFields: function () {
        if (this.get_recurrenceTypeDropDown()) {
            $removeHandler(this.get_recurrenceTypeDropDown(), "change", this._recurrenceTypeDropDownChangeDelegate);
        }

        if (this.get_recurrenceRule()) {
            var pattern = this.get_recurrenceRule().get_pattern();
            var frequency = pattern.get_frequency();
            var frequencyValue = this._privateGetFrequencyModeValue(frequency);
            jQuery(this.get_recurrenceTypeDropDown()).val(frequencyValue);

            switch (frequency) {
                case Telerik.Sitefinity.Web.UI.Fields.RecurrenceFrequency.Daily:
                    this.get_dailyPatternView().setPattern(pattern);
                    break;
                case Telerik.Sitefinity.Web.UI.Fields.RecurrenceFrequency.Weekly:
                    this.get_weeklyPatternView().setPattern(pattern);
                    break;
                case Telerik.Sitefinity.Web.UI.Fields.RecurrenceFrequency.Monthly:
                    this.get_monthlyPatternView().setPattern(pattern);
                    break;
                case Telerik.Sitefinity.Web.UI.Fields.RecurrenceFrequency.Yearly:
                    this.get_yearlyPatternView().setPattern(pattern);
                    break;
            }

            var range = this.get_recurrenceRule().get_range();
            range.set_start(new Date(range.get_start().getTime() - range.get_start().getTimezoneOffset() * 60 * 1000));
            range.set_recursUntil(new Date(range.get_recursUntil().getTime() - range.get_recursUntil().getTimezoneOffset() * 60 * 1000));

            this.get_timeView().setRange(range);
            this.get_rangeView().setRange(range);
            this.get_timeView().get_timeZoneIdField().set_value(this.get_recurrenceRule().get_range().get_timeZoneId());
        }
        else {
            jQuery(this.get_recurrenceTypeDropDown()).val("None");
            this.get_rangeView().setRange(null);
        }

        this._updateVisibility();

        if (this.get_recurrenceTypeDropDown()) {
            $addHandler(this.get_recurrenceTypeDropDown(), "change", this._recurrenceTypeDropDownChangeDelegate);
        }
    },

    /* --------------------------------- event handlers ---------------------------------- */

    _privateGetFrequencyModeValue: function (mode) {
        for (var key in Telerik.Sitefinity.Web.UI.Fields.RecurrenceFrequency) {
            if (Telerik.Sitefinity.Web.UI.Fields.RecurrenceFrequency[key] == mode) {
                return key;
            }
        }
        return "None";
    },

    _recurrenceTypeDropDownChangeHandler: function (sender, args) {
        this._updateRecurrenceRuleObject();

        this._updateVisibility();
    },

    _allDayCheckBoxClickHandler: function (sender, args) {
        jQuery(this.get_timeView().get_timeZoneIdField().get_element()).toggle(!this.get_timeView().get_allDayCheckBox().checked);
    },

    /* --------------------------------- private methods --------------------------------- */

    _updateRecurrenceRuleObject: function () {
        var pattern;
        var selectedValue = jQuery(this.get_recurrenceTypeDropDown()).val();
        var frequency = Telerik.Sitefinity.Web.UI.Fields.RecurrenceFrequency[selectedValue];

        switch (frequency) {
            case Telerik.Sitefinity.Web.UI.Fields.RecurrenceFrequency.Daily:
                pattern = this.get_dailyPatternView().getPattern();
                break;
            case Telerik.Sitefinity.Web.UI.Fields.RecurrenceFrequency.Weekly:
                pattern = this.get_weeklyPatternView().getPattern();
                break;
            case Telerik.Sitefinity.Web.UI.Fields.RecurrenceFrequency.Monthly:
                pattern = this.get_monthlyPatternView().getPattern();
                break;
            case Telerik.Sitefinity.Web.UI.Fields.RecurrenceFrequency.Yearly:
                pattern = this.get_yearlyPatternView().getPattern();
                break;
            default:
                pattern = new Telerik.Sitefinity.Web.UI.Fields.RecurrencePattern();
                break;
        }

        pattern.set_frequency(frequency);

        var range = this.get_rangeView().getRange();
        var time = this.get_timeView().getRange();
        var timeZoneId = this.get_timeView().get_timeZoneIdField().get_value();

        var startDateTime = new Date(time.get_start());
        var endDateTime = new Date(time.get_start())
        var startDate = range.get_start();
        var endDate = range.get_recursUntil();

        var startDateTimeDSTOffset = startDateTime.getTimezoneOffset() - startDate.getTimezoneOffset();
        startDateTime.setFullYear(startDate.getFullYear(), startDate.getMonth(), startDate.getDate());
        startDateTime = new Date(startDateTime.setMinutes(startDateTime.getMinutes() + startDateTimeDSTOffset));

        var endDateTimeDSTOffset = endDateTime.getTimezoneOffset() - endDate.getTimezoneOffset();
        endDateTime.setFullYear(endDate.getFullYear(), endDate.getMonth(), endDate.getDate());
        endDateTime = new Date(endDateTime.setMinutes(endDateTime.getMinutes() + endDateTimeDSTOffset));
        
        range.set_start(startDateTime);
        range.set_recursUntil(endDateTime);
        range.set_eventDuration(time.get_eventDuration());
        range.set_timeZoneId(timeZoneId);
        this._recurrenceRule = Telerik.Sitefinity.Web.UI.Fields.RecurrenceRule.fromPatternAndRange(pattern, range);
    },

    _updateVisibility: function () {
        if (this.get_recurrenceRule()) {
            var defaultEnd = new Date();
            this._allDayCheckBoxClickHandler();
            switch (this.get_recurrenceRule().get_pattern().get_frequency()) {
                case Telerik.Sitefinity.Web.UI.Fields.RecurrenceFrequency.Daily:
                    jQuery(this.get_repeatOptionsPanel()).show();
                    this.get_recurrencePatternMultiPage().findPageViewByID(this.get_dailyPatternPageId()).select();
                    if (!this.get_rangeView().get_endDate()) {
                        defaultEnd.setDate(defaultEnd.getDate() + 1);
                        this.get_rangeView().set_endDate(defaultEnd);
                    }
                    break;
                case Telerik.Sitefinity.Web.UI.Fields.RecurrenceFrequency.Weekly:
                    jQuery(this.get_repeatOptionsPanel()).show();
                    this.get_recurrencePatternMultiPage().findPageViewByID(this.get_weeklyPatternPageId()).select();
                    if (!this.get_rangeView().get_endDate()) {
                        defaultEnd.setDate(defaultEnd.getDate() + 7);
                        this.get_rangeView().set_endDate(defaultEnd);
                    }
                    break;
                case Telerik.Sitefinity.Web.UI.Fields.RecurrenceFrequency.Monthly:
                    jQuery(this.get_repeatOptionsPanel()).show();
                    this.get_recurrencePatternMultiPage().findPageViewByID(this.get_monthlyPatternPageId()).select();
                    if (!this.get_rangeView().get_endDate()) {
                        defaultEnd.setMonth(defaultEnd.getMonth() + 1);
                        this.get_rangeView().set_endDate(defaultEnd);
                    }
                    break;
                case Telerik.Sitefinity.Web.UI.Fields.RecurrenceFrequency.Yearly:
                    jQuery(this.get_repeatOptionsPanel()).show();
                    this.get_recurrencePatternMultiPage().findPageViewByID(this.get_yearlyPatternPageId()).select();
                    if (!this.get_rangeView().get_endDate()) {
                        defaultEnd.setFullYear(defaultEnd.getFullYear() + 1);
                        this.get_rangeView().set_endDate(defaultEnd);
                    }
                    break;
                default:
                    jQuery(this.get_repeatOptionsPanel()).hide();
                    break;
            }
        }
        else {
            jQuery(this.get_repeatOptionsPanel()).hide();
        }
    },

    /* --------------------------------- properties -------------------------------------- */

    validate: function () {
        var isValid = Telerik.Sitefinity.Web.UI.Fields.RecurrencyField.callBaseMethod(this, "validate");

        isValid = this.get_timeView().validate() && isValid;
        isValid = this.get_rangeView().validate() && isValid;
        return isValid;
    },

    get_value: function () {
        this._updateRecurrenceRuleObject();

        if (this.get_recurrenceRule() && this.get_recurrenceRule().get_pattern() &&
            this.get_recurrenceRule().get_pattern().get_frequency() != Telerik.Sitefinity.Web.UI.Fields.RecurrenceFrequency.None) {
            return this.get_recurrenceRule().toString();
        }
        else {
            return null;
        }
    },
    set_value: function (value) {
        this._value = value;

        if (this._isInitialized) {
            this.set_recurrenceRule(Telerik.Sitefinity.Web.UI.Fields.RecurrenceRule.parse(value));
        }
    },

    get_recurrenceRule: function () {
        return this._recurrenceRule;
    },
    set_recurrenceRule: function (value) {
        this._recurrenceRule = value;
        this.updateFields();
    },

    get_recurrenceTypeDropDown: function () {
        return this._recurrenceTypeDropDown;
    },
    set_recurrenceTypeDropDown: function (value) {
        this._recurrenceTypeDropDown = value;
    },

    get_repeatOptionsPanel: function () {
        return this._repeatOptionsPanel;
    },
    set_repeatOptionsPanel: function (value) {
        this._repeatOptionsPanel = value;
    },

    get_recurrencePatternMultiPage: function () {
        return this._recurrencePatternMultiPage;
    },
    set_recurrencePatternMultiPage: function (value) {
        this._recurrencePatternMultiPage = value;
    },

    get_dailyPatternPageId: function () {
        return this._dailyPatternPageId;
    },
    set_dailyPatternPageId: function (value) {
        this._dailyPatternPageId = value;
    },

    get_weeklyPatternPageId: function () {
        return this._weeklyPatternPageId;
    },
    set_weeklyPatternPageId: function (value) {
        this._weeklyPatternPageId = value;
    },

    get_weeklyPatternPageId: function () {
        return this._weeklyPatternPageId;
    },
    set_weeklyPatternPageId: function (value) {
        this._weeklyPatternPageId = value;
    },

    get_monthlyPatternPageId: function () {
        return this._monthlyPatternPageId;
    },
    set_monthlyPatternPageId: function (value) {
        this._monthlyPatternPageId = value;
    },

    get_yearlyPatternPageId: function () {
        return this._yearlyPatternPageId;
    },
    set_yearlyPatternPageId: function (value) {
        this._yearlyPatternPageId = value;
    },

    get_dailyPatternView: function () {
        return this._dailyPatternView;
    },
    set_dailyPatternView: function (value) {
        this._dailyPatternView = value;
    },

    get_weeklyPatternView: function () {
        return this._weeklyPatternView;
    },
    set_weeklyPatternView: function (value) {
        this._weeklyPatternView = value;
    },

    get_monthlyPatternView: function () {
        return this._monthlyPatternView;
    },
    set_monthlyPatternView: function (value) {
        this._monthlyPatternView = value;
    },

    get_yearlyPatternView: function () {
        return this._yearlyPatternView;
    },
    set_yearlyPatternView: function (value) {
        this._yearlyPatternView = value;
    },

    get_rangeView: function () {
        return this._rangeView;
    },
    set_rangeView: function (value) {
        this._rangeView = value;
    },

    get_timeView: function () {
        return this._timeView;
    },
    set_timeView: function (value) {
        this._timeView = value;
    }
};

Telerik.Sitefinity.Web.UI.Fields.RecurrencyField.registerClass("Telerik.Sitefinity.Web.UI.Fields.RecurrencyField", Telerik.Sitefinity.Web.UI.Fields.FieldControl);