Type.registerNamespace("Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets");

Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets.ContentFilteringWidget = function (element) {
    Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets.ContentFilteringWidget.initializeBase(this, [element]);
    this._widgets = [];
    this._widgetIds = [];
    this._propertyNameToFilter = null;
    this._filterCommandName = null;                    
    this._commandDelegate = null;    
}

Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets.ContentFilteringWidget.prototype = {

    initialize: function () {
        Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets.ContentFilteringWidget.callBaseMethod(this, 'initialize');
        Sys.Application.add_load(Function.createDelegate(this, this._loadHandler));               
    },

    dispose: function () {
        Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets.ContentFilteringWidget.callBaseMethod(this, 'dispose');
        if (this._commandDelegate) {
            var widgetCount = this._widgets.length;
            for (var widgetCounter = 0; widgetCounter < widgetCount; widgetCounter++) {
                this._widgets[widgetCounter].remove_command(this._commandDelegate);
            }
            //OnValueChanged
            delete this._commandDelegate;
        }
    },

    /* ------------------------------------ Private functions --------------------------------- */

    //Sets the initial values and configurations for comments master view controls
    _loadHandler: function () {
        this._constructWidgetsFromIds();
        this._subscribeToWidgetEvents();
    },

    //Handles the commands from sidebar filtering widgets
    _commandHandler: function (sender, args) {
        var commandName = args.get_commandName();

        switch (commandName) {            
            case this._filterCommandName:
                var commandArgument = args.get_commandArgument();
                if (commandArgument && commandArgument.contentType) {                    
                    commandArgument.filterExpression = this._propertyNameToFilter + '=="' + commandArgument.contentType + '"';
                    commandArgument.propertyToFilter = this._propertyNameToFilter;
                }
        }

        args.Widget = sender;

        var h = this.get_events().getHandler('command');
        if (h) h(this, args);
    },

    //Subscribe the dynamic generated widgets to add command event
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

    //Construct the dynamic generated widgets.
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

    /* ------------------------------------ Public Methods ----------------------------------- */


    /* ------------------------------------- Properties --------------------------------------- */
  
    // Gets the filter command widget
    get_filterCommandWidget: function () {
        return this._filterCommandWidget;
    },
    // Sets the filter command widget
    set_filterCommandWidget: function (value) {
        this._filterCommandWidget = value;
    }
}

Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets.ContentFilteringWidget.registerClass("Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets.ContentFilteringWidget", Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets.CommandWidget);