Type.registerNamespace("Telerik.Sitefinity.Web.UI.Fields.RecurrenceViews");

Telerik.Sitefinity.Web.UI.Fields.RecurrenceViews.RecurrenceTimeView = function (element) {
    Telerik.Sitefinity.Web.UI.Fields.RecurrenceViews.RecurrenceTimeView.initializeBase(this, [element]);

    this._allDayCheckBoxClickDelegate = null;
    this._allDayCheckBox = null;

    this._timePanel = null;
    this._startTimeField = null;
    this._endTimeField = null;
    this._timeZoneIdField = null;

    this._allDayDuration = 24 * 60 * 60 * 1000;
}

Telerik.Sitefinity.Web.UI.Fields.RecurrenceViews.RecurrenceTimeView.prototype = {
    /* --------------------------------- set up and tear down --------------------------------- */
    initialize: function () {
        Telerik.Sitefinity.Web.UI.Fields.RecurrenceViews.RecurrenceTimeView.callBaseMethod(this, 'initialize');

        this._allDayCheckBoxClickDelegate = Function.createDelegate(this, this._allDayCheckBoxClickHandler);
        $addHandler(this.get_allDayCheckBox(), "click", this._allDayCheckBoxClickDelegate);

        var that = this;
        if (this.get_startTimeField()) {
        	this.get_startTimeField().get_datePicker().datepicker().bind('change', function (eventArgs) {
        		that._eventStartFieldValueChangedHandler();
        	});
        }
    },

    dispose: function () {
        if (this._allDayCheckBoxClickDelegate) {
            if (this.get_allDayCheckBox()) {
                $removeHandler(this.get_allDayCheckBox(), "click", this._allDayCheckBoxClickDelegate);
            }
            delete this._allDayCheckBoxClickDelegate;
        }

        Telerik.Sitefinity.Web.UI.Fields.RecurrenceViews.RecurrenceTimeView.callBaseMethod(this, 'dispose');
    },

    /* --------------------------------- public methods --------------------------------- */
    validate: function () {
        var validControls = true;

        validControls = this.get_startTimeField().validate() && validControls;
        validControls = this.get_endTimeField().validate() && validControls;

        return validControls;
    },

    getRange: function () {
        var range = new Telerik.Sitefinity.Web.UI.Fields.RecurrenceRange();

        if (this.get_allDayCheckBox().checked) {
            var startDate = new Date();
            startDate.setHours(0, 0, 0, 0);
            startDate.setMinutes(-startDate.getTimezoneOffset());
            range.set_start(startDate);
            range.set_eventDuration(this._allDayDuration);
        }
        else {
            var start = new Date(this.get_startTimeField().get_value());
            if (start) {
                var calculatedStart = new Date();
                calculatedStart.setHours(start.getHours(), start.getMinutes(), 0, 0);
                range.set_start(calculatedStart);
                var end = new Date(this.get_endTimeField().get_value());
                if (end) {
                    var calculatedEnd = new Date();
                    calculatedEnd.setHours(end.getHours(), end.getMinutes(), 0, 0);
                    while (calculatedStart >= calculatedEnd) {
                        calculatedEnd.setDate(calculatedEnd.getDate() + 1);
                    }
                    var startUtc = new Date(calculatedStart.getTime() + calculatedStart.getTimezoneOffset() * 60000);
                    var endUtc = new Date(calculatedEnd.getTime() + calculatedEnd.getTimezoneOffset() * 60000);

                    range.set_eventDuration(Telerik.Sitefinity.Web.UI.Fields.DateTime.subtract(endUtc, startUtc));
                }
            }
        }

        return range;
    },

    setRange: function (range) {
        this.reset();
        if (!range) {
            return;
        }

        if (range.get_eventDuration() == this._allDayDuration) {
            this.get_allDayCheckBox().checked = true;
            jQuery(this.get_timePanel()).hide();
        }
        else {
            var startDate = range.get_start();
            var startDateTime = new Date();

            var startDateTimeDSTOffset = startDateTime.getTimezoneOffset() - startDate.getTimezoneOffset();

            startDateTime.setHours(startDate.getHours());
            startDateTime.setMinutes(startDate.getMinutes());
            startDateTime.setSeconds(startDate.getSeconds());
            startDateTime.setMilliseconds(startDate.getMilliseconds());

            startDateTime.setMinutes(startDateTime.getMinutes() - startDateTimeDSTOffset);

            this.get_startTimeField().set_value(startDateTime);
            
            var end = Telerik.Sitefinity.Web.UI.Fields.DateTime.add(startDateTime, range.get_eventDuration());
            this.get_endTimeField().set_value(end);
            jQuery(this.get_timePanel()).show();
        }
    },

    reset: function () {
        this.get_allDayCheckBox().checked = false;

        var roundUpTime = function (time, roundByMinutes) {
            var resultTime = new Date(time);
            resultTime.setMilliseconds(Math.round(resultTime.getMilliseconds() / 1000) * 1000);
            resultTime.setSeconds(Math.round(resultTime.getSeconds() / 60) * 60);
            resultTime.setMinutes(Math.round(resultTime.getMinutes() / roundByMinutes) * roundByMinutes);

            if (resultTime < time) {
                resultTime.setMinutes(resultTime.getMinutes() + roundByMinutes);
            }
            return resultTime;
        }

    	var startDate = roundUpTime(new Date(), 30);

    	this.get_startTimeField().set_valueIgnoreTimeZone(startDate);

    	var nowPlusHour = roundUpTime(new Date(), 30);

    	nowPlusHour.setHours(nowPlusHour.getHours() + 1);

        this.get_endTimeField().set_valueIgnoreTimeZone(nowPlusHour);

        var sitefinityTimeZoneId = GetUserPreferences().get_timeZoneId();
        this.get_timeZoneIdField().set_value(sitefinityTimeZoneId);

        jQuery(this.get_timePanel()).toggle(!this.get_allDayCheckBox().checked);
    },

    /* --------------------------------- event handlers --------------------------------- */

    _allDayCheckBoxClickHandler: function (sender, args) {
        jQuery(this.get_timePanel()).toggle(!this.get_allDayCheckBox().checked);
    },

    _eventStartFieldValueChangedHandler: function () {
    	var startDate = this.get_startTimeField().get_value();
    	var currentEventEnd = this.get_endTimeField().get_value();

    	if (startDate && currentEventEnd) {
    		var potentialEventEnd = new Date(startDate);
    		potentialEventEnd.setHours(potentialEventEnd.getHours() + 1);
    		if (potentialEventEnd > currentEventEnd) {
    			this.get_endTimeField().set_value(potentialEventEnd);
    		}
    	}
    },

	/* --------------------------------- private methods --------------------------------- */

    /* --------------------------------- properties --------------------------------- */

    get_allDayCheckBox: function () {
        return this._allDayCheckBox;
    },
    set_allDayCheckBox: function (value) {
        this._allDayCheckBox = value;
    },

    get_timePanel: function () {
        return this._timePanel;
    },
    set_timePanel: function (value) {
        this._timePanel = value;
    },

    get_startTimeField: function () {
        return this._startTimeField;
    },
    set_startTimeField: function (value) {
        this._startTimeField = value;
    },

    get_endTimeField: function () {
        return this._endTimeField;
    },
    set_endTimeField: function (value) {
        this._endTimeField = value;
    },
    get_timeZoneIdField: function () {
        return this._timeZoneIdField;
    },
    set_timeZoneIdField: function (value) {
        this._timeZoneIdField = value;
    },
}

Telerik.Sitefinity.Web.UI.Fields.RecurrenceViews.RecurrenceTimeView.registerClass('Telerik.Sitefinity.Web.UI.Fields.RecurrenceViews.RecurrenceTimeView', Sys.UI.Control);
