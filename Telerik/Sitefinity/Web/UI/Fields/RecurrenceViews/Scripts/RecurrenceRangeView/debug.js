﻿Type.registerNamespace("Telerik.Sitefinity.Web.UI.Fields.RecurrenceViews");

Telerik.Sitefinity.Web.UI.Fields.RecurrenceViews.RecurrenceRangeView = function (element) {
    Telerik.Sitefinity.Web.UI.Fields.RecurrenceViews.RecurrenceRangeView.initializeBase(this, [element]);

    this._startDateField = null;
    this._endDateField = null;
    this._endAfterRepeatsRadio = null;
    this._repeatsTextBox = null;
    this._repeatUntilRadio = null;
    this._repeatAlwaysRadio = null;

    this._maxDate = new Date("9000/01/01");
    this._maxInt = 2147483647;
}

Telerik.Sitefinity.Web.UI.Fields.RecurrenceViews.RecurrenceRangeView.prototype = {
    /* --------------------------------- set up and tear down --------------------------------- */
    initialize: function () {
    	Telerik.Sitefinity.Web.UI.Fields.RecurrenceViews.RecurrenceRangeView.callBaseMethod(this, 'initialize');

    	var that = this;
    	if (this.get_startDateField()) {
    		this.get_startDateField().get_datePicker().datepicker().bind('change', function (eventArgs) {
    			that._eventStartFieldValueChangedHandler();
    		});
    	}
    },

    dispose: function () {
    	Telerik.Sitefinity.Web.UI.Fields.RecurrenceViews.RecurrenceRangeView.callBaseMethod(this, 'dispose');
    },

    /* --------------------------------- public methods --------------------------------- */
    validate: function () {
        var validControls = true;

        validControls = this.get_startDateField().validate() && validControls;
        if (this.get_repeatUntilRadio().checked) {
            validControls = this.get_endDateField().validate() && validControls;
        }
        return validControls;
    },

    getRange: function () {
        var range = new Telerik.Sitefinity.Web.UI.Fields.RecurrenceRange();

        range.set_start(this.get_startDateField().get_rawValue());
        if (this.get_repeatUntilRadio().checked) {
            range.set_recursUntil(this.get_endDateField().get_rawValue());
        }
        else if (this.get_endAfterRepeatsRadio().checked) {
            range.set_maxOccurrences(jQuery(this.get_repeatsTextBox()).val());
        }

        return range;
    },

    setRange: function (range) {
        this.reset();
        if (!range) {
            this._setEndFieldMinDate();
            return;
        }

        this.get_startDateField().set_value(range.get_start());
        this._setEndFieldMinDate();

        var recursUntilInUtc = range.get_recursUntil();
        recursUntilInUtc = new Date(recursUntilInUtc.getTime() + (recursUntilInUtc.getTimezoneOffset()) * 60 * 1000);
        
        if (recursUntilInUtc.getTime() != this._maxDate.getTime()) {
            this.get_endDateField().set_value(range.get_recursUntil());
            this.get_repeatUntilRadio().checked = true;
        }
        else if (range.get_maxOccurrences() != this._maxInt) {
            jQuery(this.get_repeatsTextBox()).val(range.get_maxOccurrences());
            this.get_endAfterRepeatsRadio().checked = true;
        }
        else {
            this.get_repeatAlwaysRadio().checked = true;
        }
    },

    reset: function () {
        this.get_startDateField().set_value(new Date());
        this.get_endAfterRepeatsRadio().checked = true;
        jQuery(this.get_repeatsTextBox()).val("10");
        this.get_endDateField().reset();
    },

	/* --------------------------------- event handlers --------------------------------- */

    _eventStartFieldValueChangedHandler: function () {
    	var startDate = this.get_startDateField().get_value();
    	var currentEventEnd = this.get_endDateField().get_value();

    	if (startDate && currentEventEnd) {
    		this._setEndFieldMinDate();

    		var potentialEventEnd = new Date(startDate);
    		potentialEventEnd.setDate(potentialEventEnd.getDate() + 1);
    		if (potentialEventEnd > currentEventEnd) {
    			this.get_endDateField().set_value(potentialEventEnd);
    		}
    	}
    },

    //_eventEndFieldDataBoundHandler: function (sender, args) {
    //	this._setEndFieldMinDate();
    //},

	/* --------------------------------- private methods --------------------------------- */

    _setEndFieldMinDate: function () {
    	var startDateValue = this.get_startDateField().get_datePicker().val();
    	var startDate = new Date(startDateValue);
    	if (startDate) {
    	    this.get_endDateField().get_datePicker().datepicker("option", "minDate", startDate);
            
    	}
    },

    /* --------------------------------- properties --------------------------------- */

    get_endDate: function () {
        return this.get_endDateField().get_value();
    },
    set_endDate: function (value) {
        this.get_endDateField().set_value(value);
    },

    get_startDateField: function () {
        return this._startDateField;
    },
    set_startDateField: function (value) {
        this._startDateField = value;
    },

    get_endDateField: function () {
        return this._endDateField;
    },
    set_endDateField: function (value) {
        this._endDateField = value;
    },

    get_endAfterRepeatsRadio: function () {
        return this._endAfterRepeatsRadio;
    },
    set_endAfterRepeatsRadio: function (value) {
        this._endAfterRepeatsRadio = value;
    },

    get_repeatsTextBox: function () {
        return this._repeatsTextBox;
    },
    set_repeatsTextBox: function (value) {
        this._repeatsTextBox = value;
    },

    get_repeatUntilRadio: function () {
        return this._repeatUntilRadio;
    },
    set_repeatUntilRadio: function (value) {
        this._repeatUntilRadio = value;
    },

    get_repeatAlwaysRadio: function () {
        return this._repeatAlwaysRadio;
    },
    set_repeatAlwaysRadio: function (value) {
        this._repeatAlwaysRadio = value;
    },

}

Telerik.Sitefinity.Web.UI.Fields.RecurrenceViews.RecurrenceRangeView.registerClass('Telerik.Sitefinity.Web.UI.Fields.RecurrenceViews.RecurrenceRangeView', Sys.UI.Control);
