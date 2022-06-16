Type.registerNamespace("Telerik.Sitefinity.Web.UI.Fields.RecurrenceViews");

Telerik.Sitefinity.Web.UI.Fields.RecurrenceViews.RecurrenceWeeklyPatternView = function (element) {
    Telerik.Sitefinity.Web.UI.Fields.RecurrenceViews.RecurrenceWeeklyPatternView.initializeBase(this, [element]);

    this._weekIntervalTextBox = null;
    this._mondayCheckBox = null;
    this._tuesdayCheckBox = null;
    this._wednesdayCheckBox = null;
    this._thursdayCheckBox = null;
    this._fridayCheckBox = null;
    this._saturdayCheckBox = null;
    this._sundayCheckBox = null;
}

Telerik.Sitefinity.Web.UI.Fields.RecurrenceViews.RecurrenceWeeklyPatternView.prototype = {
    /* --------------------------------- set up and tear down --------------------------------- */
    initialize: function () {
        Telerik.Sitefinity.Web.UI.Fields.RecurrenceViews.RecurrenceWeeklyPatternView.callBaseMethod(this, 'initialize');
    },

    dispose: function () {
        Telerik.Sitefinity.Web.UI.Fields.RecurrenceViews.RecurrenceWeeklyPatternView.callBaseMethod(this, 'dispose');
    },

    /* --------------------------------- public methods --------------------------------- */

    getPattern: function () {
        var pattern = new Telerik.Sitefinity.Web.UI.Fields.RecurrencePattern();

        pattern.set_interval(jQuery(this.get_weekIntervalTextBox()).val());
        pattern.set_daysOfWeekMask(this._getRecurrenceDay());

        return pattern;
    },

    setPattern: function (pattern) {
        this.reset();
        if (!pattern) {
            return;
        }

        jQuery(this.get_weekIntervalTextBox()).val(pattern.get_interval());
        this._updateCheckBoxes(pattern.get_daysOfWeekMask());
    },

    reset: function () {
        jQuery(this.get_weekIntervalTextBox()).val("1");
        this._updateCheckBoxes(this._getCurrentWeekDay());
    },

    /* --------------------------------- private methods --------------------------------- */

    _updateCheckBoxes: function (recurrenceDay) {
        var $RecurrenceDay = Telerik.Sitefinity.Web.UI.Fields.RecurrenceDay;
        this.get_mondayCheckBox().checked = (recurrenceDay & $RecurrenceDay.Monday) > 0;
        this.get_tuesdayCheckBox().checked = (recurrenceDay & $RecurrenceDay.Tuesday) > 0;
        this.get_wednesdayCheckBox().checked = (recurrenceDay & $RecurrenceDay.Wednesday) > 0;
        this.get_thursdayCheckBox().checked = (recurrenceDay & $RecurrenceDay.Thursday) > 0;
        this.get_fridayCheckBox().checked = (recurrenceDay & $RecurrenceDay.Friday) > 0;
        this.get_saturdayCheckBox().checked = (recurrenceDay & $RecurrenceDay.Saturday) > 0;
        this.get_sundayCheckBox().checked = (recurrenceDay & $RecurrenceDay.Sunday) > 0;
    },

    _getRecurrenceDay: function () {
        var $RecurrenceDay = Telerik.Sitefinity.Web.UI.Fields.RecurrenceDay;
        var recurrenceDay = $RecurrenceDay.None;

        if (this.get_mondayCheckBox().checked) {
            recurrenceDay |= $RecurrenceDay.Monday;
        }
        if (this.get_tuesdayCheckBox().checked) {
            recurrenceDay |= $RecurrenceDay.Tuesday;
        }
        if (this.get_wednesdayCheckBox().checked) {
            recurrenceDay |= $RecurrenceDay.Wednesday;
        }
        if (this.get_thursdayCheckBox().checked) {
            recurrenceDay |= $RecurrenceDay.Thursday;
        }
        if (this.get_fridayCheckBox().checked) {
            recurrenceDay |= $RecurrenceDay.Friday;
        }
        if (this.get_saturdayCheckBox().checked) {
            recurrenceDay |= $RecurrenceDay.Saturday;
        }
        if (this.get_sundayCheckBox().checked) {
            recurrenceDay |= $RecurrenceDay.Sunday;
        }

        return recurrenceDay;
    },

    _getCurrentWeekDay: function () {
        var $RecurrenceDay = Telerik.Sitefinity.Web.UI.Fields.RecurrenceDay;

        var now = new Date();
        var day = now.getDay();

        switch (day) {
            case 0:
                return $RecurrenceDay.Sunday;
            case 1:
                return $RecurrenceDay.Monday;
            case 2:
                return $RecurrenceDay.Tuesday;
            case 3:
                return $RecurrenceDay.Wednesday;
            case 4:
                return $RecurrenceDay.Thursday;
            case 5:
                return $RecurrenceDay.Friday;
            case 6:
                return $RecurrenceDay.Saturday;
            default:
                return 0;
        }
    },

    /* --------------------------------- properties --------------------------------- */

    get_weekIntervalTextBox: function () {
        return this._weekIntervalTextBox;
    },
    set_weekIntervalTextBox: function (value) {
        this._weekIntervalTextBox = value;
    },
    
    get_mondayCheckBox: function () {
        return this._mondayCheckBox;
    },
    set_mondayCheckBox: function (value) {
        this._mondayCheckBox = value;
    },

    get_tuesdayCheckBox: function () {
        return this._tuesdayCheckBox;
    },
    set_tuesdayCheckBox: function (value) {
        this._tuesdayCheckBox = value;
    },

    get_wednesdayCheckBox: function () {
        return this._wednesdayCheckBox;
    },
    set_wednesdayCheckBox: function (value) {
        this._wednesdayCheckBox = value;
    },

    get_thursdayCheckBox: function () {
        return this._thursdayCheckBox;
    },
    set_thursdayCheckBox: function (value) {
        this._thursdayCheckBox = value;
    },

    get_fridayCheckBox: function () {
        return this._fridayCheckBox;
    },
    set_fridayCheckBox: function (value) {
        this._fridayCheckBox = value;
    },

    get_saturdayCheckBox: function () {
        return this._saturdayCheckBox;
    },
    set_saturdayCheckBox: function (value) {
        this._saturdayCheckBox = value;
    },

    get_sundayCheckBox: function () {
        return this._sundayCheckBox;
    },
    set_sundayCheckBox: function (value) {
        this._sundayCheckBox = value;
    }
}

Telerik.Sitefinity.Web.UI.Fields.RecurrenceViews.RecurrenceWeeklyPatternView.registerClass('Telerik.Sitefinity.Web.UI.Fields.RecurrenceViews.RecurrenceWeeklyPatternView', Sys.UI.Control);
