﻿Type.registerNamespace("Telerik.Sitefinity.Web.UI.Backend.Elements");

Telerik.Sitefinity.Web.UI.Backend.Elements.WidgetBar = function (element) {    
    this._widgetIds = null;
    this._sectionIdMappings = null;
    this._widgets = [];
    this._sectionControls = [];
    this._commandDelegate = null;
    Telerik.Sitefinity.Web.UI.Backend.Elements.WidgetBar.initializeBase(this, [element]);
}

Telerik.Sitefinity.Web.UI.Backend.Elements.WidgetBar.prototype = {

    initialize: function () {
        Telerik.Sitefinity.Web.UI.Backend.Elements.WidgetBar.callBaseMethod(this, 'initialize');
        // subscribe to load event to load widgets there
        Sys.Application.add_load(Function.createDelegate(this, this._loadHandler));
        // TODO: Render all the widgets already added to the server component and subscribe to their events


    },

    dispose: function () {
        Telerik.Sitefinity.Web.UI.Backend.Elements.WidgetBar.callBaseMethod(this, 'dispose');
        if (this._commandDelegate) {
            var widgetCount = this._widgets.length;
            for (var widgetCounter = 0; widgetCounter < widgetCount; widgetCounter++) {
                var widget = this._widgets[widgetCounter];
                if (widget.remove_command) {
                    widget.remove_command(this._commandDelegate);
                }
            }
            delete this._commandDelegate;
        }
        delete this._sectionControls;
        delete this._widgets;
    },

    /* -------------------------- Public Methods ---------------------------------------- */

    refresh: function () {
        this._unsubscribeToWidgetEvents();
        this._subscribeToWidgetEvents();
    },

    addWidget: function (widget) {
        this._widgets.push(widget);
        this.refresh();
    },

    insertWidget: function (widget, index) {
    },

    clearWidgets: function () {
        this._unsubscribeToWidgetEvents();
        this._widgets = [];
    },

    removeWidget: function (index) {
        this._widgets.splice(index, 1);
        this.refresh();
    },

    getWidgetByName: function (name) {
        var length = this._widgets.length;
        for (var i = 0; i < length; i++) {
            var widget = this._widgets[i];
            if (widget.get_name() == name)
                return widget;
        }
        return null;
    },

    getWidgetByCommandName: function (commandName) {
        return this._getWidgetByProp(commandName, function () {
            if (typeof this.get_commandName == "function")
                return this.get_commandName();
            return null;
        });
    },

    getWidgetByProp: function (value, selector) {
        return this._getWidgetByProp(value, selector);
    },

    getAllWidgets: function () {
        return this._widgets;
    },

    // Shows all section elements.
    showAllSections: function () {
        this._setElementsDisplayStyle(this.get_sections(), '');
    },

    // Shows all section elements except those wich Ids are passed as params.
    showSectionsExcept: function (excludedSectionIds) {
        excludedSectionIds = this._createHashSetFromArray(excludedSectionIds);
        this._setElementsDisplayStyle(this.get_sections(), '', excludedSectionIds, 'none');
    },

    // Hides all section elements.
    hideAllSections: function () {
        this._setElementsDisplayStyle(this.get_sections(), 'none');
    },

    // Hides all section elements except those wich Ids are passed as params.
    hideSectionsExcept: function (excludedSectionIds) {
        excludedSectionIds = this._createHashSetFromArray(excludedSectionIds);
        this._setElementsDisplayStyle(this.get_sections(), 'none', excludedSectionIds, '');
    },

    add_command: function (handler) {
        this.get_events().addHandler('command', handler);
    },

    remove_command: function (handler) {
        this.get_events().removeHandler('command', handler);
    },

    getSectionById: function (id) {
        var length = this.get_sections().length;
        for (var i = 0; i < length; i++) {
            var section = this.get_sections()[i];
            if (section.id == id)
                return section.control;
        }
        return null;
    },

    /* -------------------------- Private functions ---------------------------------- */

    _loadHandler: function () {
        // TODO: load widgets from ids
        this._getWidgetsFromServer();
        this._subscribeToWidgetEvents();
    },

    _getWidgetsFromServer: function () {
        if (this._widgetIds) {
            var widgetIdLength = this._widgetIds.length;
            for (var widgetIdCounter = 0; widgetIdCounter < widgetIdLength; widgetIdCounter++) {
                var widget = $find(this._widgetIds[widgetIdCounter]);
                if (widget !== null) {
                    // add widget to collection
                    this._widgets.push(widget);
                }
            }
        }
    },

    _populateSectionControls: function () {
        this._sectionControls = [];
        if (this._sectionIdMappings) {
            for (var id in this._sectionIdMappings) {
                var sectionIdsLength = this._sectionIdMappings.length;
                var section = $get(this._sectionIdMappings[id]);
                if (section) {
                    this._sectionControls.push({ "control": section, "id": id });
                }
            }
        }
    },

    _subscribeToWidgetEvents: function () {
        var widgetCount = this._widgets.length;
        for (var widgetCounter = 0; widgetCounter < widgetCount; widgetCounter++) {
            var widget = this._widgets[widgetCounter];
            if (widget !== null) {
                if (this._commandDelegate == null) {
                    this._commandDelegate = Function.createDelegate(this, this._commandHandler);
                }
                if (Object.getType(widget).implementsInterface(Telerik.Sitefinity.UI.ICommandWidget)) {
                    widget.add_command(this._commandDelegate);
                }
            }
        }
    },

    _unsubscribeToWidgetEvents: function () {
        var widgetCount = this._widgets.length;
        if (widgetCount > 0) {
            if (this._commandDelegate == null) {
                this._commandDelegate = Function.createDelegate(this, this._commandHandler);
            }
            for (var widgetCounter = 0; widgetCounter < widgetCount; widgetCounter++) {
                var widget = this._widgets[widgetCounter];
                if (widget !== null) {

                    widget.remove_command(this._commandDelegate);
                }
            }
        }
    },

    _setElementsDisplayStyle: function (domElements, displayStyleValue, excludedIds, excludedElementsDisplayStyle) {
        if (domElements) {
            for (var i = 0, domElementsLenght = domElements.length; i < domElementsLenght; i++) {
                //check if the current domElement is in the excludedIds dictionary.
                if (excludedIds) {
                    if (domElements[i].id in excludedIds && excludedElementsDisplayStyle !== null && excludedElementsDisplayStyle !== undefined) {
                        this._setElementVisibility(domElements[i].control, excludedElementsDisplayStyle);
                        continue;
                    }
                }
                this._setElementVisibility(domElements[i].control, displayStyleValue);
            }
        }
    },

    _setElementVisibility: function (element, displayStyleValue) {
        if (element.style.display != displayStyleValue) {
            var widgetsHere = this._widgets;
            if (widgetsHere) {
                for (var i = 0; i < widgetsHere.length; i++) {
                    var widget = widgetsHere[i];
                    if (Object.getTypeName(widget) == "Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets.DynamicCommandWidget") {
                        if ($(widget.get_element()).parents("[id = " + element.id + "]").length > 0) {
                            widget.chnageVisibility(displayStyleValue != 'none');
                        }
                    }

                }
            }
            element.style.display = displayStyleValue;
        }
    },

    _createHashSetFromArray: function (array) {
        var hashSet = {};
        var i = 0;
        while (array[i]) {
            hashSet[array[i]] = 1;
            i++;
        }
        return hashSet;
    },

    _commandHandler: function (sender, args) {
        // handles the command event of CommandWidgets and calles the handlers for the WidgetBar's command event
        var commandName = args.get_commandName();
        var commandArg = args.get_commandArgument();
        if (!args.get_cancel()) {
            switch (commandName) {
                case "showAllSections":
                    this.showAllSections();
                    args.set_cancel(true);
                    break;
                case "hideAllSections":
                    this.hideAllSections();
                    args.set_cancel(true);
                    break;
                case "showSectionsExcept":
                    this.showSectionsExcept(commandArg.sectionIds);
                    args.set_cancel(true);
                    break;
                case "hideSectionsExcept":
                    this.hideSectionsExcept(commandArg.sectionIds);
                    args.set_cancel(true);
                    break;
                case "showSectionsExceptAndResetFilter":
                    this.showSectionsExcept(commandArg.sectionIds);
                    break;
            }
        }

        //Add the widget that called the command to the arguments (only if no widget is already set)
        if (!args.Widget) {
            if (Object.getType(sender).implementsInterface(Telerik.Sitefinity.UI.IWidget)) {
                args.Widget = sender;
            }
        }

        var h = this.get_events().getHandler('command');
        if (h) h(this, args);
    },

    _getWidgetByProp: function (value, selector) {
        var length = this._widgets.length, i = 0, widget, callVal;
        if (typeof selector == "string") {
            for (; i < length; i++) {
                widget = this._widgets[i];
                if (widget[selector] === value)
                    return widget;
            }
        }
        else if (typeof selector == "function") {
            for (; i < length; i++) {
                widget = this._widgets[i];
                try {
                    callVal = selector.call(widget);
                } catch (e) {
                    callVal = undefined;
                }
                if (callVal === value)
                    return widget;
            }
        }
        return null;
    },

    /* -------------------------- Properties ---------------------------------------- */

    get_sections: function () {
        if (!this._sectionControls || (this._sectionControls.length < 1)) {
            this._populateSectionControls();
        }
        return this._sectionControls;
    },

    get_widgets: function () {
        return this._widgets;
    },

    get_sectionIdMappings: function () {
        return this._sectionIdMappings;
    },
    set_sectionIdMappings: function (value) {
        this._sectionIdMappings = value;
    },
    get_widgetIds: function () {
        return this._widgetIds;
    },
    set_widgetIds: function (value) {
        this._widgetIds = value;
    }
}

Telerik.Sitefinity.Web.UI.Backend.Elements.WidgetBar.registerClass("Telerik.Sitefinity.Web.UI.Backend.Elements.WidgetBar", Sys.UI.Control);