Type.registerNamespace("Telerik.Sitefinity.Web.UI");

Telerik.Sitefinity.Web.UI.DateRangeSelector = function (element) {
    Telerik.Sitefinity.Web.UI.DateRangeSelector.initializeBase(this, [element]);

    this._customRangeValue = null;

    this._dateRangesChoiceField = null;
    this._fromDateField = null;
    this._toDateField = null;
    this._datesPanel = null;

    this._dateRangesValueChangedDelegate = null;
    this._onLoadDelegate = null;
}

Telerik.Sitefinity.Web.UI.DateRangeSelector.prototype = {
    /* --------------------------------- set up and tear down --------------------------------- */
    initialize: function () {
        Telerik.Sitefinity.Web.UI.DateRangeSelector.callBaseMethod(this, 'initialize');
        this._dateRangesValueChangedDelegate = Function.createDelegate(this, this._dateRangesValueChangedHandler);
        this._dateRangesChoiceField.add_valueChanged(this._dateRangesValueChangedDelegate);
        jQuery(this._datesPanel).attr("disabled", true);
        this._onLoadDelegate = Function.createDelegate(this, this._onLoadHandler);
        Sys.Application.add_load(this._onLoadDelegate);
    },

    dispose: function () {
        Telerik.Sitefinity.Web.UI.DateRangeSelector.callBaseMethod(this, 'dispose');
        if (this._dateRangesValueChangedDelegate) {
            this._dateRangesChoiceField.remove_valueChanged(this._dateRangesValueChangedDelegate);
        }
    },

    /* --------------------------------- public methods ---------------------------------- */

    // Returns an object of type {From: value, To: value}, or null for all
    get_value: function () {

        var choiceVal = this._dateRangesChoiceField.get_value();

        if (choiceVal == this._customRangeValue) {
            var from = this._fromDateField.get_value();
            var to = this._toDateField.get_value();
            var fmt = "dd MMM, yyyy HH:mm:ss";
            var fromText = "";
            if (from) {
                fromText = from.sitefinityLocaleFormat(fmt);
                from = GetUserPreferences().sitefinityToUniversalDate(from).toUTCString();
            }
            var toText = "";
            if (to) {
                toText = to.sitefinityLocaleFormat(fmt);
                to = GetUserPreferences().sitefinityToUniversalDate(to).toUTCString();
            }
            var text = "";
            if (fromText && toText)
                text = String.format("{0} - {1}", fromText, toText);
            else if (fromText)
                text = "From " + fromText;
            else
                text = "To " + toText;
            return { From: from, To: to, Text: text };
        }
        else if (choiceVal == "") {
            // all dates
            return null;
        }
        else {
            var choiceField = this._dateRangesChoiceField;
            var text = choiceField.get_choices()[choiceField.get_selectedChoicesIndex()].Text;
            return { From: choiceVal, To: null, Text: text };
        }

    },

    // Expects object of type {From: value, To: value}
    set_value: function (value) {
        //this._fromDateField.reset();
        //this._toDateField.reset();
        if (value === undefined || value == null || value == "") {
            this._dateRangesChoiceField.set_value("");
        }
        else if (value.From && !value.To && isNaN(Date.parse(value.From))) {
            this._dateRangesChoiceField.set_value(value.From);
        }
        else {
            this._dateRangesChoiceField.set_value(this._customRangeValue);
            if (value.From) {
                this._fromDateField.set_value(new Date(value.From));
            }
            else {
                this._fromDateField.set_value(null);
            }
            if (value.To) {
                this._toDateField.set_value(new Date(value.To));
            }
            else {
                this._toDateField.set_value(null);
            }
        }
        this._dateRangesValueChangedHandler();
    },

    validate: function () {
        var choiceVal = this._dateRangesChoiceField.get_value();
        if (choiceVal == this._customRangeValue) {
            return (this._fromDateField.validate() && this._toDateField.validate()) && // the two date fields are valid
            (this._fromDateField.get_value() || this._toDateField.get_value()); // and at least one of them has value
        }
        return true;
    },

    /* --------------------------------- event handlers ---------------------------------- */
    // Handles the selection changes in the date range radio buttons
    _dateRangesValueChangedHandler: function (sender, args) {
        if (this._dateRangesChoiceField.get_value() == this._customRangeValue) {
            jQuery(this._datesPanel).removeAttr("disabled");
            //this._fromDateField.get_datePicker().datepicker("enable");
            //this._toDateField.get_datePicker().datepicker("enable");
        }
        else {
            jQuery(this._datesPanel).attr("disabled", true);
            //this._fromDateField.get_datePicker().datepicker("disable");
            //this._toDateField.get_datePicker().datepicker("disable");
        }
    },

    // Handles the page load event
    _onLoadHandler: function (e) {
        //        this._fromDateField.get_datePicker().datepicker("disable");
        //        this._toDateField.get_datePicker().datepicker("disable");
        this._dateRangesValueChangedHandler();
    },

    /* --------------------------------- private methods --------------------------------- */

    /* --------------------------------- properties -------------------------------------- */


    // Gets the choice field with the date ranges 
    get_dateRangesChoiceField: function () { return this._dateRangesChoiceField; },
    // Sets the choice field with the date ranges 
    set_dateRangesChoiceField: function (value) { this._dateRangesChoiceField = value; },

    // Gets the date field for from date
    get_fromDateField: function () { return this._fromDateField; },
    // sets the date field for "from date"
    set_fromDateField: function (value) { this._fromDateField = value; },

    // Gets the date field for "to date"
    get_toDateField: function () { return this._toDateField; },
    // Sets the date field for "to date"
    set_toDateField: function (value) { this._toDateField = value; },

    // Gets the element that contains the date fields
    get_datesPanel: function () { return this._datesPanel; },
    // Sets the element that contains the date fields
    set_datesPanel: function (value) { this._datesPanel = value; }
}

Telerik.Sitefinity.Web.UI.DateRangeSelector.registerClass('Telerik.Sitefinity.Web.UI.DateRangeSelector', Sys.UI.Control);
