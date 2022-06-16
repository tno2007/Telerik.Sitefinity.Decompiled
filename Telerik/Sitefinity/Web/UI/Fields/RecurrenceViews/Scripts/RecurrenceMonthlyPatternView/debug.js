﻿Type.registerNamespace("Telerik.Sitefinity.Web.UI.Fields.RecurrenceViews");

Telerik.Sitefinity.Web.UI.Fields.RecurrenceViews.RecurrenceMonthlyPatternView = function (element) {
    Telerik.Sitefinity.Web.UI.Fields.RecurrenceViews.RecurrenceMonthlyPatternView.initializeBase(this, [element]);

    this._dayOfMonthRadio = null;
    this._dayOfMonthTextBox = null;
    this._dayOfMonthIntervalTextBox = null;
    this._ordinalDayRadio = null;
    this._ordinalDayDropDown = null;
    this._dayOfWeekDropDown = null;
    this._ordinalDayIntervalTextBox = null;
}

Telerik.Sitefinity.Web.UI.Fields.RecurrenceViews.RecurrenceMonthlyPatternView.prototype = {
    /* --------------------------------- set up and tear down --------------------------------- */
    initialize: function () {
        Telerik.Sitefinity.Web.UI.Fields.RecurrenceViews.RecurrenceMonthlyPatternView.callBaseMethod(this, 'initialize');
    },

    dispose: function () {
        Telerik.Sitefinity.Web.UI.Fields.RecurrenceViews.RecurrenceMonthlyPatternView.callBaseMethod(this, 'dispose');
    },

    /* --------------------------------- public methods --------------------------------- */

    getPattern: function () {
        var pattern = new Telerik.Sitefinity.Web.UI.Fields.RecurrencePattern();

        if (this.get_dayOfMonthRadio().checked) {
            pattern.set_interval(jQuery(this.get_dayOfMonthIntervalTextBox()).val());
            pattern.set_dayOfMonth(jQuery(this.get_dayOfMonthTextBox()).val());
        }
        else {
            pattern.set_interval(jQuery(this.get_ordinalDayIntervalTextBox()).val());

            var selectedDaysOfWeek = jQuery(this.get_dayOfWeekDropDown()).val();
            pattern.set_daysOfWeekMask(Telerik.Sitefinity.Web.UI.Fields.RecurrenceDay[selectedDaysOfWeek]);

            pattern.set_dayOrdinal(jQuery(this.get_ordinalDayDropDown()).val());
        }

        return pattern;
    },

    setPattern: function (pattern) {
        this.reset();
        if (!pattern) {
            return;
        }
        
        if (pattern.get_dayOfMonth()) {
            this.get_dayOfMonthRadio().checked = true;

            jQuery(this.get_dayOfMonthTextBox()).val(pattern.get_dayOfMonth());
            jQuery(this.get_dayOfMonthIntervalTextBox()).val(pattern.get_interval());
        }
        else {
            this.get_ordinalDayRadio().checked = true;

            jQuery(this.get_ordinalDayDropDown()).val(pattern.get_dayOrdinal());
            var daysOfWeek = this._getDayOfWeekMaskName(pattern.get_daysOfWeekMask());
            jQuery(this.get_dayOfWeekDropDown()).val(daysOfWeek);
            jQuery(this.get_ordinalDayIntervalTextBox()).val(pattern.get_interval());
        }
    },

    reset: function () {
        var now = new Date();
        this.get_dayOfMonthRadio().checked = true;
        jQuery(this.get_dayOfMonthTextBox()).val(now.getDate());
        jQuery(this.get_dayOfMonthIntervalTextBox()).val("1");
        jQuery(this.get_ordinalDayDropDown()).val("1");
        jQuery(this.get_dayOfWeekDropDown()).val(this._getCurrentWeekDayValue());
        jQuery(this.get_ordinalDayIntervalTextBox()).val("1");
    },

    /* --------------------------------- private methods --------------------------------- */

    _getCurrentWeekDayValue: function () {
        var now = new Date();
        var day = now.getDay();

        switch (day) {
            case 0:
                return "Sunday";
            case 1:
                return "Monday";
            case 2:
                return "Tuesday";
            case 3:
                return "Wednesday";
            case 4:
                return "Thursday";
            case 5:
                return "Friday";
            case 6:
                return "Saturday";
            default:
                return "Wednesday";
        }
    },

    _getDayOfWeekMaskName: function (value) {
        for (var k in Telerik.Sitefinity.Web.UI.Fields.RecurrenceDay) {
            if (Telerik.Sitefinity.Web.UI.Fields.RecurrenceDay[k] == value) {
                return k;
            }
        }
        return "Wednesday";
    },

    /* --------------------------------- properties --------------------------------- */

    get_dayOfMonthRadio: function () {
        return this._dayOfMonthRadio;
    },
    set_dayOfMonthRadio: function (value) {
        this._dayOfMonthRadio = value;
    },

    get_dayOfMonthTextBox: function () {
        return this._dayOfMonthTextBox;
    },
    set_dayOfMonthTextBox: function (value) {
        this._dayOfMonthTextBox = value;
    },

    get_dayOfMonthIntervalTextBox: function () {
        return this._dayOfMonthIntervalTextBox;
    },
    set_dayOfMonthIntervalTextBox: function (value) {
        this._dayOfMonthIntervalTextBox = value;
    },

    get_ordinalDayRadio: function () {
        return this._ordinalDayRadio;
    },
    set_ordinalDayRadio: function (value) {
        this._ordinalDayRadio = value;
    },

    get_ordinalDayDropDown: function () {
        return this._ordinalDayDropDown;
    },
    set_ordinalDayDropDown: function (value) {
        this._ordinalDayDropDown = value;
    },

    get_dayOfWeekDropDown: function () {
        return this._dayOfWeekDropDown;
    },
    set_dayOfWeekDropDown: function (value) {
        this._dayOfWeekDropDown = value;
    },

    get_ordinalDayIntervalTextBox: function () {
        return this._ordinalDayIntervalTextBox;
    },
    set_ordinalDayIntervalTextBox: function (value) {
        this._ordinalDayIntervalTextBox = value;
    }
}

Telerik.Sitefinity.Web.UI.Fields.RecurrenceViews.RecurrenceMonthlyPatternView.registerClass('Telerik.Sitefinity.Web.UI.Fields.RecurrenceViews.RecurrenceMonthlyPatternView', Sys.UI.Control);
