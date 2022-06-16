Type.registerNamespace("Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets");

Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets.DateFilteringWidget = function (element) {
    Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets.DateFilteringWidget.initializeBase(this, [element]);
    this._widgets = [];
    this._widgetIds = [];
    this._customRangePanel = null;
    this._propertyNameToFilter = null;
    this._filterCommandName = null;
    this._customRangeExpander = null;
    this._filterCommandWidget = null;
	this._dateFromId = null;
	this._dateToId = null;
    this._dateFrom = null;
    this._dateTo = null;
    this._messageControl = null;
    this._commandDelegate = null;
    this._pickerValueChangedDelegate = null;

    this._invalidDateRangeMessage = null;
}

Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets.DateFilteringWidget.prototype = {

    initialize: function () {
        Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets.DateFilteringWidget.callBaseMethod(this, 'initialize');
        Sys.Application.add_load(Function.createDelegate(this, this._loadHandler));

        var valueChangedDelegate;
        if (this._pickerValueChangedDelegate == null) {
            this._pickerValueChangedDelegate = Function.createDelegate(this, this._pickerValueChanged);
            valueChangedDelegate = this._pickerValueChangedDelegate;
        }

        this._dateFrom = jQuery("#" + this._dateFromId).datepicker({
            dateFormat: "mm/dd/yy",
            onClose: valueChangedDelegate
        });

        this._dateTo = jQuery("#" + this._dateToId).datepicker({
            dateFormat: "mm/dd/yy",
            onClose: valueChangedDelegate
        });
    },

    dispose: function () {
        Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets.DateFilteringWidget.callBaseMethod(this, 'dispose');
        if (this._commandDelegate) {
            var widgetCount = this._widgets.length;
            for (var widgetCounter = 0; widgetCounter < widgetCount; widgetCounter++) {
                this._widgets[widgetCounter].remove_command(this._commandDelegate);
            }
            //OnValueChanged
            delete this._commandDelegate;
        }

        if (this._pickerValueChangedDelegate) {
            if (this.get_dateFrom()) {
                this.get_dateFrom().onSelect = null;
            }
            if (this.get_dateTo()) {
                this.get_dateTo().onSelect = null;
            }
            delete this._pickerValueChangedDelegate;
        }
    },

    /* ------------------------------------ Private functions --------------------------------- */

    _loadHandler: function () {
        this._constructWidgetsFromIds();
        this._subscribeToWidgetEvents();
    },

    _commandHandler: function (sender, args) {
        var commandName = args.get_commandName();

        switch (commandName) {
            case "showCustomRange":
                this._showCustomRange();
                args.set_cancel(true);
                break;
            case "filterCustomRange":
                var customRangeFilter = this._getCustomRangeFilterArgs();
                if (customRangeFilter) {
                    var dateFrom = this.get_dateFrom().datepicker("getDate");
                    var dateTo =this.get_dateTo().datepicker("getDate");
                    args = new Telerik.Sitefinity.UI.CommandEventArgs(this._filterCommandName, { 'filterExpression': customRangeFilter, dateTo: dateTo.sitefinityLocaleFormat(), dateFrom: dateFrom.sitefinityLocaleFormat() });
                }
                break;
            case this._filterCommandName:
                var commandArgument = args.get_commandArgument();
                if (commandArgument && commandArgument.dateRange) {
                    // Since the dateRange is a TimeSpan we calculating the actual date to use as a filter value.
                    var currDate = new Date();
                    var resultDateInMilliseconds = currDate.getTime() - commandArgument.dateRange.TotalMilliseconds;
                    // Constructing the filter command argument.
                    var dateFrom = GetUserPreferences().sitefinityToUniversalDate(new Date(resultDateInMilliseconds)).toUTCString();
                    commandArgument.filterExpression = '(' + this._propertyNameToFilter + '>(' + dateFrom + '))';
                    commandArgument.propertyToFilter = this._propertyNameToFilter;
                    commandArgument.dateFrom = dateFrom;
                }
        }

        args.Widget = sender;

        var h = this.get_events().getHandler('command');
        if (h) h(this, args);
    },

    _subscribeToWidgetEvents: function () {
        var widgetCount = this._widgets.length;
        if (this._commandDelegate == null) {
            this._commandDelegate = Function.createDelegate(this, this._commandHandler);
        }
        for (var widgetCounter = 0; widgetCounter < widgetCount; widgetCounter++) {
            var widget = this._widgets[widgetCounter];
            if (widget !== null && Object.getType(widget).implementsInterface(Telerik.Sitefinity.UI.ICommandWidget)) {
                widget.add_command(this._commandDelegate);
            }
        }
    },

    _constructWidgetsFromIds: function () {
        if (this._widgetIds) {
            var widgetIdLength = this._widgetIds.length;
            for (var widgetIdCounter = 0; widgetIdCounter < widgetIdLength; widgetIdCounter++) {
                var widget = $find(this._widgetIds[widgetIdCounter]);
                if (widget !== null) {
                    this._widgets.push(widget);
                }
            }
        }
    },

    _getCustomRangeFilterArgs: function () {
        // TODO: Make this configurable
        var dateFrom = this.get_dateFrom().datepicker("getDate");
        var dateTo = this.get_dateTo().datepicker("getDate");
        dateTo.setDate(dateTo.getDate() + 1);
        if (!dateFrom && !dateTo) {
            throw "No date range specified!";
        }
        if (dateFrom)
            var dateFromFilterExpression = this._propertyNameToFilter + '>(' + GetUserPreferences().sitefinityToUniversalDate(dateFrom).toUTCString() + ')';
        if (dateTo)
            var dateToFilterExpression = this._propertyNameToFilter + '<(' + GetUserPreferences().sitefinityToUniversalDate(dateTo).toUTCString() + ')';
        if (dateFrom && dateTo) {
            return '(' + dateFromFilterExpression + ' AND ' + dateToFilterExpression + ')';
        }
        else {
            var dateFilterExpression = dateFromFilterExpression || dateToFilterExpression;
            return '(' + dateFilterExpression + ')';
        }
    },

    _pickerValueChanged: function (sender, args) {
        this.get_messageControl().hide();
        if (this.get_dateFrom().datepicker("getDate") == null && this.get_dateTo().datepicker("getDate") == null) {
            this.get_filterCommandWidget().control.disable();
        }
        else {
            this.get_filterCommandWidget().control.enable();
            if (this.get_dateFrom().datepicker("getDate") != null && this.get_dateTo().datepicker("getDate") != null) {
                this._validateRange();
            }
        }
    },

    _showCustomRange: function () {
        $(this.get_customRangePanel()).show();
        $(this.get_customRangeExpander()).hide();
    },

    _validateRange: function () {
        var dateFrom = this.get_dateFrom().datepicker("getDate");
        var dateTo = this.get_dateTo().datepicker("getDate");
        if (dateFrom > dateTo) {
            this.get_messageControl().showNegativeMessage(this.get_invalidDateRangeMessage());
            this.get_filterCommandWidget().control.disable();
        }
    },
    /* ------------------------------------ Public Methods ----------------------------------- */


    /* ------------------------------------- Properties --------------------------------------- */

    // Gets the custom range expander
    get_customRangeExpander: function () {
        return this._customRangeExpander;
    },
    // Sets the custom range expander
    set_customRangeExpander: function (value) {
        this._customRangeExpander = value;
    },

    // Gets the custom range panel
    get_customRangePanel: function () {
        return this._customRangePanel;
    },
    // Sets the custom range panel
    set_customRangePanel: function (value) {
        this._customRangePanel = value;
    },

    // Gets the Id of the dateFrom text field
    get_dateFromId: function () {
        return this._dateFromId;
    },

    // Sets the Id of the dateFrom text field
    set_dateFromId: function (value) {
        this._dateFromId = value;
    },

    // Gets the Id of the dateTo text field
    get_dateToId: function () {
        return this._dateToId;
    },

    // Sets the Id of the dateTo text field
    set_dateToId: function (value) {
        this._dateToId = value;
    },

    // Gets the dateFrom control
    get_dateFrom: function () {
        return this._dateFrom;
    },
    // Sets the dateFrom control
    set_dateFrom: function (value) {
        this._dateFrom = value;
    },

    // Gets the dateTo control
    get_dateTo: function () {
        return this._dateTo;
    },
    // Sets the dateTo control
    set_dateTo: function (value) {
        this._dateTo = value;
    },

    // Gets the filter command widget
    get_filterCommandWidget: function () {
        return this._filterCommandWidget;
    },
    // Sets the filter command widget
    set_filterCommandWidget: function (value) {
        this._filterCommandWidget = value;
    },

    // Gets the message control
    get_messageControl: function () {
        return this._messageControl;
    },
    // Sets the message control
    set_messageControl: function (value) {
        this._messageControl = value;
    },

    // Gets the message to be show when an invalid date range is being selected.
    get_invalidDateRangeMessage: function () {
        return this._invalidDateRangeMessage;
    },
    // Sets the message to be show when an invalid date range is being selected.
    set_invalidDateRangeMessage: function (value) {
        this._invalidDateRangeMessage = value;
    }
}

Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets.DateFilteringWidget.registerClass("Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets.DateFilteringWidget", Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets.CommandWidget);