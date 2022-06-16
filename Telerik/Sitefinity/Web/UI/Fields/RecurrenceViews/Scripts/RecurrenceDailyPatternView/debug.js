﻿Type.registerNamespace("Telerik.Sitefinity.Web.UI.Fields.RecurrenceViews");

Telerik.Sitefinity.Web.UI.Fields.RecurrenceViews.RecurrenceDailyPatternView = function (element) {
    Telerik.Sitefinity.Web.UI.Fields.RecurrenceViews.RecurrenceDailyPatternView.initializeBase(this, [element]);

    this._dailyRadio = null;
    this._weekdaysRadio = null;
    this._dailyTextBox = null;
}

Telerik.Sitefinity.Web.UI.Fields.RecurrenceViews.RecurrenceDailyPatternView.prototype = {
    /* --------------------------------- set up and tear down --------------------------------- */
    initialize: function () {
        Telerik.Sitefinity.Web.UI.Fields.RecurrenceViews.RecurrenceDailyPatternView.callBaseMethod(this, 'initialize');
    },

    dispose: function () {
        Telerik.Sitefinity.Web.UI.Fields.RecurrenceViews.RecurrenceDailyPatternView.callBaseMethod(this, 'dispose');
    },

    /* --------------------------------- public methods --------------------------------- */

    getPattern: function () {
        var pattern = new Telerik.Sitefinity.Web.UI.Fields.RecurrencePattern();

        if (this.get_dailyRadio().checked) {
            pattern.set_interval(jQuery(this.get_dailyTextBox()).val());
        }
        else {
            pattern.set_daysOfWeekMask(Telerik.Sitefinity.Web.UI.Fields.RecurrenceDay.WeekDays);
        }

        return pattern;
    },

    setPattern: function (pattern) {
        this.reset();
        if (!pattern) {
            return;
        }

        if (pattern.get_daysOfWeekMask() == Telerik.Sitefinity.Web.UI.Fields.RecurrenceDay.WeekDays) {
            this.get_weekdaysRadio().checked = true;
        }
        else {
            this.get_dailyRadio().checked = true;
            jQuery(this.get_dailyTextBox()).val(pattern.get_interval());
        }
    },

    reset: function () {
        this.get_dailyRadio().checked = true;
        jQuery(this.get_dailyTextBox()).val("1");
    },

    /* --------------------------------- properties --------------------------------- */

    get_dailyRadio: function () {
        return this._dailyRadio;
    },
    set_dailyRadio: function (value) {
        this._dailyRadio = value;
    },

    get_weekdaysRadio: function () {
        return this._weekdaysRadio;
    },
    set_weekdaysRadio: function (value) {
        this._weekdaysRadio = value;
    },

    get_dailyTextBox: function () {
        return this._dailyTextBox;
    },
    set_dailyTextBox: function (value) {
        this._dailyTextBox = value;
    }
}

Telerik.Sitefinity.Web.UI.Fields.RecurrenceViews.RecurrenceDailyPatternView.registerClass('Telerik.Sitefinity.Web.UI.Fields.RecurrenceViews.RecurrenceDailyPatternView', Sys.UI.Control);
