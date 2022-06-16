Type.registerNamespace("Telerik.Sitefinity.Web.UI.Fields.RecurrenceViews");

Telerik.Sitefinity.Web.UI.Fields.RecurrenceViews.RecurrenceYearlyPatternView = function (element) {
    Telerik.Sitefinity.Web.UI.Fields.RecurrenceViews.RecurrenceYearlyPatternView.initializeBase(this, [element]);

    this._dayOfMonthRadio = null;
    this._dayOfMonthTextBox = null;
    this._ordinalDayRadio = null;
    this._ordinalDayDropDown = null;
    this._dayOfWeekDropDown = null;
    this._monthsDropDown = null;
    this._ordinalMonthsDropDown = null;
}

Telerik.Sitefinity.Web.UI.Fields.RecurrenceViews.RecurrenceYearlyPatternView.prototype = {
    /* --------------------------------- set up and tear down --------------------------------- */
    initialize: function () {
        Telerik.Sitefinity.Web.UI.Fields.RecurrenceViews.RecurrenceYearlyPatternView.callBaseMethod(this, 'initialize');
    },

    dispose: function () {
        Telerik.Sitefinity.Web.UI.Fields.RecurrenceViews.RecurrenceYearlyPatternView.callBaseMethod(this, 'dispose');
    },

    /* --------------------------------- public methods --------------------------------- */

    getPattern: function () {
        var pattern = new Telerik.Sitefinity.Web.UI.Fields.RecurrencePattern();

        if (this.get_dayOfMonthRadio().checked) {
            pattern.set_dayOfMonth(jQuery(this.get_dayOfMonthTextBox()).val());

            var selectedMonth = jQuery(this.get_monthsDropDown()).val();
            pattern.set_month(Telerik.Sitefinity.Web.UI.Fields.RecurrenceMonth[selectedMonth]);
        }
        else {
            var selectedDaysOfWeek = jQuery(this.get_dayOfWeekDropDown()).val();
            pattern.set_daysOfWeekMask(Telerik.Sitefinity.Web.UI.Fields.RecurrenceDay[selectedDaysOfWeek]);

            pattern.set_dayOrdinal(jQuery(this.get_ordinalDayDropDown()).val());

            var selectedMonth = jQuery(this.get_ordinalMonthsDropDown()).val();
            pattern.set_month(Telerik.Sitefinity.Web.UI.Fields.RecurrenceMonth[selectedMonth]);
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
            
            var monthName = this._getMonthName(pattern.get_month());
            jQuery(this.get_monthsDropDown()).val(monthName);
        }
        else {
            this.get_ordinalDayRadio().checked = true;

            jQuery(this.get_ordinalDayDropDown()).val(pattern.get_dayOrdinal());
            var daysOfWeek = this._getDayOfWeekMaskName(pattern.get_daysOfWeekMask());
            jQuery(this.get_dayOfWeekDropDown()).val(daysOfWeek);
            
            var monthName = this._getMonthName(pattern.get_month());
            jQuery(this.get_ordinalMonthsDropDown()).val(monthName);
        }
    },

    reset: function () {
        var now = new Date();
        this.get_dayOfMonthRadio().checked = true;
        jQuery(this.get_dayOfMonthTextBox()).val(now.getDate());
        jQuery(this.get_ordinalDayDropDown()).val("1");
        jQuery(this.get_dayOfWeekDropDown()).val(this._getCurrentWeekDayValue());
        jQuery(this.get_monthsDropDown()).val(this._getCurrentMonthValue());
        jQuery(this.get_ordinalMonthsDropDown()).val(this._getCurrentMonthValue());
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

    _getCurrentMonthValue: function () {
        var now = new Date();
        var month = now.getMonth();
        
        switch (month) {
            case 0:
                return "January";
            case 1:
                return "February";
            case 2:
                return "March";
            case 3:
                return "April";
            case 4:
                return "May";
            case 5:
                return "June";
            case 6:
                return "July";
            case 7:
                return "August";
            case 8:
                return "September";
            case 9:
                return "October";
            case 10:
                return "November";
            case 11:
                return "December";
            default:
                return "February";
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

    _getMonthName: function (value) {
        for (var k in Telerik.Sitefinity.Web.UI.Fields.RecurrenceMonth) {
            if (Telerik.Sitefinity.Web.UI.Fields.RecurrenceMonth[k] == value) {
                return k;
            }
        }
        return "February";
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

    get_monthsDropDown: function () {
        return this._monthsDropDown;
    },
    set_monthsDropDown: function (value) {
        this._monthsDropDown = value;
    },

    get_ordinalMonthsDropDown: function () {
        return this._ordinalMonthsDropDown;
    },
    set_ordinalMonthsDropDown: function (value) {
        this._ordinalMonthsDropDown = value;
    }
}

Telerik.Sitefinity.Web.UI.Fields.RecurrenceViews.RecurrenceYearlyPatternView.registerClass('Telerik.Sitefinity.Web.UI.Fields.RecurrenceViews.RecurrenceYearlyPatternView', Sys.UI.Control);
